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
        private static FreightDataset _Zones = null,_TLs = null;
        public static event EventHandler ZonesChanged = null;
        public static event EventHandler TLsChanged = null;

        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static TsortGateway() { 
            //
            _Zones = new FreightDataset();
            _TLs = new FreightDataset();

            TsortServiceClient client = new TsortServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private TsortGateway() { }
        public static FreightDataset Zones { get { return _Zones; } }
        public static FreightDataset TLs { get { return _TLs; } }
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
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            TsortServiceClient client = new TsortServiceClient();
            try {
                config = client.GetUserConfiguration(int.Parse(Program.TerminalCode),application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            TsortServiceClient client = new TsortServiceClient();
            try {
                client.WriteLogEntry(int.Parse(Program.TerminalCode),m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static void RefreshZones(bool assignableOnly) {
            //Update a collection (dataset) of all open TLs for the terminal on the local LAN database
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                _Zones.Clear();
                if (assignableOnly)
                    _Zones.Merge(client.GetUnassignedTLs(int.Parse(Program.TerminalCode)));
                else
                    _Zones.Merge(client.GetTLs(int.Parse(Program.TerminalCode)));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { if (ZonesChanged != null) ZonesChanged(null,EventArgs.Empty); }
        }
        public static void RefreshTLs() {
            //Update a collection (dataset) of all closed TLs for the terminal on the local LAN database
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                _TLs.Clear();
                _TLs.Merge(client.GetUnassignedClosedTLs(int.Parse(Program.TerminalCode),App.Config.ClosedTLsDays));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            finally { if (TLsChanged != null) TLsChanged(null,EventArgs.Empty); }
        }
        public static Zone NewZone(FreightDataset.ZoneTableRow zone) {
            //Constructor
            Zone newZone = new Zone();
            try {
                if (zone != null) {
                    newZone.ZoneCode = zone.Zone;
                    if (!zone.Is_TL_Null()) newZone.TL = zone._TL_;
                    if (!zone.IsClientNumberNull()) newZone.ClientNumber = zone.ClientNumber;
                    if (!zone.IsClientNameNull()) newZone.ClientName = zone.ClientName;
                    if (!zone.IsNewLaneNull()) newZone.NewLane = zone.NewLane;
                    if (!zone.IsLaneNull()) newZone.Lane = zone.Lane;
                    if (!zone.IsNewSmallSortLaneNull()) newZone.NewSmallSortLane = zone.NewSmallSortLane;
                    if (!zone.IsSmallSortLaneNull()) newZone.SmallSortLane = zone.SmallSortLane;
                    if (!zone.IsDescriptionNull()) newZone.Description = zone.Description;
                    if (!zone.IsTypeNull()) newZone.Type = zone.Type;
                    if (!zone.IsTypeIDNull()) newZone.TypeID = zone.TypeID;
                    if (!zone.IsStatusNull()) newZone.Status = zone.Status;
                    if (!zone.Is_RollbackTL_Null()) newZone.RollbackTL = zone._RollbackTL_;
                    if (!zone.IsIsExclusiveNull()) newZone.IsExclusive = zone.IsExclusive;
                    if (!zone.IsCAN_BE_CLOSEDNull()) newZone.CanBeClosed = zone.CAN_BE_CLOSED;
                    if (!zone.IsAssignedToShipScdeNull()) newZone.AssignedToShipScde = zone.AssignedToShipScde;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return newZone;
        }
        public static FreightDataset GetLanes() {
            //
            FreightDataset lanes = new FreightDataset();
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                DataSet ds = client.GetLanes(int.Parse(Program.TerminalCode));
                if (ds != null) lanes.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return lanes;
        }
        public static bool ChangeLanes(Zone zone) {
            //
            bool changed = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                changed = client.ChangeLanes(int.Parse(Program.TerminalCode),zone);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CloseZone(Zone zone) {
            //
            bool closed = false;
            ZoneClosingServiceClient client = new ZoneClosingServiceClient();
            try {
                closed = client.CloseZone(int.Parse(Program.TerminalCode),zone);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TsortFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return closed;
        }
    }
}
