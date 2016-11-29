using System;
using System.Data;
using System.Web.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _LoginAccount :System.Web.UI.Page {
    //Members
    private string mUserName="";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Get query request and setup for new or existing user
            this.mUserName = Request.QueryString["username"] == null ? "" : Request.QueryString["username"].ToString();
            ViewState.Add("username",this.mUserName);
            if(this.mUserName.Length == 0) {
                //New member
            }
            else {
                //Existing member
                //  Membership
                MembershipUser member = Membership.GetUser(this.mUserName,false);
                this.txtUserName.Text = member.UserName;
                this.txtEmail.Text = member.Email;
                try { if(!member.IsLockedOut) this.txtPassword.Text = member.GetPassword(); } catch(Exception ex) { Master.ReportError(ex, 3); }
                this.txtComments.Text = member.Comment;
                this.chkApproved.Checked = member.IsApproved;
                this.chkLockedOut.Checked = member.IsLockedOut;

                //  Profile
                ProfileCommon profileCommon = new ProfileCommon();
                ProfileCommon profile = profileCommon.GetProfile(this.mUserName);
                this.txtCompany.Text = profile.ClientID;

                //  Roles
                if(Roles.IsUserInRole(this.mUserName,MembershipServices.GUESTROLE))
                    this.optRole.SelectedValue = MembershipServices.GUESTROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.ADMINROLE))
                    this.optRole.SelectedValue = MembershipServices.ADMINROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.SALESROLE))
                    this.optRole.SelectedValue = MembershipServices.SALESROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.CLIENTROLE))
                    this.optRole.SelectedValue = MembershipServices.CLIENTROLE;
            }
            OnValidateForm(null,EventArgs.Empty);
        }
        else {
            this.mUserName = ViewState["username"].ToString();
        }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        //Set services
        this.btnSubmit.Enabled = false;
        this.btnClose.Enabled = true;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for cancel button clicked
        switch(e.CommandName) {
            case "Close":
                Response.Redirect("~/Admin/LoginAccounts.aspx?username=" + HttpUtility.UrlEncode(this.mUserName),false);
                break;
            case "OK":
                break;
        }
    }
}
