namespace Argix {
    partial class dlgTrackingDetail {
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
            this.rsDetail = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rsDetail
            // 
            this.rsDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsDetail.Location = new System.Drawing.Point(0, 0);
            this.rsDetail.Name = "rsDetail";
            this.rsDetail.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsDetail.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rsDetail.ShowBackButton = false;
            this.rsDetail.ShowCredentialPrompts = false;
            this.rsDetail.ShowDocumentMapButton = false;
            this.rsDetail.ShowFindControls = false;
            this.rsDetail.ShowPageNavigationControls = false;
            this.rsDetail.ShowParameterPrompts = false;
            this.rsDetail.ShowPromptAreaButton = false;
            this.rsDetail.ShowStopButton = false;
            this.rsDetail.ShowZoomControl = false;
            this.rsDetail.Size = new System.Drawing.Size(584, 366);
            this.rsDetail.TabIndex = 1;
            // 
            // dlgTrackingDetail
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 366);
            this.Controls.Add(this.rsDetail);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "dlgTrackingDetail";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tracking Detail";
            this.Load += new System.EventHandler(this.OnLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rsDetail;
    }
}