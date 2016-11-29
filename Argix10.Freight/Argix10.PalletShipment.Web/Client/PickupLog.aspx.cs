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

                //Setup UI
                OnCurrentClientChanged(null,EventArgs.Empty);
            }
            Master.CurrentClientChanged += new EventHandler(OnCurrentClientChanged);
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnCurrentClientChanged(object sender,EventArgs e) {
        //Event handelr for change in current client from Master page
        try {
            //Set client account status
            this.cvStatus.ErrorMessage = Master.CurrentClient.Approved ? "" : "** Your account is currently under review by our Finance department.";
            this.cvStatus.IsValid = Master.CurrentClient.Approved;
            this.lblClient.Text = Master.CurrentClient.Name;

            //Load information
            this.odsPickupLog.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number.ToString();
            this.grdPickupLog.DataBind();
            this.grdPickupLog.SelectedIndex = -1;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "pickuplog":
                    this.mvwPage.ActiveViewIndex = 0;
                    this.liPickupLog.Style["border-bottom-style"] = "none";
                    this.odsPickupLog.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number.ToString();
                    this.grdPickupLog.DataBind();
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
        try {
            this.btnNew.Enabled = Master.CurrentClient.Approved;

            DateTime scheduleDate = this.grdPickupLog.SelectedDataKey != null ? DateTime.Parse(this.grdPickupLog.SelectedDataKey.Values["ScheduleDate"].ToString()) : DateTime.MinValue;
            bool pickedup = this.grdPickupLog.SelectedDataKey != null ? this.grdPickupLog.SelectedDataKey.Values["ActualPickup"].ToString().Length > 0 : false;
            bool cancelled = this.grdPickupLog.SelectedDataKey != null ? this.grdPickupLog.SelectedDataKey.Values["Cancelled"].ToString().Length > 0 : false;

            //Allow update/cancel if not pickup day, not picked up already, and not cancelled already
            this.btnUpdate.Enabled = Master.CurrentClient.Approved && this.grdPickupLog.SelectedRow != null && scheduleDate.CompareTo(DateTime.Today) > 0 && !pickedup && !cancelled;
            this.btnCancel.Enabled = Master.CurrentClient.Approved && this.grdPickupLog.SelectedRow != null && scheduleDate.CompareTo(DateTime.Today) > 0 && !pickedup && !cancelled;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnPickupLogCommand(object sender,CommandEventArgs e) {
        //Event handler for change in view
        int pickupID = 0;
        try {
            switch (e.CommandName) {
                case "New": 
                    Response.Redirect("~/Client/PickupRequest.aspx?pickupid=0",false); break;
                case "Update":
                    pickupID = int.Parse(this.grdPickupLog.SelectedDataKey.Value.ToString());
                    Response.Redirect("~/Client/PickupRequest.aspx?pickupid=" + pickupID.ToString(),false);
                    break;
                case "Cancel":
                    //Cancel the selected pickup request
                    pickupID = int.Parse(this.grdPickupLog.SelectedDataKey.Value.ToString());
                    bool cancelled = new Argix.Freight.Client.FreightClientGateway().CancelPickup(pickupID);
                
                    //Send confirmation message to client and sales rep (if applicable)
                    Argix.Freight.Client.PickupRequest pickup = new Argix.Freight.Client.FreightClientGateway().ReadPickup(pickupID);
                    new NotifyService().NotifyPickupCancelled(Master.CurrentClient.ContactEmail,pickupID.ToString(),pickup);
                    if (Master.SalesRep != null && Master.CurrentClient.ID != Master.SalesRep.ID) new NotifyService().NotifyPickupCancelled(Master.SalesRep.ContactEmail,pickupID.ToString(),pickup);

                    //Update log
                    this.grdPickupLog.DataBind();

                    //Notify client
                    Master.ShowMessageBox("Pickup request " + pickupID.ToString() + " has been cancelled.");
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
}
