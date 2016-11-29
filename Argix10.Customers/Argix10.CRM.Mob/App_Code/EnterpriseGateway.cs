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

namespace Argix.Enterprise {
	//
	public class EnterpriseGateway {
		//Members
        
        //Interface
        public EnterpriseGateway() { }

        public ClientDataset GetClients() {
            //Get clients
            ClientDataset clients = new ClientDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetClients(null);
                client.Close();

                if (ds.Tables["ClientTable"] != null && ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds.Tables["ClientTable"].Select("","CompanyName ASC"));
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }
        public TrackingDataset TrackCartonsForStoreSummary(string clientID,string storeNumber,DateTime from,DateTime to, string by) {
            //Get TL summary
            TrackingDataset cartons = new TrackingDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = null;
                if(by.ToLower() == "delivery")
                    ds = client.TrackCartonsForStoreByDeliveryDate(clientID,storeNumber,from,to,null);
                else
                    ds = client.TrackCartonsForStoreByPickupDate(clientID,storeNumber,from,to,null);
                client.Close();

                //Snag the carton detail
                TrackingDataset detail = new TrackingDataset();
                if (ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) detail.Merge(ds,true,MissingSchemaAction.Ignore);

                //Build a summary by TL
                TrackingDataset summary = new TrackingDataset();
                summary.Merge(detail.CartonDetailForStoreTable.DefaultView.ToTable(true,new string[] { "TL" }));
                foreach (TrackingDataset.CartonDetailForStoreTableRow row in summary.CartonDetailForStoreTable.Rows) {
                    row.CartonCount = detail.CartonDetailForStoreTable.Select("TL='" + row.TL + "'").Length;
                    row.Weight = int.Parse(detail.CartonDetailForStoreTable.Compute("Sum(weight)","TL='" + row.TL + "'").ToString());
                    object minDate = detail.CartonDetailForStoreTable.Compute("Min(PodDate)","TL='" + row.TL + "' AND (IsNull(PodDate,#01/01/1900#) <> #01/01/1900#)");

                    TrackingDataset.CartonDetailForStoreTableRow row0 = (TrackingDataset.CartonDetailForStoreTableRow)(detail.CartonDetailForStoreTable.Select("TL='" + row.TL + "'"))[0];
                    if (minDate != System.DBNull.Value)
                        row.PodDate = DateTime.Parse(minDate.ToString());
                    else {
                        if (!row0.IsOFD1Null()) row.OFD1 = row0.OFD1;
                    }
                    row.Store = row0.Store;
                    row.CBOL = row0.IsCBOLNull() ? "" : row0.CBOL;
                    row.AG = !row0.IsAGNull() ? row0.AG : "";
                    row.AgName = row0.Trf == "N" ? row0.AgName : row0.AgName + " (Transfer)";
                    row.AcceptChanges();
                }
                cartons.Merge(summary);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cartons;
        }
        public TrackingDataset TrackCartonsForStoreDetail(string clientID,string storeNumber,DateTime from,DateTime to,string tl,string by) {
            //Get carton details
            TrackingDataset cartons = new TrackingDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = null;
                if (by.ToLower() == "delivery")
                    ds = client.TrackCartonsForStoreByDeliveryDate(clientID,storeNumber,from,to,null);
                else
                    ds = client.TrackCartonsForStoreByPickupDate(clientID,storeNumber,from,to,null);
                client.Close();

                //Snag the carton detail
                TrackingDataset detail = new TrackingDataset();
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