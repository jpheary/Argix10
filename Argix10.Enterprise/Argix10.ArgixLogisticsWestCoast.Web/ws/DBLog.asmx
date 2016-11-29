<%@ WebService Language="C#" Class="Argix_Contact.DBLog" %>
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;
using System.IO;

namespace Argix_Contact
{
    /// <summary>
    /// This service is used to send the email information of Argix Contact details
    /// </summary>
    [WebService(Namespace = "http://Argix.com/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class DBLog: System.Web.Services.WebService
    {
        [WebMethod]
        public bool ProcessRequest(string FirstName, string LastName, string Company, string Title, string Email, string PhoneNumber, bool Option1, bool Option2, bool Option3, bool Option4, bool Option5, bool Option6,string Path)
        {
			bool retVal = false;
            SmtpClient oSmtp = new SmtpClient();
            MailMessage oMail;
            string AdminEmail="";
            string mailContent; 
			
            try
            {
                //Admin Email
                oMail = new MailMessage();
                if(ConfigurationManager.AppSettings["AdminEmail"].ToString().Split(',').Length<=1)
                {
                    AdminEmail = ConfigurationManager.AppSettings["AdminEmail"].ToString();
                    oMail.To.Add(new MailAddress(AdminEmail, "Contact Submission"));
                }
                else
                {
                    foreach(string email in ConfigurationManager.AppSettings["AdminEmail"].ToString().Split(','))
                    {
                        oMail.To.Add(new MailAddress(email, "Contact Submission"));
                    }
                }
                
                oMail.Subject = "Landing Page Inquiry – Contact Details";
                mailContent = File.ReadAllText(HttpContext.Current.Server.MapPath("~/ws") + "/AdminTemplate.htm");
                mailContent = mailContent.Replace("{0}", FirstName);
                mailContent = mailContent.Replace("{1}", LastName);
                mailContent = mailContent.Replace("{2}", Company);
                mailContent = mailContent.Replace("{3}", Title);
                mailContent = mailContent.Replace("{4}", Email);
                mailContent = mailContent.Replace("{5}", PhoneNumber);
                mailContent = mailContent.Replace("{6}", Option1.ToString());
                mailContent = mailContent.Replace("{7}", Option2.ToString());
                mailContent = mailContent.Replace("{8}", Option3.ToString());
                mailContent = mailContent.Replace("{9}", Option4.ToString());
                mailContent = mailContent.Replace("{10}", Option5.ToString());
                mailContent = mailContent.Replace("{11}", Option6.ToString());
                mailContent = mailContent.Replace("{12}", Path);
                oMail.Body = mailContent;
                oMail.IsBodyHtml = true;
                oSmtp.Send(oMail);
                oMail.Dispose();


                //User Email
                oMail = new MailMessage();
                if (Email.Split(',').Length <= 1)
                {
                    oMail.To.Add(new MailAddress(Email, "Argix Logistics"));
                }
                else
                {
                    foreach (string email in Email.Split(','))
                    {
                        oMail.To.Add(new MailAddress(email, "Argix Logistics"));
                    }
                }
                oMail.Subject = "Thank you for contacting Argix Logistics";
                mailContent = File.ReadAllText(HttpContext.Current.Server.MapPath("~/ws") + "/UserTemplate.htm").Replace("{0}",FirstName);
                string virtualPath = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority) + HttpRuntime.AppDomainAppVirtualPath + "/ws/logo.jpg";
                mailContent = mailContent.Replace("{logo}", virtualPath);
                oMail.Body = mailContent;
                oMail.IsBodyHtml = true;
                oSmtp.Send(oMail);

                SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ApplicationServices"].ConnectionString);
                SqlCommand cmd = con.CreateCommand();
                try
                {
                    cmd.Parameters.Add("@firstName", SqlDbType.NVarChar, 200).Value = FirstName;
                    cmd.Parameters.Add("@lastName", SqlDbType.NVarChar, 200).Value = LastName;
                    cmd.Parameters.Add("@company", SqlDbType.NVarChar, 300).Value = Company;
                    cmd.Parameters.Add("@title", SqlDbType.NVarChar, 150).Value = Title;
                    cmd.Parameters.Add("@email", SqlDbType.NVarChar, 500).Value = Email;
                    cmd.Parameters.Add("@phoneNumber", SqlDbType.NVarChar, 50).Value = PhoneNumber;
                    cmd.Parameters.Add("@deconsolidation", SqlDbType.NVarChar, 5).Value = Option1;
                    cmd.Parameters.Add("@DCByPass", SqlDbType.NVarChar, 5).Value = Option2;
                    cmd.Parameters.Add("@warehousing", SqlDbType.NVarChar, 5).Value = Option3;
                    cmd.Parameters.Add("@fulfillment", SqlDbType.NVarChar, 5).Value = Option4;
                    cmd.Parameters.Add("@WMSIntegration", SqlDbType.NVarChar, 5).Value = Option5;
                    cmd.Parameters.Add("@customShipping", SqlDbType.NVarChar, 5).Value = Option6;
                    
                    cmd.CommandText = "Insert Into Argix_Contacts([FirstName],[LastName],[Company],[Title],[Email],[PhoneNumber],[Deconsolidation],[DCByPass],[Warehousing],[Fulfillment],[WMSIntegration],[CustomShipping]) Values(@firstName, @lastName, @company, @title, @email, @phoneNumber, @deconsolidation, @DCByPass, @warehousing, @fulfillment, @WMSIntegration, @customShipping)";
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
                
				retVal = true;	
            }
            catch { }
            finally
            {
                oSmtp = null;
            }
            return retVal;
        }
    }
}
