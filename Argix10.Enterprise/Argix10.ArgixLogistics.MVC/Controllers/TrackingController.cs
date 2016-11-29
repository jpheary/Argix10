using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using Argix.Models;
using Argix.Enterprise;

namespace Argix.Controllers {
    //
    public class TrackingController : Controller {
        //Members
        private const string SEARCHBY_CARTON = "Carton Number",SEARCHBY_LABEL = "Argix Label Number",SEARCHBY_PLATE = "Plate Number";
        private const string SEARCHBY_STORE = "Store",SEARCHBY_PRO = "PRO Number",SEARCHBY_PO = "PO Number";

        //Interface
        public TrackingController() { }

        public ActionResult Index() { return View(); }

        public ActionResult Tracking() {
            string actionName = "TrackByItem";
            if (Session["TrackBy"] != null) {
                switch (Session["TrackBy"].ToString()) {
                    case SEARCHBY_CARTON:
                    case SEARCHBY_LABEL:
                    case SEARCHBY_PLATE:
                        actionName = "TrackByItem";
                        break;
                    case SEARCHBY_PO:
                    case SEARCHBY_PRO:
                        actionName = "TrackByContract";
                        break;
                    case SEARCHBY_STORE:
                        actionName = "TrackByStore";
                        break;
                }
            }
            return RedirectToAction(actionName,"Tracking"); 
        }

