using System;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Argix.Terminals {
	//
    public class RoadshowExporter {
        //Members

        //Interface
        public RoadshowExporter() { }
        public bool Export(string exportFile, DispatchDataset requests) {
            //Export pickup requests to the specified file
            bool exported = false;
            StreamWriter writer = null;
            try {
                    //Validate file is unique
                    //if(File.Exists(exportFile)) throw new Exception("Export file " + exportFile + " already exists. ");

                    //Create the new file and save pickup requests
                    writer = new StreamWriter(new FileStream(exportFile,FileMode.Create,FileAccess.ReadWrite));
                    writer.BaseStream.Seek(0,SeekOrigin.Begin);
                    for (int j = 0;j < requests.PickupLogTable.Rows.Count;j++) {
                        string order = formatOrderRecord(requests.PickupLogTable[j]);
                        if(order.Length > 0) writer.WriteLine(order);
                    }
                    writer.Flush();
                    exported = true;
                }
            catch(ApplicationException ex) { throw ex; }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            finally { if(writer != null) writer.Close(); }
            return exported;
        }
        private string formatOrderRecord(DispatchDataset.PickupLogTableRow request) {
            //
            #region Roadshow import format
            //061212201901079200002P00012002509001700A00010000002500000000002Rready after 11am                                  
            //061312C0680100734    P00012002408001000A00015000000750000000002Rbright light brite light                          
            //070712300101018030020P00012002308001200A00003000000300000000002Rcancelled                                         
            //061212L0011301532    P00012002209001700A00002000000250000000002Rwaxonwaxoff                                       
            //070612M0480101827    P00012001509001500A00015000001000000000002Rtrash                                             
            //------=----------====----------====----=-----========----------=--------------------------------------------------
            //|     ||         |   |         |   |   ||    |       |         ||
            //a     bc         d   e         f   g   hi    j       k         lm

            //a) Routing Date [6] - MMddyy
            //b) Routing Class [1]- Roadshow Routing class number (i.e. 2,3,4,C,A,L,M)
            //c) Customer Account Number [10] - 10 digit Tsort accountID
            //d) Customer SubAccount Number [4]- last 4 digits of 14 digit ISA accountID (or blanks for Tsort accountID)
            //e) Order Number [10] - pickup request ID
            //f) Opening Time [4] - military time
            //g) Closing Time [4] - military time
            //h) Commodity Type [1] - assume commodity category Books with A: carton, Z: pallet
            //i) Order Size [5]
            //j) Order Weight [8]
            //k) Order Volume [10] - assume carton: 2cuft, pallet: 80cuft
            //l) Order Type [1] - B (backhaul), D (delivery), F (follow-up), R (return), T (target stop)
            //m) Comments [50]
            #endregion
            string order = "";
            try {
                if (!request.IsShipperNumberNull() && request.ShipperNumber.Trim().Length > 1) {
                    order =
                        request.ScheduleDate.ToString("MMddyy") +
                        getRoutingClass(request.TerminalNumber) +
                        (request.ShipperNumber.Trim().Length >= 10 ? request.ShipperNumber.Trim().Substring(0,10) : request.ShipperNumber.Trim().PadRight(10,' ')) +
                        (request.ShipperNumber.Trim().Length == 14 ? request.ShipperNumber.Trim().Substring(10,4) : "    ") +
                        "P" + request.RequestID.ToString().PadLeft(9,'0').Substring(0,9) +
                        request.WindowOpen.ToString().PadLeft(4,'0').Substring(0,4) +
                        request.WindowClose.ToString().PadLeft(4,'0').Substring(0,4) +
                        (request.AmountType == "Pallets" ? "Z" : "A") +
                        request.Amount.ToString().PadLeft(5,'0').Substring(0,5) +
                        request.Weight.ToString().PadLeft(8,'0').Substring(0,8) +
                        (request.AmountType == "Pallets" ? (80 * request.Amount).ToString("0000000000") : (2 * request.Amount).ToString("0000000000")) +
                        request.OrderType +
                        request.Comments.PadRight(50,' ').Substring(0,50);
                }
                else {
                    string message = "Pickup request #" + request.RequestID.ToString() + " is does not have a shipper number for shipper " + request.Shipper + ". This pickup will be omitted from the export file.";
                    System.Windows.Forms.MessageBox.Show(message,"Dispatch Export",System.Windows.Forms.MessageBoxButtons.OK);
                }
            }
            catch(Exception ex) { throw new ApplicationException(ex.Message,ex); }
            return order;
        }
        private string getRoutingClass(string terminalNumber) {
            //Change terminalNumber to routing class
            string _class=" ";
            Depots depots = TerminalGateway.GetDepots();
            foreach(Depot depot in depots) {
                if(depot.Number == int.Parse(terminalNumber)) { _class = depot.Class; break; }
            }
            return _class;
        }
    }
}
