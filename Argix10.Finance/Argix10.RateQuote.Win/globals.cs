using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Data;
//using System.Deployment.Application;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using Argix.Finance;
using Argix.Windows;

namespace Argix {
    /// <summary>The main entry point for the application.</summary>
    static class Program {
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Process appInstance = AppServices.RunningInstance("Argix Logistics " + App.Product);
                if(appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.Finance.winLTLRates());
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

	/// <summary>Global application object</summary>
	public class App: AppBase {
		//Members
        private static RateQuoteConfiguration _Config = null;

		//Interface
		static App() { 
			//Class constructor: get application configuration
            try {
                _Config = new RateQuoteConfiguration();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        public static RateQuoteConfiguration Config { get { return _Config; } }
        public static void ShowConfig() {
            new Argix.Support.dlgConfig(_Config).ShowDialog();
        }
        public static void Trace(string message,LogLevel level) {
            //Trace
            //if((int)level >= _Config.TraceLevel) {
                TraceMessage m = new TraceMessage();
                m.Name = "Argix10";
                m.Source = App.Product;
                m.User = Environment.UserName;
                m.Computer = Environment.MachineName;
                m.LogLevel = level;
                m.Message = message;
                FinanceGateway.WriteLogEntry(m);
            //}
        }
        public static string EventLogName { get { return "Argix08"; } }
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
                    Trace(ex.ToString(),level);
            }
            catch(Exception) { }
        }
        public static void CheckVersion() {
            //Check for a version update
            try {
                if(global::Argix.Properties.Settings.Default.LastVersion != App.Version)
                    MessageBox.Show("This is an updated version of " + App.Product + ". Please refer to Help\\Release Notes for release information.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch(Exception) { }
        }
    }

    public class RateQuoteConfiguration {
        //Memnbers
        private UserConfiguration mConfig=null;
        public RateQuoteConfiguration() {
            //Constructor
            this.mConfig = FinanceGateway.GetUserConfiguration(App.Product,new string[] { Environment.UserName,Environment.MachineName });
        }
        [Category("Behavior"),Description("Application trace logging level.")]
        public LogLevel TraceLevel {
            get {
                switch(Convert.ToInt32(LogLevel.None)) {
                    case 0: return LogLevel.None;
                    case 1: return LogLevel.Debug;
                    case 2: return LogLevel.Information;
                    case 3: return LogLevel.Warning;
                    case 4: return LogLevel.Error;
                    default: return LogLevel.Warning;
                }
            }
            set {
                switch(value) {
                    case LogLevel.None: this.mConfig["TraceLevel"] = "0"; break;
                    case LogLevel.Debug: this.mConfig["TraceLevel"] = "1"; break;
                    case LogLevel.Information: this.mConfig["TraceLevel"] = "2"; break;
                    case LogLevel.Warning: this.mConfig["TraceLevel"] = "3"; break;
                    case LogLevel.Error: this.mConfig["TraceLevel"] = "4"; break;
                }
            }
        }
        [Category("Data"),Description("Directory location of quote files.")]
        public string WorkingDirectory { get { return global::Argix.Properties.Settings.Default.WorkingDirectory; } }
    }

    public delegate void StatusEventHandler(object source,StatusEventArgs e);
    public class StatusEventArgs:EventArgs {
        private string _message = "";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }
}