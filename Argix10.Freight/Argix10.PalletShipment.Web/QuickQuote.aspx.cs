using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class QuickQuote : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                this.txtShipDate.Text = DateTime.Today.AddDays(1).ToString("MM/dd/yyyy");
                //try { this.lblEnroll.Visible = this.btnEnroll.Visible = Membership.GetUser() == null; } catch { }
                this.btnEnroll.Visible = false;
                resetQuote();
            }
            else {
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnOriginChanged(object sender,EventArgs e) {
        //
        try {
            string zipCode = this.txtOriginZip.Text;
            if (new FreightGateway().ReadPickupLocation(zipCode) == null) {
                this.txtOriginZip.Text = "";
                this.txtOriginZip.Focus();
                throw new ApplicationException(zipCode + " is currently not supported for pickup.");
            }
            else
                this.txtDestZip.Focus();
            resetQuote();
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnDestinationChanged(object sender,EventArgs e) {
        //
        try {
            string zipCode = this.txtDestZip.Text;
            if (new FreightGateway().ReadServiceLocation(zipCode) == null) {
                this.txtDestZip.Text = "";
                this.txtDestZip.Focus();
                throw new ApplicationException(zipCode + " is currently not supported for delivery.");
            }
            else
                this.txtWeight1.Focus();
            resetQuote();
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnSubmit(object sender,EventArgs e) {
        //Submit a quote
        if (!Page.IsValid) return;
        try {
            //Reset

            //Validate inputs
            ApplicationException aex = null;
            if (this.txtShipDate.Text.Length == 0) aex = new ApplicationException("Please enter a valid shipment date of the form yyyy-mm-dd.");
            else if (this.txtOriginZip.Text.Length != 5) aex = new ApplicationException("Please enter a valid 5-digit origin zip code.");
            else if (this.txtDestZip.Text.Length != 5) aex = new ApplicationException("Please enter a valid 5-digit destination zip code.");
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
                quote.OriginZip = this.txtOriginZip.Text;
                quote.DestinationZip = this.txtDestZip.Text;
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
                quote.AppointmentOrigin = this.chkApptO.Checked;
                quote.InsideDelivery = this.chkInsideD.Checked;
                quote.LiftGateDestination = this.chkLiftGateD.Checked;
                quote.AppointmentDestination = this.chkApptD.Checked;
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
                this.btnEnroll.Enabled = true;
            }
            else {
                resetQuote();
                Master.ReportError(aex, 3);
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
    protected void OnEnroll(object sender,EventArgs e) {
        //
        if (!Page.IsValid) return;
        try {
            LTLQuote quote = new LTLQuote();
            quote.Created = DateTime.Now;
            quote.ShipDate = DateTime.Parse(this.txtShipDate.Text);
            quote.OriginZip = this.txtOriginZip.Text;
            quote.DestinationZip = this.txtDestZip.Text;
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
            quote.AppointmentOrigin = this.chkApptO.Checked;
            quote.InsideDelivery = this.chkInsideD.Checked;
            quote.LiftGateDestination = this.chkLiftGateD.Checked;
            quote.AppointmentDestination = this.chkApptD.Checked;
            quote.Pallets = int.Parse(this.txtPallets.Text);
            quote.Weight = decimal.Parse(this.txtWeight.Text);
            quote.PalletRate = decimal.Parse(this.txtRate.Text.Replace("$",""));
            quote.FuelSurcharge = decimal.Parse(this.txtFSC.Text.Replace("$",""));
            quote.AccessorialCharge = decimal.Parse(this.txtAccessorial.Text.Replace("$",""));
            quote.InsuranceCharge = decimal.Parse(this.txtInsurance.Text.Replace("$",""));
            quote.TollCharge = decimal.Parse(this.txtTSC.Text.Replace("$",""));
            quote.TotalCharge = decimal.Parse(this.txtCharges.Text.Replace("$",""));

            Session.Add("Quote",quote);
            Response.Redirect("~/Enroll.aspx",false);
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }

    private void resetQuote() {
        //
        this.txtPallets.Text = this.txtWeight.Text = this.txtRate.Text = this.txtFSC.Text = this.txtAccessorial.Text = this.txtTSC.Text = this.txtCharges.Text = "";
        this.btnEnroll.Enabled = false;
    }
}
