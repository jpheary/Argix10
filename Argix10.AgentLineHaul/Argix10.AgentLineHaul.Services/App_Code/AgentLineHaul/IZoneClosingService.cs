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
    public interface IZoneClosingService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DataSet GetShipScheduleView(int terminalID,DateTime scheduleDate);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DateTime FindEarlierTripOnPriorSchedule(int terminalID,DateTime scheduleDate,string tripID,string freightID);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DateTime FindEarlierTripOnCurrentSchedule(int terminalID,DateTime scheduleDate,string tripID,string freightID);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        DateTime FindShipSchedule(int terminalID,string TLNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool OpenTrip(int terminalID,string tripID);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool CloseTrip(int terminalID,string tripID);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool AssignTL(int terminalID,string tripID,string tl);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool UnassignTL(int terminalID,string tl);
        [OperationContract]
        [FaultContractAttribute(typeof(ShipScheduleFault),Action = "http://Argix.AgentLineHaul.ShipScheduleFault")]
        bool MoveTL(int terminalID,string tripID,string tl);

    }
}
