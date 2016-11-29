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
    public partial class AgentDashboardControl:UserControl {
        //Members
        private bool mCalendarOpen = false;
        private bool mTerminalMapLoaded = false;

        //Interface
        public AgentDashboardControl() {
            //Constructor
            try {
                InitializeComponent();
                this.wbTerminalMap.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    if (this.cboAgent.Items.Count == 0) {
                        this.mAgentDS.Clear();
                        this.mAgentDS.Merge(CRMGateway.GetAgents());
                        //if (this.cboAgent.Items.Count > 0) this.cboAgent.SelectedIndex = 0;
                        this.cboAgent.SelectedIndex = -1;
                        OnAgentChanged(null,EventArgs.Empty);
                    }
                    this.dtpFrom.Value = DateTime.Today.AddDays(-7);
                    this.dtpTo.Value = DateTime.Today;
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
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
        private void OnAgentChanged(object sender,EventArgs e) {
            //Event handler for change in selected agent
            try {
                this.mTerminals.Clear();
                if (this.cboAgent.SelectedValue != null) {
                    this.mTerminals.Merge(CRMGateway.GetAgentTerminals(this.cboAgent.SelectedValue.ToString()));
                    if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
                    OnAgentTerminalChanged(null,EventArgs.Empty);
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException("Unexpected error on agent selection.",ex)); }
        }
        private void OnAgentTerminalChanged(object sender,EventArgs e) {
            //Event handler for change in agent terminal
            try {
                this.txtTerminalName.Text = this.txtTerminalAddress.Text = this.txtTermainlDetail.Text = "";
                if (this.cboTerminal.SelectedValue != null) {
                    CRMDataset ds = new CRMDataset();
                    ds.Merge(CRMGateway.GetAgentTerminalDetail(this.cboTerminal.SelectedValue.ToString()));
                    if (ds.AgentTable.Rows.Count > 0) {
                        CRMDataset.AgentTableRow agent = ds.AgentTable[0];
                        this.txtTerminalName.Text = agent.AgentNumber.ToString() + " - " + agent.AgentName.Trim();

                        StringBuilder address = new StringBuilder();
                        address.AppendLine((!agent.IsAddressLine1Null() ? agent.AddressLine1.Trim() : ""));
                        address.AppendLine((!agent.IsAddressLine2Null() ? agent.AddressLine2.Trim() : ""));
                        address.AppendLine((!agent.IsCityNull() ? agent.City.Trim() : "") + ", " +
                                        (!agent.IsStateNull() ? agent.State.Trim() : "") + " " +
                                        (!agent.IsZipNull() ? agent.Zip.Trim() : ""));
                        this.txtTerminalAddress.Text = address.ToString();

                        StringBuilder detail = new StringBuilder();
                        detail.AppendLine("Contact: " + (!agent.IsContactNull() ? agent.Contact.Trim() : ""));
                        detail.AppendLine("Phone: " + (!agent.IsPhoneNull() ? agent.Phone.Trim() : ""));
                        detail.AppendLine("Coordinator: " + (!agent.IsCoordinatorNull() ? agent.Coordinator.Trim() : ""));
                        detail.AppendLine("Agent Type: " + (!agent.IsTypeNull() ? agent.Type.Trim() : ""));
                        detail.AppendLine(
                            ("Main Zone: " + (!agent.IsMainZoneNull() ? agent.MainZone.Trim() : "")) + ", " +
                            ("Type= " + (!agent.IsZoneTypeNull() ? agent.ZoneType.Trim() : "")) + ", " +
                            ("Status= " + (!agent.IsZoneStatusNull() ? agent.ZoneStatus.Trim() : "")) + ", " +
                            ("Is Main= " + (!agent.IsIsMainZoneNull() ? agent.IsMainZone.ToString() : ""))
                        );
                        this.txtTermainlDetail.Text = detail.ToString();
                    }
                    refreshReports();
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException("Unexpected error on agent selection.",ex)); }
        }
        private void OnTerminalAddressChanged(object sender,EventArgs e) {
            //Event handler for terminal address (text) changed event
            try {
                //Display the address in a map
                if (this.mTerminalMapLoaded && this.wbTerminalMap.Document != null)
                    this.wbTerminalMap.Document.InvokeScript("MapLocation",new object[] { this.txtTerminalAddress.Text });
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnTerminalDocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) { this.mTerminalMapLoaded = true; OnTerminalAddressChanged(null,EventArgs.Empty); }

        private void refreshReports() {
            //
            if (this.cboTerminal.SelectedValue != null) {
                string snull = null;
                ReportParameter p1 = new ReportParameter("SortCenterID","100000532000000053");
                ReportParameter p2 = new ReportParameter("ScheduleDate",DateTime.Today.AddDays(0).ToString("yyyy-MM-dd"));
                ReportParameter p3 = new ReportParameter("AgentNumber",this.cboTerminal.SelectedValue.ToString().PadLeft(8,'0'));
                this.rv2.ServerReport.DisplayName = "Agent Ship Schedule";
                this.rv2.ServerReport.ReportPath = "/Customer Service/CRM Agent Ship Schedule";
                this.rv2.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3 });
                this.rv2.RefreshReport();

                p1 = new ReportParameter("ClientNumber",snull);
                p2 = new ReportParameter("Division",snull);
                p3 = new ReportParameter("AgentParentNumber",snull);
                ReportParameter p4 = new ReportParameter("AgentNumber",this.cboTerminal.SelectedValue.ToString());
                ReportParameter p5 = new ReportParameter("Region",snull);
                ReportParameter p6 = new ReportParameter("District",snull);
                ReportParameter p7 = new ReportParameter("StoreNumber",snull);
                ReportParameter p8 = new ReportParameter("StartDate",this.dtpFrom.Value.ToString("yyyy-MM-dd"));
                ReportParameter p9 = new ReportParameter("EndDate",this.dtpTo.Value.ToString("yyyy-MM-dd"));
                ReportParameter p10 = new ReportParameter("ClientName","");
                this.rv1.ServerReport.DisplayName = "Delivery Window Summary By Agent";
                this.rv1.ServerReport.ReportPath = "/Customer Service/CRM Delivery Window Summary By Agent";
                this.rv1.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5,p6,p7,p8,p9,p10 });
                this.rv1.RefreshReport();
            }
        }
    }
}
