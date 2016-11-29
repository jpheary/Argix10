using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Argix.Models;

namespace Argix.Controllers {
    [HandleError]
    public class AboutUsController:Controller {
        public ActionResult AboutUs() { return View(); }
        public ActionResult Location() { return View(); }
        public ActionResult LocationLargeMap() { return View(); }
    }
}
