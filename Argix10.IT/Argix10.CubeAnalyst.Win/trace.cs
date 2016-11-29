using System;
using System.Data;
using System.Diagnostics;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Text;
using Argix.Data;

namespace Argix.Tools {
	//
	public class TraceLog {
		//Members
		private string mEventLogName="";			//Application key in database configuration table
		private ScannerDS mLogEntries=null;			//Collection of log entries 
		private int mDaysBack=1;
		private Mediator mMediator=null;			//Database connectivity
		
		//Constants
		public const int MINDAYS_BACK = 1;
		public const int MAXDAYS_BACK = 365;
		
		//Events
		public event EventHandler Refreshed=null;
		
		//Interface
		public TraceLog(string logName, int initialDaysback, string DBConnection) { 
			//Constructor
			try {
				//Set custom attributes
				this.mEventLogName = logName;
				this.mDaysBack = initialDaysback;
				this.mMediator = new SQLMediator(DBConnection);
//				this.mMediator.DataStatusUpdate += new DataStatusHandler(frmMain.OnDataStatusUpdate);
				this.mLogEntries = new ScannerDS();
			}
			catch(Exception ex) { throw ex; }
		}
		#region Accessors/Modifiers
		#endregion
		public ScannerDS LogEntries { get { return this.mLogEntries; } }
		public int DaysBack { 
			get { return this.mDaysBack; } 
			set { 
				if(value < MINDAYS_BACK)
					throw new ApplicationException("Days back cannot be leass than " + MINDAYS_BACK.ToString() + " day.");
				if(value > MAXDAYS_BACK)
					throw new ApplicationException("Days back cannot be greater than " + MAXDAYS_BACK.ToString() + " day.");
				if(value != this.mDaysBack) this.mDaysBack = value;
				Refresh();
			} 
		} 
		public void Refresh() { 
			//Refresh data for this object
			try {
				//Clear cuurent collection and refresh
				this.mLogEntries.Clear();
				this.mLogEntries.Merge(this.mMediator.FillDataset(App.USP_TRACELOG_GETLIST, App.TBL_TRACELOG_GETLIST, new object[]{this.mEventLogName, DateTime.Today.AddDays(-this.mDaysBack), DateTime.Now}));
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.Refreshed != null)this.Refreshed(this, EventArgs.Empty); }
		}
		public bool Add(TraceLogEntry entry) {
			//Add a new trace log entry
			return false;
		}
		public int Count { get { return this.mLogEntries.ArgixLogTable.Rows.Count; } }
		public TraceLogEntry Item(int id) {
			//Return an existing log entry from the log collection
			TraceLogEntry entry=null;
			try { 
				//Merge from collection (dataset)
				if(id > 0) {
					DataRow[] rows = this.mLogEntries.ArgixLogTable.Select("ID=" + id);
					if(rows.Length == 0)
						throw new ApplicationException("Log entry for id=" + id + " does not exist in this log table!\n");
					ScannerDS.ArgixLogTableRow row = (ScannerDS.ArgixLogTableRow)rows[0];
					entry = new TraceLogEntry(row, this.mMediator);
					entry.Changed += new EventHandler(OnEntryChanged);
				}
			}
			catch (Exception ex) { throw ex; }
			return entry;
		}
		public bool Remove(TraceLogEntry entry) {
			//Remove the specified trace log entry
			bool bRet=false;
			try { 
				bRet = entry.Delete();
			}
			catch(Exception ex) { throw ex; }
			return bRet;
		}
		
