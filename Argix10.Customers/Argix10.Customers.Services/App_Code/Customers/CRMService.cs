using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Transactions;
using System.Threading;
using Argix.Enterprise;

namespace Argix.Customers {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    public class CRMService:ICRMService {
        //Members

        //Interface
        public CRMService() { }
        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            return new AppService(CRMGateway.SQL_CONNID).GetServiceInfo(); ;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(CRMGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet ViewIssues() { return ViewIssues(DateTime.Today.AddDays(-Convert.ToInt32(ConfigurationManager.AppSettings["IssueDaysBack"]))); }
        public DataSet ViewIssues(DateTime fromDate) {
            //Get issues
            DataSet ds = null;
            try {
                ds = new CRMGateway().ViewIssues(fromDate);
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewIssues(string agentNumber) {
            //View issues for the specified agent
            DataSet issues = new DataSet();
            try {
                DataSet ds = ViewIssues();
                if(ds != null) {
                    CRMDataset _issues = new CRMDataset();
                    _issues.Merge(ds);
                    if(agentNumber == null)
                        issues.Merge(_issues.IssueTable.Select("","LastActionCreated DESC"));
                    else
                        issues.Merge(_issues.IssueTable.Select("AgentNumber='" + agentNumber + "'","LastActionCreated DESC"));
                }
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return issues;
        }
        public DataSet SearchIssues(string searchText) {
            //Get issue search data
            DataSet ds = null;
            try {
                ds = new CRMGateway().SearchIssues(searchText);
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet SearchIssuesAdvanced(object[] criteria) {
            //Get issue search data
            DataSet ds = null;
            try {
                ds = new CRMGateway().SearchIssuesAdvanced(criteria);
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet SearchIssuesAdvanced(string agentNumber,object[] criteria) {
            //Get issue search data
            DataSet issues = new DataSet();
            try {
                DataSet ds = SearchIssuesAdvanced(criteria);
                if(ds != null) {
                    CRMDataset _issues = new CRMDataset();
                    _issues.Merge(ds);
                    if(agentNumber == null)
                        issues.Merge(_issues.IssueTable.Select("","LastActionCreated DESC"));
                    else
                        issues.Merge(_issues.IssueTable.Select("AgentNumber='" + agentNumber + "'","LastActionCreated DESC"));
                }
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return issues;
        }
        public Issue GetIssue(long issueID) {
            //Get an existing issue
            Issue issue = null;
            try {
                //Load the issue
                CRMDataset issuesDS = new CRMDataset();
                issuesDS.Merge(new CRMGateway().GetIssue(issueID));
                if (issuesDS.IssueTable.Rows.Count > 0) {
                    issue = new Issue(issuesDS.IssueTable[0]);

                    //Get the actions and attachments for this issue
                    CRMDataset actionDS = new CRMDataset();
                    actionDS.Merge(new CRMGateway().GetActions(issueID));
                    CRMDataset attachmentDS = new CRMDataset();
                    attachmentDS.Merge(new CRMGateway().GetAttachments(issueID));

                    //Attach actions to the issue and attachents to the actions
                    Actions actions = new Actions();
                    if (actionDS != null) {
                        for (int i = 0;i < actionDS.ActionTable.Rows.Count;i++) {
                            //Load an action and add to actions collection
                            Action action = new Action(actionDS.ActionTable[i]);
                            actions.Add(action);

                            //Attach attachments to this action if applicable
                            if (action.AttachmentCount > 0 && attachmentDS != null) {
                                Attachments attachments = new Attachments();
                                for (int j = 0;j < attachmentDS.AttachmentTable.Rows.Count;j++) {
                                    Attachment attachment = new Attachment(attachmentDS.AttachmentTable[j]);
                                    if (attachment.ActionID == action.ID) attachments.Add(attachment);
                                }
                                action.Attachments = attachments;
                            }
                            else
                                action.Attachments = new Attachments();
                        }
                    }
                    issue.Actions = actions;
                }
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message), "Service Error"); }
            return issue;
        }
        public byte[] GetAttachment(int id) {
            //Get an existing file attachment from database
            byte[] bytes=null;
            try {
                bytes = new CRMGateway().GetAttachment(id);
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return bytes;
        }
        public long CreateIssue(Issue issue) {
            //Create a new issue
            long iid = 0;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    //Create issue
                    object io = new CRMGateway().CreateIssue(issue.TypeID,issue.Subject,issue.Contact,issue.CompanyID,issue.RegionNumber,issue.DistrictNumber,issue.AgentNumber,issue.StoreNumber);
                    iid = (long)io;

                    //Add the single 'Open' action
                    if(issue.Actions.Count == 0) throw new ApplicationException("No action specified."); ;
                    bool ok = new CRMGateway().CreateAction(issue.Actions[0].TypeID,iid,issue.Actions[0].UserID,issue.Actions[0].Comment);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return iid;
        }
        public bool AddAction(Action action) {
            //Add a new action to an existing issue
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    added = new CRMGateway().CreateAction(action.TypeID,action.IssueID,action.UserID,action.Comment);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return added;
        }
        public bool AddAttachment(Attachment attachment) {
            //Create a new issue
            bool added=false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    added = new CRMGateway().CreateAttachment(attachment.Filename,attachment.File,attachment.ActionID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Service Error"); }
            return added;
        }

        public DataSet GetIssueTypes(string issueCategory) {
            //Issue types- all or filtered by category
            DataSet issueTypes = new DataSet();
            try {
                DataSet ds = new CRMGateway().GetIssueTypes();
                if(ds != null && ds.Tables["IssueTypeTable"] != null && ds.Tables["IssueTypeTable"].Rows.Count > 0) {
                    if(issueCategory != null && issueCategory.Trim().Length > 0)
                        issueTypes.Merge(ds.Tables["IssueTypeTable"].Select("Category='" + issueCategory + "'"));
                    else
                        issueTypes.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return issueTypes;
        }
        public bool CreateIssueType(string type, string category, string description) {
            //Create a new issue type
            bool created = false;
            try {
                //
                created = new CRMGateway().CreateIssueType(type, category, description);
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message), "Service Error"); }
            return created;
        }
        public bool UpdateIssueType(int id, string description, int isactive) {
            //Update an existing issue type
            bool updated = false;
            try {
                //
                updated = new CRMGateway().UpdateIssueType(id, description, isactive);
            }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message), "Service Error"); }
            return updated;
        }
        public DataSet GetActionTypes(long issueID) {
            //Action types for an issue (state driven)
            DataSet types=new DataSet();
            try {
                //Get full list
                CRMDataset actionTypes = new CRMDataset();
                actionTypes.Merge(new CRMGateway().GetActionTypes());

                //Remove actions that don't apply
                Actions actions = new Actions();
                CRMDataset actionsDS = new CRMDataset();
                actionsDS.Merge(new CRMGateway().GetActions(issueID));
                for(int i=0;i<actionsDS.ActionTable.Rows.Count;i++) actions.Add(new Action(actionsDS.ActionTable[i]));
                if(actions.Count == 0) {
                    //New: Notify All, Notify Agent Systems, Notify CRG
                    for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                        if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                        else if(actionTypes.ActionTypeTable[i].ID == 2) actionTypes.ActionTypeTable[i].Delete();
                        else if(actionTypes.ActionTypeTable[i].ID == 3) actionTypes.ActionTypeTable[i].Delete();
                    }
                }
                else if(actions.Count == 1) {
                    //Open: Dismiss, Notify All, Notify Agent Systems, Notify CRG
                    for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                        if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                        else if(actionTypes.ActionTypeTable[i].ID == 3) actionTypes.ActionTypeTable[i].Delete();
                    }
                }
                else if(actions.Count > 1) {
                    //Unresolved: Close, Notify All, Notify Agent Systems, Notify CRG, Other
                    for(int i = 0;i < actionTypes.ActionTypeTable.Rows.Count;i++) {
                        if(actionTypes.ActionTypeTable[i].ID == 1) actionTypes.ActionTypeTable[i].Delete();
                        else if(actionTypes.ActionTypeTable[i].ID == 2) actionTypes.ActionTypeTable[i].Delete();
                    }
                }
                actionTypes.AcceptChanges();
                types.Merge(actionTypes);

            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return types;
        }

        public DataSet GetTerminals() {
            //Get a list of Argix terminals
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetTerminals();
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetCompanies() {
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetCompanies();
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetRegionsDistricts(string clientNumber) {
            //Get a list of client districts
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetRegionsDistricts(clientNumber);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            DataSet ds = null;
            try {
                ds = new EnterpriseGateway().GetAgents(clientNumber);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetAgents() {
            //Get a list of all agents
            DataSet ds = null;
            try {
                ds = new EnterpriseGateway().GetAgents();
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetAgentTerminals(string agentNumber) {
            //Get a list of agent terminals for the specified agent (null returns all)
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetAgentTerminals(agentNumber);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetStoreDetail(companyID,storeNumber);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetStoreDetail(companyID,subStore);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }

        public DataSet GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //
            DataSet ds=null;
            try {
                ds = new EnterpriseGateway().GetDeliveries(companyID,storeNumber, from, to);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return ds;
        }
    }
}