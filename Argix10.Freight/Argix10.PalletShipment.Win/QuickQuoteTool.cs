using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Enterprise;
using Argix.Security;
using Argix.Windows;

namespace Argix.Freight {
    //
    public partial class QuickQuoteTool : UserControl {
        //Members
        private UltraGridSvc mGridSvcP = null;

        private const int MAX_PALLETS = 5;
        private const decimal MAX_WEIGHT = 2000, MAX_INSURANCE = 10000;

        //Interface
        public QuickQuoteTool() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvcP = new UltraGridSvc(this.grdPallets);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    #region Grid customizations from normal layout (to support cell editing)
                    this.grdPallets.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                    this.grdPallets.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                    this.grdPallets.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                    this.grdPallets.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                    this.grdPallets.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                    this.grdPallets.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
                    this.grdPallets.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                    this.grdPallets.DisplayLayout.Override.MaxSelectedCells = 1;
                    this.grdPallets.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                    this.grdPallets.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.EditAndSelectText;
                    this.grdPallets.DisplayLayout.Bands[0].Columns["TrackingNumber"].CellActivation = Activation.NoEdit;
                    this.grdPallets.DisplayLayout.Bands[0].Columns["Weight"].CellActivation = Activation.AllowEdit;
                    this.grdPallets.DisplayLayout.Bands[0].Columns["NMFCClass"].CellActivation = Activation.NoEdit;
                    this.grdPallets.DisplayLayout.Bands[0].Columns["InsuranceValue"].CellActivation = Activation.AllowEdit;
                    this.grdPallets.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                    this.grdPallets.DisplayLayout.Bands[0].Columns["TrackingNumber"].SortIndicator = SortIndicator.Ascending;

                    this.grdQuotes.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                    this.grdQuotes.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                    this.grdQuotes.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                    this.grdQuotes.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                    this.grdQuotes.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                    this.grdQuotes.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
                    this.grdQuotes.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                    this.grdQuotes.DisplayLayout.Override.MaxSelectedCells = 1;
                    this.grdQuotes.DisplayLayout.Override.CellClickAction = CellClickAction.CellSelect;
                    this.grdQuotes.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.CellSelect;
                    this.grdQuotes.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                    this.grdQuotes.DisplayLayout.Bands[0].Columns["Created"].SortIndicator = SortIndicator.Ascending;
                    #endregion

                    this.dtpShipDate.MinDate = DateTime.Today;
                    this.dtpShipDate.MaxDate = DateTime.Today.AddDays(30);
                    this.dtpShipDate.Value = getNextValidShipDate();

