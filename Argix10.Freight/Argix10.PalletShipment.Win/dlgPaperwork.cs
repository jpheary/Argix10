using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Argix.Freight {
    //
    public partial class dlgPaperwork:Form {
        //Members
        private string mShipmentNumber = "";

        //Interface
        public dlgPaperwork(string shipmentNumber) {
            //
            InitializeComponent();
            this.mShipmentNumber = shipmentNumber;
        }
        private void OnFormLoad(object sender,System.EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                ReportParameter p1 = new ReportParameter("ShipmentNumber",this.mShipmentNumber);

                this.rsDialog.ServerReport.DisplayName = "Pallet Shipment Paperwork";
                this.rsDialog.ServerReport.ReportPath = "/Freight/Pallet Shipment Paperwork";
                this.rsDialog.ServerReport.SetParameters(new ReportParameter[] { p1 });
                this.rsDialog.RefreshReport();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
