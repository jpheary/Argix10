using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Threading;
using Argix.Windows;

namespace Argix.Terminals {
	//
	public class TerminalsGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        private static int _cacheTimeout = 240;
       
		//Interface
        static TerminalsGateway() { 
            //
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private TerminalsGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static DeliveryPointsDataset GetDeliveryPoints(DateTime startDate,DateTime lastUpated) {
            //Get delivery points
            DeliveryPointsDataset points = new DeliveryPointsDataset();
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                DataSet ds = client.GetDeliveryPoints(startDate, lastUpated);
                if (ds != null) points.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TerminalsFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return points;
        }
        public static DateTime GetExportDate() {
            //Get the latest delivery point LastUpdated date from the last export
            DateTime date=DateTime.Now;
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                date = client.GetExportDate();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TerminalsFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return date;
        }
        public static bool UpdateExportDate(DateTime lastUpdated) {
            //Update the latest delivery point LastUpdated date from the last export
            bool updated=false;
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                updated = client.UpdateExportDate(lastUpdated);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TerminalsFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public static DeliveryPointsDataset GetCustomers() {
            //Get roadshow customers
            DeliveryPointsDataset customers = null;
            DeliveryPointsServiceClient client = new DeliveryPointsServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                customers = cache["customers"] as DeliveryPointsDataset;
                if (customers == null) {
                    customers = new DeliveryPointsDataset();
                    DataSet ds = client.GetRoadshowCustomers();
                    if (ds != null) customers.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("customers",customers,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<RoadshowFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return customers;
        }
    }
}