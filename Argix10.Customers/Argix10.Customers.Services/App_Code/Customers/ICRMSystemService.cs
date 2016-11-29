using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Customers {
    //
    [ServiceContract(Namespace="http://Argix.Customers")]
    public interface ICRMSystemService {
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        long CreateIssue(CRMIssue issue);
    }

    [DataContract]
    public class CRMIssue {
        //Members
        private int _typeid = 0,_companyid = 0;
        private string _contact = "",_subject = "",_companyname = "";
        private string _regionnumber = null,_districtnumber = null,_agentnumber = null;
        private int _storenumber = 0;

        private byte _actionTypeid = (byte)ACTIONTYPE_OPEN;
        private string _userid = Environment.UserName;
        private string _comment = "";

        private const int ACTIONTYPE_OPEN = 1;

        //Interface
        public CRMIssue() { }
        #region Members
        [DataMember]
        public int TypeID { get { return this._typeid; } set { this._typeid = value; } }
        [DataMember]
        public string Subject { get { return this._subject; } set { this._subject = value; } }
        [DataMember]
        public string Contact { get { return this._contact; } set { this._contact = value; } }
        [DataMember]
        public int CompanyID { get { return this._companyid; } set { this._companyid = value; } }
        [DataMember]
        public string CompanyName { get { return this._companyname; } set { this._companyname = value; } }
        [DataMember]
        public string RegionNumber { get { return this._regionnumber; } set { this._regionnumber = value; } }
        [DataMember]
        public string DistrictNumber { get { return this._districtnumber; } set { this._districtnumber = value; } }
        [DataMember]
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        [DataMember]
        public int StoreNumber { get { return this._storenumber; } set { this._storenumber = value; } }
        [DataMember]
        public byte ActionTypeID { get { return this._actionTypeid; } set { this._actionTypeid = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public string Comment { get { return this._comment; } set { this._comment = value; } }
        #endregion
    }

}
