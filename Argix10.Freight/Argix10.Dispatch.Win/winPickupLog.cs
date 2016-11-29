using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Configuration;
using System.Text;
using System.Windows.Forms;
using Argix.Security;
using Argix.Windows;
using Argix.Windows.Printers;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
    //
    public partial class winPickupLog:Form,ISchedule {
        //Members
        private WinPrinter mPrinter = null;
        private UltraGridSvc mGridSvc = null,mGridTSvc = null;
        private bool mIsDragging = false;
        private PickupLogAutoRefreshService mAutoRefreshSvc = null;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winPickupLog() {
            //Constructor
            try {
                InitializeComponent();
                this.mPrinter = new WinPrinter();
                this.mPrinter.Doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(OnPrintPage);
                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
                this.mGridTSvc = new UltraGridSvc(this.grdTemplates);
                this.mAutoRefreshSvc = new PickupLogAutoRefreshService(this);
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
        public void Cancel() { this.csCancel.PerformClick(); }
        public bool CanSave { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mSchedule,"PickupLogTable",filename); }
        public bool CanExport { get { return this.csExport.Enabled; } }
        public void Export() { Export(ConfigurationManager.AppSettings["ExportPath" + Program.TerminalCode] + "DailyPickups.txt"); }
        public void Export(string filename) {
            DispatchDataset log = new DispatchDataset();
            DateTime scheduleDate = DateTime.Today;
            switch (this.cboSchedule.SelectedItem.ToString()) {
                case "Today": scheduleDate = DateTime.Today; break;
                case "Tomorrow": scheduleDate = DateTime.Today.AddDays(1); break;
                case "Yesterday": scheduleDate = DateTime.Today.AddDays(-1); break;
            }
            log.Merge(this.mSchedule.PickupLogTable.Select("ScheduleDate='" + scheduleDate + "'"));
            //for (int i = 0;i < this.grdSchedule.Selected.Rows.Count;i++) {
            //    int requestID = Convert.ToInt32(this.grdSchedule.Selected.Rows[i].Cells["RequestID"].Value);
            //    DispatchDataset.PickupLogTableRow request = (DispatchDataset.PickupLogTableRow)this.mSchedule.PickupLogTable.Select("RequestID=" + requestID)[0];
            //    DispatchDataset.PickupLogTableRow pr = log.PickupLogTable.NewPickupLogTableRow();
            //    pr.ItemArray = request.ItemArray;
            //    log.PickupLogTable.AddPickupLogTableRow(pr);
            //}
            log.AcceptChanges();
            new Argix.Terminals.RoadshowExporter().Export(filename,log);
        }
        public bool CanPrint { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing pickup log..."));
                string caption = "Pickup Requests for  " + this.cboSchedule.SelectedItem.ToString();
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
                string caption = "Pickup Requests ";
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": caption = "Pickup Requests " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Pickup Requests " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); break;
                    case "Tomorrow": caption = "Pickup Requests " + DateTime.Today.AddDays(1).ToString("MM/dd/yyyy"); break;
                    case "Advanced": caption = "Pickup Requests Advanced"; break;
                    case "Archive": caption = "Pickup Requests Archive"; break;
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
                reportStatus(new StatusEventArgs("Refreshing Pickup Log..."));
                this.mGridSvc.CaptureState();
                this.mSchedule.Clear();
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Today": this.mSchedule.Merge(FreightGateway.ViewPickupLog()); break;
                    case "Yesterday": this.mSchedule.Merge(FreightGateway.ViewPickupLogYesterday()); break;
                    case "Tomorrow": this.mSchedule.Merge(FreightGateway.ViewPickupLogTomorrow()); break;
                    case "Advanced": this.mSchedule.Merge(FreightGateway.ViewPickupLogAdvanced()); break;
                    case "Archive": this.mSchedule.Merge(FreightGateway.ViewPickupLogArchive()); break;
                }
                this.mGridSvc.RestoreState();

                this.mGridTSvc.CaptureState();
                this.mTemplates.Clear();
                this.mTemplates.Merge(FreightGateway.ViewPickupLogTemplates());
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
                this.grdSchedule.DisplayLayout.Bands[0].Columns["RequestID"].CellActivation = Activation.NoEdit;
                this.grdSchedule.DisplayLayout.Bands[0].Columns["Created"].SortIndicator = SortIndicator.Descending;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.PickupLogColumnHeaders;
                this.bsAgents.DataSource = FreightGateway.GetAgents();
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
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Pickup Log..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Pickup Log..."));
                    this.mAutoRefreshSvc.Stop();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            //Turn off auto refresh
            try {
                reportStatus(new StatusEventArgs("Stopping auto-refresh on Pickup Log..."));
                this.mAutoRefreshSvc.Stop();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
                if (!e.Cancel) {
                    //Save settings
                    global::Argix.Properties.Settings.Default.PickupLogColumnHeaders = this.ColumnHeaders;
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
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Pickup Log..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Pickup Log..."));
                    this.mAutoRefreshSvc.Stop();
                }
                this.grdSchedule.ActiveRow = null;
                this.grdSchedule.Focus();
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnAgentBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddAgents.DisplayLayout.Bands[0].Columns["Name"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["Terminal"].Width;
            } catch { }
        }
        private void OnAgentSelected(object sender,Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            try {
                if (e.Row != null) {
                    this.grdSchedule.ActiveRow.Cells["TerminalNumber"].Value = e.Row.Cells["Number"].Text;
                }
            } catch { }
        }
        private void OnDriverBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddDrivers.DisplayLayout.Bands[0].Columns["Name"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["DriverName"].Width;
            } catch { }
        }
        #region Schedule: OnScheduleInitializeLayout(), OnScheduleInitializeRow(), OnScheduleBeforeRowFilterDropDownPopulate(), OnScheduleMouseDown(), OnScheduleAfterSelectChange(), ...
        private void OnScheduleInitializeLayout(object sender,InitializeLayoutEventArgs e) { 
            //
            try {
                e.Layout.Bands[0].Columns["Terminal"].ValueList = this.uddAgents;
                e.Layout.Bands[0].Columns["DriverName"].ValueList = this.uddDrivers;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnScheduleInitializeRow(object sender,InitializeRowEventArgs e) {
            //Event handler for intialize row event
            try {
                if(e.Row.Cells["Cancelled"].Value.ToString().Trim().Length > 0) {
                    e.Row.Appearance.ForeColor = System.Drawing.Color.DarkGray;
                    e.Row.Activation = Activation.NoEdit;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Information); }
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
                    case "ClientNumber":
                    case "Client":
                    case "ShipperNumber":
                    case "Shipper":
                    case "ShipperAddress":
                    case "ShipperPhone":
                    case "WindowOpen":
                    case "WindowClose":
                    case "Amount":
                    case "AmountType":
                    case "FreightType":
                    case "Weight":
                    case "TerminalNumber":
                    case "Terminal":
                    case "DriverName":
                    case "ActualPickup":
                    case "Comments":
                        e.Cell.Activation = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || RoleServiceGateway.IsClientRep || RoleServiceGateway.IsBBBClerk ? Activation.AllowEdit : Activation.NoEdit;
                        break;
                    case "OrderType":
                        int terminalNumber = 0;
                        if(this.grdSchedule.ActiveRow.Cells["TerminalNumber"].Value != DBNull.Value)
                            terminalNumber = int.Parse(this.grdSchedule.ActiveRow.Cells["TerminalNumber"].Value.ToString());
                        if(terminalNumber > 0)
                            this.bsDrivers.DataSource = Argix.Terminals.TerminalGateway.GetDrivers(terminalNumber);
                        else
                            this.bsDrivers.DataSource = null;
                        e.Cell.Activation = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || RoleServiceGateway.IsClientRep || RoleServiceGateway.IsBBBClerk ? Activation.AllowEdit : Activation.NoEdit;
                        break;
                    case "RequestID":
                    case "Created":
                    case "CreateUserID":
                    case "ScheduleDate":
                    case "CallerName":
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
                    if (this.grdSchedule.ActiveRow != null) {
                        int id = int.Parse(this.grdSchedule.ActiveRow.Cells["RequestID"].Value.ToString());
                        DataRow[] requests = this.mSchedule.PickupLogTable.Select("RequestID=" + id);
                        Clipboard.SetData(DataFormats.Serializable,requests[0].ItemArray);
                    }
                }
                else if (e.Control && e.KeyCode == Keys.V) {
                    object o = Clipboard.GetData(DataFormats.Serializable);
                    if (o != null) {
                        Cursor.Current = Cursors.WaitCursor;
                        object[] os = (object[])o;
                        DispatchDataset.PickupLogTableRow request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        request.ItemArray = os;
                        FreightGateway.AddPickupRequest(request);
                        Refresh();
                    }
                }
                else if (e.KeyCode == Keys.Delete) {
                    if (this.csCancel.Enabled) {
                        int id = int.Parse(this.grdSchedule.ActiveRow.Cells["RequestID"].Value.ToString());
                        if (MessageBox.Show(this,"Are you sure you want to cancel request " + id.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                            FreightGateway.CancelPickupRequest(id,DateTime.Now,Environment.UserName);
                            Refresh();
                        }
                    }
                }
            }
            catch (ArgumentException aex) { App.ReportError(new ApplicationException("Not a valid pickup request.",aex),true); }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
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
                int requestID = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["RequestID"].Value);
                DispatchDataset.PickupLogTableRow request = (DispatchDataset.PickupLogTableRow)this.mSchedule.PickupLogTable.Select("RequestID=" + requestID)[0];
                FreightGateway.ChangePickupRequest(request);
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
                    for (int i = 0;i < this.mTemplates.PickupLogTable.Rows.Count;i++) {
                        if (this.mTemplates.PickupLogTable[i].Selected == true) {
                            DispatchDataset.PickupLogTableRow request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                            request.ItemArray = this.mTemplates.PickupLogTable[i].ItemArray;
                            request.CreateUserID = Environment.UserName;
                            request.Created = DateTime.Now;
                            switch (this.cboSchedule.SelectedItem.ToString()) {
                                case "Today": request.ScheduleDate = DateTime.Today; break;
                                case "Tomorrow": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                                case "Advanced": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                            }
                            request.IsTemplate = false;
                            FreightGateway.AddPickupRequest(request);
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
                            //OnDragDropMouseDown(sender, e);
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
        #region User Services: OnItemClick(), OnPrintPage()
        private void OnItemClick(object sender,System.EventArgs e) {
            dlgPickupRequest dlg = null;
            DispatchDataset.PickupLogTableRow request = null;
            int requestID = 0;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "csNew":
                        request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        switch (this.cboSchedule.SelectedItem.ToString()) {
                            case "Today": request.ScheduleDate = DateTime.Today; break;
                            case "Tomorrow": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                            case "Advanced": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                        }
                        request.IsTemplate = false;
                        dlg = new dlgPickupRequest(request);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) addPickupRequest(request);
                        break;
                    case "csOpen":
                        requestID = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["RequestID"].Value);
                        DispatchDataset.PickupLogTableRow _request = (DispatchDataset.PickupLogTableRow)this.mSchedule.PickupLogTable.Select("RequestID=" + requestID)[0];
                        request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        request.ItemArray = _request.ItemArray;
                        dlg = new dlgPickupRequest(request);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangePickupRequest(request);
                            Refresh();
                        }
                        break;
                    case "csClone":
                        requestID = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["RequestID"].Value);
                        request = (DispatchDataset.PickupLogTableRow)this.mSchedule.PickupLogTable.Select("RequestID=" + requestID)[0];
                        DispatchDataset.PickupLogTableRow clone = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        switch (this.cboSchedule.SelectedItem.ToString()) {
                            case "Today": clone.ScheduleDate = DateTime.Today; break;
                            case "Tomorrow": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                            case "Advanced": clone.ScheduleDate = DateTime.Today.AddDays(1); break;
                        }
                        if (!request.IsCallerNameNull()) clone.CallerName = request.CallerName;
                        if (!request.IsClientNull()) clone.Client = request.Client;
                        if (!request.IsClientNumberNull()) clone.ClientNumber = request.ClientNumber;
                        if (!request.IsShipperNull()) clone.Shipper = request.Shipper;
                        if (!request.IsShipperAddressNull()) clone.ShipperAddress = request.ShipperAddress;
                        if (!request.IsShipperNumberNull()) clone.ShipperNumber = request.ShipperNumber;
                        if (!request.IsWindowOpenNull()) clone.WindowOpen = request.WindowOpen;
                        if (!request.IsWindowCloseNull()) clone.WindowClose = request.WindowClose;
                        if (!request.IsCommentsNull()) clone.Comments = request.Comments;
                        if (!request.IsAmountNull()) clone.Amount = request.Amount;
                        if (!request.IsAmountTypeNull()) clone.AmountType = request.AmountType;
                        if (!request.IsWeightNull()) clone.Weight = request.Weight;
                        dlg = new dlgPickupRequest(clone);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) addPickupRequest(clone);
                        break;
                    case "csCancel":
                        requestID = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["RequestID"].Value);
                        if (MessageBox.Show(this,"Are you sure you want to cancel request " + requestID.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                            FreightGateway.CancelPickupRequest(requestID,DateTime.Now,Environment.UserName);
                            Refresh();
                        }
                        break;
                    case "csExport":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Export Pickup Orders...";
                        dlgSave.FileName = "DailyPickups";
                        dlgSave.CheckFileExists = false;
                        dlgSave.OverwritePrompt = false;
                        dlgSave.InitialDirectory = ConfigurationManager.AppSettings["ExportPath" + Program.TerminalCode];
                        if (dlgSave.ShowDialog(this) == DialogResult.OK) Export(dlgSave.FileName);
                        break;
                    case "csRefresh": Refresh(); break;

                    case "csTempNew":
                        request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        request.IsTemplate = true;
                        dlg = new dlgPickupRequest(request,true);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddPickupRequest(request);
                            Refresh();
                        }
                        break;
                    case "csTempOpen":
                        requestID = Convert.ToInt32(this.grdTemplates.ActiveRow.Cells["RequestID"].Value);
                        DispatchDataset.PickupLogTableRow _template = (DispatchDataset.PickupLogTableRow)this.mTemplates.PickupLogTable.Select("RequestID=" + requestID)[0];
                        request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        request.ItemArray = _template.ItemArray;
                        dlg = new dlgPickupRequest(request,true);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangePickupRequest(request);
                            Refresh();
                        }
                        break;
                    case "csTempCancel":
                        requestID = Convert.ToInt32(this.grdTemplates.ActiveRow.Cells["RequestID"].Value);
                        if (MessageBox.Show(this,"Are you sure you want to cancel request " + requestID.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                            FreightGateway.CancelPickupRequest(requestID,DateTime.Now,Environment.UserName);
                            Refresh();
                        }
                        break;
                    case "csTempLoad":
				        for(int i=0; i<this.mTemplates.PickupLogTable.Rows.Count; i++) {
                            if (this.mTemplates.PickupLogTable[i].Selected == true) {
                                request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                                request.ItemArray = this.mTemplates.PickupLogTable[i].ItemArray;
                                request.CreateUserID = Environment.UserName;
                                request.Created = DateTime.Now;
                                switch (this.cboSchedule.SelectedItem.ToString()) {
                                    case "Today": request.ScheduleDate = DateTime.Today; break;
                                    case "Tomorrow": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                                    case "Advanced": request.ScheduleDate = DateTime.Today.AddDays(1); break;
                                }
                                request.IsTemplate = false;
                                FreightGateway.AddPickupRequest(request);
                            }
				        }
				        Refresh();
                        break;
                    case "csTempRefresh": Refresh(); break;
                    case "csPrintRequest":
                        this.mPrinter.Print("PickupRequest"," ",true);
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnPrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
            //Provide the printing logic for the document
            try {
                //Print the current employee
                Font font = new Font("Courier New",8.25F,FontStyle.Regular,GraphicsUnit.Point,((System.Byte)(0)));
                float fX = e.MarginBounds.Left;
                float fY = e.MarginBounds.Top;
                StringFormat format = new StringFormat();
                float lineHeight = font.GetHeight(e.Graphics);

                int requestID = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["RequestID"].Value);
                DispatchDataset.PickupLogTableRow request = (DispatchDataset.PickupLogTableRow)this.mSchedule.PickupLogTable.Select("RequestID=" + requestID)[0];
                e.Graphics.DrawString("Pickup Date \t" + request.ScheduleDate.ToShortDateString(),font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(" Request ID \t" + request.RequestID.ToString(),font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;

                e.Graphics.DrawString("CLIENT -------------------------",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.ClientNumber + "-" + request.Client,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.CallerName,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;

                e.Graphics.DrawString("SHIPPER ------------------------",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.Shipper,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.ShipperAddress,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("Account# " + (!request.IsShipperNumberNull() ? request.ShipperNumber : ""),font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("  Phone# " + request.ShipperPhone,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(" Window  " + request.WindowOpen.ToString() + " to " + request.WindowClose.ToString(),font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;

                e.Graphics.DrawString("FREIGHT ------------------------",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.FreightType + " " + request.Amount + " " + request.AmountType.ToLower() + ", " + request.Weight.ToString() + "lbs",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;

                e.Graphics.DrawString("COMMENTS -----------------------",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.Comments,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;

                e.Graphics.DrawString("AGENT --------------------------",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.Terminal,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.DriverName,font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.Graphics.DrawString(request.OrderType=="B"?"Backhaul":"Pickup",font,Brushes.Black,fX,fY,format);
                fY += lineHeight;
                e.HasMorePages = false;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        #endregion
        #region Local Services: addPickupRequest(), setUserServices(), reportStatus(), ColumnHeaders
        private void addPickupRequest(DispatchDataset.PickupLogTableRow request) {
            //
            DispatchDataset.PickupLogTableRow _request = null;
            try {
                _request = (DispatchDataset.PickupLogTableRow)this.mSchedule.PickupLogTable.Select("ScheduleDate='" + request.ScheduleDate + "' AND ShipperNumber='" + request.ShipperNumber + "'")[0];
            }
            catch { }
            if (_request == null) {
                FreightGateway.AddPickupRequest(request);
                Refresh();
            }
            else {
                DialogResult res = MessageBox.Show("There is an existing pickup request for the same shipper and pickup date; would you like to update the exsiting request with this one?","Dispatch",MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                switch (res) {
                    case System.Windows.Forms.DialogResult.Yes:
                        _request.CallerName = request.CallerName;
                        _request.Amount = request.Amount;
                        _request.AmountType = request.AmountType;
                        _request.Weight = request.Weight;
                        _request.Comments = request.Comments;
                        FreightGateway.ChangePickupRequest(_request);
                        Refresh();
                        break;
                    case System.Windows.Forms.DialogResult.No:
                        FreightGateway.AddPickupRequest(request);
                        Refresh();
                        break;
                }
            }
        }
        private void setUserServices() {
            //Set user services
            try {
                bool canCancel = false;
                bool canManage = this.cboSchedule.SelectedItem.ToString() == "Today" || this.cboSchedule.SelectedItem.ToString() == "Tomorrow" || this.cboSchedule.SelectedItem.ToString() == "Advanced";
                try {
                    int requestID = this.grdSchedule.ActiveRow != null ? Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["RequestID"].Value) : 0;
                    DispatchDataset.PickupLogTableRow[] requests = (DispatchDataset.PickupLogTableRow[])this.mSchedule.PickupLogTable.Select("RequestID=" + requestID);
                    canCancel = requests.Length > 0 ? (!requests[0].IsDriverNameNull() && requests[0].DriverName.Trim().Length == 0) : false;
                }
                catch { }

                this.cboSchedule.Enabled = true;
                this.csNew.Enabled = canManage && this.grdSchedule.Focused && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsClientRep || RoleServiceGateway.IsBBBClerk);
                this.csOpen.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null;
                this.csClone.Enabled = canManage && this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsClientRep || RoleServiceGateway.IsBBBClerk);
                this.csCancel.Enabled = canManage && this.grdSchedule.Focused && canCancel && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsClientRep || RoleServiceGateway.IsBBBClerk);
                this.csExport.Enabled = canManage && this.grdSchedule.Focused && RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk;
                this.csRefresh.Enabled = true;
                this.csPrintRequest.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null;

                this.csTempNew.Enabled = this.grdTemplates.Focused && (RoleServiceGateway.IsDispatchSupervisor);
                this.csTempOpen.Enabled = this.grdTemplates.Focused && this.grdTemplates.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor);
                this.csTempCancel.Enabled = this.grdTemplates.Focused && this.grdTemplates.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor);
                this.csTempLoad.Enabled = canManage && this.grdTemplates.Focused && this.grdTemplates.Selected.Rows.Count > 0;
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
                for (int i = 0;i < this.mTemplates.PickupLogTable.Rows.Count;i++) this.mTemplates.PickupLogTable[i].Selected = false;

                //Update all selected load templates as selected for Add
                for (int i = 0;i < this.grdTemplates.Selected.Rows.Count;i++) {
                    int templateID = int.Parse(this.grdTemplates.Selected.Rows[i].Cells["RequestID"].Value.ToString());
                    for (int j = 0;j < this.mTemplates.PickupLogTable.Rows.Count;j++) {
                        if (this.mTemplates.PickupLogTable[j].RequestID == templateID) {
                            this.mTemplates.PickupLogTable[j].Selected = true;
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
                        reportStatus(new StatusEventArgs("Refreshing Pickup Log..."));
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
    public class PickupLogAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public PickupLogAutoRefreshService(winPickupLog postback) {
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
            try { e.Result = FreightGateway.ViewPickupLog(); }
            catch { }
        }
    }
}
