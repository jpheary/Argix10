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
    public class DriverBadgeGateway {
        //Members
        public const string SQL_CONNID = "Drivers";

        private const string USP_BADGES_VIEW = "uspIDViewerBadgeDataGetList",TBL_BADGES = "BadgeTable";
        private const string USP_BADGES_SUMVIEW = "uspIDViewerBadgeDataGetSummaryList";
        private const string USP_BADGE = "uspIDViewerBadgeDataGet";
        private const string USP_BADGES_SEARCH = "uspIDViewerBadgeDataSearch";
        
        //Interface
        public DriverBadgeGateway() { }
        public DataSet ReadBadges() {
            //
            DataSet badges = null;
            try {
                badges = new DataService().FillDataset(SQL_CONNID,USP_BADGES_VIEW,TBL_BADGES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badges;
        }
        public DataSet ReadBadgesSummary() {
            //
            DataSet badges = null;
            try {
                badges = new DataService().FillDataset(SQL_CONNID,USP_BADGES_SUMVIEW,TBL_BADGES,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badges;
        }
        public DataSet ReadBadge(int idNumber) {
            //
            DataSet badge = null;
            try {
                badge = new DataService().FillDataset(SQL_CONNID,USP_BADGE,TBL_BADGES,new object[] { idNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badge;
        }
        public DataSet SearchBadges(string lastName,string firstName,string location,string badgeNumber) {
            //Search for badges
            DataSet badges = null;
            try {
                badges = new DataService().FillDataset(SQL_CONNID,USP_BADGES_SEARCH,TBL_BADGES,new object[] { lastName,firstName,location,badgeNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return badges;
        }
    }
}