using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;
using System.Drawing.Printing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Enterprise;
using Argix.Security;
using Argix.Windows;

namespace Argix.AgentLineHaul {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private Pickup mPickup=null;
		private UltraGridSvc mGridSvcPickups=null, mGridSvcCartons=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		private bool mCalendarOpen=false;
		
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdPickups;
		private System.Windows.Forms.DateTimePicker dtpPickupDate;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdSortedItems;
        private Argix.ISDExportDataset mSortedItems;
        private Argix.ISDExportDataset mPickups;
        private Argix.Windows.ArgixStatusBar ssMain;
        private System.Windows.Forms.MonthCalendar calPickupDate;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripMenuItem msFileExport;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileSetup;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePreview;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripMenuItem msViewCartons;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewCalendar;
        private ToolStripSeparator msViewSep2;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripSeparator msHelpSep1;
        private ToolStripMenuItem msHelpAbout;
        private Panel pnlCalHeader;
        private Label lblCloseCal;
        private Label lblCalHeader;
        private ToolStripMenuItem msToolsConfig;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSave;
        private ToolStripButton tsExport;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsPrint;
        private ToolStripButton tsRefresh;
        private ToolStripButton tsSortedItems;
        private ToolStripMenuItem msViewClientConfig;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csCut;
        private ToolStripMenuItem csCopy;
        private ToolStripMenuItem csPaste;
        private ToolStripMenuItem msViewFont;
        private SplitContainer scMain;
        private SplitContainer scTop;
		private System.ComponentModel.IContainer components;
		#endregion
		
