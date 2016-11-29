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


namespace Argix.Freight {
    //
    public class TLViewerGateway {
        //Members
        public const string SQL_CONNID = "TLViewer";

        private const string USP_TERMINALS = "uspTLViewerTerminalsGetList",TBL_TERMINALS = "TerminalTable";
        private const string USP_TLVIEW = "uspTLViewer2",TBL_TLVIEW = "TLTable";
        private const string USP_TLDETAIL = "uspTLViewerTLDetailGet",TBL_TLDETAIL = "TLTable";
        private const string USP_AGENTSUMMARY = "uspTLViewerAgentSummary2",TBL_AGENTSUMMARY = "TLTable";

        //Interface
        public TLViewerGateway() { }

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
        public DataSet GetTLView(int terminalID) {
            //Get a view of TLs for the specified terminal
            DataSet tls = null;
            try {
                tls = new DataSet();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TLVIEW,TBL_TLVIEW,new object[] { terminalID });
                if (ds != null && ds.Tables[TBL_TLVIEW].Rows.Count > 0) tls.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading TL's.",ex); }
            return tls;
        }
        public DataSet GetTLDetail(int terminalID,string tlNumber) {
            //Get TL detail for the specified TL#
            DataSet tls = null;
            try {
                tls = new DataSet();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TLDETAIL,TBL_TLDETAIL,new object[] { terminalID,tlNumber });
                if (ds != null && ds.Tables[TBL_TLDETAIL].Rows.Count > 0) tls.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading TL detail.",ex); }
            return tls;
        }
        public DataSet GetAgentSummary(int terminalID) {
            //Get an agent summary view for the specified terminal
            DataSet tls = null;
            try {
                tls = new DataSet();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_AGENTSUMMARY,TBL_AGENTSUMMARY,new object[] { terminalID });
                if (ds != null && ds.Tables[TBL_AGENTSUMMARY].Rows.Count > 0) tls.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while reading agent summary.",ex); }
            return tls;
        }
    }
}
