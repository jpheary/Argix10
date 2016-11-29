using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public class EnterpriseRGateway {
        //Members
        public const string SQL_CONNID = "EnterpriseR";
        
        private const string USP_CUSTOMERS = "uspTrackingCustomerGetList",TBL_CUSTOMERS = "ClientTable";
        private const string USP_CLIENTS = "uspTrackingClientGetList",TBL_CLIENTS = "ClientTable";
        private const string USP_STORESFORSUBSTORE = "uspTrackingStoreForSubStoreGetList",TBL_STORESFORSUBSTORE = "StoreTable";
        private const string USP_STOREBYPICKUP = "uspTrackingPickupCartonsGetListForStore", TBL_STORE = "CartonDetailForStoreTable";
        private const string USP_STOREBYDELIV = "uspTrackingDeliveryCartonsGetListForStore";
        private const string USP_STOREBYPICKUP2 = "uspTrackingPickupCartonsGetListForStore2", USP_STOREBYDELIV2 = "uspTrackingDeliveryCartonsGetListForStore2";
        private const string USP_DETAILBYCARTON = "uspTrackingGetListForCartons2",TBL_TRACKING = "TrackingTable";
        private const string USP_DETAILBYLABEL = "uspTrackingGetListForLabels2";
        private const string USP_DETAILBYPLATE = "uspTrackingGetListForTrackingNumbers";
        private const string USP_DETAILBYPO = "uspTrackingGetListForPO2";
        private const string USP_DETAILBYPRO = "uspTrackingGetListForShipment2";
        private const string USP_DETAILBYBOL = "uspTrackingGetListForStoreBol2";

        public const string USP_USPSCARTONS = "uspTrackingGetListForUSPSCartons",TBL_USPSCARTONS = "TrackingTable";
        public const string USP_DETAILBYLTLSHIPMENT = "uspLTLTrackingGetListForShipment",TBL_LTLSHIPMENT = "TrackingTable";

        //Interface
        public EnterpriseRGateway() { }

        public DataSet GetCustomers() {
            //Returns a list of clients and vendors used to identify a users 'customer' affiliation
            DataSet cutomers = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CUSTOMERS,TBL_CUSTOMERS,new object[] { });
                if(ds != null && ds.Tables[TBL_CUSTOMERS] != null && ds.Tables[TBL_CUSTOMERS].Rows.Count > 0) cutomers.Merge(ds.Tables[TBL_CUSTOMERS].Select("","CompanyName ASC"));
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cutomers;
        }
        public DataSet GetClients(string vendorNumber) {
            //Return a list of clients filtered by vendorNumber (vendorNumber=null returns all Argix clients)
            DataSet clients = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CLIENTS,TBL_CLIENTS,new object[] { vendorNumber });
                if(ds != null && ds.Tables[TBL_CLIENTS] != null && ds.Tables[TBL_CLIENTS].Rows.Count > 0) clients.Merge(ds.Tables[TBL_CLIENTS].Select("","CompanyName ASC"));
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return clients;
        }
        public DataSet GetStoresForSubStore(string subStoreNumber,string clientNumber,string vendorNumber) {
            //Get a list of client\vendor stores for the specified sub-store number
            DataSet stores = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_STORESFORSUBSTORE,TBL_STORESFORSUBSTORE,new object[] { subStoreNumber,clientNumber,vendorNumber });
                if(ds != null && ds.Tables[TBL_STORESFORSUBSTORE] != null && ds.Tables[TBL_STORESFORSUBSTORE].Rows.Count > 0) stores.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return stores;
        }
        public DataSet GetCartonsForStoreByDeliveryDate(string clientNumber, string storeNumber, DateTime fromDate, DateTime toDate, string vendorNumber) {
            //Get a list of cartons (details) for the specified store by pickup or delivery
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_STOREBYDELIV, TBL_STORE, new object[] { clientNumber, storeNumber, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"), vendorNumber });
                if(ds != null && ds.Tables[TBL_STORE] != null && ds.Tables[TBL_STORE].Rows.Count > 0) cartons.Merge(ds.Tables[TBL_STORE]);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsForStoreByDeliveryDate2(string clientNumber, string storeNumber, DateTime fromDate, DateTime toDate, string vendorNumber) {
            //Get a list of cartons (details) for the specified store by pickup or delivery
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_STOREBYDELIV2, TBL_STORE, new object[] { clientNumber, storeNumber, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"), vendorNumber });
                if(ds != null && ds.Tables[TBL_STORE] != null && ds.Tables[TBL_STORE].Rows.Count > 0) cartons.Merge(ds.Tables[TBL_STORE]);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsForStoreByPickupDate(string clientNumber, string storeNumber, DateTime fromDate, DateTime toDate, string vendorNumber) {
            //Get a list of cartons (details) for the specified store by pickup or delivery
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_STOREBYPICKUP, TBL_STORE, new object[] { clientNumber, storeNumber, fromDate.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"), vendorNumber });
                if(ds != null && ds.Tables[TBL_STORE] != null && ds.Tables[TBL_STORE].Rows.Count > 0) cartons.Merge(ds.Tables[TBL_STORE]);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsForStoreByPickupDate2(string clientNumber, string storeNumber, DateTime fromDate, DateTime toDate, string vendorNumber) {
            //Get a list of cartons (details) for the specified store by pickup or delivery
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_STOREBYPICKUP2,TBL_STORE,new object[] { clientNumber,storeNumber,fromDate.ToString("yyyy-MM-dd"),toDate.ToString("yyyy-MM-dd"),vendorNumber });
                if(ds != null && ds.Tables[TBL_STORE] != null && ds.Tables[TBL_STORE].Rows.Count > 0) cartons.Merge(ds.Tables[TBL_STORE]);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsByCartonNumber(string trackingNumbers,string clientNumber,string vendorNumber) {
            //Get a list of cartons (details) for the specified tracking number (carton or label sequence)
            //One or two records are returned for each carton: ScanType=0: 1; ScanType=1: 1, ScanType=3: 2 (ScanTypes 1, 3)
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DETAILBYCARTON,TBL_TRACKING,new object[] { trackingNumbers,clientNumber,vendorNumber });
                if(ds != null && ds.Tables[TBL_TRACKING] != null && ds.Tables[TBL_TRACKING].Rows.Count > 0) cartons.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsByLabelNumber(string trackingNumbers,string clientNumber,string vendorNumber) {
            //Get a list of cartons (details) for the specified tracking number (carton or label sequence)
            //One or two records are returned for each carton: ScanType=0: 1; ScanType=1: 1, ScanType=3: 2 (ScanTypes 1, 3)
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DETAILBYLABEL,TBL_TRACKING,new object[] { trackingNumbers,clientNumber,vendorNumber });
                if(ds != null && ds.Tables[TBL_TRACKING] != null && ds.Tables[TBL_TRACKING].Rows.Count > 0) cartons.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsByPlateNumber(string trackingNumbers,string clientNumber,string vendorNumber) {
            //Get a list of cartons (details) for the specified tracking number (carton or label sequence)
            //One or two records are returned for each carton: ScanType=0: 1; ScanType=1: 1, ScanType=3: 2 (ScanTypes 1, 3)
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DETAILBYPLATE,TBL_TRACKING,new object[] { trackingNumbers,clientNumber,vendorNumber });
                if(ds != null && ds.Tables[TBL_TRACKING] != null && ds.Tables[TBL_TRACKING].Rows.Count > 0) cartons.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsForPO(string clientNumber,string PONumber) {
            //Get a list of cartons (details) for the specified client and PO number
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DETAILBYPO,TBL_TRACKING,new object[] { clientNumber,PONumber });
                if(ds != null && ds.Tables[TBL_TRACKING] != null && ds.Tables[TBL_TRACKING].Rows.Count > 0) cartons.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsForPRO(string clientNumber,string shipmentNumber) {
            //Get a list of cartons (details) for the specified client and PO number
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DETAILBYPRO,TBL_TRACKING,new object[] { clientNumber,shipmentNumber });
                if(ds != null && ds.Tables[TBL_TRACKING] != null && ds.Tables[TBL_TRACKING].Rows.Count > 0) cartons.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }
        public DataSet GetCartonsForBOL(string clientNumber,string BOLNumber) {
            //Get a list of cartons (details) for the specified client and BOL number
            DataSet cartons = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_DETAILBYBOL,TBL_TRACKING,new object[] { BOLNumber,clientNumber,null });
                if(ds != null && ds.Tables[TBL_TRACKING] != null && ds.Tables[TBL_TRACKING].Rows.Count > 0) cartons.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return cartons;
        }

        public DataSet TrackUSPSCartons(string trackingNumbers,string clientNumber,string vendorNumber) {
            //Get a list of carton tracking details
            DataSet trackResponse = new DataSet();
            try {
                Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
                DataSet ds = new DataSet();
                DbCommand cmd = db.GetStoredProcCommand(USP_USPSCARTONS,new object[] { trackingNumbers,clientNumber,vendorNumber });
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]); ;
                db.LoadDataSet(cmd,ds,TBL_USPSCARTONS);
                if(ds != null && ds.Tables[TBL_USPSCARTONS] != null && ds.Tables[TBL_USPSCARTONS].Rows.Count > 0) trackResponse.Merge(ds.Tables[TBL_USPSCARTONS].Select("","Date DESC"));
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return trackResponse;
        }
        public DataSet TrackLTLPallets(string shipmentNumber) {
            //Get a list of pallet tracking details
            DataSet trackResponse = new DataSet();
            try {
                Database db = DatabaseFactory.CreateDatabase(SQL_CONNID);
                DataSet ds = new DataSet();
                DbCommand cmd = db.GetStoredProcCommand(USP_DETAILBYLTLSHIPMENT,new object[] { shipmentNumber });
                cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);
                db.LoadDataSet(cmd,ds,TBL_LTLSHIPMENT);
                if(ds != null && ds.Tables[TBL_LTLSHIPMENT] != null && ds.Tables[TBL_LTLSHIPMENT].Rows.Count > 0) trackResponse.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return trackResponse;
        }
    }
}
