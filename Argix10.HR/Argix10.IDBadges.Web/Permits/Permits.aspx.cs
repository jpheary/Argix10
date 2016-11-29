using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.HR.Permits;

public partial class _Permits:System.Web.UI.Page {
    //Members
    public int mView = 0;
    private string mTabName = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Starting tab?
                this.mTabName = Request.QueryString["type"] != null ? Request.QueryString["tab"] : "Permits";
                switch(this.mTabName.ToLower()) {
                    case "permits": this.mView = 0; break;
                    case "search": this.mView = 1; break;
                    case "history": this.mView = 2; break;
                }
                ViewState.Add("TabName", this.mTabName);
            }
            else {
                this.mTabName = ViewState["TabName"].ToString();
            }
            OnValidateForm(null,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnPermitSelected(object sender, EventArgs e) {
        //Event handler for change in selected employee
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnSearchByChanged(object sender, EventArgs e) {
        //Event handler for change in selected search type
        switch(this.dduSearchBy.Text) {
            case "Permit#": 
                this.mvSearch.ActiveViewIndex = 0; 
                break;
            case "License Plate#": 
                this.mvSearch.ActiveViewIndex = 1; 
                this.dduStates.SelectedValue = "NJ";
                break;
            case "Vehicle Description": 
                this.mvSearch.ActiveViewIndex = 2; 
                break;
        }
        this.grdSearch.DataSource = null;
        this.grdSearch.DataBind();
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            DateTime inactivated = this.grdPermits.SelectedDataKey != null && DateTime.TryParse(this.grdPermits.SelectedDataKey.Values["Inactivated"].ToString(), out inactivated) ? inactivated : DateTime.MinValue;
            
            this.btnRegister.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator"));
            this.btnReplace.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator")) && this.grdPermits.SelectedValue != null && inactivated.CompareTo(DateTime.MinValue) == 0;
            this.btnRevoke.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator")) && this.grdPermits.SelectedValue != null && inactivated.CompareTo(DateTime.MinValue) == 0;
            this.btnChangeVehicle.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator")) && this.grdPermits.SelectedValue != null && inactivated.CompareTo(DateTime.MinValue) == 0;
            this.btnSearch.Enabled = true;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnManageCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Register":
                    Response.Redirect("~/Permits/RegisterVehicle.aspx?id=0", false);
                    break;
                case "Replace":
                    Response.Redirect("~/Permits/ReplacePermit.aspx?id=" + this.grdPermits.SelectedDataKey.Value.ToString(), false);
                    break;
                case "Revoke":
                    Response.Redirect("~/Permits/RevokePermit.aspx?id=" + this.grdPermits.SelectedDataKey.Value.ToString(), false);
                    break;
                case "ChangeVehicle":
                    Response.Redirect("~/Permits/ChangeVehicle.aspx?id=" + this.grdPermits.SelectedDataKey.Value.ToString(), false);
                   break;
                case "Search":
                    DataSet ds = null;
                    this.grdSearch.DataSource = null;
                    switch(this.dduSearchBy.Text) {
                        case "Permit#":
                            ds = new PermitGateway().FindPermitsByNumber(this.txtPermitNumber.Text);
                            break;
                        case "License Plate#":
                            ds = new PermitGateway().FindPermitsByPlate(this.dduStates.SelectedValue, this.txtPlate.Text);
                            break;
                        case "Vehicle Description":
                            ds = new PermitGateway().FindPermitsByVehicle(this.txtYear.Text, this.txtMake.Text, this.txtModel.Text, this.txtColor.Text);
                            break;
                    }
                    this.grdSearch.DataSource = ds;
                    this.grdSearch.DataBind();
                   break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
