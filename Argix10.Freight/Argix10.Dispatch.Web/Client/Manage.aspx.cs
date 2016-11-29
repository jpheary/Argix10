using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Argix.Freight;

public partial class Manage:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                //TODO
                this.btnAccountUpdate.Enabled = false;

                //Starting tab?
                string view =  Request.QueryString["view"] != null ? Request.QueryString["view"] : "client";
                OnChangeView(null,new CommandEventArgs(view,null));
            }
            else {
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "client":
                    this.mvwPage.ActiveViewIndex = 0;
                    this.liClient.Style["border-bottom-style"] = "none";
                    break;
            }
            this.liBlank1.Style["border-top-style"] = this.liBlank2.Style["border-top-style"] = "none";
            this.liBlank1.Style["border-right-style"] = this.liBlank2.Style["border-right-style"] = "none";
            this.liBlank1.Style["border-bottom-style"] = this.liBlank2.Style["border-bottom-style"] = "solid";
            this.liBlank1.Style["border-left-style"] = this.liBlank2.Style["border-left-style"] = "none";
            this.liBlank3.Style["border-top-style"] = this.liBlank4.Style["border-top-style"] = "none";
            this.liBlank3.Style["border-right-style"] = this.liBlank4.Style["border-right-style"] = "none";
            this.liBlank3.Style["border-bottom-style"] = this.liBlank4.Style["border-bottom-style"] = "solid";
            this.liBlank3.Style["border-left-style"] = this.liBlank4.Style["border-left-style"] = "none";
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        this.btnAccountUpdate.Enabled = true;
    }
    protected void OnManageCommand(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName) {
                case "AccountUpdate":
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex, 4); }
    }
}
