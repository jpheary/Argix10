using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;
using Argix.Enterprise.USPS;

public partial class Shipper : System.Web.UI.Page {
    //Members
    private LTLShipper2 mShipper = null;
    private bool mAddressValid = false;
    private string mCallingURI = "";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                string number = Request.QueryString["number"];
                if (number.Length > 0) this.mShipper = new FreightGateway().ReadLTLShipper(number);
                ViewState.Add("Shipper",this.mShipper);

                this.mAddressValid = this.mShipper != null;      //No address validation required on existing
                ViewState.Add("AddressValid", this.mAddressValid);

                this.mCallingURI = Page.Request.UrlReferrer.AbsoluteUri.Split('?')[0];
                ViewState.Add("CallingURI",this.mCallingURI);

                //Setup UI; disable client change for new/update
                Master.ClientsEnabled = false;
                this.lblClientName.Text = Master.CurrentClient.Name;
                if (this.mShipper != null) {
                    this.txtName.Text = this.mShipper.Name.Trim();
                    this.txtAddressLine1.Text = this.mShipper.AddressLine1.Trim();
                    this.txtAddressLine2.Text = this.mShipper.AddressLine2.Trim();
                    this.txtCity.Text = this.mShipper.City.Trim();
                    this.txtState.Text = this.mShipper.State.Trim();
                    this.txtZip5.Text = this.mShipper.Zip.Trim();
                    this.txtZip4.Text = this.mShipper.Zip4.Trim();
                    this.txtContactName.Text = this.mShipper.ContactName.Trim();
                    this.txtContactPhone.Text = this.mShipper.ContactPhone.Trim();
                    this.txtContactEmail.Text = this.mShipper.ContactEmail.Trim();
                    this.txtWindowOpen.Text = this.mShipper.WindowTimeStart.ToString("hh:mm tt");
                    this.txtWindowClose.Text = this.mShipper.WindowTimeEnd.ToString("hh:mm tt");
                }
                else {
                    this.txtWindowOpen.Text = "09:00 AM";
                    this.txtWindowClose.Text = "05:00 PM";
                }
                this.txtCity.ReadOnly = this.txtState.ReadOnly = this.txtZip5.ReadOnly = this.mShipper != null;
                this.mvwPage.ActiveViewIndex = 0;
            }
            else {
                this.mShipper = ViewState["Shipper"] != null ? (LTLShipper2)ViewState["Shipper"] : null;
                this.mAddressValid = ViewState["AddressValid"] != null ? (bool)ViewState["AddressValid"] : false;
                this.mCallingURI = ViewState["CallingURI"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnZipChanged(object sender,EventArgs e) {
        //Event handler for change in zip
        try {
            //Validate the zip is servicable; display city/state if it is; notify and log unsupported zips if not
            string zipCode = this.txtZip5.Text;
            if(this.mShipper == null && zipCode.Trim().Length == 5) {
                ServiceLocation location = new FreightGateway().ReadPickupLocation(zipCode);
                if(location == null) {
                    this.txtZip5.Text = "";
                    this.txtZip5.Focus();
                    throw new ApplicationException(zipCode + " is currently not supported for pickup.");
                }
                else {
                    this.txtCity.Text = location.City.Trim();
                    this.txtState.Text = location.State.Trim();
                    this.txtName.Focus();
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnValidate.Enabled = true;
            this.btnSubmit.Enabled = Master.CurrentClientApproved;
            this.btnCancel.Enabled = true;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Validate":
                    bool verified = verifyAddress(this.txtName.Text, this.txtAddressLine1.Text, this.txtAddressLine2.Text, this.txtCity.Text, this.txtState.Text, this.txtZip5.Text, this.txtZip4.Text);
                    if(verified) {
                        ViewState["AddressValid"] = true;
                        Master.ShowMessageBox("Address is valid.");
                    }
                    break;
                case "ChooseAddress":
                    //Choose user address as valid address
                    this.txtName.Text = this.txtSrcName.Text;
                    this.txtAddressLine1.Text = this.txtSrcAddressLine1.Text;
                    this.txtAddressLine2.Text = this.txtSrcAddressLine2.Text;
                    this.txtCity.Text = this.txtSrcCity.Text;
                    this.txtState.Text = this.txtSrcState.Text;
                    this.txtZip5.Text = this.txtSrcZip5.Text;
                    this.txtZip4.Text = this.txtSrcZip4.Text;

                    ViewState["AddressValid"] = true;
                    this.mvwPage.ActiveViewIndex = 0;
                    break;
                case "ChooseUSPSAddress":
                    //Choose USPS address as valid address
                    this.txtName.Text = this.txtUSPSName.Text;
                    this.txtAddressLine1.Text = this.txtUSPSAddressLine1.Text;
                    this.txtAddressLine2.Text = this.txtUSPSAddressLine2.Text;
                    this.txtCity.Text = this.txtUSPSCity.Text;
                    this.txtState.Text = this.txtUSPSState.Text;
                    this.txtZip5.Text = this.txtUSPSZip5.Text;
                    this.txtZip4.Text = this.txtUSPSZip4.Text;

                    ViewState["AddressValid"] = true;
                    this.mvwPage.ActiveViewIndex = 0;
                    break;
                case "Submit":
                    if (this.mShipper == null) {
                        //New- ensure the address is valid
                        if(!this.mAddressValid) {
                            this.mAddressValid = verifyAddress(this.txtName.Text, this.txtAddressLine1.Text, this.txtAddressLine2.Text, this.txtCity.Text, this.txtState.Text, this.txtZip5.Text, this.txtZip4.Text);
                            ViewState["AddressValid"] = this.mAddressValid;
                        }
                        if(this.mAddressValid) {
                            LTLShipper2 shipper = new LTLShipper2();
                            shipper.ClientNumber = Master.CurrentClient.Number;
                            shipper.Name = this.txtName.Text;
                            shipper.AddressLine1 = this.txtAddressLine1.Text;
                            shipper.AddressLine2 = this.txtAddressLine2.Text;
                            shipper.City = this.txtCity.Text;
                            shipper.State = this.txtState.Text;
                            shipper.Zip = this.txtZip5.Text;
                            shipper.Zip4 = this.txtZip4.Text;
                            shipper.WindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                            shipper.WindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                            shipper.ContactName = this.txtContactName.Text;
                            shipper.ContactPhone = this.txtContactPhone.Text;
                            shipper.ContactEmail = this.txtContactEmail.Text;
                            shipper.UserID = Membership.GetUser().UserName;
                            shipper.LastUpdated = DateTime.Now;
                            string number = new FreightGateway().CreateLTLShipper(shipper);

                            this.btnValidate.Enabled = this.btnSubmit.Enabled = false;
                            this.btnCancel.Text = "Close";
                            Master.ShowMessageBox("New shipper " + shipper.Name + " has been created.");
                        }
                    }
                    else {
                        //Update
                        this.mShipper.Name = this.txtName.Text;
                        this.mShipper.AddressLine1 = this.txtAddressLine1.Text;
                        this.mShipper.AddressLine2 = this.txtAddressLine2.Text;
                        this.mShipper.Zip4 = this.txtZip4.Text;
                        this.mShipper.WindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                        this.mShipper.WindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                        this.mShipper.ContactName = this.txtContactName.Text;
                        this.mShipper.ContactPhone = this.txtContactPhone.Text;
                        this.mShipper.ContactEmail = this.txtContactEmail.Text;
                        this.mShipper.UserID = Membership.GetUser().UserName;
                        this.mShipper.LastUpdated = DateTime.Now;
                        bool updated = new FreightGateway().UpdateLTLShipper(this.mShipper);

                        this.btnValidate.Enabled = this.btnSubmit.Enabled = false;
                        this.btnCancel.Text = "Close";
                        Master.ShowMessageBox("Shipper " + this.mShipper.Name + " has been updated.");
                    }
                    break;
                case "Cancel":
                    Response.Redirect(this.mCallingURI + "?view=shippers",false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }

    private bool verifyAddress(string firmName, string addressLine1, string addressLine2, string city, string state, string zip5, string zip4) {
        //Call USPS WebAPI to verify address
        bool verified = false;
        try {
            AddressValidateResponse avr = new USPSGateway().VerifyAddress(firmName, addressLine1, addressLine2, city, state, zip5, zip4);
            if(avr.Error.Rows.Count > 0) {
                //Bad address or syntax
                string error = (!avr.Error[0].IsNumberNull() ? avr.Error[0].Number : "") + " " + (!avr.Error[0].IsSourceNull() ? avr.Error[0].Source : "") + "\r\n" + (!avr.Error[0].IsDescriptionNull() ? avr.Error[0].Description : "");
                Master.ShowMessageBox(error);
            }
            else if(avr.Address.Rows.Count > 0) {
                ////Does it match the request?
                string srcAddress = addressLine2.Trim().ToLower() + addressLine1.Trim().ToLower();
                string uspsAddress = (!avr.Address[0].IsAddress1Null() ? avr.Address[0].Address1.Trim().ToLower() : "") + (!avr.Address[0].IsAddress2Null() ? avr.Address[0].Address2.Trim().ToLower() : "");
                bool match = (srcAddress == uspsAddress) &&
                             (city.Trim().ToLower() == (!avr.Address[0].IsCityNull() ? avr.Address[0].City.Trim().ToLower() : "")) &&
                             (state.Trim().ToLower() == (!avr.Address[0].IsStateNull() ? avr.Address[0].State.Trim().ToLower() : "")) &&
                             (zip5.Trim().ToLower() == (!avr.Address[0].IsZip5Null() ? avr.Address[0].Zip5.Trim().ToLower() : ""));
                if(match && avr.Address[0].IsReturnTextNull()) {
                    //Use scrubbed USPS address
                    this.txtName.Text = firmName;
                    this.txtAddressLine1.Text = !avr.Address[0].IsAddress2Null() ? avr.Address[0].Address2 : "";
                    this.txtAddressLine2.Text = !avr.Address[0].IsAddress1Null() ? avr.Address[0].Address1 : "";
                    this.txtCity.Text = !avr.Address[0].IsCityNull() ? avr.Address[0].City : "";
                    this.txtState.Text = !avr.Address[0].IsStateNull() ? avr.Address[0].State : "";
                    this.txtZip5.Text = !avr.Address[0].IsZip5Null() ? avr.Address[0].Zip5 : "";
                    this.txtZip4.Text = !avr.Address[0].IsZip4Null() ? avr.Address[0].Zip4 : "";
                    verified = true;
                }
                else {
                    //Let user choose
                    this.txtSrcName.Text = firmName;
                    this.txtSrcAddressLine1.Text = addressLine1;
                    this.txtSrcAddressLine2.Text = addressLine2;
                    this.txtSrcCity.Text = city;
                    this.txtSrcState.Text = state;
                    this.txtSrcZip5.Text = zip5;
                    this.txtSrcZip4.Text = zip4;

                    this.lblMessage.Text = !avr.Address[0].IsReturnTextNull() ? avr.Address[0].ReturnText : "";
                    this.txtUSPSName.Text = firmName;
                    this.txtUSPSAddressLine1.Text = !avr.Address[0].IsAddress2Null() ? avr.Address[0].Address2 : "";
                    this.txtUSPSAddressLine2.Text = !avr.Address[0].IsAddress1Null() ? avr.Address[0].Address1 : "";
                    this.txtUSPSCity.Text = !avr.Address[0].IsCityNull() ? avr.Address[0].City : "";
                    this.txtUSPSState.Text = !avr.Address[0].IsStateNull() ? avr.Address[0].State : "";
                    this.txtUSPSZip5.Text = !avr.Address[0].IsZip5Null() ? avr.Address[0].Zip5 : "";
                    this.txtUSPSZip4.Text = !avr.Address[0].IsZip4Null() ? avr.Address[0].Zip4 : "";

                    this.mvwPage.ActiveViewIndex = 1;
                }
            }
            else {
                //Not verified
                Master.ShowMessageBox("The adddress could not be verified by the US Postal Service.");
            }
        }
        catch(Exception ex) { Master.ReportError(ex, 4); }
        return verified;
    }
}
