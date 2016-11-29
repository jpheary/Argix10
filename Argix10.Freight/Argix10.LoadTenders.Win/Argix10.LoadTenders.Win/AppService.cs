using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

//
namespace Argix {
    //
    public class ServiceInfo {
        private int mTerminalID = 0;
        private string mNumber = "",mDescription = "",mConnection = "";

        public int TerminalID { get { return this.mTerminalID; } set { this.mTerminalID = value; } }
        public string Number { get { return this.mNumber; } set { this.mNumber = value; } }
        public string Description { get { return this.mDescription; } set { this.mDescription = value; } }
        public string Connection { get { return this.mConnection; } set { this.mConnection = value; } }
    }

    public class UserConfiguration:Dictionary<string,string> {
        //Members
        private string mApplication = "";
        public const string USER_DEFAULT = "Default";
        public const string USER_NEW = "<New User>";

        //Interface
        public UserConfiguration() { }
        public UserConfiguration(string application) { this.mApplication = application; }
    }

    public enum LogLevel {
        None,
        Debug,
        Information,
        Warning,
        Error
    }

    public class TraceMessage {
        //Members
        private long mID = 0;
        private string mName = "";
        private DateTime mDate;
        private string mCategory = "",mEvent = "";
        private string mUser = Environment.UserName,mComputer = Environment.MachineName;
        private string mSource = "",mMessage = "";
        private LogLevel mLogLevel = LogLevel.None;
        private string mKeyword1 = "",mKeyword2 = "",mKeyword3 = "";

        //Interface
        public TraceMessage() { }
        public TraceMessage(string user,string computer,string message,string source,LogLevel logLevel) : this(user,computer,message,source,logLevel,"","","") { }
        public TraceMessage(string user,string computer,string message,string source,LogLevel logLevel,string keyData1,string keyData2,string keyData3) {
            //Constructor
            this.mUser = user;
            this.mComputer = computer;
            this.mMessage = message;
            this.mSource = source;
            this.mLogLevel = logLevel;
            this.mKeyword1 = keyData1;
            this.mKeyword2 = keyData2;
            this.mKeyword3 = keyData3;
        }
        public long ID { get { return this.mID; } set { this.mID = value; } }
        public string Name { get { return this.mName; } set { this.mName = value; } }
        public DateTime Date { get { return this.mDate; } set { this.mDate = value; } }
        public string Category { get { return this.mCategory; } set { this.mCategory = value; } }
        public string Event { get { return this.mEvent; } set { this.mEvent = value; } }
        public string User { get { return this.mUser; } set { this.mUser = value; } }
        public string Computer { get { return this.mComputer; } set { this.mComputer = value; } }
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
        public string Source { get { return this.mSource; } set { this.mSource = value; } }
        public LogLevel LogLevel { get { return this.mLogLevel; } set { this.mLogLevel = value; } }
        public string Keyword1 { get { return this.mKeyword1; } set { this.mKeyword1 = value; } }
        public string Keyword2 { get { return this.mKeyword2; } set { this.mKeyword2 = value; } }
        public string Keyword3 { get { return this.mKeyword3; } set { this.mKeyword3 = value; } }
    }

    public class AppService {
        //Members
        private string mConnectionID = "";
        private LogLevel mLogLevelFloor = LogLevel.None;

        private const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet",TBL_LOCALTERMINAL = "TerminalTable";
        private const string USP_CONFIG_GETLIST = "uspToolsConfigurationGetList",TBL_CONFIGURATION = "ConfigTable";
        private const string USP_LOG_NEW = "uspLogEntryNew";

        //Interface
        public AppService(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
            this.mLogLevelFloor = (LogLevel)Convert.ToInt32(ConfigurationManager.AppSettings["LogLevelFloor"]);
        }

        public ServiceInfo GetServiceInfo() {
            //Get information about this service
            ServiceInfo info = null;
            try {
                info = new ServiceInfo();
                info.Connection = DatabaseFactory.CreateDatabase(this.mConnectionID).ConnectionStringWithoutCredentials;
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_LOCALTERMINAL,TBL_LOCALTERMINAL,new object[] { });
                if (ds != null && ds.Tables[TBL_LOCALTERMINAL].Rows.Count > 0) {
                    info.TerminalID = Convert.ToInt32(ds.Tables[TBL_LOCALTERMINAL].Rows[0]["TerminalID"]);
                    info.Number = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Number"].ToString().Trim();
                    info.Description = ds.Tables[TBL_LOCALTERMINAL].Rows[0]["Description"].ToString().Trim();
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return info;
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            UserConfiguration config = null;
            try {
                //
                config = new UserConfiguration(application);
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_CONFIG_GETLIST,TBL_CONFIGURATION,new object[] { application });
                if (ds != null && ds.Tables[TBL_CONFIGURATION] != null) {
                    for (int i = 0;i < ds.Tables[TBL_CONFIGURATION].Rows.Count;i++) {
                        string userName = ds.Tables[TBL_CONFIGURATION].Rows[i]["PCName"].ToString();
                        string key = ds.Tables[TBL_CONFIGURATION].Rows[i]["Key"].ToString();
                        string value = ds.Tables[TBL_CONFIGURATION].Rows[i]["Value"].ToString();
                        if (!config.ContainsKey(key)) {
                            if (userName.ToLower() == UserConfiguration.USER_DEFAULT.ToLower())
                                config.Add(key,value);
                            else {
                                for (int j = 0;j < usernames.Length;j++) {
                                    if (userName.ToLower() == usernames[j].ToLower())
                                        config.Add(key,value);
                                }
                            }
                        }
                        else {
                            for (int j = 0;j < usernames.Length;j++) {
                                if (userName.ToLower() == usernames[j].ToLower())
                                    config[key] = value;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return config;
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            try {
                if (m.LogLevel >= this.mLogLevelFloor) {
                    string message = (m.Message != null) ? m.Message : "";
                    message = message.Replace("\\n"," ");
                    message = message.Replace("\r","");
                    message = message.Replace("\n","");
                    string category = (m.Category != null) ? m.Category : "";
                    string _event = (m.Event != null) ? m.Event : "";
                    string keyData1 = (m.Keyword1 != null) ? m.Keyword1 : "";
                    string keyData2 = (m.Keyword2 != null) ? m.Keyword2 : "";
                    string keyData3 = (m.Keyword3 != null) ? m.Keyword3 : "";

                    new DataService().ExecuteNonQuery(this.mConnectionID,USP_LOG_NEW,new object[] { m.Name,Convert.ToInt32(m.LogLevel),DateTime.Now,m.Source,category,_event,m.User,m.Computer,keyData1,keyData2,keyData3,message });
                }
            }
            catch { }
        }
    }
}
