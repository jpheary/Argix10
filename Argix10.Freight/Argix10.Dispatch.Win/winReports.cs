using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix.Freight {
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
        public bool CanClone { get { return false; } }
        public void Clone() { }
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
        public bool CanNewTemplate { get { return false; } }
        public void NewTemplate() { }
        public bool CanOpenTemplate { get { return false; } }
        public void OpenTemplate() { }
        public bool CanCancelTemplate { get { return false; } }
        public void CancelTemplate() { }
        public bool CanLoadTemplates { get { return false; } }
        public void LoadTemplates() { }
        public bool TemplatesVisible { get { return false; } set { ; } }
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
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                //Initialize controls
                this.cboReports.SelectedIndex = 0;
                Refresh();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnFormActivated(object sender,EventArgs e) {
            //Event handler for form activated event
            try {
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormDeactivate(object sender,EventArgs e) {
            //Event handler for form deactivate event
            try {
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Ask only if there are detail forms open
            try {
            }
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnFormFontChanged(object sender,EventArgs e) { try { } catch { } }
        private void OnReportChanged(object sender, EventArgs e) {
            //
            try {
                switch(this.cboReports.SelectedItem.ToString()) {
                    case "Advanced Pickup Requests":
                        this.rvReports.ServerReport.ReportPath = "/Freight/Advanced Pickup Requests";
                        break;
                    case "Delivery Appointment Sheet":
                        this.rvReports.ServerReport.ReportPath = "/Freight/Delivery Appointment Sheet";
                        break;
                    case "Driver Settlement Sheet":
                        this.rvReports.ServerReport.ReportPath = "/Freight/Driver Settlement Sheet";
                        break;
                    case "PCS Inbound Schedule (Active)":
                        this.rvReports.ServerReport.ReportPath = "/Freight/PCS Inbound Schedule";
                        break;
                    case "PCS Pickup Log (Active)":
                        this.rvReports.ServerReport.ReportPath = "/Freight/PCS Pickup Log";
                        break;
                    case "PCS Trailer Log Inbound":
                        this.rvReports.ServerReport.ReportPath = "/Freight/PCS Trailer Log Inbound";
                        break;
                    case "PCS Trailer Log Outbound":
                        this.rvReports.ServerReport.ReportPath = "/Freight/PCS Trailer Log Outbound";
                        break;
                }
                this.rvReports.RefreshReport();
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        #region User Services: 
        #endregion
        #region Local Services: reportStatus()
        private void reportStatus(StatusEventArgs e) { if (this.StatusMessage != null) this.StatusMessage(this,e); }
        #endregion
    }
}
