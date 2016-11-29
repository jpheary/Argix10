using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.RateWare {
	//
	public class RateWareGateway {
		//Members
        
		//Interface
        public RateWareGateway() { }

        public DataModule[] GetAvailableTariffs() {
            //
            DataModule[] tariffs = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) 
                    tariffs = client.AvailableTariffs(authToken);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tariffs;
        }
        public ClassesAndWeightsResponse GetClassesAndWeights(DataModule dataModule) {
            //
            ClassesAndWeightsResponse classes = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken))
                    classes = client.DataModuleValidClassesAndWeights(authToken,dataModule);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return classes;
        }

        public LTLRateShipmentSimpleResponse CalculateLTLRate(LTLRateShipmentSimpleRequest request) {
            //
            LTLRateShipmentSimpleResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    //LTL rate shipment request
                    response = client.LTLRateShipmentSimple(authToken,request);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public LTLRateShipmentResponse CalculateLTLRate(LTLRateShipmentRequest request) {
            //
            LTLRateShipmentResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    //LTL rate shipment request
                    response = client.LTLRateShipment(authToken,request);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public LTLRateShipmentResponse[] CalculateLTLRates(LTLRateShipmentRequest[] requests) {
            //
            LTLRateShipmentResponse[] response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    //LTL rate shipment request
                    response = client.LTLRateShipmentMultiple(authToken,requests);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public LTLPointListResponse GetLTLPointList(LTLPointListRequest request) {
            //
            LTLPointListResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken))
                    response = client.LTLPointList(authToken,request);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        
        public DensityRateShipmentSimpleResponse CalculateDensityRates(DensityRateShipmentSimpleRequest request) {
            //
            DensityRateShipmentSimpleResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {                    
                    response = client.DensityRateShipmentSimple(authToken,request);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public DensityRateShipmentResponse CalculateDensityRates(DensityRateShipmentRequest request) {
            //
            DensityRateShipmentResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    response = client.DensityRateShipment(authToken,request);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public DensityRateShipmentResponse[] CalculateDensityRates(DensityRateShipmentRequest[] requests) {
            //
            DensityRateShipmentResponse[] response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    response = client.DensityRateShipmentMultiple(authToken,requests);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }

        public LinearRateShipmentSimpleResponse CalculateLinearRates(LinearRateShipmentSimpleRequest request) {
            //
            LinearRateShipmentSimpleResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    response = client.LinearRateShipmentSimple(authToken,request);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public LinearRateShipmentResponse CalculateLinearRates(LinearRateShipmentRequest request) {
            //
            LinearRateShipmentResponse response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    response = client.LinearRateShipment(authToken,request);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }
        public LinearRateShipmentResponse[] CalculateLinearRates(LinearRateShipmentRequest[] requests) {
            //
            LinearRateShipmentResponse[] response = null;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    response = client.LinearRateShipmentMultiple(authToken,requests);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return response;
        }

        public Decimal GetMileage(string originPostalCode,string destinationPostalCode) {
            //
            Decimal miles = 0;
            RateWareXLPortTypeClient client = new RateWareXLPortTypeClient();
            try {
                AuthenticationToken authToken = getAuthenticationToken();
                if (client.isReady(authToken)) {
                    ShipmentLocale org = new ShipmentLocale();
                    org.postalCode = originPostalCode;
                    org.countryCode = "USA";

                    ShipmentLocale dest = new ShipmentLocale();
                    dest.postalCode = destinationPostalCode;
                    dest.countryCode = "USA";

                    Mileage mileage = new Mileage();
                    mileage.releaseVersion = "22";
                    mileage.routingType = "P";
                    mileage.systemId = 1;
                    mileage.origin = org;
                    mileage.destination = dest;

                    miles = (Decimal)client.Mileage(authToken,mileage);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return miles;
        }

        private AuthenticationToken getAuthenticationToken() {

            AuthenticationToken authToken = new AuthenticationToken();
            authToken.licenseKey = ConfigurationManager.AppSettings["licenseKey"];
            authToken.password = ConfigurationManager.AppSettings["password"];
            authToken.username = ConfigurationManager.AppSettings["username"];
            return authToken;
        }
    }
}
