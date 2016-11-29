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
    public int mView = 0;

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "shipments";

                //Setup UI
                this.txtShipDateStart.Text = DateTime.Today.AddDays(-30).ToString("MM/dd/yyyy");
                this.txtShipDateEnd.Text = DateTime.Today.ToString("MM/dd/yyyy");
                OnCurrentClientChanged(null, EventArgs.Empty);
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
            if(Master.CurrentClient.DenialDate.CompareTo(DateTime.MinValue) > 0)
                this.cvStatus.ErrorMessage = "** Your account has been rejected.";
            else if(Master.CurrentClient.ApprovalDate.CompareTo(DateTime.MinValue) == 0 && Master.CurrentClient.DenialDate.CompareTo(DateTime.MinValue) == 0)
                this.cvStatus.ErrorMessage = "** Your account is currently under review by our Finance department.";
            this.cvStatus.IsValid = Master.CurrentClientApproved;
            this.lblClient.Text = Master.CurrentClient.Name;

            //Load information
            this.odsShipments.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
            this.grdShipments.DataBind();
            this.grdShipments.SelectedIndex = -1;

            this.cboShippers.Items.Clear();
            this.cboShippers.Items.Add("");
            this.cboConsignees.Items.Clear();
            this.cboConsignees.Items.Add("");
            this.odsShippers.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
            this.cboShippers.DataBind();
            this.cboShippers.SelectedIndex = -1;
            this.odsConsignees.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
            this.cboConsignees.DataBind();
            this.cboConsignees.SelectedIndex = -1;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnShipmentSelected(object sender,EventArgs e) {
        //Change in selected pickup
        try {
            if(this.grdShipments.SelectedDataKey != null) {
                this.btnLabels.OnClientClick = "javascript:var w=window.open('Labels.aspx?number=" + this.grdShipments.SelectedDataKey.Values["ShipmentNumber"].ToString() + "', '_blank', 'height=550,width=1000,status=no,toolbar=yes,menubar=no,location=no,scrollbars=yes');return false;";
                this.btnBOL.OnClientClick = "javascript:var w=window.open('BOL.aspx?number=" + this.grdShipments.SelectedDataKey.Values["ShipmentNumber"].ToString() + "', '_blank', 'height=550,width=1000,status=no,toolbar=yes,menubar=no,location=no,scrollbars=yes');return false;";
            }
            else
                this.btnLabels.OnClientClick = this.btnBOL.OnClientClick = "";
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            //Apply service rules
            DateTime shipDate = this.grdShipments.SelectedDataKey != null ? DateTime.Parse(this.grdShipments.SelectedDataKey.Values["ShipDate"].ToString()) : DateTime.MinValue;
            bool cancelled = this.grdShipments.SelectedDataKey != null ? this.grdShipments.SelectedDataKey.Values["Cancelled"].ToString().Length > 0 : false;
            
            this.btnNew.Enabled = Master.CurrentClientApproved;
            this.btnUpdate.Enabled = Master.CurrentClientApproved && this.grdShipments.SelectedRow != null && !cancelled;
            this.btnCancel.Enabled = Master.CurrentClientApproved && this.grdShipments.SelectedRow != null && shipDate.CompareTo(DateTime.Today) > 0 && !cancelled;
            this.btnBOL.Enabled = Master.CurrentClientApproved && this.grdShipments.SelectedRow != null && !cancelled;
            this.btnLabels.Enabled = Master.CurrentClientApproved && this.grdShipments.SelectedRow != null && !cancelled;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnShipmentCommand(object sender,CommandEventArgs e) {
        //Event handler for change in view
        string number = "";
        try {
            switch (e.CommandName) {
                case "New": 
                    Response.Redirect("~/Client/Shipment.aspx?number=",false); 
                    break;
                case "Update":
                    number = this.grdShipments.SelectedDataKey.Value.ToString();
                    Response.Redirect("~/Client/Shipment.aspx?number=" + number,false);
                    break;
                case "Cancel":
                    //Cancel the selected shipment
                    number = this.grdShipments.SelectedDataKey.Value.ToString();
                    bool cancelled = new FreightGateway().CancelLTLShipment(number,Membership.GetUser().UserName);

                    //Update view
                    this.grdShipments.DataBind();
                    this.mView = 0;

                    //Notify client
                    Master.ShowMessageBox("Shipment " + number + " has been cancelled.");
                    break;
                case "Search":
                    this.odsSearch.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
                    this.grdSearch.DataBind();
                    this.mView = 1;
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
}
