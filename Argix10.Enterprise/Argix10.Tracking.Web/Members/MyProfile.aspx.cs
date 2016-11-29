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

public partial class _MyProfile :System.Web.UI.Page {
    //Members
    private string mUserName="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Get query request and setup for new or existing user
                this.mUserName = new MembershipServices().Username;
                ViewState.Add("username",this.mUserName);

                //  Membership
                MembershipUser member = Membership.GetUser(this.mUserName,false);
                this.txtUserName.Text = member.UserName;
                this.txtUserName.Enabled = false;
                this.txtEmail.Text = member.Email;
                //try { if(!member.IsLockedOut) this.txtPassword.Text = member.GetPassword(); } catch { }
                //this.txtComments.Text = member.Comment;

                //  Profile
                ProfileCommon profileCommon = new ProfileCommon();
                ProfileCommon profile = profileCommon.GetProfile(this.mUserName);
                this.txtFullName.Text = profile.UserFullName;
                this.txtCompany.Text = profile.Company;
                //this.txtCompanyType.Text = profile.Type;
                //this.txtCustomer.Text = profile.ClientVendorID;
                this.txtStoreNumber.Text = profile.StoreNumber;
            }
            else
                this.mUserName = ViewState["username"].ToString();
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnEmailChanged(object sender,EventArgs e) {
        //Event handler for change in email
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnFullNameChanged(object sender,EventArgs e) {
        //Event handler for change in full name
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        //Set services
        this.btnUpdate.Enabled = this.txtEmail.Text.Length > 0 && this.txtFullName.Text.Length > 0;
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for cancel button clicked
        MembershipUser member=null;
        try {
            switch(e.CommandName) {
                case "Update":
                    if(!Page.IsValid) return;
                    //Update existing user if account is not locked
                    member = Membership.GetUser(this.mUserName);
                    if(member.IsLockedOut) {
                        Master.ShowMessageBox(this.mUserName + " account must be unlocked before updating.");
                        return;
                    }
                    //Membership
                    member.Email = this.txtEmail.Text;
                    //member.Comment = this.txtComments.Text;
                    Membership.UpdateUser(member);

                    //Profile
                    ProfileCommon profileCommon = new ProfileCommon();
                    ProfileCommon profile = profileCommon.GetProfile(this.mUserName);
                    profile.UserFullName = this.txtFullName.Text;
                    profile.Save();
 
                    System.Text.StringBuilder script = new System.Text.StringBuilder();
                    script.Append("<script language=javascript>");
                    script.Append("\talert('Your profile has been updated.');");
                    script.Append("</script>");
                    Page.ClientScript.RegisterStartupScript(typeof(Page),"ProfileUpdated",script.ToString());
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
}
