namespace Argix.Freight {
    partial class TDSInfoTool {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TDSTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientDivision");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("VendorName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TDSID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CarrierName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TrailerNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SealNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("BLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Unloaded");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FloorStatus");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PuDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ArrivalDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReceivedDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StartSortDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StopSortDate");
            Infragistics.Win.Appearance appearance12 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            this.mClients = new Argix.DispatchDataset();
            this.btnFind = new System.Windows.Forms.Button();
            this._lblStart = new System.Windows.Forms.Label();
            this.dtpStartDate = new System.Windows.Forms.DateTimePicker();
            this._lblClient = new System.Windows.Forms.Label();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.grdTDSInfo = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.btnClear = new System.Windows.Forms.Button();
            this.dtpEndDate = new System.Windows.Forms.DateTimePicker();
            this._lblEnd = new System.Windows.Forms.Label();
            this._lblVendor = new System.Windows.Forms.Label();
            this.txtVendorName = new System.Windows.Forms.TextBox();
            this.mTDSInfo = new Argix.DispatchDataset();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTDSInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTDSInfo)).BeginInit();
            this.SuspendLayout();
            // 
            // mClients
            // 
            this.mClients.DataSetName = "DispatchDataset";
            this.mClients.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnFind
            // 
            this.btnFind.Location = new System.Drawing.Point(5, 169);
            this.btnFind.Name = "btnFind";
            this.btnFind.Size = new System.Drawing.Size(50, 23);
            this.btnFind.TabIndex = 5;
            this.btnFind.Text = "Find";
            this.btnFind.UseVisualStyleBackColor = true;
            this.btnFind.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // _lblStart
            // 
            this._lblStart.Location = new System.Drawing.Point(10, 55);
            this._lblStart.Name = "_lblStart";
            this._lblStart.Size = new System.Drawing.Size(75, 20);
            this._lblStart.TabIndex = 103;
            this._lblStart.Text = "Start Date";
            this._lblStart.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpStartDate
            // 
            this.dtpStartDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpStartDate.Location = new System.Drawing.Point(91, 55);
            this.dtpStartDate.Name = "dtpStartDate";
            this.dtpStartDate.Size = new System.Drawing.Size(125, 20);
            this.dtpStartDate.TabIndex = 1;
            this.dtpStartDate.ValueChanged += new System.EventHandler(this.OnDateChanged);
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(10, 5);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(75, 20);
            this._lblClient.TabIndex = 101;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboClient
            // 
            this.cboClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClient.DataSource = this.mClients;
            this.cboClient.DisplayMember = "ClientTable.Name";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(5, 27);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(338, 21);
            this.cboClient.TabIndex = 0;
            this.cboClient.ValueMember = "ClientTable.Number";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
            // 
            // grdTDSInfo
            // 
            this.grdTDSInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdTDSInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTDSInfo.DataMember = "TDSTable";
            this.grdTDSInfo.DataSource = this.mTDSInfo;
            appearance7.BackColor = System.Drawing.SystemColors.Window;
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance7.TextHAlignAsString = "Left";
            this.grdTDSInfo.DisplayLayout.Appearance = appearance7;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn2.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn2.Header.Caption = "Client Div";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 75;
            ultraGridColumn3.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn3.Header.Caption = "Vendor#";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 75;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn4.Header.Caption = "Vendor";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 150;
            ultraGridColumn5.Header.Caption = "TDS";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 100;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn8.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn8.Header.Caption = "Carrier";
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 150;
            ultraGridColumn9.Header.Caption = "Trailer#";
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 75;
            ultraGridColumn10.Header.Caption = "Seal#";
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Width = 75;
            ultraGridColumn11.Header.Caption = "BL#";
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn12.Width = 75;
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn13.Width = 75;
            ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn14.Header.Caption = "Unloaded?";
            ultraGridColumn14.Header.VisiblePosition = 13;
            ultraGridColumn14.Width = 75;
            ultraGridColumn15.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn15.Header.Caption = "Floor?";
            ultraGridColumn15.Header.VisiblePosition = 14;
            ultraGridColumn15.Width = 75;
            ultraGridColumn16.Format = "MM-dd-yyyy";
            ultraGridColumn16.Header.Caption = "Pickup";
            ultraGridColumn16.Header.VisiblePosition = 15;
            ultraGridColumn16.Width = 100;
            ultraGridColumn17.Format = "MM-dd-yyyy";
            ultraGridColumn17.Header.Caption = "Arrival";
            ultraGridColumn17.Header.VisiblePosition = 16;
            ultraGridColumn17.Width = 100;
            ultraGridColumn18.Format = "MM-dd-yyyy";
            ultraGridColumn18.Header.Caption = "Received";
            ultraGridColumn18.Header.VisiblePosition = 17;
            ultraGridColumn18.Width = 100;
            ultraGridColumn19.Format = "MM-dd-yyyy";
            ultraGridColumn19.Header.Caption = "Start Sort";
            ultraGridColumn19.Header.VisiblePosition = 18;
            ultraGridColumn19.Width = 100;
            ultraGridColumn20.Format = "MM-dd-yyyy";
            ultraGridColumn20.Header.Caption = "Stop Sort";
            ultraGridColumn20.Header.VisiblePosition = 19;
            ultraGridColumn20.Width = 100;
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
            ultraGridColumn14,
            ultraGridColumn15,
            ultraGridColumn16,
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20});
            this.grdTDSInfo.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance12.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance12.FontData.SizeInPoints = 8F;
            appearance12.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance12.TextHAlignAsString = "Left";
            this.grdTDSInfo.DisplayLayout.CaptionAppearance = appearance12;
            this.grdTDSInfo.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTDSInfo.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTDSInfo.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTDSInfo.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.EditAndSelectText;
            appearance13.BackColor = System.Drawing.SystemColors.Control;
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.TextHAlignAsString = "Left";
            this.grdTDSInfo.DisplayLayout.Override.HeaderAppearance = appearance13;
            this.grdTDSInfo.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdTDSInfo.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance14.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTDSInfo.DisplayLayout.Override.RowAppearance = appearance14;
            this.grdTDSInfo.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTDSInfo.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdTDSInfo.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTDSInfo.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTDSInfo.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTDSInfo.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTDSInfo.Location = new System.Drawing.Point(5, 198);
            this.grdTDSInfo.Name = "grdTDSInfo";
            this.grdTDSInfo.Size = new System.Drawing.Size(342, 226);
            this.grdTDSInfo.TabIndex = 12;
            this.grdTDSInfo.Text = "TDS Info";
            this.grdTDSInfo.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(61, 169);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(50, 23);
            this.btnClear.TabIndex = 112;
            this.btnClear.Text = "&Clear";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // dtpEndDate
            // 
            this.dtpEndDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpEndDate.Location = new System.Drawing.Point(91, 81);
            this.dtpEndDate.Name = "dtpEndDate";
            this.dtpEndDate.Size = new System.Drawing.Size(125, 20);
            this.dtpEndDate.TabIndex = 113;
            // 
            // _lblEnd
            // 
            this._lblEnd.Location = new System.Drawing.Point(10, 78);
            this._lblEnd.Name = "_lblEnd";
            this._lblEnd.Size = new System.Drawing.Size(75, 20);
            this._lblEnd.TabIndex = 114;
            this._lblEnd.Text = "End Date";
            this._lblEnd.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblVendor
            // 
            this._lblVendor.Location = new System.Drawing.Point(10, 109);
            this._lblVendor.Name = "_lblVendor";
            this._lblVendor.Size = new System.Drawing.Size(75, 20);
            this._lblVendor.TabIndex = 115;
            this._lblVendor.Text = "Vendor Name";
            this._lblVendor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtVendorName
            // 
            this.txtVendorName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtVendorName.Location = new System.Drawing.Point(5, 132);
            this.txtVendorName.Name = "txtVendorName";
            this.txtVendorName.Size = new System.Drawing.Size(338, 20);
            this.txtVendorName.TabIndex = 116;
            // 
            // mTDSInfo
            // 
            this.mTDSInfo.DataSetName = "DispatchDataset";
            this.mTDSInfo.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TDSInfoTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.txtVendorName);
            this.Controls.Add(this._lblVendor);
            this.Controls.Add(this._lblEnd);
            this.Controls.Add(this.dtpEndDate);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.grdTDSInfo);
            this.Controls.Add(this.btnFind);
            this.Controls.Add(this._lblStart);
            this.Controls.Add(this.dtpStartDate);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.cboClient);
            this.Name = "TDSInfoTool";
            this.Size = new System.Drawing.Size(350, 427);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdTDSInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTDSInfo)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DispatchDataset mClients;
        private System.Windows.Forms.Button btnFind;
        private System.Windows.Forms.Label _lblStart;
        private System.Windows.Forms.DateTimePicker dtpStartDate;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.ComboBox cboClient;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdTDSInfo;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.DateTimePicker dtpEndDate;
        private System.Windows.Forms.Label _lblEnd;
        private System.Windows.Forms.Label _lblVendor;
        private System.Windows.Forms.TextBox txtVendorName;
        private DispatchDataset mTDSInfo;
    }
}
