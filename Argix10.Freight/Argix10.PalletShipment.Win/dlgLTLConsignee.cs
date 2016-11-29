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
    public partial class dlgLTLConsignee:Form {
        //Members
        private LTLConsignee2 mConsignee = null;
        private bool mAddressValid=false, mMapLoaded = false;

        //Interface
        public dlgLTLConsignee(LTLConsignee2 consignee) {
            //Constructor
            try {
                InitializeComponent();
                this.mConsignee = consignee;
                if(this.mConsignee == null) throw new ApplicationException("Consignee cannot be null.");
                this.wbMap.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.mConsignee.Number > 0) {
                    //Existing consignee
                    this.mAddressValid = true;  //No validation for existing consignee
                    this.Text = "Consignee# " + this.mConsignee.Number.ToString();
                    this.txtName.Text = this.mConsignee.Name.Trim();
                    this.txtAddressLine1.Text = this.mConsignee.AddressLine1.Trim();
                    this.txtAddressLine2.Text = this.mConsignee.AddressLine2.Trim();
                    this.txtCity.Text = this.mConsignee.City.Trim();
                    this.txtState.Text = this.mConsignee.State.Trim();
                    this.txtZip5.Text = this.mConsignee.Zip.Trim();
                    this.txtZip4.Text = this.mConsignee.Zip4.Trim();
                    this.mskWindowStart.Text = this.mConsignee.WindowTimeStart.ToString("HH:mm");
                    this.mskWindowEnd.Text = this.mConsignee.WindowTimeEnd.ToString("HH:mm");
                    this.txtContactName.Text = this.mConsignee.ContactName.Trim();
                    this.mskContactPhone.Text = this.mConsignee.ContactPhone.Trim();
                    this.txtContactEmail.Text = this.mConsignee.ContactEmail.Trim();
                    this.chkActive.Checked = true;
                }
                else {
                    //New client
                    this.Text = "New Consignee";
                    this.txtName.Text = this.txtAddressLine1.Text = this.txtAddressLine2.Text = this.txtCity.Text = this.txtState.Text = this.txtZip5.Text = this.txtZip4.Text = "";
                    this.mskWindowStart.Text = "09:00";
                    this.mskWindowEnd.Text = "17:00";
                    this.txtContactName.Text = this.mskContactPhone.Text = this.txtContactEmail.Text = "";
                    this.chkActive.Checked = false;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnZipChanged(object sender, EventArgs e) {
            //Validate consignee zip as deliverable (new consignees only)
            try {
                string zipCode = this.txtZip5.Text;
                if(this.mConsignee.Number == 0 && zipCode.Trim().Length == 5) {
                    ServiceLocation location = FreightGateway.ReadServiceLocation(zipCode);
                    if(location == null) {
                        this.txtCity.Text = this.txtState.Text = this.txtZip5.Text = "";
                        this.txtZip5.Focus();
                        MessageBox.Show(zipCode + " is currently not supported for delivery.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else {
                        this.txtCity.Text = location.City.Trim();
                        this.txtState.Text = location.State.Trim();
                        this.txtName.Focus();
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Information); }
            finally { OnAddressChanged(null, EventArgs.Empty); }
        }
        private void OnAddressChanged(object sender, EventArgs e) {
            //Event handler for address (text) changed event
            try {
                //Display the address in a map
                if(this.txtAddressLine1.Text.Length > 0 && this.txtCity.Text.Length > 0 && this.txtState.Text.Length == 2 && this.txtZip5.Text.Length == 5) {
                    if(this.mMapLoaded && this.wbMap.Document != null) {
                        string address = this.txtAddressLine1.Text + " " + this.txtCity.Text + ", " + this.txtState.Text + " " + this.txtZip5.Text;
                        this.wbMap.Document.InvokeScript("MapLocation", new object[] { address });
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnWindowValidating(object sender, CancelEventArgs e) {
            //Event handler for start/end hours of operation MaskedTextbox::Validating event
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                DateTime time;
                switch(mtb.Name) {
                    case "mskWindowStart":
                        e.Cancel = !DateTime.TryParse(this.mskWindowStart.Text, out time);
                        break;
                    case "mskWindowEnd":
                        e.Cancel = !DateTime.TryParse(this.mskWindowEnd.Text, out time);
                        break;
                }
                if(e.Cancel) MessageBox.Show("Invalid window time; please enter military time (i.e. 00:00 - 23:59).", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnWindowValidated(object sender, EventArgs e) {
            //Event handler for start/end hours of operation MaskedTextbox::Validated event
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                DateTime start, end;
                if(DateTime.TryParse(this.mskWindowStart.Text, out start) && DateTime.TryParse(this.mskWindowEnd.Text, out end)) {
                    //Check crossovers
                    switch(mtb.Name) {
                        case "mskWindowStart":
                            //Validate not after end
                            if(DateTime.Compare(start, end) > 0) {
                                this.mskWindowStart.Text = end.ToString("HH:mm");
                                MessageBox.Show("Window start time cannot later than the window end time.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                        case "mskWindowEnd":
                            //Validate not before start
                            if(DateTime.Compare(start, end) > 0) {
                                this.mskWindowEnd.Text = start.ToString("HH:mm");
                                MessageBox.Show("Window end time cannot be earlier than the window start time.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnMapDocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e) { this.mMapLoaded = true; OnAddressChanged(null, EventArgs.Empty); }
        private void OnValidateForm(object sender, EventArgs e) {
            //Set user services
            try {
                //Set services
                this.txtZip5.Enabled = this.txtState.Enabled = this.mConsignee.Number == 0;
                this.chkActive.Enabled = false;
                this.btnVerifyAddress.Enabled = true;
                this.btnOk.Enabled = (  this.mAddressValid && 
                                        this.txtName.Text.Length > 0 && this.txtAddressLine1.Text.Length > 0 && this.txtCity.Text.Length > 0 && this.txtState.Text.Length == 2 && this.txtZip5.Text.Length == 5 &&
                                        this.txtContactName.Text.Length > 0 && this.mskContactPhone.Text.Length > 0
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
                        //this.mConsignee.Number
                        this.mConsignee.Name = this.txtName.Text;
                        this.mConsignee.AddressLine1 = this.txtAddressLine1.Text;
                        this.mConsignee.AddressLine2 = this.txtAddressLine2.Text;
                        this.mConsignee.City = this.txtCity.Text;
                        this.mConsignee.State = this.txtState.Text;
                        this.mConsignee.Zip = this.txtZip5.Text;
                        this.mConsignee.Zip4 = this.txtZip4.Text;
                        this.mConsignee.WindowTimeStart = DateTime.Parse("01-01-2000 " + this.mskWindowStart.Text);
                        this.mConsignee.WindowTimeEnd = DateTime.Parse("01-01-2000 " + this.mskWindowEnd.Text);
                        this.mConsignee.ContactName = this.txtContactName.Text;
                        this.mConsignee.ContactPhone = this.mskContactPhone.Text;
                        this.mConsignee.ContactEmail = this.txtContactEmail.Text;
                        this.mConsignee.UserID = Environment.UserName;
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
