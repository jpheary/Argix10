using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;

namespace Argix.HR {
	//
    public class PermitGateway {
        //Members
        public const string SQL_CONNID = "Permits";

        private const string USP_PERMITS_VIEW = "parking.uspPermitsView", TBL_PERMITS = "PermitTable";
        private const string USP_PERMIT_CREATE = "parking.uspPermitCreate";
        private const string USP_PERMIT_READ = "parking.uspPermitRead", USP_PERMIT_READBYNUMBER = "parking.uspPermitReadByNumber", USP_PERMIT_READBYPLATE = "parking.uspPermitReadByVehiclePlate";
        private const string USP_PERMIT_UPDATE = "parking.uspPermitUpdate";
        private const string USP_VEHICLE_CREATE = "parking.uspVehicleCreate", TBL_VEHICLES = "VehicleTable";
        private const string USP_VEHICLE_READ = "parking.uspVehicleRead", USP_VEHICLE_READBYMAKE = "parking.uspVehicleReadByMake", USP_VEHICLE_READBYPLATE = "parking.uspVehicleReadByPlate";
        private const string USP_VEHICLE_UPDATE = "parking.uspVehicleUpdate";

        private const string USP_PERMITS_FINDBYNUMBER = "parking.uspPermitsFindByNumber", USP_PERMITS_FINDBYPLATE = "parking.uspPermitsFindByPlate", USP_PERMITS_FINDBYVEHICLE = "parking.uspPermitsFindByVehicle";
        
        //Interface
        public PermitGateway() { }
        public DataSet ViewPermits() {
            //View all permits
            DataSet data = null;
            try {
                data = new DataService().FillDataset(SQL_CONNID, USP_PERMITS_VIEW, TBL_PERMITS, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return data;
        }
        public int CreatePermit(Permit permit) {
            //Create a new permit
            int id = 0;
            try {
                id = (int)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_PERMIT_CREATE,
                            new object[] {  
                                null, permit.Number,
                                permit.VehicleID,
                                permit.Activated, permit.ActivatedBy,
                                permit.Updated, permit.UpdatedBy
                            });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return id;
        }
        public DataSet ReadPermit(int id) {
            //Read a permit by ID
            DataSet permit = null;
            try {
                permit = new DataService().FillDataset(SQL_CONNID, USP_PERMIT_READ, TBL_PERMITS, new object[] { id });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return permit;
        }
        public DataSet ReadPermit(string number) {
            //Read a permit by number
            DataSet permit = null;
            try {
                permit = new DataService().FillDataset(SQL_CONNID, USP_PERMIT_READBYNUMBER, TBL_PERMITS, new object[] { number });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return permit;
        }
        public DataSet ReadPermit(string issueState, string plateNumber) {
            //Read a permit by vehicle plate
            DataSet permit = null;
            try {
                permit = new DataService().FillDataset(SQL_CONNID, USP_PERMIT_READBYPLATE, TBL_PERMITS, new object[] { issueState, plateNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return permit;
        }
        public bool UpdatePermit(Permit permit) {
            //Update an existing permit
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID, USP_PERMIT_UPDATE,
                            new object[] { 
                                permit.ID, 
                                permit.Inactivated, permit.InactivatedBy, permit.InactivatedReason, 
                                permit.Updated, permit.UpdatedBy
                            });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return updated;
        }
        public int CreateVehicle(Vehicle vehicle) {
            //Create a new vehicle
            int id = 0;
            try {
                id = (int)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_VEHICLE_CREATE,
                            new object[] {  
                                null, vehicle.IssueState, vehicle.PlateNumber, 
                                vehicle.Year, vehicle.Make, vehicle.Model, vehicle.Color,
                                vehicle.ContactLastName, vehicle.ContactFirstName, vehicle.ContactMiddleName, vehicle.ContactPhoneNumber, 
                                vehicle.Updated, vehicle.UpdatedBy, 
                                (vehicle.BadgeNumber > 0 ? vehicle.BadgeNumber : null as object)
                            });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return id;
        }
        public DataSet ReadVehicle(int id) {
            //Read a vehicle by ID
            DataSet vehicle = null;
            try {
                vehicle = new DataService().FillDataset(SQL_CONNID, USP_VEHICLE_READ, TBL_VEHICLES, new object[] { id });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return vehicle;
        }
        public DataSet ReadVehicle(string year, string make, string model, string color) {
            //Read a vehicle by make/model
            DataSet vehicle = null;
            try {
                vehicle = new DataService().FillDataset(SQL_CONNID, USP_VEHICLE_READBYMAKE, TBL_VEHICLES, 
                    new object[] { year, make, model, color });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return vehicle;
        }
        public DataSet ReadVehicle(string issueState, string plateNumber) {
            //Read a vehicle by plate
            DataSet vehicle = null;
            try {
                vehicle = new DataService().FillDataset(SQL_CONNID, USP_VEHICLE_READBYPLATE, TBL_VEHICLES, 
                    new object[] { issueState, plateNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return vehicle;
        }
        public bool UpdateVehicle(Vehicle vehicle) {
            //Update an existing vehicle
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID, USP_VEHICLE_UPDATE,
                            new object[] { 
                                vehicle.ID, 
                                vehicle.IssueState, vehicle.PlateNumber,
                                vehicle.Year, vehicle.Make, vehicle.Model, vehicle.Color,
                                vehicle.ContactLastName, vehicle.ContactFirstName, vehicle.ContactMiddleName, vehicle.ContactPhoneNumber,
                                vehicle.Updated, vehicle.UpdatedBy, 
                                (vehicle.BadgeNumber > 0 ? vehicle.BadgeNumber : null as object)
                            });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return updated;
        }

        public DataSet FindPermitsByNumber(string number) {
            //Find a permits by number
            DataSet permits = null;
            try {
                permits = new DataService().FillDataset(SQL_CONNID, USP_PERMITS_FINDBYNUMBER, TBL_PERMITS,
                    new object[] { number });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return permits;
        }
        public DataSet FindPermitsByPlate(string issueState, string plateNumber) {
            //Find a permits by plate
            DataSet permits = null;
            try {
                permits = new DataService().FillDataset(SQL_CONNID, USP_PERMITS_FINDBYPLATE, TBL_PERMITS, 
                    new object[] { issueState, plateNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return permits;
        }
        public DataSet FindPermitsByVehicle(string year, string make, string model, string color) {
            //Find a permits by vehicle
            DataSet permits = null;
            try {
                permits = new DataService().FillDataset(SQL_CONNID, USP_PERMITS_FINDBYVEHICLE, TBL_PERMITS,
                    new object[] { (year.Trim().Length > 0 ? year : null), make, model, (color.Trim().Length > 0 ? color : null) });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return permits;
        }
    }
}