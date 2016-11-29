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
        private static bool _state=false;
        private static string _address="";

		//Interface
        static FreightGateway() { 
            //
            LTLService2Client client = new LTLService2Client();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private FreightGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }


        public static LTLQuote2 CreateQuote(LTLQuote2 quote) {
            //Create a new LTL Quote
            LTLAdminService2Client client = new LTLAdminService2Client();
            try {
                quote = client.CreateQuoteForAdmin(quote);
            }
            catch(TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch(FaultException<LTLFault> cfe) { client.Abort(); throw new ApplicationException(cfe.Detail.Message); }
            catch(FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch(CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return quote;
        }
    }
}