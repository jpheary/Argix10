using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Diagnostics;
using System.Xml;
using Argix.Enterprise;

public partial class _TrackByStore:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Init controls
                this.txtFromDate.Text = DateTime.Today.AddDays(-7).ToString("MM/dd/yyyy");
                this.txtToDate.Text = DateTime.Today.ToString("MM/dd/yyyy");
                this.cboClient.DataBind();

                MembershipServices membership = new MembershipServices();
                this.chkSubSearch.Checked = (membership.MemberProfile.StoreSearchType == "Sub");
                this.chkSubSearch.Visible = (membership.IsAdmin || membership.IsArgix);

                ProfileCommon profile = new MembershipServices().MemberProfile;
                this.txtStore.Text = profile.StoreNumber.Trim().Length > 0 ? profile.StoreNumber : "";
                this.txtStore.Enabled = profile.StoreNumber.Trim().Length == 0;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //Event handler for user requested to track one or more cartons
        try {
            switch(e.CommandName) {
                case "Track":
                    if(Page.IsValid) {
                        //Track by store or substore depending upon user profile specification
                        MembershipServices membership = new MembershipServices();
                        ProfileCommon profile = membership.MemberProfile;
                        if(profile.StoreSearchType == "Sub" || this.chkSubSearch.Checked) {
                            //Get list of Argix store selections for the requested substore
                            StoreDataset stores = getArgixStores(this.txtStore.Text);
                            if(stores.StoreTable.Rows.Count == 0) {
                                //No stores; notify store not found
                                Master.ShowMessageBox("Could not find any stores for sub-store " + this.txtStore.Text + ".");
                            }
                            else if(stores.StoreTable.Rows.Count == 1) {
                                //Single substore; process without prompting the user
                                track(stores.StoreTable[0].NUMBER.ToString());
                            }
                            else {
                                //Ambiguous substore selections; prompt user to select the desired substore
                                this.lstStores.DataSource = stores;
                                this.lstStores.DataBind();
                                this.mvMain.SetActiveView(this.vwSelectStore);
                            }
                        }
                        else 
                            track(this.txtStore.Text);
                    }
                    break;
                case "Continue":
                    track(this.lstStores.SelectedValue);
                    break;
                case "Cancel":
                    this.mvMain.SetActiveView(this.vwSearchStore);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    #region Data Services: getArgixStores(), track(), buildSummary()
    private StoreDataset getArgixStores(string subStoreNumber) {
        //Get a list of Argix-numbered stores for the specified sub-store number
        MembershipServices membership = new MembershipServices();
        ProfileCommon profile = membership.MemberProfile;
        string vendorNumber = (profile.Type.ToLower() == "vendor") ? profile.ClientVendorID : null;
        string clientNumber = this.cboClient.SelectedValue;

        StoreDataset stores = new TrackingGateway().GetStoresForSubStore(subStoreNumber,clientNumber,vendorNumber);
        return stores;
    }
    private void track(string storeNumber) {
        //
        //Flag search by method
        Session["TrackBy"] = "Store";

        //Track
        bool byPickup = this.cboSearchType.SelectedValue == "Pickup";
        ProfileCommon profile = new MembershipServices().MemberProfile;
        string vendorNumber = (profile.Type.ToLower() == "vendor") ? profile.ClientVendorID : null;
        string clientNumber = this.cboClient.SelectedValue;
        TrackingStoreItems items = new TrackingGateway().TrackCartonsByStoreSummary(clientNumber, storeNumber, DateTime.Parse(this.txtFromDate.Text), DateTime.Parse(this.txtToDate.Text), vendorNumber, byPickup);
        Session["StoreSummary"] = items;
        items = new TrackingGateway().TrackCartonsByStoreDetail(clientNumber, storeNumber, DateTime.Parse(this.txtFromDate.Text), DateTime.Parse(this.txtToDate.Text), vendorNumber, byPickup, null);
        Session["StoreDetail"] = items;
        if (items != null && items.Count > 0) {
            Response.Redirect("StoreSummary.aspx",false);
        }
        else
            Master.ShowMessageBox("No records found. Please try again.");
    }
    #endregion
}
