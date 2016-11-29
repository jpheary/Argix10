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

public partial class Signature:System.Web.UI.Page {
    //Members
    private string mIDType="";
    private int mIDNumber = 0;

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        if(!this.IsPostBack) {
            //Get query params
            this.mIDType = Request.QueryString["type"];
            this.mIDNumber = int.Parse(Request.QueryString["id"]);
        }
    }
    protected override void Render(HtmlTextWriter output) {
        //
        Stream stream=null;
        Bitmap image=null;
        try {
            //Return an image in the web reponse
            byte[] bytes = null;
            Argix.HR.Badge badge = null;
            switch (this.mIDType) {
                case "Drivers":
                    break;
                case "Employees":
                    badge = new Argix.HR.BadgeGateway().GetEmployeeBadge(this.mIDNumber);
                    bytes = ((Argix.HR.EmployeeBadge)badge).Signature;
                    if(bytes != null) {
                        stream = new MemoryStream(bytes);
                        image = new Bitmap(stream);
                    }
                    break;
                case "Vendors":
                    break;
            }

            //Render as jpeg to browser
            HttpResponse response = this.Context.Response;
            response.ContentType = "image/jpeg";
            response.BufferOutput = true;
            response.Clear();
            if (image != null) image.Save(response.OutputStream,ImageFormat.Jpeg);
        }
        catch { }
        finally { if(stream != null) stream.Dispose(); if(image != null) image.Dispose(); }
    }
}
