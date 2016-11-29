using System;
using System.Data;
using System.Web.Configuration;
using System.Web;
using System.Data.SqlClient;
using System.Data.Common;
using System.Collections;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Enterprise {
    //
    public class EnterpriseGateway {
        //Members
        public const string SQL_CONNID = "Enterprise";

        private const string USP_LOG_READ = "uspLogEntriesView",TBL_LOG_READ = "LogEntryTable";
        private const string USP_LOG_NEW = "uspLogEntryNew";

        private const string LOG_NAME = "Argix10";
        private const string LOG_SOURCE = "PalletShipment";

        //Interface
        public EnterpriseGateway() { }

        public DataSet LogEntriesRead() {
            //Write log entry to database
            DataSet ds = new DataSet();
            try {
                ds = new DataService().FillDataset(SQL_CONNID,USP_LOG_READ,TBL_LOG_READ,new object[] { });
            }
            catch { }
            return ds;
        }
        public void WriteLogEntry(int logLevel,string username,Exception ex) {
            //Write log entry to database
            try {
                new DataService().ExecuteNonQuery(SQL_CONNID,USP_LOG_NEW,new object[] { LOG_NAME,logLevel,DateTime.Now,LOG_SOURCE,"","",username,"","","","",ex.ToString() });
            }
            catch { }
        }
    }
}
