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
    public partial class PointsWindowsControl:UserControl {
        //Members
        private DeliveryPointsDataset.DeliveryPointTableRow mPoint = null;
        private System.Windows.Forms.ToolTip mToolTip = null;

        //Interface
        public PointsWindowsControl() {
            //Constructor
            try {
                InitializeComponent();
                this.mToolTip = new System.Windows.Forms.ToolTip();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        public DeliveryPointsDataset.DeliveryPointTableRow Point { 
            get { return this.mPoint; } 
            set { 
                this.mPoint = value;
                if (this.mPoint != null) {
                    this.lblCustM.Text = !this.mPoint.IsMondayWindowNull() ? this.mPoint.OpenTimeMonday + "-" + this.mPoint.CloseTimeMonday : "";
                    this.lblCustTu.Text = !this.mPoint.IsTuesdayWindowNull() ? this.mPoint.OpenTimeTuesday + "-" + this.mPoint.CloseTimeTuesday : "";
                    this.lblCustW.Text = !this.mPoint.IsWednesdayWindowNull() ? this.mPoint.OpenTimeWednesday + "-" + this.mPoint.CloseTimeWednesday : "";
                    this.lblCustTh.Text = !this.mPoint.IsThursdayWindowNull() ? this.mPoint.OpenTimeThursday + "-" + this.mPoint.CloseTimeThursday : "";
                    this.lblCustF.Text = !this.mPoint.IsFridayWindowNull() ? this.mPoint.OpenTimeFriday + "-" + this.mPoint.CloseTimeFriday : "";
                    this.lblCustSa.Text = !this.mPoint.IsSaturdayWindowNull() ? this.mPoint.OpenTimeSaturday + "-" + this.mPoint.CloseTimeSaturday : "";
                    this.lblCustSu.Text = !this.mPoint.IsSundayWindowNull() ? this.mPoint.OpenTimeSunday + "-" + this.mPoint.CloseTimeSunday : "";

                    this.lblStopM.Text = !this.mPoint.IsStopMondayWindowNull() ? this.mPoint.StopOpenTimeMonday + "-" + this.mPoint.StopCloseTimeMonday : "";
                    this.lblStopTu.Text = !this.mPoint.IsStopTuesdayWindowNull() ? this.mPoint.StopOpenTimeTuesday + "-" + this.mPoint.StopCloseTimeTuesday : "";
                    this.lblStopW.Text = !this.mPoint.IsStopWednesdayWindowNull() ? this.mPoint.StopOpenTimeWednesday + "-" + this.mPoint.StopCloseTimeWednesday : "";
                    this.lblStopTh.Text = !this.mPoint.IsStopThursdayWindowNull() ? this.mPoint.StopOpenTimeThursday + "-" + this.mPoint.StopCloseTimeThursday : "";
                    this.lblStopF.Text = !this.mPoint.IsStopFridayWindowNull() ? this.mPoint.StopOpenTimeFriday + "-" + this.mPoint.StopCloseTimeFriday : "";
                    this.lblStopSa.Text = !this.mPoint.IsStopSaturdayWindowNull() ? this.mPoint.StopOpenTimeSaturday + "-" + this.mPoint.StopCloseTimeSaturday : "";
                    this.lblStopSu.Text = !this.mPoint.IsStopSundayWindowNull() ? this.mPoint.StopOpenTimeSunday + "-" + this.mPoint.StopCloseTimeSunday : "";
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
