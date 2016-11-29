using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR.Permits;

public partial class _RegisterVehicle:System.Web.UI.Page {
    //Members
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                OnTypeChanged(null, EventArgs.Empty);
                this.lblPrefix.Text = "R";
                this.dduStates.SelectedValue = "NJ";
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null, EventArgs.Empty); }
    }
    protected void OnTypeChanged(object sender, EventArgs e) {
        //Vehicle owner type changed
        try {
            switch(this.dduType.SelectedValue) {
                case "Employee": 
                    this.lblPrefix.Text = "R";
                    this.lblPrefix.CssClass = this.txtPermitNumber.CssClass = "permitprefixr";
                    break;
                case "Driver": 
                case "Vendor": 
                case "Other": 
                    this.lblPrefix.Text = "B";
                    this.lblPrefix.CssClass = this.txtPermitNumber.CssClass = "permitprefixb";
                    break;
            }
            OnValidatePermitNumber(null, EventArgs.Empty);
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnValidatePermitNumber(object sender, EventArgs e) {
        //Validate the entered permit number
        try {
            Permit permit = new PermitGateway().ValidatePermitNumber(this.lblPrefix.Text + this.txtPermitNumber.Text);
            if(permit != null) {
                Master.ShowMessageBox("Permit# " + this.lblPrefix.Text + this.txtPermitNumber.Text + " is in use; please enter a new permit number.");
                this.txtPermitNumber.Text = "";
                this.txtPermitNumber.Focus();
            }
            else
                this.dduStates.Focus();
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
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
                    Permit permit = new Permit();
                    permit.ID = 0;
                    permit.Number = this.lblPrefix.Text + this.txtPermitNumber.Text;
                    permit.Activated = DateTime.Now;
                    permit.ActivatedBy = User.Identity.Name;
                    permit.Updated = DateTime.Now;
                    permit.UpdatedBy = User.Identity.Name;

                    permit.Vehicle = new Vehicle();
                    permit.Vehicle.IssueState = this.dduStates.SelectedValue;
                    permit.Vehicle.PlateNumber = this.txtPlate.Text.Trim();
                    permit.Vehicle.Year = this.txtYear.Text.Trim();
                    permit.Vehicle.Make = this.txtMake.Text.Trim();
                    permit.Vehicle.Model = this.txtModel.Text.Trim();
                    permit.Vehicle.Color = this.txtColor.Text.Trim();
                    permit.Vehicle.ContactFirstName = this.txtFirstName.Text.Trim();
                    permit.Vehicle.ContactMiddleName = this.txtMiddle.Text.Trim();
                    permit.Vehicle.ContactLastName = this.txtLastName.Text.Trim();
                    permit.Vehicle.ContactPhoneNumber = this.txtPhone.Text;
                    permit.Vehicle.Updated = DateTime.Now;
                    permit.Vehicle.UpdatedBy = User.Identity.Name;
                    permit.Vehicle.BadgeNumber = this.txtBadgeNumber.Text.Trim().Length > 0 ? int.Parse(this.txtBadgeNumber.Text) : 0;

                    int id = new PermitGateway().RegisterPermit(permit);
                    Master.ShowMessageBox("New permit# " + permit.Number + " has been registered.");
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
