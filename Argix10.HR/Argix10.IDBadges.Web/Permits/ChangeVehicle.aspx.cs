using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR.Permits;

public partial class _ChangeVehicle:System.Web.UI.Page {
    //Members
    private int mID = 0;
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                this.mID = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;
                ViewState.Add("ID",this.mID);

                Permit permit = new PermitGateway().ReadPermit(this.mID);
                this.lblPermitNumber.Text = permit.Number;
                this.dduStates.SelectedValue = permit.Vehicle.IssueState;
                this.txtPlate.Text = permit.Vehicle.PlateNumber;
                this.txtYear.Text = permit.Vehicle.Year;
                this.txtMake.Text = permit.Vehicle.Make;
                this.txtModel.Text = permit.Vehicle.Model;
                this.txtColor.Text = permit.Vehicle.Color;
                this.txtFirstName.Text = permit.Vehicle.ContactFirstName;
                this.txtMiddle.Text = permit.Vehicle.ContactMiddleName;
                this.txtLastName.Text = permit.Vehicle.ContactLastName;
                this.txtPhone.Text = permit.Vehicle.ContactPhoneNumber;
                this.txtBadgeNumber.Text = permit.Vehicle.BadgeNumber > 0 ? permit.Vehicle.BadgeNumber.ToString() : "";
            }
            else {
                this.mID = int.Parse(ViewState["ID"].ToString());
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null, EventArgs.Empty); }
    }
    protected void OnValidatePlateNumber(object sender, EventArgs e) {
        //Validate the entered license plate number
        try {
            Permit permit = new PermitGateway().ValidateVehicle(this.dduStates.SelectedValue, this.txtPlate.Text);
            if(permit != null) {
                Master.ShowMessageBox("Vehicle license plate " + this.dduStates.SelectedValue + " " + this.txtPlate.Text + " is in use on permit# " + permit.Number + "; please enter another plate.");
                this.txtPlate.Text = "";
                this.txtPlate.Focus();
            }
            else
                this.txtYear.Focus();
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnValidateForm(object sender, EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnOk.Enabled = true;
            this.btnCancel.Enabled = true;
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnCommand(object sender, CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "Ok":
                    Permit permit = new PermitGateway().ReadPermit(this.mID);
                    Vehicle vehicle = permit.Vehicle;
                    vehicle.IssueState = this.dduStates.SelectedValue;
                    vehicle.PlateNumber = this.txtPlate.Text.Trim();
                    vehicle.Year = this.txtYear.Text.Trim();
                    vehicle.Make = this.txtMake.Text.Trim();
                    vehicle.Model = this.txtModel.Text.Trim();
                    vehicle.Color = this.txtColor.Text.Trim();
                    vehicle.ContactFirstName = this.txtFirstName.Text.Trim();
                    vehicle.ContactMiddleName = this.txtMiddle.Text.Trim();
                    vehicle.ContactLastName = this.txtLastName.Text.Trim();
                    vehicle.ContactPhoneNumber = this.txtPhone.Text;
                    vehicle.Updated = DateTime.Now;
                    vehicle.UpdatedBy = User.Identity.Name;
                    vehicle.BadgeNumber = this.txtBadgeNumber.Text.Trim().Length > 0 ? int.Parse(this.txtBadgeNumber.Text) : 0;
                    bool changed = new PermitGateway().ChangeVehicle(vehicle);
                    Master.ShowMessageBox("Vehicle has been changed.");
                    this.btnOk.Enabled = false;
                    this.btnCancel.Text = "Close";
                    break;
                case "Cancel":
                    Response.Redirect("~/Permits/Permits.aspx?tab=Permits", false);
                    break;
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 4); }
    }
}
