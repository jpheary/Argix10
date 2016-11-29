using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Terminals {
    //Interface
    [ServiceContract(Namespace="http://Argix.Terminals")]
    public interface IRoadshowService {
        //Interface
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action="http://Argix.Terminals.RoadshowFault")]
        Customers GetCustomers();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action = "http://Argix.Terminals.RoadshowFault")]
        Customers2 GetCustomers2();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action="http://Argix.Terminals.RoadshowFault")]
        Depots GetDepots();
        [OperationContract]
        [FaultContractAttribute(typeof(RoadshowFault),Action="http://Argix.Terminals.RoadshowFault")]
        Drivers GetDrivers(int depotNumber);
    }

    [CollectionDataContract]
    public class Customers:BindingList<Customer> { public Customers() { } }

    [DataContract]
    public class Customer {
        //Members
        private int _CustomerID=0;
        private string _AccountID="",_Name="";
        private string _AddressLine1="",_AddressLine2="",_City="",_State="",_Zip="",_Zip4="";
        private string _Phone="";
        private string _Address="";
        private int _WindowOpen=900,_WindowClose=1700;

        public Customer() : this(null) { }
        public Customer(RoadshowDataset.CustomerTableRow customer) {
            //Constructor
            try {
                if(customer != null) {
                    this._CustomerID = customer.CustomerID;
                    if(!customer.IsAccountIDNull()) this._AccountID = customer.AccountID;
                    if(!customer.IsNameNull()) this._Name = customer.Name;
                    if(!customer.IsAddressLine1Null()) this._AddressLine1 = customer.AddressLine1;
                    if(!customer.IsAddressLine2Null()) this._AddressLine1 = customer.AddressLine2;
                    if(!customer.IsCityNull()) this._City = customer.City;
                    if(!customer.IsStateNull()) this._State = customer.State;
                    if(!customer.IsZipNull()) this._Zip = customer.Zip;
                    if(!customer.IsZip4Null()) this._Zip4 = customer.Zip4;
                    if (!customer.IsPhoneNull()) this._Phone = customer.Phone;
                    if(!customer.IsWindowOpenNull()) this._WindowOpen = customer.WindowOpen;
                    if(!customer.IsWindowCloseNull()) this._WindowClose = customer.WindowClose;
                    this._Address = (!customer.IsAddressLine1Null() ? customer.AddressLine1 : "") + 
                                    (!customer.IsAddressLine2Null() ? "\r\n" + customer.AddressLine2 : "") + 
                                    "\r\n" + (!customer.IsCityNull() ? customer.City : "") + 
                                    (!customer.IsStateNull() ? ", " + customer.State : "") + 
                                    (!customer.IsZipNull() ? " " + customer.Zip : "") + (!customer.IsZip4Null() ? "-" + customer.Zip4 : "");
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new customer.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int CustomerID { get { return this._CustomerID; } set { this._CustomerID = value; } }
        [DataMember]
        public string AccountID { get { return this._AccountID; } set { this._AccountID = value; } }
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._AddressLine1; } set { this._AddressLine1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._AddressLine2; } set { this._AddressLine2 = value; } }
        [DataMember]
        public string Address { get { return this._Address; } set { this._Address = value; } }
        [DataMember]
        public string City { get { return this._City; } set { this._City = value; } }
        [DataMember]
        public string State { get { return this._State; } set { this._State = value; } }
        [DataMember]
        public string Zip { get { return this._Zip; } set { this._Zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._Zip4; } set { this._Zip4 = value; } }
        [DataMember]
        public string Phone { get { return this._Phone; } set { this._Phone = value; } }
        [DataMember]
        public int WindowOpen { get { return this._WindowOpen; } set { this._WindowOpen = value; } }
        [DataMember]
        public int WindowClose { get { return this._WindowClose; } set { this._WindowClose = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Customers2:BindingList<Customer2> { public Customers2() { } }

    [DataContract]
    public class Customer2 {
        //Members
        private int _CustomerID = 0;
        private string _AccountID = "",_Name = "";
        private string _AddressLine1 = "",_AddressLine2 = "",_City = "",_State = "",_Zip = "",_Zip4 = "";
        private int _TerritoryID = 0;
        private string _Phone = "";
        private string _Address = "";
        private int _WindowOpen = 900,_WindowClose = 1700;

        public Customer2() : this(null) { }
        public Customer2(RoadshowDataset.CustomerTableRow customer) {
            //Constructor
            try {
                if (customer != null) {
                    this._CustomerID = customer.CustomerID;
                    if (!customer.IsAccountIDNull()) this._AccountID = customer.AccountID;
                    if (!customer.IsNameNull()) this._Name = customer.Name;
                    if (!customer.IsAddressLine1Null()) this._AddressLine1 = customer.AddressLine1;
                    if (!customer.IsAddressLine2Null()) this._AddressLine1 = customer.AddressLine2;
                    if (!customer.IsCityNull()) this._City = customer.City;
                    if (!customer.IsStateNull()) this._State = customer.State;
                    if (!customer.IsZipNull()) this._Zip = customer.Zip;
                    if (!customer.IsZip4Null()) this._Zip4 = customer.Zip4;
                    if (!customer.IsTerritoryIDNull()) this._TerritoryID = customer.TerritoryID;
                    if (!customer.IsPhoneNull()) this._Phone = customer.Phone;
                    if (!customer.IsWindowOpenNull()) this._WindowOpen = customer.WindowOpen;
                    if (!customer.IsWindowCloseNull()) this._WindowClose = customer.WindowClose;
                    this._Address = (!customer.IsAddressLine1Null() ? customer.AddressLine1 : "") +
                                    (!customer.IsAddressLine2Null() ? "\r\n" + customer.AddressLine2 : "") +
                                    "\r\n" + (!customer.IsCityNull() ? customer.City : "") +
                                    (!customer.IsStateNull() ? ", " + customer.State : "") +
                                    (!customer.IsZipNull() ? " " + customer.Zip : "") + (!customer.IsZip4Null() ? "-" + customer.Zip4 : "");
                }
            }
            catch (Exception ex) { throw new ApplicationException("Failed to create new customer.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int CustomerID { get { return this._CustomerID; } set { this._CustomerID = value; } }
        [DataMember]
        public string AccountID { get { return this._AccountID; } set { this._AccountID = value; } }
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._AddressLine1; } set { this._AddressLine1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._AddressLine2; } set { this._AddressLine2 = value; } }
        [DataMember]
        public string Address { get { return this._Address; } set { this._Address = value; } }
        [DataMember]
        public string City { get { return this._City; } set { this._City = value; } }
        [DataMember]
        public string State { get { return this._State; } set { this._State = value; } }
        [DataMember]
        public string Zip { get { return this._Zip; } set { this._Zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._Zip4; } set { this._Zip4 = value; } }
        [DataMember]
        public int TerritoryID { get { return this._TerritoryID; } set { this._TerritoryID = value; } }
        [DataMember]
        public string Phone { get { return this._Phone; } set { this._Phone = value; } }
        [DataMember]
        public int WindowOpen { get { return this._WindowOpen; } set { this._WindowOpen = value; } }
        [DataMember]
        public int WindowClose { get { return this._WindowClose; } set { this._WindowClose = value; } }
        #endregion
    }

    [CollectionDataContract]
    public class Depots:BindingList<Depot> { public Depots() { } }

    [DataContract]
    public class Depot {
        //Members
        private short _Number=0;
        private string _Name="";
        private string _AddressLine1="",_AddressLine2="",_City="",_State="",_Zip="",_Zip4="";
        private string _Class="";

        public Depot(): this(null) { }
        public Depot(RoadshowDataset.DepotTableRow depot) { 
            //Constructor
            try {
                if(depot != null) {
                    if(!depot.IsNumberNull()) this._Number = depot.Number;
                    if(!depot.IsNameNull()) this._Name = depot.Name;
                    if(!depot.IsAddressLine1Null()) this._AddressLine1 = depot.AddressLine1;
                    if(!depot.IsAddressLine2Null()) this._AddressLine1 = depot.AddressLine2;
                    if(!depot.IsCityNull()) this._City = depot.City;
                    if(!depot.IsStateNull()) this._State = depot.State;
                    if(!depot.IsZipNull()) this._Zip = depot.Zip;
                    if(!depot.IsZip4Null()) this._Zip4 = depot.Zip4;
                    if(!depot.IsClassNull()) this._Class = depot.Class;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new depot.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public short Number { get { return this._Number; } set { this._Number = value; } }
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string AddressLine1 { get { return this._AddressLine1; } set { this._AddressLine1 = value; } }
        [DataMember]
        public string AddressLine2 { get { return this._AddressLine2; } set { this._AddressLine2 = value; } }
        [DataMember]
        public string City { get { return this._City; } set { this._City = value; } }
        [DataMember]
        public string State { get { return this._State; } set { this._State = value; } }
        [DataMember]
        public string Zip { get { return this._Zip; } set { this._Zip = value; } }
        [DataMember]
        public string Zip4 { get { return this._Zip4; } set { this._Zip4 = value; } }
        [DataMember]
        public string Class { get { return this._Class; } set { this._Class = value; } }
        #endregion
    }
    
    [CollectionDataContract]
    public class Drivers:BindingList<Driver> { public Drivers() { } }
    
    [DataContract]
    public class Driver {
        //Members
        private int _DriverID=0;
        private string _Number="",_Name="",_RouteName="";
        private int _DeviceNumber=0;
        private string _Status="";

        public Driver(): this(null) { }
        public Driver(RoadshowDataset.DriverTableRow driver) { 
            //Constructor
            try {
                if(driver != null) {
                    if(!driver.IsNumberNull()) this._Name = driver.Number;
                    if(!driver.IsNameNull()) this._Name = driver.Name;
                    if(!driver.IsRouteNameNull()) this._RouteName = driver.RouteName;
                    if(!driver.IsDeviceNumberNull()) this._DeviceNumber = driver.DeviceNumber;
                    if(!driver.IsStatusNull()) this._Status = driver.Status;
                }
            }
            catch(Exception ex) { throw new ApplicationException("Failed to create new driver.",ex); }
        }
        #region Accessors\Modifiers: [Members...]
        [DataMember]
        public int DriverID { get { return this._DriverID; } set { this._DriverID = value; } }
        [DataMember]
        public string Number { get { return this._Number; } set { this._Number = value; } }
        [DataMember]
        public string Name { get { return this._Name; } set { this._Name = value; } }
        [DataMember]
        public string RouteName { get { return this._RouteName; } set { this._RouteName = value; } }
        [DataMember]
        public int DeviceNumber { get { return this._DeviceNumber; } set { this._DeviceNumber = value; } }
        [DataMember]
        public string Status { get { return this._Status; } set { this._Status = value; } }
        #endregion
    }

    [DataContract]
    public class RoadshowFault {
        private string mMessage="";
        public RoadshowFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
