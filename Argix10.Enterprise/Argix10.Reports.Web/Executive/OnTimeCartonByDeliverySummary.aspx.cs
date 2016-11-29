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

public partial class OnTimeCartonByDeliverySummary:System.Web.UI.Page {
    //Members
    private string mTitle = "On Time Carton By Delivery Summary";
    private string mSource = "/Executive/On Time Carton By Delivery Summary";
    private string mDSName = "OnTimeCartonByDeliverySummaryDS";
    private string mUSPName = "uspRptOnTimeCartonByDeliverySummary",mTBLName = "NewTable";
   
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
            this.dgdClientAgent.ClientSelectedIndex = -1;
            this.dgdClientAgent.AgentSort("AgentNumber",SortDirection.Ascending);
            OnAllAgentsChecked(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void Page_LoadComplete(object sender,EventArgs e) {
        if(!Page.IsPostBack) {
            OnAllAgentsChecked(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
        //Event handler for change in from/to date
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAllAgentsChecked(object sender,EventArgs e) {
        //Event handler for change in from date
        this.dgdClientAgent.AgentSelectedIndex = this.chkAllAgents.Checked ? -1 : 0;
        this.dgdClientAgent.AgentsEnabled = !this.chkAllAgents.Checked;
        OnAgentSelected(null,EventArgs.Empty);
    }
    protected void OnClientSelected(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAgentSelected(object sender,EventArgs e) {
        //Event handler for change in selected vendor
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = this.dgdClientAgent.ClientSelectedRow != null && (this.chkAllAgents.Checked || this.dgdClientAgent.AgentSelectedRow != null);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string _from = this.ddpDelivery.FromDate.ToString("yyyy-MM-dd");
            string _to = this.ddpDelivery.ToDate.ToString("yyyy-MM-dd");
            string _client = this.dgdClientAgent.ClientNumber;
            string _agent = this.chkAllAgents.Checked ? null : this.dgdClientAgent.AgentNumber;
            string _pctontome = this.txtPctOnTime.Text;

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _from,_to,_client,_agent,_pctontome });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter from = new ReportParameter("FromDate",_from);
                    ReportParameter to = new ReportParameter("ToDate",_to);
                    ReportParameter client = new ReportParameter("ClientNumber",_client + "-" + this.dgdClientAgent.ClientName);
                    ReportParameter agent = new ReportParameter("AgentNumber",_agent);
                    ReportParameter pctontome = new ReportParameter("PctOnTime",_pctontome);
                    report.SetParameters(new ReportParameter[] { from,to,client,agent,pctontome });

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
                    Response.AddHeader("Content-Disposition","inline; filename=OnTimeCartonByDeliverySummary.xls");
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
