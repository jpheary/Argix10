using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.AgentLineHaul {
    //AgentLineHaul Interfaces
    [ServiceContract(Namespace="http://Argix.AgentLineHaul")]
    public interface IAgentLineHaulService {
        //General Interface        
        [OperationContract]
        [FaultContractAttribute(typeof(AgentLineHaulFault), Action = "http://Argix.AgentLineHaul.AgentLineHaulFault")]
        DataSet InboundManifestsView(string clientNumber, string clientDivision, DateTime startPickupDate, DateTime endPickupDate);

        [OperationContract]
        [FaultContractAttribute(typeof(AgentLineHaulFault), Action = "http://Argix.AgentLineHaul.AgentLineHaulFault")]
        DataSet InboundManifestRead(string manifestID);
    }

    [DataContract]
    public class AgentLineHaulFault {
        private string _messsage;
        public AgentLineHaulFault(string message) { this._messsage = message; }

        [DataMember]
        public string Message { get { return this._messsage; } set { this._messsage = value; } }
    }
}
