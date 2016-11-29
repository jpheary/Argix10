using System;
using System.Collections;
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

namespace Argix.Terminals {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members		
        private DateTime mStartDate=DateTime.Today, mLastUpated=DateTime.Today;
		private bool mCalendarOpen=false, mGridDirty=false;
        private DeliveryPointsDataset mCustomers = null;
        private bool mPointMapLoaded = false, mRoadshowMapLoaded=false;
		
        private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
        #region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
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
        private ContextMenuStrip cmsMain;
        private ToolStripMenuItem cmsCut;
        private ToolStripMenuItem cmsCopy;
        private ToolStripMenuItem cmsPaste;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private DeliveryPointsDataset mPoints;
        private SplitContainer scMain;
        private SplitContainer scMaps;
        private WebBrowser wbPoint;
        private WebBrowser wbRoadshow;
        private TextBox txtPoint;
        private TextBox txtRoadshow;
        private Label lblPoint;
        private Label lblRoadshow;
        private ToolStripMenuItem msViewMaps;
        private ToolStripSeparator msViewSep3;
        private Label lblCloseMaps;
        private PointsWindowsControl pwcPoint;
        private RoadshowWindowsControl rwcCustomer;
		private System.ComponentModel.IContainer components;
		#endregion
		
		public frmMain() {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.wbPoint.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
                this.wbRoadshow.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
                this.Text = "Argix Logistics " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
				this.tsMain.Dock = DockStyle.Top;
                //this.Controls.AddRange(new Control[]{this.grdMain, this.tsMain, this.msMain, this.ssMain});
				this.grdMain.Controls.AddRange(new Control[]{this.dtpStartDate});
				#endregion
				
				//Create data and UI services
				this.mGridSvc = new UltraGridSvc(this.grdMain);
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryPointTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn127 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Command");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn128 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Account");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn129 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NickName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn130 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn131 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Building");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn132 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Route");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn133 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Phone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn134 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn135 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeMonday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn136 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeMonday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn137 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MondayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn138 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeTuesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn139 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeTuesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn140 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TuesdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn141 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeWednesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn142 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeWednesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn143 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WednesdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn144 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeThursday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn145 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeThursday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn146 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ThursdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn147 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeFriday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn148 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeFriday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn149 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FridayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn150 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeSaturday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn151 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeSaturday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn152 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SaturdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn153 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OpenTimeSunday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn154 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTimeSunday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn155 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SundayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn156 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ServiceTimeFactor");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn157 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Unit");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn158 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SetupTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn159 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Appt");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn160 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn161 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNickName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn162 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopPhone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn163 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Address");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn164 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn165 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("State");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn166 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn167 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeMonday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn168 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeMonday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn169 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopMondayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn170 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeTuesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn171 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeTuesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn172 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopTuesdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn173 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeWednesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn174 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeWednesday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn175 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopWednesdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn176 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeThursday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn177 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeThursday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn178 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopThursdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn179 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeFriday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn180 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeFriday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn181 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopFridayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn182 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeSaturday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn183 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeSaturday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn184 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopSaturdayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn185 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopOpenTimeSunday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn186 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopCloseTimeSunday");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn187 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopSundayWindow");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn188 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopComment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn189 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.cmsMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsCut = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsPaste = new System.Windows.Forms.ToolStripMenuItem();
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
            this.msEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewMaps = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this.mPoints = new Argix.DeliveryPointsDataset();
            this.scMaps = new System.Windows.Forms.SplitContainer();
            this.pwcPoint = new Argix.Terminals.PointsWindowsControl();
            this.lblPoint = new System.Windows.Forms.Label();
            this.lblCloseMaps = new System.Windows.Forms.Label();
            this.txtPoint = new System.Windows.Forms.TextBox();
            this.wbPoint = new System.Windows.Forms.WebBrowser();
            this.rwcCustomer = new Argix.Terminals.RoadshowWindowsControl();
            this.lblRoadshow = new System.Windows.Forms.Label();
            this.txtRoadshow = new System.Windows.Forms.TextBox();
            this.wbRoadshow = new System.Windows.Forms.WebBrowser();
            this.cmsMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.grdMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mPoints)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scMaps)).BeginInit();
            this.scMaps.Panel1.SuspendLayout();
            this.scMaps.Panel2.SuspendLayout();
            this.scMaps.SuspendLayout();
            this.lblPoint.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmsMain
            // 
            this.cmsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsCut,
            this.cmsCopy,
            this.cmsPaste});
            this.cmsMain.Name = "cmsMain";
            this.cmsMain.Size = new System.Drawing.Size(103, 70);
            // 
            // cmsCut
            // 
            this.cmsCut.Name = "cmsCut";
            this.cmsCut.Size = new System.Drawing.Size(102, 22);
            this.cmsCut.Text = "Cu&t";
            this.cmsCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // cmsCopy
            // 
            this.cmsCopy.Name = "cmsCopy";
            this.cmsCopy.Size = new System.Drawing.Size(102, 22);
            this.cmsCopy.Text = "&Copy";
            this.cmsCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // cmsPaste
            // 
            this.cmsPaste.Name = "cmsPaste";
            this.cmsPaste.Size = new System.Drawing.Size(102, 22);
            this.cmsPaste.Text = "&Paste";
            this.cmsPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 638);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(894, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 11;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // tsMain
            // 
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
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(894, 54);
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
            this.tsNew.ToolTipText = "New delivery points file";
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
            this.tsOpen.ToolTipText = "Open and import an existing delivery points file";
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
            this.tsSave.ToolTipText = "Save the current delivery points to a user specified file";
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
            this.tsExport.ToolTipText = "Export the current delivery points to the export file and update the LastUpdated " +
    "time";
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
            this.tsPrint.ToolTipText = "Print the current delivery points";
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
            this.tsRefresh.ToolTipText = "Refresh the delivery points";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClicked);
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
            this.msMain.Size = new System.Drawing.Size(894, 24);
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
            this.msFileNew.Size = new System.Drawing.Size(154, 22);
            this.msFileNew.Text = "&New...";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(154, 22);
            this.msFileOpen.Text = "&Open...";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(151, 6);
            // 
            // msFileSave
            // 
            this.msFileSave.Image = global::Argix.Properties.Resources.Save;
            this.msFileSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(154, 22);
            this.msFileSave.Text = "&Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(154, 22);
            this.msFileSaveAs.Text = "Save &As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileExport
            // 
            this.msFileExport.Image = global::Argix.Properties.Resources.XMLFile;
            this.msFileExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileExport.Name = "msFileExport";
            this.msFileExport.Size = new System.Drawing.Size(154, 22);
            this.msFileExport.Text = "&Export";
            this.msFileExport.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(151, 6);
            // 
            // msFileSettings
            // 
            this.msFileSettings.Image = global::Argix.Properties.Resources.PageSetup;
            this.msFileSettings.ImageTransparentColor = System.Drawing.Color.Black;
            this.msFileSettings.Name = "msFileSettings";
            this.msFileSettings.Size = new System.Drawing.Size(154, 22);
            this.msFileSettings.Text = "Page Settings...";
            this.msFileSettings.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(154, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(154, 22);
            this.msFilePreview.Text = "Print Pre&view...";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(151, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(154, 22);
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
            this.msViewMaps,
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
            this.msViewRefresh.Size = new System.Drawing.Size(123, 22);
            this.msViewRefresh.Text = "Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(120, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(123, 22);
            this.msViewFont.Text = "Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(120, 6);
            // 
            // msViewMaps
            // 
            this.msViewMaps.Name = "msViewMaps";
            this.msViewMaps.Size = new System.Drawing.Size(123, 22);
            this.msViewMaps.Text = "Maps";
            this.msViewMaps.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewSep3
            // 
            this.msViewSep3.Name = "msViewSep3";
            this.msViewSep3.Size = new System.Drawing.Size(120, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(123, 22);
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(123, 22);
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
            this.msHelpAbout.Size = new System.Drawing.Size(197, 22);
            this.msHelpAbout.Text = "&About Delivery Points...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(194, 6);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scMain.IsSplitterFixed = true;
            this.scMain.Location = new System.Drawing.Point(0, 78);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.grdMain);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scMaps);
            this.scMain.Size = new System.Drawing.Size(894, 560);
            this.scMain.SplitterDistance = 474;
            this.scMain.TabIndex = 15;
            // 
            // grdMain
            // 
            this.grdMain.CausesValidation = false;
            this.grdMain.ContextMenuStrip = this.cmsMain;
            this.grdMain.Controls.Add(this.dtpStartDate);
            this.grdMain.DataMember = "DeliveryPointTable";
            this.grdMain.DataSource = this.mPoints;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.ColHeaderLines = 3;
            ultraGridColumn127.Header.Caption = "Import\r\nCommand";
            ultraGridColumn127.Header.VisiblePosition = 0;
            ultraGridColumn127.Width = 75;
            ultraGridColumn128.Header.Caption = "Customer\r\nAccount#";
            ultraGridColumn128.Header.VisiblePosition = 1;
            ultraGridColumn128.Width = 75;
            ultraGridColumn129.Header.Caption = "Customer\r\nNickName";
            ultraGridColumn129.Header.VisiblePosition = 2;
            ultraGridColumn129.Hidden = true;
            ultraGridColumn129.Width = 75;
            ultraGridColumn130.Header.Caption = "Customer\r\nName";
            ultraGridColumn130.Header.VisiblePosition = 3;
            ultraGridColumn130.Width = 150;
            ultraGridColumn131.Header.Caption = "Customer\r\nBuilding";
            ultraGridColumn131.Header.VisiblePosition = 4;
            ultraGridColumn131.Width = 150;
            ultraGridColumn132.Header.Caption = "Customer\r\nRoute";
            ultraGridColumn132.Header.VisiblePosition = 5;
            ultraGridColumn132.Width = 100;
            ultraGridColumn133.Header.Caption = "Customer \r\nPhone#";
            ultraGridColumn133.Header.VisiblePosition = 6;
            ultraGridColumn133.Width = 75;
            ultraGridColumn134.Format = "MM/dd/yyyy";
            ultraGridColumn134.Header.Caption = "Customer\r\nOpen Date";
            ultraGridColumn134.Header.VisiblePosition = 7;
            ultraGridColumn134.Hidden = true;
            ultraGridColumn134.Width = 75;
            ultraGridColumn135.Format = "HHmm";
            ultraGridColumn135.Header.Caption = "Open\r\nMon";
            ultraGridColumn135.Header.VisiblePosition = 8;
            ultraGridColumn135.Width = 50;
            ultraGridColumn136.Format = "HHmm";
            ultraGridColumn136.Header.Caption = "Close\r\nMon";
            ultraGridColumn136.Header.VisiblePosition = 9;
            ultraGridColumn136.Width = 50;
            ultraGridColumn137.Header.Caption = "Mon\r\nWind";
            ultraGridColumn137.Header.VisiblePosition = 10;
            ultraGridColumn137.Width = 50;
            ultraGridColumn138.Format = "HHmm";
            ultraGridColumn138.Header.Caption = "Open\r\nTue";
            ultraGridColumn138.Header.VisiblePosition = 11;
            ultraGridColumn138.Width = 50;
            ultraGridColumn139.Format = "HHmm";
            ultraGridColumn139.Header.Caption = "Close\r\nTue";
            ultraGridColumn139.Header.VisiblePosition = 12;
            ultraGridColumn139.Width = 50;
            ultraGridColumn140.Header.Caption = "Tue\r\nWind";
            ultraGridColumn140.Header.VisiblePosition = 13;
            ultraGridColumn140.Width = 50;
            ultraGridColumn141.Format = "HHmm";
            ultraGridColumn141.Header.Caption = "Open\r\nWed";
            ultraGridColumn141.Header.VisiblePosition = 14;
            ultraGridColumn141.Width = 50;
            ultraGridColumn142.Format = "HHmm";
            ultraGridColumn142.Header.Caption = "Close\r\nWed";
            ultraGridColumn142.Header.VisiblePosition = 15;
            ultraGridColumn142.Width = 50;
            ultraGridColumn143.Header.Caption = "Wed\r\nWind";
            ultraGridColumn143.Header.VisiblePosition = 16;
            ultraGridColumn143.Width = 50;
            ultraGridColumn144.Format = "HHmm";
            ultraGridColumn144.Header.Caption = "Open\r\nThu";
            ultraGridColumn144.Header.VisiblePosition = 17;
            ultraGridColumn144.Width = 50;
            ultraGridColumn145.Format = "HHmm";
            ultraGridColumn145.Header.Caption = "Close\r\nThu";
            ultraGridColumn145.Header.VisiblePosition = 18;
            ultraGridColumn145.Width = 50;
            ultraGridColumn146.Header.Caption = "Thu\r\nWind";
            ultraGridColumn146.Header.VisiblePosition = 19;
            ultraGridColumn146.Width = 50;
            ultraGridColumn147.Format = "HHmm";
            ultraGridColumn147.Header.Caption = "Open\r\nFri";
            ultraGridColumn147.Header.VisiblePosition = 20;
            ultraGridColumn147.Width = 50;
            ultraGridColumn148.Format = "HHmm";
            ultraGridColumn148.Header.Caption = "Close\r\nFri";
            ultraGridColumn148.Header.VisiblePosition = 21;
            ultraGridColumn148.Width = 50;
            ultraGridColumn149.Header.Caption = "Fri\r\nWind";
            ultraGridColumn149.Header.VisiblePosition = 22;
            ultraGridColumn149.Width = 50;
            ultraGridColumn150.Format = "HHmm";
            ultraGridColumn150.Header.Caption = "Open\r\nSat";
            ultraGridColumn150.Header.VisiblePosition = 23;
            ultraGridColumn150.Width = 50;
            ultraGridColumn151.Format = "HHmm";
            ultraGridColumn151.Header.Caption = "Close\r\nSat";
            ultraGridColumn151.Header.VisiblePosition = 24;
            ultraGridColumn151.Width = 50;
            ultraGridColumn152.Header.Caption = "Sat\r\nWind";
            ultraGridColumn152.Header.VisiblePosition = 25;
            ultraGridColumn152.Width = 50;
            ultraGridColumn153.Format = "HHmm";
            ultraGridColumn153.Header.Caption = "Open\r\nSun";
            ultraGridColumn153.Header.VisiblePosition = 26;
            ultraGridColumn153.Width = 50;
            ultraGridColumn154.Format = "HHmm";
            ultraGridColumn154.Header.Caption = "Close\r\nSun";
            ultraGridColumn154.Header.VisiblePosition = 27;
            ultraGridColumn154.Width = 50;
            ultraGridColumn155.Header.Caption = "Sun\r\nWind";
            ultraGridColumn155.Header.VisiblePosition = 28;
            ultraGridColumn155.Width = 50;
            ultraGridColumn156.Header.Caption = "Customer\r\nSvc Time\r\nFactor";
            ultraGridColumn156.Header.VisiblePosition = 29;
            ultraGridColumn156.Hidden = true;
            ultraGridColumn156.Width = 50;
            ultraGridColumn157.Header.Caption = "Customer\r\nType\r\nUnit";
            ultraGridColumn157.Header.VisiblePosition = 30;
            ultraGridColumn157.Hidden = true;
            ultraGridColumn157.Width = 50;
            ultraGridColumn158.Header.Caption = "Customer\r\nSetup\r\nTime";
            ultraGridColumn158.Header.VisiblePosition = 31;
            ultraGridColumn158.Hidden = true;
            ultraGridColumn158.Width = 50;
            ultraGridColumn159.Header.VisiblePosition = 32;
            ultraGridColumn160.Header.Caption = "Stop \r\nName";
            ultraGridColumn160.Header.VisiblePosition = 33;
            ultraGridColumn160.Width = 150;
            ultraGridColumn161.Header.Caption = "Stop\r\nNick\r\nName";
            ultraGridColumn161.Header.VisiblePosition = 34;
            ultraGridColumn161.Hidden = true;
            ultraGridColumn161.Width = 75;
            ultraGridColumn162.Header.Caption = "Stop\r\nPhone";
            ultraGridColumn162.Header.VisiblePosition = 35;
            ultraGridColumn162.Width = 100;
            ultraGridColumn163.Header.Caption = "Stop\r\nAddress";
            ultraGridColumn163.Header.VisiblePosition = 36;
            ultraGridColumn163.Width = 125;
            ultraGridColumn164.Header.Caption = "Stop\r\nCity";
            ultraGridColumn164.Header.VisiblePosition = 37;
            ultraGridColumn164.Width = 100;
            ultraGridColumn165.Header.Caption = "Stop\r\nState";
            ultraGridColumn165.Header.VisiblePosition = 38;
            ultraGridColumn165.Width = 50;
            ultraGridColumn166.Header.Caption = "Stop\r\nZip";
            ultraGridColumn166.Header.VisiblePosition = 39;
            ultraGridColumn166.Width = 75;
            ultraGridColumn167.Header.Caption = "Stop\r\nOpen\r\nMon";
            ultraGridColumn167.Header.VisiblePosition = 40;
            ultraGridColumn167.Width = 50;
            ultraGridColumn168.Header.Caption = "Stop\r\nClose\r\nMon";
            ultraGridColumn168.Header.VisiblePosition = 41;
            ultraGridColumn168.Width = 50;
            ultraGridColumn169.Header.Caption = "Stop\r\nMon\r\nWind";
            ultraGridColumn169.Header.VisiblePosition = 42;
            ultraGridColumn169.Width = 50;
            ultraGridColumn170.Header.Caption = "Stop\r\nOpen\r\nTue";
            ultraGridColumn170.Header.VisiblePosition = 43;
            ultraGridColumn170.Width = 50;
            ultraGridColumn171.Header.Caption = "Stop\r\nClose\r\nTue";
            ultraGridColumn171.Header.VisiblePosition = 44;
            ultraGridColumn171.Width = 50;
            ultraGridColumn172.Header.Caption = "Stop\r\nTue\r\nWind";
            ultraGridColumn172.Header.VisiblePosition = 45;
            ultraGridColumn172.Width = 50;
            ultraGridColumn173.Header.Caption = "Stop\r\nOpen\r\nWed";
            ultraGridColumn173.Header.VisiblePosition = 46;
            ultraGridColumn173.Width = 50;
            ultraGridColumn174.Header.Caption = "Stop\r\nClose\r\nWed";
            ultraGridColumn174.Header.VisiblePosition = 47;
            ultraGridColumn174.Width = 50;
            ultraGridColumn175.Header.Caption = "Stop\r\nWed\r\nWind";
            ultraGridColumn175.Header.VisiblePosition = 48;
            ultraGridColumn175.Width = 50;
            ultraGridColumn176.Header.Caption = "Stop\r\nOpen\r\nThu";
            ultraGridColumn176.Header.VisiblePosition = 49;
            ultraGridColumn176.Width = 50;
            ultraGridColumn177.Header.Caption = "Stop\r\nClose\r\nThu";
            ultraGridColumn177.Header.VisiblePosition = 50;
            ultraGridColumn177.Width = 50;
            ultraGridColumn178.Header.Caption = "Stop\r\nThu\r\nWind";
            ultraGridColumn178.Header.VisiblePosition = 51;
            ultraGridColumn178.Width = 50;
            ultraGridColumn179.Header.Caption = "Stop\r\nOpen\r\nFri";
            ultraGridColumn179.Header.VisiblePosition = 52;
            ultraGridColumn179.Width = 50;
            ultraGridColumn180.Header.Caption = "Stop\r\nClose\r\nFri";
            ultraGridColumn180.Header.VisiblePosition = 53;
            ultraGridColumn180.Width = 50;
            ultraGridColumn181.Header.Caption = "Stop\r\nFri\r\nWind";
            ultraGridColumn181.Header.VisiblePosition = 54;
            ultraGridColumn181.Width = 50;
            ultraGridColumn182.Header.Caption = "Stop\r\nOpen\r\nSat";
            ultraGridColumn182.Header.VisiblePosition = 55;
            ultraGridColumn182.Width = 50;
            ultraGridColumn183.Header.Caption = "Stop\r\nClose\r\nSat";
            ultraGridColumn183.Header.VisiblePosition = 56;
            ultraGridColumn183.Width = 50;
            ultraGridColumn184.Header.Caption = "Stop\r\nSat\r\nWind";
            ultraGridColumn184.Header.VisiblePosition = 57;
            ultraGridColumn184.Width = 50;
            ultraGridColumn185.Header.Caption = "Stop\r\nOpen\r\nSun";
            ultraGridColumn185.Header.VisiblePosition = 58;
            ultraGridColumn185.Width = 50;
            ultraGridColumn186.Header.Caption = "Stop\r\nClose\r\nSun";
            ultraGridColumn186.Header.VisiblePosition = 59;
            ultraGridColumn186.Width = 50;
            ultraGridColumn187.Header.Caption = "Stop\r\nSun\r\nWind";
            ultraGridColumn187.Header.VisiblePosition = 60;
            ultraGridColumn187.Width = 50;
            ultraGridColumn188.Header.Caption = "Stop\r\nComment";
            ultraGridColumn188.Header.VisiblePosition = 61;
            ultraGridColumn189.Header.Caption = "Last\r\nUpdated";
            ultraGridColumn189.Header.VisiblePosition = 62;
            ultraGridColumn189.Hidden = true;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn127,
            ultraGridColumn128,
            ultraGridColumn129,
            ultraGridColumn130,
            ultraGridColumn131,
            ultraGridColumn132,
            ultraGridColumn133,
            ultraGridColumn134,
            ultraGridColumn135,
            ultraGridColumn136,
            ultraGridColumn137,
            ultraGridColumn138,
            ultraGridColumn139,
            ultraGridColumn140,
            ultraGridColumn141,
            ultraGridColumn142,
            ultraGridColumn143,
            ultraGridColumn144,
            ultraGridColumn145,
            ultraGridColumn146,
            ultraGridColumn147,
            ultraGridColumn148,
            ultraGridColumn149,
            ultraGridColumn150,
            ultraGridColumn151,
            ultraGridColumn152,
            ultraGridColumn153,
            ultraGridColumn154,
            ultraGridColumn155,
            ultraGridColumn156,
            ultraGridColumn157,
            ultraGridColumn158,
            ultraGridColumn159,
            ultraGridColumn160,
            ultraGridColumn161,
            ultraGridColumn162,
            ultraGridColumn163,
            ultraGridColumn164,
            ultraGridColumn165,
            ultraGridColumn166,
            ultraGridColumn167,
            ultraGridColumn168,
            ultraGridColumn169,
            ultraGridColumn170,
            ultraGridColumn171,
            ultraGridColumn172,
            ultraGridColumn173,
            ultraGridColumn174,
            ultraGridColumn175,
            ultraGridColumn176,
            ultraGridColumn177,
            ultraGridColumn178,
            ultraGridColumn179,
            ultraGridColumn180,
            ultraGridColumn181,
            ultraGridColumn182,
            ultraGridColumn183,
            ultraGridColumn184,
            ultraGridColumn185,
            ultraGridColumn186,
            ultraGridColumn187,
            ultraGridColumn188,
            ultraGridColumn189});
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.CaptionAppearance = appearance2;
            this.grdMain.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMain.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            appearance3.TextVAlignAsString = "Top";
            this.grdMain.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMain.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMain.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMain.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 0);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(474, 560);
            this.grdMain.TabIndex = 1;
            this.grdMain.Text = "Delivery Points";
            this.grdMain.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.AfterCellUpdate += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCellChanged);
            this.grdMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnInitializeRow);
            this.grdMain.AfterEnterEditMode += new System.EventHandler(this.OnGridAfterEnterEditMode);
            this.grdMain.AfterRowActivate += new System.EventHandler(this.OnGridRowActivated);
            this.grdMain.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnCellChanged);
            this.grdMain.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdMain.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnCellActivating);
            this.grdMain.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnBeforeRowFilterDropDownPopulate);
            this.grdMain.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpStartDate.CausesValidation = false;
            this.dtpStartDate.CustomFormat = "MM/dd/yyyy hh:mm tt";
            this.dtpStartDate.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpStartDate.Location = new System.Drawing.Point(317, 0);
            this.dtpStartDate.MaxDate = new System.DateTime(2031, 12, 31, 0, 0, 0, 0);
            this.dtpStartDate.MinDate = new System.DateTime(1990, 1, 1, 0, 0, 0, 0);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.ShowUpDown = true;
            this.dtpStartDate.Size = new System.Drawing.Size(156, 21);
            this.dtpStartDate.TabIndex = 2;
            this.dtpStartDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpStartDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // mPoints
            // 
            this.mPoints.DataSetName = "DeliveryPointsDataset";
            this.mPoints.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // scMaps
            // 
            this.scMaps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMaps.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMaps.Location = new System.Drawing.Point(0, 0);
            this.scMaps.Name = "scMaps";
            this.scMaps.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMaps.Panel1
            // 
            this.scMaps.Panel1.Controls.Add(this.pwcPoint);
            this.scMaps.Panel1.Controls.Add(this.lblPoint);
            this.scMaps.Panel1.Controls.Add(this.txtPoint);
            this.scMaps.Panel1.Controls.Add(this.wbPoint);
            // 
            // scMaps.Panel2
            // 
            this.scMaps.Panel2.Controls.Add(this.rwcCustomer);
            this.scMaps.Panel2.Controls.Add(this.lblRoadshow);
            this.scMaps.Panel2.Controls.Add(this.txtRoadshow);
            this.scMaps.Panel2.Controls.Add(this.wbRoadshow);
            this.scMaps.Size = new System.Drawing.Size(416, 560);
            this.scMaps.SplitterDistance = 280;
            this.scMaps.TabIndex = 0;
            // 
            // pwcPoint
            // 
            this.pwcPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pwcPoint.Location = new System.Drawing.Point(0, 233);
            this.pwcPoint.Name = "pwcPoint";
            this.pwcPoint.Point = null;
            this.pwcPoint.Size = new System.Drawing.Size(420, 45);
            this.pwcPoint.TabIndex = 5;
            // 
            // lblPoint
            // 
            this.lblPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPoint.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblPoint.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPoint.Controls.Add(this.lblCloseMaps);
            this.lblPoint.Location = new System.Drawing.Point(0, 0);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(415, 20);
            this.lblPoint.TabIndex = 2;
            this.lblPoint.Text = "Delivery Point";
            this.lblPoint.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCloseMaps
            // 
            this.lblCloseMaps.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseMaps.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCloseMaps.Location = new System.Drawing.Point(396, 2);
            this.lblCloseMaps.Name = "lblCloseMaps";
            this.lblCloseMaps.Size = new System.Drawing.Size(16, 16);
            this.lblCloseMaps.TabIndex = 4;
            this.lblCloseMaps.Text = "X";
            this.lblCloseMaps.TextAlign = System.Drawing.ContentAlignment.TopRight;
            this.lblCloseMaps.Click += new System.EventHandler(this.OnCloseMaps);
            this.lblCloseMaps.MouseEnter += new System.EventHandler(this.OnCloseMapsMouseEnter);
            this.lblCloseMaps.MouseLeave += new System.EventHandler(this.OnCloseMapsMouseLeave);
            // 
            // txtPoint
            // 
            this.txtPoint.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtPoint.Location = new System.Drawing.Point(0, 211);
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.Size = new System.Drawing.Size(415, 21);
            this.txtPoint.TabIndex = 1;
            this.txtPoint.TextChanged += new System.EventHandler(this.OnPointAddressChanged);
            // 
            // wbPoint
            // 
            this.wbPoint.AllowNavigation = false;
            this.wbPoint.AllowWebBrowserDrop = false;
            this.wbPoint.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbPoint.Location = new System.Drawing.Point(0, 20);
            this.wbPoint.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbPoint.Name = "wbPoint";
            this.wbPoint.ScriptErrorsSuppressed = true;
            this.wbPoint.ScrollBarsEnabled = false;
            this.wbPoint.Size = new System.Drawing.Size(415, 190);
            this.wbPoint.TabIndex = 0;
            this.wbPoint.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnPointDocumentCompleted);
            // 
            // rwcCustomer
            // 
            this.rwcCustomer.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rwcCustomer.Customer = null;
            this.rwcCustomer.Location = new System.Drawing.Point(0, 229);
            this.rwcCustomer.Name = "rwcCustomer";
            this.rwcCustomer.Size = new System.Drawing.Size(420, 45);
            this.rwcCustomer.TabIndex = 4;
            // 
            // lblRoadshow
            // 
            this.lblRoadshow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRoadshow.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblRoadshow.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblRoadshow.Location = new System.Drawing.Point(0, 0);
            this.lblRoadshow.Name = "lblRoadshow";
            this.lblRoadshow.Size = new System.Drawing.Size(415, 20);
            this.lblRoadshow.TabIndex = 3;
            this.lblRoadshow.Text = "Roadshow Customer";
            this.lblRoadshow.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtRoadshow
            // 
            this.txtRoadshow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtRoadshow.Location = new System.Drawing.Point(0, 207);
            this.txtRoadshow.Name = "txtRoadshow";
            this.txtRoadshow.Size = new System.Drawing.Size(415, 21);
            this.txtRoadshow.TabIndex = 2;
            this.txtRoadshow.TextChanged += new System.EventHandler(this.OnRoadshowAddressChanged);
            // 
            // wbRoadshow
            // 
            this.wbRoadshow.AllowNavigation = false;
            this.wbRoadshow.AllowWebBrowserDrop = false;
            this.wbRoadshow.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.wbRoadshow.Location = new System.Drawing.Point(0, 20);
            this.wbRoadshow.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbRoadshow.Name = "wbRoadshow";
            this.wbRoadshow.ScriptErrorsSuppressed = true;
            this.wbRoadshow.ScrollBarsEnabled = false;
            this.wbRoadshow.Size = new System.Drawing.Size(415, 185);
            this.wbRoadshow.TabIndex = 0;
            this.wbRoadshow.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnRoadshowDocumentCompleted);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(894, 662);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Delivery Points";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.cmsMain.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.grdMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mPoints)).EndInit();
            this.scMaps.Panel1.ResumeLayout(false);
            this.scMaps.Panel1.PerformLayout();
            this.scMaps.Panel2.ResumeLayout(false);
            this.scMaps.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMaps)).EndInit();
            this.scMaps.ResumeLayout(false);
            this.lblPoint.ResumeLayout(false);
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
                    this.msViewMaps.Checked = !(this.scMain.Panel2Collapsed = !Convert.ToBoolean(global::Argix.Properties.Settings.Default.Maps));
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
				this.mToolTip.SetToolTip(this.dtpStartDate, "Select a start date for the oldest delivery point.");
				#endregion
				
				//Set control defaults
				#region Grid Overrides
				this.grdMain.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
				this.grdMain.DisplayLayout.Bands[0].Override.AllowUpdate = DefaultableBoolean.True;
				this.grdMain.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
				this.grdMain.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdMain.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
				this.grdMain.DisplayLayout.Bands[0].Columns["Command"].AllowRowFiltering = DefaultableBoolean.True;
                this.grdMain.DisplayLayout.Bands[0].Columns["Account"].SortIndicator = SortIndicator.Ascending;
				#endregion

                ServiceInfo t = TerminalsGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 150;
                this.ssMain.User2Panel.Width = 175;
                try {
                    //Set start date for delivery point records to last updated datetime
                    this.mLastUpated = TerminalsGateway.GetExportDate();
                } catch { }
                this.mStartDate = this.mLastUpated;
                this.dtpStartDate.MinDate = new DateTime(1990,1,1,0,0,0,0);
				this.dtpStartDate.MaxDate = new DateTime(DateTime.Today.Year,DateTime.Today.Month,DateTime.Today.Day,23,59,59,999);
				this.dtpStartDate.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
				this.dtpStartDate.Value = this.mStartDate;
                this.mCustomers = TerminalsGateway.GetCustomers();
                this.msViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Ask only if there are detail forms open
            if(!e.Cancel) {
                //Save settings
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Maps = this.msViewMaps.Checked;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnPointDocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) { this.mPointMapLoaded = true; OnPointAddressChanged(null,EventArgs.Empty); }
        private void OnRoadshowDocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) { this.mRoadshowMapLoaded = true; OnRoadshowAddressChanged(null,EventArgs.Empty); }
        private void OnCloseMaps(object sender,EventArgs e) { this.msViewMaps.PerformClick(); }
        private void OnCloseMapsMouseEnter(object sender,EventArgs e) { this.lblCloseMaps.BackColor = System.Drawing.SystemColors.Info; }
        private void OnCloseMapsMouseLeave(object sender,EventArgs e) { this.lblCloseMaps.BackColor = System.Drawing.SystemColors.InactiveCaption; }
        private void OnPointAddressChanged(object sender,EventArgs e) {
            //
            try {
                if (this.mPointMapLoaded && this.wbPoint.Document != null && this.txtPoint.Text.Trim().Length > 0) {
                    this.wbPoint.Document.InvokeScript("MapLocation",new object[] { this.txtPoint.Text });
                }
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnRoadshowAddressChanged(object sender,EventArgs e) {
            //
            try {
                if (this.mRoadshowMapLoaded && this.wbRoadshow.Document != null && this.txtRoadshow.Text.Trim().Length > 0) {
                    this.wbRoadshow.Document.InvokeScript("MapLocation",new object[] { this.txtRoadshow.Text });
                }
                else
                    this.wbRoadshow.Document.InvokeScript("MapLocation",null);
            }
            catch (Exception ex) { App.ReportError(ex); }
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
				this.dtpStartDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; sync calendars & change terminal pickup date
				this.mCalendarOpen = false;
                OnCalendarValueChanged(null,EventArgs.Empty);
            }
			catch(Exception ex) { App.ReportError(ex); }
        }
		private void OnCalendarValueChanged(object sender, System.EventArgs e) {
			//Event handler for pickup date changed
			try {
				//Change terminal start date if the calendar is closed
                if(!this.mCalendarOpen) {
                    if(DateTime.Compare(this.dtpStartDate.Value,this.mStartDate) != 0) {
                        DialogResult refresh = DialogResult.Yes;
				        if(this.mGridDirty) 
					        refresh = MessageBox.Show(this, "The current data has been modified. Do you want to overwrite these changes?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
				        if(refresh == DialogResult.Yes) {
                            this.mStartDate = this.dtpStartDate.Value;
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Refreshing...");
                            this.mPoints.Clear();
                            this.mPoints.Merge(TerminalsGateway.GetDeliveryPoints(this.mStartDate,this.mLastUpated));
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mPoints.DeliveryPointTable.Rows.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                        }
                        else {
                            this.dtpStartDate.ValueChanged -= new System.EventHandler(this.OnCalendarValueChanged);
                            this.dtpStartDate.Value = this.mStartDate;
                            this.dtpStartDate.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
                        }
                    }
				}
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
        #region Grid Support: OnInitializeRow(), OnBeforeRowFilterDropDownPopulate(), OnGridSelectionChanged(), OnGridMouseDown(), OnCellActivating(), OnGridAfterEnterEditMode(), OnGridKeyUp(), OnCellChanged(), OnGridAfterRowUpdate()
        private void OnInitializeRow(object sender, InitializeRowEventArgs e) {
            //Event handler for intialize row event
            try {
                string accountID = e.Row.Cells["Account"].Value.ToString();
                DeliveryPointsDataset.CustomerTableRow customer = null;
                try { customer = (DeliveryPointsDataset.CustomerTableRow)this.mCustomers.CustomerTable.Select("AccountID = '" + accountID.Trim() + "'")[0]; } catch { }
                if (customer != null) {
                    if (e.Row.Cells["Name"].Value.ToString().Trim() != customer.CustomerName.Trim()) e.Row.Cells["Name"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["Building"].Value.ToString().Trim() != customer.CustomerAddress.Trim()) e.Row.Cells["Building"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenMondayNull() && int.Parse(e.Row.Cells["OpenTimeMonday"].Value.ToString()) != customer.CustomerWindowOpenMonday) e.Row.Cells["OpenTimeMonday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseMondayNull() && int.Parse(e.Row.Cells["CloseTimeMonday"].Value.ToString()) != customer.CustomerWindowCloseMonday) e.Row.Cells["CloseTimeMonday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenTuesdayNull() && int.Parse(e.Row.Cells["OpenTimeTuesday"].Value.ToString()) != customer.CustomerWindowOpenTuesday) e.Row.Cells["OpenTimeTuesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseTuesdayNull() && int.Parse(e.Row.Cells["CloseTimeTuesday"].Value.ToString()) != customer.CustomerWindowCloseTuesday) e.Row.Cells["CloseTimeTuesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenWednesdayNull() && int.Parse(e.Row.Cells["OpenTimeWednesday"].Value.ToString()) != customer.CustomerWindowOpenWednesday) e.Row.Cells["OpenTimeWednesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseWednesdayNull() && int.Parse(e.Row.Cells["CloseTimeWednesday"].Value.ToString()) != customer.CustomerWindowCloseWednesday) e.Row.Cells["CloseTimeWednesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenThursdayNull() && int.Parse(e.Row.Cells["OpenTimeThursday"].Value.ToString()) != customer.CustomerWindowOpenThursday) e.Row.Cells["OpenTimeThursday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseThursdayNull() && int.Parse(e.Row.Cells["CloseTimeThursday"].Value.ToString()) != customer.CustomerWindowCloseThursday) e.Row.Cells["CloseTimeThursday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenFridayNull() && int.Parse(e.Row.Cells["OpenTimeFriday"].Value.ToString()) != customer.CustomerWindowOpenFriday) e.Row.Cells["OpenTimeFriday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseFridayNull() && int.Parse(e.Row.Cells["CloseTimeFriday"].Value.ToString()) != customer.CustomerWindowCloseFriday) e.Row.Cells["CloseTimeFriday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenSaturdayNull() && int.Parse(e.Row.Cells["OpenTimeSaturday"].Value.ToString()) != customer.CustomerWindowOpenSaturday) e.Row.Cells["OpenTimeSaturday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseSaturdayNull() && int.Parse(e.Row.Cells["CloseTimeSaturday"].Value.ToString()) != customer.CustomerWindowCloseSaturday) e.Row.Cells["CloseTimeSaturday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowOpenSundayNull() && int.Parse(e.Row.Cells["OpenTimeSunday"].Value.ToString()) != customer.CustomerWindowOpenSunday) e.Row.Cells["OpenTimeSunday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsCustomerWindowCloseSundayNull() && int.Parse(e.Row.Cells["CloseTimeSunday"].Value.ToString()) != customer.CustomerWindowCloseSunday) e.Row.Cells["CloseTimeSunday"].Appearance.BackColor = Color.Red;

                    if (e.Row.Cells["StopName"].Value.ToString().Trim() != customer.StopName.Trim()) e.Row.Cells["StopName"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["Address"].Value.ToString().Trim() != customer.StopAddress.Trim()) e.Row.Cells["Address"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["City"].Value.ToString().Trim() != customer.StopCity.Trim()) e.Row.Cells["City"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["State"].Value.ToString().Trim() != customer.StopState.Trim()) e.Row.Cells["State"].Appearance.BackColor = Color.Red;
                    if (e.Row.Cells["Zip"].Value.ToString().Trim() != customer.StopZip.Trim()) e.Row.Cells["Zip"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenMondayNull() && int.Parse(e.Row.Cells["StopOpenTimeMonday"].Value.ToString().Trim()) != customer.StopWindowOpenMonday) e.Row.Cells["StopOpenTimeMonday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseMondayNull() && int.Parse(e.Row.Cells["StopCloseTimeMonday"].Value.ToString().Trim()) != customer.StopWindowCloseMonday) e.Row.Cells["StopCloseTimeMonday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenTuesdayNull() && int.Parse(e.Row.Cells["StopOpenTimeTuesday"].Value.ToString().Trim()) != customer.StopWindowOpenTuesday) e.Row.Cells["StopOpenTimeTuesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseTuesdayNull() && int.Parse(e.Row.Cells["StopCloseTimeTuesday"].Value.ToString().Trim()) != customer.StopWindowCloseTuesday) e.Row.Cells["StopCloseTimeTuesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenWednesdayNull() && int.Parse(e.Row.Cells["StopOpenTimeWednesday"].Value.ToString().Trim()) != customer.StopWindowOpenWednesday) e.Row.Cells["StopOpenTimeWednesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseWednesdayNull() && int.Parse(e.Row.Cells["StopCloseTimeWednesday"].Value.ToString().Trim()) != customer.StopWindowCloseWednesday) e.Row.Cells["StopCloseTimeWednesday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenThursdayNull() && int.Parse(e.Row.Cells["StopOpenTimeThursday"].Value.ToString().Trim()) != customer.StopWindowOpenThursday) e.Row.Cells["StopOpenTimeThursday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseThursdayNull() && int.Parse(e.Row.Cells["StopCloseTimeThursday"].Value.ToString().Trim()) != customer.StopWindowCloseThursday) e.Row.Cells["StopCloseTimeThursday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenFridayNull() && int.Parse(e.Row.Cells["StopOpenTimeFriday"].Value.ToString().Trim()) != customer.StopWindowOpenFriday) e.Row.Cells["StopOpenTimeFriday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseFridayNull() && int.Parse(e.Row.Cells["StopCloseTimeFriday"].Value.ToString().Trim()) != customer.StopWindowCloseFriday) e.Row.Cells["StopCloseTimeFriday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenSaturdayNull() && int.Parse(e.Row.Cells["StopOpenTimeSaturday"].Value.ToString().Trim()) != customer.StopWindowOpenSaturday) e.Row.Cells["StopOpenTimeSaturday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseSaturdayNull() && int.Parse(e.Row.Cells["StopCloseTimeSaturday"].Value.ToString().Trim()) != customer.StopWindowCloseSaturday) e.Row.Cells["StopCloseTimeSaturday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowOpenSundayNull() && int.Parse(e.Row.Cells["StopOpenTimeSunday"].Value.ToString().Trim()) != customer.StopWindowOpenSunday) e.Row.Cells["StopOpenTimeSunday"].Appearance.BackColor = Color.Red;
                    if (!customer.IsStopWindowCloseSundayNull() && int.Parse(e.Row.Cells["StopCloseTimeSunday"].Value.ToString().Trim()) != customer.StopWindowCloseSunday) e.Row.Cells["StopCloseTimeSunday"].Appearance.BackColor = Color.Red;
                }
                else
                    e.Row.Appearance.BackColor = Color.Yellow;

                //Re format the output for field [CU ROU] from “Floor Grid:xx”  to “Floor Grid: xx”  (ad a space after the colon)
                string route = e.Row.Cells["Route"].Value.ToString();
                if (!route.Contains(": ")) e.Row.Cells["Route"].Value = route.Replace(":",": ");
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Event handler for before row filter drop down populates
            try {
                //Removes only blanks and non-blanks from default filter
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) { }
        private void OnGridRowActivated(object sender,EventArgs e) {
            //Event handler for selection change
            try {
                if(this.grdMain.ActiveRow != null) {
                    //Get point key
                    string accountID = this.grdMain.ActiveRow.Cells["Account"].Value.ToString().Trim();

                    //Set point map and window control
                    this.txtPoint.Text = this.grdMain.ActiveRow.Cells["Address"].Value.ToString().Trim() + " " + this.grdMain.ActiveRow.Cells["City"].Value.ToString().Trim() + ", " + this.grdMain.ActiveRow.Cells["State"].Value.ToString().Trim() + " " + this.grdMain.ActiveRow.Cells["Zip"].Value.ToString().Trim();
                    OnPointAddressChanged(null,EventArgs.Empty);
                    DeliveryPointsDataset.DeliveryPointTableRow point = (DeliveryPointsDataset.DeliveryPointTableRow)this.mPoints.DeliveryPointTable.Select("Account = '" + accountID.Trim() + "'")[0]; ;
                    this.pwcPoint.Point = point;

                    //Set roadshow map and window control
                    this.txtRoadshow.Text = "";
                    DeliveryPointsDataset.CustomerTableRow customer = null;
                    try { customer = (DeliveryPointsDataset.CustomerTableRow)this.mCustomers.CustomerTable.Select("AccountID = '" + accountID.Trim() + "'")[0]; } catch { }
                    if (customer != null) this.txtRoadshow.Text = customer.StopAddress.Trim() + " " + customer.StopCity.Trim() + ", " + customer.StopState.Trim() + " " + customer.StopZip.Trim();
                    OnRoadshowAddressChanged(null,EventArgs.Empty);
                    this.rwcCustomer.Customer = customer;
                }
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(uiElement != null) {
                    object oContext = uiElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)oContext;
                            if (!row.Selected) grid.Selected.Rows.Clear();
                            row.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(uiElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if (grid.ActiveRow != null) grid.ActiveRow = null;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnCellActivating(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for cell activated
            try {
                //Set cell editing
                switch(e.Cell.Column.Key.ToString()) {
                    case "NickName":
                    case "ServiceTimeFactor":
                    case "Unit":
                    case "SetupTime":
                    case "StopOpenTimeMonday": 
                    case "StopCloseTimeMonday": 
                    case "StopMondayWindow": 
                    case "StopOpenTimeTuesday": 
                    case "StopCloseTimeTuesday": 
                    case "StopTuesdayWindow": 
                    case "StopOpenTimeWednesday": 
                    case "StopCloseTimeWednesday": 
                    case "StopWednesdayWindow": 
                    case "StopOpenTimeThursday": 
                    case "StopCloseTimeThursday": 
                    case "StopThursdayWindow": 
                    case "StopOpenTimeFriday": 
                    case "StopCloseTimeFriday": 
                    case "StopFridayWindow": 
                    case "StopOpenTimeSaturday": 
                    case "StopCloseTimeSaturday": 
                    case "StopSaturdayWindow": 
                    case "StopOpenTimeSunday": 
                    case "StopCloseTimeSunday": 
                    case "StopSundayWindow": 
                       e.Cell.Activation = Activation.AllowEdit;
                        break;
                    default:
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { setUserServices(); }
        }
        private void OnGridAfterEnterEditMode(object sender,System.EventArgs e) {
            //Event handler for 
            setUserServices();
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            if(e.KeyCode == Keys.Enter) {
                //Update row on Enter
                this.grdMain.ActiveRow.Update();
                e.Handled = true;
            }
        }
        private void OnCellChanged(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a cell value
            try {
                //Flag data as dirty
                this.mGridDirty = true;

                //Apply cell rules
                switch(e.Cell.Column.Key.ToString()) {
                    case "NickName":
                        //Max 8 chars (i.e. 3 char mnemonic plus 5 char store#)
                        if(e.Cell.Text.Length > 8) {
                            e.Cell.Value = e.Cell.Text.Substring(0,8);
                            e.Cell.SelStart = 8;
                        }
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) { 
            //
            this.grdMain.Update();
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
                        DialogResult open = DialogResult.Yes;
                        if(this.mGridDirty)
                            open = MessageBox.Show(this,"The current data has been modified. Do you want to overwrite these changes?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if(open == DialogResult.Yes) {
                            OpenFileDialog dlgOpen = new OpenFileDialog();
                            dlgOpen.AddExtension = true;
                            dlgOpen.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                            dlgOpen.FilterIndex = 0;
                            dlgOpen.Title = "Open Delivery Points File...";
                            dlgOpen.FileName = App.Config.ExportFile;
                            if(dlgOpen.ShowDialog(this)==DialogResult.OK) {
                                //Open file
                                this.Cursor = Cursors.WaitCursor;
                                this.mPoints.Clear();
                                this.mPoints.Merge(importDeliveryPoints(dlgOpen.FileName));
                                this.mGridDirty = false;
                                this.mMessageMgr.AddMessage(this.mPoints.DeliveryPointTable.Rows.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                                this.grdMain.Refresh();
                            }
                        }
                        break;
                    case "msFileSave":
                    case "tsSave":
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Data Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Delivery Points As...";
                        dlgSave.FileName = "";
                        dlgSave.OverwritePrompt = false;
                        if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            if (dlgSave.FileName.EndsWith("xls")) {
                                new Argix.ExcelFormat().Transform(this.mPoints,dlgSave.FileName);
                            }
                            else {
                                this.mPoints.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                            }
                            this.mMessageMgr.AddMessage("Saved to " + dlgSave.FileName + ".");
                        }
                        break;
                    case "msFileExport":
                        SaveFileDialog dlgExport = new SaveFileDialog();
                        dlgExport.AddExtension = true;
                        dlgExport.Filter = "Text Files (*.txt) | *.txt";
                        dlgExport.FilterIndex = 0;
                        dlgExport.Title = "Export Delivery Points To...";
                        dlgExport.FileName = App.Config.ExportFile;
                        dlgExport.OverwritePrompt = true;
                        if(dlgExport.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            App.Config.ExportFile = dlgExport.FileName;
                            this.mLastUpated = exportDeliveryPoints(dlgExport.FileName,this.mLastUpated);
                            TerminalsGateway.UpdateExportDate(this.mLastUpated);
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mPoints.DeliveryPointTable.Rows.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                            this.mMessageMgr.AddMessage("Delivery points exported to " + dlgExport.FileName + ".");
                        }
                        break;
                    case "tsExport":
                        this.Cursor = Cursors.WaitCursor;
                        this.mLastUpated = exportDeliveryPoints(App.Config.ExportFile,this.mLastUpated);
                        TerminalsGateway.UpdateExportDate(this.mLastUpated);
                        this.mGridDirty = false;
                        this.mMessageMgr.AddMessage(this.mPoints.DeliveryPointTable.Rows.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                        this.grdMain.Refresh();
                        this.mMessageMgr.AddMessage("Delivery points exported to " + App.Config.ExportFile + ".");
                        break;
                    case "msFileSettings": UltraGridPrinter.PageSettings(); break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdMain,"Delivery Points"); break;
                    case "msFilePrint": UltraGridPrinter.Print(this.grdMain,"Delivery Points",true); break;
                    case "tsPrint": UltraGridPrinter.Print(this.grdMain,"Delivery Points",false); break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "cmsCut":
                    case "tsCut":
                        Clipboard.SetDataObject(this.grdMain.ActiveCell.SelText,false);
                        this.grdMain.ActiveCell.Value = this.grdMain.ActiveCell.Text.Remove(this.grdMain.ActiveCell.SelStart,this.grdMain.ActiveCell.SelLength);
                        break;
                    case "msEditCopy":
                    case "cmsCopy":
                    case "tsCopy":
                        Clipboard.SetDataObject(this.grdMain.ActiveCell.SelText,false);
                        break;
                    case "msEditPaste":
                    case "cmsPaste":
                    case "tsPaste":
                        IDataObject o = Clipboard.GetDataObject();
                        this.grdMain.ActiveCell.Value = this.grdMain.ActiveCell.Text.Remove(this.grdMain.ActiveCell.SelStart,this.grdMain.ActiveCell.SelLength).Insert(this.grdMain.ActiveCell.SelStart,(string)o.GetData("Text"));
                        break;
                    case "msEditSearch": 
                    case "tsSearch":
                        break;
                    case "msViewRefresh":
                    case "tsRefresh":
                        //Refresh pickups collection
                        DialogResult refresh = DialogResult.Yes;
                        if(this.mGridDirty)
                            refresh = MessageBox.Show(this,"The current data has been modified. Do you want to overwrite these changes?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                        if(refresh == DialogResult.Yes) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Refreshing...");
                            this.mPoints.Clear();
                            this.mPoints.Merge(TerminalsGateway.GetDeliveryPoints(this.mStartDate,this.mLastUpated));
                            this.mGridDirty = false;
                            this.mMessageMgr.AddMessage(this.mPoints.DeliveryPointTable.Rows.Count.ToString() + " delivery points for " + this.mStartDate.ToString("MM/dd/yyyy hh:mm tt") + " - " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt") + ".");
                            this.grdMain.Refresh();
                        }
                        break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewMaps": this.scMain.Panel2Collapsed = !(this.msViewMaps.Checked = (!this.msViewMaps.Checked)); break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
                    case "msViewStatusBar": this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
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
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception) { }
        }
        #endregion
        #region Local Services: importDeliveryPoints(), exportDeliveryPoints(), setUserServices(), buildHelpMenu()
        private DeliveryPointsDataset importDeliveryPoints(string filename) {
            //Import delivery points from a file into the current dataset
            DeliveryPointsDataset points = new DeliveryPointsDataset();
            StreamReader reader=null;
            const string CSV_DELIM = ",";
            try {
                //Clear dataset and fetch new data from the export file
                reader = new StreamReader(filename,System.Text.Encoding.ASCII);
                string line = reader.ReadLine();
                while(line != null) {
                    string[] tokens = line.Split(Convert.ToChar(CSV_DELIM));
                    DeliveryPointsDataset.DeliveryPointTableRow point = points.DeliveryPointTable.NewDeliveryPointTableRow();
                    #region Copy tokens to datarow
                    point.Command = tokens[0];
                    point.Account = tokens[1];
                    point.Name = tokens[2];
                    point.Building = tokens[3];
                    point.Route = tokens[4];
                    point.Phone = tokens[5];
                    point.OpenTimeMonday = tokens[6];
                    point.CloseTimeMonday = tokens[7];
                    point.MondayWindow = tokens[8];
                    point.OpenTimeTuesday = tokens[9];
                    point.CloseTimeTuesday = tokens[10];
                    point.TuesdayWindow = tokens[11];
                    point.OpenTimeWednesday = tokens[12];
                    point.CloseTimeWednesday = tokens[13];
                    point.WednesdayWindow = tokens[14];
                    point.OpenTimeThursday = tokens[15];
                    point.CloseTimeThursday = tokens[16];
                    point.ThursdayWindow = tokens[17];
                    point.OpenTimeFriday = tokens[18];
                    point.CloseTimeFriday = tokens[19];
                    point.FridayWindow = tokens[20];
                    point.OpenTimeSaturday = tokens[21];
                    point.CloseTimeSaturday = tokens[22];
                    point.SaturdayWindow = tokens[23];
                    point.OpenTimeSunday = tokens[24];
                    point.CloseTimeSunday = tokens[25];
                    point.SundayWindow = tokens[26];
                    point.Appt = tokens[27];
                    point.StopName = tokens[28];
                    point.StopPhone = tokens[29];
                    point.Address = tokens[30];
                    point.City = tokens[31];
                    point.State = tokens[32];
                    point.Zip = tokens[33];
                    point.StopOpenTimeMonday = tokens[34];
                    point.StopCloseTimeMonday = tokens[35];
                    point.StopMondayWindow = tokens[36];
                    point.StopOpenTimeTuesday = tokens[37];
                    point.StopCloseTimeTuesday = tokens[38];
                    point.StopTuesdayWindow = tokens[39];
                    point.StopOpenTimeWednesday = tokens[40];
                    point.StopCloseTimeWednesday = tokens[41];
                    point.StopWednesdayWindow = tokens[42];
                    point.StopOpenTimeThursday = tokens[43];
                    point.StopCloseTimeThursday = tokens[44];
                    point.StopThursdayWindow = tokens[45];
                    point.StopOpenTimeFriday = tokens[46];
                    point.StopCloseTimeFriday = tokens[47];
                    point.StopFridayWindow = tokens[48];
                    point.StopOpenTimeSaturday = tokens[49];
                    point.StopCloseTimeSaturday = tokens[50];
                    point.StopSaturdayWindow = tokens[51];
                    point.StopOpenTimeSunday = tokens[52];
                    point.StopCloseTimeSunday = tokens[53];
                    point.StopSundayWindow = tokens[54];
                    point.StopComment = tokens[55];
                    #endregion
                    points.DeliveryPointTable.AddDeliveryPointTableRow(point);
                    line = reader.ReadLine();
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while importing delivery points from " + filename + ".",ex); }
            finally { if(reader != null) reader.Close(); }
            return points;
        }
        private DateTime exportDeliveryPoints(string filename,DateTime lastUpdated) {
            //Exports delivery points dataset to a file
            DateTime lastUpdate=lastUpdated;
            StreamWriter writer=null;
            const string CSV_DELIM = ",";

            //Determine export filename (application or user specified)
            string exportFile = filename.Length > 0 ? filename : App.Config.ExportFile;
            try {
                //Create the export file and save to disk
                writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                for (int i = 0;i < this.mPoints.DeliveryPointTable.Rows.Count;i++) {
                    //Create a delivery point and persist as CSV
                    DeliveryPointsDataset.DeliveryPointTableRow point = this.mPoints.DeliveryPointTable[i];
                    #region Copy datarow to csv record
                    string line = point.Command + CSV_DELIM +
                                    point.Account + CSV_DELIM +
                                    point.Name + CSV_DELIM +
                                    point.Building + CSV_DELIM +
                                    point.Route + CSV_DELIM +
                                    point.Phone + CSV_DELIM +
                                    point.OpenTimeMonday + CSV_DELIM +
                                    point.CloseTimeMonday + CSV_DELIM +
                                    point.MondayWindow + CSV_DELIM +
                                    point.OpenTimeTuesday + CSV_DELIM +
                                    point.CloseTimeTuesday + CSV_DELIM +
                                    point.TuesdayWindow + CSV_DELIM +
                                    point.OpenTimeWednesday + CSV_DELIM +
                                    point.CloseTimeWednesday + CSV_DELIM +
                                    point.WednesdayWindow + CSV_DELIM +
                                    point.OpenTimeThursday + CSV_DELIM +
                                    point.CloseTimeThursday + CSV_DELIM +
                                    point.ThursdayWindow + CSV_DELIM +
                                    point.OpenTimeFriday + CSV_DELIM +
                                    point.CloseTimeFriday + CSV_DELIM +
                                    point.FridayWindow + CSV_DELIM +
                                    point.OpenTimeSaturday + CSV_DELIM +
                                    point.CloseTimeSaturday + CSV_DELIM +
                                    point.SaturdayWindow + CSV_DELIM +
                                    point.OpenTimeSunday + CSV_DELIM +
                                    point.CloseTimeSunday + CSV_DELIM +
                                    point.SundayWindow + CSV_DELIM +
                                    point.Appt + CSV_DELIM +
                                    point.StopName + CSV_DELIM +
                                    point.StopPhone + CSV_DELIM +
                                    point.Address + CSV_DELIM +
                                    point.City + CSV_DELIM +
                                    point.State + CSV_DELIM +
                                    point.Zip + CSV_DELIM +
                                    point.StopOpenTimeMonday + CSV_DELIM +
                                    point.StopCloseTimeMonday + CSV_DELIM +
                                    point.StopMondayWindow + CSV_DELIM +
                                    point.StopOpenTimeTuesday + CSV_DELIM +
                                    point.StopCloseTimeTuesday + CSV_DELIM +
                                    point.StopTuesdayWindow + CSV_DELIM +
                                    point.StopOpenTimeWednesday + CSV_DELIM +
                                    point.StopCloseTimeWednesday + CSV_DELIM +
                                    point.StopWednesdayWindow + CSV_DELIM +
                                    point.StopOpenTimeThursday + CSV_DELIM +
                                    point.StopCloseTimeThursday + CSV_DELIM +
                                    point.StopThursdayWindow + CSV_DELIM +
                                    point.StopOpenTimeFriday + CSV_DELIM +
                                    point.StopCloseTimeFriday + CSV_DELIM +
                                    point.StopFridayWindow + CSV_DELIM +
                                    point.StopOpenTimeSaturday + CSV_DELIM +
                                    point.StopCloseTimeSaturday + CSV_DELIM +
                                    point.StopSaturdayWindow + CSV_DELIM +
                                    point.StopOpenTimeSunday + CSV_DELIM +
                                    point.StopCloseTimeSunday + CSV_DELIM +
                                    point.StopSundayWindow + CSV_DELIM +
                                    point.StopComment;
                    #endregion
                    writer.WriteLine(line);

                    //Capture for most recent last updated datetime
                    if(point.LastUpdated.CompareTo(lastUpdate) > 0) lastUpdate = point.LastUpdated;
                }
                writer.Flush();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while exporting delivery points to " + exportFile + ".",ex); }
            finally { if(writer != null) writer.Close(); }
            return lastUpdate;
        }
		private void setUserServices() {
			//Set user services
			try {
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen.Enabled = true;
				this.msFileSave.Enabled = this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = this.mPoints.DeliveryPointTable.Rows.Count > 0;
                this.msFileExport.Enabled = this.tsExport.Enabled = (RoleServiceGateway.IsRS && this.mPoints.DeliveryPointTable.Rows.Count > 0);
				this.msFileSettings.Enabled = true;
                this.msFilePreview.Enabled = this.msFilePrint.Enabled = this.tsPrint.Enabled = (this.mPoints.DeliveryPointTable.Rows.Count > 0);
				this.msFileExit.Enabled = true;
				this.msEditCut.Enabled = this.cmsCut.Enabled = this.tsCut.Enabled = (this.grdMain.ActiveCell != null && this.grdMain.ActiveCell.IsInEditMode);
                this.msEditCopy.Enabled = this.cmsCopy.Enabled = this.tsCopy.Enabled = (this.grdMain.ActiveCell != null && this.grdMain.ActiveCell.IsInEditMode);
				this.msEditPaste.Enabled = this.cmsPaste.Enabled = this.tsPaste.Enabled = (this.grdMain.ActiveCell != null && this.grdMain.ActiveCell.IsInEditMode && Clipboard.GetDataObject() != null);
				this.msEditSearch.Enabled = this.tsSearch.Enabled = false;
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
				this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(TerminalsGateway.ServiceState,TerminalsGateway.ServiceAddress));
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
                this.ssMain.User2Panel.Icon = null;
                this.ssMain.User2Panel.ToolTipText = "Last updated datetime ... # of delivery points";
                this.ssMain.User2Panel.Text = this.mLastUpated.ToString("MM/dd/yyyy hh:mm tt") + " ... " + this.mPoints.DeliveryPointTable.Rows.Count.ToString() + "pts";
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
    }
}