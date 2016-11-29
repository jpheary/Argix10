using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Argix.Models;

namespace Argix.Controllers {
    //
    public class HomeController:Controller {
        //
        public ActionResult Index() { return View(); }

        public ActionResult ContactUs() { return View(); }
        [HttpPost]
        public ActionResult ContactUs(ContactModel model) {
            if(ModelState.IsValid) {
                //
                string message = "Argix Logistics Contact Form submission on " + DateTime.Now.ToString() + ". Below are the details:\r\n\r\n";
                if (model.Name.Length > 0) message += "Name: " + model.Name + "\r\n";
                if (model.Company.Length > 0) message += "Company: " + model.Company + "\r\n";
                if (model.Title.Length > 0) message += "Title: " + model.Title + "\r\n";
                if (model.Email.Length > 0) message += "Email Address: " + model.Email + "\r\n";
                if (model.Phone.Length > 0) message += "Telephone: " + model.Phone + "\r\n";
                if (model.Address.Length > 0) message += "Address: " + model.Address + "\r\n\r\n";

                string services = (model.SendBrochure ? "Send Brochure" : "") + (model.FreeAssessment ? "\nFree Assessment" : "") + (model.ScheduleTour ? "\nSchedule Tour" : "") + (model.ContactMe ? "\nContact Me" : "");
                if (services.Length > 0) message += "Services: " + services + "\r\n\r\n";
                
                if (model.AdditionalRequests.Length > 0) message += "Additional Requests: " + model.AdditionalRequests + "\r\n";

                EmailGateway email = new EmailGateway();
                if (email.SendContactUsMessage(model.Email,"MESSAGE FROM ARGIX LOGISTICS WEBSITE",message))
                    return RedirectToAction("Thank_You","Home");
                else
                    ModelState.AddModelError("","Failed to send contact information; please try again.");
            }
            return View(model);
        }

        public ActionResult About() { return View(); }
        public ActionResult Air_Freight() { return View(); }
        public ActionResult Brands_Served() { return View(); }
        public ActionResult Bypass() { return View(); }
        public ActionResult ChangePassword() { return View(); }
        public ActionResult Consolidation() { return View(); }
        public ActionResult Contact() { return View(); }
        public ActionResult Customs_Brokerage() { return View(); }
        public ActionResult DCBypass() { return View(); }
        public ActionResult Difference() { return View(); }
        public ActionResult Distribution() { return View(); }
        public ActionResult Domestic_Deconsolidation() { return View(); }
        public ActionResult Drayage() { return View(); }
        public ActionResult FulfillmentServices() { return View(); }
        public ActionResult Industries_Served() { return View(); }
        public ActionResult International() { return View(); }
        public ActionResult LTDDelivery() { return View(); }
        public ActionResult NationwideB2BDelivery() { return View(); }
        public ActionResult NationwideB2CDelivery() { return View(); }
        public ActionResult Network() { return View(); }
        public ActionResult Ocean_Freight() { return View(); }
        public ActionResult Privacy() { return View(); }
        public ActionResult RegionalDelivery() { return View(); }
        public ActionResult SortingServices() { return View(); }
        public ActionResult Supply_Chain() { return View(); }
        public ActionResult Technology_Difference() { return View(); }
        public ActionResult Testimonials() { return View(); }
        public ActionResult Thank_You() { return View(); }
        public ActionResult Transportation() { return View(); }
        public ActionResult Warehousing() { return View(); }

    }
}
