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
    public interface IBadgeService {
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
    public interface IAccessControlBadgeService {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewAccessControlBadges();
    }

    [ServiceContract(Namespace = "http://Argix.HR")]
    public interface IDriverBadgeService {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewDriverBadges();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewDriverBadgesSummary();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DriverBadge GetDriverBadge(int idNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet SearchDriverBadges(string lastName,string firstName,string location,string badgeNumber);
    }

    [ServiceContract(Namespace = "http://Argix.HR")]
    public interface IEmployeeBadgeService {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewEmployeeBadges();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewEmployeeBadgesSummary();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        EmployeeBadge GetEmployeeBadge(int idNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        bool AddEmployeeBadge(EmployeeBadge badge);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        bool UpdateEmployeeBadge(EmployeeBadge badge);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet SearchEmployeeBadges(string lastName,string firstName,string location,string badgeNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetEmployeeDepartmentList();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        bool AddEmployeeDepartment(string name);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetEmployeeLocationList();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetEmployeeSubLocationList();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetEmployeeStatusList();
    }

    [ServiceContract(Namespace = "http://Argix.HR")]
    public interface IVendorBadgeService {
        //
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewVendorBadges();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet ViewVendorBadgesSummary();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        VendorBadge GetVendorBadge(int idNumber);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        bool AddVendorBadge(VendorBadge badge);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        bool UpdateVendorBadge(VendorBadge badge);
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet SearchVendorBadges(string lastName,string firstName,string location,string badgeNumber);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetVendorDepartmentList();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault), Action = "http://Argix.HR.HRFault")]
        bool AddVendorDepartment(string name);

        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetVendorLocationList();
        [OperationContract]
        [FaultContractAttribute(typeof(Argix.HR.HRFault),Action = "http://Argix.HR.HRFault")]
        DataSet GetVendorStatusList();
    }

    [DataContract]
    public class Badge {
        //Members
        protected int _idnumber = 0;
        protected string _lastname = "",_firstname = "",_middle = "",_suffix = "";
        protected string _department = "";
        protected string _location = "";
        protected int _badgenumber = 0;
        protected string _status = "";
        protected DateTime _statusdate,_issuedate,_expirationdate,_hiredate;
        protected bool _hasphoto = false,_hassignature = false;

        //Interface
        public Badge() { }
        public Badge(BadgeDataset.BadgeTableRow badge) {
            //Constructor
            try {
                if (badge != null) {
                    this._idnumber = !badge.IsIDNumberNull() ? badge.IDNumber : 0;
                    this._lastname = !badge.IsLastNameNull() ? badge.LastName : "";
                    this._firstname = !badge.IsFirstNameNull() ? badge.FirstName : "";
                    this._middle = !badge.IsMiddleNull() ? badge.Middle : "";
                    this._suffix = !badge.IsSuffixNull() ? badge.Suffix : "";
                    this._department = !badge.IsDepartmentNull() ? badge.Department : "";
                    this._location = !badge.IsLocationNull() ? badge.Location : "";
                    this._badgenumber = !badge.IsBadgeNumberNull() ? badge.BadgeNumber : 0;
                    this._status = !badge.IsStatusNull() ? badge.Status : "";
                    this._statusdate = !badge.IsStatusDateNull() ? badge.StatusDate : DateTime.MinValue;
                    this._issuedate = !badge.IsIssueDateNull() ? badge.IssueDate : DateTime.MinValue;
                    this._expirationdate = !badge.IsExpirationDateNull() ? badge.ExpirationDate : DateTime.MinValue;
                    this._hiredate = !badge.IsHireDateNull() ? badge.HireDate : DateTime.MinValue;
                    this._hasphoto = !badge.IsPhotoNull();
                    this._hassignature = !badge.IsSignatureNull();
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int IDNumber { get { return this._idnumber; } set { this._idnumber = value; } }
        [DataMember]
        public string LastName { get { return this._lastname; } set { this._lastname = value; } }
        [DataMember]
        public string FirstName { get { return this._firstname; } set { this._firstname = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Middle { get { return this._middle; } set { this._middle = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Suffix { get { return this._suffix; } set { this._suffix = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Department { get { return this._department; } set { this._department = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Location { get { return this._location; } set { this._location = value; } }
        [DataMember]
        public int BadgeNumber { get { return this._badgenumber; } set { this._badgenumber = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember(EmitDefaultValue = false)]
        public DateTime StatusDate { get { return this._statusdate; } set { this._statusdate = value; } }
        [DataMember]
        public DateTime IssueDate { get { return this._issuedate; } set { this._issuedate = value; } }
        [DataMember(EmitDefaultValue = false)]
        public DateTime ExpirationDate { get { return this._expirationdate; } set { this._expirationdate = value; } }
        [DataMember(EmitDefaultValue = false)]
        public DateTime HireDate { get { return this._hiredate; } set { this._hiredate = value; } }
        [DataMember(EmitDefaultValue = false)]
        public bool HasPhoto { get { return this._hasphoto; } set { this._hasphoto = value; } }
        [DataMember(EmitDefaultValue = false)]
        public bool HasSignature { get { return this._hassignature; } set { this._hassignature = value; } }
        #endregion
    }

    [DataContract]
    public class DriverBadge:Badge {
        //Members
        private string _organization = "Argix";
        private int _faccode = 0;
        private string _sublocation = "";
        private string _employeeid = "";
        private byte[] _photo = null,_signature = null;
        private DateTime _birthdate;

        //Interface
        public DriverBadge() { }
        public DriverBadge(BadgeDataset.BadgeTableRow badge): base(badge) {
            //Constructor
            try {
                if (badge != null) {
                    this._organization = !badge.IsOrganizationNull() ? badge.Organization : "";
                    this._faccode = !badge.IsFaccodeNull() ? badge.Faccode : 0;
                    this._sublocation = !badge.IsSubLocationNull() ? badge.SubLocation : "";
                    this._employeeid = !badge.IsEmployeeIDNull() ? badge.EmployeeID : "";
                    this._photo = !badge.IsPhotoNull() ? badge.Photo : null;
                    this._signature = !badge.IsSignatureNull() ? badge.Signature : null;
                    this._birthdate = !badge.IsBirthDateNull() ? badge.BirthDate : DateTime.MinValue;

                    base._hasphoto = !badge.IsPhotoNull();
                    base._hassignature = !badge.IsSignatureNull();
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember(EmitDefaultValue = false)]
        public string Organization { get { return this._organization; } set { this._organization = value; } }
        [DataMember(EmitDefaultValue = false)]
        public int Faccode { get { return this._faccode; } set { this._faccode = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubLocation { get { return this._sublocation; } set { this._sublocation = value; } }
        [DataMember]
        public string EmployeeID { get { return this._employeeid; } set { this._employeeid = value; } }
        [DataMember(EmitDefaultValue = false)]
        public byte[] Photo { get { return this._photo; } set { this._photo = value; } }
        [DataMember(EmitDefaultValue = false)]
        public byte[] Signature { get { return this._signature; } set { this._signature = value; } }
        [DataMember(EmitDefaultValue = false)]
        public DateTime BirthDate { get { return this._birthdate; } set { this._birthdate = value; } }
        #endregion
    }

    [DataContract]
    public class EmployeeBadge:Badge {
        //Members
        private string _organization = "Argix";
        private int _faccode = 0;
        private string _sublocation = "";
        private string _employeeid = "";
        private byte[] _photo = null,_signature = null;
        private DateTime _birthdate;
        private string _ssn = "";

        //Interface
        public EmployeeBadge() { }
        public EmployeeBadge(BadgeDataset.BadgeTableRow badge): base(badge) {
            //Constructor
            try {
                if (badge != null) {
                    //base._idnumber = !badge.IsIDNumberNull() ? badge.IDNumber : 0;
                    //base._lastname = !badge.IsLastNameNull() ? badge.LastName : "";
                    //base._firstname = !badge.IsFirstNameNull() ? badge.FirstName : "";
                    //base._middle = !badge.IsMiddleNull() ? badge.Middle : "";
                    //base._suffix = !badge.IsSuffixNull() ? badge.Suffix : "";
                    //base._department = !badge.IsDepartmentNull() ? badge.Department : "";
                    //base._location = !badge.IsLocationNull() ? badge.Location : "";
                    //base._badgenumber = !badge.IsBadgeNumberNull() ? badge.BadgeNumber : 0;
                    //base._status = !badge.IsStatusNull() ? badge.Status : "";
                    //base._statusdate = !badge.IsStatusDateNull() ? badge.StatusDate : DateTime.MinValue;
                    //base._issuedate = !badge.IsIssueDateNull() ? badge.IssueDate : DateTime.MinValue;
                    //base._expirationdate = !badge.IsExpirationDateNull() ? badge.ExpirationDate : DateTime.MinValue;
                    //base._hiredate = !badge.IsHireDateNull() ? badge.HireDate : DateTime.MinValue;

                    this._organization = !badge.IsOrganizationNull() ? badge.Organization : "";
                    this._faccode = !badge.IsFaccodeNull() ? badge.Faccode : 0;
                    this._sublocation = !badge.IsSubLocationNull() ? badge.SubLocation : "";
                    this._employeeid = !badge.IsEmployeeIDNull() ? badge.EmployeeID : "";
                    this._photo = !badge.IsPhotoNull() ? badge.Photo : null;
                    this._signature = !badge.IsSignatureNull() ? badge.Signature : null;
                    this._birthdate = !badge.IsBirthDateNull() ? badge.BirthDate : DateTime.MinValue;
                    this._ssn = !badge.IsSSNNull() ? badge.SSN : "";

                    base._hasphoto = !badge.IsPhotoNull();
                    base._hassignature = !badge.IsSignatureNull();
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember(EmitDefaultValue = false)]
        public string Organization { get { return this._organization; } set { this._organization = value; } }
        [DataMember(EmitDefaultValue = false)]
        public int Faccode { get { return this._faccode; } set { this._faccode = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SubLocation { get { return this._sublocation; } set { this._sublocation = value; } }
        [DataMember]
        public string EmployeeID { get { return this._employeeid; } set { this._employeeid = value; } }
        [DataMember(EmitDefaultValue = false)]
        public byte[] Photo { get { return this._photo; } set { this._photo = value; } }
        [DataMember(EmitDefaultValue = false)]
        public byte[] Signature { get { return this._signature; } set { this._signature = value; } }
        [DataMember(EmitDefaultValue = false)]
        public DateTime BirthDate { get { return this._birthdate; } set { this._birthdate = value; } }
        [DataMember(EmitDefaultValue = false)]
        public string SSN { get { return this._ssn; } set { this._ssn = value; } }
        #endregion
    }

    [DataContract]
    public class VendorBadge:Badge {
        //Members

        //Interface
        public VendorBadge() { }
        public VendorBadge(BadgeDataset.BadgeTableRow badge): base(badge) { }
        #region Accessors\Modifiers: [Members...]
        #endregion
    }
}
