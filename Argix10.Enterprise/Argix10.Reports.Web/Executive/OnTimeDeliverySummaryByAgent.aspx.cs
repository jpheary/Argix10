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

public partial class OnTimeDeliverySummaryByAgent :System.Web.UI.Page {
    //Members
    private string mTitle = "On Time Delivery Summary By Agent";
    private string mSource = "/Executive/On Time Delivery Summary By Agent";
    private string mDSName = "OnTimeDeliverySummaryByAgentDS";
    private string mUSPName = "uspRptOnTimeDeliverySummaryByAgent",mTBLName = "NewTable";
    
    //Interface
    protected override void OnInit(EventArgs e) {
        //Event handler for page Init event
        if (!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Get configuration values for this control
            System.Xml.XmlNode configuration = Master.ConfigurationData;
            if (configuration != null) {
                this.mTitle = configuration.Attributes["name"].Value;
                this.mSource = configuration.Attributes["src"].Value;
                this.mDSName = configuration.Attributes["ds"].Value;
                this.mUSPName = configuration.Attributes["usp"].Value;
                this.mTBLName = configuration.Attributes["tbl"].Value;
            }
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            Master.ShowSubAgents = true;
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
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
            string _client = Master.ClientNumber;
            string _division = Master.Division;
            string _agentnumber = Master.AgentNumber;
            string _subagentnumber = Master.SubAgentNumber;
            string _region = Master.Region;
            string _district = Master.District;
            string _store = Master.StoreNumber;
            string _start = Master.StartDate;
            string _end = Master.EndDate;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _client,_division,_agentnumber,_subagentnumber,_region,_district,_store,_start,_end });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter clientName = new ReportParameter("ClientName",Master.ClientName);
                    ReportParameter client = new ReportParameter("ClientNumber",_client);
                    ReportParameter division = new ReportParameter("Division",_division);
                    ReportParameter agentnumber = new ReportParameter("AgentParentNumber",_agentnumber);
                    ReportParameter subagentnumber = new ReportParameter("AgentNumber",_subagentnumber);
                    ReportParameter region = new ReportParameter("Region"); if(_region != null) region.Values.Add(_region);
                    ReportParameter district = new ReportParameter("District"); if(_district != null) district.Values.Add(_district);
                    ReportParameter store = new ReportParameter("StoreNumber"); if(_store != null) store.Values.Add(_store);
                    ReportParameter start = new ReportParameter("StartDate",_start);
                    ReportParameter end = new ReportParameter("EndDate",_end);
                    report.SetParameters(new ReportParameter[] { client,division,agentnumber,subagentnumber,region,district,store,start,end,clientName });

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
                    Response.AddHeader("Content-Disposition","inline; filename=OnTimeDeliverySummaryByAgent.xls");
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
