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

public partial class IndirectSort :System.Web.UI.Page {
    //Members
    private static int SpinMax = 14, SpinMin = 1, SpinDefault = 7; 

    private string mTitle = "Client Indirect Sort";
    private string mSource = "/Operations/Indirect Sort";
    private string mDSName = "IndirectSortDS";
    private string mUSPName = "uspRptIndirectScanGetListForTrips",mTBLName = "NewTable";
    
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            //Initialize control values
            this.Title = Master.ReportTitle = this.mTitle + " Report";
            this.cboTerminal.DataBind();
            if(this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = 0;
            this.txtTripDaysBack.Text = SpinDefault.ToString();
            OnTripRangeChanged(null,EventArgs.Empty);
        }
        Master.ButtonCommand += new CommandEventHandler(OnButtonCommand);
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTerminalSelected(object sender,System.EventArgs e) {
        //Event hanlder for change in selected terminal
        if(this.grdTrips.Rows.Count > 0) this.grdTrips.SelectedIndex = 0;
        OnValidateForm(this,EventArgs.Empty);
    }
    protected void OnTripRangeChanged(object sender,EventArgs e) {
        //Event handler for change in trip range
        if(int.Parse(this.txtTripDaysBack.Text) < SpinMin)
            this.txtTripDaysBack.Text = SpinMin.ToString();
        else if(int.Parse(this.txtTripDaysBack.Text) > SpinMax)
            this.txtTripDaysBack.Text = SpinMax.ToString();
        OnValidateForm(this,EventArgs.Empty);
    }
    protected void OnAllTripsSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        CheckBox chkAll = (CheckBox)this.grdTrips.HeaderRow.FindControl("chkAll");
        foreach(GridViewRow row in this.grdTrips.Rows) {
            CheckBox chk = (CheckBox)row.FindControl("chkSelect");
            chk.Checked = chkAll.Checked;
        }
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnTripSelected(object sender,EventArgs e) {
        //Event handler for change in selected trip
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        Master.Validated = SelectedRows.Length > 0;
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for view button clicked
        try {
            //Clear existing data
            Master.Viewer.Reset();
            
            //Get parameters for the query
            string _terminal = this.cboTerminal.SelectedValue;
            string _terminalName = this.cboTerminal.SelectedItem.Text.Trim();
            string _trips = SelectedTrips;

            //Initialize control values
            EnterpriseGateway enterprise = new EnterpriseGateway();
            DataSet ds = enterprise.FillDataset(this.mUSPName,mTBLName,new object[] { _terminal,"44",_trips });
            if (ds.Tables[mTBLName] == null) ds.Tables.Add(mTBLName);

            //Set local report and data source
            LocalReport report = Master.Viewer.LocalReport;
            report.ReportPath = this.mSource;
            report.DataSources.Clear();
            report.DataSources.Add(new ReportDataSource(this.mDSName,ds.Tables[mTBLName]));

            //Set the report parameters for the report
            ReportParameter terminal = new ReportParameter("TerminalID",_terminal);
            ReportParameter terminalName = new ReportParameter("Terminal",_terminalName);
            ReportParameter tripNumber = new ReportParameter("TripNumber",_trips);
            report.SetParameters(new ReportParameter[] { terminal,tripNumber,terminalName });

            //Update report rendering with new data
            report.Refresh();
        }
        catch(Exception ex) { Master.ReportError(ex); }
    }
    private string SelectedTrips { 
		get { 
			string tripNumbers="";
            foreach(GridViewRow row in SelectedRows) {
                DataKey dataKey = (DataKey)this.grdTrips.DataKeys[row.RowIndex];
                tripNumbers = String.Concat(tripNumbers,", ",dataKey["BolNumber"].ToString().Trim());
            }
			tripNumbers = tripNumbers.Remove(0, 2);
			return tripNumbers;
		} 
	}
    private GridViewRow[] SelectedRows {
        get {
            GridViewRow[] rows=new GridViewRow[] { };
            int i=0;
            foreach(GridViewRow row in this.grdTrips.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j=0;
                foreach(GridViewRow row in this.grdTrips.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
}
