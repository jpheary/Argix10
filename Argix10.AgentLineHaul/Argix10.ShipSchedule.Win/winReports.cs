using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting;

namespace Argix.AgentLineHaul {
    //
    public partial class winReports : Form, ISchedule {
        //Members
        public event StatusEventHandler StatusMessage = null;
        public event EventHandler ServiceStatesChanged = null;

        //Interface
        public winReports() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        #region ISchedule interface
        public bool CanNew { get { return false; } }
        public void New() { }
        public bool CanOpen { get { return false; } }
        public void Open() { }
        public bool CanCancel { get { return false; } }
        public void Cancel() { }
        public bool CanSave { get { return false; } }
        public void Save(string filename) { }
        public bool CanExport { get { return false; } }
        public void Export() { }
        public void Export(string filename) { }
        public bool CanPrint { get { return false; } }
        public void Print(bool showDialog) { }
        public bool CanPreview { get { return false; } }
        public void PrintPreview() { }

        public bool CanCut { get { return false; } }
        public void Cut() { }
        public bool CanCopy { get { return false; } }
        public void Copy() { }
        public bool CanPaste { get { return false; } }
        public void Paste() { }

        public bool CanAddLoads { get { return false; } }
        public void AddLoads() { }
        public bool CanCancelLoad { get { return false; } }
        public bool IsLoadCancelled { get { return false; } }
        public void CancelLoad() { }

        public bool CanEmailCarriers { get { return false; } }
        public void EmailCarriers(bool showDialog) { }
        public bool CanEmailAgents { get { return false; } }
        public void EmailAgents(bool showDialog) { }
        #endregion
        public override void Refresh() {
            try {
                this.Cursor = Cursors.WaitCursor;
                reportStatus(new StatusEventArgs("Refreshing..."));
                this.rvReports.RefreshReport();
                base.Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnFormLoad(object sender, EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                this.cboReports.SelectedIndex = 0;
                Refresh();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender, EventArgs e) {
            //Event handler for form activated event
            try {
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender, EventArgs e) {
            //Event handler for form deactivate event
            try {
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnFormClosing(object sender, FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender, EventArgs e) { try { } catch { } }
        private void OnReportChanged(object sender, EventArgs e) {
            //
            try {
                switch(this.cboReports.SelectedItem.ToString()) {
                    case "Ship Schedule Arrivals":
                        this.rvReports.ServerReport.ReportPath = "/AgentLineHaul/Ship Schedule Arrivals";
                        break;
                    case "Ship Schedule Departures":
                        this.rvReports.ServerReport.ReportPath = "/AgentLineHaul/Ship Schedule Departures";
                        break;
                    case "Ship Schedule OFDs":
                        this.rvReports.ServerReport.ReportPath = "/AgentLineHaul/Ship Schedule OFDs";
                        break;
                }
                this.rvReports.RefreshReport();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region User Services:
        #endregion
        #region Local Services: reportStatus()
        private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
        #endregion
    }
}
