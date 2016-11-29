using System;
using System.Collections;
using System.Data;
using System.IO;
using System.ServiceModel;
using System.Web.Configuration;

namespace Argix.Enterprise {
	//
	public class TrackingGateway {
		//Members
        private string mMatchClientByARNumber = "";
        public const string ID_ARGIX = "000";
        public const string SEARCHBY_LABELNUMBER = "LabelNumber",SEARCHBY_CARTONNUMBER = "CartonNumber",SEARCHBY_PLATENUMBER = "PlateNumber";
        public const string SEARCHBY_STORE = "Store",SEARCHBY_PRO = "Pro",SEARCHBY_PO = "PO",SEARCHBY_BOL = "BOL";

        public const int SCANTYPE_SORT = 0,SCANTYPE_AGENT = 1,SCANTYPE_STORE = 3;

		//Interface
        public TrackingGateway() { 
            //Constructor
            this.mMatchClientByARNumber = WebConfigurationManager.AppSettings["MatchClientByARNumber"].ToString();
        }
        public CommunicationState ServiceState { get { return new TrackingServiceClient().State; } }
        public string ServiceAddress { get { return new TrackingServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public ClientDataset GetClients(string vendorID) {
            //Return a list of clients filtered by vendorID (vendorID=null returns all Argix clients)
            ClientDataset clients = new ClientDataset();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                DataSet ds = client.GetClients(vendorID);
                if (ds != null) clients.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return clients;
        }
        public ClientDataset GetClients() {
            //Get a list of clients
            ClientDataset clients = new ClientDataset();

            //If user is:
            // Vendor: get list of all it's clients
            // Client: no need to get client's list - fill the drop-down with client's name
            //  Argix: get list of all clients
            MembershipServices membership = new MembershipServices();
            ProfileCommon profile = membership.MemberProfile;
            if (profile.ClientVendorID == TrackingGateway.ID_ARGIX || membership.IsAdmin) {
                clients.Merge(GetClients(null));
            }
            else {
                if(profile.Type.ToLower() == "vendor")
                    clients.Merge(GetClients(profile.ClientVendorID));
                else {
                    //Get the single client from login profile OR get all clients that have same ARNumber
                    ClientDataset _clients = GetClients(null);
                    ClientDataset.ClientTableRow _client = (ClientDataset.ClientTableRow)_clients.ClientTable.Select("ClientID='" + profile.ClientVendorID + "'")[0];
                    if(this.mMatchClientByARNumber.Contains(_client.ARNumber)) {
                        clients.Merge(_clients.ClientTable.Select("ARNumber='" + _client.ARNumber + "'"));
                    }
                    else {
                        clients.ClientTable.AddClientTableRow(profile.ClientVendorID, "", profile.Company, "", "");
                    }
                }
            }
            return clients;
        }
        public ClientDataset GetCustomers(string companyType) {
            //Get customer list
            ClientDataset customers = new ClientDataset();
            TrackingServiceClient client = null;
            try {
                customers.ClientTable.AddClientTableRow(ID_ARGIX,"","Argix Logistics Inc.",companyType,"");

                client = new TrackingServiceClient();
                DataSet ds = client.GetCustomers();
                client.Close();

                if (ds != null && ds.Tables["ClientTable"] != null) {
                    ClientDataset _customers = new ClientDataset();
                    _customers.Merge(ds);
                    customers.Merge(_customers.ClientTable.Select("CompanyType='" + companyType + "'","CompanyName ASC"));
                }
                customers.AcceptChanges();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return customers;
        }
        public StoreDataset GetStoresForSubStore(string subStoreNumber,string clientNumber,string vendorNumber) {
            //Get a list of client\vendor stores for the specified sub-store number
            StoreDataset stores = new StoreDataset();
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                DataSet ds = client.GetStoresForSubStore(subStoreNumber,clientNumber,vendorNumber);
                client.Close();
                if (ds != null && ds.Tables["StoreTable"] != null && ds.Tables["StoreTable"].Rows.Count > 0) {
                    stores.Merge(ds);
                    for (int i = 0;i < stores.StoreTable.Rows.Count;i++)
                        stores.StoreTable[i].DESCRIPTION = stores.StoreTable[i].NAME + " " +
                                                            stores.StoreTable[i].ADDRESSLINE1 + ", " +
                                                            stores.StoreTable[i].ADDRESS_LINE2 + " " +
                                                            stores.StoreTable[i].CITY + ", " +
                                                            stores.StoreTable[i].STATE + " " +
                                                            stores.StoreTable[i].ZIP;
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return stores;
        }

        public TrackingItems TrackCartons(string[] trackingNumbers,string searchBy,string companyType,string companyID) {
            //Get a list of cartons (details) for the specified tracking number (carton or label sequence)
            TrackingItems items = null;
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                //string[] _numbers = trackingNumbers.Split(new char[]{','});
                string _client = companyID != ID_ARGIX && companyType.ToLower() == "client" ? companyID : null;
                string _vendor = companyID != ID_ARGIX && companyType.ToLower() == "vendor" ? companyID : null;
                switch (searchBy) {
                    case SEARCHBY_LABELNUMBER:
                        items = client.TrackCartonsByLabelNumber(trackingNumbers,_client,_vendor);
                        break;
                    case SEARCHBY_CARTONNUMBER:
                        items = client.TrackCartonsByCartonNumber(trackingNumbers,_client,_vendor);
                        break;
                    case SEARCHBY_PLATENUMBER:
                        items = client.TrackCartonsByPlateNumber(trackingNumbers,_client,_vendor);
                        break;
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return items;
        }
        //public TrackingDataset TrackCartonsForStore(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorID,bool searchByPickup) {
        //    //Get a list of cartons (details) for the specified store by pickup or delivery
        //    TrackingDataset cartons = new TrackingDataset();
        //    TrackingServiceClient client = null;
        //    try {
        //        DataSet ds=null;
        //        client = new TrackingServiceClient();
        //        if (searchByPickup) 
        //            ds = client.TrackCartonsForStoreByPickupDate(clientNumber,store,fromDate,toDate,vendorID);
        //        else
        //            ds = client.TrackCartonsForStoreByDeliveryDate(clientNumber,store,fromDate,toDate,vendorID);
        //        client.Close();
        //        if(ds != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) cartons.Merge(ds);
        //    }
        //    catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
        //    catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
        //    catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
        //    return cartons;
        //}
        public TrackingStoreItems TrackCartonsByStoreSummary(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorID,bool searchByPickup) {
            //
            TrackingStoreItems items = null;
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsForStoreSummary(clientNumber,store,fromDate,toDate,vendorID,(searchByPickup ? "pickup" : "delivery"));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return items;
        }
        public TrackingStoreItems TrackCartonsByStoreDetail(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorID,bool searchByPickup,string tlNumber) {
            //
            TrackingStoreItems items = null;
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                items = client.TrackCartonsForStoreDetail(clientNumber,store,fromDate,toDate,vendorID,(searchByPickup ? "pickup" : "delivery"),tlNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return items;
        }
        public TrackingItems TrackCartonsByShipment(string clientNumber,string contractNumber,string searchBy) {
            //Get a list of cartons (details) for the specified client and contract (i.e. PO, PRO) number
            TrackingItems items = null;
            TrackingServiceClient client = null;
            try {
                client = new TrackingServiceClient();
                switch (searchBy) {
                    case SEARCHBY_PO:
                        items = client.TrackCartonsForPO(clientNumber,contractNumber);
                        break;
                    case SEARCHBY_PRO: 
                        items = client.TrackCartonsForPRO(clientNumber,contractNumber);
                        break;
                    case SEARCHBY_BOL:
                        items = client.TrackCartonsForBOL(clientNumber,contractNumber);
                        break;
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TrackingFault> tfe) { client.Abort(); throw new ApplicationException(tfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return items;
        }
    }
}