using System;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.IO;
using Enterprise;

public class EmailGateway {
    //Members
    private string mSalesMailbox = "";
    
    //Interface
    public EmailGateway() {
        //Constructor
        this.mSalesMailbox = WebConfigurationManager.AppSettings["SalesMailbox"].ToString();
    }
    public bool SendContactUsMessage(string from,string subject,string body) {
        //
        bool retValue = false;
        if (body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(from,this.mSalesMailbox,subject,false,body);
            retValue = true;
        }
        return retValue;
    }
}
