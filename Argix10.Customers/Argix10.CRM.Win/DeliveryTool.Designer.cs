﻿namespace Argix.Customers {
    partial class DeliveryTool {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DeliveryTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CBOL");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CPROID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CPRONumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StoreNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OFD2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PodDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PodTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShouldBeDeliveredOn");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WindowStartTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WindowEndTime");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CartonsSorted");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLS");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsMatch");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsShort");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsOver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsMisroute");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsDamaged");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsScanned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PODCartonsManual");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsMatch");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsShort");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsOver");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsMisroute");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsDamaged");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsScanned");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OSDCartonsManual");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.btnView = new System.Windows.Forms.Button();
            this.grdDeliveries = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mDeliveries = new Argix.CRMDataset();
            this._lblStore = new System.Windows.Forms.Label();
            this.mskStoreNumber = new System.Windows.Forms.MaskedTextBox();
            this._lblClient = new System.Windows.Forms.Label();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this._lblTo = new System.Windows.Forms.Label();
            this._lblFrom = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.mClients = new Argix.CRMDataset();
            ((System.ComponentModel.ISupportInitialize)(this.grdDeliveries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDeliveries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).BeginInit();
            this.SuspendLayout();
            // 
            // btnView
            // 
            this.btnView.Location = new System.Drawing.Point(183, 86);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(75, 23);
            this.btnView.TabIndex = 50;
            this.btnView.Text = "View";
            this.btnView.UseVisualStyleBackColor = true;
            this.btnView.Click += new System.EventHandler(this.OnView);
            // 
            // grdDeliveries
            // 
            this.grdDeliveries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdDeliveries.DataMember = "DeliveryTable";
            this.grdDeliveries.DataSource = this.mDeliveries;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdDeliveries.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 1;
            ultraGridColumn1.Width = 96;
            ultraGridColumn2.Header.VisiblePosition = 2;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.Caption = "CPRO#";
            ultraGridColumn3.Header.VisiblePosition = 3;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 72;
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn5.Format = "MM-dd-yyyy";
            ultraGridColumn5.Header.VisiblePosition = 6;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 96;
            ultraGridColumn6.Header.VisiblePosition = 8;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn7.Format = "MM-dd-yyyy";
            ultraGridColumn7.Header.Caption = "Pod Date";
            ultraGridColumn7.Header.VisiblePosition = 7;
            ultraGridColumn7.Width = 96;
            ultraGridColumn8.Format = "HH:mm";
            ultraGridColumn8.Header.Caption = "Pod Time";
            ultraGridColumn8.Header.VisiblePosition = 9;
            ultraGridColumn8.Width = 72;
            ultraGridColumn9.Format = "MM-dd-yyyy";
            ultraGridColumn9.Header.Caption = "Est Delivery On";
            ultraGridColumn9.Header.VisiblePosition = 10;
            ultraGridColumn9.Width = 120;
            ultraGridColumn10.Format = "HH:mm";
            ultraGridColumn10.Header.Caption = "Window Start";
            ultraGridColumn10.Header.VisiblePosition = 11;
            ultraGridColumn10.Width = 96;
            ultraGridColumn11.Format = "HH:mm";
            ultraGridColumn11.Header.Caption = "Window End";
            ultraGridColumn11.Header.VisiblePosition = 12;
            ultraGridColumn11.Width = 96;
            ultraGridColumn12.Header.Caption = "Cartons";
            ultraGridColumn12.Header.VisiblePosition = 5;
            ultraGridColumn12.Width = 72;
            ultraGridColumn13.Header.VisiblePosition = 13;
            ultraGridColumn13.Width = 72;
            ultraGridColumn14.Header.VisiblePosition = 14;
            ultraGridColumn14.Width = 192;
            ultraGridColumn15.Header.Caption = "POD Match";
            ultraGridColumn15.Header.VisiblePosition = 15;
            ultraGridColumn15.Hidden = true;
            ultraGridColumn15.Width = 72;
            ultraGridColumn16.Header.Caption = "POD Short";
            ultraGridColumn16.Header.VisiblePosition = 16;
            ultraGridColumn16.Hidden = true;
            ultraGridColumn16.Width = 72;
            ultraGridColumn17.Header.Caption = "POD Over";
            ultraGridColumn17.Header.VisiblePosition = 17;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn17.Width = 72;
            ultraGridColumn18.Header.Caption = "POD Misroute";
            ultraGridColumn18.Header.VisiblePosition = 18;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn18.Width = 72;
            ultraGridColumn19.Header.Caption = "POD Damaged";
            ultraGridColumn19.Header.VisiblePosition = 19;
            ultraGridColumn19.Hidden = true;
            ultraGridColumn19.Width = 72;
            ultraGridColumn20.Header.Caption = "POD Scanned";
            ultraGridColumn20.Header.VisiblePosition = 20;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn20.Width = 72;
            ultraGridColumn21.Header.Caption = "POD Manual";
            ultraGridColumn21.Header.VisiblePosition = 21;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn21.Width = 72;
            ultraGridColumn22.Header.Caption = "OSD Match";
            ultraGridColumn22.Header.VisiblePosition = 22;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn22.Width = 72;
            ultraGridColumn23.Header.Caption = "OSD Short";
            ultraGridColumn23.Header.VisiblePosition = 23;
            ultraGridColumn23.Hidden = true;
            ultraGridColumn23.Width = 72;
            ultraGridColumn24.Header.Caption = "OSD Over";
            ultraGridColumn24.Header.VisiblePosition = 24;
            ultraGridColumn24.Hidden = true;
            ultraGridColumn24.Width = 72;
            ultraGridColumn25.Header.Caption = "OSD Misroute";
            ultraGridColumn25.Header.VisiblePosition = 25;
            ultraGridColumn25.Hidden = true;
            ultraGridColumn25.Width = 72;
            ultraGridColumn26.Header.Caption = "OSD Damaged";
            ultraGridColumn26.Header.VisiblePosition = 26;
            ultraGridColumn26.Hidden = true;
            ultraGridColumn26.Width = 72;
            ultraGridColumn27.Header.Caption = "OSD Scanned";
            ultraGridColumn27.Header.VisiblePosition = 27;
            ultraGridColumn27.Hidden = true;
            ultraGridColumn27.Width = 72;
            ultraGridColumn28.Header.Caption = "OSD Manual";
            ultraGridColumn28.Header.VisiblePosition = 28;
            ultraGridColumn28.Hidden = true;
            ultraGridColumn28.Width = 72;
            ultraGridColumn29.Header.VisiblePosition = 0;
            ultraGridColumn29.Width = 60;
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
            ultraGridColumn20,
            ultraGridColumn21,
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29});
            this.grdDeliveries.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdDeliveries.DisplayLayout.CaptionAppearance = appearance2;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            this.grdDeliveries.DisplayLayout.Override.ActiveCellAppearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.Highlight;
            appearance6.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.grdDeliveries.DisplayLayout.Override.ActiveRowAppearance = appearance6;
            this.grdDeliveries.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdDeliveries.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdDeliveries.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdDeliveries.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdDeliveries.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdDeliveries.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdDeliveries.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdDeliveries.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdDeliveries.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdDeliveries.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdDeliveries.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdDeliveries.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdDeliveries.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdDeliveries.Location = new System.Drawing.Point(6, 115);
            this.grdDeliveries.Name = "grdDeliveries";
            this.grdDeliveries.Size = new System.Drawing.Size(252, 207);
            this.grdDeliveries.TabIndex = 49;
            this.grdDeliveries.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdDeliveries.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mDeliveries
            // 
            this.mDeliveries.DataSetName = "CRMDataset";
            this.mDeliveries.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblStore
            // 
            this._lblStore.Location = new System.Drawing.Point(6, 39);
            this._lblStore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblStore.Name = "_lblStore";
            this._lblStore.Size = new System.Drawing.Size(54, 18);
            this._lblStore.TabIndex = 48;
            this._lblStore.Text = "Store#";
            this._lblStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskStoreNumber
            // 
            this.mskStoreNumber.AllowPromptAsInput = false;
            this.mskStoreNumber.HidePromptOnLeave = true;
            this.mskStoreNumber.Location = new System.Drawing.Point(67, 39);
            this.mskStoreNumber.Mask = "#####";
            this.mskStoreNumber.Name = "mskStoreNumber";
            this.mskStoreNumber.Size = new System.Drawing.Size(60, 20);
            this.mskStoreNumber.TabIndex = 46;
            this.mskStoreNumber.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mskStoreNumber.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnStoreNumberKeyUp);
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(6, 13);
            this._lblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(54, 18);
            this._lblClient.TabIndex = 47;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpTo
            // 
            this.dtpTo.CustomFormat = "MM-dd-yyyy";
            this.dtpTo.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpTo.Location = new System.Drawing.Point(67, 88);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(111, 20);
            this.dtpTo.TabIndex = 44;
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnDeliverydatesChanged);
            // 
            // _lblTo
            // 
            this._lblTo.Location = new System.Drawing.Point(6, 88);
            this._lblTo.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(54, 18);
            this._lblTo.TabIndex = 43;
            this._lblTo.Text = "To";
            this._lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblFrom
            // 
            this._lblFrom.Location = new System.Drawing.Point(6, 62);
            this._lblFrom.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(54, 18);
            this._lblFrom.TabIndex = 41;
            this._lblFrom.Text = "From";
            this._lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.CustomFormat = "MM-dd-yyyy";
            this.dtpFrom.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpFrom.Location = new System.Drawing.Point(67, 62);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(111, 20);
            this.dtpFrom.TabIndex = 42;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnDeliverydatesChanged);
            // 
            // cboClient
            // 
            this.cboClient.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboClient.DataSource = this.mClients;
            this.cboClient.DisplayMember = "CompanyTable.CompanyName";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(67, 13);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(191, 21);
            this.cboClient.TabIndex = 45;
            this.cboClient.ValueMember = "CompanyTable.CompanyID";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
            // 
            // mClients
            // 
            this.mClients.DataSetName = "CRMDataset";
            this.mClients.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // DeliveryTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnView);
            this.Controls.Add(this.grdDeliveries);
            this.Controls.Add(this._lblStore);
            this.Controls.Add(this.mskStoreNumber);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this._lblTo);
            this.Controls.Add(this._lblFrom);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this.cboClient);
            this.Name = "DeliveryTool";
            this.Size = new System.Drawing.Size(266, 336);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdDeliveries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDeliveries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnView;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdDeliveries;
        private System.Windows.Forms.Label _lblStore;
        private System.Windows.Forms.MaskedTextBox mskStoreNumber;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label _lblTo;
        private System.Windows.Forms.Label _lblFrom;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.ComboBox cboClient;
        private CRMDataset mClients;
        private CRMDataset mDeliveries;
    }
}
