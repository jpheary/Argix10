using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argix.Areas.Tracking.Controllers {
    //
    [Authorize(Roles = "Administrators,Members,RSMembers")]
    public class ReportsController : Controller {
        //
        public ActionResult Reports() { return View(); }
    }
}
