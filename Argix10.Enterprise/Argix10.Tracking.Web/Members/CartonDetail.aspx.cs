using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Argix.Enterprise;

public partial class _CartonDetail:System.Web.UI.Page {
    //Members
    private string mLabelNumber = "", mSearchByStoreTL = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Init
                this.lnkBack.Visible = this.lnkFileClaim.Visible = this.lnkPODReq.Visible = false;

                //Display tracking information for the specified carton
                this.mLabelNumber = Request.QueryString["item"] == null ? "" : Request.QueryString["item"].ToString();
                ViewState["LabelNumber"] = this.mLabelNumber;
                this.mSearchByStoreTL = Request.QueryString["TL"] == null ? "" : Request.QueryString["TL"].ToString();
                ViewState["SearchByStoreTL"] = this.mSearchByStoreTL;
                if (this.mLabelNumber.Length > 0) {
                    TrackingItems items = null;
                    if (Session["TrackData"] is TrackingItems) items = (TrackingItems)Session["TrackData"];
                    if (items != null && items.Count > 0) {
                        //Find the items info for labelNumber
                        foreach (TrackingItem item in items) {
                            if (item.LabelNumber == this.mLabelNumber) {
                                showItem(item);
                                break;
                            }
                        }
                    }
                    else
                        Master.ReportError(new ApplicationException("Could not find carton information. Please return to the tracking page and try again."), 4);
                }
                else
                    Master.ReportError(new ApplicationException("Could not find item number. Please return to the tracking page and try again."), 4);

