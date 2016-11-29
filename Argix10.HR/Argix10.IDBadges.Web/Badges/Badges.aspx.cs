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

public partial class _Badges:System.Web.UI.Page {
    //Members
    public int mView = 0;
    private string mType = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Starting tab?
                this.mType = Request.QueryString["type"] != null ? Request.QueryString["type"] : "Employees";
                switch(this.mType.ToLower()) {
                    case "drivers": this.mView = 0; break;
                    case "employees": this.mView = 1; break;
                    case "vendors": this.mView = 2; break;
                }
                ViewState.Add("BadgeType", this.mType);
            }
            else {
                this.mType = ViewState["BadgeType"].ToString();
            }
            OnValidateForm(null,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnFindDriver(object sender, ImageClickEventArgs e) { OnSearchDrivers(sender, EventArgs.Empty); }
    protected void OnSearchDrivers(object sender, EventArgs e) {
        //Event handler for search
        findRow(this.grdDriverBadges, 3, this.txtFindDriver.Text);
        ScriptManager.RegisterStartupScript(this.txtFindDriver, typeof(TextBox), "ScrollDrivers", "scroll('" + this.grdDriverBadges.ClientID + "', 'idDrivers', '" + this.txtFindDriver.Text + "');", true);
    }
    protected void OnFindEmployee(object sender, ImageClickEventArgs e) { OnSearchEmployees(sender, EventArgs.Empty); }
    protected void OnSearchEmployees(object sender, EventArgs e) {
        //Event handler for search
        findRow(this.grdEmployeeBadges, 3, this.txtFindEmployee.Text);
        ScriptManager.RegisterStartupScript(this.txtFindEmployee, typeof(TextBox), "ScrollEmployees", "scroll('" + this.grdEmployeeBadges.ClientID + "', 'idEmployees', '" + this.txtFindEmployee.Text + "');", true);
    }
    protected void OnFindVendor(object sender, ImageClickEventArgs e) { OnSearchVendors(sender, EventArgs.Empty); }
    protected void OnSearchVendors(object sender, EventArgs e) {
        //Event handler for search
        findRow(this.grdVendorBadges, 3, this.txtFindVendor.Text);
        ScriptManager.RegisterStartupScript(this.txtFindVendor, typeof(TextBox), "ScrollVendors", "scroll('" + this.grdVendorBadges.ClientID + "', 'idVendors', '" + this.txtFindVendor.Text + "');", true);
    }
    protected void OnBadgeSelected(object sender, EventArgs e) {
        //Event handler for change in selected employee
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnEmployeeNew.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator"));
            this.btnEmployeeUpdate.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator")) && this.grdEmployeeBadges.SelectedRow != null;
            this.btnVendorNew.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator"));
            this.btnVendorUpdate.Enabled = (Roles.IsUserInRole("HR Assistant") || Roles.IsUserInRole("HR Manager") || Roles.IsUserInRole("Administrator")) && this.grdVendorBadges.SelectedRow != null;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnManageCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "EmployeeNew":
                    Response.Redirect("~/Badges/EmployeeBadge.aspx?id=0",false);
                    break;
                case "EmployeeUpdate":
                    Response.Redirect("~/Badges/EmployeeBadge.aspx?id=" + this.grdEmployeeBadges.SelectedDataKey.Value.ToString(), false);
                    break;
                case "VendorNew":
                    Response.Redirect("~/Badges/VendorBadge.aspx?id=0", false);
                    break;
                case "VendorUpdate":
                    Response.Redirect("~/Badges/VendorBadge.aspx?id=" + this.grdVendorBadges.SelectedDataKey.Value.ToString(), false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    #region Local Services: findRow()
    private void findRow(GridView grid, int colIndex, string searchWord) {
        //Event handler for change in search text value
        GridViewRow row = null, rowSimiliar = null, rowMatch = null;
        string cellText = "";
        long cellValue = 0, searchValue = 0;
        int length = 0, iL = 0, count = 0, i = 0, j = 0;
        bool isASC = true, isNumeric = false, isHigher = false;

        //Validate
        if(grid.Rows.Count == 0) return;

        //Get specifics for search word and grid
        length = searchWord.Length;
        count = grid.Rows.Count;
        if(colIndex < grid.Columns.Count && searchWord.Length > 0) {
            //Initial search conditions
            isASC = (grid.SortDirection == SortDirection.Ascending);
            isNumeric = (grid.Columns[colIndex].GetType() == Type.GetType("System.Int32"));
            i = 0;
            while(i < count) {
                //Get next row, cell value, and cell length
                row = grid.Rows[i];
                if(isNumeric) {
                    cellValue = Convert.ToInt64(row.Cells[colIndex].Text);
                    try { searchValue = Convert.ToInt64(searchWord); }
                    catch(FormatException) { searchValue = 0; }
                    if(isASC) {
                        if(searchValue == cellValue) rowMatch = row; else if(searchValue > cellValue) rowSimiliar = row;
                    }
                    else {
                        if(searchValue == cellValue) rowMatch = row; else if(searchValue < cellValue) rowSimiliar = row;
                    }
                }
                else {
                    cellText = row.Cells[colIndex].Text;
                    iL = cellText.Length;
                    if(iL > 0) {
                        //Compare a substring of the cell text with the search word
                        for(j = 1; j <= iL; j++) {
                            if(cellText.Substring(0, j).ToUpper() == searchWord.Substring(0, j).ToUpper()) {
                                //Look for exact match or closest match
                                if(j == length) {
                                    rowMatch = row; break;
                                }
                                else {
                                    if(j == iL) { rowSimiliar = row; break; }
                                }
                            }
                            else {
                                //Is search word alphabetically higher than cell?
                                if(isASC)
                                    isHigher = (searchWord.ToUpper().ToCharArray()[j - 1] > cellText.ToUpper().ToCharArray()[j - 1]);
                                else
                                    isHigher = (searchWord.ToUpper().ToCharArray()[j - 1] < cellText.ToUpper().ToCharArray()[j - 1]);
                                if(isHigher)
                                    rowSimiliar = row;
                                break;
                            }
                        }
                    }
                }
                if(rowMatch != null) break;
                i++;
            }
            //Select match or closest row
            if(count > 0 && rowMatch == null)
                rowMatch = (rowSimiliar != null) ? rowSimiliar : grid.Rows[0];
            grid.SelectedIndex = rowMatch.RowIndex;
            OnBadgeSelected(grid, EventArgs.Empty);
        }
    }
    #endregion
}
