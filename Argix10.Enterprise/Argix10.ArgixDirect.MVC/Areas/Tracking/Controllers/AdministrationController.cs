using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argix.Areas.Tracking.Controllers {
    //
    [Authorize(Roles = "Administrators")]
    public class AdministrationController:Controller {
        //
        public ActionResult Administration() { return View(); }
        public ActionResult ChangePassword() { return View(); }
        public ActionResult ManageGuests() { return View(); }
        public ActionResult ManageUsers() { return View(); }
        public ActionResult ManageMembership() { return View(); }
    }
}
