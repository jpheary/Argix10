using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using Argix.Windows;

namespace Argix.Crossmatch {
	//
	public class VisTrakGateway {
		//Members
        private static VisTrakWebService _Client = null;
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static VisTrakGateway() { 
            //
            _Client = new VisTrakWebService();
            _state = true;
            _address = _Client.Url;
        }
        private VisTrakGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static VisTrakResult Search(string firstName,string lastName,string user1,string host,int group,string searchType) {
            //
            VisTrakResult result = null;
            try {
                //Create reporting service web client proxy
                _Client = new VisTrakWebService();
                _Client.Credentials = System.Net.CredentialCache.DefaultCredentials;

                result = _Client.Search(firstName,lastName,user1,"","","","","","","","","",host,group,searchType);
            }
            catch (TimeoutException te) { _Client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException fe) { _Client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { _Client.Abort(); throw new ApplicationException(ce.Message); }
            return result;
        }
    }
}