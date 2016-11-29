using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix.Enterprise.USPS {
    //
    public partial class dlgVerifyAddress : Form {
        //Members
        private string mFirmName = "",mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip5 = "",mZip4 = "";

        //Interface
        public dlgVerifyAddress() {
            //Constructor
            InitializeComponent();
        }
        public string FirmName { get { return this.mFirmName; } }
        public string AddressLine1 { get { return this.mAddressLine1; } }
        public string AddressLine2 { get { return this.mAddressLine2; } }
        public string City { get { return this.mCity; } }
        public string State { get { return this.mState; } }
        public string Zip5 { get { return this.mZip5; } }
        public string Zip4 { get { return this.mZip4; } }
        public bool VerifyAddress(string firmName,string addressLine1,string addressLine2,string city,string state,string zip5,string zip4) {
            //Set user services
            bool verified = false;
            try {
                //
                AddressValidateResponse avr = EnterpriseUSPSGateway.VerifyAddress(firmName, addressLine1, addressLine2, city, state, zip5, zip4);
                if (avr.Error.Rows.Count > 0) {
                    //Bad address or syntax
                    string error = (!avr.Error[0].IsNumberNull() ? avr.Error[0].Number : "") + " " + (!avr.Error[0].IsSourceNull() ? avr.Error[0].Source : "") + "\r\n" + (!avr.Error[0].IsDescriptionNull() ? avr.Error[0].Description : "");
                    MessageBox.Show(error,App.Title,MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
                else if (avr.Address.Rows.Count > 0) {
                    //Does it match the request?
                    string srcAddress = addressLine2.Trim().ToLower() + addressLine1.Trim().ToLower();
                    string uspsAddress = (!avr.Address[0].IsAddress1Null() ? avr.Address[0].Address1.Trim().ToLower() : "") + (!avr.Address[0].IsAddress2Null() ? avr.Address[0].Address2.Trim().ToLower() : "");
                    bool match = (srcAddress == uspsAddress) &&
                                 (city.Trim().ToLower() == (!avr.Address[0].IsCityNull() ? avr.Address[0].City.Trim().ToLower() : "")) &&
                                 (state.Trim().ToLower() == (!avr.Address[0].IsStateNull() ? avr.Address[0].State.Trim().ToLower() : "")) &&
                                 (zip5.Trim().ToLower() == (!avr.Address[0].IsZip5Null() ? avr.Address[0].Zip5.Trim().ToLower() : ""));
                    if (match && avr.Address[0].IsReturnTextNull()) {
                        //Use scrubbed USPS address
                        this.mFirmName = firmName;
                        this.mAddressLine1 = !avr.Address[0].IsAddress2Null() ? avr.Address[0].Address2 : "";
                        this.mAddressLine2 = !avr.Address[0].IsAddress1Null() ? avr.Address[0].Address1 : "";
                        this.mCity = !avr.Address[0].IsCityNull() ? avr.Address[0].City : "";
                        this.mState = !avr.Address[0].IsStateNull() ? avr.Address[0].State : "";
                        this.mZip5 = !avr.Address[0].IsZip5Null() ? avr.Address[0].Zip5 : "";
                        this.mZip4 = !avr.Address[0].IsZip4Null() ? avr.Address[0].Zip4 : "";
                    }
                    else {
                        //Let user choose
                        this.txtName.Text = firmName;
                        this.txtAddressLine1.Text = addressLine1;
                        this.txtAddressLine2.Text = addressLine2;
                        this.txtCity.Text = city;
                        this.txtState.Text = state;
                        this.txtZip5.Text = zip5;
                        this.txtZip4.Text = zip4;

                        this.txtUSPSName.Text = firmName;
                        this.txtUSPSAddressLine1.Text = !avr.Address[0].IsAddress2Null() ? avr.Address[0].Address2 : "";
                        this.txtUSPSAddressLine2.Text = !avr.Address[0].IsAddress1Null() ? avr.Address[0].Address1 : "";
                        this.txtUSPSCity.Text = !avr.Address[0].IsCityNull() ? avr.Address[0].City : "";
                        this.txtUSPSState.Text = !avr.Address[0].IsStateNull() ? avr.Address[0].State : "";
                        this.txtUSPSZip5.Text = !avr.Address[0].IsZip5Null() ? avr.Address[0].Zip5 : "";
                        this.txtUSPSZip4.Text = !avr.Address[0].IsZip4Null() ? avr.Address[0].Zip4 : "";
                        
                        this.lblMessage.Text = !avr.Address[0].IsReturnTextNull() ? avr.Address[0].ReturnText : "";
                        
                        this.ShowDialog();
                    }
                    verified = true;
                }
                else {
                    //Not verified
                    MessageBox.Show("The adddress could not be verified by the US Postal Service.",App.Title,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex) { App.ReportError(ex, true); }
            return verified;
        }
        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
            try {
            }
            catch(Exception ex) { App.ReportError(ex, true); }
        }
        private void OnCommandClick(object sender,System.EventArgs e) {
            //Event handler for button selection
            this.Cursor = Cursors.WaitCursor;
            try {
                //USPS services
                Button btn = (Button)sender;
                switch (btn.Name) {
                    case "btnSrc":
                        this.DialogResult = DialogResult.OK;
                        this.mFirmName = this.txtName.Text;
                        this.mAddressLine1 = this.txtAddressLine1.Text;
                        this.mAddressLine2 = this.txtAddressLine2.Text;
                        this.mCity = this.txtCity.Text;
                        this.mState = this.txtState.Text;
                        this.mZip5 = this.txtZip5.Text;
                        this.mZip4 = this.txtZip4.Text;
                        this.Close();
                        break;
                    case "btnUSPS":
                        this.DialogResult = DialogResult.OK;
                        this.mFirmName = this.txtUSPSName.Text;
                        this.mAddressLine1 = this.txtUSPSAddressLine1.Text;
                        this.mAddressLine2 = this.txtUSPSAddressLine2.Text;
                        this.mCity = this.txtUSPSCity.Text;
                        this.mState = this.txtUSPSState.Text;
                        this.mZip5 = this.txtUSPSZip5.Text;
                        this.mZip4 = this.txtUSPSZip4.Text;
                        this.Close();
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
