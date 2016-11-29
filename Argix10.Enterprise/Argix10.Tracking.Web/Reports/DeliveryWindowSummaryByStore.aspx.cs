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

public partial class DeliveryWindowSummaryByStore :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Delivery Window Summary By Store";
    private const string REPORT_DESC = "";
    private const string REPORT_SRC = "/Tracking/Delivery Window Summary By Store";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptDeliveryWindowSummaryByStoreWithEarly",TBL_REPORT = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = REPORT_NAME + " Report";
                Master.ReportDesc = REPORT_DESC;
                this.cboClient.DataBind();
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(this.cboClient, EventArgs.Empty);
                OnValidateForm(null, EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnClientChanged(object sender, EventArgs e) {
        //Event handler for change in selected client
        OnScopeParamChanged(this.cboParam, EventArgs.Empty);
    }
    protected void OnScopeParamChanged(object sender, EventArgs e) {
        //Event handler for change in selected filter parameter
        this.txtStore.Visible = this.cboParam.SelectedValue == "Stores";
        this.txtStore.Text = "";
        OnValidateForm(null, EventArgs.Empty);
    }
    protected void OnStoreChanged(object sender, EventArgs e) {
        //Event handler for change in store selection
        OnValidateForm(null, EventArgs.Empty);
    }
    protected void OnDateParamChanged(object sender, EventArgs e) {
        //Load a list of date range selections
        if(this.cboDateValue.Items.Count > 0) this.cboDateValue.SelectedIndex = 0;
        OnValidateForm(null, EventArgs.Empty);
    }
    protected void OnValidateForm(object sender, EventArgs e) {
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
            string _client = this.cboClient.SelectedValue != "" ? this.cboClient.SelectedValue : null;
            string _division = null;
            string _agent = null;
            string _region = null;
            string _district = null;
            string _store = this.cboParam.SelectedValue == "Stores" && this.txtStore.Text.Length > 0 ? this.txtStore.Text : null;
            string _start = this.cboDateValue.SelectedValue.Split(',')[1].Split(':')[0].Trim();
            string _end = this.cboDateValue.SelectedValue.Split(',')[1].Split(':')[1].Trim();

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _client,_division,_agent,_region,_district,_store,_start,_end });
            if(ds.Tables[TBL_REPORT].Rows.Count >= 0) {
                switch(e.CommandName) {
                    case "Run":
                        //Set local report and data source
                        System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                        report.LoadReportDefinition(stream);
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                        //Set the report parameters for the report
                        ReportParameter clientName = new ReportParameter("ClientName", this.cboClient.SelectedItem.Text);
                        ReportParameter client = new ReportParameter("ClientNumber",_client);
                        ReportParameter division = new ReportParameter("Division",_division);
                        ReportParameter agent = new ReportParameter("AgentNumber",_agent);
                        ReportParameter region = new ReportParameter("Region",_region);
                        ReportParameter district = new ReportParameter("District",_district);
                        ReportParameter store = new ReportParameter("StoreNumber",_store);
                        ReportParameter start = new ReportParameter("StartDate",_start);
                        ReportParameter end = new ReportParameter("EndDate",_end);
                        report.SetParameters(new ReportParameter[] { client,division,agent,region,district,store,start,end,clientName });

                        //Update report rendering with new data
                        report.Refresh();
                        if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                        break;
                }
            }
            else 
                Master.ShowMessageBox("There were no records found.");
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
