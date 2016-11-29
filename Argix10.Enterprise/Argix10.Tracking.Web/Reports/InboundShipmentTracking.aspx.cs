using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class InboundShipmentTracking:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Inbound Shipment Tracking";
    private const string REPORT_DESC = "";
    private const string REPORT_SRC = "/Tracking/Inbound Shipment Tracking";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptInboundShipmentTrackingByDate",TBL_REPORT = "NewTable";
   
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
                OnPickupDatesChanged(null, EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnPickupDatesChanged(object sender, EventArgs e) {
        //Event handler for chnage in from/to date
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
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
            string _client = this.ddlClient.SelectedValue;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,null,_start,_end });
            if (ds.Tables[TBL_REPORT].Rows.Count >= 0) {
                switch (e.CommandName) {
                    case "Run":
                        //Set local report and data source
                        System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                        report.LoadReportDefinition(stream);
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                        DataSet _ds = enterprise.FillDataset("uspRptClientRead","NewTable",new object[] { _client });
                        report.DataSources.Add(new ReportDataSource("DataSet2",_ds.Tables["NewTable"]));

                        //Set the report parameters for the report
                        ReportParameter number = new ReportParameter("ClientNumber",_client);
                        ReportParameter div = new ReportParameter("ClientDivision");
                        ReportParameter start = new ReportParameter("StartDate",_start);
                        ReportParameter end = new ReportParameter("EndDate",_end);
                        report.SetParameters(new ReportParameter[] { number,div,start,end });

                        //Update report rendering with new data
                        report.Refresh();

                        if (!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                        break;
                }
            }
            else 
                Master.ShowMessageBox("There were no records found.");
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
