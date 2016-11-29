namespace Argix.Finance {
    partial class winLTLRates {
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
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            this.grdRates = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.rateQuoteDataset1 = new Argix.RateQuoteDataset();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cmsExport = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.grdRates)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateQuoteDataset1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grdRates
            // 
            this.grdRates.ContextMenuStrip = this.contextMenuStrip1;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.Appearance = appearance5;
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.CaptionAppearance = appearance6;
            this.grdRates.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdRates.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.TextHAlignAsString = "Left";
            this.grdRates.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdRates.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdRates.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdRates.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdRates.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdRates.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdRates.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdRates.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdRates.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdRates.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdRates.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRates.Location = new System.Drawing.Point(0, 0);
            this.grdRates.Name = "grdRates";
            this.grdRates.Size = new System.Drawing.Size(823, 284);
            this.grdRates.TabIndex = 23;
            this.grdRates.Text = "Rate Response";
            this.grdRates.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // rateQuoteDataset1
            // 
            this.rateQuoteDataset1.DataSetName = "RateQuoteDataset";
            this.rateQuoteDataset1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cmsExport});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(108, 26);
            // 
            // cmsExport
            // 
            this.cmsExport.Name = "cmsExport";
            this.cmsExport.Size = new System.Drawing.Size(107, 22);
            this.cmsExport.Text = "Export";
            this.cmsExport.Click += new System.EventHandler(this.OnExport);
            // 
            // winLTLRates
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(823, 284);
            this.Controls.Add(this.grdRates);
            this.Name = "winLTLRates";
            this.Text = "LTL Rates";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdRates)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rateQuoteDataset1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grdRates;
        private RateQuoteDataset rateQuoteDataset1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem cmsExport;
    }
}