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

public partial class TransitByDay:System.Web.UI.Page {
    //Members
    private string mTitle = "Transit By Day";
    private string mSource = "/Customer Service/Transit By Day";
    private string mDSName = "TransitByDayDS";
    private string mUSPName = "uspRptTransitByday",mTBLName = "NewTable";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            OnActiveOnlyChecked(this.chkActiveOnly,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from/to date
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnActiveOnlyChecked(object sender,EventArgs e) {
        //Event handler for check state changed
        this.cboClient.Items.Clear();
        this.cboClient.Items.Add(new ListItem("All",""));
        this.cboClient.DataBind();
        this.cboClient.SelectedIndex = 0;
        OnClientChanged(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = true;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string _start = this.ddpSetup.FromDate.ToString("yyyy-MM-dd");
            string _end = this.ddpSetup.ToDate.ToString("yyyy-MM-dd");
            string _client = this.cboClient.SelectedValue;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _start,_end,(_client == "" ? null : _client) });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
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
                    Response.AddHeader("Content-Disposition","inline; filename=TransitByDay.xls");
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