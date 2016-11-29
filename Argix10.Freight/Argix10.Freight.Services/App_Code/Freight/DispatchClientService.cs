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
    public class DispatchClientService:IDispatchClientService {
        //Members

        //Interface
        public DispatchClientService() { }

        public DataSet ViewPickupLog(string clientNumber) {
            DataSet pickups = new DataSet();
            try {
                int back = int.Parse(WebConfigurationManager.AppSettings["ClientPickupsViewDaysBack"].ToString());
                int forward = int.Parse(WebConfigurationManager.AppSettings["ClientPickupsViewDaysForward"].ToString());
                DataSet ds = new DispatchGateway().ReadPickupLogByClient(DateTime.Today.AddDays(-back), DateTime.Today.AddDays(forward), clientNumber);
                if (ds != null) pickups.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return pickups;
        }
        public int RequestPickup(PickupRequest pickup) {
            //Add a new pickup request
            int pickupID = 0;
            try {
                //Apply simple business rules
                //Validate schedule date > today
                if (pickup.ScheduleDate.CompareTo(DateTime.Today) <= 0) throw new ApplicationException("Schedule date must be next day or later.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Enforce identification of LTL pickups (needed for BizTalk integration)
                    pickup.CreateUserID = "PSP";
                    pickupID = new DispatchGateway().InsertPickupRequest3(pickup);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return pickupID;
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
        public bool UpdatePickup(PickupRequest pickup) {
            //Update an existing pickup requesst
            bool updated = false;
            try {
                //Apply simple business rules
                //Cannot cancel if arrived or if day of pickup
                PickupRequest _pickup = ReadPickup(pickup.RequestID);
                if (_pickup.ScheduleDate.CompareTo(DateTime.Today) <= 0) throw new ApplicationException("Pickups must be updated at least one day before the scheduled pickup date.");
                if (_pickup.ActualPickup > DateTime.MinValue) throw new ApplicationException("This pickup has already been arrived.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new DispatchGateway().UpdatePickupRequest(pickup);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return updated;
        }
        public bool CancelPickup(int requestID,DateTime cancelled,string cancelledUserID) {
            //Cancel an existing pickup requesst
            bool removed = false;
            try {
                //Apply simple business rules
                //Cannot cancel if arrived or if day of pickup
                PickupRequest pickup = ReadPickup(requestID);
                if (pickup.ScheduleDate.CompareTo(DateTime.Today) <= 0) throw new ApplicationException("Pickups must be cancelled at least one day before the scheduled pickup date.");
                if (pickup.ActualPickup > DateTime.MinValue) throw new ApplicationException("This pickup has already been arrived.");
                
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    removed = new DispatchGateway().CancelPickupRequest(requestID,cancelled,cancelledUserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }
    }
}
