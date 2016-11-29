using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    class MembershipGateway { 
        //Members
        public const string SQL_CONNID = "Membership";

        private const string USP_USERS = "aspnet_Membership_GetAllUsers",TBL_USERS = "UserTable";

        //Interface
        public MembershipGateway() { }

        public MembershipDataset GetUsers() {
            MembershipDataset users = new MembershipDataset();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_USERS,TBL_USERS,new object[] {"Tracking",0,800 });
                if (ds.Tables[TBL_USERS] != null && ds.Tables[TBL_USERS].Rows.Count > 0) users.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return users;
        }
    }
}
