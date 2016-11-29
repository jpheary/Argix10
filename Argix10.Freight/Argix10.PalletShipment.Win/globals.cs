using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Application;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.ServiceModel;
using System.Windows.Forms;
using Argix.Freight;
using Argix.Windows;

namespace Argix {
    /// <summary>The main entry point for the application.</summary>
    static class Program {
        //Members

        //Interface
        [STAThread]
        static void Main() {
            //Application entry point
            try {
                //Start app
                Process appInstance = AppServices.RunningInstance("Argix Logistics " + App.Product);
                if(appInstance == null) {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Argix.Freight.winMain());
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
        private static ApplicationConfiguration _Config = null;

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                _Config = new ApplicationConfiguration();
            }
            catch(Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        public static ApplicationConfiguration Config { get { return _Config; } }
        public static void ShowConfig() {
            new Argix.Support.dlgConfig(_Config).ShowDialog();
        }
        public static void Trace(string message,LogLevel level) {
            //Trace
            if(level >= _Config.TraceLevel) {
                TraceMessage m = new TraceMessage();
                m.Name = "Argix10";
                m.Source = App.Product;
                m.User = Environment.UserName;
                m.Computer = Environment.MachineName;
                m.LogLevel = level;
                m.Message = message;
                FreightGateway.WriteLogEntry(m);
            }
        }
        public static void ReportError(Exception ex) { ReportError(ex,false,LogLevel.Information); }
        public static void ReportError(Exception ex, bool displayMessage) { ReportError(ex, displayMessage, LogLevel.Information); }
        public static void ReportError(Exception ex,bool displayMessage,LogLevel level) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if(ex.InnerException != null) {
                    if((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if(displayMessage) MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if(level != LogLevel.None) Trace(ex.ToString(),level);
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

    public class ApplicationConfiguration {
        //Members
        private UserConfiguration mConfig=null;
        public ApplicationConfiguration() {
            //Constructor
            this.mConfig = FreightGateway.GetUserConfiguration(App.Product,new string[] { Environment.UserName,Environment.MachineName });
        }
        [Category("Data"), Description("Maximum pallet insured value.")]
        public decimal InsuranceMax { get { return global::Argix.Properties.Settings.Default.InsuranceMax; } }
        [Category("Data"), Description("Maximum pallets per shipment.")]
        public int PalletsMax { get { return global::Argix.Properties.Settings.Default.PalletsMax; } }
        [Category("Behavior"), Description("The number of milliseconds (i.e. 30000 msec = 30 sec) between automatic view refreshes.")]
        public int AutoRefreshTimer { get { return global::Argix.Properties.Settings.Default.AutoRefreshTimer; } }
        [Category("Behavior"), Description("Gets or sets the location of a Temp folder for file attachments.")]
        public string TempFolder { get { return global::Argix.Properties.Settings.Default.TempFolder; } set { global::Argix.Properties.Settings.Default.TempFolder = value; } }
        [Category("Data"), Description("Maximum pallet weight when a lift gate is required.")]
        public decimal WeightLiftGateMax { get { return global::Argix.Properties.Settings.Default.WeightMax; } set { global::Argix.Properties.Settings.Default.WeightMax = value; } }
        [Category("Data"), Description("Maximum pallet weight.")]
        public decimal WeightMax { get { return global::Argix.Properties.Settings.Default.WeightMax; } set { global::Argix.Properties.Settings.Default.WeightMax = value; } }
        [Category("Behavior"), Description("Application trace logging level.")]
        public LogLevel TraceLevel {
            get {
                switch(Convert.ToInt32(this.mConfig["TraceLevel"])) {
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
    }

    public delegate void StatusEventHandler(object source, StatusEventArgs e);
    public class StatusEventArgs : EventArgs {
        private string _message = "";
        public StatusEventArgs(string message) { this._message = message; }
        public string Message { get { return this._message; } set { this._message = value; } }
    }
}
