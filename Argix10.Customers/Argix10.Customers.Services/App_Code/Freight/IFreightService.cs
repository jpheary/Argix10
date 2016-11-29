using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Freight {
    //
    [ServiceContract(Namespace="http://Argix.Freight")]
    public interface IFreightService {
        [OperationContract]
        DataSet GetDeliveries(int companyID,int storeNumber,DateTime from,DateTime to);

        [OperationContract]
        DataSet GetDelivery(int companyID,int storeNumber,DateTime from,DateTime to,long proID);

        [OperationContract]
        DataSet GetOSDScans(long cProID);

        [OperationContract]
        DataSet GetPODScans(long cProID);
    }
}
