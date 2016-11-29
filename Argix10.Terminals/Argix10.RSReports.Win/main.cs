using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;
using Microsoft.Reporting.WinForms;

namespace Argix.Terminals {
	//
	public class winMain : System.Windows.Forms.Form {
		//Members		
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;

        #region Controls

        //		private System.Windows.Forms.MenuItem msHelpContents;
        private Argix.Windows.ArgixStatusBar ssMain;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSave;
        private ToolStripButton tsExport;
        private ToolStripButton tsPrint;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsCut;
        private ToolStripButton tsCopy;
        private ToolStripButton tsPaste;
        private ToolStripSeparator tsSep3;
        private ToolStripButton tsSearch;
        private ToolStripSeparator tsSep4;
        private ToolStripButton tsRefresh;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
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
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msEditCut;
        private ToolStripMenuItem msEditCopy;
        private ToolStripMenuItem msEditPaste;
        private ToolStripSeparator msEditSep1;
        private ToolStripMenuItem msEditSearch;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStripMenuItem msToolsConfig;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csCut;
        private ToolStripMenuItem csCopy;
        private ToolStripMenuItem csPaste;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripComboBox cboDepots;
        private RSReportsDataset mDepots;
        private TabControl tabMain;
        private TabPage tabPickups;
        private UltraGrid grdPickups;
        private RSReportsDataset mPickups;
        private TabPage tabFeedback;
        private Label _lblRtDate;
        private TextBox txtCmdtyDesc;
        private Label _lblCmdtyDesc;
        private TextBox txtOrdType;
        private Label _lblOrdType;
        private TextBox txtPieces;
        private Label _lblPieces;
        private TextBox txtOrderID;
        private Label _lblOrderID;
        private TextBox txtClose;
        private Label _lblClose;
        private TextBox txtOpen;
        private Label _lblOpen;
        private TextBox txtRtSeq;
        private Label _lblRSeq;
        private Label _lblCustAcct;
        private TextBox txtRtName;
        private Label _lblRtName;
        private Label _lblDriver;
        private TextBox txtRtDate;
        private ComboBox cboOnTime;
        private Label _lblOnTime;
        private ComboBox cboTimeEntry;
        private Label _lblTimeEntry;
        private TextBox txtDelivEnd;
        private Label _lblDelivEnd;
        private TextBox txtDelivStart;
        private Label _lblDelivStart;
        private TextBox txtMallBldg;
        private Label _lblMallBldg;
        private TextBox txtCustName;
        private Label _lblCustName;
        private Label _lblComments;
        private TextBox txtComments;
        private UltraDropDown uddCustomer;
        private UltraDropDown uddDriver;
        private RSReportsDataset mDrivers;
        private RSReportsDataset mCustomers;
        private DateTimePicker dtpRouteDate;
        private UltraDropDown uddOrderTypes;
        private RSReportsDataset mOrderTypes;
        private RSReportsDataset mCmdtyClasses;
        private UltraDropDown uddCmdtyClass;
        private BindingSource bsScanAuditData;
        private RSReportsDataset mScanAudits;
        private TabPage tabReports;
        private Microsoft.Reporting.WinForms.ReportViewer rvMain;
        private ComboBox cboReports;
        private RSReportsDataset mUserNames;
        private RSReportsDataset mOnTimeIssues;
        private TextBox txtCustAcct;
        private BindingNavigator bnScanAudit;
        private ToolStripLabel bindingNavigatorCountItem;
        private ToolStripButton bindingNavigatorMoveFirstItem;
        private ToolStripButton bindingNavigatorMovePreviousItem;
        private ToolStripSeparator bindingNavigatorSeparator;
        private ToolStripTextBox bindingNavigatorPositionItem;
        private ToolStripSeparator bindingNavigatorSeparator1;
        private ToolStripButton bindingNavigatorMoveNextItem;
        private ToolStripButton bindingNavigatorMoveLastItem;
        private ToolStripSeparator bindingNavigatorSeparator2;
        private ToolStripButton tsScanAuditLoad;
        private TextBox textBox1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripComboBox cboDrivers;
        private RSReportsDataset mDrivers2;
        private ToolStripMenuItem msFileScanAuditLoad;
        private ToolStripMenuItem msFileScanAuditSave;
        private ToolStripButton tsScanAuditSave;
        private ToolStripMenuItem msFilePickupsLoad;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
		private System.ComponentModel.IContainer components;
		#endregion

		public winMain() {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Logistics " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
                #region Window docking
                this.tabMain.Dock = DockStyle.Fill;
                this.msMain.Dock = DockStyle.Top;
                this.tsMain.Dock = DockStyle.Top;
                this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tabMain,this.tsMain,this.msMain,this.ssMain });
                this.grdPickups.Dock = DockStyle.Fill;
                #endregion