        public ActionResult TrackByItem() { return View(); }
        [HttpPost]
        public ActionResult TrackByItem(TrackByItemRequest request) {
            //    
            if (ModelState.IsValid) {
                try {
                    //
                    Session["TrackBy"] = request.TrackBy;

                    //Validate
                    string input = encodeInput(request.TrackingNumbers);
                    if (input.Length == 0) { ModelState.AddModelError("","Please enter valid tracking numbers."); return View(request); }
                    string[] numbers = input.Split(Convert.ToChar(13));
                    if (numbers.Length > 10) { ModelState.AddModelError("","Please limit your search to 10 items."); return View(request); }

                    //Get tracking details for all items and retain in Session state
                    string clientNumber = null,vendorNumber = null;
                    ProfileBase profile = System.Web.HttpContext.Current.Profile;
                    if (profile["ClientVendorID"].ToString() != "000" && profile["Type"].ToString().ToLower() == "client")
                        clientNumber = profile["ClientVendorID"].ToString();
                    else if (profile["ClientVendorID"].ToString() != "000" && profile["Type"].ToString().ToLower() == "vendor")
                        vendorNumber = profile["ClientVendorID"].ToString();

                    TrackingItems items = new TrackingItems();
                    switch (request.TrackBy) {
                        case SEARCHBY_LABEL: items = new EnterpriseGateway().TrackItemsByLabelNumber(numbers,clientNumber,vendorNumber); break;
                        case SEARCHBY_PLATE: items = new EnterpriseGateway().TrackItemsByPlateNumber(numbers,clientNumber,vendorNumber); break;
                        default: items = new EnterpriseGateway().TrackItemsByCartonNumber(numbers,clientNumber,vendorNumber); break;
                    }
                    Session["TrackData"] = items;

                    //Redirect to appropriate UI
                    if (items.Count == 0) {
                        ModelState.AddModelError("","Carton(s) not found. Please verify the tracking number(s) and retry."); 
                        return View(request);
                    }
                    //else if (items.Count == 1)
                    //    return RedirectToAction("ItemDetail","Tracking",new { itemNumber = items[0].ItemNumber.Trim() });
                    else if (items.Count > 0)
                        return RedirectToAction("ItemSummary","Tracking");
                    else
                        return View(request);
                }
                catch (Exception ex) { ModelState.AddModelError("",ex); return View(request); }
            }
            else
                return View(request);
        }
        public ActionResult TrackByContract() { return View(); }
        [HttpPost]
        public ActionResult TrackByContract(TrackByItemRequest request) {
            //    
            if (ModelState.IsValid) {
                try {
                    //
                    Session["TrackBy"] = request.TrackBy;

                    //Validate
                    string input = request.TrackingNumbers;
                    if (input.Length == 0) { ModelState.AddModelError("","Please enter a valid tracking number."); return View(request); }

                    //Get tracking details for all items and retain in Session state
                    TrackingItems items = new TrackingItems();
                    switch (request.TrackBy) {
                        case SEARCHBY_PO: items = new EnterpriseGateway().TrackCartonsForPO(request.Client,request.TrackingNumbers); break;
                        case SEARCHBY_PRO: items = new EnterpriseGateway().TrackCartonsForPRO(request.Client,request.TrackingNumbers); break;
                    }
                    Session["TrackData"] = items;

                    //Redirect to appropriate UI
                    if (items.Count == 0) {
                        ModelState.AddModelError("","Carton(s) not found. Please verify the tracking number and retry.");
                        return View(request);
                    }
                    //else if (items.Count == 1)
                    //    return RedirectToAction("ItemDetail","Tracking",new { itemNumber = items[0].ItemNumber.Trim() });
                    else if (items.Count > 0)
                        return RedirectToAction("ItemSummary","Tracking");
                    else
                        return View(request);
                }
                catch (Exception ex) { ModelState.AddModelError("",ex); return View(request); }
            }
            else
                return View(request);
        }
        public ActionResult ItemSummary() {
            //
            TrackingItems items = (TrackingItems)Session["TrackData"];
            ItemSummaryResponse response = new ItemSummaryResponse("Item Summary",items);
            if (items != null && items.Count > 0) {
                if (Session["TrackBy"].ToString() == SEARCHBY_PRO)
                    response.Title = "PRO# " + items[0].ShipmentNumber + "   (" + items.Count.ToString() + " cartons)";
                else if (Session["TrackBy"].ToString() == SEARCHBY_PO)
                    response.Title = "PO# " + items[0].PONumber + "   (" + items.Count.ToString() + " cartons)";
            }
            else
                ModelState.AddModelError("","Could not find summary information. Please return to tracking page and try again.");

            return View(response);
        }
        public ActionResult ItemDetail(string itemNumber) {
            //
            TrackingItems items = (TrackingItems)Session["TrackData"];
            ItemDetailResponse response = new ItemDetailResponse();
            foreach (TrackingItem item in items) {
                if (item.ItemNumber == itemNumber) {
                    response.Store = item.StoreName + ", " + item.StoreAddress1 + ", " + item.StoreAddress2 + ", " + item.StoreCity + ", " + item.StoreState + " " + item.StoreZip;
                    response.Item = item;
                    break;
                }
            }
            return View(response);
        }

