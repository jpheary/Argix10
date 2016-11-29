using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Argix.Areas.Tracking.Models;

namespace Argix.Areas.Tracking.Controllers {
    //
    [Authorize(Roles = "Administrators,Members")]
    public class TrackingController: Controller {
        //
        public ActionResult Tracking() { return View(); }
        public ActionResult TrackByCarton() { 
            //Track by carton view; build TrackBy list of items
            IList<SelectListItem> list = new List<SelectListItem>();
            SelectListItem item = new SelectListItem();
            item.Text = "Carton Number";
            item.Value = "CartonNumber";
            item.Selected = true;
            list.Add(item);
            item = new SelectListItem();
            item.Text = "LabelNumber";
            item.Value = "LabelNumber";
            list.Add(item);
            item = new SelectListItem();
            item.Text = "PlateNumber";
            item.Value = "PlateNumber";
            list.Add(item);
            ViewData["TrackBy"] = list;

            return View(); 
        }
        [HttpPost]
        public ActionResult TrackByCarton(TrackByCartonModel model) {
            if(ModelState.IsValid) {
                string searchBy = model.TrackBy;
                string items = model.Items;
            }
            return View(model);
        }
        public ActionResult TrackByContract() { return View(); }
        public ActionResult TrackByStore() { return View(); }
    }
}
