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


namespace Argix.AgentLineHaul {
    //
    public class TsortGateway {
        //Members
        public string SQL_CONNID = "0";

        private const string USP_ISDE_PICKUPS = "dbo.uspISDExportPickupGetListForPUDate", TBL_ISDE_PICKUPS = "PickupTable";
        private const string USP_ISDE_SORTEDITEMS = "dbo.uspISDExportSortedItemGetList", TBL_ISDE_SORTEDITEMS = "SortedItemTable";
        private const string USP_ISDE_CONFIG_READ = "dbo.uspISDExportFileConfigurationGet", TBL_ISDE_CONFIG = "ClientTable";
        private const string USP_ISDE_CONFIG_CREATE = "dbo.uspISDExportFileConfigurationCreate", USP_ISDE_CONFIG_UPDATE = "dbo.uspISDExportFileConfigurationUpdate", USP_ISDE_CONFIG_DELETE = "dbo.uspISDExportFileConfigurationDelete";
        private const string USP_ISDE_EXPORTFILENAME_GET = "dbo.uspISDExportFileNameGet", TBL_EXPORTFILENAME = "ClientTable";

        private const string USP_SS_SCHEDULES = "dbo.uspShipScdeScheduleGetListForDate", TBL_SS_SCHEDULES = "ShipScheduleTable";
        private const string USP_SS_SCHEDULE = "dbo.uspShipScdeScheduleGetList2",TBL_SS_SCHEDULE = "ShipScheduleViewTable";
        private const string USP_SS_TEMPLATES = "dbo.uspShipScdeTemplateGetListAvailableForShipSchedule",TBL_SS_TEMPLATES = "TemplateViewTable";
        private const string USP_SS_SCHEDULE_CREATE = "dbo.uspShipScdeScheduleNew";
        private const string USP_SS_TRIP_CREATE = "dbo.uspShipScdeTripNew";
        private const string USP_SS_TRIP_FIND = "dbo.uspShipScdeTripGetForCarrierLoad",TBL_TRIP = "DuplicateLoadNumberTable";
        private const string USP_SS_TRIP_UPDATE = "dbo.uspShipScdeTripUpdate";
        private const string USP_SS_TRIP_DISPATCH = "dbo.uspShipScdeTripDispatch",USP_SS_TRIP_DEPART = "dbo.uspShipScdeTripDepart";
        private const string USP_SS_STOP_UPDATE = "dbo.uspShipScdeTripStopUpdate";
        private const string USP_SS_TERMINALS = "dbo.uspShipScdeTerminalGetList", TBL_TERMINALS = "TerminalTable";
        private const string USP_SS_SHIPPERS = "dbo.uspShipScdeShipperGetList", TBL_SHIPPERS = "TerminalTable";
        private const string USP_SS_CARRIERS = "dbo.uspShipScdeCarrierGetList", TBL_CARRIERS = "CarrierTable";

        private const string USP_ZC_TRIPS = "dbo.uspZoneCloseShipScdeScheduleGet", TBL_ZC_TRIPS = "ShipScheduleViewTable";
        private const string USP_ZC_FREIGHT = "dbo.uspZoneCloseAssignedFreightGetList", TBL_ZC_FREIGHT = "ShipScheduleTLTable";
        private const string USP_ZC_PRIORTRIPS = "dbo.uspZoneCloseTripPriorGetList", TBL_ZC_PRIORTRIPS = "ShipScheduleViewTable";
        private const string USP_ZC_FINDSCHEDULE = "dbo.uspZoneCloseShipScheduleGetForTL", TBL_ZC_FINDSCHEDULE = "TLSearchTable";
        private const string USP_ZC_ASSIGNTL = "dbo.uspZoneCloseFreightAssign";
        private const string USP_ZC_UNASSIGNTL = "dbo.uspZoneCloseFreightUnAssign";
        private const string USP_ZC_MOVETL = "dbo.uspZoneCloseFreightMove";
        private const string USP_ZC_UPDATETRIP = "dbo.uspZoneCloseTripIsAssignedUpdate";


        //Interface
        public TsortGateway(int terminalID) { SQL_CONNID = terminalID.ToString(); }

