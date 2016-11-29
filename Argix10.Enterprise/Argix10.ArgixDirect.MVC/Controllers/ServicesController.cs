using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Argix.Controllers {
    //
    public class ServicesController : Controller {
        //
        public ActionResult Services() { return View(); }
        public ActionResult StoreDelivery() { return View(); }
        public ActionResult ConsumerDirect() { return View(); }
        public ActionResult Consolidation() { return View(); }
        public ActionResult DCBypass() { return View(); }
        public ActionResult FreightPickup() { return View(); }
        public ActionResult ImportDeconsolidation() { return View(); }
        public ActionResult LineHaul() { return View(); }
        public ActionResult Warehousing() { return View(); }
        public ActionResult SmartScan() { return View(); }
    }
}
