using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _SOATopology : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
                OnChangeView(null,new CommandEventArgs("agentlinehaul",null));
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnChangeView(object sender,CommandEventArgs e) {
        //
        try {
            switch (e.CommandName.ToLower()) {
                case "agentlinehaul":
                    this.mvwPage.ActiveViewIndex = 0;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "none";
                    this.liCustomers.Style["border-bottom-style"] = "solid";
                    this.liEnterprise.Style["border-bottom-style"] = "solid";
                    this.liFinance.Style["border-bottom-style"] = "solid";
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liHR.Style["border-bottom-style"] = "solid";
                    this.liTerminals.Style["border-bottom-style"] = "solid";
                   break;
                case "customers":
                    this.mvwPage.ActiveViewIndex = 1;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "solid";
                    this.liCustomers.Style["border-bottom-style"] = "none";
                    this.liEnterprise.Style["border-bottom-style"] = "solid";
                    this.liFinance.Style["border-bottom-style"] = "solid";
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liHR.Style["border-bottom-style"] = "solid";
                    this.liTerminals.Style["border-bottom-style"] = "solid";
                    break;
                case "enterprise":
                    this.mvwPage.ActiveViewIndex = 2;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "solid";
                    this.liCustomers.Style["border-bottom-style"] = "solid";
                    this.liEnterprise.Style["border-bottom-style"] = "none";
                    this.liFinance.Style["border-bottom-style"] = "solid";
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liHR.Style["border-bottom-style"] = "solid";
                    this.liTerminals.Style["border-bottom-style"] = "solid";
                   break;
                case "finance":
                    this.mvwPage.ActiveViewIndex = 3;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "solid";
                    this.liCustomers.Style["border-bottom-style"] = "solid";
                    this.liEnterprise.Style["border-bottom-style"] = "solid";
                    this.liFinance.Style["border-bottom-style"] = "none";
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liHR.Style["border-bottom-style"] = "solid";
                    this.liTerminals.Style["border-bottom-style"] = "solid";
                    break;
                case "freight":
                    this.mvwPage.ActiveViewIndex = 4;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "solid";
                    this.liCustomers.Style["border-bottom-style"] = "solid";
                    this.liEnterprise.Style["border-bottom-style"] = "solid";
                    this.liFinance.Style["border-bottom-style"] = "solid";
                    this.liFreight.Style["border-bottom-style"] = "none";
                    this.liHR.Style["border-bottom-style"] = "solid";
                    this.liTerminals.Style["border-bottom-style"] = "solid";
                    break;
                case "hr":
                    this.mvwPage.ActiveViewIndex = 5;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "solid";
                    this.liCustomers.Style["border-bottom-style"] = "solid";
                    this.liEnterprise.Style["border-bottom-style"] = "solid";
                    this.liFinance.Style["border-bottom-style"] = "solid";
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liHR.Style["border-bottom-style"] = "none";
                    this.liTerminals.Style["border-bottom-style"] = "solid";
                    break;
                case "terminals":
                    this.mvwPage.ActiveViewIndex = 6;
                    this.liAgentLineHaul.Style["border-bottom-style"] = "solid";
                    this.liCustomers.Style["border-bottom-style"] = "solid";
                    this.liEnterprise.Style["border-bottom-style"] = "solid";
                    this.liFinance.Style["border-bottom-style"] = "solid";
                    this.liFreight.Style["border-bottom-style"] = "solid";
                    this.liHR.Style["border-bottom-style"] = "solid";
                    this.liTerminals.Style["border-bottom-style"] = "none";
                    break;
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnValidateForm(object sender,EventArgs e) {
        //Event handler for changes in parameter data
        try {
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
}