using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Terminals;
using Argix.Windows;
using Argix.Windows.Printers;
using Microsoft.Reporting.WinForms;

namespace Argix.HR {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
        private BadgeDataset.BadgeTableRow mBadge=null;
		private UltraGridSvc mGridSvc=null;
		private WinPrinter mPrinter=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		#region Controls
		private System.ComponentModel.IContainer components;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdBadges;
        private Argix.Windows.ArgixStatusBar ssMain;
		private System.Windows.Forms.PictureBox picPhoto;
		private System.Windows.Forms.PictureBox picSignature;
		private System.Windows.Forms.Label lblBadge;
		private System.Windows.Forms.Panel pnlProfile;
		private System.Windows.Forms.Label _lblOrganization;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Label _lblDepartment;
		private System.Windows.Forms.Label _lblLocation;
		private System.Windows.Forms.Label _lblOffice;
		private System.Windows.Forms.Label lblOrganization;
		private System.Windows.Forms.Label lblDepartment;
		private System.Windows.Forms.Label lblLocation;
		private System.Windows.Forms.Label lblOffice;
        private System.Windows.Forms.Label lblStatus;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSave;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileExport;
        private ToolStripMenuItem msFileEmail;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFilePageSetup;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePrintEmp;
        private ToolStripMenuItem msFilePreview;
        private ToolStripSeparator msFileSep4;
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
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripButton tsSave;
        private ToolStripButton tsExport;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsPrint;
        private ToolStripButton tsEmail;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsCut;
        private ToolStripButton tsCopy;
        private ToolStripButton tsPaste;
        private ToolStripButton tsSearch;
        private ToolStripButton tsRefresh;
        private ToolStripSeparator tsSep3;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem csCut;
        private ToolStripMenuItem csCopy;
        private ToolStripMenuItem csPaste;
        private ToolStripSeparator csSep1;
        private ToolStripMenuItem csSaveAs;
        private ToolStripSeparator csSep2;
        private ToolStripMenuItem csPrint;
        private TabControl tabMain;
        private TabPage tabBadges;
        private TabPage tabRoutes;
        private Microsoft.Reporting.WinForms.ReportViewer rsRoutes;
        private ToolStripMenuItem msViewSubscriptions;
        private ToolStripSeparator msViewSep2;
        private DateTimePicker dtpRouteDate;
        private Label lblDate;
        private ComboBox cboDriver;
        private ComboBox cboRouteClass;
        private Label lblDepot;
        private Label lblDriver;
        private TabPage tabVisTrak;
        private BadgeDataset mBadges;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep3;
		private System.Windows.Forms.TextBox txtSearchSort;
		#endregion
		
		public frmMain() {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);
				#region Set window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.grdBadges.Dock = DockStyle.Fill;
				this.pnlProfile.Dock = DockStyle.Right;
				this.ssMain.Dock = DockStyle.Bottom;
				this.grdBadges.Controls.AddRange(new Control[]{this.txtSearchSort});
				this.txtSearchSort.Left = this.grdBadges.Width - this.txtSearchSort.Width - 3;
                this.tabBadges.Controls.AddRange(new Control[] { this.grdBadges,this.pnlProfile });
                this.Controls.AddRange(new Control[] { this.tabMain,this.tsMain,this.msMain,this.ssMain });
				#endregion
				
