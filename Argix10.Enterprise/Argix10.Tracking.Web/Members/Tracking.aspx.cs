using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Argix.Enterprise;

public partial class _Tracking : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        try {
            bool hasStoreNumber = new MembershipServices().MemberProfile.StoreNumber.Trim().Length > 0;
            string url = hasStoreNumber ? "~/Members/TrackByStore.aspx" : "~/Members/TrackByCarton.aspx";
            if (Session["TrackBy"] != null) {
                switch (Session["TrackBy"].ToString()) {
                    case TrackingGateway.SEARCHBY_CARTONNUMBER:
                    case TrackingGateway.SEARCHBY_LABELNUMBER:
                    case TrackingGateway.SEARCHBY_PLATENUMBER:
                        url = "~/Members/TrackByCarton.aspx";
                        break;
                    case TrackingGateway.SEARCHBY_PO:
                    case TrackingGateway.SEARCHBY_PRO:
                    case TrackingGateway.SEARCHBY_BOL:
                        url = "~/Members/TrackByShipment.aspx";
                        break;
                    case TrackingGateway.SEARCHBY_STORE:
                        url = "~/Members/TrackByStore.aspx";
                        break;
                }
            }
            Response.Redirect(url,false);
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
}