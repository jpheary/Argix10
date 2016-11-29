using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class LOccitane:System.Web.UI.Page {
    //Members
    private string mCompanyID = "012";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        if (!Page.IsPostBack) {
            string item = Request.QueryString["item"] == null ? "" : Request.QueryString["item"].ToString();
            if (item.Length > 0) {
                //Prepare the request
                string cn = item.Trim();
                cn = cn.Replace("\n","");
                cn = cn.Replace("-","");
                cn = cn.Replace(Convert.ToChar(','),Convert.ToChar(13));
                string[] numbers = cn.Split(Convert.ToChar(13));

                //Get tracking details for item
                Argix.TrackingItems items = new Argix.TrackingProxy().TrackCartons(numbers,this.mCompanyID);

                //Display the item
                if (items.Count > 0) showItem(items[0]);
            }
        }
        Master.GoSummaryVisible = Master.GoTrackVisible = false;
    }
    private void showItem(Argix.TrackingItem item) {
        //Display detail
        Argix.TrackingItems detail = new Argix.TrackingItems();
        Argix.TrackingItem row = null;
        if (item.PODScanDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.PODScanDate;
            row.Status = item.PODScanStatus;
            row.Location = item.PODScanLocation;
            detail.Add(row);
        }
        if (item.ActualStoreDeliveryDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualStoreDeliveryDate;
            row.Status = item.ActualStoreDeliveryStatus;
            row.Location = item.ActualStoreDeliveryLocation;
            detail.Add(row);
        }
        if (item.ActualArrivalDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualArrivalDate;
            row.Status = item.ActualArrivalStatus;
            row.Location = item.ActualArrivalLocation;
            detail.Add(row);
        }
        if (item.ActualDepartureDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.ActualDepartureDate;
            row.Status = item.ActualDepartureStatus;
            row.Location = item.ActualDepartureLocation;
            detail.Add(row);
        }
        if (item.SortFacilityArrivalDate.Trim().Length > 0) {
            row = new Argix.TrackingItem();
            row.ItemNumber = item.ItemNumber;
            row.DateTime = item.SortFacilityArrivalDate;
            row.Status = item.SortFacilityArrivalStatus;
            row.Location = item.SortFacilityLocation;
            detail.Add(row);
        }
        this.grdDetail.DataSource = detail;
        this.grdDetail.DataBind();

        //Display summary
        this.lblDetail_ID.Text += item.ItemNumber;
        this.lblDetail_Status.Text = item.Status.ToString();
        this.lblDetailSum.Text = "in " + item.Location + " on " + item.DateTime;
        this.lblFromInfo.Text = item.VendorName;
        this.lblFromInfo.Text += "\nPickup " + item.PickupDate;
        this.lblFromInfo.Text += "\nBOL# " + item.BOLNumber;
        this.lblFromInfo.Text += "\nLabel# " + item.LabelNumber + " on TL# " + item.TLNumber;
        this.lblToInfo.Text = item.StoreName;
        this.lblToInfo.Text += "\n" + item.StoreAddress1 + ", " + item.StoreAddress2;
        this.lblToInfo.Text += "\n" + item.StoreCity + ", " + item.StoreState + " " + item.StoreZip;
        this.lblToInfo.Text += "\nSigner: " + item.Signer;
        this.lbShipInfo.Text = "Ship date: " + item.DateTime;
        this.lbShipInfo.Text += "\nPieces: ";
        this.lbShipInfo.Text += "\nTotal weight: " + item.Weight.ToString() + " lbs";
        this.lbShipInfo.Text += "\n";
    }
}
