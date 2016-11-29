namespace Argix.Customers {
    partial class ManagerDashboardControl2 {
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.mAgentDS = new Argix.CRMDataset();
            this.mClientDS = new Argix.CRMDataset();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this._lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this._lblFrom = new System.Windows.Forms.Label();
            this._lblAgent = new System.Windows.Forms.Label();
            this.cboAgent = new System.Windows.Forms.ComboBox();
            this._lblClient = new System.Windows.Forms.Label();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.scControl = new System.Windows.Forms.SplitContainer();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.rv1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rv2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.rv3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rv4 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.mAgentDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClientDS)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scControl)).BeginInit();
            this.scControl.Panel1.SuspendLayout();
            this.scControl.Panel2.SuspendLayout();
            this.scControl.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // mAgentDS
            // 
            this.mAgentDS.DataSetName = "EnterpriseDataset";
            this.mAgentDS.Locale = new System.Globalization.CultureInfo("");
            this.mAgentDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // mClientDS
            // 
            this.mClientDS.DataSetName = "CRMDataset";
            this.mClientDS.Locale = new System.Globalization.CultureInfo("");
            this.mClientDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(84, 33);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 57;
            this.dtpTo.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpTo.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // _lblTo
            // 
            this._lblTo.Location = new System.Drawing.Point(5, 32);
            this._lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(72, 18);
            this._lblTo.TabIndex = 56;
            this._lblTo.Text = "To";
            this._lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(84, 5);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 55;
            this.dtpFrom.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpFrom.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // _lblFrom
            // 
            this._lblFrom.Location = new System.Drawing.Point(5, 5);
            this._lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(72, 18);
            this._lblFrom.TabIndex = 54;
            this._lblFrom.Text = "From";
            this._lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblAgent
            // 
            this._lblAgent.Location = new System.Drawing.Point(298, 33);
            this._lblAgent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblAgent.Name = "_lblAgent";
            this._lblAgent.Size = new System.Drawing.Size(72, 18);
            this._lblAgent.TabIndex = 52;
            this._lblAgent.Text = "Agent";
            this._lblAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboAgent
            // 
            this.cboAgent.DataSource = this.mAgentDS;
            this.cboAgent.DisplayMember = "AgentTable.AgentName";
            this.cboAgent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAgent.FormattingEnabled = true;
            this.cboAgent.Location = new System.Drawing.Point(377, 33);
            this.cboAgent.Name = "cboAgent";
            this.cboAgent.Size = new System.Drawing.Size(288, 21);
            this.cboAgent.TabIndex = 51;
            this.cboAgent.ValueMember = "AgentTable.AgentNumber";
            this.cboAgent.SelectionChangeCommitted += new System.EventHandler(this.OnAgentChanged);
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(298, 5);
            this._lblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(72, 18);
            this._lblClient.TabIndex = 50;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboClient
            // 
            this.cboClient.DataSource = this.mClientDS;
            this.cboClient.DisplayMember = "CompanyTable.CompanyName";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(377, 5);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(288, 21);
            this.cboClient.TabIndex = 49;
            this.cboClient.ValueMember = "CompanyTable.CompanyID";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientChanged);
            // 
            // scControl
            // 
            this.scControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scControl.Location = new System.Drawing.Point(84, 60);
            this.scControl.Name = "scControl";
            this.scControl.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scControl.Panel1
            // 
            this.scControl.Panel1.Controls.Add(this.splitContainer1);
            // 
            // scControl.Panel2
            // 
            this.scControl.Panel2.Controls.Add(this.splitContainer2);
            this.scControl.Size = new System.Drawing.Size(674, 269);
            this.scControl.SplitterDistance = 135;
            this.scControl.TabIndex = 58;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.rv1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.rv2);
            this.splitContainer1.Size = new System.Drawing.Size(674, 135);
            this.splitContainer1.SplitterDistance = 306;
            this.splitContainer1.TabIndex = 0;
            // 
            // rv1
            // 
            this.rv1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rv1.Location = new System.Drawing.Point(0, 0);
            this.rv1.Name = "rv1";
            this.rv1.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rv1.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rv1.ShowBackButton = false;
            this.rv1.ShowCredentialPrompts = false;
            this.rv1.ShowDocumentMapButton = false;
            this.rv1.ShowExportButton = false;
            this.rv1.ShowFindControls = false;
            this.rv1.ShowParameterPrompts = false;
            this.rv1.ShowProgress = false;
            this.rv1.ShowPromptAreaButton = false;
            this.rv1.ShowStopButton = false;
            this.rv1.Size = new System.Drawing.Size(306, 135);
            this.rv1.TabIndex = 30;
            this.rv1.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // rv2
            // 
            this.rv2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rv2.Location = new System.Drawing.Point(0, 0);
            this.rv2.Name = "rv2";
            this.rv2.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rv2.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rv2.ShowBackButton = false;
            this.rv2.ShowCredentialPrompts = false;
            this.rv2.ShowDocumentMapButton = false;
            this.rv2.ShowExportButton = false;
            this.rv2.ShowFindControls = false;
            this.rv2.ShowParameterPrompts = false;
            this.rv2.ShowProgress = false;
            this.rv2.ShowPromptAreaButton = false;
            this.rv2.ShowStopButton = false;
            this.rv2.Size = new System.Drawing.Size(364, 135);
            this.rv2.TabIndex = 59;
            this.rv2.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.rv3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.rv4);
            this.splitContainer2.Size = new System.Drawing.Size(674, 130);
            this.splitContainer2.SplitterDistance = 304;
            this.splitContainer2.TabIndex = 0;
            // 
            // rv3
            // 
            this.rv3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rv3.Location = new System.Drawing.Point(0, 0);
            this.rv3.Name = "rv3";
            this.rv3.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rv3.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rv3.ShowBackButton = false;
            this.rv3.ShowCredentialPrompts = false;
            this.rv3.ShowDocumentMapButton = false;
            this.rv3.ShowExportButton = false;
            this.rv3.ShowFindControls = false;
            this.rv3.ShowParameterPrompts = false;
            this.rv3.ShowProgress = false;
            this.rv3.ShowPromptAreaButton = false;
            this.rv3.ShowStopButton = false;
            this.rv3.Size = new System.Drawing.Size(304, 130);
            this.rv3.TabIndex = 59;
            this.rv3.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // rv4
            // 
            this.rv4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rv4.Location = new System.Drawing.Point(0, 0);
            this.rv4.Name = "rv4";
            this.rv4.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Remote;
            this.rv4.ServerReport.ReportServerUrl = new System.Uri("http://rgxvmsqlrpt08/ReportServer", System.UriKind.Absolute);
            this.rv4.ShowBackButton = false;
            this.rv4.ShowCredentialPrompts = false;
            this.rv4.ShowDocumentMapButton = false;
            this.rv4.ShowExportButton = false;
            this.rv4.ShowFindControls = false;
            this.rv4.ShowParameterPrompts = false;
            this.rv4.ShowProgress = false;
            this.rv4.ShowPromptAreaButton = false;
            this.rv4.ShowStopButton = false;
            this.rv4.Size = new System.Drawing.Size(366, 130);
            this.rv4.TabIndex = 59;
            this.rv4.ZoomMode = Microsoft.Reporting.WinForms.ZoomMode.PageWidth;
            // 
            // ManagerDashboardControl2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scControl);
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this._lblTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this._lblFrom);
            this.Controls.Add(this._lblAgent);
            this.Controls.Add(this.cboAgent);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.cboClient);
            this.Name = "ManagerDashboardControl2";
            this.Size = new System.Drawing.Size(761, 332);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mAgentDS)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClientDS)).EndInit();
            this.scControl.Panel1.ResumeLayout(false);
            this.scControl.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scControl)).EndInit();
            this.scControl.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CRMDataset mAgentDS;
        private CRMDataset mClientDS;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label _lblTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label _lblFrom;
        private System.Windows.Forms.Label _lblAgent;
        private System.Windows.Forms.ComboBox cboAgent;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.SplitContainer scControl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private Microsoft.Reporting.WinForms.ReportViewer rv1;
        private Microsoft.Reporting.WinForms.ReportViewer rv2;
        private Microsoft.Reporting.WinForms.ReportViewer rv3;
        private Microsoft.Reporting.WinForms.ReportViewer rv4;
    }
}
