using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.ServiceModel;
using System.Threading;
using System.Web;
using System.Web.Profile;
using System.Web.Security;
using Argix;

public class EnterpriseGateway {
	//Members
        
    //Interface
    public EnterpriseGateway() { }

    public TrackingDataset TrackCartons(string[] itemNumbers,string clientNumber,string vendorNumber) {
        //Track items by customer carton number
        TrackingDataset items = new TrackingDataset();
        ConsumerTrackingServiceClient client = null;
        try {
            client = new ConsumerTrackingServiceClient();
            items.Merge(client.TrackCartons(itemNumbers,clientNumber,vendorNumber));
        }
        catch (TimeoutException te) { client.Abort(); throw new ApplicationException(te.Message); }
        catch (FaultException<TrackingFault> tf) { client.Abort(); throw new ApplicationException(tf.Detail.Message); }
        catch (FaultException fe) { client.Abort(); throw new ApplicationException(fe.Message); }
        catch (CommunicationException ce) { client.Abort(); throw new ApplicationException(ce.Message); }
        finally { client.Close(); }
        return items;
    }
}
