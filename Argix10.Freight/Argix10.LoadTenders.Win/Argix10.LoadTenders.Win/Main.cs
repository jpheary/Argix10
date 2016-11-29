using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Windows;

namespace Argix.Freight {
    //
    public partial class frmMain:Form {
        //Members
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvc = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr = null;
        private NameValueCollection mHelpItems = null;

        //Interface
        public frmMain() {
            //Constructor			
            this.Cursor = Cursors.WaitCursor;
            try {
                //Required for Windows Form Designer support
                InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);

                //Create data and UI services
                this.mPageSettings = new PageSettings();
                this.mPageSettings.Landscape = true;
                this.mGridSvc = new UltraGridSvc(this.grdMain);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],500,3000);
            }
            catch (Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure",ex); }
        }

        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                Splash.Close();
                this.Visible = true;
                Application.DoEvents();
                #region Set user preferences
                try {
                    this.WindowState = global::Argix.Properties.Settings.Default.WindowState;
                    switch (this.WindowState) {
                        case FormWindowState.Maximized: break;
                        case FormWindowState.Minimized: break;
                        case FormWindowState.Normal:
                            this.Location = global::Argix.Properties.Settings.Default.Location;
                            this.Size = global::Argix.Properties.Settings.Default.Size;
                            break;
                    }
                    this.msViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.msViewStatusbar.Checked = this.ssMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
                this.mToolTip.InitialDelay = 500;
                this.mToolTip.AutoPopDelay = 3000;
                this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;		//Even when form is inactve
                #endregion

                //Set control defaults
                #region Grid Initialization
                this.grdMain.DisplayLayout.Bands[0].Columns["Load"].SortIndicator = SortIndicator.Ascending;
                #endregion
                ServiceInfo si = TsortGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(si.TerminalID.ToString(),si.Description);
                this.ssMain.User1Panel.Width = 144;

                this.mClientDS.Merge(TsortGateway.GetClients());
                if (this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                this.dtpFrom.Value = DateTime.Today.AddDays(-7);
                this.dtpTo.Value = DateTime.Today;
                this.grdMain.Focus();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            if (!e.Cancel) {
                #region Save user preferences
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusbar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
            }
        }
        private void OnClientChanged(object sender,EventArgs e) {
            //Event handler for change in selected terminal
            try {
                this.msViewRefresh.PerformClick();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDatesChanged(object sender,System.EventArgs e) {
            //Event handler for sorted days changed
            try {
                this.msViewRefresh.PerformClick();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnLoadTenderSelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for change in selected load tender
            try {
                //
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #region Grid Support: OnGridMouseDown()
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                if (e.Button == MouseButtons.Right) {
                    UltraGrid oGrid = (UltraGrid)sender;
                    UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                    object oContext = oUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
                    if (oContext != null) {
                        //On row
                        UltraGridCell oCell = (UltraGridCell)oContext;
                        oGrid.ActiveRow = oCell.Row;
                        oGrid.ActiveRow.Selected = true;
                    }
                    else {
                        //Off row
                        oContext = oUIElement.GetContext(typeof(RowScrollRegion));
                        if (oContext != null) {
                            oGrid.ActiveRow = null;
                            if (oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
                        }
                    }
                    oGrid.Focus();
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #endregion
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Menu services
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "msFileNew":
                    case "tsNew":
                        break;
                    case "msFileOpen":
                    case "tsOpen":
                        if (this.grdMain.Selected.Rows.Count > 0) {
                            string loadNumber = this.grdMain.Selected.Rows[0].Cells["Load"].Value.ToString();
                            LoadTenderDS.LoadTenderTableRow loadTender = (LoadTenderDS.LoadTenderTableRow)this.mLoadTenderDS.LoadTenderTable.Select("Load='" + loadNumber + "'")[0];
                            new frmLoadTender(loadTender).Show();
                        }
                       break;
                    case "msFileSave":
                    case "tsSave":
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Export Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Freight As...";
                        dlgSave.FileName = this.cboClient.Text + ", " + DateTime.Today.ToLongDateString();
                        dlgSave.OverwritePrompt = true;
                        if (dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
                            Application.DoEvents();
                            this.mLoadTenderDS.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                        }
                        break;
                    case "msFilePageSetup": UltraGridPrinter.PageSettings(); break;
                    case "msFilePrint":
                        UltraGridPrinter.Print(this.grdMain,this.cboClient.Text.Trim().ToUpper() + " LOAD TENDERS , " + DateTime.Today.ToLongDateString(),true);
                        break;
                    case "tsPrint":
                        UltraGridPrinter.Print(this.grdMain,this.cboClient.Text.Trim().ToUpper() + " LOAD TENDERS , " + DateTime.Today.ToLongDateString(),false);
                        break;
                    case "msFilePrintPreview":
                    case "tsPrintPreview":
                        UltraGridPrinter.PrintPreview(this.grdMain,this.cboClient.Text.Trim().ToUpper() + " LOAD TENDERS , " + DateTime.Today.ToLongDateString());
                        break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "tsCut":
                        break;
                    case "msEditCopy":
                    case "tsCopy":
                        break;
                    case "msEditPaste":
                    case "tsPaste":
                        break;
                    case "msViewRefresh":
                    case "tsRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        this.mLoadTenderDS.Clear();
                        this.mLoadTenderDS.Merge(TsortGateway.GetLoadTenders(this.cboClient.SelectedValue.ToString(),this.dtpFrom.Value,this.dtpTo.Value));
                        break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusbar": this.ssMain.Visible = (this.msViewStatusbar.Checked = (!this.msViewStatusbar.Checked)); break;
                    case "msToolsConfig": App.ShowConfig(); break;
                    case "msHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch (Exception) { }
        }
        #endregion
        #region Local Services: setUserServices(), buildHelpMenu()
        private void setUserServices() {
            //Set user services
            try {
                this.msFileNew.Enabled = this.tsNew.Enabled = false;
                this.msFileOpen.Enabled = this.tsOpen.Enabled = this.grdMain.Selected.Rows.Count > 0;
                this.msFileSave.Enabled = false;
                //this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = false;
                this.msFilePageSetup.Enabled = true;
                this.msFilePrintPreview.Enabled = true;
                this.msFilePrint.Enabled = true;
                //this.tsPrint.Enabled = true;
                this.msFileExit.Enabled = true;
                this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
                //this.msEditCut.Enabled = this.msEditCopy.Enabled = this.msEditPaste.Enabled = false;
                //this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                //this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(TsortGateway.ServiceState,TsortGateway.ServiceAddress));
                this.ssMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                this.ssMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Application.DoEvents(); }
        }
        private void buildHelpMenu() {
            //Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for (int i = 0;i < this.mHelpItems.Count;i++) {
                    string sKey = this.mHelpItems.GetKey(i);
                    string sValue = this.mHelpItems.GetValues(sKey)[0];
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    //item.Name = "msHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
            catch (Exception) { }
        }
        #endregion
    }
}
