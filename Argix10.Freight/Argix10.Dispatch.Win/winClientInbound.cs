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
    public partial class winClientInbound:Form, ISchedule {
        //Members
        private UltraGridSvc mGridSvc = null, mGridTSvc=null;
        private bool mIsDragging = false;
        private ClientInboundAutoRefreshService mAutoRefreshSvc = null;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winClientInbound() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
                this.mGridTSvc = new UltraGridSvc(this.grdTemplates);
                this.mAutoRefreshSvc = new ClientInboundAutoRefreshService(this);
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
                if (MessageBox.Show(this,"Are you sure you want to cancel appointment " + id.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                    FreightGateway.CancelClientInboundFreight(id,DateTime.Now,Environment.UserName);
                    Refresh();
                }
            }
        }
        public bool CanSave { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mSchedule, "ClientInboundScheduleTable", filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing this schedule..."));
                string caption = "Client Inbound Sheet";
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": caption = "Client Inbound Sheet " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Client Inbound Sheet " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); break;
                    case "Advanced": caption = "Client Inbound Sheet Advanced"; break;
                    case "Archive": caption = "Client Inbound Sheet Archive"; break;
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
                string caption = "Client Inbound Sheet";
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": caption = "Client Inbound Sheet " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Client Inbound Sheet " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); break;
                    case "Advanced": caption = "Client Inbound Sheet Advanced"; break;
                    case "Archive": caption = "Client Inbound Sheet Archive"; break;
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
                reportStatus(new StatusEventArgs("Refreshing Client Inbound Schedule..."));
                this.mGridSvc.CaptureState();
                this.mSchedule.Clear();
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": this.mSchedule.Merge(FreightGateway.ViewClientInboundSchedule()); break;
                    case "Yesterday": this.mSchedule.Merge(FreightGateway.ViewClientInboundScheduleYesterday()); break;
                    case "Advanced": this.mSchedule.Merge(FreightGateway.ViewClientInboundScheduleAdvanced()); break;
                    case "Archive": this.mSchedule.Merge(FreightGateway.ViewClientInboundScheduleArchive()); break;
                }
                this.mGridSvc.RestoreState();

                this.mGridTSvc.CaptureState();
                this.mTemplates.Clear();
                this.mTemplates.Merge(FreightGateway.ViewClientInboundScheduleTemplates());
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
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ClientInboundColumnHeaders;
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
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Client Inbound Schedule..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Client Inbound Schedule..."));
                    this.mAutoRefreshSvc.Stop();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            //Turn off auto refresh
            try {
                reportStatus(new StatusEventArgs("Stopping auto-refresh on Client Inbound Schedule..."));
                this.mAutoRefreshSvc.Stop();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
                if (!e.Cancel) {
                    //Save settings
                    global::Argix.Properties.Settings.Default.ClientInboundColumnHeaders = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csMain.Font = this.csTemp.Font = this.Font; } catch { } }
        private void OnScheduleChanged(object sender,EventArgs e) {
            //Event handler fr change in schedule
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Today") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Client Inbound Schedule..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Client Inbound Schedule..."));
                    this.mAutoRefreshSvc.Stop();
                }
                this.grdSchedule.ActiveRow = null;
                this.grdSchedule.Focus();
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
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
                e.Layout.Bands[0].Columns["CarrierName"].ValueList = this.uddCarriers;
                e.Layout.Bands[0].Columns["DriverName"].ValueList = this.uddDrivers;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnScheduleInitializeRow(object sender,InitializeRowEventArgs e) {
            //Event handler for intialize row event
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Today" && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk) && 
                    e.Row.Cells["ActualArrival"].Value == DBNull.Value && e.Row.Cells["Cancelled"].Value.ToString().Trim().Length == 0) {
                    //Date may not be same as schedule date
                    DateTime d = DateTime.Parse(e.Row.Cells["ScheduledArrival"].Value.ToString());
                    DateTime scheduledArrival = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,d.Hour,d.Minute,d.Second);
                    if (scheduledArrival.AddMinutes(global::Argix.Properties.Settings.Default.AlertWindow) < DateTime.Now) e.Row.Cells["ActualArrival"].Appearance.BackColor = Color.Red;
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
                    case "VendorName":
                    case "ConsigneeName":
                    case "ScheduledArrival":
                    case "CarrierName":
                    case "DriverName":
                    case "Amount":
                    case "AmountType":
                    case "FreightType":
                    case "SortDate":
                    case "IsLiveUnload":
                    case "TrailerNumber":
                    case "ActualArrival":
                    case "TDSNumber":
                    case "TDSCreateUserID":
                    case "In":
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
                        DataRow[] appointments = this.mSchedule.ClientInboundScheduleTable.Select("ID=" + id);
                        Clipboard.SetData(DataFormats.Serializable,appointments[0].ItemArray);
                    }
                }
                else if (e.Control && e.KeyCode == Keys.V) {
                    if (this.cboSchedule.SelectedItem.ToString() == "Today" || this.cboSchedule.SelectedItem.ToString() == "Advanced") {
                        Cursor.Current = Cursors.WaitCursor;
                        object o = Clipboard.GetData(DataFormats.Serializable);
                        if (o != null) {
                            Cursor.Current = Cursors.WaitCursor;
                            object[] os = (object[])o;
                            DispatchDataset.ClientInboundScheduleTableRow appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                            appointment.ItemArray = os;
                            appointment.CreateUserID = Environment.UserName;
                            appointment.Created = DateTime.Now;
                            switch (this.cboSchedule.SelectedItem.ToString()) {
                                case "Today": appointment.ScheduleDate = DateTime.Today; break;
                                case "Advanced": appointment.ScheduleDate = DateTime.Today.AddDays(1); break;
                            }
                            appointment.ScheduledArrival = appointment.ScheduleDate.Date + appointment.ScheduledArrival.TimeOfDay;
                            FreightGateway.AddClientInboundFreight(appointment);
                            Refresh();
                        }
                    }
                }
                else if (e.KeyCode == Keys.Delete) {
                    if (this.csCancel.Enabled) Cancel();
                }
            }
            catch (ArgumentException aex) { App.ReportError(new ApplicationException("Not a valid appointment.", aex),true); }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void OnScheduleAfterCellUpdate(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
            try {
                //Apply cell rules
                switch (e.Cell.Column.Key.ToString()) {
                    case "NickName":
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
                DispatchDataset.ClientInboundScheduleTableRow appointment = (DispatchDataset.ClientInboundScheduleTableRow)this.mSchedule.ClientInboundScheduleTable.Select("ID=" + id)[0];
                FreightGateway.ChangeClientInboundFreight(appointment);
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnScheduleDoubleClick(object sender,EventArgs e) { if (this.csOpen.Enabled && this.grdSchedule.ActiveCell != null && !this.grdSchedule.ActiveCell.IsInEditMode) this.csOpen.PerformClick(); }
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
                    for (int i = 0;i < this.mTemplates.ClientInboundScheduleTable.Rows.Count;i++) {
                        if (this.mTemplates.ClientInboundScheduleTable[i].Selected == true) {
                            DispatchDataset.ClientInboundScheduleTableRow appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                            appointment.ItemArray = this.mTemplates.ClientInboundScheduleTable[i].ItemArray;
                            appointment.CreateUserID = Environment.UserName;
                            appointment.Created = DateTime.Now;
                            switch (this.cboSchedule.SelectedItem.ToString()) {
                                case "Today": appointment.ScheduleDate = DateTime.Today; break;
                                case "Advanced": appointment.ScheduleDate = DateTime.Today.AddDays(1); break;
                            }
                            appointment.ScheduledArrival = appointment.ScheduleDate.Date + appointment.ScheduledArrival.TimeOfDay;
                            appointment.IsTemplate = false;
                            FreightGateway.AddClientInboundFreight(appointment);
                        }
                    }
                    Refresh();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        #endregion
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
            dlgClientInboundFreight dlg = null;
            DispatchDataset.ClientInboundScheduleTableRow appointment = null;
            int id = 0;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "csNew":
                        appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                        switch (this.cboSchedule.SelectedItem.ToString()) {
                            case "Today": appointment.ScheduleDate = DateTime.Today; break;
                            case "Advanced": appointment.ScheduleDate = DateTime.Today.AddDays(1); break;
                        }
                        appointment.ConsigneeName = "ARGIX LOGISTICS NATIONAL";
                        appointment.CarrierName = "ARGIX";
                        appointment.IsTemplate = false;
                        dlg = new dlgClientInboundFreight(appointment);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddClientInboundFreight(appointment);
                            Refresh();
                        }
                        break;
                    case "csOpen":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        DispatchDataset.ClientInboundScheduleTableRow  _freight = (DispatchDataset.ClientInboundScheduleTableRow)this.mSchedule.ClientInboundScheduleTable.Select("ID=" + id)[0];
                        appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                        appointment.ItemArray = _freight.ItemArray;
                        dlg = new dlgClientInboundFreight(appointment);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangeClientInboundFreight(appointment);
                            Refresh();
                        }
                        break;
                    case "csClone":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        appointment = (DispatchDataset.ClientInboundScheduleTableRow)this.mSchedule.ClientInboundScheduleTable.Select("ID=" + id)[0];
                        DispatchDataset.ClientInboundScheduleTableRow clone = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                        switch (this.cboSchedule.SelectedItem.ToString()) {
                            case "Today": clone.ScheduleDate = DateTime.Today; break;
                            case "Advanced": clone.ScheduleDate = DateTime.Today.AddDays(1); break;
                        }
                        clone.VendorName = appointment.VendorName;
                        clone.ConsigneeName = appointment.ConsigneeName;
                        clone.ScheduledArrival = clone.ScheduleDate.Date + appointment.ScheduledArrival.TimeOfDay;
                        if (!appointment.IsAmountNull()) clone.Amount = appointment.Amount;
                        if (!appointment.IsAmountTypeNull()) clone.AmountType = appointment.AmountType;
                        if (!appointment.IsFreightTypeNull()) clone.FreightType = appointment.FreightType;
                        if (!appointment.IsCommentsNull()) clone.Comments = appointment.Comments;
                        clone.IsTemplate = false;
                        dlg = new dlgClientInboundFreight(clone);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddClientInboundFreight(clone);
                            Refresh();
                        }
                        break;
                    case "csCancel":
                        Cancel();
                        break;
                    case "csRefresh": Refresh(); break;
                    case "csTempNew":
                        appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                        appointment.ConsigneeName = "ARGIX LOGISTICS NATIONAL";
                        appointment.IsTemplate = true;
                        dlg = new dlgClientInboundFreight(appointment, true);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddClientInboundFreight(appointment);
                            Refresh();
                        }
                        break;
                    case "csTempOpen":
                        id = Convert.ToInt32(this.grdTemplates.ActiveRow.Cells["ID"].Value);
                        DispatchDataset.ClientInboundScheduleTableRow _template = (DispatchDataset.ClientInboundScheduleTableRow)this.mTemplates.ClientInboundScheduleTable.Select("ID=" + id)[0];
                        appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                        appointment.ItemArray = _template.ItemArray;
                        dlg = new dlgClientInboundFreight(appointment,true);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangeClientInboundFreight(appointment);
                            Refresh();
                        }
                        break;
                    case "csTempCancel":
                        id = Convert.ToInt32(this.grdTemplates.ActiveRow.Cells["ID"].Value);
                        if (MessageBox.Show(this,"Are you sure you want to cancel appointment template " + id.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                            FreightGateway.CancelClientInboundFreight(id,DateTime.Now,Environment.UserName);
                            Refresh();
                        }
                        break;
                    case "csTempLoad":
				        for(int i=0; i<this.mTemplates.ClientInboundScheduleTable.Rows.Count; i++) {
                            if (this.mTemplates.ClientInboundScheduleTable[i].Selected == true) {
                                appointment = new DispatchDataset().ClientInboundScheduleTable.NewClientInboundScheduleTableRow();
                                appointment.ItemArray = this.mTemplates.ClientInboundScheduleTable[i].ItemArray;
                                appointment.CreateUserID = Environment.UserName;
                                appointment.Created = DateTime.Now;
                                switch (this.cboSchedule.SelectedItem.ToString()) {
                                    case "Today": appointment.ScheduleDate = DateTime.Today; break;
                                    case "Advanced": appointment.ScheduleDate = DateTime.Today.AddDays(1); break;
                                }
                                appointment.ScheduledArrival = appointment.ScheduleDate.Date + appointment.ScheduledArrival.TimeOfDay;
                                appointment.IsTemplate = false;
                                FreightGateway.AddClientInboundFreight(appointment);
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
                    DispatchDataset.ClientInboundScheduleTableRow appointment = (DispatchDataset.ClientInboundScheduleTableRow)this.mSchedule.ClientInboundScheduleTable.Select("ID=" + id)[0];
                    canCancel = appointment.IsActualArrivalNull();
                }
                catch { }

                this.cboSchedule.Enabled = true;
                this.csNew.Enabled = canManage && this.grdSchedule.Focused && (RoleServiceGateway.IsDispatchSupervisor);
                this.csOpen.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null;
                this.csClone.Enabled = canManage && this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor);
                this.csCancel.Enabled = canManage && this.grdSchedule.Focused && canCancel && (RoleServiceGateway.IsDispatchSupervisor);
                this.csRefresh.Enabled = true;

                this.csTempNew.Enabled = this.grdTemplates.Focused && (RoleServiceGateway.IsDispatchSupervisor);
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
                for (int i = 0;i < this.mTemplates.ClientInboundScheduleTable.Rows.Count;i++) this.mTemplates.ClientInboundScheduleTable[i].Selected = false;

                //Update all selected load templates as selected for Add
                for (int i = 0;i < this.grdTemplates.Selected.Rows.Count;i++) {
                    int templateID = int.Parse(this.grdTemplates.Selected.Rows[i].Cells["ID"].Value.ToString());
                    for (int j = 0;j < this.mTemplates.ClientInboundScheduleTable.Rows.Count;j++) {
                        if (this.mTemplates.ClientInboundScheduleTable[j].ID == templateID) {
                            this.mTemplates.ClientInboundScheduleTable[j].Selected = true;
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
            catch (Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
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
                        reportStatus(new StatusEventArgs("Refreshing Client Inbound Schedule..."));
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

    public class ClientInboundAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public ClientInboundAutoRefreshService(winClientInbound postback) {
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
            try { e.Result = FreightGateway.ViewClientInboundSchedule(); } catch { }
        }
    }
}
