using System;
using System.Data;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Argix;

public partial class DeliveryInfoByPickup:System.Web.UI.Page {
    //Members
    private const string REPORT_NAME = "Delivery Info By Pickup";
    private const string REPORT_SRC = "/Tracking/Delivery Info By Pickup";
    private const string REPORT_DS = "DataSet1";
    private const string USP_REPORT = "uspRptCartonsInfoForPickup",TBL_REPORT = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = REPORT_NAME + " Report";
                this.ddpPickups.FromDate = DateTime.Today.AddDays(-7);
                this.ddpPickups.ToDate = DateTime.Today;
                OnFromToDateChanged(null,EventArgs.Empty);
                this.grdPickups.Sort("VendorNumber",SortDirection.Ascending);
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
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        try {
            this.ddlVendor.Items.Clear();
            this.ddlVendor.Items.Add(new ListItem("All",""));
            this.ddlVendor.DataBind();
            OnVendorChanged(this.ddlVendor,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnVendorChanged(object sender,EventArgs e) {
        //Event handler for change in selected vendor
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
            foreach (GridViewRow row in this.grdPickups.Rows) {
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

            //Get parameters for the query
            string _clientName = this.ddlClient.SelectedItem.Text;
            string _pickupID = "",_termCode = "";

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = REPORT_NAME;
            report.EnableExternalImages = true;
            EnterpriseService enterprise = new EnterpriseService();
            DataSet ds = new DataSet(REPORT_DS);
            foreach (GridViewRow row in SelectedRows) {
                DataKey dataKey = (DataKey)this.grdPickups.DataKeys[row.RowIndex];
                _pickupID = dataKey["PickupID"].ToString();
                _termCode = dataKey["TerminalCode"].ToString();
                DataSet _ds = enterprise.FillDataset(USP_REPORT,TBL_REPORT,new object[] { _termCode,_pickupID });
                ds.Merge(_ds);
            }
            if (ds.Tables[TBL_REPORT].Rows.Count >= 0) {
                switch (e.CommandName) {
                    case "Run":
                        //Set local report and data source
                        System.IO.Stream stream = Master.GetReportDefinition(REPORT_SRC);
                        report.LoadReportDefinition(stream);
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));

                        //Set the report parameters for the report
                        ReportParameter puID = new ReportParameter("PickupID","0");
                        ReportParameter clientName = new ReportParameter("ClientName",_clientName);
                        ReportParameter termCode = new ReportParameter("TerminalCode","0");
                        report.SetParameters(new ReportParameter[] { puID,clientName,termCode });

                        //Update report rendering with new data
                        report.Refresh();

                        if (!Master.Viewer.Enabled) Master.Viewer.CurrentPage = 1;
                        break;
                    case "Data":
                        //Set local export report and data source
                        report.LoadReportDefinition(Master.CreateExportRdl(ds,REPORT_DS,"RGXVMSQLR.TSORT"));
                        report.DataSources.Clear();
                        report.DataSources.Add(new ReportDataSource(REPORT_DS,ds.Tables[TBL_REPORT]));
                        report.Refresh(); break;
                    case "Excel":
                        //Create Excel mime-type page
                        Response.ClearHeaders();
                        Response.Clear();
                        Response.Charset = "";
                        Response.AddHeader("Content-Disposition","inline; filename=DeliveryInfoByPickup.xls");
                        Response.ContentType = "application/vnd.ms-excel";  //application/octet-stream";
                        System.IO.StringWriter sw = new System.IO.StringWriter();
                        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(sw);
                        DataGrid dg = new DataGrid();
                        dg.DataSource = ds.Tables[TBL_REPORT];
                        dg.DataBind();
                        dg.RenderControl(hw);
                        Response.Write(sw.ToString());
                        Response.End();
                        break;
                }
            }
            else {
                Master.Status = "There were no records found.";
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
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
