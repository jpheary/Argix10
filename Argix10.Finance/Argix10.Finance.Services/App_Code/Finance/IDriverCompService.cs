using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Finance {
    //Finance Interfaces
    [ServiceContract(Namespace="http://Argix.Finance")]
    public interface IDriverCompService {
        //General Interface
        [OperationContract] [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();
        [OperationContract] [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);


        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        DataSet ReadRoadshowRoutes(string agentNumber,DateTime startDate,DateTime endDate);
        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        DataSet ReadDriverRoutes(string agentNumber,DateTime startDate,DateTime endDate);
        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        bool CreateDriverRoute(DriverRoute route);
        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        bool UpdateDriverRoute(DriverRoute route);
        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        bool DeleteDriverRoute(long routeID);

        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        DataSet ReadTerminalConfigurations(string agentNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        TerminalConfiguration GetTerminalConfiguration(string agentNumber);
        [OperationContract] 
        [FaultContractAttribute(typeof(DriverCompensationFault),Action="http://Argix.Finance.DriverCompensationFault")]
        DataSet ReadDriverEquipment(string financeVendorID,string operatorName);
        [OperationContract] 
        [FaultContractAttribute(typeof(DriverCompensationFault),Action="http://Argix.Finance.DriverCompensationFault")]
        bool CreateDriverEquipment(string financeVendorID,string operatorName,int equipmentID);
        [OperationContract] 
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        bool UpdateDriverEquipment(string financeVendorID,string operatorName,int equipmentID);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet ReadVehicleMileageRates(DateTime date,string terminalAgent,int equipmentTypeID);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool CreateVehicleMileageRate(string agentNumber,int equipmentID,DateTime effectiveDate,double mile,decimal baseRate,decimal rate);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool UpdateVehicleMileageRate(int id,int equipmentID,double mile,decimal baseRate,decimal rate);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet ReadVehicleUnitRates(DateTime date,string terminalAgent,int equipmentTypeID);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool CreateVehicleUnitRate(string agentNumber,int equipmentID,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool UpdateVehicleUnitRate(int id,int equipmentID,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet ReadRouteMileageRates(DateTime date,string terminalAgent,string route);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool CreateRouteMileageRate(string agentNumber,string route,DateTime effectiveDate,double mile,decimal baseRate,decimal rate,int status);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool UpdateRouteMileageRate(int id,string route,double mile,decimal baseRate,decimal rate,int status);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet ReadRouteUnitRates(DateTime date,string terminalAgent,string route);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool CreateRouteUnitRate(string agentNumber,string route,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status);
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        bool UpdateRouteUnitRate(int id,string route,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status);

        [OperationContract] [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet GetLocalTerminals();
        [OperationContract] [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        DataSet GetAdjustmentTypes();
        [OperationContract] [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet GetDriverEquipmentTypes();
        [OperationContract] [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        DataSet GetRateTypes();
        [OperationContract] [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.DriverRatingFault")]
        PayPeriod GetPayPeriod(DateTime date);
        [OperationContract]
        [FaultContractAttribute(typeof(DriverCompensationFault),Action = "http://Argix.Finance.DriverCompensationFault")]
        decimal GetFuelCost(DateTime date,string agentNumber);
    }

    [DataContract]
    public class DriverRoute {
        //Members
        private long mID=0;
        private bool mIsNew=false,mIsCombo=false, mIsAdjust=false;
        private int mRouteIndex = 0;
        private DateTime mRouteDate;
        private string mRouteName = "";
        private string mAgentNumber = "";
        private string mOperator="";
        private string mPayee="";
        private string mFinanceVendorID = "";
        private int mEquipmentTypeID = 0;
        private int mRateTypeID=0;
        private decimal mDayRate=0M;
        private decimal mDayAmount=0M;
        private decimal mMiles=0M;
        private decimal mMilesBaseRate=0M;
        private decimal mMilesRate=0M;
        private decimal mMilesAmount=0M;
        private int mTrip=0;
        private decimal mTripRate=0M;
        private decimal mTripAmount=0M;
        private int mStops=0;
        private decimal mStopsRate=0M;
        private decimal mStopsAmount=0M;
        private int mCartons=0;
        private decimal mCartonsRate=0M;
        private decimal mCartonsAmount=0M;
        private int mPallets=0;
        private decimal mPalletsRate=0M;
        private decimal mPalletsAmount=0M;
        private int mPickupCartons=0;
        private decimal mPickupCartonsRate=0M;
        private decimal mPickupCartonsAmount=0M;
        private decimal mMinimunAmount=0M;
        private decimal mFSCMiles = 0M;
        private decimal mFuelCost = 0M;
        private decimal mFSCGal=0M;
        private decimal mFSCBaseRate=0M;
        private decimal mFSC=0M;
        private decimal mAdjustmentAmount1=0M;
        private string mAdjustmentAmount1TypeID="";
        private decimal mAdjustmentAmount2=0M;
        private string mAdjustmentAmount2TypeID="";
        private decimal mAdminCharge=0M;
        private decimal mTotalAmount=0M;
        private DateTime mImported;
        private DateTime mExported;
        private string mArgixRtType="";
        private DateTime mLastUpdated;
        private string mUserID = "";

        //Interface
        public DriverRoute() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public long ID { get { return this.mID; } set { this.mID = value; } }
        [DataMember]
        public bool IsNew { get { return this.mIsNew; } set { this.mIsNew = value; } }
        [DataMember]
        public bool IsCombo { get { return this.mIsCombo; } set { this.mIsCombo = value; } }
        [DataMember]
        public bool IsAdjust { get { return this.mIsAdjust; } set { this.mIsAdjust = value; } }
        [DataMember]
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber = value; } }
        [DataMember]
        public string FinanceVendorID { get { return this.mFinanceVendorID; } set { this.mFinanceVendorID = value; } }
        [DataMember]
        public int RouteIndex { get { return this.mRouteIndex; } set { this.mRouteIndex = value; } }
        [DataMember]
        public DateTime RouteDate { get { return this.mRouteDate; } set { this.mRouteDate = value; } }
        [DataMember]
        public string RouteName { get { return this.mRouteName; } set { this.mRouteName = value; } }
        [DataMember]
        public string Operator { get { return this.mOperator; } set { this.mOperator = value; } }
        [DataMember]
        public string Payee { get { return this.mPayee; } set { this.mPayee = value; } }
        [DataMember]
        public int EquipmentTypeID { get { return this.mEquipmentTypeID; } set { this.mEquipmentTypeID = value; } }
        [DataMember]
        public int RateTypeID { get { return this.mRateTypeID; } set { this.mRateTypeID = value; } }
        [DataMember]
        public decimal DayRate { get { return this.mDayRate; } set { this.mDayRate = value; } }
        [DataMember]
        public decimal DayAmount { get { return this.mDayAmount; } set { this.mDayAmount = value; } }
        [DataMember]
        public decimal Miles { get { return this.mMiles; } set { this.mMiles = value; } }
        [DataMember]
        public decimal MilesBaseRate { get { return this.mMilesBaseRate; } set { this.mMilesBaseRate = value; } }
        [DataMember]
        public decimal MilesRate { get { return this.mMilesRate; } set { this.mMilesRate = value; } }
        [DataMember]
        public decimal MilesAmount { get { return this.mMilesAmount; } set { this.mMilesAmount = value; } }
        [DataMember]
        public int Trip { get { return this.mTrip; } set { this.mTrip = value; } }
        [DataMember]
        public decimal TripRate { get { return this.mTripRate; } set { this.mTripRate = value; } }
        [DataMember]
        public decimal TripAmount { get { return this.mTripAmount; } set { this.mTripAmount = value; } }
        [DataMember]
        public int Stops { get { return this.mStops; } set { this.mStops = value; } }
        [DataMember]
        public decimal StopsRate { get { return this.mStopsRate; } set { this.mStopsRate = value; } }
        [DataMember]
        public decimal StopsAmount { get { return this.mStopsAmount; } set { this.mStopsAmount = value; } }
        [DataMember]
        public int Cartons { get { return this.mCartons; } set { this.mCartons = value; } }
        [DataMember]
        public decimal CartonsRate { get { return this.mCartonsRate; } set { this.mCartonsRate = value; } }
        [DataMember]
        public decimal CartonsAmount { get { return this.mCartonsAmount; } set { this.mCartonsAmount = value; } }
        [DataMember]
        public int Pallets { get { return this.mPallets; } set { this.mPallets = value; } }
        [DataMember]
        public decimal PalletsRate { get { return this.mPalletsRate; } set { this.mPalletsRate = value; } }
        [DataMember]
        public decimal PalletsAmount { get { return this.mPalletsAmount; } set { this.mPalletsAmount = value; } }
        [DataMember]
        public int PickupCartons { get { return this.mPickupCartons; } set { this.mPickupCartons = value; } }
        [DataMember]
        public decimal PickupCartonsRate { get { return this.mPickupCartonsRate; } set { this.mPickupCartonsRate = value; } }
        [DataMember]
        public decimal PickupCartonsAmount { get { return this.mPickupCartonsAmount; } set { this.mPickupCartonsAmount = value; } }
        [DataMember]
        public decimal MinimunAmount { get { return this.mMinimunAmount; } set { this.mMinimunAmount = value; } }
        [DataMember]
        public decimal FSCMiles { get { return this.mFSCMiles; } set { this.mFSCMiles = value; } }
        [DataMember]
        public decimal FuelCost { get { return this.mFuelCost; } set { this.mFuelCost = value; } }
        [DataMember]
        public decimal FSCGal { get { return this.mFSCGal; } set { this.mFSCGal = value; } }
        [DataMember]
        public decimal FSCBaseRate { get { return this.mFSCBaseRate; } set { this.mFSCBaseRate = value; } }
        [DataMember]
        public decimal FSC { get { return this.mFSC; } set { this.mFSC = value; } }
        [DataMember]
        public decimal AdjustmentAmount1 { get { return this.mAdjustmentAmount1; } set { this.mAdjustmentAmount1 = value; } }
        [DataMember]
        public string AdjustmentAmount1TypeID { get { return this.mAdjustmentAmount1TypeID; } set { this.mAdjustmentAmount1TypeID = value; } }
        [DataMember]
        public decimal AdjustmentAmount2 { get { return this.mAdjustmentAmount2; } set { this.mAdjustmentAmount2 = value; } }
        [DataMember]
        public string AdjustmentAmount2TypeID { get { return this.mAdjustmentAmount2TypeID; } set { this.mAdjustmentAmount2TypeID = value; } }
        [DataMember]
        public decimal AdminCharge { get { return this.mAdminCharge; } set { this.mAdminCharge = value; } }
        [DataMember]
        public decimal TotalAmount { get { return this.mTotalAmount; } set { this.mTotalAmount = value; } }
        [DataMember]
        public DateTime Imported { get { return this.mImported; } set { this.mImported = value; } }
        [DataMember]
        public DateTime Exported { get { return this.mExported; } set { this.mExported = value; } }
        [DataMember]
        public string ArgixRtType { get { return this.mArgixRtType; } set { this.mArgixRtType = value; } }
        [DataMember]
        public DateTime LastUpdated { get { return this.mLastUpdated; } set { this.mLastUpdated = value; } }
        [DataMember]
        public string UserID { get { return this.mUserID; } set { this.mUserID = value; } }
        #endregion
    }

    [DataContract]
    public struct PayPeriod {
        [DataMember]
        public string Month;
        [DataMember]
        public string Year;
    }

    [DataContract]
    public class TerminalConfiguration {
        //Members
        private string mAgentNumber = "", mAgentName = "";
        private string mGLNumber = "", mAdminGLNumber = "";
        private decimal mAdminFee = 0.0M, mFSBase = 0.0M, mBonusRate = 0.0M;

        //Interface
        public TerminalConfiguration() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember] 
        public string AgentNumber { get { return this.mAgentNumber; } set { this.mAgentNumber=value; } }
        [DataMember]
        public string AgentName { get { return this.mAgentName; } set { this.mAgentName = value; } }
        [DataMember]
        public string GLNumber { get { return this.mGLNumber; } set { this.mGLNumber = value; } }
        [DataMember]
        public string AdminGLNumber { get { return this.mAdminGLNumber; } set { this.mAdminGLNumber = value; } }
        [DataMember]
        public decimal AdminFee { get { return this.mAdminFee; } set { this.mAdminFee = value; } }
        [DataMember]
        public decimal FSBase { get { return this.mFSBase; } set { this.mFSBase = value; } }
        [DataMember]
        public decimal BonusRate { get { return this.mBonusRate; } set { this.mBonusRate = value; } }
        #endregion
    }

    //Ratings structure with ratings for a single route (i.e. date, agent, route, & equipID)
    [DataContract]
    public class RouteRatings {
        //Members
        private DateTime mRatesDate = DateTime.Today;
        private int mRateTypeID = RATETYPE_NONE;
        private decimal mMileBaseRate = 0.0M,mMileRate = 0.0M;
        private decimal mDayRate = 0.0M,mMultiTripRate = 0.0M,mStopRate = 0.0M;
        private decimal mCartonRate = 0.0M,mPalletRate = 0.0M,mPickupCartonRate = 0.0M;
        private decimal mMinimumAmount = 0.0M,mMaximumAmount = 0.0M;
        private string mMaximumTriggerField = "";
        private int mMaximumTriggerValue = 0;
        private decimal mFSBase = 0.0M;

        public const int RATETYPE_NONE = 0,RATETYPE_VEHICLE = 1,RATETYPE_ROUTE = 2;

        //Interface
        public RouteRatings() { }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int RateTypeID { get { return this.mRateTypeID; } set { this.mRateTypeID = value; } }
        [DataMember]
        public decimal MileBaseRate { get { return this.mMileBaseRate; } set { this.mMileBaseRate = value; } }
        [DataMember]
        public decimal MileRate { get { return this.mMileRate; } set { this.mMileRate = value; } }
        [DataMember]
        public decimal DayRate { get { return this.mDayRate; } set { this.mDayRate = value; } }
        [DataMember]
        public decimal TripRate { get { return this.mMultiTripRate; } set { this.mMultiTripRate = value; } }
        [DataMember]
        public decimal StopRate { get { return this.mStopRate; } set { this.mStopRate = value; } }
        [DataMember]
        public decimal CartonRate { get { return this.mCartonRate; } set { this.mCartonRate = value; } }
        [DataMember]
        public decimal PalletRate { get { return this.mPalletRate; } set { this.mPalletRate = value; } }
        [DataMember]
        public decimal PickupCartonRate { get { return this.mPickupCartonRate; } set { this.mPickupCartonRate = value; } }
        [DataMember]
        public decimal MinimumAmount { get { return this.mMinimumAmount; } set { this.mMinimumAmount = value; } }
        [DataMember]
        public decimal MaximumAmount { get { return this.mMaximumAmount; } set { this.mMaximumAmount = value; } }
        [DataMember]
        public string MaximumTriggerField { get { return this.mMaximumTriggerField; } set { this.mMaximumTriggerField = value; } }
        [DataMember]
        public int MaximumTriggerValue { get { return this.mMaximumTriggerValue; } set { this.mMaximumTriggerValue = value; } }
        [DataMember]
        public decimal FSBase { get { return this.mFSBase; } set { this.mFSBase = value; } }
        #endregion
    }

    [DataContract]
    public class DriverCompensationFault {
        private string mMessage = "";
        public DriverCompensationFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }

    [DataContract]
    public class DriverRatingFault {
        private string mMessage = "";
        public DriverRatingFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
