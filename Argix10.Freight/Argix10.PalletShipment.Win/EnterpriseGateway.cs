using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Enterprise {
	//
	public class EnterpriseGateway {
		//Members

		//Interface
        public EnterpriseGateway() { }
        public CommunicationState ServiceState { get { return new LTLTrackingServiceClient().State; } }
        public string ServiceAddress { get { return new LTLTrackingServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public static TrackingItems TrackShipment(string shipmentNumber) {
            //
            TrackingItems items = null;
            LTLTrackingServiceClient client = new LTLTrackingServiceClient();
            try {
                items = client.TrackPalletShipment(shipmentNumber);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<TrackingFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return items;
        }
    }
}