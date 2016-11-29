using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Account_Login : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //
        if (!Page.IsPostBack) {
            //this.lnkRegister.NavigateUrl = "~/Account/Register.aspx";
            this.LoginUser.Focus();
        }
    }
}
