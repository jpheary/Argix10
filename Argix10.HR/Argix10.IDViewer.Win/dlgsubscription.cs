using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Terminals;

namespace Argix.HR {
    //
    public partial class dlgSubscription:Form {
        //Members

        //Interface
        public dlgSubscription() {
            //Constructor
            InitializeComponent();
        }
        public string RouteClass { get { return ((Argix.Terminals.Depot)this.cboRouteClass.SelectedItem).Class; } }
        public string Driver { get { return ((Argix.Terminals.Driver)this.cboDriver.SelectedItem).Name; } }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.cboRouteClass.DisplayMember = "Name";
                this.cboRouteClass.ValueMember = "Number";
                this.cboDriver.DisplayMember = "Name";
                this.cboDriver.ValueMember = "Name";
                
                this.cboRouteClass.DataSource = RoadshowGateway.GetDepots();
                this.cboRouteClass.SelectedIndex = 0;
                OnRouteClassChanged(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnRouteClassChanged(object sender,EventArgs e) {
            //Event handler for change in selected route class
            this.Cursor = Cursors.WaitCursor;
            try {
                if(this.cboRouteClass.SelectedValue != null)
                    this.cboDriver.DataSource = RoadshowGateway.GetDrivers(int.Parse(this.cboRouteClass.SelectedValue.ToString()));
                if (this.cboDriver.Items.Count > 0) this.cboDriver.SelectedIndex = 0;
                OnDriverChanged(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnDriverChanged(object sender,EventArgs e) {
            //Event handler for change in driver
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnValidateForm(object sender,System.EventArgs e) {
            //Event handler for close button clicked
            this.Cursor = Cursors.WaitCursor;
            try {
                this.btnCancel.Enabled = true;
                this.btnOk.Enabled = this.cboRouteClass.SelectedItem != null && this.cboDriver.SelectedItem != null;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnOk(object sender,EventArgs e) {
            this.Cursor = Cursors.WaitCursor;
            try {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnCancel(object sender,EventArgs e) {
            try {
                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
    }
}
