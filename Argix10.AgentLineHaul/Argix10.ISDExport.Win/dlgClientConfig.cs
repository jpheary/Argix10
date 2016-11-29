using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Argix.Enterprise;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;

namespace Argix.AgentLineHaul {
    //
    public partial class dlgClientConfig :Form {
        //Members

        //Interface
        public dlgClientConfig() {
            //Constructor
            try {
                InitializeComponent();
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while crating new dlgClientConfig instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form laod event
            try {
                #region Grid customizations from normal layout (to support cell editing)
                this.grdConfig.DisplayLayout.Override.RowSelectors = DefaultableBoolean.True;
                this.grdConfig.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdConfig.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                if (RoleServiceGateway.IsTsortSupervisor) {
                    this.grdConfig.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                    this.grdConfig.DisplayLayout.Override.AllowAddNew = AllowAddNew.TemplateOnBottom;
                    this.grdConfig.DisplayLayout.Override.AllowDelete = DefaultableBoolean.True;
                    this.grdConfig.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.True;
                    this.grdConfig.DisplayLayout.Override.MaxSelectedCells = 1;
                    this.grdConfig.DisplayLayout.Override.CellClickAction = CellClickAction.Edit;
                    this.grdConfig.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.Edit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["CLID"].CellActivation = Activation.AllowEdit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["ExportFormat"].CellActivation = Activation.AllowEdit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["ExportPath"].CellActivation = Activation.AllowEdit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["CounterKey"].CellActivation = Activation.AllowEdit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["Client"].CellActivation = Activation.AllowEdit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["Scanner"].CellActivation = Activation.AllowEdit;
                    this.grdConfig.DisplayLayout.Bands[0].Columns["UserID"].CellActivation = Activation.AllowEdit;
                }
                this.grdConfig.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdConfig.DisplayLayout.Bands[0].Columns["CLID"].SortIndicator = SortIndicator.Ascending;
                #endregion
                OnRefresh(this.btnRefresh, EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Warning); }
        }
        #region Grid Services: OnGridMouseDown(), OnGridAfterSelectChange(), OnGridBeforeCellActivate(), OnGridCellChange(), OnGridBeforeRowUpdate(), OnGridBeforeRowsDeleted()
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
            //Event handler for mouse down event for all grids
            try {
                //Select rows on right click
                UltraGrid oGrid = (UltraGrid)sender;
                oGrid.Focus();
                UIElement oUIElement = oGrid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X,e.Y));
                if(oUIElement != null) {
                    object oContext = oUIElement.GetContext(typeof(UltraGridRow));
                    if(oContext != null) {
                        if(e.Button == MouseButtons.Left) {
                            //OnDragDropMouseDown(sender, e);
                        }
                        else if(e.Button == MouseButtons.Right) {
                            UltraGridRow oRow = (UltraGridRow)oContext;
                            if(!oRow.Selected) oGrid.Selected.Rows.Clear();
                            oRow.Selected = true;
                        }
                    }
                    else {
                        //Deselect rows in the white space of the grid or deactivate the active   
                        //row when in a scroll region to prevent double-click action
                        if(oUIElement.Parent.GetType() == typeof(DataAreaUIElement))
                            oGrid.Selected.Rows.Clear();
                        else if(oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || oUIElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
                            if(oGrid.Selected.Rows.Count > 0) oGrid.Selected.Rows[0].Activated = false;
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridAfterSelectChange(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for change in selected data entry
        }
        private void OnGridBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for data entry cell activating
        }
        private void OnGridCellChange(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a data entry cell value
        }
        private void OnGridBeforeRowUpdate(object sender,Infragistics.Win.UltraWinGrid.CancelableRowEventArgs e) {
            //Event handler for data entry row updated
            try {
                //There is no selected row when updating- at a cell level
                string clid = e.Row.Cells["CLID"].Value.ToString();
                string format = e.Row.Cells["ExportFormat"].Value.ToString();
                string path = e.Row.Cells["ExportPath"].Value.ToString();
                string key = e.Row.Cells["CounterKey"].Value.ToString();
                string client = e.Row.Cells["Client"].Value.ToString();
                string scanner = e.Row.Cells["Scanner"].Value.ToString();
                string userid = e.Row.Cells["UserID"].Value.ToString();
                if(clid != "" && format != "" && path != "" && key != "" && client != "") {
                    ISDClient isdClient = new ISDClient();
                    isdClient.CLID = clid;
                    isdClient.ExportFormat = format;
                    isdClient.ExportPath = path;
                    isdClient.CounterKey = key;
                    isdClient.Client = client;
                    isdClient.Scanner = scanner;
                    isdClient.UserID = userid;
                    if (e.Row.IsAddRow) {
                        //Add new entry
                        bool created = AgentLineHaulGateway.CreateISDClient(isdClient);
                        OnRefresh(this.btnRefresh,EventArgs.Empty);
                    }
                    else {
                        //Update existing
                        bool updated = AgentLineHaulGateway.UpdateISDClient(isdClient);
                        OnRefresh(this.btnRefresh,EventArgs.Empty);
                    }
                }
                else
                    e.Cancel = true;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridBeforeRowsDeleted(object sender,BeforeRowsDeletedEventArgs e) {
            //Event hanlder for rows deleting
            try {
                //Cannot delete 'Default' entries or the new row entry
                e.DisplayPromptMsg = true;
                if(!e.Cancel) {
                    ISDClient isdClient = new ISDClient();
                    isdClient.CLID = e.Rows[0].Cells["CLID"].Value.ToString();
                    isdClient.ExportFormat = e.Rows[0].Cells["ExportFormat"].Value.ToString();
                    isdClient.ExportPath = e.Rows[0].Cells["ExportPath"].Value.ToString();
                    isdClient.CounterKey = e.Rows[0].Cells["CounterKey"].Value.ToString();
                    isdClient.Client = e.Rows[0].Cells["Client"].Value.ToString();
                    isdClient.Scanner = e.Rows[0].Cells["Scanner"].Value.ToString();
                    isdClient.UserID = e.Rows[0].Cells["UserID"].Value.ToString();
                    bool deleted = AgentLineHaulGateway.DeleteISDClient(isdClient);
                    OnRefresh(this.btnRefresh,EventArgs.Empty);
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion
        private void OnClose(object sender,EventArgs e) {
            //Event handler for command click event
            this.Close();
        }
        private void OnRefresh(object sender,EventArgs e) {
            //Refresh view
            this.mISDClientDS.Clear();
            DataSet ds = AgentLineHaulGateway.GetISDClients();
            this.mISDClientDS.Merge(ds);
        }
    }
}