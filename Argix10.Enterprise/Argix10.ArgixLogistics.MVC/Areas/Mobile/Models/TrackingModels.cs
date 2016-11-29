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

namespace Argix.Areas.Mobile.Models {
    //
    public class By {
        private string mDescription = "";
        
        public By(string description) { this.mDescription = description; }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
    }
    public class Client {
        private string mClientID="", mCompanyName="";

        public Client(string clientID,string companyName) { this.mClientID = clientID; this.mCompanyName = companyName; }
        public string ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        public string CompanyName { get { return this.mCompanyName; } set { this.mCompanyName = value; } }
    }

    public class TrackingModel {
        [DisplayName("ClientID")]
        public string ClientID { get; set; }

        [DisplayName("ClientName")]
        public string ClientName { get; set; }

        [DisplayName("Store")]
        public string Store { get; set; }

        [DisplayName("From")]
        [DataType(DataType.Date)]
        public DateTime From { get; set; }

        [DisplayName("To")]
        [DataType(DataType.Date)]
        public DateTime To { get; set; }

        [DisplayName("By")]
        [DefaultValue("Delivery")]
        public string By { get; set; }

        [DisplayName("TL")]
        public string TL { get; set; }

        public static IEnumerable<By> Bys { get { return new List<By> { new By("Delivery"),new By("Pickup") }; } }

        public static IEnumerable<Client> Clients {
            get {
                List<Client> clients = new List<Client>();
                TrackingDataSet _clients = new EnterpriseGateway().GetClients();
                for (int i = 0;i < _clients.ClientTable.Rows.Count;i++) { clients.Add(new Client(_clients.ClientTable[i].ClientID,_clients.ClientTable[i].CompanyName.Trim())); }
                return clients;

            }
        }
    }

    public class TrackingStoreSummary {
        private string mClientID = "", mClientName = "";
        private string mStore = "";
        private DateTime mFrom,mTo;
        private string mBy = "";
        private List<TrackingStoreTL> mTLs=null;

        public TrackingStoreSummary() { }
        public TrackingStoreSummary(TrackingModel model,TrackingDataSet tls) {
            this.mClientID = model.ClientID;
            this.mClientName = model.ClientName;
            this.mStore = model.Store;
            this.mFrom = model.From;
            this.mTo = model.To;
            this.mBy = model.By;
            this.mTLs = new List<TrackingStoreTL>();
            for (int i = 0;i < tls.CartonDetailForStoreTable.Rows.Count;i++) {
                TrackingStoreTL tl = new TrackingStoreTL(tls.CartonDetailForStoreTable[i]);
                this.mTLs.Add(tl);
            }
        }
        public string ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        public string Store { get { return this.mStore; } set { this.mStore = value; } }
        public DateTime From { get { return this.mFrom; } set { this.mFrom = value; } }
        public DateTime To { get { return this.mTo; } set { this.mTo = value; } }
        public string By { get { return this.mBy; } set { this.mBy = value; } }
        public IEnumerable<TrackingStoreTL> TLs { get { return this.mTLs; } set { this.mTLs = (List<TrackingStoreTL>)value; } }
    }
    public class TrackingStoreTL {
        private string mTL="";
        private int mCartons = 0;
        private decimal mWeight = 0;
        private string mCBOL = "";
        private DateTime mOFD=DateTime.MinValue, mPOD=DateTime.MinValue;
        private string mAgentNumber = "",mAgentName = "";

        public TrackingStoreTL(TrackingDataSet.CartonDetailForStoreTableRow tl) {
            this.mTL = tl.TL;
            this.mCartons = tl.CartonCount;
            this.mWeight = tl.Weight;
            this.mCBOL = tl.CBOL;
            if (!tl.IsOFD1Null()) this.mOFD = tl.OFD1;
            if(!tl.IsPodDateNull()) this.mPOD = tl.PodDate;
            this.mAgentNumber = tl.AG;
            this.mAgentName = tl.AgName;
        }
        public string TL { get { return this.mTL; } set { this.mTL = value; } }
        public int CartonCount { get { return this.mCartons; } set { this.mCartons = value; } }
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        public string CBOL { get { return this.mCBOL; } set { this.mCBOL = value; } }
        public DateTime OFD { get { return this.mOFD; } set { this.mOFD = value; } }
        public DateTime POD { get { return this.mPOD; } set { this.mPOD = value; } }
        public string OFDorPODLabel { get { return this.mPOD.CompareTo(DateTime.MinValue) > 0 ? "POD:" : "OFD:"; } }
        public string OFDorPODValue { 
            get {
                if (this.mPOD.CompareTo(DateTime.MinValue) > 0) 
                    return this.mPOD.ToString("MM-dd-yyyy");
                else if (this.mOFD.CompareTo(DateTime.MinValue) > 0) 
                    return this.mOFD.ToString("MM-dd-yyyy");
                else
                    return ""; 
            } 
        }
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
    }

