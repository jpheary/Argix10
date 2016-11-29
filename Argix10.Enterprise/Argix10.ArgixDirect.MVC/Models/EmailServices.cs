using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Hosting;
using System.IO;

namespace Argix.Models {
    //
    public class EmailServices {
        //Members
        private string mSMTPServer = "";
        private string mEmailTo = "";

        //Interface
        public EmailServices() {
            //Constructor
            this.mSMTPServer = WebConfigurationManager.AppSettings["SMTPServer"].ToString();
            this.mEmailTo = WebConfigurationManager.AppSettings["EmailTo"].ToString();
        }

        public bool SendMail(string from,string subject,string body) {
            //
            bool retValue = false;
            MailMessage msg = new MailMessage(from,this.mEmailTo,subject,body);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            if(msg.Body.Length > 0) {
                SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
                smtpClient.Send(msg);
                retValue = true;
            }
            return retValue;
        }
    }
}