                //Update links
                this.lnkBack.Text = Session["TrackBy"].ToString() == TrackingGateway.SEARCHBY_STORE ? "<<   Store Detail" : "<<   Carton Summary";
                this.lnkBack.PostBackUrl = Session["TrackBy"].ToString() == TrackingGateway.SEARCHBY_STORE ? "~/Members/StoreDetail.aspx?TL=" + HttpUtility.UrlEncode(this.mSearchByStoreTL) : "~/Members/CartonSummary.aspx";
                this.lnkBack.Visible = true;
                this.lnkFileClaim.NavigateUrl = "~/Members/FileClaim.aspx?ID=" + this.mLabelNumber;
                this.lnkFileClaim.ToolTip = "Submit a file claim";
                this.lnkFileClaim.Visible = new MembershipServices().IsFileClaims;
                this.lnkPODReq.Visible = true;
            }
            else {
                this.mLabelNumber = ViewState["LabelNumber"].ToString();
                this.mSearchByStoreTL = ViewState["SearchByStoreTL"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnPODRequest(object sender,EventArgs e) {
        //
        DataSet ds = null;
        bool imagesFound = false;
        try {
            if (this.lnkPODReq.Text == "Request POD") {
                sendPODRequest();
            }
            else {
                //Get all detail rows for this carton
                Argix.Enterprise.TrackingItem item = null;
                if (this.mLabelNumber.Length > 0) {
                    TrackingItems items = (TrackingItems)Session["TrackData"];
                    if (items != null) {
                        //Find the items info for labelNumber
                        foreach (TrackingItem _item in items) {
                            if (_item.LabelNumber == this.mLabelNumber) {
                                //Get carton data and check for a POD image for this CBOL
                                item = _item;
                                string cl = item.Client.Trim().PadLeft(3,'0');
                                //string div = "01";      Can't wildcard div in KQL
                                string st = item.StoreNumber.Trim().PadLeft(5,'0');
                                string cbol = item.CBOL.Trim();
                                if (cbol.Length > 0) {
                                    ds = new Argix.Enterprise.ImagingGateway().SearchSharePointImageStore(Application["ImagingDocClass"].ToString(), Application["ImagingPropertyName"].ToString(), cbol + cl + "*");
                                    imagesFound = (ds != null && ds.Tables[0] != null && ds.Tables[0].Rows.Count > 0 && ds.Tables[0].Select("Store=" + st).Length > 0);
                                }
                                break;
                            }
                        }
                    }
                }
                else
                    Master.ShowMessageBox("Could not find tracking information. Please return to tracking page and try again.");

                if (imagesFound) {
                    //Images available- open images into other browser instances
                    StringBuilder sb = new StringBuilder();
                    for (int i = 0;i < ds.Tables[0].Rows.Count;i++) {
                        if(ds.Tables[0].Rows[i]["Store"].ToString() == item.StoreNumber.Trim().PadLeft(5, '0')) {
                            string uri = ds.Tables[0].Rows[i]["Path"].ToString();
                            sb.Append("window.open('PODImage.aspx?uri=" + uri + "', '_blank', 'width=480,height=576,menubar=yes,location=no,toolbar=no,status=yes,resizable=yes');");
                        }
                    }
                    Page.ClientScript.RegisterStartupScript(typeof(Page),"POD Image",sb.ToString(),true);
                }
                else {
                    //Image(s) unavailable
                    ViewState.Add("PODRequestItem",item);
                    this.lnkPODReq.Text = "Request POD";
                    this.lnkPODReq.ToolTip = "Request POD image(s) from Customer Service";
                    Master.ShowMessageBox("The POD was not be found. Click the Request POD link to request the POD from Customer Service.");
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    #region Local Services: showItem(), sendPODRequest()
    private void showItem(TrackingItem item) {
        //Display summary
        this.lblCartonNumber.Text = item.CartonNumber;
        this.lblClientName.Text = item.ClientName;
        //this.lblStoreNumber.Text = "Store# " + item.StoreNumber.PadLeft(5,'0');
        this.lblStore.Text = item.StoreName + ", " + item.StoreAddress1 + (item.StoreAddress2.Length > 0 ? ", " + item.StoreAddress2 + ", " : " ") + item.StoreCity + ", " + item.StoreState + " " + item.StoreZip;
        this.lblVendorName.Text = item.VendorName.Trim();
        this.lblPickupDate.Text = item.PickupDate.Trim();
        this.lblBOLNumber.Text = item.BOLNumber.ToString();
        this.lblTLNumber.Text = item.TLNumber.Trim();
        this.lblLabelNumber.Text = item.LabelNumber.ToString();
        this.lblPONumber.Text = item.PONumber.Trim();
        this.lblWeight.Text = item.Weight.ToString();
        this.lblShipmentNumber.Text = item.ShipmentNumber.Trim();
        this.lblSchDelivery.Text = item.ActualStoreDeliveryDate.Trim();

        //Display detail
        Argix.Enterprise.TrackingItems detail = new Argix.Enterprise.TrackingItems();
        Argix.Enterprise.TrackingItem row = null;
        if (item.SortFacilityArrivalDate.Trim().Length > 0) {
            row = new Argix.Enterprise.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.SortFacilityArrivalDate;
            row.Status = item.SortFacilityArrivalStatus;
            row.Location = item.SortFacilityLocation;
            detail.Add(row);
        }
        if (item.ActualDepartureDate.Trim().Length > 0) {
            row = new Argix.Enterprise.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualDepartureDate;
            row.Status = item.ActualDepartureStatus;
            row.Location = item.ActualDepartureLocation;
            detail.Add(row);
        }
        if (item.ActualArrivalDate.Trim().Length > 0) {
            row = new Argix.Enterprise.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualArrivalDate;
            row.Status = item.ActualArrivalStatus;
            row.Location = item.ActualArrivalLocation;
            detail.Add(row);
        }
        if (item.ActualStoreDeliveryDate.Trim().Length > 0) {
            row = new Argix.Enterprise.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualStoreDeliveryDate;
            row.Status = item.ActualStoreDeliveryStatus;
            row.Location = item.ActualStoreDeliveryLocation;
            detail.Add(row);
        }
        if (item.PODScanDate.Trim().Length > 0) {
            row = new Argix.Enterprise.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.PODScanDate;
            row.Status = item.PODScanStatus;
            row.Location = item.PODScanLocation;
            detail.Add(row);
        }
        this.grdDetail.DataSource = detail;
        this.grdDetail.DataBind();

        //POD Request link available if carton delivered
        this.lnkPODReq.Enabled = (item.ScanType == 3 && item.PODScanStatus.ToLower().Contains("delivered") == true);
        this.lnkPODReq.ToolTip = (item.ScanType == 3 && item.PODScanStatus.ToLower().Contains("delivered") == true) ? "Display POD image (if image available)" : "POD only available after carton delivery";
    }
    private void sendPODRequest() {
        //Send a POD request to Argix, send confirmation to user, and display confirmation
        TrackingItem item = null;
        if(ViewState["PODRequestItem"] is TrackingItem) item = (TrackingItem)ViewState["PODRequestItem"];
        if(item != null) {
            EmailGateway svcs = new EmailGateway();
            svcs.SendPODRequest(Membership.GetUser(), item);
            svcs.SendPODRequestConfirm(Membership.GetUser(), item);
            Master.ShowMessageBox("Thank you. Your POD request has been submitted. You will receive an email confirming your request.");
        }
    }
    #endregion
}
