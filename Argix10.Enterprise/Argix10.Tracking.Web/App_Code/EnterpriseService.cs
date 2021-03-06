﻿using System;
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
        public const string SQL_CONNID = "EnterpriseR";
        private const string USP_LOG_NEW = "uspLogEntryNew";
        private const string LOG_NAME = "Argix10", LOG_SOURCE = "TrackingReports";
        
        public const string USP_CLIENTS = "uspRptClientGetList",TBL_CLIENTS = "ClientTable";
        public const string USP_CLIENTSFORVENDOR = "uspRptClientGetListForVendor";
        public const string USP_VENDORS = "uspRptVendorGetlistForClient",TBL_VENDORS = "VendorTable";
        public const string USP_EXCEPTIONS = "uspRptDeliveryExceptionGetList",TBL_EXCEPTIONS = "ExceptionTable";
        public const string USP_PICKUPS = "uspRptPickupsGetListFromToDate",TBL_PICKUPS = "PickupViewTable";

        //Interface
        public DataSet GetClients(string number, string division, bool activeOnly) {
            //Get a list of clients filtered for a specific division
            DataSet clients = new DataSet();
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
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_CLIENTS, TBL_CLIENTS, new object[] { });
                if(ds != null && ds.Tables[TBL_CLIENTS] != null && ds.Tables[TBL_CLIENTS].Rows.Count > 0) {
                    clients.Merge(ds.Tables[TBL_CLIENTS].Select(filter, "ClientNumber ASC, DivisionNumber ASC"));
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return clients;
        }
        public DataSet GetSecureClients(bool activeOnly) {
            //Load a list of client selections
            DataSet clients = new DataSet();
            try {
                clients.Tables.Add(TBL_CLIENTS);
                clients.Tables[TBL_CLIENTS].Columns.AddRange(new DataColumn[] { new DataColumn("ClientNumber"), new DataColumn("DivisionNumber"), new DataColumn("ClientName"), new DataColumn("TerminalCode"), new DataColumn("Status") });

                bool isAdmin = false;
                ProfileCommon profile = null;
                string clientVendorID = "000";
                MembershipUser user = Membership.GetUser();
                if(user != null) {
                    //Internal\external member logged in
                    isAdmin = Roles.IsUserInRole(user.UserName, "administrators");
                    profile = new ProfileCommon().GetProfile(user.UserName);
                    if(profile != null) clientVendorID = profile.ClientVendorID;
                }
                if(isAdmin || clientVendorID == "000") {
                    //Internal user- get list of all clients
                    clients.Tables[TBL_CLIENTS].Rows.Add(new object[] { "", "", "All", "", "" });
                    DataSet _clients = GetClients(null, "01", activeOnly);
                    clients.Merge(_clients.Tables[TBL_CLIENTS].Select("", "ClientName ASC"));
                }
                else {
                    //Client- list this client only; Vendor: list all of it's clients
                    if(profile.Type.ToLower() == "vendor")
                        clients.Merge(GetClientsForVendor(profile.ClientVendorID));
                    else
                        clients.Tables[TBL_CLIENTS].Rows.Add(new object[] { profile.ClientVendorID, "", profile.Company, "", "" });
                }
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return clients;
        }
        public DataSet GetClientsForVendor(string vendorID) {
            //
            DataSet clients = new DataSet();
            try {
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_CLIENTSFORVENDOR, TBL_CLIENTS, new object[] { vendorID });
                if(ds != null && ds.Tables[TBL_CLIENTS] != null && ds.Tables[TBL_CLIENTS].Rows.Count > 0)
                    clients.Merge(ds.Tables[TBL_CLIENTS].Select("", "ClientName ASC"));
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return clients;
        }
        public DataSet GetVendors(string clientNumber, string clientTerminal) {
            //Get a list of vendors
            DataSet vendors = new DataSet();
            try {
                vendors.Tables.Add(TBL_VENDORS);
                vendors.Tables[TBL_VENDORS].Columns.AddRange(new DataColumn[] { new DataColumn("VendorNumber"), new DataColumn("VendorName"), new DataColumn("VendorParentNumber"), new DataColumn("VendorSummary") });
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_VENDORS, TBL_VENDORS, new object[] { clientNumber, clientTerminal });
                if(ds != null && ds.Tables[TBL_VENDORS] != null && ds.Tables[TBL_VENDORS].Rows.Count > 0) {
                    vendors.Merge(ds.Tables[TBL_VENDORS].Select("", "VendorNumber ASC"));
                    for(int i = 0; i < vendors.Tables[TBL_VENDORS].Rows.Count; i++) {
                        vendors.Tables[TBL_VENDORS].Rows[i]["VendorSummary"] = (!vendors.Tables[TBL_VENDORS].Rows[i].IsNull("VendorNumber") ? vendors.Tables[TBL_VENDORS].Rows[i]["VendorNumber"].ToString() : "     ") + " - " +
                                                                          (!vendors.Tables[TBL_VENDORS].Rows[i].IsNull("VendorName") ? vendors.Tables[TBL_VENDORS].Rows[i]["VendorName"].ToString().Trim() : "");
                    }
                }
                vendors.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return vendors;
        }
        public DataSet GetPickups(string client, string division, DateTime startDate, DateTime endDate, string vendor) {
            //Get a list of pickups
            DataSet pickups = new DataSet();
            try {
                pickups.Tables.Add(TBL_PICKUPS);
                pickups.Tables[TBL_PICKUPS].Columns.AddRange(new DataColumn[] { new DataColumn("VendorNumber"), new DataColumn("VendorName"), new DataColumn("PUDate", typeof(DateTime)), new DataColumn("PUNumber", typeof(Int32)), new DataColumn("ManifestNumbers"), new DataColumn("TrailerNumbers"), new DataColumn("PickupID"), new DataColumn("TerminalCode"), new DataColumn("ClientDivision") });
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_PICKUPS, TBL_PICKUPS, new object[] { client, division, startDate.ToString("yyyy-MM-dd"), endDate.ToString("yyyy-MM-dd"), vendor });
                if(ds != null && ds.Tables[TBL_PICKUPS] != null && ds.Tables[TBL_PICKUPS].Rows.Count > 0) {
                    pickups.Merge(ds);
                    for(int i = 0; i < pickups.Tables[TBL_PICKUPS].Rows.Count; i++) {
                        pickups.Tables[TBL_PICKUPS].Rows[i]["ManifestNumbers"] = pickups.Tables[TBL_PICKUPS].Rows[i]["ManifestNumbers"].ToString().Replace(",", ", ");
                        pickups.Tables[TBL_PICKUPS].Rows[i]["TrailerNumbers"] = pickups.Tables[TBL_PICKUPS].Rows[i]["TrailerNumbers"].ToString().Replace(",", ", ");
                    }
                }
                pickups.AcceptChanges();
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
                DataSet ds = new DataService().FillDataset(SQL_CONNID, USP_EXCEPTIONS, TBL_EXCEPTIONS, new object[] { });
                if(ds.Tables[TBL_EXCEPTIONS].Rows.Count != 0)
                    exceptions.Merge(ds);
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return exceptions;
        }
        public DataSet GetRetailDates(string scope) {
            //Create a list of retail dates
            DataSet ds = new DataSet();
            try {
                ds.Tables.Add("DateRangeTable");
                ds.Tables["DateRangeTable"].Columns.Add("Year", Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Quarter", Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Month", Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("Name");
                ds.Tables["DateRangeTable"].Columns.Add("Week", Type.GetType("System.Int32"));
                ds.Tables["DateRangeTable"].Columns.Add("StartDate", Type.GetType("System.DateTime"));
                ds.Tables["DateRangeTable"].Columns.Add("EndDate", Type.GetType("System.DateTime"));
                ds.Tables["DateRangeTable"].Columns.Add("Value");
                DataSet _ds = null;
                string field = "";
                switch(scope.ToLower()) {
                    case "week":
                        field = "Week";
                        _ds = new DataService().FillDataset(SQL_CONNID, "uspRptRetailCalendarWeekGetList", "DateRangeTable", new object[] { });
                        break;
                    case "month":
                        field = "Month";
                        _ds = new DataService().FillDataset(SQL_CONNID, "uspRptRetailCalendarMonthGetList", "DateRangeTable", new object[] { });
                        break;
                    case "quarter":
                        field = "Quarter";
                        _ds = new DataService().FillDataset(SQL_CONNID, "uspRptRetailCalendarQuarterGetList", "DateRangeTable", new object[] { });
                        break;
                    case "ytd":
                        field = "Year";
                        _ds = new DataService().FillDataset(SQL_CONNID, "uspRptRetailCalendarYearGetList", "DateRangeTable", new object[] { });
                        break;
                }
                for(int i = _ds.Tables["DateRangeTable"].Rows.Count; i > 0; i--)
                    ds.Tables["DateRangeTable"].ImportRow(_ds.Tables["DateRangeTable"].Rows[i - 1]);
                for(int i = 0; i < ds.Tables["DateRangeTable"].Rows.Count; i++) {
                    string val = ds.Tables["DateRangeTable"].Rows[i][field].ToString().Trim();
                    string start = ((DateTime)ds.Tables["DateRangeTable"].Rows[i]["StartDate"]).ToString("yyyy-MM-dd");
                    string end = ((DateTime)ds.Tables["DateRangeTable"].Rows[i]["EndDate"]).ToString("yyyy-MM-dd");
                    ds.Tables["DateRangeTable"].Rows[i]["Value"] = val + ", " + start + " : " + end + "";
                }
                ds.AcceptChanges();
            }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return ds;
        }

        public DataSet FillDataset(string uspName, string tableName, object[] parameters) {
            //
            return new DataService().FillDataset(SQL_CONNID,uspName,tableName,parameters);
        }
    }
}