		private void OnEntryChanged(object sender, EventArgs e) {
			//Event handler for change to a log entry
			try { Refresh(); } catch(Exception) { }
		}
	}

    public class TraceLogEntry {
        //Members
        private System.Int64 _id=0;
        private System.String _name="";
        private System.Int32 _level=0;
        private System.DateTime _date;
        private System.String _source="";
        private System.String _category="";
        private System.String _event="";
        private System.String _user="";
        private System.String _computer="";
        private System.String _keyword1="";
        private System.String _keyword2="";
        private System.String _keyword3="";
        private System.String _message="";
        private Mediator mMediator=null;

        //Constants

        //Events
        public event EventHandler Changed=null;

        //Interface
        public TraceLogEntry(Mediator mediator) : this(null,mediator) { }
        public TraceLogEntry(ScannerDS.ArgixLogTableRow logEntry,Mediator mediator) {
            //Constructor
            try {
                this.mMediator = mediator;
                if(logEntry != null) {
                    if(!logEntry.IsIDNull()) this._id = logEntry.ID;
                    if(!logEntry.IsNameNull()) this._name = logEntry.Name;
                    if(!logEntry.IsLevelNull()) this._level = logEntry.Level;
                    if(!logEntry.IsDateNull()) this._date = logEntry.Date;
                    if(!logEntry.IsSourceNull()) this._source = logEntry.Source;
                    if(!logEntry.IsCategoryNull()) this._category = logEntry.Category;
                    if(!logEntry.IsEventNull()) this._event = logEntry.Event;
                    if(!logEntry.IsUserNull()) this._user = logEntry.User;
                    if(!logEntry.IsComputerNull()) this._computer = logEntry.Computer;
                    if(!logEntry.IsKeyword1Null()) this._keyword1 = logEntry.Keyword1;
                    if(!logEntry.IsKeyword2Null()) this._keyword2 = logEntry.Keyword2;
                    if(!logEntry.IsKeyword3Null()) this._keyword3 = logEntry.Keyword3;
                    if(!logEntry.IsMessageNull()) this._message = logEntry.Message;
                }
            }
            catch(Exception ex) { throw ex; }
        }
        #region Accessors\Modifiers, ToDataSet(), ToString()
        public System.Int64 ID {
            get { return this._id; }
            set { this._id = value; }
        }
        public System.String Name {
            get { return this._name; }
            set { this._name = value; }
        }
        public System.Int32 Level {
            get { return this._level; }
            set { this._level = value; }
        }
        public System.DateTime Date {
            get { return this._date; }
            set { this._date = value; }
        }
        public System.String Source {
            get { return this._source; }
            set { this._source = value; }
        }
        public System.String Category {
            get { return this._category; }
            set { this._category = value; }
        }
        public System.String Event {
            get { return this._event; }
            set { this._event = value; }
        }
        public System.String User {
            get { return this._user; }
            set { this._user = value; }
        }
        public System.String Computer {
            get { return this._computer; }
            set { this._computer = value; }
        }
        public System.String Keyword1 {
            get { return this._keyword1; }
            set { this._keyword1 = value; }
        }
        public System.String Keyword2 {
            get { return this._keyword2; }
            set { this._keyword2 = value; }
        }
        public System.String Keyword3 {
            get { return this._keyword3; }
            set { this._keyword3 = value; }
        }
        public System.String Message {
            get { return this._message; }
            set { this._message = value; }
        }
        public DataSet ToDataSet() {
            //Return a dataset containing values for this object
            ScannerDS ds=null;
            try {
                ds = new ScannerDS();
                ScannerDS.ArgixLogTableRow logEntry = ds.ArgixLogTable.NewArgixLogTableRow();
                logEntry.ID = this._id;
                logEntry.Name = this._name;
                logEntry.Level = this._level;
                logEntry.Date = this._date;
                logEntry.Source = this._source;
                logEntry.Category = this._category;
                logEntry.Event = this._event;
                logEntry.User = this._user;
                logEntry.Computer = this._computer;
                logEntry.Keyword1 = this._keyword1;
                logEntry.Keyword2 = this._keyword2;
                logEntry.Keyword3 = this._keyword3;
                logEntry.Message = this._message;
                ds.ArgixLogTable.AddArgixLogTableRow(logEntry);
            }
            catch(Exception) { }
            return ds;
        }
        public override string ToString() {
            //Custom ToString() method
            string sThis=base.ToString();
            try {
                //Form string detail of this object
                StringBuilder builder = new StringBuilder();
                builder.Append("ID=" + this._id.ToString() + "\t");
                builder.Append("Name=" + this._name.ToString() + "\t");
                builder.Append("Level=" + this._level.ToString() + "\t");
                builder.Append("Date=" + this._date.ToString() + "\t");
                builder.Append("Source=" + this._source.ToString() + "\t");
                builder.Append("Category=" + this._category.ToString() + "\t");
                builder.Append("Event=" + this._event.ToString() + "\t");
                builder.Append("User=" + this._user.ToString() + "\t");
                builder.Append("Computer=" + this._computer.ToString() + "\t");
                builder.Append("Keyword1=" + this._keyword1.ToString() + "\t");
                builder.Append("Keyword2=" + this._keyword2.ToString() + "\t");
                builder.Append("Keyword3=" + this._keyword3.ToString() + "\t");
                builder.Append("Message=" + this._message.ToString() + "\t");
                builder.Append("\n");
                sThis = builder.ToString();
            }
            catch(Exception) { }
            return sThis;
        }
        #endregion
        public bool Delete() {
            //Delete this log entry
            bool bRet=false;
            try {
                bRet = this.mMediator.ExecuteNonQuery(App.USP_TRACELOG_DELETE,new object[] { this._id });
                if(this.Changed != null) this.Changed(this,new EventArgs());
            }
            catch(Exception ex) { throw ex; }
            return bRet;
        }
    }
}
