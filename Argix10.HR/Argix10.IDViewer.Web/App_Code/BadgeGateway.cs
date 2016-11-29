using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.HR {
	//
	public class BadgeGateway {
		//Members
        private bool _state=false;
        private string _address="";
        
		//Interface
        public BadgeGateway() { 
            //
            Argix.HR.BadgeServiceClient client = new BadgeServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        public bool ServiceState { get { return _state; } }
        public string ServiceAddress { get { return _address; } }

        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            BadgeServiceClient client = new BadgeServiceClient();
            try {
                return client.GetServiceInfo();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }
        public UserConfiguration GetUserConfiguration(string application,string[] usernames) {
            //Get the application configuration for the specified user
            UserConfiguration config=null;
            BadgeServiceClient client = new BadgeServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(string message,Argix.HR.LogLevel level, string user) {
            //Write an entry into the Argix log
            BadgeServiceClient client = new BadgeServiceClient();
            try {
                Argix.HR.TraceMessage m = new Argix.HR.TraceMessage();
                m.Date = DateTime.Now;
                m.Name = "Argix10";
                m.Source = "Argix10.IDBadges.Web";
                m.User = user;
                m.Computer = Environment.MachineName;
                m.LogLevel = level;
                m.Message = message;
                client.WriteLogEntry(m);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        }

        public BadgeDataset ViewDriverBadges() {
            //
            BadgeDataset badges = new BadgeDataset();
            DriverBadgeServiceClient client = new DriverBadgeServiceClient();
            try {
                DataSet ds = client.ViewDriverBadges();
                if (ds != null) badges.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return badges;
        }

        public BadgeDataset ViewEmployeeBadges() {
            //
            BadgeDataset badges = new BadgeDataset();
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                DataSet ds = client.ViewEmployeeBadges();
                if (ds != null) badges.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return badges;
        }
        public EmployeeBadge GetEmployeeBadge(int idNumber) {
            //
            EmployeeBadge badge = null;
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                badge = client.GetEmployeeBadge(idNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return badge;
        }
        public bool AddEmployeeBadge(EmployeeBadge badge) {
            //
            bool added = false;
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                added = client.AddEmployeeBadge(badge);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public bool UpdateEmployeeBadge(EmployeeBadge badge) {
            //
            bool updated = false;
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                updated = client.UpdateEmployeeBadge(badge);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public DataSet GetEmployeeDepartmentList() {
            //
            DataSet list = new DataSet();
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                DataSet ds = client.GetEmployeeDepartmentList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }
        public DataSet GetEmployeeLocationList() {
            //
            DataSet list = new DataSet();
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                DataSet ds = client.GetEmployeeLocationList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }
        public DataSet GetEmployeeSubLocationList() {
            //
            DataSet list = new DataSet();
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                DataSet ds = client.GetEmployeeSubLocationList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }
        public DataSet GetEmployeeStatusList() {
            //
            DataSet list = new DataSet();
            EmployeeBadgeServiceClient client = new EmployeeBadgeServiceClient();
            try {
                DataSet ds = client.GetEmployeeStatusList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }

        public BadgeDataset ViewVendorBadges() {
            //
            BadgeDataset badges = new BadgeDataset();
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                DataSet ds = client.ViewVendorBadges();
                if (ds != null) badges.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return badges;
        }
        public VendorBadge GetVendorBadge(int idNumber) {
            //
            VendorBadge badge = null;
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                badge = client.GetVendorBadge(idNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return badge;
        }
        public bool AddVendorBadge(VendorBadge badge) {
            //
            bool added = false;
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                added = client.AddVendorBadge(badge);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return added;
        }
        public bool UpdateVendorBadge(VendorBadge badge) {
            //
            bool updated = false;
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                updated = client.UpdateVendorBadge(badge);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return updated;
        }

        public DataSet GetVendorDepartmentList() {
            //
            DataSet list = new DataSet();
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                DataSet ds = client.GetVendorDepartmentList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }
        public DataSet GetVendorLocationList() {
            //
            DataSet list = new DataSet();
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                DataSet ds = client.GetVendorLocationList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }
        public DataSet GetVendorStatusList() {
            //
            DataSet list = new DataSet();
            VendorBadgeServiceClient client = new VendorBadgeServiceClient();
            try {
                DataSet ds = client.GetVendorStatusList();
                if (ds != null) list.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return list;
        }
    }
}