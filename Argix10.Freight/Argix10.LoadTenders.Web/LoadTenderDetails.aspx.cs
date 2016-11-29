using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LoadTenderDetails : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        if (!Page.IsPostBack) {
            this.grdLoad.DataBind();
        }
    }
}