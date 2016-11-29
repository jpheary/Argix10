<%@ WebService Language="C#" Class="Carton" %>

using System;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Data;
using Argix.Enterprise;

/// <summary>
/// SoapCredential inherits from SoapHeader and defines what to expect in Soap Header for credentials.
/// </summary>
public class SoapCredential:SoapHeader {
    //Members
    public string UserName = "";
    public string Password = "";
}

/// <summary>
/// Web-service for tracking single carton.
/// Notes: 
/// Tasks:
/// 1. Validate Carton number - there should be only one - check min and max before executing.
/// 2. Convert TrackingDataset into CartonWSDetail.
/// 3. Return error message in the ErrorMessage field on CartonWSDetail.
/// </summary>
[WebService(Namespace = "https://www.argixdirect.com/Tracking/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
public class Carton :System.Web.Services.WebService {
    //Members
    public SoapCredential Credentials;
    
    private const string MAX_CHAR_LIMIT_EXCEEDED = "Length of the carton number exceeded the maximum allowed.";
    private const string MIN_CHAR_LIMIT_EXCEEDED = "Length of the carton number is less than the minimum required.";
    private const string MSG_PROFILE_NOT_FOUND = "Your registration information not found. Please contact technical support.";
    private const string TRACKING_CARTONS_NOT_FOUND = "No carton information found. Make sure your tracking number is correct.";
    private const string TRACKINGWS_USER_NOT_AUTH = "Your account has not been given access to use webservice.";
    private const string TRACKINGWS_UNKNOWN_USER = "You will need to pass valid credentials using SoapCredential class to access this web-service.";
    
    //Interface
    [WebMethod]
    [SoapHeader("Credentials")]
    public CartonWSDetail TrackCarton(string cartonNumber) {
        //Return track details for a single carton
        TrackingItem item = new TrackingItem();
        int cartonLenMin = 6, cartonLenMax = 25;
        try {
            //Validate if carton number satisfies rules
            string val = WebConfigurationManager.AppSettings["maxCartonLen"];
            if (val != null && val.Length > 0) cartonLenMax = Convert.ToInt32(val);
            val = WebConfigurationManager.AppSettings["minCartonLen"];
            if (val != null && val.Length > 0) cartonLenMin = Convert.ToInt32(val);
            item.ItemNumber = cartonNumber.Trim();
            item.ItemNumber = item.ItemNumber.Replace("'","''");
            item.ItemNumber = item.ItemNumber.Replace("[","[[]");
            item.ItemNumber = item.ItemNumber.Replace("%","[%]");
            item.ItemNumber = item.ItemNumber.Replace("_","[_]");
            if (item.ItemNumber.Length < cartonLenMin) item.ErrorMessage = MIN_CHAR_LIMIT_EXCEEDED;
            if (item.ItemNumber.Length > cartonLenMax) item.ErrorMessage = MAX_CHAR_LIMIT_EXCEEDED;

            //First verify if user has passed valid credentials
            bool verified = this.Credentials != null ? Membership.ValidateUser(Credentials.UserName,Credentials.Password) : false;
            if(verified) {
                //Verify the username has the web service access
                if(!isAdmin(Credentials.UserName) && !isWebSvc(Credentials.UserName)) item.ErrorMessage = TRACKINGWS_USER_NOT_AUTH;

                //Now retrieve user profile properties
                ProfileCommon profile = new ProfileCommon().GetProfile(Credentials.UserName);
                if(profile != null && (isAdmin(Credentials.UserName) || profile.ClientVendorID != "")) {
                    TrackingItems items = new TrackingGateway().TrackCartons(new string[] { cartonNumber }, TrackingGateway.SEARCHBY_CARTONNUMBER, profile.Type, profile.ClientVendorID);
                    if(items.Count > 0)
                        item = items[0];
                    else
                        item.ErrorMessage = TRACKING_CARTONS_NOT_FOUND;
                }
                else
                    item.ErrorMessage = MSG_PROFILE_NOT_FOUND;
            }
            else
                item.ErrorMessage = TRACKINGWS_UNKNOWN_USER;
        }
        catch(Exception ex) { item.ErrorMessage = ex.Message; }
        return mapTrackingItemtoDataset(item);
    }

    //Tracking: mapTrackingItemtoDataset()
    private CartonWSDetail mapTrackingItemtoDataset(TrackingItem item) {
        //
        CartonWSDetail cartons = new CartonWSDetail();
        CartonWSDetail.CartonWSDetailTableRow carton = cartons.CartonWSDetailTable.NewCartonWSDetailTableRow();
        carton.CartonNumber = item.CartonNumber;
        carton.Client = item.Client;
        carton.StoreNumber = item.StoreNumber;
        carton.StoreName = item.StoreName + ", " + item.StoreAddress1 + item.StoreCity + ", " + item.StoreState + " " + item.StoreZip;
        carton.Vendor = item.Vendor;
        carton.PickupDate = Convert.ToDateTime(item.PickupDate);
        carton.BLNumber = item.BOLNumber;
        carton.TLNumber = item.TLNumber;
        carton.LblSeqNumber = item.LabelNumber;
        carton.PONumber = item.PONumber;
        carton.Weight = item.Weight;
        carton.SortFacilityArrivalDate = Convert.ToDateTime(item.SortFacilityArrivalDate);
        carton.SortFacilityArrivalStatus = item.SortFacilityArrivalStatus;
        carton.SortFacilityLocation = item.SortFacilityLocation;
        carton.ActualDepartureDate = Convert.ToDateTime(item.ActualDepartureDate);
        carton.ActualDepartureStatus = item.ActualDepartureStatus;
        carton.ActualDepartureLocation = item.ActualDepartureLocation;
        carton.ActualArrivalDate = Convert.ToDateTime(item.ActualArrivalDate);
        carton.ActualArrivalStatus = item.ActualArrivalStatus;
        carton.ActualArrivalLocation = item.ActualArrivalLocation;
        carton.ActualStoreDeliveryDate = Convert.ToDateTime(item.ActualStoreDeliveryDate);
        carton.ActualStoreDeliveryStatus = item.ActualStoreDeliveryStatus;
        carton.ActualStoreDeliveryLocation = item.ActualStoreDeliveryLocation;
        //carton.ScheduledDeliveryDate = ;
        carton.PODScanDate = Convert.ToDateTime(item.PODScanDate);
        carton.PODScanStatus = item.PODScanStatus;
        carton.PODScanLocation = item.PODScanLocation;
        carton.ErrorMessage = item.ErrorMessage;

        cartons.CartonWSDetailTable.AddCartonWSDetailTableRow(carton);
        cartons.AcceptChanges();
        return cartons;
    }
    private bool isAdmin(string userName) {
        bool result = false;
        string[] userRoles = Roles.GetRolesForUser(userName);
        for (int i = 0;i < userRoles.Length;i++) {
            if (userRoles[i].ToLower() == MembershipServices.ADMINROLE) {
                result = true;
                break;
            }
        }
        return result;
    }
    private bool isWebSvc(string userName) {
        bool result = false;
        string[] userRoles = Roles.GetRolesForUser(userName);
        for (int i = 0;i < userRoles.Length;i++) {
            if (userRoles[i].ToLower() == MembershipServices.TRACKINGWSROLE) {
                result = true;
                break;
            }
        }
        return result;
    }
}