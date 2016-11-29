using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Argix.Models;
using Argix.Areas.Mobile.Models;

namespace Argix.Areas.Mobile.Controllers {
    //
    public class HomeController : Controller {
        //Members
        private const string USER_LOGIN_NOACCOUNT = "Your account does not exist. Make sure you have entered a correct user id. If you never registered before then register first.";
        private const string USER_LOGIN_FAILED = "Login attempt failed. Your user id or password did not match.";
        private const string USER_ACCOUNT_LOCKEDOUT = "Your account is locked out. Please contact customer support at extranet.support@argixlogistics.com to resolve the issue.";
        private const string USER_ACCOUNT_INACTIVE = "Your account is currently inactive.";
        private const string PASSWORD_MIN_LENGHT = "Password should be minimum 6 characters long.";
        private const string EMAIL_NOT_UNIQUE = "Your email address already exists.";
        private const string USERID_NOT_UNIQUE = "This login UserID already exists.";

        //Interface
        public ActionResult Index() { return View(); }
        public ActionResult Brands_Served() { return View(); }
        public ActionResult Difference() { return View(); }
        public ActionResult Distribution() { return View(); }
        public ActionResult Supply_Chain() { return View(); }
        public ActionResult Technology_Difference() { return View(); }
        public ActionResult Transportation() { return View(); }
        public ActionResult Contact() { return View(); }
        [HttpPost]
        public ActionResult Contact(ContactModel model) {
            if (ModelState.IsValid) {
                //
                string message = "Argix Logistics Contact Form submission on " + DateTime.Now.ToString() + ". Below are the details:\r\n\r\n";
                if (model.Name.Length > 0) message += "Name: " + model.Name + "\r\n";
                if (model.Company !=null && model.Company.Length > 0) message += "Company: " + model.Company + "\r\n";
                if (model.Title != null && model.Title.Length > 0) message += "Title: " + model.Title + "\r\n";
                if (model.Email.Length > 0) message += "Email Address: " + model.Email + "\r\n";
                if (model.Phone != null && model.Phone.Length > 0) message += "Telephone: " + model.Phone + "\r\n";
                if (model.Address != null && model.Address.Length > 0) message += "Address: " + model.Address + "\r\n\r\n";

                string services = (model.SendBrochure ? "Send Brochure" : "") + (model.FreeAssessment ? "\nFree Assessment" : "") + (model.ScheduleTour ? "\nSchedule Tour" : "") + (model.ContactMe ? "\nContact Me" : "");
                if (services.Length > 0) message += "Services: " + services + "\r\n\r\n";

                if (model.AdditionalRequests.Length > 0) message += "Additional Requests: " + model.AdditionalRequests + "\r\n";

                EmailGateway email = new EmailGateway();
                if (email.SendContactUsMessage(model.Email,"MESSAGE FROM ARGIX LOGISTICS WEBSITE",message))
                    return RedirectToAction("ThankYou","Home");
                else
                    ModelState.AddModelError("","Failed to send contact information; please try again.");
            }
            return View(model);
        }
        public ActionResult ThankYou() { return View(); }
        //public ActionResult Login() { return RedirectToAction("Login","Tracking"); }
        public ActionResult Login() { return View(); }
        [HttpPost]
        public ActionResult Login(LoginModel model) {
            if (ModelState.IsValid) {
                //Validate login
                MembershipUserCollection users = Membership.FindUsersByName(model.UserName);
                if (users.Count == 0) { ModelState.AddModelError("",USER_LOGIN_NOACCOUNT); return View(model); }
                if (users[model.UserName].IsLockedOut) { ModelState.AddModelError("",USER_ACCOUNT_LOCKEDOUT); return View(model); }
                if (!users[model.UserName].IsApproved) { ModelState.AddModelError("",USER_ACCOUNT_INACTIVE); return View(model); }

                //Validate userid/password
                if (Membership.ValidateUser(model.UserName,model.Password)) {
                    //Create an authentication ticket
                    FormsAuthentication.SetAuthCookie(model.UserName,false);

                    //Force user to change the password if required as per policy (i.e. password reset or expired)
                    //if (isPasswordExpired)
                    //    return RedirectToAction("ChangePassword","Acccount");
                    //else {
                    return RedirectToAction("Tracking","Tracking");
                    //}
                }
                else { ModelState.AddModelError("",USER_LOGIN_FAILED); return View(model); }
            }
            else
                return View(model);
        }
    }
}
