using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Web.Security;

namespace Argix.Freight.Client {
	//
	public class FreightClientGateway {
		//Members
        
		//Interface
        public FreightClientGateway() { }

        public PickupLogDataset ViewPickupLog(string clientNumber) {
            //View the pickup log
            PickupLogDataset requests = new PickupLogDataset();
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                DataSet ds = client.ViewPickupLog(clientNumber);
                if (ds != null) requests.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return requests;
        }
        public int RequestPickup(PickupRequest request) {
            //Create a new pickup request
            int pickupID=0;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                pickupID = client.RequestPickup(request);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return pickupID;
        }
        public bool CancelPickup(int requestID) {
            //Cancel an existing pickup request
            bool cancelled = false;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                cancelled = client.CancelPickup(requestID,DateTime.Now,Membership.GetUser().UserName);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return cancelled;
        }

        public ClientPickupDataset ViewClientPickups(string clientNumber) {
            //View the pickup log
            ClientPickupDataset pickups = new ClientPickupDataset();
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                DataSet ds = client.ViewClientPickups(clientNumber);
                if (ds != null && ds.Tables["ClientPickupTable"] != null && ds.Tables["ClientPickupTable"].Rows.Count > 0) pickups.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return pickups;
        }
        public int RequestClientPickup(ClientPickup pickup) {
            //Create a new client pickup
            int pickupID = 0;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                pickupID = client.RequestClientPickup(pickup);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return pickupID;
        }
        public bool CancelClientPickup(int requestID) {
            //Cancel an existing pickup request
            bool cancelled = false;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                cancelled = client.CancelClientPickup(requestID,DateTime.Now,Membership.GetUser().UserName);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return cancelled;
        }
    }
}