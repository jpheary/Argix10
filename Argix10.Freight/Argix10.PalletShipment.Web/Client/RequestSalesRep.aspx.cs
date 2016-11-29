using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Client_RequestSalesRep : System.Web.UI.Page {
    //
    protected void Page_Load(object sender, EventArgs e) {
        //
    }
    protected void OnRequestSalesRep(object sender,EventArgs e) {
        //
        try {
            new NotifyService().NotifySalesRepRequest(Master.Client);
            Master.ShowMessageBox("Your request has been submitted to an Argix representative.");
        }
        catch (Exception ex) { Master.ReportError(ex,4); }
    }
}