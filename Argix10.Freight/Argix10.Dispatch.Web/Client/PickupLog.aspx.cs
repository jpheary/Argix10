using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class _PickupLog:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "pickuplog";
                OnChangeView(null,new CommandEventArgs(view,null));
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "pickuplog":
                    this.mvwPage.ActiveViewIndex = 0;
                    this.liPickupLog.Style["border-bottom-style"] = "none";
                    break;
            }
            this.liBlank1.Style["border-top-style"] = this.liBlank2.Style["border-top-style"] = "none";
            this.liBlank1.Style["border-right-style"] = this.liBlank2.Style["border-right-style"] = "none";
            this.liBlank1.Style["border-bottom-style"] = this.liBlank2.Style["border-bottom-style"] = "solid";
            this.liBlank1.Style["border-left-style"] = this.liBlank2.Style["border-left-style"] = "none";
            this.liBlank3.Style["border-top-style"] = this.liBlank4.Style["border-top-style"] = "none";
            this.liBlank3.Style["border-right-style"] = this.liBlank4.Style["border-right-style"] = "none";
            this.liBlank3.Style["border-bottom-style"] = this.liBlank4.Style["border-bottom-style"] = "solid";
            this.liBlank3.Style["border-left-style"] = this.liBlank4.Style["border-left-style"] = "none";
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnPickupSelected(object sender,EventArgs e) {
        //Change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        this.btnNew.Enabled = true;
        this.btnCancel.Enabled = this.grdPickupLog.SelectedRow != null;
    }
    protected void OnPickupLogCommand(object sender,CommandEventArgs e) {
        //Event handler for change in view
        switch (e.CommandName) {
            case "New": 
                Response.Redirect("~/Client/PickupRequest.aspx?pickupid=0"); break;
            case "Cancel":
                //Cancel the selected pickup request
                int pickupID = int.Parse(this.grdPickupLog.SelectedDataKey.Value.ToString());
                bool cancelled = new Argix.Freight.Client.FreightClientGateway().CancelPickup(pickupID);
                
                //Send confirmation message to client
                new NotifyService().NotifyPickupCancelled(Membership.GetUser().Email,pickupID.ToString());

                //Notify client
                Master.ShowMessageBox("Pickup request " + pickupID.ToString() + " has been cancelled.");
                break;
        }
    }
    protected void OnTimerTick(object sender,EventArgs e) {
        //Event handler for issue timer tick event
        this.grdPickupLog.DataBind();
    }
}
