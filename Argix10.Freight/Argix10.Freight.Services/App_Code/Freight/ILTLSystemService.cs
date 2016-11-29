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
    public interface ILTLSystemService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        void DispatchShipment(LTLShipment shipment);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        void ArriveShipment(LTLShipment shipment);
    }
}