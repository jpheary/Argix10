using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Transactions;
using System.Web.Configuration;
using System.Web.Security;
using Argix.Enterprise;

namespace Argix.Freight {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class LTLService:ILTLService,ILTLAdminService,ILTLClientService {
        //Members

        //Interface
        public LTLService() { }

        #region ILTLService
        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(LTLGateway.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(LTLGateway.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }
        #endregion

        #region ILTLClientService
        public LTLQuote CreateQuote(LTLQuote quote) {
            //Create and log the quote
            try {
                //For existing clients: determne zip codes from shipper/consignee
                if (quote.ShipperID > 0) quote.OriginZip = ReadLTLShipper(quote.ShipperID).Zip;
                if (quote.ConsigneeID > 0) quote.DestinationZip = ReadLTLConsignee(quote.ConsigneeID).Zip;

                //Create the quote
                quote.Pallets++;
                quote.Weight += quote.Pallet1Weight;
                if (quote.Pallet2Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet2Weight; }
                if (quote.Pallet3Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet3Weight; }
                if (quote.Pallet4Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet4Weight; }
                if (quote.Pallet5Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet5Weight; }
                quote.PalletRate = 0.0M;
                quote.FuelSurcharge = 0.0M;
                quote.AccessorialCharge = 0.0M;
                quote.InsuranceCharge = 0.0M;
                quote.TollCharge = 0.0M;
                quote.TotalCharge = 0.0M;

                //Get quote
                DataSet ds = new LTLGateway().GetQuote(quote);

                quote.PalletRate = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["PalletDeliveryCharge"].ToString());
                quote.TransitMin = 0;
                quote.TransitMax = 0;
                quote.FuelSurcharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["FuelSurcharge"].ToString());
                quote.AccessorialCharge += decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsidePickupCharge"].ToString());
                quote.AccessorialCharge += decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsideDeliveryCharge"].ToString());
                quote.AccessorialCharge += decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentOriginCharge"].ToString());
                quote.AccessorialCharge += decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentDestinationCharge"].ToString());
                quote.AccessorialCharge += decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeOrigin"].ToString());
                quote.AccessorialCharge += decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeDestination"].ToString());
                quote.InsuranceCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["ShipmentInsuranceCharge"].ToString());
                quote.TollCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TollCharge"].ToString());
                quote.TotalCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TotalCharge"].ToString());

                //Log the quote
                new LTLGateway().CreateQuoteLogEntry(quote);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return quote;
        }

