using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewIssues:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.Master.IssuesButtonFontColor = System.Drawing.Color.White;

                this.cboTerminal.DataBind();
                if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnOnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "Refresh":
                    this.lsvIssues.DataBind();
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
        string companyInfo="";
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
