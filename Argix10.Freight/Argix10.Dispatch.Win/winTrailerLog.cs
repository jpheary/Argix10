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
    public partial class winTrailerLog:Form,ISchedule {
        //Members
        private UltraGridSvc mGridSvc = null;
        private TrailerLogAutoRefreshService mAutoRefreshSvc = null;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winTrailerLog() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
                this.mAutoRefreshSvc = new TrailerLogAutoRefreshService(this);
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
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mSchedule,"InboundScheduleTable",filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing Trailer Log..."));
                string caption = "Trailer Log " + this.cboSchedule.SelectedItem.ToString();
                UltraGridPrinter.Print(this.grdSchedule,caption,showDialog);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                reportStatus(new StatusEventArgs("Print previewing Trailer Log..."));
                string caption = "Trailer Log " + this.cboSchedule.SelectedItem.ToString();
                UltraGridPrinter.PrintPreview(this.grdSchedule,caption);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }

        }
        public bool CanNewTemplate { get { return false; } }
        public void NewTemplate() { }
        public bool CanOpenTemplate { get { return false; } }
        public void OpenTemplate() { }
        public bool CanCancelTemplate { get { return false; } }
        public void CancelTemplate() { }
        public bool CanLoadTemplates { get { return false; } }
        public void LoadTemplates() { }
        public bool TemplatesVisible { get { return false; } set { ; } }
        #endregion
        public override void Refresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                reportStatus(new StatusEventArgs("Refreshing Trailer Log..."));
                this.mGridSvc.CaptureState();
                this.mSchedule.Clear();
                switch (this.cboSchedule.SelectedItem.ToString()) {
                    case "Yard Check": this.mSchedule.Merge(FreightGateway.ViewTrailerLog()); break;
                    case "Archive": this.mSchedule.Merge(FreightGateway.ViewTrailerLogArchive()); break;
                }
                this.mGridSvc.RestoreState();
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
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.TrailerLogColumnHeaders;
                this.mInCarriers.Merge(FreightGateway.GetCarriers());
                this.mInDrivers.Merge(FreightGateway.GetDrivers());
                this.mOutCarriers.Merge(FreightGateway.GetCarriers());
                this.mOutDrivers.Merge(FreightGateway.GetDrivers());
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
                if(this.cboSchedule.SelectedItem.ToString() == "Yard Check") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Trailer Log..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Trailer Log..."));
                    this.mAutoRefreshSvc.Stop();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            //Turn off auto refresh
            try {
                reportStatus(new StatusEventArgs("Stopping auto-refresh on Trailer Log..."));
                this.mAutoRefreshSvc.Stop();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
                if (!e.Cancel) {
                    //Save settings
                    global::Argix.Properties.Settings.Default.TrailerLogColumnHeaders = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csMain.Font = this.csTemp.Font = this.Font; } catch { } }
        private void OnScheduleChanged(object sender,EventArgs e) {
            //Event handler fr change in schedule
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Yard Check") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Trailer Log..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Trailer Log..."));
                    this.mAutoRefreshSvc.Stop();
                }
                this.grdSchedule.ActiveRow = null;
                this.grdSchedule.Focus();
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnSearchTextChanged(object sender,EventArgs e) {
            //Event handler for change in search text
            this.mSchedule.Clear();
        }
        private void OnSearchKeyUp(object sender,KeyEventArgs e) {
            //Event handler for keyup event
            if (e.KeyCode == Keys.Enter) {
                this.mSchedule.Clear();
                this.mSchedule.Merge(FreightGateway.SearchTrailerLog(this.txtSearch.Text));
                e.Handled = true;
            }
        }
        private void OnInCarrierBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddInCarriers.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["InboundCarrier"].Width;
            }
            catch { }
        }
        private void OnInDriverBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddInDrivers.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["InboundDriverName"].Width;
            }
            catch { }
        }
        private void OnOutCarrierBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddOutCarriers.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["OutboundCarrier"].Width;
            }
            catch { }
        }
        private void OnOutDriverBeforeDropDown(object sender,System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddOutDrivers.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["OutboundDriverName"].Width;
            }
            catch { }
        }
        #region Schedule: OnScheduleInitializeLayout(), OnScheduleInitializeRow(), OnScheduleBeforeRowFilterDropDownPopulate(), OnScheduleMouseDown(), OnScheduleAfterSelectChange(), ...
        private void OnScheduleInitializeLayout(object sender,InitializeLayoutEventArgs e) {
            try {
                e.Layout.Bands[0].Columns["InboundCarrier"].ValueList = this.uddInCarriers;
                e.Layout.Bands[0].Columns["InboundDriverName"].ValueList = this.uddInDrivers;
                e.Layout.Bands[0].Columns["OutboundCarrier"].ValueList = this.uddOutCarriers;
                e.Layout.Bands[0].Columns["OutboundDriverName"].ValueList = this.uddOutDrivers;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnScheduleInitializeRow(object sender,InitializeRowEventArgs e) { }
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
                    case "TrailerNumber":
                    case "InboundDate":
                    case "InboundCarrier":
                    case "InboundSeal":
                    case "InboundDriverName":
                    case "TDSNumber":
                    case "InitialYardLocation":
                    case "OutboundDate":
                    case "OutboundCarrier":
                    case "OutboundSeal":
                    case "OutboundDriverName":
                    case "BOLNumber":
                    case "Comments":
                        e.Cell.Activation = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || RoleServiceGateway.IsSafetySupervisor ? Activation.AllowEdit : Activation.NoEdit;
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
            catch (Exception ex) { App.ReportError(ex); }
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
                if (e.KeyCode == Keys.Enter) { 
                    this.grdSchedule.ActiveRow.Update(); e.Handled = true; 
                } 
                else if (e.Control && e.KeyCode == Keys.C) {
                    if (this.grdSchedule.ActiveRow != null) {
                        int id = int.Parse(this.grdSchedule.ActiveRow.Cells["ID"].Value.ToString());
                        //DataRow[] trips = this.mSchedule.InboundScheduleTable.Select("ID=" + id);
                        //Clipboard.SetData(DataFormats.Serializable,trips[0].ItemArray);
                    }
                }
                else if (e.Control && e.KeyCode == Keys.V) {
                    object o = Clipboard.GetData(DataFormats.Serializable);
                    if (o != null) {
                        Cursor.Current = Cursors.WaitCursor;
                        //object[] os = (object[])o;
                        //DispatchDataset.InboundScheduleTableRow trip = new DispatchDataset().InboundScheduleTable.NewInboundScheduleTableRow();
                        //trip.ItemArray = os;
                        //FreightGateway.AddScheduledInboundFreight(trip);
                        //Refresh();
                    }
                }
            }
            catch (ArgumentException aex) { App.ReportError(new ApplicationException("Not a valid trip.",aex),true); }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Cursor.Current = Cursors.Default; }
        }
        private void OnScheduleAfterCellUpdate(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
        }
        private void OnScheduleAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //
            this.grdSchedule.Update();
            int id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
            DispatchDataset.TrailerLogTableRow entry = (DispatchDataset.TrailerLogTableRow)this.mSchedule.TrailerLogTable.Select("ID=" + id)[0];
            FreightGateway.ChangeTrailerEntry(entry);
            Refresh();
        }
        private void OnScheduleDoubleClick(object sender,EventArgs e) { if (this.csOpen.Enabled && this.grdSchedule.ActiveCell != null && !this.grdSchedule.ActiveCell.IsInEditMode) this.csOpen.PerformClick(); }
        #endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            dlgTrailerEntry dlg = null;
            DispatchDataset.TrailerLogTableRow entry = null;
            int id = 0;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "csNew":
                        entry = new DispatchDataset().TrailerLogTable.NewTrailerLogTableRow();
                        entry.ScheduleDate = DateTime.Today;
                        entry.InboundDate = DateTime.Now;
                        entry.IsTemplate = false;
                        dlg = new dlgTrailerEntry(entry);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddTrailerEntry(entry);
                            Refresh();
                        }
                        break;
                    case "csOpen":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        DispatchDataset.TrailerLogTableRow _entry = (DispatchDataset.TrailerLogTableRow)this.mSchedule.TrailerLogTable.Select("ID=" + id)[0];
                        entry = new DispatchDataset().TrailerLogTable.NewTrailerLogTableRow();
                        entry.ItemArray = _entry.ItemArray;
                        dlg = new dlgTrailerEntry(entry);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ChangeTrailerEntry(entry);
                            Refresh();
                        }
                        break;
                    case "csClone":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        entry = (DispatchDataset.TrailerLogTableRow)this.mSchedule.TrailerLogTable.Select("ID=" + id)[0];
                        DispatchDataset.TrailerLogTableRow clone = new DispatchDataset().TrailerLogTable.NewTrailerLogTableRow();
                        entry.ScheduleDate = DateTime.Today;
                        entry.InboundDate = DateTime.Now;
                        if (!entry.IsInboundCarrierNull()) clone.InboundCarrier = entry.InboundCarrier;
                        if (!entry.IsInboundDriverNameNull()) clone.InboundDriverName = entry.InboundDriverName;
                        if (!entry.IsCommentsNull()) clone.Comments = entry.Comments;
                        dlg = new dlgTrailerEntry(clone);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.AddTrailerEntry(clone);
                            Refresh();
                        }
                        break;
                    case "csCancel":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        FreightGateway.CancelTrailerEntry(id,DateTime.Now,Environment.UserName);
                        Refresh();
                        break;
                    case "csRefresh": Refresh(); break;
                    case "csTempNew": break;
                    case "csTempOpen": break;
                    case "csTempLoad": break;
                    case "csTempRefresh": break;
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
                bool canManage = this.cboSchedule.SelectedItem.ToString() == "Yard Check";
                try {
                    int id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                    DispatchDataset.TrailerLogTableRow entry = (DispatchDataset.TrailerLogTableRow)this.mSchedule.TrailerLogTable.Select("ID=" + id)[0];
                    canCancel = entry.IsOutboundDateNull();
                }
                catch { }

                this.cboSchedule.Enabled = true;
                this.csNew.Enabled = canManage && this.grdSchedule.Focused && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsSafetySupervisor || RoleServiceGateway.IsWindowClerk);
                this.csOpen.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null;
                this.csClone.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsSafetySupervisor || RoleServiceGateway.IsWindowClerk);
                this.csCancel.Enabled = this.grdSchedule.Focused && canCancel && RoleServiceGateway.IsDispatchSupervisor;
                this.csRefresh.Enabled = true;

                this.csTempNew.Enabled = this.csTempOpen.Enabled = this.csTempLoad.Enabled = this.csTempRefresh.Enabled = false;

                this.txtSearch.Visible = this.cboSchedule.SelectedItem.ToString() == "Search";
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
                        reportStatus(new StatusEventArgs("Refreshing Trailer Log..."));
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
    public class TrailerLogAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public TrailerLogAutoRefreshService(winTrailerLog postback) {
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
            try { e.Result = FreightGateway.ViewTrailerLog(); }
            catch { }
        }
    }
}
