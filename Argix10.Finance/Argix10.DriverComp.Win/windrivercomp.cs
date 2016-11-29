using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;
using Microsoft.Reporting.WinForms;

namespace Argix.Finance {
	//MDI child window for driver compensation
	public class winDriverComp : System.Windows.Forms.Form {
		//Members
        private CompensationAgent mCompAgent = null;
        private UltraGridSvc mRouteGridSvc = null, mRoadshowGridSvc;
        private int mLastOperator = 0, mLastOwner = 0;

        private const int KEYSTATE_SHIFT = 5, KEYSTATE_CTL = 9;
        private const string REPORT_ALL_OPERATORS = "* All Operators";
        private const string REPORT_ALL_OWNERS = "* All Owners";
        
        #region Controls

        private Infragistics.Win.UltraWinGrid.UltraGrid grdCompensation;
        private System.ComponentModel.IContainer components;
        private DriverCompDataset mDriverCompDS;
        private UltraDropDown uddRateType;
        private UltraDropDown uddEquipType;
        private UltraGrid grdRoutes;
        private ContextMenuStrip csComp;
        private ToolStripMenuItem csCRefresh;
        private ToolStripSeparator csCSep1;
        private ToolStripMenuItem csCDelete;
        private ContextMenuStrip csRoutes;
        private ToolStripMenuItem csRRefresh;
        private ToolStripSeparator csRSep1;
        private ToolStripMenuItem csRAddRoutes;
        private ToolStripMenuItem csCCut;
        private ToolStripMenuItem csCCopy;
        private ToolStripMenuItem csCPaste;
        private ToolStripSeparator csCSep2;
        private TabControl tabDialog;
        private TabPage tabRoutes;
        private TabPage tabSummary;
        private TabPage tabDriverComp;
        private Splitter splitterH;
        private Microsoft.Reporting.WinForms.ReportViewer rsvSummary;
        private Microsoft.Reporting.WinForms.ReportViewer rsvDrivers;
        private TabPage tabExport;
        private RichTextBox txtExport;
        private UltraDropDown uddAdjType;
        private Label lblCloseRoutes;
        private ToolStripMenuItem csCExport;
        private ComboBox cboOperators;
        private ToolStripMenuItem csCSaveAs;
        private ToolStripMenuItem csCPrint;
        private ToolStripSeparator csCSep3;
        private ToolStripMenuItem csRSelectAll;
        private ContextMenuStrip csExport;
        private ToolStripMenuItem csERefresh;
        private ToolStripSeparator csESep1;
        private ToolStripMenuItem csEExport;
        private TabPage tabOwnerComp;
        private ReportViewer rsOwners;
        private ComboBox cboOwners;
        private ToolStripSeparator csCSep4;
        private ToolStripMenuItem csCExpandAll;
        private ToolStripMenuItem csCCollapseAll;
        private ToolStripMenuItem csCEquipOverride;
        private ToolStripSeparator csCSep5;
        private CheckBox chkShowRates;
        #endregion
		public event StatusEventHandler StatusMessage=null;
		public event EventHandler ServiceStatesChanged=null;
		
