using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix.Terminals {
    //
    public partial class RoadshowWindowsControl:UserControl {
        //Members
        private DeliveryPointsDataset.CustomerTableRow mCustomer = null;
        private System.Windows.Forms.ToolTip mToolTip = null;

        //Interface
        public RoadshowWindowsControl() {
            //Constructor
            try {
                InitializeComponent();
                this.mToolTip = new System.Windows.Forms.ToolTip();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        public DeliveryPointsDataset.CustomerTableRow Customer { 
            get { return this.mCustomer; } 
            set { 
                this.mCustomer = value;
                if (this.mCustomer != null) {
                    this.lblCustM.Text = !this.mCustomer.IsCustomerWindowOpenMondayNull() ? this.mCustomer.CustomerWindowOpenMonday + "-" + this.mCustomer.CustomerWindowCloseMonday : "";
                    this.lblCustTu.Text = !this.mCustomer.IsCustomerWindowOpenTuesdayNull() ? this.mCustomer.CustomerWindowOpenTuesday + "-" + this.mCustomer.CustomerWindowCloseTuesday : "";
                    this.lblCustW.Text = !this.mCustomer.IsCustomerWindowOpenWednesdayNull() ? this.mCustomer.CustomerWindowOpenWednesday + "-" + this.mCustomer.CustomerWindowCloseWednesday : "";
                    this.lblCustTh.Text = !this.mCustomer.IsCustomerWindowOpenThursdayNull() ? this.mCustomer.CustomerWindowOpenThursday + "-" + this.mCustomer.CustomerWindowCloseThursday : "";
                    this.lblCustF.Text = !this.mCustomer.IsCustomerWindowOpenFridayNull() ? this.mCustomer.CustomerWindowOpenFriday + "-" + this.mCustomer.CustomerWindowCloseFriday : "";
                    this.lblCustSa.Text = !this.mCustomer.IsCustomerWindowOpenSaturdayNull() ? this.mCustomer.CustomerWindowOpenSaturday + "-" + this.mCustomer.CustomerWindowCloseSaturday : "";
                    this.lblCustSu.Text = !this.mCustomer.IsCustomerWindowOpenSundayNull() ? this.mCustomer.CustomerWindowOpenSunday + "-" + this.mCustomer.CustomerWindowCloseSunday : "";

                    this.lblStopM.Text = !this.mCustomer.IsStopWindowOpenMondayNull() ? this.mCustomer.StopWindowOpenMonday + "-" + this.mCustomer.StopWindowCloseMonday : "";
                    this.lblStopTu.Text = !this.mCustomer.IsStopWindowOpenTuesdayNull() ? this.mCustomer.StopWindowOpenTuesday + "-" + this.mCustomer.StopWindowCloseTuesday : "";
                    this.lblStopW.Text = !this.mCustomer.IsStopWindowOpenWednesdayNull() ? this.mCustomer.StopWindowOpenWednesday + "-" + this.mCustomer.StopWindowCloseWednesday : "";
                    this.lblStopTh.Text = !this.mCustomer.IsStopWindowOpenThursdayNull() ? this.mCustomer.StopWindowOpenThursday + "-" + this.mCustomer.StopWindowCloseThursday : "";
                    this.lblStopF.Text = !this.mCustomer.IsStopWindowOpenFridayNull() ? this.mCustomer.StopWindowOpenFriday + "-" + this.mCustomer.StopWindowCloseFriday : "";
                    this.lblStopSa.Text = !this.mCustomer.IsStopWindowOpenSaturdayNull() ? this.mCustomer.StopWindowOpenSaturday + "-" + this.mCustomer.StopWindowCloseSaturday : "";
                    this.lblStopSu.Text = !this.mCustomer.IsStopWindowOpenSundayNull() ? this.mCustomer.StopWindowOpenSunday + "-" + this.mCustomer.StopWindowCloseSunday : "";
                }
                else {
                    this.lblCustM.Text = this.lblCustTu.Text = this.lblCustW.Text = this.lblCustTh.Text = this.lblCustF.Text = this.lblCustSa.Text = this.lblCustSu.Text = "";
                    this.lblStopM.Text = this.lblStopTu.Text = this.lblStopW.Text = this.lblStopTh.Text = this.lblStopF.Text = this.lblStopSa.Text = this.lblStopSu.Text = "";
                }
            } 
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    #region Set tooltips
                    this.mToolTip.InitialDelay = 500;
                    this.mToolTip.AutoPopDelay = 3000;
                    this.mToolTip.ReshowDelay = 1000;
                    this.mToolTip.ShowAlways = true;		//Even when form is inactve
                    this.mToolTip.SetToolTip(this.lblCustM,"Customer windows");
                    this.mToolTip.SetToolTip(this.lblStopM,"Stop windows");
                    #endregion
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
