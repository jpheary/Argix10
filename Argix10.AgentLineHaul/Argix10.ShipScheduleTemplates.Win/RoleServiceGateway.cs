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

        private const string ROLE_TEMPLATOR = "LineHaul Templator";
        private const string ROLE_ADMINISTRATOR = "LineHaul Administrator";
        private const string ROLE_COORDINATOR = "LineHaul Coordinator";
        private const string ROLE_OPERATOR = "LineHaul Operator";

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
            if (IsLineHaulTemplator) return ROLE_TEMPLATOR;
            else if (IsLineHaulAdministrator) return ROLE_ADMINISTRATOR;
            else if (IsLineHaulCoordinator) return ROLE_COORDINATOR;
            else if (IsLineHaulOperator) return ROLE_OPERATOR;
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

        public static bool IsLineHaulTemplator {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_TEMPLATOR);
                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsLineHaulAdministrator {
            get {
                bool authorized=false;
                string[] roles = GetRolesForCurrentUser();
                for(int i=0;i<roles.Length;i++) {
                    authorized = (roles[i] == ROLE_ADMINISTRATOR);
                    if(authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsLineHaulCoordinator {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_COORDINATOR);
                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool IsLineHaulOperator {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_OPERATOR);
                    if (authorized) break;
                }
                return authorized;
            }
        }
    }
}