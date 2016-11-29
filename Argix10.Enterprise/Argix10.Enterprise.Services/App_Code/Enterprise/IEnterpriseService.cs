using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix {
    //
    [ServiceContract(Namespace="http://Argix")]
    public interface IEnterpriseService {
        [OperationContract]
        DataSet GetArgixTerminals();
        [OperationContract]
        DataSet GetTerminals();
        [OperationContract]
        DataSet GetClientsList(bool activeOnly);
        [OperationContract]
        DataSet GetClients();
        [OperationContract(Name="GetFilteredClients")]
        DataSet GetClients(string number,string division,bool activeOnly);
        [OperationContract]
        DataSet GetClientsForVendor(string vendorID);
        [OperationContract]
        DataSet GetClientDivisions(string number);
        [OperationContract]
        DataSet GetClientTerminals(string number);
        [OperationContract]
        DataSet GetClientRegions(string number);
        [OperationContract]
        DataSet GetClientDistricts(string number);
        [OperationContract]
        DataSet GetConsumerDirectCustomers();
        [OperationContract]
        DataSet GetVendorsList(string clientNumber,string clientTerminal);
        [OperationContract]
        DataSet GetVendors(string clientNumber,string clientTerminal);
        [OperationContract]
        DataSet GetParentVendors(string clientNumber,string clientTerminal);
        [OperationContract]
        DataSet GetVendorLocations(string clientNumber,string clientTerminal,string vendorNumber);
        [OperationContract]
        DataSet GetAgents(bool mainZoneOnly);
        [OperationContract]
        DataSet GetParentAgents();
        [OperationContract]
        DataSet GetAgentLocations(string agent);
        [OperationContract]
        DataSet GetShippers(string freightType,string clientNumber,string clientTerminal);
        [OperationContract]
        DataSet GetZones();

        [OperationContract]
        DataSet GetPickups(string client,string division,DateTime startDate,DateTime endDate,string vendor);
        [OperationContract]
        DataSet GetDeliveryExceptions();
        [OperationContract]
        DataSet GetIndirectTrips(string terminal,int daysBack);
        [OperationContract]
        DataSet GetTLs(string terminal,int daysBack);
        [OperationContract]
        DataSet GetShifts(string terminal,DateTime date);
        [OperationContract]
        DataSet GetVendorLog(string client,string clientDivision,DateTime startDate,DateTime endDate);

        [OperationContract]
        DataSet GetRetailDates(string scope);
        [OperationContract]
        DataSet GetSortDates();
        [OperationContract]
        DataSet GetDamageCodes();
        [OperationContract]
        DataSet GetCartons(string cartonNumber,string terminalCode,string clientNumber);
    }

    [DataContract]
    public class EnterpriseFault {
        private string mMessage = "";
        public EnterpriseFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}