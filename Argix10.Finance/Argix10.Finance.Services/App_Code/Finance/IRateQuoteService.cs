using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Argix.RateWare;

namespace Argix.Finance {
    //Finance Interfaces
    [ServiceContract(Namespace="http://Argix.Finance", SessionMode=SessionMode.Allowed)]
    public interface IRateQuoteService {
        //General Interface
        [OperationContract]
        ServiceInfo GetServiceInfo();

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action="http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);
        
        [OperationContract(IsOneWay=true)]
        void WriteLogEntry(TraceMessage m);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        DataModule[] GetAvailableTariffs();

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        DataSet GetClassCodes();

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        Rates CalculateRates(DataModule tariff,string originZip,string classCode,string discount,string floorMin,string[] destinationZips);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault), Action = "http://Argix.Finance.RateQuoteFault")]
        LTLRateShipmentSimpleResponse CalculateLTLSimpleRate(LTLRateShipmentSimpleRequest request);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault), Action = "http://Argix.Finance.RateQuoteFault")]
        LTLRateShipmentResponse CalculateLTLRate(LTLRateShipmentRequest request);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        LTLRateShipmentResponse[] CalculateLTLRates(LTLRateShipmentRequest[] requests);
        
        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        LTLPointListResponse GetLTLPointList(LTLPointListRequest request);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        DensityRateShipmentResponse[] CalculateDensityRates(DensityRateShipmentRequest[] requests);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        LinearRateShipmentResponse[] CalculateLinearRates(LinearRateShipmentRequest[] requests);

        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault),Action = "http://Argix.Finance.RateQuoteFault")]
        Decimal GetMileage(string originPostalCode,string destinationPostalCode);


        //Just messing around
        [OperationContract]
        [FaultContractAttribute(typeof(RateQuoteFault), Action = "http://Argix.Finance.RateQuoteFault")]
        DataSet ViewDeliveryZips();

    }

    [CollectionDataContract]
    public class Rates:BindingList<Rate> { public Rates() { } }

    [DataContract]
    public class Rate {
        //Members
        private string mOrgZip = "",mDestZip = "",mMinCharge = "";
        private string mRate1 = "",mRate501 = "",mRate1001 = "",mRate2001 = "",mRate5001 = "",mRate10001 = "",mRate20001 = "";

        //Interface
        public Rate() : this(null) { }
        public Rate(RateQuoteDataset.RateTableRow rate) {
            //Constructor
            try {
                if (rate != null) {
                    if (!rate.IsOrgZipNull()) this.mOrgZip = rate.OrgZip;
                    if (!rate.IsDestZipNull()) this.mDestZip = rate.DestZip;
                    if (!rate.IsMinChargeNull()) this.mMinCharge = rate.MinCharge;
                    if (!rate.IsRate1Null()) this.mRate1 = rate.Rate1;
                    if (!rate.IsRate501Null()) this.mRate501 = rate.Rate501;
                    if (!rate.IsRate1001Null()) this.mRate1001 = rate.Rate1001;
                    if (!rate.IsRate2001Null()) this.mRate2001 = rate.Rate2001;
                    if (!rate.IsRate5001Null()) this.mRate5001 = rate.Rate5001;
                    if (!rate.IsRate10001Null()) this.mRate10001 = rate.Rate10001;
                    if (!rate.IsRate20001Null()) this.mRate20001 = rate.Rate20001;
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors/Modifiers [Members...]
        [DataMember]
        public string OrgZip { get { return this.mOrgZip; } set { this.mOrgZip = value; } }
        [DataMember]
        public string DestZip { get { return this.mDestZip; } set { this.mDestZip = value; } }
        [DataMember]
        public string MinCharge { get { return this.mMinCharge; } set { this.mMinCharge = value; } }
        [DataMember]
        public string Rate1 { get { return this.mRate1; } set { this.mRate1 = value; } }
        [DataMember]
        public string Rate501 { get { return this.mRate501; } set { this.mRate501 = value; } }
        [DataMember]
        public string Rate1001 { get { return this.mRate1001; } set { this.mRate1001 = value; } }
        [DataMember]
        public string Rate2001 { get { return this.mRate2001; } set { this.mRate2001 = value; } }
        [DataMember]
        public string Rate5001 { get { return this.mRate5001; } set { this.mRate5001 = value; } }
        [DataMember]
        public string Rate10001 { get { return this.mRate10001; } set { this.mRate10001 = value; } }
        [DataMember]
        public string Rate20001 { get { return this.mRate20001; } set { this.mRate20001 = value; } }
        #endregion
    }

    [DataContract]
    public class RateQuoteFault {
        private string mMessage="";
        public RateQuoteFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}

