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
using Excel=Microsoft.Office.Interop.Excel;

namespace Argix.Finance {
    //
    public class _frmMain:System.Windows.Forms.Form {
        //Members
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvc = null,mGridSvcT = null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
        private string mWorkingDirectory=AppDomain.CurrentDomain.BaseDirectory, mQuoteFile="";

        private const string MSG_TITLE = "Argix Logistics Rate Quote";

        #region Controls

        private System.Windows.Forms.TextBox txtDiscount;
        private System.Windows.Forms.TextBox txtFloorMin;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdRates;
        private System.Windows.Forms.ComboBox cboClassCode;
        private System.Windows.Forms.GroupBox grpProperties;
        private System.Windows.Forms.Label _lblClassCode;
        private System.Windows.Forms.Label _lblFloorMin;
        private System.Windows.Forms.Label _lblOrigin;
        private System.Windows.Forms.Label _lblDiscount;
        private System.Windows.Forms.ListBox lstZips;
        private System.Windows.Forms.TextBox txtZips;
        private System.Windows.Forms.TextBox txtOrigin;
        private System.Windows.Forms.GroupBox grpZips;
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
        private Label label1;
        private BindingSource mRates;
        private ArgixStatusBar ssMain;
        private ToolStripMenuItem msToolsConfig;
        private Button btnFill;
        private Label _lblHigh;
        private Label _lblLow;
        private NumericUpDown updHigh;
        private NumericUpDown updLow;
        private Button btnClear;
        private GroupBox grpAuto;
        private BindingSource mTariffs;
        private BindingSource mClassCodes;
        private SplitContainer scMain;
        private UltraGrid grdTariffs;
        private RateQuoteDataset rateQuoteDataset;
        private SplitContainer scInputs;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private Panel pnlInputHeader;
        private Label lblCloseRequest;
        private Label lblInputHeader;
        private IContainer components;
        #endregion

