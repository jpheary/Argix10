using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Assign : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //
        try {
            if (!Page.IsPostBack) {
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Assign":
                    if (this.cboSortType.SelectedValue.Trim().Length > 0 && this.cboStation.SelectedValue.Trim().Length > 0) {
                        Argix.Freight.Workstation station = new Argix.Freight.Workstation();
                        station.WorkStationID = this.cboStation.SelectedValue;
                        Argix.Freight.InboundShipment shipment = new Argix.Freight.InboundShipment();
                        shipment.TerminalID = int.Parse(Request.QueryString["terminalID"]);
                        shipment.FreightID = Request.QueryString["freightID"];
                        int sortTypeID = int.Parse(this.cboSortType.SelectedValue);
                        bool added = new Argix.Freight.TsortGateway().CreateStationAssignment(station,shipment,sortTypeID);
                        if (added) Response.Redirect("~/Stations.aspx");
                    }
                    break;
                case "Cancel":
                    Response.Redirect("~/Default.aspx");
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnAssign.Enabled = this.cboSortType.SelectedValue != null && this.cboStation.SelectedValue != null;
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
