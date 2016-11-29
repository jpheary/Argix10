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
    public partial class ClientDashboardControl:UserControl {
        //Members
        private bool mCalendarOpen = false;
        private bool mStoreMapLoaded = false;

        //Interface
        public ClientDashboardControl() {
            //
            try {
                InitializeComponent();
                this.wbStoreMap.Url = new Uri(global::Argix.Properties.Settings.Default.MapUrl);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    if (cboClient.Items.Count == 0) {
                        this.mClientDS.Clear();
                        this.mClientDS.Merge(CRMGateway.GetCompanies());
                        if (this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = -1;
                        OnClientSelected(null,EventArgs.Empty);
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
        private void OnClientSelected(object sender,EventArgs e) {
            //Event handler for change in comapny
            try {
                this.mskStore.Text = "";
            }
            catch (Exception ex) { App.ReportError(new ApplicationException("Unexpected error when client selected.",ex)); }
        }
        private void OnStoreTextChanged(object sender,EventArgs e) {
            //Event handler for change in store number
            try {
                this.txtStoreName.Text = this.txtStoreAddress.Text = this.txtStoreDetail.Text = "";
            }
            catch (Exception ex) { App.ReportError(new ApplicationException("Unexpected error when store changed.",ex)); }
        }
        private void OnStoreKeyUp(object sender,KeyEventArgs e) {
            //Event handler for store textbox key up event
            try {
                if (e.KeyCode == Keys.Enter) {
                    this.txtStoreName.Text = this.txtStoreAddress.Text = this.txtStoreDetail.Text = "";
                    if (this.cboClient.SelectedValue != null && this.mskStore.Text.Trim().Length > 0) {
                        CRMDataset ds = new CRMDataset();
                        ds.Merge(CRMGateway.GetStoreDetail(int.Parse(this.cboClient.SelectedValue.ToString()),int.Parse(this.mskStore.Text)));
                        if (ds.StoreTable.Rows.Count > 0) {
                            CRMDataset.StoreTableRow store = ds.StoreTable[0];
                            this.txtStoreName.Text = store.StoreName.Trim() + " (store #" + store.StoreNumber.ToString() + "; substore #" + store.SubStoreNumber.Trim() + ")";

                            StringBuilder address = new StringBuilder();
                            address.AppendLine((!store.IsStoreAddressline1Null() ? store.StoreAddressline1.Trim() : ""));
                            address.AppendLine((!store.IsStoreAddressline2Null() ? store.StoreAddressline2.Trim() : ""));
                            address.AppendLine((!store.IsStoreCityNull() ? store.StoreCity.Trim() : "") + ", " +
                                            (!store.IsStoreStateNull() ? store.StoreState.Trim() : "") + " " +
                                            (!store.IsStoreZipNull() ? store.StoreZip.Trim() : ""));
                            this.txtStoreAddress.Text = address.ToString();

                            StringBuilder detail = new StringBuilder();
                            detail.AppendLine((!store.IsRegionDescriptionNull() ? store.RegionDescription.Trim() : "") +
                                                            " (" + (!store.IsRegionNull() ? store.Region.Trim() : "") + "), " +
                                                            (!store.IsDistrictNameNull() ? store.DistrictName.Trim() : "") +
                                                            " (" + (!store.IsDistrictNull() ? store.District.Trim() : "") + ")");
                            detail.AppendLine("Zone " + (!store.IsZoneNull() ? store.Zone.Trim() : "") + ", " +
                                                            "Agent " + (!store.IsAgentNumberNull() ? store.AgentNumber.Trim() : "") + " " +
                                                            (!store.IsAgentNameNull() ? store.AgentName.Trim() : ""));
                            detail.AppendLine("Window " + (!store.IsWindowTimeStartNull() ? store.WindowTimeStart.ToString("HH:mm") : "") + " - " +
                                                (!store.IsWindowTimeEndNull() ? store.WindowTimeEnd.ToString("HH:mm") : "") + ", " +
                                                "Del Days " + getDeliveryDays(store) + ", " +
                                                (!store.IsScanStatusDescrptionNull() ? store.ScanStatusDescrption.Trim() : ""));
                            detail.AppendLine("JA Transit " + (!store.IsStandardTransitNull() ? store.StandardTransit.ToString() : "") + ", " + "OFD " + getOFD(store));
                            detail.AppendLine("Special Inst: " + (!store.IsSpecialInstructionsNull() ? store.SpecialInstructions.Trim() : ""));
                            this.txtStoreDetail.Text = detail.ToString();
                        }
                    }
                    refreshReports();
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException("Unexpected error when store keyed in.",ex)); }
        }
        private void OnStoreAddressChanged(object sender,EventArgs e) {
            //Event handler for shipper address (text) changed event
            try {
                //Display the address in a map
                if (this.mStoreMapLoaded && this.wbStoreMap.Document != null)
                    this.wbStoreMap.Document.InvokeScript("MapLocation",new object[] { this.txtStoreAddress.Text });
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnStoreDocumentCompleted(object sender,WebBrowserDocumentCompletedEventArgs e) { this.mStoreMapLoaded = true; OnStoreAddressChanged(null,EventArgs.Empty); }

        private void refreshReports() {
            //
            if (this.cboClient.SelectedValue != null) {
                string snull = null;
                string clientNum = this.cboClient.Text == "All" ? null : this.cboClient.SelectedValue.ToString().Substring(this.cboClient.SelectedValue.ToString().Length - 3,3);
                ReportParameter p1 = new ReportParameter("ClientNumber",clientNum);
                ReportParameter p2 = new ReportParameter("StoreNumber",(this.mskStore.Text.Trim() == "" ? null : this.mskStore.Text.Trim()));
                ReportParameter p3 = new ReportParameter("StartDate",this.dtpFrom.Value.ToString("yyyy-MM-dd"));
                ReportParameter p4 = new ReportParameter("EndDate",this.dtpTo.Value.ToString("yyyy-MM-dd"));
                ReportParameter p5 = new ReportParameter("Division",snull);
                ReportParameter p6 = new ReportParameter("AgentNumber",snull);
                ReportParameter p7 = new ReportParameter("Region",snull);
                ReportParameter p8 = new ReportParameter("District",snull);
                ReportParameter p9 = new ReportParameter("Exception","All Deliveries");
                
                this.rv1.ServerReport.DisplayName = "Delivery Window Summary";
                this.rv1.ServerReport.ReportPath = "/Customer Service/CRM Delivery Window Summary By Store";
                this.rv1.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5,p6,p7,p8 });
                this.rv1.RefreshReport();
                
                this.rv2.ServerReport.DisplayName = "Delivery Window Detail";
                this.rv2.ServerReport.ReportPath = "/Customer Service/CRM Delivery Window Detail By Store";
                this.rv2.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3,p4,p5,p6,p7,p8,p9 });
                this.rv2.RefreshReport();
            }
        }
        private string getDeliveryDays(CRMDataset.StoreTableRow store) {
            //Return delivery days from the dataset
            string ddays = "";

            //Check for overrides
            if (!store.IsIsDeliveryDayMondayNull()) ddays += (store.IsDeliveryDayMonday.Trim() == "Y" ? "M" : "");
            if (!store.IsIsDeliveryDayTuesdayNull()) ddays += (store.IsDeliveryDayTuesday.Trim() == "Y" ? "T" : "");
            if (!store.IsIsDeliveryDayWednesdayNull()) ddays += (store.IsDeliveryDayWednesday.Trim() == "Y" ? "W" : "");
            if (!store.IsIsDeliveryDayThursdayNull()) ddays += (store.IsDeliveryDayThursday.Trim() == "Y" ? "R" : "");
            if (!store.IsIsDeliveryDayFridayNull()) ddays += (store.IsDeliveryDayFriday.Trim() == "Y" ? "F" : "");

            //If no overrides, then all days are valid
            if (ddays.Trim().Length == 0) ddays = "MTWRF";
            return ddays;
        }
        private string getOFD(CRMDataset.StoreTableRow store) {
            //Return delivery days from the dataset
            string ofd = "";

            //OFD1, OFD2, or NONE
            if (!store.IsIsOutforDeliveryDay1Null())
                ofd += (store.IsOutforDeliveryDay1.Trim() == "Y" ? "DAY1" : "");
            else if (!store.IsIsOutforDeliveryDay2Null())
                ofd += (store.IsOutforDeliveryDay2.Trim() == "Y" ? "DAY2" : "");
            else
                ofd += "";
            return ofd;
        }
    }
}
