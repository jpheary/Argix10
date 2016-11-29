using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Argix.Enterprise;

namespace Argix.Freight {
    //Shipping Interfaces
    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface IDispatchClientService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewPickupLog(string clientNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        int RequestPickup(PickupRequest pickup);

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