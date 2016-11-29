using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Enterprise.USPS;

namespace Argix.Freight {
    //
    public partial class dlgLTLClient:Form {
        //Members
        private LTLClient2 mClient = null;
        private bool mAddressValid=false;

        //Interface
        public dlgLTLClient(LTLClient2 client) {
            //Constructor
            try {
                InitializeComponent();
                this.mClient = client;
                if(this.mClient == null) throw new ApplicationException("Client cannot be null.");
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.mClient.ID > 0) {
                    //Existing client
                    this.mAddressValid = true;
                    this.Text = "Client# " + this.mClient.ID.ToString();
                    this.txtName.Text = this.mClient.Name.Trim();
                    this.txtAddressLine1.Text = this.mClient.AddressLine1.Trim();
                    this.txtAddressLine2.Text = this.mClient.AddressLine2.Trim();
                    this.txtCity.Text = this.mClient.City.Trim();
                    this.txtState.Text = this.mClient.State.Trim();
                    this.txtZip5.Text = this.mClient.Zip.Trim();
                    this.txtZip4.Text = this.mClient.Zip4.Trim();
                    this.txtContactName.Text = this.mClient.ContactName.Trim();
                    this.mskContactPhone.Text = this.mClient.ContactPhone.Trim();
                    this.txtContactEmail.Text = this.mClient.ContactEmail.Trim();
                    this.txtCorpName.Text = this.mClient.CorporateName.Trim();
                    this.txtCorpAddressLine1.Text = this.mClient.CorporateAddressLine1.Trim();
                    this.txtCorpAddressLine2.Text = this.mClient.CorporateAddressLine2.Trim();
                    this.txtCorpCity.Text = this.mClient.CorporateCity.Trim();
                    this.txtCorpState.Text = this.mClient.CorporateState.Trim();
                    this.txtCorpZip5.Text = this.mClient.CorporateZip.Trim();
                    this.txtCorpZip4.Text = this.mClient.CorporateZip4.Trim();
                    this.txtTaxID.Text = this.mClient.TaxID.Trim();
                    this.txtBillAddressLine1.Text = this.mClient.BillingAddressLine1.Trim();
                    this.txtBillAddressLine2.Text = this.mClient.BillingAddressLine2.Trim();
                    this.txtBillCity.Text = this.mClient.BillingCity.Trim();
                    this.txtBillState.Text = this.mClient.BillingState.Trim();
                    this.txtBillZip5.Text = this.mClient.BillingZip.Trim();
                    this.txtBillZip4.Text = this.mClient.BillingZip4.Trim();
                    this.cboStatus.SelectedIndex = -1;
                    if(this.mClient.ApprovalDate.CompareTo(DateTime.MinValue) > 0) this.cboStatus.SelectedIndex = 0;
                    if(this.mClient.DenialDate.CompareTo(DateTime.MinValue) > 0) this.cboStatus.SelectedIndex = 1;
                    this.txtTsortClientNumber.Text = this.mClient.Number.Trim();
                    this.txtSalesRepClientNumber.Text = this.mClient.SalesRepClientNumber.Trim();
                }
                else {
                    //New client
                    this.Text = "New Client";
                    this.txtName.Text = this.txtAddressLine1.Text = this.txtAddressLine2.Text = this.txtCity.Text = this.txtState.Text = this.txtZip5.Text = this.txtZip4.Text = "";
                    this.txtContactName.Text = this.mskContactPhone.Text = this.txtContactEmail.Text = "";
                    this.txtCorpName.Text = this.txtCorpAddressLine1.Text = this.txtCorpAddressLine2.Text = this.txtCorpCity.Text = this.txtCorpState.Text = this.txtCorpZip5.Text = this.txtCorpZip4.Text = this.txtTaxID.Text = "";
                    this.txtBillAddressLine1.Text = this.txtBillAddressLine2.Text = this.txtBillCity.Text = this.txtBillState.Text = this.txtBillZip5.Text = this.txtBillZip4.Text = "";
                    this.cboStatus.SelectedIndex = -1;
                    this.txtTsortClientNumber.Text = "";
                    this.mClient.SalesRepClientNumber = "";
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnUseCompanyForCorporate(object sender, EventArgs e) {
            //Copy company to corporate
            try {
                this.txtCorpName.Text = this.chkUseCompanyforCorporate.Checked ? this.txtName.Text : "";
                this.txtCorpAddressLine1.Text = this.chkUseCompanyforCorporate.Checked ? this.txtAddressLine1.Text : "";
                this.txtCorpAddressLine2.Text = this.chkUseCompanyforCorporate.Checked ? this.txtAddressLine2.Text : "";
                this.txtCorpCity.Text = this.chkUseCompanyforCorporate.Checked ? this.txtCity.Text : "";
                this.txtCorpState.Text = this.chkUseCompanyforCorporate.Checked ? this.txtState.Text : "";
                this.txtCorpZip5.Text = this.chkUseCompanyforCorporate.Checked ? this.txtZip5.Text : "";
                this.txtCorpZip4.Text = this.chkUseCompanyforCorporate.Checked ? this.txtZip4.Text : "";
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnUseCompanyForBilling(object sender, EventArgs e) {
            //Copy company to billing
            try {
                this.txtBillAddressLine1.Text = this.chkUseCompanyforBilling.Checked ? this.txtAddressLine1.Text : "";
                this.txtBillAddressLine2.Text = this.chkUseCompanyforBilling.Checked ? this.txtAddressLine2.Text : "";
                this.txtBillCity.Text = this.chkUseCompanyforBilling.Checked ? this.txtCity.Text : "";
                this.txtBillState.Text = this.chkUseCompanyforBilling.Checked ? this.txtState.Text : "";
                this.txtBillZip5.Text = this.chkUseCompanyforBilling.Checked ? this.txtZip5.Text : "";
                this.txtBillZip4.Text = this.chkUseCompanyforBilling.Checked ? this.txtZip4.Text : "";
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnStatusChanged(object sender, EventArgs e) { OnValidateForm(null, EventArgs.Empty); }
        private void OnNumberChanged(object sender,EventArgs e) { OnValidateForm(null,EventArgs.Empty); }
        private void OnValidateForm(object sender,EventArgs e) {
            //Set user services
            try {
                //Set services
                this.chkUseCompanyforBilling.Enabled = !(this.txtBillAddressLine1.ReadOnly = this.txtBillCity.ReadOnly = this.txtBillState.ReadOnly = this.txtBillZip5.ReadOnly = this.mClient.ID > 0);
                this.cboStatus.Enabled = false;
                this.txtTsortClientNumber.ReadOnly = true;
                this.btnVerifyAddress.Enabled = true;
                this.btnOk.Enabled = this.mAddressValid && (this.txtName.Text.Length > 0 && this.txtAddressLine1.Text.Length > 0 && this.txtCity.Text.Length > 0 && this.txtState.Text.Length > 0 && this.txtZip5.Text.Length > 0 &&
                                    this.txtContactName.Text.Length > 0 && this.mskContactPhone.Text.Length > 0 && 
                                    this.txtCorpName.Text.Length > 0 && this.txtCorpAddressLine1.Text.Length > 0 && this.txtCorpCity.Text.Length > 0 && this.txtCorpState.Text.Length > 0 && this.txtCorpZip5.Text.Length > 0 && 
                                    (this.mClient.ID > 0 || (this.mClient.ID == 0 && this.txtBillAddressLine1.Text.Length > 0 && this.txtBillCity.Text.Length > 0 && this.txtBillState.Text.Length > 0 && this.txtBillZip5.Text.Length > 0))
                                    );
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnCommandClick(object sender,System.EventArgs e) {
            //Event handler for button selection
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch (btn.Name) {
                    case "btnClose":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                       break;
                    case "btnVerifyAddress":
                        //Validate\correct the address
                        dlgVerifyAddress verify = new dlgVerifyAddress();
                        if(verify.VerifyAddress(this.txtName.Text, this.txtAddressLine1.Text, this.txtAddressLine2.Text, this.txtCity.Text, this.txtState.Text, this.txtZip5.Text, this.txtZip4.Text)) {
                            this.txtName.Text = verify.FirmName;
                            this.txtAddressLine1.Text = verify.AddressLine1;
                            this.txtAddressLine2.Text = verify.AddressLine2;
                            this.txtCity.Text = verify.City;
                            this.txtState.Text = verify.State;
                            this.txtZip5.Text = verify.Zip5;
                            this.txtZip4.Text = verify.Zip4;

                            this.mAddressValid = true;
                            OnValidateForm(null, EventArgs.Empty);
                            MessageBox.Show("Address is valid.", App.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;
                    case "btnOk":
                        //this.mClient.ID
                        this.mClient.Name = this.txtName.Text;
                        this.mClient.AddressLine1 = this.txtAddressLine1.Text;
                        this.mClient.AddressLine2 = "";
                        this.mClient.City = this.txtCity.Text;
                        this.mClient.State = this.txtState.Text;
                        this.mClient.Zip = this.txtZip5.Text;
                        this.mClient.Zip4 = "";
                        this.mClient.ContactName = this.txtContactName.Text;
                        this.mClient.ContactPhone = this.mskContactPhone.Text;
                        this.mClient.ContactEmail = this.txtContactEmail.Text;
                        this.mClient.CorporateName = this.txtCorpName.Text;
                        this.mClient.CorporateAddressLine1 = this.txtCorpAddressLine1.Text;
                        this.mClient.CorporateAddressLine2 = "";
                        this.mClient.CorporateCity = this.txtCorpCity.Text;
                        this.mClient.CorporateState = this.txtCorpState.Text;
                        this.mClient.CorporateZip = this.txtCorpZip5.Text;
                        this.mClient.CorporateZip4 = "";
                        this.mClient.TaxID = this.txtTaxID.Text;
                        if(this.mClient.ID == 0) {
                            this.mClient.BillingAddressLine1 = this.txtBillAddressLine1.Text;
                            this.mClient.BillingAddressLine2 = "";
                            this.mClient.BillingCity = this.txtBillCity.Text;
                            this.mClient.BillingState = this.txtBillState.Text;
                            this.mClient.BillingZip = this.txtBillZip5.Text;
                            this.mClient.BillingZip4 = "";
                        }
                        this.mClient.UserID = Environment.UserName;
                        this.mClient.SalesRepClientNumber = this.txtSalesRepClientNumber.Text;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

    }
}
