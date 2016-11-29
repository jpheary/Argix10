using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Web.Configuration;
using System.Web.Security;
using Argix.Enterprise;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class DispatchService:IDispatchService {
        //Members

        //Interface
        public DispatchService() { }

        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(DispatchGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(DispatchGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet ViewPickupLog(DateTime start,DateTime end) {
            DataSet ds=null;
            try {
                ds = new DispatchGateway().ReadPickupLog(start,end);
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewPickupLogTemplates() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadPickupLogTemplates();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand, Role = "Client Rep")]
        [PrincipalPermission(SecurityAction.Demand, Role = "BBB Clerk")]
        public bool AddPickupRequest(PickupRequest request) {
            //Add a new pickup request
            bool added=false;
            try {
                //Apply simple business rules
                
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    added = new DispatchGateway().InsertPickupRequest(request);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return added;
        }
        public PickupRequest ReadPickup(int requestID) {
            //Read an existing pickup request
            PickupRequest request = null;
            try {
                //
                DataSet ds = new DispatchGateway().GetPickupRequest(requestID);
                if (ds != null) {
                    DispatchDataset _request = new DispatchDataset();
                    _request.Merge(ds);
                    if (_request.PickupLogTable.Rows.Count > 0) request = new PickupRequest(_request.PickupLogTable[0]);
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return request;
        }       
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Client Rep")]
        [PrincipalPermission(SecurityAction.Demand, Role = "BBB Clerk")]
        public bool ChangePickupRequest(PickupRequest request) {
            //Change an existing pickup request          
            bool changed=false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    //Get the pickups current state
                    PickupRequest pickup = ReadPickup(request.RequestID);

                    //Update the pickup
                    changed = new DispatchGateway().UpdatePickupRequest(request);

                    //Arrive/unarrive LTL shipment (if applicable); see if the actual pickup date is now being changed
                    if (pickup != null && pickup.CreateUserID == "PSP" && (pickup.ActualPickup == null || pickup.ActualPickup == DateTime.MinValue) && (request.ActualPickup != null && request.ActualPickup > DateTime.MinValue)) {
                        //This is an LTL pickup being arrived
                        new LTLGateway2().ArriveLTLShipment(request.RequestID, request.ActualPickup);
                    }
                    else if(pickup != null && pickup.CreateUserID == "PSP" && (pickup.ActualPickup != null && pickup.ActualPickup > DateTime.MinValue) && (request.ActualPickup == null || request.ActualPickup == DateTime.MinValue)) {
                        //This is an LTL pickup being unarrived
                        new LTLGateway2().ArriveLTLShipment(request.RequestID, DateTime.MinValue);
                    }
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        public bool VerifyPickupRequest(int requestID, string shipperNumber, string shipper, string shipperAddress, string shipperPhone, int windowOpen, int windowClose, string driverName, DateTime actual, string orderType, DateTime lastUpdated, string userID) {
            //Change an existing order detail item            
            bool changed=false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdatePickupRequest(requestID,shipperNumber,shipper,shipperAddress,shipperPhone,windowOpen,windowClose,driverName,actual,orderType,lastUpdated,userID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        public bool AssignDriverToPickupRequest(int requestID,string driverName,DateTime lastUpdated,string userID) {
            //Change an existing order detail item            
            bool changed=false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdatePickupRequest(requestID,driverName,lastUpdated,userID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        public bool ArrivePickupRequest(int requestID,string driverName,DateTime actual,string orderType,DateTime lastUpdated,string userID) {
            //Change an existing order detail item            
            bool changed=false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdatePickupRequest(requestID,driverName,actual,orderType,lastUpdated,userID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool CancelPickupRequest(int requestID,DateTime cancelled,string cancelledUserID) {
            //Delete an existing pickup requesst
            bool removed=false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelPickupRequest(requestID,cancelled,cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }


        public DataSet ViewClientInboundSchedule(DateTime start,DateTime end) {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadClientInboundSchedule(start,end);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewClientInboundScheduleTemplates() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadClientInboundScheduleTemplates();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool AddClientInboundFreight(ClientInboundFreight freight) {
            //Add a new client inbound freight
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new DispatchGateway().InsertClientInboundFreight(freight);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return added;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        public bool ChangeClientInboundFreight(ClientInboundFreight freight) {
            //Change an existing client inbound freight      
            bool changed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Update appointment
                    changed = new DispatchGateway().UpdateClientInboundFreight(freight);

                    //Blog a notification when appointments arrive
                    if (freight.ActualArrival > DateTime.MinValue) {
                        BlogEntry entry = new BlogEntry();
                        entry.Date = DateTime.Now;
                        entry.Comment = freight.VendorName + " appt# " + freight.ID.ToString() + " arrived " + freight.ActualArrival.ToString("HH:mm tt") + ", TDS# " + freight.TDSNumber;
                        entry.UserID = "Dispatch";
                        AddBlogEntry(entry);
                    }

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool CancelClientInboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //Delete an existing client inbound freight
            bool removed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelClientInboundFreight(id,cancelled,cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }


        public DataSet ViewInboundSchedule(DateTime start,DateTime end) {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadInboundSchedule(start,end);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewInboundScheduleTemplates() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadInboundScheduleTemplates();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool AddScheduledInboundFreight(InboundFreight freight) {
            //Add a new inbound freight
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new DispatchGateway().InsertInboundFreight(freight);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return added;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        public bool ChangeScheduledInboundFreight(InboundFreight freight) {
            //Change an existing inbound freight        
            bool changed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdateInboundFreight(freight);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool CancelScheduledInboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //Delete an existing inbound freight
            bool removed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelInboundFreight(id,cancelled,cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }


        public DataSet ViewOutboundSchedule(DateTime start,DateTime end) {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadOutboundSchedule(start,end);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewOutboundScheduleTemplates() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadOutboundScheduleTemplates();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool AddScheduledOutboundFreight(OutboundFreight freight) {
            //Add a new outbound freight
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new DispatchGateway().InsertOutboundFreight(freight);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return added;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        public bool ChangeScheduledOutboundFreight(OutboundFreight freight) {
            //Change an existing outbound freight       
            bool changed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdateOutboundFreight(freight);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool CancelScheduledOutboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //Delete an existing outbound freight
            bool removed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelOutboundFreight(id,cancelled,cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }

        public DataSet ViewTrailerLog(DateTime start,DateTime end) {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadTrailerLog(start,end);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewTrailerLogYardCheck() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadTrailerLogYardCheck();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ViewTrailerLogArchive() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadTrailerLogArchive();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet SearchTrailerLog(string trailerNumber) {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().SearchTrailerLog(trailerNumber);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Safety Supervisor")]
        public bool AddTrailerEntry(TrailerEntry entry) {
            //Add a new trailer log entry
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new DispatchGateway().InsertTrailerEntry(entry);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return added;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Window Clerk")]
        [PrincipalPermission(SecurityAction.Demand,Role = "Safety Supervisor")]
        public bool ChangeTrailerEntry(TrailerEntry entry) {
            //Change an existing trailer log entry           
            bool changed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdateTrailerEntry(entry);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return changed;
        }
        [PrincipalPermission(SecurityAction.Demand,Role = "Dispatch Supervisor")]
        public bool CancelTrailerEntry(int id,DateTime cancelled,string cancelledUserID) {
            //Delete an existing trailer log entry
            bool removed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelTrailerEntry(id,cancelled,cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }

        public DataSet ViewLoadTenderLog(DateTime start, DateTime end) {
            //View the load tender log
            DataSet log = null;
            try {
                log = new DispatchGateway().ViewLoadTenderLog(start, end);
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return log;
        }
        public int CreateLoadTenderEntry(LoadTenderEntry entry) {
            //Create a new load tender
            int id = 0;
            try {
                //Apply simple business rules (if applicable)


                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the LoadTenderQuote
                    id = new DispatchGateway().CreateLoadTenderEntry(entry);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return id;
        }
        public LoadTenderEntry ReadLoadTenderEntry(int id) {
            //Read an existing load tender
            LoadTenderEntry entry = new LoadTenderEntry();
            try {
                DataSet ds = new DispatchGateway().ReadLoadTenderEntry(id);
                if(ds != null && ds.Tables["LoadTenderLogTable"] != null && ds.Tables["LoadTenderLogTable"].Rows.Count > 0) {
                    DataRow _entry = ds.Tables["LoadTenderLogTable"].Rows[0];
                    #region Set fields
                    entry.ID = int.Parse(_entry["ID"].ToString());
                    entry.Created = DateTime.Parse(_entry["Created"].ToString());
                    entry.CreateUserID = _entry["CreateUserID"].ToString();
                    entry.ScheduleDate = DateTime.Parse(_entry["ScheduleDate"].ToString());
                    entry.ClientNumber = _entry["ClientNumber"].ToString();
                    entry.Client = _entry["Client"].ToString();
                    entry.VendorNumber = _entry["VendorNumber"].ToString();
                    entry.VendorName = _entry["VendorName"].ToString();
                    entry.VendorAddressLine1 = _entry["VendorAddressLine1"].ToString();
                    entry.VendorAddressLine2 = !_entry.IsNull("VendorAddressLine2") ? _entry["VendorAddressLine2"].ToString() : "";
                    entry.VendorCity = _entry["VendorCity"].ToString();
                    entry.VendorState = _entry["VendorState"].ToString();
                    entry.VendorZip = _entry["VendorZip"].ToString();
                    entry.VendorZip4 = !_entry.IsNull("VendorZip4") ? _entry["VendorZip4"].ToString() : "";
                    entry.ContactName = !_entry.IsNull("ContactName") ? _entry["ContactName"].ToString() : "";
                    entry.ContactPhone = !_entry.IsNull("ContactPhone") ? _entry["ContactPhone"].ToString() : "";
                    entry.ContactEmail = !_entry.IsNull("ContactEmail") ? _entry["ContactEmail"].ToString() : "";
                    entry.WindowOpen = !_entry.IsNull("WindowOpen") ? int.Parse(_entry["WindowOpen"].ToString()) : 0;
                    entry.WindowClose = !_entry.IsNull("WindowClose") ? int.Parse(_entry["WindowClose"].ToString()) : 0;
                    entry.Description = !_entry.IsNull("Description") ? _entry["Description"].ToString() : "";
                    entry.Amount = !_entry.IsNull("Amount") ? int.Parse(_entry["Amount"].ToString()) : 0;
                    entry.AmountType = !_entry.IsNull("AmountType") ? _entry["AmountType"].ToString() : "";
                    entry.Weight = !_entry.IsNull("Weight") ? int.Parse(_entry["Weight"].ToString()) : 0;
                    entry.IsFullTrailer = !_entry.IsNull("IsFullTrailer") ? bool.Parse(_entry["IsFullTrailer"].ToString()) : false;
                    entry.LoadTenderNumber = !_entry.IsNull("LoadTenderNumber") ? int.Parse(_entry["LoadTenderNumber"].ToString()) : 0;
                    entry.PickupNumber = !_entry.IsNull("PickupNumber") ? int.Parse(_entry["PickupNumber"].ToString()) : 0;
                    entry.Cancelled = !_entry.IsNull("Cancelled") ? DateTime.Parse(_entry["Cancelled"].ToString()) : DateTime.MinValue;
                    entry.CancelledUserID = !_entry.IsNull("CancelledBy") ? _entry["CancelledBy"].ToString() : "";
                    entry.Comments = !_entry.IsNull("Comments") ? _entry["Comments"].ToString() : "";
                    entry.LastUpdated = !_entry.IsNull("LastUpdated") ? DateTime.Parse(_entry["LastUpdated"].ToString()) : DateTime.MinValue;
                    entry.UserID = !_entry.IsNull("UserID") ? _entry["UserID"].ToString() : "";
                    #endregion
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return entry;
        }
        public bool UpdateLoadTenderEntry(LoadTenderEntry entry) {
            //UPdate an existing load tender
            bool updated = false;
            try {
                updated = new DispatchGateway().UpdateLoadTenderEntry(entry);
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return updated;
        }
        public bool TenderLoadTenderEntry(int entryID, LoadTender loadTender) {
            //Attach the load tender file to the load tender
            bool result = false;
            try {
                //Apply simple business rules

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the load tender
                    int loadTenderNumber = new DispatchGateway().CreateLoadTender(loadTender.Filename, loadTender.File);

                    //Update the load tender entry with the load tender file number
                    result = new DispatchGateway().TenderLoadTenderEntry(entryID, loadTenderNumber);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return result;
        }
        public bool ScheduleLoadTenderEntry(int entryID, BBBTrip trip) {
            //Schedule a pickup (record pickup number) for the load tender
            bool result = false;
            try {
                //Apply simple business rules (if applicable)


                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the pickup appointment
                    int pickupNumber = new DispatchGateway().CreateBBBTrip(trip);

                    //Update the load tender as scheduled for pickup
                    result = new DispatchGateway().ScheduleLoadTenderEntry(entryID, pickupNumber);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return result;
        }
        public bool ScheduleLoadTenderEntry(int entryID, PickupRequest request) {
            //Schedule a pickup (record pickup number) for the load tender
            bool result = false;
            try {
                //Apply simple business rules (if applicable)


                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the pickup request
                    int pickupNumber = new DispatchGateway().InsertPickupRequest3(request);

                    //Update the load tender as scheduled for pickup
                    result = new DispatchGateway().ScheduleLoadTenderEntry(entryID, pickupNumber);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return result;
        }
        public bool CancelLoadTenderEntry(int entryID, DateTime cancelled, string cancelledBy) {
            //
            bool result = false;
            try {
                //Apply simple business rules
                LoadTenderEntry entry = ReadLoadTenderEntry(entryID);
                if(entry.PickupNumber > 0) {
                    //Get the pickup appointment or request and check if picked up
                    if(entry.PickupNumber.ToString().Substring(0, 1) == "1") {
                        ClientInboundFreight appt = null;
                        if(appt != null && appt.ActualArrival != DateTime.MinValue) throw new ApplicationException("This load tender has already been picked up.");
                    }
                    else {
                        PickupRequest pickup = null;
                        if(pickup != null && pickup.ActualPickup != DateTime.MinValue) throw new ApplicationException("This load tender has already been picked up.");
                    }
                }

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    if(entry.PickupNumber > 0) {
                        //Get the pickup appointment or request and check if picked up
                        if(entry.PickupNumber.ToString().Substring(0, 1) == "1") {
                            //Cancel the pickup appointment
                            new DispatchGateway().CancelClientInboundFreight(entry.PickupNumber, cancelled, cancelledBy);
                        }
                        else {
                            //Cancel the pickup request
                            new DispatchGateway().CancelPickupRequest(entry.PickupNumber, cancelled, cancelledBy);
                        }
                    }

                    //Cancel the load tender entry
                    result = new DispatchGateway().CancelLoadTenderEntry(entryID, cancelled, cancelledBy);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return result;
        }
        public LoadTender GetLoadTender(int number) {
            //Get an existing file attachment from database
            LoadTender loadTender = null;
            try {
                DataSet ds = new DispatchGateway().GetLoadTender(number);
                if(ds != null && ds.Tables["LoadTenderTable"] != null && ds.Tables["LoadTenderTable"].Rows.Count > 0) {
                    loadTender = new LoadTender();
                    loadTender.Number = number;
                    loadTender.Filename = (string)ds.Tables["LoadTenderTable"].Rows[0]["FileName"];
                    loadTender.File = (byte[])ds.Tables["LoadTenderTable"].Rows[0]["File"];
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return loadTender;
        }

        public DataSet ViewBBBSchedule(DateTime start, DateTime end) {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ViewBBBSchedule(start, end);
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return ds;
        }
        public int AddBBBScheduleTrip(BBBTrip trip) {
            //Add a new inbound freight
            int id = 0;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    id = new DispatchGateway().CreateBBBTrip(trip);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return id;
        }
        public bool ChangeBBBScheduleTrip(BBBTrip trip) {
            //Change an existing inbound freight        
            bool changed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    changed = new DispatchGateway().UpdateBBBTrip(trip);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return changed;
        }
        public bool CancelBBBScheduleTrip(int id, DateTime cancelled, string cancelledUserID) {
            //Delete an existing inbound freight
            bool removed = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelBBBTrip(id, cancelled, cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message), "Service Error"); }
            return removed;
        }

        public DataSet GetAppointmentTypes() {
            //
            DataSet types = new DataSet();
            try {
                types = new DispatchGateway().GetAppointmentTypes();
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return types;
        }
        public DataSet GetFreghtDesginationTypes() {
            //
            DataSet types = new DataSet();
            try {
                types = new DispatchGateway().GetFreghtDesginationTypes();
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return types;
        }

        public DataSet ViewBlog() {
            DataSet ds = null;
            try {
                ds = new DispatchGateway().ReadBlog();
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return ds;
        }
        public bool AddBlogEntry(BlogEntry entry) {
            //Add a new blog entry
            bool added = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    added = new DispatchGateway().InsertBlogEntry(entry);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return added;
        }

        public DataSet GetClients() {
            //Returns a list of clients
            DataSet clients = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetClients();
                if (ds != null) clients.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return clients;
        }
        public DataSet GetAgents() {
            //Returns a list of agents
            DataSet agents = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetAgents();
                if (ds != null) agents.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return agents;
        }
        public DataSet GetTerminals() {
            //Returns a list of terminals
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetTerminals();
                if (ds != null) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return terminals;
        }
        public DataSet GetCarriers() {
            //Returns a list of carriers
            DataSet carriers = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetCarriers();
                if (ds != null) carriers.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return carriers;
        }
        public DataSet GetDrivers() {
            //Returns a list of drivers
            DataSet drivers = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetDrivers();
                if (ds != null) drivers.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return drivers;
        }
        public DataSet GetLocations() {
            //Returns a list of locations
            DataSet locations = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetLocations();
                if (ds != null) locations.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message),"Service Error"); }
            return locations;
        }
        public DataSet GetVendors() {
            //Returns a list of vendors
            DataSet vendors = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetVendors();
                if(ds != null) vendors.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<EnterpriseFault>(new EnterpriseFault(ex.Message), "Service Error"); }
            return vendors;
        }
    }
}
