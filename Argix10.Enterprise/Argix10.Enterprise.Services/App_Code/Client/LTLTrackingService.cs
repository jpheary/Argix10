using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;
using Argix.Customers;

namespace Argix {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class LTLTrackingService: ILTLTrackingService {
        //Members
      
        //Interface
        public LTLTrackingService() { }

        public TrackingItems TrackPalletShipment(string shipmentNumber) {
            //
            TrackingItems items = new TrackingItems();
            try {
                TrackingDataset pallets = new TrackingDataset();
                pallets.Merge(new EnterpriseRGateway().TrackLTLPallets(shipmentNumber));

                //Return records for all found items
                foreach(TrackingDataset.TrackingTableRow pallet in pallets.TrackingTable.Rows) {
                    TrackingItem item = new TrackingItem(pallet.CTN,pallet);
                    items.Add(item);
                }
            }
            catch(Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return items;
        }
    }
}
