using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Argix.Enterprise;
using Argix.Security;
using Argix.Windows;

namespace Argix.Freight {
    //
    public partial class winMain : Form {
        //Members
        private Form mActiveForm = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr = null;
        private NameValueCollection mHelpItems = null;

        //Interface
        public winMain() {
            //Constructor
            try {
                //Required for Windows Form Designer support
                InitializeComponent();
                InitializeToolbox();
                this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
                #region Splash Screen Support
                Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
                Thread.Sleep(3000);
                #endregion
                #region Set window docking
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tsMain, this.msMain, this.ssMain });
                #endregion

                //Create data and UI servicesv
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 1000, 3000);
            }
            catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
        }
        private void OnFormLoad(object sender, System.EventArgs e) {
            //Load conditions
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
                    this.msViewToolbar.Checked = this.tsMain.Visible = global::Argix.Properties.Settings.Default.Toolbar;
                    this.msViewStatusBar.Checked = this.ssMain.Visible = global::Argix.Properties.Settings.Default.StatusBar;
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                #endregion
                #region Set tooltips
                this.mToolTip.InitialDelay = 500;
                this.mToolTip.AutoPopDelay = 3000;
                this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;		//Even when form is inactve
                #endregion

                //Navigation services
                this.tsClients.Enabled = this.tsShippers.Enabled = this.tsConsignees.Enabled = this.tsShipments.Enabled = this.tsQuickQuote.Enabled = this.tsReports.Enabled = true;
                
                //Set control defaults
                ServiceInfo si = FreightGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(si.TerminalID.ToString(), si.Description);
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Event handler for form closing event
            if(!e.Cancel) {
                //Save settings
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnFormResize(object sender, System.EventArgs e) {
            //Event handler for form resized event
        }
        #region User Services: OnNavClick(), OnItemClick(), OnHelpMenuClick(), OnServiceStatesChanged()
        private void OnNavClick(object sender, EventArgs e) {
            //Event handler for menu/toolbar item clicked
            Form win = null;
            IToolbar window = null;
            this.Cursor = Cursors.WaitCursor;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "tsClients":
                        this.mMessageMgr.AddMessage("Opening clients...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winClients) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winClients();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            window = (IToolbar)win;
                            window.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            window.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsShippers":
                        this.mMessageMgr.AddMessage("Opening shippers...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winShippers) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winShippers();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            window = (IToolbar)win;
                            window.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            window.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsConsignees":
                        this.mMessageMgr.AddMessage("Opening consignees...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winConsignees) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winConsignees();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            window = (IToolbar)win;
                            window.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            window.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsShipments":
                        this.mMessageMgr.AddMessage("Opening shipments...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winShipments) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winShipments();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            window = (IToolbar)win;
                            window.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            window.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsQuickQuote":
                        this.mMessageMgr.AddMessage("Opening quotes...");
                        for(int i = 0; i < this.MdiChildren.Length; i++) {
                            Form _win = (Form)this.MdiChildren[i];
                            if(_win is winQuotes) { win = _win; win.Activate(); break; }
                        }
                        if(win == null) {
                            win = new winQuotes();
                            win.MdiParent = this;
                            win.Activated += new EventHandler(OnWindowActivated);
                            win.Deactivate += new EventHandler(OnWindowDeactivated);
                            win.FormClosing += new FormClosingEventHandler(OnWindowClosing);
                            win.FormClosed += new FormClosedEventHandler(OnWindowClosed);
                            window = (IToolbar)win;
                            window.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            window.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                    case "tsReports":
                        this.mMessageMgr.AddMessage("Opening quotes...");
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
                            window = (IToolbar)win;
                            window.StatusMessage += new StatusEventHandler(OnStatusMessage);
                            window.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                            win.WindowState = FormWindowState.Maximized;
                            win.Show();
                        }
                        //win.WindowState = FormWindowState.Maximized;
                        this.mActiveForm = win;
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnItemClick(object sender, System.EventArgs e) {
            //Menu services
            IToolbar window = null;
            IPSPToolbar pspwindow = null;
            IQuoteToolbar quotewindow = null;
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "msFileNew":
                    case "tsbNew":
                        window = (IToolbar)this.mActiveForm;
                        window.New();
                        break;
                    case "msFileOpen":
                    case "tsbOpen":
                        window = (IToolbar)this.mActiveForm;
                        window.Open();
                        break;
                    case "msFileCancel":
                    case "tsbCancel":
                        window = (IToolbar)this.mActiveForm;
                        window.Cancel();
                        break;
                    case "msFileSave":
                    case "tsbSave":
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Export...";
                        dlgSave.FileName = "";
                        dlgSave.CheckFileExists = false;
                        dlgSave.OverwritePrompt = true;
                        dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            window = (IToolbar)this.mActiveForm;
                            window.Save(dlgSave.FileName);
                        }
                        break;
                    case "msFilePageSetup":
                        UltraGridPrinter.PageSettings();
                        break;
                    case "msFilePrint":
                        window = (IToolbar)this.mActiveForm;
                        window.Print(true);
                        break;
                    case "tsbPrint":
                        window = (IToolbar)this.mActiveForm;
                        window.Print(false);
                        break;
                    case "msFilePreview":
                        window = (IToolbar)this.mActiveForm;
                        window.PrintPreview();
                        break;
                    case "msFileExit":
                        this.Close();
                        break;
                    case "msViewRefresh":
                    case "tsbRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Refreshing...");
                        this.mActiveForm.Refresh();
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if(fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.tsNav.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar":
                        this.msViewToolbar.Checked = (!this.msViewToolbar.Checked);
                        this.tsMain.Visible = this.msViewToolbar.Checked;
                        break;
                    case "msViewStatusBar":
                        this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked);
                        this.ssMain.Visible = this.msViewStatusBar.Checked;
                        break;
                    case "msApprove":
                    case "tsbApprove":
                        pspwindow = (IPSPToolbar)this.mActiveForm;
                        pspwindow.ApproveClient();
                        break;
                    case "msDeny":
                    case "tsbDeny":
                        pspwindow = (IPSPToolbar)this.mActiveForm;
                        pspwindow.DenyClient();
                        break;
                    case "msPrintLabels":
                    case "tsbPrintLabels":
                        pspwindow = (IPSPToolbar)this.mActiveForm;
                        pspwindow.PrintLabels();
                        break;
                    case "msPrintPaperwork":
                    case "tsbPrintPaperwork":
                        pspwindow = (IPSPToolbar)this.mActiveForm;
                        pspwindow.PrintPaperwork();
                        break;
                    case "tsbApproveQuote":
                        quotewindow = (IQuoteToolbar)this.mActiveForm;
                        quotewindow.ApproveQuote();
                        break;
                    case "tsbTenderQuote":
                        quotewindow = (IQuoteToolbar)this.mActiveForm;
                        quotewindow.TenderQuote();
                        break;
                    case "tsbViewTender":
                        quotewindow = (IQuoteToolbar)this.mActiveForm;
                        quotewindow.ViewTender();
                        break;
                    case "tsbBookQuote":
                        quotewindow = (IQuoteToolbar)this.mActiveForm;
                        quotewindow.BookQuote();
                        break;
                    case "msToolsConfig":
                        App.ShowConfig();
                        break;
                    case "msWinCascade": this.LayoutMdi(MdiLayout.Cascade); break;
                    case "msWinTileH": this.LayoutMdi(MdiLayout.TileHorizontal); break;
                    case "msWinTileV": this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "msHelpAbout": new dlgAbout(App.Description, App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender, System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this, this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Local Services: setUserServices(), buildHelpMenu()
        private void setUserServices() {
            //Set user services
            try {
                //Set menu states 
                IToolbar window = (IToolbar)this.mActiveForm;
                this.msFileNew.Enabled = this.tsbNew.Enabled = window != null && window.CanNew;
                this.msFileOpen.Enabled = this.tsbOpen.Enabled = window != null && window.CanOpen;
                this.msFileCancel.Enabled = this.tsbCancel.Enabled = window != null && window.CanCancel;
                this.msFileSave.Enabled = this.tsbSave.Enabled = false;
                this.msFileSaveAs.Enabled = window != null && window.CanSave;
                this.msFilePageSetup.Enabled = true;
                this.msFilePrint.Enabled = this.tsbPrint.Enabled = window != null && window.CanPrint;
                this.msFilePreview.Enabled = window != null && window.CanPreview;
                this.msFileExit.Enabled = true;
                this.msEditSearch.Enabled = false;
                this.msViewRefresh.Enabled = this.tsbRefresh.Enabled = window != null;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                if(this.mActiveForm is IPSPToolbar) {
                    IPSPToolbar pspwindow = (IPSPToolbar)this.mActiveForm;
                    this.tsbApprove.Enabled = pspwindow.CanApproveClient;
                    this.tsbDeny.Enabled = pspwindow.CanDenyClient;
                    this.tsbPrintLabels.Enabled = pspwindow.CanPrintLabels;
                    this.tsbPrintPaperwork.Enabled = pspwindow.CanPrintPaperwork;
                }
                else {
                    this.tsbApprove.Enabled = this.tsbDeny.Enabled = this.tsbPrintLabels.Enabled = this.tsbPrintPaperwork.Enabled = false;
                }

                if(this.mActiveForm is IQuoteToolbar) {
                    IQuoteToolbar quotewindow = (IQuoteToolbar)this.mActiveForm;
                    this.tsbApproveQuote.Enabled = quotewindow.CanApproveQuote;
                    this.tsbTenderQuote.Enabled = quotewindow.CanTenderQuote;
                    this.tsbViewTender.Enabled = quotewindow.CanViewTender;
                    this.tsbBookQuote.Enabled = quotewindow.CanBookQuote;
                }
                else {
                    this.tsbApproveQuote.Enabled = this.tsbTenderQuote.Enabled = this.tsbViewTender.Enabled = this.tsbBookQuote.Enabled = false;
                }

                this.ssMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(FreightGateway.ServiceState, FreightGateway.ServiceAddress));
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
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
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Toolbox: InitializeToolbox(), OnToolboxResize(), ...
        private const string AUTOHIDE_OFF = "X";
        private const string AUTOHIDE_ON = "-";
        private void InitializeToolbox() {
            //Configure toolbox size, state, and event handlers
            try {
                //Iterate through control heirarchy and set handlers
                foreach(Control ctl1 in this.pnlToolbox.Controls) {
                    System.Diagnostics.Debug.WriteLine(ctl1.Name);
                    ctl1.Enter += new System.EventHandler(this.OnEnterToolbox);
                    ctl1.Leave += new System.EventHandler(this.OnLeaveToolbox);
                    ctl1.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                    ctl1.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                    foreach(Control ctl2 in ctl1.Controls) {
                        System.Diagnostics.Debug.WriteLine("\t" + ctl2.Name);
                        ctl2.Enter += new System.EventHandler(this.OnEnterToolbox);
                        ctl2.Leave += new System.EventHandler(this.OnLeaveToolbox);
                        ctl2.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                        ctl2.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                        foreach(Control ctl3 in ctl2.Controls) {
                            System.Diagnostics.Debug.WriteLine("\t\t" + ctl3.Name);
                            ctl3.Enter += new System.EventHandler(this.OnEnterToolbox);
                            ctl3.Leave += new System.EventHandler(this.OnLeaveToolbox);
                            ctl3.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                            ctl3.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                            foreach(Control ctl4 in ctl3.Controls) {
                                System.Diagnostics.Debug.WriteLine("\t\t\t" + ctl4.Name);
                                ctl4.Enter += new System.EventHandler(this.OnEnterToolbox);
                                ctl4.Leave += new System.EventHandler(this.OnLeaveToolbox);
                                ctl4.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                                ctl4.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                                foreach(Control ctl5 in ctl4.Controls) {
                                    System.Diagnostics.Debug.WriteLine("\t\t\t\t" + ctl5.Name);
                                    ctl5.Enter += new System.EventHandler(this.OnEnterToolbox);
                                    ctl5.Leave += new System.EventHandler(this.OnLeaveToolbox);
                                    ctl5.MouseEnter += new System.EventHandler(this.OnMouseEnterToolbox);
                                    ctl5.MouseLeave += new System.EventHandler(this.OnMouseLeaveToolbox);
                                }
                            }
                        }
                    }
                }

                //Configure auto-hide
                this.pnlToolbox.Width = global::Argix.Properties.Settings.Default.ToolboxWidth;
                this.lblPin.Text = global::Argix.Properties.Settings.Default.ToolboxAutoHide ? AUTOHIDE_ON : AUTOHIDE_OFF;
                this.lblPin.Click += new System.EventHandler(this.OnToggleAutoHide);
                this.tmrAutoHide.Interval = 500;
                this.tmrAutoHide.Tick += new System.EventHandler(this.OnAutoHideToolbox);
                OnLeaveToolbox(null, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnEnterToolbox(object sender, System.EventArgs e) {
            //Occurs when the control becomes the active control on the form
            try {
                //Disable auto-hide when active; show toolbar as active
                if(this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
                this.lblToolbox.BackColor = this.lblPin.BackColor = SystemColors.ActiveCaption;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch { }
        }
        private void OnLeaveToolbox(object sender, System.EventArgs e) {
            //Occurs when the control is no longer the active control on the form
            try {
                //Enable auto-hide when inactive and not pinned; show toolbar as inactive
                this.lblToolbox.BackColor = this.lblPin.BackColor = System.Drawing.SystemColors.Control;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
                if(this.lblPin.Text == AUTOHIDE_ON) { this.tmrAutoHide.Enabled = true; this.tmrAutoHide.Start(); }
            }
            catch { }
        }
        private void OnMouseEnterToolbox(object sender, System.EventArgs e) {
            //Occurs when the mouse enters the visible part of the control
            try {
                //Auto-open if not pinned and toolbar is closed; disable auto-hide if on
                if(this.lblPin.Text == AUTOHIDE_ON && this.pnlToolbox.Width == 25)
                    this.pnlToolbox.Width = global::Argix.Properties.Settings.Default.ToolboxWidth;
                if(this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
            }
            catch { }
        }
        private void OnMouseLeaveToolbox(object sender, System.EventArgs e) {
            //Occurs when the mouse leaves the visible part of the control
            try {
                //Enable auto-hide when inactive and unpinned
                if(this.lblToolbox.BackColor == SystemColors.Control && this.lblPin.Text == AUTOHIDE_ON) { this.tmrAutoHide.Enabled = true; this.tmrAutoHide.Start(); }
            }
            catch { }
        }
        private void OnToggleAutoHide(object sender, System.EventArgs e) { this.lblPin.Text = this.lblPin.Text == AUTOHIDE_OFF ? AUTOHIDE_ON : AUTOHIDE_OFF; }
        private void OnAutoHideToolbox(object sender, System.EventArgs e) {
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
        private void OnSplitterMoving(object sender, SplitterCancelEventArgs e) {
            try {
                this.lblToolbox.BackColor = this.lblPin.BackColor = SystemColors.ActiveCaption;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = SystemColors.ActiveCaptionText;
                if(this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Window services: OnWindowActivated(), OnWindowDeactivated(), OnWindowClosing(), OnWindowClosed(), OnServiceStatesChanged(), OnStatusMessage()
        private void OnWindowActivated(object sender, System.EventArgs e) {
            //Event handler for activaton of a child window
            try {
                this.mActiveForm = (Form)sender;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnWindowDeactivated(object sender, System.EventArgs e) {
            //Event handler for deactivaton of a child window
            this.mActiveForm = null;
            setUserServices();
        }
        private void OnWindowClosing(object sender, FormClosingEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                this.mActiveForm = null;
                setUserServices();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnWindowClosed(object sender, FormClosedEventArgs e) {
            //Event handler for closing of a child window
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                this.mActiveForm = null;
                setUserServices();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnServiceStatesChanged(object sender, System.EventArgs e) { setUserServices(); }
        private void OnStatusMessage(object sender, StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        #endregion
    }
}
