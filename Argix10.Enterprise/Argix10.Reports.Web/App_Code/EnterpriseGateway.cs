using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Web.Security;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public enum FreightType { Regular=0, Returns }
    
    public class EnterpriseGateway {
        //Members
        public const string USP_ARGIXTERMINALS = "uspRptTerminalsGetList",TBL_ARGIXTERMINALS = "TerminalTable";
        public const string USP_TERMINALS = "uspRptTerminalGetList",TBL_TERMINALS = "TerminalTable";
        public const string USP_CLIENTS = "uspRptClientGetList",TBL_CLIENTS = "ClientTable";
        public const string USP_CLIENTDIVS = "uspRptClientCustomerDivisionGetList",TBL_CLIENTDIVS = "ClientDivisionTable";
        public const string USP_CLIENTTERMINALS = "uspRptTerminalGetListForClient",TBL_CLIENTTERMINALS = "ClientTerminalTable";
        public const string USP_CONSUMERDIRECTCUSTOMERS = "uspRptConsumerDirectCustomerGetList",TBL_CONSUMERDIRECTCUSTOMERS="CustomersTable";
        public const string USP_VENDORS = "uspRptVendorGetlistForClient",TBL_VENDORS = "VendorTable";
        public const string USP_AGENTS = "uspRptAgentGetList",TBL_AGENTS = "AgentTable";
        public const string USP_ZONES = "uspRptZoneMainGetList",TBL_ZONES = "ZoneTable";
        public const string USP_REGIONS_DISTRICTS = "uspRptRegionDistrictGetList",TBL_REGIONS = "RegionTable",TBL_DISTRICTS = "DistrictTable";
        public const string USP_EXCEPTIONS = "uspRptDeliveryExceptionGetList",TBL_EXCEPTIONS = "ExceptionTable";
        public const string USP_PICKUPS = "uspRptPickupsGetListFromToDate",TBL_PICKUPS = "PickupViewTable";
        public const string USP_INDIRECTTRIPS = "uspRptIndirectTripGetList",TBL_INDIRECTTRIPS = "IndirectTripTable";
        public const string USP_TLS = "uspRptTLGetListClosedForDateRange",TBL_TLS = "TLListViewTable";
        public const string USP_TLS_FIND = "uspRptTLFindClosedForTLNumber";
        public const string USP_SHIFTS = "uspRptTerminalShiftGetListForDate",TBL_SHIFTS = "ShiftTable";
        public const string USP_DAMAGECODES = "uspRptDamageCodeGetList",TBL_DAMAGECODES = "DamageDetailTable";
        public const string USP_LABELSEQNUMBERS = "uspRptSortedItemGetForCartonNumber",TBL_LABELSEQNUMBERS = "LabelStationTable";
        public const string USP_VENDORLOG = "uspRptManifestGetListForClient",TBL_VENDORLOG = "VendorLogTable";
        public const string USP_INDUCTEDFREIGHT = "uspRptInductedFreightGetList", TBL_INDUCTEDFREIGHT = "FreightTable";

        public const string DAMAGEDESCRIPTON_ALL = "All";
        public const string DAMAGEDESCRIPTON_ALL_EXCEPT_NC = "All, except NON-CONVEYABLE";

        //Interface
        public DataSet GetArgixTerminals() {
            //Get a list of Argix terminals
            TerminalDataset terminals = null;
            try {
                terminals = new TerminalDataset();
                DataSet ds = FillDataset(USP_ARGIXTERMINALS,TBL_ARGIXTERMINALS,new object[] { });
                if(ds.Tables[TBL_ARGIXTERMINALS].Rows.Count != 0) {
                    TerminalDataset _terminals = new TerminalDataset();
                    _terminals.Merge(ds);
                    terminals.Merge(_terminals.TerminalTable.Select("", "Description ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating Argix terminal list.",ex); }
            return terminals;
        }
        public DataSet GetTerminals() {
            //Get a list of Argix terminals
            TerminalDataset terminals = null;
            try {
                terminals = new TerminalDataset();
                DataSet ds = FillDataset(USP_TERMINALS,TBL_TERMINALS,new object[] { });
                if(ds.Tables[TBL_TERMINALS].Rows.Count > 0) {
                    TerminalDataset _terminals = new TerminalDataset();
                    _terminals.Merge(ds);
                    terminals.Merge(_terminals.TerminalTable.Select("","Description ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating Argix terminal list.",ex); }
            return terminals;
        }
        public ClientDataset GetClientsList(bool activeOnly) {
            //Get a list of clients
            ClientDataset clients = null;
            try {
                string filter = "DivisionNumber='01'";
                if(activeOnly) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "Status = 'A'";
                }
                clients = new ClientDataset();
                DataSet ds = FillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDataset _clients = new ClientDataset();
                    _clients.Merge(ds);
                    clients.Merge(_clients.ClientTable.Select(filter,"ClientName ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public ClientDataset GetClients() { return GetClients(null,null,false); }
        public ClientDataset GetClients(string number, string division, bool activeOnly) {
            //Get a list of clients filtered for a specific client or division or both
            //Added logic that checks for division 00 as switch to null TerminalCode for TimePeriodCartonCompare report
            ClientDataset clients = null;
            try {
                string filter = "";
                bool nullTerminalCode = division == "00";
                if(number != null && number.Length > 0) filter = "ClientNumber='" + number + "'";
                if(division != null && division.Length > 0) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "DivisionNumber='" + (division == "00" ? "01" : division) + "'";
                }
                if(activeOnly) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "Status = 'A'";
                }
                clients = new ClientDataset();
                DataSet ds = FillDataset(USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDataset _clients = new ClientDataset();
                    _clients.Merge(ds);
                    clients.Merge(_clients.ClientTable.Select(filter,"ClientNumber ASC, DivisionNumber ASC"));
                }
                if(nullTerminalCode) {
                    for (int i=0; i<clients.ClientTable.Rows.Count; i++) { clients.ClientTable[i].TerminalCode = ""; }
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public ClientDataset GetSecureClients(bool activeOnly) {
            //Load a list of client selections
            ClientDataset clients = null;
            try {
                clients = new ClientDataset();
                clients.ClientTable.AddClientTableRow("","","All","","");
                ClientDataset _clients = GetClients(null,"01",activeOnly);
                clients.Merge(_clients.ClientTable.Select("","ClientName ASC"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client list.",ex); }
            return clients;
        }
        public ClientDataset GetClientsForVendor(string vendorID) {
            //
            ClientDataset clients = new ClientDataset();
            DataSet ds = FillDataset("[uspRptClientGetListForVendor]",TBL_CLIENTS,new object[] { vendorID });
            if(ds.Tables[TBL_CLIENTS].Rows.Count > 0)
                clients.Merge(ds.Tables[TBL_CLIENTS].Select("","ClientName ASC"));
            return clients;
        }
        public DataSet GetClientDivisions(string number) {
            //Get a list of client divisions
            DataSet divisions = null;
            try {
                divisions = FillDataset(USP_CLIENTDIVS,TBL_CLIENTDIVS,new object[] { number });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client division list.",ex); }
            return divisions;
        }
        public DataSet GetClientTerminals(string number) {
            //Get a list of client terminals
            DataSet terminals = null;
            try {
                terminals = new DataSet();
                DataSet ds = FillDataset(USP_CLIENTTERMINALS,TBL_CLIENTTERMINALS,new object[] { number });
                terminals.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client terminal list.",ex); }
            return terminals;
        }
        public DataSet GetClientRegions(string number) {
            //Get a list of client divisions
            DataSet regions = null;
            try {
                regions = new DataSet();
                DataSet ds = FillDataset(USP_REGIONS_DISTRICTS,TBL_REGIONS,new object[] { number });
                Hashtable table = new Hashtable();
                for(int i = 0;i < ds.Tables[TBL_REGIONS].Rows.Count;i++) {
                    string region = ds.Tables[TBL_REGIONS].Rows[i]["Region"].ToString().Trim();
                    if(region.Length == 0)
                        ds.Tables[TBL_REGIONS].Rows[i].Delete();
                    else {
                        if(table.ContainsKey(region))
                            ds.Tables[TBL_REGIONS].Rows[i].Delete();
                        else {
                            table.Add(region,ds.Tables[TBL_REGIONS].Rows[i]["RegionName"].ToString().Trim());
                            ds.Tables[TBL_REGIONS].Rows[i]["Region"] = region;
                            ds.Tables[TBL_REGIONS].Rows[i]["RegionName"] = ds.Tables[TBL_REGIONS].Rows[i]["RegionName"].ToString().Trim();
                        }
                    }
                }
                regions.Merge(ds,true);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client regions list.",ex); }
            return regions;
        }
        public DataSet GetClientDistricts(string number) {
            //Get a list of client divisions
            DataSet districts = null;
            try {
                districts = FillDataset(USP_REGIONS_DISTRICTS,TBL_DISTRICTS,new object[] { number });
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating client districts list.",ex); }
            return districts;
        }
        public DataSet GetConsumerDirectCustomers() {
            //Get a list of consumer direct customers
            DataSet customers = null;
            try {
                customers = new DataSet();
                DataSet ds = FillDataset(USP_CONSUMERDIRECTCUSTOMERS,TBL_CONSUMERDIRECTCUSTOMERS,new object[] { });
                if(ds.Tables[TBL_CONSUMERDIRECTCUSTOMERS] != null && ds.Tables[TBL_CONSUMERDIRECTCUSTOMERS].Rows.Count > 0) {
                    customers.Merge(ds);
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating customers list.",ex); }
            return customers;
        }
        public VendorDataset GetVendorsList(string clientNumber,string clientTerminal) {
            //Get a list of vendors
            VendorDataset vendors = null;
            try {
                vendors = new VendorDataset();
                DataSet ds = FillDataset(USP_VENDORS,TBL_VENDORS,new object[] { clientNumber,clientTerminal });
                if(ds.Tables[TBL_VENDORS] != null && ds.Tables[TBL_VENDORS].Rows.Count > 0) {
                    VendorDataset _vendors = new VendorDataset();
                    _vendors.Merge(ds);
                    vendors.Merge(_vendors.VendorTable.Select("","VendorName ASC"));
                    vendors.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return vendors;
        }
        public VendorDataset GetVendors(string clientNumber,string clientTerminal) {
            //Get a list of vendors
            VendorDataset vendors = null;
            try {
                vendors = new VendorDataset();
                DataSet ds = FillDataset(USP_VENDORS,TBL_VENDORS,new object[] { clientNumber,clientTerminal });
                if(ds.Tables[TBL_VENDORS] != null && ds.Tables[TBL_VENDORS].Rows.Count > 0) {
                    VendorDataset _vendors = new VendorDataset();
                    _vendors.Merge(ds);
                    for(int i = 0;i < _vendors.VendorTable.Rows.Count;i++) {
                        _vendors.VendorTable.Rows[i]["VendorSummary"] = (!_vendors.VendorTable.Rows[i].IsNull("VendorNumber") ? _vendors.VendorTable.Rows[i]["VendorNumber"].ToString() : "     ") + " - " +
                                                                          (!_vendors.VendorTable.Rows[i].IsNull("VendorName") ? _vendors.VendorTable.Rows[i]["VendorName"].ToString().Trim() : "");
                    }
                    vendors.Merge(_vendors.VendorTable.Select("","VendorNumber ASC"));
                    vendors.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return vendors;
        }
        public VendorDataset GetParentVendors(string clientNumber,string clientTerminal) {
            //Get a list of parent vendors
            System.Diagnostics.Debug.WriteLine("GetParentVendors");
            VendorDataset vendors = null;
            try {
                vendors = new VendorDataset();
                VendorDataset ds = GetVendors(clientNumber,clientTerminal);
                if(clientNumber != null && ds.VendorTable.Rows.Count > 0) {
                    vendors.Merge(ds.VendorTable.Select("VendorParentNumber = ''"));
                    vendors.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return vendors;
        }
        public VendorDataset GetVendorLocations(string clientNumber,string clientTerminal,string vendorNumber) {
            //Get a list of vendor locations (child vendors) for the specified client-vendor
            System.Diagnostics.Debug.WriteLine("GetVendorLocations");
            VendorDataset locs=null;
            try {
                locs = new VendorDataset();
                VendorDataset ds = GetVendors(clientNumber,clientTerminal);
                if(clientNumber != null && vendorNumber != null && ds.VendorTable.Rows.Count > 0) 
                    locs.Merge(ds.VendorTable.Select("VendorParentNumber = '" + vendorNumber + "'"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating vendor list.",ex); }
            return locs;
        }
        public AgentDataset GetAgents(bool mainZoneOnly) {
            //Get a list of agents
            AgentDataset agents = null;
            try {
                agents = new AgentDataset();
                DataSet ds = FillDataset(USP_AGENTS,TBL_AGENTS,new object[] { });
                if(ds.Tables[TBL_AGENTS].Rows.Count != 0) {
                    AgentDataset _ds = new AgentDataset();
                    if(mainZoneOnly) {
                        AgentDataset __ds = new AgentDataset();
                        __ds.Merge(ds);
                        _ds.Merge(__ds.AgentTable.Select("MainZone <> ''"));
                    }
                    else
                        _ds.Merge(ds);
                    for(int i = 0;i < _ds.AgentTable.Rows.Count;i++) {
                        _ds.AgentTable.Rows[i]["AgentSummary"] = (!_ds.AgentTable.Rows[i].IsNull("MainZone") ? _ds.AgentTable.Rows[i]["MainZone"].ToString().PadLeft(2,' ') : "  ") + " - " +
                                                             (!_ds.AgentTable.Rows[i].IsNull("AgentNumber") ? _ds.AgentTable.Rows[i]["AgentNumber"].ToString() : "    ") + " - " +
                                                             (!_ds.AgentTable.Rows[i].IsNull("AgentName") ? _ds.AgentTable.Rows[i]["AgentName"].ToString().Trim() : "");
                    }
                    agents.Merge(_ds.AgentTable.Select("","MainZone ASC"));
                    agents.AgentTable.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating agent list.",ex); }
            return agents;
        }
        public AgentDataset GetParentAgents() {
            //Get a list of parent agent
            System.Diagnostics.Debug.WriteLine("GetParentAgents");
            AgentDataset agents = null;
            try {
                agents = new AgentDataset();
                AgentDataset ds = GetAgents(false);
                if(ds.AgentTable.Rows.Count > 0)
                    agents.Merge(ds.AgentTable.Select("AgentParentNumber = ''"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating subagent list.",ex); }
            return agents;
        }
        public AgentDataset GetAgentLocations(string agent) {
            //Get a list of agents
            System.Diagnostics.Debug.WriteLine("GetAgentLocations");
            AgentDataset subagents = null;
            try {
                subagents = new AgentDataset();
                AgentDataset ds = GetAgents(false);
                if(agent != null && ds.AgentTable.Rows.Count > 0) 
                    subagents.Merge(ds.AgentTable.Select("AgentParentNumber = '" + agent + "'"));
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating subagent list.",ex); }
            return subagents;
        }

        public ShipperDataset GetShippers(FreightType freightType,string clientNumber,string clientTerminal) {
            //Get a list of shippers
            ShipperDataset shippers = null;
            try {
                shippers = new ShipperDataset();
                if(freightType == FreightType.Regular) {
                    VendorDataset vendors = GetVendors(clientNumber,clientTerminal);
                    shippers = new ShipperDataset();
                    for(int i = 0;i < vendors.VendorTable.Rows.Count;i++)
                        shippers.ShipperTable.AddShipperTableRow(vendors.VendorTable[i].VendorNumber,vendors.VendorTable[i].VendorName,vendors.VendorTable[i].VendorParentNumber,vendors.VendorTable[i].VendorSummary);
                    shippers.AcceptChanges();
                }
                else if(freightType == FreightType.Returns) {
                    AgentDataset agents = GetAgents(false);
                    ShipperDataset _shippers = new ShipperDataset();
                    for(int i = 0;i < agents.AgentTable.Rows.Count;i++)
                        _shippers.ShipperTable.AddShipperTableRow(agents.AgentTable[i].AgentNumber,agents.AgentTable[i].AgentName,agents.AgentTable[i].AgentParentNumber,agents.AgentTable[i].AgentSummary);
                    shippers.Merge(_shippers.ShipperTable.Select("","ShipperNumber ASC")); 
                    shippers.AcceptChanges();
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating shipper list.",ex); }
            return shippers;
        }
        public ZoneDataset GetZones() {
            //Get a list of zones
            ZoneDataset zones = null;
            try {
                zones = new ZoneDataset();
                DataSet ds = FillDataset(USP_ZONES,TBL_ZONES,new object[] { });
                if(ds.Tables[TBL_ZONES].Rows.Count != 0)
                    zones.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating zone list.",ex); }
            return zones;
        }

        public PickupDataset GetPickups(string client,string division,DateTime startDate,DateTime endDate,string vendor) {
            //Get a list of pickups
            PickupDataset pickups = null;
            try {
                pickups = new PickupDataset();
                DataSet ds = FillDataset(USP_PICKUPS,TBL_PICKUPS,new object[] { client,division,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd"),vendor });
                if(ds.Tables[TBL_PICKUPS].Rows.Count != 0)
                    pickups.Merge(ds);
                for(int i=0;i<pickups.PickupViewTable.Rows.Count;i++) {
                    pickups.PickupViewTable[i].ManifestNumbers = pickups.PickupViewTable[i].ManifestNumbers.Replace(",",", ");
                    pickups.PickupViewTable[i].TrailerNumbers = pickups.PickupViewTable[i].TrailerNumbers.Replace(",",", ");
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating pickup list.",ex); }
            return pickups;
        }
        public DataSet GetDeliveryExceptions() {
            //Get a list of delivery exceptions
            DataSet exceptions = null;
            try {
                exceptions = new DataSet();
                DataSet ds = FillDataset(USP_EXCEPTIONS,TBL_EXCEPTIONS,new object[] { });
                if(ds.Tables[TBL_EXCEPTIONS].Rows.Count != 0)
                    exceptions.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating delivery exceptions list.",ex); }
            return exceptions;
        }
        public IndirectTripDataset GetIndirectTrips(string terminal,int daysBack) {
            //Get a list of indirect trips
            IndirectTripDataset trips = null;
            try {
                trips = new IndirectTripDataset();
                DateTime startDate = DateTime.Today.AddDays(-daysBack);
                DateTime endDate = DateTime.Today;
                DataSet ds = FillDataset(USP_INDIRECTTRIPS,TBL_INDIRECTTRIPS,new object[] { terminal,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_INDIRECTTRIPS] != null && ds.Tables[TBL_INDIRECTTRIPS].Rows.Count != 0)
                    trips.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating indirect trips list.",ex); }
            return trips;
        }
        public TLDataset GetTLs(string terminal, int daysBack) {
            //Event handler for change in selected terminal
            TLDataset tls = new TLDataset();
            try {
                DateTime startDate = DateTime.Today.AddDays(-daysBack);
                DateTime endDate = DateTime.Today;
                DataSet ds = FillDataset(USP_TLS,TBL_TLS,new object[] { terminal,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_TLS].Rows.Count > 0)
                    tls.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating TL list.",ex); }
            return tls;
        }
        public TLDataset GetTLs(string terminal, DateTime startDate, DateTime endDate) {
            //Event handler for change in selected terminal
            TLDataset tls = new TLDataset();
            try {
                DataSet ds = FillDataset(USP_TLS,TBL_TLS,new object[] { terminal,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_TLS] != null)
                    tls.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating TL list.",ex); }
            return tls;
        }
        public TLDataset FindTL(string terminal, string TLNumber) {
            //
            TLDataset tls = new TLDataset();
            try {
                DataSet ds = FillDataset(USP_TLS_FIND,TBL_TLS,new object[] { terminal,TLNumber });
                if(ds.Tables[TBL_TLS] != null)
                    tls.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception finding TL.",ex); }
            return tls;
        }
        public ShiftDataset GetShifts(string terminal,DateTime date) {
            //Event handler for change in selected terminal
            ShiftDataset shifts = null;
            try {
                shifts = new ShiftDataset();
                DataSet ds = FillDataset(USP_SHIFTS,TBL_SHIFTS,new object[] { terminal,date.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_SHIFTS] != null && ds.Tables[TBL_SHIFTS].Rows.Count > 0)
                    shifts.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating shift list.",ex); }
            return shifts;
        }
        public VendorLogDataset GetVendorLog(string client,string clientDivision,DateTime startDate,DateTime endDate) {
            //Event handler for change in selected terminal
            VendorLogDataset log = null;
            try {
                log = new VendorLogDataset();
                DataSet ds = FillDataset(USP_VENDORLOG,TBL_VENDORLOG,new object[] { client,clientDivision,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd") });
                if(ds.Tables[TBL_VENDORLOG].Rows.Count != 0)
                    log.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating Vendor Log list.",ex); }
            return log;
        }

        public DataSet GetRetailDates(string scope) {
            //Create a list of retail dates
            DataSet ds = new DataSet();
            try {
                ds.Tables.Add("DateRangeTable");
                ds.Tables["DateRangeTable"].Columns.Add("Year",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Quarter",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Month",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Name");
                ds.Tables["DateRangeTable"].Columns.Add("Week",Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("StartDate",Type.GetType("System.DateTime"));
                ds.Tables["DateRangeTable"].Columns.Add("EndDate",Type.GetType("System.DateTime"));
                ds.Tables["DateRangeTable"].Columns.Add("Value");
                DataSet _ds = null;
                string field = "";
                switch(scope.ToLower()) {
                    case "week":
                        field = "Week";
                        _ds = FillDataset("uspRptRetailCalendarWeekGetList","DateRangeTable",new object[] { });
                        break;
                    case "month":
                        field = "Month";
                        _ds = FillDataset("uspRptRetailCalendarMonthGetList","DateRangeTable",new object[] { });
                        break;
                    case "quarter":
                        field = "Quarter";
                        _ds = FillDataset("uspRptRetailCalendarQuarterGetList","DateRangeTable",new object[] { });
                        break;
                    case "ytd":
                        field = "Year";
                        _ds = FillDataset("uspRptRetailCalendarYearGetList","DateRangeTable",new object[] { });
                        break;
                }
                for(int i = _ds.Tables["DateRangeTable"].Rows.Count;i > 0;i--)
                    ds.Tables["DateRangeTable"].ImportRow(_ds.Tables["DateRangeTable"].Rows[i - 1]);
                for(int i = 0;i < ds.Tables["DateRangeTable"].Rows.Count;i++) {
                    string val = ds.Tables["DateRangeTable"].Rows[i][field].ToString().Trim();
                    string start = ((DateTime)ds.Tables["DateRangeTable"].Rows[i]["StartDate"]).ToString("yyyy-MM-dd");
                    string end = ((DateTime)ds.Tables["DateRangeTable"].Rows[i]["EndDate"]).ToString("yyyy-MM-dd");
                    ds.Tables["DateRangeTable"].Rows[i]["Value"] = val + ", " + start + " : " + end + "";
                }
                ds.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating retail dates.",ex); }
            return ds;
        }
        public DataSet GetSortDates() {
            //Create a list of sort dates
            DataSet ds = new DataSet();
            try {
                ds.Tables.Add("DateRangeTable");
                ds.Tables["DateRangeTable"].Columns.Add("Value");
                int d = (int)DateTime.Today.DayOfWeek;
                DateTime _end = DateTime.Today.AddDays(-d);
                for(int i=-1;i<52;i++) {
                    DateTime end = _end.AddDays(-7 * i);
                    DateTime start = end.AddDays(-6);
                    ds.Tables["DateRangeTable"].Rows.Add(new object[] { start.ToString("yyyy-MM-dd") + " : " + end.ToString("yyyy-MM-dd") });
                }
                ds.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating sort dates.",ex); }
            return ds;
        }
        public DamageDataset GetDamageCodes() {
            //Get a list of Argix damage codes
            DamageDataset codes = null;
            try {
                codes = new DamageDataset();
                codes.DamageDetailTable.AddDamageDetailTableRow("0",DAMAGEDESCRIPTON_ALL);
                codes.DamageDetailTable.AddDamageDetailTableRow("00",DAMAGEDESCRIPTON_ALL_EXCEPT_NC);
                DataSet ds = FillDataset(USP_DAMAGECODES,TBL_DAMAGECODES,new object[] { });
                if(ds.Tables[TBL_DAMAGECODES].Rows.Count != 0)
                    codes.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating damage codes list.",ex); }
            return codes;
        }
        public LabelStationDataset GetCartons(string cartonNumber,string terminalCode,string clientNumber) {
            //Get all cartons that have the specified carton number
            LabelStationDataset cartons = null;
            try {
                cartons = new LabelStationDataset();
                if(cartonNumber != null) {
                    DataSet ds = FillDataset(USP_LABELSEQNUMBERS,TBL_LABELSEQNUMBERS,new object[] { cartonNumber,terminalCode,clientNumber });
                    if(ds.Tables[TBL_LABELSEQNUMBERS].Rows.Count > 0)
                        cartons.Merge(ds);
                }
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected exception creating list of cartons.",ex); }
            return cartons;
        }
        public InductedFreightDataset GetInductedFreight(DateTime startImportedDate, DateTime endImportedDate, string terminalCode) {
            //
            InductedFreightDataset freight = null;
            try {
                freight = new InductedFreightDataset();
                DataSet ds = FillDataset(USP_INDUCTEDFREIGHT, TBL_INDUCTEDFREIGHT, new object[] { startImportedDate, endImportedDate, terminalCode });
                if (ds.Tables[TBL_INDUCTEDFREIGHT].Rows.Count > 0)
                    freight.Merge(ds);
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return freight;
        }

        public DataSet FillDataset(string sp, string table, object[] o) {
            //
            DataSet ds = new DataSet();
            Database db = DatabaseFactory.CreateDatabase("EnterpriseR");
            DbCommand cmd = db.GetStoredProcCommand(sp,o);
            cmd.CommandTimeout = Convert.ToInt32(ConfigurationManager.AppSettings["CommandTimeout"]);
            db.LoadDataSet(cmd,ds,table);
            return ds;
        }
    }
}