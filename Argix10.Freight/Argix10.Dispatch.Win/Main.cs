using System;
using System.Configuration;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Argix.Security;
using Argix.Windows;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
    //
    public partial class Main:Form {
        //Members
        private Form mActiveForm = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
        private BlogAutoRefreshService mAutoRefreshSvc = null;
        private DateTime mLastBlogUpdate = DateTime.MinValue;

        //Interface
        public Main() {
			//Constructor
			try {
                InitializeComponent();
                InitializeToolbox();
                this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                ShippersLoader loader = new ShippersLoader();   //Load and cache the shippers on a background thread 
                Thread.Sleep(5000);

                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],1000,3000);
                this.mAutoRefreshSvc = new BlogAutoRefreshService(this);
            }
            catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
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
                    this.Font = this.msMain.Font = this.tsMain.Font = this.tsNav.Font = this.ssMain.Font = global::Argix.Properties.Settings.Default.Font;
                    this.msViewToolbar.Checked = this.tsMain.Visible = global::Argix.Properties.Settings.Default.Toolbar;
                    this.msViewStatusBar.Checked = this.ssMain.Visible = global::Argix.Properties.Settings.Default.StatusBar;
                    this.msViewTemplates.Checked = global::Argix.Properties.Settings.Default.Templates;
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

                //Navigation services
                this.tsClientInbound.Enabled = this.tsInbound.Enabled = this.tsOutbound.Enabled = this.tsPickupLog.Enabled = this.tsTrailerTracking.Enabled = true;

                //Set control defaults
                string terminal = Program.TerminalCode;
                ServiceInfo si = FreightGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(si.TerminalID.ToString(),si.Description);
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
                this.ssMain.User2Panel.Width = 48;
                this.ssMain.User2Panel.Text = Program.TerminalCode.Trim().Length > 0 ? Program.TerminalCode : "All";
                this.ssMain.User2Panel.ToolTipText = "Operating terminal";

                //Toolbox
                OnLeaveToolbox(null,EventArgs.Empty);
                this.mAutoRefreshSvc.Start();
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            if(!e.Cancel) {
                //Save settings
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.Templates = this.msViewTemplates.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        #region User Services: OnNavClick(), OnItemClick(), OnHelpMenuClick()
        private void OnNavClick(object sender,EventArgs e) {
            //Event handler for menu/toolbar item clicked
            Form win = null;
            ISchedule schedule = null;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "tsClientInbound":
                        this.mMessageMgr.AddMessage("Opening Client Inbound Schedule...");
                        for (int i = 0;i < this.MdiChildren.Length;i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if (_win is winClientInbound) { win = _win; win.Activate(); break; }
                        }
                        if (win == null) {
                            win = new winClientInbound();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized; 
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsInbound":
                        this.mMessageMgr.AddMessage("Opening Inbound Schedule...");
                        for (int i = 0;i < this.MdiChildren.Length;i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if (_win is winInbound) { win = _win; win.Activate(); break; }
                        }
                        if (win == null) {
                            win = new winInbound();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized; 
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsOutbound":
                        this.mMessageMgr.AddMessage("Opening Outbound Schedule...");
                        for (int i = 0;i < this.MdiChildren.Length;i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if (_win is winOutbound) { win = _win; win.Activate(); break; }
                        }
                        if (win == null) {
                            win = new winOutbound();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized; 
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsPickupLog":
                        this.mMessageMgr.AddMessage("Opening Pickup Log...");
                        for (int i = 0;i < this.MdiChildren.Length;i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if (_win is winPickupLog) { win = _win; win.Activate(); break; }
                        }
                        if (win == null) {
                            win = new winPickupLog();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized; 
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsTrailerTracking":
                        this.mMessageMgr.AddMessage("Opening Trailer Log...");
                        for (int i = 0;i < this.MdiChildren.Length;i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if (_win is winTrailerLog) { win = _win; win.Activate(); break; }
                        }
                        if (win == null) {
                            win = new winTrailerLog();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsLoadTenders":
                        this.mMessageMgr.AddMessage("Opening PCS Load Tenders...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winLoadTenderLog) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winLoadTenderLog();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsBBB":
                        this.mMessageMgr.AddMessage("Opening BBB Schedule...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winBBB) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winBBB();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            schedule.TemplatesVisible = this.msViewTemplates.Checked && RoleServiceGateway.IsDispatchSupervisor;
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsReports":
                        this.mMessageMgr.AddMessage("Opening reports...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winReports) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winReports();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            schedule = (ISchedule)win;
                            schedule.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            schedule.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnItemClick(object sender,EventArgs e) {
            //Event handler for menu/toolbar item clicked
            ISchedule schedule = null;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
                    case "msFileNew":
                    case "tsNew":
                    case "csNew":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.New();
                        break;
                    case "msFileOpen":
                    case "tsOpen":
                    case "csOpen":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.Open();
                       break;
                    case "msFileClone":
                    case "tsClone":
                    case "csClone":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.Clone();
                        break;
                    case "msFileCancel":
                    case "tsCancel":
                    case "csCancel":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.Cancel();
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Export Schedule...";
                        dlgSave.FileName = this.mActiveForm.Text;
                        dlgSave.CheckFileExists = false;
                        dlgSave.OverwritePrompt = true;
                        dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                        if (dlgSave.ShowDialog(this) == DialogResult.OK) {
                            schedule = (ISchedule)this.mActiveForm;
                            schedule.Save(dlgSave.FileName);
                        }
                        break;
                    case "msFileExport":
                    case "csExport":
                        SaveFileDialog dlgExport = new SaveFileDialog();
                        dlgExport.AddExtension = true;
                        dlgExport.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                        dlgExport.FilterIndex = 0;
                        dlgExport.Title = "Export Pickup Orders...";
                        dlgExport.FileName = "DailyPickups";
                        dlgExport.CheckFileExists = false;
                        dlgExport.OverwritePrompt = false;
                        dlgExport.InitialDirectory = ConfigurationManager.AppSettings["ExportPath" + Program.TerminalCode];
                        if (dlgExport.ShowDialog(this) == DialogResult.OK) {
                            schedule = (ISchedule)this.mActiveForm;
                            schedule.Export(dlgExport.FileName);
                        }
                       break;
                    case "tsExport":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.Export();
                        break;
                    case "msFilePageSetup":
                        UltraGridPrinter.PageSettings(); 
                        break;
                    case "msFilePrint":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.Print(true);
                        break;
                    case "msFilePrintPreview":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.PrintPreview();
                        break;
                    case "msFileExit":
                        this.Close();
                        break;
                    case "msViewRefresh":
                    case "tsRefresh":
                        this.mActiveForm.Refresh();
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.tsNav.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewFullScreen":
                    case "tsFullScreen":
                        this.tsNav.Visible = !(this.msViewFullScreen.Checked = (!this.msViewFullScreen.Checked));
                    break;
                    case "msViewTemplates":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.TemplatesVisible = (this.msViewTemplates.Checked = (!this.msViewTemplates.Checked));
                        break;
                    case "msViewToolbar":   this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msToolsConfig":   App.ShowConfig(); break;
                    case "msWinCascade": this.LayoutMdi(MdiLayout.Cascade); break;
                    case "msWinTileH":      this.LayoutMdi(MdiLayout.TileHorizontal); break;
                    case "msWinTileV":      this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "msHelpAbout":     new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                    case "tsTempNew": 
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.NewTemplate();
                        break;
                    case "tsTempOpen": 
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.OpenTemplate();
                        break;
                    case "tsTempCancel":
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.CancelTemplate();
                        break;
                    case "tsTempLoad": 
                        schedule = (ISchedule)this.mActiveForm;
                        schedule.LoadTemplates();
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception) { }
        }
        #endregion
        #region Local Services: setUserServices(), buildHelpMenu()
        private void setUserServices() {
            //Set user services depending upon an item selected in the grid
            try {
                ISchedule schedule = (ISchedule)this.mActiveForm;
                this.msFileNew.Enabled = this.csNew.Enabled = this.tsNew.Enabled = schedule != null && schedule.CanNew;
                this.msFileOpen.Enabled = this.csOpen.Enabled = this.tsOpen.Enabled = schedule != null && schedule.CanOpen;
                this.msFileClone.Enabled = this.csClone.Enabled = this.tsClone.Enabled = schedule != null && schedule.CanClone;
                this.msFileCancel.Enabled = this.csCancel.Enabled = this.tsCancel.Enabled = schedule != null && schedule.CanCancel;
                this.msFileSaveAs.Enabled = schedule != null && schedule.CanSave;
                this.msFileExport.Enabled = this.csExport.Enabled = this.tsExport.Enabled = schedule != null && schedule.CanExport;
                this.msFilePageSetup.Enabled = true;
                this.msFilePrint.Enabled = schedule != null && schedule.CanPrint;
                this.msFilePrintPreview.Enabled = schedule != null && schedule.CanPreview;
                this.msFileExit.Enabled = true;
                this.msViewRefresh.Enabled = this.tsRefresh.Enabled = schedule != null;
                this.msViewTemplates.Enabled = schedule != null && RoleServiceGateway.IsDispatchSupervisor;
                this.msViewTemplates.Checked = schedule != null && schedule.TemplatesVisible;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.tsTempNew.Enabled = schedule != null && schedule.CanNewTemplate;
                this.tsTempOpen.Enabled = schedule != null && schedule.CanOpenTemplate;
                this.tsTempCancel.Enabled = schedule != null && schedule.CanCancelTemplate;
                this.tsTempLoad.Enabled = schedule != null && schedule.CanLoadTemplates;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(FreightGateway.ServiceState,FreightGateway.ServiceAddress));
                //this.ssMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                //this.ssMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
		private void buildHelpMenu() {
			//Build dynamic help menu from configuration file
            try {
                //Read help menu configuration from app.config
                this.mHelpItems = (NameValueCollection)ConfigurationManager.GetSection("menu/help");
                for(int i = 0;i < this.mHelpItems.Count;i++) {
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
        #region Quote services: OnWindowActivated(), OnWindowDeactivated(), OnWindowClosing(), OnWindowClosed(), OnServiceStatesChanged(), OnStatusMessage()
        private void OnWindowActivated(object sender,System.EventArgs e) {
            //Event handler for activaton of a child window
            try {
                this.mActiveForm = (Form)sender;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnWindowDeactivated(object sender,System.EventArgs e) {
            //Event handler for deactivaton of a child window
            this.mActiveForm = null;
            setUserServices();
        }
        private void OnWindowClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                this.mActiveForm = null;
                setUserServices();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnWindowClosed(object sender,FormClosedEventArgs e) {
            //Event handler for closing of a child window
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                this.mActiveForm = null;
                setUserServices();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnServiceStatesChanged(object sender,System.EventArgs e) { setUserServices(); }
        private void OnStatusMessage(object sender,StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        #endregion
        #region Toolbox: InitializeToolbox(), OnToolboxResize(), ...
        private const string AUTOHIDE_OFF = "X";
        private const string AUTOHIDE_ON = "-";
        private void InitializeToolbox() {
            //Configure toolbox size, state, and event handlers
            try {
                //Set parent tab control, splitter, and pin button event handlers
                this.tabToolbox.Enter += new System.EventHandler(this.OnEnterToolbox);
                this.tabToolbox.Leave += new System.EventHandler(this.OnLeaveToolbox);
                this.tabToolbox.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                this.tabToolbox.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                foreach (Control ctl1 in this.pnlToolbox.Controls) {
                    foreach (Control ctl2 in ctl1.Controls) {
                        foreach (Control ctl3 in ctl2.Controls) {
                            ctl3.Enter += new System.EventHandler(this.OnEnterToolbox);
                            ctl3.Leave += new System.EventHandler(this.OnLeaveToolbox);
                            ctl3.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                            ctl3.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                        }
                        ctl2.Enter += new System.EventHandler(this.OnEnterToolbox);
                        ctl2.Leave += new System.EventHandler(this.OnLeaveToolbox);
                        ctl2.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                        ctl2.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                    }
                    ctl1.Enter += new System.EventHandler(this.OnEnterToolbox);
                    ctl1.Leave += new System.EventHandler(this.OnLeaveToolbox);
                    ctl1.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                    ctl1.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                }

                //Configure auto-hide
                this.pnlToolbox.Width = global::Argix.Properties.Settings.Default.ToolboxWidth;
                this.lblPin.Text = global::Argix.Properties.Settings.Default.ToolboxAutoHide ? AUTOHIDE_ON : AUTOHIDE_OFF;
                this.lblPin.Click += new System.EventHandler(this.OnToggleAutoHide);
                this.tmrAutoHide.Interval = 500;
                this.tmrAutoHide.Tick += new System.EventHandler(this.OnAutoHideToolbox);

                //Show toolbar as inactive
                this.lblToolbox.BackColor = System.Drawing.SystemColors.Control;
                this.lblToolbox.ForeColor = System.Drawing.SystemColors.ControlText;
                this.lblPin.BackColor = System.Drawing.SystemColors.Control;
                this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnEnterToolbox(object sender,System.EventArgs e) {
            //Occurs when the control becomes the active control on the form
            try {
                //Disable auto-hide when active; show toolbar as active
                if (this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
                this.lblToolbox.BackColor = this.lblPin.BackColor = SystemColors.ActiveCaption;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch { }
        }
        private void OnLeaveToolbox(object sender,System.EventArgs e) {
            //Occurs when the control is no longer the active control on the form
            try {
                //Enable auto-hide when inactive and not pinned; show toolbar as inactive
                this.lblToolbox.BackColor = this.lblPin.BackColor = System.Drawing.SystemColors.Control;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
                if (this.lblPin.Text == AUTOHIDE_ON) { this.tmrAutoHide.Enabled = true; this.tmrAutoHide.Start(); }
            }
            catch { }
        }
        private void OnMouseEnterToolbox(object sender,System.EventArgs e) {
            //Occurs when the mouse enters the visible part of the control
            try {
                //Auto-open if not pinned and toolbar is closed; disable auto-hide if on
                if (this.lblPin.Text == AUTOHIDE_ON && this.pnlToolbox.Width == 25)
                    this.pnlToolbox.Width = global::Argix.Properties.Settings.Default.ToolboxWidth;
                if (this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
            }
            catch { }
        }
        private void OnMouseLeaveToolbox(object sender,System.EventArgs e) {
            //Occurs when the mouse leaves the visible part of the control
            try {
                //Enable auto-hide when inactive and unpinned
                if (this.lblToolbox.BackColor == SystemColors.Control && this.lblPin.Text == AUTOHIDE_ON) { this.tmrAutoHide.Enabled = true; this.tmrAutoHide.Start(); }
            }
            catch { }
        }
        private void OnToggleAutoHide(object sender,System.EventArgs e) { this.lblPin.Text = this.lblPin.Text == AUTOHIDE_OFF ? AUTOHIDE_ON : AUTOHIDE_OFF; }
        private void OnAutoHideToolbox(object sender,System.EventArgs e) {
            //Toolbox timer event handler
            try {
                //Auto-close timer
                this.tmrAutoHide.Stop();
                this.tmrAutoHide.Enabled = false;
                global::Argix.Properties.Settings.Default.ToolboxWidth = this.pnlToolbox.Width;
                this.pnlToolbox.Width = 25;
            }
            catch { }
        }
        private void OnSplitterMoving(object sender,SplitterCancelEventArgs e) {
            try {
                this.lblToolbox.BackColor = this.lblPin.BackColor = SystemColors.ActiveCaption;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = SystemColors.ActiveCaptionText;
                if (this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnBlogKeyUp(object sender,KeyEventArgs e) {
            //Event handler for new blog entry
            Cursor.Current = Cursors.WaitCursor;
            try {
                if (e.KeyCode == Keys.Enter) {
                    BlogEntry entry = new BlogEntry();
                    entry.Date = DateTime.Now;
                    entry.Comment = this.txtComment.Text;
                    entry.UserID = Environment.UserName;
                    if (FreightGateway.AddBlogEntry(entry)) {
                        this.txtComment.Clear();
                        lock (this.txtBlog) {
                            DispatchDataset ds = FreightGateway.ViewBlog();
                            refreshBlog(ds);
                        }

                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { Cursor.Current = Cursors.Default; }
        }
        #endregion
        #region AutoRefresh Services: OnAutoRefreshCompleted(), refreshBlog()
        public void OnAutoRefreshCompleted(object sender,RunWorkerCompletedEventArgs e) {
            //
            try {
                DispatchDataset ds = null;
                if (this.InvokeRequired) {
                    this.Invoke(new RunWorkerCompletedEventHandler(OnAutoRefreshCompleted),new object[] { sender,e });
                }
                else {
                    ds = (DispatchDataset)e.Result;
                    lock (this.txtBlog) {
                        refreshBlog(ds);
                    }
                }
            }
            catch { }
        }
        private void refreshBlog(DispatchDataset ds) {
            //Update the Blog- resultset is ordered by Date ASC
            if (ds.BlogTable[ds.BlogTable.Rows.Count - 1].Date.CompareTo(this.mLastBlogUpdate) > 0) {
                this.mLastBlogUpdate = ds.BlogTable[ds.BlogTable.Rows.Count - 1].Date;
                this.txtBlog.Clear();
                for (int i = 0;i < ds.BlogTable.Rows.Count;i++) {
                    string entry = ds.BlogTable[i].Date.ToString("MM/dd/yyyy hh:mm tt") + " [" + ds.BlogTable[i].UserID + "]\r\n" + ds.BlogTable[i].Comment + "\r\n";
                    this.txtBlog.AppendText(entry);
                    this.txtBlog.AppendText("\r\n");
                }
            }
        }
        #endregion
    }

    public class BlogAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;

        //Interface
        public BlogAutoRefreshService(Main postback) {
            //
            this.mTimer = new System.Windows.Forms.Timer();
            this.mTimer.Interval = 60000;
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
            try { e.Result = FreightGateway.ViewBlog(); }
            catch { }
        }
    }
}
