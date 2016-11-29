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

namespace Argix.Models {
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

        public TrackingItems TrackItemsByCartonNumber(string[] itemNumbers,string clientNumber,string vendorNumber) {
            //Track items by customer carton number
            TrackingItems items = new TrackingItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsByCartonNumber(itemNumbers,clientNumber,vendorNumber);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { client.Close(); }
            return items;
        }
        public TrackingItems TrackItemsByLabelNumber(string[] itemNumbers,string clientNumber,string vendorNumber) {
            //Track items by Argix label number
            TrackingItems items = new TrackingItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsByLabelNumber(itemNumbers,clientNumber,vendorNumber);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { client.Close(); }
            return items;
        }
        public TrackingItems TrackItemsByPlateNumber(string[] itemNumbers,string clientNumber,string vendorNumber) {
            //Track items by Argix label number
            TrackingItems items = new TrackingItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsByPlateNumber(itemNumbers,clientNumber,vendorNumber);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { client.Close(); }
            return items;
        }
        public TrackingItems TrackCartonsForPO(string clientNumber,string PONumber) {
            //Track items by Argix label number
            TrackingItems items = new TrackingItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsForPO(clientNumber,PONumber);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { client.Close(); }
            return items;
        }
        public TrackingItems TrackCartonsForPRO(string clientNumber,string PRONumber) {
            //Track items by Argix label number
            TrackingItems items = new TrackingItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsForPRO(clientNumber,PRONumber);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { client.Close(); }
            return items;
        }

        public TrackingStoreItems TrackCartonsForStoreSummary(string clientID,string storeNumber,DateTime from,DateTime to,string vendorID,string by) {
            //Get TL summary
            TrackingStoreItems items = new TrackingStoreItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsForStoreSummary(clientID,storeNumber,from,to,vendorID,by);
                client.Close();

            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return items;
        }
        public TrackingStoreItems TrackCartonsForStoreDetail(string clientID,string storeNumber,DateTime from,DateTime to,string vendorID,string by,string tl) {
            //Get carton details
            TrackingStoreItems items = new TrackingStoreItems();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsForStoreDetail(clientID,storeNumber,from,to,vendorID,by,tl);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return items;
        }
    }
}