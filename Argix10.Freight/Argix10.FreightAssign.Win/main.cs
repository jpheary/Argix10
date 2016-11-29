using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;

namespace Argix.Freight {
	//Freight Assignment main application window
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private string mTerminalName = "";
        private InboundShipment mSelectedFreight = null;
        private StationAssignment mSelectedAssignment=null;
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvcShipments = null,mGridSvcAssignments = null,mGridSvcHistory = null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
		
		#region Controls

        private Argix.Windows.ArgixStatusBar ssMain;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdShipments;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdAssignments;
        private Argix.Freight.TsortDataset mShipmentsDS;
        private Argix.Freight.TsortDataset mAssignmentsDS;
        private System.Windows.Forms.Label _lblDays;
        private System.Windows.Forms.NumericUpDown updSortedDays;
		private System.Windows.Forms.TabControl tabAssignments;
		private System.Windows.Forms.TabPage tabRealTime;
		private System.Windows.Forms.TabPage tabHistory;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdHistory;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSave;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileSetup;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePreview;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msEditSearch;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msFreight;
        private ToolStripMenuItem msFreightAssign;
        private ToolStripMenuItem msFreightUnassign;
        private ToolStripSeparator msFreightSep1;
        private ToolStripMenuItem msFreightStopSort;
        private ToolStripMenuItem msAssignments;
        private ToolStripMenuItem msAssignmentsUnassign;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ContextMenuStrip csFreight;
        private ToolStripMenuItem csFAssign;
        private ToolStripMenuItem csFUnassign;
        private ToolStripSeparator csFSep1;
        private ToolStripMenuItem csFStopSort;
        private ContextMenuStrip csAssignments;
        private ToolStripMenuItem csAUnassign;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSave;
        private ToolStripButton tsPrint;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsRefresh;
        private ToolStripButton tsSearch;
        private ToolStripSeparator tsSep3;
        private ToolStripButton tsAssign;
        private ToolStripButton tsUnassign;
        private ToolStripButton tsStopSort;
        private ToolStripSeparator tsSep4;
        private ToolStripButton tsUnassign2;
        private ToolStripTextBox txtSearch;
        private Splitter splitterH;
        private ComboBox cboFreightType;
        private ToolStripMenuItem csARefresh;
        private ToolStripMenuItem csAClear;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripSeparator tsSep5;
        private ToolStripSeparator tsSep6;
        private ToolStripMenuItem msViewHistory;
        private ToolStripMenuItem msAssignmentsClearHistory;
        private ToolStripMenuItem csFRefresh;
        private ToolStripSeparator csFSep2;
        private TsortDataset mTerminalDS;
        private Label lblShowSorted;
		private System.ComponentModel.IContainer components;
		
