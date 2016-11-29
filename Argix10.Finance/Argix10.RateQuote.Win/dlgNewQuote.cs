using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix {
    //
    public partial class dlgNewQuote:Form {
        //Members

        //Interface
        public dlgNewQuote() {
            //Constructor
            InitializeComponent();
        }
        public string QuoteType { get { return this.cboRequestType.SelectedItem.ToString(); } }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.cboRequestType.SelectedIndex = 0;
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Validate services
            this.btnCancel.Enabled = true;
            this.btnOk.Enabled = this.cboRequestType.SelectedItem != null;
        }
        private void OnButtonClick(object sender,EventArgs e) {
            //Event handle for button click event
            Button button = (Button)sender;
            switch (button.Name) {
                case "btnCancel":
                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    break;
                case "btnOk":
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                    break;
            }
        } 

    }
}
