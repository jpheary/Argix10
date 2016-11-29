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

namespace Argix.Finance {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members				
        private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
        #region Controls
        private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        //		private System.Windows.Forms.MenuItem msHelpContents;
        private Argix.Windows.ArgixStatusBar ssMain;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSave;
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
        private ComboBox cboClient;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private InvoicingDataset mClients;
        private InvoicingDataset mInvoices;
		private System.ComponentModel.IContainer components;
		#endregion
		
		public frmMain() {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Logistics " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.grdMain.Controls.AddRange(new Control[] { this.cboClient });
                this.Controls.AddRange(new Control[] { this.grdMain,this.tsMain,this.msMain,this.ssMain });
                this.cboClient.Top = 0;
                this.cboClient.Left = 88;
                #endregion
				
				//Create data and UI services
				this.mGridSvc = new UltraGridSvc(this.grdMain);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 1000, 3000);
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ClientInvoiceTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostToARDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Amount");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReleaseDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceTypeCode");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceTypeDescription");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("InvoiceTypeTarget");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BillTo");
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.mClients = new Argix.InvoicingDataset();
            this.mInvoices = new Argix.InvoicingDataset();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
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
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.grdMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mInvoices)).BeginInit();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.CausesValidation = false;
            this.grdMain.Controls.Add(this.cboClient);
            this.grdMain.DataMember = "ClientInvoiceTable";
            this.grdMain.DataSource = this.mInvoices;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Invoice#";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 91;
            ultraGridColumn2.Format = "MM/dd/yyyy";
            ultraGridColumn2.Header.Caption = "Invoice Date";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 96;
            ultraGridColumn3.Format = "MM/dd/yyyy";
            ultraGridColumn3.Header.Caption = "Post-AR Date";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 96;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn4.CellAppearance = appearance2;
            ultraGridColumn4.Header.Caption = "Ctns";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 40;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn5.CellAppearance = appearance3;
            ultraGridColumn5.Header.Caption = "Plts";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 40;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn6.CellAppearance = appearance4;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 72;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn7.CellAppearance = appearance5;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 72;
            ultraGridColumn8.Format = "MM/dd/yyyy";
            ultraGridColumn8.Header.Caption = "Release Date";
            ultraGridColumn8.Header.VisiblePosition = 8;
            ultraGridColumn8.Width = 96;
            appearance6.TextHAlignAsString = "Center";
            ultraGridColumn9.CellAppearance = appearance6;
            ultraGridColumn9.Header.Caption = "Type";
            ultraGridColumn9.Header.VisiblePosition = 10;
            ultraGridColumn9.Width = 39;
            ultraGridColumn10.Header.Caption = "Type Desc";
            ultraGridColumn10.Header.VisiblePosition = 7;
            ultraGridColumn10.Width = 96;
            ultraGridColumn11.Header.Caption = "Target";
            ultraGridColumn11.Header.VisiblePosition = 11;
            ultraGridColumn11.Width = 480;
            ultraGridColumn12.Header.Caption = "Bill To";
            ultraGridColumn12.Header.VisiblePosition = 9;
            ultraGridColumn12.Width = 96;
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
            ultraGridColumn12});
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance7.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance7.FontData.BoldAsString = "True";
            appearance7.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance7.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.CaptionAppearance = appearance7;
            this.grdMain.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMain.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance8.BackColor = System.Drawing.SystemColors.Control;
            appearance8.FontData.BoldAsString = "True";
            appearance8.TextHAlignAsString = "Left";
            appearance8.TextVAlignAsString = "Top";
            this.grdMain.DisplayLayout.Override.HeaderAppearance = appearance8;
            this.grdMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMain.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance9.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMain.DisplayLayout.Override.RowAppearance = appearance9;
            this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMain.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMain.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 80);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(664, 225);
            this.grdMain.TabIndex = 1;
            this.grdMain.Text = "Invoices for ";
            this.grdMain.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnInvoicesInitializeLayout);
            this.grdMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnInvoicesInitializeRow);
            this.grdMain.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnInvoiceAfterSelectChange);
            this.grdMain.DoubleClickRow += new Infragistics.Win.UltraWinGrid.DoubleClickRowEventHandler(this.OnInvoiceDoubleClicked);
            // 
            // cboClient
            // 
            this.cboClient.DataSource = this.mClients;
            this.cboClient.DisplayMember = "ClientTable.ClientName";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(88, 0);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(192, 21);
            this.cboClient.TabIndex = 15;
            this.cboClient.ValueMember = "ClientTable.ClientNumber";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
            // 
            // mClients
            // 
            this.mClients.DataSetName = "InvoicingDataset";
            this.mClients.Locale = new System.Globalization.CultureInfo("");
            this.mClients.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mInvoices
            // 
            this.mInvoices.DataSetName = "InvoicingDataset";
            this.mInvoices.Locale = new System.Globalization.CultureInfo("");
            this.mInvoices.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 305);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(664, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 11;
            this.ssMain.TerminalText = "Local Terminal";
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
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsSep3,
            this.tsSearch,
            this.tsSep4,
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(664, 56);
            this.tsMain.TabIndex = 13;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.Document;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 53);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New";
            this.tsNew.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(40, 53);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open the selected invoice";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 56);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::Argix.Properties.Resources.Save;
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 53);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save";
            this.tsSave.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 53);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print the list of invoices";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 56);
            // 
            // tsCut
            // 
            this.tsCut.Image = global::Argix.Properties.Resources.Cut;
            this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCut.Name = "tsCut";
            this.tsCut.Size = new System.Drawing.Size(36, 53);
            this.tsCut.Text = "Cut";
            this.tsCut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCut.ToolTipText = "Cut";
            this.tsCut.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsCopy
            // 
            this.tsCopy.Image = global::Argix.Properties.Resources.Copy;
            this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.Size = new System.Drawing.Size(39, 53);
            this.tsCopy.Text = "Copy";
            this.tsCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCopy.ToolTipText = "Copy";
            this.tsCopy.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsPaste
            // 
            this.tsPaste.Image = global::Argix.Properties.Resources.Paste;
            this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(39, 53);
            this.tsPaste.Text = "Paste";
            this.tsPaste.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPaste.ToolTipText = "Paste";
            this.tsPaste.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 56);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Search;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(36, 53);
            this.tsSearch.Text = "Find";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.ToolTipText = "Search";
            this.tsSearch.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 56);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 53);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh the list of invoices";
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
            this.msMain.Size = new System.Drawing.Size(664, 24);
            this.msMain.TabIndex = 14;
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
            this.msHelpAbout.Size = new System.Drawing.Size(168, 22);
            this.msHelpAbout.Text = "&About Invoicing...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClicked);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(165, 6);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(664, 329);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Invoicing";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.Resize += new System.EventHandler(this.OnFormResize);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.grdMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mInvoices)).EndInit();
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
                    this.msViewToolbar.Checked = Convert.ToBoolean(global::Argix.Properties.Settings.Default.Toolbar);
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
                this.mToolTip.SetToolTip(this.cboClient,"Select a client for a list of invoices.");
                #endregion
				
				//Set control defaults
				#region Grid Overrides
				this.grdMain.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdMain.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                this.grdMain.DisplayLayout.Bands[0].Columns["InvoiceDate"].SortIndicator = SortIndicator.Descending;
                #endregion

                ServiceInfo t = FinanceGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";

                this.mClients.Merge(FinanceGateway.GetClients());
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(null,EventArgs.Empty);
			}
			catch(Exception ex) { App.ReportError(ex); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { 
			//Event handler for form resized event
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
        private void OnClientChanged(object sender,EventArgs e) {
            //Event handler for change in client
            this.msViewRefresh.PerformClick();
        }
        #region Invoice Grid Services: OnInvoicesInitializeLayout(), OnInvoiceAfterSelectChange(), OnGridAfterSelectChange(), OnInvoiceDoubleClicked()
        private void OnInvoicesInitializeLayout(object sender,InitializeLayoutEventArgs e) { }
        private void OnInvoicesInitializeRow(object sender,InitializeRowEventArgs e) { }
        private void OnInvoiceAfterSelectChange(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for invoice row selected
            setUserServices();
        }
        private void OnInvoiceDoubleClicked(object sender,DoubleClickRowEventArgs e) {
            //Event handler for invoice row double-clicked
            if(this.msFileOpen.Enabled) this.msFileOpen.PerformClick();
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
                        string target = this.grdMain.Selected.Rows[0].Cells["InvoiceTypeTarget"].Text;
                        this.mMessageMgr.AddMessage("Opening invoice to " + target);
                        if(target.EndsWith("xlt") || target.EndsWith("xltx")) {
                            string args = " /e ";
                            args += target;
                            args += "?clid=" + this.cboClient.SelectedValue;
                            args += "&invoice=" + this.grdMain.Selected.Rows[0].Cells["InvoiceNumber"].Text;
                            System.Diagnostics.Process.Start("Excel.exe",args);
                        }
                        else
                            System.Diagnostics.Process.Start(target);
                        break;
                    case "msFileSave":     
                    case "tsSave":
                        break;
                    case "msFileSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Xml Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save " + App.Product + " As...";
                        dlgSave.FileName = "";
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this)==DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Saving " + this.cboClient.Text.Trim() + " invoice list to " + dlgSave.FileName);
                            FileStream fs = new FileStream(dlgSave.FileName,FileMode.Create,FileAccess.Write);
                            this.mInvoices.WriteXml(fs);
                            fs.Flush();
                            fs.Close();
                        }
                        break;
                    case "msFileSettings": UltraGridPrinter.PageSettings(); break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdMain,"Invoice list for " + this.cboClient.Text.Trim()); break;
                    case "msFilePrint": UltraGridPrinter.Print(this.grdMain,"Invoice list for " + this.cboClient.Text.Trim(),true); break;
                    case "tsPrint": UltraGridPrinter.Print(this.grdMain,"Invoice list for " + this.cboClient.Text.Trim(),false); break;
                    case "msFileExit": this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "tsCut": 
                        break;
                    case "msEditCopy":
                    case "tsCopy":
                        break;
                    case "msEditPaste":
                    case "tsPaste":
                        break;
                    case "msEditSearch":
                    case "tsSearch": 
                        break;
                    case "msViewRefresh":
                    case "tsRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        this.mGridSvc.CaptureState();
                        this.mMessageMgr.AddMessage("Refreshing invoice list for " + this.cboClient.Text.Trim());
                        this.mInvoices.Clear();
                        this.mInvoices.Merge(FinanceGateway.GetClientInvoices(this.cboClient.SelectedValue.ToString(),null,null));
                        this.mGridSvc.RestoreState();
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
                    case "msToolsTrace":
                        break;
                    case "msToolsConfig": App.ShowConfig(); break;
                    case "msHelpAbout":
                        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this);
                        break;
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
        #region Local Services: setUserServices(), buildHelpMenu()
		private void setUserServices() {
			//Set user services
			try {
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
                this.msFileOpen.Enabled = this.tsOpen.Enabled = this.grdMain.Selected.Rows.Count > 0 && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
				this.msFileSave.Enabled = this.tsSave.Enabled = false;
                this.msFileSaveAs.Enabled = this.grdMain.Rows.Count > 0;
				this.msFileSettings.Enabled = true;
                this.msFilePreview.Enabled = this.msFilePrint.Enabled = this.tsPrint.Enabled = this.grdMain.Rows.Count > 0;
				this.msFileExit.Enabled = true;
				this.msEditCut.Enabled = this.tsCut.Enabled = this.msEditCopy.Enabled = this.tsCopy.Enabled = this.msEditPaste.Enabled = this.tsPaste.Enabled = false;
				this.msEditSearch.Enabled = this.tsSearch.Enabled = false;
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
                this.msViewFont.Enabled = true;
                this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(FinanceGateway.ServiceState,FinanceGateway.ServiceAddress));
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