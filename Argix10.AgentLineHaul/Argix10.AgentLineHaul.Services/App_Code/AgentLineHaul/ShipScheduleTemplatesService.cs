using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Data.SqlClient;
using System.Text;
using System.Transactions;
using Argix.Enterprise;

namespace Argix.AgentLineHaul {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ShipScheduleTemplatesService:IShipScheduleTemplatesService {
        //Members

        //Interface
        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(ShipScheduleTemplatesGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(ShipScheduleTemplatesGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet GetShippersAndTerminals() {
            //Get a list of shippers and terminals
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new ShipScheduleTemplatesGateway().GetShippersAndTerminals();
                if(ds != null && ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return terminals;
        }
        public DataSet GetCarriers() {
            //Get a list of carriers
            DataSet carriers = new DataSet();
            try {
                DataSet ds = new ShipScheduleTemplatesGateway().GetCarriers();
                if(ds != null && ds.Tables["CarrierTable"] != null && ds.Tables["CarrierTable"].Rows.Count > 0) carriers.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return carriers;
        }
        public DataSet GetAgents() {
            //Get a list of agents
            DataSet agents = new DataSet();
            try {
                DataSet ds = new ShipScheduleTemplatesGateway().GetAgents();
                if(ds != null && ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) agents.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return agents;
        }
        public DataSet GetDaysOfWeek() {
            DataSet daysOfWeek = new DataSet();
            daysOfWeek.Tables.Add("SelectionListTable");
            daysOfWeek.Tables["SelectionListTable"].Columns.Add("ID");
            daysOfWeek.Tables["SelectionListTable"].Columns.Add("Description");
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 1,"Mon" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 2,"Tue" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 3,"Wed" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 4,"Thu" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 5,"Fri" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 6,"Sat" });
            daysOfWeek.Tables["SelectionListTable"].Rows.Add(new object[] { 7,"Sun" });
            daysOfWeek.AcceptChanges();
            return daysOfWeek;
        }
        public int GetWeekday(string weekdayName) {
            int weekday = 0;
            switch(weekdayName.ToLower()) {
                case "mon": weekday = 1; break;
                case "tue": weekday = 2; break;
                case "wed": weekday = 3; break;
                case "thu": weekday = 4; break;
                case "fri": weekday = 5; break;
                case "sat": weekday = 6; break;
                case "sun": weekday = 7; break;
            }
            return weekday;
        }

        public DataSet GetTemplates() {
            //Get a collection of ship schedules templates for all terminals
            DataSet templates = new DataSet();
            try {
                DataSet ds = new ShipScheduleTemplatesGateway().GetShipScheduleTemplates();
                if(ds != null && ds.Tables["ShipScheduleTemplateTable"] != null && ds.Tables["ShipScheduleTemplateTable"].Rows.Count > 0) templates.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return templates;
        }
        public string AddTemplate(ShipScheduleTemplate template) {
            //
            string templateID="";
            try {
                //Apply simple business rules
                if (template == null) return "";

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Create the trip
                    templateID = new ShipScheduleTemplatesGateway().CreateShipScheduleTemplate(template);

                    //Stop 1
                    new ShipScheduleTemplatesGateway().CreateShipScheduleTemplateStop(templateID,template.StopNumber,template.AgentTerminalID,
                                                                            template.Tag,template.Notes,template.ScheduledArrivalDateOffset,
                                                                            template.ScheduledArrivalTime,template.ScheduledOFD1Offset,
                                                                            DateTime.Now,template.Stop1User);

                    //Stop 2
                    if (template.S2MainZone.Trim().Length > 0) {
                        new ShipScheduleTemplatesGateway().CreateShipScheduleTemplateStop(templateID,template.S2StopNumber,template.S2AgentTerminalID,
                                                                                template.S2Tag, template.S2Notes,template.S2ScheduledArrivalDateOffset,
                                                                                template.S2ScheduledArrivalTime,template.S2ScheduledOFD1Offset,
                                                                                DateTime.Now,template.Stop2User);
                    }

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return templateID;
        }
        public bool UpdateTemplate(ShipScheduleTemplate template) {
            //
            bool updated=false;
            try {
                //Apply simple business rules
                if (template == null) return false;

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                        //Trip
                        new ShipScheduleTemplatesGateway().UpdateShipScheduleTemplate(template);

                        //Stop 1
                        new ShipScheduleTemplatesGateway().UpdateShipScheduleTemplateStop(template.StopID,template.AgentTerminalID,template.Tag, 
                                                                                template.Notes,template.ScheduledArrivalDateOffset, 
                                                                                template.ScheduledArrivalTime,template.ScheduledOFD1Offset, 
                                                                                DateTime.Now,template.Stop1User,template.Stop1RowVersion);

                        //Stop 2
                        if(template.S2MainZone != null && template.S2MainZone.Trim().Length > 0) {
                            //New or update?
                            if(template.S2StopID.Trim().Length == 0) {
                                new ShipScheduleTemplatesGateway().CreateShipScheduleTemplateStop(template.TemplateID,template.S2StopNumber,template.S2AgentTerminalID,
                                                                                        template.S2Tag,template.S2Notes,template.S2ScheduledArrivalDateOffset, 
                                                                                        template.S2ScheduledArrivalTime,template.S2ScheduledOFD1Offset,
                                                                                        DateTime.Now,template.Stop2User);
                            }
                            else {
                                new ShipScheduleTemplatesGateway().UpdateShipScheduleTemplateStop(template.S2StopID,template.S2AgentTerminalID,template.S2Tag,template.S2Notes,
                                                                                        template.S2ScheduledArrivalDateOffset,template.S2ScheduledArrivalTime,
                                                                                        template.S2ScheduledOFD1Offset,DateTime.Now,template.Stop2User,template.Stop2RowVersion);
                            }
                        }
                        updated = true;

                        //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                        scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return updated;
        }
        public System.IO.Stream GetExportDefinition() {
            //
            System.IO.StreamReader reader = null;
            try {
                reader = new System.IO.StreamReader(System.Web.Hosting.HostingEnvironment.MapPath("~\\App_Data\\ExportDS.xsd"));
            }
            catch(Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message), "Service Error"); }
            finally { if(reader != null) reader.Close(); }
            return reader.BaseStream;
        }
    }
}
