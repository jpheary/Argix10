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
using System.Threading;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.Reporting.WinForms;
using Argix.Windows;

namespace Argix.Customers {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members        
        private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
        private UltraGridSvc mGridSvcIssues=null;
        private TrayIcon mTrayIcon=null;
        private ReminderService mReminders=null;

        private DateTime mLastNewIssueTime=DateTime.Now;
        private System.Collections.Hashtable mReadIssues=new Hashtable();
        private Issue mIssue = null;

		#region Controls

        private System.Windows.Forms.ToolBarButton btnEdit;
        private Argix.Windows.ArgixStatusBar stbMain;
        private System.ComponentModel.IContainer components;
        private ToolStripMenuItem mnuFile;
        private ToolStripMenuItem mnuEdit;
        private ToolStripMenuItem mnuView;
        private ToolStripMenuItem mnuTools;
        private ToolStripMenuItem mnuHelp;
        private ToolStripMenuItem mnuViewRefresh;
        private ToolStripSeparator mnuViewSep1;
        private ToolStripMenuItem mnuViewToolbar;
        private ToolStripMenuItem mnuViewStatusBar;
        private ToolStripMenuItem mnuToolsConfig;
        private ToolStripMenuItem mnuHelpAbout;
        private ToolStripSeparator mnuHelpSep1;
        private ToolStripMenuItem mnuFileNew;
        private ToolStripMenuItem mnuFileOpen;
        private ToolStripSeparator mnuFileSep1;
        private ToolStripMenuItem mnuFileSaveAs;
        private ToolStripSeparator mnuFileSep2;
        private ToolStripSeparator mnuFileSep3;
        private ToolStripMenuItem mnuFileSetup;
        private ToolStripMenuItem mnuFilePrint;
        private ToolStripMenuItem mnuFilePreview;
        private ToolStripMenuItem mnuFileExit;
        private ToolStripMenuItem mnuEditCut;
        private ToolStripMenuItem mnuEditCopy;
        private ToolStripMenuItem mnuEditPaste;
        private ToolStripSeparator mnuEditSep1;
        private ToolStripMenuItem mnuEditSearch;
        private ToolStripMenuItem mnuFileSave;
        private ToolStripMenuItem mnuFileActionNew;
        private ToolStripSeparator mnuFileSep4;
        private ToolStripMenuItem mnuViewFont;
        private ToolStripSeparator mnuViewSep2;
        private ToolStripMenuItem mnuViewShowAlert;
        private ToolStripMenuItem mnuViewHideWhenMin;
        private ToolStripSeparator mnuViewSep3;
        private Panel pnlToolbox;
        private TabControl tabToolbox;
        private TabPage tabSearch;
        private Panel pnlToolboxTitlebar;
        private Label lblPin;
        private Label lblToolbox;
        private System.Windows.Forms.Timer tmrAutoHide;
        private TextBox txtZone;
        private Label _lblZone;
        private TextBox txtCoordinator;
        private Label _lblCoordinator;
        private TextBox txtOriginator;
        private Label _lblOriginator;
        private TextBox txtContact;
        private Label _lblContact;
        private TextBox txtSubject;
        private Label _lblSubject;
        private TextBox txtReceived;
        private Label _lblReceived;
        private TextBox txtAction;
        private Label _lblAction;
        private TextBox txtType;
        private Label _lblType;
        private TextBox txtCompany;
        private Label _lblCompany;
        private TextBox txtAgent;
        private Label _lblAgent;
        private TextBox txtStore;
        private Label _lblStore;
        private Button btnSearch;
        private Button btnReset;
        private ImageList imlMain;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdIssues;
        private ContextMenuStrip csMain;
        private ToolStripMenuItem ctxNew;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem csRefresh;
        private ToolStrip tsMain;
        private ToolStripComboBox cboView;
        private ToolStripButton btnNew;
        private ToolStripButton btnOpen;
        private ToolStripButton btnSave;
        private ToolStripSeparator btnSep1;
        private ToolStripButton btnPrint;
        private ToolStripSeparator btnSep2;
        private ToolStripButton btnRefresh;
        private ToolStripSeparator btnSep3;
        private ToolStripComboBox cboSearch;
        private ToolStripLabel tslFilters;
        private CRMDataset mIssues;
        private SplitContainer scMain;
        private SplitContainer scActions;
        private ListView lsvActions;
        private System.Windows.Forms.ColumnHeader colName;
        private System.Windows.Forms.ColumnHeader colDateTime;
        private ToolStrip tsActions;
        private ToolStripButton btnActionsNew;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton btnActionsRefresh;
        private ToolStripSeparator ctxSep2;
        private ToolStripButton btnActionsPrint;
        private ToolStripSeparator ctxSep3;
        private ToolStripButton btnActionsOpenAttach;
        private ToolStripButton btnActionsAttach;
        private Panel pnlAction;
        private ListView lsvAttachments;
        private RichTextBox rtbAction;
        private Label lblAction;
        private ImageList imgActions;
        private ContextMenuStrip csActions;
        private ToolStripMenuItem csActionsNew;
        private ToolStripSeparator toolStripMenuItem1;
        private ToolStripMenuItem csActionsRefresh;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem csActionsPrint;
        private CRMDataset mFindIssues;
        private TabPage tabDeliveries;
        private TabPage tabStrTrack;
        private TabControl tabMain;
        private TabPage tabIssues;
        private TabPage tabClient;
        private TabPage tabAgent;
        private SplitContainer scParent;
        private TabPage tabCtnTrack;
        private ToolStripMenuItem csReminder;
        private ToolStripSeparator toolStripMenuItem2;
        private ToolStripMenuItem csClearFlag;
        private Label lblTitle;
        private DeliveryTool mDeliveryTool;
        private StoreTrackingTool mStoreTrackingTool;
        private CartonTrackingTool mCartonTrackingTool;
        private TabPage tabManager;
        private ManagerDashboardControl2 managerDashboardControl21;
        private ClientDashboardControl clientDashboardControl1;
        private AgentDashboardControl agentDashboardControl1;
        private MenuStrip msMain;
		
		#endregion
		
