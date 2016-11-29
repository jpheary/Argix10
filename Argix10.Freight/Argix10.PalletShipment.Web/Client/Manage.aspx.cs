using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Manage:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Starting tab?
                string view =  Request.QueryString["view"] != null ? Request.QueryString["view"] : "client";
                OnChangeView(null,new CommandEventArgs(view,null));

                //Setup UI
                OnCurrentClientChanged(null,EventArgs.Empty);
            }
            Master.CurrentClientChanged += new EventHandler(OnCurrentClientChanged);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "client":
                    this.mvwPage.ActiveViewIndex = 0;
                    this.liClient.Style["border-bottom-style"] = "none";
                    this.liShippers.Style["border-bottom-style"] = "solid";
                    this.liConsignees.Style["border-bottom-style"] = "solid";
                    break;
                case "shippers":
                    this.mvwPage.ActiveViewIndex = 1;
                    this.liClient.Style["border-bottom-style"] = "solid";
                    this.liShippers.Style["border-bottom-style"] = "none";
                    this.liConsignees.Style["border-bottom-style"] = "solid";
                    break;
                case "consignees":
                    this.mvwPage.ActiveViewIndex = 2;
                    this.liClient.Style["border-bottom-style"] = "solid";
                    this.liShippers.Style["border-bottom-style"] = "solid";
                    this.liConsignees.Style["border-bottom-style"] = "none";
                    break;
            }
            //this.liShippers.Style["border-top-style"] = this.liConsignees.Style["border-top-style"] = "none";
            //this.liShippers.Style["border-right-style"] = this.liConsignees.Style["border-right-style"] = "none";
            //this.liShippers.Style["border-bottom-style"] = this.liConsignees.Style["border-bottom-style"] = "solid";
            //this.liShippers.Style["border-left-style"] = this.liConsignees.Style["border-left-style"] = "none";
            this.liBlank1.Style["border-top-style"] = this.liBlank2.Style["border-top-style"] = "none";
            this.liBlank1.Style["border-right-style"] = this.liBlank2.Style["border-right-style"] = "none";
            this.liBlank1.Style["border-bottom-style"] = this.liBlank2.Style["border-bottom-style"] = "solid";
            this.liBlank1.Style["border-left-style"] = this.liBlank2.Style["border-left-style"] = "none";
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnCurrentClientChanged(object sender,EventArgs e) {
        try {
            //Set client account status
            this.cvStatus.ErrorMessage = Master.CurrentClient.Approved ? "" : "** Your account is currently under review by our Finance department.";
            this.cvStatus.IsValid = Master.CurrentClient.Approved;

            //Password change enabled for logged in user only
            this.lnkChangePassword.Visible = Master.CurrentClient.ID == Master.Client.ID;

            //Load information
            this.lblID.Text = Master.CurrentClient.ID.ToString();
            this.txtCompanyName.Text = Master.CurrentClient.Name;
            this.txtCompanyStreet.Text = Master.CurrentClient.AddressLine1;
            this.txtCompanyCity.Text = Master.CurrentClient.City;
            this.txtCompanyState.Text = Master.CurrentClient.State;
            this.txtCompanyZip.Text = Master.CurrentClient.Zip;
            this.txtContactName.Text = Master.CurrentClient.ContactName;
            this.txtContactPhone.Text = Master.CurrentClient.ContactPhone;
            this.txtContactEmail.Text = Master.CurrentClient.ContactEmail;
            this.txtCorporateName.Text = Master.CurrentClient.CorporateName;
            this.txtCorporateStreet.Text = Master.CurrentClient.CorporateAddressLine1;
            this.txtCorporateCity.Text = Master.CurrentClient.CorporateCity;
            this.txtCorporateState.Text = Master.CurrentClient.CorporateState;
            this.txtCorporateZip.Text = Master.CurrentClient.CorporateZip;
            this.txtTaxIDNumber.Text = Master.CurrentClient.TaxIDNumber;
            this.txtBillingStreet.Text = Master.CurrentClient.BillingAddressLine1;
            this.txtBillingCity.Text = Master.CurrentClient.BillingCity;
            this.txtBillingState.Text = Master.CurrentClient.BillingState;
            this.txtBillingZip.Text = Master.CurrentClient.BillingZip;

            this.lblShippersClient.Text = Master.CurrentClient.Name;
            this.odsShippers.SelectParameters["clientID"].DefaultValue = Master.CurrentClient.ID.ToString();
            this.grdShippers.DataBind();

            this.lblConsigneesClient.Text = Master.CurrentClient.Name;
            this.odsConsignees.SelectParameters["clientID"].DefaultValue = Master.CurrentClient.ID.ToString();
            this.grdConsignees.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnShipperSelected(object sender,EventArgs e) { OnValidateForm(null,EventArgs.Empty); }
    protected void OnConsigneeSelected(object sender,EventArgs e) { OnValidateForm(null,EventArgs.Empty); }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
            this.btnAccountUpdate.Enabled = true;
            this.btnShipperNew.Enabled = Master.CurrentClient.Approved;
            this.btnShipperUpdate.Enabled = Master.CurrentClient.Approved && this.grdShippers.SelectedRow != null;
            this.btnConsigneeNew.Enabled = Master.CurrentClient.Approved;
            this.btnConsigneeUpdate.Enabled = Master.CurrentClient.Approved && this.grdConsignees.SelectedRow != null;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnManageCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "AccountUpdate":
                    ApplicationException aex = null;
                    //if (this.txtTaxIDNumber.Text.Length != 10) aex = new ApplicationException("Please enter a valid federal taxID number of the form xx-xxxxxxx.");
                    if (aex == null) {
                        LTLClient client = new LTLClient();
                        client.ID = int.Parse(this.lblID.Text);
                        client.Name = this.txtCompanyName.Text;
                        client.AddressLine1 = this.txtCompanyStreet.Text;
                        client.AddressLine2 = "";
                        client.City = this.txtCompanyCity.Text;
                        client.State = this.txtCompanyState.Text;
                        client.Zip = this.txtCompanyZip.Text;
                        client.Zip4 = "";
                        client.ContactName = this.txtContactName.Text;
                        client.ContactPhone = this.txtContactPhone.Text;
                        client.ContactEmail = this.txtContactEmail.Text;
                        client.CorporateName = this.txtCorporateName.Text;
                        client.CorporateAddressLine1 = this.txtCorporateStreet.Text;
                        client.CorporateAddressLine2 = "";
                        client.CorporateCity = this.txtCorporateCity.Text;
                        client.CorporateState = this.txtCorporateState.Text;
                        client.CorporateZip = this.txtCorporateZip.Text;
                        client.CorporateZip4 = "";
                        client.TaxIDNumber = this.txtTaxIDNumber.Text;
                        client.BillingAddressLine1 = this.txtBillingStreet.Text;
                        client.BillingAddressLine2 = "";
                        client.BillingCity = this.txtBillingCity.Text;
                        client.BillingState = this.txtBillingState.Text;
                        client.BillingZip = this.txtBillingZip.Text;
                        client.BillingZip4 = "";
                        client.Status = "A";
                        client.UserID = Master.User.UserName;
                        client.LastUpdated = DateTime.Now;
                        //if(Master.SalesRep != null) client.SalesRepClientID = Master.SalesRep.ID;
                        bool updated = new FreightGateway().UpdateLTLClient(client);

                        //Update Membership account email
                        //MembershipUserCollection members = Membership.FindUsersByEmail(Master.CurrentClient.ContactEmail);
                        //foreach (MembershipUser member in members) {
                        //    if (member.Email == Master.CurrentClient.ContactEmail) {
                        //        member.Email = this.txtContactEmail.Text;
                        //        break;
                        //    }
                        //}

                        //Confirmation
                        Master.ShowMessageBox("Client " + client.Name + " has been updated.");
                    }
                    else {
                        Master.ReportError(aex, 3);
                    }
                    break;
                case "ShipperNew":
                    Response.Redirect("~/Client/Shipper.aspx?id=0",false);
                    break;
                case "ShipperUpdate":
                    Response.Redirect("~/Client/Shipper.aspx?id=" + this.grdShippers.SelectedDataKey.Value.ToString(),false);
                    break;
                case "ConsigneeNew":
                    Response.Redirect("~/Client/Consignee.aspx?id=0",false);
                    break;
                case "ConsigneeUpdate":
                    Response.Redirect("~/Client/Consignee.aspx?id=" + this.grdConsignees.SelectedDataKey.Value.ToString(),false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
