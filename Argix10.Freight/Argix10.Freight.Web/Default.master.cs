using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default:System.Web.UI.MasterPage {
    //Members
    
    //Interface
    public System.Drawing.Color FreightButtonFontColor { get { return this.lblFreight.ForeColor; } set { this.lblFreight.ForeColor = value; } }
    public System.Drawing.Color SortingButtonFontColor { get { return this.lblSorting.ForeColor; } set { this.lblSorting.ForeColor = value; } }
    public System.Drawing.Color TLButtonFontColor { get { return this.lblTLs.ForeColor; } set { this.lblTLs.ForeColor = value; } }
    public System.Drawing.Color ZoneButtonFontColor { get { return this.lblZones.ForeColor; } set { this.lblZones.ForeColor = value; } }
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
        //Event handler for page load event
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //Event handler for change in view
        switch(e.CommandName) {
            case "Freight": Response.Redirect("~/ViewFreight.aspx"); break;
            case "Sorting": Response.Redirect("~/ViewSorting.aspx"); break;
            case "TLs": Response.Redirect("~/ViewTLs.aspx"); break;
            case "Zones": Response.Redirect("~/ViewZones.aspx"); break; 
        }
    }
}
