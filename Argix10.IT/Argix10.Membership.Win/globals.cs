using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Deployment.Application;
using System.Diagnostics;
using System.Data;
using System.Reflection;
using System.Windows.Forms;
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
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Argix.frmMain());
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
    }

    //Global application object
    public class App:AppBase {
		//Members

        //Interface
        static App() {
            //Class constructor: get application configuration
            try {
            }
            catch (Exception ex) { ReportError(ex); Application.Exit(); }
        }
        private App() { }
        public static void ReportError(Exception ex) { 
            //Report an exception to the user
            try {
                string src = (ex.Source != null) ? ex.Source + "-\n" : "";
                string msg = src + ex.Message;
                if (ex.InnerException != null) {
                    if ((ex.InnerException.Source != null)) src = ex.InnerException.Source + "-\n";
                    msg = src + ex.Message + "\n\n NOTE: " + ex.InnerException.Message;
                }
                MessageBox.Show(msg,App.Product,MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            catch (Exception) { }
        }
    }
}