        //Interface
		public frmMain() {
			try {
				InitializeComponent();
                InitializeToolbox();
                this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
                this.msMain.Dock = this.tsMain.Dock = DockStyle.Top;
                this.stbMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tsMain,this.msMain,this.stbMain });
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);

                #region Tray Icon
                this.mTrayIcon = new TrayIcon("CRM",this.Icon);
                this.mTrayIcon.Visible = true;
                this.mTrayIcon.Unhide += new EventHandler(OnTrayIconUnhide);
                this.mTrayIcon.HideWhenMinimizedChanged += new EventHandler(OnTrayIconHideWhenMinimizedChanged);
                #endregion
                this.mGridSvcIssues = new UltraGridSvc(this.grdIssues);                
                this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.stbMain.Panels[0], 500,3000);
                this.mReminders = new ReminderService();
                this.mReminders.OpenItem += new ReminderEventHandler(OnOpenReminderItem);
            }
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("IssueTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Contact");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CompanyName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RegionNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DistrictNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StoreNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionCreated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FirstActionComment");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionCreated", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActionUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Coordinator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientRep");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.btnEdit = new System.Windows.Forms.ToolBarButton();
            this.stbMain = new Argix.Windows.ArgixStatusBar();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.mnuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileActionNew = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFilePreview = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuFileSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuEditSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuEditSearch = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuView = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewShowAlert = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewHideWhenMin = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.mnuViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuTools = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.mnuHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.pnlToolbox = new System.Windows.Forms.Panel();
            this.tabToolbox = new System.Windows.Forms.TabControl();
            this.tabSearch = new System.Windows.Forms.TabPage();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            this.txtCoordinator = new System.Windows.Forms.TextBox();
            this._lblCoordinator = new System.Windows.Forms.Label();
            this.txtOriginator = new System.Windows.Forms.TextBox();
            this._lblOriginator = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this._lblContact = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this._lblSubject = new System.Windows.Forms.Label();
            this.txtReceived = new System.Windows.Forms.TextBox();
            this._lblReceived = new System.Windows.Forms.Label();
            this.txtAction = new System.Windows.Forms.TextBox();
            this._lblAction = new System.Windows.Forms.Label();
            this.txtType = new System.Windows.Forms.TextBox();
            this._lblType = new System.Windows.Forms.Label();
            this.txtCompany = new System.Windows.Forms.TextBox();
            this._lblCompany = new System.Windows.Forms.Label();
            this.txtAgent = new System.Windows.Forms.TextBox();
            this._lblAgent = new System.Windows.Forms.Label();
            this.txtStore = new System.Windows.Forms.TextBox();
            this._lblStore = new System.Windows.Forms.Label();
            this.txtZone = new System.Windows.Forms.TextBox();
            this._lblZone = new System.Windows.Forms.Label();
            this.tabDeliveries = new System.Windows.Forms.TabPage();
            this.mDeliveryTool = new Argix.Customers.DeliveryTool();
            this.tabStrTrack = new System.Windows.Forms.TabPage();
            this.mStoreTrackingTool = new Argix.Customers.StoreTrackingTool();
            this.tabCtnTrack = new System.Windows.Forms.TabPage();
            this.mCartonTrackingTool = new Argix.Customers.CartonTrackingTool();
            this.imlMain = new System.Windows.Forms.ImageList(this.components);
            this.pnlToolboxTitlebar = new System.Windows.Forms.Panel();
            this.lblPin = new System.Windows.Forms.Label();
            this.lblToolbox = new System.Windows.Forms.Label();
            this.tmrAutoHide = new System.Windows.Forms.Timer(this.components);
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxNew = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.csReminder = new System.Windows.Forms.ToolStripMenuItem();
            this.csClearFlag = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.cboView = new System.Windows.Forms.ToolStripComboBox();
            this.btnNew = new System.Windows.Forms.ToolStripButton();
            this.btnOpen = new System.Windows.Forms.ToolStripButton();
            this.btnSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSave = new System.Windows.Forms.ToolStripButton();
            this.btnPrint = new System.Windows.Forms.ToolStripButton();
            this.btnSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.btnSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.cboSearch = new System.Windows.Forms.ToolStripComboBox();
            this.tslFilters = new System.Windows.Forms.ToolStripLabel();
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.grdIssues = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mIssues = new Argix.CRMDataset();
            this.scActions = new System.Windows.Forms.SplitContainer();
            this.lsvActions = new System.Windows.Forms.ListView();
            this.colName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colDateTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.csActions = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csActionsNew = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.csActionsRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.csActionsPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.imgActions = new System.Windows.Forms.ImageList(this.components);
            this.tsActions = new System.Windows.Forms.ToolStrip();
            this.btnActionsNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActionsRefresh = new System.Windows.Forms.ToolStripButton();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActionsPrint = new System.Windows.Forms.ToolStripButton();
            this.ctxSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnActionsOpenAttach = new System.Windows.Forms.ToolStripButton();
            this.btnActionsAttach = new System.Windows.Forms.ToolStripButton();
            this.pnlAction = new System.Windows.Forms.Panel();
            this.lsvAttachments = new System.Windows.Forms.ListView();
            this.rtbAction = new System.Windows.Forms.RichTextBox();
            this.lblAction = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabIssues = new System.Windows.Forms.TabPage();
            this.tabClient = new System.Windows.Forms.TabPage();
            this.clientDashboardControl1 = new Argix.Customers.ClientDashboardControl();
            this.tabAgent = new System.Windows.Forms.TabPage();
            this.agentDashboardControl1 = new Argix.Customers.AgentDashboardControl();
            this.tabManager = new System.Windows.Forms.TabPage();
            this.managerDashboardControl21 = new Argix.Customers.ManagerDashboardControl2();
            this.scParent = new System.Windows.Forms.SplitContainer();
            this.mFindIssues = new Argix.CRMDataset();
            this.msMain.SuspendLayout();
            this.pnlToolbox.SuspendLayout();
            this.tabToolbox.SuspendLayout();
            this.tabSearch.SuspendLayout();
            this.tabDeliveries.SuspendLayout();
            this.tabStrTrack.SuspendLayout();
            this.tabCtnTrack.SuspendLayout();
            this.pnlToolboxTitlebar.SuspendLayout();
            this.csMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mIssues)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scActions)).BeginInit();
            this.scActions.Panel1.SuspendLayout();
            this.scActions.Panel2.SuspendLayout();
            this.scActions.SuspendLayout();
            this.csActions.SuspendLayout();
            this.tsActions.SuspendLayout();
            this.pnlAction.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabIssues.SuspendLayout();
            this.tabClient.SuspendLayout();
            this.tabAgent.SuspendLayout();
            this.tabManager.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scParent)).BeginInit();
            this.scParent.Panel1.SuspendLayout();
            this.scParent.Panel2.SuspendLayout();
            this.scParent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mFindIssues)).BeginInit();
            this.SuspendLayout();
            // 
            // btnEdit
            // 
            this.btnEdit.Name = "btnEdit";
            // 
            // stbMain
            // 
            this.stbMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.stbMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.stbMain.Location = new System.Drawing.Point(0, 520);
            this.stbMain.Name = "stbMain";
            this.stbMain.Size = new System.Drawing.Size(944, 24);
            this.stbMain.StatusText = "";
            this.stbMain.TabIndex = 1;
            this.stbMain.TerminalText = "Terminal";
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFile,
            this.mnuEdit,
            this.mnuView,
            this.mnuTools,
            this.mnuHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(944, 24);
            this.msMain.TabIndex = 117;
            this.msMain.Text = "Standard";
            // 
            // mnuFile
            // 
            this.mnuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuFileNew,
            this.mnuFileOpen,
            this.mnuFileSep1,
            this.mnuFileActionNew,
            this.mnuFileSep2,
            this.mnuFileSave,
            this.mnuFileSaveAs,
            this.mnuFileSep3,
            this.mnuFileSetup,
            this.mnuFilePrint,
            this.mnuFilePreview,
            this.mnuFileSep4,
            this.mnuFileExit});
            this.mnuFile.Name = "mnuFile";
            this.mnuFile.Size = new System.Drawing.Size(37, 20);
            this.mnuFile.Text = "&File";
            // 
            // mnuFileNew
            // 
            this.mnuFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.mnuFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileNew.Name = "mnuFileNew";
            this.mnuFileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.mnuFileNew.Size = new System.Drawing.Size(187, 22);
            this.mnuFileNew.Text = "&New Issue...";
            this.mnuFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileOpen
            // 
            this.mnuFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.mnuFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileOpen.Name = "mnuFileOpen";
            this.mnuFileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.mnuFileOpen.Size = new System.Drawing.Size(187, 22);
            this.mnuFileOpen.Text = "&Open...";
            this.mnuFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep1
            // 
            this.mnuFileSep1.Name = "mnuFileSep1";
            this.mnuFileSep1.Size = new System.Drawing.Size(184, 6);
            // 
            // mnuFileActionNew
            // 
            this.mnuFileActionNew.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileActionNew.Image")));
            this.mnuFileActionNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileActionNew.Name = "mnuFileActionNew";
            this.mnuFileActionNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.mnuFileActionNew.Size = new System.Drawing.Size(187, 22);
            this.mnuFileActionNew.Text = "New A&ction...";
            this.mnuFileActionNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep2
            // 
            this.mnuFileSep2.Name = "mnuFileSep2";
            this.mnuFileSep2.Size = new System.Drawing.Size(184, 6);
            // 
            // mnuFileSave
            // 
            this.mnuFileSave.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSave.Image")));
            this.mnuFileSave.Name = "mnuFileSave";
            this.mnuFileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.mnuFileSave.Size = new System.Drawing.Size(187, 22);
            this.mnuFileSave.Text = "&Save";
            this.mnuFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSaveAs
            // 
            this.mnuFileSaveAs.Name = "mnuFileSaveAs";
            this.mnuFileSaveAs.Size = new System.Drawing.Size(187, 22);
            this.mnuFileSaveAs.Text = "Save &As...";
            this.mnuFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep3
            // 
            this.mnuFileSep3.Name = "mnuFileSep3";
            this.mnuFileSep3.Size = new System.Drawing.Size(184, 6);
            // 
            // mnuFileSetup
            // 
            this.mnuFileSetup.Image = ((System.Drawing.Image)(resources.GetObject("mnuFileSetup.Image")));
            this.mnuFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuFileSetup.Name = "mnuFileSetup";
            this.mnuFileSetup.Size = new System.Drawing.Size(187, 22);
            this.mnuFileSetup.Text = "Page Set&up...";
            this.mnuFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePrint
            // 
            this.mnuFilePrint.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePrint.Image")));
            this.mnuFilePrint.Name = "mnuFilePrint";
            this.mnuFilePrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.mnuFilePrint.Size = new System.Drawing.Size(187, 22);
            this.mnuFilePrint.Text = "&Print...";
            this.mnuFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFilePreview
            // 
            this.mnuFilePreview.Image = ((System.Drawing.Image)(resources.GetObject("mnuFilePreview.Image")));
            this.mnuFilePreview.Name = "mnuFilePreview";
            this.mnuFilePreview.Size = new System.Drawing.Size(187, 22);
            this.mnuFilePreview.Text = "Print Pre&view...";
            this.mnuFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuFileSep4
            // 
            this.mnuFileSep4.Name = "mnuFileSep4";
            this.mnuFileSep4.Size = new System.Drawing.Size(184, 6);
            // 
            // mnuFileExit
            // 
            this.mnuFileExit.Name = "mnuFileExit";
            this.mnuFileExit.Size = new System.Drawing.Size(187, 22);
            this.mnuFileExit.Text = "E&xit...";
            this.mnuFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEdit
            // 
            this.mnuEdit.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuEditCut,
            this.mnuEditCopy,
            this.mnuEditPaste,
            this.mnuEditSep1,
            this.mnuEditSearch});
            this.mnuEdit.Name = "mnuEdit";
            this.mnuEdit.Size = new System.Drawing.Size(39, 20);
            this.mnuEdit.Text = "&Edit";
            // 
            // mnuEditCut
            // 
            this.mnuEditCut.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCut.Image")));
            this.mnuEditCut.Name = "mnuEditCut";
            this.mnuEditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.mnuEditCut.Size = new System.Drawing.Size(149, 22);
            this.mnuEditCut.Text = "Cu&t";
            this.mnuEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditCopy
            // 
            this.mnuEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditCopy.Image")));
            this.mnuEditCopy.Name = "mnuEditCopy";
            this.mnuEditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.mnuEditCopy.Size = new System.Drawing.Size(149, 22);
            this.mnuEditCopy.Text = "&Copy";
            this.mnuEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditPaste
            // 
            this.mnuEditPaste.Image = ((System.Drawing.Image)(resources.GetObject("mnuEditPaste.Image")));
            this.mnuEditPaste.Name = "mnuEditPaste";
            this.mnuEditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.mnuEditPaste.Size = new System.Drawing.Size(149, 22);
            this.mnuEditPaste.Text = "&Paste";
            this.mnuEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuEditSep1
            // 
            this.mnuEditSep1.Name = "mnuEditSep1";
            this.mnuEditSep1.Size = new System.Drawing.Size(146, 6);
            // 
            // mnuEditSearch
            // 
            this.mnuEditSearch.Image = global::Argix.Properties.Resources.Find;
            this.mnuEditSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.mnuEditSearch.Name = "mnuEditSearch";
            this.mnuEditSearch.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F)));
            this.mnuEditSearch.Size = new System.Drawing.Size(149, 22);
            this.mnuEditSearch.Text = "&Search";
            this.mnuEditSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuView
            // 
            this.mnuView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuViewRefresh,
            this.mnuViewSep1,
            this.mnuViewFont,
            this.mnuViewSep2,
            this.mnuViewShowAlert,
            this.mnuViewHideWhenMin,
            this.mnuViewSep3,
            this.mnuViewToolbar,
            this.mnuViewStatusBar});
            this.mnuView.Name = "mnuView";
            this.mnuView.Size = new System.Drawing.Size(44, 20);
            this.mnuView.Text = "&View";
            // 
            // mnuViewRefresh
            // 
            this.mnuViewRefresh.Image = ((System.Drawing.Image)(resources.GetObject("mnuViewRefresh.Image")));
            this.mnuViewRefresh.Name = "mnuViewRefresh";
            this.mnuViewRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.mnuViewRefresh.Size = new System.Drawing.Size(192, 22);
            this.mnuViewRefresh.Text = "&Refresh";
            this.mnuViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep1
            // 
            this.mnuViewSep1.Name = "mnuViewSep1";
            this.mnuViewSep1.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuViewFont
            // 
            this.mnuViewFont.Name = "mnuViewFont";
            this.mnuViewFont.Size = new System.Drawing.Size(192, 22);
            this.mnuViewFont.Text = "&Font...";
            this.mnuViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep2
            // 
            this.mnuViewSep2.Name = "mnuViewSep2";
            this.mnuViewSep2.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuViewShowAlert
            // 
            this.mnuViewShowAlert.Name = "mnuViewShowAlert";
            this.mnuViewShowAlert.Size = new System.Drawing.Size(192, 22);
            this.mnuViewShowAlert.Text = "&Show Desktop Alert";
            this.mnuViewShowAlert.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewHideWhenMin
            // 
            this.mnuViewHideWhenMin.Name = "mnuViewHideWhenMin";
            this.mnuViewHideWhenMin.Size = new System.Drawing.Size(192, 22);
            this.mnuViewHideWhenMin.Text = "&Hide When Minimized";
            this.mnuViewHideWhenMin.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewSep3
            // 
            this.mnuViewSep3.Name = "mnuViewSep3";
            this.mnuViewSep3.Size = new System.Drawing.Size(189, 6);
            // 
            // mnuViewToolbar
            // 
            this.mnuViewToolbar.Name = "mnuViewToolbar";
            this.mnuViewToolbar.Size = new System.Drawing.Size(192, 22);
            this.mnuViewToolbar.Text = "&Toolbar";
            this.mnuViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuViewStatusBar
            // 
            this.mnuViewStatusBar.Name = "mnuViewStatusBar";
            this.mnuViewStatusBar.Size = new System.Drawing.Size(192, 22);
            this.mnuViewStatusBar.Text = "Status &Bar";
            this.mnuViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuTools
            // 
            this.mnuTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuToolsConfig});
            this.mnuTools.Name = "mnuTools";
            this.mnuTools.Size = new System.Drawing.Size(48, 20);
            this.mnuTools.Text = "&Tools";
            // 
            // mnuToolsConfig
            // 
            this.mnuToolsConfig.Name = "mnuToolsConfig";
            this.mnuToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.mnuToolsConfig.Text = "&Configuration...";
            this.mnuToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelp
            // 
            this.mnuHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnuHelpAbout,
            this.mnuHelpSep1});
            this.mnuHelp.Name = "mnuHelp";
            this.mnuHelp.Size = new System.Drawing.Size(44, 20);
            this.mnuHelp.Text = "&Help";
            // 
            // mnuHelpAbout
            // 
            this.mnuHelpAbout.Name = "mnuHelpAbout";
            this.mnuHelpAbout.Size = new System.Drawing.Size(145, 22);
            this.mnuHelpAbout.Text = "&About CRM...";
            this.mnuHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mnuHelpSep1
            // 
            this.mnuHelpSep1.Name = "mnuHelpSep1";
            this.mnuHelpSep1.Size = new System.Drawing.Size(142, 6);
            // 
            // pnlToolbox
            // 
            this.pnlToolbox.Controls.Add(this.tabToolbox);
            this.pnlToolbox.Controls.Add(this.pnlToolboxTitlebar);
            this.pnlToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlToolbox.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbox.Name = "pnlToolbox";
            this.pnlToolbox.Size = new System.Drawing.Size(385, 440);
            this.pnlToolbox.TabIndex = 127;
            // 
            // tabToolbox
            // 
            this.tabToolbox.Alignment = System.Windows.Forms.TabAlignment.Right;
            this.tabToolbox.Controls.Add(this.tabSearch);
            this.tabToolbox.Controls.Add(this.tabDeliveries);
            this.tabToolbox.Controls.Add(this.tabStrTrack);
            this.tabToolbox.Controls.Add(this.tabCtnTrack);
            this.tabToolbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabToolbox.ImageList = this.imlMain;
            this.tabToolbox.ItemSize = new System.Drawing.Size(120, 24);
            this.tabToolbox.Location = new System.Drawing.Point(0, 24);
            this.tabToolbox.Multiline = true;
            this.tabToolbox.Name = "tabToolbox";
            this.tabToolbox.SelectedIndex = 0;
            this.tabToolbox.ShowToolTips = true;
            this.tabToolbox.Size = new System.Drawing.Size(385, 416);
            this.tabToolbox.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabToolbox.TabIndex = 119;
            // 
            // tabSearch
            // 
            this.tabSearch.Controls.Add(this.btnReset);
            this.tabSearch.Controls.Add(this.btnSearch);
            this.tabSearch.Controls.Add(this.txtCoordinator);
            this.tabSearch.Controls.Add(this._lblCoordinator);
            this.tabSearch.Controls.Add(this.txtOriginator);
            this.tabSearch.Controls.Add(this._lblOriginator);
            this.tabSearch.Controls.Add(this.txtContact);
            this.tabSearch.Controls.Add(this._lblContact);
            this.tabSearch.Controls.Add(this.txtSubject);
            this.tabSearch.Controls.Add(this._lblSubject);
            this.tabSearch.Controls.Add(this.txtReceived);
            this.tabSearch.Controls.Add(this._lblReceived);
            this.tabSearch.Controls.Add(this.txtAction);
            this.tabSearch.Controls.Add(this._lblAction);
            this.tabSearch.Controls.Add(this.txtType);
            this.tabSearch.Controls.Add(this._lblType);
            this.tabSearch.Controls.Add(this.txtCompany);
            this.tabSearch.Controls.Add(this._lblCompany);
            this.tabSearch.Controls.Add(this.txtAgent);
            this.tabSearch.Controls.Add(this._lblAgent);
            this.tabSearch.Controls.Add(this.txtStore);
            this.tabSearch.Controls.Add(this._lblStore);
            this.tabSearch.Controls.Add(this.txtZone);
            this.tabSearch.Controls.Add(this._lblZone);
            this.tabSearch.ImageKey = "search.gif";
            this.tabSearch.Location = new System.Drawing.Point(4, 4);
            this.tabSearch.Name = "tabSearch";
            this.tabSearch.Size = new System.Drawing.Size(188, 408);
            this.tabSearch.TabIndex = 0;
            this.tabSearch.Text = "Issue Search";
            this.tabSearch.ToolTipText = "Search issue headers";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(87, 319);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(75, 23);
            this.btnReset.TabIndex = 23;
            this.btnReset.Text = "Reset";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.OnSearch);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(6, 319);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 22;
            this.btnSearch.Text = "Search";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.OnSearch);
            // 
            // txtCoordinator
            // 
            this.txtCoordinator.Location = new System.Drawing.Point(81, 282);
            this.txtCoordinator.Name = "txtCoordinator";
            this.txtCoordinator.Size = new System.Drawing.Size(125, 20);
            this.txtCoordinator.TabIndex = 21;
            this.txtCoordinator.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblCoordinator
            // 
            this._lblCoordinator.Location = new System.Drawing.Point(3, 282);
            this._lblCoordinator.Name = "_lblCoordinator";
            this._lblCoordinator.Size = new System.Drawing.Size(78, 21);
            this._lblCoordinator.TabIndex = 20;
            this._lblCoordinator.Text = "Coordinato";
            this._lblCoordinator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOriginator
            // 
            this.txtOriginator.Location = new System.Drawing.Point(81, 255);
            this.txtOriginator.Name = "txtOriginator";
            this.txtOriginator.Size = new System.Drawing.Size(125, 20);
            this.txtOriginator.TabIndex = 19;
            this.txtOriginator.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblOriginator
            // 
            this._lblOriginator.Location = new System.Drawing.Point(3, 255);
            this._lblOriginator.Name = "_lblOriginator";
            this._lblOriginator.Size = new System.Drawing.Size(72, 21);
            this._lblOriginator.TabIndex = 18;
            this._lblOriginator.Text = "Originator";
            this._lblOriginator.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(81, 228);
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(125, 20);
            this.txtContact.TabIndex = 17;
            this.txtContact.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblContact
            // 
            this._lblContact.Location = new System.Drawing.Point(3, 228);
            this._lblContact.Name = "_lblContact";
            this._lblContact.Size = new System.Drawing.Size(72, 21);
            this._lblContact.TabIndex = 16;
            this._lblContact.Text = "Contact";
            this._lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(81, 201);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(125, 20);
            this.txtSubject.TabIndex = 15;
            this.txtSubject.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblSubject
            // 
            this._lblSubject.Location = new System.Drawing.Point(3, 201);
            this._lblSubject.Name = "_lblSubject";
            this._lblSubject.Size = new System.Drawing.Size(72, 21);
            this._lblSubject.TabIndex = 14;
            this._lblSubject.Text = "Subject";
            this._lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtReceived
            // 
            this.txtReceived.Location = new System.Drawing.Point(81, 174);
            this.txtReceived.Name = "txtReceived";
            this.txtReceived.Size = new System.Drawing.Size(125, 20);
            this.txtReceived.TabIndex = 13;
            this.txtReceived.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblReceived
            // 
            this._lblReceived.Location = new System.Drawing.Point(3, 174);
            this._lblReceived.Name = "_lblReceived";
            this._lblReceived.Size = new System.Drawing.Size(72, 21);
            this._lblReceived.TabIndex = 12;
            this._lblReceived.Text = "Received";
            this._lblReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAction
            // 
            this.txtAction.Location = new System.Drawing.Point(81, 147);
            this.txtAction.Name = "txtAction";
            this.txtAction.Size = new System.Drawing.Size(125, 20);
            this.txtAction.TabIndex = 11;
            this.txtAction.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblAction
            // 
            this._lblAction.Location = new System.Drawing.Point(3, 147);
            this._lblAction.Name = "_lblAction";
            this._lblAction.Size = new System.Drawing.Size(72, 21);
            this._lblAction.TabIndex = 10;
            this._lblAction.Text = "Action";
            this._lblAction.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtType
            // 
            this.txtType.Location = new System.Drawing.Point(81, 120);
            this.txtType.Name = "txtType";
            this.txtType.Size = new System.Drawing.Size(125, 20);
            this.txtType.TabIndex = 9;
            this.txtType.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblType
            // 
            this._lblType.Location = new System.Drawing.Point(3, 120);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(72, 21);
            this._lblType.TabIndex = 8;
            this._lblType.Text = "Type";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCompany
            // 
            this.txtCompany.Location = new System.Drawing.Point(81, 93);
            this.txtCompany.Name = "txtCompany";
            this.txtCompany.Size = new System.Drawing.Size(125, 20);
            this.txtCompany.TabIndex = 7;
            this.txtCompany.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblCompany
            // 
            this._lblCompany.Location = new System.Drawing.Point(3, 93);
            this._lblCompany.Name = "_lblCompany";
            this._lblCompany.Size = new System.Drawing.Size(72, 21);
            this._lblCompany.TabIndex = 6;
            this._lblCompany.Text = "Company";
            this._lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAgent
            // 
            this.txtAgent.Location = new System.Drawing.Point(81, 66);
            this.txtAgent.Name = "txtAgent";
            this.txtAgent.Size = new System.Drawing.Size(125, 20);
            this.txtAgent.TabIndex = 5;
            this.txtAgent.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblAgent
            // 
            this._lblAgent.Location = new System.Drawing.Point(3, 66);
            this._lblAgent.Name = "_lblAgent";
            this._lblAgent.Size = new System.Drawing.Size(72, 21);
            this._lblAgent.TabIndex = 4;
            this._lblAgent.Text = "Agent";
            this._lblAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStore
            // 
            this.txtStore.Location = new System.Drawing.Point(81, 39);
            this.txtStore.Name = "txtStore";
            this.txtStore.Size = new System.Drawing.Size(125, 20);
            this.txtStore.TabIndex = 3;
            this.txtStore.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblStore
            // 
            this._lblStore.Location = new System.Drawing.Point(3, 39);
            this._lblStore.Name = "_lblStore";
            this._lblStore.Size = new System.Drawing.Size(72, 21);
            this._lblStore.TabIndex = 2;
            this._lblStore.Text = "Store";
            this._lblStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZone
            // 
            this.txtZone.Location = new System.Drawing.Point(81, 12);
            this.txtZone.Name = "txtZone";
            this.txtZone.Size = new System.Drawing.Size(125, 20);
            this.txtZone.TabIndex = 1;
            this.txtZone.TextChanged += new System.EventHandler(this.OnSearchChanged);
            // 
            // _lblZone
            // 
            this._lblZone.Location = new System.Drawing.Point(3, 12);
            this._lblZone.Name = "_lblZone";
            this._lblZone.Size = new System.Drawing.Size(72, 21);
            this._lblZone.TabIndex = 0;
            this._lblZone.Text = "Zone";
            this._lblZone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabDeliveries
            // 
            this.tabDeliveries.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabDeliveries.Controls.Add(this.mDeliveryTool);
            this.tabDeliveries.Location = new System.Drawing.Point(4, 4);
            this.tabDeliveries.Name = "tabDeliveries";
            this.tabDeliveries.Size = new System.Drawing.Size(188, 408);
            this.tabDeliveries.TabIndex = 1;
            this.tabDeliveries.Text = "Deliveries";
            // 
            // mDeliveryTool
            // 
            this.mDeliveryTool.Cursor = System.Windows.Forms.Cursors.Default;
            this.mDeliveryTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mDeliveryTool.Location = new System.Drawing.Point(0, 0);
            this.mDeliveryTool.Name = "mDeliveryTool";
            this.mDeliveryTool.Size = new System.Drawing.Size(188, 408);
            this.mDeliveryTool.TabIndex = 0;
            // 
            // tabStrTrack
            // 
            this.tabStrTrack.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabStrTrack.Controls.Add(this.mStoreTrackingTool);
            this.tabStrTrack.Location = new System.Drawing.Point(4, 4);
            this.tabStrTrack.Name = "tabStrTrack";
            this.tabStrTrack.Size = new System.Drawing.Size(188, 408);
            this.tabStrTrack.TabIndex = 2;
            this.tabStrTrack.Text = "Store Tracking";
            // 
            // mStoreTrackingTool
            // 
            this.mStoreTrackingTool.Cursor = System.Windows.Forms.Cursors.Default;
            this.mStoreTrackingTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mStoreTrackingTool.Location = new System.Drawing.Point(0, 0);
            this.mStoreTrackingTool.Name = "mStoreTrackingTool";
            this.mStoreTrackingTool.Size = new System.Drawing.Size(188, 408);
            this.mStoreTrackingTool.TabIndex = 0;
            // 
            // tabCtnTrack
            // 
            this.tabCtnTrack.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabCtnTrack.Controls.Add(this.mCartonTrackingTool);
            this.tabCtnTrack.Location = new System.Drawing.Point(4, 4);
            this.tabCtnTrack.Name = "tabCtnTrack";
            this.tabCtnTrack.Size = new System.Drawing.Size(329, 408);
            this.tabCtnTrack.TabIndex = 3;
            this.tabCtnTrack.Text = "Carton Tracking";
            // 
            // mCartonTrackingTool
            // 
            this.mCartonTrackingTool.Cursor = System.Windows.Forms.Cursors.Default;
            this.mCartonTrackingTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mCartonTrackingTool.Location = new System.Drawing.Point(0, 0);
            this.mCartonTrackingTool.Name = "mCartonTrackingTool";
            this.mCartonTrackingTool.Size = new System.Drawing.Size(329, 408);
            this.mCartonTrackingTool.TabIndex = 0;
            // 
            // imlMain
            // 
            this.imlMain.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imlMain.ImageStream")));
            this.imlMain.TransparentColor = System.Drawing.Color.Transparent;
            this.imlMain.Images.SetKeyName(0, "findreplace.gif");
            this.imlMain.Images.SetKeyName(1, "search.gif");
            // 
            // pnlToolboxTitlebar
            // 
            this.pnlToolboxTitlebar.Controls.Add(this.lblPin);
            this.pnlToolboxTitlebar.Controls.Add(this.lblToolbox);
            this.pnlToolboxTitlebar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolboxTitlebar.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlToolboxTitlebar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolboxTitlebar.Name = "pnlToolboxTitlebar";
            this.pnlToolboxTitlebar.Padding = new System.Windows.Forms.Padding(3);
            this.pnlToolboxTitlebar.Size = new System.Drawing.Size(385, 24);
            this.pnlToolboxTitlebar.TabIndex = 118;
            // 
            // lblPin
            // 
            this.lblPin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPin.BackColor = System.Drawing.SystemColors.Control;
            this.lblPin.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPin.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPin.Location = new System.Drawing.Point(335, 4);
            this.lblPin.Name = "lblPin";
            this.lblPin.Size = new System.Drawing.Size(16, 16);
            this.lblPin.TabIndex = 120;
            this.lblPin.Text = "X";
            this.lblPin.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblToolbox
            // 
            this.lblToolbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToolbox.BackColor = System.Drawing.SystemColors.Control;
            this.lblToolbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblToolbox.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblToolbox.Location = new System.Drawing.Point(3, 3);
            this.lblToolbox.Name = "lblToolbox";
            this.lblToolbox.Size = new System.Drawing.Size(353, 18);
            this.lblToolbox.TabIndex = 119;
            this.lblToolbox.Text = "Toolbox";
            this.lblToolbox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxNew,
            this.ctxSep1,
            this.csRefresh,
            this.toolStripMenuItem2,
            this.csReminder,
            this.csClearFlag});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(157, 104);
            // 
            // ctxNew
            // 
            this.ctxNew.Image = ((System.Drawing.Image)(resources.GetObject("ctxNew.Image")));
            this.ctxNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxNew.Name = "ctxNew";
            this.ctxNew.Size = new System.Drawing.Size(156, 22);
            this.ctxNew.Text = "New";
            this.ctxNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(153, 6);
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(156, 22);
            this.csRefresh.Text = "Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(153, 6);
            // 
            // csReminder
            // 
            this.csReminder.Image = global::Argix.Properties.Resources.Flag_red;
            this.csReminder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csReminder.Name = "csReminder";
            this.csReminder.Size = new System.Drawing.Size(156, 22);
            this.csReminder.Text = "Add &Reminder..";
            this.csReminder.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csClearFlag
            // 
            this.csClearFlag.Name = "csClearFlag";
            this.csClearFlag.Size = new System.Drawing.Size(156, 22);
            this.csClearFlag.Text = "&Clear Flag";
            this.csClearFlag.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cboView,
            this.btnNew,
            this.btnOpen,
            this.btnSep1,
            this.btnSave,
            this.btnPrint,
            this.btnSep2,
            this.btnRefresh,
            this.btnSep3,
            this.cboSearch,
            this.tslFilters});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(944, 56);
            this.tsMain.TabIndex = 132;
            // 
            // cboView
            // 
            this.cboView.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboView.Items.AddRange(new object[] {
            "Current Issues",
            "Search Results"});
            this.cboView.Name = "cboView";
            this.cboView.Size = new System.Drawing.Size(121, 56);
            this.cboView.SelectedIndexChanged += new System.EventHandler(this.OnViewChanged);
            // 
            // btnNew
            // 
            this.btnNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(36, 53);
            this.btnNew.Text = "New";
            this.btnNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnNew.ToolTipText = "New issue...";
            this.btnNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnOpen
            // 
            this.btnOpen.Image = global::Argix.Properties.Resources.Open;
            this.btnOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(40, 53);
            this.btnOpen.Text = "Open";
            this.btnOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnOpen.ToolTipText = "Open selected issue...";
            this.btnOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep1
            // 
            this.btnSep1.Name = "btnSep1";
            this.btnSep1.Size = new System.Drawing.Size(6, 56);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::Argix.Properties.Resources.Save;
            this.btnSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(36, 53);
            this.btnSave.Text = "Save";
            this.btnSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnSave.ToolTipText = "Save issues to file...";
            this.btnSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnPrint
            // 
            this.btnPrint.Image = ((System.Drawing.Image)(resources.GetObject("btnPrint.Image")));
            this.btnPrint.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(36, 53);
            this.btnPrint.Text = "Print";
            this.btnPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnPrint.ToolTipText = "Print all issues...";
            this.btnPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep2
            // 
            this.btnSep2.Name = "btnSep2";
            this.btnSep2.Size = new System.Drawing.Size(6, 56);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(50, 53);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnRefresh.ToolTipText = "Refresh issues";
            this.btnRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // btnSep3
            // 
            this.btnSep3.Name = "btnSep3";
            this.btnSep3.Size = new System.Drawing.Size(6, 56);
            // 
            // cboSearch
            // 
            this.cboSearch.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboSearch.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboSearch.Name = "cboSearch";
            this.cboSearch.Size = new System.Drawing.Size(121, 56);
            this.cboSearch.Text = "Search";
            this.cboSearch.ToolTipText = "Search...";
            this.cboSearch.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnSearchKeyPress);
            // 
            // tslFilters
            // 
            this.tslFilters.ForeColor = System.Drawing.Color.Red;
            this.tslFilters.Name = "tslFilters";
            this.tslFilters.Size = new System.Drawing.Size(58, 53);
            this.tslFilters.Text = "Filters Off";
            this.tslFilters.Click += new System.EventHandler(this.OnItemClick);
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.Location = new System.Drawing.Point(3, 3);
            this.scMain.Name = "scMain";
            this.scMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.grdIssues);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.scActions);
            this.scMain.Panel2.Controls.Add(this.lblTitle);
            this.scMain.Size = new System.Drawing.Size(541, 408);
            this.scMain.SplitterDistance = 226;
            this.scMain.SplitterWidth = 6;
            this.scMain.TabIndex = 133;
            // 
            // grdIssues
            // 
            this.grdIssues.ContextMenuStrip = this.csMain;
            this.grdIssues.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdIssues.DataMember = "IssueTable";
            this.grdIssues.DataSource = this.mIssues;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 19;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Header.VisiblePosition = 5;
            ultraGridColumn3.Width = 96;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn4.Header.VisiblePosition = 8;
            ultraGridColumn4.Width = 144;
            ultraGridColumn5.Header.VisiblePosition = 9;
            ultraGridColumn6.Header.VisiblePosition = 10;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn6.Width = 144;
            ultraGridColumn7.Header.Caption = "Company";
            ultraGridColumn7.Header.VisiblePosition = 4;
            ultraGridColumn7.Width = 144;
            ultraGridColumn8.Header.Caption = "Region#";
            ultraGridColumn8.Header.VisiblePosition = 11;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn8.Width = 72;
            ultraGridColumn9.Header.Caption = "District#";
            ultraGridColumn9.Header.VisiblePosition = 12;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 72;
            ultraGridColumn10.Header.Caption = "Agent#";
            ultraGridColumn10.Header.VisiblePosition = 3;
            ultraGridColumn10.Width = 60;
            ultraGridColumn11.Header.Caption = "Store#";
            ultraGridColumn11.Header.VisiblePosition = 2;
            ultraGridColumn11.Width = 60;
            ultraGridColumn12.Header.VisiblePosition = 13;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.Caption = "Initial Action";
            ultraGridColumn13.Header.VisiblePosition = 14;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.Width = 96;
            ultraGridColumn14.Header.VisiblePosition = 15;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.Caption = "Originator";
            ultraGridColumn15.Header.VisiblePosition = 20;
            ultraGridColumn15.Width = 96;
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.VisiblePosition = 17;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn17.Width = 96;
            ultraGridColumn18.Header.Caption = "Action";
            ultraGridColumn18.Header.VisiblePosition = 6;
            ultraGridColumn18.Width = 96;
            ultraGridColumn19.Format = "MM/dd/yyyy hh:mm tt";
            ultraGridColumn19.Header.Caption = "Received";
            ultraGridColumn19.Header.VisiblePosition = 7;
            ultraGridColumn19.Width = 144;
            ultraGridColumn20.Header.Caption = "Last User";
            ultraGridColumn20.Header.VisiblePosition = 18;
            ultraGridColumn20.Width = 96;
            ultraGridColumn21.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn21.Header.VisiblePosition = 1;
            ultraGridColumn21.Width = 50;
            ultraGridColumn22.Header.VisiblePosition = 21;
            ultraGridColumn22.Width = 96;
            ultraGridColumn45.Header.Caption = "Client Rep";
            ultraGridColumn45.Header.VisiblePosition = 22;
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
            ultraGridColumn45});
            this.grdIssues.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.CaptionAppearance = appearance2;
            this.grdIssues.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdIssues.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdIssues.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdIssues.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdIssues.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdIssues.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdIssues.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdIssues.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdIssues.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdIssues.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdIssues.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdIssues.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdIssues.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdIssues.Location = new System.Drawing.Point(0, 0);
            this.grdIssues.Name = "grdIssues";
            this.grdIssues.Size = new System.Drawing.Size(541, 226);
            this.grdIssues.TabIndex = 131;
            this.grdIssues.UseAppStyling = false;
            this.grdIssues.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdIssues.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdIssues.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnInitializeRow);
            this.grdIssues.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdIssues.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnRowFilterChanged);
            this.grdIssues.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdIssues.DoubleClick += new System.EventHandler(this.OnGridDoubleClick);
            this.grdIssues.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mIssues
            // 
            this.mIssues.DataSetName = "CRMDataset";
            this.mIssues.Locale = new System.Globalization.CultureInfo("");
            this.mIssues.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // scActions
            // 
            this.scActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scActions.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scActions.Location = new System.Drawing.Point(0, 20);
            this.scActions.Name = "scActions";
            // 
            // scActions.Panel1
            // 
            this.scActions.Panel1.Controls.Add(this.lsvActions);
            this.scActions.Panel1.Controls.Add(this.tsActions);
            // 
            // scActions.Panel2
            // 
            this.scActions.Panel2.Controls.Add(this.pnlAction);
            this.scActions.Size = new System.Drawing.Size(541, 156);
            this.scActions.SplitterDistance = 288;
            this.scActions.TabIndex = 36;
            this.scActions.Enter += new System.EventHandler(this.OnEnterActions);
            this.scActions.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // lsvActions
            // 
            this.lsvActions.AllowDrop = true;
            this.lsvActions.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colDateTime});
            this.lsvActions.ContextMenuStrip = this.csActions;
            this.lsvActions.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lsvActions.FullRowSelect = true;
            this.lsvActions.GridLines = true;
            this.lsvActions.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lsvActions.HideSelection = false;
            this.lsvActions.LargeImageList = this.imgActions;
            this.lsvActions.Location = new System.Drawing.Point(0, 56);
            this.lsvActions.MultiSelect = false;
            this.lsvActions.Name = "lsvActions";
            this.lsvActions.ShowItemToolTips = true;
            this.lsvActions.Size = new System.Drawing.Size(288, 100);
            this.lsvActions.SmallImageList = this.imgActions;
            this.lsvActions.TabIndex = 29;
            this.lsvActions.UseCompatibleStateImageBehavior = false;
            this.lsvActions.View = System.Windows.Forms.View.Details;
            this.lsvActions.SelectedIndexChanged += new System.EventHandler(this.OnActionSelected);
            this.lsvActions.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.lsvActions.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.lsvActions.Enter += new System.EventHandler(this.OnEnterActions);
            this.lsvActions.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // colName
            // 
            this.colName.Text = "Arranged By: Date";
            this.colName.Width = 126;
            // 
            // colDateTime
            // 
            this.colDateTime.Text = "Newest on top";
            this.colDateTime.Width = 108;
            // 
            // csActions
            // 
            this.csActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csActionsNew,
            this.toolStripMenuItem1,
            this.csActionsRefresh,
            this.toolStripSeparator2,
            this.csActionsPrint});
            this.csActions.Name = "ctxActions";
            this.csActions.Size = new System.Drawing.Size(137, 82);
            // 
            // csActionsNew
            // 
            this.csActionsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.csActionsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csActionsNew.Name = "csActionsNew";
            this.csActionsNew.Size = new System.Drawing.Size(136, 22);
            this.csActionsNew.Text = "New Action";
            this.csActionsNew.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(133, 6);
            // 
            // csActionsRefresh
            // 
            this.csActionsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csActionsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csActionsRefresh.Name = "csActionsRefresh";
            this.csActionsRefresh.Size = new System.Drawing.Size(136, 22);
            this.csActionsRefresh.Text = "Refresh";
            this.csActionsRefresh.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(133, 6);
            // 
            // csActionsPrint
            // 
            this.csActionsPrint.Image = global::Argix.Properties.Resources.Print;
            this.csActionsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csActionsPrint.Name = "csActionsPrint";
            this.csActionsPrint.Size = new System.Drawing.Size(136, 22);
            this.csActionsPrint.Text = "Print...";
            this.csActionsPrint.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // imgActions
            // 
            this.imgActions.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imgActions.ImageStream")));
            this.imgActions.TransparentColor = System.Drawing.Color.Magenta;
            this.imgActions.Images.SetKeyName(0, "Attachment.bmp");
            this.imgActions.Images.SetKeyName(1, "flg_whi.gif");
            this.imgActions.Images.SetKeyName(2, "flg_gre.gif");
            this.imgActions.Images.SetKeyName(3, "flg_yel.gif");
            this.imgActions.Images.SetKeyName(4, "flg_red.gif");
            this.imgActions.Images.SetKeyName(5, "flg_ora.gif");
            this.imgActions.Images.SetKeyName(6, "flg_pur.gif");
            this.imgActions.Images.SetKeyName(7, "flg_blu.gif");
            // 
            // tsActions
            // 
            this.tsActions.AutoSize = false;
            this.tsActions.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsActions.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsActions.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnActionsNew,
            this.toolStripSeparator1,
            this.btnActionsRefresh,
            this.ctxSep2,
            this.btnActionsPrint,
            this.ctxSep3,
            this.btnActionsOpenAttach,
            this.btnActionsAttach});
            this.tsActions.Location = new System.Drawing.Point(0, 0);
            this.tsActions.Name = "tsActions";
            this.tsActions.Size = new System.Drawing.Size(288, 56);
            this.tsActions.TabIndex = 34;
            this.tsActions.Enter += new System.EventHandler(this.OnEnterActions);
            this.tsActions.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // btnActionsNew
            // 
            this.btnActionsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.btnActionsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActionsNew.Name = "btnActionsNew";
            this.btnActionsNew.Size = new System.Drawing.Size(36, 53);
            this.btnActionsNew.Text = "New";
            this.btnActionsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActionsNew.ToolTipText = "Create a new action...";
            this.btnActionsNew.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // btnActionsRefresh
            // 
            this.btnActionsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.btnActionsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActionsRefresh.Name = "btnActionsRefresh";
            this.btnActionsRefresh.Size = new System.Drawing.Size(50, 53);
            this.btnActionsRefresh.Text = "Refresh";
            this.btnActionsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActionsRefresh.ToolTipText = "Refresh actions";
            this.btnActionsRefresh.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(6, 56);
            // 
            // btnActionsPrint
            // 
            this.btnActionsPrint.Image = global::Argix.Properties.Resources.Print;
            this.btnActionsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActionsPrint.Name = "btnActionsPrint";
            this.btnActionsPrint.Size = new System.Drawing.Size(36, 53);
            this.btnActionsPrint.Text = "Print";
            this.btnActionsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActionsPrint.ToolTipText = "Print issue...";
            this.btnActionsPrint.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // ctxSep3
            // 
            this.ctxSep3.Name = "ctxSep3";
            this.ctxSep3.Size = new System.Drawing.Size(6, 56);
            // 
            // btnActionsOpenAttach
            // 
            this.btnActionsOpenAttach.Image = global::Argix.Properties.Resources.eps_open;
            this.btnActionsOpenAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActionsOpenAttach.Name = "btnActionsOpenAttach";
            this.btnActionsOpenAttach.Size = new System.Drawing.Size(40, 53);
            this.btnActionsOpenAttach.Text = "Open";
            this.btnActionsOpenAttach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActionsOpenAttach.ToolTipText = "Open the selected attachment";
            this.btnActionsOpenAttach.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // btnActionsAttach
            // 
            this.btnActionsAttach.Image = global::Argix.Properties.Resources.Attachment;
            this.btnActionsAttach.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnActionsAttach.Name = "btnActionsAttach";
            this.btnActionsAttach.Size = new System.Drawing.Size(46, 53);
            this.btnActionsAttach.Text = "Attach";
            this.btnActionsAttach.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.btnActionsAttach.ToolTipText = "Add an attachment to selected action";
            this.btnActionsAttach.Click += new System.EventHandler(this.OnActionItemClick);
            // 
            // pnlAction
            // 
            this.pnlAction.BackColor = System.Drawing.SystemColors.ControlDark;
            this.pnlAction.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlAction.Controls.Add(this.lsvAttachments);
            this.pnlAction.Controls.Add(this.rtbAction);
            this.pnlAction.Controls.Add(this.lblAction);
            this.pnlAction.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlAction.Location = new System.Drawing.Point(0, 0);
            this.pnlAction.Name = "pnlAction";
            this.pnlAction.Padding = new System.Windows.Forms.Padding(2);
            this.pnlAction.Size = new System.Drawing.Size(249, 156);
            this.pnlAction.TabIndex = 31;
            // 
            // lsvAttachments
            // 
            this.lsvAttachments.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lsvAttachments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lsvAttachments.AutoArrange = false;
            this.lsvAttachments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lsvAttachments.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            this.lsvAttachments.HideSelection = false;
            this.lsvAttachments.LabelWrap = false;
            this.lsvAttachments.Location = new System.Drawing.Point(60, 0);
            this.lsvAttachments.Margin = new System.Windows.Forms.Padding(3, 12, 3, 3);
            this.lsvAttachments.MultiSelect = false;
            this.lsvAttachments.Name = "lsvAttachments";
            this.lsvAttachments.Scrollable = false;
            this.lsvAttachments.ShowGroups = false;
            this.lsvAttachments.Size = new System.Drawing.Size(183, 32);
            this.lsvAttachments.TabIndex = 37;
            this.lsvAttachments.UseCompatibleStateImageBehavior = false;
            this.lsvAttachments.View = System.Windows.Forms.View.List;
            this.lsvAttachments.SelectedIndexChanged += new System.EventHandler(this.OnAttachmentSelected);
            this.lsvAttachments.DoubleClick += new System.EventHandler(this.OnAttachmentDoubleClick);
            this.lsvAttachments.Enter += new System.EventHandler(this.OnEnterActions);
            this.lsvAttachments.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // rtbAction
            // 
            this.rtbAction.AcceptsTab = true;
            this.rtbAction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.rtbAction.BackColor = System.Drawing.SystemColors.Window;
            this.rtbAction.Location = new System.Drawing.Point(0, 32);
            this.rtbAction.Name = "rtbAction";
            this.rtbAction.ReadOnly = true;
            this.rtbAction.Size = new System.Drawing.Size(243, 119);
            this.rtbAction.TabIndex = 38;
            this.rtbAction.Text = "";
            this.rtbAction.Enter += new System.EventHandler(this.OnEnterActions);
            this.rtbAction.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // lblAction
            // 
            this.lblAction.BackColor = System.Drawing.SystemColors.Window;
            this.lblAction.Image = global::Argix.Properties.Resources.inbox;
            this.lblAction.Location = new System.Drawing.Point(2, 0);
            this.lblAction.Margin = new System.Windows.Forms.Padding(1);
            this.lblAction.Name = "lblAction";
            this.lblAction.Size = new System.Drawing.Size(58, 32);
            this.lblAction.TabIndex = 5;
            this.lblAction.Enter += new System.EventHandler(this.OnEnterActions);
            this.lblAction.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
            this.lblTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(541, 20);
            this.lblTitle.TabIndex = 35;
            this.lblTitle.Text = "Issue";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTitle.Enter += new System.EventHandler(this.OnEnterActions);
            this.lblTitle.Leave += new System.EventHandler(this.OnLeaveActions);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabIssues);
            this.tabMain.Controls.Add(this.tabClient);
            this.tabMain.Controls.Add(this.tabAgent);
            this.tabMain.Controls.Add(this.tabManager);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(192, 18);
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(555, 440);
            this.tabMain.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabMain.TabIndex = 132;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.OnTabMainSelectedIndexChanged);
            // 
            // tabIssues
            // 
            this.tabIssues.Controls.Add(this.scMain);
            this.tabIssues.Location = new System.Drawing.Point(4, 4);
            this.tabIssues.Name = "tabIssues";
            this.tabIssues.Padding = new System.Windows.Forms.Padding(3);
            this.tabIssues.Size = new System.Drawing.Size(547, 414);
            this.tabIssues.TabIndex = 0;
            this.tabIssues.Text = "Issues";
            this.tabIssues.UseVisualStyleBackColor = true;
            // 
            // tabClient
            // 
            this.tabClient.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabClient.Controls.Add(this.clientDashboardControl1);
            this.tabClient.Location = new System.Drawing.Point(4, 4);
            this.tabClient.Name = "tabClient";
            this.tabClient.Padding = new System.Windows.Forms.Padding(3);
            this.tabClient.Size = new System.Drawing.Size(688, 414);
            this.tabClient.TabIndex = 1;
            this.tabClient.Text = "Client Dashboard";
            // 
            // clientDashboardControl1
            // 
            this.clientDashboardControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.clientDashboardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.clientDashboardControl1.Location = new System.Drawing.Point(3, 3);
            this.clientDashboardControl1.Name = "clientDashboardControl1";
            this.clientDashboardControl1.Size = new System.Drawing.Size(682, 408);
            this.clientDashboardControl1.TabIndex = 0;
            // 
            // tabAgent
            // 
            this.tabAgent.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.tabAgent.Controls.Add(this.agentDashboardControl1);
            this.tabAgent.Location = new System.Drawing.Point(4, 4);
            this.tabAgent.Name = "tabAgent";
            this.tabAgent.Size = new System.Drawing.Size(688, 414);
            this.tabAgent.TabIndex = 2;
            this.tabAgent.Text = "Agent Dashboard";
            // 
            // agentDashboardControl1
            // 
            this.agentDashboardControl1.Cursor = System.Windows.Forms.Cursors.Default;
            this.agentDashboardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.agentDashboardControl1.Location = new System.Drawing.Point(0, 0);
            this.agentDashboardControl1.Name = "agentDashboardControl1";
            this.agentDashboardControl1.Size = new System.Drawing.Size(688, 414);
            this.agentDashboardControl1.TabIndex = 0;
            // 
            // tabManager
            // 
            this.tabManager.BackColor = System.Drawing.SystemColors.Control;
            this.tabManager.Controls.Add(this.managerDashboardControl21);
            this.tabManager.Location = new System.Drawing.Point(4, 4);
            this.tabManager.Name = "tabManager";
            this.tabManager.Size = new System.Drawing.Size(688, 414);
            this.tabManager.TabIndex = 3;
            this.tabManager.Text = "Manager Dashboard";
            // 
            // managerDashboardControl21
            // 
            this.managerDashboardControl21.Cursor = System.Windows.Forms.Cursors.Default;
            this.managerDashboardControl21.Dock = System.Windows.Forms.DockStyle.Fill;
            this.managerDashboardControl21.Location = new System.Drawing.Point(0, 0);
            this.managerDashboardControl21.Name = "managerDashboardControl21";
            this.managerDashboardControl21.Size = new System.Drawing.Size(688, 414);
            this.managerDashboardControl21.TabIndex = 0;
            // 
            // scParent
            // 
            this.scParent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scParent.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.scParent.Location = new System.Drawing.Point(0, 80);
            this.scParent.Name = "scParent";
            // 
            // scParent.Panel1
            // 
            this.scParent.Panel1.Controls.Add(this.tabMain);
            // 
            // scParent.Panel2
            // 
            this.scParent.Panel2.Controls.Add(this.pnlToolbox);
            this.scParent.Size = new System.Drawing.Size(944, 440);
            this.scParent.SplitterDistance = 555;
            this.scParent.TabIndex = 133;
            this.scParent.SplitterMoving += new System.Windows.Forms.SplitterCancelEventHandler(this.OnSplitterMoving);
            // 
            // mFindIssues
            // 
            this.mFindIssues.DataSetName = "CRMDataset";
            this.mFindIssues.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(944, 544);
            this.Controls.Add(this.scParent);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.stbMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics CRM";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.pnlToolbox.ResumeLayout(false);
            this.tabToolbox.ResumeLayout(false);
            this.tabSearch.ResumeLayout(false);
            this.tabSearch.PerformLayout();
            this.tabDeliveries.ResumeLayout(false);
            this.tabStrTrack.ResumeLayout(false);
            this.tabCtnTrack.ResumeLayout(false);
            this.pnlToolboxTitlebar.ResumeLayout(false);
            this.csMain.ResumeLayout(false);
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdIssues)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mIssues)).EndInit();
            this.scActions.Panel1.ResumeLayout(false);
            this.scActions.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scActions)).EndInit();
            this.scActions.ResumeLayout(false);
            this.csActions.ResumeLayout(false);
            this.tsActions.ResumeLayout(false);
            this.tsActions.PerformLayout();
            this.pnlAction.ResumeLayout(false);
            this.tabMain.ResumeLayout(false);
            this.tabIssues.ResumeLayout(false);
            this.tabClient.ResumeLayout(false);
            this.tabAgent.ResumeLayout(false);
            this.tabManager.ResumeLayout(false);
            this.scParent.Panel1.ResumeLayout(false);
            this.scParent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scParent)).EndInit();
            this.scParent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mFindIssues)).EndInit();
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
                    this.mnuViewToolbar.Checked = this.tsMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
                    this.mnuViewStatusBar.Checked = this.stbMain.Visible = Convert.ToBoolean(global::Argix.Properties.Settings.Default.StatusBar);
                    this.Font = this.msMain.Font = this.tsMain.Font = this.stbMain.Font = global::Argix.Properties.Settings.Default.Font;
                    this.mLastNewIssueTime = global::Argix.Properties.Settings.Default.LastRefresh;
                    this.ColumnHeaders = global::Argix.Properties.Settings.Default.ColumnHeaders;
                    this.ColumnFilters = global::Argix.Properties.Settings.Default.ColumnFilters;
                    this.mReminders.Reminders = global::Argix.Properties.Settings.Default.Reminders;
                    this.mnuViewShowAlert.Checked = this.mTrayIcon.ShowAlert = true;
                    this.mnuViewHideWhenMin.Checked = this.mTrayIcon.HideWhenMinimized = false;
                    App.CheckVersion();
                }
                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
				#endregion
				#region Set tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
                this.mToolTip.ShowAlways = true;
				this.mToolTip.SetToolTip(this.lblPin, "Auto Hide");
                #endregion
				
				//Set control defaults
                ServiceInfo t = CRMGateway.GetServiceInfo();
                this.stbMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.stbMain.User1Panel.Width = 144;
                this.grdIssues.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                this.cboView.SelectedIndex = 0;
                if(App.Config.AutoRefreshOn) CRMGateway.StartAutoRefresh(this); else this.mnuViewRefresh.PerformClick();
                this.mReminders.Start();

                //Toolbox
                OnLeaveToolbox(null,EventArgs.Empty);
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Ask only if there are detail forms open
 		    try {
                if(!e.Cancel) {
                    //Kill services
                    this.mReminders.Stop();
                    this.mTrayIcon.Visible = false;

                    //Save settings
                    global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                    global::Argix.Properties.Settings.Default.Location = this.Location;
                    global::Argix.Properties.Settings.Default.Size = this.Size;
                    global::Argix.Properties.Settings.Default.Toolbar = this.mnuViewToolbar.Checked;
                    global::Argix.Properties.Settings.Default.StatusBar = this.mnuViewStatusBar.Checked;
                    global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                    global::Argix.Properties.Settings.Default.Font = this.Font;
                    global::Argix.Properties.Settings.Default.LastRefresh = DateTime.Now;
                    global::Argix.Properties.Settings.Default.ColumnHeaders = this.ColumnHeaders;
                    global::Argix.Properties.Settings.Default.ColumnFilters = this.ColumnFilters;
                    global::Argix.Properties.Settings.Default.Reminders = this.mReminders.Reminders;
                    global::Argix.Properties.Settings.Default.ToolboxWidth = this.pnlToolbox.Width;
                    global::Argix.Properties.Settings.Default.ToolboxAutoHide = this.lblPin.Text != AUTOHIDE_OFF;
                    global::Argix.Properties.Settings.Default.Save();
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Error); }
        }
        private void OnFormResize(object sender,System.EventArgs e) { 
			//Event handler for form resized event
            if(this.WindowState == FormWindowState.Minimized) this.Visible = !this.mTrayIcon.HideWhenMinimized;
        }
        private void OnTabMainSelectedIndexChanged(object sender,EventArgs e) {
            //Event handler for change in view tab selected index
            this.Cursor = Cursors.WaitCursor;
            try {
                switch(this.tabMain.SelectedTab.Name) {
                    case "tabIssues":
                        break;
                    case "tabClient":
                        break;
                    case "tabAgent":
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnActionSelected(object sender,EventArgs e) {
            //Event handler for change in selected action
            try {
                this.rtbAction.Text = "";
                this.lsvAttachments.Items.Clear();
                if(this.lsvActions.SelectedItems.Count > 0) {
                    //Show the selected action
                    long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
                    int start=0;
                    bool go=false;
                    for(int i=0;i<this.mIssue.Actions.Count;i++) {
                        Action action = this.mIssue.Actions[i];
                        if(!go && action.ID == actionID) go = true;
                        if(action.ID == actionID) {
                            Attachments attachments = action.Attachments;
                            this.lsvAttachments.Items.Clear();
                            if(attachments != null) {
                                for(int j=0;j<attachments.Count;j++)
                                    this.lsvAttachments.Items.Add(attachments[j].ID.ToString(),attachments[j].Filename,-1);
                            }
                            this.lsvAttachments.View = View.List;
                        }
                        if(go) {
                            string header = action.Created.ToString("f") + "     " + action.UserID + ", " + action.TypeName;
                            this.rtbAction.AppendText(header);
                            this.rtbAction.Select(start,header.Length);
                            this.rtbAction.SelectionFont = new Font(this.rtbAction.Font,FontStyle.Bold);
                            this.rtbAction.AppendText("\r\n\r\n");
                            this.rtbAction.AppendText(action.Comment);
                            this.rtbAction.AppendText("\r\n");
                            this.rtbAction.AppendText("".PadRight(75,'-'));
                            this.rtbAction.AppendText("\r\n");
                            start = this.rtbAction.Text.Length;
                        }
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); };
        }
        private void OnAttachmentSelected(object sender,EventArgs e) { setUserServices(); }
        private void OnAttachmentDoubleClick(object sender,EventArgs e) {
            //Event handler for attachment double-clicked
            try {
                if(this.btnActionsOpenAttach.Enabled) this.btnActionsOpenAttach.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #region Grid Support: OnGridInitializeLayout(), OnGridBeforeRowFilterDropDownPopulate(), OnRowFilterChanged(), OnInitializeRow(), OnGridSelectionChanged(), GridMouseDown(), OnGridDoubleClick()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //Event handler for grid layout initialization
            try {
                e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"OpenTime");
                e.Layout.Bands[0].Columns["OpenTime"].DataType = typeof(int);
                e.Layout.Bands[0].Columns["OpenTime"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                e.Layout.Bands[0].Columns["OpenTime"].Header.Caption = "Open";
                e.Layout.Bands[0].Columns["OpenTime"].Format = "#0";
                e.Layout.Bands[0].Columns["OpenTime"].Header.Appearance.TextHAlign = HAlign.Center;
                e.Layout.Bands[0].Columns["OpenTime"].CellAppearance.TextHAlign = HAlign.Right;
                e.Layout.Bands[0].Columns["OpenTime"].AllowRowFiltering = DefaultableBoolean.False;
                e.Layout.Bands[0].Columns["OpenTime"].Width = 50;
                e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"QueueHrs");
                e.Layout.Bands[0].Columns["QueueHrs"].DataType = typeof(int);
                e.Layout.Bands[0].Columns["QueueHrs"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                e.Layout.Bands[0].Columns["QueueHrs"].Header.Caption = "Queue";
                e.Layout.Bands[0].Columns["QueueHrs"].Format = "#0";
                e.Layout.Bands[0].Columns["QueueHrs"].Header.Appearance.TextHAlign = HAlign.Center;
                e.Layout.Bands[0].Columns["QueueHrs"].CellAppearance.TextHAlign = HAlign.Right;
                e.Layout.Bands[0].Columns["QueueHrs"].AllowRowFiltering = DefaultableBoolean.False;
                e.Layout.Bands[0].Columns["QueueHrs"].Width = 50;
                e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"Reminder");
                e.Layout.Bands[0].Columns["Reminder"].DataType = typeof(Image);
                e.Layout.Bands[0].Columns["Reminder"].Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Default;
                e.Layout.Bands[0].Columns["Reminder"].Header.Caption = "";
                e.Layout.Bands[0].Columns["Reminder"].CellAppearance.ImageHAlign = HAlign.Center;
                e.Layout.Bands[0].Columns["Reminder"].AllowRowFiltering = DefaultableBoolean.False;
                e.Layout.Bands[0].Columns["Reminder"].Width = 20;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                //e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnRowFilterChanged(object sender,AfterRowFilterChangedEventArgs e) {
            try {
                this.tslFilters.Text = "Filters Off";
                this.tslFilters.Enabled = false;
                for(int i=0;i<this.grdIssues.DisplayLayout.Bands[0].ColumnFilters.Count;i++) {
                    if(this.grdIssues.DisplayLayout.Bands[0].ColumnFilters[i].ToString().Trim().Length > 0) {
                        this.tslFilters.Text = "Filters On";
                        this.tslFilters.Enabled = true;
                        break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnInitializeRow(object sender,InitializeRowEventArgs e) {
            //Event handler for row intialize event
            try {
                //Bold rows of new issues/actions
                int id = Convert.ToInt32(e.Row.Cells["ID"].Value);
                DateTime dt1 = Convert.ToDateTime(e.Row.Cells["LastActionCreated"].Value);
                if(!this.mReadIssues.ContainsKey(id)) {
                    //Not viewed or startup (i.e. collection is empty)
                    if(dt1.CompareTo(this.mLastNewIssueTime) > 0) e.Row.CellAppearance.FontData.Bold = DefaultableBoolean.True;
                }
                else {
                    //LastActionCreated different then last time viewed?
                    DateTime dt2 = Convert.ToDateTime(this.mReadIssues[id]);
                    if(dt1.CompareTo(dt2) > 0) e.Row.CellAppearance.FontData.Bold = DefaultableBoolean.True;
                }

                //Calculate derived columns
                DateTime first = DateTime.Parse(e.Row.Cells["FirstActionCreated"].Value.ToString());
                DateTime last = DateTime.Parse(e.Row.Cells["LastActionCreated"].Value.ToString());
                bool closed = (e.Row.Cells["LastActionDescription"].Value.ToString().ToLower() == "closed");
                e.Row.Cells["OpenTime"].Value = closed ? 0 : DateTime.Now.Subtract(first).TotalHours;
                e.Row.Cells["QueueHrs"].Value = closed ? 0 : DateTime.Now.Subtract(last).TotalHours;

                //Add reminder flags
                if(this.mReminders.HasReminder(id,Environment.UserName)) e.Row.Cells["Reminder"].Value = Properties.Resources.Flag_red;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            this.Cursor = Cursors.WaitCursor;
            try {
                //Update the selected issue if not looking at another issue view
                Issue issue = this.mIssue;
                this.mIssue = null;
                UltraGrid grid = (UltraGrid)sender;
                if(grid.Selected.Rows.Count > 0) {
                    switch(this.cboView.Text) {
                        case "Search Results":
                            this.mIssue = CRMGateway.GetIssue(Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value));
                            break;
                        default:
                            this.mIssue = CRMGateway.GetIssue(Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value));
                            break;
                    }
                    try {
                        //Unbold viewed issues/actions
                        grid.Selected.Rows[0].CellAppearance.FontData.Bold = DefaultableBoolean.False;
                        int id = Convert.ToInt32(grid.Selected.Rows[0].Cells["ID"].Value);
                        DateTime dt1 = Convert.ToDateTime(grid.Selected.Rows[0].Cells["LastActionCreated"].Value);
                        if(this.mReadIssues.ContainsKey(id)) this.mReadIssues[id] = dt1; else this.mReadIssues.Add(id,dt1);
                    }
                    catch { }
                }
                if(this.mIssue == null || issue == null || (this.mIssue != null && issue != null && this.mIssue.ID != issue.ID) || e == null) {
                    this.lblTitle.Text = this.Title;
                    listActions();
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
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
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) grid.Selected.Rows.Clear();
                            oRow.Selected = true;
                            oRow.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement))
                            grid.Selected.Rows.Clear();
                        else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridDoubleClick(object sender,EventArgs e) {
            //Event handler for grid double-clicked
            try {
                if (this.mnuFileOpen.Enabled && this.grdIssues.Selected.Rows.Count > 0 && this.grdIssues.Selected.Rows[0].Activated) this.mnuFileOpen.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #region Grid Drag/Drop Events: OnDragOver(), OnDragDrop()
        private void OnDragOver(object sender,System.Windows.Forms.DragEventArgs e) {
            //Event handler for dragging over the grid
            try {
                //Enable appropriate drag drop effect
                DataObject data = (DataObject)e.Data;
                if(data.GetDataPresent(DataFormats.FileDrop) || data.GetDataPresent("FileGroupDescriptor")) {
                    e.Effect = this.btnActionsAttach.Enabled ? DragDropEffects.Copy : DragDropEffects.None;
                }
                else
                    e.Effect = DragDropEffects.None;
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnDragDrop(object sender,System.Windows.Forms.DragEventArgs e) {
            //Event handler for dropping onto the listview
            try {
                DataObject data = (DataObject)e.Data;
                if(data.GetDataPresent(DataFormats.FileDrop)) {
                    //Local file
                    string[] names = (string[])data.GetData(DataFormats.FileDrop);
                    if(names.Length > 0) {
                        //Save attachment
                        string name = new System.IO.FileInfo(names[0]).Name;
                        System.IO.FileStream stream = new System.IO.FileStream(names[0],System.IO.FileMode.Open);
                        byte[] bytes = new byte[stream.Length];
                        //stream.Position = 0;
                        stream.Read(bytes,0,(int)stream.Length);
                        long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
                        CRMGateway.AddAttachment(name,bytes,actionID);
                        stream.Close();
                        OnGridSelectionChanged(this.grdIssues,null);
                    }
                }
                else if(data.GetDataPresent("FileGroupDescriptor")) {
                    //Outlook attachment
                    //Set the position within the current stream to the beginning of the file name
                    //return the file name in the fileName variable
                    System.IO.MemoryStream stream = (System.IO.MemoryStream)data.GetData("FileGroupDescriptor");
                    stream.Seek(76,System.IO.SeekOrigin.Begin);
                    byte[] fileName = new byte[256];
                    stream.Read(fileName,0,256);
                    System.Text.Encoding encoding = System.Text.Encoding.ASCII;
                    string name = encoding.GetString(fileName);
                    name = name.TrimEnd('\0');
                    stream.Close();

                    //Save attachment
                    stream = (System.IO.MemoryStream)e.Data.GetData("FileContents");
                    byte[] bytes = new byte[stream.Length];
                    //stream.Position = 0;
                    stream.Read(bytes,0,(int)stream.Length);
                    long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
                    CRMGateway.AddAttachment(name,bytes,actionID);
                    stream.Close();
                    OnGridSelectionChanged(this.grdIssues,null);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        #endregion
        #endregion
        #region Actions: listActions(), OnEnterActions(), OnLeaveActions()
        private void listActions() {
            //Event handler for change in actions collection
            //Load actions for this issue
            int index = this.lsvActions.SelectedItems.Count > 0 ? this.lsvActions.SelectedItems[0].Index : -1;
            this.lsvActions.Groups.Clear();
            this.lsvActions.Items.Clear();
            if(this.mIssue != null) {
                //Create action listitems sorted by date/time
                Actions actions = this.mIssue.Actions;
                for(int i = 0;i < actions.Count;i++) {
                    //Add attachment symbol as required 
                    //Tag is used to enable attachement to newest action only
                    Action action = actions[i];
                    ListViewItem item = this.lsvActions.Items.Add(action.ID.ToString(),action.UserID,(action.AttachmentCount > 0 ? 0 : -1));
                    item.Tag = i.ToString();

                    //Assign to listitem group
                    bool useYesterday = DateTime.Today.DayOfWeek != DayOfWeek.Monday;
                    if(action.Created.CompareTo(DateTime.Today) >= 0) {
                        this.lsvActions.Groups.Add("Today","Today");
                        item.SubItems.Add(action.Created.ToString("ddd HH:mm"));
                        item.Group = this.lsvActions.Groups["Today"];
                    }
                    else if(useYesterday && action.Created.CompareTo(DateTime.Today.AddDays(-1)) >= 0) {
                        this.lsvActions.Groups.Add("Yesterday","Yesterday");
                        item.SubItems.Add(action.Created.ToString("ddd HH:mm"));
                        item.Group = this.lsvActions.Groups["Yesterday"];
                    }
                    else if(action.Created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek)) >= 0) {
                        this.lsvActions.Groups.Add("This Week","This Week");
                        item.SubItems.Add(action.Created.ToString("ddd HH:mm"));
                        item.Group = this.lsvActions.Groups["This Week"];
                    }
                    else if(action.Created.CompareTo(DateTime.Today.AddDays(0 - DateTime.Today.DayOfWeek - 7)) >= 0) {
                        this.lsvActions.Groups.Add("Last Week","Last Week");
                        item.SubItems.Add(action.Created.ToString("ddd MM/dd HH:mm"));
                        item.Group = this.lsvActions.Groups["Last Week"];
                    }
                    else {
                        this.lsvActions.Groups.Add("Other","Other");
                        item.SubItems.Add(action.Created.ToString("ddd MM/dd/yyyy HH:mm"));
                        item.Group = this.lsvActions.Groups["Other"];
                    }
                }
            }
            try {
                if(this.lsvActions.Items.Count > 0) {
                    if(index > -1 && index < this.lsvActions.Items.Count)
                        this.lsvActions.Items[index].Selected = true;
                    else
                        this.lsvActions.Items[0].Selected = true;
                }
                else
                    OnActionSelected(null,EventArgs.Empty);
            }
            catch { }
        }
        private void OnEnterActions(object sender,System.EventArgs e) {
            //Occurs when the control becomes the active control on the form
            try {
                this.lblTitle.BackColor = SystemColors.ActiveCaption;
                this.lblTitle.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch { }
        }
        private void OnLeaveActions(object sender,System.EventArgs e) {
            //Occurs when the control is no longer the active control on the form
            try {
                this.lblTitle.BackColor = System.Drawing.SystemColors.Control;
                this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            }
            catch { }
        }
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
                foreach(Control ctl1 in this.pnlToolbox.Controls) {
                    foreach(Control ctl2 in ctl1.Controls) {
                        foreach(Control ctl3 in ctl2.Controls) {
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
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnEnterToolbox(object sender,System.EventArgs e) {
            //Occurs when the control becomes the active control on the form
            try {
                //Disable auto-hide when active; show toolbar as active
                if(this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
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
                if(this.lblPin.Text==AUTOHIDE_ON) { this.tmrAutoHide.Enabled = true; this.tmrAutoHide.Start(); }
            }
            catch { }
        }
        private void OnMouseEnterToolbox(object sender,System.EventArgs e) {
            //Occurs when the mouse enters the visible part of the control
            try {
                //Auto-open if not pinned and toolbar is closed; disable auto-hide if on
                if(this.lblPin.Text==AUTOHIDE_ON && this.scParent.Panel2.Width==25)
                    this.scParent.SplitterDistance = this.scParent.Width - global::Argix.Properties.Settings.Default.ToolboxWidth;
                if(this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
            }
            catch { }
        }
        private void OnMouseLeaveToolbox(object sender,System.EventArgs e) {
            //Occurs when the mouse leaves the visible part of the control
            try {
                //Enable auto-hide when inactive and unpinned
                if(this.lblToolbox.BackColor==SystemColors.Control && this.lblPin.Text==AUTOHIDE_ON) { this.tmrAutoHide.Enabled = true; this.tmrAutoHide.Start(); }
            }
            catch { }
        }
        private void OnToggleAutoHide(object sender,System.EventArgs e) { this.lblPin.Text = this.lblPin.Text==AUTOHIDE_OFF ? AUTOHIDE_ON : AUTOHIDE_OFF; }
        private void OnAutoHideToolbox(object sender,System.EventArgs e) {
            //Toolbox timer event handler
            try {
                //Auto-close timer
                this.tmrAutoHide.Stop();
                this.tmrAutoHide.Enabled = false;
                global::Argix.Properties.Settings.Default.ToolboxWidth = this.scParent.Width - this.scParent.SplitterDistance;
                this.scParent.SplitterDistance = this.scParent.Width - 24;
            }
            catch { }
        }
        private void OnSplitterMoving(object sender,SplitterCancelEventArgs e) {
            try {
                this.lblToolbox.BackColor = this.lblPin.BackColor = SystemColors.ActiveCaption;
                this.lblToolbox.ForeColor = this.lblPin.ForeColor = SystemColors.ActiveCaptionText;
                if(this.tmrAutoHide.Enabled) { this.tmrAutoHide.Stop(); this.tmrAutoHide.Enabled = false; }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region User Services: OnItemClick(), OnViewChanged(), OnSearchKeyPress(), OnActionItemClick(), OnHelpItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
					case "mnuFileNew": 
                    case "btnNew": 
                    case "csNew":
                        Issue issue = new Issue();
                        issue.CompanyID = -1;
                        issue.DistrictNumber = issue.RegionNumber = issue.AgentNumber = null;
                        issue.StoreNumber = -1;
                        issue.FirstActionUserID = Environment.UserName;
                        issue.TypeID = -1;
                        issue.Subject = "";
                        issue.Contact = "";
                        issue.Actions = new Actions();
                        dlgIssue dlg = new dlgIssue(issue);
                        dlg.Font = this.Font;

                        DialogResult result = DialogResult.Retry;
                        while (result == DialogResult.Retry) {
                            if (dlg.ShowDialog() == DialogResult.OK) {
                                this.Cursor = Cursors.WaitCursor;
                                long iid = 0;
                                try {
                                    iid = CRMGateway.CreateIssue(issue);
                                    result = DialogResult.OK;
                                }
                                catch (Exception ex) { result = MessageBox.Show(ex.Message,"New Issue",MessageBoxButtons.RetryCancel); }
                                if (result == DialogResult.OK && iid > 0) {
                                    this.mnuViewRefresh.PerformClick();
                                    for (int i = 0;i < this.grdIssues.Rows.Count;i++) {
                                        int id = Convert.ToInt32(this.grdIssues.Rows[i].Cells["ID"].Value);
                                        if (id == iid) {
                                            this.grdIssues.Rows[i].Selected = true;
                                            this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                                            OnGridSelectionChanged(this.grdIssues,null);
                                            break;
                                        }
                                    }
                                }
                            }
                            else 
                                result = DialogResult.Cancel;
                        }
                        break;
                    case "mnuFileOpen":
                    case "btnOpen":
                        Issue issue2 = CRMGateway.GetIssue(Convert.ToInt32(this.grdIssues.Selected.Rows[0].Cells["ID"].Value));
                        dlgIssue dlg2 = new dlgIssue(issue2);
                        dlg2.Font = this.Font;

                        DialogResult result2 = DialogResult.Retry;
                        while (result2 == DialogResult.Retry) {
                            if (dlg2.ShowDialog() == DialogResult.OK) {
                                //Add the action to the issue
                                this.Cursor = Cursors.WaitCursor;
                                try {
                                    CRMGateway.AddAction(issue2.Actions[issue2.Actions.Count - 1]);
                                    result2 = DialogResult.OK;
                                }
                                catch (Exception ex) { result2 = MessageBox.Show(ex.Message,"New Issue",MessageBoxButtons.RetryCancel); }
                                this.mnuViewRefresh.PerformClick();
                                OnGridSelectionChanged(this.grdIssues,null);
                            }
                            else
                                result2 = DialogResult.Cancel;
                        }
                       break;
                    case "mnuFileActionNew":
                       this.csActionsNew.PerformClick();
                        break;
                    case "mnuFileSave":
                    case "btnSave":
                        break;
					case "mnuFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Issues As...";
                        dlgSave.FileName = "";
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            switch(this.cboView.Text) {
                                case "Current Issues": 
                                    if(dlgSave.FileName.EndsWith("xls")) 
                                        new Argix.ExcelFormat().Transform(this.mIssues,dlgSave.FileName);
                                    else 
                                        this.mIssues.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                                    break;
                                case "Search Results": 
                                    if(dlgSave.FileName.EndsWith("xls"))
                                        new Argix.ExcelFormat().Transform(this.mFindIssues, dlgSave.FileName);
                                    else
                                        this.mFindIssues.WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema);
                                   break;
                            }
                        }
                        break;
					case "mnuFileSetup": UltraGridPrinter.PageSettings(); break;
					case "mnuFilePrint":
                    case "btnPrint": UltraGridPrinter.Print(this.grdIssues,this.grdIssues.Text,true); break;
					case "mnuFilePreview": UltraGridPrinter.PrintPreview(this.grdIssues,this.grdIssues.Text); break;
					case "mnuFileExit": this.Close(); Application.Exit(); break;
					case "mnuEditCut": break;
					case "mnuEditCopy":	break;
					case "mnuEditPaste": break;
                    case "mnuEditSearch": OnSearchKeyPress(this.cboSearch,new KeyPressEventArgs((char)Keys.Enter)); break;
					case "mnuViewRefresh":
                    case "btnRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        this.mGridSvcIssues.CaptureState("ID");
                        switch(this.cboView.Text) {
                            case "Search Results": break;
                            default:
                                DataSet ds = CRMGateway.GetIssues();
                                lock(this.mIssues) {
                                    this.mIssues.Clear();
                                    this.mIssues.Merge(ds);
                                }
                                postIssueUpdates();
                                break;
                        }
                        this.mGridSvcIssues.RestoreState();
                        break;
                    case "mnuViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if(fd.ShowDialog() == DialogResult.OK) 
                            this.Font = this.msMain.Font = this.stbMain.Font = fd.Font;
                        break;
                    case "mnuViewToolbar":      
                        this.tsMain.Visible = (this.mnuViewToolbar.Checked = (!this.mnuViewToolbar.Checked)); 
                        break;
					case "mnuViewStatusBar":
                        this.stbMain.Visible = (this.mnuViewStatusBar.Checked = (!this.mnuViewStatusBar.Checked)); 
                        break;
                    case "mnuToolsConfig": 
                        App.ShowConfig();
                        if(App.Config.AutoRefreshOn) CRMGateway.StartAutoRefresh(this); else CRMGateway.StopAutoRefresh();
                        this.mnuViewRefresh.PerformClick();
                        break;
                    case "mnuHelpAbout":        
                        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); 
                        break;
                    case "tslFilters":
                        this.grdIssues.DisplayLayout.Bands[0].ColumnFilters.ClearAllFilters();
                        OnRowFilterChanged(null,null);
                        break;
                    case "csReminder":
                        this.mReminders.AddReminder(this.mIssue.ID,this.Title,Environment.UserName);
                        this.mnuViewRefresh.PerformClick();
                        break;
                    case "csClearFlag":
                        this.mReminders.RemoveReminder(Convert.ToInt32(this.grdIssues.Selected.Rows[0].Cells["ID"].Value),Environment.UserName);
                        this.mnuViewRefresh.PerformClick();
                        break;
                }
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnViewChanged(object sender,EventArgs e) {
            //Event handler for change in combobox view selection
            try {
                switch(this.cboView.Text) {
                    case "Search Results":
                        this.grdIssues.DataSource = this.mFindIssues;
                        this.grdIssues.DataBind();
                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                        break;
                    default:
                        Issue issue = this.mIssue;
                        this.grdIssues.DataSource = this.mIssues;
                        this.grdIssues.DataBind();
                        this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                        this.mnuViewRefresh.PerformClick();
                        if(issue != null) {
                            for(int i = 0;i < this.grdIssues.Rows.VisibleRowCount;i++) {
                                if(Convert.ToInt32(this.grdIssues.Rows[i].Cells["ID"].Value) == issue.ID) {
                                    this.grdIssues.Rows[i].Selected = true;
                                    this.grdIssues.Rows[i].Activate();
                                    break;
                                }
                            }
                        }
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); }
        }
        private void OnSearchKeyPress(object sender,KeyPressEventArgs e) {
            //Search
            try {
                this.mFindIssues.Clear();
                if(e.KeyChar == (char)Keys.Enter && this.cboSearch.Text.Trim().Length > 0) {
                    //this.cboSearch.Items.Add(this.cboSearch.Text);
                    this.mFindIssues.Merge(CRMGateway.SearchIssues(this.cboSearch.Text));
                    this.cboView.SelectedItem = "Search Results";
                    OnViewChanged(this.cboView,EventArgs.Empty);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnActionItemClick(object sender,EventArgs e) {
            //Event handler for mneu item clicked
            byte[] bytes=null;
            long actionID = Convert.ToInt64(this.lsvActions.SelectedItems[0].Name);
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "csActionsNew":
                    case "btnActionsNew":
                        Action action = new Action();
                        action.IssueID = this.mIssue.ID;
                        action.TypeID = 0;
                        action.UserID = Environment.UserName;
                        action.Created = DateTime.Now;
                        action.Comment = "";
                        CRMDataset.ActionTableRow _action = new CRMDataset().ActionTable.NewActionTableRow();
                        dlgAction dlgNA = new dlgAction(this.mIssue, action);
                        dlgNA.Font = this.Font;
                        DialogResult result = DialogResult.Retry;
                        while (result == DialogResult.Retry) {
                            if (dlgNA.ShowDialog(this) == DialogResult.OK) {
                                //Add the action to the issue
                                this.Cursor = Cursors.WaitCursor;
                                try {
                                    CRMGateway.AddAction(action);
                                    result = DialogResult.OK;
                                }
                                catch (Exception ex) { result = MessageBox.Show(ex.Message,"New Action",MessageBoxButtons.RetryCancel); }
                                this.mnuViewRefresh.PerformClick();
                                OnGridSelectionChanged(this.grdIssues,null);
                            }
                            else
                                result = DialogResult.Cancel;
                        }
                        break;
                    case "csActionsPrint":
                    case "btnActionsPrint":
                        Argix.Windows.Printers.WinPrinter2 wp = new Argix.Windows.Printers.WinPrinter2("",this.rtbAction.Font);
                        string doc = "Issue Type: \t" + this.mIssue.Type + "\r\nSubject: \t\t" + this.mIssue.Subject + "\r\nContact: \t\t" + this.mIssue.Contact + "\r\n" + "\r\nCompany: \t" + this.mIssue.CompanyName + "\r\nStore#: \t\t" + this.mIssue.StoreNumber.ToString() + "\r\nAgent#: \t" + this.mIssue.AgentNumber + "\r\nZone: \t\t" + this.mIssue.Zone;
                        doc += "\r\n\r\n\r\n";
                        Actions actions = this.mIssue.Actions;
                        for(int i = 0;i < actions.Count;i++) {
                            doc += actions[i].Created.ToString("f") + "     " + actions[i].UserID + ", " + actions[i].TypeName;
                            doc += "\r\n\r\n";
                            doc += actions[i].Comment;
                            doc += "\r\n";
                            doc += "".PadRight(75,'-');
                            doc += "\r\n";
                        }
                        wp.Print(this.mIssue.Subject,doc,true);
                        break;
                    case "csActionsRefresh":
                    case "btnActionsRefresh":
                        OnGridSelectionChanged(this.grdIssues, null);
                        break;
                    case "btnActionsOpenAttach":
                        //Open the selected attachment
                        int attachmentID = Convert.ToInt32(this.lsvAttachments.SelectedItems[0].Name);
                        bytes = CRMGateway.GetAttachment(attachmentID);
                        string file = App.Config.TempFolder + this.lsvAttachments.SelectedItems[0].Text.Trim();
                        try { System.IO.File.Delete(file); } catch { }
                        FileStream fs=null;
                        try {
                            fs = new FileStream(file,FileMode.OpenOrCreate,FileAccess.Write);
                            System.IO.BinaryWriter writer = new BinaryWriter(fs);
                            writer.Write(bytes);
                            writer.Flush();
                            writer.Close();
                        }
                        finally { fs.Close(); }
                        System.Diagnostics.Process.Start(file);
                        break;
                    case "btnActionsAttach":
                        //Save an attachment to the selected action
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.AddExtension = true;
                        dlgOpen.Filter = "All Files (*.*) | *.*";
                        dlgOpen.FilterIndex = 0;
                        dlgOpen.Title = "Select Attachment to Save...";
                        dlgOpen.FileName = "";
                        if(dlgOpen.ShowDialog(this)==DialogResult.OK) {
                            string name = new System.IO.FileInfo(dlgOpen.FileName).Name;
                            fs = new System.IO.FileStream(dlgOpen.FileName,System.IO.FileMode.Open,System.IO.FileAccess.Read);
                            System.IO.BinaryReader reader = new System.IO.BinaryReader(fs);
                            bytes = reader.ReadBytes((int)fs.Length);
                            reader.Close();
                            bool created = CRMGateway.AddAttachment(name,bytes,actionID);
                            OnGridSelectionChanged(this.grdIssues,null);
                        }
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnHelpItemClick(object sender,System.EventArgs e) {
            //Event hanlder for configurable help menu items
            try {
                ToolStripDropDownItem menu = (ToolStripDropDownItem)sender;
                Help.ShowHelp(this,this.mHelpItems.GetValues(menu.Text)[0]);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        #endregion
        #region Local Services: Title, ColumnHeaders, ColumnFilters, setUserServices(), buildHelpMenu()
        private string Title {
            get {
                string title="";
                if(this.mIssue != null) {
                    title = this.mIssue.Type.Trim();
                    if(this.mIssue.CompanyName.Trim() != "All") {
                        title += ": " + this.mIssue.CompanyName.Trim();
                        if(this.mIssue.StoreNumber > 0)
                            title += " #" + this.mIssue.StoreNumber.ToString();
                    }
                    else {
                        if(this.mIssue.AgentNumber.Trim() != "All")
                            title += ": Agent#" + this.mIssue.AgentNumber.Trim();
                        else
                            title += ": All Agents";
                    }
                    if(this.mIssue.Subject.Trim().Length > 0) title += " - " + this.mIssue.Subject.Trim();
                }
                return title;
            }
        }
        private string ColumnHeaders {
            get {
                MemoryStream ms = new MemoryStream();
                this.grdIssues.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if(value.Length > 0) {
                    MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdIssues.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                    this.grdIssues.DisplayLayout.Bands[0].Columns["LastActionCreated"].SortIndicator = SortIndicator.Descending;
                }
            }
        }
        private string ColumnFilters {
            get {
                MemoryStream ms = new MemoryStream();
                this.grdIssues.DisplayLayout.SaveAsXml(ms,PropertyCategories.ColumnFilters);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if(value.Length > 0) {
                    MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdIssues.DisplayLayout.LoadFromXml(ms,PropertyCategories.ColumnFilters);
                    this.grdIssues.DisplayLayout.RefreshFilters();
                    OnRowFilterChanged(null,null);
                }
            }
        }
        private void setUserServices() {
			//Set user services
			try {
                this.mnuFileNew.Enabled = this.btnNew.Enabled = this.cboView.Text=="Current Issues";
                this.mnuFileOpen.Enabled = this.btnOpen.Enabled = this.cboView.Text=="Current Issues" && this.grdIssues.Selected.Rows.Count > 0;
                this.mnuFileSave.Enabled = this.btnSave.Enabled = false;
                this.mnuFileSaveAs.Enabled = this.grdIssues.Focused && this.grdIssues.Rows.Count > 0;
                this.mnuFileSetup.Enabled = true;
                this.mnuFilePrint.Enabled = this.btnPrint.Enabled = this.grdIssues.Focused && this.grdIssues.Rows.Count > 0;
                this.mnuFilePreview.Enabled = this.grdIssues.Focused && this.grdIssues.Rows.Count > 0;
                this.mnuFileExit.Enabled = true;
                this.mnuEditCut.Enabled = this.mnuEditCopy.Enabled = this.mnuEditPaste.Enabled = false;
                this.mnuEditSearch.Enabled = true;
                this.mnuViewRefresh.Enabled = this.csRefresh.Enabled = this.btnRefresh.Enabled = true;
                this.mnuViewFont.Enabled = true;
                this.mnuViewShowAlert.Enabled = this.mnuViewHideWhenMin.Enabled = true;
                this.mnuViewToolbar.Enabled = this.mnuViewStatusBar.Enabled = true;
                this.csReminder.Enabled = this.grdIssues.Selected.Rows.Count > 0;
                this.csClearFlag.Enabled = this.grdIssues.Selected.Rows.Count > 0 && this.grdIssues.Selected.Rows[0].Cells["Reminder"].Value != null;
                this.mnuToolsConfig.Enabled = true;
				this.mnuHelpAbout.Enabled = true;

                this.mnuFileActionNew.Enabled = this.csActionsNew.Enabled = this.btnActionsNew.Enabled = (this.mIssue != null && this.mIssue.ID > 0);
                this.csActionsPrint.Enabled = this.btnActionsPrint.Enabled = this.mIssue != null;
                this.csRefresh.Enabled = this.btnActionsRefresh.Enabled = this.mIssue != null;
                this.btnActionsOpenAttach.Enabled = this.mIssue != null && this.lsvAttachments.Focused && this.lsvAttachments.SelectedItems.Count > 0;
                this.btnActionsAttach.Enabled = (this.mIssue != null && this.mIssue.ID > 0 && this.lsvActions.SelectedItems.Count > 0 && this.lsvActions.SelectedItems[0].Tag.ToString() == "0");

                this.stbMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(CRMGateway.ServiceState,CRMGateway.ServiceAddress));
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            //finally { Application.DoEvents(); }
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
                    item.Click += new System.EventHandler(this.OnHelpItemClick);
                    item.Enabled = (sValue != "");
                    this.mnuHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region Tray Icon: OnTrayIconHideWhenMinimizedChanged(), OnTrayIconUnhide()
        private void OnTrayIconHideWhenMinimizedChanged(object sender,EventArgs e) {
            //
            if(this.WindowState == FormWindowState.Minimized) this.Visible = !this.mTrayIcon.HideWhenMinimized;
        }
        private void OnTrayIconUnhide(object sender,EventArgs e) {
            //
            this.WindowState = FormWindowState.Maximized;
            this.Visible = true;
            this.Activate();
        }
        #endregion
        #region Auto Refresh Services: OnRunWorkerCompleted(), postIssueUpdates()
        public void OnRunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
            //
            try {
                DataSet ds=null;
                if(this.InvokeRequired) {
                    this.Invoke(new RunWorkerCompletedEventHandler(OnRunWorkerCompleted),new object[] { sender,e });
                }
                else {
                    ds = (DataSet)e.Result;
                    this.mGridSvcIssues.CaptureState("ID");
                    lock(this.mIssues) {
                        this.mIssues.Clear();
                        this.mIssues.Merge(ds);
                    }
                    this.mGridSvcIssues.RestoreState();
                    postIssueUpdates();
                }
            }
            catch { }
        }
        private void postIssueUpdates() {
            //Check for new issue actions and fire an event for each one found
            DateTime lastUpdated = CRMGateway.LastIssueUpdateTime;
            for(int i=0;i<this.mIssues.IssueTable.Rows.Count;i++) {
                //Find issues with LastAction that has not been posted yet
                //Skip 'New' actions and actions from creator
                CRMDataset.IssueTableRow issue = this.mIssues.IssueTable[i];
                if(issue.LastActionCreated.CompareTo(lastUpdated) > 0 && issue.LastActionDescription != "New" && issue.LastActionUserID != Environment.UserName) {
                    //Post a NewIssue event with an issue instance that includes the last action
                    if(this.mTrayIcon.ShowAlert) {
                        string tipText = "\nType: " + issue.Type + "\nAction: " + issue.LastActionDescription + "\n\nZone: " + issue.Zone + "\nStore#: " + (!issue.IsStoreNumberNull() ? issue.StoreNumber.ToString() : "") + "\nCompany: " + (!issue.IsCompanyNameNull() ? issue.CompanyName : "") + "\nAgent: " + (!issue.IsAgentNumberNull() ? issue.AgentNumber : "");
                        this.mTrayIcon.ShowBalloonTip(5000,issue.Subject,tipText,ToolTipIcon.Info);
                    }

                    //Update mLastIssueUpdateTime time to keep notification to once for an updated issue
                    if(issue.LastActionCreated.CompareTo(CRMGateway.LastIssueUpdateTime) > 0) CRMGateway.LastIssueUpdateTime = issue.LastActionCreated;
                }
            }
        }
        #endregion
        #region Reminders: OnOpenReminderItem()
        private void OnOpenReminderItem(object source,ReminderEventArgs e) {
            //Event handler for OpenItem event
            try {
                for(int i=0;i<this.grdIssues.Rows.Count;i++) {
                    if(Convert.ToInt32(this.grdIssues.Rows[i].Cells["ID"].Value) == e.ID) {
                        this.grdIssues.Rows[i].Selected = true;
                        this.grdIssues.Rows.ScrollCardIntoView(this.grdIssues.Rows[i]);
                        break;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #endregion
        #region Search Tool
        private void OnSearchChanged(object sender,EventArgs e) {
            //Event handler for change in search criteria
            //Validate
            this.btnSearch.Enabled = true;
        }
        private void OnSearch(object sender,EventArgs e) {
            //Event handler for search/reset button clicked
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnReset":
                        this.txtZone.Text = this.txtStore.Text = this.txtAgent.Text = "";
                        this.txtCompany.Text = "";
                        this.txtType.Text = this.txtAction.Text = this.txtReceived.Text = "";
                        this.txtSubject.Text = this.txtContact.Text = "";
                        this.txtOriginator.Text = this.txtCoordinator.Text = "";
                        break;
                    case "btnSearch":
                        //Search
                        this.mFindIssues.Clear();
                        object[] o = new object[] { 
                        (this.txtZone.Text.Length > 0?this.txtZone.Text:null),
                        (this.txtStore.Text.Length > 0?this.txtStore.Text:null),
                        (this.txtAgent.Text.Length > 0?this.txtAgent.Text:null),
                        (this.txtCompany.Text.Length > 0?this.txtCompany.Text:null),
                        (this.txtType.Text.Length > 0?this.txtType.Text:null),
                        (this.txtAction.Text.Length > 0?this.txtAction.Text:null),
                        (this.txtReceived.Text.Length > 0?this.txtReceived.Text:null),
                        (this.txtSubject.Text.Length > 0?this.txtSubject.Text:null),
                        (this.txtContact.Text.Length > 0?this.txtContact.Text:null),
                        (this.txtOriginator.Text.Length > 0?this.txtOriginator.Text:null),
                        (this.txtCoordinator.Text.Length > 0?this.txtCoordinator.Text:null) };
                        this.mFindIssues.Merge(CRMGateway.SearchIssuesAdvanced(o));
                        this.cboView.SelectedItem = "Search Results";
                        OnViewChanged(this.cboView, EventArgs.Empty);
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #endregion
    }
}
