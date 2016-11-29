using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //Enterprise Interfaces
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface IZoneClosingService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.Freight.TsortFault")]
        DataSet GetTLs(int terminalID);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.Freight.TsortFault")]
        DataSet GetUnassignedTLs(int terminalID);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.Freight.TsortFault")]
        DataSet GetUnassignedClosedTLs(int terminalID,int closedDays);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.Freight.TsortFault")]
        DataSet GetLanes(int terminalID);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.Freight.TsortFault")]
        bool ChangeLanes(int terminalID,Zone zone);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.Freight.TsortFault")]
        bool CloseZone(int terminalID,Zone zone);

    }

    [DataContract]
    public class Zone {
        //Members
        private string _zone = "",_tl = "";
        private string _clientnumber = "",_clientname = "";
        private string _newlane = "",_lane = "",_newsmallsortlane = "",_smallsortlane = "";
        private string _description = "",_type = "",_typeid = "";
        private string _status = "",_rollbacktl = "";
        private int _isexclusive = 0;
        private string _can_be_closed = "T",_assignedtoshipscde = "";

        //Interface
        public Zone(FreightDataset.ZoneTableRow zone) {
            //Constructor
            try {
                if (zone != null) {
                    this._zone = zone.Zone;
                    if (!zone.Is_TL_Null()) this._tl = zone._TL_;
                    if (!zone.IsClientNumberNull()) this._clientnumber = zone.ClientNumber;
                    if (!zone.IsClientNameNull()) this._clientname = zone.ClientName;
                    if (!zone.IsNewLaneNull()) this._newlane = zone.NewLane;
                    if (!zone.IsLaneNull()) this._lane = zone.Lane;
                    if (!zone.IsNewSmallSortLaneNull()) this._newsmallsortlane = zone.NewSmallSortLane;
                    if (!zone.IsSmallSortLaneNull()) this._smallsortlane = zone.SmallSortLane;
                    if (!zone.IsDescriptionNull()) this._description = zone.Description;
                    if (!zone.IsTypeNull()) this._type = zone.Type;
                    if (!zone.IsTypeIDNull()) this._typeid = zone.TypeID;
                    if (!zone.IsStatusNull()) this._status = zone.Status;
                    if (!zone.Is_RollbackTL_Null()) this._rollbacktl = zone._RollbackTL_;
                    if (!zone.IsIsExclusiveNull()) this._isexclusive = zone.IsExclusive;
                    if (!zone.IsCAN_BE_CLOSEDNull()) this._can_be_closed = zone.CAN_BE_CLOSED;
                    if (!zone.IsAssignedToShipScdeNull()) this._assignedtoshipscde = zone.AssignedToShipScde;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ZoneCode { get { return this._zone; } set { this._zone = value; } }
        [DataMember]
        public string TL { get { return this._tl; } set { this._tl = value; } }
        [DataMember]
        public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
        [DataMember]
        public string ClientName { get { return this._clientname; } set { this._clientname = value; } }
        [DataMember]
        public string NewLane { get { return this._newlane; } set { this._newlane = value; } }
        [DataMember]
        public string Lane { get { return this._lane; } set { this._lane = value; } }
        [DataMember]
        public string NewSmallSortLane { get { return this._newsmallsortlane; } set { this._newsmallsortlane = value; } }
        [DataMember]
        public string SmallSortLane { get { return this._smallsortlane; } set { this._smallsortlane = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
        [DataMember]
        public string Type { get { return this._type; } set { this._type = value; } }
        [DataMember]
        public string TypeID { get { return this._typeid; } set { this._typeid = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public string RollbackTL { get { return this._rollbacktl; } set { this._rollbacktl = value; } }
        [DataMember]
        public int IsExclusive { get { return this._isexclusive; } set { this._isexclusive = value; } }
        [DataMember]
        public string CanBeClosed { get { return this._can_be_closed; } set { this._can_be_closed = value; } }
        [DataMember]
        public string AssignedToShipScde { get { return this._assignedtoshipscde; } set { this._assignedtoshipscde = value; } }
        #endregion
    }
}