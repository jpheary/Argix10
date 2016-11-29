using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Threading;

namespace Argix.Freight {
	//
	public class FreightGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        private static int _cacheTimeout = 240;
        
		//Interface
        static FreightGateway() { 
            //
            try {
                DispatchServiceClient client = new DispatchServiceClient();
                _state = true;
                _address = client.Endpoint.Address.Uri.AbsoluteUri;
                _cacheTimeout = global::Argix.Properties.Settings.Default.CacheTimeout;
            }
            catch (Exception ex) { App.ReportError(ex,true,Argix.Freight.LogLevel.Error); }
        }
        private FreightGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                client.WriteLogEntry(m);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static DispatchDataset ViewPickupLog() {
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewPickupLog(DateTime.Today,DateTime.Today);
                if (ds != null) {
                    if (Program.TerminalCode.Length > 0)
                        log.Merge(ds.Tables["PickupLogTable"].Select("TerminalNumber='" + Program.TerminalCode + "'"));
                    else
                        log.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewPickupLogYesterday() {
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
                DataSet ds = client.ViewPickupLog(start, DateTime.Today.AddDays(-1));
                if (ds != null) {
                    if (Program.TerminalCode.Length > 0)
                        log.Merge(ds.Tables["PickupLogTable"].Select("TerminalNumber='" + Program.TerminalCode + "'"));
                    else
                        log.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewPickupLogTomorrow() {
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewPickupLog(DateTime.Today.AddDays(1),DateTime.Today.AddDays(1));
                if (ds != null) {
                    if (Program.TerminalCode.Length > 0)
                        log.Merge(ds.Tables["PickupLogTable"].Select("TerminalNumber='" + Program.TerminalCode + "'"));
                    else
                        log.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewPickupLogAdvanced() {
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewPickupLog(DateTime.Today.AddDays(1),DateTime.Today.AddDays(30));
                if (ds != null) {
                    if (Program.TerminalCode.Length > 0)
                        log.Merge(ds.Tables["PickupLogTable"].Select("TerminalNumber='" + Program.TerminalCode + "'"));
                    else
                        log.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewPickupLogArchive() {
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewPickupLog(DateTime.Today.AddDays(-global::Argix.Properties.Settings.Default.ArchiveDaysBack),DateTime.Today.AddDays(-1));
                if(ds != null) {
                    if (Program.TerminalCode.Length > 0)
                        log.Merge(ds.Tables["PickupLogTable"].Select("TerminalNumber='" + Program.TerminalCode + "'"));
                    else
                        log.Merge(ds);
                }
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewPickupLogTemplates() {
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewPickupLogTemplates();
                if (ds != null) {
                    if (Program.TerminalCode.Length > 0)
                        log.Merge(ds.Tables["PickupLogTable"].Select("TerminalNumber='" + Program.TerminalCode + "'"));
                    else
                        log.Merge(ds);
                }
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static bool AddPickupRequest(DispatchDataset.PickupLogTableRow request) {
            //Dispatch Clerk\Client Rep add a new pickup request
            bool added=false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                PickupRequest pr = new PickupRequest();
                pr.Created = request.Created;
                pr.CreateUserID = request.CreateUserID;
                pr.ScheduleDate = request.ScheduleDate;
                pr.CallerName = (!request.IsCallerNameNull() ? request.CallerName : "");
                pr.ClientNumber = (!request.IsClientNumberNull() ? request.ClientNumber : null);
                pr.Client = request.Client;
                pr.ShipperNumber = (!request.IsShipperNumberNull() ? request.ShipperNumber : null);
                pr.Shipper = request.Shipper;
                pr.ShipperAddress = (!request.IsShipperAddressNull() ? request.ShipperAddress : "");
                pr.ShipperPhone = (!request.IsShipperPhoneNull() ? request.ShipperPhone : "");
                if(!request.IsWindowOpenNull()) pr.WindowOpen = request.WindowOpen;
                if(!request.IsWindowCloseNull()) pr.WindowClose = request.WindowClose;
                pr.TerminalNumber = (!request.IsTerminalNumberNull() ? request.TerminalNumber : null);
                pr.Terminal = (!request.IsTerminalNull() ? request.Terminal : "");
                pr.DriverName = (!request.IsDriverNameNull() ? request.DriverName : "");
                if (!request.IsActualPickupNull()) pr.ActualPickup = request.ActualPickup;
                pr.OrderType = (!request.IsOrderTypeNull() ? request.OrderType : "");
                pr.Amount = request.Amount;
                pr.AmountType = request.AmountType;
                pr.FreightType = (!request.IsFreightTypeNull() ? request.FreightType : "");
                pr.Weight = (!request.IsWeightNull() ? request.Weight : 0);
                pr.Comments = (!request.IsCommentsNull() ? request.Comments : "");
                pr.IsTemplate = (!request.IsIsTemplateNull() ? request.IsTemplate : false);
                pr.LastUpdated = request.LastUpdated;
                pr.UserID = request.UserID;
                
                added = client.AddPickupRequest(pr);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public static bool ChangePickupRequest(DispatchDataset.PickupLogTableRow request) {
            //Dispatch Clerk\Client Rep modify an existing pickup request
            bool changed=false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                PickupRequest pr = new PickupRequest();
                pr.RequestID = request.RequestID;
                pr.ScheduleDate = request.ScheduleDate;
                pr.CallerName = (!request.IsCallerNameNull() ? request.CallerName : "");
                pr.ClientNumber = (!request.IsClientNumberNull() ? request.ClientNumber : null);
                pr.Client = request.Client;
                pr.ShipperNumber = (!request.IsShipperNumberNull() ? request.ShipperNumber : null);
                pr.Shipper = request.Shipper;
                pr.ShipperAddress = (!request.IsShipperAddressNull() ? request.ShipperAddress : "");
                pr.ShipperPhone = (!request.IsShipperPhoneNull() ? request.ShipperPhone : "");
                if(!request.IsWindowOpenNull()) pr.WindowOpen = request.WindowOpen;
                if(!request.IsWindowCloseNull()) pr.WindowClose = request.WindowClose;
                pr.TerminalNumber = (!request.IsTerminalNumberNull() ? request.TerminalNumber : null);
                pr.Terminal = (!request.IsTerminalNull() ? request.Terminal : "");
                pr.DriverName = (!request.IsDriverNameNull() ? request.DriverName : "");
                if (!request.IsActualPickupNull()) pr.ActualPickup = request.ActualPickup;
                pr.OrderType = (!request.IsOrderTypeNull() ? request.OrderType : "");
                pr.Amount = request.Amount;
                pr.AmountType = request.AmountType;
                pr.Weight = (!request.IsWeightNull() ? request.Weight : 0);
                pr.FreightType = (!request.IsFreightTypeNull() ? request.FreightType : "");
                pr.Comments = (!request.IsCommentsNull() ? request.Comments : "");
                pr.LastUpdated = request.LastUpdated;
                pr.UserID = request.UserID;
                
                changed = client.ChangePickupRequest(pr);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool VerifyPickupRequest(DispatchDataset.PickupLogTableRow request) {
            //Local Dispatcher verifies/updates the pickup request           
            bool changed=false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                changed = client.VerifyPickupRequest(request.RequestID,request.ShipperNumber,request.Shipper,request.ShipperAddress,request.ShipperPhone,request.WindowOpen,request.WindowClose,request.DriverName,request.ActualPickup,request.OrderType,request.LastUpdated,request.UserID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool AssignDriverToPickupRequest(DispatchDataset.PickupLogTableRow request) {
            //Local Dispatcher assigns a driver to the pickup request           
            bool changed=false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                changed = client.AssignDriverToPickupRequest(request.RequestID,request.DriverName,request.LastUpdated,request.UserID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool ArrivePickupRequest(DispatchDataset.PickupLogTableRow request) {
            //Local Window Clerk arrives the pickup request
            bool changed=false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                changed = client.ArrivePickupRequest(request.RequestID,request.DriverName,request.ActualPickup,request.OrderType,request.LastUpdated,request.UserID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CancelPickupRequest(int requestID,DateTime cancelled,string cancelledUserID) {
            //Cancel an existing order detail item; update the sales order
            bool removed=false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                removed = client.CancelPickupRequest(requestID, cancelled, cancelledUserID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }

        public static DispatchDataset ViewClientInboundSchedule() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewClientInboundSchedule(DateTime.Today,DateTime.Today);
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewClientInboundScheduleYesterday() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
                DataSet ds = client.ViewClientInboundSchedule(start,DateTime.Today.AddDays(-1));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewClientInboundScheduleAdvanced() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewClientInboundSchedule(DateTime.Today.AddDays(1),DateTime.Today.AddDays(30));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewClientInboundScheduleArchive() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewClientInboundSchedule(DateTime.Today.AddDays(-global::Argix.Properties.Settings.Default.ArchiveDaysBack),DateTime.Today.AddDays(-1));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewClientInboundScheduleTemplates() {
            //
            DispatchDataset templates = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewClientInboundScheduleTemplates();
                if (ds != null) templates.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return templates;
        }
        public static bool AddClientInboundFreight(DispatchDataset.ClientInboundScheduleTableRow appt) {
            //
            bool added = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ClientInboundFreight cif = new ClientInboundFreight();
                cif.Created = appt.Created;
                cif.CreateUserID = appt.CreateUserID;
                cif.ScheduleDate = appt.ScheduleDate;
                cif.VendorName = appt.VendorName;
                cif.ConsigneeName = appt.ConsigneeName;
                cif.ScheduledArrival = appt.ScheduledArrival;
                cif.CarrierName = (!appt.IsCarrierNameNull() ? appt.CarrierName : null);
                cif.DriverName = (!appt.IsDriverNameNull() ? appt.DriverName : null);
                cif.TrailerNumber = (!appt.IsTrailerNumberNull() ? appt.TrailerNumber : null);
                cif.Amount = (!appt.IsAmountNull() ? appt.Amount : 0);
                cif.AmountType = (!appt.IsAmountTypeNull() ? appt.AmountType : "");
                cif.FreightType = (!appt.IsFreightTypeNull() ? appt.FreightType : "");
                if (!appt.IsSortDateNull()) cif.SortDate = appt.SortDate;
                cif.Comments = !appt.IsCommentsNull() ? appt.Comments : "";
                cif.IsLiveUnload = (!appt.IsIsLiveUnloadNull() ? appt.IsLiveUnload : false);
                cif.IsTemplate = (!appt.IsIsTemplateNull() ? appt.IsTemplate : false);
                cif.LastUpdated = appt.LastUpdated;
                cif.UserID = appt.UserID;

                added = client.AddClientInboundFreight(cif);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public static bool ChangeClientInboundFreight(DispatchDataset.ClientInboundScheduleTableRow appt) {
            //
            bool changed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ClientInboundFreight cif = new ClientInboundFreight();
                cif.ID = appt.ID;
                cif.ScheduleDate = appt.ScheduleDate;
                cif.VendorName = appt.VendorName;
                cif.ConsigneeName = appt.ConsigneeName;
                cif.ScheduledArrival = appt.ScheduledArrival;
                if (!appt.IsActualArrivalNull()) cif.ActualArrival = appt.ActualArrival; else cif.ActualArrival = DateTime.MinValue;
                cif.CarrierName = (!appt.IsCarrierNameNull() ? appt.CarrierName : null);
                cif.DriverName = (!appt.IsDriverNameNull() ? appt.DriverName : null);
                cif.TrailerNumber = (!appt.IsTrailerNumberNull() ? appt.TrailerNumber : null);
                cif.Amount = (!appt.IsAmountNull() ? appt.Amount : 0);
                cif.AmountType = (!appt.IsAmountTypeNull() ? appt.AmountType : "");
                cif.FreightType = (!appt.IsFreightTypeNull() ? appt.FreightType : "");
                if (!appt.IsSortDateNull()) cif.SortDate = appt.SortDate;
                cif.Comments = !appt.IsCommentsNull() ? appt.Comments : "";
                cif.IsLiveUnload = (!appt.IsIsLiveUnloadNull() ? appt.IsLiveUnload : false);
                cif.TDSNumber = !appt.IsTDSNumberNull() ? appt.TDSNumber : "";
                cif.TDSCreateUserID = !appt.IsTDSCreateUserIDNull() ? appt.TDSCreateUserID : "";
                cif.LastUpdated = appt.LastUpdated;
                cif.UserID = appt.UserID;

                changed = client.ChangeClientInboundFreight(cif);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CancelClientInboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //
            bool removed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                removed = client.CancelClientInboundFreight(id,cancelled,cancelledUserID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }

        public static DispatchDataset ViewInboundSchedule() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewInboundSchedule(DateTime.Today,DateTime.Today);
                if(ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewInboundScheduleYesterday() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
                DataSet ds = client.ViewInboundSchedule(start,DateTime.Today.AddDays(-1));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewInboundScheduleAdvanced() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewInboundSchedule(DateTime.Today.AddDays(1),DateTime.Today.AddDays(30));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewInboundScheduleArchive() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewInboundSchedule(DateTime.Today.AddDays(-global::Argix.Properties.Settings.Default.ArchiveDaysBack),DateTime.Today.AddDays(-1));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewInboundScheduleTemplates() {
            //
            DispatchDataset templates = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewInboundScheduleTemplates();
                if (ds != null) templates.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return templates;
        }
        public static bool AddScheduledInboundFreight(DispatchDataset.InboundScheduleTableRow trip) {
            //
            bool added = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                InboundFreight ibf = new InboundFreight();
                ibf.Created = trip.Created;
                ibf.CreateUserID = trip.CreateUserID;
                ibf.ScheduleDate = trip.ScheduleDate;
                ibf.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : "");
                ibf.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : "");
                ibf.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : "");
                ibf.DropEmptyTrailerNumber = (!trip.IsDropEmptyTrailerNumberNull() ? trip.DropEmptyTrailerNumber : "");
                ibf.Origin = trip.Origin;
                ibf.OriginLocation = (!trip.IsOriginLocationNull() ? trip.OriginLocation : "");
                ibf.Destination = trip.Destination;
                ibf.DestinationLocation = (!trip.IsDestinationLocationNull() ? trip.DestinationLocation : "");
                ibf.ScheduledDeparture = trip.ScheduledDeparture;
                ibf.ScheduledArrival = trip.ScheduledArrival;
                ibf.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                ibf.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                ibf.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");
                ibf.Comments = (!trip.IsCommentsNull() ? trip.Comments : "");
                ibf.Confirmed = (!trip.IsConfirmedNull() ? trip.Confirmed : false);
                ibf.IsTemplate = (!trip.IsIsTemplateNull() ? trip.IsTemplate : false);
                ibf.LastUpdated = trip.LastUpdated;
                ibf.UserID = trip.UserID;

                added = client.AddScheduledInboundFreight(ibf);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public static bool ChangeScheduledInboundFreight(DispatchDataset.InboundScheduleTableRow trip) {
            //Dispatch Clerk\Client Rep modify an existing pickup trip
            bool changed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                InboundFreight ibf = new InboundFreight();
                ibf.ID = trip.ID;
                ibf.ScheduleDate = trip.ScheduleDate;
                ibf.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : "");
                ibf.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : "");
                ibf.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : "");
                ibf.DropEmptyTrailerNumber = (!trip.IsDropEmptyTrailerNumberNull() ? trip.DropEmptyTrailerNumber : "");
                ibf.Origin = trip.Origin;
                ibf.OriginLocation = (!trip.IsOriginLocationNull() ? trip.OriginLocation : "");
                ibf.Destination = trip.Destination;
                ibf.DestinationLocation = (!trip.IsDestinationLocationNull() ? trip.DestinationLocation : "");
                ibf.ScheduledDeparture = trip.ScheduledDeparture;
                if (!trip.IsActualDepartureNull()) ibf.ActualDeparture = trip.ActualDeparture;
                ibf.ScheduledArrival = trip.ScheduledArrival;
                if (!trip.IsActualArrivalNull()) ibf.ActualArrival = trip.ActualArrival;
                ibf.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                ibf.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                ibf.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");
                ibf.Comments = (!trip.IsCommentsNull() ? trip.Comments : "");
                ibf.Confirmed = (!trip.IsConfirmedNull() ? trip.Confirmed : false);
                ibf.LastUpdated = trip.LastUpdated;
                ibf.UserID = trip.UserID;

                changed = client.ChangeScheduledInboundFreight(ibf);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CancelScheduledInboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //
            bool removed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                removed = client.CancelScheduledInboundFreight(id,cancelled,cancelledUserID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }

        public static DispatchDataset ViewOutboundSchedule() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewOutboundSchedule(DateTime.Today, DateTime.Today);
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewOutboundScheduleYesterday() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
                DataSet ds = client.ViewOutboundSchedule(start,DateTime.Today.AddDays(-1));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewOutboundScheduleAdvanced() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewOutboundSchedule(DateTime.Today.AddDays(1),DateTime.Today.AddDays(30));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewOutboundScheduleArchive() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewOutboundSchedule(DateTime.Today.AddDays(-global::Argix.Properties.Settings.Default.ArchiveDaysBack),DateTime.Today.AddDays(-1));
                if (ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewOutboundScheduleTemplates() {
            //
            DispatchDataset templates = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewOutboundScheduleTemplates();
                if (ds != null) templates.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return templates;
        }
        public static bool AddScheduledOutboundFreight(DispatchDataset.OutboundScheduleTableRow trip) {
            //
            bool added = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                OutboundFreight obf = new OutboundFreight();
                obf.Created = trip.Created;
                obf.CreateUserID = trip.CreateUserID;
                obf.ScheduleDate = trip.ScheduleDate;
                obf.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : "");
                obf.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : "");
                obf.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : "");
                obf.DropEmptyTrailerNumber = (!trip.IsDropEmptyTrailerNumberNull() ? trip.DropEmptyTrailerNumber : "");
                obf.Origin = trip.Origin;
                obf.OriginLocation = (!trip.IsOriginLocationNull() ? trip.OriginLocation : "");
                obf.Destination = trip.Destination;
                obf.DestinationLocation = (!trip.IsDestinationLocationNull() ? trip.DestinationLocation : "");
                obf.ScheduledDeparture = trip.ScheduledDeparture;
                obf.ScheduledArrival = trip.ScheduledArrival;
                obf.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                obf.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                obf.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");
                obf.Comments = (!trip.IsCommentsNull() ? trip.Comments : "");
                obf.Confirmed = (!trip.IsConfirmedNull() ? trip.Confirmed : false);
                obf.IsTemplate = (!trip.IsIsTemplateNull() ? trip.IsTemplate : false);
                obf.LastUpdated = trip.LastUpdated;
                obf.UserID = trip.UserID;

                added = client.AddScheduledOutboundFreight(obf);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public static bool ChangeScheduledOutboundFreight(DispatchDataset.OutboundScheduleTableRow trip) {
            //Dispatch Clerk\Client Rep modify an existing pickup trip
            bool changed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                OutboundFreight obf = new OutboundFreight();
                obf.ID = trip.ID;
                obf.ScheduleDate = trip.ScheduleDate;
                obf.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : "");
                obf.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : "");
                obf.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : "");
                obf.DropEmptyTrailerNumber = (!trip.IsDropEmptyTrailerNumberNull() ? trip.DropEmptyTrailerNumber : "");
                obf.Origin = trip.Origin;
                obf.OriginLocation = (!trip.IsOriginLocationNull() ? trip.OriginLocation : "");
                obf.Destination = trip.Destination;
                obf.DestinationLocation = (!trip.IsDestinationLocationNull() ? trip.DestinationLocation : "");
                obf.ScheduledDeparture = trip.ScheduledDeparture;
                if (!trip.IsActualDepartureNull()) obf.ActualDeparture = trip.ActualDeparture;
                obf.ScheduledArrival = trip.ScheduledArrival;
                if (!trip.IsActualArrivalNull()) obf.ActualArrival = trip.ActualArrival;
                obf.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                obf.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                obf.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");
                obf.Comments = (!trip.IsCommentsNull() ? trip.Comments : "");
                obf.Confirmed = (!trip.IsConfirmedNull() ? trip.Confirmed : false);
                obf.LastUpdated = trip.LastUpdated;
                obf.UserID = trip.UserID;

                changed = client.ChangeScheduledOutboundFreight(obf);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CancelScheduledOutboundFreight(int id,DateTime cancelled,string cancelledUserID) {
            //
            bool removed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                removed = client.CancelScheduledOutboundFreight(id,cancelled,cancelledUserID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }

        public static DispatchDataset ViewTrailerLog() {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewTrailerLogYardCheck();
                if (ds != null) log.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewTrailerLogArchive() {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewTrailerLogArchive();
                if (ds != null) log.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset SearchTrailerLog(string trailerNumber) {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.SearchTrailerLog(trailerNumber);
                if (ds != null) log.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static bool AddTrailerEntry(DispatchDataset.TrailerLogTableRow entry) {
            //
            bool added = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                TrailerEntry te = new TrailerEntry();
                te.Created = entry.Created;
                te.CreateUserID = entry.CreateUserID;
                te.ScheduleDate = entry.ScheduleDate;
                te.TrailerNumber = (!entry.IsTrailerNumberNull() ? entry.TrailerNumber : null);
                te.InboundDate = (!entry.IsInboundDateNull() ? entry.InboundDate : DateTime.MinValue);
                te.InboundCarrier = (!entry.IsInboundCarrierNull() ? entry.InboundCarrier : null);
                te.InboundSeal = (!entry.IsInboundSealNull() ? entry.InboundSeal : null);
                te.InboundDriverName = (!entry.IsInboundDriverNameNull() ? entry.InboundDriverName : null);
                te.TDSNumber = (!entry.IsTDSNumberNull() ? entry.TDSNumber : null);
                te.InitialYardLocation = (!entry.IsInitialYardLocationNull() ? entry.InitialYardLocation : null);
                te.OutboundDate = (!entry.IsOutboundDateNull() ? entry.OutboundDate : DateTime.MinValue);
                te.OutboundCarrier = (!entry.IsOutboundCarrierNull() ? entry.OutboundCarrier : null);
                te.OutboundSeal = (!entry.IsOutboundSealNull() ? entry.OutboundSeal : null);
                te.OutboundDriverName = (!entry.IsOutboundDriverNameNull() ? entry.OutboundDriverName : null);
                te.BOLNumber = (!entry.IsBOLNumberNull() ? entry.BOLNumber : null);
                te.Comments = (!entry.IsCommentsNull() ? entry.Comments : "");
                te.LastUpdated = entry.LastUpdated;
                te.UserID = entry.UserID;

                added = client.AddTrailerEntry(te);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public static bool ChangeTrailerEntry(DispatchDataset.TrailerLogTableRow entry) {
            //
            bool changed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                TrailerEntry te = new TrailerEntry();
                te.ID = entry.ID;
                te.ScheduleDate = entry.ScheduleDate;
                te.TrailerNumber = (!entry.IsTrailerNumberNull() ? entry.TrailerNumber : null);
                te.InboundDate = (!entry.IsInboundDateNull() ? entry.InboundDate : DateTime.MinValue);
                te.InboundCarrier = (!entry.IsInboundCarrierNull() ? entry.InboundCarrier : null);
                te.InboundSeal = (!entry.IsInboundSealNull() ? entry.InboundSeal : null);
                te.InboundDriverName = (!entry.IsInboundDriverNameNull() ? entry.InboundDriverName : null);
                te.TDSNumber = (!entry.IsTDSNumberNull() ? entry.TDSNumber : null);
                te.InitialYardLocation = (!entry.IsInitialYardLocationNull() ? entry.InitialYardLocation : null);
                te.OutboundDate = (!entry.IsOutboundDateNull() ? entry.OutboundDate : DateTime.MinValue);
                te.OutboundCarrier = (!entry.IsOutboundCarrierNull() ? entry.OutboundCarrier : null);
                te.OutboundSeal = (!entry.IsOutboundSealNull() ? entry.OutboundSeal : null);
                te.OutboundDriverName = (!entry.IsOutboundDriverNameNull() ? entry.OutboundDriverName : null);
                te.BOLNumber = (!entry.IsBOLNumberNull() ? entry.BOLNumber : null);
                te.Comments = (!entry.IsCommentsNull() ? entry.Comments : "");
                te.LastUpdated = entry.LastUpdated;
                te.UserID = entry.UserID;

                changed = client.ChangeTrailerEntry(te);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CancelTrailerEntry(int id,DateTime cancelled,string cancelledUserID) {
            //
            bool removed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                client = new DispatchServiceClient();
                removed = client.CancelTrailerEntry(id,cancelled,cancelledUserID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }

        public static DispatchDataset ViewLoadTenderLog() {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewLoadTenderLog(DateTime.Today, DateTime.Today);
                if(ds != null && ds.Tables["LoadTenderLogTable"] != null && ds.Tables["LoadTenderLogTable"].Rows.Count > 0) log.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewLoadTenderLogYesterday() {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
                DataSet ds = client.ViewLoadTenderLog(start, DateTime.Today.AddDays(-1));
                if(ds != null && ds.Tables["LoadTenderLogTable"] != null && ds.Tables["LoadTenderLogTable"].Rows.Count > 0) log.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewLoadTenderLogAdvanced() {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewLoadTenderLog(DateTime.Today.AddDays(1), DateTime.Today.AddDays(30));
                if(ds != null && ds.Tables["LoadTenderLogTable"] != null && ds.Tables["LoadTenderLogTable"].Rows.Count > 0) log.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static DispatchDataset ViewLoadTenderLogArchive() {
            //
            DispatchDataset log = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewLoadTenderLog(DateTime.Today.AddDays(-global::Argix.Properties.Settings.Default.ArchiveDaysBack), DateTime.Today.AddDays(-1));
                if(ds != null && ds.Tables["LoadTenderLogTable"] != null && ds.Tables["LoadTenderLogTable"].Rows.Count > 0) log.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return log;
        }
        public static int CreateLoadTenderEntry(DispatchDataset.LoadTenderLogTableRow entry) {
            //
            int entryID = 0;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                LoadTenderEntry lte = new LoadTenderEntry();
                lte.Created = entry.Created;
                lte.CreateUserID = entry.CreateUserID;
                lte.ScheduleDate = entry.ScheduleDate;
                lte.ClientNumber = entry.ClientNumber;
                lte.Client = entry.Client;
                lte.VendorNumber = entry.VendorNumber;
                lte.VendorName = entry.VendorName;
                lte.VendorAddressLine1 = entry.VendorAddressLine1;
                lte.VendorAddressLine2 = !entry.IsVendorAddressLine2Null() ? entry.VendorAddressLine2 : "";
                lte.VendorCity = entry.VendorCity;
                lte.VendorState = entry.VendorState;
                lte.VendorZip = entry.VendorZip;
                lte.VendorZip4 = !entry.IsVendorZip4Null() ? entry.VendorZip4 : "";
                lte.ContactName = !entry.IsContactNameNull() ? entry.ContactName : "";
                lte.ContactPhone = !entry.IsContactPhoneNull() ? entry.ContactPhone : "";
                lte.ContactEmail = !entry.IsContactEmailNull() ? entry.ContactEmail : "";
                lte.WindowOpen = !entry.IsWindowOpenNull() ? entry.WindowOpen : 0;
                lte.WindowClose = !entry.IsWindowCloseNull() ? entry.WindowClose : 0;
                lte.Description = !entry.IsDescriptionNull() ? entry.Description : "";
                lte.Amount = !entry.IsAmountNull() ? entry.Amount : 0;
                lte.AmountType = !entry.IsAmountTypeNull() ? entry.AmountType : "";
                lte.Weight = !entry.IsWeightNull() ? entry.Weight : 0;
                lte.IsFullTrailer = !entry.IsIsFullTrailerNull() ? entry.IsFullTrailer : false;
                lte.Comments = !entry.IsCommentsNull() ? entry.Comments : "";
                lte.LastUpdated = entry.LastUpdated;
                lte.UserID = entry.UserID;

                entryID = client.CreateLoadTenderEntry(lte);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return entryID;
        }
        public static LoadTenderEntry ReadLoadTenderEntry(int id) {
            //
            LoadTenderEntry entry = null;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                entry = client.ReadLoadTenderEntry(id);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return entry;
        }
        public static bool UpdateLoadTenderEntry(DispatchDataset.LoadTenderLogTableRow entry) {
            //
            bool updated = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                LoadTenderEntry lte = new LoadTenderEntry();
                lte.Created = entry.Created;
                lte.CreateUserID = entry.CreateUserID;
                lte.ScheduleDate = entry.ScheduleDate;
                lte.ClientNumber = entry.ClientNumber;
                lte.Client = entry.Client;
                lte.VendorNumber = entry.VendorNumber;
                lte.VendorName = entry.VendorName;
                lte.VendorAddressLine1 = entry.VendorAddressLine1;
                lte.VendorAddressLine2 = !entry.IsVendorAddressLine2Null() ? entry.VendorAddressLine2 : "";
                lte.VendorCity = entry.VendorCity;
                lte.VendorState = entry.VendorState;
                lte.VendorZip = entry.VendorZip;
                lte.VendorZip4 = !entry.IsVendorZip4Null() ? entry.VendorZip4 : "";
                lte.ContactName = !entry.IsContactNameNull() ? entry.ContactName : "";
                lte.ContactPhone = !entry.IsContactPhoneNull() ? entry.ContactPhone : "";
                lte.ContactEmail = !entry.IsContactEmailNull() ? entry.ContactEmail : "";
                lte.WindowOpen = !entry.IsWindowOpenNull() ? entry.WindowOpen : 0;
                lte.WindowClose = !entry.IsWindowCloseNull() ? entry.WindowClose : 0;
                lte.Description = !entry.IsDescriptionNull() ? entry.Description : "";
                lte.Amount = !entry.IsAmountNull() ? entry.Amount : 0;
                lte.AmountType = !entry.IsAmountTypeNull() ? entry.AmountType : "";
                lte.Weight = !entry.IsWeightNull() ? entry.Weight : 0;
                lte.IsFullTrailer = !entry.IsIsFullTrailerNull() ? entry.IsFullTrailer : false;
                lte.Comments = !entry.IsCommentsNull() ? entry.Comments : "";
                lte.LastUpdated = entry.LastUpdated;
                lte.UserID = entry.UserID;

                updated = client.UpdateLoadTenderEntry(lte);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool TenderLoadTenderEntry(int entryID, LoadTender loadTender) {
            //
            bool tendered = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                client = new DispatchServiceClient();
                tendered = client.TenderLoadTenderEntry(entryID, loadTender);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tendered;
        }
        public static bool ScheduleLoadTenderEntry(int entryID, DispatchDataset.BBBScheduleTableRow trip) {
            //
            bool scheduled = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                BBBTrip bbbt = new BBBTrip();
                bbbt.Created = trip.Created;
                bbbt.CreateUserID = trip.CreateUserID;
                bbbt.ScheduleDate = trip.ScheduleDate;
                bbbt.Origin = trip.Origin;
                bbbt.Destination = trip.Destination;
                bbbt.ScheduledArrival = trip.ScheduledArrival;
                bbbt.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : null);
                bbbt.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : null);
                bbbt.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : null);
                bbbt.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                bbbt.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                bbbt.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");

                bbbt.Comments = !trip.IsCommentsNull() ? trip.Comments : "";
                bbbt.IsLiveUnload = (!trip.IsIsLiveUnloadNull() ? trip.IsLiveUnload : false);
                bbbt.IsTemplate = (!trip.IsIsTemplateNull() ? trip.IsTemplate : false);
                bbbt.LastUpdated = trip.LastUpdated;
                bbbt.UserID = trip.UserID;

                scheduled = client.ScheduleBBBTripLoadTenderEntry(entryID, bbbt);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return scheduled;
        }
        public static bool ScheduleLoadTenderEntry(int entryID, DispatchDataset.PickupLogTableRow request) {
            //
            bool scheduled = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                PickupRequest pr = new PickupRequest();
                pr.Created = request.Created;
                pr.CreateUserID = request.CreateUserID;
                pr.ScheduleDate = request.ScheduleDate;
                pr.CallerName = (!request.IsCallerNameNull() ? request.CallerName : "");
                pr.ClientNumber = (!request.IsClientNumberNull() ? request.ClientNumber : null);
                pr.Client = request.Client;
                pr.ShipperNumber = (!request.IsShipperNumberNull() ? request.ShipperNumber : null);
                pr.Shipper = request.Shipper;
                pr.ShipperAddress = (!request.IsShipperAddressNull() ? request.ShipperAddress : "");
                pr.ShipperPhone = (!request.IsShipperPhoneNull() ? request.ShipperPhone : "");
                if(!request.IsWindowOpenNull()) pr.WindowOpen = request.WindowOpen;
                if(!request.IsWindowCloseNull()) pr.WindowClose = request.WindowClose;
                pr.TerminalNumber = (!request.IsTerminalNumberNull() ? request.TerminalNumber : null);
                pr.Terminal = (!request.IsTerminalNull() ? request.Terminal : "");
                pr.DriverName = (!request.IsDriverNameNull() ? request.DriverName : "");
                if(!request.IsActualPickupNull()) pr.ActualPickup = request.ActualPickup;
                pr.OrderType = (!request.IsOrderTypeNull() ? request.OrderType : "");
                pr.Amount = request.Amount;
                pr.AmountType = request.AmountType;
                pr.FreightType = (!request.IsFreightTypeNull() ? request.FreightType : "");
                pr.Weight = (!request.IsWeightNull() ? request.Weight : 0);
                pr.Comments = (!request.IsCommentsNull() ? request.Comments : "");
                pr.IsTemplate = (!request.IsIsTemplateNull() ? request.IsTemplate : false);
                pr.LastUpdated = request.LastUpdated;
                pr.UserID = request.UserID;

                scheduled = client.SchedulePickupLoadTenderEntry(entryID, pr);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return scheduled;
        }
        public static bool CancelLoadTenderEntry(int entryID, DateTime cancelled, string cancelledBy) {
            //
            bool removed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                client = new DispatchServiceClient();
                removed = client.CancelLoadTenderEntry(entryID, cancelled, cancelledBy);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }
        public static LoadTender GetLoadTender(int number) {
            //
            LoadTender tender = null;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                client = new DispatchServiceClient();
                tender = client.GetLoadTender(number);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tender;
        }

        public static DispatchDataset ViewBBBSchedule() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewBBBSchedule(DateTime.Today, DateTime.Today);
                if(ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewBBBScheduleYesterday() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today.DayOfWeek == DayOfWeek.Monday ? DateTime.Today.AddDays(-3) : DateTime.Today.AddDays(-1);
                DataSet ds = client.ViewBBBSchedule(start, DateTime.Today.AddDays(-1));
                if(ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewBBBScheduleAdvanced() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewBBBSchedule(DateTime.Today.AddDays(1), DateTime.Today.AddDays(30));
                if(ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static DispatchDataset ViewBBBScheduleArchive() {
            //
            DispatchDataset schedule = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewBBBSchedule(DateTime.Today.AddDays(-global::Argix.Properties.Settings.Default.ArchiveDaysBack), DateTime.Today.AddDays(-1));
                if(ds != null) schedule.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static int AddBBBScheduleTrip(DispatchDataset.BBBScheduleTableRow trip) {
            //
            int id = 0;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                BBBTrip ibf = new BBBTrip();
                ibf.Created = trip.Created;
                ibf.CreateUserID = trip.CreateUserID;
                ibf.ScheduleDate = trip.ScheduleDate;
                ibf.OriginLocationID = (!trip.IsOriginLocationIDNull() ? trip.OriginLocationID : 0);
                ibf.Origin = trip.Origin;
                ibf.OriginLocation = (!trip.IsOriginLocationNull() ? trip.OriginLocation : "");
                ibf.DestinationLocationID = (!trip.IsDestinationLocationIDNull() ? trip.DestinationLocationID : 0);
                ibf.Destination = trip.Destination;
                ibf.DestinationLocation = (!trip.IsDestinationLocationNull() ? trip.DestinationLocation : "");
                ibf.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : "");
                ibf.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : "");
                ibf.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : "");
                ibf.DropEmptyTrailerNumber = (!trip.IsDropEmptyTrailerNumberNull() ? trip.DropEmptyTrailerNumber : "");
                ibf.IsLiveUnload = trip.IsLiveUnload;
                ibf.ScheduledDeparture = trip.ScheduledDeparture;
                ibf.ScheduledArrival = trip.ScheduledArrival;
                ibf.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                ibf.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                ibf.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");
                ibf.Comments = (!trip.IsCommentsNull() ? trip.Comments : "");
                ibf.Confirmed = (!trip.IsConfirmedNull() ? trip.Confirmed : false);
                ibf.IsTemplate = (!trip.IsIsTemplateNull() ? trip.IsTemplate : false);
                ibf.LastUpdated = trip.LastUpdated;
                ibf.UserID = trip.UserID;

                id = client.AddBBBScheduleTrip(ibf);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return id;
        }
        public static bool ChangeBBBScheduleTrip(DispatchDataset.BBBScheduleTableRow trip) {
            //Dispatch Clerk\Client Rep modify an existing pickup trip
            bool changed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                BBBTrip ibf = new BBBTrip();
                ibf.ID = trip.ID;
                ibf.ScheduleDate = trip.ScheduleDate;
                ibf.OriginLocationID = (!trip.IsOriginLocationIDNull() ? trip.OriginLocationID : 0);
                ibf.Origin = trip.Origin;
                ibf.OriginLocation = (!trip.IsOriginLocationNull() ? trip.OriginLocation : "");
                ibf.DestinationLocationID = (!trip.IsDestinationLocationIDNull() ? trip.DestinationLocationID : 0);
                ibf.Destination = trip.Destination;
                ibf.DestinationLocation = (!trip.IsDestinationLocationNull() ? trip.DestinationLocation : "");
                ibf.CarrierName = (!trip.IsCarrierNameNull() ? trip.CarrierName : "");
                ibf.DriverName = (!trip.IsDriverNameNull() ? trip.DriverName : "");
                ibf.TrailerNumber = (!trip.IsTrailerNumberNull() ? trip.TrailerNumber : "");
                ibf.DropEmptyTrailerNumber = (!trip.IsDropEmptyTrailerNumberNull() ? trip.DropEmptyTrailerNumber : "");
                ibf.IsLiveUnload = trip.IsLiveUnload;
                ibf.ScheduledDeparture = trip.ScheduledDeparture;
                if(!trip.IsActualDepartureNull()) ibf.ActualDeparture = trip.ActualDeparture;
                ibf.ScheduledArrival = trip.ScheduledArrival;
                if(!trip.IsActualArrivalNull()) ibf.ActualArrival = trip.ActualArrival;
                ibf.Amount = (!trip.IsAmountNull() ? trip.Amount : 0);
                ibf.AmountType = (!trip.IsAmountTypeNull() ? trip.AmountType : "");
                ibf.FreightType = (!trip.IsFreightTypeNull() ? trip.FreightType : "");
                ibf.TDSNumber = (!trip.IsTDSNumberNull() ? trip.TDSNumber : "");
                ibf.Comments = (!trip.IsCommentsNull() ? trip.Comments : "");
                ibf.Confirmed = (!trip.IsConfirmedNull() ? trip.Confirmed : false);
                ibf.LastUpdated = trip.LastUpdated;
                ibf.UserID = trip.UserID;

                changed = client.ChangeBBBScheduleTrip(ibf);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public static bool CancelBBBScheduleTrip(int id, DateTime cancelled, string cancelledUserID) {
            //
            bool removed = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                removed = client.CancelBBBScheduleTrip(id, cancelled, cancelledUserID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return removed;
        }

        public static DispatchDataset ViewBlog() {
            //
            DispatchDataset blog = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DataSet ds = client.ViewBlog();
                if (ds != null) blog.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return blog;
        }
        public static bool AddBlogEntry(BlogEntry entry) {
            //
            bool added = false;
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                added = client.AddBlogEntry(entry);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        
        public static DispatchDataset GetAgents() {
            //Get agents list
            DispatchDataset agents = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                agents = cache["agents"] as DispatchDataset;
                if (agents == null) {
                    agents = new DispatchDataset();
                    DataSet ds = client.GetAgents();
                    if (ds != null) agents.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("agents",agents,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public static DispatchDataset GetClients() {
            //Get clients list
            DispatchDataset clients = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                clients = cache["clients"] as DispatchDataset;
                if (clients == null) {
                    clients = new DispatchDataset();
                    DataSet ds = client.GetClients();
                    if (ds != null) clients.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("clients",clients,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return clients;
        }
        public static DispatchDataset GetCarriers() {
            //Get carrier list
            DispatchDataset carriers = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                carriers = cache["carriers"] as DispatchDataset;
                if (carriers == null) {
                    carriers = new DispatchDataset();
                    DataSet ds = client.GetCarriers();
                    if (ds != null) carriers.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("carriers",carriers,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return carriers;
        }
        public static DispatchDataset GetDrivers() {
            //Get driver list
            DispatchDataset drivers = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                drivers = cache["drivers"] as DispatchDataset;
                if (drivers == null) {
                    drivers = new DispatchDataset();
                    DataSet ds = client.GetDrivers();
                    if (ds != null) drivers.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("drivers",drivers,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return drivers;
        }
        public static DispatchDataset GetLocations() {
            //Get location list
            DispatchDataset locations = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                locations = cache["locations"] as DispatchDataset;
                if (locations == null) {
                    locations = new DispatchDataset();
                    DataSet ds = client.GetLocations();
                    if (ds != null) locations.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("locations",locations,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return locations;
        }
        public static DispatchDataset GetTerminals() {
            //Get terminal list
            DispatchDataset terminals = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                terminals = cache["terminals"] as DispatchDataset;
                if (terminals == null) {
                    terminals = new DispatchDataset();
                    DataSet ds = client.GetTerminals();
                    if (ds != null) terminals.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("terminals",terminals,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public static DispatchDataset GetVendors() {
            //Get vendor list
            DispatchDataset vendors = new DispatchDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                ObjectCache cache = MemoryCache.Default;
                vendors = cache["vendors"] as DispatchDataset;
                if(vendors == null) {
                    vendors = new DispatchDataset();
                    DataSet ds = client.GetVendors();
                    if(ds != null) vendors.Merge(ds);
                    client.Close();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(_cacheTimeout));
                    cache.Set("vendors", vendors, policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> efe) { client.Abort(); throw new ApplicationException(efe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return vendors;
        }
        public static DispatchDataset.VendorTableRow GetVendor(string vendorNumber) {
            //Get the requested vendor
            DispatchDataset.VendorTableRow vendor = null;
            try {
                DispatchDataset vendors = GetVendors();
                DataRow[] _vendors = vendors.VendorTable.Select("Number='" + vendorNumber + "'");
                if(_vendors.Length > 0) vendor = (DispatchDataset.VendorTableRow)_vendors[0];
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return vendor;
        }
    }
}