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
using System.Xml.Linq;
using Argix.Customers;

public partial class ActionNew:System.Web.UI.Page {
    //Members
    private long mIssueID=0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Get query params
                this.mIssueID = Request.QueryString["issueID"].Length > 0 ? Convert.ToInt64(Request.QueryString["issueID"]) : 0;
                ViewState["IssueID"] = this.mIssueID;

                this.lblTitle.Text = "New Action (issue# " + this.mIssueID.ToString() + ")";
                if (this.cboActionType.Items.Count > 0) this.cboActionType.SelectedIndex = 0;
                OnTypeChanged(this.cboActionType,EventArgs.Empty);
            }
            else {
                this.mIssueID = (long)ViewState["IssueID"];
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnCommentsChanged(object sender,EventArgs e) {
        //Event handler for command button clicked
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for control value changes
        this.btnOk.Enabled = this.mIssueID > 0;
    }
    protected void OnCommandClick(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            switch(e.CommandName) {
                case "Cancel":
                    Response.Redirect("~/Default.aspx?issueID=" + this.mIssueID.ToString());
                    break;
                case "OK": 
                    Argix.Customers.Action action = new Argix.Customers.Action();
                    action.TypeID = Convert.ToByte(this.cboActionType.SelectedValue);
                    action.IssueID = this.mIssueID;
                    action.UserID = HttpContext.Current.User.Identity.Name;
                    action.Comment = this.txtComments.Text;
                    bool ret = new CustomersGateway().AddAction(action);
                    Response.Redirect("~/Default.aspx?issueID=" + this.mIssueID.ToString());
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
