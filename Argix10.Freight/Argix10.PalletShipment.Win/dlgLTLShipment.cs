using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Enterprise;

namespace Argix.Freight {
    //
    public partial class dlgLTLShipment:Form {
        //Members
        private LTLShipment2 mShipment = null;
        LTLQuote2 mQuote = null;
        private bool mPreQuoted = false;        //Flag that quote was passed in
        private TrackingItems mTrackingItems = null;
        private System.Windows.Forms.ToolTip mToolTip = null;

        private int MAX_PALLETS = 5;
        private decimal MAX_WEIGHT = 2000;
        private decimal MAX_INSURANCE = 10000;

        //Interface
        public dlgLTLShipment(LTLShipment2 shipment) {
            //Constructor
            try {
                InitializeComponent();
                this.mToolTip = new System.Windows.Forms.ToolTip();
                MAX_PALLETS = App.Config.PalletsMax;
                MAX_WEIGHT = App.Config.WeightMax;
                MAX_INSURANCE = App.Config.InsuranceMax;
                this.mShipment = shipment;
                if(this.mShipment == null) throw new ApplicationException("Shipment cannot be null.");
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        public dlgLTLShipment(LTLShipment2 shipment, LTLQuote2 quote) : this(shipment) {
            this.mQuote = quote; 
            this.mPreQuoted = true;
            if(this.mQuote == null) throw new ApplicationException("Quote cannot be null.");
        }
        private void OnFormLoad(object sender, System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                #region Grid customizations from normal layout (to support cell editing)
                this.grdPallets.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                this.grdPallets.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdPallets.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdPallets.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdPallets.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                this.grdPallets.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
                this.grdPallets.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdPallets.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdPallets.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdPallets.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                this.grdPallets.DisplayLayout.Bands[0].Columns["TrackingNumber"].CellActivation = Activation.NoEdit;
                this.grdPallets.DisplayLayout.Bands[0].Columns["Weight"].CellActivation = Activation.AllowEdit;
                this.grdPallets.DisplayLayout.Bands[0].Columns["InsuranceValue"].CellActivation = Activation.AllowEdit;
                this.grdPallets.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdPallets.DisplayLayout.Bands[0].Columns["TrackingNumber"].SortIndicator = SortIndicator.Ascending;
                #endregion
                #region Set tooltips
                this.mToolTip.InitialDelay = 500;
                this.mToolTip.AutoPopDelay = 3000;
                this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;		//Even when form is inactve
                this.mToolTip.SetToolTip(this.btnNewShipper, "Create a new shipper...");
                this.mToolTip.SetToolTip(this.btnNewConsignee, "Create a new consignee..");
                #endregion
                this.grdPallets.Font = this.grdTracking.Font = this.Font;

                //Load clients
                this.mClients.Merge(FreightGateway.ReadLTLClientList());

                //Set shipment data
                if(this.mShipment.ShipmentNumber.Trim().Length > 0) {
                    //Existing shipment
                    this.Text = "Shipment# " + this.mShipment.ShipmentNumber;
                    this.lblCreated.Text = this.mShipment.CreatedUserID + " on " + this.mShipment.Created.ToString("MM/dd/yyyy hh:mm tt");
                    if(this.mShipment.Cancelled.CompareTo(DateTime.MinValue) > 0) {
                        this.lblCancelled.Text = this.mShipment.Cancelled.ToString("MM/dd/yyyy hh:mm tt");
                        this._lblCancelled.Visible = this.lblCancelled.Visible = true;
                    }
                    this.dtpShipDate.MinDate = this.mShipment.ShipDate.CompareTo(getNextValidShipDate()) > 0 ? DateTime.Today : this.mShipment.ShipDate;
                    this.dtpShipDate.MaxDate = DateTime.Today.AddDays(30);
                    this.dtpShipDate.Value = this.mShipment.ShipDate;
                    this.cboClient.SelectedValue = this.mShipment.ClientNumber;
                    OnClientChanged(null, EventArgs.Empty);
                    this.cboShipper.SelectedValue = this.mShipment.ShipperNumber;
                    OnShipperSelected(null, EventArgs.Empty);
                    this.chkShipperLiftGate.Checked = this.mShipment.LiftGateOrigin;
                    this.chkShipperInsidePickup.Checked = this.mShipment.InsidePickup;
                    this.chkShipperAppt.Checked = this.mShipment.PickupAppointmentWindowTimeStart.CompareTo(DateTime.MinValue) > 0;
                    this.chkShipperSameDay.Checked = this.mShipment.SameDayPickup;
                    OnShipperApptChecked(null, EventArgs.Empty);
                    this.cboConsignee.SelectedValue = this.mShipment.ConsigneeNumber;
                    OnConsigneeSelected(null, EventArgs.Empty);
                    this.chkConsigneeLiftGate.Checked = this.mShipment.LiftGateDestination;
                    this.chkConsigneeInsideDelivery.Checked = this.mShipment.InsideDelivery;
                    this.chkConsigneeAppt.Checked = this.mShipment.DeliveryAppointmentWindowTimeStart.CompareTo(DateTime.MinValue) > 0;
                    OnConsigneeApptChecked(null, EventArgs.Empty);
                    this.txtContactName.Text = this.mShipment.ContactName.Trim();
                    this.mskContactPhone.Text = this.mShipment.ContactPhone;
                    this.mPallets.DataSource = this.mShipment.Items;
                    this.txtBOLNumber.Text = this.mShipment.BLNumber;
                    this.txtPickupID.Text = this.mShipment.PickupID.ToString();
                    this.txtPickupDate.Text = this.mShipment.PickupDate.CompareTo(DateTime.MinValue) > 0 ? this.mShipment.PickupDate.ToString("MM/dd/yyyy") : "";
                    this.mTrackingItems = EnterpriseGateway.TrackShipment(this.mShipment.ShipmentNumber);
                    if(this.grdPallets.Rows.Count > 0) this.grdPallets.Rows[0].Selected = true;
                    OnPalletGridSelected(null, null);

                    this.txtPallets.Text = this.mShipment.Pallets.ToString("#");
                    this.txtWeight.Text = this.mShipment.Weight.ToString("#");
                    this.txtRate.Text = "$" + this.mShipment.PalletRate.ToString("#.00");
                    this.txtFSC.Text = "$" + this.mShipment.FuelSurcharge.ToString("#.00");
                    this.txtAccessorials.Text = "$" + this.mShipment.AccessorialCharge.ToString("#.00");
                    this.txtInsurance.Text = "$" + this.mShipment.InsuranceCharge.ToString("#.00");
                    this.txtTSC.Text = "$" + this.mShipment.TollCharge.ToString("#.00");
                    this.txtTotal.Text = "$" + this.mShipment.TotalCharge.ToString("#.00");
                }
                else {
                    //New shipment
                    this.Text = "New Shipment";
                    this.lblCreated.Text = Environment.UserName;
                    this.dtpShipDate.MinDate = DateTime.Today.AddDays(-7);
                    this.dtpShipDate.MaxDate = DateTime.Today.AddDays(30);
                    this.dtpShipDate.Value = this.mPreQuoted ? this.mQuote.ShipDate : this.mShipment.ShipDate;
                    if(this.mShipment.ClientNumber.Length > 0) this.cboClient.SelectedValue = this.mShipment.ClientNumber; else this.cboClient.SelectedIndex = 0;
                    OnClientChanged(null, EventArgs.Empty);
                    if(this.cboShipper.Items.Count > 0) this.cboShipper.SelectedIndex = 0;
                    OnShipperSelected(null, EventArgs.Empty);
                    this.chkShipperLiftGate.Checked = this.mPreQuoted ? this.mQuote.LiftGateOrigin : false;
                    this.chkShipperInsidePickup.Checked = this.mPreQuoted ? this.mQuote.InsidePickup : false;
                    this.chkShipperAppt.Checked = this.mPreQuoted ? this.mQuote.AppointmentOrigin : false;
                    this.chkShipperSameDay.Checked = this.mPreQuoted ? this.mQuote.SameDayPickup : false;
                    OnShipperApptChecked(null, EventArgs.Empty);
                    if(this.cboConsignee.Items.Count > 0) this.cboConsignee.SelectedIndex = 0;
                    OnConsigneeSelected(null, EventArgs.Empty);
                    this.chkConsigneeLiftGate.Checked = this.mPreQuoted ? this.mQuote.LiftGateDestination : false;
                    this.chkConsigneeInsideDelivery.Checked = this.mPreQuoted ? this.mQuote.InsideDelivery : false;
                    this.chkConsigneeAppt.Checked = this.mPreQuoted ? this.mQuote.AppointmentDestination : false;
                    OnConsigneeApptChecked(null, EventArgs.Empty);
                    this.txtContactName.Text = "";
                    this.mskContactPhone.Text = "";

                    if(this.mPreQuoted) {
                        //Set hint for shipper/consignee
                        this.mToolTip.SetToolTip(this.cboShipper, "Quote origin: " + this.mQuote.OriginCity.Trim() + ", " + this.mQuote.OriginState.Trim() + " " + this.mQuote.OriginZip.Trim());
                        this.mToolTip.SetToolTip(this.cboConsignee, "Quote destination: " + this.mQuote.DestinationCity.Trim() + ", " + this.mQuote.DestinationState.Trim() + " " + this.mQuote.DestinationZip.Trim());

                        //Show the load
                        LTLPallet2 pallet = new LTLPallet2();
                        pallet.Weight = this.mQuote.Pallet1Weight;
                        pallet.NMFCClass = this.mQuote.Pallet1Class;
                        pallet.InsuranceValue = this.mQuote.Pallet1InsuranceValue;
                        this.mPallets.Add(pallet);
                        if(this.mQuote.Pallet2Weight > 0) {
                            pallet = new LTLPallet2();
                            pallet.Weight = this.mQuote.Pallet2Weight;
                            pallet.NMFCClass = this.mQuote.Pallet2Class;
                            pallet.InsuranceValue = this.mQuote.Pallet2InsuranceValue;
                            this.mPallets.Add(pallet);
                        }
                        if(this.mQuote.Pallet3Weight > 0) {
                            pallet = new LTLPallet2();
                            pallet.Weight = this.mQuote.Pallet3Weight;
                            pallet.NMFCClass = this.mQuote.Pallet3Class;
                            pallet.InsuranceValue = this.mQuote.Pallet3InsuranceValue;
                            this.mPallets.Add(pallet);
                        }
                        if(this.mQuote.Pallet4Weight > 0) {
                            pallet = new LTLPallet2();
                            pallet.Weight = this.mQuote.Pallet4Weight;
                            pallet.NMFCClass = this.mQuote.Pallet4Class;
                            pallet.InsuranceValue = this.mQuote.Pallet4InsuranceValue;
                            this.mPallets.Add(pallet);
                        }
                        if(this.mQuote.Pallet5Weight > 0) {
                            pallet = new LTLPallet2();
                            pallet.Weight = this.mQuote.Pallet5Weight;
                            pallet.NMFCClass = this.mQuote.Pallet5Class;
                            pallet.InsuranceValue = this.mQuote.Pallet5InsuranceValue;
                            this.mPallets.Add(pallet);
                        }

                        this.txtPallets.Text = this.mQuote.Pallets.ToString();
                        this.txtWeight.Text = this.mQuote.Weight.ToString();
                        this.txtRate.Text = this.mQuote.PalletRate.ToString();
                        this.txtFSC.Text = this.mQuote.FuelSurcharge.ToString();
                        this.txtAccessorials.Text = this.mQuote.AccessorialCharge.ToString();
                        this.txtInsurance.Text = this.mQuote.InsuranceCharge.ToString();
                        this.txtTSC.Text = this.mQuote.TollCharge.ToString();
                        this.txtTotal.Text = this.mQuote.TotalCharge.ToString();
                    }

                    this.txtPickupID.Text = "";
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnShipDateChanged(object sender, EventArgs e) {
            //Event handler for change in selected ship date
            try {
                if(this.mShipment.ShipmentNumber.Trim().Length == 0) {
                    //New shipment: check same day pickup if applicable and warn user to validate with the local ternminal
                    this.chkShipperSameDay.Checked = this.dtpShipDate.Value.CompareTo(DateTime.Today) == 0;
                    if(this.dtpShipDate.Value.CompareTo(DateTime.Today) <= 0) MessageBox.Show("It is too late to route this pickup in the local terminal. Please ensure the local terminal can pickup this shipment.", App.Title, MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnClientChanged(object sender, EventArgs e) {
            //Eevent handler for change in selected client
            try {
                //Clear any quote if not pre-quoted
                if(!this.mPreQuoted) showQuote(this.mQuote = null);
                
                //Load shippers and consignees for the selected client
                this.mShippers.Clear();
                this.mShippers.Merge(FreightGateway.ReadLTLShippersList(this.cboClient.SelectedValue.ToString()));
                if(this.cboShipper.Items.Count > 0) this.cboShipper.SelectedIndex = 0;
                OnShipperSelected(null, EventArgs.Empty);
                this.mConsignees.Clear();
                this.mConsignees.Merge(FreightGateway.ReadLTLConsigneesList(this.cboClient.SelectedValue.ToString()));
                if(this.cboConsignee.Items.Count > 0) this.cboConsignee.SelectedIndex = 0;
                OnConsigneeSelected(null, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipperSelected(object sender, EventArgs e) {
            //Event handler for change in selected shipper
            try {
                //Display shipper address
                LTLShipper2 shipper = this.cboShipper.SelectedValue != null ? FreightGateway.ReadLTLShipper(this.cboShipper.SelectedValue.ToString()) : null;
                this.lblShipperAddress.Text = shipper != null ? shipper.AddressLine1.Trim() + "\r\n" + shipper.City.Trim() + ", " + shipper.State + " " + shipper.Zip : "";

                //Clear any quote if not pre-quoted
                if(!this.mPreQuoted) showQuote(this.mQuote = null);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnConsigneeSelected(object sender, EventArgs e) {
            //Event handler for change in selected consignee
            try {
                //Display consignee address
                LTLConsignee2 consignee = this.cboConsignee.SelectedValue != null ? FreightGateway.ReadLTLConsignee(int.Parse(this.cboConsignee.SelectedValue.ToString()), this.cboClient.SelectedValue.ToString()) : null;
                this.lblConsigneeAddress.Text = consignee != null ? consignee.AddressLine1.Trim() + "\r\n" + consignee.City.Trim() + ", " + consignee.State + " " + consignee.Zip : "";

                //Clear any quote if not pre-quoted
                if(!this.mPreQuoted) showQuote(this.mQuote = null);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnAccessorialsChanged(object sender, EventArgs e) {
            //Event handler for change in selected accessorials
            try {
                //Clear any quote if not pre-quoted
                if(!this.mPreQuoted) showQuote(this.mQuote = null);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipperApptChecked(object sender, EventArgs e) {
            //Shipper appointment checked
            try {
                //Clear any quote if not pre-quoted
                if(!this.mPreQuoted) showQuote(this.mQuote = null);

                if(this.chkShipperAppt.Checked) {
                    //Put shipment actuals if applicable
                    if(this.mShipment.ShipmentNumber.Trim().Length > 0) {
                        this.mskShipperApptStart.Text = this.mShipment.PickupAppointmentWindowTimeStart > DateTime.MinValue ? this.mShipment.PickupAppointmentWindowTimeStart.ToString("HH:mm") : "";
                        this.mskShipperApptEnd.Text = this.mShipment.PickupAppointmentWindowTimeEnd > DateTime.MinValue ? this.mShipment.PickupAppointmentWindowTimeEnd.ToString("HH:mm") : "";
                    }
                    else 
                        this.mskShipperApptStart.Text = this.mskShipperApptEnd.Text = "";
                }
                else 
                    this.mskShipperApptStart.Text = this.mskShipperApptEnd.Text = "";
                this.mskShipperApptStart.Enabled = this.mskShipperApptEnd.Enabled = this.chkShipperAppt.Checked;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipperApptValidating(object sender, CancelEventArgs e) {
            //Event handler for start/end hours of operation MaskedTextbox::Validating event
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                DateTime time;
                switch(mtb.Name) {
                    case "mskShipperApptStart":
                        e.Cancel = !DateTime.TryParse(this.mskShipperApptStart.Text, out time);
                        break;
                    case "mskShipperApptEnd":
                        e.Cancel = !DateTime.TryParse(this.mskShipperApptEnd.Text, out time);
                        break;
                }
                if(e.Cancel) MessageBox.Show("Invalid appointment time; please enter military time (i.e. 00:00 - 23:59).", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnShipperApptValidated(object sender, EventArgs e) {
            //Event handler for start/end hours of operation MaskedTextbox::Validated event
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                DateTime start, end;
                if(DateTime.TryParse(this.mskShipperApptStart.Text, out start) && DateTime.TryParse(this.mskShipperApptEnd.Text, out end)) {
                    //Check crossovers
                    switch(mtb.Name) {
                        case "mskShipperApptStart":
                            //Validate not after end
                            if(DateTime.Compare(start, end) > 0) {
                                this.mskShipperApptStart.Text = end.ToString("HH:mm");
                                MessageBox.Show("Appointment start time cannot later than the appointment end time.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                        case "mskShipperApptEnd":
                            //Validate not before start
                            if(DateTime.Compare(start, end) > 0) {
                                this.mskShipperApptEnd.Text = start.ToString("HH:mm");
                                MessageBox.Show("Appointment end time cannot be earlier than the appointment start time.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnSameDayPickupChecked(object sender, EventArgs e) {
            //Event handler for same day pickup checked
            try {
                if(this.mShipment.ShipmentNumber.Trim().Length == 0 && this.chkShipperSameDay.Checked) this.dtpShipDate.Value = DateTime.Today;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnConsigneeApptChecked(object sender, EventArgs e) {
            //
            try {
                //Clear any quote if not pre-quoted
                if(!this.mPreQuoted) showQuote(this.mQuote = null);

                if(this.chkConsigneeAppt.Checked) {
                    //Put shipment actuals if applicable
                    if(this.mShipment.ShipmentNumber.Trim().Length > 0) {
                        this.mskConsigneeApptStart.Text = this.mShipment.DeliveryAppointmentWindowTimeStart > DateTime.MinValue ? this.mShipment.DeliveryAppointmentWindowTimeStart.ToString("HH:mm") : "";
                        this.mskConsigneeApptEnd.Text = this.mShipment.DeliveryAppointmentWindowTimeEnd > DateTime.MinValue ? this.mShipment.DeliveryAppointmentWindowTimeEnd.ToString("HH:mm") : "";
                    }
                    else
                        this.mskConsigneeApptStart.Text = this.mskConsigneeApptEnd.Text = "";
                }
                else
                    this.mskConsigneeApptStart.Text = this.mskConsigneeApptEnd.Text = "";
                this.mskConsigneeApptStart.Enabled = this.mskConsigneeApptEnd.Enabled = this.chkConsigneeAppt.Checked;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnConsigneeApptValidating(object sender, CancelEventArgs e) {
            //Event handler for start/end hours of operation MaskedTextbox::Validating event
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                DateTime time;
                switch(mtb.Name) {
                    case "mskConsigneeApptStart":
                        e.Cancel = !DateTime.TryParse(this.mskConsigneeApptStart.Text, out time);
                        break;
                    case "mskConsigneeApptEnd":
                        e.Cancel = !DateTime.TryParse(this.mskConsigneeApptEnd.Text, out time);
                        break;
                }
                if(e.Cancel) MessageBox.Show("Invalid appointment time; please enter military time (i.e. 00:00 - 23:59).", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnConsigneeApptValidated(object sender, EventArgs e) {
            //Event handler for start/end hours of operation MaskedTextbox::Validated event
            try {
                MaskedTextBox mtb = (MaskedTextBox)sender;
                DateTime start, end;
                if(DateTime.TryParse(this.mskConsigneeApptStart.Text, out start) && DateTime.TryParse(this.mskConsigneeApptEnd.Text, out end)) {
                    //Check crossovers
                    switch(mtb.Name) {
                        case "mskConsigneeApptStart":
                            //Validate not after end
                            if(DateTime.Compare(start, end) > 0) {
                                this.mskConsigneeApptStart.Text = end.ToString("HH:mm");
                                MessageBox.Show("Appointment start time cannot later than the appointment end time.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                        case "mskConsigneeApptEnd":
                            //Validate not before start
                            if(DateTime.Compare(start, end) > 0) {
                                this.mskConsigneeApptEnd.Text = start.ToString("HH:mm");
                                MessageBox.Show("Appointment end time cannot be earlier than the appointment start time.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #region Grid Services: OnPalletGridMouseDown(), OnPalletGridSelected(), OnPalletGridBeforeRowInsert(), OnPalletGridBeforeCellUpdate()
        private void OnPalletGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Select rows on right click
                UltraGrid oGrid = (UltraGrid)sender;
                oGrid.Focus();
                UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) oGrid.Selected.Rows.Clear();
                            oRow.Selected = true;
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            oGrid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnPalletGridSelected(object sender, AfterSelectChangeEventArgs e) {
            //
            try {
                if(this.mTrackingItems != null) {
                    string itemNumber = this.grdPallets.Selected.Rows[0].Cells["TrackingNumber"].Value.ToString().PadLeft(13, '0');
                    foreach(TrackingItem item in this.mTrackingItems) {
                        if(item.ItemNumber.Trim() == itemNumber) this.mTrackingItem.DataSource = item;
                    }
                    this.grdTracking.DataBind();
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnPalletGridBeforeRowInsert(object sender, BeforeRowInsertEventArgs e) {
            //
            try {
                if(this.grdPallets.Rows.Count >= MAX_PALLETS) e.Cancel = true;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnPalletGridBeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e) {
            //
            try {
                switch(e.Cell.Column.Key) {
                    case "Weight":
                        decimal weight = (decimal)e.NewValue;
                        if(weight <= 0 || weight > MAX_WEIGHT) { MessageBox.Show("Please enter a valid pallet weight (1 - " + MAX_WEIGHT.ToString() + "lbs)."); e.Cancel = true; }
                        break;
                    case "InsuranceValue":
                        decimal insurance = (decimal)e.NewValue;
                        if(insurance > MAX_INSURANCE) { MessageBox.Show("Maximum insurance value $" + MAX_INSURANCE.ToString() + "."); e.Cancel = true; }
                       break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        #endregion
        private void OnValidateForm(object sender, EventArgs e) {
            //Set user services
            try {
                //Determine states
                bool isNew = this.mShipment.ShipmentNumber.Trim().Length == 0;
                bool pickedup = this.mShipment.PickupDate.CompareTo(DateTime.MinValue) > 0;
                bool inducted = this.mTrackingItems != null ? this.mTrackingItems.Count > 0 : false;
                bool cancelled = this.mShipment.Cancelled.CompareTo(DateTime.MinValue) > 0;

                //Set services
                this.cboClient.Enabled = isNew;
                this.dtpShipDate.Enabled = !pickedup && !inducted && !cancelled;
                this.cboShipper.Enabled = this.btnNewShipper.Enabled = isNew;
                this.chkShipperLiftGate.Enabled = this.chkShipperInsidePickup.Enabled = this.chkShipperAppt.Enabled = this.chkShipperSameDay.Enabled = isNew;
                this.cboConsignee.Enabled = this.btnNewConsignee.Enabled = isNew;
                this.chkConsigneeLiftGate.Enabled = this.chkConsigneeInsideDelivery.Enabled = this.chkConsigneeAppt.Enabled = isNew;
                this.grdPallets.DisplayLayout.Override.AllowAddNew = isNew && !this.mPreQuoted ? AllowAddNew.TemplateOnBottom : AllowAddNew.No;
                this.grdPallets.DisplayLayout.Override.AllowDelete = isNew && !this.mPreQuoted ? DefaultableBoolean.True : DefaultableBoolean.False;
                this.grdPallets.DisplayLayout.Override.AllowUpdate = isNew && !this.mPreQuoted ? DefaultableBoolean.True : DefaultableBoolean.False;
                this.grdPallets.DisplayLayout.Bands[0].Columns["Weight"].CellActivation = isNew && !this.mPreQuoted ? Activation.AllowEdit : Activation.NoEdit;
                this.grdPallets.DisplayLayout.Bands[0].Columns["InsuranceValue"].CellActivation = isNew && !this.mPreQuoted ? Activation.AllowEdit : Activation.NoEdit;
                this.txtBOLNumber.Enabled = !inducted && !cancelled;

                this.btnQuote.Enabled = isNew && !this.mPreQuoted && 
                                    this.cboClient.SelectedValue != null && this.cboShipper.SelectedValue != null && this.cboConsignee.SelectedValue != null &&
                                    (!this.chkShipperAppt.Checked || (this.chkShipperAppt.Checked && this.mskShipperApptStart.Text.Length > 0 && this.mskShipperApptEnd.Text.Length > 0)) &&
                                    (!this.chkConsigneeAppt.Checked || (this.chkConsigneeAppt.Checked && this.mskConsigneeApptStart.Text.Length > 0 && this.mskConsigneeApptEnd.Text.Length > 0)) &&
                                    this.mPallets.List.Count > 0;
                this.btnOk.Enabled = (!isNew || this.mQuote != null) && 
                                    this.cboClient.SelectedValue != null && this.cboShipper.SelectedValue != null && this.cboConsignee.SelectedValue != null &&
                                    (!this.chkShipperAppt.Checked || (this.chkShipperAppt.Checked && this.mskShipperApptStart.Text.Length > 0 && this.mskShipperApptEnd.Text.Length > 0)) &&
                                    (!this.chkConsigneeAppt.Checked || (this.chkConsigneeAppt.Checked && this.mskConsigneeApptStart.Text.Length > 0 && this.mskConsigneeApptEnd.Text.Length > 0)) &&
                                    this.mPallets.List.Count > 0 && 
                                    !pickedup && !inducted && !cancelled;
                this.btnCancel.Enabled = true;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnCommandClick(object sender,System.EventArgs e) {
            //Event handler for button selection
            LTLShipper2 shipper = null;
            LTLConsignee2 consignee = null;
            try {
                Button btn = (Button)sender;
                switch (btn.Name) {
                    case "btnNewShipper":
                        shipper = new LTLShipper2();
                        shipper.Number = "";
                        shipper.ClientNumber = this.cboClient.SelectedValue.ToString();
                        dlgLTLShipper dlgS = new dlgLTLShipper(shipper);
                        dlgS.Font = this.Font;
                        if(dlgS.ShowDialog(this) == DialogResult.OK) {
                            //Create new shipper; update shipper list
                            this.Cursor = Cursors.WaitCursor;
                            string shipperNumber = FreightGateway.CreateLTLShipper(shipper);
                            this.mShippers.Clear();
                            this.mShippers.Merge(FreightGateway.ReadLTLShippersList(this.cboClient.SelectedValue.ToString()));
                            if(this.cboShipper.Items.Count > 0) this.cboShipper.SelectedValue = shipperNumber;
                            OnShipperSelected(null, EventArgs.Empty);
                        }
                        break;
                    case "btnNewConsignee":
                        consignee = new LTLConsignee2();
                        consignee.ClientNumber = this.cboClient.SelectedValue.ToString();
                        consignee.Number = 0;
                        dlgLTLConsignee dlgC = new dlgLTLConsignee(consignee);
                        dlgC.Font = this.Font;
                        if(dlgC.ShowDialog(this) == DialogResult.OK) {
                            //Create new consignee; update consignee list
                            this.Cursor = Cursors.WaitCursor;
                            int consigneeNumber = FreightGateway.CreateLTLConsignee(consignee);
                            this.mConsignees.Clear();
                            this.mConsignees.Merge(FreightGateway.ReadLTLConsigneesList(this.cboClient.SelectedValue.ToString()));
                            if(this.cboConsignee.Items.Count > 0) this.cboConsignee.SelectedValue = consigneeNumber;
                            OnConsigneeSelected(null, EventArgs.Empty);
                        }
                        break;
                    case "btnQuote":
                        if(!this.mPreQuoted) {
                            //Get a new quote
                            this.mQuote = new LTLQuote2();
                            #region Get a quote if we don't have one
                            this.mQuote.Created = DateTime.Now;
                            this.mQuote.ShipDate = this.dtpShipDate.Value;
                            this.mQuote.ClientID = getClientID(this.cboClient.SelectedValue.ToString());
                            this.mQuote.ShipperNumber = this.cboShipper.SelectedValue.ToString();
                            this.mQuote.ConsigneeNumber = int.Parse(this.cboConsignee.SelectedValue.ToString());
                            this.mQuote.Pallet1Class = this.mQuote.Pallet2Class = this.mQuote.Pallet3Class = this.mQuote.Pallet4Class = this.mQuote.Pallet5Class = "FAK";
                            this.mQuote.Pallet1Weight = int.Parse(this.grdPallets.Rows[0].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint);
                            this.mQuote.Pallet1InsuranceValue = decimal.Parse(this.grdPallets.Rows[0].Cells["InsuranceValue"].Value.ToString());
                            this.mQuote.Pallet2Weight = this.grdPallets.Rows.Count > 1 ? int.Parse(this.grdPallets.Rows[1].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                            this.mQuote.Pallet2InsuranceValue = this.grdPallets.Rows.Count > 1 ? decimal.Parse(this.grdPallets.Rows[1].Cells["InsuranceValue"].Value.ToString()) : 0;
                            this.mQuote.Pallet3Weight = this.grdPallets.Rows.Count > 2 ? int.Parse(this.grdPallets.Rows[2].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                            this.mQuote.Pallet3InsuranceValue = this.grdPallets.Rows.Count > 2 ? decimal.Parse(this.grdPallets.Rows[2].Cells["InsuranceValue"].Value.ToString()) : 0;
                            this.mQuote.Pallet4Weight = this.grdPallets.Rows.Count > 3 ? int.Parse(this.grdPallets.Rows[3].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                            this.mQuote.Pallet4InsuranceValue = this.grdPallets.Rows.Count > 3 ? decimal.Parse(this.grdPallets.Rows[3].Cells["InsuranceValue"].Value.ToString()) : 0;
                            this.mQuote.Pallet5Weight = this.grdPallets.Rows.Count > 4 ? int.Parse(this.grdPallets.Rows[4].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                            this.mQuote.Pallet5InsuranceValue = this.grdPallets.Rows.Count > 4 ? decimal.Parse(this.grdPallets.Rows[4].Cells["InsuranceValue"].Value.ToString()) : 0;
                            this.mQuote.InsidePickup = this.chkShipperInsidePickup.Checked;
                            this.mQuote.LiftGateOrigin = this.chkShipperLiftGate.Checked;
                            this.mQuote.AppointmentOrigin = this.chkShipperAppt.Checked;
                            this.mQuote.SameDayPickup = this.chkShipperSameDay.Checked;
                            this.mQuote.InsideDelivery = this.chkConsigneeInsideDelivery.Checked;
                            this.mQuote.LiftGateDestination = this.chkConsigneeLiftGate.Checked;
                            this.mQuote.AppointmentDestination = this.chkConsigneeAppt.Checked;
                            this.mQuote.Pallets = 0;
                            this.mQuote.Weight = 0;
                            this.mQuote.PalletRate = 0;
                            this.mQuote.FuelSurcharge = 0;
                            this.mQuote.AccessorialCharge = 0;
                            this.mQuote.InsuranceCharge = 0;
                            this.mQuote.TollCharge = 0;
                            this.mQuote.TotalCharge = 0;
                            #endregion
                            this.mQuote = FreightGateway.CreateQuote(this.mQuote);
                        }
                        else {
                            //Set existing quote
                            this.mQuote.Created = DateTime.Now;
                            this.mQuote.ClientID = getClientID(this.cboClient.SelectedValue.ToString());
                            this.mQuote.ShipperNumber = this.cboShipper.SelectedValue.ToString();
                            this.mQuote.ConsigneeNumber = int.Parse(this.cboConsignee.SelectedValue.ToString());
                        }

                        //Display the quote
                        showQuote(this.mQuote);

                        //Enable booking?
                        OnValidateForm(null, EventArgs.Empty);
                        break;
                    case "btnOk":
                        //Validate shipper and consignee with the passed in quote if applicable
                        //NOTE: Cannot validate the zip as the quote was for city/state and maybe an incorrect 
                        //      zip (i.e. when multiple zips are returned he doesn't know which is the correct one)
                        //if(this.mPreQuoted) {
                        //    shipper = FreightGateway.ReadLTLShipper(this.cboShipper.SelectedValue.ToString());
                        //    if(!(shipper.City.Trim().ToLower() == this.mQuote.OriginCity.Trim().ToLower() && shipper.State.Trim().ToLower() == this.mQuote.OriginState.Trim().ToLower()))
                        //        throw new ApplicationException("The shipper city/state (" + shipper.City.Trim() + ", " + shipper.State.Trim() + ") does not match the quote's origin city/state (" + this.mQuote.OriginCity.Trim() + ", " + this.mQuote.OriginState.Trim() + ").");

                        //    consignee = FreightGateway.ReadLTLConsignee(int.Parse(this.cboConsignee.SelectedValue.ToString()), this.cboClient.SelectedValue.ToString());
                        //    if(!(consignee.City.Trim().ToLower() == this.mQuote.DestinationCity.Trim().ToLower() && consignee.State.Trim().ToLower() == this.mQuote.DestinationState.Trim().ToLower()))
                        //        throw new ApplicationException("The consignee city/state (" + consignee.City.Trim() + ", " + consignee.State.Trim() + ") does not match the quote's destination city/state (" + this.mQuote.DestinationCity.Trim() + ", " + this.mQuote.DestinationState.Trim() + ").");
                        //}

                        #region Update the new/existing shipment for create/update
                        if(this.mShipment.ShipmentNumber.Trim().Length == 0) {
                            //New shipment
                            this.mShipment.Created = DateTime.Now;
                            this.mShipment.ClientNumber = this.cboClient.SelectedValue.ToString();
                            this.mShipment.ShipperNumber = this.cboShipper.SelectedValue.ToString();
                            this.mShipment.ConsigneeNumber = int.Parse(this.cboConsignee.SelectedValue.ToString());
                            this.mShipment.BLNumber = "";
                        }
                        this.mShipment.ShipDate = this.dtpShipDate.Value;
                        this.mShipment.ContactName = this.txtContactName.Text;
                        this.mShipment.ContactPhone = this.mskContactPhone.Text;
                        this.mShipment.Pallet1Class = this.mShipment.Pallet2Class = this.mShipment.Pallet3Class = this.mShipment.Pallet4Class = this.mShipment.Pallet5Class = "FAK";
                        this.mShipment.Pallet1Weight = int.Parse(this.grdPallets.Rows[0].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint);
                        this.mShipment.Pallet1InsuranceValue = decimal.Parse(this.grdPallets.Rows[0].Cells["InsuranceValue"].Value.ToString());
                        this.mShipment.Pallet2Weight = this.grdPallets.Rows.Count > 1 ? int.Parse(this.grdPallets.Rows[1].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        this.mShipment.Pallet2InsuranceValue = this.grdPallets.Rows.Count > 1 ? decimal.Parse(this.grdPallets.Rows[1].Cells["InsuranceValue"].Value.ToString()) : 0;
                        this.mShipment.Pallet3Weight = this.grdPallets.Rows.Count > 2 ? int.Parse(this.grdPallets.Rows[2].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        this.mShipment.Pallet3InsuranceValue = this.grdPallets.Rows.Count > 2 ? decimal.Parse(this.grdPallets.Rows[2].Cells["InsuranceValue"].Value.ToString()) : 0;
                        this.mShipment.Pallet4Weight = this.grdPallets.Rows.Count > 3 ? int.Parse(this.grdPallets.Rows[3].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        this.mShipment.Pallet4InsuranceValue = this.grdPallets.Rows.Count > 3 ? decimal.Parse(this.grdPallets.Rows[3].Cells["InsuranceValue"].Value.ToString()) : 0;
                        this.mShipment.Pallet5Weight = this.grdPallets.Rows.Count > 4 ? int.Parse(this.grdPallets.Rows[4].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        this.mShipment.Pallet5InsuranceValue = this.grdPallets.Rows.Count > 4 ? decimal.Parse(this.grdPallets.Rows[4].Cells["InsuranceValue"].Value.ToString()) : 0;
                        this.mShipment.InsidePickup = this.chkShipperInsidePickup.Checked;
                        this.mShipment.LiftGateOrigin = this.chkShipperLiftGate.Checked;
                        if(this.chkShipperAppt.Checked) {
                            DateTime start, end;
                            if(DateTime.TryParse(this.mskShipperApptStart.Text, out start) && DateTime.TryParse(this.mskShipperApptEnd.Text, out end)) {
                                this.mShipment.PickupAppointmentWindowTimeStart = start;
                                this.mShipment.PickupAppointmentWindowTimeEnd = end;
                            }
                            else {
                                MessageBox.Show("Please enter a valid shipper appointment in military time (i.e. 13:00 - 14:00)", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        this.mShipment.SameDayPickup = this.chkShipperSameDay.Checked;
                        this.mShipment.InsideDelivery = this.chkConsigneeInsideDelivery.Checked;
                        this.mShipment.LiftGateDestination = this.chkConsigneeLiftGate.Checked;
                        if(this.chkConsigneeAppt.Checked) {
                            DateTime start, end;
                            if(DateTime.TryParse(this.mskConsigneeApptStart.Text, out start) && DateTime.TryParse(this.mskConsigneeApptEnd.Text, out end)) {
                                this.mShipment.DeliveryAppointmentWindowTimeStart = start;
                                this.mShipment.DeliveryAppointmentWindowTimeEnd = end;
                            }
                            else {
                                MessageBox.Show("Please enter a valid consignee appointment in military time (i.e. 13:00 - 14:00)", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        this.mShipment.BLNumber = this.txtBOLNumber.Text;
                        this.mShipment.Pallets = int.Parse(this.txtPallets.Text);
                        this.mShipment.Weight = decimal.Parse(this.txtWeight.Text);
                        this.mShipment.PalletRate = decimal.Parse(this.txtRate.Text.Replace("$",""));
                        this.mShipment.FuelSurcharge = decimal.Parse(this.txtFSC.Text.Replace("$", ""));
                        this.mShipment.AccessorialCharge = decimal.Parse(this.txtAccessorials.Text.Replace("$", ""));
                        this.mShipment.InsuranceCharge = decimal.Parse(this.txtInsurance.Text.Replace("$", ""));
                        this.mShipment.TollCharge = decimal.Parse(this.txtTSC.Text.Replace("$", ""));
                        this.mShipment.TotalCharge = decimal.Parse(this.txtTotal.Text.Replace("$", ""));
                        this.mShipment.UserID = Environment.UserName;
                        this.mShipment.LastUpdated = DateTime.Now;
                        #endregion
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                    case "btnCancel":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

        private int getClientID(string clientNumber) {
            //Determine clientID from clientNumber
            return int.Parse(this.mClients.LTLClientTable.Select("ClientNumber='" + clientNumber + "'")[0]["ID"].ToString());
        }
        private DateTime getNextValidShipDate() {
            //Determine next valid ship date (i.e. no weekends)
            DateTime shipDate = DateTime.Today;
            if(shipDate.DayOfWeek == DayOfWeek.Saturday) shipDate.AddDays(2);
            if(shipDate.DayOfWeek == DayOfWeek.Sunday) shipDate.AddDays(1);
            return shipDate;
        }
        private void showQuote(LTLQuote2 quote) {
            //
            try {
                if(quote != null) {
                    this.lblEstimatedDeliveryDate.Text = "Estimated delivery on " + quote.EstimatedDeliveryDate.ToShortDateString();
                    this.txtPallets.Text = this.mQuote.Pallets.ToString("#0");
                    this.txtWeight.Text = this.mQuote.Weight.ToString("#0");
                    this.txtRate.Text = "$" + this.mQuote.PalletRate.ToString("#0.00");
                    this.txtFSC.Text = "$" + this.mQuote.FuelSurcharge.ToString("#0.00");
                    this.txtAccessorials.Text = "$" + this.mQuote.AccessorialCharge.ToString("#0.00");
                    this.txtInsurance.Text = "$" + this.mQuote.InsuranceCharge.ToString("#0.00");
                    this.txtTSC.Text = "$" + this.mQuote.TollCharge.ToString("#0.00");
                    this.txtTotal.Text = "$" + this.mQuote.TotalCharge.ToString("#0.00");

                }
                else {
                    this.txtPallets.Text = this.txtWeight.Text = this.txtRate.Text = this.txtFSC.Text = this.txtAccessorials.Text = this.txtInsurance.Text = this.txtTSC.Text = this.txtTotal.Text = "";
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
        }
    }
}
