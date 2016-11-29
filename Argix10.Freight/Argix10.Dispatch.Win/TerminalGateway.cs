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
	public class TerminalGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        private static int _cacheTimeout = 240;
        
		//Interface
        static TerminalGateway() { 
            //
            try {
                RoadshowServiceClient client = new RoadshowServiceClient();
                _state = true;
                _address = client.Endpoint.Address.Uri.AbsoluteUri;
                _cacheTimeout = global::Argix.Properties.Settings.Default.CacheTimeout;
            }
            catch (Exception ex) { App.ReportError(ex,true,Argix.Freight.LogLevel.Error); }
        }
        private TerminalGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static Customers GetCustomers() {
            //Get customers list
            Customers customers = null;
            RoadshowServiceClient client = new RoadshowServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                customers = cache["customers"] as Customers;
                if(customers == null) {
                    customers = client.GetCustomers();
                    client.Close();

                    //CacheItemPolicy policy = new CacheItemPolicy();
                    //policy.ChangeMonitors.Add(new SqlChangeMonitor(new SqlDependency()));
                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("customers",customers,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return customers;
        }
        public static Customers GetCustomers(string clientNumber) {
            //Get customers list
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
            catch(Exception ce) { throw new ApplicationException(ce.Message,ce); }
            return customers;
        }
        public static Customers2 GetCustomers2() {
            //Get customers list
            Customers2 customers = null;
            RoadshowServiceClient client = new RoadshowServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                customers = cache["customers"] as Customers2;
                if (customers == null) {
                    customers = client.GetCustomers2();
                    client.Close();

                    //CacheItemPolicy policy = new CacheItemPolicy();
                    //policy.ChangeMonitors.Add(new SqlChangeMonitor(new SqlDependency()));
                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("customers",customers,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return customers;
        }
        public static Customers2 GetCustomers2(string clientNumber) {
            //Get customers list
            Customers2 customers = new Customers2();
            try {
                Customers2 _customers = GetCustomers2();
                if (clientNumber != null && clientNumber.Length > 0) {
                    for (int i = 0;i < _customers.Count;i++) {
                        Customer2 c = _customers[i];
                        if (c.AccountID.Trim().Length > 0 && c.AccountID.Substring(0,3) == clientNumber) customers.Add(c);
                    }
                }
                else
                    customers = _customers;
            }
            catch (ApplicationException aex) { throw aex; }
            catch (Exception ce) { throw new ApplicationException(ce.Message,ce); }
            return customers;
        }
        public static Depots GetDepots() {
            //Get depots list
            Depots depots = null;
            RoadshowServiceClient client = new RoadshowServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                depots = cache["depots"] as Depots;
                if(depots == null) {
                    depots = client.GetDepots();
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("depots",depots,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return depots;
        }
        public static Drivers GetDrivers(int depotNumber) {
            //Get drivers list
            Drivers drivers = null;
            RoadshowServiceClient client = new RoadshowServiceClient();
            try {
                drivers = client.GetDrivers(depotNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> rfe) { client.Abort(); throw new ApplicationException(rfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return drivers;
        }
    }
}