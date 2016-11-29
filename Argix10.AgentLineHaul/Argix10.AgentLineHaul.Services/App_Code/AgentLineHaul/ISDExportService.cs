using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Argix.Enterprise;

namespace Argix.AgentLineHaul {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class ISDExportService: IISDExportService {
        //Members
               
        //Interface
        public ISDExportService() { }
        public ServiceInfo GetServiceInfo(int terminalID) {
            //Get service information
            return new Argix.AppService(new TsortGateway(terminalID).SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(new TsortGateway(terminalID).SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(int terminalID,TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public DataSet GetPickups(int terminalID,DateTime pickupDate) {
            //Get a collection of all pickups for the terminal on the local LAN database
            DataSet pickups = new DataSet();
            try {
                DataSet ds = new TsortGateway(terminalID).GetPickups(pickupDate);
                if(ds != null && ds.Tables["PickupTable"] != null && ds.Tables["PickupTable"].Rows.Count > 0) pickups.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return pickups;
        }
        public DataSet GetSortedItems(int terminalID,string pickupID) {
            //Get sorted items for a pickup
            DataSet items = new DataSet();
            try {
                DataSet ds = new TsortGateway(terminalID).GetSortedItems(pickupID);
                if(ds != null && ds.Tables["SortedItemTable"] != null && ds.Tables["SortedItemTable"].Rows.Count > 0) items.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return items;
        }
        public string GetExportFilename(int terminalID,string counterKey) {
            string filename="";
            try {
                filename = new TsortGateway(terminalID).GetExportFilename(counterKey);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return filename;
        }
        public DataSet GetISDClients(int terminalID,string clientNumber) {
            DataSet clients = new DataSet();
            try {
                DataSet ds = new TsortGateway(terminalID).ReadISDClients(clientNumber);
                if(ds != null && ds.Tables["ClientTable"] != null && ds.Tables["ClientTable"].Rows.Count > 0) clients.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return clients;
        }
        public bool CreateISDClient(int terminalID,ISDClient client) {
            //
            bool created=false;
            try {
                created = new TsortGateway(terminalID).CreateISDClient(client);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return created;
        }
        public bool UpdateISDClient(int terminalID,ISDClient client) {
            //
            bool updated=false;
            try {
                updated = new TsortGateway(terminalID).UpdateISDClient(client);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return updated;
        }
        public bool DeleteISDClient(int terminalID,ISDClient client) {
            //
            bool deleted=false;
            try {
                deleted = new TsortGateway(terminalID).DeleteISDClient(client);
            }
            catch (Exception ex) { throw new FaultException<ISDExportFault>(new ISDExportFault(ex.Message),"Service Error"); }
            return deleted;
        }
    }
}
