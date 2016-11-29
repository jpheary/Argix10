using System;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using System.IO;
using Argix;

public class EmailGateway {
    //Members
    private string mEmailFrom = "", mEmailAdmin = "", mEmailPODReq = "", mEmailIT="";
    
    private const string HTML_PASSWORD_RESET = "\\EmailMsgs\\PasswordReset.htm";
    private const string HTML_REGISTER_RECEIPT = "\\EmailMsgs\\RegistrationReceipt.htm";
    private const string HTML_REGISTER_REJECT = "\\EmailMsgs\\RejectRegistration.htm";
    private const string HTML_WELCOME = "\\EmailMsgs\\Welcome.htm";
    private const string HTML_POD_REQUEST = "\\EmailMsgs\\PODRequest.htm";
    private const string HTML_POD_REQCONFIRM = "\\EmailMsgs\\PODRequestConfirm.htm";

    //Interface
    public EmailGateway() {
        //Constructor
        //Read configuration values for this service
        this.mEmailFrom = WebConfigurationManager.AppSettings["EmailFrom"].ToString();
        this.mEmailAdmin = WebConfigurationManager.AppSettings["EmailAdmin"].ToString();
        this.mEmailPODReq = WebConfigurationManager.AppSettings["EmailPODReq"].ToString();
        this.mEmailIT = WebConfigurationManager.AppSettings["EmailIT"].ToString();
    }
    #region Accessors: SMTPServer, FromEmailAddress, AdminEmailAddress, PODReqEmailAddress
    public string FromEmailAddress { get { return this.mEmailFrom; } }
    public string AdminEmailAddress { get { return this.mEmailAdmin; } }
    public string PODReqEmailAddress { get { return this.mEmailPODReq; } }
    #endregion
    public bool SendRegisterReceiptEmail(string userName,string toEmailAddress) {
        //
        bool retValue = false;
        string subject = "Argix Logistics Tracking System - Registration";
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_REGISTER_RECEIPT).Replace("##",userName);
        if(body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(this.mEmailFrom,toEmailAddress,subject,true,body,this.mEmailAdmin);
            retValue = true;
        }
        return retValue;
    }
    public bool SendWelcomeMessage(string userFullName,string userID,string toEmailAddress) {
        //
        bool retValue = false;
        string subject = "Welcome to Argix Logistics Tracking System";
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_WELCOME).Replace("**",userID).Replace("##",userFullName);
        if(body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(this.mEmailFrom,toEmailAddress,subject,true,body);
            retValue = true;
        }
        return retValue;
    }
    public void SendPendingApprovalAlert() {
        //
        string subject = "New User Pending Approval Alert";
        string body = "A new user has registered from the site. Please go to the Tracking site for approval.";
        SMTPGateway smtpGateway = new SMTPGateway();
        smtpGateway.SendMailMessage(this.mEmailFrom,this.mEmailAdmin,subject,true,body);
    }
    public bool SendRejectRegistrationMessage(string userName,string toEmailAddress,string rejectReason) {
        //
        bool retValue = false;
        string subject = "Argix Logistics Tracking System - Registration";
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_REGISTER_REJECT).Replace("****",rejectReason);
        if(body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(this.mEmailFrom,toEmailAddress,subject,true,body);
            retValue = true;
        }
        return retValue;
    }
    public bool SendPasswordResetMessage(string userName,string toEmailAddress,string password) {
        //
        bool retValue = false;
        string subject = "Argix Logistics Tracking System notification";
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_PASSWORD_RESET).Replace("**",userName).Replace("##",password);
        if(body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(this.mEmailFrom,toEmailAddress,subject,true,body);
            retValue = true;
        }
        return retValue;
    }
    public bool SendPODRequest(MembershipUser user,Argix.Enterprise.TrackingItem item) {
        //
        bool retValue = false;
        string subject = "Argix Logistics POD Request";
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_POD_REQUEST);
        body = body.Replace("*carton*",item.CartonNumber.Trim());
        body = body.Replace("*user*",user.UserName);
        body = body.Replace("*email*",user.Email);
        body = body.Replace("*client*",item.Client.Trim() + "-" + item.ClientName.Trim());
        body = body.Replace("*store*",item.StoreNumber.ToString());
        body = body.Replace("*substore*",item.StoreName);
        body = body.Replace("*storeaddress*",item.StoreAddress1.Trim() + " " + item.StoreAddress2.Trim() + " " + item.StoreCity.Trim() + ", " + item.StoreState.Trim() + " " + item.StoreZip);
        body = body.Replace("*pickupdate*",item.PickupDate.Trim());
        body = body.Replace("*scheduleddelivery*",item.ActualStoreDeliveryDate);
        string podScan="";
        if(item.ScanType == 3) {
            if (item.PODScanDate.Trim().Length > 0) podScan = item.PODScanDate.Trim();
        }
        body = body.Replace("*actualdelivery*",podScan);
        body = body.Replace("*tl*",item.TLNumber.Trim());
        body = body.Replace("*cbol*",item.CBOL.Trim());
        body = body.Replace("*po*",item.PONumber.Trim());
        body = body.Replace("*pro*","");
        body = body.Replace("*shipment*",item.VendorKey.Trim());
        body = body.Replace("*bol*",item.BOLNumber.ToString());
        body = body.Replace("*weight*",item.Weight.ToString());
        body = body.Replace("*label*",item.LabelNumber.ToString());
        if(body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(this.mEmailFrom,this.mEmailPODReq,subject,true,body);
            retValue = true;
        }
        return retValue;
    }
    public bool SendPODRequestConfirm(MembershipUser user,Argix.Enterprise.TrackingItem item) {
        //
        bool retValue = false;
        string subject = "POD Request Confirmation";
        string body = getHTMLBody(HostingEnvironment.ApplicationPhysicalPath + HTML_POD_REQCONFIRM);
        body = body.Replace("*user*",user.UserName);
        body = body.Replace("*email*",user.Email);
        body = body.Replace("*store*",item.StoreNumber);
        body = body.Replace("*storename*",item.StoreName.Trim());
        body = body.Replace("*carton*",item.CartonNumber.Trim());
        body = body.Replace("*client*",item.Client.Trim() + "-" + item.ClientName.Trim());
        body = body.Replace("*vendor*",item.Vendor.Trim() + "-" + item.VendorName.Trim());
        body = body.Replace("*pickupdate*",item.PickupDate.Trim());
        body = body.Replace("*scheduleddelivery*",item.ActualStoreDeliveryDate);
        body = body.Replace("*shipment*",item.VendorKey.Trim());
        body = body.Replace("*bol*",item.BOLNumber.ToString());
        body = body.Replace("*tl*",item.TLNumber.Trim());
        body = body.Replace("*label*",item.LabelNumber.ToString());
        body = body.Replace("*po*",item.PONumber.Trim());
        body = body.Replace("*weight*",item.Weight.ToString());
        if(body.Length > 0) {
            SMTPGateway smtpGateway = new SMTPGateway();
            smtpGateway.SendMailMessage(this.mEmailFrom,user.Email,subject,true,body);
            retValue = true;
        }
        return retValue;
    }
    public void SendITNotification(string username,Exception ex) {
        //
        try {
            if (this.mEmailIT.Length > 0) {
                string subject = "Tracking Error";
                string body = "Username: " + username + "\r\n";
                body += "\r\n";
                body += "Exception:\r\n" + ex.ToString() + "\r\n";

                SMTPGateway smtpGateway = new SMTPGateway();
                smtpGateway.SendMailMessage(this.mEmailFrom,this.mEmailIT,subject,false,body);
            }
        }
        catch { }
    }
    public void SendITErrorComments(string username,Exception ex, string comments) {
        //
        try {
            if (this.mEmailIT.Length > 0) {
                string subject = "Tracking Error User Comments";
                string body = "Username: " + username + "\r\n";
                body += "\r\n";
                body += "Exception:\r\n" + ex.ToString() + "\r\n";
                body += "\r\n";
                body += "Comments:\r\n" + comments + "\r\n";

                SMTPGateway smtpGateway = new SMTPGateway();
                smtpGateway.SendMailMessage(this.mEmailFrom,this.mEmailIT,subject,false,body);
            }
        }
        catch { }
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
