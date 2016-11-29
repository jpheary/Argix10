using System;
using System.Net.Mail;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.IO;

namespace Argix.Models {
    public class EmailGateway {
        //Members
        private string mSMTPServer = "";
        private string mCRGMailbox = "";
        private string mSalesMailbox = "";

        private const string HTML_PASSWORD_RESET = "\\messages\\PasswordReset.htm";
        private const string HTML_REGISTRATION = "\\messages\\Registration.htm";

        //Interface
        public EmailGateway() {
            //Constructor
            //Read configuration values for this service
            this.mSMTPServer = WebConfigurationManager.AppSettings["SMTPServer"].ToString();
            this.mCRGMailbox = WebConfigurationManager.AppSettings["CRGMailbox"].ToString();
            this.mSalesMailbox = WebConfigurationManager.AppSettings["SalesMailbox"].ToString();
        }
        public bool SendContactUsMessage(string from,string subject,string body) {
            //
            bool retValue = false;
            MailMessage msg = new MailMessage(from,this.mSalesMailbox,subject,body);
            msg.BodyEncoding = System.Text.Encoding.UTF8;
            msg.IsBodyHtml = false;
            if (msg.Body.Length > 0) {
                SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
                smtpClient.Send(msg);
                retValue = true;
            }
            return retValue;
        }
        public bool SendRegistrationMessage(string userName,string toEmailAddress) {
            //
            bool retValue = false;
            //MailMessage msg = getRegisterReceiptMessage(userName, toEmailAddress);
            MailMessage email = new MailMessage(this.mCRGMailbox,toEmailAddress);
            email.Subject = "Argix Logistics Tracking System - Registration";
            MailAddress copy = new MailAddress(this.mCRGMailbox);
            email.Bcc.Add(copy);
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = true;
            email.Body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_REGISTRATION).Replace("##",userName);
            if (email.Body.Length > 0) {
                SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
                smtpClient.Send(email);
                retValue = true;
            }
            return retValue;
        }
        public bool SendPasswordResetMessage(string userName,string toEmailAddress,string password) {
            //
            bool retValue = false;
            //MailMessage msg = getPasswordResetMessage(userName,toEmailAddress,password);
            MailMessage email = new MailMessage(this.mCRGMailbox,toEmailAddress);
            email.Subject = "Argix Logistics Tracking System notification";
            email.BodyEncoding = System.Text.Encoding.UTF8;
            email.IsBodyHtml = true;
            email.Body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_PASSWORD_RESET).Replace("**",userName).Replace("##",password);
            if (email.Body.Length > 0) {
                SmtpClient smtpClient = new SmtpClient(this.mSMTPServer);
                smtpClient.Send(email);
                retValue = true;
            }
            return retValue;
        }
        #region Local Services: getHTMLBody()
        private string getHTMLBody(string filePath) {
            //
            string bodyText = "";
            FileInfo mailFileInfo = new FileInfo(filePath);
            StreamReader reader = mailFileInfo.OpenText();
            bodyText = reader.ReadToEnd();
            return bodyText;
        }
        #endregion
    }
}
