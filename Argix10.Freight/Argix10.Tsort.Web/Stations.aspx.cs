using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Stations:System.Web.UI.Page {
    //Members

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

                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "stations";
                OnChangeView(null,new CommandEventArgs(view,null));
            }
            else {
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.grdStations.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.grdStations.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "stations":
                    this.grdStations.DataBind();
                    this.liStations.Style["border-bottom-style"] = "none";
                    break;
            }
            this.liBlank1.Style["border-top-style"] = this.liBlank1.Style["border-right-style"] = this.liBlank1.Style["border-left-style"] = "none";
            this.liBlank2.Style["border-top-style"] = this.liBlank2.Style["border-right-style"] = this.liBlank2.Style["border-left-style"] = "none";
            this.liBlank3.Style["border-top-style"] = this.liBlank3.Style["border-right-style"] = this.liBlank3.Style["border-left-style"] = "none";
            this.liBlank4.Style["border-top-style"] = this.liBlank4.Style["border-right-style"] = this.liBlank4.Style["border-left-style"] = "none";
            this.liBlank5.Style["border-top-style"] = this.liBlank5.Style["border-right-style"] = this.liBlank5.Style["border-left-style"] = "none";
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Unassign":
                    if (this.grdStations.SelectedRow != null) {
                        DataKey dataKey = (DataKey)this.grdStations.DataKeys[this.grdStations.SelectedRow.RowIndex];

                        Argix.Freight.StationAssignment assignment = new Argix.Freight.StationAssignment();
                        assignment.SortStation = new Argix.Freight.Workstation();
                        assignment.SortStation.WorkStationID = dataKey["WorkStationID"].ToString();
                        assignment.InboundFreight = new Argix.Freight.InboundShipment();
                        assignment.InboundFreight.TerminalID = int.Parse(this.cboTerminal.SelectedValue);
                        assignment.InboundFreight.FreightID = dataKey["FreightID"].ToString();
                        assignment.SortTypeID = int.Parse(dataKey["SortTypeID"].ToString());
                        bool removed = new Argix.Freight.TsortGateway().DeleteStationAssignment(assignment);
                        this.grdStations.DataBind();
                    }
                    else
                        Master.ShowMessageBox("Please select an assignment.");
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
