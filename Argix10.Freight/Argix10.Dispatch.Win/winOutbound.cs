using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
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
    public partial class winOutbound:Form,ISchedule {
        //Members
        private UltraGridSvc mGridSvc = null, mGridTSvc=null;
        private bool mIsDragging = false;
        private OutboundAutoRefreshService mAutoRefreshSvc = null;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winOutbound() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
                this.mGridTSvc = new UltraGridSvc(this.grdTemplates);
                this.mAutoRefreshSvc = new OutboundAutoRefreshService(this);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region ISchedule interface
        public bool CanNew { get { return this.csNew.Enabled; } }
        public void New() { this.csNew.PerformClick(); }
        public bool CanOpen { get { return this.csOpen.Enabled; } }
        public void Open() { this.csOpen.PerformClick(); }
        public bool CanClone { get { return this.csClone.Enabled; } }
        public void Clone() { this.csClone.PerformClick(); }
        public bool CanCancel { get { return this.csCancel.Enabled; } }
        public void Cancel() {
            if (this.csCancel.Enabled) {
                int id = int.Parse(this.grdSchedule.ActiveRow.Cells["ID"].Value.ToString());
                if (MessageBox.Show(this,"Are you sure you want to cancel trip " + id.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                    FreightGateway.CancelScheduledOutboundFreight(id,DateTime.Now,Environment.UserName);
                    Refresh();
                }
            }
        }
        public bool CanSave { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mSchedule,"OutboundScheduleTable",filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing this schedule..."));
                string caption = "Outbound Schedule ";
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": caption = "Outbound Schedule " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Outbound Schedule " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); ; break;
                    case "Advanced": caption = "Outbound Schedule Advanced"; break;
                    case "Archive": caption = "Outbound Schedule Archive"; break;
                }
                UltraGridPrinter.Print(this.grdSchedule,caption,showDialog);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                reportStatus(new StatusEventArgs("Print previewing this schedule..."));
                string caption = "Outbound Schedule ";
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": caption = "Outbound Schedule " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Outbound Schedule " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); ; break;
                    case "Advanced": caption = "Outbound Schedule Advanced"; break;
                    case "Archive": caption = "Outbound Schedule Archive"; break;
                }
                UltraGridPrinter.PrintPreview(this.grdSchedule,caption);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }

        }
        public bool CanNewTemplate { get { return this.csTempNew.Enabled; } }
        public void NewTemplate() { this.csTempNew.PerformClick(); }
        public bool CanOpenTemplate { get { return this.csTempOpen.Enabled; } }
        public void OpenTemplate() { this.csTempOpen.PerformClick(); }
        public bool CanCancelTemplate { get { return this.csTempCancel.Enabled; } }
        public void CancelTemplate() { this.csTempCancel.PerformClick(); }
        public bool CanLoadTemplates { get { return this.csTempLoad.Enabled; } }
        public void LoadTemplates() { this.csTempLoad.PerformClick(); }
        public bool TemplatesVisible { get { return !this.scWin.Panel2Collapsed; } set { this.scWin.Panel2Collapsed = !value; } }
        #endregion
        public override void Refresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                reportStatus(new StatusEventArgs("Refreshing Outbound Schedule..."));
                this.mGridSvc.CaptureState();
                this.mSchedule.Clear();
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": this.mSchedule.Merge(FreightGateway.ViewOutboundSchedule()); break;
                    case "Yesterday": this.mSchedule.Merge(FreightGateway.ViewOutboundScheduleYesterday()); break;
                    case "Advanced": this.mSchedule.Merge(FreightGateway.ViewOutboundScheduleAdvanced()); break;
                    case "Archive": this.mSchedule.Merge(FreightGateway.ViewOutboundScheduleArchive()); break;
                }
                this.mGridSvc.RestoreState();

                this.mGridTSvc.CaptureState();
                this.mTemplates.Clear();
                this.mTemplates.Merge(FreightGateway.ViewOutboundScheduleTemplates());
                this.mGridTSvc.RestoreState();
                base.Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                #region Grid customizations from normal layout (to support cell editing)
                this.grdSchedule.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdSchedule.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdSchedule.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdSchedule.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdSchedule.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdSchedule.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdSchedule.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdSchedule.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdSchedule.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdSchedule.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdSchedule.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                this.grdSchedule.DisplayLayout.Bands[0].Columns["ID"].CellActivation = Activation.NoEdit;
                this.grdSchedule.DisplayLayout.Bands[0].Columns["Created"].SortIndicator = SortIndicator.Descending;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.OutboundColumnHeaders;
                this.mOrigins.Merge(FreightGateway.GetLocations());
                this.mDestinations.Merge(FreightGateway.GetLocations());
                this.mCarriers.Merge(FreightGateway.GetCarriers());
                this.mDrivers.Merge(FreightGateway.GetDrivers());
                this.cboTemplates.ComboBox.SelectedIndex = 0;
                this.cboSchedule.SelectedIndex = 0;
                Application.DoEvents();
                OnScheduleChanged(this.cboSchedule,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender,EventArgs e) {
            //Event handler for form activated event
            //Turn on auto refresh if applicable
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Today") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Outbound Schedule..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Outbound Schedule..."));
                    this.mAutoRefreshSvc.Stop();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            //Turn off auto refresh
            try {
                reportStatus(new StatusEventArgs("Stopping auto-refresh on Outbound Schedule..."));
                this.mAutoRefreshSvc.Stop();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
                if (!e.Cancel) {
                    //Save settings
                    global::Argix.Properties.Settings.Default.OutboundColumnHeaders = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csMain.Font = this.csTemp.Font = this.Font; } catch { } }
        private void OnScheduleChanged(object sender,EventArgs e) {
            //Event handler for change in schedule
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Today") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Outbound Schedule..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Outbound Schedule..."));
                    this.mAutoRefreshSvc.Stop();
                }
                this.grdSchedule.ActiveRow = null;
                this.grdSchedule.Focus();
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnTemplateTypeChanged(object sender,EventArgs e) {
            //Event handler for change in template view (i.e. templates, ship scheules)
            try {
                switch (this.cboTemplates.ComboBox.SelectedItem.ToString()) {
                    case "Templates":
                        this.grdTemplates.Visible = true;
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnOriginBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddOrigins.DisplayLayout.Bands[0].Columns["Name"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["Origin"].Width;
            }
            catch { }
        }
        private void OnOriginSelected(object sender,Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            try {
                if (e.Row != null) {
                    this.grdSchedule.ActiveRow.Cells["OriginLocation"].Value = e.Row.Cells["Location"].Text;
                }
            }
            catch { }
        }
        private void OnDestinationBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddDestinations.DisplayLayout.Bands[0].Columns["Name"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["Destination"].Width;
            }
            catch { }
        }
        private void OnDestinationSelected(object sender,Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            try {
                if (e.Row != null) {
                    this.grdSchedule.ActiveRow.Cells["DestinationLocation"].Value = e.Row.Cells["Location"].Text;
                }
            }
            catch { }
        }
        private void OnCarrierBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddCarriers.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["CarrierName"].Width;
            }
            catch { }
        }
        private void OnDriverBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddDrivers.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["DriverName"].Width;
            }
            catch { }
        }
        #region Schedule: OnScheduleInitializeLayout(), OnScheduleInitializeRow(), OnScheduleBeforeRowFilterDropDownPopulate(), OnScheduleMouseDown(), OnScheduleAfterSelectChange(), ...
        private void OnScheduleInitializeLayout(object sender,InitializeLayoutEventArgs e) {
            try {
                e.Layout.Bands[0].Columns["Origin"].ValueList = this.uddOrigins;
                e.Layout.Bands[0].Columns["Destination"].ValueList = this.uddDestinations;
                e.Layout.Bands[0].Columns["CarrierName"].ValueList = this.uddCarriers;
                e.Layout.Bands[0].Columns["DriverName"].ValueList = this.uddDrivers;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnScheduleInitializeRow(object sender,InitializeRowEventArgs e) {
            //Event handler for intialize row event
            try {
                if (this.cboSchedule.SelectedItem.ToString() == "Today" && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk)) {
                    if(e.Row.Cells["ActualDeparture"].Value == DBNull.Value && e.Row.Cells["Cancelled"].Value.ToString().Trim().Length == 0) {
                        //Date may not be same as schedule date
                        DateTime scheduledDeparture = DateTime.Parse(e.Row.Cells["ScheduledDeparture"].Value.ToString());
                        if (scheduledDeparture.AddMinutes(global::Argix.Properties.Settings.Default.AlertWindow) < DateTime.Now) e.Row.Cells["ActualDeparture"].Appearance.BackColor = Color.Red;
                    }
                    if(e.Row.Cells["ActualArrival"].Value == DBNull.Value && e.Row.Cells["Cancelled"].Value.ToString().Trim().Length == 0) {
                        //Date may not be same as schedule date
                        DateTime scheduledArrival = DateTime.Parse(e.Row.Cells["ScheduledArrival"].Value.ToString());
                        if (scheduledArrival.AddMinutes(global::Argix.Properties.Settings.Default.AlertWindow) < DateTime.Now) e.Row.Cells["ActualArrival"].Appearance.BackColor = Color.Red;
                    }
                }
                if(e.Row.Cells["Cancelled"].Value.ToString().Trim().Length > 0) {
                    e.Row.Appearance.ForeColor = System.Drawing.Color.DarkGray;
                    e.Row.Activation = Activation.NoEdit;
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Information); }
        }
        private void OnScheduleBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Information); }
        }
        private void OnScheduleMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if (uiElement != null) {
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if (context != null) {
                        if (e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if (e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)context;
                            row.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if (uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) {
                            grid.ActiveRow = null;
                        }
                        //else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                        //    if (grid.ActiveRow != null) grid.ActiveRow = null;
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnScheduleAfterSelectChange(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for pickup grid AfterSelectChange event
            setUserServices();
        }
        private void OnScheduleBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for cell activated
            try {
                //Set cell editing
                switch (e.Cell.Column.Key.ToString()) {
                    case "Origin":
                    case "OriginLocation":
                    case "Destination":
                    case "DestinationLocation":
                    case "ScheduledDeparture":
                    case "ScheduledArrival":
                    case "CarrierName":
                    case "DriverName":
                    case "Confirmed":
                    case "Amount":
                    case "AmountType":
                    case "TrailerNumber":
                    case "DropEmptyTrailerNumber":
                    case "ActualDeparture":
                    case "ActualArrival":
                    case "Comments":
                        e.Cell.Activation = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk ? Activation.AllowEdit : Activation.NoEdit;
                        break;
                    case "ID":
                    case "Created":
                    case "CreateUserID":
                    case "ScheduleDate":
                    case "IsTemplate":
                    case "CancelledUserID":
                    case "Cancelled":
                    case "LastUpdated":
                    case "UserID":
                    case "RowVersion":
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                    default:
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnScheduleBeforeEnterEditMode(object sender, CancelEventArgs e) {
            //
            try {
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnScheduleAfterEnterEditMode(object sender, System.EventArgs e) {
            //Event handler for 
            try {
                setUserServices();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnScheduleKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            try {
                if (e.KeyCode == Keys.Enter && this.grdSchedule.ActiveRow != null) {
                    this.grdSchedule.ActiveRow.Update(); e.Handled = true;
                }
                else if (e.Control && e.KeyCode == Keys.C) {
                    Cursor.Current = Cursors.WaitCursor;
                    if (this.grdSchedule.ActiveRow != null) {
                        int id = int.Parse(this.grdSchedule.ActiveRow.Cells["ID"].Value.ToString());
                        DataRow[] trips = this.mSchedule.OutboundScheduleTable.Select("ID=" + id);
                        Clipboard.SetData(DataFormats.Serializable,trips[0].ItemArray);
                    }
                }
                else if (e.Control && e.KeyCode == Keys.V) {
                    if (this.cboSchedule.SelectedItem.ToString() == "Today" || this.cboSchedule.SelectedItem.ToString() == "Advanced") {
                        Cursor.Current = Cursors.WaitCursor;
                        object o = Clipboard.GetData(DataFormats.Serializable);
                        if (o != null) {
                            Cursor.Current = Cursors.WaitCursor;
                            object[] os = (object[])o;
                            DispatchDataset.OutboundScheduleTableRow trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                            trip.ItemArray = os;
                            trip.CreateUserID = Environment.UserName;
                            trip.Created = DateTime.Now;
                            switch (this.cboSchedule.SelectedItem.ToString()) {
                                case "Today": trip.ScheduleDate = DateTime.Today; break;
                                case "Advanced": trip.ScheduleDate = DateTime.Today.AddDays(1); break;
                            }
                            trip.ScheduledArrival = trip.ScheduleDate.Date + trip.ScheduledArrival.TimeOfDay;
                            trip.ScheduledDeparture = trip.ScheduleDate.Date + trip.ScheduledDeparture.TimeOfDay;
                            if (trip.ScheduledArrival.CompareTo(trip.ScheduledDeparture) < 0) trip.ScheduledArrival = trip.ScheduledArrival.AddDays(1);
                            FreightGateway.AddScheduledOutboundFreight(trip);
                            Refresh();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete) {
                    if (this.csCancel.Enabled) Cancel();
                }
            }
            catch (ArgumentException aex) { App.ReportError(new ApplicationException("Not a valid trip.",aex),true); }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void OnScheduleAfterCellUpdate(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
            try {
                //Apply cell rules
                switch (e.Cell.Column.Key.ToString()) {
                    case "NickName":
                    case "StopNickName":
                        //Max 8 chars (i.e. 3 char mnemonic plus 5 char store#)
                        if (e.Cell.Text.Length > 8) {
                            e.Cell.Value = e.Cell.Text.Substring(0,8);
                            e.Cell.SelStart = 8;
                        }
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnScheduleAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //
            try {
                this.grdSchedule.Update();
                int id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                DispatchDataset.OutboundScheduleTableRow trip = (DispatchDataset.OutboundScheduleTableRow)this.mSchedule.OutboundScheduleTable.Select("ID=" + id)[0];
                FreightGateway.ChangeScheduledOutboundFreight(trip);
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        #region Drag/Drop Services: OnDragOver(), OnDragDrop()
        private void OnDragOver(object sender,System.Windows.Forms.DragEventArgs e) {
            //Event handler for dragging over the grid
            try {
                //Enable appropriate drag drop effect
                //NOTE: Cannot COPY or MOVE to self
                UltraGrid oGrid = (UltraGrid)sender;
                DataObject oData = (DataObject)e.Data;
                if (CanLoadTemplates && !oGrid.Focused && oData.GetDataPresent(DataFormats.StringFormat,true)) {
                    switch (e.KeyState) {
                        case KEYSTATE_SHIFT: e.Effect = (!oGrid.Focused) ? DragDropEffects.Move : DragDropEffects.None; break;
                        case KEYSTATE_CTL: e.Effect = (!oGrid.Focused) ? DragDropEffects.None : DragDropEffects.None; break;
                        default: e.Effect = (!oGrid.Focused) ? DragDropEffects.Move : DragDropEffects.None; break;
                    }
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDragDrop(object sender,System.Windows.Forms.DragEventArgs e) {
            //Event handler for dropping onto the grid
            try {
                DataObject oData = (DataObject)e.Data;
                if (oData.GetDataPresent(DataFormats.StringFormat,true)) {
                    for (int i = 0;i < this.mTemplates.OutboundScheduleTable.Rows.Count;i++) {
                        if (this.mTemplates.OutboundScheduleTable[i].Selected == true) {
                            DispatchDataset.OutboundScheduleTableRow trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                            trip.ItemArray = this.mTemplates.OutboundScheduleTable[i].ItemArray;
                            trip.CreateUserID = Environment.UserName;
                            trip.Created = DateTime.Now;
                            switch (this.cboSchedule.SelectedItem.ToString()) {
                                case "Today": trip.ScheduleDate = DateTime.Today; break;
                                case "Advanced": trip.ScheduleDate = DateTime.Today.AddDays(1); break;
                            }
                            trip.ScheduledArrival = trip.ScheduleDate.Date + trip.ScheduledArrival.TimeOfDay;
                            trip.ScheduledDeparture = trip.ScheduleDate.Date + trip.ScheduledDeparture.TimeOfDay;
                            if (trip.ScheduledArrival.CompareTo(trip.ScheduledDeparture) < 0) trip.ScheduledArrival = trip.ScheduledArrival.AddDays(1);
                            trip.IsTemplate = false;
                            FreightGateway.AddScheduledOutboundFreight(trip);
                        }
                    }
                    Refresh();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        #endregion
        private void OnScheduleDoubleClick(object sender,EventArgs e) { if (this.csOpen.Enabled && this.grdSchedule.ActiveCell != null && !this.grdSchedule.ActiveCell.IsInEditMode) this.csOpen.PerformClick(); }
        #endregion
        #region Templates: OnTemplatesMouseDown(), OnTemplatesDoubleClick()
        private void OnTemplatesMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if (uiElement != null) {
                    object oContext = uiElement.GetContext(typeof(UltraGridRow));
                    if (oContext != null) {
                        if (e.Button == MouseButtons.Left) {
                            this.mIsDragging = true;
                        }
                        else if (e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if (!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if (uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) {
                            grid.Selected.Rows.Clear();
                            grid.ActiveRow = null;
                        }
                        //else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                        //    if (grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnTemplatesDoubleClick(object sender,EventArgs e) { if (this.csTempOpen.Enabled) this.csTempOpen.PerformClick(); }
        #region Drag/Drop Services: OnDragDropMouseMove(), OnDragDropMouseUp()
        private void OnDragDropMouseMove(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Start drag\drop if user is dragging
            DataObject oData = null;
            try {
                switch (e.Button) {
                    case MouseButtons.Left:
                        UltraGrid oGrid = (UltraGrid)sender;
                        if (this.mIsDragging) {
                            //Initiate drag drop operation from the grid source
                            if (oGrid.Focused && oGrid.Selected.Rows.Count > 0) {
                                oData = new DataObject();
                                oData.SetData("");
                                DragDropEffects effect = oGrid.DoDragDrop(oData,DragDropEffects.All);
                                this.mIsDragging = false;

                                //After the drop- handled by drop code
                                switch (effect) {
                                    case DragDropEffects.Move: break;
                                    case DragDropEffects.Copy: break;
                                }
                            }
                        }
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDragDropMouseUp(object sender,System.Windows.Forms.MouseEventArgs e) { this.mIsDragging = false; }
        #endregion
        #endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            dlgOutboundFreight dlg = null;
            DispatchDataset.OutboundScheduleTableRow trip = null;
            int id = 0;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "csNew":
                        trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                        switch (this.cboSchedule.SelectedItem.ToString()) {
                            case "Today": trip.ScheduleDate = DateTime.Today; break;
                            case "Advanced": trip.ScheduleDate = DateTime.Today.AddDays(1); break;
                        }
                        trip.Origin = "ARGIX LOGISTICS NATIONAL";
                        trip.CarrierName = "ARGIX";
                        trip.IsTemplate = false;
                        dlg = new dlgOutboundFreight(trip);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddScheduledOutboundFreight(trip);
                            Refresh();
                        }
                        break;
                    case "csOpen":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        DispatchDataset.OutboundScheduleTableRow _freight = (DispatchDataset.OutboundScheduleTableRow)this.mSchedule.OutboundScheduleTable.Select("ID=" + id)[0];
                        trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                        trip.ItemArray = _freight.ItemArray;
                        dlg = new dlgOutboundFreight(trip);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangeScheduledOutboundFreight(trip);
                            Refresh();
                        }
                        break;
                    case "csClone":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        trip = (DispatchDataset.OutboundScheduleTableRow)this.mSchedule.OutboundScheduleTable.Select("ID=" + id)[0];
                        DispatchDataset.OutboundScheduleTableRow clone = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                        switch (this.cboSchedule.SelectedItem.ToString()) {
                            case "Today": clone.ScheduleDate = DateTime.Today; break;
                            case "Advanced": clone.ScheduleDate = DateTime.Today.AddDays(1); break;
                        }
                        clone.ScheduledDeparture = clone.ScheduleDate.Date + trip.ScheduledDeparture.TimeOfDay;
                        clone.Origin = trip.Origin;
                        if (!trip.IsOriginLocationNull()) clone.OriginLocation = trip.OriginLocation;
                        clone.Destination = trip.Destination;
                        if (!trip.IsDestinationLocationNull()) clone.DestinationLocation = trip.DestinationLocation;
                        clone.ScheduledArrival = clone.ScheduleDate.Date + trip.ScheduledArrival.TimeOfDay;
                        if (!trip.IsCommentsNull()) clone.Comments = trip.Comments;
                        clone.IsTemplate = false;
                        dlg = new dlgOutboundFreight(clone);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddScheduledOutboundFreight(clone);
                            Refresh();
                        }
                        break;
                    case "csCancel":
                        Cancel();
                        break;
                    case "csRefresh": Refresh(); break;

                    case "csTempNew":
                        trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                        trip.IsTemplate = true;
                        dlg = new dlgOutboundFreight(trip,true);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddScheduledOutboundFreight(trip);
                            Refresh();
                        }
                        break;
                    case "csTempOpen":
                        id = Convert.ToInt32(this.grdTemplates.ActiveRow.Cells["ID"].Value);
                        DispatchDataset.OutboundScheduleTableRow _template = (DispatchDataset.OutboundScheduleTableRow)this.mTemplates.OutboundScheduleTable.Select("ID=" + id)[0];
                        trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                        trip.ItemArray = _template.ItemArray;
                        dlg = new dlgOutboundFreight(trip,true);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangeScheduledOutboundFreight(trip);
                            Refresh();
                        }
                        break;
                    case "csTempCancel":
                        id = Convert.ToInt32(this.grdTemplates.ActiveRow.Cells["ID"].Value);
                        if (MessageBox.Show(this,"Are you sure you want to cancel trip " + id.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                            FreightGateway.CancelScheduledOutboundFreight(id,DateTime.Now,Environment.UserName);
                            Refresh();
                        }
                        break;
                    case "csTempLoad":
				        for(int i=0; i<this.mTemplates.OutboundScheduleTable.Rows.Count; i++) {
                            if (this.mTemplates.OutboundScheduleTable[i].Selected == true) {
                                trip = new DispatchDataset().OutboundScheduleTable.NewOutboundScheduleTableRow();
                                trip.ItemArray = this.mTemplates.OutboundScheduleTable[i].ItemArray;
                                trip.CreateUserID = Environment.UserName;
                                trip.Created = DateTime.Now;
                                switch (this.cboSchedule.SelectedItem.ToString()) {
                                    case "Today": trip.ScheduleDate = DateTime.Today; break;
                                    case "Advanced": trip.ScheduleDate = DateTime.Today.AddDays(1); break;
                                }
                                trip.ScheduledDeparture = trip.ScheduleDate.Date + trip.ScheduledDeparture.TimeOfDay;
                                trip.ScheduledArrival = trip.ScheduleDate.Date + trip.ScheduledArrival.TimeOfDay;
                                if (trip.ScheduledArrival.CompareTo(trip.ScheduledDeparture) < 0) trip.ScheduledArrival = trip.ScheduledArrival.AddDays(1);
                                trip.IsTemplate = false;
                                FreightGateway.AddScheduledOutboundFreight(trip);
                            }
				        }
				        Refresh();
                        break;
                    case "csTempRefresh": Refresh(); break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        #endregion
        #region Local Services: setUserServices(), reportStatus(), ColumnHeaders
        private void setUserServices() {
            //Set user services
            try {
                bool canCancel = false;
                bool canManage = this.cboSchedule.SelectedItem.ToString() == "Today" || this.cboSchedule.SelectedItem.ToString() == "Advanced";
                try {
                    int id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                    DispatchDataset.OutboundScheduleTableRow trip = (DispatchDataset.OutboundScheduleTableRow)this.mSchedule.OutboundScheduleTable.Select("ID=" + id)[0];
                    canCancel = trip.IsActualDepartureNull();
                }
                catch { }

                this.cboSchedule.Enabled = true;
                this.csNew.Enabled = canManage && this.grdSchedule.Focused && (RoleServiceGateway.IsDispatchSupervisor);
                this.csOpen.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null;
                this.csClone.Enabled = canManage && this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor);
                this.csCancel.Enabled = canManage && this.grdSchedule.Focused && canCancel && (RoleServiceGateway.IsDispatchSupervisor);
                this.csRefresh.Enabled = true;

                this.csTempNew.Enabled = this.grdTemplates.Focused && RoleServiceGateway.IsDispatchSupervisor;
                this.csTempOpen.Enabled = this.grdTemplates.Focused && this.grdTemplates.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor);
                this.csTempCancel.Enabled = this.grdTemplates.Focused && this.grdTemplates.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor);
                this.csTempLoad.Enabled = canManage && this.grdTemplates.Focused && this.grdTemplates.Selected.Rows.Count > 0 && (RoleServiceGateway.IsDispatchSupervisor);
                this.csTempRefresh.Enabled = true;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Application.DoEvents(); if (this.ServiceStatesChanged != null) this.ServiceStatesChanged(this,new EventArgs()); }
        }
        private void reportStatus(StatusEventArgs e) { if (this.StatusMessage != null) this.StatusMessage(this,e); }
        private string ColumnHeaders {
            get {
                MemoryStream ms = new MemoryStream();
                this.grdSchedule.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if(global::Argix.Properties.Settings.Default.LastVersion == App.Version && value.Length > 0) {
                    MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdSchedule.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                    //this.grdSchedule.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                }
            }
        }
        #endregion
        #region Template Services: OnTemplateSelected(), OnCloseTemplates(), OnEnterTemplates(), OnLeaveTemplates()
        private void OnTemplateSelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for change in template row selections
            try {
                //Clear current selections
                for (int i = 0;i < this.mTemplates.OutboundScheduleTable.Rows.Count;i++) this.mTemplates.OutboundScheduleTable[i].Selected = false;

                //Update all selected load templates as selected for Add
                for (int i = 0;i < this.grdTemplates.Selected.Rows.Count;i++) {
                    int templateID = int.Parse(this.grdTemplates.Selected.Rows[i].Cells["ID"].Value.ToString());
                    for (int j = 0;j < this.mTemplates.OutboundScheduleTable.Rows.Count;j++) {
                        if (this.mTemplates.OutboundScheduleTable[j].ID == templateID) {
                            this.mTemplates.OutboundScheduleTable[j].Selected = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnCloseTemplates(object sender,System.EventArgs e) {
            //Event handler to close log windows
            this.scWin.Panel2Collapsed = true;
            setUserServices();
        }
        private void OnEnterTemplates(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblTemplateHeader.BackColor = this.lblCloseTemplates.BackColor = SystemColors.ActiveCaption;
                this.lblTemplateHeader.ForeColor = this.lblCloseTemplates.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnLeaveTemplates(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblTemplateHeader.BackColor = this.lblCloseTemplates.BackColor = SystemColors.Control;
                this.lblTemplateHeader.ForeColor = this.lblCloseTemplates.ForeColor = SystemColors.ControlText;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region AutoRefresh Services: OnAutoRefreshCompleted()
        public void OnAutoRefreshCompleted(object sender,RunWorkerCompletedEventArgs e) {
            //
            try {
                DispatchDataset ds = null;
                if (this.InvokeRequired) {
                    this.Invoke(new RunWorkerCompletedEventHandler(OnAutoRefreshCompleted),new object[] { sender,e });
                }
                else {
                    ds = (DispatchDataset)e.Result;
                    if (this.grdSchedule.ActiveCell == null || !this.grdSchedule.ActiveCell.IsInEditMode) {
                        reportStatus(new StatusEventArgs("Refreshing Outbound Schedule..."));
                        this.mGridSvc.CaptureState();
                        lock (this.mSchedule) {
                            this.mSchedule.Clear();
                            this.mSchedule.Merge(ds);
                        }
                        this.mGridSvc.RestoreState();
                    }
                }
            }
            catch { }
        }
        #endregion
    }
    public class OutboundAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public OutboundAutoRefreshService(winOutbound postback) {
            //
            this.mTimer = new System.Windows.Forms.Timer();
            this.mTimer.Interval = global::Argix.Properties.Settings.Default.AutoRefreshTimer;
            this.mTimer.Tick += new EventHandler(OnTick);
            this.mWorker = new BackgroundWorker();
            this.mWorker.DoWork += new DoWorkEventHandler(OnAutoRefresh);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(postback.OnAutoRefreshCompleted);
        }
        public void Start() { this.mTimer.Start(); }
        public void Stop() { this.mTimer.Stop(); }

        private void OnTick(object sender,EventArgs e) {
            //Event handler for timer tick event
            try { if (!this.mWorker.IsBusy) this.mWorker.RunWorkerAsync(); }
            catch { }
        }
        private void OnAutoRefresh(object sender,DoWorkEventArgs e) {
            //Event handler for background worker thread DoWork event; runs on worker thread
            try { e.Result = FreightGateway.ViewOutboundSchedule(); }
            catch { }
        }
    }
}