        //Interface
        public _frmMain() {
            //Constructor
            InitializeComponent();
            this.Text = "Argix Logistics " + App.Product;
            buildHelpMenu();
            Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
            Thread.Sleep(3000);
            
            //Create data and UI services
            this.mPageSettings = new PageSettings();
            this.mPageSettings.Landscape = true;
            this.mGridSvc = new UltraGridSvc(this.grdRates);
            this.mGridSvcT = new UltraGridSvc(this.grdTariffs);
            this.mToolTip = new System.Windows.Forms.ToolTip();
            this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],1000,3000);
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Rate", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DestZip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinCharge");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OrgZip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate1");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate10001");
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate1001");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate20001");
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate2001");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate5001");
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rate501");
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DataModule", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PropertyChanged1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("descriptionField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("effectiveDateField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("productNumberField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("releaseField");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("tariffNameField");
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            this._lblOrigin = new System.Windows.Forms.Label();
            this.txtZips = new System.Windows.Forms.TextBox();
            this._lblClassCode = new System.Windows.Forms.Label();
            this._lblDiscount = new System.Windows.Forms.Label();
            this.txtDiscount = new System.Windows.Forms.TextBox();
            this._lblFloorMin = new System.Windows.Forms.Label();
            this.txtFloorMin = new System.Windows.Forms.TextBox();
            this.grdRates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mRates = new System.Windows.Forms.BindingSource(this.components);
            this.cboClassCode = new System.Windows.Forms.ComboBox();
            this.mClassCodes = new System.Windows.Forms.BindingSource(this.components);
            this.rateQuoteDataset = new Argix.RateQuoteDataset();
            this.grpProperties = new System.Windows.Forms.GroupBox();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.grpZips = new System.Windows.Forms.GroupBox();
            this.btnClear = new System.Windows.Forms.Button();
            this.grpAuto = new System.Windows.Forms.GroupBox();
            this.updLow = new System.Windows.Forms.NumericUpDown();
            this.btnFill = new System.Windows.Forms.Button();
            this._lblHigh = new System.Windows.Forms.Label();
            this.updHigh = new System.Windows.Forms.NumericUpDown();
            this._lblLow = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lstZips = new System.Windows.Forms.ListBox();
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
            this.scMain = new System.Windows.Forms.SplitContainer();
            this.scInputs = new System.Windows.Forms.SplitContainer();
            this.grdTariffs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mTariffs = new System.Windows.Forms.BindingSource(this.components);
            this.pnlInputHeader = new System.Windows.Forms.Panel();
            this.lblCloseRequest = new System.Windows.Forms.Label();
            this.lblInputHeader = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.grdRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClassCodes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateQuoteDataset)).BeginInit();
            this.grpProperties.SuspendLayout();
            this.grpZips.SuspendLayout();
            this.grpAuto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updLow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.updHigh)).BeginInit();
            this.csMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).BeginInit();
            this.scMain.Panel1.SuspendLayout();
            this.scMain.Panel2.SuspendLayout();
            this.scMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.scInputs)).BeginInit();
            this.scInputs.Panel1.SuspendLayout();
            this.scInputs.Panel2.SuspendLayout();
            this.scInputs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTariffs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTariffs)).BeginInit();
            this.pnlInputHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblOrigin
            // 
            this._lblOrigin.Location = new System.Drawing.Point(6, 24);
            this._lblOrigin.Name = "_lblOrigin";
            this._lblOrigin.Size = new System.Drawing.Size(96, 16);
            this._lblOrigin.TabIndex = 0;
            this._lblOrigin.Text = "Origin";
            this._lblOrigin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZips
            // 
            this.txtZips.Location = new System.Drawing.Point(50, 28);
            this.txtZips.Name = "txtZips";
            this.txtZips.Size = new System.Drawing.Size(112, 20);
            this.txtZips.TabIndex = 10;
            this.txtZips.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnDestinationKeyPress);
            this.txtZips.Leave += new System.EventHandler(this.OnDestinationLeave);
            // 
            // _lblClassCode
            // 
            this._lblClassCode.Location = new System.Drawing.Point(6, 50);
            this._lblClassCode.Name = "_lblClassCode";
            this._lblClassCode.Size = new System.Drawing.Size(96, 16);
            this._lblClassCode.TabIndex = 8;
            this._lblClassCode.Text = "Class Code";
            this._lblClassCode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDiscount
            // 
            this._lblDiscount.Location = new System.Drawing.Point(6, 76);
            this._lblDiscount.Name = "_lblDiscount";
            this._lblDiscount.Size = new System.Drawing.Size(96, 16);
            this._lblDiscount.TabIndex = 15;
            this._lblDiscount.Text = "Discount %";
            this._lblDiscount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDiscount
            // 
            this.txtDiscount.Location = new System.Drawing.Point(109, 76);
            this.txtDiscount.Name = "txtDiscount";
            this.txtDiscount.Size = new System.Drawing.Size(96, 20);
            this.txtDiscount.TabIndex = 6;
            this.txtDiscount.TextChanged += new System.EventHandler(this.OnDiscountChanged);
            this.txtDiscount.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnDiscountKeyPress);
            // 
            // _lblFloorMin
            // 
            this._lblFloorMin.Location = new System.Drawing.Point(6, 102);
            this._lblFloorMin.Name = "_lblFloorMin";
            this._lblFloorMin.Size = new System.Drawing.Size(96, 16);
            this._lblFloorMin.TabIndex = 17;
            this._lblFloorMin.Text = "Floor Min";
            this._lblFloorMin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFloorMin
            // 
            this.txtFloorMin.Location = new System.Drawing.Point(109, 102);
            this.txtFloorMin.Name = "txtFloorMin";
            this.txtFloorMin.Size = new System.Drawing.Size(96, 20);
            this.txtFloorMin.TabIndex = 8;
            this.txtFloorMin.TextChanged += new System.EventHandler(this.OnFloorMinChanged);
            this.txtFloorMin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnFloorMinKeyPress);
            // 
            // grdRates
            // 
            this.grdRates.DataSource = this.mRates;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Dest Zip";
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 75;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn2.CellAppearance = appearance5;
            ultraGridColumn2.Header.Caption = "Min Chg";
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Width = 75;
            ultraGridColumn3.Header.Caption = "Org Zip";
            ultraGridColumn3.Header.VisiblePosition = 0;
            ultraGridColumn3.Width = 75;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn4.CellAppearance = appearance6;
            ultraGridColumn4.Header.Caption = "1-499";
            ultraGridColumn4.Header.VisiblePosition = 3;
            appearance7.TextHAlignAsString = "Right";
            ultraGridColumn5.CellAppearance = appearance7;
            ultraGridColumn5.Header.Caption = "10,000-19,999";
            ultraGridColumn5.Header.VisiblePosition = 8;
            appearance8.TextHAlignAsString = "Right";
            ultraGridColumn6.CellAppearance = appearance8;
            ultraGridColumn6.Header.Caption = "1,000-1,999";
            ultraGridColumn6.Header.VisiblePosition = 5;
            appearance9.TextHAlignAsString = "Right";
            ultraGridColumn7.CellAppearance = appearance9;
            ultraGridColumn7.Header.Caption = ">20,000";
            ultraGridColumn7.Header.VisiblePosition = 9;
            appearance10.TextHAlignAsString = "Right";
            ultraGridColumn8.CellAppearance = appearance10;
            ultraGridColumn8.Header.Caption = "2,000-4,999";
            ultraGridColumn8.Header.VisiblePosition = 6;
            appearance11.TextHAlignAsString = "Right";
            ultraGridColumn9.CellAppearance = appearance11;
            ultraGridColumn9.Header.Caption = "5,000-9,999";
            ultraGridColumn9.Header.VisiblePosition = 7;
            appearance16.TextHAlignAsString = "Right";
            ultraGridColumn10.CellAppearance = appearance16;
            ultraGridColumn10.Header.Caption = "500-999";
            ultraGridColumn10.Header.VisiblePosition = 4;
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
            ultraGridColumn10});
            this.grdRates.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.CaptionAppearance = appearance2;
            this.grdRates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdRates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdRates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdRates.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdRates.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdRates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdRates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdRates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdRates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRates.Location = new System.Drawing.Point(2, 5);
            this.grdRates.Name = "grdRates";
            this.grdRates.Size = new System.Drawing.Size(515, 363);
            this.grdRates.TabIndex = 22;
            this.grdRates.Text = "LTL Rates";
            this.grdRates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mRates
            // 
            this.mRates.DataSource = typeof(Argix.Finance.Rates);
            // 
            // cboClassCode
            // 
            this.cboClassCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClassCode.DataSource = this.mClassCodes;
            this.cboClassCode.DisplayMember = "Description";
            this.cboClassCode.Location = new System.Drawing.Point(109, 50);
            this.cboClassCode.Name = "cboClassCode";
            this.cboClassCode.Size = new System.Drawing.Size(214, 21);
            this.cboClassCode.TabIndex = 4;
            this.cboClassCode.ValueMember = "Class";
            this.cboClassCode.TextChanged += new System.EventHandler(this.OnClassCodeChanged);
            // 
            // mClassCodes
            // 
            this.mClassCodes.DataMember = "ClassCodeTable";
            this.mClassCodes.DataSource = this.rateQuoteDataset;
            // 
            // rateQuoteDataset
            // 
            this.rateQuoteDataset.DataSetName = "RateQuoteDataset";
            this.rateQuoteDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpProperties
            // 
            this.grpProperties.Controls.Add(this._lblOrigin);
            this.grpProperties.Controls.Add(this._lblFloorMin);
            this.grpProperties.Controls.Add(this.txtDiscount);
            this.grpProperties.Controls.Add(this._lblDiscount);
            this.grpProperties.Controls.Add(this._lblClassCode);
            this.grpProperties.Controls.Add(this.txtFloorMin);
            this.grpProperties.Controls.Add(this.cboClassCode);
            this.grpProperties.Controls.Add(this.txtOrigin);
            this.grpProperties.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpProperties.Location = new System.Drawing.Point(5, 24);
            this.grpProperties.Margin = new System.Windows.Forms.Padding(5);
            this.grpProperties.Name = "grpProperties";
            this.grpProperties.Size = new System.Drawing.Size(329, 137);
            this.grpProperties.TabIndex = 0;
            this.grpProperties.TabStop = false;
            this.grpProperties.Enter += new System.EventHandler(this.OnEnterRequest);
            this.grpProperties.Leave += new System.EventHandler(this.OnLeaveRequest);
            // 
            // txtOrigin
            // 
            this.txtOrigin.AcceptsReturn = true;
            this.txtOrigin.Location = new System.Drawing.Point(109, 24);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.Size = new System.Drawing.Size(96, 20);
            this.txtOrigin.TabIndex = 2;
            this.txtOrigin.TextChanged += new System.EventHandler(this.OnOriginChanged);
            this.txtOrigin.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnOriginKeyPress);
            // 
            // grpZips
            // 
            this.grpZips.Controls.Add(this.btnClear);
            this.grpZips.Controls.Add(this.grpAuto);
            this.grpZips.Controls.Add(this.label1);
            this.grpZips.Controls.Add(this.lstZips);
            this.grpZips.Controls.Add(this.txtZips);
            this.grpZips.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpZips.Location = new System.Drawing.Point(5, 161);
            this.grpZips.Name = "grpZips";
            this.grpZips.Size = new System.Drawing.Size(329, 243);
            this.grpZips.TabIndex = 10;
            this.grpZips.TabStop = false;
            this.grpZips.Text = "Destinations";
            this.grpZips.Enter += new System.EventHandler(this.OnEnterRequest);
            this.grpZips.Leave += new System.EventHandler(this.OnLeaveRequest);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(180, 197);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(70, 23);
            this.btnClear.TabIndex = 20;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.OnClearZips);
            // 
            // grpAuto
            // 
            this.grpAuto.Controls.Add(this.updLow);
            this.grpAuto.Controls.Add(this.btnFill);
            this.grpAuto.Controls.Add(this._lblHigh);
            this.grpAuto.Controls.Add(this.updHigh);
            this.grpAuto.Controls.Add(this._lblLow);
            this.grpAuto.Location = new System.Drawing.Point(180, 54);
            this.grpAuto.Name = "grpAuto";
            this.grpAuto.Size = new System.Drawing.Size(121, 135);
            this.grpAuto.TabIndex = 19;
            this.grpAuto.TabStop = false;
            this.grpAuto.Text = "Auto Populate";
            // 
            // updLow
            // 
            this.updLow.Location = new System.Drawing.Point(53, 27);
            this.updLow.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updLow.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updLow.Name = "updLow";
            this.updLow.Size = new System.Drawing.Size(54, 20);
            this.updLow.TabIndex = 15;
            this.updLow.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updLow.ValueChanged += new System.EventHandler(this.OnLowHighChanged);
            // 
            // btnFill
            // 
            this.btnFill.Location = new System.Drawing.Point(53, 91);
            this.btnFill.Name = "btnFill";
            this.btnFill.Size = new System.Drawing.Size(54, 23);
            this.btnFill.TabIndex = 14;
            this.btnFill.Text = "Fill";
            this.btnFill.UseVisualStyleBackColor = true;
            this.btnFill.Click += new System.EventHandler(this.OnAddZips);
            // 
            // _lblHigh
            // 
            this._lblHigh.Location = new System.Drawing.Point(8, 59);
            this._lblHigh.Name = "_lblHigh";
            this._lblHigh.Size = new System.Drawing.Size(39, 21);
            this._lblHigh.TabIndex = 18;
            this._lblHigh.Text = "High";
            this._lblHigh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // updHigh
            // 
            this.updHigh.Location = new System.Drawing.Point(53, 59);
            this.updHigh.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.updHigh.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updHigh.Name = "updHigh";
            this.updHigh.Size = new System.Drawing.Size(54, 20);
            this.updHigh.TabIndex = 16;
            this.updHigh.Value = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.updHigh.ValueChanged += new System.EventHandler(this.OnLowHighChanged);
            // 
            // _lblLow
            // 
            this._lblLow.Location = new System.Drawing.Point(8, 27);
            this._lblLow.Name = "_lblLow";
            this._lblLow.Size = new System.Drawing.Size(39, 21);
            this._lblLow.TabIndex = 17;
            this._lblLow.Text = "Low";
            this._lblLow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(6, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 22);
            this.label1.TabIndex = 13;
            this.label1.Text = "Zip";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lstZips
            // 
            this.lstZips.ContextMenuStrip = this.csMain;
            this.lstZips.Location = new System.Drawing.Point(50, 60);
            this.lstZips.Name = "lstZips";
            this.lstZips.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstZips.Size = new System.Drawing.Size(112, 160);
            this.lstZips.Sorted = true;
            this.lstZips.TabIndex = 12;
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
            this.msMain.Size = new System.Drawing.Size(862, 24);
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
            this.msHelpAbout.Size = new System.Drawing.Size(183, 22);
            this.msHelpAbout.Text = "&About Rate Quotes...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(180, 6);
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
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(862, 54);
            this.tsMain.TabIndex = 32;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 51);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New quote";
            this.tsNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 51);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open an existing quote";
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
            this.tsSave.ToolTipText = "Save the current quote";
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
            this.tsExport.ToolTipText = "Export the current rates to Excel";
            this.tsExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 51);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print the current rates";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 54);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.FormulaEvaluator;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(36, 51);
            this.tsRefresh.Text = "Rate";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Calculate rates for this quote";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 451);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(862, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 33;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // scMain
            // 
            this.scMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scMain.Location = new System.Drawing.Point(0, 78);
            this.scMain.Name = "scMain";
            // 
            // scMain.Panel1
            // 
            this.scMain.Panel1.Controls.Add(this.scInputs);
            // 
            // scMain.Panel2
            // 
            this.scMain.Panel2.Controls.Add(this.grdRates);
            this.scMain.Panel2.Padding = new System.Windows.Forms.Padding(2, 5, 5, 5);
            this.scMain.Size = new System.Drawing.Size(862, 373);
            this.scMain.SplitterDistance = 336;
            this.scMain.TabIndex = 34;
            // 
            // scInputs
            // 
            this.scInputs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scInputs.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scInputs.Location = new System.Drawing.Point(0, 0);
            this.scInputs.Name = "scInputs";
            this.scInputs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scInputs.Panel1
            // 
            this.scInputs.Panel1.Controls.Add(this.grdTariffs);
            this.scInputs.Panel1.Padding = new System.Windows.Forms.Padding(5, 5, 2, 2);
            this.scInputs.Panel1MinSize = 100;
            // 
            // scInputs.Panel2
            // 
            this.scInputs.Panel2.Controls.Add(this.grpZips);
            this.scInputs.Panel2.Controls.Add(this.grpProperties);
            this.scInputs.Panel2.Controls.Add(this.pnlInputHeader);
            this.scInputs.Panel2.Padding = new System.Windows.Forms.Padding(5, 0, 2, 0);
            this.scInputs.Panel2.Enter += new System.EventHandler(this.OnEnterRequest);
            this.scInputs.Panel2.Leave += new System.EventHandler(this.OnLeaveRequest);
            this.scInputs.Panel2MinSize = 100;
            this.scInputs.Size = new System.Drawing.Size(336, 373);
            this.scInputs.SplitterDistance = 154;
            this.scInputs.TabIndex = 36;
            // 
            // grdTariffs
            // 
            this.grdTariffs.DataSource = this.mTariffs;
            appearance12.BackColor = System.Drawing.SystemColors.Window;
            appearance12.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance12.TextHAlignAsString = "Left";
            this.grdTariffs.DisplayLayout.Appearance = appearance12;
            ultraGridColumn11.Header.VisiblePosition = 0;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.Caption = "Description";
            ultraGridColumn12.Header.VisiblePosition = 3;
            ultraGridColumn12.Width = 150;
            ultraGridColumn13.Header.Caption = "Eff Date";
            ultraGridColumn13.Header.VisiblePosition = 2;
            ultraGridColumn13.Width = 75;
            ultraGridColumn14.Header.Caption = "Product#";
            ultraGridColumn14.Header.VisiblePosition = 4;
            ultraGridColumn14.Width = 75;
            ultraGridColumn15.Header.Caption = "Release";
            ultraGridColumn15.Header.VisiblePosition = 5;
            ultraGridColumn15.Width = 75;
            ultraGridColumn16.Header.Caption = "Name";
            ultraGridColumn16.Header.VisiblePosition = 1;
            ultraGridColumn16.Width = 75;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16});
            this.grdTariffs.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance13.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance13.FontData.BoldAsString = "True";
            appearance13.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance13.TextHAlignAsString = "Left";
            this.grdTariffs.DisplayLayout.CaptionAppearance = appearance13;
            this.grdTariffs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTariffs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance14.BackColor = System.Drawing.SystemColors.Control;
            appearance14.FontData.BoldAsString = "True";
            appearance14.TextHAlignAsString = "Left";
            this.grdTariffs.DisplayLayout.Override.HeaderAppearance = appearance14;
            this.grdTariffs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTariffs.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance15.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTariffs.DisplayLayout.Override.RowAppearance = appearance15;
            this.grdTariffs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTariffs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTariffs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTariffs.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTariffs.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTariffs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTariffs.Location = new System.Drawing.Point(5, 5);
            this.grdTariffs.Name = "grdTariffs";
            this.grdTariffs.Size = new System.Drawing.Size(329, 147);
            this.grdTariffs.TabIndex = 35;
            this.grdTariffs.Text = "Available Tariffs";
            this.grdTariffs.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTariffs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTariffAfterSelectChange);
            // 
            // mTariffs
            // 
            this.mTariffs.DataSource = typeof(Argix.Finance.DataModule);
            // 
            // pnlInputHeader
            // 
            this.pnlInputHeader.Controls.Add(this.lblCloseRequest);
            this.pnlInputHeader.Controls.Add(this.lblInputHeader);
            this.pnlInputHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInputHeader.ForeColor = System.Drawing.SystemColors.WindowText;
            this.pnlInputHeader.Location = new System.Drawing.Point(5, 0);
            this.pnlInputHeader.Name = "pnlInputHeader";
            this.pnlInputHeader.Padding = new System.Windows.Forms.Padding(3);
            this.pnlInputHeader.Size = new System.Drawing.Size(329, 24);
            this.pnlInputHeader.TabIndex = 125;
            this.pnlInputHeader.Enter += new System.EventHandler(this.OnEnterRequest);
            this.pnlInputHeader.Leave += new System.EventHandler(this.OnLeaveRequest);
            // 
            // lblCloseRequest
            // 
            this.lblCloseRequest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseRequest.BackColor = System.Drawing.SystemColors.Control;
            this.lblCloseRequest.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCloseRequest.Location = new System.Drawing.Point(308, 4);
            this.lblCloseRequest.Name = "lblCloseRequest";
            this.lblCloseRequest.Size = new System.Drawing.Size(16, 16);
            this.lblCloseRequest.TabIndex = 115;
            this.lblCloseRequest.Text = "X";
            this.lblCloseRequest.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseRequest.Visible = false;
            this.lblCloseRequest.Enter += new System.EventHandler(this.OnEnterRequest);
            this.lblCloseRequest.Leave += new System.EventHandler(this.OnLeaveRequest);
            // 
            // lblInputHeader
            // 
            this.lblInputHeader.BackColor = System.Drawing.SystemColors.Control;
            this.lblInputHeader.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblInputHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblInputHeader.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblInputHeader.Location = new System.Drawing.Point(3, 3);
            this.lblInputHeader.Name = "lblInputHeader";
            this.lblInputHeader.Size = new System.Drawing.Size(323, 18);
            this.lblInputHeader.TabIndex = 113;
            this.lblInputHeader.Text = "LTL Request";
            this.lblInputHeader.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblInputHeader.Enter += new System.EventHandler(this.OnEnterRequest);
            this.lblInputHeader.Leave += new System.EventHandler(this.OnLeaveRequest);
            // 
            // _frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(862, 475);
            this.Controls.Add(this.scMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "_frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Rate Quote";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClassCodes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateQuoteDataset)).EndInit();
            this.grpProperties.ResumeLayout(false);
            this.grpProperties.PerformLayout();
            this.grpZips.ResumeLayout(false);
            this.grpZips.PerformLayout();
            this.grpAuto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.updLow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.updHigh)).EndInit();
            this.csMain.ResumeLayout(false);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.scMain.Panel1.ResumeLayout(false);
            this.scMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scMain)).EndInit();
            this.scMain.ResumeLayout(false);
            this.scInputs.Panel1.ResumeLayout(false);
            this.scInputs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scInputs)).EndInit();
            this.scInputs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTariffs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTariffs)).EndInit();
            this.pnlInputHeader.ResumeLayout(false);
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
                    this.scInputs.SplitterDistance = global::Argix.Properties.Settings.Default.InputSplitterDistance;
                    this.scMain.SplitterDistance = global::Argix.Properties.Settings.Default.MainSplitterDistance;
                    this.mWorkingDirectory = global::Argix.Properties.Settings.Default.WorkingDirectory.Length > 0 ? global::Argix.Properties.Settings.Default.WorkingDirectory : AppDomain.CurrentDomain.BaseDirectory;
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
                this.mToolTip.SetToolTip(this.txtZips,"Enter 5-digit zip code and press Enter.");
				#endregion
				
				//Set control defaults
				#region Grid customizations from normal layout (to support cell editing)
				this.grdRates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                //this.grdRates.DisplayLayout.Bands[0].Columns["SortCenter"].SortIndicator = SortIndicator.Ascending;
				#endregion

                ServiceInfo t = FinanceGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;

                this.mTariffs.DataSource = FinanceGateway.GetAvailableTariffs();
                if (this.grdTariffs.Rows.Count > 0) this.grdTariffs.Rows[0].Selected = true;
                this.mClassCodes.DataSource = FinanceGateway.GetClassCodes();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,System.ComponentModel.CancelEventArgs e) {
            //Event handler for form closing event
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.InputSplitterDistance = this.scInputs.SplitterDistance;
                global::Argix.Properties.Settings.Default.MainSplitterDistance = this.scMain.SplitterDistance;
                global::Argix.Properties.Settings.Default.WorkingDirectory = this.mWorkingDirectory;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.Save();
            }
        }
        private void OnTariffAfterSelectChange(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for change in the selected tariff
            try {
                DataModule tariff = (DataModule)this.mTariffs.Current;
                if(tariff != null)
                    this.grdRates.Text = "LTL Rates (" + tariff.tariffNameField + " " + tariff.effectiveDateField + ")";
                else
                    this.grdRates.Text = "LTL Rates";
                this.mRates.Clear();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnOriginKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            //char 8 = Backspace key
            try {
                if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
                    e.Handled = true;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnOriginChanged(object sender,System.EventArgs e) {
            try {
                this.mRates.Clear();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnClassCodeChanged(object sender,System.EventArgs e) {
            try {
                this.mRates.Clear();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDiscountKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            try {
                if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8 || e.KeyChar == '.'))
                    e.Handled = true;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDiscountChanged(object sender,System.EventArgs e) {
            try {
                this.mRates.Clear();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnFloorMinKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            try {
                if (!(Char.IsNumber(e.KeyChar) || e.KeyChar == (char)8))
                    e.Handled = true;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnFloorMinChanged(object sender,System.EventArgs e) {
            try {
                this.mRates.Clear();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDestinationKeyPress(object sender,System.Windows.Forms.KeyPressEventArgs e) {
            //Trap Enter key and add entered Zip to the list box
            try {
                if (e.KeyChar == (char)13) {
                    OnDestinationLeave(this.txtZips,EventArgs.Empty);
                    e.Handled = true;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnDestinationLeave(object sender,System.EventArgs e) {
            //Add a valid zip code to the list
            try {
                string zip = this.txtZips.Text;
                if (zip.Length == 5 || zip.Length == 3) {
                    //Check if it already exists in the list
                    if (this.lstZips.FindStringExact(zip) == -1) {
                        this.lstZips.Items.Add(zip);
                        this.txtZips.Text = "";
                    }
                    else
                        this.txtZips.Text = "";
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnLowHighChanged(object sender, EventArgs e) {
            //Event handler for change in up/down values
            try {
                //Prevent crossover
                NumericUpDown upd = (NumericUpDown)sender;
                if (upd.Name == "updLow")
                    if (upd.Value > this.updHigh.Value) this.updHigh.Value = upd.Value;
                    else
                        if (upd.Value < this.updLow.Value) this.updLow.Value = upd.Value;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnAddZips(object sender, EventArgs e) {
            try {
                for (int i = (int)this.updLow.Value;i < (int)this.updHigh.Value;i++) { this.lstZips.Items.Add(i.ToString("000") + "00"); }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnClearZips(object sender, EventArgs e) { this.lstZips.Items.Clear(); }
        private void OnCloseRequest(object sender,System.EventArgs e) {
            //Event handler to close request windows
            setUserServices();
        }
        private void OnEnterRequest(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblInputHeader.BackColor = this.lblCloseRequest.BackColor = SystemColors.ActiveCaption;
                this.lblInputHeader.ForeColor = this.lblCloseRequest.ForeColor = SystemColors.ActiveCaptionText;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnLeaveRequest(object sender,System.EventArgs e) {
            //Event handler for enter and leave events
            try {
                this.lblInputHeader.BackColor = this.lblCloseRequest.BackColor = SystemColors.Control;
                this.lblInputHeader.ForeColor = this.lblCloseRequest.ForeColor = SystemColors.ControlText;
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Event handler for menu selection
            SaveFileDialog dlgSave=null;
            RateQuoteDataset quote=null;
            DataModule tariff = null;
            string classCode="";
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "msFileNew":
                    case "tsNew":
                        this.Text = "Argix Logistics " + App.Product;
                        this.txtOrigin.Text = "";
                        if(this.cboClassCode.Items.Count > 0) this.cboClassCode.SelectedIndex=0;
                        this.txtDiscount.Text = "";
                        this.txtFloorMin.Text = "";
                        this.lstZips.Items.Clear();
                        this.mQuoteFile = "";
                        break;
                    case "msFileOpen":
                    case "tsOpen":
                        OpenFileDialog dlgOpen = new OpenFileDialog();
                        dlgOpen.DefaultExt = "xml";
                        dlgOpen.Filter = "XML Files (*.xml)|*.xml";
                        dlgOpen.InitialDirectory = this.mWorkingDirectory;
                        dlgOpen.Title = "Choose path and file name";
                        if(dlgOpen.ShowDialog() == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            if(!dlgOpen.FileName.EndsWith("xml")) {
                                MessageBox.Show(this,"Make sure it's a valid xml file saved from this program.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                                return;
                            }
                            this.Text = "Argix Logistics " + App.Product + " - " + dlgOpen.FileName;
                            this.mWorkingDirectory = new FileInfo(dlgOpen.FileName).DirectoryName;
                            this.mQuoteFile = dlgOpen.FileName;
                            quote = new RateQuoteDataset();
                            DataSet _quote = new DataSet();
                            _quote.ReadXml(dlgOpen.FileName);
                            if (_quote.DataSetName == "RateQuoteDS") {
                                //Older quote file- map to RateQuoteDataset, update the tariff, and save
                                quote.RateQuoteTable.ImportRow(_quote.Tables["RateQuoteTable"].Rows[0]);
                                quote.ZipCodeTable.Merge(_quote.Tables["ZipCodeTable"]);
                                foreach (DataModule module in this.mTariffs) {
                                    if (module.tariffNameField == quote.RateQuoteTable[0].Tariff.Substring(0,8) && module.effectiveDateField == quote.RateQuoteTable[0].Tariff.Substring(8,8)) {
                                        quote.RateQuoteTable[0].Tariff = module.productNumberField;
                                        break;
                                    }
                                }
                                quote.WriteXml(dlgOpen.FileName);
                            }
                            else {
                                quote.ReadXml(dlgOpen.FileName);
                            }
                            string tariffProductNumber = quote.RateQuoteTable[0].Tariff;
                            this.grdTariffs.Selected.Rows.Clear();
                            for (int i = 0;i < this.grdTariffs.Rows.Count;i++) {
                                if (this.grdTariffs.Rows[i].Cells["productNumberField"].Value.ToString() == tariffProductNumber) {
                                    this.grdTariffs.ActiveRow = this.grdTariffs.Rows[i];
                                    this.grdTariffs.Selected.Rows.Add(this.grdTariffs.Rows[i]);
                                    break;
                                }
                            }
                            if (this.grdTariffs.Selected.Rows.Count == 0) { this.grdTariffs.ActiveRow = this.grdTariffs.Rows[0]; this.grdTariffs.Selected.Rows.Add(this.grdTariffs.Rows[0]); }
                            this.txtOrigin.Text = !quote.RateQuoteTable[0].IsOriginNull() ? quote.RateQuoteTable[0].Origin : "";
                            this.cboClassCode.Text = !quote.RateQuoteTable[0].IsClassCodeNull() ? quote.RateQuoteTable[0].ClassCode : "";
                            this.txtDiscount.Text = !quote.RateQuoteTable[0].IsDiscountNull() ? quote.RateQuoteTable[0].Discount : "";
                            this.txtFloorMin.Text = !quote.RateQuoteTable[0].IsFloorMinNull() ? quote.RateQuoteTable[0].FloorMin : "";
                            this.lstZips.Items.Clear();
                            for(int i=0;i<quote.ZipCodeTable.Rows.Count;i++) { this.lstZips.Items.Add(quote.ZipCodeTable[i].ZipCode); }
                        }
                        break;
                    case "msFileSave":
                    case "tsSave":
                        this.Cursor = Cursors.WaitCursor;
                        quote = new RateQuoteDataset();
                        tariff = (DataModule)this.mTariffs.Current;
                        classCode = this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text;
                        quote.RateQuoteTable.AddRateQuoteTableRow(tariff.productNumberField,this.txtOrigin.Text,classCode,this.txtDiscount.Text,this.txtFloorMin.Text);
                        for(int i=0;i<this.lstZips.Items.Count;i++) { quote.ZipCodeTable.AddZipCodeTableRow(this.lstZips.Items[i].ToString()); }
                        quote.WriteXml(this.mQuoteFile);
                        break;
                    case "msFileSaveAs":
                        if(this.lstZips.Items.Count > 0) {
                            dlgSave = new SaveFileDialog();
                            dlgSave.AddExtension = true;
                            dlgSave.DefaultExt = "xml";
                            dlgSave.InitialDirectory = this.mWorkingDirectory;
                            dlgSave.Title = "Choose path and file name";
                            dlgSave.Filter = "XML Files (*.xml)|*.xml";
                            dlgSave.AddExtension = true;
                            if(dlgSave.ShowDialog() == DialogResult.OK) {
                                this.Cursor = Cursors.WaitCursor;
                                this.Text = "Argix Logistics " + App.Product + " - " + dlgSave.FileName;
                                this.mWorkingDirectory = new FileInfo(dlgSave.FileName).DirectoryName;
                                this.mQuoteFile = dlgSave.FileName;
                                quote = new RateQuoteDataset();
                                tariff = (DataModule)this.mTariffs.Current;
                                classCode = this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text;
                                quote.RateQuoteTable.AddRateQuoteTableRow(tariff.productNumberField,this.txtOrigin.Text,classCode,this.txtDiscount.Text,this.txtFloorMin.Text);
                                for(int i=0;i<this.lstZips.Items.Count;i++) { quote.ZipCodeTable.AddZipCodeTableRow(this.lstZips.Items[i].ToString()); }
                                quote.WriteXml(this.mQuoteFile);
                            }
                        }
                        else
                            MessageBox.Show(this,"There are no Zip Codes to save.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        break;
                    case "msFileExport":
                    case "tsExport":
                        if(mRates.Count > 0) {
                            dlgSave = new SaveFileDialog();
                            dlgSave.DefaultExt = "xls";
                            dlgSave.AddExtension = true;
                            dlgSave.FileName = "Rates.xls";
                            dlgSave.Filter = "XSL Files (*.xsl)|*.xsl";
                            dlgSave.ValidateNames = true;
                            dlgSave.OverwritePrompt = false;
                            dlgSave.InitialDirectory = this.mWorkingDirectory;
                            if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                                this.Cursor = Cursors.WaitCursor;
                                exportToExcel(dlgSave.FileName);
                            }
                        }
                        else
                            MessageBox.Show(this,"There are no Rates no save.",MSG_TITLE,MessageBoxButtons.OK,MessageBoxIcon.Warning);
                        break;
                    case "msFileSetup": UltraGridPrinter.PageSettings(); break;
                    case "msFilePrint": UltraGridPrinter.Print(this.grdRates,"Rate Quote " +  DateTime.Today.ToString("dd-MMM-yyyy"),true); break;
                    case "tsPrint":     UltraGridPrinter.Print(this.grdRates,"Rate Quote " +  DateTime.Today.ToString("dd-MMM-yyyy"),false); break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdRates,"Rate Quote " +  DateTime.Today.ToString("dd-MMM-yyyy")); break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut": case "csCut": case "tsCut": break;
                    case "msEditCopy": case "csCopy": case "tsCopy": break;
                    case "msEditPaste": case "csPaste": case "tsPaste": break;
                    case "csDelete":
                        if(this.lstZips.SelectedItems.Count > 0) {
                            object[] lzips = new object[this.lstZips.SelectedItems.Count];
                            this.lstZips.SelectedItems.CopyTo(lzips,0);
                            foreach(object zip in lzips) this.lstZips.Items.Remove(zip);
                            this.mRates.Clear();
                        }
                        break;
                    case "msEditSearch": case "tsSearch": break;
                    case "msViewRefresh": 
                    case "csRefresh": 
                    case "tsRefresh": 
                        //Calcualate rates
                        this.Cursor = Cursors.WaitCursor;                        
                        tariff = (DataModule)this.mTariffs.Current;
                        RateQuoteRequest request = new RateQuoteRequest(tariff);
                        request.OriginPostalCode = this.txtOrigin.Text.Trim();
                        request.ClassCode = (this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text);
                        request.MCDiscount = this.txtDiscount.Text;
                        request.UserMinimumChargeFloor = this.txtFloorMin.Text;
                        string[] _zips = new string[this.lstZips.Items.Count];
                        this.lstZips.Items.CopyTo(_zips,0);
                        request.DestinationPostalCodes = _zips;
                        this.mRates.DataSource = FinanceGateway.CalculateRates(request);
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
        #region Local Services: configApplication(), setUserServices(), buildHelpMenu(), exportToExcel()
        private void setUserServices() {
            //Set user services
            try {
                this.msFileNew.Enabled = this.tsNew.Enabled = true;
                this.msFileOpen.Enabled = this.tsOpen.Enabled = true;
                this.msFileSave.Enabled = this.tsSave.Enabled = this.mQuoteFile.Length > 0;
                this.msFileSaveAs.Enabled = (this.lstZips.Items.Count > 0);
                this.msFileExport.Enabled  = this.tsExport.Enabled = (this.grdRates.Rows.Count > 0);
                this.msFileSettings.Enabled = true;
                this.msFilePrint.Enabled = this.tsPrint.Enabled = (this.grdRates.Rows.Count > 0);
                this.msFilePreview.Enabled = (this.grdRates.Rows.Count > 0);
                this.msFileExit.Enabled = true;

                string classCode = this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text;
                this.msViewRefresh.Enabled = this.tsRefresh.Enabled = this.txtOrigin.Text.Trim().Length > 0 && classCode.Length > 0 && this.txtDiscount.Text.Trim().Length > 0 && this.txtFloorMin.Text.Trim().Length > 0 && this.lstZips.Items.Count > 0;

                this.msViewFont.Enabled = true;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
                this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(FinanceGateway.ServiceState,FinanceGateway.ServiceAddress));
                //this.ssMain.User2Panel.Icon = App.Config.ReadOnly ? new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("Argix.Resources.readonly.ico")) : null;
                //this.ssMain.User2Panel.ToolTipText = App.Config.ReadOnly ? "Read only mode; notify IT if you require update permissions." : "";
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
        private void exportToExcel(string fileName) {
            //Write rates to Excel
            Excel.Application app = new Excel.ApplicationClass();
            Excel.Workbook workbook = app.Workbooks.Add(Type.Missing);
            Excel.Worksheet worksheet = (Excel.Worksheet)workbook.Worksheets.Add(Type.Missing,Type.Missing,Type.Missing,Type.Missing);

            //Title
            Excel.Range range = worksheet.get_Range("A1",Type.Missing);
            range.Value2 = "Argix Logistics Quotations";
            range.Font.Bold = true; range.Font.Size = 14;

            //Sub-title
            DataModule tariff = (DataModule)this.mTariffs.Current;
            range = worksheet.get_Range("A2",Type.Missing);
            range.Font.Bold = false; range.Font.Size = 10;
            range.Value2 = "Tariffs Used: " + tariff.effectiveDateField;

            //Fixed Fields
            range = worksheet.get_Range("A4",Type.Missing);
            range = range.get_Resize(3,2);
            range.Font.Bold = true; range.Font.Size = 10;
            range.NumberFormat = "@";
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            object[,] fields = new object[3,2];
            fields[0,0] = "Origin Zip"; fields[0,1] = this.txtOrigin.Text.Trim();
            fields[1,0] = "Class Code"; fields[1,1] = (this.cboClassCode.SelectedValue != null ? this.cboClassCode.SelectedValue.ToString() : this.cboClassCode.Text);
            fields[2,0] = "Floor Charge"; fields[2,1] = Convert.ToInt32(this.txtFloorMin.Text).ToString("C");
            range.Value2 = fields;

            //Table Header
            object[] headers = { "Dest. Zip Code","Min Charge","Rate 0-499","  500-999","1,000-1,999","2,000-2,999","5,000-9,999","10,000-19,999","20,000+" };
            range = worksheet.get_Range("A9","I9");
            range.Font.Bold = true; range.Font.Size = 10;
            range.ColumnWidth = 15;
            range.Value2 = headers;

            //Create an array from the dataset
            object[,] data = new object[this.mRates.Count,9];
            for(int r=0;r< this.mRates.Count;r++) {
                Rate rate = (Rate)this.mRates[r];
                data[r,0] = rate.DestZip;
                data[r,1] = rate.MinCharge.ToString();
                data[r,2] = rate.Rate1.ToString();
                data[r,3] = rate.Rate501.ToString();
                data[r,4] = rate.Rate1001.ToString();
                data[r,5] = rate.Rate2001.ToString();
                data[r,6] = rate.Rate5001.ToString();
                data[r,7] = rate.Rate10001.ToString();
                data[r,8] = rate.Rate20001.ToString();
            }
            range = worksheet.get_Range("A10","I10");
            range = range.get_Resize(this.mRates.Count,9);
            range.Font.Size = 10; 
            range.NumberFormat = "@";
            range.Value2 = data;

            range = worksheet.get_Range("B10","I10");
            range = range.get_Resize(this.mRates.Count,8);
            range.HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            //Save the workbook and quit excel
            workbook.SaveAs(fileName,Excel.XlFileFormat.xlWorkbookNormal,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Excel.XlSaveAsAccessMode.xlNoChange,Type.Missing,Type.Missing,Type.Missing,Type.Missing,Type.Missing);
            workbook.Close(false,fileName, Type.Missing);
            app.Workbooks.Close();
            app.Quit();
        }
        #endregion
    }
}
