using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Transactions;
using System.Threading;
using Argix.Enterprise;

namespace Argix.Customers {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall,IncludeExceptionDetailInFaults=true)]
    public class CRMSystemService:ICRMSystemService {
        //Members

        //Interface
        public CRMSystemService() { }
        public long CreateIssue(CRMIssue issue) {
            //Create a new issue
            long iid = 0;
            try {
                //Create the TransactionScope to execute the commands, guaranteeing that both commands can commit or roll back as a single unit of work
                using(TransactionScope scope = new TransactionScope()) {
                
                //Create issue
                object io = new CRMGateway().CreateIssue(issue.TypeID,issue.Subject,issue.Contact,issue.CompanyID,issue.RegionNumber,issue.DistrictNumber,issue.AgentNumber,issue.StoreNumber);
                iid = (long)io;

                //Add the single 'Open' action
                bool ok = new CRMGateway().CreateAction(issue.ActionTypeID,iid,issue.UserID,issue.Comment);
                
                //Commits the transaction; if an exception is thrown, Complete is not called and the transaction is rolled back
                scope.Complete();
                }
            }
            catch(ApplicationException aex) { throw new FaultException<CustomersFault>(new CustomersFault(aex.Message),"Gateway Error"); }
            catch(Exception ex) { throw new FaultException<CustomersFault>(new CustomersFault(ex.Message),"Unexpected Error"); }
            return iid;
        }
    }
}