        public DataSet GetPickups(DateTime pickupDate) {
            //Get a collection of all pickups for the terminal on the local LAN database
            DataSet pickups = null;
            try {
                pickups = new DataService().FillDataset(SQL_CONNID, USP_ISDE_PICKUPS, TBL_ISDE_PICKUPS, new object[] { pickupDate });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return pickups;
        }
        public DataSet GetSortedItems(string pickupID) {
            //Get sorted items for a pickup
            DataSet items = null;
            try {
                items = new DataService().FillDataset(SQL_CONNID, USP_ISDE_SORTEDITEMS, TBL_ISDE_SORTEDITEMS, new object[] { pickupID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return items;
        }
        public string GetExportFilename(string counterKey) {
            string filename = "";
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ISDE_EXPORTFILENAME_GET,TBL_EXPORTFILENAME,new object[] { counterKey });
                filename = ds.Tables[TBL_EXPORTFILENAME].Rows[0]["FILENAME"].ToString();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return filename;
        }
        public DataSet ReadISDClients(string clientNumber) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID, USP_ISDE_CONFIG_READ, TBL_ISDE_CONFIG, new object[] { clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool CreateISDClient(ISDClient client) {
            //
            bool created = false;
            try {
                created = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ISDE_CONFIG_CREATE,
                        new object[] { 
                            client.CLID,client.ExportFormat,client.ExportPath,
                            client.CounterKey,client.Client,client.Scanner,client.UserID 
                        });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return created;
        }
        public bool UpdateISDClient(ISDClient client) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ISDE_CONFIG_UPDATE,
                        new object[] { 
                            client.CLID,client.ExportFormat,client.ExportPath,
                            client.CounterKey,client.Client,client.Scanner,client.UserID 
                        });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool DeleteISDClient(ISDClient client) {
            //
            bool deleted = false;
            try {                
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ISDE_CONFIG_DELETE,
                        new object[] { 
                            client.CLID,client.ExportFormat,client.ExportPath,
                            client.CounterKey,client.Client,client.Scanner,client.UserID 
                        });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }


        public DataSet GetShipSchedulesList(DateTime scheduleDate) {
            //
            DataSet schedules = null;
            try {
                schedules = new DataService().FillDataset(SQL_CONNID, USP_SS_SCHEDULES, TBL_SS_SCHEDULES, new object[] { scheduleDate });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return schedules;
        }
        public DataSet GetShipSchedule(long sortCenterID,DateTime scheduleDate) {
            //
            DataSet trips = null;
            try {
                trips = new DataService().FillDataset(SQL_CONNID, USP_SS_SCHEDULE, TBL_SS_SCHEDULE, new object[] { sortCenterID, scheduleDate });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return trips;
        }
        public DataSet GetShipScheduleTemplates(long sortCenterID,DateTime scheduleDate) {
            //
            DataSet templates = null;
            try {
                templates = new DataService().FillDataset(SQL_CONNID, USP_SS_TEMPLATES, TBL_SS_TEMPLATES, new object[] { sortCenterID, scheduleDate });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return templates;
        }
        public string CreateShipSchedule(long sortCenterID,DateTime scheduleDate,DateTime lastUpdated,string userID) {
            //
            string scheduleID = "";
            try {
                scheduleID = (string)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_SS_SCHEDULE_CREATE,new object[] { "",sortCenterID,scheduleDate,lastUpdated,userID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return scheduleID;
        }
        public string CreateShipScheduleTrip(string scheduleID,string templateID,DateTime lastUpdated,string userID) {
            //Create a new ship schedule trip from a template (templates are the only way)
            string tripID = "";
            try {
                tripID = (string)new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_SS_TRIP_CREATE,new object[] { "",scheduleID,templateID,lastUpdated,userID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tripID;
        }
        public string FindShipScheduleTrip(DateTime scheduleDate,long carrierServiceID,string loadNumber) {
            //Find a ship schedule date for the specified loadNumber
            string tripID = "";
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_SS_TRIP_FIND,TBL_TRIP,new object[] { scheduleDate,System.DBNull.Value,loadNumber });
                if (ds != null && ds.Tables[TBL_TRIP].Rows.Count > 0)
                    tripID = ds.Tables[TBL_TRIP].Rows[0]["ScheduleDate"].ToString();
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tripID;
        }
        public bool UpdateShipScheduleTrip(ShipScheduleTrip trip) {
            //
            bool updated=false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SS_TRIP_UPDATE,
                            new object[] { trip.TripID,trip.CarrierServiceID,trip.LoadNumber,
                                            trip.TrailerNumber,trip.TractorNumber,trip.DriverName,
                                            (trip.ScheduledClose!=DateTime.MinValue ? trip.ScheduledClose : null as object),
                                            (trip.ScheduledDeparture!=DateTime.MinValue ? trip.ScheduledDeparture : null as object),
                                            (trip.FreightAssigned!=DateTime.MinValue ? trip.FreightAssigned : null as object),
                                            (trip.TrailerComplete!=DateTime.MinValue ? trip.TrailerComplete : null as object),
                                            (trip.PaperworkComplete!=DateTime.MinValue ? trip.PaperworkComplete : null as object),
                                            (trip.TrailerDispatched!=DateTime.MinValue ? trip.TrailerDispatched : null as object),
                                            (trip.Canceled!=DateTime.MinValue ? trip.Canceled : null as object),
                                            trip.LastUpdated,trip.UserID,trip.RowVersion});
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool DispatchShipScheduleTrip(ShipScheduleTrip trip) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SS_TRIP_DISPATCH,
                            new object[] { trip.TripID,trip.LoadNumber,trip.TrailerNumber,trip.DriverName,
                                            (trip.ScheduledDeparture!=DateTime.MinValue ? trip.ScheduledDeparture : null as object),
                                            trip.LastUpdated,trip.UserID});
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool DepartShipScheduleTrip(ShipScheduleTrip trip) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SS_TRIP_DEPART,
                            new object[] { trip.TripID,trip.BolNumber,trip.TrailerNumber,
                                            trip.LastUpdated,trip.UserID});
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool UpdateShipScheduleTripStop(ShipScheduleStop stop) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SS_STOP_UPDATE,
                            new object[] { stop.StopID,stop.Notes,
                                            (stop.ScheduledArrival!=DateTime.MinValue ? stop.ScheduledArrival : null as object),
                                            (stop.ScheduledOFD1!=DateTime.MinValue ? stop.ScheduledOFD1 : null as object),
                                            stop.LastUpdated,stop.UserID,stop.RowVersion});
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public DataSet GetCarriers() {
            //
            DataSet carriers = null;
            try {
                carriers = new DataService().FillDataset(SQL_CONNID, USP_SS_CARRIERS, TBL_CARRIERS, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return carriers;
        }
        public DataSet GetShippers() {
            //Get a list of shippers
            DataSet shippers = null;
            try {
                shippers = new DataService().FillDataset(SQL_CONNID, USP_SS_SHIPPERS, TBL_SHIPPERS, new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shippers;
        }
        public DataSet GetTerminals() {
            //Get a list of terminals
            DataSet terminals = null;
            try {
                terminals = new DataService().FillDataset(SQL_CONNID,USP_SS_TERMINALS,TBL_TERMINALS,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return terminals;
        }


        public DataSet GetTrips(DateTime scheduleDate) {
            //
            DataSet trips = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ZC_TRIPS,TBL_ZC_TRIPS,new object[] { scheduleDate });
                if (ds.Tables[TBL_ZC_TRIPS].Rows.Count > 0) trips.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return trips;
        }
        public DataSet GetTLs(string scheduleID) {
            //
            DataSet freight = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ZC_FREIGHT,TBL_ZC_FREIGHT,new object[] { scheduleID });
                if (ds.Tables[TBL_ZC_FREIGHT].Rows.Count > 0) freight.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return freight;
        }
        public DataSet GetPriorTrips(string tripID,string freightID) {
            //Get all earlier trips (open, not cancelled)
            DataSet trips = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ZC_PRIORTRIPS,TBL_ZC_PRIORTRIPS,new object[] { tripID,freightID });
                if (ds.Tables[TBL_ZC_PRIORTRIPS].Rows.Count > 0) trips.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return trips;
        }
        public DateTime FindShipSchedule(string tlNumber) {
            //Determine if the specified TL is on a ship schedule
            //Stored procedure returns:
            //TL doesn't exist: no row 
            //TL exists: one row, ScheduleDate is NULL if TL is unassigned to a trip
            DateTime scheduleDate = DateTime.MinValue;
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ZC_FINDSCHEDULE,TBL_ZC_FINDSCHEDULE,new object[] { tlNumber });
                if (ds.Tables[TBL_ZC_FINDSCHEDULE] != null && ds.Tables[TBL_ZC_FINDSCHEDULE].Rows.Count > 0) {
                    if(ds.Tables[TBL_ZC_FINDSCHEDULE].Rows[0].IsNull("ScheduleDate"))
                        scheduleDate = DateTime.MaxValue;
                    else
                        scheduleDate = DateTime.Parse(ds.Tables[TBL_ZC_FINDSCHEDULE].Rows[0]["ScheduleDate"].ToString());
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return scheduleDate;
        }
        public bool UpdateTripStatus(string tripID,int isAssigned) {
            //Open (0) or close (1) a trip to further TL assignments
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ZC_UPDATETRIP,new object[] { tripID,isAssigned });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }
        public bool AssignTL(string tripID,string tl) {
            //Assign an open TL to this trip
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ZC_ASSIGNTL,new object[] { tl,tripID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }
        public bool UnassignTL(string tl) {
            //Unassign an open TL from this trip
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ZC_UNASSIGNTL,new object[] { tl });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }
        public bool MoveTL(string tripID,string tl) {
            //Move a closed TL to this trip
            bool ret = false;
            try {
                ret = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ZC_MOVETL,new object[] { tl,tripID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ret;
        }
    }
}
