using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;

namespace Argix.Reporting {
	//
	public class ReportingGateway {
		//Members
        private static ReportingService2010 _Client = null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static ReportingGateway() { 
            //
            _Client = new ReportingService2010();
            _state = true;
            _address = _Client.Url;
        }
        private ReportingGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static SubscriptionDataset GetSubscriptions() {
            //Get subscriptions  for the specidied report
            string reportPath = "/Customer Service/Internal Delivery Window By Store";
            SubscriptionDataset subscriptionDataset = new SubscriptionDataset();
            try {
                //Create reporting service web client proxy
                _Client = new ReportingService2010();
                _Client.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Request all subscriptions for the specified report
                Subscription[] subscriptions = _Client.ListSubscriptions(reportPath);
                if (subscriptions != null) {
                    //Enumerate all subscriptions
                    for (int i = 0;i < subscriptions.Length;i++) {
                        //Get subscription properties
                        Subscription sub = subscriptions[i];
                        ExtensionSettings extSettings = null;
                        ActiveState active = null;
                        string desc = "",status = "",eventType = "",matchData = "";
                        ParameterValue[] paramValues = null;
                        _Client.GetSubscriptionProperties(subscriptions[i].SubscriptionID,out extSettings,out desc,out active,out status,out eventType,out matchData,out paramValues);

                        string agentNumber = getParamValue(paramValues,"AgentNumber");
                        string endDate = getParamValue(paramValues,"EndDate");

                        subscriptionDataset.SubscriptionTable.AddSubscriptionTableRow(sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings),agentNumber,endDate);
                    }
                }
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            return subscriptionDataset;
        }
        public static string CreateSubscription(string agentNumber, string agentRepEmail) {
            //Create a new subscription
            string reportPath = "/Customer Service/Internal Delivery Window By Store";
            string subscriptionID = "";
            try {
                //Create reporting service web client proxy
                _Client = new ReportingService2010();
                _Client.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //
                ExtensionSettings extSettings = setExtSettings(agentRepEmail);
                string description = "Send e-mail to " + agentRepEmail;
                string eventType = "TimedSubscription";
                string matchData = setMatchData(DateTime.Today);
                ParameterValue pv1 = new ParameterValue(); pv1.Name = "AgentNumber"; pv1.Value = agentNumber;
                ParameterValue pv2 = new ParameterValue(); pv2.Name = "EndDate"; pv2.Value = null;
                ParameterValue pv3 = new ParameterValue(); pv3.Name = "ClientNumber"; pv3.Value = null;
                ParameterValue pv4 = new ParameterValue(); pv4.Name = "Division"; pv4.Value = null;
                ParameterValue pv5 = new ParameterValue(); pv5.Name = "District"; pv5.Value = null;
                ParameterValue pv6 = new ParameterValue(); pv6.Name = "Region"; pv6.Value = null;
                ParameterValue pv7 = new ParameterValue(); pv7.Name = "StoreNumber"; pv7.Value = null;
                ParameterValue[] parameters = new ParameterValue[] { pv4,pv1,pv5,pv6,pv7,pv3 };
                subscriptionID = _Client.CreateSubscription(reportPath,extSettings,description,eventType,matchData,parameters);
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            return subscriptionID;
        }
        public static void DeleteSubscription(string subscriptionID) {
            //Delete the specified subscription
            try {
                //Create reporting service web client proxy
                _Client = new ReportingService2010();
                _Client.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Delete
                _Client.DeleteSubscription(subscriptionID);
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
        }
        #region Local Services: getSubjectLine(), setSubjectLine(), getParamValue(), getExtSettings(), getMatchData(), setMatchData(), getParamValues()
        private static string getSubjectLine(ExtensionSettings extSettings) {
            //
            string subjectLine = "";
            ParameterValueOrFieldReference[] extensionParams = extSettings.ParameterValues;
            if (extensionParams != null) {
                foreach (ParameterValueOrFieldReference extensionParam in extensionParams) {
                    if (((ParameterValue)extensionParam).Name.ToLower() == "subject") {
                        subjectLine = ((ParameterValue)extensionParam).Value;
                        break;
                    }
                }
            }
            return subjectLine;
        }
        private static void setSubjectLine(ExtensionSettings extSettings,string subject) {
            ParameterValueOrFieldReference[] extensionParams = extSettings.ParameterValues;
            string existingValue;
            int indexOfVariableChar;
            if (extensionParams != null) {
                foreach (ParameterValueOrFieldReference extensionParam in extensionParams) {
                    if (((ParameterValue)extensionParam).Name.ToLower() == "subject") {
                        existingValue = ((ParameterValue)extensionParam).Value;
                        indexOfVariableChar = existingValue.IndexOf("~");
                        if (indexOfVariableChar >= 0)
                            ((ParameterValue)extensionParam).Value = subject + existingValue.Substring(indexOfVariableChar - 1);
                        else
                            ((ParameterValue)extensionParam).Value = subject + " ~ " + existingValue;
                        break;
                    }
                }
            }
        }
        private static string getParamValue(ParameterValue[] paramValues,string paramName) {
            //
            string paramValue = "";
            for (int i = 0;i < paramValues.Length;i++) {
                if (paramValues[i].Name == paramName) { paramValue = paramValues[i].Value; break; }
            }
            return paramValue;
        }
        private static string getExtSettings(ExtensionSettings extSettings) {
            //
            string extensions = "";
            for (int i = 0;i < extSettings.ParameterValues.Length;i++)
                extensions += ((ParameterValue)extSettings.ParameterValues[i]).Name + ": " + ((ParameterValue)extSettings.ParameterValues[i]).Value + "; ";
            return extensions;
        }
        private static ExtensionSettings setExtSettings(string agentRepEmail) {
            //
            ExtensionSettings extSettings = new ExtensionSettings();
            extSettings.Extension = "Report Server Email";
            ParameterValue pv0 = new ParameterValue(); pv0.Name = "TO"; pv0.Value = agentRepEmail;
            ParameterValue pv1 = new ParameterValue(); pv1.Name = "CC"; pv1.Value = "";
            ParameterValue pv2 = new ParameterValue(); pv2.Name = "IncludeReport"; pv2.Value = "True";
            ParameterValue pv3 = new ParameterValue(); pv3.Name = "RenderFormat"; pv3.Value = "PDF";
            ParameterValue pv4 = new ParameterValue(); pv4.Name = "Subject"; pv4.Value = "@ReportName was executed at @ExecutionTime";
            ParameterValue pv5 = new ParameterValue(); pv5.Name = "IncludeLink"; pv5.Value = "False";
            ParameterValue pv6 = new ParameterValue(); pv6.Name = "Priority"; pv6.Value = "NORMAL";
            extSettings.ParameterValues = new ParameterValueOrFieldReference[]{pv0,pv1,pv2,pv3,pv4,pv5,pv6};
            return extSettings;
        }
        private static string getMatchData() {
            //
            //<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\"?>
            //<ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">
            //  <StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">2011-06-20T06:00:00.000-04:00</StartDateTime>
            //  <WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">
            //      <WeeksInterval>1</WeeksInterval>
            //      <DaysOfWeek>
            //          <Wednesday>true</Wednesday>
            //      </DaysOfWeek>
            //  </WeeklyRecurrence>
            //</ScheduleDefinition>
            return "<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\"?><ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">2011-06-20T06:00:00.000-04:00</StartDateTime><WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\"><WeeksInterval>1</WeeksInterval><DaysOfWeek><Wednesday>true</Wednesday></DaysOfWeek></WeeklyRecurrence></ScheduleDefinition>";
        }
        private static string setMatchData(DateTime startDate) {
            //<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\"?>
            //<ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">
            //    <StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">2012-12-21T08:00:00.000-05:00</StartDateTime>
            //    <WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">
            //    <WeeksInterval>1</WeeksInterval>
            //        <DaysOfWeek>
            //            <Wednesday>true</Wednesday>
            //        </DaysOfWeek>
            //    </WeeklyRecurrence>
            //</ScheduleDefinition>
            string startDateTime = startDate.ToString("yyyy-MM-dd") + "T09:00:00-08:00";
            string scheduleXml = "<ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
            scheduleXml += "<StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">" + "2012-12-21T08:00:00.000-05:00" + "</StartDateTime>";
            scheduleXml += "<WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">";
            scheduleXml += "<WeeksInterval>1</WeeksInterval>";
            scheduleXml += "<DaysOfWeek><Wednesday>true</Wednesday></DaysOfWeek>";
            scheduleXml += "</WeeklyRecurrence></ScheduleDefinition>";
            return scheduleXml;
        }
        private static string getParamValues(ParameterValue[] paramValues) {
            //
            string parameters = "";
            for (int i = 0;i < paramValues.Length;i++)
                parameters += paramValues[i].Name + ": " + paramValues[i].Value + "; ";
            return parameters;
        }
        #endregion
    }
}