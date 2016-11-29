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

public partial class ConsumerDirectDDUWeeklySummaryByAgent:System.Web.UI.Page {
    //Members
    private string mTitle = "Consumer Direct DDU Weekly Summary By Agent";
    private string mSource = "/Consumer Direct/Consumer Direct DDU Weekly Summary By Agent";
    private string mDSName = "DataSet1";
    private string mUSPName = "uspRptConsumerDirectDDUWeeklySummaryByAgent",mTBLName = "NewTable";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            OnDateParamChanged(this.cboDateParam,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnDateParamChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        this.cboDateValue.DataBind();
        if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 4;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnCustomerChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = this.cboCustomer.SelectedValue != null;
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
            string _vendor = this.cboCustomer.SelectedValue;
            int _delivDays = Convert.ToInt32(this.cboDelivDays.Text);

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _start,_end,_vendor,_delivDays });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter start = new ReportParameter("StartDate",_start);
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    ReportParameter vendor = new ReportParameter("BNVendor",_vendor);
                    ReportParameter delivDays = new ReportParameter("DeliveryDays",_delivDays.ToString());
                    report.SetParameters(new ReportParameter[] { start,end,vendor,delivDays });

                    //Update report rendering with new data
                    report.Refresh();
                    
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds,this.mDSName));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));
                    report.Refresh();
                    break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=BNConsumerDirectCartonDetail.xls");
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
