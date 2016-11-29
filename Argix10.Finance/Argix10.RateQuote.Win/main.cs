using System;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.ComponentModel;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Argix.Windows;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Finance {
    //
    public class frmMain:System.Windows.Forms.Form {
        //Members
        private Form mActiveForm = null;
        private UltraGridSvc mReqGridSvc = null;
        private PageSettings mPageSettings = null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;

        private const string MSG_TITLE = "Argix Logistics Rate Quote";

        #region Controls

        private ContextMenuStrip csMain;
        private ToolStripMenuItem csDelete;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msHelp;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSave;
        private ToolStripButton tsPrint;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsRefresh;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSave;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripMenuItem msFileExport;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileSettings;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePreview;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStripButton tsExport;
        private ArgixStatusBar ssMain;
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private BindingSource mTariffs;
        private ToolStripMenuItem msViewRequestGrid;
        private Panel pnlMain;
        private SplitContainer scRequest;
        private UltraGrid grdTariffs;
        private PropertyGrid grdRequest;
        private ComboBox cboRequestType;
        private Panel pnlRequestHeader;
        private Label lblCloseRequest;
        private Label lblRequestHeader;
        private Splitter splMain;
        private IContainer components;
        #endregion

        //Interface
        public frmMain() {
            //Constructor
            InitializeComponent();
            this.Text = "Argix Logistics " + App.Product;
            buildHelpMenu();
            Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
            Thread.Sleep(3000);
            
            //Create data and UI services
            this.mPageSettings = new PageSettings();
            this.mPageSettings.Landscape = true;
            this.mReqGridSvc = new UltraGridSvc(this.grdTariffs);
            this.mToolTip = new System.Windows.Forms.ToolTip();
            this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],1000,3000);
            configApplication();
        }
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose(disposing); }
        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DataModule", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PropertyChanged1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("descriptionField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("effectiveDateField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("productNumberField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("releaseField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("tariffNameField");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewRequestGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsExport = new System.Windows.Forms.ToolStripButton();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.scRequest = new System.Windows.Forms.SplitContainer();
            this.grdTariffs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.grdRequest = new System.Windows.Forms.PropertyGrid();
            this.cboRequestType = new System.Windows.Forms.ComboBox();
            this.pnlRequestHeader = new System.Windows.Forms.Panel();
            this.lblCloseRequest = new System.Windows.Forms.Label();
            this.lblRequestHeader = new System.Windows.Forms.Label();
            this.splMain = new System.Windows.Forms.Splitter();
            this.mTariffs = new System.Windows.Forms.BindingSource(this.components);
            this.csMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scRequest)).BeginInit();
            this.scRequest.Panel1.SuspendLayout();
            this.scRequest.Panel2.SuspendLayout();
            this.scRequest.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTariffs)).BeginInit();
            this.pnlRequestHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTariffs)).BeginInit();
            this.SuspendLayout();
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csDelete});
            this.csMain.Name = "cmsMain";
            this.csMain.Size = new System.Drawing.Size(108, 26);
            // 
            // csDelete
            // 
            this.csDelete.Name = "csDelete";
            this.csDelete.Size = new System.Drawing.Size(107, 22);
            this.csDelete.Text = "Delete";
            this.csDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msEdit,
            this.msView,
            this.msTools,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(865, 24);
            this.msMain.TabIndex = 30;
            this.msMain.Text = "menuStrip1";
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFileSep1,
            this.msFileSave,
            this.msFileSaveAs,
            this.msFileExport,
            this.msFileSep2,
            this.msFileSettings,
            this.msFilePrint,
            this.msFilePreview,
            this.msFileSep3,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37, 20);
            this.msFile.Text = "&File";
            // 
            // msFileNew
            // 
            this.msFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(159, 22);
            this.msFileNew.Text = "&New Quote";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(159, 22);
            this.msFileOpen.Text = "&Open Quote...";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(156, 6);
            // 
            // msFileSave
            // 
            this.msFileSave.Image = global::Argix.Properties.Resources.Save;
            this.msFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(159, 22);
            this.msFileSave.Text = "&Save Quote";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(159, 22);
            this.msFileSaveAs.Text = "Save Quote &As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.msFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExport.Name = "msFileExport";
            this.msFileExport.Size = new System.Drawing.Size(159, 22);
            this.msFileExport.Text = "&Export Rates...";
            this.msFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(156, 6);
            // 
            // msFileSettings
            // 
            this.msFileSettings.Image = global::Argix.Properties.Resources.PageSetup;
            this.msFileSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.msFileSettings.Name = "msFileSettings";
            this.msFileSettings.Size = new System.Drawing.Size(159, 22);
            this.msFileSettings.Text = "Page &Settings...";
            this.msFileSettings.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(159, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(159, 22);
            this.msFilePreview.Text = "Print Pre&view...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(156, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(159, 22);
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEdit
            // 
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 20);
            this.msEdit.Text = "&Edit";
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewRequestGrid,
            this.msViewFont,
            this.msViewSep2,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 20);
            this.msView.Text = "&View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = global::Argix.Properties.Resources.FormulaEvaluator;
            this.msViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(154, 22);
            this.msViewRefresh.Text = "&Calculate Rates";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(151, 6);
            // 
            // msViewRequestGrid
            // 
            this.msViewRequestGrid.Name = "msViewRequestGrid";
            this.msViewRequestGrid.Size = new System.Drawing.Size(154, 22);
            this.msViewRequestGrid.Text = "Request Grid";
            this.msViewRequestGrid.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(154, 22);
            this.msViewFont.Text = "Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(151, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(154, 22);
            this.msViewToolbar.Text = "&Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(154, 22);
            this.msViewStatusBar.Text = "&StatusBar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msTools
            // 
            this.msTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msToolsConfig});
            this.msTools.Name = "msTools";
            this.msTools.Size = new System.Drawing.Size(48, 20);
            this.msTools.Text = "&Tools";
            // 
            // msToolsConfig
            // 
            this.msToolsConfig.Name = "msToolsConfig";
            this.msToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.msToolsConfig.Text = "&Configuration...";
            this.msToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 20);
            this.msHelp.Text = "&Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(178, 22);
            this.msHelpAbout.Text = "&About Rate Quote...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(175, 6);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsOpen,
            this.tsSep1,
            this.tsSave,
            this.tsExport,
            this.tsPrint,
            this.tsSep2,
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(865, 52);
            this.tsMain.TabIndex = 32;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 49);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New rate quote";
            this.tsNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 49);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open an existing rate quote";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 49);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save the current rate quote";
            this.tsSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsExport
            // 
            this.tsExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.tsExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExport.Name = "tsExport";
            this.tsExport.Size = new System.Drawing.Size(44, 49);
            this.tsExport.Text = "Export";
            this.tsExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsExport.ToolTipText = "Export the current rates to Excel";
            this.tsExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 49);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print the current rates";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 52);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.FormulaEvaluator;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(60, 49);
            this.tsRefresh.Text = "Calculate";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Calculate rates for this quote";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 370);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(865, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 33;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.scRequest);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(0, 76);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(300, 294);
            this.pnlMain.TabIndex = 37;
            // 
            // scRequest
            // 
            this.scRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scRequest.Location = new System.Drawing.Point(0, 0);
            this.scRequest.Name = "scRequest";
            this.scRequest.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scRequest.Panel1
            // 
            this.scRequest.Panel1.Controls.Add(this.grdTariffs);
            // 
            // scRequest.Panel2
            // 
            this.scRequest.Panel2.Controls.Add(this.grdRequest);
            this.scRequest.Panel2.Controls.Add(this.cboRequestType);
            this.scRequest.Panel2.Controls.Add(this.pnlRequestHeader);
            this.scRequest.Size = new System.Drawing.Size(300, 294);
            this.scRequest.SplitterDistance = 94;
            this.scRequest.TabIndex = 39;
            // 
            // grdTariffs
            // 
            this.grdTariffs.DataSource = this.mTariffs;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTariffs.DisplayLayout.Appearance = appearance1;
            ultraGridColumn7.Header.VisiblePosition = 5;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.Caption = "Description";
            ultraGridColumn8.Header.VisiblePosition = 1;
            ultraGridColumn8.Width = 200;
            ultraGridColumn9.Header.Caption = "Effective Date";
            ultraGridColumn9.Header.VisiblePosition = 2;
            ultraGridColumn10.Header.Caption = "Product#";
            ultraGridColumn10.Header.VisiblePosition = 3;
            ultraGridColumn10.Width = 75;
            ultraGridColumn11.Header.Caption = "Release";
            ultraGridColumn11.Header.VisiblePosition = 4;
            ultraGridColumn11.Width = 60;
            ultraGridColumn12.Header.Caption = "Name";
            ultraGridColumn12.Header.VisiblePosition = 0;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12});
            this.grdTariffs.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdTariffs.DisplayLayout.CaptionAppearance = appearance2;
            this.grdTariffs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTariffs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdTariffs.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdTariffs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTariffs.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTariffs.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdTariffs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTariffs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTariffs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTariffs.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTariffs.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTariffs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTariffs.Location = new System.Drawing.Point(0, 0);
            this.grdTariffs.Name = "grdTariffs";
            this.grdTariffs.Size = new System.Drawing.Size(300, 94);
            this.grdTariffs.TabIndex = 34;
            this.grdTariffs.Text = "Available Tariffs";
            this.grdTariffs.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTariffAfterSelectChange);
            // 
            // grdRequest
            // 
            this.grdRequest.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRequest.Location = new System.Drawing.Point(0, 45);
            this.grdRequest.Name = "grdRequest";
            this.grdRequest.Size = new System.Drawing.Size(300, 151);
            this.grdRequest.TabIndex = 35;
            // 
            // cboRequestType
            // 
            this.cboRequestType.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboRequestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRequestType.FormattingEnabled = true;
            this.cboRequestType.Items.AddRange(new object[] {
            "LTL Simple",
            "LTL Single",
            "LTL Multiple"});
            this.cboRequestType.Location = new System.Drawing.Point(0, 24);
            this.cboRequestType.Name = "cboRequestType";
            this.cboRequestType.Size = new System.Drawing.Size(300, 21);
            this.cboRequestType.TabIndex = 36;
            this.cboRequestType.SelectionChangeCommitted += new System.EventHandler(this.OnRequestTypeChanged);
            // 
            // pnlRequestHeader
            // 
            this.pnlRequestHeader.Controls.Add(this.lblCloseRequest);
            this.pnlRequestHeader.Controls.Add(this.lblRequestHeader);
            this.pnlRequestHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRequestHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlRequestHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlRequestHeader.Name = "pnlRequestHeader";
            this.pnlRequestHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlRequestHeader.Size = new System.Drawing.Size(300, 24);
            this.pnlRequestHeader.TabIndex = 124;
            // 
            // lblCloseRequest
            // 
            this.lblCloseRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseRequest.BackColor = System.Drawing.SystemColors.Control;
            this.lblCloseRequest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloseRequest.Location = new System.Drawing.Point(279, 4);
            this.lblCloseRequest.Name = "lblCloseRequest";
            this.lblCloseRequest.Size = new System.Drawing.Size(16, 16);
            this.lblCloseRequest.TabIndex = 115;
            this.lblCloseRequest.Text = "X";
            this.lblCloseRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblRequestHeader
            // 
            this.lblRequestHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblRequestHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRequestHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblRequestHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblRequestHeader.Location = new System.Drawing.Point(3, 3);
            this.lblRequestHeader.Name = "lblRequestHeader";
            this.lblRequestHeader.Size = new System.Drawing.Size(294, 18);
            this.lblRequestHeader.TabIndex = 113;
            this.lblRequestHeader.Text = "Rate Request";
            this.lblRequestHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // splMain
            // 
            this.splMain.Location = new System.Drawing.Point(300, 76);
            this.splMain.Name = "splMain";
            this.splMain.Size = new System.Drawing.Size(5, 294);
            this.splMain.TabIndex = 38;
            this.splMain.TabStop = false;
            // 
            // mTariffs
            // 
            this.mTariffs.DataSource = typeof(Argix.Finance.DataModule);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(865, 394);
            this.Controls.Add(this.splMain);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Rate Quote";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.csMain.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.pnlMain.ResumeLayout(false);
            this.scRequest.Panel1.ResumeLayout(false);
            this.scRequest.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scRequest)).EndInit();
            this.scRequest.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTariffs)).EndInit();
            this.pnlRequestHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTariffs)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
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
                    this.Font = this.msMain.Font = this.csMain.Font = this.tsMain.Font = this.ssMain.Font = global::Argix.Properties.Settings.Default.Font;
                    this.msViewToolbar.Checked = this.msMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.msViewStatusBar.Checked = this.ssMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
                //this.mToolTip.SetToolTip(this.txtZips,"Enter 5-digit zip code and press Enter.");
				#endregion
				
				//Set control defaults
                ServiceInfo t = FinanceGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.mTariffs.DataSource = FinanceGateway.GetAvailableTariffs();
                this.msViewRequestGrid.Checked = true;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing event
            if (!e.Cancel) {
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
        private void OnTariffAfterSelectChange(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for change in the selected tariff
            setUserServices();
        }
        private void OnRequestTypeChanged(object sender,EventArgs e) {
            //Event handler for change in request type
        }
        private void OnCloseRequest(object sender,System.EventArgs e) {
            //Event handler to close log windows
            this.msViewRequestGrid.PerformClick();
            setUserServices();
        }
        private void OnEnterRequest(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblRequestHeader.BackColor = this.lblCloseRequest.BackColor = SystemColors.ActiveCaption;
                this.lblRequestHeader.ForeColor = this.lblCloseRequest.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnLeaveRequest(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblRequestHeader.BackColor = this.lblCloseRequest.BackColor = SystemColors.Control;
                this.lblRequestHeader.ForeColor = this.lblCloseRequest.ForeColor = SystemColors.ControlText;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Event handler for menu selection
            SaveFileDialog dlgSave=null;
            IRateQuote quote = null;
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "msFileNew":
                    case "tsNew":
                        dlgNewQuote newquote = new dlgNewQuote();
                        newquote.ShowDialog(this);
                        if (newquote.DialogResult == System.Windows.Forms.DialogResult.OK) {
                            DataModule tariff = this.mTariffs.Current != null ? (DataModule)this.mTariffs.Current : null;
                            switch (newquote.QuoteType) {
                                case "LTL":
                                    //winLTLRates win = new winLTLRates(newquote.QuoteType,tariff);
                                    //win.MdiParent = this;
                                    //win.Activated += new EventHandler(OnQuoteActivated);
                                    //win.Deactivate += new EventHandler(OnQuoteDeactivated);
                                    //win.FormClosing += new FormClosingEventHandler(OnQuoteClosing);
                                    //win.FormClosed += new FormClosedEventHandler(OnQuoteClosed);
                                    //quote = (IRateQuote)win;
                                    //quote.StatusMessage += new StatusEventHandler(OnStatusMessage);
                                    //quote.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                                    //win.WindowState = FormWindowState.Maximized;
                                    //win.Show();
                                    //this.mActiveForm = win;
                                    break;
                            }
                        }
                        break;
                    case "msFileOpen":
                    case "tsOpen":
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.DefaultExt = "xml";
                        dlgOpen.Filter = "XML Files (*.xml)|*.xml";
                        dlgOpen.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        dlgOpen.Title = "Choose path and file name";
                        if(dlgOpen.ShowDialog() == DialogResult.OK) {
                            if(!dlgOpen.FileName.EndsWith("xml")) {
                                MessageBox.Show(this,"Make sure it's a valid xml file saved from this program.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                return;
                            }
                        }
                        break;
                    case "msFileSave":
                    case "tsSave":
                        quote = (IRateQuote)this.mActiveForm;
                        quote.Save("");
                        break;
                    case "msFileSaveAs":
                        dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.DefaultExt = "xml";
                        dlgSave.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        dlgSave.Title = "Choose path and file name";
                        dlgSave.Filter = "XML Files (*.xml)|*.xml";
                        dlgSave.AddExtension = true;
                        if(dlgSave.ShowDialog() == DialogResult.OK) {
                            string quoteFile = dlgSave.FileName;
                            quote = (IRateQuote)this.mActiveForm;
                            quote.Save(quoteFile);
                        }
                        break;
                    case "msFileExport":
                    case "tsExport":
                        this.Cursor = Cursors.WaitCursor;
                        dlgSave = new SaveFileDialog();
                        dlgSave.DefaultExt = "xls";
                        dlgSave.AddExtension = true;
                        dlgSave.FileName = "Rates.xls";
                        dlgSave.Filter = "XSL Files (*.xsl)|*.xsl";
                        dlgSave.ValidateNames = true;
                        dlgSave.OverwritePrompt = false;
                        dlgSave.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            string exportFile = dlgSave.FileName;
                            quote = (IRateQuote)this.mActiveForm;
                            quote.Export(exportFile);

                        }
                        break;
                    case "msFileSetup": UltraGridPrinter.PageSettings(); break;
                    case "msFilePrint": 
                        quote = (IRateQuote)this.mActiveForm;
                        quote.Print(true);
                        break;
                    case "tsPrint": 
                        quote = (IRateQuote)this.mActiveForm;
                        quote.Print(false);
                        break;
                    case "msFilePreview": 
                        quote = (IRateQuote)this.mActiveForm;
                        quote.PrintPreview();
                        break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut": case "ctxCut": case "btnCut":
                        break;
                    case "msEditCopy": case "ctxCopy": case "btnCopy":
                        break;
                    case "msEditPaste": case "ctxPaste": case "btnPaste":
                        break;
                    case "csDelete":
                        //TODO
                        break;
                    case "msEditSearch": case "btnSearch": break;
                    case "msViewRefresh": 
                    case "csRefresh": 
                    case "tsRefresh": 
                        this.mActiveForm.Refresh();
                        break;
                    case "msViewRequestGrid":
                        this.scRequest.Panel2Collapsed = !(this.msViewRequestGrid.Checked = !this.msViewRequestGrid.Checked);
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.csMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msToolsTrace":
                        break;
                    case "msToolsConfig": App.ShowConfig(); break;
                    case "msHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem item = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(item.Text)[0]);
            }
            catch(Exception) { }
        }
        #endregion
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu()
        private void configApplication() {
            try {
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Configuration Failure",ex); }
        }
        private void setUserServices() {
            //Set user services
            try {
                IRateQuote quote = this.mActiveForm != null ? (IRateQuote)this.mActiveForm : null;
                this.msFileNew.Enabled = this.tsNew.Enabled = this.grdTariffs.Selected != null;
                this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
                this.msFileSave.Enabled = this.tsSave.Enabled = (quote != null && quote.CanSave);
                this.msFileSaveAs.Enabled = false;
                this.msFileExport.Enabled = this.tsExport.Enabled = (quote != null && quote.CanExport);
                this.msFileSettings.Enabled = true;
                this.msFilePreview.Enabled = (quote != null && quote.CanPreview);
                this.msFilePrint.Enabled = this.tsPrint.Enabled = (quote != null && quote.CanPrint);
                this.msFileExit.Enabled = true;

                this.msViewRefresh.Enabled = true;

                this.msViewFont.Enabled = true;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(FinanceGateway.ServiceState,FinanceGateway.ServiceAddress));
                //this.stbMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                //this.stbMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Error); }
            finally { Application.DoEvents(); }
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
                    //item.Name = "msHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region Quote services: OnQuoteActivated(), OnQuoteDeactivated(), OnQuoteClosing(), OnQuoteClosed(), OnServiceStatesChanged(), OnStatusMessage()
        private void OnQuoteActivated(object sender,System.EventArgs e) {
            //Event handler for activaton of a child window
            try {
                this.mActiveForm = (Form)sender;
                IRateQuote quote = (IRateQuote)this.mActiveForm;
                this.cboRequestType.SelectedItem = quote.RequestType;
                this.grdRequest.SelectedObject = quote.Request;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnQuoteDeactivated(object sender,System.EventArgs e) {
            //Event handler for deactivaton of a child window
            this.mActiveForm = null;
            this.cboRequestType.SelectedItem = null;
            this.grdRequest.SelectedObject = null;
            setUserServices();
        }
        private void OnQuoteClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                this.mActiveForm = null;
                this.cboRequestType.SelectedItem = null;
                this.grdRequest.SelectedObject = null;
                setUserServices();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnQuoteClosed(object sender,FormClosedEventArgs e) {
            //Event handler for closing of a child window
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                this.mActiveForm = null;
                this.cboRequestType.SelectedItem = null;
                this.grdRequest.SelectedObject = null;
                setUserServices();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnServiceStatesChanged(object sender,System.EventArgs e) { setUserServices(); }
        private void OnStatusMessage(object sender,StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        #endregion
    }
}
