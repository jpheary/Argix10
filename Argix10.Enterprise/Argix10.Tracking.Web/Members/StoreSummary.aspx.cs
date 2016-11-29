using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using Argix.Enterprise;

public partial class _StoreSummary :System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //Display search by store summary information
                TrackingStoreItems items = null;
                if(Session["StoreSummary"] is TrackingStoreItems) items = (TrackingStoreItems)Session["StoreSummary"];
                if (items != null && items.Count > 0) {
                    //Title
                    this.lblTitle.Text = "Store# " + items[0].Store.PadLeft(5,'0');
                    this.grdSummary.DataSource = items;
                    this.grdSummary.DataBind();
                }
                else
                    Master.ReportError(new ApplicationException("Could not find store summary information. Please return to the tracking page and try again."), 4);
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    public string GetOFDorPOD(object ofd,object poddate,object podtime) {
        if (poddate != DBNull.Value && DateTime.Parse(poddate.ToString()) != DateTime.MinValue)
            return DateTime.Parse(poddate.ToString()).ToShortDateString() + " " + DateTime.Parse(podtime.ToString()).ToShortTimeString();
        else {
            if (ofd != DBNull.Value && DateTime.Parse(ofd.ToString()) != DateTime.MinValue)
                return DateTime.Parse(ofd.ToString()).ToShortDateString();
            else
                return "";
        }
    }
}