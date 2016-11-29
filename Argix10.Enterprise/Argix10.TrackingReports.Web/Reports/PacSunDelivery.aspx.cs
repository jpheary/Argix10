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

public partial class _PacSunDelivery:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "PacSun Delivery";
    private const string REPORT_SRC = "/Tracking/PacSun Delivery";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptPacSunShipmentsByDeliveryDate",TBL_REPORT = "NewTable";
    private const string USP_REPORT_OSD = "uspRptPacSunShipmentsByDeliveryDateException";
   
    //Interface
    protected void Page_Init(object sender,EventArgs e) {
        //Event handler for page init event
        try {
            if(!Page.IsPostBack) {
                //Data cache for drillthrough reports
                Session.Add("PacSunDS",null);
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = REPORT_NAME + " Report";
                OnValidateForm(null,EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
            Master.Viewer.Drillthrough += new DrillthroughEventHandler(OnViewerDrillthrough);
            Master.Viewer.ShowBackButton = true;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            //Validate business rules
            Master.Validated = true;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();
            Session["PacSunDS"] = null;

            //Get parameters for the query
            string _start = this.ddpSetup.FromDate.ToString("yyyy-MM-dd");
            string _end = this.ddpSetup.ToDate.ToString("yyyy-MM-dd");
            string _flag = this.cboFilter.SelectedValue;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = enterprise.FillDataset((_flag=="0"?USP_REPORT:USP_REPORT_OSD),TBL_REPORT,new object[] { _start,_end });
            Session["PacSunDS"] = ds;
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
                    case "Data":
                        //Set local export report and data source
                        report.LoadReportDefinition(Master.CreateExportRdl(ds, REPORT_DS, "RGXVMSQLR.TSORT"));
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                        report.Refresh(); break;
                    case "Excel":
                        //Create Excel mime-type page
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.Charset = "";
                        Response.AddHeader("Content-Disposition","inline; filename=PacSunDelivery.xls");
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
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    #region Data Services: OnViewerDrillthrough()
    protected void OnViewerDrillthrough(object sender,DrillthroughEventArgs e) {
        //Event handler for drill through
        try {
            if(!e.Report.IsDrillthroughReport) return;

            //Determine which drillthrough report is requested
            LocalReport report = (LocalReport)e.Report;
            switch(e.ReportPath) {
                case "PacSun Delivery Carton Detail":
                case "PacSun Delivery OSD Detail": 
                    //Get drillthrough parameters
                    string _start="",_end="",_terminal="",_store="",_manifest="";
                    for(int i=0;i<report.OriginalParametersToDrillthrough.Count;i++) {
                        switch(report.OriginalParametersToDrillthrough[i].Name) {
                            case "FromDate": _start = report.OriginalParametersToDrillthrough[i].Values[0]; break;
                            case "ToDate": _end = report.OriginalParametersToDrillthrough[i].Values[0]; break;
                            case "TerminalName": _terminal = report.OriginalParametersToDrillthrough[i].Values[0]; break;
                            case "StoreNumber": _store = report.OriginalParametersToDrillthrough[i].Values[0]; break;
                            case "ManifestNumber": _manifest = report.OriginalParametersToDrillthrough[i].Values[0]; break;
                        }
                    }

                    //Set data source and parameters
                    System.IO.Stream stream = Master.GetReportDefinition("/Tracking/" + e.ReportPath);
                    report.LoadReportDefinition(stream);
                    DataSet ds = (DataSet)Session["PacSunDS"];
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                    ReportParameter start = new ReportParameter("FromDate",_start);
                    ReportParameter end = new ReportParameter("ToDate",_end);
                    ReportParameter terminal = new ReportParameter("TerminalName",_terminal);
                    ReportParameter store = new ReportParameter("StoreNumber",_store);
                    ReportParameter manifest = new ReportParameter("ManifestNumber",_manifest);
                    e.Report.SetParameters(new ReportParameter[] { start,end,terminal,store,manifest });
                    e.Report.Refresh();
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    #endregion
}
