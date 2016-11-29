using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;

namespace Argix {
    // 
    [ServiceContract(Namespace = "http://Argix")]
    public interface ILTLTrackingService {
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackPalletShipment(string shipmentNumber);
    }
}