				//Create data and UI services
				this.mGridSvc = new UltraGridSvc(this.grdBadges, this.txtSearchSort);
				this.mPrinter = new WinPrinter();
				this.mPrinter.Doc.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(OnPrintPage);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 500, 3000);
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("BadgeTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IDNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Middle");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Suffix");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Organization");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Department");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Faccode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubLocation");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EmployeeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BadgeNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Photo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Signature");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StatusDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IssueDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ExpirationDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BirthDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HireDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SSN");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HasPhoto");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HasSignature");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdBadges = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mBadges = new Argix.BadgeDataset();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.txtSearchSort = new System.Windows.Forms.TextBox();
            this.lblBadge = new System.Windows.Forms.Label();
            this._lblOrganization = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.pnlProfile = new System.Windows.Forms.Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblOffice = new System.Windows.Forms.Label();
            this.lblLocation = new System.Windows.Forms.Label();
            this.lblDepartment = new System.Windows.Forms.Label();
            this.lblOrganization = new System.Windows.Forms.Label();
            this._lblOffice = new System.Windows.Forms.Label();
            this._lblLocation = new System.Windows.Forms.Label();
            this._lblDepartment = new System.Windows.Forms.Label();
            this.picPhoto = new System.Windows.Forms.PictureBox();
            this.picSignature = new System.Windows.Forms.PictureBox();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExport = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileEmail = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrintEmp = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep4 = new System.Windows.Forms.ToolStripSeparator();
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
            this.msViewSubscriptions = new System.Windows.Forms.ToolStripMenuItem();
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
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsExport = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsEmail = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCut = new System.Windows.Forms.ToolStripButton();
            this.tsCopy = new System.Windows.Forms.ToolStripButton();
            this.tsPaste = new System.Windows.Forms.ToolStripButton();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabBadges = new System.Windows.Forms.TabPage();
            this.tabRoutes = new System.Windows.Forms.TabPage();
            this.dtpRouteDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.cboDriver = new System.Windows.Forms.ComboBox();
            this.cboRouteClass = new System.Windows.Forms.ComboBox();
            this.lblDepot = new System.Windows.Forms.Label();
            this.lblDriver = new System.Windows.Forms.Label();
            this.rsRoutes = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabVisTrak = new System.Windows.Forms.TabPage();
            ((System.ComponentModel.ISupportInitialize)(this.grdBadges)).BeginInit();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mBadges)).BeginInit();
            this.pnlProfile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabBadges.SuspendLayout();
            this.tabRoutes.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdBadges
            // 
            this.grdBadges.ContextMenuStrip = this.csMain;
            this.grdBadges.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdBadges.DataMember = "BadgeTable";
            this.grdBadges.DataSource = this.mBadges;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdBadges.DisplayLayout.Appearance = appearance1;
            ultraGridColumn24.Header.VisiblePosition = 12;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn25.Header.Caption = "Last Name";
            ultraGridColumn25.Header.VisiblePosition = 2;
            ultraGridColumn25.Width = 96;
            ultraGridColumn26.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn26.Header.Caption = "First Name";
            ultraGridColumn26.Header.VisiblePosition = 3;
            ultraGridColumn26.Width = 96;
            ultraGridColumn27.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn27.Header.Caption = "Mid";
            ultraGridColumn27.Header.VisiblePosition = 4;
            ultraGridColumn27.Width = 48;
            ultraGridColumn28.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn28.Header.VisiblePosition = 5;
            ultraGridColumn28.Width = 60;
            ultraGridColumn29.Header.VisiblePosition = 6;
            ultraGridColumn30.Header.VisiblePosition = 8;
            ultraGridColumn30.Width = 120;
            ultraGridColumn31.Header.VisiblePosition = 10;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 7;
            ultraGridColumn32.Width = 96;
            ultraGridColumn33.Header.VisiblePosition = 9;
            ultraGridColumn34.Header.VisiblePosition = 1;
            ultraGridColumn35.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn35.Header.Caption = "Badge#";
            ultraGridColumn35.Header.VisiblePosition = 0;
            ultraGridColumn35.Width = 72;
            ultraGridColumn36.Header.VisiblePosition = 17;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 18;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 16;
            ultraGridColumn38.Width = 72;
            ultraGridColumn39.Header.VisiblePosition = 19;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn40.Format = "MM/dd/yyyy";
            ultraGridColumn40.Header.Caption = "Issue Date";
            ultraGridColumn40.Header.VisiblePosition = 15;
            ultraGridColumn40.Width = 96;
            ultraGridColumn41.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn41.Format = "MM/dd/yyyy";
            ultraGridColumn41.Header.Caption = "Exp Date";
            ultraGridColumn41.Header.VisiblePosition = 20;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn41.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn41.Width = 96;
            ultraGridColumn42.Header.VisiblePosition = 21;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn43.Format = "MM/dd/yyyy";
            ultraGridColumn43.Header.Caption = "Hire Date";
            ultraGridColumn43.Header.VisiblePosition = 14;
            ultraGridColumn43.Width = 96;
            ultraGridColumn44.Header.VisiblePosition = 22;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn45.Header.VisiblePosition = 11;
            ultraGridColumn45.Width = 72;
            ultraGridColumn46.Header.VisiblePosition = 13;
            ultraGridColumn46.Width = 72;
            ultraGridBand1.Columns.AddRange(new object[] {
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
            ultraGridColumn46});
            this.grdBadges.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdBadges.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.SizeInPoints = 9F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.grdBadges.DisplayLayout.CaptionAppearance = appearance2;
            this.grdBadges.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdBadges.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdBadges.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdBadges.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdBadges.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdBadges.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdBadges.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdBadges.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdBadges.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdBadges.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdBadges.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdBadges.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdBadges.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdBadges.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdBadges.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdBadges.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdBadges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBadges.Location = new System.Drawing.Point(3, 3);
            this.grdBadges.Name = "grdBadges";
            this.grdBadges.Size = new System.Drawing.Size(508, 387);
            this.grdBadges.TabIndex = 2;
            this.grdBadges.Text = "Argix Badges";
            this.grdBadges.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnBadgeSelected);
            this.grdBadges.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnAfterRowFilterChanged);
            this.grdBadges.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnBeforeRowFilterDropDownPopulate);
            this.grdBadges.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csCut,
            this.csCopy,
            this.csPaste,
            this.csSep1,
            this.csSaveAs,
            this.csSep2,
            this.csPrint});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(103, 126);
            // 
            // csCut
            // 
            this.csCut.Image = global::Argix.Properties.Resources.Cut;
            this.csCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCut.Name = "csCut";
            this.csCut.Size = new System.Drawing.Size(102, 22);
            this.csCut.Text = "Cut";
            this.csCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCopy
            // 
            this.csCopy.Image = global::Argix.Properties.Resources.Copy;
            this.csCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCopy.Name = "csCopy";
            this.csCopy.Size = new System.Drawing.Size(102, 22);
            this.csCopy.Text = "Copy";
            this.csCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csPaste
            // 
            this.csPaste.Image = global::Argix.Properties.Resources.Paste;
            this.csPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csPaste.Name = "csPaste";
            this.csPaste.Size = new System.Drawing.Size(102, 22);
            this.csPaste.Text = "Paste";
            this.csPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(99, 6);
            // 
            // csSaveAs
            // 
            this.csSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.csSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csSaveAs.Name = "csSaveAs";
            this.csSaveAs.Size = new System.Drawing.Size(102, 22);
            this.csSaveAs.Text = "Save";
            this.csSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep2
            // 
            this.csSep2.Name = "csSep2";
            this.csSep2.Size = new System.Drawing.Size(99, 6);
            // 
            // csPrint
            // 
            this.csPrint.Image = global::Argix.Properties.Resources.Print;
            this.csPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csPrint.Name = "csPrint";
            this.csPrint.Size = new System.Drawing.Size(102, 22);
            this.csPrint.Text = "Print";
            this.csPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mBadges
            // 
            this.mBadges.DataSetName = "BadgeDataset";
            this.mBadges.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 497);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(819, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 5;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // txtSearchSort
            // 
            this.txtSearchSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSort.Location = new System.Drawing.Point(411, 3);
            this.txtSearchSort.Name = "txtSearchSort";
            this.txtSearchSort.Size = new System.Drawing.Size(99, 21);
            this.txtSearchSort.TabIndex = 6;
            // 
            // lblBadge
            // 
            this.lblBadge.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBadge.Location = new System.Drawing.Point(63, 360);
            this.lblBadge.Name = "lblBadge";
            this.lblBadge.Size = new System.Drawing.Size(172, 18);
            this.lblBadge.TabIndex = 9;
            this.lblBadge.Text = "Badge #00000";
            this.lblBadge.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblOrganization
            // 
            this._lblOrganization.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOrganization.Location = new System.Drawing.Point(9, 48);
            this._lblOrganization.Name = "_lblOrganization";
            this._lblOrganization.Size = new System.Drawing.Size(96, 18);
            this._lblOrganization.TabIndex = 10;
            this._lblOrganization.Text = "Organization:";
            this._lblOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblName
            // 
            this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblName.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblName.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblName.Location = new System.Drawing.Point(3, 3);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(287, 21);
            this.lblName.TabIndex = 11;
            this.lblName.Text = "James P Heary";
            this.lblName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlProfile
            // 
            this.pnlProfile.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlProfile.Controls.Add(this.lblStatus);
            this.pnlProfile.Controls.Add(this.lblOffice);
            this.pnlProfile.Controls.Add(this.lblLocation);
            this.pnlProfile.Controls.Add(this.lblDepartment);
            this.pnlProfile.Controls.Add(this.lblOrganization);
            this.pnlProfile.Controls.Add(this._lblOffice);
            this.pnlProfile.Controls.Add(this._lblLocation);
            this.pnlProfile.Controls.Add(this._lblDepartment);
            this.pnlProfile.Controls.Add(this.picPhoto);
            this.pnlProfile.Controls.Add(this.picSignature);
            this.pnlProfile.Controls.Add(this._lblOrganization);
            this.pnlProfile.Controls.Add(this.lblBadge);
            this.pnlProfile.Controls.Add(this.lblName);
            this.pnlProfile.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlProfile.Location = new System.Drawing.Point(511, 3);
            this.pnlProfile.Name = "pnlProfile";
            this.pnlProfile.Padding = new System.Windows.Forms.Padding(3);
            this.pnlProfile.Size = new System.Drawing.Size(297, 387);
            this.pnlProfile.TabIndex = 12;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblStatus.Font = new System.Drawing.Font("Verdana", 8.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblStatus.Location = new System.Drawing.Point(244, 24);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(46, 13);
            this.lblStatus.TabIndex = 19;
            this.lblStatus.Text = "Active";
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblOffice
            // 
            this.lblOffice.Location = new System.Drawing.Point(117, 120);
            this.lblOffice.Name = "lblOffice";
            this.lblOffice.Size = new System.Drawing.Size(168, 18);
            this.lblOffice.TabIndex = 18;
            this.lblOffice.Text = "Office";
            this.lblOffice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblLocation
            // 
            this.lblLocation.Location = new System.Drawing.Point(117, 96);
            this.lblLocation.Name = "lblLocation";
            this.lblLocation.Size = new System.Drawing.Size(168, 18);
            this.lblLocation.TabIndex = 17;
            this.lblLocation.Text = "Jamesburg";
            this.lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDepartment
            // 
            this.lblDepartment.Location = new System.Drawing.Point(117, 72);
            this.lblDepartment.Name = "lblDepartment";
            this.lblDepartment.Size = new System.Drawing.Size(168, 18);
            this.lblDepartment.TabIndex = 16;
            this.lblDepartment.Text = "IT";
            this.lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrganization
            // 
            this.lblOrganization.Location = new System.Drawing.Point(117, 48);
            this.lblOrganization.Name = "lblOrganization";
            this.lblOrganization.Size = new System.Drawing.Size(168, 18);
            this.lblOrganization.TabIndex = 15;
            this.lblOrganization.Text = "Argix";
            this.lblOrganization.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblOffice
            // 
            this._lblOffice.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOffice.Location = new System.Drawing.Point(9, 120);
            this._lblOffice.Name = "_lblOffice";
            this._lblOffice.Size = new System.Drawing.Size(96, 18);
            this._lblOffice.TabIndex = 14;
            this._lblOffice.Text = "Office:";
            this._lblOffice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblLocation
            // 
            this._lblLocation.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLocation.Location = new System.Drawing.Point(9, 96);
            this._lblLocation.Name = "_lblLocation";
            this._lblLocation.Size = new System.Drawing.Size(96, 18);
            this._lblLocation.TabIndex = 13;
            this._lblLocation.Text = "Location:";
            this._lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblDepartment
            // 
            this._lblDepartment.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDepartment.Location = new System.Drawing.Point(9, 72);
            this._lblDepartment.Name = "_lblDepartment";
            this._lblDepartment.Size = new System.Drawing.Size(96, 18);
            this._lblDepartment.TabIndex = 12;
            this._lblDepartment.Text = "Department:";
            this._lblDepartment.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // picPhoto
            // 
            this.picPhoto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picPhoto.ContextMenuStrip = this.csMain;
            this.picPhoto.Location = new System.Drawing.Point(63, 153);
            this.picPhoto.Name = "picPhoto";
            this.picPhoto.Size = new System.Drawing.Size(172, 196);
            this.picPhoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPhoto.TabIndex = 7;
            this.picPhoto.TabStop = false;
            // 
            // picSignature
            // 
            this.picSignature.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picSignature.Location = new System.Drawing.Point(3, 390);
            this.picSignature.Name = "picSignature";
            this.picSignature.Size = new System.Drawing.Size(287, 72);
            this.picSignature.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picSignature.TabIndex = 8;
            this.picSignature.TabStop = false;
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
            this.msMain.Size = new System.Drawing.Size(819, 24);
            this.msMain.TabIndex = 14;
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
            this.msFileExport,
            this.msFileEmail,
            this.msFileSep3,
            this.msFilePageSetup,
            this.msFilePrint,
            this.msFilePrintEmp,
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
            this.msFileNew.Size = new System.Drawing.Size(168, 22);
            this.msFileNew.Text = "New...";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.Document;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(168, 22);
            this.msFileOpen.Text = "Open...";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(165, 6);
            // 
            // msFileSave
            // 
            this.msFileSave.Image = global::Argix.Properties.Resources.Save;
            this.msFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(168, 22);
            this.msFileSave.Text = "Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(168, 22);
            this.msFileSaveAs.Text = "Save As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(165, 6);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.msFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExport.Name = "msFileExport";
            this.msFileExport.Size = new System.Drawing.Size(168, 22);
            this.msFileExport.Text = "Export...";
            this.msFileExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileEmail
            // 
            this.msFileEmail.Image = global::Argix.Properties.Resources.Send;
            this.msFileEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileEmail.Name = "msFileEmail";
            this.msFileEmail.Size = new System.Drawing.Size(168, 22);
            this.msFileEmail.Text = "Send As Email";
            this.msFileEmail.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(165, 6);
            // 
            // msFilePageSetup
            // 
            this.msFilePageSetup.Image = global::Argix.Properties.Resources.PageSetup;
            this.msFilePageSetup.ImageTransparentColor = System.Drawing.Color.Black;
            this.msFilePageSetup.Name = "msFilePageSetup";
            this.msFilePageSetup.Size = new System.Drawing.Size(168, 22);
            this.msFilePageSetup.Text = "Print Setup...";
            this.msFilePageSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(168, 22);
            this.msFilePrint.Text = "Print Employees...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrintEmp
            // 
            this.msFilePrintEmp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrintEmp.Name = "msFilePrintEmp";
            this.msFilePrintEmp.Size = new System.Drawing.Size(168, 22);
            this.msFilePrintEmp.Text = "Print Employee...";
            this.msFilePrintEmp.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(168, 22);
            this.msFilePreview.Text = "Print Preview...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep4
            // 
            this.msFileSep4.Name = "msFileSep4";
            this.msFileSep4.Size = new System.Drawing.Size(165, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(168, 22);
            this.msFileExit.Text = "Exit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
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
            this.msEdit.Text = "Edit";
            // 
            // msEditCut
            // 
            this.msEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.msEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCut.Name = "msEditCut";
            this.msEditCut.Size = new System.Drawing.Size(109, 22);
            this.msEditCut.Text = "Cut";
            this.msEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditCopy
            // 
            this.msEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.msEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCopy.Name = "msEditCopy";
            this.msEditCopy.Size = new System.Drawing.Size(109, 22);
            this.msEditCopy.Text = "Copy";
            this.msEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditPaste
            // 
            this.msEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.msEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditPaste.Name = "msEditPaste";
            this.msEditPaste.Size = new System.Drawing.Size(109, 22);
            this.msEditPaste.Text = "Paste";
            this.msEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditSep1
            // 
            this.msEditSep1.Name = "msEditSep1";
            this.msEditSep1.Size = new System.Drawing.Size(106, 6);
            // 
            // msEditSearch
            // 
            this.msEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.msEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditSearch.Name = "msEditSearch";
            this.msEditSearch.Size = new System.Drawing.Size(109, 22);
            this.msEditSearch.Text = "Search";
            this.msEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewSubscriptions,
            this.msViewSep2,
            this.msViewFont,
            this.msViewSep3,
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
            this.msViewRefresh.Size = new System.Drawing.Size(145, 22);
            this.msViewRefresh.Text = "Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(142, 6);
            // 
            // msViewSubscriptions
            // 
            this.msViewSubscriptions.Name = "msViewSubscriptions";
            this.msViewSubscriptions.Size = new System.Drawing.Size(145, 22);
            this.msViewSubscriptions.Text = "Subscriptions";
            this.msViewSubscriptions.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(142, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(145, 22);
            this.msViewFont.Text = "Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep3
            // 
            this.msViewSep3.Name = "msViewSep3";
            this.msViewSep3.Size = new System.Drawing.Size(142, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(145, 22);
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(145, 22);
            this.msViewStatusBar.Text = "StatusBar";
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
            this.msHelp.Text = "Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(165, 22);
            this.msHelpAbout.Text = "&About IDViewer...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(162, 6);
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
            this.tsExport,
            this.tsSep1,
            this.tsPrint,
            this.tsEmail,
            this.tsSep2,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsSearch,
            this.tsSep3,
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(819, 54);
            this.tsMain.TabIndex = 15;
            this.tsMain.Text = "tsMain";
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 51);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Document;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 51);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 51);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
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
            this.tsExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 54);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 51);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print selected employee";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsEmail
            // 
            this.tsEmail.Image = global::Argix.Properties.Resources.Send;
            this.tsEmail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsEmail.Name = "tsEmail";
            this.tsEmail.Size = new System.Drawing.Size(40, 51);
            this.tsEmail.Text = "Email";
            this.tsEmail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsEmail.ToolTipText = "Email employee image";
            this.tsEmail.Click += new System.EventHandler(this.OnItemClick);
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
            this.tsCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsCopy
            // 
            this.tsCopy.Image = global::Argix.Properties.Resources.Copy;
            this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.Size = new System.Drawing.Size(39, 51);
            this.tsCopy.Text = "Copy";
            this.tsCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPaste
            // 
            this.tsPaste.Image = global::Argix.Properties.Resources.Paste;
            this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(39, 51);
            this.tsPaste.Text = "Paste";
            this.tsPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Find;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(36, 51);
            this.tsSearch.Text = "Find";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 54);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 51);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabBadges);
            this.tabMain.Controls.Add(this.tabRoutes);
            this.tabMain.Controls.Add(this.tabVisTrak);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 78);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(819, 419);
            this.tabMain.TabIndex = 16;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.OnTabChanged);
            // 
            // tabBadges
            // 
            this.tabBadges.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabBadges.Controls.Add(this.txtSearchSort);
            this.tabBadges.Controls.Add(this.grdBadges);
            this.tabBadges.Controls.Add(this.pnlProfile);
            this.tabBadges.Location = new System.Drawing.Point(4, 4);
            this.tabBadges.Name = "tabBadges";
            this.tabBadges.Padding = new System.Windows.Forms.Padding(3);
            this.tabBadges.Size = new System.Drawing.Size(811, 393);
            this.tabBadges.TabIndex = 0;
            this.tabBadges.Text = "ID Badges";
            // 
            // tabRoutes
            // 
            this.tabRoutes.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabRoutes.Controls.Add(this.dtpRouteDate);
            this.tabRoutes.Controls.Add(this.lblDate);
            this.tabRoutes.Controls.Add(this.cboDriver);
            this.tabRoutes.Controls.Add(this.cboRouteClass);
            this.tabRoutes.Controls.Add(this.lblDepot);
            this.tabRoutes.Controls.Add(this.lblDriver);
            this.tabRoutes.Controls.Add(this.rsRoutes);
            this.tabRoutes.Location = new System.Drawing.Point(4, 4);
            this.tabRoutes.Name = "tabRoutes";
            this.tabRoutes.Padding = new System.Windows.Forms.Padding(3);
            this.tabRoutes.Size = new System.Drawing.Size(811, 393);
            this.tabRoutes.TabIndex = 1;
            this.tabRoutes.Text = "Roadshow Routes";
            // 
            // dtpRouteDate
            // 
            this.dtpRouteDate.Location = new System.Drawing.Point(60, 7);
            this.dtpRouteDate.Name = "dtpRouteDate";
            this.dtpRouteDate.Size = new System.Drawing.Size(200, 21);
            this.dtpRouteDate.TabIndex = 11;
            this.dtpRouteDate.ValueChanged += new System.EventHandler(this.OnRouteDateChanged);
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.Location = new System.Drawing.Point(6, 6);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(48, 23);
            this.lblDate.TabIndex = 10;
            this.lblDate.Text = "Date";
            this.lblDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDriver
            // 
            this.cboDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDriver.FormattingEnabled = true;
            this.cboDriver.Location = new System.Drawing.Point(581, 7);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Size = new System.Drawing.Size(231, 21);
            this.cboDriver.TabIndex = 9;
            this.cboDriver.SelectionChangeCommitted += new System.EventHandler(this.OnDriverChanged);
            // 
            // cboRouteClass
            // 
            this.cboRouteClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRouteClass.FormattingEnabled = true;
            this.cboRouteClass.Location = new System.Drawing.Point(324, 7);
            this.cboRouteClass.Name = "cboRouteClass";
            this.cboRouteClass.Size = new System.Drawing.Size(192, 21);
            this.cboRouteClass.TabIndex = 8;
            this.cboRouteClass.SelectionChangeCommitted += new System.EventHandler(this.OnRouteClassChanged);
            // 
            // lblDepot
            // 
            this.lblDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepot.Location = new System.Drawing.Point(269, 6);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(48, 23);
            this.lblDepot.TabIndex = 7;
            this.lblDepot.Text = "Depot";
            this.lblDepot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDriver
            // 
            this.lblDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriver.Location = new System.Drawing.Point(525, 6);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(48, 23);
            this.lblDriver.TabIndex = 6;
            this.lblDriver.Text = "Driver";
            this.lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // rsRoutes
            // 
            this.rsRoutes.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rsRoutes.Location = new System.Drawing.Point(3, 34);
            this.rsRoutes.Name = "rsRoutes";
            this.rsRoutes.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsRoutes.ServerReport.DisplayName = "Security Route Detail";
            this.rsRoutes.ServerReport.ReportPath = "/Terminals/Security Route Detail";
            this.rsRoutes.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rsRoutes.ServerReport.Timeout = 30000;
            this.rsRoutes.ShowParameterPrompts = false;
            this.rsRoutes.Size = new System.Drawing.Size(805, 356);
            this.rsRoutes.TabIndex = 0;
            // 
            // tabVisTrak
            // 
            this.tabVisTrak.Location = new System.Drawing.Point(4, 4);
            this.tabVisTrak.Name = "tabVisTrak";
            this.tabVisTrak.Size = new System.Drawing.Size(811, 393);
            this.tabVisTrak.TabIndex = 2;
            this.tabVisTrak.Text = "Vistor Tracking";
            this.tabVisTrak.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(819, 521);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Securtiy Center";
            this.Closed += new System.EventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdBadges)).EndInit();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mBadges)).EndInit();
            this.pnlProfile.ResumeLayout(false);
            this.pnlProfile.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picPhoto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picSignature)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabBadges.ResumeLayout(false);
            this.tabBadges.PerformLayout();
            this.tabRoutes.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
        
        private void OnFormLoad(object sender,System.EventArgs e) {
			//Load conditions
			this.Cursor = Cursors.WaitCursor;
			try {
				//Initialize controls
				Splash.Close();
				this.Visible = true;
				Application.DoEvents();
				#region Set user preferences
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
                #endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
                #region Grid initialization
				this.grdBadges.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdBadges.DisplayLayout.Bands[0].Columns["LastName"].SortIndicator = SortIndicator.Ascending;
				this.grdBadges.DisplayLayout.Bands[0].ColumnFilters["Status"].FilterConditions.Clear();
				this.grdBadges.DisplayLayout.Bands[0].ColumnFilters["Status"].FilterConditions.Add(FilterComparisionOperator.Equals, "Active");
				this.grdBadges.DisplayLayout.RefreshFilters();
                #endregion

                //Set control defaults
                ServiceInfo t = BadgeGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";

                this.dtpRouteDate.Value = DateTime.Today;
                this.cboRouteClass.DisplayMember = "Name";
                this.cboRouteClass.ValueMember = "Number";
                this.cboDriver.DisplayMember = "Name";
                this.cboDriver.ValueMember = "Name";
                this.cboRouteClass.DataSource = RoadshowGateway.GetDepots();
                if(this.cboRouteClass.Items.Count > 0) this.cboRouteClass.SelectedIndex = 0;
                this.rsRoutes.ServerReport.DisplayName = "Security Route Detail";
                this.rsRoutes.ServerReport.ReportPath = "/Terminals/Security Route Detail";

                this.msViewRefresh.PerformClick();
			}
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
		private void OnFormClosed(object sender, System.EventArgs e) {
            //Event handler for form closed event
            global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
            global::Argix.Properties.Settings.Default.Location = this.Location;
            global::Argix.Properties.Settings.Default.Size = this.Size;
            global::Argix.Properties.Settings.Default.Font = this.Font;
            global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
            global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
            global::Argix.Properties.Settings.Default.LastVersion = App.Version;
            global::Argix.Properties.Settings.Default.Save();
        }
        private void OnTabChanged(object sender,EventArgs e) {
            //Event handler for change in view tab selected index
            this.Cursor = Cursors.WaitCursor;
            try {
                switch (this.tabMain.SelectedTab.Name) {
                    case "tabBadges":
                        break;
                    case "tabRoutes":
                        break;
                    case "tabVisTrak":
                        break;
                }
                setUserServices();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnBadgeSelected(object sender,AfterSelectChangeEventArgs e) {
            //Update totals
            this.Cursor = Cursors.WaitCursor;
            try {
                //Steal focus from search textbox
                this.grdBadges.Focus();

                //Set current employee
                this.mBadge = null;
                if(this.grdBadges.Selected.Rows.Count > 0) {
                    this.mMessageMgr.AddMessage("Retrieving badge...");
                    this.mBadge = (BadgeDataset.BadgeTableRow)this.mBadges.BadgeTable.Select("BadgeNumber=" + this.grdBadges.Selected.Rows[0].Cells["BadgeNumber"].Value.ToString())[0];
                }
                OnBadgeChanged(null,null);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnBadgeChanged(object sender,EventArgs e) {
            //Event handler for change in employee
            try {
                if(this.mBadge != null) {
                    //Set details of the selected badge
                    this.lblName.Text = this.mBadge.FirstName.Trim() + " " + this.mBadge.Middle.Trim() + " " + this.mBadge.LastName.Trim();
                    this.lblStatus.Text = this.mBadge.Status;
                    switch(this.mBadge.Status) {
                        case "Active":
                            this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
                            this.lblStatus.ForeColor = System.Drawing.SystemColors.ActiveCaption;
                            break;
                        case "Terminated":
                            this.lblName.BackColor = System.Drawing.Color.Red;
                            this.lblStatus.ForeColor = System.Drawing.Color.Red;
                            break;
                        default:
                            this.lblName.BackColor = System.Drawing.Color.Yellow;
                            this.lblStatus.ForeColor = System.Drawing.Color.Yellow;
                            break;
                    }
                    this.lblOrganization.Text = "Argix Logistics, Inc.";
                    this.lblDepartment.Text = this.mBadge.Department;
                    this.lblLocation.Text = this.mBadge.Location;
                    this.lblOffice.Text = "";
                    this.lblBadge.Text = "Badge# " + this.mBadge.BadgeNumber;
                    this.picPhoto.Image = !this.mBadge.IsPhotoNull() ? Image.FromStream(new MemoryStream(this.mBadge.Photo)) : null;
                    this.picSignature.Image = !this.mBadge.IsSignatureNull() ? Image.FromStream(new MemoryStream(this.mBadge.Signature)) : null;
                }
                else {
                    this.lblName.Text = "";
                    this.lblName.BackColor = System.Drawing.SystemColors.ActiveCaption;
                    this.lblStatus.Text = "";
                    this.lblStatus.ForeColor = System.Drawing.SystemColors.ControlText;
                    this.lblOrganization.Text = this.lblDepartment.Text = "";
                    this.lblLocation.Text = this.lblOffice.Text = "";
                    this.lblBadge.Text = "Badge# " + "";
                    this.picPhoto.Image = this.picSignature.Image = null;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnRouteDateChanged(object sender,EventArgs e) {
            //Event handler for change in route date
            this.rsRoutes.Clear();
            setUserServices();
        }
        private void OnRouteClassChanged(object sender,EventArgs e) {
            //Event handler for change in selected route class
            this.Cursor = Cursors.WaitCursor;
            try {
                this.rsRoutes.Clear();
                if (this.cboRouteClass.SelectedValue != null)
                    this.cboDriver.DataSource = RoadshowGateway.GetDrivers(int.Parse(this.cboRouteClass.SelectedValue.ToString()));
                if (this.cboDriver.Items.Count > 0) this.cboDriver.SelectedIndex = 0;
                OnDriverChanged(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnDriverChanged(object sender,EventArgs e) {
            //Event handler for change in driver
            this.rsRoutes.Clear();
            setUserServices();
        }
        #region Grid servics: OnBeforeRowFilterDropDownPopulate(), OnAfterRowFilterChanged(), OnGridMouseDown()
		private void OnBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
			//Event handler for before row filter drop down populates
			try {
				//Removes only blanks and non-blanks from default filter
				e.ValueList.ValueListItems.Remove(3);
				e.ValueList.ValueListItems.Remove(2);
				e.ValueList.ValueListItems.Remove(1);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnAfterRowFilterChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs e) {
			//Event handler for change in column row filtering
			try {
				//Set a valid selection
				if(this.grdBadges.Rows.VisibleRowCount > 0) {
					this.grdBadges.Rows.GetRowAtVisibleIndex(0).Selected = true;
					this.grdBadges.Rows.GetRowAtVisibleIndex(0).Activate();
				}
				OnBadgeSelected(null, null);
			}
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
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick(), OnPrintPage()
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
				    case "csSaveAs":
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
                        dlgSave.Filter = "Photo files (*.jpg)|*.jpg|Image files (*.gif)|*.gif";
						dlgSave.FilterIndex = 1;
						dlgSave.Title = "Save Photo As...";
						dlgSave.OverwritePrompt = true;
                        dlgSave.FileName = this.mBadge.LastName + ", " + this.mBadge.FirstName;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							if(dlgSave.FileName.Length > 0) {
								Image img = this.picPhoto.Image;
								Size size = new Size(img.Width, img.Height);
								Bitmap bmp = new Bitmap(size.Width, size.Height);
								Graphics g = Graphics.FromImage(bmp);
								g.SmoothingMode = SmoothingMode.HighQuality;
								g.InterpolationMode = InterpolationMode.HighQualityBicubic;
								g.PixelOffsetMode = PixelOffsetMode.HighQuality;
								Rectangle rect = new Rectangle(0, 0, size.Width, size.Height);
								g.DrawImage(img, rect, 0, 0, img.Width, img.Height, GraphicsUnit.Pixel);
								if(dlgSave.FilterIndex == 2) {
									foreach(PropertyItem item in img.PropertyItems) 
										bmp.SetPropertyItem(item);
									bmp.Save(dlgSave.FileName, System.Drawing.Imaging.ImageFormat.Gif);
								}
								else {
									ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
									ImageCodecInfo codec = null;
									for(int i=0; i<codecs.Length; i++) {
										if(codecs[i].MimeType.Equals("image/jpeg")) {
											codec = codecs[i];
											break;
										}
									}
									if (codec != null) {
										EncoderParameters encoderParams = new EncoderParameters(2);
										encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, 80L);
										encoderParams.Param[1] = new EncoderParameter(Encoder.ColorDepth, 24L);
										bmp.Save(dlgSave.FileName, codec, encoderParams);
									}
									else
										bmp.Save(dlgSave.FileName, ImageFormat.Jpeg);
								}
							}
						}
						break;
					case "msFileExport":		
					case "tsExport":	
						break;
					case "msFilePageSetup":	
					    UltraGridPrinter.PageSettings(); 
					    break;
					case "msFilePrint":
                        this.Cursor = Cursors.WaitCursor; 
                        UltraGridPrinter.Print(this.grdBadges,"Argix Employees",true);
                        break;
					case "msFilePrintEmp":
                    case "csPrint":
                        this.Cursor = Cursors.WaitCursor; 
                        this.mPrinter.Print("[IDNumber]", " ", true);
                        break;
                    case "tsPrint":
                        this.Cursor = Cursors.WaitCursor;
                        this.mPrinter.Print("[IDNumber]", " ", false);
                        break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdBadges,"Argix Employees"); break;
					case "msFileEmail":
					case "tsEmail":
						Microsoft.Office.Interop.Outlook.ApplicationClass app = new Microsoft.Office.Interop.Outlook.ApplicationClass();
						Microsoft.Office.Interop.Outlook.MailItem mail = (Microsoft.Office.Interop.Outlook.MailItem)app.CreateItem(Microsoft.Office.Interop.Outlook.OlItemType.olMailItem);
						//mail.Recipients.Add
						mail.Subject = this.mBadge.LastName + ", " + this.mBadge.FirstName;
						Bitmap _bmp = new Bitmap(this.picPhoto.Image);
						_bmp.Save("c:\\temp.bmp", System.Drawing.Imaging.ImageFormat.Bmp);
						mail.Attachments.Add("c:\\temp.bmp", Microsoft.Office.Interop.Outlook.OlAttachmentType.olByValue, 1, this.mBadge.LastName + ", " + this.mBadge.FirstName);
						mail.Display(this);
						break;
					case "msFileExit":			
					    this.Close(); 
					    break;
					case "msEditCut":			
					case "tsCut":
				    case "csCut":
					    break;
					case "msEditCopy":
                    case "tsCopy":		
                    case "csCopy":
                        Clipboard.SetDataObject(this.mBadge.Photo,true);
                        Clipboard.SetImage(this.picPhoto.Image);
					    break;
					case "msEditPaste":
                    case "tsPaste":	
                    case "csPaste":
					    break;
					case "msEditSearch":
                    case "tsSearch": 
                        this.mGridSvc.FindRow(0,this.grdBadges.Tag.ToString(),this.txtSearchSort.Text); 
                        break;
                    case "msViewRefresh":
                    case "tsRefresh":
                        switch (this.tabMain.SelectedTab.Name) {
                            case "tabBadges":
                                this.mMessageMgr.AddMessage("Refreshing badges...");
                                this.mBadges.Clear();
                                this.mBadges.Merge(BadgeGateway.ViewAccessControlBadges());
                                if(this.grdBadges.Rows.VisibleRowCount > 0) {
                                    this.grdBadges.Rows.GetRowAtVisibleIndex(0).Selected = true;
                                    this.grdBadges.Rows.GetRowAtVisibleIndex(0).Activate();
                                }
                                OnBadgeSelected(null,null);
                                break;
                            case "tabRoutes":
                                string snull = null;
                                ReportParameter p1 = new ReportParameter("RouteDate",this.dtpRouteDate.Value.ToString("yyyy-MM-dd"));
                                ReportParameter p2 = new ReportParameter("RouteClass",((Argix.Terminals.Depot)this.cboRouteClass.SelectedItem).Class);
                                ReportParameter p3 = new ReportParameter("DriverName",this.cboDriver.SelectedValue.ToString() == "All" ? snull : ((Argix.Terminals.Driver)this.cboDriver.SelectedItem).Name);
                                this.rsRoutes.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3 });
                                this.rsRoutes.RefreshReport();
                                break;
                        }
                        break;
                    case "msViewSubscriptions": new dlgSubscriptions().ShowDialog(); break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked));break;
                    case "msHelpAbout": new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnHelpMenuClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnPrintPage(object sender,System.Drawing.Printing.PrintPageEventArgs e) {
			//Provide the printing logic for the document
			try {
				//Print the current employee
				Font font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, ((System.Byte)(0)));
				float fX = e.MarginBounds.Left;
				float fY = e.MarginBounds.Top;
				StringFormat format = new StringFormat();
				float lineHeight = font.GetHeight(e.Graphics);
				
				e.Graphics.DrawString("Name:         " + this.mBadge.FirstName.Trim() + " " + this.mBadge.Middle.Trim() + " " + this.mBadge.LastName.Trim(), font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Status:       " + this.mBadge.Status, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				
				e.Graphics.DrawString("Organization: " + "Argix Logistics, Inc.", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Department:   " + this.mBadge.Department, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Location:     " + this.mBadge.Location, font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("Office:       " + "", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;
				e.Graphics.DrawString("", font, Brushes.Black, fX, fY, format);
				fY += lineHeight;

                Image photo = Image.FromStream(new MemoryStream(this.mBadge.Photo));
                Image signature = this.mBadge.Signature != null ? Image.FromStream(new MemoryStream(this.mBadge.Signature)) : null;
                if (photo != null) {
                    float width = 600;
                    float height = 600 * photo.Height / photo.Width;
                    e.Graphics.DrawImage(photo,fX,fY,width,height);
                    fY += height;
                    e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                    fY += lineHeight;
                    e.Graphics.DrawString("Badge# " + this.mBadge.BadgeNumber,font,Brushes.Black,fX,fY,format);
                    fY += lineHeight;
                    e.Graphics.DrawString("",font,Brushes.Black,fX,fY,format);
                    fY += lineHeight;
                    if (signature != null) e.Graphics.DrawImage(signature,fX,fY);
                }
				e.HasMorePages = false;
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
		}
		#endregion
		#region Local Services: setUserServices(), buildHelpMenu()
		private void setUserServices() {
			//Set user services
			try {				
				//Set menu states
                //RoleServiceGateway.IsSecurityDirector
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
				this.msFileSave.Enabled = this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = this.csSaveAs.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.mBadge != null);
                this.msFileExport.Enabled = this.tsExport.Enabled = false;    // (this.grdEmployees.Rows.Count > 0);
				this.msFilePageSetup.Enabled = true;
                this.msFilePrint.Enabled = this.tsPrint.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.grdBadges.Rows.Count > 0);
                this.msFilePrintEmp.Enabled = this.csPrint.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.mBadge != null);
                this.msFilePreview.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.mBadge != null);
                this.msFileEmail.Enabled = this.tsEmail.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.mBadge != null);
				this.msFileExit.Enabled = true;
				this.msEditCut.Enabled = this.tsCut.Enabled = this.csCut.Enabled = false;
                this.msEditCopy.Enabled = this.tsCopy.Enabled = this.csCopy.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.mBadge != null);
                this.msEditPaste.Enabled = this.tsPaste.Enabled = this.csPaste.Enabled = false;
                this.msEditSearch.Enabled = this.tsSearch.Enabled = (this.tabMain.SelectedTab == this.tabBadges && this.txtSearchSort.Text.Length > 0);
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(BadgeGateway.ServiceState,BadgeGateway.ServiceAddress));
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
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
		#endregion
	}
}
