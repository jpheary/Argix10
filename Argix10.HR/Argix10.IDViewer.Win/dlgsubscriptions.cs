using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Principal;
using System.Windows.Forms;
using Argix.Reporting;
using Argix.Windows;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.HR {
	//
	public class dlgSubscriptions : System.Windows.Forms.Form {
		//Members
        private const string REPORT_PATH = "/Terminals/Security Route Detail";

        private Infragistics.Win.UltraWinGrid.UltraGrid grdSubscriptions;
		private Argix.SubscriptionDataset mSubscriptionDS;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRefresh;
        private Button btnAdd;
        private Button btnRemove;
		/// <summary>Required designer variable. </summary>
        private System.ComponentModel.Container components = null;
				
		//Interface
		public dlgSubscriptions() {
			//Constructor
			try {
				InitializeComponent();
			}
			catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if(components != null) { components.Dispose(); } } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SubscriptionTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Report");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubscriptionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EventType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastRun");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Active");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliverySettings");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MatchData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Parameters");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("RouteClass");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DriverName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSubscriptions));
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdSubscriptions = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSubscriptionDS = new Argix.SubscriptionDataset();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.grdSubscriptions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubscriptionDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(649, 228);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 24);
            this.btnClose.TabIndex = 122;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(445, 228);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96, 24);
            this.btnRefresh.TabIndex = 123;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // grdSubscriptions
            // 
            this.grdSubscriptions.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdSubscriptions.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdSubscriptions.DataMember = "SubscriptionTable";
            this.grdSubscriptions.DataSource = this.mSubscriptionDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdSubscriptions.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "TLViewTable";
            ultraGridColumn1.Header.VisiblePosition = 8;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 91;
            ultraGridColumn2.Header.Caption = "ID";
            ultraGridColumn2.Header.VisiblePosition = 0;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn2.Width = 24;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 192;
            ultraGridColumn4.Header.Caption = "Trigger";
            ultraGridColumn4.Header.VisiblePosition = 9;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn4.Width = 96;
            ultraGridColumn5.Format = "MM-dd-yyyy";
            ultraGridColumn5.Header.Caption = "Last Run";
            ultraGridColumn5.Header.VisiblePosition = 3;
            ultraGridColumn5.Width = 84;
            ultraGridColumn6.Header.VisiblePosition = 4;
            ultraGridColumn6.Width = 288;
            ultraGridColumn7.Header.VisiblePosition = 10;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 52;
            ultraGridColumn8.Header.Caption = "Delivery Settings";
            ultraGridColumn8.Header.VisiblePosition = 11;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn8.Width = 139;
            ultraGridColumn9.Header.Caption = "Match Data";
            ultraGridColumn9.Header.VisiblePosition = 12;
            ultraGridColumn9.Hidden = true;
            ultraGridColumn9.Width = 137;
            ultraGridColumn10.Header.VisiblePosition = 13;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn10.Width = 24;
            ultraGridColumn11.Header.VisiblePosition = 1;
            ultraGridColumn11.Width = 288;
            ultraGridColumn12.Header.VisiblePosition = 6;
            ultraGridColumn12.Hidden = true;
            ultraGridColumn12.Width = 96;
            ultraGridColumn13.Header.Caption = "Route Class";
            ultraGridColumn13.Header.VisiblePosition = 5;
            ultraGridColumn13.Width = 96;
            ultraGridColumn14.Header.Caption = "Driver";
            ultraGridColumn14.Header.VisiblePosition = 7;
            ultraGridColumn14.Width = 144;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7,
            ultraGridColumn8,
            ultraGridColumn9,
            ultraGridColumn10,
            ultraGridColumn11,
            ultraGridColumn12,
            ultraGridColumn13,
            ultraGridColumn14});
            this.grdSubscriptions.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdSubscriptions.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.Inset;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdSubscriptions.DisplayLayout.CaptionAppearance = appearance2;
            this.grdSubscriptions.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdSubscriptions.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdSubscriptions.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdSubscriptions.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdSubscriptions.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdSubscriptions.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdSubscriptions.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.ExtendedAutoDrag;
            this.grdSubscriptions.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Hide;
            this.grdSubscriptions.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdSubscriptions.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdSubscriptions.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdSubscriptions.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdSubscriptions.Location = new System.Drawing.Point(0, 0);
            this.grdSubscriptions.Name = "grdSubscriptions";
            this.grdSubscriptions.Size = new System.Drawing.Size(753, 222);
            this.grdSubscriptions.TabIndex = 121;
            this.grdSubscriptions.Text = "Security Route Detail";
            this.grdSubscriptions.UpdateMode = Infragistics.Win.UltraWinGrid.UpdateMode.OnUpdate;
            this.grdSubscriptions.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdSubscriptions.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdSubscriptions.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdSubscriptions.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            this.grdSubscriptions.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridAfterSelectChange);
            this.grdSubscriptions.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnGridBeforeCellActivate);
            this.grdSubscriptions.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdSubscriptions.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mSubscriptionDS
            // 
            this.mSubscriptionDS.DataSetName = "SubscriptionDataset";
            this.mSubscriptionDS.Locale = new System.Globalization.CultureInfo("en-US");
            this.mSubscriptionDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnAdd
            // 
            this.btnAdd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAdd.Location = new System.Drawing.Point(343, 228);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(96, 24);
            this.btnAdd.TabIndex = 124;
            this.btnAdd.Text = "&Add";
            this.btnAdd.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnRemove
            // 
            this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRemove.Location = new System.Drawing.Point(241, 228);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(96, 24);
            this.btnRemove.TabIndex = 125;
            this.btnRemove.Text = "&Remove";
            this.btnRemove.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // dlgSubscriptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(752, 256);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grdSubscriptions);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimizeBox = false;
            this.Name = "dlgSubscriptions";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Security Route Subscriptions";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdSubscriptions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubscriptionDS)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				this.Text = "Subscriptions for Security Route Detail Report";
				this.grdSubscriptions.Text = "Terminal: All";
                #region Grid customizations from normal layout (to support cell editing)
                this.grdSubscriptions.DisplayLayout.Override.RowSelectors = DefaultableBoolean.False;
                this.grdSubscriptions.DisplayLayout.Override.SelectTypeRow = SelectType.Single;
                this.grdSubscriptions.DisplayLayout.Override.SelectTypeCell = SelectType.Single;
                this.grdSubscriptions.DisplayLayout.TabNavigation = TabNavigation.NextCell;
                this.grdSubscriptions.DisplayLayout.Override.AllowAddNew = AllowAddNew.No;
                this.grdSubscriptions.DisplayLayout.Override.AllowDelete = DefaultableBoolean.False;
                this.grdSubscriptions.DisplayLayout.Override.AllowUpdate = DefaultableBoolean.False;
                this.grdSubscriptions.DisplayLayout.Override.MaxSelectedCells = 1;
                this.grdSubscriptions.DisplayLayout.Override.CellClickAction = CellClickAction.RowSelect;
                this.grdSubscriptions.DisplayLayout.Bands[0].Override.CellClickAction = CellClickAction.RowSelect;
                this.grdSubscriptions.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                #endregion
                this.btnRefresh.PerformClick();
                OnValidateForm(null,EventArgs.Empty);
			}
			catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
			finally { this.Cursor = Cursors.Default; }
		}
        private void OnGridAfterSelectChange(object sender,AfterSelectChangeEventArgs e) {
            //
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnValidateForm(object sender,System.EventArgs e) {
            //Event handler for close button clicked
            this.Cursor = Cursors.WaitCursor;
            try {
                this.btnClose.Enabled = true;
                this.btnAdd.Enabled = true;
                this.btnRefresh.Enabled = true;
                this.btnRemove.Enabled = this.grdSubscriptions.Selected != null && this.grdSubscriptions.Selected.Rows.Count > 0;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Warning); }
            finally { this.Cursor = Cursors.Default; }
        }
        private void OnCommandClick(object sender,EventArgs e) {
            //Event handler for command button click events
            this.Cursor = Cursors.WaitCursor;
            try {
                Button button = (Button)sender;
                switch (button.Name) {
                    case "btnAdd":
                        dlgSubscription dlg = new dlgSubscription();
                        dlg.ShowDialog(this);
                        if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK) {
                            ReportingGateway.CreateSubscription(REPORT_PATH,dlg.RouteClass,dlg.Driver);
                            this.mSubscriptionDS.Clear();
                            this.mSubscriptionDS.Merge(ReportingGateway.GetSubscriptions(REPORT_PATH));
                        }
                        break;
                    case "btnClose":
                        this.Close();
                        break;
                    case "btnRefresh":
                        this.mSubscriptionDS.Clear();
                        this.mSubscriptionDS.Merge(ReportingGateway.GetSubscriptions(REPORT_PATH));
                        break;
                    case "btnRemove":
                        string id = this.grdSubscriptions.Selected.Rows[0].Cells["SubscriptionID"].Value.ToString();
                        ReportingGateway.DeleteSubscription(id);
                        this.mSubscriptionDS.Clear();
                        this.mSubscriptionDS.Merge(ReportingGateway.GetSubscriptions(REPORT_PATH));
                        break;
                }
                OnValidateForm(null,EventArgs.Empty);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
        #region Grid Servces: OnGridInitializeLayout(), OnGridInitializeRow(), OnGridBeforeRowFilterDropDownPopulate(), OnGridMouseDown(), OnGridBeforeCellActivate(), OnGridCellChange()
        private void OnGridInitializeLayout(object sender,Infragistics.Win.UltraWinGrid.InitializeLayoutEventArgs e) {
            //
        }
        private void OnGridInitializeRow(object sender,Infragistics.Win.UltraWinGrid.InitializeRowEventArgs e) {
            //
            if(e.Row.Band.Key == "SubscriptionTable") {
                e.Row.Cells["Report"].Activation = Activation.NoEdit;
                e.Row.Cells["SubscriptionID"].Activation = Activation.NoEdit;
                e.Row.Cells["Description"].Activation = Activation.NoEdit;
                e.Row.Cells["EventType"].Activation = Activation.NoEdit;
                e.Row.Cells["LastRun"].Activation = Activation.NoEdit;
                e.Row.Cells["Status"].Activation = Activation.NoEdit;
                e.Row.Cells["Active"].Activation = Activation.NoEdit;
                e.Row.Cells["DeliverySettings"].Activation = Activation.NoEdit;
                e.Row.Cells["MatchData"].Activation = Activation.NoEdit;
                e.Row.Cells["Parameters"].Activation = Activation.NoEdit;
                e.Row.Cells["Subject"].Activation = Activation.NoEdit;
                e.Row.Cells["RouteDate"].Activation = Activation.NoEdit;
                e.Row.Cells["RouteClass"].Activation = Activation.NoEdit;
                e.Row.Cells["DriverName"].Activation = Activation.NoEdit;
            }
        }
        private void OnGridBeforeRowFilterDropDownPopulate(object sender,Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventArgs e) {
            //Removes only (Blanks) and Non Blanks default filter
            try {
                e.ValueList.ValueListItems.Remove(3);
                e.ValueList.ValueListItems.Remove(2);
                e.ValueList.ValueListItems.Remove(1);
            }
            catch(Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnGridMouseDown(object sender,System.Windows.Forms.MouseEventArgs e) {
			//Event handler for mouse down event for all grids
			try {
				//Ensure focus when user mouses (embedded child objects sometimes hold focus)
				UltraGrid grid = (UltraGrid)sender;
				grid.Focus();
				
				//Determine grid element pointed to by the mouse
				UIElement uiElement = grid.DisplayLayout.UIElement.ElementFromPoint(new Point(e.X, e.Y));
				if(uiElement != null) {
					//Determine if user selected a grid row
					object context = uiElement.GetContext(typeof(UltraGridRow));
					if(context != null) {
						//Row was selected- if mouse button is:
						// left: forward to mouse move event handler
						//right: clear all (multi-)selected rows and select a single row
						if(e.Button == MouseButtons.Left) {
						}
						else if(e.Button == MouseButtons.Right) {
							UltraGridRow row = (UltraGridRow)context;
							if(!row.Selected) grid.Selected.Rows.Clear();
							row.Selected = true;
						}
					}
					else {
						//Deselect rows in the white space of the grid or deactivate the active   
						//row when in a scroll region to prevent double-click action
						if(uiElement.Parent != null && uiElement.Parent.GetType() == typeof(DataAreaUIElement))
							grid.Selected.Rows.Clear();
						else if(uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollThumbUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollArrowUIElement) || uiElement.GetType() == typeof(Infragistics.Win.UltraWinScrollBar.ScrollTrackSubAreaUIElement))
							if(grid.Selected.Rows.Count > 0) grid.Selected.Rows[0].Activated = false;
					}
				}
			} 
			catch(Exception ex) { App.ReportError(ex, false, LogLevel.Warning); }
			//finally { setUserServices(); }
		}
        private void OnGridBeforeCellActivate(object sender,Infragistics.Win.UltraWinGrid.CancelableCellEventArgs e) {
            //Event handler for data entry cell activating
            try {
                //Enable\disable cell editing
                //switch(e.Cell.Column.Key.ToString()) {
                //    //Allow send change if applicable subscription
                //    case "Send":
                //        bool allow=false;
                //        string terminalID = e.Cell.Row.Cells["TerminalID"].Value.ToString().Trim();
                //        if(terminalID.Length > 0) {
                //            //Carrier subscriptions
                //            string carrierID = e.Cell.Row.Cells["CarrierAgentID"].Value.ToString().Trim();
                //            allow = (terminalID == this.mSchedule.SortCenterID.ToString() && (carrierID.Length > 0 && this.mSchedule.Trips.ShipScheduleTable.Select("carrierID=" + carrierID).Length > 0));
                //        }
                //        else {
                //            //Agent subscriptions
                //            string agentNumber = e.Cell.Row.Cells["CarrierAgentID"].Value.ToString().Trim();
                //            allow = ((agentNumber.Length > 0) && (this.mSchedule.Trips.ShipScheduleTable.Select("AgentNumber='" + agentNumber + "'").Length > 0) || this.mSchedule.Trips.ShipScheduleTable.Select("S2AgentNumber='" + agentNumber + "'").Length > 0);
                //        }
                //        e.Cell.Activation = allow ? Activation.AllowEdit : Activation.Disabled;
                //        break;
                //}
                //e.Cell.Selected = true;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnGridCellChange(object sender,Infragistics.Win.UltraWinGrid.CellEventArgs e) {
            //Event handler for change in a data entry cell value
            try {
                //
                //if(e.Cell.Row.Cells["Application"].Value.ToString() == "")
                //    e.Cell.Row.Cells["Application"].Value = this.mTsortApp.DBConfiguration.ProductName;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #endregion

	}
}
