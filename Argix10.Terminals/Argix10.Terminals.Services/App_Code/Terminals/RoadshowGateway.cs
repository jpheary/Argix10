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


namespace Argix.Terminals {
    //
    public class RoadshowGateway {
        //Members
        public const string SQL_CONNID = "Roadshow";

        private const string USP_PICKUPS_LOAD = "uspPickupsFromOrders2";
        private const string USP_PICKUPS = "uspPickupGetList",TBL_PICKUPS = "PickupTable";
        private const string USP_PICKUP_NEW = "uspPickupNew";
        private const string USP_PICKUP_UPDATE = "uspPickupUpdate2";
        private const string USP_SCANAUDIT_LOAD = "uspScanAuditFromOrders";
        private const string USP_SCANAUDIT = "uspScanAuditGetList",TBL_SCANAUDIT = "ScanAuditTable";
        private const string USP_SCANAUDIT_UPDATE = "uspScanAuditUpdate";

        private const string USP_COMMODITYCLASSES = "uspCommodityClassGetList",TBL_COMMODITYCLASSES = "CommodityClassTable";
        private const string USP_CUSTOMERS = "uspCustomerGetList",TBL_CUSTOMERS = "CustomerTable";
        private const string USP_DEPOTS = "uspDepotsGetList",TBL_DEPOTS = "DepotTable";
        private const string USP_DRIVERS = "uspDriverGetList",TBL_DRIVERS = "DriverTable";
        private const string USP_ONTIMEISSUES = "uspOnTimeIssueGetList",TBL_ONTIMEISSUES = "OnTimeIssueTable";
        private const string USP_ORDERTYPES = "uspOrderTypeGetList",TBL_ORDERTYPES = "OrderTypeTable";
        private const string USP_UPDATEDBY = "uspUpdatedByGetList",TBL_UPDATEDBY = "UpdatedByTable";

        private const string USP_DELIVPTS_CUSTOMERS = "uspDeliveryPointsCustomerGetList2";

        private const string USP_DISP_CUSTOMERS = "uspDispatchCustomerGetList";
        private const string USP_DISP_CUSTOMERS2 = "uspDispatchCustomerGetList2";
        private const string USP_DISP_DEPOTS = "uspDispatchDepotGetList";
        private const string USP_DISP_DRIVERS = "uspDispatchDriverGetList";

        //Interface
        public RoadshowGateway() { }

        public bool LoadPickups(DateTime pickupDate,string routeClass) {
            //Load pickup data
            bool loaded = false;
            try {
                loaded = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUPS_LOAD,new object[] { pickupDate,routeClass });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return loaded;
        }
        public bool CreatePickup(Pickup pickup) {
            //
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUP_NEW,new object[] { 
                    pickup.RouteDate, pickup.Driver, pickup.RouteName, pickup.ReturnTime, 
                    pickup.CustomerID, pickup.CustomerName, pickup.CustomerType, 
                    pickup.CustomerAddress, pickup.CustomerCity, pickup.CustomerState, pickup.CustomerZip, 
                    pickup.OrderID, pickup.ActualOrderSize, pickup.ActualOrderWeight, 
                    pickup.UnscheduledPickup, pickup.Comments, pickup.OrderType, pickup.ActualCommodity, pickup.Depot
                });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }
        public DataSet ReadPickups(DateTime pickupDate,string routeClass) {
            //
            DataSet pickups = null;
            try {
                pickups = new DataService().FillDataset(SQL_CONNID,USP_PICKUPS,TBL_PICKUPS,new object[] { pickupDate,routeClass });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return pickups;
        }
        public bool UpdatePickup(Pickup pickup) {
            //
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUP_UPDATE,new object[] { 
                    pickup.RecordID, pickup.Driver, pickup.RouteName, 
                    pickup.CustomerID, pickup.CustomerName, pickup.CustomerType, 
                    pickup.CustomerAddress, pickup.CustomerCity, pickup.CustomerState, pickup.CustomerZip, 
                    pickup.ActualOrderSize, pickup.ActualOrderWeight, 
                    pickup.UnscheduledPickup, pickup.Comments, pickup.ActualCommodity 
                });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }

