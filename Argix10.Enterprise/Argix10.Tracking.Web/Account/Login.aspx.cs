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

public partial class _Login : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.LoginUser.FindControl("UserName").Focus();
        }
    }
    protected void OnLoginError(object sender, EventArgs e) {
        //
        try {
            Master.ShowMessageBox("Your login attempt was unsuccessful; please try again.");
            this.LoginUser.FindControl("Password").Focus();
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnLoggedIn(object sender, EventArgs e) {
        //
        try {
        }
        catch(Exception ex) { Master.ReportError(ex, 4); }
    }
}
