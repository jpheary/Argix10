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
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    public class FreightService:IFreightService {
        //Members
        private string mConnectionID="";
        public const string USP_DELIVERY = "uspIMDeliveryGetList1 ",TBL_DELIVERY = "DeliveryTable";
        public const string USP_OSDSCANS = "uspIMDeliveryOSDScansGetList ",TBL_OSDSCANS = "ScanTable";
        public const string USP_PODSCANS = "uspIMDeliveryPODScansGetList ",TBL_PODSCANS = "ScanTable";

        //Interface
        public FreightService(string connectionID) {
            //Constructor
            this.mConnectionID = connectionID;
        }

        public DataSet GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to) {
            //Get a list of store locations
            DataSet deliveries = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to });
                if(ds.Tables[TBL_DELIVERY].Rows.Count > 0) deliveries.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return deliveries;
        }
        public DataSet GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID) {
            //Get a list of store locations
            DataSet delivery = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_DELIVERY,TBL_DELIVERY,new object[] { companyID,storeNumber,from,to });
                if (ds.Tables[TBL_DELIVERY].Rows.Count > 0) delivery.Merge(ds.Tables[TBL_DELIVERY].Select("CPROID=" + proID));
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return delivery;
        }
        public DataSet GetOSDScans(long cProID) {
            //Get a list of store locations
            DataSet scans = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_OSDSCANS,TBL_OSDSCANS,new object[] { cProID });
                if(ds.Tables[TBL_OSDSCANS].Rows.Count > 0) scans.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return scans;
        }
        public DataSet GetPODScans(long cProID) {
            //Get a list of store locations
            DataSet scans = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(this.mConnectionID,USP_PODSCANS,TBL_PODSCANS,new object[] { cProID });
                if(ds.Tables[TBL_PODSCANS].Rows.Count > 0) scans.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message); }
            return scans;
        }
    }
}