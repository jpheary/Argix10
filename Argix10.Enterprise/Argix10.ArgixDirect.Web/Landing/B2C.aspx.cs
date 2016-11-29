﻿using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

public partial class B2C : System.Web.UI.Page
{
    //
    protected void Page_Load(object sender, EventArgs e)
    {
        //Event handler for page load event
        if (!this.IsPostBack)
        {
            this.ClientScript.RegisterStartupScript(typeof(Page), "Focus", "document.B2CForm.name.focus()", true);
        }
        else {
            if (this.Request.Form != null && this.Request.Form["name"] != null && this.Request.Form["email"] != null)
            {
                string from = this.Request.Form["email"];
                string subject = "Request from B2C Landing Page";
                string message = "This request was submitted from the B2C Landing Page which provided the following information: \n\n" +
                                    "Name:  " + this.Request.Form["name"] + "\n" +
                                    "Company:  " + this.Request.Form["company"] + "\n" + "Address:" + this.Request.Form["address"] + "\n" +
                                    "State:   " + this.Request.Form["state"] + "\n" +
                                    "Email:  " + this.Request.Form["email"] + "\n" +
                                    "Request:  " + this.Request.Form["userOpts"] + "\n" +
                                    "Phone:  " + this.Request.Form["phone"];
                //EmailServices email = new EmailServices();
                //email.SendMail(from, subject, message);
                //Response.Redirect("ThankYou_B2C.aspx");

                this.ClientScript.RegisterStartupScript(typeof(Page), "Thanks", "showThankyou();", true);
            }
        }
    }
}
