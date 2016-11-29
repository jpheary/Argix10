using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Argix.Customers {
    //
    public partial class ManagerDashboardControl2:UserControl {
        //Members
        private bool mCalendarOpen = false;

        //Interface
        public ManagerDashboardControl2() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    if (this.cboClient.Items.Count == 0) {
                        this.mClientDS.Clear();
                        this.mClientDS.Merge(CRMGateway.GetCompanies());
                        OnClientChanged(null,EventArgs.Empty);
                    }
                    if (this.cboAgent.Items.Count == 0) {
                        this.mAgentDS.Clear();
                        this.mAgentDS.AgentTable.AddAgentTableRow("","","All","","","","","",0,"","","","","","","","","","");
                        this.mAgentDS.Merge(CRMGateway.GetAgents());
                        if (this.cboAgent.Items.Count > 0) this.cboAgent.SelectedIndex = 0;
                        OnAgentChanged(null,EventArgs.Empty);
                    }
                    this.dtpFrom.Value = DateTime.Today.AddDays(-90);
                    this.dtpTo.Value = DateTime.Today;
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message, ex)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnCalendarOpened(object sender,System.EventArgs e) {
            //Event handler for calendar dropped down
            this.mCalendarOpen = true;
        }
        private void OnCalendarClosed(object sender,System.EventArgs e) {
            //Event handler for date picker calendar closed
            try {
                //Allow calendar to close
                this.dtpFrom.Refresh();
                this.dtpTo.Refresh();
                Application.DoEvents();

                //Flag calendar as closed; sync calendars & change terminal pickup date
                this.mCalendarOpen = false;
                refreshReports();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message,ex)); }
        }
        private void OnCalendarValueChanged(object sender,System.EventArgs e) {
            //Event handler for pickup date changed
            try {
                //Sync calendars & change terminal pickup date if the calendar is closed
                if (!this.mCalendarOpen) refreshReports();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message,ex)); }
        }
        private void OnClientChanged(object sender,EventArgs e) {
            //Event handler for change in selected client
            try {
                refreshReports();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message,ex)); }
        }
        private void OnAgentChanged(object sender,EventArgs e) {
            //Event handler for change in selected agent
            try {
                refreshReports();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message,ex)); }
        }
        private void refreshReports() {
            //
            try {
                if (this.cboClient.SelectedValue != null && this.cboAgent.SelectedValue != null) {
                    string clientNum = this.cboClient.Text == "All" ? null : this.cboClient.SelectedValue.ToString();
                    string clientName = this.cboClient.Text == "All" ? "All Clients" : this.cboClient.Text;
                    string agentNum = this.cboAgent.Text == "All" ? null : this.cboAgent.SelectedValue.ToString();
                    string agentName = this.cboAgent.Text == "All" ? "All Agents" : this.cboAgent.Text;

                    ReportParameter p1 = new ReportParameter("FromDate",this.dtpFrom.Value.ToString("yyyy-MM-dd"));
                    ReportParameter p2 = new ReportParameter("ToDate",this.dtpTo.Value.ToString("yyyy-MM-dd"));
                    ReportParameter p3 = new ReportParameter("CompanyID",clientNum);
                    ReportParameter p4 = new ReportParameter("CompanyName",clientName);
                    ReportParameter p5 = new ReportParameter("AgentNumber",agentNum);
                    ReportParameter p6 = new ReportParameter("AgentName",agentName);
                    this.rv1.ServerReport.DisplayName = "CRM Issues By Type";
                    this.rv1.ServerReport.ReportPath = "/Customer Service/CRM Issues By Type";
                    this.rv1.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5,p6 });
                    this.rv1.RefreshReport();

                    this.rv2.ServerReport.DisplayName = "CRM Issues By Month";
                    this.rv2.ServerReport.ReportPath = "/Customer Service/CRM Issues By Month";
                    this.rv2.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5,p6 });
                    this.rv2.RefreshReport();

                    this.rv3.ServerReport.DisplayName = "CRM Issues By Client";
                    this.rv3.ServerReport.ReportPath = "/Customer Service/CRM Issues By Client";
                    this.rv3.ServerReport.SetParameters(new ReportParameter[] { p1,p2 });
                    this.rv3.RefreshReport();

                    this.rv4.ServerReport.DisplayName = "CRM Issues By Agent";
                    this.rv4.ServerReport.ReportPath = "/Customer Service/CRM Issues By Agent";
                    this.rv4.ServerReport.SetParameters(new ReportParameter[] { p1,p2 });
                    this.rv4.RefreshReport();
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException("",ex)); }
        }
    }
}
