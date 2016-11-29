using System;
using System.Collections.Generic;
using System.Data;

using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.HR;

public partial class _Default : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        Response.Redirect("~/Permits/FindPermit.aspx", false);
    }
}
