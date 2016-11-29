namespace Argix.Customers {
    partial class AgentDashboardControl {
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
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.mTerminals = new Argix.CRMDataset();
            this.scAgent = new System.Windows.Forms.SplitContainer();
            this.rv1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rv2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.txtTermainlDetail = new System.Windows.Forms.TextBox();
            this.txtTerminalName = new System.Windows.Forms.TextBox();
            this.wbTerminalMap = new System.Windows.Forms.WebBrowser();
            this._lblTerminalDetail = new System.Windows.Forms.Label();
            this.txtTerminalAddress = new System.Windows.Forms.TextBox();
            this._lblAgent = new System.Windows.Forms.Label();
            this._lblAgentParent = new System.Windows.Forms.Label();
            this.cboAgent = new System.Windows.Forms.ComboBox();
            this.mAgentDS = new Argix.CRMDataset();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this._lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this._lblFrom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.scAgent)).BeginInit();
            this.scAgent.Panel1.SuspendLayout();
            this.scAgent.Panel2.SuspendLayout();
            this.scAgent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mAgentDS)).BeginInit();
            this.SuspendLayout();
            // 
            // cboTerminal
            // 
            this.cboTerminal.DataSource = this.mTerminals;
            this.cboTerminal.DisplayMember = "AgentTable.AgentName";
            this.cboTerminal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTerminal.FormattingEnabled = true;
            this.cboTerminal.Location = new System.Drawing.Point(84, 32);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(288, 21);
            this.cboTerminal.TabIndex = 53;
            this.cboTerminal.ValueMember = "AgentTable.AgentNumber";
            this.cboTerminal.SelectionChangeCommitted += new System.EventHandler(this.OnAgentTerminalChanged);
            // 
            // mTerminals
            // 
            this.mTerminals.DataSetName = "EnterpriseDataset";
            this.mTerminals.Locale = new System.Globalization.CultureInfo("");
            this.mTerminals.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // scAgent
            // 
            this.scAgent.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scAgent.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scAgent.Location = new System.Drawing.Point(390, 58);
            this.scAgent.Name = "scAgent";
            this.scAgent.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scAgent.Panel1
            // 
            this.scAgent.Panel1.Controls.Add(this.rv1);
            // 
            // scAgent.Panel2
            // 
            this.scAgent.Panel2.Controls.Add(this.rv2);
            this.scAgent.Size = new System.Drawing.Size(396, 387);
            this.scAgent.SplitterDistance = 250;
            this.scAgent.TabIndex = 52;
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
            this.rv1.ShowPrintButton = false;
            this.rv1.ShowProgress = false;
            this.rv1.ShowPromptAreaButton = false;
            this.rv1.ShowStopButton = false;
            this.rv1.Size = new System.Drawing.Size(396, 250);
            this.rv1.TabIndex = 29;
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
            this.rv2.ShowPrintButton = false;
            this.rv2.ShowProgress = false;
            this.rv2.ShowPromptAreaButton = false;
            this.rv2.ShowStopButton = false;
            this.rv2.Size = new System.Drawing.Size(396, 133);
            this.rv2.TabIndex = 29;
            // 
            // txtTermainlDetail
            // 
            this.txtTermainlDetail.BackColor = System.Drawing.SystemColors.Window;
            this.txtTermainlDetail.Location = new System.Drawing.Point(84, 133);
            this.txtTermainlDetail.Multiline = true;
            this.txtTermainlDetail.Name = "txtTermainlDetail";
            this.txtTermainlDetail.ReadOnly = true;
            this.txtTermainlDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtTermainlDetail.Size = new System.Drawing.Size(288, 97);
            this.txtTermainlDetail.TabIndex = 51;
            this.txtTermainlDetail.TabStop = false;
            this.txtTermainlDetail.WordWrap = false;
            // 
            // txtTerminalName
            // 
            this.txtTerminalName.Location = new System.Drawing.Point(84, 58);
            this.txtTerminalName.Name = "txtTerminalName";
            this.txtTerminalName.Size = new System.Drawing.Size(288, 20);
            this.txtTerminalName.TabIndex = 50;
            // 
            // wbTerminalMap
            // 
            this.wbTerminalMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.wbTerminalMap.Location = new System.Drawing.Point(84, 237);
            this.wbTerminalMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbTerminalMap.Name = "wbTerminalMap";
            this.wbTerminalMap.ScriptErrorsSuppressed = true;
            this.wbTerminalMap.ScrollBarsEnabled = false;
            this.wbTerminalMap.Size = new System.Drawing.Size(288, 208);
            this.wbTerminalMap.TabIndex = 49;
            this.wbTerminalMap.Url = new System.Uri("http://localhost:53791/Argix10.Enterprise.Services/Map.aspx", System.UriKind.Absolute);
            this.wbTerminalMap.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnTerminalDocumentCompleted);
            // 
            // _lblTerminalDetail
            // 
            this._lblTerminalDetail.Location = new System.Drawing.Point(5, 58);
            this._lblTerminalDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblTerminalDetail.Name = "_lblTerminalDetail";
            this._lblTerminalDetail.Size = new System.Drawing.Size(72, 18);
            this._lblTerminalDetail.TabIndex = 48;
            this._lblTerminalDetail.Text = "Detail";
            this._lblTerminalDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTerminalAddress
            // 
            this.txtTerminalAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtTerminalAddress.HideSelection = false;
            this.txtTerminalAddress.Location = new System.Drawing.Point(84, 84);
            this.txtTerminalAddress.Multiline = true;
            this.txtTerminalAddress.Name = "txtTerminalAddress";
            this.txtTerminalAddress.ReadOnly = true;
            this.txtTerminalAddress.Size = new System.Drawing.Size(288, 50);
            this.txtTerminalAddress.TabIndex = 45;
            this.txtTerminalAddress.TabStop = false;
            this.txtTerminalAddress.WordWrap = false;
            this.txtTerminalAddress.TextChanged += new System.EventHandler(this.OnTerminalAddressChanged);
            // 
            // _lblAgent
            // 
            this._lblAgent.Location = new System.Drawing.Point(5, 31);
            this._lblAgent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblAgent.Name = "_lblAgent";
            this._lblAgent.Size = new System.Drawing.Size(72, 18);
            this._lblAgent.TabIndex = 47;
            this._lblAgent.Text = "Terminal";
            this._lblAgent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblAgentParent
            // 
            this._lblAgentParent.Location = new System.Drawing.Point(5, 5);
            this._lblAgentParent.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblAgentParent.Name = "_lblAgentParent";
            this._lblAgentParent.Size = new System.Drawing.Size(72, 18);
            this._lblAgentParent.TabIndex = 46;
            this._lblAgentParent.Text = "Agent";
            this._lblAgentParent.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboAgent
            // 
            this.cboAgent.DataSource = this.mAgentDS;
            this.cboAgent.DisplayMember = "AgentTable.AgentName";
            this.cboAgent.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboAgent.FormattingEnabled = true;
            this.cboAgent.Location = new System.Drawing.Point(84, 5);
            this.cboAgent.Name = "cboAgent";
            this.cboAgent.Size = new System.Drawing.Size(288, 21);
            this.cboAgent.TabIndex = 44;
            this.cboAgent.ValueMember = "AgentTable.AgentNumber";
            this.cboAgent.SelectionChangeCommitted += new System.EventHandler(this.OnAgentChanged);
            // 
            // mAgentDS
            // 
            this.mAgentDS.DataSetName = "EnterpriseDataset";
            this.mAgentDS.Locale = new System.Globalization.CultureInfo("");
            this.mAgentDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(468, 34);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 61;
            this.dtpTo.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpTo.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // _lblTo
            // 
            this._lblTo.Location = new System.Drawing.Point(389, 33);
            this._lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(72, 18);
            this._lblTo.TabIndex = 60;
            this._lblTo.Text = "To";
            this._lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(468, 6);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 59;
            this.dtpFrom.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpFrom.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // _lblFrom
            // 
            this._lblFrom.Location = new System.Drawing.Point(389, 6);
            this._lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(72, 18);
            this._lblFrom.TabIndex = 58;
            this._lblFrom.Text = "From";
            this._lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // AgentDashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this._lblTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this._lblFrom);
            this.Controls.Add(this.cboTerminal);
            this.Controls.Add(this.scAgent);
            this.Controls.Add(this.txtTermainlDetail);
            this.Controls.Add(this.txtTerminalName);
            this.Controls.Add(this.wbTerminalMap);
            this.Controls.Add(this._lblTerminalDetail);
            this.Controls.Add(this.txtTerminalAddress);
            this.Controls.Add(this._lblAgent);
            this.Controls.Add(this._lblAgentParent);
            this.Controls.Add(this.cboAgent);
            this.Name = "AgentDashboardControl";
            this.Size = new System.Drawing.Size(789, 448);
            this.Load += new System.EventHandler(this.OnControlLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mTerminals)).EndInit();
            this.scAgent.Panel1.ResumeLayout(false);
            this.scAgent.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scAgent)).EndInit();
            this.scAgent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mAgentDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cboTerminal;
        private System.Windows.Forms.SplitContainer scAgent;
        private Microsoft.Reporting.WinForms.ReportViewer rv1;
        private Microsoft.Reporting.WinForms.ReportViewer rv2;
        private System.Windows.Forms.TextBox txtTermainlDetail;
        private System.Windows.Forms.TextBox txtTerminalName;
        private System.Windows.Forms.WebBrowser wbTerminalMap;
        private System.Windows.Forms.Label _lblTerminalDetail;
        private System.Windows.Forms.TextBox txtTerminalAddress;
        private System.Windows.Forms.Label _lblAgent;
        private System.Windows.Forms.Label _lblAgentParent;
        private System.Windows.Forms.ComboBox cboAgent;
        private CRMDataset mAgentDS;
        private CRMDataset mTerminals;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label _lblTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label _lblFrom;
    }
}
