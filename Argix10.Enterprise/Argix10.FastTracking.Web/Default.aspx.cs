using System;
using System.Data;
using System.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

public partial class Default : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        Master.GoSummaryVisible = false;
        Master.GoTrackVisible = true;

        //Redirect based upon client
        string companyID = Session["CompanyID"] == null ? "" : Session["CompanyID"].ToString();
        switch(companyID) {
            case "001": Response.Redirect("~/BN.aspx"); break;
            case "035": Response.Redirect("~/Ecko.aspx"); break;
            case "136": Response.Redirect("~/Loreal.aspx"); break;
        }
    }
}
