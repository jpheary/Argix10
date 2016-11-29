using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;
using Argix;
using Argix.Windows;

namespace Argix {
    //The main entry point for the application
    static class Program {
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Process appInstance = AppServices.RunningInstance("Argix Direct " + App.Product);
                if(appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.Tools.frmMain());
                }
                else {
                    MessageBox.Show("Another instance of this application is already running.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
                    AppServices.ShowWindow(appInstance.MainWindowHandle,1);
                    AppServices.SetForegroundWindow(appInstance.MainWindowHandle);
                }
            }
            catch(Exception ex) {
                MessageBox.Show("FATAL ERROR\n\n" + ex.ToString() + "\n\n Application will be closed. Please contact the IT department for help.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                Application.Exit();
            }
        }
    }
    
    //Global application object
	public class App: AppBase {
		//Members
		
		//Constants
		public const string USP_LOCALTERMINAL = "uspEnterpriseCurrentTerminalGet", TBL_LOCALTERMINAL = "LocalTerminalTable";
		public const string USP_CUBESTATS = "uspToolsCubeStatsGetList", TBL_CUBESTATS = "CubeStatisticsTable";
		public const string USP_CUBESTATSSUM = "uspToolsCubeStatsSummaryGetList", TBL_CUBESTATSSUM = "CubeStatisticsSummaryTable";
		public const string USP_CUBEDETAILS = "uspToolsCubeDetailsGetList", TBL_CUBEDETAILS = "CubeDetailsTable";
		public const string USP_SCANDIRECTGET = "uspToolsCubeScanDirectGet", TBL_SCANDIRECT = "ScanDetailsTable";
		public const string USP_SCANINDIRECTGET = "uspToolsCubeScanIndirectGet", TBL_SCANINDIRECT = "ScanDetailsTable";
		public const string USP_SCANDETAILGET = "uspToolsCubeScanDetailGet", TBL_SCANDETAIL = "ScanDetailsTable";
		public const string USP_TRACELOG_GETLIST = "uspToolsCubeTraceLogGet", TBL_TRACELOG_GETLIST = "ArgixLogTable";
		public const string USP_TRACELOG_DELETE = "uspToolsTraceLogDelete";
		
		public const int ICON_ARGIX = 14;
		public const int ICON_TERMINAL = 12;
		public const int ICON_SCANNER = 13;
		
		//Interface
		static App() { }
		private App() { }
		public static string EventLogName { get { return "CubeAnalyst"; } }
		public static string RegistryKey { get { return "CubeAnalyst"; } }
        public static void ReportError(Exception ex) { ReportError(ex,true,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage) { ReportError(ex,displayMessage,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage,LogLevel level) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage)
                    MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(level != LogLevel.None)
                    ArgixTrace.WriteLine(new TraceMessage(ex.ToString(),App.EventLogName,level));
            }
            catch(Exception) { }
        }
    }
	
	namespace Tools {
		//TsortNode creates objects that can reside in a TreeView control (i.e. TreeNode)
		//and also provide custom application functionality when selected
		public abstract class TsortNode: TreeNode {
			//Members
			protected TreeNode[] mChildNodes=null;
			//Constants
			//Events
			//Interface
			public TsortNode(string text, int imageIndex, int selectedImageIndex) {
				//Constructor
				try {
					//Set members and base node members
					this.Text = text.Trim();
					this.ImageIndex = imageIndex;
					this.SelectedImageIndex = selectedImageIndex;
				}
				catch(Exception ex) { throw new ApplicationException("Unexpected error while creating (abstract) Tsort Node instance.", ex); }
			}
			public void ExpandNode() { 
				//Load [visible] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(TsortNode node in this.mChildNodes) 
						node.LoadChildNodes();
				}
			}
			public void CollapseNode() { 
				//Unload [hidden] child nodes for each child
				if(this.mChildNodes!=null) {
					foreach(TsortNode node in this.mChildNodes) 
						node.UnloadChildNodes();
				}
			}
			public virtual void LoadChildNodes() { return; }
			public virtual void UnloadChildNodes() { base.Nodes.Clear(); this.mChildNodes = null; }
			public abstract void Refresh();
		}

        public class Properties {
            //Members
            private Scanner mScanner=null;

            //Constants
            //Events		
            //Interface
            public Properties(Scanner scanner) {
                //Constructor
                this.mScanner = scanner;
            }

            [Category("General"),Description("Days back from today.")]
            public int DaysBack { get { return this.mScanner.DaysBack; } set { this.mScanner.DaysBack = value; } }
        }
    }
	
	//Event support	
	public delegate void StatusEventHandler(object source, StatusEventArgs e);
	public class StatusEventArgs : EventArgs {
		private string _message;
		public StatusEventArgs(string message) {
			this._message = message;
		}
		public string Message { get { return this._message; } set { this._message = value; } }
	}

}