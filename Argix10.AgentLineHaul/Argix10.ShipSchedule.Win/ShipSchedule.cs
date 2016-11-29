using System;
using System.ComponentModel;
using System.Data;

namespace Argix.AgentLineHaul {
    //
    public class ShipSchedule {
        //Members
        private string mScheduleID = "";
        private long mSortCenterID = 0;
        private string mSortCenter = "";
        private DateTime mScheduleDate = DateTime.Today;
        private ShipScheduleDataset mTrips = null, mTemplates = null;
        private AutoRefreshService mAutoRefreshSvc = null;

        public event EventHandler Changed = null;
        public event StatusEventHandler StatusMessage = null;

        //Interface
        public ShipSchedule() : this(null) { }
        public ShipSchedule(ShipScheduleDataset.ShipScheduleTableRow schedule) {
            //Constructor
            try {
                if(schedule != null) {
                    this.mScheduleID = schedule.ScheduleID;
                    this.mSortCenterID = schedule.SortCenterID;
                    this.mSortCenter = schedule.SortCenter.Trim();
                    this.mScheduleDate = schedule.ScheduleDate;
                }
                this.mTrips = new ShipScheduleDataset();
                this.mTemplates = new ShipScheduleDataset();
                this.mAutoRefreshSvc = new AutoRefreshService(this);
                Refresh();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        #region Accessors\Modifiers: ScheduleID, SortCenterID, SortCenter, ScheduleDate, Trips
        public string ScheduleID { get { return this.mScheduleID; } }
        public long SortCenterID { get { return this.mSortCenterID; } }
        public string SortCenter { get { return this.mSortCenter.Trim(); } }
        public DateTime ScheduleDate { get { return this.mScheduleDate; } }
        public ShipScheduleDataset Trips { get { return this.mTrips; } }
        public ShipScheduleDataset Templates { get { return this.mTemplates; } }
        public AutoRefreshService AutoRefreshSvc { get { return this.mAutoRefreshSvc; } }
        #endregion
        public void Refresh() {
            //Update a collection (dataset) of all ship schedule trips for the terminal and schedule date
            try {
                //Clear and update trips for this schedule
                this.mTrips.Clear();
                this.mTemplates.Clear();
                this.mTrips.Merge(ShipScheduleGateway.GetShipSchedule(this.mSortCenterID, this.mScheduleDate));
                this.mTemplates.Merge(ShipScheduleGateway.GetShipScheduleTemplates(this.mSortCenterID, this.mScheduleDate));
                foreach(ShipScheduleDataset.TemplateViewTableRow row in this.mTemplates.TemplateViewTable.Rows) row.Selected = (row.IsMandatory == 1);
                this.mTemplates.AcceptChanges();
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            finally { if(this.Changed != null) this.Changed(this, EventArgs.Empty); }
        }
        public void Create() {
            //Persist this definition
            try {
                //Validate
                if(this.mSortCenterID == 0) throw new ApplicationException("Failed to create new ship schedule: must have a valid sort center ID.");

                //Create a new ship schedule for this SortCenterID and ScheduleDate
                this.mScheduleID = ShipScheduleGateway.CreateShipSchedule(this.mSortCenterID, this.mScheduleDate, DateTime.Now, Environment.UserName);
                for(int i = 0; i < this.mTemplates.TemplateViewTable.Rows.Count; i++) {
                    //Add all mandatory loads only to the new schedule
                    if(this.mTemplates.TemplateViewTable[i].IsMandatory == 1)
                        ShipScheduleGateway.CreateShipScheduleTrip(this.mScheduleID, this.mTemplates.TemplateViewTable[i].TemplateID, DateTime.Now, Environment.UserName);
                }
                Refresh();
            }
            catch(ApplicationException aex) { throw aex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        public void Update() {
            //Extract the row versions and update the updated rows. This will keep the selected row in the view
            //Checks for duplicate load number within the same carrier during the past and future 7 days
            try {
                //Determine changes made to the trips in this ship schedule
                ShipScheduleDataset trips = (ShipScheduleDataset)this.mTrips.GetChanges(DataRowState.Modified);
                if(trips != null && trips.ShipScheduleViewTable.Rows.Count > 0) {
                    //Update each modified trip
                    foreach(ShipScheduleDataset.ShipScheduleViewTableRow row in trips.ShipScheduleViewTable.Rows) {
                        //Check to see if load# or carrier has changed; if so, then make sure it's unique within the same 
                        //carrier (updated once if it's updated along with load#) and during the past one week schedule
                        if(row.LoadNumber.Trim() != row["LoadNumber", DataRowVersion.Original].ToString().Trim() && row.CarrierServiceID.ToString().Trim() == row["CarrierServiceID", DataRowVersion.Original].ToString().Trim()) {
                            string tripID = ShipScheduleGateway.FindShipScheduleTrip(row.ScheduleDate, 0, row.LoadNumber.Trim());
                            if(tripID.Trim().Length > 0) throw new DuplicateLoadNumberException("Duplicate load# found in ship schedule for " + tripID + ".");
                        }

                        //Save trip details
                        ShipScheduleGateway.UpdateShipSchedule(row);
                        try {
                            //Refresh the details of the current trip (instead of a full refresh)
                            ShipScheduleDataset viewItems = ShipScheduleGateway.GetShipSchedule(row.SortCenterID, row.ScheduleDate);
                            if(viewItems.ShipScheduleViewTable.Rows.Count > 0) {
                                ShipScheduleDataset.ShipScheduleViewTableRow viewItem = viewItems.ShipScheduleViewTable.FindByTripID(row.TripID);
                                ShipScheduleDataset.ShipScheduleViewTableRow trip = this.mTrips.ShipScheduleViewTable.FindByTripID(row.TripID);
                                trip.SCDERowVersion = viewItem.SCDERowVersion;
                                trip.S1RowVersion = viewItem.S1RowVersion;
                                if(!row.IsS2StopIDNull()) trip.S2RowVersion = viewItem.S2RowVersion;
                                this.mTrips.AcceptChanges();
                            }
                        }
                        catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
                    }
                    //Refresh();		Doing partial refresh above for performance reasons (i.e. cell editing)
                }
            }
            catch(DuplicateLoadNumberException ex) { throw ex; }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }
        public void AddLoads() {
            //Add selected template loads to this schedule
            try {
                for(int i = 0; i < this.mTemplates.TemplateViewTable.Rows.Count; i++) {
                    if(this.mTemplates.TemplateViewTable[i].Selected)
                        ShipScheduleGateway.CreateShipScheduleTrip(this.mScheduleID, this.mTemplates.TemplateViewTable[i].TemplateID, DateTime.Now, Environment.UserName);
                }
                Refresh();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
        }

        #region AutoRefresh Services: OnAutoRefreshCompleted()
        public void StartAutoRefresh() {
            if(global::Argix.Properties.Settings.Default.AutoRefreshEnabled) {
                reportStatus(new StatusEventArgs("Starting auto-refresh on " + this.mSortCenter + ", " + this.mScheduleDate.ToShortDateString() + "..."));
                this.mAutoRefreshSvc.Start();
            }
        }
        public void StopAutoRefresh() {
            if(global::Argix.Properties.Settings.Default.AutoRefreshEnabled) {
                reportStatus(new StatusEventArgs("Stopping auto-refresh on " + this.mSortCenter + ", " + this.mScheduleDate.ToShortDateString() + "..."));
            }
            this.mAutoRefreshSvc.Stop();
        }
        public void OnAutoRefreshCompleted(object sender, RunWorkerCompletedEventArgs e) {
            //
            try {
                ShipScheduleDataset ds = null;
                //if(this.InvokeRequired) {
                //    this.Invoke(new RunWorkerCompletedEventHandler(OnAutoRefreshCompleted), new object[] { sender, e });
                //}
                //else {
                    ds = (ShipScheduleDataset)e.Result;
                    //if(this.grdTLs.ActiveCell == null || !this.grdTLs.ActiveCell.IsInEditMode) {
                        lock(this.mTrips) {
                            this.mTrips.Clear();
                            this.mTrips.Merge(ds.Tables["ShipScheduleViewTable"]);
                        }
                        lock(this.mTemplates) {
                            this.mTemplates.Clear();
                            this.mTemplates.Merge(ds.Tables["TemplateViewTable"]);
                        }
                    //}
                //}
            }
            catch { }
            finally { if(this.Changed != null) this.Changed(this, EventArgs.Empty); }
        }
        #endregion

        private void reportStatus(StatusEventArgs e) { if(this.StatusMessage != null) this.StatusMessage(this, e); }
    }

    public class AutoRefreshService {
        //Members
        private System.Windows.Forms.Timer mTimer = null;
        private BackgroundWorker mWorker = null;
        private long mSortCenterID = 0;
        private DateTime mScheduleDate = DateTime.MinValue;

        //Interface
        public AutoRefreshService(ShipSchedule schedule) {
            //
            this.mSortCenterID = schedule.SortCenterID;
            this.mScheduleDate = schedule.ScheduleDate;
            this.mTimer = new System.Windows.Forms.Timer();
            this.mTimer.Interval = global::Argix.Properties.Settings.Default.AutoRefreshTimer;
            this.mTimer.Tick += new EventHandler(OnTick);
            this.mWorker = new BackgroundWorker();
            this.mWorker.DoWork += new DoWorkEventHandler(OnAutoRefresh);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(schedule.OnAutoRefreshCompleted);
        }
        public void Start() { this.mTimer.Start(); }
        public void Stop() { this.mTimer.Stop(); }

        private void OnTick(object sender, EventArgs e) {
            //Event handler for timer tick event
            try { if(!this.mWorker.IsBusy) this.mWorker.RunWorkerAsync(); }
            catch { }
        }
        private void OnAutoRefresh(object sender, DoWorkEventArgs e) {
            //Event handler for background worker thread DoWork event; runs on worker thread
            try {
                ShipScheduleDataset ds = new ShipScheduleDataset();
                ds.Merge(ShipScheduleGateway.GetShipSchedule(this.mSortCenterID, this.mScheduleDate));
                ds.Merge(ShipScheduleGateway.GetShipScheduleTemplates(this.mSortCenterID, this.mScheduleDate));
                e.Result = ds;
            }
            catch { }
        }
    }
}
