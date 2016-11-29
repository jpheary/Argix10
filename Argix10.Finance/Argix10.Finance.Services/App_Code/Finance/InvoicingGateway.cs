using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Finance {
    //
    public class InvoicingGateway {
        //Members
        public const string SQL_CONNID = "Invoicing";
        private const string USP_CLIENTS = "uspInvClientGetList",TBL_CLIENTS = "ClientTable";
        private const string USP_INVOICES = "uspInvClientInvoiceGetListAllTypes",TBL_INVOICES = "ClientInvoiceTable";       

        //Interface
        public InvoicingGateway() { }

        public DataSet GetClients() {
            //Get a list of clients
            DataSet clients = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds != null && ds.Tables[TBL_CLIENTS].Rows.Count > 0) clients.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return clients;
        }
        public DataSet GetClientInvoices(string clientNumber,string clientDivision,string startDate) {
            //Get a list of clients invoices filtered for a specific division
            DataSet invoices = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_INVOICES,TBL_INVOICES,new object[] { clientNumber,clientDivision,startDate });
                if(ds != null && ds.Tables[TBL_INVOICES].Rows.Count > 0) invoices.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return invoices;
        }
    }
}
