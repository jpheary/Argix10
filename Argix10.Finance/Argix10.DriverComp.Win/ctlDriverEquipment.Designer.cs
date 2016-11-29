namespace Argix.Finance {
    partial class ctlDriverEquipment {
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("DriverEquipmentTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("FinanceVendorID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("OperatorName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EquipmentName");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.mEquipment = new Argix.Finance.DriverCompDataset();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.uddEquipType = new Infragistics.Win.UltraWinGrid.UltraDropDown();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mEquipment)).BeginInit();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).BeginInit();
            this.SuspendLayout();
            // 
            // grdMain
            // 
            this.grdMain.ContextMenuStrip = this.csMain;
            this.grdMain.DataMember = "DriverEquipmentTable";
            this.grdMain.DataSource = this.mEquipment;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn1.Header.Caption = "VendorID";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 78;
            ultraGridColumn2.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn2.Header.Caption = "Operator";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 192;
            ultraGridColumn3.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn3.Header.Caption = "Equip Type";
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Width = 120;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn4.Header.Caption = "Equipment";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn4.Width = 120;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4});
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.CaptionAppearance = appearance2;
            this.grdMain.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMain.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMain.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMain.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMain.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMain.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdMain.Location = new System.Drawing.Point(0, 25);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(576, 71);
            this.grdMain.TabIndex = 0;
            this.grdMain.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.InitializeLayout += new Infragistics.Win.UltraWinGrid.InitializeLayoutEventHandler(this.OnGridInitializeLayout);
            this.grdMain.InitializeRow += new Infragistics.Win.UltraWinGrid.InitializeRowEventHandler(this.OnGridInitializeRow);
            this.grdMain.BeforeRowUpdate += new Infragistics.Win.UltraWinGrid.CancelableRowEventHandler(this.OnGridBeforeRowUpdate);
            this.grdMain.CellChange += new Infragistics.Win.UltraWinGrid.CellEventHandler(this.OnGridCellChange);
            this.grdMain.BeforeRowFilterDropDownPopulate += new Infragistics.Win.UltraWinGrid.BeforeRowFilterDropDownPopulateEventHandler(this.OnGridBeforeRowFilterDropDownPopulate);
            this.grdMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csRefresh});
            this.csMain.Name = "ctxMain";
            this.csMain.Size = new System.Drawing.Size(114, 26);
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.ImageTransparentColor = System.Drawing.Color.Maroon;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(113, 22);
            this.csRefresh.Text = "Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mEquipment
            // 
            this.mEquipment.DataSetName = "DriverCompDataset";
            this.mEquipment.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tsMain
            // 
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 0);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(576, 25);
            this.tsMain.TabIndex = 1;
            // 
            // tsRefresh
            // 
            this.tsRefresh.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(23, 22);
            this.tsRefresh.ToolTipText = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // uddEquipType
            // 
            this.uddEquipType.Cursor = System.Windows.Forms.Cursors.Default;
            this.uddEquipType.Location = new System.Drawing.Point(0, 78);
            this.uddEquipType.Name = "uddEquipType";
            this.uddEquipType.Size = new System.Drawing.Size(104, 18);
            this.uddEquipType.TabIndex = 8;
            this.uddEquipType.Text = "ultraDropDown1";
            this.uddEquipType.Visible = false;
            // 
            // ctlDriverEquipment
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.uddEquipType);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.tsMain);
            this.Name = "ctlDriverEquipment";
            this.Size = new System.Drawing.Size(576, 96);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mEquipment)).EndInit();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.uddEquipType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        private System.Windows.Forms.ToolStrip tsMain;
        private DriverCompDataset mEquipment;
        private System.Windows.Forms.ToolStripButton tsRefresh;
        private System.Windows.Forms.ContextMenuStrip csMain;
        private System.Windows.Forms.ToolStripMenuItem csRefresh;
        private Infragistics.Win.UltraWinGrid.UltraDropDown uddEquipType;
    }
}
