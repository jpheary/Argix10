using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.Caching;
using System.ServiceModel;
using System.Threading;
using Argix.Windows;

namespace Argix.Customers {
	//
	public class CRMGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        private static IssueCache _Cache=null;

        private static System.Windows.Forms.Timer RefreshTimer=null;
        private static BackgroundWorker RefreshWorker=null;
        public static DateTime LastIssueUpdateTime=DateTime.Now;
        
        //Interface
        static CRMGateway() { 
            //
            CRMServiceClient client = new CRMServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
            //_Cache = new IssueCache(DateTime.Today.AddDays(-App.Config.IssueDaysBack));

            RefreshTimer = new System.Windows.Forms.Timer();
            RefreshTimer.Interval = 15000;
            RefreshTimer.Tick += new EventHandler(OnTick);
            RefreshWorker = new BackgroundWorker();
            RefreshWorker.DoWork += new DoWorkEventHandler(OnDoWork);
        }
        private CRMGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }
        public static void ResetCache() { _Cache = null; }

        public static ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            ServiceInfo terminal=null;
            CRMServiceClient client = null; 
            try {
                client = new CRMServiceClient();
                terminal = client.GetServiceInfo();
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminal;
        }
        public static UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the operating enterprise terminal
            UserConfiguration config=null;
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                config = client.GetUserConfiguration(application,usernames);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public static void WriteLogEntry(TraceMessage m) {
            //Get the operating enterprise terminal
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                client.WriteLogEntry(m);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public static CRMDataset GetIssues() {
            //Get issues
            CRMDataset issues=new CRMDataset();
            CRMServiceClient client = null;
            try {
                if(_Cache == null) _Cache = new IssueCache(DateTime.Today.AddDays(-App.Config.IssueDaysBack));
                client = new CRMServiceClient();
                DataSet ds = client.GetIssuesForDate(_Cache.LastUpdated);
                client.Close();
                System.Diagnostics.Debug.WriteLine("PAYLOAD: fromDate=" + _Cache.LastUpdated.ToString("MM/dd/yyyy HH:mm:ss") + "; bytes=" + ds.GetXml().Length);

                CRMDataset _issues=new CRMDataset();
                if(ds.Tables["IssueTable"] != null && ds.Tables["IssueTable"].Rows.Count > 0) {
                    _issues.Merge(ds);
                    _Cache.UpdateCache(_issues);
                    issues.Merge(_Cache.Issues);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public static CRMDataset SearchIssues(string searchText) {
            //Get issues
            CRMDataset issues=new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.SearchIssues(searchText);
                client.Close();
                if(ds.Tables["IssueTable"] != null && ds.Tables["IssueTable"].Rows.Count > 0) issues.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }
        public static CRMDataset SearchIssuesAdvanced(object[] criteria) {
            //Get issues
            CRMDataset issues=new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.SearchIssuesAdvanced(criteria);
                client.Close();
                if(ds.Tables["IssueTable"] != null && ds.Tables["IssueTable"].Rows.Count > 0) issues.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issues;
        }

        public static Issue GetIssue(long issueID) {
            //Get issues
            Issue issue=null;
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                issue = client.GetIssue(issueID);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issue;
        }
        public static byte[] GetAttachment(int id) {
            //Get attachments for the specified issue
            byte[] attachment=null;
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                attachment = client.GetAttachment(id);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return attachment;
        }

        public static long CreateIssue(Issue issue) {
            //
            long id=0;
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                id = client.CreateIssue(issue);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return id;
        }
        public static bool AddAction(Action action) {
            //Add an action to an existing issue
            bool res=false;
            CRMServiceClient client = null;
            try {
                //Call service
                client = new CRMServiceClient();
                res = client.AddAction(action);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return res;
        }
        public static bool AddAttachment(string filename,byte[] bytes,long actionID) {
            //
            bool res=false;
            CRMServiceClient client = null;
            try {
                //Create DTO and call service
                Attachment attachment = new Attachment();
                attachment.Filename = filename;
                attachment.File = bytes;
                attachment.ActionID = actionID;

                client = new CRMServiceClient();
                res = client.AddAttachment(attachment);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return res;
        }

        public static CRMDataset GetIssueCategories() {
            //Issue type category
            CRMDataset categorys=null;
            try {
                ObjectCache cache = MemoryCache.Default;
                categorys = cache["categorys"] as CRMDataset;
                if(categorys == null) {
                    categorys = new CRMDataset();
                    CRMDataset issueTypes = GetIssueTypes("");
                    Hashtable groups = new Hashtable();
                    for(int i=0;i<issueTypes.IssueTypeTable.Rows.Count;i++) {
                        if(!groups.ContainsKey(issueTypes.IssueTypeTable[i].Category)) {
                            groups.Add(issueTypes.IssueTypeTable[i].Category,issueTypes.IssueTypeTable[i].Category);
                            categorys.IssueTypeTable.AddIssueTypeTableRow(0,"",issueTypes.IssueTypeTable[i].Category,"");
                        }
                    }
                    categorys.AcceptChanges();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("categorys",categorys,policy);
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return categorys;
        }
        public static CRMDataset GetIssueTypes(string issueCategory) {
            //Issue types- all or filtered by category
            CRMDataset issueTypes=null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                issueTypes = cache["issueTypes" + issueCategory] as CRMDataset;
                if(issueTypes == null) {
                    issueTypes = new CRMDataset();
                    client = new CRMServiceClient();
                    DataSet ds = client.GetIssueTypes(issueCategory);
                    client.Close();
                    if(ds.Tables["IssueTypeTable"] != null && ds.Tables["IssueTypeTable"].Rows.Count > 0) issueTypes.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("issueTypes" + issueCategory,issueTypes,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException fe) { throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return issueTypes;
        }
        public static CRMDataset GetActionTypes(long issueID) {
            //Action types for an issue (state driven)
            CRMDataset actionTypes = new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetActionTypes(issueID);
                client.Close();
                if(ds.Tables["ActionTypeTable"] != null && ds.Tables["ActionTypeTable"].Rows.Count > 0) actionTypes.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<CustomersFault> cfe) { throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return actionTypes;
        }

        public static CRMDataset GetCompanies() {
            //Companies; cache companies
            CRMDataset companies=null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                companies = cache["companies"] as CRMDataset;
                if(companies == null) {
                    companies = new CRMDataset();
                    client = new CRMServiceClient();
                    DataSet ds = client.GetCompanies();
                    client.Close();
                    if(ds.Tables["CompanyTable"] != null && ds.Tables["CompanyTable"].Rows.Count > 0) {
                        CRMDataset _ds = new CRMDataset();
                        _ds.CompanyTable.AddCompanyTableRow(0,"All","000","",1);
                        _ds.Merge(ds.Tables["CompanyTable"].Select("IsActive = 1","CompanyName ASC"));
                        companies.Merge(_ds);
                    }

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("companies",companies,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return companies;
        }
        public static CRMDataset GetRegions(string clientNumber) {
            //Get a list of regional locations for the specified client; convert schema field names to generic Location, LocationName
            CRMDataset regions = null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                regions = cache["regions" + clientNumber] as CRMDataset;
                if(regions == null) {
                    regions = new CRMDataset();
                    regions.LocationTable.AddLocationTableRow("All","All");
                    if(clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                    if(clientNumber == "000") clientNumber = null;

                    client = new CRMServiceClient();
                    DataSet ds = client.GetRegionsDistricts(clientNumber);
                    client.Close();

                    //Filter out duplicate regions since resultset is district-region (child-parent)
                    System.Collections.Hashtable table = new System.Collections.Hashtable();
                    if(ds.Tables["LocationTable"] != null) {
                        for(int i = 0;i < ds.Tables["LocationTable"].Rows.Count;i++) {
                            string location = ds.Tables["LocationTable"].Rows[i]["Region"].ToString().Trim();
                            string locationName = ds.Tables["LocationTable"].Rows[i]["RegionName"].ToString().Trim();
                            if(!table.ContainsKey(location)) {
                                table.Add(location,locationName);
                                regions.LocationTable.AddLocationTableRow(location,locationName);
                            }
                        }
                    }
                    regions.AcceptChanges();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("regions" + clientNumber,regions,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return regions;
        }
        public static CRMDataset GetDistricts(string clientNumber) {
            //Get a list of client districts; convert schema field names to generic Location, LocationName
            CRMDataset districts = null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                districts = cache["districts" + clientNumber] as CRMDataset;
                if(districts == null) {
                    districts = new CRMDataset();
                    districts.LocationTable.AddLocationTableRow("All","All");
                    if(clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                    if(clientNumber == "000") clientNumber = null;

                    client = new CRMServiceClient();
                    DataSet ds = client.GetRegionsDistricts(clientNumber);
                    client.Close();

                    if(ds.Tables["LocationTable"] != null) {
                        for(int i = 0;i < ds.Tables["LocationTable"].Rows.Count;i++) {
                            string location = ds.Tables["LocationTable"].Rows[i]["District"].ToString().Trim();
                            string locationName = ds.Tables["LocationTable"].Rows[i]["DistrictName"].ToString().Trim();
                            districts.LocationTable.AddLocationTableRow(location,locationName);
                        }
                    }
                    districts.AcceptChanges();

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("districts" + clientNumber,districts,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return districts;
        }
        public static CRMDataset GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            CRMDataset agents = null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                agents = cache["agents" + clientNumber] as CRMDataset;
                if(agents == null) {
                    agents = new CRMDataset();
                    agents.AgentTable.AddAgentTableRow("All","","All","","","","","",0,"","","","","","","","","","All");
                    if(clientNumber.Length > 3) clientNumber = clientNumber.Substring(clientNumber.Length - 3,3);
                    if(clientNumber == "000") clientNumber = null;

                    client = new CRMServiceClient();
                    DataSet ds = client.GetAgentsByClient(clientNumber);
                    client.Close();
                    if(ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) {
                        CRMDataset _ds = new CRMDataset();
                        _ds.Merge(ds);
                        for(int i = 0;i < _ds.AgentTable.Rows.Count;i++) {
                            _ds.AgentTable.Rows[i]["AgentSummary"] = (!_ds.AgentTable.Rows[i].IsNull("MainZone") ? _ds.AgentTable.Rows[i]["MainZone"].ToString().PadLeft(2,' ') : "  ") + " - " +
                                                                 (!_ds.AgentTable.Rows[i].IsNull("AgentNumber") ? _ds.AgentTable.Rows[i]["AgentNumber"].ToString() : "    ") + " - " +
                                                                 (!_ds.AgentTable.Rows[i].IsNull("AgentName") ? _ds.AgentTable.Rows[i]["AgentName"].ToString().Trim() : "");
                        }
                        agents.Merge(_ds.AgentTable.Select("","MainZone ASC"));
                        agents.AgentTable.AcceptChanges();
                    }

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("agents" + clientNumber,agents,policy);
                }
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public static CRMDataset GetAgents() {
            //Get a list of all agents
            CRMDataset agents = null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                agents = cache["agents"] as CRMDataset;
                if (agents == null) {
                    agents = new CRMDataset();

                    client = new CRMServiceClient();
                    DataSet ds = client.GetAgents();
                    client.Close();
                    if (ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) agents.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("agents",agents,policy);
                }
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agents;
        }
        public static CRMDataset GetAgentTerminals(string agentNumber) {
            //Get a list of agent terminals for the specified agent
            CRMDataset terminals = null;
            CRMServiceClient client = null;
            try {
                ObjectCache cache = MemoryCache.Default;
                CRMDataset allTerminals = cache["agentterminals"] as CRMDataset;
                if (allTerminals == null) {
                    allTerminals = new CRMDataset();
                    client = new CRMServiceClient();
                    DataSet ds = client.GetAgentTerminals(null);
                    client.Close();
                    if (ds.Tables["AgentTable"] != null && ds.Tables["AgentTable"].Rows.Count > 0) allTerminals.Merge(ds);

                    DateTimeOffset policy = new DateTimeOffset(DateTime.Now.AddMinutes(1440));
                    cache.Set("agentterminals",allTerminals,policy);
                }

                terminals = new CRMDataset();
                if(allTerminals.AgentTable.Rows.Count > 0)
                    terminals.Merge(allTerminals.AgentTable.Select("AgentNumber = '" + agentNumber + "' OR AgentParentNumber = '" + agentNumber + "'","AgentName ASC"));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return terminals;
        }
        public static CRMDataset GetAgentTerminalDetail(string agentNumber) {
            //Get details of an agent terminal
            CRMDataset agent = new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                CRMDataset agents = GetAgentTerminals(agentNumber);
                client.Close();
                agent.Merge(agents.AgentTable.Select("AgentNumber = '" + agentNumber + "'",""));
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return agent;
        }
        public static CRMDataset GetStoreDetail(int companyID,int storeNumber) {
            //Get a list of store locations
            CRMDataset stores = new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetStoreDetail(companyID,storeNumber);
                client.Close();
                stores.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return stores;
        }
        public static CRMDataset GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            CRMDataset stores = new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetSubStoreDetail(companyID,subStore);
                client.Close();
                stores.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return stores;
        }

        public static CRMDataset GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            CRMDataset deliveries = new CRMDataset();
            CRMServiceClient client = null;
            try {
                client = new CRMServiceClient();
                DataSet ds = client.GetDeliveries(companyID,storeNumber,from,to);
                client.Close();
                deliveries.Merge(ds);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return deliveries;
        }

        #region Auto Refresh Services: StartAutoRefresh(), StopAutoRefresh(), OnTick(), OnDoWork()
        public static void StartAutoRefresh(frmMain postback) {
            RefreshWorker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(postback.OnRunWorkerCompleted);
            RefreshWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(postback.OnRunWorkerCompleted);
            RefreshTimer.Start();
        }
        public static void StopAutoRefresh() {
            RefreshTimer.Stop();
        }
        private static void OnTick(object sender,EventArgs e) {
            //Event handler for timer tick event
            try { if(!RefreshWorker.IsBusy) RefreshWorker.RunWorkerAsync(); }
            catch { }
        }
        private static void OnDoWork(object sender,DoWorkEventArgs e) {
            //
            try { e.Result = CRMGateway.GetIssues(); }
            catch { }
        }
        #endregion
    }
}