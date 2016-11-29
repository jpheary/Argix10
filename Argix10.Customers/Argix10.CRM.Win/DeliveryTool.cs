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

namespace Argix.Customers {
    //
    public partial class DeliveryTool:UserControl {
        //Members

        //Interface
        public DeliveryTool() {
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
                #region Grid customizations from normal layout (to support cell editing)
                this.grdDeliveries.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdDeliveries.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdDeliveries.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdDeliveries.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdDeliveries.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdDeliveries.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdDeliveries.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                this.grdDeliveries.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdDeliveries.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdDeliveries.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdDeliveries.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                //this.grdDeliveries.DisplayLayout.Bands[0].Columns["CBOL"].SortIndicator = SortIndicator.Descending;
                #endregion
                if (!this.DesignMode) {
                    this.mClients.Merge(CRMGateway.GetCompanies());
                    if (this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                }
                this.dtpFrom.Value = DateTime.Today.AddDays(-7);
                this.dtpTo.Value = DateTime.Today;
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnClientChanged(object sender,EventArgs e) {
            //Event handler for change in company
            try {
                this.mskStoreNumber.Text = "";
                this.mDeliveries.Clear();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnDeliverydatesChanged(object sender,EventArgs e) {
            //Event handler for from/to date value changed event
            try {
                OnView(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnStoreNumberKeyUp(object sender,KeyEventArgs e) {
            //
            try {
                if (e.KeyCode == Keys.Enter) OnView(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
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
        private void OnView(object sender,EventArgs e) {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mDeliveries.Clear();
                if (this.cboClient.SelectedValue != null && this.mskStoreNumber.Text.Trim().Length > 0)
                    this.grdDeliveries.DataSource = CRMGateway.GetDeliveries(int.Parse(this.cboClient.SelectedValue.ToString()),int.Parse(this.mskStoreNumber.Text),this.dtpFrom.Value,this.dtpTo.Value);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
