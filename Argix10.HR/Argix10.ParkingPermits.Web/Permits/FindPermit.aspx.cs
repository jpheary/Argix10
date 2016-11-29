using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR;

public partial class _FindPermit : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.mvSearch.ActiveViewIndex = 0;
            }
        }
        catch(Exception ex) { Master.ReportError(ex); }
    }
    protected void OnSearchByChanged(object sender, EventArgs e) {
        //Event handler for change in selected search type
        switch(this.dduSearchBy.Text) {
            case "Permit#":
                this.mvSearch.ActiveViewIndex = 0;              
                break;
            case "License Plate#":
                this.mvSearch.ActiveViewIndex = 1;
                this.dduStates.SelectedValue = "NJ";
                break;
            case "Vehicle Description":
                this.mvSearch.ActiveViewIndex = 2;
               break;
        }
        this.lsvPermits.DataSource = null;
        this.lsvPermits.DataBind();
    }
    protected void OnManageCommand(object sender, CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "Search":
                    DataSet ds = null;
                    this.lsvPermits.DataSource = null;
                    this.grdSearch.DataSource = null;
                    switch(this.dduSearchBy.Text) {
                        case "Permit#":
                            ds = new PermitGateway().FindPermitsByNumber(this.txtPermitNumber.Text);
                            break;
                        case "License Plate#":
                            ds = new PermitGateway().FindPermitsByPlate(this.dduStates.SelectedValue, this.txtPlate.Text);
                            break;
                        case "Vehicle Description":
                            ds = new PermitGateway().FindPermitsByVehicle(this.txtYear.Text, this.txtMake.Text, this.txtModel.Text, this.txtColor.Text);
                            break;
                    }
                    this.lsvPermits.DataSource = ds;
                    this.lsvPermits.DataBind();
                    this.grdSearch.DataSource = ds;
                    this.grdSearch.DataBind();
                    break;
            }
        }
        catch(Exception ex) { Master.ReportError(ex); }
    }

    protected string GetStatusDate(object activated, object inactivated) {
        //
        string dateInfo = "";
        try {
            DateTime _activated = (DateTime)activated;
            DateTime _inactivated = DateTime.TryParse(inactivated.ToString(), out _inactivated) ? (DateTime)inactivated : DateTime.MinValue;
            if(_inactivated.CompareTo(DateTime.MinValue) > 0)
                dateInfo = "Inactive";  //_inactivated.ToString("MM/dd/yyyy");
            else
                dateInfo = "Active";    // _activated.ToString("MM/dd/yyyy");
        }
        catch(Exception ex) { Master.ReportError(ex); }
        return dateInfo;
    }
    protected string GetVehiclePlate(object issueState, object plateNumber) {
        string vehcilePlate = "";
        try {
            vehcilePlate = issueState.ToString().Trim() + " " + plateNumber.ToString().Trim();
        }
        catch(Exception ex) { Master.ReportError(ex); }
        return vehcilePlate;
    }
    protected string GetStatusReason(object inactivated, object inactiveReason) {
        //
        string reason = "";
        try {
            DateTime _inactivated = DateTime.TryParse(inactivated.ToString(), out _inactivated) ? (DateTime)inactivated : DateTime.MinValue;
            if(_inactivated.CompareTo(DateTime.MinValue) > 0)
                reason = inactiveReason.ToString().Length > 17 ? inactiveReason.ToString().Substring(0, 17) + "..." : inactiveReason.ToString();
        }
        catch(Exception ex) { Master.ReportError(ex); }
        return reason;
    }
    protected string GetVehicle(object year, object make, object model, object color) {
        //
        string vehicle = "";
        try {
            vehicle = year + " " + make + " " + model + (color.ToString().Trim().Length > 0 ? " (" + color + ")" : "");
        }
        catch(Exception ex) { Master.ReportError(ex); }
        return vehicle;
    }
    protected string GetContact(object firstname, object middlename, object lastname, object phone) {
        //
        string contact = "";
        try {
            contact = firstname.ToString().Trim() + " " + middlename.ToString().Trim() + " " + lastname.ToString().Trim();  // +"     " + phone.ToString().Trim();
        }
        catch(Exception ex) { Master.ReportError(ex); }
        return contact;
    }
}
