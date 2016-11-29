using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Profile;
using System.Web.Security;
using Argix.Enterprise;

namespace Argix.Models {
    //
    public class Client {
        private string mClientID = "",mCompanyName = "";

        public Client(string clientID,string companyName) { this.mClientID = clientID; this.mCompanyName = companyName; }
        public string ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        public string CompanyName { get { return this.mCompanyName; } set { this.mCompanyName = value; } }
    }
    public class TrackByItemType {
        private string mDescription = "";

        public TrackByItemType(string description) { this.mDescription = description; }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
    }
    public class TrackByDateType {
        private string mDescription = "";

        public TrackByDateType(string description) { this.mDescription = description; }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
    }

    public class TrackByItemRequest {
        [Display(Name = "Track By")]
        public string TrackBy { get; set; }

        [Display(Name = "Client")]
        public string Client { get; set; }

        [Display(Name = "Tracking #")]
        [Required(ErrorMessage = " (required)")]
        public string TrackingNumbers { get; set; }

        public static IEnumerable<TrackByItemType> TrackByItemTypes { get { return new List<TrackByItemType> { new TrackByItemType("Carton Number"),new TrackByItemType("Argix Label Number"),new TrackByItemType("Plate Number") }; } }
        public static IEnumerable<TrackByItemType> SearchByItemTypes { get { return new List<TrackByItemType> { new TrackByItemType("PRO Number"),new TrackByItemType("PO Number") }; } }
        public static IEnumerable<Client> Clients {
            get {
                List<Client> clients = new List<Client>();
                TrackingDataSet _clients = new EnterpriseGateway().GetClients();
                for (int i = 0;i < _clients.ClientTable.Rows.Count;i++) { clients.Add(new Client(_clients.ClientTable[i].ClientID,_clients.ClientTable[i].CompanyName.Trim())); }
                return clients;

            }
        }
    }
    public class TrackByStoreRequest {
        [DisplayName("ClientID")]
        public string ClientID { get; set; }

        [DisplayName("ClientName")]
        public string ClientName { get; set; }

        [DisplayName("Store#")]
        public string StoreNumber { get; set; }

        [DisplayName("From")]
        [DataType(DataType.Date)]
        public DateTime FromDate { get; set; }

        [DisplayName("To")]
        [DataType(DataType.Date)]
        public DateTime ToDate { get; set; }

        [DisplayName("By")]
        [DefaultValue("Delivery")]
        public string By { get; set; }

        public static IEnumerable<Client> Clients {
            get {
                List<Client> clients = new List<Client>();
                TrackingDataSet _clients = new EnterpriseGateway().GetClients();
                for (int i = 0;i < _clients.ClientTable.Rows.Count;i++) { clients.Add(new Client(_clients.ClientTable[i].ClientID,_clients.ClientTable[i].CompanyName.Trim())); }
                return clients;

            }
        }
        public static IEnumerable<TrackByDateType> TrackByDateTypes { get { return new List<TrackByDateType> { new TrackByDateType("Delivery"),new TrackByDateType("Pickup") }; } }
    }

    public class ItemSummaryResponse {
        private string mTitle = "";
        private TrackingItems mItems = new TrackingItems();

        public ItemSummaryResponse() { }
        public ItemSummaryResponse(string title,TrackingItems items) { this.mTitle = title; this.mItems = items; }
        public string Title { get { return this.mTitle; } set { this.mTitle = value; } }
        public TrackingItems Items { get { return this.mItems; } set { this.mItems = value; } }
    }
    public class ItemDetailResponse {
        private string mStore = "";
        private TrackingItem mItem = new TrackingItem();

        public ItemDetailResponse() { }
        public ItemDetailResponse(string store, TrackingItem item) { this.mStore = store; this.mItem = item; }
        public string Store { get { return this.mStore; } set { this.mStore = value; } }
        public TrackingItem Item { get { return this.mItem; } set { this.mItem = value; } }
    }

    public class StoreItemSummaryResponse {
        private string mTitle = "";
        private TrackingStoreItems mItems = new TrackingStoreItems();

        public StoreItemSummaryResponse() { }
        public StoreItemSummaryResponse(string title,TrackingStoreItems items) { this.mTitle = title; this.mItems = items; }
        public string Title { get { return this.mTitle; } set { this.mTitle = value; } }
        public TrackingStoreItems Items { get { return this.mItems; } set { this.mItems = value; } }
    }
    public class StoreItemDetailResponse {
        private string mStore = "";
        private TrackingStoreItems mItem = new TrackingStoreItems();

        public StoreItemDetailResponse() { }
        public StoreItemDetailResponse(string store,TrackingStoreItems item) { this.mStore = store; this.mItem = item; }
        public string Store { get { return this.mStore; } set { this.mStore = value; } }
        public TrackingStoreItems Items { get { return this.mItem; } set { this.mItem = value; } }
    }

    public class ProfileModel {
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " (required)")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = " (required)")]
        [Display(Name = "FullName")]
        public string FullName { get; set; }

        [Required(ErrorMessage = " (required)")]
        [Display(Name = "Company")]
        public string Company { get; set; }
    }
}
