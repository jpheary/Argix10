using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Shipper : System.Web.UI.Page {
    //Members
    private LTLShipper mShipper = null;
    private string mCallingURI = "";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                int id = int.Parse(Request.QueryString["id"]);
                if (id > 0) this.mShipper = new FreightGateway().ReadLTLShipper(id);
                ViewState.Add("Shipper",this.mShipper);

                this.mCallingURI = Page.Request.UrlReferrer.AbsoluteUri.Split('?')[0];
                ViewState.Add("CallingURI",this.mCallingURI);

                //Setup UI; disable client change for new/update
                Master.ClientsEnabled = false;
                this.lblClientName.Text = Master.CurrentClient.Name;
                if (this.mShipper != null) {
                    this.txtName.Text = this.mShipper.Name;
                    this.txtStreet.Text = this.mShipper.AddressLine1;
                    this.txtCity.Text = this.mShipper.City;
                    this.txtState.Text = this.mShipper.State;
                    this.txtZip.Text = this.mShipper.Zip;
                    this.txtContactName.Text = this.mShipper.ContactName;
                    this.txtContactPhone.Text = this.mShipper.ContactPhone;
                    this.txtContactEmail.Text = this.mShipper.ContactEmail;
                    this.txtWindowOpen.Text = this.mShipper.WindowStartTime.ToString("HH:mm");
                    this.txtWindowClose.Text = this.mShipper.WindowEndTime.ToString("HH:mm");
                }
                else {
                    this.txtWindowOpen.Text = "09:00";
                    this.txtWindowClose.Text = "17:00";
                }
                this.txtName.ReadOnly = this.txtCity.ReadOnly = this.txtState.ReadOnly = this.txtZip.ReadOnly = this.mShipper != null;
            }
            else {
                this.mShipper = ViewState["Shipper"] != null ? (LTLShipper)ViewState["Shipper"] : null;
                this.mCallingURI = ViewState["CallingURI"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnZipChanged(object sender,EventArgs e) {
        //
        try {
            string zipCode = this.txtZip.Text;
            ServiceLocation location = new FreightGateway().ReadPickupLocation(zipCode);
            if (location == null) {
                this.txtZip.Text = "";
                this.txtZip.Focus();
                throw new ApplicationException(zipCode + " is currently not supported for pickup.");
            }
            else {
                this.txtCity.Text = location.City.Trim();
                this.txtState.Text = location.State.Trim();
                this.txtName.Focus();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnSubmit.Enabled = Master.CurrentClient.Approved;
            this.btnCancel.Enabled = true;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Submit":
                    if (this.mShipper == null) {
                        //New
                        LTLShipper shipper = new LTLShipper();
                        shipper.ClientID = Master.CurrentClient.ID;
                        shipper.Name = this.txtName.Text;
                        shipper.AddressLine1 = this.txtStreet.Text;
                        shipper.AddressLine2 = "";
                        shipper.City = this.txtCity.Text;
                        shipper.State = this.txtState.Text;
                        shipper.Zip = this.txtZip.Text;
                        shipper.Zip4 = "";
                        shipper.WindowStartTime = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                        shipper.WindowEndTime = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                        shipper.ContactName = this.txtContactName.Text;
                        shipper.ContactPhone = this.txtContactPhone.Text;
                        shipper.ContactEmail = this.txtContactEmail.Text;
                        shipper.Status = "A";
                        shipper.UserID = Membership.GetUser().UserName;
                        shipper.LastUpdated = DateTime.Now;
                        int id = new FreightGateway().CreateLTLShipper(shipper);
                        ScriptManager.RegisterStartupScript(this.txtName,typeof(TextBox),"Shipper","alert('New shipper " + shipper.Name + " has been created.');",true);
                    }
                    else {
                        //Update
                        this.mShipper.AddressLine1 = this.txtStreet.Text;
                        this.mShipper.AddressLine2 = "";
                        this.mShipper.City = this.txtCity.Text;
                        this.mShipper.State = this.txtState.Text;
                        this.mShipper.Zip = this.txtZip.Text;
                        this.mShipper.Zip4 = "";
                        this.mShipper.WindowStartTime = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                        this.mShipper.WindowEndTime = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                        this.mShipper.ContactName = this.txtContactName.Text;
                        this.mShipper.ContactPhone = this.txtContactPhone.Text;
                        this.mShipper.ContactEmail = this.txtContactEmail.Text;
                        this.mShipper.Status = "A";
                        this.mShipper.UserID = Membership.GetUser().UserName;
                        this.mShipper.LastUpdated = DateTime.Now;
                        bool updated = new FreightGateway().UpdateLTLShipper(this.mShipper);
                        ScriptManager.RegisterStartupScript(this.txtName,typeof(TextBox),"Shipper","alert('Shipper " + this.mShipper.Name + " has been updated.');",true);
                    }
                    this.btnSubmit.Enabled = false;
                    this.btnCancel.Text = "Close";
                    break;
                case "Cancel":
                    Response.Redirect(this.mCallingURI + "?view=shippers",false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
