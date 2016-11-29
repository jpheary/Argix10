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
    private bool mRun=true,mData=true,mExcel=true,mPDFOnly=false;
    public event CommandEventHandler ButtonCommand=null;

    //Interface
    public string ReportTitle { get { return this.lblReportTitle.Text; } set { this.lblReportTitle.Text = value; } }
    public ReportViewer Viewer { get { return this.rsViewer; } }
    public bool Validated { 
        set {
            this.btnRun.Enabled = this.mRun && value;
            if(!this.mPDFOnly) {
                this.btnData.Enabled = this.mData && value;
                this.btnExcel.Enabled = this.mExcel && value;
            }
        } 
    }
    public string Status { set { ShowMessageBox(value); } }
    public Stream GetReportDefinition(string report) {
        //Return a report definition from the SQL reporting server
        SQLReports.ReportingService2010 rs = new SQLReports.ReportingService2010();
        rs.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        byte[] bytes = rs.GetItemDefinition("/Argix08.Reports" + report);
        return new System.IO.MemoryStream(bytes);
    }
    public Stream CreateExportRdl(DataSet ds,string dataSetName,string dataSourceName) {
        //Open a new stream for writing
        MemoryStream stream = new MemoryStream();
        XmlTextWriter writer = new XmlTextWriter(stream,Encoding.UTF8);
        writer.Formatting = Formatting.Indented;

        //Create list of dataset fields
        ArrayList fields = new ArrayList();
        for(int i=0;i<=ds.Tables[0].Columns.Count-1;i++)
            fields.Add(ds.Tables[0].Columns[i].ColumnName);

        //Report element
        writer.WriteProcessingInstruction("xml","version=\"1.0\" encoding=\"utf-8\"");
        writer.WriteStartElement("Report");
        writer.WriteAttributeString("xmlns",null,"http://schemas.microsoft.com/sqlserver/reporting/2005/01/reportdefinition");
        writer.WriteElementString("Width","6.825in");
        #region DataSource element
        writer.WriteStartElement("DataSources");
        writer.WriteStartElement("DataSource");
        writer.WriteAttributeString("Name",dataSourceName);
        writer.WriteElementString("DataSourceReference",dataSourceName);
        writer.WriteEndElement();
        writer.WriteEndElement();
        #endregion
        #region Body element
        writer.WriteStartElement("Body");
        writer.WriteElementString("Height","1.2in");
        writer.WriteStartElement("ReportItems");
        writer.WriteStartElement("Table");
        writer.WriteAttributeString("Name","table1");
        writer.WriteElementString("DataSetName",dataSetName);
        writer.WriteStartElement("Style");
        writer.WriteElementString("FontSize","8pt");
        writer.WriteEndElement(); //Style
        #region Details
        writer.WriteStartElement("Details");
        writer.WriteStartElement("TableRows");
        writer.WriteStartElement("TableRow");
        writer.WriteStartElement("TableCells");
        foreach(string fieldName in fields) {
            //Textbox
            writer.WriteStartElement("TableCell");
            writer.WriteStartElement("ReportItems");
            writer.WriteStartElement("Textbox");
            writer.WriteAttributeString("Name","txt" + System.Xml.XmlConvert.EncodeName(fieldName));
            writer.WriteStartElement("Style");
            writer.WriteElementString("FontSize","8pt");
            writer.WriteStartElement("BorderStyle");
            writer.WriteElementString("Default","Solid");
            writer.WriteEndElement(); //BorderStyle
            writer.WriteStartElement("BorderWidth");
            writer.WriteElementString("Default","0.5pt");
            writer.WriteEndElement(); //BorderWidth
            writer.WriteEndElement(); //Style
            writer.WriteElementString("Value","=Fields!" + System.Xml.XmlConvert.EncodeName(fieldName) + ".Value");
            writer.WriteEndElement(); //Textbox
            writer.WriteEndElement(); //ReportItems
            writer.WriteEndElement(); //TableCell
        }
        writer.WriteEndElement(); //TableCells
        writer.WriteElementString("Height","0.1875in");
        writer.WriteEndElement(); //TableRow
        writer.WriteEndElement(); //TableRows
        writer.WriteEndElement(); //Details
        #endregion
        #region Header
        writer.WriteStartElement("Header");
        writer.WriteStartElement("TableRows");
        writer.WriteStartElement("TableRow");
        writer.WriteElementString("Height","0.25in");
        writer.WriteStartElement("TableCells");
        foreach(string fieldName in fields) {
            //Textbox
            writer.WriteStartElement("TableCell");
            writer.WriteStartElement("ReportItems");
            writer.WriteStartElement("Textbox");
            writer.WriteAttributeString("Name","hdr" + System.Xml.XmlConvert.EncodeName(fieldName));
            writer.WriteStartElement("Style");
            writer.WriteElementString("FontSize","8pt");
            writer.WriteStartElement("BorderStyle");
            writer.WriteElementString("Default","Solid");
            writer.WriteEndElement(); //BorderStyle
            writer.WriteStartElement("BorderWidth");
            writer.WriteElementString("Default","0.5pt");
            writer.WriteEndElement(); //BorderWidth
            writer.WriteEndElement(); //Style
            writer.WriteElementString("CanGrow","false");
            writer.WriteElementString("Value",fieldName);
            writer.WriteEndElement(); //Textbox
            writer.WriteEndElement(); //ReportItems
            writer.WriteEndElement(); //TableCell
        }
        writer.WriteEndElement(); //TableCells
        writer.WriteEndElement(); //TableRow
        writer.WriteEndElement(); //TableRows
        writer.WriteEndElement(); //Header
        #endregion
        #region TableColumns
        writer.WriteStartElement("TableColumns");
        for(int i=0;i<fields.Count;i++) {
            writer.WriteStartElement("TableColumn");
            writer.WriteElementString("Width","0.75in");
            writer.WriteEndElement(); //TableColumn
        }
        writer.WriteEndElement(); //TableColumns
        #endregion
        writer.WriteEndElement(); //Table
        writer.WriteEndElement(); //ReportItems
        writer.WriteEndElement(); //Body
        #endregion
        #region DataSet element
        writer.WriteStartElement("DataSets");
        writer.WriteStartElement("DataSet");
        writer.WriteAttributeString("Name",dataSetName);
        writer.WriteStartElement("Query");
        writer.WriteElementString("CommandText","");
        writer.WriteElementString("DataSourceName",dataSourceName);
        writer.WriteEndElement(); //Query
        writer.WriteStartElement("Fields");
        foreach(string fieldName in fields) {
            writer.WriteStartElement("Field");
            writer.WriteAttributeString("Name",System.Xml.XmlConvert.EncodeName(fieldName));
            writer.WriteElementString("DataField",fieldName);
            writer.WriteEndElement();
        }
        writer.WriteEndElement(); //Fields
        writer.WriteEndElement(); //DataSet
        writer.WriteEndElement(); //DataSets
        #endregion
        writer.WriteEndElement(); //Report
        writer.Flush();
        writer.BaseStream.Seek(0,0);
        return writer.BaseStream;
    }

    protected override void OnInit(EventArgs e) {
        //Event handler for page Init event
        if(!Page.IsPostBack && !ScriptManager.GetCurrent(Page).IsInAsyncPostBack) {
            //Get configuration values for this control
            string path = Page.Request.CurrentExecutionFilePath.Replace(".aspx","");
            string reportName = path.Substring(path.LastIndexOf('/') + 1);
            System.Xml.XmlNode rpt = this.xmlConfig.GetXmlDocument().SelectSingleNode("//report[@name='" + reportName + "']");
            if(rpt != null) {
                System.Xml.XmlNode svcs = rpt.SelectSingleNode("Services");
                if(svcs != null) {
                    this.mRun = bool.Parse(svcs.Attributes["run"].Value);
                    this.mData = bool.Parse(svcs.Attributes["data"].Value);
                    this.mExcel = bool.Parse(svcs.Attributes["excel"].Value);
                    this.mPDFOnly = bool.Parse(svcs.Attributes["pdfonly"].Value);
                }
            }
        }
        base.OnInit(e);
    }
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!Page.IsPostBack) {
            ViewState.Add("Run",this.mRun);
            ViewState.Add("Data", this.mData);
            ViewState.Add("Excel", this.mExcel);
            ViewState.Add("PDFOnly",this.mPDFOnly);
        }
        else {
            this.mRun = (bool)ViewState["Run"];
            this.mData = (bool)ViewState["Data"];
            this.mExcel = (bool)ViewState["Excel"];
            this.mPDFOnly = (bool)ViewState["PDFOnly"];
        }
        this.btnRun.Visible = this.mRun;
        this.btnData.Visible = this.mData;
        this.btnExcel.Visible = this.mExcel;
        if(this.mPDFOnly) this.btnData.Visible = this.btnExcel.Visible = false;
        OnButtonCommand(null,new CommandEventArgs(Session["View"].ToString(),null));
    }
    protected void OnButtonCommand(object sender,CommandEventArgs e) {
        //Event handler for export button clicked
        //try {
            DateTime start = DateTime.Now;
            switch(e.CommandName) {
                case "Setup":
                    this.mvMain.SetActiveView(this.vwParams);
                    this.rsViewer.Reset();
                    break;
                case "Run":
                    if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                    this.mvMain.SetActiveView(this.vwReport);
                    if(this.mPDFOnly) {
                        this.rsViewer.ShowExportControls = false;
                        string mimeType="",encoding="",extension="";
                        string[] streamids;
                        Warning[] warnings;
                        byte[] bytes = this.rsViewer.LocalReport.Render("PDF",null,out mimeType,out encoding,out extension,out streamids,out warnings);
                        Response.Buffer = true;
                        Response.Clear();
                        Response.ContentType = mimeType;
                        Response.AddHeader("Accept-Header",bytes.Length.ToString());
                        Response.ContentType = mimeType;
                        Response.OutputStream.Write(bytes,0,Convert.ToInt32(bytes.Length));
                        Response.Flush();
                        Response.End();
                    }
                    break;
                case "Data":
                    if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                    this.mvMain.SetActiveView(this.vwReport);
                    break;
                case "Excel":
                    if(this.ButtonCommand != null) this.ButtonCommand(sender,e);
                    this.mvMain.SetActiveView(this.vwReport);
                    break;
            }
        //}
        //catch (Exception ex) { ReportError(ex); }
    }
    protected void OnViewerError(object sender,ReportErrorEventArgs e) { ReportError(e.Exception, 4); }

    public void ReportError(Exception ex,int logLevel) {
        //Report an exception to the user
        try {
            string msg = ex.Message;
            if (ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;

            string username = Membership.GetUser().UserName;
            new Argix.EnterpriseService().WriteLogEntry(3,username,ex);
            //if (logLevel > 3) new EmailGateway().SendITNotification(username,ex);
            ShowMessageBox(msg);
        }
        catch (Exception) { }
    }
    public void ShowMessageBox(string message) {
        //
        message = message.Replace("'","").Replace("\n"," ").Replace("\r"," ");
        ScriptManager.RegisterStartupScript(this.lblMsg,typeof(Label),"Error","alert('" + message + "');",true);
    }
}
