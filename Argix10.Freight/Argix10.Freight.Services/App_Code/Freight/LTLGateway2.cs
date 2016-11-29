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
    public class LTLGateway2 {
        //Members
        public const string SQL_CONNID = "LTL2";

        private const string USP_QUOTELOG_VIEW = "uspLTLShipmentQuoteGetList5",TBL_QUOTELOG = "LTLQuoteTable";
        private const string USP_QUOTE_GET = "uspLTLQuoteCreate5", TBL_QUOTE = "LTLQuoteTable";
        private const string USP_QUOTEWITHOVERRIDE_GET = "uspLTLQuoteCreateWithOverride5";
        private const string USP_QUOTELOG_INSERT = "uspLTLQuoteLogEntryCreate3";

        public const string USP_LOADTENDERQUOTE_VIEW = "uspLTLLoadTenderQuotesView", TBL_LOADTENDERQUOTES = "LoadTenderQuoteTable";
        public const string USP_LOADTENDERQUOTE_READ = "uspLTLLoadTenderQuoteRead";
        public const string USP_LOADTENDERQUOTE_CREATE = "uspLTLLoadTenderQuoteCreate", USP_LOADTENDERQUOTE_UPDATE = "uspLTLLoadTenderQuoteUpdate";
        public const string USP_LOADTENDERQUOTE_APPROVE = "uspLTLLoadTenderQuoteApprove", USP_LOADTENDERQUOTE_TENDER = "uspLTLLoadTenderQuoteTender";
        public const string USP_LOADTENDERQUOTE_BOOK = "uspLTLLoadTenderQuoteBook", USP_LOADTENDERQUOTE_CANCEL = "uspLTLLoadTenderQuoteCancel";
        public const string USP_LOADTENDERQUOTE_CHANGEOWNER = "uspLTLLoadTenderQuoteChangeOwner";

        public const string USP_LOADTENDER_CREATE = "uspLTLLoadTenderCreate";
        public const string USP_LOADTENDER_READ = "uspLTLLoadTenderRead", TBL_LOADTENDER = "LoadTenderTable";

        private const string USP_CLIENTS_VIEW = "uspLTLClientsView",TBL_CLIENT = "LTLClientTable";
        private const string USP_CLIENT_CREATE = "uspLTLClientCreate",USP_CLIENT_UPDATE = "uspLTLClientUpdate";
        private const string USP_CLIENT_READ = "uspLTLClientRead",USP_CLIENT_READ2 = "uspLTLClientReadByNumber";
        private const string USP_CLIENT_APPROVE = "uspLTLClientApprove";
        private const string USP_CLIENT_GETLIST = "uspLTLClientsGetList";

        private const string USP_SHIPPERS_VIEW = "uspLTLShippersView",TBL_SHIPPER = "LTLShipperTable";
        private const string USP_SHIPPER_LIST = "uspLTLShippersGetList";
        private const string USP_SHIPPER_CREATE = "uspLTLShipperCreate", USP_SHIPPER_UPDATE = "uspLTLShipperUpdate";
        private const string USP_SHIPPER_READ = "uspLTLShipperRead";

        private const string USP_CONSIGNEES_VIEW = "uspLTLConsigneesView",TBL_CONSIGNEE = "LTLConsigneeTable";
        private const string USP_CONSIGNEE_LIST = "uspLTLConsigneesGetList";
        private const string USP_CONSIGNEE_CREATE = "uspLTLConsigneeCreate", USP_CONSIGNEE_UPDATE = "uspLTLConsigneeUpdate";
        private const string USP_CONSIGNEE_READ = "uspLTLConsigneeRead";

        private const string USP_SHIPMENTS_VIEW = "uspLTLShipmentsView5",TBL_SHIPMENT = "LTLShipmentTable";
        private const string USP_SHIPMENTS_SEARCH = "uspLTLShipmentsSearch";
        private const string USP_SHIPMENT_CREATE = "uspLTLShipmentCreate4",USP_SHIPMENT_UPDATE = "uspLTLShipmentUpdate4";
        private const string USP_SHIPMENT_READ = "uspLTLShipmentRead5";
        private const string USP_SHIPMENT_CANCEL = "uspLTLShipmentCancel";
        private const string USP_SHIPMENT_DISPATCH = "uspLTLShipmentDispatch",USP_SHIPMENT_ARRIVE = "uspLTLShipmentArrive";

        private const string USP_PALLETLABELS_READ = "uspLTLShipmentPalletLabelInfoGet5",TBL_PALLETLABEL = "LTLPalletLabelTable";
        private const string USP_PALLETBOL_READ = "uspLTLShipmentPaperworkGet5", TBL_PALLETBOL = "LTLPalletBOLTable";

        private const string USP_PICKUPLOCATION_READBYZIP = "uspLTLShipperZipRead", USP_PICKUPLOCATION_READBYCITYSTATE = "uspLTLShipperCityStateRead", TBL_SERVICELOCATION = "ServiceLocationTable";
        private const string USP_SERVICELOCATION_READBYZIP = "uspLTLConsigneeZipRead", USP_SERVICELOCATION_READBYCITYSTATE = "uspLTLConsigneeCityStateRead";

        //Interface
        public LTLGateway2() { }

        public DataSet ViewQuoteLog(DateTime start, DateTime end) {
            //
            DataSet log = null;
            try {
                log = new DataService().FillDataset(SQL_CONNID,USP_QUOTELOG_VIEW,TBL_QUOTELOG,new object[] { start, end });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return log;
        }
        public DataSet GetQuote(LTLQuote2 quote) {
            //
            DataSet ltlQuote = null;
            try {
                ltlQuote = new DataService().FillDataset(SQL_CONNID, USP_QUOTE_GET, TBL_QUOTE,
                    new object[] { 
                        quote.ShipDate, quote.OriginZip, quote.DestinationZip, 
                        quote.InsidePickup, quote.InsideDelivery, 
                        quote.AppointmentOrigin, quote.AppointmentDestination,
                        quote.LiftGateOrigin, quote.LiftGateDestination, quote.SameDayPickup, 
                        quote.Pallet1InsuranceValue, quote.Pallet2InsuranceValue, quote.Pallet3InsuranceValue, quote.Pallet4InsuranceValue, quote.Pallet5InsuranceValue, 
                        quote.Pallets, quote.ClientID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return ltlQuote;
        }
        public DataSet GetQuoteWithOverride(LTLQuote2 quote) {
            //
            DataSet ltlQuote = null;
            try {
                ltlQuote = new DataService().FillDataset(SQL_CONNID, USP_QUOTEWITHOVERRIDE_GET, TBL_QUOTE,
                    new object[] { 
                        quote.ShipDate, quote.OriginZip, quote.DestinationZip, 
                        quote.InsidePickup, quote.InsideDelivery, 
                        quote.AppointmentOrigin, quote.AppointmentDestination,
                        quote.LiftGateOrigin, quote.LiftGateDestination, quote.SameDayPickup, 
                        quote.Pallet1InsuranceValue, quote.Pallet2InsuranceValue, quote.Pallet3InsuranceValue, quote.Pallet4InsuranceValue, quote.Pallet5InsuranceValue, 
                        quote.Pallets, quote.ClientID, quote.TotalCharge
                    });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return ltlQuote;
        }
        public void CreateQuoteLogEntry(LTLQuote2 quote) {
            //Log a quote
            try {
                new DataService().ExecuteNonQuery(SQL_CONNID,USP_QUOTELOG_INSERT,
                    new object[] { 
                        quote.ShipDate, quote.OriginZip, quote.DestinationZip, 
                        quote.Pallets, quote.Weight, 
                        quote.PalletRate, quote.FuelSurcharge, 
                        quote.InsidePickupCharge, quote.InsideDeliveryCharge, 
                        quote.AppointmentOriginCharge, quote.AppointmentDestinationCharge, 
                        quote.LiftGateOriginCharge, quote.LiftGateDestinationCharge, 
                        quote.InsuranceCharge, quote.TollCharge, quote.SameDayPickupCharge, quote.TotalCharge, quote.ClientID
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
        }

        public DataSet ViewLoadTenderQuotes(string owner) {
            //
            DataSet quotes = null;
            try {
                quotes = new DataService().FillDataset(SQL_CONNID, USP_LOADTENDERQUOTE_VIEW, TBL_LOADTENDERQUOTES, new object[] { owner });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return quotes;
        }
        public int CreateLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Create a new LTLLoadTenderQuote
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_LOADTENDERQUOTE_CREATE,
                    new object[] { 
                    0, 
                    quote.Owner
	                ,quote.BrokerName,quote.ContactName,quote.ContactPhone,quote.ContactEmail,quote.Description,quote.Comments
	                ,quote.LTLQuote.ShipDate
	                ,quote.LTLQuote.OriginCity,quote.LTLQuote.OriginState,quote.LTLQuote.OriginZip,quote.LTLQuote.InsidePickup,quote.LTLQuote.LiftGateOrigin,quote.LTLQuote.AppointmentOrigin,quote.LTLQuote.SameDayPickup
	                ,quote.LTLQuote.DestinationCity,quote.LTLQuote.DestinationState,quote.LTLQuote.DestinationZip,quote.LTLQuote.InsideDelivery,quote.LTLQuote.LiftGateDestination,quote.LTLQuote.AppointmentDestination
	                ,quote.LTLQuote.Pallet1Weight,quote.LTLQuote.Pallet1InsuranceValue,quote.LTLQuote.Pallet2Weight,quote.LTLQuote.Pallet2InsuranceValue,quote.LTLQuote.Pallet3Weight,quote.LTLQuote.Pallet3InsuranceValue,quote.LTLQuote.Pallet4Weight,quote.LTLQuote.Pallet4InsuranceValue,quote.LTLQuote.Pallet5Weight,quote.LTLQuote.Pallet5InsuranceValue
	                ,quote.LTLQuote.Pallets,quote.LTLQuote.Weight,quote.LTLQuote.PalletRate,quote.LTLQuote.FuelSurcharge,quote.LTLQuote.AccessorialCharge,quote.LTLQuote.InsuranceCharge,quote.LTLQuote.TollCharge,quote.LTLQuote.TotalCharge
	                ,quote.TotalChargeMin,quote.Logged,quote.LoggedBy
                });
                id = (int)o;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return id;
        }
        public DataSet ReadLoadTenderQuote(int id) {
            //
            DataSet quote = null;
            try {
                quote = new DataService().FillDataset(SQL_CONNID, USP_LOADTENDERQUOTE_READ, TBL_LOADTENDERQUOTES, new object[] { id });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return quote;
        }
        public bool UpdateLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Update an existing LTLLoadTenderQuote
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERQUOTE_UPDATE,
                    new object[] { 
                    quote.ID,quote.BrokerName,quote.ContactName,quote.ContactPhone,quote.ContactEmail,quote.Description,quote.Comments,
                    quote.LTLQuote.PalletRate,quote.LTLQuote.FuelSurcharge,quote.LTLQuote.TotalCharge
                });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return updated;
        }
        public bool ApproveLoadTenderQuote(int quoteID, DateTime approved, string approvedBy) {
            //Approve an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERQUOTE_APPROVE, new object[] { quoteID, approved, approvedBy });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public bool TenderLoadTenderQuote(int quoteID, int loadTenderNumber) {
            //Tender an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERQUOTE_TENDER, new object[] { quoteID, loadTenderNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public bool BookLoadTenderQuote(int quoteID, string shipmentNumber) {
            //Book an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERQUOTE_BOOK, new object[] { quoteID, shipmentNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public bool CancelLoadTenderQuote(int quoteID, DateTime cancelled, string cancelledBy) {
            //Cancel an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERQUOTE_CANCEL, new object[] { quoteID, cancelled, cancelledBy });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }
        public bool ChangeOwnerLoadTenderQuote(int quoteID, string owner) {
            //Change the owner of an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new DataService().ExecuteNonQuery(SQL_CONNID, USP_LOADTENDERQUOTE_CHANGEOWNER, new object[] { quoteID, owner });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return result;
        }

        public DataSet GetLoadTender(int number) {
            //Get an existing load tender
            DataSet loadTender = null;
            try {
                loadTender = new DataService().FillDataset(SQL_CONNID, USP_LOADTENDER_READ, TBL_LOADTENDER, new object[] { number });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return loadTender;
        }
        public int CreateLoadTender(string name, byte[] bytes) {
            //Create a new load tender
            int id = 0;
            try {
                //Save the attachment
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID, USP_LOADTENDER_CREATE, new object[] { 0, name, bytes });
                id = (int)o;
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message); }
            return id;
        }

        public DataSet ViewLTLClients() {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_CLIENTS_VIEW,TBL_CLIENT,new object[] { });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public int CreateLTLClient(LTLClient2 client) {
            //Create a new LTL client
            int id = 0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_CLIENT_CREATE,
                    new object[] { 
                    0, 
                    client.Name, client.AddressLine1, client.AddressLine2, client.City, client.State, client.Zip, client.Zip4,
                    client.ContactName, client.ContactPhone, client.ContactEmail, 
                    client.CorporateName, client.CorporateAddressLine1, client.CorporateAddressLine2, client.CorporateCity, client.CorporateState, client.CorporateZip, client.CorporateZip4, client.TaxID,
                    client.BillingAddressLine1, client.BillingAddressLine2, client.BillingCity, client.BillingState, client.BillingZip, client.BillingZip4,
                    (client.SalesRepClientNumber!=null && client.SalesRepClientNumber.Trim().Length>0?client.SalesRepClientNumber:null)
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
                clients = new DataService().FillDataset(SQL_CONNID,USP_CLIENT_READ,TBL_CLIENT,new object[] { clientID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public DataSet ReadLTLClient(string clientNumber) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_CLIENT_READ2,TBL_CLIENT,new object[] { clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool UpdateLTLClient(LTLClient2 client) {
            //Update an existing LTL client
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CLIENT_UPDATE,
                    new object[] { 
                    client.ID, client.Name, 
                    client.AddressLine1, client.AddressLine2, client.City, client.State, client.Zip, client.Zip4,
                    client.ContactName, client.ContactPhone, client.ContactEmail, 
                    client.CorporateName, client.CorporateAddressLine1, client.CorporateAddressLine2, client.CorporateCity, client.CorporateState, client.CorporateZip, client.CorporateZip4, client.TaxID, 
                    client.UserID, (client.SalesRepClientNumber!=null && client.SalesRepClientNumber.Trim().Length>0?client.SalesRepClientNumber:null)
                });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool ApproveLTLClient(int clientID,bool approved,string userID) {
            //Approve a new LTL client
            bool _approved = false;
            try {
                _approved = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CLIENT_APPROVE,new object[] { clientID,approved,userID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return _approved;
        }
        public DataSet GetLTLClientList(string salesRepClientNumber) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_CLIENT_GETLIST,TBL_CLIENT,new object[] { salesRepClientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }

        public DataSet ViewLTLShippers(string clientNumber) {
            //
            DataSet shippers = null;
            try {
                shippers = new DataService().FillDataset(SQL_CONNID,USP_SHIPPERS_VIEW,TBL_SHIPPER,new object[] { clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shippers;
        }
        public string CreateLTLShipper(LTLShipper2 shipper) {
            //Create a new LTL shipper
            string number = "";
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_SHIPPER_CREATE,
                    new object[] { 
                        0, shipper.ClientNumber, 
                        shipper.Name, shipper.AddressLine1, shipper.AddressLine2, shipper.City, shipper.State, shipper.Zip, shipper.Zip4,
                        shipper.ContactName, shipper.ContactPhone, shipper.ContactEmail, 
                        shipper.WindowTimeStart, shipper.WindowTimeEnd, shipper.UserID
                    });
                number = (string)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return number;
        }
        public DataSet ReadLTLShippersList(string clientNumber) {
            //
            DataSet shippers = null;
            try {
                shippers = new DataService().FillDataset(SQL_CONNID,USP_SHIPPER_LIST,TBL_SHIPPER,new object[] { clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shippers;
        }
        public DataSet ReadLTLShipper(string shipperNumber) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_SHIPPER_READ,TBL_SHIPPER,new object[] { shipperNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool UpdateLTLShipper(LTLShipper2 shipper) {
            //Update an existing LTL shipper
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPPER_UPDATE,
                    new object[] { 
                        shipper.Number, shipper.Name, shipper.AddressLine1, shipper.AddressLine2, 
                        shipper.ContactName, shipper.ContactPhone, shipper.ContactEmail, 
                        shipper.WindowTimeStart, shipper.WindowTimeEnd, 
                        shipper.UserID, shipper.Rowversion
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }

        public DataSet ViewLTLConsignees(string clientNumber) {
            //
            DataSet consignees = null;
            try {
                consignees = new DataService().FillDataset(SQL_CONNID,USP_CONSIGNEES_VIEW,TBL_CONSIGNEE,new object[] { clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return consignees;
        }
        public int CreateLTLConsignee(LTLConsignee2 consignee) {
            //Create a new LTL consignee
            int number=0;
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_CONSIGNEE_CREATE,
                    new object[] { 
                        0, consignee.ClientNumber, 
                        consignee.Name, consignee.AddressLine1, consignee.AddressLine2, consignee.City, consignee.State, consignee.Zip, consignee.Zip4,
                        consignee.ContactName, consignee.ContactPhone, consignee.ContactEmail, 
                        consignee.WindowTimeStart, consignee.WindowTimeEnd, consignee.UserID
                    });
                number = (int)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return number;
        }
        public DataSet ReadLTLConsigneesList(string clientNumber) {
            //
            DataSet consignees = null;
            try {
                consignees = new DataService().FillDataset(SQL_CONNID,USP_CONSIGNEE_LIST,TBL_CONSIGNEE,new object[] { clientNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return consignees;
        }
        public DataSet ReadLTLConsignee(int consigneeNumber,string clientNumber) {
            //
            DataSet clients = null;
            try {
                clients = new DataService().FillDataset(SQL_CONNID,USP_CONSIGNEE_READ,TBL_CONSIGNEE,new object[] { clientNumber, consigneeNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return clients;
        }
        public bool UpdateLTLConsignee(LTLConsignee2 consignee) {
            //Update an existing LTL consignee
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_CONSIGNEE_UPDATE,
                    new object[] { 
                        consignee.Number, consignee.ClientNumber, consignee.Name, consignee.AddressLine1, consignee.AddressLine2, 
                        consignee.ContactName, consignee.ContactPhone, consignee.ContactEmail, 
                        consignee.WindowTimeStart, consignee.WindowTimeEnd, 
                        consignee.UserID, consignee.Rowversion
                    });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }

        public DataSet ViewLTLShipments(string clientNumber) {
            //
            DataSet shipments = null;
            try {
                shipments = new DataService().FillDataset(SQL_CONNID,USP_SHIPMENTS_VIEW,TBL_SHIPMENT,new object[] { clientNumber.Trim().Length>0 ? clientNumber : null });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shipments;
        }
        public DataSet SearchLTLShipments(LTLSearch2 criteria) {
            //
            DataSet shipments = null;
            try {
                shipments = new DataService().FillDataset(SQL_CONNID, USP_SHIPMENTS_SEARCH, TBL_SHIPMENT, 
                    new object[] { 
                        criteria.ShipDateStart, criteria.ShipDateEnd,
                        criteria.ClientNumber, 
                        (criteria.ShipperNumber != null && criteria.ShipperNumber.Trim().Length > 0 ? criteria.ShipperNumber : null), 
                        (criteria.ConsigneeNumber > 0 ? criteria.ConsigneeNumber : null as object)
                    });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return shipments;
        }
        public string CreateLTLShipment(LTLShipment2 shipment) {
            //Create a new shipment
            string number="";
            try {
                object o = new DataService().ExecuteNonQueryWithReturn(SQL_CONNID,USP_SHIPMENT_CREATE,
                    new object[] { 
                        "", (shipment.BLNumber!=null?shipment.BLNumber:""), shipment.ShipDate, shipment.ClientNumber, 
                        shipment.ShipperNumber, null, (shipment.PickupAppointmentWindowTimeStart != DateTime.MinValue ? shipment.PickupAppointmentWindowTimeStart : null as object), (shipment.PickupAppointmentWindowTimeEnd != DateTime.MinValue ? shipment.PickupAppointmentWindowTimeEnd : null as object), 
                        null, null, 
                        shipment.ConsigneeNumber, null, (shipment.DeliveryAppointmentWindowTimeStart != DateTime.MinValue ? shipment.DeliveryAppointmentWindowTimeStart : null as object), (shipment.DeliveryAppointmentWindowTimeEnd != DateTime.MinValue ? shipment.DeliveryAppointmentWindowTimeEnd : null as object), 
                        shipment.InsidePickup, shipment.InsideDelivery, shipment.LiftGateOrigin, shipment.LiftGateDestination, shipment.SameDayPickup, 
                        (shipment.ContactName!=null?shipment.ContactName:""), (shipment.ContactPhone!=null?shipment.ContactPhone:""), 
                        null, null, null, shipment.Pallet1Weight, shipment.Pallet1InsuranceValue,
                        null, null, null, shipment.Pallet2Weight, shipment.Pallet2InsuranceValue,
                        null, null, null, shipment.Pallet3Weight, shipment.Pallet3InsuranceValue,
                        null, null, null, shipment.Pallet4Weight, shipment.Pallet4InsuranceValue,
                        null, null, null, shipment.Pallet5Weight, shipment.Pallet5InsuranceValue,
                        shipment.TotalCharge, shipment.UserID
                    });
                number = (string)o;
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return number;
        }
        public DataSet ReadLTLShipment(string shipmentNumber) {
            DataSet shipment = null;
            try {
                shipment = new DataService().FillDataset(SQL_CONNID,USP_SHIPMENT_READ,TBL_SHIPMENT,new object[] { shipmentNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return shipment;
        }
        public bool UpdateLTLShipment(LTLShipment2 shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_UPDATE,
                    new object[] { 
                        shipment.ShipmentNumber, shipment.BLNumber, shipment.ShipDate, 
                        null, (shipment.PickupAppointmentWindowTimeStart != DateTime.MinValue ? shipment.PickupAppointmentWindowTimeStart : null as object), (shipment.PickupAppointmentWindowTimeEnd != DateTime.MinValue ? shipment.PickupAppointmentWindowTimeEnd : null as object), 
                        shipment.PickupID, (shipment.PickupDate != DateTime.MinValue ? shipment.PickupDate : null as object), 
                        null, (shipment.DeliveryAppointmentWindowTimeStart != DateTime.MinValue ? shipment.DeliveryAppointmentWindowTimeStart : null as object), (shipment.DeliveryAppointmentWindowTimeEnd != DateTime.MinValue ? shipment.DeliveryAppointmentWindowTimeEnd : null as object), 
                        shipment.InsidePickup, shipment.InsideDelivery, shipment.LiftGateOrigin, shipment.LiftGateDestination, shipment.SameDayPickup, 
                        null, null, 
                        null, null, null, shipment.Pallet1Weight, shipment.Pallet1InsuranceValue,
                        null, null, null, shipment.Pallet2Weight, shipment.Pallet2InsuranceValue,
                        null, null, null, shipment.Pallet3Weight, shipment.Pallet3InsuranceValue,
                        null, null, null, shipment.Pallet4Weight, shipment.Pallet4InsuranceValue,
                        null, null, null, shipment.Pallet5Weight, shipment.Pallet5InsuranceValue,
                        shipment.TotalCharge, shipment.UserID
                   });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool CancelLTLShipment(string shipmentNumber, string userID) {
            //Cancel an existing LTL shiment
            bool cancelled = false;
            try {
                cancelled = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_CANCEL,
                    new object[] { shipmentNumber, userID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return cancelled;
        }
        public bool DispatchLTLShipment(LTLShipment2 shipment) {
            //Dispatch an existing LTL shipment
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_DISPATCH,
                    new object[] { shipment.ShipmentNumber, shipment.PickupID, (shipment.PickupDate != DateTime.MinValue ? shipment.PickupDate : null as object), shipment.UserID });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }
        public bool ArriveLTLShipment(int pickupID, DateTime pickupDate) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                updated = new DataService().ExecuteNonQuery(SQL_CONNID,USP_SHIPMENT_ARRIVE,
                        new object[] { pickupID, (pickupDate != DateTime.MinValue ? pickupDate : null as object) });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return updated;
        }

        public DataSet ReadPalletLabels(string shipmentNumber) {
            //
            DataSet labels = null;
            try {
                labels = new DataService().FillDataset(SQL_CONNID,USP_PALLETLABELS_READ,TBL_PALLETLABEL,new object[] { shipmentNumber });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return labels;
        }
        public DataSet ReadPalletBOL(string shipmentNumber) {
            //
            DataSet labels = null;
            try {
                labels = new DataService().FillDataset(SQL_CONNID, USP_PALLETBOL_READ, TBL_PALLETBOL, new object[] { shipmentNumber });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return labels;
        }

        public DataSet ReadPickupLocation(string zipCode) {
            //
            DataSet location = null;
            try {
                location = new DataService().FillDataset(SQL_CONNID, USP_PICKUPLOCATION_READBYZIP, TBL_SERVICELOCATION, new object[] { zipCode });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return location;
        }
        public DataSet ReadPickupLocations(string city, string state) {
            //
            DataSet location = null;
            try {
                location = new DataService().FillDataset(SQL_CONNID, USP_PICKUPLOCATION_READBYCITYSTATE, TBL_SERVICELOCATION, new object[] { city, state });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return location;
        }
        public DataSet ReadServiceLocation(string zipCode) {
            //
            DataSet location = null;
            try {
                location = new DataService().FillDataset(SQL_CONNID,USP_SERVICELOCATION_READBYZIP,TBL_SERVICELOCATION,new object[] { zipCode });
            }
            catch (Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return location;
        }
        public DataSet ReadServiceLocations(string city, string state) {
            //
            DataSet location = null;
            try {
                location = new DataService().FillDataset(SQL_CONNID, USP_SERVICELOCATION_READBYCITYSTATE, TBL_SERVICELOCATION, new object[] { city, state });
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return location;
        }
    }
}
