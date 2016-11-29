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
        private bool _state = false;
        private string _address = "";

        private const string LOG_NAME = "Argix10";
        private const string LOG_SOURCE = "TLViewer";

		//Interface
        public FreightGateway() {
            TLViewerService2Client client = new TLViewerService2Client();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        public bool ServiceState { get { return _state; } }
        public string ServiceAddress { get { return _address; } }

        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(LogLevel level,string username,Exception ex) {
            //Write an entry into the Argix log
            TLViewerServiceClient client = new TLViewerServiceClient();
            try {
                TraceMessage tm = new TraceMessage();
                tm.Name = LOG_NAME;
                tm.LogLevel = level;
                tm.Date = DateTime.Now;
                tm.Source = LOG_SOURCE;
                tm.Category = tm.Event = "";
                tm.User = username;
                tm.Computer = "";
                tm.Keyword1 = tm.Keyword2 = tm.Keyword3 = "";
                tm.Message = ex.ToString();
                client.WriteLogEntry(tm);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public TerminalDataset GetTerminals(int terminalID) {
            //Returns a list of terminals
            TerminalDataset terminals = new TerminalDataset();
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetTerminals2();
                if (ds != null) {
                    TerminalDataset ts = new TerminalDataset();
                    ts.Merge(ds);
                    for (int i = 0;i < ts.TerminalTable.Rows.Count;i++) {
                        TerminalDataset.TerminalTableRow t = ts.TerminalTable[i];
                        if (terminalID == 0 || t.TerminalID == terminalID) {
                            terminals.TerminalTable.LoadDataRow(t.ItemArray, true);
                        }
                    }
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public DataView GetTLView(int terminalID,string sortBy) {
            //Get a view of TLs for the specified terminal
            TLDataset tls = new TLDataset();
            DataView view = tls.TLTable.DefaultView;
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                if (terminalID > 0) {
                    DataSet ds = client.GetTLView2(terminalID);
                    if (ds != null) {
                        tls.Merge(ds);
                        for (int i = 0;i < tls.TLTable.Rows.Count;i++) tls.TLTable[i].TerminalID = terminalID;
                        if (sortBy.Trim().Length == 0) sortBy = "TLNumber";
                        view.Sort = sortBy;
                    }
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return view;
        }
        public DataView GetTLDetail(int terminalID,string tlNumber,string sortBy) {
            //Get TL detail for the specified TL#
            TLDataset tls = new TLDataset();
            DataView view = tls.TLTable.DefaultView;
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetTLDetail2(terminalID,tlNumber);
                if (ds != null) {
                    tls.Merge(ds);

                    if (sortBy.Trim().Length == 0) sortBy = "ClientNumber";
                    view.Sort = sortBy;
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return view;
        }
        public DataView GetAgentSummary(int terminalID,string sortBy) {
            //Get an agent summary view for the specified terminal
            TLDataset tls = new TLDataset();
            DataView view = tls.TLTable.DefaultView;
            TLViewerService2Client client = new TLViewerService2Client();
            try {
                DataSet ds = client.GetAgentSummary2(terminalID);
                if (ds != null) {
                    tls.Merge(ds);
                    if (sortBy.Trim().Length == 0) sortBy = "AgentNumber";
                    view.Sort = sortBy;
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TLViewerFault> tle) { client.Abort(); throw new ApplicationException(tle.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return view;
        }
    }
}