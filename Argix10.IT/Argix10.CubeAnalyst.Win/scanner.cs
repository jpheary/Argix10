using System;
using System.Data;
using System.Diagnostics;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;
using System.Text;
using Argix.Data;

namespace Argix.Tools {
	//
	public class Scanner: TsortNode {
		//Members
		private string mTerminalName="";
		private string mSourceName="";
		private ScannerDS mCubeStatsDS=null;
		private ScannerDS mCubeStatsSummaryDS=null;
		private ScannerDS mCubeDetailsDS=null;
		private TraceLog mTraceLog=null;
		private Properties mProperties=null;
		private int mDaysBack=1;
		private Mediator mMediator=null;
		
		//Constants
		public const int MINDAYS_BACK = 1;
		public const int MAXDAYS_BACK = 365;
		
		//Events
		public event EventHandler CubeStatsChanged=null;
		public event EventHandler CubeStatsSummaryChanged=null;
		public event EventHandler CubeDetailsChanged=null;
		public event EventHandler EventLogChanged=null;
		
		//Interface
		public Scanner(string text, int imageIndex, int selectedImageIndex, ScannerDS.ScannerTableRow scanner, int initialDaysback, Mediator mediator) : base(text, imageIndex, selectedImageIndex) { 
			//Constructor
			try {
				//Configure this terminal from the terminal configuration information
				this.mMediator = mediator;
				if(scanner != null) {
					this.mTerminalName = scanner.TerminalName;
					this.mSourceName = scanner.SourceName;
				}
				this.mProperties = new Properties(this);
				this.mDaysBack = initialDaysback;
				this.mCubeStatsDS = new ScannerDS();
				this.mCubeStatsSummaryDS = new ScannerDS();
				this.mCubeDetailsDS = new ScannerDS();
				this.mTraceLog = new TraceLog(this.mSourceName, initialDaysback, this.mMediator.Connection);
			} 
			catch(Exception ex) { throw ex; }
		}
		public string TerminalName { get { return this.mTerminalName; } }
		public string SourceName { get { return this.mSourceName; } }
		public Properties Properties { get { return this.mProperties; } }
		public ScannerDS CubeStats { get { return this.mCubeStatsDS; } }
		public ScannerDS CubeStatsSummary { get { return this.mCubeStatsSummaryDS; } }
		public ScannerDS CubeDetails { get { return this.mCubeDetailsDS; } }
		public int DaysBack { 
			get {return this.mDaysBack; } 
			set {
				if(value < MINDAYS_BACK)
					throw new ApplicationException("Days back cannot be leass than " + MINDAYS_BACK.ToString() + " day.");
				if(value > MAXDAYS_BACK)
					throw new ApplicationException("Days back cannot be greater than " + MAXDAYS_BACK.ToString() + " day.");
				if(value != this.mDaysBack) {
					this.mDaysBack = this.mTraceLog.DaysBack = value;
					Refresh();
				}
			} 
		} 
		public ScannerDS EventLog { get { return this.mTraceLog.LogEntries; } }
		public Mediator Mediator { get { return this.mMediator; } }
		#region Accessors\Modifiers, ToString(), ToDataSet()
		public DataSet ToDataSet() {
			//Return a dataset containing values for this terminal
			DataSet ds=null;
			try {
				ds = new DataSet();
				ds.Merge(this.mCubeStatsDS);
				ds.Merge(this.mCubeDetailsDS);
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
				builder.Append("Terminal Name=" + this.mTerminalName + "\t");
				builder.Append("Source Name=" + this.mSourceName + "\t");
				builder.Append("\n");
				sThis = builder.ToString();
			} 
			catch(Exception) { }
			return sThis;
		}
		#endregion
		public void RefreshCubeStats() { 
			//Clear existing entries and refresh collection
			try {				
				//Clear cuurent collection and re-populate
				this.mCubeStatsDS.Clear();
				DataSet ds = this.mMediator.FillDataset(App.USP_CUBESTATS, App.TBL_CUBESTATS, new object[]{this.mSourceName, DateTime.Today.AddDays(-this.mDaysBack), DateTime.Now});
                if(ds.Tables[App.TBL_CUBESTATS].Rows.Count > 0) {
                    ScannerDS stats = new ScannerDS();
                    stats.Merge(ds);
                    this.mCubeStatsDS.Merge(stats.CubeStatisticsTable.Select("","DATE DESC, HOUR DESC"));
                }
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.CubeStatsChanged != null) this.CubeStatsChanged(this, EventArgs.Empty); }
		}
		public void RefreshCubeStatsSummary() { 
			//Clear existing entries and refresh collection
			try {				
				//Clear cuurent collection and re-populate
				this.mCubeStatsSummaryDS.Clear();
				DataSet ds = this.mMediator.FillDataset(App.USP_CUBESTATSSUM, App.TBL_CUBESTATSSUM, new object[]{this.mSourceName, DateTime.Today.AddDays(-this.mDaysBack), DateTime.Now});
				if(ds != null)
					this.mCubeStatsSummaryDS.Merge(ds);
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.CubeStatsSummaryChanged != null) this.CubeStatsSummaryChanged(this, EventArgs.Empty); }
		}
		public void RefreshCubeDetails() { 
			//Clear existing entries and refresh collection
			try {				
				//Clear cuurent collection and re-populate
				this.mCubeDetailsDS.Clear();
				DataSet ds = this.mMediator.FillDataset(App.USP_CUBEDETAILS, App.TBL_CUBEDETAILS, new object[]{this.mSourceName, DateTime.Today.AddDays(-this.mDaysBack), DateTime.Now});
                if(ds != null) {
                    ScannerDS _ds = new ScannerDS();
                    _ds.Merge(ds);
                    this.mCubeDetailsDS.Merge(_ds.Tables[App.TBL_CUBEDETAILS].Select("","CubeDate DESC"));
                }
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.CubeDetailsChanged != null) this.CubeDetailsChanged(this, EventArgs.Empty); }
		}
		public void RefreshEventLog() { 
			//Clear existing entries and refresh collection
			try {
				this.mTraceLog.Refresh();
			}
			catch(Exception ex) { throw ex; }
			finally { if(this.EventLogChanged != null) this.EventLogChanged(this, EventArgs.Empty); }
		}
		#region TsortNode implementations: Refresh()
		public override void Refresh() { 
			//Refresh data for this treeview node
			try {
				RefreshCubeStats();
				RefreshCubeStatsSummary();
				RefreshCubeDetails();
				RefreshEventLog();
			}
			catch(Exception) { }
		}
		#endregion
	}
}