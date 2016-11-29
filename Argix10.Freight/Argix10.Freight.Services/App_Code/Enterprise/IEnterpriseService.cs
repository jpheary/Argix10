using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Enterprise {
    //Enterprise Interfaces
    [CollectionDataContract]
    public class Agents:BindingList<Agent> {
        public Agents() { }
    }

    [DataContract]
    public class Agent:Shipper {
        //Members
        private string _contactname = "",_phone = "";
        private string _parentnumber = "",_scannertype = "",_agenttype = "";
        private int _scangunqty = 0;

        //Interface
        public Agent() : this(null) { }
        public Agent(EnterpriseDataset.AgentTableRow agent) : base(agent) {
            //Constructor
            try {
                if (agent != null) {
                    if (!agent.IsContactNameNull()) this._contactname = agent.ContactName;
                    if (!agent.IsPhoneNull()) this._phone = agent.Phone;
                    this._parentnumber = agent.ParentNumber;
                    this._scannertype = agent.ScannerType;
                    this._agenttype = agent.AgentType;
                    this._scangunqty = agent.ScanGunQty;
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected exception creating new agent instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ContactName { get { return this._contactname; } set { this._contactname = value; } }
        [DataMember]
        public string Phone { get { return this._phone; } set { this._phone = value; } }
        [DataMember]
        public string ParentNumber { get { return this._parentnumber; } set { this._parentnumber = value; } }
        [DataMember]
        public string ScannerType { get { return this._scannertype; } set { this._scannertype = value; } }
        [DataMember]
        public string AgentType { get { return this._agenttype; } set { this._agenttype = value; } }
        [DataMember]
        public int ScanGunQty { get { return this._scangunqty; } set { this._scangunqty = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Clients:BindingList<Client> {
        public Clients() { }
    }

    [DataContract]
    public class Client {
        //Members        
        private string _number = "",_division = "",_name = "",_status = "";

        //Interface
        public Client() { }
        public Client(EnterpriseDataset.ClientTableRow client) {
            //Constructor
            try {
                if (client != null) {
                    this._number = client.Number;
                    this._division = client.Division;
                    if (!client.IsNameNull()) this._name = client.Name;
                    if (!client.IsStatusNull()) this._status = client.Status;
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while creating new Client instance",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Division { get { return this._division; } set { this._division = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Shippers:BindingList<Shipper> {
        public Shippers() { }
    }

    [DataContract]
    public class Shipper {
        //Members
        protected string _number = "",_name = "",_status = "";
        protected string _addressline1 = "",_addressline2 = "",_city = "",_state = "",_zip = "",_zip4 = "";

        //Interface	
        public Shipper() { }
        public Shipper(EnterpriseDataset.AgentTableRow agent) {
            //Constructor
            try {
                if (agent != null) {
                    this._number = agent.Number;
                    if (!agent.IsNameNull()) this._name = agent.Name;
                    if (!agent.IsStatusNull()) this._status = agent.Status;
                    if (!agent.IsAddressLine1Null()) this._addressline1 = agent.AddressLine1;
                    if (!agent.IsAddressLine2Null()) this._addressline2 = agent.AddressLine2;
                    if (!agent.IsCityNull()) this._city = agent.City;
                    if (!agent.IsStateNull()) this._state = agent.State;
                    if (!agent.IsZipNull()) this._zip = agent.Zip;
                    if (!agent.IsZip4Null()) this._zip4 = agent.Zip4;
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        public Shipper(EnterpriseDataset.VendorTableRow vendor) {
            //Constructor
            try {
                if (vendor != null) {
                    this._number = vendor.Number;
                    if (!vendor.IsNameNull()) this._name = vendor.Name;
                    if (!vendor.IsStatusNull()) this._status = vendor.Status;
                    if (!vendor.IsAddressLine1Null()) this._addressline1 = vendor.AddressLine1;
                    if (!vendor.IsAddressLine2Null()) this._addressline2 = vendor.AddressLine2;
                    if (!vendor.IsCityNull()) this._city = vendor.City;
                    if (!vendor.IsStateNull()) this._state = vendor.State;
                    if (!vendor.IsZipNull()) this._zip = vendor.Zip;
                    if (!vendor.IsZip4Null()) this._zip4 = vendor.Zip4;
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected exception creating new shipper instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._addressline1; } set { this._addressline1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._addressline2; } set { this._addressline2 = value; } }
        [DataMember]
        public string City { get { return this._city; } set { this._city = value; } }
        [DataMember]
        public string State { get { return this._state; } set { this._state = value; } }
        [DataMember]
        public string Zip { get { return this._zip; } set { this._zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._zip4; } set { this._zip4 = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Stores:BindingList<Store> {
        public Stores() { }
    }

    [DataContract]
    public class Store {
        //Members
        private string _clientnumber = "",_clientdivision = "";
        private int _number = 0;
        private string _name = "";
        private string _addressline1 = "",_addressline2 = "",_city = "",_state = "",_zip = "",_zip4 = "";
        private string _status = "";

        //Interface
        public Store() : this(null) { }
        public Store(EnterpriseDataset.StoreTableRow store) {
            //Constructor
            try {
                if (store != null) {
                    this._clientnumber = store.ClientNumber;
                    this._clientdivision = store.ClientDivision;
                    this._number = store.Number;
                    if (!store.IsNameNull()) this._name = store.Name;
                    if (!store.IsAddressLine1Null()) this._addressline1 = store.AddressLine1;
                    if (!store.IsAddressLine2Null()) this._addressline2 = store.AddressLine2;
                    if (!store.IsCityNull()) this._city = store.City;
                    if (!store.IsStateNull()) this._state = store.State;
                    if (!store.IsZipNull()) this._zip = store.Zip;
                    if (!store.IsZip4Null()) this._zip4 = store.Zip4;
                    if (!store.IsStatusNull()) this._status = store.Status;
                }
            }
            catch (Exception ex) { throw new ApplicationException("Unexpected exception creating new store instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public string ClientNumber { get { return this._clientnumber; } set { this._clientnumber = value; } }
        [DataMember]
        public string ClientDivision { get { return this._clientdivision; } set { this._clientdivision = value; } }
        [DataMember]
        public int Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Name { get { return this._name; } set { this._name = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._addressline1; } set { this._addressline1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._addressline2; } set { this._addressline2 = value; } }
        [DataMember]
        public string City { get { return this._city; } set { this._city = value; } }
        [DataMember]
        public string State { get { return this._state; } set { this._state = value; } }
        [DataMember]
        public string Zip { get { return this._zip; } set { this._zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._zip4; } set { this._zip4 = value; } }
        [DataMember]
        public string Status { get { return this._status; } set { this._status = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Terminals:BindingList<Terminal> {
        public Terminals() { }
    }

    [DataContract]
    public class Terminal {
        //Members
        private int _terminalID = 0;
        private string _number = "",_description = "";
        private int _agentID = 0;
        private string _agentnumber = "",_shipperid = "",_clientdivision = "";
        private byte _isactive = (byte)1;

        //Interface
        public Terminal() : this(null) { }
        public Terminal(EnterpriseDataset.TerminalTableRow terminal) {
            //Constructor
            if (terminal != null) {
                if (!terminal.IsTerminalIDNull()) this._terminalID = terminal.TerminalID;
                if (!terminal.IsNumberNull()) this._number = terminal.Number;
                if (!terminal.IsDescriptionNull()) this._description = terminal.Description;
                if (!terminal.IsAgentIDNull()) this._agentID = terminal.AgentID;
                if (!terminal.IsAgentNumberNull()) this._agentnumber = terminal.AgentNumber;
                if (!terminal.IsShipperIDNull()) this._shipperid = terminal.ShipperID;
                if (!terminal.IsClientDivisionNull()) this._clientdivision = terminal.ClientDivision;
                if (!terminal.IsIsActiveNull()) this._isactive = terminal.IsActive;
            }
        }
        public Terminal(int terminalID,string number,string description,int agentID,string agentNumber) {
            //Constructor
            this._terminalID = terminalID;
            this._number = number;
            this._description = description;
            this._agentID = agentID;
            this._agentnumber = agentNumber;
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int TerminalID { get { return this._terminalID; } set { this._terminalID = value; } }
        [DataMember]
        public string Number { get { return this._number; } set { this._number = value; } }
        [DataMember]
        public string Description { get { return this._description; } set { this._description = value; } }
        [DataMember]
        public int AgentID { get { return this._agentID; } set { this._agentID = value; } }
        [DataMember]
        public string AgentNumber { get { return this._agentnumber; } set { this._agentnumber = value; } }
        [DataMember]
        public string ShipperID { get { return this._shipperid; } set { this._shipperid = value; } }
        [DataMember]
        public string ClientDivision { get { return this._clientdivision; } set { this._clientdivision = value; } }
        [DataMember]
        public byte IsActive { get { return this._isactive; } set { this._isactive = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Vendors:BindingList<Vendor> {
        public Vendors() { }
    }

    [DataContract]
    public class Vendor:Shipper {
        //Members

        //Interface
        public Vendor() : this(null) { }
        public Vendor(EnterpriseDataset.VendorTableRow vendor)
            : base(vendor) {
            //Constructor
            try { }
            catch (Exception ex) { throw new ApplicationException("Unexpected error while creating new vendor instance.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        #endregion
    }

    [DataContract]
    public class EnterpriseFault {
        private string mMessage = "";
        public EnterpriseFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
