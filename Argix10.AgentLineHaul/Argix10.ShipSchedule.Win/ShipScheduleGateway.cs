using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.ServiceModel;
using Argix.Windows;

namespace Argix.AgentLineHaul {
    //
    public class ShipScheduleGateway {
        //Members
        private static bool _state = false;
        private static string _address = "";

        //Interface
        static ShipScheduleGateway() {
            //
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private ShipScheduleGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal = null;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                terminal = client.GetServiceInfo(int.Parse(Program.TerminalCode));
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application, string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config = null;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                config = client.GetUserConfiguration(int.Parse(Program.TerminalCode), application, usernames);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                client.WriteLogEntry(int.Parse(Program.TerminalCode), m);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static ShipScheduleDataset GetShipSchedules(DateTime scheduleDate) {
            //Get a list of ship schedules for the specified date
            ShipScheduleDataset schedules = new ShipScheduleDataset();
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                schedules.Merge(client.GetShipSchedules(int.Parse(Program.TerminalCode), scheduleDate));
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedules;
        }
        public static ShipScheduleDataset GetShipSchedule(long sortCenterID, DateTime scheduleDate) {
            //Return a ship schedule for the specified sort center and date
            ShipScheduleDataset schedule = new ShipScheduleDataset();
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                schedule.Merge(client.GetShipSchedule(int.Parse(Program.TerminalCode), sortCenterID, scheduleDate));
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return schedule;
        }
        public static ShipScheduleDataset GetShipScheduleTemplates(long sortCenterID, DateTime scheduleDate) {
            //Return templates for the ship schedule specified by sort center and date
            ShipScheduleDataset templates = new ShipScheduleDataset();
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                templates.Merge(client.GetShipScheduleTemplates(int.Parse(Program.TerminalCode), sortCenterID, scheduleDate));
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return templates;
        }
        public static string CreateShipSchedule(long sortCenterID, DateTime scheduleDate, DateTime lastUpdated, string userID) {
            //Create a new ship schedule for the specified sort center and date
            string scheduleID = "";
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                scheduleID = client.CreateShipSchedule(int.Parse(Program.TerminalCode), sortCenterID, scheduleDate, lastUpdated, userID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return scheduleID;
        }
        public static string CreateShipScheduleTrip(string scheduleID, string templateID, DateTime lastUpdated, string userID) {
            //Create a new ship schedule trip for the specified schedule using the specified template
            string tripID = "";
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                tripID = client.CreateShipScheduleTrip(int.Parse(Program.TerminalCode), scheduleID, templateID, lastUpdated, userID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tripID;
        }
        public static bool UpdateShipSchedule(ShipScheduleDataset.ShipScheduleViewTableRow viewItem) {
            //
            bool updated = false;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                //Map datarows to data transfer objects
                ShipScheduleTrip sstrip = new ShipScheduleTrip();
                sstrip.TripID = viewItem.TripID;
                sstrip.ScheduleID = viewItem.ScheduleID;
                sstrip.TemplateID = viewItem.TemplateID;
                sstrip.BolNumber = !viewItem.IsBolNumberNull() ? viewItem.BolNumber : 0;
                sstrip.CarrierServiceID = !viewItem.IsCarrierServiceIDNull() ? viewItem.CarrierServiceID : 0;
                sstrip.LoadNumber = !viewItem.IsLoadNumberNull() ? viewItem.LoadNumber : "";
                sstrip.TrailerNumber = !viewItem.IsTrailerNumberNull() ? viewItem.TrailerNumber : "";
                sstrip.TractorNumber = !viewItem.IsTractorNumberNull() ? viewItem.TractorNumber : "";
                sstrip.DriverName = !viewItem.IsDriverNameNull() ? viewItem.DriverName : "";
                if(!viewItem.IsScheduledCloseNull()) sstrip.ScheduledClose = viewItem.ScheduledClose;
                if(!viewItem.IsScheduledDepartureNull()) sstrip.ScheduledDeparture = viewItem.ScheduledDeparture;
                sstrip.IsMandatory = !viewItem.IsIsMandatoryNull() ? viewItem.IsMandatory : (byte)0;
                if(!viewItem.IsFreightAssignedNull()) sstrip.FreightAssigned = viewItem.FreightAssigned;
                if(!viewItem.IsTrailerCompleteNull()) sstrip.TrailerComplete = viewItem.TrailerComplete;
                if(!viewItem.IsPaperworkCompleteNull()) sstrip.PaperworkComplete = viewItem.PaperworkComplete;
                if(!viewItem.IsTrailerDispatchedNull()) sstrip.TrailerDispatched = viewItem.TrailerDispatched;
                if(!viewItem.IsCanceledNull()) sstrip.Canceled = viewItem.Canceled;
                sstrip.LastUpdated = DateTime.Now;
                sstrip.UserID = Environment.UserName;
                sstrip.RowVersion = viewItem.SCDERowVersion;

                ShipScheduleStop ssstop1 = new ShipScheduleStop();
                ssstop1.StopID = viewItem.StopID;
                ssstop1.TripID = viewItem.TripID;
                ssstop1.StopNumber = !viewItem.IsStopNumberNull() ? viewItem.StopNumber : "";
                ssstop1.AgentTerminalID = !viewItem.IsAgentTerminalIDNull() ? viewItem.AgentTerminalID : 0;
                ssstop1.Tag = !viewItem.IsTagNull() ? viewItem.Tag : "";
                ssstop1.Notes = !viewItem.IsNotesNull() ? viewItem.Notes : "";
                if(!viewItem.IsScheduledArrivalNull()) ssstop1.ScheduledArrival = viewItem.ScheduledArrival;
                if(!viewItem.IsScheduledOFD1Null()) ssstop1.ScheduledOFD1 = viewItem.ScheduledOFD1;
                ssstop1.LastUpdated = DateTime.Now;
                ssstop1.UserID = Environment.UserName;
                ssstop1.RowVersion = viewItem.S1RowVersion;

                ShipScheduleStop ssstop2 = null;
                if(!viewItem.IsS2MainZoneNull() && viewItem.S2MainZone.Trim().Length > 0) {
                    ssstop2 = new ShipScheduleStop();
                    ssstop2.StopID = viewItem.S2StopID;
                    ssstop2.TripID = viewItem.TripID;
                    ssstop2.StopNumber = !viewItem.IsS2StopNumberNull() ? viewItem.S2StopNumber : "";
                    ssstop2.AgentTerminalID = !viewItem.IsS2AgentTerminalIDNull() ? viewItem.S2AgentTerminalID : 0;
                    ssstop2.Tag = !viewItem.IsS2TagNull() ? viewItem.S2Tag : "";
                    ssstop2.Notes = !viewItem.IsS2NotesNull() ? viewItem.S2Notes : "";
                    if(!viewItem.IsS2ScheduledArrivalNull()) ssstop2.ScheduledArrival = viewItem.S2ScheduledArrival;
                    if(!viewItem.IsS2ScheduledOFD1Null()) ssstop2.ScheduledOFD1 = viewItem.S2ScheduledOFD1;
                    ssstop2.LastUpdated = DateTime.Now;
                    ssstop2.UserID = Environment.UserName;
                    ssstop2.RowVersion = viewItem.S2RowVersion;
                }

                updated = client.UpdateShipSchedule(int.Parse(Program.TerminalCode), sstrip, ssstop1, ssstop2);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool UpdateShipScheduleTrip(ShipScheduleDataset.ShipScheduleTripTableRow trip) {
            //
            bool updated = false;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                //Map datarow to data transfer object
                ShipScheduleTrip sstrip = new ShipScheduleTrip();
                sstrip.TripID = trip.TripID;
                sstrip.ScheduleID = trip.ScheduleID;
                sstrip.TemplateID = trip.TemplateID;
                sstrip.BolNumber = !trip.IsBolNumberNull() ? trip.BolNumber : 0;
                sstrip.CarrierServiceID = !trip.IsCarrierServiceIDNull() ? trip.CarrierServiceID : 0;
                sstrip.LoadNumber = !trip.IsLoadNumberNull() ? trip.LoadNumber : "";
                sstrip.TrailerNumber = !trip.IsTrailerNumberNull() ? trip.TrailerNumber : "";
                sstrip.TractorNumber = !trip.IsTractorNumberNull() ? trip.TractorNumber : "";
                sstrip.DriverName = !trip.IsDriverNameNull() ? trip.DriverName : "";
                if(!trip.IsScheduledCloseNull()) sstrip.ScheduledClose = trip.ScheduledClose;
                if(!trip.IsScheduledDepartureNull()) sstrip.ScheduledDeparture = trip.ScheduledDeparture;
                sstrip.IsMandatory = !trip.IsIsMandatoryNull() ? trip.IsMandatory : (byte)0;
                if(!trip.IsFreightAssignedNull()) sstrip.FreightAssigned = trip.FreightAssigned;
                if(!trip.IsTrailerCompleteNull()) sstrip.TrailerComplete = trip.TrailerComplete;
                if(!trip.IsPaperworkCompleteNull()) sstrip.PaperworkComplete = trip.PaperworkComplete;
                if(!trip.IsTrailerDispatchedNull()) sstrip.TrailerDispatched = trip.TrailerDispatched;
                if(!trip.IsCanceledNull()) sstrip.Canceled = trip.Canceled;
                sstrip.LastUpdated = DateTime.Now;
                sstrip.UserID = Environment.UserName;
                sstrip.RowVersion = trip.RowVersion;

                updated = client.UpdateShipScheduleTrip(int.Parse(Program.TerminalCode), sstrip);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static bool UpdateShipScheduleTripStop(ShipScheduleDataset.ShipScheduleStopTableRow stop) {
            //
            bool updated = false;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                //Map datarow to data transfer object
                ShipScheduleStop ssstop = new ShipScheduleStop();
                ssstop.StopID = stop.StopID;
                ssstop.TripID = stop.TripID;
                ssstop.StopNumber = !stop.IsStopNumberNull() ? stop.StopNumber : "";
                ssstop.AgentTerminalID = !stop.IsAgentTerminalIDNull() ? stop.AgentTerminalID : 0;
                ssstop.Tag = !stop.IsTagNull() ? stop.Tag : "";
                ssstop.Notes = !stop.IsNotesNull() ? stop.Notes : "";
                if(!stop.IsScheduledArrivalNull()) ssstop.ScheduledArrival = stop.ScheduledArrival;
                if(!stop.IsScheduledOFD1Null()) ssstop.ScheduledOFD1 = stop.ScheduledOFD1;
                ssstop.LastUpdated = DateTime.Now;
                ssstop.UserID = Environment.UserName;
                ssstop.RowVersion = stop.RowVersion;

                updated = client.UpdateShipScheduleTripStop(int.Parse(Program.TerminalCode), ssstop);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }
        public static string FindShipScheduleTrip(DateTime scheduleDate, long carrierServiceID, string loadNumber) {
            //
            string tripID = "";
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                tripID = client.FindShipScheduleTrip(int.Parse(Program.TerminalCode), scheduleDate, carrierServiceID, loadNumber);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ShipScheduleFault> ssf) { client.Abort(); throw new ApplicationException(ssf.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return tripID;
        }
        public static DataSet GetSortCenters() {
            //Get a list of sortCenters (terminals or shippers)
            DataSet sortCenters = null;
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                sortCenters = client.GetSortCenters(int.Parse(Program.TerminalCode));
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return sortCenters;
        }
        public static DataSet GetCarriers() {
            //
            DataSet carriers = new DataSet();
            ShipScheduleServiceClient client = new ShipScheduleServiceClient();
            try {
                DataSet ds = client.GetCarriers(int.Parse(Program.TerminalCode));
                if(ds != null) carriers.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return carriers;
        }
    }
}