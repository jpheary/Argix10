using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;
using Argix.Freight.Client;

public partial class _PickupRequest : System.Web.UI.Page {
    //Members
    private PickupRequest mPickup = null;

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                int id = int.Parse(Request.QueryString["pickupid"]);
                if (id > 0) {
                    this.mPickup = new FreightClientGateway().ReadPickup(id);
                    ViewState.Add("Pickup",this.mPickup);
                    
                    //Populate form fields from existing
                    this.txtPickupDate.Text = this.mPickup.ScheduleDate.ToString("MM/dd/yyyy");
                    this.txtShipperName.Text = this.mPickup.Shipper;
                    this.txtShipperStreet.Text = this.mPickup.ShipperAddress.Split(new string[]{"\r\n"}, StringSplitOptions.None)[0];
                    this.txtShipperCity.Text = this.mPickup.ShipperAddress.Split(new string[] { "\r\n" },StringSplitOptions.None)[1].Split(new string[] { ", " },StringSplitOptions.None)[0];
                    this.txtShipperState.Text = this.mPickup.ShipperAddress.Split(new string[] { "\r\n" },StringSplitOptions.None)[1].Split(new string[] { ", " },StringSplitOptions.None)[1].Split(new string[] { " " },StringSplitOptions.None)[0];
                    this.txtShipperZip.Text = this.mPickup.ShipperAddress.Split(new string[] { "\r\n" },StringSplitOptions.None)[1].Split(new string[] { ", " },StringSplitOptions.None)[1].Split(new string[] { " " },StringSplitOptions.None)[1];
                    this.txtShipperPhone.Text = this.mPickup.ShipperPhone;
                    this.txtWindowOpen.Text = this.mPickup.WindowOpen.ToString().PadLeft(4,'0');
                    this.txtWindowClose.Text = this.mPickup.WindowClose.ToString().PadLeft(4,'0');
                    this.txtQuantity.Text = this.mPickup.Amount.ToString();
                    this.txtWeight.Text = this.mPickup.Weight.ToString();
                    this.txtComments.Text = this.mPickup.Comments;
                    
                    //Set read only fields
                    this.txtShipperZip.ReadOnly = this.txtConsigneeZip.ReadOnly = this.txtShipperStreet.ReadOnly = this.txtShipperCity.ReadOnly = this.txtShipperState.ReadOnly = id > 0;
                }
                else {
                    this.txtWindowOpen.Text = "09:00";
                    this.txtWindowClose.Text = "17:00";
                }
                this.lblClientName.Text = Master.CurrentClient.Name;
            }
            else {
                this.mPickup = ViewState["Pickup"] != null ? (PickupRequest)ViewState["Pickup"] : null;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnShipperZipChanged(object sender,EventArgs e) {
        //
        try {
            string zipCode = this.txtShipperZip.Text;
            ServiceLocation location = new FreightGateway().ReadPickupLocation(zipCode);
            if (location == null) {
                this.txtShipperZip.Text = "";
                this.txtShipperZip.Focus();
                throw new ApplicationException(zipCode + " is currently not supported for pickup.");
            }
            else {
                this.txtShipperCity.Text = location.City.Trim();
                this.txtShipperState.Text = location.State.Trim();
                this.txtConsigneeZip.Focus();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnConsigneeZipChanged(object sender,EventArgs e) {
        //
        try {
            string zipCode = this.txtConsigneeZip.Text;
            ServiceLocation location = new FreightGateway().ReadServiceLocation(zipCode);
            if (location == null) {
                this.txtConsigneeZip.Text = "";
                this.txtConsigneeZip.Focus();
                throw new ApplicationException(zipCode + " is currently not supported for delivery.");
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Submit":
                    //Prepare the pickup request
                    Argix.Freight.Client.PickupRequest request = null;
                    if (this.mPickup == null) {
                        request = new Argix.Freight.Client.PickupRequest();
                        request.RequestID = 0;
                        request.Created = DateTime.Now;
                        request.CreateUserID = "PSP";
                        request.ScheduleDate = DateTime.Parse(this.txtPickupDate.Text);
                        request.CallerName = Membership.GetUser().UserName;

                        //Need Tsort clientNumber for Dispatch
                        request.ClientNumber = Master.CurrentClient.Number;
                        request.Client = Master.CurrentClient.Name;

                        request.ShipperNumber = "";
                        request.Shipper = this.txtShipperName.Text;
                        request.ShipperAddress = this.txtShipperStreet.Text + "\r\n" + this.txtShipperCity.Text + ", " + this.txtShipperState.Text + " " + this.txtShipperZip.Text;
                        request.ShipperPhone = this.txtShipperPhone.Text;
                        request.WindowOpen = int.Parse(this.txtWindowOpen.Text.Replace(":",""));
                        request.WindowClose = int.Parse(this.txtWindowClose.Text.Replace(":",""));
                        request.TerminalNumber = "0001";
                        request.Terminal = "ARGIX LOGISTICS RIDGEFIELD";
                        request.DriverName = "";
                        request.OrderType = "B";
                        request.Amount = int.Parse(this.txtQuantity.Text);
                        request.AmountType = "Pallets";
                        request.FreightType = "Tsort";
                        request.Weight = int.Parse(this.txtWeight.Text);
                        request.Comments = this.txtComments.Text;
                        request.IsTemplate = false;
                        request.UserID = Membership.GetUser().UserName;
                        request.LastUpdated = DateTime.Now;

                        //Request a pickup
                        int pickupID = new Argix.Freight.Client.FreightClientGateway().RequestPickup(request);

                        //Send confirmation message to client and sales rep (if applicable)
                        new NotifyService().NotifyPickupConfirmation(Master.CurrentClient.ContactEmail,pickupID.ToString(),request);
                        if (Master.SalesRep != null && Master.CurrentClient.ID != Master.SalesRep.ID) new NotifyService().NotifyPickupConfirmation(Master.SalesRep.ContactEmail,pickupID.ToString(),request);

                        //Notify client
                        Master.ShowMessageBox("New pickup request " + pickupID.ToString() + " has been created.");

                        this.btnSubmit.Enabled = false;
                        this.btnCancel.Text = "Close";
                    }
                    else {
                        request = this.mPickup;
                        request.ScheduleDate = DateTime.Parse(this.txtPickupDate.Text);
                        request.WindowOpen = int.Parse(this.txtWindowOpen.Text.Replace(":",""));
                        request.WindowClose = int.Parse(this.txtWindowClose.Text.Replace(":",""));
                        request.Amount = int.Parse(this.txtQuantity.Text);
                        request.Weight = int.Parse(this.txtWeight.Text);
                        request.Comments = this.txtComments.Text;
                        request.UserID = Membership.GetUser().UserName;
                        request.LastUpdated = DateTime.Now;

                        //Update the pickup
                        bool updated = new Argix.Freight.Client.FreightClientGateway().UpdatePickup(request);

                        //Send confirmation message to client and sales rep (if applicable)
                        new NotifyService().NotifyPickupConfirmation(Master.CurrentClient.ContactEmail,request.RequestID.ToString(),request);
                        if (Master.SalesRep != null && Master.CurrentClient.ID != Master.SalesRep.ID) new NotifyService().NotifyPickupConfirmation(Master.SalesRep.ContactEmail,request.RequestID.ToString(),request);

                        //Notify client
                        Master.ShowMessageBox("Pickup request " + request.RequestID.ToString() + " has been updated.");

                        this.btnSubmit.Enabled = false;
                        this.btnCancel.Text = "Close";
                    }
                    break;
                case "Cancel":
                    Response.Redirect("~/Client/PickupLog.aspx",false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 4); }
    }
}
