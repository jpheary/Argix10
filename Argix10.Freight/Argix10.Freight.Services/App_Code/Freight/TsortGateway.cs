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
    public class TsortGateway {
        //Members       
        public string SQL_CONNID = "0";

        private const string USP_FREIGHT = "uspFAInboundFreightGetList",TBL_FREIGHT = "InboundFreightTable";
        private const string USP_SHIPMENT = "uspFAInboundFreightGet";
        private const string USP_SHIPMENTUPDATESTART = "uspFAShipmentUpdateStart";
        private const string USP_SHIPMENTUPDATESTOP = "uspFAShipmentUpdateStop";
        private const string USP_SHIPMENTDELETE = "uspFAShipmentDelete";
        private const string USP_ASSIGNMENTS = "uspFAAssignmentGetList",TBL_ASSIGNMENTS = "StationFreightAssignmentTable";
        private const string USP_ASSIGNMENTCREATE = "uspFAAssignmentNew";
        private const string USP_ASSIGNMENTDELETE = "uspFAAssignmentDelete";
        private const string USP_SORTSTATIONS = "uspFAStationsGetListAssignable",TBL_SORTSTATIONS = "WorkstationTable";
        private const string USP_SORTTYPES = "uspFASortTypesGetList",TBL_SORTTYPES = "SortTypesTable";

        public const string USP_LANES = "uspZoneCloseLaneGetList",TBL_LANES = "LaneTable";
        public const string USP_SMALLLANES = "uspZoneCloseLaneSmallSortGetList",TBL_SMALLLANES = "SmallLaneTable";
        public const string USP_TLS = "uspZoneCloseZoneGetList",TBL_TLS = "ZoneTable";
        public const string USP_TLS_UNASSIGNED = "uspZoneCloseUnassignedFreightGetList",TBL_TLS_UNASSIGNED = "ZoneTable";
        public const string USP_TLS_CLOSED = "uspZoneCloseFreightUnassignedClosedGetList",TBL_TLS_CLOSED = "ZoneTable";
        public const string USP_LANE_UPDATE = "uspZoneCloseLaneUpdateTL";
        public const string USP_ZONE_CLOSE = "uspZoneCloseZoneCloseTL";

        //Interface
        public TsortGateway(int terminalID) { SQL_CONNID = terminalID.ToString(); }

        public DataSet GetInboundFreight(int terminalID,DateTime fromDate) {
            //Get a list of inbound shipments for the specified terminal
            DataSet shipments = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_FREIGHT,TBL_FREIGHT,new object[] { terminalID,fromDate });
                if (ds != null && ds.Tables[TBL_FREIGHT].Rows.Count > 0) shipments.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shipments;
        }
        public DataSet GetInboundShipment(string freightID) {
            //Return the inbound shipment for the specified freightID
            DataSet shipment = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_SHIPMENT,TBL_FREIGHT,new object[] { freightID });
                if (ds != null & ds.Tables[TBL_FREIGHT].Rows.Count > 0) shipment.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return shipment;
        }
        public bool StartSort(string freightID,DateTime date) {
            //Set start sort date and status for this shipment
            bool started = false;
            try {
                started = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENTUPDATESTART,new object[] { freightID,date.ToString("yyyyMMdd"),date.ToString("HHmmss") });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return started;
        }
        public bool StopSort(string freightID,DateTime date) {
            //Set stop sort date and status for this shipment
            bool stopped = false;
            try {
                stopped = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENTUPDATESTOP,new object[] { freightID,date.ToString("yyyyMMdd"),date.ToString("HHmmss") });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return stopped;
        }
        public bool DeleteShipment(string freightID) {
            //Delete an inbound shipment from the local LAN database
            bool deleted = false;
            try {
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENTDELETE,new object[] { freightID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }
        public DataSet GetStationAssignments() {
            //Get a list of station-freight assignments for the local terminal
            DataSet assignments = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_ASSIGNMENTS,TBL_ASSIGNMENTS,new object[] { });
                if (ds != null && ds.Tables[TBL_ASSIGNMENTS].Rows.Count > 0) assignments.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return assignments;
        }
        public bool CreateStationAssignment(string workStationID,string freightID,int sortTypeID) {
            //Assign this freight to the specified sort station
            bool created = false;
            try {
                //Create the station assignment
                created = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ASSIGNMENTCREATE,new object[] { workStationID,freightID,sortTypeID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return created;
        }
        public bool DeleteStationAssignment(string workStationID,string freightID) {
            //Delete the specified freight assignment
            bool deleted = false;
            try {
                //Update local LAN database
                deleted = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ASSIGNMENTDELETE,new object[] { workStationID,freightID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return deleted;
        }
        public DataSet GetFreightSortTypes(string freightID) {
            //Get a list of available freight sort types for the specified freight
            DataSet ds = null;
            try {
                ds = new DataSet();
                DataSet _ds = new DataService().FillDataset(SQL_CONNID,USP_SORTTYPES,TBL_SORTTYPES,new object[] { freightID });
                if (_ds != null && _ds.Tables[TBL_SORTTYPES].Rows.Count > 0) ds.Merge(_ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ds;
        }
        public DataSet GetAssignableSortStations(int terminalID,string freightID,int sortTypeID) {
            //Get a list of assignable sort stations (stations without assignments) for the 
            //specified freight and sort type; if sortTypeID = SAN, all those stations that 
            //are sorting SAN for a different client are eligible
            DataSet workstations = null;
            try {
                workstations = new DataSet();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_SORTSTATIONS,TBL_SORTSTATIONS,new object[] { freightID,terminalID,sortTypeID });
                if (ds != null && ds.Tables[TBL_SORTSTATIONS].Rows.Count > 0) workstations.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return workstations;
        }

        public DataSet GetTLs() {
            //
            DataSet tls = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TLS,TBL_TLS,new object[] { });
                if (ds != null && ds.Tables[TBL_TLS].Rows.Count > 0) tls.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tls;
        }
        public DataSet GetUnassignedTLs() {
            //
            DataSet tls = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TLS_UNASSIGNED,TBL_TLS_UNASSIGNED,new object[] { });
                if (ds != null && ds.Tables[TBL_TLS_UNASSIGNED].Rows.Count > 0) tls.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tls;
        }
        public DataSet GetUnassignedClosedTLs(int closedDays) {
            //
            DataSet tls = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_TLS_CLOSED,TBL_TLS_CLOSED,new object[] { closedDays });
                if (ds != null && ds.Tables[TBL_TLS_CLOSED].Rows.Count > 0) tls.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return tls;
        }
        public DataSet GetLanes() {
            //Get lists of all sort/small sort lanes for this terminal
            DataSet lanes = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_LANES,TBL_LANES,new object[]{});
                if (ds != null && ds.Tables[TBL_LANES].Rows.Count > 0) lanes.Merge(ds);
                ds = new DataService().FillDataset(SQL_CONNID,USP_SMALLLANES,TBL_SMALLLANES,new object[] { });
                if (ds != null && ds.Tables[TBL_SMALLLANES].Rows.Count > 0) lanes.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return lanes;
        }
        public bool UpdateZoneLanes(Zone zone) {
            //Update the lane assignments for this zone
            bool changed = false;
            try {
                changed = new DataService().ExecuteNonQuery(SQL_CONNID,USP_LANE_UPDATE,new object[] { zone.TL,zone.NewLane,zone.NewSmallSortLane });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return changed;
        }
        public bool CloseZonesTL(Zone zone) {
            //Close this zone and update the lane assignments
            bool closed = false;
            try {
                closed = new DataService().ExecuteNonQuery(SQL_CONNID,USP_ZONE_CLOSE,new object[] { zone.ZoneCode,zone.TL,null,zone.NewLane,zone.NewSmallSortLane,zone.ClientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return closed;
        }
    }
}
