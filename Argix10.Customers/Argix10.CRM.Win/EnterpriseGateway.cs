using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Threading;

namespace Argix.Enterprise {
	//
	public class EnterpriseGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
        //Interface
        static EnterpriseGateway() { 
            //
            CRMServiceClient client = new CRMServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private EnterpriseGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static EnterpriseDataset GetClients(string vendorID) {
            //
            EnterpriseDataset clients = new EnterpriseDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetClients(vendorID);
                client.Close();

                if(ds.Tables["ClientTable"] != null && ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }
        public static EnterpriseDataset TrackCartonsForStoreByDeliveryDate(string clientID,string store,DateTime fromDate,DateTime toDate,string vendorID) {
            //
            EnterpriseDataset cartons = new EnterpriseDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = null;
                if(store.Trim().Length > 0) ds = client.TrackCartonsForStoreByDeliveryDate(clientID, store, fromDate, toDate, vendorID);
                client.Close();

                if(ds != null && ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) cartons.Merge(ds,true,MissingSchemaAction.Ignore);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cartons;
        }
        public static EnterpriseDataset TrackCartonsForStoreByPickupDate(string clientID,string store,DateTime fromDate,DateTime toDate,string vendorID) {
            //
            EnterpriseDataset cartons = new EnterpriseDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = null;
                if(store.Trim().Length > 0) ds = client.TrackCartonsForStoreByPickupDate(clientID, store, fromDate, toDate, vendorID);
                client.Close();

                if(ds != null && ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) cartons.Merge(ds, true, MissingSchemaAction.Ignore);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cartons;
        }
        public static TrackingItems TrackCartonsByCartonNumber(string[] itemNumbers) {
            //
            TrackingItems items=null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                items = client.TrackCartonsByCartonNumber(itemNumbers,null,null);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return items;
        }
        public static TrackingItems TrackCartonsByLabelNumber(string[] itemNumbers) {
            //
            TrackingItems items=null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                items = client.TrackCartonsByLabelNumber(itemNumbers,null,null);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return items;
        }
    }
}