        public bool LoadScanAudits(DateTime routeDate,string routeClass) {
            //Load scan audit data
            bool loaded = false;
            try {
                loaded = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCANAUDIT_LOAD,new object[] { routeDate,routeClass });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return loaded;
        }
        public DataSet ReadScanAudits(DateTime routeDate,string routeClass) {
            //
            DataSet scans = null;
            try {
                scans = new DataService().FillDataset(SQL_CONNID,USP_SCANAUDIT,TBL_SCANAUDIT,new object[] { routeDate,routeClass });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return scans;
        }
        public bool UpdateScanAudit(ScanAudit scan) {
            //
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCANAUDIT_UPDATE,new object[] { 
                    scan.RecordID, 
                    scan.Arrive, scan.Bell, scan.DeliveryStart, scan.DeliveryEnd, scan.Departure, 
                    scan.TimeEntryBy, scan.OnTimeIssue, scan.AdditionalComments });
                ret = true;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }

        public DataSet GetCommodityClasses() {
            //
            DataSet classes = null;
            try {
                classes = new DataService().FillDataset(SQL_CONNID,USP_COMMODITYCLASSES,TBL_COMMODITYCLASSES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return classes;
        }
        public DataSet GetCustomers() {
            //
            DataSet customers = null;
            try {
                customers = new DataService().FillDataset(SQL_CONNID,USP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return customers;
        }
        public DataSet GetDepots() {
            //
            DataSet depots = null;
            try {
                depots = new DataService().FillDataset(SQL_CONNID,USP_DEPOTS,TBL_DEPOTS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return depots;
        }
        public DataSet GetDrivers(string routeClass) {
            //
            DataSet drivers = null;
            try {
                drivers = new DataService().FillDataset(SQL_CONNID,USP_DRIVERS,TBL_DRIVERS,new object[] { routeClass });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return drivers;
        }
        public DataSet GetOnTimeIssues() {
            //
            DataSet issues = null;
            try {
                issues = new DataService().FillDataset(SQL_CONNID,USP_ONTIMEISSUES,TBL_ONTIMEISSUES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return issues;
        }
        public DataSet GetOrderTypes() {
            //
            DataSet types = null;
            try {
                types = new DataService().FillDataset(SQL_CONNID,USP_ORDERTYPES,TBL_ORDERTYPES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return types;
        }
        public DataSet GetUpdateUsers(string routeClass) {
            //
            DataSet users = null;
            try {
                users = new DataService().FillDataset(SQL_CONNID,USP_UPDATEDBY,TBL_UPDATEDBY,new object[] { routeClass });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return users;
        }

        public DataSet GetDeliveryPointsCustomers() {
            //
            DataSet customers = null;
            try {
                customers = new DataService().FillDataset(SQL_CONNID,USP_DELIVPTS_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return customers;
        }

        public DataSet GetDispatchCustomers() {
            //
            DataSet customers = null;
            try {
                customers = new DataService().FillDataset(SQL_CONNID,USP_DISP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return customers;
        }
        public DataSet GetDispatchCustomers2() {
            //
            DataSet customers = null;
            try {
                customers = new DataService().FillDataset(SQL_CONNID,USP_DISP_CUSTOMERS2,TBL_CUSTOMERS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return customers;
        }
        public DataSet GetDispatchDepots() {
            //
            DataSet depots = null;
            try {
                depots = new DataService().FillDataset(SQL_CONNID,USP_DISP_DEPOTS,TBL_DEPOTS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return depots;
        }
        public DataSet GetDispatchDrivers(int depotNumber) {
            //
            DataSet drivers = null;
            try {
                drivers = new DataService().FillDataset(SQL_CONNID,USP_DISP_DRIVERS,TBL_DRIVERS,new object[] { depotNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return drivers;
        }
    }
}