                //Create data and UI services
                this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 500, 3000);
            }
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
        protected override void Dispose(bool disposing) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CommodityClassTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("COMMODITY_CLASS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DESCRIPTION");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("OrderTypeTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DESCRIPTION");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CustomerTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WHOLE_ACCOUNT_ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STREET_ADDRESS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CITY");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ZIP");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ROUTE_NAME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HANDDEVICETRACKINGNUMBER");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PickupTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RecordID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rt_Date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Driver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rt_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RetnTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Customer_ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CustomerName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CustType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Address");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("State");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrderID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PlanOrdSize");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PlanOrdLbs");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PlanOrdCuFt");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ActOrdSize");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ActOrdLbs");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Unsched_PU");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Comments");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrdTyp");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PlanCmdty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ActCmdty");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Depot");
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winMain));
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsExport = new System.Windows.Forms.ToolStripButton();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCut = new System.Windows.Forms.ToolStripButton();
            this.tsCopy = new System.Windows.Forms.ToolStripButton();
            this.tsPaste = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.cboDepots = new System.Windows.Forms.ToolStripComboBox();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePickupsLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileScanAuditLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileScanAuditSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabPickups = new System.Windows.Forms.TabPage();
            this.uddCmdtyClass = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mCmdtyClasses = new Argix.RSReportsDataset();
            this.uddOrderTypes = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mOrderTypes = new Argix.RSReportsDataset();
            this.uddCustomer = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mCustomers = new Argix.RSReportsDataset();
            this.uddDriver = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mDrivers = new Argix.RSReportsDataset();
            this.grdPickups = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mPickups = new Argix.RSReportsDataset();
            this.tabFeedback = new System.Windows.Forms.TabPage();
            this.bnScanAudit = new System.Windows.Forms.BindingNavigator(this.components);
            this.bsScanAuditData = new System.Windows.Forms.BindingSource(this.components);
            this.mScanAudits = new Argix.RSReportsDataset();
            this.bindingNavigatorCountItem = new System.Windows.Forms.ToolStripLabel();
            this.tsScanAuditLoad = new System.Windows.Forms.ToolStripButton();
            this.tsScanAuditSave = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.cboDrivers = new System.Windows.Forms.ToolStripComboBox();
            this.bindingNavigatorSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorMoveFirstItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMovePreviousItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.bindingNavigatorPositionItem = new System.Windows.Forms.ToolStripTextBox();
            this.bindingNavigatorMoveNextItem = new System.Windows.Forms.ToolStripButton();
            this.bindingNavigatorMoveLastItem = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.txtCustAcct = new System.Windows.Forms.TextBox();
            this._lblComments = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.cboOnTime = new System.Windows.Forms.ComboBox();
            this.mOnTimeIssues = new Argix.RSReportsDataset();
            this._lblOnTime = new System.Windows.Forms.Label();
            this.cboTimeEntry = new System.Windows.Forms.ComboBox();
            this.mUserNames = new Argix.RSReportsDataset();
            this._lblTimeEntry = new System.Windows.Forms.Label();
            this.txtDelivEnd = new System.Windows.Forms.TextBox();
            this._lblDelivEnd = new System.Windows.Forms.Label();
            this.txtDelivStart = new System.Windows.Forms.TextBox();
            this._lblDelivStart = new System.Windows.Forms.Label();
            this.txtMallBldg = new System.Windows.Forms.TextBox();
            this._lblMallBldg = new System.Windows.Forms.Label();
            this.txtCustName = new System.Windows.Forms.TextBox();
            this._lblCustName = new System.Windows.Forms.Label();
            this.txtCmdtyDesc = new System.Windows.Forms.TextBox();
            this._lblCmdtyDesc = new System.Windows.Forms.Label();
            this.txtOrdType = new System.Windows.Forms.TextBox();
            this._lblOrdType = new System.Windows.Forms.Label();
            this.txtPieces = new System.Windows.Forms.TextBox();
            this._lblPieces = new System.Windows.Forms.Label();
            this.txtOrderID = new System.Windows.Forms.TextBox();
            this._lblOrderID = new System.Windows.Forms.Label();
            this.txtClose = new System.Windows.Forms.TextBox();
            this._lblClose = new System.Windows.Forms.Label();
            this.txtOpen = new System.Windows.Forms.TextBox();
            this._lblOpen = new System.Windows.Forms.Label();
            this.txtRtSeq = new System.Windows.Forms.TextBox();
            this._lblRSeq = new System.Windows.Forms.Label();
            this._lblCustAcct = new System.Windows.Forms.Label();
            this.txtRtName = new System.Windows.Forms.TextBox();
            this._lblRtName = new System.Windows.Forms.Label();
            this._lblDriver = new System.Windows.Forms.Label();
            this.txtRtDate = new System.Windows.Forms.TextBox();
            this._lblRtDate = new System.Windows.Forms.Label();
            this.tabReports = new System.Windows.Forms.TabPage();
            this.cboReports = new System.Windows.Forms.ComboBox();
            this.rvMain = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dtpRouteDate = new System.Windows.Forms.DateTimePicker();
            this.mDepots = new Argix.RSReportsDataset();
            this.mDrivers2 = new Argix.RSReportsDataset();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabPickups.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddCmdtyClass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCmdtyClasses)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddOrderTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOrderTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCustomer)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCustomers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddDriver)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPickups)).BeginInit();
            this.tabFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnScanAudit)).BeginInit();
            this.bnScanAudit.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsScanAuditData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mScanAudits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOnTimeIssues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mUserNames)).BeginInit();
            this.tabReports.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mDepots)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers2)).BeginInit();
            this.SuspendLayout();
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csCut,
            this.csCopy,
            this.csPaste});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(103, 70);
            // 
            // csCut
            // 
            this.csCut.Name = "csCut";
            this.csCut.Size = new System.Drawing.Size(102, 22);
            this.csCut.Text = "Cu&t";
            this.csCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csCopy
            // 
            this.csCopy.Name = "csCopy";
            this.csCopy.Size = new System.Drawing.Size(102, 22);
            this.csCopy.Text = "&Copy";
            this.csCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // csPaste
            // 
            this.csPaste.Name = "csPaste";
            this.csPaste.Size = new System.Drawing.Size(102, 22);
            this.csPaste.Text = "&Paste";
            this.csPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 422);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(851, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 11;
            this.ssMain.TerminalText = "Local Terminal";
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
            this.tsPrint,
            this.tsSep2,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsSep3,
            this.tsSearch,
            this.tsSep4,
            this.tsRefresh,
            this.toolStripSeparator1,
            this.cboDepots});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(851, 54);
            this.tsMain.TabIndex = 13;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.Document;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 51);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New file";
            this.tsNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 51);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open and import an existing file";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClicked);
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
            this.tsSave.ToolTipText = "Save";
            this.tsSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsExport
            // 
            this.tsExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.tsExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExport.Name = "tsExport";
            this.tsExport.Size = new System.Drawing.Size(44, 51);
            this.tsExport.Text = "Export";
            this.tsExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsExport.ToolTipText = "Export";
            this.tsExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 51);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsCut
            // 
            this.tsCut.Image = global::Argix.Properties.Resources.Cut;
            this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCut.Name = "tsCut";
            this.tsCut.Size = new System.Drawing.Size(36, 51);
            this.tsCut.Text = "Cut";
            this.tsCut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCut.ToolTipText = "Cut the selected text";
            this.tsCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsCopy
            // 
            this.tsCopy.Image = global::Argix.Properties.Resources.Copy;
            this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.Size = new System.Drawing.Size(39, 51);
            this.tsCopy.Text = "Copy";
            this.tsCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCopy.ToolTipText = "Copy the selected text";
            this.tsCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPaste
            // 
            this.tsPaste.Image = global::Argix.Properties.Resources.Paste;
            this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(39, 51);
            this.tsPaste.Text = "Paste";
            this.tsPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPaste.ToolTipText = "Paste text from the clipboard to the selected cell";
            this.tsPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Search;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(46, 51);
            this.tsSearch.Text = "Search";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.ToolTipText = "Search";
            this.tsSearch.Click += new System.EventHandler(this.OnItemClicked);
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
            this.tsRefresh.ToolTipText = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 54);
            // 
            // cboDepots
            // 
            this.cboDepots.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDepots.Name = "cboDepots";
            this.cboDepots.Size = new System.Drawing.Size(144, 54);
            this.cboDepots.SelectedIndexChanged += new System.EventHandler(this.OnDepotChanged);
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
            this.msMain.Size = new System.Drawing.Size(851, 24);
            this.msMain.TabIndex = 14;
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFilePickupsLoad,
            this.msFileScanAuditLoad,
            this.msFileSep1,
            this.msFileSave,
            this.msFileScanAuditSave,
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
            this.msFileNew.Image = global::Argix.Properties.Resources.Document;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(202, 22);
            this.msFileNew.Text = "&New...";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(202, 22);
            this.msFileOpen.Text = "&Open...";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePickupsLoad
            // 
            this.msFilePickupsLoad.Name = "msFilePickupsLoad";
            this.msFilePickupsLoad.Size = new System.Drawing.Size(202, 22);
            this.msFilePickupsLoad.Text = "Load Scheduled Pickups";
            this.msFilePickupsLoad.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileScanAuditLoad
            // 
            this.msFileScanAuditLoad.Image = global::Argix.Properties.Resources.AddTable;
            this.msFileScanAuditLoad.Name = "msFileScanAuditLoad";
            this.msFileScanAuditLoad.Size = new System.Drawing.Size(202, 22);
            this.msFileScanAuditLoad.Text = "Load Scan Audit Data";
            this.msFileScanAuditLoad.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(199, 6);
            // 
            // msFileSave
            // 
            this.msFileSave.Image = global::Argix.Properties.Resources.Save;
            this.msFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(202, 22);
            this.msFileSave.Text = "&Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileScanAuditSave
            // 
            this.msFileScanAuditSave.Image = global::Argix.Properties.Resources.ImportXML;
            this.msFileScanAuditSave.Name = "msFileScanAuditSave";
            this.msFileScanAuditSave.Size = new System.Drawing.Size(202, 22);
            this.msFileScanAuditSave.Text = "Save Scan Audit Data";
            this.msFileScanAuditSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(202, 22);
            this.msFileSaveAs.Text = "Save &As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.msFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExport.Name = "msFileExport";
            this.msFileExport.Size = new System.Drawing.Size(202, 22);
            this.msFileExport.Text = "&Export";
            this.msFileExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(199, 6);
            // 
            // msFileSettings
            // 
            this.msFileSettings.Image = global::Argix.Properties.Resources.PageSetup;
            this.msFileSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.msFileSettings.Name = "msFileSettings";
            this.msFileSettings.Size = new System.Drawing.Size(202, 22);
            this.msFileSettings.Text = "Page Settings...";
            this.msFileSettings.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(202, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(202, 22);
            this.msFilePreview.Text = "Print Pre&view...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(199, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(202, 22);
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEdit
            // 
            this.msEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msEditCut,
            this.msEditCopy,
            this.msEditPaste,
            this.msEditSep1,
            this.msEditSearch});
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 20);
            this.msEdit.Text = "&Edit";
            // 
            // msEditCut
            // 
            this.msEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.msEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCut.Name = "msEditCut";
            this.msEditCut.Size = new System.Drawing.Size(109, 22);
            this.msEditCut.Text = "Cut";
            this.msEditCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditCopy
            // 
            this.msEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.msEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCopy.Name = "msEditCopy";
            this.msEditCopy.Size = new System.Drawing.Size(109, 22);
            this.msEditCopy.Text = "Copy";
            this.msEditCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditPaste
            // 
            this.msEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.msEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditPaste.Name = "msEditPaste";
            this.msEditPaste.Size = new System.Drawing.Size(109, 22);
            this.msEditPaste.Text = "Paste";
            this.msEditPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msEditSep1
            // 
            this.msEditSep1.Name = "msEditSep1";
            this.msEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // msEditSearch
            // 
            this.msEditSearch.Image = global::Argix.Properties.Resources.Search;
            this.msEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditSearch.Name = "msEditSearch";
            this.msEditSearch.Size = new System.Drawing.Size(109, 22);
            this.msEditSearch.Text = "Search";
            this.msEditSearch.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
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
            this.msViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.msViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(152, 22);
            this.msViewRefresh.Text = "Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(152, 22);
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(152, 22);
            this.msViewStatusBar.Text = "StatusBar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClicked);
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
            this.msToolsConfig.Text = "Configuration...";
            this.msToolsConfig.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 20);
            this.msHelp.Text = "&Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(172, 22);
            this.msHelpAbout.Text = "&About RSReports...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(169, 6);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabPickups);
            this.tabMain.Controls.Add(this.tabFeedback);
            this.tabMain.Controls.Add(this.tabReports);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(96, 24);
            this.tabMain.Location = new System.Drawing.Point(0, 78);
            this.tabMain.Multiline = true;
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(3, 3);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(851, 344);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 15;
            this.tabMain.TabStop = false;
            this.tabMain.TabIndexChanged += new System.EventHandler(this.OnTabSelected);
            // 
            // tabPickups
            // 
            this.tabPickups.BackColor = System.Drawing.SystemColors.Control;
            this.tabPickups.Controls.Add(this.uddCmdtyClass);
            this.tabPickups.Controls.Add(this.uddOrderTypes);
            this.tabPickups.Controls.Add(this.uddCustomer);
            this.tabPickups.Controls.Add(this.uddDriver);
            this.tabPickups.Controls.Add(this.grdPickups);
            this.tabPickups.Location = new System.Drawing.Point(4, 4);
            this.tabPickups.Name = "tabPickups";
            this.tabPickups.Padding = new System.Windows.Forms.Padding(3);
            this.tabPickups.Size = new System.Drawing.Size(843, 312);
            this.tabPickups.TabIndex = 0;
            this.tabPickups.Text = "Pickups";
            // 
            // uddCmdtyClass
            // 
            this.uddCmdtyClass.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddCmdtyClass.DataMember = "CommodityClassTable";
            this.uddCmdtyClass.DataSource = this.mCmdtyClasses;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.uddCmdtyClass.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Class";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 48;
            ultraGridColumn2.Header.Caption = "Description";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 144;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2});
            this.uddCmdtyClass.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.uddCmdtyClass.DisplayLayout.CaptionAppearance = appearance2;
            this.uddCmdtyClass.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddCmdtyClass.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddCmdtyClass.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddCmdtyClass.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.uddCmdtyClass.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.uddCmdtyClass.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddCmdtyClass.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddCmdtyClass.DisplayLayout.Override.RowAppearance = appearance4;
            this.uddCmdtyClass.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddCmdtyClass.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddCmdtyClass.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddCmdtyClass.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddCmdtyClass.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddCmdtyClass.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddCmdtyClass.DisplayMember = "DESCRIPTION";
            this.uddCmdtyClass.Location = new System.Drawing.Point(515, 152);
            this.uddCmdtyClass.Name = "uddCmdtyClass";
            this.uddCmdtyClass.Size = new System.Drawing.Size(93, 58);
            this.uddCmdtyClass.TabIndex = 15;
            this.uddCmdtyClass.ValueMember = "DESCRIPTION";
            this.uddCmdtyClass.Visible = false;
            // 
            // mCmdtyClasses
            // 
            this.mCmdtyClasses.DataSetName = "RSReportsDataset";
            this.mCmdtyClasses.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddOrderTypes
            // 
            this.uddOrderTypes.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddOrderTypes.DataMember = "OrderTypeTable";
            this.uddOrderTypes.DataSource = this.mOrderTypes;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Verdana";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.uddOrderTypes.DisplayLayout.Appearance = appearance5;
            ultraGridColumn3.Header.Caption = "Type";
            ultraGridColumn3.Header.VisiblePosition = 0;
            ultraGridColumn3.Width = 48;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn3});
            this.uddOrderTypes.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.uddOrderTypes.DisplayLayout.CaptionAppearance = appearance6;
            this.uddOrderTypes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddOrderTypes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddOrderTypes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddOrderTypes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.uddOrderTypes.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.uddOrderTypes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddOrderTypes.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddOrderTypes.DisplayLayout.Override.RowAppearance = appearance8;
            this.uddOrderTypes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddOrderTypes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddOrderTypes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddOrderTypes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddOrderTypes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddOrderTypes.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddOrderTypes.DisplayMember = "DESCRIPTION";
            this.uddOrderTypes.Location = new System.Drawing.Point(411, 152);
            this.uddOrderTypes.Name = "uddOrderTypes";
            this.uddOrderTypes.Size = new System.Drawing.Size(93, 58);
            this.uddOrderTypes.TabIndex = 14;
            this.uddOrderTypes.ValueMember = "DESCRIPTION";
            this.uddOrderTypes.Visible = false;
            // 
            // mOrderTypes
            // 
            this.mOrderTypes.DataSetName = "RSReportsDataset";
            this.mOrderTypes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddCustomer
            // 
            this.uddCustomer.DataMember = "CustomerTable";
            this.uddCustomer.DataSource = this.mCustomers;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.FontData.Name = "Verdana";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.uddCustomer.DisplayLayout.Appearance = appearance9;
            ultraGridColumn4.Header.Caption = "AccountID";
            ultraGridColumn4.Header.VisiblePosition = 0;
            ultraGridColumn4.Width = 120;
            ultraGridColumn5.Header.Caption = "Name";
            ultraGridColumn5.Header.VisiblePosition = 1;
            ultraGridColumn5.Width = 192;
            ultraGridColumn6.Header.Caption = "Address";
            ultraGridColumn6.Header.VisiblePosition = 2;
            ultraGridColumn6.Width = 192;
            ultraGridColumn7.Header.Caption = "City";
            ultraGridColumn7.Header.VisiblePosition = 3;
            ultraGridColumn7.Width = 120;
            ultraGridColumn8.Header.Caption = "State";
            ultraGridColumn8.Header.VisiblePosition = 4;
            ultraGridColumn8.Width = 48;
            ultraGridColumn9.Header.Caption = "Zip";
            ultraGridColumn9.Header.VisiblePosition = 5;
            ultraGridColumn9.Width = 72;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9});
            this.uddCustomer.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            appearance10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.Name = "Verdana";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.uddCustomer.DisplayLayout.CaptionAppearance = appearance10;
            this.uddCustomer.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddCustomer.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddCustomer.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddCustomer.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.FontData.BoldAsString = "True";
            appearance11.FontData.Name = "Verdana";
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.TextHAlignAsString = "Left";
            this.uddCustomer.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.uddCustomer.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddCustomer.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance12.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddCustomer.DisplayLayout.Override.RowAppearance = appearance12;
            this.uddCustomer.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddCustomer.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddCustomer.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddCustomer.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddCustomer.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddCustomer.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddCustomer.Location = new System.Drawing.Point(220, 152);
            this.uddCustomer.Name = "uddCustomer";
            this.uddCustomer.Size = new System.Drawing.Size(170, 58);
            this.uddCustomer.TabIndex = 13;
            this.uddCustomer.Visible = false;
            this.uddCustomer.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnCustomerSelected);
            // 
            // mCustomers
            // 
            this.mCustomers.DataSetName = "CustomerDS";
            this.mCustomers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddDriver
            // 
            this.uddDriver.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddDriver.DataMember = "DriverTable";
            this.uddDriver.DataSource = this.mDrivers;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.uddDriver.DisplayLayout.Appearance = appearance13;
            ultraGridColumn10.Header.Caption = "Driver Name";
            ultraGridColumn10.Header.VisiblePosition = 0;
            ultraGridColumn10.Width = 192;
            ultraGridColumn11.Header.Caption = "Route Name";
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Width = 96;
            ultraGridColumn12.Header.VisiblePosition = 2;
            ultraGridColumn12.Hidden = true;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12});
            this.uddDriver.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.uddDriver.DisplayLayout.CaptionAppearance = appearance14;
            this.uddDriver.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddDriver.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddDriver.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddDriver.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.uddDriver.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.uddDriver.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddDriver.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddDriver.DisplayLayout.Override.RowAppearance = appearance16;
            this.uddDriver.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddDriver.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddDriver.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddDriver.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddDriver.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddDriver.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddDriver.DisplayMember = "NAME";
            this.uddDriver.Location = new System.Drawing.Point(18, 152);
            this.uddDriver.Name = "uddDriver";
            this.uddDriver.Size = new System.Drawing.Size(168, 58);
            this.uddDriver.TabIndex = 12;
            this.uddDriver.ValueMember = "NAME";
            this.uddDriver.Visible = false;
            this.uddDriver.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnDriverSelected);
            // 
            // mDrivers
            // 
            this.mDrivers.DataSetName = "RSReportsDataset";
            this.mDrivers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdPickups
            // 
            this.grdPickups.ContextMenuStrip = this.csMain;
            this.grdPickups.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdPickups.DataMember = "PickupTable";
            this.grdPickups.DataSource = this.mPickups;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.FontData.Name = "Verdana";
            appearance17.FontData.SizeInPoints = 8F;
            appearance17.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance17.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.Appearance = appearance17;
            ultraGridColumn13.Header.Caption = "ID";
            ultraGridColumn13.Header.VisiblePosition = 0;
            ultraGridColumn13.Width = 60;
            ultraGridColumn14.Header.Caption = "Date";
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn14.Width = 96;
            ultraGridColumn15.Header.VisiblePosition = 2;
            ultraGridColumn15.Width = 192;
            ultraGridColumn16.Header.Caption = "Route";
            ultraGridColumn16.Header.VisiblePosition = 3;
            ultraGridColumn16.Width = 96;
            ultraGridColumn17.DefaultCellValue = "0";
            ultraGridColumn17.Header.Caption = "Return";
            ultraGridColumn17.Header.VisiblePosition = 4;
            ultraGridColumn17.Width = 72;
            ultraGridColumn18.Header.Caption = "Customer ID";
            ultraGridColumn18.Header.VisiblePosition = 5;
            ultraGridColumn18.Width = 120;
            ultraGridColumn19.Header.Caption = "Customer Name";
            ultraGridColumn19.Header.VisiblePosition = 6;
            ultraGridColumn19.Width = 192;
            ultraGridColumn20.Header.Caption = "Cust Type";
            ultraGridColumn20.Header.VisiblePosition = 7;
            ultraGridColumn20.Width = 72;
            ultraGridColumn21.Header.VisiblePosition = 8;
            ultraGridColumn21.Width = 192;
            ultraGridColumn22.Header.VisiblePosition = 9;
            ultraGridColumn22.Width = 144;
            ultraGridColumn23.Header.VisiblePosition = 10;
            ultraGridColumn23.Width = 48;
            ultraGridColumn24.Header.VisiblePosition = 11;
            ultraGridColumn24.Width = 72;
            ultraGridColumn25.Header.VisiblePosition = 12;
            ultraGridColumn25.Width = 96;
            ultraGridColumn26.Header.VisiblePosition = 13;
            ultraGridColumn26.Width = 72;
            ultraGridColumn27.Header.VisiblePosition = 14;
            ultraGridColumn27.Width = 72;
            ultraGridColumn28.Header.VisiblePosition = 15;
            ultraGridColumn28.Width = 72;
            ultraGridColumn29.DefaultCellValue = "0";
            ultraGridColumn29.Header.VisiblePosition = 16;
            ultraGridColumn29.Width = 72;
            ultraGridColumn30.DefaultCellValue = "0";
            ultraGridColumn30.Header.VisiblePosition = 17;
            ultraGridColumn30.Width = 72;
            ultraGridColumn31.DefaultCellValue = "";
            ultraGridColumn31.Header.Caption = "Unsched Pickup";
            ultraGridColumn31.Header.VisiblePosition = 18;
            ultraGridColumn31.Width = 96;
            ultraGridColumn32.DefaultCellValue = "";
            ultraGridColumn32.Header.VisiblePosition = 19;
            ultraGridColumn32.Width = 144;
            ultraGridColumn33.Header.VisiblePosition = 20;
            ultraGridColumn33.Width = 48;
            ultraGridColumn34.Header.VisiblePosition = 21;
            ultraGridColumn34.Width = 96;
            ultraGridColumn35.DefaultCellValue = "";
            ultraGridColumn35.Header.VisiblePosition = 22;
            ultraGridColumn35.Width = 96;
            ultraGridColumn36.Header.VisiblePosition = 23;
            ultraGridColumn36.Width = 144;
            ultraGridBand5.Columns.AddRange(new object[] {
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
            ultraGridColumn36});
            this.grdPickups.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            appearance18.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance18.FontData.BoldAsString = "True";
            appearance18.FontData.Name = "Verdana";
            appearance18.FontData.SizeInPoints = 8F;
            appearance18.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance18.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.CaptionAppearance = appearance18;
            this.grdPickups.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdPickups.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance19.BackColor = System.Drawing.SystemColors.Control;
            appearance19.FontData.BoldAsString = "True";
            appearance19.FontData.Name = "Verdana";
            appearance19.FontData.SizeInPoints = 8F;
            appearance19.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.Override.HeaderAppearance = appearance19;
            this.grdPickups.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdPickups.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance20.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdPickups.DisplayLayout.Override.RowAppearance = appearance20;
            this.grdPickups.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdPickups.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdPickups.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdPickups.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdPickups.Dock = System.Windows.Forms.DockStyle.Top;
            this.grdPickups.Location = new System.Drawing.Point(3, 3);
            this.grdPickups.Name = "grdPickups";
            this.grdPickups.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdPickups.Size = new System.Drawing.Size(837, 139);
            this.grdPickups.TabIndex = 9;
            this.grdPickups.TabStop = false;
            this.grdPickups.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdPickups.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdPickups.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            this.grdPickups.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridAfterSelectChange);
            this.grdPickups.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnGridBeforeCellActivate);
            this.grdPickups.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.OnGridBeforeRowsDeleted);
            this.grdPickups.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdPickups.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mPickups
            // 
            this.mPickups.DataSetName = "RSReportsDataset";
            this.mPickups.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabFeedback
            // 
            this.tabFeedback.BackColor = System.Drawing.SystemColors.Control;
            this.tabFeedback.Controls.Add(this.bnScanAudit);
            this.tabFeedback.Controls.Add(this.textBox1);
            this.tabFeedback.Controls.Add(this.txtCustAcct);
            this.tabFeedback.Controls.Add(this._lblComments);
            this.tabFeedback.Controls.Add(this.txtComments);
            this.tabFeedback.Controls.Add(this.cboOnTime);
            this.tabFeedback.Controls.Add(this._lblOnTime);
            this.tabFeedback.Controls.Add(this.cboTimeEntry);
            this.tabFeedback.Controls.Add(this._lblTimeEntry);
            this.tabFeedback.Controls.Add(this.txtDelivEnd);
            this.tabFeedback.Controls.Add(this._lblDelivEnd);
            this.tabFeedback.Controls.Add(this.txtDelivStart);
            this.tabFeedback.Controls.Add(this._lblDelivStart);
            this.tabFeedback.Controls.Add(this.txtMallBldg);
            this.tabFeedback.Controls.Add(this._lblMallBldg);
            this.tabFeedback.Controls.Add(this.txtCustName);
            this.tabFeedback.Controls.Add(this._lblCustName);
            this.tabFeedback.Controls.Add(this.txtCmdtyDesc);
            this.tabFeedback.Controls.Add(this._lblCmdtyDesc);
            this.tabFeedback.Controls.Add(this.txtOrdType);
            this.tabFeedback.Controls.Add(this._lblOrdType);
            this.tabFeedback.Controls.Add(this.txtPieces);
            this.tabFeedback.Controls.Add(this._lblPieces);
            this.tabFeedback.Controls.Add(this.txtOrderID);
            this.tabFeedback.Controls.Add(this._lblOrderID);
            this.tabFeedback.Controls.Add(this.txtClose);
            this.tabFeedback.Controls.Add(this._lblClose);
            this.tabFeedback.Controls.Add(this.txtOpen);
            this.tabFeedback.Controls.Add(this._lblOpen);
            this.tabFeedback.Controls.Add(this.txtRtSeq);
            this.tabFeedback.Controls.Add(this._lblRSeq);
            this.tabFeedback.Controls.Add(this._lblCustAcct);
            this.tabFeedback.Controls.Add(this.txtRtName);
            this.tabFeedback.Controls.Add(this._lblRtName);
            this.tabFeedback.Controls.Add(this._lblDriver);
            this.tabFeedback.Controls.Add(this.txtRtDate);
            this.tabFeedback.Controls.Add(this._lblRtDate);
            this.tabFeedback.Location = new System.Drawing.Point(4, 4);
            this.tabFeedback.Name = "tabFeedback";
            this.tabFeedback.Padding = new System.Windows.Forms.Padding(3);
            this.tabFeedback.Size = new System.Drawing.Size(754, 341);
            this.tabFeedback.TabIndex = 1;
            this.tabFeedback.Text = "Feedback";
            // 
            // bnScanAudit
            // 
            this.bnScanAudit.AddNewItem = null;
            this.bnScanAudit.BindingSource = this.bsScanAuditData;
            this.bnScanAudit.CountItem = this.bindingNavigatorCountItem;
            this.bnScanAudit.DeleteItem = null;
            this.bnScanAudit.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.bnScanAudit.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsScanAuditLoad,
            this.tsScanAuditSave,
            this.bindingNavigatorSeparator2,
            this.cboDrivers,
            this.bindingNavigatorSeparator1,
            this.bindingNavigatorMoveFirstItem,
            this.bindingNavigatorMovePreviousItem,
            this.bindingNavigatorSeparator,
            this.bindingNavigatorPositionItem,
            this.bindingNavigatorCountItem,
            this.bindingNavigatorMoveNextItem,
            this.bindingNavigatorMoveLastItem,
            this.toolStripSeparator2});
            this.bnScanAudit.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.bnScanAudit.Location = new System.Drawing.Point(3, 3);
            this.bnScanAudit.MoveFirstItem = this.bindingNavigatorMoveFirstItem;
            this.bnScanAudit.MoveLastItem = this.bindingNavigatorMoveLastItem;
            this.bnScanAudit.MoveNextItem = this.bindingNavigatorMoveNextItem;
            this.bnScanAudit.MovePreviousItem = this.bindingNavigatorMovePreviousItem;
            this.bnScanAudit.Name = "bnScanAudit";
            this.bnScanAudit.PositionItem = this.bindingNavigatorPositionItem;
            this.bnScanAudit.Size = new System.Drawing.Size(748, 25);
            this.bnScanAudit.TabIndex = 43;
            // 
            // bsScanAuditData
            // 
            this.bsScanAuditData.DataMember = "ScanAuditTable";
            this.bsScanAuditData.DataSource = this.mScanAudits;
            this.bsScanAuditData.ListChanged += new System.ComponentModel.ListChangedEventHandler(this.OnListChanged);
            this.bsScanAuditData.PositionChanged += new System.EventHandler(this.OnPositionChanged);
            // 
            // mScanAudits
            // 
            this.mScanAudits.DataSetName = "RSReportsDataset";
            this.mScanAudits.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // bindingNavigatorCountItem
            // 
            this.bindingNavigatorCountItem.Name = "bindingNavigatorCountItem";
            this.bindingNavigatorCountItem.Size = new System.Drawing.Size(35, 22);
            this.bindingNavigatorCountItem.Text = "of {0}";
            this.bindingNavigatorCountItem.ToolTipText = "Total number of items";
            // 
            // tsScanAuditLoad
            // 
            this.tsScanAuditLoad.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsScanAuditLoad.Image = global::Argix.Properties.Resources.AddTable;
            this.tsScanAuditLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsScanAuditLoad.Name = "tsScanAuditLoad";
            this.tsScanAuditLoad.Size = new System.Drawing.Size(23, 22);
            this.tsScanAuditLoad.ToolTipText = "Load scan audit data for the specified route date";
            // 
            // tsScanAuditSave
            // 
            this.tsScanAuditSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsScanAuditSave.Image = global::Argix.Properties.Resources.ImportXML;
            this.tsScanAuditSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsScanAuditSave.Name = "tsScanAuditSave";
            this.tsScanAuditSave.Size = new System.Drawing.Size(23, 22);
            this.tsScanAuditSave.ToolTipText = "Save the current scan audit data";
            this.tsScanAuditSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // bindingNavigatorSeparator2
            // 
            this.bindingNavigatorSeparator2.Name = "bindingNavigatorSeparator2";
            this.bindingNavigatorSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // cboDrivers
            // 
            this.cboDrivers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDrivers.Name = "cboDrivers";
            this.cboDrivers.Size = new System.Drawing.Size(192, 25);
            this.cboDrivers.ToolTipText = "Filter scan audit data by the selected driver";
            this.cboDrivers.SelectedIndexChanged += new System.EventHandler(this.OnDriverChanged);
            // 
            // bindingNavigatorSeparator1
            // 
            this.bindingNavigatorSeparator1.Name = "bindingNavigatorSeparator1";
            this.bindingNavigatorSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorMoveFirstItem
            // 
            this.bindingNavigatorMoveFirstItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveFirstItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveFirstItem.Image")));
            this.bindingNavigatorMoveFirstItem.Name = "bindingNavigatorMoveFirstItem";
            this.bindingNavigatorMoveFirstItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveFirstItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveFirstItem.Text = "Move first";
            // 
            // bindingNavigatorMovePreviousItem
            // 
            this.bindingNavigatorMovePreviousItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMovePreviousItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMovePreviousItem.Image")));
            this.bindingNavigatorMovePreviousItem.Name = "bindingNavigatorMovePreviousItem";
            this.bindingNavigatorMovePreviousItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMovePreviousItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMovePreviousItem.Text = "Move previous";
            // 
            // bindingNavigatorSeparator
            // 
            this.bindingNavigatorSeparator.Name = "bindingNavigatorSeparator";
            this.bindingNavigatorSeparator.Size = new System.Drawing.Size(6, 25);
            // 
            // bindingNavigatorPositionItem
            // 
            this.bindingNavigatorPositionItem.AccessibleName = "Position";
            this.bindingNavigatorPositionItem.AutoSize = false;
            this.bindingNavigatorPositionItem.Name = "bindingNavigatorPositionItem";
            this.bindingNavigatorPositionItem.Size = new System.Drawing.Size(50, 23);
            this.bindingNavigatorPositionItem.Text = "0";
            this.bindingNavigatorPositionItem.ToolTipText = "Current position";
            // 
            // bindingNavigatorMoveNextItem
            // 
            this.bindingNavigatorMoveNextItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveNextItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveNextItem.Image")));
            this.bindingNavigatorMoveNextItem.Name = "bindingNavigatorMoveNextItem";
            this.bindingNavigatorMoveNextItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveNextItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveNextItem.Text = "Move next";
            // 
            // bindingNavigatorMoveLastItem
            // 
            this.bindingNavigatorMoveLastItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.bindingNavigatorMoveLastItem.Image = ((System.Drawing.Image)(resources.GetObject("bindingNavigatorMoveLastItem.Image")));
            this.bindingNavigatorMoveLastItem.Name = "bindingNavigatorMoveLastItem";
            this.bindingNavigatorMoveLastItem.RightToLeftAutoMirrorImage = true;
            this.bindingNavigatorMoveLastItem.Size = new System.Drawing.Size(23, 22);
            this.bindingNavigatorMoveLastItem.Text = "Move last";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // textBox1
            // 
            this.textBox1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "Driver", true));
            this.textBox1.Location = new System.Drawing.Point(183, 61);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(198, 21);
            this.textBox1.TabIndex = 44;
            this.textBox1.TabStop = false;
            // 
            // txtCustAcct
            // 
            this.txtCustAcct.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "CustAcct", true));
            this.txtCustAcct.Location = new System.Drawing.Point(14, 114);
            this.txtCustAcct.Name = "txtCustAcct";
            this.txtCustAcct.ReadOnly = true;
            this.txtCustAcct.Size = new System.Drawing.Size(156, 21);
            this.txtCustAcct.TabIndex = 42;
            this.txtCustAcct.TabStop = false;
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(14, 308);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(143, 16);
            this._lblComments.TabIndex = 41;
            this._lblComments.Text = "Additional Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtComments
            // 
            this.txtComments.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "AdditComments", true));
            this.txtComments.Location = new System.Drawing.Point(14, 328);
            this.txtComments.MaxLength = 244;
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(730, 47);
            this.txtComments.TabIndex = 40;
            this.txtComments.Leave += new System.EventHandler(this.OnLeaveComments);
            // 
            // cboOnTime
            // 
            this.cboOnTime.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScanAuditData, "OnTimeIssue", true));
            this.cboOnTime.DataSource = this.mOnTimeIssues;
            this.cboOnTime.DisplayMember = "OnTimeIssueTable.OnTimeIssue";
            this.cboOnTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOnTime.FormattingEnabled = true;
            this.cboOnTime.Location = new System.Drawing.Point(14, 276);
            this.cboOnTime.Name = "cboOnTime";
            this.cboOnTime.Size = new System.Drawing.Size(300, 21);
            this.cboOnTime.TabIndex = 39;
            this.cboOnTime.ValueMember = "OnTimeIssueTable.OnTimeIssue";
            // 
            // mOnTimeIssues
            // 
            this.mOnTimeIssues.DataSetName = "RSReportsDataset";
            this.mOnTimeIssues.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblOnTime
            // 
            this._lblOnTime.Location = new System.Drawing.Point(14, 255);
            this._lblOnTime.Name = "_lblOnTime";
            this._lblOnTime.Size = new System.Drawing.Size(120, 16);
            this._lblOnTime.TabIndex = 38;
            this._lblOnTime.Text = "On Time Issue";
            this._lblOnTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboTimeEntry
            // 
            this.cboTimeEntry.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", this.bsScanAuditData, "EntryBy", true));
            this.cboTimeEntry.DataSource = this.mUserNames;
            this.cboTimeEntry.DisplayMember = "UpdatedByTable.UpdatedBy";
            this.cboTimeEntry.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTimeEntry.FormattingEnabled = true;
            this.cboTimeEntry.Location = new System.Drawing.Point(336, 220);
            this.cboTimeEntry.Name = "cboTimeEntry";
            this.cboTimeEntry.Size = new System.Drawing.Size(143, 21);
            this.cboTimeEntry.TabIndex = 37;
            this.cboTimeEntry.ValueMember = "UpdatedByTable.UpdatedBy";
            // 
            // mUserNames
            // 
            this.mUserNames.DataSetName = "RSReportsDataset";
            this.mUserNames.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblTimeEntry
            // 
            this._lblTimeEntry.Location = new System.Drawing.Point(336, 200);
            this._lblTimeEntry.Name = "_lblTimeEntry";
            this._lblTimeEntry.Size = new System.Drawing.Size(72, 16);
            this._lblTimeEntry.TabIndex = 36;
            this._lblTimeEntry.Text = "Time Entry";
            this._lblTimeEntry.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDelivEnd
            // 
            this.txtDelivEnd.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "DelEnd", true));
            this.txtDelivEnd.Location = new System.Drawing.Point(116, 220);
            this.txtDelivEnd.MaxLength = 4;
            this.txtDelivEnd.Name = "txtDelivEnd";
            this.txtDelivEnd.Size = new System.Drawing.Size(96, 21);
            this.txtDelivEnd.TabIndex = 33;
            // 
            // _lblDelivEnd
            // 
            this._lblDelivEnd.Location = new System.Drawing.Point(116, 200);
            this._lblDelivEnd.Name = "_lblDelivEnd";
            this._lblDelivEnd.Size = new System.Drawing.Size(72, 16);
            this._lblDelivEnd.TabIndex = 32;
            this._lblDelivEnd.Text = "Deliv End";
            this._lblDelivEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDelivStart
            // 
            this.txtDelivStart.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "DelStart", true));
            this.txtDelivStart.Location = new System.Drawing.Point(14, 220);
            this.txtDelivStart.MaxLength = 4;
            this.txtDelivStart.Name = "txtDelivStart";
            this.txtDelivStart.Size = new System.Drawing.Size(96, 21);
            this.txtDelivStart.TabIndex = 31;
            // 
            // _lblDelivStart
            // 
            this._lblDelivStart.Location = new System.Drawing.Point(14, 200);
            this._lblDelivStart.Name = "_lblDelivStart";
            this._lblDelivStart.Size = new System.Drawing.Size(72, 16);
            this._lblDelivStart.TabIndex = 30;
            this._lblDelivStart.Text = "Deliv Start";
            this._lblDelivStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtMallBldg
            // 
            this.txtMallBldg.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "MallBldg", true));
            this.txtMallBldg.Location = new System.Drawing.Point(339, 167);
            this.txtMallBldg.Name = "txtMallBldg";
            this.txtMallBldg.ReadOnly = true;
            this.txtMallBldg.Size = new System.Drawing.Size(316, 21);
            this.txtMallBldg.TabIndex = 25;
            this.txtMallBldg.TabStop = false;
            // 
            // _lblMallBldg
            // 
            this._lblMallBldg.Location = new System.Drawing.Point(339, 147);
            this._lblMallBldg.Name = "_lblMallBldg";
            this._lblMallBldg.Size = new System.Drawing.Size(72, 16);
            this._lblMallBldg.TabIndex = 24;
            this._lblMallBldg.Text = "MallBldg";
            this._lblMallBldg.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCustName
            // 
            this.txtCustName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "CustName", true));
            this.txtCustName.Location = new System.Drawing.Point(14, 167);
            this.txtCustName.Name = "txtCustName";
            this.txtCustName.ReadOnly = true;
            this.txtCustName.Size = new System.Drawing.Size(316, 21);
            this.txtCustName.TabIndex = 23;
            this.txtCustName.TabStop = false;
            // 
            // _lblCustName
            // 
            this._lblCustName.Location = new System.Drawing.Point(14, 147);
            this._lblCustName.Name = "_lblCustName";
            this._lblCustName.Size = new System.Drawing.Size(72, 16);
            this._lblCustName.TabIndex = 22;
            this._lblCustName.Text = "CustName";
            this._lblCustName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtCmdtyDesc
            // 
            this.txtCmdtyDesc.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "CmdtyDesc", true));
            this.txtCmdtyDesc.Location = new System.Drawing.Point(675, 114);
            this.txtCmdtyDesc.Name = "txtCmdtyDesc";
            this.txtCmdtyDesc.ReadOnly = true;
            this.txtCmdtyDesc.Size = new System.Drawing.Size(72, 21);
            this.txtCmdtyDesc.TabIndex = 21;
            this.txtCmdtyDesc.TabStop = false;
            // 
            // _lblCmdtyDesc
            // 
            this._lblCmdtyDesc.Location = new System.Drawing.Point(675, 94);
            this._lblCmdtyDesc.Name = "_lblCmdtyDesc";
            this._lblCmdtyDesc.Size = new System.Drawing.Size(75, 16);
            this._lblCmdtyDesc.TabIndex = 20;
            this._lblCmdtyDesc.Text = "CmdtyDesc";
            this._lblCmdtyDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOrdType
            // 
            this.txtOrdType.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "OrdTyp", true));
            this.txtOrdType.Location = new System.Drawing.Point(597, 114);
            this.txtOrdType.Name = "txtOrdType";
            this.txtOrdType.ReadOnly = true;
            this.txtOrdType.Size = new System.Drawing.Size(72, 21);
            this.txtOrdType.TabIndex = 19;
            this.txtOrdType.TabStop = false;
            // 
            // _lblOrdType
            // 
            this._lblOrdType.Location = new System.Drawing.Point(597, 94);
            this._lblOrdType.Name = "_lblOrdType";
            this._lblOrdType.Size = new System.Drawing.Size(72, 16);
            this._lblOrdType.TabIndex = 18;
            this._lblOrdType.Text = "OrdType";
            this._lblOrdType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtPieces
            // 
            this.txtPieces.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "Pieces", true));
            this.txtPieces.Location = new System.Drawing.Point(519, 114);
            this.txtPieces.Name = "txtPieces";
            this.txtPieces.ReadOnly = true;
            this.txtPieces.Size = new System.Drawing.Size(72, 21);
            this.txtPieces.TabIndex = 17;
            this.txtPieces.TabStop = false;
            // 
            // _lblPieces
            // 
            this._lblPieces.Location = new System.Drawing.Point(519, 94);
            this._lblPieces.Name = "_lblPieces";
            this._lblPieces.Size = new System.Drawing.Size(72, 16);
            this._lblPieces.TabIndex = 16;
            this._lblPieces.Text = "Pieces";
            this._lblPieces.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOrderID
            // 
            this.txtOrderID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "OrderID", true));
            this.txtOrderID.Location = new System.Drawing.Point(417, 114);
            this.txtOrderID.Name = "txtOrderID";
            this.txtOrderID.ReadOnly = true;
            this.txtOrderID.Size = new System.Drawing.Size(96, 21);
            this.txtOrderID.TabIndex = 15;
            this.txtOrderID.TabStop = false;
            // 
            // _lblOrderID
            // 
            this._lblOrderID.Location = new System.Drawing.Point(417, 94);
            this._lblOrderID.Name = "_lblOrderID";
            this._lblOrderID.Size = new System.Drawing.Size(72, 16);
            this._lblOrderID.TabIndex = 14;
            this._lblOrderID.Text = "OrderID";
            this._lblOrderID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtClose
            // 
            this.txtClose.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "OrdClose", true));
            this.txtClose.Location = new System.Drawing.Point(339, 114);
            this.txtClose.Name = "txtClose";
            this.txtClose.ReadOnly = true;
            this.txtClose.Size = new System.Drawing.Size(72, 21);
            this.txtClose.TabIndex = 13;
            this.txtClose.TabStop = false;
            // 
            // _lblClose
            // 
            this._lblClose.Location = new System.Drawing.Point(339, 94);
            this._lblClose.Name = "_lblClose";
            this._lblClose.Size = new System.Drawing.Size(72, 16);
            this._lblClose.TabIndex = 12;
            this._lblClose.Text = "Close";
            this._lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtOpen
            // 
            this.txtOpen.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "OrdOpen", true));
            this.txtOpen.Location = new System.Drawing.Point(261, 114);
            this.txtOpen.Name = "txtOpen";
            this.txtOpen.ReadOnly = true;
            this.txtOpen.Size = new System.Drawing.Size(72, 21);
            this.txtOpen.TabIndex = 11;
            this.txtOpen.TabStop = false;
            // 
            // _lblOpen
            // 
            this._lblOpen.Location = new System.Drawing.Point(261, 94);
            this._lblOpen.Name = "_lblOpen";
            this._lblOpen.Size = new System.Drawing.Size(72, 16);
            this._lblOpen.TabIndex = 10;
            this._lblOpen.Text = "Open";
            this._lblOpen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRtSeq
            // 
            this.txtRtSeq.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "RtSeq", true));
            this.txtRtSeq.Location = new System.Drawing.Point(183, 114);
            this.txtRtSeq.Name = "txtRtSeq";
            this.txtRtSeq.ReadOnly = true;
            this.txtRtSeq.Size = new System.Drawing.Size(72, 21);
            this.txtRtSeq.TabIndex = 9;
            this.txtRtSeq.TabStop = false;
            // 
            // _lblRSeq
            // 
            this._lblRSeq.Location = new System.Drawing.Point(183, 94);
            this._lblRSeq.Name = "_lblRSeq";
            this._lblRSeq.Size = new System.Drawing.Size(72, 16);
            this._lblRSeq.TabIndex = 8;
            this._lblRSeq.Text = "RtSeq";
            this._lblRSeq.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblCustAcct
            // 
            this._lblCustAcct.Location = new System.Drawing.Point(14, 94);
            this._lblCustAcct.Name = "_lblCustAcct";
            this._lblCustAcct.Size = new System.Drawing.Size(72, 16);
            this._lblCustAcct.TabIndex = 6;
            this._lblCustAcct.Text = "CustAcct";
            this._lblCustAcct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRtName
            // 
            this.txtRtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "RtName", true));
            this.txtRtName.Location = new System.Drawing.Point(417, 61);
            this.txtRtName.Name = "txtRtName";
            this.txtRtName.ReadOnly = true;
            this.txtRtName.Size = new System.Drawing.Size(96, 21);
            this.txtRtName.TabIndex = 5;
            this.txtRtName.TabStop = false;
            // 
            // _lblRtName
            // 
            this._lblRtName.Location = new System.Drawing.Point(417, 41);
            this._lblRtName.Name = "_lblRtName";
            this._lblRtName.Size = new System.Drawing.Size(72, 16);
            this._lblRtName.TabIndex = 4;
            this._lblRtName.Text = "RtName";
            this._lblRtName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDriver
            // 
            this._lblDriver.Location = new System.Drawing.Point(183, 41);
            this._lblDriver.Name = "_lblDriver";
            this._lblDriver.Size = new System.Drawing.Size(72, 16);
            this._lblDriver.TabIndex = 2;
            this._lblDriver.Text = "Driver";
            this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRtDate
            // 
            this.txtRtDate.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsScanAuditData, "RtDate", true));
            this.txtRtDate.Location = new System.Drawing.Point(14, 61);
            this.txtRtDate.Name = "txtRtDate";
            this.txtRtDate.ReadOnly = true;
            this.txtRtDate.Size = new System.Drawing.Size(96, 21);
            this.txtRtDate.TabIndex = 1;
            this.txtRtDate.TabStop = false;
            // 
            // _lblRtDate
            // 
            this._lblRtDate.Location = new System.Drawing.Point(14, 41);
            this._lblRtDate.Name = "_lblRtDate";
            this._lblRtDate.Size = new System.Drawing.Size(72, 16);
            this._lblRtDate.TabIndex = 0;
            this._lblRtDate.Text = "RtDate";
            this._lblRtDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabReports
            // 
            this.tabReports.Controls.Add(this.cboReports);
            this.tabReports.Controls.Add(this.rvMain);
            this.tabReports.Location = new System.Drawing.Point(4, 4);
            this.tabReports.Name = "tabReports";
            this.tabReports.Size = new System.Drawing.Size(754, 341);
            this.tabReports.TabIndex = 2;
            this.tabReports.Text = "Reports";
            this.tabReports.UseVisualStyleBackColor = true;
            // 
            // cboReports
            // 
            this.cboReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cboReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReports.FormattingEnabled = true;
            this.cboReports.Items.AddRange(new object[] {
            "Daily Customer Pickup",
            "Daily Floor Loadout",
            "Driver Return Times",
            "Load Percent Summary",
            "Routes Auto",
            "Routes Auto Summary",
            "Routes Auto vs Edit",
            "Routes Edit",
            "Routes Edit Summary",
            "Routes Edit Final",
            "Routes Events",
            "Limited"});
            this.cboReports.Location = new System.Drawing.Point(515, 3);
            this.cboReports.Name = "cboReports";
            this.cboReports.Size = new System.Drawing.Size(236, 21);
            this.cboReports.TabIndex = 1;
            this.cboReports.TabStop = false;
            this.cboReports.SelectionChangeCommitted += new System.EventHandler(this.OnReportSelected);
            // 
            // rvMain
            // 
            this.rvMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvMain.Location = new System.Drawing.Point(0, 0);
            this.rvMain.Name = "rvMain";
            this.rvMain.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rvMain.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/reportserver", System.UriKind.Absolute);
            this.rvMain.ShowCredentialPrompts = false;
            this.rvMain.ShowDocumentMapButton = false;
            this.rvMain.ShowFindControls = false;
            this.rvMain.ShowParameterPrompts = false;
            this.rvMain.ShowPromptAreaButton = false;
            this.rvMain.Size = new System.Drawing.Size(754, 341);
            this.rvMain.TabIndex = 0;
            // 
            // dtpRouteDate
            // 
            this.dtpRouteDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpRouteDate.Location = new System.Drawing.Point(608, 55);
            this.dtpRouteDate.MinDate = new System.DateTime(2011, 1, 1, 0, 0, 0, 0);
            this.dtpRouteDate.Name = "dtpRouteDate";
            this.dtpRouteDate.Size = new System.Drawing.Size(239, 21);
            this.dtpRouteDate.TabIndex = 14;
            this.dtpRouteDate.TabStop = false;
            this.dtpRouteDate.ValueChanged += new System.EventHandler(this.OnRouteDateChanged);
            // 
            // mDepots
            // 
            this.mDepots.DataSetName = "RSReportsDataset";
            this.mDepots.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mDrivers2
            // 
            this.mDrivers2.DataSetName = "RSReportsDataset";
            this.mDrivers2.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(152, 22);
            this.msViewFont.Text = "Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // winMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(851, 446);
            this.Controls.Add(this.dtpRouteDate);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "winMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Roadshow Reporting";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.csMain.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabPickups.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uddCmdtyClass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCmdtyClasses)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddOrderTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOrderTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCustomer)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCustomers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddDriver)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mPickups)).EndInit();
            this.tabFeedback.ResumeLayout(false);
            this.tabFeedback.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bnScanAudit)).EndInit();
            this.bnScanAudit.ResumeLayout(false);
            this.bnScanAudit.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsScanAuditData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mScanAudits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOnTimeIssues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mUserNames)).EndInit();
            this.tabReports.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mDepots)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers2)).EndInit();
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
                    this.msViewToolbar.Checked = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.msViewStatusBar.Checked = this.ssMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
                #endregion
                #region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
                this.mToolTip.SetToolTip(this.cboDepots.ComboBox,"Select a depot");
                this.mToolTip.SetToolTip(this.dtpRouteDate,"Select a route date");
				#endregion

                //Set control defaults
                #region Grid customizations from normal layout (to support cell editing)
                this.grdPickups.RowUpdateCancelAction = RowUpdateCancelAction.RetainDataAndActivation;
                this.grdPickups.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                this.grdPickups.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdPickups.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdPickups.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdPickups.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                this.grdPickups.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdPickups.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdPickups.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdPickups.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                this.grdPickups.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                //this.grdPickups.DisplayLayout.Bands[0].Columns["Rt_Date"].CellActivation = Activation.AllowEdit;
                this.grdPickups.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdPickups.DisplayLayout.Bands[0].Columns["RecordID"].SortIndicator = SortIndicator.Ascending;

                this.uddCustomer.DataSource = TerminalsGateway.GetCustomers();
                this.uddCustomer.DisplayMember = "WHOLE_ACCOUNT_ID";
                this.uddCustomer.ValueMember = "WHOLE_ACCOUNT_ID";
                this.uddCustomer.DisplayLayout.Bands[0].Columns["Name"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.uddOrderTypes.DataSource = TerminalsGateway.GetOrderTypes();
                this.uddOrderTypes.DisplayMember = "DESCRIPTION";
                this.uddOrderTypes.ValueMember = "DESCRIPTION";
                this.uddOrderTypes.DisplayLayout.Bands[0].Columns["DESCRIPTION"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.uddCmdtyClass.DataSource = TerminalsGateway.GetCommodityClasses();
                this.uddCmdtyClass.DisplayMember = "DESCRIPTION";
                this.uddCmdtyClass.ValueMember = "DESCRIPTION";
                this.uddCmdtyClass.DisplayLayout.Bands[0].Columns["DESCRIPTION"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                #endregion

                ServiceInfo t = TerminalsGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;

                this.mCustomers.Merge(TerminalsGateway.GetCustomers());
                this.mOnTimeIssues.Merge(TerminalsGateway.GetOnTimeIssues());
                this.mDepots.Merge(TerminalsGateway.GetDepots(Program.TerminalCode));
                this.cboDepots.ComboBox.DisplayMember = "DepotTable.Depotname";
                this.cboDepots.ComboBox.ValueMember = "DepotTable.RS_OrderClass";
                this.cboDepots.ComboBox.DataSource = this.mDepots;
                this.cboDepots.ComboBox.SelectedIndex = 0;
                this.dtpRouteDate.MaxDate = DateTime.Today.AddDays(1);
                this.dtpRouteDate.Value = DateTime.Today;
                this.cboReports.SelectedIndex = -1;
                this.rvMain.RefreshReport();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
			finally { setUserServices(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Ask only if there are detail forms open
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
        private void OnDepotChanged(object sender,EventArgs e) {
            //Event handler for change in depot
            this.Cursor = Cursors.WaitCursor;
            try {
                this.uddDriver.DataSource = TerminalsGateway.GetDrivers(this.cboDepots.ComboBox.SelectedValue.ToString());
                this.uddDriver.DisplayMember = "NAME";
                this.uddDriver.ValueMember = "ROUTE_NAME";
                this.uddDriver.DisplayLayout.Bands[0].Columns["NAME"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.mDrivers.Clear();
                this.mDrivers.Merge(TerminalsGateway.GetDrivers(this.cboDepots.ComboBox.SelectedValue.ToString()));

                this.mUserNames.Clear();
                this.mUserNames.Merge(TerminalsGateway.GetUpdateUsers(this.cboDepots.ComboBox.SelectedValue.ToString()));

                OnRouteDateChanged(this.dtpRouteDate,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnRouteDateChanged(object sender,EventArgs e) {
            //Event handler for change in route date
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mPickups.Clear();
                this.mPickups.Merge(TerminalsGateway.GetPickups(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString()));

                this.mDrivers2.Clear();
                this.mDrivers2.DriverTable.AddDriverTableRow("All","",0);
                this.mDrivers2.Merge(TerminalsGateway.GetScanAuditDrivers(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString()));
                this.cboDrivers.ComboBox.DisplayMember = "DriverTable.NAME";
                this.cboDrivers.ComboBox.ValueMember = "DriverTable.NAME";
                this.cboDrivers.ComboBox.DataSource = this.mDrivers2;
                this.cboDrivers.ComboBox.SelectedIndex = 0;
                OnDriverChanged(null,EventArgs.Empty);
                
                OnReportSelected(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(null,EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnTabSelected(object sender,EventArgs e) {
            //Event handler for change in selected tab

        }
        private void OnDriverSelected(object sender,Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            try {
                 if(e.Row != null) {
                    this.grdPickups.ActiveRow.Cells["Driver"].Value = e.Row.Cells["NAME"].Text;
                    this.grdPickups.ActiveRow.Cells["Rt_Name"].Value = e.Row.Cells["ROUTE_NAME"].Text;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnCustomerSelected(object sender,Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            try {
                if(e.Row != null) {
                    this.grdPickups.ActiveRow.Cells["Customer_ID"].Value = e.Row.Cells["WHOLE_ACCOUNT_ID"].Text;
                    this.grdPickups.ActiveRow.Cells["CustomerName"].Value = e.Row.Cells["NAME"].Text;
                    this.grdPickups.ActiveRow.Cells["Address"].Value = e.Row.Cells["STREET_ADDRESS"].Text;
                    this.grdPickups.ActiveRow.Cells["City"].Value = e.Row.Cells["CITY"].Text;
                    this.grdPickups.ActiveRow.Cells["State"].Value = e.Row.Cells["STATE"].Text;
                    this.grdPickups.ActiveRow.Cells["Zip"].Value = e.Row.Cells["ZIP"].Text;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDriverChanged(object sender,EventArgs e) {
            //Event handler for change in scna audit driver
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.mScanAudits.HasChanges(DataRowState.Modified)) {
                    if(MessageBox.Show("The current scan audit has unsaved changes. Would you like to save these changes before proceeding?",App.Title,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        TerminalsGateway.UpdateScanAudits(this.mScanAudits);
                }
                
                this.mScanAudits.Clear();
                if(this.cboDrivers.ComboBox.SelectedValue != null)
                    this.mScanAudits.Merge(TerminalsGateway.GetScanAudits(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString(),this.cboDrivers.ComboBox.SelectedValue.ToString()));
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnListChanged(object sender,ListChangedEventArgs e) {
            setUserServices(null,EventArgs.Empty);
        }
        private void OnReportSelected(object sender,EventArgs e) {
            //Event handler for change in selected report
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboReports.Text.Length > 0) {
                    this.rvMain.ServerReport.ReportPath = "/Terminals/Roadshow " + this.cboReports.Text;
                    ReportParameterInfoCollection ps = this.rvMain.ServerReport.GetParameters();
                    ReportParameter p1=null,p2=null,p3=null,p4=null;
                    foreach(ReportParameterInfo p in ps) {
                        switch(p.Name) {
                            case "RouteClass": 
                                p1 = new ReportParameter("RouteClass",this.cboDepots.ComboBox.SelectedValue.ToString()); 
                                break;
                            case "RouteDate": 
                            case "RouteStartDate":
                                p2 = new ReportParameter(p.Name,this.dtpRouteDate.Value.ToString());
                                break;
                            case "RouteEndDate":
                                p3 = new ReportParameter("RouteEndDate",this.dtpRouteDate.Value.ToString());
                                break;
                            case "RouteEvent":
                                p4 = new ReportParameter("RouteEvent");
                                break;
                        }
                    }
                    if(p4 != null)
                        this.rvMain.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4 }); 
                    else if(p3 != null)
                        this.rvMain.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3 });
                    else
                        this.rvMain.ServerReport.SetParameters(new ReportParameter[] { p1,p2 });
                    this.rvMain.RefreshReport();
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        #region Grid Services: OnGridMouseDown(), OnGridAfterSelectChange(), OnGridBeforeCellActivate(), OnGridCellChange(), OnGridBeforeRowUpdate(), OnGridBeforeRowsDeleted()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
            try {
                e.Layout.Bands[0].Columns["Driver"].ValueList = this.uddDriver;
                e.Layout.Bands[0].Columns["Customer_ID"].ValueList = this.uddCustomer;
                e.Layout.Bands[0].Columns["OrdTyp"].ValueList = this.uddOrderTypes;
                e.Layout.Bands[0].Columns["ActCmdty"].ValueList = this.uddCmdtyClass;

                //select the first row
                if(this.grdPickups.Rows.Count > 0) this.grdPickups.ActiveRow = this.grdPickups.Rows[0];
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Select rows on right click
                UltraGrid oGrid = (UltraGrid)sender;
                oGrid.Focus();
                UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
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
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(null,EventArgs.Empty); }
        }
        private void OnGridAfterSelectChange(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for change in selected entry
            try {
                //Clear reference to prior entry object
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(null,EventArgs.Empty); }
        }
        private void OnGridBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for data entry cell activating
            try {
                //Enable\disable cell editing
                switch(e.Cell.Column.Key.ToString()) {
                    case "Driver": e.Cell.Activation = Activation.AllowEdit; break;
                    //case "Rt_Name": e.Cell.Activation = Activation.NoEdit; break;
                    case "Customer_ID": e.Cell.Activation = Activation.AllowEdit; break;
                    case "OrderID": e.Cell.Activation = e.Cell.Row.IsAddRow ? Activation.AllowEdit : Activation.NoEdit; break;
                    case "ActOrdSize": e.Cell.Activation = Activation.AllowEdit; break;
                    case "ActOrdLbs": e.Cell.Activation = Activation.AllowEdit; break;
                    case "Unsched_PU": e.Cell.Activation = Activation.AllowEdit; break;
                    case "Comments": e.Cell.Activation = Activation.AllowEdit; break;
                    case "OrdTyp": e.Cell.Activation = e.Cell.Row.IsAddRow ? Activation.AllowEdit : Activation.NoEdit; break;
                    case "ActCmdty": e.Cell.Activation = Activation.AllowEdit; break;
                    default: e.Cell.Activation = Activation.NoEdit; break;
                }
                e.Cell.Selected = true;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridCellChange(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a data entry cell value
            try {
                //
                if(e.Cell.Row.IsAddRow && e.Cell.Row.Cells["Rt_Date"].Value == DBNull.Value) {
                    e.Cell.Row.Cells["Rt_Date"].Value = this.dtpRouteDate.Value;
                    e.Cell.Row.Cells["OrderID"].Value = DateTime.Now.ToString("MMddyyyyhhmmss");
                    e.Cell.Row.Cells["OrdTyp"].Value = TerminalsGateway.GetOrderTypes().OrderTypeTable[0].DESCRIPTION;
                    e.Cell.Row.Cells["ActCmdty"].Value = TerminalsGateway.GetCommodityClasses().CommodityClassTable[0].DESCRIPTION;
                    e.Cell.Row.Cells["Depot"].Value = this.cboDepots.Text;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            UltraGrid grid = (UltraGrid)sender;
            if(e.KeyCode == System.Windows.Forms.Keys.Enter) {
                //Update row on Enter
                grid.ActiveRow.Update();
                e.Handled = true;
            }
            else if(e.KeyCode == System.Windows.Forms.Keys.Escape) {
                this.msViewRefresh.PerformClick();
            }
            //else if(e.KeyCode == System.Windows.Forms.Keys.Delete) {
            //    this.ctxCDelete.PerformClick();
            //    e.Handled = true;
            //}
        }
        private void OnGridBeforeRowUpdate(object sender,Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            //Event handler for data entry row updated
            try {
                //There is no selected row when updating- at a cell level
                RSReportsDataset.PickupTableRow pickup = new RSReportsDataset().PickupTable.NewPickupTableRow();
                pickup.Rt_Date = Convert.ToDateTime(e.Row.Cells["Rt_Date"].Value);
                pickup.Driver = e.Row.Cells["Driver"].Value.ToString();
                pickup.Rt_Name = e.Row.Cells["Rt_Name"].Value.ToString();
                pickup.RetnTime =  Convert.ToInt16(e.Row.Cells["RetnTime"].Value);
                pickup.Customer_ID = e.Row.Cells["Customer_ID"].Value.ToString();
                pickup.CustomerName = e.Row.Cells["CustomerName"].Value.ToString();
                if(e.Row.Cells["CustType"].Value != DBNull.Value) pickup.CustType = e.Row.Cells["CustType"].Value.ToString();
                pickup.Address = e.Row.Cells["Address"].Value.ToString();
                pickup.City = e.Row.Cells["City"].Value.ToString();
                pickup.State = e.Row.Cells["State"].Value.ToString();
                pickup.Zip = e.Row.Cells["Zip"].Value.ToString();
                pickup.OrderID = e.Row.Cells["OrderID"].Value.ToString();
                pickup.ActOrdSize = Convert.ToSingle(e.Row.Cells["ActOrdSize"].Value);
                pickup.ActOrdLbs =  Convert.ToSingle(e.Row.Cells["ActOrdLbs"].Value);
                pickup.Unsched_PU = e.Row.Cells["Unsched_PU"].Value.ToString();
                pickup.Comments = e.Row.Cells["Comments"].Value.ToString();
                pickup.OrdTyp = e.Row.Cells["OrdTyp"].Value.ToString();
                pickup.ActCmdty = e.Row.Cells["ActCmdty"].Value.ToString();
                pickup.Depot = e.Row.Cells["Depot"].Value.ToString();
                if(pickup.Driver.Length > 0 && pickup.Customer_ID.Length > 0 && !pickup.HasErrors) {
                    if(e.Row.IsAddRow) {
                        if(!TerminalsGateway.AddPickup(pickup)) {
                            e.Row.CancelUpdate();
                            App.ReportError(new ApplicationException("An error occured and pickup could not be added."),true, LogLevel.Error);
                        }
                    }
                    else {
                        pickup.RecordID = Convert.ToInt32(e.Row.Cells["RecordID"].Value);
                        if(!TerminalsGateway.UpdatePickup(pickup)) {
                            e.Row.CancelUpdate();
                            App.ReportError(new ApplicationException("An error occured and pickup could not be updated."),true, LogLevel.Error);
                        }
                    }
                    this.msViewRefresh.PerformClick();
                }
                else
                    e.Cancel = true;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(null, EventArgs.Empty); }
        }
        private void OnGridBeforeRowsDeleted(object sender,BeforeRowsDeletedEventArgs e) {
            //Event hanlder for rows deleting
            try {
                //Cannot delete 
                e.DisplayPromptMsg = false;
                e.Cancel = true;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        #endregion
        #region User Services: OnItemClicked(), OnHelpMenuClick()
        private void OnItemClicked(object sender,EventArgs e) {
            //Event handler for manu item clicked
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "msFileNew":
                    case "tsNew":
                        break;
                    case "msFileOpen":
                    case "tsOpen":
                        break;
                    case "msFilePickupsLoad":
                    case "tsPickupsLoad":
                        TerminalsGateway.LoadPickups(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString());
                        OnRouteDateChanged(this.dtpRouteDate,EventArgs.Empty);
                        break;
                    case "msFileScanAuditLoad":
                    case "tsScanAuditLoad":
                        TerminalsGateway.LoadScanAudits(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString());
                        OnRouteDateChanged(this.dtpRouteDate,EventArgs.Empty); 
                        break;
                    case "msFileSave":
                    case "tsSave":
                        break;
                    case "msFileScanAuditSave":
                    case "tsScanAuditSave":
                        TerminalsGateway.UpdateScanAudits(this.mScanAudits);
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Data Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Pickups As...";
                        dlgSave.FileName = this.Text;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            this.mPickups.WriteXml(dlgSave.FileName);
                            this.mMessageMgr.AddMessage("Pickups saved to " + dlgSave.FileName);
                        }
                        break;
                    case "tsSaveScan": 
                        break;
                    case "msFileExport":
                    case "tsExport":
                        break;
                    case "msFileSettings": UltraGridPrinter.PageSettings(); break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdPickups,"Pickups for " + this.cboDepots.Text + ", " + this.dtpRouteDate.Value.ToString("MM/dd/yyyy")); break;
                    case "msFilePrint": UltraGridPrinter.Print(this.grdPickups,"Pickups for " + this.cboDepots.Text + ", " + this.dtpRouteDate.Value.ToString("MM/dd/yyyy"),true); break;
                    case "tsPrint": UltraGridPrinter.Print(this.grdPickups,"Pickups for " + this.cboDepots.Text + ", " + this.dtpRouteDate.Value.ToString("MM/dd/yyyy"),false); break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "csCut":
                    case "tsCut":
                        break;
                    case "msEditCopy":
                    case "csCopy":
                    case "tsCopy":
                        break;
                    case "msEditPaste":
                    case "csPaste":
                    case "tsPaste":
                        break;
                    case "msEditSearch": 
                    case "tsSearch":
                        break;
                    case "msViewRefresh":
                    case "tsRefresh":
                        //Refresh pickups collection
                        this.Cursor = Cursors.WaitCursor;
                        OnRouteDateChanged(this.dtpRouteDate,EventArgs.Empty);
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msToolsConfig": App.ShowConfig(); break;
                    case "msHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(null, EventArgs.Empty); this.Cursor = Cursors.Default; }
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
        private void setUserServices(object sender,EventArgs e) {
			//Set user services
            try {
                this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
                this.msFilePickupsLoad.Enabled = this.cboDepots.ComboBox.SelectedValue != null && TerminalsGateway.CanLoadPickups(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString());
                this.msFileScanAuditLoad.Enabled = this.tsScanAuditLoad.Enabled = this.cboDepots.ComboBox.SelectedValue != null && TerminalsGateway.CanLoadScanAudit(this.dtpRouteDate.Value,this.cboDepots.ComboBox.SelectedValue.ToString());
                this.msFileScanAuditSave.Enabled = this.tsScanAuditSave.Enabled = this.mScanAudits.HasChanges(DataRowState.Modified);
                this.msFileSave.Enabled = this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = (this.tabMain.SelectedTab==this.tabPickups & this.grdPickups.Focused);
				this.msFileExport.Enabled = this.tsExport.Enabled = false;
				this.msFileSettings.Enabled = true;
                this.msFilePreview.Enabled = this.msFilePrint.Enabled = this.tsPrint.Enabled = this.tabMain.SelectedTab==this.tabPickups & this.grdPickups.Focused;
				this.msFileExit.Enabled = true;
				this.msEditCut.Enabled = this.csCut.Enabled = this.tsCut.Enabled = false;
                this.msEditCopy.Enabled = this.csCopy.Enabled = this.tsCopy.Enabled = false;
                this.msEditPaste.Enabled = this.csPaste.Enabled = this.tsPaste.Enabled = false;
				this.msEditSearch.Enabled = this.tsSearch.Enabled = false;
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
				this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(TerminalsGateway.ServiceState,TerminalsGateway.ServiceAddress));
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
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
                    item.Text = sKey;
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
		#endregion

        private void OnLeaveComments(object sender,EventArgs e) {
            //Save record (if changes); then move to next record
            try {
                //this.bsScanAuditData.EndEdit();
                this.bsScanAuditData.MoveNext();
                TerminalsGateway.UpdateScanAudits(this.mScanAudits);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnPositionChanged(object sender,EventArgs e) {
            setUserServices(null,EventArgs.Empty);
        }
    }
}