using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Argix.Windows;

namespace Argix.Customers {
	//
	public class IssueCache {
		//Members
        private CRMDataset mIssues=null;
        private DateTime mLastUpdated=DateTime.Now;
        
        //Interface
        public IssueCache(DateTime lastUpdated) {
            //Constructor
            this.mIssues = new CRMDataset();
            this.mLastUpdated = lastUpdated;
        }

        public CRMDataset Issues { get { return mIssues; } }
        public DateTime LastUpdated { get { return mLastUpdated; } }
        public void UpdateCache(CRMDataset issues) {
            //Update issue cache
            DateTime lastUpdated = this.mLastUpdated;
            for(int i=0;i<issues.IssueTable.Rows.Count;i++) {
                CRMDataset.IssueTableRow issue = issues.IssueTable[i];
                CRMDataset.IssueTableRow[] _issues = (CRMDataset.IssueTableRow[])this.mIssues.IssueTable.Select("ID=" + issue.ID);
                if(_issues.Length == 0) {
                    //Not in cache; add new
                    CRMDataset.IssueTableRow _issue = this.mIssues.IssueTable.NewIssueTableRow();
                    #region New to cache: add new issue
                    _issue.ID = issue.ID;
                    if(!issue.IsAgentNumberNull()) _issue.AgentNumber = issue.AgentNumber;
                    if(!issue.IsCompanyIDNull()) _issue.CompanyID = issue.CompanyID;
                    if(!issue.IsCompanyNameNull()) _issue.CompanyName = issue.CompanyName;
                    if(!issue.IsContactNull()) _issue.Contact = issue.Contact;
                    if(!issue.IsCoordinatorNull()) _issue.Coordinator = issue.Coordinator;
                    if(!issue.IsClientRepNull()) _issue.ClientRep = issue.ClientRep;
                    if(!issue.IsDistrictNumberNull()) _issue.DistrictNumber = issue.DistrictNumber;
                    if(!issue.IsFirstActionCreatedNull()) _issue.FirstActionCreated = issue.FirstActionCreated;
                    if(!issue.IsFirstActionDescriptionNull()) _issue.FirstActionDescription = issue.FirstActionDescription;
                    if(!issue.IsFirstActionIDNull()) _issue.FirstActionID = issue.FirstActionID;
                    if(!issue.IsFirstActionUserIDNull()) _issue.FirstActionUserID = issue.FirstActionUserID;
                    if(!issue.IsFirstActionCommentNull()) _issue.FirstActionComment = issue.FirstActionComment;
                    if(!issue.IsLastActionCreatedNull()) _issue.LastActionCreated = issue.LastActionCreated;
                    if(!issue.IsLastActionDescriptionNull()) _issue.LastActionDescription = issue.LastActionDescription;
                    if(!issue.IsLastActionIDNull()) _issue.LastActionID = issue.LastActionID;
                    if(!issue.IsLastActionUserIDNull()) _issue.LastActionUserID = issue.LastActionUserID;
                    if(!issue.IsStoreNumberNull()) _issue.StoreNumber = issue.StoreNumber;
                    if(!issue.IsSubjectNull()) _issue.Subject = issue.Subject;
                    if(!issue.IsTypeNull()) _issue.Type = issue.Type;
                    if(!issue.IsTypeIDNull()) _issue.TypeID = issue.TypeID;
                    if(!issue.IsZoneNull()) _issue.Zone = issue.Zone;
                    #endregion
                    this.mIssues.IssueTable.AddIssueTableRow(_issue);
                    Debug.WriteLine("CACHE: New issue#" + _issue.ID.ToString() + "; lastActionCreated=" + _issue.LastActionCreated.ToString("MM/dd/yyyy HH:mm:ss"));
                }
                else {
                    //In cache; updated?
                    if(issue.LastActionCreated.CompareTo(_issues[0].LastActionCreated) > 0) {
                        #region Update existing
                        if(!issue.IsAgentNumberNull()) _issues[0].AgentNumber = issue.AgentNumber;
                        if(!issue.IsCompanyIDNull()) _issues[0].CompanyID = issue.CompanyID;
                        if(!issue.IsCompanyNameNull()) _issues[0].CompanyName = issue.CompanyName;
                        if(!issue.IsContactNull()) _issues[0].Contact = issue.Contact;
                        if(!issue.IsCoordinatorNull()) _issues[0].Coordinator = issue.Coordinator;
                        if(!issue.IsClientRepNull()) _issues[0].ClientRep = issue.ClientRep;
                        if (!issue.IsDistrictNumberNull()) _issues[0].DistrictNumber = issue.DistrictNumber;
                        if(!issue.IsFirstActionCreatedNull()) _issues[0].FirstActionCreated = issue.FirstActionCreated;
                        if(!issue.IsFirstActionDescriptionNull()) _issues[0].FirstActionDescription = issue.FirstActionDescription;
                        if(!issue.IsFirstActionIDNull()) _issues[0].FirstActionID = issue.FirstActionID;
                        if(!issue.IsFirstActionUserIDNull()) _issues[0].FirstActionUserID = issue.FirstActionUserID;
                        if(!issue.IsFirstActionCommentNull()) _issues[0].FirstActionComment = issue.FirstActionComment;
                        if(!issue.IsLastActionCreatedNull()) _issues[0].LastActionCreated = issue.LastActionCreated;
                        if(!issue.IsLastActionDescriptionNull()) _issues[0].LastActionDescription = issue.LastActionDescription;
                        if(!issue.IsLastActionIDNull()) _issues[0].LastActionID = issue.LastActionID;
                        if(!issue.IsLastActionUserIDNull()) _issues[0].LastActionUserID = issue.LastActionUserID;
                        if(!issue.IsStoreNumberNull()) _issues[0].StoreNumber = issue.StoreNumber;
                        if(!issue.IsSubjectNull()) _issues[0].Subject = issue.Subject;
                        if(!issue.IsTypeNull()) _issues[0].Type = issue.Type;
                        if(!issue.IsTypeIDNull()) _issues[0].TypeID = issue.TypeID;
                        if(!issue.IsZoneNull()) _issues[0].Zone = issue.Zone;
                        _issues[0].AcceptChanges();
                        #endregion
                        Debug.WriteLine("CACHE: Updated issue#" + _issues[0].ID.ToString() + "; lastActionCreated=" + _issues[0].LastActionCreated.ToString("MM/dd/yyyy HH:mm:ss"));
                    }
                }
                if(issue.LastActionCreated.CompareTo(lastUpdated) > 0 && issue.LastActionCreated.CompareTo(this.mLastUpdated) > 0)
                    this.mLastUpdated = issue.LastActionCreated;
            }
        }
    }
}