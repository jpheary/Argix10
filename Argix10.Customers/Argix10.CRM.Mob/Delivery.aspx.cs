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

public partial class Delivery:System.Web.UI.Page {
    //Members
    private string mFromDate = "", mToDate = "";

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                this.Master.DeliveryButtonFontColor = System.Drawing.Color.White;
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
                case "View":
                    if (this.mFromDate.Trim().Length > 0 && this.mToDate.Trim().Length > 0) {
                        this.odsDeliveries.SelectParameters["from"].DefaultValue = this.mFromDate;
                        this.odsDeliveries.SelectParameters["to"].DefaultValue = this.mToDate;
                        this.lsvDeliveries.DataBind();
                    }
                    this.mvPage.SetActiveView(this.vwDeliveries);
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    public string Format(object dateTime,object format) {
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
}
