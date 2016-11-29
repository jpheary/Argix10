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
using System.Web.Security;
using Argix.Enterprise;

namespace Argix.Freight {
    //FreightSystemService for system to system services
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class FreightSystemService:IDispatchSystemService, ILTLSystemService {
        //Members

        //Interface
        public FreightSystemService() { }

        //Dispatch services
        public int SchedulePickupRequest(PickupRequest request) {
            //Add a new pickup request
            int id = 0;
            try {
                id = new DispatchGateway().InsertPickupRequest3(request);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return id;
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

                updated = new DispatchGateway().UpdatePickupRequest(pickup);
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

                removed = new DispatchGateway().CancelPickupRequest(requestID,cancelled,cancelledUserID);
            }
            catch (Exception ex) { throw new FaultException<DispatchFault>(new DispatchFault(ex.Message),"Service Error"); }
            return removed;
        }

        //LTL services
        public void DispatchShipment(LTLShipment shipment) {
            //Dispatch an existing LTL shipment (i.e. record pickupID
            try {
                new LTLGateway().DispatchLTLShipment(shipment);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
        }
        public void ArriveShipment(LTLShipment shipment) {
            //Arrive an existing LTL shipment (i.e. record arrival datetime)
            try {
                new LTLGateway().ArriveLTLShipment(shipment);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
        }
    }
}
