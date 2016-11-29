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
    public partial class winLoadTenderLog:Form,ISchedule {
        //Members
        private UltraGridSvc mGridSvc = null;
        private LoadTenderAutoRefreshService mAutoRefreshSvc = null;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winLoadTenderLog() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
                this.mAutoRefreshSvc = new LoadTenderAutoRefreshService(this);
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
        public void Cancel() { if (this.csCancel.Enabled) this.csCancel.PerformClick(); }
        public bool CanSave { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mSchedule,"LoadTenderLogTable",filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.grdSchedule.Rows != null ? this.grdSchedule.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing this log..."));
                string caption = "Load Tenders ";
                switch(this.cboSchedule.SelectedItem.ToString()) {
                    case "Advanced": caption = "Load Tenders Advanced"; break;
                    case "Today": caption = "Load Tenders " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Load Tenders " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); ; break;
                    case "Archive": caption = "Load Tenders Archive"; break;
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
                reportStatus(new StatusEventArgs("Print previewing this log..."));
                string caption = "Load Tenders ";
                switch(this.cboSchedule.SelectedItem.ToString()) {
                    case "Advanced": caption = "Load Tenders Advanced"; break;
                    case "Today": caption = "Load Tenders " + DateTime.Today.ToString("MM/dd/yyyy"); break;
                    case "Yesterday": caption = "Load Tenders " + DateTime.Today.AddDays(-1).ToString("MM/dd/yyyy"); ; break;
                    case "Archive": caption = "Load Tenders Archive"; break;
                }
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
                reportStatus(new StatusEventArgs("Refreshing load tenders..."));
                this.mGridSvc.CaptureState();
                this.mSchedule.Clear();
                switch(this.cboSchedule.SelectedItem.ToString()) {
                    case "Advanced": this.mSchedule.Merge(FreightGateway.ViewLoadTenderLogAdvanced()); break;
                    case "Today": this.mSchedule.Merge(FreightGateway.ViewLoadTenderLog()); break;
                    case "Yesterday": this.mSchedule.Merge(FreightGateway.ViewLoadTenderLogYesterday()); break;
                    case "Archive": this.mSchedule.Merge(FreightGateway.ViewLoadTenderLogArchive()); break;
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
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.LoadTenderColumnHeaders;
                this.mOrigins.Merge(FreightGateway.GetLocations());
                this.mDestinations.Merge(FreightGateway.GetLocations());
                this.mCarriers.Merge(FreightGateway.GetCarriers());
                this.mDrivers.Merge(FreightGateway.GetDrivers());
                this.cboSchedule.SelectedIndex = 1;
                Application.DoEvents();
                OnScheduleChanged(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender,EventArgs e) {
            //Event handler for form activated event
            //Turn on auto refresh if applicable
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Today") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Load Tenders..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Load Tenders..."));
                    this.mAutoRefreshSvc.Stop();
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            //Turn off auto refresh
            try {
                reportStatus(new StatusEventArgs("Stopping auto-refresh on Load Tenders..."));
                this.mAutoRefreshSvc.Stop();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
                if (!e.Cancel) {
                    //Save settings
                    global::Argix.Properties.Settings.Default.LoadTenderColumnHeaders = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csMain.Font = this.Font; } catch { } }
        private void OnScheduleChanged(object sender,EventArgs e) {
            try {
                if(this.cboSchedule.SelectedItem.ToString() == "Today") {
                    reportStatus(new StatusEventArgs("Starting auto-refresh on Load Tenders..."));
                    this.mAutoRefreshSvc.Start();
                }
                else {
                    reportStatus(new StatusEventArgs("Stopping auto-refresh on Load Tenders..."));
                    this.mAutoRefreshSvc.Stop();
                }
                this.grdSchedule.ActiveRow = null;
                this.grdSchedule.Focus();
                Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region Schedule: OnScheduleInitializeLayout(), OnScheduleInitializeRow(), OnScheduleBeforeRowFilterDropDownPopulate(), OnScheduleMouseDown(), OnScheduleAfterSelectChange(), ...
        private void OnScheduleInitializeLayout(object sender,InitializeLayoutEventArgs e) {
            try {
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
                    case "ID":
                    case "Created":
                    case "CreateUserID":
                    case "ScheduleDate":
                    case "LoadTenderNumber":
                    case "Cancelled":
                    case "CancelledBy":
                    case "LastUpdated":
                    case "UserID":
                    case "RowVersion":
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                    default:
                        e.Cell.Activation = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || RoleServiceGateway.IsBBBClerk ? Activation.AllowEdit : Activation.NoEdit;
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
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnScheduleAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //
            try {
                this.grdSchedule.Update();
                int id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                DispatchDataset.LoadTenderLogTableRow loadtender = (DispatchDataset.LoadTenderLogTableRow)this.mSchedule.LoadTenderLogTable.Select("ID=" + id)[0];
                FreightGateway.UpdateLoadTenderEntry(loadtender);
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnScheduleDoubleClick(object sender,EventArgs e) { if (this.csOpen.Enabled && this.grdSchedule.ActiveCell != null && !this.grdSchedule.ActiveCell.IsInEditMode) this.csOpen.PerformClick(); }
        #endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            dlgLoadTender dlg = null;
            DispatchDataset.LoadTenderLogTableRow entry = null;
            int id = 0;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "csNew":
                        entry = new DispatchDataset().LoadTenderLogTable.NewLoadTenderLogTableRow();
                        entry.ScheduleDate = DateTime.Today.AddDays(1);
                        dlg = new dlgLoadTender(entry);
                        dlg.Font = this.Font;
                        if(dlg.ShowDialog(this) == DialogResult.OK) {
                            //Validate that a similiar load tender doesn't exist
                            //TODO: ScheduleDate, Vendor, Amount, Weight
                            FreightGateway.CreateLoadTenderEntry(entry);
                        }
                        break;
                    case "csOpen":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        DispatchDataset.LoadTenderLogTableRow _entry = (DispatchDataset.LoadTenderLogTableRow)this.mSchedule.LoadTenderLogTable.Select("ID=" + id)[0];
                        entry = new DispatchDataset().LoadTenderLogTable.NewLoadTenderLogTableRow();
                        entry.ItemArray = _entry.ItemArray;
                        dlg = new dlgLoadTender(entry);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.UpdateLoadTenderEntry(entry);
                            Refresh();
                        }
                        break;
                    case "csClone":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        entry = (DispatchDataset.LoadTenderLogTableRow)this.mSchedule.LoadTenderLogTable.Select("ID=" + id)[0];
                        DispatchDataset.LoadTenderLogTableRow clone = new DispatchDataset().LoadTenderLogTable.NewLoadTenderLogTableRow();
                        clone.ScheduleDate = DateTime.Today.AddDays(1); break;
                        if (!entry.IsClientNull()) clone.Client = entry.Client;
                        if (!entry.IsClientNumberNull()) clone.ClientNumber = entry.ClientNumber;
                        if(!entry.IsVendorNumberNull()) clone.VendorNumber = entry.VendorNumber;
                        if(!entry.IsVendorNameNull()) clone.VendorName = entry.VendorName;
                        if (!entry.IsVendorAddressLine1Null()) clone.VendorAddressLine1 = entry.VendorAddressLine1;
                        if (!entry.IsVendorAddressLine2Null()) clone.VendorAddressLine2 = entry.VendorAddressLine2;
                        if (!entry.IsVendorCityNull()) clone.VendorCity = entry.VendorCity;
                        if (!entry.IsVendorStateNull()) clone.VendorState = entry.VendorState;
                        if (!entry.IsVendorZipNull()) clone.VendorZip = entry.VendorZip;
                        if (!entry.IsVendorZipNull()) clone.VendorZip4 = entry.VendorZip4;
                        if (!entry.IsWindowOpenNull()) clone.WindowOpen = entry.WindowOpen;
                        if (!entry.IsWindowCloseNull()) clone.WindowClose = entry.WindowClose;
                        if (!entry.IsDescriptionNull()) clone.Description = entry.Description;
                        if (!entry.IsAmountNull()) clone.Amount = entry.Amount;
                        if (!entry.IsAmountTypeNull()) clone.AmountType = entry.AmountType;
                        if (!entry.IsWeightNull()) clone.Weight = entry.Weight;
                        if (!entry.IsCommentsNull()) clone.Comments = entry.Comments;
                        dlg = new dlgLoadTender(clone);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            //TODO: ScheduleDate, Vendor, Amount, Weight
                            //TODO
                            FreightGateway.CreateLoadTenderEntry(entry);
                        }
                     break;
                    case "csCancel":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        if (MessageBox.Show(this,"Are you sure you want to cancel load tender " + id.ToString() + "?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes) {
                            FreightGateway.CancelLoadTenderEntry(id,DateTime.Now,Environment.UserName);
                            Refresh();
                        }
                        break;
                    case "csTender":
                        //Tender (attach image file) an existing load
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
                                id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                                LoadTender lt = new LoadTender();
                                lt.Filename = dlgOpen.SafeFileName;
                                fsa = new FileStream(dlgOpen.FileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                                reader = new BinaryReader(fsa);
                                lt.File = reader.ReadBytes((int)fsa.Length);
                                bool tendered = FreightGateway.TenderLoadTenderEntry(id, lt);
                                MessageBox.Show(this, "Load Tender entry " + id.ToString() + " was tendered.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Refresh();
                            }
                            finally { if(reader != null) reader.Close(); if(fsa != null) fsa.Close(); }
                        }
                        break;
                    case "csViewTender":
                        //Open the load tender for the selected quote
                        int number = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["LoadTenderNumber"].Value);
                        LoadTender loadTender = FreightGateway.GetLoadTender(number);
                        string file = App.Config.TempFolder + loadTender.Filename;
                        try { System.IO.File.Delete(file); }
                        catch { }
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
                    case "csScheduleTrip":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        entry = (DispatchDataset.LoadTenderLogTableRow)this.mSchedule.LoadTenderLogTable.Select("ID=" + id)[0];
                        DispatchDataset.BBBScheduleTableRow trip = new DispatchDataset().BBBScheduleTable.NewBBBScheduleTableRow();
                        trip.Created = DateTime.Now;
                        trip.CreateUserID = Environment.UserName;
                        trip.ScheduleDate = entry.ScheduleDate;
                        //trip.OriginLocationID = entry.VendorNumber;
                        trip.Origin = entry.VendorName.Trim();
                        trip.OriginLocation = entry.VendorCity.Trim() + ", " + entry.VendorState.Trim();
                        trip.DestinationLocationID = 100000532000000053;
                        trip.Destination = "ARGIX LOGISTICS NATIONAL";
                        trip.DestinationLocation = "JAMESBURG, NJ";
                        trip.CarrierName = "ARGIX";
                        trip.DriverName = "";
                        trip.Confirmed = false;
                        trip.TrailerNumber = "";
                        trip.DropEmptyTrailerNumber = "";
                        trip.IsLiveUnload = !entry.IsFullTrailer;
                        trip.Amount = entry.Amount;
                        trip.AmountType = entry.AmountType;
                        trip.FreightType = "PCS";
                        trip.TDSNumber = "";
                        trip.ScheduledDeparture = DateTime.MinValue.AddHours(11);
                        trip.ScheduledArrival = DateTime.MinValue.AddHours(11);
                        trip.Comments = entry.Comments;
                        trip.IsTemplate = false;
                        dlgBBBTrip dlgcif = new dlgBBBTrip(trip);
                        dlgcif.Font = this.Font;
                        if(dlgcif.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ScheduleLoadTenderEntry(id, trip);
                            Refresh();
                        }
                        break;
                    case "csSchedulePickup":
                        id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                        entry = (DispatchDataset.LoadTenderLogTableRow)this.mSchedule.LoadTenderLogTable.Select("ID=" + id)[0];
                        DispatchDataset.PickupLogTableRow request = new DispatchDataset().PickupLogTable.NewPickupLogTableRow();
                        request.Created = DateTime.Now;
                        request.CreateUserID = Environment.UserName;
                        request.ScheduleDate = entry.ScheduleDate;
                        request.CallerName = "PCS Load Tender";
                        request.ClientNumber = entry.ClientNumber;
                        request.Client = entry.Client;
                        request.ShipperNumber = ""; //entry.VendorNumber Needs to be a Roadshow AccountID- let user select;
                        request.Shipper = entry.VendorName;
                        request.ShipperAddress = entry.VendorAddressLine1 + "\r\n" + entry.VendorAddressLine2 + "\r\n" + entry.VendorCity + ", " + entry.VendorState + " " + entry.VendorZip;
                        request.ShipperPhone = entry.ContactPhone;
                        request.WindowOpen = entry.WindowOpen;
                        request.WindowClose = entry.WindowClose;
                        request.Amount = entry.Amount;
                        request.AmountType = entry.AmountType;
                        request.FreightType = "PCS";
                        request.Weight = entry.Weight;
                        request.OrderType = "R";
                        request.Comments = entry.Comments;
                        request.TerminalNumber = "0001";
                        request.Terminal = "ARGIX LOGISTICS RIDGEFIELD";
                        request.IsTemplate = false;
                        dlgPickupRequest dlgpr = new dlgPickupRequest(request);
                        dlgpr.Font = this.Font;
                        if(dlgpr.ShowDialog(this) == DialogResult.OK) {
                            FreightGateway.ScheduleLoadTenderEntry(id, request);
                        }
                        break;
                    case "csSearch":
                        break;
                    case "csRefresh": 
                        Refresh(); 
                        break;
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
                bool canTender = false, canSchedule=false;
                try {
                    int id = Convert.ToInt32(this.grdSchedule.ActiveRow.Cells["ID"].Value);
                    DispatchDataset.LoadTenderLogTableRow entry = (DispatchDataset.LoadTenderLogTableRow)this.mSchedule.LoadTenderLogTable.Select("ID=" + id)[0];
                    canCancel = true;   // entry.IsPickupNumberNull();
                    canTender = entry.IsLoadTenderNumberNull();
                    canSchedule = entry.IsPickupNumberNull();
                }
                catch { }

                this.cboSchedule.Enabled = true;
                this.csNew.Enabled = canManage && this.grdSchedule.Focused && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsBBBClerk);
                this.csOpen.Enabled = this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null;
                this.csClone.Enabled = canManage && this.grdSchedule.Focused && this.grdSchedule.ActiveRow != null && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsBBBClerk);
                this.csSearch.Enabled = true;
                this.csCancel.Enabled = canManage && this.grdSchedule.Focused && canCancel && (RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsBBBClerk);
                this.csTender.Enabled = canTender;
                this.csViewTender.Enabled = !canTender;
                this.csScheduleTrip.Enabled = canSchedule;
                this.csSchedulePickup.Enabled = canSchedule;
                this.csRefresh.Enabled = true;
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
                        reportStatus(new StatusEventArgs("Refreshing Load Tenders..."));
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
    public class LoadTenderAutoRefreshService{
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public LoadTenderAutoRefreshService(winLoadTenderLog postback) {
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
            try { e.Result = FreightGateway.ViewLoadTenderLog(); }
            catch { }
        }
    }
}
