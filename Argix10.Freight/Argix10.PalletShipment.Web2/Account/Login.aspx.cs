using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Account_Login : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //
        if (!Page.IsPostBack) {
            this.LoginUser.FindControl("UserName").Focus();
        }
    }
    protected void OnLoginError(object sender, EventArgs e) {
        //
        try {
            Master.ShowMessageBox("Your login attempt was unsuccessful; please try again.");
            this.LoginUser.FindControl("Password").Focus();
        }
        catch(Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnLoggedIn(object sender,EventArgs e) {
        //
        try {
            MembershipUser user = Membership.GetUser(this.LoginUser.UserName);
            Session["user"] = user;
            if (user != null) {
                //Get the client account for the current login
                LTLClient2 client = new FreightGateway().ReadLTLClient(int.Parse(Profile.GetProfile(user.UserName).ClientID));
                Session["client"] = client;
                Session["currentclient"] = client;

                //Get the sales rep for the current client (if applicable)
                if (new MembershipServices(this.LoginUser.UserName).IsAdmin) {
                    //Load list of all clients for admin
                    LTLClientDataset clients = new FreightGateway().GetLTLClientList(null);
                    Session["clients"] = clients;
                }
                else if (new MembershipServices(this.LoginUser.UserName).IsSalesRep) {
                    //Sales rep for current client
                    Session["salesrep"] = client;

                    //Load list of clients for sales rep
                    LTLClientDataset clients = new FreightGateway().GetLTLClientList(client.Number);
                    Session["clients"] = clients;
                }
                else if (new MembershipServices(this.LoginUser.UserName).IsClient && client.SalesRepClientNumber.Length > 0) {
                    //Client with sales rep
                    LTLClient2 salesRep = new FreightGateway().ReadLTLClientByNumber(client.SalesRepClientNumber);
                    Session["salesrep"] = salesRep;
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}
