//	File:	RecoverPassword.aspx.cs
//	Author:	J. Heary
//	Date:	03/09/07
//	Desc:	Allows a user to recover or reset a lost password and receive it in e-mail.
//          This page uses a PasswordRecovery control that allows user passwords to be 
//          retrieved based on the userID that was used when the account was created. The 
//          control sends an e-mail message containing a password to the user. ASP.NET 
//          membership can be configured to store passwords using non-reversible encryption. 
//          In this case, the control sends a new password instead of the original.
//          The control has three states, or views: 
//          - UserName view asks the user for his or her registered user name
//          - Question view requires the user to provide the answer to a stored question to reset the password
//          - Success view tells the user whether the password recovery or reset was successful
//          NOTES:
//          1. Users can recover passwords only when the membership provider defined in the 
//             MembershipProvider property supports clear text or encrypted passwords. Because 
//             hashed passwords cannot be recovered, users at sites that use hashed passwords 
//             can only reset their passwords.
//          2. The control can be used when a membership user has not been approved 
//             (MembershipUser.IsApproved = false), but it cannot be used when a
//             membership user has been locked out (MembershipUser.IsLockedOut = true).
//          3. The control displays the Question view only when the membership provider 
//             defined in the MembershipProvider property supports password question/answer.
//          4. The control creates a validation group for all required field validators 
//             in the control so that other input controls on the page are not affected by 
//             validating the control. By default, the ID property of the control is used 
//             as the name of the validation group. If you want the control to participate 
//             in another validation group, you must template the control.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _RecoverPassword :System.Web.UI.Page {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.RecoverUserPassword.MailDefinition.From = new Argix.Freight.NotifyService().AdminEmailAddress;
            this.RecoverUserPassword.FindControl("UserNameContainerID").FindControl("UserName").Focus();
        }
    }
    protected void OnVerifyingUser(object sender,LoginCancelEventArgs e) {
        //Event handler for event that occurs before the user name is validated by the membership provider
        e.Cancel = false;
    }
    protected void OnUserLookupError(object sender,EventArgs e) {
        //Event handler for event that occurs when the membership provider cannot find the user name entered by the user
        try {
            Master.ShowMessageBox("We were unable to access your information; please try again.");
            this.RecoverUserPassword.FindControl("UserNameContainerID").FindControl("UserName").Focus();
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    protected void OnSendingMail(object sender,MailMessageEventArgs e) {
        //Event handler for event that occurs before the user is sent a password in e-mail
        try {
            //Retrieve the password
            MembershipUser user = Membership.GetUser(this.RecoverUserPassword.UserName);
            string password = Membership.Provider.GetPassword(user.UserName,"");

            //Send an email
            new Argix.Freight.NotifyService().SendPasswordResetMessage(user.UserName,user.Email,password);
            e.Cancel = true;
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    protected void OnSendMailError(object sender,SendMailErrorEventArgs e) {
        //Event handler for event that occurs when the SMTP Mail system throws an error while attempting to send an e-mail message
        Master.ReportError(e.Exception, 4);
        e.Handled = true;
    }
}
