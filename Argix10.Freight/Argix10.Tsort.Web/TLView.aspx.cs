using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class TLView:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //
                this.cboTerminal.DataBind();
                int index = Convert.ToInt32(Session["TerminalIndex"]);
                if (this.cboTerminal.Items.Count > 0) this.cboTerminal.SelectedIndex = index > 0 ? index : 0;
                OnTerminalChanged(null,EventArgs.Empty);

                //Starting tab?
                string view = Request.QueryString["view"] != null ? Request.QueryString["view"] : "tls";
                OnChangeView(null,new CommandEventArgs(view,null));
            }
            else {
            }
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            Session["TerminalIndex"] = this.cboTerminal.SelectedIndex;
            this.grdTLs.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnRefresh(object sender,EventArgs e) {
        //Event handler for refresh button clicked
        try {
            this.grdTLs.DataBind();
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "tls":
                    this.liTLs.Style["border-bottom-style"] = "none";
                    this.liAgents.Style["border-bottom-style"] = "solid";
                    this.grdTLs.DataBind();
                    this.mvTLs.SetActiveView(this.vwTLView);
                    break;
                case "agents":
                    this.liTLs.Style["border-bottom-style"] = "solid";
                    this.liAgents.Style["border-bottom-style"] = "none";
                    this.grdTLs.DataBind();
                     this.mvTLs.SetActiveView(this.vwAgentView);
                   break;
            }
            this.liBlank2.Style["border-top-style"] = this.liBlank2.Style["border-right-style"] = this.liBlank2.Style["border-left-style"] = "none";
            this.liBlank3.Style["border-top-style"] = this.liBlank3.Style["border-right-style"] = this.liBlank3.Style["border-left-style"] = "none";
            this.liBlank4.Style["border-top-style"] = this.liBlank4.Style["border-right-style"] = this.liBlank4.Style["border-left-style"] = "none";
            this.liBlank5.Style["border-top-style"] = this.liBlank5.Style["border-right-style"] = this.liBlank5.Style["border-left-style"] = "none";
        }
        catch (Exception ex) { Master.ReportError(ex); }
    }
}
