using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Argix.Customers;

public partial class Default:System.Web.UI.Page {
    //Members
    private const int CELL_ID=1, CELL_LASTACTIONCREATED=8;
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Create a cookie for the last new issue time (i.e. issues created after this get bolded)
                DateTime lastNewIssueTime = DateTime.Now;
                if (Request.Cookies["LastNewIssueTime"] != null && Request.Cookies["LastNewIssueTime"].Value != null)
                    lastNewIssueTime = DateTime.Parse(Request.Cookies["LastNewIssueTime"].Value);
                else {
                    HttpCookie cookie = new HttpCookie("LastNewIssueTime",lastNewIssueTime.ToString());
                    cookie.Expires = DateTime.Now.AddYears(1);
                    Response.Cookies.Add(cookie);
                }

                ViewState.Add("SearchType", "Standard");
                ViewState.Add("LastNewIssueTime",lastNewIssueTime);
                ViewState.Add("OldItems",new Hashtable());
                Session.Add("AgentNumber",Request.QueryString["agentNumber"]);

                this.cboTerminal.DataBind();
                if (this.cboTerminal.Items.Count > 0) {
                    string agentNumber = Session["AgentNumber"] != null ? Session["AgentNumber"].ToString() : "";
                    if (agentNumber.Length > 0) this.cboTerminal.SelectedValue = agentNumber; else this.cboTerminal.SelectedIndex = 0;
                }
                OnTerminalChanged(null, EventArgs.Empty);

