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
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Argix.Enterprise;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class TLViewerService:ITLViewerService,ITLViewerService2 {
        //Members

        //Interface
        public TLViewerService() { }

        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(TLViewerGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(TLViewerGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }

        public Argix.Enterprise.Terminals GetTerminals() {
            //Returns a list of terminals
            Argix.Enterprise.Terminals terminals = null;
            try {
                terminals = new Argix.Enterprise.Terminals();
                DataSet ds = new TLViewerGateway().GetTerminals();
                if (ds != null) {
                    EnterpriseDataset tDS = new EnterpriseDataset();
                    tDS.Merge(ds);
                    for (int i = 0;i < tDS.TerminalTable.Rows.Count;i++) {
                        Argix.Enterprise.Terminal terminal = new Argix.Enterprise.Terminal(tDS.TerminalTable[i]);
                        terminals.Add(terminal);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return terminals;
        }
        public TLs GetTLView(int terminalID) {
            //Get a view of TLs for the specified terminal
            TLs tls=null;
            try {
                tls = new TLs();
                DataSet ds = new TLViewerGateway().GetTLView(terminalID);
                if(ds != null) {
                    FreightDataset tlDS = new FreightDataset();
                    tlDS.Merge(ds);
                    for(int i=0;i<tlDS.TLTable.Rows.Count;i++) {
                        TL tl = new TL(tlDS.TLTable[i]);
                        tls.Add(tl);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tls;
        }
        public TLs GetTLDetail(int terminalID,string tlNumber) {
            //Get TL detail for the specified TL#
            TLs tls=null;
            try {
                tls = new TLs();
                DataSet ds = new TLViewerGateway().GetTLDetail(terminalID,tlNumber);
                if(ds != null) {
                    FreightDataset tlDS = new FreightDataset();
                    tlDS.Merge(ds);
                    for(int i=0;i<tlDS.TLTable.Rows.Count;i++) {
                        TL tl = new TL(tlDS.TLTable[i]);
                        tls.Add(tl);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tls;
        }
        public TLs GetAgentSummary(int terminalID) {
            //Get an agent summary view for the specified terminal
            TLs tls=null;
            try {
                tls = new TLs();
                DataSet ds = new TLViewerGateway().GetAgentSummary(terminalID);
                if(ds != null) {
                    FreightDataset tlDS = new FreightDataset();
                    tlDS.Merge(ds);
                    for(int i=0;i<tlDS.TLTable.Rows.Count;i++) {
                        TL tl = new TL(tlDS.TLTable[i]);
                        tls.Add(tl);
                    }
                }
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tls;
        }

        public DataSet GetTerminals2() {
            //Returns a list of terminals
            DataSet terminals = null;
            try {
                terminals = new DataSet();
                DataSet ds = new TLViewerGateway().GetTerminals();
                if (ds != null) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<TLViewerFault>(new TLViewerFault(ex.Message),"Service Error"); }
            return terminals;
        }
        public DataSet GetTLView2(int terminalID) {
            //Get a view of TLs for the specified terminal
            DataSet tls = null;
            try {
                tls = new DataSet();
                DataSet ds = new TLViewerGateway().GetTLView(terminalID);
                if (ds != null) tls.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<TLViewerFault>(new TLViewerFault(ex.Message),"Service Error"); }
            return tls;
        }
        public DataSet GetTLDetail2(int terminalID,string tlNumber) {
            //Get TL detail for the specified TL#
            DataSet tls = null;
            try {
                tls = new DataSet();
                DataSet ds = new TLViewerGateway().GetTLDetail(terminalID,tlNumber);
                if (ds != null) tls.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<TLViewerFault>(new TLViewerFault(ex.Message),"Service Error"); }
            return tls;
        }
        public DataSet GetAgentSummary2(int terminalID) {
            //Get an agent summary view for the specified terminal
            DataSet tls = null;
            try {
                tls = new DataSet();
                DataSet ds = new TLViewerGateway().GetAgentSummary(terminalID);
                if (ds != null) tls.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<TLViewerFault>(new TLViewerFault(ex.Message),"Service Error"); }
            return tls;
        }
    }
}
