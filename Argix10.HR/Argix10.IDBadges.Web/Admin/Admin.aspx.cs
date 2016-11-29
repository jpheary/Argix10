using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR;

public partial class Admin : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                this.txtName.Text = "";
                OnBadgeTypeChanged(null, EventArgs.Empty);
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
        finally { OnValidateForm(null, EventArgs.Empty); }
    }
    protected void OnBadgeTypeChanged(object sender, EventArgs e) {
        //Event handler for changes in parameter data
        try {
            switch(this.cboBadgeType.SelectedValue) {
                case "Employees":
                    this.lstDepartments.DataSourceID = "odsEmployeeDepartments";
                    break;
                case "Vendors":
                    this.lstDepartments.DataSourceID = "odsVendorDepartments";
                    break;
            }
            this.lstDepartments.DataBind();
            this.txtName.Text = "";
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
        finally { OnValidateForm(null, EventArgs.Empty); }
    }
    protected void OnValidateForm(object sender, EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnOk.Enabled = this.cboBadgeType.SelectedValue.Length > 0 && this.txtName.Text.Trim().Length > 0;
            this.btnCancel.Enabled = true;
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnCommand(object sender, CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "Ok":
                    bool res = false;
                    switch(this.cboBadgeType.SelectedValue) {
                        case "Employees":
                            res = new BadgeGateway().AddEmployeeDepartment(this.txtName.Text);
                            break;
                        case "Vendors":
                            res = new BadgeGateway().AddVendorDepartment(this.txtName.Text);
                            break;
                    }
                    if(res) {
                        Master.ShowMessageBox("New " + this.cboBadgeType.SelectedValue + " department has been created.");
                        OnBadgeTypeChanged(null, EventArgs.Empty);
                        this.btnOk.Enabled = false;
                        this.btnCancel.Text = "Close";
                    }
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ClearCursor", "document.body.style.cursor='default';", true);
                    break;
                case "Cancel":
                    Response.Redirect("~/Badges/Badges.aspx?type=Employees", false);
                    break;
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 4); }
    }
}