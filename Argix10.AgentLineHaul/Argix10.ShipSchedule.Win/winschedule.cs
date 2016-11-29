using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Windows;
using Argix.Security;

namespace Argix.AgentLineHaul {
    //
    public class winSchedule : System.Windows.Forms.Form, ISchedule {
        //Members
        private ShipSchedule mSchedule = null;
        private UltraGridSvc mGridSvc = null;

        #region Controls

        private Infragistics.Win.UltraWinGrid.UltraGrid grdSchedule;
        private Argix.ShipScheduleDataset mTrips;
        private ContextMenuStrip csSchedule;
        private ToolStripMenuItem ctxRefresh;
        private ToolStripSeparator ctxSep1;
        private ToolStripMenuItem ctxCut;
        private ToolStripMenuItem ctxCopy;
        private ToolStripMenuItem ctxPaste;
        private ToolStripSeparator ctxSep2;
        private ToolStripMenuItem ctxCancelLoad;
        private UltraDropDown uddCarrier;
        private System.ComponentModel.IContainer components;
        #endregion
        private const int KEYSTATE_SHIFT = 5, KEYSTATE_CTL = 9;

        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winSchedule(ShipSchedule schedule) {
            //Constructor
            try {
                InitializeComponent();
                Infragistics.Win.ValueListsCollection lists = this.grdSchedule.DisplayLayout.ValueLists;
                Infragistics.Win.ValueList vl = lists.Add("YesNoValueList");
                vl.ValueListItems.Add(0, "Yes");
                vl.ValueListItems.Add(0, "No");

                //Init objects
                this.mSchedule = schedule;
                this.mSchedule.Changed += new EventHandler(OnScheduleChanged);
                this.mSchedule.StatusMessage += new StatusEventHandler(OnStatusMessage);

                this.mGridSvc = new UltraGridSvc(this.grdSchedule);
                this.Text = this.mSchedule.SortCenter + ", " + this.mSchedule.ScheduleDate.ToShortDateString();
                this.grdSchedule.Text = "Ship Schedule: " + this.mSchedule.SortCenter + ", " + this.mSchedule.ScheduleDate.ToShortDateString();
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new winSchedule.", ex); }
        }
        public ShipSchedule Schedule { get { return this.mSchedule; } }
        #region ISchedule interface
        public bool CanNew { get { return false; } }
        public void New() { }
        public bool CanOpen { get { return false; } }
        public void Open() { }
        public bool CanSave { get { return (this.Schedule.Trips.ShipScheduleViewTable.Rows.Count > 0); } }
        public void Save(string filename) { }
        public bool CanExport { get { return (this.Schedule.Trips.ShipScheduleViewTable.Rows.Count > 0); } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return (this.Schedule.Trips.ShipScheduleViewTable.Rows.Count > 0); } }
        public void Print(bool showDialog) {
            //Print this schedule
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Printing this schedule..."));
                string caption = this.Schedule.ScheduleDate.DayOfWeek.ToString().ToUpper() + "  " + DateTime.Now.ToShortTimeString() + Environment.NewLine + "SHIP SCHEDULE FOR " + this.Text;
                UltraGridPrinter.Print(this.grdSchedule, caption, showDialog);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanPreview { get { return (this.Schedule.Trips.ShipScheduleViewTable.Rows.Count > 0); } }
        public void PrintPreview() {
            //Print preview this schedule
            try {
                reportStatus(new StatusEventArgs("Print previewing this schedule..."));
                string caption = this.Schedule.ScheduleDate.DayOfWeek.ToString().ToUpper() + "  " + DateTime.Now.ToShortTimeString() + Environment.NewLine + "SHIP SCHEDULE FOR " + this.Text;
                UltraGridPrinter.PrintPreview(this.grdSchedule, caption);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }

        }
      
        public bool CanCut { get { return this.ctxCut.Enabled; } }
        public void Cut() { this.ctxCut.PerformClick(); }
        public bool CanCopy { get { return this.ctxCopy.Enabled; } }
        public void Copy() { this.ctxCopy.PerformClick(); }
        public bool CanPaste { get { return this.ctxPaste.Enabled; } }
        public void Paste() { this.ctxPaste.PerformClick(); }

        public bool CanAddLoads { get { return RoleServiceGateway.IsLineHaulAdministrator; } }
        public void AddLoads() {
            //Make sure we have at least one template to add
            reportStatus(new StatusEventArgs("Adding loads to this schedule..."));
            this.mSchedule.AddLoads();
        }
        public bool CanCancelLoad { get { return this.ctxCancelLoad.Enabled; } }
        public bool IsLoadCancelled { get { return this.ctxCancelLoad.Checked; } }
        public void CancelLoad() {
            //Cancel the selected load
            try {
                if(this.grdSchedule.ActiveRow != null) {
                    if(this.grdSchedule.ActiveRow.Cells["Canceled"].Value.ToString().Trim().Length > 0) {
                        //Un-cancel
                        if(MessageBox.Show("Do you really want to uncancel this schedule?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                            this.Cursor = Cursors.WaitCursor;
                            reportStatus(new StatusEventArgs("Uncancelling the selected load..."));
                            this.grdSchedule.ActiveRow.Cells["Canceled"].Value = System.DBNull.Value;
                            this.grdSchedule.ActiveRow.Cells["Notes"].Value = "";
                            this.grdSchedule.UpdateData();
                        }
                    }
                    else {
                        //Cancel
                        if(MessageBox.Show("Do you really want to cancel this schedule?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                            this.Cursor = Cursors.WaitCursor;
                            reportStatus(new StatusEventArgs("Cancelling the selected load..."));
                            this.grdSchedule.ActiveRow.Cells["Canceled"].Value = System.DateTime.Now;
                            this.grdSchedule.ActiveRow.Cells["Notes"].Value = "Cancelled";
                            this.grdSchedule.UpdateData();
                        }
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }

        public bool CanEmailCarriers { get { return (RoleServiceGateway.IsLineHaulAdministrator && this.Schedule.Trips.ShipScheduleViewTable.Rows.Count > 0); } }
        public void EmailCarriers(bool showDialog) {
            try {
                if(showDialog) {
                    dlgSubscriptions subs = new dlgSubscriptions(global::Argix.Properties.Settings.Default.CarrierReportPath, this.mSchedule);
                    subs.Font = this.Font;
                    subs.ShowDialog();
                }
                else {
                    if(MessageBox.Show(this, "Do you want to send this schedule to all Carriers?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Emailing this schedule to all carriers..."));
                        new dlgSubscriptions(global::Argix.Properties.Settings.Default.CarrierReportPath, this.mSchedule).SendReport();
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        public bool CanEmailAgents { get { return (RoleServiceGateway.IsLineHaulAdministrator && this.Schedule.Trips.ShipScheduleViewTable.Rows.Count > 0); } }
        public void EmailAgents(bool showDialog) {
            try {
                if(showDialog) {
                    dlgSubscriptions subs = new dlgSubscriptions(global::Argix.Properties.Settings.Default.AgentReportPath, this.mSchedule);
                    subs.Font = this.Font;
                    subs.ShowDialog();
                }
                else {
                    if(MessageBox.Show(this, "Do you want to send this schedule to all Agents?", App.Product, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes) {
                        this.Cursor = Cursors.WaitCursor;
                        reportStatus(new StatusEventArgs("Emailing this schedule to all agents..."));
                        new dlgSubscriptions(global::Argix.Properties.Settings.Default.AgentReportPath, this.mSchedule).SendReport();
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        #endregion
        public override void Refresh() {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                reportStatus(new StatusEventArgs("Refreshing ship schedule " + this.Text + "."));
                this.mGridSvc.CaptureState();
                this.mSchedule.Refresh();
                this.mGridSvc.RestoreState();
                base.Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("CarrierTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance11 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ShipScheduleViewTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenterID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SortCenter");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduleDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TripID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TemplateID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BolNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierServiceID", -1, "uddCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Carrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("NextCarrier");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LoadNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TractorNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledClose");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledDeparture");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsMandatory");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Canceled");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDEUserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDELastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SCDERowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn41 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn42 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn43 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn44 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn45 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn46 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn47 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn48 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn49 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn50 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S1RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn51 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn52 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2StopNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn53 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentTerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn54 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn55 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2MainZone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn56 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Tag");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn57 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2Notes");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn58 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledArrival");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn59 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ScheduledOFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn60 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn61 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn62 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2RowVersion");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn63 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLs");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn121 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArrivalDay", 0);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn122 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArrivalTime", 1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn123 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ArrivalDay", 2);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn124 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ArrivalTime", 3);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn125 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerComplete2", 4);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn126 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PaperworkComplete2", 5);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn127 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerDispatched2", 6);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn128 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ZoneTag", 7);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn129 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("S2ZoneTag", 8);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn130 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FreightAssigned2", 9);
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance17 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance18 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(winSchedule));
            this.csSchedule = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ctxRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCut = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.ctxSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.ctxCancelLoad = new System.Windows.Forms.ToolStripMenuItem();
            this.uddCarrier = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            this.grdSchedule = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mTrips = new Argix.ShipScheduleDataset();
            this.csSchedule.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrips)).BeginInit();
            this.SuspendLayout();
            // 
            // csSchedule
            // 
            this.csSchedule.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ctxRefresh,
            this.ctxSep1,
            this.ctxCut,
            this.ctxCopy,
            this.ctxPaste,
            this.ctxSep2,
            this.ctxCancelLoad});
            this.csSchedule.Name = "csLoad";
            this.csSchedule.Size = new System.Drawing.Size(140, 126);
            // 
            // ctxRefresh
            // 
            this.ctxRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.ctxRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxRefresh.Name = "ctxRefresh";
            this.ctxRefresh.Size = new System.Drawing.Size(139, 22);
            this.ctxRefresh.Text = "&Refresh";
            this.ctxRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep1
            // 
            this.ctxSep1.Name = "ctxSep1";
            this.ctxSep1.Size = new System.Drawing.Size(136, 6);
            // 
            // ctxCut
            // 
            this.ctxCut.Image = global::Argix.Properties.Resources.Cut;
            this.ctxCut.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCut.Name = "ctxCut";
            this.ctxCut.Size = new System.Drawing.Size(139, 22);
            this.ctxCut.Text = "C&ut";
            this.ctxCut.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxCopy
            // 
            this.ctxCopy.Image = global::Argix.Properties.Resources.Copy;
            this.ctxCopy.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCopy.Name = "ctxCopy";
            this.ctxCopy.Size = new System.Drawing.Size(139, 22);
            this.ctxCopy.Text = "&Copy";
            this.ctxCopy.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxPaste
            // 
            this.ctxPaste.Image = global::Argix.Properties.Resources.Paste;
            this.ctxPaste.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxPaste.Name = "ctxPaste";
            this.ctxPaste.Size = new System.Drawing.Size(139, 22);
            this.ctxPaste.Text = "&Paste";
            this.ctxPaste.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ctxSep2
            // 
            this.ctxSep2.Name = "ctxSep2";
            this.ctxSep2.Size = new System.Drawing.Size(136, 6);
            // 
            // ctxCancelLoad
            // 
            this.ctxCancelLoad.Image = global::Argix.Properties.Resources.Delete;
            this.ctxCancelLoad.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ctxCancelLoad.Name = "ctxCancelLoad";
            this.ctxCancelLoad.Size = new System.Drawing.Size(139, 22);
            this.ctxCancelLoad.Text = "Cancel &Load";
            this.ctxCancelLoad.Click += new System.EventHandler(this.OnItemClick);
            // 
            // uddCarrier
            // 
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.BorderColor = System.Drawing.SystemColors.InactiveCaption;
            this.uddCarrier.DisplayLayout.Appearance = appearance1;
            ultraGridColumn7.Header.VisiblePosition = 0;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.Header.Caption = "Carrier";
            ultraGridColumn8.Header.VisiblePosition = 1;
            ultraGridColumn8.Width = 210;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn7,
            ultraGridColumn8});
            this.uddCarrier.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.uddCarrier.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            this.uddCarrier.DisplayLayout.CaptionVisible = Infragistics.Win.DefaultableBoolean.False;
            appearance2.BackColor = System.Drawing.SystemColors.ActiveBorder;
            appearance2.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance2.BackGradientStyle = Infragistics.Win.GradientStyle.Vertical;
            appearance2.BorderColor = System.Drawing.SystemColors.Window;
            this.uddCarrier.DisplayLayout.GroupByBox.Appearance = appearance2;
            appearance3.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uddCarrier.DisplayLayout.GroupByBox.BandLabelAppearance = appearance3;
            this.uddCarrier.DisplayLayout.GroupByBox.BorderStyle = Infragistics.Win.UIElementBorderStyle.Solid;
            appearance4.BackColor = System.Drawing.SystemColors.ControlLightLight;
            appearance4.BackColor2 = System.Drawing.SystemColors.Control;
            appearance4.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance4.ForeColor = System.Drawing.SystemColors.GrayText;
            this.uddCarrier.DisplayLayout.GroupByBox.PromptAppearance = appearance4;
            this.uddCarrier.DisplayLayout.MaxColScrollRegions = 1;
            this.uddCarrier.DisplayLayout.MaxRowScrollRegions = 1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.uddCarrier.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.uddCarrier.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.uddCarrier.DisplayLayout.Override.BorderStyleCell = Infragistics.Win.UIElementBorderStyle.Dotted;
            this.uddCarrier.DisplayLayout.Override.BorderStyleRow = Infragistics.Win.UIElementBorderStyle.Dotted;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            this.uddCarrier.DisplayLayout.Override.CardAreaAppearance = appearance7;
            appearance8.BorderColor = System.Drawing.Color.Silver;
            appearance8.TextTrimming = Infragistics.Win.TextTrimming.EllipsisCharacter;
            this.uddCarrier.DisplayLayout.Override.CellAppearance = appearance8;
            this.uddCarrier.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            this.uddCarrier.DisplayLayout.Override.CellPadding = 0;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.BackColor2 = System.Drawing.SystemColors.ControlDark;
            appearance9.BackGradientAlignment = Infragistics.Win.GradientAlignment.Element;
            appearance9.BackGradientStyle = Infragistics.Win.GradientStyle.Horizontal;
            appearance9.BorderColor = System.Drawing.SystemColors.Window;
            this.uddCarrier.DisplayLayout.Override.GroupByRowAppearance = appearance9;
            appearance10.TextHAlignAsString = "Left";
            this.uddCarrier.DisplayLayout.Override.HeaderAppearance = appearance10;
            this.uddCarrier.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.uddCarrier.DisplayLayout.Override.HeaderStyle = Infragistics.Win.HeaderStyle.WindowsXPCommand;
            appearance11.BackColor = System.Drawing.SystemColors.Window;
            appearance11.BorderColor = System.Drawing.Color.Silver;
            this.uddCarrier.DisplayLayout.Override.RowAppearance = appearance11;
            this.uddCarrier.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            appearance12.BackColor = System.Drawing.SystemColors.ControlLight;
            this.uddCarrier.DisplayLayout.Override.TemplateAddRowAppearance = appearance12;
            this.uddCarrier.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.uddCarrier.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.uddCarrier.DisplayLayout.ViewStyleBand = Infragistics.Win.UltraWinGrid.ViewStyleBand.OutlookGroupBy;
            this.uddCarrier.Location = new System.Drawing.Point(12, 149);
            this.uddCarrier.Name = "uddCarrier";
            this.uddCarrier.Size = new System.Drawing.Size(176, 76);
            this.uddCarrier.TabIndex = 2;
            this.uddCarrier.Visible = false;
            this.uddCarrier.BeforeDropDown += new System.ComponentModel.CancelEventHandler(this.OnCarrierBeforeDropDown);
            // 
            // grdSchedule
            // 
            this.grdSchedule.AllowDrop = true;
            this.grdSchedule.ContextMenuStrip = this.csSchedule;
            this.grdSchedule.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSchedule.DataMember = "ShipScheduleViewTable";
            this.grdSchedule.DataSource = this.mTrips;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdSchedule.DisplayLayout.Appearance = appearance13;
            ultraGridBand2.ColHeaderLines = 3;
            ultraGridColumn10.Header.VisiblePosition = 40;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.VisiblePosition = 41;
            ultraGridColumn11.Hidden = true;
            ultraGridColumn12.Header.VisiblePosition = 43;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn13.Header.VisiblePosition = 44;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn14.Header.VisiblePosition = 45;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn15.Header.VisiblePosition = 46;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn16.Header.VisiblePosition = 47;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn17.Header.Caption = "Carrier";
            ultraGridColumn17.Header.VisiblePosition = 9;
            ultraGridColumn17.Width = 200;
            ultraGridColumn18.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn18.Header.VisiblePosition = 10;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn18.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn18.Width = 200;
            ultraGridColumn19.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn19.Header.VisiblePosition = 7;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn20.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn20.Header.Caption = "Next Carrier";
            ultraGridColumn20.Header.VisiblePosition = 11;
            ultraGridColumn20.Width = 140;
            ultraGridColumn21.Header.VisiblePosition = 12;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn22.Header.VisiblePosition = 14;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.Header.Caption = "Load\r\nNumber";
            ultraGridColumn23.Header.VisiblePosition = 27;
            ultraGridColumn23.Width = 95;
            ultraGridColumn24.Header.VisiblePosition = 48;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn25.Header.Caption = "Trailer";
            ultraGridColumn25.Header.VisiblePosition = 8;
            ultraGridColumn25.MaxLength = 12;
            ultraGridColumn25.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn25.Width = 85;
            ultraGridColumn26.Header.Caption = "Tractor\r\nNumber";
            ultraGridColumn26.Header.VisiblePosition = 28;
            ultraGridColumn26.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn26.Width = 85;
            ultraGridColumn27.Header.Caption = "Driver";
            ultraGridColumn27.Header.VisiblePosition = 29;
            ultraGridColumn27.Nullable = Infragistics.Win.UltraWinGrid.Nullable.EmptyString;
            ultraGridColumn27.Width = 96;
            ultraGridColumn28.Format = "HH:mm/ddd";
            ultraGridColumn28.Header.Caption = "Close\r\nTime/Day";
            ultraGridColumn28.Header.VisiblePosition = 6;
            ultraGridColumn28.MaskInput = "{LOC}mm/dd/yyyy hh:mm";
            ultraGridColumn28.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn28.UseEditorMaskSettings = true;
            ultraGridColumn28.Width = 110;
            ultraGridColumn29.Format = "HH:mm/ddd";
            ultraGridColumn29.Header.Caption = "Depart\r\nTime/Day";
            ultraGridColumn29.Header.VisiblePosition = 13;
            ultraGridColumn29.MaskInput = "{LOC}mm/dd/yyyy hh:mm";
            ultraGridColumn29.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn29.UseEditorMaskSettings = true;
            ultraGridColumn29.Width = 110;
            ultraGridColumn30.Header.VisiblePosition = 49;
            ultraGridColumn30.Hidden = true;
            ultraGridColumn31.Header.VisiblePosition = 32;
            ultraGridColumn31.Hidden = true;
            ultraGridColumn32.ButtonDisplayStyle = Infragistics.Win.UltraWinGrid.ButtonDisplayStyle.Always;
            ultraGridColumn32.Header.VisiblePosition = 34;
            ultraGridColumn32.Hidden = true;
            ultraGridColumn33.Header.VisiblePosition = 36;
            ultraGridColumn33.Hidden = true;
            ultraGridColumn34.Header.VisiblePosition = 38;
            ultraGridColumn34.Hidden = true;
            ultraGridColumn35.Header.VisiblePosition = 39;
            ultraGridColumn35.Hidden = true;
            ultraGridColumn36.Header.VisiblePosition = 50;
            ultraGridColumn36.Hidden = true;
            ultraGridColumn37.Header.VisiblePosition = 51;
            ultraGridColumn37.Hidden = true;
            ultraGridColumn38.Header.VisiblePosition = 52;
            ultraGridColumn38.Hidden = true;
            ultraGridColumn39.Header.VisiblePosition = 53;
            ultraGridColumn39.Hidden = true;
            ultraGridColumn40.Header.VisiblePosition = 54;
            ultraGridColumn40.Hidden = true;
            ultraGridColumn41.Header.VisiblePosition = 55;
            ultraGridColumn41.Hidden = true;
            ultraGridColumn42.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn42.Header.Caption = "Agent";
            ultraGridColumn42.Header.VisiblePosition = 16;
            ultraGridColumn42.Hidden = true;
            ultraGridColumn42.Width = 45;
            ultraGridColumn43.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn43.Header.Caption = "Zone";
            ultraGridColumn43.Header.VisiblePosition = 2;
            ultraGridColumn43.Hidden = true;
            ultraGridColumn43.Width = 50;
            ultraGridColumn44.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn44.Header.VisiblePosition = 3;
            ultraGridColumn44.Hidden = true;
            ultraGridColumn44.Width = 35;
            ultraGridColumn45.Header.VisiblePosition = 15;
            ultraGridColumn45.Width = 100;
            ultraGridColumn46.Format = "MM-dd";
            ultraGridColumn46.Header.Caption = "Arr \r\nDate";
            ultraGridColumn46.Header.VisiblePosition = 19;
            ultraGridColumn46.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn46.Width = 75;
            ultraGridColumn47.Format = "MM-dd";
            ultraGridColumn47.Header.Caption = "OFD Date";
            ultraGridColumn47.Header.VisiblePosition = 21;
            ultraGridColumn47.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn47.Width = 75;
            ultraGridColumn48.Header.VisiblePosition = 56;
            ultraGridColumn48.Hidden = true;
            ultraGridColumn49.Header.VisiblePosition = 57;
            ultraGridColumn49.Hidden = true;
            ultraGridColumn50.Header.VisiblePosition = 58;
            ultraGridColumn50.Hidden = true;
            ultraGridColumn51.Header.VisiblePosition = 59;
            ultraGridColumn51.Hidden = true;
            ultraGridColumn52.Header.VisiblePosition = 60;
            ultraGridColumn52.Hidden = true;
            ultraGridColumn53.Header.VisiblePosition = 42;
            ultraGridColumn53.Hidden = true;
            ultraGridColumn54.Header.Caption = "S2 \r\nAgent";
            ultraGridColumn54.Header.VisiblePosition = 22;
            ultraGridColumn54.Hidden = true;
            ultraGridColumn54.Width = 45;
            ultraGridColumn55.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn55.Header.Caption = "S2\r\nZone";
            ultraGridColumn55.Header.VisiblePosition = 4;
            ultraGridColumn55.Hidden = true;
            ultraGridColumn55.Width = 50;
            ultraGridColumn56.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn56.Header.Caption = "S2\r\nTag";
            ultraGridColumn56.Header.VisiblePosition = 5;
            ultraGridColumn56.Hidden = true;
            ultraGridColumn56.Width = 35;
            ultraGridColumn57.Header.Caption = "S2 Notes";
            ultraGridColumn57.Header.VisiblePosition = 17;
            ultraGridColumn57.Width = 100;
            ultraGridColumn58.Format = "MM-dd";
            ultraGridColumn58.Header.Caption = "S2 Arr\r\nDate";
            ultraGridColumn58.Header.VisiblePosition = 24;
            ultraGridColumn58.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn58.Width = 75;
            ultraGridColumn59.Format = "MM-dd";
            ultraGridColumn59.Header.Caption = "S2\r\nOFD\r\nDate";
            ultraGridColumn59.Header.VisiblePosition = 26;
            ultraGridColumn59.MaskInput = "{LOC}mm/dd/yy";
            ultraGridColumn59.Width = 75;
            ultraGridColumn60.Header.VisiblePosition = 61;
            ultraGridColumn60.Hidden = true;
            ultraGridColumn61.Header.VisiblePosition = 62;
            ultraGridColumn61.Hidden = true;
            ultraGridColumn62.Header.VisiblePosition = 63;
            ultraGridColumn62.Hidden = true;
            ultraGridColumn63.Header.VisiblePosition = 30;
            ultraGridColumn63.Width = 30;
            ultraGridColumn121.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn121.Header.Caption = "Arr\r\nDay";
            ultraGridColumn121.Header.VisiblePosition = 18;
            ultraGridColumn121.Width = 35;
            ultraGridColumn122.DataType = typeof(System.DateTime);
            ultraGridColumn122.Format = "HH:mm";
            ultraGridColumn122.Header.Caption = "Arr\r\nTime";
            ultraGridColumn122.Header.VisiblePosition = 20;
            ultraGridColumn122.MaskInput = "{LOC}hh:mm";
            ultraGridColumn122.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn122.UseEditorMaskSettings = true;
            ultraGridColumn122.Width = 45;
            ultraGridColumn123.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn123.Header.Caption = "S2 \r\nArr\r\nDay";
            ultraGridColumn123.Header.VisiblePosition = 23;
            ultraGridColumn123.Width = 35;
            ultraGridColumn124.DataType = typeof(System.DateTime);
            ultraGridColumn124.Format = "HH:mm";
            ultraGridColumn124.Header.Caption = "S2\r\nArr\r\nTime";
            ultraGridColumn124.Header.VisiblePosition = 25;
            ultraGridColumn124.MaskInput = "{LOC}hh:mm";
            ultraGridColumn124.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.Edit;
            ultraGridColumn124.UseEditorMaskSettings = true;
            ultraGridColumn124.Width = 45;
            ultraGridColumn125.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn125.Header.Caption = "Trailer\r\nComplete";
            ultraGridColumn125.Header.VisiblePosition = 33;
            ultraGridColumn125.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn125.Width = 70;
            ultraGridColumn126.AllowGroupBy = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn126.Header.Caption = "Paper\r\nComplete";
            ultraGridColumn126.Header.VisiblePosition = 35;
            ultraGridColumn126.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn126.Width = 70;
            ultraGridColumn127.Header.Caption = "Trailer\r\nDispatch";
            ultraGridColumn127.Header.VisiblePosition = 37;
            ultraGridColumn127.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn127.Width = 70;
            ultraGridColumn128.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn128.Header.Caption = "Zone\r\nTag";
            ultraGridColumn128.Header.Fixed = true;
            ultraGridColumn128.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.Button;
            ultraGridColumn128.Header.VisiblePosition = 0;
            ultraGridColumn128.Width = 55;
            ultraGridColumn129.CellActivation = Infragistics.Win.UltraWinGrid.Activation.NoEdit;
            ultraGridColumn129.Header.Caption = "S2 \r\nZone\r\nTag";
            ultraGridColumn129.Header.Fixed = true;
            ultraGridColumn129.Header.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.Button;
            ultraGridColumn129.Header.VisiblePosition = 1;
            ultraGridColumn129.Width = 55;
            ultraGridColumn130.Header.Caption = "Freight\r\nAssigned";
            ultraGridColumn130.Header.VisiblePosition = 31;
            ultraGridColumn130.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.DropDownValidate;
            ultraGridColumn130.Width = 70;
            ultraGridBand2.Columns.AddRange(new object[] {
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
            ultraGridColumn43,
            ultraGridColumn44,
            ultraGridColumn45,
            ultraGridColumn46,
            ultraGridColumn47,
            ultraGridColumn48,
            ultraGridColumn49,
            ultraGridColumn50,
            ultraGridColumn51,
            ultraGridColumn52,
            ultraGridColumn53,
            ultraGridColumn54,
            ultraGridColumn55,
            ultraGridColumn56,
            ultraGridColumn57,
            ultraGridColumn58,
            ultraGridColumn59,
            ultraGridColumn60,
            ultraGridColumn61,
            ultraGridColumn62,
            ultraGridColumn63,
            ultraGridColumn121,
            ultraGridColumn122,
            ultraGridColumn123,
            ultraGridColumn124,
            ultraGridColumn125,
            ultraGridColumn126,
            ultraGridColumn127,
            ultraGridColumn128,
            ultraGridColumn129,
            ultraGridColumn130});
            this.grdSchedule.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdSchedule.DisplayLayout.CaptionAppearance = appearance14;
            appearance15.BackColor = System.Drawing.SystemColors.Window;
            appearance15.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grdSchedule.DisplayLayout.Override.ActiveCellAppearance = appearance15;
            appearance16.BackColor = System.Drawing.SystemColors.Highlight;
            appearance16.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdSchedule.DisplayLayout.Override.ActiveRowAppearance = appearance16;
            this.grdSchedule.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSchedule.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.True;
            this.grdSchedule.DisplayLayout.Override.FixedHeaderIndicator = Infragistics.Win.UltraWinGrid.FixedHeaderIndicator.None;
            appearance17.BackColor = System.Drawing.SystemColors.Control;
            appearance17.FontData.BoldAsString = "True";
            appearance17.FontData.SizeInPoints = 8F;
            appearance17.TextHAlignAsString = "Left";
            this.grdSchedule.DisplayLayout.Override.HeaderAppearance = appearance17;
            this.grdSchedule.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSchedule.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance18.BorderColor = System.Drawing.SystemColors.WindowText;
            this.grdSchedule.DisplayLayout.Override.RowAppearance = appearance18;
            this.grdSchedule.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdSchedule.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSchedule.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSchedule.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSchedule.DisplayLayout.UseFixedHeaders = true;
            this.grdSchedule.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSchedule.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSchedule.Location = new System.Drawing.Point(0, 0);
            this.grdSchedule.Name = "grdSchedule";
            this.grdSchedule.RowUpdateCancelAction = Infragistics.Win.UltraWinGrid.RowUpdateCancelAction.RetainDataAndActivation;
            this.grdSchedule.Size = new System.Drawing.Size(676, 254);
            this.grdSchedule.TabIndex = 1;
            this.grdSchedule.Text = "Schedule";
            this.grdSchedule.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSchedule.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdSchedule.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdSchedule.AfterEnterEditMode += new System.EventHandler(this.OnGridAfterEnterEditMode);
            this.grdSchedule.AfterExitEditMode += new System.EventHandler(this.OnGridAfterExitEditMode);
            this.grdSchedule.AfterRowUpdate += new Infragistics.Win.UltraWinGrid.RowEventHandler(this.OnGridAfterRowUpdate);
            this.grdSchedule.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdSchedule.CellListSelect += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellListSelect);
            this.grdSchedule.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridSelectionChanged);
            this.grdSchedule.BeforeEnterEditMode += new System.ComponentModel.CancelEventHandler(this.OnGridBeforeEnterEditMode);
            this.grdSchedule.BeforeExitEditMode += new Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventHandler(this.OnGridBeforeExitEditMode);
            this.grdSchedule.AfterRowFilterChanged += new Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventHandler(this.OnGridAfterRowFilterChanged);
            this.grdSchedule.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdSchedule.DragDrop += new System.Windows.Forms.DragEventHandler(this.OnDragDrop);
            this.grdSchedule.DragEnter += new System.Windows.Forms.DragEventHandler(this.OnDragEnter);
            this.grdSchedule.DragOver += new System.Windows.Forms.DragEventHandler(this.OnDragOver);
            this.grdSchedule.DragLeave += new System.EventHandler(this.OnDragLeave);
            this.grdSchedule.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnGridKeyUp);
            this.grdSchedule.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mTrips
            // 
            this.mTrips.DataSetName = "ShipScheduleDataset";
            this.mTrips.Locale = new System.Globalization.CultureInfo("en-US");
            this.mTrips.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // winSchedule
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(676, 254);
            this.Controls.Add(this.uddCarrier);
            this.Controls.Add(this.grdSchedule);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "winSchedule";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ship Schedule";
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Deactivate += new System.EventHandler(this.OnFormDeactivate);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FontChanged += new System.EventHandler(this.OnFormFontChanged);
            this.csSchedule.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.uddCarrier)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSchedule)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTrips)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion
        private void OnFormLoad(object sender, System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Init
                #region Grid Initialization
                this.grdSchedule.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdSchedule.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdSchedule.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdSchedule.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdSchedule.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdSchedule.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdSchedule.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                this.grdSchedule.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdSchedule.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdSchedule.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdSchedule.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                this.grdSchedule.DisplayLayout.Bands[0].Columns["MainZone"].SortIndicator = SortIndicator.Ascending;
                #endregion
                //this.grdSchedule.UpdateMode = (UpdateMode.OnCellChangeOrLostFocus & UpdateMode.OnUpdate);
                this.uddCarrier.DataMember = "CarrierTable";
                this.uddCarrier.DisplayMember = "Description";
                this.uddCarrier.ValueMember = "ID";
                this.uddCarrier.DataSource = ShipScheduleGateway.GetCarriers();

                this.grdSchedule.DataSource = this.mSchedule.Trips;
                this.grdSchedule.Select();
                this.grdSchedule.ActiveRow = this.grdSchedule.Rows.GetRowAtVisibleIndex(0);
                if(this.grdSchedule.ActiveRow != null) this.grdSchedule.ActiveRow.Selected = true;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender, EventArgs e) {
            //Event handler for form activated event
            try {
                //Turn on auto refresh if applicable
                this.mSchedule.StartAutoRefresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender, EventArgs e) {
            //Event handler for form deactivate event
            try {
                //Turn off auto refresh
                this.mSchedule.StopAutoRefresh();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender, EventArgs e) { try { this.csSchedule.Font = this.Font; } catch { } }
        private void OnScheduleChanged(object sender, EventArgs e) {
            //Event handler for change in ship schedule
            reportStatus(new StatusEventArgs("Updated ship schedule " + this.Text + "."));
        }
        private void OnStatusMessage(object sender, StatusEventArgs e) { reportStatus(new StatusEventArgs(e.Message)); }
        private void OnGridSelectionChanged(object sender, Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for selection change
            setUserServices();
        }
        #region Grid Services: OnGridInitializeLayout(), OnGridInitializeRow()
        private void OnGridInitializeLayout(object sender, Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
            try {
                //Set Yes/No value lists for trip state fields; state fields have datetime values so using a proxy column (i.e. TrailerComplete, TrailerComplete2) with yes/no values
                Infragistics.Win.UltraWinGrid.UltraGridBand band = e.Layout.Bands[0];
                band.Columns["TrailerComplete2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
                band.Columns["PaperworkComplete2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
                band.Columns["TrailerDispatched2"].ValueList = e.Layout.ValueLists["YesNoValueList"];
                band.Columns["FreightAssigned2"].ValueList = e.Layout.ValueLists["YesNoValueList"];

                //Setup multi-sort
                band.SortedColumns.Add("TrailerComplete2", true, false);
                band.SortedColumns.Add("PaperworkComplete2", true, false);
                band.SortedColumns.Add("TrailerDispatched2", true, false);
                band.SortedColumns.Add("ScheduledClose", false, false);
                band.SortedColumns.Add("ZoneTag", false, false);
                band.Override.HeaderClickAction = HeaderClickAction.SortMulti;
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
        }
        private void OnGridInitializeRow(object sender, Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //Rules for new field TractorNumber and FreighAssigned added
            try {
                //Combine Zone and Tag fields (show only last 2 digits of tag field)
                string tag = e.Row.Cells["Tag"].Text.Trim().Length > 2 ? e.Row.Cells["Tag"].Text.Substring(1) : e.Row.Cells["Tag"].Text;
                e.Row.Cells["ZoneTag"].Value = e.Row.Cells["MainZone"].Text + "  " + tag;
                string s2tag = e.Row.Cells["S2Tag"].Text.Trim().Length > 2 ? e.Row.Cells["S2Tag"].Text.Substring(1) : e.Row.Cells["S2Tag"].Text;
                e.Row.Cells["S2ZoneTag"].Value = e.Row.Cells["S2MainZone"].Text + "  " + s2tag;

                if(e.Row.Cells["ScheduledArrival"].Value.ToString() != "") {
                    DateTime arrivalDate = Convert.ToDateTime(e.Row.Cells["ScheduledArrival"].Value.ToString());
                    e.Row.Cells["ArrivalDay"].Value = arrivalDate.DayOfWeek.ToString().Substring(0, 3);
                    e.Row.Cells["ArrivalTime"].Value = arrivalDate.ToString("HH:mm");
                }
                if(e.Row.Cells["S2ScheduledArrival"].Value.ToString() != "") {
                    DateTime s2ArrivalDate = Convert.ToDateTime(e.Row.Cells["S2ScheduledArrival"].Value.ToString());
                    e.Row.Cells["S2ArrivalDay"].Value = s2ArrivalDate.DayOfWeek.ToString().Substring(0, 3);
                    e.Row.Cells["S2ArrivalTime"].Value = s2ArrivalDate.ToString("HH:mm");
                }

                if(e.Row.Cells["Canceled"].Value.ToString().Trim().Length > 0) {
                    e.Row.Appearance.ForeColor = System.Drawing.Color.DarkGray;
                    e.Row.Activation = Activation.NoEdit;
                }
                else {
                    if(!(RoleServiceGateway.IsLineHaulAdministrator || RoleServiceGateway.IsLineHaulCoordinator)) {
                        e.Row.Cells["TrailerNumber"].Activation = Activation.NoEdit;
                        e.Row.Cells["Carrier"].Activation = Activation.NoEdit;
                        e.Row.Cells["LoadNumber"].Activation = Activation.NoEdit;
                        //e.Row.Cells["DriverName"].Activation = Activation.NoEdit;
                        e.Row.Cells["ScheduledClose"].Activation = Activation.NoEdit;
                        e.Row.Cells["ScheduledDeparture"].Activation = Activation.NoEdit;
                        e.Row.Cells["Notes"].Activation = Activation.NoEdit;
                        e.Row.Cells["ScheduledArrival"].Activation = Activation.NoEdit;
                        e.Row.Cells["ScheduledOFD1"].Activation = Activation.NoEdit;
                        e.Row.Cells["ArrivalTime"].Activation = Activation.NoEdit;
                    }
                    if(!((RoleServiceGateway.IsLineHaulAdministrator || RoleServiceGateway.IsLineHaulCoordinator) && e.Row.Cells["S2MainZone"].Text.Trim().Length > 0)) {
                        //If Stop 2 Main Zone is empty, then disallow any stop 2 field updates
                        e.Row.Cells["S2Notes"].Activation = Activation.NoEdit;
                        e.Row.Cells["S2ScheduledArrival"].Activation = Activation.NoEdit;
                        e.Row.Cells["S2ScheduledOFD1"].Activation = Activation.NoEdit;
                        e.Row.Cells["S2ArrivalTime"].Activation = Activation.NoEdit;
                    }

                    e.Row.Cells["FreightAssigned2"].Value = e.Row.Cells["FreightAssigned"].Value.ToString().Length > 0 ? "Yes" : "No";
                    e.Row.Cells["FreightAssigned2"].Activation = !global::Argix.Properties.Settings.Default.CanEditFreightAssigned ? Activation.NoEdit : Activation.AllowEdit;
                    e.Row.Cells["TrailerComplete2"].Value = e.Row.Cells["TrailerComplete"].Value.ToString().Length > 0 ? "Yes" : "No";
                    e.Row.Cells["TrailerComplete2"].Activation = e.Row.Cells["FreightAssigned2"].Value.ToString() == "No" ? Activation.NoEdit : Activation.AllowEdit;
                    e.Row.Cells["PaperworkComplete2"].Value = e.Row.Cells["PaperworkComplete"].Value.ToString().Length > 0 ? "Yes" : "No";
                    e.Row.Cells["TrailerDispatched2"].Value = e.Row.Cells["TrailerDispatched"].Value.ToString().Length > 0 ? "Yes" : "No";
                }
                //get active row //Note: This will fire this event again and will get executed at least twice.
                e.Row.RefreshSortPosition();
            }
            catch(ArgumentException ex) { App.ReportError(ex, false, LogLevel.Debug); }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Grid Edit Services: OnCarrierBeforeDropDown(), OnGridCellListSelect(), OnGridBeforeRowUpdate(), OnGridAfterRowUpdate(), OnGridBeforeRowFilterDropDownPopulate(), OnGridAfterRowFilterChanged(), OnGridBeforeExitEditMode()
        private void OnCarrierBeforeDropDown(object sender, System.ComponentModel.CancelEventArgs e) {
            //
            try {
                this.uddCarrier.DisplayLayout.Bands[0].Columns[1].Width = this.grdSchedule.DisplayLayout.Bands[0].Columns["Carrier"].Width;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridCellListSelect(object sender, Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //
            try {
                switch(e.Cell.Column.Key) {
                    case "FreightAssigned2":
                        this.grdSchedule.ActiveRow.Cells["FreightAssigned"].Value = e.Cell.Text == "Yes" ? System.DateTime.Now : System.DBNull.Value as object;
                        break;
                    case "TrailerComplete2":
                        this.grdSchedule.ActiveRow.Cells["TrailerComplete"].Value = e.Cell.Text == "Yes" ? System.DateTime.Now : System.DBNull.Value as object;
                        break;
                    case "PaperworkComplete2":
                        this.grdSchedule.ActiveRow.Cells["PaperworkComplete"].Value = e.Cell.Text == "Yes" ? System.DateTime.Now : System.DBNull.Value as object;
                        break;
                    case "TrailerDispatched2":
                        this.grdSchedule.ActiveRow.Cells["TrailerDispatched"].Value = e.Cell.Text == "Yes" ? System.DateTime.Now : System.DBNull.Value as object;
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridBeforeRowUpdate(object sender, Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            //
            try {
                if(!validateRules(e)) e.Cancel = true;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridAfterRowUpdate(object sender, Infragistics.Win.UltraWinGrid.RowEventArgs e) {
            //Updated: August 29, 2005
            //Handles new DuplicateLoadNumberException exception.
            try {
                this.mSchedule.Update();
                e.Row.Appearance.ResetForeColor();
                if(e.Row.Cells["Canceled"].Value.ToString().Trim() != "") {
                    e.Row.Appearance.ForeColor = System.Drawing.Color.DarkGray;
                    e.Row.Activation = Activation.NoEdit;
                }
                else {
                    e.Row.Activation = Activation.AllowEdit;
                }
                e.Row.Refresh(RefreshRow.FireInitializeRow);
            }
            catch(DuplicateLoadNumberException ex) {
                //Don't discard all changes; set Load# to empty string and update the row
                MessageBox.Show(ex.Message + " Row will be saved without Load Number.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
                e.Row.Cells["LoadNumber"].Value = "";
                e.Row.Update();
                e.Row.Appearance.ForeColor = System.Drawing.Color.Red;
            }
            catch(Exception ex) {
                this.mSchedule.Trips.RejectChanges();
                e.Row.CancelUpdate();
                App.ReportError(ex, true, LogLevel.Error);
            }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender, Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridAfterRowFilterChanged(object sender, Infragistics.Win.UltraWinGrid.AfterRowFilterChangedEventArgs e) {
            //
            UltraGridBand band = this.grdSchedule.DisplayLayout.Bands[0];
            try {
                //Check to see if filter condition is clear - it will be clear if user selects All from 
                //the filter list; otherwise, make sure filter is cleared from the CarrierID column as well
                if(band.ColumnFilters["Carrier"].FilterConditions.Count > 0) {
                    //If there is a filter condition, then modify it by filtering it by CarrierID instead of 
                    //Carrier which filters by unique carrier service name
                    int carrierID = Convert.ToInt32(this.grdSchedule.Rows.GetRowAtVisibleIndex(0).Cells["CarrierID"].Value);
                    band.ColumnFilters.ClearAllFilters();
                    band.ColumnFilters["CarrierID"].FilterConditions.Add(FilterComparisionOperator.Equals, carrierID);
                }
                else
                    band.ColumnFilters.ClearAllFilters();
            }
            catch(Exception ex) { band.ColumnFilters.ClearAllFilters(); App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnGridBeforeEnterEditMode(object sender, CancelEventArgs e) {
            //
            try {
                this.mSchedule.StopAutoRefresh();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridAfterEnterEditMode(object sender, System.EventArgs e) {
            //Event handler for 
            setUserServices();
        }
        private void OnGridBeforeExitEditMode(object sender, Infragistics.Win.UltraWinGrid.BeforeExitEditModeEventArgs e) {
            //This event gets fired just before user is leaving a cell after entering into Edit mode.
            //Two unbound fields Arrival Time and S2 Arrival Time will not be persisted unless we update the bound fields where
            //they get their values in the first place: ScheduledArrival and S2ScheduledArrival.
            //If Time format is incorrect, then UltraGrid automatically throws generic error message;
            try {
                switch(grdSchedule.ActiveCell.Column.Key) {
                    case "ArrivalTime": this.grdSchedule.ActiveRow.Cells["ScheduledArrival"].Value = Convert.ToDateTime(Convert.ToDateTime(grdSchedule.ActiveRow.Cells["ScheduledArrival"].Text).ToShortDateString() + " " + Convert.ToDateTime(grdSchedule.ActiveRow.Cells["ArrivalTime"].Text).ToShortTimeString()); break;
                    case "S2ArrivalTime": this.grdSchedule.ActiveRow.Cells["S2ScheduledArrival"].Value = Convert.ToDateTime(Convert.ToDateTime(grdSchedule.ActiveRow.Cells["S2ScheduledArrival"].Text).ToShortDateString() + " " + Convert.ToDateTime(grdSchedule.ActiveRow.Cells["S2ArrivalTime"].Text).ToShortTimeString()); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnGridAfterExitEditMode(object sender, EventArgs e) {
            //
            try {
                this.mSchedule.StartAutoRefresh();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        #endregion
        #region Grid Support: OnGridMouseDown(), OnGridKeyUp()
        private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
                if(uiElement != null) {
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if(context != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow row = (UltraGridRow)context;
                            row.Activate();
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) {
                            grid.ActiveRow = null;
                        }
                        //else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                        //    if (grid.ActiveRow != null) grid.ActiveRow = null;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { setUserServices(); }
        }
        private void OnGridKeyUp(object sender, System.Windows.Forms.KeyEventArgs e) {
            //Event handler for key up event
            if(e.KeyCode == System.Windows.Forms.Keys.Enter) {
                //Update row on Enter
                this.grdSchedule.ActiveRow.Update();
                e.Handled = true;
            }
        }
        #endregion
        #region Grid Drag/Drop Services: OnDragEnter(), OnDragOver(), OnDragDrop(), OnDragLeave()
        private void OnDragEnter(object sender, System.Windows.Forms.DragEventArgs e) { }
        private void OnDragOver(object sender, System.Windows.Forms.DragEventArgs e) {
            //Event handler for dragging over the grid
            try {
                //Enable appropriate drag drop effect
                //NOTE: Cannot COPY or MOVE to self
                UltraGrid oGrid = (UltraGrid)sender;
                DataObject oData = (DataObject)e.Data;
                if(CanAddLoads && !oGrid.Focused && oData.GetDataPresent(DataFormats.StringFormat, true)) {
                    switch(e.KeyState) {
                        case KEYSTATE_SHIFT: e.Effect = (!oGrid.Focused) ? DragDropEffects.Move : DragDropEffects.None; break;
                        case KEYSTATE_CTL: e.Effect = (!oGrid.Focused) ? DragDropEffects.None : DragDropEffects.None; break;
                        default: e.Effect = (!oGrid.Focused) ? DragDropEffects.Move : DragDropEffects.None; break;
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
                DataObject oData = (DataObject)e.Data;
                if(oData.GetDataPresent(DataFormats.StringFormat, true))
                    AddLoads();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnDragLeave(object sender, System.EventArgs e) { }
        #endregion
        #region User Services: OnItemClick()
        private void OnItemClick(object sender, System.EventArgs e) {
            //Event handler for mneu item clicked
            try {
                ToolStripItem item = (ToolStripItem)sender;
                switch(item.Name) {
                    case "ctxRefresh": Refresh(); break;
                    case "ctxCut":
                        Clipboard.SetDataObject(this.grdSchedule.ActiveCell.SelText, false);
                        this.grdSchedule.ActiveCell.Value = this.grdSchedule.ActiveCell.Text.Remove(this.grdSchedule.ActiveCell.SelStart, this.grdSchedule.ActiveCell.SelLength);
                        break;
                    case "ctxCopy":
                        Clipboard.SetDataObject(this.grdSchedule.ActiveCell.SelText, false);
                        break;
                    case "ctxPaste":
                        IDataObject o = Clipboard.GetDataObject();
                        this.grdSchedule.ActiveCell.Value = this.grdSchedule.ActiveCell.Text.Remove(this.grdSchedule.ActiveCell.SelStart, this.grdSchedule.ActiveCell.SelLength).Insert(this.grdSchedule.ActiveCell.SelStart, (string)o.GetData("Text"));
                        break;
                    case "ctxCancelLoad": CancelLoad(); break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { setUserServices(); this.Cursor = Cursors.Default; }
        }
        #endregion
        #region Local Services: validateRules(), setUserServices(), reportStatus()
        private bool validateRules(Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            //Updated: Apr. 10, 2006 - New rule added
            //Trailer can't be completed if Freight is not assigned.
            bool validated = false;
            DateTime schCloseDate;
            DateTime schDepartDate;
            DateTime schArrivalDate;
            DateTime schOFD1Date;
            DateTime s2SchArrivalDate;
            DateTime s2SchOFD1Date;
            DateTime thisDate = this.mSchedule.ScheduleDate;    // DateTime.Today;
            int valDays = global::Argix.Properties.Settings.Default.ValidationWindow;
            string schCloseDateCellText = e.Row.Cells["ScheduledClose"].Value.ToString().Trim();
            string schDepartDateCellText = e.Row.Cells["ScheduledDeparture"].Value.ToString().Trim();
            string schArrivalDateCellText = e.Row.Cells["ScheduledArrival"].Value.ToString().Trim();
            string schOFD1DateCellText = e.Row.Cells["ScheduledOFD1"].Value.ToString().Trim();
            string arrivalTimeCellText = e.Row.Cells["ArrivalTime"].Value.ToString().Trim();
            string S2MainZoneCellText = e.Row.Cells["S2MainZone"].Value.ToString().Trim();
            string S2ScheduledArrivalCellText = e.Row.Cells["S2ScheduledArrival"].Value.ToString().Trim();
            string S2ScheduledOFD1CellText = e.Row.Cells["S2ScheduledOFD1"].Value.ToString().Trim();
            string trailerComplete = e.Row.Cells["TrailerComplete2"].Value.ToString();
            string freightAssigned = e.Row.Cells["FreightAssigned2"].Value.ToString();
            bool cancel = (e.Row.Cells["Canceled"].Value != System.DBNull.Value);

            //Biz Rules - All dates can be less or greater than 30 days
            //1 - Schedule Close Date <= Scheduled Departure Date
            StringBuilder message = new StringBuilder();
            if(schCloseDateCellText != "") {
                schCloseDate = Convert.ToDateTime(schCloseDateCellText);
                if(schCloseDate.Subtract(thisDate).Days < -valDays || schCloseDate.Subtract(thisDate).Days > valDays)
                    message.Append("Scheduled Close date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
                if(schDepartDateCellText != "") {
                    schDepartDate = Convert.ToDateTime(schDepartDateCellText);
                    if(schDepartDate.Subtract(thisDate).Days < -valDays || schDepartDate.Subtract(thisDate).Days > valDays)
                        message.Append("Scheduled Departure date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
                    if(schCloseDate > schDepartDate)
                        message.Append("Schedule Close Date and Time must be less than or equal to Scheduled Departure Date and Time." + Environment.NewLine);
                }
                else
                    message.Append("Scheduled Departure Date cell can't be empty.");
            }
            else
                message.Append("Scheduled Close Date cell can't be empty.");

            //2- Scheduled arrival Date <= OFD1 Date
            if(schArrivalDateCellText != "") {
                schArrivalDate = Convert.ToDateTime(schArrivalDateCellText).Date;
                if(schArrivalDate.Subtract(thisDate).Days < -valDays || schArrivalDate.Subtract(thisDate).Days > valDays)
                    message.Append("Scheduled Arrival date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
                if(schOFD1DateCellText != "") {
                    schOFD1Date = Convert.ToDateTime(schOFD1DateCellText).Date;
                    if(schOFD1Date.Subtract(thisDate).Days < -valDays || schOFD1Date.Subtract(thisDate).Days > valDays)
                        message.Append("Scheduled OFD1 date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);

                    //Compare just the Date Component 'cause OFD does not have time
                    if(schArrivalDate > schOFD1Date)
                        message.Append("Scheduled Arrival Date must be less than or equal to Scheduled OFD1 Date." + Environment.NewLine);
                }
                else
                    message.Append("Schedule OFD1 Date cell can't be empty." + Environment.NewLine);
            }
            else
                message.Append("Schedule Arrival Date cell can't be empty." + Environment.NewLine);

            //3- Scheduled Departure Date <= Arrival Date
            if(schArrivalDateCellText != "" && schDepartDateCellText != "") {
                //compare date  and time
                schArrivalDate = Convert.ToDateTime(Convert.ToDateTime(schArrivalDateCellText).ToShortDateString() + " " + Convert.ToDateTime(arrivalTimeCellText).ToShortTimeString());
                schDepartDate = Convert.ToDateTime(schDepartDateCellText);
                if(schDepartDate > schArrivalDate)
                    message.Append("Scheduled Departure Date and Time must be less than or equal to Scheduled Arrival Date and Time." + Environment.NewLine);
            }

            //4- If one field is filled for Stop 2, then all should be filled-in
            if(S2MainZoneCellText != "" || S2ScheduledOFD1CellText != "" || S2ScheduledArrivalCellText != "") {
                if(S2MainZoneCellText == "") message.Append("S2 Main Zone can't be empty." + Environment.NewLine);
                if(S2ScheduledArrivalCellText != "") {
                    s2SchArrivalDate = Convert.ToDateTime(S2ScheduledArrivalCellText).Date;
                    if(s2SchArrivalDate.Subtract(thisDate).Days < -valDays || s2SchArrivalDate.Subtract(thisDate).Days > valDays)
                        message.Append("S2 Scheduled Arrival date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
                    if(S2ScheduledOFD1CellText != "") {
                        s2SchOFD1Date = Convert.ToDateTime(S2ScheduledOFD1CellText).Date;
                        if(s2SchOFD1Date.Subtract(thisDate).Days < -valDays || s2SchOFD1Date.Subtract(thisDate).Days > valDays)
                            message.Append("S2 Scheduled OFD1 date can't be less or greater than " + valDays.ToString() + " days." + Environment.NewLine);
                        if(s2SchArrivalDate > s2SchOFD1Date)
                            message.Append("S2 Scheduled Arrival Date must be less than or equal to S2 Scheduled OFD1 Date." + Environment.NewLine);
                    }
                    else
                        message.Append("S2 Scheduled OFD1 Date cell can't be empty." + Environment.NewLine);
                }
                else
                    message.Append("S2 Scheduled Arrival Date cell can't be empty." + Environment.NewLine);

            }
            //5 - TrailerComplete can't be set to yes if FreightAssinged is set to no.
            if(trailerComplete == "Yes" && freightAssigned == "No")
                message.Append("Freight should be assigned first before trailer is marked complete.");
            //************************

            //Invalidate and display validation message unless the load is being cancelled
            if(!cancel && message.ToString() != "")
                MessageBox.Show(message.ToString(), App.Product, MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
                validated = true;
            return validated;
        }
        private void setUserServices() {
            //Set user services
            try {
                //State
                bool hasfreight = this.grdSchedule.ActiveRow != null ? int.Parse(this.grdSchedule.ActiveRow.Cells["TLs"].Value.ToString()) > 0 : false;
                bool cancelled = this.grdSchedule.ActiveRow != null ? this.grdSchedule.ActiveRow.Cells["Canceled"].Value.ToString().Trim().Length > 0 : false;
                bool assigned = this.grdSchedule.ActiveRow != null ? this.grdSchedule.ActiveRow.Cells["FreightAssigned"].Value.ToString().Trim().Length > 0 : false;
                bool loaded = this.grdSchedule.ActiveRow != null ? this.grdSchedule.ActiveRow.Cells["TrailerComplete"].Value.ToString().Trim().Length > 0 : false;
                bool documented = this.grdSchedule.ActiveRow != null ? this.grdSchedule.ActiveRow.Cells["PaperworkComplete"].Value.ToString().Trim().Length > 0 : false;
                bool dispatched = this.grdSchedule.ActiveRow != null ? this.grdSchedule.ActiveRow.Cells["TrailerDispatched"].Value.ToString().Trim().Length > 0 : false;

                //Set services
                this.ctxRefresh.Enabled = true;
                this.ctxCut.Enabled = (this.grdSchedule.ActiveCell != null && this.grdSchedule.ActiveCell.IsInEditMode);
                this.ctxCopy.Enabled = (this.grdSchedule.ActiveCell != null);
                this.ctxPaste.Enabled = (this.grdSchedule.ActiveCell != null && this.grdSchedule.ActiveCell.IsInEditMode && Clipboard.GetDataObject() != null);
                this.ctxCancelLoad.Enabled = RoleServiceGateway.IsLineHaulAdministrator && !hasfreight;
                this.ctxCancelLoad.Text = cancelled ? "Uncancel Load" : "Cancel Load";
                this.ctxCancelLoad.Checked = cancelled;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Error); }
            finally { Application.DoEvents(); if(this.ServiceStatesChanged != null) this.ServiceStatesChanged(this, new EventArgs()); }
        }
        private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
        #endregion
    }
}
