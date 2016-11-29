using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.rgxsqlrpts05;

namespace Argix {
    //
    public partial class Main:Form {
        //
        private string mReportName="/Shipping/Agent Ship Schedule";
        
        //
        public Main() {
            InitializeComponent();
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //
            refresh05();
        }
        private void refresh05() {
            //Refresh subscription status
            try {
                //Create reporting service web client proxy
                ReportingService rs = new ReportingService();
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Request all subscriptions for the specified report
                this.mSubs05.Clear();
                Subscription[] subscriptions = rs.ListSubscriptions(this.mReportName,null);
                if(subscriptions != null) {
                    //Enumerate all subscriptions
                    for(int i=0;i<subscriptions.Length;i++) {
                        //Get subscription properties
                        Subscription sub = subscriptions[i];
                        ExtensionSettings extSettings=null;
                        ActiveState active=null;
                        string desc="",status="",eventType="",matchData="";
                        ParameterValue[] paramValues=null;
                        rs.GetSubscriptionProperties(subscriptions[i].SubscriptionID,out extSettings,out desc,out active,out status,out eventType,out matchData,out paramValues);

                        if(paramValues != null) {
                            this.mSubs05.SubscriptionTable.AddSubscriptionTableRow(false,sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings));
                        }
                    }
                    this.mSubs05.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh subscriptions.",ex); }
        }
        private void refresh08() {
            //Refresh subscription status
            try {
                //Create reporting service web client proxy
                Argix.rgxvmsqlrpt08.ReportingService2010 rs = new Argix.rgxvmsqlrpt08.ReportingService2010();
                rs.Credentials = System.Net.CredentialCache.DefaultCredentials;

                //Request all subscriptions for the specified report
                this.mSubs05.Clear();
                Argix.rgxvmsqlrpt08.Subscription[] subscriptions = rs.ListSubscriptions(this.mReportName);
                if(subscriptions != null) {
                    //Enumerate all subscriptions
                    for(int i=0;i<subscriptions.Length;i++) {
                        //Get subscription properties
                        Argix.rgxvmsqlrpt08.Subscription sub = subscriptions[i];
                        Argix.rgxvmsqlrpt08.ExtensionSettings extSettings=null;
                        Argix.rgxvmsqlrpt08.ActiveState active=null;
                        string desc="",status="",eventType="",matchData="";
                        Argix.rgxvmsqlrpt08.ParameterValue[] paramValues=null;
                        rs.GetSubscriptionProperties(subscriptions[i].SubscriptionID,out extSettings,out desc,out active,out status,out eventType,out matchData,out paramValues);

                        if(paramValues != null) {
                            this.mSubs08.SubscriptionTable.AddSubscriptionTableRow(false,sub.Report,sub.SubscriptionID,desc,eventType,sub.LastExecuted,status,active.ToString(),getExtSettings(extSettings),matchData,getParamValues(paramValues),getSubjectLine(extSettings));
                        }
                    }
                    this.mSubs08.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Failed to refresh subscriptions.",ex); }
        }
        #region Report Services: getSubjectLine(), getExtSettings(), getMatchData(), getParamValues()
        private string getSubjectLine(ExtensionSettings extSettings) {
            //
            string subjectLine="";
            ParameterValueOrFieldReference[] extensionParams = extSettings.ParameterValues;
            if(extensionParams != null) {
                foreach(ParameterValueOrFieldReference extensionParam in extensionParams) {
                    if(((ParameterValue)extensionParam).Name.ToLower() == "subject") {
                        subjectLine = ((ParameterValue)extensionParam).Value;
                        break;
                    }
                }
            }
            return subjectLine;
        }
        private string getExtSettings(ExtensionSettings extSettings) {
            //
            string extensions="";
            for(int i=0;i<extSettings.ParameterValues.Length;i++)
                extensions += ((ParameterValue)extSettings.ParameterValues[i]).Name + ": " + ((ParameterValue)extSettings.ParameterValues[i]).Value + "; ";
            return extensions;
        }
        private string getMatchData(string matchData) {
            //
            //<ScheduleDefinition>
            //	<StartDateTime>2008-08-23T09:00:00-08:00</StartDateTime>
            //	<WeeklyRecurrence>
            //		<WeeksInterval>1</WeeksInterval>
            //		<DaysOfWeek><Monday>true</Monday></DaysOfWeek>
            //	</WeeklyRecurrence>
            //</ScheduleDefinition>
            return matchData;
        }
        private string getParamValues(ParameterValue[] paramValues) {
            //
            string parameters="";
            for(int i=0;i<paramValues.Length;i++)
                parameters += paramValues[i].Name + ": " + paramValues[i].Value + "; ";
            return parameters;
        }
        #endregion

    }
}
