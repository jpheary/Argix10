namespace Argix.Customers {
    partial class dlgIssue {
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
            this.cboIssueType = new System.Windows.Forms.ComboBox();
            this.mIssueTypes = new Argix.CRMDataset();
            this._lblType = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this._lblSubject = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cboIssueCategory = new System.Windows.Forms.ComboBox();
            this.mIssueCategorys = new Argix.CRMDataset();
            this._lblStoreDetail = new System.Windows.Forms.Label();
            this.txtStoreDetail = new System.Windows.Forms.TextBox();
            this._lblLocation = new System.Windows.Forms.Label();
            this.mCompanyDS = new Argix.CRMDataset();
            this.cboLocation = new System.Windows.Forms.ComboBox();
            this.txtStore = new System.Windows.Forms.MaskedTextBox();
            this.cboCompany = new System.Windows.Forms.ComboBox();
            this.cboScope = new System.Windows.Forms.ComboBox();
            this._lblCompany = new System.Windows.Forms.Label();
            this._lblScope = new System.Windows.Forms.Label();
            this._lblContact = new System.Windows.Forms.Label();
            this.txtContact = new System.Windows.Forms.TextBox();
            this.pnlDialog = new System.Windows.Forms.Panel();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.cboActionType = new System.Windows.Forms.ComboBox();
            this.mActionsDS = new Argix.CRMDataset();
            this.label1 = new System.Windows.Forms.Label();
            this._lblComment = new System.Windows.Forms.Label();
            this.btnSpellCheck = new System.Windows.Forms.Button();
            this.lblSpellCheck = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mIssueTypes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mIssueCategorys)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCompanyDS)).BeginInit();
            this.pnlDialog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mActionsDS)).BeginInit();
            this.SuspendLayout();
            // 
            // cboIssueType
            // 
            this.cboIssueType.DataSource = this.mIssueTypes;
            this.cboIssueType.DisplayMember = "IssueTypeTable.Type";
            this.cboIssueType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIssueType.FormattingEnabled = true;
            this.cboIssueType.Location = new System.Drawing.Point(184, 249);
            this.cboIssueType.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboIssueType.Name = "cboIssueType";
            this.cboIssueType.Size = new System.Drawing.Size(188, 21);
            this.cboIssueType.TabIndex = 5;
            this.cboIssueType.ValueMember = "IssueTypeTable.ID";
            this.cboIssueType.SelectionChangeCommitted += new System.EventHandler(this.OnIssueTypeSelected);
            // 
            // mIssueTypes
            // 
            this.mIssueTypes.DataSetName = "CRMDataset";
            this.mIssueTypes.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblType
            // 
            this._lblType.Location = new System.Drawing.Point(8, 249);
            this._lblType.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(72, 18);
            this._lblType.TabIndex = 4;
            this._lblType.Text = "Issue Type";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubject
            // 
            this.txtSubject.Location = new System.Drawing.Point(84, 309);
            this.txtSubject.Margin = new System.Windows.Forms.Padding(4);
            this.txtSubject.MaxLength = 50;
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(288, 20);
            this.txtSubject.TabIndex = 7;
            this.txtSubject.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblSubject
            // 
            this._lblSubject.Location = new System.Drawing.Point(22, 308);
            this._lblSubject.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblSubject.Name = "_lblSubject";
            this._lblSubject.Size = new System.Drawing.Size(58, 18);
            this._lblSubject.TabIndex = 2;
            this._lblSubject.Text = "Subject";
            this._lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(855, 343);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 24);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(769, 343);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 24);
            this.btnOk.TabIndex = 10;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // cboIssueCategory
            // 
            this.cboIssueCategory.DataSource = this.mIssueCategorys;
            this.cboIssueCategory.DisplayMember = "IssueTypeTable.Category";
            this.cboIssueCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboIssueCategory.FormattingEnabled = true;
            this.cboIssueCategory.Location = new System.Drawing.Point(84, 249);
            this.cboIssueCategory.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.cboIssueCategory.Name = "cboIssueCategory";
            this.cboIssueCategory.Size = new System.Drawing.Size(96, 21);
            this.cboIssueCategory.TabIndex = 4;
            this.cboIssueCategory.ValueMember = "IssueTypeTable.Category";
            this.cboIssueCategory.SelectionChangeCommitted += new System.EventHandler(this.OnIssueCategorySelected);
            // 
            // mIssueCategorys
            // 
            this.mIssueCategorys.DataSetName = "CRMDataset";
            this.mIssueCategorys.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblStoreDetail
            // 
            this._lblStoreDetail.Location = new System.Drawing.Point(5, 91);
            this._lblStoreDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblStoreDetail.Name = "_lblStoreDetail";
            this._lblStoreDetail.Size = new System.Drawing.Size(72, 18);
            this._lblStoreDetail.TabIndex = 21;
            this._lblStoreDetail.Text = "Store";
            this._lblStoreDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStoreDetail
            // 
            this.txtStoreDetail.BackColor = System.Drawing.SystemColors.Window;
            this.txtStoreDetail.HideSelection = false;
            this.txtStoreDetail.Location = new System.Drawing.Point(84, 91);
            this.txtStoreDetail.Multiline = true;
            this.txtStoreDetail.Name = "txtStoreDetail";
            this.txtStoreDetail.ReadOnly = true;
            this.txtStoreDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtStoreDetail.Size = new System.Drawing.Size(288, 144);
            this.txtStoreDetail.TabIndex = 3;
            this.txtStoreDetail.TabStop = false;
            this.txtStoreDetail.WordWrap = false;
            // 
            // _lblLocation
            // 
            this._lblLocation.Location = new System.Drawing.Point(5, 64);
            this._lblLocation.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblLocation.Name = "_lblLocation";
            this._lblLocation.Size = new System.Drawing.Size(72, 18);
            this._lblLocation.TabIndex = 19;
            this._lblLocation.Text = "#";
            this._lblLocation.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mCompanyDS
            // 
            this.mCompanyDS.DataSetName = "EnterpriseDataset";
            this.mCompanyDS.Locale = new System.Globalization.CultureInfo("");
            this.mCompanyDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboLocation
            // 
            this.cboLocation.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocation.FormattingEnabled = true;
            this.cboLocation.Location = new System.Drawing.Point(84, 64);
            this.cboLocation.Name = "cboLocation";
            this.cboLocation.Size = new System.Drawing.Size(293, 21);
            this.cboLocation.TabIndex = 3;
            this.cboLocation.SelectionChangeCommitted += new System.EventHandler(this.OnCompanyLocationChanged);
            // 
            // txtStore
            // 
            this.txtStore.AllowPromptAsInput = false;
            this.txtStore.HidePromptOnLeave = true;
            this.txtStore.Location = new System.Drawing.Point(84, 64);
            this.txtStore.Mask = "#########";
            this.txtStore.Name = "txtStore";
            this.txtStore.Size = new System.Drawing.Size(60, 20);
            this.txtStore.TabIndex = 2;
            this.txtStore.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.txtStore.Visible = false;
            this.txtStore.TextChanged += new System.EventHandler(this.OnCompanyLocationChanged);
            this.txtStore.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnStoreKeyUp);
            // 
            // cboCompany
            // 
            this.cboCompany.DataSource = this.mCompanyDS;
            this.cboCompany.DisplayMember = "CompanyTable.CompanyName";
            this.cboCompany.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCompany.FormattingEnabled = true;
            this.cboCompany.Location = new System.Drawing.Point(84, 10);
            this.cboCompany.Name = "cboCompany";
            this.cboCompany.Size = new System.Drawing.Size(288, 21);
            this.cboCompany.TabIndex = 0;
            this.cboCompany.ValueMember = "CompanyTable.CompanyID";
            this.cboCompany.SelectionChangeCommitted += new System.EventHandler(this.OnCompanySelected);
            // 
            // cboScope
            // 
            this.cboScope.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboScope.FormattingEnabled = true;
            this.cboScope.Items.AddRange(new object[] {
            "Districts",
            "Regions",
            "Stores",
            "Substores",
            "Agents"});
            this.cboScope.Location = new System.Drawing.Point(84, 37);
            this.cboScope.Name = "cboScope";
            this.cboScope.Size = new System.Drawing.Size(143, 21);
            this.cboScope.TabIndex = 1;
            this.cboScope.SelectionChangeCommitted += new System.EventHandler(this.OnScopeChanged);
            // 
            // _lblCompany
            // 
            this._lblCompany.Location = new System.Drawing.Point(5, 10);
            this._lblCompany.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblCompany.Name = "_lblCompany";
            this._lblCompany.Size = new System.Drawing.Size(72, 18);
            this._lblCompany.TabIndex = 17;
            this._lblCompany.Text = "Company";
            this._lblCompany.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblScope
            // 
            this._lblScope.Location = new System.Drawing.Point(5, 37);
            this._lblScope.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblScope.Name = "_lblScope";
            this._lblScope.Size = new System.Drawing.Size(72, 18);
            this._lblScope.TabIndex = 18;
            this._lblScope.Text = "Location";
            this._lblScope.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblContact
            // 
            this._lblContact.Location = new System.Drawing.Point(18, 283);
            this._lblContact.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblContact.Name = "_lblContact";
            this._lblContact.Size = new System.Drawing.Size(58, 18);
            this._lblContact.TabIndex = 22;
            this._lblContact.Text = "Contact";
            this._lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContact
            // 
            this.txtContact.Location = new System.Drawing.Point(84, 281);
            this.txtContact.Margin = new System.Windows.Forms.Padding(4);
            this.txtContact.MaxLength = 200;
            this.txtContact.Name = "txtContact";
            this.txtContact.Size = new System.Drawing.Size(192, 20);
            this.txtContact.TabIndex = 6;
            this.txtContact.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // pnlDialog
            // 
            this.pnlDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlDialog.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlDialog.Controls.Add(this.splitterH);
            this.pnlDialog.Controls.Add(this.txtComment);
            this.pnlDialog.Controls.Add(this.txtComments);
            this.pnlDialog.Location = new System.Drawing.Point(462, 37);
            this.pnlDialog.Name = "pnlDialog";
            this.pnlDialog.Size = new System.Drawing.Size(475, 292);
            this.pnlDialog.TabIndex = 9;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 137);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(471, 3);
            this.splitterH.TabIndex = 2;
            this.splitterH.TabStop = false;
            // 
            // txtComment
            // 
            this.txtComment.AcceptsReturn = true;
            this.txtComment.AcceptsTab = true;
            this.txtComment.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtComment.Location = new System.Drawing.Point(0, 0);
            this.txtComment.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComment.MaxLength = 2500;
            this.txtComment.Multiline = true;
            this.txtComment.Name = "txtComment";
            this.txtComment.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComment.Size = new System.Drawing.Size(471, 140);
            this.txtComment.TabIndex = 0;
            this.txtComment.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.SystemColors.Window;
            this.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtComments.ForeColor = System.Drawing.Color.Black;
            this.txtComments.Location = new System.Drawing.Point(0, 140);
            this.txtComments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComments.Size = new System.Drawing.Size(471, 148);
            this.txtComments.TabIndex = 0;
            this.txtComments.TabStop = false;
            // 
            // chkShowAll
            // 
            this.chkShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowAll.BackColor = System.Drawing.SystemColors.Control;
            this.chkShowAll.Location = new System.Drawing.Point(814, 15);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(121, 17);
            this.chkShowAll.TabIndex = 47;
            this.chkShowAll.Text = "Show prior actions";
            this.chkShowAll.UseVisualStyleBackColor = false;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.OnShowAllCheckedChanged);
            // 
            // cboActionType
            // 
            this.cboActionType.DataSource = this.mActionsDS;
            this.cboActionType.DisplayMember = "ActionTypeTable.Type";
            this.cboActionType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboActionType.FormattingEnabled = true;
            this.cboActionType.Location = new System.Drawing.Point(462, 10);
            this.cboActionType.Name = "cboActionType";
            this.cboActionType.Size = new System.Drawing.Size(124, 21);
            this.cboActionType.TabIndex = 8;
            this.cboActionType.ValueMember = "ActionTypeTable.ID";
            this.cboActionType.SelectionChangeCommitted += new System.EventHandler(this.OnActionTypeSelected);
            // 
            // mActionsDS
            // 
            this.mActionsDS.DataSetName = "CRMDataset";
            this.mActionsDS.Locale = new System.Globalization.CultureInfo("");
            this.mActionsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(374, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 18);
            this.label1.TabIndex = 46;
            this.label1.Text = "Action";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComment
            // 
            this._lblComment.Location = new System.Drawing.Point(388, 37);
            this._lblComment.Name = "_lblComment";
            this._lblComment.Size = new System.Drawing.Size(67, 18);
            this._lblComment.TabIndex = 45;
            this._lblComment.Text = "Comments";
            this._lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSpellCheck
            // 
            this.btnSpellCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpellCheck.Location = new System.Drawing.Point(464, 343);
            this.btnSpellCheck.Name = "btnSpellCheck";
            this.btnSpellCheck.Size = new System.Drawing.Size(82, 24);
            this.btnSpellCheck.TabIndex = 49;
            this.btnSpellCheck.Text = "Spell Check";
            this.btnSpellCheck.UseVisualStyleBackColor = true;
            this.btnSpellCheck.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // lblSpellCheck
            // 
            this.lblSpellCheck.Location = new System.Drawing.Point(551, 345);
            this.lblSpellCheck.Name = "lblSpellCheck";
            this.lblSpellCheck.Size = new System.Drawing.Size(219, 18);
            this.lblSpellCheck.TabIndex = 48;
            this.lblSpellCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dlgIssue
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 373);
            this.Controls.Add(this.btnSpellCheck);
            this.Controls.Add(this.lblSpellCheck);
            this.Controls.Add(this.pnlDialog);
            this.Controls.Add(this.chkShowAll);
            this.Controls.Add(this.cboActionType);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lblComment);
            this.Controls.Add(this.txtContact);
            this.Controls.Add(this._lblContact);
            this.Controls.Add(this._lblStoreDetail);
            this.Controls.Add(this.txtStoreDetail);
            this.Controls.Add(this._lblLocation);
            this.Controls.Add(this.cboLocation);
            this.Controls.Add(this.txtStore);
            this.Controls.Add(this.cboCompany);
            this.Controls.Add(this.cboScope);
            this.Controls.Add(this._lblCompany);
            this.Controls.Add(this._lblScope);
            this.Controls.Add(this.cboIssueCategory);
            this.Controls.Add(this.cboIssueType);
            this.Controls.Add(this._lblType);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this._lblSubject);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgIssue";
            this.Padding = new System.Windows.Forms.Padding(2, 3, 2, 3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Issue";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mIssueTypes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mIssueCategorys)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCompanyDS)).EndInit();
            this.pnlDialog.ResumeLayout(false);
            this.pnlDialog.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mActionsDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboIssueType;
        private System.Windows.Forms.Label _lblType;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label _lblSubject;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cboIssueCategory;
        private System.Windows.Forms.Label _lblStoreDetail;
        private System.Windows.Forms.TextBox txtStoreDetail;
        private System.Windows.Forms.Label _lblLocation;
        private CRMDataset mCompanyDS;
        private System.Windows.Forms.ComboBox cboLocation;
        private System.Windows.Forms.MaskedTextBox txtStore;
        private System.Windows.Forms.ComboBox cboCompany;
        private System.Windows.Forms.ComboBox cboScope;
        private System.Windows.Forms.Label _lblCompany;
        private System.Windows.Forms.Label _lblScope;
        private System.Windows.Forms.Label _lblContact;
        private System.Windows.Forms.TextBox txtContact;
        private System.Windows.Forms.Panel pnlDialog;
        private System.Windows.Forms.Splitter splitterH;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.CheckBox chkShowAll;
        private System.Windows.Forms.ComboBox cboActionType;
        private CRMDataset mActionsDS;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _lblComment;
        private CRMDataset mIssueCategorys;
        private CRMDataset mIssueTypes;
        private System.Windows.Forms.Button btnSpellCheck;
        private System.Windows.Forms.Label lblSpellCheck;

    }
}