using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Web;

namespace Argix.Freight {
	//
	public class TsortGateway {
		//Members
        private int mSortedDays = 1;

		//Interface
        public TsortGateway() { 
            //Constructor
            try {
                this.mSortedDays = int.Parse(ConfigurationManager.AppSettings["SortedDays"]);
            }
            catch { }
        }
        public CommunicationState ServiceState { get { return new TsortServiceClient().State; } }
        public string ServiceAddress { get { return new TsortServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public ServiceInfo GetServiceInfo(int terminalID) {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            TsortServiceClient client = new TsortServiceClient();
            try {
                terminal = client.GetServiceInfo(terminalID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            TsortServiceClient client = new TsortServiceClient();
            try {
                config = client.GetUserConfiguration(terminalID,application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(int terminalID,TraceMessage m) {
            //Get the operating enterprise terminal
            TsortServiceClient client = new TsortServiceClient();
            try {
                client.WriteLogEntry(terminalID,m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public TerminalDataset GetTerminals() {
            //Get a list of Argix terminals
            TerminalDataset terminals = new TerminalDataset();
            TsortServiceClient client = new TsortServiceClient();
            try {
                DataSet ds = client.GetTerminals();
                if (ds != null) {
                    TerminalDataset ts = new TerminalDataset();
                    ts.Merge(ds);
                    if (HttpContext.Current.User.Identity.Name != "jheary") {
                        string codes = ConfigurationManager.AppSettings["Terminals"];
                        for (int i = ts.TerminalTable.Rows.Count-1;i >= 0;i--) 
                            if(!codes.Contains(ts.TerminalTable[i].Number)) ts.TerminalTable[i].Delete();
                    }
                    terminals.Merge(ts);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }

        public FreightDataset GetInboundFreight(int terminalID,string freightType) {
            FreightDataset shipments = new FreightDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetInboundFreight(terminalID,DateTime.Today.AddDays(-this.mSortedDays));
                if (ds != null) {
                    FreightDataset _shipments = new FreightDataset();
                    _shipments.Merge(ds);
                    shipments.Merge(_shipments.InboundFreightTable.Select("FreightType='" + freightType + "'","TDSNumber ASC"));
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipments;
        }
        public InboundShipment GetInboundShipment(int terminalID,string freightID) {
            //Return the inbound shipment for the specified terminal and freightID
            InboundShipment shipment = null;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                shipment = client.GetInboundShipment(terminalID,freightID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipment;
        }
        public AssignmentDataset GetStationAssignments(int terminalID,string freightID) {
            //Get a list of station-freight assignments for the specified terminal
            AssignmentDataset assignments = new AssignmentDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                AssignmentDataset _assignments = new AssignmentDataset();
                DataSet ds = client.GetStationAssignments(terminalID);
                if (ds != null) _assignments.Merge(ds);
                client.Close();
                
                if (freightID != null && freightID.Length > 0)
                    assignments.Merge(_assignments.StationFreightAssignmentTable.Select("FreightID='" + freightID + "'","StationNumber ASC"));
                else
                    assignments.Merge(_assignments.StationFreightAssignmentTable.Select("","StationNumber ASC"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return assignments;
        }
        public SortTypeDataset GetFreightSortTypes(int terminalID,string freightID) {
            //
            SortTypeDataset sortTypes = new SortTypeDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetFreightSortTypes(terminalID,freightID);
                if (ds != null) sortTypes.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return sortTypes;
        }
        public WorkstationDataset GetAssignableSortStations(int terminalID,string freightID,int sortTypeID) {
            //
            WorkstationDataset sortStations = new WorkstationDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetAssignableSortStations(terminalID,freightID,sortTypeID);
                client.Close();
                if (ds != null) sortStations.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return sortStations;
        }
        public bool CreateStationAssignment(Workstation station,InboundShipment shipment,int sortTypeID) {
            //
            bool created = false;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                created = client.CreateStationAssignment(shipment.TerminalID,station.WorkStationID,shipment.FreightID,sortTypeID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public bool DeleteStationAssignment(StationAssignment assignment) {
            //
            bool deleted = false;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                deleted = client.DeleteStationAssignment(assignment);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return deleted;
        }

        public ZoneDataset GetZones(int terminalID) {
            //Get a list of open TLs
            ZoneDataset zones = new ZoneDataset();
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                DataSet ds = client.GetTLs(terminalID);
                if (ds != null) zones.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return zones;
        }
        public ZoneDataset GetZone(int terminalID, string zoneCode) {
            //Get a single open TL
            ZoneDataset zone = new ZoneDataset();
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                DataSet ds = client.GetTLs(terminalID);
                if (ds != null) {
                    ZoneDataset _zones = new ZoneDataset();
                    _zones.Merge(ds);
                    zone.Merge(_zones.ZoneTable.Select("Zone='" + zoneCode + "'"));
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return zone;
        }
    }
}