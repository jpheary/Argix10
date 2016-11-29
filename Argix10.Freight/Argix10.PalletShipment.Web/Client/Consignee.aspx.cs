using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Consignee:System.Web.UI.Page {
    //Members
    private LTLConsignee mConsignee = null;
    private string mCallingURI = "";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                int id = int.Parse(Request.QueryString["id"]);
                if (id > 0) this.mConsignee = new FreightGateway().ReadLTLConsignee(id);
                ViewState.Add("Consignee",this.mConsignee);

                this.mCallingURI = Page.Request.UrlReferrer.AbsoluteUri.Split('?')[0];
                ViewState.Add("CallingURI",this.mCallingURI);

                //Setup UI; disable client change for new/update
                Master.ClientsEnabled = false;
                this.lblClientName.Text = Master.CurrentClient.Name;
                if (this.mConsignee != null) {
                    this.txtName.Text = this.mConsignee.Name;
                    this.txtStreet.Text = this.mConsignee.AddressLine1;
                    this.txtCity.Text = this.mConsignee.City;
                    this.txtState.Text = this.mConsignee.State;
                    this.txtZip.Text = this.mConsignee.Zip;
                    this.txtContactName.Text = this.mConsignee.ContactName;
                    this.txtContactPhone.Text = this.mConsignee.ContactPhone;
                    this.txtContactEmail.Text = this.mConsignee.ContactEmail;
                    this.txtWindowOpen.Text = this.mConsignee.WindowStartTime.ToString("HH:mm");
                    this.txtWindowClose.Text = this.mConsignee.WindowEndTime.ToString("HH:mm");
                }
                else {
                    this.txtWindowOpen.Text = "09:00";
                    this.txtWindowClose.Text = "17:00";
                }
                this.txtName.ReadOnly = this.txtCity.ReadOnly = this.txtState.ReadOnly = this.txtZip.ReadOnly = this.mConsignee != null;
            }
            else {
                this.mConsignee = ViewState["Consignee"] != null ? (LTLConsignee)ViewState["Consignee"] : null;
                this.mCallingURI = ViewState["CallingURI"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnZipChanged(object sender,EventArgs e) {
        //
        try {
            string zipCode = this.txtZip.Text;
            ServiceLocation location = new FreightGateway().ReadServiceLocation(zipCode);
            if (location == null) {
                this.txtZip.Text = "";
                this.txtZip.Focus();
                throw new ApplicationException(zipCode + " is currently not supported for delivery.");
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
                    if (this.mConsignee == null) {
                        //New
                        LTLConsignee consignee = new LTLConsignee();
                        consignee.ClientID = Master.CurrentClient.ID;
                        consignee.Name = this.txtName.Text;
                        consignee.AddressLine1 = this.txtStreet.Text;
                        consignee.AddressLine2 = "";
                        consignee.City = this.txtCity.Text;
                        consignee.State = this.txtState.Text;
                        consignee.Zip = this.txtZip.Text;
                        consignee.Zip4 = "";
                        consignee.WindowStartTime = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                        consignee.WindowEndTime = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                        consignee.ContactName = this.txtContactName.Text;
                        consignee.ContactPhone = this.txtContactPhone.Text;
                        consignee.ContactEmail = this.txtContactEmail.Text;
                        consignee.Status = "A";
                        consignee.UserID = Membership.GetUser().UserName;
                        consignee.LastUpdated = DateTime.Now;
                        int id = new FreightGateway().CreateLTLConsignee(consignee);
                        ScriptManager.RegisterStartupScript(this.txtName,typeof(TextBox),"Consignee","alert('New consignee " + consignee.Name + " has been created.');",true);
                    }
                    else {
                        //Update
                        this.mConsignee.AddressLine1 = this.txtStreet.Text;
                        this.mConsignee.AddressLine2 = "";
                        this.mConsignee.City = this.txtCity.Text;
                        this.mConsignee.State = this.txtState.Text;
                        this.mConsignee.Zip = this.txtZip.Text;
                        this.mConsignee.Zip4 = "";
                        this.mConsignee.WindowStartTime = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                        this.mConsignee.WindowEndTime = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                        this.mConsignee.ContactName = this.txtContactName.Text;
                        this.mConsignee.ContactPhone = this.txtContactPhone.Text;
                        this.mConsignee.ContactEmail = this.txtContactEmail.Text;
                        this.mConsignee.UserID = Membership.GetUser().UserName;
                        this.mConsignee.LastUpdated = DateTime.Now;
                        bool updated = new FreightGateway().UpdateLTLConsignee(this.mConsignee);
                        ScriptManager.RegisterStartupScript(this.txtName,typeof(TextBox),"Consignee","alert('Consignee " + this.mConsignee.Name + " has been updated.');",true);
                    }
                    this.btnSubmit.Enabled = false;
                    this.btnCancel.Text = "Close";
                    break;
                case "Cancel":
                    Response.Redirect(this.mCallingURI + "?view=consignees",false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
