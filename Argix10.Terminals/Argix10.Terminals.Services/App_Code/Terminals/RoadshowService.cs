using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using Argix.Enterprise;

namespace Argix.Terminals {
    //
    [ServiceBehavior(InstanceContextMode=InstanceContextMode.PerCall)]
    public class RoadshowService:IRoadshowService {
        //Members

        //Interface
        public RoadshowService() { }

        public Customers GetCustomers() {
            //
            Customers customers = new Customers();
            try {
                DataSet ds = new RoadshowGateway().GetDispatchCustomers();
                if (ds != null) {
                    RoadshowDataset _customers = new RoadshowDataset();
                    _customers.Merge(ds);
                    for (int i = 0;i < _customers.CustomerTable.Rows.Count;i++)
                        customers.Add(new Customer(_customers.CustomerTable[i]));
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return customers;
        }
        public Customers2 GetCustomers2() {
            //
            Customers2 customers = new Customers2();
            try {
                DataSet ds = new RoadshowGateway().GetDispatchCustomers2();
                if (ds != null) {
                    RoadshowDataset _customers = new RoadshowDataset();
                    _customers.Merge(ds);
                    for (int i = 0;i < _customers.CustomerTable.Rows.Count;i++)
                        customers.Add(new Customer2(_customers.CustomerTable[i]));
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return customers;
        }
        public Depots GetDepots() {
            //
            Depots depots = new Depots();
            try {
                DataSet ds = new RoadshowGateway().GetDispatchDepots();
                if (ds != null) {
                    RoadshowDataset _depots = new RoadshowDataset();
                    _depots.Merge(ds);
                    for (int i = 0;i < _depots.DepotTable.Rows.Count;i++)
                        depots.Add(new Depot(_depots.DepotTable[i]));
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return depots;
        }
        public Drivers GetDrivers(int depotNumber) {
            //
            Drivers drivers = new Drivers();
            try {
                DataSet ds = new RoadshowGateway().GetDispatchDrivers(depotNumber);
                if (ds != null) {
                    RoadshowDataset _drivers = new RoadshowDataset();
                    _drivers.Merge(ds);
                    for(int i = 0; i < _drivers.DriverTable.Rows.Count; i++)
                        drivers.Add(new Driver(_drivers.DriverTable[i]));
                }
            }
            catch (ApplicationException aex) { throw new FaultException<RoadshowFault>(new RoadshowFault(aex.Message),"Gateway Error"); }
            catch (Exception ex) { throw new FaultException<RoadshowFault>(new RoadshowFault(ex.Message),"Unexpected Error"); }
            return drivers;
        }
    }
}
