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

namespace Argix.Customers {
    //
    public partial class CartonTrackingTool:UserControl {
        //Members

        //Interface
        public CartonTrackingTool() {
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
                this.grdSummary.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdSummary.DisplayLayout.Override.SelectTypeRow = SelectType.None;
                this.grdSummary.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdSummary.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdSummary.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdSummary.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdSummary.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                this.grdSummary.DisplayLayout.Override.CellClickAction = CellClickAction.EditAndSelectText;
                this.grdSummary.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdSummary.DisplayLayout.Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdSummary.DisplayLayout.Override.RowFilterAction = RowFilterAction.HideFilteredOutRows;
                //this.grdSummary.DisplayLayout.Bands[0].Columns["CBOL"].SortIndicator = SortIndicator.Descending;
                #endregion
                if (!this.DesignMode) {
                    if (this.cboItemType.Items.Count > 0) this.cboItemType.SelectedIndex = 0;
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
        private void OnSummaryAfterRowActivate(object sender,EventArgs e) {
            //
            try {
                Argix.Enterprise.TrackingItem item = (Argix.Enterprise.TrackingItem)this.bsItems.Current;
                string snull = null;
                ReportParameter p1 = new ReportParameter("Cartons",item.CartonNumber);
                ReportParameter p2 = new ReportParameter("ClientNumber",item.Client);
                ReportParameter p3 = new ReportParameter("VendorNumber",snull);
                this.rsDetail.ServerReport.DisplayName = "Tracking Detail";
                this.rsDetail.ServerReport.ReportPath = "/Customer Service/CRM Tracking Detail";
                this.rsDetail.ServerReport.SetParameters(new ReportParameter[] { p1,p2,p3 });
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

                string items = this.txtItems.Text.Trim();
                items = items.Replace("\r\n","\r");
                items = items.Replace("\n","\r");
                string[] numbers = items.Split(Convert.ToChar(13));
                if (this.cboItemType.Text == "Label Sequence")
                    this.bsItems.DataSource = Argix.Enterprise.EnterpriseGateway.TrackCartonsByLabelNumber(numbers);
                else
                    this.bsItems.DataSource = Argix.Enterprise.EnterpriseGateway.TrackCartonsByCartonNumber(numbers);
            }
            catch (Exception ex) { App.ReportError(new ApplicationException(ex.Message)); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
