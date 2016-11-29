using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using Microsoft.Reporting.WebForms;

public partial class _ReportsMaster : System.Web.UI.MasterPage {
    //Members
    private const string ITEM_SUBPATH = "/Argix08.Reports";

    public event CommandEventHandler ButtonCommand = null;

    //Interface
    public string ReportTitle { get { return this.lblReportTitle.Text; } set { this.lblReportTitle.Text = value; } }
    public string ReportDesc { get { return this.lblReportDesc.Text; } set { this.lblReportDesc.Text = value; } }
    public ReportViewer Viewer { get { return this.rsViewer; } }
    public bool Validated { set { this.btnRun.Enabled = value; } }
    public Stream GetReportDefinition(string report) {
        //Return a report definition from the SQL reporting server
        SSRS.ReportingService2010 rs = new SSRS.ReportingService2010();
        rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        byte[] bytes = rs.GetItemDefinition(ITEM_SUBPATH + report);
        return new System.IO.MemoryStream(bytes);
    }
    protected void Page_Load(object sender, EventArgs e) {
        //Event handler for form load event
        try {
            if (!Page.IsPostBack) {
                //Set reports:  Admin\Argix role get everything (00000.xml);
                //              Reports role gets custom file if exists or default file otherwise
                MembershipServices membership = new MembershipServices();
                if (membership.IsAdmin || membership.IsArgix)
                    this.xmlReports.DataFile = "~/App_Data/00000.xml";
                else if (membership.IsRSMember) {
                    if (membership.MemberProfile != null) {
                        string vfile = "~/App_Data/" + membership.MemberProfile.ClientVendorID.Trim().PadLeft(5,'0') + ".xml";
                        string file = Server.MapPath(vfile);
                        if (System.IO.File.Exists(file))
                            this.xmlReports.DataFile = vfile;
                        else
                            this.xmlReports.DataFile = "~/App_Data/default.xml";
                    }
                }
                else
                    this.xmlReports.DataFile = "~/App_Data/blank.xml";
            }
        }
        catch(Exception ex) { ReportError(ex,3); }
    }
    protected void OnTreeNodeDataBound(object sender, TreeNodeEventArgs e) {
        //Event handler for treeview node data bounded
        string url = e.Node.NavigateUrl;
        if(url.Trim().Length > 0) {
            if(e.Node.Text + " Report" == this.lblReportTitle.Text) {
                e.Node.Selected = true;
                e.Node.Parent.Expanded = true;
            }
        }
    }
    protected void OnButtonCommand(object sender, CommandEventArgs e) {
        //Event handler for export button clicked
        switch(e.CommandName) {
            case "Setup":
                this.mvMain.SetActiveView(this.vwParams);
                this.rsViewer.Reset();
                break;
            case "Run":
                this.mvMain.SetActiveView(this.vwReport);
                if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                //if(this.mPDFOnly) {
                //    this.rsViewer.ShowExportControls = false;
                //    string mimeType="",encoding="",extension="";
                //    string[] streamids;
                //    Warning[] warnings;
                //    byte[] bytes = this.rsViewer.LocalReport.Render("PDF",null,out mimeType,out encoding,out extension,out streamids,out warnings);
                //    Response.Buffer = true;
                //    Response.Clear();
                //    Response.ContentType = mimeType;
                //    Response.AddHeader("Accept-Header",bytes.Length.ToString());
                //    Response.ContentType = mimeType;
                //    Response.OutputStream.Write(bytes,0,Convert.ToInt32(bytes.Length));
                //    Response.Flush();
                //    Response.End();
                //}
                break;
        }
    }
    protected void OnViewerError(object sender, ReportErrorEventArgs e) { Master.ReportError(e.Exception, 4); }
    public void ReportError(Exception ex, int logLevel) { Master.ReportError(ex, logLevel); }
    public void ShowMessageBox(string message) { Master.ShowMessageBox(message); }
}
