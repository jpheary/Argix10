using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix.Freight {
    //
    public partial class frmLoadTender:Form {
        //Members
        LoadTenderDS.LoadTenderTableRow mLoadTender = null;
        
        //Interface
        public frmLoadTender(LoadTenderDS.LoadTenderTableRow loadTender) {
            InitializeComponent();
            this.mLoadTender = loadTender;
        }

        private void OnFormLoad(object sender,EventArgs e) {
            //
            this._lblBarcode1.Text = this.lblBarcode1.Text = this.mLoadTender.Barcode1;
            this._lblBarcode2.Text = this.lblBarcode2.Text = this.mLoadTender.Barcode2;
        }

        private void OnPrint(object sender,EventArgs e) {
            //
        }
    }
}
