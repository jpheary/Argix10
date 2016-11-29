using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Microsoft.Reporting.WinForms;

namespace Argix.Freight {
    //
    public partial class TrackingTool:UserControl {
        //Members

        //Interface
        public TrackingTool() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnControlLoad(object sender,EventArgs e) {
            //Event handler for control load event
            this.Cursor = Cursors.WaitCursor;
            try {
                if (!this.DesignMode) {
                    #region Grid customizations from normal layout (to support cell editing)
                    this.grdSummary.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                    this.grdSummary.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                    this.grdSummary.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                    this.grdSummary.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                    this.grdSummary.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                    this.grdSummary.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                    this.grdSummary.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                    this.grdSummary.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
                    this.grdSummary.DisplayLayout.Override.MaxSelectedCells = 1;
                    this.grdSummary.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                    this.grdSummary.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                    this.grdSummary.DisplayLayout.Bands[0].Columns["ItemNumber"].SortIndicator = SortIndicator.Ascending;
                    #endregion
                    if(this.cboItemType.Items.Count > 0) this.cboItemType.SelectedIndex = 0;
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnItemTypeChanged(object sender,EventArgs e) {
            //Event handler for change in item type
            try {
                this.txtItems.Clear();
                this.bsItems.Clear();
                this.rsDetail.Clear();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnItemTextChanged(object sender,EventArgs e) {
            //
            try {
                this.bsItems.Clear();
                this.rsDetail.Clear();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnItemKeyUp(object sender, KeyEventArgs e) {
            //Event handler item keyup event
            if(e.KeyCode == Keys.Enter) this.btnTrack.PerformClick();
        }
        private void OnGridMouseDown(object sender, System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Set menu and toolbar services
                UltraGrid grid = (UltraGrid)sender;
                grid.Focus();
                UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if (uiElement != null) {
                    object context = uiElement.GetContext(typeof(UltraGridRow));
                    if (context != null) {
                        UltraGridRow row = (UltraGridRow)context;
                        if (e.Button == MouseButtons.Right) row.Activate();
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active row when in a scroll region to prevent double-click action
                        if (uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement)) grid.ActiveRow = null;
                        else if (uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement)) grid.ActiveRow = null;
                    }
                }
            }
            catch (Exception ex) { App.ReportError(ex); }
        }
        private void OnSummaryAfterRowActivate(object sender,EventArgs e) {
            //
            try {
                Argix.Enterprise.TrackingItem item = (Argix.Enterprise.TrackingItem)this.bsItems.Current;
                ReportParameter p1 = new ReportParameter("PalletNumber",item.CartonNumber);
                this.rsDetail.ServerReport.DisplayName = "Tracking Detail";
                this.rsDetail.ServerReport.ReportPath = "/Freight/Pallet Shipment Tracking";
                this.rsDetail.ServerReport.SetParameters(new ReportParameter[] { p1 });
                this.rsDetail.RefreshReport();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnTrackCartons(object sender,EventArgs e) {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                this.bsItems.Clear();
                this.rsDetail.Clear();
                Enterprise.TrackingItems items = Argix.Enterprise.EnterpriseGateway.TrackShipment(this.txtItems.Text.Trim());
                if(items.Count > 0)
                    this.bsItems.DataSource = items;
                else
                    MessageBox.Show("No items found for this shipment.", App.Product, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
