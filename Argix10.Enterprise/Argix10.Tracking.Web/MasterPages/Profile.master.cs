using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

public partial class _ProfileMaster : System.Web.UI.MasterPage {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        try {
            if (!Page.IsPostBack) {
                this.lnkGuests.Visible = this.lnkUsers.Visible = this.lnkMembers.Visible = new MembershipServices().IsAdmin;
            }
        }
        catch(Exception ex) { ReportError(ex,3); }
    }
    public void ReportError(Exception ex,int logLevel) { Master.ReportError(ex,logLevel); }
    public void ShowMessageBox(string message) { Master.ShowMessageBox(message); }
}
