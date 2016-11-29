using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.lnkAdmin.Visible = Roles.IsUserInRole("Administrator");
            }
        }
        catch(Exception ex) { ReportError(ex, 3); }
    }
    public void ReportError(Exception ex,int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;

            string username = HttpContext.Current.User != null ? HttpContext.Current.User.Identity.Name : "";
            new Argix.HR.BadgeGateway().WriteLogEntry(ex.Message, (Argix.HR.LogLevel)logLevel, username);
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
