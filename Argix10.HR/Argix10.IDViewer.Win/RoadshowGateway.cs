using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Runtime.Caching;
using System.Threading;
using Argix.Windows;

namespace Argix.Terminals {
	//
	public class RoadshowGateway {
		//Members
        private static RoadshowServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static RoadshowGateway() { 
            //
            _Client = new RoadshowServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private RoadshowGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static Customers GetCustomers() {
            //Get carriers list
            Customers customers = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                customers = cache["customers"] as Customers;
                if(customers == null) {
                    _Client = new RoadshowServiceClient();
                    customers = _Client.GetCustomers();
                    _Client.Close();

                    //CacheItemPolicy policy = new CacheItemPolicy();
                    //policy.ChangeMonitors.Add(new SqlChangeMonitor(new SqlDependency()));
                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(30));
                    cache.Set("customers",customers,policy);
                }
            }
            catch(FaultException fe) { throw new ApplicationException("GetCustomers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetCustomers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetCustomers() communication error.",ce); }
            return customers;
        }
        public static Customers GetCustomers(string clientNumber) {
            //Get carriers list
            Customers customers = new Customers();
            try {
                Customers _customers = GetCustomers();
                if(clientNumber != null && clientNumber.Length > 0) {
                    for(int i=0;i<_customers.Count;i++) {
                        Customer c = _customers[i];
                        if(c.AccountID.Trim().Length > 0 && c.AccountID.Substring(0,3) == clientNumber) customers.Add(c);
                    }
                }
                else
                    customers = _customers;
            }
            catch(ApplicationException aex) { throw aex; }
            catch(Exception ce) { throw new ApplicationException("GetCustomers() unexpected error.",ce); }
            return customers;
        }
        public static Depots GetDepots() {
            //Get depots list
            Depots depots = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                depots = cache["depots"] as Depots;
                if(depots == null) {
                    _Client = new RoadshowServiceClient();
                    depots = _Client.GetDepots();
                    _Client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1));
                    cache.Set("depots",depots,policy);
                }
            }
            catch(FaultException fe) { throw new ApplicationException("GetDepots() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDepots() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDepots() communication error.",ce); }
            return depots;
        }
        public static Drivers GetDrivers(int depotNumber) {
            //Get drivers list
            Drivers drivers = null;
            try {
                _Client = new RoadshowServiceClient();
                drivers = _Client.GetDrivers(depotNumber);
                _Client.Close();

                Driver driver = new Driver();
                driver.Name = "All";
                drivers.Insert(0, driver);
            }
            catch(FaultException fe) { throw new ApplicationException("GetDrivers() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("GetDrivers() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("GetDrivers() communication error.",ce); }
            return drivers;
        }
    }
}