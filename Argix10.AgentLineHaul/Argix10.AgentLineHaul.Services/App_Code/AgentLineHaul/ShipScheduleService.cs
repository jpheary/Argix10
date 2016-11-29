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
    public class ShipScheduleService:IShipScheduleService,IZoneClosingService {
        //Members

        //Interface
        public ServiceInfo GetServiceInfo(int terminalID) {
            //Get service information
            return new Argix.AppService(new TsortGateway(terminalID).SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(new TsortGateway(terminalID).SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(int terminalID,TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet GetShipSchedules(int terminalID,DateTime scheduleDate) {
            //Get a list of ship schedules going back the specified number of business days
            DataSet schedules = new DataSet();
            try {
                DataSet ds = new TsortGateway(terminalID).GetShipSchedulesList(scheduleDate);
                if(ds != null && ds.Tables["ShipScheduleTable"] != null && ds.Tables["ShipScheduleTable"].Rows.Count > 0) schedules.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return schedules;
        }
        public DataSet GetShipSchedule(int terminalID,long sortCenterID,DateTime scheduleDate) {
            //Get the ship schedule for the specified sortCenterID and scheduleDate
            DataSet schedule = new DataSet();
            try {
                DataSet ds = new TsortGateway(terminalID).GetShipSchedule(sortCenterID,scheduleDate);
                if(ds != null && ds.Tables["ShipScheduleViewTable"] != null && ds.Tables["ShipScheduleViewTable"].Rows.Count > 0) schedule.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return schedule;
        }
        public DataSet GetShipScheduleTemplates(int terminalID,long sortCenterID,DateTime scheduleDate) {
            //Get templates associated with the ship schedule specified by sortCenterID and scheduleDate
            DataSet templates = new DataSet();
            try {
                DataSet ds = new TsortGateway(terminalID).GetShipScheduleTemplates(sortCenterID,scheduleDate);
                if(ds != null && ds.Tables["TemplateViewTable"] != null && ds.Tables["TemplateViewTable"].Rows.Count > 0) templates.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return templates;
        }
        public string CreateShipSchedule(int terminalID,long sortCenterID,DateTime scheduleDate,DateTime lastUpdated,string userID) {
            //Create a new ship schedule for the specified sortCenterID and scheduleDate
            string scheduleID = "";
            try {
                scheduleID = new TsortGateway(terminalID).CreateShipSchedule(sortCenterID,scheduleDate,lastUpdated,userID);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return scheduleID;
        }
        public string CreateShipScheduleTrip(int terminalID,string scheduleID,string templateID,DateTime lastUpdated,string userID) {
            //Create a new ship schedule trip from a template specified by templateID on the ship schedule specified by scheduleID
            string tripID = "";
            try {
                tripID = new TsortGateway(terminalID).CreateShipScheduleTrip(scheduleID.Trim(),templateID.Trim(),lastUpdated,userID);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return tripID;
        }
        public string FindShipScheduleTrip(int terminalID,DateTime scheduleDate,long carrierServiceID,string loadNumber) {
            //Find a ship schedue trip for the specified scheduleDate, carrierServiceID, and loadNumber
            string tripID = "";
            try {
                tripID = new TsortGateway(terminalID).FindShipScheduleTrip(scheduleDate,carrierServiceID,loadNumber);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return tripID;
        }
        public bool UpdateShipSchedule(int terminalID,ShipScheduleTrip trip,ShipScheduleStop stop1,ShipScheduleStop stop2) {
            //Update the specified ship schedule including the trip and 2 stops
            bool updated = false;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //UPdate trip and stop1; update stop2 if not null
                    new TsortGateway(terminalID).UpdateShipScheduleTrip(trip);
                    new TsortGateway(terminalID).UpdateShipScheduleTripStop(stop1);
                    if(stop2 != null) new TsortGateway(terminalID).UpdateShipScheduleTripStop(stop2);
                    updated = true;

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return updated;
        }
        public bool UpdateShipScheduleTrip(int terminalID,ShipScheduleTrip trip) {
            //Update the specified ship schedule trip
            bool updated = false;
            try {
                updated = new TsortGateway(terminalID).UpdateShipScheduleTrip(trip);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return updated;
        }
        public bool UpdateShipScheduleTripStop(int terminalID,ShipScheduleStop stop) {
            //Update the specified ship schedule stop
            bool updated = false;
            try {
                updated = new TsortGateway(terminalID).UpdateShipScheduleTripStop(stop);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return updated;
        }
        public DataSet GetSortCenters(int terminalID) {
            DataSet sortCenters = new DataSet();
            DataSet ds = null;
            try {
                if(terminalID == 0) {
                    ds = new TsortGateway(terminalID).GetTerminals();
                    if(ds != null && ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0)
                        sortCenters.Merge(ds.Tables["TerminalTable"].Select("", "Description ASC"));
                } 
                else if(terminalID == 1) {
                    ds = new TsortGateway(terminalID).GetShippers();
                    if(ds != null && ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0)
                        sortCenters.Merge(ds.Tables["TerminalTable"].Select("", "Description ASC"));
                }
                else if(terminalID > 1) {
                    ds = new TsortGateway(terminalID).GetTerminals();
                    if(ds != null && ds.Tables["TerminalTable"] != null && ds.Tables["TerminalTable"].Rows.Count > 0)
                        sortCenters.Merge(ds.Tables["TerminalTable"].Select("", "Description ASC"));
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return sortCenters;
        }
        public DataSet GetCarriers(int terminalID) {
            DataSet carriers = new DataSet();
            try {
                if(terminalID >= 0) {
                    DataSet ds = new TsortGateway(terminalID).GetCarriers();
                    if(ds != null && ds.Tables["CarrierTable"] != null && ds.Tables["CarrierTable"].Rows.Count > 0)
                        carriers.Merge(ds.Tables["CarrierTable"].Select("", "Description ASC"));
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return carriers;
        }

        public DataSet GetShipScheduleView(int terminalID,DateTime scheduleDate) {
            //Return a ship schedule for the specified terminal
            DataSet schedule = new DataSet();
            try {
                //AgentLineHaulDataset trips = new AgentLineHaulDataset();
                DataSet trips = new TsortGateway(terminalID).GetTrips(scheduleDate);
                if(trips != null && trips.Tables["ShipScheduleViewTable"] != null && trips.Tables["ShipScheduleViewTable"].Rows.Count > 0) {
                    schedule.Merge(trips);
                    string scheduleID = trips.Tables["ShipScheduleViewTable"].Rows[0]["ScheduleID"].ToString();

                    //AgentLineHaulDataset tls = new AgentLineHaulDataset();
                    DataSet tls = new TsortGateway(terminalID).GetTLs(scheduleID);
                    if(tls != null && tls.Tables["ShipScheduleTLTable"] != null && tls.Tables["ShipScheduleTLTable"].Rows.Count > 0) 
                    schedule.Merge(tls);
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return schedule;
        }
        public DateTime FindEarlierTripOnPriorSchedule(int terminalID,DateTime scheduleDate,string tripID,string freightID) {
            //Return an earlier trip from a schedule prior to the one specified
            DateTime tripDate = DateTime.MinValue;
            try {
                //Get all earlier trips (open, not cancelled)
                DataSet schedule = new DataSet();
                DataSet ds = new TsortGateway(terminalID).GetPriorTrips(tripID,freightID);
                if(ds != null && ds.Tables["ShipScheduleViewTable"] != null && ds.Tables["ShipScheduleViewTable"].Rows.Count > 0) {
                    schedule.Merge(ds);
                    DateTime date = scheduleDate.AddYears(-5);
                    int tag = 0;
                    for (int i = 0;i < schedule.Tables["ShipScheduleViewTable"].Rows.Count;i++) {
                        //Select a trip with the most recent schedule date (not including trip.ScheduleDate)
                        DataRow _trip = schedule.Tables["ShipScheduleViewTable"].Rows[i];
                        DateTime _scheduleDate = DateTime.Parse(_trip["ScheduleDate"].ToString());
                        if(_scheduleDate.CompareTo(scheduleDate) < 0) {
                            //Capture the most recent trip date
                            if(_scheduleDate.CompareTo(date) > 0) { date = _scheduleDate; tag = 0; }
                            if(_scheduleDate.CompareTo(date) == 0) {
                                //Capture the trip that is most recent and with the largest tag #
                                int _tag = int.Parse(_trip["Tag"].ToString().Trim());
                                if(_tag > tag) {
                                    tag = _tag;
                                    tripDate = _scheduleDate;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return tripDate;
        }
        public DateTime FindEarlierTripOnCurrentSchedule(int terminalID,DateTime scheduleDate,string tripID,string freightID) {
            //Return an earlier trip from the current schedule than the one specified if one exists
            DateTime tripDate = DateTime.MinValue;
            try {
                //Get all earlier trips (open, not cancelled)
                DataSet schedule = new DataSet();
                DataSet ds = new TsortGateway(terminalID).GetPriorTrips(tripID,freightID);
                if(ds != null && ds.Tables["ShipScheduleViewTable"] != null && ds.Tables["ShipScheduleViewTable"].Rows.Count > 0) {
                    schedule.Merge(ds);
                    int tag = 0;
                    for(int i = 0; i < schedule.Tables["ShipScheduleViewTable"].Rows.Count; i++) {
                        //Select only trips with the same schedule date as this.mScheduleDate
                        DataRow _trip = schedule.Tables["ShipScheduleViewTable"].Rows[i];
                        DateTime _scheduleDate = DateTime.Parse(_trip["ScheduleDate"].ToString());
                        if(_scheduleDate.CompareTo(scheduleDate) == 0) {
                            //Capture the trip with the largest tag #
                            int _tag = int.Parse(_trip["Tag"].ToString().Trim());
                            if(_tag > tag) {
                                tag = _tag;
                                tripDate = _scheduleDate;
                            }
                        }
                    }
                }
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return tripDate;
        }
        public DateTime FindShipSchedule(int terminalID,string tlNumber) {
            //Find a TL on an existing ship schedule
            DateTime schedulDate = DateTime.MinValue;
            try {
                schedulDate = new TsortGateway(terminalID).FindShipSchedule(tlNumber);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return schedulDate;
        }
        public bool OpenTrip(int terminalID,string tripID) {
            //Open a trip to further TL assignments
            bool ret = false;
            try {
                //Validate, then open
                ret = new TsortGateway(terminalID).UpdateTripStatus(tripID,0);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return ret;
        }
        public bool CloseTrip(int terminalID,string tripID) {
            //Close a trip from further TL assignments
            bool ret = false;
            try {
                ret = new TsortGateway(terminalID).UpdateTripStatus(tripID,1);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return ret;
        }
        public bool AssignTL(int terminalID,string tripID,string tl) {
            //Assign an open TL to this trip
            bool ret = false;
            try {
                ret = new TsortGateway(terminalID).AssignTL(tripID,tl);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return ret;
        }
        public bool UnassignTL(int terminalID,string tl) {
            //Unassign an open TL from this trip
            bool ret = false;
            try {
                ret = new TsortGateway(terminalID).UnassignTL(tl);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return ret;
        }
        public bool MoveTL(int terminalID,string tripID,string tl) {
            //Move a closed TL to this trip
            bool ret = false;
            try {
                ret = new TsortGateway(terminalID).MoveTL(tripID,tl);
            }
            catch (Exception ex) { throw new FaultException<ShipScheduleFault>(new ShipScheduleFault(ex.Message),"Service Error"); }
            return ret;
        }
    }
}