        public ActionResult TrackByStore() { return View(); }
        [HttpPost]
        public ActionResult TrackByStore(TrackByStoreRequest request) {
            //    
            if (ModelState.IsValid) {
                Session["TrackBy"] = SEARCHBY_STORE;
                foreach (Client client in TrackByStoreRequest.Clients) {
                    if (client.ClientID == request.ClientID) request.ClientName = client.CompanyName;
                }
                TrackingStoreItems summary = new EnterpriseGateway().TrackCartonsForStoreSummary(request.ClientID,request.StoreNumber,request.FromDate,request.ToDate,null,request.By);
                Session["StoreSummary"] = summary;
                TrackingStoreItems detail = new EnterpriseGateway().TrackCartonsForStoreDetail(request.ClientID,request.StoreNumber,request.FromDate,request.ToDate,null,request.By,null);
                Session["StoreDetail"] = detail;
                return RedirectToAction("StoreSummary","Tracking");
            }
            return View(request);
        }
        public ActionResult StoreSummary() {
            //
            TrackingStoreItems items = (TrackingStoreItems)Session["StoreSummary"];
            string title = "Store#" + items[0].Store;
            StoreItemSummaryResponse summary = new StoreItemSummaryResponse(title,items);
            return View(summary);
        }
        public ActionResult StoreDetail(string tl) {
            //
            TrackingStoreItems items = (TrackingStoreItems)Session["StoreDetail"];
            StoreItemDetailResponse response = new StoreItemDetailResponse();
            TrackingStoreItems _items = new TrackingStoreItems();
            foreach (TrackingStoreItem item in items) {
                if (item.TL == tl) _items.Add(item);
            }
            response.Store = _items[0].Store;
            response.Items = _items;
            return View(response);
        }
        public ActionResult ItemDetailForStore(string itemNumber) {
            //
            string clientNumber = null,vendorNumber = null;
            ProfileBase profile = System.Web.HttpContext.Current.Profile;
            if (profile["ClientVendorID"].ToString() != "000" && profile["Type"].ToString().ToLower() == "client")
                clientNumber = profile["ClientVendorID"].ToString();
            else if (profile["ClientVendorID"].ToString() != "000" && profile["Type"].ToString().ToLower() == "vendor")
                vendorNumber = profile["ClientVendorID"].ToString();
            TrackingItems items = new EnterpriseGateway().TrackItemsByLabelNumber(new string[] { itemNumber },clientNumber,vendorNumber);
            Session["TrackData"] = items;
            return RedirectToAction("ItemDetail",new { itemNumber });
        }

        #region Local Tracking Services: encodeInput(), isNumeric()
        private string encodeInput(string input) {
            //This method makes sure no invalid chars exist in the user input
            string cn = Server.HtmlEncode(input);
            cn = cn.Replace("'","''");
            cn = cn.Replace("[","[[]");
            cn = cn.Replace("%","[%]");
            cn = cn.Replace("_","[_]");
            cn = cn.Replace(",","\r");
            cn = cn.Replace("\r\n","\r");
            cn = cn.Replace("\n","\r");
            return cn.Trim();
        }
        private bool isNumeric(string val) {
            //Determine if the specified value is numeric
            bool valIsNumeric = true;
            char[] chars = val.ToCharArray();
            for (int i = 0;i < chars.Length;i++) {
                if (!char.IsNumber(val,i)) {
                    valIsNumeric = false;
                    break;
                }
            }
            return valIsNumeric;
        }
        #endregion
        
        public ActionResult Reports() { return View(); }
        
        public ActionResult Profile() { return RedirectToAction("MyProfile","Tracking"); }
        public ActionResult MyProfile() {
            //Load the model
            ProfileModel profile = new ProfileModel();

            //  Membership
            MembershipUser member = Membership.GetUser();
            profile.UserName = member.UserName;
            profile.Email = member.Email;

            //  Profile
            ProfileBase profileBase = System.Web.HttpContext.Current.Profile;
            profile.FullName = profileBase["UserFullName"].ToString();
            profile.Company = profileBase["Company"].ToString();
            
            return View(profile);
        }
        [HttpPost]
        public ActionResult MyProfile(ProfileModel profile) {
            //Update member profile
            if (ModelState.IsValid) {
                try {
                    //Update existing user if account is not locked
                    MembershipUser member = Membership.GetUser();
                    if (member.IsLockedOut) {
                        ModelState.AddModelError("",member.UserName + " account must be unlocked before updating."); 
                        return View(profile);
                    }
                    
                    //Membership
                    member.Email = profile.Email;
                    Membership.UpdateUser(member);

                    //Profile
                    ProfileBase profileBase = System.Web.HttpContext.Current.Profile;
                    profileBase["FullName"] = profile.FullName;
                    profileBase["Company"] = profile.Company;
                    profileBase.Save();

                    return View(profile);
                }
                catch(Exception ex) { ModelState.AddModelError("",ex); return View(profile); }
            }
            else
                return View(profile);
        }
    }
}
