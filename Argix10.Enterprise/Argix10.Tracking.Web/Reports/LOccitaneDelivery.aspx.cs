using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class _LOccitaneDelivery:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "L'Occitane Delivery";
    private const string REPORT_DESC = "";
    private const string REPORT_SRC = "/Tracking/LOccitane Delivery";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptLOCCITANEShipmentsByDeliveryDate",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = REPORT_NAME + " Report";
                Master.ReportDesc = REPORT_DESC;
                this.txtStart.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                this.txtEnd.Text = DateTime.Today.ToString("MM/dd/yyyy");
                OnValidateForm(null, EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnPickupDatesChanged(object sender, EventArgs e) {
        //Event handler for change in from/to date
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            Master.Validated = true;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string _start = DateTime.Parse(this.txtStart.Text).ToString("yyyy-MM-dd");
            string _end = DateTime.Parse(this.txtEnd.Text).ToString("yyyy-MM-dd");
            string _flag = this.cboFilter.SelectedValue;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _start,_end,_flag });
            if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
                switch(e.CommandName) {
                    case "Run":
                        //Set local report and data source
                        System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                        report.LoadReportDefinition(stream);
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                        //Set the report parameters for the report
                        ReportParameter start = new ReportParameter("FromDate",_start);
                        ReportParameter end = new ReportParameter("ToDate",_end);
                        report.SetParameters(new ReportParameter[] { start,end });

                        //Update report rendering with new data
                        report.Refresh();
                    
                        if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                        break;
                }
            }
            else 
                Master.ShowMessageBox("There were no records found.");
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
