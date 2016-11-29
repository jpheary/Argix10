using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Freight;
using Argix.Security;
using Argix.Windows;

namespace Argix.Freight {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private UltraGridSvc mGridSvc = null, mGridSvcA = null;
        private TLAutoRefreshService mAutoRefreshSvc = null;
        private System.Windows.Forms.ToolTip mToolTip = null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
        #region Controls
		private System.ComponentModel.IContainer components;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdTLs;
		private System.Windows.Forms.TextBox txtTLCartons;
		private System.Windows.Forms.TextBox txtTLPallets;
		private System.Windows.Forms.TextBox txtTLWeight;
        private System.Windows.Forms.TextBox txtTLCube;
		private System.Windows.Forms.TextBox txtISACube;
		private System.Windows.Forms.TextBox txtGrandTotalWeight;
		private System.Windows.Forms.TextBox txtGrandTotalCube;
		private System.Windows.Forms.TextBox txtLoadWeightRatio;
		private System.Windows.Forms.TextBox txtTrailerCubeRatio;
		private System.Windows.Forms.Label lblSelectedTLs;
		private System.Windows.Forms.ComboBox cboTerminals;
		private System.Windows.Forms.GroupBox grpTotals;
		private System.Windows.Forms.Label _lblTrailerLoad;
		private System.Windows.Forms.Label _lblTotals;
		private System.Windows.Forms.Label _lblISA;
        private System.Windows.Forms.Label _TLS;
        private System.Windows.Forms.GroupBox grpLine;
		private System.Windows.Forms.GroupBox grpLine11;
        private System.Windows.Forms.GroupBox grpLine12;
		private System.Windows.Forms.Label _lblLoadCube;
		private System.Windows.Forms.Label _lblLoadWeight;
		private System.Windows.Forms.Label _lblTotalCube;
		private System.Windows.Forms.Label _lblTotalWeight;
		private System.Windows.Forms.Label _lblISAWeight;
		private System.Windows.Forms.Label _lblISACube;
		private System.Windows.Forms.Label _lblTLCube;
		private System.Windows.Forms.Label _lblTLWeight;
		private System.Windows.Forms.Label _lblTLPallets;
        private System.Windows.Forms.Label _lblTLCartons;
        private Argix.Windows.ArgixStatusBar ssMain;
        private System.Windows.Forms.TextBox txtSearchSort;
        private System.Windows.Forms.PictureBox _picSearch;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSave;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFilePageSetup;
        private ToolStripMenuItem msFilePreview;
        private ToolStripMenuItem msFilePrint;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msEditSearch;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripButton tsSave;
        private ToolStripButton tsPrint;
        private ToolStripButton tsSearch;
        private ToolStripButton tsRefresh;
        private ToolStripSeparator tsSep1;
        private ToolStripSeparator tsSep2;
        private ToolStripMenuItem msFileSaveAs;
        private FreightDataset mTerminals;
        private FreightDataset mTLs;
        private TabControl tabMain;
        private TabPage tabTLs;
        private TabPage tabAgents;
        private UltraGrid grdAgentSummary;
        private FreightDataset mTLAgs;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private ToolStripButton tsExport;
        private ToolStripSeparator tsSep3;
        private ToolStripMenuItem msFileExport;
        private ToolStripMenuItem msViewTLDetail;
        private ToolStripSeparator msViewSep3;
        private ToolStripButton tsTLDetail;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csExport;
        private ToolStripMenuItem csRefresh;
        private ToolStripMenuItem csTLDetail;
        private ToolStripSeparator tsSep4;
        private ToolStripSeparator csSep1;
        private NumericUpDown updISAWeight;
        #endregion

        //Interface
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Logistics " + App.Product;

