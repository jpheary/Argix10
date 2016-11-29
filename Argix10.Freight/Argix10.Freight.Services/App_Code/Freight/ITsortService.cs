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
    public interface ITsortService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo(int terminalID);

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames);

        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(int terminalID,TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(TsortFault),Action = "http://Argix.TsortFault")]
        DataSet GetTerminals();
    }

    [DataContract]
    public class TsortFault {
        private string mMessage = "";
        public TsortFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}