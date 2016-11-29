using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Shipping {
	//
	public class TLViewerGateway {
		//Members

		//Interface
        public TLViewerGateway() { }
        public CommunicationState ServiceState { get { return new TLViewerServiceClient().State; } }
        public string ServiceAddress { get { return new TLViewerServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public ServiceInfo GetServiceInfo(int terminalID) {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(int terminalID,TraceMessage m) {
            //Get the operating enterprise terminal
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public DataSet GetTerminals() {
            //Get a list of Argix terminals
            DataSet terminals = null;
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                terminals = client.GetTerminals2();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public DataSet GetTLView(int terminalID) {
            //Get a view of open TLs
            DataSet tls = null;
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                tls = client.GetTLView2(terminalID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tls;
        }
        public TLs GetTLDetail(int terminalID,string tlNumber) {
            //Get a view of open TLs
            TLs tls = new TLs();
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                tls = client.GetTLDetail(terminalID,tlNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tls;
        }
        public TLs GetAgentSummary(int terminalID) {
            //Get a view of open TLs
            TLs tls = new TLs();
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                tls = client.GetAgentSummary(terminalID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tls;
        }
    }
}