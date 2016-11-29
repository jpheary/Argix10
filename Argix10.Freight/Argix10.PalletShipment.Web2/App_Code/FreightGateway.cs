using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Freight {
	//
	public class FreightGateway {
		//Members
        private bool _state=false;
        private string _address="";

        private const string LOG_NAME = "Argix10";
        private const string LOG_SOURCE = "PalletShipment";

		//Interface
        public FreightGateway() { 
            //
            LTLService2Client client = new LTLService2Client();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        public bool ServiceState { get { return _state; } }
        public string ServiceAddress { get { return _address; } }

        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            LTLService2Client client = new LTLService2Client();
            try {
                return client.GetServiceInfo();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the application configuration for the specified user
            UserConfiguration config=null;
            LTLService2Client client = new LTLService2Client();
            try {
                config = client.GetUserConfiguration(application,usernames);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(LogLevel level, string username, Exception ex) {
            //Write an entry into the Argix log
            LTLService2Client client = new LTLService2Client();
            try {
                TraceMessage tm = new TraceMessage();
                tm.Name = LOG_NAME;
                tm.LogLevel = level;
                tm.Date = DateTime.Now;
                tm.Source = LOG_SOURCE;
                tm.Category = tm.Event = "";
                tm.User = username;
                tm.Computer = "";
                tm.Keyword1 = tm.Keyword2 = tm.Keyword3 = "";
                tm.Message = ex.ToString();
                client.WriteLogEntry(tm);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public LTLQuote2 CreateQuote(LTLQuote2 quote) {
            //Create a new LTL Quote
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                quote = client.CreateQuote(quote);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quote;
        }
        
        public int CreateLTLClient(LTLClient2 ltlClient) {
            //Create a new LTL client
            int clientID = 0;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                clientID = client.CreateLTLClient(ltlClient);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clientID;
        }
        public LTLClient2 ReadLTLClient(int clientID) {
            LTLClient2 ltlClient = new LTLClient2();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                ltlClient = client.ReadLTLClient(clientID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return ltlClient;
        }
        public LTLClient2 ReadLTLClientByNumber(string clientNumber) {
            LTLClient2 ltlClient = new LTLClient2();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                ltlClient = client.ReadLTLClientByNumber(clientNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return ltlClient;
        }
        public bool UpdateLTLClient(LTLClient2 ltlClient) {
            //Update an existing LTL client
            bool updated=false;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                updated = client.UpdateLTLClient(ltlClient);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public LTLClientDataset GetLTLClientList(string salesRepClientNumber) {
            LTLClientDataset clients = new LTLClientDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.GetLTLClientList(salesRepClientNumber);
                if (ds != null && ds.Tables["LTLClientTable"].Rows.Count > 0) clients.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }

        public LTLShipperDataset ViewLTLShippers(string clientNumber) {
            LTLShipperDataset shippers = new LTLShipperDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ViewLTLShippersForClient(clientNumber);
                if (ds != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) shippers.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shippers;
        }
        public string CreateLTLShipper(LTLShipper2 ltlShipper) {
            //Create a new LTL shipper
            string number = "";
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                 number = client.CreateLTLShipper(ltlShipper);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return number;
        }
        public LTLShipperDataset ReadLTLShippersList(string clientNumber) {
            LTLShipperDataset shippers = new LTLShipperDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ReadLTLShippersList(clientNumber);
                if(ds != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) {
                    shippers.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shippers;
        }
        public LTLShipper2 ReadLTLShipper(string shipperNumber) {
            LTLShipper2 shipper = new LTLShipper2();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                shipper = client.ReadLTLShipper(shipperNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipper;
        }
        public bool UpdateLTLShipper(LTLShipper2 ltlShipper) {
            //Update an existing LTL shipper
            bool updated = false;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                updated = client.UpdateLTLShipper(ltlShipper);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public LTLConsigneeDataset ViewLTLConsignees(string clientNumber) {
            LTLConsigneeDataset consignees = new LTLConsigneeDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ViewLTLConsigneesForClient(clientNumber);
                if (ds != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) consignees.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignees;
        }
        public int CreateLTLConsignee(LTLConsignee2 ltlConsignee) {
            //Create a new LTL consignee
            int number = 0;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                number = client.CreateLTLConsignee(ltlConsignee);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return number;
        }
        public LTLConsigneeDataset ReadLTLConsigneesList(string clientNumber) {
            LTLConsigneeDataset consignees = new LTLConsigneeDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ReadLTLConsigneesList(clientNumber);
                if (ds != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) {
                    consignees.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignees;
        }
        public LTLConsignee2 ReadLTLConsignee(int consigneeNumber,string clientNumber) {
            LTLConsignee2 consignee = new LTLConsignee2();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                consignee = client.ReadLTLConsignee(consigneeNumber,clientNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignee;
        }
        public bool UpdateLTLConsignee(LTLConsignee2 ltlConsignee) {
            //Update an existing LTL consignee
            bool updated = false;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                updated = client.UpdateLTLConsignee(ltlConsignee);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public LTLShipmentDataset ViewLTLShipments(string clientNumber) {
            //View all LTL shipments for the specified client
            LTLShipmentDataset shipments = new LTLShipmentDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ViewLTLShipments(clientNumber);
                if (ds != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) shipments.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipments;
        }
        public LTLShipmentDataset SearchLTLShipments(DateTime shipDateStart, DateTime shipDateEnd, string clientNumber, string shipperNumber, int consigneeNumber) {
            //
            LTLShipmentDataset shipments = new LTLShipmentDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                if(clientNumber != null && clientNumber.Trim().Length > 0) {
                    LTLSearch2 criteria = new LTLSearch2();
                    criteria.ShipDateStart = shipDateStart;
                    criteria.ShipDateEnd = shipDateEnd;
                    criteria.ClientNumber = clientNumber;
                    criteria.ShipperNumber = shipperNumber;
                    criteria.ConsigneeNumber = consigneeNumber;
                    DataSet ds = client.SearchLTLShipments(criteria);
                    if(ds != null && ds.Tables["LTLShipmentTable"] != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) shipments.Merge(ds);
                }
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipments;
        }
        public string CreateLTLShipment(LTLShipment2 shipment) {
            //Create a new LTL shipment
            string number="";
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                number = client.CreateLTLShipment(shipment);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return number;
        }
        public LTLShipment2 ReadLTLShipment(string shipmentNumber) {
            //Read an existing LTL shipment
            LTLShipment2 shipment = null;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                shipment = client.ReadLTLShipment(shipmentNumber);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipment;
        }
        public bool UpdateLTLShipment(LTLShipment2 shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                updated = client.UpdateLTLShipment(shipment);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public bool CancelLTLShipment(string shipmentNumber,string userID) {
            //Cancel an existing LTL shipment
            bool cancelled = false;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                cancelled = client.CancelLTLShipment(shipmentNumber,userID);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cancelled;
        }

        public ServiceLocation ReadServiceLocation(string zipCode) {
            //
            ServiceLocation location = null;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                location = client.ReadServiceLocation(zipCode);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return location;
        }
        public ServiceLocation ReadPickupLocation(string zipCode) {
            //
            ServiceLocation location = null;
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                location = client.ReadPickupLocation(zipCode);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return location;
        }

        public LTLPalletLabelDataset ReadPalletLabels(string shipmentNumber) {
            LTLPalletLabelDataset labels = new LTLPalletLabelDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ReadPalletLabelData(shipmentNumber);
                if (ds != null && ds.Tables["LTLPalletLabelTable"].Rows.Count > 0) labels.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return labels;
        }
        public LTLPalletBOLDataset ReadPalletBOLData(string shipmentNumber) {
            LTLPalletBOLDataset bol = new LTLPalletBOLDataset();
            LTLClientService2Client client = new LTLClientService2Client();
            try {
                DataSet ds = client.ReadPalletBOLData(shipmentNumber);
                if(ds != null && ds.Tables["LTLPalletBOLTable"].Rows.Count > 0) bol.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return bol;
        }
    }
}