using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Enterprise.Tracking;

public partial class _Tracking : System.Web.UI.Page {
    //Members
    TrackingItems mItems = null;

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                string number = Request.QueryString["number"];
                if (number.Length > 0) {
                    this.mItems = new TrackingGateway().TrackShipment(number);
                    ViewState.Add("Items",this.mItems);
                    if (this.mItems != null && this.mItems.Count > 0) {
                        //Show summary
                        this.grdTrack.DataSource = this.mItems;
                        this.grdTrack.DataBind();
                        this.grdTrack.SelectedIndex = 0;
                        OnItemSelected(null,EventArgs.Empty);
                    }
                    else
                        showMessageBox("Could not find shipment information.");
                }
            }
            else {
                this.mItems = (TrackingItems)ViewState["Items"];
            }
        }
        catch (Exception ex) { reportError(ex,3); }
    }
    protected void OnItemSelected(object sender,EventArgs e) { 
        //
        if (this.grdTrack.SelectedDataKey != null) {
            string itemNumber = this.grdTrack.SelectedDataKey.Value.ToString();
            foreach (TrackingItem item in this.mItems) {
                if (item.ItemNumber == itemNumber) {
                    showItem(item);
                    break;
                }
            }
        }
        else
            showItem(null);
    }

    #region Local Services: showItem()
    private void showItem(TrackingItem item) {
        //Display summary
        if (item != null) {
            this.lblCartonNumber.Text = item.CartonNumber;
            this.lblClientName.Text = item.ClientName;
            this.lblStoreNumber.Text = "Consignee " + item.StoreNumber.PadLeft(5,'0');
            this.lblStore.Text = ": " + item.StoreName + ", " + item.StoreAddress1 + (item.StoreAddress2.Length > 0 ? ", " + item.StoreAddress2 + ", " : " ") + item.StoreCity + ", " + item.StoreState + " " + item.StoreZip;
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
            TrackingItems detail = new TrackingItems();
            TrackingItem row = null;
            if (item.SortFacilityArrivalDate.Trim().Length > 0) {
                row = new TrackingItem();
                row.ItemNumber = item.ItemNumber;
                row.DateTime = item.SortFacilityArrivalDate;
                row.Status = item.SortFacilityArrivalStatus;
                row.Location = item.SortFacilityLocation;
                detail.Add(row);
            }
            if (item.ActualDepartureDate.Trim().Length > 0) {
                row = new TrackingItem();
                row.ItemNumber = item.ItemNumber;
                row.DateTime = item.ActualDepartureDate;
                row.Status = item.ActualDepartureStatus;
                row.Location = item.ActualDepartureLocation;
                detail.Add(row);
            }
            if (item.ActualArrivalDate.Trim().Length > 0) {
                row = new TrackingItem();
                row.ItemNumber = item.ItemNumber;
                row.DateTime = item.ActualArrivalDate;
                row.Status = item.ActualArrivalStatus;
                row.Location = item.ActualArrivalLocation;
                detail.Add(row);
            }
            if (item.ActualStoreDeliveryDate.Trim().Length > 0) {
                row = new TrackingItem();
                row.ItemNumber = item.ItemNumber;
                row.DateTime = item.ActualStoreDeliveryDate;
                row.Status = item.ActualStoreDeliveryStatus;
                row.Location = item.ActualStoreDeliveryLocation;
                detail.Add(row);
            }
            if (item.PODScanDate.Trim().Length > 0) {
                row = new TrackingItem();
                row.ItemNumber = item.ItemNumber;
                row.DateTime = item.PODScanDate;
                row.Status = item.PODScanStatus;
                row.Location = item.PODScanLocation;
                detail.Add(row);
            }
            this.grdDetail.DataSource = detail;
            this.grdDetail.DataBind();
        }
        else {
            this.lblCartonNumber.Text = this.lblClientName.Text = this.lblStoreNumber.Text = "";
            this.lblStore.Text = this.lblVendorName.Text = this.lblPickupDate.Text = "";
            this.lblBOLNumber.Text = this.lblTLNumber.Text = this.lblLabelNumber.Text = this.lblPONumber.Text = "";
            this.lblWeight.Text = this.lblShipmentNumber.Text = this.lblSchDelivery.Text = "";
            this.grdDetail.DataSource = null;
            this.grdDetail.DataBind();
        }
    }
    #endregion
    private void reportError(Exception ex,int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;

            string username = Membership.GetUser() != null ? Membership.GetUser().UserName : "guest";
            new Argix.Freight.FreightGateway().WriteLogEntry(Argix.Freight.LogLevel.Warning,username,ex);
            showMessageBox(msg);
        }
        catch (Exception) { }
    }
    private void showMessageBox(string message) {
        //
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.lblMsg,typeof(Label),"Error","alert('" + message + "');",true);
    }
}