using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class DefaultMaster : System.Web.UI.MasterPage {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
            }
            else {
            }
        }
        catch (Exception ex) { ReportError(ex); }
    }
    protected void OnLogout(object sender, EventArgs e) {
        Page.Session.Clear();
    }
    public void ReportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            string username = Membership.GetUser() != null ? Membership.GetUser().UserName : "guest";
            new Argix.Enterprise.EnterpriseGateway().WriteLogEntry(3,username,ex);
            ShowMessageBox(msg);
        }
        catch(Exception) { }
    }
    public void ShowMessageBox(string message) { 
        //
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.lblStatus,typeof(Label),"Error","alert('" + message + "');",true);
    }
}
