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

namespace Argix.Customers {
    //
    public partial class StoreTrackingTool:UserControl {
        //Members
        private EnterpriseDataset mDetail = null;

        //Interface
        public StoreTrackingTool() {
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
                this.grdTLs.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdTLs.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdTLs.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdTLs.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdTLs.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdTLs.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdTLs.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                this.grdTLs.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdTLs.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdTLs.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdTLs.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                //this.grdTLs.DisplayLayout.Bands[0].Columns["CBOL"].SortIndicator = SortIndicator.Descending;

                this.grdCartons.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdCartons.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdCartons.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdCartons.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdCartons.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdCartons.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdCartons.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                this.grdCartons.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdCartons.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdCartons.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdCartons.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                //this.grdCartons.DisplayLayout.Bands[0].Columns["CBOL"].SortIndicator = SortIndicator.Descending;
                #endregion
                if (!this.DesignMode) {
                    this.mClients.Merge(Argix.Enterprise.EnterpriseGateway.GetClients(null));
                    if (this.cboClient.Items.Count > 0) this.cboClient.SelectedIndex = 0;
                }
                this.cboDateType.SelectedIndex = 0;
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
                this.mTLs.Clear();
                this.mCartons.Clear();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnDeliverydatesChanged(object sender,EventArgs e) {
            //Event handler for from/to date value changed event
            try {
                OnTrack(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnDateTypeChanged(object sender,EventArgs e) {
            //Event handler for change in date type
            try {
                this.mTLs.Clear();
                this.mCartons.Clear();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnStoreNumberKeyUp(object sender,KeyEventArgs e) {
            //
            try {
                if (e.KeyCode == Keys.Enter) OnTrack(null,EventArgs.Empty);
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
        private void OnTLAfterRowActivate(object sender,EventArgs e) {
            try {
                this.mCartons.Clear();
                string tl = this.grdTLs.ActiveRow.Cells["TL"].Value.ToString();
                this.mCartons.Merge(this.mDetail.CartonDetailForStoreTable.Select("TL='" + tl + "'"));
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnCartonDoubleClicked(object sender,EventArgs e) {
            try {
                string lbl = this.grdCartons.ActiveRow.Cells["LBLNo"].Value.ToString();
                dlgTrackingDetail dlg = new dlgTrackingDetail(lbl);
                dlg.ShowDialog();
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
        }
        private void OnTrack(object sender,EventArgs e) {
            //
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mTLs.Clear();
                this.mCartons.Clear();
                this.mDetail = new EnterpriseDataset();
                if (!this.DesignMode) {
                    //DataSet ds = Argix.Enterprise.EnterpriseGateway.TrackCartonsForStoreByDeliveryDate(this.cboClient.SelectedValue.ToString(),this.mskStoreNumber.Text,this.dtpFrom.Value,this.dtpTo.Value,null);
                    if (this.cboDateType.Text == "Delivery")
                        this.mDetail.Merge(Argix.Enterprise.EnterpriseGateway.TrackCartonsForStoreByDeliveryDate(this.cboClient.SelectedValue.ToString(),this.mskStoreNumber.Text,this.dtpFrom.Value,this.dtpTo.Value,null));
                    else
                        this.mDetail.Merge(Argix.Enterprise.EnterpriseGateway.TrackCartonsForStoreByPickupDate(this.cboClient.SelectedValue.ToString(),this.mskStoreNumber.Text,this.dtpFrom.Value,this.dtpTo.Value,null));

                    EnterpriseDataset summary = new EnterpriseDataset();
                    summary.Merge(this.mDetail.CartonDetailForStoreTable.DefaultView.ToTable(true,new string[] { "TL" }));
                    foreach (EnterpriseDataset.CartonDetailForStoreTableRow row in summary.CartonDetailForStoreTable.Rows) {
                        row.CartonCount = this.mDetail.CartonDetailForStoreTable.Select("TL='" + row.TL + "'").Length;
                        row.Weight = int.Parse(this.mDetail.CartonDetailForStoreTable.Compute("Sum(weight)","TL='" + row.TL + "'").ToString());
                        object minDate = this.mDetail.CartonDetailForStoreTable.Compute("Min(PodDate)","TL='" + row.TL + "' AND (IsNull(PodDate,#01/01/1900#) <> #01/01/1900#)");

                        EnterpriseDataset.CartonDetailForStoreTableRow row0 = (EnterpriseDataset.CartonDetailForStoreTableRow)(this.mDetail.CartonDetailForStoreTable.Select("TL='" + row.TL + "'"))[0];
                        if (minDate != System.DBNull.Value)
                            row.PodDate = DateTime.Parse(minDate.ToString());
                        else {
                            if (!row0.IsOFD1Null()) row.OFD1 = row0.OFD1;
                        }
                        row.Zone = row0.Zone;
                        row.Store = row0.Store;
                        row.CBOL = row0.IsCBOLNull() ? "" : row0.CBOL;
                        row.AG = !row0.IsAGNull() ? row0.AG : "";
                        row.AgName = row0.Trf == "N" ? row0.AgName : row0.AgName + " (Transfer)";
                        row.AcceptChanges();
                    }
                    this.mTLs.Merge(summary);
                }
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
