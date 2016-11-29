using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Argix.Customers;

public partial class IssueNew:System.Web.UI.Page {
    //Members
    private const string SCOPE_NONE = "", SCOPE_AGENTS = "Agents", SCOPE_STORES = "Stores", SCOPE_SUBSTORES = "Substores";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                ViewState.Add("AgentNumber",Request.QueryString["agentNumber"]);

                this.cboCompany.DataBind();
                if (this.cboCompany.Items.Count > 0) this.cboCompany.SelectedIndex = 0;
                OnCompanyChanged(this.cboCompany,EventArgs.Empty);
            }
            
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnCompanyChanged(object sender,EventArgs e) {
        //Event handler for change in selected company
        try {
            //Validate
            if(this.cboCompany.SelectedItem == null) return;

            //Set applicable scopes for companies (i.e. client, pre-paid vendor)
            string number = this.cboCompany.SelectedValue;
            CompanyDataset companies = new CustomersGateway().GetCompanies();
            CompanyDataset.CompanyTableRow[] rows = (CompanyDataset.CompanyTableRow[])companies.CompanyTable.Select("Number='" + number + "'");
            this.cboScope.Items.Clear();
            this.cboScope.Items.AddRange(new ListItem[] { new ListItem(SCOPE_AGENTS) });
            this.cboScope.SelectedValue = SCOPE_AGENTS;
            if (rows.Length > 0 && rows[0].CompanyType == "20") {
                this.cboScope.Items.AddRange(new ListItem[] { new ListItem(SCOPE_STORES), new ListItem(SCOPE_SUBSTORES) });
                this.cboScope.SelectedValue = SCOPE_STORES;
            }

            //Update locations since company changed
            this.cboScope.Enabled = true;
            OnScopeChanged(null,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnScopeChanged(object sender,EventArgs e) {
        //Event handler for change in selected scope
        try {
            //Validate
            if(this.cboScope.SelectedItem == null) return;

            //Prepare a location selector for the specified scope
            this.txtStoreDetail.Text = "";
            switch(this.cboScope.SelectedItem.ToString()) {
                case SCOPE_STORES:
                case SCOPE_SUBSTORES:
                    this.mvLocation.SetActiveView(this.vwStore);
                    this.txtStore.Text = "";
                    break;
                case SCOPE_AGENTS:
                    this.mvLocation.SetActiveView(this.vwOther);
                    this.cboLocation.DataBind();
                    this.cboLocation.Enabled = this.cboLocation.Items.Count > 0;
                    if(this.cboLocation.Items.Count > 0) this.cboLocation.SelectedIndex = 0;
                    break;
            }
            OnLocationChanged(null,EventArgs.Empty);
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnLocationChanged(object sender,EventArgs e) {
        //Event handler for change in selected location
        try {
            switch(this.cboScope.SelectedItem.ToString()) {
                case SCOPE_AGENTS:
                    break;
                case SCOPE_STORES:
                case SCOPE_SUBSTORES:
                    this.txtStoreDetail.Text = ""; ;
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnStoreChanged(object sender,EventArgs e) {
        //Event handler for change in store
        try {
            if(this.txtStore.Text.Length > 0) {
                switch(this.cboScope.SelectedItem.ToString()) {
                    case SCOPE_AGENTS:
                        break;
                    case SCOPE_STORES:
                    case SCOPE_SUBSTORES:
                        showStoreDetail();
                        break;
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnCommandClick(object sender,CommandEventArgs e) {
        //Event handler for command button clicked
        try {
            switch(e.CommandName) {
                case "Cancel":  
                    Response.Redirect("~/Default.aspx"); 
                    break;
                case "OK":
                    Issue issue = new Issue();
                    if (this.cboCompany.SelectedValue.Length > 0) {
                        CompanyDataset companies = new CustomersGateway().GetCompanies();
                        CompanyDataset.CompanyTableRow[] rows = (CompanyDataset.CompanyTableRow[])companies.CompanyTable.Select("Number='" + this.cboCompany.SelectedValue + "'");
                        issue.CompanyID = rows[0].CompanyID;
                    }
                    switch(this.cboScope.SelectedValue) {
                        case SCOPE_AGENTS: issue.AgentNumber = (this.cboLocation.SelectedValue!="All" ? this.cboLocation.SelectedValue : ""); break;
                        case SCOPE_STORES: issue.StoreNumber = Convert.ToInt32(this.txtStore.Text); break;
                        case SCOPE_SUBSTORES: issue.StoreNumber = Convert.ToInt32(this.txtStore.Text); break;
                    }
                    issue.Contact = this.txtContact.Text;
                    issue.TypeID = Convert.ToInt32(this.cboIssueType.SelectedValue);
                    issue.Subject = this.txtSubject.Text;
                    issue.FirstActionUserID = HttpContext.Current.User.Identity.Name;

                    Argix.Customers.Action action = new Argix.Customers.Action();
                    action.TypeID = Convert.ToByte(this.cboActionType.SelectedValue);
                    action.IssueID = issue.ID;
                    action.UserID = HttpContext.Current.User.Identity.Name;
                    action.Comment = this.txtComments.Text;
                    action.Created = DateTime.Now;
                    issue.Actions = new Actions();
                    issue.Actions.Add(action);

                    long id = new CustomersGateway().CreateIssue(issue);
                    Response.Redirect("~/Default.aspx?issueID=" + id.ToString());
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    #region Local Services: showStoreDetail(), getStoreDetailString(), getDeliveryDays(), getOFD()
    private void showStoreDetail() {
        //
        try {
            this.txtStoreDetail.Text = "";
            if (this.cboCompany.SelectedValue.Length > 0 && this.txtStore.Text.Length > 0) {
                StoreDataset ds = new StoreDataset();
                CompanyDataset companies = new CustomersGateway().GetCompanies();
                CompanyDataset.CompanyTableRow[] rows = (CompanyDataset.CompanyTableRow[])companies.CompanyTable.Select("Number='" + this.cboCompany.SelectedValue + "'");
                if (rows.Length > 0) {
                    int companyID = rows[0].CompanyID;
                    switch (this.cboScope.SelectedItem.ToString()) {
                        case SCOPE_STORES: ds.Merge(new CustomersGateway().GetStoreDetail(companyID,Convert.ToInt32(this.txtStore.Text))); break;
                        case SCOPE_SUBSTORES: ds.Merge(new CustomersGateway().GetStoreDetail(companyID,this.txtStore.Text)); break;
                    }
                    if (ds.StoreTable.Rows.Count > 0) this.txtStoreDetail.Text = getStoreDetailString(ds);
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    private string getStoreDetailString(StoreDataset storeDS) {
        //Return a string of store detail
        StringBuilder detail = new StringBuilder();
        try {
            if (storeDS.StoreTable.Rows.Count > 0) {
                StoreDataset.StoreTableRow store = storeDS.StoreTable[0];
                detail.AppendLine(store.StoreName.Trim() + " (store #" + store.StoreNumber.ToString() + "; substore #" + store.SubStoreNumber.Trim() + ")");
                detail.AppendLine((!store.IsStoreAddressline1Null() ? store.StoreAddressline1.Trim() : ""));
                detail.AppendLine((!store.IsStoreAddressline2Null() ? store.StoreAddressline2.Trim() : ""));
                detail.AppendLine((!store.IsStoreCityNull() ? store.StoreCity.Trim() : "") + ", " +
                                                    (!store.IsStoreStateNull() ? store.StoreState.Trim() : "") + " " +
                                                    (!store.IsStoreZipNull() ? store.StoreZip.Trim() : ""));
                detail.AppendLine((!store.IsContactNameNull() ? store.ContactName.Trim() : "") + ", " + (!store.IsPhoneNumberNull() ? store.PhoneNumber.Trim() : ""));
                detail.AppendLine((!store.IsRegionDescriptionNull() ? store.RegionDescription.Trim() : "") +
                                                    " (" + (!store.IsRegionNull() ? store.Region.Trim() : "") + "), " +
                                                    (!store.IsDistrictNameNull() ? store.DistrictName.Trim() : "") +
                                                    " (" + (!store.IsDistrictNull() ? store.District.Trim() : "") + ")");
                detail.AppendLine("Zone " + (!store.IsZoneNull() ? store.Zone.Trim() : "") + ", " +
                                                    "Agent " + (!store.IsAgentNumberNull() ? store.AgentNumber.Trim() : "") + " " +
                                                    (!store.IsAgentNameNull() ? store.AgentName.Trim() : ""));
                detail.AppendLine("Window " + (!store.IsWindowTimeStartNull() ? store.WindowTimeStart.ToString("HH:mm") : "") + " - " +
                                                    (!store.IsWindowTimeEndNull() ? store.WindowTimeEnd.ToString("HH:mm") : "") + ", " +
                                                    "Del Days " + getDeliveryDays(store) + ", " +
                                                    (!store.IsScanStatusDescrptionNull() ? store.ScanStatusDescrption.Trim() : ""));
                detail.AppendLine("JA Transit " + (!store.IsStandardTransitNull() ? store.StandardTransit.ToString() : "") + ", " + "OFD " + getOFD(store));
                detail.AppendLine("Special Inst: " + (!store.IsSpecialInstructionsNull() ? store.SpecialInstructions.Trim() : ""));
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return detail.ToString();
    }
    private string getDeliveryDays(StoreDataset.StoreTableRow store) {
        //Return delivery days from the dataset
        string ddays = "";
        try {
            //Check for overrides
            if (!store.IsIsDeliveryDayMondayNull()) ddays += (store.IsDeliveryDayMonday.Trim() == "Y" ? "M" : "");
            if (!store.IsIsDeliveryDayTuesdayNull()) ddays += (store.IsDeliveryDayTuesday.Trim() == "Y" ? "T" : "");
            if (!store.IsIsDeliveryDayWednesdayNull()) ddays += (store.IsDeliveryDayWednesday.Trim() == "Y" ? "W" : "");
            if (!store.IsIsDeliveryDayThursdayNull()) ddays += (store.IsDeliveryDayThursday.Trim() == "Y" ? "R" : "");
            if (!store.IsIsDeliveryDayFridayNull()) ddays += (store.IsDeliveryDayFriday.Trim() == "Y" ? "F" : "");

            //If no overrides, then all days are valid
            if (ddays.Trim().Length == 0) ddays = "MTWRF";
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return ddays;
    }
    private string getOFD(StoreDataset.StoreTableRow store) {
        //Return delivery days from the dataset
        string ofd = "";
        try {
            //OFD1, OFD2, or NONE
            if (!store.IsIsOutforDeliveryDay1Null())
                ofd += (store.IsOutforDeliveryDay1.Trim() == "Y" ? "DAY1" : "");
            else if (!store.IsIsOutforDeliveryDay2Null())
                ofd += (store.IsOutforDeliveryDay2.Trim() == "Y" ? "DAY2" : "");
            else
                ofd += "";
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return ofd;
    }
    #endregion
}
