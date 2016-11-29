using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewIssue:System.Web.UI.Page {
    //Members
    private long mIssueID = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query params
            this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
            ViewState.Add("IssueID", this.mIssueID);

            this.Master.IssuesButtonFontColor = System.Drawing.Color.White;
            //this.Master.SearchButtonFontColor = System.Drawing.Color.White;
            
            this.cboActionType.DataBind();
            this.cboActionType.SelectedIndex = -1;
        }
        else {
            this.mIssueID = (long)ViewState["IssueID"];
        }

        Argix.Customers.Issue issue = new Argix.Customers.CustomersGateway().GetIssue(this.mIssueID);
        if(issue != null) {
            this.lblType.Text = issue.Type.Trim();
            this.lblCompany.Text = "";
            if(issue.CompanyName.Trim() != "All") {
                this.lblCompany.Text += issue.CompanyName.Trim();
                if(issue.StoreNumber > 0)
                    this.lblCompany.Text += " #" + issue.StoreNumber.ToString();
            }
            else {
                if(issue.AgentNumber.Trim() != "All")
                    this.lblCompany.Text += ": Agent#" + issue.AgentNumber.Trim();
                else
                    this.lblCompany.Text += ": All Agents";
            }
            this.lblSubject.Text = issue.Subject.Trim();
        }
    }
    protected void OnOnCommand(object sender,CommandEventArgs e) {
        //Event handler for refresh button clicked
        switch (e.CommandName) {
            case "Back":
                Response.Redirect("ViewIssues.aspx?issueID=" + this.mIssueID.ToString());
                break;
            case "New":
                Argix.Customers.Action action = new Argix.Customers.Action();
                action.IssueID = this.mIssueID;
                action.TypeID = byte.Parse(this.cboActionType.SelectedValue);
                action.UserID = HttpContext.Current.User.Identity.Name;
                action.Created = DateTime.Now;
                action.Comment = this.txtComment.Text;
                bool added = new Argix.Customers.CustomersGateway().AddAction(action);
                this.lsvAction.DataBind();
                break;
        }
    }
}
