using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;

namespace Argix.AgentLineHaul {
    //
    public class frmMain : System.Windows.Forms.Form {
        //Members
        private winSchedule mActiveSchedule = null;
        private UltraGridSvc mGridSvc = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
        private MessageManager mMessageMgr = null;
        private NameValueCollection mHelpItems = null;
        private bool mIsDragging = false;

        private const int KEYSTATE_SHIFT = 5;
        private const int KEYSTATE_CTL = 9;
        #region Controls

        private System.Windows.Forms.ToolBarButton tsEdit;
        private Argix.Windows.ArgixStatusBar ssMain;
        private System.Windows.Forms.Splitter splitterV;
        private System.ComponentModel.IContainer components;
        private Argix.ShipScheduleDataset mSchedules;
        private System.Windows.Forms.Panel pnlTemplates;
        private System.Windows.Forms.Panel pnlTemplateHeader;
        private System.Windows.Forms.Label lblCloseTemplates;
        private System.Windows.Forms.Label lblTemplateHeader;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdTemplates;
        private System.Windows.Forms.Splitter splitterH;
        private Argix.ShipScheduleDataset mTemplates;
        private System.Windows.Forms.Panel pnlNav;
        private System.Windows.Forms.TabControl tabNav;
        private System.Windows.Forms.TabPage tabBrowse;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedules;
        private System.Windows.Forms.TabPage tabSearch;
        private System.Windows.Forms.Label _lblDate;
        private System.Windows.Forms.Label _lblSortCenter;
        private System.Windows.Forms.ComboBox cboSortCenter;
        private System.Windows.Forms.MonthCalendar calDate;
        private System.Windows.Forms.Button cmdSearch;
        private System.Windows.Forms.Label lblNavHeader;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripButton tsSave;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsExport;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsPrint;
        private ToolStripSeparator tsSep3;
        private ToolStripButton tsCut;
        private ToolStripButton tsCopy;
        private ToolStripButton tsPaste;
        private ToolStripButton tsSearch;
        private ToolStripSeparator tsSep4;
        private ToolStripButton tsAdd;
        private ToolStripButton tsCancel;
        private ToolStripSeparator tsSep5;
        private ToolStripButton tsRefresh;
        private ToolStripButton tsFullScreen;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msWin;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewTemplates;
        private ToolStripSeparator msViewSep2;
        private ToolStripMenuItem msViewFullScreen;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msWinCascade;
        private ToolStripMenuItem msWinTileH;
        private ToolStripMenuItem msWinTileV;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileExport;
        private ToolStripMenuItem msFileEmail;
        private ToolStripMenuItem msFileEmailCarriers;
        private ToolStripMenuItem msFileEmailAgents;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileSetup;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePreview;
        private ToolStripSeparator msFileSep4;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msEditCut;
        private ToolStripMenuItem msEditCopy;
        private ToolStripMenuItem msEditPaste;
        private ToolStripSeparator msEditSep1;
        private ToolStripMenuItem msEditSearch;
        private ToolStripSeparator msEditSep2;
        private ToolStripMenuItem msEditAdd;
        private ToolStripMenuItem msEditCancel;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csRefresh;
        private ToolStripSeparator csSep1;
        private ToolStripMenuItem csNew;
        private ToolStripMenuItem csOpen;
        private ToolStripSeparator csSep2;
        private ToolStripMenuItem csExport;
        private ContextMenuStrip csTemplates;
        private ToolStripMenuItem csAddLoads;
        private ToolStripDropDownButton tsEmail;
        private ToolStripMenuItem tsSendCarriers;
        private ToolStripMenuItem tsSendAgents;
        private ToolStripMenuItem msViewFont;
        private ToolStripMenuItem csCancel;
        private MenuStrip msMain;

        #endregion
        //Interface
        public frmMain() {
            try {
                InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
                Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
                #region Window docking
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                this.splitterV.MinExtra = 192;
                this.splitterV.MinSize = 48;
                this.splitterV.Dock = DockStyle.Left;
                this.pnlNav.Dock = DockStyle.Left;
                this.splitterH.MinExtra = 96;
                this.splitterH.MinSize = 48;
                this.splitterH.Dock = DockStyle.Bottom;
                this.pnlTemplates.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.splitterH, this.pnlTemplates, this.splitterV, this.pnlNav, this.tsMain, this.ssMain, this.msMain });
                #endregion

