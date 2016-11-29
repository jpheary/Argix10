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
        private const string TBL_ZIPS = "DeliveryZipTable";

        //Interface
        public EnterpriseGateway() { }

        //Just messing around
        public DataSet ViewDeliveryZips() {
            //
            DataSet zips = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, "SELECT [Zip] FROM [dbo].[LTLRateDeliveryZip]", new string[] { TBL_ZIPS });
                if(ds.Tables[TBL_ZIPS].Rows.Count > 0) zips.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return zips;
        }
    }
}