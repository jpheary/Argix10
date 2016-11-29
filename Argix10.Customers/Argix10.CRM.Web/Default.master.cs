using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
 
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.lblUser.Text = HttpContext.Current.User.Identity.Name;
        }
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
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.lblTitle,typeof(Label),"Error","alert('" + message + "');",true);
    }
}
