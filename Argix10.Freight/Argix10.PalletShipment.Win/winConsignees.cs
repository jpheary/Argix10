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
    public partial class winConsignees : Form, IToolbar {
        //Members
        private UltraGridSvc mGridSvc = null;
        private ConsigneeAutoRefreshService mAutoRefreshSvc = null;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winConsignees() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdConsignees);
                this.mAutoRefreshSvc = new ConsigneeAutoRefreshService(this);
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
        public bool CanSave { get { return (this.grdConsignees.Rows != null ? this.grdConsignees.Rows.Count > 0 : false); } }
        public void Save(string filename) { new Argix.ExcelFormat().Transform(this.mConsignees, "LTLConsigneeTable", filename); }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.grdConsignees.Rows != null ? this.grdConsignees.Rows.Count > 0 : false); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing..."));
                UltraGridPrinter.Print(this.grdConsignees, "LTL Consignees", showDialog);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { get { return (this.grdConsignees.Rows != null ? this.grdConsignees.Rows.Count > 0 : false); } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                reportStatus(new StatusEventArgs("Print previewing..."));
                UltraGridPrinter.PrintPreview(this.grdConsignees, "LTL Consignees");
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }

        }
        #endregion
        public override void Refresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                reportStatus(new StatusEventArgs("Refreshing consignees..."));
                string clientNumber = this.cboClient.SelectedValue.ToString();
                this.mConsignees.Clear();
                this.mConsignees.Merge(FreightGateway.ViewLTLConsignees(clientNumber));
                this.grdConsignees.Focus();
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
                this.grdConsignees.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = SortIndicator.Ascending;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ConsigneeColumns;
                Application.DoEvents();

                this.mClients.Merge(FreightGateway.ReadLTLClientList());
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(null, EventArgs.Empty); this.mAutoRefreshSvc.Start();
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
                    global::Argix.Properties.Settings.Default.ConsigneeColumns = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { this.csConsignees.Font = this.Font; } catch { } }
        private void OnClientChanged(object sender, EventArgs e) {
            try {
                this.mAutoRefreshSvc.ClientNumber = this.cboClient.SelectedValue.ToString();
                Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region Location Grid Support: OnGridMouseDown(), OnGridAfterSelect(), OnGridDoubleClick()
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
            int number = 0;
            LTLConsignee2 consignee = null;
            dlgLTLConsignee dlg = null;
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "csNew":
                        consignee = new LTLConsignee2();
                        consignee.ClientNumber = this.cboClient.SelectedValue.ToString();
                        consignee.Number = 0;
                        dlg = new dlgLTLConsignee(consignee);
                        dlg.Font = this.Font;
                        if(dlg.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            number = FreightGateway.CreateLTLConsignee(consignee);
                            MessageBox.Show(this, "New consignee created.", App.Product, MessageBoxButtons.OK);
                            Refresh();
                        }
                        break;
                    case "csOpen":
                        number = int.Parse(this.grdConsignees.Selected.Rows[0].Cells["ConsigneeNumber"].Value.ToString());
                        consignee = FreightGateway.ReadLTLConsignee(number, this.cboClient.SelectedValue.ToString());
                        dlg = new dlgLTLConsignee(consignee);
                        dlg.Font = this.Font;
                        if(dlg.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            FreightGateway.UpdateLTLConsignee(consignee);
                            MessageBox.Show(this, "Consignee updated.", App.Product, MessageBoxButtons.OK);
                            Refresh();
                        }
                        break;
					case "csRefresh":
						this.Cursor = Cursors.WaitCursor;
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
                //Determine access
                bool isDispatchSupervisor = false, isSalesRep = false;
                try {
                    isDispatchSupervisor = RoleServiceGateway.IsDispatchSupervisor;
                    isSalesRep = RoleServiceGateway.IsSalesRep;
                }
                catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }

                //Set menu states 
                this.csNew.Enabled = (isDispatchSupervisor || isSalesRep);
                this.csOpen.Enabled = (isDispatchSupervisor || isSalesRep) && this.grdConsignees.Selected.Rows.Count > 0;
                this.csRefresh.Enabled = true;
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { Application.DoEvents(); if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this, new EventArgs()); }
        }		
        private void reportStatus(StatusEventArgs e) { if (this.StatusMessage != null) this.StatusMessage(this,e); }
        private string ColumnHeaders {
            get {
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                this.grdConsignees.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if (value.Length > 0) {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdConsignees.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                    this.grdConsignees.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = SortIndicator.Ascending;
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
                    if (this.grdConsignees.ActiveCell == null || !this.grdConsignees.ActiveCell.IsInEditMode) {
                        reportStatus(new StatusEventArgs("Refreshing consignees..."));
                        this.mGridSvc.CaptureState();
                        lock (this.mConsignees) {
                            this.mConsignees.Clear();
                            this.mConsignees.Merge(ds);
                        }
                        this.mGridSvc.RestoreState();
                    }
                }
            }
            catch { }
        }
        #endregion
    }

    public class ConsigneeAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;
        private string mClientNumber = null;

        //Interface
        public ConsigneeAutoRefreshService(winConsignees postback) {
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
            try { e.Result = FreightGateway.ViewLTLConsignees(this.mClientNumber); }
            catch { }
        }
    }
}
