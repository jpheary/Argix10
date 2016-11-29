using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Freight {
	//
	public class FreightGateway {
		//Members
        
		//Interface
        public FreightGateway() { }

        public PickupLogDataset ViewPickupLog(string schedule) {
            //View the pickup log
            PickupLogDataset requests = new PickupLogDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;
                switch (schedule) {
                    case "Today":
                        break;
                    case "Advanced":
                        start=DateTime.Today.AddDays(1); end=DateTime.Today.AddDays(30);
                        break;
                    case "Archive":
                        start=DateTime.Today.AddDays(-30); end=DateTime.Today.AddDays(-1);
                        break;
                    default:
                        start = DateTime.Today.AddDays(-30); end = DateTime.Today.AddDays(30);
                        break;
                }
                DataSet ds = client.ViewPickupLog(start, end);
                if (ds != null) requests.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return requests;
        }
        public ClientInboundDataset ViewClientInboundSchedule(string schedule) {
            //View the client inbound schedule
            ClientInboundDataset appointments = new ClientInboundDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;
                switch (schedule) {
                    case "Today":
                        break;
                    case "Advanced":
                        start = DateTime.Today.AddDays(1); end = DateTime.Today.AddDays(30);
                        break;
                    case "Archive":
                        start = DateTime.Today.AddDays(-30); end = DateTime.Today.AddDays(-1);
                        break;
                }
                DataSet ds = client.ViewClientInboundSchedule(start,end);
                if (ds != null) appointments.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return appointments;
        }
        public InboundDataset ViewInboundSchedule(string schedule) {
            //View the inbound schedule
            InboundDataset trips = new InboundDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;
                switch (schedule) {
                    case "Today":
                        break;
                    case "Advanced":
                        start = DateTime.Today.AddDays(1); end = DateTime.Today.AddDays(30);
                        break;
                    case "Archive":
                        start = DateTime.Today.AddDays(-30); end = DateTime.Today.AddDays(-1);
                        break;
                }
                DataSet ds = client.ViewInboundSchedule(start,end);
                if (ds != null) trips.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return trips;
        }
        public OutboundDataset ViewOutboundSchedule(string schedule) {
            //View the outbound schedule
            OutboundDataset trips = new OutboundDataset();
            DispatchServiceClient client = new DispatchServiceClient();
            try {
                DateTime start = DateTime.Today;
                DateTime end = DateTime.Today;
                switch (schedule) {
                    case "Today":
                        break;
                    case "Advanced":
                        start = DateTime.Today.AddDays(1); end = DateTime.Today.AddDays(30);
                        break;
                    case "Archive":
                        start = DateTime.Today.AddDays(-30); end = DateTime.Today.AddDays(-1);
                        break;
                }
                DataSet ds = client.ViewOutboundSchedule(start,end);
                if (ds != null) trips.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message,te); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message,fe); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message,ce); }
            return trips;
        }
    }
}