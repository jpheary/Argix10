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
    public class EmployeeBadgeGateway {
        //Members
        public const string SQL_CONNID = "Employees";

        private const string USP_BADGES_VIEW = "uspIDViewerBadgeDataGetList",TBL_BADGES = "BadgeTable";
        private const string USP_BADGES_SUMVIEW = "uspIDViewerBadgeDataGetSummaryList";
        private const string USP_BADGE = "uspIDViewerBadgeDataGet";
        private const string USP_BADGE_NEW = "uspIDViewerBadgedataNew", USP_BADGE_UPDATE = "uspIDViewerBadgedataUpdate";
        private const string USP_BADGE_SEARCH = "uspIDViewerBadgeDataSearch";

        private const string USP_DEPARTMENT_LIST = "uspIDViewerDepartmentGetList",TBL_DEPARTMENT = "DepartmentTable";
        private const string USP_DEPARTMENT_NEW = "uspIDViewerDepartmentNew";
        private const string USP_LOCATION_LIST = "uspIDViewerLocationGetList",TBL_LOCATION = "LocationTable";
        private const string USP_SUBLOCATION_LIST = "uspIDViewerSubLocationGetList",TBL_SUBLOCATION = "SubLocationTable";
        private const string USP_STATUS_LIST = "uspIDViewerStatusGetList",TBL_STATUS = "StatusTable";

        //Interface
        public EmployeeBadgeGateway() { }
        public DataSet ReadBadges() {
            //
            DataSet badges = null;
            try {
                badges = new DataService().FillDataset(SQL_CONNID,USP_BADGES_VIEW,TBL_BADGES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badges;
        }
        public DataSet ReadBadgesSummary() {
            //
            DataSet badges = null;
            try {
                badges = new DataService().FillDataset(SQL_CONNID,USP_BADGES_SUMVIEW,TBL_BADGES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badges;
        }
        public DataSet ReadBadge(int idNumber) {
            //
            DataSet badge = null;
            try {
                badge = new DataService().FillDataset(SQL_CONNID,USP_BADGE,TBL_BADGES,new object[] { idNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badge;
        }
        public bool CreateBadge(EmployeeBadge badge) {
            //Add a new badge
            bool created = false;
            try {
                created = new DataService().ExecuteNonQuery(SQL_CONNID,USP_BADGE_NEW,
                            new object[] { 
                                badge.LastName,badge.FirstName,badge.Middle,badge.Suffix,
                                badge.Department,badge.Location,badge.SubLocation,badge.Status,
                                (badge.HireDate != DateTime.MinValue ? badge.HireDate : null as object),badge.SSN 
                            });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return created;
        }
        public bool UpdateBadge(EmployeeBadge badge) {
            //Update an existing badge
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_BADGE_UPDATE,
                            new object[] { 
                                badge.IDNumber,badge.LastName,badge.FirstName,badge.Middle,badge.Suffix,
                                badge.Department,badge.Location,badge.SubLocation,badge.Status 
                            });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public DataSet SearchBadges(string lastName,string firstName,string location,string badgeNumber) {
            //Search for badges
            DataSet badges = null;
            try {
                badges = new DataService().FillDataset(SQL_CONNID,USP_BADGE_SEARCH,TBL_BADGES,new object[] { lastName,firstName,location,badgeNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badges;
        }

        public DataSet ReadDepartmentList() {
            //
            DataSet list = null;
            try {
                list = new DataService().FillDataset(SQL_CONNID,USP_DEPARTMENT_LIST,TBL_DEPARTMENT,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return list;
        }
        public bool CreateDepartment(string name) {
            //Add a new department
            bool created = false;
            try {
                created = new DataService().ExecuteNonQuery(SQL_CONNID, USP_DEPARTMENT_NEW, new object[] { name });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return created;
        }
        public DataSet ReadLocationList() {
            //
            DataSet list = null;
            try {
                list = new DataService().FillDataset(SQL_CONNID,USP_LOCATION_LIST,TBL_LOCATION,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return list;
        }
        public DataSet ReadSubLocationList() {
            //
            DataSet list = null;
            try {
                list = new DataService().FillDataset(SQL_CONNID,USP_SUBLOCATION_LIST,TBL_SUBLOCATION,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return list;
        }
        public DataSet ReadStatusList() {
            //
            DataSet list = null;
            try {
                list = new DataService().FillDataset(SQL_CONNID,USP_STATUS_LIST,TBL_STATUS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return list;
        }
    }
}