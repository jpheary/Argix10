using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using Argix.Enterprise;

namespace Argix.Areas.Mobile.Models {
	//
	public class EnterpriseGateway {
		//Members
        
        //Interface
        public EnterpriseGateway() { }

        public TrackingDataSet GetClients() {
            //Get a list of clients
            TrackingDataSet clients = new TrackingDataSet();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();

                //If user is:
                // Vendor: get list of all it's clients
                // Client: no need to get client's list - fill the drop-down with client's name
                // Argix: get list of all clients
                string username = Membership.GetUser().UserName;
                ProfileBase profile = HttpContext.Current.Profile;
                if (profile["ClientVendorID"].ToString() == "000" || Roles.IsUserInRole(username,"administrators")) {
                    DataSet ds = client.GetClients(null);
                    if (ds.Tables["ClientTable"] != null && ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds);
                }
                else {
                    if (profile["Type"].ToString().ToLower() == "vendor") {
                        DataSet ds = client.GetClients(profile["ClientVendorID"].ToString());
                        if (ds.Tables["ClientTable"] != null && ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds);
                    }
                    else
                        clients.ClientTable.AddClientTableRow(profile["ClientVendorID"].ToString(),"",profile["Company"].ToString(),"");
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { client.Close(); }
            return clients;
        }
        public TrackingDataSet TrackCartonsForStoreSummary(string clientID,string storeNumber,DateTime from,DateTime to,string by) {
            //Get TL summary
            TrackingDataSet tlSummary = new TrackingDataSet();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                DataSet ds = null;
                if(by.ToLower() == "delivery")
                    ds = client.TrackCartonsForStoreByDeliveryDate(clientID,storeNumber,from,to,null);
                else
                    ds = client.TrackCartonsForStoreByPickupDate(clientID,storeNumber,from,to,null);
                client.Close();

                //Snag the carton detail
                TrackingDataSet detail = new TrackingDataSet();
                if (ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) detail.Merge(ds,true,MissingSchemaAction.Ignore);

                //Build a summary by TL; start with a dataset of unique 
                TrackingDataSet tls = new TrackingDataSet();
                tls.Merge(detail.CartonDetailForStoreTable.DefaultView.ToTable(true,new string[] { "TL" }));
                foreach (TrackingDataSet.CartonDetailForStoreTableRow tl in tls.CartonDetailForStoreTable.Rows) {
                    //Get one of the cartons from this TL group
                    TrackingDataSet.CartonDetailForStoreTableRow tlCarton0 = (TrackingDataSet.CartonDetailForStoreTableRow)(detail.CartonDetailForStoreTable.Select("TL='" + tl.TL + "'", "TL ASC"))[0];

                    tl.Store = tlCarton0.Store;
                    tl.CartonCount = detail.CartonDetailForStoreTable.Select("TL='" + tl.TL + "'").Length;
                    tl.Weight = int.Parse(detail.CartonDetailForStoreTable.Compute("Sum(weight)","TL='" + tl.TL + "'").ToString());
                    tl.CBOL = tlCarton0.IsCBOLNull() ? "" : tlCarton0.CBOL;
                    object minDate = detail.CartonDetailForStoreTable.Compute("Min(PodDate)","TL='" + tl.TL + "' AND (IsNull(PodDate,#01/01/1900#) <> #01/01/1900#)");
                    if (minDate != System.DBNull.Value)
                        tl.PodDate = DateTime.Parse(minDate.ToString());
                    else {
                        if (!tlCarton0.IsOFD1Null()) tl.OFD1 = tlCarton0.OFD1;
                    }
                    tl.AG = !tlCarton0.IsAGNull() ? tlCarton0.AG : "";
                    tl.AgName = tlCarton0.Trf == "N" ? tlCarton0.AgName : tlCarton0.AgName + " (Transfer)";
                    tl.AcceptChanges();
                }
                tlSummary.Merge(tls);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tlSummary;
        }
        public TrackingDataSet TrackCartonsForStoreDetail(string clientID,string storeNumber,DateTime from,DateTime to,string by,string tl) {
            //Get carton details
            TrackingDataSet cartons = new TrackingDataSet();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                DataSet ds = null;
                if (by.ToLower() == "delivery")
                    ds = client.TrackCartonsForStoreByDeliveryDate(clientID,storeNumber,from,to,null);
                else
                    ds = client.TrackCartonsForStoreByPickupDate(clientID,storeNumber,from,to,null);
                client.Close();

                //Snag the carton detail
                TrackingDataSet detail = new TrackingDataSet();
                if (ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) detail.Merge(ds,true,MissingSchemaAction.Ignore);

                //Get all cartons for the specified tl
                cartons.Merge(detail.CartonDetailForStoreTable.Select("TL='" + tl + "'"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cartons;
        }
    }
}