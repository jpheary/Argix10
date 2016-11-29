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
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.AgentLineHaul {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ShipScheduleTemplatesGateway {
        //Members
        public const string SQL_CONNID = "ShipScheduleTemplates";

        private const string USP_SHIPPERTERMINALS = "dbo.uspShipScdeShipperTerminalGetList", TBL_SHIPPERTERMINALS = "TerminalTable";
        private const string USP_CARRIERS = "dbo.uspShipScdeCarrierGetList", TBL_CARRIERS = "CarrierTable";
        private const string USP_AGENTS = "dbo.uspShipScdeAgentGetList", TBL_AGENTS = "AgentTable";

        private const string USP_TEMPLATES = "dbo.uspShipScdeTemplateGetList", TBL_TEMPLATES = "ShipScheduleTemplateTable";
        private const string USP_TEMPLATES_NEW = "dbo.uspShipScdeTemplateNew";
        private const string USP_TEMPLATES_UPDATE = "dbo.uspShipScdeTemplateUpdate";
        private const string USP_TEMPLATESSTOP_NEW = "dbo.uspShipScdeTemplateStopNew";
        private const string USP_TEMPLATESSTOP_UPDATE = "dbo.uspShipScdeTemplateStopUpdate";

        //Interface
        public ShipScheduleTemplatesGateway() { }

        public DataSet GetShippersAndTerminals() {
            //Get a list of shippers and terminals
            DataSet terminals =null;
            try {
                terminals = new DataService().FillDataset(SQL_CONNID, USP_SHIPPERTERMINALS, TBL_SHIPPERTERMINALS, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return terminals;
        }
        public DataSet GetCarriers() {
            //Get a list of carriers
            DataSet carriers = null;
            try {
                carriers = new DataService().FillDataset(SQL_CONNID, USP_CARRIERS, TBL_CARRIERS, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return carriers;
        }
        public DataSet GetAgents() {
            //Get a list of agents
            DataSet agents = null;
            try {
                agents = new DataService().FillDataset(SQL_CONNID, USP_AGENTS, TBL_AGENTS, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
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
            switch (weekdayName.ToLower()) {
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

        public DataSet GetShipScheduleTemplates() {
            //Get a collection of ship schedules templates for all terminals
            DataSet templates = null;
            try {
                templates = new DataService().FillDataset(SQL_CONNID, USP_TEMPLATES, TBL_TEMPLATES, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return templates;
        }
        public string CreateShipScheduleTemplate(ShipScheduleTemplate template) {
            //
            string templateID = "";
            try {
                //Trip
                templateID = (string)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_TEMPLATES_NEW,
                    new object[] { null, template.SortCenterID, template.DayOfTheWeek, template.CarrierServiceID, template.ScheduledCloseDateOffset, 
                                    template.ScheduledCloseTime, template.ScheduledDepartureDateOffset, template.ScheduledDepartureTime, 
                                    template.IsMandatory, template.IsActive, DateTime.Now, template.TemplateUser, null } );
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return templateID;
        }
        public bool UpdateShipScheduleTemplate(ShipScheduleTemplate template) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_TEMPLATES_UPDATE,
                    new object[] {  template.TemplateID, template.DayOfTheWeek, template.CarrierServiceID, template.ScheduledCloseDateOffset, 
                                    template.ScheduledCloseTime, template.ScheduledDepartureDateOffset, template.ScheduledDepartureTime, 
                                    template.IsMandatory, template.IsActive, DateTime.Now, template.TemplateUser, template.TemplateRowVersion });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CreateShipScheduleTemplateStop(string templateID,string stopNumber,long agentTerminalID,string tag,string notes,byte scheduledArrivalDateOffset,DateTime scheduledArrivalTime,byte scheduledOFD1Offset,DateTime lastUpdated,string userID) {
            //
            bool created=false;
            try {
                created = new DataService().ExecuteNonQuery(SQL_CONNID,USP_TEMPLATESSTOP_NEW,
                    new object[] { null, templateID, stopNumber, agentTerminalID, tag, notes,  scheduledArrivalDateOffset, 
                                    scheduledArrivalTime, scheduledOFD1Offset, lastUpdated, userID, null });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return created;
        }
        public bool UpdateShipScheduleTemplateStop(string stopID,long agentTerminalID,string tag,string notes,byte scheduledArrivalDateOffset,DateTime scheduledArrivalTime,byte scheduledOFD1Offset,DateTime lastUpdated,string userID,string rowVersion) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_TEMPLATESSTOP_UPDATE,
                    new object[] { stopID, agentTerminalID, tag, notes, scheduledArrivalDateOffset, 
                                    scheduledArrivalTime, scheduledOFD1Offset, lastUpdated,  userID, rowVersion });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
    }
}
