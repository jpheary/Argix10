using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;

namespace Argix.Customers {
    // 
    [ServiceContract(Namespace = "http://Argix.Customers")]
    public interface ICRMService {

        [OperationContract]
        DataSet GetClients(string vendorID);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        DataSet TrackCartonsForStoreByPickupDate(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        DataSet TrackCartonsForStoreByDeliveryDate(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsByCartonNumber(string[] itemNumbers,string clientNumber,string vendorNumber);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsByLabelNumber(string[] itemNumbers,string clientNumber,string vendorNumber);
    }


}
