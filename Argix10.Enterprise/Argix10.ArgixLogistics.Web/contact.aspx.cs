using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class contact : System.Web.UI.Page {
    //
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for page load event
        try {
            if(!Page.IsPostBack) {
                //Setup UI; disable client change for new/update
                this.txtName.Text = "";
            }
        }
        catch(Exception ex) { reportError(ex, 3); }
    }
    protected void OnCommand(object sender, CommandEventArgs e) {
        //Button command event handler
        try {
            switch(e.CommandName) {
                case "Submit":
                    //Validate
                    if(!Page.IsValid) return;
                    if(this.txtEmail.Text.Trim().Length == 0 && this.txtTele.Text.Trim().Length == 0) showMessageBox("Please provide either an email address or a telephone number.");

                    //Submit
                    string from = this.txtEmail.Text.Trim();
                    string subject = "Argix Logistics - Contact Submission";
                    string message = "Argix Logistics Contact Form submission on " + DateTime.Now.ToString() + ". Below are the details:\r\n\r\n";
                    if(this.txtName.Text.Trim().Length > 0) message += "Name: " + this.txtName.Text.Trim() + "\r\n";
                    if(this.txtCompany.Text.Trim().Length > 0) message += "Company: " + this.txtCompany.Text.Trim() + "\r\n";
                    if(this.txtTitle.Text.Trim().Length > 0) message += "Title: " + this.txtTitle.Text.Trim() + "\r\n";
                    if(this.txtEmail.Text.Trim().Length > 0) message += "Email: " + this.txtEmail.Text.Trim() + "\r\n";
                    if(this.txtTele.Text.Trim().Length > 0) message += "Phone: " + this.txtTele.Text.Trim() + "\r\n";
                    if(this.txtAddress.Text.Trim().Length > 0) message += "Address: " + this.txtAddress.Text.Trim() + "\r\n\r\n";
                    message += "\r\n";
                    message += "Services: \r\n";
                    if(this.chkBrochure.Checked) message += "\t" + this.chkBrochure.Text + "\r\n";
                    if(this.chkTour.Checked) message += "\t" + this.chkTour.Text + "\r\n";
                    if(this.chkAssessment.Checked) message += "\t" + this.chkAssessment.Text + "\r\n";
                    if(this.chkContact.Checked) message += "\t" + this.chkContact.Text + "\r\n";
                    if(this.txtRequests.Text.Trim().Length > 0) {
                        message += "\r\n";
                        message += "Additional Requests: \r\n";
                        message += "\t" + this.txtRequests.Text.Trim() + "\r\n";
                    }
                    new EmailGateway().SendContactUsMessage(from, subject, message);
                    Response.Redirect("thank-you.html");
                    //showMessageBox("Thank you.");
                    break;
            }
        }
        catch(Exception ex) { reportError(ex, 4); }
    }
    public void reportError(Exception ex, int logLevel) {
        try {
            string msg = ex.Message;
            if(ex.InnerException != null) msg = ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
            showMessageBox(msg);
        }
        catch(Exception) { }
    }
    public void showMessageBox(string message) {
        message = message.Replace("'", "").Replace("\n", " ").Replace("\r", " ");
        ScriptManager.RegisterStartupScript(this.lblMsg, typeof(Label), "Error", "alert('" + message + "');", true);
    }
}