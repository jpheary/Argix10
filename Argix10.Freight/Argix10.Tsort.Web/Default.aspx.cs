using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Default:System.Web.UI.Page {
    //Members
    private string mFreightType = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //
                this.cboTerminal.DataBind();
                int index = Convert.ToInt32(Session["TerminalIndex"]);
                if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = index > 0 ? index : 0;
                OnTerminalChanged(null,EventArgs.Empty);

                //
                this.mFreightType = "Regular";
                ViewState.Add("FreightType","Regular");

                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "regular";
                OnChangeView(null,new CommandEventArgs(view,null));
            }
            else {
                this.mFreightType = ViewState["FreightType"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.grdShipments.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.grdShipments.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "regular":
                    this.mFreightType = "Regular";
                    ViewState["FreightType"] = "Regular";
                    this.odsShipments.SelectParameters["freightType"].DefaultValue = "Regular";
                    this.grdShipments.DataBind();
                    this.liRegular.Style["border-bottom-style"] = "none";
                    this.liReturns.Style["border-bottom-style"] = "solid";
                    break;
                case "returns":
                    this.mFreightType = "Returns";
                    ViewState["FreightType"] = "Returns";
                    this.odsShipments.SelectParameters["freightType"].DefaultValue = "Returns";
                    this.grdShipments.DataBind();
                    this.liRegular.Style["border-bottom-style"] = "solid";
                    this.liReturns.Style["border-bottom-style"] = "none";
                    break;
            }
            this.liBlank1.Style["border-top-style"] = this.liBlank1.Style["border-right-style"] = this.liBlank1.Style["border-left-style"] = "none";
            this.liBlank2.Style["border-top-style"] = this.liBlank2.Style["border-right-style"] = this.liBlank2.Style["border-left-style"] = "none";
            this.liBlank3.Style["border-top-style"] = this.liBlank3.Style["border-right-style"] = this.liBlank3.Style["border-left-style"] = "none";
            this.liBlank4.Style["border-top-style"] = this.liBlank4.Style["border-right-style"] = this.liBlank4.Style["border-left-style"] = "none";
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Assign":
                    if (this.grdShipments.SelectedRow != null) {
                        string terminalID = this.cboTerminal.SelectedValue;
                        string freightID = this.grdShipments.SelectedDataKey.Value.ToString();
                        Response.Redirect("~/Assign.aspx?terminalID=" + terminalID + "&freightID=" + freightID);
                    }
                    else
                        Master.ShowMessageBox("Please select a shipment for station assignment.");
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
