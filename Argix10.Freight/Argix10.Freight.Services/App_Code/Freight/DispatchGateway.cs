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


namespace Argix.Freight {
    //
    public class DispatchGateway {
        //Members
        public const string SQL_CONNID = "Dispatch";

        private const string USP_PICKUPLOG_READ = "uspDispatchPickupLogView2",USP_PICKUPLOGTEMPLATES_READ = "uspDispatchPickupLogTemplatesView",TBL_PICKUPLOG_READ = "PickupLogTable";
        private const string USP_PICKUPLOG_READBYCLIENT = "uspDispatchPickupLogViewByClient";
        private const string USP_PICKUPREQUEST_READ = "uspDispatchPickupLogRead";
        private const string USP_PICKUPREQUEST_INSERT = "uspDispatchPickupLogInsert",USP_PICKUPREQUEST_UPDATE = "uspDispatchPickupLogUpdate",USP_PICKUPREQUEST__UPDATE = "uspDispatchPickupLogRequestUpdate",USP_PICKUPREQUEST_CANCEL = "uspDispatchPickupLogCancel";
        private const string USP_PICKUPREQUEST_INSERT3 = "uspDispatchPickupLogInsert3";

        private const string USP_CLIENTINBOUND_READ = "uspDispatchClientInboundScheduleView2",USP_CLIENTINBOUNDTEMPLATES_READ = "uspDispatchClientInboundScheduleTemplatesView",TBL_CLIENTINBOUND_READ = "ClientInboundScheduleTable";
        private const string USP_CLIENTINBOUND_INSERT = "uspDispatchClientInboundScheduleInsert",USP_CLIENTINBOUND_UPDATE = "uspDispatchClientInboundScheduleUpdate",USP_CLIENTINBOUND_CANCEL = "uspDispatchClientInboundScheduleCancel";
        private const string USP_CLIENTINBOUND_INSERT3 = "uspDispatchClientInboundScheduleInsert3";

        private const string USP_SCHEDULEDINBOUND_READ = "uspDispatchInboundScheduleView2",USP_SCHEDULEDINBOUNDTEMPLATES_READ = "uspDispatchInboundScheduleTemplatesView",TBL_SCHEDULEDINBOUND_READ = "InboundScheduleTable";
        private const string USP_SCHEDULEDINBOUND_INSERT = "uspDispatchInboundScheduleInsert2",USP_SCHEDULEDINBOUND_UPDATE = "uspDispatchInboundScheduleUpdate2",USP_SCHEDULEDINBOUND_CANCEL = "uspDispatchInboundScheduleCancel";
        
        private const string USP_SCHEDULEDOUTBOUND_READ = "uspDispatchOutboundScheduleView2",USP_SCHEDULEDOUTBOUNDTEMPLATES_READ = "uspDispatchOutboundScheduleTemplatesView",TBL_SCHEDULEDOUTBOUND_READ = "OutboundScheduleTable";
        private const string USP_SCHEDULEDOUTBOUND_INSERT = "uspDispatchOutboundScheduleInsert2",USP_SCHEDULEDOUTBOUND_UPDATE = "uspDispatchOutboundScheduleUpdate2",USP_SCHEDULEDOUTBOUND_CANCEL = "uspDispatchOutboundScheduleCancel";
        
        private const string USP_TRAILERLOG_READ = "uspDispatchTrailerLogView2",TBL_TRAILERLOG_READ = "TrailerLogTable";
        private const string USP_TRAILERLOG_INSERT = "uspDispatchTrailerLogInsert",USP_TRAILERLOG_UPDATE = "uspDispatchTrailerLogUpdate",USP_TRAILERLOG_CANCEL = "uspDispatchTrailerLogCancel";
        private const string USP_TRAILERLOG_YARDCHECK = "uspDispatchTrailerLogYardCheck",USP_TRAILERLOG_ARCHIVE = "uspDispatchTrailerLogArchive",USP_TRAILERLOG_SEARCH = "uspDispatchTrailerLogSearch";

