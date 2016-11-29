using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using Argix.Enterprise;

public partial class _TrackByCarton : System.Web.UI.Page {
    //Members

    //Inteface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        try {
            if (!Page.IsPostBack) {
                if (Session["TrackBy"] != null) this.cboTrackBy.SelectedValue = Session["TrackBy"].ToString();

                Page.ClientScript.RegisterStartupScript(typeof(Page),"StartupScript","document.all." + this.txtNumbers.UniqueID + ".select();",true);
                this.txtNumbers.Attributes.Add("onkeypress","checkTextLen(this.form." + this.txtNumbers.UniqueID + ",1000);");
                this.txtNumbers.Attributes.Add("onblur","checkTextLen(this.form." + this.txtNumbers.UniqueID + ",1000);");
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnTrack(object sender,EventArgs e) {
        //Track one or more cartons
        try {
            if (Page.IsValid) {
                //Flag search by method
                string trackBy = TrackingGateway.SEARCHBY_CARTONNUMBER;
                switch (this.cboTrackBy.SelectedValue) {
                    case "CartonNumber": trackBy = TrackingGateway.SEARCHBY_CARTONNUMBER; break;
                    case "LabelNumber": trackBy = TrackingGateway.SEARCHBY_LABELNUMBER; break;
                    case "PlateNumber": trackBy = TrackingGateway.SEARCHBY_PLATENUMBER; break;
                }
                Session["TrackBy"] = trackBy;

                //Validate
                string input = encodeInput(this.txtNumbers.Text);
                if (input.Length == 0) { Master.ShowMessageBox("Please enter valid tracking numbers."); return; }
                string[] numbers = input.Split(Convert.ToChar(13));
                if (numbers.Length > 25) { Master.ShowMessageBox("Please limit your search to 25 items."); return; }

                //Track
                ProfileCommon profile = new MembershipServices().MemberProfile;
                TrackingItems items = new TrackingGateway().TrackCartons(numbers,trackBy,profile.Type,profile.ClientVendorID);
                Session["TrackData"] = items;
                if (items != null && items.Count > 0) {
                    if (items.Count == 1 && items[0].LabelNumber.Trim().Length > 0)
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
    #region Local Services: encodeInput()
    private string encodeInput(string input) {
        //This method makes sure no invalid chars exist in the user input
        string cn = Server.HtmlEncode(input);
        cn = cn.Replace("'", "''");
        cn = cn.Replace("[", "[[]");
        cn = cn.Replace("%", "[%]");
        cn = cn.Replace("_", "[_]");
        cn = cn.Replace(",", "\r");
        cn = cn.Replace("\r\n", "\r");
        cn = cn.Replace("\n", "\r");
        cn = cn.Replace(" ", "");
        return cn.Trim();
    }
    #endregion
}
