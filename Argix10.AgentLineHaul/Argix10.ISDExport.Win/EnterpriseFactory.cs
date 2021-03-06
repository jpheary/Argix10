﻿using System;
using System.Collections;
using System.Diagnostics;
using System.Data;
using System.Reflection;

namespace Argix.Enterprise {
    //
    public class EnterpriseFactory {
        //Members
        public const string USP_PICKUPS = "uspISDExportPickupGetListForPUDate",TBL_PICKUPS = "PickupTable";
        public const string USP_SORTEDITEMS = "uspISDExportSortedItemGetList",TBL_SORTEDITEMS = "SortedItemTable";
        public const string USP_EXPORTFILECONFIG = "uspISDExportFileConfigurationGet",TBL_EXPORTFILECONFIG = "ExportTable";
        public const string USP_EXPORTFILECONFIGCREATE = "uspISDExportFileConfigurationCreate";
        public const string USP_EXPORTFILECONFIGUPDATE = "uspISDExportFileConfigurationUpdate";
        public const string USP_EXPORTFILECONFIGDELETE = "uspISDExportFileConfigurationDelete";
        public const string USP_EXPORTFILENAME = "uspISDExportFileNameGet",TBL_EXPORTFILENAME = "ExportTable";
               
        //Interface
        static EnterpriseFactory() { }
        private EnterpriseFactory() { }
        public static ISDExportDataset GetPickups(DateTime pickupDate) {
            //Update a collection (dataset) of all pickups for the terminal on the local LAN database
            ISDExportDataset pickups=null;
            try {
                pickups = new ISDExportDataset();
                DataSet ds = fillDataset(USP_PICKUPS,TBL_PICKUPS,new object[] { pickupDate });
                if(ds!=null) pickups.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while reading pickups.", ex); }
            return pickups;
        }
        public static ISDExportDataset GetSortedItems(string pickupID) {
            //Get sorted items for a pickup
            ISDExportDataset sortedItems=null;
            try {
                sortedItems = new ISDExportDataset();
                DataSet ds = fillDataset(USP_SORTEDITEMS,TBL_SORTEDITEMS,new object[] { pickupID });
                if(ds != null)
                    sortedItems.Merge(ds,false,MissingSchemaAction.Ignore);
            }
            catch(Exception ex) { throw ex; }
            return sortedItems;
        }
        public static string GetExportFilename(string counterKey) {
            string filename="";
            try {
                DataSet ds = fillDataset(USP_EXPORTFILENAME,TBL_EXPORTFILENAME,new object[] { counterKey });
                filename = ds.Tables[TBL_EXPORTFILENAME].Rows[0]["FILENAME"].ToString();
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
            return filename;
        }
        public static ISDExportDataset GetClients(string clientNumber) {
            ISDExportDataset configuration=null;
            try {
                configuration = new ISDExportDataset();
                DataSet ds = fillDataset(USP_EXPORTFILECONFIG,TBL_EXPORTFILECONFIG,new object[] { clientNumber });
                if(ds != null) configuration.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
            return configuration;
        }
        public static ISDExportDataset GetClients() {
            //Refresh view
            ISDExportDataset configuration=null;
            try {
                configuration = new ISDExportDataset();
                DataSet ds = fillDataset(USP_EXPORTFILECONFIG,TBL_EXPORTFILECONFIG,new object[] { null });
                if(ds != null) configuration.Merge(ds);
            }
            catch(Exception ex) { throw new ApplicationException("",ex); }
            return configuration;
        }
        public static bool CreateClient(string clid,string format,string path,string key,string client,string scanner,string userid) {
            //
            bool created=false;
            created = executeNonQuery(USP_EXPORTFILECONFIGCREATE,new object[] { clid,format,path,key,client,scanner,userid });
            return created;
        }
        public static bool UpdateClient(string clid,string format,string path,string key,string client,string scanner,string userid) {
            //
            bool updated=false;
            updated = executeNonQuery(USP_EXPORTFILECONFIGUPDATE,new object[] { clid,format,path,key,client,scanner,userid });
            return updated;
        }
        public static bool DeleteClient(string clid,string format,string path,string key,string client,string scanner,string userid) {
            //
            bool deleted=false;
            deleted = executeNonQuery(USP_EXPORTFILECONFIGDELETE,new object[] { clid,format,path,key,client,scanner,userid });
            return deleted;
        }

        public static DataSet fillDataset(string spName,string table,object[] paramValues) { return new DataSet(); }
        public static bool executeNonQuery(string spName,object[] paramValues) { return false; }
    }
}
