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


namespace Argix.Terminals {
    //
    public class DeliveryPointsGateway {
        //Members
        public const string SQL_CONNID = "DeliveryPoints";

        private const string USP_DELIVERYPOINTS = "uspRoadshowExportGet2",TBL_DELIVERYPOINTS = "DeliveryPointTable";
        private const string USP_EXPORTDATE_GET = "uspRoadshowExportDateGet";
        private const string USP_EXPORTDATE_UPDATE = "uspRoadshowExportDateUpdate";

        //Interface
        public DeliveryPointsGateway() { }

        public DataSet GetDeliveryPoints(DateTime startDate,DateTime lastUpated) {
            DataSet points = null;
            //Read all current delivery points
            try {
                points = new DataService().FillDataset(SQL_CONNID,USP_DELIVERYPOINTS,TBL_DELIVERYPOINTS,new object[] { startDate });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return points;
        }
        public DateTime GetExportDate() {
            //Get the latest delivery point LastUpdated date from the last export
            DateTime exportDate;
            try {
                exportDate = Convert.ToDateTime((string)new DataService().ExecuteScalar(SQL_CONNID,USP_EXPORTDATE_GET,new object[] { }));
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return exportDate;
        }
        public bool UpdateExportDate(DateTime lastUpdated) {
            //Update the latest delivery point LastUpdated date from the last export
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_EXPORTDATE_UPDATE,new object[] { lastUpdated.ToString("MM-dd-yyyy HH:mm") });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
    }
}
