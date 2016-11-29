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

namespace Argix.AgentLineHaul {
	//
	public class ShipScheduleTemplatesGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static ShipScheduleTemplatesGateway() { 
            //
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private ShipScheduleTemplatesGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
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
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
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
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                client.WriteLogEntry(m);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        
        public static TemplateDataset GetShippersAndTerminals() {
            //Get terminals list
            TemplateDataset terminals = new TemplateDataset();
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                DataSet ds = client.GetShippersAndTerminals();
                if (ds != null) terminals.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public static TemplateDataset GetCarriers() {
            //Get carriers list
            TemplateDataset carriers = new TemplateDataset();
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                DataSet ds = client.GetCarriers();
                if (ds != null) carriers.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return carriers;
        }
        public static TemplateDataset GetAgents() {
            //Get agents list
            TemplateDataset agents = new TemplateDataset();
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                DataSet ds = client.GetAgents();
                if (ds != null) agents.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ShipScheduleFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public static DataSet GetDaysOfWeek() {
            //Get days of the week list
            DataSet days = null;
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                days = client.GetDaysOfWeek();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return days;
        }
        public static int GetWeekday(string weekdayName) {
            //Get weekday as a number
            int weekday = 0;
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                weekday = client.GetWeekday(weekdayName);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return weekday;
        }

        public static TemplateDataset GetTemplates() {
            //Get templates list
            TemplateDataset templates = new TemplateDataset();
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                DataSet ds = client.GetTemplates();
                if(ds != null) templates.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch (FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return templates;
        }
        public static string AddTemplate(ShipScheduleTemplate template) {
            //
            string templateID="";
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                templateID = client.AddTemplate(template);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch (FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return templateID;
        }
        public static bool UpdateTemplate(ShipScheduleTemplate template) {
            //
            bool ret = false;
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                ret = client.UpdateTemplate(template);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch (FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return ret;
        }

        public static System.IO.Stream GetExportDefinition() {
            //
            System.IO.Stream xml = null;
            ShipScheduleTemplatesServiceClient client = new ShipScheduleTemplatesServiceClient();
            try {
                xml = client.GetExportDefinition();
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te.InnerException); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return xml;
        }
    }
}