using System;
using System.Data;

namespace Argix.AgentLineHaul {
    //
    public class ShipSchedules {
        //Members
        private static ShipScheduleDataset _Schedules = null;
        public static event EventHandler SchedulesChanged = null;

        //Interface
        static ShipSchedules() {
            //Constructor
            _Schedules = new ShipScheduleDataset();
        }
        private ShipSchedules() { }
        public static ShipScheduleDataset Schedules { get { return _Schedules; } }
        public static void RefreshSchedules() {
            //Update a collection of all ship schedules for single terminal
            try {
                //Clear and update ship schedules
                _Schedules.Clear();
                DateTime dateMin = DateTime.Today.AddDays(-App.Config.ScheduleDaysBack);
                DateTime dateMax = DateTime.Today.AddDays(App.Config.ScheduleDaysForward);
                DateTime date = DateTime.Today;
                for(int i = 0; i < App.Config.PastBusinessDays; i++) {
                    date = date.AddDays(-1);
                    while(date.DayOfWeek == DayOfWeek.Saturday || date.DayOfWeek == DayOfWeek.Sunday) { date = date.AddDays(-1); }
                }
                ShipScheduleDataset schedules = ShipScheduleGateway.GetShipSchedules(date);
                _Schedules.Merge(schedules.ShipScheduleTable.Select("ScheduleDate >= '" + dateMin + "' AND ScheduleDate <= '" + dateMax + "'"));
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            finally { if(SchedulesChanged != null) SchedulesChanged(null, EventArgs.Empty); }
        }
        public static ShipSchedule SchedulesAdd(long sortCenterID, string sortCenter, DateTime scheduleDate) {
            //Add a new ship schedule
            ShipSchedule schedule = null;
            try {
                //Add and update collection
                ShipScheduleDataset ds = new ShipScheduleDataset();
                ds.ShipScheduleTable.AddShipScheduleTableRow("", sortCenterID, sortCenter, scheduleDate, DateTime.Now, Environment.UserName);
                schedule = new ShipSchedule(ds.ShipScheduleTable[0]);
                schedule.Changed += new EventHandler(OnShipScheduleChanged);
                schedule.Create();
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return schedule;
        }
        public static ShipSchedule SchedulesItem(string scheduleID) {
            //Return a new or an existing ship schedule
            ShipSchedule schedule = null;
            try {
                //Existing- import from collection
                DataRow[] rows = _Schedules.ShipScheduleTable.Select("ScheduleID='" + scheduleID + "'");
                if(rows.Length > 0) {
                    schedule = new ShipSchedule((ShipScheduleDataset.ShipScheduleTableRow)rows[0]);
                    schedule.Changed += new EventHandler(OnShipScheduleChanged);
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return schedule;
        }
        public static ShipSchedule SchedulesItem(long sortCenterID, DateTime date) {
            //Return a new or an existing ship schedule
            ShipSchedule schedule = null;
            try {
                //Existing- import from collection (notice table change)
                DataRow[] rows = _Schedules.ShipScheduleTable.Select("SortCenterID=" + sortCenterID + " AND ScheduleDate='" + date + "'");
                if(rows.Length > 0) {
                    schedule = new ShipSchedule((ShipScheduleDataset.ShipScheduleTableRow)rows[0]);
                    schedule.Changed += new EventHandler(OnShipScheduleChanged);
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return schedule;
        }
        public static ShipSchedule SchedulesArchiveItem(long sortCenterID, string sortCenter, DateTime scheduleDate) {
            //Return an archived ship schedule
            ShipSchedule schedule = null;
            try {
                //
                ShipScheduleDataset schedules = new ShipScheduleDataset();
                schedules.Merge(ShipScheduleGateway.GetShipSchedules(scheduleDate));
                if(schedules.ShipScheduleTable.Rows.Count > 0) {
                    DataRow[] rows = schedules.ShipScheduleTable.Select("SortCenterID=" + sortCenterID + " AND ScheduleDate='" + scheduleDate + "'");
                    if(rows.Length > 0) {
                        ShipScheduleDataset scheduleDS = new ShipScheduleDataset();
                        scheduleDS.ShipScheduleTable.AddShipScheduleTableRow("", sortCenterID, sortCenter, scheduleDate, DateTime.Now, Environment.UserName);
                        schedule = new ShipSchedule(scheduleDS.ShipScheduleTable[0]);
                        schedule.Changed += new EventHandler(OnShipScheduleChanged);
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return schedule;
        }
        private static void OnShipScheduleChanged(object sender, EventArgs e) {
            //Event handler for change in a (child) ship schedule
            try { RefreshSchedules(); }
            catch(Exception) { }
        }
    }
}
