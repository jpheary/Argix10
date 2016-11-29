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

public partial class GNCInvoice:System.Web.UI.Page {
    //Members
    private string mTitle = "GNC Invoice";
    private string mSource = "/Finance/GNC Invoice";
    private string mDSName = "DataSet1";
    private string mUSPName = "uspInvGNCInvoiceByReleaseDateGet", mTBLName = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Initialize control values
                this.Title = Master.ReportTitle = this.mTitle + " Report";
                this.grdInvoices.DataSource = GetClientInvoices("152", null, null);
                this.grdInvoices.DataBind();
                OnValidateForm(null,EventArgs.Empty);
            }
            Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnInvoiceSelected(object sender, EventArgs e) {
        //Event handler for change in selected vendor
        OnValidateForm(null, EventArgs.Empty);
    }
    protected void OnValidateForm(object sender, EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = selectedInvoices.Length > 0;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            //Change view to Viewer and reset to clear existing data
            Master.Viewer.Reset();

            //Get parameters for the query
            string _invoiceNumber = "";

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            DataSet ds = new DataSet();
            EnterpriseGateway enterprise = new EnterpriseGateway();
            foreach(GridViewRow row in selectedInvoices) {
                _invoiceNumber = row.Cells[1].Text;
                DataSet _ds = enterprise.FillDataset(this.mUSPName, this.mTBLName, new object[] { _invoiceNumber });
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

                    //Set the report parameters for the report
                    ReportParameter invoiceNumber = new ReportParameter("InvoiceNumber", _invoiceNumber);
                    report.SetParameters(new ReportParameter[] { invoiceNumber });

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
                    Response.AddHeader("Content-Disposition","inline; filename=BNConsumerDirectDDUPerformanceDetail.xls");
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

    private GridViewRow[] selectedInvoices {
        get {
            GridViewRow[] rows = new GridViewRow[] { };
            int i = 0;
            foreach(GridViewRow row in this.grdInvoices.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j = 0;
                foreach(GridViewRow row in this.grdInvoices.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
    public DataSet GetClientInvoices(string clientNumber, string clientDivision, string startDate) {
        //Get a list of clients invoices filtered for a specific division
        DataSet invoices = new DataSet();
        try {
            DataSet ds = new EnterpriseGateway().FillDataset("uspInvClientInvoiceGetListAllTypes", "ClientInvoiceTable", new object[] { clientNumber, clientDivision, startDate });
            if(ds != null && ds.Tables["ClientInvoiceTable"].Rows.Count > 0) invoices.Merge(ds);
        }
        catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        return invoices;
    }
}
