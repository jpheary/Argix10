using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Threading;
using Argix.Windows;

namespace Argix.Finance {
	//
	public class FinanceGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        private static int _cacheTimeout = 240;
        
		//Interface
        static FinanceGateway() { 
            //
            DriverCompServiceClient client = new DriverCompServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
            _cacheTimeout = global::Argix.Properties.Settings.Default.CacheTimeout;
        }
        private FinanceGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal=null;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                client.WriteLogEntry(m);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static DriverCompDataset ImportRoutes(string agentNumber,DateTime startDate,DateTime endDate) {
            //
            DriverCompDataset routes = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadRoadshowRoutes(agentNumber,startDate,endDate);
                if (ds.Tables["RoadshowRouteTable"] != null && ds.Tables["RoadshowRouteTable"].Rows.Count > 0) routes.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return routes;
        }
        public static DriverCompDataset ReadDriverRoutes(string agentNumber, DateTime startDate, DateTime endDate) {
            //
            DriverCompDataset routes = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadDriverRoutes(agentNumber, startDate, endDate);
                if(ds.Tables["DriverRouteTable"] != null && ds.Tables["DriverRouteTable"].Rows.Count > 0) {
                    routes.Merge(ds);
                    for(int i = 0; i < routes.DriverRouteTable.Rows.Count; i++) {
                        //Set local fields (i.e. not persisted)
                        DriverCompDataset.DriverRouteTableRow route = (DriverCompDataset.DriverRouteTableRow)routes.DriverRouteTable.Rows[i];
                        route.IsNew = false;
                        route.IsCombo = (routes.DriverRouteTable.Select("Operator='" + route.Operator + "' AND RouteDate='" + route.RouteDate + "'").Length > 1);
                        route.IsAdjust = route.RouteName.Contains("ADJUST");
                    }
                    routes.AcceptChanges();
                }
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return routes;
        }
        public static bool CreateDriverRoute(DriverCompDataset.DriverRouteTableRow route) {
            //
            bool created = false;
            DriverCompServiceClient client = null;
            try {
                DriverRoute dr = new DriverRoute();
                dr.IsNew = !route.IsIsNewNull() ? route.IsNew : false;
                dr.IsCombo = !route.IsIsComboNull() ? route.IsCombo : false;
                dr.IsAdjust = !route.IsIsAdjustNull() ? route.IsAdjust : false;
                dr.AgentNumber = !route.IsAgentNumberNull() ? route.AgentNumber : "";
                dr.FinanceVendorID = !route.IsFinanceVendorIDNull() ? route.FinanceVendorID : "";
                dr.RouteIndex = route.RouteIndex;
                dr.RouteDate = !route.IsRouteDateNull() ? route.RouteDate : DateTime.MinValue;
                dr.RouteName = !route.IsRouteNameNull() ? route.RouteName : "";
                dr.Operator = !route.IsOperatorNull() ? route.Operator : "";
                dr.Payee = !route.IsPayeeNull() ? route.Payee : "";
                dr.EquipmentTypeID = !route.IsEquipmentTypeIDNull() ? route.EquipmentTypeID : 0;
                dr.RateTypeID = !route.IsRateTypeIDNull() ? route.RateTypeID : 0;
                dr.DayRate = !route.IsDayRateNull() ? route.DayRate : 0.0M;
                dr.DayAmount = !route.IsDayAmountNull() ? route.DayAmount : 0.0M;
                dr.Miles = !route.IsMilesNull() ? route.Miles : 0.0M;
                dr.MilesBaseRate = !route.IsMilesBaseRateNull() ? route.MilesBaseRate : 0.0M;
                dr.MilesRate = !route.IsMilesRateNull() ? route.MilesRate : 0.0M;
                dr.MilesAmount = !route.IsMilesAmountNull() ? route.MilesAmount : 0.0M;
                dr.Trip = !route.IsTripNull() ? route.Trip : 0;
                dr.TripRate = !route.IsTripRateNull() ? route.TripRate : 0.0M;
                dr.TripAmount = !route.IsTripAmountNull() ? route.TripAmount : 0.0M;
                dr.Stops = !route.IsStopsNull() ? route.Stops : 0;
                dr.StopsRate = !route.IsStopsRateNull() ? route.StopsRate : 0.0M;
                dr.StopsAmount = !route.IsStopsAmountNull() ? route.StopsAmount : 0.0M;
                dr.Cartons = !route.IsCartonsNull() ? route.Cartons : 0;
                dr.CartonsRate = !route.IsCartonsRateNull() ? route.CartonsRate : 0.0M;
                dr.CartonsAmount = !route.IsCartonsAmountNull() ? route.CartonsAmount : 0.0M;
                dr.Pallets = !route.IsPalletsNull() ? route.Pallets : 0;
                dr.PalletsRate = !route.IsPalletsRateNull() ? route.PalletsRate : 0.0M;
                dr.PalletsAmount = !route.IsPalletsAmountNull() ? route.PalletsAmount : 0.0M;
                dr.PickupCartons = !route.IsPickupCartonsNull() ? route.PickupCartons : 0;
                dr.PickupCartonsRate = !route.IsPickupCartonsRateNull() ? route.PickupCartonsRate : 0.0M;
                dr.PickupCartonsAmount = !route.IsPickupCartonsAmountNull() ? route.PickupCartonsAmount : 0.0M;
                dr.MinimunAmount = !route.IsMinimunAmountNull() ? route.MinimunAmount : 0.0M;
                dr.FSCMiles = !route.IsFSCMilesNull() ? route.FSCMiles : 0.0M;
                dr.FuelCost = !route.IsFuelCostNull() ? route.FuelCost : 0.0M;
                dr.FSCGal = !route.IsFSCGalNull() ? route.FSCGal : 0.0M;
                dr.FSCBaseRate = !route.IsFSCBaseRateNull() ? route.FSCBaseRate : 0.0M;
                dr.FSC = !route.IsFSCNull() ? route.FSC : 0.0M;
                dr.AdjustmentAmount1 = !route.IsAdjustmentAmount1Null() ? route.AdjustmentAmount1 : 0.0M;
                dr.AdjustmentAmount1TypeID = !route.IsAdjustmentAmount1TypeIDNull() ? route.AdjustmentAmount1TypeID : "";
                dr.AdjustmentAmount2 = !route.IsAdjustmentAmount2Null() ? route.AdjustmentAmount2 : 0.0M;
                dr.AdjustmentAmount2TypeID = !route.IsAdjustmentAmount2TypeIDNull() ? route.AdjustmentAmount2TypeID : "";
                dr.AdminCharge = !route.IsAdminChargeNull() ? route.AdminCharge : 0.0M;
                dr.TotalAmount = !route.IsTotalAmountNull() ? route.TotalAmount : 0.0M;
                dr.Imported = !route.IsImportedNull() ? route.Imported : DateTime.MinValue;
                dr.Exported = !route.IsExportedNull() ? route.Exported : DateTime.MinValue;
                dr.ArgixRtType = !route.IsArgixRtTypeNull() ? route.ArgixRtType : "";
                dr.LastUpdated = DateTime.Now;
                dr.UserID = Environment.UserName;

                client = new DriverCompServiceClient();
                created = client.CreateDriverRoute(dr);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateDriverRoute(DriverCompDataset.DriverRouteTableRow route) {
            //
            bool updated = false;
            DriverCompServiceClient client = null;
            try {
                DriverRoute dr = new DriverRoute();
                dr.ID = route.ID;
                dr.IsNew = !route.IsIsNewNull() ? route.IsNew : false;
                dr.IsCombo = !route.IsIsComboNull() ? route.IsCombo : false;
                dr.IsAdjust = !route.IsIsAdjustNull() ? route.IsAdjust : false;
                dr.AgentNumber = !route.IsAgentNumberNull() ? route.AgentNumber : "";
                dr.FinanceVendorID = !route.IsFinanceVendorIDNull() ? route.FinanceVendorID : "";
                dr.RouteDate = !route.IsRouteDateNull() ? route.RouteDate : DateTime.MinValue;
                dr.RouteName = !route.IsRouteNameNull() ? route.RouteName : "";
                dr.Operator = !route.IsOperatorNull() ? route.Operator : "";
                dr.Payee = !route.IsPayeeNull() ? route.Payee : "";
                dr.EquipmentTypeID = !route.IsEquipmentTypeIDNull() ? route.EquipmentTypeID : 0;
                dr.RateTypeID = !route.IsRateTypeIDNull() ? route.RateTypeID : 0;
                dr.DayRate = !route.IsDayRateNull() ? route.DayRate : 0.0M;
                dr.DayAmount = !route.IsDayAmountNull() ? route.DayAmount : 0.0M;
                dr.Miles = !route.IsMilesNull() ? route.Miles : 0.0M;
                dr.MilesBaseRate = !route.IsMilesBaseRateNull() ? route.MilesBaseRate : 0.0M;
                dr.MilesRate = !route.IsMilesRateNull() ? route.MilesRate : 0.0M;
                dr.MilesAmount = !route.IsMilesAmountNull() ? route.MilesAmount : 0.0M;
                dr.Trip = !route.IsTripNull() ? route.Trip : 0;
                dr.TripRate = !route.IsTripRateNull() ? route.TripRate : 0.0M;
                dr.TripAmount = !route.IsTripAmountNull() ? route.TripAmount : 0.0M;
                dr.Stops = !route.IsStopsNull() ? route.Stops : 0;
                dr.StopsRate = !route.IsStopsRateNull() ? route.StopsRate : 0.0M;
                dr.StopsAmount = !route.IsStopsAmountNull() ? route.StopsAmount : 0.0M;
                dr.Cartons = !route.IsCartonsNull() ? route.Cartons : 0;
                dr.CartonsRate = !route.IsCartonsRateNull() ? route.CartonsRate : 0.0M;
                dr.CartonsAmount = !route.IsCartonsAmountNull() ? route.CartonsAmount : 0.0M;
                dr.Pallets = !route.IsPalletsNull() ? route.Pallets : 0;
                dr.PalletsRate = !route.IsPalletsRateNull() ? route.PalletsRate : 0.0M;
                dr.PalletsAmount = !route.IsPalletsAmountNull() ? route.PalletsAmount : 0.0M;
                dr.PickupCartons = !route.IsPickupCartonsNull() ? route.PickupCartons : 0;
                dr.PickupCartonsRate = !route.IsPickupCartonsRateNull() ? route.PickupCartonsRate : 0.0M;
                dr.PickupCartonsAmount = !route.IsPickupCartonsAmountNull() ? route.PickupCartonsAmount : 0.0M;
                dr.MinimunAmount = !route.IsMinimunAmountNull() ? route.MinimunAmount : 0.0M;
                dr.FSCMiles = !route.IsFSCMilesNull() ? route.FSCMiles : 0.0M;
                dr.FuelCost = !route.IsFuelCostNull() ? route.FuelCost : 0.0M;
                dr.FSCGal = !route.IsFSCGalNull() ? route.FSCGal : 0.0M;
                dr.FSCBaseRate = !route.IsFSCBaseRateNull() ? route.FSCBaseRate : 0.0M;
                dr.FSC = !route.IsFSCNull() ? route.FSC : 0.0M;
                dr.AdjustmentAmount1 = !route.IsAdjustmentAmount1Null() ? route.AdjustmentAmount1 : 0.0M;
                dr.AdjustmentAmount1TypeID = !route.IsAdjustmentAmount1TypeIDNull() ? route.AdjustmentAmount1TypeID : "";
                dr.AdjustmentAmount2 = !route.IsAdjustmentAmount2Null() ? route.AdjustmentAmount2 : 0.0M;
                dr.AdjustmentAmount2TypeID = !route.IsAdjustmentAmount2TypeIDNull() ? route.AdjustmentAmount2TypeID : "";
                dr.AdminCharge = !route.IsAdminChargeNull() ? route.AdminCharge : 0.0M;
                dr.TotalAmount = !route.IsTotalAmountNull() ? route.TotalAmount : 0.0M;
                dr.Imported = !route.IsImportedNull() ? route.Imported : DateTime.MinValue;
                dr.Exported = !route.IsExportedNull() ? route.Exported : DateTime.MinValue;
                dr.ArgixRtType = !route.IsArgixRtTypeNull() ? route.ArgixRtType : "";
                dr.LastUpdated = DateTime.Now;
                dr.UserID = Environment.UserName;

                client = new DriverCompServiceClient();
                updated = client.UpdateDriverRoute(dr);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool DeleteDriverRoute(long routeID) {
            //
            bool deleted = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                deleted = client.DeleteDriverRoute(routeID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return deleted;
        }

        public static DriverCompDataset ReadTerminalConfigurations() {
            //
            DriverCompDataset configurations = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadTerminalConfigurations(null);
                client.Close();
                if (ds.Tables["TerminalConfigurationTable"] != null && ds.Tables["TerminalConfigurationTable"].Rows.Count > 0) configurations.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return configurations;
        }
        public static TerminalConfiguration GetTerminalConfiguration(string agentNumber) {
            //Return a single terminal configuration
            TerminalConfiguration configuration = null;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                configuration = client.GetTerminalConfiguration(agentNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return configuration;
        }
        public static DriverCompDataset ReadDriverEquipment() {
            //
            DriverCompDataset equipment = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadDriverEquipment(null,null);
                client.Close();
                if (ds.Tables["DriverEquipmentTable"] != null && ds.Tables["DriverEquipmentTable"].Rows.Count > 0) equipment.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return equipment;
        }
        public static bool CreateDriverEquipment(string financeVendorID,string operatorName,int equipmentID) {
            //
            bool created = false;
            DriverCompServiceClient client = null;
            try {
                //Validate
                DriverCompDataset equipment = ReadDriverEquipment();
                DataRow[] rows = equipment.DriverEquipmentTable.Select("FinanceVendorID='" + financeVendorID + "' AND OperatorName='" + operatorName + "'");
                if (rows.Length > 0) throw new ApplicationException("Equipment already specified for " + operatorName + ".");

                //Save driver equipment
                client = new DriverCompServiceClient();
                created = client.CreateDriverEquipment(financeVendorID,operatorName,equipmentID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateDriverEquipment(string financeVendorID,string operatorName,int equipmentID) {
            //
            bool updated = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                updated = client.UpdateDriverEquipment(financeVendorID,operatorName,equipmentID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        
        public static DataSet ReadVehicleMileageRates(DateTime effectiveDate,string terminalAgent,int equipmentTypeID) {
            //
            DriverCompDataset rates = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadVehicleMileageRates(effectiveDate,terminalAgent,equipmentTypeID);
                client.Close();
                if (ds.Tables["RateMileageTable"] != null && ds.Tables["RateMileageTable"].Rows.Count > 0) rates.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return rates;
        }
        public static bool CreateVehicleMileageRate(string agentNumber,int equipmentID,DateTime effectiveDate,double mile,decimal baseRate,decimal rate) {
            //
            bool created = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                created = client.CreateVehicleMileageRate(agentNumber,equipmentID,effectiveDate,mile,baseRate,rate);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateVehicleMileageRate(int id,int equipmentID,double mile,decimal baseRate,decimal rate) {
            //
            bool updated = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                updated = client.UpdateVehicleMileageRate(id,equipmentID,mile,baseRate,rate);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static DataSet ReadVehicleUnitRates(DateTime effectiveDate,string terminalAgent,int equipmentTypeID) {
            //
            DriverCompDataset rates = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadVehicleUnitRates(effectiveDate,terminalAgent,equipmentTypeID);
                client.Close();
                if (ds.Tables["RateUnitTable"] != null && ds.Tables["RateUnitTable"].Rows.Count > 0) rates.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return rates;
        }
        public static bool CreateVehicleUnitRate(string agentNumber,int equipmentID,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            //
            bool created = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                created = client.CreateVehicleUnitRate(agentNumber,equipmentID,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateVehicleUnitRate(int id,int equipmentID,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            //
            bool updated = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                updated = client.UpdateVehicleUnitRate(id,equipmentID,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static DataSet ReadRouteMileageRates(DateTime effectiveDate,string terminalAgent,string route) {
            //
            DriverCompDataset rates = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadRouteMileageRates(effectiveDate,terminalAgent,route);
                client.Close();
                if (ds.Tables["RateMileageRouteTable"] != null && ds.Tables["RateMileageRouteTable"].Rows.Count > 0) rates.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return rates;
        }
        public static bool CreateRouteMileageRate(string agentNumber,string route,DateTime effectiveDate,double mile,decimal baseRate,decimal rate,int status) {
            //
            bool created = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                created = client.CreateRouteMileageRate(agentNumber,route,effectiveDate,mile,baseRate,rate,status);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateRouteMileageRate(int id,string route,double mile,decimal baseRate,decimal rate,int status) {
            //
            bool updated = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                updated = client.UpdateRouteMileageRate(id,route,mile,baseRate,rate,status);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static DataSet ReadRouteUnitRates(DateTime effectiveDate,string terminalAgent,string route) {
            //
            DriverCompDataset rates = new DriverCompDataset();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                DataSet ds = client.ReadRouteUnitRates(effectiveDate,terminalAgent,route);
                client.Close();
                if (ds.Tables["RateUnitRouteTable"] != null && ds.Tables["RateUnitRouteTable"].Rows.Count > 0) rates.Merge(ds);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return rates;
        }
        public static bool CreateRouteUnitRate(string agentNumber,string route,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            //
            bool created = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                created = client.CreateRouteUnitRate(agentNumber,route,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase,status);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateRouteUnitRate(int id,string route,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            //
            bool updated = false;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                updated = client.UpdateRouteUnitRate(id,route,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase,status);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public static DataSet GetLocalTerminals() {
            //
            DriverCompDataset terminals = null;
            DriverCompServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                terminals = cache["localTerminals"] as DriverCompDataset;
                if (terminals == null) {
                    terminals = new DriverCompDataset();
                    client = new DriverCompServiceClient();
                    DataSet ds = client.GetLocalTerminals();
                    client.Close();
                    if (ds.Tables["LocalTerminalTable"] != null && ds.Tables["LocalTerminalTable"].Rows.Count > 0) terminals.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("localTerminals",terminals,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public static DriverCompDataset GetRouteAdjustmentTypes() {
            //
            DriverCompDataset types = null;
            DriverCompServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                types = cache["adjustmentTypes"] as DriverCompDataset;
                if (types == null) {
                    types = new DriverCompDataset();
                    client = new DriverCompServiceClient();
                    DataSet ds = client.GetAdjustmentTypes();
                    client.Close();
                    if (ds.Tables["AdjustmentTypeTable"] != null && ds.Tables["AdjustmentTypeTable"].Rows.Count > 0) types.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("adjustmentTypes",types,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return types;
        }
        public static DataSet GetDriverEquipmentTypes() {
            //
            DriverCompDataset types = null;
            DriverCompServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                types = cache["equipmentTypes"] as DriverCompDataset;
                if (types == null) {
                    types = new DriverCompDataset();
                    client = new DriverCompServiceClient();
                    DataSet ds = client.GetDriverEquipmentTypes();
                    client.Close();
                    if (ds.Tables["EquipmentTypeTable"] != null && ds.Tables["EquipmentTypeTable"].Rows.Count > 0) types.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("equipmentTypes",types,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return types;
        }
        public static int GetDriverEquipmentMPG(int equipmentTypeID) {
            //Get MPG rating for the specified driver equipment typeID
            int mpg = 0;
            DataSet types = GetDriverEquipmentTypes();
            try {
                if (types.Tables["EquipmentTypeTable"] != null && types.Tables["EquipmentTypeTable"].Rows.Count > 0) {
                    DataRow[] _types = types.Tables["EquipmentTypeTable"].Select("ID=" + equipmentTypeID);
                    if (_types.Length > 0) mpg = Convert.ToInt32(_types[0]["MPG"]);
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return mpg;

        }
        public static DriverCompDataset GetRateTypes() {
            //
            DriverCompDataset types = null;
            DriverCompServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                types = cache["rateTypes"] as DriverCompDataset;
                if (types == null) {
                    types = new DriverCompDataset();
                    client = new DriverCompServiceClient();
                    DataSet ds = client.GetRateTypes();
                    client.Close();
                    if (ds.Tables["RateTypeTable"] != null && ds.Tables["RateTypeTable"].Rows.Count > 0) types.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("rateTypes",types,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return types;
        }
        public static PayPeriod GetPayPeriod(DateTime effectiveDate) {
            //
            PayPeriod payPeriod = new PayPeriod();
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                payPeriod = client.GetPayPeriod(effectiveDate);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return payPeriod;
        }
        public static decimal GetFuelCost(DateTime effectiveDate,string agentNumber) {
            //
            decimal cost = 0.0M;
            DriverCompServiceClient client = null;
            try {
                client = new DriverCompServiceClient();
                cost = client.GetFuelCost(effectiveDate,agentNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DriverCompensationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return cost;
        }
    }
}