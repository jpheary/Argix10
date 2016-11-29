using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class Login : System.Web.UI.Page {
    //Members
    private const string USER_LOGIN_FAILED = "Login attempt failed. Your user id or password did not match.";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if (!Page.IsPostBack) {
            this.txtUserID.Focus();
        }
    }
    protected void OnOnCommand(object sender,CommandEventArgs e) {
        //Event handler for refresh button clicked
        try {
            switch (e.CommandName) {
                case "Login":
                    string username = this.txtUserID.Text.Trim();
                    string password = this.txtPassword.Text;
                    if (FormsAuthentication.Authenticate(username,password)) {
                        FormsAuthentication.SetAuthCookie(username,true);
                        FormsAuthentication.RedirectFromLoginPage(username,true);
                    }
                    else {
                        rfvUserID.IsValid = false;
                        rfvUserID.ErrorMessage = USER_LOGIN_FAILED;
                    }
                    break;
            }
        }
        catch (Exception ex) { rfvUserID.IsValid = false; rfvUserID.ErrorMessage = ex.Message; }
    }
}
