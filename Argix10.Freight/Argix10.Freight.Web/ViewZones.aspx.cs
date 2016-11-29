using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewZones:System.Web.UI.Page {
    //Members
    private string mZone = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                this.mZone = Request.QueryString["zone"] != null ? Request.QueryString["zone"] : "";
                ViewState.Add("Zone",this.mZone);
                this.Master.ZoneButtonFontColor = System.Drawing.Color.White;

                this.cboTerminal.DataBind();
                int index = Convert.ToInt32(Session["TerminalIndex"]);
                if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = index > 0 ? index : 0;
                OnTerminalChanged(null,EventArgs.Empty);

                OnChangeView(null,new CommandEventArgs("Zones",null));
            }
            else {
                this.mZone = ViewState["Zone"].ToString();
            }
            if (this.mZone.Trim().Length > 0) this.mvPage.SetActiveView(this.vwZone);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.lsvZones.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.lsvZones.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Zones":
                    this.lsvZones.DataBind();
                    this.mvPage.SetActiveView(this.vwZones);
                    break;
                case "Zone":
                    this.odsZone.SelectParameters["zoneCode"].DefaultValue = e.CommandArgument.ToString();
                    this.dvZone.DataBind();
                    this.mvPage.SetActiveView(this.vwZone);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }

    public string GetTL(object tlNumber,object closeNumber) {
        return tlNumber.ToString() + "-" + closeNumber.ToString();
    }
    public string GetClientInfo(object clientNumber,object clientName) {
        return clientNumber.ToString() + "-" + clientName.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
}
