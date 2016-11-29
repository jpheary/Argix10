using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class ShipScheduleOFDs :System.Web.UI.Page {
    //Members
    private string mTitle = "Ship Schedule OFDs";
    private string mSource = "/Terminals/Ship Schedule OFDs";
    private string mDSName = "Dataset1";
    private string mUSPName = "uspShipScdeOFDView2",mTBLName = "NewTable";
   
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            this.txtOFDStart.Text = DateTime.Today.ToString("MM/dd/yyyy");
            this.txtOFDEnd.Text = DateTime.Today.ToString("MM/dd/yyyy");
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
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
            string _ofdStart = DateTime.Parse(this.txtOFDStart.Text).ToString("yyyy-MM-dd");
            string _ofdEnd = DateTime.Parse(this.txtOFDEnd.Text).ToString("yyyy-MM-dd");

            //Initialize control values
            LocalReport report = Master.Viewer.LocalReport;
            report.DisplayName = this.mTitle;
            report.EnableExternalImages = true;
            DataSet ds = fillDataset(this.mUSPName, mTBLName, new object[] { _ofdStart, _ofdEnd });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);
            switch(e.CommandName) {
                case "Run":
                    //Set local report and data source
                    System.IO.Stream stream = Master.GetReportDefinition(this.mSource);
                    report.LoadReportDefinition(stream);
                    report.DataSources.Clear();
                    report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

                    //Set the report parameters for the report
                    ReportParameter ofdStart = new ReportParameter("OFDDateStart", _ofdStart);
                    ReportParameter ofdEnd = new ReportParameter("OFDDateEnd", _ofdEnd);
                    report.SetParameters(new ReportParameter[] { ofdStart, ofdEnd });

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
                    Response.AddHeader("Content-Disposition","inline; filename=ShipScheduleOFDs.xls");
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
    public DataSet fillDataset(string sp, string table, object[] o) {
        //
        DataSet ds = new DataSet();
        Database db = DatabaseFactory.CreateDatabase("Enterprise");
        DbCommand cmd = db.GetStoredProcCommand(sp, o);
        cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);
        db.LoadDataSet(cmd, ds, table);
        return ds;
    }

}
