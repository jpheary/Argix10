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
using System.Xml.Linq;

public partial class AgentView : System.Web.UI.Page {
	//Members

    //Interface
	protected void Page_Load(object sender, System.EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
                //ScriptManager.RegisterStartupScript(this.imgTLView,typeof(ImageButton),"ClearCursor","document.body.style.cursor='default';",true);
            }
            Master.TerminalChanged += new EventHandler(OnTerminalChanged);
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for change in selected terminal
        try {
            //Load information
            this.odsSummary.SelectParameters["terminalID"].DefaultValue = Master.TerminalCode;
            this.grdSummary.DataBind();
            this.upnlAgents.Update();
            ScriptManager.RegisterStartupScript(this.upnlAgents,typeof(UpdatePanel),"ClearCursor","document.body.style.cursor='default';",true);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
}
