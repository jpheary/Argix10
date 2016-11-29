using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SiteMaster : System.Web.UI.MasterPage {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {

    }
    public void ReportError(Exception ex) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if(ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            ShowMsgBox(msg);
        }
        catch(Exception) { }
    }
    public void ShowMsgBox(string message) {
        message = message.Replace("'", "").Replace("\n", " ").Replace("\r", " ");
        ScriptManager.RegisterStartupScript(this.lblMsg, typeof(Label), "Error", "alert('" + message + "');", true);
    }
}
