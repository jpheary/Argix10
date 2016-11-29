using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Argix;
using Microsoft.Reporting.WebForms;

public partial class ExecutiveMaster:System.Web.UI.MasterPage {
    //Members
    public event EventHandler ClientChanged=null;

    //Interface
    public string ReportTitle { get { return Master.ReportTitle; } set { Master.ReportTitle = value; } }
    public ReportViewer Viewer { get { return Master.Viewer; } }
    public bool Validated { set { if(value) OnValidateForm(null,EventArgs.Empty); else Master.Validated = false; } }
    public string Status { set { Master.Status = value; } }
    public void ReportError(Exception ex,int logLevel) { Master.ReportError(ex,logLevel); }
    public Stream GetReportDefinition(string report) { return Master.GetReportDefinition(report); }
    public Stream CreateExportRdl(DataSet ds,string dataSetName,string dataSourceName) { return Master.CreateExportRdl(ds,dataSetName,dataSourceName); }

    public string ParamsSelectedValue { get { return this.cboParam.SelectedValue; } set { this.cboParam.SelectedValue  = value; OnScopeParamChanged(this.cboParam,EventArgs.Empty); } }
    public bool ParamsEnabled { get { return this.cboParam.Enabled; } set { this.cboParam.Enabled  = value; } }
    public string StartDate { get { return this.cboDateValue.SelectedValue.Split(',')[1].Split(':')[0].Trim(); } }
    public string EndDate { get { return this.cboDateValue.SelectedValue.Split(',')[1].Split(':')[1].Trim(); } }
    public string ClientNumber { get { if(this.cboClient.SelectedValue != "") return this.cboClient.SelectedValue; else return null; } }
    public string ClientName { get { return this.cboClient.SelectedItem.Text; } }
    public string Division { get { return null; } }
    public string AgentNumber { get { return null; } }
    public string SubAgentNumber { get { return null; } }
    public string Region { get { return null; } }
    public string District { get { return null; } }
    public string StoreNumber { get { if (this.cboParam.SelectedValue == "Stores" && this.txtStore.Text.Length > 0) return this.txtStore.Text; else return null; } }

    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.cboClient.DataBind();
            if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
            OnClientChanged(this.cboClient,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        OnScopeParamChanged(this.cboParam,EventArgs.Empty);
        OnValidateForm(null,EventArgs.Empty);
        if(this.ClientChanged != null) this.ClientChanged(this.cboClient,EventArgs.Empty);
    }
    protected void OnScopeParamChanged(object sender,EventArgs e) {
        //Event handler for change in selected filter parameter
        this.txtStore.Visible = this.cboParam.SelectedValue == "Stores";
        this.txtStore.Text = "";
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnStoreChanged(object sender,EventArgs e) {
        //Event handler for change in store selection
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnDateParamChanged(object sender,EventArgs e) {
        //Load a list of date range selections
        if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 0;
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = true;
    }
}
