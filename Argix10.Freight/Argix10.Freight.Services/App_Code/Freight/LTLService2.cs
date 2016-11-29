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
    public class LTLService2 : ILTLService2, ILTLAdminService2, ILTLClientService2, ILTLLoadTenderService2 {
        //Members

        //Interface
        public LTLService2() { }

        #region ILTLService2
        public ServiceInfo GetServiceInfo() {
            //Get service information
            return new Argix.AppService(LTLGateway2.SQL_CONNID).GetServiceInfo();
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get configuration data for the specified application and usernames
            return new Argix.AppService(LTLGateway2.SQL_CONNID).GetUserConfiguration(application,usernames);
        }
        public void WriteLogEntry(TraceMessage m) {
            //Write o to database log if event level is severe enough
            new Argix.AppService(EnterpriseGateway.SQL_CONNID).WriteLogEntry(m);
        }
        #endregion

        #region ILTLClientService2
        public LTLQuote2 CreateQuote(LTLQuote2 quote) {
            //Create and log the quote
            try {
                //For existing clients: determne zip codes from shipper/consignee
                LTLClient2 client = null;
                if (quote.ClientID > 0) client = ReadLTLClient(quote.ClientID);
                if(quote.ShipperNumber != null && quote.ShipperNumber.Trim().Length > 0) quote.OriginZip = ReadLTLShipper(quote.ShipperNumber).Zip;
                if (quote.ConsigneeNumber > 0) quote.DestinationZip = ReadLTLConsignee(quote.ConsigneeNumber,client.Number).Zip;

                //Create the quote
                quote.Pallets = 1;
                quote.Weight = quote.Pallet1Weight;
                if (quote.Pallet2Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet2Weight; }
                if (quote.Pallet3Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet3Weight; }
                if (quote.Pallet4Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet4Weight; }
                if (quote.Pallet5Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet5Weight; }
                quote.PalletRate = quote.FuelSurcharge = quote.AccessorialCharge = quote.InsuranceCharge = quote.TollCharge = quote.TotalCharge = 0.0M;

                //Get quote
                DataSet ds = new LTLGateway2().GetQuote(quote);
                quote.PalletRate = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["PalletDeliveryCharge"].ToString());
                quote.FuelSurcharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["FuelSurcharge"].ToString());
                quote.InsidePickupCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsidePickupCharge"].ToString());
                quote.LiftGateOriginCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeOrigin"].ToString());
                quote.AppointmentOriginCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentOriginCharge"].ToString());
                quote.InsideDeliveryCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsideDeliveryCharge"].ToString());
                quote.SameDayPickupCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TheSameDayPickup"].ToString());
                quote.LiftGateDestinationCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeDestination"].ToString());
                quote.AppointmentDestinationCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentDestinationCharge"].ToString());
                quote.AccessorialCharge = quote.InsidePickupCharge + quote.LiftGateOriginCharge + quote.AppointmentOriginCharge + quote.SameDayPickupCharge + quote.InsideDeliveryCharge + quote.LiftGateDestinationCharge + quote.AppointmentDestinationCharge;
                quote.InsuranceCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["ShipmentInsuranceCharge"].ToString());
                quote.TollCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TollCharge"].ToString());
                quote.TotalCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TotalCharge"].ToString());
                quote.TransitMin = quote.TransitMax = 0;
                quote.EstimatedDeliveryDate = !ds.Tables["LTLQuoteTable"].Rows[0].IsNull("EstimatedDeliveryDate") ? DateTime.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["EstimatedDeliveryDate"].ToString()) : DateTime.MinValue;

                //Log the quote
                new LTLGateway2().CreateQuoteLogEntry(quote);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return quote;
        }

        public int CreateLTLClient(LTLClient2 client) {
            //Add a new LTL client
            int id = 0;
            try {
                //Apply simple business rules
                client.ContactPhone = client.ContactPhone.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Create the client
                    id = new LTLGateway2().CreateLTLClient(client);

                    //Send email notification to Finance about a new cient
                    client.ID = id;
                    new NotifyService().NotifyClientCreated2(client);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return id;
        }
        public LTLClient2 ReadLTLClient(int clientID) {
            LTLClient2 client = null;
            try {
                DataSet ds = new LTLGateway2().ReadLTLClient(clientID);
                if (ds != null && ds.Tables["LTLClientTable"] != null && ds.Tables["LTLClientTable"].Rows.Count > 0) {
                    DataRow _client = ds.Tables["LTLClientTable"].Rows[0];
                    client = new LTLClient2();
                    client.ID = int.Parse(_client["ID"].ToString());
                    client.Number = !_client.IsNull("ClientNumber") ? _client["ClientNumber"].ToString() : "";
                    client.Name = _client["Name"].ToString().Trim();
                    client.AddressLine1 = _client["AddressLine1"].ToString().Trim();
                    client.AddressLine2 = _client["AddressLine2"].ToString().Trim();
                    client.City = _client["City"].ToString().Trim();
                    client.State = _client["State"].ToString().Trim();
                    client.Zip = _client["Zip"].ToString().Trim();
                    client.Zip4 = _client["Zip4"].ToString().Trim();
                    client.ContactName = _client["ContactName"].ToString().Trim();
                    client.ContactPhone = _client["ContactPhone"].ToString().Trim();
                    client.ContactEmail = _client["ContactEmail"].ToString().Trim();
                    client.CorporateName = _client["CorporateName"].ToString().Trim();
                    client.CorporateAddressLine1 = _client["CorporateAddressLine1"].ToString().Trim();
                    client.CorporateAddressLine2 = _client["CorporateAddressLine2"].ToString().Trim();
                    client.CorporateCity = _client["CorporateCity"].ToString().Trim();
                    client.CorporateState = _client["CorporateState"].ToString().Trim();
                    client.CorporateZip = _client["CorporateZip"].ToString().Trim();
                    client.CorporateZip4 = _client["CorporateZip4"].ToString().Trim();
                    client.TaxID = _client["TaxID"].ToString().Trim();
                    client.BillingAddressLine1 = _client["BillingAddressLine1"].ToString().Trim();
                    client.BillingAddressLine2 = _client["BillingAddressLine2"].ToString().Trim();
                    client.BillingCity = _client["BillingCity"].ToString().Trim();
                    client.BillingState = _client["BillingState"].ToString().Trim();
                    client.BillingZip = _client["BillingZip"].ToString().Trim();
                    client.BillingZip4 = _client["BillingZip4"].ToString().Trim();
                    client.ApprovalDate = !_client.IsNull("ApprovalDate") ? DateTime.Parse(_client["ApprovalDate"].ToString()) : DateTime.MinValue;
                    client.ApprovalUser = !_client.IsNull("ApprovalUser") ? _client["ApprovalUser"].ToString() : "";
                    client.DenialDate = !_client.IsNull("DenialDate") ? DateTime.Parse(_client["DenialDate"].ToString()) : DateTime.MinValue;
                    client.DenialUser = !_client.IsNull("DenialUser") ? _client["DenialUser"].ToString() : "";
                    client.IsActive = !_client.IsNull("IsActive") ? int.Parse(_client["IsActive"].ToString()) : 0;
                    client.LastUpdated = DateTime.MinValue; // !_client.IsNull("LastUpdated") ? DateTime.Parse(_client["LastUpdated"].ToString()) : DateTime.MinValue;
                    client.UserID = "";                     // !_client.IsNull("UserID") ? _client["UserID"].ToString() : "";
                    client.SalesRepClientNumber = !_client.IsNull("SalesRepClientNumber") ? _client["SalesRepClientNumber"].ToString().Trim() : "";
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return client;
        }
        public LTLClient2 ReadLTLClient(string clientNumber) {
            LTLClient2 client = null;
            try {
                DataSet ds = new LTLGateway2().ReadLTLClient(clientNumber);
                if (ds != null && ds.Tables["LTLClientTable"] != null && ds.Tables["LTLClientTable"].Rows.Count > 0) {
                    DataRow _client = ds.Tables["LTLClientTable"].Rows[0];
                    client = new LTLClient2();
                    client.ID = int.Parse(_client["ID"].ToString());
                    client.Number = !_client.IsNull("ClientNumber") ? _client["ClientNumber"].ToString() : "";
                    client.Name = _client["Name"].ToString().Trim();
                    client.AddressLine1 = _client["AddressLine1"].ToString().Trim();
                    client.AddressLine2 = _client["AddressLine2"].ToString().Trim();
                    client.City = _client["City"].ToString().Trim();
                    client.State = _client["State"].ToString().Trim();
                    client.Zip = _client["Zip"].ToString().Trim();
                    client.Zip4 = _client["Zip4"].ToString().Trim();
                    client.ContactName = _client["ContactName"].ToString().Trim();
                    client.ContactPhone = _client["ContactPhone"].ToString().Trim();
                    client.ContactEmail = _client["ContactEmail"].ToString().Trim();
                    client.CorporateName = _client["CorporateName"].ToString().Trim();
                    client.CorporateAddressLine1 = _client["CorporateAddressLine1"].ToString().Trim();
                    client.CorporateAddressLine2 = _client["CorporateAddressLine2"].ToString().Trim();
                    client.CorporateCity = _client["CorporateCity"].ToString().Trim();
                    client.CorporateState = _client["CorporateState"].ToString().Trim();
                    client.CorporateZip = _client["CorporateZip"].ToString().Trim();
                    client.CorporateZip4 = _client["CorporateZip4"].ToString().Trim();
                    client.TaxID = _client["TaxID"].ToString().Trim();
                    client.BillingAddressLine1 = _client["BillingAddressLine1"].ToString().Trim();
                    client.BillingAddressLine2 = _client["BillingAddressLine2"].ToString().Trim();
                    client.BillingCity = _client["BillingCity"].ToString().Trim();
                    client.BillingState = _client["BillingState"].ToString().Trim();
                    client.BillingZip = _client["BillingZip"].ToString().Trim();
                    client.BillingZip4 = _client["BillingZip4"].ToString().Trim();
                    client.ApprovalDate = !_client.IsNull("ApprovalDate") ? DateTime.Parse(_client["ApprovalDate"].ToString()) : DateTime.MinValue;
                    client.ApprovalUser = !_client.IsNull("ApprovalUser") ? _client["ApprovalUser"].ToString() : "";
                    client.DenialDate = !_client.IsNull("DenialDate") ? DateTime.Parse(_client["DenialDate"].ToString()) : DateTime.MinValue;
                    client.DenialUser = !_client.IsNull("DenialUser") ? _client["DenialUser"].ToString() : "";
                    client.IsActive = !_client.IsNull("IsActive") ? int.Parse(_client["IsActive"].ToString()) : 0;
                    client.LastUpdated = DateTime.MinValue; // !_client.IsNull("LastUpdated") ? DateTime.Parse(_client["LastUpdated"].ToString()) : DateTime.MinValue;
                    client.UserID = "";                     // !_client.IsNull("UserID") ? _client["UserID"].ToString() : "";
                    client.SalesRepClientNumber = !_client.IsNull("SalesRepClientNumber") ? _client["SalesRepClientNumber"].ToString().Trim() : "";
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return client;
        }
        public bool UpdateLTLClient(LTLClient2 client) {
            //Update an existing LTL client
            bool updated = false;
            try {
                //Apply simple business rules
                client.ContactPhone = client.ContactPhone.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Update the client
                    updated = new LTLGateway2().UpdateLTLClient(client);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }
        public DataSet GetLTLClientList(string salesRepClientNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().GetLTLClientList(salesRepClientNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }

        public DataSet ViewLTLShippers(string clientNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ViewLTLShippers(clientNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public string CreateLTLShipper(LTLShipper2 shipper) {
            //Add a new LTL shipper
            string number="";
            try {
                //Apply simple business rules
                //Validate the shipper zip code is a pickup location for an Argix local terminal
                ServiceLocation location = ReadPickupLocation(shipper.Zip);
                if (location == null) throw new ApplicationException(shipper.Zip + " is currently not supported for pickup.");

                shipper.ContactPhone = shipper.ContactPhone.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Create the shipper
                    number = new LTLGateway2().CreateLTLShipper(shipper);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return number;
        }
        public DataSet ReadLTLShippersList(string clientNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ReadLTLShippersList(clientNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public LTLShipper2 ReadLTLShipper(string shipperNumber) {
            LTLShipper2 shipper = null;
            try {
                DataSet ds = new LTLGateway2().ReadLTLShipper(shipperNumber);
                if (ds != null && ds.Tables["LTLShipperTable"] != null && ds.Tables["LTLShipperTable"].Rows.Count > 0) {
                    DataRow _shipper = ds.Tables["LTLShipperTable"].Rows[0];
                    shipper = new LTLShipper2();
                    shipper.Number = _shipper["ShipperNumber"].ToString();
                    shipper.ClientNumber = _shipper["ClientNumber"].ToString();
                    shipper.Name = _shipper["Name"].ToString();
                    shipper.AddressLine1 = _shipper["AddressLine1"].ToString();
                    shipper.AddressLine2 = !_shipper.IsNull("AddressLine2") ? _shipper["AddressLine2"].ToString() : "";
                    shipper.City = _shipper["City"].ToString();
                    shipper.State = _shipper["State"].ToString();
                    shipper.Zip = _shipper["Zip"].ToString();
                    shipper.Zip4 = !_shipper.IsNull("Zip4") ? _shipper["Zip4"].ToString() : "";
                    shipper.WindowTimeStart = !_shipper.IsNull("WindowTimeStart") ? DateTime.Parse(_shipper["WindowTimeStart"].ToString()) : DateTime.MinValue;
                    shipper.WindowTimeEnd = !_shipper.IsNull("WindowTimeEnd") ? DateTime.Parse(_shipper["WindowTimeEnd"].ToString()) : DateTime.MinValue;
                    shipper.ContactName = _shipper["ContactName"].ToString();
                    shipper.ContactPhone = !_shipper.IsNull("ContactPhone") ? _shipper["ContactPhone"].ToString() : "";
                    shipper.ContactEmail = _shipper["ContactEmail"].ToString();
                    shipper.AgentNumber = _shipper["AgentNumber"].ToString();
                    shipper.AgentName = _shipper["AgentName"].ToString();
                    shipper.LastUpdated = DateTime.Parse(_shipper["LastUpdated"].ToString());
                    shipper.UserID = _shipper["UserID"].ToString();
                    shipper.Rowversion = (byte[])_shipper["Rowversion"];
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return shipper;
        }
        public bool UpdateLTLShipper(LTLShipper2 shipper) {
            //Update an existing LTL shipper
            bool updated=false;
            try {
                //Apply simple business rules
                shipper.ContactPhone = shipper.ContactPhone.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace("_","").Replace(" ","");

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Update the shipper
                    updated = new LTLGateway2().UpdateLTLShipper(shipper);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }

        public DataSet ViewLTLConsignees(string clientNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ViewLTLConsignees(clientNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public int CreateLTLConsignee(LTLConsignee2 consignee) {
            //Add a new LTL consignee
            int number = 0;
            try {
                //Apply simple business rules
                //Validate the consignee zip code is a delivery location by an Argix agent terminal
                ServiceLocation location = ReadServiceLocation(consignee.Zip);
                if (location == null) throw new ApplicationException(consignee.Zip + " is currently not supported for delivery.");

                consignee.ContactPhone = consignee.ContactPhone.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Create the consignee
                    number = new LTLGateway2().CreateLTLConsignee(consignee);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return number;
        }
        public DataSet ReadLTLConsigneesList(string clientNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ReadLTLConsigneesList(clientNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public LTLConsignee2 ReadLTLConsignee(int consigneeNumber,string clientNumber) {
            LTLConsignee2 consignee = null;
            try {
                DataSet ds = new LTLGateway2().ReadLTLConsignee(consigneeNumber,clientNumber);
                if (ds != null && ds.Tables["LTLConsigneeTable"] != null && ds.Tables["LTLConsigneeTable"].Rows.Count > 0) {
                    DataRow _consignee = ds.Tables["LTLConsigneeTable"].Rows[0];
                    consignee = new LTLConsignee2();
                    consignee.Number = int.Parse(_consignee["ConsigneeNumber"].ToString());
                    consignee.ClientNumber = _consignee["ClientNumber"].ToString();
                    consignee.Name = _consignee["Name"].ToString();
                    consignee.AddressLine1 = _consignee["AddressLine1"].ToString();
                    consignee.AddressLine2 = !_consignee.IsNull("AddressLine2") ? _consignee["AddressLine2"].ToString() : "";
                    consignee.City = _consignee["City"].ToString();
                    consignee.State = _consignee["State"].ToString();
                    consignee.Zip = _consignee["Zip"].ToString();
                    consignee.Zip4 = !_consignee.IsNull("Zip4") ? _consignee["Zip4"].ToString() : "";
                    consignee.WindowTimeStart = !_consignee.IsNull("WindowTimeStart") ? DateTime.Parse(_consignee["WindowTimeStart"].ToString()) : DateTime.MinValue;
                    consignee.WindowTimeEnd = !_consignee.IsNull("WindowTimeEnd") ? DateTime.Parse(_consignee["WindowTimeEnd"].ToString()) : DateTime.MinValue;
                    consignee.ContactName = _consignee["ContactName"].ToString();
                    consignee.ContactPhone = !_consignee.IsNull("ContactPhone") ? _consignee["ContactPhone"].ToString() : "";
                    consignee.ContactEmail = _consignee["ContactEmail"].ToString();
                    //consignee.AgentNumber = _shipper["AgentNumber"].ToString();
                    //consignee.AgentName = _shipper["AgentName"].ToString();
                    consignee.LastUpdated = DateTime.Parse(_consignee["LastUpdated"].ToString());
                    consignee.UserID = _consignee["UserID"].ToString();
                    consignee.Rowversion = (byte[])_consignee["Rowversion"];
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return consignee;
        }
        public bool UpdateLTLConsignee(LTLConsignee2 consignee) {
            //Update an existing LTL consignee
            bool updated=false;
            try {
                //Apply simple business rules
                consignee.ContactPhone = consignee.ContactPhone.Trim().Replace("-", "").Replace("(", "").Replace(")", "").Replace("_", "").Replace(" ", "");

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Update the consignee
                    updated = new LTLGateway2().UpdateLTLConsignee(consignee);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }

        public DataSet ViewLTLShipments(string clientNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ViewLTLShipments(clientNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet SearchLTLShipments(LTLSearch2 criteria) {
            //
            DataSet ds = null;
            try {
                ds = new LTLGateway2().SearchLTLShipments(criteria);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return ds;
        }
        public string CreateLTLShipment(LTLShipment2 shipment) {
            //Create a new LTL shipment
            string number="";
            try {
                //Apply simple business rules (if applicable)
                LTLClient2 client = ReadLTLClient(shipment.ClientNumber);
                LTLShipper2 shipper = ReadLTLShipper(shipment.ShipperNumber);
                LTLConsignee2 consignee = ReadLTLConsignee(shipment.ConsigneeNumber,shipment.ClientNumber);
                ServiceLocation location = ReadPickupLocation(shipper.Zip);
                LTLClient2 salesRep = null;
                if (client.SalesRepClientNumber.Trim().Length > 0) salesRep = ReadLTLClient(client.SalesRepClientNumber);

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Create the shipment
                    decimal totalCharge = shipment.TotalCharge;
                    shipment.TotalCharge = 0;   //Not an override
                    number = new LTLGateway2().CreateLTLShipment(shipment);
                    shipment.ShipmentNumber = number;
                    shipment.TotalCharge = totalCharge;

                    //Schedule a pickup request for the new shipment
                    PickupRequest pickup = new PickupRequest();
                    #region Populate pickup
                    pickup.RequestID = 0;
                    pickup.ScheduleDate = shipment.ShipDate;
                    pickup.CallerName = shipment.UserID;
                    pickup.ClientNumber = client.Number;
                    pickup.Client = client.Name;
                    pickup.ShipperNumber = shipper.ClientNumber.Trim() + shipper.Number.Trim() + "VE";     //AcountID NOT shipper number
                    pickup.Shipper = shipper.Name;
                    pickup.ShipperAddress = shipper.AddressLine1.Trim() + "\r\n" + shipper.City.Trim() + ", " + shipper.State.Trim() + " " + shipper.Zip;
                    pickup.ShipperPhone = shipper.ContactPhone;
                    pickup.WindowOpen = shipper.WindowTimeStart.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeStart.ToString("HHmm")) : 0;
                    pickup.WindowClose = shipper.WindowTimeEnd.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeEnd.ToString("HHmm")) : 0;
                    pickup.Amount = shipment.Pallets;
                    pickup.AmountType = "Pallets";
                    pickup.FreightType = "Tsort";
                    pickup.OrderType = "B";
                    pickup.Weight = int.Parse(shipment.Weight.ToString());
                    pickup.Comments = "";
                    pickup.IsTemplate = false;
                    pickup.Created = DateTime.Now;
                    pickup.CreateUserID = "PSP";
                    pickup.TerminalNumber = shipper.AgentNumber;
                    pickup.Terminal = shipper.AgentName;
                    pickup.LastUpdated = shipment.LastUpdated;
                    pickup.UserID = shipment.UserID;
                    #endregion
                    int pickupID = new DispatchGateway().InsertPickupRequest3(pickup);

                    //Update the shipment with the pickupID
                    shipment.PickupID = pickupID;
                    shipment.PickupDate = DateTime.MinValue;        //No pickup yet
                    new LTLGateway2().DispatchLTLShipment(shipment);

                    //Send email notification to customer
                    new NotifyService().NotifyShipmentCreated2(client,shipper,consignee,shipment,number,salesRep);
                    
                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { 
                throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); 
            }
            return number;
        }
        public LTLShipment2 ReadLTLShipment(string shipmentNumber) {
            LTLShipment2 shipment = null;
            try {
                DataSet ds = new LTLGateway2().ReadLTLShipment(shipmentNumber);
                if (ds != null && ds.Tables["LTLShipmentTable"] != null && ds.Tables["LTLShipmentTable"].Rows.Count > 0) {
                    DataRow _shipment = ds.Tables["LTLShipmentTable"].Rows[0];
                    shipment = new LTLShipment2();
                    shipment.ShipmentNumber = _shipment["ShipmentNumber"].ToString();
                    shipment.BLNumber = !_shipment.IsNull("BLNumber") ? _shipment["BLNumber"].ToString() : "";
                    shipment.ShipDate = DateTime.Parse(_shipment["ShipDate"].ToString());
                    shipment.ClientNumber = _shipment["ClientNumber"].ToString();
                    shipment.ClientName = !_shipment.IsNull("ClientName") ? _shipment["ClientName"].ToString() : "";
                    shipment.ShipperNumber = _shipment["ShipperNumber"].ToString();
                    shipment.ShipperName = !_shipment.IsNull("ShipperName") ? _shipment["ShipperName"].ToString() : "";
                    shipment.ConsigneeNumber = int.Parse(_shipment["ConsigneeNumber"].ToString());
                    shipment.ConsigneeName = !_shipment.IsNull("ConsigneeName") ? _shipment["ConsigneeName"].ToString() : "";
                    shipment.ContactName = !_shipment.IsNull("ContactName") ? _shipment["ContactName"].ToString() : "";
                    shipment.ContactPhone = !_shipment.IsNull("ContactPhone") ? _shipment["ContactPhone"].ToString() : "";

                    if (ds.Tables["Table1"] != null && ds.Tables["Table1"].Rows.Count > 0) {
                        shipment.Items = new LTLPallets2();
                        LTLPallet2 item = new LTLPallet2();
                        item.TrackingNumber = long.Parse(ds.Tables["Table1"].Rows[0]["TrackingNumber"].ToString());
                        item.ItemNumber = ds.Tables["Table1"].Rows[0].IsNull("ItemNumber") ? ds.Tables["Table1"].Rows[0]["ItemNumber"].ToString() : "";
                        item.PONumber = !ds.Tables["Table1"].Rows[0].IsNull("PONumber") ? ds.Tables["Table1"].Rows[0]["PONumber"].ToString() : "";
                        item.ReferenceNumber = !ds.Tables["Table1"].Rows[0].IsNull("ReferenceNumber") ? ds.Tables["Table1"].Rows[0]["ReferenceNumber"].ToString() : "";
                        item.Weight = shipment.Pallet1Weight = decimal.Parse(ds.Tables["Table1"].Rows[0]["Weight"].ToString());
                        item.InsuranceValue = shipment.Pallet1InsuranceValue = !ds.Tables["Table1"].Rows[0].IsNull("InsuranceValue") ? decimal.Parse(ds.Tables["Table1"].Rows[0]["InsuranceValue"].ToString()) : 0m;
                        item.ShipmentNumber = ds.Tables["Table1"].Rows[0]["ShipmentNumber"].ToString();
                        shipment.Items.Add(item);
                        if (ds.Tables["Table1"].Rows.Count > 1) {
                            item = new LTLPallet2();
                            item.TrackingNumber = long.Parse(ds.Tables["Table1"].Rows[1]["TrackingNumber"].ToString());
                            item.ItemNumber = ds.Tables["Table1"].Rows[1].IsNull("ItemNumber") ? ds.Tables["Table1"].Rows[1]["ItemNumber"].ToString() : "";
                            item.PONumber = !ds.Tables["Table1"].Rows[1].IsNull("PONumber") ? ds.Tables["Table1"].Rows[1]["PONumber"].ToString() : "";
                            item.ReferenceNumber = !ds.Tables["Table1"].Rows[1].IsNull("ReferenceNumber") ? ds.Tables["Table1"].Rows[1]["ReferenceNumber"].ToString() : "";
                            item.Weight = shipment.Pallet2Weight = decimal.Parse(ds.Tables["Table1"].Rows[1]["Weight"].ToString());
                            item.InsuranceValue = shipment.Pallet2InsuranceValue = !ds.Tables["Table1"].Rows[1].IsNull("InsuranceValue") ? decimal.Parse(ds.Tables["Table1"].Rows[1]["InsuranceValue"].ToString()) : 0m;
                            item.ShipmentNumber = ds.Tables["Table1"].Rows[1]["ShipmentNumber"].ToString();
                            shipment.Items.Add(item);
                        }
                        if (ds.Tables["Table1"].Rows.Count > 2) {
                            item = new LTLPallet2();
                            item.TrackingNumber = long.Parse(ds.Tables["Table1"].Rows[2]["TrackingNumber"].ToString());
                            item.ItemNumber = ds.Tables["Table1"].Rows[2].IsNull("ItemNumber") ? ds.Tables["Table1"].Rows[2]["ItemNumber"].ToString() : "";
                            item.PONumber = !ds.Tables["Table1"].Rows[2].IsNull("PONumber") ? ds.Tables["Table1"].Rows[2]["PONumber"].ToString() : "";
                            item.ReferenceNumber = !ds.Tables["Table1"].Rows[2].IsNull("ReferenceNumber") ? ds.Tables["Table1"].Rows[2]["ReferenceNumber"].ToString() : "";
                            item.Weight = shipment.Pallet3Weight = decimal.Parse(ds.Tables["Table1"].Rows[2]["Weight"].ToString());
                            item.InsuranceValue = shipment.Pallet3InsuranceValue = !ds.Tables["Table1"].Rows[2].IsNull("InsuranceValue") ? decimal.Parse(ds.Tables["Table1"].Rows[2]["InsuranceValue"].ToString()) : 0m;
                            item.ShipmentNumber = ds.Tables["Table1"].Rows[2]["ShipmentNumber"].ToString();
                            shipment.Items.Add(item);
                        }
                        if (ds.Tables["Table1"].Rows.Count > 3) {
                            item = new LTLPallet2();
                            item.TrackingNumber = long.Parse(ds.Tables["Table1"].Rows[3]["TrackingNumber"].ToString());
                            item.ItemNumber = ds.Tables["Table1"].Rows[3].IsNull("ItemNumber") ? ds.Tables["Table1"].Rows[3]["ItemNumber"].ToString() : "";
                            item.PONumber = !ds.Tables["Table1"].Rows[3].IsNull("PONumber") ? ds.Tables["Table1"].Rows[3]["PONumber"].ToString() : "";
                            item.ReferenceNumber = !ds.Tables["Table1"].Rows[3].IsNull("ReferenceNumber") ? ds.Tables["Table1"].Rows[3]["ReferenceNumber"].ToString() : "";
                            item.Weight = shipment.Pallet4Weight = decimal.Parse(ds.Tables["Table1"].Rows[3]["Weight"].ToString());
                            item.InsuranceValue = shipment.Pallet4InsuranceValue = !ds.Tables["Table1"].Rows[3].IsNull("InsuranceValue") ? decimal.Parse(ds.Tables["Table1"].Rows[3]["InsuranceValue"].ToString()) : 0m;
                            item.ShipmentNumber = ds.Tables["Table1"].Rows[3]["ShipmentNumber"].ToString();
                            shipment.Items.Add(item);
                        }
                        if (ds.Tables["Table1"].Rows.Count > 4) {
                            item = new LTLPallet2();
                            item.TrackingNumber = long.Parse(ds.Tables["Table1"].Rows[4]["TrackingNumber"].ToString());
                            item.ItemNumber = ds.Tables["Table1"].Rows[4].IsNull("ItemNumber") ? ds.Tables["Table1"].Rows[4]["ItemNumber"].ToString() : "";
                            item.PONumber = !ds.Tables["Table1"].Rows[4].IsNull("PONumber") ? ds.Tables["Table1"].Rows[4]["PONumber"].ToString() : "";
                            item.ReferenceNumber = !ds.Tables["Table1"].Rows[4].IsNull("ReferenceNumber") ? ds.Tables["Table1"].Rows[4]["ReferenceNumber"].ToString() : "";
                            item.Weight = shipment.Pallet5Weight = decimal.Parse(ds.Tables["Table1"].Rows[4]["Weight"].ToString());
                            item.InsuranceValue = shipment.Pallet5InsuranceValue = !ds.Tables["Table1"].Rows[4].IsNull("InsuranceValue") ? decimal.Parse(ds.Tables["Table1"].Rows[4]["InsuranceValue"].ToString()) : 0m;
                            item.ShipmentNumber = ds.Tables["Table1"].Rows[4]["ShipmentNumber"].ToString();
                            shipment.Items.Add(item);
                        }
                    }
                    shipment.InsidePickup = !_shipment.IsNull("InsidePickup") ? bool.Parse(_shipment["InsidePickup"].ToString()) : false;
                    shipment.LiftGateOrigin = !_shipment.IsNull("LiftGateOrigin") ? bool.Parse(_shipment["LiftGateOrigin"].ToString()) : false;
                    shipment.PickupAppointmentDate = !_shipment.IsNull("PickupAppointmentDate") ? DateTime.Parse(_shipment["PickupAppointmentDate"].ToString()) : DateTime.MinValue;
                    shipment.PickupAppointmentWindowTimeStart = !_shipment.IsNull("PickupAppointmentWindowTimeStart") ? DateTime.Parse(_shipment["PickupAppointmentWindowTimeStart"].ToString()) : DateTime.MinValue;
                    shipment.PickupAppointmentWindowTimeEnd = !_shipment.IsNull("PickupAppointmentWindowTimeEnd") ? DateTime.Parse(_shipment["PickupAppointmentWindowTimeEnd"].ToString()) : DateTime.MinValue;
                    shipment.SameDayPickup = !_shipment.IsNull("TheSameDayPickup") ? bool.Parse(_shipment["TheSameDayPickup"].ToString()) : false;
                    shipment.InsideDelivery = !_shipment.IsNull("InsideDelivery") ? bool.Parse(_shipment["InsideDelivery"].ToString()) : false;
                    shipment.LiftGateDestination = !_shipment.IsNull("LiftGateDestination") ? bool.Parse(_shipment["LiftGateDestination"].ToString()) : false;
                    shipment.DeliveryAppointmentDate = !_shipment.IsNull("DeliveryAppointmentDate") ? DateTime.Parse(_shipment["DeliveryAppointmentDate"].ToString()) : DateTime.MinValue;
                    shipment.DeliveryAppointmentWindowTimeStart = !_shipment.IsNull("DeliveryAppointmentWindowTimeStart") ? DateTime.Parse(_shipment["DeliveryAppointmentWindowTimeStart"].ToString()) : DateTime.MinValue;
                    shipment.DeliveryAppointmentWindowTimeEnd = !_shipment.IsNull("DeliveryAppointmentWindowTimeEnd") ? DateTime.Parse(_shipment["DeliveryAppointmentWindowTimeEnd"].ToString()) : DateTime.MinValue;
                    shipment.Pallets = !_shipment.IsNull("Pallets") ? int.Parse(_shipment["Pallets"].ToString()) : 0;
                    shipment.Weight = !_shipment.IsNull("Weight") ? decimal.Parse(_shipment["Weight"].ToString()) : 0m;
                    shipment.PalletRate = !_shipment.IsNull("PalletRate") ? decimal.Parse(_shipment["PalletRate"].ToString()) : 0m;
                    shipment.FuelSurcharge = !_shipment.IsNull("FuelSurcharge") ? decimal.Parse(_shipment["FuelSurcharge"].ToString()) : 0m;
                    shipment.AccessorialCharge = !_shipment.IsNull("AccessorialCharge") ? decimal.Parse(_shipment["AccessorialCharge"].ToString()) : 0m;
                    shipment.InsuranceCharge = !_shipment.IsNull("InsuranceCharge") ? decimal.Parse(_shipment["InsuranceCharge"].ToString()) : 0m;
                    shipment.TollCharge = !_shipment.IsNull("TollCharge") ? decimal.Parse(_shipment["TollCharge"].ToString()) : 0m;
                    shipment.TotalCharge = !_shipment.IsNull("TotalCharge") ? decimal.Parse(_shipment["TotalCharge"].ToString()) : 0m;
                    shipment.TerminalCode = !_shipment.IsNull("TerminalCode") ? _shipment["TerminalCode"].ToString() : "";
                    shipment.LTLZone = !_shipment.IsNull("LTLZone") ? decimal.Parse(_shipment["LTLZone"].ToString()) : 0m;
                    shipment.Created = DateTime.Parse(_shipment["Created"].ToString());
                    shipment.CreatedUserID = !_shipment.IsNull("CreatedUserID") ? _shipment["CreatedUserID"].ToString() : "";
                    shipment.PickupID = !_shipment.IsNull("PickupID") ? int.Parse(_shipment["PickupID"].ToString()) : 0;
                    shipment.PickupDate = !_shipment.IsNull("PickupDate") ? DateTime.Parse(_shipment["PickupDate"].ToString()) : DateTime.MinValue;
                    shipment.Cancelled = !_shipment.IsNull("Cancelled") ? DateTime.Parse(_shipment["Cancelled"].ToString()) : DateTime.MinValue;
                    //shipment.LastUpdated = !_shipment.IsNull("LastUpdated") ? DateTime.Parse(_shipment["LastUpdated"].ToString()) : DateTime.MinValue;
                    //shipment.UserID = !_shipment.IsNull("UserID") ? _shipment["UserID"].ToString() : "";
                    shipment.Rowversion = (byte[])_shipment["Rowversion"];
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return shipment;
        }
        public bool UpdateLTLShipment(LTLShipment2 shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                //Apply simple business rules
                //Cannot cancel if day of pickup
                if (shipment.ShipDate.CompareTo(DateTime.Today) <= 0) throw new ApplicationException("Shipments must be cancelled at least one day before the scheduled shipment date.");
                LTLClient2 client = ReadLTLClient(shipment.ClientNumber);
                LTLShipper2 shipper = ReadLTLShipper(shipment.ShipperNumber);
                LTLConsignee2 consignee = ReadLTLConsignee(shipment.ConsigneeNumber,shipment.ClientNumber);
                LTLClient2 salesRep = null;
                if(client.SalesRepClientNumber.Trim().Length > 0) salesRep = ReadLTLClient(client.SalesRepClientNumber);
                PickupRequest pickup = new FreightSystemService().ReadPickup(shipment.PickupID);

                //Execute the business transcation
                using (TransactionScope scope = new TransactionScope()) {
                    //Update the shipment
                    updated = new LTLGateway2().UpdateLTLShipment(shipment);

                    //Update the pickup request for the shipment
                    if (pickup != null) {
                        #region Populate pickup
                        pickup.ScheduleDate = shipment.ShipDate;
                        pickup.WindowOpen = shipper.WindowTimeStart.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeStart.ToString("HHmm")) : 0;
                        pickup.WindowClose = shipper.WindowTimeEnd.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeEnd.ToString("HHmm")) : 0;
                        pickup.Amount = shipment.Pallets;
                        pickup.Weight = int.Parse(shipment.Weight.ToString("#0"));
                        pickup.Comments = "Updated " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt");
                        pickup.TerminalNumber = shipper.AgentNumber;
                        pickup.Terminal = shipper.AgentName;
                        pickup.LastUpdated = shipment.LastUpdated;
                        pickup.UserID = shipment.UserID;
                        #endregion
                        new FreightSystemService().UpdatePickup(pickup);
                    }

                    //Send email notification to customer
                    new NotifyService().NotifyShipmentUpdated2(client,shipper,consignee,shipment,shipment.ShipmentNumber,salesRep);
                    
                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return updated;
        }
        public bool CancelLTLShipment(string shipmentNumber, string userID) {
            //Cancel an existing LTL shipment
            bool cancelled = false;
            try {
                //Apply simple business rules
                //Cannot cancel if day of pickup
                LTLShipment2 shipment = ReadLTLShipment(shipmentNumber);
                if (shipment.ShipDate.CompareTo(DateTime.Today) <= 0) throw new ApplicationException("Shipments must be cancelled at least one day before the scheduled shipment date.");
                
                LTLClient2 client = ReadLTLClient(shipment.ClientNumber);
                LTLClient2 salesRep = null;
                if(client.SalesRepClientNumber.Trim().Length > 0) salesRep = ReadLTLClient(client.SalesRepClientNumber);
                LTLShipper2 shipper = ReadLTLShipper(shipment.ShipperNumber);
                LTLConsignee2 consignee = ReadLTLConsignee(shipment.ConsigneeNumber,shipment.ClientNumber);

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //Cancel the shipment
                    cancelled = new LTLGateway2().CancelLTLShipment(shipmentNumber,userID);

                    //Cancel the pickup request
                    new DispatchGateway().CancelPickupRequest(shipment.PickupID,DateTime.Now,shipment.UserID);

                    //Send email notification to customer
                    new NotifyService().NotifyShipmentCancelled2(client,shipper,consignee,shipment,shipmentNumber,salesRep);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (Exception ex) {
                WriteLogEntry(new TraceMessage("System","",ex.Message,"PalletShipment",LogLevel.Error));
                throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); 
            }
            return cancelled;
        }

        public DataSet ReadPalletLabelData(string shipmentNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ReadPalletLabels(shipmentNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public DataSet ReadPalletBOLData(string shipmentNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ReadPalletBOL(shipmentNumber);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return ds;
        }
        #endregion

        #region ILTLAdminService2
        public LTLQuote2 CreateQuoteForAdmin(LTLQuote2 quote) {
            //Create the quote
            try {
                //For existing clients: determne zip codes from shipper/consignee
                LTLClient2 client = null;
                if(quote.ClientID > 0) client = ReadLTLClient(quote.ClientID);
                if(quote.ShipperNumber != null && quote.ShipperNumber.Trim().Length > 0) quote.OriginZip = ReadLTLShipper(quote.ShipperNumber).Zip;
                if(quote.ConsigneeNumber > 0) quote.DestinationZip = ReadLTLConsignee(quote.ConsigneeNumber, client.Number).Zip;

                //Create the quote
                quote.Pallets = 1;
                quote.Weight = quote.Pallet1Weight;
                if(quote.Pallet2Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet2Weight; }
                if(quote.Pallet3Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet3Weight; }
                if(quote.Pallet4Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet4Weight; }
                if(quote.Pallet5Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet5Weight; }
                quote.PalletRate = quote.FuelSurcharge = quote.AccessorialCharge = quote.InsuranceCharge = quote.TollCharge = 0.0M;
                //quote.TotalCharge = 0.0M;     Allow overrides to pass through

                //Get quote
                DataSet ds = new LTLGateway2().GetQuote(quote);
                quote.PalletRate = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["PalletDeliveryCharge"].ToString());
                quote.FuelSurcharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["FuelSurcharge"].ToString());
                quote.InsidePickupCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsidePickupCharge"].ToString());
                quote.LiftGateOriginCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeOrigin"].ToString());
                quote.AppointmentOriginCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentOriginCharge"].ToString());
                quote.InsideDeliveryCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsideDeliveryCharge"].ToString());
                quote.SameDayPickupCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TheSameDayPickup"].ToString());
                quote.LiftGateDestinationCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeDestination"].ToString());
                quote.AppointmentDestinationCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentDestinationCharge"].ToString());
                quote.AccessorialCharge = quote.InsidePickupCharge + quote.LiftGateOriginCharge + quote.AppointmentOriginCharge + quote.SameDayPickupCharge + quote.InsideDeliveryCharge + quote.LiftGateDestinationCharge + quote.AppointmentDestinationCharge;
                quote.InsuranceCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["ShipmentInsuranceCharge"].ToString());
                quote.TollCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TollCharge"].ToString());
                quote.TotalCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TotalCharge"].ToString());
                quote.TransitMin = quote.TransitMax = 0;
                quote.EstimatedDeliveryDate = !ds.Tables["LTLQuoteTable"].Rows[0].IsNull("EstimatedDeliveryDate") ? DateTime.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["EstimatedDeliveryDate"].ToString()) : DateTime.MinValue;
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return quote;
        }

        public DataSet ViewLTLClients() {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ViewLTLClients();
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        public int CreateLTLClientForAdmin(LTLClient2 client) { return CreateLTLClient(client); }
        public LTLClient2 GetLTLClient(int clientID) { return ReadLTLClient(clientID); }
        public bool UpdateLTLClientForAdmin(LTLClient2 client) { return UpdateLTLClient(client); }
        //[PrincipalPermission(SecurityAction.Demand,Role = "Billing Supervisor")]
        public bool ApproveLTLClient(int clientID,bool approve,string username) {
            //Approve a new LTL client
            bool approved = false;
            try {
                //Apply simple business rules

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using (TransactionScope scope = new TransactionScope()) {
                    //
                    approved = new LTLGateway2().ApproveLTLClient(clientID,approve,username);

                    //Send email notification to customer
                    LTLClient2 client = ReadLTLClient(clientID);
                    LTLClient2 salesRep = null;
                    if(client.SalesRepClientNumber.Trim().Length > 0) salesRep = ReadLTLClient(client.SalesRepClientNumber);
                    new NotifyService().NotifyClientApproval2(client,approve,salesRep);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch (FaultException<LTLFault> lfe) { throw new FaultException<LTLFault>(new LTLFault(lfe.Detail.Message),"Internal Error"); }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return approved;
        }
        public DataSet ReadLTLClientListForAdmin() {
            //Get a list of LTL clients
            DataSet ds = null;
            try {
                ds = new LTLGateway2().GetLTLClientList(null);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return ds;
        }

        public string CreateLTLShipperForAdmin(LTLShipper2 shipper) { return CreateLTLShipper(shipper); }
        public bool UpdateLTLShipperForAdmin(LTLShipper2 shipper) { return UpdateLTLShipper(shipper); }
        public DataSet ReadLTLShippersListForAdmin(string clientNumber) { return ReadLTLShippersList(clientNumber); }

        public int CreateLTLConsigneeForAdmin(LTLConsignee2 consignee) { return CreateLTLConsignee(consignee); }
        public bool UpdateLTLConsigneeForAdmin(LTLConsignee2 consignee) { return UpdateLTLConsignee(consignee); }
        public DataSet ReadLTLConsigneesListForAdmin(string clientNumber) { return ReadLTLConsigneesList(clientNumber); }

        //[PrincipalPermission(SecurityAction.Demand, Role = "Dispatch Supervisor")]
        public string CreateLTLShipmentForAdmin(LTLShipment2 shipment) {
            //Create a new LTL shipment
            string number = "";
            try {
                //Apply simple business rules (if applicable)
                LTLClient2 client = ReadLTLClient(shipment.ClientNumber);
                LTLShipper2 shipper = ReadLTLShipper(shipment.ShipperNumber);

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the shipment
                    decimal totalCharge = shipment.TotalCharge;
                    shipment.TotalCharge = 0;   //Not an override
                    number = new LTLGateway2().CreateLTLShipment(shipment);
                    shipment.ShipmentNumber = number;
                    shipment.TotalCharge = totalCharge;

                    //Schedule a pickup request for the new shipment if the ship date is valid 
                    //(i.e. admin needs to create a shipment after receiving freight so Dispatch not needed)
                    if(shipment.ShipDate.CompareTo(getNextValidShipDate()) >= 0) {
                        PickupRequest pickup = new PickupRequest();
                        #region Populate pickup
                        pickup.RequestID = 0;
                        pickup.ScheduleDate = shipment.ShipDate;
                        pickup.CallerName = shipment.UserID;
                        pickup.ClientNumber = client.Number;
                        pickup.Client = client.Name;
                        pickup.ShipperNumber = shipper.ClientNumber.Trim() + shipper.Number.Trim() + "VE";     //AcountID NOT shipper number
                        pickup.Shipper = shipper.Name;
                        pickup.ShipperAddress = shipper.AddressLine1.Trim() + "\r\n" + shipper.City.Trim() + ", " + shipper.State.Trim() + " " + shipper.Zip;
                        pickup.ShipperPhone = shipper.ContactPhone;
                        pickup.WindowOpen = shipper.WindowTimeStart.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeStart.ToString("HHmm")) : 0;
                        pickup.WindowClose = shipper.WindowTimeEnd.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeEnd.ToString("HHmm")) : 0;
                        pickup.Amount = shipment.Pallets;
                        pickup.AmountType = "Pallets";
                        pickup.FreightType = "Tsort";
                        pickup.OrderType = "B";
                        pickup.Weight = int.Parse(shipment.Weight.ToString());
                        pickup.Comments = "";
                        pickup.IsTemplate = false;
                        pickup.Created = DateTime.Now;
                        pickup.CreateUserID = "PSP";
                        pickup.TerminalNumber = shipper.AgentNumber;
                        pickup.Terminal = shipper.AgentName;
                        pickup.LastUpdated = shipment.LastUpdated;
                        pickup.UserID = shipment.UserID;
                        #endregion
                        int pickupID = new DispatchGateway().InsertPickupRequest3(pickup);

                        //Update the shipment with the pickupID
                        shipment.PickupID = pickupID;
                        shipment.PickupDate = DateTime.MinValue;
                        new LTLGateway2().DispatchLTLShipment(shipment);
                    }
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return number;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "Dispatch Supervisor")]
        public bool UpdateLTLShipmentForAdmin(LTLShipment2 shipment) {
            //Update an existing LTL shipment
            bool updated = false;
            try {
                //Apply simple business rules
                LTLClient2 client = ReadLTLClient(shipment.ClientNumber);
                LTLShipper2 shipper = ReadLTLShipper(shipment.ShipperNumber);
                PickupRequest pickup = null;
                DataSet ds = new DispatchGateway().GetPickupRequest(shipment.PickupID);
                if(ds != null) {
                    DispatchDataset _pickup = new DispatchDataset();
                    _pickup.Merge(ds);
                    if(_pickup.PickupLogTable.Rows.Count > 0) pickup = new PickupRequest(_pickup.PickupLogTable[0]);
                }

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Update the shipment
                    shipment.TotalCharge = 0;   //Not an override
                    updated = new LTLGateway2().UpdateLTLShipment(shipment);

                    //Update the pickup request for the shipment
                    if(pickup != null) {
                        #region Populate pickup
                        pickup.ScheduleDate = shipment.ShipDate;
                        pickup.WindowOpen = shipper.WindowTimeStart.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeStart.ToString("HHmm")) : 0;
                        pickup.WindowClose = shipper.WindowTimeEnd.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeEnd.ToString("HHmm")) : 0;
                        pickup.Amount = shipment.Pallets;
                        pickup.Weight = int.Parse(shipment.Weight.ToString("#0"));
                        pickup.Comments = "Updated " + DateTime.Now.ToString("MM/dd/yyyy HH:mm tt");
                        pickup.TerminalNumber = shipper.AgentNumber;
                        pickup.Terminal = shipper.AgentName;
                        pickup.LastUpdated = shipment.LastUpdated;
                        pickup.UserID = shipment.UserID;
                        #endregion
                        new DispatchGateway().UpdatePickupRequest(pickup);
                    }

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return updated;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "Dispatch Supervisor")]
        public bool CancelLTLShipmentForAdmin(string shipmentNumber, string userID) {
            //Cancel an existing LTL shipment
            bool cancelled = false;
            try {
                //Apply simple business rules
                LTLShipment2 shipment = ReadLTLShipment(shipmentNumber);
                if(shipment.PickupDate != DateTime.MinValue) throw new ApplicationException("This shipment has already been picked up.");

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //Cancel the shipment
                    cancelled = new LTLGateway2().CancelLTLShipment(shipmentNumber, userID);

                    //Cancel the pickup request
                    new DispatchGateway().CancelPickupRequest(shipment.PickupID, DateTime.Now, shipment.UserID);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return cancelled;
        }
        
        public DataSet ReadPalletLabels(string shipmentNumber) {
            DataSet ds = null;
            try {
                ds = new LTLGateway2().ReadPalletLabels(shipmentNumber);
            }
            catch (Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message),"Service Error"); }
            return ds;
        }
        #endregion

        #region ILTLLoadTenderService2
        public LTLQuote2 CreateQuoteForSalesRep(LTLQuote2 quote) {
            //Create the quote
            try {
                //For existing clients: determne zip codes from shipper/consignee
                LTLClient2 client = null;
                if(quote.ClientID > 0) client = ReadLTLClient(quote.ClientID);
                if(quote.ShipperNumber != null && quote.ShipperNumber.Trim().Length > 0) quote.OriginZip = ReadLTLShipper(quote.ShipperNumber).Zip;
                if(quote.ConsigneeNumber > 0) quote.DestinationZip = ReadLTLConsignee(quote.ConsigneeNumber, client.Number).Zip;

                //Create the quote
                quote.Pallets = 1;
                quote.Weight = quote.Pallet1Weight;
                if(quote.Pallet2Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet2Weight; }
                if(quote.Pallet3Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet3Weight; }
                if(quote.Pallet4Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet4Weight; }
                if(quote.Pallet5Weight > 0) { quote.Pallets++; quote.Weight += quote.Pallet5Weight; }
                quote.PalletRate = quote.FuelSurcharge = quote.AccessorialCharge = quote.InsuranceCharge = quote.TollCharge = 0.0M;
                //quote.TotalCharge = 0.0M;     Allow overrides to pass through

                //Get quote
                DataSet ds = new LTLGateway2().GetQuoteWithOverride(quote);
                quote.PalletRate = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["PalletDeliveryCharge"].ToString());
                quote.FuelSurcharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["FuelSurcharge"].ToString());
                quote.InsidePickupCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsidePickupCharge"].ToString());
                quote.LiftGateOriginCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeOrigin"].ToString());
                quote.AppointmentOriginCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentOriginCharge"].ToString());
                quote.InsideDeliveryCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["InsideDeliveryCharge"].ToString());
                quote.SameDayPickupCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TheSameDayPickup"].ToString());
                quote.LiftGateDestinationCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["LiftGateChargeDestination"].ToString());
                quote.AppointmentDestinationCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["AppointmentDestinationCharge"].ToString());
                quote.AccessorialCharge = quote.InsidePickupCharge + quote.LiftGateOriginCharge + quote.AppointmentOriginCharge + quote.SameDayPickupCharge + quote.InsideDeliveryCharge + quote.LiftGateDestinationCharge + quote.AppointmentDestinationCharge;
                quote.InsuranceCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["ShipmentInsuranceCharge"].ToString());
                quote.TollCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TollCharge"].ToString());
                quote.TotalCharge = decimal.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["TotalCharge"].ToString());
                quote.TransitMin = quote.TransitMax = 0;
                quote.EstimatedDeliveryDate = !ds.Tables["LTLQuoteTable"].Rows[0].IsNull("EstimatedDeliveryDate") ? DateTime.Parse(ds.Tables["LTLQuoteTable"].Rows[0]["EstimatedDeliveryDate"].ToString()) : DateTime.MinValue;
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return quote;
        }
        public DataSet ViewLoadTenderQuotes(string owner) {
            //
            DataSet quotes = null;
            try {
                quotes = new LTLGateway2().ViewLoadTenderQuotes(owner);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return quotes;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public int CreateLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Create a new LTLLoadTenderQuote
            int id = 0;
            try {
                //Apply simple business rules (if applicable)


                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the LoadTenderQuote
                    id = new LTLGateway2().CreateLoadTenderQuote(quote);

                    //Approve the quote if applicable
                    if(quote.Approved != DateTime.MinValue) new LTLGateway2().ApproveLoadTenderQuote(id, quote.Approved, quote.ApprovedBy);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return id;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public LTLLoadTenderQuote ReadLoadTenderQuote(int id) {
            //
            LTLLoadTenderQuote quote = new LTLLoadTenderQuote();
            quote.LTLQuote = new LTLQuote2();
            try {
                DataSet ds = new LTLGateway2().ReadLoadTenderQuote(id);
                if(ds != null && ds.Tables["LoadTenderQuoteTable"] != null && ds.Tables["LoadTenderQuoteTable"].Rows.Count > 0) {
                    DataRow _quote = ds.Tables["LoadTenderQuoteTable"].Rows[0];
                    #region Set fields
                    quote.ID = int.Parse(_quote["ID"].ToString());
                    quote.Owner = _quote["Owner"].ToString();
                    quote.Description = _quote["Description"].ToString();
                    quote.BrokerName = _quote["BrokerName"].ToString();
                    quote.ContactName = _quote["ContactName"].ToString();
                    quote.ContactEmail = _quote["ContactEmail"].ToString();
                    quote.ContactPhone = _quote["ContactPhone"].ToString();
                    quote.Comments = _quote["Comments"].ToString();
                    quote.LTLQuote.ShipDate = DateTime.Parse(_quote["ShipDate"].ToString());
                    quote.LTLQuote.OriginCity = _quote["OriginCity"].ToString();
                    quote.LTLQuote.OriginState = _quote["OriginState"].ToString();
                    quote.LTLQuote.OriginZip = _quote["OriginZip"].ToString();
                    quote.LTLQuote.InsidePickup = bool.Parse(_quote["InsidePickup"].ToString());
                    quote.LTLQuote.LiftGateOrigin = bool.Parse(_quote["LiftGateOrigin"].ToString());
                    quote.LTLQuote.AppointmentOrigin = bool.Parse(_quote["AppointmentOrigin"].ToString());
                    quote.LTLQuote.SameDayPickup = bool.Parse(_quote["SameDayPickup"].ToString());
                    quote.LTLQuote.DestinationCity = _quote["DestinationCity"].ToString();
                    quote.LTLQuote.DestinationState = _quote["DestinationState"].ToString();
                    quote.LTLQuote.DestinationZip = _quote["DestinationZip"].ToString();
                    quote.LTLQuote.InsideDelivery = bool.Parse(_quote["InsideDelivery"].ToString());
                    quote.LTLQuote.LiftGateDestination = bool.Parse(_quote["LiftGateDestination"].ToString());
                    quote.LTLQuote.AppointmentDestination = bool.Parse(_quote["AppointmentDestination"].ToString());
                    quote.LTLQuote.Pallet1Weight = int.Parse(_quote["Pallet1Weight"].ToString());
                    quote.LTLQuote.Pallet1InsuranceValue = decimal.Parse(_quote["Pallet1InsuranceValue"].ToString());
                    quote.LTLQuote.Pallet2Weight = int.Parse(_quote["Pallet2Weight"].ToString());
                    quote.LTLQuote.Pallet2InsuranceValue = decimal.Parse(_quote["Pallet2InsuranceValue"].ToString());
                    quote.LTLQuote.Pallet3Weight = int.Parse(_quote["Pallet3Weight"].ToString());
                    quote.LTLQuote.Pallet3InsuranceValue = decimal.Parse(_quote["Pallet3InsuranceValue"].ToString());
                    quote.LTLQuote.Pallet4Weight = int.Parse(_quote["Pallet4Weight"].ToString());
                    quote.LTLQuote.Pallet4InsuranceValue = decimal.Parse(_quote["Pallet4InsuranceValue"].ToString());
                    quote.LTLQuote.Pallet5Weight = int.Parse(_quote["Pallet5Weight"].ToString());
                    quote.LTLQuote.Pallet5InsuranceValue = decimal.Parse(_quote["Pallet5InsuranceValue"].ToString());
                    quote.LTLQuote.Pallets = int.Parse(_quote["Pallets"].ToString());
                    quote.LTLQuote.Weight = decimal.Parse(_quote["Weight"].ToString());
                    quote.LTLQuote.PalletRate = decimal.Parse(_quote["PalletRate"].ToString());
                    quote.LTLQuote.FuelSurcharge = decimal.Parse(_quote["FuelSurcharge"].ToString());
                    quote.LTLQuote.AccessorialCharge = decimal.Parse(_quote["AccessorialCharge"].ToString());
                    quote.LTLQuote.InsuranceCharge = decimal.Parse(_quote["InsuranceCharge"].ToString());
                    quote.LTLQuote.TollCharge = decimal.Parse(_quote["TollCharge"].ToString());
                    quote.LTLQuote.TotalCharge = decimal.Parse(_quote["TotalCharge"].ToString());
                    quote.TotalChargeMin = decimal.Parse(_quote["TotalChargeMin"].ToString());
                    quote.Logged = DateTime.Parse(_quote["Logged"].ToString());
                    quote.LoggedBy = _quote["LoggedBy"].ToString();
                    quote.Approved = !_quote.IsNull("Approved") ? DateTime.Parse(_quote["Approved"].ToString()) : DateTime.MinValue;
                    quote.ApprovedBy = !_quote.IsNull("ApprovedBy") ? _quote["ApprovedBy"].ToString() : "";
                    quote.LoadTenderNumber = !_quote.IsNull("LoadTenderNumber") ? int.Parse(_quote["LoadTenderNumber"].ToString()) : 0;
                    quote.ShipmentNumber = !_quote.IsNull("ShipmentNumber") ? _quote["ShipmentNumber"].ToString() : "";
                    quote.Cancelled =  !_quote.IsNull("Cancelled") ? DateTime.Parse(_quote["Cancelled"].ToString()) : DateTime.MinValue;
                    quote.CancelledBy = !_quote.IsNull("CancelledBy") ? _quote["CancelledBy"].ToString() : "";
                    #endregion
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return quote;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public bool UpdateLoadTenderQuote(LTLLoadTenderQuote quote) {
            //Update an existing LTLLoadTenderQuote
            bool updated = false;
            try {
                updated = new LTLGateway2().UpdateLoadTenderQuote(quote);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return updated;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public bool ApproveLoadTenderQuote(int quoteID, DateTime approved, string approvedBy) {
            //Approve an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new LTLGateway2().ApproveLoadTenderQuote(quoteID, approved, approvedBy);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return result;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public bool TenderLoadTenderQuote(int quoteID, LoadTender loadTender) {
            //Tender an existing LTLLoadTenderQuote
            bool result = false;
            try {
                //Apply simple business rules

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the load tender
                    int loadTenderNumber = new LTLGateway2().CreateLoadTender(loadTender.Filename, loadTender.File);

                    //Update the LoadTenderQuote
                    result = new LTLGateway2().TenderLoadTenderQuote(quoteID, loadTenderNumber);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return result;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public bool BookLoadTenderQuote(int quoteID, LTLShipment2 shipment) {
            //Book an existing LTLLoadTenderQuote
            bool result = false;
            try {
                //Apply simple business rules (if applicable)
                LTLClient2 client = ReadLTLClient(shipment.ClientNumber);
                LTLShipper2 shipper = ReadLTLShipper(shipment.ShipperNumber);
                //Can't book same day pickups after the cutoff hour
                if(shipment.ShipDate.CompareTo(DateTime.Today) == 0 && DateTime.Now.Hour >= int.Parse(ConfigurationManager.AppSettings["BookingCutoffHour"])) throw new ApplicationException("It is too late to book a same day shipment.)");

                //Execute the business transcation
                using(TransactionScope scope = new TransactionScope()) {
                    //Create the shipment
                    string number = new LTLGateway2().CreateLTLShipment(shipment);
                    shipment.ShipmentNumber = number;

                    //Schedule a pickup request for the new shipment if the ship date is valid 
                    //(i.e. admin needs to create a shipment after receiving freight so Dispatch not needed)
                    if(shipment.ShipDate.CompareTo(getNextValidShipDate()) >= 0) {
                        PickupRequest pickup = new PickupRequest();
                        #region Populate pickup
                        pickup.RequestID = 0;
                        pickup.ScheduleDate = shipment.ShipDate;
                        pickup.CallerName = shipment.UserID;
                        pickup.ClientNumber = client.Number;
                        pickup.Client = client.Name;
                        pickup.ShipperNumber = shipper.ClientNumber.Trim() + shipper.Number.Trim() + "VE";     //AcountID NOT shipper number
                        pickup.Shipper = shipper.Name;
                        pickup.ShipperAddress = shipper.AddressLine1.Trim() + "\r\n" + shipper.City.Trim() + ", " + shipper.State.Trim() + " " + shipper.Zip;
                        pickup.ShipperPhone = shipper.ContactPhone;
                        pickup.WindowOpen = shipper.WindowTimeStart.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeStart.ToString("HHmm")) : 0;
                        pickup.WindowClose = shipper.WindowTimeEnd.CompareTo(DateTime.MinValue) > 0 ? int.Parse(shipper.WindowTimeEnd.ToString("HHmm")) : 0;
                        pickup.Amount = shipment.Pallets;
                        pickup.AmountType = "Pallets";
                        pickup.FreightType = "Tsort";
                        pickup.OrderType = "B";
                        pickup.Weight = Convert.ToInt32(shipment.Weight);
                        pickup.Comments = "";
                        pickup.IsTemplate = false;
                        pickup.Created = DateTime.Now;
                        pickup.CreateUserID = "PSP";
                        pickup.TerminalNumber = shipper.AgentNumber;
                        pickup.Terminal = shipper.AgentName;
                        pickup.LastUpdated = shipment.LastUpdated;
                        pickup.UserID = shipment.UserID;
                        #endregion
                        int pickupID = new DispatchGateway().InsertPickupRequest3(pickup);

                        //Update the shipment with the pickupID
                        shipment.PickupID = pickupID;
                        shipment.PickupDate = DateTime.MinValue;
                        new LTLGateway2().DispatchLTLShipment(shipment);
                    }

                    //Update the LoadTenderQuote
                    result = new LTLGateway2().BookLoadTenderQuote(quoteID, number);

                    //Commit the transaction
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return result;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        public bool CancelLoadTenderQuote(int quoteID, DateTime cancelled, string cancelledBy) {
            //Cancel an existing LTLLoadTenderQuote
            bool result = false;
            try {
                //Apply simple business rules
                LTLLoadTenderQuote quote = ReadLoadTenderQuote(quoteID);
                LTLShipment2 shipment = null;
                if(quote.ShipmentNumber.Trim().Length > 0) {
                    shipment = ReadLTLShipment(quote.ShipmentNumber);
                    if(shipment.PickupDate != DateTime.MinValue) throw new ApplicationException("This shipment has already been picked up.");
                }

                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                    //Cancel the shipment if applicable
                    if(shipment != null) new LTLGateway2().CancelLTLShipment(shipment.ShipmentNumber, cancelledBy);

                    //Cancel the pickup request
                    if(shipment != null) new DispatchGateway().CancelPickupRequest(shipment.PickupID, cancelled, cancelledBy);

                    //Cancel the load tender quote
                    result = new LTLGateway2().CancelLoadTenderQuote(quoteID, cancelled, cancelledBy);

                    //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                    scope.Complete();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return result;
        }
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRep")]
        //[PrincipalPermission(SecurityAction.Demand, Role = "SalesRepAdmin")]
        public bool ChangeOwnerLoadTenderQuote(int quoteID, string owner) {
            //Change the owner of an existing LTLLoadTenderQuote
            bool result = false;
            try {
                result = new LTLGateway2().ChangeOwnerLoadTenderQuote(quoteID, owner);
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return result;
        }

        public LoadTender GetLoadTender(int number) {
            //Get an existing file attachment from database
            LoadTender loadTender = null;
            try {
                DataSet ds = new LTLGateway2().GetLoadTender(number);
                if(ds != null && ds.Tables["LoadTenderTable"] != null && ds.Tables["LoadTenderTable"].Rows.Count > 0) {
                    loadTender = new LoadTender();
                    loadTender.Number = number;
                    loadTender.Filename = (string)ds.Tables["LoadTenderTable"].Rows[0]["FileName"];
                    loadTender.File = (byte[])ds.Tables["LoadTenderTable"].Rows[0]["File"];
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return loadTender;
        }
        #endregion

        public ServiceLocation ReadPickupLocation(string zipCode) {
            ServiceLocation location = null;
            try {
                DataSet ds = new LTLGateway2().ReadPickupLocation(zipCode);
                if(ds != null && ds.Tables["ServiceLocationTable"] != null && ds.Tables["ServiceLocationTable"].Rows.Count > 0) {
                    location = new ServiceLocation();
                    location.ZipCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zip"].ToString();
                    location.City = ds.Tables["ServiceLocationTable"].Rows[0]["City"].ToString();
                    location.State = ds.Tables["ServiceLocationTable"].Rows[0]["State"].ToString();
                    location.ZoneCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zone"].ToString();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return location;
        }
        public ServiceLocations ReadPickupLocations(string city, string state) {
            ServiceLocations locations = null;
            try {
                DataSet ds = new LTLGateway2().ReadPickupLocations(city, state);
                if(ds != null && ds.Tables["ServiceLocationTable"] != null && ds.Tables["ServiceLocationTable"].Rows.Count > 0) {
                    locations = new ServiceLocations();
                    for(int i = 0; i < ds.Tables["ServiceLocationTable"].Rows.Count; i++) {
                        ServiceLocation location = new ServiceLocation();
                        location.ZipCode = ds.Tables["ServiceLocationTable"].Rows[i]["Zip"].ToString();
                        location.City = ds.Tables["ServiceLocationTable"].Rows[i]["City"].ToString();
                        location.State = ds.Tables["ServiceLocationTable"].Rows[i]["State"].ToString();
                        location.ZoneCode = ds.Tables["ServiceLocationTable"].Rows[i]["Zone"].ToString();
                        locations.Add(location);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return locations;
        }
        public ServiceLocation ReadServiceLocation(string zipCode) {
            ServiceLocation location = null;
            try {
                DataSet ds = new LTLGateway2().ReadServiceLocation(zipCode);
                if(ds != null && ds.Tables["ServiceLocationTable"] != null && ds.Tables["ServiceLocationTable"].Rows.Count > 0) {
                    location = new ServiceLocation();
                    location.ZipCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zip"].ToString();
                    location.City = ds.Tables["ServiceLocationTable"].Rows[0]["City"].ToString();
                    location.State = ds.Tables["ServiceLocationTable"].Rows[0]["State"].ToString();
                    location.ZoneCode = ds.Tables["ServiceLocationTable"].Rows[0]["Zone"].ToString();
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return location;
        }
        public ServiceLocations ReadServiceLocations(string city, string state) {
            ServiceLocations locations = null;
            try {
                DataSet ds = new LTLGateway2().ReadServiceLocations(city, state);
                if(ds != null && ds.Tables["ServiceLocationTable"] != null && ds.Tables["ServiceLocationTable"].Rows.Count > 0) {
                    locations = new ServiceLocations();
                    for(int i = 0; i < ds.Tables["ServiceLocationTable"].Rows.Count; i++) {
                        ServiceLocation location = new ServiceLocation();
                        location.ZipCode = ds.Tables["ServiceLocationTable"].Rows[i]["Zip"].ToString();
                        location.City = ds.Tables["ServiceLocationTable"].Rows[i]["City"].ToString();
                        location.State = ds.Tables["ServiceLocationTable"].Rows[i]["State"].ToString();
                        location.ZoneCode = ds.Tables["ServiceLocationTable"].Rows[i]["Zone"].ToString();
                        locations.Add(location);
                    }
                }
            }
            catch(Exception ex) { throw new FaultException<LTLFault>(new LTLFault(ex.Message), "Service Error"); }
            return locations;
        }

        private DateTime getNextValidShipDate() {
            //Determine next valid ship date (i.e. no weekends)
            DateTime shipDate = DateTime.Today.AddDays(0);
            if(shipDate.DayOfWeek == DayOfWeek.Saturday) shipDate.AddDays(2);
            if(shipDate.DayOfWeek == DayOfWeek.Sunday) shipDate.AddDays(1);
            return shipDate;
        }
    }
}
