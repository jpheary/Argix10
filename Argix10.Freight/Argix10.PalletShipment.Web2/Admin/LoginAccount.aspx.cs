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
                this.txtUserName.Enabled = true;
                this.txtUserName.ReadOnly = false;
                this.chkLockedOut.Checked = false;
                this.chkLockedOut.Enabled = false;
                this.optRole.SelectedValue = MembershipServices.GUESTROLE;
                OnRoleChanged(this.optRole,EventArgs.Empty);
            }
            else {
                //Existing member
                //  Membership
                MembershipUser member = Membership.GetUser(this.mUserName,false);
                this.txtUserName.Text = member.UserName;
                this.txtUserName.Enabled = false;
                this.txtEmail.Text = member.Email;
                try { if(!member.IsLockedOut) this.txtPassword.Text = member.GetPassword(); } catch(Exception ex) { Master.ReportError(ex, 3); }
                this.txtComments.Text = member.Comment;
                this.chkApproved.Checked = member.IsApproved;
                this.chkLockedOut.Checked = member.IsLockedOut;

                //  Profile
                ProfileCommon profile = new ProfileCommon().GetProfile(this.mUserName);
                this.txtClientID.Text = profile.ClientID;

                //  Roles
                if(Roles.IsUserInRole(this.mUserName,MembershipServices.GUESTROLE)) this.optRole.SelectedValue = MembershipServices.GUESTROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.ADMINROLE)) this.optRole.SelectedValue = MembershipServices.ADMINROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.SALESROLE)) this.optRole.SelectedValue = MembershipServices.SALESROLE;
                else if(Roles.IsUserInRole(this.mUserName,MembershipServices.CLIENTROLE)) this.optRole.SelectedValue = MembershipServices.CLIENTROLE;
                OnRoleChanged(this.optRole,EventArgs.Empty);
            }
            OnValidateForm(null,EventArgs.Empty);
        }
        else {
            this.mUserName = ViewState["username"].ToString();
        }
    }
    protected void OnApprovedChanged(object sender,EventArgs e) {
        //Event handler for change in approved status
        //Cannot be a Guest once approved
        if (this.chkApproved.Checked) this.optRole.Items[0].Selected = false;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnRoleChanged(object sender,EventArgs e) {
        //Event handler for change in roles
        switch (this.optRole.SelectedValue) {
            case MembershipServices.GUESTROLE:
                //Guest member- cannot be in approved state, no supplemental roles
                this.chkApproved.Checked = false;
                this.chkApproved.Enabled = false;
                this.optRole.Items[1].Selected = this.optRole.Items[2].Selected = this.optRole.Items[3].Selected = false;
                break;
            case MembershipServices.ADMINROLE:
                //Administrator role- automatically approved, no supplemental roles required
                //this.chkApproved.Checked = true;
                this.chkApproved.Enabled = true;
                this.optRole.Items[0].Selected = this.optRole.Items[2].Selected = this.optRole.Items[3].Selected = false;
                break;
            case MembershipServices.SALESROLE:
                //SalesRep role- automatically approved, no supplemental roles
                //this.chkApproved.Checked = true;
                this.chkApproved.Enabled = true;
                this.optRole.Items[0].Selected = this.optRole.Items[1].Selected = this.optRole.Items[3].Selected = false;
                break;
            case MembershipServices.CLIENTROLE:
                //Client role- automatically approved, supplemental roles as needed
                //this.chkApproved.Checked = true;
                this.chkApproved.Enabled = true;
                this.optRole.Items[0].Selected = this.optRole.Items[1].Selected = this.optRole.Items[2].Selected = false;
                break;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for validating user services
        //Set services
        this.btnClose.Enabled = true;
        this.btnSubmit.Enabled = this.txtUserName.Text.Length > 0 &&
                             this.txtEmail.Text.Length > 0 &&
                             this.txtPassword.Text.Length > 0 &&
                             this.txtClientID.Text.Length > 0 &&
                             (this.optRole.Items[0].Selected || this.optRole.Items[1].Selected || this.optRole.Items[2].Selected || this.optRole.Items[3].Selected);
        this.btnUnlock.Enabled = this.chkLockedOut.Checked;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for cancel button clicked
        MembershipUser member = null;
        switch (e.CommandName) {
            case "Unlock":
                //Unlock user if locked out
                member = Membership.GetUser(this.txtUserName.Text,false);
                if (member.IsLockedOut) {
                    if (member.UnlockUser()) {
                        Master.ShowMessageBox(this.txtUserName.Text + " account was unlocked successfully.");
                        try {
                            if (!member.IsLockedOut) this.txtPassword.Text = member.GetPassword();
                        }
                        catch (Exception ex) { Master.ReportError(ex,3); }
                        this.chkLockedOut.Checked = member.IsLockedOut;
                    }
                    else {
                        Master.ShowMessageBox(this.txtUserName.Text + " account failed to unlock.");
                    }
                }
                OnValidateForm(null,EventArgs.Empty);
                break;
            case "Close":
                Response.Redirect("~/Admin/LoginAccounts.aspx?username=" + HttpUtility.UrlEncode(this.mUserName),false);
                break;
            case "OK":
                if(!Page.IsValid) return;
                bool isNewMember = this.mUserName.Length == 0;
                if(isNewMember) {
                    //Create a new user
                    MembershipCreateStatus status;
                    member = Membership.CreateUser(this.txtUserName.Text.Trim(),this.txtPassword.Text.Trim(),this.txtEmail.Text.Trim(),null,null,this.chkApproved.Checked,out status);
                    member.Comment = this.txtComments.Text;
                    Membership.UpdateUser(member);
                    switch(status) {
                        case MembershipCreateStatus.Success:
                            //Update profile (add user to guest role 'cause anonymous user cannot have a profile)
                            ProfileCommon profile = new ProfileCommon().GetProfile(this.txtUserName.Text);
                            profile.ClientID = this.txtClientID.Text;
                            profile.Save();

                            //Update roles
                            if(this.optRole.Items[0].Selected) Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.GUESTROLE);
                            if(this.optRole.Items[1].Selected) {
                                Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.ADMINROLE);
                            }
                            if(this.optRole.Items[2].Selected) Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.SALESROLE);
                            if(this.optRole.Items[3].Selected) Roles.AddUserToRole(this.txtUserName.Text.Trim(),MembershipServices.CLIENTROLE);
                            
                            Master.ShowMessageBox(this.txtUserName.Text + " was created successfully.");
                            this.btnSubmit.Enabled = false;
                            break;
                        case MembershipCreateStatus.DuplicateEmail: Master.ShowMessageBox("Failed to create new member- DuplicateEmail."); break;
                        case MembershipCreateStatus.DuplicateProviderUserKey: Master.ShowMessageBox("Failed to create new member- DuplicateProviderUserKey"); break;
                        case MembershipCreateStatus.DuplicateUserName: Master.ShowMessageBox("Failed to create new member- DuplicateUserName"); break;
                        case MembershipCreateStatus.InvalidAnswer: Master.ShowMessageBox("Failed to create new member- InvalidAnswer"); break;
                        case MembershipCreateStatus.InvalidEmail: Master.ShowMessageBox("Failed to create new member- InvalidEmail"); break;
                        case MembershipCreateStatus.InvalidPassword: Master.ShowMessageBox("Failed to create new member- InvalidPassword"); break;
                        case MembershipCreateStatus.InvalidProviderUserKey: Master.ShowMessageBox("Failed to create new member- InvalidProviderUserKey"); break;
                        case MembershipCreateStatus.InvalidQuestion: Master.ShowMessageBox("Failed to create new member- InvalidQuestion"); break;
                        case MembershipCreateStatus.InvalidUserName: Master.ShowMessageBox("Failed to create new member- InvalidUserName"); break;
                        case MembershipCreateStatus.ProviderError: Master.ShowMessageBox("Failed to create new member- ProviderError"); break;
                        case MembershipCreateStatus.UserRejected: Master.ShowMessageBox("Failed to create new member- UserRejected"); break;
                    }
                }
                else {
                    //Update existing user if account is not locked
                    member = Membership.GetUser(this.mUserName);
                    if(member.IsLockedOut) {
                        Master.ShowMessageBox(this.mUserName + " account must be unlocked before updating.");
                        return;
                    }
                    //Membership
                    if(member.GetPassword() != this.txtPassword.Text) member.ChangePassword(member.GetPassword(),this.txtPassword.Text);
                    member.Comment = this.txtComments.Text;
                    member.IsApproved = this.chkApproved.Checked;
                    member.Email = this.txtEmail.Text;
                    Membership.UpdateUser(member);

                    //Profile
                    ProfileCommon profile = new ProfileCommon().GetProfile(this.mUserName);
                    profile.ClientID = this.txtClientID.Text;
                    profile.Save();

                    //Roles
                    for(int i=0;i<this.optRole.Items.Count;i++) {
                        if(this.optRole.Items[i].Selected && !Roles.IsUserInRole(this.mUserName,this.optRole.Items[i].Value)) Roles.AddUserToRole(this.mUserName,this.optRole.Items[i].Value);
                        if(!this.optRole.Items[i].Selected && Roles.IsUserInRole(this.mUserName,this.optRole.Items[i].Value)) Roles.RemoveUserFromRole(this.mUserName,this.optRole.Items[i].Value);
                    }
                    this.btnSubmit.Enabled = false;
                    Master.ShowMessageBox(this.txtUserName.Text + " was updated successfully.");
                }
                break;
        }
    }
}
