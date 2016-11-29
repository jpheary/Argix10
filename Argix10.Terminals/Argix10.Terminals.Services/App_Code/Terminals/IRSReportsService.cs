using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Interface
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IRSReportsService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool LoadPickups(DateTime pickupDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet ReadPickups(DateTime pickupDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool AddPickup(Pickup pickup);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        void UpdatePickups(Pickups pickups);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool UpdatePickup(Pickup pickup);

        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool LoadScanAudits(DateTime routeDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet ReadScanAudits(DateTime routeDate,string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        void UpdateScanAudits(ScanAudits audits);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        bool UpdateScanAudit(ScanAudit audit);

        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetCommodityClasses();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetCustomers();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetDepots();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetDrivers(string routeClass);
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetOnTimeIssues();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetOrderTypes();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        DataSet GetUpdateUsers(string routeClass);
    }

    [CollectionDataContract]
    public class Pickups:BindingList<Pickup> { public Pickups() { } }

    [DataContract]
    public class Pickup {
        //Members
        private int mRecordID=0;
        private DateTime mRouteDate;
        private string mDriver="", mRouteName="";
        private short mReturnTime = 0;
        private string mCustomerID = "",mCustomerName = "",mCustomerType = "",mCustomerAddress = "",mCustomerCity = "",mCustomerState = "",mCustomerZip = "";
        private string mOrderID="";
        private float mPlannedOrderSize = 0,mPlannedOrderWeight = 0,mPlannedOrderCube = 0;
        private float mActualOrderSize=0, mActualOrderWeight=0;
        private string mUnscheduledPickup="", mComments="";
        private string mOrderType = "R",mPlannedCommodity = "",mActualCommodity = "CARTONS";
        private string mDepot="";

        public Pickup() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int RecordID { get { return this.mRecordID; } set { this.mRecordID = value; } }
        [DataMember]
        public DateTime RouteDate { get { return this.mRouteDate; } set { this.mRouteDate = value; } }
        [DataMember]
        public string Driver { get { return this.mDriver; } set { this.mDriver = value; } }
        [DataMember]
        public string RouteName { get { return this.mRouteName; } set { this.mRouteName = value; } }
        [DataMember]
        public short ReturnTime { get { return this.mReturnTime; } set { this.mReturnTime = value; } }
        [DataMember]
        public string CustomerID { get { return this.mCustomerID; } set { this.mCustomerID = value; } }
        [DataMember]
        public string CustomerName { get { return this.mCustomerName; } set { this.mCustomerName = value; } }
        [DataMember]
        public string CustomerType { get { return this.mCustomerType; } set { this.mCustomerType = value; } }
        [DataMember]
        public string CustomerAddress { get { return this.mCustomerAddress; } set { this.mCustomerAddress = value; } }
        [DataMember]
        public string CustomerCity { get { return this.mCustomerCity; } set { this.mCustomerCity = value; } }
        [DataMember]
        public string CustomerState { get { return this.mCustomerState; } set { this.mCustomerState = value; } }
        [DataMember]
        public string CustomerZip { get { return this.mCustomerZip; } set { this.mCustomerZip = value; } }
        [DataMember]
        public string OrderID { get { return this.mOrderID; } set { this.mOrderID = value; } }
        [DataMember]
        public float PlannedOrderSize { get { return this.mPlannedOrderSize; } set { this.mPlannedOrderSize = value; } }
        [DataMember]
        public float PlannedOrderWeight { get { return this.mPlannedOrderWeight; } set { this.mPlannedOrderWeight = value; } }
        [DataMember]
        public float PlannedOrderCube { get { return this.mPlannedOrderCube; } set { this.mPlannedOrderCube = value; } }
        [DataMember]
        public float ActualOrderSize { get { return this.mActualOrderSize; } set { this.mActualOrderSize = value; } }
        [DataMember]
        public float ActualOrderWeight { get { return this.mActualOrderWeight; } set { this.mActualOrderWeight = value; } }
        [DataMember]
        public string UnscheduledPickup { get { return this.mUnscheduledPickup; } set { this.mUnscheduledPickup = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public string OrderType { get { return this.mOrderType; } set { this.mOrderType = value; } }
        [DataMember]
        public string PlannedCommodity { get { return this.mPlannedCommodity; } set { this.mPlannedCommodity = value; } }
        [DataMember]
        public string ActualCommodity { get { return this.mActualCommodity; } set { this.mActualCommodity = value; } }
        [DataMember]
        public string Depot { get { return this.mDepot; } set { this.mDepot = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class ScanAudits:BindingList<ScanAudit> { public ScanAudits() { } }

    [DataContract]
    public class ScanAudit {
        //Members
        private int mRecordID=0;
        private DateTime mRouteDate;
        private string mDriver = "",mRouteName = "";
        private int mRouteSequence = 0;
        private string mCustomerAccount = "",mCustomerName = "",mMallBuilding = "";
        private short mOrderOpen=0, mOrderClose=0, mWaitMinimum=0, mPlannedArrival=0, mPlannedDeparture=0;
        private string mArrive="", mBell="", mDeliveryStart="", mDeliveryEnd="", mDeparture="", mTimeEntryBy="";
        private string mOrderID="";
        private float mPieces=0;
        private string mCommodityClass = "",mCommodityDescription = "",mOrderType = "";
        private int mCartonsScanned=0;
        private string mScanUser="", mPayee="";
        private short mTrip=0, mTripStop=0;
        private string mRouteClass="", mRouteSet="";
        private string mOnTimeIssue="", mScanIssue="", mAdditionalComments="";
        private string mEntryBy="";
        private DateTime mUpdated;
        private string mCRGStatus="", mCRGResolution="";

        public ScanAudit() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int RecordID { get { return this.mRecordID; } set { this.mRecordID = value; } }
        [DataMember]
        public DateTime RouteDate { get { return this.mRouteDate; } set { this.mRouteDate = value; } }
        [DataMember]
        public string Driver { get { return this.mDriver; } set { this.mDriver = value; } }
        [DataMember]
        public string RouteName { get { return this.mRouteName; } set { this.mRouteName = value; } }
        [DataMember]
        public int RouteSequence { get { return this.mRouteSequence; } set { this.mRouteSequence = value; } }
        [DataMember]
        public string CustomerAccount { get { return this.mCustomerAccount; } set { this.mCustomerAccount = value; } }
        [DataMember]
        public string CustomerName { get { return this.mCustomerName; } set { this.mCustomerName = value; } }
        [DataMember]
        public string MallBuilding { get { return this.mMallBuilding; } set { this.mMallBuilding = value; } }
        [DataMember]
        public short OrderOpen { get { return this.mOrderOpen; } set { this.mOrderOpen = value; } }
        [DataMember]
        public short OrderClose { get { return this.mOrderClose; } set { this.mOrderClose = value; } }
        [DataMember]
        public short WaitMinimum { get { return this.mWaitMinimum; } set { this.mWaitMinimum = value; } }
        [DataMember]
        public short PlannedArrival { get { return this.mPlannedArrival; } set { this.mPlannedArrival = value; } }
        [DataMember]
        public short PlannedDeparture { get { return this.mPlannedDeparture; } set { this.mPlannedDeparture = value; } }
        [DataMember]
        public string Arrive { get { return this.mArrive; } set { this.mArrive = value; } }
        [DataMember]
        public string Bell { get { return this.mBell; } set { this.mBell = value; } }
        [DataMember]
        public string DeliveryStart { get { return this.mDeliveryStart; } set { this.mDeliveryStart = value; } }
        [DataMember]
        public string DeliveryEnd { get { return this.mDeliveryEnd; } set { this.mDeliveryEnd = value; } }
        [DataMember]
        public string Departure { get { return this.mDeparture; } set { this.mDeparture = value; } }
        [DataMember]
        public string TimeEntryBy { get { return this.mTimeEntryBy; } set { this.mTimeEntryBy = value; } }
        [DataMember]
        public string OrderID { get { return this.mOrderID; } set { this.mOrderID = value; } }
        [DataMember]
        public float Pieces { get { return this.mPieces; } set { this.mPieces = value; } }
        [DataMember]
        public string CommodityClass { get { return this.mCommodityClass; } set { this.mCommodityClass = value; } }
        [DataMember]
        public string CommodityDescription { get { return this.mCommodityDescription; } set { this.mCommodityDescription = value; } }
        [DataMember]
        public string OrderType { get { return this.mOrderType; } set { this.mOrderType = value; } }
        [DataMember]
        public int CartonsScanned { get { return this.mCartonsScanned; } set { this.mCartonsScanned = value; } }
        [DataMember]
        public string ScanUser { get { return this.mScanUser; } set { this.mScanUser = value; } }
        [DataMember]
        public string Payee { get { return this.mPayee; } set { this.mPayee = value; } }
        [DataMember]
        public short Trip { get { return this.mTrip; } set { this.mTrip = value; } }
        [DataMember]
        public short TripStop { get { return this.mTripStop; } set { this.mTripStop = value; } }
        [DataMember]
        public string RouteClass { get { return this.mRouteClass; } set { this.mRouteClass = value; } }
        [DataMember]
        public string RouteSet { get { return this.mRouteSet; } set { this.mRouteSet = value; } }
        [DataMember]
        public string OnTimeIssue { get { return this.mOnTimeIssue; } set { this.mOnTimeIssue = value; } }
        [DataMember]
        public string ScanIssue { get { return this.mScanIssue; } set { this.mScanIssue = value; } }
        [DataMember]
        public string AdditionalComments { get { return this.mAdditionalComments; } set { this.mAdditionalComments = value; } }
        [DataMember]
        public string EntryBy { get { return this.mEntryBy; } set { this.mEntryBy = value; } }
        [DataMember]
        public DateTime Updated { get { return this.mUpdated; } set { this.mUpdated = value; } }
        [DataMember]
        public string CRGStatus { get { return this.mCRGStatus; } set { this.mCRGStatus = value; } }
        [DataMember]
        public string CRGResolution { get { return this.mCRGResolution; } set { this.mCRGResolution = value; } }
        #endregion
    }
}
