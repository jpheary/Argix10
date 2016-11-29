using System;
using System.Net;
using System.Threading;
using System.IO.Ports;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Argix.Freight {
    //
    public class ShippersLoader {
        //Members
        private BackgroundWorker mWorker = null;

        //Interface
        public ShippersLoader() { 
            //Constructor
            this.mWorker = new BackgroundWorker();
            this.mWorker.DoWork += new DoWorkEventHandler(OnLoadShippers);
            this.mWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(OnRunWorkerCompleted);
            this.mWorker.RunWorkerAsync();
        }
        private void OnLoadShippers(object sender,DoWorkEventArgs e) {
            //Load shippers on a background thread; return data in e.Result object
            try {
                Argix.Terminals.Customers2 customers = Argix.Terminals.TerminalGateway.GetCustomers2(null);
                e.Result = customers;
            }
            catch { }
        }
        private void OnRunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {
            //Back to the main thread with the result
            try {
                Argix.Terminals.Customers2 customers = (Argix.Terminals.Customers2)e.Result;
            }
            catch { }
        }
    }
}
