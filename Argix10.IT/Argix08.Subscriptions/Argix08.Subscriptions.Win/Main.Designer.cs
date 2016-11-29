namespace Argix {
    partial class Main {
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
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SubscriptionTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Send");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Report");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubscriptionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EventType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastRun");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Active");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliverySettings");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MatchData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Parameters");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand2 = new Infragistics.Win.UltraWinGrid.UltraGridBand("SubscriptionTable",-1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Send");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Report");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SubscriptionID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Description");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("EventType");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastRun");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Status");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Active");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("DeliverySettings");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("MatchData");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Parameters");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.grd05 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSubs05 = new Argix.SubscriptionDS();
            this.grd08 = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mSubs08 = new Argix.SubscriptionDS();
            ((System.ComponentModel.ISupportInitialize)(this.grd05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubs05)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd08)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubs08)).BeginInit();
            this.SuspendLayout();
            // 
            // grd05
            // 
            this.grd05.DataSource = this.mSubs05;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grd05.DisplayLayout.Appearance = appearance1;
            ultraGridColumn13.Header.VisiblePosition = 0;
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn15.Header.VisiblePosition = 2;
            ultraGridColumn16.Header.VisiblePosition = 3;
            ultraGridColumn17.Header.VisiblePosition = 4;
            ultraGridColumn18.Header.VisiblePosition = 5;
            ultraGridColumn19.Header.VisiblePosition = 6;
            ultraGridColumn20.Header.VisiblePosition = 7;
            ultraGridColumn21.Header.VisiblePosition = 8;
            ultraGridColumn22.Header.VisiblePosition = 9;
            ultraGridColumn23.Header.VisiblePosition = 10;
            ultraGridColumn24.Header.VisiblePosition = 11;
            ultraGridBand1.Columns.AddRange(new object[] {
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
            ultraGridColumn24});
            this.grd05.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grd05.DisplayLayout.CaptionAppearance = appearance2;
            this.grd05.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grd05.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grd05.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grd05.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grd05.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grd05.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grd05.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grd05.DisplayLayout.Override.RowAppearance = appearance4;
            this.grd05.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grd05.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grd05.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grd05.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grd05.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grd05.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grd05.Dock = System.Windows.Forms.DockStyle.Top;
            this.grd05.Location = new System.Drawing.Point(0,0);
            this.grd05.Name = "grd05";
            this.grd05.Size = new System.Drawing.Size(692,123);
            this.grd05.TabIndex = 0;
            this.grd05.Text = "rgxsqlrpts05";
            // 
            // mSubs05
            // 
            this.mSubs05.DataSetName = "SubscriptionDS";
            this.mSubs05.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grd08
            // 
            this.grd08.DataSource = this.mSubs08;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grd08.DisplayLayout.Appearance = appearance13;
            ultraGridColumn25.Header.VisiblePosition = 0;
            ultraGridColumn26.Header.VisiblePosition = 1;
            ultraGridColumn27.Header.VisiblePosition = 2;
            ultraGridColumn28.Header.VisiblePosition = 3;
            ultraGridColumn29.Header.VisiblePosition = 4;
            ultraGridColumn30.Header.VisiblePosition = 5;
            ultraGridColumn31.Header.VisiblePosition = 6;
            ultraGridColumn32.Header.VisiblePosition = 7;
            ultraGridColumn33.Header.VisiblePosition = 8;
            ultraGridColumn34.Header.VisiblePosition = 9;
            ultraGridColumn35.Header.VisiblePosition = 10;
            ultraGridColumn36.Header.VisiblePosition = 11;
            ultraGridBand2.Columns.AddRange(new object[] {
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36});
            this.grd08.DisplayLayout.BandsSerializer.Add(ultraGridBand2);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grd08.DisplayLayout.CaptionAppearance = appearance14;
            this.grd08.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grd08.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grd08.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grd08.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.grd08.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grd08.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grd08.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grd08.DisplayLayout.Override.RowAppearance = appearance16;
            this.grd08.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grd08.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grd08.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grd08.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grd08.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grd08.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grd08.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.grd08.Location = new System.Drawing.Point(0,136);
            this.grd08.Name = "grd08";
            this.grd08.Size = new System.Drawing.Size(692,123);
            this.grd08.TabIndex = 1;
            this.grd08.Text = "rgxvmsqlrpt08";
            // 
            // mSubs08
            // 
            this.mSubs08.DataSetName = "SubscriptionDS";
            this.mSubs08.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F,13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(692,259);
            this.Controls.Add(this.grd08);
            this.Controls.Add(this.grd05);
            this.Name = "Main";
            this.Text = "Subscriptions";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grd05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubs05)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grd08)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mSubs08)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Infragistics.Win.UltraWinGrid.UltraGrid grd05;
        private SubscriptionDS mSubs05;
        private Infragistics.Win.UltraWinGrid.UltraGrid grd08;
        private SubscriptionDS mSubs08;
    }
}

