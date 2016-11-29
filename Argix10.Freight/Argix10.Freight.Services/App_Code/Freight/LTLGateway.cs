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
    public class LTLGateway {
        //Members
        public const string SQL_CONNID = "LTL";
        private bool mUseDispatchClient = true;     //Temporary thang

        private const string USP_QUOTELOG_VIEW = "uspLTLQuoteLog2View",TBL_QUOTELOG = "LTLQuoteTable";
        private const string USP_QUOTELOG_INSERT = "uspLTLQuoteLog2EntryCreate";
        private const string USP_QUOTE_GET = "uspLTLQuoteCreate",TBL_QUOTE = "LTLQuoteTable";

        private const string USP_CLIENTS_VIEW = "uspLTLClientsView",TBL_CLIENT = "LTLClientTable";
        private const string USP_CLIENT_CREATE = "uspLTLClientCreate",USP_CLIENT_UPDATE = "uspLTLClientUpdate";
        private const string USP_CLIENT_READ = "uspLTLClientRead";
        private const string USP_CLIENT_APPROVE = "uspLTLClientApprove";
        private const string USP_CLIENT_GETLIST = "uspLTLClientsGetList";

        private const string USP_DISPATCHCLIENTS_VIEW = "uspLTLDispatchClientsView2";
        private const string USP_DISPATCHCLIENT_CREATE = "uspLTLDispatchClientCreate2",USP_DISPATCHCLIENT_UPDATE = "uspLTLDispatchClientUpdate";
        private const string USP_DISPATCHCLIENT_READ = "uspLTLDispatchClientRead2";
        private const string USP_DISPATCHCLIENT_APPROVE = "uspLTLDispatchClientApprove";
        private const string USP_DISPATCHCLIENT_GETLIST = "uspLTLDispatchClientsGetList";

        private const string USP_SHIPPERS_VIEW = "uspLTLShippersView",TBL_SHIPPER = "LTLShipperTable";
        private const string USP_SHIPPER_LIST = "uspLTLShipperGetList";
        private const string USP_SHIPPER_CREATE = "uspLTLShipperCreate", USP_SHIPPER_UPDATE = "uspLTLShipperUpdate";
        private const string USP_SHIPPER_READ = "uspLTLShipperRead";

        private const string USP_CONSIGNEES_VIEW = "uspLTLConsigneesView",TBL_CONSIGNEE = "LTLConsigneeTable";
        private const string USP_CONSIGNEE_LIST = "uspLTLConsigneeGetList";
        private const string USP_CONSIGNEE_CREATE = "uspLTLConsigneeCreate", USP_CONSIGNEE_UPDATE = "uspLTLConsigneeUpdate";
        private const string USP_CONSIGNEE_READ = "uspLTLConsigneeRead";

        private const string USP_SHIPMENTS_VIEW = "uspLTLShipmentsView",TBL_SHIPMENT = "LTLShipmentTable";
        private const string USP_SHIPMENT_CREATE = "uspLTLShipmentCreate",USP_SHIPMENT_UPDATE = "uspLTLShipmentUpdate";
        private const string USP_SHIPMENT_READ = "uspLTLShipmentRead";
        private const string USP_SHIPMENT_CANCEL = "uspLTLShipmentCancel";
        private const string USP_SHIPMENT_DISPATCH = "uspLTLShipmentDispatch",USP_SHIPMENT_ARRIVE = "uspLTLShipmentArrive";

        private const string USP_PALLET_CREATE = "uspLTLPalletCreate",USP_PALLET_UPDATE = "uspLTLPalletUpdate";
        private const string USP_PALLETLABELS_READ = "uspLTLPalletLabelsRead",TBL_PALLETLABEL = "LTLPalletLabelTable";
        private const string USP_PALLET_DELETE = "uspLTLPalletDelete";

        private const string USP_SERVICELOCATION_READ = "uspLTLConsigneeZipRead",TBL_SERVICELOCATION = "ServiceLocationTable";
        private const string USP_PICKUPLOCATION_READ = "uspLTLShipperZipRead";

        //Interface
        public LTLGateway() { }

        public void CreateQuoteLogEntry(LTLQuote quote) {
            //Log a quote
            try {
                new DataService().ExecuteNonQuery(SQL_CONNID,USP_QUOTELOG_INSERT,
                    new object[] { 
                        quote.Created, quote.ShipDate, quote.OriginZip, quote.DestinationZip, 
                        quote.Pallet1Weight, quote.Pallet1Class, quote.Pallet1InsuranceValue, 
                        quote.Pallet2Weight, quote.Pallet2Class, quote.Pallet2InsuranceValue, 
                        quote.Pallet3Weight, quote.Pallet3Class, quote.Pallet3InsuranceValue, 
                        quote.Pallet4Weight, quote.Pallet4Class, quote.Pallet4InsuranceValue, 
                        quote.Pallet5Weight, quote.Pallet5Class, quote.Pallet5InsuranceValue, 
                        quote.InsidePickup, quote.LiftGateOrigin, quote.AppointmentOrigin, 
                        quote.InsideDelivery, quote.LiftGateDestination, quote.AppointmentDestination,
                        quote.Pallets, quote.Weight, quote.PalletRate, quote.FuelSurcharge, quote.AccessorialCharge, quote.InsuranceCharge, quote.TollCharge, quote.TotalCharge
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }
        public DataSet GetQuote(LTLQuote quote) {
            //
            DataSet ltlQuote = null;
            try {
                ltlQuote = new DataService().FillDataset(SQL_CONNID,USP_QUOTE_GET,TBL_QUOTE,
                    new object[] { 
                        quote.ShipDate, quote.OriginZip, quote.DestinationZip, 
                        quote.InsidePickup, quote.InsideDelivery, 
                        quote.AppointmentOrigin, quote.AppointmentDestination,
                        quote.LiftGateOrigin, quote.LiftGateDestination, 
                        quote.Pallet1InsuranceValue, quote.Pallet2InsuranceValue, quote.Pallet3InsuranceValue, quote.Pallet4InsuranceValue, quote.Pallet5InsuranceValue, 
                        quote.Pallets
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ltlQuote;
        }
        public DataSet ReadQuoteLog() {
            //
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_QUOTELOG_VIEW,TBL_QUOTELOG,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }

        public DataSet ViewLTLClients() {
            //
            DataSet clients = null;
            try {
                string usp = this.mUseDispatchClient ? USP_DISPATCHCLIENTS_VIEW : USP_CLIENTS_VIEW;
                clients = new DataService().FillDataset(SQL_CONNID,usp,TBL_CLIENT,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public int CreateLTLClient(LTLClient client) {
            //Create a new LTL client
            int id = 0;
            try {
                string usp = this.mUseDispatchClient ? USP_DISPATCHCLIENT_CREATE : USP_CLIENT_CREATE;
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,usp,
                    new object[] { 
                    0, 
                    client.Name, client.AddressLine1, client.AddressLine2, client.City, client.State, client.Zip, client.Zip4,
                    client.ContactName, client.ContactPhone, client.ContactEmail, 
                    client.CorporateName, client.CorporateAddressLine1, client.CorporateAddressLine2, client.CorporateCity, client.CorporateState, client.CorporateZip, client.CorporateZip4, client.TaxIDNumber,
                    client.BillingAddressLine1, client.BillingAddressLine2, client.BillingCity, client.BillingState, client.BillingZip, client.BillingZip4,
                    (client.SalesRepClientID>0?client.SalesRepClientID:null as object), client.Status, client.LastUpdated, client.UserID
                });
                id = (int)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public DataSet ReadLTLClient(int clientID) {
            //
            DataSet clients = null;
            try {
                string usp = this.mUseDispatchClient ? USP_DISPATCHCLIENT_READ : USP_CLIENT_READ;
                clients = new DataService().FillDataset(SQL_CONNID,usp,TBL_CLIENT,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool UpdateLTLClient(LTLClient client) {
            //Update an existing LTL client
            bool updated = false;
            try {
                string usp = this.mUseDispatchClient ? USP_DISPATCHCLIENT_UPDATE : USP_CLIENT_UPDATE;
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,usp,
                    new object[] { 
                    client.ID, 
                    client.AddressLine1, client.AddressLine2, client.City, client.State, client.Zip, client.Zip4,
                    client.ContactName, client.ContactPhone, client.ContactEmail, 
                    client.CorporateName, client.CorporateAddressLine1, client.CorporateAddressLine2, client.CorporateCity, client.CorporateState, client.CorporateZip, client.CorporateZip4, client.TaxIDNumber, 
                    client.BillingAddressLine1, client.BillingAddressLine2, client.BillingCity, client.BillingState, client.BillingZip, client.BillingZip4,
                    client.LastUpdated, client.UserID
                });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool ApproveLTLClient(int clientID,string clientNumber,bool approve,string username) {
            //Approve a new LTL client
            bool approved = false;
            try {
                string usp = this.mUseDispatchClient ? USP_DISPATCHCLIENT_APPROVE : USP_CLIENT_APPROVE;
                approved = new DataService().ExecuteNonQuery(SQL_CONNID,usp,new object[] { clientID,clientNumber,approve,username });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return approved;
        }
        public DataSet GetLTLClientList(int salesRepClientID) {
            //
            DataSet clients = null;
            try {
                string usp = this.mUseDispatchClient ? USP_DISPATCHCLIENT_GETLIST : USP_CLIENT_GETLIST;
                clients = new DataService().FillDataset(SQL_CONNID,usp,TBL_CLIENT,new object[] { salesRepClientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }

        public DataSet ViewLTLShippers(int clientID) {
            //
            DataSet shippers = null;
            try {
                shippers = new DataService().FillDataset(SQL_CONNID,USP_SHIPPERS_VIEW,TBL_SHIPPER,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shippers;
        }
        public int CreateLTLShipper(LTLShipper shipper) {
            //Create a new LTL shipper
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_SHIPPER_CREATE,
                    new object[] { 
                        0, shipper.ClientID, 
                        shipper.Name, shipper.AddressLine1, shipper.AddressLine2, shipper.City, shipper.State, shipper.Zip, shipper.Zip4,
                        shipper.WindowStartTime, shipper.WindowEndTime, shipper.ContactName, shipper.ContactPhone, shipper.ContactEmail, 
                        shipper.Status, shipper.LastUpdated, shipper.UserID
                    });
                id = (int)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public DataSet ReadLTLShippersList(int clientID) {
            //
            DataSet shippers = null;
            try {
                shippers = new DataService().FillDataset(SQL_CONNID,USP_SHIPPER_LIST,TBL_SHIPPER,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shippers;
        }
        public DataSet ReadLTLShipper(int shipperID) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_SHIPPER_READ,TBL_SHIPPER,new object[] { shipperID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool UpdateLTLShipper(LTLShipper shipper) {
            //Update an existing LTL shipper
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPPER_UPDATE,
                    new object[] { 
                        shipper.ID, 
                        shipper.AddressLine1, shipper.AddressLine2, 
                        shipper.WindowStartTime, shipper.WindowEndTime, shipper.ContactName, shipper.ContactPhone, shipper.ContactEmail, 
                        shipper.LastUpdated, shipper.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }

        public DataSet ViewLTLConsignees(int clientID) {
            //
            DataSet consignees = null;
            try {
                consignees = new DataService().FillDataset(SQL_CONNID,USP_CONSIGNEES_VIEW,TBL_CONSIGNEE,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return consignees;
        }
        public int CreateLTLConsignee(LTLConsignee consignee) {
            //Create a new LTL consignee
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_CONSIGNEE_CREATE,
                    new object[] { 
                        0, consignee.ClientID, 
                        consignee.Name, consignee.AddressLine1, consignee.AddressLine2, consignee.City, consignee.State, consignee.Zip, consignee.Zip4,
                        consignee.WindowStartTime, consignee.WindowEndTime, consignee.ContactName, consignee.ContactPhone, consignee.ContactEmail, 
                        consignee.Status, consignee.LastUpdated, consignee.UserID
                    });
                id = (int)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public DataSet ReadLTLConsigneesList(int clientID) {
            //
            DataSet consignees = null;
            try {
                consignees = new DataService().FillDataset(SQL_CONNID,USP_CONSIGNEE_LIST,TBL_CONSIGNEE,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return consignees;
        }
        public DataSet ReadLTLConsignee(int consigneeID) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_CONSIGNEE_READ,TBL_CONSIGNEE,new object[] { consigneeID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool UpdateLTLConsignee(LTLConsignee consignee) {
            //Update an existing LTL consignee
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CONSIGNEE_UPDATE,
                    new object[] { 
                        consignee.ID, 
                        consignee.AddressLine1, consignee.AddressLine2, 
                        consignee.WindowStartTime, consignee.WindowEndTime, consignee.ContactName, consignee.ContactPhone, consignee.ContactEmail, 
                        consignee.LastUpdated, consignee.UserID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }

        public DataSet ViewLTLShipments(int clientID) {
            //
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_SHIPMENTS_VIEW,TBL_SHIPMENT,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public int CreateLTLShipment(LTLShipment shipment) {
            //Create a new Book Log shipment
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_SHIPMENT_CREATE,
                    new object[] { 
                        0, shipment.Created, shipment.ClientID, shipment.ShipDate, shipment.ShipperID, shipment.ConsigneeID,  
                        shipment.Pallets, shipment.Weight, shipment.PalletRate, shipment.FuelSurcharge, shipment.AccessorialCharge, shipment.InsuranceCharge, shipment.TollCharge, shipment.TotalCharge, 
                        shipment.InsidePickup, shipment.LiftGateOrigin, 
                        (shipment.AppointmentOrigin != DateTime.MinValue ? shipment.AppointmentOrigin : null as object), 
                         shipment.InsideDelivery, shipment.LiftGateDestination, 
                       (shipment.AppointmentDestination != DateTime.MinValue ? shipment.AppointmentDestination : null as object), 
                        shipment.LastUpdated, shipment.UserID
                    });
                id = (int)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public DataSet ReadLTLShipment(int entryID) {
            DataSet shipment = null;
            try {
                shipment = new DataService().FillDataset(SQL_CONNID,USP_SHIPMENT_READ,TBL_SHIPMENT,new object[] { entryID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shipment;
        }
        public bool UpdateLTLShipment(LTLShipment shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_UPDATE,
                    new object[] { 
                         0, shipment.ShipDate, 
                        shipment.Pallets, shipment.Weight, shipment.PalletRate, shipment.FuelSurcharge, shipment.AccessorialCharge, shipment.InsuranceCharge, shipment.TollCharge, shipment.TotalCharge, 
                        shipment.InsidePickup, shipment.LiftGateOrigin, 
                        (shipment.AppointmentOrigin != DateTime.MinValue ? shipment.AppointmentOrigin : null as object), 
                         shipment.InsideDelivery, shipment.LiftGateDestination, 
                       (shipment.AppointmentDestination != DateTime.MinValue ? shipment.AppointmentDestination : null as object), 
                        shipment.LastUpdated, shipment.UserID
                   });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool DispatchLTLShipment(LTLShipment shipment) {
            //Dispatch an existing LTL shipment
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_DISPATCH,
                    new object[] { shipment.ID, shipment.PickupID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool ArriveLTLShipment(LTLShipment shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_ARRIVE,
                    new object[] { shipment.ID, shipment.PickupDate  });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelLTLShipment(int shipmentID) {
            //Cancel an existing LTL shiment
            bool cancelled = false;
            try {
                cancelled = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_CANCEL,
                    new object[] { shipmentID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return cancelled;
        }

        public int CreateLTLPallet(LTLPallet pallet) {
            //Create a new LTL pallet
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_PALLET_CREATE,
                    new object[] { 
                        0, pallet.ShipmentID, pallet.Weight, pallet.InsuranceValue
                    });
                id = (int)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return id;
        }
        public DataSet ReadPalletLabels(int shipmentID) {
            //
            DataSet labels = null;
            try {
                labels = new DataService().FillDataset(SQL_CONNID,USP_PALLETLABELS_READ,TBL_PALLETLABEL,new object[] { shipmentID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return labels;
        }
        public bool UpdateLTLPallet(LTLPallet pallet) {
            //Update an existing LTL pallet
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PALLET_UPDATE,
                    new object[] { 
                        pallet.ID, pallet.Weight
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool DeleteLTLPallet(int palletID) {
            //Delete an existing LTL pallet
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_PALLET_DELETE,
                    new object[] { palletID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }

        public DataSet ReadServiceLocation(string zipCode) {
            //
            DataSet location = null;
            try {
                location = new DataService().FillDataset(SQL_CONNID,USP_SERVICELOCATION_READ,TBL_SERVICELOCATION,new object[] { zipCode });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return location;
        }
        public DataSet ReadPickupLocation(string zipCode) {
            //
            DataSet location = null;
            try {
                location = new DataService().FillDataset(SQL_CONNID,USP_PICKUPLOCATION_READ,TBL_SERVICELOCATION,new object[] { zipCode });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return location;
        }
    }
}
