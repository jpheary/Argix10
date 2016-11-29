using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewSorting:System.Web.UI.Page {
    //Members
    private string mFreightID = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.mFreightID = Request.QueryString["freightID"] != null ? Request.QueryString["freightID"] : "";
                ViewState.Add("FreightID",this.mFreightID);
                this.Master.SortingButtonFontColor = System.Drawing.Color.White;
            
                this.cboTerminal.DataBind();
                int index = Convert.ToInt32(Session["TerminalIndex"]);
                if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = index > 0 ? index : 0;
                OnTerminalChanged(null,EventArgs.Empty);
            }
            else {
                this.mFreightID = ViewState["FreightID"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.lsvAssignments.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.lsvAssignments.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnAssignment(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Unassign":
                    Argix.Freight.StationAssignment assignment = new Argix.Freight.StationAssignment();
                    assignment.SortStation = new Argix.Freight.Workstation();
                    assignment.SortStation.WorkStationID = e.CommandArgument.ToString().Split(new char[] { ',' })[0];
                    assignment.InboundFreight = new Argix.Freight.InboundShipment();
                    assignment.InboundFreight.TerminalID = int.Parse(this.cboTerminal.SelectedValue);
                    assignment.InboundFreight.FreightID = e.CommandArgument.ToString().Split(new char[] { ',' })[1];
                    assignment.SortTypeID = int.Parse(e.CommandArgument.ToString().Split(new char[] { ',' })[2]);
                    bool removed = new Argix.Freight.TsortGateway().DeleteStationAssignment(assignment);
                    this.lsvAssignments.DataBind();
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }

    public string GetClientInfo(object client) {
        return client.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
    public string GetShipperInfo(object shipper) {
        return shipper.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
    public string GetStationAssignment(object workstationID,object freightID,object sortTypeID) {
        return workstationID + "," + freightID + "," + sortTypeID;
    }
}
