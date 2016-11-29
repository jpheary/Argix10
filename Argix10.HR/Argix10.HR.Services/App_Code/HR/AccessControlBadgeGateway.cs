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

namespace Argix.HR {
	//
    public class AccessControlBadgeGateway {
        //Members
        public const string SQL_CONNID = "Access";

        private const string USP_BADGES_VIEW = "uspIDViewerBadgeDataGetList",TBL_BADGES = "BadgeTable";
        
        //Interface
        public AccessControlBadgeGateway() { }
        public DataSet ReadBadges() {
            //
            DataSet data = null;
            try {
                data = new DataService().FillDataset(SQL_CONNID,USP_BADGES_VIEW,TBL_BADGES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return data;
        }
    }
}