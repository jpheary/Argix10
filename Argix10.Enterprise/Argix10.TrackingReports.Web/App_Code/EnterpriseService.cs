using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web.Security;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Argix {
    //
    public enum FreightType { Regular=0, Returns }
    
    [ServiceBehavior(IncludeExceptionDetailInFaults=true)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class EnterpriseService {
        //Members
        public const string SQL_CONNID = "SQLConnection";
        private const string USP_LOG_NEW = "uspLogEntryNew";
        private const string LOG_NAME = "Argix10", LOG_SOURCE = "TrackingReports";
        
        public const string USP_CLIENTS = "uspRptClientGetList",TBL_CLIENTS = "ClientTable";
        public const string USP_CLIENTSFORVENDOR = "uspRptClientGetListForVendor";
        public const string USP_VENDORS = "uspRptVendorGetlistForClient",TBL_VENDORS = "VendorTable";
        public const string USP_EXCEPTIONS = "uspRptDeliveryExceptionGetList",TBL_EXCEPTIONS = "ExceptionTable";
        public const string USP_PICKUPS = "uspRptPickupsGetListFromToDate",TBL_PICKUPS = "PickupViewTable";

        //Interface
        public ClientDataset GetClients(string number, string division, bool activeOnly) {
            //Get a list of clients filtered for a specific division
            ClientDataset clients = null;
            try {
                string filter = "";
                if(number != null && number.Length > 0) filter = "ClientNumber='" + number + "'";
                if(division != null && division.Length > 0) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "DivisionNumber='" + division + "'";
                }
                if(activeOnly) {
                    if(filter.Length > 0) filter += " AND ";
                    filter += "Status = 'A'";
                }
                clients = new ClientDataset();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CLIENTS,TBL_CLIENTS,new object[] { });
                if(ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    ClientDataset _clients = new ClientDataset();
                    _clients.Merge(ds);
                    clients.Merge(_clients.ClientTable.Select(filter,"ClientNumber ASC, DivisionNumber ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public ClientDataset GetSecureClients(bool activeOnly) {
            //Load a list of client selections
            ClientDataset clients = null;
            try {
                clients = new ClientDataset();
                bool isAdmin=false;
                ProfileCommon profile=null;
                string clientVendorID = "000";
                MembershipUser user = Membership.GetUser();
                if(user != null) {
                    //Internal\external member logged in
                    isAdmin = Roles.IsUserInRole(user.UserName,"administrators");
                    profile = new ProfileCommon().GetProfile(user.UserName);
                    if(profile != null) clientVendorID =  profile.ClientVendorID;
                }
                if(isAdmin || clientVendorID == "000") {
                    //Internal user- get list of all clients
                    clients.ClientTable.AddClientTableRow("","","All","","");
                    ClientDataset _clients = GetClients(null,"01",activeOnly);
                    clients.Merge(_clients.ClientTable.Select("","ClientName ASC"));
                }
                else {
                    //Client- list this client only; Vendor: list all of it's clients
                    if(profile.Type.ToLower() == "vendor")
                        clients.Merge(GetClientsForVendor(profile.ClientVendorID));
                    else
                        clients.ClientTable.AddClientTableRow(profile.ClientVendorID,"",profile.Company,"","");
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public ClientDataset GetClientsForVendor(string vendorID) {
            //
            ClientDataset clients = new ClientDataset();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_CLIENTSFORVENDOR,TBL_CLIENTS,new object[] { vendorID });
                if (ds.Tables[TBL_CLIENTS].Rows.Count > 0)
                    clients.Merge(ds.Tables[TBL_CLIENTS].Select("","ClientName ASC"));
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public VendorDataset GetVendors(string clientNumber,string clientTerminal) {
            //Get a list of vendors
            VendorDataset vendors = null;
            try {
                vendors = new VendorDataset();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_VENDORS,TBL_VENDORS,new object[] { clientNumber,clientTerminal });
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
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return vendors;
        }
        public PickupDataset GetPickups(string client,string division,DateTime startDate,DateTime endDate,string vendor) {
            //Get a list of pickups
            PickupDataset pickups = null;
            try {
                pickups = new PickupDataset();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_PICKUPS,TBL_PICKUPS,new object[] { client,division,startDate.ToString("yyyy-MM-dd"),endDate.ToString("yyyy-MM-dd"),vendor });
                if(ds.Tables[TBL_PICKUPS].Rows.Count != 0)
                    pickups.Merge(ds);
                for(int i=0;i<pickups.PickupViewTable.Rows.Count;i++) {
                    pickups.PickupViewTable[i].ManifestNumbers = pickups.PickupViewTable[i].ManifestNumbers.Replace(",",", ");
                    pickups.PickupViewTable[i].TrailerNumbers = pickups.PickupViewTable[i].TrailerNumbers.Replace(",",", ");
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return pickups;
        }
        public DataSet GetDeliveryExceptions() {
            //Get a list of delivery exceptions
            DataSet exceptions = null;
            try {
                exceptions = new DataSet();
                DataSet ds = new DataService().FillDataset(SQL_CONNID,USP_EXCEPTIONS,TBL_EXCEPTIONS,new object[] { });
                if(ds.Tables[TBL_EXCEPTIONS].Rows.Count != 0)
                    exceptions.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return exceptions;
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
                        _ds = new DataService().FillDataset(SQL_CONNID,"uspRptRetailCalendarWeekGetList","DateRangeTable",new object[] { });
                        break;
                    case "month":
                        field = "Month";
                        _ds = new DataService().FillDataset(SQL_CONNID,"uspRptRetailCalendarMonthGetList","DateRangeTable",new object[] { });
                        break;
                    case "quarter":
                        field = "Quarter";
                        _ds = new DataService().FillDataset(SQL_CONNID,"uspRptRetailCalendarQuarterGetList","DateRangeTable",new object[] { });
                        break;
                    case "ytd":
                        field = "Year";
                        _ds = new DataService().FillDataset(SQL_CONNID,"uspRptRetailCalendarYearGetList","DateRangeTable",new object[] { });
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
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ds;
        }

        public void WriteLogEntry(int logLevel,string username,Exception ex) {
            //Write log entry to database
            try {
                new DataService().ExecuteNonQuery("Enterprise",USP_LOG_NEW,new object[] { LOG_NAME,logLevel,DateTime.Now,LOG_SOURCE,"","",username,"","","","",ex.ToString() });
            }
            catch { }
        }

        public DataSet FillDataset(string uspName, string tableName, object[] parameters) {
            //
            return new DataService().FillDataset(SQL_CONNID,uspName,tableName,parameters);
        }
    }
}