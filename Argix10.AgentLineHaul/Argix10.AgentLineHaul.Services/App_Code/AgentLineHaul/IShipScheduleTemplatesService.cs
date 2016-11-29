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
    public interface IShipScheduleTemplatesService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(TraceMessage m);
        
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetShippersAndTerminals();      
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetCarriers();   
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetAgents();
        [OperationContract]
        DataSet GetDaysOfWeek();
        [OperationContract]
        int GetWeekday(string weekdayName);

        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action="http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetTemplates();
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action="http://Argix.AgentLineHaul.ShipScheduleFault")]
        string AddTemplate(ShipScheduleTemplate template);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action="http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool UpdateTemplate(ShipScheduleTemplate template);
        [OperationContract]
        System.IO.Stream GetExportDefinition();
    }

    [DataContract]
    public class ShipScheduleTemplate {
        //Members
        private string mTemplateID="";
        private long mSortCenterID=0;
        private string mSortCenter="";
        private byte mDayOfTheWeek=0;
        private long mCarrierServiceID=0;
        private string mCarrier="";
        private byte mScheduledCloseDateOffset=0;
        private DateTime mScheduledCloseTime;
        private byte mScheduledDepartureDateOffset=0;
        private DateTime mScheduledDepartureTime;
        private byte mIsMandatory=0,  mIsActive=0;
        private DateTime mTemplateLastUpdated;
        private string mTemplateUser="", mTemplateRowVersion="", mStopID="",  mStopNumber="", mMainZone="", mTag="";
        private long mAgentTerminalID=0;
        private string mAgentNumber="";
        private byte mScheduledArrivalDateOffset=0;
        private DateTime mScheduledArrivalTime;
        private byte mScheduledOFD1Offset=0;
        private string mNotes="";
        private DateTime mStop1LastUpdated;
        private string mStop1User="", mStop1RowVersion="", mS2StopID="", mS2StopNumber="", mS2MainZone="", mS2Tag="";
        private long mS2AgentTerminalID=0;
        private string mS2AgentNumber="";
        private byte mS2ScheduledArrivalDateOffset=0;
        private DateTime mS2ScheduledArrivalTime;
        private byte mS2ScheduledOFD1Offset=0;
        private string mS2Notes="";
        private DateTime mStop2LastUpdated;
        private string mStop2User="", mStop2RowVersion="";

        //Interface
        public ShipScheduleTemplate() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string TemplateID { get { return this.mTemplateID; } set { this.mTemplateID = value; } }
        [DataMember]
        public long SortCenterID { get { return this.mSortCenterID; } set { this.mSortCenterID = value; } }
        [DataMember]
        public string SortCenter { get { return this.mSortCenter; } set { this.mSortCenter = value; } }
        [DataMember]
        public byte DayOfTheWeek { get { return this.mDayOfTheWeek; } set { this.mDayOfTheWeek = value; } }
        [DataMember]
        public long CarrierServiceID { get { return this.mCarrierServiceID; } set { this.mCarrierServiceID = value; } }
        [DataMember]
        public string Carrier { get { return this.mCarrier; } set { this.mCarrier = value; } }
        [DataMember]
        public byte ScheduledCloseDateOffset { get { return this.mScheduledCloseDateOffset; } set { this.mScheduledCloseDateOffset = value; } }
        [DataMember]
        public DateTime ScheduledCloseTime { get { return this.mScheduledCloseTime; } set { this.mScheduledCloseTime = value; } }
        [DataMember]
        public byte ScheduledDepartureDateOffset { get { return this.mScheduledDepartureDateOffset; } set { this.mScheduledDepartureDateOffset = value; } }
        [DataMember]
        public DateTime ScheduledDepartureTime { get { return this.mScheduledDepartureTime; } set { this.mScheduledDepartureTime = value; } }
        [DataMember]
        public byte IsMandatory { get { return this.mIsMandatory; } set { this.mIsMandatory = value; } }
        [DataMember]
        public byte IsActive { get { return this.mIsActive; } set { this.mIsActive = value; } }
        [DataMember]
        public DateTime TemplateLastUpdated { get { return this.mTemplateLastUpdated; } set { this.mTemplateLastUpdated = value; } }
        [DataMember]
        public string TemplateUser { get { return this.mTemplateUser; } set { this.mTemplateUser = value; } }
        [DataMember]
        public string TemplateRowVersion { get { return this.mTemplateRowVersion; } set { this.mTemplateRowVersion = value; } }
        [DataMember]
        public string StopID { get { return this.mStopID; } set { this.mStopID = value; } }
        [DataMember]
        public string StopNumber { get { return this.mStopNumber; } set { this.mStopNumber = value; } }
        [DataMember]
        public string MainZone { get { return this.mMainZone; } set { this.mMainZone = value; } }
        [DataMember]
        public string Tag { get { return this.mTag; } set { this.mTag = value; } }
        [DataMember]
        public long AgentTerminalID { get { return this.mAgentTerminalID; } set { this.mAgentTerminalID = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        [DataMember]
        public byte ScheduledArrivalDateOffset { get { return this.mScheduledArrivalDateOffset; } set { this.mScheduledArrivalDateOffset = value; } }
        [DataMember]
        public DateTime ScheduledArrivalTime { get { return this.mScheduledArrivalTime; } set { this.mScheduledArrivalTime = value; } }
        [DataMember]
        public byte ScheduledOFD1Offset { get { return this.mScheduledOFD1Offset; } set { this.mScheduledOFD1Offset = value; } }
        [DataMember]
        public string Notes { get { return this.mNotes; } set { this.mNotes = value; } }
        [DataMember]
        public DateTime Stop1LastUpdated { get { return this.mStop1LastUpdated; } set { this.mStop1LastUpdated = value; } }
        [DataMember]
        public string Stop1User { get { return this.mStop1User; } set { this.mStop1User = value; } }
        [DataMember]
        public string Stop1RowVersion { get { return this.mStop1RowVersion; } set { this.mStop1RowVersion = value; } }
        [DataMember]
        public string S2StopID { get { return this.mS2StopID; } set { this.mS2StopID = value; } }
        [DataMember]
        public string S2StopNumber { get { return this.mS2StopNumber; } set { this.mS2StopNumber = value; } }
        [DataMember]
        public string S2MainZone { get { return this.mS2MainZone; } set { this.mS2MainZone = value; } }
        [DataMember]
        public string S2Tag { get { return this.mS2Tag; } set { this.mS2Tag = value; } }
        [DataMember]
        public long S2AgentTerminalID { get { return this.mS2AgentTerminalID; } set { this.mS2AgentTerminalID = value; } }
        [DataMember]
        public string S2AgentNumber { get { return this.mS2AgentNumber; } set { this.mS2AgentNumber = value; } }
        [DataMember(EmitDefaultValue=false,IsRequired=false)]
        public byte S2ScheduledArrivalDateOffset { get { return this.mS2ScheduledArrivalDateOffset; } set { this.mS2ScheduledArrivalDateOffset = value; } }
        [DataMember(EmitDefaultValue=false,IsRequired=false)]
        public DateTime S2ScheduledArrivalTime { get { return this.mS2ScheduledArrivalTime; } set { this.mS2ScheduledArrivalTime = value; } }
        [DataMember(EmitDefaultValue=false,IsRequired=false)]
        public byte S2ScheduledOFD1Offset { get { return this.mS2ScheduledOFD1Offset; } set { this.mS2ScheduledOFD1Offset = value; } }
        [DataMember]
        public string S2Notes { get { return this.mS2Notes; } set { this.mS2Notes = value; } }
        [DataMember]
        public DateTime Stop2LastUpdated { get { return this.mStop2LastUpdated; } set { this.mStop2LastUpdated = value; } }
        [DataMember]
        public string Stop2User { get { return this.mStop2User; } set { this.mStop2User = value; } }
        [DataMember]
        public string Stop2RowVersion { get { return this.mStop2RowVersion; } set { this.mStop2RowVersion = value; } }
        #endregion
    }
}
