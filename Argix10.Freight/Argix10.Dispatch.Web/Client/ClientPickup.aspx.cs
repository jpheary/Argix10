using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class _ClientPickup : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                int id = int.Parse(Request.QueryString["pickupid"]);
                if (id > 0) {
                    //this.mPickup = new FreightGateway().ReadLTLShipper(id);
                    //ViewState.Add("Pickup",this.mPickup);

                    //Populate form fields from existing
                    //TODO

                    //Set read only fields
                    //this.txtName.ReadOnly = this.txtCity.ReadOnly = this.txtState.ReadOnly = this.txtZip.ReadOnly = id > 0;
                }
                else {
                    this.txtWindowOpen.Text = "900";
                    this.txtWindowClose.Text = "1700";
                }
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
                    Argix.Freight.Client.ClientPickup request = new Argix.Freight.Client.ClientPickup();
                    request.ID = 0;
                    request.Created = DateTime.Now;
                    request.CreateUserID = Membership.GetUser().UserName;
                    request.ScheduleDate = DateTime.Parse(this.txtPickupDate.Text);
                    request.ClientNumber = Profile.GetProfile(Membership.GetUser().UserName).ClientID;
                    request.Client = " ";
                    request.ShipperNumber = "";
                    request.Shipper = this.txtShipperName.Text;
                    request.ShipperAddress = this.txtShipperStreet.Text + "\r\n" + this.txtShipperCity.Text + ", " + this.txtShipperState.Text + " " + this.txtShipperZip.Text;
                    request.ShipperPhone = this.txtShipperPhone.Text;
                    request.ShipperWindowOpen = int.Parse(this.txtWindowOpen.Text);
                    request.ShipperWindowClose = int.Parse(this.txtWindowClose.Text);
                    request.Amount = int.Parse(this.txtQuantity.Text);
                    request.AmountType = "Pallets";
                    request.Weight = int.Parse(this.txtWeight.Text);
                    request.Comments = this.txtComments.Text;
                    request.UserID = Membership.GetUser().UserName;
                    request.LastUpdated = DateTime.Now;

                    //Request a pickup
                    int pickupID = new Argix.Freight.Client.FreightClientGateway().RequestClientPickup(request);

                    //Send confirmation message to client
                    new NotifyService().NotifyPickupConfirmation(Membership.GetUser().Email,pickupID.ToString());
                    
                    //Notify client
                    Master.ShowMessageBox("New client pickup " + pickupID.ToString() + " has been created.");

                    this.btnSubmit.Enabled = false;
                    this.btnCancel.Text = "Close";
                    break;
                case "Cancel":
                    Response.Redirect("~/Client/ClientPickups.aspx",false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 4); }
    }
}
