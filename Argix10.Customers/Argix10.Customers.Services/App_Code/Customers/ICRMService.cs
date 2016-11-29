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
    public interface ICRMService {
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        DataSet ViewIssues();

        [OperationContract(Name="GetIssuesForDate")]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        DataSet ViewIssues(DateTime fromDate);

        [OperationContract(Name = "ViewIssuesForAgent")]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        DataSet ViewIssues(string agentNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        DataSet SearchIssues(string searchText);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        DataSet SearchIssuesAdvanced(object[] criteria);

        [OperationContract(Name = "SearchIssuesAdvancedForAgent")]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        DataSet SearchIssuesAdvanced(string agentNumber,object[] criteria);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        Issue GetIssue(long issueID);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        byte[] GetAttachment(int id);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        long CreateIssue(Issue issue);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        bool AddAction(Action action);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Customers.CustomersFault),Action="http://Argix.Customers.CustomersFault")]
        bool AddAttachment(Attachment attachment);

        [OperationContract]
        DataSet GetIssueTypes(string issueCategory);
        [OperationContract]
        bool CreateIssueType(string type, string category, string description);
        [OperationContract]
        bool UpdateIssueType(int id, string description, int isactive);
        [OperationContract]
        DataSet GetActionTypes(long issueID);


        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetTerminals();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetCompanies();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetRegionsDistricts(string clientNumber);
        [OperationContract(Name="GetAgentsByClient")]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetAgents(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetAgents();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetAgentTerminals(string agentNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetStoreDetail(int companyID,int storeNumber);
        [OperationContract(Name="GetSubStoreDetail")]
        [FaultContractAttribute(typeof(Argix.Enterprise.EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetStoreDetail(int companyID,string subStore);

        [OperationContract]
        DataSet GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to);
    }

    [CollectionDataContract]
    public class Issues:BindingList<Issue> { public Issues() { } }

    [DataContract]
    public class Issue {
        //Members
        private long _id=0;
        private int _typeid=0,_companyid=0;
        private string _contact="", _subject = "",_type="",_contactname="",_companyname="";
        private string _regionnumber = null,_districtnumber = null,_agentnumber = null;
        private int _storenumber = 0;
        private long _firstactionid=0,_lastactionid=0;
        private string _firstactiondescription="",_firstactionuserid="",_lastactiondescription="",_lastactionuserid="";
        private DateTime _firstactioncreated,_lastactioncreated;
        private string _zone="",_coordinator="";
        private Actions mActions=null;

        //Interface
        public Issue() {
            //Constructor
            this._id = this._typeid = this._companyid = 0;
            this._contact = this._subject = this._type = "";
            this._contactname = this._companyname="";
            this._regionnumber = this._districtnumber = this._agentnumber = null;
            this._storenumber = 0;
            this._firstactionid = this._lastactionid=0;
            this._firstactiondescription = this._firstactionuserid = this._lastactiondescription = this._lastactionuserid = "";
            this._firstactioncreated = this._lastactioncreated;
            this._zone = this._coordinator="";
        }
        public Issue(CRMDataset.IssueTableRow issue) {
            //Constructor
            try {
                if(issue != null) {
                    if(!issue.IsIDNull()) this._id = issue.ID;
                    if(!issue.IsTypeIDNull()) this._typeid = issue.TypeID;
                    if(!issue.IsTypeNull()) this._type = issue.Type;
                    if(!issue.IsSubjectNull()) this._subject = issue.Subject.Trim();
                    if(!issue.IsContactNull()) this._contact = issue.Contact;
                    if(!issue.IsCompanyIDNull()) this._companyid = issue.CompanyID;
                    if(!issue.IsCompanyNameNull()) this._companyname = issue.CompanyName;
                    if(!issue.IsRegionNumberNull()) this._regionnumber = issue.RegionNumber.Trim();
                    if(!issue.IsDistrictNumberNull()) this._districtnumber = issue.DistrictNumber.Trim();
                    if(!issue.IsAgentNumberNull()) this._agentnumber = issue.AgentNumber.Trim();
                    if(!issue.IsStoreNumberNull()) this._storenumber = issue.StoreNumber;
                    if(!issue.IsFirstActionIDNull()) this._firstactionid = issue.FirstActionID;
                    if(!issue.IsFirstActionDescriptionNull()) this._firstactiondescription = issue.FirstActionDescription;
                    if(!issue.IsFirstActionUserIDNull()) this._firstactionuserid = issue.FirstActionUserID;
                    if(!issue.IsFirstActionCreatedNull()) this._firstactioncreated = issue.FirstActionCreated;
                    if(!issue.IsLastActionIDNull()) this._lastactionid = issue.LastActionID;
                    if(!issue.IsLastActionDescriptionNull()) this._lastactiondescription = issue.LastActionDescription;
                    if(!issue.IsLastActionUserIDNull()) this._lastactionuserid = issue.LastActionUserID;
                    if(!issue.IsLastActionCreatedNull()) this._lastactioncreated = issue.LastActionCreated;
                    if(!issue.IsZoneNull()) this._zone = issue.Zone;
                    if(!issue.IsCoordinatorNull()) this._coordinator = issue.Coordinator;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Issue instance",ex); }
        }
        #region Members
        [DataMember]
        public long ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public int TypeID { get { return this._typeid; } set { this._typeid = value; } }
        [DataMember]
        public string Type { get { return this._type; } set { this._type = value; } }
        [DataMember]
        public string Subject { get { return this._subject; } set { this._subject = value; } }
        [DataMember]
        public string Contact { get { return this._contact; } set { this._contact = value; } }
        [DataMember]
        public int CompanyID { get { return this._companyid; } set { this._companyid = value; } }
        [DataMember]
        public string CompanyName { get { return this._companyname; } set { this._companyname =  value; } }
        [DataMember]
        public string RegionNumber { get { return this._regionnumber; } set { this._regionnumber = value; } }
        [DataMember]
        public string DistrictNumber { get { return this._districtnumber; } set { this._districtnumber = value; } }
        [DataMember]
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        [DataMember]
        public int StoreNumber { get { return this._storenumber; } set { this._storenumber = value; } }
        [DataMember]
        public long FirstActionID { get { return this._firstactionid; } set { this._firstactionid = value; } }
        [DataMember]
        public string FirstActionDescription { get { return this._firstactiondescription; } set { this._firstactiondescription = value; } }
        [DataMember]
        public DateTime FirstActionCreated { get { return this._firstactioncreated; } set { this._firstactioncreated = value; } }
        [DataMember]
        public string FirstActionUserID { get { return this._firstactionuserid; } set { this._firstactionuserid = value; } }
        [DataMember]
        public long LastActionID { get { return this._lastactionid; } set { this._lastactionid = value; } }
        [DataMember]
        public string LastActionDescription { get { return this._lastactiondescription; } set { this._lastactiondescription = value; } }
        [DataMember]
        public DateTime LastActionCreated { get { return this._lastactioncreated; } set { this._lastactioncreated = value; } }
        [DataMember]
        public string LastActionUserID { get { return this._lastactionuserid; } set { this._lastactionuserid = value; } }
        [DataMember]
        public string Zone { get { return this._zone; } set { this._zone = value; } }
        [DataMember]
        public string Coordinator { get { return this._coordinator; } set { this._coordinator = value; } }
        [DataMember]
        public Actions Actions { get { return this.mActions; } set { this.mActions = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Actions:BindingList<Action> { public Actions() { } }

    [DataContract]
    public class Action {
        //Members
        private long _id=0;
        private byte _typeid = (byte)ACTIONTYPE_OPEN;
        private string _typename = "";
        private string _userid=Environment.UserName;
        private DateTime _created=DateTime.Now;
        private string _comment="";
        private int _attachments=0;
        private long _issueid=0;
        private Attachments mAttachments=null;

        public const int ACTIONTYPE_OPEN = 1;
        public const int ACTIONTYPE_DISMISS = 2;
        public const int ACTIONTYPE_CLOSE = 3;

        //Interface
        public Action() {
            //Constructor
            this._id = 0;
            this._typeid = (byte)ACTIONTYPE_OPEN;
            this._typename = "Open";
            this._userid = Environment.UserName;
            this._created = DateTime.Now;
            this._comment = "";
            this._attachments = 0;
            this._issueid = 0;
        }
        public Action(CRMDataset.ActionTableRow action) {
            //Constructor
            try {
                if(action != null) {
                    if(!action.IsIDNull()) this._id = action.ID;
                    if(!action.IsTypeIDNull()) this._typeid = action.TypeID;
                    if(!action.IsTypeNameNull()) this._typename = action.TypeName;
                    if(!action.IsUserIDNull()) this._userid = action.UserID;
                    if(!action.IsCreatedNull()) this._created = action.Created;
                    if(!action.IsCommentNull()) this._comment = action.Comment;
                    if(!action.IsAttachmentsNull()) this._attachments = action.Attachments;
                    if(!action.IsIssueIDNull()) this._issueid = action.IssueID;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Action instance",ex); }
        }
        #region Members
        [DataMember]
        public long ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public byte TypeID { get { return this._typeid; } set { this._typeid = value; } }
        [DataMember]
        public string TypeName { get { return this._typename; } set { this._typename = value; } }
        [DataMember]
        public long IssueID { get { return this._issueid; } set { this._issueid = value; } }
        [DataMember]
        public string UserID { get { return this._userid; } set { this._userid = value; } }
        [DataMember]
        public DateTime Created { get { return this._created; } set { this._created = value; } }
        [DataMember]
        public string Comment { get { return this._comment; } set { this._comment = value; } }
        [DataMember]
        public int AttachmentCount { get { return this._attachments; } set { this._attachments = value; } }
        [DataMember]
        public Attachments Attachments { get { return this.mAttachments; } set { this.mAttachments = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Attachments:BindingList<Attachment> { public Attachments() { } }

    [DataContract]
    public class Attachment {
        //Members
        private int _id=0;
        private string _filename="";
        private byte[] _file=null;
        private long _actionid=0;

        //Interface
        public Attachment() {
            //Constructor
            this._id = 0;
            this._filename = "";
            this._file = null;
            this._actionid = 0;
        }
        public Attachment(CRMDataset.AttachmentTableRow attachment) {
            //Constructor
            try {
                if(attachment != null) {
                    if(!attachment.IsIDNull()) this._id = attachment.ID;
                    if(!attachment.IsFileNameNull()) this._filename = attachment.FileName;
                    if(!attachment.IsFileNull()) this._file = attachment.File;
                    if(!attachment.IsActionIDNull()) this._actionid = attachment.ActionID;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new Action instance",ex); }
        }
        #region Members
        [DataMember]
        public int ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public string Filename { get { return this._filename; } set { this._filename = value; } }
        [DataMember]
        public byte[] File { get { return this._file; } set { this._file = value; } }
        [DataMember]
        public long ActionID { get { return this._actionid; } set { this._actionid = value; } }
        #endregion
    }

    [DataContract]
    public class CustomersFault {
        private string mMessage="";
        public CustomersFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
