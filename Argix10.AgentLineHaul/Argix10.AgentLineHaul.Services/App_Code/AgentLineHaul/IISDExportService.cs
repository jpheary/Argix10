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
    public interface IISDExportService {
        //General Interface        
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo(int terminalID);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames);
        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(int terminalID,TraceMessage m);

        [OperationContract]
        DataSet GetPickups(int terminalID, DateTime pickupDate);

        [OperationContract]
        DataSet GetSortedItems(int terminalID,string pickupID);

        [OperationContract]
        string GetExportFilename(int terminalID,string counterKey);

        [OperationContract]
        DataSet GetISDClients(int terminalID,string clientNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(ISDExportFault),Action="http://Argix.AgentLineHaul.ISDExportFault")]
        bool CreateISDClient(int terminalID,ISDClient client);

        [OperationContract]
        [FaultContractAttribute(typeof(ISDExportFault),Action="http://Argix.AgentLineHaul.ISDExportFault")]
        bool UpdateISDClient(int terminalID,ISDClient client);

        [OperationContract]
        [FaultContractAttribute(typeof(ISDExportFault),Action="http://Argix.AgentLineHaul.ISDExportFault")]
        bool DeleteISDClient(int terminalID,ISDClient client);
    }

    [DataContract]
    public class ISDClient {
        //Members
        private string _clid="";
        private string _exportformat="", _exportpath="";
        private string _counterkey="", _client="";
        private string _scanner="", _userID="";

        //Interface
        public ISDClient() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string CLID { get { return this._clid; } set { this._clid = value; } }
        [DataMember]
        public string ExportFormat { get { return this._exportformat; } set { this._exportformat = value; } }
        [DataMember]
        public string ExportPath { get { return this._exportpath; } set { this._exportpath = value; } }
        [DataMember]
        public string CounterKey { get { return this._counterkey; } set { this._counterkey = value; } }
        [DataMember]
        public string Client { get { return this._client; } set { this._client = value; } }
        [DataMember]
        public string Scanner { get { return this._scanner; } set { this._scanner = value; } }
        [DataMember]
        public string UserID { get { return this._userID; } set { this._userID = value; } }
        #endregion
    }

    [DataContract]
    public class ISDExportFault {
        private string _messsage;
        public ISDExportFault(string message) { this._messsage = message; }

        [DataMember]
        public string Message { get { return this._messsage; } set { this._messsage = value; } }
    }
}
