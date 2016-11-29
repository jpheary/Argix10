using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;
using Argix.Enterprise.USPS;

public partial class Consignee:System.Web.UI.Page {
    //Members
    private LTLConsignee2 mConsignee = null;
    private bool mAddressValid = false;
    private string mCallingURI = "";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                string number = Request.QueryString["number"];
                if (number.Trim().Length > 0) this.mConsignee = new FreightGateway().ReadLTLConsignee(int.Parse(number),Master.CurrentClient.Number);
                ViewState.Add("Consignee",this.mConsignee);

                this.mAddressValid = this.mConsignee != null;      //No address validation required on existing
                ViewState.Add("AddressValid", this.mAddressValid);

                this.mCallingURI = Page.Request.UrlReferrer.AbsoluteUri.Split('?')[0];
                ViewState.Add("CallingURI",this.mCallingURI);

                //Setup UI; disable client change for new/update
                Master.ClientsEnabled = false;
                this.lblClientName.Text = Master.CurrentClient.Name;
                if (this.mConsignee != null) {
                    this.txtName.Text = this.mConsignee.Name.Trim();
                    this.txtAddressLine1.Text = this.mConsignee.AddressLine1.Trim();
                    this.txtAddressLine2.Text = this.mConsignee.AddressLine2.Trim();
                    this.txtCity.Text = this.mConsignee.City.Trim();
                    this.txtState.Text = this.mConsignee.State.Trim();
                    this.txtZip5.Text = this.mConsignee.Zip.Trim();
                    this.txtZip4.Text = this.mConsignee.Zip4.Trim();
                    this.txtContactName.Text = this.mConsignee.ContactName.Trim();
                    this.txtContactPhone.Text = this.mConsignee.ContactPhone.Trim();
                    this.txtContactEmail.Text = this.mConsignee.ContactEmail.Trim();
                    this.txtWindowOpen.Text = this.mConsignee.WindowTimeStart.ToString("hh:mm tt");
                    this.txtWindowClose.Text = this.mConsignee.WindowTimeEnd.ToString("hh:mm tt");
                }
                else {
                    this.txtWindowOpen.Text = "09:00 AM";
                    this.txtWindowClose.Text = "05:00 PM";
                }
                this.txtCity.ReadOnly = this.txtState.ReadOnly = this.txtZip5.ReadOnly = this.mConsignee != null;
                this.mvwPage.ActiveViewIndex = 0;
            }
            else {
                this.mConsignee = ViewState["Consignee"] != null ? (LTLConsignee2)ViewState["Consignee"] : null;
                this.mAddressValid = ViewState["AddressValid"] != null ? (bool)ViewState["AddressValid"] : false;
                this.mCallingURI = ViewState["CallingURI"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnZipChanged(object sender,EventArgs e) {
        //Event handler for change in zip
        try {
            //Validate the zip is servicable; display city/state if it is; notify and log unsupported zips if not
            string zipCode = this.txtZip5.Text;
            if(this.mConsignee == null && zipCode.Trim().Length == 5) {
                ServiceLocation location = new FreightGateway().ReadServiceLocation(zipCode);
                if(location == null) {
                    this.txtZip5.Text = "";
                    this.txtZip5.Focus();
                    throw new ApplicationException(zipCode + " is currently not supported for delivery.");
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
                        Master.ShowMessageBox("The Address has been validated.");
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
                    if (this.mConsignee == null) {
                        //New- ensure the address is valid
                        if(!this.mAddressValid) {
                            this.mAddressValid = verifyAddress(this.txtName.Text, this.txtAddressLine1.Text, this.txtAddressLine2.Text, this.txtCity.Text, this.txtState.Text, this.txtZip5.Text, this.txtZip4.Text);
                            ViewState["AddressValid"] = this.mAddressValid;
                        }
                        if(this.mAddressValid) {
                            LTLConsignee2 consignee = new LTLConsignee2();
                            consignee.ClientNumber = Master.CurrentClient.Number;
                            consignee.Name = this.txtName.Text;
                            consignee.AddressLine1 = this.txtAddressLine1.Text;
                            consignee.AddressLine2 = this.txtAddressLine2.Text;
                            consignee.City = this.txtCity.Text;
                            consignee.State = this.txtState.Text;
                            consignee.Zip = this.txtZip5.Text;
                            consignee.Zip4 = this.txtZip4.Text;
                            consignee.WindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                            consignee.WindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                            consignee.ContactName = this.txtContactName.Text;
                            consignee.ContactPhone = this.txtContactPhone.Text;
                            consignee.ContactEmail = this.txtContactEmail.Text;
                            consignee.UserID = Membership.GetUser().UserName;
                            consignee.LastUpdated = DateTime.Now;
                            int number = new FreightGateway().CreateLTLConsignee(consignee);

                            this.btnValidate.Enabled = this.btnSubmit.Enabled = false;
                            this.btnCancel.Text = "Close";
                            Master.ShowMessageBox("New consignee " + consignee.Name + " has been created.");
                        }
                    }
                    else {
                        //Update
                        this.mConsignee.Name = this.txtName.Text;
                        this.mConsignee.AddressLine1 = this.txtAddressLine1.Text;
                        this.mConsignee.AddressLine2 = this.txtAddressLine2.Text;
                        this.mConsignee.Zip4 = this.txtZip4.Text;
                        this.mConsignee.WindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtWindowOpen.Text);
                        this.mConsignee.WindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtWindowClose.Text);
                        this.mConsignee.ContactName = this.txtContactName.Text;
                        this.mConsignee.ContactPhone = this.txtContactPhone.Text;
                        this.mConsignee.ContactEmail = this.txtContactEmail.Text;
                        this.mConsignee.UserID = Membership.GetUser().UserName;
                        this.mConsignee.LastUpdated = DateTime.Now;
                        bool updated = new FreightGateway().UpdateLTLConsignee(this.mConsignee);
                        
                        this.btnValidate.Enabled = this.btnSubmit.Enabled = false;
                        this.btnCancel.Text = "Close";
                        Master.ShowMessageBox("Consignee " + this.mConsignee.Name + " has been updated.");
                    }
                    break;
                case "Cancel":
                    Response.Redirect(this.mCallingURI + "?view=consignees",false);
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
