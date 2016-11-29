using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Transactions;
using System.Text;
using Argix.Enterprise;

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class RSReportsService:IRSReportsService {
        //Members

        //Interface
        public RSReportsService() { }

        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(RoadshowGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(RoadshowGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public bool LoadPickups(DateTime pickupDate,string routeClass) {
            //Load pickup data
            bool loaded = false;
            try {
                //Apply simple business rules
                //Load pickups for today or a prior day
                if (pickupDate.CompareTo(DateTime.Today) > 0) throw new ApplicationException("Pickups cannot be loaded for future dates.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Load pickups if they haven't been loaded yet
                    DataSet pickups = ReadPickups(pickupDate,routeClass);
                    if(pickups.Tables["PickupTable"].Rows.Count == 0)
                        loaded = new RoadshowGateway().LoadPickups(pickupDate,routeClass);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return loaded;
        }
        public DataSet ReadPickups(DateTime pickupDate,string routeClass) {
            //
            DataSet pickups = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().ReadPickups(pickupDate,routeClass);
                if (ds != null) pickups.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return pickups;
        }
        public bool AddPickup(Pickup pickup) {
            //
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new RoadshowGateway().CreatePickup(pickup);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return added;
        }
        public void UpdatePickups(Pickups pickups) {
            //
            for (int i = 0;i < pickups.Count;i++) UpdatePickup(pickups[i]);
        }
        public bool UpdatePickup(Pickup pickup) {
            //
            bool updated = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new RoadshowGateway().UpdatePickup(pickup);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return updated;
        }

        public bool LoadScanAudits(DateTime routeDate,string routeClass) {
            //Load scan audit data
            bool loaded = false;
            try {
                //Apply simple business rules
                //Load pickups for today or a prior day
                if (routeDate.CompareTo(DateTime.Today) >= 0) throw new ApplicationException("Scan audits cannot be loaded for today or future dates.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Load pickups if they haven't been loaded yet
                    DataSet scans = ReadScanAudits(routeDate,routeClass);
                    if (scans.Tables["ScanAuditTable"].Rows.Count == 0)
                        loaded = new RoadshowGateway().LoadScanAudits(routeDate,routeClass);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return loaded;
        }
        public DataSet ReadScanAudits(DateTime routeDate,string routeClass) {
            //
            DataSet scans = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().ReadScanAudits(routeDate,routeClass);
                if (ds != null) scans.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return scans;
        }
        public void UpdateScanAudits(ScanAudits audits) {
            //
            for (int i = 0;i < audits.Count;i++) UpdateScanAudit(audits[i]);
        }
        public bool UpdateScanAudit(ScanAudit audit) {
            //
            bool updated = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new RoadshowGateway().UpdateScanAudit(audit);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return updated;
        }

        public DataSet GetCommodityClasses() {
            //
            DataSet classes = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetCommodityClasses();
                if (ds != null) classes.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return classes;
        }
        public DataSet GetCustomers() {
            //
            DataSet customers = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetCustomers();
                if (ds != null) customers.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return customers;
        }
        public DataSet GetDepots() {
            //
            DataSet depots = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetDepots();
                if (ds != null) depots.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return depots;
        }
        public DataSet GetDrivers(string routeClass) {
            //
            DataSet drivers = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetDrivers(routeClass);
                if (ds != null) drivers.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return drivers;
        }
        public DataSet GetOnTimeIssues() {
            //
            DataSet issues = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetOnTimeIssues();
                if (ds != null) issues.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return issues;
        }
        public DataSet GetOrderTypes() {
            //
            DataSet types = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetOrderTypes();
                if (ds != null) types.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return types;
        }
        public DataSet GetUpdateUsers(string routeClass) {
            //
            DataSet users = new DataSet();
            try {
                DataSet ds = new RoadshowGateway().GetUpdateUsers(routeClass);
                if (ds != null) users.Merge(ds);
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return users;
        }
    }
}
