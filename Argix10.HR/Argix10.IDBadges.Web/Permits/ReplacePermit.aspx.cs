﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR.Permits;

public partial class _ReplacePermit:System.Web.UI.Page {
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
                this.lblPrefix.Text = permit.Number.Substring(0, 1);
                this.lblPermitNumber.Text = permit.Number;
                this.lblPrefix.CssClass = this.txtNewPermitNumber.CssClass = permit.Number.Substring(0, 1).ToLower() == "r" ? "permitprefixr" : "permitprefixb";

                this.lblPlate.Text = permit.Vehicle.IssueState + " " + permit.Vehicle.PlateNumber;
                this.lblMake.Text = permit.Vehicle.Year + " " + permit.Vehicle.Make + " " + permit.Vehicle.Model + " (" + permit.Vehicle.Color + ")";
                this.lblName.Text = permit.Vehicle.ContactFirstName + " " + permit.Vehicle.ContactMiddleName + " " + permit.Vehicle.ContactLastName;
                this.lblPhone.Text = permit.Vehicle.ContactPhoneNumber;
                this.lblBadge.Text = permit.Vehicle.BadgeNumber.ToString();
            }
            else {
                this.mID = int.Parse(ViewState["ID"].ToString());
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null, EventArgs.Empty); }
    }
    protected void OnValidatePermitNumber(object sender, EventArgs e) {
        //Validate the entered permit number
        try {
            Permit permit = new PermitGateway().ValidatePermitNumber(this.lblPrefix.Text + this.txtNewPermitNumber.Text);
            if(permit != null) {
                Master.ShowMessageBox("Permit# " + this.lblPrefix.Text + this.txtNewPermitNumber.Text + " is in use; please enter a new permit number.");
                this.txtNewPermitNumber.Text = "";
                this.txtNewPermitNumber.Focus();
            }
            else
                this.txtReason.Focus();
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
                    permit.Inactivated = DateTime.Now;
                    permit.InactivatedBy = User.Identity.Name;
                    permit.InactivatedReason = this.txtReason.Text;
                    permit.Updated = DateTime.Now;
                    permit.UpdatedBy = User.Identity.Name;
                    int id = new PermitGateway().ReplacePermit(permit, this.lblPrefix.Text + this.txtNewPermitNumber.Text);
                    Master.ShowMessageBox("Permit# " + permit.Number + " has been replaced by permit# " + this.lblPrefix.Text + this.txtNewPermitNumber.Text + ".");
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
