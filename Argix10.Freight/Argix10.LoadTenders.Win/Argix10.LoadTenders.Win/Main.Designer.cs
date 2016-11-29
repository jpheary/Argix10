namespace Argix.Freight {
    partial class frmMain {
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
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("LoadTenderTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn13 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Load");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn14 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ReferenceNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn15 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Location");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn16 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn17 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AddressLine2");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn18 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("City");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn19 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("StateOrProvince");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn20 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("PostalCode");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn21 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Barcode1");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Barcode2");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsNew = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsOpen = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRefresh = new System.Windows.Forms.ToolStripButton();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mLoadTenderDS = new Argix.LoadTenderDS();
            this.mClientDS = new Argix.ClientDS();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrint = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePageSetup = new System.Windows.Forms.ToolStripMenuItem();
            this.msFilePrintPreview = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSep2 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileSep3 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this._lblClient = new System.Windows.Forms.Label();
            this._lblFrom = new System.Windows.Forms.Label();
            this._lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mLoadTenderDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClientDS)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msEdit,
            this.msView,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(767, 24);
            this.msMain.TabIndex = 1;
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileOpen,
            this.msFileSep1,
            this.msFileSave,
            this.msFileSaveAs,
            this.msFileSep2,
            this.msFilePrint,
            this.msFilePageSetup,
            this.msFilePrintPreview,
            this.msFileSep3,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37, 20);
            this.msFile.Text = "&File";
            // 
            // msEdit
            // 
            this.msEdit.Name = "msEdit";
            this.msEdit.Size = new System.Drawing.Size(39, 20);
            this.msEdit.Text = "&Edit";
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewToolbar,
            this.msViewStatusbar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 20);
            this.msView.Text = "&View";
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout,
            this.msHelpSep1});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 20);
            this.msHelp.Text = "&Help";
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.toolStripSeparator1,
            this.tsOpen,
            this.toolStripSeparator2,
            this.tsRefresh});
            this.tsMain.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(767, 56);
            this.tsMain.TabIndex = 4;
            // 
            // tsNew
            // 
            this.tsNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(38, 53);
            this.tsNew.Text = "New";
            this.tsNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsNew.ToolTipText = "New pickup request";
            this.tsNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 56);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::Argix.Properties.Resources.Open;
            this.tsOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(44, 53);
            this.tsOpen.Text = "Open";
            this.tsOpen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsOpen.ToolTipText = "Open pickup request";
            this.tsOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 56);
            // 
            // tsRefresh
            // 
            this.tsRefresh.Image = global::Argix.Properties.Resources.RefreshDocView;
            this.tsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsRefresh.Name = "tsRefresh";
            this.tsRefresh.Size = new System.Drawing.Size(56, 53);
            this.tsRefresh.Text = "Refresh";
            this.tsRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsRefresh.ToolTipText = "Refresh";
            this.tsRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 226);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(767, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 6;
            this.ssMain.TerminalText = "Terminal";
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdMain.DataMember = "LoadTenderTable";
            this.grdMain.DataSource = this.mLoadTenderDS;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.Name = "Verdana";
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance13;
            ultraGridColumn13.Header.VisiblePosition = 0;
            ultraGridColumn13.Width = 120;
            ultraGridColumn14.Header.Caption = "Reference#";
            ultraGridColumn14.Header.VisiblePosition = 1;
            ultraGridColumn15.Header.VisiblePosition = 4;
            ultraGridColumn16.Header.Caption = "Address Line 1";
            ultraGridColumn16.Header.VisiblePosition = 5;
            ultraGridColumn17.Header.Caption = "Address Line 2";
            ultraGridColumn17.Header.VisiblePosition = 6;
            ultraGridColumn18.Header.VisiblePosition = 7;
            ultraGridColumn19.Header.Caption = "State";
            ultraGridColumn19.Header.VisiblePosition = 8;
            ultraGridColumn20.Header.Caption = "Zip";
            ultraGridColumn20.Header.VisiblePosition = 9;
            ultraGridColumn21.Header.Caption = "Location#";
            ultraGridColumn21.Header.VisiblePosition = 2;
            ultraGridColumn22.Header.Caption = "Location ID";
            ultraGridColumn22.Header.VisiblePosition = 3;
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
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
            appearance14.FontData.Name = "Verdana";
            appearance14.FontData.SizeInPoints = 8F;
            appearance14.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance14.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.CaptionAppearance = appearance14;
            this.grdMain.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMain.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance15.BackColor = System.Drawing.SystemColors.Control;
            appearance15.FontData.BoldAsString = "True";
            appearance15.FontData.Name = "Verdana";
            appearance15.FontData.SizeInPoints = 8F;
            appearance15.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Override.HeaderAppearance = appearance15;
            this.grdMain.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMain.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance16.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMain.DisplayLayout.Override.RowAppearance = appearance16;
            this.grdMain.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdMain.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMain.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMain.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMain.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdMain.Location = new System.Drawing.Point(0, 121);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(767, 105);
            this.grdMain.TabIndex = 3;
            this.grdMain.Text = "Load Tenders";
            this.grdMain.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnGridMouseDown);
            // 
            // mLoadTenderDS
            // 
            this.mLoadTenderDS.DataSetName = "LoadTenderDS";
            this.mLoadTenderDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mClientDS
            // 
            this.mClientDS.DataSetName = "ClientDS";
            this.mClientDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // msFileNew
            // 
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(152, 22);
            this.msFileNew.Text = "&New...";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileOpen
            // 
            this.msFileOpen.Name = "msFileOpen";
            this.msFileOpen.Size = new System.Drawing.Size(152, 22);
            this.msFileOpen.Text = "&Open...";
            this.msFileOpen.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSave
            // 
            this.msFileSave.Name = "msFileSave";
            this.msFileSave.Size = new System.Drawing.Size(152, 22);
            this.msFileSave.Text = "&Save";
            this.msFileSave.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSaveAs
            // 
            this.msFileSaveAs.Name = "msFileSaveAs";
            this.msFileSaveAs.Size = new System.Drawing.Size(152, 22);
            this.msFileSaveAs.Text = "Save &As...";
            this.msFileSaveAs.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrint
            // 
            this.msFilePrint.Name = "msFilePrint";
            this.msFilePrint.Size = new System.Drawing.Size(152, 22);
            this.msFilePrint.Text = "&Print...";
            this.msFilePrint.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePageSetup
            // 
            this.msFilePageSetup.Name = "msFilePageSetup";
            this.msFilePageSetup.Size = new System.Drawing.Size(152, 22);
            this.msFilePageSetup.Text = "Pa&ge Setup...";
            this.msFilePageSetup.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFilePrintPreview
            // 
            this.msFilePrintPreview.Name = "msFilePrintPreview";
            this.msFilePrintPreview.Size = new System.Drawing.Size(152, 22);
            this.msFilePrintPreview.Text = "Print Pre&view...";
            this.msFilePrintPreview.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(152, 22);
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileSep2
            // 
            this.msFileSep2.Name = "msFileSep2";
            this.msFileSep2.Size = new System.Drawing.Size(149, 6);
            // 
            // msFileSep3
            // 
            this.msFileSep3.Name = "msFileSep3";
            this.msFileSep3.Size = new System.Drawing.Size(149, 6);
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(152, 22);
            this.msViewRefresh.Text = "&Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(152, 22);
            this.msViewToolbar.Text = "&Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusbar
            // 
            this.msViewStatusbar.Name = "msViewStatusbar";
            this.msViewStatusbar.Size = new System.Drawing.Size(152, 22);
            this.msViewStatusbar.Text = "&Statusbar";
            this.msViewStatusbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(152, 22);
            this.msHelpAbout.Text = "&About...";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelpSep1
            // 
            this.msHelpSep1.Name = "msHelpSep1";
            this.msHelpSep1.Size = new System.Drawing.Size(149, 6);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dtpTo);
            this.panel1.Controls.Add(this.dtpFrom);
            this.panel1.Controls.Add(this._lblTo);
            this.panel1.Controls.Add(this._lblFrom);
            this.panel1.Controls.Add(this._lblClient);
            this.panel1.Controls.Add(this.cboClient);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 80);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(767, 43);
            this.panel1.TabIndex = 7;
            // 
            // cboClient
            // 
            this.cboClient.DataSource = this.mClientDS;
            this.cboClient.DisplayMember = "ClientTable.ClientName";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(85, 14);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(215, 21);
            this.cboClient.TabIndex = 0;
            this.cboClient.ValueMember = "ClientTable.ClientNumber";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(12, 12);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(67, 23);
            this._lblClient.TabIndex = 1;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblFrom
            // 
            this._lblFrom.Location = new System.Drawing.Point(305, 12);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(37, 23);
            this._lblFrom.TabIndex = 2;
            this._lblFrom.Text = "From";
            this._lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblTo
            // 
            this._lblTo.Location = new System.Drawing.Point(532, 12);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(37, 23);
            this._lblTo.TabIndex = 3;
            this._lblTo.Text = "To";
            this._lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(349, 15);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(180, 20);
            this.dtpFrom.TabIndex = 4;
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnDatesChanged);
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(565, 14);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(180, 20);
            this.dtpTo.TabIndex = 5;
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnDatesChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(767, 250);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.MainMenuStrip = this.msMain;
            this.Name = "frmMain";
            this.Text = "Argix Logistics Load Tenders";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mLoadTenderDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClientDS)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem msFile;
        private System.Windows.Forms.ToolStripMenuItem msEdit;
        private System.Windows.Forms.ToolStripMenuItem msHelp;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsNew;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton tsOpen;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton tsRefresh;
        private System.Windows.Forms.ToolStripMenuItem msView;
        private Argix.Windows.ArgixStatusBar ssMain;
        private LoadTenderDS mLoadTenderDS;
        private ClientDS mClientDS;
        private System.Windows.Forms.ToolStripMenuItem msFileNew;
        private System.Windows.Forms.ToolStripMenuItem msFileOpen;
        private System.Windows.Forms.ToolStripSeparator msFileSep1;
        private System.Windows.Forms.ToolStripMenuItem msFileSave;
        private System.Windows.Forms.ToolStripMenuItem msFileSaveAs;
        private System.Windows.Forms.ToolStripSeparator msFileSep2;
        private System.Windows.Forms.ToolStripMenuItem msFilePrint;
        private System.Windows.Forms.ToolStripMenuItem msFilePageSetup;
        private System.Windows.Forms.ToolStripMenuItem msFilePrintPreview;
        private System.Windows.Forms.ToolStripSeparator msFileSep3;
        private System.Windows.Forms.ToolStripMenuItem msFileExit;
        private System.Windows.Forms.ToolStripMenuItem msViewRefresh;
        private System.Windows.Forms.ToolStripSeparator msViewSep1;
        private System.Windows.Forms.ToolStripMenuItem msViewToolbar;
        private System.Windows.Forms.ToolStripMenuItem msViewStatusbar;
        private System.Windows.Forms.ToolStripMenuItem msHelpAbout;
        private System.Windows.Forms.ToolStripSeparator msHelpSep1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label _lblTo;
        private System.Windows.Forms.Label _lblFrom;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.ComboBox cboClient;
    }
}

