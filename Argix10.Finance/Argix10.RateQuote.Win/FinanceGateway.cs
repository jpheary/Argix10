using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Argix.Windows;

namespace Argix.Finance {
	//
	public class FinanceGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static FinanceGateway() { 
            //
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private FinanceGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                ServiceInfo info = new ServiceInfo();
                info.TerminalID = 0;
                info.Description = "";
                return info;   // client.GetServiceInfo();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the application configuration for the specified user
            UserConfiguration config=null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                config = new UserConfiguration();   // client.GetUserConfiguration(application, usernames);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Write an entry into the Argix log
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                //client.WriteLogEntry(m);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static DataModule[] GetAvailableTariffs() {
            //
            DataModule[] tariffs = null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                tariffs = client.GetAvailableTariffs();
                client.Close();
            }            
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tariffs;
        }
        public static RateQuoteDataset GetClassCodes() {
            //
            RateQuoteDataset classes = new RateQuoteDataset();
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                classes.Merge(client.GetClassCodes());
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return classes;
        }

        public static Rates CalculateRates(RateQuoteRequest request) {
            //
            Rates rates = null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                rates = client.CalculateRates(request.Tariff,request.OriginPostalCode,request.ClassCode,request.MCDiscount,request.UserMinimumChargeFloor,request.DestinationPostalCodes);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return rates;
        }
        public static LTLRateShipmentSimpleResponse CalculateLTLSimpleRate(LTLRateShipmentSimpleRequest request) {
            //
            LTLRateShipmentSimpleResponse response = null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                response = client.CalculateLTLSimpleRate(request);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public static LTLRateShipmentResponse CalculateLTLRate(LTLRateShipmentRequest request) {
            //
            LTLRateShipmentResponse response = null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                response = client.CalculateLTLRate(request);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public static LTLRateShipmentResponse[] CalculateLTLRates(LTLRateShipmentRequest[] requests) {
            //
            LTLRateShipmentResponse[] response = null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                response = client.CalculateLTLRates(requests);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public static LTLPointListResponse GetLTLPointList(LTLPointListRequest request) {
            //
            LTLPointListResponse response = null;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                response = client.GetLTLPointList(request);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }

        public static Decimal GetMileage(string originPostalCode,string destinationPostalCode) {
            //
            Decimal miles = 0;
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                miles = (Decimal)client.GetMileage(originPostalCode,destinationPostalCode);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return miles;
        }


        //Just messing around
        public static DataSet ViewDeliveryZips() {
            //
            DataSet zips = new DataSet();
            RateQuoteServiceClient client = new RateQuoteServiceClient();
            try {
                zips.Merge(client.ViewDeliveryZips());
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<RateQuoteFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return zips;
        }

    }
}