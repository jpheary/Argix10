using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_ChangePassword : System.Web.UI.Page {
    //
    protected void Page_Load(object sender, EventArgs e) {
        //
        try {
            this.ChangeUserPassword.FindControl("ChangePasswordContainerID").FindControl("CurrentPassword").Focus();
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnChangePasswordError(object sender, EventArgs e) {
        //
        try {
            Master.ShowMessageBox("Your change password attempt was unsuccessful; please try again.");
            this.ChangeUserPassword.FindControl("ChangePasswordContainerID").FindControl("CurrentPassword").Focus();
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
}
