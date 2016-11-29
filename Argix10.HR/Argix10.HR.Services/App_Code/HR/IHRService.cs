using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.HR {
    // 
    [DataContract]
    public class HRFault {
        private string mMessage = "";
        public HRFault(string message) { this.mMessage = message; }
        [DataMember]
        public string Message { get { return this.mMessage; } set { this.mMessage = value; } }
    }
}
