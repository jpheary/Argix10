using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Argix {
    //
    public class ApplicationServicesGateway {
        //Members
        public const string SQL_CONNID = "ApplicationServices";

        private const string USP_APPLICATION_READID = "aspnet_Personalization_GetApplicationId";
        private const string USP_USERS_READ = "aspnet_Users_ReadUsers",TBL_USERS = "UserTable";
        private const string USP_USERS_CREATE = "aspnet_Users_CreateUser";
        private const string USP_USERS_DELETE = "aspnet_Users_DeleteUser";

        private const string USP_ROLES_READ = "aspnet_Roles_GetAllRoles",TBL_ROLES = "RoleTable";
        private const string USP_ROLES_GETROLES = "aspnet_UsersInRoles_GetRolesForUser";
        private const string USP_ROLES_ADDUSER = "aspnet_UsersInRoles_AddUsersToRoles";
        private const string USP_ROLES_REMOVEUSER = "aspnet_UsersInRoles_RemoveUsersFromRoles";


        //Interface
        public ApplicationServicesGateway() { }

        public static Guid GetApplicationID(string applicationName) {
            //Get the ID of for specified applicationName
            Guid id = new Guid();
            try {
                id = (Guid)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_APPLICATION_READID,new object[] { applicationName,null });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public static DataSet GetUsers(string applicationName) {
            //Get a list of all users
            DataSet users = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_USERS_READ,TBL_USERS,new object[] { });
                if (ds != null && ds.Tables[TBL_USERS] != null && ds.Tables[TBL_USERS].Rows.Count > 0) users.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return users;
        }
        public static Guid CreateUser(string applicationName,string userName) {
            //Create a new user
            Guid userID = new Guid();
            try {
                Guid applicationID = GetApplicationID(applicationName);
                byte isAnonymous = 0;
                DateTime lastActivityDate = DateTime.Now;
                userID = (Guid)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_USERS_CREATE,new object[] { applicationID,userName,isAnonymous,lastActivityDate,null });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return userID;
        }
        public static int DeleteUser(string applicationName,string userName) {
            //Delete an existing user
            int numTablesDeletedFrom = 0;
            try {
                int tablesToDeleteFrom = 15;        //OR value takes following values:
                                                    //1: vw_aspnet_MembershipUsers
                                                    //2: vw_aspnet_UsersInRoles
                                                    //4: vw_aspnet_Profiles
                                                    //8: vw_aspnet_WebPartState_User
                numTablesDeletedFrom = (int)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_USERS_CREATE,new object[] { applicationName,userName,tablesToDeleteFrom });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return numTablesDeletedFrom;
        }

        public static DataSet GetRoles(string applicationName) {
            //Get a list of all roles
            DataSet roles = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ROLES_READ,TBL_ROLES,new object[] { applicationName });
                if (ds != null && ds.Tables[TBL_ROLES] != null && ds.Tables[TBL_ROLES].Rows.Count > 0) roles.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return roles;
        }
        public static DataSet GetRolesForUser(string applicationName,string userName) {
            //
            DataSet roles = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ROLES_GETROLES,TBL_ROLES,new object[] { applicationName,userName });
                if (ds != null && ds.Tables[TBL_ROLES] != null && ds.Tables[TBL_ROLES].Rows.Count > 0) roles.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return roles;
        }
        public static bool AddUserToRole(string applicationName,string userName,string roleName) {
            //
            bool added = false;
            try {
                DateTime currentTime = DateTime.Now;
                added = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ROLES_ADDUSER,new object[] { applicationName,userName,roleName,currentTime });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return added;
        }
        public static bool RemoveUserFromRole(string applicationName,string userName,string roleName) {
            //
            bool removed = false;
            try {
                removed = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ROLES_REMOVEUSER,new object[] { applicationName,userName,roleName });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return removed;
        }
    }
}
