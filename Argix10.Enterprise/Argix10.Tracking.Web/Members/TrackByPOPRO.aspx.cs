using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _TrackByPOPRO : System.Web.UI.Page {
    //
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for form load event
        Response.Redirect("~/members/trackbyshipment.aspx", false);
    }
}