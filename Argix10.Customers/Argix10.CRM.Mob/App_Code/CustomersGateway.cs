using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

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

        public TerminalDataSet GetTerminals() {
            //Returns a list of terminals
            TerminalDataSet terminals = new TerminalDataSet();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetTerminals();
                client.Close();
                if (ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0) terminals.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public AgentDataSet GetAgents() {
            //Get agents
            AgentDataSet agents = new AgentDataSet();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetAgents();
                client.Close();

                if (ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) agents.Merge(ds.Tables["AgentTable"].Select("","AgentName ASC"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public CompanyDataSet GetCompanies() {
            //Returns a list of companies
            CompanyDataSet companies = new CompanyDataSet();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetCompanies();
                client.Close();
                if (ds.Tables["CompanyTable"] != null && ds.Tables["CompanyTable"].Rows.Count > 0) companies.Merge(ds.Tables["CompanyTable"].Select("","CompanyName ASC"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return companies;
        }
        public CompanyDataSet GetCompanies2() {
            //Returns a list of companies without the clientnumber hyphen (i.e. companyname - 001)
            CompanyDataSet companies = new CompanyDataSet();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetCompanies();
                client.Close();
                if(ds.Tables["CompanyTable"] != null && ds.Tables["CompanyTable"].Rows.Count > 0) {
                    companies.Merge(ds.Tables["CompanyTable"].Select("", "CompanyName ASC"));
                    for(int i = 0; i < companies.CompanyTable.Count; i++) {
                        companies.CompanyTable[i].CompanyName = companies.CompanyTable[i].CompanyName.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries)[0].Trim();
                    }
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return companies;
        }
        public CRMDataset ViewIssues(string agentNumber) {
            //Get issue search data
            CRMDataset issues = new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.ViewIssuesForAgent(agentNumber);
                client.Close();
                if (ds != null) {
                    CRMDataset _issues = new CRMDataset();
                    _issues.Merge(ds);
                    issues.Merge(_issues.IssueTable.Select("LastActionDescription <> 'closed'","LastActionCreated DESC"));
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public Issue GetIssue(long issueID) {
            //Get issue
            CRMServiceClient client = null;
            Issue issue = null;
            try {
                client = new CRMServiceClient();
                issue = client.GetIssue(issueID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issue;
        }
        public CRMDataset SearchIssuesAdvanced(string agentNumber,string zone,string store,string agent,string company,string type,string action,string received,string subject,string contact,string originator,string coordinator) {
            //Get issue search data
            CRMDataset issues = new CRMDataset();
            CRMServiceClient client=null;
            try {
                string _zone = (zone != null && zone.Trim().Length > 0) ? zone : null;
                string _store = (store != null && store.Trim().Length > 0) ? store : null;
                string _agent = (agent != null && agent.Trim().Length > 0) ? agent : null;
                string _company = (company != null && company.Trim().Length > 0) ? company : null;
                string _type = (type != null && type.Trim().Length > 0) ? type : null;
                string _action = (action != null && action.Trim().Length > 0) ? action : null;
                string _received = (received != null && received.Trim().Length > 0) ? received : null;
                string _subject = (subject != null && subject.Trim().Length > 0) ? subject : null;
                string _contact = (contact != null && contact.Trim().Length > 0) ? contact : null;
                string _originator = (originator != null && originator.Trim().Length > 0) ? originator : null;
                string _coordinator = (coordinator != null && coordinator.Trim().Length > 0) ? coordinator : null;
                if(_zone==null && _store==null && _agent==null && _company==null && _type==null && _action==null && _received==null && _subject==null && _contact==null && _originator==null && _coordinator==null)
                    issues = new CRMDataset();
                else {
                    object[] criteria = new object[] { _zone,_store,_agent,_company,_type,_action,_received,_subject,_contact,_originator,_coordinator };
                    client = new CRMServiceClient();
                    DataSet ds = client.SearchIssuesAdvancedForAgent(agentNumber,criteria);
                    client.Close();

                    if(ds != null && ds.Tables["IssueTable"] != null && ds.Tables["IssueTable"].Rows.Count > 0) issues.Merge(ds);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public Actions GetIssueActions(long issueID) {
            //Get issue actions
            Issue issue=null;
            CRMServiceClient client=null;
            try {
                client = new CRMServiceClient();
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
        public IssueTypeDataSet GetIssueTypes(string category) {
            //Issue types for a category, or category="" for all types
            IssueTypeDataSet issueTypes = new IssueTypeDataSet();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetIssueTypes(category);
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
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetActionTypes(issueID);
                client.Close();
                if (ds.Tables["ActionTypeTable"] != null && ds.Tables["ActionTypeTable"].Rows.Count > 0) actionTypes.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return actionTypes;
        }
        public bool AddAction(Action action) {
            //Add a new action to an existing issue
            bool added = false;
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                added = client.AddAction(action);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }

        public DeliveryDataset GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            DeliveryDataset deliveries = new DeliveryDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetDeliveries(companyID,storeNumber,from,to);
                client.Close();
                deliveries.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return deliveries;
        }
    }
}