using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public class TsortGateway {
        //Members	
        private const string SQL_CONNID = "Tsort";
        public const string USP_CLIENTS = "uspClientGetList",TBL_CLIENTS = "ClientTable";
        public const string USP_LOADTENDERS = "uspLoadTenderGetList",TBL_LOADTENDERS = "LoadTenderTable";
        public const string USP_LOADTENDERDETAILS = "uspLoadTenderDetailGetList",TBL_LOADTENDERDETAILS = "LoadTenderDetailTable";
        public const int CMD_TIMEOUT_DEFAULT = 300;

        //Interface
        static TsortGateway() { }
        private TsortGateway() { }

        public static ServiceInfo GetServiceInfo() { return new AppService(SQL_CONNID).GetServiceInfo(); }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) { return new AppService(SQL_CONNID).GetUserConfiguration(application,usernames); }
        public static void WriteLogEntry(TraceMessage m) { new AppService(SQL_CONNID).WriteLogEntry(m); }
        public static ClientDS GetClients() {
            //Create a list of load tender clients
            ClientDS clients = null;
            try {
                clients = new ClientDS();
                clients.ClientTable.AddClientTableRow("012","01","L'OCCITANE","05","A");
                clients.ClientTable.AddClientTableRow("014","01","MELVITA","05","A");
                clients.ClientTable.AddClientTableRow("025","01","PRATT RETAIL SPECIALTIES","05","A");
                clients.ClientTable.AcceptChanges();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return clients;
        }
        public static LoadTenderDS GetLoadTenders(string clientNumber,DateTime startDate,DateTime endDate) {
        //Get load tenders for the selected client and date range
        LoadTenderDS lts = null;
        try {
            lts = new LoadTenderDS();
            DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_LOADTENDERS,TBL_LOADTENDERS,new object[] { clientNumber,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
            if(ds != null && ds.Tables[TBL_LOADTENDERS].Rows.Count > 0)
                lts.Merge(ds);
        }
        catch (Exception ex) { throw new ApplicationException(ex.Message); }
        return lts;
    }
        public static LoadTenderDetailDS GetLoadTenderDetails(string loadNumber) {
            //Get load tender details
            LoadTenderDetailDS lts = null;
            try {
                lts = new LoadTenderDetailDS();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_LOADTENDERDETAILS,TBL_LOADTENDERDETAILS,new object[] { loadNumber });
                if (ds != null && ds.Tables[TBL_LOADTENDERDETAILS].Rows.Count > 0) lts.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return lts;
        }
    }
}
