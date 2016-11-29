using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Threading;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;

namespace Argix.AgentLineHaul {
	//
	public class frmMain : System.Windows.Forms.Form {
		//Members
		private PageSettings mPageSettings = null;
		private UltraGridSvc mGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private MessageManager mMessageMgr=null;
		private NameValueCollection mHelpItems=null;
		
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdTemplates;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddSortCenter;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddCarrier;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddZone;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddS2Zone;
		private Infragistics.Win.UltraWinGrid.UltraDropDown uddDay;
        private Infragistics.Win.UltraWinGrid.UltraGridRow grdRow;
        private Argix.Windows.ArgixStatusBar ssMain;
        private ToolStrip tsMain;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen;
        private ToolStripButton tsSave;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsPrint;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsCut;
        private ToolStripButton tsCopy;
        private ToolStripButton tsPaste;
        private ToolStripButton tsSearch;
        private ToolStripSeparator tsSep3;
        private ToolStripButton tsRefresh;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripSeparator msFileSep2;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileSetup;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePreview;
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
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ToolStripMenuItem csRefresh;
        private ToolStripSeparator csSep1;
        private ToolStripMenuItem csCut;
        private ToolStripMenuItem csCopy;
        private ToolStripMenuItem csPaste;
        private ToolStripMenuItem msFileSave;
        private ToolStripSeparator tsSep4;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep2;
        private TemplateDataset mSortCenters;
        private TemplateDataset mS2Zones;
        private TemplateDataset mTemplates;
        private TemplateDataset mCarriers;
        private TemplateDataset mZones;
        private ContextMenuStrip csMain;
		private System.ComponentModel.IContainer components;
		#endregion
		//Interface
		public frmMain() {
			try {
				InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
				buildHelpMenu();
				Splash.Start(App.Product, Assembly.GetExecutingAssembly(), App.Copyright);
				Thread.Sleep(3000);
				#region Window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tsMain,this.ssMain,this.msMain });
				#endregion
				
				//Create data and UI services
				this.mPageSettings = new PageSettings();
				this.mPageSettings.Landscape = true;
				this.mGridSvc = new UltraGridSvc(this.grdTemplates);
				this.mToolTip = new System.Windows.Forms.ToolTip();
				this.mMessageMgr = new MessageManager(this.ssMain.Panels[0], 500, 3000);
				
				//Only Line Haul Administrators can run the application
                if (!RoleServiceGateway.IsLineHaulTemplator) throw new ApplicationException("You are not authorized to run the application. Application will shut down.");
				#region Init UltraGrid ValueLists
				Infragistics.Win.ValueListsCollection lists = this.grdTemplates.DisplayLayout.ValueLists;
				Infragistics.Win.ValueList vl=null;
				vl = lists.Add("schCloseDaysOffset"); for(int i=0; i<3; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				vl = lists.Add("schDepartDaysOffset"); for(int i=0; i<4; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				vl = lists.Add("schArrivalDaysOffset"); for(int i=0; i<15; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				vl = lists.Add("schOFD1DaysOffset"); for (int i=0; i<17; i++) { vl.ValueListItems.Add(i, i.ToString()); }
				#endregion
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TerminalTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CarrierTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("AgentTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("AgentTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Number");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Band 0", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn110 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn111 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleTemplateTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID", -1, "uddSortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayOfTheWeek", -1, "uddDay");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID", -1, "uddCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledCloseDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledCloseTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDepartureDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDepartureTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone", -1, "uddZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrivalDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrivalTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1Offset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone", -1, "uddS2Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrivalDateOffset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrivalTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1Offset");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2User");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stop2RowVersion");
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.uddSortCenter = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mSortCenters = new Argix.AgentLineHaul.TemplateDataset();
            this.uddCarrier = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mCarriers = new Argix.AgentLineHaul.TemplateDataset();
            this.uddZone = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mZones = new Argix.AgentLineHaul.TemplateDataset();
            this.uddS2Zone = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.mS2Zones = new Argix.AgentLineHaul.TemplateDataset();
            this.uddDay = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.grdTemplates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mTemplates = new Argix.AgentLineHaul.TemplateDataset();
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsSave = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsCut = new System.Windows.Forms.ToolStripButton();
            this.tsCopy = new System.Windows.Forms.ToolStripButton();
            this.tsPaste = new System.Windows.Forms.ToolStripButton();
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
            this.msFileSetup = new System.Windows.Forms.ToolStripMenuItem();
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
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.uddSortCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortCenters)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddZone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddS2Zone)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mS2Zones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddDay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplates)).BeginInit();
            this.tsMain.SuspendLayout();
            this.msMain.SuspendLayout();
            this.csMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // uddSortCenter
            // 
            this.uddSortCenter.DataMember = "TerminalTable";
            this.uddSortCenter.DataSource = this.mSortCenters;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.uddSortCenter.DisplayLayout.Appearance = appearance1;
            ultraGridColumn53.Header.VisiblePosition = 1;
            ultraGridColumn53.Hidden = true;
            ultraGridColumn52.Header.Caption = "Name";
            ultraGridColumn52.Header.VisiblePosition = 0;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn53,
            ultraGridColumn52});
            this.uddSortCenter.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.uddSortCenter.DisplayLayout.CaptionAppearance = appearance2;
            this.uddSortCenter.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddSortCenter.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddSortCenter.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddSortCenter.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.uddSortCenter.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.uddSortCenter.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddSortCenter.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddSortCenter.DisplayLayout.Override.RowAppearance = appearance4;
            this.uddSortCenter.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddSortCenter.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddSortCenter.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddSortCenter.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddSortCenter.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddSortCenter.DisplayMember = "Description";
            this.uddSortCenter.Location = new System.Drawing.Point(12, 206);
            this.uddSortCenter.Name = "uddSortCenter";
            this.uddSortCenter.Size = new System.Drawing.Size(102, 32);
            this.uddSortCenter.TabIndex = 9;
            this.uddSortCenter.ValueMember = "ID";
            this.uddSortCenter.Visible = false;
            this.uddSortCenter.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.OnSortCenterBeforeDropDown);
            // 
            // mSortCenters
            // 
            this.mSortCenters.DataSetName = "TemplateDataset";
            this.mSortCenters.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddCarrier
            // 
            this.uddCarrier.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddCarrier.DataMember = "CarrierTable";
            this.uddCarrier.DataSource = this.mCarriers;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.Appearance = appearance5;
            ultraGridColumn55.Header.VisiblePosition = 1;
            ultraGridColumn55.Hidden = true;
            ultraGridColumn54.Header.Caption = "Name";
            ultraGridColumn54.Header.VisiblePosition = 0;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn55,
            ultraGridColumn54});
            this.uddCarrier.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.CaptionAppearance = appearance6;
            this.uddCarrier.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddCarrier.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddCarrier.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddCarrier.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.uddCarrier.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddCarrier.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddCarrier.DisplayLayout.Override.RowAppearance = appearance8;
            this.uddCarrier.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddCarrier.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddCarrier.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddCarrier.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddCarrier.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddCarrier.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddCarrier.DisplayMember = "Description";
            this.uddCarrier.Location = new System.Drawing.Point(120, 206);
            this.uddCarrier.Name = "uddCarrier";
            this.uddCarrier.Size = new System.Drawing.Size(112, 32);
            this.uddCarrier.TabIndex = 10;
            this.uddCarrier.ValueMember = "ID";
            this.uddCarrier.Visible = false;
            this.uddCarrier.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.OnCarrierBeforeDropDown);
            // 
            // mCarriers
            // 
            this.mCarriers.DataSetName = "TemplateDataset";
            this.mCarriers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddZone
            // 
            this.uddZone.DataMember = "AgentTable";
            this.uddZone.DataSource = this.mZones;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.uddZone.DisplayLayout.Appearance = appearance9;
            ultraGridColumn57.Header.VisiblePosition = 1;
            ultraGridColumn57.Hidden = true;
            ultraGridColumn56.Header.Caption = "Zone";
            ultraGridColumn56.Header.VisiblePosition = 0;
            ultraGridColumn58.Header.VisiblePosition = 2;
            ultraGridColumn58.Hidden = true;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn57,
            ultraGridColumn56,
            ultraGridColumn58});
            this.uddZone.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            appearance10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.uddZone.DisplayLayout.CaptionAppearance = appearance10;
            this.uddZone.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddZone.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddZone.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddZone.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.FontData.BoldAsString = "True";
            appearance11.TextHAlignAsString = "Left";
            this.uddZone.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.uddZone.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddZone.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance12.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddZone.DisplayLayout.Override.RowAppearance = appearance12;
            this.uddZone.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddZone.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddZone.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddZone.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddZone.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddZone.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddZone.DisplayMember = "Description";
            this.uddZone.Location = new System.Drawing.Point(234, 206);
            this.uddZone.Name = "uddZone";
            this.uddZone.Size = new System.Drawing.Size(104, 32);
            this.uddZone.TabIndex = 11;
            this.uddZone.ValueMember = "Description";
            this.uddZone.Visible = false;
            this.uddZone.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnZoneSelected);
            // 
            // mZones
            // 
            this.mZones.DataSetName = "TemplateDataset";
            this.mZones.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddS2Zone
            // 
            this.uddS2Zone.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddS2Zone.DataMember = "AgentTable";
            this.uddS2Zone.DataSource = this.mS2Zones;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.uddS2Zone.DisplayLayout.Appearance = appearance13;
            ultraGridColumn60.Header.VisiblePosition = 1;
            ultraGridColumn60.Hidden = true;
            ultraGridColumn59.Header.Caption = "Zone";
            ultraGridColumn59.Header.VisiblePosition = 0;
            ultraGridColumn61.Header.VisiblePosition = 2;
            ultraGridColumn61.Hidden = true;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn60,
            ultraGridColumn59,
            ultraGridColumn61});
            this.uddS2Zone.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.uddS2Zone.DisplayLayout.CaptionAppearance = appearance14;
            this.uddS2Zone.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.uddS2Zone.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.uddS2Zone.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.uddS2Zone.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.TextHAlignAsString = "Left";
            this.uddS2Zone.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.uddS2Zone.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.uddS2Zone.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.uddS2Zone.DisplayLayout.Override.RowAppearance = appearance16;
            this.uddS2Zone.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.uddS2Zone.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.uddS2Zone.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddS2Zone.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddS2Zone.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.uddS2Zone.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.uddS2Zone.DisplayMember = "Description";
            this.uddS2Zone.Location = new System.Drawing.Point(348, 206);
            this.uddS2Zone.Name = "uddS2Zone";
            this.uddS2Zone.Size = new System.Drawing.Size(104, 32);
            this.uddS2Zone.TabIndex = 12;
            this.uddS2Zone.ValueMember = "Description";
            this.uddS2Zone.Visible = false;
            this.uddS2Zone.RowSelected += new Infragistics.Win.UltraWinGrid.RowSelectedEventHandler(this.OnS2ZoneSelected);
            // 
            // mS2Zones
            // 
            this.mS2Zones.DataSetName = "TemplateDataset";
            this.mS2Zones.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // uddDay
            // 
            this.uddDay.Cursor = System.Windows.Forms.Cursors.Default;
            ultraGridColumn110.Header.VisiblePosition = 0;
            ultraGridColumn110.Hidden = true;
            ultraGridColumn111.Header.Caption = "Day";
            ultraGridColumn111.Header.VisiblePosition = 1;
            ultraGridColumn111.Width = 48;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn110,
            ultraGridColumn111});
            this.uddDay.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.uddDay.Location = new System.Drawing.Point(494, 206);
            this.uddDay.Name = "uddDay";
            this.uddDay.Size = new System.Drawing.Size(107, 32);
            this.uddDay.TabIndex = 13;
            this.uddDay.Visible = false;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 258);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(929, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 15;
            this.ssMain.TerminalText = "Terminal";
            // 
            // grdTemplates
            // 
            this.grdTemplates.ContextMenuStrip = this.csMain;
            this.grdTemplates.DataMember = "ShipScheduleTemplateTable";
            this.grdTemplates.DataSource = this.mTemplates;
            appearance17.BackColor = System.Drawing.SystemColors.Window;
            appearance17.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance17.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Appearance = appearance17;
            ultraGridBand6.ColHeaderLines = 3;
            ultraGridColumn3.Header.VisiblePosition = 0;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.Caption = "\r\n\r\nTerminal";
            ultraGridColumn4.Header.VisiblePosition = 1;
            ultraGridColumn4.Width = 150;
            ultraGridColumn5.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn5.Header.VisiblePosition = 2;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 150;
            ultraGridColumn6.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn6.Header.Caption = "\r\n\r\nDay";
            ultraGridColumn6.Header.VisiblePosition = 3;
            ultraGridColumn7.Header.Caption = "\r\n\r\nCarrier";
            ultraGridColumn7.Header.VisiblePosition = 8;
            ultraGridColumn7.Width = 150;
            ultraGridColumn8.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn8.Header.VisiblePosition = 9;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn8.Width = 150;
            ultraGridColumn9.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn9.Header.Caption = "\r\nClose\r\nDay";
            ultraGridColumn9.Header.VisiblePosition = 10;
            ultraGridColumn9.Width = 50;
            ultraGridColumn10.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn10.Format = "HH:mm";
            ultraGridColumn10.Header.Caption = "\r\nClose\r\nTime";
            ultraGridColumn10.Header.VisiblePosition = 11;
            ultraGridColumn10.MaskInput = "{LOC}hh:mm";
            ultraGridColumn10.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn10.Width = 50;
            ultraGridColumn11.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn11.Header.Caption = "\r\nDepart\r\nDay";
            ultraGridColumn11.Header.VisiblePosition = 12;
            ultraGridColumn11.Width = 50;
            ultraGridColumn12.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn12.Format = "HH:mm";
            ultraGridColumn12.Header.Caption = "\r\nDepart\r\nTime";
            ultraGridColumn12.Header.VisiblePosition = 13;
            ultraGridColumn12.MaskInput = "{LOC}hh:mm";
            ultraGridColumn12.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn12.Width = 50;
            ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn13.Header.Caption = "\r\n\r\nMand?";
            ultraGridColumn13.Header.VisiblePosition = 18;
            ultraGridColumn13.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn13.Width = 50;
            ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn14.Header.Caption = "\r\n\r\nActive?";
            ultraGridColumn14.Header.VisiblePosition = 19;
            ultraGridColumn14.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn14.Width = 50;
            ultraGridColumn15.Header.VisiblePosition = 32;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 33;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.VisiblePosition = 34;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 20;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.VisiblePosition = 21;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn20.Header.Caption = "\r\n\r\nZone";
            ultraGridColumn20.Header.VisiblePosition = 4;
            ultraGridColumn20.Width = 50;
            ultraGridColumn21.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn21.Header.Caption = "\r\n\r\nTag";
            ultraGridColumn21.Header.VisiblePosition = 5;
            ultraGridColumn21.Width = 50;
            ultraGridColumn22.Header.VisiblePosition = 6;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn23.Header.Caption = "\r\n\r\nAgent#";
            ultraGridColumn23.Header.VisiblePosition = 7;
            ultraGridColumn23.Width = 75;
            ultraGridColumn24.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn24.Header.Caption = "\r\nArrival\r\nDay";
            ultraGridColumn24.Header.VisiblePosition = 14;
            ultraGridColumn25.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn25.Format = "HH:mm";
            ultraGridColumn25.Header.Caption = "\r\nArrival\r\nTime";
            ultraGridColumn25.Header.VisiblePosition = 15;
            ultraGridColumn25.MaskInput = "{LOC}hh:mm";
            ultraGridColumn25.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn25.Width = 50;
            ultraGridColumn26.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn26.Header.Caption = "\r\nOFD\r\nDay";
            ultraGridColumn26.Header.VisiblePosition = 16;
            ultraGridColumn27.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn27.Header.Caption = "\r\n\r\nNotes";
            ultraGridColumn27.Header.VisiblePosition = 17;
            ultraGridColumn28.Header.VisiblePosition = 35;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn29.Header.VisiblePosition = 36;
            ultraGridColumn29.Hidden = true;
            ultraGridColumn30.Header.VisiblePosition = 37;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 22;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.Header.VisiblePosition = 23;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn33.Header.Caption = "\r\nS2\r\nZone";
            ultraGridColumn33.Header.VisiblePosition = 24;
            ultraGridColumn33.Width = 50;
            ultraGridColumn34.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn34.Header.Caption = "\r\nS2\r\nTag";
            ultraGridColumn34.Header.VisiblePosition = 25;
            ultraGridColumn34.Width = 50;
            ultraGridColumn35.Header.VisiblePosition = 26;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn36.Header.Caption = "\r\nS2\r\nAgent#";
            ultraGridColumn36.Header.VisiblePosition = 27;
            ultraGridColumn36.Width = 75;
            ultraGridColumn37.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn37.Header.Caption = "S2\r\nArrival\r\nDay";
            ultraGridColumn37.Header.VisiblePosition = 28;
            ultraGridColumn38.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn38.Format = "HH:mm";
            ultraGridColumn38.Header.Caption = "S2\r\nArrival\r\nTime";
            ultraGridColumn38.Header.VisiblePosition = 29;
            ultraGridColumn38.MaskInput = "{LOC}hh:mm";
            ultraGridColumn38.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn38.Width = 50;
            ultraGridColumn39.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn39.Header.Caption = "S2\r\nOFD\r\nDay";
            ultraGridColumn39.Header.VisiblePosition = 30;
            ultraGridColumn39.Width = 50;
            ultraGridColumn40.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn40.Header.Caption = "\r\nS2\r\nNotes";
            ultraGridColumn40.Header.VisiblePosition = 31;
            ultraGridColumn41.Header.VisiblePosition = 38;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.Header.VisiblePosition = 39;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn43.Header.VisiblePosition = 40;
            ultraGridColumn43.Hidden = true;
            ultraGridBand6.Columns.AddRange(new object[] {
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
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40,
            ultraGridColumn41,
            ultraGridColumn42,
            ultraGridColumn43});
            ultraGridBand6.GroupHeaderLines = 3;
            ultraGridBand6.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTemplates.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            appearance18.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance18.FontData.BoldAsString = "True";
            appearance18.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance18.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.CaptionAppearance = appearance18;
            this.grdTemplates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.TemplateOnBottom;
            this.grdTemplates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdTemplates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            appearance19.BackColor = System.Drawing.SystemColors.Control;
            appearance19.FontData.BoldAsString = "True";
            appearance19.TextHAlignAsString = "Left";
            this.grdTemplates.DisplayLayout.Override.HeaderAppearance = appearance19;
            this.grdTemplates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTemplates.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance20.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTemplates.DisplayLayout.Override.RowAppearance = appearance20;
            this.grdTemplates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdTemplates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTemplates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTemplates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTemplates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTemplates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTemplates.Location = new System.Drawing.Point(0, 76);
            this.grdTemplates.Name = "grdTemplates";
            this.grdTemplates.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdTemplates.Size = new System.Drawing.Size(929, 182);
            this.grdTemplates.TabIndex = 0;
            this.grdTemplates.Text = "Templates";
            this.grdTemplates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTemplates.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdTemplates.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdTemplates.InitializeTemplateAddRow += new Infragistics.Win.UltraWinGrid.InitializeTemplateAddRowEventHandler(this.OnGridInitializeTemplateAddRow);
            this.grdTemplates.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdTemplates.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdTemplates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdTemplates.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdTemplates.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdTemplates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mTemplates
            // 
            this.mTemplates.DataSetName = "TemplateDataset";
            this.mTemplates.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(113, 22);
            this.csRefresh.Text = "Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(110, 6);
            // 
            // csCut
            // 
            this.csCut.Image = global::Argix.Properties.Resources.Cut;
            this.csCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCut.Name = "csCut";
            this.csCut.Size = new System.Drawing.Size(113, 22);
            this.csCut.Text = "Cut";
            this.csCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCopy
            // 
            this.csCopy.Image = global::Argix.Properties.Resources.Copy;
            this.csCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCopy.Name = "csCopy";
            this.csCopy.Size = new System.Drawing.Size(113, 22);
            this.csCopy.Text = "Copy";
            this.csCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csPaste
            // 
            this.csPaste.Image = global::Argix.Properties.Resources.Paste;
            this.csPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csPaste.Name = "csPaste";
            this.csPaste.Size = new System.Drawing.Size(113, 22);
            this.csPaste.Text = "Paste";
            this.csPaste.Click += new System.EventHandler(this.OnItemClick);
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
            this.tsSep2,
            this.tsPrint,
            this.tsSep3,
            this.tsCut,
            this.tsCopy,
            this.tsPaste,
            this.tsSearch,
            this.tsSep4,
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(929, 52);
            this.tsMain.Stretch = true;
            this.tsMain.TabIndex = 116;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(36, 49);
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
            this.tsOpen.Size = new System.Drawing.Size(40, 49);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open...";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsSave
            // 
            this.tsSave.Image = ((System.Drawing.Image)(resources.GetObject("tsSave.Image")));
            this.tsSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(36, 49);
            this.tsSave.Text = "Save";
            this.tsSave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSave.ToolTipText = "Save";
            this.tsSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep2
            // 
            this.tsSep2.Name = "tsSep2";
            this.tsSep2.Size = new System.Drawing.Size(6, 52);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = ((System.Drawing.Image)(resources.GetObject("tsPrint.Image")));
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 49);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print ship schedule...";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 52);
            // 
            // tsCut
            // 
            this.tsCut.Image = ((System.Drawing.Image)(resources.GetObject("tsCut.Image")));
            this.tsCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCut.Name = "tsCut";
            this.tsCut.Size = new System.Drawing.Size(36, 49);
            this.tsCut.Text = "Cut";
            this.tsCut.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCut.ToolTipText = "Cut text";
            this.tsCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsCopy
            // 
            this.tsCopy.Image = ((System.Drawing.Image)(resources.GetObject("tsCopy.Image")));
            this.tsCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCopy.Name = "tsCopy";
            this.tsCopy.Size = new System.Drawing.Size(39, 49);
            this.tsCopy.Text = "Copy";
            this.tsCopy.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCopy.ToolTipText = "Copy text";
            this.tsCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsPaste
            // 
            this.tsPaste.Image = ((System.Drawing.Image)(resources.GetObject("tsPaste.Image")));
            this.tsPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPaste.Name = "tsPaste";
            this.tsPaste.Size = new System.Drawing.Size(39, 49);
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
            this.tsSearch.Size = new System.Drawing.Size(46, 49);
            this.tsSearch.Text = "Search";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.ToolTipText = "Find";
            this.tsSearch.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 52);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("tsRefresh.Image")));
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(50, 49);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh ship schedule";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
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
            this.msMain.Size = new System.Drawing.Size(929, 24);
            this.msMain.TabIndex = 118;
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
            this.msFileSetup.Image = ((System.Drawing.Image)(resources.GetObject("msFileSetup.Image")));
            this.msFileSetup.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSetup.Name = "msFileSetup";
            this.msFileSetup.Size = new System.Drawing.Size(152, 22);
            this.msFileSetup.Text = "Page Set&up...";
            this.msFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = ((System.Drawing.Image)(resources.GetObject("msFilePrint.Image")));
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(152, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = ((System.Drawing.Image)(resources.GetObject("msFilePreview.Image")));
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(152, 22);
            this.msFilePreview.Text = "Print P&review...";
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
            this.msEditCut.Text = "Cu&t";
            this.msEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditCopy
            // 
            this.msEditCopy.Image = ((System.Drawing.Image)(resources.GetObject("msEditCopy.Image")));
            this.msEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCopy.Name = "msEditCopy";
            this.msEditCopy.Size = new System.Drawing.Size(109, 22);
            this.msEditCopy.Text = "&Copy";
            this.msEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditPaste
            // 
            this.msEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.msEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditPaste.Name = "msEditPaste";
            this.msEditPaste.Size = new System.Drawing.Size(109, 22);
            this.msEditPaste.Text = "&Paste";
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
            this.msEditSearch.Text = "&Search";
            this.msEditSearch.Click += new System.EventHandler(this.OnItemClick);
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
            this.msView.Text = "View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = ((System.Drawing.Image)(resources.GetObject("msViewRefresh.Image")));
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
            this.msViewFont.Text = "&Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
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
            this.msViewToolbar.Text = "Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(126, 22);
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
            this.msHelp.Text = "Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(251, 22);
            this.msHelpAbout.Text = "&About Ship Schedule Templates...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(248, 6);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csRefresh,
            this.csSep1,
            this.csCut,
            this.csCopy,
            this.csPaste});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(114, 98);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(929, 282);
            this.Controls.Add(this.uddS2Zone);
            this.Controls.Add(this.uddZone);
            this.Controls.Add(this.uddDay);
            this.Controls.Add(this.uddCarrier);
            this.Controls.Add(this.uddSortCenter);
            this.Controls.Add(this.grdTemplates);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.Controls.Add(this.ssMain);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ship Schedule Templates";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.uddSortCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSortCenters)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddZone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddS2Zone)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mS2Zones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddDay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTemplates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTemplates)).EndInit();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.csMain.ResumeLayout(false);
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
				//this.mToolTip.SetToolTip(this.cboTerminals, "Select an enterprise terminal for the TL and Agent Summary views.");
				#endregion
				
				//Set control defaults
				#region Grid loading
				this.grdTemplates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTemplates.DisplayLayout.Bands[0].Columns["SortCenter"].SortIndicator = SortIndicator.Ascending;

                this.mSortCenters.Merge(ShipScheduleTemplatesGateway.GetShippersAndTerminals());
                this.uddSortCenter.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.uddCarrier.DataSource = ShipScheduleTemplatesGateway.GetCarriers();
                this.uddCarrier.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;

                this.uddZone.DataSource = ShipScheduleTemplatesGateway.GetAgents();
                this.uddZone.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;
                this.uddZone.DisplayLayout.Bands[0].Columns["Description"].Width = 50;

                this.uddS2Zone.DataSource = ShipScheduleTemplatesGateway.GetAgents();
                this.uddS2Zone.DisplayLayout.Bands[0].Columns["Description"].SortIndicator = Infragistics.Win.UltraWinGrid.SortIndicator.Ascending;
                this.uddS2Zone.DisplayLayout.Bands[0].Columns["Description"].Width = 50;

                this.uddDay.DataSource = ShipScheduleTemplatesGateway.GetDaysOfWeek();
                this.uddDay.DataMember = "SelectionListTable";
				this.uddDay.DisplayMember = "Description";
				this.uddDay.ValueMember = "ID";
				#endregion
                this.ColumnHeaders = global::Argix.Properties.Settings.Default.ColumnHeaders;
                ServiceInfo t = ShipScheduleTemplatesGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(), t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.msViewRefresh.PerformClick();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form closing event
            if(!e.Cancel) {
                global::Argix.Properties.Settings.Default.WindowState = this.WindowState;
                global::Argix.Properties.Settings.Default.Location = this.Location;
                global::Argix.Properties.Settings.Default.Size = this.Size;
                global::Argix.Properties.Settings.Default.Font = this.Font;
                global::Argix.Properties.Settings.Default.Toolbar = this.msViewToolbar.Checked;
                global::Argix.Properties.Settings.Default.StatusBar = this.msViewStatusBar.Checked;
                global::Argix.Properties.Settings.Default.LastVersion = App.Version;
                global::Argix.Properties.Settings.Default.ColumnHeaders = this.ColumnHeaders;
                global::Argix.Properties.Settings.Default.Save();
            }
		}
		private void OnSortCenterBeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e) {
			//
            this.uddSortCenter.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdTemplates.DisplayLayout.Bands[0].Columns["SortCenter"].Width;
		}
		private void OnCarrierBeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e) {
			//
            this.uddCarrier.DisplayLayout.Bands[0].Columns["Description"].Width = this.grdTemplates.DisplayLayout.Bands[0].Columns["Carrier"].Width;
        }
        private void OnZoneSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            if (e.Row != null) {
                this.grdTemplates.ActiveRow.Cells["AgentNumber"].Value = e.Row.Cells["Number"].Text;
                this.grdTemplates.ActiveRow.Cells["AgentTerminalID"].Value = e.Row.Cells["ID"].Text;
            }
        }
        private void OnS2ZoneSelected(object sender, Infragistics.Win.UltraWinGrid.RowSelectedEventArgs e) {
            //
            if (e.Row != null) {
                this.grdTemplates.ActiveRow.Cells["S2AgentNumber"].Value = e.Row.Cells["Number"].Text;
                this.grdTemplates.ActiveRow.Cells["S2AgentTerminalID"].Value = e.Row.Cells["ID"].Text;
            }
        }
        #region Grid Support: OnGridInitializeLayout(), OnGridInitializeTemplateAddRow(), OnGridInitializeRow(), GridSelectionChanged(), OnGridKeyUp(), GridMouseDown(), OnGridBeforeRowFilterDropDownPopulate(), OnGridBeforeRowUpdate(), OnGridAfterRowUpdate()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//
			try {
				Infragistics.Win.UltraWinGrid.UltraGridBand band = e.Layout.Bands[0];
				band.Columns["ScheduledCloseDateOffset"].ValueList = e.Layout.ValueLists["schCloseDaysOffset"];
				band.Columns["ScheduledDepartureDateOffset"].ValueList = e.Layout.ValueLists["schDepartDaysOffset"];
				band.Columns["ScheduledArrivalDateOffset"].ValueList = e.Layout.ValueLists["schArrivalDaysOffset"];
				band.Columns["ScheduledOFD1Offset"].ValueList = e.Layout.ValueLists["schOFD1DaysOffset"];
				band.Columns["S2ScheduledArrivalDateOffset"].ValueList = e.Layout.ValueLists["schArrivalDaysOffset"];
				band.Columns["S2ScheduledOFD1Offset"].ValueList = e.Layout.ValueLists["schOFD1DaysOffset"];

				//select the first row
				if(this.grdTemplates.Rows.Count > 0) this.grdTemplates.ActiveRow = this.grdTemplates.Rows[0];
			}
			catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
		}
        private void OnGridInitializeTemplateAddRow(object sender,InitializeTemplateAddRowEventArgs e) {
            //Event handler for initialization of the add row
            e.TemplateAddRow.Cells["SortCenterID"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["SortCenter"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["DayOfTheWeek"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["MainZone"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["Tag"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["AgentNumber"].Activation = Activation.NoEdit;
            e.TemplateAddRow.Cells["Carrier"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["CarrierServiceID"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledCloseDateOffset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledCloseTime"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledDepartureDateOffset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["ScheduledDepartureTime"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["IsMandatory"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["IsActive"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2MainZone"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2Tag"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2AgentNumber"].Activation = Activation.NoEdit;
            e.TemplateAddRow.Cells["S2ScheduledArrivalDateOffset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2ScheduledArrivalTime"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2ScheduledOFD1Offset"].Activation = Activation.AllowEdit;
            e.TemplateAddRow.Cells["S2Notes"].Activation = Activation.AllowEdit;

            e.TemplateAddRow.Cells["IsMandatory"].Value = false;
            e.TemplateAddRow.Cells["IsActive"].Value = true;
            e.TemplateAddRow.Cells["StopNumber"].Value = "01";
            e.TemplateAddRow.Cells["S2StopNumber"].Value = "02";
        }
        private void OnGridInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //
            try {
                e.Row.Cells["SortCenterID"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["DayOfTheWeek"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["MainZone"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["Tag"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["CarrierServiceID"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledCloseDateOffset"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledCloseTime"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledDepartureDateOffset"].Activation = Activation.AllowEdit;
                e.Row.Cells["ScheduledDepartureTime"].Activation = Activation.AllowEdit;
                e.Row.Cells["IsMandatory"].Activation = Activation.AllowEdit;
                e.Row.Cells["IsActive"].Activation = Activation.AllowEdit;
                e.Row.Cells["S2MainZone"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2Tag"].Activation = (e.Row.IsAddRow) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["S2ScheduledArrivalDateOffset"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2ScheduledArrivalTime"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2ScheduledOFD1Offset"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["S2Notes"].Activation = (e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) ? Activation.AllowEdit : Activation.NoEdit;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
        }
        private void OnGridSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for after selection changes
            setUserServices();
        }
        private void OnGridKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            UltraGrid grid = (UltraGrid)sender;
            if(e.KeyCode == System.Windows.Forms.Keys.Enter && grid.ActiveRow != null) {
                grid.ActiveRow.Update();
                e.Handled = true;
            }
            //else if(e.KeyCode == System.Windows.Forms.Keys.Delete) {
            //    this.csCDelete.PerformClick();
            //    e.Handled = true;
            //}
            else
                e.Handled = false;
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                UIElement oUIElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
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
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
			//Removes only (Blanks) and Non Blanks default filter
			e.ValueList.ValueListItems.Remove(3);
			e.ValueList.ValueListItems.Remove(2);
			e.ValueList.ValueListItems.Remove(1);
		}
        private void OnGridBeforeRowUpdate(object sender,Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
			try {
                //Cancel the event, but leave the row data (i.e. don't call row.CancelUpdate())
                e.Cancel = !validateRules(e);
            }
			catch(Exception) { e.Cancel = true; }
		}
		private void OnGridAfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            try {
                //There is no selected row when updating- at a cell level
                ShipScheduleTemplate template = new ShipScheduleTemplate();
                template.TemplateID = e.Row.Cells["TemplateID"].Value.ToString();
                template.SortCenterID = Convert.ToInt64(e.Row.Cells["SortCenterID"].Value);
                template.DayOfTheWeek = Convert.ToByte(e.Row.Cells["DayOfTheWeek"].Value);
                template.CarrierServiceID = Convert.ToInt64(e.Row.Cells["CarrierServiceID"].Value);
                template.ScheduledCloseDateOffset = Convert.ToByte(e.Row.Cells["ScheduledCloseDateOffset"].Value);
                template.ScheduledCloseTime = Convert.ToDateTime(e.Row.Cells["ScheduledCloseTime"].Value);
                template.ScheduledDepartureDateOffset = Convert.ToByte(e.Row.Cells["ScheduledDepartureDateOffset"].Value);
                template.ScheduledDepartureTime = Convert.ToDateTime(e.Row.Cells["ScheduledDepartureTime"].Value);
                template.IsMandatory = Convert.ToByte(e.Row.Cells["IsMandatory"].Value);
                template.IsActive = Convert.ToByte(e.Row.Cells["IsActive"].Value);
                template.TemplateLastUpdated = DateTime.Now;
                template.TemplateUser = Environment.UserName;
                template.TemplateRowVersion = e.Row.Cells["TemplateRowVersion"].Value.ToString();

                template.StopID = e.Row.Cells["StopID"].Value.ToString();
                template.StopNumber = e.Row.Cells["StopNumber"].Value.ToString();
                template.AgentTerminalID = Convert.ToInt64(e.Row.Cells["AgentTerminalID"].Value.ToString());
                template.MainZone = e.Row.Cells["MainZone"].Value.ToString().Trim();
                template.Tag = e.Row.Cells["Tag"].Value.ToString();
                template.Notes = e.Row.Cells["Notes"].Value.ToString();
                template.ScheduledArrivalDateOffset = Convert.ToByte(e.Row.Cells["ScheduledArrivalDateOffset"].Value.ToString());
                template.ScheduledArrivalTime = Convert.ToDateTime(e.Row.Cells["ScheduledArrivalTime"].Value.ToString());
                template.ScheduledOFD1Offset = Convert.ToByte(e.Row.Cells["ScheduledOFD1Offset"].Value.ToString());
                template.Stop1LastUpdated = DateTime.Now;
                template.Stop1User = Environment.UserName;
                template.Stop1RowVersion = e.Row.Cells["Stop1RowVersion"].Value.ToString();

                template.S2StopID = e.Row.Cells["S2StopID"].Value.ToString().Trim();
                template.S2StopNumber = e.Row.Cells["S2StopNumber"].Value.ToString().Trim();
                if(e.Row.Cells["S2AgentTerminalID"].Value.ToString().Length > 0) template.S2AgentTerminalID = Convert.ToInt64(e.Row.Cells["S2AgentTerminalID"].Value.ToString());
                template.S2MainZone = e.Row.Cells["S2MainZone"].Value.ToString().Trim();
                template.S2Tag = e.Row.Cells["S2Tag"].Value.ToString().Trim();
                template.S2Notes = e.Row.Cells["S2Notes"].Value.ToString().Trim();
                if(e.Row.Cells["S2ScheduledArrivalDateOffset"].Value.ToString().Length > 0) template.S2ScheduledArrivalDateOffset = Convert.ToByte(e.Row.Cells["S2ScheduledArrivalDateOffset"].Value.ToString());
                if(e.Row.Cells["S2ScheduledArrivalTime"].Value.ToString().Length > 0) template.S2ScheduledArrivalTime = Convert.ToDateTime(e.Row.Cells["S2ScheduledArrivalTime"].Value.ToString());
                if(e.Row.Cells["S2ScheduledOFD1Offset"].Value.ToString().Length > 0) template.S2ScheduledOFD1Offset = Convert.ToByte(e.Row.Cells["S2ScheduledOFD1Offset"].Value.ToString());
                template.Stop2LastUpdated = DateTime.Now;
                template.Stop2User = Environment.UserName;
                template.Stop2RowVersion = e.Row.Cells["Stop2RowVersion"].Value.ToString();
                if(template.TemplateID.Trim().Length == 0) {
                    //Add new
                    string templateID = ShipScheduleTemplatesGateway.AddTemplate(template);
                    if(templateID.Length == 0) 
                        App.ReportError(new ApplicationException("The new template was not added."),true);
                    else 
                        MessageBox.Show(this,"A new template #" + templateID + " has been added.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
                else {
                    //Update existing
                    if(!ShipScheduleTemplatesGateway.UpdateTemplate(template)) 
                        App.ReportError(new ApplicationException("The template was not updated."),true);
                    else 
                        MessageBox.Show(this,"Existing template #" + template.TemplateID + " has been updated.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.msViewRefresh.PerformClick(); }
        }
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "msFileNew":  case "tsNew":  break;
                    case "msFileOpen": case "tsOpen": break;
                    case "msFileSave": case "tsSave": break;
					case "msFileSaveAs":			
						SaveFileDialog dlgSave = new SaveFileDialog();
						dlgSave.AddExtension = true;
						dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
						dlgSave.FilterIndex = 0;
						dlgSave.Title = "Save Templates As...";
						dlgSave.FileName = "Ship Schedule Templates";
						dlgSave.OverwritePrompt = true;
						if(dlgSave.ShowDialog(this)==DialogResult.OK) {
							this.Cursor = Cursors.WaitCursor;
							this.mMessageMgr.AddMessage("Saving to " + dlgSave.FileName + "...");
							Application.DoEvents();
                            if(dlgSave.FileName.EndsWith("xls")) {
                                new Argix.ExcelFormat().Transform(this.mTemplates,"ShipScheduleTemplateTable",dlgSave.FileName);
                            }
                            else {
                                this.mTemplates.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                            }
                        }
						break;
					case "msFileSetup":    UltraGridPrinter.PageSettings(); break;
					case "msFilePrint":	UltraGridPrinter.Print(this.grdTemplates, "SHIP SCHEDULE TEMPLATES                          " +  DateTime.Today.ToString("dd-MMM-yyyy"), true); break;
                    case "tsPrint":        UltraGridPrinter.Print(this.grdTemplates,"SHIP SCHEDULE TEMPLATES                          " +  DateTime.Today.ToString("dd-MMM-yyyy"),false); break;
                    case "msFilePreview":  UltraGridPrinter.PrintPreview(this.grdTemplates,"SHIP SCHEDULE TEMPLATES                          " +  DateTime.Today.ToString("dd-MMM-yyyy")); break;
					case "msFileExit":		this.Close(); Application.Exit(); break;
                    case "msEditCut":
                    case "csCut":
                    case "tsCut": 
                        break;
					case "msEditCopy":
                    case "csCopy":
                    case "tsCopy": 
                        this.grdRow = this.grdTemplates.ActiveRow; 
                        break;
					case "msEditPaste":
			        case "csPaste": 
					case "tsPaste": 
                        //Infragistics.Win.UltraWinGrid.UltraGridRow newRow = this.grdTemplates.DisplayLayout.Bands[0].AddNew();
						foreach(Infragistics.Win.UltraWinGrid.UltraGridCell cell in this.grdTemplates.ActiveRow.Cells) {
							if( cell.Column.Key != "TemplateID" && 
								cell.Column.Key != "TemplateRowVersion" &&
								cell.Column.Key != "StopID" &&
								cell.Column.Key != "Stop1RowVersion" &&
								cell.Column.Key != "S2StopID" &&		//cell.Column.Key != "SortCenterID" &&
								cell.Column.Key != "Stop2RowVersion" ) {
								cell.Value = grdRow.Cells[cell.Column.Index].Value;
							}
						}
						grdRow = null;
						break;
					case "msEditSearch":   case "tsSearch": break;
					case "msViewRefresh":
                    case "csRefresh": 
                    case "tsRefresh":
                        this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Refreshing templates list...");
                        this.mTemplates.Clear();
                        this.mTemplates.Merge(ShipScheduleTemplatesGateway.GetTemplates());
                        //this.grdTemplates.DataBind();
                        this.mMessageMgr.AddMessage("Loading templates...");
						break;
                    case "msViewFont":
                        FontDialog fd = new FontDialog();
                        fd.FontMustExist = true;
                        fd.Font = this.Font;
                        if (fd.ShowDialog() == DialogResult.OK)
                            this.Font = this.msMain.Font = this.tsMain.Font = this.ssMain.Font = fd.Font;
                        break;
                    case "msViewToolbar": this.tsMain.Visible = (this.msViewToolbar.Checked = (!this.msViewToolbar.Checked)); break;
					case "msViewStatusBar":    this.ssMain.Visible = (this.msViewStatusBar.Checked = (!this.msViewStatusBar.Checked)); break;
                    case "msToolsConfig":      App.ShowConfig(); break;
					case "msHelpAbout":		new dlgAbout(App.Product + " Application", App.Version, App.Copyright, App.Configuration).ShowDialog(this); break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
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
        #region Local Services: setUserServices(), buildHelpMenu(), validateRules()
		private void setUserServices() {
			//Set user services
			try {				
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen.Enabled = false;
                this.msFileSave.Enabled = this.tsSave.Enabled = false;
				this.msFileSaveAs.Enabled = (this.grdTemplates.Rows.Count > 0);
				this.msFileSetup.Enabled = true;
				this.msFilePrint.Enabled = this.tsPrint.Enabled = (this.grdTemplates.Rows.Count > 0);
				this.msFilePreview.Enabled = (this.grdTemplates.Rows.Count > 0);
				this.msFileExit.Enabled = true;
				this.msEditCut.Enabled = this.csCut.Enabled = this.tsCut.Enabled = false;
				this.msEditCopy.Enabled = this.csCopy.Enabled = this.tsCopy.Enabled = ((this.grdTemplates.ActiveRow != null) && (!this.grdTemplates.ActiveRow.IsAddRow));
				this.msEditPaste.Enabled = this.csPaste.Enabled = this.tsPaste.Enabled = ((this.grdTemplates.ActiveRow != null) && (this.grdTemplates.ActiveRow.IsAddRow) && (this.grdRow != null));
				this.msEditSearch.Enabled = this.tsSearch.Enabled = false;
				this.msViewRefresh.Enabled = this.csRefresh.Enabled = this.tsRefresh.Enabled = true;
				this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
                this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;

                this.ssMain.OnOnlineStatusUpdate(null, new OnlineStatusArgs(ShipScheduleTemplatesGateway.ServiceState, ShipScheduleTemplatesGateway.ServiceAddress));
                this.ssMain.User1Panel.Width = 144;
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
                    item.Click += new System.EventHandler(this.OnHelpMenuClick);
                    item.Enabled = (sValue != "");
                    this.msHelp.DropDownItems.Add(item);
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private string ColumnHeaders {
            get {
                MemoryStream ms = new MemoryStream();
                this.grdTemplates.DisplayLayout.SaveAsXml(ms,PropertyCategories.SortedColumns);
                return Encoding.ASCII.GetString(ms.ToArray());
            }
            set {
                if (value.Length > 0) {
                    MemoryStream ms = new MemoryStream(Encoding.ASCII.GetBytes(value));
                    this.grdTemplates.DisplayLayout.LoadFromXml(ms,PropertyCategories.SortedColumns);
                }
            }
        }
        private bool validateRules(Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            bool validated = false;
            int schCloseOffset=-1,schDepartOffset=-1,schArrivalOffset=-1,schOFD1Offset=-1;
            DateTime schCloseDate,schDepartDate,schArrivalDate;
            //Biz Rules
            //1 - Schedule Close Days Offset <= Scheduled Departure Days Offset
            StringBuilder message = new StringBuilder();
            if(e.Row.Cells["ScheduledCloseDateOffset"].Text.Trim().Length > 0 && e.Row.Cells["ScheduledDepartureDateOffset"].Text.Trim().Length > 0) {
                schCloseOffset = Convert.ToInt32(e.Row.Cells["ScheduledCloseDateOffset"].Text);
                schDepartOffset = Convert.ToInt32(e.Row.Cells["ScheduledDepartureDateOffset"].Text);
                schCloseDate = Convert.ToDateTime(DateTime.Today.AddDays(schCloseOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledCloseTime"].Text);
                schDepartDate = Convert.ToDateTime(DateTime.Today.AddDays(schDepartOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledDepartureTime"].Text);
                if(schCloseDate > schDepartDate)
                    message.Append("Schedule Close Days Offset and Time must be less than or equal to Scheduled Departure Days Offset and Time." + Environment.NewLine);
            }
            else
                message.Append("Schedule Close and Departure Days Offset cells can't be empty.");
            
            //2- Scheduled arrival days offset <= OFD1 Days Offset
            if(e.Row.Cells["ScheduledArrivalDateOffset"].Text.Trim().Length > 0 && e.Row.Cells["ScheduledOFD1Offset"].Text.Trim().Length > 0) {
                schArrivalOffset = Convert.ToInt32(e.Row.Cells["ScheduledArrivalDateOffset"].Text);
                schOFD1Offset = Convert.ToInt32(e.Row.Cells["ScheduledOFD1Offset"].Text);
                if(schArrivalOffset > schOFD1Offset)
                    message.Append("Scheduled Arrival Days Offset must be less than or equal to Scheduled OFD1 Offset." + Environment.NewLine);
            }
            else
                message.Append("Schedule Close and Departure Days Offset cells can't be empty." + Environment.NewLine);

            //3- Scheduled Departure days offset and Time <= Arrival Days Offset and Time
            if(schDepartOffset != -1  && schArrivalOffset != -1) {
                schDepartDate = Convert.ToDateTime(DateTime.Today.AddDays(schDepartOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledDepartureTime"].Text);
                schArrivalDate = Convert.ToDateTime(DateTime.Today.AddDays(schArrivalOffset).ToShortDateString() + " " + e.Row.Cells["ScheduledArrivalTime"].Text);
                if(schDepartDate > schArrivalDate)
                    message.Append("Scheduled Departure Days Offset and Time must be less than or equal to Scheduled Arrival Days Offset and Time." + Environment.NewLine);
            }
            //4- OfD1 should not fall on a weekend day
            if(e.Row.Cells["DayOfTheWeek"].Text != "" && schOFD1Offset != -1) {
                int remainder1 = 0; int remainder2 = 0;
                int offsetDay = ShipScheduleTemplatesGateway.GetWeekday(e.Row.Cells["DayOfTheWeek"].Text) + schOFD1Offset;
                //weekends are 6 and 7 days and additions of 7 days like 6+7=13, 7+7=14
                //if the remainder results into 0 by dividing offsetDay by 7 or offsetDay +1 (for saturdays) by 7
                Math.DivRem(offsetDay,7,out remainder1);
                Math.DivRem(offsetDay + 1,7,out remainder2);
                if(remainder1 == 0 || remainder2 == 0)
                    message.Append("OFD1 Days Offset should not fall on the weekend day."  + Environment.NewLine);
            }
            //5- Validate S2
            if(e.Row.Cells["S2MainZone"].Text.Trim().Length > 0) {
                if(e.Row.Cells["S2ScheduledArrivalDateOffset"].Text.Trim().Length > 0 && e.Row.Cells["S2ScheduledOFD1Offset"].Text.Trim().Length > 0) {
                    if(Convert.ToInt32(e.Row.Cells["S2ScheduledArrivalDateOffset"].Text) > Convert.ToInt32(e.Row.Cells["S2ScheduledOFD1Offset"].Text))
                        message.Append("S2 Scheduled Arrival Offset must be less than or equal to S2 Scheduled OFD1 Offset." + Environment.NewLine);
                }
                else
                    message.Append("S2 Scheduled Arrival Offset and S2 Scheduled OFD1 Offset fields can't be empty." + Environment.NewLine);                if(e.Row.Cells["AgentNumber"].Text.Trim() == e.Row.Cells["S2AgentNumber"].Text.Trim()) {
                    message.Append("Agent Number for the second stop can't be the same as the first." + Environment.NewLine);
                }
            }
            if(message.ToString() != "")
                MessageBox.Show(message.ToString(),App.Product);
            else
                validated = true;
            return validated;

        }
        #endregion
	}
}
