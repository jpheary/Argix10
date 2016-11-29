using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
    public event EventHandler TerminalChanged = null;

    //Interface
    protected void Page_Init(object sender, EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
                if (Session["terminals"] == null) {
                    string terminalID = Request.QueryString["location"];
                    if (terminalID == null || terminalID.Trim().Length == 0) terminalID = "0";
                    Session["terminalcode"] = terminalID;
                    TerminalDataset terminals = new FreightGateway().GetTerminals(int.Parse(terminalID));
                    Session["terminals"] = terminals;
                }
                this.cboTerminal.DataSource = (TerminalDataset)Session["terminals"];
                this.cboTerminal.DataBind();
            }
        }
        catch (Exception ex) { ReportError(ex,3); }
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
                this.cboTerminal.SelectedValue = Session["terminalcode"].ToString();
                OnTerminalChanged(null,EventArgs.Empty);
            }
        }
        catch (Exception ex) { ReportError(ex,3); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for change in selected terminal
        try {
            Session["terminalcode"] = this.cboTerminal.SelectedItem.Value;
            if (this.TerminalChanged != null) this.TerminalChanged(sender,e);
        }
        catch (Exception ex) { ReportError(ex,3); }
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for toolbar button clicked
        try {
            switch (e.CommandName) {
                case "Refresh":
                    if (this.TerminalChanged != null) this.TerminalChanged(sender,e);
                    break;
            }
        }
        catch (Exception ex) { ReportError(ex,3); }
    }

    public string TerminalCode { get { return this.cboTerminal.SelectedItem.Value; } }
    public void ReportError(Exception ex,int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;

            string username = HttpContext.Current.User != null ? HttpContext.Current.User.Identity.Name : "";
            new Argix.Freight.FreightGateway().WriteLogEntry((Argix.Freight.LogLevel)logLevel,username,ex);
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
