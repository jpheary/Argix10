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
        
		//Interface
        public FreightGateway() { 
            //
            LTLServiceClient client = new LTLServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        public bool ServiceState { get { return _state; } }
        public string ServiceAddress { get { return _address; } }

        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            LTLServiceClient client = new LTLServiceClient();
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
            LTLServiceClient client = new LTLServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write an entry into the Argix log
            LTLServiceClient client = new LTLServiceClient();
            try {
                client.WriteLogEntry(m);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public LTLQuote CreateQuote(LTLQuote quote) {
            //Create a new LTL Quote
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                quote = client.CreateQuote(quote);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quote;
        }
        
        public int CreateLTLClient(LTLClient ltlClient) {
            //Create a new LTL client
            int clientID = 0;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                clientID = client.CreateLTLClient(ltlClient);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clientID;
        }
        public LTLClient ReadLTLClient(int clientID) {
            LTLClient ltlClient = new LTLClient();
            LTLClientServiceClient client = new LTLClientServiceClient();
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
        public bool UpdateLTLClient(LTLClient ltlClient) {
            //Update an existing LTL client
            bool updated=false;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                updated = client.UpdateLTLClient(ltlClient);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public LTLClientDataset GetLTLClientList(int salesRepClientID) {
            LTLClientDataset clients = new LTLClientDataset();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                DataSet ds = client.GetLTLClientList(salesRepClientID);
                if (ds != null && ds.Tables["LTLClientTable"].Rows.Count > 0) clients.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }

        public LTLShipperDataset ViewLTLShippers(int clientID) {
            LTLShipperDataset shippers = new LTLShipperDataset();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                DataSet ds = client.ViewLTLShippersForClient(clientID);
                if (ds != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) shippers.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shippers;
        }
        public int CreateLTLShipper(LTLShipper ltlShipper) {
            //Create a new LTL shipper
            int shipperID = 0;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                shipperID = client.CreateLTLShipper(ltlShipper);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipperID;
        }
        public LTLShipperDataset ReadLTLShippersList(int clientID) {
            LTLShipperDataset shippers = new LTLShipperDataset();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                DataSet ds = client.ReadLTLShippersList(clientID);
                if (ds != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) shippers.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shippers;
        }
        public LTLShipper ReadLTLShipper(int shipperID) {
            LTLShipper shipper = new LTLShipper();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                shipper = client.ReadLTLShipper(shipperID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipper;
        }
        public bool UpdateLTLShipper(LTLShipper ltlShipper) {
            //Update an existing LTL shipper
            bool updated = false;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                updated = client.UpdateLTLShipper(ltlShipper);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public LTLConsigneeDataset ViewLTLConsignees(int clientID) {
            LTLConsigneeDataset consignees = new LTLConsigneeDataset();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                DataSet ds = client.ViewLTLConsigneesForClient(clientID);
                if (ds != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) consignees.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignees;
        }
        public int CreateLTLConsignee(LTLConsignee ltlConsignee) {
            //Create a new LTL consignee
            int consigneeID = 0;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                consigneeID = client.CreateLTLConsignee(ltlConsignee);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consigneeID;
        }
        public LTLConsigneeDataset ReadLTLConsigneesList(int clientID) {
            LTLConsigneeDataset consignees = new LTLConsigneeDataset();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                DataSet ds = client.ReadLTLConsigneesList(clientID);
                if (ds != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) consignees.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignees;
        }
        public LTLConsignee ReadLTLConsignee(int consigneeID) {
            LTLConsignee consignee = new LTLConsignee();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                consignee = client.ReadLTLConsignee(consigneeID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return consignee;
        }
        public bool UpdateLTLConsignee(LTLConsignee ltlConsignee) {
            //Update an existing LTL consignee
            bool updated = false;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                updated = client.UpdateLTLConsignee(ltlConsignee);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public LTLShipmentDataset ViewLTLShipments(int clientID) {
            //View all LTL shipments for the specified client
            LTLShipmentDataset shipments = new LTLShipmentDataset();
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                DataSet ds = client.ViewLTLShipments(clientID);
                if (ds != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) shipments.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipments;
        }
        public int CreateLTLShipment(LTLShipment shipment) {
            //Create a new LTL shipment
            int id = 0;
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                id = client.CreateLTLShipment(shipment);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return id;
        }

        public ServiceLocation ReadServiceLocation(string zipCode) {
            //
            ServiceLocation location = null;
            LTLClientServiceClient client = new LTLClientServiceClient();
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
            LTLClientServiceClient client = new LTLClientServiceClient();
            try {
                location = client.ReadPickupLocation(zipCode);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return location;
        }
    }
}