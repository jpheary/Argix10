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
using Argix.Windows;

namespace Argix.HR {
	//
	public class KronosProxy {
		//Members
        private static KronosServiceClient _Client=null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static KronosProxy() { 
            //
            _Client = new KronosServiceClient();
            _state = true;
            _address = _Client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private KronosProxy() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            try {
                _Client = new KronosServiceClient();
                terminal = _Client.GetServiceInfo();
                _Client.Close();
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { _Client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            try {
                _Client = new KronosServiceClient();
                config = _Client.GetUserConfiguration(application,usernames);
                _Client.Close();
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { _Client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            try {
                _Client = new KronosServiceClient();
                _Client.WriteLogEntry(m);
                _Client.Close();
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { _Client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static object[] GetIDTypes() {
            //Get invoices for the specified client
            object[] types = null;
            try {
                _Client = new KronosServiceClient();
                types = _Client.GetIDTypes();
                _Client.Close();
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { _Client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return types;
        }
        public static Employees GetEmployees(string idType) {
            //Get client list
            Employees employees = null;
            try {
                _Client = new KronosServiceClient();
                employees = _Client.GetEmployees(idType);
                _Client.Close();
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { _Client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return employees;
        }
    }
}