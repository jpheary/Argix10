using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Freight {
	//
	public class FreightGateway {
		//Members
        private static bool _state=false;
        private static string _address="";

		//Interface
        static FreightGateway() { 
            //
            LTLService2Client client = new LTLService2Client();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private FreightGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            LTLService2Client client = null; 
            try {
                client = new LTLService2Client();
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            LTLService2Client client = null;
            try {
                client = new LTLService2Client();
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
        }
        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            LTLService2Client client = null;
            try {
                client = new LTLService2Client();
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return terminal;
        }

        public static LTLQuote2 CreateQuote(LTLQuote2 quote) {
            //Create a new LTL Quote
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                quote = client.CreateQuoteForAdmin(quote);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quote;
        }

        public static FreightDataset ViewLTLClients() {
            FreightDataset clients = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ViewLTLClientsForAdmin();
                if (ds != null && ds.Tables["LTLClientTable"] != null && ds.Tables["LTLClientTable"].Rows.Count > 0) clients.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }
        public static int CreateLTLClient(LTLClient2 ltlClient) {
            //Create a new LTL client
            int clientID = 0;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                clientID = client.CreateLTLClientForAdmin(ltlClient);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clientID;
        }
        public static LTLClient2 ReadLTLClient(int clientID) {
            LTLClient2 ltlClient = null;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                ltlClient = client.ReadLTLClientForAdmin(clientID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return ltlClient;
        }
        public static bool UpdateLTLClient(LTLClient2 ltlClient) {
            //Update an existing LTL client
            bool updated = false;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                updated = client.UpdateLTLClientForAdmin(ltlClient);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool ApproveLTLClient(int clientID, bool approve) {
            bool result = false;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                result = client.ApproveLTLClient(clientID,approve,Environment.UserName);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }
        public static FreightDataset ReadLTLClientList(bool addAll=false) {
            FreightDataset clients = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                if(addAll) clients.LTLClientTable.AddLTLClientTableRow(0, "", "All", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", DateTime.Today, "", DateTime.Today, "", 0, DateTime.Today, "", "");
                DataSet ds = client.ReadLTLClientListForAdmin();
                if(ds != null && ds.Tables["LTLClientTable"] != null && ds.Tables["LTLClientTable"].Rows.Count > 0) clients.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }

        public static FreightDataset ViewLTLShippers(string clientNumber) {
            FreightDataset shippers = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ViewLTLShippersForAdmin(clientNumber);
                if(ds != null && ds.Tables["LTLShipperTable"] != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) shippers.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shippers;
        }
        public static string CreateLTLShipper(LTLShipper2 ltlShipper) {
            //Create a new LTL shipper
            string number = "";
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                number = client.CreateLTLShipperForAdmin(ltlShipper);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return number;
        }
        public static LTLShipper2 ReadLTLShipper(string shipperNumber) {
            LTLShipper2 shipper = new LTLShipper2();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                shipper = client.ReadLTLShipperForAdmin(shipperNumber);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipper;
        }
        public static bool UpdateLTLShipper(LTLShipper2 ltlShipper) {
            //Update an existing LTL shipper
            bool updated = false;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                updated = client.UpdateLTLShipperForAdmin(ltlShipper);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static FreightDataset ReadLTLShippersList(string clientNumber) {
            FreightDataset shippers = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ReadLTLShippersListForAdmin(clientNumber);
                if(ds != null && ds.Tables["LTLShipperTable"] != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) shippers.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shippers;
        }

        public static FreightDataset ViewLTLConsignees(string clientNumber) {
            FreightDataset consignees = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ViewLTLConsigneesForAdmin(clientNumber);
                if(ds != null && ds.Tables["LTLConsigneeTable"] != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) consignees.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignees;
        }
        public static int CreateLTLConsignee(LTLConsignee2 ltlConsignee) {
            //Create a new LTL consignee
            int number = 0;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                number = client.CreateLTLConsigneeForAdmin(ltlConsignee);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return number;
        }
        public static LTLConsignee2 ReadLTLConsignee(int consigneeNumber, string clientNumber) {
            LTLConsignee2 consignee = new LTLConsignee2();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                consignee = client.ReadLTLConsigneeForAdmin(consigneeNumber, clientNumber);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignee;
        }
        public static bool UpdateLTLConsignee(LTLConsignee2 ltlConsignee) {
            //Update an existing LTL consignee
            bool updated = false;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                updated = client.UpdateLTLConsigneeForAdmin(ltlConsignee);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static FreightDataset ReadLTLConsigneesList(string clientNumber) {
            FreightDataset consignees = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ReadLTLConsigneesListForAdmin(clientNumber);
                if(ds != null && ds.Tables["LTLConsigneeTable"] != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) consignees.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignees;
        }

        public static FreightDataset ViewLTLShipments(string clientNumber) {
            //View all LTL shipments for the specified client
            FreightDataset shipments = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ViewLTLShipmentsForDispatch(clientNumber);
                if(ds != null && ds.Tables["LTLShipmentTable"] != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) shipments.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipments;
        }
        public static FreightDataset SearchLTLShipments(LTLSearch2 criteria) {
            //
            FreightDataset shipments = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.SearchLTLShipmentsForAdmin(criteria);
                if(ds != null && ds.Tables["LTLShipmentTable"] != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) shipments.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipments;
        }
        public static string CreateLTLShipment(LTLShipment2 shipment) {
            //Create a new LTL shipment
            string number = "";
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                number = client.CreateLTLShipmentForAdmin(shipment);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return number;
        }
        public static LTLShipment2 ReadLTLShipment(string shipmentNumber) {
            LTLShipment2 ltlShipment = null;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                ltlShipment = client.ReadLTLShipmentForDispatch(shipmentNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return ltlShipment;
        }
        public static bool UpdateLTLShipment(LTLShipment2 shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                updated = client.UpdateLTLShipmentForAdmin(shipment);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool CancelLTLShipment(string shipmentNumber, string userID) {
            //Cancel an existing LTL shipment
            bool cancelled = false;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                cancelled = client.CancelLTLShipmentForAdmin(shipmentNumber, userID);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cancelled;
        }

        public static ServiceLocation ReadPickupLocation(string zipCode) {
            //
            ServiceLocation location = null;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                location = client.ReadPickupLocationForAdmin(zipCode);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return location;
        }
        public static ServiceLocations ReadPickupLocations(string city, string state) {
            //
            ServiceLocations locations = null;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                locations = client.ReadPickupLocationsForAdmin(city, state);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return locations;
        }
        public static ServiceLocation ReadServiceLocation(string zipCode) {
            //
            ServiceLocation location = null;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                location = client.ReadServiceLocationForAdmin(zipCode);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return location;
        }
        public static ServiceLocations ReadServiceLocations(string city, string state) {
            //
            ServiceLocations locations = null;
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                locations = client.ReadServiceLocationsForAdmin(city, state);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return locations;
        }

        public static FreightDataset ReadPalletLabels(string shipmentNumber) {
            FreightDataset labels = new FreightDataset();
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                DataSet ds = client.ReadPalletLabels(shipmentNumber);
                if (ds != null && ds.Tables["LTLPalletLabelTable"].Rows.Count > 0) labels.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return labels;
        }


        public static LTLQuote2 CreateQuoteForSalesRep(LTLQuote2 quote) {
            //Create a new LTL Quote
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                quote = client.CreateQuoteForSalesRep(quote);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quote;
        }
        public static LoadTenderQuoteDataset ViewLoadTenderQuotes(string owner) {
            //
            LoadTenderQuoteDataset quotes = new LoadTenderQuoteDataset();
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                DataSet ds = client.ViewLoadTenderQuotes(owner);
                if(ds != null) quotes.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quotes;
        }
        public static int CreateLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Create a new LTLLoadTenderQuote
            int id = 0;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                id = client.CreateLoadTenderQuote(quote);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return id;
        }
        public static LTLLoadTenderQuote ReadLoadTenderQuote(int id) {
            //
            LTLLoadTenderQuote quote = null;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                quote = client.ReadLoadTenderQuote(id);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quote;
        }
        public static bool UpdateLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Update an existing LTLLoadTenderQuote
            bool updated = false;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                updated = client.UpdateLoadTenderQuote(quote);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool ApproveLoadTenderQuote(int quoteID, DateTime approved, string approvedBy) {
            //Approve an existing LTLLoadTenderQuote
            bool result = false;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                result = client.ApproveLoadTenderQuote(quoteID, approved, approvedBy);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }
        public static bool TenderLoadTenderQuote(int quoteID, LoadTender loadTender) {
            //Tender an existing LTLLoadTenderQuote
            bool result = false;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                result = client.TenderLoadTenderQuote(quoteID, loadTender);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }
        public static bool BookLoadTenderQuote(int quoteID, LTLShipment2 shipment) {
            //Book an existing LTLLoadTenderQuote
            bool result = false;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                result = client.BookLoadTenderQuote(quoteID, shipment);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }
        public static bool CancelLoadTenderQuote(int quoteID, DateTime cancelled, string cancelledBy) {
            //Cancel an existing LTLLoadTenderQuote
            bool result = false;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                result = client.CancelLoadTenderQuote(quoteID, cancelled, cancelledBy);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }
        public static bool ChangeOwnerLoadTenderQuote(int quoteID, string owner) {
            //Change the owner of an existing LTLLoadTenderQuote
            bool result = false;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                result = client.ChangeOwnerLoadTenderQuote(quoteID, owner);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }

        public static LoadTender GetLoadTender(int number) {
            //Get an existing file attachment from database
            LoadTender loadTender = null;
            LTLLoadTenderService2Client client = new LTLLoadTenderService2Client();
            try {
                loadTender = client.GetLoadTender(number);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return loadTender;
        }

    }
}