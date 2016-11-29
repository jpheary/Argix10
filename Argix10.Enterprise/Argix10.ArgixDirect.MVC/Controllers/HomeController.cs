using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Argix.Models;

namespace Argix.Controllers {
    [HandleError]
    public class HomeController:Controller {
        public ActionResult Index() { return View(); }
        public ActionResult ClientLogin() { return View(); }
        public ActionResult ContactUs() { return View(); }
        [HttpPost]
        public ActionResult ContactUs(ContactModel model) {
            if(ModelState.IsValid) {
                string request =    (model.Brochure ? "Send Brochure" : "") +  
                                    (model.Assessment ? "\nProvide Free Logistics Assessment" : "") + 
                                    (model.Tour ? "\nArrange Retail Sort Center Tour" : "") + 
                                    (model.CallMe ? "\nCall Me" : "") + 
                                    (model.EmailMe ? "\nEmail Me" : "");
                string message = model.Name + " has submitted a request for information. \n\n" + request  + "\n\n" + 
                                "Below is their information:" + "\n" + "==========================" + "\n" +
                                "Name: " + model.Name + "\n" + "Title: " + model.Title + "\n" + 
                                "Company: " + model.Company + "\n" + "Address: " + model.Address + "\n" + 
                                "Email: " + model.Email + "\n" + "Phone: " + model.Phone + "\n" + "Fax: " + model.Fax;
                EmailServices email = new EmailServices();
                if(email.SendMail(model.Email, "MESSAGE FROM ARGIX DIRECT WEBSITE", message))
                    return RedirectToAction("ThankYou","Home");
                else
                    ModelState.AddModelError("","Failed to send contact information; please try again.");
            }
            return View(model);
        }
        public ActionResult ThankYou() { return View(); }
        public ActionResult Testimonials() { return View(); }
    }
}
