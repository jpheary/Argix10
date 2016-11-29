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
using System.Text;
using Argix.Enterprise;

public partial class _CartonSummary:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        try {
            if(!Page.IsPostBack) {
                //
                TrackingItems items = null;
                if (Session["TrackData"] is TrackingItems) items = (TrackingItems)Session["TrackData"];
                if (items != null && items.Count > 0) {
                    //Title
                    this.lblTitle.Text = items.Count.ToString() + " cartons";
                    try {
                        if (Session["TrackBy"].ToString() == TrackingGateway.SEARCHBY_PRO) 
                            this.lblTitle.Text = "PRO# " + items[0].ShipmentNumber + " (" + items.Count.ToString() + " cartons)";
                        if (Session["TrackBy"].ToString() == TrackingGateway.SEARCHBY_PO) 
                            this.lblTitle.Text = "PO# " + items[0].PONumber + " (" + items.Count.ToString() + " cartons)";
                    }
                    catch { }
                    this.grdTrack.DataSource = items;
                    this.grdTrack.DataBind();
                }
                else
                    Master.ReportError(new ApplicationException("Could not find summary information. Please return to the tracking page and try again."), 4);
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected virtual void OnItemDataBound(object sender,GridViewRowEventArgs e) {
        //
        try {
            if (e.Row.RowType == DataControlRowType.DataRow) {
                string labelNumber = this.grdTrack.DataKeys[e.Row.RowIndex].Value.ToString();
                if (labelNumber.Length == 0) ((HyperLink)e.Row.Cells[0].Controls[0]).NavigateUrl = "";
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
}
