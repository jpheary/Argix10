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
using System.Diagnostics;
using System.Xml;
using Argix.Enterprise;

public partial class _TrackByShipment:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler fro page load event
        try {
            if(!Page.IsPostBack) {
                if (Session["TrackBy"] != null) this.cboTrackBy.SelectedValue = Session["TrackBy"].ToString();
                this.cboClient.DataBind();
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnTrack(object sender,EventArgs e) {
        //Track by PO or PRO number
        try {
            if(Page.IsValid) {
                //Flag search by method
                string trackBy = TrackingGateway.SEARCHBY_PO;
                switch (this.cboTrackBy.SelectedValue) {
                    case "PONumber": trackBy = TrackingGateway.SEARCHBY_PO; break;
                    case "PRONumber": trackBy = TrackingGateway.SEARCHBY_PRO; break;
                    case "BOLNumber": trackBy = TrackingGateway.SEARCHBY_BOL; break;
                }
                Session["TrackBy"] = trackBy;

                //Track
                TrackingItems items = new TrackingGateway().TrackCartonsByShipment(this.cboClient.SelectedValue,this.txtNumber.Text,trackBy);
                Session["TrackData"] = items;
                if (items != null && items.Count > 0) {
                    if (items.Count == 1)
                        Response.Redirect("CartonDetail.aspx?item=" + items[0].LabelNumber.Trim(),false);
                    else
                        Response.Redirect("CartonSummary.aspx",false);
                }
                else
                    Master.ShowMessageBox("No records found. Please try again.");
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
}
