using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ViewFreight:System.Web.UI.Page {
    //Members
    private string mFreightID = "", mFreightType="";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.mFreightID = Request.QueryString["freightID"] != null ? Request.QueryString["freightID"] : "";
                ViewState.Add("FreightID",this.mFreightID);
                this.Master.FreightButtonFontColor = System.Drawing.Color.White;
                this.mFreightType = "Regular";
                ViewState.Add("FreightType","Regular");
            
                this.cboTerminal.DataBind();
                int index = Convert.ToInt32(Session["TerminalIndex"]);
                if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = index > 0 ? index : 0;
                OnTerminalChanged(null, EventArgs.Empty);
            
                OnChangeView(null,new CommandEventArgs("Shipments",null));
            }
            else {
                this.mFreightID = ViewState["FreightID"].ToString();
                this.mFreightType = ViewState["FreightType"].ToString();
            }
            if(this.mFreightID.Trim().Length > 0) this.mvPage.SetActiveView(this.vwShipment);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.lsvShipments.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.lsvShipments.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "Shipments":
                    this.odsShipments.SelectParameters["freightType"].DefaultValue = this.mFreightType;
                    this.lsvShipments.DataBind();
                    this.liRegular.Style["border-bottom-style"] = this.mFreightType == "Regular" ? "none" : "solid";
                    this.liReturns.Style["border-bottom-style"] = this.mFreightType == "Returns" ? "none" : "solid";
                    this.mvPage.SetActiveView(this.vwShipments);
                    break;
                case "Regular":
                    this.mFreightType = "Regular";
                    ViewState["FreightType"] = "Regular";
                    this.odsShipments.SelectParameters["freightType"].DefaultValue = "Regular";
                    this.lsvShipments.DataBind();
                    this.liRegular.Style["border-bottom-style"] = "none";
                    this.liReturns.Style["border-bottom-style"] = "solid";
                    break;
                case "Returns":
                    this.mFreightType = "Returns";
                    ViewState["FreightType"] = "Returns";
                    this.odsShipments.SelectParameters["freightType"].DefaultValue = "Returns";
                    this.lsvShipments.DataBind();
                    this.liRegular.Style["border-bottom-style"] = "solid";
                    this.liReturns.Style["border-bottom-style"] = "none";
                    break;
                case "Shipment":
                    this.odsShipment.SelectParameters["freightID"].DefaultValue = e.CommandArgument.ToString();
                    this.dvShipment.DataBind();
                    this.odsAssignments.SelectParameters["freightID"].DefaultValue = e.CommandArgument.ToString();
                    this.lsvAssignments.DataBind();
                    this.odsSortTypes.SelectParameters["freightID"].DefaultValue = e.CommandArgument.ToString();
                    this.cboSortType.DataBind();
                    this.odsStations.SelectParameters["freightID"].DefaultValue = e.CommandArgument.ToString();
                    this.cboStation.DataBind();

                    this.liFreight.Style["border-bottom-style"] = "none";
                    this.liAssignments.Style["border-bottom-style"] = "solid";
                    this.mvShipment.SetActiveView(this.vwDetail);
                    this.mvPage.SetActiveView(this.vwShipment);
                    break;
                case "Freight":
                    this.liFreight.Style["border-bottom-style"] = "none";
                    this.liAssignments.Style["border-bottom-style"] = "solid";
                    this.mvShipment.SetActiveView(this.vwDetail);
                    break;
                case "Assignments":
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liAssignments.Style["border-bottom-style"] = this.btnAssignments.Style["border-right-style"] = "none";
                    this.mvShipment.SetActiveView(this.vwAssignments);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnAssignment(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Add":
                    if (this.cboSortType.SelectedValue.Trim().Length > 0 && this.cboStation.SelectedValue.Trim().Length > 0) {
                        Argix.Freight.Workstation station = new Argix.Freight.Workstation();
                        station.WorkStationID = this.cboStation.SelectedValue;
                        Argix.Freight.InboundShipment shipment = new Argix.Freight.InboundShipment();
                        shipment.TerminalID = int.Parse(this.cboTerminal.SelectedValue);
                        shipment.FreightID = this.odsStations.SelectParameters["freightID"].DefaultValue;
                        int sortTypeID = int.Parse(this.cboSortType.SelectedValue);
                        bool added = new Argix.Freight.TsortGateway().CreateStationAssignment(station,shipment,sortTypeID);
                        this.lsvAssignments.DataBind();
                    }
                    break;
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

    public string GetItemCount(object cartons,object pallets) {
        return cartons != DBNull.Value ? cartons.ToString() + " ctns" : pallets.ToString() + " plts";
    }
    public string GetClientInfo(object clientNumber,object clientName) {
        return clientNumber.ToString() + "-" + clientName.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
    public string GetShipperInfo(object shipperNumber,object shipperName) {
        return shipperNumber.ToString() + "-" + shipperName.ToString().Trim().PadRight(25,' ').Substring(0,25);
    }
    public string GetStationAssignment(object workstationID,object freightID,object sortTypeID) {
        return workstationID + "," + freightID + "," + sortTypeID;
    }
}
