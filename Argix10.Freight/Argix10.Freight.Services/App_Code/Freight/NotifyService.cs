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
        private string mEmailAdmin = "",mEmailFinance = "";

        private const string HTML_CLIENT_CREATED = "~\\EmailMessages\\ClientCreated.htm";
        private const string HTML_CLIENT_APPROVAL = "~\\EmailMessages\\ClientApproval.htm";
        private const string HTML_SHIPMENT_CREATED = "~\\EmailMessages\\ShipmentCreated.htm";
        private const string HTML_SHIPMENT_UPDATED = "~\\EmailMessages\\ShipmentUpdated.htm";
        private const string HTML_SHIPMENT_CANCELLED = "~\\EmailMessages\\ShipmentCancelled.htm";

        //Interface
        public NotifyService() {
            //Constructor
            //Read configuration values for this service
            try {
                this.mEmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"].ToString();
                this.mEmailFinance = WebConfigurationManager.AppSettings["EmailFinance"].ToString();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors [Members...]
        public string AdminEmailAddress { get { return this.mEmailAdmin; } }
        public string FinanceEmailAddress { get { return this.mEmailFinance; } }
        #endregion
        public void NotifyClientCreated(LTLClient client) {
            try {
                //Notify finance
                string title = "New Pallet Shipment Client";
                string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CLIENT_CREATED));
                body = body.Replace("*clientID*",client.ID.ToString());
                body = body.Replace("*Status*","NEW");
                body = body.Replace("txtCompanyName",client.Name);
                body = body.Replace("txtCompanyStreet",client.AddressLine1);
                body = body.Replace("txtCompanyCity",client.City);
                body = body.Replace("txtCompanyState",client.State);
                body = body.Replace("txtCompanyZip",client.Zip);
                body = body.Replace("txtContactName",client.ContactName);
                body = body.Replace("txtContactPhone",client.ContactPhone);
                body = body.Replace("txtContactEmail",client.ContactEmail);
                new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,this.mEmailFinance,title,true,body);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }

        }
        public void NotifyClientApproval(LTLClient client,bool approved,LTLClient salesRep) {
            try {
                //Send conformation email
                if (client.ContactEmail.Trim().Length > 0) {
                    string title = approved ? "Client Status Approved" : "Client Status Declined";
                    string status = approved ? "APPROVED" : "DECLINED";
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CLIENT_APPROVAL));
                    body = body.Replace("*clientID*",client.ID.ToString());
                    body = body.Replace("*Status*",status);
                    body = body.Replace("txtCompanyName",client.Name);
                    body = body.Replace("txtCompanyStreet",client.AddressLine1);
                    body = body.Replace("txtCompanyCity",client.City);
                    body = body.Replace("txtCompanyState",client.State);
                    body = body.Replace("txtCompanyZip",client.Zip);
                    body = body.Replace("txtContactName",client.ContactName);
                    body = body.Replace("txtContactPhone",client.ContactPhone);
                    body = body.Replace("txtContactEmail",client.ContactEmail);
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,client.ContactEmail,title,true,body);
                    if (salesRep != null) new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,salesRep.ContactEmail,title,true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }

        }
        public void NotifyShipmentCreated(LTLClient client,LTLShipper shipper,LTLConsignee consignee,LTLShipment shipment,int shipmentID,LTLClient salesRep) {
            try {
                //Send conformation email
                if (client.ContactEmail.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_SHIPMENT_CREATED));
                    body = body.Replace("*shipmentNumber*",shipmentID.ToString());
                    body = body.Replace("*clientName*",client.Name);
                    body = body.Replace("*clientAddress*",client.AddressLine1 + " " + client.City + ", " + client.State + " " + client.Zip);
                    body = body.Replace("*clientContact*",client.ContactName);
                    body = body.Replace("txtShipDate",shipment.ShipDate.ToString("MM/dd/yyyy"));
                    body = body.Replace("txtOrigin",shipper.Name + " (" + shipper.Zip + ")");
                    body = body.Replace("txtDest",consignee.Name + " (" + consignee.Zip + ")");
                    body = body.Replace("txtPallets",shipment.Pallets.ToString());
                    body = body.Replace("txtWeight",shipment.Weight.ToString());
                    body = body.Replace("txtRate",shipment.PalletRate.ToString());
                    body = body.Replace("txtFSC",shipment.FuelSurcharge.ToString());
                    body = body.Replace("txtAccessorial",shipment.AccessorialCharge.ToString());
                    body = body.Replace("txtInsurance",shipment.InsuranceCharge.ToString());
                    body = body.Replace("txtTSC",shipment.TollCharge.ToString());
                    body = body.Replace("txtCharges",shipment.TotalCharge.ToString());
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,client.ContactEmail,"Shipment Booked",true,body);
                    if (salesRep != null) new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,salesRep.ContactEmail,"Shipment Booked",true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }

        }

        public void NotifyClientCreated2(LTLClient2 client) {
            try {
                //Notify finance
                string title = "New Pallet Shipment Client";
                string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CLIENT_CREATED));
                body = body.Replace("*clientID*",client.ID.ToString());
                body = body.Replace("*Status*","NEW");
                body = body.Replace("txtCompanyName",client.Name);
                body = body.Replace("txtCompanyStreet",client.AddressLine1);
                body = body.Replace("txtCompanyCity",client.City);
                body = body.Replace("txtCompanyState",client.State);
                body = body.Replace("txtCompanyZip",client.Zip);
                body = body.Replace("txtContactName",client.ContactName);
                body = body.Replace("txtContactPhone",client.ContactPhone);
                body = body.Replace("txtContactEmail",client.ContactEmail);
                new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,this.mEmailFinance,title,true,body);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }

        }
        public void NotifyClientApproval2(LTLClient2 client,bool approved,LTLClient2 salesRep) {
            try {
                //Send conformation email
                if (client.ContactEmail.Trim().Length > 0) {
                    string title = approved ? "Client Status Approved" : "Client Status Declined";
                    string status = approved ? "APPROVED" : "DECLINED";
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_CLIENT_APPROVAL));
                    body = body.Replace("*clientID*",client.ID.ToString());
                    body = body.Replace("*Status*",status);
                    body = body.Replace("txtCompanyName",client.Name);
                    body = body.Replace("txtCompanyStreet",client.AddressLine1);
                    body = body.Replace("txtCompanyCity",client.City);
                    body = body.Replace("txtCompanyState",client.State);
                    body = body.Replace("txtCompanyZip",client.Zip);
                    body = body.Replace("txtContactName",client.ContactName);
                    body = body.Replace("txtContactPhone",client.ContactPhone);
                    body = body.Replace("txtContactEmail",client.ContactEmail);
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,client.ContactEmail,title,true,body);
                    if (salesRep != null) new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,salesRep.ContactEmail,title,true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }

        }
        public void NotifyShipmentCreated2(LTLClient2 client,LTLShipper2 shipper,LTLConsignee2 consignee,LTLShipment2 shipment,string shipmentNumber,LTLClient2 salesRep) {
            try {
                //Send conformation email
                if (client.ContactEmail.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_SHIPMENT_CREATED));
                    body = body.Replace("*shipmentNumber*",shipmentNumber);
                    body = body.Replace("*clientName*",client.Name);
                    body = body.Replace("*clientAddress*",client.AddressLine1 + " " + client.City + ", " + client.State + " " + client.Zip);
                    body = body.Replace("*clientContact*",client.ContactName);
                    body = body.Replace("txtShipDate",shipment.ShipDate.ToString("MM/dd/yyyy"));
                    body = body.Replace("txtOrigin",shipper.Name + " (" + shipper.Zip + ")");
                    body = body.Replace("txtDest",consignee.Name + " (" + consignee.Zip + ")");
                    body = body.Replace("txtPallets",shipment.Pallets.ToString());
                    body = body.Replace("txtWeight",shipment.Weight.ToString());
                    body = body.Replace("txtRate",shipment.PalletRate.ToString());
                    body = body.Replace("txtFSC",shipment.FuelSurcharge.ToString());
                    body = body.Replace("txtAccessorial",shipment.AccessorialCharge.ToString());
                    body = body.Replace("txtInsurance",shipment.InsuranceCharge.ToString());
                    body = body.Replace("txtTSC",shipment.TollCharge.ToString());
                    body = body.Replace("txtCharges",shipment.TotalCharge.ToString());
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,client.ContactEmail,"Shipment Booked",true,body);
                    if (salesRep != null) new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,salesRep.ContactEmail,"Shipment Booked",true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }

        }
        public void NotifyShipmentUpdated2(LTLClient2 client,LTLShipper2 shipper,LTLConsignee2 consignee,LTLShipment2 shipment,string shipmentNumber,LTLClient2 salesRep) {
            try {
                //Send conformation email
                if (client.ContactEmail.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_SHIPMENT_UPDATED));
                    body = body.Replace("*shipmentNumber*",shipmentNumber);
                    body = body.Replace("*clientName*",client.Name);
                    body = body.Replace("*clientAddress*",client.AddressLine1 + " " + client.City + ", " + client.State + " " + client.Zip);
                    body = body.Replace("*clientContact*",client.ContactName);
                    body = body.Replace("txtShipDate",shipment.ShipDate.ToString("MM/dd/yyyy"));
                    body = body.Replace("txtOrigin",shipper.Name + " (" + shipper.Zip + ")");
                    body = body.Replace("txtDest",consignee.Name + " (" + consignee.Zip + ")");
                    body = body.Replace("txtPallets",shipment.Pallets.ToString());
                    body = body.Replace("txtWeight",shipment.Weight.ToString());
                    body = body.Replace("txtRate",shipment.PalletRate.ToString());
                    body = body.Replace("txtFSC",shipment.FuelSurcharge.ToString());
                    body = body.Replace("txtAccessorial",shipment.AccessorialCharge.ToString());
                    body = body.Replace("txtInsurance",shipment.InsuranceCharge.ToString());
                    body = body.Replace("txtTSC",shipment.TollCharge.ToString());
                    body = body.Replace("txtCharges",shipment.TotalCharge.ToString());
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,client.ContactEmail,"Shipment Booked",true,body);
                    if (salesRep != null) new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,salesRep.ContactEmail,"Shipment Booked",true,body);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }

        }
        public void NotifyShipmentCancelled2(LTLClient2 client,LTLShipper2 shipper,LTLConsignee2 consignee,LTLShipment2 shipment,string shipmentNumber,LTLClient2 salesRep) {
            try {
                //Send conformation email
                if (client.ContactEmail.Trim().Length > 0) {
                    string body = getHTMLBody(System.Web.Hosting.HostingEnvironment.MapPath(HTML_SHIPMENT_CANCELLED));
                    body = body.Replace("*shipmentNumber*",shipmentNumber);
                    body = body.Replace("*clientName*",client.Name);
                    body = body.Replace("*clientAddress*",client.AddressLine1 + " " + client.City + ", " + client.State + " " + client.Zip);
                    body = body.Replace("*clientContact*",client.ContactName);
                    body = body.Replace("txtShipDate",shipment.ShipDate.ToString("MM/dd/yyyy"));
                    body = body.Replace("txtOrigin",shipper.Name + " (" + shipper.Zip + ")");
                    body = body.Replace("txtDest",consignee.Name + " (" + consignee.Zip + ")");
                    body = body.Replace("txtPallets",shipment.Pallets.ToString());
                    body = body.Replace("txtWeight",shipment.Weight.ToString());
                    body = body.Replace("txtRate",shipment.PalletRate.ToString());
                    body = body.Replace("txtFSC",shipment.FuelSurcharge.ToString());
                    body = body.Replace("txtAccessorial",shipment.AccessorialCharge.ToString());
                    body = body.Replace("txtInsurance",shipment.InsuranceCharge.ToString());
                    body = body.Replace("txtTSC",shipment.TollCharge.ToString());
                    body = body.Replace("txtCharges",shipment.TotalCharge.ToString());
                    new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,client.ContactEmail,"Shipment Cancelled",true,body);
                    if (salesRep != null) new Argix.Enterprise.SMTPGateway().SendMailMessage(this.mEmailAdmin,salesRep.ContactEmail,"Shipment Cancelled",true,body);
                }
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
