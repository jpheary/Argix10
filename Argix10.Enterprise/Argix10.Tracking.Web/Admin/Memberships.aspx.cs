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

public partial class _Memberships:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            //Get query request
            if(!Page.IsPostBack) {
                //Initialize control values                
                string username = Request.QueryString["username"] == null ? "" : Request.QueryString["username"].ToString();

                if (Cache[this.odsMembers.CacheKeyDependency] == null) Cache[this.odsMembers.CacheKeyDependency] = new object();
                this.grdMembers.DataBind();
                if(username.Length > 0) {
                    for(int i=0;i<this.grdMembers.Rows.Count;i++) {
                        if(this.grdMembers.Rows[i].Cells[1].Text == username) {
                            this.grdMembers.SelectedIndex = i;

                            System.Text.StringBuilder script = new System.Text.StringBuilder();
                            script.Append("<script language=javascript>");
                            script.Append("scroll('" + username + "')");
                            script.Append("</script>");
                            Page.ClientScript.RegisterStartupScript(typeof(Page),"Scroll",script.ToString());
                            Cache.Remove(this.odsMembers.CacheKeyDependency);
                            break;
                        }
                    }
                }
                OnValidateForm(null,EventArgs.Empty);
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnMemberSelected(object sender,EventArgs e) {
        //Event handler for user search text entered
        try {
            OnValidateForm(null,EventArgs.Empty);
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnAdd.Enabled = true;
            this.btnUpdate.Enabled = this.grdMembers.SelectedRow != null;
            this.btnDelete.Enabled = this.grdMembers.SelectedRow != null;
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            switch(e.CommandName) {
                case "Add":
                    Cache.Remove(this.odsMembers.CacheKeyDependency);
                    Response.Redirect("~/Admin/MembershipUserPage.aspx?username=",false);
                    break;
                case "Edit":
                    Cache.Remove(this.odsMembers.CacheKeyDependency);
                    Response.Redirect("~/Admin/MembershipUserPage.aspx?username=" + this.grdMembers.SelectedDataKey.Value.ToString(),false);
                    break;
                case "Delete":
                    Membership.DeleteUser(this.grdMembers.SelectedDataKey.Value.ToString());
                    Master.ShowMessageBox(this.grdMembers.SelectedDataKey.Value.ToString() + " was deleted successfully.");
                    Cache[this.odsMembers.CacheKeyDependency] = new object(); 
                    this.grdMembers.DataBind();
                    OnValidateForm(null,EventArgs.Empty);
                    break;
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 4); }
    }
}
