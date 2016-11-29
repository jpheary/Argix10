using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Text;

namespace Argix.AgentLineHaul {
	//
	public class ShipScheduleItem {
		//Members
		private string _scheduleid="";
		private long _sortcenterid=0;
		private string _sortcenter="";
		private DateTime _scheduledate=DateTime.Now;
		private string _tripid="", _templateid="";
		private int _bolnumber=0;
		private long _carrierserviceid=0;
		private string _carrier="", _loadnumber="";
		private int _trailerid=0;
		private string _trailernumber="", _tractornumber="";
		private DateTime _scheduledclose=DateTime.MinValue, _scheduleddeparture=DateTime.MinValue;
		private byte _ismandatory=1;
		private DateTime _freightassigned=DateTime.MinValue, _trailercomplete=DateTime.MinValue;
		private DateTime _paperworkcomplete=DateTime.MinValue, _trailerdispatched=DateTime.MinValue;
		private DateTime _canceled=DateTime.MinValue;
		private string _scdeuserid="",_scderowversion="";
		private DateTime _scdelastupdated=DateTime.MinValue;
		private string _stopid="", _stopnumber="";
		private long _agentterminalid=0;
		private string _agentnumber="", _mainzone="";
		private string _tag="", _notes="";
		private DateTime _scheduledarrival=DateTime.MinValue, _scheduledofd1=DateTime.MinValue;
        private string _s1userid="",_s1rowversion="";
		private DateTime _s1lastupdated=DateTime.Now;
		private string _s2stopid="", _s2stopnumber="";
		private long _s2agentterminalid=0;
		private string _s2agentnumber="", _s2mainzone="", _s2tag="", _s2notes="";
		private DateTime _s2scheduledarrival=DateTime.MinValue, _s2scheduledofd1=DateTime.MinValue;
        private string _s2userid="",_s2rowversion="";
		private DateTime _s2lastupdated=DateTime.Now;
		private string _nextcarrier="";
		private int _carrierid=0;
		private ShipScheduleDataset mAssignedTLs=null;
       		
