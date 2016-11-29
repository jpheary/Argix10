using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;

namespace Argix {
    //
    public partial class dlgTrackingDetail:Form {
        //Members
        private string mLabelNumber = "";

        //Interface
        public dlgTrackingDetail(string labelNumber) {
            //Constructor
            InitializeComponent();
            this.mLabelNumber = labelNumber;
        }

        private void OnLoad(object sender,EventArgs e) {
            //Load event handler
            try {
                Argix.Enterprise.TrackingItems items = Argix.Enterprise.EnterpriseGateway.TrackCartonsByLabelNumber(new string[] { this.mLabelNumber });
                string snull = null;
                ReportParameter p1 = new ReportParameter("Cartons",items[0].CartonNumber);
                ReportParameter p2 = new ReportParameter("ClientNumber",items[0].Client);
                ReportParameter p3 = new ReportParameter("VendorNumber",snull);
                this.rsDetail.ServerReport.DisplayName = "Tracking Detail";
                this.rsDetail.ServerReport.ReportPath = "/Customer Service/CRM Tracking Detail";
                this.rsDetail.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3 });
                this.rsDetail.RefreshReport();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
    }
}
