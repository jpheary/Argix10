using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix.Freight {
    //
    public partial class dlgLoadTenderQuote : Form {
        //Members
        private LTLLoadTenderQuote mQuote = null;

        //Interface
        public dlgLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Constructor
            try {
                InitializeComponent();
                this.mQuote = quote;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormLoad(object sender, System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.mQuote.ID > 0) {
                    //Existing quote- load
                    this.Text = "Load Tender Quote# " + this.mQuote.ID.ToString();
                    this.txtDescription.Text = this.mQuote.Description.Trim();
                    this.txtBroker.Text = this.mQuote.BrokerName.Trim();
                    this.txtContactName.Text = this.mQuote.ContactName.Trim();
                    this.mskContactPhone.Text = this.mQuote.ContactPhone.Trim();
                    this.txtContactEmail.Text = this.mQuote.ContactEmail.Trim();
                    this.txtComments.Text = this.mQuote.Comments.Trim();
                }
                else {
                    //New quote- initialize
                    this.Text = "New Load Tender Quote";
                    this.txtDescription.Text = "";
                    this.txtBroker.Text = "";
                    this.txtContactName.Text = "";
                    this.mskContactPhone.Text = "";
                    this.txtContactEmail.Text = "";
                    this.txtComments.Text = "";
                    this.txtTotal.Text = "";
                }
                this.txtShipDate.Text = this.mQuote.LTLQuote.ShipDate.ToString("MM/dd/yyyy");
                this.txtOrigin.Text = this.mQuote.LTLQuote.OriginCity.Trim() + ", " + this.mQuote.LTLQuote.OriginState.Trim() + " " + this.mQuote.LTLQuote.OriginZip.Trim();
                this.txtDestination.Text = this.mQuote.LTLQuote.DestinationCity.Trim() + ", " + this.mQuote.LTLQuote.DestinationState.Trim() + " " + this.mQuote.LTLQuote.DestinationZip.Trim();
                this.txtLoad.Text = "";
                this.txtPallets.Text = this.mQuote.LTLQuote.Pallets.ToString();
                this.txtWeight.Text = this.mQuote.LTLQuote.Weight.ToString();
                this.txtRate.Text = this.mQuote.LTLQuote.PalletRate.ToString();
                this.txtFSC.Text = this.mQuote.LTLQuote.FuelSurcharge.ToString();
                this.txtAccessorials.Text = this.mQuote.LTLQuote.AccessorialCharge.ToString();
                this.txtInsurance.Text = this.mQuote.LTLQuote.InsuranceCharge.ToString();
                this.txtTSC.Text = this.mQuote.LTLQuote.TollCharge.ToString();
                this.txtTotal.Text = this.mQuote.LTLQuote.TotalCharge.ToString();
                this.chkApproved.Checked = this.mQuote.Approved != DateTime.MinValue;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnTotalQuoteKeyUp(object sender, KeyEventArgs e) {
            //
            try {
                if(e.KeyCode == Keys.Enter) overrideQuote();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnTotalQuoteLeave(object sender, EventArgs e) {
            //
            try {
                //Update quote
                overrideQuote();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnApprovedChecked(object sender, EventArgs e) {
            //Event handler for pparoved check changed
            try {
                //New quotes can be approved (or they could change their mind)
                this.mQuote.Approved = this.chkApproved.Checked ? DateTime.Now : DateTime.MinValue;
                this.mQuote.ApprovedBy = this.chkApproved.Checked ? Environment.UserName : "";
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnValidateForm(object sender, EventArgs e) {
            //Set user services
            try {
                //Set services
                this.txtTotal.Enabled = this.mQuote.Approved == DateTime.MinValue;
                this.chkApproved.Enabled = this.mQuote.ID == 0 || (this.mQuote.ID > 0 && this.mQuote.Approved == DateTime.MinValue);
                this.btnCancel.Enabled = true;
                this.btnOk.Enabled = this.txtDescription.Text.Trim().Length > 0 &&
                                     this.txtBroker.Text.Trim().Length > 0 &&
                                     this.txtContactName.Text.Trim().Length > 0 &&
                                     (this.mskContactPhone.Text.Length > 0 || this.txtContactEmail.Text.Trim().Length > 0);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnCommandClick(object sender, System.EventArgs e) {
            //Event handler for button selection
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":
                        this.DialogResult = DialogResult.Cancel;
                        break;
                    case "btnOk":
                        this.DialogResult = DialogResult.OK;
                        this.mQuote.Description = this.txtDescription.Text.Trim();
                        this.mQuote.BrokerName = this.txtBroker.Text.Trim();
                        this.mQuote.ContactName = this.txtContactName.Text.Trim();
                        this.mQuote.ContactPhone = this.mskContactPhone.Text.Trim();
                        this.mQuote.ContactEmail = this.txtContactEmail.Text.Trim();
                        this.mQuote.Comments = this.txtComments.Text.Trim();
                        break;
                }
                this.Close();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

        private void overrideQuote() {
            //
            try {
                if(this.mQuote != null) {
                    //Validate total charge does not go below the total charge minimum
                    decimal totalCharge = decimal.Parse(this.txtTotal.Text);
                    if(totalCharge >= this.mQuote.TotalChargeMin) {
                        //Update the quote
                        this.mQuote.LTLQuote.TotalCharge = totalCharge;
                        LTLQuote2 quote = FreightGateway.CreateQuoteForSalesRep(this.mQuote.LTLQuote);
                        this.txtRate.Text = quote.PalletRate.ToString();
                        this.txtFSC.Text = quote.FuelSurcharge.ToString();
                    }
                    else {
                        //Restore the quote
                        this.txtTotal.Text = this.mQuote.LTLQuote.TotalCharge.ToString();
                        this.txtTotal.Focus();
                        MessageBox.Show("The total charge cannot be less than " + this.mQuote.TotalChargeMin.ToString(), App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
    }
}