        public int CreateLTLClient(LTLClient client) {
            //Add a new LTL client
            int id = 0;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    id = new LTLGateway().CreateLTLClient(client);

                    //Send email notification to Finance
                    client.ID = id;
                    new NotifyService().NotifyClientCreated(client);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return id;
        }
        public LTLClient ReadLTLClient(int clientID) {
            LTLClient client = new LTLClient();
            try {
                DataSet ds = new LTLGateway().ReadLTLClient(clientID);
                if (ds != null && ds.Tables["LTLClientTable"] != null && ds.Tables["LTLClientTable"].Rows.Count > 0) {
                    DataRow _client = ds.Tables["LTLClientTable"].Rows[0];
                    client = new LTLClient();
                    client.ID = int.Parse(_client["ID"].ToString());
                    client.Number = _client["Number"].ToString();
                    client.Name = _client["Name"].ToString();
                    client.AddressLine1 = _client["AddressLine1"].ToString();
                    client.AddressLine2 = _client["AddressLine2"].ToString();
                    client.City = _client["City"].ToString();
                    client.State = _client["State"].ToString();
                    client.Zip = _client["Zip"].ToString();
                    client.Zip4 = _client["Zip4"].ToString();
                    client.ContactName = _client["ContactName"].ToString();
                    client.ContactPhone = _client["ContactPhone"].ToString();
                    client.ContactEmail = _client["ContactEmail"].ToString();
                    client.CorporateName = _client["CorporateName"].ToString();
                    client.CorporateAddressLine1 = _client["CorporateAddressLine1"].ToString();
                    client.CorporateAddressLine2 = _client["CorporateAddressLine2"].ToString();
                    client.CorporateCity = _client["CorporateCity"].ToString();
                    client.CorporateState = _client["CorporateState"].ToString();
                    client.CorporateZip = _client["CorporateZip"].ToString();
                    client.CorporateZip4 = _client["CorporateZip4"].ToString();
                    client.TaxIDNumber = _client["TaxIDNumber"].ToString();
                    client.BillingAddressLine1 = _client["BillingAddressLine1"].ToString();
                    client.BillingAddressLine2 = _client["BillingAddressLine2"].ToString();
                    client.BillingCity = _client["BillingCity"].ToString();
                    client.BillingState = _client["BillingState"].ToString();
                    client.BillingZip = _client["BillingZip"].ToString();
                    client.BillingZip4 = _client["BillingZip4"].ToString();
                    client.Approved = !_client.IsNull("Approved") ? bool.Parse(_client["Approved"].ToString()) : false;
                    client.ApprovedDate = !_client.IsNull("ApprovedDate") ? DateTime.Parse(_client["ApprovedDate"].ToString()) : DateTime.MinValue;
                    client.ApprovedUser = !_client.IsNull("ApprovedUser") ? _client["ApprovedDate"].ToString() : "";
                    client.Status = _client["Status"].ToString();
                    client.LastUpdated = DateTime.Parse(_client["LastUpdated"].ToString());
                    client.UserID = _client["UserID"].ToString();
                    client.SalesRepClientID = !_client.IsNull("SalesRepClientID") ? int.Parse(_client["SalesRepClientID"].ToString()) : 0;
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return client;
        }
        public bool UpdateLTLClient(LTLClient client) {
            //Update an existing LTL client
            bool updated = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new LTLGateway().UpdateLTLClient(client);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }
        public DataSet GetLTLClientList(int salesRepClientID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().GetLTLClientList(salesRepClientID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }

        public DataSet ViewLTLShippers(int clientID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ViewLTLShippers(clientID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public int CreateLTLShipper(LTLShipper shipper) {
            //Add a new LTL shipper
            int id = 0;
            try {
                //Apply simple business rules
                //Validate the shipper zip code is a pickup location for an Argix local terminal
                ServiceLocation location = ReadPickupLocation(shipper.Zip);
                if (location == null) throw new ApplicationException(shipper.Zip + " is currently not supported for pickup.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    id = new LTLGateway().CreateLTLShipper(shipper);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return id;
        }
        public DataSet ReadLTLShippersList(int clientID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ReadLTLShippersList(clientID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public LTLShipper ReadLTLShipper(int shipperID) {
            LTLShipper shipper = null;
            try {
                DataSet ds = new LTLGateway().ReadLTLShipper(shipperID);
                if (ds != null && ds.Tables["LTLShipperTable"] != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) {
                    DataRow _shipper = ds.Tables["LTLShipperTable"].Rows[0];
                    shipper = new LTLShipper();
                    shipper.ID = int.Parse(_shipper["ID"].ToString());
                    shipper.ClientID = int.Parse(_shipper["ClientID"].ToString());
                    shipper.Name = _shipper["Name"].ToString();
                    shipper.AddressLine1 = _shipper["AddressLine1"].ToString();
                    shipper.AddressLine2 = !_shipper.IsNull("AddressLine2") ? _shipper["AddressLine2"].ToString() : "";
                    shipper.City = _shipper["City"].ToString();
                    shipper.State = _shipper["State"].ToString();
                    shipper.Zip = _shipper["Zip"].ToString();
                    shipper.Zip4 = !_shipper.IsNull("AddressLine2") ? _shipper["Zip4"].ToString() : "";
                    shipper.WindowStartTime = !_shipper.IsNull("WindowStartTime") ? DateTime.Parse(_shipper["WindowStartTime"].ToString()) : DateTime.MinValue;
                    shipper.WindowEndTime = !_shipper.IsNull("WindowEndTime") ? DateTime.Parse(_shipper["WindowEndTime"].ToString()) : DateTime.MinValue;
                    shipper.ContactName = _shipper["ContactName"].ToString();
                    shipper.ContactPhone = !_shipper.IsNull("AddressLine2") ? _shipper["ContactPhone"].ToString() : "";
                    shipper.ContactEmail = _shipper["ContactEmail"].ToString();
                    shipper.Status = _shipper["Status"].ToString();
                    shipper.LastUpdated = DateTime.Parse(_shipper["LastUpdated"].ToString());
                    shipper.UserID = _shipper["UserID"].ToString();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return shipper;
        }
        public bool UpdateLTLShipper(LTLShipper shipper) {
            //Update an existing LTL shipper
            bool updated=false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new LTLGateway().UpdateLTLShipper(shipper);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }

        public DataSet ViewLTLConsignees(int clientID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ViewLTLConsignees(clientID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public int CreateLTLConsignee(LTLConsignee consignee) {
            //Add a new LTL consignee
            int id = 0;
            try {
                //Apply simple business rules
                //Validate the consignee zip code is a delivery location by an Argix agent terminal
                ServiceLocation location = ReadServiceLocation(consignee.Zip);
                if (location == null) throw new ApplicationException(consignee.Zip + " is currently not supported for delivery.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    id = new LTLGateway().CreateLTLConsignee(consignee);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return id;
        }
        public DataSet ReadLTLConsigneesList(int clientID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ReadLTLConsigneesList(clientID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public LTLConsignee ReadLTLConsignee(int consigneeID) {
            LTLConsignee consignee = null;
            try {
                DataSet ds = new LTLGateway().ReadLTLConsignee(consigneeID);
                if (ds != null && ds.Tables["LTLConsigneeTable"] != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) {
                    DataRow _consignee = ds.Tables["LTLConsigneeTable"].Rows[0];
                    consignee = new LTLConsignee();
                    consignee.ID = int.Parse(_consignee["ID"].ToString());
                    consignee.ClientID = int.Parse(_consignee["ClientID"].ToString());
                    consignee.Name = _consignee["Name"].ToString();
                    consignee.AddressLine1 = _consignee["AddressLine1"].ToString();
                    consignee.AddressLine2 = !_consignee.IsNull("AddressLine2") ? _consignee["AddressLine2"].ToString() : "";
                    consignee.City = _consignee["City"].ToString();
                    consignee.State = _consignee["State"].ToString();
                    consignee.Zip = _consignee["Zip"].ToString();
                    consignee.Zip4 = !_consignee.IsNull("Zip4") ? _consignee["Zip4"].ToString() : "";
                    consignee.WindowStartTime = !_consignee.IsNull("WindowStartTime") ? DateTime.Parse(_consignee["WindowStartTime"].ToString()) : DateTime.MinValue;
                    consignee.WindowEndTime = !_consignee.IsNull("WindowEndTime") ? DateTime.Parse(_consignee["WindowEndTime"].ToString()) : DateTime.MinValue;
                    consignee.ContactName = _consignee["ContactName"].ToString();
                    consignee.ContactPhone = !_consignee.IsNull("ContactPhone") ? _consignee["ContactPhone"].ToString() : "";
                    consignee.ContactEmail = _consignee["ContactEmail"].ToString();
                    consignee.Status = _consignee["Status"].ToString();
                    consignee.LastUpdated = DateTime.Parse(_consignee["LastUpdated"].ToString());
                    consignee.UserID = _consignee["UserID"].ToString();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return consignee;
        }
        public bool UpdateLTLConsignee(LTLConsignee consignee) {
            //Update an existing LTL consignee
            bool updated=false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new LTLGateway().UpdateLTLConsignee(consignee);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }

        public DataSet ViewLTLShipments(int clientID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ViewLTLShipments(clientID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public int CreateLTLShipment(LTLShipment shipment) {
            //Add a new LTL shipment
            int shipmentID = 0;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Create the shipment
                    LTLGateway gateway = new LTLGateway();
                    shipmentID = gateway.CreateLTLShipment(shipment);
                    shipment.ID = shipmentID;

                    //Create the pallets
                    LTLPallet pallet = new LTLPallet();
                    pallet.ShipmentID = shipmentID;
                    if (shipment.Pallet1Weight > 0) { pallet.Weight = shipment.Pallet1Weight; pallet.InsuranceValue = shipment.Pallet1InsuranceValue; gateway.CreateLTLPallet(pallet); }
                    if (shipment.Pallet2Weight > 0) { pallet.Weight = shipment.Pallet2Weight; pallet.InsuranceValue = shipment.Pallet2InsuranceValue; gateway.CreateLTLPallet(pallet); }
                    if (shipment.Pallet3Weight > 0) { pallet.Weight = shipment.Pallet3Weight; pallet.InsuranceValue = shipment.Pallet3InsuranceValue; gateway.CreateLTLPallet(pallet); }
                    if (shipment.Pallet4Weight > 0) { pallet.Weight = shipment.Pallet4Weight; pallet.InsuranceValue = shipment.Pallet4InsuranceValue; gateway.CreateLTLPallet(pallet); }
                    if (shipment.Pallet5Weight > 0) { pallet.Weight = shipment.Pallet5Weight; pallet.InsuranceValue = shipment.Pallet5InsuranceValue; gateway.CreateLTLPallet(pallet); }

                    //Schedule pickup request
                    LTLClient client = ReadLTLClient(shipment.ClientID);
                    LTLShipper shipper = ReadLTLShipper(shipment.ShipperID);
                    LTLConsignee consignee = ReadLTLConsignee(shipment.ConsigneeID);
                    //bool useBizTalk = bool.Parse(WebConfigurationManager.AppSettings["UseBizTalk"].ToString());
                    //if (useBizTalk) {
                    //    Argix.BizTalk.LTLShipment _shipment = new BizTalk.LTLShipment();
                    //    _shipment.ID = shipmentID;
                    //    _shipment.Created = shipment.Created;
                    //    _shipment.ShipDate = shipment.ShipDate;
                    //    _shipment.ShipmentNumber = shipment.ShipmentNumber;
                    //    _shipment.ClientID = shipment.ClientID;
                    //    _shipment.ClientNumber = client.Number;
                    //    _shipment.ClientName = client.Name;
                    //    _shipment.ShipperNumber = "";
                    //    _shipment.ShipperName = shipper.Name;
                    //    _shipment.ShipperAddress = shipper.AddressLine1 + "\n" + shipper.City + ", " + shipper.State + " " + shipper.Zip;
                    //    _shipment.ShipperContactName = shipper.ContactName;
                    //    _shipment.ShipperContactPhone = shipper.ContactPhone;
                    //    _shipment.ShipperWindowStartTime = shipper.WindowStartTime;
                    //    _shipment.ShipperWindowEndTime = shipper.WindowEndTime;
                    //    _shipment.ConsigneeID = shipment.ConsigneeID;
                    //    _shipment.Pallets = shipment.Pallets;
                    //    _shipment.Weight = int.Parse(shipment.Weight.ToString());
                    //    _shipment.PalletRate = shipment.PalletRate;
                    //    _shipment.FuelSurcharge = shipment.FuelSurcharge;
                    //    _shipment.AccessorialCharge = shipment.AccessorialCharge;
                    //    _shipment.InsuranceCharge = shipment.InsuranceCharge;
                    //    _shipment.TollCharge = shipment.TollCharge;
                    //    _shipment.TotalCharge = shipment.TotalCharge;
                    //    _shipment.LastUpdated = shipment.LastUpdated;
                    //    _shipment.UserID = shipment.UserID;
                    //    new Argix.BizTalk.BizTalkGateway().ScheduleLTLPickup(_shipment);
                    //}
                    //else {
                        //Direct to Pickup Log
                        PickupRequest request = new PickupRequest();
                        request.RequestID = 0;
                        request.ScheduleDate = shipment.ShipDate;
                        request.CallerName = "Online";
                        request.ClientNumber = client.Number;
                        request.Client = client.Name;
                        request.ShipperNumber = "";
                        request.Shipper = shipper.Name;
                        request.ShipperAddress = shipper.AddressLine1 + "\n" + shipper.City + ", " + shipper.State + " " + shipper.Zip;
                        request.ShipperPhone = shipper.ContactPhone;
                        request.WindowOpen = shipper.WindowStartTime.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowStartTime.ToString("HHmm")) : 0;
                        request.WindowClose = shipper.WindowEndTime.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowEndTime.ToString("HHmm")) : 0;
                        request.Amount = shipment.Pallets;
                        request.AmountType = "Pallets";
                        request.FreightType = "Tsort";
                        request.OrderType = "B";
                        request.Weight = int.Parse(shipment.Weight.ToString());
                        request.Comments = "";
                        request.IsTemplate = false;
                        request.Created = DateTime.Now;
                        request.CreateUserID = "PSP";
                        request.TerminalNumber = "";
                        request.Terminal = "";
                        request.LastUpdated = shipment.LastUpdated;
                        request.UserID = shipment.UserID;
                        int pickupID = new DispatchGateway().InsertPickupRequest3(request);

                        //Update shipment with PickupID
                        shipment.PickupID = pickupID;
                        new LTLGateway().DispatchLTLShipment(shipment);
                    //}

                    //Send email notification to customer
                    ServiceLocation location = ReadPickupLocation(shipper.Zip);
                    LTLClient salesRep = null;
                    if (client.SalesRepClientID > 0) salesRep = ReadLTLClient(client.SalesRepClientID);
                    new NotifyService().NotifyShipmentCreated(client,shipper,consignee,shipment,shipmentID,salesRep);
                    
                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return shipmentID;
        }
        public LTLShipment ReadLTLShipment(int shipmentID) {
            LTLShipment shipment = null;
            try {
                DataSet ds = new LTLGateway().ReadLTLShipment(shipmentID);
                if (ds != null && ds.Tables["LTLShipmentTable"] != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) {
                    DataRow _shipment = ds.Tables["LTLShipmentTable"].Rows[0];
                    shipment = new LTLShipment();
                    shipment.ID = int.Parse(_shipment["ID"].ToString());
                    shipment.ShipmentNumber = _shipment["ShipmentNumber"].ToString();
                    shipment.Created = DateTime.Parse(_shipment["Created"].ToString());
                    shipment.ShipDate = DateTime.Parse(_shipment["ShipDate"].ToString());
                    shipment.ClientID = int.Parse(_shipment["ClientID"].ToString());
                    shipment.ShipperID = int.Parse(_shipment["ShipperID"].ToString());
                    shipment.ConsigneeID = int.Parse(_shipment["ConsigneeID"].ToString());
                    shipment.Pallet1Weight = int.Parse(_shipment["Pallet1Weight"].ToString());
                    shipment.Pallet1Class = _shipment["Pallet1Class"].ToString();
                    shipment.Pallet1InsuranceValue = decimal.Parse(_shipment["Pallet1InsuranceValue"].ToString());
                    shipment.Pallet2Weight = int.Parse(_shipment["Pallet2Weight"].ToString());
                    shipment.Pallet2Class = _shipment["Pallet2Class"].ToString();
                    shipment.Pallet2InsuranceValue = decimal.Parse(_shipment["Pallet2InsuranceValue"].ToString());
                    shipment.Pallet3Weight = int.Parse(_shipment["Pallet3Weight"].ToString());
                    shipment.Pallet3Class = _shipment["Pallet3Class"].ToString();
                    shipment.Pallet3InsuranceValue = decimal.Parse(_shipment["Pallet3InsuranceValue"].ToString());
                    shipment.Pallet4Weight = int.Parse(_shipment["Pallet4Weight"].ToString());
                    shipment.Pallet4Class = _shipment["Pallet4Class"].ToString();
                    shipment.Pallet4InsuranceValue = decimal.Parse(_shipment["Pallet4InsuranceValue"].ToString());
                    shipment.Pallet5Weight = int.Parse(_shipment["Pallet5Weight"].ToString());
                    shipment.Pallet5Class = _shipment["Pallet5Class"].ToString();
                    shipment.Pallet5InsuranceValue = decimal.Parse(_shipment["Pallet5InsuranceValue"].ToString());
                    shipment.InsidePickup = !_shipment.IsNull("InsidePickup") ? bool.Parse(_shipment["InsidePickup"].ToString()) : false;
                    shipment.LiftGateOrigin = !_shipment.IsNull("LiftGateOrigin") ? bool.Parse(_shipment["LiftGateOrigin"].ToString()) : false;
                    shipment.AppointmentOrigin = !_shipment.IsNull("AppointmentOrigin") ? DateTime.Parse(_shipment["AppointmentOrigin"].ToString()) : DateTime.MinValue;
                    shipment.InsideDelivery = !_shipment.IsNull("InsideDelivery") ? bool.Parse(_shipment["InsideDelivery"].ToString()) : false;
                    shipment.LiftGateDestination = !_shipment.IsNull("LiftGateDestination") ? bool.Parse(_shipment["LiftGateDestination"].ToString()) : false;
                    shipment.AppointmentDestination = !_shipment.IsNull("AppointmentDestination") ? DateTime.Parse(_shipment["AppointmentDestination"].ToString()) : DateTime.MinValue;
                    shipment.Pallets = int.Parse(_shipment["Pallets"].ToString());
                    shipment.Weight = int.Parse(_shipment["Weight"].ToString());
                    shipment.PalletRate = decimal.Parse(_shipment["PalletRate"].ToString());
                    shipment.FuelSurcharge = decimal.Parse(_shipment["FuelSurcharge"].ToString());
                    shipment.AccessorialCharge = decimal.Parse(_shipment["AccessorialCharge"].ToString());
                    shipment.InsuranceCharge = decimal.Parse(_shipment["InsuranceCharge"].ToString());
                    shipment.TollCharge = decimal.Parse(_shipment["TollCharge"].ToString());
                    shipment.TotalCharge = decimal.Parse(_shipment["TotalCharge"].ToString());
                    shipment.LastUpdated = DateTime.Parse(_shipment["LastUpdated"].ToString());
                    shipment.UserID = _shipment["UserID"].ToString();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return shipment;
        }
        public bool UpdateLTLShipment(LTLShipment shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new LTLGateway().UpdateLTLShipment(shipment);

                    //Update the pallets
                    throw new ApplicationException("TODO: Update shipment pallets.");

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }
        public bool CancelLTLShipment(int shipmentID) {
            //Cancel an existing LTL shipment
            bool updated = false;
            try {
                //Apply simple business rules
                //TODO: Verify pickup is not dispatched??

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    updated = new LTLGateway().CancelLTLShipment(shipmentID);

                    //Update the pallets
                    throw new ApplicationException("TODO: Cancel shipment pallets.");

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }

        public ServiceLocation ReadServiceLocation(string zipCode) {
            ServiceLocation location = null;
            try {
                DataSet ds = new LTLGateway().ReadServiceLocation(zipCode);
                if (ds != null && ds.Tables["ServiceLocationTable"].Rows.Count > 0) {
                    location = new ServiceLocation();
                    location.ZipCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zip"].ToString();
                    location.City = ds.Tables["ServiceLocationTable"].Rows[0]["City"].ToString();
                    location.State = ds.Tables["ServiceLocationTable"].Rows[0]["State"].ToString();
                    location.ZoneCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zone"].ToString();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return location;
        }
        public ServiceLocation ReadPickupLocation(string zipCode) {
            ServiceLocation location = null;
            try {
                DataSet ds = new LTLGateway().ReadPickupLocation(zipCode);
                if (ds != null && ds.Tables["ServiceLocationTable"].Rows.Count > 0) {
                    location = new ServiceLocation();
                    location.ZipCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zip"].ToString();
                    location.City = ds.Tables["ServiceLocationTable"].Rows[0]["City"].ToString();
                    location.State = ds.Tables["ServiceLocationTable"].Rows[0]["State"].ToString();
                    location.ZoneCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zone"].ToString();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return location;
        }
        #endregion

        #region ILTLAdminService
        public DataSet ViewLTLClients() {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ViewLTLClients();
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public LTLClient GetLTLClient(int clientID) {
            return ReadLTLClient(clientID);
        }
        //[PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool ApproveLTLClient(int clientID,string clientNumber,bool approve,string username) {
            //Approve a new LTL client
            bool approved = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    approved = new LTLGateway().ApproveLTLClient(clientID,clientNumber,approve,username);

                    //Send email notification to customer
                    LTLClient client = ReadLTLClient(clientID);
                    LTLClient salesRep = null;
                    if(client.SalesRepClientID > 0) salesRep = ReadLTLClient(client.SalesRepClientID);
                    new NotifyService().NotifyClientApproval(client,approve,salesRep);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (FaultException<LTLFault> lfe) { throw new FaultException<LTLFault>(new LTLFault(lfe.Detail.Message),"Internal Error"); }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return approved;
        }

        public DataSet ReadPalletLabels(int shipmentID) {
            DataSet ds = null;
            try {
                ds = new LTLGateway().ReadPalletLabels(shipmentID);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        #endregion
    }
}
