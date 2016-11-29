using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;

namespace Argix {
    // 
    [ServiceContract(Namespace = "http://Argix")]
    public interface ITrackingService {

        [OperationContract]
        DataSet GetCustomers();
        
        [OperationContract]
        DataSet GetClients(string vendorID);
        
        [OperationContract]
        DataSet GetStoresForSubStore(string subStoreNumber,string clientNumber,string vendorNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        DataSet TrackCartonsForStoreByPickupDate(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        DataSet TrackCartonsForStoreByDeliveryDate(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsByCartonNumber(string[] itemNumbers,string clientNumber,string vendorNumber);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsByLabelNumber(string[] itemNumbers,string clientNumber,string vendorNumber);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsByPlateNumber(string[] itemNumbers,string clientNumber,string vendorNumber);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsForPO(string clientNumber,string PONumber);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsForPRO(string clientNumber,string shipmentNumber);
        
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingItems TrackCartonsForBOL(string clientNumber,string BOLNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingStoreItems TrackCartonsForStoreSummary(string clientNumber,string storeNumber,DateTime fromDate,DateTime toDate,string vendorNumber,string by);

        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingStoreItems TrackCartonsForStoreDetail(string clientNumber,string storeNumber,DateTime fromDate,DateTime toDate,string vendorNumber,string by,string tlNumber);
    }

    [CollectionDataContract]
    public class TrackingItems:BindingList<TrackingItem> { }

    [DataContract]
    public class TrackingItem {
        //Members
        private string mItemNumber="", mCartonNumber="", mLabelNumber="";
        private string mDateTime="", mLocation="", mStatus="";
        private string mBOLNumber="", mTLNumber="", mPONumber="";
        private string mVendorKey="", mShipmentNumber="", mCBOL="";
        private string mClient="", mClientName="";
        private string mShipper = "", mSigner="";
        private string mVendor = "",mVendorName = "",mVendorCity = "",mVendorState = "",mVendorZip = "";
        private string mAgentName="", mAgentCity="", mAgentState="", mAgentZip="";
        private string mSubAgentName="", mSubAgentCity="", mSubAgentState="", mSubAgentZip="";
        private string mStoreNumber="", mStoreName="", mStoreAddress1="", mStoreAddress2="", mStoreCity="", mStoreState="", mStoreZip="";
        private string mPickupDate="";
        private string mCenterArrivalDate="", mCenterArrivalStatus="", mCenterArrivalLocation="";
        private string mCenterDepartureDate="", mCenterDepartureStatus="", mCenterDepartureLocation="";
        private string mAgentArrivalDate="", mAgentArrivalStatus="", mAgentArrivalLocation="";
        private string mStoreDeliveryDate="", mStoreDeliveryStatus="", mStoreDeliveryLocation="";
        private string mPODScanDate="", mPODScanStatus="", mPODScanLocation="";
        private string mTrackingNumber="";
        private int mIsManualEntry=0;
        private decimal mWeight=0, mScanType=0;
        private string mPVNO="";
        private string mErrorMessage="";
    
        //Interface
        public TrackingItem(string itemNumber): this(itemNumber, null) { }
        public TrackingItem(string itemNumber, TrackingDataset.TrackingTableRow carton) {
            //Constructor
            this.mItemNumber = itemNumber;
            this.mStatus = "Item Not Found";
            if(carton != null) {
                this.mCartonNumber = !carton.IsCTNNull() ? carton.CTN.Trim() : "";
                this.mLabelNumber = !carton.IsLBLNull() ? carton.LBL.ToString() : "";
                this.mBOLNumber = !carton.IsBLNull() ? carton.BL.ToString() : "";
                this.mTLNumber = !carton.IsTLNull() ? carton.TL.Trim() : "";
                this.mPONumber = !carton.IsPONull() ? carton.PO.Trim() : "";
                this.mVendorKey = !carton.IsVKNull() ? carton.VK.Trim() : "";
                this.mShipmentNumber = !carton.IsShipmentNumberNull() ? carton.ShipmentNumber.Trim() : "";
                this.mCBOL = !carton.IsCBOLNull() ? carton.CBOL : "";
                this.mClient = !carton.IsCLNull() ? carton.CL.Trim() : "";
                this.mClientName = !carton.IsCLNMNull() ? carton.CLNM.Trim() : "";
                this.mShipper = !carton.IsTNull() ? carton.T : "";
                this.mSigner = !carton.IsSignerNull() ? carton.Signer : "";
                this.mVendor = !carton.IsVNull() ? carton.V.Trim() : "";
                this.mVendorName = !carton.IsVNMNull() ? carton.VNM.Trim() : "";
                this.mVendorCity = !carton.IsVCTNull() ? carton.VCT.Trim() : "";
                this.mVendorState = !carton.IsVSTNull() ? carton.VST.Trim() : "";
                this.mVendorZip = !carton.IsVZNull() ? carton.VZ.ToString() : "";
                this.mAgentName = !carton.IsAGNMNull() ? carton.AGNM.Trim() : "";
                this.mAgentCity = !carton.IsAGCTNull() ? carton.AGCT.Trim() : "";
                this.mAgentState = !carton.IsAGSTNull() ? carton.AGST.Trim() : "";
                this.mAgentZip = !carton.IsAGZNull() ? carton.AGZ.ToString() : "";
                this.mSubAgentName = !carton.IsSAGNMNull() ? carton.SAGNM.Trim() : "";
                this.mSubAgentCity = !carton.IsSAGCTNull() ? carton.SAGCT.Trim() : "";
                this.mSubAgentState = !carton.IsSAGSTNull() ? carton.SAGST.Trim() : "";
                this.mSubAgentZip = !carton.IsSAGZNull() ? carton.SAGZ.ToString() : "";
                this.mStoreNumber = !carton.IsSNull() ? carton.S.ToString() : "";
                this.mStoreName = !carton.IsSNMNull() ? carton.SNM.Trim() : "";
                this.mStoreAddress1 = !carton.IsSA1Null() ? carton.SA1.Trim() : "";
                this.mStoreAddress2 = !carton.IsSA2Null() ? carton.SA2.Trim() : "";
                this.mStoreCity = !carton.IsSCTNull() ? carton.SCT.Trim() : "";
                this.mStoreState = !carton.IsSSTNull() ? carton.SST.Trim() : "";
                this.mStoreZip = !carton.IsSZNull() ? carton.SZ.ToString() : "";
                if(!carton.IsPUDNull() && carton.PUD.Trim().Length > 0) this.mPickupDate = carton.PUD.Trim();

                //Sort facility
                this.mCenterArrivalDate = this.mCenterArrivalStatus = this.mCenterArrivalLocation = "";
                if(!carton.IsASFDNull() && carton.ASFD.Trim().Length > 0) {
                    this.mDateTime = this.mCenterArrivalDate = carton.ASFD.Trim() + " " + carton.ASFT.Trim();
                    this.mStatus = this.mCenterArrivalStatus = "Arrived At Sort Facility";
                    this.mLocation = this.mCenterArrivalLocation = !carton.IsSRTLOCNull() ? carton.SRTLOC.Trim() : "";
                }
                this.mCenterDepartureDate = this.mCenterDepartureStatus = this.mCenterDepartureLocation = "";
                if(!carton.IsADPDNull() && carton.ADPD.Trim().Length > 0) {
                    this.mDateTime = this.mCenterDepartureDate = carton.ADPD.Trim() + " " + carton.ADPT.Trim();
                    this.mStatus = this.mCenterDepartureStatus = "Departed Sort Facility";
                    this.mLocation = this.mCenterDepartureLocation = !carton.IsSRTLOCNull() ? carton.SRTLOC.Trim() : "";
                }

                //Delivery terminal 
                //1. BOL confirmed (trailer arrived into AS400): SCNTP=0, AARD!=null; 
                //2. Agent scan: SCNTP=1, AARD!=null, OM=Over(O)||Short(S)||MisRoute(A)||Match(M)
                this.mAgentArrivalDate = this.mAgentArrivalStatus = this.mAgentArrivalLocation = "";
                if(!carton.IsAARDNull() && carton.AARD.Trim().Length > 0) {
                    this.mDateTime = this.mAgentArrivalDate = carton.AARD.Trim() + " " + carton.AART.Trim();
                    if(carton.SCNTP == 1) {
                        switch(carton.OM) {
                            case "M": this.mStatus = this.mAgentArrivalStatus = "Scanned At Delivery Terminal"; break;
                            case "S": this.mStatus = this.mAgentArrivalStatus = "Short At Delivery Terminal"; break;
                            case "O": this.mStatus = this.mAgentArrivalStatus = "Over At Delivery Terminal"; break;
                            case "A": this.mStatus = this.mAgentArrivalStatus = "MisRoute At Delivery Terminal"; break;
                        }
                    }
                    else
                        this.mStatus = this.mAgentArrivalStatus = "Arrived At Delivery Terminal";
                    if(!carton.IsSAGCTNull() && carton.SAGCT.Trim().Length > 0)
                        this.mLocation = this.mAgentArrivalLocation = carton.SAGCT.Trim() + "/" + carton.SAGST.Trim();
                    else
                        this.mLocation = this.mAgentArrivalLocation = !carton.IsAGCTNull() ? carton.AGCT.Trim() + "/" + carton.AGST.Trim() : "";
                }

                //Store delivery
                this.mStoreDeliveryDate = this.mStoreDeliveryStatus = this.mStoreDeliveryLocation;
                if(carton.SCNTP == 3 && !carton.IsACTSDDNull() && carton.ACTSDD.Trim().Length > 0) {
                    this.mDateTime = this.mStoreDeliveryDate = carton.ACTSDD.Trim();
                    this.mStatus = this.mStoreDeliveryStatus = "Out For Delivery";
                    this.mLocation = this.mStoreDeliveryLocation = !carton.IsSCTNull() ? carton.SCT.Trim() + "/" + carton.SST.Trim() : "";
                }

                //POD
                this.mPODScanDate = this.mPODScanStatus = this.mPODScanLocation = "";
                if(carton.SCNTP == 3 && !carton.IsSCDNull() && carton.SCD.Trim().Length > 0) {
                    this.mDateTime = this.mPODScanDate = carton.SCD.Trim() + " " + carton.SCTM.Trim();
                    //Check for mis-routed carton- podScan is estimated by UPS (or other agent)
                    if(carton.T.Trim().Length == 18 && carton.T.Trim().Substring(0,2).ToLower() == "1z")
                        this.mStatus = this.mPODScanStatus = "Rerouted: Tracking # " + carton.T.Trim();
                    else {
                        switch(carton.OM) {
                            case "M": this.mStatus = this.mPODScanStatus = carton.ISMN == 1 ? "Delivered (Scan N/A - Manual Entry)" : "Delivered"; break;
                            case "S": this.mStatus = this.mPODScanStatus = "Short At Delivery"; break;
                            case "O": this.mStatus = this.mPODScanStatus = "Over At Delivery"; break;
                            case "A": this.mStatus = this.mPODScanStatus = "MisRoute At Delivery"; break;
                        }
                    }
                    this.mLocation = this.mPODScanLocation = carton.IsSCTNull() ? "" : carton.SCT.Trim() + "/" + carton.SST.Trim();
                }

                this.mTrackingNumber = !carton.IsTNull() ? carton.T : "";
                this.mIsManualEntry = !carton.IsISMNNull() ? carton.ISMN : 0;
                this.mWeight = carton.WT;
                this.mScanType = !carton.IsSCNTPNull() ? carton.SCNTP : 0m;
                this.mPVNO = !carton.IsPVNONull() ? carton.PVNO : "";
            }
        }
        #region Members [...]
        [DataMember]
        public string ItemNumber { get { return this.mItemNumber; } set { this.mItemNumber = value; } }
        [DataMember]
        public string CartonNumber { get { return this.mCartonNumber; } set { this.mCartonNumber = value; } }
        [DataMember]
        public string LabelNumber { get { return this.mLabelNumber; } set { this.mLabelNumber = value; } }
        [DataMember]
        public string DateTime { get { return this.mDateTime; } set { this.mDateTime = value; } }
        [DataMember]
        public string Location { get { return this.mLocation; } set { this.mLocation = value; } }
        [DataMember]
        public string Status { get { return this.mStatus; } set { this.mStatus = value; } }
        [DataMember]
        public string BOLNumber { get { return this.mBOLNumber; } set { this.mBOLNumber = value; } }
        [DataMember]
        public string TLNumber { get { return this.mTLNumber; } set { this.mTLNumber = value; } }
        [DataMember]
        public string PONumber { get { return this.mPONumber; } set { this.mPONumber = value; } }
        [DataMember]
        public string VendorKey { get { return this.mVendorKey; } set { this.mVendorKey = value; } }
        [DataMember]
        public string ShipmentNumber { get { return this.mShipmentNumber; } set { this.mShipmentNumber = value; } }
        [DataMember]
        public string CBOL { get { return this.mCBOL; } set { this.mCBOL = value; } }
        [DataMember]
        public string Client { get { return this.mClient; } set { this.mClient = value; } }
        [DataMember]
        public string ClientName { get { return this.mClientName; } set { this.mClientName = value; } }
        [DataMember]
        public string Shipper { get { return this.mShipper; } set { this.mShipper = value; } }
        [DataMember]
        public string Signer { get { return this.mSigner; } set { this.mSigner = value; } }
        [DataMember]
        public string Vendor { get { return this.mVendor; } set { this.mVendor = value; } }
        [DataMember]
        public string VendorName { get { return this.mVendorName; } set { this.mVendorName = value; } }
        [DataMember]
        public string VendorCity { get { return this.mVendorCity; } set { this.mVendorCity = value; } }
        [DataMember]
        public string VendorState { get { return this.mVendorState; } set { this.mVendorState = value; } }
        [DataMember]
        public string VendorZip { get { return this.mVendorZip; } set { this.mVendorZip = value; } }
        [DataMember]
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
        [DataMember]
        public string AgentCity { get { return this.mAgentCity; } set { this.mAgentCity = value; } }
        [DataMember]
        public string AgentState { get { return this.mAgentState; } set { this.mAgentState = value; } }
        [DataMember]
        public string AgentZip { get { return this.mAgentZip; } set { this.mAgentZip = value; } }
        [DataMember(EmitDefaultValue=false)]
        public string SubAgentName { get { return this.mSubAgentName; } set { this.mSubAgentName = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubAgentCity { get { return this.mSubAgentCity; } set { this.mSubAgentCity = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubAgentState { get { return this.mSubAgentState; } set { this.mSubAgentState = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubAgentZip { get { return this.mSubAgentZip; } set { this.mSubAgentZip = value; } }
        [DataMember]
        public string StoreNumber { get { return this.mStoreNumber; } set { this.mStoreNumber = value; } }
        [DataMember]
        public string StoreName { get { return this.mStoreName; } set { this.mStoreName = value; } }
        [DataMember]
        public string StoreAddress1 { get { return this.mStoreAddress1; } set { this.mStoreAddress1 = value; } }
        [DataMember]
        public string StoreAddress2 { get { return this.mStoreAddress2; } set { this.mStoreAddress2 = value; } }
        [DataMember]
        public string StoreCity { get { return this.mStoreCity; } set { this.mStoreCity = value; } }
        [DataMember]
        public string StoreState { get { return this.mStoreState; } set { this.mStoreState = value; } }
        [DataMember]
        public string StoreZip { get { return this.mStoreZip; } set { this.mStoreZip = value; } }
        [DataMember]
        public string PickupDate { get { return this.mPickupDate; } set { this.mPickupDate = value; } }
        [DataMember]
        public string SortFacilityArrivalDate { get { return this.mCenterArrivalDate; } set { this.mCenterArrivalDate = value; } }
        [DataMember]
        public string SortFacilityArrivalStatus { get { return this.mCenterArrivalStatus; } set { this.mCenterArrivalStatus = value; } }
        [DataMember]
        public string SortFacilityLocation { get { return this.mCenterArrivalLocation; } set { this.mCenterArrivalLocation = value; } }
        [DataMember]
        public string ActualDepartureDate { get { return this.mCenterDepartureDate; } set { this.mCenterDepartureDate = value; } }
        [DataMember]
        public string ActualDepartureStatus { get { return this.mCenterDepartureStatus; } set { this.mCenterDepartureStatus = value; } }
        [DataMember]
        public string ActualDepartureLocation { get { return this.mCenterDepartureLocation; } set { this.mCenterDepartureLocation = value; } }
        [DataMember]
        public string ActualArrivalDate { get { return this.mAgentArrivalDate; } set { this.mAgentArrivalDate = value; } }
        [DataMember]
        public string ActualArrivalStatus { get { return this.mAgentArrivalStatus; } set { this.mAgentArrivalStatus = value; } }
        [DataMember]
        public string ActualArrivalLocation { get { return this.mAgentArrivalLocation; } set { this.mAgentArrivalLocation = value; } }
        [DataMember]
        public string ActualStoreDeliveryDate { get { return this.mStoreDeliveryDate; } set { this.mStoreDeliveryDate = value; } }
        [DataMember]
        public string ActualStoreDeliveryStatus { get { return this.mStoreDeliveryStatus; } set { this.mStoreDeliveryStatus = value; } }
        [DataMember]
        public string ActualStoreDeliveryLocation { get { return this.mStoreDeliveryLocation; } set { this.mStoreDeliveryLocation = value; } }
        [DataMember]
        public string PODScanDate { get { return this.mPODScanDate; } set { this.mPODScanDate = value; } }
        [DataMember]
        public string PODScanStatus { get { return this.mPODScanStatus; } set { this.mPODScanStatus = value; } }
        [DataMember]
        public string PODScanLocation { get { return this.mPODScanLocation; } set { this.mPODScanLocation = value; } }
        [DataMember]
        public string TrackingNumber { get { return this.mTrackingNumber; } set { this.mTrackingNumber = value; } }
        [DataMember]
        public int IsManualEntry { get { return this.mIsManualEntry; } set { this.mIsManualEntry = value; } }
        [DataMember]
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember]
        public decimal ScanType { get { return this.mScanType; } set { this.mScanType = value; } }
        [DataMember]
        public string PVNO { get { return this.mPVNO; } set { this.mPVNO = value; } }
        [DataMember]
        public string ErrorMessage { get { return this.mErrorMessage; } set { this.mErrorMessage = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class TrackingStoreItems:BindingList<TrackingStoreItem> { }

    [DataContract]
    public class TrackingStoreItem {
        //Members        
        private string mStore="", mTL = "";
        private int mCartonCount=0;
        private string mCBOL="";
        private DateTime mOFD1;
        private string mTerminal="";
        private string mCartonNumber = "";
        private decimal mLabelNumber = 0;
        private DateTime mPickupDate;
        private decimal mWeight=0;
        private string mShipperName="", mShipperCity = "", mShipperState="", mShipperZip="";
        private string mCartonStatus="", mScanStatus="";
        private DateTime mPODDate,mPODTime;
        private string mAgentNumber="", mAgentName="";
        private string mTransfer="";

        //Interface
        public TrackingStoreItem() : this(null) { }
        public TrackingStoreItem(TrackingDataset.CartonDetailForStoreTableRow carton) {
            //Constructor
            if (carton != null) {
                this.mStore = !carton.IsSNull() ? carton.S.ToString() : "";
                this.mTL = !carton.IsTLNull() ? carton.TL.Trim() : "";
                this.mCartonCount = !carton.IsCartonCountNull() ? carton.CartonCount : 0;
                this.mCBOL = !carton.IsCBOLNull() ? carton.CBOL.Trim() : "";
                this.mOFD1 = !carton.IsOFD1Null() ? carton.OFD1 : DateTime.MinValue;
                this.mTerminal = !carton.IsSrtLocNull() ? carton.SrtLoc.Trim() : "";
                this.mCartonNumber = (!carton.IsCtnNull() && carton.Ctn.Trim().Length > 0) ? carton.Ctn.Trim() : "No Carton Number";
                this.mLabelNumber = !carton.IsLblNull() ? carton.Lbl : 0;
                this.mPickupDate = !carton.IsPuDNull() ? DateTime.Parse(carton.PuD) : DateTime.MinValue;
                this.mWeight = !carton.IsWtNull() ? carton.Wt : 0;;
                this.mShipperName = !carton.IsVNmNull() ? carton.VNm.Trim() : "";
                this.mShipperCity = !carton.IsVCtNull() ? carton.VCt.Trim() : "";
                this.mShipperState = !carton.IsVStNull() ? carton.VSt.Trim() : "";
                this.mShipperZip = !carton.IsVZNull() ? carton.VZ.ToString() : "";
                this.mCartonStatus = !carton.IsOMNull() ? carton.OM.Trim() : "";
                this.mScanStatus = "";  // !carton.IsScnStsNull() ? carton.ScnSts.Trim() : "";
                this.mPODDate = !carton.IsScDNull() ? DateTime.Parse(carton.ScD) : DateTime.MinValue;
                this.mPODTime = !carton.IsScTmNull() ? DateTime.Parse(carton.ScTm) : DateTime.MinValue;
                this.mAgentNumber = !carton.IsAgNull() ? carton.Ag.Trim() : "";
                this.mAgentName = !carton.IsAgNmNull() ? carton.AgNm.Trim() : "";
                this.mTransfer = "";    // !carton.IsTrfNull() ? carton.Trf.Trim() : "";
            }
        }
        #region Members [...]
        [DataMember]
        public string Store { get { return this.mStore; } set { this.mStore = value; } }
        [DataMember]
        public string TL { get { return this.mTL; } set { this.mTL = value; } }
        [DataMember]
        public int CartonCount { get { return this.mCartonCount; } set { this.mCartonCount = value; } }
        [DataMember]
        public string CBOL { get { return this.mCBOL; } set { this.mCBOL = value; } }
        [DataMember]
        public DateTime OFD1 { get { return this.mOFD1; } set { this.mOFD1 = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Terminal { get { return this.mTerminal; } set { this.mTerminal = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string CartonNumber { get { return this.mCartonNumber; } set { this.mCartonNumber = value; } }
        [DataMember(EmitDefaultValue = false)]
        public decimal LabelNumber { get { return this.mLabelNumber; } set { this.mLabelNumber = value; } }
        [DataMember(EmitDefaultValue = false)]
        public DateTime PickupDate { get { return this.mPickupDate; } set { this.mPickupDate = value; } }
        [DataMember]
        public decimal Weight { get { return this.mWeight; } set { this.mWeight = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string ShipperName { get { return this.mShipperName; } set { this.mShipperName = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string ShipperCity { get { return this.mShipperCity; } set { this.mShipperCity = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string ShipperState { get { return this.mShipperState; } set { this.mShipperState = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string ShipperZip { get { return this.mShipperZip; } set { this.mShipperZip = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string CartonStatus { get { return this.mCartonStatus; } set { this.mCartonStatus = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string ScanStatus { get { return this.mScanStatus; } set { this.mScanStatus = value; } }
        [DataMember]
        public DateTime PODDate { get { return this.mPODDate; } set { this.mPODDate = value; } }
        [DataMember]
        public DateTime PODTime { get { return this.mPODTime; } set { this.mPODTime = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        [DataMember]
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Transfer { get { return this.mTransfer; } set { this.mTransfer = value; } }
        #endregion
    }

    [DataContract]
    public class TrackingFault {
        private string mMessage="";
        public TrackingFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
