using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Security;
using Argix.Windows;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
    //
    public partial class winQuotes : Form, IToolbar, IQuoteToolbar {
        //Members
        private LTLQuote2 mQuote = null;
        private ReminderService mReminders = null;
        private UltraGridSvc mGridSvcP = null, mGridSvcQ = null;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        private int MAX_PALLETS = 5;
        private decimal MAX_WEIGHT = 1500;
        private decimal MAX_INSURANCE = 10000;

        //Interface
        public winQuotes() {
            //Constructor
            InitializeComponent();
            try {
                MAX_PALLETS = App.Config.PalletsMax;
                MAX_WEIGHT = App.Config.WeightMax;
                MAX_INSURANCE = App.Config.InsuranceMax;
                this.mReminders = new ReminderService();
                this.mReminders.OpenItem += new ReminderEventHandler(OnReminderOpenItem);
                this.mReminders.ItemsChanged += new EventHandler(OnReminderItemsChanged);
                this.mGridSvcP = new UltraGridSvc(this.grdPallets);
                this.mGridSvcQ = new UltraGridSvc(this.grdPendingQuotes);
                this.grdPendingQuotes.Controls.Add(this.cboQuotesFilter);
            }
            catch(Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        public override bool Focused { get { return this.grdPendingQuotes.Focused; } }
        public override Font Font { get { return base.Font; } set { this.grdPallets.Font = this.grdPendingQuotes.Font = base.Font = value; } }
        #region IToolbar interface
        public bool CanNew { get { return false; } }
        public void New() { }
        public bool CanOpen { get { return this.cmsOpen.Enabled; } }
        public void Open() { this.cmsOpen.PerformClick(); }
        public bool CanCancel { get { return this.cmsCancelQuote.Enabled; } }
        public void Cancel() { this.cmsCancelQuote.PerformClick(); }
        public bool CanSave { get { return this.grdPendingQuotes.Focused && this.grdPendingQuotes.Rows.Count > 0; } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mLoadTenderQuotes, "LoadTenderQuoteTable", filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return this.grdPendingQuotes.Focused && this.grdPendingQuotes.Rows.Count > 0; } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                UltraGridPrinter.Print(this.grdPendingQuotes, "Load Tender Quotes", showDialog);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { get { return this.grdPendingQuotes.Focused && this.grdPendingQuotes.Rows.Count > 0; } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                UltraGridPrinter.PrintPreview(this.grdPendingQuotes, "Load Tender Quotes");
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }

        }
        #endregion
        #region IQuoteToolbar interface
        public bool CanApproveQuote { get { return this.cmsApproveQuote.Enabled; } }
        public void ApproveQuote() { this.cmsApproveQuote.PerformClick(); }
        public bool CanTenderQuote { get { return this.cmsTenderQuote.Enabled; } }
        public void TenderQuote() { this.cmsTenderQuote.PerformClick(); }
        public bool CanViewTender { get { return this.cmsViewLoadTender.Enabled; } }
        public void ViewTender() { this.cmsViewLoadTender.PerformClick(); }
        public bool CanBookQuote { get { return this.cmsBookQuote.Enabled; } }
        public void BookQuote() { this.cmsBookQuote.PerformClick(); }
        #endregion
        public override void Refresh() { this.cmsRefresh.PerformClick(); }
        private void OnFormLoad(object sender, EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.grdPallets.Font = this.grdPendingQuotes.Font = this.Font;
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

                this.grdPendingQuotes.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                this.grdPendingQuotes.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdPendingQuotes.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdPendingQuotes.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdPendingQuotes.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdPendingQuotes.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdPendingQuotes.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdPendingQuotes.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdPendingQuotes.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdPendingQuotes.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdPendingQuotes.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.QuotesColumnHeaders;
                this.dtpShipDate.MinDate = DateTime.Today;
                this.dtpShipDate.MaxDate = DateTime.Today.AddDays(30);
                this.dtpShipDate.Value = getNextValidShipDate();

                this.mClients.LTLClientTable.AddLTLClientTableRow(0, "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", "", DateTime.MinValue, "", DateTime.MinValue, "", 0, DateTime.MinValue, "", "");
                this.mClients.Merge(FreightGateway.ReadLTLClientList());
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(null, EventArgs.Empty);

                this.rdoShipperZip.Checked = global::Argix.Properties.Settings.Default.QuoteShipperByZip;
                this.rdoShipperCityState.Checked = !global::Argix.Properties.Settings.Default.QuoteShipperByZip;
                this.rdoConsigneeZip.Checked = global::Argix.Properties.Settings.Default.QuoteConsigneeByZip;
                this.rdoConsigneeCityState.Checked = !global::Argix.Properties.Settings.Default.QuoteConsigneeByZip;

                this.mReminders.Reminders = global::Argix.Properties.Settings.Default.Reminders;
                this.mReminders.Start();

                this.cboQuotesFilter.SelectedIndex = 0;
                OnQuotesFilterChanged(null, EventArgs.Empty);
                this.cmsRefresh.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender, EventArgs e) {
            //Event handler for form activated event
            try {
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender, EventArgs e) {
            //Event handler for form deactivate event
            try {
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Event handler for form closing event
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.QuotesColumnHeaders = this.ColumnHeaders;

                this.mReminders.Stop();
                global::Argix.Properties.Settings.Default.Reminders = this.mReminders.Reminders;
            }
        }
        private void OnDisplayQuoter(object sender, EventArgs e) {
            //Event handler to show/hide the quoter
            try {
                if(this.scControl.Panel1Collapsed) {
                    this.scControl.Panel1Collapsed = false;
                    this.btnQuoter.Image = global::Argix.Properties.Resources.Collapse_large;
                }
                else {
                    this.scControl.Panel1Collapsed = true;
                    this.btnQuoter.Image = global::Argix.Properties.Resources.Expand_large;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnShipDateChanged(object sender, EventArgs e) {
            //
            try {
                this.chkShipperSameDay.Checked = this.dtpShipDate.Value.CompareTo(DateTime.Today) == 0;
                this.chkShipperSameDay.ForeColor = this.dtpShipDate.Value.CompareTo(DateTime.Today) == 0 ? System.Drawing.Color.Red : System.Drawing.SystemColors.ControlText;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnClientChanged(object sender, EventArgs e) {
            //
            try {
                showQuote(null);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipperTypeChanged(object sender, EventArgs e) {
            //Event handler for change in shipper zip or city/state option
            try {
                //Clear data, enable user services
                this.cboShipperZip.Text = this.txtShipperCity.Text = this.txtShipperState.Text = "";
                this.cboShipperZip.Items.Clear();
                this.cboShipperZip.DropDownStyle = this.rdoShipperZip.Checked ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
                this.cboShipperZip.Enabled = this.rdoShipperZip.Checked;
                this.txtShipperCity.Enabled = this.txtShipperState.Enabled = this.rdoShipperCityState.Checked;
                showQuote(null);
                global::Argix.Properties.Settings.Default.QuoteShipperByZip = this.rdoShipperZip.Checked;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnShipperZipChanged(object sender, EventArgs e) {
            //Event handler for change in shipper zip text
            this.Cursor = Cursors.WaitCursor;
            try {
                //Validate zip entry
                if(this.rdoShipperZip.Checked && this.cboShipperZip.Text.Length == 5) {
                    //Validate this is a servicable location
                    string zip = this.cboShipperZip.Text;
                    ServiceLocation location = FreightGateway.ReadPickupLocation(zip);
                    if(location != null) {
                        this.txtShipperCity.Text = location.City;
                        this.txtShipperState.Text = location.State;
                    }
                    else {
                        this.cboShipperZip.Text = "";
                        this.cboShipperZip.Focus();
                        MessageBox.Show(zip + " is currently not supported for pickup.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    showQuote(null);
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnShipperAddressChanged(object sender, EventArgs e) {
            //Event handler for change in shipper city/state text
            this.Cursor = Cursors.WaitCursor;
            try {
                //Validate city and state entries
                if(this.rdoShipperCityState.Checked && this.txtShipperCity.Text.Length > 0 && this.txtShipperState.Text.Length == 2) {
                    //Validate this is a servicable location
                    string city = this.txtShipperCity.Text;
                    string state = this.txtShipperState.Text;
                    ServiceLocations locations = FreightGateway.ReadPickupLocations(city, state);
                    if(locations != null && locations.Count > 0) {
                        for(int i = 0; i < locations.Count; i++) { this.cboShipperZip.Items.Add(locations[i].ZipCode); }
                        this.cboShipperZip.Enabled = locations.Count > 1;
                        this.cboShipperZip.Text = locations[0].ZipCode;
                    }
                    else {
                        this.txtShipperCity.Text = this.txtShipperState.Text = "";
                        this.txtShipperCity.Focus();
                        MessageBox.Show(city + ", " + state + " is currently not supported for pickup.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    showQuote(null);
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnConsigneeTypeChanged(object sender, EventArgs e) {
            //Event handler for change in consignee zip or city/state option
            try {
                //Clear data, enable user services
                this.cboConsigneeZip.Text = this.txtConsigneeCity.Text = this.txtConsigneeState.Text = "";
                this.cboConsigneeZip.Items.Clear();
                this.cboConsigneeZip.DropDownStyle = this.rdoConsigneeZip.Checked ? ComboBoxStyle.DropDown : ComboBoxStyle.DropDownList;
                this.cboConsigneeZip.Enabled = this.rdoConsigneeZip.Checked;
                this.txtConsigneeCity.Enabled = this.txtConsigneeState.Enabled = this.rdoConsigneeCityState.Checked;
                showQuote(null);
                global::Argix.Properties.Settings.Default.QuoteConsigneeByZip = this.rdoConsigneeZip.Checked;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnConsigneeZipChanged(object sender, EventArgs e) {
            //Event handler for change in consignee zip text
            this.Cursor = Cursors.WaitCursor;
            try {
                //Validate zip entry
                if(this.rdoConsigneeZip.Checked && this.cboConsigneeZip.Text.Length == 5) {
                    //Validate this is a servicable location
                    string zip = this.cboConsigneeZip.Text;
                    ServiceLocation location = FreightGateway.ReadServiceLocation(zip);
                    if(location != null) {
                        this.txtConsigneeCity.Text = location.City;
                        this.txtConsigneeState.Text = location.State;
                    }
                    else {
                        this.cboConsigneeZip.Text = "";
                        this.cboConsigneeZip.Focus();
                        MessageBox.Show(zip + " is currently not supported for delivery.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    showQuote(null);
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnConsigneeAddressChanged(object sender, EventArgs e) {
            //Event handler for change in consignee city/state text
            this.Cursor = Cursors.WaitCursor;
            try {
                //Validate city and state entries
                if(this.rdoConsigneeCityState.Checked && this.txtConsigneeCity.Text.Length > 0 && this.txtConsigneeState.Text.Length == 2) {
                    //Validate this is a servicable location
                    string city = this.txtConsigneeCity.Text;
                    string state = this.txtConsigneeState.Text;
                    ServiceLocations locations = FreightGateway.ReadServiceLocations(city, state);
                    if(locations != null && locations.Count > 0) {
                        for(int i = 0; i < locations.Count; i++) { this.cboConsigneeZip.Items.Add(locations[i].ZipCode); }
                        this.cboConsigneeZip.Enabled = locations.Count > 1;
                        this.cboConsigneeZip.Text = locations[0].ZipCode;
                    }
                    else {
                        this.txtConsigneeCity.Text = this.txtConsigneeState.Text = "";
                        this.txtConsigneeCity.Focus();
                        MessageBox.Show(city + ", " + state + " is currently not supported for delivery.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
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
        #region Quote Grid Services: OnQuoteGridInitializeLayout(), OnQuoteGridInitializeRow(), OnQuoteGridMouseDown(), OnQuoteGridBeforeRowFilterDropDownPopulate(), OnQuoteGridAfterSelectChange(), OnQuoteGridBeforeCellActivate(), OnQuoteGridKeyUp(), OnQuoteGridAfterRowUpdate2()
        private void OnQuoteGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //Event handler for grid layout initialization
            try {
                e.Layout.Bands[0].Columns.Insert(0, "Reminder");
                e.Layout.Bands[0].Columns["Reminder"].DataType = typeof(Image);
                e.Layout.Bands[0].Columns["Reminder"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                e.Layout.Bands[0].Columns["Reminder"].Header.Caption = "";
                e.Layout.Bands[0].Columns["Reminder"].CellAppearance.ImageHAlign = HAlign.Center;
                e.Layout.Bands[0].Columns["Reminder"].AllowRowFiltering = DefaultableBoolean.False;
                e.Layout.Bands[0].Columns["Reminder"].Width = 20;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridInitializeRow(object sender, InitializeRowEventArgs e) {
            //Event handler for row intialize event
            try {
                //Color code quotes
                //Pending
                if((e.Row.Cells["Approved"].Value != DBNull.Value || e.Row.Cells["LoadTenderNumber"].Value != DBNull.Value) && e.Row.Cells["ShipmentNumber"].Value == DBNull.Value && e.Row.Cells["Cancelled"].Value == DBNull.Value) e.Row.Appearance.BackColor = Color.Yellow;
                
                //Booked
                if(e.Row.Cells["ShipmentNumber"].Value != DBNull.Value && e.Row.Cells["Cancelled"].Value == DBNull.Value) { e.Row.Appearance.ForeColor = Color.White; e.Row.Appearance.BackColor = Color.Green; }

                //Add reminder flags
                if(this.mReminders.HasReminder(Convert.ToInt32(e.Row.Cells["ID"].Value), Environment.UserName)) e.Row.Cells["Reminder"].Value = Properties.Resources.Flag_red;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Set menu and toolbar services
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
                            row.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) {
                            grid.ActiveRow = null;
                        }
                        //else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                        //    if (grid.ActiveRow != null) grid.ActiveRow = null;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnQuoteGridBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridAfterSelectChange(object sender, AfterSelectChangeEventArgs e) {
            //
            try {
                OnValidateForm(null, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridBeforeCellActivate(object sender, Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for cell activated
            try {
                //Set cell editing
                switch(e.Cell.Column.Key.ToString()) {
                    case "Owner":
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                    case "BrokerName":
                    case "ContactName":
                    case "ContactPhone":
                    case "ContactEmail":
                    case "Description":
                    case "Comments":
                        e.Cell.Activation = Activation.AllowEdit;
                        break;
                    case "TotalCharge":
                        e.Cell.Activation = e.Cell.Row.Cells["Approved"].Value == DBNull.Value ? Activation.AllowEdit : Activation.NoEdit;
                        break;
                    default:
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridAfterCellUpdate(object sender, CellEventArgs e) {
            //Event handler for after cell update
            try {
                //Validate total charge does not exceed the minimum
                switch(e.Cell.Column.Key.ToString()) {
                    case "TotalCharge":
                        if(e.Cell.Value != e.Cell.OriginalValue) {
                            decimal totalChargeMin = (decimal)e.Cell.Row.Cells["TotalChargeMin"].Value;
                            if((decimal)e.Cell.Value >= totalChargeMin) {
                                //Re-calculate and update pallet rate and FSC; quote will be updated when the user finishes editing
                                int id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                                LTLLoadTenderQuote ltQuote = FreightGateway.ReadLoadTenderQuote(id);
                                LTLQuote2 quote = ltQuote.LTLQuote;
                                quote.TotalCharge = (decimal)e.Cell.Value;
                                quote = FreightGateway.CreateQuote(quote);
                                e.Cell.Row.Cells["PalletRate"].Value = quote.PalletRate;
                                e.Cell.Row.Cells["FuelSurcharge"].Value = quote.FuelSurcharge;
                            }
                            else {
                                e.Cell.Value = e.Cell.OriginalValue;
                                MessageBox.Show(this, "The total charge cannot be less than " + totalChargeMin.ToString() + ".", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
            //Event handler for keyup event
            try {
                //Update the quote if the user pressed enter
                if(e.KeyCode == Keys.Enter && this.grdPendingQuotes.ActiveRow != null) {
                    this.grdPendingQuotes.ActiveRow.Update(); e.Handled = true;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnQuoteGridAfterRowUpdate2(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //
            try {
                //Update
                this.grdPendingQuotes.Update();
                int id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                LTLLoadTenderQuote ltQuote = FreightGateway.ReadLoadTenderQuote(id);
                ltQuote.BrokerName = e.Row.Cells["BrokerName"].Value.ToString();
                ltQuote.ContactName = e.Row.Cells["ContactName"].Value.ToString();
                ltQuote.ContactPhone = e.Row.Cells["ContactPhone"].Value.ToString();
                ltQuote.ContactEmail = e.Row.Cells["ContactEmail"].Value.ToString();
                ltQuote.Description = e.Row.Cells["Description"].Value.ToString();
                ltQuote.Comments = e.Row.Cells["Comments"].Value.ToString();
                ltQuote.LTLQuote.PalletRate = (decimal)e.Row.Cells["PalletRate"].Value;
                ltQuote.LTLQuote.FuelSurcharge = (decimal)e.Row.Cells["FuelSurcharge"].Value;
                ltQuote.LTLQuote.TotalCharge = (decimal)e.Row.Cells["TotalCharge"].Value;
                FreightGateway.UpdateLoadTenderQuote(ltQuote);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnQuoteGridDoubleClick(object sender, EventArgs e) {
            //Event handler double click event
            try {
                //Open the quote
                if(this.cmsOpen.Enabled && this.grdPendingQuotes.ActiveCell != null && !this.grdPendingQuotes.ActiveCell.IsInEditMode) this.cmsOpen.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Reminders: OnReminderOpenItem(), OnReminderItemsChanged()
        private void OnReminderOpenItem(object source, ReminderEventArgs e) {
            //Event handler for OpenItem event
            try {
                for(int i = 0; i < this.grdPendingQuotes.Rows.Count; i++) {
                    if(Convert.ToInt32(this.grdPendingQuotes.Rows[i].Cells["ID"].Value) == e.ID) {
                        this.grdPendingQuotes.Rows[i].Selected = true;
                        this.grdPendingQuotes.Rows.ScrollCardIntoView(this.grdPendingQuotes.Rows[i]);
                        break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnReminderItemsChanged(object source, EventArgs e) {
            //Event handler for ItemChanged event
            try {
                this.cmsRefresh.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
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
        private void OnQuotesFilterChanged(object sender, System.EventArgs e) {
            //Event handler for change in quote view
            try {
                //Apply grid filters to show/hide certain quote states
                this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                switch(this.cboQuotesFilter.Text) {
                    case "All":
                        break;
                    case "Pending":
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters["Approved"].FilterConditions.Add(FilterComparisionOperator.NotEquals, DBNull.Value);
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters["ShipmentNumber"].FilterConditions.Add(FilterComparisionOperator.Equals, DBNull.Value);
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters["Cancelled"].FilterConditions.Add(FilterComparisionOperator.Equals, DBNull.Value);
                        break;
                    case "Booked":
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters["ShipmentNumber"].FilterConditions.Add(FilterComparisionOperator.NotEquals, DBNull.Value);
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters.LogicalOperator = FilterLogicalOperator.And;
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters["Cancelled"].FilterConditions.Add(FilterComparisionOperator.Equals, DBNull.Value);
                        break;
                    case "Cancelled":
                        this.grdPendingQuotes.DisplayLayout.Bands[0].ColumnFilters["Cancelled"].FilterConditions.Add(FilterComparisionOperator.NotEquals, DBNull.Value);
                        break;
                }
                this.grdPendingQuotes.DisplayLayout.RefreshFilters();
                this.grdPendingQuotes.Selected.Rows.Clear();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnValidateForm(object sender, EventArgs e) {
            //Set user services
            setUserServices();
        }
        #region User Services: OnItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
            //Event handler for button selection
            int id = 0;
            dlgLoadTenderQuote dlglt=null;
            try {
                string name =  sender is Button ? ((Button)sender).Name : ((ToolStripMenuItem)sender).Name;
                switch(name) {
                    case "btnQuote":
                        //Get a quote
                        this.Cursor = Cursors.WaitCursor;
                        showQuote(null);
                        LTLQuote2 quote = new LTLQuote2();
                        #region Create Quote
                        quote.ID = 0;
                        quote.Created = DateTime.Now;
                        quote.ShipDate = this.dtpShipDate.Value;
                        quote.ClientID = int.Parse(this.cboClient.SelectedValue.ToString());
                        quote.OriginCity = this.txtShipperCity.Text;
                        quote.OriginState = this.txtShipperState.Text;
                        quote.OriginZip = this.cboShipperZip.Text;
                        quote.DestinationCity = this.txtConsigneeCity.Text;
                        quote.DestinationState = this.txtConsigneeState.Text;
                        quote.DestinationZip = this.cboConsigneeZip.Text;
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
                        showQuote(quote);
                        break;
                    case "btnSave":
                        LTLLoadTenderQuote ltQuote = new LTLLoadTenderQuote();
                        ltQuote.LTLQuote = new LTLQuote2();
                        #region Set fields
                        ltQuote.ID = 0;
                        ltQuote.Owner = Environment.UserName;
                        ltQuote.LTLQuote.ShipDate = this.mQuote.ShipDate;
                        ltQuote.LTLQuote.ClientID = int.Parse(this.cboClient.SelectedValue.ToString());
                        ltQuote.LTLQuote.OriginCity = this.mQuote.OriginCity;
                        ltQuote.LTLQuote.OriginState = this.mQuote.OriginState;
                        ltQuote.LTLQuote.OriginZip = this.mQuote.OriginZip;
                        ltQuote.LTLQuote.InsidePickup = this.mQuote.InsidePickup;
                        ltQuote.LTLQuote.LiftGateOrigin = this.mQuote.LiftGateOrigin;
                        ltQuote.LTLQuote.AppointmentOrigin = this.mQuote.AppointmentOrigin;
                        ltQuote.LTLQuote.SameDayPickup = this.mQuote.SameDayPickup;
                        ltQuote.LTLQuote.DestinationCity = this.mQuote.DestinationCity;
                        ltQuote.LTLQuote.DestinationState = this.mQuote.DestinationState;
                        ltQuote.LTLQuote.DestinationZip = this.mQuote.DestinationZip;
                        ltQuote.LTLQuote.InsideDelivery = this.mQuote.InsideDelivery;
                        ltQuote.LTLQuote.LiftGateDestination = this.mQuote.LiftGateDestination;
                        ltQuote.LTLQuote.AppointmentDestination = this.mQuote.AppointmentDestination;
                        ltQuote.LTLQuote.Pallet1Weight = this.mQuote.Pallet1Weight;
                        ltQuote.LTLQuote.Pallet1InsuranceValue = this.mQuote.Pallet1InsuranceValue;
                        ltQuote.LTLQuote.Pallet2Weight = this.mQuote.Pallet2Weight;
                        ltQuote.LTLQuote.Pallet2InsuranceValue = this.mQuote.Pallet2InsuranceValue;
                        ltQuote.LTLQuote.Pallet3Weight = this.mQuote.Pallet3Weight;
                        ltQuote.LTLQuote.Pallet3InsuranceValue = this.mQuote.Pallet3InsuranceValue;
                        ltQuote.LTLQuote.Pallet4Weight = this.mQuote.Pallet4Weight;
                        ltQuote.LTLQuote.Pallet4InsuranceValue = this.mQuote.Pallet4InsuranceValue;
                        ltQuote.LTLQuote.Pallet5Weight = this.mQuote.Pallet5Weight;
                        ltQuote.LTLQuote.Pallet5InsuranceValue = this.mQuote.Pallet5InsuranceValue;
                        ltQuote.LTLQuote.Pallets = this.mQuote.Pallets;
                        ltQuote.LTLQuote.Weight = this.mQuote.Weight;
                        ltQuote.LTLQuote.PalletRate = this.mQuote.PalletRate;
                        ltQuote.LTLQuote.FuelSurcharge = this.mQuote.FuelSurcharge;
                        ltQuote.LTLQuote.AccessorialCharge = this.mQuote.AccessorialCharge;
                        ltQuote.LTLQuote.InsuranceCharge = this.mQuote.InsuranceCharge;
                        ltQuote.LTLQuote.TollCharge = this.mQuote.TollCharge;
                        ltQuote.LTLQuote.TotalCharge = this.mQuote.TotalCharge;
                        ltQuote.TotalChargeMin = (decimal)this.txtTotal.Tag;
                        ltQuote.Logged = DateTime.Now;
                        ltQuote.LoggedBy = Environment.UserName;
                        #endregion
                        dlglt = new dlgLoadTenderQuote(ltQuote);
                        dlglt.Font = this.Font;
                        if(dlglt.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            int quoteID = FreightGateway.CreateLoadTenderQuote(ltQuote);
                            MessageBox.Show(this, "New Load Tender Quote was created.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cmsRefresh.PerformClick();
                        }
                        break;
                    case "btnClear":
                        this.dtpShipDate.Value = getNextValidShipDate();
                        this.rdoShipperZip.Checked = global::Argix.Properties.Settings.Default.QuoteShipperByZip;
                        this.rdoShipperCityState.Checked = !global::Argix.Properties.Settings.Default.QuoteShipperByZip;
                        this.cboShipperZip.Items.Clear();
                        this.cboShipperZip.Text = this.txtShipperCity.Text = this.txtShipperState.Text = "";
                        this.chkShipperLiftGate.Checked = this.chkShipperInsidePickup.Checked = this.chkShipperAppt.Checked = false;
                        this.rdoConsigneeZip.Checked = global::Argix.Properties.Settings.Default.QuoteConsigneeByZip;
                        this.rdoConsigneeCityState.Checked = !global::Argix.Properties.Settings.Default.QuoteConsigneeByZip;
                        this.cboConsigneeZip.Items.Clear();
                        this.cboConsigneeZip.Text = this.txtConsigneeCity.Text = this.txtConsigneeState.Text = "";
                        this.chkConsigneeLiftGate.Checked = this.chkConsigneeInsideDelivery.Checked = this.chkConsigneeAppt.Checked = false;
                        this.mPallets.Clear();
                        showQuote(null);
                        break;
                    case "cmsRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        this.mLoadTenderQuotes.Clear();
                        this.mLoadTenderQuotes.Merge(FreightGateway.ViewLoadTenderQuotes(RoleServiceGateway.IsSalesRepAdmin ? null : Environment.UserName));
                        this.grdPendingQuotes.Refresh();
                        break;
                    case "cmsOpen":
                        id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                        ltQuote = FreightGateway.ReadLoadTenderQuote(id);
                        dlglt = new dlgLoadTenderQuote(ltQuote);
                        bool _approved = ltQuote.Approved != DateTime.MinValue;
                        dlglt.Font = this.Font;
                        if(dlglt.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.UpdateLoadTenderQuote(ltQuote);
                            //Approve if now approved
                            if(!_approved && ltQuote.Approved != DateTime.MinValue)
                                FreightGateway.ApproveLoadTenderQuote(ltQuote.ID, ltQuote.Approved, ltQuote.ApprovedBy);
                            MessageBox.Show(this, "Load Tender Quote " + ltQuote.ID.ToString() + " was updated.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cmsRefresh.PerformClick();
                        }
                       break;
                    case "cmsApproveQuote":
                        //Approve a logged quote
                        this.Cursor = Cursors.WaitCursor;
                        id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                        bool approved = FreightGateway.ApproveLoadTenderQuote(id, DateTime.Now, Environment.UserName);
                        MessageBox.Show(this, "Load Tender Quote " + id.ToString() + " was approved.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.cmsRefresh.PerformClick();
                        break;
                    case "cmsTenderQuote":
                        //Tender a pending quote
                         OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.AddExtension = true;
                        dlgOpen.Filter = "All Files (*.*) | *.*";
                        dlgOpen.FilterIndex = 0;
                        dlgOpen.Title = "Select Load Tender to Attach...";
                        dlgOpen.FileName = "";
                        if(dlgOpen.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            string fileName = new System.IO.FileInfo(dlgOpen.FileName).Name;
                            FileStream fsa = null;
                            BinaryReader reader = null;
                            try {
                                id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                                LoadTender lt = new LoadTender();
                                lt.Filename = dlgOpen.SafeFileName;
                                fsa = new FileStream(dlgOpen.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                reader = new BinaryReader(fsa);
                                lt.File = reader.ReadBytes((int)fsa.Length);
                                bool tendered = FreightGateway.TenderLoadTenderQuote(id, lt);
                                MessageBox.Show(this, "Load Tender Quote " + id.ToString() + " was tendered.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                this.cmsRefresh.PerformClick();
                            }
                            finally { if(reader != null) reader.Close(); if(fsa != null) fsa.Close(); }
                        }
                       break;
                    case "cmsBookQuote":
                        //Book a pending quote
                        this.Cursor = Cursors.WaitCursor;
                        id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                        ltQuote = FreightGateway.ReadLoadTenderQuote(id);

                        //Call the new shipment dialog and create a shipment
                        LTLShipment2 shipment = new LTLShipment2();
                        shipment.ShipmentNumber = "";
                        dlgLTLShipment dlgS = new dlgLTLShipment(shipment, ltQuote.LTLQuote);
                        dlgS.Font = this.Font;
                        if (dlgS.ShowDialog(this) == DialogResult.OK) {
                            //Update the pending quote as "booked"
                            this.Cursor = Cursors.WaitCursor;
                            bool booked = FreightGateway.BookLoadTenderQuote(id, shipment);
                            MessageBox.Show(this, "Load Tender Quote " + id.ToString() + " was tendered.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cmsRefresh.PerformClick();
                        }
                        break;
                    case "cmsCancelQuote":
                        //Cancell a pending quote
                        if(MessageBox.Show(this, "Are you sure you want to cancel this quote?", App.Product, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                            bool cancelled = FreightGateway.CancelLoadTenderQuote(id, DateTime.Now, Environment.UserName);
                            MessageBox.Show(this, "Load Tender Quote " + id.ToString() + " was cancelled.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cmsRefresh.PerformClick();
                        }
                        break;
                    case "cmsViewLoadTender":
                        //Open the load tender for the selected quote
                        int number = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["LoadTenderNumber"].Value);
                        LoadTender loadTender = FreightGateway.GetLoadTender(number);
                        string file = App.Config.TempFolder + loadTender.Filename;
                        try { System.IO.File.Delete(file); } catch { }
                        FileStream fso = null;
                        BinaryWriter writer = null;
                        try {
                            fso = new FileStream(file, FileMode.OpenOrCreate, FileAccess.Write);
                            writer = new BinaryWriter(fso);
                            writer.Write(loadTender.File);
                            writer.Flush();
                        }
                        finally { if(writer != null) writer.Close(); if(fso != null) fso.Close(); }
                        System.Diagnostics.Process.Start(file);
                        break;
                    case "cmsViewShipment":
                        //Open the shjipment for the selected quote
                        string snumber = this.grdPendingQuotes.ActiveRow.Cells["ShipmentNumber"].Value.ToString();
                        shipment = FreightGateway.ReadLTLShipment(snumber);
                        dlgS = new dlgLTLShipment(shipment);
                        dlgS.Font = this.Font;
                        dlgS.ShowDialog(this);
                        //if (dlgS.ShowDialog(this) == DialogResult.OK) {
                        //    this.Cursor = Cursors.WaitCursor;
                        //    FreightGateway.UpdateLTLShipment(shipment);
                        //    MessageBox.Show(this, "Shipment updated.", App.Product, MessageBoxButtons.OK);
                        //}
                        break;
                    case "cmsChangeOwner":
                        this.Cursor = Cursors.WaitCursor;
                        dlgInputBox dlgib = new dlgInputBox("Enter another owner for this LoadTenderQuote.", "", App.Product);
                        if(dlgib.ShowDialog(this) == DialogResult.OK) {
                            id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                            string owner = dlgib.Value;
                            bool changed = FreightGateway.ChangeOwnerLoadTenderQuote(id, owner);
                            MessageBox.Show(this, "Owner changed for Load Tender Quote " + id.ToString() + ".", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.cmsRefresh.PerformClick();
                        }
                       break;
                    case "cmsAddReminder":
                        id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                        this.mReminders.AddReminder(id, this.grdPendingQuotes.ActiveRow.Cells["Description"].Value.ToString(), Environment.UserName);
                        this.cmsRefresh.PerformClick();
                        break;
                    case "cmsClearFlag":
                        id = Convert.ToInt32(this.grdPendingQuotes.ActiveRow.Cells["ID"].Value);
                        this.mReminders.RemoveReminder(id, Environment.UserName);
                        this.cmsRefresh.PerformClick();
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        #endregion
        #region Local Services: setUserServices(), reportStatus(), ColumnHeaders, getNextValidShipDate(), overrideQuote(), showQuote()
        private void setUserServices() {
            //Set user services
            try {
                //Set access
                bool isSalesRep = false;
                try {
                    isSalesRep = RoleServiceGateway.IsSalesRep;
                }
                catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }

                //Set menu states 
                UltraGridRow row = this.grdPendingQuotes.ActiveRow != null ? this.grdPendingQuotes.ActiveRow : null;
                bool approved = false, tendered = false, booked = false, cancelled = false;
                if(row != null) {
                    approved = row.Cells["Approved"].Value != DBNull.Value;
                    tendered = row.Cells["LoadTenderNumber"].Value != DBNull.Value;
                    booked = row.Cells["ShipmentNumber"].Value != DBNull.Value;
                    cancelled = row.Cells["Cancelled"].Value != DBNull.Value;
                }
                this.cmsRefresh.Enabled = true;
                this.cmsOpen.Enabled = this.grdPendingQuotes.ActiveRow != null;
                this.cmsApproveQuote.Enabled = RoleServiceGateway.IsSalesRep && this.grdPendingQuotes.ActiveRow != null && !approved;
                this.cmsTenderQuote.Enabled = RoleServiceGateway.IsSalesRep && approved && !tendered && !cancelled;
                this.cmsBookQuote.Enabled = RoleServiceGateway.IsSalesRep && tendered && !booked && !cancelled;
                this.cmsCancelQuote.Enabled = RoleServiceGateway.IsSalesRep && approved && !cancelled;
                this.cmsViewLoadTender.Enabled = tendered;
                this.cmsViewShipment.Enabled = booked;
                this.cmsChangeOwner.Enabled = RoleServiceGateway.IsSalesRepAdmin;
                this.cmsAddReminder.Enabled = this.grdPendingQuotes.ActiveRow != null;
                this.cmsClearFlag.Enabled = this.grdPendingQuotes.ActiveRow != null && row.Cells["Reminder"].Value != null;
                this.txtTotal.Enabled = this.mQuote != null;
                this.btnQuote.Enabled = this.cboShipperZip.Text.Length > 0 && this.cboConsigneeZip.Text.Length > 0 && this.grdPallets.Rows.Count > 0;
                this.btnSave.Enabled = RoleServiceGateway.IsSalesRep && this.mQuote != null;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { Application.DoEvents(); if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this, new EventArgs()); }
        }
        private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
        private string ColumnHeaders {
            get {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.grdPendingQuotes.DisplayLayout.SaveAsXml(ms, PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if(value.Length > 0) {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdPendingQuotes.DisplayLayout.LoadFromXml(ms, PropertyCategories.SortedColumns);
                }
            }
        }
        private DateTime getNextValidShipDate() {
            //Determine next valid ship date (i.e. no weekends)
            DateTime shipDate = DateTime.Today.AddDays(0);
            if(shipDate.DayOfWeek == DayOfWeek.Saturday) shipDate.AddDays(2);
            if(shipDate.DayOfWeek == DayOfWeek.Sunday) shipDate.AddDays(1);
            return shipDate;
        }
        private void overrideQuote() {
            //
            try {
                if(this.mQuote != null) {
                    //Validate total charge does not go below the total charge minimum
                    decimal totalCharge = decimal.Parse(this.txtTotal.Text);
                    if(totalCharge >= (decimal)this.txtTotal.Tag) {
                        //Update the quote
                        this.mQuote.TotalCharge = totalCharge;
                        LTLQuote2 quote = FreightGateway.CreateQuote(this.mQuote);
                        showQuote(quote);   //Can be null which clears display
                    }
                    else {
                        //Restore the quote
                        this.txtTotal.Text = this.txtTotal.Tag.ToString();
                        this.txtTotal.Focus();
                        MessageBox.Show("The total charge cannot be less than " + this.txtTotal.Tag.ToString(), App.Product, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
        }
        private void showQuote(LTLQuote2 quote) {
            //
            try {
                if(quote != null) {
                    this.mQuote = quote;
                    this.lblEstimatedDeliveryDate.Text = "Estimated delivery on " + quote.EstimatedDeliveryDate.ToShortDateString();
                    this.txtPallets.Text = this.mQuote.Pallets.ToString("#0");
                    this.txtWeight.Text = this.mQuote.Weight.ToString("#0");
                    this.txtRate.Text = this.mQuote.PalletRate.ToString("#0.00");
                    this.txtFSC.Text = this.mQuote.FuelSurcharge.ToString("#0.00");
                    this.txtAccessorials.Text = this.mQuote.AccessorialCharge.ToString("#0.00");
                    this.txtInsurance.Text = this.mQuote.InsuranceCharge.ToString("#0.00");
                    this.txtTSC.Text = this.mQuote.TollCharge.ToString("#0.00");
                    this.txtTotal.Text = this.mQuote.TotalCharge.ToString("#0.00");
                    this.txtTotal.Tag = this.mQuote.TotalCharge;        //Hold the minimum charge
                }
                else {
                    this.mQuote = null;
                    this.lblEstimatedDeliveryDate.Text = this.txtPallets.Text = this.txtWeight.Text = this.txtRate.Text = this.txtFSC.Text = this.txtAccessorials.Text = this.txtInsurance.Text = this.txtTSC.Text = this.txtTotal.Text = "";
                    this.txtTotal.Tag = null;   //Clear the minimum charge
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
        }
        #endregion
    }
}
