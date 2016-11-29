using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace Argix.Enterprise {
    //Enterprise Interfaces

    [DataContract]
    public class EnterpriseFault {
        private string _messsage;
        public EnterpriseFault(string message) { this._messsage = message; }

        [DataMember]
        public string Message { get { return this._messsage; } set { this._messsage = value; } }
    }
}
