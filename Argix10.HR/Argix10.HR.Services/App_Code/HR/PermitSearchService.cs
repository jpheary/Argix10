using System;
using System.Collections;
using System.Configuration;
using System.Diagnostics;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel;
using System.ServiceModel.Activation;
using Argix.Enterprise;

namespace Argix.HR {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode=AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class PermitSearchService: IPermitSearchService {
        //Members

        //Interface
        public PermitSearchService() { }
        public DataSet FindPermitsByNumber(string number) {
            //Find a permit; can return more than one object??
            DataSet permits = null;
            try {
                permits = new PermitGateway().FindPermitsByNumber(number);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permits;
        }
        public DataSet FindPermitsByPlate(string issueState, string plateNumber) {
            //Find a permit; can return more than one object??
            DataSet permits = null;
            try {
                permits = new PermitGateway().FindPermitsByPlate(issueState, plateNumber);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permits;
        }
        public DataSet FindPermitsByVehicle(string year, string make, string model, string color) {
            //Find a permit; can return more than one object??
            DataSet permits = null;
            try {
                permits = new PermitGateway().FindPermitsByVehicle(year, make, model, color);
            }
            catch(Exception ex) { throw new FaultException<HRFault>(new HRFault(ex.Message), "Service Error"); }
            return permits;
        }
    }
}
