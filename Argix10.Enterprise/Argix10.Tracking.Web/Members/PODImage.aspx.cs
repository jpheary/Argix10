using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class PODImage:System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            //Get query params
            string uri = Request.QueryString["uri"];

            //Return an image from SharePoint in the web reponse
            byte[] bytes = new Argix.Enterprise.ImagingGateway().GetSharePointImageStream(uri);
            Stream stream = new MemoryStream(bytes);

            //Render as jpeg to browser
            HttpResponse response = this.Context.Response;
            response.Clear();
            response.ContentType = "application/pdf";
            response.BufferOutput = false;
            response.BinaryWrite(bytes);
        }
        catch(Exception ex) { this.Context.Response.Write(ex.ToString()); }
    }
}
