namespace Argix.Freight {
    partial class dlgTLDetail {
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TLTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WeightPercent");
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubePercent");
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance9 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance10 = new Infragistics.Win.Appearance();
            this.grdTLDetail = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mTLDetail = new Argix.FreightDataset();
            this.csDialog = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.csSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.csPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.csPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            ((System.ComponentModel.ISupportInitialize)(this.grdTLDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLDetail)).BeginInit();
            this.csDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdTLDetail
            // 
            this.grdTLDetail.ContextMenuStrip = this.csDialog;
            this.grdTLDetail.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdTLDetail.DataMember = "TLTable";
            this.grdTLDetail.DataSource = this.mTLDetail;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdTLDetail.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 1;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn3.Width = 50;
            ultraGridColumn4.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn4.Header.Caption = "Agent#";
            ultraGridColumn4.Header.VisiblePosition = 2;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn4.Width = 75;
            ultraGridColumn5.Header.Caption = "Agent";
            ultraGridColumn5.Header.VisiblePosition = 3;
            ultraGridColumn5.Hidden = true;
            ultraGridColumn5.Width = 150;
            ultraGridColumn6.Header.Caption = "TL#";
            ultraGridColumn6.Header.VisiblePosition = 4;
            ultraGridColumn6.Hidden = true;
            ultraGridColumn6.Width = 75;
            ultraGridColumn7.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn7.Format = "MM/dd/yyyy";
            ultraGridColumn7.Header.Caption = "TL Date";
            ultraGridColumn7.Header.VisiblePosition = 5;
            ultraGridColumn7.Hidden = true;
            ultraGridColumn7.Width = 100;
            ultraGridColumn8.Header.Caption = "Close#";
            ultraGridColumn8.Header.VisiblePosition = 6;
            ultraGridColumn8.Hidden = true;
            ultraGridColumn8.Width = 50;
            ultraGridColumn9.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn9.Header.Caption = "Client#";
            ultraGridColumn9.Header.VisiblePosition = 7;
            ultraGridColumn9.Width = 75;
            ultraGridColumn10.Header.Caption = "Client";
            ultraGridColumn10.Header.VisiblePosition = 8;
            ultraGridColumn10.Width = 200;
            ultraGridColumn11.Header.Caption = "Ship To#";
            ultraGridColumn11.Header.VisiblePosition = 9;
            ultraGridColumn11.Width = 75;
            ultraGridColumn12.Header.Caption = "Ship To Name";
            ultraGridColumn12.Header.VisiblePosition = 10;
            ultraGridColumn12.Width = 200;
            ultraGridColumn13.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn13.Header.VisiblePosition = 11;
            ultraGridColumn13.Hidden = true;
            ultraGridColumn13.Width = 50;
            ultraGridColumn14.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.True;
            ultraGridColumn14.Header.Caption = "Sm.Lane";
            ultraGridColumn14.Header.VisiblePosition = 12;
            ultraGridColumn14.Hidden = true;
            ultraGridColumn14.Width = 50;
            appearance2.TextHAlignAsString = "Right";
            ultraGridColumn15.CellAppearance = appearance2;
            ultraGridColumn15.Format = "#0";
            ultraGridColumn15.Header.VisiblePosition = 13;
            ultraGridColumn15.Width = 50;
            appearance3.TextHAlignAsString = "Right";
            ultraGridColumn17.CellAppearance = appearance3;
            ultraGridColumn17.Format = "#0";
            ultraGridColumn17.Header.VisiblePosition = 14;
            ultraGridColumn17.Width = 50;
            appearance4.TextHAlignAsString = "Right";
            ultraGridColumn18.CellAppearance = appearance4;
            ultraGridColumn18.Format = "#0";
            ultraGridColumn18.Header.VisiblePosition = 15;
            ultraGridColumn18.Width = 75;
            appearance5.TextHAlignAsString = "Right";
            ultraGridColumn19.CellAppearance = appearance5;
            ultraGridColumn19.Format = "#0";
            ultraGridColumn19.Header.VisiblePosition = 17;
            ultraGridColumn19.Width = 75;
            appearance6.TextHAlignAsString = "Right";
            ultraGridColumn20.CellAppearance = appearance6;
            ultraGridColumn20.Format = "#0";
            ultraGridColumn20.Header.Caption = "Weight%";
            ultraGridColumn20.Header.VisiblePosition = 16;
            ultraGridColumn20.Hidden = true;
            ultraGridColumn20.Width = 75;
            appearance7.TextHAlignAsString = "Right";
            ultraGridColumn21.CellAppearance = appearance7;
            ultraGridColumn21.Format = "#0";
            ultraGridColumn21.Header.Caption = "Cube%";
            ultraGridColumn21.Header.VisiblePosition = 18;
            ultraGridColumn21.Hidden = true;
            ultraGridColumn21.Width = 75;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
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
            ultraGridColumn17,
            ultraGridColumn18,
            ultraGridColumn19,
            ultraGridColumn20,
            ultraGridColumn21});
            this.grdTLDetail.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdTLDetail.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance8.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance8.FontData.BoldAsString = "True";
            appearance8.FontData.SizeInPoints = 9F;
            appearance8.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance8.TextHAlignAsString = "Left";
            this.grdTLDetail.DisplayLayout.CaptionAppearance = appearance8;
            this.grdTLDetail.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdTLDetail.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLDetail.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLDetail.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance9.BackColor = System.Drawing.SystemColors.Control;
            appearance9.FontData.BoldAsString = "True";
            appearance9.FontData.SizeInPoints = 8F;
            appearance9.TextHAlignAsString = "Left";
            this.grdTLDetail.DisplayLayout.Override.HeaderAppearance = appearance9;
            this.grdTLDetail.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdTLDetail.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance10.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdTLDetail.DisplayLayout.Override.RowAppearance = appearance10;
            this.grdTLDetail.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdTLDetail.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdTLDetail.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdTLDetail.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdTLDetail.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdTLDetail.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdTLDetail.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdTLDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdTLDetail.Location = new System.Drawing.Point(0, 0);
            this.grdTLDetail.Name = "grdTLDetail";
            this.grdTLDetail.Size = new System.Drawing.Size(802, 362);
            this.grdTLDetail.TabIndex = 3;
            this.grdTLDetail.Text = "TL #";
            this.grdTLDetail.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // mTLDetail
            // 
            this.mTLDetail.DataSetName = "FreightDataset";
            this.mTLDetail.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // csDialog
            // 
            this.csDialog.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csRefresh,
            this.csSep1,
            this.csSaveAs,
            this.csSep2,
            this.csPrint,
            this.csPreview});
            this.csDialog.Name = "csDialog";
            this.csDialog.Size = new System.Drawing.Size(153, 104);
            // 
            // csRefresh
            // 
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(152, 22);
            this.csRefresh.Text = "&Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSaveAs
            // 
            this.csSaveAs.Name = "csSaveAs";
            this.csSaveAs.Size = new System.Drawing.Size(152, 22);
            this.csSaveAs.Text = "Save&As...";
            this.csSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep2
            // 
            this.csSep2.Name = "csSep2";
            this.csSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // csPrint
            // 
            this.csPrint.Name = "csPrint";
            this.csPrint.Size = new System.Drawing.Size(152, 22);
            this.csPrint.Text = "&Print...";
            this.csPrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csPreview
            // 
            this.csPreview.Name = "csPreview";
            this.csPreview.Size = new System.Drawing.Size(152, 22);
            this.csPreview.Text = "&Print Preview...";
            this.csPreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // dlgTLDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(802, 362);
            this.Controls.Add(this.grdTLDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "dlgTLDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TL Detail";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdTLDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLDetail)).EndInit();
            this.csDialog.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdTLDetail;
        private FreightDataset mTLDetail;
        private System.Windows.Forms.ContextMenuStrip csDialog;
        private System.Windows.Forms.ToolStripMenuItem csRefresh;
        private System.Windows.Forms.ToolStripSeparator csSep1;
        private System.Windows.Forms.ToolStripMenuItem csSaveAs;
        private System.Windows.Forms.ToolStripSeparator csSep2;
        private System.Windows.Forms.ToolStripMenuItem csPrint;
        private System.Windows.Forms.ToolStripMenuItem csPreview;
    }
}