		//Interface
		public ShipScheduleItem(): this(null) { }
        public ShipScheduleItem(ShipScheduleDataset.ShipScheduleViewTableRow viewRow) : this(viewRow,null) { }
        public ShipScheduleItem(ShipScheduleDataset.ShipScheduleViewTableRow viewRow,ShipScheduleDataset.ShipScheduleTLTableRow[] tls) { 
			//Constructor
			try { 
				this.mAssignedTLs = new ShipScheduleDataset();
				if(viewRow != null) { 
					this._scheduleid = viewRow.ScheduleID;
					this._sortcenterid = viewRow.SortCenterID;
					if(!viewRow.IsSortCenterNull()) this._sortcenter = viewRow.SortCenter;
					this._scheduledate = viewRow.ScheduleDate;
					this._tripid = viewRow.TripID;
					if(!viewRow.IsTemplateIDNull()) this._templateid = viewRow.TemplateID;
					if(!viewRow.IsBolNumberNull()) this._bolnumber = viewRow.BolNumber;
					if(!viewRow.IsCarrierServiceIDNull()) this._carrierserviceid = viewRow.CarrierServiceID;
					if(!viewRow.IsCarrierNull()) this._carrier = viewRow.Carrier;
					if(!viewRow.IsLoadNumberNull()) this._loadnumber = viewRow.LoadNumber;
					if(!viewRow.IsTrailerIDNull()) this._trailerid = viewRow.TrailerID;
					if(!viewRow.IsTrailerNumberNull()) this._trailernumber = viewRow.TrailerNumber;
					if(!viewRow.IsTractorNumberNull()) this._tractornumber = viewRow.TractorNumber;
					if(!viewRow.IsScheduledCloseNull()) this._scheduledclose = viewRow.ScheduledClose;
					if(!viewRow.IsScheduledDepartureNull()) this._scheduleddeparture = viewRow.ScheduledDeparture;
					if(!viewRow.IsIsMandatoryNull()) this._ismandatory = viewRow.IsMandatory;
					if(!viewRow.IsFreightAssignedNull()) this._freightassigned = viewRow.FreightAssigned;
					if(!viewRow.IsTrailerCompleteNull()) this._trailercomplete = viewRow.TrailerComplete;
					if(!viewRow.IsPaperworkCompleteNull()) this._paperworkcomplete = viewRow.PaperworkComplete;
					if(!viewRow.IsTrailerDispatchedNull()) this._trailerdispatched = viewRow.TrailerDispatched;
					if(!viewRow.IsCanceledNull()) this._canceled = viewRow.Canceled;
					if(!viewRow.IsSCDEUserIDNull()) this._scdeuserid = viewRow.SCDEUserID;
					if(!viewRow.IsSCDELastUpdatedNull()) this._scdelastupdated = viewRow.SCDELastUpdated;
					if(!viewRow.IsSCDERowVersionNull()) this._scderowversion = viewRow.SCDERowVersion;
					if(!viewRow.IsStopIDNull()) this._stopid = viewRow.StopID;
					if(!viewRow.IsStopNumberNull()) this._stopnumber = viewRow.StopNumber;
					if(!viewRow.IsAgentTerminalIDNull()) this._agentterminalid = viewRow.AgentTerminalID;
					if(!viewRow.IsAgentNumberNull()) this._agentnumber = viewRow.AgentNumber;
					if(!viewRow.IsMainZoneNull()) this._mainzone = viewRow.MainZone;
					if(!viewRow.IsTagNull()) this._tag = viewRow.Tag;
					if(!viewRow.IsNotesNull()) this._notes = viewRow.Notes;
					if(!viewRow.IsScheduledArrivalNull()) this._scheduledarrival = viewRow.ScheduledArrival;
					if(!viewRow.IsScheduledOFD1Null()) this._scheduledofd1 = viewRow.ScheduledOFD1;
					if(!viewRow.IsS1UserIDNull()) this._s1userid = viewRow.S1UserID;
					if(!viewRow.IsS1LastUpdatedNull()) this._s1lastupdated = viewRow.S1LastUpdated;
					if(!viewRow.IsS1RowVersionNull()) this._s1rowversion = viewRow.S1RowVersion;
					if(!viewRow.IsS2StopIDNull()) this._s2stopid = viewRow.S2StopID;
					if(!viewRow.IsS2StopNumberNull()) this._s2stopnumber = viewRow.S2StopNumber;
					if(!viewRow.IsS2AgentTerminalIDNull()) this._s2agentterminalid = viewRow.S2AgentTerminalID;
					if(!viewRow.IsS2AgentNumberNull()) this._s2agentnumber = viewRow.S2AgentNumber;
					if(!viewRow.IsS2MainZoneNull()) this._s2mainzone = viewRow.S2MainZone;
					if(!viewRow.IsS2TagNull()) this._s2tag = viewRow.S2Tag;
					if(!viewRow.IsS2NotesNull()) this._s2notes = viewRow.S2Notes;
					if(!viewRow.IsS2ScheduledArrivalNull()) this._s2scheduledarrival = viewRow.S2ScheduledArrival;
					if(!viewRow.IsS2ScheduledOFD1Null()) this._s2scheduledofd1 = viewRow.S2ScheduledOFD1;
					if(!viewRow.IsS2UserIDNull()) this._s2userid = viewRow.S2UserID;
					if(!viewRow.IsS2LastUpdatedNull()) this._s2lastupdated = viewRow.S2LastUpdated;
					if(!viewRow.IsS2RowVersionNull()) this._s2rowversion = viewRow.S2RowVersion;
					if(!viewRow.IsNextCarrierNull()) this._nextcarrier = viewRow.NextCarrier;
					if(!viewRow.IsCarrierIDNull()) this._carrierid = viewRow.CarrierID;
					if(tls != null) {
                        ShipScheduleDataset.ShipScheduleViewTableRow _trip = this.mAssignedTLs.ShipScheduleViewTable.NewShipScheduleViewTableRow();
						_trip.ScheduleID = this.ScheduleID;
						_trip.SortCenterID = this.SortCenterID;
						_trip.TripID = this.TripID;
                        this.mAssignedTLs.ShipScheduleViewTable.AddShipScheduleViewTableRow(_trip);
						this.mAssignedTLs.Merge(tls);
					}
				}
			}
			catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
		}
		#region Accessors\Modifiers: [Members]..., AssignedTLs
		public string ScheduleID {  get { return this._scheduleid; } }
		public long SortCenterID { get { return this._sortcenterid; } }
		public string SortCenter { get { return this._sortcenter; } }
		public DateTime ScheduleDate { get { return this._scheduledate; } }
		public string TripID { get { return this._tripid; } }
		public string TemplateID { get { return this._templateid; } }
		public int BolNumber { get { return this._bolnumber; } }
		public long CarrierServiceID { get { return this._carrierserviceid; } }
		public string Carrier { get { return this._carrier; } }
		public string LoadNumber { get { return this._loadnumber; } }
		public int TrailerID { get { return this._trailerid; } }
		public string TrailerNumber { get { return this._trailernumber; } }
		public string TractorNumber { get { return this._tractornumber; } }
		public DateTime ScheduledClose { get { return this._scheduledclose; } }
		public DateTime ScheduledDeparture { get { return this._scheduleddeparture; } }
		public byte IsMandatory { get { return this._ismandatory; } }
		public DateTime FreightAssigned { get { return this._freightassigned; } }
		public DateTime TrailerComplete { get { return this._trailercomplete; } }
		public DateTime PaperworkComplete { get { return this._paperworkcomplete; } }
		public DateTime TrailerDispatched { get { return this._trailerdispatched; } }
		public DateTime Canceled { get { return this._canceled; } }
		public string SCDEUserID { get { return this._scdeuserid; } }
		public DateTime SCDELastUpdated { get { return this._scdelastupdated; } }
		public string SCDERowVersion { get { return this._scderowversion; } }
		public string StopID { get { return this._stopid; } }
		public string StopNumber { get { return this._stopnumber; } }
		public long AgentTerminalID { get { return this._agentterminalid; } }
		public string AgentNumber { get { return this._agentnumber; } }
		public string MainZone { get { return this._mainzone; } }
		public string Tag { get { return this._tag; } }
		public string Notes { get { return this._notes; } }
		public DateTime ScheduledArrival { get { return this._scheduledarrival; } }
		public DateTime ScheduledOFD1 { get { return this._scheduledofd1; } }
		public string S1UserID { get { return this._s1userid; } }
		public DateTime S1LastUpdated { get { return this._s1lastupdated; } }
		public string S1RowVersion { get { return this._s1rowversion; } }
		public string S2StopID { get { return this._s2stopid; } }
		public string S2StopNumber { get { return this._s2stopnumber; } }
		public long S2AgentTerminalID { get { return this._s2agentterminalid; } }
		public string S2AgentNumber { get { return this._s2agentnumber; } }
		public string S2MainZone { get { return this._s2mainzone; } }
		public string S2Tag { get { return this._s2tag; } }
		public string S2Notes { get { return this._s2notes; } }
		public DateTime S2ScheduledArrival { get { return this._s2scheduledarrival; } }
		public DateTime S2ScheduledOFD1 { get { return this._s2scheduledofd1; } }
		public string S2UserID { get { return this._s2userid; } }
		public DateTime S2LastUpdated { get { return this._s2lastupdated; } }
		public string S2RowVersion { get { return this._s2rowversion; } }
		public string NextCarrier { get { return this._nextcarrier; } }
		public int CarrierID { get { return this._carrierid; } }
		public ShipScheduleDataset AssignedTLs { get { return this.mAssignedTLs; } }
		#endregion
		public bool IsOpen { get { return (DateTime.Compare(this._freightassigned, DateTime.MinValue) == 0); } }
		public bool IsComplete { get { return (DateTime.Compare(this._trailercomplete, DateTime.MinValue) > 0); } }
		public bool AllTLsClosed { 
			get { 
				bool anyOpen=false;
				for(int i=0; i<this.mAssignedTLs.ShipScheduleTLTable.Rows.Count; i++) {
                    if (this.mAssignedTLs.ShipScheduleTLTable[i].IsCloseDateNull()) {
						anyOpen = true;
						break;
					}
				}
				return !anyOpen; 
			} 
		}
	}
}
