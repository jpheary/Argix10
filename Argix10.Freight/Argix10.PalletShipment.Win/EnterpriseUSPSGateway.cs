using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;

namespace Argix.Enterprise.USPS {
	//
	public class EnterpriseUSPSGateway {
		//Members

		//Interface
        public EnterpriseUSPSGateway() { }
        public CommunicationState ServiceState { get { return new USPSServiceClient().State; } }
        public string ServiceAddress { get { return new USPSServiceClient().Endpoint.Address.Uri.AbsoluteUri; } }

        public static AddressValidateResponse VerifyAddress(string firmName, string address1, string address2, string city, string state, string zip5, string zip4) {
            //
            AddressValidateResponse address = new AddressValidateResponse();
            USPSServiceClient client = new USPSServiceClient();
            try {
                DataSet ds = client.VerifyAddress(firmName, address1, address2, city, state, zip5, zip4);
                if(ds != null) address.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<EnterpriseFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return address;
        }
    }
}