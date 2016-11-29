using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace Argix.Freight {
	//
	public class TsortGateway {
		//Members
        private static TsortDataset _InboundFreight = null;
        private static TsortDataset _Assignments = null;
        private static TsortDataset _AssignmentHistory = null;
        private static int _SortedRange = 0;
        public static event EventHandler FreightChanged = null;
        public static event EventHandler AssignmentsChanged = null;
        public static event EventHandler AssignmentHistoryChanged = null;

        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static TsortGateway() { 
            //
            _InboundFreight = new TsortDataset();
            _Assignments = new TsortDataset();
            _AssignmentHistory = new TsortDataset();

            TsortServiceClient client = new TsortServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
            _AssignmentHistory = new TsortDataset();
        }
        private TsortGateway() { }
        public static TsortDataset InboundFreight { get { return _InboundFreight; } }
        public static TsortDataset StationAssignments { get { return _Assignments; } }
        public static TsortDataset StationAssignmentHistory { get { return _AssignmentHistory; } }
        public static int SortedRange {  get { return _SortedRange; } set { if (value != _SortedRange) { _SortedRange = value; RefreshFreight(); } }  }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            TsortServiceClient client = new TsortServiceClient();
            try {
                terminal = client.GetServiceInfo(int.Parse(Program.TerminalCode));
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            TsortServiceClient client = new TsortServiceClient();
            try {
                config = client.GetUserConfiguration(int.Parse(Program.TerminalCode),application,usernames);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            TsortServiceClient client = new TsortServiceClient();
            try {
                client.WriteLogEntry(int.Parse(Program.TerminalCode),m);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static TsortDataset GetTerminals() {
            //
            TsortDataset terminals = new TsortDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetTerminals();
                if (ds != null) terminals.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public static void RefreshFreight() {
            //Refresh the list of inbound shipments for the specified terminal
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                _InboundFreight.Clear();
                _InboundFreight.Merge(client.GetInboundFreight(int.Parse(Program.TerminalCode),DateTime.Today.AddDays(-(SortedRange-1))));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { if (FreightChanged != null) FreightChanged(null,EventArgs.Empty); }
        }
        public static InboundShipment GetInboundShipment(string freightID) {
            //Return the inbound shipment for the specified terminal and freightID
            InboundShipment shipment = null;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                shipment = client.GetInboundShipment(int.Parse(Program.TerminalCode),freightID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return shipment;
        }
        public static bool HasAssignments(string freightID) {
            //Determine if the specified fright has assignments
            TsortDataset assignments = new TsortDataset();
            assignments.Merge(StationAssignments.StationFreightAssignmentTable.Select("FreightID = '" + freightID + "'"));
            return (assignments.StationFreightAssignmentTable.Count > 0);
        }
        public static bool IsSortStarted(InboundShipment shipment) {
            //Determine if a shipment has started sort
            bool sortStarted = false;
            try {
                InboundShipment ib = GetInboundShipment(shipment.FreightID);
                sortStarted = (ib.Status.ToLower() != "unsorted");
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException("Failed to determine if sort was started for freight ID#" + shipment.FreightID + ".",ex); }
            return sortStarted;
        }
        public static bool StartSort(InboundShipment shipment) {
            //
            bool started = false;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                started = client.StartSort(shipment);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return started;
        }
        public static bool IsSortStopped(InboundShipment shipment) {
            //Determine if a shipment has completed sort
            bool sortStopped = false;
            try {
                InboundShipment ib = GetInboundShipment(shipment.FreightID);
                sortStopped = (ib.Status.ToLower() == "sorted");
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException("Failed to determine if sort was stopped for freight ID#" + shipment.FreightID + ".",ex); }
            return sortStopped;
        }
        public static bool StopSort(InboundShipment shipment) {
            //
            bool stopped = false;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                stopped = client.StopSort(shipment);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return stopped;
        }

        public static void RefreshStationAssignments() {
            //Refresh the list of freight assignments for the local terminal
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                _Assignments.Clear();
                _Assignments.Merge(client.GetStationAssignments(int.Parse(Program.TerminalCode)));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { if (AssignmentsChanged != null) AssignmentsChanged(null,EventArgs.Empty); }
        }
        public static TsortDataset GetStationAssignments() {
            //Get a list of station-freight assignments for the specified terminal
            TsortDataset assignments = new TsortDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetStationAssignments(int.Parse(Program.TerminalCode));
                if (ds != null) assignments.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return assignments;
        }
        public static TsortDataset GetStationAssignments(InboundShipment shipment,string workStationID) {
            //Get a list of station-freight assignments for the specified terminal and workstation
            TsortDataset assignments = new TsortDataset();
            try {
                TsortDataset _assignments = GetStationAssignments();
                TsortDataset selectedAssignments = new TsortDataset();
                if (workStationID.Trim().Length == 0)
                    selectedAssignments.Merge(_assignments.StationFreightAssignmentTable.Select("FreightID = '" + shipment.FreightID + "'"));
                else
                    selectedAssignments.Merge(_assignments.StationFreightAssignmentTable.Select("FreightID = '" + shipment.FreightID + "' AND WorkStationID = '" + workStationID + "'"));
                for (int i = 0;i < selectedAssignments.StationFreightAssignmentTable.Rows.Count;i++)
                    assignments.StationFreightAssignmentTable.ImportRow(selectedAssignments.StationFreightAssignmentTable[i]);
            }
            catch (ApplicationException aex) { throw aex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return assignments;
        }
        public static bool CreateStationAssignment(Workstation station,InboundShipment shipment,int sortTypeID,string initials) {
            //
            bool created = false;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                created = client.CreateStationAssignment(shipment.TerminalID,station.WorkStationID,shipment.FreightID,sortTypeID);
                client.Close();

                //Add to station assignment history
                _AssignmentHistory.FreightAssignmentHistoryTable.AddFreightAssignmentHistoryTableRow(DateTime.Today,shipment.TDSNumber,shipment.ClientNumber + "-" + shipment.ClientName,station.Number,DateTime.Now,initials);
                if (AssignmentHistoryChanged != null) AssignmentHistoryChanged(null,EventArgs.Empty);

                RefreshStationAssignments();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool DeleteStationAssignment(StationAssignment assignment,string initials) {
            //
            bool deleted = false;
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                deleted = client.DeleteStationAssignment(assignment);
                client.Close();

                //Add to station unassignment history
                _AssignmentHistory.FreightAssignmentHistoryTable.AddFreightAssignmentHistoryTableRow(DateTime.Today,assignment.InboundFreight.TDSNumber,assignment.InboundFreight.ClientName,assignment.SortStation.Number,DateTime.Now,initials);
                if (AssignmentHistoryChanged != null) AssignmentHistoryChanged(null,EventArgs.Empty);

                RefreshStationAssignments();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return deleted;
        }

        public static TsortDataset GetFreightSortTypes(InboundShipment shipment) {
            //
            TsortDataset sortTypes = new TsortDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetFreightSortTypes(shipment.TerminalID,shipment.FreightID);
                if (ds != null) sortTypes.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return sortTypes;
        }
        public static TsortDataset GetAssignableSortStations(InboundShipment shipment,int sortTypeID) {
            //
            TsortDataset sortStations = new TsortDataset();
            FreightAssignServiceClient client = new FreightAssignServiceClient();
            try {
                DataSet ds = client.GetAssignableSortStations(shipment.TerminalID,shipment.FreightID,sortTypeID);
                if (ds != null) sortStations.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return sortStations;
        }
    }
}
