using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public class TsortGateway {
        //Members	
        private const string SQL_CONNID = "Tsort";

        public const string USP_LOADTENDERS = "uspLoadTenderGetList",TBL_LOADTENDERS = "LoadTenderTable";
        public const string USP_LOADTENDERDETAILS = "uspLoadTenderDetailGetList",TBL_LOADTENDERDETAILS = "LoadTenderDetailTable";
        public const int CMD_TIMEOUT_DEFAULT = 300;

        //Interface
        public TsortGateway() { }
        public LoadTenderDataset GetLoadTenders(string clientNumber,DateTime startDate,DateTime endDate) {
        //Get load tenders for the selected client and date range
        LoadTenderDataset lts = null;
        try {
            lts = new LoadTenderDataset();
            if(clientNumber != null && clientNumber.Trim().Length > 0) {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_LOADTENDERS, TBL_LOADTENDERS, new object[] { clientNumber, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd") });
                if(ds != null && ds.Tables[TBL_LOADTENDERS].Rows.Count > 0) lts.Merge(ds);
            }
        }
        catch(Exception ex) { throw new ApplicationException(ex.Message); }
        return lts;
    }
        public LoadTenderDetailDataset GetLoadTenderDetails(string loadNumber) {
            //Get load tender details
            LoadTenderDetailDataset lts = null;
            try {
                lts = new LoadTenderDetailDataset();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_LOADTENDERDETAILS,TBL_LOADTENDERDETAILS,new object[] { loadNumber });
                if (ds != null && ds.Tables[TBL_LOADTENDERDETAILS].Rows.Count > 0) lts.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return lts;
        }
    }
}