                buildHelpMenu();
				#region Splash Screen Support
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#endregion
				#region Set window docking
				this.grdTLs.Controls.AddRange(new Control[]{this.cboTerminals, this._picSearch, this.txtSearchSort});
				this.cboTerminals.Top = this.txtSearchSort.Top = 1;
                this.cboTerminals.Left = 55;
                this._picSearch.Top = 3;
				this._picSearch.Left = this.grdTLs.Width - _picSearch.Width - this.txtSearchSort.Width - 5;
				this.txtSearchSort.Left = this.grdTLs.Width - this.txtSearchSort.Width - 2;
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tabMain,this.tsMain,this.msMain,this.ssMain });
                #endregion
				
				//Create data and UI servicesv
				this.mGridSvc = new UltraGridSvc(this.grdTLs, this.txtSearchSort);
                this.mGridSvcA = new UltraGridSvc(this.grdAgentSummary);
                this.mAutoRefreshSvc = new TLAutoRefreshService(this);
                this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 1000, 3000);
			}
            catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure",ex); }
        }
		protected override void Dispose( bool disposing ) { if(disposing) { if(components!=null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TLTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn95 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn96 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn97 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn98 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn99 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn100 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn101 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn102 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn103 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn104 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn105 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn106 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn107 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn108 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn109 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WeightPercent");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn111 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubePercent");
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TLTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WeightPercent");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubePercent");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            this.grdTLs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csExport = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csTLDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.cboTerminals = new System.Windows.Forms.ComboBox();
            this.mTerminals = new Argix.FreightDataset();
            this._picSearch = new System.Windows.Forms.PictureBox();
            this.txtSearchSort = new System.Windows.Forms.TextBox();
            this.mTLs = new Argix.FreightDataset();
            this.grpTotals = new System.Windows.Forms.GroupBox();
            this.updISAWeight = new System.Windows.Forms.NumericUpDown();
            this.grpLine12 = new System.Windows.Forms.GroupBox();
            this.grpLine11 = new System.Windows.Forms.GroupBox();
            this.grpLine = new System.Windows.Forms.GroupBox();
            this.lblSelectedTLs = new System.Windows.Forms.Label();
            this._lblLoadCube = new System.Windows.Forms.Label();
            this._lblLoadWeight = new System.Windows.Forms.Label();
            this._lblTrailerLoad = new System.Windows.Forms.Label();
            this._lblTotalCube = new System.Windows.Forms.Label();
            this._lblTotalWeight = new System.Windows.Forms.Label();
            this._lblTotals = new System.Windows.Forms.Label();
            this._lblISAWeight = new System.Windows.Forms.Label();
            this._lblISA = new System.Windows.Forms.Label();
            this._TLS = new System.Windows.Forms.Label();
            this.txtTrailerCubeRatio = new System.Windows.Forms.TextBox();
            this.txtLoadWeightRatio = new System.Windows.Forms.TextBox();
            this.txtGrandTotalCube = new System.Windows.Forms.TextBox();
            this.txtGrandTotalWeight = new System.Windows.Forms.TextBox();
            this.txtTLPallets = new System.Windows.Forms.TextBox();
            this.txtISACube = new System.Windows.Forms.TextBox();
            this.txtTLCartons = new System.Windows.Forms.TextBox();
            this.txtTLCube = new System.Windows.Forms.TextBox();
            this.txtTLWeight = new System.Windows.Forms.TextBox();
            this._lblISACube = new System.Windows.Forms.Label();
            this._lblTLCube = new System.Windows.Forms.Label();
            this._lblTLWeight = new System.Windows.Forms.Label();
            this._lblTLPallets = new System.Windows.Forms.Label();
            this._lblTLCartons = new System.Windows.Forms.Label();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewTLDetail = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep3 = new System.Windows.Forms.ToolStripSeparator();
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
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsTLDetail = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabTLs = new System.Windows.Forms.TabPage();
            this.tabAgents = new System.Windows.Forms.TabPage();
            this.grdAgentSummary = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mTLAgs = new Argix.FreightDataset();
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).BeginInit();
            this.grdTLs.SuspendLayout();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._picSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLs)).BeginInit();
            this.grpTotals.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updISAWeight)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabTLs.SuspendLayout();
            this.tabAgents.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdAgentSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLAgs)).BeginInit();
            this.SuspendLayout();
            // 
            // grdTLs
            // 
            this.grdTLs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTLs.ContextMenuStrip = this.csMain;
            this.grdTLs.Controls.Add(this.cboTerminals);
            this.grdTLs.Controls.Add(this._picSearch);
            this.grdTLs.Controls.Add(this.txtSearchSort);
            this.grdTLs.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTLs.DataMember = "TLTable";
            this.grdTLs.DataSource = this.mTLs;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Appearance = appearance1;
            ultraGridColumn16.Header.VisiblePosition = 0;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn95.Header.VisiblePosition = 1;
            ultraGridColumn95.Width = 50;
            ultraGridColumn2.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn2.Header.Caption = "Agent#";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 75;
            ultraGridColumn96.Header.Caption = "Agent";
            ultraGridColumn96.Header.VisiblePosition = 3;
            ultraGridColumn96.Hidden = true;
            ultraGridColumn96.Width = 150;
            ultraGridColumn97.Header.Caption = "TL#";
            ultraGridColumn97.Header.VisiblePosition = 4;
            ultraGridColumn97.Width = 75;
            ultraGridColumn98.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn98.Format = "MM/dd/yyyy";
            ultraGridColumn98.Header.Caption = "TL Date";
            ultraGridColumn98.Header.VisiblePosition = 5;
            ultraGridColumn98.Width = 100;
            ultraGridColumn99.Header.Caption = "Close#";
            ultraGridColumn99.Header.VisiblePosition = 6;
            ultraGridColumn99.Width = 50;
            ultraGridColumn100.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn100.Header.Caption = "Client#";
            ultraGridColumn100.Header.VisiblePosition = 7;
            ultraGridColumn100.Width = 75;
            ultraGridColumn101.Header.Caption = "Client";
            ultraGridColumn101.Header.VisiblePosition = 8;
            ultraGridColumn101.Width = 200;
            ultraGridColumn102.Header.VisiblePosition = 9;
            ultraGridColumn102.Hidden = true;
            ultraGridColumn103.Header.VisiblePosition = 10;
            ultraGridColumn103.Hidden = true;
            ultraGridColumn104.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn104.Header.VisiblePosition = 11;
            ultraGridColumn104.Width = 50;
            ultraGridColumn105.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn105.Header.Caption = "Sm.Lane";
            ultraGridColumn105.Header.VisiblePosition = 12;
            ultraGridColumn105.Width = 50;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn106.CellAppearance = appearance2;
            ultraGridColumn106.Format = "#0";
            ultraGridColumn106.Header.VisiblePosition = 13;
            ultraGridColumn106.Width = 50;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn107.CellAppearance = appearance3;
            ultraGridColumn107.Format = "#0";
            ultraGridColumn107.Header.VisiblePosition = 14;
            ultraGridColumn107.Width = 50;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn108.CellAppearance = appearance4;
            ultraGridColumn108.Format = "#0";
            ultraGridColumn108.Header.VisiblePosition = 15;
            ultraGridColumn108.Width = 75;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn109.CellAppearance = appearance5;
            ultraGridColumn109.Format = "#0";
            ultraGridColumn109.Header.VisiblePosition = 17;
            ultraGridColumn109.Width = 75;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn110.CellAppearance = appearance6;
            ultraGridColumn110.Format = "#0";
            ultraGridColumn110.Header.Caption = "Weight%";
            ultraGridColumn110.Header.VisiblePosition = 16;
            ultraGridColumn110.Width = 75;
            appearance7.TextHAlignAsString = "Right";
            ultraGridColumn111.CellAppearance = appearance7;
            ultraGridColumn111.Format = "#0";
            ultraGridColumn111.Header.Caption = "Cube%";
            ultraGridColumn111.Header.VisiblePosition = 18;
            ultraGridColumn111.Width = 75;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn16,
            ultraGridColumn95,
            ultraGridColumn2,
            ultraGridColumn96,
            ultraGridColumn97,
            ultraGridColumn98,
            ultraGridColumn99,
            ultraGridColumn100,
            ultraGridColumn101,
            ultraGridColumn102,
            ultraGridColumn103,
            ultraGridColumn104,
            ultraGridColumn105,
            ultraGridColumn106,
            ultraGridColumn107,
            ultraGridColumn108,
            ultraGridColumn109,
            ultraGridColumn110,
            ultraGridColumn111});
            this.grdTLs.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTLs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance8.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.SizeInPoints = 9F;
            appearance8.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance8.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.CaptionAppearance = appearance8;
            this.grdTLs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTLs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.grdTLs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTLs.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance10.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTLs.DisplayLayout.Override.RowAppearance = appearance10;
            this.grdTLs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdTLs.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTLs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTLs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTLs.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTLs.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTLs.Location = new System.Drawing.Point(222, 6);
            this.grdTLs.Name = "grdTLs";
            this.grdTLs.Size = new System.Drawing.Size(636, 215);
            this.grdTLs.TabIndex = 2;
            this.grdTLs.Text = "Open TL\'s for ";
            this.grdTLs.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdTLs.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdTLs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTLsSelected);
            this.grdTLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csExport,
            this.csSep1,
            this.csRefresh,
            this.csTLDetail});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(135, 76);
            // 
            // csExport
            // 
            this.csExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.csExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csExport.Name = "csExport";
            this.csExport.Size = new System.Drawing.Size(134, 22);
            this.csExport.Text = "Export...";
            this.csExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(131, 6);
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(134, 22);
            this.csRefresh.Text = "Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csTLDetail
            // 
            this.csTLDetail.Image = global::Argix.Properties.Resources.OrgChart;
            this.csTLDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTLDetail.Name = "csTLDetail";
            this.csTLDetail.Size = new System.Drawing.Size(134, 22);
            this.csTLDetail.Text = "TL Details...";
            this.csTLDetail.Click += new System.EventHandler(this.OnItemClick);
            // 
            // cboTerminals
            // 
            this.cboTerminals.DataSource = this.mTerminals;
            this.cboTerminals.DisplayMember = "TerminalTable.Description";
            this.cboTerminals.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminals.Location = new System.Drawing.Point(90, 1);
            this.cboTerminals.MaxDropDownItems = 5;
            this.cboTerminals.Name = "cboTerminals";
            this.cboTerminals.Size = new System.Drawing.Size(192, 21);
            this.cboTerminals.TabIndex = 1;
            this.cboTerminals.ValueMember = "TerminalTable.TerminalID";
            this.cboTerminals.SelectionChangeCommitted += new System.EventHandler(this.OnTerminalSelected);
            // 
            // mTerminals
            // 
            this.mTerminals.DataSetName = "FreightDataset";
            this.mTerminals.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _picSearch
            // 
            this._picSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._picSearch.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this._picSearch.Image = ((System.Drawing.Image)(resources.GetObject("_picSearch.Image")));
            this._picSearch.Location = new System.Drawing.Point(518, 2);
            this._picSearch.Name = "_picSearch";
            this._picSearch.Size = new System.Drawing.Size(18, 18);
            this._picSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this._picSearch.TabIndex = 7;
            this._picSearch.TabStop = false;
            // 
            // txtSearchSort
            // 
            this.txtSearchSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSort.Location = new System.Drawing.Point(540, 1);
            this.txtSearchSort.Name = "txtSearchSort";
            this.txtSearchSort.Size = new System.Drawing.Size(96, 20);
            this.txtSearchSort.TabIndex = 6;
            this.txtSearchSort.TextChanged += new System.EventHandler(this.OnSearchValueChanged);
            // 
            // mTLs
            // 
            this.mTLs.DataSetName = "FreightDataset";
            this.mTLs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpTotals
            // 
            this.grpTotals.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grpTotals.Controls.Add(this.updISAWeight);
            this.grpTotals.Controls.Add(this.grpLine12);
            this.grpTotals.Controls.Add(this.grpLine11);
            this.grpTotals.Controls.Add(this.grpLine);
            this.grpTotals.Controls.Add(this.lblSelectedTLs);
            this.grpTotals.Controls.Add(this._lblLoadCube);
            this.grpTotals.Controls.Add(this._lblLoadWeight);
            this.grpTotals.Controls.Add(this._lblTrailerLoad);
            this.grpTotals.Controls.Add(this._lblTotalCube);
            this.grpTotals.Controls.Add(this._lblTotalWeight);
            this.grpTotals.Controls.Add(this._lblTotals);
            this.grpTotals.Controls.Add(this._lblISAWeight);
            this.grpTotals.Controls.Add(this._lblISA);
            this.grpTotals.Controls.Add(this._TLS);
            this.grpTotals.Controls.Add(this.txtTrailerCubeRatio);
            this.grpTotals.Controls.Add(this.txtLoadWeightRatio);
            this.grpTotals.Controls.Add(this.txtGrandTotalCube);
            this.grpTotals.Controls.Add(this.txtGrandTotalWeight);
            this.grpTotals.Controls.Add(this.txtTLPallets);
            this.grpTotals.Controls.Add(this.txtISACube);
            this.grpTotals.Controls.Add(this.txtTLCartons);
            this.grpTotals.Controls.Add(this.txtTLCube);
            this.grpTotals.Controls.Add(this.txtTLWeight);
            this.grpTotals.Controls.Add(this._lblISACube);
            this.grpTotals.Controls.Add(this._lblTLCube);
            this.grpTotals.Controls.Add(this._lblTLWeight);
            this.grpTotals.Controls.Add(this._lblTLPallets);
            this.grpTotals.Controls.Add(this._lblTLCartons);
            this.grpTotals.Location = new System.Drawing.Point(6, 6);
            this.grpTotals.Name = "grpTotals";
            this.grpTotals.Size = new System.Drawing.Size(210, 215);
            this.grpTotals.TabIndex = 4;
            this.grpTotals.TabStop = false;
            this.grpTotals.Text = "Totals";
            // 
            // updISAWeight
            // 
            this.updISAWeight.Location = new System.Drawing.Point(105, 189);
            this.updISAWeight.Maximum = new decimal(new int[] {
            9999999,
            0,
            0,
            0});
            this.updISAWeight.Name = "updISAWeight";
            this.updISAWeight.Size = new System.Drawing.Size(96, 20);
            this.updISAWeight.TabIndex = 29;
            this.updISAWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.updISAWeight.ThousandsSeparator = true;
            this.updISAWeight.UpDownAlign = System.Windows.Forms.LeftRightAlignment.Left;
            this.updISAWeight.ValueChanged += new System.EventHandler(this.OnISAWeightChanged);
            // 
            // grpLine12
            // 
            this.grpLine12.BackColor = System.Drawing.SystemColors.Control;
            this.grpLine12.Location = new System.Drawing.Point(9, 330);
            this.grpLine12.Name = "grpLine12";
            this.grpLine12.Size = new System.Drawing.Size(192, 6);
            this.grpLine12.TabIndex = 28;
            this.grpLine12.TabStop = false;
            // 
            // grpLine11
            // 
            this.grpLine11.BackColor = System.Drawing.SystemColors.Control;
            this.grpLine11.Location = new System.Drawing.Point(9, 336);
            this.grpLine11.Name = "grpLine11";
            this.grpLine11.Size = new System.Drawing.Size(192, 6);
            this.grpLine11.TabIndex = 27;
            this.grpLine11.TabStop = false;
            // 
            // grpLine
            // 
            this.grpLine.BackColor = System.Drawing.SystemColors.Control;
            this.grpLine.Location = new System.Drawing.Point(9, 240);
            this.grpLine.Name = "grpLine";
            this.grpLine.Size = new System.Drawing.Size(192, 6);
            this.grpLine.TabIndex = 26;
            this.grpLine.TabStop = false;
            // 
            // lblSelectedTLs
            // 
            this.lblSelectedTLs.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSelectedTLs.Location = new System.Drawing.Point(105, 21);
            this.lblSelectedTLs.Name = "lblSelectedTLs";
            this.lblSelectedTLs.Size = new System.Drawing.Size(96, 21);
            this.lblSelectedTLs.TabIndex = 17;
            this.lblSelectedTLs.Text = "0";
            this.lblSelectedTLs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLoadCube
            // 
            this._lblLoadCube.Location = new System.Drawing.Point(21, 399);
            this._lblLoadCube.Name = "_lblLoadCube";
            this._lblLoadCube.Size = new System.Drawing.Size(84, 18);
            this._lblLoadCube.TabIndex = 25;
            this._lblLoadCube.Text = "Cube (%)";
            this._lblLoadCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLoadWeight
            // 
            this._lblLoadWeight.Location = new System.Drawing.Point(21, 375);
            this._lblLoadWeight.Name = "_lblLoadWeight";
            this._lblLoadWeight.Size = new System.Drawing.Size(84, 18);
            this._lblLoadWeight.TabIndex = 24;
            this._lblLoadWeight.Text = "Weight (%)";
            this._lblLoadWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTrailerLoad
            // 
            this._lblTrailerLoad.Location = new System.Drawing.Point(6, 348);
            this._lblTrailerLoad.Name = "_lblTrailerLoad";
            this._lblTrailerLoad.Size = new System.Drawing.Size(192, 18);
            this._lblTrailerLoad.TabIndex = 23;
            this._lblTrailerLoad.Text = "Trailer Load % (53 foot)";
            this._lblTrailerLoad.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotalCube
            // 
            this._lblTotalCube.Location = new System.Drawing.Point(21, 303);
            this._lblTotalCube.Name = "_lblTotalCube";
            this._lblTotalCube.Size = new System.Drawing.Size(84, 18);
            this._lblTotalCube.TabIndex = 22;
            this._lblTotalCube.Text = "Cube (in3)";
            this._lblTotalCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotalWeight
            // 
            this._lblTotalWeight.Location = new System.Drawing.Point(21, 276);
            this._lblTotalWeight.Name = "_lblTotalWeight";
            this._lblTotalWeight.Size = new System.Drawing.Size(84, 18);
            this._lblTotalWeight.TabIndex = 21;
            this._lblTotalWeight.Text = "Weight (lbs)";
            this._lblTotalWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTotals
            // 
            this._lblTotals.Location = new System.Drawing.Point(6, 249);
            this._lblTotals.Name = "_lblTotals";
            this._lblTotals.Size = new System.Drawing.Size(192, 18);
            this._lblTotals.TabIndex = 20;
            this._lblTotals.Text = "= Total";
            this._lblTotals.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblISAWeight
            // 
            this._lblISAWeight.Location = new System.Drawing.Point(21, 189);
            this._lblISAWeight.Name = "_lblISAWeight";
            this._lblISAWeight.Size = new System.Drawing.Size(84, 18);
            this._lblISAWeight.TabIndex = 19;
            this._lblISAWeight.Text = "Weight (lbs)";
            this._lblISAWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblISA
            // 
            this._lblISA.Location = new System.Drawing.Point(6, 162);
            this._lblISA.Name = "_lblISA";
            this._lblISA.Size = new System.Drawing.Size(192, 18);
            this._lblISA.TabIndex = 18;
            this._lblISA.Text = "+  ISA";
            this._lblISA.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _TLS
            // 
            this._TLS.Location = new System.Drawing.Point(6, 21);
            this._TLS.Name = "_TLS";
            this._TLS.Size = new System.Drawing.Size(96, 18);
            this._TLS.TabIndex = 16;
            this._TLS.Text = "Selected TL\'s";
            this._TLS.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtTrailerCubeRatio
            // 
            this.txtTrailerCubeRatio.Location = new System.Drawing.Point(105, 399);
            this.txtTrailerCubeRatio.Name = "txtTrailerCubeRatio";
            this.txtTrailerCubeRatio.ReadOnly = true;
            this.txtTrailerCubeRatio.Size = new System.Drawing.Size(96, 20);
            this.txtTrailerCubeRatio.TabIndex = 15;
            this.txtTrailerCubeRatio.TabStop = false;
            this.txtTrailerCubeRatio.Text = "0";
            this.txtTrailerCubeRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtLoadWeightRatio
            // 
            this.txtLoadWeightRatio.Location = new System.Drawing.Point(105, 375);
            this.txtLoadWeightRatio.Name = "txtLoadWeightRatio";
            this.txtLoadWeightRatio.ReadOnly = true;
            this.txtLoadWeightRatio.Size = new System.Drawing.Size(96, 20);
            this.txtLoadWeightRatio.TabIndex = 14;
            this.txtLoadWeightRatio.TabStop = false;
            this.txtLoadWeightRatio.Text = "0";
            this.txtLoadWeightRatio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrandTotalCube
            // 
            this.txtGrandTotalCube.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtGrandTotalCube.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtGrandTotalCube.Location = new System.Drawing.Point(105, 303);
            this.txtGrandTotalCube.Name = "txtGrandTotalCube";
            this.txtGrandTotalCube.ReadOnly = true;
            this.txtGrandTotalCube.Size = new System.Drawing.Size(96, 20);
            this.txtGrandTotalCube.TabIndex = 13;
            this.txtGrandTotalCube.TabStop = false;
            this.txtGrandTotalCube.Text = "0";
            this.txtGrandTotalCube.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtGrandTotalWeight
            // 
            this.txtGrandTotalWeight.BackColor = System.Drawing.SystemColors.Highlight;
            this.txtGrandTotalWeight.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.txtGrandTotalWeight.Location = new System.Drawing.Point(105, 276);
            this.txtGrandTotalWeight.Name = "txtGrandTotalWeight";
            this.txtGrandTotalWeight.ReadOnly = true;
            this.txtGrandTotalWeight.Size = new System.Drawing.Size(96, 20);
            this.txtGrandTotalWeight.TabIndex = 12;
            this.txtGrandTotalWeight.TabStop = false;
            this.txtGrandTotalWeight.Text = "0";
            this.txtGrandTotalWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLPallets
            // 
            this.txtTLPallets.Location = new System.Drawing.Point(105, 75);
            this.txtTLPallets.Name = "txtTLPallets";
            this.txtTLPallets.ReadOnly = true;
            this.txtTLPallets.Size = new System.Drawing.Size(96, 20);
            this.txtTLPallets.TabIndex = 11;
            this.txtTLPallets.TabStop = false;
            this.txtTLPallets.Text = "0";
            this.txtTLPallets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtISACube
            // 
            this.txtISACube.Location = new System.Drawing.Point(105, 216);
            this.txtISACube.Name = "txtISACube";
            this.txtISACube.ReadOnly = true;
            this.txtISACube.Size = new System.Drawing.Size(96, 20);
            this.txtISACube.TabIndex = 10;
            this.txtISACube.TabStop = false;
            this.txtISACube.Text = "0";
            this.txtISACube.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLCartons
            // 
            this.txtTLCartons.Location = new System.Drawing.Point(105, 48);
            this.txtTLCartons.Name = "txtTLCartons";
            this.txtTLCartons.ReadOnly = true;
            this.txtTLCartons.Size = new System.Drawing.Size(96, 20);
            this.txtTLCartons.TabIndex = 9;
            this.txtTLCartons.TabStop = false;
            this.txtTLCartons.Text = "0";
            this.txtTLCartons.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLCube
            // 
            this.txtTLCube.Location = new System.Drawing.Point(105, 129);
            this.txtTLCube.Name = "txtTLCube";
            this.txtTLCube.ReadOnly = true;
            this.txtTLCube.Size = new System.Drawing.Size(96, 20);
            this.txtTLCube.TabIndex = 8;
            this.txtTLCube.TabStop = false;
            this.txtTLCube.Text = "0";
            this.txtTLCube.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtTLWeight
            // 
            this.txtTLWeight.Location = new System.Drawing.Point(105, 102);
            this.txtTLWeight.Name = "txtTLWeight";
            this.txtTLWeight.ReadOnly = true;
            this.txtTLWeight.Size = new System.Drawing.Size(96, 20);
            this.txtTLWeight.TabIndex = 7;
            this.txtTLWeight.TabStop = false;
            this.txtTLWeight.Text = "0";
            this.txtTLWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblISACube
            // 
            this._lblISACube.Location = new System.Drawing.Point(21, 216);
            this._lblISACube.Name = "_lblISACube";
            this._lblISACube.Size = new System.Drawing.Size(84, 18);
            this._lblISACube.TabIndex = 5;
            this._lblISACube.Text = "Cube (in3)";
            this._lblISACube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLCube
            // 
            this._lblTLCube.Location = new System.Drawing.Point(21, 129);
            this._lblTLCube.Name = "_lblTLCube";
            this._lblTLCube.Size = new System.Drawing.Size(84, 18);
            this._lblTLCube.TabIndex = 3;
            this._lblTLCube.Text = "Cube (in3)";
            this._lblTLCube.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLWeight
            // 
            this._lblTLWeight.Location = new System.Drawing.Point(21, 102);
            this._lblTLWeight.Name = "_lblTLWeight";
            this._lblTLWeight.Size = new System.Drawing.Size(84, 18);
            this._lblTLWeight.TabIndex = 2;
            this._lblTLWeight.Text = "Weight (lbs)";
            this._lblTLWeight.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLPallets
            // 
            this._lblTLPallets.Location = new System.Drawing.Point(21, 75);
            this._lblTLPallets.Name = "_lblTLPallets";
            this._lblTLPallets.Size = new System.Drawing.Size(84, 18);
            this._lblTLPallets.TabIndex = 1;
            this._lblTLPallets.Text = "Pallets";
            this._lblTLPallets.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblTLCartons
            // 
            this._lblTLCartons.Location = new System.Drawing.Point(21, 48);
            this._lblTLCartons.Name = "_lblTLCartons";
            this._lblTLCartons.Size = new System.Drawing.Size(84, 18);
            this._lblTLCartons.TabIndex = 0;
            this._lblTLCartons.Text = "Cartons";
            this._lblTLCartons.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 333);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(866, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 15;
            this.ssMain.TerminalText = "Terminal";
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
            this.msMain.Size = new System.Drawing.Size(866, 24);
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
            this.msFileExport,
            this.msFileSep2,
            this.msFilePageSetup,
            this.msFilePreview,
            this.msFilePrint,
            this.msFileSep3,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37, 20);
            this.msFile.Text = "&File";
            // 
            // msFileNew
            // 
            this.msFileNew.Image = global::Argix.Properties.Resources.Document;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(152, 22);
            this.msFileNew.Text = "New...";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.OpenFolder;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(152, 22);
            this.msFileOpen.Text = "Open...";
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
            this.msFileSave.Text = "Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.msFileSaveAs.Text = "Save As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.msFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExport.Name = "msFileExport";
            this.msFileExport.Size = new System.Drawing.Size(152, 22);
            this.msFileExport.Text = "Export...";
            this.msFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // msFilePageSetup
            // 
            this.msFilePageSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.msFilePageSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePageSetup.Name = "msFilePageSetup";
            this.msFilePageSetup.Size = new System.Drawing.Size(152, 22);
            this.msFilePageSetup.Text = "Page Setup...";
            this.msFilePageSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(152, 22);
            this.msFilePreview.Text = "Print Preveiw...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(152, 22);
            this.msFilePrint.Text = "Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(152, 22);
            this.msFileExit.Text = "Exit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEdit
            // 
            this.msEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msEditSearch});
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 20);
            this.msEdit.Text = "&Edit";
            // 
            // msEditSearch
            // 
            this.msEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.msEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditSearch.Name = "msEditSearch";
            this.msEditSearch.Size = new System.Drawing.Size(118, 22);
            this.msEditSearch.Text = "Search...";
            this.msEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewTLDetail,
            this.msViewSep2,
            this.msViewFont,
            this.msViewSep3,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 20);
            this.msView.Text = "&View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.msViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(129, 22);
            this.msViewRefresh.Text = "Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(126, 6);
            // 
            // msViewTLDetail
            // 
            this.msViewTLDetail.Image = global::Argix.Properties.Resources.OrgChart;
            this.msViewTLDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewTLDetail.Name = "msViewTLDetail";
            this.msViewTLDetail.Size = new System.Drawing.Size(129, 22);
            this.msViewTLDetail.Text = "TL Detail...";
            this.msViewTLDetail.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(126, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(129, 22);
            this.msViewFont.Text = "&Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep3
            // 
            this.msViewSep3.Name = "msViewSep3";
            this.msViewSep3.Size = new System.Drawing.Size(126, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(129, 22);
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(129, 22);
            this.msViewStatusBar.Text = "StatusBar";
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
            this.msToolsConfig.Image = global::Argix.Properties.Resources.XMLFile;
            this.msToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msToolsConfig.Name = "msToolsConfig";
            this.msToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.msToolsConfig.Text = "Configuration...";
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
            this.msHelpAbout.Size = new System.Drawing.Size(158, 22);
            this.msHelpAbout.Text = "About TLViewer";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(155, 6);
            this.msHelpSep1.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsOpen,
            this.tsSep1,
            this.tsSave,
            this.tsExport,
            this.tsSep2,
            this.tsPrint,
            this.tsSep3,
            this.tsSearch,
            this.tsSep4,
            this.tsRefresh,
            this.tsTLDetail});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(866, 54);
            this.tsMain.TabIndex = 6;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.Document;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 51);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New...";
            this.tsNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.OpenFolder;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 51);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open...";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 51);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save...";
            this.tsSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsExport
            // 
            this.tsExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.tsExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExport.Name = "tsExport";
            this.tsExport.Size = new System.Drawing.Size(44, 51);
            this.tsExport.Text = "Export";
            this.tsExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsExport.ToolTipText = "Export selected TLs...";
            this.tsExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 51);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print TLView...";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Find;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(36, 51);
            this.tsSearch.Text = "Find";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.ToolTipText = "Search...";
            this.tsSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 54);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 51);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh TL\'s";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsTLDetail
            // 
            this.tsTLDetail.Image = global::Argix.Properties.Resources.OrgChart;
            this.tsTLDetail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsTLDetail.Name = "tsTLDetail";
            this.tsTLDetail.Size = new System.Drawing.Size(57, 51);
            this.tsTLDetail.Text = "TL Detail";
            this.tsTLDetail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsTLDetail.ToolTipText = "Show TL details";
            this.tsTLDetail.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabTLs);
            this.tabMain.Controls.Add(this.tabAgents);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(100, 20);
            this.tabMain.Location = new System.Drawing.Point(0, 78);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(866, 255);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 7;
            // 
            // tabTLs
            // 
            this.tabTLs.BackColor = System.Drawing.SystemColors.Control;
            this.tabTLs.Controls.Add(this.grpTotals);
            this.tabTLs.Controls.Add(this.grdTLs);
            this.tabTLs.Location = new System.Drawing.Point(4, 4);
            this.tabTLs.Name = "tabTLs";
            this.tabTLs.Padding = new System.Windows.Forms.Padding(3);
            this.tabTLs.Size = new System.Drawing.Size(858, 227);
            this.tabTLs.TabIndex = 0;
            this.tabTLs.Text = "TLView";
            // 
            // tabAgents
            // 
            this.tabAgents.BackColor = System.Drawing.SystemColors.Control;
            this.tabAgents.Controls.Add(this.grdAgentSummary);
            this.tabAgents.Location = new System.Drawing.Point(4, 4);
            this.tabAgents.Name = "tabAgents";
            this.tabAgents.Padding = new System.Windows.Forms.Padding(3);
            this.tabAgents.Size = new System.Drawing.Size(858, 227);
            this.tabAgents.TabIndex = 1;
            this.tabAgents.Text = "Agent Summary";
            // 
            // grdAgentSummary
            // 
            this.grdAgentSummary.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdAgentSummary.DataMember = "TLTable";
            this.grdAgentSummary.DataSource = this.mTLAgs;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance11.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.Appearance = appearance11;
            ultraGridBand2.AddButtonCaption = "ClientViewTable";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Width = 50;
            ultraGridColumn4.Header.Caption = "Agent#";
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Width = 75;
            ultraGridColumn5.Header.Caption = "Agent";
            ultraGridColumn5.Header.VisiblePosition = 2;
            ultraGridColumn5.Width = 200;
            ultraGridColumn6.Header.Caption = "TL#";
            ultraGridColumn6.Header.VisiblePosition = 4;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn6.Width = 75;
            ultraGridColumn7.Header.Caption = "TL Date";
            ultraGridColumn7.Header.VisiblePosition = 5;
            ultraGridColumn7.Width = 75;
            ultraGridColumn8.Header.Caption = "Close#";
            ultraGridColumn8.Header.VisiblePosition = 6;
            ultraGridColumn8.Width = 50;
            ultraGridColumn9.Header.Caption = "Client#";
            ultraGridColumn9.Header.VisiblePosition = 7;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 75;
            ultraGridColumn10.Header.Caption = "Client";
            ultraGridColumn10.Header.VisiblePosition = 8;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 200;
            ultraGridColumn11.Header.Caption = "Ship To#";
            ultraGridColumn11.Header.VisiblePosition = 9;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn11.Width = 75;
            ultraGridColumn12.Header.Caption = "Ship To";
            ultraGridColumn12.Header.VisiblePosition = 10;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn12.Width = 200;
            ultraGridColumn13.Header.VisiblePosition = 11;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.Width = 50;
            ultraGridColumn14.Header.Caption = "S.Lane";
            ultraGridColumn14.Header.VisiblePosition = 12;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn14.Width = 50;
            ultraGridColumn15.Header.VisiblePosition = 13;
            ultraGridColumn15.Width = 50;
            ultraGridColumn17.Header.VisiblePosition = 14;
            ultraGridColumn17.Width = 50;
            ultraGridColumn18.Header.VisiblePosition = 15;
            ultraGridColumn18.Width = 75;
            ultraGridColumn19.Header.VisiblePosition = 17;
            ultraGridColumn19.Width = 75;
            ultraGridColumn20.Header.Caption = "Weight%";
            ultraGridColumn20.Header.VisiblePosition = 16;
            ultraGridColumn20.Width = 75;
            ultraGridColumn21.Header.Caption = "Cube%";
            ultraGridColumn21.Header.VisiblePosition = 18;
            ultraGridColumn21.Width = 75;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn1,
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
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21});
            appearance12.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance12.FontData.SizeInPoints = 8F;
            appearance12.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            ultraGridBand2.Override.ActiveRowAppearance = appearance12;
            appearance13.BackColor = System.Drawing.SystemColors.Control;
            appearance13.FontData.BoldAsString = "True";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance13.TextHAlignAsString = "Left";
            ultraGridBand2.Override.HeaderAppearance = appearance13;
            appearance14.BackColor = System.Drawing.SystemColors.Window;
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.WindowText;
            ultraGridBand2.Override.RowAlternateAppearance = appearance14;
            appearance15.BackColor = System.Drawing.SystemColors.Window;
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance15.TextHAlignAsString = "Left";
            ultraGridBand2.Override.RowAppearance = appearance15;
            this.grdAgentSummary.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdAgentSummary.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance16.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance16.FontData.BoldAsString = "True";
            appearance16.FontData.SizeInPoints = 8F;
            appearance16.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance16.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.CaptionAppearance = appearance16;
            this.grdAgentSummary.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAgentSummary.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance17.BackColor = System.Drawing.SystemColors.Control;
            appearance17.FontData.BoldAsString = "True";
            appearance17.FontData.SizeInPoints = 8F;
            appearance17.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.Override.HeaderAppearance = appearance17;
            this.grdAgentSummary.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAgentSummary.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance18.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAgentSummary.DisplayLayout.Override.RowAppearance = appearance18;
            this.grdAgentSummary.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAgentSummary.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAgentSummary.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAgentSummary.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAgentSummary.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAgentSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdAgentSummary.Location = new System.Drawing.Point(3, 3);
            this.grdAgentSummary.Name = "grdAgentSummary";
            this.grdAgentSummary.Size = new System.Drawing.Size(852, 221);
            this.grdAgentSummary.TabIndex = 2;
            this.grdAgentSummary.Text = "Agent Summary View for ";
            this.grdAgentSummary.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mTLAgs
            // 
            this.mTLAgs.DataSetName = "FreightDataset";
            this.mTLAgs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(866, 357);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TLViewer";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).EndInit();
            this.grdTLs.ResumeLayout(false);
            this.grdTLs.PerformLayout();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._picSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLs)).EndInit();
            this.grpTotals.ResumeLayout(false);
            this.grpTotals.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updISAWeight)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabTLs.ResumeLayout(false);
            this.tabAgents.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdAgentSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLAgs)).EndInit();
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
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				this.mToolTip.SetToolTip(this.cboTerminals, "Select an enterprise terminal for the TL and Agent Summary views.");
				this.mToolTip.SetToolTip(this.updISAWeight, "Enter ISA weight in lbs.");
				#endregion
				
				//Set control defaults
				#region Grid initialization
				this.grdTLs.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTLs.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;

                this.grdAgentSummary.Text = "Agent Summary for " + this.cboTerminals.Text;
                this.grdAgentSummary.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdAgentSummary.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;
                #endregion

                string terminal = Program.TerminalCode;
                ServiceInfo si = FreightGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(si.TerminalID.ToString(), si.Description);
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
                this.ssMain.User2Panel.Width = 48;
                this.ssMain.User2Panel.Text = Program.TerminalCode.Trim().Length > 0 ? Program.TerminalCode : "All";
                this.ssMain.User2Panel.ToolTipText = "Operating terminal";

                this.mTerminals.Merge(FreightGateway.GetTerminals());
                if(this.cboTerminals.Items.Count > 0) this.cboTerminals.SelectedIndex = 0;
                OnTerminalSelected(this.cboTerminals,EventArgs.Empty);
                //this.mAutoRefreshSvc.Start();
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing event
            if(!e.Cancel) {
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
        private void OnFormResize(object sender,System.EventArgs e) {
            //Event handler for form resized event
        }
		private void OnTerminalSelected(object sender, System.EventArgs e) {
			//Event handler for change in combobox terminal selection
			try {
				this.mMessageMgr.AddMessage("Changing the active terminal for TL's...");
                this.grdAgentSummary.Text = "Agent Summary for " + this.cboTerminals.Text;

                this.mAutoRefreshSvc.TerminalID = Convert.ToInt32(this.cboTerminals.SelectedValue);
                this.msViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnTLsSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Update totals
			UltraGridRow oRow=null;
			long lTotalWeight=0, lTotalCube=0, lTotalCartons=0, lTotalPallets=0;
			this.Cursor = Cursors.WaitCursor;
			try {
				//Steal focus from combobox
				this.grdTLs.Focus();
				
				//Set selected count; total selected column values
				this.mMessageMgr.AddMessage("Updating totals for the selected TL's...");
				this.lblSelectedTLs.Text = this.grdTLs.Selected.Rows.Count.ToString();
				for(int i=0; i<this.grdTLs.Selected.Rows.Count; i++) {
					oRow = this.grdTLs.Selected.Rows[i];
					lTotalWeight += Convert.ToInt64(oRow.Cells["Weight"].Value);
					lTotalCube += Convert.ToInt64(oRow.Cells["Cube"].Value);
					lTotalCartons += Convert.ToInt64(oRow.Cells["Cartons"].Value);
					lTotalPallets += Convert.ToInt64(oRow.Cells["Pallets"].Value);
				}
				this.txtTLWeight.Text = lTotalWeight.ToString("#,0");
				this.txtTLCube.Text = lTotalCube.ToString("#,0");
				this.txtTLPallets.Text = lTotalPallets.ToString("#,0");
				this.txtTLCartons.Text = lTotalCartons.ToString("#,0");
				updateGrandTotals();
			} 
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnISAWeightChanged(object sender, System.EventArgs e) {
			//Update totals and grand totlas with ISA weight changes
			float isaCube=0f;
			try {
                isaCube = ((float)App.Config.TrailerFullCube / (float)App.Config.TrailerFullWeight) * (float)this.updISAWeight.Value;
				this.txtISACube.Text = isaCube.ToString("#,0");
				updateGrandTotals();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void updateGrandTotals() {
			//Update totals for weight and cube to include ISA values
			float fGrandTotalWeight=0f, fGrandTotalCube=0f, fTrailerWeightPercent=0f, fTrailerCubePercent=0f;
			
			//Calculate
            fGrandTotalWeight = float.Parse(this.txtTLWeight.Text,System.Globalization.NumberStyles.AllowThousands) + (float)this.updISAWeight.Value;
			fGrandTotalCube = float.Parse(this.txtTLCube.Text, System.Globalization.NumberStyles.AllowThousands) + float.Parse(this.txtISACube.Text, System.Globalization.NumberStyles.AllowThousands);
            fTrailerWeightPercent = (fGrandTotalWeight / App.Config.TrailerFullWeight) * 100;
            fTrailerCubePercent = (fGrandTotalCube / App.Config.TrailerFullCube) * 100;
			
			//Update display
			this.txtGrandTotalWeight.Text = fGrandTotalWeight.ToString("#,0");
			this.txtGrandTotalCube.Text = fGrandTotalCube.ToString("#,0");
			this.txtLoadWeightRatio.Text = fTrailerWeightPercent.ToString("#0");
			this.txtTrailerCubeRatio.Text = fTrailerCubePercent.ToString("#0");
		}
		#region Grid Support: OnGridInitializeLayout(), OnGridInitializeRow(), OnSearchValueChanged()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//Calculate derived columns
                e.Row.Cells["WeightPercent"].Value = (Convert.ToSingle(e.Row.Cells["Weight"].Value) / App.Config.TrailerFullWeight) * 100;
                e.Row.Cells["CubePercent"].Value = (Convert.ToSingle(e.Row.Cells["Cube"].Value) / App.Config.TrailerFullCube) * 100;
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if (uiElement != null) {
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if (context != null) {
                        if (e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if (e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)context;
                            if (!row.Selected) grid.Selected.Rows.Clear();
                            row.Selected = true;
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if (uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if (grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnSearchValueChanged(object sender,System.EventArgs e) {
			//Event handler for change in search text value
			try { this.grdTLs.Selected.Rows.Clear(); } 
			catch { }
		}
        #endregion
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Menu services
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
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
                        dlgSave.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save TL View As...";
                        dlgSave.FileName = this.cboTerminals.Text + " TLs";
                        dlgSave.CheckFileExists = false;
                        dlgSave.OverwritePrompt = true;
                        dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            if (this.tabMain.SelectedTab == this.tabTLs) 
                                new Argix.ExcelFormat().Transform(this.mTLs,"TLTable",dlgSave.FileName);
                            else
                                new Argix.ExcelFormat().Transform(this.mTLAgs,"TLTable",dlgSave.FileName);
                        }
                        break;
                    case "msFileExport":
                    case "csExport":
                    case "tsExport":
                        SaveFileDialog dlgExport = new SaveFileDialog();
                        dlgExport.AddExtension = true;
                        dlgExport.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        dlgExport.FilterIndex = 0;
                        dlgExport.Title = "Export TL View As...";
                        dlgExport.FileName = this.cboTerminals.Text + " TLs";
                        dlgExport.CheckFileExists = false;
                        dlgExport.OverwritePrompt = true;
                        dlgExport.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                        if (dlgExport.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            if (this.tabMain.SelectedTab == this.tabTLs) {
                                FreightDataset tls = new FreightDataset();
                                for (int i = 0;i < this.grdTLs.Selected.Rows.Count;i++) {
                                    tls.Merge(this.mTLs.TLTable.Select("TLNumber='" + this.grdTLs.Selected.Rows[i].Cells["TLNumber"].Value.ToString() + "'"));
                                }
                                new Argix.ExcelFormat().Transform(tls,"TLTable",dlgExport.FileName);
                            }
                        }
                        break;
                    case "msFilePageSetup":    UltraGridPrinter.PageSettings(); break;
                    case "msFilePrint":    UltraGridPrinter.Print(this.grdTLs,"TL View for " + this.cboTerminals.Text + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),true); break;
                    case "tsPrint":    UltraGridPrinter.Print(this.grdTLs,"TL View for " + this.cboTerminals.Text + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),false); break;
                    case "msFilePreview":      UltraGridPrinter.PrintPreview(this.grdTLs,"TL View for " + this.cboTerminals.Text + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")); break;
					case "msFileExit":			this.Close(); break;
					case "msEditSearch":			
                    case "tsSearch":
                        this.mGridSvc.FindRow(0, this.grdTLs.Tag.ToString(), this.txtSearchSort.Text); 
                        break;
					case "msViewRefresh":
				    case "tsRefresh":
						this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Refreshing TL's...");
                        this.grdTLs.Text = "TL's for       " + this.cboTerminals.SelectedValue.ToString();
                        this.mTLs.Clear();
                        this.mTLs.Merge(FreightGateway.GetTLView(Convert.ToInt32(this.cboTerminals.SelectedValue)));
                        if(this.grdTLs.Rows.Count > 0) this.grdTLs.ActiveRow = this.grdTLs.Rows[0];
                        OnTLsSelected(this.grdTLs,null);

                        this.mTLAgs.Clear();
                        this.mTLAgs.Merge(FreightGateway.GetAgentSummary(Convert.ToInt32(this.cboTerminals.SelectedValue)));
                        if (this.grdAgentSummary.Rows.Count > 0) this.grdAgentSummary.ActiveRow = this.grdAgentSummary.Rows[0];

                        break;
                    case "msViewTLDetail":
                    case "csTLDetail":
                    case "tsTLDetail":
                        int terminal = int.Parse(this.cboTerminals.SelectedValue.ToString());
                        string tl = this.grdTLs.Selected.Rows[0].Cells["TLNumber"].Value.ToString();
                        dlgTLDetail dlg = new dlgTLDetail(terminal,tl);
                        dlg.Font = this.Font;
                        dlg.ShowDialog(this);
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar":
						this.msViewToolbar.Checked = (!this.msViewToolbar.Checked);
						this.tsMain.Visible = this.msViewToolbar.Checked;
						if(this.tsMain.Visible) {
							this.grpTotals.Top += this.tsMain.Height;
							this.grpTotals.Height -= this.tsMain.Height;
							this.grdTLs.Top += this.tsMain.Height;
							this.grdTLs.Height -= this.tsMain.Height;
						}
						else {
							this.grpTotals.Top -= this.tsMain.Height;
							this.grpTotals.Height += this.tsMain.Height;
							this.grdTLs.Top -= this.tsMain.Height;
							this.grdTLs.Height += this.tsMain.Height;
						}
						break;
					case "msViewStatusBar":
						this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked);
						this.ssMain.Visible = this.msViewStatusBar.Checked;
						if(this.ssMain.Visible) {
							this.grpTotals.Height -= this.ssMain.Height;
							this.grdTLs.Height -= this.ssMain.Height;
						}
						else {
							this.grpTotals.Height += this.ssMain.Height;
							this.grdTLs.Height += this.ssMain.Height;
						}
						break;
                    case "msToolsConfig": 
                        App.ShowConfig();
                        break;
					case "msHelpAbout": new dlgAbout(App.Description, App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                MenuItem ms = (MenuItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(ms.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
		#region Local Services: setUserServices(), buildHelpMenu()
		private void setUserServices() {
			//Set user services
			try {				
				//Set menu states
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
                this.msFileSave.Enabled = this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = (this.grdTLs.Rows.Count > 0 || this.grdAgentSummary.Rows.Count > 0);
                this.msFileExport.Enabled = this.csExport.Enabled = this.tsExport.Enabled = (this.grdTLs.Selected.Rows.Count > 0);
				this.msFilePageSetup.Enabled = true;
                this.msFilePrint.Enabled = (this.grdTLs.Rows.Count > 0);
                this.msFilePreview.Enabled = (this.grdTLs.Rows.Count > 0);
				this.msFileExit.Enabled = true;
				this.msEditSearch.Enabled = this.tsSearch.Enabled = (this.txtSearchSort.Text.Length > 0);
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
                this.msViewTLDetail.Enabled = this.csTLDetail.Enabled = this.tsTLDetail.Enabled = (this.grdTLs.Selected.Rows.Count == 1);
				this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(FreightGateway.ServiceState,FreightGateway.ServiceAddress));
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
                    //item.Name = "msHelp" + sKey;
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
        #endregion

        #region AutoRefresh Services: OnAutoRefreshCompleted()
        public void OnAutoRefreshCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //
            try {
                FreightDataset ds = null;
                if(this.InvokeRequired) {
                    this.Invoke(new RunWorkerCompletedEventHandler(OnAutoRefreshCompleted), new object[] { sender, e });
                }
                else {
                    ds = (FreightDataset)e.Result;
                    if(this.grdTLs.ActiveCell == null || !this.grdTLs.ActiveCell.IsInEditMode) {
                        this.mMessageMgr.AddMessage("Refreshing TL's...");
                        this.mGridSvc.CaptureState();
                        lock(this.mTLs) {
                            this.mTLs.Clear();
                            this.mTLs.Merge(ds);
                        }
                        this.mGridSvc.RestoreState();
                    }
                }
            }
            catch { }
        }
        #endregion
    }

    public class TLAutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;
        private int mTerminalID = 0;

        //Interface
        public TLAutoRefreshService(frmMain postback) {
            //
            this.mTimer = new System.Windows.Forms.Timer();
            this.mTimer.Interval = global::Argix.Properties.Settings.Default.AutoRefreshTimer;
            this.mTimer.Tick += new EventHandler(OnTick);
            this.mWorker = new BackgroundWorker();
            this.mWorker.DoWork += new DoWorkEventHandler(OnAutoRefresh);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(postback.OnAutoRefreshCompleted);
        }
        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        public void Start() { this.mTimer.Start(); }
        public void Stop() { this.mTimer.Stop(); }

        private void OnTick(object sender, EventArgs e) {
            //Event handler for timer tick event
            try { if(!this.mWorker.IsBusy) this.mWorker.RunWorkerAsync(); }
            catch { }
        }
        private void OnAutoRefresh(object sender, DoWorkEventArgs e) {
            //Event handler for background worker thread DoWork event; runs on worker thread
            try { e.Result = FreightGateway.GetTLView(this.mTerminalID); }
            catch { } 
        }
    }
}
