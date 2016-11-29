using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class BookQuote:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                this.txtShipDate.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
                
                //Setup UI
                OnCurrentClientChanged(null,EventArgs.Empty);
            }
            else {
            }
            Master.CurrentClientChanged += new EventHandler(OnCurrentClientChanged);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnCurrentClientChanged(object sender,EventArgs e) {
        try {
            //Load information
            this.lblClientName.Text = Master.CurrentClient.Name;

            this.odsShippers.SelectParameters["clientID"].DefaultValue = Master.CurrentClient.ID.ToString();
            this.ddlShippers.DataBind();
            this.ddlShippers.SelectedIndex = 0;

            this.odsConsignees.SelectParameters["clientID"].DefaultValue = Master.CurrentClient.ID.ToString();
            this.ddlConsignees.DataBind();
            this.ddlConsignees.SelectedIndex = 0;
            
            resetQuote();
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnShipperChanged(object sender,EventArgs e) {
        //
        try {
            if (this.ddlShippers.SelectedValue == "New") Response.Redirect("~/Client/Shipper.aspx?id=0",false);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnConsigneeChanged(object sender,EventArgs e) {
        //
        try {
            if (this.ddlConsignees.SelectedValue == "New") Response.Redirect("~/Client/Consignee.aspx?id=0",false);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnSubmit(object sender,EventArgs e) {
        //Submit a quote
        if (!Page.IsValid) return;
        try {
            //Validate inputs
            ApplicationException aex = null;
            if (this.txtShipDate.Text.Length == 0) aex = new ApplicationException("Please enter a valid shipment date of the form yyyy-mm-dd.");
            else if (this.ddlShippers.SelectedIndex < 2) aex = new ApplicationException("Please select a valid shipper.");
            else if (this.ddlConsignees.SelectedIndex < 2) aex = new ApplicationException("Please select a valid consignee.");
            else if (this.txtWeight1.Text.Trim().Length == 0 || (int.Parse(this.txtWeight1.Text) < 1 || int.Parse(this.txtWeight1.Text) > 1500)) aex = new ApplicationException("Please enter a valid weight for pallet 1 (1 - 1500lbs).");
            else if (this.txtWeight2.Text.Trim().Length > 0 && (int.Parse(this.txtWeight2.Text) < 1 || int.Parse(this.txtWeight2.Text) > 1500)) aex = new ApplicationException("Please enter a valid weight for pallet 2 (1 - 1500lbs).");
            else if (this.txtWeight3.Text.Trim().Length > 0 && (int.Parse(this.txtWeight3.Text) < 1 || int.Parse(this.txtWeight3.Text) > 1500)) aex = new ApplicationException("Please enter a valid weight for pallet 3 (1 - 1500lbs).");
            else if (this.txtWeight4.Text.Trim().Length > 0 && (int.Parse(this.txtWeight4.Text) < 1 || int.Parse(this.txtWeight4.Text) > 1500)) aex = new ApplicationException("Please enter a valid weight for pallet 4 (1 - 1500lbs).");
            else if (this.txtWeight5.Text.Trim().Length > 0 && (int.Parse(this.txtWeight5.Text) < 1 || int.Parse(this.txtWeight5.Text) > 1500)) aex = new ApplicationException("Please enter a valid weight for pallet 5 (1 - 1500lbs).");
            else if (this.txtInsuranceValue1.Text.Trim().Length > 0 && (int.Parse(this.txtInsuranceValue1.Text) > 10000)) aex = new ApplicationException("Maximum insurance value $10,000.");
            else if (this.txtInsuranceValue2.Text.Trim().Length > 0 && (int.Parse(this.txtInsuranceValue2.Text) > 10000)) aex = new ApplicationException("Maximum insurance value $10,000.");
            else if (this.txtInsuranceValue3.Text.Trim().Length > 0 && (int.Parse(this.txtInsuranceValue3.Text) > 10000)) aex = new ApplicationException("Maximum insurance value $10,000.");
            else if (this.txtInsuranceValue4.Text.Trim().Length > 0 && (int.Parse(this.txtInsuranceValue4.Text) > 10000)) aex = new ApplicationException("Maximum insurance value $10,000.");
            else if (this.txtInsuranceValue5.Text.Trim().Length > 0 && (int.Parse(this.txtInsuranceValue5.Text) > 10000)) aex = new ApplicationException("Maximum insurance value $10,000.");
            if (aex == null) {
                //Calculate the quote
                LTLQuote quote = new LTLQuote();
                quote.Created = DateTime.Now;
                quote.ShipDate = DateTime.Parse(this.txtShipDate.Text);
                quote.ShipperID = int.Parse(this.ddlShippers.SelectedValue);
                quote.ConsigneeID = int.Parse(this.ddlConsignees.SelectedValue);
                quote.Pallet1Weight = int.Parse(this.txtWeight1.Text);
                quote.Pallet1Class = this.ddlClass1.SelectedValue;
                quote.Pallet1InsuranceValue = this.txtInsuranceValue1.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue1.Text) : 0;
                quote.Pallet2Weight = this.txtWeight2.Text.Trim().Length > 0 ? int.Parse(this.txtWeight2.Text) : 0;
                quote.Pallet2Class = this.ddlClass2.SelectedValue;
                quote.Pallet2InsuranceValue = this.txtInsuranceValue2.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue2.Text) : 0;
                quote.Pallet3Weight = this.txtWeight3.Text.Trim().Length > 0 ? int.Parse(this.txtWeight3.Text) : 0;
                quote.Pallet3Class = this.ddlClass3.SelectedValue;
                quote.Pallet3InsuranceValue = this.txtInsuranceValue3.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue3.Text) : 0;
                quote.Pallet4Weight = this.txtWeight4.Text.Trim().Length > 0 ? int.Parse(this.txtWeight4.Text) : 0;
                quote.Pallet4Class = this.ddlClass4.SelectedValue;
                quote.Pallet4InsuranceValue = this.txtInsuranceValue4.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue4.Text) : 0;
                quote.Pallet5Weight = this.txtWeight5.Text.Trim().Length > 0 ? int.Parse(this.txtWeight5.Text) : 0;
                quote.Pallet5Class = this.ddlClass5.SelectedValue;
                quote.Pallet5InsuranceValue = this.txtInsuranceValue5.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue5.Text) : 0;
                quote.InsidePickup = this.chkInsideO.Checked;
                quote.LiftGateOrigin = this.chkLiftGateO.Checked;
                quote.AppointmentOrigin = this.txtApptO.Text.Length > 0;
                quote.InsideDelivery = this.chkInsideD.Checked;
                quote.LiftGateDestination = this.chkLiftGateD.Checked;
                quote.AppointmentDestination = this.txtApptD.Text.Length > 0;
                quote.Pallets = 0;
                quote.Weight = 0;
                quote.PalletRate = 0;
                quote.FuelSurcharge = 0;
                quote.AccessorialCharge = 0;
                quote.InsuranceCharge = 0;
                quote.TollCharge = 0;
                quote.TotalCharge = 0;
                quote = new FreightGateway().CreateQuote(quote);

                //Update the quote
                //this.lblTransit.Text = quote.TransitMin.ToString() + " - " + quote.TransitMax.ToString() + " days transit";
                this.txtPallets.Text = quote.Pallets.ToString();
                this.txtWeight.Text = quote.Weight.ToString();
                this.txtRate.Text = "$" + quote.PalletRate.ToString();
                this.txtFSC.Text = "$" + quote.FuelSurcharge.ToString();
                this.txtAccessorial.Text = "$" + quote.AccessorialCharge.ToString();
                this.txtInsurance.Text = "$" + quote.InsuranceCharge.ToString();
                this.txtTSC.Text = "$" + quote.TollCharge.ToString();
                this.txtCharges.Text = "$" + quote.TotalCharge.ToString();

                //Enable booking
                this.btnBook.Enabled = true;
            }
            else {
                resetQuote();
                Master.ReportError(aex,3);
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    protected void OnBookQuote(object sender,EventArgs e) {
        //
        if (!Page.IsValid) return;
        try {
            LTLShipment shipment = new LTLShipment();
            shipment.Created = DateTime.Now;
            shipment.ClientID = Master.CurrentClient.ID;
            shipment.ShipDate = DateTime.Parse(this.txtShipDate.Text);
            shipment.ShipperID = int.Parse(this.ddlShippers.SelectedValue);
            shipment.ConsigneeID = int.Parse(this.ddlConsignees.SelectedValue);
            shipment.Pallet1Weight = int.Parse(this.txtWeight1.Text);
            shipment.Pallet1Class = this.ddlClass1.SelectedValue;
            shipment.Pallet1InsuranceValue = this.txtInsuranceValue1.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue1.Text) : 0;
            shipment.Pallet2Weight = this.txtWeight2.Text.Trim().Length > 0 ? int.Parse(this.txtWeight2.Text) : 0;
            shipment.Pallet2Class = this.ddlClass2.SelectedValue;
            shipment.Pallet2InsuranceValue = this.txtInsuranceValue2.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue2.Text) : 0;
            shipment.Pallet3Weight = this.txtWeight3.Text.Trim().Length > 0 ? int.Parse(this.txtWeight3.Text) : 0;
            shipment.Pallet3Class = this.ddlClass3.SelectedValue;
            shipment.Pallet3InsuranceValue = this.txtInsuranceValue3.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue3.Text) : 0;
            shipment.Pallet4Weight = this.txtWeight4.Text.Trim().Length > 0 ? int.Parse(this.txtWeight4.Text) : 0;
            shipment.Pallet4Class = this.ddlClass4.SelectedValue;
            shipment.Pallet4InsuranceValue = this.txtInsuranceValue4.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue4.Text) : 0;
            shipment.Pallet5Weight = this.txtWeight5.Text.Trim().Length > 0 ? int.Parse(this.txtWeight5.Text) : 0;
            shipment.Pallet5Class = this.ddlClass5.SelectedValue;
            shipment.Pallet5InsuranceValue = this.txtInsuranceValue5.Text.Trim().Length > 0 ? int.Parse(this.txtInsuranceValue5.Text) : 0;
            shipment.InsidePickup = this.chkInsideO.Checked;
            shipment.LiftGateOrigin = this.chkLiftGateO.Checked;
            shipment.AppointmentOrigin = this.txtApptO.Text.Trim().Length > 0 ? DateTime.Parse(this.txtApptO.Text) : DateTime.MinValue;
            shipment.InsideDelivery = this.chkInsideD.Checked;
            shipment.LiftGateDestination = this.chkLiftGateD.Checked;
            shipment.AppointmentDestination = this.txtApptD.Text.Trim().Length > 0 ? DateTime.Parse(this.txtApptD.Text) : DateTime.MinValue;
            shipment.Pallets = int.Parse(this.txtPallets.Text);
            shipment.Weight = decimal.Parse(this.txtWeight.Text);
            shipment.PalletRate = decimal.Parse(this.txtRate.Text.Replace("$",""));
            shipment.FuelSurcharge = decimal.Parse(this.txtFSC.Text.Replace("$",""));
            shipment.AccessorialCharge = decimal.Parse(this.txtAccessorial.Text.Replace("$",""));
            shipment.InsuranceCharge = decimal.Parse(this.txtInsurance.Text.Replace("$",""));
            shipment.TollCharge = decimal.Parse(this.txtTSC.Text.Replace("$",""));
            shipment.TotalCharge = decimal.Parse(this.txtCharges.Text.Replace("$",""));
            shipment.UserID = Membership.GetUser().UserName;
            shipment.LastUpdated = DateTime.Now;

            int id = new FreightGateway().CreateLTLShipment(shipment);
            Response.Redirect("~/Client/Shipments.aspx",false); 
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }

    private void resetQuote() {
        //
        this.txtPallets.Text = this.txtWeight.Text = this.txtRate.Text = this.txtFSC.Text = this.txtAccessorial.Text = this.txtTSC.Text = this.txtCharges.Text = "";
        this.btnBook.Enabled = false;
    }
}
