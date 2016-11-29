using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Argix.Enterprise;

namespace Argix.Finance {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class InvoicingService: IInvoicingService {
        //Members

        //Interface
        public InvoicingService() { }
        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            return new AppService(InvoicingGateway.SQL_CONNID).GetServiceInfo(); ;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(InvoicingGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet GetClients() {
            //Get a list of clients
            DataSet clients = new DataSet();
            try {
                DataSet __clients = new DataSet();
                __clients.Merge(new InvoicingGateway().GetClients());
                clients = __clients.Clone();

                DataSet _clients = new DataSet();
                _clients.Merge(__clients.Tables["ClientTable"].Select("DivisionNumber='01'","ClientName ASC"));
                for (int i = 0;i < _clients.Tables["ClientTable"].Rows.Count;i++) {
                    DataRow client = _clients.Tables["ClientTable"].Rows[i];
                    if (InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + client["ClientNumber"] + "']") != null)
                        clients.Tables["ClientTable"].ImportRow(client);
                }
            }
            catch(Exception ex) { throw new FaultException<InvoicingFault>(new InvoicingFault(ex.Message),"Service Error"); }
            return clients;
        }
        public DataSet GetClientInvoices(string clientNumber,string clientDivision,string startDate) {
            //Get a list of clients invoices filtered for a specific division
            DataSet invoices = new DataSet();
            try {
                DataSet __invoices = new DataSet();
                __invoices.Merge(new InvoicingGateway().GetClientInvoices(clientNumber,clientDivision,startDate));
                invoices = __invoices.Clone();
                invoices.Tables["ClientInvoiceTable"].Columns.Add("InvoiceTypeTarget",typeof(string));

                string filter = getInvoiceFilter(clientNumber);
                DataSet _invoices = new DataSet();
                _invoices.Merge(__invoices.Tables["ClientInvoiceTable"].Select(filter,"InvoiceNumber ASC"));
                _invoices.Tables["ClientInvoiceTable"].Columns.Add("InvoiceTypeTarget",typeof(string));

                System.Xml.XmlNode node = InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + clientNumber + "']");
                if(node != null) {
                    System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
                    if(inv != null) {
                        for (int i = 0;i < _invoices.Tables["ClientInvoiceTable"].Rows.Count;i++) {
                            DataRow invoice = _invoices.Tables["ClientInvoiceTable"].Rows[i];

                            string invoiceType = invoice["InvoiceTypeCode"].ToString().Trim();
                            if (inv.Attributes[invoiceType] != null) invoice["InvoiceTypeTarget"] = inv.Attributes[invoiceType].Value;

                            invoices.Tables["ClientInvoiceTable"].ImportRow(invoice);
                        }
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<InvoicingFault>(new InvoicingFault(ex.Message),"Service Error"); }
            return invoices;
        }
        
        private string getInvoiceFilter(string clientNumber) {
            //Get invoice document target for specified client and invoice type
            string filter="InvoiceTypeCode=''";

            System.Xml.XmlNode node = InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + clientNumber + "']");
            if(node != null) {
                System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
                if(inv != null) {
                    string types = inv.Attributes["types"].Value;
                    if(types == "")
                        filter="InvoiceTypeCode=''";
                    else if(types == "*")
                        filter="";
                    else {
                        string[] codes = types.Split(',');
                        filter="";
                        for(int i=0;i<codes.Length;i++) {
                            if(i > 0) filter += " OR ";
                            filter += "InvoiceTypeCode='" + codes[i].Trim() + "'";
                        }
                    }
                }
            }
            return filter;
        }
        private string getTarget(string clientNumber,string invoiceType) {
            //Get invoice document target for specified client and invoice type
            string target="";

            System.Xml.XmlNode node = InvoicingConfig.Document.DocumentElement.SelectSingleNode("//client[@number='" + clientNumber + "']");
            if(node != null) {
                System.Xml.XmlNode inv = node.SelectSingleNode("invoices");
                if(inv != null) {
                    if(inv.Attributes[invoiceType] != null) target = inv.Attributes[invoiceType].Value;
                }
            }
            return target;
        }
    }

    internal static class InvoicingConfig {
        //Members
        private static System.Xml.XmlDocument xmlConfig=null;
        private const string INVOICE_CONFIG = "~\\App_Data\\Invoicing.xml";

        //Interface
        static InvoicingConfig() {
            xmlConfig = new System.Xml.XmlDocument();
            string path = System.Web.Hosting.HostingEnvironment.MapPath(INVOICE_CONFIG);
            xmlConfig.Load(path);
        }
        public static System.Xml.XmlDocument Document { get { return xmlConfig; } }
    }
}
