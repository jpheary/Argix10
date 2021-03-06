﻿namespace Argix.Freight {
    partial class winConsignees {
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("LTLConsigneeTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ConsigneeNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Name");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn6 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn7 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn8 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("State");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn9 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn10 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zip4");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn11 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn12 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactPhone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ContactEmail");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WindowTimeStart");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WindowTimeEnd");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IsActive");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastUpdated");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Rowversion");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.mConsignees = new Argix.FreightDataset();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.mClients = new Argix.FreightDataset();
            this.csConsignees = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csNew = new System.Windows.Forms.ToolStripMenuItem();
            this.csOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.csSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.csRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.grdConsignees = new Infragistics.Win.UltraWinGrid.UltraGrid();
            ((System.ComponentModel.ISupportInitialize)(this.mConsignees)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).BeginInit();
            this.csConsignees.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdConsignees)).BeginInit();
            this.SuspendLayout();
            // 
            // mConsignees
            // 
            this.mConsignees.DataSetName = "FreightDataset";
            this.mConsignees.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboClient
            // 
            this.cboClient.DataSource = this.mClients;
            this.cboClient.DisplayMember = "LTLClientTable.Name";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(100, 1);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(300, 21);
            this.cboClient.TabIndex = 7;
            this.cboClient.ValueMember = "LTLClientTable.ClientNumber";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
            // 
            // mClients
            // 
            this.mClients.DataSetName = "FreightDataset";
            this.mClients.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // csConsignees
            // 
            this.csConsignees.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csNew,
            this.csOpen,
            this.csSep1,
            this.csRefresh});
            this.csConsignees.Name = "csBookings";
            this.csConsignees.Size = new System.Drawing.Size(114, 76);
            // 
            // csNew
            // 
            this.csNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.csNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csNew.Name = "csNew";
            this.csNew.Size = new System.Drawing.Size(113, 22);
            this.csNew.Text = "&New";
            this.csNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csOpen
            // 
            this.csOpen.Image = global::Argix.Properties.Resources.Open;
            this.csOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csOpen.Name = "csOpen";
            this.csOpen.Size = new System.Drawing.Size(113, 22);
            this.csOpen.Text = "&Open";
            this.csOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csSep1
            // 
            this.csSep1.Name = "csSep1";
            this.csSep1.Size = new System.Drawing.Size(110, 6);
            // 
            // csRefresh
            // 
            this.csRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.csRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.csRefresh.Name = "csRefresh";
            this.csRefresh.Size = new System.Drawing.Size(113, 22);
            this.csRefresh.Text = "&Refresh";
            this.csRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // grdConsignees
            // 
            this.grdConsignees.ContextMenuStrip = this.csConsignees;
            this.grdConsignees.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdConsignees.DataMember = "LTLConsigneeTable";
            this.grdConsignees.DataSource = this.mConsignees;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdConsignees.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Consignee#";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Width = 75;
            ultraGridColumn2.Header.Caption = "Client#";
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 50;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 200;
            ultraGridColumn5.Header.Caption = "Street 1";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 200;
            ultraGridColumn6.Header.Caption = "Street 2";
            ultraGridColumn6.Header.VisiblePosition = 5;
            ultraGridColumn7.Header.VisiblePosition = 6;
            ultraGridColumn8.Header.VisiblePosition = 7;
            ultraGridColumn8.Width = 50;
            ultraGridColumn9.Header.VisiblePosition = 8;
            ultraGridColumn9.Width = 75;
            ultraGridColumn10.Header.VisiblePosition = 9;
            ultraGridColumn10.Hidden = true;
            ultraGridColumn11.Header.Caption = "Contact";
            ultraGridColumn11.Header.VisiblePosition = 10;
            ultraGridColumn12.Header.Caption = "Phone";
            ultraGridColumn12.Header.VisiblePosition = 11;
            ultraGridColumn13.Header.Caption = "Email";
            ultraGridColumn13.Header.VisiblePosition = 12;
            ultraGridColumn14.Format = "HH:mm";
            ultraGridColumn14.Header.Caption = "Window Start";
            ultraGridColumn14.Header.VisiblePosition = 13;
            ultraGridColumn14.Width = 75;
            ultraGridColumn15.Format = "HH:mm";
            ultraGridColumn15.Header.Caption = "Window End";
            ultraGridColumn15.Header.VisiblePosition = 14;
            ultraGridColumn15.Width = 75;
            ultraGridColumn16.Header.Caption = "Active?";
            ultraGridColumn16.Header.VisiblePosition = 15;
            ultraGridColumn16.Style = Infragistics.Win.UltraWinGrid.ColumnStyle.CheckBox;
            ultraGridColumn16.Width = 50;
            ultraGridColumn17.Header.VisiblePosition = 16;
            ultraGridColumn17.Hidden = true;
            ultraGridColumn18.Header.VisiblePosition = 17;
            ultraGridColumn18.Hidden = true;
            ultraGridColumn19.Header.VisiblePosition = 18;
            ultraGridColumn19.Hidden = true;
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
            ultraGridColumn19});
            this.grdConsignees.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdConsignees.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.SizeInPoints = 9F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdConsignees.DisplayLayout.CaptionAppearance = appearance2;
            this.grdConsignees.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdConsignees.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdConsignees.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdConsignees.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdConsignees.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdConsignees.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortMulti;
            this.grdConsignees.DisplayLayout.Override.MaxSelectedRows = 0;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdConsignees.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdConsignees.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdConsignees.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Extended;
            this.grdConsignees.DisplayLayout.Override.TipStyleScroll = Infragistics.Win.UltraWinGrid.TipStyle.Show;
            this.grdConsignees.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdConsignees.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdConsignees.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdConsignees.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdConsignees.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdConsignees.Location = new System.Drawing.Point(0, 0);
            this.grdConsignees.Name = "grdConsignees";
            this.grdConsignees.Size = new System.Drawing.Size(902, 283);
            this.grdConsignees.TabIndex = 6;
            this.grdConsignees.Text = "Consignees for";
            this.grdConsignees.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdConsignees.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridAfterSelect);
            this.grdConsignees.DoubleClick += new System.EventHandler(this.OnGridDoubleClick);
            this.grdConsignees.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // winConsignees
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(902, 283);
            this.Controls.Add(this.cboClient);
            this.Controls.Add(this.grdConsignees);
            this.Name = "winConsignees";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Consignees";
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Deactivate += new System.EventHandler(this.OnFormDeactivate);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FontChanged += new System.EventHandler(this.OnFormFontChanged);
            ((System.ComponentModel.ISupportInitialize)(this.mConsignees)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).EndInit();
            this.csConsignees.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdConsignees)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private FreightDataset mConsignees;
        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.ContextMenuStrip csConsignees;
        private System.Windows.Forms.ToolStripMenuItem csNew;
        private System.Windows.Forms.ToolStripMenuItem csOpen;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdConsignees;
        private FreightDataset mClients;
        private System.Windows.Forms.ToolStripSeparator csSep1;
        private System.Windows.Forms.ToolStripMenuItem csRefresh;
    }
}