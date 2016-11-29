using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
using Argix.Freight;
using Argix.Windows;

namespace Argix {
    /// <summary>The main entry point for the application.</summary>
    static class Program {
        //Members
        private static string _TerminalCode = null;

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
                    Application.Run(new Argix.Freight.frmMain());
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
        static Dictionary<string,string> GetQueryStringParameters() {
            //Form a dictionary of the query string launch parameters
            Dictionary<string,string> nameValueTable = new Dictionary<string,string>();
            if(ApplicationDeployment.IsNetworkDeployed) {
                if(AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData != null) {
                    string url = AppDomain.CurrentDomain.SetupInformation.ActivationArguments.ActivationData[0];
                    string queryString = (new Uri(url)).Query;
                    queryString = queryString.Replace("?","");
                    string[] nameValuePairs = queryString.Split('&');
                    foreach(string pair in nameValuePairs) {
                        string[] vars = pair.Split('=');
                        if(!nameValueTable.ContainsKey(vars[0])) {
                            nameValueTable.Add(vars[0],vars[1]);
                        }
                    }
                }
            }
            return (nameValueTable);
        }
        public static string TerminalCode {
            get {
                if(_TerminalCode == null) {
                    Dictionary<string,string> d = GetQueryStringParameters();
                    if(d.Count > 0) {
                        string code = "";
                        _TerminalCode = d.TryGetValue("terminal",out code) ? code : "";
                    }
                    else {
                        Argix.Windows.dlgInputBox ib = new dlgInputBox("Enter a terminal code (i.e. JA)","","Terminal Code");
                        ib.StartPosition = FormStartPosition.CenterScreen;
                        ib.TopMost = true;
                        if (ib.ShowDialog() == DialogResult.OK) _TerminalCode = ib.Value; else _TerminalCode = "";
                    }
                }
                return _TerminalCode;

            }
        }
    }

    //Global application object
    public class App:AppBase {
		//Members
        private static TsortConfiguration _Config = null;

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
                _Config = new TsortConfiguration();
            }
            catch (Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        public static TsortConfiguration Config { get { return _Config; } }
        public static void ShowConfig() {
            new Argix.Support.dlgConfig(_Config).ShowDialog();
        }
        public static void Trace(string message,LogLevel level) {
            //Trace
            if (level >= _Config.TraceLevel) {
                TraceMessage m = new TraceMessage();
                m.Name = "Argix10";
                m.Source = App.Product;
                m.User = Environment.UserName;
                m.Computer = Environment.MachineName;
                m.LogLevel = level;
                m.Message = message;
                TsortGateway.WriteLogEntry(m);
            }
        }
        public static void ReportError(Exception ex) { ReportError(ex,true,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage) { ReportError(ex,displayMessage,LogLevel.None); }
        public static void ReportError(Exception ex,bool displayMessage,LogLevel level) {
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if (ex.InnerException != null) {
                    if ((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                if (displayMessage)
                    MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
                if (level != LogLevel.None)
                    Trace(ex.ToString(),level);
            }
            catch (Exception) { }
        }
        public static void CheckVersion() {
            //Check for a version update
            try {
                if (global::Argix.Properties.Settings.Default.LastVersion != App.Version)
                    MessageBox.Show("This is an updated version of " + App.Product + ". Please refer to Help\\Release Notes for release information.",App.Product,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
            catch (Exception) { }
        }
    }

    public class TsortConfiguration {
        //Memnbers
        private UserConfiguration mConfig = null;
        public TsortConfiguration() {
            //Constructor
            this.mConfig = TsortGateway.GetUserConfiguration(App.Product,new string[] { Environment.UserName,Environment.MachineName });
        }
        [Category("Behavior"),Description("Application trace logging level.")]
        public LogLevel TraceLevel {
            get {
                switch (Convert.ToInt32(this.mConfig["TraceLevel"])) {
                    case 0: return LogLevel.None;
                    case 1: return LogLevel.Debug;
                    case 2: return LogLevel.Information;
                    case 3: return LogLevel.Warning;
                    case 4: return LogLevel.Error;
                    default: return LogLevel.Warning;
                }
            }
            set {
                switch (value) {
                    case LogLevel.None: this.mConfig["TraceLevel"] = "0"; break;
                    case LogLevel.Debug: this.mConfig["TraceLevel"] = "1"; break;
                    case LogLevel.Information: this.mConfig["TraceLevel"] = "2"; break;
                    case LogLevel.Warning: this.mConfig["TraceLevel"] = "3"; break;
                    case LogLevel.Error: this.mConfig["TraceLevel"] = "4"; break;
                }
            }
        }
    }
    
    public enum DialogActionEnum { DialogActionAssign = 0, DialogActionUnassign = 1, DialogActionUnassignAny = 2 }
}