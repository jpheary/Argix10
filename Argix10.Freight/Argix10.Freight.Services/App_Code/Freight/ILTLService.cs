using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //Shipping Interfaces
    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface ILTLService {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(TraceMessage m);
    }

    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface ILTLClientService {
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLQuote CreateQuote(LTLQuote entry);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLClient(LTLClient client);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLClient ReadLTLClient(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLClient(LTLClient client);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet GetLTLClientList(int salesRepClientID);

        [OperationContract(Name = "ViewLTLShippersForClient")]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLShippers(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLShipper(LTLShipper shipper);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLShippersList(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLShipper ReadLTLShipper(int shipperID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLShipper(LTLShipper shipper);

        [OperationContract(Name = "ViewLTLConsigneesForClient")]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLConsignees(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLConsignee(LTLConsignee consignee);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLConsigneesList(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLConsignee ReadLTLConsignee(int consigneeID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLConsignee(LTLConsignee consignee);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLShipments(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLShipment(LTLShipment shipment);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLShipment ReadLTLShipment(int shipmentID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLShipment(LTLShipment shipment);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool CancelLTLShipment(int shipmentID);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        ServiceLocation ReadServiceLocation(string zipCode);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        ServiceLocation ReadPickupLocation(string zipCode);
    }

    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface ILTLAdminService {
        //

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLClients();
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLClient GetLTLClient(int clientID);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool ApproveLTLClient(int clientID,string clientNumber,bool approve,string username);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadPalletLabels(int shipmentID);
    }

    [DataContract]
    public class LTLQuote {
        //Members
        private int mID = 0;
        private DateTime mShipDate;
        private int mShipperID = 0,mConsigneeID = 0;
        private string mOriginZip = "",mDestinationZip = "";
        private int mPallet1Weight = 0,mPallet2Weight = 0,mPallet3Weight = 0,mPallet4Weight = 0,mPallet5Weight = 0;
        private string mPallet1Class = "",mPallet2Class = "",mPallet3Class = "",mPallet4Class = "",mPallet5Class = "";
        private decimal mPallet1InsuranceValue = 0,mPallet2InsuranceValue = 0,mPallet3InsuranceValue = 0,mPallet4InsuranceValue = 0,mPallet5InsuranceValue = 0;
        private bool mInsidePickup = false,mLiftGateOrigin = false,mAppointmentOrigin = false;
        private bool mInsideDelivery = false,mLiftGateDestination = false,mAppointmentDestination = false;
        private int mPallets = 0;
        private decimal mWeight = 0;
        private decimal mPalletRate = 0,mFuelSurcharge = 0,mAccessorialCharge = 0,mInsuranceCharge = 0,mTollCharge = 0,mTotalCharge = 0;
        private int mTransitMin = 0,mTransitMax = 0;
        private DateTime mCreated;

        //Interface
        public LTLQuote() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public DateTime ShipDate { get { return this.mShipDate; } set { this.mShipDate = value; } }
        [DataMember]
        public int ShipperID { get { return this.mShipperID; } set { this.mShipperID = value; } }
        [DataMember]
        public int ConsigneeID { get { return this.mConsigneeID; } set { this.mConsigneeID = value; } }
        [DataMember]
        public string OriginZip { get { return this.mOriginZip; } set { this.mOriginZip = value; } }
        [DataMember]
        public string DestinationZip { get { return this.mDestinationZip; } set { this.mDestinationZip = value; } }
        [DataMember]
        public int Pallet1Weight { get { return this.mPallet1Weight; } set { this.mPallet1Weight = value; } }
        [DataMember]
        public string Pallet1Class { get { return this.mPallet1Class; } set { this.mPallet1Class = value; } }
        [DataMember]
        public decimal Pallet1InsuranceValue { get { return this.mPallet1InsuranceValue; } set { this.mPallet1InsuranceValue = value; } }
        [DataMember]
        public int Pallet2Weight { get { return this.mPallet2Weight; } set { this.mPallet2Weight = value; } }
        [DataMember]
        public string Pallet2Class { get { return this.mPallet2Class; } set { this.mPallet2Class = value; } }
        [DataMember]
        public decimal Pallet2InsuranceValue { get { return this.mPallet2InsuranceValue; } set { this.mPallet2InsuranceValue = value; } }
        [DataMember]
        public int Pallet3Weight { get { return this.mPallet3Weight; } set { this.mPallet3Weight = value; } }
        [DataMember]
        public string Pallet3Class { get { return this.mPallet3Class; } set { this.mPallet3Class = value; } }
        [DataMember]
        public decimal Pallet3InsuranceValue { get { return this.mPallet3InsuranceValue; } set { this.mPallet3InsuranceValue = value; } }
        [DataMember]
        public int Pallet4Weight { get { return this.mPallet4Weight; } set { this.mPallet4Weight = value; } }
        [DataMember]
        public string Pallet4Class { get { return this.mPallet4Class; } set { this.mPallet4Class = value; } }
        [DataMember]
        public decimal Pallet4InsuranceValue { get { return this.mPallet4InsuranceValue; } set { this.mPallet4InsuranceValue = value; } }
        [DataMember]
        public int Pallet5Weight { get { return this.mPallet5Weight; } set { this.mPallet5Weight = value; } }
        [DataMember]
        public string Pallet5Class { get { return this.mPallet5Class; } set { this.mPallet5Class = value; } }
        [DataMember]
        public decimal Pallet5InsuranceValue { get { return this.mPallet5InsuranceValue; } set { this.mPallet5InsuranceValue = value; } }
        [DataMember]
        public bool InsidePickup { get { return this.mInsidePickup; } set { this.mInsidePickup = value; } }
        [DataMember]
        public bool LiftGateOrigin { get { return this.mLiftGateOrigin; } set { this.mLiftGateOrigin = value; } }
        [DataMember]
        public bool AppointmentOrigin { get { return this.mAppointmentOrigin; } set { this.mAppointmentOrigin = value; } }
        [DataMember]
        public bool InsideDelivery { get { return this.mInsideDelivery; } set { this.mInsideDelivery = value; } }
        [DataMember]
        public bool LiftGateDestination { get { return this.mLiftGateDestination; } set { this.mLiftGateDestination = value; } }
        [DataMember]
        public bool AppointmentDestination { get { return this.mAppointmentDestination; } set { this.mAppointmentDestination = value; } }
        [DataMember]
        public int Pallets { get { return this.mPallets; } set { this.mPallets = value; } }
        [DataMember]
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public decimal PalletRate { get { return this.mPalletRate; } set { this.mPalletRate = value; } }
        [DataMember]
        public decimal FuelSurcharge { get { return this.mFuelSurcharge; } set { this.mFuelSurcharge = value; } }
        [DataMember]
        public decimal AccessorialCharge { get { return this.mAccessorialCharge; } set { this.mAccessorialCharge = value; } }
        [DataMember]
        public decimal InsuranceCharge { get { return this.mInsuranceCharge; } set { this.mInsuranceCharge = value; } }
        [DataMember]
        public decimal TollCharge { get { return this.mTollCharge; } set { this.mTollCharge = value; } }
        [DataMember]
        public decimal TotalCharge { get { return this.mTotalCharge; } set { this.mTotalCharge = value; } }
        [DataMember]
        public int TransitMin { get { return this.mTransitMin; } set { this.mTransitMin = value; } }
        [DataMember]
        public int TransitMax { get { return this.mTransitMax; } set { this.mTransitMax = value; } }
        #endregion
    }

    [DataContract]
    public class LTLClient {
        //Members
        private int mID = 0;
        private string mNumber = "";
        private string mName = "";
        private string mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip = "",mZip4 = "";
        private string mContactName = "",mContactPhone = "",mContactEmail = "";
        private string mCorporateName = "";
        private string mCorporateAddressLine1 = "",mCorporateAddressLine2 = "",mCorporateCity = "",mCorporateState = "",mCorporateZip = "",mCorporateZip4 = "",mTaxIDNumber = "";
        private string mBillingAddressLine1 = "",mBillingAddressLine2 = "",mBillingCity = "",mBillingState = "",mBillingZip = "",mBillingZip4 = "";
        private bool mApproved = false;
        private DateTime mApprovedDate;
        private string mApprovedUser = "";
        private int mSalesRepClientID = 0;
        private string mStatus = "A";
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public LTLClient()  { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Name { get { return this.mName; } set { this.mName = value; } }
        [DataMember]
        public string AddressLine1 { get { return this.mAddressLine1; } set { this.mAddressLine1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this.mAddressLine2; } set { this.mAddressLine2 = value; } }
        [DataMember]
        public string City { get { return this.mCity; } set { this.mCity = value; } }
        [DataMember]
        public string State { get { return this.mState; } set { this.mState = value; } }
        [DataMember]
        public string Zip { get { return this.mZip; } set { this.mZip = value; } }
        [DataMember]
        public string Zip4 { get { return this.mZip4; } set { this.mZip4 = value; } }
        [DataMember]
        public string ContactName { get { return this.mContactName; } set { this.mContactName = value; } }
        [DataMember]
        public string ContactPhone { get { return this.mContactPhone; } set { this.mContactPhone = value; } }
        [DataMember]
        public string ContactEmail { get { return this.mContactEmail; } set { this.mContactEmail = value; } }
        [DataMember]
        public string CorporateName { get { return this.mCorporateName; } set { this.mCorporateName = value; } }
        [DataMember]
        public string CorporateAddressLine1 { get { return this.mCorporateAddressLine1; } set { this.mCorporateAddressLine1 = value; } }
        [DataMember]
        public string CorporateAddressLine2 { get { return this.mCorporateAddressLine2; } set { this.mCorporateAddressLine2 = value; } }
        [DataMember]
        public string CorporateCity { get { return this.mCorporateCity; } set { this.mCorporateCity = value; } }
        [DataMember]
        public string CorporateState { get { return this.mCorporateState; } set { this.mCorporateState = value; } }
        [DataMember]
        public string CorporateZip { get { return this.mCorporateZip; } set { this.mCorporateZip = value; } }
        [DataMember]
        public string CorporateZip4 { get { return this.mCorporateZip4; } set { this.mCorporateZip4 = value; } }
        [DataMember]
        public string TaxIDNumber { get { return this.mTaxIDNumber; } set { this.mTaxIDNumber = value; } }
        [DataMember]
        public string BillingAddressLine1 { get { return this.mBillingAddressLine1; } set { this.mBillingAddressLine1 = value; } }
        [DataMember]
        public string BillingAddressLine2 { get { return this.mBillingAddressLine2; } set { this.mBillingAddressLine2 = value; } }
        [DataMember]
        public string BillingCity { get { return this.mBillingCity; } set { this.mBillingCity = value; } }
        [DataMember]
        public string BillingState { get { return this.mBillingState; } set { this.mBillingState = value; } }
        [DataMember]
        public string BillingZip { get { return this.mBillingZip; } set { this.mBillingZip = value; } }
        [DataMember]
        public string BillingZip4 { get { return this.mBillingZip4; } set { this.mBillingZip4 = value; } }
        [DataMember]
        public bool Approved { get { return this.mApproved; } set { this.mApproved = value; } }
        [DataMember]
        public DateTime ApprovedDate { get { return this.mApprovedDate; } set { this.mApprovedDate = value; } }
        [DataMember]
        public string ApprovedUser { get { return this.mApprovedUser; } set { this.mApprovedUser = value; } }
        [DataMember]
        public int SalesRepClientID { get { return this.mSalesRepClientID; } set { this.mSalesRepClientID = value; } }
        [DataMember]
        public string Status { get { return this.mStatus; } set { this.mStatus = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class LTLShipper {
        //Members
        private int mID = 0,mClientID = 0;
        private string mName = "";
        private string mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip = "",mZip4 = "";
        private string mContactName = "",mContactPhone = "",mContactEmail = "";
        private DateTime mWindowStartTime, mWindowEndTime;
        private string mStatus = "A";
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public LTLShipper() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public int ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        [DataMember]
        public string Name { get { return this.mName; } set { this.mName = value; } }
        [DataMember]
        public string AddressLine1 { get { return this.mAddressLine1; } set { this.mAddressLine1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this.mAddressLine2; } set { this.mAddressLine2 = value; } }
        [DataMember]
        public string City { get { return this.mCity; } set { this.mCity = value; } }
        [DataMember]
        public string State { get { return this.mState; } set { this.mState = value; } }
        [DataMember]
        public string Zip { get { return this.mZip; } set { this.mZip = value; } }
        [DataMember]
        public string Zip4 { get { return this.mZip4; } set { this.mZip4 = value; } }
        [DataMember]
        public string ContactName { get { return this.mContactName; } set { this.mContactName = value; } }
        [DataMember]
        public string ContactPhone { get { return this.mContactPhone; } set { this.mContactPhone = value; } }
        [DataMember]
        public string ContactEmail { get { return this.mContactEmail; } set { this.mContactEmail = value; } }
        [DataMember]
        public DateTime WindowStartTime { get { return this.mWindowStartTime; } set { this.mWindowStartTime = value; } }
        [DataMember]
        public DateTime WindowEndTime { get { return this.mWindowEndTime; } set { this.mWindowEndTime = value; } }
        [DataMember]
        public string Status { get { return this.mStatus; } set { this.mStatus = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class LTLConsignee {
        //Members
        private int mID = 0,mClientID = 0;
        private string mName = "";
        private string mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip = "",mZip4 = "";
        private string mContactName = "",mContactPhone = "",mContactEmail = "";
        private DateTime mWindowStartTime,mWindowEndTime;
        private string mStatus = "A";
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public LTLConsignee() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public int ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        [DataMember]
        public string Name { get { return this.mName; } set { this.mName = value; } }
        [DataMember]
        public string AddressLine1 { get { return this.mAddressLine1; } set { this.mAddressLine1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this.mAddressLine2; } set { this.mAddressLine2 = value; } }
        [DataMember]
        public string City { get { return this.mCity; } set { this.mCity = value; } }
        [DataMember]
        public string State { get { return this.mState; } set { this.mState = value; } }
        [DataMember]
        public string Zip { get { return this.mZip; } set { this.mZip = value; } }
        [DataMember]
        public string Zip4 { get { return this.mZip4; } set { this.mZip4 = value; } }
        [DataMember]
        public string ContactName { get { return this.mContactName; } set { this.mContactName = value; } }
        [DataMember]
        public string ContactPhone { get { return this.mContactPhone; } set { this.mContactPhone = value; } }
        [DataMember]
        public string ContactEmail { get { return this.mContactEmail; } set { this.mContactEmail = value; } }
        [DataMember]
        public DateTime WindowStartTime { get { return this.mWindowStartTime; } set { this.mWindowStartTime = value; } }
        [DataMember]
        public DateTime WindowEndTime { get { return this.mWindowEndTime; } set { this.mWindowEndTime = value; } }
        [DataMember]
        public string Status { get { return this.mStatus; } set { this.mStatus = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class LTLShipment {
        //Members
        private int mID = 0;
        private string mShipmentNumber = "";
        private DateTime mCreated, mShipDate;
        private int mClientID = 0;
        private string mClientNumber = "",mClientName = "";
        private int mShipperID = 0,mConsigneeID = 0;
        private string mShipperNumber="", mShipperName = "", mShipperAddress = "";
        private string mShipperContactName = "",mShipperContactPhone = "";
        private DateTime mShipperWindowStartTime = DateTime.MinValue,mShipperWindowEndTime = DateTime.MinValue;
        private int mPallet1Weight = 0,mPallet2Weight = 0,mPallet3Weight = 0,mPallet4Weight = 0,mPallet5Weight = 0;
        private string mPallet1Class = "",mPallet2Class = "",mPallet3Class = "",mPallet4Class = "",mPallet5Class = "";
        private decimal mPallet1InsuranceValue = 0,mPallet2InsuranceValue = 0,mPallet3InsuranceValue = 0,mPallet4InsuranceValue = 0,mPallet5InsuranceValue = 0;
        private bool mInsidePickup = false,mLiftGateOrigin = false;
        private bool mInsideDelivery = false,mLiftGateDestination = false;
        private DateTime mAppointmentOrigin = DateTime.MinValue,mAppointmentDestination = DateTime.MinValue;
        private int mPallets = 0;
        private decimal mWeight = 0;
        private decimal mPalletRate = 0,mFuelSurcharge = 0,mAccessorialCharge = 0,mInsuranceCharge = 0,mTollCharge = 0,mTotalCharge = 0;
        private DateTime mLastUpdated;
        private string mUserID = "";
        private int mPickupID = 0;
        private DateTime mPickupDate;

        //Interface
        public LTLShipment() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public string ShipmentNumber { get { return this.mShipmentNumber; } set { this.mShipmentNumber = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public DateTime ShipDate { get { return this.mShipDate; } set { this.mShipDate = value; } }
        [DataMember]
        public int ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        [DataMember]
        public int ShipperID { get { return this.mShipperID; } set { this.mShipperID = value; } }
        [DataMember]
        public string ShipperNumber { get { return this.mShipperNumber; } set { this.mShipperNumber = value; } }
        [DataMember]
        public string ShipperName { get { return this.mShipperName; } set { this.mShipperName = value; } }
        [DataMember]
        public string ShipperAddress { get { return this.mShipperAddress; } set { this.mShipperAddress = value; } }
        [DataMember]
        public string ShipperContactName { get { return this.mShipperContactName; } set { this.mShipperContactName = value; } }
        [DataMember]
        public string ShipperContactPhone { get { return this.mShipperContactPhone; } set { this.mShipperContactPhone = value; } }
        [DataMember]
        public DateTime ShipperWindowStartTime { get { return this.mShipperWindowStartTime; } set { this.mShipperWindowStartTime = value; } }
        [DataMember]
        public DateTime ShipperWindowEndTime { get { return this.mShipperWindowEndTime; } set { this.mShipperWindowEndTime = value; } }
        [DataMember]
        public int ConsigneeID { get { return this.mConsigneeID; } set { this.mConsigneeID = value; } }
        [DataMember]
        public int Pallet1Weight { get { return this.mPallet1Weight; } set { this.mPallet1Weight = value; } }
        [DataMember]
        public string Pallet1Class { get { return this.mPallet1Class; } set { this.mPallet1Class = value; } }
        [DataMember]
        public decimal Pallet1InsuranceValue { get { return this.mPallet1InsuranceValue; } set { this.mPallet1InsuranceValue = value; } }
        [DataMember]
        public int Pallet2Weight { get { return this.mPallet2Weight; } set { this.mPallet2Weight = value; } }
        [DataMember]
        public string Pallet2Class { get { return this.mPallet2Class; } set { this.mPallet2Class = value; } }
        [DataMember]
        public decimal Pallet2InsuranceValue { get { return this.mPallet2InsuranceValue; } set { this.mPallet2InsuranceValue = value; } }
        [DataMember]
        public int Pallet3Weight { get { return this.mPallet3Weight; } set { this.mPallet3Weight = value; } }
        [DataMember]
        public string Pallet3Class { get { return this.mPallet3Class; } set { this.mPallet3Class = value; } }
        [DataMember]
        public decimal Pallet3InsuranceValue { get { return this.mPallet3InsuranceValue; } set { this.mPallet3InsuranceValue = value; } }
        [DataMember]
        public int Pallet4Weight { get { return this.mPallet4Weight; } set { this.mPallet4Weight = value; } }
        [DataMember]
        public string Pallet4Class { get { return this.mPallet4Class; } set { this.mPallet4Class = value; } }
        [DataMember]
        public decimal Pallet4InsuranceValue { get { return this.mPallet4InsuranceValue; } set { this.mPallet4InsuranceValue = value; } }
        [DataMember]
        public int Pallet5Weight { get { return this.mPallet5Weight; } set { this.mPallet5Weight = value; } }
        [DataMember]
        public string Pallet5Class { get { return this.mPallet5Class; } set { this.mPallet5Class = value; } }
        [DataMember]
        public decimal Pallet5InsuranceValue { get { return this.mPallet5InsuranceValue; } set { this.mPallet5InsuranceValue = value; } }
        [DataMember]
        public bool InsidePickup { get { return this.mInsidePickup; } set { this.mInsidePickup = value; } }
        [DataMember]
        public bool LiftGateOrigin { get { return this.mLiftGateOrigin; } set { this.mLiftGateOrigin = value; } }
        [DataMember]
        public DateTime AppointmentOrigin { get { return this.mAppointmentOrigin; } set { this.mAppointmentOrigin = value; } }
        [DataMember]
        public bool InsideDelivery { get { return this.mInsideDelivery; } set { this.mInsideDelivery = value; } }
        [DataMember]
        public bool LiftGateDestination { get { return this.mLiftGateDestination; } set { this.mLiftGateDestination = value; } }
        [DataMember]
        public DateTime AppointmentDestination { get { return this.mAppointmentDestination; } set { this.mAppointmentDestination = value; } }
        [DataMember]
        public int Pallets { get { return this.mPallets; } set { this.mPallets = value; } }
        [DataMember]
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public decimal PalletRate { get { return this.mPalletRate; } set { this.mPalletRate = value; } }
        [DataMember]
        public decimal FuelSurcharge { get { return this.mFuelSurcharge; } set { this.mFuelSurcharge = value; } }
        [DataMember]
        public decimal AccessorialCharge { get { return this.mAccessorialCharge; } set { this.mAccessorialCharge = value; } }
        [DataMember]
        public decimal InsuranceCharge { get { return this.mInsuranceCharge; } set { this.mInsuranceCharge = value; } }
        [DataMember]
        public decimal TollCharge { get { return this.mTollCharge; } set { this.mTollCharge = value; } }
        [DataMember]
        public decimal TotalCharge { get { return this.mTotalCharge; } set { this.mTotalCharge = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        [DataMember]
        public int PickupID { get { return this.mPickupID; } set { this.mPickupID = value; } }
        [DataMember]
        public DateTime PickupDate { get { return this.mPickupDate; } set { this.mPickupDate = value; } }
        #endregion
    }

    [DataContract]
    public class LTLPallet {
        //Members
        private int mID = 0;
        private string mPalletNumber = "";
        private int mShipmentID = 0;
        private int mWeight = 0;
        private string mNMFCClass = "FAK";
        private decimal mInsuranceValue = 0;

        //Interface
        public LTLPallet() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public string PalletNumber { get { return this.mPalletNumber; } set { this.mPalletNumber = value; } }
        [DataMember]
        public int ShipmentID { get { return this.mShipmentID; } set { this.mShipmentID = value; } }
        [DataMember]
        public int Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public string NMFCClass { get { return this.mNMFCClass; } set { this.mNMFCClass = value; } }
        [DataMember]
        public decimal InsuranceValue { get { return this.mInsuranceValue; } set { this.mInsuranceValue = value; } }
        #endregion
    }

    [DataContract]
    public class ServiceLocation {
        //Members
        private string mZipCode = "", mCity = "", mState = "";
        private string mZoneCode = "",mAgentNumber = "";

        //Interface
        public ServiceLocation() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ZipCode { get { return this.mZipCode; } set { this.mZipCode = value; } }
        [DataMember]
        public string City { get { return this.mCity; } set { this.mCity = value; } }
        [DataMember]
        public string State { get { return this.mState; } set { this.mState = value; } }
        [DataMember]
        public string ZoneCode { get { return this.mZoneCode; } set { this.mZoneCode = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        #endregion
    }

    [DataContract]
    public class LTLTariffRate {
        //Members
        private string mOriginZone = "",mDestinationZipCode = "";
        private DateTime mEffectiveDate;
        private decimal mRate = 0.0M;
        private int mTransitMin = 1,mTransitMax = 1;

        //Interface
        public LTLTariffRate() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string OriginZone { get { return this.mOriginZone; } set { this.mOriginZone = value; } }
        [DataMember]
        public string DestinationZipCode { get { return this.mDestinationZipCode; } set { this.mDestinationZipCode = value; } }
        [DataMember]
        public DateTime EffectiveDate { get { return this.mEffectiveDate; } set { this.mEffectiveDate = value; } }
        [DataMember]
        public decimal Rate { get { return this.mRate; } set { this.mRate = value; } }
        [DataMember]
        public int TransitMin { get { return this.mTransitMin; } set { this.mTransitMin = value; } }
        [DataMember]
        public int TransitMax { get { return this.mTransitMax; } set { this.mTransitMax = value; } }
        #endregion
    }

    [DataContract]
    public class LTLFault {
        private string mMessage = "";
        public LTLFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
