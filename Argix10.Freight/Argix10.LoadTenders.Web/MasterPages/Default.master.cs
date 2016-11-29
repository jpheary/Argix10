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
    private const string CLIENT_LIST = "~/App_Data/ClientDataset.xml";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
            }
        }
        catch (Exception ex) { ReportError(ex,3); }
    }

    /// <summary>Retuen a list of clients that provide load tenders.</summary>
    public ClientDataset GetClients() {
        //Return a list of load tender clients
        ClientDataset clients = null;
        try {
            clients = new ClientDataset();
            clients.ReadXml(Request.MapPath(CLIENT_LIST));
            clients.ClientTable.AcceptChanges();
        }
        catch(Exception ex) { throw new ApplicationException(ex.Message); }
        return clients;
    }

    public void ReportError(Exception ex, int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
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