		//Interface
        public winDriverComp(CompensationAgent compAgent) {
			//Constructor
			try {
				InitializeComponent();
                #region Window docking
                this.splitterH.Dock = DockStyle.Bottom;
                this.grdRoutes.Dock = DockStyle.Bottom;
                this.grdRoutes.Controls.Add(this.lblCloseRoutes);
                this.grdRoutes.Height = 192;
                this.grdCompensation.Dock = DockStyle.Fill;
                this.grdCompensation.Controls.Add(this.chkShowRates);
                this.tabRoutes.Controls.AddRange(new Control[] { this.grdCompensation,this.splitterH,this.grdRoutes });
                this.rsvDrivers.Dock = DockStyle.Fill;
                this.cboOperators.Dock = DockStyle.Top;
                this.tabDriverComp.Controls.AddRange(new Control[] { this.rsvDrivers,this.cboOperators });
                this.rsOwners.Dock = DockStyle.Fill;
                this.cboOwners.Dock = DockStyle.Top;
                this.tabOwnerComp.Controls.AddRange(new Control[] { this.rsOwners,this.cboOwners });
                #endregion
				
				//Init objects
                this.mCompAgent = compAgent;
                this.mCompAgent.Changed += new EventHandler(OnCompensationChanged);
                this.mCompAgent.RoutesChanged += new EventHandler(OnRoutesChanged);
                this.mRouteGridSvc = new UltraGridSvc(this.grdCompensation);
                this.mRoadshowGridSvc = new UltraGridSvc(this.grdRoutes);
                this.Text = this.grdCompensation.Text = this.mCompAgent.Title;
                Uri uri = new Uri(global::Argix.Properties.Settings.Default.ReportServerUrl);
                this.rsvSummary.ServerReport.ReportServerUrl = uri;
                this.rsvDrivers.ServerReport.ReportServerUrl = uri;
                this.rsOwners.ServerReport.ReportServerUrl = uri;
			}
			catch(Exception ex) { throw new ApplicationException("Failed to create new winSchedule.", ex); }
		}
        public CompensationAgent Agent { get { return this.mCompAgent; } }
        public override void Refresh() {
            //Refresh selected object
            this.Cursor = Cursors.WaitCursor;
            try {
                setUserServices();
                if (this.tabDialog.SelectedTab == this.tabRoutes) {
                    if (this.grdCompensation.Focused)
                        this.csCRefresh.PerformClick();
                    else if (this.grdRoutes.Focused)
                        this.csRRefresh.PerformClick();
                }
                else if (this.tabDialog.SelectedTab == this.tabSummary) {
                    reportStatus(new StatusEventArgs("Refreshing driver compensation..."));
                    ReportParameter p1 = new ReportParameter("AgentNumber",this.mCompAgent.AgentNumber);
                    ReportParameter p2 = new ReportParameter("AgentName",this.mCompAgent.AgentName);
                    ReportParameter p3 = new ReportParameter("StartDate",this.mCompAgent.BeginDate.ToString("yyyy-MM-dd"));
                    ReportParameter p4 = new ReportParameter("EndDate",this.mCompAgent.EndDate.ToString("yyyy-MM-dd"));
                    this.rsvSummary.ServerReport.DisplayName = "Driver Compensation Summary";
                    this.rsvSummary.ServerReport.ReportPath = global::Argix.Properties.Settings.Default.ReportPathSummaryComp + this.mCompAgent.AgentNumber;
                    this.rsvSummary.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4 });
                    this.rsvSummary.RefreshReport();
                }
                else if (this.tabDialog.SelectedTab == this.tabDriverComp)
                    OnOperatorChanged(this.cboOperators,EventArgs.Empty);
                else if (this.tabDialog.SelectedTab == this.tabOwnerComp)
                    OnOwnerChanged(this.cboOwners,EventArgs.Empty);
                else if (this.tabDialog.SelectedTab == this.tabExport)
                    this.csERefresh.PerformClick();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        protected override void Dispose(bool disposing) { if (disposing) { if (components != null) { components.Dispose(); } } base.Dispose(disposing); }
        #region UI Services: Save(), SaveAs(), AddRoutes(), Export(), Cut(), Copy(), Paste(), Delete(), Print(), PrintPreview(), ShowDriverRates(), RefreshX()
        public bool CanSave { get { return this.mCompAgent.IsDirty; } }
        public void Save() { this.mCompAgent.SaveCompensation(); }
        public bool CanSaveAs { get { return this.csCSaveAs.Enabled; } }
        public void SaveAs() { this.csCSaveAs.PerformClick(); }
        public bool CanAddRoutes { get { return this.csRAddRoutes.Enabled; } }
        public void AddRoutes() { this.csRAddRoutes.PerformClick(); }
        public bool CanExport { get { return this.csCExport.Enabled || this.csEExport.Enabled; } }
        public void Export() { 
            try {
                if(this.tabDialog.SelectedTab == this.tabRoutes) 
                    this.csCExport.PerformClick();
                else if(this.tabDialog.SelectedTab == this.tabExport) 
                    this.csEExport.PerformClick();
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        public void PrintSetup() {
            //Print setup
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.tabDialog.SelectedTab == this.tabRoutes) 
                    UltraGridPrinter.PageSettings();
                //else if(this.tabDialog.SelectedTab == this.tabSummary) 
                //  
                //else if(this.tabDialog.SelectedTab == this.tabDriverComp) 
                //  
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPrint { get { return this.csCPrint.Enabled; } }
        public void Print(bool showDialog) {
            //Print this schedule
            try {
                if(this.tabDialog.SelectedTab == this.tabRoutes) {
                    this.csCPrint.PerformClick();
                }
                else if(this.tabDialog.SelectedTab == this.tabSummary) {
                    reportStatus(new StatusEventArgs("Printing driver paystub..."));
                    this.rsvSummary.PrintDialog();
                }
                else if(this.tabDialog.SelectedTab == this.tabDriverComp) {
                    reportStatus(new StatusEventArgs("Printing driver paystubs..."));
                    this.rsvDrivers.PrintDialog();
                }
                else if(this.tabDialog.SelectedTab == this.tabOwnerComp) {
                    reportStatus(new StatusEventArgs("Printing fleet owner paystubs..."));
                    this.rsOwners.PrintDialog();
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        public bool CanPreview { get { return (this.tabDialog.SelectedTab == this.tabRoutes && this.mCompAgent.Compensation.DriverCompTable.Rows.Count > 0); } }
        public void PrintPreview() {
            try {
                reportStatus(new StatusEventArgs("Print previewing this schedule..."));
                string caption = "DRIVER COMPENSATION" + Environment.NewLine + this.mCompAgent.AgentName.Trim() + " : " + this.mCompAgent.BeginDate.ToString("dd-MMM-yyyy") + "-" + this.mCompAgent.EndDate.ToString("dd-MMM-yyyy");
                UltraGridPrinter.PrintPreview(this.grdCompensation,caption);
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        public bool CanCut { get { return this.csCCut.Enabled; } }
        public void Cut() { this.csCCut.PerformClick(); }
        public bool CanCopy { get { return this.csCCopy.Enabled; } }
        public void Copy() { this.csCCopy.PerformClick(); }
        public bool CanPaste { get { return this.csCPaste.Enabled; } }
        public void Paste() { this.csCPaste.PerformClick(); }
        public bool CanDelete { get { return this.csCDelete.Enabled; } }
        public void Delete() { this.csCDelete.PerformClick(); }
        public bool CanCheckAll { get { return this.csRSelectAll.Enabled; } }
        public void CheckAll() { this.csRSelectAll.PerformClick(); }
        public bool RoutesVisible { get { return this.grdRoutes.Visible; } set { this.grdRoutes.Visible = this.splitterH.Visible = value; } }
        public bool ExportVisible { get { return this.tabExport.CanSelect; } set { if(value) this.tabExport.Show(); else this.tabExport.Hide(); } }
        #endregion
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverCompTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn113 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Select");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn114 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsNew");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn115 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsCombo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn116 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsAdjust");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn117 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn118 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn119 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendor");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn120 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn121 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn122 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn123 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayRate");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn124 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayAmount");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn125 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Miles");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn126 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesBaseRate");
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn127 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesRate");
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn128 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesAmount");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn129 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Trip");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn130 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripRate");
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn131 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripAmount");
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance19 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn132 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stops");
            Infragistics.Win.Appearance appearance20 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance21 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn133 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsRate");
            Infragistics.Win.Appearance appearance22 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance23 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn134 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsAmount");
            Infragistics.Win.Appearance appearance24 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance25 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn135 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance26 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance27 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn136 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsRate");
            Infragistics.Win.Appearance appearance28 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance29 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn137 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsAmount");
            Infragistics.Win.Appearance appearance30 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance31 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn138 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance32 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance33 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn139 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsRate");
            Infragistics.Win.Appearance appearance34 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance35 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn140 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsAmount");
            Infragistics.Win.Appearance appearance36 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance37 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn141 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartons");
            Infragistics.Win.Appearance appearance38 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance39 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn142 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsRate");
            Infragistics.Win.Appearance appearance40 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance41 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn143 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsAmount");
            Infragistics.Win.Appearance appearance42 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance43 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn144 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinimunAmount");
            Infragistics.Win.Appearance appearance44 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance45 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn145 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Amount");
            Infragistics.Win.Appearance appearance46 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance47 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn146 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FuelCost");
            Infragistics.Win.Appearance appearance48 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance49 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn147 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCGal");
            Infragistics.Win.Appearance appearance50 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance51 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn148 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCBaseRate");
            Infragistics.Win.Appearance appearance52 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance53 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn149 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSC");
            Infragistics.Win.Appearance appearance54 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance55 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn150 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount1");
            Infragistics.Win.Appearance appearance56 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance57 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn151 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount2");
            Infragistics.Win.Appearance appearance58 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance59 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn152 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdminCharge");
            Infragistics.Win.Appearance appearance60 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance61 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn153 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TotalAmount");
            Infragistics.Win.Appearance appearance62 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance63 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn154 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCMiles");
            Infragistics.Win.Appearance appearance64 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance65 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn155 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverCompTable_DriverRouteTable");
            Infragistics.Win.Appearance appearance66 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverCompTable_DriverRouteTable", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn156 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn157 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsNew");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn158 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsCombo");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn159 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsAdjust");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn160 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn161 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn162 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteIndex");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn163 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn164 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn165 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OperatorHireDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn166 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Payee");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn167 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn168 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RateTypeID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn169 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayRate");
            Infragistics.Win.Appearance appearance67 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance68 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn170 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DayAmount");
            Infragistics.Win.Appearance appearance69 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance70 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn171 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Miles");
            Infragistics.Win.Appearance appearance71 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance72 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn172 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesBaseRate");
            Infragistics.Win.Appearance appearance73 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance74 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn173 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesRate");
            Infragistics.Win.Appearance appearance75 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance76 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn174 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MilesAmount");
            Infragistics.Win.Appearance appearance77 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance78 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn175 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Trip");
            Infragistics.Win.Appearance appearance79 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance80 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn176 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripRate");
            Infragistics.Win.Appearance appearance81 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance82 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn177 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripAmount");
            Infragistics.Win.Appearance appearance83 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance84 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn178 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Stops");
            Infragistics.Win.Appearance appearance85 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance86 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn179 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsRate");
            Infragistics.Win.Appearance appearance87 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance88 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn180 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopsAmount");
            Infragistics.Win.Appearance appearance89 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance90 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn181 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance91 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance92 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn182 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsRate");
            Infragistics.Win.Appearance appearance93 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance94 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn183 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsAmount");
            Infragistics.Win.Appearance appearance95 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance96 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn184 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance97 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance98 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn185 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsRate");
            Infragistics.Win.Appearance appearance99 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance100 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn186 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PalletsAmount");
            Infragistics.Win.Appearance appearance101 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance102 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn187 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartons");
            Infragistics.Win.Appearance appearance103 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance104 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn188 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsRate");
            Infragistics.Win.Appearance appearance105 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance106 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn189 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PickupCartonsAmount");
            Infragistics.Win.Appearance appearance107 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance108 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn190 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MinimunAmount");
            Infragistics.Win.Appearance appearance109 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance110 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn191 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FuelCost");
            Infragistics.Win.Appearance appearance111 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance112 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn192 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCGal");
            Infragistics.Win.Appearance appearance113 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance114 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn193 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCBaseRate");
            Infragistics.Win.Appearance appearance115 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance116 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn194 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSC");
            Infragistics.Win.Appearance appearance117 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance118 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn195 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount1");
            Infragistics.Win.Appearance appearance119 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance120 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn196 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount1TypeID");
            Infragistics.Win.Appearance appearance121 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance122 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn197 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount2");
            Infragistics.Win.Appearance appearance123 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance124 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn198 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdjustmentAmount2TypeID");
            Infragistics.Win.Appearance appearance125 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance126 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn199 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AdminCharge");
            Infragistics.Win.Appearance appearance127 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance128 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn200 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TotalAmount");
            Infragistics.Win.Appearance appearance129 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance130 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn201 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Imported");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn202 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Exported");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn203 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArgixRtType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn204 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn205 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn206 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FSCMiles");
            Infragistics.Win.Appearance appearance131 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance132 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsNewRoute");
            Infragistics.Win.Appearance appearance133 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance134 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance135 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance136 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance137 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand3 = new Infragistics.Win.UltraWinGrid.UltraGridBand("RoadshowRouteTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn207 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("New");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn208 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteIndex");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn209 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rt_Date");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn210 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rt_Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn211 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Operator");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OperatorHireDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn212 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VEHICLE_TYPE1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn213 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Payee");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn214 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn215 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TtlMiles");
            Infragistics.Win.Appearance appearance138 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance139 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn216 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MultiTrp");
            Infragistics.Win.Appearance appearance140 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance141 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn217 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UniqueStops");
            Infragistics.Win.Appearance appearance142 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance143 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn218 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DelCtns");
            Infragistics.Win.Appearance appearance144 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance145 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn219 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RtnCtn");
            Infragistics.Win.Appearance appearance146 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance147 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn220 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DelPltsorRcks");
            Infragistics.Win.Appearance appearance148 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance149 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn221 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Depot");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn222 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DepotNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn223 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn224 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArgixRtType");
            Infragistics.Win.Appearance appearance150 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance151 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance152 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winDriverComp));
            this.chkShowRates = new System.Windows.Forms.CheckBox();
            this.uddRateType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.uddEquipType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.csRoutes = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csRRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csRSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csRSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.csRAddRoutes = new System.Windows.Forms.ToolStripMenuItem();
            this.csComp = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csCRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csCSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csCSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.csCExport = new System.Windows.Forms.ToolStripMenuItem();
            this.csCSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csCPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.csCSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.csCEquipOverride = new System.Windows.Forms.ToolStripMenuItem();
            this.csCSep4 = new System.Windows.Forms.ToolStripSeparator();
            this.csCCut = new System.Windows.Forms.ToolStripMenuItem();
            this.csCCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.csCPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.csCDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.csCSep5 = new System.Windows.Forms.ToolStripSeparator();
            this.csCExpandAll = new System.Windows.Forms.ToolStripMenuItem();
            this.csCCollapseAll = new System.Windows.Forms.ToolStripMenuItem();
            this.tabDialog = new System.Windows.Forms.TabControl();
            this.tabRoutes = new System.Windows.Forms.TabPage();
            this.grdCompensation = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mDriverCompDS = new Argix.Finance.DriverCompDataset();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.grdRoutes = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.lblCloseRoutes = new System.Windows.Forms.Label();
            this.tabSummary = new System.Windows.Forms.TabPage();
            this.rsvSummary = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabDriverComp = new System.Windows.Forms.TabPage();
            this.rsvDrivers = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cboOperators = new System.Windows.Forms.ComboBox();
            this.tabOwnerComp = new System.Windows.Forms.TabPage();
            this.rsOwners = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cboOwners = new System.Windows.Forms.ComboBox();
            this.tabExport = new System.Windows.Forms.TabPage();
            this.txtExport = new System.Windows.Forms.RichTextBox();
            this.csExport = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csERefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csESep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csEExport = new System.Windows.Forms.ToolStripMenuItem();
            this.uddAdjType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            ((System.ComponentModel.ISupportInitialize)(this.uddRateType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).BeginInit();
            this.csRoutes.SuspendLayout();
            this.csComp.SuspendLayout();
            this.tabDialog.SuspendLayout();
            this.tabRoutes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCompensation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriverCompDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoutes)).BeginInit();
            this.grdRoutes.SuspendLayout();
            this.tabSummary.SuspendLayout();
            this.tabDriverComp.SuspendLayout();
            this.tabOwnerComp.SuspendLayout();
            this.tabExport.SuspendLayout();
            this.csExport.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddAdjType)).BeginInit();
            this.SuspendLayout();
            // 
            // chkShowRates
            // 
            this.chkShowRates.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowRates.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.chkShowRates.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkShowRates.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.chkShowRates.Location = new System.Drawing.Point(644, 3);
            this.chkShowRates.Margin = new System.Windows.Forms.Padding(0);
            this.chkShowRates.Name = "chkShowRates";
            this.chkShowRates.Size = new System.Drawing.Size(96, 16);
            this.chkShowRates.TabIndex = 2;
            this.chkShowRates.Text = "Show Rates";
            this.chkShowRates.UseVisualStyleBackColor = false;
            this.chkShowRates.CheckedChanged += new System.EventHandler(this.OnShowRates);
            // 
            // uddRateType
            // 
            this.uddRateType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddRateType.Location = new System.Drawing.Point(-2, 336);
            this.uddRateType.Name = "uddRateType";
            this.uddRateType.Size = new System.Drawing.Size(104, 18);
            this.uddRateType.TabIndex = 5;
            this.uddRateType.Text = "ultraDropDown1";
            this.uddRateType.Visible = false;
            // 
            // uddEquipType
            // 
            this.uddEquipType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddEquipType.Location = new System.Drawing.Point(108, 336);
            this.uddEquipType.Name = "uddEquipType";
            this.uddEquipType.Size = new System.Drawing.Size(104, 18);
            this.uddEquipType.TabIndex = 6;
            this.uddEquipType.Text = "ultraDropDown1";
            this.uddEquipType.Visible = false;
            // 
            // csRoutes
            // 
            this.csRoutes.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csRRefresh,
            this.csRSep1,
            this.csRSelectAll,
            this.csRAddRoutes});
            this.csRoutes.Name = "csRoutes";
            this.csRoutes.Size = new System.Drawing.Size(252, 76);
            // 
            // csRRefresh
            // 
            this.csRRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRRefresh.Name = "csRRefresh";
            this.csRRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.csRRefresh.Size = new System.Drawing.Size(251, 22);
            this.csRRefresh.Text = "&Refresh Roadshow Routes";
            this.csRRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csRSep1
            // 
            this.csRSep1.Name = "csRSep1";
            this.csRSep1.Size = new System.Drawing.Size(248, 6);
            // 
            // csRSelectAll
            // 
            this.csRSelectAll.Checked = true;
            this.csRSelectAll.CheckOnClick = true;
            this.csRSelectAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.csRSelectAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRSelectAll.Name = "csRSelectAll";
            this.csRSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.csRSelectAll.Size = new System.Drawing.Size(251, 22);
            this.csRSelectAll.Text = "Select &All";
            this.csRSelectAll.ToolTipText = "Check\\uncheck all";
            this.csRSelectAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csRAddRoutes
            // 
            this.csRAddRoutes.Image = global::Argix.Properties.Resources.AddTable;
            this.csRAddRoutes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRAddRoutes.Name = "csRAddRoutes";
            this.csRAddRoutes.Size = new System.Drawing.Size(251, 22);
            this.csRAddRoutes.Text = "Add Roadshow &Routes";
            this.csRAddRoutes.ToolTipText = "Add checked routes to compensation";
            this.csRAddRoutes.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csComp
            // 
            this.csComp.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csCRefresh,
            this.csCSep1,
            this.csCSaveAs,
            this.csCExport,
            this.csCSep2,
            this.csCPrint,
            this.csCSep3,
            this.csCEquipOverride,
            this.csCSep4,
            this.csCCut,
            this.csCCopy,
            this.csCPaste,
            this.csCDelete,
            this.csCSep5,
            this.csCExpandAll,
            this.csCCollapseAll});
            this.csComp.Name = "csComp";
            this.csComp.Size = new System.Drawing.Size(239, 276);
            // 
            // csCRefresh
            // 
            this.csCRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csCRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCRefresh.Name = "csCRefresh";
            this.csCRefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.csCRefresh.Size = new System.Drawing.Size(238, 22);
            this.csCRefresh.Text = "&Refresh Driver Routes";
            this.csCRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCSep1
            // 
            this.csCSep1.Name = "csCSep1";
            this.csCSep1.Size = new System.Drawing.Size(235, 6);
            // 
            // csCSaveAs
            // 
            this.csCSaveAs.Image = global::Argix.Properties.Resources.Save;
            this.csCSaveAs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCSaveAs.Name = "csCSaveAs";
            this.csCSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.csCSaveAs.Size = new System.Drawing.Size(238, 22);
            this.csCSaveAs.Text = "Save &As...";
            this.csCSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCExport
            // 
            this.csCExport.Image = global::Argix.Properties.Resources.ImportXML;
            this.csCExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCExport.Name = "csCExport";
            this.csCExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.csCExport.Size = new System.Drawing.Size(238, 22);
            this.csCExport.Text = "&Export Compensation...";
            this.csCExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCSep2
            // 
            this.csCSep2.Name = "csCSep2";
            this.csCSep2.Size = new System.Drawing.Size(235, 6);
            // 
            // csCPrint
            // 
            this.csCPrint.Image = global::Argix.Properties.Resources.Print;
            this.csCPrint.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCPrint.Name = "csCPrint";
            this.csCPrint.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.csCPrint.Size = new System.Drawing.Size(238, 22);
            this.csCPrint.Text = "&Print...";
            this.csCPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCSep3
            // 
            this.csCSep3.Name = "csCSep3";
            this.csCSep3.Size = new System.Drawing.Size(235, 6);
            // 
            // csCEquipOverride
            // 
            this.csCEquipOverride.Name = "csCEquipOverride";
            this.csCEquipOverride.Size = new System.Drawing.Size(238, 22);
            this.csCEquipOverride.Text = "Add Equipment Override";
            // 
            // csCSep4
            // 
            this.csCSep4.Name = "csCSep4";
            this.csCSep4.Size = new System.Drawing.Size(235, 6);
            // 
            // csCCut
            // 
            this.csCCut.Image = global::Argix.Properties.Resources.Cut;
            this.csCCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCCut.Name = "csCCut";
            this.csCCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.csCCut.Size = new System.Drawing.Size(238, 22);
            this.csCCut.Text = "Cu&t";
            this.csCCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCCopy
            // 
            this.csCCopy.Image = global::Argix.Properties.Resources.Copy;
            this.csCCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCCopy.Name = "csCCopy";
            this.csCCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.csCCopy.Size = new System.Drawing.Size(238, 22);
            this.csCCopy.Text = "&Copy";
            this.csCCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCPaste
            // 
            this.csCPaste.Image = global::Argix.Properties.Resources.Paste;
            this.csCPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCPaste.Name = "csCPaste";
            this.csCPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.csCPaste.Size = new System.Drawing.Size(238, 22);
            this.csCPaste.Text = "&Paste";
            this.csCPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCDelete
            // 
            this.csCDelete.Image = global::Argix.Properties.Resources.Delete;
            this.csCDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csCDelete.Name = "csCDelete";
            this.csCDelete.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.csCDelete.Size = new System.Drawing.Size(238, 22);
            this.csCDelete.Text = "&Delete Route(s)";
            this.csCDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCSep5
            // 
            this.csCSep5.Name = "csCSep5";
            this.csCSep5.Size = new System.Drawing.Size(235, 6);
            // 
            // csCExpandAll
            // 
            this.csCExpandAll.Name = "csCExpandAll";
            this.csCExpandAll.Size = new System.Drawing.Size(238, 22);
            this.csCExpandAll.Text = "Expand To Routes";
            this.csCExpandAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csCCollapseAll
            // 
            this.csCCollapseAll.Name = "csCCollapseAll";
            this.csCCollapseAll.Size = new System.Drawing.Size(238, 22);
            this.csCCollapseAll.Text = "Collapse To Summaries";
            this.csCCollapseAll.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tabDialog
            // 
            this.tabDialog.Alignment = System.Windows.Forms.TabAlignment.Bottom;
            this.tabDialog.Controls.Add(this.tabRoutes);
            this.tabDialog.Controls.Add(this.tabSummary);
            this.tabDialog.Controls.Add(this.tabDriverComp);
            this.tabDialog.Controls.Add(this.tabOwnerComp);
            this.tabDialog.Controls.Add(this.tabExport);
            this.tabDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDialog.ItemSize = new System.Drawing.Size(150, 24);
            this.tabDialog.Location = new System.Drawing.Point(0, 0);
            this.tabDialog.Margin = new System.Windows.Forms.Padding(0);
            this.tabDialog.Name = "tabDialog";
            this.tabDialog.Padding = new System.Drawing.Point(10, 3);
            this.tabDialog.SelectedIndex = 0;
            this.tabDialog.Size = new System.Drawing.Size(751, 387);
            this.tabDialog.TabIndex = 112;
            this.tabDialog.Selected += new System.Windows.Forms.TabControlEventHandler(this.OnTabSelected);
            // 
            // tabRoutes
            // 
            this.tabRoutes.Controls.Add(this.chkShowRates);
            this.tabRoutes.Controls.Add(this.grdCompensation);
            this.tabRoutes.Controls.Add(this.splitterH);
            this.tabRoutes.Controls.Add(this.grdRoutes);
            this.tabRoutes.Location = new System.Drawing.Point(4, 4);
            this.tabRoutes.Name = "tabRoutes";
            this.tabRoutes.Size = new System.Drawing.Size(743, 355);
            this.tabRoutes.TabIndex = 0;
            this.tabRoutes.Text = "Driver Routes";
            this.tabRoutes.UseVisualStyleBackColor = true;
            // 
            // grdCompensation
            // 
            this.grdCompensation.AllowDrop = true;
            this.grdCompensation.ContextMenuStrip = this.csComp;
            this.grdCompensation.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdCompensation.DataSource = this.mDriverCompDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdCompensation.DisplayLayout.Appearance = appearance1;
            ultraGridColumn113.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn113.Header.Caption = "Export";
            ultraGridColumn113.Header.Fixed = true;
            ultraGridColumn113.Header.ToolTipText = "Export operator compensation";
            ultraGridColumn113.Header.VisiblePosition = 0;
            ultraGridColumn113.Width = 48;
            ultraGridColumn114.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn114.Header.Caption = "N";
            ultraGridColumn114.Header.Fixed = true;
            ultraGridColumn114.Header.ToolTipText = "New Routes";
            ultraGridColumn114.Header.VisiblePosition = 1;
            ultraGridColumn114.Width = 32;
            ultraGridColumn115.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn115.Header.Caption = "C";
            ultraGridColumn115.Header.Fixed = true;
            ultraGridColumn115.Header.ToolTipText = "Combo Routes";
            ultraGridColumn115.Header.VisiblePosition = 2;
            ultraGridColumn115.Width = 32;
            ultraGridColumn116.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn116.Header.Caption = "A";
            ultraGridColumn116.Header.Fixed = true;
            ultraGridColumn116.Header.ToolTipText = "Adjustment Routes";
            ultraGridColumn116.Header.VisiblePosition = 3;
            ultraGridColumn116.Width = 32;
            ultraGridColumn117.Format = "C";
            ultraGridColumn117.Header.VisiblePosition = 7;
            ultraGridColumn117.Hidden = true;
            ultraGridColumn117.Width = 72;
            ultraGridColumn118.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn118.Header.Caption = "Vendor#";
            ultraGridColumn118.Header.Fixed = true;
            ultraGridColumn118.Header.VisiblePosition = 5;
            ultraGridColumn118.Width = 96;
            ultraGridColumn119.Header.VisiblePosition = 6;
            ultraGridColumn119.Hidden = true;
            ultraGridColumn119.Width = 168;
            ultraGridColumn120.Header.Fixed = true;
            ultraGridColumn120.Header.VisiblePosition = 4;
            ultraGridColumn120.Width = 168;
            ultraGridColumn121.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn121.Header.Caption = "Equip Type";
            ultraGridColumn121.Header.VisiblePosition = 8;
            ultraGridColumn121.Width = 96;
            ultraGridColumn122.Header.VisiblePosition = 9;
            ultraGridColumn122.Hidden = true;
            ultraGridColumn122.Width = 48;
            appearance2.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn123.CellAppearance = appearance2;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn123.Header.Appearance = appearance3;
            ultraGridColumn123.Header.Caption = "Day Rate";
            ultraGridColumn123.Header.VisiblePosition = 14;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn124.CellAppearance = appearance4;
            ultraGridColumn124.Format = "c";
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn124.Header.Appearance = appearance5;
            ultraGridColumn124.Header.Caption = "Day Amt";
            ultraGridColumn124.Header.VisiblePosition = 15;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn125.CellAppearance = appearance6;
            ultraGridColumn125.Format = "#0.0";
            appearance7.TextHAlignAsString = "Right";
            ultraGridColumn125.Header.Appearance = appearance7;
            ultraGridColumn125.Header.VisiblePosition = 10;
            ultraGridColumn125.Width = 75;
            appearance8.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance8.TextHAlignAsString = "Right";
            ultraGridColumn126.CellAppearance = appearance8;
            appearance9.TextHAlignAsString = "Right";
            ultraGridColumn126.Header.Appearance = appearance9;
            ultraGridColumn126.Header.Caption = "Mile Base Rate";
            ultraGridColumn126.Header.VisiblePosition = 11;
            ultraGridColumn126.Width = 72;
            appearance10.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance10.TextHAlignAsString = "Right";
            ultraGridColumn127.CellAppearance = appearance10;
            appearance11.TextHAlignAsString = "Right";
            ultraGridColumn127.Header.Appearance = appearance11;
            ultraGridColumn127.Header.Caption = "Mile Rate";
            ultraGridColumn127.Header.VisiblePosition = 12;
            ultraGridColumn127.Width = 72;
            appearance12.TextHAlignAsString = "Right";
            ultraGridColumn128.CellAppearance = appearance12;
            ultraGridColumn128.Format = "c";
            appearance13.TextHAlignAsString = "Right";
            ultraGridColumn128.Header.Appearance = appearance13;
            ultraGridColumn128.Header.Caption = "Mile Amt";
            ultraGridColumn128.Header.VisiblePosition = 13;
            ultraGridColumn128.Width = 81;
            appearance14.TextHAlignAsString = "Right";
            ultraGridColumn129.CellAppearance = appearance14;
            appearance15.TextHAlignAsString = "Right";
            ultraGridColumn129.Header.Appearance = appearance15;
            ultraGridColumn129.Header.Caption = "Trips";
            ultraGridColumn129.Header.VisiblePosition = 16;
            ultraGridColumn129.Width = 81;
            appearance16.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance16.TextHAlignAsString = "Right";
            ultraGridColumn130.CellAppearance = appearance16;
            appearance17.TextHAlignAsString = "Right";
            ultraGridColumn130.Header.Appearance = appearance17;
            ultraGridColumn130.Header.Caption = "Trip Rate";
            ultraGridColumn130.Header.VisiblePosition = 17;
            ultraGridColumn130.Width = 72;
            appearance18.TextHAlignAsString = "Right";
            ultraGridColumn131.CellAppearance = appearance18;
            ultraGridColumn131.Format = "c";
            appearance19.TextHAlignAsString = "Right";
            ultraGridColumn131.Header.Appearance = appearance19;
            ultraGridColumn131.Header.Caption = "Trip Amt";
            ultraGridColumn131.Header.VisiblePosition = 18;
            ultraGridColumn131.Width = 72;
            appearance20.TextHAlignAsString = "Right";
            ultraGridColumn132.CellAppearance = appearance20;
            appearance21.TextHAlignAsString = "Right";
            ultraGridColumn132.Header.Appearance = appearance21;
            ultraGridColumn132.Header.VisiblePosition = 19;
            ultraGridColumn132.Width = 72;
            appearance22.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance22.TextHAlignAsString = "Right";
            ultraGridColumn133.CellAppearance = appearance22;
            appearance23.TextHAlignAsString = "Right";
            ultraGridColumn133.Header.Appearance = appearance23;
            ultraGridColumn133.Header.Caption = "Stop Rate";
            ultraGridColumn133.Header.VisiblePosition = 20;
            ultraGridColumn133.Width = 72;
            appearance24.TextHAlignAsString = "Right";
            ultraGridColumn134.CellAppearance = appearance24;
            ultraGridColumn134.Format = "c";
            appearance25.TextHAlignAsString = "Right";
            ultraGridColumn134.Header.Appearance = appearance25;
            ultraGridColumn134.Header.Caption = "Stop Amt";
            ultraGridColumn134.Header.VisiblePosition = 21;
            ultraGridColumn134.Width = 72;
            appearance26.TextHAlignAsString = "Right";
            ultraGridColumn135.CellAppearance = appearance26;
            appearance27.TextHAlignAsString = "Right";
            ultraGridColumn135.Header.Appearance = appearance27;
            ultraGridColumn135.Header.Caption = "Ctns";
            ultraGridColumn135.Header.VisiblePosition = 22;
            ultraGridColumn135.Width = 72;
            appearance28.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance28.TextHAlignAsString = "Right";
            ultraGridColumn136.CellAppearance = appearance28;
            appearance29.TextHAlignAsString = "Right";
            ultraGridColumn136.Header.Appearance = appearance29;
            ultraGridColumn136.Header.Caption = "Ctn Rate";
            ultraGridColumn136.Header.VisiblePosition = 23;
            ultraGridColumn136.Width = 72;
            appearance30.TextHAlignAsString = "Right";
            ultraGridColumn137.CellAppearance = appearance30;
            ultraGridColumn137.Format = "c";
            appearance31.TextHAlignAsString = "Right";
            ultraGridColumn137.Header.Appearance = appearance31;
            ultraGridColumn137.Header.Caption = "Ctn Amt";
            ultraGridColumn137.Header.VisiblePosition = 24;
            ultraGridColumn137.Width = 72;
            appearance32.TextHAlignAsString = "Right";
            ultraGridColumn138.CellAppearance = appearance32;
            appearance33.TextHAlignAsString = "Right";
            ultraGridColumn138.Header.Appearance = appearance33;
            ultraGridColumn138.Header.Caption = "Pllts";
            ultraGridColumn138.Header.VisiblePosition = 28;
            ultraGridColumn138.Width = 72;
            appearance34.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance34.TextHAlignAsString = "Right";
            ultraGridColumn139.CellAppearance = appearance34;
            appearance35.TextHAlignAsString = "Right";
            ultraGridColumn139.Header.Appearance = appearance35;
            ultraGridColumn139.Header.Caption = "Pllt Rate";
            ultraGridColumn139.Header.VisiblePosition = 29;
            ultraGridColumn139.Width = 72;
            appearance36.TextHAlignAsString = "Right";
            ultraGridColumn140.CellAppearance = appearance36;
            ultraGridColumn140.Format = "c";
            appearance37.TextHAlignAsString = "Right";
            ultraGridColumn140.Header.Appearance = appearance37;
            ultraGridColumn140.Header.Caption = "Pllt Amt";
            ultraGridColumn140.Header.VisiblePosition = 30;
            ultraGridColumn140.Width = 72;
            appearance38.TextHAlignAsString = "Right";
            ultraGridColumn141.CellAppearance = appearance38;
            appearance39.TextHAlignAsString = "Right";
            ultraGridColumn141.Header.Appearance = appearance39;
            ultraGridColumn141.Header.Caption = "PU Ctns";
            ultraGridColumn141.Header.VisiblePosition = 25;
            ultraGridColumn141.Width = 72;
            appearance40.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance40.TextHAlignAsString = "Right";
            ultraGridColumn142.CellAppearance = appearance40;
            appearance41.TextHAlignAsString = "Right";
            ultraGridColumn142.Header.Appearance = appearance41;
            ultraGridColumn142.Header.Caption = "PU Ctn Rate";
            ultraGridColumn142.Header.VisiblePosition = 26;
            ultraGridColumn142.Width = 72;
            appearance42.TextHAlignAsString = "Right";
            ultraGridColumn143.CellAppearance = appearance42;
            ultraGridColumn143.Format = "c";
            appearance43.TextHAlignAsString = "Right";
            ultraGridColumn143.Header.Appearance = appearance43;
            ultraGridColumn143.Header.Caption = "PU Ctn Amt";
            ultraGridColumn143.Header.VisiblePosition = 27;
            ultraGridColumn143.Width = 72;
            appearance44.TextHAlignAsString = "Right";
            ultraGridColumn144.CellAppearance = appearance44;
            ultraGridColumn144.Format = "c";
            appearance45.TextHAlignAsString = "Right";
            ultraGridColumn144.Header.Appearance = appearance45;
            ultraGridColumn144.Header.Caption = "Min Amt";
            ultraGridColumn144.Header.VisiblePosition = 31;
            ultraGridColumn144.Width = 72;
            appearance46.TextHAlignAsString = "Right";
            ultraGridColumn145.CellAppearance = appearance46;
            ultraGridColumn145.Format = "C";
            appearance47.TextHAlignAsString = "Right";
            ultraGridColumn145.Header.Appearance = appearance47;
            ultraGridColumn145.Header.Caption = "Sum Amt";
            ultraGridColumn145.Header.VisiblePosition = 32;
            ultraGridColumn145.Width = 72;
            appearance48.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance48.TextHAlignAsString = "Right";
            ultraGridColumn146.CellAppearance = appearance48;
            ultraGridColumn146.Format = "C";
            appearance49.TextHAlignAsString = "Right";
            ultraGridColumn146.Header.Appearance = appearance49;
            ultraGridColumn146.Header.Caption = "Fuel Cost";
            ultraGridColumn146.Header.VisiblePosition = 35;
            ultraGridColumn146.Width = 72;
            appearance50.TextHAlignAsString = "Right";
            ultraGridColumn147.CellAppearance = appearance50;
            ultraGridColumn147.Format = "";
            appearance51.TextHAlignAsString = "Right";
            ultraGridColumn147.Header.Appearance = appearance51;
            ultraGridColumn147.Header.Caption = "FSC Gal";
            ultraGridColumn147.Header.VisiblePosition = 34;
            ultraGridColumn147.Width = 72;
            appearance52.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance52.TextHAlignAsString = "Right";
            ultraGridColumn148.CellAppearance = appearance52;
            ultraGridColumn148.Format = "C";
            appearance53.TextHAlignAsString = "Right";
            ultraGridColumn148.Header.Appearance = appearance53;
            ultraGridColumn148.Header.Caption = "FSC Rate";
            ultraGridColumn148.Header.VisiblePosition = 36;
            ultraGridColumn148.Width = 72;
            appearance54.TextHAlignAsString = "Right";
            ultraGridColumn149.CellAppearance = appearance54;
            ultraGridColumn149.Format = "C";
            appearance55.TextHAlignAsString = "Right";
            ultraGridColumn149.Header.Appearance = appearance55;
            ultraGridColumn149.Header.VisiblePosition = 37;
            ultraGridColumn149.Width = 72;
            appearance56.TextHAlignAsString = "Right";
            ultraGridColumn150.CellAppearance = appearance56;
            ultraGridColumn150.Format = "C";
            appearance57.TextHAlignAsString = "Right";
            ultraGridColumn150.Header.Appearance = appearance57;
            ultraGridColumn150.Header.Caption = "Adj1 Amt";
            ultraGridColumn150.Header.VisiblePosition = 39;
            ultraGridColumn150.Width = 96;
            appearance58.TextHAlignAsString = "Right";
            ultraGridColumn151.CellAppearance = appearance58;
            ultraGridColumn151.Format = "C";
            appearance59.TextHAlignAsString = "Right";
            ultraGridColumn151.Header.Appearance = appearance59;
            ultraGridColumn151.Header.Caption = "Adj2 Amt";
            ultraGridColumn151.Header.VisiblePosition = 40;
            ultraGridColumn151.Width = 96;
            appearance60.TextHAlignAsString = "Right";
            ultraGridColumn152.CellAppearance = appearance60;
            ultraGridColumn152.Format = "C";
            appearance61.TextHAlignAsString = "Right";
            ultraGridColumn152.Header.Appearance = appearance61;
            ultraGridColumn152.Header.Caption = "Admin Fee";
            ultraGridColumn152.Header.VisiblePosition = 38;
            ultraGridColumn152.Width = 72;
            appearance62.TextHAlignAsString = "Right";
            ultraGridColumn153.CellAppearance = appearance62;
            ultraGridColumn153.Format = "C";
            appearance63.TextHAlignAsString = "Right";
            ultraGridColumn153.Header.Appearance = appearance63;
            ultraGridColumn153.Header.Caption = "Total Amt";
            ultraGridColumn153.Header.VisiblePosition = 41;
            ultraGridColumn153.Width = 96;
            appearance64.TextHAlignAsString = "Right";
            ultraGridColumn154.CellAppearance = appearance64;
            ultraGridColumn154.Format = "#0.0";
            appearance65.TextHAlignAsString = "Right";
            ultraGridColumn154.Header.Appearance = appearance65;
            ultraGridColumn154.Header.Caption = "FSC Miles";
            ultraGridColumn154.Header.VisiblePosition = 33;
            ultraGridColumn154.Width = 72;
            ultraGridColumn155.Header.VisiblePosition = 42;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn113,
            ultraGridColumn114,
            ultraGridColumn115,
            ultraGridColumn116,
            ultraGridColumn117,
            ultraGridColumn118,
            ultraGridColumn119,
            ultraGridColumn120,
            ultraGridColumn121,
            ultraGridColumn122,
            ultraGridColumn123,
            ultraGridColumn124,
            ultraGridColumn125,
            ultraGridColumn126,
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
            ultraGridColumn155});
            appearance66.BackColor = System.Drawing.SystemColors.Info;
            ultraGridBand1.Override.RowAppearance = appearance66;
            ultraGridBand1.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            ultraGridColumn156.Header.Fixed = true;
            ultraGridColumn156.Header.VisiblePosition = 0;
            ultraGridColumn156.Width = 29;
            ultraGridColumn157.Header.Caption = "N";
            ultraGridColumn157.Header.Fixed = true;
            ultraGridColumn157.Header.ToolTipText = "New Routes";
            ultraGridColumn157.Header.VisiblePosition = 1;
            ultraGridColumn157.Width = 32;
            ultraGridColumn158.Header.Caption = "C";
            ultraGridColumn158.Header.Fixed = true;
            ultraGridColumn158.Header.ToolTipText = "Combo Routes";
            ultraGridColumn158.Header.VisiblePosition = 2;
            ultraGridColumn158.Width = 32;
            ultraGridColumn159.Header.Caption = "A";
            ultraGridColumn159.Header.Fixed = true;
            ultraGridColumn159.Header.ToolTipText = "Adjustment Routes";
            ultraGridColumn159.Header.VisiblePosition = 3;
            ultraGridColumn159.Width = 32;
            ultraGridColumn160.Header.VisiblePosition = 10;
            ultraGridColumn160.Hidden = true;
            ultraGridColumn160.Width = 75;
            ultraGridColumn161.Header.VisiblePosition = 9;
            ultraGridColumn161.Hidden = true;
            ultraGridColumn161.Width = 49;
            ultraGridColumn162.Header.VisiblePosition = 6;
            ultraGridColumn162.Hidden = true;
            ultraGridColumn163.Format = "D";
            ultraGridColumn163.Header.Caption = "Date";
            ultraGridColumn163.Header.Fixed = true;
            ultraGridColumn163.Header.VisiblePosition = 4;
            ultraGridColumn163.Width = 168;
            ultraGridColumn164.Header.Caption = "Route";
            ultraGridColumn164.Header.Fixed = true;
            ultraGridColumn164.Header.VisiblePosition = 5;
            ultraGridColumn164.Width = 96;
            ultraGridColumn165.Header.VisiblePosition = 7;
            ultraGridColumn165.Hidden = true;
            ultraGridColumn2.Header.VisiblePosition = 11;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn166.Header.VisiblePosition = 8;
            ultraGridColumn166.Hidden = true;
            ultraGridColumn167.Header.Caption = "Equip Type";
            ultraGridColumn167.Header.VisiblePosition = 12;
            ultraGridColumn168.Header.Caption = "Rate Type";
            ultraGridColumn168.Header.VisiblePosition = 46;
            ultraGridColumn168.Width = 100;
            appearance67.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance67.TextHAlignAsString = "Right";
            ultraGridColumn169.CellAppearance = appearance67;
            appearance68.TextHAlignAsString = "Right";
            ultraGridColumn169.Header.Appearance = appearance68;
            ultraGridColumn169.Header.Caption = "Day Rate";
            ultraGridColumn169.Header.VisiblePosition = 17;
            appearance69.TextHAlignAsString = "Right";
            ultraGridColumn170.CellAppearance = appearance69;
            ultraGridColumn170.Format = "c";
            appearance70.TextHAlignAsString = "Right";
            ultraGridColumn170.Header.Appearance = appearance70;
            ultraGridColumn170.Header.Caption = "Day Amt";
            ultraGridColumn170.Header.VisiblePosition = 18;
            appearance71.TextHAlignAsString = "Right";
            ultraGridColumn171.CellAppearance = appearance71;
            ultraGridColumn171.Format = "#0.0";
            appearance72.TextHAlignAsString = "Right";
            ultraGridColumn171.Header.Appearance = appearance72;
            ultraGridColumn171.Header.VisiblePosition = 13;
            ultraGridColumn171.Width = 75;
            appearance73.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance73.TextHAlignAsString = "Right";
            ultraGridColumn172.CellAppearance = appearance73;
            appearance74.TextHAlignAsString = "Right";
            ultraGridColumn172.Header.Appearance = appearance74;
            ultraGridColumn172.Header.Caption = "Mile Base Rate";
            ultraGridColumn172.Header.VisiblePosition = 14;
            ultraGridColumn172.Width = 72;
            appearance75.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance75.TextHAlignAsString = "Right";
            ultraGridColumn173.CellAppearance = appearance75;
            appearance76.TextHAlignAsString = "Right";
            ultraGridColumn173.Header.Appearance = appearance76;
            ultraGridColumn173.Header.Caption = "Mile Rate";
            ultraGridColumn173.Header.VisiblePosition = 15;
            ultraGridColumn173.Width = 72;
            appearance77.TextHAlignAsString = "Right";
            ultraGridColumn174.CellAppearance = appearance77;
            ultraGridColumn174.Format = "c";
            appearance78.TextHAlignAsString = "Right";
            ultraGridColumn174.Header.Appearance = appearance78;
            ultraGridColumn174.Header.Caption = "Mile Amt";
            ultraGridColumn174.Header.VisiblePosition = 16;
            ultraGridColumn174.Width = 81;
            appearance79.TextHAlignAsString = "Right";
            ultraGridColumn175.CellAppearance = appearance79;
            appearance80.TextHAlignAsString = "Right";
            ultraGridColumn175.Header.Appearance = appearance80;
            ultraGridColumn175.Header.Caption = "Trips";
            ultraGridColumn175.Header.VisiblePosition = 19;
            ultraGridColumn175.Width = 81;
            appearance81.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance81.TextHAlignAsString = "Right";
            ultraGridColumn176.CellAppearance = appearance81;
            appearance82.TextHAlignAsString = "Right";
            ultraGridColumn176.Header.Appearance = appearance82;
            ultraGridColumn176.Header.Caption = "Trip Rate";
            ultraGridColumn176.Header.VisiblePosition = 20;
            ultraGridColumn176.Width = 72;
            appearance83.TextHAlignAsString = "Right";
            ultraGridColumn177.CellAppearance = appearance83;
            ultraGridColumn177.Format = "c";
            appearance84.TextHAlignAsString = "Right";
            ultraGridColumn177.Header.Appearance = appearance84;
            ultraGridColumn177.Header.Caption = "Trip Amt";
            ultraGridColumn177.Header.VisiblePosition = 21;
            ultraGridColumn177.Width = 72;
            appearance85.TextHAlignAsString = "Right";
            ultraGridColumn178.CellAppearance = appearance85;
            appearance86.TextHAlignAsString = "Right";
            ultraGridColumn178.Header.Appearance = appearance86;
            ultraGridColumn178.Header.VisiblePosition = 22;
            ultraGridColumn178.Width = 72;
            appearance87.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance87.TextHAlignAsString = "Right";
            ultraGridColumn179.CellAppearance = appearance87;
            appearance88.TextHAlignAsString = "Right";
            ultraGridColumn179.Header.Appearance = appearance88;
            ultraGridColumn179.Header.Caption = "Stop Rate";
            ultraGridColumn179.Header.VisiblePosition = 23;
            ultraGridColumn179.Width = 72;
            appearance89.TextHAlignAsString = "Right";
            ultraGridColumn180.CellAppearance = appearance89;
            ultraGridColumn180.Format = "c";
            appearance90.TextHAlignAsString = "Right";
            ultraGridColumn180.Header.Appearance = appearance90;
            ultraGridColumn180.Header.Caption = "Stop Amt";
            ultraGridColumn180.Header.VisiblePosition = 24;
            ultraGridColumn180.Width = 72;
            appearance91.TextHAlignAsString = "Right";
            ultraGridColumn181.CellAppearance = appearance91;
            appearance92.TextHAlignAsString = "Right";
            ultraGridColumn181.Header.Appearance = appearance92;
            ultraGridColumn181.Header.Caption = "Ctns";
            ultraGridColumn181.Header.VisiblePosition = 25;
            ultraGridColumn181.Width = 72;
            appearance93.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance93.TextHAlignAsString = "Right";
            ultraGridColumn182.CellAppearance = appearance93;
            appearance94.TextHAlignAsString = "Right";
            ultraGridColumn182.Header.Appearance = appearance94;
            ultraGridColumn182.Header.Caption = "Ctn Rate";
            ultraGridColumn182.Header.VisiblePosition = 26;
            ultraGridColumn182.Width = 72;
            appearance95.TextHAlignAsString = "Right";
            ultraGridColumn183.CellAppearance = appearance95;
            ultraGridColumn183.Format = "c";
            appearance96.TextHAlignAsString = "Right";
            ultraGridColumn183.Header.Appearance = appearance96;
            ultraGridColumn183.Header.Caption = "Ctn Amt";
            ultraGridColumn183.Header.VisiblePosition = 27;
            ultraGridColumn183.Width = 72;
            appearance97.TextHAlignAsString = "Right";
            ultraGridColumn184.CellAppearance = appearance97;
            appearance98.TextHAlignAsString = "Right";
            ultraGridColumn184.Header.Appearance = appearance98;
            ultraGridColumn184.Header.Caption = "Pllts";
            ultraGridColumn184.Header.VisiblePosition = 31;
            ultraGridColumn184.Width = 72;
            appearance99.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance99.TextHAlignAsString = "Right";
            ultraGridColumn185.CellAppearance = appearance99;
            appearance100.TextHAlignAsString = "Right";
            ultraGridColumn185.Header.Appearance = appearance100;
            ultraGridColumn185.Header.Caption = "Pllt Rate";
            ultraGridColumn185.Header.VisiblePosition = 32;
            ultraGridColumn185.Width = 72;
            appearance101.TextHAlignAsString = "Right";
            ultraGridColumn186.CellAppearance = appearance101;
            ultraGridColumn186.Format = "c";
            appearance102.TextHAlignAsString = "Right";
            ultraGridColumn186.Header.Appearance = appearance102;
            ultraGridColumn186.Header.Caption = "Pllt Amt";
            ultraGridColumn186.Header.VisiblePosition = 33;
            ultraGridColumn186.Width = 72;
            appearance103.TextHAlignAsString = "Right";
            ultraGridColumn187.CellAppearance = appearance103;
            appearance104.TextHAlignAsString = "Right";
            ultraGridColumn187.Header.Appearance = appearance104;
            ultraGridColumn187.Header.Caption = "PU Ctns";
            ultraGridColumn187.Header.VisiblePosition = 28;
            ultraGridColumn187.Width = 72;
            appearance105.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance105.TextHAlignAsString = "Right";
            ultraGridColumn188.CellAppearance = appearance105;
            appearance106.TextHAlignAsString = "Right";
            ultraGridColumn188.Header.Appearance = appearance106;
            ultraGridColumn188.Header.Caption = "PU Ctn Rate";
            ultraGridColumn188.Header.VisiblePosition = 29;
            ultraGridColumn188.Width = 72;
            appearance107.TextHAlignAsString = "Right";
            ultraGridColumn189.CellAppearance = appearance107;
            ultraGridColumn189.Format = "c";
            appearance108.TextHAlignAsString = "Right";
            ultraGridColumn189.Header.Appearance = appearance108;
            ultraGridColumn189.Header.Caption = "PU Ctn Amt";
            ultraGridColumn189.Header.VisiblePosition = 30;
            ultraGridColumn189.Width = 72;
            appearance109.TextHAlignAsString = "Right";
            ultraGridColumn190.CellAppearance = appearance109;
            ultraGridColumn190.Format = "c";
            appearance110.TextHAlignAsString = "Right";
            ultraGridColumn190.Header.Appearance = appearance110;
            ultraGridColumn190.Header.Caption = "Min Amt";
            ultraGridColumn190.Header.VisiblePosition = 34;
            ultraGridColumn190.Width = 72;
            appearance111.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance111.TextHAlignAsString = "Right";
            ultraGridColumn191.CellAppearance = appearance111;
            ultraGridColumn191.Format = "C";
            appearance112.TextHAlignAsString = "Right";
            ultraGridColumn191.Header.Appearance = appearance112;
            ultraGridColumn191.Header.Caption = "Fuel Cost";
            ultraGridColumn191.Header.VisiblePosition = 38;
            ultraGridColumn191.Width = 72;
            appearance113.TextHAlignAsString = "Right";
            ultraGridColumn192.CellAppearance = appearance113;
            ultraGridColumn192.Format = "";
            appearance114.TextHAlignAsString = "Right";
            ultraGridColumn192.Header.Appearance = appearance114;
            ultraGridColumn192.Header.Caption = "FSC Gal";
            ultraGridColumn192.Header.VisiblePosition = 37;
            ultraGridColumn192.Width = 72;
            appearance115.BackColor = System.Drawing.SystemColors.ButtonFace;
            appearance115.TextHAlignAsString = "Right";
            ultraGridColumn193.CellAppearance = appearance115;
            ultraGridColumn193.Format = "C";
            appearance116.TextHAlignAsString = "Right";
            ultraGridColumn193.Header.Appearance = appearance116;
            ultraGridColumn193.Header.Caption = "FSC Rate";
            ultraGridColumn193.Header.VisiblePosition = 39;
            ultraGridColumn193.Width = 72;
            appearance117.TextHAlignAsString = "Right";
            ultraGridColumn194.CellAppearance = appearance117;
            ultraGridColumn194.Format = "C";
            appearance118.TextHAlignAsString = "Right";
            ultraGridColumn194.Header.Appearance = appearance118;
            ultraGridColumn194.Header.VisiblePosition = 40;
            ultraGridColumn194.Width = 72;
            appearance119.TextHAlignAsString = "Right";
            ultraGridColumn195.CellAppearance = appearance119;
            ultraGridColumn195.Format = "C";
            appearance120.TextHAlignAsString = "Right";
            ultraGridColumn195.Header.Appearance = appearance120;
            ultraGridColumn195.Header.Caption = "Adj1 Amt";
            ultraGridColumn195.Header.VisiblePosition = 42;
            ultraGridColumn195.Width = 96;
            appearance121.TextHAlignAsString = "Right";
            ultraGridColumn196.CellAppearance = appearance121;
            appearance122.TextHAlignAsString = "Right";
            ultraGridColumn196.Header.Appearance = appearance122;
            ultraGridColumn196.Header.Caption = "Adj1 Type";
            ultraGridColumn196.Header.VisiblePosition = 43;
            ultraGridColumn196.Width = 96;
            appearance123.TextHAlignAsString = "Right";
            ultraGridColumn197.CellAppearance = appearance123;
            ultraGridColumn197.Format = "C";
            appearance124.TextHAlignAsString = "Right";
            ultraGridColumn197.Header.Appearance = appearance124;
            ultraGridColumn197.Header.Caption = "Adj2 Amt";
            ultraGridColumn197.Header.VisiblePosition = 44;
            ultraGridColumn197.Width = 96;
            appearance125.TextHAlignAsString = "Right";
            ultraGridColumn198.CellAppearance = appearance125;
            appearance126.TextHAlignAsString = "Right";
            ultraGridColumn198.Header.Appearance = appearance126;
            ultraGridColumn198.Header.Caption = "Adj2 Type";
            ultraGridColumn198.Header.VisiblePosition = 45;
            ultraGridColumn198.Width = 96;
            appearance127.TextHAlignAsString = "Right";
            ultraGridColumn199.CellAppearance = appearance127;
            ultraGridColumn199.Format = "C";
            appearance128.TextHAlignAsString = "Right";
            ultraGridColumn199.Header.Appearance = appearance128;
            ultraGridColumn199.Header.Caption = "Admin Fee";
            ultraGridColumn199.Header.VisiblePosition = 41;
            ultraGridColumn199.Width = 72;
            appearance129.TextHAlignAsString = "Right";
            ultraGridColumn200.CellAppearance = appearance129;
            ultraGridColumn200.Format = "C";
            appearance130.TextHAlignAsString = "Right";
            ultraGridColumn200.Header.Appearance = appearance130;
            ultraGridColumn200.Header.Caption = "Daily Amt";
            ultraGridColumn200.Header.VisiblePosition = 35;
            ultraGridColumn200.Width = 72;
            ultraGridColumn201.Header.VisiblePosition = 47;
            ultraGridColumn201.Width = 96;
            ultraGridColumn202.Header.VisiblePosition = 48;
            ultraGridColumn202.Hidden = true;
            ultraGridColumn203.Header.VisiblePosition = 49;
            ultraGridColumn203.Hidden = true;
            ultraGridColumn204.Header.VisiblePosition = 50;
            ultraGridColumn204.Hidden = true;
            ultraGridColumn205.Header.VisiblePosition = 51;
            ultraGridColumn205.Hidden = true;
            appearance131.TextHAlignAsString = "Right";
            ultraGridColumn206.CellAppearance = appearance131;
            ultraGridColumn206.Format = "#0.0";
            appearance132.TextHAlignAsString = "Right";
            ultraGridColumn206.Header.Appearance = appearance132;
            ultraGridColumn206.Header.Caption = "FSC Miles";
            ultraGridColumn206.Header.VisiblePosition = 36;
            ultraGridColumn206.Width = 72;
            ultraGridColumn3.Header.VisiblePosition = 52;
            ultraGridColumn3.Hidden = true;
            ultraGridBand2.Columns.AddRange(new object[] {
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
            ultraGridColumn2,
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
            ultraGridColumn189,
            ultraGridColumn190,
            ultraGridColumn191,
            ultraGridColumn192,
            ultraGridColumn193,
            ultraGridColumn194,
            ultraGridColumn195,
            ultraGridColumn196,
            ultraGridColumn197,
            ultraGridColumn198,
            ultraGridColumn199,
            ultraGridColumn200,
            ultraGridColumn201,
            ultraGridColumn202,
            ultraGridColumn203,
            ultraGridColumn204,
            ultraGridColumn205,
            ultraGridColumn206,
            ultraGridColumn3});
            appearance133.BackColor = System.Drawing.SystemColors.ControlLight;
            ultraGridBand2.Override.RowAlternateAppearance = appearance133;
            this.grdCompensation.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdCompensation.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            this.grdCompensation.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.WindowsVista;
            appearance134.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance134.FontData.BoldAsString = "True";
            appearance134.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.grdCompensation.DisplayLayout.CaptionAppearance = appearance134;
            this.grdCompensation.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdCompensation.DisplayLayout.Override.AllowColMoving = Infragistics.Win.UltraWinGrid.AllowColMoving.NotAllowed;
            this.grdCompensation.DisplayLayout.Override.AllowColSwapping = Infragistics.Win.UltraWinGrid.AllowColSwapping.NotAllowed;
            this.grdCompensation.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdCompensation.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.grdCompensation.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdCompensation.DisplayLayout.Override.FilterUIType = Infragistics.Win.UltraWinGrid.FilterUIType.HeaderIcons;
            this.grdCompensation.DisplayLayout.Override.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            appearance135.BackColor = System.Drawing.SystemColors.Control;
            appearance135.FontData.BoldAsString = "True";
            appearance135.TextHAlignAsString = "Left";
            this.grdCompensation.DisplayLayout.Override.HeaderAppearance = appearance135;
            this.grdCompensation.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            appearance136.BorderColor = System.Drawing.SystemColors.WindowText;
            this.grdCompensation.DisplayLayout.Override.RowAppearance = appearance136;
            this.grdCompensation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
            this.grdCompensation.DisplayLayout.Override.RowSizing = Infragistics.Win.UltraWinGrid.RowSizing.Fixed;
            this.grdCompensation.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdCompensation.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdCompensation.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdCompensation.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdCompensation.DisplayLayout.UseFixedHeaders = true;
            this.grdCompensation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCompensation.Location = new System.Drawing.Point(0, 0);
            this.grdCompensation.Name = "grdCompensation";
            this.grdCompensation.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdCompensation.Size = new System.Drawing.Size(743, 219);
            this.grdCompensation.TabIndex = 1;
            this.grdCompensation.Text = "Compensation";
            this.grdCompensation.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdCompensation.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdCompensation.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridCInitializeLayout);
            this.grdCompensation.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridCInitializeRow);
            this.grdCompensation.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridCAfterRowUpdate);
            this.grdCompensation.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.OnGridCBeforeExitEditMode);
            this.grdCompensation.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridCBeforeRowFilterDropDownPopulate);
            this.grdCompensation.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridCKeyUp);
            this.grdCompensation.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridCMouseDown);
            // 
            // mDriverCompDS
            // 
            this.mDriverCompDS.DataSetName = "DriverCompDataset";
            this.mDriverCompDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 219);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(743, 3);
            this.splitterH.TabIndex = 112;
            this.splitterH.TabStop = false;
            // 
            // grdRoutes
            // 
            this.grdRoutes.ContextMenuStrip = this.csRoutes;
            this.grdRoutes.Controls.Add(this.lblCloseRoutes);
            this.grdRoutes.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdRoutes.DataMember = "RoadshowRouteTable";
            this.grdRoutes.DataSource = this.mDriverCompDS;
            appearance137.BackColor = System.Drawing.SystemColors.Window;
            appearance137.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance137.TextHAlignAsString = "Left";
            this.grdRoutes.DisplayLayout.Appearance = appearance137;
            ultraGridColumn207.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn207.Header.Caption = "";
            ultraGridColumn207.Header.VisiblePosition = 0;
            ultraGridColumn207.Width = 48;
            ultraGridColumn208.Header.VisiblePosition = 1;
            ultraGridColumn208.Hidden = true;
            ultraGridColumn209.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn209.Format = "D";
            ultraGridColumn209.Header.Caption = "Route Date";
            ultraGridColumn209.Header.VisiblePosition = 8;
            ultraGridColumn209.Width = 144;
            ultraGridColumn210.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn210.Header.Caption = "Route";
            ultraGridColumn210.Header.VisiblePosition = 9;
            ultraGridColumn210.Width = 72;
            ultraGridColumn211.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn211.Header.VisiblePosition = 6;
            ultraGridColumn211.Width = 168;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn1.Format = "MM/dd/yyyy";
            ultraGridColumn1.Header.Caption = "Hire Date";
            ultraGridColumn1.Header.VisiblePosition = 7;
            ultraGridColumn1.Width = 75;
            ultraGridColumn212.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn212.Header.Caption = "Vehicle Type";
            ultraGridColumn212.Header.VisiblePosition = 10;
            ultraGridColumn212.Width = 72;
            ultraGridColumn213.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn213.Header.VisiblePosition = 5;
            ultraGridColumn213.Hidden = true;
            ultraGridColumn213.Width = 96;
            ultraGridColumn214.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn214.Header.Caption = "Vendor#";
            ultraGridColumn214.Header.VisiblePosition = 4;
            ultraGridColumn214.Width = 72;
            ultraGridColumn215.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance138.TextHAlignAsString = "Right";
            ultraGridColumn215.CellAppearance = appearance138;
            ultraGridColumn215.Format = "#0.0";
            appearance139.TextHAlignAsString = "Right";
            ultraGridColumn215.Header.Appearance = appearance139;
            ultraGridColumn215.Header.Caption = "Miles";
            ultraGridColumn215.Header.VisiblePosition = 12;
            ultraGridColumn215.Width = 72;
            ultraGridColumn216.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance140.TextHAlignAsString = "Right";
            ultraGridColumn216.CellAppearance = appearance140;
            appearance141.TextHAlignAsString = "Right";
            ultraGridColumn216.Header.Appearance = appearance141;
            ultraGridColumn216.Header.Caption = "Trips";
            ultraGridColumn216.Header.VisiblePosition = 13;
            ultraGridColumn216.Width = 72;
            ultraGridColumn217.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance142.TextHAlignAsString = "Right";
            ultraGridColumn217.CellAppearance = appearance142;
            appearance143.TextHAlignAsString = "Right";
            ultraGridColumn217.Header.Appearance = appearance143;
            ultraGridColumn217.Header.Caption = "Stops";
            ultraGridColumn217.Header.VisiblePosition = 14;
            ultraGridColumn217.Width = 72;
            ultraGridColumn218.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance144.TextHAlignAsString = "Right";
            ultraGridColumn218.CellAppearance = appearance144;
            appearance145.TextHAlignAsString = "Right";
            ultraGridColumn218.Header.Appearance = appearance145;
            ultraGridColumn218.Header.Caption = "Ctns";
            ultraGridColumn218.Header.VisiblePosition = 15;
            ultraGridColumn218.Width = 72;
            ultraGridColumn219.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance146.TextHAlignAsString = "Right";
            ultraGridColumn219.CellAppearance = appearance146;
            appearance147.TextHAlignAsString = "Right";
            ultraGridColumn219.Header.Appearance = appearance147;
            ultraGridColumn219.Header.Caption = "Rtn Ctns";
            ultraGridColumn219.Header.VisiblePosition = 16;
            ultraGridColumn219.Width = 72;
            ultraGridColumn220.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            appearance148.TextHAlignAsString = "Right";
            ultraGridColumn220.CellAppearance = appearance148;
            appearance149.TextHAlignAsString = "Right";
            ultraGridColumn220.Header.Appearance = appearance149;
            ultraGridColumn220.Header.Caption = "Pllts";
            ultraGridColumn220.Header.VisiblePosition = 17;
            ultraGridColumn220.Width = 72;
            ultraGridColumn221.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn221.Header.VisiblePosition = 3;
            ultraGridColumn221.Hidden = true;
            ultraGridColumn221.Width = 96;
            ultraGridColumn222.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn222.Header.Caption = "Depot#";
            ultraGridColumn222.Header.VisiblePosition = 2;
            ultraGridColumn222.Hidden = true;
            ultraGridColumn222.Width = 72;
            ultraGridColumn223.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn223.Header.Caption = "Equip Type";
            ultraGridColumn223.Header.VisiblePosition = 11;
            ultraGridColumn223.Width = 78;
            ultraGridColumn224.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            ultraGridColumn224.Header.Caption = "Argix Rt Type";
            ultraGridColumn224.Header.VisiblePosition = 18;
            ultraGridBand3.Columns.AddRange(new object[] {
            ultraGridColumn207,
            ultraGridColumn208,
            ultraGridColumn209,
            ultraGridColumn210,
            ultraGridColumn211,
            ultraGridColumn1,
            ultraGridColumn212,
            ultraGridColumn213,
            ultraGridColumn214,
            ultraGridColumn215,
            ultraGridColumn216,
            ultraGridColumn217,
            ultraGridColumn218,
            ultraGridColumn219,
            ultraGridColumn220,
            ultraGridColumn221,
            ultraGridColumn222,
            ultraGridColumn223,
            ultraGridColumn224});
            ultraGridBand3.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            ultraGridBand3.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.Edit;
            this.grdRoutes.DisplayLayout.BandsSerializer.Add(ultraGridBand3);
            this.grdRoutes.DisplayLayout.BorderStyleCaption = Infragistics.Win.UIElementBorderStyle.WindowsVista;
            appearance150.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance150.FontData.BoldAsString = "True";
            appearance150.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.grdRoutes.DisplayLayout.CaptionAppearance = appearance150;
            this.grdRoutes.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdRoutes.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdRoutes.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            this.grdRoutes.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdRoutes.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.CellSelect;
            appearance151.BackColor = System.Drawing.SystemColors.Control;
            appearance151.FontData.BoldAsString = "True";
            appearance151.TextHAlignAsString = "Left";
            this.grdRoutes.DisplayLayout.Override.HeaderAppearance = appearance151;
            this.grdRoutes.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdRoutes.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance152.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdRoutes.DisplayLayout.Override.RowAppearance = appearance152;
            this.grdRoutes.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdRoutes.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdRoutes.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdRoutes.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdRoutes.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdRoutes.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdRoutes.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdRoutes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grdRoutes.Location = new System.Drawing.Point(0, 222);
            this.grdRoutes.Name = "grdRoutes";
            this.grdRoutes.Size = new System.Drawing.Size(743, 133);
            this.grdRoutes.TabIndex = 119;
            this.grdRoutes.Text = "Roadshow Routes";
            this.grdRoutes.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdRoutes.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdRoutes.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridRInitializeLayout);
            this.grdRoutes.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridRInitializeRow);
            this.grdRoutes.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridRAfterSelectChange);
            this.grdRoutes.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnGridRBeforeCellActivate);
            this.grdRoutes.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridRBeforeRowFilterDropDownPopulate);
            this.grdRoutes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridRMouseDown);
            // 
            // lblCloseRoutes
            // 
            this.lblCloseRoutes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCloseRoutes.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.lblCloseRoutes.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.lblCloseRoutes.Location = new System.Drawing.Point(724, 3);
            this.lblCloseRoutes.Name = "lblCloseRoutes";
            this.lblCloseRoutes.Size = new System.Drawing.Size(16, 16);
            this.lblCloseRoutes.TabIndex = 120;
            this.lblCloseRoutes.Text = "X";
            this.lblCloseRoutes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblCloseRoutes.Click += new System.EventHandler(this.OnCloseRoutes);
            // 
            // tabSummary
            // 
            this.tabSummary.Controls.Add(this.rsvSummary);
            this.tabSummary.Location = new System.Drawing.Point(4, 4);
            this.tabSummary.Name = "tabSummary";
            this.tabSummary.Padding = new System.Windows.Forms.Padding(3);
            this.tabSummary.Size = new System.Drawing.Size(743, 355);
            this.tabSummary.TabIndex = 1;
            this.tabSummary.Text = "Cost/Carton Summary";
            this.tabSummary.UseVisualStyleBackColor = true;
            // 
            // rsvSummary
            // 
            this.rsvSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsvSummary.Location = new System.Drawing.Point(3, 3);
            this.rsvSummary.Name = "rsvSummary";
            this.rsvSummary.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsvSummary.ServerReport.DisplayName = "Driver Report";
            this.rsvSummary.ServerReport.ReportPath = "/Finance/Driver Summary";
            this.rsvSummary.ShowBackButton = false;
            this.rsvSummary.ShowCredentialPrompts = false;
            this.rsvSummary.ShowDocumentMapButton = false;
            this.rsvSummary.ShowFindControls = false;
            this.rsvSummary.ShowPageNavigationControls = false;
            this.rsvSummary.ShowParameterPrompts = false;
            this.rsvSummary.ShowProgress = false;
            this.rsvSummary.ShowPromptAreaButton = false;
            this.rsvSummary.ShowStopButton = false;
            this.rsvSummary.ShowToolBar = false;
            this.rsvSummary.Size = new System.Drawing.Size(737, 349);
            this.rsvSummary.TabIndex = 1;
            // 
            // tabDriverComp
            // 
            this.tabDriverComp.Controls.Add(this.rsvDrivers);
            this.tabDriverComp.Controls.Add(this.cboOperators);
            this.tabDriverComp.Location = new System.Drawing.Point(4, 4);
            this.tabDriverComp.Name = "tabDriverComp";
            this.tabDriverComp.Size = new System.Drawing.Size(743, 355);
            this.tabDriverComp.TabIndex = 2;
            this.tabDriverComp.Text = "Driver Compensation";
            this.tabDriverComp.UseVisualStyleBackColor = true;
            // 
            // rsvDrivers
            // 
            this.rsvDrivers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsvDrivers.Location = new System.Drawing.Point(0, 21);
            this.rsvDrivers.Name = "rsvDrivers";
            this.rsvDrivers.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsvDrivers.ServerReport.DisplayName = "Driver Report";
            this.rsvDrivers.ServerReport.ReportPath = "/Finance/Driver Compensation";
            this.rsvDrivers.ShowBackButton = false;
            this.rsvDrivers.ShowCredentialPrompts = false;
            this.rsvDrivers.ShowDocumentMapButton = false;
            this.rsvDrivers.ShowFindControls = false;
            this.rsvDrivers.ShowParameterPrompts = false;
            this.rsvDrivers.ShowPromptAreaButton = false;
            this.rsvDrivers.ShowStopButton = false;
            this.rsvDrivers.Size = new System.Drawing.Size(743, 334);
            this.rsvDrivers.TabIndex = 1;
            // 
            // cboOperators
            // 
            this.cboOperators.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboOperators.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOperators.FormattingEnabled = true;
            this.cboOperators.Location = new System.Drawing.Point(0, 0);
            this.cboOperators.Name = "cboOperators";
            this.cboOperators.Size = new System.Drawing.Size(743, 21);
            this.cboOperators.Sorted = true;
            this.cboOperators.TabIndex = 2;
            this.cboOperators.SelectionChangeCommitted += new System.EventHandler(this.OnOperatorChanged);
            // 
            // tabOwnerComp
            // 
            this.tabOwnerComp.Controls.Add(this.rsOwners);
            this.tabOwnerComp.Controls.Add(this.cboOwners);
            this.tabOwnerComp.Location = new System.Drawing.Point(4, 4);
            this.tabOwnerComp.Name = "tabOwnerComp";
            this.tabOwnerComp.Size = new System.Drawing.Size(743, 355);
            this.tabOwnerComp.TabIndex = 4;
            this.tabOwnerComp.Text = "Fleet Owner Compensation";
            this.tabOwnerComp.UseVisualStyleBackColor = true;
            // 
            // rsOwners
            // 
            this.rsOwners.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsOwners.Location = new System.Drawing.Point(0, 21);
            this.rsOwners.Name = "rsOwners";
            this.rsOwners.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsOwners.ServerReport.DisplayName = "Fleet Owner Report";
            this.rsOwners.ServerReport.ReportPath = "/Finance/Fleet Owner Compensation";
            this.rsOwners.ShowBackButton = false;
            this.rsOwners.ShowCredentialPrompts = false;
            this.rsOwners.ShowDocumentMapButton = false;
            this.rsOwners.ShowFindControls = false;
            this.rsOwners.ShowParameterPrompts = false;
            this.rsOwners.ShowPromptAreaButton = false;
            this.rsOwners.ShowStopButton = false;
            this.rsOwners.Size = new System.Drawing.Size(743, 334);
            this.rsOwners.TabIndex = 2;
            // 
            // cboOwners
            // 
            this.cboOwners.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboOwners.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboOwners.FormattingEnabled = true;
            this.cboOwners.Location = new System.Drawing.Point(0, 0);
            this.cboOwners.Name = "cboOwners";
            this.cboOwners.Size = new System.Drawing.Size(743, 21);
            this.cboOwners.Sorted = true;
            this.cboOwners.TabIndex = 3;
            this.cboOwners.SelectionChangeCommitted += new System.EventHandler(this.OnOwnerChanged);
            // 
            // tabExport
            // 
            this.tabExport.Controls.Add(this.txtExport);
            this.tabExport.Location = new System.Drawing.Point(4, 4);
            this.tabExport.Name = "tabExport";
            this.tabExport.Padding = new System.Windows.Forms.Padding(3);
            this.tabExport.Size = new System.Drawing.Size(743, 355);
            this.tabExport.TabIndex = 3;
            this.tabExport.Text = "Export Data";
            this.tabExport.UseVisualStyleBackColor = true;
            // 
            // txtExport
            // 
            this.txtExport.ContextMenuStrip = this.csExport;
            this.txtExport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtExport.Location = new System.Drawing.Point(3, 3);
            this.txtExport.Name = "txtExport";
            this.txtExport.Size = new System.Drawing.Size(737, 349);
            this.txtExport.TabIndex = 0;
            this.txtExport.Text = "";
            this.txtExport.WordWrap = false;
            // 
            // csExport
            // 
            this.csExport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csERefresh,
            this.csESep1,
            this.csEExport});
            this.csExport.Name = "csRoutes";
            this.csExport.Size = new System.Drawing.Size(239, 54);
            // 
            // csERefresh
            // 
            this.csERefresh.Image = ((System.Drawing.Image)(resources.GetObject("csERefresh.Image")));
            this.csERefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csERefresh.Name = "csERefresh";
            this.csERefresh.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.csERefresh.Size = new System.Drawing.Size(238, 22);
            this.csERefresh.Text = "&Refresh Export";
            this.csERefresh.ToolTipText = "Refresh export";
            this.csERefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csESep1
            // 
            this.csESep1.Name = "csESep1";
            this.csESep1.Size = new System.Drawing.Size(235, 6);
            // 
            // csEExport
            // 
            this.csEExport.Image = global::Argix.Properties.Resources.ImportXML;
            this.csEExport.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csEExport.Name = "csEExport";
            this.csEExport.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.csEExport.Size = new System.Drawing.Size(238, 22);
            this.csEExport.Text = "&Export Compensation...";
            this.csEExport.ToolTipText = "Export compensation";
            this.csEExport.Click += new System.EventHandler(this.OnItemClick);
            // 
            // uddAdjType
            // 
            this.uddAdjType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddAdjType.Location = new System.Drawing.Point(215, 336);
            this.uddAdjType.Name = "uddAdjType";
            this.uddAdjType.Size = new System.Drawing.Size(104, 18);
            this.uddAdjType.TabIndex = 113;
            this.uddAdjType.Text = "ultraDropDown1";
            this.uddAdjType.Visible = false;
            // 
            // winDriverComp
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(751, 387);
            this.Controls.Add(this.tabDialog);
            this.Controls.Add(this.uddRateType);
            this.Controls.Add(this.uddEquipType);
            this.Controls.Add(this.uddAdjType);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winDriverComp";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Driver Compensation";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.OnFormClosed);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.uddRateType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).EndInit();
            this.csRoutes.ResumeLayout(false);
            this.csComp.ResumeLayout(false);
            this.tabDialog.ResumeLayout(false);
            this.tabRoutes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCompensation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriverCompDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdRoutes)).EndInit();
            this.grdRoutes.ResumeLayout(false);
            this.tabSummary.ResumeLayout(false);
            this.tabDriverComp.ResumeLayout(false);
            this.tabOwnerComp.ResumeLayout(false);
            this.tabExport.ResumeLayout(false);
            this.csExport.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uddAdjType)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
                //Create menu selections for adding equipment overrides
                DataSet ds = FinanceGateway.GetDriverEquipmentTypes();
                for (int i = 0;i < ds.Tables["EquipmentTypeTable"].Rows.Count;i++) {
                    ToolStripItem item = new ToolStripMenuItem();
                    item.Text = ds.Tables["EquipmentTypeTable"].Rows[i]["Description"].ToString();
                    item.Tag = ds.Tables["EquipmentTypeTable"].Rows[i]["ID"];
                    item.Click += new EventHandler(OnAddEquipmentOverrideClick);
                    this.csCEquipOverride.DropDownItems.Add(item);
                }
				#region Grid Initialization
                this.grdCompensation.DisplayLayout.Bands["DriverCompTable"].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdCompensation.DisplayLayout.Bands["DriverCompTable"].Columns["Operator"].SortIndicator = SortIndicator.Ascending;
                this.grdCompensation.DisplayLayout.Bands["DriverCompTable_DriverRouteTable"].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdCompensation.DisplayLayout.Bands["DriverCompTable_DriverRouteTable"].Columns["RouteDate"].SortIndicator = SortIndicator.Ascending;
				this.grdCompensation.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.True;
                this.grdCompensation.UpdateMode = (UpdateMode.OnCellChangeOrLostFocus & UpdateMode.OnUpdate);               

                this.grdRoutes.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdRoutes.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdRoutes.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdRoutes.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdRoutes.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdRoutes.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdRoutes.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdRoutes.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdRoutes.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdRoutes.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdRoutes.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                this.grdRoutes.DisplayLayout.Bands["RoadshowRouteTable"].Columns["New"].CellActivation = Activation.AllowEdit;
                this.grdRoutes.DisplayLayout.Bands["RoadshowRouteTable"].ColumnFilters["New"].FilterConditions.Add(FilterComparisionOperator.Equals,true);
                this.grdRoutes.DisplayLayout.Bands["RoadshowRouteTable"].Columns["Operator"].SortIndicator = SortIndicator.Ascending;
                this.grdRoutes.UpdateMode = UpdateMode.OnCellChange;

                this.uddEquipType.DataSource = FinanceGateway.GetDriverEquipmentTypes();
                this.uddEquipType.DataMember = "EquipmentTypeTable";
                this.uddEquipType.DisplayMember = "Description";
                this.uddEquipType.ValueMember = "ID";
                this.uddAdjType.DataSource = FinanceGateway.GetRouteAdjustmentTypes();
                this.uddAdjType.DataMember = "AdjustmentTypeTable";
                this.uddAdjType.DisplayMember = this.uddAdjType.ValueMember = "AdjustmentType";
                this.uddRateType.DataSource = FinanceGateway.GetRateTypes();
                this.uddRateType.DataMember = "RateTypeTable";
                this.uddRateType.DisplayMember = "Description";
                this.uddRateType.ValueMember = "ID";
                #endregion 
                this.grdCompensation.DataSource = this.mCompAgent.Compensation;
                this.grdRoutes.DataSource = this.mCompAgent.Routes;
                RoutesVisible = global::Argix.Properties.Settings.Default.RoutesWindow;
                this.txtExport.ReadOnly = true;
                OnShowRates(this.chkShowRates,EventArgs.Empty);
                this.grdCompensation.Select();
                this.WindowState = FormWindowState.Maximized;       //Bug fix- window clips bottom when maximized
            }
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing event
            try {
                if (this.mCompAgent.IsDirty) {
                    DialogResult result = MessageBox.Show("You have unsaved changes to this compensation. Would you like to save it now?",App.Product,MessageBoxButtons.YesNo,MessageBoxIcon.Question);
                    if (result == System.Windows.Forms.DialogResult.Yes) {
                        bool saved = this.mCompAgent.SaveCompensation();
                    }
                }
            }
            catch (Exception ex) { e.Cancel = true; App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormClosed(object sender,FormClosedEventArgs e) {
            //Event handler for form closed event
            global::Argix.Properties.Settings.Default.RoutesWindow = RoutesVisible;
        }
        private void OnTabSelected(object sender,TabControlEventArgs e) {
            //Event handler for change in tab selection
            this.Cursor = Cursors.WaitCursor;
            try {
                switch(e.TabPage.Name) {
                    case "tabRoutes": 
                        break;
                    case "tabSummary":
                        Refresh();
                        break;
                    case "tabDriverComp":
                        this.cboOperators.Items.Clear();
                        this.cboOperators.Items.Add(REPORT_ALL_OPERATORS);
                        for(int i = 0; i < this.mCompAgent.Compensation.DriverCompTable.Rows.Count; i++) {
                            if(this.mCompAgent.Compensation.DriverCompTable[i].Select)
                                this.cboOperators.Items.Add(this.mCompAgent.Compensation.DriverCompTable[i].Operator);
                        }
                        if(this.cboOperators.Items.Count > 0) this.cboOperators.SelectedIndex = (this.mLastOperator<this.cboOperators.Items.Count?this.mLastOperator:0);
                        this.cboOperators.Enabled = this.rsvDrivers.Enabled = this.cboOperators.Items.Count > 0;
                        Refresh();
                        break;
                    case "tabOwnerComp":
                        this.cboOwners.Items.Clear();
                        this.cboOwners.Items.Add(REPORT_ALL_OWNERS);
                        for(int i = 0;i < this.mCompAgent.Compensation.DriverCompTable.Rows.Count;i++) {
                            string owner = this.mCompAgent.Compensation.DriverCompTable[i].FinanceVendor;
                            if(this.mCompAgent.Compensation.DriverCompTable.Select("FinanceVendor=\'" + owner.Replace("'","") + "\'").Length > 1) {
                                if(this.cboOwners.Items.IndexOf(owner) == -1)
                                    this.cboOwners.Items.Add(owner);
                            }
                        }
                        if(this.cboOwners.Items.Count > 0) this.cboOwners.SelectedIndex = (this.mLastOwner < this.cboOwners.Items.Count ? this.mLastOwner : 0);
                        this.cboOwners.Enabled = this.rsOwners.Enabled = this.cboOwners.Items.Count > 0;
                        Refresh();
                        break;
                    case "tabExport":
                        Refresh(); 
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnShowRates(object sender,EventArgs e) {
            //Event handler for show/hide rate columns
            try {
                bool hideRates = !this.chkShowRates.Checked;
                UltraGridBand summaryBand = this.grdCompensation.DisplayLayout.Bands["DriverCompTable"];
                summaryBand.Columns["MilesRate"].Hidden = hideRates;
                summaryBand.Columns["MilesBaseRate"].Hidden = hideRates;
                summaryBand.Columns["DayRate"].Hidden = hideRates;
                summaryBand.Columns["TripRate"].Hidden = hideRates;
                summaryBand.Columns["StopsRate"].Hidden = hideRates;
                summaryBand.Columns["CartonsRate"].Hidden = hideRates;
                summaryBand.Columns["PalletsRate"].Hidden = hideRates;
                summaryBand.Columns["PickupCartonsRate"].Hidden = hideRates;
                summaryBand.Columns["FuelCost"].Hidden = hideRates;
                summaryBand.Columns["FSCBaseRate"].Hidden = hideRates;
                UltraGridBand routeBand = this.grdCompensation.DisplayLayout.Bands["DriverCompTable_DriverRouteTable"];
                routeBand.Columns["MilesRate"].Hidden = hideRates;
                routeBand.Columns["MilesBaseRate"].Hidden = hideRates;
                routeBand.Columns["DayRate"].Hidden = hideRates;
                routeBand.Columns["TripRate"].Hidden = hideRates;
                routeBand.Columns["StopsRate"].Hidden = hideRates;
                routeBand.Columns["CartonsRate"].Hidden = hideRates;
                routeBand.Columns["PalletsRate"].Hidden = hideRates;
                routeBand.Columns["PickupCartonsRate"].Hidden = hideRates;
                routeBand.Columns["FuelCost"].Hidden = hideRates;
                routeBand.Columns["FSCBaseRate"].Hidden = hideRates;
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnOperatorChanged(object sender,EventArgs e) {
            //Event handler for change in selected operator
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboOperators.Items.Count > 0) {
                    //Display driver compensation for selected operator
                    reportStatus(new StatusEventArgs("Refreshing driver paystubs..."));
                    ReportParameter p1 = new ReportParameter("AgentNumber",this.mCompAgent.AgentNumber.Trim());
                    ReportParameter p2 = new ReportParameter("AgentName",this.mCompAgent.AgentName.Trim());
                    ReportParameter p3 = new ReportParameter("StartDate",this.mCompAgent.BeginDate.ToString("yyyy-MM-dd"));
                    ReportParameter p4 = new ReportParameter("EndDate",this.mCompAgent.EndDate.ToString("yyyy-MM-dd"));
                    ReportParameter p5 = new ReportParameter("Operator",(this.cboOperators.Text == REPORT_ALL_OPERATORS ? null : this.cboOperators.Text));
                    this.rsvDrivers.ServerReport.DisplayName = "Driver Compensation";
                    this.rsvDrivers.ServerReport.ReportPath = global::Argix.Properties.Settings.Default.ReportPathDriverComp + this.mCompAgent.AgentNumber;
                    this.rsvDrivers.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5 });
                    this.rsvDrivers.RefreshReport();

                    //Memory
                    this.mLastOperator = this.cboOperators.SelectedIndex;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnOwnerChanged(object sender,EventArgs e) {
            //Event handler for change in selected fleet owner
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboOwners.Items.Count > 0) {
                    //Display comapensation for selected Fleet Owner
                    reportStatus(new StatusEventArgs("Refreshing fleet owner paystubs..."));
                    ReportParameter p1 = new ReportParameter("AgentNumber",this.mCompAgent.AgentNumber.Trim());
                    ReportParameter p2 = new ReportParameter("AgentName",this.mCompAgent.AgentName.Trim());
                    ReportParameter p3 = new ReportParameter("StartDate",this.mCompAgent.BeginDate.ToString("yyyy-MM-dd"));
                    ReportParameter p4 = new ReportParameter("EndDate",this.mCompAgent.EndDate.ToString("yyyy-MM-dd"));
                    ReportParameter p5 = new ReportParameter("Owner",(this.cboOwners.Text == REPORT_ALL_OWNERS ? null : this.cboOwners.Text));
                    this.rsOwners.ServerReport.DisplayName = "Fleet Owner";
                    this.rsOwners.ServerReport.ReportPath = global::Argix.Properties.Settings.Default.ReportPathFleetComp;
                    this.rsOwners.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5 });
                    this.rsOwners.RefreshReport();

                    //Memory
                    this.mLastOwner = this.cboOwners.SelectedIndex;
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnCompensationChanged(object sender,EventArgs e) {
            //Event handler for change in driver routes
            setUserServices();
        }
        private void OnRoutesChanged(object sender,EventArgs e) {
            //Event handler for change in Roadshow routes
            setUserServices();
        }
        #region Compensation Grid Services: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridMouseDown(), OnGridKeyUp(), OnGridBeforeExitEditMode(), OnGridAfterRowUpdate()
        private void OnGridCInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
			//
            e.Layout.Bands["DriverCompTable"].Columns["EquipmentTypeID"].ValueList = this.uddEquipType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["EquipmentTypeID"].ValueList = this.uddEquipType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["RateTypeID"].ValueList = this.uddRateType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["AdjustmentAmount1TypeID"].ValueList = this.uddAdjType;
            e.Layout.Bands["DriverCompTable_DriverRouteTable"].Columns["AdjustmentAmount2TypeID"].ValueList = this.uddAdjType;
        }
		private void OnGridCInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
			//
            if(e.Row.Band.Key == "DriverCompTable") {
                e.Row.Cells["Select"].Activation = (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["IsNew"].Activation = Activation.NoEdit;
                e.Row.Cells["IsCombo"].Activation = Activation.NoEdit;
                e.Row.Cells["IsAdjust"].Activation = Activation.NoEdit;
                e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["FinanceVendorID"].Activation = Activation.NoEdit;
                e.Row.Cells["FinanceVendor"].Activation = Activation.NoEdit;
                e.Row.Cells["Operator"].Activation = Activation.NoEdit;
                e.Row.Cells["EquipmentTypeID"].Activation = Activation.NoEdit;
                e.Row.Cells["EquipmentType"].Activation = Activation.NoEdit;
                e.Row.Cells["DayRate"].Activation = Activation.NoEdit;
                e.Row.Cells["DayAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Miles"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Trip"].Activation = Activation.NoEdit;
                e.Row.Cells["TripRate"].Activation = Activation.NoEdit;
                e.Row.Cells["TripAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Stops"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Cartons"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Pallets"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartons"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["MinimunAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Amount"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCMiles"].Activation = Activation.NoEdit;
                e.Row.Cells["FuelCost"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCGal"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["FSC"].Activation = Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount1"].Activation = Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount2"].Activation = Activation.NoEdit;
                e.Row.Cells["AdminCharge"].Activation = Activation.NoEdit;
                e.Row.Cells["TotalAmount"].Activation = Activation.NoEdit;
            }
            if(e.Row.Band.Key == "DriverCompTable_DriverRouteTable") {
                e.Row.Cells["ID"].Activation = Activation.NoEdit;
                e.Row.Cells["IsNew"].Activation = Activation.NoEdit;
                e.Row.Cells["IsCombo"].Activation = Activation.NoEdit;
                e.Row.Cells["IsAdjust"].Activation = Activation.NoEdit;
                e.Row.Cells["AgentNumber"].Activation = Activation.NoEdit;
                e.Row.Cells["FinanceVendorID"].Activation = Activation.NoEdit;
                e.Row.Cells["RouteDate"].Activation = Activation.NoEdit;
                e.Row.Cells["RouteName"].Activation = Activation.NoEdit;
                e.Row.Cells["Operator"].Activation = Activation.NoEdit;
                e.Row.Cells["Payee"].Activation = Activation.NoEdit;
                e.Row.Cells["EquipmentTypeID"].Activation = Activation.NoEdit;
                e.Row.Cells["RateTypeID"].Activation = Activation.NoEdit;
                e.Row.Cells["DayRate"].Activation = Activation.NoEdit;
                e.Row.Cells["DayAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Miles"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesRate"].Activation = Activation.NoEdit;
                e.Row.Cells["MilesAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Trip"].Activation = Activation.NoEdit;
                e.Row.Cells["TripRate"].Activation = Activation.NoEdit;
                e.Row.Cells["TripAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Stops"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["StopsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Cartons"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["CartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Pallets"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PalletsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartons"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsRate"].Activation = Activation.NoEdit;
                e.Row.Cells["PickupCartonsAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["MinimunAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCMiles"].Activation = Activation.NoEdit;
                e.Row.Cells["FuelCost"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCGal"].Activation = Activation.NoEdit;
                e.Row.Cells["FSCBaseRate"].Activation = Activation.NoEdit;
                e.Row.Cells["FSC"].Activation = Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount1"].Activation = (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount1TypeID"].Activation = (Convert.ToDecimal(e.Row.Cells["AdjustmentAmount1"].Value) != 0.0M && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk)) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AdjustmentAmount2"].Activation = Activation.AllowEdit;
                e.Row.Cells["AdjustmentAmount2TypeID"].Activation = (Convert.ToDecimal(e.Row.Cells["AdjustmentAmount2"].Value) != 0.0M && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk)) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["AdminCharge"].Activation = (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk) ? Activation.AllowEdit : Activation.NoEdit;
                e.Row.Cells["TotalAmount"].Activation = Activation.NoEdit;
                e.Row.Cells["Imported"].Activation = Activation.NoEdit;
                e.Row.Cells["Exported"].Activation = Activation.NoEdit;
                e.Row.Cells["LastUpdated"].Activation = Activation.NoEdit;
                e.Row.Cells["UserID"].Activation = Activation.NoEdit;
            }
		}
        private void OnGridCBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridCMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
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
        private void OnGridCKeyUp(object sender,System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            if(e.KeyCode == System.Windows.Forms.Keys.Enter) {
                //Update row on Enter
                this.grdCompensation.ActiveRow.Update();
                e.Handled = true;
            }
            else if(e.KeyCode == System.Windows.Forms.Keys.Delete) {
                this.csCDelete.PerformClick();
                e.Handled = true;
            }
        }
        private void OnGridCBeforeExitEditMode(object sender,Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e) {
			//	
            try {
                this.grdCompensation.ActiveCell.SetValue(this.grdCompensation.ActiveCell.Text,true);
                this.mCompAgent.CalculateCompensation(this.grdCompensation.ActiveCell.Row.Cells["Operator"].Text, false);

                UltraGridCell cell = this.grdCompensation.ActiveCell;
                if(cell.Column.Key == "AdjustmentAmount1") {
                    if(Convert.ToDecimal(cell.Value) == 0.0M) cell.Row.Cells["AdjustmentAmount1TypeID"].Value = "";
                    cell.Row.Cells["AdjustmentAmount1TypeID"].Activation = Convert.ToDecimal(cell.Value) != 0.0M ? Activation.AllowEdit : Activation.NoEdit;
                }
                if(cell.Column.Key == "AdjustmentAmount2") {
                    if(Convert.ToDecimal(cell.Value) == 0.0M) cell.Row.Cells["AdjustmentAmount2TypeID"].Value = "";
                    cell.Row.Cells["AdjustmentAmount2TypeID"].Activation = Convert.ToDecimal(cell.Value) != 0.0M ? Activation.AllowEdit : Activation.NoEdit;
                }
                if (cell.Column.Key == "AdjustmentAmount1TypeID" || cell.Column.Key == "AdjustmentAmount2TypeID") {
                    this.uddEquipType.DataSource = FinanceGateway.GetDriverEquipmentTypes();
                    this.uddEquipType.DataBind();
                    this.uddAdjType.DataSource = FinanceGateway.GetRouteAdjustmentTypes();
                    this.uddAdjType.DataBind();
                    this.uddRateType.DataSource = FinanceGateway.GetRateTypes();
                    this.uddRateType.DataBind();
                }
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
		}
        private void OnGridCAfterRowUpdate(object sender,Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //Event handler for after row update event fires
            try {
                this.mCompAgent.UpdateCompensation();
            }
            catch(Exception ex) {
                e.Row.CancelUpdate();
                App.ReportError(ex,true,LogLevel.Error);
            }
        }
        #endregion
        #region Route Grid Services: OnGridRInitializeLayout(), OnGridRInitializeRow(), OnGridRMouseDown(), OnGridRBeforeRowFilterDropDownPopulate(), OnCloseRoutes()
        private void OnGridRInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
            e.Layout.Bands["RoadshowRouteTable"].Columns["EquipmentID"].ValueList = this.uddEquipType;
        }
        private void OnGridRInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) { }
        private void OnGridRBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridRMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
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
        private void OnGridRAfterSelectChange(object sender,AfterSelectChangeEventArgs e) {
            //Event handler for pickup grid AfterSelectChange event
            setUserServices();
        }
        private void OnGridRBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for cell activated
            try {
                //Set cell editing
                switch (e.Cell.Column.Key.ToString()) {
                    case "New":
                        e.Cell.Activation = (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk) ? Activation.AllowEdit : Activation.NoEdit;
                        break;
                    default:
                        e.Cell.Activation = Activation.NoEdit;
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnCloseRoutes(object sender,System.EventArgs e) {
            //Event handler to close routes window
            RoutesVisible = false;
            setUserServices();
        }
        #endregion
        #region User Services: OnItemClick(), OnAddEquipmentOverrideClick()
        private void OnItemClick(object sender, System.EventArgs e) {
			//Event handler for mneu item clicked
            UltraGridRow row = null;
            int index = 0;
			try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
					case "csCRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Refreshing driver compensation..."));
                        this.mCompAgent.ViewCompensation();
                        break;
                    case "csCSaveAs":
                        #region Save
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Data Files (*.xml) | *.xml";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save Driver Compensation As...";
                        dlgSave.FileName = this.mCompAgent.Title;
                        dlgSave.OverwritePrompt = true;
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            this.mCompAgent.Compensation.WriteXml(dlgSave.FileName,XmlWriteMode.WriteSchema);
                        }
                        #endregion
                        break;
                    case "csCExport":
                    case "csEExport": 
                        //Export this driver compensation to file
                        #region Export
                        SaveFileDialog dlgExport = new SaveFileDialog();
                        dlgExport.AddExtension = true;
                        dlgExport.Filter = "Export Files (*.txt) | *.txt";
                        dlgExport.FilterIndex = 0;
                        dlgExport.Title = "Export Driver Compensation To...";
                        dlgExport.FileName = this.mCompAgent.Title;
                        dlgExport.OverwritePrompt = true;
                        if(dlgExport.ShowDialog(this) == DialogResult.OK) {
                            //Validate file is unique
                            if(File.Exists(dlgExport.FileName))
                                throw new ApplicationException("Export file " + dlgExport.FileName + " already exists. ");

                            //Create the new file and save driver compensation to disk
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            StreamWriter writer = null;
                            try {
                                writer = new StreamWriter(new FileStream(dlgExport.FileName,FileMode.Create,FileAccess.ReadWrite));
                                writer.BaseStream.Seek(0,SeekOrigin.Begin);
                                writer.WriteLine(this.mCompAgent.ExportCompensation(true));
                                writer.Flush();
                            }
                            catch(Exception ex) { throw ex; }
                            finally { if(writer != null) writer.Close(); this.Cursor = Cursors.Default; }
                        }
                        #endregion
                        break;
                    case "csCEquipOverride":     break;
                    case "csCPrint":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Printing this schedule..."));
                        string caption = "DRIVER COMPENSATION" + Environment.NewLine + this.mCompAgent.AgentName.Trim() + " : " + this.mCompAgent.BeginDate.ToString("dd-MMM-yyyy") + "-" + this.mCompAgent.EndDate.ToString("dd-MMM-yyyy");
                        UltraGridPrinter.Print(this.grdCompensation,caption,true);
                        break;
                    case "csCCut":
                        this.Cursor = Cursors.WaitCursor;
                        Clipboard.SetDataObject(this.grdCompensation.ActiveCell.SelText,false);
						this.grdCompensation.ActiveCell.Value = this.grdCompensation.ActiveCell.Text.Remove(this.grdCompensation.ActiveCell.SelStart, this.grdCompensation.ActiveCell.SelLength);
						break;
                    case "csCCopy":
                        this.Cursor = Cursors.WaitCursor;
                        Clipboard.SetDataObject(this.grdCompensation.ActiveCell.SelText,false);
						break;
                    case "csCPaste":
                        this.Cursor = Cursors.WaitCursor;
                        IDataObject o = Clipboard.GetDataObject();
						this.grdCompensation.ActiveCell.Value = this.grdCompensation.ActiveCell.Text.Remove(this.grdCompensation.ActiveCell.SelStart, this.grdCompensation.ActiveCell.SelLength).Insert(this.grdCompensation.ActiveCell.SelStart, (string)o.GetData("Text"));
						break;
                    case "csCDelete":
                        this.Cursor = Cursors.WaitCursor;
                        row = this.grdCompensation.Selected.Rows[0];
                        index = (row.HasParent()) ? row.ParentRow.Index : row.Index;
                        //try {
                            if(this.grdCompensation.Selected.Rows[0].Band.Key == "DriverCompTable") {
                                //Parent (route summary) band- delete all children and then the parent for 
                                //each selected parent
                                int parents = this.grdCompensation.Selected.Rows.Count;
                                for(int k = parents; k > 0; k--) {
                                    UltraGridRow parent = this.grdCompensation.Selected.Rows[k - 1];
                                    int kids = parent.ChildBands[0].Rows.Count;
                                    for(int i = kids; i > 0; i--) {
                                        parent.ChildBands[0].Rows[i - 1].Delete(false);
                                    }
                                    if(parent.HasChild())
                                        this.mCompAgent.CalculateCompensation(parent.Cells["Operator"].Text,true);
                                    else
                                        parent.Delete(false);
                                }
                            }
                            else {
                                //Child (driver route) band- delete each selected route from the single parent;
                                //either re-calculate the parent or delete the parent if no child routes
                                UltraGridRow parent = this.grdCompensation.Selected.Rows[0].ParentRow;
                                this.grdCompensation.DeleteSelectedRows(false);
                                if(parent.HasChild()) {
                                    this.mCompAgent.CalculateCompensation(parent.Cells["Operator"].Text,true);
                                }
                                else
                                    parent.Delete(false);
                            }
                            this.grdCompensation.UpdateData();
                            this.mCompAgent.UpdateCompensation();
                            this.csRRefresh.PerformClick();
                        //}
                        //finally {
                        //    if(this.grdCompensation.Rows.VisibleRowCount > 0) {
                        //        this.grdCompensation.Rows[index].Activate();
                        //        this.grdCompensation.Rows[index].Selected = true;
                        //        this.grdCompensation.Rows[index].Expanded = true;
                        //    }
                        //}
                        break;
                    case "csCExpandAll":       this.grdCompensation.Rows.ExpandAll(true); break;
                    case "csCCollapseAll":     this.grdCompensation.Rows.CollapseAll(true);  break;
                    case "csRRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Importing Roadshow routes..."));
                        this.mCompAgent.ImportRoutes();
                        break;
                    case "csRSelectAll":
                        this.Cursor = Cursors.WaitCursor;
                        for(int i = 0; i < this.grdRoutes.Rows.Count; i++)
                            this.grdRoutes.Rows[i].Cells["New"].Value = this.csRSelectAll.Checked;
                        break;
                    case "csRAddRoutes":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Adding Roadshow routes..."));
                        this.mCompAgent.CreateCompensation();
                        break;
                    case "csERefresh":
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Refreshing export..."));
                        this.txtExport.Clear();
                        this.txtExport.Text = this.mCompAgent.ExportCompensation(false);
                        break;
				}
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { setUserServices(); this.Cursor = Cursors.Default; }
		}
        private void OnAddEquipmentOverrideClick(object sender,EventArgs e) {
            //Event handler for Add Equipment Override menu item clicked
            UltraGridRow row = this.grdCompensation.Selected.Rows[0];
            int index = (row.HasParent()) ? row.ParentRow.Index : row.Index;
            try {
                //Create the override; if successful, delete and re-add the route
                string vendorID = row.Cells["FinanceVendorID"].Value.ToString();
                string operatorName = row.Cells["Operator"].Value.ToString();
                string routeDate = row.Cells["RouteDate"].Value.ToString();
                int equipID = Convert.ToInt32(((ToolStripItem)sender).Tag);
                if(FinanceGateway.CreateDriverEquipment(vendorID,operatorName,equipID)) {
                    this.csCDelete.PerformClick();
                    for(int i = 0; i < this.grdRoutes.Rows.Count; i++) {
                        UltraGridRow r = this.grdRoutes.Rows[i];
                        bool val = (r.Cells["FinanceVendID"].Value.ToString() == vendorID && r.Cells["Operator"].Value.ToString() == operatorName && r.Cells["Rt_Date"].Value.ToString() == routeDate);
                        this.grdRoutes.Rows[i].Cells["New"].Value = val;
                    }
                    this.mCompAgent.CreateCompensation();
                }
            }
            catch(Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally {
                this.grdCompensation.Rows[index].Activate(); this.grdCompensation.Rows[index].Selected = true; this.grdCompensation.Rows[index].Expanded = true;
                setUserServices(); this.Cursor = Cursors.Default; 
            }
        }
        #endregion
		#region Local Services: setUserServices(), reportStatus()
		private void setUserServices() {
			//Set user services
			try {
                bool isDrivers = (this.tabDialog.SelectedTab == this.tabRoutes && this.grdCompensation.Focused);
                bool isRoadshows = (this.tabDialog.SelectedTab == this.tabRoutes && this.grdRoutes.Focused);
                bool hasCompensation = (this.mCompAgent.Compensation.DriverCompTable.Rows.Count > 0);
                bool hasSelection = (this.grdCompensation.Selected != null && this.grdCompensation.Selected.Rows.Count > 0);
                bool isRoute = (hasSelection && this.grdCompensation.Selected.Rows[0].ParentRow != null);
                bool isSummary = (this.tabDialog.SelectedTab == this.tabSummary);
                bool isPaystubs = (this.tabDialog.SelectedTab == this.tabDriverComp);
                bool isExports = (this.tabDialog.SelectedTab == this.tabExport);

                this.csCRefresh.Enabled = isDrivers;
                this.csCSaveAs.Enabled = isDrivers && hasCompensation && !hasSelection;
                this.csCExport.Enabled = isDrivers && hasCompensation && !hasSelection && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.csCPrint.Enabled = (isDrivers && hasCompensation && !hasSelection) || isSummary || isPaystubs;
                this.csCEquipOverride.Enabled = isDrivers && isRoute && (RoleServiceGateway.IsBillingSupervisor);
                this.csCCut.Enabled = isDrivers && (this.grdCompensation.ActiveCell != null && this.grdCompensation.ActiveCell.IsInEditMode) && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.csCCopy.Enabled = isDrivers && (this.grdCompensation.ActiveCell != null) && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.csCPaste.Enabled = isDrivers && (this.grdCompensation.ActiveCell != null && this.grdCompensation.ActiveCell.IsInEditMode && Clipboard.GetDataObject() != null) && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.csCDelete.Enabled = isDrivers && hasSelection && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.csCExpandAll.Enabled = true;
                this.csCCollapseAll.Enabled = true;
                this.csRRefresh.Enabled = isRoadshows;
                this.csRSelectAll.Enabled = isRoadshows;
                this.csRAddRoutes.Enabled = isRoadshows && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
                this.csERefresh.Enabled = isExports;
                this.csEExport.Enabled = isExports && (RoleServiceGateway.IsBillingSupervisor || RoleServiceGateway.IsBillingClerk);
            }
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
			finally { Application.DoEvents(); if(this.ServiceStatesChanged!=null) this.ServiceStatesChanged(this, new EventArgs()); }
		}
		private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
		#endregion
    }
}
