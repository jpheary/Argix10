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

public partial class ProjectedDeliverySchedule :System.Web.UI.Page {
    //Members
    private string mTitle = "Projected Delivery Schedule";
    private string mSource = "/Customer Service/Projected Delivery Schedule";
    private string mDSName = "ProjectedDeliveryScheduleDS";
    private string mUSPName = "uspRptShipSCDEProjectedDelivery",mTBLName = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            OnActiveOnlyChecked(this.chkActiveOnly,EventArgs.Empty);
            this.cboAgent.DataBind();
            if(this.cboAgent.Items.Count > 0) this.cboAgent.SelectedIndex = 0;
            OnAgentChanged(null,EventArgs.Empty);
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
        if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
        OnClientChanged(null,EventArgs.Empty);
    }
    protected void OnAgentChanged(object sender,EventArgs e) {
        //Event handler for change in filter value
        OnValidateForm(null,EventArgs.Empty);
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
            DateTime _from = this.ddpScheduleDate.FromDate;
            DateTime _to = this.ddpScheduleDate.ToDate;
            string _client = this.cboClient.SelectedValue == "" ? null : this.cboClient.SelectedValue;
            string _agent = (this.cboAgent.SelectedValue == "" ? null : this.cboAgent.SelectedValue);

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _from,_to,_client,_agent });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter from = new ReportParameter("FromDate",_from.ToString("yyyy-MM-dd"));
                    ReportParameter to = new ReportParameter("ToDate",_to.ToString("yyyy-MM-dd"));
                    ReportParameter client = new ReportParameter("ClientNumber"); if(_client != null) client.Values.Add(this.cboClient.SelectedValue + "- " + this.cboClient.SelectedItem.Text);
                    ReportParameter agent = new ReportParameter("AgentNumber"); if(_agent != null) agent.Values.Add(this.cboAgent.SelectedItem.Text);
                    report.SetParameters(new ReportParameter[] { from,to,client,agent });

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
                    Response.AddHeader("Content-Disposition","inline; filename=ProjectedDeliverySchedule.xls");
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
