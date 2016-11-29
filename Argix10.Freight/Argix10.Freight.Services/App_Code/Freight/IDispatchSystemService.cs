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
    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface IDispatchSystemService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        int SchedulePickupRequest(PickupRequest request);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        PickupRequest ReadPickup(int requestID);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool UpdatePickup(PickupRequest pickup);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool CancelPickup(int requestID,DateTime cancelled,string cancelledUserID);
    }
}