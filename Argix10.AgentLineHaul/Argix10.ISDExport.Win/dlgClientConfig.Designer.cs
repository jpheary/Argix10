namespace Argix.AgentLineHaul {
    partial class dlgClientConfig {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ClientTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CLID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ExportFormat");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ExportPath");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CounterKey");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Client");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Scanner");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.grdConfig = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mISDClientDS = new Argix.ISDExportDataset();
            ((System.ComponentModel.ISupportInitialize)(this.grdConfig)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mISDClientDS)).BeginInit();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(554, 227);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnClose);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefresh.Location = new System.Drawing.Point(452, 227);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(96, 24);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "&Refresh";
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.OnRefresh);
            // 
            // grdConfig
            // 
            this.grdConfig.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdConfig.DataMember = "ClientTable";
            this.grdConfig.DataSource = this.mISDClientDS;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdConfig.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 48;
            ultraGridColumn2.Header.Caption = "Format";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 72;
            ultraGridColumn3.Header.Caption = "Export Path";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 240;
            ultraGridColumn4.Header.Caption = "Count Key";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 72;
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 72;
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn6.Width = 72;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn7.Width = 72;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5,
            ultraGridColumn6,
            ultraGridColumn7});
            this.grdConfig.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdConfig.DisplayLayout.CaptionAppearance = appearance2;
            this.grdConfig.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdConfig.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdConfig.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdConfig.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdConfig.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdConfig.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdConfig.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdConfig.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdConfig.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdConfig.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdConfig.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdConfig.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdConfig.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdConfig.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdConfig.Location = new System.Drawing.Point(0, 0);
            this.grdConfig.Name = "grdConfig";
            this.grdConfig.Size = new System.Drawing.Size(656, 220);
            this.grdConfig.TabIndex = 0;
            this.grdConfig.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdConfig.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdConfig.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            this.grdConfig.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridAfterSelectChange);
            this.grdConfig.BeforeCellActivate += new Infragistics.Win.UltraWinGrid.CancelableCellEventHandler(this.OnGridBeforeCellActivate);
            this.grdConfig.BeforeRowsDeleted += new Infragistics.Win.UltraWinGrid.BeforeRowsDeletedEventHandler(this.OnGridBeforeRowsDeleted);
            this.grdConfig.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mISDClientDS
            // 
            this.mISDClientDS.DataSetName = "ISDExportDataset";
            this.mISDClientDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dlgClientConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(656, 254);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grdConfig);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "dlgClientConfig";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client Configuration";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdConfig)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mISDClientDS)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdConfig;
        private System.Windows.Forms.Button btnClose;
        private Argix.ISDExportDataset mISDClientDS;
        private System.Windows.Forms.Button btnRefresh;
    }
}