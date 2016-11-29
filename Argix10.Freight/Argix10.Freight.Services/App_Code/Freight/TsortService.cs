using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Transactions;
using Argix.Enterprise;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class TsortService:ITsortService, IFreightAssignService, IZoneClosingService {
        //Members

        //Interface
        public TsortService() { }

        public ServiceInfo GetServiceInfo(int terminalID) {
            //Get service information
            return new Argix.AppService(new TsortGateway(terminalID).SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(int terminalID,string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(new TsortGateway(terminalID).SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(int terminalID,TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }
        public DataSet GetTerminals() {
            //
            DataSet terminals = new DataSet();
            try {
                DataSet ds = new EnterpriseGateway().GetArgixTerminals();
                if (ds != null) terminals.Merge(ds);
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return terminals;
        }

        public DataSet GetInboundFreight(int terminalID,DateTime fromDate) {
            //Get a list of inbound shipments for the specified terminal
            DataSet shipments = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetInboundFreight(terminalID,fromDate);
                    if (ds != null) shipments.Merge(ds);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return shipments;
        }
        public InboundShipment GetInboundShipment(int terminalID,string freightID) {
            //Return the inbound shipment for the specified terminal and freightID
            InboundShipment shipment = null;
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetInboundShipment(freightID);
                    if (ds != null) {
                        FreightDataset freight = new FreightDataset();
                        freight.Merge(ds,false,MissingSchemaAction.Ignore);
                        shipment = new InboundShipment(freight.InboundFreightTable[0]);
                    }
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return shipment;
        }
        public bool StartSort(InboundShipment shipment) {
            //
            bool started = false;
            try {
                if (shipment.TerminalID > 0) {
                    started = new TsortGateway(shipment.TerminalID).StartSort(shipment.FreightID,DateTime.Now);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return started;
        }
        public bool StopSort(InboundShipment shipment) {
            //
            bool stopped = false;
            try {
                //Shipment status = 'sorting' and no station assignments
                InboundShipment _shipment = GetInboundShipment(shipment.TerminalID,shipment.FreightID);
                if (_shipment == null) throw new ApplicationException("Inbound shipment " + shipment.FreightID + " could not be found.");
                if (_shipment.Status.ToLower() != "sorting") throw new ApplicationException("Inbound shipment " + shipment.FreightID + " is currently not sorting.");
                FreightDataset _assignments = new FreightDataset();
                _assignments.Merge(GetStationAssignments(shipment.TerminalID));
                if (_assignments.StationFreightAssignmentTable.Rows.Count > 0) {
                    if(_assignments.StationFreightAssignmentTable.Select("FreightID = '" + shipment.FreightID + "'").Length > 0)
                        throw new ApplicationException("Inbound shipment " + shipment.FreightID + " currently has station assignments.");
                }

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    if (shipment.TerminalID > 0) {
                        stopped = new TsortGateway(shipment.TerminalID).StopSort(shipment.FreightID,DateTime.Now);
                    }

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return stopped;
        }
        public DataSet GetStationAssignments(int terminalID) {
            //Get a list of station-freight assignments for the specified terminal
            DataSet assignments = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetStationAssignments();
                    if (ds != null) assignments.Merge(ds);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return assignments;
        }
        public bool CreateStationAssignment(int terminalID,string workStationID,string freightID,int sortTypeID) {
            //
            bool created=false;
            try {
                //Verify shipment exists at this terminal
                InboundShipment _shipment = GetInboundShipment(terminalID,freightID);
                if (_shipment == null) throw new ApplicationException("Inbound shipment " + freightID + " could not be found at terminal " + terminalID.ToString() + ".");

                //Verify shipment is sortable
                if (!_shipment.IsSortable) throw new ApplicationException("Freight cannot be assigned because all TDS arrival information has not been entered.");
                
                //Verify freight is assignable to the specified station at this terminals
                FreightDataset stations = new FreightDataset();
                stations.Merge(GetAssignableSortStations(terminalID,freightID,sortTypeID));
                if(stations.WorkstationTable.Select("WorkstationID ='" + workStationID + "'", "").Length == 0)
                    throw new ApplicationException("WorkstationID " + workStationID + " is not assignable for freight " + freightID + " at terminal " + terminalID.ToString() + ".");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    if (terminalID > 0) {
                        bool unsorted = _shipment.Status.ToLower() == "unsorted";
                        bool sorting = _shipment.Status.ToLower() == "sorting";
                        bool sorted = _shipment.Status.ToLower() == "sorted";
                        if (unsorted)
                            sorting = new TsortGateway(terminalID).StartSort(freightID,DateTime.Now);

                        if (sorting || sorted)
                            created = new TsortGateway(terminalID).CreateStationAssignment(workStationID,freightID,sortTypeID);
                    }

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return created;
        }
        public bool DeleteStationAssignment(StationAssignment assignment) {
            //
            bool deleted = false;
            try {
                if (assignment.InboundFreight.TerminalID > 0) {
                    deleted = new TsortGateway(assignment.InboundFreight.TerminalID).DeleteStationAssignment(assignment.SortStation.WorkStationID,assignment.InboundFreight.FreightID);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return deleted;
        }
        public DataSet GetFreightSortTypes(int terminalID,string freightID) {
            //
            DataSet sortTypes = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetFreightSortTypes(freightID);
                    if (ds != null) sortTypes.Merge(ds);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return sortTypes;
        }
        public DataSet GetAssignableSortStations(int terminalID,string freightID,int sortTypeID) {
            //
            DataSet sortStations = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetAssignableSortStations(terminalID,freightID,sortTypeID);
                    if (ds != null) sortStations.Merge(ds);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return sortStations;
        }

        public DataSet GetTLs(int terminalID) {
            //
            DataSet tls = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetTLs();
                    FreightDataset _tls = new FreightDataset();
                    _tls.Merge(ds);
                    if (_tls.ZoneTable.Rows.Count > 0) tls.Merge(_tls);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return tls;
        }
        public DataSet GetUnassignedTLs(int terminalID) {
            //
            DataSet tls = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetUnassignedTLs();
                    FreightDataset _tls = new FreightDataset();
                    _tls.Merge(ds);
                    if (_tls.ZoneTable.Rows.Count > 0) tls.Merge(_tls);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return tls;
        }
        public DataSet GetUnassignedClosedTLs(int terminalID,int closedDays) {
            //
            DataSet tls = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetUnassignedClosedTLs(closedDays);
                    FreightDataset _tls = new FreightDataset();
                    _tls.Merge(ds);
                    if (_tls.ZoneTable.Rows.Count > 0) tls.Merge(_tls);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return tls;
        }
        public DataSet GetLanes(int terminalID) {
            //Get lists of all sort/small sort lanes for this terminal
            DataSet lanes = new DataSet();
            try {
                if (terminalID > 0) {
                    DataSet ds = new TsortGateway(terminalID).GetLanes();
                    FreightDataset _lanes = new FreightDataset();
                    _lanes.Merge(ds);
                    if (_lanes.LaneTable.Rows.Count > 0) lanes.Merge(_lanes);
                }
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return lanes;
        }
        public bool ChangeLanes(int terminalID,Zone zone) {
            //Update the lane assignments for this zone
            bool changed = false;
            try {
                if (terminalID > 0)
                    changed = new TsortGateway(terminalID).UpdateZoneLanes(zone);
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return changed;
        }
        public bool CloseZone(int terminalID,Zone zone) {
            //Close this zone and update the lane assignments
            bool closed = false;
            try {
                if (terminalID > 0)
                    closed = new TsortGateway(terminalID).CloseZonesTL(zone);
            }
            catch (Exception ex) { throw new FaultException<TsortFault>(new TsortFault(ex.Message),"Service Error"); }
            return closed;
        }
    }
}
