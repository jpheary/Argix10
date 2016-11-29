using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Web.Configuration;

namespace Argix.Enterprise {
	//
	public class SMTPGateway {
		//Members
        private string mEmailFrom = "extranet.support@argixlogistics.com";
        private string mEmailIT = "jheary@argixlogistics.com";

		//Interface
        public SMTPGateway() {
            //Constructor
            //Read configuration values for this service
            this.mEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"].ToString();
            this.mEmailIT = WebConfigurationManager.AppSettings["EmailIT"].ToString();
        }
        #region Accessors: FromEmailAddress, ITEmailAddress
        public string FromEmailAddress { get { return this.mEmailFrom; } }
        public string ITEmailAddress { get { return this.mEmailIT; } }
        #endregion
        public CommunicationState ServiceState { get { return new SMTPServiceClient().State; } }
        public string ServiceAddress { get { return new SMTPServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public void SendMailMessage(string fromEmailAddress,string toEmailAddress,string subject,bool isBodyHtml,string body) {
            //
            SMTPServiceClient client = null;
            try {
                client = new SMTPServiceClient();
                client.SendMailMessage(fromEmailAddress,toEmailAddress,subject,isBodyHtml,body);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public void SendMailMessage(string fromEmailAddress,string toEmailAddress,string subject,bool isBodyHtml,string body,string bccEmailAddress) {
            //
            SMTPServiceClient client = null;
            try {
                client = new SMTPServiceClient();
                client.SendMailMessageWithBlindCopy(fromEmailAddress,toEmailAddress,subject,isBodyHtml,body,bccEmailAddress);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public void SendITNotification(string username, Exception ex) {
            //
            try {
                if(this.mEmailIT.Length > 0) {
                    string subject = "Argix10.Customers.Services Error";
                    string body = "Username: " + username + "\r\n";
                    body += "\r\n";
                    body += "Exception:\r\n" + ex.ToString() + "\r\n";

                    SendMailMessage(this.mEmailFrom, this.mEmailIT, subject, false, body);
                }
            }
            catch { }
        }
    }
}