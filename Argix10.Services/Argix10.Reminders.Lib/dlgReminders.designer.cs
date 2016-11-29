namespace Argix {
    partial class dlgReminders {
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
            this.btnDismiss = new System.Windows.Forms.Button();
            this.btnSnooze = new System.Windows.Forms.Button();
            this.mOpenReminders = new Argix.RemindersDataset();
            this.btnOpenItem = new System.Windows.Forms.Button();
            this.btnDismissAll = new System.Windows.Forms.Button();
            this.lblSnooze = new System.Windows.Forms.Label();
            this.cboSnooze = new System.Windows.Forms.ComboBox();
            this.dgvReminders = new System.Windows.Forms.DataGridView();
            this.issueIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.subjectDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.userIDDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.timeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.messageDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenReminders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReminders)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDismiss
            // 
            this.btnDismiss.Location = new System.Drawing.Point(377, 158);
            this.btnDismiss.Name = "btnDismiss";
            this.btnDismiss.Size = new System.Drawing.Size(75, 23);
            this.btnDismiss.TabIndex = 6;
            this.btnDismiss.Text = "Dismiss";
            this.btnDismiss.UseVisualStyleBackColor = true;
            this.btnDismiss.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnSnooze
            // 
            this.btnSnooze.Location = new System.Drawing.Point(377, 219);
            this.btnSnooze.Name = "btnSnooze";
            this.btnSnooze.Size = new System.Drawing.Size(75, 23);
            this.btnSnooze.TabIndex = 5;
            this.btnSnooze.Text = "Snooze";
            this.btnSnooze.UseVisualStyleBackColor = true;
            this.btnSnooze.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // mOpenReminders
            // 
            this.mOpenReminders.DataSetName = "RemindersDataset";
            this.mOpenReminders.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // btnOpenItem
            // 
            this.btnOpenItem.Location = new System.Drawing.Point(296, 158);
            this.btnOpenItem.Name = "btnOpenItem";
            this.btnOpenItem.Size = new System.Drawing.Size(75, 23);
            this.btnOpenItem.TabIndex = 133;
            this.btnOpenItem.Text = "Open Item";
            this.btnOpenItem.UseVisualStyleBackColor = true;
            this.btnOpenItem.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnDismissAll
            // 
            this.btnDismissAll.Location = new System.Drawing.Point(8, 158);
            this.btnDismissAll.Name = "btnDismissAll";
            this.btnDismissAll.Size = new System.Drawing.Size(75, 23);
            this.btnDismissAll.TabIndex = 134;
            this.btnDismissAll.Text = "Dismiss All";
            this.btnDismissAll.UseVisualStyleBackColor = true;
            this.btnDismissAll.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // lblSnooze
            // 
            this.lblSnooze.AutoSize = true;
            this.lblSnooze.Location = new System.Drawing.Point(5, 199);
            this.lblSnooze.Name = "lblSnooze";
            this.lblSnooze.Size = new System.Drawing.Size(185, 13);
            this.lblSnooze.TabIndex = 135;
            this.lblSnooze.Text = "Click Snooze to be reminded again in:";
            // 
            // cboSnooze
            // 
            this.cboSnooze.FormattingEnabled = true;
            this.cboSnooze.Items.AddRange(new object[] {
            "5 minutes",
            "10 minutes",
            "15 minutes",
            "30 minutes",
            "1 hour",
            "2 hours",
            "4 hours",
            "8 hours",
            "0.5 days",
            "1 day",
            "2 days",
            "3 days",
            "4 days",
            "1 week",
            "2 weeks"});
            this.cboSnooze.Location = new System.Drawing.Point(8, 221);
            this.cboSnooze.Name = "cboSnooze";
            this.cboSnooze.Size = new System.Drawing.Size(363, 21);
            this.cboSnooze.TabIndex = 136;
            this.cboSnooze.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // dgvReminders
            // 
            this.dgvReminders.AllowUserToAddRows = false;
            this.dgvReminders.AllowUserToDeleteRows = false;
            this.dgvReminders.AutoGenerateColumns = false;
            this.dgvReminders.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvReminders.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvReminders.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.issueIDDataGridViewTextBoxColumn,
            this.subjectDataGridViewTextBoxColumn,
            this.userIDDataGridViewTextBoxColumn,
            this.timeDataGridViewTextBoxColumn,
            this.messageDataGridViewTextBoxColumn});
            this.dgvReminders.DataMember = "ReminderTable";
            this.dgvReminders.DataSource = this.mOpenReminders;
            this.dgvReminders.Location = new System.Drawing.Point(8, 6);
            this.dgvReminders.MultiSelect = false;
            this.dgvReminders.Name = "dgvReminders";
            this.dgvReminders.ReadOnly = true;
            this.dgvReminders.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvReminders.Size = new System.Drawing.Size(444, 144);
            this.dgvReminders.TabIndex = 137;
            this.dgvReminders.SelectionChanged += new System.EventHandler(this.OnReminderSelected);
            // 
            // issueIDDataGridViewTextBoxColumn
            // 
            this.issueIDDataGridViewTextBoxColumn.DataPropertyName = "IssueID";
            this.issueIDDataGridViewTextBoxColumn.HeaderText = "IssueID";
            this.issueIDDataGridViewTextBoxColumn.Name = "issueIDDataGridViewTextBoxColumn";
            this.issueIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.issueIDDataGridViewTextBoxColumn.Width = 5;
            // 
            // subjectDataGridViewTextBoxColumn
            // 
            this.subjectDataGridViewTextBoxColumn.DataPropertyName = "Subject";
            this.subjectDataGridViewTextBoxColumn.HeaderText = "Subject";
            this.subjectDataGridViewTextBoxColumn.Name = "subjectDataGridViewTextBoxColumn";
            this.subjectDataGridViewTextBoxColumn.ReadOnly = true;
            this.subjectDataGridViewTextBoxColumn.Width = 200;
            // 
            // userIDDataGridViewTextBoxColumn
            // 
            this.userIDDataGridViewTextBoxColumn.DataPropertyName = "UserID";
            this.userIDDataGridViewTextBoxColumn.HeaderText = "UserID";
            this.userIDDataGridViewTextBoxColumn.Name = "userIDDataGridViewTextBoxColumn";
            this.userIDDataGridViewTextBoxColumn.ReadOnly = true;
            this.userIDDataGridViewTextBoxColumn.Visible = false;
            // 
            // timeDataGridViewTextBoxColumn
            // 
            this.timeDataGridViewTextBoxColumn.DataPropertyName = "Time";
            this.timeDataGridViewTextBoxColumn.HeaderText = "Time";
            this.timeDataGridViewTextBoxColumn.Name = "timeDataGridViewTextBoxColumn";
            this.timeDataGridViewTextBoxColumn.ReadOnly = true;
            this.timeDataGridViewTextBoxColumn.Visible = false;
            // 
            // messageDataGridViewTextBoxColumn
            // 
            this.messageDataGridViewTextBoxColumn.DataPropertyName = "Message";
            this.messageDataGridViewTextBoxColumn.HeaderText = "Message";
            this.messageDataGridViewTextBoxColumn.Name = "messageDataGridViewTextBoxColumn";
            this.messageDataGridViewTextBoxColumn.ReadOnly = true;
            this.messageDataGridViewTextBoxColumn.Width = 250;
            // 
            // dlgReminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 250);
            this.Controls.Add(this.dgvReminders);
            this.Controls.Add(this.cboSnooze);
            this.Controls.Add(this.lblSnooze);
            this.Controls.Add(this.btnDismissAll);
            this.Controls.Add(this.btnOpenItem);
            this.Controls.Add(this.btnDismiss);
            this.Controls.Add(this.btnSnooze);
            this.MaximizeBox = false;
            this.Name = "dlgReminders";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Argix Reminders";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mOpenReminders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReminders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDismiss;
        private System.Windows.Forms.Button btnSnooze;
        private System.Windows.Forms.Button btnOpenItem;
        private System.Windows.Forms.Button btnDismissAll;
        private System.Windows.Forms.Label lblSnooze;
        private System.Windows.Forms.ComboBox cboSnooze;
        private RemindersDataset mOpenReminders;
        private System.Windows.Forms.DataGridView dgvReminders;
        private System.Windows.Forms.DataGridViewTextBoxColumn issueIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn subjectDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn userIDDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn timeDataGridViewTextBoxColumn;
        private System.Windows.Forms.DataGridViewTextBoxColumn messageDataGridViewTextBoxColumn;
    }
}