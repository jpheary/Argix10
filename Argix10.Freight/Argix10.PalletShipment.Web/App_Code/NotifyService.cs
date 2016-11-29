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
        private string mEmailAdmin = "",mSalesMailbox="";

        private const string HTML_WELCOME = "~\\EmailMessages\\Welcome.htm";
        private const string HTML_CONFIRM = "~\\EmailMessages\\PickupConfirmation.htm";
        private const string HTML_CANCEL = "~\\EmailMessages\\PickupCancelled.htm";
        private const string HTML_PASSWORD_REQUEST = "~\\EmailMessages\\PasswordRequest.htm";
        private const string HTML_SALESREP_REQUEST = "~\\EmailMessages\\SalesRepRequest.htm";

        //Interface
        public NotifyService() {
            //Constructor
            try {
                //Read configuration values for this service
                this.mEmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"].ToString();
                this.mSalesMailbox = WebConfigurationManager.AppSettings["SalesMailbox"].ToString();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors [Members...]
        public string AdminEmailAddress { get { return this.mEmailAdmin; } }
        #endregion
        public void NotifyWelcome(string userName, string email, string password,LTLClient client) {
            try {
                //Notify welcome
                if (email.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_WELCOME));
                    body = body.Replace("txtUserName",userName);
                    body = body.Replace("txtPassword",password);
                    body = body.Replace("txtCompanyName",client.Name);
                    body = body.Replace("txtCompanyStreet",client.AddressLine1);
                    body = body.Replace("txtCompanyCity",client.City);
                    body = body.Replace("txtCompanyState",client.State);
                    body = body.Replace("txtCompanyZip",client.Zip);
                    body = body.Replace("txtContactName",client.ContactName);
                    body = body.Replace("txtContactPhone",client.ContactPhone);
                    body = body.Replace("txtContactEmail",client.ContactEmail);
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,email,"Pallet Shipment Customer Enrollment",true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }

        }
        public void NotifyPickupConfirmation(string email,string pickupID,Argix.Freight.Client.PickupRequest pickup) {
            try {
                //Notify client of pickup confirmation
                if (email.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CONFIRM));
                    body = body.Replace("lblPickupID",pickupID);
                    body = body.Replace("txtPickupDate",pickup.ScheduleDate.ToString("MM/dd/yyyy"));
                    body = body.Replace("txtShipperName",pickup.Shipper);
                    body = body.Replace("txtShipperAddress",pickup.ShipperAddress);
                    body = body.Replace("txtShipperPhone",pickup.ShipperPhone);
                    body = body.Replace("txtWindowOpen",pickup.WindowOpen.ToString());
                    body = body.Replace("txtWindowClose",pickup.WindowClose.ToString());
                    body = body.Replace("txtQuantity",pickup.Amount.ToString());
                    body = body.Replace("txtWeight",pickup.Weight.ToString());
                    body = body.Replace("txtComments",pickup.Comments);
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,email,"Pallet Shipment Confirmation",true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }

        }
        public void NotifyPickupCancelled(string email,string pickupID,Argix.Freight.Client.PickupRequest pickup) {
            try {
                //Notify client of pickup cancelled
                if (email.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CANCEL));
                    body = body.Replace("lblPickupID",pickupID);
                    body = body.Replace("txtPickupDate",pickup.ScheduleDate.ToString("MM/dd/yyyy"));
                    body = body.Replace("txtShipperName",pickup.Shipper);
                    body = body.Replace("txtShipperAddress",pickup.ShipperAddress);
                    body = body.Replace("txtShipperPhone",pickup.ShipperPhone);
                    body = body.Replace("txtWindowOpen",pickup.WindowOpen.ToString());
                    body = body.Replace("txtWindowClose",pickup.WindowClose.ToString());
                    body = body.Replace("txtQuantity",pickup.Amount.ToString());
                    body = body.Replace("txtWeight",pickup.Weight.ToString());
                    body = body.Replace("txtComments",pickup.Comments);
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,email,"Pallet Shipment Cancellation",true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
        }
        public bool SendPasswordResetMessage(string userName,string email,string password) {
            //
            bool retValue = false;
            try {
                string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_PASSWORD_REQUEST));
                body = body.Replace("txtUserName",userName);
                body = body.Replace("txtPassword",password);
                if (body.Length > 0) {
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,email,"Pallet Shipment Password Reset",true,body);
                    retValue = true;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return retValue;
        }
        public void NotifySalesRepRequest(Argix.Freight.LTLClient client) {
            try {
                //Notify Customer Service
                string title = "Pallet Shipment Sales Rep Request";
                string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_SALESREP_REQUEST));
                body = body.Replace("*clientID*",client.ID.ToString());
                body = body.Replace("txtCompanyName",client.Name);
                body = body.Replace("txtCompanyStreet",client.AddressLine1);
                body = body.Replace("txtCompanyCity",client.City);
                body = body.Replace("txtCompanyState",client.State);
                body = body.Replace("txtCompanyZip",client.Zip);
                body = body.Replace("txtContactName",client.ContactName);
                body = body.Replace("txtContactPhone",client.ContactPhone);
                body = body.Replace("txtContactEmail",client.ContactEmail);
                new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,this.mSalesMailbox,title,true,body);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }

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
