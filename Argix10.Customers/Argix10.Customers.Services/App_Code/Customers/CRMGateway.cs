using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Customers {
    //
    public class CRMGateway {
        //Members
        public const string SQL_CONNID = "CRM";
        public const string USP_ISSUES = "uspCRMIssueGetList2",TBL_ISSUES = "IssueTable";
        public const string USP_ISSUES_SEARCH = "uspCRMIssueSearchGetList";
        public const string USP_ISSUES_SEARCHADVANCED = "uspCRMIssueSearchAdvancedGetList";
        public const string USP_ISSUE_GET = "uspCRMIssueGet",TBL_ISSUE = "IssueTable";
        public const string USP_ACTIONS = "uspCRMActionGetList",TBL_ACTIONS = "ActionTable";
        public const string USP_ATTACHMENTS = "uspCRMAttachmentGetList",TBL_ATTACHMENTS="AttachmentTable";
        public const string USP_ATTACHMENT_GET = "uspCRMAttachmentGet";

        public const string USP_ISSUE_NEW = "uspCRMIssueNew";
        public const string USP_ACTION_NEW = "uspCRMActionNew";
        public const string USP_ATTACHMENT_NEW = "uspCRMAttachmentNew";

        public const string USP_ISSUETYPES = "uspCRMIssueTypesGetList2",TBL_ISSUETYPES = "IssueTypeTable";
        public const string USP_ISSUETYPE_NEW = "uspCRMIssueTypeNew", USP_ISSUETYPE_UPDATE = "uspCRMIssueTypeUpdate";
        public const string USP_ACTIONTYPES = "uspCRMActionTypesGetList",TBL_ACTIONTYPES = "ActionTypeTable";
       

        //Interface
        public CRMGateway() { }

        public DataSet ViewIssues(DateTime fromDate) {
            //Get issues
            DataSet issues = new DataSet();
            try {
                DateTime toDate = DateTime.Today.AddDays(1).AddSeconds(-1);
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ISSUES,TBL_ISSUES,new object[] { fromDate,toDate });
                if(ds != null && ds.Tables[TBL_ISSUES].Rows.Count > 0) issues.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return issues;
        }
        public DataSet SearchIssues(string searchText) {
            //Get issue search data
            DataSet search = new DataSet();
            try {
                //Validate data access
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ISSUES_SEARCH,TBL_ISSUES,new object[] { searchText });
                if(ds != null && ds.Tables[TBL_ISSUES].Rows.Count > 0) search.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return search;
        }
        public DataSet SearchIssuesAdvanced(object[] criteria) {
            //Get issue search data
            DataSet search = new DataSet();
            try {
                //Validate data access
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ISSUES_SEARCHADVANCED,TBL_ISSUES,criteria);
                if(ds != null && ds.Tables[TBL_ISSUES].Rows.Count > 0) search.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return search;
        }
        public DataSet GetIssue(long issueID) {
            //Get an existing issue
            DataSet issue = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ISSUE_GET,TBL_ISSUE,new object[] { issueID });
                if(ds != null) issue.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return issue;
        }
        public DataSet GetActions(long issueID) {
            //Get an existing issue
            DataSet actions = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ACTIONS,TBL_ACTIONS,new object[] { issueID });
                if(ds != null && ds.Tables[TBL_ACTIONS] != null && ds.Tables[TBL_ACTIONS].Rows.Count > 0) {
                    DataSet _ds = new DataSet();
                    _ds.Merge(ds.Tables[TBL_ACTIONS].Select("","Created DESC"));
                    actions.Merge(_ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return actions;
        }
        public DataSet GetAttachments(long issueID) {
            //Get an existing issue
            DataSet attachments = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ATTACHMENTS,TBL_ATTACHMENTS,new object[] { issueID });
                if(ds != null) attachments.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return attachments;
        }
        public byte[] GetAttachment(int id) {
            //Get an existing file attachment from database
            byte[] bytes=null;
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_ATTACHMENT_GET, TBL_ATTACHMENTS, new object[] { id });
                if(ds != null && ds.Tables[TBL_ATTACHMENTS].Rows.Count > 0) bytes = (byte[])ds.Tables[TBL_ATTACHMENTS].Rows[0]["File"];
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return bytes;
        }

        public long CreateIssue(int typeID,string subject,string contact, int companyID, string regionNumber, string districtNumber,string agentNumber,int storeNumber) {
            //Create a new issue
            long iid = 0;
            try {
                //Save issue
                object store = null;
                if(storeNumber > 0) store = storeNumber;
                object io = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_ISSUE_NEW,new object[] { null,typeID,subject,contact,companyID,regionNumber,districtNumber,agentNumber,store });
                iid = (long)io;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return iid;
        }
        public bool CreateAction(byte typeID,long issueID,string userID,string comment) {
            //Create a new action
            bool b = false;
            try {
                //Validate
                if(issueID == 0) return false;

                //Add any new actions and associated attachments
                object ao = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_ACTION_NEW,new object[] { null,typeID,issueID,userID,comment });
                b = true;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return b;
        }
        public bool CreateAttachment(string name,byte[] bytes,long actionID) {
            //Create a new issue
            bool saved=false;
            try {
                //Save issue
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_ATTACHMENT_NEW,new object[] { null,name,bytes,actionID });
                int id = (int)o;
                saved = (id > 0);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return saved;
        }

        public DataSet GetIssueTypes() {
            //Issue types- all or filtered by category
            DataSet issueTypes = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ISSUETYPES,TBL_ISSUETYPES,new object[] { });
                if(ds != null) issueTypes.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return issueTypes;
        }
        public bool CreateIssueType(string type, string category, string description) {
            //Create a new issue type
            bool created = false;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_ISSUETYPE_NEW, new object[] { type, category, description });
                created = o != null;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return created;
        }
        public bool UpdateIssueType(int id, string description, int isactive) {
            //Update an existing issue type
            bool updated = false;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_ISSUETYPE_UPDATE, new object[] { id, description, isactive });
                updated = o != null;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return updated;
        }
        public DataSet GetActionTypes() {
            //Get a list of all action types
            DataSet types = new DataSet();
            try {
                //Get full list
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ACTIONTYPES,TBL_ACTIONTYPES,new object[] { });
                if(ds != null) types.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return types;
        }
    }
}
