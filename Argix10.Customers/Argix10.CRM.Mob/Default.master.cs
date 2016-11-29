using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
    
    //Interface
    public System.Drawing.Color IssuesButtonFontColor { get { return this.lblIssues.ForeColor; } set { this.lblIssues.ForeColor = value; } }
    public System.Drawing.Color SearchButtonFontColor { get { return this.lblSearch.ForeColor; } set { this.lblSearch.ForeColor = value; } }
    public System.Drawing.Color DeliveryButtonFontColor { get { return this.lblDelivery.ForeColor; } set { this.lblDelivery.ForeColor = value; } }
    public System.Drawing.Color TrackingButtonFontColor { get { return this.lblTracking.ForeColor; } set { this.lblTracking.ForeColor = value; } }
    public void ReportError(Exception ex) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            ShowMsgBox(msg);
        }
        catch (Exception) { }
    }
    public void ShowMsgBox(string message) {
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.imgLogo,typeof(Image),"Error","alert('" + message + "');",true);
    }

    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //Event handler for change in view
        switch(e.CommandName) {
            case "Issues":  Response.Redirect("~/ViewIssues.aspx"); break;
            case "Search":  Response.Redirect("~/ViewSearch.aspx"); break;
            case "Delivery": Response.Redirect("~/Delivery.aspx"); break;
            case "Tracking": Response.Redirect("~/Tracking.aspx"); break;
        }
    }
}
