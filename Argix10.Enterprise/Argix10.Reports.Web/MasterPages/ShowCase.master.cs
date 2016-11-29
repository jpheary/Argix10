using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

public partial class DefaultMaster:System.Web.UI.MasterPage {
    //Members
    private bool mHideNav=false;
 
    //Interface
    public string ReportTitle { get { return this.lblReportTitle.Text; } set { this.lblReportTitle.Text = value; } }
    public string NavTitleBackImage { get { return this.tcNavTitle.Style[HtmlTextWriterStyle.BackgroundImage]; } set { this.tcNavTitle.Style[HtmlTextWriterStyle.BackgroundImage] = value; } }
    public bool NavigatorVisible {
        get { return !this.splMain.Panes[0].Collapsed; } 
        set {
            if(!this.mHideNav && value) {
                this.imgExplore.ImageUrl = "~/App_Themes/Reports/Images/explore_on.gif";
                this.tcExplore.Style["border-right-style"] = "none";
                this.splMain.Panes[0].Collapsed = false;
            }
            else {
                this.imgExplore.ImageUrl = "~/App_Themes/Reports/Images/explore_off.gif";
                this.tcExplore.Style["border-right-style"] = "solid";
                this.splMain.Panes[0].Collapsed = true;
            }
        } 
    }
    public ReportViewer Viewer { get { return this.rsViewer; } }
    public string Status { set { ShowMsgBox(value); } }
    public void ReportError(Exception ex) { reportError(ex); }

    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            this.mHideNav = bool.Parse(ConfigurationManager.AppSettings["HideNavigator"]);
            ViewState.Add("HideNav",this.mHideNav);

            //Initialize control values
            this.imgExplore.Attributes.Add("onclick","javascript:document.body.style.cursor='wait';");
            this.tblFlyout.Visible = !this.mHideNav; 
            this.splMain.Panes[0].Collapsed = this.mHideNav;
            NavigatorVisible = !this.mHideNav;

            this.rsViewer.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer",System.UriKind.Absolute);
        }
        else {
            this.mHideNav = (bool)ViewState["HideNav"];
        }
    }
    protected void OnExploreTabClicked(object sender,ImageClickEventArgs e) {
        NavigatorVisible = !NavigatorVisible;
        ScriptManager.RegisterStartupScript(this.imgExplore,typeof(ImageButton),"ClearCursor","document.body.style.cursor='default';",true);
    }
    protected void OnTreeNodeDataBound(object sender,TreeNodeEventArgs e) {
        //Event handler for treeview node data bounded
        string url = e.Node.NavigateUrl;
        if(url.Trim().Length > 0) {
            if(e.Node.Text + " Report" == this.lblReportTitle.Text) {
                e.Node.Selected = true;
                e.Node.Parent.Expanded = true;
                if (e.Node.Parent.Parent != null) e.Node.Parent.Parent.Expanded = true;
            }
        }
    }
    protected void OnViewerError(object sender,ReportErrorEventArgs e) { reportError(e.Exception); }
    #region Local Services: reportError(), ShowMsgBox()
    public void reportError(Exception ex) {
        //Report an exception to the user
        try {
            string src = (ex.Source != null) ? ex.Source + "-\n" : "";
            string msg = src + ex.Message;
            if(ex.InnerException != null) {
                if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            }
            ShowMsgBox(msg);
        }
        catch(Exception) { }
    }
    public void ShowMsgBox(string message) {
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.lblStatus,typeof(Label),"Error","alert('" + message + "');",true);
    }
    #endregion
}
