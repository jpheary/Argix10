using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;

namespace Argix.Enterprise {
	//
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    public class EnterpriseGateway {
        //Members
        public const string SQL_CONNID = "Enterprise";

        public const string USP_TERMINALS = "uspCRMTerminalsGetList",TBL_TERMINALS = "TerminalTable";
        public const string USP_COMPANIES = "uspCRMCompanyGetList",TBL_COMPANIES = "CompanyTable";
        public const string USP_REGIONS_DISTRICTS = "uspCRMRegionDistrictGetList",TBL_REGIONS = "LocationTable",TBL_DISTRICTS = "LocationTable";
        public const string USP_AGENTS_BYCLIENT = "uspCRMAgentGetListForClient", TBL_AGENTS = "AgentTable";
        public const string USP_AGENTS = "uspCRMAgentGetList";
        public const string USP_AGENT_TERMINALS = "uspCRMAgentTerminalGetList";
        public const string USP_STORE = "uspCRMStoreGet",TBL_STORE = "StoreTable";

        public const string USP_DELIVERY = "uspCRMDeliveryGetList ",TBL_DELIVERY = "DeliveryTable";
        
        //Interface
        public EnterpriseGateway() { }

        public DataSet GetTerminals() {
            //Get a list of Argix terminals
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TERMINALS,TBL_TERMINALS,new object[] { });
                if(ds != null && ds.Tables[TBL_TERMINALS].Rows.Count > 0) terminals.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return terminals;
        }
        public DataSet GetCompanies() {
            //Get a list of all companies
            DataSet companies = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_COMPANIES,TBL_COMPANIES,new object[] { });
                if(ds.Tables[TBL_COMPANIES].Rows.Count > 0) companies.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return companies;
        }
        public DataSet GetRegionsDistricts(string clientNumber) {
            //Get a list of regions and districts for the specified client number
            DataSet districts = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_REGIONS_DISTRICTS,TBL_DISTRICTS,new object[] { clientNumber });
                if(ds.Tables[TBL_DISTRICTS].Rows.Count > 0) districts.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return districts;
        }
        public DataSet GetAgents(string clientNumber) {
            //Get a list of agents for the specified client
            DataSet agents = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_AGENTS_BYCLIENT,TBL_AGENTS,new object[] { clientNumber });
                if(ds.Tables[TBL_AGENTS].Rows.Count > 0) agents.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return agents;
        }
        public DataSet GetAgents() {
            //Get a list of all agents
            DataSet agents = new DataSet();
            try  {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_AGENTS, TBL_AGENTS, new object[] { });
                if (ds.Tables[TBL_AGENTS].Rows.Count > 0) agents.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return agents;
        }
        public DataSet GetAgentTerminals(string agentNumber) {
            //Get a list of agent terminals for the specified agent (null returns all agents)
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_AGENT_TERMINALS,TBL_AGENTS,new object[] { agentNumber });
                if (ds.Tables[TBL_AGENTS].Rows.Count > 0) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return terminals;
        }
        public DataSet GetStoreDetail(int companyID, int storeNumber)  {
            //Get a list of store locations
            DataSet stores = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_STORE,TBL_STORE,new object[] { companyID,storeNumber,null });
                if(ds.Tables[TBL_STORE].Rows.Count > 0) stores.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return stores;
        }
        public DataSet GetStoreDetail(int companyID,string subStore) {
            //Get a list of store locations
            DataSet stores = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_STORE,TBL_STORE,new object[] { companyID,null,subStore });
                if(ds.Tables[TBL_STORE].Rows.Count > 0) stores.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return stores;
        }
        public DataSet GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            DataSet deliveries = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to });
                if(ds.Tables[TBL_DELIVERY].Rows.Count > 0) deliveries.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading deliveries.",ex); }
            return deliveries;
        }
    }
}