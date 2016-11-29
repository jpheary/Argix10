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

public partial class ConsumerDirectWeeklyPerformanceSummary:System.Web.UI.Page {
    //Members
    private string mTitle = "Consumer Direct Weekly Performance Summary";
    private const string REPORT_SRC_DDU = "/Consumer Direct/Consumer Direct Weekly DDU Performance Summary";
    private const string REPORT_SRC_NDC = "/Consumer Direct/Consumer Direct Weekly NDC Performance Summary";
    private const string REPORT_SRC_SCF = "/Consumer Direct/Consumer Direct Weekly SCF Performance Summary";
    private string mDSName = "DataSet1";
    private const string USP_REPORT_DDU = "uspRptConsumerDirectWeeklyDDUPerformanceSummary",mTBLName = "NewTable";
    private const string USP_REPORT_DDU_SUBSET = "uspRptConsumerDirectWeeklyDDUPerformanceSummaryZipSubset";
    private const string USP_REPORT_NDC = "uspRptConsumerDirectWeeklyNDCPerformanceSummary";
    private const string USP_REPORT_SCF = "uspRptConsumerDirectWeeklySCFPerformanceSummary";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            this.cboDateValue.DataBind();
            if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 1;
            OnTypeChanged(this.cboType,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for change in selected type
        this.chkZips.Enabled = this.cboType.SelectedValue == "DDU";
    }
    protected void OnCustomerChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.cboDateValue.SelectedValue.Length > 0);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string dates = this.cboDateValue.SelectedValue;
            string _start = dates.Split(':')[0].Trim();
            string _end = dates.Split(':')[1].Trim();
            string type = this.cboType.SelectedValue;
            string _vendor = this.cboCustomer.SelectedValue;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds=null;
            switch(type) {
                case "DDU":
                    if(this.chkZips.Checked)
                        ds = enterprise.FillDataset(USP_REPORT_DDU,mTBLName,new object[] { _end,_vendor });
                    else
                        ds = enterprise.FillDataset(USP_REPORT_DDU_SUBSET,mTBLName,new object[] { _end,_vendor });
                    break;
                case "NDC":
                    ds = enterprise.FillDataset(USP_REPORT_NDC,mTBLName,new object[] { _end,_vendor });
                    break;
                case "SCF":
                    ds = enterprise.FillDataset(USP_REPORT_SCF,mTBLName,new object[] { _end,_vendor });
                    break;
            }
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = null;
                    switch(type) {
                        case "DDU": stream = Master.GetReportDefinition(REPORT_SRC_DDU); break;
                        case "NDC": stream = Master.GetReportDefinition(REPORT_SRC_NDC); break;
                        case "SCF": stream = Master.GetReportDefinition(REPORT_SRC_SCF); break;
                    }
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    ReportParameter vendor = new ReportParameter("VendorNumber",_vendor);
                    ReportParameter customer = new ReportParameter("Customer",this.cboCustomer.SelectedItem.Text);
                    report.SetParameters(new ReportParameter[] { end,vendor,customer });

                    //Update report rendering with new data
                    report.Refresh();
                    
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds,this.mDSName));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));
                    report.Refresh(); break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=BNConsumerDirectWeeklyPerformanceSummary.xls");
                    Response.ContentType = "application/vnd.ms-excel";  //application/octet-stream";
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                    DataGrid dg = new DataGrid();
                    dg.DataSource = ds.Tables[mTBLName];
                    dg.DataBind();
                    dg.RenderControl(hw);
                    Response.Write(sw.ToString());
                    Response.End();
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
