using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewSearch:System.Web.UI.Page {
    //Members
    private long mIssueID = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
                ViewState.Add("IssueID",this.mIssueID);
                this.Master.SearchButtonFontColor = System.Drawing.Color.White;
            }
            else {
                this.mIssueID = (long)ViewState["IssueID"];
            }
            if(this.mIssueID > 0) this.mvPage.SetActiveView(this.vwIssues);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnOnCommand(object sender,CommandEventArgs e) {
        //Event handler for search buttons
        try {
            switch(e.CommandName) {
                case "Back":
                    this.mvPage.SetActiveView(this.vwSearch);
                    break;
                case "Search":
                    this.mvPage.SetActiveView(this.vwIssues);
                    break;
                case "Reset":
                    this.txtZone.Text = this.txtStore.Text = "";
                    this.cboAgent.SelectedValue = this.cboCompany.SelectedValue = this.cboIssueType.SelectedValue = this.cboActionType.SelectedValue = "";
                    this.txtReceived.Text = this.txtSubject.Text = this.txtOriginator.Text = this.txtCoordinator.Text = "";
                    break;
                case "View":
                    string issueID = e.CommandArgument.ToString();
                    Response.Redirect("ViewIssue.aspx?issueID=" + issueID);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected string GetDateInfo(object lastActionCreated) {
        //
        string dateInfo = "";
        try {
            DateTime created = (DateTime)lastActionCreated;
            bool useYesterday = DateTime.Today.DayOfWeek != DayOfWeek.Monday;
            if(created.CompareTo(DateTime.Today) >= 0) 
                dateInfo = "Today, " + created.ToString("ddd HH:mm");       //Today
            else if(useYesterday && created.CompareTo(DateTime.Today.AddDays(-1)) >= 0) 
                dateInfo = "Yesterday, " + created.ToString("ddd HH:mm");   //Yesterday
            else if(created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek)) >= 0) 
                dateInfo = created.ToString("ddd HH:mm");                   //This Week
            else if(created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek - 7)) >= 0) 
                dateInfo = created.ToString("ddd MM/dd HH:mm");             //Last Week
            else 
                dateInfo = created.ToString("ddd MM/dd/yyyy HH:mm");        //Other
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return dateInfo;
    }
    protected string GetCompanyInfo(object companyName,object storeNumber,object agentNumber) {
        string companyInfo = "";
        try {
            if(companyName.ToString().Trim() != "All") {
                companyInfo += companyName.ToString().Trim();
                if(storeNumber != DBNull.Value && Convert.ToInt32(storeNumber) > 0)
                    companyInfo += " #" + storeNumber.ToString();
            }
            else {
                if(agentNumber.ToString().Trim() != "All")
                    companyInfo += ": Agent#" + agentNumber.ToString().Trim();
                else
                    companyInfo += ": All Agents";
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return companyInfo;
    }
}
