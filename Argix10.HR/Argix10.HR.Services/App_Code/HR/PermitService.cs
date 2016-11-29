using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Transactions;
using Argix.Enterprise;

namespace Argix.HR {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class PermitService : IPermitService, IPermitAdminService, IPermitSearchService {
        //Members

        //Interface
        public PermitService() { }

        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(EnterpriseGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(EnterpriseGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet ViewPermits() {
            //
            DataSet permits = new DataSet();
            try {
                DataSet ds = new PermitGateway().ViewPermits();
                if(ds != null) permits.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permits;
        }
        public Permit ReadPermit(int id) {
            //Read an existing permit
            Permit permit = null;
            try {
                DataSet ds = new PermitGateway().ReadPermit(id);
                if(ds != null && ds.Tables["PermitTable"] != null && ds.Tables["PermitTable"].Rows.Count > 0) {
                    permit = new Permit(ds.Tables["PermitTable"].Rows[0]);
                }
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permit;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public int RegisterPermit(Permit permit) { 
            //Register a new vehicle (create a new permit)
            int id = 0;
            try {
                //Apply simple business rules
                Permit p = ValidatePermitNumber(permit.Number);
                if(p != null) throw new ApplicationException("There is an existing permit in the system for permit # " + permit.Number + ".");

                p = ValidateVehicle(permit.Vehicle.IssueState, permit.Vehicle.PlateNumber);
                if(p != null) throw new ApplicationException("There is an active permit in the system with license plate " + permit.Vehicle.IssueState + " " + permit.Vehicle.PlateNumber + ".");

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Creaet the vehicle, then the permit
                    permit.VehicleID = new PermitGateway().CreateVehicle(permit.Vehicle);
                    id = new PermitGateway().CreatePermit(permit);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return id;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public int ReplacePermit(Permit permit, string newNumber) { 
            //Replace a lost/stolen/damaged permit
            int id = 0;
            try {
                //Apply simple business rules
                Permit p = ValidatePermitNumber(newNumber);
                if(p != null) throw new ApplicationException("There is an existing permit in the system for permit # " + newNumber + ".");

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Inactivate the current one and create a new one
                    //Current permit should have Inactivated, InactivatedBy, and InactivatedReason set
                    bool inactivated = new PermitGateway().UpdatePermit(permit);
                    if(inactivated) {
                        //Overwrite the permit number on the current permit and create a new one
                        permit.ID = 0;
                        permit.Number = newNumber;
                        permit.Activated = permit.Updated;
                        permit.ActivatedBy = permit.UpdatedBy;
                        permit.Inactivated = DateTime.MinValue;
                        permit.InactivatedBy = "";
                        permit.InactivatedReason = "";
                        id = new PermitGateway().CreatePermit(permit);
                    }

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return id;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public bool RevokePermit(Permit permit) {
            //Revoke a permit: change the permit to inactive
            bool revoked = false;
            try {
                revoked = new PermitGateway().UpdatePermit(permit);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return revoked;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public bool ChangeVehicle(Vehicle vehicle) {
            //Change license plate or veicle or contact
            bool changed = false;
            try {
                changed = new PermitGateway().UpdateVehicle(vehicle);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return changed;
        }
        public Permit ValidatePermitNumber(string number) { 
            //Validate the specified number does not exist in the system
            //Return the permit if it does
            Permit permit = null;
            try {
                DataSet ds = new PermitGateway().ReadPermit(number);
                if(ds != null && ds.Tables["PermitTable"] != null && ds.Tables["PermitTable"].Rows.Count > 0) {
                    permit = new Permit(ds.Tables["PermitTable"].Rows[0]);
                }
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permit;
        }
        public Permit ValidateVehicle(string issueState, string plateNumber) {
            //Validate the specified vehicle does not exist in the system with an "active" permit
            //Return the active permit if it does
            Permit permit = null;
            try {
                DataSet ds = new PermitGateway().ReadPermit(issueState, plateNumber);
                if(ds != null && ds.Tables["PermitTable"] != null && ds.Tables["PermitTable"].Rows.Count > 0) {
                    DataSet dss = new DataSet();
                    dss.Merge(ds.Tables["PermitTable"].Select("Inactivated is null"));
                    if(dss.Tables["PermitTable"] != null && dss.Tables["PermitTable"].Rows.Count > 0)
                        permit = new Permit(dss.Tables["PermitTable"].Rows[0]);
                }
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permit;
        }

        public DataSet FindPermitsByNumber(string number) {
            //Find a permit; can return more than one object??
            DataSet permit = null;
            try {
                permit = new PermitGateway().FindPermitsByNumber(number);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permit;
        }
        public DataSet FindPermitsByPlate(string issueState, string plateNumber) {
            //Find a permit; can return more than one object??
            DataSet permit = null;
            try {
                permit = new PermitGateway().FindPermitsByPlate(issueState, plateNumber);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permit;
        }
        public DataSet FindPermitsByVehicle(string year, string make, string model, string color) {
            //Find a permit; can return more than one object??
            DataSet permit = null;
            try {
                permit = new PermitGateway().FindPermitsByVehicle(year, make, model, color);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permit;
        }
    }
}
