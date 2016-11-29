using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPages_Default : System.Web.UI.MasterPage {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
                this.lnkSOA.Visible = Roles.IsUserInRole("Administrator");
            }
        }
        catch (Exception ex) { ReportError(ex,3); }
    }
    protected void OnLogout(object sender,EventArgs e) {
        Page.Session.Clear();
    }

    public void ReportError(Exception ex,int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;

            string username = Membership.GetUser() != null ? Membership.GetUser().UserName : "guest";
            //new Argix.Enterprise.EnterpriseGateway().WriteLogEntry(3,username,ex);
            //if (logLevel > 3) new Argix.Enterprise.SMTPGateway().SendITNotification(username,ex);
            ShowMessageBox(msg);
        }
        catch (Exception) { }
    }
    public void ShowMessageBox(string message) {
        //
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.lblMsg,typeof(Label),"Error","alert('" + message + "');",true);
    }
}
