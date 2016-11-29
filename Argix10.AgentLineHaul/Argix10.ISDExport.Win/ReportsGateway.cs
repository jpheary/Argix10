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

namespace Argix.Reports {
	//
	public class ReportsGateway {
		//Members
        private static bool _state=false;
        private static string _address="";
        
		//Interface
        static ReportsGateway() { 
            //
            AgentLineHaulServiceClient client = new AgentLineHaulServiceClient();
            _state = true;
            _address = client.Endpoint.Address.Uri.AbsoluteUri;
        }
        private ReportsGateway() { }
        public static bool ServiceState { get { return _state; } }
        public static string ServiceAddress { get { return _address; } }

        public static DataSet InboundManifestsView(string clientNumber, string clientDivision, DateTime startPickupDate, DateTime endPickupDate) {
            //
            DataSet manifests = new DataSet();
            AgentLineHaulServiceClient client = new AgentLineHaulServiceClient();
            try {
                DataSet ds = client.InboundManifestsView(clientNumber, clientDivision, startPickupDate, endPickupDate);
                if(ds != null) manifests.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return manifests;
        }
        public static DataSet InboundManifestRead(string manifestID) {
            //
            DataSet manifest = new DataSet();
            AgentLineHaulServiceClient client = new AgentLineHaulServiceClient();
            try {
                DataSet ds = client.InboundManifestRead(manifestID);
                if(ds != null) manifest.Merge(ds);
                client.Close();
            }
            catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
            catch (FaultException<AgentLineHaulFault> afe) { client.Abort(); throw new ApplicationException(afe.Detail.Message); }
            catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
            catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
            return manifest;
        }

        public static int SortedItemsManifested(string clientNumber, string clientDivision, DateTime pickupDate, int pickupNumber) {
            //Return sorted items manifested for the specified client and pickup date
            int items = 0;
            try {
                DataSet manifests = InboundManifestsView(clientNumber, clientDivision, pickupDate, pickupDate);
                if(manifests != null && manifests.Tables["ManifestTable"] != null && manifests.Tables["ManifestTable"].Rows.Count > 0) {
                    //Find by pickupNumber
                    string manifestID="";
                    if(manifests.Tables["ManifestTable"].Rows.Count > 1) {
                        DataRow manifest = manifests.Tables["ManifestTable"].Select("PickupNumber=" + pickupNumber)[0];
                        manifestID = manifest["ID"].ToString();
                    }
                    else
                        manifestID = manifests.Tables["ManifestTable"].Rows[0]["ID"].ToString();
                    DataSet details = manifestID.Length > 0 ? InboundManifestRead(manifestID) : null;
                    if(details != null && details.Tables["ManifestDetailTable"] != null && details.Tables["ManifestDetailTable"].Rows.Count > 0) {
                        items = int.Parse(details.Tables["ManifestDetailTable"].Compute("Sum(Cartons)", "").ToString());
                    }
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
            return items;
        }
    }
}