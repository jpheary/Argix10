using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Argix.Enterprise;

namespace Argix.Freight {
    //Shipping Interfaces
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface IDispatchService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);      
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);
        
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        DataSet ViewPickupLog(DateTime start,DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewPickupLogTemplates();       
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool AddPickupRequest(PickupRequest request);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool ChangePickupRequest(PickupRequest request);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool VerifyPickupRequest(int requestID,string shipperNumber,string shipper,string shipperAddress,string shipperPhone,int windowOpen,int windowClose,string driverName,DateTime actual,string orderType,DateTime lastUpdated,string userID);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool AssignDriverToPickupRequest(int requestID,string driverName,DateTime lastUpdated,string userID);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool ArrivePickupRequest(int requestID,string driverName,DateTime actual,string orderType,DateTime lastUpdated,string userID);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool CancelPickupRequest(int requestID,DateTime cancelled,string cancelledUserID);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        DataSet ViewClientInboundSchedule(DateTime start,DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewClientInboundScheduleTemplates();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool AddClientInboundFreight(ClientInboundFreight freight);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool ChangeClientInboundFreight(ClientInboundFreight freight);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool CancelClientInboundFreight(int id,DateTime cancelled,string cancelledUserID);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        DataSet ViewInboundSchedule(DateTime start,DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewInboundScheduleTemplates();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool AddScheduledInboundFreight(InboundFreight freight);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool ChangeScheduledInboundFreight(InboundFreight freight);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool CancelScheduledInboundFreight(int id,DateTime cancelled,string cancelledUserID);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        DataSet ViewOutboundSchedule(DateTime start,DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewOutboundScheduleTemplates();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action="http://Argix.Freight.DispatchFault")]
        bool AddScheduledOutboundFreight(OutboundFreight freight);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool ChangeScheduledOutboundFreight(OutboundFreight freight);       
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool CancelScheduledOutboundFreight(int id,DateTime cancelled,string cancelledUserID);
        
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewTrailerLog(DateTime start,DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewTrailerLogYardCheck();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewTrailerLogArchive();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet SearchTrailerLog(string trailerNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool AddTrailerEntry(TrailerEntry entry);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool ChangeTrailerEntry(TrailerEntry entry);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool CancelTrailerEntry(int id,DateTime cancelled,string cancelledUserID);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewLoadTenderLog(DateTime start, DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        int CreateLoadTenderEntry(LoadTenderEntry entry);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        LoadTenderEntry ReadLoadTenderEntry(int id);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool UpdateLoadTenderEntry(LoadTenderEntry entry);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool TenderLoadTenderEntry(int entryID, LoadTender loadTender);
        [OperationContract(Name = "ScheduleBBBTripLoadTenderEntry")]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool ScheduleLoadTenderEntry(int entryID, BBBTrip trip);
        [OperationContract(Name = "SchedulePickupLoadTenderEntry")]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool ScheduleLoadTenderEntry(int entryID, PickupRequest request);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool CancelLoadTenderEntry(int entryID, DateTime cancelled, string cancelledBy);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        LoadTender GetLoadTender(int number);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewBBBSchedule(DateTime start, DateTime end);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        int AddBBBScheduleTrip(BBBTrip trip);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool ChangeBBBScheduleTrip(BBBTrip trip);
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault), Action = "http://Argix.Freight.DispatchFault")]
        bool CancelBBBScheduleTrip(int id, DateTime cancelled, string cancelledUserID);

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet GetAppointmentTypes();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet GetFreghtDesginationTypes();

        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        DataSet ViewBlog();
        [OperationContract]
        [FaultContractAttribute(typeof(DispatchFault),Action = "http://Argix.Freight.DispatchFault")]
        bool AddBlogEntry(BlogEntry entry);


        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetClients();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action="http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetAgents();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetTerminals();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetCarriers();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetDrivers();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault),Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetLocations();
        [OperationContract]
        [FaultContractAttribute(typeof(EnterpriseFault), Action = "http://Argix.Enterprise.EnterpriseFault")]
        DataSet GetVendors();
    }

    [DataContract]
    public class PickupRequest {
        //Members
        private int mRequestID=0;
        private DateTime mCreated = DateTime.MinValue, mScheduleDate = DateTime.MinValue;
        private string mCreateUserID="", mCallerName="";
        private string mClientNumber="", mClient="", mShipperNumber="", mShipper="", mShipperAddress="", mShipperPhone="";
        private int mWindowOpen=900,mWindowClose=1700;
        private string mTerminalNumber = "",mTerminal = "",mDriverName = "";
        private DateTime mActualPickup;
        private string mOrderType = "R";
        private int mAmount=0;
        private string mAmountType="Cartons";
        private string mFreightType="Tsort";
        private int mWeight = 0;
        private string mComments = "";
        private bool mIsTemplate = false;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID="";

        //Interface
        public PickupRequest() : this(null) { }
        public PickupRequest(DispatchDataset.PickupLogTableRow request) {
            //Constructor
            try {
                if(request != null) {
                    this.mRequestID = request.RequestID;
                    if (!request.IsCreatedNull()) this.mCreated = request.Created;
                    if (!request.IsCreateUserIDNull()) this.mCreateUserID = request.CreateUserID;
                    if (!request.IsScheduleDateNull()) this.mScheduleDate = request.ScheduleDate;
                    if (!request.IsCallerNameNull()) this.mCallerName = request.CallerName;
                    if (!request.IsClientNumberNull()) this.mClientNumber = request.ClientNumber;
                    if (!request.IsClientNull()) this.mClient = request.Client;
                    if (!request.IsShipperNumberNull()) this.mShipperNumber = request.ShipperNumber;
                    if (!request.IsShipperNull()) this.mShipper = request.Shipper;
                    if (!request.IsShipperAddressNull()) this.mShipperAddress = request.ShipperAddress;
                    if (!request.IsShipperPhoneNull()) this.mShipperPhone = request.ShipperPhone;
                    if (!request.IsWindowOpenNull()) this.mWindowOpen = request.WindowOpen;
                    if (!request.IsWindowCloseNull()) this.mWindowClose = request.WindowClose;
                    if (!request.IsAmountNull()) this.mAmount = request.Amount;
                    if (!request.IsAmountTypeNull()) this.mAmountType = request.AmountType;
                    if (!request.IsWeightNull()) this.mWeight = request.Weight;
                    if (!request.IsFreightTypeNull()) this.mFreightType = request.FreightType;
                    if (!request.IsCommentsNull()) this.mComments = request.Comments;
                    if (!request.IsTerminalNumberNull()) this.mTerminalNumber = request.TerminalNumber;
                    if (!request.IsTerminalNull()) this.mTerminal = request.Terminal;
                    if (!request.IsDriverNameNull()) this.mDriverName = request.DriverName;
                    if (!request.IsActualPickupNull()) this.mActualPickup = request.ActualPickup;
                    if (!request.IsOrderTypeNull()) this.mOrderType = request.OrderType;
                    if (!request.IsIsTemplateNull()) this.mIsTemplate = request.IsTemplate;
                    if (!request.IsCancelledUserIDNull()) this.mCancelledUserID = request.CancelledUserID;
                    if (!request.IsCancelledNull()) this.mCancelled = request.Cancelled;
                    if (!request.IsLastUpdatedNull()) this.mLastUpdated = request.LastUpdated;
                    if (!request.IsUserIDNull()) this.mUserID = request.UserID;
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int RequestID { get { return this.mRequestID; } set { this.mRequestID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public string CallerName { get { return this.mCallerName; } set { this.mCallerName = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string Client { get { return this.mClient; } set { this.mClient = value; } }
        [DataMember]
        public string ShipperNumber { get { return this.mShipperNumber; } set { this.mShipperNumber = value; } }
        [DataMember]
        public string Shipper { get { return this.mShipper; } set { this.mShipper = value; } }
        [DataMember]
        public string ShipperAddress { get { return this.mShipperAddress; } set { this.mShipperAddress = value; } }
        [DataMember]
        public string ShipperPhone { get { return this.mShipperPhone; } set { this.mShipperPhone = value; } }
        [DataMember]
        public int WindowOpen { get { return this.mWindowOpen; } set { this.mWindowOpen = value; } }
        [DataMember]
        public int WindowClose { get { return this.mWindowClose; } set { this.mWindowClose = value; } }
        [DataMember]
        public string TerminalNumber { get { return this.mTerminalNumber; } set { this.mTerminalNumber = value; } }
        [DataMember]
        public string Terminal { get { return this.mTerminal; } set { this.mTerminal = value; } }
        [DataMember]
        public string DriverName { get { return this.mDriverName; } set { this.mDriverName = value; } }
        [DataMember]
        public DateTime ActualPickup { get { return this.mActualPickup; } set { this.mActualPickup = value; } }
        [DataMember]
        public string OrderType { get { return this.mOrderType; } set { this.mOrderType = value; } }
        [DataMember]
        public int Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string AmountType { get { return this.mAmountType; } set { this.mAmountType = value; } }
        [DataMember]
        public string FreightType { get { return this.mFreightType; } set { this.mFreightType = value; } }
        [DataMember]
        public int Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public bool IsTemplate { get { return this.mIsTemplate; } set { this.mIsTemplate = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class ClientInboundFreight {
        //Members
        private int mID=0;
        private DateTime mCreated = DateTime.Now,mScheduleDate = DateTime.Now;
        private string mCreateUserID=Environment.UserName;
        private string mVendorName="",mConsigneeName="";
        private string mCarrierName = "",mDriverName = "",mTrailerNumber = "";
        private DateTime mScheduledArrival = DateTime.MinValue,mActualArrival = DateTime.MinValue;
        private int mAmount = 0;
        private string mAmountType="",mFreightType="";
        private bool mIsLiveUnload = false;
        private DateTime mSortDate = DateTime.MinValue;
        private string mTDSNumber = "",mTDSCreateUserID = "";
        private string mComments="";
        private bool mIsTemplate=false;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public ClientInboundFreight() : this(null) { }
        public ClientInboundFreight(DispatchDataset.ClientInboundScheduleTableRow freight) {
            //Constructor
            try {
                if(freight != null) {
                    this.mID = freight.ID;
                    if (!freight.IsCreatedNull()) this.mCreated = freight.Created;
                    if (!freight.IsCreateUserIDNull()) this.mCreateUserID = freight.CreateUserID;
                    if (!freight.IsScheduleDateNull()) this.mScheduleDate = freight.ScheduleDate;
                    if (!freight.IsVendorNameNull()) this.mVendorName = freight.VendorName;
                    if (!freight.IsConsigneeNameNull()) this.mConsigneeName = freight.ConsigneeName;
                    if (!freight.IsCarrierNameNull()) this.mCarrierName = freight.CarrierName;
                    if (!freight.IsDriverNameNull()) this.mDriverName = freight.DriverName;
                    if (!freight.IsTrailerNumberNull()) this.mTrailerNumber = freight.TrailerNumber;
                    if (!freight.IsScheduledArrivalNull()) this.mScheduledArrival = freight.ScheduledArrival;
                    if (!freight.IsActualArrivalNull()) this.mActualArrival = freight.ActualArrival;
                    if (!freight.IsAmountNull()) this.mAmount = freight.Amount;
                    if (!freight.IsAmountTypeNull()) this.mAmountType = freight.AmountType;
                    if (!freight.IsFreightTypeNull()) this.mFreightType = freight.FreightType;
                    if (!freight.IsIsLiveUnloadNull()) this.mIsLiveUnload = freight.IsLiveUnload;
                    if (!freight.IsSortDateNull()) this.mSortDate = freight.SortDate;
                    if (!freight.IsTDSNumberNull()) this.mTDSNumber = freight.TDSNumber;
                    if (!freight.IsTDSCreateUserIDNull()) this.mTDSCreateUserID = freight.TDSCreateUserID;
                    if (!freight.IsCommentsNull()) this.mComments = freight.Comments;
                    if (!freight.IsIsTemplateNull()) this.mIsTemplate = freight.IsTemplate;
                    if (!freight.IsCancelledUserIDNull()) this.mCancelledUserID = freight.CancelledUserID;
                    if (!freight.IsCancelledNull()) this.mCancelled = freight.Cancelled;
                    if (!freight.IsLastUpdatedNull()) this.mLastUpdated = freight.LastUpdated;
                    if (!freight.IsUserIDNull()) this.mUserID = freight.UserID;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public string VendorName { get { return this.mVendorName; } set { this.mVendorName = value; } }
        [DataMember]
        public string ConsigneeName { get { return this.mConsigneeName; } set { this.mConsigneeName = value; } }
        [DataMember]
        public string CarrierName { get { return this.mCarrierName; } set { this.mCarrierName = value; } }
        [DataMember]
        public string DriverName { get { return this.mDriverName; } set { this.mDriverName = value; } }
        [DataMember]
        public string TrailerNumber { get { return this.mTrailerNumber; } set { this.mTrailerNumber = value; } }
        [DataMember]
        public DateTime ScheduledArrival { get { return this.mScheduledArrival; } set { this.mScheduledArrival = value; } }
        [DataMember]
        public DateTime ActualArrival { get { return this.mActualArrival; } set { this.mActualArrival = value; } }
        [DataMember]
        public int Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string AmountType { get { return this.mAmountType; } set { this.mAmountType = value; } }
        [DataMember]
        public string FreightType { get { return this.mFreightType; } set { this.mFreightType = value; } }
        [DataMember]
        public bool IsLiveUnload { get { return this.mIsLiveUnload; } set { this.mIsLiveUnload = value; } }
        [DataMember]
        public DateTime SortDate { get { return this.mSortDate; } set { this.mSortDate = value; } }
        [DataMember]
        public string TDSNumber { get { return this.mTDSNumber; } set { this.mTDSNumber = value; } }
        [DataMember]
        public string TDSCreateUserID { get { return this.mTDSCreateUserID; } set { this.mTDSCreateUserID = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public bool IsTemplate { get { return this.mIsTemplate; } set { this.mIsTemplate = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class InboundFreight {
        //Members
        private int mID=0;
        private DateTime mCreated = DateTime.MinValue,mScheduleDate = DateTime.MinValue;
        private string mCreateUserID = Environment.UserName;
        private string mOrigin = "",mOriginLocation = "",mDestination = "",mDestinationLocation = "";
        private string mCarrierName = "",mDriverName = "",mTrailerNumber = "",mDropEmptyTrailerNumber = "";
        private DateTime mScheduledDeparture = DateTime.Today,mActualDeparture = DateTime.MinValue;
        private DateTime mScheduledArrival=DateTime.Today,mActualArrival=DateTime.MinValue;
        private bool mConfirmed = false;
        private int mAmount = 0;
        private string mAmountType = "", mFreightType="";
        private string mComments = "";
        private bool mIsTemplate = false;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public InboundFreight() : this(null) { }
        public InboundFreight(DispatchDataset.InboundScheduleTableRow freight) {
            //Constructor
            try {
                if(freight != null) {
                    this.mID = freight.ID;
                    if (!freight.IsCreatedNull()) this.mCreated = freight.Created;
                    if (!freight.IsCreateUserIDNull()) this.mCreateUserID = freight.CreateUserID;
                    if (!freight.IsScheduleDateNull()) this.mScheduleDate = freight.ScheduleDate;
                    if (!freight.IsOriginNull()) this.mOrigin = freight.Origin;
                    if (!freight.IsOriginLocationNull()) this.mOriginLocation = freight.OriginLocation;
                    if (!freight.IsDestinationNull()) this.mDestination = freight.Destination;
                    if (!freight.IsDestinationLocationNull()) this.mDestinationLocation = freight.DestinationLocation;
                    if (!freight.IsCarrierNameNull()) this.mCarrierName = freight.CarrierName;
                    if (!freight.IsDriverNameNull()) this.mDriverName = freight.DriverName;
                    if (!freight.IsTrailerNumberNull()) this.mTrailerNumber = freight.TrailerNumber;
                    if (!freight.IsDropEmptyTrailerNumberNull()) this.mDropEmptyTrailerNumber = freight.DropEmptyTrailerNumber;
                    if (!freight.IsScheduledDepartureNull()) this.mScheduledDeparture = freight.ScheduledDeparture;
                    if (!freight.IsActualDepartureNull()) this.mActualDeparture = freight.ActualDeparture;
                    if (!freight.IsScheduledArrivalNull()) this.mScheduledArrival = freight.ScheduledArrival;
                    if (!freight.IsActualArrivalNull()) this.mActualArrival = freight.ActualArrival;
                    if (!freight.IsConfirmedNull()) this.mConfirmed = freight.Confirmed;
                    if (!freight.IsAmountNull()) this.mAmount = freight.Amount;
                    if (!freight.IsAmountTypeNull()) this.mAmountType = freight.AmountType;
                    if (!freight.IsFreightTypeNull()) this.mFreightType = freight.FreightType;
                    if (!freight.IsCommentsNull()) this.mComments = freight.Comments;
                    if (!freight.IsIsTemplateNull()) this.mIsTemplate = freight.IsTemplate;
                    if (!freight.IsCancelledUserIDNull()) this.mCancelledUserID = freight.CancelledUserID;
                    if (!freight.IsCancelledNull()) this.mCancelled = freight.Cancelled;
                    if (!freight.IsLastUpdatedNull()) this.mLastUpdated = freight.LastUpdated;
                    if (!freight.IsUserIDNull()) this.mUserID = freight.UserID;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public string Origin { get { return this.mOrigin; } set { this.mOrigin = value; } }
        [DataMember]
        public string OriginLocation { get { return this.mOriginLocation; } set { this.mOriginLocation = value; } }
        [DataMember]
        public string Destination { get { return this.mDestination; } set { this.mDestination = value; } }
        [DataMember]
        public string DestinationLocation { get { return this.mDestinationLocation; } set { this.mDestinationLocation = value; } }
        [DataMember]
        public string CarrierName { get { return this.mCarrierName; } set { this.mCarrierName = value; } }
        [DataMember]
        public string DriverName { get { return this.mDriverName; } set { this.mDriverName = value; } }
        [DataMember]
        public string TrailerNumber { get { return this.mTrailerNumber; } set { this.mTrailerNumber = value; } }
        [DataMember]
        public string DropEmptyTrailerNumber { get { return this.mDropEmptyTrailerNumber; } set { this.mDropEmptyTrailerNumber = value; } }
        [DataMember]
        public DateTime ScheduledDeparture { get { return this.mScheduledDeparture; } set { this.mScheduledDeparture = value; } }
        [DataMember]
        public DateTime ActualDeparture { get { return this.mActualDeparture; } set { this.mActualDeparture = value; } }
        [DataMember]
        public DateTime ScheduledArrival { get { return this.mScheduledArrival; } set { this.mScheduledArrival = value; } }
        [DataMember]
        public DateTime ActualArrival { get { return this.mActualArrival; } set { this.mActualArrival = value; } }
        [DataMember]
        public bool Confirmed { get { return this.mConfirmed; } set { this.mConfirmed = value; } }
        [DataMember]
        public int Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string AmountType { get { return this.mAmountType; } set { this.mAmountType = value; } }
        [DataMember]
        public string FreightType { get { return this.mFreightType; } set { this.mFreightType = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public bool IsTemplate { get { return this.mIsTemplate; } set { this.mIsTemplate = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class OutboundFreight {
        //Members
        private int mID = 0;
        private DateTime mCreated = DateTime.MinValue,mScheduleDate = DateTime.MinValue;
        private string mCreateUserID = Environment.UserName;
        private string mOrigin = "",mOriginLocation = "",mDestination = "",mDestinationLocation = "";
        private string mCarrierName = "",mDriverName = "",mTrailerNumber = "",mDropEmptyTrailerNumber = "";
        private DateTime mScheduledDeparture = DateTime.Today,mActualDeparture = DateTime.MinValue;
        private DateTime mScheduledArrival = DateTime.Today,mActualArrival = DateTime.MinValue;
        private bool mConfirmed = false;
        private int mAmount = 0;
        private string mAmountType = "",mFreightType = "";
        private string mComments = "";
        private bool mIsTemplate = false;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public OutboundFreight() : this(null) { }
        public OutboundFreight(DispatchDataset.OutboundScheduleTableRow freight) {
            //Constructor
            try {
                if(freight != null) {
                    this.mID = freight.ID;
                    if (!freight.IsCreatedNull()) this.mCreated = freight.Created;
                    if (!freight.IsCreateUserIDNull()) this.mCreateUserID = freight.CreateUserID;
                    if (!freight.IsScheduleDateNull()) this.mScheduleDate = freight.ScheduleDate;
                    if (!freight.IsOriginNull()) this.mOrigin = freight.Origin;
                    if (!freight.IsOriginLocationNull()) this.mOriginLocation = freight.OriginLocation;
                    if (!freight.IsDestinationNull()) this.mDestination = freight.Destination;
                    if (!freight.IsDestinationLocationNull()) this.mDestinationLocation = freight.DestinationLocation;
                    if (!freight.IsCarrierNameNull()) this.mCarrierName = freight.CarrierName;
                    if (!freight.IsDriverNameNull()) this.mDriverName = freight.DriverName;
                    if (!freight.IsTrailerNumberNull()) this.mTrailerNumber = freight.TrailerNumber;
                    if (!freight.IsDropEmptyTrailerNumberNull()) this.mDropEmptyTrailerNumber = freight.DropEmptyTrailerNumber;
                    if (!freight.IsScheduledDepartureNull()) this.mScheduledDeparture = freight.ScheduledDeparture;
                    if (!freight.IsActualDepartureNull()) this.mActualDeparture = freight.ActualDeparture;
                    if (!freight.IsScheduledArrivalNull()) this.mScheduledArrival = freight.ScheduledArrival;
                    if (!freight.IsActualArrivalNull()) this.mActualArrival = freight.ActualArrival;
                    if (!freight.IsConfirmedNull()) this.mConfirmed = freight.Confirmed;
                    if (!freight.IsAmountNull()) this.mAmount = freight.Amount;
                    if (!freight.IsAmountTypeNull()) this.mAmountType = freight.AmountType;
                    if (!freight.IsFreightTypeNull()) this.mFreightType = freight.FreightType;
                    if (!freight.IsCommentsNull()) this.mComments = freight.Comments;
                    if (!freight.IsIsTemplateNull()) this.mIsTemplate = freight.IsTemplate;
                    if (!freight.IsCancelledUserIDNull()) this.mCancelledUserID = freight.CancelledUserID;
                    if (!freight.IsCancelledNull()) this.mCancelled = freight.Cancelled;
                    if (!freight.IsLastUpdatedNull()) this.mLastUpdated = freight.LastUpdated;
                    if (!freight.IsUserIDNull()) this.mUserID = freight.UserID;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public string Origin { get { return this.mOrigin; } set { this.mOrigin = value; } }
        [DataMember]
        public string OriginLocation { get { return this.mOriginLocation; } set { this.mOriginLocation = value; } }
        [DataMember]
        public string Destination { get { return this.mDestination; } set { this.mDestination = value; } }
        [DataMember]
        public string DestinationLocation { get { return this.mDestinationLocation; } set { this.mDestinationLocation = value; } }
        [DataMember]
        public string CarrierName { get { return this.mCarrierName; } set { this.mCarrierName = value; } }
        [DataMember]
        public string DriverName { get { return this.mDriverName; } set { this.mDriverName = value; } }
        [DataMember]
        public string TrailerNumber { get { return this.mTrailerNumber; } set { this.mTrailerNumber = value; } }
        [DataMember]
        public string DropEmptyTrailerNumber { get { return this.mDropEmptyTrailerNumber; } set { this.mDropEmptyTrailerNumber = value; } }
        [DataMember]
        public DateTime ScheduledDeparture { get { return this.mScheduledDeparture; } set { this.mScheduledDeparture = value; } }
        [DataMember]
        public DateTime ActualDeparture { get { return this.mActualDeparture; } set { this.mActualDeparture = value; } }
        [DataMember]
        public DateTime ScheduledArrival { get { return this.mScheduledArrival; } set { this.mScheduledArrival = value; } }
        [DataMember]
        public DateTime ActualArrival { get { return this.mActualArrival; } set { this.mActualArrival = value; } }
        [DataMember]
        public bool Confirmed { get { return this.mConfirmed; } set { this.mConfirmed = value; } }
        [DataMember]
        public int Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string AmountType { get { return this.mAmountType; } set { this.mAmountType = value; } }
        [DataMember]
        public string FreightType { get { return this.mFreightType; } set { this.mFreightType = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public bool IsTemplate { get { return this.mIsTemplate; } set { this.mIsTemplate = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class TrailerEntry {
        //Members
        private int mID = 0;
        private DateTime mCreated = DateTime.Now, mScheduleDate = DateTime.MinValue;
        private string mCreateUserID = "";
        private string mTrailerNumber = "";
        private DateTime mInboundDate = DateTime.MinValue;
        private string mInboundCarrier = "",mInboundSeal = "",mInboundDriverName = "", mTDSNumber = "";
        private string mInitialYardLocation = "";
        private DateTime mOutboundDate = DateTime.MinValue;
        private string mOutboundCarrier = "",mOutboundSeal = "",mOutboundDriverName = "",mBOLNumber = "";
        private string mComments = "";
        private bool mIsTemplate = false;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public TrailerEntry() : this(null) { }
        public TrailerEntry(DispatchDataset.TrailerLogTableRow trailer) {
            //Constructor
            try {
                if (trailer != null) {
                    this.mID = trailer.ID;
                    this.mCreated = trailer.Created;
                    this.mCreateUserID = trailer.CreateUserID;
                    if (!trailer.IsScheduleDateNull()) this.mScheduleDate = trailer.ScheduleDate;
                    if (!trailer.IsTrailerNumberNull()) this.mTrailerNumber = trailer.TrailerNumber;
                    if (!trailer.IsInboundDateNull()) this.mInboundDate = trailer.InboundDate;
                    if (!trailer.IsInboundCarrierNull()) this.mInboundCarrier = trailer.InboundCarrier;
                    if (!trailer.IsInboundSealNull()) this.mInboundSeal = trailer.InboundSeal;
                    if (!trailer.IsInboundDriverNameNull()) this.mInboundDriverName = trailer.InboundDriverName;
                    if (!trailer.IsTDSNumberNull()) this.mTDSNumber = trailer.TDSNumber;
                    if (!trailer.IsInitialYardLocationNull()) this.mInitialYardLocation = trailer.InitialYardLocation;
                    if (!trailer.IsOutboundDateNull()) this.mOutboundDate = trailer.OutboundDate;
                    if (!trailer.IsOutboundCarrierNull()) this.mOutboundCarrier = trailer.OutboundCarrier;
                    if (!trailer.IsOutboundSealNull()) this.mOutboundSeal = trailer.OutboundSeal;
                    if (!trailer.IsOutboundDriverNameNull()) this.mOutboundDriverName = trailer.OutboundDriverName;
                    if (!trailer.IsBOLNumberNull()) this.mBOLNumber = trailer.BOLNumber;
                    if (!trailer.IsCommentsNull()) this.mComments = trailer.Comments;
                    if (!trailer.IsIsTemplateNull()) this.mIsTemplate = trailer.IsTemplate;
                    if (!trailer.IsCancelledUserIDNull()) this.mCancelledUserID = trailer.CancelledUserID;
                    if (!trailer.IsCancelledNull()) this.mCancelled = trailer.Cancelled;
                    if (!trailer.IsLastUpdatedNull()) this.mLastUpdated = trailer.LastUpdated;
                    if (!trailer.IsUserIDNull()) this.mUserID = trailer.UserID;
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public string TrailerNumber { get { return this.mTrailerNumber; } set { this.mTrailerNumber = value; } }
        [DataMember]
        public DateTime InboundDate { get { return this.mInboundDate; } set { this.mInboundDate = value; } }
        [DataMember]
        public string InboundCarrier { get { return this.mInboundCarrier; } set { this.mInboundCarrier = value; } }
        [DataMember]
        public string InboundSeal { get { return this.mInboundSeal; } set { this.mInboundSeal = value; } }
        [DataMember]
        public string InboundDriverName { get { return this.mInboundDriverName; } set { this.mInboundDriverName = value; } }
        [DataMember]
        public string TDSNumber { get { return this.mTDSNumber; } set { this.mTDSNumber = value; } }
        [DataMember]
        public string InitialYardLocation { get { return this.mInitialYardLocation; } set { this.mInitialYardLocation = value; } }
        [DataMember]
        public DateTime OutboundDate { get { return this.mOutboundDate; } set { this.mOutboundDate = value; } }
        [DataMember]
        public string OutboundCarrier { get { return this.mOutboundCarrier; } set { this.mOutboundCarrier = value; } }
        [DataMember]
        public string OutboundSeal { get { return this.mOutboundSeal; } set { this.mOutboundSeal = value; } }
        [DataMember]
        public string OutboundDriverName { get { return this.mOutboundDriverName; } set { this.mOutboundDriverName = value; } }
        [DataMember]
        public string BOLNumber { get { return this.mBOLNumber; } set { this.mBOLNumber = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public bool IsTemplate { get { return this.mIsTemplate; } set { this.mIsTemplate = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class LoadTenderEntry {
        //Members
        private int mID = 0;
        private DateTime mCreated = DateTime.Now, mScheduleDate = DateTime.MinValue;
        private string mCreateUserID = "";
        private string mClientNumber = "", mClient = "";
        private string mVendorNumber = "", mVendorName = "";
        private string mVendorAddressLine1 = "", mVendorAddressLine2 = "", mVendorCity = "", mVendorState = "", mVendorZip = "", mVendorZip4 = "";
        private string mContactName = "", mContactPhone = "", mContactEmail = "";
        private int mWindowOpen = 900, mWindowClose = 1700;
        private int mAmount = 0;
        private string mAmountType = "Cartons";
        private int mWeight = 0;
        private bool mIsFullTrailer = false;
        private string mDescription = "", mComments = "";
        private int mLoadTenderNumber = 0, mPickupNumber=0;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public LoadTenderEntry() { }
        #region Accessors\Modifiers: [Members...]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string Client { get { return this.mClient; } set { this.mClient = value; } }
        [DataMember]
        public string VendorNumber { get { return this.mVendorNumber; } set { this.mVendorNumber = value; } }
        [DataMember]
        public string VendorName { get { return this.mVendorName; } set { this.mVendorName = value; } }
        [DataMember]
        public string VendorAddressLine1 { get { return this.mVendorAddressLine1; } set { this.mVendorAddressLine1 = value; } }
        [DataMember]
        public string VendorAddressLine2 { get { return this.mVendorAddressLine2; } set { this.mVendorAddressLine2 = value; } }
        [DataMember]
        public string VendorCity { get { return this.mVendorCity; } set { this.mVendorCity = value; } }
        [DataMember]
        public string VendorState { get { return this.mVendorState; } set { this.mVendorState = value; } }
        [DataMember]
        public string VendorZip { get { return this.mVendorZip; } set { this.mVendorZip = value; } }
        [DataMember]
        public string VendorZip4 { get { return this.mVendorZip4; } set { this.mVendorZip4 = value; } }
        [DataMember]
        public string ContactName { get { return this.mContactName; } set { this.mContactName = value; } }
        [DataMember]
        public string ContactPhone { get { return this.mContactPhone; } set { this.mContactPhone = value; } }
        [DataMember]
        public string ContactEmail { get { return this.mContactEmail; } set { this.mContactEmail = value; } }
        [DataMember]
        public int WindowOpen { get { return this.mWindowOpen; } set { this.mWindowOpen = value; } }
        [DataMember]
        public int WindowClose { get { return this.mWindowClose; } set { this.mWindowClose = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public int Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string AmountType { get { return this.mAmountType; } set { this.mAmountType = value; } }
        [DataMember]
        public int Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public bool IsFullTrailer { get { return this.mIsFullTrailer; } set { this.mIsFullTrailer = value; } }
        [DataMember]
        public int LoadTenderNumber { get { return this.mLoadTenderNumber; } set { this.mLoadTenderNumber = value; } }
        [DataMember]
        public int PickupNumber { get { return this.mPickupNumber; } set { this.mPickupNumber = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class BBBTrip {
        //Members
        private int mID = 0;
        private DateTime mCreated = DateTime.MinValue, mScheduleDate = DateTime.MinValue;
        private string mCreateUserID = Environment.UserName;
        private long mOriginLocationID = 0, mDestinationLocationID=0;
        private string mOrigin = "", mOriginLocation = "", mDestination = "", mDestinationLocation = "";
        private string mCarrierName = "", mDriverName = "", mTrailerNumber = "", mDropEmptyTrailerNumber = "";
        private bool mIsLiveUnload = false;
        private DateTime mScheduledDeparture = DateTime.Today, mActualDeparture = DateTime.MinValue;
        private DateTime mScheduledArrival = DateTime.Today, mActualArrival = DateTime.MinValue;
        private bool mConfirmed = false;
        private int mAmount = 0;
        private string mAmountType = "", mFreightType = "";
        private string mTDSNumber = "", mComments = "";
        private bool mIsTemplate = false;
        private string mCancelledUserID = "";
        private DateTime mCancelled;
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public BBBTrip() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreateUserID { get { return this.mCreateUserID; } set { this.mCreateUserID = value; } }
        [DataMember]
        public DateTime ScheduleDate { get { return this.mScheduleDate; } set { this.mScheduleDate = value; } }
        [DataMember]
        public long OriginLocationID { get { return this.mOriginLocationID; } set { this.mOriginLocationID = value; } }
        [DataMember]
        public string Origin { get { return this.mOrigin; } set { this.mOrigin = value; } }
        [DataMember]
        public string OriginLocation { get { return this.mOriginLocation; } set { this.mOriginLocation = value; } }
        [DataMember]
        public long DestinationLocationID { get { return this.mDestinationLocationID; } set { this.mDestinationLocationID = value; } }
        [DataMember]
        public string Destination { get { return this.mDestination; } set { this.mDestination = value; } }
        [DataMember]
        public string DestinationLocation { get { return this.mDestinationLocation; } set { this.mDestinationLocation = value; } }
        [DataMember]
        public string CarrierName { get { return this.mCarrierName; } set { this.mCarrierName = value; } }
        [DataMember]
        public string DriverName { get { return this.mDriverName; } set { this.mDriverName = value; } }
        [DataMember]
        public bool Confirmed { get { return this.mConfirmed; } set { this.mConfirmed = value; } }
        [DataMember]
        public string TrailerNumber { get { return this.mTrailerNumber; } set { this.mTrailerNumber = value; } }
        [DataMember]
        public string DropEmptyTrailerNumber { get { return this.mDropEmptyTrailerNumber; } set { this.mDropEmptyTrailerNumber = value; } }
        [DataMember]
        public bool IsLiveUnload { get { return this.mIsLiveUnload; } set { this.mIsLiveUnload = value; } }
        [DataMember]
        public DateTime ScheduledDeparture { get { return this.mScheduledDeparture; } set { this.mScheduledDeparture = value; } }
        [DataMember]
        public DateTime ActualDeparture { get { return this.mActualDeparture; } set { this.mActualDeparture = value; } }
        [DataMember]
        public DateTime ScheduledArrival { get { return this.mScheduledArrival; } set { this.mScheduledArrival = value; } }
        [DataMember]
        public DateTime ActualArrival { get { return this.mActualArrival; } set { this.mActualArrival = value; } }
        [DataMember]
        public int Amount { get { return this.mAmount; } set { this.mAmount = value; } }
        [DataMember]
        public string AmountType { get { return this.mAmountType; } set { this.mAmountType = value; } }
        [DataMember]
        public string FreightType { get { return this.mFreightType; } set { this.mFreightType = value; } }
        [DataMember]
        public string TDSNumber { get { return this.mTDSNumber; } set { this.mTDSNumber = value; } }
        [DataMember]
        public string CancelledUserID { get { return this.mCancelledUserID; } set { this.mCancelledUserID = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public bool IsTemplate { get { return this.mIsTemplate; } set { this.mIsTemplate = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class BlogEntry {
        //Members
        private DateTime mDate = DateTime.MinValue;
        private string mComment = "";
        private string mUserID = "";

        //Interface
        public BlogEntry() : this(null) { }
        public BlogEntry(DispatchDataset.BlogTableRow entry) {
            //Constructor
            try {
                if (entry != null) {
                    if (!entry.IsDateNull()) this.mDate = entry.Date;
                    if (!entry.IsCommentNull()) this.mComment = entry.Comment;
                    if (!entry.IsUserIDNull()) this.mUserID = entry.UserID;
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public DateTime Date { get { return this.mDate; } set { this.mDate = value; } }
        [DataMember]
        public string Comment { get { return this.mComment; } set { this.mComment = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class DispatchFault {
        private string mMessage="";
        public DispatchFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}