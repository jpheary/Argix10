namespace Argix.Freight {
    partial class dlgPaperwork {
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
            this.rsDialog = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // rsDialog
            // 
            this.rsDialog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rsDialog.Location = new System.Drawing.Point(0, 0);
            this.rsDialog.Name = "rsDialog";
            this.rsDialog.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rsDialog.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rsDialog.ShowBackButton = false;
            this.rsDialog.ShowCredentialPrompts = false;
            this.rsDialog.ShowDocumentMapButton = false;
            this.rsDialog.ShowFindControls = false;
            this.rsDialog.ShowParameterPrompts = false;
            this.rsDialog.ShowPromptAreaButton = false;
            this.rsDialog.ShowStopButton = false;
            this.rsDialog.ShowZoomControl = false;
            this.rsDialog.Size = new System.Drawing.Size(684, 466);
            this.rsDialog.TabIndex = 4;
            // 
            // dlgPaperwork
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 466);
            this.Controls.Add(this.rsDialog);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "dlgPaperwork";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pallet Paperwork";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rsDialog;
    }
}