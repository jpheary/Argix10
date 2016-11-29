using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;

public partial class _LoginAccounts:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.grdAccounts.DataBind();
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAccountSelected(object sender,EventArgs e) {
        //Event handler for user search text entered
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        this.btnRefresh.Enabled = true;
        this.btnEdit.Enabled = this.grdAccounts.SelectedRow != null;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        switch(e.CommandName) {
            case "Refresh":
                this.grdAccounts.DataBind();
                break;
            case "Edit":
                Response.Redirect("~/Admin/LoginAccount.aspx?username=" + this.grdAccounts.SelectedDataKey.Value.ToString(),false);
                break;
        }
    }
}
