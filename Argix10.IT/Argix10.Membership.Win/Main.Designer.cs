namespace Argix {
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
            this.components = new System.ComponentModel.Container();
            Infragistics.Win.Appearance appearance13 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("UserTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ApplicationId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ApplicationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserId");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("LastActivityDate");
            Infragistics.Win.Appearance appearance14 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance15 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance16 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.msMain = new System.Windows.Forms.MenuStrip();
            this.msFile = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.msFileSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msFileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.msView = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewFont = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.msViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.msViewStatusBar = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.msHelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.tsMain = new System.Windows.Forms.ToolStrip();
            this.tsbNew = new System.Windows.Forms.ToolStripButton();
            this.tsbDelete = new System.Windows.Forms.ToolStripButton();
            this.tsbSep1 = new System.Windows.Forms.ToolStripSeparator();
            this.tsbRefresh = new System.Windows.Forms.ToolStripButton();
            this.grdMain = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.csMain = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.csNew = new System.Windows.Forms.ToolStripMenuItem();
            this.csDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mUsers = new Argix.MembershipDataset();
            this.ssMain = new Argix.Windows.ArgixStatusBar();
            this.lstRoles = new System.Windows.Forms.ListBox();
            this.mRoles = new Argix.MembershipDataset();
            this.lstUserRoles = new System.Windows.Forms.ListBox();
            this.mUserRoles = new Argix.MembershipDataset();
            this.grpUser = new System.Windows.Forms.GroupBox();
            this._lblUserRoles = new System.Windows.Forms.Label();
            this._lblRoles = new System.Windows.Forms.Label();
            this.btnRem = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this._lblNote = new System.Windows.Forms.Label();
            this.msMain.SuspendLayout();
            this.tsMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).BeginInit();
            this.csMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mUserRoles)).BeginInit();
            this.grpUser.SuspendLayout();
            this.SuspendLayout();
            // 
            // msMain
            // 
            this.msMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFile,
            this.msView,
            this.msHelp});
            this.msMain.Location = new System.Drawing.Point(0, 0);
            this.msMain.Name = "msMain";
            this.msMain.Size = new System.Drawing.Size(683, 24);
            this.msMain.TabIndex = 0;
            this.msMain.Text = "menuStrip1";
            // 
            // msFile
            // 
            this.msFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msFileNew,
            this.msFileDelete,
            this.msFileSep1,
            this.msFileExit});
            this.msFile.Name = "msFile";
            this.msFile.Size = new System.Drawing.Size(37, 20);
            this.msFile.Text = "&File";
            // 
            // msFileNew
            // 
            this.msFileNew.Name = "msFileNew";
            this.msFileNew.Size = new System.Drawing.Size(133, 22);
            this.msFileNew.Text = "&New User";
            this.msFileNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileDelete
            // 
            this.msFileDelete.Name = "msFileDelete";
            this.msFileDelete.Size = new System.Drawing.Size(133, 22);
            this.msFileDelete.Text = "&Delete User";
            this.msFileDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msFileSep1
            // 
            this.msFileSep1.Name = "msFileSep1";
            this.msFileSep1.Size = new System.Drawing.Size(130, 6);
            // 
            // msFileExit
            // 
            this.msFileExit.Name = "msFileExit";
            this.msFileExit.Size = new System.Drawing.Size(133, 22);
            this.msFileExit.Text = "E&xit";
            this.msFileExit.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msView
            // 
            this.msView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msViewRefresh,
            this.msViewSep1,
            this.msViewFont,
            this.toolStripMenuItem1,
            this.msViewToolbar,
            this.msViewStatusBar});
            this.msView.Name = "msView";
            this.msView.Size = new System.Drawing.Size(44, 20);
            this.msView.Text = "&View";
            // 
            // msViewRefresh
            // 
            this.msViewRefresh.Name = "msViewRefresh";
            this.msViewRefresh.Size = new System.Drawing.Size(123, 22);
            this.msViewRefresh.Text = "&Refresh";
            this.msViewRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewSep1
            // 
            this.msViewSep1.Name = "msViewSep1";
            this.msViewSep1.Size = new System.Drawing.Size(120, 6);
            // 
            // msViewFont
            // 
            this.msViewFont.Name = "msViewFont";
            this.msViewFont.Size = new System.Drawing.Size(123, 22);
            this.msViewFont.Text = "&Font...";
            this.msViewFont.Click += new System.EventHandler(this.OnItemClick);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(120, 6);
            // 
            // msViewToolbar
            // 
            this.msViewToolbar.Name = "msViewToolbar";
            this.msViewToolbar.Size = new System.Drawing.Size(123, 22);
            this.msViewToolbar.Text = "&Toolbar";
            this.msViewToolbar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msViewStatusBar
            // 
            this.msViewStatusBar.Name = "msViewStatusBar";
            this.msViewStatusBar.Size = new System.Drawing.Size(123, 22);
            this.msViewStatusBar.Text = "&StatusBar";
            this.msViewStatusBar.Click += new System.EventHandler(this.OnItemClick);
            // 
            // msHelp
            // 
            this.msHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.msHelpAbout});
            this.msHelp.Name = "msHelp";
            this.msHelp.Size = new System.Drawing.Size(44, 20);
            this.msHelp.Text = "&Help";
            // 
            // msHelpAbout
            // 
            this.msHelpAbout.Name = "msHelpAbout";
            this.msHelpAbout.Size = new System.Drawing.Size(107, 22);
            this.msHelpAbout.Text = "&About";
            this.msHelpAbout.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsMain
            // 
            this.tsMain.AutoSize = false;
            this.tsMain.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tsMain.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.tsMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbNew,
            this.tsbDelete,
            this.tsbSep1,
            this.tsbRefresh});
            this.tsMain.Location = new System.Drawing.Point(0, 24);
            this.tsMain.Name = "tsMain";
            this.tsMain.Size = new System.Drawing.Size(683, 52);
            this.tsMain.TabIndex = 1;
            // 
            // tsbNew
            // 
            this.tsbNew.Image = global::Argix.Properties.Resources.NewDocument;
            this.tsbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbNew.Name = "tsbNew";
            this.tsbNew.Size = new System.Drawing.Size(61, 49);
            this.tsbNew.Text = "New User";
            this.tsbNew.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsbDelete
            // 
            this.tsbDelete.Image = global::Argix.Properties.Resources.Delete;
            this.tsbDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbDelete.Name = "tsbDelete";
            this.tsbDelete.Size = new System.Drawing.Size(70, 49);
            this.tsbDelete.Text = "Delete User";
            this.tsbDelete.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // tsbSep1
            // 
            this.tsbSep1.Name = "tsbSep1";
            this.tsbSep1.Size = new System.Drawing.Size(6, 52);
            // 
            // tsbRefresh
            // 
            this.tsbRefresh.Image = global::Argix.Properties.Resources.Refresh;
            this.tsbRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbRefresh.Name = "tsbRefresh";
            this.tsbRefresh.Size = new System.Drawing.Size(50, 49);
            this.tsbRefresh.Text = "Refresh";
            this.tsbRefresh.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText;
            this.tsbRefresh.Click += new System.EventHandler(this.OnItemClick);
            // 
            // grdMain
            // 
            this.grdMain.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.grdMain.ContextMenuStrip = this.csMain;
            this.grdMain.DataMember = "UserTable";
            this.grdMain.DataSource = this.mUsers;
            appearance13.BackColor = System.Drawing.SystemColors.Window;
            appearance13.FontData.SizeInPoints = 8F;
            appearance13.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance13.TextHAlignAsString = "Left";
            this.grdMain.DisplayLayout.Appearance = appearance13;
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 200;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Hidden = true;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Header.Caption = "User Name";
            ultraGridColumn4.Header.VisiblePosition = 3;
            ultraGridColumn4.Width = 150;
            ultraGridColumn5.Header.Caption = "Last Activity";
            ultraGridColumn5.Header.VisiblePosition = 4;
            ultraGridColumn5.Width = 100;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
            this.grdMain.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance14.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance14.FontData.BoldAsString = "True";
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
            this.grdMain.Location = new System.Drawing.Point(0, 79);
            this.grdMain.Name = "grdMain";
            this.grdMain.Size = new System.Drawing.Size(255, 275);
            this.grdMain.TabIndex = 2;
            this.grdMain.Text = "Argix Domain Users";
            this.grdMain.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdMain.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnUserSelected);
            // 
            // csMain
            // 
            this.csMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.csNew,
            this.csDelete});
            this.csMain.Name = "csMain";
            this.csMain.Size = new System.Drawing.Size(134, 48);
            // 
            // csNew
            // 
            this.csNew.Name = "csNew";
            this.csNew.Size = new System.Drawing.Size(133, 22);
            this.csNew.Text = "&New User";
            this.csNew.Click += new System.EventHandler(this.OnItemClick);
            // 
            // csDelete
            // 
            this.csDelete.Name = "csDelete";
            this.csDelete.Size = new System.Drawing.Size(133, 22);
            this.csDelete.Text = "&Delete User";
            this.csDelete.Click += new System.EventHandler(this.OnItemClick);
            // 
            // mUsers
            // 
            this.mUsers.DataSetName = "MembershipDataset";
            this.mUsers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // ssMain
            // 
            this.ssMain.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ssMain.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ssMain.Location = new System.Drawing.Point(0, 360);
            this.ssMain.Name = "ssMain";
            this.ssMain.Size = new System.Drawing.Size(683, 24);
            this.ssMain.StatusText = "";
            this.ssMain.TabIndex = 101;
            this.ssMain.TerminalText = "Local Terminal";
            // 
            // lstRoles
            // 
            this.lstRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstRoles.DataSource = this.mRoles;
            this.lstRoles.DisplayMember = "RoleTable.RoleName";
            this.lstRoles.FormattingEnabled = true;
            this.lstRoles.Location = new System.Drawing.Point(22, 38);
            this.lstRoles.Name = "lstRoles";
            this.lstRoles.Size = new System.Drawing.Size(145, 199);
            this.lstRoles.TabIndex = 102;
            // 
            // mRoles
            // 
            this.mRoles.DataSetName = "MembershipDataset";
            this.mRoles.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lstUserRoles
            // 
            this.lstUserRoles.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.lstUserRoles.DataSource = this.mUserRoles;
            this.lstUserRoles.DisplayMember = "RoleTable.RoleName";
            this.lstUserRoles.FormattingEnabled = true;
            this.lstUserRoles.Location = new System.Drawing.Point(227, 38);
            this.lstUserRoles.Name = "lstUserRoles";
            this.lstUserRoles.Size = new System.Drawing.Size(145, 199);
            this.lstUserRoles.TabIndex = 103;
            // 
            // mUserRoles
            // 
            this.mUserRoles.DataSetName = "MembershipDataset";
            this.mUserRoles.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpUser
            // 
            this.grpUser.Controls.Add(this._lblUserRoles);
            this.grpUser.Controls.Add(this._lblRoles);
            this.grpUser.Controls.Add(this.btnRem);
            this.grpUser.Controls.Add(this.btnAdd);
            this.grpUser.Controls.Add(this.lstRoles);
            this.grpUser.Controls.Add(this.lstUserRoles);
            this.grpUser.Location = new System.Drawing.Point(278, 79);
            this.grpUser.Name = "grpUser";
            this.grpUser.Size = new System.Drawing.Size(392, 254);
            this.grpUser.TabIndex = 104;
            this.grpUser.TabStop = false;
            this.grpUser.Text = "Role Assignments";
            // 
            // _lblUserRoles
            // 
            this._lblUserRoles.Location = new System.Drawing.Point(227, 20);
            this._lblUserRoles.Name = "_lblUserRoles";
            this._lblUserRoles.Size = new System.Drawing.Size(144, 15);
            this._lblUserRoles.TabIndex = 107;
            this._lblUserRoles.Text = "Assigned Roles";
            // 
            // _lblRoles
            // 
            this._lblRoles.Location = new System.Drawing.Point(22, 20);
            this._lblRoles.Name = "_lblRoles";
            this._lblRoles.Size = new System.Drawing.Size(141, 15);
            this._lblRoles.TabIndex = 106;
            this._lblRoles.Text = "All Roles";
            // 
            // btnRem
            // 
            this.btnRem.Location = new System.Drawing.Point(178, 144);
            this.btnRem.Name = "btnRem";
            this.btnRem.Size = new System.Drawing.Size(36, 20);
            this.btnRem.TabIndex = 105;
            this.btnRem.Text = "<<";
            this.btnRem.UseVisualStyleBackColor = true;
            this.btnRem.Click += new System.EventHandler(this.OnButtonCommand);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(178, 96);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(36, 20);
            this.btnAdd.TabIndex = 104;
            this.btnAdd.Text = ">>";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.OnButtonCommand);
            // 
            // _lblNote
            // 
            this._lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this._lblNote.Location = new System.Drawing.Point(278, 336);
            this._lblNote.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this._lblNote.Name = "_lblNote";
            this._lblNote.Size = new System.Drawing.Size(392, 17);
            this._lblNote.TabIndex = 105;
            this._lblNote.Text = "Note: Administrator role is REQUIRED to create/edit role assignments.";
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(683, 384);
            this.Controls.Add(this._lblNote);
            this.Controls.Add(this.grpUser);
            this.Controls.Add(this.ssMain);
            this.Controls.Add(this.grdMain);
            this.Controls.Add(this.tsMain);
            this.Controls.Add(this.msMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.msMain;
            this.MaximizeBox = false;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Argix Logistics Role Manager";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.msMain.ResumeLayout(false);
            this.msMain.PerformLayout();
            this.tsMain.ResumeLayout(false);
            this.tsMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMain)).EndInit();
            this.csMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mUsers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mUserRoles)).EndInit();
            this.grpUser.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msMain;
        private System.Windows.Forms.ToolStripMenuItem msFile;
        private System.Windows.Forms.ToolStripMenuItem msHelp;
        private System.Windows.Forms.ToolStrip tsMain;
        private System.Windows.Forms.ToolStripButton tsbNew;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdMain;
        private Windows.ArgixStatusBar ssMain;
        private MembershipDataset mUsers;
        private System.Windows.Forms.ListBox lstRoles;
        private MembershipDataset mRoles;
        private System.Windows.Forms.ListBox lstUserRoles;
        private MembershipDataset mUserRoles;
        private System.Windows.Forms.GroupBox grpUser;
        private System.Windows.Forms.Button btnRem;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Label _lblUserRoles;
        private System.Windows.Forms.Label _lblRoles;
        private System.Windows.Forms.ToolStripMenuItem msFileExit;
        private System.Windows.Forms.ToolStripMenuItem msHelpAbout;
        private System.Windows.Forms.ToolStripMenuItem msFileNew;
        private System.Windows.Forms.ToolStripSeparator msFileSep1;
        private System.Windows.Forms.ToolStripMenuItem msView;
        private System.Windows.Forms.ToolStripMenuItem msViewRefresh;
        private System.Windows.Forms.ToolStripSeparator msViewSep1;
        private System.Windows.Forms.ToolStripMenuItem msViewToolbar;
        private System.Windows.Forms.ToolStripMenuItem msViewStatusBar;
        private System.Windows.Forms.ToolStripButton tsbRefresh;
        private System.Windows.Forms.Label _lblNote;
        private System.Windows.Forms.ToolStripButton tsbDelete;
        private System.Windows.Forms.ToolStripSeparator tsbSep1;
        private System.Windows.Forms.ToolStripMenuItem msFileDelete;
        private System.Windows.Forms.ContextMenuStrip csMain;
        private System.Windows.Forms.ToolStripMenuItem csNew;
        private System.Windows.Forms.ToolStripMenuItem csDelete;
        private System.Windows.Forms.ToolStripMenuItem msViewFont;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    }
}

