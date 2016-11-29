using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.ServiceModel;

namespace Argix.Terminals {
    //
    public class TerminalsGateway {
        //Members
        private static bool _state = false;
        private static string _address = "";
         
        //Interface
        static TerminalsGateway() {
            //
            RSReportsServiceClient client = new RSReportsServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private TerminalsGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static bool LoadPickups(DateTime pickupDate,string routeClass) {
            //Load pickup data
            bool loaded = false;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                loaded = client.LoadPickups(pickupDate,routeClass);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return loaded;
        }
        public static bool CanLoadPickups(DateTime pickupDate,string routeClass) {
            bool ret = false;
            if(DateTime.Compare(pickupDate,DateTime.Today) <= 0) {
                RSReportsDataset pickups = GetPickups(pickupDate,routeClass);
                ret = pickups.PickupTable.Rows.Count == 0;
            }
            return ret;
        }
        public static RSReportsDataset GetPickups(DateTime pickupDate,string routeClass) {
            //
            RSReportsDataset pickups = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.ReadPickups(pickupDate,routeClass);
                if(ds!=null) pickups.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return pickups;
        }
        public static bool AddPickup(RSReportsDataset.PickupTableRow pickup) {
            //
            bool added=false;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                Pickup p = new Pickup();
                p.RouteDate = !pickup.IsRt_DateNull() ? pickup.Rt_Date : DateTime.MinValue;
                p.Driver = !pickup.IsDriverNull() ? pickup.Driver : "";
                p.RouteName = !pickup.IsRt_NameNull() ? pickup.Rt_Name : "";
                p.ReturnTime = !pickup.IsRetnTimeNull() ? pickup.RetnTime : (short)0;
                p.CustomerID = !pickup.IsCustomer_IDNull() ? pickup.Customer_ID : "";
                p.CustomerName = !pickup.IsCustomerNameNull() ? pickup.CustomerName : "";
                p.CustomerType = !pickup.IsCustTypeNull() ? pickup.CustType : "";
                p.CustomerAddress = !pickup.IsAddressNull() ? pickup.Address : "";
                p.CustomerCity = !pickup.IsCityNull() ? pickup.City : "";
                p.CustomerState = !pickup.IsStateNull() ? pickup.State : "";
                p.CustomerZip = !pickup.IsZipNull() ? pickup.Zip : "";
                p.OrderID = !pickup.IsOrderIDNull() ? pickup.OrderID : "";
                p.ActualOrderSize = !pickup.IsActOrdSizeNull() ? pickup.ActOrdSize : 0;
                p.ActualOrderWeight = !pickup.IsActOrdLbsNull() ? pickup.ActOrdLbs : 0;
                p.UnscheduledPickup = !pickup.IsUnsched_PUNull() ? pickup.Unsched_PU : "";
                p.Comments = !pickup.IsCommentsNull() ? pickup.Comments : "";
                p.OrderType = !pickup.IsOrdTypNull() ? pickup.OrdTyp : "";
                p.ActualCommodity = !pickup.IsActCmdtyNull() ? pickup.ActCmdty : "";
                p.Depot = !pickup.IsDepotNull() ? pickup.Depot : "";
                added = client.AddPickup(p);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public static bool UpdatePickups(RSReportsDataset pickups) {
            //
            bool ret = false;
            if(pickups.HasChanges()) {
                RSReportsDataset _pickups = (RSReportsDataset)pickups.GetChanges(DataRowState.Modified);
                for(int i = 0; i < _pickups.PickupTable.Rows.Count; i++) {
                    RSReportsDataset.PickupTableRow pickup = _pickups.PickupTable[i];
                    bool changed = UpdatePickup(pickup);
                }
                pickups.AcceptChanges();
            }
            ret = true;
            return ret;
        }
        public static bool UpdatePickup(RSReportsDataset.PickupTableRow pickup) {
            //
            bool updated=false;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                Pickup p = new Pickup();
                p.RecordID = !pickup.IsRecordIDNull() ? pickup.RecordID : 0;
                p.Driver = !pickup.IsDriverNull() ? pickup.Driver : "";
                p.RouteName = !pickup.IsRt_NameNull() ? pickup.Rt_Name : "";
                p.CustomerID = !pickup.IsCustomer_IDNull() ? pickup.Customer_ID : "";
                p.CustomerName = !pickup.IsCustomerNameNull() ? pickup.CustomerName : "";
                p.CustomerType = !pickup.IsCustTypeNull() ? pickup.CustType : "";
                p.CustomerAddress = !pickup.IsAddressNull() ? pickup.Address : "";
                p.CustomerCity = !pickup.IsCityNull() ? pickup.City : "";
                p.CustomerState = !pickup.IsStateNull() ? pickup.State : "";
                p.CustomerZip = !pickup.IsZipNull() ? pickup.Zip : "";
                p.ActualOrderSize = !pickup.IsActOrdSizeNull() ? pickup.ActOrdSize : 0;
                p.ActualOrderWeight = !pickup.IsActOrdLbsNull() ? pickup.ActOrdLbs : 0;
                p.UnscheduledPickup = !pickup.IsUnsched_PUNull() ? pickup.Unsched_PU : "";
                p.Comments = !pickup.IsCommentsNull() ? pickup.Comments : "";
                p.ActualCommodity = !pickup.IsActCmdtyNull() ? pickup.ActCmdty : "";
                updated = client.UpdatePickup(p);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public static bool LoadScanAudits(DateTime routeDate,string routeClass) {
            //Load scan audit data
            bool loaded = false;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                loaded = client.LoadScanAudits(routeDate,routeClass);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return loaded;
        }
        public static bool CanLoadScanAudit(DateTime routeDate,string routeClass) {
            bool ret=false;
            if(DateTime.Compare(routeDate,DateTime.Today) < 0) {
                RSReportsDataset scans = GetScanAudits(routeDate,routeClass);
                ret = scans.ScanAuditTable.Rows.Count == 0;
            }
            return ret; 
        }
        public static RSReportsDataset GetScanAudits(DateTime routeDate,string routeClass) {
            //
            RSReportsDataset scans = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.ReadScanAudits(routeDate,routeClass);
                if(ds!=null) scans.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return scans;
        }
        public static RSReportsDataset GetScanAudits(DateTime routeDate,string routeClass, string driverName) {
            //
            RSReportsDataset scans=null;
            try {
                scans = new RSReportsDataset();
                RSReportsDataset _scans = GetScanAudits(routeDate,routeClass);
                if(driverName != "All")
                    scans.Merge(_scans.ScanAuditTable.Select("Driver ='" + driverName + "'"));
                else
                    scans.Merge(_scans);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return scans;
        }
        public static RSReportsDataset GetScanAuditDrivers(DateTime routeDate,string routeClass) {
            //
            RSReportsDataset drivers = new RSReportsDataset();
            try {
                RSReportsDataset _scans = GetScanAudits(routeDate,routeClass);
                for(int i=0;i<_scans.ScanAuditTable.Rows.Count;i++) {
                    string driver = _scans.ScanAuditTable[i].Driver;
                    if(drivers.DriverTable.Select("NAME='" + driver + "'").Length == 0)
                        drivers.DriverTable.AddDriverTableRow(driver,"",0);
                }
                drivers.AcceptChanges();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return drivers;
        }
        public static bool UpdateScanAudits(RSReportsDataset scans) {
            //
            bool ret=false;
            if(scans.HasChanges()) {
                RSReportsDataset _scans = (RSReportsDataset)scans.GetChanges(DataRowState.Modified);
                for(int i=0;i<_scans.ScanAuditTable.Rows.Count;i++) {
                    RSReportsDataset.ScanAuditTableRow scan = _scans.ScanAuditTable[i];
                    bool changed = UpdateScanAudit(scan);
                }
                scans.AcceptChanges();
            }
            ret = true;
            return ret;
        }
        public static bool UpdateScanAudit(RSReportsDataset.ScanAuditTableRow audit) {
            //
            bool updated = false;
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                ScanAudit sa = new ScanAudit();
                sa.RecordID = audit.RecordID;
                sa.Arrive = !audit.IsArriveNull() ? audit.Arrive : "";
                sa.Bell = !audit.IsBellNull() ? audit.Bell : "";
                sa.DeliveryStart = !audit.IsDelStartNull() ? audit.DelStart : "";
                sa.DeliveryEnd = !audit.IsDelEndNull() ? audit.DelEnd : "";
                sa.Departure = !audit.IsDepartNull() ? audit.Depart : "";
                sa.TimeEntryBy = !audit.IsTimeEntryByNull() ? audit.TimeEntryBy : "";
                sa.OnTimeIssue = !audit.IsOnTimeIssueNull() ? audit.OnTimeIssue : "";
                sa.AdditionalComments = !audit.IsAdditCommentsNull() ? audit.AdditComments : "";
                updated = client.UpdateScanAudit(sa);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public static RSReportsDataset GetCommodityClasses() {
            //
            RSReportsDataset classes = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetCommodityClasses();
                if (ds != null) classes.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return classes;
        }
        public static RSReportsDataset GetCustomers() {
            //
            RSReportsDataset customers = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetCustomers();
                if (ds != null) customers.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return customers;
        }
        public static RSReportsDataset GetDepots(string terminalCode) {
            //
            RSReportsDataset depots = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetDepots();
                if (ds != null) {
                    depots.Merge(ds);
                    for (int i = 0;i < depots.DepotTable.Rows.Count;i++) {
                        string orderClass = depots.DepotTable[i].RS_OrderClass;
                        if (!(terminalCode.Length == 0 || orderClass == terminalCode))
                            depots.DepotTable[i].Delete();
                    }
                    depots.DepotTable.AcceptChanges();
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return depots;
        }
        public static RSReportsDataset GetDrivers(string routeClass) {
            //
            RSReportsDataset drivers = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetDrivers(routeClass);
                if(ds!=null) drivers.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return drivers;
        }
        public static RSReportsDataset GetOnTimeIssues() {
            //
            RSReportsDataset issues = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetOnTimeIssues();
                if (ds != null) issues.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public static RSReportsDataset GetOrderTypes() {
            //
            RSReportsDataset types = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetOrderTypes();
                if(ds!=null) types.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return types;
        }
        public static RSReportsDataset GetUpdateUsers(string routeClass) {
            //
            RSReportsDataset users = new RSReportsDataset();
            RSReportsServiceClient client = new RSReportsServiceClient();
            try {
                DataSet ds = client.GetUpdateUsers(routeClass);
                if(ds!=null) users.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return users;
        }
    }
}
