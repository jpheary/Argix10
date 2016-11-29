using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Argix.Enterprise;

namespace Argix.AgentLineHaul {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.Allowed)]
    public class AgentLineHaulService : IAgentLineHaulService {
        //Members
               
        //Interface
        public AgentLineHaulService() { }

        public DataSet InboundManifestsView(string clientNumber, string clientDivision, DateTime startPickupDate, DateTime endPickupDate) {
            //Get a view of manifests for the specified client and date range
            DataSet manifests = new DataSet();
            try {
                DataSet ds = new EnterpriseRGateway().InboundManifestsView(clientNumber, clientDivision, startPickupDate, endPickupDate);
                if(ds != null) manifests.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<AgentLineHaulFault>(new AgentLineHaulFault(ex.Message), "Service Error"); }
            return manifests;
        }
        public DataSet InboundManifestRead(string manifestID) {
            //Return a single inbound manifest
            DataSet manifests = new DataSet();
            try {
                DataSet ds = new EnterpriseRGateway().InboundManifestRead(manifestID);
                if(ds != null) manifests.Merge(ds);
            }
            catch(Exception ex) { throw new FaultException<AgentLineHaulFault>(new AgentLineHaulFault(ex.Message), "Service Error"); }
            return manifests;
        }
    }
}
