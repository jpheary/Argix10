using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Enroll : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
            }
            else {
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnSubmit(object sender,EventArgs e) {
        //
        if (!Page.IsValid) return;
        try {
            //Validate
            ApplicationException aex = null;
            //if (this.txtTaxIDNumber.Text.Length != 10) aex = new ApplicationException("Please enter a valid federal taxID number of the form xx-xxxxxxx.");
            if(aex == null) {
                //Set all objects before creating anything
                LTLClient client = new LTLClient();
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
                client.Approved = false;
                client.Status = "A";
                client.UserID = this.txtUserName.Text;
                client.LastUpdated = DateTime.Today;

                //Is this created by a salesRep? Then set sales rep reference on new client
                if (Master.SalesRep != null) client.SalesRepClientID = Master.SalesRep.ID;

                //Create a new membership account
                MembershipCreateStatus status = MembershipCreateStatus.Success;
                Membership.CreateUser(this.txtUserName.Text,this.txtPassword.Text,this.txtContactEmail.Text,null,null,true,out status);
                switch (status) {
                    case MembershipCreateStatus.Success:
                        //Add user to the client role
                        Roles.AddUserToRole(this.txtUserName.Text,"client");

                        //If this fails rollback the new login
                        try {
                            //Create the client
                            int clientID = new FreightGateway().CreateLTLClient(client);
                            client.ID = clientID;   //Add the ID for enrolling shippers, consignees, etc

                            //Create user profile and set properties value
                            ProfileCommon profile = Profile.GetProfile(this.txtUserName.Text);
                            profile.ClientID = clientID.ToString();
                            profile.Save();

                            //Send welcome message to new client and sales rep (if a sales rep created this enrollment)
                            new NotifyService().NotifyWelcome(this.txtUserName.Text,this.txtContactEmail.Text,this.txtPassword.Text,client);
                            if (Master.SalesRep != null) new NotifyService().NotifyWelcome(this.txtUserName.Text,Master.SalesRep.ContactEmail,this.txtPassword.Text,client);

                            //Update client list if there is one (admin or sales rep)
                            if (Master.Clients != null) {
                                if (new MembershipServices().IsAdmin)
                                    Session["clients"] = new FreightGateway().GetLTLClientList(0);
                                else if (new MembershipServices().IsSalesRep)
                                    Session["clients"] = new FreightGateway().GetLTLClientList(Master.Client.ID);
                            }

                            //Go to pickup log
                            Response.Redirect("~/Client/Shipments.aspx",false);
                        }
                        catch (Exception ex) {
                            Membership.DeleteUser(this.txtUserName.Text);
                            Master.ReportError(ex,4); 
                        }
                        break;
                    case MembershipCreateStatus.DuplicateUserName:
                        Master.ReportError(new ApplicationException("This login username already exists."),3);
                        break;
                    case MembershipCreateStatus.InvalidPassword:
                        Master.ReportError(new ApplicationException("Password should be minimum 6 characters long."),3);
                        break;
                    case MembershipCreateStatus.UserRejected:
                        break;
                }
            }
            else {
                Master.ReportError(aex, 3);
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
