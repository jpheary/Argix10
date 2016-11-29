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

        public static SubscriptionDataset GetSubscriptions(string reportPath) {
            //Get subscriptions  for the specidied report
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

                        string routeDate = getParamValue(paramValues,"RouteDate");
                        string routeClass = getParamValue(paramValues,"RouteClass");
                        string driverName = getParamValue(paramValues,"DriverName");

                        subscriptionDataset.SubscriptionTable.AddSubscriptionTableRow(sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings),routeDate,routeClass,driverName);
                    }
                }
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return subscriptionDataset;
        }
        public static string CreateSubscription(string reportPath,string routeClass,string driverName) {
            //Create a new subscription
            string subscriptionID = "";
            try {
                //Create reporting service web client proxy
                _Client = new ReportingService2010();
                _Client.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //
                ExtensionSettings extSettings = setExtSettings();
                string description = "Send e-mail to cgormley@argixdirect.com";
                string eventType = "TimedSubscription";
                string matchData = setMatchData(DateTime.Today);
                ParameterValue pv1 = new ParameterValue(); pv1.Name = "RouteDate"; pv1.Value = null;
                ParameterValue pv2 = new ParameterValue(); pv2.Name = "RouteClass"; pv2.Value = routeClass;
                ParameterValue pv3 = new ParameterValue(); pv3.Name = "DriverName"; pv3.Value = driverName;
                ParameterValue[] parameters = new ParameterValue[]{pv1,pv2,pv3};
                subscriptionID = _Client.CreateSubscription(reportPath,extSettings,description,eventType,matchData,parameters);
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
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
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
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
        private static ExtensionSettings setExtSettings() {
            //
            ExtensionSettings extSettings = new ExtensionSettings();
            extSettings.Extension = "Report Server Email";
            ParameterValue pv0 = new ParameterValue(); pv0.Name = "TO"; pv0.Value = "cgormley@argixdirect.com";
            ParameterValue pv1 = new ParameterValue(); pv1.Name = "CC"; pv1.Value = "gmasteller@argixdirect.com;";
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
            //          <Monday>true</Monday>
            //          <Tuesday>true</Tuesday>
            //          <Wednesday>true</Wednesday>
            //          <Thursday>true</Thursday>
            //          <Friday>true</Friday>
            //      </DaysOfWeek>
            //  </WeeklyRecurrence>
            //</ScheduleDefinition>
            return "<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\"?><ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\"><StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">2011-06-20T06:00:00.000-04:00</StartDateTime><WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\"><WeeksInterval>1</WeeksInterval><DaysOfWeek><Monday>true</Monday><Tuesday>true</Tuesday><Wednesday>true</Wednesday><Thursday>true</Thursday><Friday>true</Friday></DaysOfWeek></WeeklyRecurrence></ScheduleDefinition>";
        }
        private static string setMatchData(DateTime startDate) {
            //<?xml version=\"1.0\" encoding=\"utf-16\" standalone=\"yes\"?>
            //<ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">
            //  <StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">2011-06-20T06:00:00.000-04:00</StartDateTime>
            //  <WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">
            //      <WeeksInterval>1</WeeksInterval>
            //      <DaysOfWeek>
            //          <Monday>true</Monday>
            //          <Tuesday>true</Tuesday>
            //          <Wednesday>true</Wednesday>
            //          <Thursday>true</Thursday>
            //          <Friday>true</Friday>
            //      </DaysOfWeek>
            //  </WeeklyRecurrence>
            //</ScheduleDefinition>
            string startDateTime = startDate.ToString("yyyy-MM-dd") + "T09:00:00-08:00";
            string scheduleXml = "<ScheduleDefinition xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">";
            scheduleXml += "<StartDateTime xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">" + startDateTime + "</StartDateTime>";
            scheduleXml += "<WeeklyRecurrence xmlns=\"http://schemas.microsoft.com/sqlserver/reporting/2010/03/01/ReportServer\">";
            scheduleXml += "<WeeksInterval>1</WeeksInterval>";
            scheduleXml += "<DaysOfWeek><Monday>true</Monday><Tuesday>true</Tuesday><Wednesday>true</Wednesday><Thursday>true</Thursday><Friday>true</Friday></DaysOfWeek>";
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