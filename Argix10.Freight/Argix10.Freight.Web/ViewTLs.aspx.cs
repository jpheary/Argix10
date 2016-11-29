using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewTLs:System.Web.UI.Page {
    //Members
    private string mTLNumber = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                this.mTLNumber = Request.QueryString["tl"] != null ? Request.QueryString["tl"] : "";
                ViewState.Add("TLNumber",this.mTLNumber);
                this.Master.TLButtonFontColor = System.Drawing.Color.White;

                this.cboTerminal.DataBind();
                int index = Convert.ToInt32(Session["TerminalIndex"]);
                if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = index > 0 ? index : 0;
                OnTerminalChanged(null,EventArgs.Empty);

                OnChangeView(null,new CommandEventArgs("TLs",null));
            }
            else {
                this.mTLNumber = ViewState["TLNumber"].ToString();
            }
            if (this.mTLNumber.Trim().Length > 0) this.mvPage.SetActiveView(this.vwTL);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.lsvTLs.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.lsvTLs.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "TLs":
                    this.liTLs.Style["border-bottom-style"] = "none";
                    this.liAgents.Style["border-bottom-style"] = "solid";
                    this.lsvTLs.DataBind();
                    this.mvPage.SetActiveView(this.vwTLs);
                    this.mvTLs.SetActiveView(this.vwTLView);
                    break;
                case "TLView":
                    this.liTLs.Style["border-bottom-style"] = "none";
                    this.liAgents.Style["border-bottom-style"] = "solid";
                    this.lsvTLs.DataBind();
                    this.mvTLs.SetActiveView(this.vwTLView);
                    break;
                case "AgentView":
                    this.liTLs.Style["border-bottom-style"] = "solid";
                    this.liAgents.Style["border-bottom-style"] = "none";
                    this.lsvAgents.DataBind();
                    this.mvTLs.SetActiveView(this.vwAgentView);
                    break;
                case "TL":
                    this.lblTL.Text = "TL# " + e.CommandArgument.ToString();
                    this.odsTL.SelectParameters["tlNumber"].DefaultValue = e.CommandArgument.ToString();
                    this.lsvTL.DataBind();
                    this.mvPage.SetActiveView(this.vwTL);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }

    public string GetTL(object tlNumber,object closeNumber) {
        return tlNumber.ToString() + "-" + closeNumber.ToString();
    }
    public string GetAgentInfo(object agentNumber,object agentName) {
        return agentNumber.ToString() + "-" + agentName.ToString().Trim().PadRight(25, ' ').Substring(0,25);
    }
    public string GetShipToInfo(object shipToLocationID,object shipToLocationName) {
        return shipToLocationID.ToString() + "-" + shipToLocationName.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
    public string GetClientInfo(object clientNumber,object clientName) {
        return clientNumber.ToString() + "-" + clientName.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
    public string GetFreightCounts(object cartons,object pallets) {
        //xxxx CTNS/xx PLTS
        return ((cartons!=DBNull.Value?cartons.ToString():"0") + " ctns/" + (pallets!=DBNull.Value?pallets.ToString():"0") + " plts");
    }
    public string GetItemWeight(object weight) {
        return weight.ToString() + " lbs";
    }
    public string GetItemSpecs(object weight,object cube) {
        return weight.ToString() + " lbs/" + cube.ToString() + " cu in";
    }
    public string GetDate(object date) {
        return DateTime.Parse(date.ToString()).ToShortDateString();
    }
}
