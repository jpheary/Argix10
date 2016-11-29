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
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Argix.Enterprise;

namespace Argix.HR {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class BadgeService:IBadgeService,IAccessControlBadgeService,IDriverBadgeService,IEmployeeBadgeService,IVendorBadgeService {
        //Members

        //Interface
        public BadgeService() { }

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

        public DataSet ViewAccessControlBadges() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new AccessControlBadgeGateway().ReadBadges();
                if (ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    for (int i = 0;i < _badges.BadgeTable.Rows.Count;i++) {
                        _badges.BadgeTable[i].HasPhoto = (!_badges.BadgeTable[i].IsPhotoNull());
                        _badges.BadgeTable[i].HasSignature = (!_badges.BadgeTable[i].IsSignatureNull());
                    }
                    badges.Merge(_badges);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }

        public DataSet ViewDriverBadges() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new DriverBadgeGateway().ReadBadges();
                if (ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    for (int i = 0;i < _badges.BadgeTable.Rows.Count;i++) {
                        _badges.BadgeTable[i].HasPhoto = (!_badges.BadgeTable[i].IsPhotoNull());
                        _badges.BadgeTable[i].HasSignature = (!_badges.BadgeTable[i].IsSignatureNull());
                    }
                    badges.Merge(_badges);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }
        public DataSet ViewDriverBadgesSummary() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new DriverBadgeGateway().ReadBadgesSummary();
                if(ds != null) badges.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }
        public DriverBadge GetDriverBadge(int idNumber) {
            //
            DriverBadge badge = null;
            try {
                DataSet ds = new DriverBadgeGateway().ReadBadge(idNumber);
                if(ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    badge = new DriverBadge(_badges.BadgeTable[0]);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badge;
        }
        public DataSet SearchDriverBadges(string lastName,string firstName,string location,string badgeNumber) {
            //Search for employees
            DataSet badges = new DataSet();
            try {
                DataSet ds = new DriverBadgeGateway().SearchBadges(lastName,firstName,location,badgeNumber);
                if(ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    for(int i = 0; i < _badges.BadgeTable.Rows.Count; i++) {
                        _badges.BadgeTable[i].HasPhoto = (!_badges.BadgeTable[i].IsPhotoNull());
                        _badges.BadgeTable[i].HasSignature = (!_badges.BadgeTable[i].IsSignatureNull());
                    }
                    badges.Merge(_badges);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }

        public DataSet ViewEmployeeBadges() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new EmployeeBadgeGateway().ReadBadges();
                if (ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    for (int i = 0;i < _badges.BadgeTable.Rows.Count;i++) {
                        _badges.BadgeTable[i].HasPhoto = (!_badges.BadgeTable[i].IsPhotoNull());
                        _badges.BadgeTable[i].HasSignature = (!_badges.BadgeTable[i].IsSignatureNull());
                    }
                    badges.Merge(_badges);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }
        public DataSet ViewEmployeeBadgesSummary() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new EmployeeBadgeGateway().ReadBadgesSummary();
                if (ds != null) badges.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }
        public EmployeeBadge GetEmployeeBadge(int idNumber) {
            //
            EmployeeBadge badge = null;
            try {
                DataSet ds = new EmployeeBadgeGateway().ReadBadge(idNumber);
                if (ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    badge = new EmployeeBadge(_badges.BadgeTable[0]);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badge;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public bool AddEmployeeBadge(EmployeeBadge badge) {
            //Add a new badge
            bool added = false;
            try {
                added = new EmployeeBadgeGateway().CreateBadge(badge);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return added;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public bool UpdateEmployeeBadge(EmployeeBadge badge) {
            //Update an existing badge
            bool updated = false;
            try {
                updated = new EmployeeBadgeGateway().UpdateBadge(badge);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return updated;
        }
        public DataSet SearchEmployeeBadges(string lastName,string firstName,string location,string badgeNumber) {
            //Search for employees
            DataSet badges = new DataSet();
            try {
                DataSet ds = new EmployeeBadgeGateway().SearchBadges(lastName,firstName,location,badgeNumber);
                if (ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    for (int i = 0;i < _badges.BadgeTable.Rows.Count;i++) {
                        _badges.BadgeTable[i].HasPhoto = (!_badges.BadgeTable[i].IsPhotoNull());
                        _badges.BadgeTable[i].HasSignature = (!_badges.BadgeTable[i].IsSignatureNull());
                    }
                    badges.Merge(_badges);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }

        public DataSet GetEmployeeDepartmentList() {
            //
            DataSet list = null;
            try {
                list = new EmployeeBadgeGateway().ReadDepartmentList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        public bool AddEmployeeDepartment(string name) {
            //Add a new department
            bool created = false;
            try {
                created = new EmployeeBadgeGateway().CreateDepartment(name);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return created;
        }
        public DataSet GetEmployeeLocationList() {
            //
            DataSet list = null;
            try {
                list = new EmployeeBadgeGateway().ReadLocationList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }
        public DataSet GetEmployeeSubLocationList() {
            //
            DataSet list = null;
            try {
                list = new EmployeeBadgeGateway().ReadSubLocationList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }
        public DataSet GetEmployeeStatusList() {
            //
            DataSet list = null;
            try {
                list = new EmployeeBadgeGateway().ReadStatusList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }

        public DataSet ViewVendorBadges() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new VendorBadgeGateway().ReadBadges();
                if (ds != null) badges.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }
        public DataSet ViewVendorBadgesSummary() {
            //
            DataSet badges = new DataSet();
            try {
                DataSet ds = new VendorBadgeGateway().ReadBadgesSummary();
                if (ds != null) badges.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }
        public VendorBadge GetVendorBadge(int idNumber) {
            //
            VendorBadge badge = null;
            try {
                DataSet ds = new VendorBadgeGateway().ReadBadge(idNumber);
                if (ds != null) {
                    BadgeDataset _badges = new BadgeDataset();
                    _badges.Merge(ds);
                    badge = new VendorBadge(_badges.BadgeTable[0]);
                }
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badge;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public bool AddVendorBadge(VendorBadge badge) {
            //Add a new badge
            bool added = false;
            try {
                added = new VendorBadgeGateway().CreateBadge(badge);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return added;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Manager")]
        [PrincipalPermission(SecurityAction.Demand, Role = "HR Assistant")]
        public bool UpdateVendorBadge(VendorBadge badge) {
            //Update an existing badge
            bool updated = false;
            try {
                updated = new VendorBadgeGateway().UpdateBadge(badge);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return updated;
        }
        public DataSet SearchVendorBadges(string lastName,string firstName,string location,string badgeNumber) {
            //Search for badges
            DataSet badges = new DataSet();
            try {
                DataSet ds = new VendorBadgeGateway().SearchBadges(lastName,firstName,location,badgeNumber);
                if (ds != null) badges.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return badges;
        }

        public DataSet GetVendorDepartmentList() {
            //
            DataSet list = null;
            try {
                list = new VendorBadgeGateway().ReadDepartmentList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }
        [PrincipalPermission(SecurityAction.Demand, Role = "Administrator")]
        public bool AddVendorDepartment(string name) {
            //Add a new department
            bool created = false;
            try {
                created = new VendorBadgeGateway().CreateDepartment(name);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return created;
        }
        public DataSet GetVendorLocationList() {
            //
            DataSet list = null;
            try {
                list = new VendorBadgeGateway().ReadLocationList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }
        public DataSet GetVendorStatusList() {
            //
            DataSet list = null;
            try {
                list = new VendorBadgeGateway().ReadStatusList();
            }
            catch (Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message),"Service Error"); }
            return list;
        }
    }
}
