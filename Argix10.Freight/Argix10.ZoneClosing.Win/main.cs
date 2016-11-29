using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Collections.Specialized;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Printing;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.AgentLineHaul;
using Argix.Security;
using Argix.Windows;

namespace Argix.Freight {
	//Application main window
	public class frmMain : System.Windows.Forms.Form {
		//Memebers
        private ShipScheduleItem mTrip = null;
        private PageSettings mPageSettings = null;
        private UltraGridSvc mGridSvcZones=null,mGridSvcTLs=null,mGridSvcLaneUpdates=null,mGridSvcZoneUpdates=null,mGridSvcShipSchedule=null;
        private System.Windows.Forms.ToolTip mToolTip=null;
        private MessageManager mMessageMgr=null;
        private NameValueCollection mHelpItems=null;
		private bool mIsDragging=false, mCalendarOpen=false;

		private const int KEYSTATE_SHIFT = 5, KEYSTATE_CTL = 9;
        #region Controls

        private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button tsCancel;
        private System.Windows.Forms.Button tsOK;
		private System.Windows.Forms.Panel pnlMain;
		private System.Windows.Forms.ComboBox cboSmallLane;
		private System.Windows.Forms.ComboBox cboLane;
		private System.Windows.Forms.TextBox txtSearchSort;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdZones;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdLaneUpdates;
		private Argix.Freight.FreightDataset mZones;
        private Argix.Freight.FreightDataset mUpdates;
		private Argix.Windows.ArgixStatusBar ssMain;
		private System.Windows.Forms.TabControl tabMain;
		private System.Windows.Forms.TabPage tabLanes;
		private System.Windows.Forms.TabPage tabZones;
		private System.Windows.Forms.TabPage tabSchedule;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdZoneUpdates;
        private System.Windows.Forms.Splitter splitterV;
		private System.Windows.Forms.DateTimePicker dtpScheduleDate;
		private Infragistics.Win.UltraWinGrid.UltraGrid grdShipSchedule;
        private Argix.AgentLineHaul.ShipScheduleDataset mShipSchedule;
		private System.Windows.Forms.Splitter splitterH;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdTLs;
        private MenuStrip msMain;
        private ToolStripMenuItem msFile;
        private ToolStripMenuItem msEdit;
        private ToolStripMenuItem msView;
        private ToolStripMenuItem msOp;
        private ToolStripMenuItem msReports;
        private ToolStripMenuItem msTools;
        private ToolStripMenuItem msHelp;
        private ToolStripMenuItem msFileNew;
        private ToolStripMenuItem msFileOpen;
        private ToolStripSeparator msFileSep1;
        private ToolStripMenuItem msFileSave;
        private ToolStripMenuItem msFileSaveAs;
        private ToolStripSeparator msFileSep2;
        private ToolStripMenuItem msFileSetup;
        private ToolStripMenuItem msFilePrint;
        private ToolStripMenuItem msFilePreview;
        private ToolStripSeparator msFileSep3;
        private ToolStripMenuItem msFileExit;
        private ToolStripMenuItem msEditCut;
        private ToolStripMenuItem msEditCopy;
        private ToolStripMenuItem msEditPaste;
        private ToolStripSeparator msEditSep1;
        private ToolStripMenuItem msEditFind;
        private ToolStripMenuItem msEditFindTL;
        private ToolStripMenuItem msViewRefresh;
        private ToolStripSeparator msViewSep1;
        private ToolStripMenuItem msViewZoneType;
        private ToolStripSeparator msViewSep2;
        private ToolStripMenuItem msViewToolbar;
        private ToolStripMenuItem msViewStatusBar;
        private ToolStripMenuItem msViewZoneTypeTsort;
        private ToolStripMenuItem msViewZoneTypeReturns;
        private ToolStripMenuItem msViewZoneTypeAll;
        private ToolStripMenuItem msOpAdd;
        private ToolStripMenuItem msOpRem;
        private ToolStripMenuItem msOpChangeLanes;
        private ToolStripMenuItem msOpCloseZones;
        private ToolStripSeparator msOpSep1;
        private ToolStripMenuItem msOpOpen;
        private ToolStripMenuItem msOpCloseAllTLs;
        private ToolStripMenuItem msOpClose;
        private ToolStripSeparator msOpSep2;
        private ToolStripMenuItem msOpAssign;
        private ToolStripMenuItem msOpUnassign;
        private ToolStripMenuItem msOpMove;
        private ToolStripMenuItem msReportsZonesByLane;
        private ToolStripMenuItem msToolsConfig;
        private ToolStripMenuItem msHelpAbout;
        private ToolStripSeparator msHelpSep1;
        private ContextMenuStrip csZone;
        private ContextMenuStrip csTrip;
        private ToolStripMenuItem csZoneAdd;
        private ToolStripSeparator csZoneSep1;
        private ToolStripMenuItem csZoneAssign;
        private ToolStripMenuItem csTripRem;
        private ToolStripSeparator csTripSep1;
        private ToolStripMenuItem csTripOpen;
        private ToolStripMenuItem csTripCloseAllTLs;
        private ToolStripMenuItem csTripClose;
        private ToolStripSeparator csTripSep2;
        private ToolStripMenuItem csTripUnassign;
        private ToolStrip tsMain;
        private ToolStripDropDownButton tsZoneType;
        private ToolStripButton tsNew;
        private ToolStripButton tsOpen_;
        private ToolStripButton tsPrint;
        private ToolStripSeparator tsSep1;
        private ToolStripButton tsSearch;
        private ToolStripSeparator tsSep2;
        private ToolStripButton tsRefresh;
        private ToolStripSeparator tsSep3;
        private ToolStripButton tsAdd;
        private ToolStripButton tsRem;
        private ToolStripButton tsChangeLanes;
        private ToolStripButton tsCloseZones;
        private ToolStripSeparator tsSep4;
        private ToolStripButton tsOpen;
        private ToolStripButton tsClose;
        private ToolStripButton tsAssign;
        private ToolStripButton tsUnassign;
        private ToolStripButton tsMove;
        private FreightDataset mLanes;
        private ToolStripMenuItem tsZoneTypeTsort;
        private ToolStripMenuItem tsZoneTypeReturns;
        private ToolStripMenuItem tsZoneTypeAll;
        private ToolStripMenuItem msViewFont;
        private ToolStripSeparator msViewSep3;
        private ToolStripMenuItem csTripMove;		
		#endregion
		
