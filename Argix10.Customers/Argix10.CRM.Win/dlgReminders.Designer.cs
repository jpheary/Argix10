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
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("ReminderTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn1 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("IssueID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn2 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Subject");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn3 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("UserID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn4 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Time");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn5 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Message");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this.btnDismiss = new System.Windows.Forms.Button();
            this.btnSnooze = new System.Windows.Forms.Button();
            this.grdReminders = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.mOpenReminders = new Argix.CRMDataset();
            this.btnOpenItem = new System.Windows.Forms.Button();
            this.btnDismissAll = new System.Windows.Forms.Button();
            this.lblSnooze = new System.Windows.Forms.Label();
            this.cboSnooze = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdReminders)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenReminders)).BeginInit();
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
            // grdReminders
            // 
            this.grdReminders.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdReminders.DataMember = "ReminderTable";
            this.grdReminders.DataSource = this.mOpenReminders;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdReminders.DisplayLayout.Appearance = appearance1;
            ultraGridColumn1.Header.Caption = "Issue";
            ultraGridColumn1.Header.VisiblePosition = 0;
            ultraGridColumn1.Hidden = true;
            ultraGridColumn1.Width = 72;
            ultraGridColumn2.Header.VisiblePosition = 1;
            ultraGridColumn2.Width = 192;
            ultraGridColumn3.Header.VisiblePosition = 2;
            ultraGridColumn3.Hidden = true;
            ultraGridColumn4.Format = "MM-dd-yyyy HH:mm tt";
            ultraGridColumn4.Header.VisiblePosition = 4;
            ultraGridColumn4.Hidden = true;
            ultraGridColumn4.Width = 144;
            ultraGridColumn5.Header.VisiblePosition = 3;
            ultraGridColumn5.Width = 240;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn1,
            ultraGridColumn2,
            ultraGridColumn3,
            ultraGridColumn4,
            ultraGridColumn5});
            this.grdReminders.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdReminders.DisplayLayout.CaptionAppearance = appearance2;
            this.grdReminders.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdReminders.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdReminders.DisplayLayout.Override.AllowRowFiltering = Infragistics.Win.DefaultableBoolean.False;
            this.grdReminders.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdReminders.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.TextHAlignAsString = "Left";
            this.grdReminders.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdReminders.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdReminders.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdReminders.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdReminders.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdReminders.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdReminders.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdReminders.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdReminders.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdReminders.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdReminders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grdReminders.Location = new System.Drawing.Point(8, 8);
            this.grdReminders.Name = "grdReminders";
            this.grdReminders.Size = new System.Drawing.Size(444, 144);
            this.grdReminders.TabIndex = 132;
            this.grdReminders.UseAppStyling = false;
            this.grdReminders.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            this.grdReminders.AfterSelectChange += new Infragistics.Win.UltraWinGrid.AfterSelectChangeEventHandler(this.OnGridAfterSelectChange);
            // 
            // mOpenReminders
            // 
            this.mOpenReminders.DataSetName = "CRMDataset";
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
            // dlgReminders
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(464, 250);
            this.Controls.Add(this.cboSnooze);
            this.Controls.Add(this.lblSnooze);
            this.Controls.Add(this.btnDismissAll);
            this.Controls.Add(this.btnOpenItem);
            this.Controls.Add(this.grdReminders);
            this.Controls.Add(this.btnDismiss);
            this.Controls.Add(this.btnSnooze);
            this.MaximizeBox = false;
            this.Name = "dlgReminders";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Argix Logistics CRM Reminders";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdReminders)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOpenReminders)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDismiss;
        private System.Windows.Forms.Button btnSnooze;
        private Infragistics.Win.UltraWinGrid.UltraGrid grdReminders;
        private System.Windows.Forms.Button btnOpenItem;
        private System.Windows.Forms.Button btnDismissAll;
        private System.Windows.Forms.Label lblSnooze;
        private System.Windows.Forms.ComboBox cboSnooze;
        private CRMDataset mOpenReminders;
    }
}