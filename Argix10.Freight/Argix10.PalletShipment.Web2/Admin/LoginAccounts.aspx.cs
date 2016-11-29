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
    public int mView = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values                
            string username = Request.QueryString["username"] == null ? "" : Request.QueryString["username"].ToString();

            this.grdAccounts.DataBind();
            if(username.Length > 0) {
                for(int i = 0; i < this.grdAccounts.Rows.Count; i++) {
                    if(this.grdAccounts.Rows[i].Cells[1].Text == username) {
                        this.grdAccounts.SelectedIndex = i;

                        System.Text.StringBuilder script = new System.Text.StringBuilder();
                        script.Append("<script type='text/jscript'>scroll('" + username + "');</script>");
                        Page.ClientScript.RegisterStartupScript(typeof(Page), "Scroll", script.ToString());
                        break;
                    }
                }
            }
            OnValidateForm(null, EventArgs.Empty);
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAccountSelected(object sender,EventArgs e) {
        //Event handler for user search text entered
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        this.btnAdd.Enabled = true;
        this.btnUpdate.Enabled = this.grdAccounts.SelectedRow != null;
        this.btnDelete.Enabled = this.grdAccounts.SelectedRow != null;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        switch(e.CommandName) {
            case "Add":
                Response.Redirect("~/Admin/LoginAccount.aspx?username=", false);
                break;
            case "Edit":
                Response.Redirect("~/Admin/LoginAccount.aspx?username=" + this.grdAccounts.SelectedDataKey.Value.ToString(),false);
                break;
            case "Delete":
                Membership.DeleteUser(this.grdAccounts.SelectedDataKey.Value.ToString());
                Master.ShowMessageBox(this.grdAccounts.SelectedDataKey.Value.ToString() + " was deleted successfully.");
                this.grdAccounts.DataBind();
                OnValidateForm(null, EventArgs.Empty);
                break;
        }
    }
}
