using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class _Shipment:System.Web.UI.Page {
    //Members
    private LTLShipment2 mShipment = null;
    private const int MAX_WEIGHT = 2000, MAX_LIFTGATE_WEIGHT = 1500;
    private const decimal MAX_INSURED = 10000.00M;

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                string number = Request.QueryString["number"];
                if (number != null && number.Length > 0) this.mShipment = new FreightGateway().ReadLTLShipment(number);
                ViewState.Add("Shipment",this.mShipment);

                //Setup UI
                OnCurrentClientChanged(null,EventArgs.Empty);
            }
            else {
                this.mShipment = ViewState["Shipment"] != null ? (LTLShipment2)ViewState["Shipment"] : null;
            }
            Master.CurrentClientChanged += new EventHandler(OnCurrentClientChanged);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnCurrentClientChanged(object sender,EventArgs e) {
        try {
            //Get lists
            this.odsShippers.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
            this.ddlShippers.DataBind();
            this.ddlShippers.SelectedIndex = 0;

            this.odsConsignees.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
            this.ddlConsignees.DataBind();
            this.ddlConsignees.SelectedIndex = 0;

            //Setup UI; disable change for new/update
            Master.ClientsEnabled = false;
            this.lblClientName.Text = Master.CurrentClient.Name;
            if (this.mShipment != null) {
                this.txtShipDate.Text = this.mShipment.ShipDate.ToString("MM/dd/yyyy");
                this.ddlShippers.SelectedValue = this.mShipment.ShipperNumber;
                OnShipperChanged(this.ddlShippers, EventArgs.Empty);
                this.ddlConsignees.SelectedValue = this.mShipment.ConsigneeNumber.ToString();
                OnConsigneeChanged(this.ddlShippers, EventArgs.Empty);
                this.txtWeight1.Text = this.mShipment.Pallet1Weight.ToString("#0");
                this.txtInsuranceValue1.Text = this.mShipment.Pallet1InsuranceValue > 0 ? this.mShipment.Pallet1InsuranceValue.ToString("#0") : "";
                this.txtWeight2.Text = this.mShipment.Pallet2Weight > 0 ? this.mShipment.Pallet2Weight.ToString("#0") : "";
                this.txtInsuranceValue2.Text = this.mShipment.Pallet2InsuranceValue > 0 ? this.mShipment.Pallet2InsuranceValue.ToString("#0") : "";
                this.txtWeight3.Text = this.mShipment.Pallet3Weight > 0 ? this.mShipment.Pallet3Weight.ToString("#0") : "";
                this.txtInsuranceValue3.Text = this.mShipment.Pallet3InsuranceValue > 0 ? this.mShipment.Pallet3InsuranceValue.ToString("#0") : "";
                this.txtWeight4.Text = this.mShipment.Pallet4Weight > 0 ? this.mShipment.Pallet4Weight.ToString("#0") : "";
                this.txtInsuranceValue4.Text = this.mShipment.Pallet4InsuranceValue > 0 ? this.mShipment.Pallet4InsuranceValue.ToString("#0") : "";
                this.txtWeight5.Text = this.mShipment.Pallet5Weight > 0 ? this.mShipment.Pallet5Weight.ToString("#0") : "";
                this.txtInsuranceValue5.Text = this.mShipment.Pallet5InsuranceValue > 0 ? this.mShipment.Pallet5InsuranceValue.ToString("#0") : "";
                this.txtBOLNumber.Text = this.mShipment.BLNumber;
                
                this.chkShipperInsidePickup.Checked = this.mShipment.InsidePickup;
                this.chkShipperLiftGate.Checked = this.mShipment.LiftGateOrigin;
                this.chkShipperAppt.Checked = this.mShipment.PickupAppointmentWindowTimeStart.CompareTo(DateTime.MinValue) > 0;
                OnShipperApptChecked(null, EventArgs.Empty);
                this.chkConsigneeInsidePickup.Checked = this.mShipment.InsideDelivery;
                this.chkConsigneeLiftGate.Checked = this.mShipment.LiftGateDestination;
                this.chkConsigneeAppt.Checked = this.mShipment.DeliveryAppointmentWindowTimeStart.CompareTo(DateTime.MinValue) > 0;
                OnConsigneeApptChecked(null, EventArgs.Empty);

                this.txtPallets.Text = this.mShipment.Pallets.ToString();
                this.txtWeight.Text = this.mShipment.Weight.ToString();
                this.txtRate.Text = "$" + this.mShipment.PalletRate.ToString();
                this.txtFSC.Text = "$" + this.mShipment.FuelSurcharge.ToString();
                this.txtAccessorial.Text = "$" + this.mShipment.AccessorialCharge.ToString();
                this.txtInsurance.Text = "$" + this.mShipment.InsuranceCharge.ToString();
                this.txtTSC.Text = "$" + this.mShipment.TollCharge.ToString();
                this.txtCharges.Text = "$" + this.mShipment.TotalCharge.ToString();

                this.btnQuote.Enabled = this.btnSubmit.Enabled = this.mShipment.ShipDate.CompareTo(DateTime.Today) > 0;
                this.btnCancel.Text = this.mShipment.ShipDate.CompareTo(DateTime.Today) > 0 ? "Cancel" : "Close";
            }
            else {
                this.txtShipDate.Text = getNextValidShipDate().ToString("MM/dd/yyyy");
                OnShipperApptChecked(null, EventArgs.Empty);
                OnConsigneeApptChecked(null, EventArgs.Empty);
                resetQuote();
            }
            this.ddlShippers.Enabled = this.ddlConsignees.Enabled = this.mShipment == null;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnShipperChanged(object sender,EventArgs e) {
        //
        try {
            if(this.ddlShippers.SelectedValue == "New") {
                Response.Redirect("~/Client/Shipper.aspx?number=", false);
            }
            LTLShipper2 shipper = this.ddlShippers.SelectedValue != null ? new FreightGateway().ReadLTLShipper(this.ddlShippers.SelectedValue) : null;
            this.lblShipperAddress.Text = shipper != null ? shipper.AddressLine1.Trim() + "     " + shipper.City.Trim() + ", " + shipper.State + " " + shipper.Zip : "";
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnConsigneeChanged(object sender,EventArgs e) {
        //
        try {
            if(this.ddlConsignees.SelectedValue == "New") {
                Response.Redirect("~/Client/Consignee.aspx?number=", false);
            }
            LTLConsignee2 consignee = this.ddlConsignees.SelectedValue != null ? new FreightGateway().ReadLTLConsignee(int.Parse(this.ddlConsignees.SelectedValue), Master.CurrentClient.Number) : null;
            this.lblConsigneeAddress.Text = consignee != null ? consignee.AddressLine1.Trim() + "     " + consignee.City.Trim() + ", " + consignee.State + " " + consignee.Zip : "";
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnShipperApptChecked(object sender, EventArgs e) {
        //
        try {
            if(this.chkShipperAppt.Checked) {
                //Put shipment actuals if applicable
                if(this.mShipment != null && this.mShipment.ShipmentNumber.Trim().Length > 0) {
                    this.txtShipperApptStart.Text = this.mShipment.PickupAppointmentWindowTimeStart > DateTime.MinValue ? this.mShipment.PickupAppointmentWindowTimeStart.ToString("hh:mm tt") : "10:00 AM";
                    this.txtShipperApptEnd.Text = this.mShipment.PickupAppointmentWindowTimeEnd > DateTime.MinValue ? this.mShipment.PickupAppointmentWindowTimeEnd.ToString("hh:mm tt") : "2:00 PM";
                }
                else {
                    this.txtShipperApptStart.Text = "10:00 AM";
                    this.txtShipperApptEnd.Text = "2:00 PM";
                }
            }
            else
                this.txtShipperApptStart.Text = this.txtShipperApptEnd.Text = "";
            this.pnlShipperAppt.Visible = this.chkShipperAppt.Checked;
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
        finally { resetQuote(); }
    }
    protected void OnConsigneeApptChecked(object sender, EventArgs e) {
        //
        try {
            if(this.chkConsigneeAppt.Checked) {
                //Put shipment actuals if applicable
                if(this.mShipment != null && this.mShipment.ShipmentNumber.Trim().Length > 0) {
                    this.txtConsigneeApptStart.Text = this.mShipment.DeliveryAppointmentWindowTimeStart > DateTime.MinValue ? this.mShipment.DeliveryAppointmentWindowTimeStart.ToString("hh:mm tt") : "10:00 AM";
                    this.txtConsigneeApptEnd.Text = this.mShipment.DeliveryAppointmentWindowTimeEnd > DateTime.MinValue ? this.mShipment.DeliveryAppointmentWindowTimeEnd.ToString("hh:mm tt") : "2:00 PM";
                }
                else {
                    this.txtConsigneeApptStart.Text = "10:00 AM";
                    this.txtConsigneeApptEnd.Text = "2:00 PM";
                }
            }
            else
                this.txtConsigneeApptStart.Text = this.txtConsigneeApptEnd.Text = "";
            this.pnlConsigneeAppt.Visible = this.chkConsigneeAppt.Checked;
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
        finally { resetQuote(); }
    }
    protected void OnValidateForm(object sender, EventArgs e) { }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            if (!Page.IsValid) return;
            switch (e.CommandName) {
                case "Quote":
                    //Validate inputs
                    ApplicationException aex = null;
                    int maxWeight = (this.chkShipperLiftGate.Checked || this.chkConsigneeLiftGate.Checked) ? MAX_LIFTGATE_WEIGHT : MAX_WEIGHT;

                    if (this.txtShipDate.Text.Length == 0) aex = new ApplicationException("Please enter a valid shipment date of the form yyyy-mm-dd.");
                    else if (this.ddlShippers.SelectedIndex < 2) aex = new ApplicationException("Please select a valid shipper.");
                    else if (this.ddlConsignees.SelectedIndex < 2) aex = new ApplicationException("Please select a valid consignee.");
                    else if(this.txtWeight1.Text.Trim().Length == 0 || (int.Parse(this.txtWeight1.Text) < 1 || int.Parse(this.txtWeight1.Text) > maxWeight)) aex = new ApplicationException("Please enter a valid weight for pallet 1 (1 - " + maxWeight + "lbs).");
                    else if(this.txtWeight2.Text.Trim().Length > 0 && (int.Parse(this.txtWeight2.Text) < 1 || int.Parse(this.txtWeight2.Text) > maxWeight)) aex = new ApplicationException("Please enter a valid weight for pallet 2 (1 - " + maxWeight + "lbs).");
                    else if(this.txtWeight3.Text.Trim().Length > 0 && (int.Parse(this.txtWeight3.Text) < 1 || int.Parse(this.txtWeight3.Text) > maxWeight)) aex = new ApplicationException("Please enter a valid weight for pallet 3 (1 - " + maxWeight + "lbs).");
                    else if(this.txtWeight4.Text.Trim().Length > 0 && (int.Parse(this.txtWeight4.Text) < 1 || int.Parse(this.txtWeight4.Text) > maxWeight)) aex = new ApplicationException("Please enter a valid weight for pallet 4 (1 - " + maxWeight + "lbs).");
                    else if(this.txtWeight5.Text.Trim().Length > 0 && (int.Parse(this.txtWeight5.Text) < 1 || int.Parse(this.txtWeight5.Text) > maxWeight)) aex = new ApplicationException("Please enter a valid weight for pallet 5 (1 - " + maxWeight + "lbs).");
                    else if(this.txtInsuranceValue1.Text.Replace("$", "").Trim().Length > 0 && (decimal.Parse(this.txtInsuranceValue1.Text.Replace("$", "")) > MAX_INSURED)) aex = new ApplicationException("Maximum insurance value $" + MAX_INSURED.ToString("##,###.00") + ".");
                    else if(this.txtInsuranceValue2.Text.Replace("$", "").Trim().Length > 0 && (decimal.Parse(this.txtInsuranceValue2.Text.Replace("$", "")) > MAX_INSURED)) aex = new ApplicationException("Maximum insurance value $" + MAX_INSURED.ToString("##,###.00") + ".");
                    else if(this.txtInsuranceValue3.Text.Replace("$", "").Trim().Length > 0 && (decimal.Parse(this.txtInsuranceValue3.Text.Replace("$", "")) > MAX_INSURED)) aex = new ApplicationException("Maximum insurance value $" + MAX_INSURED.ToString("##,###.00") + ".");
                    else if(this.txtInsuranceValue4.Text.Replace("$", "").Trim().Length > 0 && (decimal.Parse(this.txtInsuranceValue4.Text.Replace("$", "")) > MAX_INSURED)) aex = new ApplicationException("Maximum insurance value $" + MAX_INSURED.ToString("##,###.00") + ".");
                    else if(this.txtInsuranceValue5.Text.Replace("$", "").Trim().Length > 0 && (decimal.Parse(this.txtInsuranceValue5.Text.Replace("$", "")) > MAX_INSURED)) aex = new ApplicationException("Maximum insurance value $" + MAX_INSURED.ToString("##,###.00") + ".");
                    if (aex == null) {
                        //Calculate the quote
                        LTLQuote2 quote = new LTLQuote2();
                        #region Create Quote
                        quote.Created = DateTime.Now;
                        quote.ShipDate = DateTime.Parse(this.txtShipDate.Text);
                        quote.ClientID = Master.CurrentClient.ID;
                        quote.ShipperNumber = this.ddlShippers.SelectedValue;
                        quote.ConsigneeNumber = int.Parse(this.ddlConsignees.SelectedValue);
                        quote.Pallet1Weight = int.Parse(this.txtWeight1.Text);
                        quote.Pallet1Class = this.ddlClass1.SelectedValue;
                        quote.Pallet1InsuranceValue = this.txtInsuranceValue1.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue1.Text.Replace("$", "")) : 0.0M;
                        quote.Pallet2Weight = this.txtWeight2.Text.Trim().Length > 0 ? int.Parse(this.txtWeight2.Text) : 0;
                        quote.Pallet2Class = this.ddlClass2.SelectedValue;
                        quote.Pallet2InsuranceValue = this.txtInsuranceValue2.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue2.Text.Replace("$", "")) : 0.0M;
                        quote.Pallet3Weight = this.txtWeight3.Text.Trim().Length > 0 ? int.Parse(this.txtWeight3.Text) : 0;
                        quote.Pallet3Class = this.ddlClass3.SelectedValue;
                        quote.Pallet3InsuranceValue = this.txtInsuranceValue3.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue3.Text.Replace("$", "")) : 0.0M;
                        quote.Pallet4Weight = this.txtWeight4.Text.Trim().Length > 0 ? int.Parse(this.txtWeight4.Text) : 0;
                        quote.Pallet4Class = this.ddlClass4.SelectedValue;
                        quote.Pallet4InsuranceValue = this.txtInsuranceValue4.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue4.Text.Replace("$", "")) : 0.0M;
                        quote.Pallet5Weight = this.txtWeight5.Text.Trim().Length > 0 ? int.Parse(this.txtWeight5.Text) : 0;
                        quote.Pallet5Class = this.ddlClass5.SelectedValue;
                        quote.Pallet5InsuranceValue = this.txtInsuranceValue5.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue5.Text.Replace("$", "")) : 0.0M;
                        quote.InsidePickup = this.chkShipperInsidePickup.Checked;
                        quote.LiftGateOrigin = this.chkShipperLiftGate.Checked;
                        quote.AppointmentOrigin = this.chkShipperAppt.Checked;
                        quote.InsideDelivery = this.chkConsigneeInsidePickup.Checked;
                        quote.LiftGateDestination = this.chkConsigneeLiftGate.Checked;
                        quote.AppointmentDestination = this.chkConsigneeAppt.Checked;
                        quote.Pallets = 0;
                        quote.Weight = 0;
                        quote.PalletRate = 0;
                        quote.FuelSurcharge = 0;
                        quote.AccessorialCharge = 0;
                        quote.InsuranceCharge = 0;
                        quote.TollCharge = 0;
                        quote.TotalCharge = 0;
                        #endregion
                        quote = new FreightGateway().CreateQuote(quote);

                        //Display the quote
                        this.lblEstimatedDeliveryDate.Text = "Estimated delivery by " + quote.EstimatedDeliveryDate.ToString("MM-dd-yyyy");
                        this.txtPallets.Text = quote.Pallets.ToString();
                        this.txtWeight.Text = quote.Weight.ToString();
                        this.txtRate.Text = "$" + quote.PalletRate.ToString();
                        this.txtFSC.Text = "$" + quote.FuelSurcharge.ToString();
                        this.txtAccessorial.Text = "$" + quote.AccessorialCharge.ToString();
                        this.txtInsurance.Text = "$" + quote.InsuranceCharge.ToString();
                        this.txtTSC.Text = "$" + quote.TollCharge.ToString();
                        this.txtCharges.Text = "$" + quote.TotalCharge.ToString();

                        //Enable booking
                        this.btnSubmit.Enabled = true;
                    }
                    else {
                        resetQuote();
                        Master.ReportError(aex,3);
                    }
                    break;
                case "Submit":
                    if (this.mShipment == null) {
                        //New
                        LTLShipment2 shipment = new LTLShipment2();
                        #region Create shipment
                        shipment.Created = DateTime.Now;
                        shipment.ClientNumber = Master.CurrentClient.Number;
                        shipment.ShipDate = DateTime.Parse(this.txtShipDate.Text);
                        shipment.ShipperNumber = this.ddlShippers.SelectedValue;
                        shipment.ConsigneeNumber = int.Parse(this.ddlConsignees.SelectedValue);
                        shipment.Pallet1Weight = int.Parse(this.txtWeight1.Text);
                        shipment.Pallet1Class = this.ddlClass1.SelectedValue;
                        shipment.Pallet1InsuranceValue = this.txtInsuranceValue1.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue1.Text.Replace("$", "")) : 0.0M;
                        shipment.Pallet2Weight = this.txtWeight2.Text.Trim().Length > 0 ? int.Parse(this.txtWeight2.Text) : 0;
                        shipment.Pallet2Class = this.ddlClass2.SelectedValue;
                        shipment.Pallet2InsuranceValue = this.txtInsuranceValue2.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue2.Text.Replace("$", "")) : 0.0M;
                        shipment.Pallet3Weight = this.txtWeight3.Text.Trim().Length > 0 ? int.Parse(this.txtWeight3.Text) : 0;
                        shipment.Pallet3Class = this.ddlClass3.SelectedValue;
                        shipment.Pallet3InsuranceValue = this.txtInsuranceValue3.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue3.Text.Replace("$", "")) : 0.0M;
                        shipment.Pallet4Weight = this.txtWeight4.Text.Trim().Length > 0 ? int.Parse(this.txtWeight4.Text) : 0;
                        shipment.Pallet4Class = this.ddlClass4.SelectedValue;
                        shipment.Pallet4InsuranceValue = this.txtInsuranceValue4.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue4.Text.Replace("$", "")) : 0.0M;
                        shipment.Pallet5Weight = this.txtWeight5.Text.Trim().Length > 0 ? int.Parse(this.txtWeight5.Text) : 0;
                        shipment.Pallet5Class = this.ddlClass5.SelectedValue;
                        shipment.Pallet5InsuranceValue = this.txtInsuranceValue5.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue5.Text.Replace("$", "")) : 0.0M;
                        shipment.BLNumber = this.txtBOLNumber.Text;
                        
                        shipment.InsidePickup = this.chkShipperInsidePickup.Checked;
                        shipment.LiftGateOrigin = this.chkShipperLiftGate.Checked;
                        if(this.chkShipperAppt.Checked) {
                            shipment.PickupAppointmentWindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtShipperApptStart.Text);
                            shipment.PickupAppointmentWindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtShipperApptEnd.Text);
                        }
                        shipment.InsideDelivery = this.chkConsigneeInsidePickup.Checked;
                        shipment.LiftGateDestination = this.chkConsigneeLiftGate.Checked;
                        if(this.chkConsigneeAppt.Checked) {
                            shipment.DeliveryAppointmentWindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtConsigneeApptStart.Text);
                            shipment.DeliveryAppointmentWindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtConsigneeApptEnd.Text);
                        }
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
                        #endregion
                        string number = new FreightGateway().CreateLTLShipment(shipment);
                        Master.ShowMessageBox("New shipment " + shipment.ShipmentNumber + " has been created.");
                    }
                    else {
                        //Update
                        #region Update shipment
                        this.mShipment.ShipDate = DateTime.Parse(this.txtShipDate.Text);
                        this.mShipment.Pallet1Weight = int.Parse(this.txtWeight1.Text);
                        this.mShipment.Pallet1Class = this.ddlClass1.SelectedValue;
                        this.mShipment.Pallet1InsuranceValue = this.txtInsuranceValue1.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue1.Text.Replace("$", "")) : 0.0M;
                        this.mShipment.Pallet2Weight = this.txtWeight2.Text.Trim().Length > 0 ? int.Parse(this.txtWeight2.Text) : 0;
                        this.mShipment.Pallet2Class = this.ddlClass2.SelectedValue;
                        this.mShipment.Pallet2InsuranceValue = this.txtInsuranceValue2.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue2.Text.Replace("$", "")) : 0.0M;
                        this.mShipment.Pallet3Weight = this.txtWeight3.Text.Trim().Length > 0 ? int.Parse(this.txtWeight3.Text) : 0;
                        this.mShipment.Pallet3Class = this.ddlClass3.SelectedValue;
                        this.mShipment.Pallet3InsuranceValue = this.txtInsuranceValue3.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue3.Text.Replace("$", "")) : 0.0M;
                        this.mShipment.Pallet4Weight = this.txtWeight4.Text.Trim().Length > 0 ? int.Parse(this.txtWeight4.Text) : 0;
                        this.mShipment.Pallet4Class = this.ddlClass4.SelectedValue;
                        this.mShipment.Pallet4InsuranceValue = this.txtInsuranceValue4.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue4.Text.Replace("$", "")) : 0.0M;
                        this.mShipment.Pallet5Weight = this.txtWeight5.Text.Trim().Length > 0 ? int.Parse(this.txtWeight5.Text) : 0;
                        this.mShipment.Pallet5Class = this.ddlClass5.SelectedValue;
                        this.mShipment.Pallet5InsuranceValue = this.txtInsuranceValue5.Text.Replace("$", "").Trim().Length > 0 ? decimal.Parse(this.txtInsuranceValue5.Text.Replace("$", "")) : 0.0M;
                        this.mShipment.InsidePickup = this.chkShipperInsidePickup.Checked;
                        this.mShipment.LiftGateOrigin = this.chkShipperLiftGate.Checked;
                        if(this.chkShipperAppt.Checked) {
                            this.mShipment.PickupAppointmentWindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtShipperApptStart.Text);
                            this.mShipment.PickupAppointmentWindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtShipperApptEnd.Text);
                        }
                        this.mShipment.InsideDelivery = this.chkConsigneeInsidePickup.Checked;
                        this.mShipment.LiftGateDestination = this.chkConsigneeLiftGate.Checked;
                        if(this.chkConsigneeAppt.Checked) {
                            this.mShipment.DeliveryAppointmentWindowTimeStart = DateTime.Parse("01-01-2000 " + this.txtConsigneeApptStart.Text);
                            this.mShipment.DeliveryAppointmentWindowTimeEnd = DateTime.Parse("01-01-2000 " + this.txtConsigneeApptEnd.Text);
                        }
                        this.mShipment.Pallets = int.Parse(this.txtPallets.Text);
                        this.mShipment.Weight = decimal.Parse(this.txtWeight.Text);
                        this.mShipment.PalletRate = decimal.Parse(this.txtRate.Text.Replace("$",""));
                        this.mShipment.FuelSurcharge = decimal.Parse(this.txtFSC.Text.Replace("$",""));
                        this.mShipment.AccessorialCharge = decimal.Parse(this.txtAccessorial.Text.Replace("$",""));
                        this.mShipment.InsuranceCharge = decimal.Parse(this.txtInsurance.Text.Replace("$",""));
                        this.mShipment.TollCharge = decimal.Parse(this.txtTSC.Text.Replace("$",""));
                        this.mShipment.TotalCharge = decimal.Parse(this.txtCharges.Text.Replace("$",""));
                        this.mShipment.UserID = Membership.GetUser().UserName;
                        this.mShipment.LastUpdated = DateTime.Now;
                        #endregion
                        bool updated = new FreightGateway().UpdateLTLShipment(this.mShipment);
                        Master.ShowMessageBox("Shipment " + this.mShipment.ShipmentNumber + " has been updated.");
                    }
                    this.btnSubmit.Enabled = false;
                    this.btnCancel.Text = "Close";
                    break;
                case "Cancel":
                    Response.Redirect("~/Client/Shipments.aspx",false);
                    break;
            }
        }
        catch(ApplicationException ex) { Master.ReportError(ex, 3); }
        catch(Exception ex) { Master.ReportError(ex, 4); }
    }

    private DateTime getNextValidShipDate() {
        //Determine next valid ship date (i.e. no weekends)
        DateTime shipDate = DateTime.Today.AddDays(1);
        //if(shipDate.DayOfWeek == DayOfWeek.Friday) shipDate.AddDays(3);
        if(shipDate.DayOfWeek == DayOfWeek.Saturday) shipDate.AddDays(2);
        if(shipDate.DayOfWeek == DayOfWeek.Sunday) shipDate.AddDays(1);
        return shipDate;
    }
    private void resetQuote() {
        //
        this.txtPallets.Text = this.txtWeight.Text = this.txtRate.Text = this.txtFSC.Text = this.txtAccessorial.Text = this.txtInsurance.Text = this.txtTSC.Text = this.txtCharges.Text = "";
        this.upnlResponse.Update();
        this.btnSubmit.Enabled = false;
    }
}