                    this.mClients.LTLClientTable.AddLTLClientTableRow(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", DateTime.MinValue, "", DateTime.MinValue, "", 0, DateTime.MinValue, "", "");
                    this.mClients.Merge(FreightGateway.ReadLTLClientList());
                    if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                    OnClientChanged(null, EventArgs.Empty);
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnClientChanged(object sender, EventArgs e) {
            //
            try {
                showQuote(null);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipDateChanged(object sender, EventArgs e) {
            //
            try {
                this.chkShipperSameDay.Checked = this.dtpShipDate.Value.CompareTo(DateTime.Today) == 0;
                this.chkShipperSameDay.ForeColor = this.dtpShipDate.Value.CompareTo(DateTime.Today) == 0 ? System.Drawing.Color.Red : System.Drawing.SystemColors.ControlText;
                showQuote(null);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipperZipChanged(object sender, EventArgs e) {
            //Event handler for change in shipper zip text
            this.Cursor = Cursors.WaitCursor;
            try {
                //Validate zip entry
                if(this.txtShipperZip.Text.Trim().Length == 5) {
                    //Validate this is a servicable location
                    string zip = this.txtShipperZip.Text;
                    ServiceLocation location = FreightGateway.ReadPickupLocation(zip);
                    if(location == null) {
                        this.txtShipperZip.Text = "";
                        this.txtShipperZip.Focus();
                        MessageBox.Show(zip + " is currently not supported for pickup.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    showQuote(null);
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnConsigneeZipChanged(object sender, EventArgs e) {
            //Event handler for change in consignee zip text
            this.Cursor = Cursors.WaitCursor;
            try {
                //Validate zip entry
                if(this.txtConsigneeZip.Text.Trim().Length == 5) {
                    //Validate this is a servicable location
                    string zip = this.txtConsigneeZip.Text;
                    ServiceLocation location = FreightGateway.ReadServiceLocation(zip);
                    if(location == null) {
                        this.txtConsigneeZip.Text = "";
                        this.txtConsigneeZip.Focus();
                        MessageBox.Show(zip + " is currently not supported for delivery.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        this.lblZone.Text = location.ZoneCode.Trim();
                    showQuote(null);
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        #region Load Grid Services: OnLoadGridMouseDown(), OnLoadGridBeforeRowInsert(), OnLoadGridBeforeCellUpdate(), OnLoadGridAfterRowUpadte()
        private void OnLoadGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Select rows on right click
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
                if(uiElement != null) {
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if(context != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)context;
                            if(!row.Selected) grid.Selected.Rows.Clear();
                            row.Selected = true;
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active row when in a scroll region to prevent double-click action
                        if(uiElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnLoadGridBeforeRowInsert(object sender, BeforeRowInsertEventArgs e) {
            //
            try {
                if(this.grdPallets.Rows.Count >= MAX_PALLETS) e.Cancel = true;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnLoadGridBeforeCellUpdate(object sender, BeforeCellUpdateEventArgs e) {
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
        private void OnLoadGridAfterRowUpdate(object sender, RowEventArgs e) {
            //
            try {
                showQuote(null);
                OnValidateForm(null, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        private void OnValidateForm(object sender, EventArgs e) {
            //Set user services
            try {
                //Set services
                this.btnQuote.Enabled = this.txtShipperZip.Text.Length == 5 && this.txtConsigneeZip.Text.Length == 5 && this.grdPallets.Rows.Count > 0;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnCommandClick(object sender, System.EventArgs e) {
            //Event handler for button selection
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnQuote":
                        this.Cursor = Cursors.WaitCursor;
                        showQuote(null);
                        LTLQuote2 quote = new LTLQuote2();
                        #region Create Quote
                        quote.ID = 0;
                        quote.Created = DateTime.Now;
                        quote.ShipDate = this.dtpShipDate.Value;
                        quote.ClientID = int.Parse(this.cboClient.SelectedValue.ToString());
                        quote.OriginZip = this.txtShipperZip.Text;
                        quote.DestinationZip = this.txtConsigneeZip.Text;
                        quote.Pallet1Class = quote.Pallet2Class = quote.Pallet3Class = quote.Pallet4Class = quote.Pallet5Class = "FAK";
                        quote.Pallet1Weight = int.Parse(this.grdPallets.Rows[0].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint);
                        quote.Pallet1InsuranceValue = decimal.Parse(this.grdPallets.Rows[0].Cells["InsuranceValue"].Value.ToString());
                        quote.Pallet2Weight = this.grdPallets.Rows.Count > 1 ? int.Parse(this.grdPallets.Rows[1].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        quote.Pallet2InsuranceValue = this.grdPallets.Rows.Count > 1 ? decimal.Parse(this.grdPallets.Rows[1].Cells["InsuranceValue"].Value.ToString()) : 0;
                        quote.Pallet3Weight = this.grdPallets.Rows.Count > 2 ? int.Parse(this.grdPallets.Rows[2].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        quote.Pallet3InsuranceValue = this.grdPallets.Rows.Count > 2 ? decimal.Parse(this.grdPallets.Rows[2].Cells["InsuranceValue"].Value.ToString()) : 0;
                        quote.Pallet4Weight = this.grdPallets.Rows.Count > 3 ? int.Parse(this.grdPallets.Rows[3].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        quote.Pallet4InsuranceValue = this.grdPallets.Rows.Count > 3 ? decimal.Parse(this.grdPallets.Rows[3].Cells["InsuranceValue"].Value.ToString()) : 0;
                        quote.Pallet5Weight = this.grdPallets.Rows.Count > 4 ? int.Parse(this.grdPallets.Rows[4].Cells["Weight"].Value.ToString(), System.Globalization.NumberStyles.AllowDecimalPoint) : 0;
                        quote.Pallet5InsuranceValue = this.grdPallets.Rows.Count > 4 ? decimal.Parse(this.grdPallets.Rows[4].Cells["InsuranceValue"].Value.ToString()) : 0;
                        quote.InsidePickup = this.chkShipperInsidePickup.Checked;
                        quote.LiftGateOrigin = this.chkShipperLiftGate.Checked;
                        quote.AppointmentOrigin = this.chkShipperAppt.Checked;
                        quote.SameDayPickup = this.chkShipperSameDay.Checked;
                        quote.InsideDelivery = this.chkConsigneeInsideDelivery.Checked;
                        quote.LiftGateDestination = this.chkConsigneeLiftGate.Checked;
                        quote.AppointmentDestination = this.chkConsigneeAppt.Checked;
                        quote.Pallets = 0;
                        quote.Weight = quote.PalletRate = quote.FuelSurcharge = quote.AccessorialCharge = quote.InsuranceCharge = quote.TollCharge = quote.TotalCharge = 0.0M;
                        #endregion
                        quote = FreightGateway.CreateQuote(quote);
                        this.mQuotes.Add(quote);
                        this.grdQuotes.Refresh();
                        showQuote(quote);
                        break;
                    case "btnClear":
                        this.txtShipperZip.Text = this.txtConsigneeZip.Text = "";
                        this.chkShipperAppt.Checked = this.chkShipperInsidePickup.Checked = this.chkShipperLiftGate.Checked = this.chkShipperSameDay.Checked = false;
                        this.chkConsigneeAppt.Checked = this.chkConsigneeInsideDelivery.Checked = this.chkConsigneeLiftGate.Checked = false;
                        this.mPallets.Clear();
                        this.lblEstimatedDeliveryDate.Text = "";
                        //this.txtPallets.Text = this.txtWeight.Text = "";
                        this.txtRate.Text = this.txtFSC.Text = this.txtAccessorials.Text = this.txtInsurance.Text = this.txtTSC.Text = this.txtTotal.Text = "";
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }

        private DateTime getNextValidShipDate() {
            //Determine next valid ship date (i.e. no weekends)
            DateTime shipDate = DateTime.Today.AddDays(0);
            if(shipDate.DayOfWeek == DayOfWeek.Saturday) shipDate.AddDays(2);
            if(shipDate.DayOfWeek == DayOfWeek.Sunday) shipDate.AddDays(1);
            return shipDate;
        }
        private void showQuote(LTLQuote2 quote) {
            //
            try {
                if(quote != null) {
                    this.lblEstimatedDeliveryDate.Text = quote.EstimatedDeliveryDate.ToShortDateString();
                    //this.txtPallets.Text = quote.Pallets.ToString("#0");
                    //this.txtWeight.Text = quote.Weight.ToString("#0");
                    this.txtRate.Text = quote.PalletRate.ToString("#0.00");
                    this.txtFSC.Text = quote.FuelSurcharge.ToString("#0.00");
                    this.txtAccessorials.Text = quote.AccessorialCharge.ToString("#0.00");
                    this.txtInsurance.Text = quote.InsuranceCharge.ToString("#0.00");
                    this.txtTSC.Text = quote.TollCharge.ToString("#0.00");
                    this.txtTotal.Text = quote.TotalCharge.ToString("#0.00");
                }
                else {
                    quote = null;
                    this.lblEstimatedDeliveryDate.Text = "";
                    //this.txtPallets.Text = this.txtWeight.Text = "";
                    this.txtRate.Text = this.txtFSC.Text = this.txtAccessorials.Text = this.txtInsurance.Text = this.txtTSC.Text = this.txtTotal.Text = "";
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
        }
    }
}
