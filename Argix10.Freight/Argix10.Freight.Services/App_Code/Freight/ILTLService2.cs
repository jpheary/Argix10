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
    public interface ILTLService2 {
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
    public interface ILTLClientService2 {
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLQuote2 CreateQuote(LTLQuote2 entry);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLClient(LTLClient2 client);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLClient2 ReadLTLClient(int clientID);
        [OperationContract(Name = "ReadLTLClientByNumber")]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLClient2 ReadLTLClient(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLClient(LTLClient2 client);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet GetLTLClientList(string salesRepClientNumber);

        [OperationContract(Name = "ViewLTLShippersForClient")]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLShippers(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        string CreateLTLShipper(LTLShipper2 shipper);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLShippersList(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLShipper2 ReadLTLShipper(string shipperNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLShipper(LTLShipper2 shipper);

        [OperationContract(Name = "ViewLTLConsigneesForClient")]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLConsignees(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLConsignee(LTLConsignee2 consignee);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLConsigneesList(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLConsignee2 ReadLTLConsignee(int consigneeNumber,string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLConsignee(LTLConsignee2 consignee);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLShipments(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet SearchLTLShipments(LTLSearch2 criteria);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        string CreateLTLShipment(LTLShipment2 shipment);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        LTLShipment2 ReadLTLShipment(string shipmentNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLShipment(LTLShipment2 shipment);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        bool CancelLTLShipment(string shipmentNumber,string userID);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        ServiceLocation ReadPickupLocation(string zipCode);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        ServiceLocation ReadServiceLocation(string zipCode);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadPalletLabelData(string shipmentNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadPalletBOLData(string shipmentNumber);
    }

    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface ILTLAdminService2 {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLQuote2 CreateQuoteForAdmin(LTLQuote2 entry);

        [OperationContract(Name = "ViewLTLClientsForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLClients();
        [OperationContract][FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLClientForAdmin(LTLClient2 client);
        [OperationContract(Name = "ReadLTLClientForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLClient2 ReadLTLClient(int clientID);
        [OperationContract][FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLClientForAdmin(LTLClient2 client);
        [OperationContract][FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool ApproveLTLClient(int clientID, bool approve, string username);
        [OperationContract][FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLClientListForAdmin();

        [OperationContract(Name = "ViewLTLShippersForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLShippers(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        string CreateLTLShipperForAdmin(LTLShipper2 shipper);
        [OperationContract(Name = "ReadLTLShipperForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLShipper2 ReadLTLShipper(string shipperNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLShipperForAdmin(LTLShipper2 shipper);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLShippersListForAdmin(string clientNumber);

        [OperationContract(Name = "ViewLTLConsigneesForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLConsignees(string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        int CreateLTLConsigneeForAdmin(LTLConsignee2 consignee);
        [OperationContract(Name = "ReadLTLConsigneeForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLConsignee2 ReadLTLConsignee(int consigneeNumber, string clientNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLConsigneeForAdmin(LTLConsignee2 consignee);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadLTLConsigneesListForAdmin(string clientNumber);

        [OperationContract(Name = "ViewLTLShipmentsForDispatch")]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLTLShipments(string clientNumber);
        [OperationContract(Name = "SearchLTLShipmentsForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet SearchLTLShipments(LTLSearch2 criteria);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        string CreateLTLShipmentForAdmin(LTLShipment2 shipment);
        [OperationContract(Name = "ReadLTLShipmentForDispatch")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLShipment2 ReadLTLShipment(string shipmentNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLTLShipmentForAdmin(LTLShipment2 shipment);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool CancelLTLShipmentForAdmin(string shipmentNumber, string userID);

        [OperationContract(Name = "ReadPickupLocationForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        ServiceLocation ReadPickupLocation(string zipCode);
        [OperationContract(Name = "ReadPickupLocationsForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        ServiceLocations ReadPickupLocations(string city, string state);
        [OperationContract(Name = "ReadServiceLocationForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        ServiceLocation ReadServiceLocation(string zipCode);
        [OperationContract(Name = "ReadServiceLocationsForAdmin")]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        ServiceLocations ReadServiceLocations(string city, string state);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault),Action = "http://Argix.Freight.LTLFault")]
        DataSet ReadPalletLabels(string shipmentNumber);
    }


    [ServiceContract(Namespace = "http://Argix.Freight")]
    public interface ILTLLoadTenderService2 {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLQuote2 CreateQuoteForSalesRep(LTLQuote2 entry);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        DataSet ViewLoadTenderQuotes(string owner);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LTLLoadTenderQuote ReadLoadTenderQuote(int id);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        int CreateLoadTenderQuote(LTLLoadTenderQuote quote);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool UpdateLoadTenderQuote(LTLLoadTenderQuote quote);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool ApproveLoadTenderQuote(int quoteID, DateTime approved, string approvedBy);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool TenderLoadTenderQuote(int quoteID, LoadTender loadTender);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool BookLoadTenderQuote(int quoteID, LTLShipment2 shipment);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool CancelLoadTenderQuote(int quoteID, DateTime cancelled, string cancelledBy);
        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        bool ChangeOwnerLoadTenderQuote(int quoteID, string owner);

        [OperationContract]
        [FaultContractAttribute(typeof(LTLFault), Action = "http://Argix.Freight.LTLFault")]
        LoadTender GetLoadTender(int number);
    }

    [DataContract]
    public class LTLQuote2 {
        //Members
        private int mID = 0;
        private DateTime mCreated;
        private DateTime mShipDate;
        private int mClientID = 0;
        private string mShipperNumber = "";
        private int mConsigneeNumber = 0;
        private string mOriginCity, mOriginState, mOriginZip = "";
        private string mDestinationCity, mDestinationState, mDestinationZip = "";
        private int mPallet1Weight = 0,mPallet2Weight = 0,mPallet3Weight = 0,mPallet4Weight = 0,mPallet5Weight = 0;
        private string mPallet1Class = "",mPallet2Class = "",mPallet3Class = "",mPallet4Class = "",mPallet5Class = "";
        private decimal mPallet1InsuranceValue = 0,mPallet2InsuranceValue = 0,mPallet3InsuranceValue = 0,mPallet4InsuranceValue = 0,mPallet5InsuranceValue = 0;
        private bool mInsidePickup = false, mLiftGateOrigin = false, mAppointmentOrigin = false, mSameDayPickup=false;
        private bool mInsideDelivery = false,mLiftGateDestination = false,mAppointmentDestination = false;
        private int mPallets = 0;
        private decimal mWeight = 0;
        private decimal mPalletRate = 0,mFuelSurcharge = 0,mAccessorialCharge = 0,mInsuranceCharge = 0,mTollCharge = 0,mTotalCharge = 0;
        private decimal mInsidePickupCharge = 0, mLiftGateOriginCharge = 0, mAppointmentOriginCharge = 0, mSameDayPickupCharge = 0, mInsideDeliveryCharge = 0, mLiftGateDestinationCharge = 0, mAppointmentDestinationCharge = 0;
        private int mTransitMin = 0,mTransitMax = 0;
        private DateTime mEstimatedDeliveryDate;

        //Interface
        public LTLQuote2() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public DateTime ShipDate { get { return this.mShipDate; } set { this.mShipDate = value; } }
        [DataMember]
        public int ClientID { get { return this.mClientID; } set { this.mClientID = value; } }
        [DataMember]
        public string ShipperNumber { get { return this.mShipperNumber; } set { this.mShipperNumber = value; } }
        [DataMember]
        public int ConsigneeNumber { get { return this.mConsigneeNumber; } set { this.mConsigneeNumber = value; } }
        [DataMember]
        public string OriginCity { get { return this.mOriginCity; } set { this.mOriginCity = value; } }
        [DataMember]
        public string OriginState { get { return this.mOriginState; } set { this.mOriginState = value; } }
        [DataMember]
        public string OriginZip { get { return this.mOriginZip; } set { this.mOriginZip = value; } }
        [DataMember]
        public string DestinationCity { get { return this.mDestinationCity; } set { this.mDestinationCity = value; } }
        [DataMember]
        public string DestinationState { get { return this.mDestinationState; } set { this.mDestinationState = value; } }
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
        public bool SameDayPickup { get { return this.mSameDayPickup; } set { this.mSameDayPickup = value; } }
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
        public decimal InsidePickupCharge { get { return this.mInsidePickupCharge; } set { this.mInsidePickupCharge = value; } }
        [DataMember]
        public decimal LiftGateOriginCharge { get { return this.mLiftGateOriginCharge; } set { this.mLiftGateOriginCharge = value; } }
        [DataMember]
        public decimal AppointmentOriginCharge { get { return this.mAppointmentOriginCharge; } set { this.mAppointmentOriginCharge = value; } }
        [DataMember]
        public decimal SameDayPickupCharge { get { return this.mSameDayPickupCharge; } set { this.mSameDayPickupCharge = value; } }
        [DataMember]
        public decimal InsideDeliveryCharge { get { return this.mInsideDeliveryCharge; } set { this.mInsideDeliveryCharge = value; } }
        [DataMember]
        public decimal LiftGateDestinationCharge { get { return this.mLiftGateDestinationCharge; } set { this.mLiftGateDestinationCharge = value; } }
        [DataMember]
        public decimal AppointmentDestinationCharge { get { return this.mAppointmentDestinationCharge; } set { this.mAppointmentDestinationCharge = value; } }
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
        [DataMember]
        public DateTime EstimatedDeliveryDate { get { return this.mEstimatedDeliveryDate; } set { this.mEstimatedDeliveryDate = value; } }
        #endregion
    }

    [DataContract]
    public class LTLClient2 {
        //Members
        private int mID = 0;
        private string mNumber = "";
        private string mName = "";
        private string mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip = "",mZip4 = "";
        private string mContactName = "",mContactPhone = "",mContactEmail = "";
        private string mCorporateName = "";
        private string mCorporateAddressLine1 = "",mCorporateAddressLine2 = "",mCorporateCity = "",mCorporateState = "",mCorporateZip = "",mCorporateZip4 = "",mTaxID = "";
        private string mBillingAddressLine1 = "",mBillingAddressLine2 = "",mBillingCity = "",mBillingState = "",mBillingZip = "",mBillingZip4 = "";
        private DateTime mApprovalDate, mDenialDate;
        private string mApprovalUser = "", mDenialUser="";
        private int mIsActive = 0;
        private string mSalesRepClientNumber = "";
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public LTLClient2()  { }
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
        public string TaxID { get { return this.mTaxID; } set { this.mTaxID = value; } }
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
        public DateTime ApprovalDate { get { return this.mApprovalDate; } set { this.mApprovalDate = value; } }
        [DataMember]
        public string ApprovalUser { get { return this.mApprovalUser; } set { this.mApprovalUser = value; } }
        [DataMember]
        public DateTime DenialDate { get { return this.mDenialDate; } set { this.mDenialDate = value; } }
        [DataMember]
        public string DenialUser { get { return this.mDenialUser; } set { this.mDenialUser = value; } }
        [DataMember]
        public string SalesRepClientNumber { get { return this.mSalesRepClientNumber; } set { this.mSalesRepClientNumber = value; } }
        [DataMember]
        public int IsActive { get { return this.mIsActive; } set { this.mIsActive = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public class LTLShipper2 {
        //Members
        private string mNumber = "",mClientNumber = "", mName = "";
        private string mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip = "",mZip4 = "";
        private string mContactName = "",mContactPhone = "",mContactEmail = "";
        private DateTime mWindowTimeStart, mWindowTimeEnd;
        private string mAgentNumber = "",mAgentName = "";
        private DateTime mLastUpdated;
        private string mUserID = "";
        private byte[] mRowversion = null;

        //Interface
        public LTLShipper2() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
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
        public DateTime WindowTimeStart { get { return this.mWindowTimeStart; } set { this.mWindowTimeStart = value; } }
        [DataMember]
        public DateTime WindowTimeEnd { get { return this.mWindowTimeEnd; } set { this.mWindowTimeEnd = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        [DataMember]
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        [DataMember]
        public byte[] Rowversion { get { return this.mRowversion; } set { this.mRowversion = value; } }
        #endregion
    }

    [DataContract]
    public class LTLConsignee2 {
        //Members
        private int mNumber = 0;
        private string mClientNumber = "", mName = "";
        private string mAddressLine1 = "",mAddressLine2 = "",mCity = "",mState = "",mZip = "",mZip4 = "";
        private string mContactName = "",mContactPhone = "",mContactEmail = "";
        private DateTime mWindowTimeStart,mWindowTimeEnd;
        private string mAgentNumber="", mAgentName="";
        private DateTime mLastUpdated;
        private string mUserID = "";
        private byte[] mRowversion = null;

        //Interface
        public LTLConsignee2() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
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
        public DateTime WindowTimeStart { get { return this.mWindowTimeStart; } set { this.mWindowTimeStart = value; } }
        [DataMember]
        public DateTime WindowTimeEnd { get { return this.mWindowTimeEnd; } set { this.mWindowTimeEnd = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        [DataMember]
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        [DataMember]
        public byte[] Rowversion { get { return this.mRowversion; } set { this.mRowversion = value; } }
        #endregion
    }

    [DataContract]
    public class LTLShipment2 {
        //Members
        private string mShipmentNumber = "", mBLNumber;
        private DateTime mShipDate;
        private string mClientNumber = "",mClientName = "";
        private string mShipperNumber="", mShipperName = "";
        private int mConsigneeNumber = 0;
        private string mConsigneeName="";
        private string mContactName = "", mContactPhone = "";
        private decimal mPallet1Weight = 0,mPallet2Weight = 0,mPallet3Weight = 0,mPallet4Weight = 0,mPallet5Weight = 0;
        private string mPallet1Class = "FAK",mPallet2Class = "FAK",mPallet3Class = "FAK",mPallet4Class = "FAK",mPallet5Class = "FAK";
        private decimal mPallet1InsuranceValue = 0,mPallet2InsuranceValue = 0,mPallet3InsuranceValue = 0,mPallet4InsuranceValue = 0,mPallet5InsuranceValue = 0;
        private bool mInsidePickup = false, mLiftGateOrigin = false, mSameDayPickup = false;
        private bool mInsideDelivery = false,mLiftGateDestination = false;
        private DateTime mPickupAppointmentDate = DateTime.MinValue,mPickupAppointmentWindowTimeStart = DateTime.MinValue,mPickupAppointmentWindowTimeEnd = DateTime.MinValue;
        private DateTime mDeliveryAppointmentDate = DateTime.MinValue,mDeliveryAppointmentWindowTimeStart = DateTime.MinValue,mDeliveryAppointmentWindowTimeEnd = DateTime.MinValue;
        private int mPallets = 0;
        private decimal mWeight = 0;
        private decimal mPalletRate = 0,mFuelSurcharge = 0,mAccessorialCharge = 0,mInsuranceCharge = 0,mTollCharge = 0,mTotalCharge = 0;
        private string mTerminalCode = "";
        private decimal mLTLZone = 0m;
        private string mCreatedUserID="";
        private DateTime mCreated, mCancelled;
        private int mPickupID = 0;
        private DateTime mPickupDate;
        private DateTime mLastUpdated;
        private string mUserID = "";
        private byte[] mRowversion = null;
        private LTLPallets2 mItems = null;
        private DateTime mEstimatedDeliveryDate, mDeliveryDate;

        //Interface
        public LTLShipment2() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ShipmentNumber { get { return this.mShipmentNumber; } set { this.mShipmentNumber = value; } }
        [DataMember]
        public string BLNumber { get { return this.mBLNumber; } set { this.mBLNumber = value; } }
        [DataMember]
        public DateTime ShipDate { get { return this.mShipDate; } set { this.mShipDate = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        [DataMember]
        public string ShipperNumber { get { return this.mShipperNumber; } set { this.mShipperNumber = value; } }
        [DataMember]
        public string ShipperName { get { return this.mShipperName; } set { this.mShipperName = value; } }
        [DataMember]
        public int ConsigneeNumber { get { return this.mConsigneeNumber; } set { this.mConsigneeNumber = value; } }
        [DataMember]
        public string ConsigneeName { get { return this.mConsigneeName; } set { this.mConsigneeName = value; } }
        [DataMember]
        public string ContactName { get { return this.mContactName; } set { this.mContactName = value; } }
        [DataMember]
        public string ContactPhone { get { return this.mContactPhone; } set { this.mContactPhone = value; } }
        [DataMember]
        public decimal Pallet1Weight { get { return this.mPallet1Weight; } set { this.mPallet1Weight = value; } }
        [DataMember]
        public string Pallet1Class { get { return this.mPallet1Class; } set { this.mPallet1Class = value; } }
        [DataMember]
        public decimal Pallet1InsuranceValue { get { return this.mPallet1InsuranceValue; } set { this.mPallet1InsuranceValue = value; } }
        [DataMember]
        public decimal Pallet2Weight { get { return this.mPallet2Weight; } set { this.mPallet2Weight = value; } }
        [DataMember]
        public string Pallet2Class { get { return this.mPallet2Class; } set { this.mPallet2Class = value; } }
        [DataMember]
        public decimal Pallet2InsuranceValue { get { return this.mPallet2InsuranceValue; } set { this.mPallet2InsuranceValue = value; } }
        [DataMember]
        public decimal Pallet3Weight { get { return this.mPallet3Weight; } set { this.mPallet3Weight = value; } }
        [DataMember]
        public string Pallet3Class { get { return this.mPallet3Class; } set { this.mPallet3Class = value; } }
        [DataMember]
        public decimal Pallet3InsuranceValue { get { return this.mPallet3InsuranceValue; } set { this.mPallet3InsuranceValue = value; } }
        [DataMember]
        public decimal Pallet4Weight { get { return this.mPallet4Weight; } set { this.mPallet4Weight = value; } }
        [DataMember]
        public string Pallet4Class { get { return this.mPallet4Class; } set { this.mPallet4Class = value; } }
        [DataMember]
        public decimal Pallet4InsuranceValue { get { return this.mPallet4InsuranceValue; } set { this.mPallet4InsuranceValue = value; } }
        [DataMember]
        public decimal Pallet5Weight { get { return this.mPallet5Weight; } set { this.mPallet5Weight = value; } }
        [DataMember]
        public string Pallet5Class { get { return this.mPallet5Class; } set { this.mPallet5Class = value; } }
        [DataMember]
        public decimal Pallet5InsuranceValue { get { return this.mPallet5InsuranceValue; } set { this.mPallet5InsuranceValue = value; } }
        [DataMember]
        public bool InsidePickup { get { return this.mInsidePickup; } set { this.mInsidePickup = value; } }
        [DataMember]
        public bool LiftGateOrigin { get { return this.mLiftGateOrigin; } set { this.mLiftGateOrigin = value; } }
        [DataMember]
        public DateTime PickupAppointmentDate { get { return this.mPickupAppointmentDate; } set { this.mPickupAppointmentDate = value; } }
        [DataMember]
        public DateTime PickupAppointmentWindowTimeStart { get { return this.mPickupAppointmentWindowTimeStart; } set { this.mPickupAppointmentWindowTimeStart = value; } }
        [DataMember]
        public DateTime PickupAppointmentWindowTimeEnd { get { return this.mPickupAppointmentWindowTimeEnd; } set { this.mPickupAppointmentWindowTimeEnd = value; } }
        [DataMember]
        public bool SameDayPickup { get { return this.mSameDayPickup; } set { this.mSameDayPickup = value; } }
        [DataMember]
        public bool InsideDelivery { get { return this.mInsideDelivery; } set { this.mInsideDelivery = value; } }
        [DataMember]
        public bool LiftGateDestination { get { return this.mLiftGateDestination; } set { this.mLiftGateDestination = value; } }
        [DataMember]
        public DateTime DeliveryAppointmentDate { get { return this.mDeliveryAppointmentDate; } set { this.mDeliveryAppointmentDate = value; } }
        [DataMember]
        public DateTime DeliveryAppointmentWindowTimeStart { get { return this.mDeliveryAppointmentWindowTimeStart; } set { this.mDeliveryAppointmentWindowTimeStart = value; } }
        [DataMember]
        public DateTime DeliveryAppointmentWindowTimeEnd { get { return this.mDeliveryAppointmentWindowTimeEnd; } set { this.mDeliveryAppointmentWindowTimeEnd = value; } }
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
        public string TerminalCode { get { return this.mTerminalCode; } set { this.mTerminalCode = value; } }
        [DataMember]
        public decimal LTLZone { get { return this.mLTLZone; } set { this.mLTLZone = value; } }
        [DataMember]
        public DateTime Created { get { return this.mCreated; } set { this.mCreated = value; } }
        [DataMember]
        public string CreatedUserID { get { return this.mCreatedUserID; } set { this.mCreatedUserID = value; } }
        [DataMember]
        public int PickupID { get { return this.mPickupID; } set { this.mPickupID = value; } }
        [DataMember]
        public DateTime PickupDate { get { return this.mPickupDate; } set { this.mPickupDate = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        [DataMember]
        public byte[] Rowversion { get { return this.mRowversion; } set { this.mRowversion = value; } }
        [DataMember]
        public LTLPallets2 Items { get { return this.mItems; } set { this.mItems = value; } }
        [DataMember]
        public DateTime EstimatedDeliveryDate { get { return this.mEstimatedDeliveryDate; } set { this.mEstimatedDeliveryDate = value; } }
        [DataMember]
        public DateTime DeliveryDate { get { return this.mDeliveryDate; } set { this.mDeliveryDate = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class LTLPallets2 : BindingList<LTLPallet2> { public LTLPallets2() { } }

    [DataContract]
    public class LTLPallet2 {
        //Members
        private long mTrackingNumber = 0;
        private string mItemNumber = "";
        private string mShipmentNumber = "", mPONumber="", mReferenceNumber="";
        private decimal mWeight = 0;
        private string mNMFCClass = "FAK";
        private decimal mInsuranceValue = 0;

        //Interface
        public LTLPallet2() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public long TrackingNumber { get { return this.mTrackingNumber; } set { this.mTrackingNumber = value; } }
        [DataMember]
        public string ItemNumber { get { return this.mItemNumber; } set { this.mItemNumber = value; } }
        [DataMember]
        public string ShipmentNumber { get { return this.mShipmentNumber; } set { this.mShipmentNumber = value; } }
        [DataMember]
        public string PONumber { get { return this.mPONumber; } set { this.mPONumber = value; } }
        [DataMember]
        public string ReferenceNumber { get { return this.mReferenceNumber; } set { this.mReferenceNumber = value; } }
        [DataMember]
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public string NMFCClass { get { return this.mNMFCClass; } set { this.mNMFCClass = value; } }
        [DataMember]
        public decimal InsuranceValue { get { return this.mInsuranceValue; } set { this.mInsuranceValue = value; } }
        #endregion
    }

    [DataContract]
    public class LTLLoadTenderQuote {
        //Members
        private int mID = 0;
        private string mOwner="";
        private string mBrokerName = "", mContactName = "", mContactPhone = "", mContactEmail = "", mDescription = "", mComments = "";
        private LTLQuote2 mLTLQuote = null;
        private decimal mTotalChargeMin = 0;
        private DateTime mLogged = DateTime.MinValue, mApproved = DateTime.MinValue, mCancelled = DateTime.MinValue;
        private string mLoggedBy = "", mApprovedBy = "", mCancelledBy = "";
        private int mLoadTenderNumber = 0;
        private string mShipmentNumber = "";

        //Interface
        public LTLLoadTenderQuote() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public string Owner { get { return this.mOwner; } set { this.mOwner = value; } }
        [DataMember]
        public string BrokerName { get { return this.mBrokerName; } set { this.mBrokerName = value; } }
        [DataMember]
        public string ContactName { get { return this.mContactName; } set { this.mContactName = value; } }
        [DataMember]
        public string ContactPhone { get { return this.mContactPhone; } set { this.mContactPhone = value; } }
        [DataMember]
        public string ContactEmail { get { return this.mContactEmail; } set { this.mContactEmail = value; } }
        [DataMember]
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        [DataMember]
        public string Comments { get { return this.mComments; } set { this.mComments = value; } }
        [DataMember]
        public LTLQuote2 LTLQuote { get { return this.mLTLQuote; } set { this.mLTLQuote = value; } }
        [DataMember]
        public decimal TotalChargeMin { get { return this.mTotalChargeMin; } set { this.mTotalChargeMin = value; } }
        [DataMember]
        public DateTime Logged { get { return this.mLogged; } set { this.mLogged = value; } }
        [DataMember]
        public string LoggedBy { get { return this.mLoggedBy; } set { this.mLoggedBy = value; } }
        [DataMember]
        public DateTime Approved { get { return this.mApproved; } set { this.mApproved = value; } }
        [DataMember]
        public string ApprovedBy { get { return this.mApprovedBy; } set { this.mApprovedBy = value; } }
        [DataMember]
        public int LoadTenderNumber { get { return this.mLoadTenderNumber; } set { this.mLoadTenderNumber = value; } }
        [DataMember]
        public string ShipmentNumber { get { return this.mShipmentNumber; } set { this.mShipmentNumber = value; } }
        [DataMember]
        public DateTime Cancelled { get { return this.mCancelled; } set { this.mCancelled = value; } }
        [DataMember]
        public string CancelledBy { get { return this.mCancelledBy; } set { this.mCancelledBy = value; } }
        #endregion
    }

    [DataContract]
    public class LoadTender {
        //Members
        private int mNumber = 0;
        private string mFilename = "";
        private byte[] mFile = null;

        //Interface
        public LoadTender() {
            //Constructor
            this.mNumber = 0;
            this.mFilename = "";
            this.mFile = null;
        }
        #region Members
        [DataMember]
        public int Number { get { return this.mNumber; } set { this.mNumber = value; } }
        [DataMember]
        public string Filename { get { return this.mFilename; } set { this.mFilename = value; } }
        [DataMember]
        public byte[] File { get { return this.mFile; } set { this.mFile = value; } }
        #endregion
    }

    [DataContract]
    public class LTLSearch2 {
        //Members
        private DateTime mShipDateStart, mShipDateEnd;
        private string mClientNumber = "";
        private string mShipperNumber = "";
        private int mConsigneeNumber = 0;

        //Interface
        public LTLSearch2() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public DateTime ShipDateStart { get { return this.mShipDateStart; } set { this.mShipDateStart = value; } }
        [DataMember]
        public DateTime ShipDateEnd { get { return this.mShipDateEnd; } set { this.mShipDateEnd = value; } }
        [DataMember]
        public string ClientNumber { get { return this.mClientNumber; } set { this.mClientNumber = value; } }
        [DataMember]
        public string ShipperNumber { get { return this.mShipperNumber; } set { this.mShipperNumber = value; } }
        [DataMember]
        public int ConsigneeNumber { get { return this.mConsigneeNumber; } set { this.mConsigneeNumber = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class ServiceLocations : BindingList<ServiceLocation> { public ServiceLocations() { } }
}
