using System;
using System.Collections;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class LOccitaneWholesaleCartonInfo :System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "LOccitane Wholesale Carton Info";
    private const string REPORT_DESC = "";
    private const string REPORT_SRC = "/Tracking/LOccitaneWholesale Carton Info";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptCartonsInfoForPickup2", TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = REPORT_NAME + " Report";
                Master.ReportDesc = REPORT_DESC;
                this.txtStart.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                this.txtEnd.Text = DateTime.Today.ToString("MM/dd/yyyy");
                OnPickupDatesChanged(null, EventArgs.Empty);
                OnValidateForm(null,EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnPickupDatesChanged(object sender, EventArgs e) {
        //Event handler for chnage in from/to date
        try {
            this.grdPickups.DataBind();
            OnPickupSelected(this.grdPickups,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnAllPickupsSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        try {
            CheckBox chkAll = (CheckBox)this.grdPickups.HeaderRow.FindControl("chkAll");
            foreach(GridViewRow row in this.grdPickups.Rows) {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                chk.Checked = chkAll.Checked;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnPickupSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            Master.Validated = SelectedRows.Length > 0;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();
        
            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            report.EnableHyperlinks = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = new DataSet(REPORT_DS);
            foreach (GridViewRow row in SelectedRows) {
                DataKey dataKey = (DataKey)this.grdPickups.DataKeys[row.RowIndex];
                DataSet _ds = enterprise.FillDataset(USP_REPORT, TBL_REPORT, new object[] { dataKey["PickupID"].ToString() });
                ds.Merge(_ds);
            }
            if(ds.Tables[TBL_REPORT] == null) ds.Tables.Add(TBL_REPORT);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(REPORT_DS, ds.Tables[TBL_REPORT]));
                    report.SetParameters(new ReportParameter[] { new ReportParameter("PickupID","0") });
                    report.Refresh();
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 4); }
    }
    private GridViewRow[] SelectedRows {
        get {
            GridViewRow[] rows=new GridViewRow[] { };
            int i=0;
            foreach(GridViewRow row in this.grdPickups.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j=0;
                foreach(GridViewRow row in this.grdPickups.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
}
