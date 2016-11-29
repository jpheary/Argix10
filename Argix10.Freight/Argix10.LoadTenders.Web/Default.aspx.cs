using System;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Initialize control values
                this.txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                this.txtFromDate.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");

                this.cboClient.DataSource = Master.GetClients();
                this.cboClient.DataBind();
                if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                OnClientChanged(this.cboClient,EventArgs.Empty);
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnDateChanged(object sender,EventArgs e) {
        //
        try {
            OnClientChanged(this.cboClient,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnClientChanged(object sender,EventArgs e) {
        //Event handler for change in selected client
        try {
            this.grdTenders.DataBind();
            OnValidateForm(null,EventArgs.Empty);
            ScriptManager.RegisterStartupScript(this.upnlTenders,typeof(UpdatePanel),"ClearCursor","document.body.style.cursor='default';",true);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnTenderSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnAllTendersSelected(object sender,EventArgs e) {
        //Event handler for change in selected pickup
        try {
            CheckBox chkAll = (CheckBox)this.grdTenders.HeaderRow.FindControl("chkAll");
            foreach (GridViewRow row in this.grdTenders.Rows) {
                CheckBox chk = (CheckBox)row.FindControl("chkSelect");
                chk.Checked = chkAll.Checked;
            }
            OnValidateForm(null,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnGo.Enabled = this.grdTenders.Rows.Count > 0;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for toolbar button clicked
        try {
            switch (e.CommandName) {
                case "Refresh":
                    OnClientChanged(this.cboClient,EventArgs.Empty);
                    break;
                case "Submit":
                    Session["LoadTenders"] = null;
                    if(this.grdTenders.DataKeys.Count > 0) {
                        //Get parameters for the query
                        string client = this.cboClient.SelectedValue;
                        DateTime start = DateTime.Parse(this.txtFromDate.Text);
                        DateTime end = DateTime.Parse(this.txtToDate.Text);
                        LoadTenderDataset ds = new LoadTenderDataset();
                        LoadTenderDataset _ds = new Argix.TsortGateway().GetLoadTenders(client,start,end);
                        foreach(GridViewRow row in SelectedRows) {
                            DataKey dataKey = (DataKey)this.grdTenders.DataKeys[row.RowIndex];
                            string load = dataKey["Load"].ToString();
                            ds.Merge(_ds.LoadTenderTable.Select("Load='" + load + "'"));

                            LoadTenderDetailDataset detail = new Argix.TsortGateway().GetLoadTenderDetails(load);
                            if (detail.LoadTenderDetailTable.Rows.Count > 0) ds.Merge(detail);
                        }
                        Session["LoadTenders"] = ds;
                        Response.Redirect("LoadTender.aspx",false);
                    }
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }

    private GridViewRow[] SelectedRows {
        get {
            GridViewRow[] rows=new GridViewRow[] { };
            int i=0;
            foreach(GridViewRow row in this.grdTenders.Rows) {
                bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                if(isChecked) i++;
            }
            if(i > 0) {
                rows = new GridViewRow[i];
                int j=0;
                foreach(GridViewRow row in this.grdTenders.Rows) {
                    bool isChecked = ((CheckBox)row.FindControl("chkSelect")).Checked;
                    if(isChecked) rows[j++] = row;
                }
            }
            return rows;
        }
    }
}
