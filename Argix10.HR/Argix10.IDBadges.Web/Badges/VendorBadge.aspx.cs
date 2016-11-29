using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR;

public partial class _VendorBadge:System.Web.UI.Page {
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
                    VendorBadge badge = client.GetVendorBadge(this.mIDNumber);
                    this.lblID.Text = "#" + this.mIDNumber.ToString();
                    this.txtLastName.Text = badge.LastName;
                    this.txtFirstName.Text = badge.FirstName;
                    this.txtMiddle.Text = badge.Middle;
                    this.txtSuffix.Text = badge.Suffix;
                    this.cboLocation.SelectedValue = badge.Location;
                    this.cboDepartment.SelectedValue = badge.Department;
                    this.cboStatus.SelectedValue = badge.Status;

                    this.imgPhoto.ImageUrl = "~/Photo.aspx?type=Vendors&id=" + badge.IDNumber;
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
                                 this.cboLocation.SelectedValue != null &&
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
                    VendorBadge badge = new VendorBadge();
                    badge.IDNumber = this.mIDNumber;
                    badge.LastName = this.txtLastName.Text;
                    badge.FirstName = this.txtFirstName.Text;
                    badge.Middle = this.txtMiddle.Text;
                    badge.Suffix = this.txtSuffix.Text;
                    badge.Location = this.cboLocation.SelectedValue;
                    badge.Department = this.cboDepartment.SelectedValue;
                    badge.Status = this.cboStatus.SelectedValue;
                    BadgeGateway client = new BadgeGateway();
                    bool res = false;
                    if (this.mIDNumber == 0) {
                        res = client.AddVendorBadge(badge);
                        Master.ShowMessageBox("New vendor badge has been created.");
                    }
                    else {
                        res = client.UpdateVendorBadge(badge);
                        Master.ShowMessageBox("Vendor badge #" + this.mIDNumber.ToString() + " has been updated.");
                    }
                    this.btnOk.Enabled = false;
                    this.btnCancel.Text = "Close";
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "ClearCursor", "document.body.style.cursor='default';", true);
                    break;
                case "Cancel":
                    Response.Redirect("~/Badges/Badges.aspx?type=Vendors", false);
                    break;
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 4); }
    }
}
