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

public partial class LOccitaneCartonInfo :System.Web.UI.Page {
    //Members
    private string mTitle = "LOccitane Carton Info";
    private string mSource = "/Tracking/LOccitane Carton Info";
    private string mDSName = "DataSet1";
    private string mUSPName = "uspRptCartonsInfoForPickup2",mTBLName = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = this.mTitle + " Report";
                this.ddpSetup.FromDate = DateTime.Today.AddDays(-7);
                this.ddpSetup.ToDate = DateTime.Today;
                OnFromToDateChanged(null,EventArgs.Empty);
                OnValidateForm(null,EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnFromToDateChanged(object sender,EventArgs e) {
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
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            report.EnableHyperlinks = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = new DataSet(this.mDSName);
            foreach (GridViewRow row in SelectedRows) {
                DataKey dataKey = (DataKey)this.grdPickups.DataKeys[row.RowIndex];
                DataSet _ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { dataKey["PickupID"].ToString() });
                ds.Merge(_ds);
            }
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));
                    report.SetParameters(new ReportParameter[] { new ReportParameter("PickupID","0") });
                    report.Refresh();
                    if(!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                    break;
                case "Data":
                    //Set local export report and data source
                    report.LoadReportDefinition(Master.CreateExportRdl(ds, this.mDSName, "RGXVMSQLR.TSORT"));
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));
                    report.Refresh(); 
                    break;
                case "Excel":
                    //Create Excel mime-type page
                    Response.ClearHeaders();
                    Response.Clear();
                    Response.Charset = "";
                    Response.AddHeader("Content-Disposition","inline; filename=LOccitaneCartonInfo.xls");
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
