using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
    public partial class winShipments : Form, IToolbar, IPSPToolbar {
        //Members
        private UltraGridSvc mGridSvc = null, mGridSearchSvc;
        private ShipmentAutoRefreshService mAutoRefreshSvc = null;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winShipments() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdShipments);
                this.mGridSearchSvc = new UltraGridSvc(this.grdShipments);
                this.mAutoRefreshSvc = new ShipmentAutoRefreshService(this);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        #region IToolbar interface
        public bool CanNew { get { return this.csNew.Enabled; } }
        public void New() { this.csNew.PerformClick(); }
        public bool CanOpen { get { return this.csOpen.Enabled; } }
        public void Open() { this.csOpen.PerformClick(); }
        public bool CanCancel { get { return false; } }
        public void Cancel() { }
        public bool CanSave { 
            get {
                switch(this.tabShipments.SelectedTab.Name) {
                    case "tabActive":
                        return (this.grdShipments.Rows != null ? this.grdShipments.Rows.Count > 0 : false); 
                    case "tabSearch":
                        return (this.grdSearch.Rows != null ? this.grdSearch.Rows.Count > 0 : false);
                    default:
                        return false;
                }
            } 
        }
        public void Save(string filename) {
            switch(this.tabShipments.SelectedTab.Name) {
                case "tabActive":
                    new Argix.ExcelFormat().Transform(this.mShipments, "LTLShipmentTable", filename); 
                    break;
                case "tabSearch":
                    new Argix.ExcelFormat().Transform(this.mSearch, "LTLShipmentTable", filename); 
                    break;
            }
        }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { 
            get {
                switch(this.tabShipments.SelectedTab.Name) {
                    case "tabActive":
                        return (this.grdShipments.Rows != null ? this.grdShipments.Rows.Count > 0 : false); 
                    case "tabSearch":
                        return (this.grdSearch.Rows != null ? this.grdSearch.Rows.Count > 0 : false);
                    default:
                        return false;
                }
            } 
        }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing..."));
                switch(this.tabShipments.SelectedTab.Name) {
                    case "tabActive": UltraGridPrinter.Print(this.grdShipments, "LTL Shipments", showDialog); break;
                    case "tabSearch": UltraGridPrinter.Print(this.grdSearch, "LTL Shipments", showDialog); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { 
            get {
                switch(this.tabShipments.SelectedTab.Name) {
                    case "tabActive":
                        return (this.grdShipments.Rows != null ? this.grdShipments.Rows.Count > 0 : false);
                    case "tabSearch":
                        return (this.grdSearch.Rows != null ? this.grdSearch.Rows.Count > 0 : false);
                    default:
                        return false;
                }
            } 
        }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                reportStatus(new StatusEventArgs("Print..."));
                switch(this.tabShipments.SelectedTab.Name) {
                    case "tabActive": UltraGridPrinter.PrintPreview(this.grdShipments, "LTL Shipments"); break;
                    case "tabSearch": UltraGridPrinter.PrintPreview(this.grdSearch, "LTL Shipments"); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }

        }
        #endregion
        #region IPSPToolbar interface
        public bool CanApproveClient { get { return false; } }
        public void ApproveClient() { }
        public bool CanDenyClient { get { return false; } }
        public void DenyClient() { }
        public bool CanPrintLabels { get { return this.csPrintLabels.Enabled; } }
        public void PrintLabels() { this.csPrintLabels.PerformClick(); }
        public bool CanPrintPaperwork { get { return this.csPrintPaperwork.Enabled; } }
        public void PrintPaperwork() { this.csPrintPaperwork.PerformClick(); }
        #endregion
        public override void Refresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                reportStatus(new StatusEventArgs("Refreshing shipments..."));
                string clientNumber = this.cboClient.SelectedValue.ToString();
                this.mShipments.Clear();
                this.mShipments.Merge(FreightGateway.ViewLTLShipments(clientNumber));
                this.grdShipments.Focus();
                base.Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                #region Grid customizations from normal layout
                this.grdShipments.DisplayLayout.Bands[0].Columns["ShipDate"].SortIndicator = SortIndicator.Descending;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ShipmentsColumns;
                Application.DoEvents();

                this.mClients.Merge(FreightGateway.ReadLTLClientList(true));
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(null, EventArgs.Empty); this.mAutoRefreshSvc.Start();

                this.dtpShipDateStart.MinDate = DateTime.Today.AddDays(-365);
                this.dtpShipDateStart.MaxDate = DateTime.Today.AddDays(30);
                this.dtpShipDateStart.Value = DateTime.Today.AddDays(-30);
                this.dtpShipDateEnd.MinDate = DateTime.Today.AddDays(-365);
                this.dtpShipDateEnd.MaxDate = DateTime.Today.AddDays(30);
                this.dtpShipDateEnd.Value = DateTime.Today;
                this.mClients2.Merge(FreightGateway.ReadLTLClientList());
                if(this.cboClient2.Items.Count > 0) this.cboClient2.SelectedIndex = 0;
                OnClient2Changed(null, EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender,EventArgs e) {
            //Event handler for form activated event
            //Turn on auto refresh
            try {
                this.mAutoRefreshSvc.Start();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            //Turn off auto refresh
            try {
                this.mAutoRefreshSvc.Stop();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
                if (!e.Cancel) {
                    //Save settings
                    global::Argix.Properties.Settings.Default.ShipmentsColumns = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csShipments.Font = this.Font; } catch { } }
        private void OnSelected(object sender, EventArgs e) {
            setUserServices();
        }
        private void OnClientChanged(object sender, EventArgs e) {
            try {
                this.mAutoRefreshSvc.ClientNumber = this.cboClient.SelectedValue.ToString() == "All" ? null : this.cboClient.SelectedValue.ToString();
                Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnClient2Changed(object sender, EventArgs e) {
            try {
                //Load shippers and consignees for the selected client
                this.mShippers.Clear();
                this.mShippers.LTLShipperTable.AddLTLShipperTableRow("", "", "", "", "", "", "", "", "", "", "", "", "", DateTime.MinValue, DateTime.MinValue, 1, "", "", DateTime.MinValue, "", null);
                this.mShippers.Merge(FreightGateway.ReadLTLShippersList(this.cboClient2.SelectedValue.ToString()));
                if(this.cboShipper.Items.Count > 0) this.cboShipper.SelectedIndex = 0;
                this.mConsignees.Clear();
                this.mConsignees.LTLConsigneeTable.AddLTLConsigneeTableRow(0, "", "", "", "", "", "", "", "", "", "", "", "", DateTime.MinValue, DateTime.MinValue, 1, DateTime.MinValue, "", null);
                this.mConsignees.Merge(FreightGateway.ReadLTLConsigneesList(this.cboClient2.SelectedValue.ToString()));
                if(this.cboConsignee.Items.Count > 0) this.cboConsignee.SelectedIndex = 0;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region Grid Support: OnGridMouseDown(), OnGridAfterSelect(), OnGridDoubleClick()
        private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Ensure focus when user mouses (embedded child objects sometimes hold focus)
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();

                //Determine grid element pointed to by the mouse
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
                if(uiElement != null) {
                    //Determine if user selected a grid row
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if(context != null) {
                        //Row was selected- if mouse button is:
                        // left: forward to mouse move event handler
                        //right: clear all (multi-)selected rows and select a single row
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)context;
                            if(!row.Selected) grid.Selected.Rows.Clear();
                            row.Selected = true;
                            row.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) {
                            grid.Selected.Rows.Clear();
                            grid.ActiveRow = null;
                        }
                        else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridAfterSelect(object sender, AfterSelectChangeEventArgs e) {
            //
            setUserServices();
        }
        private void OnGridDoubleClick(object sender, EventArgs e) {
            //
            if(this.csOpen.Enabled) this.csOpen.PerformClick();
        }
        #endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Menu services
            string number = "";
            LTLShipment2 shipment = null;
            dlgLTLShipment dlg = null;
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "csNew":
                        switch(this.tabShipments.SelectedTab.Name) {
                            case "tabActive":
                                shipment = new LTLShipment2();
                                shipment.ShipmentNumber = "";
                                shipment.ClientNumber = this.cboClient.SelectedValue.ToString();
                                shipment.ShipDate = DateTime.Today.AddDays(1);
                                dlg = new dlgLTLShipment(shipment);
                                dlg.Font = this.Font;
                                if(dlg.ShowDialog(this) == DialogResult.OK) {
                                    this.Cursor = Cursors.WaitCursor;
                                    number = FreightGateway.CreateLTLShipment(shipment);
                                    MessageBox.Show(this, "New shipment created.", App.Product, MessageBoxButtons.OK);
                                    Refresh();
                                }
                                break;
                        }
                        break;
                    case "csOpen":
                        switch(this.tabShipments.SelectedTab.Name) {
                            case "tabActive": number = this.grdShipments.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString(); break;
                            case "tabSearch": number = this.grdSearch.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString(); break;
                        }
                        shipment = FreightGateway.ReadLTLShipment(number);
                        dlg = new dlgLTLShipment(shipment);
                        dlg.Font = this.Font;
                        if(dlg.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            FreightGateway.UpdateLTLShipment(shipment);
                            MessageBox.Show(this, "Shipment updated.", App.Product, MessageBoxButtons.OK);
                            Refresh();
                        }
                        break;
                    case "csCancel":
                        switch(this.tabShipments.SelectedTab.Name) {
                            case "tabActive":
                                number = this.grdShipments.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString();
                                if(MessageBox.Show(this, "Cancel the shipment# " + number + "?", App.Product, MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                                    this.Cursor = Cursors.WaitCursor;
                                    FreightGateway.CancelLTLShipment(number, Environment.UserName);
                                    MessageBox.Show(this, "Shipment cancelled.", App.Product, MessageBoxButtons.OK);
                                    Refresh();
                                }
                                break;
                        }
                        break;
					case "csRefresh":
						this.Cursor = Cursors.WaitCursor;
                        Refresh();
                       break;
                    case "csPrintLabels":
                        switch(this.tabShipments.SelectedTab.Name) {
                            case "tabActive": number = this.grdShipments.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString(); break;
                            case "tabSearch": number = this.grdSearch.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString(); break;
                        }
                       dlgLabels dlgL = new dlgLabels(number);
                       dlgL.Font = this.Font;
                       dlgL.ShowDialog(this);
                       break;
                    case "csPrintPaperwork":
                        switch(this.tabShipments.SelectedTab.Name) {
                            case "tabActive": number = this.grdShipments.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString(); break;
                            case "tabSearch": number = this.grdSearch.Selected.Rows[0].Cells["ShipmentNumber"].Value.ToString(); break;
                        }
                       dlgPaperwork dlgP = new dlgPaperwork(number);
                       dlgP.Font = this.Font;
                       dlgP.ShowDialog(this);
                       break;
                }
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnSearchClick(object sender, EventArgs e) {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                this.grdSearch.Text = "Search Results for " + this.cboClient2.Text;
                this.mSearch.Clear();
                
                LTLSearch2 criteria = new LTLSearch2();
                criteria.ShipDateStart = this.dtpShipDateStart.Value;
                criteria.ShipDateEnd = this.dtpShipDateEnd.Value;
                criteria.ClientNumber = this.cboClient2.SelectedValue.ToString();
                criteria.ShipperNumber = this.cboShipper.SelectedValue.ToString();
                criteria.ConsigneeNumber = (int)this.cboConsignee.SelectedValue;
                this.mSearch.Merge(FreightGateway.SearchLTLShipments(criteria));
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        #endregion
        #region Local Services: setUserServices(), reportStatus(), ColumnHeaders
        private void setUserServices() {
			//Set user services
			try {
                //Determine access
                bool isDispatchSupervisor=false, isSalesRep=false;
                try {
                    isDispatchSupervisor = RoleServiceGateway.IsDispatchSupervisor;
                    isSalesRep = RoleServiceGateway.IsSalesRep;
                }
                catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }

                //Set menu states 
                switch(this.tabShipments.SelectedTab.Name) {
                    case "tabActive":
                        this.csNew.Enabled = (isDispatchSupervisor || isSalesRep);
                        this.csOpen.Enabled = (isDispatchSupervisor || isSalesRep) && this.grdShipments.Selected.Rows.Count > 0;

                        bool pickedup = this.grdShipments.Selected.Rows.Count > 0 ? this.grdShipments.Selected.Rows[0].Cells["PickupDate"].Value.ToString() != "" : false;
                        bool cancelled = this.grdShipments.Selected.Rows.Count > 0 ? this.grdShipments.Selected.Rows[0].Cells["Cancelled"].Value.ToString() != "" : false;
                        this.csCancel.Enabled = ((isDispatchSupervisor || isSalesRep) && this.grdShipments.Focused && !pickedup && !cancelled);
                
                        this.csRefresh.Enabled = true;
                        this.csPrintLabels.Enabled = this.grdShipments.Selected.Rows.Count > 0;
                        this.csPrintPaperwork.Enabled = this.grdShipments.Selected.Rows.Count > 0;
                        break;
                    case "tabSearch":
                        this.csNew.Enabled = false;
                        this.csOpen.Enabled = (isDispatchSupervisor || isSalesRep) && this.grdSearch.Selected.Rows.Count > 0;
                        this.csCancel.Enabled = false;
               
                        this.csRefresh.Enabled = false;
                        this.csPrintLabels.Enabled = this.csPrintPaperwork.Enabled = false;
                        break;
                }
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { Application.DoEvents(); if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this, new EventArgs()); }
        }		
        private void reportStatus(StatusEventArgs e) { if (this.StatusMessage != null) this.StatusMessage(this,e); }
        private string ColumnHeaders {
            get {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.grdShipments.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if (value.Length > 0) {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdShipments.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                    this.grdShipments.DisplayLayout.Bands[0].Columns["ShipDate"].SortIndicator = SortIndicator.Ascending;
                }
            }
        }
        #endregion
        #region AutoRefresh Services: OnAutoRefreshCompleted()
        public void OnAutoRefreshCompleted(object sender,RunWorkerCompletedEventArgs e) {
            //
            try {
                FreightDataset ds = null;
                if (this.InvokeRequired) {
                    this.Invoke(new RunWorkerCompletedEventHandler(OnAutoRefreshCompleted),new object[] { sender,e });
                }
                else {
                    ds = (FreightDataset)e.Result;
                    if (this.grdShipments.ActiveCell == null || !this.grdShipments.ActiveCell.IsInEditMode) {
                        reportStatus(new StatusEventArgs("Refreshing..."));
                        this.mGridSvc.CaptureState();
                        lock (this.mShipments) {
                            this.mShipments.Clear();
                            this.mShipments.Merge(ds);
                        }
                        this.mGridSvc.RestoreState();
                    }
                }
            }
            catch { }
        }
        #endregion
    }

    public class ShipmentAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;
        private string mClientNumber = null;

        //Interface
        public ShipmentAutoRefreshService(winShipments postback) {
            //
            this.mTimer = new System.Windows.Forms.Timer();
            this.mTimer.Interval = global::Argix.Properties.Settings.Default.AutoRefreshTimer;
            this.mTimer.Tick += new EventHandler(OnTick);
            this.mWorker = new BackgroundWorker();
            this.mWorker.DoWork += new DoWorkEventHandler(OnAutoRefresh);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(postback.OnAutoRefreshCompleted);
        }
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        public void Start() { this.mTimer.Start(); }
        public void Stop() { this.mTimer.Stop(); }

        private void OnTick(object sender,EventArgs e) {
            //Event handler for timer tick event
            try { if (!this.mWorker.IsBusy) this.mWorker.RunWorkerAsync(); }
            catch { }
        }
        private void OnAutoRefresh(object sender,DoWorkEventArgs e) {
            //Event handler for background worker thread DoWork event; runs on worker thread
            try { e.Result = FreightGateway.ViewLTLShipments(this.mClientNumber); }
            catch { }
        }
    }
}
