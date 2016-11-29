using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;


namespace Argix.Enterprise {
    //
    public class EnterpriseGateway {
        //Members
        public const string SQL_CONNID = "Enterprise";

        private const string USP_AGENTS = "uspDispatchAgentGetList",TBL_AGENTS = "AgentTable";
        private const string USP_CARRIERS = "uspDispatchCarrierGetList",TBL_CARRIERS = "CarrierTable";
        private const string USP_CLIENTS = "uspDispatchClientGetlist",TBL_CLIENTS = "ClientTable";
        private const string USP_DRIVERS = "uspDispatchDriverGetList",TBL_DRIVERS = "DriverTable";
        private const string USP_LOCATIONS = "uspDispatchLocationGetList",TBL_LOCATIONS = "LocationTable";
        private const string USP_SORTCENTERS = "uspDispatchSortCenterGetList",TBL_SORTCENTERS = "TerminalTable";
        private const string USP_TERMINALS = "uspDispatchTerminalGetList",TBL_TERMINALS = "TerminalTable";
        private const string USP_VENDORS = "uspDispatchVendorGetList",TBL_VENDORS = "VendorTable";

        private const string USP_ARGIXTERMINALS = "uspTerminalsGetList";
        private const string USP_TDSINFO = "uspDispatchTDSInfo", TBL_TDSINFO = "TDSTable";

        //Interface
        public EnterpriseGateway() { }

        public DataSet GetClients() {
            //Returns a list of clients
            DataSet clients = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if (ds != null && ds.Tables[TBL_CLIENTS].Rows.Count > 0) clients.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public DataSet GetAgents() {
            //Returns a list of agents
            DataSet agents = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_AGENTS,TBL_AGENTS,new object[] { });
                if (ds != null && ds.Tables[TBL_AGENTS].Rows.Count > 0) agents.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return agents;
        }
        public DataSet GetVendors() {
            //Returns a list of vendors
            DataSet vendors = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_VENDORS,TBL_VENDORS,new object[] { });
                if (ds != null && ds.Tables[TBL_VENDORS].Rows.Count > 0) vendors.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return vendors;
        }
        public DataSet GetCarriers() {
            //Returns a list of carriers
            DataSet carriers = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CARRIERS,TBL_CARRIERS,new object[] { });
                if (ds != null && ds.Tables[TBL_CARRIERS].Rows.Count > 0) carriers.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return carriers;
        }
        public DataSet GetDrivers() {
            //Returns a list of drivers
            DataSet drivers = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DRIVERS,TBL_DRIVERS,new object[] { });
                if (ds != null && ds.Tables[TBL_DRIVERS].Rows.Count > 0) drivers.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return drivers;
        }
        public DataSet GetLocations() {
            //Returns a list of locations
            DataSet locations = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_LOCATIONS,TBL_LOCATIONS,new object[] { });
                if (ds != null && ds.Tables[TBL_LOCATIONS].Rows.Count > 0) locations.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return locations;
        }
        public DataSet GetTerminals() {
            //Returns a list of terminals
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TERMINALS,TBL_TERMINALS,new object[] { });
                if (ds != null && ds.Tables[TBL_TERMINALS].Rows.Count > 0) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return terminals;
        }
        public DataSet GetSortCenters() {
            //Returns a list of sort centers
            DataSet sortCenters = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_SORTCENTERS,TBL_SORTCENTERS,new object[] { });
                if (ds != null && ds.Tables[TBL_SORTCENTERS].Rows.Count > 0) sortCenters.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return sortCenters;
        }

        public DataSet GetArgixTerminals() {
            //Returns a list of Argix terminals
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ARGIXTERMINALS,TBL_TERMINALS,new object[] { });
                if (ds != null && ds.Tables[TBL_TERMINALS].Rows.Count > 0) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return terminals;
        }
        public DataSet GetTDSInfo(string clientNumber, DateTime start, DateTime end, string vendorName) {
            //Returns a list of Argix terminals
            DataSet tds = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_TDSINFO, TBL_TDSINFO,
                    new object[] { (clientNumber.Trim().Length > 0 ? clientNumber : null), start, end, (vendorName.Trim().Length > 0 ? vendorName : null) });
                if(ds != null && ds.Tables[TBL_TDSINFO].Rows.Count > 0) tds.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return tds;
        }
    }
}
