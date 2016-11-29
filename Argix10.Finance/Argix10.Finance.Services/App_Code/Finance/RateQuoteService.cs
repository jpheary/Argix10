using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Argix.Enterprise;
using Argix.RateWare;

namespace Argix.Finance {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class RateQuoteService:IRateQuoteService {
        //Members
        private const string SQL_CONNID = "RateQuote";
        private const string CLSCODE_XMLFILE = "~\\App_Data\\ClassCodes.xml";

        //Interface
        public RateQuoteService() { }
        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            return new AppService(SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataModule[] GetAvailableTariffs() {
            //Get available tariffs
            DataModule[] tariffs = null;
            try {
                tariffs = new RateWareGateway().GetAvailableTariffs();
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return tariffs;
        }
        public DataSet GetClassCodes() {
            //Get class codes
            DataSet codes = new DataSet();
            try {
                FileInfo fi = new FileInfo(System.Web.Hosting.HostingEnvironment.MapPath(CLSCODE_XMLFILE));
                if (fi.Exists) codes.ReadXml(System.Web.Hosting.HostingEnvironment.MapPath(CLSCODE_XMLFILE));
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return codes;
        }
        public Rates CalculateRates(DataModule tariff,string originZip,string classCode,string discount,string floorMin,string[] destinationZips) {
            //Calcualate rates
            string[] WEIGHT_RANGES = { "1","501","1001","2001","5001","10001","20001" };
            Rates rates = new Rates();
            try {
                for (int i = 0;i < destinationZips.Length;i++) {
                    LTLRateShipmentRequest[] requests = new LTLRateShipmentRequest[WEIGHT_RANGES.Length];
                    for (int j = 0;j < WEIGHT_RANGES.Length;j++) {
                        LTLRateShipmentRequest request = new LTLRateShipmentRequest();
                        request.tariffName = tariff != null ? tariff.tariffName : "";
                        request.shipmentDateCCYYMMDD = tariff != null ? tariff.effectiveDate : "";
                        request.originPostalCode = originZip;
                        request.destinationPostalCode = destinationZips[i];
                        request.destinationCountry = request.originCountry = "USA";
                        request.useSingleShipmentCharges = "N";
                        request.rateAdjustmentFactor = "1.0000";
                        request.useDiscounts = "Y";
                        request.discountApplication = "R";          //Apply discount to R=Rates, C=Charges
                        request.mcDiscount = discount;
                        request.userMinimumChargeFloor = floorMin;
                        request.LTL_Surcharge = "0";
                        request.TL_Surcharge = "0";
                        request.surchargeApplication = "G";         //Apply surcharge to G=Gross, N=Net

                        LTLRequestDetail detail = new LTLRequestDetail();
                        detail.nmfcClass = classCode;
                        detail.weight = WEIGHT_RANGES[j].ToString();
                        request.details = new LTLRequestDetail[] { detail };
                        requests[j] = request;
                    }
                    LTLRateShipmentResponse[] responses = new RateWareGateway().CalculateLTLRates(requests);

                    ////Take lesser of min charge from rate ware or set floor min
                    //double minCharge = Convert.ToDouble(responses[0].minimumCharge);
                    //if (minCharge < floorMin) minCharge = floorMin;

                    //Add rates to the grid
                    RateQuoteDataset.RateTableRow _rate = new RateQuoteDataset().RateTable.NewRateTableRow();
                    _rate.OrgZip = responses[0].originPostalCode;
                    _rate.DestZip = responses[0].destinationPostalCode.Substring(0,3);
                    _rate.MinCharge = responses[0].minimumCharge;
                    _rate.Rate1 = responses[0].details[0].rate;
                    _rate.Rate501 = responses[1].details[0].rate;
                    _rate.Rate1001 = responses[2].details[0].rate;
                    _rate.Rate2001 = responses[3].details[0].rate;
                    _rate.Rate5001 = responses[4].details[0].rate;
                    _rate.Rate10001 = responses[5].details[0].rate;
                    _rate.Rate20001 = responses[6].details[0].rate;
                    rates.Add(new Rate(_rate));
                }
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return rates;
        }

        public LTLRateShipmentSimpleResponse CalculateLTLSimpleRate(LTLRateShipmentSimpleRequest request) {
            //
            LTLRateShipmentSimpleResponse response = null;
            try {
                response = new RateWareGateway().CalculateLTLRate(request);
            }
            catch(Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message), "Service Error"); }
            return response;
        }
        public LTLRateShipmentResponse CalculateLTLRate(LTLRateShipmentRequest request) {
            //
            LTLRateShipmentResponse response = null;
            try {
                response = new RateWareGateway().CalculateLTLRate(request);
            }
            catch(Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message), "Service Error"); }
            return response;
        }
        public LTLRateShipmentResponse[] CalculateLTLRates(LTLRateShipmentRequest[] requests) {
            //
            LTLRateShipmentResponse[] response = null;
            try {
                response = new RateWareGateway().CalculateLTLRates(requests);
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return response;
        }
        public LTLPointListResponse GetLTLPointList(LTLPointListRequest request) {
            //
            LTLPointListResponse response = null;
            try {
                response = new RateWareGateway().GetLTLPointList(request);
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return response;
        }
        public DensityRateShipmentResponse[] CalculateDensityRates(DensityRateShipmentRequest[] requests) {
            //
            DensityRateShipmentResponse[] response = null;
            try {
                response = new RateWareGateway().CalculateDensityRates(requests);
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return response;
        }
        public LinearRateShipmentResponse[] CalculateLinearRates(LinearRateShipmentRequest[] requests) {
            //
            LinearRateShipmentResponse[] response = null;
            try {
                response = new RateWareGateway().CalculateLinearRates(requests);
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return response;
        }
        public Decimal GetMileage(string originPostalCode,string destinationPostalCode) {
            //
            Decimal miles = 0;
            try {
                miles = new RateWareGateway().GetMileage(originPostalCode,destinationPostalCode);
            }
            catch (Exception ex) { throw new FaultException<RateQuoteFault>(new RateQuoteFault(ex.Message),"Service Error"); }
            return miles;
        }


        //Just messing around
        public DataSet ViewDeliveryZips() {
            //
            DataSet zips = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().ViewDeliveryZips();
                zips.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message), "Service Error"); }
            return zips;
        }

    }
}
