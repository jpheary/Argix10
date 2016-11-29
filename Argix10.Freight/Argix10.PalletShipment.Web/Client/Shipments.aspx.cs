using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Shipments:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "shipments";
                OnChangeView(null,new CommandEventArgs(view,null));

                //Setup UI
                OnCurrentClientChanged(null,EventArgs.Empty);
            }
            Master.CurrentClientChanged += new EventHandler(OnCurrentClientChanged);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
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
            this.odsShipments.SelectParameters["clientID"].DefaultValue = Master.CurrentClient.ID.ToString();
            this.grdShipments.DataBind();
            this.grdShipments.SelectedIndex = -1;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "shipments":
                    this.mvwPage.ActiveViewIndex = 0;
                    this.liShipments.Style["border-bottom-style"] = "none";
                    this.odsShipments.SelectParameters["clientID"].DefaultValue = Master.CurrentClient.ID.ToString();
                    this.grdShipments.DataBind();
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
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnShipmentSelected(object sender,EventArgs e) {
        //Change in selected pickup
        OnValidateForm(null,EventArgs.Empty);
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnNew.Enabled = Master.CurrentClient.Approved;

            //TODO: Apply rules
            //DateTime scheduleDate = this.grdPickupLog.SelectedDataKey != null ? DateTime.Parse(this.grdPickupLog.SelectedDataKey.Values["ScheduleDate"].ToString()) : DateTime.MinValue;
            //bool pickedup = this.grdPickupLog.SelectedDataKey != null ? this.grdPickupLog.SelectedDataKey.Values["ActualPickup"].ToString().Length > 0 : false;
            //bool cancelled = this.grdPickupLog.SelectedDataKey != null ? this.grdPickupLog.SelectedDataKey.Values["Cancelled"].ToString().Length > 0 : false;
            this.btnUpdate.Enabled = Master.CurrentClient.Approved && this.grdShipments.SelectedRow != null;
            this.btnCancel.Enabled = Master.CurrentClient.Approved && this.grdShipments.SelectedRow != null;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnShipmentCommand(object sender,CommandEventArgs e) {
        //Event handler for change in view
        int id = 0;
        try {
            switch (e.CommandName) {
                case "New": Response.Redirect("~/Client/BookQuote.aspx",false); break;
                case "Update":
                    id = int.Parse(this.grdShipments.SelectedDataKey.Value.ToString());
                    Response.Redirect("~/Client/BookQuote.aspx?id=" + id.ToString(),false);
                    break;
                case "Cancel":
                    //Cancel the selected pickup request
                    id = int.Parse(this.grdShipments.SelectedDataKey.Value.ToString());
                    bool cancelled = false; //new Argix.Freight.FreightGateway().CancelLTLShipment(id);

                    //Send confirmation message to client and sales rep (if applicable)
                    Argix.Freight.LTLShipment shipment = null;  //new Argix.Freight.FreightGateway().ReadLTLShipment(id);
                    //new NotifyService().NotifyShipmentCancelled(Master.CurrentClient.ContactEmail,id.ToString(),shipment);
                    //if (Master.SalesRep != null && Master.CurrentClient.ID != Master.SalesRep.ID) new NotifyService().NotifyShipmentCancelled(Master.SalesRep.ContactEmail,id.ToString(),pickup);

                    //Update log
                    this.grdShipments.DataBind();

                    //Notify client
                    Master.ShowMessageBox("Shipment " + id.ToString() + " has been cancelled.");
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
}
