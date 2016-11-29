namespace Argix.Customers {
    partial class ClientDashboardControl {
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
            this.scClient = new System.Windows.Forms.SplitContainer();
            this.rv1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.rv2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.txtStoreDetail = new System.Windows.Forms.TextBox();
            this.txtStoreName = new System.Windows.Forms.TextBox();
            this.wbStoreMap = new System.Windows.Forms.WebBrowser();
            this._lblStoreDetail = new System.Windows.Forms.Label();
            this.txtStoreAddress = new System.Windows.Forms.TextBox();
            this._lblStore = new System.Windows.Forms.Label();
            this.mskStore = new System.Windows.Forms.MaskedTextBox();
            this._lblClient = new System.Windows.Forms.Label();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.mClientDS = new Argix.CRMDataset();
            this.dtpTo = new System.Windows.Forms.DateTimePicker();
            this._lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new System.Windows.Forms.DateTimePicker();
            this._lblFrom = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.scClient)).BeginInit();
            this.scClient.Panel1.SuspendLayout();
            this.scClient.Panel2.SuspendLayout();
            this.scClient.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mClientDS)).BeginInit();
            this.SuspendLayout();
            // 
            // scClient
            // 
            this.scClient.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.scClient.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.scClient.Location = new System.Drawing.Point(390, 58);
            this.scClient.Name = "scClient";
            this.scClient.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scClient.Panel1
            // 
            this.scClient.Panel1.Controls.Add(this.rv1);
            // 
            // scClient.Panel2
            // 
            this.scClient.Panel2.Controls.Add(this.rv2);
            this.scClient.Size = new System.Drawing.Size(421, 435);
            this.scClient.SplitterDistance = 175;
            this.scClient.TabIndex = 42;
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
            this.rv1.Size = new System.Drawing.Size(421, 175);
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
            this.rv2.Size = new System.Drawing.Size(421, 256);
            this.rv2.TabIndex = 29;
            // 
            // txtStoreDetail
            // 
            this.txtStoreDetail.BackColor = System.Drawing.SystemColors.Window;
            this.txtStoreDetail.Location = new System.Drawing.Point(84, 133);
            this.txtStoreDetail.Multiline = true;
            this.txtStoreDetail.Name = "txtStoreDetail";
            this.txtStoreDetail.ReadOnly = true;
            this.txtStoreDetail.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.txtStoreDetail.Size = new System.Drawing.Size(288, 97);
            this.txtStoreDetail.TabIndex = 41;
            this.txtStoreDetail.TabStop = false;
            this.txtStoreDetail.WordWrap = false;
            // 
            // txtStoreName
            // 
            this.txtStoreName.Location = new System.Drawing.Point(84, 58);
            this.txtStoreName.Name = "txtStoreName";
            this.txtStoreName.Size = new System.Drawing.Size(288, 20);
            this.txtStoreName.TabIndex = 40;
            // 
            // wbStoreMap
            // 
            this.wbStoreMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.wbStoreMap.Location = new System.Drawing.Point(84, 237);
            this.wbStoreMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbStoreMap.Name = "wbStoreMap";
            this.wbStoreMap.ScriptErrorsSuppressed = true;
            this.wbStoreMap.ScrollBarsEnabled = false;
            this.wbStoreMap.Size = new System.Drawing.Size(288, 256);
            this.wbStoreMap.TabIndex = 39;
            this.wbStoreMap.Url = new System.Uri("http://localhost:53791/Argix10.Enterprise.Services/Map.aspx", System.UriKind.Absolute);
            this.wbStoreMap.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnStoreDocumentCompleted);
            // 
            // _lblStoreDetail
            // 
            this._lblStoreDetail.Location = new System.Drawing.Point(5, 58);
            this._lblStoreDetail.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblStoreDetail.Name = "_lblStoreDetail";
            this._lblStoreDetail.Size = new System.Drawing.Size(72, 18);
            this._lblStoreDetail.TabIndex = 38;
            this._lblStoreDetail.Text = "Detail";
            this._lblStoreDetail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtStoreAddress
            // 
            this.txtStoreAddress.BackColor = System.Drawing.SystemColors.Window;
            this.txtStoreAddress.HideSelection = false;
            this.txtStoreAddress.Location = new System.Drawing.Point(84, 84);
            this.txtStoreAddress.Multiline = true;
            this.txtStoreAddress.Name = "txtStoreAddress";
            this.txtStoreAddress.ReadOnly = true;
            this.txtStoreAddress.Size = new System.Drawing.Size(288, 50);
            this.txtStoreAddress.TabIndex = 35;
            this.txtStoreAddress.TabStop = false;
            this.txtStoreAddress.WordWrap = false;
            this.txtStoreAddress.TextChanged += new System.EventHandler(this.OnStoreAddressChanged);
            // 
            // _lblStore
            // 
            this._lblStore.Location = new System.Drawing.Point(5, 31);
            this._lblStore.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblStore.Name = "_lblStore";
            this._lblStore.Size = new System.Drawing.Size(72, 18);
            this._lblStore.TabIndex = 37;
            this._lblStore.Text = "Store#";
            this._lblStore.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskStore
            // 
            this.mskStore.AllowPromptAsInput = false;
            this.mskStore.HidePromptOnLeave = true;
            this.mskStore.Location = new System.Drawing.Point(84, 31);
            this.mskStore.Mask = "#####";
            this.mskStore.Name = "mskStore";
            this.mskStore.Size = new System.Drawing.Size(60, 20);
            this.mskStore.TabIndex = 34;
            this.mskStore.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mskStore.TextChanged += new System.EventHandler(this.OnStoreTextChanged);
            this.mskStore.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnStoreKeyUp);
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(5, 5);
            this._lblClient.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(72, 18);
            this._lblClient.TabIndex = 36;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboClient
            // 
            this.cboClient.DataSource = this.mClientDS;
            this.cboClient.DisplayMember = "CompanyTable.CompanyName";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.FormattingEnabled = true;
            this.cboClient.Location = new System.Drawing.Point(84, 5);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(288, 21);
            this.cboClient.TabIndex = 33;
            this.cboClient.ValueMember = "CompanyTable.CompanyID";
            this.cboClient.SelectionChangeCommitted += new System.EventHandler(this.OnClientSelected);
            // 
            // mClientDS
            // 
            this.mClientDS.DataSetName = "CRMDataset";
            this.mClientDS.Locale = new System.Globalization.CultureInfo("");
            this.mClientDS.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(467, 34);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(200, 20);
            this.dtpTo.TabIndex = 61;
            this.dtpTo.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpTo.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpTo.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // _lblTo
            // 
            this._lblTo.Location = new System.Drawing.Point(388, 33);
            this._lblTo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(72, 18);
            this._lblTo.TabIndex = 60;
            this._lblTo.Text = "To";
            this._lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(467, 6);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(200, 20);
            this.dtpFrom.TabIndex = 59;
            this.dtpFrom.CloseUp += new System.EventHandler(this.OnCalendarClosed);
            this.dtpFrom.ValueChanged += new System.EventHandler(this.OnCalendarValueChanged);
            this.dtpFrom.DropDown += new System.EventHandler(this.OnCalendarOpened);
            // 
            // _lblFrom
            // 
            this._lblFrom.Location = new System.Drawing.Point(388, 6);
            this._lblFrom.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(72, 18);
            this._lblFrom.TabIndex = 58;
            this._lblFrom.Text = "From";
            this._lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ClientDashboardControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dtpTo);
            this.Controls.Add(this._lblTo);
            this.Controls.Add(this.dtpFrom);
            this.Controls.Add(this._lblFrom);
            this.Controls.Add(this.scClient);
            this.Controls.Add(this.txtStoreDetail);
            this.Controls.Add(this.txtStoreName);
            this.Controls.Add(this.wbStoreMap);
            this.Controls.Add(this._lblStoreDetail);
            this.Controls.Add(this.txtStoreAddress);
            this.Controls.Add(this._lblStore);
            this.Controls.Add(this.mskStore);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.cboClient);
            this.Name = "ClientDashboardControl";
            this.Size = new System.Drawing.Size(814, 496);
            this.Load += new System.EventHandler(this.OnControlLoad);
            this.scClient.Panel1.ResumeLayout(false);
            this.scClient.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.scClient)).EndInit();
            this.scClient.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mClientDS)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.SplitContainer scClient;
        private Microsoft.Reporting.WinForms.ReportViewer rv1;
        private Microsoft.Reporting.WinForms.ReportViewer rv2;
        private System.Windows.Forms.TextBox txtStoreDetail;
        private System.Windows.Forms.TextBox txtStoreName;
        private System.Windows.Forms.WebBrowser wbStoreMap;
        private System.Windows.Forms.Label _lblStoreDetail;
        private System.Windows.Forms.TextBox txtStoreAddress;
        private System.Windows.Forms.Label _lblStore;
        private System.Windows.Forms.MaskedTextBox mskStore;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.ComboBox cboClient;
        private CRMDataset mClientDS;
        private System.Windows.Forms.DateTimePicker dtpTo;
        private System.Windows.Forms.Label _lblTo;
        private System.Windows.Forms.DateTimePicker dtpFrom;
        private System.Windows.Forms.Label _lblFrom;
    }
}
