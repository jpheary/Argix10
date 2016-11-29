using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Argix.Reporting;

namespace Argix {
    class Program {
        static void Main(string[] args) {

            SubscriptionDataset subs = ReportingGateway.GetSubscriptions();
            System.Diagnostics.Debug.Write(subs.GetXml());
            //string subscriptionID = ReportingGateway.CreateSubscription("0191","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0001","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0006","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0010","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0011","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0023","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0025","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0026","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0031","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0032","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0044","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0046","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0047","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0049","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0055","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0059","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0064","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0065","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0069","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0087","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0101","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0104","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0105","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0107","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0109","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0110","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0113","kdohl@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0126","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0129","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0130","jramos@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0132","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0134","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0139","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0140","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0143","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0145","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0146","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0152","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0189","wgarner@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0190","eroach@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("1017","dprimiano@argixlogistics.com");
            //subscriptionID = ReportingGateway.CreateSubscription("0004","wgarner@argixlogistics.com");
        }
    }
}