		public frmMain() {
			//Constructor			
			try {
				InitializeComponent();
				this.Text = App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.grdPickups.Controls.AddRange(new Control[] { this.dtpPickupDate });
				#endregion
				
				//Create data and UI services
				this.mGridSvcPickups = new UltraGridSvc(this.grdPickups);
				this.mGridSvcCartons = new UltraGridSvc(this.grdSortedItems);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 500, 3000);
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("PickupTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DivisionNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipperName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickUpDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorKey");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SealNumber");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SortedItemTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LABEL_SEQ_NUMBER", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Ascending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CLIENT_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CLIENT_DIV_NUM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AGENT_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VENDOR_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SORTED_LOCATION");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DAMAGE_CODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PICKUP_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PICKUP_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STORE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ZONE_CODE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TRAILER_LOAD_NUM");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEM_TYPE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEM_WEIGHT");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VENDOR_KEY");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VENDOR_ITEM_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RETURN_FLAG");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RETURN_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SHIFT_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SHIFT_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("END_TIME");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ARC_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("STATION");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ITEM_CUBE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SORT_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SAN_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ELAPSED_SECONDS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DOWN_SECONDS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PO_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OS_TRACKING_NUMBER");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SHIPPING_METHOD");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SAMPLE_DATE");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScanString");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InboundLabelID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightType");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdPickups = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.dtpPickupDate = new System.Windows.Forms.DateTimePicker();
            this.mPickups = new Argix.ISDExportDataset();
            this.grdSortedItems = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSortedItems = new Argix.ISDExportDataset();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.pnlCalHeader = new System.Windows.Forms.Panel();
            this.lblCloseCal = new System.Windows.Forms.Label();
            this.lblCalHeader = new System.Windows.Forms.Label();
            this.calPickupDate = new System.Windows.Forms.MonthCalendar();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewCartons = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewClientConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewCalendar = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsSortedItems = new System.Windows.Forms.ToolStripButton();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scTop = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.grdPickups)).BeginInit();
            this.grdPickups.SuspendLayout();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPickups)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSortedItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortedItems)).BeginInit();
            this.pnlCalHeader.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scTop)).BeginInit();
            this.scTop.Panel1.SuspendLayout();
            this.scTop.Panel2.SuspendLayout();
            this.scTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdPickups
            // 
            this.grdPickups.CausesValidation = false;
            this.grdPickups.ContextMenuStrip = this.csMain;
            this.grdPickups.Controls.Add(this.dtpPickupDate);
            this.grdPickups.DataMember = "PickupTable";
            this.grdPickups.DataSource = this.mPickups;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "TLViewTable";
            ultraGridColumn1.Header.Caption = "Client#";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.Caption = "Div#";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 48;
            ultraGridColumn3.Header.Caption = "Client Name";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 168;
            ultraGridColumn4.Header.Caption = "Shipper#";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 72;
            ultraGridColumn5.Header.Caption = "Shipper Name";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 168;
            ultraGridColumn6.Header.Caption = "Pickup Date";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 96;
            ultraGridColumn7.Header.Caption = "Pickup#";
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 72;
            ultraGridColumn8.Header.Caption = "Freight Type";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 96;
            ultraGridColumn9.Header.Caption = "TDS#";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.Header.Caption = "Vendor Key";
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Width = 72;
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn11.Width = 96;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.Hidden = true;
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
            ultraGridColumn13});
            this.grdPickups.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.CaptionAppearance = appearance2;
            this.grdPickups.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdPickups.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdPickups.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdPickups.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdPickups.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdPickups.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdPickups.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdPickups.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdPickups.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdPickups.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdPickups.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdPickups.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPickups.Location = new System.Drawing.Point(0, 0);
            this.grdPickups.Name = "grdPickups";
            this.grdPickups.Size = new System.Drawing.Size(430, 243);
            this.grdPickups.TabIndex = 1;
            this.grdPickups.Text = "Pickups for ";
            this.grdPickups.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdPickups.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnPickupSelected);
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
            this.csCut.Image = global::Argix.Properties.Resources.Cut;
            this.csCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCut.Name = "csCut";
            this.csCut.Size = new System.Drawing.Size(102, 22);
            this.csCut.Text = "Cu&t";
            this.csCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCopy
            // 
            this.csCopy.Image = global::Argix.Properties.Resources.Copy;
            this.csCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCopy.Name = "csCopy";
            this.csCopy.Size = new System.Drawing.Size(102, 22);
            this.csCopy.Text = "&Copy";
            this.csCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csPaste
            // 
            this.csPaste.Image = global::Argix.Properties.Resources.Paste;
            this.csPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csPaste.Name = "csPaste";
            this.csPaste.Size = new System.Drawing.Size(102, 22);
            this.csPaste.Text = "&Paste";
            this.csPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // dtpPickupDate
            // 
            this.dtpPickupDate.CausesValidation = false;
            this.dtpPickupDate.Location = new System.Drawing.Point(67, 0);
            this.dtpPickupDate.MaxDate = new System.DateTime(2031, 12, 31, 0, 0, 0, 0);
            this.dtpPickupDate.MinDate = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpPickupDate.Name = "dtpPickupDate";
            this.dtpPickupDate.Size = new System.Drawing.Size(180, 20);
            this.dtpPickupDate.TabIndex = 2;
            this.dtpPickupDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpPickupDate.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpPickupDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // mPickups
            // 
            this.mPickups.DataSetName = "ISDExportDataset";
            this.mPickups.Locale = new System.Globalization.CultureInfo("en-US");
            this.mPickups.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdSortedItems
            // 
            this.grdSortedItems.CausesValidation = false;
            this.grdSortedItems.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSortedItems.DataSource = this.mSortedItems.SortedItemTable;
            appearance5.BackColor = System.Drawing.SystemColors.Control;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdSortedItems.DisplayLayout.Appearance = appearance5;
            ultraGridBand2.AddButtonCaption = "FreightAssignmentDetailTable";
            ultraGridColumn14.Header.Caption = "Label Seqnce#";
            ultraGridColumn14.Header.VisiblePosition = 8;
            ultraGridColumn14.Width = 120;
            ultraGridColumn15.Header.Caption = "Client#";
            ultraGridColumn15.Header.VisiblePosition = 0;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.Caption = "Div#";
            ultraGridColumn16.Header.VisiblePosition = 1;
            ultraGridColumn16.Width = 48;
            ultraGridColumn17.Header.VisiblePosition = 3;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.Caption = "Vendor#";
            ultraGridColumn18.Header.VisiblePosition = 2;
            ultraGridColumn18.Width = 72;
            ultraGridColumn19.Header.VisiblePosition = 6;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.Header.Caption = "Damage Code";
            ultraGridColumn20.Header.VisiblePosition = 14;
            ultraGridColumn20.Width = 72;
            ultraGridColumn21.Header.Caption = "Pickup Date";
            ultraGridColumn21.Header.VisiblePosition = 4;
            ultraGridColumn21.Width = 96;
            ultraGridColumn22.Header.Caption = "Pickup#";
            ultraGridColumn22.Header.VisiblePosition = 5;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.Caption = "Store";
            ultraGridColumn23.Header.VisiblePosition = 10;
            ultraGridColumn23.Width = 60;
            ultraGridColumn24.Header.VisiblePosition = 12;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.VisiblePosition = 15;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn26.Header.VisiblePosition = 16;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn27.Header.Caption = "Weight";
            ultraGridColumn27.Header.VisiblePosition = 9;
            ultraGridColumn27.Width = 60;
            ultraGridColumn28.Header.VisiblePosition = 18;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.Caption = "Vendor Item#";
            ultraGridColumn29.Header.VisiblePosition = 7;
            ultraGridColumn29.Width = 120;
            ultraGridColumn30.Header.VisiblePosition = 19;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 20;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 21;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 22;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Format = "hh:mm tt";
            ultraGridColumn34.Header.Caption = "End Time";
            ultraGridColumn34.Header.VisiblePosition = 13;
            ultraGridColumn34.Width = 72;
            ultraGridColumn35.Header.VisiblePosition = 23;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 24;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 25;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Format = "MM/dd/yyyy";
            ultraGridColumn38.Header.Caption = "Sort Date";
            ultraGridColumn38.Header.VisiblePosition = 11;
            ultraGridColumn38.Width = 96;
            ultraGridColumn39.Header.VisiblePosition = 26;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.Header.VisiblePosition = 27;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.VisiblePosition = 28;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.Header.VisiblePosition = 29;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.Header.VisiblePosition = 30;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn44.Header.VisiblePosition = 31;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn45.Header.VisiblePosition = 32;
            ultraGridColumn45.Hidden = true;
            ultraGridColumn46.Header.VisiblePosition = 33;
            ultraGridColumn46.Hidden = true;
            ultraGridColumn47.Header.VisiblePosition = 34;
            ultraGridColumn47.Hidden = true;
            ultraGridColumn48.Header.Caption = "Freight Type";
            ultraGridColumn48.Header.VisiblePosition = 17;
            ultraGridColumn48.Width = 96;
            ultraGridBand2.Columns.AddRange(new object[] {
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
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48});
            this.grdSortedItems.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdSortedItems.DisplayLayout.CaptionAppearance = appearance6;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.grdSortedItems.DisplayLayout.Override.ActiveRowAppearance = appearance7;
            this.grdSortedItems.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSortedItems.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortedItems.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortedItems.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            this.grdSortedItems.DisplayLayout.Override.CellMultiLine = Infragistics.Win.DefaultableBoolean.False;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.FontData.BoldAsString = "True";
            appearance8.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance8.TextHAlignAsString = "Left";
            this.grdSortedItems.DisplayLayout.Override.HeaderAppearance = appearance8;
            this.grdSortedItems.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSortedItems.DisplayLayout.Override.MaxSelectedCells = 1;
            this.grdSortedItems.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BorderColor = System.Drawing.SystemColors.ControlDark;
            this.grdSortedItems.DisplayLayout.Override.RowAppearance = appearance9;
            this.grdSortedItems.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSortedItems.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdSortedItems.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSortedItems.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSortedItems.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSortedItems.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSortedItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSortedItems.Location = new System.Drawing.Point(0, 0);
            this.grdSortedItems.Name = "grdSortedItems";
            this.grdSortedItems.Size = new System.Drawing.Size(664, 88);
            this.grdSortedItems.TabIndex = 4;
            this.grdSortedItems.Text = "Sorted Items";
            this.grdSortedItems.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mSortedItems
            // 
            this.mSortedItems.DataSetName = "ISDExportDataset";
            this.mSortedItems.Locale = new System.Globalization.CultureInfo("en-US");
            this.mSortedItems.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 408);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(664, 25);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 11;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // pnlCalHeader
            // 
            this.pnlCalHeader.Controls.Add(this.lblCloseCal);
            this.pnlCalHeader.Controls.Add(this.lblCalHeader);
            this.pnlCalHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCalHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlCalHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCalHeader.Name = "pnlCalHeader";
            this.pnlCalHeader.Padding = new System.Windows.Forms.Padding(1);
            this.pnlCalHeader.Size = new System.Drawing.Size(230, 22);
            this.pnlCalHeader.TabIndex = 119;
            this.pnlCalHeader.Enter += new System.EventHandler(this.OnEnterCalendar);
            this.pnlCalHeader.Leave += new System.EventHandler(this.OnLeaveCalendar);
            // 
            // lblCloseCal
            // 
            this.lblCloseCal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseCal.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCloseCal.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblCloseCal.Location = new System.Drawing.Point(215, 2);
            this.lblCloseCal.Name = "lblCloseCal";
            this.lblCloseCal.Size = new System.Drawing.Size(13, 15);
            this.lblCloseCal.TabIndex = 115;
            this.lblCloseCal.Text = "X";
            this.lblCloseCal.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseCal.Click += new System.EventHandler(this.OnCloseCalendar);
            this.lblCloseCal.Enter += new System.EventHandler(this.OnEnterCalendar);
            this.lblCloseCal.Leave += new System.EventHandler(this.OnLeaveCalendar);
            // 
            // lblCalHeader
            // 
            this.lblCalHeader.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCalHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCalHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCalHeader.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblCalHeader.Location = new System.Drawing.Point(1, 1);
            this.lblCalHeader.Name = "lblCalHeader";
            this.lblCalHeader.Size = new System.Drawing.Size(228, 20);
            this.lblCalHeader.TabIndex = 113;
            this.lblCalHeader.Text = "Pickup Date";
            this.lblCalHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCalHeader.Enter += new System.EventHandler(this.OnEnterCalendar);
            this.lblCalHeader.Leave += new System.EventHandler(this.OnLeaveCalendar);
            // 
            // calPickupDate
            // 
            this.calPickupDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.calPickupDate.Location = new System.Drawing.Point(0, 22);
            this.calPickupDate.MaxSelectionCount = 1;
            this.calPickupDate.Name = "calPickupDate";
            this.calPickupDate.TabIndex = 14;
            this.calPickupDate.DateChanged += new System.Windows.Forms.DateRangeEventHandler(this.OnPickupDateChanged);
            this.calPickupDate.DateSelected += new System.Windows.Forms.DateRangeEventHandler(this.OnPickupDateSelected);
            this.calPickupDate.Enter += new System.EventHandler(this.OnEnterCalendar);
            this.calPickupDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnPickupDateKeyed);
            this.calPickupDate.Leave += new System.EventHandler(this.OnLeaveCalendar);
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
            this.msMain.Padding = new System.Windows.Forms.Padding(0);
            this.msMain.Size = new System.Drawing.Size(664, 24);
            this.msMain.TabIndex = 18;
            this.msMain.Text = "RDS Menu";
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFileSep1,
            this.msFileSaveAs,
            this.msFileExport,
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
            // msFileSaveAs
            // 
            this.msFileSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.msFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.msFileSaveAs.Text = "Save&As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.AddTable;
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
            // msFileSetup
            // 
            this.msFileSetup.Image = global::Argix.Properties.Resources.PrintSetup;
            this.msFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSetup.Name = "msFileSetup";
            this.msFileSetup.Size = new System.Drawing.Size(152, 22);
            this.msFileSetup.Text = "Page Setup...";
            this.msFileSetup.Click += new System.EventHandler(this.OnItemClick);
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
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(152, 22);
            this.msFilePreview.Text = "Print Preview...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClick);
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
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEdit
            // 
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 24);
            this.msEdit.Text = "&Edit";
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewCartons,
            this.msViewSep1,
            this.msViewFont,
            this.msViewClientConfig,
            this.msViewSep2,
            this.msViewCalendar,
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
            this.msViewRefresh.Size = new System.Drawing.Size(191, 22);
            this.msViewRefresh.Text = "Refresh Pickups";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewCartons
            // 
            this.msViewCartons.Image = global::Argix.Properties.Resources.BarCode;
            this.msViewCartons.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewCartons.Name = "msViewCartons";
            this.msViewCartons.Size = new System.Drawing.Size(191, 22);
            this.msViewCartons.Text = "Sorted Items";
            this.msViewCartons.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(188, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(191, 22);
            this.msViewFont.Text = "&Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewClientConfig
            // 
            this.msViewClientConfig.Name = "msViewClientConfig";
            this.msViewClientConfig.Size = new System.Drawing.Size(191, 22);
            this.msViewClientConfig.Text = "Client Configuration...";
            this.msViewClientConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(188, 6);
            // 
            // msViewCalendar
            // 
            this.msViewCalendar.Image = global::Argix.Properties.Resources.Calendar_schedule;
            this.msViewCalendar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewCalendar.Name = "msViewCalendar";
            this.msViewCalendar.Size = new System.Drawing.Size(191, 22);
            this.msViewCalendar.Text = "Calendar";
            this.msViewCalendar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(191, 22);
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(191, 22);
            this.msViewStatusBar.Text = "StatusBar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
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
            this.msHelp.Size = new System.Drawing.Size(44, 24);
            this.msHelp.Text = "&Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(172, 22);
            this.msHelpAbout.Text = "About ISD Export...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.MergeAction = System.Windows.Forms.MergeAction.Insert;
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(169, 6);
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
            this.tsRefresh,
            this.tsSortedItems});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(664, 49);
            this.tsMain.TabIndex = 19;
            this.tsMain.Text = "Main";
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
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 49);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 46);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save...";
            this.tsSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsExport
            // 
            this.tsExport.Image = global::Argix.Properties.Resources.AddTable;
            this.tsExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsExport.Name = "tsExport";
            this.tsExport.Size = new System.Drawing.Size(44, 46);
            this.tsExport.Text = "Export";
            this.tsExport.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsExport.ToolTipText = "Export sorted items...";
            this.tsExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 46);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print sorted items...";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 49);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 46);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh pickups";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSortedItems
            // 
            this.tsSortedItems.Image = global::Argix.Properties.Resources.BarCode;
            this.tsSortedItems.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSortedItems.Name = "tsSortedItems";
            this.tsSortedItems.Size = new System.Drawing.Size(77, 46);
            this.tsSortedItems.Text = "Show Sorted";
            this.tsSortedItems.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSortedItems.ToolTipText = "Display sorted items";
            this.tsSortedItems.Click += new System.EventHandler(this.OnItemClick);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 73);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scTop);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.grdSortedItems);
            this.scMain.Size = new System.Drawing.Size(664, 335);
            this.scMain.SplitterDistance = 243;
            this.scMain.TabIndex = 20;
            // 
            // scTop
            // 
            this.scTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scTop.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scTop.Location = new System.Drawing.Point(0, 0);
            this.scTop.Name = "scTop";
            // 
            // scTop.Panel1
            // 
            this.scTop.Panel1.BackColor = System.Drawing.Color.White;
            this.scTop.Panel1.Controls.Add(this.calPickupDate);
            this.scTop.Panel1.Controls.Add(this.pnlCalHeader);
            this.scTop.Panel1MinSize = 230;
            // 
            // scTop.Panel2
            // 
            this.scTop.Panel2.Controls.Add(this.grdPickups);
            this.scTop.Size = new System.Drawing.Size(664, 243);
            this.scTop.SplitterDistance = 230;
            this.scTop.TabIndex = 18;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 433);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.msMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Inbound Scan Data Export";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdPickups)).EndInit();
            this.grdPickups.ResumeLayout(false);
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mPickups)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSortedItems)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortedItems)).EndInit();
            this.pnlCalHeader.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scTop.Panel1.ResumeLayout(false);
            this.scTop.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scTop)).EndInit();
            this.scTop.ResumeLayout(false);
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
				this.mToolTip.SetToolTip(this.dtpPickupDate, "Select a date for pickups.");
				#endregion
				
				//Set control defaults
				#region Grid Overrides
				this.grdPickups.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdPickups.DisplayLayout.Bands[0].Columns["ClientNumber"].SortIndicator = SortIndicator.Ascending;
				this.grdSortedItems.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdSortedItems.DisplayLayout.Bands[0].Columns["LABEL_SEQ_NUMBER"].SortIndicator = SortIndicator.Ascending;
				#endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ColumnHeaders;
                ServiceInfo t = AgentLineHaulGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;

                this.calPickupDate.MaxSelectionCount = 1;
				this.calPickupDate.MinDate = this.dtpPickupDate.MinDate = DateTime.Today.AddDays(-App.Config.DateDaysBack);
				this.calPickupDate.MaxDate = DateTime.Today;
				this.dtpPickupDate.MaxDate = this.dtpPickupDate.Value = DateTime.Today;
				this.msViewCalendar.Checked = false;
				this.msViewCalendar.PerformClick();
				this.msViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
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
                global::Argix.Properties.Settings.Default.ColumnHeaders = this.ColumnHeaders;
                global::Argix.Properties.Settings.Default.Save();
                #endregion
            }
        }
		#region Calendar Support: OnCalendarOpened(), OnCalendarClosed(), OnCalendarValueChanged()
		private void OnCalendarOpened(object sender, System.EventArgs e) {
			//Event handler for calendar dropped down
			this.mCalendarOpen = true;
		}
		private void OnCalendarClosed(object sender, System.EventArgs e) {
			//Event handler for date picker calendar closed
			try {
				//Allow calendar to close
				this.dtpPickupDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; sync calendars & change terminal pickup date
				this.mCalendarOpen = false;
				this.calPickupDate.SetDate(this.dtpPickupDate.Value);
                this.msViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		private void OnCalendarValueChanged(object sender, System.EventArgs e) {
			//Event handler for pickup date changed
			try {
				//Sync calendars & change terminal pickup date if the calendar is closed
				if(!this.mCalendarOpen) {
					this.calPickupDate.SetDate(this.dtpPickupDate.Value);
					this.msViewRefresh.PerformClick();
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
		}
		#endregion
		#region DateTimePicker Support: OnPickupDateChanged(), OnPickupDateSelected(), OnPickupDateKeyed()
		private void OnPickupDateChanged(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//Event handler for pickup date changed
            setUserServices();
		}
		private void OnPickupDateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e) {
			//Event handler for pickup date selected
			try {
				this.dtpPickupDate.Value = this.calPickupDate.SelectionRange.Start;
			}
			catch(Exception ex) { App.ReportError(ex); }
        }
		private void OnPickupDateKeyed(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for key down in the calendar
			try {
				if(e.KeyCode == Keys.Enter)
					this.dtpPickupDate.Value = this.calPickupDate.SelectionRange.Start;
			}
			catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnCloseCalendar(object sender,System.EventArgs e) {
            //Event handler to close routes window
            this.msViewCalendar.PerformClick();
            setUserServices();
        }
        private void OnEnterCalendar(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblCalHeader.BackColor = this.lblCloseCal.BackColor = SystemColors.ActiveCaption;
                this.lblCalHeader.ForeColor = this.lblCloseCal.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnLeaveCalendar(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblCalHeader.BackColor = this.lblCloseCal.BackColor = SystemColors.InactiveCaption;
                this.lblCalHeader.ForeColor = this.lblCloseCal.ForeColor = SystemColors.InactiveCaptionText;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
		private void OnPickupSelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected pickup
			try {
				//Clear reference to prior pickup object
				this.mPickup = null;
                this.mSortedItems.Clear();
                if(this.grdPickups.Selected.Rows.Count > 0) {
					//Get a pickup object for the selected pickup
					this.grdPickups.Focus();
					string id = this.grdPickups.Selected.Rows[0].Cells["ID"].Value.ToString();
                    ISDExportDataset.PickupTableRow row = (ISDExportDataset.PickupTableRow)this.mPickups.PickupTable.Select("ID='" + id + "'")[0];
                    this.mPickup = new Pickup(row);
				}
			} 
			catch(Exception ex) { App.ReportError(ex); } finally { setUserServices(); }
            this.msViewCartons.PerformClick();
        }
		private void OnSortedItemSelected(object sender, System.EventArgs e) {
			//Event handler for sorted item selection
			setUserServices();
        }
        #region User Services: OnItemClick(), OnHelpItemClick()
        private void OnItemClick(object sender,EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
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
                        break;
					case "msFileExport":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Export Files (*.txt) | *.txt";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Export Sorted Items As...";
                        dlgSave.FileName = this.mPickup.ID;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            exportSortedItems(dlgSave.FileName);
                        }
                        break;
                    case "tsExport": 
                        exportSortedItems(""); 
                        break;
					case "msFileSetup":	    UltraGridPrinter.PageSettings(); break;
					case "msFilePrint":		UltraGridPrinter.Print(this.grdSortedItems, "Sorted Items", true); break;
                    case "tsPrint":            UltraGridPrinter.Print(this.grdSortedItems,"Sorted Items",false); break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdSortedItems,"Sorted Items"); break;
					case "msFileExit":			this.Close(); Application.Exit(); break;
					case "msViewRefresh":
                    case "tsRefresh": 						
                        this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Refreshing pickups...");
                        this.grdPickups.Text = "Pickups for  " + this.dtpPickupDate.Value.ToShortDateString();
                        this.mPickups.Clear();
                        this.mPickups.Merge(AgentLineHaulGateway.GetPickups(this.dtpPickupDate.Value));
                        if(this.grdPickups.Rows.Count > 0) 
                            this.grdPickups.Rows[0].Selected = true;
                        else
                            OnPickupSelected(null,null);
                        this.mMessageMgr.AddMessage(this.mPickups.PickupTable.Rows.Count.ToString() + " pickups for " + this.dtpPickupDate.Value.ToShortDateString() + ".");
                        break;
					case "msViewCartons": 
                    case "tsSortedItems": 
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Requesting sorted items for pickup#" + this.mPickup.ID + "....");
                        this.mSortedItems.Clear();
                        DataSet ds = AgentLineHaulGateway.GetSortedItems(this.mPickup.ID);
                        if(ds != null) this.mSortedItems.Merge(ds,false,MissingSchemaAction.Ignore);
                        this.mMessageMgr.AddMessage(this.mSortedItems.SortedItemTable.Rows.Count.ToString() + " sorted items.");

                        //Display sorted vs manifested
                        int items = Argix.Reports.ReportsGateway.SortedItemsManifested(this.mPickup.ClientNumber, this.mPickup.DivisionNumber, DateTime.Parse(this.mPickup.PickUpDate), this.mPickup.PickupNumber);
                        this.grdSortedItems.Text = "Sorted Items [ Manifested= " + (items > 0 ? items.ToString() : "unavailable") + "; Sorted=" + this.mSortedItems.SortedItemTable.Rows.Count + " ]";
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewClientConfig":
                        new dlgClientConfig().ShowDialog(this);
                        break;
					case "msViewCalendar": 
						this.scTop.Panel1Collapsed = !(this.msViewCalendar.Checked = (!this.msViewCalendar.Checked));
                        this.dtpPickupDate.Visible = this.scTop.Panel1Collapsed; 
						break;
					case "msViewToolbar":	this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
					case "msViewStatusBar":	this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msToolsConfig":   App.ShowConfig(); break;
					case "msHelpAbout":     new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
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
            catch (Exception) { }
        }
        #endregion
        #region Local Services: setUserServices(), buildHelpMenu(), ColumnHeaders
        private void setUserServices() {
			//Set user services
			try {
                this.msFileNew.Enabled = this.tsNew.Enabled = false;
                this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
                this.msFileSaveAs.Enabled = this.tsSave.Enabled = false;
                this.msFileExport.Enabled = this.tsExport.Enabled = ((RoleServiceGateway.IsTsortSupervisor || RoleServiceGateway.IsTsortClerk) && this.mPickup != null && this.mSortedItems.SortedItemTable.Count > 0);
                this.msFilePrint.Enabled = this.tsPrint.Enabled = (this.mPickup != null && this.mSortedItems.SortedItemTable.Count > 0);
                this.msFilePreview.Enabled = (this.mPickup != null && this.mSortedItems.SortedItemTable.Count > 0);
				this.msFileExit.Enabled = true;
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
				this.msViewCartons.Enabled = this.tsSortedItems.Enabled = (this.mPickup != null);
                this.msViewClientConfig.Enabled = true;
				this.msViewCalendar.Enabled = true;
				this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(AgentLineHaulGateway.ServiceState,AgentLineHaulGateway.ServiceAddress));
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
                this.ssMain.User2Panel.Icon = null;
                this.ssMain.User2Panel.ToolTipText = "";
                if(this.mSortedItems.SortedItemTable.Rows.Count > 0) {
                    this.ssMain.User2Panel.Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.sorteditem.ico"));
                    this.ssMain.User2Panel.ToolTipText = this.mSortedItems.SortedItemTable.Rows.Count.ToString() + " sorted items.";
                }
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
			catch(Exception) { }
		}
        private string ColumnHeaders {
            get {
                MemoryStream ms = new MemoryStream();
                this.grdPickups.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if (value.Length > 0) {
                    MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdPickups.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                }
            }
        }
        #endregion
        private void exportSortedItems(string file) {
            //Exports sorted items dataset to a file specified by the database
            try {
                //Get filename and export type
                Exporter exporter = null;
                ISDExportDataset ds = AgentLineHaulGateway.GetISDClients(this.mPickup.ClientNumber);
                if (ds.ClientTable.Rows.Count > 0) {
                    for (int i = 0;i < ds.ClientTable.Rows.Count;i++) {
                        ISDClient isdClient = new ISDClient();
                        isdClient.CLID = ds.ClientTable[i].CLID;
                        isdClient.ExportFormat = ds.ClientTable[i].ExportFormat.Trim();
                        isdClient.ExportPath = ds.ClientTable[i].ExportPath;
                        isdClient.CounterKey = ds.ClientTable[i].CounterKey;
                        isdClient.Client = ds.ClientTable[i].Client;
                        isdClient.Scanner = ds.ClientTable[i].Scanner;
                        isdClient.UserID = ds.ClientTable[i].UserID;
                        switch (isdClient.ExportFormat.ToLower()) {
                            case "rds3": exporter = new RDS3Exporter(); break;
                            case "rds4": exporter = new RDS4Exporter(); break;
                            case "pcs": exporter = new PCSExporter(); break;
                            default: throw new ApplicationException(isdClient.ExportFormat + " is an unknown export format.");
                        }
                        if (file.Trim().Length == 0) {
                            if (isdClient.ExportPath.Trim().Length == 0) {
                                SaveFileDialog dlgSave = new SaveFileDialog();
                                dlgSave.AddExtension = true;
                                dlgSave.Filter = "Export Files (*.txt) | *.txt";
                                dlgSave.FilterIndex = 0;
                                dlgSave.Title = "Export Sorted Items As...";
                                dlgSave.FileName = this.mPickup.ID;
                                dlgSave.OverwritePrompt = true;
                                if (dlgSave.ShowDialog(this) == DialogResult.OK) file = dlgSave.FileName;
                            }
                            else
                                file = isdClient.ExportPath + AgentLineHaulGateway.GetExportFilename(ds.ClientTable[i].CounterKey);
                        }
                        bool exported = exporter.Export(file,isdClient,this.mPickup,this.mSortedItems);
                        if (exported) this.mMessageMgr.AddMessage(this.mSortedItems.SortedItemTable.Count.ToString() + " records exported to " + file + ".");
                    }
                }
                else
                    MessageBox.Show("There is no export configuration for this client.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
    }
}