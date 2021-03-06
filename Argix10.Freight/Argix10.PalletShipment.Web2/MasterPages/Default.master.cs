using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Argix.Freight;

public partial class DefaultMaster : System.Web.UI.MasterPage {
    //Members
    public event EventHandler CurrentClientChanged = null;

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
                //Set main navigation for Guest or Client/SalesRep
                this.lnkQuote.Visible = this.lnkTrack.Visible = true;
                this.lnkEnroll.Visible = this.User == null;
                this.lnkShipments.Visible = this.lnkManage.Visible = this.User != null;
                this.lnkAdmin.Visible = this.User != null && new MembershipServices().IsAdmin;

                //Set client selector for Admin/SalesRep
                if (this.User != null) {
                    if (new MembershipServices().IsAdmin) {
                        //Load list of all clients for admin
                        this.cboClients.DataSource = this.Clients;
                        this.cboClients.DataBind();
                        this.cboClients.SelectedValue = this.CurrentClient.ID.ToString();
                        this.lblSalesRep.Text = "Administrator";

                        this._lblClient.Visible = this.cboClients.Visible = true;
                        this.lblSalesRep.Visible = true;
                    }
                    else if (new MembershipServices().IsSalesRep) {
                        //Load list of clients belonging to sales rep
                        this.cboClients.DataSource = this.Clients;
                        this.cboClients.DataBind();
                        this.cboClients.SelectedValue = this.CurrentClient.ID.ToString();
                        this.lblSalesRep.Text = this.Client.Name;

                        this._lblClient.Visible = this.cboClients.Visible = this.lnkEnrollClient.Visible = true;
                        this._lblSalesRep.Visible = this.lblSalesRep.Visible = true;
                    }
                    else if (new MembershipServices().IsClient) {
                        this.lblClient.Text = this.Client.Name;
                        if (this.Client.SalesRepClientNumber.Length > 0) {
                            //Client with sales rep
                            this.lblSalesRep.Text = this.SalesRep.Name;
                            this._lblSalesRep.Visible = this.lblSalesRep.Visible = true;
                        }
                        else {
                            //Client with out sales rep
                            this._lblSalesRep.Visible = this.lnkGetSalesRep.Visible = true;
                        }
                        this._lblClient.Visible = this.lblClient.Visible = true;
                    }
                }
                else {
                    //Hide all for Guest
                    this._lblClient.Visible = this.lblClient.Visible = this.cboClients.Visible = this.lnkEnrollClient.Visible = false;
                    this._lblSalesRep.Visible = this.lblSalesRep.Visible = this.lnkGetSalesRep.Visible = false;
                }
                this.tblToolbar.Visible = this.User != null;
            }
        }
        catch (Exception ex) { ReportError(ex,3); }
    }
    public void OnCurrentClientChanged(object sender,EventArgs e) {
        //Event handler for change in sleected client
        try {
            LTLClient2 client = new FreightGateway().ReadLTLClient(int.Parse(this.cboClients.SelectedValue));
            Session["currentclient"] = client;
            if (this.CurrentClientChanged != null) this.CurrentClientChanged(sender,e);
        }
        catch (Exception ex) { ReportError(ex,3); }
    }
    protected void OnLogout(object sender,EventArgs e) {
        Page.Session.Clear();
    }

    public MembershipUser User { get { return Session["user"] != null ? (MembershipUser)Session["user"] : null; } }
    public LTLClient2 Client { get { return Session["client"] != null ? (LTLClient2)Session["client"] : null; } }
    public LTLClient2 SalesRep { get { return Session["salesrep"] != null ? (LTLClient2)Session["salesrep"] : null; } }
    public LTLClientDataset Clients { get { return Session["clients"] != null ? (LTLClientDataset)Session["clients"] : null; } }
    public LTLClient2 CurrentClient { get { return Session["currentclient"] != null ? (LTLClient2)Session["currentclient"] : null; } }
    public bool CurrentClientApproved { get { return Session["currentclient"] != null ? ((LTLClient2)Session["currentclient"]).ApprovalDate.CompareTo(DateTime.MinValue) > 0 : false; } }

    public bool ClientsEnabled { get { return this.cboClients.Enabled; } set { this.cboClients.Enabled = value; } }
    public void ReportError(Exception ex,int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;

            string username = Membership.GetUser() != null ? Membership.GetUser().UserName : "guest";
            new Argix.Freight.FreightGateway().WriteLogEntry((Argix.Freight.LogLevel)logLevel,username,ex);
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
