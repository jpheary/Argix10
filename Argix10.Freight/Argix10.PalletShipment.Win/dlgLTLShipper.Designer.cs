namespace Argix.Freight {
    partial class dlgLTLShipper {
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.grpCompany = new System.Windows.Forms.GroupBox();
            this.txtAddressLine2 = new System.Windows.Forms.TextBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this._lblState = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this._lblCity = new System.Windows.Forms.Label();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this._lblAddress = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this._lblName = new System.Windows.Forms.Label();
            this.mskWindowEnd = new System.Windows.Forms.MaskedTextBox();
            this.mskWindowStart = new System.Windows.Forms.MaskedTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this._lbl = new System.Windows.Forms.Label();
            this.txtZip5 = new System.Windows.Forms.TextBox();
            this._lblZip = new System.Windows.Forms.Label();
            this.grpContact = new System.Windows.Forms.GroupBox();
            this.mskContactPhone = new System.Windows.Forms.MaskedTextBox();
            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this._lblContactEmail = new System.Windows.Forms.Label();
            this._lblContactPhone = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this._lblContactName = new System.Windows.Forms.Label();
            this.wbMap = new System.Windows.Forms.WebBrowser();
            this.chkActive = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZip4 = new System.Windows.Forms.TextBox();
            this.btnVerifyAddress = new System.Windows.Forms.Button();
            this.grpCompany.SuspendLayout();
            this.grpContact.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(757, 386);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "&Close";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(676, 386);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "O&k";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // grpCompany
            // 
            this.grpCompany.Controls.Add(this.txtAddressLine2);
            this.grpCompany.Controls.Add(this.txtState);
            this.grpCompany.Controls.Add(this._lblState);
            this.grpCompany.Controls.Add(this.txtCity);
            this.grpCompany.Controls.Add(this._lblCity);
            this.grpCompany.Controls.Add(this.txtAddressLine1);
            this.grpCompany.Controls.Add(this._lblAddress);
            this.grpCompany.Controls.Add(this.txtName);
            this.grpCompany.Controls.Add(this._lblName);
            this.grpCompany.Location = new System.Drawing.Point(12, 38);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Size = new System.Drawing.Size(375, 155);
            this.grpCompany.TabIndex = 3;
            this.grpCompany.TabStop = false;
            this.grpCompany.Text = "Company";
            // 
            // txtAddressLine2
            // 
            this.txtAddressLine2.Location = new System.Drawing.Point(90, 72);
            this.txtAddressLine2.MaxLength = 40;
            this.txtAddressLine2.Name = "txtAddressLine2";
            this.txtAddressLine2.Size = new System.Drawing.Size(250, 20);
            this.txtAddressLine2.TabIndex = 2;
            this.txtAddressLine2.TextChanged += new System.EventHandler(this.OnAddressChanged);
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(90, 124);
            this.txtState.MaxLength = 2;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(50, 20);
            this.txtState.TabIndex = 4;
            this.txtState.TextChanged += new System.EventHandler(this.OnAddressChanged);
            // 
            // _lblState
            // 
            this._lblState.Location = new System.Drawing.Point(5, 124);
            this._lblState.Name = "_lblState";
            this._lblState.Size = new System.Drawing.Size(75, 20);
            this._lblState.TabIndex = 10;
            this._lblState.Text = "State";
            this._lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCity
            // 
            this.txtCity.Location = new System.Drawing.Point(90, 98);
            this.txtCity.MaxLength = 40;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(250, 20);
            this.txtCity.TabIndex = 3;
            this.txtCity.TextChanged += new System.EventHandler(this.OnAddressChanged);
            // 
            // _lblCity
            // 
            this._lblCity.Location = new System.Drawing.Point(5, 98);
            this._lblCity.Name = "_lblCity";
            this._lblCity.Size = new System.Drawing.Size(75, 20);
            this._lblCity.TabIndex = 8;
            this._lblCity.Text = "City";
            this._lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.Location = new System.Drawing.Point(90, 46);
            this.txtAddressLine1.MaxLength = 40;
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.Size = new System.Drawing.Size(250, 20);
            this.txtAddressLine1.TabIndex = 1;
            this.txtAddressLine1.TextChanged += new System.EventHandler(this.OnAddressChanged);
            // 
            // _lblAddress
            // 
            this._lblAddress.Location = new System.Drawing.Point(5, 46);
            this._lblAddress.Name = "_lblAddress";
            this._lblAddress.Size = new System.Drawing.Size(75, 20);
            this._lblAddress.TabIndex = 6;
            this._lblAddress.Text = "Address";
            this._lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtName
            // 
            this.txtName.Location = new System.Drawing.Point(90, 20);
            this.txtName.MaxLength = 40;
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(250, 20);
            this.txtName.TabIndex = 0;
            this.txtName.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblName
            // 
            this._lblName.Location = new System.Drawing.Point(5, 20);
            this._lblName.Name = "_lblName";
            this._lblName.Size = new System.Drawing.Size(75, 20);
            this._lblName.TabIndex = 4;
            this._lblName.Text = "Name";
            this._lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // mskWindowEnd
            // 
            this.mskWindowEnd.Location = new System.Drawing.Point(189, 323);
            this.mskWindowEnd.Mask = "00:00";
            this.mskWindowEnd.Name = "mskWindowEnd";
            this.mskWindowEnd.Size = new System.Drawing.Size(50, 20);
            this.mskWindowEnd.TabIndex = 6;
            this.mskWindowEnd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskWindowEnd.ValidatingType = typeof(System.DateTime);
            this.mskWindowEnd.Validating += new System.ComponentModel.CancelEventHandler(this.OnWindowValidating);
            this.mskWindowEnd.Validated += new System.EventHandler(this.OnWindowValidated);
            // 
            // mskWindowStart
            // 
            this.mskWindowStart.Location = new System.Drawing.Point(102, 323);
            this.mskWindowStart.Mask = "00:00";
            this.mskWindowStart.Name = "mskWindowStart";
            this.mskWindowStart.Size = new System.Drawing.Size(50, 20);
            this.mskWindowStart.TabIndex = 5;
            this.mskWindowStart.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mskWindowStart.ValidatingType = typeof(System.DateTime);
            this.mskWindowStart.Validating += new System.ComponentModel.CancelEventHandler(this.OnWindowValidating);
            this.mskWindowStart.Validated += new System.EventHandler(this.OnWindowValidated);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(158, 322);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 20);
            this.label1.TabIndex = 16;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lbl
            // 
            this._lbl.Location = new System.Drawing.Point(17, 322);
            this._lbl.Name = "_lbl";
            this._lbl.Size = new System.Drawing.Size(75, 20);
            this._lbl.TabIndex = 14;
            this._lbl.Text = "Operating Hrs";
            this._lbl.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZip5
            // 
            this.txtZip5.Location = new System.Drawing.Point(102, 12);
            this.txtZip5.MaxLength = 5;
            this.txtZip5.Name = "txtZip5";
            this.txtZip5.Size = new System.Drawing.Size(75, 20);
            this.txtZip5.TabIndex = 2;
            this.txtZip5.TextChanged += new System.EventHandler(this.OnZipChanged);
            // 
            // _lblZip
            // 
            this._lblZip.Location = new System.Drawing.Point(67, 12);
            this._lblZip.Name = "_lblZip";
            this._lblZip.Size = new System.Drawing.Size(25, 20);
            this._lblZip.TabIndex = 12;
            this._lblZip.Text = "Zip";
            this._lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpContact
            // 
            this.grpContact.Controls.Add(this.mskContactPhone);
            this.grpContact.Controls.Add(this.txtContactEmail);
            this.grpContact.Controls.Add(this._lblContactEmail);
            this.grpContact.Controls.Add(this._lblContactPhone);
            this.grpContact.Controls.Add(this.txtContactName);
            this.grpContact.Controls.Add(this._lblContactName);
            this.grpContact.Location = new System.Drawing.Point(12, 199);
            this.grpContact.Name = "grpContact";
            this.grpContact.Size = new System.Drawing.Size(375, 105);
            this.grpContact.TabIndex = 4;
            this.grpContact.TabStop = false;
            this.grpContact.Text = "Contact";
            // 
            // mskContactPhone
            // 
            this.mskContactPhone.Location = new System.Drawing.Point(90, 46);
            this.mskContactPhone.Mask = "000-000-0000";
            this.mskContactPhone.Name = "mskContactPhone";
            this.mskContactPhone.Size = new System.Drawing.Size(100, 20);
            this.mskContactPhone.TabIndex = 1;
            this.mskContactPhone.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Location = new System.Drawing.Point(90, 72);
            this.txtContactEmail.MaxLength = 50;
            this.txtContactEmail.Name = "txtContactEmail";
            this.txtContactEmail.Size = new System.Drawing.Size(250, 20);
            this.txtContactEmail.TabIndex = 2;
            this.txtContactEmail.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblContactEmail
            // 
            this._lblContactEmail.Location = new System.Drawing.Point(5, 72);
            this._lblContactEmail.Name = "_lblContactEmail";
            this._lblContactEmail.Size = new System.Drawing.Size(75, 20);
            this._lblContactEmail.TabIndex = 8;
            this._lblContactEmail.Text = "Email";
            this._lblContactEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblContactPhone
            // 
            this._lblContactPhone.Location = new System.Drawing.Point(5, 46);
            this._lblContactPhone.Name = "_lblContactPhone";
            this._lblContactPhone.Size = new System.Drawing.Size(75, 20);
            this._lblContactPhone.TabIndex = 6;
            this._lblContactPhone.Text = "Phone";
            this._lblContactPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(90, 20);
            this.txtContactName.MaxLength = 40;
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(250, 20);
            this.txtContactName.TabIndex = 0;
            this.txtContactName.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblContactName
            // 
            this._lblContactName.Location = new System.Drawing.Point(5, 20);
            this._lblContactName.Name = "_lblContactName";
            this._lblContactName.Size = new System.Drawing.Size(75, 20);
            this._lblContactName.TabIndex = 4;
            this._lblContactName.Text = "Name";
            this._lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // wbMap
            // 
            this.wbMap.Location = new System.Drawing.Point(412, 12);
            this.wbMap.MinimumSize = new System.Drawing.Size(20, 20);
            this.wbMap.Name = "wbMap";
            this.wbMap.Size = new System.Drawing.Size(420, 331);
            this.wbMap.TabIndex = 8;
            this.wbMap.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnMapDocumentCompleted);
            // 
            // chkActive
            // 
            this.chkActive.AutoSize = true;
            this.chkActive.Location = new System.Drawing.Point(12, 392);
            this.chkActive.Name = "chkActive";
            this.chkActive.Size = new System.Drawing.Size(56, 17);
            this.chkActive.TabIndex = 18;
            this.chkActive.Text = "Active";
            this.chkActive.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(183, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtZip4
            // 
            this.txtZip4.Location = new System.Drawing.Point(214, 12);
            this.txtZip4.MaxLength = 4;
            this.txtZip4.Name = "txtZip4";
            this.txtZip4.ReadOnly = true;
            this.txtZip4.Size = new System.Drawing.Size(60, 20);
            this.txtZip4.TabIndex = 20;
            // 
            // btnVerifyAddress
            // 
            this.btnVerifyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerifyAddress.Location = new System.Drawing.Point(514, 386);
            this.btnVerifyAddress.Name = "btnVerifyAddress";
            this.btnVerifyAddress.Size = new System.Drawing.Size(100, 23);
            this.btnVerifyAddress.TabIndex = 7;
            this.btnVerifyAddress.Text = "&Verify Address";
            this.btnVerifyAddress.UseVisualStyleBackColor = true;
            this.btnVerifyAddress.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // dlgLTLShipper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(844, 421);
            this.Controls.Add(this.btnVerifyAddress);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtZip4);
            this.Controls.Add(this.chkActive);
            this.Controls.Add(this.txtZip5);
            this.Controls.Add(this._lblZip);
            this.Controls.Add(this.mskWindowEnd);
            this.Controls.Add(this.mskWindowStart);
            this.Controls.Add(this.wbMap);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpContact);
            this.Controls.Add(this._lbl);
            this.Controls.Add(this.grpCompany);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgLTLShipper";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Shipper";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.grpContact.ResumeLayout(false);
            this.grpContact.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.GroupBox grpCompany;
        private System.Windows.Forms.TextBox txtZip5;
        private System.Windows.Forms.Label _lblZip;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label _lblState;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label _lblCity;
        private System.Windows.Forms.TextBox txtAddressLine1;
        private System.Windows.Forms.Label _lblAddress;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.Label _lblName;
        private System.Windows.Forms.GroupBox grpContact;
        private System.Windows.Forms.TextBox txtContactEmail;
        private System.Windows.Forms.Label _lblContactEmail;
        private System.Windows.Forms.Label _lblContactPhone;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label _lblContactName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label _lbl;
        private System.Windows.Forms.MaskedTextBox mskContactPhone;
        private System.Windows.Forms.WebBrowser wbMap;
        private System.Windows.Forms.MaskedTextBox mskWindowEnd;
        private System.Windows.Forms.MaskedTextBox mskWindowStart;
        private System.Windows.Forms.CheckBox chkActive;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZip4;
        private System.Windows.Forms.TextBox txtAddressLine2;
        private System.Windows.Forms.Button btnVerifyAddress;
    }
}