                //Create data and UI services
                this.mGridSvc = new UltraGridSvc(this.grdSchedules);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 3000);
                ShipSchedules.SchedulesChanged += new EventHandler(OnSchedulesChanged);
            }
            catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TemplateViewTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayOfTheWeek");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Selected");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.mSchedules = new Argix.ShipScheduleDataset();
            this.tsEdit = new System.Windows.Forms.ToolBarButton();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.splitterV = new System.Windows.Forms.Splitter();
            this.pnlTemplates = new System.Windows.Forms.Panel();
            this.grdTemplates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csTemplates = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csAddLoads = new System.Windows.Forms.ToolStripMenuItem();
            this.mTemplates = new Argix.ShipScheduleDataset();
            this.pnlTemplateHeader = new System.Windows.Forms.Panel();
            this.lblCloseTemplates = new System.Windows.Forms.Label();
            this.lblTemplateHeader = new System.Windows.Forms.Label();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.pnlNav = new System.Windows.Forms.Panel();
            this.lblNavHeader = new System.Windows.Forms.Label();
            this.tabNav = new System.Windows.Forms.TabControl();
            this.tabBrowse = new System.Windows.Forms.TabPage();
            this.grdSchedules = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csNew = new System.Windows.Forms.ToolStripMenuItem();
            this.csOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.csCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csExport = new System.Windows.Forms.ToolStripMenuItem();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this._lblDate = new System.Windows.Forms.Label();
            this._lblSortCenter = new System.Windows.Forms.Label();
            this.cboSortCenter = new System.Windows.Forms.ComboBox();
            this.calDate = new System.Windows.Forms.MonthCalendar();
            this.cmdSearch = new System.Windows.Forms.Button();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsExport = new System.Windows.Forms.ToolStripButton();
            this.tsEmail = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsSendCarriers = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSendAgents = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCut = new System.Windows.Forms.ToolStripButton();
            this.tsCopy = new System.Windows.Forms.ToolStripButton();
            this.tsPaste = new System.Windows.Forms.ToolStripButton();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.tsCancel = new System.Windows.Forms.ToolStripButton();
            this.tsSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsFullScreen = new System.Windows.Forms.ToolStripButton();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileEmailCarriers = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileEmailAgents = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msEditAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCancel = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewTemplates = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msWin = new System.Windows.Forms.ToolStripMenuItem();
            this.msWinCascade = new System.Windows.Forms.ToolStripMenuItem();
            this.msWinTileH = new System.Windows.Forms.ToolStripMenuItem();
            this.msWinTileV = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.mSchedules)).BeginInit();
            this.pnlTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).BeginInit();
            this.csTemplates.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplates)).BeginInit();
            this.pnlTemplateHeader.SuspendLayout();
            this.pnlNav.SuspendLayout();
            this.tabNav.SuspendLayout();
            this.tabBrowse.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedules)).BeginInit();
            this.csMain.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // mSchedules
            // 
            this.mSchedules.DataSetName = "ShipScheduleDataset";
            this.mSchedules.Locale = new System.Globalization.CultureInfo("en-US");
            this.mSchedules.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsEdit
            // 
            this.tsEdit.Name = "tsEdit";
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 407);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(745, 25);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 1;
            this.ssMain.TerminalText = "Terminal";
            // 
            // splitterV
            // 
            this.splitterV.Location = new System.Drawing.Point(300, 73);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(2, 334);
            this.splitterV.TabIndex = 2;
            this.splitterV.TabStop = false;
            // 
            // pnlTemplates
            // 
            this.pnlTemplates.Controls.Add(this.grdTemplates);
            this.pnlTemplates.Controls.Add(this.pnlTemplateHeader);
            this.pnlTemplates.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTemplates.Location = new System.Drawing.Point(302, 295);
            this.pnlTemplates.Name = "pnlTemplates";
            this.pnlTemplates.Size = new System.Drawing.Size(443, 112);
            this.pnlTemplates.TabIndex = 108;
            // 
            // grdTemplates
            // 
            this.grdTemplates.ContextMenuStrip = this.csTemplates;
            this.grdTemplates.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTemplates.DataMember = "TemplateViewTable";
            this.grdTemplates.DataSource = this.mTemplates;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "TLViewTable";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.Header.Caption = "Sort Center";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 120;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.Caption = "Day";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Width = 48;
            ultraGridColumn5.Header.Caption = "Zone";
            ultraGridColumn5.Header.VisiblePosition = 5;
            ultraGridColumn5.Width = 48;
            ultraGridColumn6.Header.VisiblePosition = 6;
            ultraGridColumn6.Width = 48;
            ultraGridColumn7.Header.Caption = "Agent#";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.Width = 48;
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn9.Header.VisiblePosition = 9;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn10.Header.VisiblePosition = 10;
            ultraGridColumn10.Width = 144;
            ultraGridColumn11.Format = "MM/dd/yy HH:mm";
            ultraGridColumn11.Header.Caption = "Close Date";
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Width = 120;
            ultraGridColumn12.Format = "MM/dd/yy HH:mm";
            ultraGridColumn12.Header.Caption = "Depart Date";
            ultraGridColumn12.Header.VisiblePosition = 12;
            ultraGridColumn12.Width = 120;
            ultraGridColumn13.Header.VisiblePosition = 13;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.Caption = "Stop#";
            ultraGridColumn14.Header.VisiblePosition = 14;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Format = "MM/dd/yy HH:mm";
            ultraGridColumn15.Header.Caption = "Arrival Date";
            ultraGridColumn15.Header.VisiblePosition = 15;
            ultraGridColumn15.Width = 120;
            ultraGridColumn16.Format = "MM/dd/yy HH:mm";
            ultraGridColumn16.Header.Caption = "OFD1 Date";
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Width = 120;
            ultraGridColumn17.Header.VisiblePosition = 17;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.Caption = "Man?";
            ultraGridColumn18.Header.VisiblePosition = 18;
            ultraGridColumn18.Width = 24;
            ultraGridColumn19.Header.Caption = "Act?";
            ultraGridColumn19.Header.VisiblePosition = 19;
            ultraGridColumn19.Width = 24;
            ultraGridColumn20.Header.Caption = "S2 Zone";
            ultraGridColumn20.Header.VisiblePosition = 20;
            ultraGridColumn20.Width = 48;
            ultraGridColumn21.Header.Caption = "S2 Tag";
            ultraGridColumn21.Header.VisiblePosition = 21;
            ultraGridColumn21.Width = 48;
            ultraGridColumn22.Header.Caption = "S2 Agent#";
            ultraGridColumn22.Header.VisiblePosition = 22;
            ultraGridColumn22.Width = 48;
            ultraGridColumn23.Header.VisiblePosition = 23;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn24.Header.VisiblePosition = 24;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.Caption = "S2 Stop#";
            ultraGridColumn25.Header.VisiblePosition = 25;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Format = "MM/dd/yy HH:mm";
            ultraGridColumn26.Header.Caption = "S2 Arrival Date";
            ultraGridColumn26.Header.VisiblePosition = 26;
            ultraGridColumn26.Width = 120;
            ultraGridColumn27.Format = "MM/dd/yy HH:mm";
            ultraGridColumn27.Header.Caption = "S2 OFD1 Date";
            ultraGridColumn27.Header.VisiblePosition = 27;
            ultraGridColumn27.Width = 120;
            ultraGridColumn28.Header.Caption = "S2 Notes";
            ultraGridColumn28.Header.VisiblePosition = 28;
            ultraGridColumn28.Width = 96;
            ultraGridColumn29.Header.VisiblePosition = 29;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.Header.VisiblePosition = 30;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 31;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 32;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 33;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Header.VisiblePosition = 34;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.Header.VisiblePosition = 35;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 36;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 37;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.Caption = "";
            ultraGridColumn38.Header.VisiblePosition = 0;
            ultraGridColumn38.Width = 24;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38});
            this.grdTemplates.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTemplates.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.CaptionAppearance = appearance2;
            this.grdTemplates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTemplates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdTemplates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTemplates.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTemplates.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdTemplates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdTemplates.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTemplates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTemplates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTemplates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTemplates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTemplates.Location = new System.Drawing.Point(0, 22);
            this.grdTemplates.Name = "grdTemplates";
            this.grdTemplates.Size = new System.Drawing.Size(443, 90);
            this.grdTemplates.TabIndex = 119;
            this.grdTemplates.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdTemplates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTemplateSelected);
            this.grdTemplates.SelectionDrag += new System.ComponentModel.CancelEventHandler(this.OnSelectionDrag);
            this.grdTemplates.QueryContinueDrag += new System.Windows.Forms.QueryContinueDragEventHandler(this.OnQueryContinueDrag);
            this.grdTemplates.Enter += new System.EventHandler(this.OnEnterTemplates);
            this.grdTemplates.Leave += new System.EventHandler(this.OnLeaveTemplates);
            this.grdTemplates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseDown);
            this.grdTemplates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            this.grdTemplates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            // 
            // csTemplates
            // 
            this.csTemplates.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csAddLoads});
            this.csTemplates.Name = "csTemplates";
            this.csTemplates.Size = new System.Drawing.Size(131, 26);
            // 
            // csAddLoads
            // 
            this.csAddLoads.Image = global::Argix.Properties.Resources.AddTable;
            this.csAddLoads.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csAddLoads.Name = "csAddLoads";
            this.csAddLoads.Size = new System.Drawing.Size(130, 22);
            this.csAddLoads.Text = "Add Loads";
            this.csAddLoads.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mTemplates
            // 
            this.mTemplates.DataSetName = "TemplateDS";
            this.mTemplates.Locale = new System.Globalization.CultureInfo("en-US");
            this.mTemplates.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // pnlTemplateHeader
            // 
            this.pnlTemplateHeader.Controls.Add(this.lblCloseTemplates);
            this.pnlTemplateHeader.Controls.Add(this.lblTemplateHeader);
            this.pnlTemplateHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTemplateHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlTemplateHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTemplateHeader.Name = "pnlTemplateHeader";
            this.pnlTemplateHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlTemplateHeader.Size = new System.Drawing.Size(443, 22);
            this.pnlTemplateHeader.TabIndex = 118;
            this.pnlTemplateHeader.Enter += new System.EventHandler(this.OnEnterTemplates);
            this.pnlTemplateHeader.Leave += new System.EventHandler(this.OnLeaveTemplates);
            // 
            // lblCloseTemplates
            // 
            this.lblCloseTemplates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseTemplates.BackColor = System.Drawing.SystemColors.Control;
            this.lblCloseTemplates.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloseTemplates.Location = new System.Drawing.Point(426, 4);
            this.lblCloseTemplates.Name = "lblCloseTemplates";
            this.lblCloseTemplates.Size = new System.Drawing.Size(13, 15);
            this.lblCloseTemplates.TabIndex = 115;
            this.lblCloseTemplates.Text = "X";
            this.lblCloseTemplates.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseTemplates.Click += new System.EventHandler(this.OnCloseTemplates);
            this.lblCloseTemplates.Enter += new System.EventHandler(this.OnEnterTemplates);
            this.lblCloseTemplates.Leave += new System.EventHandler(this.OnLeaveTemplates);
            // 
            // lblTemplateHeader
            // 
            this.lblTemplateHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblTemplateHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTemplateHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTemplateHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTemplateHeader.Location = new System.Drawing.Point(3, 3);
            this.lblTemplateHeader.Name = "lblTemplateHeader";
            this.lblTemplateHeader.Size = new System.Drawing.Size(437, 16);
            this.lblTemplateHeader.TabIndex = 113;
            this.lblTemplateHeader.Text = "Template Loads";
            this.lblTemplateHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTemplateHeader.Enter += new System.EventHandler(this.OnEnterTemplates);
            this.lblTemplateHeader.Leave += new System.EventHandler(this.OnLeaveTemplates);
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(302, 293);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(443, 2);
            this.splitterH.TabIndex = 109;
            this.splitterH.TabStop = false;
            // 
            // pnlNav
            // 
            this.pnlNav.Controls.Add(this.lblNavHeader);
            this.pnlNav.Controls.Add(this.tabNav);
            this.pnlNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlNav.Location = new System.Drawing.Point(0, 73);
            this.pnlNav.Name = "pnlNav";
            this.pnlNav.Padding = new System.Windows.Forms.Padding(3);
            this.pnlNav.Size = new System.Drawing.Size(300, 334);
            this.pnlNav.TabIndex = 113;
            // 
            // lblNavHeader
            // 
            this.lblNavHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblNavHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNavHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblNavHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNavHeader.Location = new System.Drawing.Point(3, 3);
            this.lblNavHeader.Name = "lblNavHeader";
            this.lblNavHeader.Size = new System.Drawing.Size(294, 20);
            this.lblNavHeader.TabIndex = 114;
            this.lblNavHeader.Text = "Ship Schedules";
            this.lblNavHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabNav
            // 
            this.tabNav.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabNav.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabNav.Controls.Add(this.tabBrowse);
            this.tabNav.Controls.Add(this.tabSearch);
            this.tabNav.Location = new System.Drawing.Point(2, 22);
            this.tabNav.Name = "tabNav";
            this.tabNav.Padding = new System.Drawing.Point(0, 0);
            this.tabNav.SelectedIndex = 0;
            this.tabNav.ShowToolTips = true;
            this.tabNav.Size = new System.Drawing.Size(298, 308);
            this.tabNav.TabIndex = 112;
            this.tabNav.Enter += new System.EventHandler(this.OnEnterNav);
            this.tabNav.Leave += new System.EventHandler(this.OnLeaveNav);
            // 
            // tabBrowse
            // 
            this.tabBrowse.Controls.Add(this.grdSchedules);
            this.tabBrowse.Location = new System.Drawing.Point(4, 4);
            this.tabBrowse.Name = "tabBrowse";
            this.tabBrowse.Size = new System.Drawing.Size(290, 282);
            this.tabBrowse.TabIndex = 0;
            this.tabBrowse.Text = "Active";
            this.tabBrowse.ToolTipText = "Browse active ship schedules";
            this.tabBrowse.Enter += new System.EventHandler(this.OnEnterNav);
            this.tabBrowse.Leave += new System.EventHandler(this.OnLeaveNav);
            // 
            // grdSchedules
            // 
            this.grdSchedules.ContextMenuStrip = this.csMain;
            this.grdSchedules.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSchedules.DataMember = "ShipScheduleTable";
            this.grdSchedules.DataSource = this.mSchedules;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdSchedules.DisplayLayout.Appearance = appearance5;
            ultraGridColumn39.Header.VisiblePosition = 0;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn39.Width = 85;
            ultraGridColumn40.Header.VisiblePosition = 1;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.Caption = "Sort Center";
            ultraGridColumn41.Header.VisiblePosition = 2;
            ultraGridColumn41.Width = 132;
            ultraGridColumn42.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn42.Format = "MM/dd/yyyy";
            ultraGridColumn42.Header.Caption = "Date";
            ultraGridColumn42.Header.VisiblePosition = 3;
            ultraGridColumn42.Width = 75;
            ultraGridColumn43.Header.VisiblePosition = 4;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.VisiblePosition = 5;
            ultraGridColumn44.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44});
            this.grdSchedules.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdSchedules.DisplayLayout.CaptionAppearance = appearance6;
            this.grdSchedules.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSchedules.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedules.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.TextHAlignAsString = "Left";
            this.grdSchedules.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdSchedules.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSchedules.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdSchedules.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdSchedules.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdSchedules.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSchedules.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSchedules.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSchedules.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSchedules.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSchedules.Location = new System.Drawing.Point(0, 0);
            this.grdSchedules.Name = "grdSchedules";
            this.grdSchedules.Size = new System.Drawing.Size(290, 282);
            this.grdSchedules.TabIndex = 0;
            this.grdSchedules.UseAppStyling = false;
            this.grdSchedules.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedules.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdSchedules.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdSchedules.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdSchedules.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnGridAfterRowFilterChanged);
            this.grdSchedules.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdSchedules.DoubleClick += new System.EventHandler(this.OnGridDoubleClicked);
            this.grdSchedules.Enter += new System.EventHandler(this.OnEnterNav);
            this.grdSchedules.Leave += new System.EventHandler(this.OnLeaveNav);
            this.grdSchedules.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csRefresh,
            this.csSep1,
            this.csNew,
            this.csOpen,
            this.csCancel,
            this.csSep2,
            this.csExport});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(162, 126);
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(161, 22);
            this.csRefresh.Text = "Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(158, 6);
            // 
            // csNew
            // 
            this.csNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.csNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csNew.Name = "csNew";
            this.csNew.Size = new System.Drawing.Size(161, 22);
            this.csNew.Text = "New Schedule";
            this.csNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csOpen
            // 
            this.csOpen.Image = global::Argix.Properties.Resources.Open;
            this.csOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csOpen.Name = "csOpen";
            this.csOpen.Size = new System.Drawing.Size(161, 22);
            this.csOpen.Text = "Open Schedule";
            this.csOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCancel
            // 
            this.csCancel.Name = "csCancel";
            this.csCancel.Size = new System.Drawing.Size(161, 22);
            this.csCancel.Text = "Cancel Schedule";
            this.csCancel.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep2
            // 
            this.csSep2.Name = "csSep2";
            this.csSep2.Size = new System.Drawing.Size(158, 6);
            // 
            // csExport
            // 
            this.csExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.csExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csExport.Name = "csExport";
            this.csExport.Size = new System.Drawing.Size(161, 22);
            this.csExport.Text = "Export";
            this.csExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this._lblDate);
            this.tabSearch.Controls.Add(this._lblSortCenter);
            this.tabSearch.Controls.Add(this.cboSortCenter);
            this.tabSearch.Controls.Add(this.calDate);
            this.tabSearch.Controls.Add(this.cmdSearch);
            this.tabSearch.Location = new System.Drawing.Point(4, 4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(290, 282);
            this.tabSearch.TabIndex = 1;
            this.tabSearch.Text = "Archive";
            this.tabSearch.ToolTipText = "Search prior ship schedules";
            this.tabSearch.Visible = false;
            this.tabSearch.Enter += new System.EventHandler(this.OnEnterNav);
            this.tabSearch.Leave += new System.EventHandler(this.OnLeaveNav);
            // 
            // _lblDate
            // 
            this._lblDate.Location = new System.Drawing.Point(5, 55);
            this._lblDate.Name = "_lblDate";
            this._lblDate.Size = new System.Drawing.Size(227, 20);
            this._lblDate.TabIndex = 10;
            this._lblDate.Text = "Ship Date";
            this._lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblSortCenter
            // 
            this._lblSortCenter.Location = new System.Drawing.Point(5, 5);
            this._lblSortCenter.Name = "_lblSortCenter";
            this._lblSortCenter.Size = new System.Drawing.Size(227, 20);
            this._lblSortCenter.TabIndex = 9;
            this._lblSortCenter.Text = "Sort Center";
            this._lblSortCenter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboSortCenter
            // 
            this.cboSortCenter.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboSortCenter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSortCenter.Location = new System.Drawing.Point(5, 28);
            this.cboSortCenter.Name = "cboSortCenter";
            this.cboSortCenter.Size = new System.Drawing.Size(274, 21);
            this.cboSortCenter.TabIndex = 8;
            this.cboSortCenter.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            this.cboSortCenter.Enter += new System.EventHandler(this.OnEnterNav);
            this.cboSortCenter.Leave += new System.EventHandler(this.OnLeaveNav);
            // 
            // calDate
            // 
            this.calDate.Location = new System.Drawing.Point(5, 78);
            this.calDate.MaxSelectionCount = 1;
            this.calDate.Name = "calDate";
            this.calDate.ShowTodayCircle = false;
            this.calDate.TabIndex = 7;
            this.calDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.OnDateSelected);
            this.calDate.Enter += new System.EventHandler(this.OnEnterNav);
            this.calDate.Leave += new System.EventHandler(this.OnLeaveNav);
            // 
            // cmdSearch
            // 
            this.cmdSearch.AutoSize = true;
            this.cmdSearch.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.cmdSearch.Location = new System.Drawing.Point(5, 245);
            this.cmdSearch.Name = "cmdSearch";
            this.cmdSearch.Size = new System.Drawing.Size(80, 23);
            this.cmdSearch.TabIndex = 6;
            this.cmdSearch.Text = "&Search";
            this.cmdSearch.Click += new System.EventHandler(this.OnSearchClick);
            this.cmdSearch.Enter += new System.EventHandler(this.OnEnterNav);
            this.cmdSearch.Leave += new System.EventHandler(this.OnLeaveNav);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsOpen,
            this.tsSave,
            this.tsSep1,
            this.tsExport,
            this.tsEmail,
            this.tsSep2,
            this.tsPrint,
            this.tsSep3,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsSearch,
            this.tsSep4,
            this.tsAdd,
            this.tsCancel,
            this.tsSep5,
            this.tsRefresh,
            this.tsFullScreen});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(745, 49);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 115;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 46);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New...";
            this.tsNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 46);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open...";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 46);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save";
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 49);
            // 
            // tsExport
            // 
            this.tsExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.tsExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExport.Name = "tsExport";
            this.tsExport.Size = new System.Drawing.Size(44, 46);
            this.tsExport.Text = "Export";
            this.tsExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsExport.ToolTipText = "Export...";
            this.tsExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsEmail
            // 
            this.tsEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsSendCarriers,
            this.tsSendAgents});
            this.tsEmail.Image = global::Argix.Properties.Resources.Send;
            this.tsEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEmail.Name = "tsEmail";
            this.tsEmail.Size = new System.Drawing.Size(49, 46);
            this.tsEmail.Text = "Email";
            this.tsEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsEmail.ToolTipText = "Email ship schedule";
            // 
            // tsSendCarriers
            // 
            this.tsSendCarriers.Name = "tsSendCarriers";
            this.tsSendCarriers.Size = new System.Drawing.Size(114, 22);
            this.tsSendCarriers.Text = "Carriers";
            this.tsSendCarriers.ToolTipText = "Email ship schedule to carriers";
            this.tsSendCarriers.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSendAgents
            // 
            this.tsSendAgents.Name = "tsSendAgents";
            this.tsSendAgents.Size = new System.Drawing.Size(114, 22);
            this.tsSendAgents.Text = "Agents";
            this.tsSendAgents.ToolTipText = "Email ship schedule to agents";
            this.tsSendAgents.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 49);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 46);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print ship schedule...";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 49);
            // 
            // tsCut
            // 
            this.tsCut.Image = global::Argix.Properties.Resources.Cut;
            this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCut.Name = "tsCut";
            this.tsCut.Size = new System.Drawing.Size(36, 46);
            this.tsCut.Text = "Cut";
            this.tsCut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCut.ToolTipText = "Cut text";
            this.tsCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsCopy
            // 
            this.tsCopy.Image = global::Argix.Properties.Resources.Copy;
            this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.Size = new System.Drawing.Size(39, 46);
            this.tsCopy.Text = "Copy";
            this.tsCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCopy.ToolTipText = "Copy text";
            this.tsCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPaste
            // 
            this.tsPaste.Image = global::Argix.Properties.Resources.Paste;
            this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(39, 46);
            this.tsPaste.Text = "Paste";
            this.tsPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPaste.ToolTipText = "Paste text";
            this.tsPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Find;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(46, 46);
            this.tsSearch.Text = "Search";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 49);
            // 
            // tsAdd
            // 
            this.tsAdd.Image = global::Argix.Properties.Resources.AddTable;
            this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.Size = new System.Drawing.Size(62, 46);
            this.tsAdd.Text = "Add Load";
            this.tsAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsAdd.ToolTipText = "Add a new load...";
            this.tsAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsCancel
            // 
            this.tsCancel.Image = global::Argix.Properties.Resources.Delete;
            this.tsCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(76, 46);
            this.tsCancel.Text = "Cancel Load";
            this.tsCancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCancel.ToolTipText = "Cancel selected load";
            this.tsCancel.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep5
            // 
            this.tsSep5.Name = "tsSep5";
            this.tsSep5.Size = new System.Drawing.Size(6, 49);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 46);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh ship schedule";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsFullScreen
            // 
            this.tsFullScreen.Image = global::Argix.Properties.Resources.FullScreen;
            this.tsFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsFullScreen.Name = "tsFullScreen";
            this.tsFullScreen.Size = new System.Drawing.Size(67, 46);
            this.tsFullScreen.Text = "Full screen";
            this.tsFullScreen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsFullScreen.ToolTipText = "Full screen view";
            this.tsFullScreen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msEdit,
            this.msView,
            this.msTools,
            this.msWin,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.MdiWindowListItem = this.msWin;
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(745, 24);
            this.msMain.TabIndex = 117;
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFileSep1,
            this.msFileSaveAs,
            this.msFileSep2,
            this.msFileExport,
            this.msFileEmail,
            this.msFileSep3,
            this.msFileSetup,
            this.msFilePrint,
            this.msFilePreview,
            this.msFileSep4,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37, 20);
            this.msFile.Text = "File";
            // 
            // msFileNew
            // 
            this.msFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.msFileNew.Size = new System.Drawing.Size(152, 22);
            this.msFileNew.Text = "&New...";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(152, 22);
            this.msFileOpen.Text = "&Open...";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.msFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.msFileSaveAs.Text = "Save &As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.msFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExport.Name = "msFileExport";
            this.msFileExport.Size = new System.Drawing.Size(152, 22);
            this.msFileExport.Text = "&Export...";
            this.msFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileEmail
            // 
            this.msFileEmail.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileEmailCarriers,
            this.msFileEmailAgents});
            this.msFileEmail.Image = global::Argix.Properties.Resources.Send;
            this.msFileEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileEmail.Name = "msFileEmail";
            this.msFileEmail.Size = new System.Drawing.Size(152, 22);
            this.msFileEmail.Text = "E&mail";
            // 
            // msFileEmailCarriers
            // 
            this.msFileEmailCarriers.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileEmailCarriers.Name = "msFileEmailCarriers";
            this.msFileEmailCarriers.Size = new System.Drawing.Size(123, 22);
            this.msFileEmailCarriers.Text = "Carriers...";
            this.msFileEmailCarriers.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileEmailAgents
            // 
            this.msFileEmailAgents.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileEmailAgents.Name = "msFileEmailAgents";
            this.msFileEmailAgents.Size = new System.Drawing.Size(123, 22);
            this.msFileEmailAgents.Text = "Agents...";
            this.msFileEmailAgents.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileSetup
            // 
            this.msFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.msFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSetup.Name = "msFileSetup";
            this.msFileSetup.Size = new System.Drawing.Size(152, 22);
            this.msFileSetup.Text = "Page Set&up...";
            this.msFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(152, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(152, 22);
            this.msFilePreview.Text = "Print Pre&view...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep4
            // 
            this.msFileSep4.Name = "msFileSep4";
            this.msFileSep4.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(152, 22);
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEdit
            // 
            this.msEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msEditCut,
            this.msEditCopy,
            this.msEditPaste,
            this.msEditSep1,
            this.msEditSearch,
            this.msEditSep2,
            this.msEditAdd,
            this.msEditCancel});
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 20);
            this.msEdit.Text = "Edit";
            // 
            // msEditCut
            // 
            this.msEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.msEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCut.Name = "msEditCut";
            this.msEditCut.Size = new System.Drawing.Size(118, 22);
            this.msEditCut.Text = "Cu&t";
            this.msEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditCopy
            // 
            this.msEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.msEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCopy.Name = "msEditCopy";
            this.msEditCopy.Size = new System.Drawing.Size(118, 22);
            this.msEditCopy.Text = "&Copy";
            this.msEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditPaste
            // 
            this.msEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.msEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditPaste.Name = "msEditPaste";
            this.msEditPaste.Size = new System.Drawing.Size(118, 22);
            this.msEditPaste.Text = "&Paste";
            this.msEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditSep1
            // 
            this.msEditSep1.Name = "msEditSep1";
            this.msEditSep1.Size = new System.Drawing.Size(115, 6);
            // 
            // msEditSearch
            // 
            this.msEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.msEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditSearch.Name = "msEditSearch";
            this.msEditSearch.Size = new System.Drawing.Size(118, 22);
            this.msEditSearch.Text = "&Search...";
            this.msEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditSep2
            // 
            this.msEditSep2.Name = "msEditSep2";
            this.msEditSep2.Size = new System.Drawing.Size(115, 6);
            // 
            // msEditAdd
            // 
            this.msEditAdd.Image = global::Argix.Properties.Resources.AddTable;
            this.msEditAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditAdd.Name = "msEditAdd";
            this.msEditAdd.Size = new System.Drawing.Size(118, 22);
            this.msEditAdd.Text = "&Add...";
            this.msEditAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditCancel
            // 
            this.msEditCancel.Image = global::Argix.Properties.Resources.Delete;
            this.msEditCancel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCancel.Name = "msEditCancel";
            this.msEditCancel.Size = new System.Drawing.Size(118, 22);
            this.msEditCancel.Text = "Ca&ncel";
            this.msEditCancel.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewFont,
            this.msViewTemplates,
            this.msViewSep2,
            this.msViewFullScreen,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 20);
            this.msView.Text = "View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.msViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(131, 22);
            this.msViewRefresh.Text = "&Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(128, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(131, 22);
            this.msViewFont.Text = "&Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewTemplates
            // 
            this.msViewTemplates.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewTemplates.Name = "msViewTemplates";
            this.msViewTemplates.Size = new System.Drawing.Size(131, 22);
            this.msViewTemplates.Text = "&Templates";
            this.msViewTemplates.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(128, 6);
            // 
            // msViewFullScreen
            // 
            this.msViewFullScreen.Image = global::Argix.Properties.Resources.FullScreen;
            this.msViewFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewFullScreen.Name = "msViewFullScreen";
            this.msViewFullScreen.Size = new System.Drawing.Size(131, 22);
            this.msViewFullScreen.Text = "Full &Screen";
            this.msViewFullScreen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(131, 22);
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(131, 22);
            this.msViewStatusBar.Text = "Status Bar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msTools
            // 
            this.msTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msToolsConfig});
            this.msTools.Name = "msTools";
            this.msTools.Size = new System.Drawing.Size(48, 20);
            this.msTools.Text = "Tools";
            // 
            // msToolsConfig
            // 
            this.msToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msToolsConfig.Name = "msToolsConfig";
            this.msToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.msToolsConfig.Text = "&Configuration...";
            this.msToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msWin
            // 
            this.msWin.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msWinCascade,
            this.msWinTileH,
            this.msWinTileV});
            this.msWin.Name = "msWin";
            this.msWin.Size = new System.Drawing.Size(63, 20);
            this.msWin.Text = "Window";
            // 
            // msWinCascade
            // 
            this.msWinCascade.Image = global::Argix.Properties.Resources.CascadeWindows;
            this.msWinCascade.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msWinCascade.Name = "msWinCascade";
            this.msWinCascade.Size = new System.Drawing.Size(160, 22);
            this.msWinCascade.Text = "Cascade";
            this.msWinCascade.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msWinTileH
            // 
            this.msWinTileH.Image = global::Argix.Properties.Resources.ArrangeWindows;
            this.msWinTileH.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msWinTileH.Name = "msWinTileH";
            this.msWinTileH.Size = new System.Drawing.Size(160, 22);
            this.msWinTileH.Text = "Tile Horizontally";
            this.msWinTileH.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msWinTileV
            // 
            this.msWinTileV.Image = global::Argix.Properties.Resources.ArrangeSideBySide;
            this.msWinTileV.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msWinTileV.Name = "msWinTileV";
            this.msWinTileV.Size = new System.Drawing.Size(160, 22);
            this.msWinTileV.Text = "Tile Vertically";
            this.msWinTileV.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 20);
            this.msHelp.Text = "Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(193, 22);
            this.msHelpAbout.Text = "&About Ship Schedule...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(190, 6);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(745, 432);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.pnlTemplates);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.pnlNav);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Text = "Argix Logistics Ship Schedule";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.mSchedules)).EndInit();
            this.pnlTemplates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).EndInit();
            this.csTemplates.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTemplates)).EndInit();
            this.pnlTemplateHeader.ResumeLayout(false);
            this.pnlNav.ResumeLayout(false);
            this.tabNav.ResumeLayout(false);
            this.tabBrowse.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedules)).EndInit();
            this.csMain.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion
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
                    this.msViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.msViewStatusBar.Checked = this.ssMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    this.msViewTemplates.Checked = this.pnlTemplates.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.TemplatesWindow);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                #endregion
                #region Set tooltips
                this.mToolTip.InitialDelay = 500;
                this.mToolTip.AutoPopDelay = 3000;
                this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;		//Even when form is inactve
                //this.mToolTip.SetToolTip(this.cboTerminals, "Select an enterprise terminal for the TL and Agent Summary views.");
                #endregion

                //Set control defaults
                #region Grid customizations from normal layout (to support cell editing)
                this.grdSchedules.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdSchedules.DisplayLayout.Bands[0].Columns["ScheduleDate"].SortIndicator = SortIndicator.Ascending;
                this.grdTemplates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdTemplates.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
                #endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ColumnHeaders;
                ServiceInfo t = ShipScheduleGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(), t.Description);
                this.ssMain.User1Panel.Width = 144;

                if(!RoleServiceGateway.IsLineHaulAdministrator) this.grdTemplates.DisplayLayout.Bands[0].Override.SelectTypeRow = SelectType.None;
                this.grdSchedules.DataSource = ShipSchedules.Schedules;
                this.cboSortCenter.DisplayMember = "TerminalTable.Description";
                this.cboSortCenter.ValueMember = "TerminalTable.ID";
                this.cboSortCenter.DataSource = ShipScheduleGateway.GetSortCenters();
                if(this.cboSortCenter.Items.Count > 0) this.cboSortCenter.SelectedIndex = 0;
                this.calDate.MinDate = DateTime.Today.AddYears(-2);
                this.calDate.MaxDate = DateTime.Today;
                this.cmdSearch.Enabled = false;
                ShipSchedules.RefreshSchedules();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            //Ask only if there are detail forms open
            if(this.MdiChildren.Length > 0) {
                if(MessageBox.Show("Are you sure you want to close the application.?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    e.Cancel = true;
            }
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.TemplatesWindow = this.pnlTemplates.Visible;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.ColumnHeaders = this.ColumnHeaders;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnFormResize(object sender, System.EventArgs e) {
            //Event handler for form resized event
        }
        private void OnSchedulesChanged(object sender, EventArgs e) {
            //Event handler for change in ship schedule list
            try {
                this.mMessageMgr.AddMessage("Loading ship schedules...");
                OnScheduleActivated(this.mActiveSchedule, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region Navigation Services: OnValidateForm(), OnDateSelected(), OnSearchClick(), OnEnterNav(), OnLeaveNav()
        private void OnValidateForm(object sender, System.EventArgs e) {
            //Event handler for changes in form data- validate OK service
            try { this.cmdSearch.Enabled = (this.cboSortCenter.SelectedIndex >= 0); }
            catch(Exception) { }
        }
        private void OnDateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
            //Event handler for date selected
            OnValidateForm(null, null);
        }
        private void OnSearchClick(object sender, System.EventArgs e) {
            //Event handler for seacrh button clicked
            this.Cursor = Cursors.WaitCursor;
            winSchedule win = null;
            try {
                //Search for a schedule; validate it is not open already
                long sortCenterID = Convert.ToInt64(this.cboSortCenter.SelectedValue);
                string sortCenter = this.cboSortCenter.Text;
                DateTime scheduleDate = calDate.SelectionStart;
                for(int i = 0; i < this.MdiChildren.Length; i++) {
                    win = (winSchedule)this.MdiChildren[i];
                    if(win.Schedule.SortCenterID == sortCenterID && win.Schedule.ScheduleDate == scheduleDate) {
                        //Already open; bring to forefront
                        win.Activate();
                        if(win.WindowState == FormWindowState.Minimized) win.WindowState = FormWindowState.Normal;
                        break;
                    }
                    else
                        win = null;
                }
                if(win == null) {
                    //Not open; open and register for events
                    ShipSchedule schedule = ShipSchedules.SchedulesArchiveItem(sortCenterID, sortCenter, scheduleDate);
                    if(schedule == null) {
                        MessageBox.Show(this, "A schedule for this date does not exist.", App.Title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else {
                        this.mMessageMgr.AddMessage("Opening " + schedule.SortCenter + " ship schedule for " + schedule.ScheduleDate + "...");
                        win = new winSchedule(schedule);
                        win.WindowState = (this.MdiChildren.Length > 0) ? this.MdiChildren[0].WindowState : FormWindowState.Maximized;
                        win.MdiParent = this;
                        win.Activated += new EventHandler(OnScheduleActivated);
                        win.Deactivate += new EventHandler(OnScheduleDeactivated);
                        win.Closing += new CancelEventHandler(OnScheduleClosing);
                        win.Closed += new EventHandler(OnScheduleClosed);
                        win.StatusMessage += new StatusEventHandler(OnStatusMessage);
                        win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                        win.Show();
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnEnterNav(object sender, System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblNavHeader.BackColor = SystemColors.ActiveCaption;
                this.lblNavHeader.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveNav(object sender, System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblNavHeader.BackColor = SystemColors.Control;
                this.lblNavHeader.ForeColor = SystemColors.ControlText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
        #region Nav Grid Initialization: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridAfterRowFilterChanged()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //Event handler for grid layout initialization
            try {
                e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "WeekDay");
                e.Layout.Bands[0].Columns["WeekDay"].DataType = typeof(string);
                e.Layout.Bands[0].Columns["WeekDay"].Width = 48;
                e.Layout.Bands[0].Columns["WeekDay"].Header.Caption = "Day";
                e.Layout.Bands[0].Columns["WeekDay"].Header.Appearance.TextHAlign = HAlign.Left;
                e.Layout.Bands[0].Columns["WeekDay"].CellAppearance.TextHAlign = HAlign.Left;
                e.Layout.Bands[0].Columns["WeekDay"].SortIndicator = SortIndicator.None;
            }
            catch(ArgumentException ex) { App.ReportError(ex, false, LogLevel.None); }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //Event handler for grid layout initialization
            try {
                DateTime schDate = Convert.ToDateTime(e.Row.Cells["ScheduleDate"].Value.ToString());
                e.Row.Cells["WeekDay"].Value = schDate.DayOfWeek.ToString().Substring(0, 3);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridAfterRowFilterChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs e) {
            //	
            try {
                OnScheduleActivated(this.mActiveSchedule, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Nav Grid Support: GridSelectionChanged(), GridMouseDown(), GridDoubleClick()
        private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            try { }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridDoubleClicked(object sender, System.EventArgs e) {
            //Event handler for double-click event
            try {
                //Select grid and forward to update
                UltraGrid grid = (UltraGrid)sender;
                if(grid.ActiveRow != null && grid.Selected.Rows.Count > 0) {
                    if(this.msFileOpen.Enabled) this.msFileOpen.PerformClick();
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Template Grid Drag/Drop Services: OnDragDropMouseDown(), OnDragDropMouseMove(), OnDragDropMouseUp(), OnQueryContinueDrag(), OnSelectionDrag()
        private void OnDragDropMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Select rows on right click
                UltraGrid oGrid = (UltraGrid)sender;
                UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            this.mIsDragging = true;
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) oGrid.Selected.Rows.Clear();
                            oRow.Selected = true;
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            oGrid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDragDropMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Start drag\drop if user is dragging
            DataObject oData = null;
            try {
                switch(e.Button) {
                    case MouseButtons.Left:
                        UltraGrid oGrid = (UltraGrid)sender;
                        if(this.mIsDragging) {
                            //Initiate drag drop operation from the grid source
                            if(oGrid.Focused && oGrid.Selected.Rows.Count > 0) {
                                oData = new DataObject();
                                oData.SetData("");
                                DragDropEffects effect = oGrid.DoDragDrop(oData, DragDropEffects.All);
                                this.mIsDragging = false;

                                //After the drop- handled by drop code
                                switch(effect) {
                                    case DragDropEffects.Move: break;
                                    case DragDropEffects.Copy: break;
                                }
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnDragDropMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) {
            this.mIsDragging = false;
        }
        private void OnQueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e) {
            //if(!this.mIsDragging) e.Action = DragAction.Cancel; 
        }
        private void OnSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) {
            //e.Cancel = !this.mIsDragging; 
        }
        #endregion
        #region User Services: OnItemClick(), OnHelpItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
            //Event handler for menu selection
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "msFileNew":
                    case "csNew":
                    case "tsNew":
                        newSchedule();
                        break;
                    case "msFileOpen":
                    case "csOpen":
                    case "tsOpen":
                        openSchedule();
                        break;
                    case "msFileSave":
                    case "tsSave":
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Schedule As...";
                        dlgSave.FileName = this.mActiveSchedule.Schedule.ScheduleID;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
                            Application.DoEvents();
                            if(dlgSave.FileName.EndsWith("xls")) {
                                new Argix.ExcelFormat().Transform(this.mActiveSchedule.Schedule.Trips, "ShipScheduleViewTable", dlgSave.FileName);
                            }
                            else {
                                this.mActiveSchedule.Schedule.Trips.WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema);
                            }
                        }
                        break;
                    case "msFileExport":
                    case "csExport":
                    case "tsExport":
                        this.Cursor = Cursors.WaitCursor;
                        DataSet ds = new DataSet();
                        string xfile = global::Argix.Properties.Settings.Default.ExportDefinitionFile + "ExportDataset.xsd";
                        this.mMessageMgr.AddMessage("Reading export definition from " + xfile);
                        if(this.grdSchedules.Focused) {
                            ds.ReadXml(xfile, XmlReadMode.Auto);
                            this.mMessageMgr.AddMessage("Exporting selected schedules to Microsoft Excel...");
                            for(int i = 0; i < this.grdSchedules.Selected.Rows.Count; i++) {
                                string scheduleID = this.grdSchedules.Selected.Rows[i].Cells["ScheduleID"].Value.ToString();
                                ShipSchedule schedule = ShipSchedules.SchedulesItem(scheduleID);
                                ds.Merge(schedule.Trips, true, MissingSchemaAction.Ignore);
                            }
                            new Argix.ExcelFormat().Transform(ds);
                        }
                        else if(this.mActiveSchedule != null) {
                            ds.ReadXml(xfile, XmlReadMode.Auto);
                            this.mMessageMgr.AddMessage("Exporting active schedule to Microsoft Excel...");
                            ds.Merge(this.mActiveSchedule.Schedule.Trips, true, MissingSchemaAction.Ignore);
                            new Argix.ExcelFormat().Transform(ds);
                        }
                        break;
                    case "msFileEmailCarriers": this.mActiveSchedule.EmailCarriers(true); break;
                    case "tsSendCarriers": this.mActiveSchedule.EmailCarriers(false); break;
                    case "msFileEmailAgents": this.mActiveSchedule.EmailAgents(true); break;
                    case "tsSendAgents": this.mActiveSchedule.EmailAgents(false); break;
                    case "msFileSetup": UltraGridPrinter.PageSettings(); break;
                    case "msFilePrint": this.mActiveSchedule.Print(true); break;
                    case "tsPrint": this.mActiveSchedule.Print(false); break;
                    case "msFilePreview":
                    case "tsPreview": this.mActiveSchedule.PrintPreview(); break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "tsCut": this.mActiveSchedule.Cut(); break;
                    case "msEditCopy":
                    case "tsCopy": this.mActiveSchedule.Copy(); break;
                    case "msEditPaste":
                    case "tsPaste": this.mActiveSchedule.Paste(); break;
                    case "msEditAdd":
                    case "csAddLoads":
                    case "tsAdd": this.mActiveSchedule.AddLoads(); break;
                    case "msEditCancel":
                    case "tsCancel": this.mActiveSchedule.CancelLoad(); break;
                    case "msEditSearch":
                    case "tsSearch": this.tabNav.SelectedIndex = 1; this.cboSortCenter.Focus(); break;
                    case "msViewRefresh":
                    case "csRefresh":
                    case "tsRefresh":
                        //Refresh schedule
                        if(this.grdSchedules.Focused) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Refreshing schedule list...");
                            ShipSchedules.RefreshSchedules();
                        }
                        else if(this.mActiveSchedule != null) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Refreshing current schedule...");
                            this.mActiveSchedule.Refresh();
                        }
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if(fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewFullScreen":
                    case "tsFullScreen": this.pnlNav.Visible = this.splitterV.Visible = !(this.tsFullScreen.Checked = this.msViewFullScreen.Checked = (!this.msViewFullScreen.Checked)); break;
                    case "msViewTemplates": this.pnlTemplates.Visible = (this.msViewTemplates.Checked = !this.msViewTemplates.Checked); break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msWinCascade": this.LayoutMdi(MdiLayout.Cascade); break;
                    case "msWinTileH": this.LayoutMdi(MdiLayout.TileHorizontal); break;
                    case "msWinTileV": this.LayoutMdi(MdiLayout.TileVertical); break;
                    case "msToolsConfig": App.ShowConfig(); break;
                    case "msHelpAbout": new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpItemClick(object sender, System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                Help.ShowHelp(this, this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception) { }
        }
        #endregion
        #region Local Services: setUserServices(), buildHelpMenu(), ColumnHeaders
        private void setUserServices() {
            //Set user services
            try {
                this.msFileNew.Enabled = this.csNew.Enabled = this.tsNew.Enabled = this.grdSchedules.Focused && RoleServiceGateway.IsLineHaulAdministrator;
                this.msFileOpen.Enabled = this.csOpen.Enabled = this.tsOpen.Enabled = this.grdSchedules.Focused && this.grdSchedules.Selected.Rows.Count > 0;
                this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanSave;
                if(this.grdSchedules.Focused)
                    this.msFileExport.Enabled = this.csExport.Enabled = this.tsExport.Enabled = this.grdSchedules.Selected.Rows.Count > 0;
                else if(this.mActiveSchedule != null)
                    this.msFileExport.Enabled = this.csExport.Enabled = this.tsExport.Enabled = this.mActiveSchedule.CanExport;
                this.msFileEmailCarriers.Enabled = this.tsSendCarriers.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanEmailCarriers;
                this.msFileEmailAgents.Enabled = this.tsSendAgents.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanEmailAgents;
                this.msFileSetup.Enabled = true;
                this.msFilePrint.Enabled = this.tsPrint.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanPrint;
                this.msFilePreview.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanPreview;
                this.msFileExit.Enabled = true;
                this.msEditCut.Enabled = this.tsCut.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanCut;
                this.msEditCopy.Enabled = this.tsCopy.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanCopy;
                this.msEditPaste.Enabled = this.tsPaste.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanPaste;
                this.msEditAdd.Enabled = this.csAddLoads.Enabled = this.tsAdd.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanAddLoads && this.grdTemplates.Selected.Rows.Count > 0;
                this.msEditCancel.Enabled = this.tsCancel.Enabled = this.mActiveSchedule != null && this.mActiveSchedule.CanCancelLoad;
                this.msEditCancel.Checked = this.mActiveSchedule != null && this.mActiveSchedule.IsLoadCancelled;
                this.tsCancel.Text = this.mActiveSchedule != null && this.mActiveSchedule.IsLoadCancelled ? "Uncancel Load" : "Cancel Load";
                this.msEditSearch.Enabled = this.tsSearch.Enabled = true;
                this.msViewRefresh.Enabled = this.csRefresh.Enabled = this.tsRefresh.Enabled = true;
                this.msViewFullScreen.Enabled = this.tsFullScreen.Enabled = this.MdiChildren.Length > 0;
                this.msViewTemplates.Enabled = RoleServiceGateway.IsLineHaulAdministrator;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(ShipScheduleGateway.ServiceState, ShipScheduleGateway.ServiceAddress));
                this.ssMain.User1Panel.Width = 150;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
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
                    //item.Name = "msHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpItemClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private string ColumnHeaders {
            get {
                MemoryStream ms = new MemoryStream();
                this.grdSchedules.DisplayLayout.SaveAsXml(ms, PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if(value.Length > 0) {
                    MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdSchedules.DisplayLayout.LoadFromXml(ms, PropertyCategories.SortedColumns);
                }
            }
        }
        #endregion
        #region Ship Schedule Services: newSchedule(), openSchedule()
        private void newSchedule() {
            //Create a new ship schedule
            dlgSelectDate dlg = new dlgSelectDate();
            if(dlg.ShowDialog() == DialogResult.OK) {
                //Validate that there isn't a schedule for the selected sortcenter and date
                if(ShipSchedules.SchedulesItem(dlg.SortCenterID, dlg.ScheduleDate) != null) {
                    //Existing schedule; allow option to edit
                    if(MessageBox.Show(this, "Schedule for this date already exists. Would you like to edit it?", "Manage Ship Schedule", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK) {
                        //Find the schedule entry, get the scheduleID, and open the schedule
                        this.Cursor = Cursors.WaitCursor;
                        for(int i = 0; i < this.grdSchedules.Rows.VisibleRowCount; i++) {
                            if(this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["SortCenterID"].Value.ToString() == dlg.SortCenterID.ToString() &&
                                this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["ScheduleDate"].Value.ToString() == dlg.ScheduleDate.ToString()) {
                                this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Selected = true;
                                this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Activate();
                                openSchedule();
                            }
                        }
                    }
                }
                else {
                    //New schedule; request a new schedule instance
                    this.Cursor = Cursors.WaitCursor;
                    ShipSchedule schedule = ShipSchedules.SchedulesAdd(dlg.SortCenterID, dlg.SortCenter, dlg.ScheduleDate);
                    //Find the new schedule entry, get the scheduleID, and open the schedule
                    for(int i = 0; i < this.grdSchedules.Rows.VisibleRowCount; i++) {
                        if(this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["SortCenterID"].Value.ToString() == dlg.SortCenterID.ToString() &&
                            this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["ScheduleDate"].Value.ToString() == dlg.ScheduleDate.ToString()) {
                            this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Selected = true;
                            this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Activate();
                            openSchedule();
                        }
                    }
                }
            }
        }
        private void openSchedule() {
            //Open an existing schedule
            this.Cursor = Cursors.WaitCursor;
            string scheduleID = "";
            winSchedule win = null;
            //if(this.grdSchedules.Selected.Rows.Count > 0) {
            for(int k = 0; k < this.grdSchedules.Selected.Rows.Count; k++) {
                //Open the selected schedule; validate it is not open already
                scheduleID = this.grdSchedules.Selected.Rows[k].Cells["ScheduleID"].Value.ToString();
                for(int i = 0; i < this.MdiChildren.Length; i++) {
                    win = (winSchedule)this.MdiChildren[i];
                    if(win.Schedule.ScheduleID == scheduleID) {
                        //Already open; bring to forefront
                        win.Activate();
                        if(win.WindowState == FormWindowState.Minimized) win.WindowState = FormWindowState.Normal;
                        break;
                    }
                    else
                        win = null;
                }
                if(win == null) {
                    //Not open; open and register for events
                    ShipSchedule schedule = ShipSchedules.SchedulesItem(scheduleID);
                    this.mMessageMgr.AddMessage("Opening " + schedule.SortCenter + " ship schedule for " + schedule.ScheduleDate + "...");
                    win = new winSchedule(schedule);
                    win.WindowState = (this.MdiChildren.Length > 0) ? this.MdiChildren[0].WindowState : FormWindowState.Maximized;
                    win.MdiParent = this;
                    win.Activated += new EventHandler(OnScheduleActivated);
                    win.Deactivate += new EventHandler(OnScheduleDeactivated);
                    win.Closing += new CancelEventHandler(OnScheduleClosing);
                    win.Closed += new EventHandler(OnScheduleClosed);
                    win.StatusMessage += new StatusEventHandler(OnStatusMessage);
                    win.ServiceStatesChanged += new EventHandler(OnServiceStatesChanged);
                    win.Show();
                }
            }
        }
        #endregion
        #region Schedule Window Mgt: OnScheduleActivated(), OnScheduleDeactivated(), OnScheduleClosing(), OnScheduleClosed()
        private void OnScheduleActivated(object sender, System.EventArgs e) {
            //Event handler for activaton of a viewer child window
            try {
                this.mActiveSchedule = null;
                if(sender != null) {
                    winSchedule frm = (winSchedule)sender;
                    this.mActiveSchedule = frm;
                    this.grdTemplates.DataSource = this.mActiveSchedule.Schedule.Templates;
                    for(int i = 0; i < this.grdSchedules.Rows.VisibleRowCount; i++) {
                        string sortCenterID = this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["SortCenterID"].Value.ToString();
                        string scheduleDate = this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Cells["ScheduleDate"].Value.ToString();
                        if(sortCenterID == this.mActiveSchedule.Schedule.SortCenterID.ToString() && scheduleDate == this.mActiveSchedule.Schedule.ScheduleDate.ToString()) {
                            this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Selected = true;
                            this.grdSchedules.Rows.GetRowAtVisibleIndex(i).Activate();
                        }
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnScheduleDeactivated(object sender, System.EventArgs e) {
            //Event handler for deactivaton of a viewer child window
            this.mActiveSchedule = null;
            this.grdTemplates.DataSource = this.mTemplates;
            this.grdSchedules.Selected.Rows.Clear();
            setUserServices();
        }
        private void OnScheduleClosing(object sender, System.ComponentModel.CancelEventArgs e) {
            //Event handler for form closing via control box; e.Cancel=true keeps window open
            try {
                e.Cancel = false;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnScheduleClosed(object sender, System.EventArgs e) {
            //Event handler for closing of a viewer child window
            if(this.MdiChildren.Length == 1 && this.tsFullScreen.Checked) this.msViewFullScreen.PerformClick();
            setUserServices();
        }
        private void OnServiceStatesChanged(object sender, System.EventArgs e) { setUserServices(); }
        private void OnStatusMessage(object sender, StatusEventArgs e) { this.mMessageMgr.AddMessage(e.Message); }
        #endregion
        #region Template Services: OnTemplateSelected(), OnCloseTemplates(), OnEnterTemplates(), OnLeaveTemplates()
        private void OnTemplateSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for change in template row selections
            try {
                //Clear current selections
                for(int i = 0; i < this.mActiveSchedule.Schedule.Templates.TemplateViewTable.Rows.Count; i++)
                    this.mActiveSchedule.Schedule.Templates.TemplateViewTable[i].Selected = false;

                //Update all selected load templates as selected for Add
                for(int i = 0; i < this.grdTemplates.Selected.Rows.Count; i++) {
                    string templateID = this.grdTemplates.Selected.Rows[i].Cells["TemplateID"].Value.ToString();
                    for(int j = 0; j < this.mActiveSchedule.Schedule.Templates.TemplateViewTable.Rows.Count; j++) {
                        if(this.mActiveSchedule.Schedule.Templates.TemplateViewTable[j].TemplateID == templateID) {
                            this.mActiveSchedule.Schedule.Templates.TemplateViewTable[j].Selected = true;
                            break;
                        }
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnCloseTemplates(object sender, System.EventArgs e) {
            //Event handler to close log windows
            this.msViewTemplates.PerformClick();
        }
        private void OnEnterTemplates(object sender, System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblTemplateHeader.BackColor = this.lblCloseTemplates.BackColor = SystemColors.ActiveCaption;
                this.lblTemplateHeader.ForeColor = this.lblCloseTemplates.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveTemplates(object sender, System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblTemplateHeader.BackColor = this.lblCloseTemplates.BackColor = SystemColors.Control;
                this.lblTemplateHeader.ForeColor = this.lblCloseTemplates.ForeColor = SystemColors.ControlText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
    }
}
