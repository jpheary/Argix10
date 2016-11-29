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
    public partial class winClients : Form, IToolbar, IPSPToolbar {
        //Members
        private UltraGridSvc mGridSvc = null;
        private ClientAutoRefreshService mAutoRefreshSvc = null;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winClients() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdClients);
                this.mAutoRefreshSvc = new ClientAutoRefreshService(this);
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
        public bool CanSave { get { return (this.grdClients.Rows != null ? this.grdClients.Rows.Count > 0 : false); } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mClients, "LTLClientTable", filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.grdClients.Rows != null ? this.grdClients.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing..."));
                UltraGridPrinter.Print(this.grdClients, "Clients", showDialog);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { get { return (this.grdClients.Rows != null ? this.grdClients.Rows.Count > 0 : false); } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                reportStatus(new StatusEventArgs("Print previewing..."));
                UltraGridPrinter.PrintPreview(this.grdClients, "Clients");
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }

        }
        #endregion
        #region IPSPToolbar interface
        public bool CanApproveClient { get { return this.csApprove.Enabled; } }
        public void ApproveClient() { this.csApprove.PerformClick(); }
        public bool CanDenyClient { get { return this.csDeny.Enabled; } }
        public void DenyClient() { this.csDeny.PerformClick(); }
        public bool CanPrintLabels { get { return false; } }
        public void PrintLabels() { }
        public bool CanPrintPaperwork { get { return false; } }
        public void PrintPaperwork() { }
        #endregion
        public override void Refresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                reportStatus(new StatusEventArgs("Refreshing clients..."));
                this.mClients.Clear();
                this.mClients.Merge(FreightGateway.ViewLTLClients());
                this.grdClients.Focus();
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
                this.grdClients.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = SortIndicator.Ascending;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ClientsColumns;
                Application.DoEvents();
                this.mAutoRefreshSvc.Start();
                this.grdClients.ActiveRow = null;
                this.grdClients.Focus();
                Refresh();
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
                    global::Argix.Properties.Settings.Default.ClientsColumns = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csClients.Font = this.Font; } catch { } }
        #region Grid Support: OnGridInitializeRow(), OnGridMouseDown(), OnGridAfterSelect(), OnGridDoubleClick()
        private void OnGridInitializeRow(object sender, InitializeRowEventArgs e) {
            //Event handler for intialize row event
            try {
                if (e.Row.Cells["ApprovalDate"].Value == DBNull.Value && e.Row.Cells["DenialDate"].Value == DBNull.Value) e.Row.Appearance.BackColor = Color.Yellow;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Information); }
        }
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
            if (this.csOpen.Enabled) this.csOpen.PerformClick(); 
        }
        #endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Menu services
            int id = 0;
            LTLClient2 client=null;
            dlgLTLClient dlg=null;
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "csNew":
                        client = new LTLClient2();
                        dlg = new dlgLTLClient(client);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            id = FreightGateway.CreateLTLClient(client);
                            MessageBox.Show(this, "New client created.", App.Product, MessageBoxButtons.OK);
                            Refresh();
                        }
                        break;
                    case "csOpen":
                        id = (int)this.grdClients.Selected.Rows[0].Cells["ID"].Value;
                        client = FreightGateway.ReadLTLClient(id);
                        dlg = new dlgLTLClient(client);
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            FreightGateway.UpdateLTLClient(client);
                            MessageBox.Show(this, "Client updated.", App.Product, MessageBoxButtons.OK);
                            Refresh();
                        }
                        break;
					case "csRefresh":
						this.Cursor = Cursors.WaitCursor;
                        Refresh();
                       break;
                    case "csApprove":
                        id = (int)this.grdClients.Selected.Rows[0].Cells["ID"].Value;
                        FreightGateway.ApproveLTLClient(id,true);
                        Refresh();
                        break;
                    case "csDeny":
                        id = (int)this.grdClients.Selected.Rows[0].Cells["ID"].Value;
                        FreightGateway.ApproveLTLClient(id,false);
                        Refresh();
                        break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        #endregion
        #region Local Services: setUserServices(), reportStatus(), ColumnHeaders
        private void setUserServices() {
			//Set user services
			try {
                //Set access
                bool isFinanceSupervisor = false, isSalesRep=false;
                try {
                    isFinanceSupervisor = RoleServiceGateway.IsFinanceSupervisor;
                    isSalesRep = RoleServiceGateway.IsSalesRep;
                }
                catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }

                //Set menu states 
                bool selected = this.grdClients.Selected.Rows.Count > 0;
                this.csNew.Enabled = (isFinanceSupervisor || isSalesRep);
                this.csOpen.Enabled = (isFinanceSupervisor || isSalesRep) && selected;
                this.csRefresh.Enabled = true;
                
                bool approved = selected ? this.grdClients.Selected.Rows[0].Cells["ApprovalDate"].Value.ToString() != "" : false;
                bool denied = selected ? this.grdClients.Selected.Rows[0].Cells["DenialDate"].Value.ToString() != "" : false;
                this.csApprove.Enabled = isFinanceSupervisor && selected && !approved && !denied;
                this.csDeny.Enabled = isFinanceSupervisor && selected && !approved && !denied;
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { Application.DoEvents(); if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this, new EventArgs()); }
        }		
        private void reportStatus(StatusEventArgs e) { if (this.StatusMessage != null) this.StatusMessage(this,e); }
        private string ColumnHeaders {
            get {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.grdClients.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if (value.Length > 0) {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdClients.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                    this.grdClients.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = SortIndicator.Ascending;
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
                    if (this.grdClients.ActiveCell == null || !this.grdClients.ActiveCell.IsInEditMode) {
                        reportStatus(new StatusEventArgs("Refreshing clients..."));
                        this.mGridSvc.CaptureState();
                        lock (this.mClients) {
                            this.mClients.Clear();
                            this.mClients.Merge(ds);
                        }
                        this.mGridSvc.RestoreState();
                    }
                }
            }
            catch { }
        }
        #endregion
    }

    public class ClientAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public ClientAutoRefreshService(winClients postback) {
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
            try { e.Result = FreightGateway.ViewLTLClients(); } catch { }
        }
    }
}
