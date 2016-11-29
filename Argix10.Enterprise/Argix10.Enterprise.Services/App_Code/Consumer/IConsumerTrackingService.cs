using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ServiceModel;
using System.Runtime.Serialization;
using System.Text;

namespace Argix {
    // 
    [ServiceContract(Namespace = "http://Argix")]
    public interface IConsumerTrackingService {
        [OperationContract]
        [FaultContractAttribute(typeof(TrackingFault),Action = "http://Argix.TrackingFault")]
        TrackingDataset TrackCartons(string[] cartonNumbers,string clientNumber,string vendorNumber);
    }

    [DataContract]
    public class TrackingSummaryItem {
        //Members
        private string mItemNumber="";

        //Interface
        public TrackingSummaryItem(string itemNumber) : this(itemNumber,null) { }
        public TrackingSummaryItem(string itemNumber,TrackingDataset.TrackingSummaryTableRow carton) {
            //Constructor
            this.mItemNumber = itemNumber;
        }
        #region Members [...]
        [DataMember]
        public string ItemNumber { get { return this.mItemNumber; } set { this.mItemNumber = value; } }
        #endregion
    }

    [DataContract]
    public class TrackingDetailItem {
        //Members
        private string mItemNumber="";

        //Interface
        public TrackingDetailItem(string itemNumber) : this(itemNumber,null) { }
        public TrackingDetailItem(string itemNumber,TrackingDataset.TrackingDetailTableRow carton) {
            //Constructor
            this.mItemNumber = itemNumber;
        }
        #region Members [...]
        [DataMember]
        public string ItemNumber { get { return this.mItemNumber; } set { this.mItemNumber = value; } }
        #endregion
    }
}