		//Interface
		public frmMain() {
			//Constructor
			try {
				//Required for designer support
                InitializeComponent();
                this.Text = "Argix Logistics " + App.Product;
                buildHelpMenu();
                #region Set window docking
                this.msMain.Dock = DockStyle.Top;
				this.tsMain.Dock = DockStyle.Top;
				this.splitterV.MinExtra = 288;
				this.splitterV.MinSize = 312;
				this.splitterV.Dock = DockStyle.Left;
				this.pnlMain.Dock = DockStyle.Left;
				this.grdZones.Controls.AddRange(new Control[]{this.txtSearchSort});
				this.txtSearchSort.Top = 1;
				this.txtSearchSort.Left = this.grdZones.Width - this.txtSearchSort.Width - 3;
				this.pnlMain.Controls.AddRange(new Control[]{this.grdZones,this.splitterH,this.grdTLs});
				this.grdShipSchedule.Controls.Add(this.dtpScheduleDate);
				this.tabMain.Dock = DockStyle.Fill;
				this.ssMain.Dock = DockStyle.Bottom;
                this.Controls.AddRange(new Control[] { this.tabMain,this.splitterV,this.pnlMain,this.ssMain, this.tsMain, this.msMain });
				#endregion
                Splash.Start(App.Product,Assembly.GetExecutingAssembly(),App.Copyright);
                Thread.Sleep(3000);

                //Create data and UI services
                this.mPageSettings = new PageSettings();
                this.mPageSettings.Landscape = true;
                this.mGridSvcZones = new UltraGridSvc(this.grdZones,this.txtSearchSort);
                this.mGridSvcTLs = new UltraGridSvc(this.grdTLs);
                this.mGridSvcLaneUpdates = new UltraGridSvc(this.grdLaneUpdates);
                this.mGridSvcZoneUpdates = new UltraGridSvc(this.grdZoneUpdates);
                this.mGridSvcShipSchedule = new UltraGridSvc(this.grdShipSchedule);
                this.mToolTip = new System.Windows.Forms.ToolTip();
                this.mMessageMgr = new MessageManager(this.ssMain.Panels[0],500,3000);
                TsortGateway.ZonesChanged += new EventHandler(OnZonesChanged);
                TsortGateway.TLsChanged += new EventHandler(OnTLsChanged);
                AgentLineHaulGateway.Changed += new EventHandler(OnShipScheduleChanged);
            }
			catch(Exception ex) { Splash.Close(); throw new ApplicationException("Startup Failure", ex); }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components!= null) components.Dispose(); } base.Dispose(disposing); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		/// 
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn153 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn162 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn163 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn161 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn154 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn169 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn170 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn155 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn156 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn165 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn166 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn167 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn164 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn157 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn158 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn159 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn160 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn168 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn171 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn172 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn173 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn182 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn183 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn181 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn174 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn189 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn190 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn175 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn176 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn185 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn186 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn187 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn184 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn177 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn178 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn179 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn180 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn188 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn191 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn192 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn193 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn202 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn203 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn201 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn194 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn209 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn210 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn195 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn196 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn205 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn206 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn207 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn204 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn197 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn198 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn199 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn200 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn208 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn211 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn212 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand4 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ZoneTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn213 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn222 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn223 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Type");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn221 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn214 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn229 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn230 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn215 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn216 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn225 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RollbackTL#");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn226 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsExclusive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn227 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CAN_BE_CLOSED");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn224 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn217 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn218 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn219 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NewSmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn220 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallSortLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn228 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedToShipScde");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn231 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn232 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand5 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleViewTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn233 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn234 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn235 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn236 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn237 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn238 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn239 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BolNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn240 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn241 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn242 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn243 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn244 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn245 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn246 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn247 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn248 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn249 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TractorNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn250 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn251 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn252 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn253 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn254 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn255 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn256 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn257 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Canceled");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn258 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDEUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn259 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDELastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn260 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDERowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn261 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn262 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn263 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn264 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn265 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn266 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn267 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn268 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn269 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn270 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn271 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn272 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn273 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn274 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn275 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn276 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn277 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn278 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn279 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn280 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn281 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn282 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn283 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn284 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn285 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Assignment");
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand6 = new Infragistics.Win.UltraWinGrid.UltraGridBand("Assignment", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn286 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn287 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn288 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn289 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn290 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn291 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn292 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn293 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn294 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn295 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn296 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn297 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn298 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopLastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn299 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopRowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn300 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn301 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn302 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn303 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedUser");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn304 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AssignedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn305 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn306 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn307 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn308 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.tsCancel = new System.Windows.Forms.Button();
            this.tsOK = new System.Windows.Forms.Button();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdTLs = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csZone = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csZoneAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.csZoneSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csZoneAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.mZones = new Argix.Freight.FreightDataset();
            this.grdZones = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.txtSearchSort = new System.Windows.Forms.TextBox();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabLanes = new System.Windows.Forms.TabPage();
            this.cboSmallLane = new System.Windows.Forms.ComboBox();
            this.mLanes = new Argix.Freight.FreightDataset();
            this.cboLane = new System.Windows.Forms.ComboBox();
            this.grdLaneUpdates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csTrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csTripRem = new System.Windows.Forms.ToolStripMenuItem();
            this.csTripSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csTripOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.csTripCloseAllTLs = new System.Windows.Forms.ToolStripMenuItem();
            this.csTripClose = new System.Windows.Forms.ToolStripMenuItem();
            this.csTripSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csTripUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.csTripMove = new System.Windows.Forms.ToolStripMenuItem();
            this.mUpdates = new Argix.Freight.FreightDataset();
            this.tabZones = new System.Windows.Forms.TabPage();
            this.grdZoneUpdates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabSchedule = new System.Windows.Forms.TabPage();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.grdShipSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mShipSchedule = new Argix.AgentLineHaul.ShipScheduleDataset();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.splitterV = new System.Windows.Forms.Splitter();
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
            this.msEditFind = new System.Windows.Forms.ToolStripMenuItem();
            this.msEditFindTL = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewZoneType = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewZoneTypeTsort = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewZoneTypeReturns = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewZoneTypeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msOp = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpRem = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpChangeLanes = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpCloseZones = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msOpOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpCloseAllTLs = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpClose = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msOpAssign = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpUnassign = new System.Windows.Forms.ToolStripMenuItem();
            this.msOpMove = new System.Windows.Forms.ToolStripMenuItem();
            this.msReports = new System.Windows.Forms.ToolStripMenuItem();
            this.msReportsZonesByLane = new System.Windows.Forms.ToolStripMenuItem();
            this.msTools = new System.Windows.Forms.ToolStripMenuItem();
            this.msToolsConfig = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsZoneType = new System.Windows.Forms.ToolStripDropDownButton();
            this.tsZoneTypeTsort = new System.Windows.Forms.ToolStripMenuItem();
            this.tsZoneTypeReturns = new System.Windows.Forms.ToolStripMenuItem();
            this.tsZoneTypeAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.tsOpen_ = new System.Windows.Forms.ToolStripButton();
            this.tsSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsPrint = new System.Windows.Forms.ToolStripButton();
            this.tsSearch = new System.Windows.Forms.ToolStripButton();
            this.tsSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.tsSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAdd = new System.Windows.Forms.ToolStripButton();
            this.tsRem = new System.Windows.Forms.ToolStripButton();
            this.tsChangeLanes = new System.Windows.Forms.ToolStripButton();
            this.tsCloseZones = new System.Windows.Forms.ToolStripButton();
            this.tsSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.tsClose = new System.Windows.Forms.ToolStripButton();
            this.tsAssign = new System.Windows.Forms.ToolStripButton();
            this.tsUnassign = new System.Windows.Forms.ToolStripButton();
            this.tsMove = new System.Windows.Forms.ToolStripButton();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).BeginInit();
            this.csZone.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mZones)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones)).BeginInit();
            this.grdZones.SuspendLayout();
            this.tabMain.SuspendLayout();
            this.tabLanes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mLanes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLaneUpdates)).BeginInit();
            this.csTrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mUpdates)).BeginInit();
            this.tabZones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdZoneUpdates)).BeginInit();
            this.tabSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdShipSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipSchedule)).BeginInit();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsCancel
            // 
            this.tsCancel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tsCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.tsCancel.ForeColor = System.Drawing.Color.Navy;
            this.tsCancel.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tsCancel.Location = new System.Drawing.Point(72, 256);
            this.tsCancel.Name = "tsCancel";
            this.tsCancel.Size = new System.Drawing.Size(64, 23);
            this.tsCancel.TabIndex = 153;
            this.tsCancel.Text = "&Cancel";
            this.tsCancel.UseVisualStyleBackColor = false;
            // 
            // tsOK
            // 
            this.tsOK.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tsOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.tsOK.Enabled = false;
            this.tsOK.ForeColor = System.Drawing.Color.Navy;
            this.tsOK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.tsOK.Location = new System.Drawing.Point(8, 256);
            this.tsOK.Name = "tsOK";
            this.tsOK.Size = new System.Drawing.Size(64, 23);
            this.tsOK.TabIndex = 152;
            this.tsOK.Text = "O&K";
            this.tsOK.UseVisualStyleBackColor = false;
            // 
            // pnlMain
            // 
            this.pnlMain.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlMain.Controls.Add(this.splitterH);
            this.pnlMain.Controls.Add(this.grdTLs);
            this.pnlMain.Controls.Add(this.grdZones);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMain.Location = new System.Drawing.Point(0, 73);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(272, 229);
            this.pnlMain.TabIndex = 2;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 89);
            this.splitterH.MinExtra = 48;
            this.splitterH.MinSize = 72;
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(268, 2);
            this.splitterH.TabIndex = 5;
            this.splitterH.TabStop = false;
            this.splitterH.Visible = false;
            // 
            // grdTLs
            // 
            this.grdTLs.AllowDrop = true;
            this.grdTLs.ContextMenuStrip = this.csZone;
            this.grdTLs.DataMember = "ZoneTable";
            this.grdTLs.DataSource = this.mZones;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Appearance = appearance1;
            ultraGridColumn153.Header.Caption = "Code";
            ultraGridColumn153.Header.Fixed = true;
            ultraGridColumn153.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn153.Header.VisiblePosition = 0;
            ultraGridColumn153.Width = 54;
            ultraGridColumn162.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn162.Header.VisiblePosition = 15;
            ultraGridColumn162.Hidden = true;
            ultraGridColumn162.Width = 72;
            ultraGridColumn163.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn163.Header.VisiblePosition = 11;
            ultraGridColumn163.Width = 72;
            ultraGridColumn161.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn161.Header.VisiblePosition = 9;
            ultraGridColumn161.Width = 144;
            ultraGridColumn154.Header.Fixed = true;
            ultraGridColumn154.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn154.Header.VisiblePosition = 2;
            ultraGridColumn154.Width = 84;
            ultraGridColumn169.Header.Caption = "Client#";
            ultraGridColumn169.Header.Fixed = true;
            ultraGridColumn169.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn169.Header.VisiblePosition = 1;
            ultraGridColumn169.Width = 60;
            ultraGridColumn170.Header.Caption = "Client";
            ultraGridColumn170.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn170.Header.VisiblePosition = 8;
            ultraGridColumn170.Width = 144;
            ultraGridColumn155.Format = "MMddyy";
            ultraGridColumn155.Header.Caption = "TL Date";
            ultraGridColumn155.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn155.Header.VisiblePosition = 3;
            ultraGridColumn155.Width = 60;
            ultraGridColumn156.Header.Caption = "Close#";
            ultraGridColumn156.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn156.Header.VisiblePosition = 4;
            ultraGridColumn156.Width = 51;
            ultraGridColumn165.Header.Caption = "Rollback TL#";
            ultraGridColumn165.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn165.Header.VisiblePosition = 13;
            ultraGridColumn165.Hidden = true;
            ultraGridColumn165.Width = 96;
            ultraGridColumn166.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn166.Header.VisiblePosition = 12;
            ultraGridColumn166.Hidden = true;
            ultraGridColumn166.Width = 72;
            ultraGridColumn167.Header.Caption = "Can Close";
            ultraGridColumn167.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn167.Header.VisiblePosition = 16;
            ultraGridColumn167.Width = 72;
            ultraGridColumn164.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn164.Header.VisiblePosition = 14;
            ultraGridColumn164.Width = 72;
            ultraGridColumn157.Header.VisiblePosition = 6;
            ultraGridColumn157.Hidden = true;
            ultraGridColumn158.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn158.Header.VisiblePosition = 5;
            ultraGridColumn158.Width = 60;
            ultraGridColumn159.Header.VisiblePosition = 10;
            ultraGridColumn159.Hidden = true;
            ultraGridColumn160.Header.Caption = "S. Lane";
            ultraGridColumn160.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn160.Header.VisiblePosition = 7;
            ultraGridColumn160.Width = 60;
            ultraGridColumn168.Header.Caption = "State";
            ultraGridColumn168.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn168.Header.VisiblePosition = 17;
            ultraGridColumn168.Width = 72;
            ultraGridColumn171.Header.Caption = "Agent";
            ultraGridColumn171.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn171.Header.VisiblePosition = 18;
            ultraGridColumn171.Hidden = true;
            ultraGridColumn171.Width = 72;
            ultraGridColumn172.Header.Caption = "Agent#";
            ultraGridColumn172.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn172.Header.VisiblePosition = 19;
            ultraGridColumn172.Width = 75;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn153,
            ultraGridColumn162,
            ultraGridColumn163,
            ultraGridColumn161,
            ultraGridColumn154,
            ultraGridColumn169,
            ultraGridColumn170,
            ultraGridColumn155,
            ultraGridColumn156,
            ultraGridColumn165,
            ultraGridColumn166,
            ultraGridColumn167,
            ultraGridColumn164,
            ultraGridColumn157,
            ultraGridColumn158,
            ultraGridColumn159,
            ultraGridColumn160,
            ultraGridColumn168,
            ultraGridColumn171,
            ultraGridColumn172});
            ultraGridBand1.SummaryFooterCaption = "Grand Summaries";
            this.grdTLs.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTLs.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.CaptionAppearance = appearance2;
            this.grdTLs.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTLs.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdTLs.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdTLs.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdTLs.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdTLs.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTLs.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTLs.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdTLs.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdTLs.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTLs.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTLs.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTLs.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTLs.DisplayLayout.UseFixedHeaders = true;
            this.grdTLs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdTLs.Location = new System.Drawing.Point(0, 91);
            this.grdTLs.Name = "grdTLs";
            this.grdTLs.Size = new System.Drawing.Size(268, 134);
            this.grdTLs.TabIndex = 6;
            this.grdTLs.Text = "TLs: Closed (Unassigned)";
            this.grdTLs.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLs.Visible = false;
            this.grdTLs.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTLSelectionChanged);
            this.grdTLs.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdTLs.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdTLs.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdTLs.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdTLs.DoubleClick += new System.EventHandler(this.OnTLDoubleClicked);
            this.grdTLs.Enter += new System.EventHandler(this.OnEnter);
            this.grdTLs.Leave += new System.EventHandler(this.OnLeave);
            this.grdTLs.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdTLs.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            this.grdTLs.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            // 
            // csZone
            // 
            this.csZone.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csZoneAdd,
            this.csZoneSep1,
            this.csZoneAssign});
            this.csZone.Name = "csZone";
            this.csZone.Size = new System.Drawing.Size(126, 54);
            // 
            // csZoneAdd
            // 
            this.csZoneAdd.Image = global::Argix.Properties.Resources.BuilderDialog_add;
            this.csZoneAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csZoneAdd.Name = "csZoneAdd";
            this.csZoneAdd.Size = new System.Drawing.Size(125, 22);
            this.csZoneAdd.Text = "Add";
            this.csZoneAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csZoneSep1
            // 
            this.csZoneSep1.Name = "csZoneSep1";
            this.csZoneSep1.Size = new System.Drawing.Size(122, 6);
            // 
            // csZoneAssign
            // 
            this.csZoneAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.csZoneAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csZoneAssign.Name = "csZoneAssign";
            this.csZoneAssign.Size = new System.Drawing.Size(125, 22);
            this.csZoneAssign.Text = "Assign TL";
            this.csZoneAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mZones
            // 
            this.mZones.DataSetName = "FreightDataset";
            this.mZones.Locale = new System.Globalization.CultureInfo("en-US");
            this.mZones.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grdZones
            // 
            this.grdZones.AllowDrop = true;
            this.grdZones.ContextMenuStrip = this.csZone;
            this.grdZones.Controls.Add(this.txtSearchSort);
            this.grdZones.DataMember = "ZoneTable";
            this.grdZones.DataSource = this.mZones;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdZones.DisplayLayout.Appearance = appearance5;
            ultraGridColumn173.Header.Caption = "Code";
            ultraGridColumn173.Header.Fixed = true;
            ultraGridColumn173.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn173.Header.VisiblePosition = 0;
            ultraGridColumn173.Width = 54;
            ultraGridColumn182.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn182.Header.VisiblePosition = 15;
            ultraGridColumn182.Hidden = true;
            ultraGridColumn182.Width = 72;
            ultraGridColumn183.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn183.Header.VisiblePosition = 11;
            ultraGridColumn183.Width = 72;
            ultraGridColumn181.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn181.Header.VisiblePosition = 9;
            ultraGridColumn181.Width = 144;
            ultraGridColumn174.Header.Fixed = true;
            ultraGridColumn174.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn174.Header.VisiblePosition = 2;
            ultraGridColumn174.Width = 84;
            ultraGridColumn189.Header.Caption = "Client#";
            ultraGridColumn189.Header.Fixed = true;
            ultraGridColumn189.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn189.Header.VisiblePosition = 1;
            ultraGridColumn189.Width = 60;
            ultraGridColumn190.Header.Caption = "Client";
            ultraGridColumn190.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn190.Header.VisiblePosition = 7;
            ultraGridColumn190.Width = 144;
            ultraGridColumn175.Format = "MMddyy";
            ultraGridColumn175.Header.Caption = "TL Date";
            ultraGridColumn175.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn175.Header.VisiblePosition = 3;
            ultraGridColumn175.Width = 60;
            ultraGridColumn176.Header.Caption = "Close#";
            ultraGridColumn176.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn176.Header.VisiblePosition = 4;
            ultraGridColumn176.Width = 51;
            ultraGridColumn185.Header.Caption = "Rollback TL#";
            ultraGridColumn185.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn185.Header.VisiblePosition = 13;
            ultraGridColumn185.Hidden = true;
            ultraGridColumn185.Width = 96;
            ultraGridColumn186.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn186.Header.VisiblePosition = 12;
            ultraGridColumn186.Hidden = true;
            ultraGridColumn186.Width = 72;
            ultraGridColumn187.Header.Caption = "Can Close";
            ultraGridColumn187.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn187.Header.VisiblePosition = 16;
            ultraGridColumn187.Width = 72;
            ultraGridColumn184.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn184.Header.VisiblePosition = 14;
            ultraGridColumn184.Width = 72;
            ultraGridColumn177.Header.VisiblePosition = 8;
            ultraGridColumn177.Hidden = true;
            ultraGridColumn178.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn178.Header.VisiblePosition = 5;
            ultraGridColumn178.Width = 60;
            ultraGridColumn179.Header.VisiblePosition = 10;
            ultraGridColumn179.Hidden = true;
            ultraGridColumn180.Header.Caption = "S. Lane";
            ultraGridColumn180.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn180.Header.VisiblePosition = 6;
            ultraGridColumn180.Width = 60;
            ultraGridColumn188.Header.Caption = "State";
            ultraGridColumn188.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn188.Header.VisiblePosition = 17;
            ultraGridColumn188.Width = 72;
            ultraGridColumn191.Header.Caption = "Agent";
            ultraGridColumn191.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn191.Header.VisiblePosition = 18;
            ultraGridColumn191.Hidden = true;
            ultraGridColumn191.Width = 72;
            ultraGridColumn192.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn192.Header.Caption = "Agent#";
            ultraGridColumn192.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn192.Header.VisiblePosition = 19;
            ultraGridColumn192.Width = 75;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn173,
            ultraGridColumn182,
            ultraGridColumn183,
            ultraGridColumn181,
            ultraGridColumn174,
            ultraGridColumn189,
            ultraGridColumn190,
            ultraGridColumn175,
            ultraGridColumn176,
            ultraGridColumn185,
            ultraGridColumn186,
            ultraGridColumn187,
            ultraGridColumn184,
            ultraGridColumn177,
            ultraGridColumn178,
            ultraGridColumn179,
            ultraGridColumn180,
            ultraGridColumn188,
            ultraGridColumn191,
            ultraGridColumn192});
            ultraGridBand2.SummaryFooterCaption = "Grand Summaries";
            this.grdZones.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdZones.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdZones.DisplayLayout.CaptionAppearance = appearance6;
            this.grdZones.DisplayLayout.MaxColScrollRegions = 1;
            this.grdZones.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdZones.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdZones.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdZones.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdZones.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdZones.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdZones.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdZones.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdZones.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdZones.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdZones.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdZones.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdZones.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdZones.DisplayLayout.UseFixedHeaders = true;
            this.grdZones.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdZones.Location = new System.Drawing.Point(0, 0);
            this.grdZones.Name = "grdZones";
            this.grdZones.Size = new System.Drawing.Size(268, 225);
            this.grdZones.TabIndex = 4;
            this.grdZones.Text = "TLs: Open (All)";
            this.grdZones.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdZones.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnZoneSelectionChanged);
            this.grdZones.BeforeSelectChange += new Infragistics.Win.UltraWinGrid.BeforeSelectChangeEventHandler(this.OnBeforeZoneSelectionChanged);
            this.grdZones.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdZones.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdZones.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdZones.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdZones.DoubleClick += new System.EventHandler(this.OnZoneDoubleClicked);
            this.grdZones.Enter += new System.EventHandler(this.OnEnter);
            this.grdZones.Leave += new System.EventHandler(this.OnLeave);
            this.grdZones.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdZones.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            this.grdZones.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            // 
            // txtSearchSort
            // 
            this.txtSearchSort.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSearchSort.Location = new System.Drawing.Point(184, 1);
            this.txtSearchSort.Name = "txtSearchSort";
            this.txtSearchSort.Size = new System.Drawing.Size(80, 20);
            this.txtSearchSort.TabIndex = 2;
            this.txtSearchSort.TextChanged += new System.EventHandler(this.OnSearchValueChanged);
            // 
            // tabMain
            // 
            this.tabMain.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabMain.Controls.Add(this.tabLanes);
            this.tabMain.Controls.Add(this.tabZones);
            this.tabMain.Controls.Add(this.tabSchedule);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.ItemSize = new System.Drawing.Size(93, 18);
            this.tabMain.Location = new System.Drawing.Point(275, 73);
            this.tabMain.Margin = new System.Windows.Forms.Padding(0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(0, 0);
            this.tabMain.SelectedIndex = 0;
            this.tabMain.ShowToolTips = true;
            this.tabMain.Size = new System.Drawing.Size(641, 229);
            this.tabMain.TabIndex = 10;
            this.tabMain.SelectedIndexChanged += new System.EventHandler(this.OnTabSelectedIndexChanged);
            // 
            // tabLanes
            // 
            this.tabLanes.Controls.Add(this.cboSmallLane);
            this.tabLanes.Controls.Add(this.cboLane);
            this.tabLanes.Controls.Add(this.grdLaneUpdates);
            this.tabLanes.Location = new System.Drawing.Point(4, 4);
            this.tabLanes.Margin = new System.Windows.Forms.Padding(0);
            this.tabLanes.Name = "tabLanes";
            this.tabLanes.Size = new System.Drawing.Size(633, 203);
            this.tabLanes.TabIndex = 0;
            this.tabLanes.Text = "Change Lanes";
            this.tabLanes.ToolTipText = "Change lanes only";
            // 
            // cboSmallLane
            // 
            this.cboSmallLane.DataSource = this.mLanes;
            this.cboSmallLane.DisplayMember = "SmallLaneTable.SmallSortLane";
            this.cboSmallLane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSmallLane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSmallLane.ItemHeight = 13;
            this.cboSmallLane.Location = new System.Drawing.Point(230, 0);
            this.cboSmallLane.Margin = new System.Windows.Forms.Padding(0);
            this.cboSmallLane.Name = "cboSmallLane";
            this.cboSmallLane.Size = new System.Drawing.Size(50, 21);
            this.cboSmallLane.TabIndex = 8;
            this.cboSmallLane.ValueMember = "SmallLaneTable.SmallSortLane";
            this.cboSmallLane.SelectionChangeCommitted += new System.EventHandler(this.OnSmallLaneSelected);
            // 
            // mLanes
            // 
            this.mLanes.DataSetName = "FreightDataset";
            this.mLanes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboLane
            // 
            this.cboLane.DataSource = this.mLanes;
            this.cboLane.DisplayMember = "LaneTable.Lane";
            this.cboLane.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLane.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboLane.ItemHeight = 13;
            this.cboLane.Location = new System.Drawing.Point(175, 0);
            this.cboLane.Margin = new System.Windows.Forms.Padding(0);
            this.cboLane.Name = "cboLane";
            this.cboLane.Size = new System.Drawing.Size(50, 21);
            this.cboLane.TabIndex = 6;
            this.cboLane.ValueMember = "LaneTable.Lane";
            this.cboLane.SelectionChangeCommitted += new System.EventHandler(this.OnLaneSelected);
            // 
            // grdLaneUpdates
            // 
            this.grdLaneUpdates.AllowDrop = true;
            this.grdLaneUpdates.ContextMenuStrip = this.csTrip;
            this.grdLaneUpdates.DataMember = "ZoneTable";
            this.grdLaneUpdates.DataSource = this.mUpdates;
            appearance9.BackColor = System.Drawing.SystemColors.Window;
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance9.TextHAlignAsString = "Left";
            this.grdLaneUpdates.DisplayLayout.Appearance = appearance9;
            ultraGridBand3.AddButtonCaption = "TLViewTable";
            ultraGridColumn193.Header.Caption = "Code";
            ultraGridColumn193.Header.VisiblePosition = 0;
            ultraGridColumn193.Width = 54;
            ultraGridColumn202.Header.VisiblePosition = 17;
            ultraGridColumn202.Hidden = true;
            ultraGridColumn202.Width = 72;
            ultraGridColumn203.Header.VisiblePosition = 12;
            ultraGridColumn203.Width = 72;
            ultraGridColumn201.Header.VisiblePosition = 11;
            ultraGridColumn201.Width = 144;
            ultraGridColumn194.Header.VisiblePosition = 2;
            ultraGridColumn194.Width = 84;
            ultraGridColumn209.Header.Caption = "Client#";
            ultraGridColumn209.Header.VisiblePosition = 1;
            ultraGridColumn209.Width = 60;
            ultraGridColumn210.Header.Caption = "Client";
            ultraGridColumn210.Header.VisiblePosition = 10;
            ultraGridColumn210.Width = 144;
            ultraGridColumn195.Format = "MMddyy";
            ultraGridColumn195.Header.Caption = "TL Date";
            ultraGridColumn195.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn195.Header.VisiblePosition = 3;
            ultraGridColumn195.Width = 60;
            ultraGridColumn196.Header.Caption = "Close#";
            ultraGridColumn196.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn196.Header.VisiblePosition = 4;
            ultraGridColumn196.Width = 51;
            ultraGridColumn205.Header.Caption = "Rollback TL#";
            ultraGridColumn205.Header.VisiblePosition = 14;
            ultraGridColumn205.Hidden = true;
            ultraGridColumn205.Width = 96;
            ultraGridColumn206.Header.VisiblePosition = 13;
            ultraGridColumn206.Hidden = true;
            ultraGridColumn206.Width = 72;
            ultraGridColumn207.Header.VisiblePosition = 18;
            ultraGridColumn207.Hidden = true;
            ultraGridColumn207.Width = 72;
            ultraGridColumn204.Header.VisiblePosition = 16;
            ultraGridColumn204.Width = 72;
            ultraGridColumn197.Header.Caption = "New Lane";
            ultraGridColumn197.Header.VisiblePosition = 5;
            ultraGridColumn197.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn197.Width = 60;
            ultraGridColumn198.Header.VisiblePosition = 6;
            ultraGridColumn198.Width = 60;
            ultraGridColumn199.Header.Caption = "New S. Lane";
            ultraGridColumn199.Header.VisiblePosition = 7;
            ultraGridColumn199.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn199.Width = 60;
            ultraGridColumn200.Header.Caption = "S. Lane";
            ultraGridColumn200.Header.VisiblePosition = 8;
            ultraGridColumn200.Width = 60;
            ultraGridColumn208.Header.VisiblePosition = 15;
            ultraGridColumn208.Hidden = true;
            ultraGridColumn211.Header.Caption = "Agent";
            ultraGridColumn211.Header.VisiblePosition = 9;
            ultraGridColumn211.Hidden = true;
            ultraGridColumn212.Header.VisiblePosition = 19;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn193,
            ultraGridColumn202,
            ultraGridColumn203,
            ultraGridColumn201,
            ultraGridColumn194,
            ultraGridColumn209,
            ultraGridColumn210,
            ultraGridColumn195,
            ultraGridColumn196,
            ultraGridColumn205,
            ultraGridColumn206,
            ultraGridColumn207,
            ultraGridColumn204,
            ultraGridColumn197,
            ultraGridColumn198,
            ultraGridColumn199,
            ultraGridColumn200,
            ultraGridColumn208,
            ultraGridColumn211,
            ultraGridColumn212});
            ultraGridBand3.SummaryFooterCaption = "Grand Summaries";
            this.grdLaneUpdates.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdLaneUpdates.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance10.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance10.FontData.BoldAsString = "True";
            appearance10.FontData.SizeInPoints = 8F;
            appearance10.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance10.TextHAlignAsString = "Left";
            this.grdLaneUpdates.DisplayLayout.CaptionAppearance = appearance10;
            this.grdLaneUpdates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdLaneUpdates.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdLaneUpdates.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdLaneUpdates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance11.BackColor = System.Drawing.SystemColors.Control;
            appearance11.FontData.BoldAsString = "True";
            appearance11.FontData.SizeInPoints = 8F;
            appearance11.TextHAlignAsString = "Left";
            this.grdLaneUpdates.DisplayLayout.Override.HeaderAppearance = appearance11;
            this.grdLaneUpdates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdLaneUpdates.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance12.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdLaneUpdates.DisplayLayout.Override.RowAppearance = appearance12;
            this.grdLaneUpdates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdLaneUpdates.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdLaneUpdates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdLaneUpdates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdLaneUpdates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdLaneUpdates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdLaneUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdLaneUpdates.Location = new System.Drawing.Point(0, 0);
            this.grdLaneUpdates.Margin = new System.Windows.Forms.Padding(0);
            this.grdLaneUpdates.Name = "grdLaneUpdates";
            this.grdLaneUpdates.Size = new System.Drawing.Size(633, 203);
            this.grdLaneUpdates.TabIndex = 4;
            this.grdLaneUpdates.Text = "Change Lanes Only";
            this.grdLaneUpdates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdLaneUpdates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnUpdateSelectionChanged);
            this.grdLaneUpdates.AfterColPosChanged += new Infragistics.Win.UltraWinGrid.AfterColPosChangedEventHandler(this.OnUpdateColumnHeaderResized);
            this.grdLaneUpdates.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdLaneUpdates.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdLaneUpdates.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdLaneUpdates.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdLaneUpdates.DoubleClick += new System.EventHandler(this.OnUpdateDoubleClicked);
            this.grdLaneUpdates.Enter += new System.EventHandler(this.OnEnter);
            this.grdLaneUpdates.Leave += new System.EventHandler(this.OnLeave);
            this.grdLaneUpdates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdLaneUpdates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            this.grdLaneUpdates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            // 
            // csTrip
            // 
            this.csTrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csTripRem,
            this.csTripSep1,
            this.csTripOpen,
            this.csTripCloseAllTLs,
            this.csTripClose,
            this.csTripSep2,
            this.csTripUnassign,
            this.csTripMove});
            this.csTrip.Name = "csTrip";
            this.csTrip.Size = new System.Drawing.Size(142, 148);
            // 
            // csTripRem
            // 
            this.csTripRem.Image = global::Argix.Properties.Resources.BuilderDialog_remove;
            this.csTripRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTripRem.Name = "csTripRem";
            this.csTripRem.Size = new System.Drawing.Size(141, 22);
            this.csTripRem.Text = "Remove";
            this.csTripRem.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csTripSep1
            // 
            this.csTripSep1.Name = "csTripSep1";
            this.csTripSep1.Size = new System.Drawing.Size(138, 6);
            // 
            // csTripOpen
            // 
            this.csTripOpen.Image = global::Argix.Properties.Resources.book_open;
            this.csTripOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTripOpen.Name = "csTripOpen";
            this.csTripOpen.Size = new System.Drawing.Size(141, 22);
            this.csTripOpen.Text = "Open Trip";
            this.csTripOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csTripCloseAllTLs
            // 
            this.csTripCloseAllTLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTripCloseAllTLs.Name = "csTripCloseAllTLs";
            this.csTripCloseAllTLs.Size = new System.Drawing.Size(141, 22);
            this.csTripCloseAllTLs.Text = "Close All TLs";
            this.csTripCloseAllTLs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csTripClose
            // 
            this.csTripClose.Image = global::Argix.Properties.Resources.book_angle;
            this.csTripClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTripClose.Name = "csTripClose";
            this.csTripClose.Size = new System.Drawing.Size(141, 22);
            this.csTripClose.Text = "Close Trip";
            this.csTripClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csTripSep2
            // 
            this.csTripSep2.Name = "csTripSep2";
            this.csTripSep2.Size = new System.Drawing.Size(138, 6);
            // 
            // csTripUnassign
            // 
            this.csTripUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.csTripUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTripUnassign.Name = "csTripUnassign";
            this.csTripUnassign.Size = new System.Drawing.Size(141, 22);
            this.csTripUnassign.Text = "Unassign";
            this.csTripUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csTripMove
            // 
            this.csTripMove.Image = global::Argix.Properties.Resources.MoveToFolder;
            this.csTripMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csTripMove.Name = "csTripMove";
            this.csTripMove.Size = new System.Drawing.Size(141, 22);
            this.csTripMove.Text = "Move";
            this.csTripMove.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mUpdates
            // 
            this.mUpdates.DataSetName = "FreightDataset";
            this.mUpdates.Locale = new System.Globalization.CultureInfo("en-US");
            this.mUpdates.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabZones
            // 
            this.tabZones.Controls.Add(this.grdZoneUpdates);
            this.tabZones.Location = new System.Drawing.Point(4, 4);
            this.tabZones.Margin = new System.Windows.Forms.Padding(0);
            this.tabZones.Name = "tabZones";
            this.tabZones.Size = new System.Drawing.Size(633, 203);
            this.tabZones.TabIndex = 1;
            this.tabZones.Text = "Close Zones/Change Lanes";
            this.tabZones.ToolTipText = "Close zones AND change lanes";
            // 
            // grdZoneUpdates
            // 
            this.grdZoneUpdates.AllowDrop = true;
            this.grdZoneUpdates.ContextMenuStrip = this.csTrip;
            this.grdZoneUpdates.DataMember = "ZoneTable";
            this.grdZoneUpdates.DataSource = this.mUpdates;
            appearance13.BackColor = System.Drawing.SystemColors.Info;
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdZoneUpdates.DisplayLayout.Appearance = appearance13;
            ultraGridBand4.AddButtonCaption = "TLViewTable";
            ultraGridColumn213.Header.Caption = "Code";
            ultraGridColumn213.Header.VisiblePosition = 0;
            ultraGridColumn213.Width = 54;
            ultraGridColumn222.Header.VisiblePosition = 17;
            ultraGridColumn222.Hidden = true;
            ultraGridColumn222.Width = 72;
            ultraGridColumn223.Header.VisiblePosition = 12;
            ultraGridColumn223.Width = 72;
            ultraGridColumn221.Header.VisiblePosition = 11;
            ultraGridColumn221.Width = 144;
            ultraGridColumn214.Header.VisiblePosition = 2;
            ultraGridColumn214.Width = 84;
            ultraGridColumn229.Header.Caption = "Client#";
            ultraGridColumn229.Header.VisiblePosition = 1;
            ultraGridColumn229.Width = 60;
            ultraGridColumn230.Header.Caption = "Client";
            ultraGridColumn230.Header.VisiblePosition = 10;
            ultraGridColumn230.Width = 144;
            ultraGridColumn215.Format = "MMddyy";
            ultraGridColumn215.Header.Caption = "TL Date";
            ultraGridColumn215.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn215.Header.VisiblePosition = 3;
            ultraGridColumn215.Width = 60;
            ultraGridColumn216.Header.Caption = "Close#";
            ultraGridColumn216.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            ultraGridColumn216.Header.VisiblePosition = 4;
            ultraGridColumn216.Width = 51;
            ultraGridColumn225.Header.Caption = "Rollback TL#";
            ultraGridColumn225.Header.VisiblePosition = 14;
            ultraGridColumn225.Hidden = true;
            ultraGridColumn225.Width = 96;
            ultraGridColumn226.Header.VisiblePosition = 13;
            ultraGridColumn226.Hidden = true;
            ultraGridColumn226.Width = 72;
            ultraGridColumn227.Header.VisiblePosition = 18;
            ultraGridColumn227.Hidden = true;
            ultraGridColumn227.Width = 72;
            ultraGridColumn224.Header.VisiblePosition = 16;
            ultraGridColumn224.Width = 72;
            ultraGridColumn217.Header.Caption = "New Lane";
            ultraGridColumn217.Header.VisiblePosition = 5;
            ultraGridColumn217.Width = 60;
            ultraGridColumn218.Header.VisiblePosition = 6;
            ultraGridColumn218.Width = 60;
            ultraGridColumn219.Header.Caption = "New S. Lane";
            ultraGridColumn219.Header.VisiblePosition = 7;
            ultraGridColumn219.Width = 60;
            ultraGridColumn220.Header.Caption = "S. Lane";
            ultraGridColumn220.Header.VisiblePosition = 8;
            ultraGridColumn220.Width = 60;
            ultraGridColumn228.Header.VisiblePosition = 15;
            ultraGridColumn228.Hidden = true;
            ultraGridColumn231.Header.Caption = "Agent";
            ultraGridColumn231.Header.VisiblePosition = 9;
            ultraGridColumn231.Hidden = true;
            ultraGridColumn232.Header.VisiblePosition = 19;
            ultraGridBand4.Columns.AddRange(new object[] {
            ultraGridColumn213,
            ultraGridColumn222,
            ultraGridColumn223,
            ultraGridColumn221,
            ultraGridColumn214,
            ultraGridColumn229,
            ultraGridColumn230,
            ultraGridColumn215,
            ultraGridColumn216,
            ultraGridColumn225,
            ultraGridColumn226,
            ultraGridColumn227,
            ultraGridColumn224,
            ultraGridColumn217,
            ultraGridColumn218,
            ultraGridColumn219,
            ultraGridColumn220,
            ultraGridColumn228,
            ultraGridColumn231,
            ultraGridColumn232});
            ultraGridBand4.SummaryFooterCaption = "Grand Summaries";
            this.grdZoneUpdates.DisplayLayout.BandsSerializer.Add(ultraGridBand4);
            this.grdZoneUpdates.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdZoneUpdates.DisplayLayout.CaptionAppearance = appearance14;
            this.grdZoneUpdates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdZoneUpdates.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdZoneUpdates.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdZoneUpdates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdZoneUpdates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            appearance15.BackColor = System.Drawing.SystemColors.Info;
            this.grdZoneUpdates.DisplayLayout.Override.CellAppearance = appearance15;
            this.grdZoneUpdates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance16.BackColor = System.Drawing.SystemColors.Control;
            appearance16.FontData.BoldAsString = "True";
            appearance16.FontData.SizeInPoints = 8F;
            appearance16.TextHAlignAsString = "Left";
            this.grdZoneUpdates.DisplayLayout.Override.HeaderAppearance = appearance16;
            this.grdZoneUpdates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdZoneUpdates.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance17.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdZoneUpdates.DisplayLayout.Override.RowAppearance = appearance17;
            this.grdZoneUpdates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdZoneUpdates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdZoneUpdates.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdZoneUpdates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdZoneUpdates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdZoneUpdates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdZoneUpdates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdZoneUpdates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdZoneUpdates.Location = new System.Drawing.Point(0, 0);
            this.grdZoneUpdates.Margin = new System.Windows.Forms.Padding(0);
            this.grdZoneUpdates.Name = "grdZoneUpdates";
            this.grdZoneUpdates.Size = new System.Drawing.Size(633, 203);
            this.grdZoneUpdates.TabIndex = 5;
            this.grdZoneUpdates.Text = "Close Zones/Change Lanes";
            this.grdZoneUpdates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdZoneUpdates.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnUpdateSelectionChanged);
            this.grdZoneUpdates.AfterColPosChanged += new Infragistics.Win.UltraWinGrid.AfterColPosChangedEventHandler(this.OnUpdateColumnHeaderResized);
            this.grdZoneUpdates.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdZoneUpdates.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdZoneUpdates.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdZoneUpdates.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdZoneUpdates.DoubleClick += new System.EventHandler(this.OnUpdateDoubleClicked);
            this.grdZoneUpdates.Enter += new System.EventHandler(this.OnEnter);
            this.grdZoneUpdates.Leave += new System.EventHandler(this.OnLeave);
            this.grdZoneUpdates.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdZoneUpdates.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            this.grdZoneUpdates.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            // 
            // tabSchedule
            // 
            this.tabSchedule.Controls.Add(this.dtpScheduleDate);
            this.tabSchedule.Controls.Add(this.grdShipSchedule);
            this.tabSchedule.Location = new System.Drawing.Point(4, 4);
            this.tabSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.tabSchedule.Name = "tabSchedule";
            this.tabSchedule.Size = new System.Drawing.Size(633, 203);
            this.tabSchedule.TabIndex = 2;
            this.tabSchedule.Text = "Ship Schedule";
            this.tabSchedule.ToolTipText = "Assign to ship schedule";
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dtpScheduleDate.CustomFormat = "MMMM dd, yyyy";
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduleDate.Location = new System.Drawing.Point(202, 0);
            this.dtpScheduleDate.MaxDate = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.dtpScheduleDate.MinDate = new System.DateTime(2005, 1, 1, 0, 0, 0, 0);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(148, 20);
            this.dtpScheduleDate.TabIndex = 1;
            this.dtpScheduleDate.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.OnScheduleDateChanged);
            this.dtpScheduleDate.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // grdShipSchedule
            // 
            this.grdShipSchedule.ContextMenuStrip = this.csTrip;
            this.grdShipSchedule.DataMember = "ShipScheduleViewTable";
            this.grdShipSchedule.DataSource = this.mShipSchedule;
            appearance18.BackColor = System.Drawing.SystemColors.Window;
            appearance18.FontData.SizeInPoints = 8F;
            appearance18.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance18.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.Appearance = appearance18;
            ultraGridColumn233.Header.VisiblePosition = 0;
            ultraGridColumn233.Hidden = true;
            ultraGridColumn234.Header.VisiblePosition = 1;
            ultraGridColumn234.Hidden = true;
            ultraGridColumn235.Header.VisiblePosition = 2;
            ultraGridColumn235.Hidden = true;
            ultraGridColumn236.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn236.Header.VisiblePosition = 3;
            ultraGridColumn236.Hidden = true;
            ultraGridColumn236.Width = 120;
            ultraGridColumn237.Header.VisiblePosition = 4;
            ultraGridColumn237.Hidden = true;
            ultraGridColumn237.Width = 96;
            ultraGridColumn238.Header.VisiblePosition = 20;
            ultraGridColumn238.Hidden = true;
            ultraGridColumn239.Header.VisiblePosition = 21;
            ultraGridColumn239.Hidden = true;
            ultraGridColumn240.Header.VisiblePosition = 22;
            ultraGridColumn240.Hidden = true;
            ultraGridColumn241.Header.VisiblePosition = 50;
            ultraGridColumn241.Hidden = true;
            ultraGridColumn242.Header.VisiblePosition = 11;
            ultraGridColumn242.Width = 144;
            ultraGridColumn243.Header.VisiblePosition = 49;
            ultraGridColumn243.Hidden = true;
            ultraGridColumn244.Header.Caption = "Client#";
            ultraGridColumn244.Header.VisiblePosition = 6;
            ultraGridColumn244.Width = 60;
            ultraGridColumn245.Header.VisiblePosition = 51;
            ultraGridColumn245.Hidden = true;
            ultraGridColumn246.Header.Caption = "Load#";
            ultraGridColumn246.Header.VisiblePosition = 15;
            ultraGridColumn246.Width = 112;
            ultraGridColumn247.Header.VisiblePosition = 23;
            ultraGridColumn247.Hidden = true;
            ultraGridColumn248.Header.Caption = "Trailer#";
            ultraGridColumn248.Header.VisiblePosition = 10;
            ultraGridColumn248.Width = 100;
            ultraGridColumn249.Header.VisiblePosition = 24;
            ultraGridColumn249.Hidden = true;
            ultraGridColumn250.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn250.Header.Caption = "Sched Close";
            ultraGridColumn250.Header.VisiblePosition = 9;
            ultraGridColumn250.Width = 120;
            ultraGridColumn251.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn251.Header.Caption = "Sched Depart";
            ultraGridColumn251.Header.VisiblePosition = 12;
            ultraGridColumn251.Width = 144;
            ultraGridColumn252.Header.VisiblePosition = 25;
            ultraGridColumn252.Hidden = true;
            ultraGridColumn253.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn253.Header.Caption = "All Assigned";
            ultraGridColumn253.Header.VisiblePosition = 17;
            ultraGridColumn253.Hidden = true;
            ultraGridColumn253.NullText = "";
            ultraGridColumn253.Width = 48;
            ultraGridColumn254.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn254.Header.Caption = "Complete";
            ultraGridColumn254.Header.VisiblePosition = 16;
            ultraGridColumn254.Hidden = true;
            ultraGridColumn254.NullText = "";
            ultraGridColumn254.Width = 48;
            ultraGridColumn255.Header.VisiblePosition = 26;
            ultraGridColumn255.Hidden = true;
            ultraGridColumn255.Width = 120;
            ultraGridColumn256.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn256.Header.VisiblePosition = 27;
            ultraGridColumn256.Hidden = true;
            ultraGridColumn256.Width = 120;
            ultraGridColumn257.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn257.Header.VisiblePosition = 28;
            ultraGridColumn257.Hidden = true;
            ultraGridColumn258.Header.VisiblePosition = 29;
            ultraGridColumn258.Hidden = true;
            ultraGridColumn259.Header.VisiblePosition = 30;
            ultraGridColumn259.Hidden = true;
            ultraGridColumn260.Header.VisiblePosition = 31;
            ultraGridColumn260.Hidden = true;
            ultraGridColumn261.Header.VisiblePosition = 32;
            ultraGridColumn261.Hidden = true;
            ultraGridColumn261.Width = 96;
            ultraGridColumn262.Header.Caption = "Stop#";
            ultraGridColumn262.Header.VisiblePosition = 33;
            ultraGridColumn262.Hidden = true;
            ultraGridColumn262.Width = 72;
            ultraGridColumn263.Header.Caption = "AgentID";
            ultraGridColumn263.Header.VisiblePosition = 18;
            ultraGridColumn263.Hidden = true;
            ultraGridColumn264.Header.Caption = "Agent#";
            ultraGridColumn264.Header.VisiblePosition = 19;
            ultraGridColumn264.Hidden = true;
            ultraGridColumn264.Width = 72;
            ultraGridColumn265.Header.Caption = "Zone";
            ultraGridColumn265.Header.VisiblePosition = 5;
            ultraGridColumn265.Width = 72;
            ultraGridColumn266.Header.VisiblePosition = 7;
            ultraGridColumn266.Width = 72;
            ultraGridColumn267.Header.VisiblePosition = 13;
            ultraGridColumn267.Width = 112;
            ultraGridColumn268.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn268.Header.VisiblePosition = 34;
            ultraGridColumn268.Hidden = true;
            ultraGridColumn268.Width = 120;
            ultraGridColumn269.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn269.Header.VisiblePosition = 35;
            ultraGridColumn269.Hidden = true;
            ultraGridColumn269.Width = 120;
            ultraGridColumn270.Header.VisiblePosition = 36;
            ultraGridColumn270.Hidden = true;
            ultraGridColumn271.Header.VisiblePosition = 37;
            ultraGridColumn271.Hidden = true;
            ultraGridColumn272.Header.VisiblePosition = 38;
            ultraGridColumn272.Hidden = true;
            ultraGridColumn273.Header.VisiblePosition = 39;
            ultraGridColumn273.Hidden = true;
            ultraGridColumn274.Header.Caption = "S2 Stop#";
            ultraGridColumn274.Header.VisiblePosition = 40;
            ultraGridColumn274.Hidden = true;
            ultraGridColumn274.Width = 72;
            ultraGridColumn275.Header.Caption = "S2AgentlID";
            ultraGridColumn275.Header.VisiblePosition = 41;
            ultraGridColumn275.Hidden = true;
            ultraGridColumn275.Width = 96;
            ultraGridColumn276.Header.VisiblePosition = 42;
            ultraGridColumn276.Hidden = true;
            ultraGridColumn277.Header.Caption = "S2 Main Zone";
            ultraGridColumn277.Header.VisiblePosition = 43;
            ultraGridColumn277.Hidden = true;
            ultraGridColumn277.Width = 96;
            ultraGridColumn278.Header.Caption = "S2 Tag";
            ultraGridColumn278.Header.VisiblePosition = 8;
            ultraGridColumn278.Width = 60;
            ultraGridColumn279.Header.Caption = "S2 Notes";
            ultraGridColumn279.Header.VisiblePosition = 14;
            ultraGridColumn279.Width = 120;
            ultraGridColumn280.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn280.Header.VisiblePosition = 44;
            ultraGridColumn280.Hidden = true;
            ultraGridColumn280.Width = 120;
            ultraGridColumn281.Format = "MM/dd/yyyy hh:mmtt";
            ultraGridColumn281.Header.VisiblePosition = 45;
            ultraGridColumn281.Hidden = true;
            ultraGridColumn281.Width = 120;
            ultraGridColumn282.Header.VisiblePosition = 46;
            ultraGridColumn282.Hidden = true;
            ultraGridColumn283.Header.VisiblePosition = 47;
            ultraGridColumn283.Hidden = true;
            ultraGridColumn284.Header.VisiblePosition = 48;
            ultraGridColumn284.Hidden = true;
            ultraGridColumn285.Header.VisiblePosition = 52;
            ultraGridBand5.Columns.AddRange(new object[] {
            ultraGridColumn233,
            ultraGridColumn234,
            ultraGridColumn235,
            ultraGridColumn236,
            ultraGridColumn237,
            ultraGridColumn238,
            ultraGridColumn239,
            ultraGridColumn240,
            ultraGridColumn241,
            ultraGridColumn242,
            ultraGridColumn243,
            ultraGridColumn244,
            ultraGridColumn245,
            ultraGridColumn246,
            ultraGridColumn247,
            ultraGridColumn248,
            ultraGridColumn249,
            ultraGridColumn250,
            ultraGridColumn251,
            ultraGridColumn252,
            ultraGridColumn253,
            ultraGridColumn254,
            ultraGridColumn255,
            ultraGridColumn256,
            ultraGridColumn257,
            ultraGridColumn258,
            ultraGridColumn259,
            ultraGridColumn260,
            ultraGridColumn261,
            ultraGridColumn262,
            ultraGridColumn263,
            ultraGridColumn264,
            ultraGridColumn265,
            ultraGridColumn266,
            ultraGridColumn267,
            ultraGridColumn268,
            ultraGridColumn269,
            ultraGridColumn270,
            ultraGridColumn271,
            ultraGridColumn272,
            ultraGridColumn273,
            ultraGridColumn274,
            ultraGridColumn275,
            ultraGridColumn276,
            ultraGridColumn277,
            ultraGridColumn278,
            ultraGridColumn279,
            ultraGridColumn280,
            ultraGridColumn281,
            ultraGridColumn282,
            ultraGridColumn283,
            ultraGridColumn284,
            ultraGridColumn285});
            ultraGridBand5.SummaryFooterCaption = "Grand Summaries";
            ultraGridColumn286.Header.Caption = "TL#";
            ultraGridColumn286.Header.VisiblePosition = 4;
            ultraGridColumn286.Width = 72;
            ultraGridColumn287.Header.VisiblePosition = 0;
            ultraGridColumn287.Hidden = true;
            ultraGridColumn288.Header.VisiblePosition = 1;
            ultraGridColumn288.Hidden = true;
            ultraGridColumn289.Header.Caption = "Stop#";
            ultraGridColumn289.Header.VisiblePosition = 10;
            ultraGridColumn290.Header.VisiblePosition = 14;
            ultraGridColumn290.Hidden = true;
            ultraGridColumn291.Header.VisiblePosition = 15;
            ultraGridColumn291.Hidden = true;
            ultraGridColumn292.Header.VisiblePosition = 16;
            ultraGridColumn292.Hidden = true;
            ultraGridColumn293.Header.VisiblePosition = 6;
            ultraGridColumn293.Width = 60;
            ultraGridColumn294.Header.VisiblePosition = 12;
            ultraGridColumn295.Format = "MM/dd/yyyy HH:mm tt";
            ultraGridColumn295.Header.Caption = "Sched Arrival";
            ultraGridColumn295.Header.VisiblePosition = 11;
            ultraGridColumn296.Format = "MM/dd/yyyy HH:mm tt";
            ultraGridColumn296.Header.Caption = "Sched OFD1";
            ultraGridColumn296.Header.VisiblePosition = 13;
            ultraGridColumn297.Header.VisiblePosition = 17;
            ultraGridColumn297.Hidden = true;
            ultraGridColumn298.Header.VisiblePosition = 18;
            ultraGridColumn298.Hidden = true;
            ultraGridColumn299.Header.VisiblePosition = 19;
            ultraGridColumn299.Hidden = true;
            ultraGridColumn300.Header.VisiblePosition = 2;
            ultraGridColumn300.Width = 53;
            ultraGridColumn301.Format = "MM/dd/yyyy";
            ultraGridColumn301.Header.Caption = "Close Date";
            ultraGridColumn301.Header.VisiblePosition = 7;
            ultraGridColumn302.Format = "HH:mm tt";
            ultraGridColumn302.Header.Caption = "Close Time";
            ultraGridColumn302.Header.VisiblePosition = 8;
            ultraGridColumn303.Header.VisiblePosition = 20;
            ultraGridColumn303.Hidden = true;
            ultraGridColumn304.Header.VisiblePosition = 21;
            ultraGridColumn304.Hidden = true;
            ultraGridColumn305.Header.VisiblePosition = 5;
            ultraGridColumn305.Hidden = true;
            ultraGridColumn306.Header.VisiblePosition = 9;
            ultraGridColumn306.Hidden = true;
            ultraGridColumn307.Header.Caption = "Client#";
            ultraGridColumn307.Header.VisiblePosition = 3;
            ultraGridColumn307.Width = 60;
            ultraGridColumn308.Header.VisiblePosition = 22;
            ultraGridColumn308.Hidden = true;
            ultraGridBand6.Columns.AddRange(new object[] {
            ultraGridColumn286,
            ultraGridColumn287,
            ultraGridColumn288,
            ultraGridColumn289,
            ultraGridColumn290,
            ultraGridColumn291,
            ultraGridColumn292,
            ultraGridColumn293,
            ultraGridColumn294,
            ultraGridColumn295,
            ultraGridColumn296,
            ultraGridColumn297,
            ultraGridColumn298,
            ultraGridColumn299,
            ultraGridColumn300,
            ultraGridColumn301,
            ultraGridColumn302,
            ultraGridColumn303,
            ultraGridColumn304,
            ultraGridColumn305,
            ultraGridColumn306,
            ultraGridColumn307,
            ultraGridColumn308});
            this.grdShipSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand5);
            this.grdShipSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand6);
            this.grdShipSchedule.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Etched;
            appearance19.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance19.FontData.BoldAsString = "True";
            appearance19.FontData.SizeInPoints = 8F;
            appearance19.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance19.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.CaptionAppearance = appearance19;
            this.grdShipSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdShipSchedule.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdShipSchedule.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdShipSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance20.BackColor = System.Drawing.SystemColors.Control;
            appearance20.FontData.BoldAsString = "True";
            appearance20.FontData.SizeInPoints = 8F;
            appearance20.TextHAlignAsString = "Left";
            this.grdShipSchedule.DisplayLayout.Override.HeaderAppearance = appearance20;
            this.grdShipSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdShipSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance21.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdShipSchedule.DisplayLayout.Override.RowAppearance = appearance21;
            this.grdShipSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdShipSchedule.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.grdShipSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdShipSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdShipSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdShipSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdShipSchedule.Location = new System.Drawing.Point(0, 0);
            this.grdShipSchedule.Margin = new System.Windows.Forms.Padding(0);
            this.grdShipSchedule.Name = "grdShipSchedule";
            this.grdShipSchedule.Size = new System.Drawing.Size(633, 203);
            this.grdShipSchedule.TabIndex = 0;
            this.grdShipSchedule.Text = "Ship Schedule";
            this.grdShipSchedule.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdShipSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdShipSchedule.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdShipSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnTripSelectionChanged);
            this.grdShipSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdShipSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdShipSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdShipSchedule.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdShipSchedule.DoubleClick += new System.EventHandler(this.OnTripDoubleClicked);
            this.grdShipSchedule.Enter += new System.EventHandler(this.OnEnter);
            this.grdShipSchedule.Leave += new System.EventHandler(this.OnLeave);
            this.grdShipSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            this.grdShipSchedule.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseMove);
            this.grdShipSchedule.MouseUp += new System.Windows.Forms.MouseEventHandler(this.OnDragDropMouseUp);
            // 
            // mShipSchedule
            // 
            this.mShipSchedule.DataSetName = "ShipScheduleDataset";
            this.mShipSchedule.Locale = new System.Globalization.CultureInfo("en-US");
            this.mShipSchedule.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 302);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(916, 25);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 3;
            this.ssMain.TerminalText = "Terminal";
            // 
            // splitterV
            // 
            this.splitterV.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.splitterV.Location = new System.Drawing.Point(272, 73);
            this.splitterV.Name = "splitterV";
            this.splitterV.Size = new System.Drawing.Size(3, 229);
            this.splitterV.TabIndex = 11;
            this.splitterV.TabStop = false;
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msEdit,
            this.msView,
            this.msOp,
            this.msReports,
            this.msTools,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Padding = new System.Windows.Forms.Padding(0);
            this.msMain.Size = new System.Drawing.Size(916, 24);
            this.msMain.TabIndex = 12;
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
            this.msFile.Size = new System.Drawing.Size(37, 24);
            this.msFile.Text = "File";
            // 
            // msFileNew
            // 
            this.msFileNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.msFileNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(152, 22);
            this.msFileNew.Text = "&New...";
            this.msFileNew.ToolTipText = "New";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Image = global::Argix.Properties.Resources.Open;
            this.msFileOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(152, 22);
            this.msFileOpen.Text = "&Open...";
            this.msFileOpen.ToolTipText = "Open";
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
            this.msFileSave.ToolTipText = "Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.msFileSaveAs.Text = "Save &As...";
            this.msFileSaveAs.ToolTipText = "Save as";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
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
            this.msFileSetup.Text = "Page Set&up...";
            this.msFileSetup.ToolTipText = "Modify print layout";
            this.msFileSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Image = global::Argix.Properties.Resources.Print;
            this.msFilePrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(152, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.ToolTipText = "Print";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePreview
            // 
            this.msFilePreview.Image = global::Argix.Properties.Resources.PrintPreview;
            this.msFilePreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msFilePreview.Name = "msFilePreview";
            this.msFilePreview.Size = new System.Drawing.Size(152, 22);
            this.msFilePreview.Text = "Print P&review...";
            this.msFilePreview.ToolTipText = "Print preview";
            this.msFilePreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.ImageTransparentColor = System.Drawing.Color.Magenta;
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
            this.msEditFind,
            this.msEditFindTL});
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 24);
            this.msEdit.Text = "Edit";
            // 
            // msEditCut
            // 
            this.msEditCut.Image = global::Argix.Properties.Resources.Cut;
            this.msEditCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCut.Name = "msEditCut";
            this.msEditCut.Size = new System.Drawing.Size(219, 22);
            this.msEditCut.Text = "Cu&t";
            this.msEditCut.ToolTipText = "Cut the selected text";
            this.msEditCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditCopy
            // 
            this.msEditCopy.Image = global::Argix.Properties.Resources.Copy;
            this.msEditCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditCopy.Name = "msEditCopy";
            this.msEditCopy.Size = new System.Drawing.Size(219, 22);
            this.msEditCopy.Text = "&Copy";
            this.msEditCopy.ToolTipText = "Copy the selected text";
            this.msEditCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditPaste
            // 
            this.msEditPaste.Image = global::Argix.Properties.Resources.Paste;
            this.msEditPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditPaste.Name = "msEditPaste";
            this.msEditPaste.Size = new System.Drawing.Size(219, 22);
            this.msEditPaste.Text = "&Paste";
            this.msEditPaste.ToolTipText = "Paste text into the current selection";
            this.msEditPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditSep1
            // 
            this.msEditSep1.Name = "msEditSep1";
            this.msEditSep1.Size = new System.Drawing.Size(216, 6);
            // 
            // msEditFind
            // 
            this.msEditFind.Image = global::Argix.Properties.Resources.Find;
            this.msEditFind.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditFind.Name = "msEditFind";
            this.msEditFind.Size = new System.Drawing.Size(219, 22);
            this.msEditFind.Text = "Search for a Zone";
            this.msEditFind.ToolTipText = "Search for a zone";
            this.msEditFind.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msEditFindTL
            // 
            this.msEditFindTL.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msEditFindTL.Name = "msEditFindTL";
            this.msEditFindTL.Size = new System.Drawing.Size(219, 22);
            this.msEditFindTL.Text = "Search for an Assigned TL...";
            this.msEditFindTL.ToolTipText = "Search for an assigned TL";
            this.msEditFindTL.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewFont,
            this.msViewSep2,
            this.msViewZoneType,
            this.msViewSep3,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 24);
            this.msView.Text = "View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.msViewRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(130, 22);
            this.msViewRefresh.Text = "&Refresh";
            this.msViewRefresh.ToolTipText = "refresh the active grid";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(127, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(130, 22);
            this.msViewFont.Text = "&Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep2
            // 
            this.msViewSep2.Name = "msViewSep2";
            this.msViewSep2.Size = new System.Drawing.Size(127, 6);
            // 
            // msViewZoneType
            // 
            this.msViewZoneType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewZoneTypeTsort,
            this.msViewZoneTypeReturns,
            this.msViewZoneTypeAll});
            this.msViewZoneType.Image = global::Argix.Properties.Resources.Legend;
            this.msViewZoneType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msViewZoneType.Name = "msViewZoneType";
            this.msViewZoneType.Size = new System.Drawing.Size(130, 22);
            this.msViewZoneType.Text = "Zone Type";
            this.msViewZoneType.ToolTipText = "Filter TLs by zone type";
            // 
            // msViewZoneTypeTsort
            // 
            this.msViewZoneTypeTsort.Name = "msViewZoneTypeTsort";
            this.msViewZoneTypeTsort.Size = new System.Drawing.Size(114, 22);
            this.msViewZoneTypeTsort.Text = "Tsort";
            this.msViewZoneTypeTsort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewZoneTypeReturns
            // 
            this.msViewZoneTypeReturns.Name = "msViewZoneTypeReturns";
            this.msViewZoneTypeReturns.Size = new System.Drawing.Size(114, 22);
            this.msViewZoneTypeReturns.Text = "Returns";
            this.msViewZoneTypeReturns.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewZoneTypeAll
            // 
            this.msViewZoneTypeAll.Name = "msViewZoneTypeAll";
            this.msViewZoneTypeAll.Size = new System.Drawing.Size(114, 22);
            this.msViewZoneTypeAll.Text = "All";
            this.msViewZoneTypeAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep3
            // 
            this.msViewSep3.Name = "msViewSep3";
            this.msViewSep3.Size = new System.Drawing.Size(127, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(130, 22);
            this.msViewToolbar.Text = "&Toolbar";
            this.msViewToolbar.ToolTipText = "Open/close the toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(130, 22);
            this.msViewStatusBar.Text = "&Status Bar";
            this.msViewStatusBar.ToolTipText = "Open/close the status bar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOp
            // 
            this.msOp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msOpAdd,
            this.msOpRem,
            this.msOpChangeLanes,
            this.msOpCloseZones,
            this.msOpSep1,
            this.msOpOpen,
            this.msOpCloseAllTLs,
            this.msOpClose,
            this.msOpSep2,
            this.msOpAssign,
            this.msOpUnassign,
            this.msOpMove});
            this.msOp.Name = "msOp";
            this.msOp.Size = new System.Drawing.Size(72, 24);
            this.msOp.Text = "Operation";
            // 
            // msOpAdd
            // 
            this.msOpAdd.Image = global::Argix.Properties.Resources.BuilderDialog_add;
            this.msOpAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpAdd.Name = "msOpAdd";
            this.msOpAdd.Size = new System.Drawing.Size(148, 22);
            this.msOpAdd.Text = "Add";
            this.msOpAdd.ToolTipText = "Add selected TLs for update";
            this.msOpAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpRem
            // 
            this.msOpRem.Image = global::Argix.Properties.Resources.BuilderDialog_remove;
            this.msOpRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpRem.Name = "msOpRem";
            this.msOpRem.Size = new System.Drawing.Size(148, 22);
            this.msOpRem.Text = "Remove";
            this.msOpRem.ToolTipText = "Remove selected TLs from update";
            this.msOpRem.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpChangeLanes
            // 
            this.msOpChangeLanes.Image = global::Argix.Properties.Resources.lanes;
            this.msOpChangeLanes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpChangeLanes.Name = "msOpChangeLanes";
            this.msOpChangeLanes.Size = new System.Drawing.Size(148, 22);
            this.msOpChangeLanes.Text = "Change Lanes";
            this.msOpChangeLanes.ToolTipText = "Change lanes for all TLs in the update grid";
            this.msOpChangeLanes.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpCloseZones
            // 
            this.msOpCloseZones.Image = global::Argix.Properties.Resources.zones;
            this.msOpCloseZones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpCloseZones.Name = "msOpCloseZones";
            this.msOpCloseZones.Size = new System.Drawing.Size(148, 22);
            this.msOpCloseZones.Text = "Close Zones";
            this.msOpCloseZones.ToolTipText = "Close zones and change lanes for all TLs in the update grid";
            this.msOpCloseZones.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpSep1
            // 
            this.msOpSep1.Name = "msOpSep1";
            this.msOpSep1.Size = new System.Drawing.Size(145, 6);
            // 
            // msOpOpen
            // 
            this.msOpOpen.Image = global::Argix.Properties.Resources.book_open;
            this.msOpOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpOpen.Name = "msOpOpen";
            this.msOpOpen.Size = new System.Drawing.Size(148, 22);
            this.msOpOpen.Text = "Open Trip";
            this.msOpOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpCloseAllTLs
            // 
            this.msOpCloseAllTLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpCloseAllTLs.Name = "msOpCloseAllTLs";
            this.msOpCloseAllTLs.Size = new System.Drawing.Size(148, 22);
            this.msOpCloseAllTLs.Text = "Close All TLs";
            this.msOpCloseAllTLs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpClose
            // 
            this.msOpClose.Image = global::Argix.Properties.Resources.book_angle;
            this.msOpClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpClose.Name = "msOpClose";
            this.msOpClose.Size = new System.Drawing.Size(148, 22);
            this.msOpClose.Text = "Close Trip";
            this.msOpClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpSep2
            // 
            this.msOpSep2.Name = "msOpSep2";
            this.msOpSep2.Size = new System.Drawing.Size(145, 6);
            // 
            // msOpAssign
            // 
            this.msOpAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.msOpAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpAssign.Name = "msOpAssign";
            this.msOpAssign.Size = new System.Drawing.Size(148, 22);
            this.msOpAssign.Text = "Assign TL";
            this.msOpAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpUnassign
            // 
            this.msOpUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.msOpUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpUnassign.Name = "msOpUnassign";
            this.msOpUnassign.Size = new System.Drawing.Size(148, 22);
            this.msOpUnassign.Text = "Unassign TL";
            this.msOpUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msOpMove
            // 
            this.msOpMove.Image = global::Argix.Properties.Resources.MoveToFolder;
            this.msOpMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msOpMove.Name = "msOpMove";
            this.msOpMove.Size = new System.Drawing.Size(148, 22);
            this.msOpMove.Text = "Move TL";
            this.msOpMove.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msReports
            // 
            this.msReports.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msReportsZonesByLane});
            this.msReports.Name = "msReports";
            this.msReports.Size = new System.Drawing.Size(59, 24);
            this.msReports.Text = "Reports";
            // 
            // msReportsZonesByLane
            // 
            this.msReportsZonesByLane.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msReportsZonesByLane.Name = "msReportsZonesByLane";
            this.msReportsZonesByLane.Size = new System.Drawing.Size(159, 22);
            this.msReportsZonesByLane.Text = "&Zones by Lane...";
            this.msReportsZonesByLane.ToolTipText = "Open the Zones by Lane report";
            this.msReportsZonesByLane.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msTools
            // 
            this.msTools.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msToolsConfig});
            this.msTools.Name = "msTools";
            this.msTools.Size = new System.Drawing.Size(48, 24);
            this.msTools.Text = "Tools";
            // 
            // msToolsConfig
            // 
            this.msToolsConfig.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.msToolsConfig.Name = "msToolsConfig";
            this.msToolsConfig.Size = new System.Drawing.Size(157, 22);
            this.msToolsConfig.Text = "&Configuration...";
            this.msToolsConfig.ToolTipText = "Access the application configuration";
            this.msToolsConfig.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 24);
            this.msHelp.Text = "Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(189, 22);
            this.msHelpAbout.Text = "&About Zone Closing...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(186, 6);
            // 
            // tsZoneType
            // 
            this.tsZoneType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsZoneTypeTsort,
            this.tsZoneTypeReturns,
            this.tsZoneTypeAll});
            this.tsZoneType.Image = global::Argix.Properties.Resources.Legend;
            this.tsZoneType.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsZoneType.Name = "tsZoneType";
            this.tsZoneType.Size = new System.Drawing.Size(76, 46);
            this.tsZoneType.Text = "Zone Type";
            this.tsZoneType.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsZoneType.ToolTipText = "Zone Type";
            // 
            // tsZoneTypeTsort
            // 
            this.tsZoneTypeTsort.Name = "tsZoneTypeTsort";
            this.tsZoneTypeTsort.Size = new System.Drawing.Size(114, 22);
            this.tsZoneTypeTsort.Text = "Tsort";
            this.tsZoneTypeTsort.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsZoneTypeReturns
            // 
            this.tsZoneTypeReturns.Name = "tsZoneTypeReturns";
            this.tsZoneTypeReturns.Size = new System.Drawing.Size(114, 22);
            this.tsZoneTypeReturns.Text = "Returns";
            this.tsZoneTypeReturns.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsZoneTypeAll
            // 
            this.tsZoneTypeAll.Name = "tsZoneTypeAll";
            this.tsZoneTypeAll.Size = new System.Drawing.Size(114, 22);
            this.tsZoneTypeAll.Text = "All";
            this.tsZoneTypeAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsOpen_,
            this.tsSep1,
            this.tsPrint,
            this.tsSearch,
            this.tsSep2,
            this.tsRefresh,
            this.tsZoneType,
            this.tsSep3,
            this.tsAdd,
            this.tsRem,
            this.tsChangeLanes,
            this.tsCloseZones,
            this.tsSep4,
            this.tsOpen,
            this.tsClose,
            this.tsAssign,
            this.tsUnassign,
            this.tsMove});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Padding = new System.Windows.Forms.Padding(0);
            this.tsMain.Size = new System.Drawing.Size(916, 49);
            this.tsMain.TabIndex = 13;
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
            // tsOpen_
            // 
            this.tsOpen_.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen_.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen_.Name = "tsOpen_";
            this.tsOpen_.Size = new System.Drawing.Size(40, 46);
            this.tsOpen_.Text = "Open";
            this.tsOpen_.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen_.ToolTipText = "Open...";
            this.tsOpen_.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep1
            // 
            this.tsSep1.Name = "tsSep1";
            this.tsSep1.Size = new System.Drawing.Size(6, 49);
            // 
            // tsPrint
            // 
            this.tsPrint.Image = global::Argix.Properties.Resources.Print;
            this.tsPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsPrint.Name = "tsPrint";
            this.tsPrint.Size = new System.Drawing.Size(36, 46);
            this.tsPrint.Text = "Print";
            this.tsPrint.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsPrint.ToolTipText = "Print...";
            this.tsPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSearch
            // 
            this.tsSearch.Image = global::Argix.Properties.Resources.Find;
            this.tsSearch.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsSearch.Name = "tsSearch";
            this.tsSearch.Size = new System.Drawing.Size(46, 46);
            this.tsSearch.Text = "Search";
            this.tsSearch.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsSearch.ToolTipText = "Search...";
            this.tsSearch.Click += new System.EventHandler(this.OnItemClick);
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
            this.tsRefresh.ToolTipText = "Refresh view";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep3
            // 
            this.tsSep3.Name = "tsSep3";
            this.tsSep3.Size = new System.Drawing.Size(6, 49);
            // 
            // tsAdd
            // 
            this.tsAdd.Image = global::Argix.Properties.Resources.BuilderDialog_add;
            this.tsAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAdd.Name = "tsAdd";
            this.tsAdd.Size = new System.Drawing.Size(49, 46);
            this.tsAdd.Text = "Add TL";
            this.tsAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsAdd.ToolTipText = "Add TL to selection";
            this.tsAdd.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsRem
            // 
            this.tsRem.Image = global::Argix.Properties.Resources.BuilderDialog_remove;
            this.tsRem.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRem.Name = "tsRem";
            this.tsRem.Size = new System.Drawing.Size(70, 46);
            this.tsRem.Text = "Remove TL";
            this.tsRem.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRem.ToolTipText = "Remove TL from selection";
            this.tsRem.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsChangeLanes
            // 
            this.tsChangeLanes.Image = global::Argix.Properties.Resources.lanes;
            this.tsChangeLanes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsChangeLanes.Name = "tsChangeLanes";
            this.tsChangeLanes.Size = new System.Drawing.Size(85, 46);
            this.tsChangeLanes.Text = "Change Lanes";
            this.tsChangeLanes.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsChangeLanes.ToolTipText = "Change lanes only";
            this.tsChangeLanes.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsCloseZones
            // 
            this.tsCloseZones.Image = global::Argix.Properties.Resources.zones;
            this.tsCloseZones.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsCloseZones.Name = "tsCloseZones";
            this.tsCloseZones.Size = new System.Drawing.Size(75, 46);
            this.tsCloseZones.Text = "Close Zones";
            this.tsCloseZones.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsCloseZones.ToolTipText = "Change lane AND close zones";
            this.tsCloseZones.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsSep4
            // 
            this.tsSep4.Name = "tsSep4";
            this.tsSep4.Size = new System.Drawing.Size(6, 49);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.book_open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(64, 46);
            this.tsOpen.Text = "Open Trip";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open the closed trip";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsClose
            // 
            this.tsClose.Image = global::Argix.Properties.Resources.book_angle;
            this.tsClose.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsClose.Name = "tsClose";
            this.tsClose.Size = new System.Drawing.Size(64, 46);
            this.tsClose.Text = "Close Trip";
            this.tsClose.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsClose.ToolTipText = "Close the open trip";
            this.tsClose.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsAssign
            // 
            this.tsAssign.Image = global::Argix.Properties.Resources.Edit_Redo;
            this.tsAssign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsAssign.Name = "tsAssign";
            this.tsAssign.Size = new System.Drawing.Size(62, 46);
            this.tsAssign.Text = "Assign TL";
            this.tsAssign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsAssign.ToolTipText = "Assign TL to ship schedule";
            this.tsAssign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsUnassign
            // 
            this.tsUnassign.Image = global::Argix.Properties.Resources.Edit_Undo;
            this.tsUnassign.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsUnassign.Name = "tsUnassign";
            this.tsUnassign.Size = new System.Drawing.Size(75, 46);
            this.tsUnassign.Text = "Unassign TL";
            this.tsUnassign.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsUnassign.ToolTipText = "Unassign TL from ship schedule";
            this.tsUnassign.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMove
            // 
            this.tsMove.Image = global::Argix.Properties.Resources.MoveToFolder;
            this.tsMove.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsMove.Name = "tsMove";
            this.tsMove.Size = new System.Drawing.Size(57, 46);
            this.tsMove.Text = "Move TL";
            this.tsMove.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsMove.ToolTipText = "Move TL to another ship schedule";
            this.tsMove.Click += new System.EventHandler(this.OnItemClick);
            // 
            // frmMain
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(916, 327);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.splitterV);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Zone Closing";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.pnlMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdTLs)).EndInit();
            this.csZone.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mZones)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdZones)).EndInit();
            this.grdZones.ResumeLayout(false);
            this.grdZones.PerformLayout();
            this.tabMain.ResumeLayout(false);
            this.tabLanes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mLanes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLaneUpdates)).EndInit();
            this.csTrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mUpdates)).EndInit();
            this.tabZones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdZoneUpdates)).EndInit();
            this.tabSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdShipSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mShipSchedule)).EndInit();
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
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
				this.mToolTip.SetToolTip(this.cboLane, "Change lane assignments");
				this.mToolTip.SetToolTip(this.cboSmallLane, "Change small lane assignments");
				#endregion
                
                //Set control defaults
                #region Grid Overrides
				this.grdZones.AllowDrop = true;
				this.grdZones.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdZones.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdTLs.AllowDrop = false;
				this.grdTLs.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdTLs.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdLaneUpdates.AllowDrop = true;
				this.grdLaneUpdates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdLaneUpdates.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdZoneUpdates.AllowDrop = true;
				this.grdZoneUpdates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdZoneUpdates.DisplayLayout.Bands[0].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				this.grdShipSchedule.AllowDrop = true;
				this.grdShipSchedule.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
				this.grdShipSchedule.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
				this.grdShipSchedule.DisplayLayout.Bands[1].Columns["Zone"].SortIndicator = SortIndicator.Ascending;
				#endregion
                ServiceInfo t = TsortGateway.GetServiceInfo();
                this.ssMain.SetTerminalPanel(t.TerminalID.ToString(),t.Description);
                this.ssMain.User1Panel.Width = 144;
                this.mLanes.Merge(TsortGateway.GetLanes());

				//Modify display as required by configuration
                if(!App.Config.ShowShipSchedule) 
					this.tabMain.TabPages.Remove(this.tabSchedule);
                if(!App.Config.ShowLaneChanges) {
					this.tabMain.TabPages.Remove(this.tabLanes);
					this.msOpChangeLanes.Visible = false;
					this.tsChangeLanes.Visible = false;
					this.cboLane.Visible = this.cboSmallLane.Visible = false;
					this.grdZoneUpdates.DisplayLayout.Bands[0].Columns["NewLane"].Hidden = true;
					this.grdZoneUpdates.DisplayLayout.Bands[0].Columns["NewSmallSortLane"].Hidden = true;
				}
				
				//Configure zone and ship schedule views
                this.grdZones.DataSource = TsortGateway.Zones;
                this.grdTLs.DataSource = TsortGateway.TLs;
                this.msViewZoneTypeAll.PerformClick();
                this.grdShipSchedule.DataSource = AgentLineHaulGateway.Trips;
                this.dtpScheduleDate.Value = AgentLineHaulGateway.ScheduleDate;
                OnTabSelectedIndexChanged(this.tabMain,EventArgs.Empty);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
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
                global::Argix.Properties.Settings.Default.Save();
                #endregion
            }
        }
        private void OnTabSelectedIndexChanged(object sender,System.EventArgs e) {
			//Event handler for change in view tab selected index
			this.Cursor = Cursors.WaitCursor;
			try {
				this.grdZones.DisplayLayout.Bands[0].ColumnFilters["AssignedToShipScde"].FilterConditions.Clear();
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":	
						//Move lane comboboxes to the lane change view; refresh zone view
                        this.grdZoneUpdates.Controls.Remove(this.cboLane);
                        this.grdZoneUpdates.Controls.Remove(this.cboSmallLane);
						this.grdLaneUpdates.Controls.AddRange(new Control[]{this.cboLane,this.cboSmallLane});
						OnUpdateColumnHeaderResized(this.grdLaneUpdates, null);
						this.grdTLs.Visible = this.splitterH.Visible = false;
						break;
					case "tabZones":	
						//Move lane comboboxes to the lane change/zone update view; refresh zone view
                        this.grdLaneUpdates.Controls.Remove(this.cboLane);
                        this.grdLaneUpdates.Controls.Remove(this.cboSmallLane);
						this.grdZoneUpdates.Controls.AddRange(new Control[]{this.cboLane,this.cboSmallLane});
						OnUpdateColumnHeaderResized(this.grdZoneUpdates, null);
                        if(App.Config.ShowShipSchedule) this.grdZones.DisplayLayout.Bands[0].ColumnFilters["AssignedToShipScde"].FilterConditions.Add(FilterComparisionOperator.NotEquals,"Always"); 
						this.grdTLs.Visible = this.splitterH.Visible = false;
						break;
					case "tabSchedule":	
						//Refresh ship schedule
                        this.grdTLs.Visible = this.splitterH.Visible = true;
                        AgentLineHaulGateway.AgentTerminalID = 0;
                        AgentLineHaulGateway.RefreshTrips();
 						TsortGateway.RefreshTLs();
						this.grdTLs.Selected.Rows.Clear();
						break;
				}
				this.grdZones.DisplayLayout.RefreshFilters();
                TsortGateway.RefreshZones(this.tabMain.SelectedTab.Name == "tabSchedule");
				this.grdZones.Selected.Rows.Clear();
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        #region Zone Grid: OnZonesChanged(), OnZoneSelectionChanged(), OnZoneDoubleClicked(), OnSearchValueChanged()
        private void OnZonesChanged(object sender,EventArgs e) {
            //Event handler for change in zones collection
            try {
                //Configure and update zone view; clear update selections since they live again in mZoneDS after refresh
                this.mMessageMgr.AddMessage("Loading zones...");
                if (this.mUpdates != null) this.mUpdates.Clear();
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnBeforeZoneSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.BeforeSelectChangeEventArgs e) {
			//Event handler for change in zone row selections
			this.Cursor = Cursors.WaitCursor;
			try {
				//Steal focus from search textbox
				if(this.tabMain.SelectedTab.Name == "tabSchedule") {
					//Validate only TLs for single agent are selected
					if(this.grdZones.Selected.Rows.Count > 0) {
						long _agent = Convert.ToInt64(this.grdZones.Selected.Rows[0].Cells["AgentTerminalID"].Value);
						if(this.grdZones.Selected.Rows.Count > 0 && e.NewSelections.Rows.Count == 1) {
							//Case where user is simply changing selection
							e.Cancel = false;
						}
						else {
							//Verify all new selections have the same agent
							for(int i=0; i<e.NewSelections.Rows.Count; i++) {
								long agent = Convert.ToInt64(e.NewSelections.Rows[i].Cells["AgentTerminalID"].Value);
								if(agent != _agent) {
									//Differnet agent- notify and cancel selecting, drag drop, etc
									MessageBox.Show(this, "When more than one TL is selected, all selections must be from the same agent.", App.Title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
									e.Cancel = true;
									this.mIsDragging = false; 
									break;
								}
							}
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnZoneSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in zone row selections
			this.Cursor = Cursors.WaitCursor;
			try {
				//Proceed only if the user changed zone selection or searched for a zone while using the ship schedule
				if((this.grdZones.Focused || this.txtSearchSort.Focused) && this.tabMain.SelectedTab.Name == "tabSchedule") {
					//Update selected agent on ship schedule (for ship schedule filtering);
					//Clear TL row selections to keep filter condition consistent
					long id = (this.grdZones.Selected.Rows.Count > 0) ? Convert.ToInt64(this.grdZones.Selected.Rows[0].Cells["AgentTerminalID"].Value) : 0;
                    AgentLineHaulGateway.AgentTerminalID = id;
                    this.grdTLs.Selected.Rows.Clear();
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnZoneDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for zone double clicked
			try {
				//Enforce single selection on double-click
				if(this.grdZones.Selected.Rows.Count > 1) {
					try {
                        this.mGridSvcShipSchedule.CaptureState();
                        this.grdZones.Selected.Rows.Clear();
                        this.grdZones.Selected.Rows[0].Selected = true;
					}
					catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
                    finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
				
				//Move selected records between zone and update grids
				if(this.grdZones.Selected.Rows.Count > 0 && this.grdZones.ActiveRow != null) {
					switch(this.tabMain.SelectedTab.Name) {
						case "tabZones":
						case "tabLanes":	if(this.csZoneAdd.Enabled) addZonesToUpdate(); break;
						case "tabSchedule": if(this.csZoneAssign.Enabled) assignTL(); break;
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnSearchValueChanged(object sender, System.EventArgs e) {
			//Event handler for change in search text value; normally, this is handled exclusively by
			//the UltraGridSvc mGridSvcZones; but with multiselect on, we need to clear prior selections
			try { this.grdZones.Selected.Rows.Clear(); this.grdZones.Focus(); } catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		#endregion
        #region TL Grid: OnTLsChanged(), OnTLSelectionChanged(), OnTLDoubleClicked()
        private void OnTLsChanged(object sender,EventArgs e) {
            //Event handler for change in zones collection
            try {
                //Configure and update zone view
                this.mMessageMgr.AddMessage("Loading closed TLs...");
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnTLSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in TL row selection
			this.Cursor = Cursors.WaitCursor;
			try {
				//Proceed only if the user changed TL selection while viewing the ship schedule
				if(this.grdTLs.Focused && this.tabMain.SelectedTab.Name == "tabSchedule") {
					//Update selected agent on ship schedule (for ship schedule filtering);
					//Clear zone row selections to keep filter condition consistent
					long id = (this.grdTLs.Selected.Rows.Count > 0) ? Convert.ToInt64(this.grdTLs.Selected.Rows[0].Cells["AgentTerminalID"].Value) : 0;
                    AgentLineHaulGateway.AgentTerminalID = id;
                    this.grdZones.Selected.Rows.Clear();
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void OnTLDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for zone double clicked
			try {
				//Enforce single selection on double-click
				if(this.grdTLs.Selected.Rows.Count > 1) {
					try {
                        this.mGridSvcShipSchedule.CaptureState();
                        this.grdTLs.Selected.Rows.Clear();
                        this.grdTLs.Selected.Rows[0].Selected = true;
					}
					catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
                    finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
				
				//Move selected records between zone and update grids
				if(this.grdTLs.Selected.Rows.Count > 0 && this.grdTLs.ActiveRow != null) {
					if(this.csZoneAssign.Enabled) assignTL(); 
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
		#region Update Grid: OnUpdateSelectionChanged(), OnUpdateColumnHeaderResized(), OnUpdateDoubleClicked(), OnLanesChanged(), OnLaneSelected(), OnSmallLaneSelected()
		private void OnUpdateSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in update lane/update zone row selections
			try {
				//Sync lane\small lane combobox values to selected row lane values;
				//IF multiple rows selected, show null selection in comboboxes
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				if(grid.Selected.Rows.Count == 1) {
					string lane = grid.Selected.Rows[0].Cells["NewLane"].Value.ToString();
					if(this.cboLane.Items.Count > 0) this.cboLane.SelectedValue = lane;
					string smallLane = grid.Selected.Rows[0].Cells["NewSmallSortLane"].Value.ToString();
					if(this.cboSmallLane.Items.Count > 0) this.cboSmallLane.SelectedValue = smallLane;
				}
				else 
					this.cboLane.SelectedItem = this.cboSmallLane.SelectedItem = null;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnUpdateColumnHeaderResized(object sender, Infragistics.Win.UltraWinGrid.AfterColPosChangedEventArgs e) {
			//Event handler for change in column locations
			int left1=0,left2=0;
			try {
				//Align new sort/small sort lane comboboxes with corresponding columns
				UltraGrid grid = (UltraGrid)sender;
                int vpos1 = grid.DisplayLayout.Bands[0].Columns["NewLane"].Header.VisiblePosition;
                int vpos2 = grid.DisplayLayout.Bands[0].Columns["NewSmallSortLane"].Header.VisiblePosition;
				for(int k=0; k<grid.DisplayLayout.Bands[0].Columns.Count; k++) {
					//Adjust for columns left of NewLane, NewSmallSortLane (columns are not in order)
					if(grid.DisplayLayout.Bands[0].Columns[k].Header.VisiblePosition < vpos1) left1 += grid.DisplayLayout.Bands[0].Columns[k].Width;
					if(grid.DisplayLayout.Bands[0].Columns[k].Header.VisiblePosition < vpos2) left2 += grid.DisplayLayout.Bands[0].Columns[k].Width;
				}
				this.cboLane.Top = this.cboSmallLane.Top = 1;
				this.cboLane.Left = left1;
				this.cboSmallLane.Left = left2;
				this.cboLane.Width = grid.DisplayLayout.Bands[0].Columns["NewLane"].Width;
				this.cboSmallLane.Width = grid.DisplayLayout.Bands[0].Columns["NewSmallSortLane"].Width;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnUpdateDoubleClicked(object sender, System.EventArgs e) {
			//Event handler for update lane/zone double clicked
			try {
				//Move selected records between zone and lane/zone update grid
				UltraGrid grid = (UltraGrid)sender;
				if(grid.Selected.Rows.Count > 0) {
					//Clear all selected rows except fo first selected row; then remove
					UltraGridRow row = grid.Selected.Rows[0];
					grid.Selected.Rows.Clear();
					row.Selected = true;
					removeZonesFromUpdate(); 
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		private void OnLaneSelected(object sender, System.EventArgs e) {
			//Event handler for change in lane
			try {
				//Update new lane on selected zones in update grid
				object oVal = (this.cboLane.Text.Length == 0) ? null : this.cboLane.SelectedValue;
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":	
						for(int i=0; i<this.grdLaneUpdates.Selected.Rows.Count; i++) 
							this.grdLaneUpdates.Selected.Rows[i].Cells["NewLane"].Value = oVal;
						break;
					case "tabZones":	
						for(int i=0; i<this.grdZoneUpdates.Selected.Rows.Count; i++) 
							this.grdZoneUpdates.Selected.Rows[i].Cells["NewLane"].Value = oVal;
						break;
					case "tabSchedule":	
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
		}
		private void OnSmallLaneSelected(object sender, System.EventArgs e) {
			//Event handler for change in small sort lane
			try {
				//Update new small lane on selected zones in update grid
				object oVal = (this.cboSmallLane.Text.Length == 0) ? null : this.cboSmallLane.SelectedValue;
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":	
						for(int i=0; i<this.grdLaneUpdates.Selected.Rows.Count; i++) 
							this.grdLaneUpdates.Selected.Rows[i].Cells["NewSmallSortLane"].Value = oVal;
						break;
					case "tabZones":	
						for(int i=0; i<this.grdZoneUpdates.Selected.Rows.Count; i++) 
							this.grdZoneUpdates.Selected.Rows[i].Cells["NewSmallSortLane"].Value = oVal;
						break;
					case "tabSchedule":	
						break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
		}
		#endregion
        #region Ship Schedule: OnGridInitializeLayout(), OnGridInitializeRow(), OnShipScheduleChanged(), OnTripSelectionChanged(), OnCalendarOpened(), OnCalendarClosed(), OnScheduleDateChanged()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//Event handler for grid layout initialization
			try {
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count,"All Assigned");
				e.Layout.Bands[0].Columns["All Assigned"].DataType = typeof(string);
				e.Layout.Bands[0].Columns["All Assigned"].Width = 48;
				e.Layout.Bands[0].Columns["All Assigned"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["All Assigned"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["All Assigned"].SortIndicator = SortIndicator.None;
				e.Layout.Bands[0].Columns.Insert(e.Layout.Bands[0].Columns.Count, "Complete");
				e.Layout.Bands[0].Columns["Complete"].DataType = typeof(string);
				e.Layout.Bands[0].Columns["Complete"].Width = 48;
				e.Layout.Bands[0].Columns["Complete"].Header.Appearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["Complete"].CellAppearance.TextHAlign = HAlign.Center;
				e.Layout.Bands[0].Columns["Complete"].SortIndicator = SortIndicator.None;
			} 
			catch(ArgumentException) { }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Debug); }
		}
		private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//Event handler for grid row initialization
			try {
				//			
				e.Row.Cells["Complete"].Value = (e.Row.Cells["TrailerComplete"].Value != DBNull.Value ? "Y" : "N");
				e.Row.Cells["All Assigned"].Value = (e.Row.Cells["FreightAssigned"].Value != DBNull.Value ? "Y" : "N");
			} 
			catch(ArgumentException) { }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Debug); }
		}
        private void OnShipScheduleChanged(object sender,EventArgs e) {
            //Event handler for change in ship schedule
            try {
                //Configure and update zone view
                this.mMessageMgr.AddMessage("Loading ship schedule for " + this.dtpScheduleDate.Value.ToShortDateString() + "...");
                this.grdShipSchedule.Text = "Ship Schedule (" + AgentLineHaulGateway.ScheduleID + ")";
                OnTripSelectionChanged(null,null);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnTripSelectionChanged(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
			//Event handler for change in trip selections
			this.Cursor = Cursors.WaitCursor;
			try {
				//Clear ref to prior trip object and reset if applicable
				this.mTrip = null;
				if(this.grdShipSchedule.Selected.Rows.Count > 0) {
					//Get a trip object for the selected trip record (could be a child TL row)
					string id="";
					if(this.grdShipSchedule.Selected.Rows[0].HasParent())
						id = this.grdShipSchedule.Selected.Rows[0].ParentRow.Cells["TripID"].Value.ToString();
					else
						id = this.grdShipSchedule.Selected.Rows[0].Cells["TripID"].Value.ToString();
                    this.mTrip = AgentLineHaulGateway.GetTrip(id);

				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnTripDoubleClicked(object sender,System.EventArgs e) {
			//Event handler for trip double clicked
			this.Cursor = Cursors.WaitCursor;
			try {
				//Move selected records between zone grid and ship schedule grid
				if(this.csTripUnassign.Enabled) unassignTL();
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnCalendarOpened(object sender, System.EventArgs e) { this.mCalendarOpen = true; }
		private void OnCalendarClosed(object sender, System.EventArgs e) {
			//Event handler for date picker calendar closed
			this.Cursor = Cursors.WaitCursor;
			try {
				//Allow calendar to close
				this.dtpScheduleDate.Refresh();
				Application.DoEvents();
				
				//Flag calendar as closed; refresh ship schedule
				this.mCalendarOpen = false;
				OnScheduleDateChanged(null,null);
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnScheduleDateChanged(object sender, System.EventArgs e) {
			//Event handler for ship schedule date changed
			this.Cursor = Cursors.WaitCursor;
			try {
				//Refresh ship schedule if the calendar is closed
                if (!this.mCalendarOpen)
                    AgentLineHaulGateway.ScheduleDate = this.dtpScheduleDate.Value;
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Common Grid Services: OnEnter(), OnLeave(), OnGridMouseDown()
		private void OnEnter(object sender, System.EventArgs e) { setUserServices(); }
		private void OnLeave(object sender, System.EventArgs e) { setUserServices(); }
		private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event for all grids
			try {
				//Ensure focus when user mouses (embedded child objects sometimes hold focus)
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				
				//Determine grid element pointed to by the mouse
				UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(uiElement != null) {
					//Determine if user selected a grid row
					object context = uiElement.GetContext(typeof(UltraGridRow));
					if(context != null) {
						//Row was selected- if mouse button is:
						// left: forward to mouse move event handler
						//right: clear all (multi-)selected rows and select a single row
						if(e.Button == MouseButtons.Left) 
							OnDragDropMouseDown(sender, e);
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow row = (UltraGridRow)context;
							if(!row.Selected) grid.Selected.Rows.Clear();
							row.Selected = true;
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
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			finally { setUserServices(); }
		}
		#endregion
		#region Grid Drag/Drop Events: OnDragDropMouseDown(), OnDragDropMouseMove(), OnDragDropMouseUp(), OnDragEnter(), OnDragOver(), OnDragDrop(), OnDragLeave()
		private void OnDragDropMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) { this.mIsDragging = (e.Button==MouseButtons.Left); }
		private void OnDragDropMouseMove(object sender, System.Windows.Forms.MouseEventArgs e) {
			//Start drag\drop if user is dragging
			DataObject data=null;
			try {
				if(e.Button == MouseButtons.Left) {
					if(this.mIsDragging) {
						//Initiate drag drop operation from the grid source
						UltraGrid grid = (UltraGrid)sender;
						if(grid.Focused && grid.Selected.Rows.Count > 0) {
							data = new DataObject();
							data.SetData("");
							DragDropEffects effect = grid.DoDragDrop(data, DragDropEffects.All);
							this.mIsDragging = false; 
								
							//After the drop- handled by drop code
							switch(effect) {
								case DragDropEffects.Move:	break;
								case DragDropEffects.Copy:	break;
							}
						}
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnDragDropMouseUp(object sender, System.Windows.Forms.MouseEventArgs e) { this.mIsDragging = false; }
		private void OnDragEnter(object sender, System.Windows.Forms.DragEventArgs e) { }
		private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dragging over the grid
			try {
				//Enable appropriate drag drop effect
				//NOTE: Cannot COPY or MOVE to self
				UltraGrid grid = (UltraGrid)sender;
				DataObject data = (DataObject)e.Data;
				if(!grid.Focused && data.GetDataPresent(DataFormats.StringFormat, true)) {
					bool allowed=false;
					switch(grid.Name) {
						case "grdZones":			
							allowed = (this.csTripRem.Enabled || this.csTripUnassign.Enabled); 
							break;
						case "grdTLs": 
							allowed = false; 
							break;
						case "grdLaneUpdates":
						case "grdZoneUpdates":	
							allowed = true; 
							break;
						case "grdShipSchedule": 
							Point pt = this.grdShipSchedule.PointToClient(new Point(e.X, e.Y));
							UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(pt);
							if(uiElement != null) {
								object context = uiElement.GetContext(typeof(UltraGridRow));
								if(context != null) 					
									((UltraGridRow)context).Selected = true;
								else 
									grid.Selected.Rows.Clear();
							}
							allowed = this.csZoneAssign.Enabled; 
							break;
					}
					switch(e.KeyState) {
						case KEYSTATE_SHIFT:	e.Effect = (!grid.Focused && allowed) ? DragDropEffects.Move : DragDropEffects.None; break;
						case KEYSTATE_CTL:		e.Effect = (!grid.Focused && allowed) ? DragDropEffects.Copy : DragDropEffects.None; break;
						default:				e.Effect = (!grid.Focused && allowed) ? DragDropEffects.Copy : DragDropEffects.None; break;
					}
				}
				else
					e.Effect = DragDropEffects.None;
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
		}
		private void OnDragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
			//Event handler for dropping onto the grid
			try {
				DataObject data = (DataObject)e.Data;
				if(data.GetDataPresent(DataFormats.StringFormat, true)) {
					UltraGrid grid = (UltraGrid)sender;
					switch(grid.Name) {
						case "grdZones":			
							switch(this.tabMain.SelectedTab.Name) {
								case "tabZones":
								case "tabLanes":	removeZonesFromUpdate(); break;
								case "tabSchedule":	unassignTL(); break;
							}
							break;
						case "grdTLs":			break;
						case "grdLaneUpdates":
						case "grdZoneUpdates":	addZonesToUpdate(); break;
						case "grdShipSchedule":	assignTL(); break;
					}
				}
			}
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
		}
		private void OnDragLeave(object sender, System.EventArgs e) { }
		private void OnQueryContinueDrag(object sender, System.Windows.Forms.QueryContinueDragEventArgs e) { }
		private void OnSelectionDrag(object sender, System.ComponentModel.CancelEventArgs e) { }
		#endregion
        #region User Services: OnItemClick(), OnHelpMenuClick()
        private void OnItemClick(object sender,System.EventArgs e) {
            //Event handler for menu selection
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "msFileNew": 
                    case "tsNew": 
                        break;
                    case "msFileOpen": 
                    case "tsOpen_": 
                        break;
                    case "msFileSave": 
                    case "tsSave": 
                        break;
                    case "msFileSaveAs": 
                        break;
                    case "msFileSetup":    UltraGridPrinter.PageSettings(); break;
                    case "msFilePrint": UltraGridPrinter.Print(this.grdShipSchedule,AgentLineHaulGateway.ScheduleDate.DayOfWeek.ToString().ToUpper() + Environment.NewLine + "SHIP SCHEDULE FOR " + AgentLineHaulGateway.ScheduleDate.ToString("dd-MMM-yyyy"),true); break;
                    case "tsPrint": UltraGridPrinter.Print(this.grdShipSchedule,AgentLineHaulGateway.ScheduleDate.DayOfWeek.ToString().ToUpper() + Environment.NewLine + "SHIP SCHEDULE FOR " + AgentLineHaulGateway.ScheduleDate.ToString("dd-MMM-yyyy"),false); break;
                    case "msFilePreview": UltraGridPrinter.PrintPreview(this.grdShipSchedule,AgentLineHaulGateway.ScheduleDate.DayOfWeek.ToString().ToUpper() + Environment.NewLine + "SHIP SCHEDULE FOR " + AgentLineHaulGateway.ScheduleDate.ToString("dd-MMM-yyyy")); break;
                    case "msFileExit":     this.Close(); Application.Exit(); break;
                    case "msEditCut": case "tsCut": break;
                    case "msEditCopy": case "tsCopy": break;
                    case "msEditPaste": case "tsPaste": break;
                    case "msEditFind": case "tsFind": 
                        this.grdZones.Selected.Rows.Clear();
                        this.mGridSvcZones.FindRow(0,this.grdZones.Tag.ToString(),this.txtSearchSort.Text);
                        this.grdZones.Focus();
                        break;
                    case "msEditFindTL":
                        dlgInputBox dlg = new dlgInputBox("Enter a TL# to search for.","","TL Search");
                        dlg.ShowDialog(this);
                        if(dlg.Value.Length > 0) {
                            DateTime scheduleDate = AgentLineHaulGateway.FindShipSchedule(dlg.Value);
                            if(scheduleDate > DateTime.MinValue) {
                                //TL exists; check if assigned
                                if (scheduleDate == DateTime.MaxValue)
                                    MessageBox.Show(this,"TL# " + dlg.Value + " is not assigned to a ship schedule.","TL Search",MessageBoxButtons.OK,MessageBoxIcon.Information);
                                else
                                    this.dtpScheduleDate.Value = scheduleDate;
                            }
                            else
                                MessageBox.Show(this,"TL# " + dlg.Value + " does not exist.","TL Search",MessageBoxButtons.OK,MessageBoxIcon.Error);
                        }
                        break;
                    case "msViewRefresh": case "tsRefresh": 
                        //Refresh zones and ship schedule
                        this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Refreshing zone view...");
                        this.mGridSvcZones.CaptureState();
                        TsortGateway.RefreshZones(this.tabMain.SelectedTab.Name == "tabSchedule");
                        if(this.tabMain.SelectedTab.Name == "tabSchedule") {
                            this.mMessageMgr.AddMessage("Refreshing closed TLs...");
                            this.mGridSvcTLs.CaptureState();
                            TsortGateway.RefreshTLs(); 
                            this.mGridSvcTLs.RestoreState();

                            this.mMessageMgr.AddMessage("Refreshing ship schedule...");
                            this.mGridSvcShipSchedule.CaptureState();
                            AgentLineHaulGateway.RefreshTrips();
                            this.mGridSvcShipSchedule.RestoreState();
                        }
                        this.mGridSvcZones.RestoreState();
                        break;
                    case "msViewZoneTypeTsort": case "tsZoneTypeTsort":
                        this.msViewZoneTypeTsort.Checked = this.tsZoneTypeTsort.Checked = true;
                        this.msViewZoneTypeReturns.Checked = this.tsZoneTypeReturns.Checked = this.msViewZoneTypeAll.Checked = this.tsZoneTypeAll.Checked = false;
                        this.grdZones.Text = "TLs: Open (Tsort)";
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Clear();
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].LogicalOperator = FilterLogicalOperator.Or;
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"T");
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"U");
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"L");
                        this.grdZones.DisplayLayout.RefreshFilters();
                        this.grdZones.Selected.Rows.Clear();
                        break;
                    case "msViewZoneTypeReturns": case "tsZoneTypeReturns":
                        this.msViewZoneTypeReturns.Checked = this.tsZoneTypeReturns.Checked = true;
                        this.msViewZoneTypeTsort.Checked = this.tsZoneTypeTsort.Checked = this.msViewZoneTypeAll.Checked = this.tsZoneTypeAll.Checked = false;
                        this.grdZones.Text = "TLs: Open (Returns)";
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Clear();
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].LogicalOperator = FilterLogicalOperator.Or;
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"V");
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Add(FilterComparisionOperator.Equals,"X");
                        this.grdZones.DisplayLayout.RefreshFilters();
                        this.grdZones.Selected.Rows.Clear();
                        break;
                    case "msViewZoneTypeAll": case "tsZoneTypeAll": 
                        this.msViewZoneTypeAll.Checked = this.tsZoneTypeAll.Checked = true;
                        this.msViewZoneTypeTsort.Checked = this.tsZoneTypeTsort.Checked = this.msViewZoneTypeReturns.Checked = this.tsZoneTypeReturns.Checked = false;
                        this.grdZones.Text = "TLs: Open (All)";
                        this.grdZones.DisplayLayout.Bands[0].ColumnFilters["TypeID"].FilterConditions.Clear();
                        this.grdZones.DisplayLayout.RefreshFilters();
                        this.grdZones.Selected.Rows.Clear();
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
                    case "msOpAdd": case "tsAdd": case "csZoneAdd": 
                        switch(this.tabMain.SelectedTab.Name) {
                            case "tabZones": case "tabLanes": addZonesToUpdate(); break;
                            case "tabSchedule": assignTL(); break;
                        }
                        break;
                    case "msOpRem": case "tsRem": case "csTripRem": 
                        switch(this.tabMain.SelectedTab.Name) {
                            case "tabZones": case "tabLanes": removeZonesFromUpdate(); break;
                            case "tabSchedule": unassignTL(); break;
                        }
                        break;
                    case "msOpChangeLanes": case "tsChangeLanes": 
                        //Change lanes for all zones in update dataset
                        if(MessageBox.Show(this,"Update lane assignments for each zone?",App.Product,MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Changing lane assignments...");
                            for(int i=0;i<this.mUpdates.ZoneTable.Count;i++) {
                                try {
                                    Zone zone = TsortGateway.NewZone(this.mUpdates.ZoneTable[i]);
                                    TsortGateway.ChangeLanes(zone);
                                }
                                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                            }
                            TsortGateway.RefreshZones(false);
                        }
                        break;
                    case "msOpCloseZones": case "tsCloseZones": 
                        //Change lanes and close zones for all zones in update dataset
                        if(MessageBox.Show(this,"Change lane assignments and close zones for each selected zone?",App.Product,MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            this.mMessageMgr.AddMessage("Changing lane assignments and closing zones...");
                            for(int i=0;i<this.mUpdates.ZoneTable.Count;i++) {
                                try {
                                    Zone zone = TsortGateway.NewZone(this.mUpdates.ZoneTable[i]);
                                    TsortGateway.CloseZone(zone);
                                }
                                catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                            }
                            TsortGateway.RefreshZones(false);
                        }
                        break;
                    case "msOpOpen": case "tsOpen": case "csTripOpen":
                        this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Opening the selected trip...");
                        openTrip();
                        break;
                    case "msOpCloseAllTLs": case "csTripCloseAllTLs": 
			            this.Cursor = Cursors.WaitCursor;
				        this.mMessageMgr.AddMessage("Selecting all open zones for closing...");
                        closeAllTLs(); 
                        break;
                    case "msOpClose": case "tsClose": case "csTripClose": 
                        closeTrip(); 
                        break;
                    case "msOpAssign": case "tsAssign": case "csZoneAssign": 
                        assignTL(); 
                        break;
                    case "msOpUnassign": case "tsUnassign": case "csTripUnassign": 
                        unassignTL(); 
                        break;
                    case "msOpMove": case "tsMove": case "csTripMove": 
                        moveTL(); 
                        break;
                    case "msReportsZonesByLane": break;
                    case "msToolsConfig":      App.ShowConfig(); break;
                    case "msHelpAbout":        new dlgAbout(App.Product + " Application",App.Version,App.Copyright,App.Configuration).ShowDialog(this); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
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
        #endregion
        #region Local Services: addZonesToUpdate(), removeZonesFromUpdate(), openTrip(), closeAllTLs(), closeTrip(), assignTL(), unassignTL(), moveTL()
        private void addZonesToUpdate() {
            //Add the selected TLs to the update grid
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mMessageMgr.AddMessage("Adding selected TLs...");
                for(int i=0;i<this.grdZones.Selected.Rows.Count;i++) {
                    //Add each selected row to the update grid- validate not already added
                    UltraGridRow row = this.grdZones.Selected.Rows[i];
                    string zoneCode = row.Cells["Zone"].Value.ToString();
                    string clientNumber = row.Cells["ClientNumber"].Value.ToString();
                    if (this.mUpdates.ZoneTable.Select("Zone='" + zoneCode + "' AND ClientNumber='" + clientNumber + "'").Length == 0) {
                        //Add the selected TL to the update grid
                        FreightDataset.ZoneTableRow zone = this.mUpdates.ZoneTable.NewZoneTableRow();
                        #region Fields
                        zone.Zone = zoneCode;
                        zone._TL_ = row.Cells["TL#"].Value.ToString();
                        zone.Lane = row.Cells["Lane"].Value.ToString();
                        zone.SmallSortLane = row.Cells["SmallSortLane"].Value.ToString();
                        zone.Description = row.Cells["Description"].Value.ToString();
                        zone.Type = row.Cells["Type"].Value.ToString();
                        zone.TypeID = row.Cells["TypeID"].Value.ToString();
                        zone.Status = row.Cells["Status"].Value.ToString();
                        zone.IsExclusive = Convert.ToInt32(row.Cells["IsExclusive"].Value);
                        zone.CAN_BE_CLOSED = row.Cells["CAN_BE_CLOSED"].Value.ToString();
                        zone.AssignedToShipScde = row.Cells["AssignedToShipScde"].Value.ToString();
                        zone.AgentTerminalID = (row.Cells["AgentTerminalID"].Value != DBNull.Value) ? Convert.ToInt64(row.Cells["AgentTerminalID"].Value) : 0;
                        zone.TLDate = (row.Cells["TLDate"].Value != DBNull.Value) ? DateTime.Parse(row.Cells["TLDate"].Value.ToString()) : DateTime.Today;
                        zone.CloseNumber = row.Cells["CloseNumber"].Value.ToString();
                        zone.ClientNumber = clientNumber;
                        zone.ClientName = row.Cells["ClientName"].Value.ToString();
                        #endregion
                        this.mUpdates.ZoneTable.AddZoneTableRow(zone);
                    }
                }
                this.grdZones.DeleteSelectedRows(false);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void removeZonesFromUpdate() {
            //Remove the selected TLs from the update grid
            this.Cursor = Cursors.WaitCursor;
            try {
                UltraGrid grid=null;
                switch(this.tabMain.SelectedTab.Name) {
                    case "tabLanes": grid = this.grdLaneUpdates; break;
                    case "tabZones": grid = this.grdZoneUpdates; break;
                }
                this.mMessageMgr.AddMessage("Removing selected TLs...");
                for(int i=0;i<grid.Selected.Rows.Count;i++) {
                    //Remove all selected row from the update grid- validate not already removed
                    UltraGridRow row = grid.Selected.Rows[i];
                    string zoneCode = row.Cells["Zone"].Value.ToString();
                    string clientNumber = row.Cells["ClientNumber"].Value.ToString();
                    if (TsortGateway.Zones.ZoneTable.Select("Zone='" + zoneCode + "' AND ClientNumber='" + clientNumber + "'").Length == 0) {
                        //Remove the selected TL from the update grid
                        FreightDataset.ZoneTableRow zone = TsortGateway.Zones.ZoneTable.NewZoneTableRow();
                        #region Fields
                        zone.Zone = zoneCode;
                        zone._TL_ = row.Cells["TL#"].Value.ToString();
                        zone.Lane = row.Cells["Lane"].Value.ToString();
                        zone.SmallSortLane = row.Cells["SmallSortLane"].Value.ToString();
                        zone.Description = row.Cells["Description"].Value.ToString();
                        zone.Type = row.Cells["Type"].Value.ToString();
                        zone.TypeID = row.Cells["TypeID"].Value.ToString();
                        zone.Status = row.Cells["Status"].Value.ToString();
                        zone.IsExclusive = Convert.ToInt32(row.Cells["IsExclusive"].Value);
                        zone.CAN_BE_CLOSED = row.Cells["CAN_BE_CLOSED"].Value.ToString();
                        zone.AssignedToShipScde = row.Cells["AssignedToShipScde"].Value.ToString();
                        zone.AgentTerminalID = (row.Cells["AgentTerminalID"].Value != DBNull.Value) ? Convert.ToInt64(row.Cells["AgentTerminalID"].Value) : 0;
                        zone.TLDate = (row.Cells["TLDate"].Value != DBNull.Value) ? DateTime.Parse(row.Cells["TLDate"].Value.ToString()) : DateTime.Today;
                        zone.CloseNumber = row.Cells["CloseNumber"].Value.ToString();
                        zone.ClientNumber = clientNumber;
                        zone.ClientName = row.Cells["ClientName"].Value.ToString();
                        #endregion
                        TsortGateway.Zones.ZoneTable.AddZoneTableRow(zone);
                    }
                }
                grid.DeleteSelectedRows(false);
                this.grdZones.Selected.Rows.Clear();
                this.grdZones.DisplayLayout.Bands[0].SortedColumns.RefreshSort(true);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void openTrip() {
			//Open a closed trip (set All Assigned=null)
            this.mGridSvcShipSchedule.CaptureState();
            AgentLineHaulGateway.Open(this.mTrip.TripID);
		    this.mGridSvcShipSchedule.RestoreState();
		}
		private void closeAllTLs()  {
			//Close all open TLs for the selected trip
			Hashtable openTLs = new Hashtable();
            ShipScheduleDataset.ShipScheduleTLTableRow[] tlRows = (ShipScheduleDataset.ShipScheduleTLTableRow[])this.mShipSchedule.ShipScheduleTLTable.Select("TripID='" + this.mTrip.TripID + "'");
            for (int i = 0;i < tlRows.Length;i++) { if (tlRows[i].IsCloseDateNull()) openTLs.Add(tlRows[i].Zone,""); }
			
            this.tabMain.SelectedIndex = 1;
			for(int i=0; i<this.grdZones.Rows.VisibleRowCount; i++) {
                UltraGridRow tlRow = this.grdZones.Rows.GetRowAtVisibleIndex(i);
                tlRow.Selected = openTLs.ContainsKey(tlRow.Cells["Zone"].Value.ToString());
			}
			addZonesToUpdate();
		}
		private void closeTrip()  {
			//Close an open trip (setAll Assigned=Now)
			DialogResult Q=DialogResult.No;
			try  {
				//Warn on closing when 0 TL's assigned
				Q = DialogResult.Yes;
                ShipScheduleDataset.ShipScheduleTLTableRow[] tls = (ShipScheduleDataset.ShipScheduleTLTableRow[])this.mShipSchedule.ShipScheduleTLTable.Select("TripID='" + this.mTrip.TripID + "'");
                if (tls.Length < 1)
					Q = MessageBox.Show(this, "There are NO TL's assigned to this trip; would you like to continue anyway?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if(Q == DialogResult.No) return;

				//Validate and warn if there are open (unassigned) TL's that may also need be assigned
				//1. Group assigned TLs by first 6 characters (i.e. MMDDYX)
				SortedList list = new SortedList();
                for (int i = 0;i < tls.Length;i++) {
					//Create key from TLDate and CloseNumber
                    DateTime tlDate = tls[i].TLDate;
                    string closeNumber = tls[i].CloseNumber;
					string key = tlDate.ToString("MMddyyyy") + closeNumber.Trim();
					if(list.ContainsKey(key))
						list[key] = (int)list[key] + 1;
					else
						list.Add(key, 1);
				}
				//2. Determine the count of the largest TL group(s)
				int max=0;
				for(int i=0; i<list.Count; i++) {
					int val = (int)list.GetByIndex(i);
					if(val > max) max = val;
				}
				Debug.Write("Max=" + max.ToString() + "\n");
				//3. Verify there are no unassigned TLs with the same 6 characters as the max groups
				bool tlsExist=false;
				foreach(DictionaryEntry entry in list) {
					if(tlsExist) break;
					string tl6 = (string)entry.Key;
					if((int)entry.Value == max) {
						Debug.Write("Validating " + tl6 + "...\n");
						for(int j=0; j<this.grdZones.Rows.VisibleRowCount; j++) {
							string mainZone = this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["Zone"].Value.ToString().Substring(0, 1);
							DateTime _tlDate = DateTime.Parse(this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["TLDate"].Value.ToString());
							string _closeNumber = this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["CloseNumber"].Value.ToString().Trim();
							//string code = this.grdZones.Rows.GetRowAtVisibleIndex(j).Cells["TL#"].Value.ToString().Substring(0, 6);
							string code = _tlDate.ToString("MMddyyyy") + _closeNumber.Trim();
							if(mainZone == this.mTrip.MainZone.TrimEnd() && code == tl6) { 
								tlsExist = true; break; 
							}
						}
					}
				}
				//4. Warn if unassigned TLs exist
				Q = DialogResult.Yes;
				if(tlsExist)
					Q = MessageBox.Show(this, "There are other TL's that may require assignment to this trip; would you like to continue anyway?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
				if(Q == DialogResult.Yes) {
                    //Close the trip
                    this.Cursor = Cursors.WaitCursor;
                    try {
						this.mMessageMgr.AddMessage("Closing the selected trip...");
                        this.mGridSvcShipSchedule.CaptureState();
                        AgentLineHaulGateway.Close(this.mTrip.TripID);
					}
					catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
					finally { this.mGridSvcShipSchedule.RestoreState(); }
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
		private void assignTL()  {
			//Assign an open TL to a trip
			UltraGrid grid = this.grdTLs.Focused ? this.grdTLs : this.grdZones;
            bool ret=false;

            //Assign all selected TLs to the selected trip
			string[] tls = new string[grid.Selected.Rows.Count];
			for(int i=0; i<grid.Selected.Rows.Count; i++) { tls[i] = grid.Selected.Rows[i].Cells["TL#"].Value.ToString(); }
			for(int i=0; i<tls.Length; i++) {
				try {
					//Prompt user for verification if there is an earlier open trip
                    DateTime priorScheduleDate = AgentLineHaulGateway.FindEarlierTripOnPriorSchedule(this.dtpScheduleDate.Value,this.mTrip.TripID,tls[i]);
                    DialogResult Q1 = priorScheduleDate > DateTime.MinValue ? Q1 = MessageBox.Show(this,"There is an earlier trip on the " + priorScheduleDate.ToShortDateString() + " schedule. Are you sure you want to assign TL " + tls[i] + " to this load?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) : DialogResult.Yes;
                    DateTime currentScheduleDate = AgentLineHaulGateway.FindEarlierTripOnCurrentSchedule(this.dtpScheduleDate.Value,this.mTrip.TripID,tls[i]);
                    DialogResult Q2 = (Q1 == DialogResult.Yes && currentScheduleDate>DateTime.MinValue) ? Q2 = MessageBox.Show(this,"There is an earlier trip on todays schedule. Are you sure you want to assign TL " + tls[i] + " to this load?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) : DialogResult.Yes;
					if(Q1 == DialogResult.Yes && Q2 == DialogResult.Yes) {
						this.Cursor = Cursors.WaitCursor;
                        this.mMessageMgr.AddMessage("Assigning selected TL's...");
                        this.mGridSvcShipSchedule.CaptureState();
                        if (AgentLineHaulGateway.AssignTL(this.mTrip.TripID,tls[i])) ret = true;
					}
				}
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
				finally { this.mGridSvcShipSchedule.RestoreState(); }
			}
			
            if(ret) {
				//Refresh zones/TLs on success
                long atid = AgentLineHaulGateway.AgentTerminalID;
                if(grid.Name == "grdZones") {
                    this.mMessageMgr.AddMessage("Refreshing zone view...");
                    this.mGridSvcZones.CaptureState();
                    try {
                        TsortGateway.RefreshZones(true);
                    }
                    catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                    finally { this.mGridSvcZones.RestoreState(); if (AgentLineHaulGateway.AgentTerminalID != atid) this.grdZones.Selected.Rows.Clear(); }
                }
                else if(grid.Name == "grdTLs") {
                    this.mMessageMgr.AddMessage("Refreshing TLs view...");
                    this.mGridSvcTLs.CaptureState();
                    try {
                        TsortGateway.RefreshTLs();
                    }
                    catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
                    finally { this.mGridSvcTLs.RestoreState(); if (AgentLineHaulGateway.AgentTerminalID != atid) AgentLineHaulGateway.AgentTerminalID = 0; }
                }
			}
		}
		private void unassignTL()  {
			//Unassign an open TL from a trip
            this.Cursor = Cursors.WaitCursor;
            bool ret=false;
            try {
                //Unassign the selected TL
                this.mMessageMgr.AddMessage("Unassigning selected freight (TL)...");
                this.mGridSvcShipSchedule.CaptureState();
                ret = AgentLineHaulGateway.UnassignTL(this.grdShipSchedule.Selected.Rows[0].Cells["FreightID"].Value.ToString().TrimEnd());
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.mGridSvcShipSchedule.RestoreState(); }

            //Refresh zone view on success 
            if (ret) {
				try {
                    this.mMessageMgr.AddMessage("Refreshing zone view...");
                    this.mGridSvcZones.CaptureState();
                    TsortGateway.RefreshZones(true);
                } 
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                finally { this.mGridSvcZones.RestoreState(); }
            }
            this.Cursor = Cursors.Default;
		}
		private void moveTL()  {
			//Move a closed TL from one trip to another trip
			//Prompt user for a destination trip
			long agentTerminalID = Convert.ToInt64(this.grdShipSchedule.Selected.Rows[0].Cells["AgentTerminalID"].Value);
			dlgTrip dlg = new dlgTrip(agentTerminalID, this.dtpScheduleDate.Value);
			if(dlg.ShowDialog(this) == DialogResult.OK) {
				bool ret=false;
				try {
					//Move selected freight to the selected trip
					ShipScheduleItem trip = dlg.DestinationTrip;
					string tl = this.grdShipSchedule.Selected.Rows[0].Cells["FreightID"].Value.ToString().TrimEnd();
					//string zone = this.grdShipSchedule.Selected.Rows[0].Cells["Zone"].Value.ToString().TrimEnd();
					
					//Prompt user for verification if there is an earlier open trip
					DialogResult Q1=DialogResult.Yes, Q2=DialogResult.Yes, Q3=DialogResult.Yes;
                    DateTime scheduleDate = AgentLineHaulGateway.FindEarlierTripOnPriorSchedule(this.dtpScheduleDate.Value,trip.TripID,tl);
                    if (scheduleDate > DateTime.MinValue)
                        Q1 = MessageBox.Show(this,"There is an earlier trip on the " + scheduleDate.ToShortDateString() + " schedule. Are you sure you want to assign TL " + tl + " to this load?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
                    scheduleDate = AgentLineHaulGateway.FindEarlierTripOnCurrentSchedule(this.dtpScheduleDate.Value,trip.TripID,tl);
                    if (Q1 == DialogResult.Yes && scheduleDate > DateTime.MinValue) 
						Q2 = MessageBox.Show(this, "There is an earlier trip on todays schedule. Are you sure you want to assign TL " + tl + " to this load?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
                    //if(Q2 == DialogResult.Yes && trip.AssignedTLs.ShipScheduleTLTable.Select("Zone='" + zone + "'").Length > 0) 
                    //    Q3 = MessageBox.Show(this, "There is already a TL " + tl + " assigned to this load for zone " + zone + ". Are you sure you want to assign multiple TL's to the same zone?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2);
					if(Q1 == DialogResult.Yes && Q2 == DialogResult.Yes && Q3 == DialogResult.Yes) {
						this.Cursor = Cursors.WaitCursor;
						this.mMessageMgr.AddMessage("Moving selected TL...");
                        this.mGridSvcShipSchedule.CaptureState();
                        ret = AgentLineHaulGateway.MoveTL(trip.TripID,tl);
                        AgentLineHaulGateway.RefreshTrips();
					}
				}
				catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                finally { this.mGridSvcShipSchedule.RestoreState(); }

                //Refresh zone view on success 
                if (ret) {
					try {
                        this.mMessageMgr.AddMessage("Refreshing zone view...");
                        this.mGridSvcZones.CaptureState();
                        TsortGateway.RefreshZones(true);
                    } 
					catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
                    finally { this.mGridSvcZones.RestoreState(); }
                }
			}
            this.Cursor = Cursors.Default;
        }
		#endregion
        #region Local Services: setUserServices(), buildHelpMenu()
		private void setUserServices() {
			//Set user services depending upon an item selected in the grid
			bool canPrint=false;
			bool canFindTL=false;
			bool canAdd=false, canRemove=false;
			bool canChangeLanes=false, canCloseZones=false;
			bool canOpen=false, canClose=false, canCloseAllTLs=false;
			bool canAssign=false, canUnassign=false, canMove=false;
			try {
				//Determine context and applicable services
				switch(this.tabMain.SelectedTab.Name) {
					case "tabLanes":
						canAdd = (this.grdZones.Focused && this.grdZones.Selected.Rows.Count > 0);
						canRemove = (this.grdLaneUpdates.Focused && this.grdLaneUpdates.Selected.Rows.Count > 0);
                        canChangeLanes = (App.Config.ShowLaneChanges && (RoleServiceGateway.IsShippingSupervisor || RoleServiceGateway.IsShippingClerk) && this.mUpdates.ZoneTable.Rows.Count > 0);
						canCloseZones = false;
						break;
					case "tabZones":
						canAdd = (this.grdZones.Focused && this.grdZones.Selected.Rows.Count > 0);
						canRemove = (this.grdZoneUpdates.Focused && this.grdZoneUpdates.Selected.Rows.Count > 0);
						canChangeLanes = false;
                        canCloseZones = ((RoleServiceGateway.IsShippingSupervisor || RoleServiceGateway.IsShippingClerk) && this.mUpdates.ZoneTable.Rows.Count > 0);
						break;
					case "tabSchedule":
						canFindTL = true;
                        canPrint = (AgentLineHaulGateway.Trips.ShipScheduleViewTable.Rows.Count > 0);
                        if (App.Config.ShowShipSchedule && (RoleServiceGateway.IsShippingSupervisor || RoleServiceGateway.IsShippingClerk) && this.grdShipSchedule.Selected.Rows.Count > 0 && this.mTrip != null) {
                            ShipScheduleDataset.ShipScheduleTLTableRow[] tls = (ShipScheduleDataset.ShipScheduleTLTableRow[])this.mShipSchedule.ShipScheduleTLTable.Select("TripID='" + this.mTrip.TripID + "'");
                            bool anyOpen = false;
                            for (int i = 0;i < tls.Length;i++) { if (tls[i].IsCloseDateNull()) { anyOpen = true; break; } }
                            bool isTripRow = !this.grdShipSchedule.Selected.Rows[0].HasParent();
							bool isTLRow = this.grdShipSchedule.Selected.Rows[0].HasParent();
							bool isTLClosed = isTLRow ? (this.grdShipSchedule.Selected.Rows[0].Cells["CloseDate"].Value.ToString().Length > 0) : false;
                            canOpen = isTripRow && !this.mTrip.IsOpen && !this.mTrip.IsComplete;
                            canCloseAllTLs = isTripRow && this.mTrip.IsOpen && !this.mTrip.IsComplete && anyOpen;
                            canClose = isTripRow && this.mTrip.IsOpen && !this.mTrip.IsComplete && (tls.Length > 0) && !anyOpen;
                            canAssign = (this.grdZones.Focused || this.grdTLs.Focused) && isTripRow && this.mTrip.IsOpen;
                            canUnassign = this.grdShipSchedule.Focused && isTLRow && this.mTrip.IsOpen && !isTLClosed;
                            canMove = this.grdShipSchedule.Focused && isTLRow && this.mTrip.IsOpen && isTLClosed;
						}
						break;
				}
				
				//Set menu and toolbar states
				this.msFileNew.Enabled = this.tsNew.Enabled = false;
				this.msFileOpen.Enabled = this.tsOpen_.Enabled = false;
                this.msFileSave.Enabled = this.msFileSaveAs.Enabled = false;
				this.msFileSetup.Enabled = true;
				this.msFilePrint.Enabled = this.tsPrint.Enabled = canPrint;
                this.msFilePreview.Enabled = canPrint;
				this.msFileExit.Enabled = true;
				this.msEditCut.Enabled = this.msEditCopy.Enabled = this.msEditPaste.Enabled = false;
				this.msEditFind.Enabled = this.tsSearch.Enabled = (this.txtSearchSort.Text.Length > 0);
				this.msEditFindTL.Enabled = canFindTL;
				this.msViewRefresh.Enabled = this.tsRefresh.Enabled = true;
				this.msViewZoneTypeTsort.Enabled = this.msViewZoneTypeReturns.Enabled = this.msViewZoneTypeAll.Enabled = true;
                this.tsZoneTypeTsort.Enabled = this.tsZoneTypeReturns.Enabled = this.tsZoneTypeAll.Enabled = true;
				this.msViewToolbar.Enabled = this.msViewStatusBar.Enabled = true;
				this.msOpAdd.Enabled = this.csZoneAdd.Enabled = this.tsAdd.Enabled = canAdd;
				this.msOpRem.Enabled = this.csTripRem.Enabled = this.tsRem.Enabled = canRemove;
				this.msOpChangeLanes.Enabled = this.tsChangeLanes.Enabled = canChangeLanes;
				this.msOpCloseZones.Enabled = this.tsCloseZones.Enabled = canCloseZones;
				this.msOpOpen.Enabled = this.csTripOpen.Enabled = this.tsOpen.Enabled = canOpen;
				this.msOpClose.Enabled = this.csTripClose.Enabled = this.tsClose.Enabled = canClose;
				this.msOpCloseAllTLs.Enabled = this.csTripCloseAllTLs.Enabled = canCloseAllTLs;
				this.msOpAssign.Enabled = this.csZoneAssign.Enabled = this.tsAssign.Enabled = canAssign;
				this.msOpUnassign.Enabled = this.csTripUnassign.Enabled = this.tsUnassign.Enabled = canUnassign;
				this.msOpMove.Enabled = this.csTripMove.Enabled = this.tsMove.Enabled = canMove;
				this.msReportsZonesByLane.Enabled = false;
                this.msToolsConfig.Enabled = true;
				this.msHelpAbout.Enabled = true;
				this.cboLane.Enabled = ((canChangeLanes || canCloseZones) && (this.cboLane.Items.Count > 0));
				this.cboSmallLane.Enabled = ((canChangeLanes || canCloseZones) && (this.cboSmallLane.Items.Count > 0));

                this.ssMain.OnOnlineStatusUpdate(null,new OnlineStatusArgs(TsortGateway.ServiceState,TsortGateway.ServiceAddress));
                this.ssMain.User1Panel.Width = 144;
                this.ssMain.User1Panel.Text = RoleServiceGateway.GetRoleForCurrentUser();
                this.ssMain.User1Panel.ToolTipText = "User role";
                this.ssMain.User2Panel.Icon = null;
                this.ssMain.User2Panel.ToolTipText = "";
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