        public const string USP_LOADTENDERLOG_VIEW = "uspDispatchLoadTenderLogView2", TBL_LOADTENDERENTRY = "LoadTenderLogTable";
        public const string USP_LOADTENDERENTRY_READ = "uspDispatchLoadTenderEntryRead";
        public const string USP_LOADTENDERENTRY_CREATE = "uspDispatchLoadTenderEntryCreate", USP_LOADTENDERENTRY_UPDATE = "uspDispatchLoadTenderEntryUpdate";
        public const string USP_LOADTENDERENTRY_TENDER = "uspDispatchLoadTenderEntryTender", USP_LOADTENDERENTRY_SCHEDULE = "uspDispatchLoadTenderEntrySchedule", USP_LOADTENDERENTRY_CANCEL = "uspDispatchLoadTenderEntryCancel";
        public const string USP_PCSLOADTENDER_CREATE = "uspDIspatchLoadTenderCreate";
        public const string USP_PCSLOADTENDER_READ = "uspDispatchLoadTenderRead", TBL_LOADTENDER = "LoadTenderTable";

        private const string USP_BBBSCHEDULE_VIEW = "uspDispatchBBBScheduleView2", TBL_BBBSCHEDULE = "BBBScheduleTable";
        private const string USP_BBBSCHEDULE_INSERT = "uspDispatchBBBScheduleInsert", USP_BBBSCHEDULE_UPDATE = "uspDispatchBBBScheduleUpdate", USP_BBBSCHEDULE_CANCEL = "uspDispatchBBBScheduleCancel";

        private const string USP_APPOINTMENTTYPES_READ = "uspDispatchAppointmentTypeGetList",TBL_APPOINTMENTTYPES = "AppointmentTypeTable";
        private const string USP_FREIGHTDESIGNATION_READ = "uspFreightDesignationGetList",TBL_FREIGHTDESIGNATION = "FreightDesignationTable";

        private const string USP_BLOG_READ = "uspDispatchBlogView",USP_BLOG_INSERT = "uspDispatchBlogInsert",TBL_BLOG = "BlogTable";
        
        //Interface
        public DispatchGateway() { }