		#endregion
		
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
                #region Window docking
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                this.grdShipments.Dock = DockStyle.Fill;
                this.splitterH.MinExtra = this.splitterH.MinSize = 96;
				this.splitterH.Dock = DockStyle.Bottom;
				this.tabAssignments.Dock = DockStyle.Bottom;
				this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.grdShipments,this.splitterH,this.tabAssignments,this.tsMain,this.msMain,this.ssMain });
				#endregion

                //Create data and UI services
                this.mPageSettings = new PageSettings();
                this.mPageSettings.Landscape = true;
                this.mGridSvcShipments = new UltraGridSvc(this.grdShipments);
                this.mGridSvcAssignments = new UltraGridSvc(this.grdAssignments);
                this.mGridSvcHistory = new UltraGridSvc(this.grdHistory);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],500,3000);

                TsortGateway.FreightChanged += new EventHandler(this.OnFreightChanged);
                TsortGateway.AssignmentsChanged += new EventHandler(this.OnAssignmentsChanged);
                TsortGateway.AssignmentHistoryChanged += new EventHandler(this.OnAssignmentHistoryChanged);
            }
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("StationFreightAssignmentTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WorkStationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StationNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Client");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Shipper");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pickup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Result");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("InboundFreightTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CurrentLocation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StorageTrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn64 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn65 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn66 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pickup");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn67 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn68 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn69 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn70 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn71 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn72 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloorStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn73 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SealNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn74 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UnloadedStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn75 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorKey");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn76 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReceiveDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn77 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn78 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsSortable");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msFreight = new System.Windows.Forms.ToolStripMenuItem();
            this.msFreightAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.msFreightUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.msFreightSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFreightStopSort = new System.Windows.Forms.ToolStripMenuItem();
            this.msAssignments = new System.Windows.Forms.ToolStripMenuItem();
            this.msAssignmentsUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.msAssignmentsClearHistory = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.csFreight = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csFRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csFSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csFAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.csFUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.csFSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csFStopSort = new System.Windows.Forms.ToolStripMenuItem();
            this.csAssignments = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csARefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csAClear = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.csAUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.txtSearch = new System.Windows.Forms.ToolStripTextBox();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAssign = new System.Windows.Forms.ToolStripButton();
            this.tsUnassign = new System.Windows.Forms.ToolStripButton();
            this.tsSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.tsStopSort = new System.Windows.Forms.ToolStripButton();
            this.tsSep6 = new System.Windows.Forms.ToolStripSeparator();
            this.tsUnassign2 = new System.Windows.Forms.ToolStripButton();
            this.tabAssignments = new System.Windows.Forms.TabControl();
            this.tabRealTime = new System.Windows.Forms.TabPage();
            this.grdAssignments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mAssignmentsDS = new Argix.Freight.TsortDataset();
            this.tabHistory = new System.Windows.Forms.TabPage();
            this.grdHistory = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdShipments = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cboFreightType = new System.Windows.Forms.ComboBox();
            this.lblShowSorted = new System.Windows.Forms.Label();
            this.updSortedDays = new System.Windows.Forms.NumericUpDown();
            this._lblDays = new System.Windows.Forms.Label();
            this.mShipmentsDS = new Argix.Freight.TsortDataset();
            this.mTerminalDS = new Argix.Freight.TsortDataset();
            this.msMain.SuspendLayout();
            this.csFreight.SuspendLayout();
            this.csAssignments.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tabAssignments.SuspendLayout();
            this.tabRealTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentsDS)).BeginInit();
            this.tabHistory.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipments)).BeginInit();
            this.grdShipments.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updSortedDays)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipmentsDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminalDS)).BeginInit();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msEdit,
            this.msView,
            this.msFreight,
            this.msAssignments,
            this.msTools,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(0);
            this.msMain.Size = new System.Drawing.Size(760, 24);
            this.msMain.TabIndex = 5;
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFileSep1,
            this.msFileSave,
            this.msFileSaveAs,
            this.msFileSep2,
            this.msFileSetup,
            this.msFilePrint,
            this.msFilePreview,
            this.msFileSep3,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37, 24);
            this.msFile.Text = "&File";
            // 
            // msFileNew
            // 
            this.msFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
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
            // msFileSave
            // 
            this.msFileSave.Image = global::Argix.Properties.Resources.Save;
            this.msFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(152, 22);
            this.msFileSave.Text = "&Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSaveAs
            // 
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
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(149, 6);
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
            this.msEditSearch});
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 24);
            this.msEdit.Text = "Edit";
            // 
            // msEditSearch
            // 
            this.msEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.msEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditSearch.Name = "msEditSearch";
            this.msEditSearch.Size = new System.Drawing.Size(109, 22);
            this.msEditSearch.Text = "&Search";
            this.msEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewFont,
            this.msViewHistory,
            this.msViewSep2,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 24);
            this.msView.Text = "&View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.msViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(126, 22);
            this.msViewRefresh.Text = "&Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(123, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(126, 22);
            this.msViewFont.Text = "Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewHistory
            // 
            this.msViewHistory.Checked = true;
            this.msViewHistory.CheckState = System.Windows.Forms.CheckState.Checked;
            this.msViewHistory.Name = "msViewHistory";
            this.msViewHistory.Size = new System.Drawing.Size(126, 22);
            this.msViewHistory.Text = "History";
            this.msViewHistory.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(123, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(126, 22);
            this.msViewToolbar.Text = "&Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(126, 22);
            this.msViewStatusBar.Text = "&Status Bar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFreight
            // 
            this.msFreight.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFreightAssign,
            this.msFreightUnassign,
            this.msFreightSep1,
            this.msFreightStopSort});
            this.msFreight.Name = "msFreight";
            this.msFreight.Size = new System.Drawing.Size(56, 24);
            this.msFreight.Text = "F&reight";
            // 
            // msFreightAssign
            // 
            this.msFreightAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.msFreightAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFreightAssign.Name = "msFreightAssign";
            this.msFreightAssign.Size = new System.Drawing.Size(207, 22);
            this.msFreightAssign.Text = "&Assign To Stations...";
            this.msFreightAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFreightUnassign
            // 
            this.msFreightUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.msFreightUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFreightUnassign.Name = "msFreightUnassign";
            this.msFreightUnassign.Size = new System.Drawing.Size(207, 22);
            this.msFreightUnassign.Text = "&Unassign From Stations...";
            this.msFreightUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFreightSep1
            // 
            this.msFreightSep1.Name = "msFreightSep1";
            this.msFreightSep1.Size = new System.Drawing.Size(204, 6);
            // 
            // msFreightStopSort
            // 
            this.msFreightStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.msFreightStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFreightStopSort.Name = "msFreightStopSort";
            this.msFreightStopSort.Size = new System.Drawing.Size(207, 22);
            this.msFreightStopSort.Text = "Stop Sorting Shipment";
            this.msFreightStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msAssignments
            // 
            this.msAssignments.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msAssignmentsUnassign,
            this.msAssignmentsClearHistory});
            this.msAssignments.Name = "msAssignments";
            this.msAssignments.Size = new System.Drawing.Size(82, 24);
            this.msAssignments.Text = "&Assignment";
            // 
            // msAssignmentsUnassign
            // 
            this.msAssignmentsUnassign.Image = global::Argix.Properties.Resources.Delete;
            this.msAssignmentsUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msAssignmentsUnassign.Name = "msAssignmentsUnassign";
            this.msAssignmentsUnassign.Size = new System.Drawing.Size(142, 22);
            this.msAssignmentsUnassign.Text = "&Remove";
            this.msAssignmentsUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msAssignmentsClearHistory
            // 
            this.msAssignmentsClearHistory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msAssignmentsClearHistory.Name = "msAssignmentsClearHistory";
            this.msAssignmentsClearHistory.Size = new System.Drawing.Size(142, 22);
            this.msAssignmentsClearHistory.Text = "Clear History";
            this.msAssignmentsClearHistory.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msTools
            // 
            this.msTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msToolsConfig});
            this.msTools.Name = "msTools";
            this.msTools.Size = new System.Drawing.Size(48, 24);
            this.msTools.Text = "&Tools";
            // 
            // msToolsConfig
            // 
            this.msToolsConfig.Name = "msToolsConfig";
            this.msToolsConfig.Size = new System.Drawing.Size(148, 22);
            this.msToolsConfig.Text = "&Configuration";
            this.msToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 24);
            this.msHelp.Text = "&Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(185, 22);
            this.msHelpAbout.Text = "&About Freight Assign";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(182, 6);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 305);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(760, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 3;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // csFreight
            // 
            this.csFreight.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csFRefresh,
            this.csFSep1,
            this.csFAssign,
            this.csFUnassign,
            this.csFSep2,
            this.csFStopSort});
            this.csFreight.Name = "csFreight";
            this.csFreight.Size = new System.Drawing.Size(199, 104);
            // 
            // csFRefresh
            // 
            this.csFRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csFRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csFRefresh.Name = "csFRefresh";
            this.csFRefresh.Size = new System.Drawing.Size(198, 22);
            this.csFRefresh.Text = "&Refresh";
            this.csFRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csFSep1
            // 
            this.csFSep1.Name = "csFSep1";
            this.csFSep1.Size = new System.Drawing.Size(195, 6);
            // 
            // csFAssign
            // 
            this.csFAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.csFAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csFAssign.Name = "csFAssign";
            this.csFAssign.Size = new System.Drawing.Size(198, 22);
            this.csFAssign.Text = "&Assign To Stations";
            this.csFAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csFUnassign
            // 
            this.csFUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.csFUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csFUnassign.Name = "csFUnassign";
            this.csFUnassign.Size = new System.Drawing.Size(198, 22);
            this.csFUnassign.Text = "&Unassign From Stations";
            this.csFUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csFSep2
            // 
            this.csFSep2.Name = "csFSep2";
            this.csFSep2.Size = new System.Drawing.Size(195, 6);
            // 
            // csFStopSort
            // 
            this.csFStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.csFStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csFStopSort.Name = "csFStopSort";
            this.csFStopSort.Size = new System.Drawing.Size(198, 22);
            this.csFStopSort.Text = "Stop Sorting Shipment";
            this.csFStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csAssignments
            // 
            this.csAssignments.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csARefresh,
            this.csAClear,
            this.toolStripMenuItem1,
            this.csAUnassign});
            this.csAssignments.Name = "csAssignments";
            this.csAssignments.Size = new System.Drawing.Size(118, 76);
            // 
            // csARefresh
            // 
            this.csARefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csARefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csARefresh.Name = "csARefresh";
            this.csARefresh.Size = new System.Drawing.Size(117, 22);
            this.csARefresh.Text = "Refresh";
            this.csARefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csAClear
            // 
            this.csAClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csAClear.Name = "csAClear";
            this.csAClear.Size = new System.Drawing.Size(117, 22);
            this.csAClear.Text = "Clear";
            this.csAClear.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(114, 6);
            // 
            // csAUnassign
            // 
            this.csAUnassign.Image = global::Argix.Properties.Resources.Delete;
            this.csAUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csAUnassign.Name = "csAUnassign";
            this.csAUnassign.Size = new System.Drawing.Size(117, 22);
            this.csAUnassign.Text = "&Remove";
            this.csAUnassign.Click += new System.EventHandler(this.OnItemClick);
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
            this.tsPrint,
            this.tsSep2,
            this.tsSearch,
            this.txtSearch,
            this.tsSep3,
            this.tsRefresh,
            this.tsSep4,
            this.tsAssign,
            this.tsUnassign,
            this.tsSep5,
            this.tsStopSort,
            this.tsSep6,
            this.tsUnassign2});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsMain.Size = new System.Drawing.Size(760, 53);
            this.tsMain.TabIndex = 6;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 50);
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
            this.tsOpen.Size = new System.Drawing.Size(40, 50);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open...";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 53);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 50);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save...";
            this.tsSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 50);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print...";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 53);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Find;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(36, 50);
            this.tsSearch.Text = "Find";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.ToolTipText = "Search for a TDS";
            this.tsSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // txtSearch
            // 
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(96, 53);
            this.txtSearch.ToolTipText = "Search for a TDS";
            this.txtSearch.TextChanged += new System.EventHandler(this.OnSearchTextChanged);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 53);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 50);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 53);
            // 
            // tsAssign
            // 
            this.tsAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.tsAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAssign.Name = "tsAssign";
            this.tsAssign.Size = new System.Drawing.Size(46, 50);
            this.tsAssign.Text = "Assign";
            this.tsAssign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsAssign.ToolTipText = "Assign freight to sort station";
            this.tsAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsUnassign
            // 
            this.tsUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.tsUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUnassign.Name = "tsUnassign";
            this.tsUnassign.Size = new System.Drawing.Size(59, 50);
            this.tsUnassign.Text = "Unassign";
            this.tsUnassign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsUnassign.ToolTipText = "Unassign freight from all station";
            this.tsUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep5
            // 
            this.tsSep5.Name = "tsSep5";
            this.tsSep5.Size = new System.Drawing.Size(6, 53);
            // 
            // tsStopSort
            // 
            this.tsStopSort.Image = global::Argix.Properties.Resources.Stop;
            this.tsStopSort.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsStopSort.Name = "tsStopSort";
            this.tsStopSort.Size = new System.Drawing.Size(59, 50);
            this.tsStopSort.Text = "Stop Sort";
            this.tsStopSort.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsStopSort.ToolTipText = "Stop sorting freight";
            this.tsStopSort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep6
            // 
            this.tsSep6.Name = "tsSep6";
            this.tsSep6.Size = new System.Drawing.Size(6, 53);
            // 
            // tsUnassign2
            // 
            this.tsUnassign2.Image = global::Argix.Properties.Resources.Delete;
            this.tsUnassign2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUnassign2.Name = "tsUnassign2";
            this.tsUnassign2.Size = new System.Drawing.Size(54, 50);
            this.tsUnassign2.Text = "Remove";
            this.tsUnassign2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsUnassign2.ToolTipText = "Remove assignment from selected sort station";
            this.tsUnassign2.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabAssignments
            // 
            this.tabAssignments.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabAssignments.Controls.Add(this.tabRealTime);
            this.tabAssignments.Controls.Add(this.tabHistory);
            this.tabAssignments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabAssignments.Location = new System.Drawing.Point(0, 170);
            this.tabAssignments.Name = "tabAssignments";
            this.tabAssignments.SelectedIndex = 0;
            this.tabAssignments.ShowToolTips = true;
            this.tabAssignments.Size = new System.Drawing.Size(760, 135);
            this.tabAssignments.TabIndex = 2;
            this.tabAssignments.SelectedIndexChanged += new System.EventHandler(this.OnAssignmentTabChanged);
            // 
            // tabRealTime
            // 
            this.tabRealTime.Controls.Add(this.grdAssignments);
            this.tabRealTime.Location = new System.Drawing.Point(4, 4);
            this.tabRealTime.Name = "tabRealTime";
            this.tabRealTime.Size = new System.Drawing.Size(752, 109);
            this.tabRealTime.TabIndex = 0;
            this.tabRealTime.Text = "Current";
            this.tabRealTime.UseVisualStyleBackColor = true;
            // 
            // grdAssignments
            // 
            this.grdAssignments.ContextMenuStrip = this.csAssignments;
            this.grdAssignments.DataMember = "StationFreightAssignmentTable";
            this.grdAssignments.DataSource = this.mAssignmentsDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Appearance = appearance1;
            ultraGridColumn43.Header.VisiblePosition = 9;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.Caption = "Station#";
            ultraGridColumn44.Header.VisiblePosition = 0;
            ultraGridColumn44.Width = 96;
            ultraGridColumn45.Header.VisiblePosition = 8;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.Header.Caption = "Type";
            ultraGridColumn46.Header.VisiblePosition = 1;
            ultraGridColumn46.Width = 60;
            ultraGridColumn47.Header.VisiblePosition = 11;
            ultraGridColumn47.Hidden = true;
            ultraGridColumn48.Header.Caption = "Sort Type";
            ultraGridColumn48.Header.VisiblePosition = 2;
            ultraGridColumn48.Width = 72;
            ultraGridColumn49.Header.Caption = "TDS#";
            ultraGridColumn49.Header.VisiblePosition = 3;
            ultraGridColumn49.Width = 96;
            ultraGridColumn50.Header.Caption = "Trailer#";
            ultraGridColumn50.Header.VisiblePosition = 4;
            ultraGridColumn50.Width = 72;
            ultraGridColumn51.Header.VisiblePosition = 5;
            ultraGridColumn51.Width = 192;
            ultraGridColumn52.Header.Caption = "Vendor\\Agent";
            ultraGridColumn52.Header.VisiblePosition = 6;
            ultraGridColumn52.Width = 192;
            ultraGridColumn53.Header.VisiblePosition = 7;
            ultraGridColumn53.Width = 120;
            ultraGridColumn54.Header.VisiblePosition = 10;
            ultraGridColumn54.Hidden = true;
            ultraGridColumn55.Header.VisiblePosition = 12;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55});
            this.grdAssignments.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.CaptionAppearance = appearance2;
            this.grdAssignments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAssignments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdAssignments.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdAssignments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAssignments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAssignments.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdAssignments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAssignments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAssignments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAssignments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAssignments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAssignments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAssignments.Location = new System.Drawing.Point(0, 0);
            this.grdAssignments.Name = "grdAssignments";
            this.grdAssignments.Size = new System.Drawing.Size(752, 109);
            this.grdAssignments.TabIndex = 0;
            this.grdAssignments.Text = "Station Assignments";
            this.grdAssignments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdAssignments.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnAssignmentSelected);
            this.grdAssignments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mAssignmentsDS
            // 
            this.mAssignmentsDS.DataSetName = "TsortDataset";
            this.mAssignmentsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mAssignmentsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabHistory
            // 
            this.tabHistory.Controls.Add(this.grdHistory);
            this.tabHistory.Location = new System.Drawing.Point(4, 4);
            this.tabHistory.Name = "tabHistory";
            this.tabHistory.Size = new System.Drawing.Size(752, 109);
            this.tabHistory.TabIndex = 1;
            this.tabHistory.Text = "History";
            this.tabHistory.UseVisualStyleBackColor = true;
            // 
            // grdHistory
            // 
            this.grdHistory.ContextMenuStrip = this.csAssignments;
            this.grdHistory.DataMember = "FreightAssignmentHistoryTable";
            this.grdHistory.DataSource = this.mAssignmentsDS;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdHistory.DisplayLayout.Appearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance6.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdHistory.DisplayLayout.CaptionAppearance = appearance6;
            this.grdHistory.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdHistory.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.TextHAlignAsString = "Left";
            this.grdHistory.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdHistory.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdHistory.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdHistory.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdHistory.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdHistory.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdHistory.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdHistory.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdHistory.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdHistory.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdHistory.Location = new System.Drawing.Point(0, 0);
            this.grdHistory.Name = "grdHistory";
            this.grdHistory.Size = new System.Drawing.Size(752, 109);
            this.grdHistory.TabIndex = 1;
            this.grdHistory.Text = "Station Assignment History";
            this.grdHistory.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdHistory.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnAssignmentHistorySelected);
            this.grdHistory.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 167);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(760, 3);
            this.splitterH.TabIndex = 7;
            this.splitterH.TabStop = false;
            // 
            // grdShipments
            // 
            this.grdShipments.ContextMenuStrip = this.csFreight;
            this.grdShipments.Controls.Add(this.cboFreightType);
            this.grdShipments.Controls.Add(this.lblShowSorted);
            this.grdShipments.Controls.Add(this.updSortedDays);
            this.grdShipments.Controls.Add(this._lblDays);
            this.grdShipments.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdShipments.DataMember = "InboundFreightTable";
            this.grdShipments.DataSource = this.mShipmentsDS;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.grdShipments.DisplayLayout.Appearance = appearance9;
            ultraGridColumn56.Header.VisiblePosition = 19;
            ultraGridColumn56.Width = 120;
            ultraGridColumn57.Header.VisiblePosition = 20;
            ultraGridColumn57.Hidden = true;
            ultraGridColumn58.Header.Caption = "Location";
            ultraGridColumn58.Header.VisiblePosition = 0;
            ultraGridColumn58.Width = 72;
            ultraGridColumn59.Header.Caption = "TDS#";
            ultraGridColumn59.Header.VisiblePosition = 1;
            ultraGridColumn59.Width = 96;
            ultraGridColumn60.Header.Caption = "Trailer#";
            ultraGridColumn60.Header.VisiblePosition = 2;
            ultraGridColumn60.Width = 60;
            ultraGridColumn61.Header.Caption = "St. Trailer#";
            ultraGridColumn61.Header.VisiblePosition = 3;
            ultraGridColumn61.Width = 60;
            ultraGridColumn62.Header.Caption = "Client#";
            ultraGridColumn62.Header.VisiblePosition = 4;
            ultraGridColumn62.Width = 60;
            ultraGridColumn63.Header.Caption = "Client";
            ultraGridColumn63.Header.VisiblePosition = 5;
            ultraGridColumn63.Width = 168;
            ultraGridColumn64.Header.Caption = "Shipper#";
            ultraGridColumn64.Header.VisiblePosition = 6;
            ultraGridColumn64.Width = 60;
            ultraGridColumn65.Header.Caption = "Shipper";
            ultraGridColumn65.Header.VisiblePosition = 7;
            ultraGridColumn65.Width = 168;
            ultraGridColumn66.Header.VisiblePosition = 8;
            ultraGridColumn66.Width = 96;
            ultraGridColumn67.Header.VisiblePosition = 9;
            ultraGridColumn67.Width = 96;
            appearance10.TextHAlignAsString = "Right";
            ultraGridColumn68.CellAppearance = appearance10;
            appearance11.TextHAlignAsString = "Right";
            ultraGridColumn68.Header.Appearance = appearance11;
            ultraGridColumn68.Header.VisiblePosition = 10;
            ultraGridColumn68.Width = 72;
            appearance12.TextHAlignAsString = "Right";
            ultraGridColumn69.CellAppearance = appearance12;
            appearance13.TextHAlignAsString = "Right";
            ultraGridColumn69.Header.Appearance = appearance13;
            ultraGridColumn69.Header.VisiblePosition = 11;
            ultraGridColumn69.Width = 72;
            ultraGridColumn70.Header.Caption = "Carrier#";
            ultraGridColumn70.Header.VisiblePosition = 12;
            ultraGridColumn70.Width = 60;
            ultraGridColumn71.Header.Caption = "Driver#";
            ultraGridColumn71.Header.VisiblePosition = 13;
            ultraGridColumn71.Width = 60;
            ultraGridColumn72.Header.Caption = "Floor Status";
            ultraGridColumn72.Header.VisiblePosition = 14;
            ultraGridColumn72.Width = 72;
            ultraGridColumn73.Header.Caption = "Seal#";
            ultraGridColumn73.Header.VisiblePosition = 15;
            ultraGridColumn73.Width = 60;
            ultraGridColumn74.Header.Caption = "Unloaded";
            ultraGridColumn74.Header.VisiblePosition = 16;
            ultraGridColumn74.Width = 72;
            ultraGridColumn75.Header.Caption = "Vendor Key";
            ultraGridColumn75.Header.VisiblePosition = 17;
            ultraGridColumn75.Width = 84;
            ultraGridColumn76.Header.Caption = "Received";
            ultraGridColumn76.Header.VisiblePosition = 18;
            ultraGridColumn76.Width = 84;
            ultraGridColumn77.Header.VisiblePosition = 21;
            ultraGridColumn77.Hidden = true;
            ultraGridColumn78.Header.Caption = "Sortable";
            ultraGridColumn78.Header.VisiblePosition = 22;
            ultraGridColumn78.Width = 60;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61,
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn64,
            ultraGridColumn65,
            ultraGridColumn66,
            ultraGridColumn67,
            ultraGridColumn68,
            ultraGridColumn69,
            ultraGridColumn70,
            ultraGridColumn71,
            ultraGridColumn72,
            ultraGridColumn73,
            ultraGridColumn74,
            ultraGridColumn75,
            ultraGridColumn76,
            ultraGridColumn77,
            ultraGridColumn78});
            this.grdShipments.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance14.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance14.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdShipments.DisplayLayout.CaptionAppearance = appearance14;
            this.grdShipments.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdShipments.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.TextHAlignAsString = "Left";
            this.grdShipments.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grdShipments.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdShipments.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdShipments.DisplayLayout.Override.RowAppearance = appearance16;
            this.grdShipments.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdShipments.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdShipments.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdShipments.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdShipments.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdShipments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipments.Location = new System.Drawing.Point(0, 77);
            this.grdShipments.Name = "grdShipments";
            this.grdShipments.Size = new System.Drawing.Size(760, 90);
            this.grdShipments.TabIndex = 0;
            this.grdShipments.Text = "Inbound Freight ";
            this.grdShipments.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipments.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnFreightSelected);
            this.grdShipments.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // cboFreightType
            // 
            this.cboFreightType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboFreightType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboFreightType.FormattingEnabled = true;
            this.cboFreightType.ItemHeight = 13;
            this.cboFreightType.Items.AddRange(new object[] {
            "Regular",
            "Returns"});
            this.cboFreightType.Location = new System.Drawing.Point(126, 1);
            this.cboFreightType.Name = "cboFreightType";
            this.cboFreightType.Size = new System.Drawing.Size(96, 21);
            this.cboFreightType.TabIndex = 8;
            this.cboFreightType.SelectionChangeCommitted += new System.EventHandler(this.OnFreightTypeChanged);
            // 
            // lblShowSorted
            // 
            this.lblShowSorted.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblShowSorted.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblShowSorted.Location = new System.Drawing.Point(581, 2);
            this.lblShowSorted.Name = "lblShowSorted";
            this.lblShowSorted.Size = new System.Drawing.Size(100, 16);
            this.lblShowSorted.TabIndex = 8;
            this.lblShowSorted.Text = "Show sorted for ";
            this.lblShowSorted.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // updSortedDays
            // 
            this.updSortedDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.updSortedDays.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.updSortedDays.Location = new System.Drawing.Point(687, 3);
            this.updSortedDays.Name = "updSortedDays";
            this.updSortedDays.Size = new System.Drawing.Size(33, 16);
            this.updSortedDays.TabIndex = 3;
            this.updSortedDays.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updSortedDays.ValueChanged += new System.EventHandler(this.OnSortedDaysChanged);
            this.updSortedDays.Leave += new System.EventHandler(this.OnSortedDaysChanged);
            // 
            // _lblDays
            // 
            this._lblDays.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblDays.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this._lblDays.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this._lblDays.ImageAlign = System.Drawing.ContentAlignment.BottomCenter;
            this._lblDays.Location = new System.Drawing.Point(723, 2);
            this._lblDays.Name = "_lblDays";
            this._lblDays.Size = new System.Drawing.Size(33, 16);
            this._lblDays.TabIndex = 4;
            this._lblDays.Text = "days";
            this._lblDays.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // mShipmentsDS
            // 
            this.mShipmentsDS.DataSetName = "TsortDataset";
            this.mShipmentsDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mShipmentsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mTerminalDS
            // 
            this.mTerminalDS.DataSetName = "TsortDataset";
            this.mTerminalDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(760, 329);
            this.Controls.Add(this.grdShipments);
            this.Controls.Add(this.splitterH);
            this.Controls.Add(this.tabAssignments);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Freight Assignment";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.csFreight.ResumeLayout(false);
            this.csAssignments.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tabAssignments.ResumeLayout(false);
            this.tabRealTime.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAssignments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mAssignmentsDS)).EndInit();
            this.tabHistory.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdHistory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipments)).EndInit();
            this.grdShipments.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updSortedDays)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipmentsDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminalDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
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
                    this.msViewHistory.Checked = !Convert.ToBoolean(global::Argix.Properties.Settings.Default.History);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
                this.mToolTip.SetToolTip(this.cboFreightType,"Select the freight type to display.");
				this.mToolTip.SetToolTip(this.updSortedDays, "Include sorted freight for the last " + this.updSortedDays.Value + " days.");
				#endregion

                //Set control defaults
                #region Grid Initialization
                this.grdShipments.DisplayLayout.Bands[0].Columns["TDSNumber"].SortIndicator = SortIndicator.Ascending;
				this.grdAssignments.DisplayLayout.Bands[0].Columns["StationNumber"].SortIndicator = SortIndicator.Ascending;
				this.grdHistory.DisplayLayout.Bands[0].Columns["Date"].SortIndicator = SortIndicator.Ascending;
                #endregion
                ServiceInfo t = TsortGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.mTerminalName = t.Description.Trim();

                this.grdShipments.DataSource = TsortGateway.InboundFreight;
                this.grdAssignments.DataSource = TsortGateway.StationAssignments;
                this.grdHistory.DataSource = TsortGateway.StationAssignmentHistory;
                if (this.cboFreightType.Items.Count > 0) this.cboFreightType.SelectedIndex = 0;
				OnFreightTypeChanged(null, EventArgs.Empty);

				this.updSortedDays.Minimum = 0;
                this.updSortedDays.Maximum = 13;
                this.updSortedDays.Value = TsortGateway.SortedRange;
                this.msViewHistory.PerformClick();
                this.msViewRefresh.PerformClick();
                this.grdShipments.Focus();
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); } 
            finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            if(!e.Cancel) {
                #region Save user preferences
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.History = this.msViewHistory.Checked;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
            }
        }
        private void OnSearchTextChanged(object sender,EventArgs e) {
            //Event handler for change in search text value
            try {
                //Get specifics for search word and grid
                this.mGridSvcShipments.FindRow(0,this.grdShipments.Tag.ToString(),this.txtSearch.Text);
                this.txtSearch.Focus();
                this.txtSearch.SelectionStart = this.txtSearch.Text.Length;
                setUserServices();
            }
            catch(Exception) { }
        }
        private void OnAssignmentTabChanged(object sender,EventArgs e) {
            //Event handler for change in selected assignment tab
            try {
                switch(this.tabAssignments.SelectedTab.Name) {
                    case "tabRealTime": this.grdAssignments.Focus(); break;
                    case "tabHistory":  this.grdHistory.Focus(); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #region Freight Services: OnFreightTypeChanged(), OnSortedDaysChanged(), OnFreightChanged(), OnFreightSelected()
        private void OnFreightTypeChanged(object sender,System.EventArgs e) {
			//Event handler for change in freight type
			try {
				//Apply grid filters as applicable
                string freightType = this.cboFreightType.Text.ToLower();
				this.grdShipments.DisplayLayout.Bands[0].ColumnFilters["FreightType"].FilterConditions.Clear();
                this.grdShipments.DisplayLayout.Bands[0].ColumnFilters["FreightType"].FilterConditions.Add(FilterComparisionOperator.Equals,freightType);
				this.grdShipments.DisplayLayout.RefreshFilters();
                this.grdShipments.DisplayLayout.Bands[0].Columns["ShipperNumber"].Header.Caption = (freightType == "regular") ? "Vendor#" : "Agent#";
                this.grdShipments.DisplayLayout.Bands[0].Columns["ShipperName"].Header.Caption = (freightType == "regular") ? "Vendor" : "Agent";
				int index = (this.grdShipments.Selected.Rows.Count > 0) ? this.grdShipments.Selected.Rows[0].VisibleIndex : 0;
				if(this.grdShipments.Rows.VisibleRowCount > 0) {
					if(index >=0 && index < this.grdShipments.Rows.VisibleRowCount) 
						this.grdShipments.Rows.GetRowAtVisibleIndex(index).Selected = true;
					else
						this.grdShipments.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdShipments.Selected.Rows[0].Activate();
					this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdShipments.Selected.Rows[0]);
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnSortedDaysChanged(object sender, System.EventArgs e) {
			//Event handler for sorted days changed
			try {
                TsortGateway.SortedRange = Convert.ToInt32(this.updSortedDays.Value);
				this.mToolTip.SetToolTip(this.updSortedDays, "Include sorted freight for the last " + this.updSortedDays.Value.ToString() + " days.");
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
        private void OnFreightChanged(object sender,System.EventArgs e) {
            //Event handler for change in inbound freight collection
            //No visible rows:							no selection
            //Visible rows; no prior selection:			select row at visible index = 0
            //Visible rows; prior selection visible:	select row of prior selection
            //Visible rows; prior selection hidden:		select row at visible index = 0
            try {
                //Select a freight entry (if any are visible)
                this.mMessageMgr.AddMessage("Loading freight for " + this.mTerminalName + "...");
                int index = -1;
                if (this.grdShipments.Rows.VisibleRowCount > 0) {
                    index = 0;
                    if (this.mSelectedFreight != null) {
                        //Determine index of last freight selection if still visible
                        for (int i = 0;i < this.grdShipments.Rows.Count;i++) {
                            if (this.grdShipments.Rows[i].Cells["FreightID"].Value.ToString() == this.mSelectedFreight.FreightID) {
                                if (this.grdShipments.Rows[i].VisibleIndex > 0)
                                    index = this.grdShipments.Rows[i].VisibleIndex;
                                break;
                            }
                        }
                    }
                    this.grdShipments.Rows.GetRowAtVisibleIndex(index).Selected = true;
                    this.grdShipments.Selected.Rows[0].Activate();
                    this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdShipments.Selected.Rows[0]);
                }
                else
                    OnFreightSelected(null,null);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFreightSelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected freight (inbound shipment)
			try {
				//Clear reference to prior shipment object
				this.mSelectedFreight = null;
				if(this.grdShipments.Selected.Rows.Count > 0) {
					//Get a shipment object for the selected shipment
					string freightID = this.grdShipments.Selected.Rows[0].Cells["FreightID"].Value.ToString();
                    this.mSelectedFreight = TsortGateway.GetInboundShipment(freightID);
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
        #endregion
        #region Assignment Services: OnAssignmentsChanged(), OnAssignmentSelected(), OnAssignmentHistoryChanged(), OnAssignmentHistorySelected()
        private void OnAssignmentsChanged(object sender,System.EventArgs e) {
            //Event handler for change in station assignments collection
            //No visible rows:							no selection
            //Visible rows; no prior selection:			select row at visible index = 0
            //Visible rows; prior selection removed:	select row with nearest station number
            try {
                this.mMessageMgr.AddMessage("Loading station assignments for " + this.mTerminalName + "...");
                this.grdAssignments.Refresh();
                Application.DoEvents();
                int index = -1;
                if (this.grdAssignments.Rows.VisibleRowCount > 0) {
                    index = 0;
                    if (this.mSelectedAssignment != null) {
                        //Determine index of next visible row; if at end, take last
                        int i = 0;
                        for (i = 0;i < this.grdAssignments.Rows.Count;i++) {
                            if (Convert.ToInt32(this.grdAssignments.Rows[i].Cells["StationNumber"].Value) >= Convert.ToInt32(this.mSelectedAssignment.SortStation.Number)) {
                                index = this.grdAssignments.Rows[i].VisibleIndex;
                                break;
                            }
                        }
                        if (i == this.grdAssignments.Rows.Count && index == 0) index = this.grdAssignments.Rows.VisibleRowCount - 1;
                    }
                    this.grdAssignments.Rows.GetRowAtVisibleIndex(index).Selected = true;
                    this.grdAssignments.Selected.Rows[0].Activate();
                    this.grdAssignments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(this.grdAssignments.Selected.Rows[0]);
                }
                else
                    OnAssignmentSelected(null,null);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnAssignmentSelected(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected assignment
			try {
				//Create new instance of StationAssignmnet after selection changed
				this.mSelectedAssignment = null;
				if(this.grdAssignments.Selected.Rows.Count > 0) {
                    Workstation workstation = new Workstation();
                    workstation.TerminalID = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["TerminalID"].Value);
                    workstation.WorkStationID = this.grdAssignments.Selected.Rows[0].Cells["WorkStationID"].Value.ToString();
                    workstation.Number = this.grdAssignments.Selected.Rows[0].Cells["StationNumber"].Value.ToString();
                    InboundShipment shipment = new InboundShipment();
                    shipment.TerminalID = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["TerminalID"].Value);
                    shipment.FreightID = this.grdAssignments.Selected.Rows[0].Cells["FreightID"].Value.ToString();
                    shipment.FreightType = this.grdAssignments.Selected.Rows[0].Cells["FreightType"].Value.ToString();
                    shipment.TDSNumber = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["TDSNumber"].Value);
                    int sortTypeID = Convert.ToInt32(this.grdAssignments.Selected.Rows[0].Cells["SortTypeID"].Value);
                    StationAssignment assignment = new StationAssignment();
                    assignment.InboundFreight = shipment;
                    assignment.SortStation = workstation;
                    assignment.SortTypeID = sortTypeID;
                    this.mSelectedAssignment = assignment;
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
        private void OnAssignmentHistoryChanged(object sender,System.EventArgs e) {
            //Event handler for change in station assignments history collection
            try {
                this.mMessageMgr.AddMessage("Updating assignment history...");
                this.grdHistory.Refresh();
                Application.DoEvents();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnAssignmentHistorySelected(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for change in history selection
            setUserServices();
        }
        #endregion
        #region Grid Support: OnGridMouseDown()
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Ensure focus when user mouses (embedded child objects sometimes hold focus)
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();

                //Determine grid element pointed to by the mouse
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if (uiElement != null) {
                    //Determine if user selected a grid row
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if (context != null) {
                        //Row was selected- if mouse button is:
                        // left: forward to mouse move event handler
                        //right: clear all (multi-)selected rows and select a single row
                        if (e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if (e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)context;
                            if (!row.Selected) grid.Selected.Rows.Clear();
                            row.Selected = true;
                            row.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if (uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) {
                            grid.Selected.Rows.Clear();
                            grid.ActiveRow = null;
                        }
                        else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if (grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
		#endregion
		#region User Services: OnItemClick(), OnHelpMenuClick()
		private void OnItemClick(object sender, System.EventArgs e) {
			//Menu services
            dlgAssignmentDetail dlgAssignment = null;
            DialogResult res = DialogResult.None;
            bool bSorted = true,hasAssignments = false;
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch (item.Name) {
					case "msFileNew":
					case "tsNew":
					    break;
					case "msFileOpen":
					case "tsOpen":
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
                        dlgSave.FileName = this.mTerminalName + ", " + DateTime.Today.ToLongDateString();
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
							Application.DoEvents();
                            TsortGateway.InboundFreight.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
						}
						break;
					case "msFileSetup":	UltraGridPrinter.PageSettings(); break;
					case "msFilePrint":
                        if(this.grdShipments.Focused)
                            UltraGridPrinter.Print(this.grdShipments,this.mTerminalName.ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString(),true);
                        else if(this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused)
                            UltraGridPrinter.Print(this.grdHistory,this.mTerminalName.ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString(),true);
                        break;
                    case "tsPrint":
                        if(this.grdShipments.Focused)
                            UltraGridPrinter.Print(this.grdShipments, this.mTerminalName.ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString(), false);
                        else if (this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused)
                            UltraGridPrinter.Print(this.grdHistory, this.mTerminalName.ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString(), false);
                        break;
                    case "msFilePreview":
                    case "tsPreview":
                        if(this.grdShipments.Focused)
                            UltraGridPrinter.PrintPreview(this.grdShipments,this.mTerminalName.ToUpper() + " FREIGHT , " + DateTime.Today.ToLongDateString());
                        else if(this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused)
                            UltraGridPrinter.PrintPreview(this.grdHistory,this.mTerminalName.ToUpper() + " ASSIGNMENT HISTORY , " + DateTime.Today.ToLongDateString());
                        break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
					case "msEditSearch":	
					case "tsSearch":
                        this.txtSearch.Focus(); 
					    break;
					case "msViewRefresh":
                    case "tsRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        TsortGateway.RefreshFreight();
                        TsortGateway.RefreshStationAssignments();
						break;
                    case "csFRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        TsortGateway.RefreshFreight();
                        break;
                    case "csARefresh":
                        this.Cursor = Cursors.WaitCursor;
                        TsortGateway.RefreshStationAssignments();
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewHistory":
                        this.msViewHistory.Checked = !this.msViewHistory.Checked;
                        if(this.msViewHistory.Checked) {
                            if(this.tabHistory.Parent == null) this.tabAssignments.TabPages.Add(this.tabHistory);
                        }
                        else
                            this.tabAssignments.TabPages.Remove(this.tabHistory);
                        break;
					case "msViewToolbar":		this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
					case "msViewStatusBar":	this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
					case "msFreightAssign":
					case "csFAssign":
                    case "tsAssign":
                        //Assign shipment to one or more sort stations
						if(this.mSelectedFreight.IsSortable) {
							dlgAssignment = new dlgAssignmentDetail(DialogActionEnum.DialogActionAssign, this.mSelectedFreight, "");
							res = dlgAssignment.ShowDialog(this);
                            if (res == DialogResult.OK) TsortGateway.RefreshFreight();
						}
						else
							MessageBox.Show("Freight cannot be assigned because all TDS arrival information has not been entered.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
						break;
					case "msFreightUnassign":
					case "csFUnassign":
                    case "tsUnassign":
                        //Unassign shipment from all applicable stations
						res=DialogResult.Cancel;
                        dlgAssignment = new dlgAssignmentDetail(DialogActionEnum.DialogActionUnassignAny,this.mSelectedFreight,"");
						res = dlgAssignment.ShowDialog(this);
						if(res == DialogResult.OK) {
							//If no other assignments for this freight and NOT 'sorted', allow user to set freight status to sorted
                            TsortGateway.RefreshStationAssignments();
                            bSorted = TsortGateway.IsSortStopped(this.mSelectedFreight);
                            hasAssignments = TsortGateway.StationAssignments.StationFreightAssignmentTable.Select("FreightID = '" + this.mSelectedFreight.FreightID + "'").Length > 0;
                            if (!hasAssignments && !bSorted) {
								res = MessageBox.Show(this, "Selected freight is not being sorted on any stations. Would you like to stop sort for this freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if(res == DialogResult.Yes) {
									this.Cursor = Cursors.WaitCursor;
                                    if (TsortGateway.StopSort(this.mSelectedFreight)) {
                                        MessageBox.Show("Sorting was stopped for the selected freight.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                        TsortGateway.RefreshFreight();
                                    }
                                    else
                                        MessageBox.Show("Sorting could not be stopped for selected freight.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Warning);
								}
							}
                        }
                        break;
					case "msFreightStopSort":
					case "csFStopSort":
                    case "tsStopSort":
                        //Stop sort for the selected shipment
						res = MessageBox.Show(this, "Stop sorting the selected freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
						if(res==DialogResult.Yes) {
							this.Cursor = Cursors.WaitCursor;
                            if (TsortGateway.StopSort(this.mSelectedFreight)) {
                                MessageBox.Show("Sorting was stopped for the selected freight.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                                TsortGateway.RefreshFreight();
                            }
                            else
                                MessageBox.Show("Sorting could not be stopped for selected freight.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        }
						break;
					case "msAssignmentsUnassign":
                    case "csAUnassign":
                    case "tsUnassign2":
                        //Unassign shipment from selected station
						res=DialogResult.Cancel;
						InboundShipment shipment = this.mSelectedAssignment.InboundFreight;
                        dlgAssignment = new dlgAssignmentDetail(DialogActionEnum.DialogActionUnassign,shipment,this.mSelectedAssignment.SortStation.WorkStationID);
						res = dlgAssignment.ShowDialog(this);
						if(res == DialogResult.OK) {
							//If no other assignemnts for this freight and NOT 'sorted', allow user to set freight status to sorted
                            TsortGateway.RefreshStationAssignments();
                            bSorted = TsortGateway.IsSortStopped(shipment);
                            hasAssignments = TsortGateway.StationAssignments.StationFreightAssignmentTable.Select("FreightID = '" + shipment.FreightID + "'").Length > 0;
                            if (!hasAssignments && !bSorted) { 
								//Select associated shipment
								foreach(UltraGridRow row in this.grdShipments.Rows) {
									if(row.Cells["FreightID"].Value.ToString() == shipment.FreightID) {
										row.Selected = true;
										this.grdShipments.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(row);
										break;
									}
								}
								res = MessageBox.Show(this, "Freight #" + shipment.TDSNumber + " is not being sorted on any stations. Would you like to stop sort for this freight?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
								if(res == DialogResult.Yes) this.msFreightStopSort.PerformClick();
							}
                        }
						break;
                    case "msAssignmentsClearHistory":     
                    case "csAClear":
                        TsortGateway.StationAssignmentHistory.Clear(); 
                        break;
					case "msToolsConfig": App.ShowConfig(); break;
					case "msHelpAbout": new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
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
			//Set user services
			bool canAssign=false, canUnassign=false, canStopSorting=false, canRemove=false;
            try {
				//Set menu states
                bool isFreight = (this.grdShipments.Focused);
                bool isCurrent = (this.tabAssignments.SelectedTab.Name == this.tabRealTime.Name && this.grdAssignments.Focused);
                bool isHistory = (this.tabAssignments.SelectedTab.Name == this.tabHistory.Name && this.grdHistory.Focused);
                if ((RoleServiceGateway.IsTsortSupervisor || RoleServiceGateway.IsTsortClerk) && isFreight && this.mSelectedFreight != null) {
                    bool hasAssignments = TsortGateway.HasAssignments(this.mSelectedFreight.FreightID);
                    canAssign = true;
                    canUnassign = hasAssignments;
                    canStopSorting = (this.mSelectedFreight.Status.ToLower() == "sorting" && !hasAssignments);
				}
                canRemove = ((RoleServiceGateway.IsTsortSupervisor || RoleServiceGateway.IsTsortClerk) && isCurrent && this.mSelectedAssignment != null);
				
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
				this.msFileSave.Enabled = this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = isFreight;
				this.msFileSetup.Enabled = true;
                this.msFilePreview.Enabled = isFreight || isHistory;
                this.msFilePrint.Enabled = this.tsPrint.Enabled = isFreight || isHistory;
				this.msFileExit.Enabled = true;
                this.msViewRefresh.Enabled = this.tsRefresh.Enabled = this.csARefresh.Enabled = true;    // isFreight || isCurrent;
				this.msFreightAssign.Enabled = this.csFAssign.Enabled = this.tsAssign.Enabled = canAssign;
				this.msFreightUnassign.Enabled = this.csFUnassign.Enabled = this.tsUnassign.Enabled = canUnassign;
				this.msFreightStopSort.Enabled = this.csFStopSort.Enabled = this.tsStopSort.Enabled = canStopSorting;
				this.msAssignmentsUnassign.Enabled = this.csAUnassign.Enabled = this.tsUnassign2.Enabled = canRemove;
                this.msAssignmentsClearHistory.Enabled = this.csAClear.Enabled = isHistory;
                this.msEditSearch.Enabled = this.txtSearch.Enabled = (this.grdShipments.Rows.VisibleRowCount > 0);
				this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;
				
                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(TsortGateway.ServiceState,TsortGateway.ServiceAddress));
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
                this.ssMain.User2Panel.Icon = null;
                this.ssMain.User2Panel.ToolTipText = "";
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
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
            catch(Exception) { }
        }
		#endregion
	}
}
