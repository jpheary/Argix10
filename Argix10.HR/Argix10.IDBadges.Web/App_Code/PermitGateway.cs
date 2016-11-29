using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.HR.Permits {
	//
	public class PermitGateway {
		//Members
        private bool _state=false;
        private string _address="";
        
		//Interface
        public PermitGateway() { 
            //
            PermitServiceClient client = new PermitServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        public bool ServiceState { get { return _state; } }
        public string ServiceAddress { get { return _address; } }

        public ServiceInfo GetServiceInfo() {
            //Get the operating enterprise terminal
            PermitServiceClient client = new PermitServiceClient();
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
            PermitServiceClient client = new PermitServiceClient();
            try {
                config = client.GetUserConfiguration(application,usernames);
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<ConfigurationFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return config;
        }
        public void WriteLogEntry(string message, Argix.HR.Permits.LogLevel level, string user) {
            //Write an entry into the Argix log
            PermitServiceClient client = new PermitServiceClient();
            try {
                Argix.HR.Permits.TraceMessage m = new Argix.HR.Permits.TraceMessage();
                m.Date = DateTime.Now;
                m.Name = "Argix10";
                m.Source = "IDBadges";
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

        public DataSet ViewPermits() {
            //
            DataSet permits = new DataSet();
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                DataSet ds = client.ViewPermits();
                if(ds != null) permits.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permits;
        }
        public Permit ReadPermit(int id) {
            //
            Permit permit = null;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                permit = client.ReadPermit(id);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permit;
        }
        public int RegisterPermit(Permit permit) {
            //
            int id = 0;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                id = client.RegisterPermit(permit);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return id;
        }
        public int ReplacePermit(Permit permit, string newNumber) {
            //
            int id = 0;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                id = client.ReplacePermit(permit, newNumber);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return id;
        }
        public bool RevokePermit(Permit permit) {
            //
            bool revoked = false;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                revoked = client.RevokePermit(permit);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return revoked;
        }
        public bool ChangeVehicle(Vehicle vehicle) {
            //
            bool changed = false;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                changed = client.ChangeVehicle(vehicle);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return changed;
        }
        public Permit ValidatePermitNumber(string number) {
            //
            Permit permit = null;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                permit = client.ValidatePermitNumber(number);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permit;
        }
        public Permit ValidateVehicle(string issueState, string plateNumber) {
            //
            Permit permit = null;
            PermitAdminServiceClient client = new PermitAdminServiceClient();
            try {
                permit = client.ValidateVehicle(issueState, plateNumber);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permit;
        }

        public DataSet FindPermitsByNumber(string number) {
            //
            DataSet permits = new DataSet();
            PermitSearchServiceClient client = new PermitSearchServiceClient();
            try {
                DataSet ds = client.FindPermitsByNumber(number);
                if(ds != null) permits.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permits;
        }
        public DataSet FindPermitsByPlate(string issueState, string plateNumber) {
            //
            DataSet permits = new DataSet();
            PermitSearchServiceClient client = new PermitSearchServiceClient();
            try {
                DataSet ds = client.FindPermitsByPlate(issueState, plateNumber);
                if(ds != null) permits.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permits;
        }
        public DataSet FindPermitsByVehicle(string year, string make, string model, string color) {
            //
            DataSet permits = new DataSet();
            PermitSearchServiceClient client = new PermitSearchServiceClient();
            try {
                DataSet ds = client.FindPermitsByVehicle(year, make, model, color);
                if(ds != null) permits.Merge(ds);
                client.Close();
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<HRFault> hfe) { client.Abort(); throw new ApplicationException(hfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return permits;
        }

        public DataSet GetStateList() {
            //Return the acronyms for all 50 states
            DataSet states = new DataSet();
            states.Tables.Add("StateTable");
            states.Tables["StateTable"].Columns.Add("Name");
            states.Tables["StateTable"].Rows.Add(new object[] { "AL" });
            states.Tables["StateTable"].Rows.Add(new object[] { "AK" });
            states.Tables["StateTable"].Rows.Add(new object[] { "AZ" });
            states.Tables["StateTable"].Rows.Add(new object[] { "AR" });
            states.Tables["StateTable"].Rows.Add(new object[] { "CA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "CO" });
            states.Tables["StateTable"].Rows.Add(new object[] { "CT" });
            states.Tables["StateTable"].Rows.Add(new object[] { "DE" });
            states.Tables["StateTable"].Rows.Add(new object[] { "FL" });
            states.Tables["StateTable"].Rows.Add(new object[] { "GA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "HI" });
            states.Tables["StateTable"].Rows.Add(new object[] { "ID" });
            states.Tables["StateTable"].Rows.Add(new object[] { "IL" });
            states.Tables["StateTable"].Rows.Add(new object[] { "IN" });
            states.Tables["StateTable"].Rows.Add(new object[] { "IA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "KS" });
            states.Tables["StateTable"].Rows.Add(new object[] { "KY" });
            states.Tables["StateTable"].Rows.Add(new object[] { "LA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "ME" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MD" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MI" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MN" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MS" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MO" });
            states.Tables["StateTable"].Rows.Add(new object[] { "MT" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NE" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NV" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NH" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NJ" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NM" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NY" });
            states.Tables["StateTable"].Rows.Add(new object[] { "NC" });
            states.Tables["StateTable"].Rows.Add(new object[] { "ND" });
            states.Tables["StateTable"].Rows.Add(new object[] { "OH" });
            states.Tables["StateTable"].Rows.Add(new object[] { "OK" });
            states.Tables["StateTable"].Rows.Add(new object[] { "OR" });
            states.Tables["StateTable"].Rows.Add(new object[] { "PA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "RI" });
            states.Tables["StateTable"].Rows.Add(new object[] { "SC" });
            states.Tables["StateTable"].Rows.Add(new object[] { "SD" });
            states.Tables["StateTable"].Rows.Add(new object[] { "TN" });
            states.Tables["StateTable"].Rows.Add(new object[] { "TX" });
            states.Tables["StateTable"].Rows.Add(new object[] { "UT" });
            states.Tables["StateTable"].Rows.Add(new object[] { "VT" });
            states.Tables["StateTable"].Rows.Add(new object[] { "VA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "WA" });
            states.Tables["StateTable"].Rows.Add(new object[] { "WV" });
            states.Tables["StateTable"].Rows.Add(new object[] { "WI" });
            states.Tables["StateTable"].Rows.Add(new object[] { "WY" });  
            return states;
        }
    }
}