        public DataSet ReadPickupLog(DateTime start, DateTime end) {
            //Read the pickup log
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_PICKUPLOG_READ,TBL_PICKUPLOG_READ,new object[] { start,end });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet ReadPickupLogByClient(DateTime start,DateTime end,string clientNumber) {
            //Read pickup appointments and requests for the specified client
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_PICKUPLOG_READBYCLIENT,TBL_PICKUPLOG_READ,new object[] { start,end,clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet ReadPickupLogTemplates() {
            //Read the pickup log templates
            DataSet log=null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_PICKUPLOGTEMPLATES_READ,TBL_PICKUPLOG_READ,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet GetPickupRequest(int requestID) {
            //Get an existing request
            DataSet request = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_PICKUPREQUEST_READ,TBL_PICKUPLOG_READ,new object[] { requestID });
                if (ds != null && ds.Tables[TBL_PICKUPLOG_READ] != null) request.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return request;
        }
        public bool InsertPickupRequest(PickupRequest request) {
            //
            bool inserted=false;
            try {
                inserted = new DataService().ExecuteNonQuery(SQL_CONNID, USP_PICKUPREQUEST_INSERT,
                    new object[] { 
                        request.Created, request.CreateUserID, request.ScheduleDate, request.CallerName, request.ClientNumber, request.Client, 
                        request.ShipperNumber, request.Shipper, request.ShipperAddress, request.ShipperPhone, request.WindowOpen, request.WindowClose, 
                        request.TerminalNumber, request.Terminal, request.DriverName, request.OrderType, 
                        request.Amount, request.AmountType, request.FreightType, request.Weight, 
                        request.Comments, request.IsTemplate, request.LastUpdated, request.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return inserted;
        }
        public int InsertPickupRequest3(PickupRequest request) {
            //
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_PICKUPREQUEST_INSERT3,
                    new object[] { 
                        null, request.Created, request.CreateUserID, request.ScheduleDate, request.CallerName, request.ClientNumber, request.Client, 
                        request.ShipperNumber, request.Shipper, request.ShipperAddress, request.ShipperPhone, request.WindowOpen, request.WindowClose, 
                        request.TerminalNumber, request.Terminal, request.DriverName, request.OrderType, 
                        request.Amount, request.AmountType, request.FreightType, request.Weight, 
                        request.Comments, request.IsTemplate, request.LastUpdated, request.UserID
                    });
                id = Convert.ToInt32(o);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public bool UpdatePickupRequest(PickupRequest request) {
            //
            bool updated=false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUPREQUEST_UPDATE,
                    new object[] { 
                        request.RequestID, request.ScheduleDate, request.CallerName, request.ClientNumber, request.Client, 
                        request.ShipperNumber, request.Shipper, request.ShipperAddress, request.ShipperPhone, request.WindowOpen, request.WindowClose, 
                        request.TerminalNumber, request.Terminal, request.DriverName, 
                        (request.ActualPickup!=DateTime.MinValue ? request.ActualPickup : null as object), 
                        request.OrderType, 
                        request.Amount, request.AmountType, request.FreightType, request.Weight, 
                        request.Comments, request.LastUpdated, request.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool UpdatePickupRequest(int requestID,string shipperNumber,string shipper,string shipperAddress,string shipperPhone,int windowOpen,int windowClose,string driverName,DateTime actual,string orderType,DateTime lastUpdated,string userID) {
            //
            bool updated=false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUPREQUEST__UPDATE,
                    new object[] { 
                        requestID, 
                        shipperNumber, shipper, shipperAddress, shipperPhone, windowOpen, windowClose, 
                        driverName, (actual!=DateTime.MinValue ? actual : null as object), orderType, lastUpdated, userID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool UpdatePickupRequest(int requestID,string driverName,DateTime actual,string orderType,DateTime lastUpdated,string userID) {
            //
            bool updated=false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUPREQUEST__UPDATE,
                    new object[] { 
                        requestID, 
                        null, null, null, null, null, null, 
                        driverName, (actual!=DateTime.MinValue ? actual : null as object), orderType, lastUpdated, userID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool UpdatePickupRequest(int requestID,string driverName,DateTime lastUpdated,string userID) {
            //
            bool updated=false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUPREQUEST__UPDATE,
                    new object[] { 
                        requestID, 
                        null, null, null, null, null, null, 
                        driverName, null, null, lastUpdated, userID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelPickupRequest(int requestID,DateTime cancelled,string cancelledUserID) {
            //
            bool deleted=false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PICKUPREQUEST_CANCEL,new object[] { requestID,cancelled,cancelledUserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }

        public DataSet ReadClientInboundSchedule(DateTime start,DateTime end) {
            //Read the Client Inbound Schedule
            DataSet schedule = null;
            try {
                schedule = new DataService().FillDataset(SQL_CONNID,USP_CLIENTINBOUND_READ,TBL_CLIENTINBOUND_READ,new object[] { start,end });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return schedule;
        }
        public DataSet ReadClientInboundScheduleTemplates() {
            //Read client inbound freight templates from the Client Inbound Schedule
            DataSet templates = null;
            try {
                templates = new DataService().FillDataset(SQL_CONNID,USP_CLIENTINBOUNDTEMPLATES_READ,TBL_CLIENTINBOUND_READ,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return templates;
        }
        public bool InsertClientInboundFreight(ClientInboundFreight freight) {
            //Insert a new client inbound freight into the Client Inbound Schedule
            bool inserted = false;
            try {
                inserted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CLIENTINBOUND_INSERT,
                    new object[] { 
                        freight.Created, freight.CreateUserID, freight.ScheduleDate, 
                        freight.VendorName, freight.ConsigneeName, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, 
                        freight.ScheduledArrival, freight.IsLiveUnload, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        (freight.SortDate!=DateTime.MinValue ? freight.SortDate : null as object), 
                        freight.Comments, freight.IsTemplate, 
                        freight.LastUpdated, freight.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return inserted;
        }
        public int InsertClientInboundFreight3(ClientInboundFreight freight) {
            //Insert a new client inbound freight into the Client Inbound Schedule
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_CLIENTINBOUND_INSERT3,
                    new object[] { 
                        null, freight.Created, freight.CreateUserID, freight.ScheduleDate, 
                        freight.VendorName, freight.ConsigneeName, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, 
                        freight.ScheduledArrival, freight.IsLiveUnload, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        (freight.SortDate!=DateTime.MinValue ? freight.SortDate : null as object), 
                        freight.Comments, freight.IsTemplate, 
                        freight.LastUpdated, freight.UserID
                    });
                id = Convert.ToInt32(o);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return id;
        }
        public bool UpdateClientInboundFreight(ClientInboundFreight freight) {
            //Update an existing client inbound freight in the Client Inbound Schedule
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CLIENTINBOUND_UPDATE,
                    new object[] { 
                        freight.ID, freight.ScheduleDate, 
                        freight.VendorName, freight.ConsigneeName, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, 
                        freight.ScheduledArrival, 
                        (freight.ActualArrival!=DateTime.MinValue ? freight.ActualArrival : null as object), 
                        freight.IsLiveUnload, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        (freight.SortDate!=DateTime.MinValue ? freight.SortDate : null as object), 
                        freight.TDSNumber, freight.TDSCreateUserID, freight.Comments, 
                        freight.LastUpdated, freight.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelClientInboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //Cancel an existing client inbound freight in the Client Inbound Schedule
            bool deleted = false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CLIENTINBOUND_CANCEL,new object[] { id,cancelled,cancelledUserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }

        public DataSet ReadInboundSchedule(DateTime start,DateTime end) {
            //
            DataSet schedule = null;
            try {
                schedule = new DataService().FillDataset(SQL_CONNID,USP_SCHEDULEDINBOUND_READ,TBL_SCHEDULEDINBOUND_READ,new object[] { start,end });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return schedule;
        }
        public DataSet ReadInboundScheduleTemplates() {
            //
            DataSet templates = null;
            try {
                templates = new DataService().FillDataset(SQL_CONNID,USP_SCHEDULEDINBOUNDTEMPLATES_READ,TBL_SCHEDULEDINBOUND_READ,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return templates;
        }
        public bool InsertInboundFreight(InboundFreight freight) {
            //
            bool inserted = false;
            try {
                inserted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCHEDULEDINBOUND_INSERT,
                    new object[] { 
                        freight.Created, freight.CreateUserID, freight.ScheduleDate, 
                        freight.Origin, freight.OriginLocation, freight.Destination, freight.DestinationLocation, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, freight.DropEmptyTrailerNumber, 
                        (freight.ScheduledDeparture!=DateTime.MinValue ? freight.ScheduledDeparture : null as object), 
                        (freight.ScheduledArrival!=DateTime.MinValue ? freight.ScheduledArrival : null as object), 
                        freight.Confirmed, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        freight.Comments, freight.IsTemplate, freight.LastUpdated, freight.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return inserted;
        }
        public bool UpdateInboundFreight(InboundFreight freight) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCHEDULEDINBOUND_UPDATE,
                    new object[] { 
                        freight.ID, freight.ScheduleDate, 
                        freight.Origin, freight.OriginLocation, freight.Destination, freight.DestinationLocation, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, freight.DropEmptyTrailerNumber, 
                        (freight.ScheduledDeparture!=DateTime.MinValue ? freight.ScheduledDeparture : null as object), 
                        (freight.ActualDeparture!=DateTime.MinValue ? freight.ActualDeparture : null as object), 
                        (freight.ScheduledArrival!=DateTime.MinValue ? freight.ScheduledArrival : null as object), 
                        (freight.ActualArrival!=DateTime.MinValue ? freight.ActualArrival : null as object),
                        freight.Confirmed, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        freight.Comments, freight.LastUpdated, freight.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelInboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //
            bool deleted = false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCHEDULEDINBOUND_CANCEL,new object[] { id,cancelled,cancelledUserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }

        public DataSet ReadOutboundSchedule(DateTime start,DateTime end) {
            //
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_SCHEDULEDOUTBOUND_READ,TBL_SCHEDULEDOUTBOUND_READ,new object[] { start,end });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet ReadOutboundScheduleTemplates() {
            //
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_SCHEDULEDOUTBOUNDTEMPLATES_READ,TBL_SCHEDULEDOUTBOUND_READ,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public bool InsertOutboundFreight(OutboundFreight freight) {
            //
            bool inserted = false;
            try {
                inserted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCHEDULEDOUTBOUND_INSERT,
                    new object[] { 
                        freight.Created, freight.CreateUserID, freight.ScheduleDate, 
                        freight.Origin, freight.OriginLocation, freight.Destination, freight.DestinationLocation, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, freight.DropEmptyTrailerNumber, 
                        (freight.ScheduledDeparture!=DateTime.MinValue ? freight.ScheduledDeparture : null as object), 
                        (freight.ScheduledArrival!=DateTime.MinValue ? freight.ScheduledArrival : null as object), 
                        freight.Confirmed, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        freight.Comments, freight.IsTemplate, freight.LastUpdated, freight.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return inserted;
        }
        public bool UpdateOutboundFreight(OutboundFreight freight) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCHEDULEDOUTBOUND_UPDATE,
                    new object[] { 
                        freight.ID, freight.ScheduleDate, 
                        freight.Origin, freight.OriginLocation, freight.Destination, freight.DestinationLocation, 
                        freight.CarrierName, freight.DriverName, freight.TrailerNumber, freight.DropEmptyTrailerNumber, 
                        (freight.ScheduledDeparture!=DateTime.MinValue ? freight.ScheduledDeparture : null as object), 
                        (freight.ActualDeparture!=DateTime.MinValue ? freight.ActualDeparture : null as object), 
                        (freight.ScheduledArrival!=DateTime.MinValue ? freight.ScheduledArrival : null as object), 
                        (freight.ActualArrival!=DateTime.MinValue ? freight.ActualArrival : null as object),
                        freight.Confirmed, 
                        freight.Amount, freight.AmountType, freight.FreightType, 
                        freight.Comments, freight.LastUpdated, freight.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelOutboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //
            bool deleted = false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SCHEDULEDOUTBOUND_CANCEL,new object[] { id,cancelled,cancelledUserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }

        public DataSet ReadTrailerLog(DateTime start,DateTime end) {
            //Read the Trailer Log
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_TRAILERLOG_READ,TBL_TRAILERLOG_READ,new object[] { start,end });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet ReadTrailerLogYardCheck() {
            //Read the Trailer Log
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_TRAILERLOG_YARDCHECK,TBL_TRAILERLOG_READ,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet ReadTrailerLogArchive() {
            //Read the Trailer Log
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_TRAILERLOG_ARCHIVE,TBL_TRAILERLOG_READ,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet SearchTrailerLog(string trailerNumber) {
            //Read the Trailer Log
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_TRAILERLOG_SEARCH,TBL_TRAILERLOG_READ,new object[] { trailerNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public bool InsertTrailerEntry(TrailerEntry entry) {
            //Insert a new trailer entry into the Trailer Log
            bool inserted = false;
            try {
                inserted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_TRAILERLOG_INSERT,
                    new object[] { 
                        entry.Created, entry.CreateUserID, entry.ScheduleDate, entry.TrailerNumber, 
                        (entry.InboundDate!=DateTime.MinValue ? entry.InboundDate : null as object), 
                        entry.InboundCarrier, entry.InboundSeal, entry.InboundDriverName, entry.TDSNumber, entry.InitialYardLocation, 
                        (entry.OutboundDate!=DateTime.MinValue ? entry.OutboundDate : null as object), 
                        entry.OutboundCarrier, entry.OutboundSeal, entry.OutboundDriverName, entry.BOLNumber,
                        entry.Comments, entry.IsTemplate, entry.LastUpdated, entry.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return inserted;
        }
        public bool UpdateTrailerEntry(TrailerEntry entry) {
            //Update an existing trailer entry in the Trailer Log
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_TRAILERLOG_UPDATE,
                    new object[] { 
                        entry.ID, entry.ScheduleDate, entry.TrailerNumber, 
                        (entry.InboundDate!=DateTime.MinValue ? entry.InboundDate : null as object), 
                        entry.InboundCarrier, entry.InboundSeal, entry.InboundDriverName, entry.TDSNumber, entry.InitialYardLocation, 
                        (entry.OutboundDate!=DateTime.MinValue ? entry.OutboundDate : null as object), 
                        entry.OutboundCarrier, entry.OutboundSeal, entry.OutboundDriverName, entry.BOLNumber, 
                        entry.Comments, entry.LastUpdated, entry.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelTrailerEntry(int id,DateTime cancelled,string cancelledUserID) {
            //Cancel an existing trailer entry in the Trailer Log
            bool deleted = false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_TRAILERLOG_CANCEL,new object[] { id,cancelled,cancelledUserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }

        public DataSet ViewLoadTenderLog(DateTime start, DateTime end) {
            //View the load tender log
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID, USP_LOADTENDERLOG_VIEW, TBL_LOADTENDERENTRY, new object[] { start, end });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return log;
        }
        public int CreateLoadTenderEntry(LoadTenderEntry entry) {
            //Create a new load tender
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_LOADTENDERENTRY_CREATE,
                    new object[] { 
                    0, entry.Created, entry.CreateUserID, entry.ScheduleDate, entry.ClientNumber, entry.Client, 
                    entry.VendorNumber, entry.VendorName, entry.VendorAddressLine1, entry.VendorAddressLine2, entry.VendorCity, entry.VendorState, entry.VendorZip, entry.VendorZip4, 
		            entry.ContactName, entry.ContactPhone, entry.ContactEmail, entry.WindowOpen, entry.WindowClose, entry.Description, 
                    entry.Amount, entry.AmountType, entry.Weight, entry.IsFullTrailer, entry.Comments, 
                    entry.LastUpdated, entry.UserID
                });
                id = (int)o;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return id;
        }
        public DataSet ReadLoadTenderEntry(int id) {
            //
            DataSet entry = null;
            try {
                entry = new DataService().FillDataset(SQL_CONNID, USP_LOADTENDERENTRY_READ, TBL_LOADTENDERENTRY, new object[] { id });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return entry;
        }
        public bool UpdateLoadTenderEntry(LoadTenderEntry entry) {
            //Update an existing load tender
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERENTRY_UPDATE,
                    new object[] { 
                    entry.ID, entry.ScheduleDate, 
                    entry.VendorNumber, entry.VendorName, entry.VendorAddressLine1, entry.VendorAddressLine2, entry.VendorCity, entry.VendorState, entry.VendorZip, entry.VendorZip4, 
		            entry.ContactName, entry.ContactPhone, entry.ContactEmail, entry.WindowOpen, entry.WindowClose, entry.Description, 
                    entry.Amount, entry.AmountType, entry.Weight, entry.IsFullTrailer, entry.Comments, 
                    entry.LastUpdated, entry.UserID
                });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return updated;
        }
        public bool TenderLoadTenderEntry(int entryID, int loadTenderNumber) {
            //Tender (record attachment id) an existing load tender
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERENTRY_TENDER, new object[] { entryID, loadTenderNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public bool ScheduleLoadTenderEntry(int entryID, int pickupNumber) {
            //Schedule pickup for an existing load tender
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERENTRY_SCHEDULE, new object[] { entryID, pickupNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public bool CancelLoadTenderEntry(int entryID, DateTime cancelled, string cancelledBy) {
            //Cancel an existing load tender
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERENTRY_CANCEL, new object[] { entryID, cancelled, cancelledBy });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public DataSet GetLoadTender(int number) {
            //Get an existing load tender
            DataSet loadTender = null;
            try {
                loadTender = new DataService().FillDataset(SQL_CONNID, USP_PCSLOADTENDER_READ, TBL_LOADTENDER, new object[] { number });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return loadTender;
        }
        public int CreateLoadTender(string name, byte[] bytes) {
            //Create a new load tender
            int id = 0;
            try {
                //Save the attachment
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_PCSLOADTENDER_CREATE, new object[] { 0, name, bytes });
                id = (int)o;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return id;
        }

        public DataSet ViewBBBSchedule(DateTime start, DateTime end) {
            //
            DataSet schedule = null;
            try {
                schedule = new DataService().FillDataset(SQL_CONNID, USP_BBBSCHEDULE_VIEW, TBL_BBBSCHEDULE, new object[] { start, end });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return schedule;
        }
        public int CreateBBBTrip(BBBTrip trip) {
            //
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_BBBSCHEDULE_INSERT,
                    new object[] { 
                        null, trip.Created, trip.CreateUserID, trip.ScheduleDate, 
                        (trip.OriginLocationID > 0 ? trip.OriginLocationID : null as object), trip.Origin, trip.OriginLocation, 
                        (trip.DestinationLocationID > 0 ? trip.DestinationLocationID : null as object), trip.Destination, trip.DestinationLocation, 
                        trip.CarrierName, trip.DriverName, trip.TrailerNumber, trip.DropEmptyTrailerNumber, trip.IsLiveUnload, 
                        (trip.ScheduledDeparture!=DateTime.MinValue ? trip.ScheduledDeparture : null as object), 
                        (trip.ScheduledArrival!=DateTime.MinValue ? trip.ScheduledArrival : null as object), 
                        trip.Confirmed, 
                        trip.Amount, trip.AmountType, trip.FreightType, 
                        trip.Comments, trip.IsTemplate, trip.LastUpdated, trip.UserID
                    });
                id = Convert.ToInt32(o);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return id;
        }
        public bool UpdateBBBTrip(BBBTrip trip) {
            //
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID, USP_BBBSCHEDULE_UPDATE,
                    new object[] { 
                        trip.ID, trip.ScheduleDate, 
                        (trip.OriginLocationID > 0 ? trip.OriginLocationID : null as object), trip.Origin, trip.OriginLocation, 
                        (trip.DestinationLocationID > 0 ? trip.DestinationLocationID : null as object), trip.Destination, trip.DestinationLocation, 
                        trip.CarrierName, trip.DriverName, trip.TrailerNumber, trip.DropEmptyTrailerNumber, trip.IsLiveUnload, 
                        (trip.ScheduledDeparture!=DateTime.MinValue ? trip.ScheduledDeparture : null as object), 
                        (trip.ActualDeparture!=DateTime.MinValue ? trip.ActualDeparture : null as object), 
                        (trip.ScheduledArrival!=DateTime.MinValue ? trip.ScheduledArrival : null as object), 
                        (trip.ActualArrival!=DateTime.MinValue ? trip.ActualArrival : null as object),
                        trip.Confirmed, 
                        trip.Amount, trip.AmountType, trip.FreightType, trip.TDSNumber, 
                        trip.Comments, trip.LastUpdated, trip.UserID
                    });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return updated;
        }
        public bool CancelBBBTrip(int id, DateTime cancelled, string cancelledUserID) {
            //
            bool deleted = false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID, USP_BBBSCHEDULE_CANCEL, new object[] { id, cancelled, cancelledUserID });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return deleted;
        }

        public DataSet GetAppointmentTypes() {
            //
            DataSet types = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_APPOINTMENTTYPES_READ,TBL_APPOINTMENTTYPES,new object[] { });
                if (ds != null && ds.Tables[TBL_APPOINTMENTTYPES].Rows.Count > 0) types.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return types;
        }
        public DataSet GetFreghtDesginationTypes() {
            //
            DataSet types = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_FREIGHTDESIGNATION_READ,TBL_FREIGHTDESIGNATION,new object[] { });
                if (ds != null && ds.Tables[TBL_FREIGHTDESIGNATION].Rows.Count > 0)
                    types.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return types;
        }

        public DataSet ReadBlog() {
            //Read the application blog
            DataSet blog = null;
            try {
                blog = new DataService().FillDataset(SQL_CONNID,USP_BLOG_READ,TBL_BLOG,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return blog;
        }
        public bool InsertBlogEntry(BlogEntry entry) {
            //Insert a new blog entry into the application blog
            bool inserted = false;
            try {
                inserted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_BLOG_INSERT,
                    new object[] { entry.Date, entry.Comment, entry.UserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return inserted;
        }
    }
}
