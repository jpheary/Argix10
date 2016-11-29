using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Finance {
    //Finance Interfaces
    [ServiceContract(Namespace="http://Argix.Finance")]
    public interface IInvoicingService {
        //General Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Finance.InvoicingFault),Action="http://Argix.Finance.InvoicingFault")]
        DataSet GetClients();
        
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Finance.InvoicingFault),Action="http://Argix.Finance.InvoicingFault")]
        DataSet GetClientInvoices(string clientNumber,string clientDivision,string startDate);
    }


    [DataContract]
    public class InvoicingFault {
        private string mMessage="";
        public InvoicingFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
