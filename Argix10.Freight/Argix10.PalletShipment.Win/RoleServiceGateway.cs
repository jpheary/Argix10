using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Security {
	//
	public class RoleServiceGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        private static string[] _roles=null;

        private const string ROLE_CFO = "Chief Financial Officer";
        private const string ROLE_FINANCESUPER = "Billing Supervisor";
        private const string ROLE_DISPATCHSSUPER = "Dispatch Supervisor";
        private const string ROLE_SALESREP = "SalesRep";
        private const string ROLE_SALESREPADMIN = "SalesRepAdmin";

        
		//Interface
        static RoleServiceGateway() { 
            //
            RoleServiceClient client = new RoleServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private RoleServiceGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static string[] GetRolesForCurrentUser() {
            //Get all roles for the current user
            RoleServiceClient client = new RoleServiceClient();
            try {
                if(_roles == null) {
                    _roles = client.GetRolesForCurrentUser();
                    client.Close();
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return _roles;
        }
        public static string GetRoleForCurrentUser() {
            if(IsCFO) return ROLE_CFO;
            else if(IsFinanceSupervisor) return ROLE_FINANCESUPER;
            else if(IsSalesRepAdmin) return ROLE_SALESREPADMIN;
            else if(IsSalesRep) return ROLE_SALESREP;
            else if(IsDispatchSupervisor) return ROLE_DISPATCHSSUPER;
            else {
                if (_roles.Length > 0) return _roles[0]; else return "No roles";
            }
        }
        public static bool IsCurrentUserInRole(string role) {
            //Determine if the current user is in the specified role
            bool inRole=false;
            RoleServiceClient client = new RoleServiceClient();
            try {
                inRole = client.IsCurrentUserInRole(role);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return inRole;
        }

        public static bool IsCFO {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_CFO);
                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsFinanceSupervisor {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_FINANCESUPER);
                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsDispatchSupervisor {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_DISPATCHSSUPER);
                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsSalesRep {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for(int i = 0; i < roles.Length; i++) {
                    authorized = (roles[i] == ROLE_SALESREP || roles[i] == ROLE_SALESREPADMIN);
                    if(authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsSalesRepAdmin {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for(int i = 0; i < roles.Length; i++) {
                    authorized = (roles[i] == ROLE_SALESREPADMIN);
                    if(authorized) break;
                }
                return authorized;
            }
        }
    }
}