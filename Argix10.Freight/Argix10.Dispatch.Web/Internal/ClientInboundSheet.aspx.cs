using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ClientInboundSheet : System.Web.UI.Page {
    //Interface
    protected void Page_Load(object sender, EventArgs e)  {
        //Event handler for page load event
        if (!Page.IsPostBack) {
            this.tmrRefresh.Enabled = Convert.ToBoolean(Session["AutoRefreshOn"]);
        }
    }
    protected void OnRefreshSchedule(object sender,CommandEventArgs e) {
        //Event handler for toolbar button clicked
        switch (e.CommandName) {
            case "Refresh":
                this.grdSchedule.DataBind();
                break;
        }
    }
    protected void OnScheduleTimerTick(object sender,EventArgs e) {
        //Event handler for issue timer tick event
        this.grdSchedule.DataBind();
    }
}