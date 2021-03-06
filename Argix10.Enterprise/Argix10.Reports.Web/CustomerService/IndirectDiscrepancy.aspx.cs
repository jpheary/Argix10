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

public partial class IndirectDiscrepancy :System.Web.UI.Page {
    //Members
    private static int SpinMax = 14,SpinMin = 1,SpinDefault = 7;
  
    private string mTitle = "Indirect Discrepancy";
    private string mSource = "/Customer Service/Indirect Discrepancy";
    private const string REPORT_SRC_OVERSHORT = "/Customer Service/Indirect Discrepancy OverShort";
    private string mDSName = "IndirectDiscrepancyDS";
    private string mUSPName = "uspRptIndirectScanGetListForTrip",mTBLName = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            this.cboTerminal.DataBind();
            if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            this.chkOverShort.Checked = true;
            this.txtTripDaysBack.Text = SpinDefault.ToString();
            OnTripRangeChanged(null,EventArgs.Empty);
            OnValidateForm(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
    }
    protected void OnTerminalSelected(object sender,EventArgs e) {
        //Event handler for change in the selected terminal
        this.grdTrips.DataBind();
        if(this.grdTrips.Rows.Count > 0) this.grdTrips.SelectedIndex = 0;
        OnValidateForm(this,EventArgs.Empty);
    }
    protected void OnTripRangeChanged(object sender,EventArgs e) {
        //Event handler for change in trip range
        //Validate
        if(int.Parse(this.txtTripDaysBack.Text) < SpinMin)
            this.txtTripDaysBack.Text = SpinMin.ToString();
        else if(int.Parse(this.txtTripDaysBack.Text) > SpinMax)
            this.txtTripDaysBack.Text = SpinMax.ToString();
        OnValidateForm(this,EventArgs.Empty);
    }
    protected void OnTripSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = (this.grdTrips.Rows.Count > 0 && this.grdTrips.SelectedRow != null);
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for view button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string _terminal = this.cboTerminal.SelectedValue;
            string _terminalName = this.cboTerminal.SelectedItem.Text;
            string _tripNumber = this.grdTrips.SelectedRow.Cells[1].Text.Trim();

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = new DataSet(this.mDSName);
            DataSet _ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _terminal,_tripNumber });
            if(_ds.Tables[mTBLName].Rows.Count >= 0) {
                //Filter for over/short if applicable
                if(this.chkOverShort.Checked) {
                    ds = _ds.Clone();
                    ds.Merge(_ds.Tables[mTBLName].Select("Match = 0"));
                }
                else
                    ds.Merge(_ds);
            }
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.chkOverShort.Checked ? REPORT_SRC_OVERSHORT : this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter terminal = new ReportParameter("TerminalID",_terminal);
                    ReportParameter terminalName = new ReportParameter("TerminalName",_terminalName);
                    ReportParameter tripNumber = new ReportParameter("TripNumber",_tripNumber);
                    report.SetParameters(new ReportParameter[] { terminal,tripNumber,terminalName });

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
                    Response.AddHeader("Content-Disposition","inline; filename=IndirectDiscrepancy.xls");
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
