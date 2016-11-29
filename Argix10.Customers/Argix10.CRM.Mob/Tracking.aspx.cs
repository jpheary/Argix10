using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Argix.Customers;

public partial class Tracking:System.Web.UI.Page {
    //Members
    private string mFromDate = "",mToDate = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                this.Master.TrackingButtonFontColor = System.Drawing.Color.White;
            }
            else {
                this.mFromDate = Request.Form["txtFrom"] != null ? Request.Form["txtFrom"].Trim() : "";
                this.mToDate = Request.Form["txtTo"] != null ? Request.Form["txtTo"].Trim() : "";
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnOnCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "Back":
                    this.mvPage.SetActiveView(this.vwSetup);
                    break;
                case "BackStore":
                    this.mvPage.SetActiveView(this.vwStore);
                    break;
                case "Track":
                    this.odsTracking.SelectParameters["from"].DefaultValue = this.mFromDate;
                    this.odsTracking.SelectParameters["to"].DefaultValue = this.mToDate;
                    this.lsvTracking.DataBind();
                    this.mvPage.SetActiveView(this.vwStore);
                    break;
                case "ViewTL":
                    string tl = e.CommandArgument.ToString();
                    this.lblTL.Text = tl;
                    this.odsCartons.SelectParameters["tl"].DefaultValue = tl;
                    this.odsCartons.SelectParameters["from"].DefaultValue = this.odsTracking.SelectParameters["from"].DefaultValue;
                    this.odsCartons.SelectParameters["to"].DefaultValue = this.odsTracking.SelectParameters["to"].DefaultValue;
                    this.lsvCartons.DataBind();
                    this.mvPage.SetActiveView(this.vwTL);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected string Format(object dateTime,object format) {
        //
        string _format = dateTime.ToString();
        try {
            if (dateTime.ToString().Trim().Length > 0) {
                DateTime dt = DateTime.Parse(dateTime.ToString().Trim());
                _format = dt.ToString((string)format);
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return _format;
    }
    protected string DeliveryDateLabel(object ofdDate,object podDate) {
        //
        string dateLabel = "OFD1";
        try {
            if (podDate.ToString().Trim().Length > 0) dateLabel = "POD:";
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return dateLabel;
    }
    protected string DeliveryDateFormat(object ofdDate,object podDate,object format) {
        //
        string dateFormat = "";
        try {
            if (podDate.ToString().Trim().Length > 0) {
                DateTime dt = DateTime.Parse(podDate.ToString().Trim());
                dateFormat = dt.ToString((string)format);
            }
            else if (ofdDate.ToString().Trim().Length > 0) {
                DateTime dt = DateTime.Parse(ofdDate.ToString().Trim());
                dateFormat = dt.ToString((string)format);
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
        return dateFormat;
    }
}
