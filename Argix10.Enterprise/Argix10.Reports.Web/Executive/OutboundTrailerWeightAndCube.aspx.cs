using System;
using System.Data;
using System.Xml;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class OutboundTrailerWeightAndCube:System.Web.UI.Page {
    //Members
    private string mTitle = "Outbound Trailer Weight and Cube";
    private string mSource = "/Executive/Outbound Trailer Weight And Cube";
    private string mDSName = "OBTrailerWeightCubeDS";
    private string mUSPName = "uspRptBOLWeightCubeReport",mTBLName = "NewTable";
    private const string USP_REPORTBYCLIENT = "uspRptBOLWeightCubeReportByClient";
   
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
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from/to dates
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for change in terminal value
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAgentChanged(object sender,EventArgs e) {
        //Event handler for change in agent value
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTypeChanged(object sender,EventArgs e) {
        //Event handler for change in agent value
        //Report  by client is export only
        //switch(this.cboType.SelectedValue) {
        //    case this.mUSPName: break;
        //    case USP_REPORTBYCLIENT: break;
        //}
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
            string _start = this.ddpBOL.FromDate.ToString("yyyy-MM-dd");
            string _end = this.ddpBOL.ToDate.ToString("yyyy-MM-dd");
            string _terminal = this.cboTerminal.SelectedValue != "" ? this.cboTerminal.SelectedValue : null;
            string _agent = this.cboAgent.SelectedValue != "" ? this.cboAgent.SelectedValue : null;
        
            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.cboType.SelectedValue,mTBLName,new object[] { _start,_end,_agent,_terminal });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter start = new ReportParameter("StartBolDate",_start);
                    ReportParameter end = new ReportParameter("EndBolDate",_end);
                    ReportParameter agent = new ReportParameter("Agent",(_agent==null?"All":_agent));
                    report.SetParameters(new ReportParameter[] { start,end,agent });

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
                    Response.AddHeader("Content-Disposition","inline; filename=OutboundTrailerWeightAndCube.xls");
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
