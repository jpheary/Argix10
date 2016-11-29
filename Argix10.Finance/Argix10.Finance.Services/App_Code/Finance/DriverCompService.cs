using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Transactions;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Argix.Enterprise;

namespace Argix.Finance {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class DriverCompService:IDriverCompService {
        //Members

        //Interface
        public DriverCompService() { }
        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            return new AppService(DriverCompGateway.SQL_CONNID).GetServiceInfo(); ;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(DriverCompGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet ReadRoadshowRoutes(string agentNumber,DateTime startDate,DateTime endDate) {
            //
            DataSet routes = new DataSet();
            try {
                DataSet ds = new DriverCompGateway().ReadRoadshowRoutes(agentNumber,startDate,endDate);
                routes.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return routes;
        }
        public DataSet ReadDriverRoutes(string agentNumber,DateTime startDate,DateTime endDate) {
            //Get a list of all driver routes for this terminal and date range
            DataSet routes = new DataSet();
            try {
                DataSet ds = new DriverCompGateway().ReadDriverRoutes(agentNumber,startDate,endDate);
                if (ds.Tables["DriverRouteTable"] != null) {
                    ds.Tables["DriverRouteTable"].Columns.Add("IsNew",typeof(bool),"IsNewRoute=1");
                    routes.Merge(ds);
                }
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return routes;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Clerk")]
        public bool CreateDriverRoute(DriverRoute route) {
            bool result = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    result = new DriverCompGateway().CreateDriverRoute(route);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return result;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Clerk")]
        public bool UpdateDriverRoute(DriverRoute route) {
            //Update a driver route
            bool result = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    result = new DriverCompGateway().UpdateDriverRoute(route);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return result;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Clerk")]
        public bool DeleteDriverRoute(long routeID) {
            //Delete a driver route
            bool result = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    result = new DriverCompGateway().DeleteDriverRoute(routeID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return result;
        }
        
        public DataSet ReadTerminalConfigurations(string agentNumber) {
            DataSet configs = null;
            try {
                configs = new DriverCompGateway().ReadTerminalConfigurations(agentNumber);
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return configs;
        }
        public TerminalConfiguration GetTerminalConfiguration(string agentNumber) {
            //Return a single terminal configuration
            TerminalConfiguration configuration = null;
            DataSet configurations = ReadTerminalConfigurations(agentNumber);
            DataRow[] configs = configurations.Tables["TerminalConfigurationTable"].Select("AgentNumber='" + agentNumber + "'");
            if (configs.Length > 0) {
                configuration = new TerminalConfiguration();
                configuration.AgentNumber = configs[0]["AgentNumber"].ToString();
                configuration.AgentName = configs[0]["AgentName"].ToString();
                configuration.GLNumber = configs[0]["GLNumber"].ToString();
                configuration.AdminGLNumber = configs[0]["AdminGLNumber"].ToString();
                configuration.AdminFee = decimal.Parse(configs[0]["AdminFee"].ToString());
                configuration.FSBase = decimal.Parse(configs[0]["FSBase"].ToString());
                configuration.BonusRate = decimal.Parse(configs[0]["BonusRate"].ToString());
            }
            return configuration;
        }
        public DataSet ReadDriverEquipment(string financeVendorID,string operatorName) {
            DataSet equipment = null;
            try {
                equipment = new DriverCompGateway().ReadDriverEquipment(financeVendorID,operatorName);
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return equipment;
        }

        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool CreateDriverEquipment(string financeVendorID,string operatorName,int equipmentID) {
            bool result = false;
            try {
                result = new DriverCompGateway().CreateDriverEquipment(financeVendorID,operatorName,equipmentID);
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return result;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool UpdateDriverEquipment(string financeVendorID,string operatorName,int equipmentID) {
            bool result = false;
            try {
                //Save driver route
                result = new DriverCompGateway().UpdateDriverEquipment(financeVendorID,operatorName,equipmentID);
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return result;
        }
        
        public DataSet ReadVehicleMileageRates(DateTime date,string terminalAgent,int equipmentTypeID) {
            DataSet rates = null;
            try {
                rates = new DriverCompGateway().ReadMileageRates(date,terminalAgent,equipmentTypeID);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return rates;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool CreateVehicleMileageRate(string agentNumber,int equipmentID,DateTime effectiveDate,double mile,decimal baseRate,decimal rate) {
            bool result = false;
            try {
                result = new DriverCompGateway().CreateMileageRate(agentNumber,equipmentID,effectiveDate,mile,baseRate,rate);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool UpdateVehicleMileageRate(int id,int equipmentID,double mile,decimal baseRate,decimal rate) {
            bool result = false;
            try {
                result = new DriverCompGateway().UpdateMileageRate(id,equipmentID,mile,baseRate,rate);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        
        public DataSet ReadVehicleUnitRates(DateTime date,string terminalAgent,int equipmentTypeID) {
            DataSet rates = null;
            try {
                rates = new DriverCompGateway().ReadUnitRates(date,terminalAgent,equipmentTypeID);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return rates;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool CreateVehicleUnitRate(string agentNumber,int equipmentID,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            bool result = false;
            try {
                result = new DriverCompGateway().CreateUnitRate(agentNumber,equipmentID,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool UpdateVehicleUnitRate(int id,int equipmentID,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase) {
            bool result = false;
            try {
                result = new DriverCompGateway().UpdateUnitRate(id,equipmentID,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        
        public DataSet ReadRouteMileageRates(DateTime date,string terminalAgent,string route) {
            DataSet rates = null;
            try {
                rates = new DriverCompGateway().ReadMileageRouteRates(date,terminalAgent,route);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return rates;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool CreateRouteMileageRate(string agentNumber,string route,DateTime effectiveDate,double mile,decimal baseRate,decimal rate,int status) {
            bool result = false;
            try {
                result = new DriverCompGateway().CreateMileageRouteRate(agentNumber,route,effectiveDate,mile,baseRate,rate,status);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool UpdateRouteMileageRate(int id,string route,double mile,decimal baseRate,decimal rate,int status) {
            bool result = false;
            try {
                result = new DriverCompGateway().UpdateMileageRouteRate(id,route,mile,baseRate,rate,status);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        
        public DataSet ReadRouteUnitRates(DateTime date,string terminalAgent,string route) {
            DataSet rates = null;
            try {
                rates = new DriverCompGateway().ReadUnitRouteRates(date,terminalAgent,route);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return rates;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool CreateRouteUnitRate(string agentNumber,string route,DateTime effectiveDate,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            bool result = false;
            try {
                result = new DriverCompGateway().CreateUnitRouteRate(agentNumber,route,effectiveDate,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase,status);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool UpdateRouteUnitRate(int id,string route,decimal dayRate,decimal tripRate,decimal stopRate,decimal cartonRate,decimal palletRate,decimal returnRate,decimal minAmount,decimal maxAmt,string maxTrigFld,int maxTrigVal,decimal fsBase,int status) {
            bool result = false;
            try {
                result = new DriverCompGateway().UpdateUnitRouteRate(id,route,dayRate,tripRate,stopRate,cartonRate,palletRate,returnRate,minAmount,maxAmt,maxTrigFld,maxTrigVal,fsBase,status);
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return result;
        }

        public DataSet GetLocalTerminals() {
            DataSet terminals = null;
            try {
                terminals = new DriverCompGateway().ReadLocalTerminals();
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return terminals;
        }
        public DataSet GetAdjustmentTypes() {
            DataSet types = null;
            try {
                types = new DriverCompGateway().ReadAdjustmentTypes();
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return types;
        }
        public DataSet GetDriverEquipmentTypes() {
            DataSet types = null;
            try {
                types = new DriverCompGateway().ReadDriverEquipmentTypes();
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return types;
        }
        public DataSet GetRateTypes() {
            DataSet types = null;
            try {
                types = new DriverCompGateway().ReadRateTypes();
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return types;
        }
        public PayPeriod GetPayPeriod(DateTime date) {
            PayPeriod pp = new PayPeriod();
            try {
                DataSet ds = new DriverCompGateway().ReadPayPeriod(date);
                if (ds.Tables["PayPeriodTable"] != null && ds.Tables["PayPeriodTable"].Rows.Count > 0) {
                    pp.Month = ds.Tables["PayPeriodTable"].Rows[0]["Month"].ToString().PadLeft(2,'0');
                    pp.Year = ds.Tables["PayPeriodTable"].Rows[0]["Year"].ToString();
                }
            }
            catch (Exception ex) { throw new FaultException<DriverRatingFault>(new DriverRatingFault(ex.Message),"Service Error"); }
            return pp;
        }
        public decimal GetFuelCost(DateTime date,string agentNumber) {
            decimal cost = 0.0M;
            try {
                cost = new DriverCompGateway().GetFuelCost(date,agentNumber);
            }
            catch (Exception ex) { throw new FaultException<DriverCompensationFault>(new DriverCompensationFault(ex.Message),"Service Error"); }
            return cost;
        }
    }
}
