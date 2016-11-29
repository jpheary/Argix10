using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Argix.Areas.Mobile.Models;

namespace Argix.Areas.Mobile.Controllers {
    //
    public class TrackingController : Controller {
        //Members

        //Interface
        public TrackingController() { }

        public ActionResult Tracking() { return View(); }
        [HttpPost]
        public ActionResult Tracking(TrackingModel model) {
            if (ModelState.IsValid) {
                foreach (Client client in TrackingModel.Clients) {
                    if (client.ClientID == model.ClientID) model.ClientName = client.CompanyName;
                }
                return RedirectToAction("TrackingStores","Tracking",model);
            }
            return View(model);
        }
        public ActionResult TrackingStores(TrackingModel model) {
            TrackingDataSet tls = new EnterpriseGateway().TrackCartonsForStoreSummary(model.ClientID,model.Store,model.From,model.To,model.By);
            TrackingStoreSummary summary = new TrackingStoreSummary(model,tls);
            return View(summary); 
        }
        public ActionResult TrackingTLs(string clientID,string clientName, string store,DateTime from,DateTime to,string by,string tl) {
            TrackingDataSet cartons = new EnterpriseGateway().TrackCartonsForStoreDetail(clientID,store,from,to,by,tl);
            TrackingStoreSummary summary = new TrackingStoreSummary();
            summary.ClientID = clientID;
            summary.ClientName = clientName;
            summary.Store = store;
            summary.From = from;
            summary.To = to;
            summary.By = by;
            summary.TLs = new List<TrackingStoreTL>();

            TrackingStoreDetail detail = new TrackingStoreDetail(summary,tl,cartons);
            return View(detail); 
        }
    }
}
