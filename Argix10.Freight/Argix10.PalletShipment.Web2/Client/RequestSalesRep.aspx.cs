using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Client_RequestSalesRep : System.Web.UI.Page {
    //Members
    private string mCallingURI = "";

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                this.mCallingURI = Page.Request.UrlReferrer.AbsoluteUri.Split('?')[0];
                ViewState.Add("CallingURI",this.mCallingURI);
            }
            else {
                this.mCallingURI = ViewState["CallingURI"].ToString();
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Submit":
                    new NotifyService().NotifySalesRepRequest(Master.Client);
                    Master.ShowMessageBox("Your request has been submitted to an Argix representative.");
                   break;
                case "Cancel":
                    Response.Redirect(this.mCallingURI + "?view=shippers",false);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}