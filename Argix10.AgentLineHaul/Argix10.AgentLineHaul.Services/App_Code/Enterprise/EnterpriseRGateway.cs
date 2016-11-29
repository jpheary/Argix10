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

namespace Argix.Enterprise {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnterpriseRGateway {
        //Members
        public const string SQL_CONNID = "EnterpriseR";

        private const string USP_MANIFESTS = "dbo.uspRptManifestGetListForClient", TBL_MANIFESTS = "ManifestTable";
        private const string USP_MANIFEST = "dbo.uspRptManifestDetailGetList", TBL_MANIFEST = "ManifestDetailTable";

        //Interface
        public EnterpriseRGateway() { }

        public DataSet InboundManifestsView(string clientNumber, string clientDivision, DateTime startPickupDate, DateTime endPickupDate) {
            //
            DataSet manifests = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_MANIFESTS, TBL_MANIFESTS, new object[] { clientNumber, clientDivision, startPickupDate.ToString("yyyy-MM-dd"), endPickupDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_MANIFESTS].Rows.Count != 0) manifests.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return manifests;
        }
        public DataSet InboundManifestRead(string manifestID) {
            //
            DataSet manifest = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_MANIFEST, TBL_MANIFEST, new object[] { manifestID });
                if(ds.Tables[TBL_MANIFEST].Rows.Count != 0) manifest.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return manifest;
        }
    }
}
