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
    public int mView = 0;

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "client";
                switch(view) {
                    case "client": this.mView = 0; break;
                    case "shippers": this.mView = 1; break;
                    case "consignees": this.mView = 2; break;
                }
                ViewState.Add("View", this.mView);

                //Setup UI
                OnCurrentClientChanged(null, EventArgs.Empty);
            }
            else {
                this.mView = Convert.ToInt32(ViewState["View"]);
            }
            Master.CurrentClientChanged += new EventHandler(OnCurrentClientChanged);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnCurrentClientChanged(object sender,EventArgs e) {
        try {
            //Set client account status
            if (Master.CurrentClient.DenialDate.CompareTo(DateTime.MinValue) > 0)
                this.cvStatus.ErrorMessage = "** Your account has been rejected.";
            else if (Master.CurrentClient.ApprovalDate.CompareTo(DateTime.MinValue) == 0 && Master.CurrentClient.DenialDate.CompareTo(DateTime.MinValue) == 0)
                this.cvStatus.ErrorMessage = "** Your account is currently under review by our Finance department.";
            this.cvStatus.IsValid = Master.CurrentClientApproved;

            //Password change enabled for logged in user only
            this.lnkChangePassword.Visible = Master.CurrentClient.ID == Master.Client.ID;

            //Load information
            this.lblClientName.Text = Master.CurrentClient.Name;
            //this.lblID.Text = Master.CurrentClient.ID.ToString();
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
            this.txtTaxID.Text = Master.CurrentClient.TaxID;
            this.txtBillingStreet.Text = Master.CurrentClient.BillingAddressLine1;
            this.txtBillingCity.Text = Master.CurrentClient.BillingCity;
            this.txtBillingState.Text = Master.CurrentClient.BillingState;
            this.txtBillingZip.Text = Master.CurrentClient.BillingZip;

            this.odsShippers.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
            this.grdShippers.DataBind();

            this.odsConsignees.SelectParameters["clientNumber"].DefaultValue = Master.CurrentClient.Number;
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
            this.btnShipperNew.Enabled = Master.CurrentClientApproved;
            this.btnShipperUpdate.Enabled = Master.CurrentClientApproved && this.grdShippers.SelectedRow != null;
            this.btnConsigneeNew.Enabled = Master.CurrentClientApproved;
            this.btnConsigneeUpdate.Enabled = Master.CurrentClientApproved && this.grdConsignees.SelectedRow != null;
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnManageCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "AccountUpdate":
                    ApplicationException aex = null;
                    //if (this.txtTaxID.Text.Length != 10) aex = new ApplicationException("Please enter a valid federal taxID number of the form xx-xxxxxxx.");
                    if (aex == null) {
                        //Get the current client email and id
                        string oldEmail = Master.CurrentClient.ContactEmail;

                        //Update the current client
                        LTLClient2 client = new LTLClient2();
                        client.ID = Master.CurrentClient.ID;    // int.Parse(this.lblID.Text);
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
                        client.TaxID = this.txtTaxID.Text;
                        client.BillingAddressLine1 = this.txtBillingStreet.Text;
                        client.BillingAddressLine2 = "";
                        client.BillingCity = this.txtBillingCity.Text;
                        client.BillingState = this.txtBillingState.Text;
                        client.BillingZip = this.txtBillingZip.Text;
                        client.BillingZip4 = "";
                        client.UserID = Master.User.UserName;
                        client.LastUpdated = DateTime.Now;
                        //if(Master.SalesRep != null) client.SalesRepClientID = Master.SalesRep.ID;
                        bool updated = new FreightGateway().UpdateLTLClient(client);

                        //Update Membership account email
                        MembershipUserCollection members = Membership.FindUsersByEmail(oldEmail);
                        foreach (MembershipUser member in members) {
                            ProfileCommon profile = Profile.GetProfile(member.UserName);
                            //if (profile.ClientID == this.lblID.Text && member.Email == oldEmail) {
                            if(profile.ClientID == Master.CurrentClient.ID.ToString() && member.Email == oldEmail) {
                                member.Email = this.txtContactEmail.Text;
                                //member.ChangePassword(oldEmail,this.txtContactEmail.Text);
                                Membership.UpdateUser(member);
                                break;
                            }
                        }

                        //Update Master.CurentClient
                        Master.OnCurrentClientChanged(null,EventArgs.Empty);

                        //Confirmation
                        Master.ShowMessageBox("Client " + client.Name + " has been updated.");
                    }
                    else {
                        Master.ReportError(aex, 3);
                    }
                    break;
                case "ShipperNew":
                    Response.Redirect("~/Client/Shipper.aspx?number=",false);
                    break;
                case "ShipperUpdate":
                    Response.Redirect("~/Client/Shipper.aspx?number=" + this.grdShippers.SelectedDataKey.Value.ToString(),false);
                    break;
                case "ConsigneeNew":
                    Response.Redirect("~/Client/Consignee.aspx?number=",false);
                    break;
                case "ConsigneeUpdate":
                    Response.Redirect("~/Client/Consignee.aspx?number=" + this.grdConsignees.SelectedDataKey.Value.ToString(),false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
