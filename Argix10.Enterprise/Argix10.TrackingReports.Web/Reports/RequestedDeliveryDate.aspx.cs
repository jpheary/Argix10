using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class RequestedDeliveryDate :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Requested Delivery Date";
    private const string REPORT_SRC = "/Tracking/Requested Delivery Date";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptRandomHouseRequestedDeliveryDate",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = REPORT_NAME + " Report";
                OnValidateForm(null,EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from to date
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
            string _ofddate = this.ddpDelivery.FromDate.ToString("yyyy-MM-dd");

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _ofddate });
            if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
                switch(e.CommandName) {
                    case "Run":
                        //Set local report and data source
                        System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                        report.LoadReportDefinition(stream);
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                        //Set the report parameters for the report
                        ReportParameter rdddate = new ReportParameter("RequestedDeliveryDate",_ofddate);
                        report.SetParameters(new ReportParameter[] { rdddate });

                        //Update report rendering with new data
                        report.Refresh();
                        break;
                    case "Data":
                        //Set local export report and data source
                        report.LoadReportDefinition(Master.CreateExportRdl(ds, REPORT_DS, "RGXVMSQLR.TSORT"));
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                        report.Refresh();break;
                    case "Excel":
                        //Create Excel mime-type page
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.Charset = "";
                        Response.AddHeader("Content-Disposition","inline; filename=RequestedDeliveryDate.xls");
                        Response.ContentType = "application/vnd.ms-excel";  //application/octet-stream";
                        System.IO.StringWriter sw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                        DataGrid dg = new DataGrid();
                        dg.DataSource = ds.Tables[TBL_REPORT];
                        dg.DataBind();
                        dg.RenderControl(hw);
                        Response.Write(sw.ToString());
                        Response.End();
                        break;
                }
            } 
            else {
                Master.Status = "There were no records found.";
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
}
