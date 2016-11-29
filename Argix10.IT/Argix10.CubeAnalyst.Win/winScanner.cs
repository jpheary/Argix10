using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Windows;

namespace Argix.Tools {
	//
	public class winScanner : System.Windows.Forms.Form {
		//Members
		private Scanner mScanner=null;
		private UltraGridSvc mStatGridSvc=null;
		private UltraGridSvc mDetailGridSvc=null;
		private UltraGridSvc mLogGridSvc=null;
		private System.Windows.Forms.ToolTip mToolTip=null;
		private string mContext="";
		private string mSearchText="";
		
		#region Constants
		private const string MNU_REFRESH = "&Refresh";
		private const string MNU_CUT = "Cu&t";
		private const string MNU_COPY = "&Copy";
		private const string MNU_PASTE = "&Paste";
		private const string MNU_DELETE = "&Delete";
		#endregion
		public event EventHandler ServiceStatesChanged=null;
		public event StatusEventHandler StatusMessage=null;
		
		#region Controls
		private Infragistics.Win.UltraWinGrid.UltraGrid grdStats;
		private Argix.Tools.ScannerDS mCubeStatsDS;
		private Argix.Tools.ScannerDS mEventLogDS;
		private System.Windows.Forms.Splitter splitterH;
		private System.Windows.Forms.ContextMenu ctxScanner;
		private System.Windows.Forms.MenuItem ctxRefresh;
		private System.Windows.Forms.MenuItem ctxSep1;
		private System.Windows.Forms.MenuItem ctxCut;
		private System.Windows.Forms.MenuItem ctxCopy;
		private System.Windows.Forms.MenuItem ctxPaste;
		private System.Windows.Forms.MenuItem ctxSep2;
		private System.Windows.Forms.MenuItem ctxDelete;
		private System.Windows.Forms.TabPage tabDetails;
		private System.Windows.Forms.TabPage tabEvents;
		private Argix.Tools.ScannerDS mCubeDetailsDS;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdDetails;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdEvents;
		private System.Windows.Forms.LinkLabel lnkCancel;
		private System.Windows.Forms.TabControl tabBottom;
		private System.Windows.Forms.TabControl tabTop;
		private System.Windows.Forms.TabPage tabStats;
		private System.Windows.Forms.TabPage tabChart;
		private Infragistics.Win.UltraWinChart.UltraChart chtStats;
		private Argix.Tools.ScannerDS mCubeStatsSummary;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		//Interface
		public winScanner(Scanner scanner) {
			//
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				#region Menu identities (used for onclick handlers) 
				this.ctxRefresh.Text = MNU_REFRESH;
				this.ctxCut.Text = MNU_CUT;
				this.ctxCopy.Text = MNU_COPY;
				this.ctxPaste.Text = MNU_PASTE;
				this.ctxDelete.Text = MNU_DELETE;
				#endregion
				#region Window docking
				this.splitterH.MinExtra = 0;
				this.splitterH.MinSize = 18;
				this.tabTop.Dock = DockStyle.Fill;
				this.splitterH.Dock = DockStyle.Bottom;
				this.tabBottom.Dock = DockStyle.Bottom;
					this.grdEvents.Controls.AddRange(new Control[]{this.lnkCancel});
					this.lnkCancel.Top = 2;
				this.Controls.AddRange(new Control[]{this.tabTop, this.splitterH, this.tabBottom});
				#endregion
				
				//Bind to scanner and register for scanner events
				this.mScanner = scanner;
				this.mScanner.CubeStatsChanged += new EventHandler(this.OnCubeStatsChanged);
				this.mScanner.CubeStatsSummaryChanged += new EventHandler(this.OnCubeStatsSummaryChanged);
				this.mScanner.CubeDetailsChanged += new EventHandler(this.OnCubeDetailsChanged);
				this.mScanner.EventLogChanged += new EventHandler(this.OnEventLogChanged);
				this.mStatGridSvc = new UltraGridSvc(this.grdStats);
				this.mDetailGridSvc = new UltraGridSvc(this.grdDetails);
				this.mLogGridSvc = new UltraGridSvc(this.grdEvents);
				this.mToolTip = new System.Windows.Forms.ToolTip();
			}
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		public string TerminalName { get { return this.mScanner.TerminalName; } }
		public bool CanSaveAs { get { return true; } }
		public void SaveAs(string fileName) { this.mScanner.ToDataSet().WriteXml(fileName, XmlWriteMode.WriteSchema); }
		public void PageSettings() { UltraGridSvc.PageSettings(); }
		public bool CanPrint { get { return false; } }
		public void Print() { UltraGridSvc.Print(this.grdStats, true); }
        public void PrintPreview() { UltraGridSvc.PrintPreview(this.grdStats); }
        public bool CanCut { get { return this.ctxCut.Enabled; } }
		public void Cut() { this.ctxCut.PerformClick(); }
		public bool CanCopy { get { return this.ctxCopy.Enabled; } }
		public void Copy() { this.ctxCopy.PerformClick(); }
		public bool CanPaste { get { return this.ctxPaste.Enabled; } }
		public void Paste() { this.ctxPaste.PerformClick(); }
		public bool CanDelete { get { return this.ctxDelete.Enabled; } }
		public void Delete() { this.ctxDelete.PerformClick(); }
		public bool CanFind { get { return (this.grdDetails.Focused && this.grdDetails.Rows.Count > 0); } }
		public void Find(string searchText) { find(searchText); }
		public override void Refresh() { this.ctxRefresh.PerformClick(); }
		public Properties Properties { get { return this.mScanner.Properties; } }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CubeStatisticsTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DATE", -1, null, 0, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("HOUR", -1, null, 1, Infragistics.Win.UltraWinGrid.SortIndicator.Descending, false);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SOURCE");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("GOOD");
			Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NOTFOUND");
			Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BADREAD");
			Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BADCUBE");
			Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OTHER");
			Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CubeDetailsTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubeDate");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Source");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Scan");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LabelSeqNumber");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Result");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Message");
			Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
			Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ArgixLogTable", -1);
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Level");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Date");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Source");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Category");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Event");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("User");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Computer");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword1");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword2");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Keyword3");
			Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Message");
			Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
			Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
			Infragistics.UltraChart.Resources.Appearance.LineChartAppearance lineChartAppearance1 = new Infragistics.UltraChart.Resources.Appearance.LineChartAppearance();
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(winScanner));
			this.grdStats = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.ctxScanner = new System.Windows.Forms.ContextMenu();
			this.ctxRefresh = new System.Windows.Forms.MenuItem();
			this.ctxSep1 = new System.Windows.Forms.MenuItem();
			this.ctxCut = new System.Windows.Forms.MenuItem();
			this.ctxCopy = new System.Windows.Forms.MenuItem();
			this.ctxPaste = new System.Windows.Forms.MenuItem();
			this.ctxSep2 = new System.Windows.Forms.MenuItem();
			this.ctxDelete = new System.Windows.Forms.MenuItem();
			this.mCubeStatsDS = new Argix.Tools.ScannerDS();
			this.mEventLogDS = new Argix.Tools.ScannerDS();
			this.splitterH = new System.Windows.Forms.Splitter();
			this.tabBottom = new System.Windows.Forms.TabControl();
			this.tabDetails = new System.Windows.Forms.TabPage();
			this.grdDetails = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.mCubeDetailsDS = new Argix.Tools.ScannerDS();
			this.tabEvents = new System.Windows.Forms.TabPage();
			this.grdEvents = new Infragistics.Win.UltraWinGrid.UltraGrid();
			this.lnkCancel = new System.Windows.Forms.LinkLabel();
			this.tabTop = new System.Windows.Forms.TabControl();
			this.tabStats = new System.Windows.Forms.TabPage();
			this.tabChart = new System.Windows.Forms.TabPage();
			this.chtStats = new Infragistics.Win.UltraWinChart.UltraChart();
			this.mCubeStatsSummary = new Argix.Tools.ScannerDS();
			((System.ComponentModel.ISupportInitialize)(this.grdStats)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCubeStatsDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mEventLogDS)).BeginInit();
			this.tabBottom.SuspendLayout();
			this.tabDetails.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdDetails)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCubeDetailsDS)).BeginInit();
			this.tabEvents.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.grdEvents)).BeginInit();
			this.grdEvents.SuspendLayout();
			this.tabTop.SuspendLayout();
			this.tabStats.SuspendLayout();
			this.tabChart.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.chtStats)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mCubeStatsSummary)).BeginInit();
			this.SuspendLayout();
			// 
			// grdStats
			// 
			this.grdStats.ContextMenu = this.ctxScanner;
			this.grdStats.DataMember = "CubeStatisticsTable";
			this.grdStats.DataSource = this.mCubeStatsDS;
			appearance1.BackColor = System.Drawing.SystemColors.Window;
			appearance1.FontData.Name = "Verdana";
			appearance1.FontData.SizeInPoints = 8F;
			appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance1.TextHAlignAsString = "Left";
			this.grdStats.DisplayLayout.Appearance = appearance1;
			ultraGridColumn1.Format = "MM/dd/yyyy";
			ultraGridColumn1.Header.Caption = "Date";
			ultraGridColumn1.Header.VisiblePosition = 0;
			ultraGridColumn1.Width = 96;
			ultraGridColumn2.Header.Caption = "Hour";
			ultraGridColumn2.Header.VisiblePosition = 1;
			ultraGridColumn2.Width = 48;
			ultraGridColumn3.Header.VisiblePosition = 2;
			ultraGridColumn3.Hidden = true;
			appearance2.TextHAlignAsString = "Right";
			ultraGridColumn4.CellAppearance = appearance2;
			appearance3.TextHAlignAsString = "Center";
			ultraGridColumn4.Header.Appearance = appearance3;
			ultraGridColumn4.Header.Caption = "Good";
			ultraGridColumn4.Header.VisiblePosition = 3;
			ultraGridColumn4.Width = 72;
			appearance4.TextHAlignAsString = "Right";
			ultraGridColumn5.CellAppearance = appearance4;
			appearance5.TextHAlignAsString = "Center";
			ultraGridColumn5.Header.Appearance = appearance5;
			ultraGridColumn5.Header.Caption = "Not Found";
			ultraGridColumn5.Header.VisiblePosition = 4;
			ultraGridColumn5.Width = 72;
			appearance6.TextHAlignAsString = "Right";
			ultraGridColumn6.CellAppearance = appearance6;
			appearance7.TextHAlignAsString = "Center";
			ultraGridColumn6.Header.Appearance = appearance7;
			ultraGridColumn6.Header.Caption = "Bad Read";
			ultraGridColumn6.Header.VisiblePosition = 5;
			ultraGridColumn6.Width = 72;
			appearance8.TextHAlignAsString = "Right";
			ultraGridColumn7.CellAppearance = appearance8;
			appearance9.TextHAlignAsString = "Center";
			ultraGridColumn7.Header.Appearance = appearance9;
			ultraGridColumn7.Header.Caption = "Bad Cube";
			ultraGridColumn7.Header.VisiblePosition = 6;
			ultraGridColumn7.Width = 72;
			appearance10.TextHAlignAsString = "Right";
			ultraGridColumn8.CellAppearance = appearance10;
			appearance11.TextHAlignAsString = "Center";
			ultraGridColumn8.Header.Appearance = appearance11;
			ultraGridColumn8.Header.Caption = "Other";
			ultraGridColumn8.Header.VisiblePosition = 7;
			ultraGridColumn8.Width = 72;
			ultraGridBand1.Columns.AddRange(new object[] {
															 ultraGridColumn1,
															 ultraGridColumn2,
															 ultraGridColumn3,
															 ultraGridColumn4,
															 ultraGridColumn5,
															 ultraGridColumn6,
															 ultraGridColumn7,
															 ultraGridColumn8});
			this.grdStats.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
			appearance12.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance12.FontData.BoldAsString = "True";
			appearance12.FontData.Name = "Verdana";
			appearance12.FontData.SizeInPoints = 8F;
			appearance12.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance12.TextHAlignAsString = "Left";
			this.grdStats.DisplayLayout.CaptionAppearance = appearance12;
			this.grdStats.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdStats.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdStats.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdStats.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance13.BackColor = System.Drawing.SystemColors.Control;
			appearance13.FontData.BoldAsString = "True";
			appearance13.FontData.Name = "Verdana";
			appearance13.FontData.SizeInPoints = 8F;
			appearance13.TextHAlignAsString = "Left";
			this.grdStats.DisplayLayout.Override.HeaderAppearance = appearance13;
			this.grdStats.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
			this.grdStats.DisplayLayout.Override.MaxSelectedRows = 1;
			appearance14.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdStats.DisplayLayout.Override.RowAppearance = appearance14;
			this.grdStats.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdStats.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdStats.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdStats.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdStats.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdStats.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdStats.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdStats.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdStats.Location = new System.Drawing.Point(0, 0);
			this.grdStats.Name = "grdStats";
			this.grdStats.Size = new System.Drawing.Size(626, 159);
			this.grdStats.TabIndex = 3;
			this.grdStats.Text = "Cube Statisics";
			this.grdStats.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			this.grdStats.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
			this.grdStats.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnCubeStatsEntrySelected);
			this.grdStats.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdStats.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
			// 
			// ctxScanner
			// 
			this.ctxScanner.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																					   this.ctxRefresh,
																					   this.ctxSep1,
																					   this.ctxCut,
																					   this.ctxCopy,
																					   this.ctxPaste,
																					   this.ctxSep2,
																					   this.ctxDelete});
			// 
			// ctxRefresh
			// 
			this.ctxRefresh.Index = 0;
			this.ctxRefresh.Text = "Refresh";
			this.ctxRefresh.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep1
			// 
			this.ctxSep1.Index = 1;
			this.ctxSep1.Text = "-";
			// 
			// ctxCut
			// 
			this.ctxCut.Index = 2;
			this.ctxCut.Text = "C&ut";
			this.ctxCut.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxCopy
			// 
			this.ctxCopy.Index = 3;
			this.ctxCopy.Text = "&Copy";
			this.ctxCopy.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxPaste
			// 
			this.ctxPaste.Index = 4;
			this.ctxPaste.Text = "&Paste";
			this.ctxPaste.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// ctxSep2
			// 
			this.ctxSep2.Index = 5;
			this.ctxSep2.Text = "-";
			// 
			// ctxDelete
			// 
			this.ctxDelete.Index = 6;
			this.ctxDelete.Text = "Delete";
			this.ctxDelete.Click += new System.EventHandler(this.OnMenuClick);
			// 
			// mCubeStatsDS
			// 
			this.mCubeStatsDS.DataSetName = "ScannerDS";
			this.mCubeStatsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// mEventLogDS
			// 
			this.mEventLogDS.DataSetName = "ScannerDS";
			this.mEventLogDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// splitterH
			// 
			this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitterH.Location = new System.Drawing.Point(0, 185);
			this.splitterH.Name = "splitterH";
			this.splitterH.Size = new System.Drawing.Size(634, 3);
			this.splitterH.TabIndex = 5;
			this.splitterH.TabStop = false;
			// 
			// tabBottom
			// 
			this.tabBottom.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabBottom.Controls.Add(this.tabDetails);
			this.tabBottom.Controls.Add(this.tabEvents);
			this.tabBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.tabBottom.Location = new System.Drawing.Point(0, 188);
			this.tabBottom.Name = "tabBottom";
			this.tabBottom.SelectedIndex = 0;
			this.tabBottom.Size = new System.Drawing.Size(634, 240);
			this.tabBottom.TabIndex = 6;
			// 
			// tabDetails
			// 
			this.tabDetails.Controls.Add(this.grdDetails);
			this.tabDetails.Location = new System.Drawing.Point(4, 4);
			this.tabDetails.Name = "tabDetails";
			this.tabDetails.Size = new System.Drawing.Size(626, 214);
			this.tabDetails.TabIndex = 0;
			this.tabDetails.Text = "Details";
			// 
			// grdDetails
			// 
			this.grdDetails.ContextMenu = this.ctxScanner;
			this.grdDetails.DataMember = "CubeDetailsTable";
			this.grdDetails.DataSource = this.mCubeDetailsDS;
			appearance15.BackColor = System.Drawing.SystemColors.Window;
			appearance15.FontData.Name = "Verdana";
			appearance15.FontData.SizeInPoints = 8F;
			appearance15.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance15.TextHAlignAsString = "Left";
			this.grdDetails.DisplayLayout.Appearance = appearance15;
			ultraGridColumn9.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn9.Header.VisiblePosition = 0;
			ultraGridColumn9.Hidden = true;
			ultraGridColumn10.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn10.Format = "MM-dd-yyyy HH:mm:ss";
			ultraGridColumn10.Header.Caption = "Date";
			ultraGridColumn10.Header.VisiblePosition = 1;
			ultraGridColumn10.Width = 144;
			ultraGridColumn11.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn11.Header.VisiblePosition = 2;
			ultraGridColumn11.Hidden = true;
			ultraGridColumn11.Width = 120;
			ultraGridColumn12.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn12.Header.VisiblePosition = 3;
			ultraGridColumn12.Width = 192;
			ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn13.Header.Caption = "Label Seq#";
			ultraGridColumn13.Header.VisiblePosition = 4;
			ultraGridColumn13.Width = 120;
			ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn14.Header.VisiblePosition = 5;
			ultraGridColumn14.Width = 48;
			ultraGridColumn15.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
			ultraGridColumn15.Header.VisiblePosition = 6;
			ultraGridColumn15.Width = 72;
			ultraGridColumn16.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
			ultraGridColumn16.Header.VisiblePosition = 7;
			ultraGridColumn16.Width = 288;
			ultraGridBand2.Columns.AddRange(new object[] {
															 ultraGridColumn9,
															 ultraGridColumn10,
															 ultraGridColumn11,
															 ultraGridColumn12,
															 ultraGridColumn13,
															 ultraGridColumn14,
															 ultraGridColumn15,
															 ultraGridColumn16});
			this.grdDetails.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
			appearance16.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance16.FontData.BoldAsString = "True";
			appearance16.FontData.Name = "Verdana";
			appearance16.FontData.SizeInPoints = 8F;
			appearance16.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance16.TextHAlignAsString = "Left";
			this.grdDetails.DisplayLayout.CaptionAppearance = appearance16;
			this.grdDetails.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdDetails.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdDetails.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
			this.grdDetails.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdDetails.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance17.BackColor = System.Drawing.SystemColors.Control;
			appearance17.FontData.BoldAsString = "True";
			appearance17.FontData.Name = "Verdana";
			appearance17.FontData.SizeInPoints = 8F;
			appearance17.TextHAlignAsString = "Left";
			this.grdDetails.DisplayLayout.Override.HeaderAppearance = appearance17;
			this.grdDetails.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdDetails.DisplayLayout.Override.MaxSelectedRows = 1;
			appearance18.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdDetails.DisplayLayout.Override.RowAppearance = appearance18;
			this.grdDetails.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdDetails.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
			this.grdDetails.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdDetails.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdDetails.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdDetails.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdDetails.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdDetails.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdDetails.Location = new System.Drawing.Point(0, 0);
			this.grdDetails.Name = "grdDetails";
			this.grdDetails.Size = new System.Drawing.Size(626, 214);
			this.grdDetails.TabIndex = 5;
			this.grdDetails.Text = "Cube Details";
			this.grdDetails.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			this.grdDetails.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnCubeDetailsEntrySelected);
			this.grdDetails.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			this.grdDetails.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
			// 
			// mCubeDetailsDS
			// 
			this.mCubeDetailsDS.DataSetName = "ScannerDS";
			this.mCubeDetailsDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// tabEvents
			// 
			this.tabEvents.Controls.Add(this.grdEvents);
			this.tabEvents.Location = new System.Drawing.Point(4, 4);
			this.tabEvents.Name = "tabEvents";
			this.tabEvents.Size = new System.Drawing.Size(626, 214);
			this.tabEvents.TabIndex = 1;
			this.tabEvents.Text = "Events";
			// 
			// grdEvents
			// 
			this.grdEvents.Controls.Add(this.lnkCancel);
			this.grdEvents.Cursor = System.Windows.Forms.Cursors.Default;
			this.grdEvents.DataMember = "ArgixLogTable";
			this.grdEvents.DataSource = this.mEventLogDS;
			appearance19.BackColor = System.Drawing.SystemColors.Window;
			appearance19.FontData.Name = "Verdana";
			appearance19.FontData.SizeInPoints = 8F;
			appearance19.ForeColor = System.Drawing.SystemColors.WindowText;
			appearance19.TextHAlignAsString = "Left";
			this.grdEvents.DisplayLayout.Appearance = appearance19;
			ultraGridBand3.AddButtonCaption = "TLViewTable";
			ultraGridColumn17.Header.VisiblePosition = 0;
			ultraGridColumn17.Hidden = true;
			ultraGridColumn18.Header.VisiblePosition = 1;
			ultraGridColumn18.Hidden = true;
			ultraGridColumn18.Width = 120;
			ultraGridColumn19.Header.VisiblePosition = 3;
			ultraGridColumn19.Width = 48;
			ultraGridColumn20.Format = "MM/dd/yyyy HH:mm:ss tt";
			ultraGridColumn20.Header.VisiblePosition = 2;
			ultraGridColumn20.Width = 144;
			ultraGridColumn21.Header.VisiblePosition = 8;
			ultraGridColumn21.Hidden = true;
			ultraGridColumn21.Width = 120;
			ultraGridColumn22.Header.VisiblePosition = 11;
			ultraGridColumn22.Hidden = true;
			ultraGridColumn22.Width = 72;
			ultraGridColumn23.Header.VisiblePosition = 12;
			ultraGridColumn23.Hidden = true;
			ultraGridColumn23.Width = 48;
			ultraGridColumn24.Header.VisiblePosition = 10;
			ultraGridColumn24.Hidden = true;
			ultraGridColumn24.Width = 96;
			ultraGridColumn25.Header.VisiblePosition = 9;
			ultraGridColumn25.Hidden = true;
			ultraGridColumn25.Width = 96;
			ultraGridColumn26.Header.VisiblePosition = 5;
			ultraGridColumn26.Width = 96;
			ultraGridColumn27.Header.VisiblePosition = 6;
			ultraGridColumn27.Width = 96;
			ultraGridColumn28.Header.VisiblePosition = 7;
			ultraGridColumn28.Width = 96;
			ultraGridColumn29.Header.VisiblePosition = 4;
			ultraGridColumn29.Width = 192;
			ultraGridBand3.Columns.AddRange(new object[] {
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
															 ultraGridColumn29});
			this.grdEvents.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
			this.grdEvents.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
			appearance20.BackColor = System.Drawing.SystemColors.InactiveCaption;
			appearance20.FontData.BoldAsString = "True";
			appearance20.FontData.Name = "Verdana";
			appearance20.FontData.SizeInPoints = 8F;
			appearance20.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			appearance20.TextHAlignAsString = "Left";
			this.grdEvents.DisplayLayout.CaptionAppearance = appearance20;
			this.grdEvents.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
			this.grdEvents.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
			this.grdEvents.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
			this.grdEvents.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
			appearance21.BackColor = System.Drawing.SystemColors.Control;
			appearance21.FontData.BoldAsString = "True";
			appearance21.FontData.Name = "Verdana";
			appearance21.FontData.SizeInPoints = 8F;
			appearance21.TextHAlignAsString = "Left";
			this.grdEvents.DisplayLayout.Override.HeaderAppearance = appearance21;
			this.grdEvents.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
			this.grdEvents.DisplayLayout.Override.MaxSelectedRows = 0;
			appearance22.BorderColor = System.Drawing.SystemColors.ControlLight;
			this.grdEvents.DisplayLayout.Override.RowAppearance = appearance22;
			this.grdEvents.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
			this.grdEvents.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
			this.grdEvents.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
			this.grdEvents.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
			this.grdEvents.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
			this.grdEvents.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
			this.grdEvents.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
			this.grdEvents.Dock = System.Windows.Forms.DockStyle.Fill;
			this.grdEvents.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grdEvents.Location = new System.Drawing.Point(0, 0);
			this.grdEvents.Name = "grdEvents";
			this.grdEvents.Size = new System.Drawing.Size(626, 214);
			this.grdEvents.TabIndex = 114;
			this.grdEvents.Text = "Argix Log";
			this.grdEvents.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
			this.grdEvents.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnEventLogEntrySelected);
			this.grdEvents.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
			// 
			// lnkCancel
			// 
			this.lnkCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lnkCancel.BackColor = System.Drawing.SystemColors.InactiveCaption;
			this.lnkCancel.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.lnkCancel.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lnkCancel.LinkColor = System.Drawing.Color.White;
			this.lnkCancel.LinkVisited = true;
			this.lnkCancel.Location = new System.Drawing.Point(572, 3);
			this.lnkCancel.Name = "lnkCancel";
			this.lnkCancel.Size = new System.Drawing.Size(48, 16);
			this.lnkCancel.TabIndex = 115;
			this.lnkCancel.TabStop = true;
			this.lnkCancel.Text = "Cancel";
			this.lnkCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lnkCancel.Visible = false;
			this.lnkCancel.VisitedLinkColor = System.Drawing.Color.White;
			// 
			// tabTop
			// 
			this.tabTop.Alignment = System.Windows.Forms.TabAlignment.Bottom;
			this.tabTop.Controls.Add(this.tabStats);
			this.tabTop.Controls.Add(this.tabChart);
			this.tabTop.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tabTop.Location = new System.Drawing.Point(0, 0);
			this.tabTop.Name = "tabTop";
			this.tabTop.SelectedIndex = 0;
			this.tabTop.Size = new System.Drawing.Size(634, 185);
			this.tabTop.TabIndex = 7;
			// 
			// tabStats
			// 
			this.tabStats.Controls.Add(this.grdStats);
			this.tabStats.Location = new System.Drawing.Point(4, 4);
			this.tabStats.Name = "tabStats";
			this.tabStats.Size = new System.Drawing.Size(626, 159);
			this.tabStats.TabIndex = 0;
			this.tabStats.Text = "Statistics";
			// 
			// tabChart
			// 
			this.tabChart.Controls.Add(this.chtStats);
			this.tabChart.Location = new System.Drawing.Point(4, 4);
			this.tabChart.Name = "tabChart";
			this.tabChart.Size = new System.Drawing.Size(626, 159);
			this.tabChart.TabIndex = 1;
			this.tabChart.Text = "Chart";
			// 
			//			'UltraChart' properties's serialization: Since 'ChartType' changes the way axes look,
			//			'ChartType' must be persisted ahead of any Axes change made in design time.
			//		
			this.chtStats.ChartType = Infragistics.UltraChart.Shared.Styles.ChartType.LineChart;
			// 
			// chtStats
			// 
			this.chtStats.Axis.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(255)), ((System.Byte)(248)), ((System.Byte)(220)));
			this.chtStats.Axis.X.Extent = 12;
			this.chtStats.Axis.X.Labels.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F);
			this.chtStats.Axis.X.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.X.Labels.SeriesLabels.FormatString = "";
			this.chtStats.Axis.X.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.X.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
			this.chtStats.Axis.X.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.X.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.X.Labels.Visible = false;
			this.chtStats.Axis.X.LineThickness = 1;
			this.chtStats.Axis.X.Visible = false;
			this.chtStats.Axis.X2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.X2.Labels.SeriesLabels.FormatString = "";
			this.chtStats.Axis.X2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.X2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.VerticalLeftFacing;
			this.chtStats.Axis.X2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.X2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Y.Extent = 18;
			this.chtStats.Axis.Y.Labels.HorizontalAlign = System.Drawing.StringAlignment.Far;
			this.chtStats.Axis.Y.Labels.ItemFormatString = "<DATA_VALUE:00>";
			this.chtStats.Axis.Y.Labels.SeriesLabels.FormatString = "";
			this.chtStats.Axis.Y.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Far;
			this.chtStats.Axis.Y.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
			this.chtStats.Axis.Y.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Y.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Y.LineThickness = 1;
			this.chtStats.Axis.Y.RangeMax = 100;
			this.chtStats.Axis.Y.RangeType = Infragistics.UltraChart.Shared.Styles.AxisRangeType.Custom;
			this.chtStats.Axis.Y2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.Y2.Labels.ItemFormatString = "<DATA_VALUE:00.00>";
			this.chtStats.Axis.Y2.Labels.SeriesLabels.FormatString = "";
			this.chtStats.Axis.Y2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.Y2.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
			this.chtStats.Axis.Y2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Y2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Z.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.Z.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.Z.Labels.SeriesLabels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
			this.chtStats.Axis.Z.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Z.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Z2.Labels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.Z2.Labels.Orientation = Infragistics.UltraChart.Shared.Styles.TextOrientation.Horizontal;
			this.chtStats.Axis.Z2.Labels.SeriesLabels.HorizontalAlign = System.Drawing.StringAlignment.Near;
			this.chtStats.Axis.Z2.Labels.SeriesLabels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Axis.Z2.Labels.VerticalAlign = System.Drawing.StringAlignment.Center;
			this.chtStats.Border.Raised = true;
			this.chtStats.ColorModel.AlphaLevel = ((System.Byte)(150));
			this.chtStats.ColorModel.Scaling = Infragistics.UltraChart.Shared.Styles.ColorScaling.Random;
			this.chtStats.Data.DataMember = "CubeStatisticsSummaryTable";
			this.chtStats.Data.MaxValue = 100;
			this.chtStats.Data.MinValue = 0;
			this.chtStats.Data.SwapRowsAndColumns = true;
			this.chtStats.DataMember = "CubeStatisticsSummaryTable";
			this.chtStats.DataSource = this.mCubeStatsSummary.CubeStatisticsSummaryTable;
			this.chtStats.Dock = System.Windows.Forms.DockStyle.Fill;
			this.chtStats.ForeColor = System.Drawing.SystemColors.ControlText;
			this.chtStats.Legend.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
			this.chtStats.Legend.Margins.Bottom = 3;
			this.chtStats.Legend.Margins.Left = 3;
			this.chtStats.Legend.Margins.Right = 3;
			this.chtStats.Legend.Margins.Top = 36;
			this.chtStats.Legend.SpanPercentage = 13;
			this.chtStats.Legend.Visible = true;
			lineChartAppearance1.Thickness = 1;
			this.chtStats.LineChart = lineChartAppearance1;
			this.chtStats.Location = new System.Drawing.Point(0, 0);
			this.chtStats.Name = "chtStats";
			this.chtStats.Size = new System.Drawing.Size(626, 159);
			this.chtStats.TabIndex = 0;
			this.chtStats.TitleBottom.Visible = false;
			this.chtStats.TitleTop.Extent = 24;
			this.chtStats.TitleTop.Font = new System.Drawing.Font("Verdana", 9.75F);
			this.chtStats.TitleTop.Margins.Bottom = 1;
			this.chtStats.TitleTop.Margins.Left = 1;
			this.chtStats.TitleTop.Margins.Right = 1;
			this.chtStats.TitleTop.Margins.Top = 1;
			this.chtStats.TitleTop.Text = "Summary";
			this.chtStats.Tooltips.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F);
			this.chtStats.Tooltips.FormatString = "<ITEM_LABEL>: <DATA_VALUE:00.##>";
			this.chtStats.Tooltips.TooltipControl = null;
			// 
			// mCubeStatsSummary
			// 
			this.mCubeStatsSummary.DataSetName = "ScannerDS";
			this.mCubeStatsSummary.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// winScanner
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.ClientSize = new System.Drawing.Size(634, 428);
			this.Controls.Add(this.tabTop);
			this.Controls.Add(this.splitterH);
			this.Controls.Add(this.tabBottom);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "winScanner";
			this.Text = "Cube Scanner";
			this.Resize += new System.EventHandler(this.OnFormResize);
			this.Load += new System.EventHandler(this.OnFormLoad);
			((System.ComponentModel.ISupportInitialize)(this.grdStats)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCubeStatsDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mEventLogDS)).EndInit();
			this.tabBottom.ResumeLayout(false);
			this.tabDetails.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdDetails)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCubeDetailsDS)).EndInit();
			this.tabEvents.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.grdEvents)).EndInit();
			this.grdEvents.ResumeLayout(false);
			this.tabTop.ResumeLayout(false);
			this.tabStats.ResumeLayout(false);
			this.tabChart.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.chtStats)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mCubeStatsSummary)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load
				this.Visible = true;
				Application.DoEvents();
				#region Tooltips
				this.mToolTip.InitialDelay = 500;
				this.mToolTip.AutoPopDelay = 3000;
				this.mToolTip.ReshowDelay = 1000;
				this.mToolTip.ShowAlways = true;		//Even when form is inactve
				#endregion
				
				//Display scanner properties
				this.Text = this.mScanner.SourceName;
				this.grdStats.Text = this.mScanner.SourceName + " Statistics";
				this.chtStats.TitleTop.Text = "Summary";
				this.grdDetails.Text = this.mScanner.SourceName + " Details";
				this.grdEvents.Text = this.mScanner.SourceName + " Events";
				
				#region Grid initialization
				this.grdStats.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdStats.DisplayLayout.Bands[0].Columns["DATE"].SortIndicator = SortIndicator.Descending;
				this.grdStats.DisplayLayout.Bands[0].Columns["HOUR"].SortIndicator = SortIndicator.Descending;
				this.grdDetails.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdDetails.DisplayLayout.Bands[0].Columns["CubeDate"].SortIndicator = SortIndicator.Descending;
				this.grdEvents.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdEvents.DisplayLayout.Bands[0].Columns["Date"].SortIndicator = SortIndicator.Descending;
				#endregion
				this.grdStats.DataSource = this.mScanner.CubeStats;
				this.chtStats.DataSource = this.mScanner.CubeStatsSummary;
				this.grdDetails.DataSource = this.mScanner.CubeDetails;
				this.grdEvents.DataSource = this.mScanner.EventLog;
				
				//Get initial views
				this.ctxRefresh.PerformClick();
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnFormResize(object sender, System.EventArgs e) { }
		#region Cube Statistics: OnCubeStatsChanged(), OnCubeStatsSummaryChanged(), OnCubeStatsEntrySelected()
		private void OnCubeStatsChanged(object sender, EventArgs e) {
			//Event handler for refresh of scanner cube stats
			try { 
				this.grdStats.Text = this.mScanner.SourceName + " Statistics: " + DateTime.Today.AddDays(-this.mScanner.DaysBack).ToShortDateString() + " - " + DateTime.Today.ToShortDateString();
				this.grdStats.Rows[0].Selected = true; 
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		private void OnCubeStatsEntrySelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			setUserServices();
		}
		private void OnCubeStatsSummaryChanged(object sender, EventArgs e) {
			//Event handler for refresh of scanner cube stats
			try { 
				this.chtStats.TitleTop.Text = this.mScanner.SourceName + " Statistics Summary: " + DateTime.Today.AddDays(-this.mScanner.DaysBack).ToShortDateString() + " - " + DateTime.Today.ToShortDateString();
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		#endregion
		#region Cube Details: OnCubeDetailsChanged(), OnCubeDetailsEntrySelected(), OnEventLogChanged(), OnEventLogEntrySelected()
		private void OnCubeDetailsChanged(object sender, EventArgs e) {
			//Event handler for refresh of scanner cube stats
			try { 
				this.grdDetails.Text = this.mScanner.SourceName + " Details: " + DateTime.Today.AddDays(-this.mScanner.DaysBack).ToShortDateString() + " - " + DateTime.Today.ToShortDateString();
				this.grdDetails.Rows[0].Selected = true; 
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		private void OnCubeDetailsEntrySelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			setUserServices();
		}
		private void OnEventLogChanged(object sender, EventArgs e) {
			//Event handler for refresh of scanner event log
			try { 
				this.grdEvents.Text = this.mScanner.SourceName + " Events: " + DateTime.Today.AddDays(-this.mScanner.DaysBack).ToShortDateString() + " - " + DateTime.Today.ToShortDateString();
				this.grdEvents.Rows[0].Selected = true; 
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		private void OnEventLogEntrySelected(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in selected data entry
			setUserServices();
		}		
		#endregion
		#region Grid Support: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridMouseDown(), OnGridKeyUp()
		private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "Total Ctns");
				e.Layout.Bands[0].Columns["Total Ctns"].DataType = typeof(int);
				e.Layout.Bands[0].Columns["Total Ctns"].Format = "#0";
				e.Layout.Bands[0].Columns["Total Ctns"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["Total Ctns"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Good");
				e.Layout.Bands[0].Columns["%Good"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Good"].Format = "#0";
				e.Layout.Bands[0].Columns["%Good"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Good"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Not Found");
				e.Layout.Bands[0].Columns["%Not Found"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Not Found"].Format = "#0";
				e.Layout.Bands[0].Columns["%Not Found"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Not Found"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Bad Read");
				e.Layout.Bands[0].Columns["%Bad Read"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Bad Read"].Format = "#0";
				e.Layout.Bands[0].Columns["%Bad Read"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Bad Read"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Bad Cube");
				e.Layout.Bands[0].Columns["%Bad Cube"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Bad Cube"].Format = "#0";
				e.Layout.Bands[0].Columns["%Bad Cube"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Bad Cube"].CellAppearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "%Other");
				e.Layout.Bands[0].Columns["%Other"].DataType = typeof(float);
				e.Layout.Bands[0].Columns["%Other"].Format = "#0";
				e.Layout.Bands[0].Columns["%Other"].Header.Appearance.TextHAlign = HAlign.Right;
				e.Layout.Bands[0].Columns["%Other"].CellAppearance.TextHAlign = HAlign.Right;
			} 
			catch(Exception) { }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//Calculate derived columns
				int totalCartons =	Convert.ToInt32(e.Row.Cells["GOOD"].Value) + Convert.ToInt32(e.Row.Cells["NOTFOUND"].Value) + 
					Convert.ToInt32(e.Row.Cells["BADREAD"].Value) + Convert.ToInt32(e.Row.Cells["BADCUBE"].Value) + 
					Convert.ToInt32(e.Row.Cells["OTHER"].Value);
				e.Row.Cells["Total Ctns"].Value = totalCartons;
				e.Row.Cells["%Good"].Value = (Convert.ToSingle(e.Row.Cells["GOOD"].Value) / totalCartons) * 100;
				e.Row.Cells["%Not Found"].Value = (Convert.ToSingle(e.Row.Cells["NOTFOUND"].Value) / totalCartons) * 100;
				e.Row.Cells["%Bad Read"].Value = (Convert.ToSingle(e.Row.Cells["BADREAD"].Value) / totalCartons) * 100;
				e.Row.Cells["%Bad Cube"].Value = (Convert.ToSingle(e.Row.Cells["BADCUBE"].Value) / totalCartons) * 100;
				e.Row.Cells["%Other"].Value = (Convert.ToSingle(e.Row.Cells["OTHER"].Value) / totalCartons) * 100;
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event
			try {
				//Set menu and toolbar services
				UltraGrid oGrid = (UltraGrid)sender;
				UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				object oContext=null;
				if(e.Button == MouseButtons.Right) {
					oContext = oUIElement.GetContext(typeof(Infragistics.Win.UltraWinGrid.UltraGridCell));
					if(oContext != null) {
						//On row
						UltraGridCell oCell = (UltraGridCell)oContext;
						oGrid.ActiveRow = oCell.Row;
						oGrid.ActiveRow.Selected = true;
					}
					else {
						//Off row
						oContext = oUIElement.GetContext(typeof(RowScrollRegion));
						if(oContext != null) {
							oGrid.ActiveRow = null;
							if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
						}
					}
					oGrid.Focus();
				}
				else if(e.Button == MouseButtons.Left) {
					//Remove selected item if scrolling
					oContext = oUIElement.GetContext(typeof(RowScrollRegion));
					if(oContext != null) {
						oGrid.ActiveRow = null;
						if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Selected = false;
					}
				}
			} 
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
			finally { setUserServices(); }
		}
		private void OnGridKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for key up event: search on F3
			try {
				if(e.KeyCode == Keys.F3 && this.mSearchText.Length > 0) 
					find(this.mSearchText);
			}
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		#endregion
		#region User services: OnMenuClick()
		private void OnMenuClick(object sender, System.EventArgs e) {
			//Event handler for menu selection
			this.Cursor = Cursors.WaitCursor;
			try  {
				MenuItem menu = (MenuItem)sender;
				switch(menu.Text)  {
					case MNU_REFRESH:	
						this.lnkCancel.Enabled = true;
						this.mScanner.Refresh(); 
						break;
					case MNU_CUT:			break;
					case MNU_COPY:		
					switch(this.mContext) {
						case "grdStats":	break;
						case "grdEvents":	break;
						case "grdDetails":
							if(this.grdDetails.Selected != null) 
								Clipboard.SetDataObject(this.grdDetails.Selected.Rows[0].Cells["LabelSeqNumber"].Value.ToString(), false);
							break;
					}
						break;
					case MNU_PASTE:			break;
					case MNU_DELETE:		break;
				}
			}
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local services: setUserServices(), find(), findRow(), sendStatusMessage()
		private void setUserServices() {
			//Set user services
			bool canCut=false, canCopy=false, canPaste=false, canDelete=false;
			try {
				//
				if(this.grdStats.Focused)
					this.mContext = "grdStats";
				if(this.grdDetails.Focused)
					this.mContext = "grdDetails";
				else if(this.grdEvents.Focused)
					this.mContext = "grdEvents";
				switch(this.mContext) {
					case "grdStats":	break;
					case "grdDetails":
						if(this.grdDetails.Selected != null) {
							canCopy = (this.grdDetails.Selected.Rows.Count > 0);
						}
						break;
					case "grdEvents":	break;
				}
				
				//Set menu/context menu states
				this.ctxRefresh.Enabled = true;
				this.ctxCut.Enabled = canCut;
				this.ctxCopy.Enabled = canCopy;
				this.ctxPaste.Enabled = canPaste;
				this.ctxDelete.Enabled = canDelete;
			}
			catch(Exception ex)  { sendStatusMessage(new StatusEventArgs(ex.Message)); }
			finally { if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		private void find(string searchText) {
			//
			try {
				//Searc the grid for a message containing this search text
				this.mSearchText = searchText;
				int startRow=0;
				if(this.grdDetails.Selected.Rows.Count > 0) {
					if(this.grdDetails.Selected.Rows[0].VisibleIndex != this.grdDetails.Rows.VisibleRowCount - 1)
						startRow = this.grdDetails.Selected.Rows[0].VisibleIndex + 1;
				}
				UltraGridRow row = findRow("LabelSeqNumber", startRow, searchText);
				if(startRow > 0 && row == null)
					row = findRow("LabelSeqNumber", 0, searchText);
				if(row != null) {
					this.grdDetails.DisplayLayout.RowScrollRegions[0].ScrollRowIntoView(row);
					this.grdDetails.ActiveRow = row;
					this.grdDetails.ActiveRow.Selected = true;
				}
				else
					MessageBox.Show(this, "Label sequence# " + searchText + " not found.", "Find...", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
			}
			catch(Exception ex) { sendStatusMessage(new StatusEventArgs(ex.Message)); }
		}
		public UltraGridRow findRow(string colKey, int startRow, string searchText) {
			//Event handler for change in search text value
			UltraGridRow matchingRow=null;
			Cursor.Current = Cursors.WaitCursor;
			try {
				//Validate grid column and search word
				if(!this.grdDetails.DisplayLayout.Bands[0].Columns.Exists(colKey) || searchText.Length == 0)
					return null;
				
				//Find a matching row
				bool isNumeric = (this.grdDetails.DisplayLayout.Bands[0].Columns[colKey].DataType == Type.GetType("System.Int32"));
				if(isNumeric) {
					//Numeric column
					long searchValue = Convert.ToInt64(searchText);
					for(int i=startRow; i<this.grdDetails.Rows.VisibleRowCount; i++) {
						try { 
							UltraGridRow row = this.grdDetails.Rows[this.grdDetails.Rows.GetRowAtVisibleIndex(i).Index];
							long cellValue = Convert.ToInt64(row.Cells[colKey].Value);
							if(cellValue == searchValue) {
								matchingRow = row;
								break;
							}
						} 
						catch { }
					}
				}
				else {
					//Non-numeric column
					for(int i=startRow; i<this.grdDetails.Rows.VisibleRowCount; i++) {
						try { 
							UltraGridRow row = this.grdDetails.Rows[this.grdDetails.Rows.GetRowAtVisibleIndex(i).Index];
							string cellText = row.Cells[colKey].Value.ToString();
							if(cellText == searchText) {
								matchingRow = row;
								break;
							}
						} 
						catch { }
					}
				}
				return matchingRow;
			}
			catch(Exception ex) { throw ex; }
			finally { Cursor.Current = Cursors.Default; }
		}
		private void sendStatusMessage(StatusEventArgs e) { if(this.StatusMessage!=null) this.StatusMessage(this, e); }
		#endregion
	}
}
