using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Shipping Interfaces
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IDeliveryPointsService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Terminals.TerminalsFault),Action = "http://Argix.Terminals.TerminalsFault")]
        DataSet GetDeliveryPoints(DateTime startDate,DateTime lastUpated);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Terminals.TerminalsFault),Action = "http://Argix.Terminals.TerminalsFault")]
        DateTime GetExportDate();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Terminals.TerminalsFault),Action = "http://Argix.Terminals.TerminalsFault")]
        bool UpdateExportDate(DateTime lastUpdated);

        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetRoadshowCustomers();
    }
}
