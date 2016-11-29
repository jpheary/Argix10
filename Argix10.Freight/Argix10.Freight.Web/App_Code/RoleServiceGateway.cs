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
        private static RoleServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        private static string[] _roles=null;

        public const string ROLE_FREIGHTASSIGNER = "Freight Assigner";
        
		//Interface
        static RoleServiceGateway() { 
            //
            _Client = new RoleServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private RoleServiceGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static string[] GetRolesForCurrentUser() {
            //Get all roles for the current user
            try {
                _Client = new RoleServiceClient();
                if(_roles == null) {
                    _roles = _Client.GetRolesForCurrentUser();
                    _Client.Close();
                }
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return _roles;
        }
        public static bool IsCurrentUserInRole(string role) {
            //Determine if the current user is in the specified role
            bool inRole=false;
            try {
                _Client = new RoleServiceClient();
                inRole = _Client.IsCurrentUserInRole(role);
                _Client.Close();
            }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return inRole;
        }

        public static bool IsCurrentUserFreightAssigner {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    authorized = (roles[i] == ROLE_FREIGHTASSIGNER);
                    if (authorized) break;
                }
                return authorized;
            }
        }

        public static bool CanCurrentUserAssignFreight {
            get {
                bool authorized=false;
                string[] roles = GetRolesForCurrentUser();
                for(int i=0;i<roles.Length;i++) {
                    if (roles[i] == ROLE_FREIGHTASSIGNER) 
                        authorized = true;
                    
                    if(authorized) break;
                }
                return authorized;
            }
        }
        public static bool CanCurrentUserUnassignFreight {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    if (roles[i] == ROLE_FREIGHTASSIGNER)
                        authorized = true;

                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool CanCurrentUserStopSorting {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    if (roles[i] == ROLE_FREIGHTASSIGNER)
                        authorized = true;

                    if (authorized) break;
                }
                return authorized;
            }
        }
        public static bool CanCurrentUserRemoveAssignment {
            get {
                bool authorized = false;
                string[] roles = GetRolesForCurrentUser();
                for (int i = 0;i < roles.Length;i++) {
                    if (roles[i] == ROLE_FREIGHTASSIGNER)
                        authorized = true;

                    if (authorized) break;
                }
                return authorized;
            }
        }
    }
}