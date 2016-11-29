using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;

namespace Argix.Finance {
    //
    public partial class frmMain :Form {
        //Members
        private winDriverComp mActiveDC = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr = null;
        private NameValueCollection mHelpItems = null;
        
        //Interface
		public frmMain() {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tsMain,this.msMain, this.ssMain });
				#endregion
				
				//Create data and UI services
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 500, 3000);
				this.mToolTip = new System.Windows.Forms.ToolTip();
			}
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
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
                    switch(this.WindowState) {
                        case FormWindowState.Maximized: break;
                        case FormWindowState.Minimized: break;
                        case FormWindowState.Normal:
                            this.Location = global::Argix.Properties.Settings.Default.Location;
                            this.Size = global::Argix.Properties.Settings.Default.Size;
                            break;
                    }
                    this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = global::Argix.Properties.Settings.Default.Font;
                    this.msViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.msViewStatusBar.Checked = this.ssMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    this.msViewTermConfigs.Checked = !global::Argix.Properties.Settings.Default.TermConfigWindow;
                    this.msViewEquip.Checked = !global::Argix.Properties.Settings.Default.DriverEquipWindow;
                    this.msViewRates.Checked = !global::Argix.Properties.Settings.Default.DriverRatesWindow;
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
                this.mToolTip.InitialDelay = 500;
                this.mToolTip.AutoPopDelay = 3000;
                this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;		//Even when form is inactve
                #endregion

                //Set control defaults
                ServiceInfo t = FinanceGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";

                this.tabMain.TabPages.Clear();
                this.msViewTermConfigs.PerformClick();
                this.msViewEquip.PerformClick();
                this.msViewRates.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormResize(object sender,System.EventArgs e) {
            //Event handler for form size changes
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            if (!e.Cancel) {
                //Save settings
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.TermConfigWindow = this.msViewTermConfigs.Checked;
                global::Argix.Properties.Settings.Default.DriverEquipWindow = this.msViewEquip.Checked;
                global::Argix.Properties.Settings.Default.DriverRatesWindow = this.msViewRates.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        #region User Services: OnItemClick(), OnHelpMenuClick(), OnControlErrorMessage()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Menu services
            bool show = false;
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "msFileNew":
                    case "tsNew": 
                        break;
                    case "msFileOpen":
                    case "tsOpen": 
                        dlgOpen dlg = new dlgOpen();
                        dlg.Font = this.Font;
                        if (dlg.ShowDialog() == DialogResult.OK) {
                            CompensationAgent compAgent = new CompensationAgent(dlg.AgentNumber,dlg.AgentName,dlg.StartDate,dlg.EndDate);
                            winDriverComp win = null;
                            int winCount = this.MdiChildren.Length;
                            for (int i = 0;i < this.MdiChildren.Length;i++) {
                                win = (winDriverComp)this.MdiChildren[i];
                                if (win.Agent.Title == compAgent.Title) {
                                    win.Activate();
                                    if (win.WindowState == FormWindowState.Minimized) win.WindowState = FormWindowState.Normal;
                                    break;
                                }
                                else
                                    win = null;
                            }
                            if (win == null) {
                                this.mMessageMgr.AddMessage("Opening " + compAgent.AgentName.Trim() + " driver pay for week ending " + compAgent.EndDate + "...");
                                win = new winDriverComp(compAgent);
                                win.MdiParent = this;
                                win.Activated += new EventHandler(OnDCActivated);
                                win.Deactivate += new EventHandler(OnDCDeactivated);
                                win.Closing += new CancelEventHandler(OnDCClosing);
                                win.Closed += new EventHandler(OnDCClosed);
                                win.ServiceStatesChanged += new EventHandler(OnDCServiceChange);
                                win.StatusMessage += new StatusEventHandler(OnDCStatus);
                                win.WindowState = (winCount > 0) ? this.MdiChildren[0].WindowState : FormWindowState.Maximized;
                                win.Show();
                            }
                        }
                        break;
                    case "msFileSave":
                    case "tsSave": 
                        this.mActiveDC.Save(); 
                        break;
                    case "msFileSaveAs":           
                        this.mActiveDC.SaveAs();  
                        break;
                    case "msFileAddRoutes":
                    case "tsAddRoutes": 
                    this.mActiveDC.AddRoutes(); 
                        break;
                    case "msFileExport":
                    case "tsExport": 
                        this.mActiveDC.Export(); 
                        break;
                    case "msFileSetup":            this.mActiveDC.PrintSetup(); break;
                    case "msFilePrint":            this.mActiveDC.Print(true); break;
                    case "tsPrint":                this.mActiveDC.Print(false); ; break;
                    case "msFilePreview":          this.mActiveDC.PrintPreview(); break;
                    case "msFileExit":             this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "tsCut":      
                        this.mActiveDC.Cut(); 
                        break;
                    case "msEditCopy":
                    case "tsCopy": 
                        this.mActiveDC.Copy(); 
                        break;
                    case "msEditPaste":
                    case "tsPaste": 
                        this.mActiveDC.Paste(); 
                        break;
                    case "msEditDelete":
                    case "tsDelete": 
                        this.mActiveDC.Delete(); 
                        break;
                    case "msEditCheckAll":         
                        this.mActiveDC.CheckAll(); 
                        break;
                    case "msEditFind":             
                        break;
                    case "msViewRefresh":
                    case "tsRefresh": 
                        this.Cursor = Cursors.WaitCursor; 
                        this.mActiveDC.Refresh(); 
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewRoutes":           
                        this.mActiveDC.RoutesVisible = (this.msViewRoutes.Checked = (!this.msViewRoutes.Checked)); 
                        break;
                    case "msViewTermConfigs":
                        show = this.msViewTermConfigs.Checked = !this.msViewTermConfigs.Checked;
                        if(show) this.tabMain.TabPages.Add(this.tabTerm); else this.tabMain.TabPages.Remove(this.tabTerm);
                        if(show) this.tabMain.SelectedTab = this.tabTerm;
                        this.tabMain.Visible = this.splitterH.Visible = (this.tabMain.TabPages.Count > 0);
                        break;
                    case "msViewEquip":
                        show = this.msViewEquip.Checked = !this.msViewEquip.Checked;
                        if(show) this.tabMain.TabPages.Add(this.tabEquip); else this.tabMain.TabPages.Remove(this.tabEquip);
                        if(show) this.tabMain.SelectedTab = this.tabEquip;
                        this.tabMain.Visible = this.splitterH.Visible = (this.tabMain.TabPages.Count > 0);
                        break;
                    case "msViewRates":
                        show = this.msViewRates.Checked = !this.msViewRates.Checked;
                        if(show) this.tabMain.TabPages.Add(this.tabRates); else this.tabMain.TabPages.Remove(this.tabRates);
                        if(show) this.tabMain.SelectedTab = this.tabRates;
                        this.tabMain.Visible = this.splitterH.Visible = (this.tabMain.TabPages.Count > 0);
                        break;
                    case "msViewToolbar":          this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar":        this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msToolsConfig":          App.ShowConfig(); break;
                    case "msWinCascade":           this.LayoutMdi(MdiLayout.Cascade); break;
                    case "msWinTileH":             this.LayoutMdi(MdiLayout.TileHorizontal); break;
                    case "msWinTileV":             this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "msHelpAbout":            new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnControlErrorMessage(object source,ErrorEventArgs e) {
            //Event handler for error message from a control
            App.ReportError(e.Exception,true,LogLevel.Warning);
        }
        #endregion
        #region Local Services: setUserServices(), buildHelpMenu()
        private void setUserServices() {
            //Set user services
            try {
                //Set menu states
                this.msFileNew.Enabled = this.tsNew.Enabled = false;
                this.msFileOpen.Enabled = this.tsOpen.Enabled = true;
                this.msFileSave.Enabled = this.tsSave.Enabled = this.mActiveDC != null && this.mActiveDC.CanSave && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msFileSaveAs.Enabled = this.mActiveDC != null && this.mActiveDC.CanSaveAs && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msFileAddRoutes.Enabled = this.tsAddRoutes.Enabled = this.mActiveDC != null && this.mActiveDC.CanAddRoutes && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msFileExport.Enabled = this.tsExport.Enabled = this.mActiveDC != null && this.mActiveDC.CanExport && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msFileSetup.Enabled = this.mActiveDC != null;
                this.msFilePreview.Enabled = (this.mActiveDC != null && this.mActiveDC.CanPreview);
                this.msFilePrint.Enabled = this.tsPrint.Enabled = (this.mActiveDC != null && this.mActiveDC.CanPrint);
                this.msFileExit.Enabled = true;
                this.msEditCut.Enabled = this.tsCut.Enabled = this.mActiveDC != null && this.mActiveDC.CanCut && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msEditCopy.Enabled = this.tsCopy.Enabled = this.mActiveDC != null && this.mActiveDC.CanCopy && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msEditPaste.Enabled = this.tsPaste.Enabled = this.mActiveDC != null && this.mActiveDC.CanPaste && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msEditDelete.Enabled = this.tsDelete.Enabled = this.mActiveDC != null && this.mActiveDC.CanDelete && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.msEditCheckAll.Enabled = (this.mActiveDC != null && this.mActiveDC.CanCheckAll);
                this.msEditFind.Enabled = this.tsFind.Enabled = false;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msViewRefresh.Enabled = this.tsRefresh.Enabled = (this.mActiveDC != null);
                this.msViewRoutes.Enabled = (this.mActiveDC != null);
                this.msViewRoutes.Checked = (this.mActiveDC != null && this.mActiveDC.RoutesVisible);
                this.msViewTermConfigs.Enabled = true;
                this.msViewEquip.Enabled = true;
                this.msViewRates.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(FinanceGateway.ServiceState,FinanceGateway.ServiceAddress));
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Application.DoEvents(); }
        }
        private void buildHelpMenu() {
            //Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for(int i = 0; i < this.mHelpItems.Count; i++) {
                    string sKey = this.mHelpItems.GetKey(i);
                    string sValue = this.mHelpItems.GetValues(sKey)[0];
                    ToolStripMenuItem item = new ToolStripMenuItem();
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region Compensation Window Services: OnDCActivated(), OnDCDeactivated(), OnDCClosing(), OnDCClosed(), OnDCServiceChange(), OnDCStatus()
        private void OnDCActivated(object sender,System.EventArgs e) {
            //Event handler for activaton of a viewer child window
            try {
                this.mActiveDC = null;
                if(sender != null) {
                    winDriverComp frm = (winDriverComp)sender;
                    this.mActiveDC = frm;
                }
                if(this.mActiveDC != null) {
                    this.ctlRates1.Rates = this.mActiveDC.Agent.Rates;
                    this.lblRatesTitle.Text = "Driver Rates: " + this.mActiveDC.Agent.Rates.AgentName.Trim() + " (" + this.mActiveDC.Agent.Rates.EffectiveDate.ToString("MM-dd-yyyy") + ")";
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDCDeactivated(object sender,System.EventArgs e) {
            //Event handler for deactivaton of a viewer child window
            this.mActiveDC = null;
            this.ctlRates1.Rates = null;
            this.lblRatesTitle.Text = "Rates";
            setUserServices();
        }
        private void OnDCClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                e.Cancel = false;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDCClosed(object sender,System.EventArgs e) {
            //Event handler for closing of a viewer child window
            //if(this.MdiChildren.Length == 1 && this.tsFullScreen.Pushed) this.mnuViewFullScreen.PerformClick();
            this.ctlRates1.Rates = null;
            this.lblRatesTitle.Text = "Rates";
            setUserServices();
        }
        private void OnDCServiceChange(object sender,System.EventArgs e) { setUserServices(); }
        private void OnDCStatus(object sender,StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        #endregion
        #region Tool Windows: OnCloseToolWindow(), OnEnterToolWindow(), OnLeaveToolWindow()
        private void OnCloseToolWindow(object sender,EventArgs e) {
            //Event handler for closing rates window
            Label lbl = (Label)sender;
            switch(lbl.Name) {
                case "lblTermClose": this.msViewTermConfigs.PerformClick(); break;
                case "lblEquipClose": this.msViewEquip.PerformClick(); break;
                case "lblRatesClose":   this.msViewRates.PerformClick(); break;
            }
        }
        private void OnEnterToolWindow(object sender,EventArgs e) {
            //Event handler for entering rates window
            try {
                if(this.tabMain.SelectedTab == this.tabTerm) {
                    this.lblTermTitle.BackColor = this.lblTermClose.BackColor = SystemColors.ActiveCaption;
                    this.lblTermTitle.ForeColor = this.lblTermClose.ForeColor = SystemColors.ActiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabEquip) {
                    this.lblEquipTitle.BackColor = this.lblEquipClose.BackColor = SystemColors.ActiveCaption;
                    this.lblEquipTitle.ForeColor = this.lblEquipClose.ForeColor = SystemColors.ActiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabRates) {
                    this.lblRatesTitle.BackColor = this.lblRatesClose.BackColor = SystemColors.ActiveCaption;
                    this.lblRatesTitle.ForeColor = this.lblRatesClose.ForeColor = SystemColors.ActiveCaptionText;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveToolWindow(object sender,EventArgs e) {
            //Event handler for leaving rates window
            try {
                if(this.tabMain.SelectedTab == this.tabTerm) {
                    this.lblTermTitle.BackColor = this.lblTermClose.BackColor = SystemColors.InactiveCaption;
                    this.lblTermTitle.ForeColor = this.lblTermClose.ForeColor = SystemColors.InactiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabEquip) {
                    this.lblEquipTitle.BackColor = this.lblEquipClose.BackColor = SystemColors.InactiveCaption;
                    this.lblEquipTitle.ForeColor = this.lblEquipClose.ForeColor = SystemColors.InactiveCaptionText;
                }
                else if(this.tabMain.SelectedTab == this.tabRates) {
                    this.lblRatesTitle.BackColor = this.lblRatesClose.BackColor = SystemColors.InactiveCaption;
                    this.lblRatesTitle.ForeColor = this.lblRatesClose.ForeColor = SystemColors.InactiveCaptionText;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
    }
}