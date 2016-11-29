using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.HR {
	//
	public class BadgeGateway {
		//Members
        private static bool _state = false;
        private static string _address = "";
        
		//Interface
        static BadgeGateway() { 
            //
            Argix.HR.BadgeServiceClient client = new BadgeServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private BadgeGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            BadgeServiceClient client = new BadgeServiceClient();
            try {
                return client.GetServiceInfo();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the application configuration for the specified user
            UserConfiguration config=null;
            BadgeServiceClient client = new BadgeServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(string message,Argix.HR.LogLevel level,string user) {
            //Write an entry into the Argix log
            BadgeServiceClient client = new BadgeServiceClient();
            try {
                Argix.HR.TraceMessage m = new Argix.HR.TraceMessage();
                m.Date = DateTime.Now;
                m.Name = "Argix10";
                m.Source = App.Product;
                m.User = user;
                m.Computer = Environment.MachineName;
                m.LogLevel = level;
                m.Message = message;
                client.WriteLogEntry(m);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static BadgeDataset ViewAccessControlBadges() {
            //
            BadgeDataset badges = new BadgeDataset();
            AccessControlBadgeServiceClient client = new AccessControlBadgeServiceClient();
            try {
                DataSet ds = client.ViewAccessControlBadges();
                if (ds != null) badges.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return badges;
        }
    }
}