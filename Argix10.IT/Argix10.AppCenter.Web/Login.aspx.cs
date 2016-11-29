using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Login : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //
        if (!Page.IsPostBack) {
            this.LoginUser.Focus();
        }
    }
    protected void OnLoggedIn(object sender,EventArgs e) {
        //
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
