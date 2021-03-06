using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Register :System.Web.UI.Page {
    //Members
    private const string PASSWORD_MIN_LENGHT = "Password should be minimum 6 characters long.";
    private const string EMAIL_NOT_UNIQUE = "Your email address already exists.";
    private const string USERID_NOT_UNIQUE = "This login UserID already exists.";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        this.txtFullName.Focus();
    }
    protected void OnRegisterImg(object sender,ImageClickEventArgs e) { OnRegister(null,null); }
    protected void OnRegister(object sender,EventArgs e) {
        //Validate control states
        if(!Page.IsValid) return;
        try {
            //Create a new user
            MembershipCreateStatus status = MembershipCreateStatus.Success;
            MembershipUser user = Membership.CreateUser(this.txtUserID.Text.Trim(),this.txtPassword.Text.Trim(),this.txtEmail.Text.Trim(),null,null,false,out status);
            switch (status) {
                case MembershipCreateStatus.Success:
                    //Add user to the guest role
                    Roles.AddUserToRole(this.txtUserID.Text.Trim(),MembershipServices.GUESTROLE);

                    //Create user profile and set properties value
                    ProfileCommon profile = Profile.GetProfile(this.txtUserID.Text.Trim());
                    profile.Company = this.txtCompany.Text.Trim();
                    profile.UserFullName = this.txtFullName.Text.Trim();
                    profile.WebServiceUser = false;
                    profile.Save();

                    try {
                        //Send notification to the administrator
                        EmailGateway svcs = new EmailGateway();
                        svcs.SendRegisterReceiptEmail(this.txtFullName.Text.Trim(),this.txtEmail.Text.Trim());

                        //Disply confirmation to user
                        string msg = "Thank you. Your registration request has been received. ";
                        msg += "You will receive email confirmation when your account becomes active. ";
                        msg += "If you dont receive confirmation in the next 2 business days then ";
                        msg += "contact customer support at extranet.support@argixlogistics.com or 800-274-4582.";
                        System.Text.StringBuilder script = new System.Text.StringBuilder();
                        script.Append("<script language='javascript'>");
                        script.Append("\talert('" + msg + "');");
                        script.Append("\twindow.navigate('Account/Login.aspx');");
                        script.Append("</script>");
                        Page.ClientScript.RegisterStartupScript(typeof(Page),"Registered",script.ToString());
                    }
                    catch (Exception ex) {
                        string msg = "Thank you. Your registration request has been received. ";
                        msg += "Unfortunately, an error has occurred while sending you a conformation email. ";
                        msg += "Please contact customer support at extranet.support@argixlogistics.com or 800-274-4582.";
                        Master.ReportError(new ApplicationException(msg, ex), 4); 
                    }
                    break;
                case MembershipCreateStatus.DuplicateEmail:
                    revEmail.IsValid = false;
                    revEmail.ErrorMessage = EMAIL_NOT_UNIQUE;
                    break;
                case MembershipCreateStatus.DuplicateUserName:
                    rfvUserID.IsValid = false;
                    rfvUserID.ErrorMessage = USERID_NOT_UNIQUE;
                    break;
                case MembershipCreateStatus.InvalidPassword:
                    rfvPassword.IsValid = false;
                    rfvPassword.ErrorMessage = PASSWORD_MIN_LENGHT;
                    break;
                case MembershipCreateStatus.UserRejected:
                    break;
            }
        }
        catch (Exception ex) {
            Master.ReportError(new ApplicationException(ex.Message), 3);
        }
    }
}
