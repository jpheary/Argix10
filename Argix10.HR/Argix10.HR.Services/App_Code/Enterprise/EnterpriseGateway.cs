using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Threading;

namespace Argix.Enterprise {
	//
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    public class EnterpriseGateway {
        //Members
        public const string SQL_CONNID = "Enterprise";

        //Interface
        public EnterpriseGateway() { }
    }
}