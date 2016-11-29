using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.HR {
    // 
    [ServiceContract(Namespace = "http://Argix.HR")]
    public interface IPermitService {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        ServiceInfo GetServiceInfo();

        [OperationContract]
        [FaultContractAttribute(typeof(ConfigurationFault),Action = "http://Argix.ConfigurationFault")]
        UserConfiguration GetUserConfiguration(string application,string[] usernames);

        [OperationContract(IsOneWay = true)]
        void WriteLogEntry(TraceMessage m);
    }

    [ServiceContract(Namespace = "http://Argix.HR")]
    public interface IPermitAdminService {
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        DataSet ViewPermits();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        Permit ReadPermit(int id);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        int RegisterPermit(Permit permit);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        int ReplacePermit(Permit permit, string newNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        bool RevokePermit(Permit permit);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        bool ChangeVehicle(Vehicle vehicle);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        Permit ValidatePermitNumber(string number);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        Permit ValidateVehicle(string issueState, string plateNumber);
    }

    [ServiceContract(Namespace = "http://Argix.HR")]
    public interface IPermitSearchService {
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        DataSet FindPermitsByNumber(string number);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        DataSet FindPermitsByPlate(string issueState, string plateNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        DataSet FindPermitsByVehicle(string year, string make, string model, string color);
    }

    [DataContract]
    public class Permit {
        //Members
        private int _id = 0, _vehicleid=0;
        private string _number = "";
        private DateTime _activated, _inactivated;
        private string _activatedby = "", _inactivatedby="";
        private string _inactivatedreason = "";
        private DateTime _updated;
        private string _updatedby = "";
        private Vehicle _vehicle = null;

        //Interface
        public Permit() { }
        public Permit(DataRow permit) {
            this._id = int.Parse(permit["ID"].ToString());
            this._number = permit["Number"].ToString();
            this._vehicleid = int.Parse(permit["VehicleID"].ToString());
            this._activated = DateTime.Parse(permit["Activated"].ToString());
            this._activatedby = permit["ActivatedBy"].ToString();
            this._inactivated = !permit.IsNull("Inactivated") ? DateTime.Parse(permit["Inactivated"].ToString()) : DateTime.MinValue;
            this._inactivatedby = !permit.IsNull("InactivatedBy") ? permit["InactivatedBy"].ToString() : "";
            this._inactivatedreason = !permit.IsNull("InactiveReason") ? permit["InactiveReason"].ToString() : "";
            this._updated = !permit.IsNull("Updated") ? DateTime.Parse(permit["Updated"].ToString()) : DateTime.MinValue;
            this._updatedby = !permit.IsNull("UpdatedBy") ? permit["UpdatedBy"].ToString() : "";
            this._vehicle = new Vehicle(permit);
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public int VehicleID { get { return this._vehicleid; } set { this._vehicleid = value; } }
        [DataMember]
        public DateTime Activated { get { return this._activated; } set { this._activated = value; } }
        [DataMember]
        public string ActivatedBy { get { return this._activatedby; } set { this._activatedby = value; } }
        [DataMember]
        public DateTime Inactivated { get { return this._inactivated; } set { this._inactivated = value; } }
        [DataMember]
        public string InactivatedBy { get { return this._inactivatedby; } set { this._inactivatedby = value; } }
        [DataMember]
        public string InactivatedReason { get { return this._inactivatedreason; } set { this._inactivatedreason = value; } }
        [DataMember]
        public DateTime Updated { get { return this._updated; } set { this._updated = value; } }
        [DataMember]
        public string UpdatedBy { get { return this._updatedby; } set { this._updatedby = value; } }
        [DataMember]
        public Vehicle Vehicle { get { return this._vehicle; } set { this._vehicle = value; } }
        #endregion
    }
    
    [DataContract]
    public class Vehicle {
        //Members
        private int _id = 0;
        private string _issuestate = "", _platenumber="";
        private string _year = "", _make = "", _model = "", _color = "";
        private string _lastname = "", _firstname = "", _middlename = "", _phonenumber = "";
        private DateTime _updated;
        private string _updatedby = "";
        private int _badgenumber = 0;

        //Interface
        public Vehicle() { }
        public Vehicle(DataRow vehicle) {
            //Create this vehicle from a permit
            //NOTE: the datarow could come from PermitTable or VehicleTable- doesn't matter except for ID
            //If PermitTable get VehicleID NOT permit's ID; VehicleID exists ony in PermitTable
            if(!vehicle.IsNull("VehicleID"))
                this._id = int.Parse(vehicle["VehicleID"].ToString());
            else
                this._id = int.Parse(vehicle["ID"].ToString());
            this._issuestate = vehicle["IssueState"].ToString();
            this._platenumber = vehicle["PlateNumber"].ToString();
            this._year = vehicle["Year"].ToString();
            this._make = vehicle["Make"].ToString();
            this._model = vehicle["Model"].ToString();
            this._color = vehicle["Color"].ToString();
            this._lastname = vehicle["ContactLastName"].ToString();
            this._firstname = vehicle["ContactFirstName"].ToString();
            this._middlename = !vehicle.IsNull("ContactMiddleName") ? vehicle["ContactMiddleName"].ToString() : "";
            this._phonenumber = vehicle["ContactPhoneNumber"].ToString();
            this._badgenumber = !vehicle.IsNull("BadgeNumber") ? int.Parse(vehicle["BadgeNumber"].ToString()) : 0;
            this._updated = !vehicle.IsNull("Updated") ? DateTime.Parse(vehicle["Updated"].ToString()) : DateTime.MinValue;
            this._updatedby = !vehicle.IsNull("UpdatedBy") ? vehicle["UpdatedBy"].ToString() : "";
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int ID { get { return this._id; } set { this._id = value; } }
        [DataMember]
        public string IssueState { get { return this._issuestate; } set { this._issuestate = value; } }
        [DataMember]
        public string PlateNumber { get { return this._platenumber; } set { this._platenumber = value; } }
        [DataMember]
        public string Year { get { return this._year; } set { this._year = value; } }
        [DataMember]
        public string Make { get { return this._make; } set { this._make = value; } }
        [DataMember]
        public string Model { get { return this._model; } set { this._model = value; } }
        [DataMember]
        public string Color { get { return this._color; } set { this._color = value; } }
        [DataMember]
        public string ContactLastName { get { return this._lastname; } set { this._lastname = value; } }
        [DataMember]
        public string ContactFirstName { get { return this._firstname; } set { this._firstname = value; } }
        [DataMember]
        public string ContactMiddleName { get { return this._middlename; } set { this._middlename = value; } }
        [DataMember]
        public string ContactPhoneNumber { get { return this._phonenumber; } set { this._phonenumber = value; } }
        [DataMember]
        public DateTime Updated { get { return this._updated; } set { this._updated = value; } }
        [DataMember]
        public string UpdatedBy { get { return this._updatedby; } set { this._updatedby = value; } }
        [DataMember]
        public int BadgeNumber { get { return this._badgenumber; } set { this._badgenumber = value; } }
        #endregion
    }
}
