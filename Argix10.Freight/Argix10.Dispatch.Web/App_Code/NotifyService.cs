using System;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.IO;
using Argix.Enterprise;

namespace Argix.Freight {
    //
    public class NotifyService {
        //Members
        private string mEmailAdmin = "";

        private const string HTML_CONFIRM = "~\\Email\\PickupConfirmation.htm";
        private const string HTML_CANCEL = "~\\Email\\PickupCancelled.htm";

        //Interface
        public NotifyService() {
            //Constructor
            //Read configuration values for this service
            this.mEmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"].ToString();
        }
        #region Accessors [Members...]
        public string AdminEmailAddress { get { return this.mEmailAdmin; } }
        #endregion
        public void NotifyPickupConfirmation(string email, string pickupID) {
            try {
                //Notify welcome
                string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CONFIRM));
                body = body.Replace("lblPickupID",pickupID);
                new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,email,"Confirmation",true,body);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }

        }
        public void NotifyPickupCancelled(string email,string pickupID) {
            try {
                //Notify welcome
                string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CANCEL));
                body = body.Replace("lblPickupID",pickupID);
                new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,email,"Cancellation",true,body);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }

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
