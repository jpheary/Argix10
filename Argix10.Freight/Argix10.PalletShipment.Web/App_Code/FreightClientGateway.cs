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
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return requests;
        }
        public int RequestPickup(PickupRequest pickup) {
            //Create a new pickup request
            int pickupID = 0;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                pickupID = client.RequestPickup(pickup);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return pickupID;
        }
        public PickupRequest ReadPickup(int requestID) {
            //Read an existing pickup request
            PickupRequest request = null;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                request = client.ReadPickup(requestID);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return request;
        }
        public bool UpdatePickup(PickupRequest pickup) {
            //Update an existing pickup request
            bool updated = false;
            DispatchClientServiceClient client = new DispatchClientServiceClient();
            try {
                updated = client.UpdatePickup(pickup);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return updated;
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
            catch (FaultException<DispatchFault> dfe) { client.Abort(); throw new ApplicationException(dfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return cancelled;
        }
    }
}