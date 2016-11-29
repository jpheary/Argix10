using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.Customers;

public partial class AttachmentNew:System.Web.UI.Page {
    //Members
    private long mIssueID=0, mActionID=0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Get query params
                this.mIssueID = Convert.ToInt64(Request.QueryString["issueID"]);
                ViewState["IssueID"] = this.mIssueID;
                this.mActionID = Convert.ToInt64(Request.QueryString["actionID"]);
                ViewState["ActionID"] = this.mActionID;

                this.lblTitle.Text = "New Attachment (issue# " + this.mIssueID.ToString() + ")";
                this.btnOk.Enabled = true;
            }
            else {
                this.mIssueID = (long)ViewState["IssueID"];
                this.mActionID = (long)ViewState["ActionID"];
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnCommandClick(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            switch(e.CommandName) {
                case "Cancel":   
                    Response.Redirect("~/Default.aspx?issueID=" + this.mIssueID.ToString()); 
                    break;
                case "OK":
                    if (this.fuAttachment.HasFile) {
                        Argix.Customers.Attachment attachment = new Argix.Customers.Attachment();
                        attachment.Filename = this.fuAttachment.FileName;
                        attachment.File = this.fuAttachment.FileBytes;
                        attachment.ActionID = this.mActionID;
                        bool ret = new CustomersGateway().AddAttachment(attachment);
                        Response.Redirect("~/Default.aspx?issueID=" + this.mIssueID.ToString());
                    }
                    else
                        Master.ShowMsgBox("Please select a file.");
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
