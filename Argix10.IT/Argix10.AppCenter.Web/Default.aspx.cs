using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page {
    //Members

    //Intreface
    protected void Page_Load(object sender, EventArgs e)  {
        //Event handler for page load event
        try {
            if (!Page.IsPostBack) {
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
        finally { OnValidateForm(null,EventArgs.Empty); }
    }
    protected void OnNavigate(object sender,CommandEventArgs e) {
        //
        try {
            //this.liBlank1.Style["border-top-style"] = this.liBlank2.Style["border-top-style"] = "none";
            //this.liBlank1.Style["border-right-style"] = this.liBlank2.Style["border-right-style"] = "none";
            //this.liBlank1.Style["border-bottom-style"] = this.liBlank2.Style["border-bottom-style"] = "solid";
            //this.liBlank1.Style["border-left-style"] = this.liBlank2.Style["border-left-style"] = "none";
            switch (e.CommandName.ToLower()) {
                case "allterminals":
                    this.mvwPage.ActiveViewIndex = 0;
                    break;
                case "atlanta":
                    this.mvwPage.ActiveViewIndex = 1;
                    break;
                case "carson":
                    this.mvwPage.ActiveViewIndex = 2;
                    break;
                case "charlotte":
                    this.mvwPage.ActiveViewIndex = 3;
                    break;
                case "chicago":
                    this.mvwPage.ActiveViewIndex = 4;
                    break;
                case "dallas":
                    this.mvwPage.ActiveViewIndex = 5;
                    break;
                case "jamesburg":
                    this.mvwPage.ActiveViewIndex = 6;
                    break;
                case "lakeland":
                    this.mvwPage.ActiveViewIndex = 7;
                    break;
                case "medley":
                    this.mvwPage.ActiveViewIndex = 8;
                    break;
                case "ridgefield":
                    this.mvwPage.ActiveViewIndex = 9;
                    break;
                case "southwindsor":
                    this.mvwPage.ActiveViewIndex = 10;
                    break;
                case "wilmingtom":
                    this.mvwPage.ActiveViewIndex = 11;
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