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

namespace Argix.Finance {
	//
	public class FinanceGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static FinanceGateway() { 
            //
            InvoicingServiceClient client = new InvoicingServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private FinanceGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal=null;
            InvoicingServiceClient client = null;
            try {
                client = new InvoicingServiceClient();
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            InvoicingServiceClient client = null;
            try {
                client = new InvoicingServiceClient();
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            InvoicingServiceClient client = null;
            try {
                client = new InvoicingServiceClient();
                client.WriteLogEntry(m);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public static InvoicingDataset GetClients() {
            //Get client list
            InvoicingDataset clients = new InvoicingDataset();
            InvoicingServiceClient client = null;
            try {
                client = new InvoicingServiceClient();
                DataSet ds = client.GetClients();
                if (ds.Tables["ClientTable"] != null && ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<InvoicingFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }
        public static InvoicingDataset GetClientInvoices(string clientNumber,string clientDivision,string startDate) {
            //Get invoices for the specified client
            InvoicingDataset invoices = new InvoicingDataset();
            InvoicingServiceClient client = null;
            try {
                client = new InvoicingServiceClient();
                DataSet ds = client.GetClientInvoices(clientNumber, clientDivision, startDate);
                if (ds.Tables["ClientInvoiceTable"] != null && ds.Tables["ClientInvoiceTable"].Rows.Count > 0) invoices.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<InvoicingFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return invoices;
        }
    }
}