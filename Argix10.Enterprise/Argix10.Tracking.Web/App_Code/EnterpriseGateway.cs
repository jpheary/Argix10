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

public class EnterpriseGateway {
    //Members
    private int mLogLevelFloor = 0;

    public const string SQL_CONNID = "Enterprise";
    private const string USP_LOG_READ = "uspLogEntriesView", TBL_LOG_READ="LogEntryTable";
    private const string USP_LOG_NEW = "uspLogEntryNew";
    private const string LOG_NAME = "Argix10", LOG_SOURCE = "Tracking";

    //Interface
    public EnterpriseGateway() {
        this.mLogLevelFloor = Convert.ToInt32(WebConfigurationManager.AppSettings["LogLevelFloor"]);
    }

    public DataSet LogEntriesRead() {
        //Write log entry to database
        DataSet ds = new DataSet();
        try {
            ds = new DataService().FillDataset(SQL_CONNID,USP_LOG_READ,TBL_LOG_READ, new object[] {});
        }
        catch { }
        return ds;
    }
    public void WriteLogEntry(int logLevel,string username,Exception ex) {
        //Write log entry to database
        try {
            if(logLevel >= this.mLogLevelFloor) {
                new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOG_NEW, new object[] { LOG_NAME, logLevel, DateTime.Now, LOG_SOURCE, "", "", username, "", "", "", "", ex.ToString() });
                if(logLevel > 3) new EmailGateway().SendITNotification(username, ex);
            }
        }
        catch { }
    }
}
