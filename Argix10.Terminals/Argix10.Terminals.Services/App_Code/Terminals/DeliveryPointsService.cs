using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Argix.Enterprise;

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class DeliveryPointsService:IDeliveryPointsService {
        //Members

        //Interface
        public DeliveryPointsService() { }

        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(DeliveryPointsGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(DeliveryPointsGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet GetDeliveryPoints(DateTime startDate,DateTime lastUpated) {
            //Update a collection (dataset) of all delivery points
            DataSet points = new DataSet();
            const string CSV_DELIM = ",";
            try {
                //Clear and fetch new data
                DataSet ds = new DeliveryPointsGateway().GetDeliveryPoints(startDate,lastUpated);
                if (ds != null && ds.Tables["DeliveryPointTable"] != null && ds.Tables["DeliveryPointTable"].Rows.Count > 0) {
                    //Create a dataset of containing unique entries
                    DeliveryPointsDataset _ds = new DeliveryPointsDataset();
                    _ds.Merge(ds);
                    DeliveryPointsDataset rds = new DeliveryPointsDataset();
                    rds.Merge(_ds.DeliveryPointTable.Select("","Account ASC"));
                    Hashtable ht = new Hashtable();
                    string acct = "";
                    for(int i=0;i<rds.DeliveryPointTable.Rows.Count;i++) {
                        //Remove duplicate account entries
                        acct = rds.DeliveryPointTable[i].Account;
                        if(ht.ContainsKey(acct))
                            rds.DeliveryPointTable[i].Delete();
                        else
                            ht.Add(acct,null);		//Keep track of keys (unique accounts)
                    }
                    rds.AcceptChanges();

                    //Modify data
                    for(int i=0;i<rds.DeliveryPointTable.Rows.Count;i++) {
                        //Set command as follows:
                        //	A:	OpenDate > lastpDated; U: OpenDate <= lastUpdated
                        DateTime opened = rds.DeliveryPointTable[i].OpenDate;
                        rds.DeliveryPointTable[i].Command = (opened.CompareTo(lastUpated) > 0) ? "A" : "U";

                        //Remove commas from address fields
                        rds.DeliveryPointTable[i].Building = rds.DeliveryPointTable[i].Building.Replace(CSV_DELIM," ");
                        rds.DeliveryPointTable[i].Address = rds.DeliveryPointTable[i].Address.Replace(CSV_DELIM," ");
                        rds.DeliveryPointTable[i].StopComment = rds.DeliveryPointTable[i].StopComment.Replace(CSV_DELIM," ");
                    }
                    rds.AcceptChanges();
                    points.Merge(rds);
                }
            }
            catch (Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(ex.Message),"Service Error"); }
            return points;
        }
        public DateTime GetExportDate() {
            //Get the latest delivery point LastUpdated date from the last export
            DateTime exportDate;
            try {
                exportDate = new DeliveryPointsGateway().GetExportDate();
            }
            catch (Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(ex.Message),"Service Error"); }
            return exportDate;
        }
        //[PrincipalPermission(SecurityAction.Demand,Role = "Roadshow Specialist")]
        public bool UpdateExportDate(DateTime lastUpdated) {
            bool updated = false;
            //Update the latest delivery point LastUpdated date from the last export
            try {
                updated = new DeliveryPointsGateway().UpdateExportDate(lastUpdated);
            }
            catch (Exception ex) { throw new FaultException<TerminalsFault>(new TerminalsFault(ex.Message),"Service Error"); }
            return updated;
        }

        public DataSet GetRoadshowCustomers() {
            //
            DataSet customers = null;
            try {
                customers = new RoadshowGateway().GetDeliveryPointsCustomers();
            }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Service Error"); }
            return customers;
        }
    }
}
