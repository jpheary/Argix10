using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Finance {
    //
    public class DriverCompGateway {
        //Members
        public const string SQL_CONNID = "DriverComp";

        private const string USP_ROADSHOWROUTES = "uspDCRoadshowRoutesGetList3",TBL_ROADSHOWROUTES = "RoadshowRouteTable";
        private const string USP_DRIVERROUTES = "uspDCDriverRoutesGetList2",TBL_DRIVERROUTES = "DriverRouteTable";
        private const string USP_ROUTECREATE = "uspDCDriverRouteNew2",USP_ROUTEUPDATE = "uspDCDriverRouteUpdate",USP_ROUTEDELETE = "uspDCDriverRouteDelete";
        private const string USP_DRIVERLASTROUTE = "uspDCDriverCompensationLatestGetList",TBL_DRIVERLASTROUTE = "DriverRouteTable";
        private const string USP_FUELCOST = "uspDCFuelCostGet",TBL_FUELCOST = "FuelCostTable";

        private const string USP_TERMCONFIG = "uspDCTerminalConfigurationGet2",TBL_TERMCONFIG = "TerminalConfigurationTable";
        private const string USP_DRIVEREQUIP = "uspDCDriverEquipmentGet",TBL_DRIVEREQUIP = "DriverEquipmentTable";
        private const string USP_DRIVEREQUIPNEW = "uspDCDriverEquipmentNew", USP_DRIVEREQUIPUPDATE = "uspDCDriverEquipmentUpdate";
        
        private const string USP_RATEMILEAGE = "uspDCRateMilageGetList",TBL_RATEMILEAGE = "RateMileageTable";
        private const string USP_RATEMILEAGENEW = "uspDCRateMilageNew",USP_RATEMILEAGEUPDATE = "uspDCRateMilageUpdate";
        private const string USP_RATEUNIT = "uspDCRateUnitGetList",TBL_RATEUNIT = "RateUnitTable";
        private const string USP_RATEUNITNEW = "uspDCRateUnitNew",USP_RATEUNITUPDATE = "uspDCRateUnitUpdate";
        private const string USP_RATEMILEAGEROUTE = "uspDCRateMilageRouteGetList",TBL_RATEMILEAGEROUTE = "RateMileageRouteTable";
        private const string USP_RATEMILEAGEROUTENEW = "uspDCRateMilageRouteNew",USP_RATEMILEAGEROUTEUPDATE = "uspDCRateMilageRouteUpdate";
        private const string USP_RATEUNITROUTE = "uspDCRateUnitRouteGetList",TBL_RATEUNITROUTE = "RateUnitRouteTable";
        private const string USP_RATEUNITROUTENEW = "uspDCRateUnitRouteNew",USP_RATEUNITROUTEUPDATE = "uspDCRateUnitRouteUpdate";

        private const string LOCALTERMINALS = "uspDCLocalTerminalGetList",TBL_ENTERPRISEAGENTS = "LocalTerminalTable";
        private const string USP_ADJUSTTYPE = "uspDCAdjustmentTypeGetList",TBL_ADJUSTTYPE = "AdjustmentTypeTable";
        private const string USP_EQUIPTYPE = "uspDCEquipmentTypeGetList",TBL_EQUIPTYPE = "EquipmentTypeTable";
        private const string USP_RATETYPE = "uspDCRateTypeGetList",TBL_RATETYPE = "RateTypeTable";
        private const string USP_PAYPERIOD = "uspDCPayPeriodGet",TBL_PAYPERIOD = "PayPeriodTable";

        //Interface
        public DriverCompGateway() { }

        public DataSet ReadRoadshowRoutes(string agentNumber,DateTime startDate,DateTime endDate) {
            //Get a list of all Roadshow routes for this terminal and date range
            DataSet routes = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ROADSHOWROUTES,TBL_ROADSHOWROUTES,new object[] { agentNumber,startDate,endDate });
                if (ds.Tables[TBL_ROADSHOWROUTES].Rows.Count > 0) routes.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return routes;
        }
        public DataSet ReadDriverRoutes(string agentNumber,DateTime startDate,DateTime endDate) {
            //Get a list of all driver routes for this terminal and date range
            DataSet routes = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DRIVERROUTES,TBL_DRIVERROUTES,new object[] { agentNumber,startDate,endDate });
                if (ds.Tables[TBL_DRIVERROUTES] != null && ds.Tables[TBL_DRIVERROUTES].Rows.Count > 0) routes.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return routes;
        }
        public bool CreateDriverRoute(DriverRoute route) {
            bool result = false;
            try {
                //Save driver route
                DateTime imported = DateTime.Now;
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ROUTECREATE,new object[] {   
                            route.RouteDate, route.RouteIndex, route.RouteName, route.AgentNumber,
                            route.Operator, route.Payee, route.FinanceVendorID,route.EquipmentTypeID,
                            route.Miles, route.Trip, route.Stops, route.Cartons, route.Pallets, route.PickupCartons, route.RateTypeID,
                            route.DayRate, route.DayAmount, route.MilesRate, route.MilesBaseRate, route.MilesAmount, 
                            route.TripRate, route.TripAmount, route.StopsRate, route.StopsAmount, route.CartonsRate, route.CartonsAmount,
                            route.PalletsRate, route.PalletsAmount, route.PickupCartonsRate, route.PickupCartonsAmount,
                            route.FSCMiles, route.FuelCost, route.FSCGal, route.FSCBaseRate, route.FSC,
                            route.MinimunAmount, route.AdjustmentAmount1, route.AdjustmentAmount1TypeID, route.AdjustmentAmount2, route.AdjustmentAmount2TypeID, 0.0M, 0, 
                            route.AdminCharge, route.TotalAmount, imported, null, 
                            route.LastUpdated, route.UserID, route.ArgixRtType });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public bool UpdateDriverRoute(DriverRoute route) {
            //Update a driver route
            bool result = false;
            try {
                //Update the specified route
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ROUTEUPDATE,new object[] {   
                            route.RouteDate, route.RouteName, route.AgentNumber, route.Operator, 
                            route.AdjustmentAmount1, route.AdjustmentAmount1TypeID, route.AdjustmentAmount2, route.AdjustmentAmount2TypeID, 0.0, 0,
                            route.AdminCharge, route.TotalAmount, 
                            (route.Exported!=DateTime.MinValue ? route.Exported : null as object), 
                            route.LastUpdated, route.UserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public bool DeleteDriverRoute(long routeID) {
            //Delete a driver route
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ROUTEDELETE,new object[] { routeID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public DataSet ReadDriverLastRoutes() {
            DataSet routes = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DRIVERLASTROUTE,TBL_DRIVERLASTROUTE,new object[] { });
                if (ds.Tables[TBL_DRIVERLASTROUTE].Rows.Count > 0) routes.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return routes;
        }

        public DataSet ReadTerminalConfigurations(string agentNumber = null) {
            DataSet configurations = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TERMCONFIG,TBL_TERMCONFIG,new object[] { agentNumber });
                if (ds.Tables[TBL_TERMCONFIG].Rows.Count > 0) configurations.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return configurations;
        }
        public DataSet ReadDriverEquipment(string financeVendorID,string operatorName) {
            DataSet equipment = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DRIVEREQUIP,TBL_DRIVEREQUIP,new object[] { financeVendorID,operatorName });
                if (ds.Tables[TBL_DRIVEREQUIP].Rows.Count > 0) equipment.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return equipment;
        }
        public bool CreateDriverEquipment(string financeVendorID,string operatorName,int equipmentID) {
            bool result = false;
            try {
                //Save driver equipment
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_DRIVEREQUIPNEW,new object[] { financeVendorID,operatorName,equipmentID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public bool UpdateDriverEquipment(string financeVendorID,string operatorName,int equipmentID) {
            bool result = false;
            try {
                //Save driver route
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_DRIVEREQUIPUPDATE,new object[] { financeVendorID,operatorName,equipmentID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return result;
        }
        public DataSet ReadMileageRates(DateTime date,string terminalAgent=null,int equipmentTypeID=-1) {
            //Return applicable mileage rates for the specified date
            DataSet rates = new DataSet();
            try {
                object id = null; if (equipmentTypeID > -1) id = equipmentTypeID;
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_RATEMILEAGE,TBL_RATEMILEAGE,new object[] { date,terminalAgent,id });
                if (ds.Tables[TBL_RATEMILEAGE].Rows.Count > 0)
                    rates.Merge(ds);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rates;
        }
        public bool CreateMileageRate(string agentNumber,int equipmentID,DateTime effectiveDate,double mile,decimal baseRate,decimal rate) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEMILEAGENEW,new object[] { agentNumber,equipmentID,effectiveDate,mile,baseRate,rate });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while creating new mileage rate...",ex); }
            return result;
        }
        public bool UpdateMileageRate(int id,int equipmentID,double mile,decimal baseRate,decimal rate) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEMILEAGEUPDATE,new object[] { id,equipmentID,mile,baseRate,rate });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while updating mileage rate...",ex); }
            return result;
        }
        public DataSet ReadUnitRates(DateTime date,string terminalAgent,int equipmentTypeID) {
            //Return applicable unit rates (i.e. multi-trip, carton, pallet) for the specified date
            DataSet rates = new DataSet();
            try {
                object id = null; if (equipmentTypeID > -1) id = equipmentTypeID;
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_RATEUNIT,TBL_RATEUNIT,new object[] { date,terminalAgent,id });
                if (ds.Tables[TBL_RATEUNIT].Rows.Count > 0) rates.Merge(ds);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rates;
        }
        public bool CreateUnitRate(string agentNumber,int equipmentID,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEUNITNEW,new object[] { agentNumber,equipmentID,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while creating new unit rate...",ex); }
            return result;
        }
        public bool UpdateUnitRate(int id,int equipmentID,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEUNITUPDATE,new object[] { id,equipmentID,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while updating unit rate...",ex); }
            return result;
        }
        public DataSet ReadMileageRouteRates(DateTime date,string terminalAgent,string route) {
            //Return applicable route-based mileage rates for the specified date
            DataSet rates = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_RATEMILEAGEROUTE,TBL_RATEMILEAGEROUTE,new object[] { date,terminalAgent,route });
                if (ds.Tables[TBL_RATEMILEAGEROUTE].Rows.Count > 0) rates.Merge(ds);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rates;
        }
        public bool CreateMileageRouteRate(string agentNumber,string route,DateTime effectiveDate,double mile,decimal baseRate,decimal rate,int status) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEMILEAGEROUTENEW,new object[] { agentNumber,route,effectiveDate,mile,baseRate,rate,status });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while creating new mileage route rate...",ex); }
            return result;
        }
        public bool UpdateMileageRouteRate(int id,string route,double mile,decimal baseRate,decimal rate,int status) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEMILEAGEROUTEUPDATE,new object[] { id,route,mile,baseRate,rate });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while updating mileage route rate...",ex); }
            return result;
        }
        public DataSet ReadUnitRouteRates(DateTime date,string terminalAgent,string route) {
            //Return applicable route-based unit rates (i.e. multi-trip, carton, pallet) for the specified date
            DataSet rates = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_RATEUNITROUTE,TBL_RATEUNITROUTE,new object[] { date,terminalAgent,route });
                if (ds.Tables[TBL_RATEUNITROUTE].Rows.Count > 0) rates.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rates;
        }
        public bool CreateUnitRouteRate(string agentNumber,string route,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            bool result = false;
            try {
                //Save
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEUNITROUTENEW,new object[] { agentNumber,route,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase,status });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while creating new unit route rate...",ex); }
            return result;
        }
        public bool UpdateUnitRouteRate(int id,string route,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            bool result = false;
            try {
                //Update
                result = new DataService().ExecuteNonQuery(SQL_CONNID,USP_RATEUNITROUTEUPDATE,new object[] { id,route,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,fsBase });
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while updating unit route rate...",ex); }
            return result;
        }

        public DataSet ReadAdjustmentTypes() {
            DataSet types = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ADJUSTTYPE,TBL_ADJUSTTYPE,new object[] { });
                if (ds.Tables[TBL_ADJUSTTYPE].Rows.Count > 0) types.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return types;
        }
        public DataSet ReadDriverEquipmentTypes() {
            DataSet types = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_EQUIPTYPE,TBL_EQUIPTYPE,new object[] { });
                if (ds.Tables[TBL_EQUIPTYPE].Rows.Count > 0) types.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return types;
        }
        public DataSet ReadLocalTerminals() {
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,LOCALTERMINALS,TBL_ENTERPRISEAGENTS,new object[] { });
                if (ds.Tables[TBL_ENTERPRISEAGENTS].Rows.Count > 0) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return terminals;
        }
        public DataSet ReadPayPeriod(DateTime date) {
            //Get the Argix pay period for the specified date
            DataSet pp = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_PAYPERIOD,TBL_PAYPERIOD,new object[] { date });
                if (ds.Tables[TBL_PAYPERIOD].Rows.Count > 0) pp.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return pp;
        }
        public DataSet ReadRateTypes() {
            //
            DataSet rateTypes = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_RATETYPE,TBL_RATETYPE,new object[] { });
                if (ds.Tables[TBL_RATETYPE].Rows.Count > 0) rateTypes.Merge(ds);
            }
            catch (ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return rateTypes;
        }
        public decimal GetFuelCost(DateTime date,string terminalAgent) {
            decimal cost = 0.0M;
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_FUELCOST,TBL_FUELCOST,new object[] { date,terminalAgent });
                if (ds.Tables[TBL_FUELCOST] != null && ds.Tables[TBL_FUELCOST].Rows.Count > 0) cost = Convert.ToDecimal(ds.Tables[TBL_FUELCOST].Rows[0]["FuelCost"]);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return cost;
        }
    }
}
