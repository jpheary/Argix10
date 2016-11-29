using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;
using Argix.Windows;

namespace Argix.Freight {
    //
    public partial class TDSInfoTool : UserControl {
        //Members
        private UltraGridSvc mGridSvc = null;

        //Interface
        public TDSInfoTool() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdTDSInfo);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    #region Grid customizations from normal layout (to support cell editing)
                    this.grdTDSInfo.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                    this.grdTDSInfo.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                    this.grdTDSInfo.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                    this.grdTDSInfo.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                    this.grdTDSInfo.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                    this.grdTDSInfo.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                    this.grdTDSInfo.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                    this.grdTDSInfo.DisplayLayout.Override.MaxSelectedCells = 1;
                    this.grdTDSInfo.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
                    this.grdTDSInfo.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
                    this.grdTDSInfo.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                    #endregion

                    this.dtpStartDate.MinDate = this.dtpEndDate.MinDate = DateTime.Today.AddDays(-90);
                    this.dtpStartDate.MaxDate = this.dtpEndDate.MaxDate = DateTime.Today;
                    this.dtpStartDate.Value = this.dtpEndDate.MinDate = DateTime.Today;

                    this.mClients.Merge(FreightGateway.GetClients());
                    if(this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                    OnClientChanged(null, EventArgs.Empty);
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnClientChanged(object sender, EventArgs e) {
            //
            try {
                this.mTDSInfo.Clear();
                this.grdTDSInfo.DataBind();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnDateChanged(object sender, EventArgs e) {
            //
            try {
                this.mTDSInfo.Clear();
                this.grdTDSInfo.DataBind();
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
            finally { OnValidateForm(null, EventArgs.Empty); }
        }
        private void OnValidateForm(object sender, EventArgs e) {
            //Set user services
            try {
                //Set services
                this.btnFind.Enabled = true;
                this.btnClear.Enabled = true;
            }
            catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
        }
        private void OnCommandClick(object sender, System.EventArgs e) {
            //Event handler for button selection
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnFind":
                        this.Cursor = Cursors.WaitCursor;
                        this.mTDSInfo.Clear();
                        this.mTDSInfo.Merge(FreightGateway.GetTDSInfo(this.cboClient.SelectedValue.ToString(), this.dtpStartDate.Value, this.dtpEndDate.Value, this.txtVendorName.Text.Trim()));
                        this.grdTDSInfo.DataBind();
                        break;
                    case "btnClear":
                        this.mTDSInfo.Clear();
                        this.grdTDSInfo.DataBind();
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
