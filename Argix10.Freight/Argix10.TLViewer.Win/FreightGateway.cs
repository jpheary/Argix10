using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Freight {
	//
	public class FreightGateway {
		//Members
        private static bool _state=false;
        private static string _address="";

		//Interface
        static FreightGateway() { 
            //
            TLViewerServiceClient client = new TLViewerServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private FreightGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            TLViewerServiceClient client = null; 
            try {
                client = new TLViewerServiceClient();
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            TLViewerServiceClient client = null;
            try {
                client = new TLViewerServiceClient();
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
        }
        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            TLViewerServiceClient client = null;
            try {
                client = new TLViewerServiceClient();
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return terminal;
        }

        public static FreightDataset GetTerminals() {
            //Returns a list of terminals
            FreightDataset terminals = new FreightDataset();
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetTerminals2();
                if (ds != null && ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0) {
                    if (Program.TerminalCode.Length > 0)
                        terminals.Merge(ds.Tables["TerminalTable"].Select("TerminalID=" + Program.TerminalCode));
                    else
                        terminals.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return terminals;
        }
        public static FreightDataset GetTLView(int terminalID) {
            //Get a view of TLs for the opertaing terminal
            FreightDataset tls = new FreightDataset();
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetTLView2(terminalID);
                if (ds != null && ds.Tables["TLTable"] != null && ds.Tables["TLTable"].Rows.Count > 0) {
                    tls.Merge(ds);
                    for (int i = 0;i < tls.TLTable.Rows.Count;i++) tls.TLTable[i].TerminalID = terminalID;
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return tls;
        }
        public static FreightDataset GetTLDetail(int terminalID, string tlNumber) {
            //Get TL detail for the specified TL#
            FreightDataset tls = new FreightDataset();
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetTLDetail2(terminalID,tlNumber);
                if (ds != null && ds.Tables["TLTable"] != null && ds.Tables["TLTable"].Rows.Count > 0) 
                    tls.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return tls;
        }
        public static FreightDataset GetAgentSummary(int terminalID) {
            //Get an agent summary view for the specified terminal
            FreightDataset tls = new FreightDataset();
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetAgentSummary2(terminalID);
                if (ds != null && ds.Tables["TLTable"] != null && ds.Tables["TLTable"].Rows.Count > 0) 
                    tls.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return tls;
        }
    }
}