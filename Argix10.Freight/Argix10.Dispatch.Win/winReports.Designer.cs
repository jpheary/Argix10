namespace Argix.Freight {
    partial class winReports {
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
            this.rvReports = new Microsoft.Reporting.WinForms.ReportViewer();
            this.cboReports = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // rvReports
            // 
            this.rvReports.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rvReports.Location = new System.Drawing.Point(0, 21);
            this.rvReports.Name = "rvReports";
            this.rvReports.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rvReports.ServerReport.ReportPath = "/Freight/Advanced Pickup Requests";
            this.rvReports.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/reportserver", System.UriKind.Absolute);
            this.rvReports.ShowBackButton = false;
            this.rvReports.ShowCredentialPrompts = false;
            this.rvReports.ShowDocumentMapButton = false;
            this.rvReports.ShowFindControls = false;
            this.rvReports.ShowParameterPrompts = false;
            this.rvReports.Size = new System.Drawing.Size(734, 213);
            this.rvReports.TabIndex = 2;
            // 
            // cboReports
            // 
            this.cboReports.Dock = System.Windows.Forms.DockStyle.Top;
            this.cboReports.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReports.FormattingEnabled = true;
            this.cboReports.Items.AddRange(new object[] {
            "Advanced Pickup Requests",
            "Delivery Appointment Sheet",
            "Driver Settlement Sheet",
            "PCS Inbound Schedule (Active)",
            "PCS Pickup Log (Active)",
            "PCS Trailer Log Inbound",
            "PCS Trailer Log Outbound"});
            this.cboReports.Location = new System.Drawing.Point(0, 0);
            this.cboReports.Name = "cboReports";
            this.cboReports.Size = new System.Drawing.Size(734, 21);
            this.cboReports.TabIndex = 3;
            this.cboReports.SelectionChangeCommitted += new System.EventHandler(this.OnReportChanged);
            // 
            // winReports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 234);
            this.Controls.Add(this.rvReports);
            this.Controls.Add(this.cboReports);
            this.Name = "winReports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Reports";
            this.Activated += new System.EventHandler(this.OnFormActivated);
            this.Deactivate += new System.EventHandler(this.OnFormDeactivate);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.FontChanged += new System.EventHandler(this.OnFormFontChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rvReports;
        private System.Windows.Forms.ComboBox cboReports;
    }
}