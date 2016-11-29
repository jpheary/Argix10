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

public partial class Default : System.Web.UI.Page {
	//Members

    //Interface
	protected void Page_Load(object sender, System.EventArgs e) {
        //Page load event handler
        try {
            if (!Page.IsPostBack) {
            }
            Master.TerminalChanged += new EventHandler(OnTerminalChanged);
        }
        catch (Exception ex) { Master.ReportError(ex, 3); }
    }
    protected void OnTerminalChanged(object sender,EventArgs e) {
        //Event handler for change in selected terminal
        try {
            //Load information
            this.odsTLs.SelectParameters["terminalID"].DefaultValue = Master.TerminalCode;
            this.grdTLs.DataBind();
            this.upnlTLs.Update();
            this.upnlTotals.Update();
            ScriptManager.RegisterStartupScript(this.upnlTLs,typeof(UpdatePanel),"ClearCursor","document.body.style.cursor='default';",true);
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnRowDataBound(object sender,GridViewRowEventArgs e) {
        //
        try {
            if (e.Row.RowIndex > -1) {
                e.Row.Attributes.Add("OnClick","OnRowClick();");
                e.Row.Attributes.Add("OnMouseOver","this.style.cursor='hand'");
                e.Row.Attributes.Add("id",e.Row.RowIndex.ToString() + "row");
                foreach (TableCell tabCell in e.Row.Cells) {
                    tabCell.Attributes.Add("UNSELECTABLE","on");
                }
            }
        }
        catch (Exception ex) { Master.ReportError(ex,3); }
    }
    protected void OnSorted(object sender,EventArgs e) {
        this.upnlTLs.Update();
        this.upnlTotals.Update();
    }
}
