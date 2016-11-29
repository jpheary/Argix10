using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Security;

namespace Argix.Freight {
    //
    public partial class dlgLoadTender:Form {
        //Members
        private DispatchDataset.LoadTenderLogTableRow mEntry=null;
        private bool mMapLoaded = false;
        private const int PALLETS_MAX=10, WEIGHT_MAX=10000;


        //Interface
        public dlgLoadTender(DispatchDataset.LoadTenderLogTableRow entry, bool isTemplate = false) {
            try {
                InitializeComponent();
                this.wbAddress.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
                this.mEntry = entry;
                if(this.mEntry.IsIDNull()) this.mEntry.ID = 0;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Get lists
                this.mClients.Merge(FreightGateway.GetClients());
                this.mVendors.Merge(FreightGateway.GetVendors());

                //Load controls
                this.Text = "Load Tender" + "(" + (!this.mEntry.IsIDNull() ? this.mEntry.ID.ToString() : "New") + ")";
                this.lblID.Text = !this.mEntry.IsIDNull() ? this.mEntry.ID.ToString("00000000") : "";
                this.dtpScheduleDate.Value = !this.mEntry.IsScheduleDateNull() ? this.mEntry.ScheduleDate : DateTime.Today;

                this.cboClient.SelectedValue = !this.mEntry.IsClientNumberNull() ? this.mEntry.ClientNumber : "141";
                OnClientSelectedIndexChanged(null, EventArgs.Empty);
                
                if(!this.mEntry.IsVendorNumberNull() && this.mEntry.VendorNumber.Trim().Length > 0) 
                    this.cboShipper.SelectedValue = this.mEntry.VendorNumber;
                else {
                    this.cboShipper.SelectedIndex = -1;
                    this.cboShipper.Text = !this.mEntry.IsVendorNameNull() ? this.mEntry.VendorName : "";
                    this.txtAddressLine1.Text = !this.mEntry.IsVendorAddressLine1Null() ? this.mEntry.VendorAddressLine1 : "";
                    this.txtAddressLine2.Text = !this.mEntry.IsVendorAddressLine2Null() ? this.mEntry.VendorAddressLine2 : "";
                    this.txtCity.Text = !this.mEntry.IsVendorCityNull() ? this.mEntry.VendorCity : "";
                    this.txtState.Text = !this.mEntry.IsVendorStateNull() ? this.mEntry.VendorState : "";
                    this.txtZip.Text = !this.mEntry.IsVendorZipNull() ? this.mEntry.VendorZip : "";
                    this.txtZip4.Text = !this.mEntry.IsVendorZip4Null() ? this.mEntry.VendorZip4 : "";
                }
                OnShipperAddressChanged(null,EventArgs.Empty);

                this.txtContactName.Text = !this.mEntry.IsContactNameNull() ? this.mEntry.ContactName : "";
                this.txtContactPhone.Text = !this.mEntry.IsContactPhoneNull() ? this.mEntry.ContactPhone : "";
                this.txtContactEmail.Text = !this.mEntry.IsContactEmailNull() ? this.mEntry.ContactEmail : "";
                this.mtbOpen.Text = !this.mEntry.IsWindowOpenNull() ? this.mEntry.WindowOpen.ToString().PadLeft(4,'0') : "";
                this.mtbClose.Text = !this.mEntry.IsWindowCloseNull() ? this.mEntry.WindowClose.ToString().PadLeft(4,'0') : "";

                this.txtDescription.Text = !this.mEntry.IsDescriptionNull() ? this.mEntry.Description : "";
                
                this.txtQuantity.Text = !this.mEntry.IsAmountNull() ? this.mEntry.Amount.ToString() : "0";
                if (!this.mEntry.IsAmountTypeNull()) this.cboContainer.Text = this.mEntry.AmountType; else this.cboContainer.SelectedIndex = 0;
                this.txtWeight.Text = !this.mEntry.IsWeightNull() ? this.mEntry.Weight.ToString() : "0";
                this.chkFullTrailer.Checked = !this.mEntry.IsIsFullTrailerNull() ? this.mEntry.IsFullTrailer : false;
                this.txtComments.Text = !this.mEntry.IsCommentsNull() ? this.mEntry.Comments : "";

                this.cboClient.Focus();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnClientSelectedIndexChanged(object sender,EventArgs e) {
            //Event handler for client SelectedIndexChanged event
            this.Cursor = Cursors.WaitCursor;
            try {
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnShipperTextChanged(object sender,EventArgs e) {
            //Event handler for shipper TextChanged event- clear bound fields when user clears shipper text
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboShipper.SelectedIndex == -1) {
                    this.txtAddressLine1.Text = "";
                    this.txtAddressLine2.Text = "";
                    this.txtCity.Text = "";
                    this.txtState.Text = "";
                    this.txtZip.Text = "";
                    this.txtZip4.Text = "";
                    //this.txtContactName.Text = "";
                    //this.txtContactPhone.Text = "";
                    //this.txtContactEmail.Text = "";
                    //this.mtbOpen.Text = this.mtbClose.Text = "";
                }
                else if(this.cboShipper.SelectedValue != null) {
                    string vendorNumber = this.cboShipper.SelectedValue.ToString();
                    DispatchDataset.VendorTableRow vendor = FreightGateway.GetVendor(vendorNumber);
                    if(vendor != null) {
                        this.txtAddressLine1.Text = vendor.AddressLine1;
                        this.txtAddressLine2.Text = !vendor.IsAddressLine2Null() ? vendor.AddressLine2 : "";
                        this.txtCity.Text = vendor.City;
                        this.txtState.Text = vendor.State;
                        this.txtZip.Text = vendor.Zip;
                        this.txtZip4.Text = !vendor.IsZip4Null() ? vendor.Zip4 : "";
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
        private void OnShipperAddressChanged(object sender,EventArgs e) {
            //Event handler for shipper address (text) changed event
            try {
                //Display the address in a map
                if(this.txtAddressLine1.Text.Length > 0 && this.txtCity.Text.Length > 0 && this.txtState.Text.Length == 2 && this.txtZip.Text.Length == 5) {
                    if(this.mMapLoaded && this.wbAddress.Document != null) {
                        string address = this.txtAddressLine1.Text + " " + this.txtCity.Text + ", " + this.txtState.Text + " " + this.txtZip.Text;
                        this.wbAddress.Document.InvokeScript("MapLocation", new object[] { address });
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnWindowValidating(object sender,CancelEventArgs e) {
            //Event handler for delivery window validating
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                string value = mtb.Text.Replace(":","").Trim();

                if(value == String.Empty) return;

                int hour=-1;
                if(int.TryParse(value.Substring(0,2),out hour) == false) { e.Cancel = true; return; }
                else { if(hour < 0 || hour > 23) { e.Cancel = true; return; } }

                int minute=-1;
                if(int.TryParse(value.Substring(2,2),out minute) == false) { e.Cancel = true; return; }
                else { if(minute < 0 || minute > 59) { e.Cancel = true; return; } }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) { this.mMapLoaded = true; OnShipperAddressChanged(null,EventArgs.Empty); }
        private void OnValidateForm(object sender,EventArgs e) {
            //Set user services
            try {
                //Set services
                bool access = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || 
                                RoleServiceGateway.IsBBBClerk;

                this.dtpScheduleDate.Enabled = true;
                this.cboClient.Enabled = true;
                this.cboShipper.Enabled = true;
                this.txtAddressLine1.Enabled = true;
                this.txtAddressLine2.Enabled = true;
                this.txtCity.Enabled = true;
                this.txtState.Enabled = true;
                this.txtZip.Enabled = true;
                this.txtZip4.Enabled = true;
                this.txtContactName.Enabled = true;
                this.txtContactPhone.Enabled = true;
                this.txtContactEmail.Enabled = true;
                this.mtbOpen.Enabled = true;
                this.mtbClose.Enabled = true;
                this.txtDescription.Enabled = true;
                this.txtQuantity.Enabled = true;
                this.cboContainer.Enabled = true;
                this.txtWeight.Enabled = true;
                this.chkFullTrailer.Enabled = true;
                this.txtComments.Enabled = true;

                bool cancelled = !this.mEntry.IsCancelledNull() && this.mEntry.Cancelled.CompareTo(DateTime.MinValue) > 0;
                this.btnOk.Enabled = (access && !cancelled &&
                                        this.cboClient.Text.Trim().Length > 0 && 
                                        this.cboShipper.Text.Trim().Length > 0 &&
                                        this.txtQuantity.Text.Trim().Length > 0 &&
                                        this.cboContainer.Text.Trim().Length > 0 &&
                                        this.txtWeight.Text.Trim().Length > 0);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnClick(object sender,EventArgs e) {
            //Event handler for button click
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close(); 
                        break;
                    case "btnOk": 
                        //Validate servicing terminal
                        if(this.mEntry.IsIDNull()) {
                             this.mEntry.Created = DateTime.Now;
                             this.mEntry.CreateUserID = Environment.UserName;
                         }
                         this.mEntry.ScheduleDate = this.dtpScheduleDate.Value;
                        if(this.cboClient.SelectedValue != null)
                            this.mEntry.ClientNumber = this.cboClient.SelectedValue.ToString();
                        else
                            this.mEntry.SetClientNumberNull();
                        this.mEntry.Client = this.cboClient.Text;

                        if(this.cboShipper.SelectedValue != null)
                            this.mEntry.VendorNumber = this.cboShipper.SelectedValue.ToString();
                        else
                            this.mEntry.SetVendorNumberNull();
                        this.mEntry.VendorName = this.cboShipper.Text;
                        //TODO
                        this.mEntry.VendorAddressLine1 = this.txtAddressLine1.Text.Trim();
                        this.mEntry.VendorAddressLine2 = this.txtAddressLine2.Text.Trim();
                        this.mEntry.VendorCity = this.txtCity.Text.Trim();
                        this.mEntry.VendorState = this.txtState.Text.Trim();
                        this.mEntry.VendorZip = this.txtZip.Text.Trim();
                        this.mEntry.VendorZip4 = this.txtZip4.Text.Trim();

                        if(this.txtContactName.Text.Trim().Length > 0) this.mEntry.ContactName = this.txtContactName.Text.Trim();
                        if(this.txtContactPhone.Text.Trim().Length > 0) this.mEntry.ContactPhone = this.txtContactPhone.Text.Trim();
                        if(this.txtContactEmail.Text.Trim().Length > 0) this.mEntry.ContactEmail = this.txtContactEmail.Text.Trim();

                        this.mEntry.WindowOpen = this.mtbOpen.Text.Length > 0 ? short.Parse(this.mtbOpen.Text) : (short)0;
                        this.mEntry.WindowClose = this.mtbClose.Text.Length > 0 ? short.Parse(this.mtbClose.Text) : (short)0;

                        this.mEntry.Description = this.txtDescription.Text.Trim();

                        this.mEntry.Amount = int.Parse(this.txtQuantity.Text);
                        this.mEntry.AmountType = this.cboContainer.Text;
                        this.mEntry.Weight = this.txtWeight.Text.Trim().Length > 0 ? int.Parse(this.txtWeight.Text) : 0;
                        this.mEntry.Comments = this.txtComments.Text;

                        this.mEntry.LastUpdated = DateTime.Now;
                        this.mEntry.UserID = Environment.UserName;

                        this.DialogResult = DialogResult.OK;
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
