using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR;

public partial class _EmployeeBadge:System.Web.UI.Page {
    //Members
    private int mIDNumber = 0;
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                this.mIDNumber = Request.QueryString["id"] != null ? int.Parse(Request.QueryString["id"]) : 0;
                ViewState.Add("ID",this.mIDNumber);

                this.Title = this.mIDNumber == 0 ? "New" : "Update";
                if(this.mIDNumber > 0) {
                    BadgeGateway client = new BadgeGateway();
                    EmployeeBadge badge = client.GetEmployeeBadge(this.mIDNumber);
                    this.lblID.Text = "#" + this.mIDNumber.ToString();
                    this.txtLastName.Text = badge.LastName;
                    this.txtFirstName.Text = badge.FirstName;
                    this.txtMiddle.Text = badge.Middle;
                    this.txtSuffix.Text = badge.Suffix;
                    this.txtSSN.Text = badge.SSN;
                    this.cboLocation.SelectedValue = badge.Location;
                    this.cboDepartment.SelectedValue = badge.Department;
                    this.cboSubLocation.SelectedValue = badge.SubLocation;
                    this.cboStatus.SelectedValue = badge.Status;
                    this.txtHireDate.Text = badge.HireDate.ToString("MM/dd/yyyy");

                    this.txtSSN.Enabled = false;
                    this.txtHireDate.Enabled = false;

                    this.imgPhoto.ImageUrl = "~/Photo.aspx?type=Employees&id=" + badge.IDNumber;
                }
                else {
                    this.lblID.Text = "New";
                    this.cboStatus.SelectedIndex = 0;
                }
            }
            else {
                this.mIDNumber = int.Parse(ViewState["ID"].ToString());
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null, EventArgs.Empty); }
    }
    protected void OnValidateForm(object sender, EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnOk.Enabled = this.txtLastName.Text.Trim().Length > 0 &&
                                 this.txtFirstName.Text.Trim().Length > 0 &&
                                 this.txtHireDate.Text.Trim().Length > 0 &&
                                 this.cboLocation.SelectedValue != null &&
                                 this.cboSubLocation.SelectedValue != null && 
                                 this.cboDepartment.SelectedValue != null &&
                                 this.cboStatus.SelectedValue != null;
            this.btnCancel.Enabled = true;
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnCommand(object sender, CommandEventArgs e) {
        //
        try {
            switch(e.CommandName) {
                case "Ok":
                    EmployeeBadge badge = new EmployeeBadge();
                    badge.IDNumber = this.mIDNumber;
                    badge.LastName = this.txtLastName.Text;
                    badge.FirstName = this.txtFirstName.Text;
                    badge.Middle = this.txtMiddle.Text;
                    badge.Suffix = this.txtSuffix.Text;
                    badge.SSN = this.txtSSN.Text.Replace("-","");
                    badge.HireDate = DateTime.Parse(this.txtHireDate.Text);
                    badge.Location = this.cboLocation.SelectedValue;
                    badge.SubLocation = this.cboSubLocation.SelectedValue;
                    badge.Department = this.cboDepartment.SelectedValue;
                    badge.Status = this.cboStatus.SelectedValue;
                    BadgeGateway client = new BadgeGateway();
                    bool res = false;
                    if (this.mIDNumber == 0) {
                        res = client.AddEmployeeBadge(badge);
                        Master.ShowMessageBox("New employee badge has been created.");
                    }
                    else {
                        res = client.UpdateEmployeeBadge(badge);
                        Master.ShowMessageBox("Employee badge #" + this.mIDNumber.ToString() + " has been updated.");
                    }
                    this.btnOk.Enabled = false;
                    this.btnCancel.Text = "Close";
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
