using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.AgentLineHaul {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.AgentLineHaul")]
    public interface IShipScheduleService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo(int terminalID);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames);
        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(int terminalID,TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetShipSchedules(int terminalID,DateTime scheduleDate);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetShipSchedule(int terminalID,long sortCenterID,DateTime scheduleDate);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetShipScheduleTemplates(int terminalID,long sortCenterID,DateTime scheduleDate);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        string CreateShipSchedule(int terminalID,long sortCenterID,DateTime scheduleDate,DateTime lastUpdated,string userID);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        string CreateShipScheduleTrip(int terminalID,string scheduleID,string templateID,DateTime lastUpdated,string userID);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        string FindShipScheduleTrip(int terminalID,DateTime scheduleDate,long carrierServiceID,string loadNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool UpdateShipSchedule(int terminalID,ShipScheduleTrip trip,ShipScheduleStop stop1,ShipScheduleStop stop2);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool UpdateShipScheduleTrip(int terminalID,ShipScheduleTrip trip);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool UpdateShipScheduleTripStop(int terminalID,ShipScheduleStop stop);

        [OperationContract]
        DataSet GetSortCenters(int terminalID);
        [OperationContract]
        DataSet GetCarriers(int terminalID);
    }


    [DataContract]
    public class ShipSchedule {
        //Members
        private string _scheduleid = "";
        private long _sortcenterid = 0;
        private string _sortcenter = "";
        private DateTime _scheduledate = DateTime.Now;
        private string _userid = "";
        private DateTime _lastupdated = DateTime.MinValue;

        //Interface
        public ShipSchedule() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ScheduleID { get { return this._scheduleid; } set { this._scheduleid = value; } }
        [DataMember]
        public long SortCenterID { get { return this._sortcenterid; } set { this._sortcenterid = value; } }
        [DataMember]
        public string SortCenter { get { return this._sortcenter; } set { this._sortcenter = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this._scheduledate; } set { this._scheduledate = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        #endregion
    }

    [DataContract]
    public class ShipScheduleTrip {
        //Members
        private string _tripid = "",_scheduleid = "",_templateid = "";
        private int _bolnumber = 0;
        private long _carrierserviceid = 0;
        private string _loadnumber = "";
        private int _trailerid = 0;
        private string _trailernumber = "",_tractornumber = "",_driverName = "";
        private DateTime _scheduledclose = DateTime.MinValue,_scheduleddeparture = DateTime.MinValue;
        private byte _ismandatory = 1;
        private DateTime _freightassigned = DateTime.MinValue,_trailercomplete = DateTime.MinValue;
        private DateTime _paperworkcomplete = DateTime.MinValue,_trailerdispatched = DateTime.MinValue;
        private DateTime _canceled = DateTime.MinValue;
        private string _userid = "",_rowversion = "";
        private DateTime _lastupdated = DateTime.MinValue;

        //Interface
        public ShipScheduleTrip() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string TripID { get { return this._tripid; } set { this._tripid = value; } }
        [DataMember]
        public string ScheduleID { get { return this._scheduleid; } set { this._scheduleid = value; } }
        [DataMember]
        public string TemplateID { get { return this._templateid; } set { this._templateid = value; } }
        [DataMember]
        public int BolNumber { get { return this._bolnumber; } set { this._bolnumber = value; } }
        [DataMember]
        public long CarrierServiceID { get { return this._carrierserviceid; } set { this._carrierserviceid = value; } }
        [DataMember]
        public string LoadNumber { get { return this._loadnumber; } set { this._loadnumber = value; } }
        [DataMember]
        public int TrailerID { get { return this._trailerid; } set { this._trailerid = value; } }
        [DataMember]
        public string TrailerNumber { get { return this._trailernumber; } set { this._trailernumber = value; } }
        [DataMember]
        public string TractorNumber { get { return this._tractornumber; } set { this._tractornumber = value; } }
        [DataMember]
        public string DriverName { get { return this._driverName; } set { this._driverName = value; } }
        [DataMember]
        public DateTime ScheduledClose { get { return this._scheduledclose; } set { this._scheduledclose = value; } }
        [DataMember]
        public DateTime ScheduledDeparture { get { return this._scheduleddeparture; } set { this._scheduleddeparture = value; } }
        [DataMember]
        public byte IsMandatory { get { return this._ismandatory; } set { this._ismandatory = value; } }
        [DataMember]
        public DateTime FreightAssigned { get { return this._freightassigned; } set { this._freightassigned = value; } }
        [DataMember]
        public DateTime TrailerComplete { get { return this._trailercomplete; } set { this._trailercomplete = value; } }
        [DataMember]
        public DateTime PaperworkComplete { get { return this._paperworkcomplete; } set { this._paperworkcomplete = value; } }
        [DataMember]
        public DateTime TrailerDispatched { get { return this._trailerdispatched; } set { this._trailerdispatched = value; } }
        [DataMember]
        public DateTime Canceled { get { return this._canceled; } set { this._canceled = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string RowVersion { get { return this._rowversion; } set { this._rowversion = value; } }
        #endregion
    }

    [DataContract]
    public class ShipScheduleStop {
        //Members
        private string _stopid = "",_tripid="",_stopnumber = "";
        private long _agentterminalid = 0;
        private string _tag = "",_notes = "";
        private DateTime _scheduledarrival = DateTime.MinValue,_scheduledofd1 = DateTime.MinValue;
        private string _userid = "",_rowversion = "";
        private DateTime _lastupdated = DateTime.Now;

        //Interface
        public ShipScheduleStop() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string StopID { get { return this._stopid; } set { this._stopid = value; } }
        [DataMember]
        public string TripID { get { return this._tripid; } set { this._tripid = value; } }
        [DataMember]
        public string StopNumber { get { return this._stopnumber; } set { this._stopnumber = value; } }
        [DataMember]
        public long AgentTerminalID { get { return this._agentterminalid; } set { this._agentterminalid = value; } }
        [DataMember]
        public string Tag { get { return this._tag; } set { this._tag = value; } }
        [DataMember]
        public string Notes { get { return this._notes; } set { this._notes = value; } }
        [DataMember]
        public DateTime ScheduledArrival { get { return this._scheduledarrival; } set { this._scheduledarrival = value; } }
        [DataMember]
        public DateTime ScheduledOFD1 { get { return this._scheduledofd1; } set { this._scheduledofd1 = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this._lastupdated; } set { this._lastupdated = value; } }
        [DataMember]
        public string RowVersion { get { return this._rowversion; } set { this._rowversion = value; } }
        #endregion
    }

    [DataContract]
    public class ShipScheduleFault {
        private string _messsage;
        public ShipScheduleFault(string message) { this._messsage = message; }

        [DataMember]
        public string Message { get { return this._messsage; } set { this._messsage = value; } }
    }
}
