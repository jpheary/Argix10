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

namespace Argix.AgentLineHaul {
	//
	public class AgentLineHaulGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static AgentLineHaulGateway() { 
            //
            ISDExportServiceClient client = new ISDExportServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private AgentLineHaulGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                terminal = client.GetServiceInfo(int.Parse(Program.TerminalCode));
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                config = client.GetUserConfiguration(int.Parse(Program.TerminalCode),application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                client.WriteLogEntry(int.Parse(Program.TerminalCode),m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static ISDExportDataset GetPickups(DateTime pickupDate) {
            //
            ISDExportDataset pickups = new ISDExportDataset();
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                DataSet ds = client.GetPickups(int.Parse(Program.TerminalCode), pickupDate);
                if (ds != null) pickups.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return pickups;
        }
        public static ISDExportDataset GetSortedItems(string pickupID) {
            //
            ISDExportDataset items = new ISDExportDataset();
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                DataSet ds = client.GetSortedItems(int.Parse(Program.TerminalCode),pickupID);
                if (ds != null) items.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return items;
        }
        public static string GetExportFilename(string counterKey) {
            //
            string filename = "";
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                filename = client.GetExportFilename(int.Parse(Program.TerminalCode),counterKey);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return filename;
        }

        public static ISDExportDataset GetISDClients() { return GetISDClients(null); }
        public static ISDExportDataset GetISDClients(string clientNumber) {
            //
            ISDExportDataset clients = new ISDExportDataset();
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                DataSet ds = client.GetISDClients(int.Parse(Program.TerminalCode),clientNumber);
                if (ds != null) clients.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }
        public static bool CreateISDClient(ISDClient isdClient) {
            //
            bool created = false;
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                created = client.CreateISDClient(int.Parse(Program.TerminalCode),isdClient);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return created;
        }
        public static bool UpdateISDClient(ISDClient isdClient) {
            //
            bool updated = false;
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                updated = client.UpdateISDClient(int.Parse(Program.TerminalCode),isdClient);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool DeleteISDClient(ISDClient isdClient) {
            //
            bool deleted = false;
            ISDExportServiceClient client = new ISDExportServiceClient();
            try {
                deleted = client.DeleteISDClient(int.Parse(Program.TerminalCode),isdClient);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return deleted;
        }
    }
}