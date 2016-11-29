using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace Argix.AgentLineHaul {
	//
	public class AgentLineHaulGateway {
		//Members
        private static string _ScheduleID = "";
        private static DateTime _ScheduleDate = DateTime.Today;
        private static ShipScheduleDataset _Trips = null;
        private static long _AgentTerminalID = 0;				//Trip filter condition
        public static event EventHandler Changed = null;

        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static AgentLineHaulGateway() { 
            //
            _Trips = new ShipScheduleDataset();

            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private AgentLineHaulGateway() { }
        public static string ScheduleID { get { return _ScheduleID; } }
        public static DateTime ScheduleDate {
            get { return _ScheduleDate; }
            set {
                if (DateTime.Compare(value,_ScheduleDate) != 0) {
                    _ScheduleDate = value;
                    RefreshTrips();
                }
            }
        }
        public static ShipScheduleDataset Trips { get { return _Trips; } }
        public static long AgentTerminalID {
            get { return _AgentTerminalID; }
            set {
                if (value != _AgentTerminalID) {
                    _AgentTerminalID = value;
                    RefreshTrips();
                }
            }
        }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            ServiceInfo si = null;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                si = client.GetServiceInfo(int.Parse(Program.TerminalCode));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return si;
        }
        public static void RefreshTrips() {
            //Update a collection (dataset) of all ship schedule trips for the terminal on the local LAN database
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                //Clear and update cached trips/stops for current schedule date
                _ScheduleID = "";
                _Trips.Clear();
                DataSet ds = client.GetShipScheduleView(int.Parse(Program.TerminalCode),_ScheduleDate);
                ShipScheduleDataset _schedule = new ShipScheduleDataset();
                if (ds != null) _schedule.Merge(ds);
                if (_schedule.ShipScheduleViewTable.Rows.Count > 0) {
                    _ScheduleID = _schedule.ShipScheduleViewTable[0].ScheduleID;
                    string filter1 = _AgentTerminalID > 0 ? "AgentTerminalID=" + _AgentTerminalID + " OR S2AgentTerminalID=" + _AgentTerminalID : "";
                    string filter2 = _AgentTerminalID > 0 ? "AgentTerminalID=" + _AgentTerminalID : "";
                    if (filter1.Length > 0) {
                        _Trips.Merge(_schedule.ShipScheduleViewTable.Select(filter1));
                        _Trips.Merge(_schedule.ShipScheduleTLTable.Select(filter2));
                    }
                    else
                        _Trips.Merge(_schedule);
                }
                client.Close();
            }
            catch (ConstraintException ex) { throw new ApplicationException(ex.Message,ex); }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            finally { if (Changed != null) Changed(null,EventArgs.Empty); }
        }
        public static ShipScheduleDataset GetShipSchedule(DateTime scheduleDate,long agentTerminalID) {
            //Get a ship schedule (collection of ttrips and associated tls) for the specified terminal, date, and agent
            ShipScheduleDataset schedule = new ShipScheduleDataset();
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                DataSet ds = client.GetShipScheduleView(int.Parse(Program.TerminalCode),scheduleDate);
                ShipScheduleDataset _schedule = new ShipScheduleDataset();
                if (ds != null) _schedule.Merge(ds);
                if (_schedule.ShipScheduleViewTable.Rows.Count > 0) {
                    string filter1 = agentTerminalID > 0 ? "AgentTerminalID=" + agentTerminalID + " OR S2AgentTerminalID=" + agentTerminalID : "";
                    string filter2 = agentTerminalID > 0 ? "AgentTerminalID=" + agentTerminalID : "";
                    if (filter1.Length > 0) {
                        schedule.Merge(_schedule.ShipScheduleViewTable.Select(filter1));
                        schedule.Merge(_schedule.ShipScheduleTLTable.Select(filter2));
                    }
                    else
                        schedule.Merge(_schedule);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static ShipScheduleDataset GetAvailableTrips(DateTime scheduleDate,long agentTerminalID) {
            //Get trips for the specified main zone and date that are available for assignment
            ShipScheduleDataset trips = new ShipScheduleDataset();
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                DataSet ds = client.GetShipScheduleView(int.Parse(Program.TerminalCode),scheduleDate);
                ShipScheduleDataset _schedule = new ShipScheduleDataset();
                if (ds != null) _schedule.Merge(ds);
                if (_schedule.ShipScheduleViewTable.Rows.Count > 0) {
                    ShipScheduleDataset __schedule = new ShipScheduleDataset();
                    __schedule.Merge(_schedule.ShipScheduleViewTable.Select("AgentTerminalID='" + agentTerminalID + "' OR S2AgentTerminalID=" + agentTerminalID));
                    trips.Merge(__schedule.ShipScheduleViewTable.Select("IsNull(FreightAssigned, #08/02/61#) = #08/02/61#"));
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return trips;
        }
        public static ShipScheduleItem GetTrip(string tripID) {
            //Return an existing trip from this ship schedule
            ShipScheduleItem trip = null;
            try {
                //Merge from collection (dataset)
                ShipScheduleDataset.ShipScheduleViewTableRow _trip = (ShipScheduleDataset.ShipScheduleViewTableRow)_Trips.ShipScheduleViewTable.Select("TripID='" + tripID + "'")[0];
                ShipScheduleDataset.ShipScheduleTLTableRow[] _tls = (ShipScheduleDataset.ShipScheduleTLTableRow[])_Trips.ShipScheduleTLTable.Select("TripID='" + tripID + "'");
                trip = new ShipScheduleItem(_trip,_tls);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return trip;
        }

        public static DateTime FindEarlierTripOnPriorSchedule(DateTime scheduleDate,string tripID,string freightID) {
            //Return an earlier trip from a schedule prior to the one specified
            DateTime tripDate = DateTime.MinValue;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                tripDate = client.FindEarlierTripOnPriorSchedule(int.Parse(Program.TerminalCode),scheduleDate,tripID,freightID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tripDate;
        }
        public static DateTime FindEarlierTripOnCurrentSchedule(DateTime scheduleDate,string tripID,string freightID) {
            //Return an earlier trip from the current schedule than the one specified if one exists
            DateTime tripDate = DateTime.MinValue;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                tripDate = client.FindEarlierTripOnCurrentSchedule(int.Parse(Program.TerminalCode),scheduleDate,tripID,freightID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tripDate;
        }
        public static DateTime FindShipSchedule(string tlNumber) {
            //Find a ship schedule that contains the specified TL
            DateTime scheduleDate = DateTime.MinValue;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                scheduleDate = client.FindShipSchedule(int.Parse(Program.TerminalCode),tlNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return scheduleDate;
        }

        public static bool AssignTL(string tripID,string tl) {
            //
            bool assigned = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                assigned = client.AssignTL(int.Parse(Program.TerminalCode),tripID,tl);
                client.Close();
                RefreshTrips();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return assigned;
        }
        public static bool UnassignTL(string tlNumber) {
            //Unassign an open TL from this trip
            bool unassigned = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                unassigned = client.UnassignTL(int.Parse(Program.TerminalCode),tlNumber);
                client.Close();
                RefreshTrips();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return unassigned;
        }
        public static bool MoveTL(string tripID,string tlNumber) {
            //Move a closed TL to this trip
            bool moved = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                moved = client.MoveTL(int.Parse(Program.TerminalCode),tripID,tlNumber);
                client.Close();
                RefreshTrips();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return moved;
        }
        public static bool Open(string tripID) {
            //Open a trip to further TL assignments
            bool opened = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                opened = client.OpenTrip(int.Parse(Program.TerminalCode),tripID);
                client.Close();
                RefreshTrips();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return opened;
        }
        public static bool Close(string tripID) {
            //Close a trip from further TL assignments
            bool closed = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                closed = client.CloseTrip(int.Parse(Program.TerminalCode),tripID);
                client.Close();
                RefreshTrips();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return closed;
        }
    }
}
