namespace Argix.Customers {
    partial class dlgAction {
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
            this.mActionsDS = new Argix.CRMDataset();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.pnlDialog = new System.Windows.Forms.Panel();
            this.splitterH = new System.Windows.Forms.Splitter();
            this.txtComment = new System.Windows.Forms.TextBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.cboActionType = new System.Windows.Forms.ComboBox();
            this._lblType = new System.Windows.Forms.Label();
            this._lblComment = new System.Windows.Forms.Label();
            this.btnSpellCheck = new System.Windows.Forms.Button();
            this.lblSpellCheck = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mActionsDS)).BeginInit();
            this.pnlDialog.SuspendLayout();
            this.SuspendLayout();
            // 
            // mActionsDS
            // 
            this.mActionsDS.DataSetName = "CRMDataset";
            this.mActionsDS.Locale = new System.Globalization.CultureInfo("");
            this.mActionsDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(490, 280);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(82, 24);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(404, 280);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(82, 24);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "&OK";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
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
            this.pnlDialog.Location = new System.Drawing.Point(75, 39);
            this.pnlDialog.Name = "pnlDialog";
            this.pnlDialog.Size = new System.Drawing.Size(497, 235);
            this.pnlDialog.TabIndex = 43;
            // 
            // splitterH
            // 
            this.splitterH.Cursor = System.Windows.Forms.Cursors.HSplit;
            this.splitterH.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.splitterH.Location = new System.Drawing.Point(0, 134);
            this.splitterH.Name = "splitterH";
            this.splitterH.Size = new System.Drawing.Size(493, 3);
            this.splitterH.TabIndex = 38;
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
            this.txtComment.Size = new System.Drawing.Size(493, 137);
            this.txtComment.TabIndex = 2;
            this.txtComment.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtComments
            // 
            this.txtComments.BackColor = System.Drawing.SystemColors.Window;
            this.txtComments.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtComments.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtComments.ForeColor = System.Drawing.Color.Black;
            this.txtComments.Location = new System.Drawing.Point(0, 137);
            this.txtComments.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.ReadOnly = true;
            this.txtComments.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtComments.Size = new System.Drawing.Size(493, 94);
            this.txtComments.TabIndex = 37;
            // 
            // chkShowAll
            // 
            this.chkShowAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.BackColor = System.Drawing.SystemColors.Control;
            this.chkShowAll.Location = new System.Drawing.Point(455, 18);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkShowAll.Size = new System.Drawing.Size(113, 17);
            this.chkShowAll.TabIndex = 42;
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
            this.cboActionType.Location = new System.Drawing.Point(75, 9);
            this.cboActionType.Name = "cboActionType";
            this.cboActionType.Size = new System.Drawing.Size(148, 21);
            this.cboActionType.TabIndex = 39;
            this.cboActionType.ValueMember = "ActionTypeTable.ID";
            this.cboActionType.SelectionChangeCommitted += new System.EventHandler(this.OnActionTypeSelected);
            // 
            // _lblType
            // 
            this._lblType.Location = new System.Drawing.Point(5, 9);
            this._lblType.Name = "_lblType";
            this._lblType.Size = new System.Drawing.Size(62, 18);
            this._lblType.TabIndex = 41;
            this._lblType.Text = "Type";
            this._lblType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComment
            // 
            this._lblComment.Location = new System.Drawing.Point(5, 39);
            this._lblComment.Name = "_lblComment";
            this._lblComment.Size = new System.Drawing.Size(67, 18);
            this._lblComment.TabIndex = 40;
            this._lblComment.Text = "Comments";
            this._lblComment.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSpellCheck
            // 
            this.btnSpellCheck.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSpellCheck.Location = new System.Drawing.Point(75, 280);
            this.btnSpellCheck.Name = "btnSpellCheck";
            this.btnSpellCheck.Size = new System.Drawing.Size(82, 24);
            this.btnSpellCheck.TabIndex = 44;
            this.btnSpellCheck.Text = "Spell Check";
            this.btnSpellCheck.UseVisualStyleBackColor = true;
            this.btnSpellCheck.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // lblSpellCheck
            // 
            this.lblSpellCheck.Location = new System.Drawing.Point(162, 282);
            this.lblSpellCheck.Name = "lblSpellCheck";
            this.lblSpellCheck.Size = new System.Drawing.Size(219, 18);
            this.lblSpellCheck.TabIndex = 40;
            this.lblSpellCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dlgAction
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(578, 309);
            this.Controls.Add(this.btnSpellCheck);
            this.Controls.Add(this.pnlDialog);
            this.Controls.Add(this.chkShowAll);
            this.Controls.Add(this.cboActionType);
            this.Controls.Add(this._lblType);
            this.Controls.Add(this.lblSpellCheck);
            this.Controls.Add(this._lblComment);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.MinimizeBox = false;
            this.Name = "dlgAction";
            this.Padding = new System.Windows.Forms.Padding(3);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Action";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mActionsDS)).EndInit();
            this.pnlDialog.ResumeLayout(false);
            this.pnlDialog.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private CRMDataset mActionsDS;
        private System.Windows.Forms.Panel pnlDialog;
        private System.Windows.Forms.Splitter splitterH;
        private System.Windows.Forms.TextBox txtComment;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.CheckBox chkShowAll;
        private System.Windows.Forms.ComboBox cboActionType;
        private System.Windows.Forms.Label _lblType;
        private System.Windows.Forms.Label _lblComment;
        private System.Windows.Forms.Button btnSpellCheck;
        private System.Windows.Forms.Label lblSpellCheck;

    }
}