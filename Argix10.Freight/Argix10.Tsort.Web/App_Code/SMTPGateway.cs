using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Enterprise {
	//
	public class SMTPGateway {
		//Members
        private const string EMAIL_ADMIN = "extranet.support@argixlogistics.com";
        private const string EMAIL_IT = "jheary@argixlogistics.com";

		//Interface
        public SMTPGateway() { }
        public CommunicationState ServiceState { get { return new SMTPServiceClient().State; } }
        public string ServiceAddress { get { return new SMTPServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public void SendITNotification(string username,Exception ex) {
            //
            try {
                if (EMAIL_IT.Length > 0) {
                    string subject = "Tsort Error";
                    string body = "Username: " + username + "\r\n";
                    body += "\r\n";
                    body += "Exception:\r\n" + ex.ToString() + "\r\n";

                    SendMailMessage(EMAIL_ADMIN,EMAIL_IT,subject,false,body);
                }
            }
            catch { }
        }

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
    }
}