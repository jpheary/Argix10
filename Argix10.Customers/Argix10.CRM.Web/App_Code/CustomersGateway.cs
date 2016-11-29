using System;
using System.Collections;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace Argix.Customers {
	//
    public class CustomersGateway {
        //Members

        //Interface
        public CustomersGateway() { }

        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo info = null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                info = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return info;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the application configuration for the specified user
            UserConfiguration config = null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write an entry into the Argix log
            CRMServiceClient client = new CRMServiceClient();
            try {
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public TerminalDataset GetTerminals(string agentNumber) {
            //Returns a list of terminals
            TerminalDataset terminals = new TerminalDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                TerminalDataset ts = new TerminalDataset();
                DataSet ds = client.GetTerminals();
                client.Close();

                if (ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0) ts.Merge(ds);
                if (agentNumber == null) {
                    terminals.TerminalTable.AddTerminalTableRow(0,"","All","");
                    terminals.Merge(ts);
                }
                else 
                    terminals.Merge(ts.TerminalTable.Select("AgentID='" + agentNumber + "'"));
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public AgentDataSet GetAgents() {
            //Get agents
            AgentDataSet agents = new AgentDataSet();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetAgents();
                client.Close();

                if (ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) agents.Merge(ds.Tables["AgentTable"].Select("","AgentName ASC"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public AgentDataSet GetAgentsByClient(string clientNumber,string agentNumber) {
            //Get agents
            AgentDataSet agents = new AgentDataSet();
            CRMServiceClient client = new CRMServiceClient();
            try {
                AgentDataSet _agents = new AgentDataSet();
                if (clientNumber != null && clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                if (clientNumber != null && clientNumber == "000") clientNumber = null;
                DataSet ds = client.GetAgentsByClient(clientNumber);
                client.Close();

                if (ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) _agents.Merge(ds.Tables["AgentTable"].Select("","AgentName ASC"));
                if (agentNumber == null) {
                    agents.AgentTable.AddAgentTableRow("","","All","","","","","",0,"","","","","","","","","","");
                    agents.Merge(_agents);
                }
                else
                    agents.Merge(_agents.AgentTable.Select("AgentNumber='" + agentNumber + "'"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public CompanyDataset GetCompanies() {
            //Returns a list of terminals
            CompanyDataset companies = new CompanyDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetCompanies();
                client.Close();
                if (ds.Tables["CompanyTable"] != null && ds.Tables["CompanyTable"].Rows.Count > 0) companies.Merge(ds.Tables["CompanyTable"].Select("","CompanyName ASC"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return companies;
        }

        public CRMDataset ViewIssues(string agentNumber) {
            //Get issue search data
            CRMDataset issues = new CRMDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.ViewIssuesForAgent(agentNumber);
                client.Close();
                if (ds != null) {
                    //CRMDataset _issues = new CRMDataset();
                    //_issues.Merge(ds);
                    //issues.Merge(_issues.IssueTable.Select("LastActionDescription <> 'closed'","LastActionCreated DESC"));
                    issues.Merge(ds);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public long CreateIssue(Issue issue) {
            //Create a new issue
            long issueID = 0;
            CRMServiceClient client = new CRMServiceClient();
            try {
                issueID = client.CreateIssue(issue);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issueID;
        }
        public Issue GetIssue(long issueID) {
            //Get issue
            Issue issue = null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                issue = client.GetIssue(issueID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issue;
        }
        public CRMDataset SearchIssues(string agentNumber,string searchText) {
            //Get issue search data
            CRMDataset issues = new CRMDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.SearchIssues(searchText);
                client.Close();
                if (ds != null && ds.Tables["IssueTable"] != null && ds.Tables["IssueTable"].Rows.Count > 0) issues.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public Actions GetIssueActions(long issueID) {
            //Get issue actions
            Issue issue=null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                issue = client.GetIssue(issueID);
                if(issue == null) issue = new Issue();
                if(issue.Actions == null) issue.Actions = new Actions();
                for(int i=0;i<issue.Actions.Count;i++) issue.Actions[i].Comment = issue.Actions[i].Comment.Replace("\n","<br />");
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issue.Actions;
        }
        public Actions GetIssueActions(long issueID, long actionID) {
            //Get issue actions
            Actions actions= new Actions();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                Issue issue = client.GetIssue(issueID);
                client.Close();

                if (issue == null) issue = new Issue();
                if (issue.Actions == null) issue.Actions = new Actions();
                bool go=false;
                for (int i = 0;i < issue.Actions.Count;i++) {
                    if(!go && issue.Actions[i].ID == actionID) go = true;
                    if (go) {
                        issue.Actions[i].Comment = issue.Actions[i].Comment.Replace("\n","<br />");
                        actions.Add(issue.Actions[i]);
                    }
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return actions;
        }
        public bool AddAction(Action action) {
            //Add a new action to an existing issue
            bool added = false;
            CRMServiceClient client = new CRMServiceClient();
            try {
                added = client.AddAction(action);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public Attachments GetAttachments(long issueID,long actionID) {
            //Get issue actions
            Attachments attachments = null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                Issue issue = client.GetIssue(issueID);
                if (issue == null) issue = new Issue();
                if (issue.Actions == null) issue.Actions = new Actions();
                for (int i = 0;i < issue.Actions.Count;i++) {
                    if (issue.Actions[i].ID == actionID) {
                        attachments = issue.Actions[i].Attachments;
                        break;
                    }
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return attachments;
        }
        public byte[] GetAttachment(int attachmentID) {
            //Get an attachment
            byte[] attachment = null;
            CRMServiceClient client = new CRMServiceClient();
            try {
                attachment = client.GetAttachment(attachmentID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return attachment;
        }
        public bool AddAttachment(Attachment attachment) {
            //Add a new attachment to an existing action
            bool added = false;
            CRMServiceClient client = new CRMServiceClient();
            try {
                added = client.AddAttachment(attachment);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }

        public IssueTypeDataset GetIssueCategories(string agentNumber) {
            //Issue type category
            IssueTypeDataset categorys = new IssueTypeDataset();
            try {
                IssueTypeDataset _categorys = new IssueTypeDataset();
                IssueTypeDataset issueTypes = GetIssueTypes("");
                Hashtable groups = new Hashtable();
                for (int i = 0;i < issueTypes.IssueTypeTable.Rows.Count;i++) {
                    if (!groups.ContainsKey(issueTypes.IssueTypeTable[i].Category)) {
                        groups.Add(issueTypes.IssueTypeTable[i].Category,issueTypes.IssueTypeTable[i].Category);
                        _categorys.IssueTypeTable.AddIssueTypeTableRow(0,"",issueTypes.IssueTypeTable[i].Category,"");
                    }
                }
                if (agentNumber == null)
                    categorys.Merge(_categorys);
                else
                    categorys.Merge(_categorys.IssueTypeTable.Select("Category='Agent/Local'"));
                categorys.AcceptChanges();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return categorys;
        }
        public IssueTypeDataset GetIssueTypes(string issueCategory) {
            //Issue types for a category, or category="" for all types
            IssueTypeDataset issueTypes = new IssueTypeDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetIssueTypes(issueCategory);
                client.Close();
                if (ds.Tables["IssueTypeTable"] != null && ds.Tables["IssueTypeTable"].Rows.Count > 0) issueTypes.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issueTypes;
        }
        public ActionTypeDataset GetActionTypes(long issueID) {
            //Action types for an issue (state driven)
            ActionTypeDataset actionTypes = new ActionTypeDataset();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetActionTypes(issueID);
                client.Close();
                if (ds.Tables["ActionTypeTable"] != null && ds.Tables["ActionTypeTable"].Rows.Count > 0) actionTypes.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return actionTypes;
        }
        
        public DataSet GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            DataSet stores = new DataSet();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetStoreDetail(companyID,storeNumber);
                client.Close();
                stores.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return stores;
        }
        public DataSet GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            DataSet stores = new DataSet();
            CRMServiceClient client = new CRMServiceClient();
            try {
                DataSet ds = client.GetSubStoreDetail(companyID,subStore);
                client.Close();
                stores.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return stores;
        }
    }
}