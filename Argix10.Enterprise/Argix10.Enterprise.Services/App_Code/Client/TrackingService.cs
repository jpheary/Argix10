using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Argix.Customers;

namespace Argix {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class TrackingService: ITrackingService, ICRMService {
        //Members
      
        //Interface
        public TrackingService() { }

        public DataSet GetCustomers() {
            //
            DataSet ds = null;
            try {
                ds = new EnterpriseRGateway().GetCustomers();
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetClients(string vendorNumber) {
            //
            DataSet ds = null;
            try {
                ds = new EnterpriseRGateway().GetClients(vendorNumber);
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet GetStoresForSubStore(string subStoreNumber,string clientNumber,string vendorNumber) {
            //
            DataSet ds = null;
            try {
                ds = new EnterpriseRGateway().GetStoresForSubStore(subStoreNumber,clientNumber,vendorNumber);
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return ds;
        }

        public DataSet TrackCartonsForStoreByPickupDate(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorNumber) {
            //
            DataSet ds = null;
            try {
                if(store.Trim().Length > 0) ds = new EnterpriseRGateway().GetCartonsForStoreByPickupDate(clientNumber,store,fromDate,toDate,vendorNumber);
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet TrackCartonsForStoreByDeliveryDate(string clientNumber,string store,DateTime fromDate,DateTime toDate,string vendorNumber) {
            //
            DataSet ds = null;
            try {
                if(store.Trim().Length > 0) ds = new EnterpriseRGateway().GetCartonsForStoreByDeliveryDate(clientNumber, store, fromDate, toDate, vendorNumber);
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return ds;
        }
        public TrackingItems TrackCartonsByCartonNumber(string[] itemNumbers,string clientNumber,string vendorNumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                string numbers = "";
                for(int i=0; i<itemNumbers.Length; i++) { if(i>0) numbers += ","; numbers += itemNumbers[i]; }
                TrackingDataset cartons = new TrackingDataset();
                cartons.Merge(new EnterpriseRGateway().GetCartonsByCartonNumber(numbers,clientNumber,vendorNumber));

                //Return records for all found items
                foreach(TrackingDataset.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    //Keep the response item if it matches a requested item (use CTN number)
                    for(int i = 0;i < itemNumbers.Length;i++) {
                        if(carton.CTN.Trim() == itemNumbers[i]) {
                            //Keep it
                            TrackingItem item = new TrackingItem(itemNumbers[i],carton);
                            items.Add(item);
                            itemNumbers[i] = "";    //Mark as found by nulling it
                            break;
                        }
                    }
                }

                //Return a record for all unfound requests
                for(int i = 0;i < itemNumbers.Length;i++) { if(itemNumbers[i].Length > 0) items.Add(new TrackingItem(itemNumbers[i])); }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingItems TrackCartonsByLabelNumber(string[] itemNumbers,string clientNumber,string vendorNumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                string numbers = "";
                for(int i=0;i<itemNumbers.Length;i++) { if(i>0) numbers += ","; numbers += itemNumbers[i]; }
                TrackingDataset cartons = new TrackingDataset();
                cartons.Merge(new EnterpriseRGateway().GetCartonsByLabelNumber(numbers,clientNumber,vendorNumber));
                
                //Return records for all found items
                foreach(TrackingDataset.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    //Keep the response item if it matches a requested item (use LBL number)
                    for(int i = 0;i < itemNumbers.Length;i++) {
                        if(carton.LBL.ToString().Trim() == itemNumbers[i]) {
                            //Keep it
                            TrackingItem item = new TrackingItem(itemNumbers[i],carton);
                            items.Add(item);
                            itemNumbers[i] = "";    //Mark as found by nulling it
                            break;
                        }
                    }
                }

                //Return a record for all unfound requests
                for(int i = 0;i < itemNumbers.Length;i++) { if(itemNumbers[i].Length > 0) items.Add(new TrackingItem(itemNumbers[i])); }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingItems TrackCartonsByPlateNumber(string[] itemNumbers,string clientNumber,string vendorNumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                string numbers = "";
                for (int i = 0;i < itemNumbers.Length;i++) { if (i > 0) numbers += ","; numbers += itemNumbers[i]; }
                TrackingDataset cartons = new TrackingDataset();
                cartons.Merge(new EnterpriseRGateway().GetCartonsByPlateNumber(numbers,clientNumber,vendorNumber));

                //Return records for all found items
                foreach (TrackingDataset.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    //Keep the response item if it matches a requested item (use CTN number)
                    for (int i = 0;i < itemNumbers.Length;i++) {
                        if (carton.CTN.ToString().Trim() == itemNumbers[i]) {
                            //Keep it
                            TrackingItem item = new TrackingItem(itemNumbers[i],carton);
                            items.Add(item);
                            itemNumbers[i] = "";    //Mark as found by nulling it
                            break;
                        }
                    }
                }

                //Return a record for all unfound requests
                for (int i = 0;i < itemNumbers.Length;i++) { if (itemNumbers[i].Length > 0) items.Add(new TrackingItem(itemNumbers[i])); }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingItems TrackCartonsForPO(string clientNumber,string PONumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                TrackingDataset cartons = new TrackingDataset();
                cartons.Merge(new EnterpriseRGateway().GetCartonsForPO(clientNumber,PONumber));

                //Return records for all items
                foreach (TrackingDataset.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    TrackingItem item = new TrackingItem(carton.CTN,carton);
                    items.Add(item);
                }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingItems TrackCartonsForPRO(string clientNumber,string shipmentNumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                TrackingDataset cartons = new TrackingDataset();
                cartons.Merge(new EnterpriseRGateway().GetCartonsForPRO(clientNumber,shipmentNumber));

                //Return records for all items
                foreach (TrackingDataset.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    TrackingItem item = new TrackingItem(carton.CTN,carton);
                    items.Add(item);
                }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingItems TrackCartonsForBOL(string clientNumber,string BOLNumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                TrackingDataset cartons = new TrackingDataset();
                cartons.Merge(new EnterpriseRGateway().GetCartonsForBOL(clientNumber,BOLNumber));

                //Return records for all items
                foreach (TrackingDataset.TrackingTableRow carton in cartons.TrackingTable.Rows) {
                    TrackingItem item = new TrackingItem(carton.CTN,carton);
                    items.Add(item);
                }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingStoreItems TrackCartonsForStoreSummary(string clientNumber,string storeNumber,DateTime fromDate,DateTime toDate,string vendorNumber,string by) {
            //
            TrackingStoreItems items = new TrackingStoreItems();
            try {
                TrackingDataset cartons = new TrackingDataset();
                DataSet ds = null;
                if(by.ToLower() == "pickup")
                    ds = new EnterpriseRGateway().GetCartonsForStoreByPickupDate2(clientNumber,storeNumber,fromDate,toDate,vendorNumber);
                else
                    ds = new EnterpriseRGateway().GetCartonsForStoreByDeliveryDate2(clientNumber,storeNumber,fromDate,toDate,vendorNumber);
                if (ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) cartons.Merge(ds,true,MissingSchemaAction.Ignore);

                //Build a summary by TL; start with a dataset of unique 
                TrackingDataset tls = new TrackingDataset();
                tls.Merge(cartons.CartonDetailForStoreTable.DefaultView.ToTable(true,new string[] { "TL" }));
                foreach (TrackingDataset.CartonDetailForStoreTableRow tl in tls.CartonDetailForStoreTable.Rows) {
                    //Get one of the cartons from this TL group
                    TrackingDataset.CartonDetailForStoreTableRow tlCarton0 = (TrackingDataset.CartonDetailForStoreTableRow)(cartons.CartonDetailForStoreTable.Select("TL='" + tl.TL + "'","TL ASC"))[0];

                    tl.S = tlCarton0.S;
                    tl.CartonCount = cartons.CartonDetailForStoreTable.Select("TL='" + tl.TL + "'").Length;
                    tl.Wt = int.Parse(cartons.CartonDetailForStoreTable.Compute("Sum(Wt)","TL='" + tl.TL + "'").ToString());
                    tl.CBOL = tlCarton0.IsCBOLNull() ? "" : tlCarton0.CBOL;
                    object minDate = cartons.CartonDetailForStoreTable.Compute("Min(ScD)","TL='" + tl.TL + "' AND (IsNull(ScD,#01/01/1900#) <> #01/01/1900#)");
                    if (minDate != System.DBNull.Value) {
                        tl.ScD = minDate.ToString();
                        object minTime = cartons.CartonDetailForStoreTable.Compute("Min(ScTm)","TL='" + tl.TL + "' AND ScD='" + minDate + "'");
                        tl.ScTm = minTime.ToString();
                    }
                    tl.ActSDD = !tlCarton0.IsActSDDNull() ? tlCarton0.ActSDD : "01/01/0001";
                    tl.OFD1 = !tlCarton0.IsOFD1Null() ? tlCarton0.OFD1 : DateTime.MinValue;
                    tl.Ag = !tlCarton0.IsAgNull() ? tlCarton0.Ag : "";
                    tl.AgNm = tlCarton0.AgNm;
                    tl.AcceptChanges();

                    TrackingStoreItem item = new TrackingStoreItem(tl);
                    items.Add(item);
                }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
        public TrackingStoreItems TrackCartonsForStoreDetail(string clientNumber,string storeNumber,DateTime fromDate,DateTime toDate,string vendorNumber,string by,string tlNumber) {
            //
            TrackingStoreItems items = new TrackingStoreItems();
            try {
                TrackingDataset cartons = new TrackingDataset();
                DataSet ds = null;
                if (by.ToLower() == "pickup")
                    ds = new EnterpriseRGateway().GetCartonsForStoreByPickupDate2(clientNumber,storeNumber,fromDate,toDate,vendorNumber);
                else
                    ds = new EnterpriseRGateway().GetCartonsForStoreByDeliveryDate2(clientNumber,storeNumber,fromDate,toDate,vendorNumber);
                if (ds.Tables["CartonDetailForStoreTable"] != null && ds.Tables["CartonDetailForStoreTable"].Rows.Count > 0) {
                    if(tlNumber != null)
                        cartons.Merge(ds.Tables["CartonDetailForStoreTable"].Select("TL='" + tlNumber + "'"),true,MissingSchemaAction.Ignore);
                    else
                        cartons.Merge(ds,true,MissingSchemaAction.Ignore);
                }

                foreach (TrackingDataset.CartonDetailForStoreTableRow carton in cartons.CartonDetailForStoreTable.Rows) {
                    TrackingStoreItem item = new TrackingStoreItem(carton);
                    items.Add(item);
                }
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
    }
}
