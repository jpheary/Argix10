using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix {
	//
	public class TrackingProxy {
		//Members

		//Interface
        public TrackingProxy() { }
        public CommunicationState ServiceState { get { return new FastTrackingServiceClient().State; } }
        public string ServiceAddress { get { return new FastTrackingServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public TrackingItems TrackCartons(string[] itemNumbers,string companyID) {
            //Get invoices for the specified client
            TrackingItems items = null;
            FastTrackingServiceClient _Client = null;
            try {
                _Client = new FastTrackingServiceClient();
                items = _Client.TrackCartons(itemNumbers, companyID);
                _Client.Close();
            }
            catch(FaultException fe) { throw new ApplicationException("TrackCartons() service error.",fe); }
            catch(TimeoutException te) { _Client.Abort(); throw new ApplicationException("TrackCartons() timeout error.",te); }
            catch(CommunicationException ce) { _Client.Abort(); throw new ApplicationException("TrackCartons() communication error.",ce); }
            return items;
        }
    }
}