                //Select prior issue (if applicable)
                string issueID = Request.QueryString["issueID"];
                if(issueID != null && issueID.Trim().Length > 0) {
                    for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                        if(this.grdIssues.Rows[i].Cells[CELL_ID].Text == issueID) {
                            this.grdIssues.SelectedIndex = i;
                            break;
                        }
                    }
                }
                OnIssueSelected(this.grdIssues,null);
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnIssueTimerTick(object sender,EventArgs e) {
        //Event handler for issue timer tick event
        try {
            OnIssueToolbarClick(this.tmrRefresh,new CommandEventArgs("Refresh",null));
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //
        try {
            OnViewChanged(null, EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnViewChanged(object sender,EventArgs e) {
        //Event handler for change in combobox view selection
        try {
            switch(this.cboView.SelectedValue) {
                case "Current": this.tmrRefresh.Enabled = Convert.ToBoolean(Session["AutoRefreshOn"]); break;
            }
            OnIssueToolbarClick(null,new CommandEventArgs("Refresh","ViewChange"));
            this.grdIssues.Sort("LastActionCreated", SortDirection.Descending);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnIssueToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for issue toolbar refresh button
        try {
            switch(e.CommandName) {
                case "New": Response.Redirect("~/IssueNew.aspx"); break;
                case "Refresh":
                    string key = this.grdIssues.SelectedDataKey != null ? this.grdIssues.SelectedDataKey[0].ToString() : "";
                    this.grdIssues.DataSourceID = this.cboView.SelectedValue == "Search" ? "odsSearch" : "odsIssues";
                    this.grdIssues.DataBind();
                    for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                        if(key.Length == 0 || this.grdIssues.Rows[i].Cells[CELL_ID].Text == key) {
                            this.grdIssues.SelectedIndex = i;
                            break;
                        }
                    }
                    OnIssueSelected(this.grdIssues,null);
                    break;
                case "Print":   
                    break;
                case "Search":
                    ViewState["SearchType"] = "Standard";
                    this.cboView.SelectedValue = "Search"; OnViewChanged(null,EventArgs.Empty);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnIssueRowDataBound(object sender, GridViewRowEventArgs e) {
        //Event handler for issue row data bound
        try {
            //Bold rows of new issues/actions
            int id=0;
            try { id = Convert.ToInt32(e.Row.Cells[CELL_ID].Text); } catch { }
            if(id > 0) {
                //Init
                DateTime lastNewIssueTime = (DateTime)ViewState["LastNewIssueTime"];
                Hashtable oldItems = (Hashtable)ViewState["OldItems"];
                DateTime dt1;
                if (DateTime.TryParse(e.Row.Cells[CELL_LASTACTIONCREATED].Text, out dt1)) {
                    if(!oldItems.ContainsKey(id)) {
                        e.Row.Font.Bold = dt1.CompareTo(lastNewIssueTime) > 0;  //Not viewed or startup (i.e. collection is empty)
                    } 
                    else {
                        DateTime dt2 = Convert.ToDateTime(oldItems[id]);
                        if(dt1.CompareTo(dt2) > 0) {
                            e.Row.Font.Bold = dt1.CompareTo(dt2) > 0;           //LastActionCreated is different then last time viewed
                        }
                    }
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnIssueSelected(object sender,EventArgs e) {
        //Event handler for change in selected issue
        try {
            this.lblSubject.Text = "";
            if (this.grdIssues.Rows.Count > 0 && this.grdIssues.SelectedRow != null) {
                //Unbold viewed issues/actions
                this.grdIssues.SelectedRow.Font.Bold = false;
                long id = Convert.ToInt64(this.grdIssues.SelectedDataKey[0]);
                DateTime dt1;
                if (DateTime.TryParse(this.grdIssues.SelectedRow.Cells[CELL_LASTACTIONCREATED].Text, out dt1)) {
                    Hashtable oldItems = (Hashtable)ViewState["OldItems"];
                    if(oldItems.ContainsKey(id)) 
                        oldItems[id] = dt1;
                    else 
                        oldItems.Add(id,dt1);
                    ViewState["OldItems"] = oldItems;
                }

                //Toolbar print buttons
                this.btnIssuesPrint.OnClientClick = "javascript:window.open('Issues.aspx','_blank','width=700px,height=440px,toolbar=yes,scrollbars=yes,resizable=yes');return false;";
                this.btnActionsPrint.OnClientClick = "javascript:window.open('IssueDetail.aspx?issueID=" + this.grdIssues.SelectedDataKey[0].ToString() + "','_blank','width=700px,height=440px,toolbar=yes,scrollbars=yes,resizable=yes');return false;";

                //Update subject
                Issue issue = new CustomersGateway().GetIssue(id);
                if(issue != null) {
                    this.lblSubject.Text = issue.Type.Trim();
                    if(issue.CompanyName.Trim() != "All") {
                        this.lblSubject.Text += ": " + issue.CompanyName.Trim();
                        if(issue.StoreNumber > 0)
                            this.lblSubject.Text += " #" + issue.StoreNumber.ToString();
                    }
                    else {
                        if(issue.AgentNumber.Trim() != "All")
                            this.lblSubject.Text += ": Agent#" + issue.AgentNumber.Trim();
                        else
                            this.lblSubject.Text += ": All Agents";
                    }
                    if(issue.Subject.Trim().Length > 0)
                        this.lblSubject.Text += " - " + issue.Subject.Trim();
                }
            }
            this.odsActions.SelectParameters["issueID"].DefaultValue = this.grdIssues.SelectedDataKey != null ? this.grdIssues.SelectedDataKey[0].ToString() : "";
            this.lsvActions.DataBind();
            if(this.lsvActions.Items.Count > 0) this.lsvActions.SelectedIndex = 0;
            OnActonSelected(null,EventArgs.Empty);
            this.upnlIssues.Update();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnIssuesSorting(object sender, GridViewSortEventArgs e) { }
    protected void OnIssuesSorted(object sender, EventArgs e) {
        try {
            this.upnlIssues.Update();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnActionToolbarClick(object sender,CommandEventArgs e) {
        //Event handler for action toolbar refresh button
        try {
            switch(e.CommandName) {
                case "New": 
                    Response.Redirect("~/ActionNew.aspx?issueID=" + (this.grdIssues.SelectedDataKey != null ? this.grdIssues.SelectedDataKey[0].ToString() : ""));
                    break;
                case "Refresh":
                    this.odsActions.SelectParameters["issueID"].DefaultValue = this.grdIssues.SelectedDataKey != null ? this.grdIssues.SelectedDataKey[0].ToString() : "";
                    this.lsvActions.DataBind(); 
                    break;
                case "NewAttachment":
                    Response.Redirect("~/AttachmentNew.aspx?issueID=" + (this.grdIssues.SelectedDataKey != null ? this.grdIssues.SelectedDataKey[0].ToString() : "") + "&actionID=" + (this.lsvActions.SelectedValue != null ? this.lsvActions.SelectedValue.ToString() : ""));
                    break;
                case "Print": 
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnActonSelected(object sender,EventArgs e) {
        //Update attachments and action details
        try {
            this.odsAttachments.SelectParameters["issueID"].DefaultValue = this.lsvActions.SelectedDataKey != null ? this.lsvActions.SelectedDataKey[1].ToString() : "";
            this.odsAttachments.SelectParameters["actionID"].DefaultValue = this.lsvActions.SelectedDataKey != null ? this.lsvActions.SelectedDataKey[0].ToString() : "";
            this.lsvAttachments.DataBind();
            this.odsActionDetail.SelectParameters["issueID"].DefaultValue = this.lsvActions.SelectedDataKey != null ? this.lsvActions.SelectedDataKey[1].ToString() : "";
            this.odsActionDetail.SelectParameters["actionID"].DefaultValue = this.lsvActions.SelectedDataKey != null ? this.lsvActions.SelectedDataKey[0].ToString() : "";
            this.lsvAction.DataBind();
            this.upnlIssues.Update();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