    public class TrackingStoreDetail {
        private string mClientID = "",mClientName = "";
        private string mStore = "";
        private DateTime mFrom,mTo;
        private string mBy = "";
        private string mTL = "";
        private List<TrackingStoreCarton> mCartons = null;

        public TrackingStoreDetail() { }
        public TrackingStoreDetail(TrackingStoreSummary summary,string tl,TrackingDataSet cartons) {
            this.mClientID = summary.ClientID;
            this.mClientName = summary.ClientName;
            this.mStore = summary.Store;
            this.mFrom = summary.From;
            this.mTo = summary.To;
            this.mBy = summary.By;
            this.mTL = tl;
            this.mCartons = new List<TrackingStoreCarton>();
            for (int i = 0;i < cartons.CartonDetailForStoreTable.Rows.Count;i++) {
                TrackingStoreCarton carton = new TrackingStoreCarton(cartons.CartonDetailForStoreTable[i]);
                this.mCartons.Add(carton);
            }
        }
        public string ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        public string Store { get { return this.mStore; } set { this.mStore = value; } }
        public DateTime From { get { return this.mFrom; } set { this.mFrom = value; } }
        public DateTime To { get { return this.mTo; } set { this.mTo = value; } }
        public string By { get { return this.mBy; } set { this.mBy = value; } }
        public string TL { get { return this.mTL; } set { this.mTL = value; } }
        public IEnumerable<TrackingStoreCarton> Cartons { get { return this.mCartons; } set { this.mCartons = (List<TrackingStoreCarton>)value; } }
    }
    public class TrackingStoreCarton {
        private string mCartonNumber = "";
        private decimal mWeight = 0;
        private DateTime mPickupDate;
        private string mShipperName = "";
        private string mCartonStatus="", mScanStatus="";
        private DateTime mPODDate=DateTime.MinValue, mPODTime=DateTime.MinValue;

        public TrackingStoreCarton(TrackingDataSet.CartonDetailForStoreTableRow carton) {
            this.mCartonNumber = carton.CartonNo;
            this.mWeight = carton.Weight;
            if (!carton.IsPudtNull()) this.mPickupDate = carton.Pudt;
            this.mShipperName = carton.ShpName;
            if (!carton.IsCtnStsNull()) this.mCartonStatus = carton.CtnSts;
            if (!carton.IsScnStsNull()) this.mScanStatus = carton.ScnSts;
            if (!carton.IsPodDateNull()) this.mPODDate = carton.PodDate;
            if (!carton.IsPodDateNull()) this.mPODTime = carton.PodTime;
        }
        public string CartonNumber { get { return this.mCartonNumber; } set { this.mCartonNumber = value; } }
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        public DateTime PickupDate { get { return this.mPickupDate; } set { this.mPickupDate = value; } }
        public string ShipperName { get { return this.mShipperName; } set { this.mShipperName = value; } }
        public string CartonStatus { get { return this.mCartonStatus; } set { this.mCartonStatus = value; } }
        public string ScanStatus { get { return this.mScanStatus; } set { this.mScanStatus = value; } }
        public DateTime PODDate { get { return this.mPODDate; } set { this.mPODDate = value; } }
        public DateTime PODTime { get { return this.mPODTime; } set { this.mPODTime = value; } }
        public string POD { get { return this.mPODDate.CompareTo(DateTime.MinValue) > 0 ? this.mPODDate.ToString("MM-dd-yyyy") + " " + this.mPODTime.ToString("HH:mm tt") : ""; } }
    }
}
