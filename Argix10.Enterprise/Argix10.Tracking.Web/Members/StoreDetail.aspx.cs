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

public partial class _StoreDetail :System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            string tl = Request.QueryString["TL"];
            string lbl = Request.QueryString["LBL"];
            string ctn = Request.QueryString["CTN"];
            if(!Page.IsPostBack) {
                //Page request
                if(tl != null && tl.Length > 0 && lbl == null && ctn == null) {
                    //Page request from StoreSummary.aspx (i.e. StoreDetail.aspx?TL=)
                    //Show TL detail
                    TrackingStoreItems items = null;
                    if (Session["StoreDetail"] is TrackingStoreItems) items = (TrackingStoreItems)Session["StoreDetail"];
                    if (items != null && items.Count > 0) {
                        TrackingStoreItems _items = new TrackingStoreItems();
                        foreach (Argix.Enterprise.TrackingStoreItem item in items) {
                            if (item.TL == tl) _items.Add(item);
                        }

                        this.lblTitle.Text = "Store# " + _items[0].Store.PadLeft(5, '0') + "   TL# " + tl;
                        this.grdTLDetail.DataSource = _items;
                        this.grdTLDetail.DataBind();
                    }
                    else
                        Master.ReportError(new ApplicationException("Could not find detailed store information. Please return to the tracking page and try again."), 4);
                }
                else if(lbl != null && lbl.Length > 0 && ctn != null && ctn.Length > 0 && tl != null && tl.Length > 0) {
                    //Page request (NOT a postback) from this page (i.e. StoreDetail.aspx?CTN=&LBL=&TL=)
                    Session["TrackData"] = null;
                    ProfileCommon profile = new MembershipServices().MemberProfile;
                    TrackingItems items = new TrackingGateway().TrackCartons(new string[] { lbl },TrackingGateway.SEARCHBY_LABELNUMBER,profile.Type,profile.ClientVendorID);
                    if (items != null && items.Count > 0) {
                        Session["TrackData"] = items;
                        Response.Redirect("CartonDetail.aspx?item=" + lbl + "&TL=" + tl,false);
                    }
                    else
                        Master.ReportError(new ApplicationException("Could not find store carton information. Please return to the tracking page and try again."), 4);
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    public string GetPODDate(object poddate,object podtime) {
        if (poddate != DBNull.Value && DateTime.Parse(poddate.ToString()) != DateTime.MinValue)
            return DateTime.Parse(poddate.ToString()).ToShortDateString() + " " + DateTime.Parse(podtime.ToString()).ToShortTimeString();
        else
            return "";
    }
}
