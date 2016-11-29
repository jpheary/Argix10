using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.BizTalk {
	//
    public class BizTalkGateway {
		//Members

		//Interface
        public BizTalkGateway() { }
        //public CommunicationState ServiceState { get { return new Argix_Freight_ManageLTLPickup_LTLPortClient().State; } }
        //public string ServiceAddress { get { return new Argix_Freight_ManageLTLPickup_LTLPortClient().Endpoint.Address.Uri.AbsoluteUri; } }

        //public void ScheduleLTLPickup(LTLShipment shipment) {
        //    //
        //    Argix_Freight_ManageLTLPickup_LTLPortClient client = new Argix_Freight_ManageLTLPickup_LTLPortClient();
        //    try {
        //        client.ScheduleLTLPickup(shipment);
        //        client.Close();
        //    }
        //    catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
        //    catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
        //    catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        //}
        //public void ArriveLTLShipment(PickupRequest request) {
        //    //
        //    Argix_Freight_ManageLTLPickup_LTLPortClient client = new Argix_Freight_ManageLTLPickup_LTLPortClient();
        //    try {
        //        client.ArriveLTLShipment(request);
        //        client.Close();
        //    }
        //    catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
        //    catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
        //    catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        //}
    }
}