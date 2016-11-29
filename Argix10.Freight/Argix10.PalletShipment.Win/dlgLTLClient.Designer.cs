namespace Argix.Freight {
    partial class dlgLTLClient {
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtZip5 = new System.Windows.Forms.TextBox();
            this.txtZip4 = new System.Windows.Forms.TextBox();
            this._lblZip = new System.Windows.Forms.Label();
            this.txtState = new System.Windows.Forms.TextBox();
            this._lblState = new System.Windows.Forms.Label();
            this.txtCity = new System.Windows.Forms.TextBox();
            this._lblCity = new System.Windows.Forms.Label();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this._lblAddress = new System.Windows.Forms.Label();
            this.txtName = new System.Windows.Forms.TextBox();
            this._lblName = new System.Windows.Forms.Label();
            this.grpCorporate = new System.Windows.Forms.GroupBox();
            this.txtCorpAddressLine2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.chkUseCompanyforCorporate = new System.Windows.Forms.CheckBox();
            this.txtCorpZip4 = new System.Windows.Forms.TextBox();
            this.txtCorpZip5 = new System.Windows.Forms.TextBox();
            this._lblCorpZip = new System.Windows.Forms.Label();
            this.txtCorpState = new System.Windows.Forms.TextBox();
            this._lblCorpState = new System.Windows.Forms.Label();
            this.txtCorpCity = new System.Windows.Forms.TextBox();
            this._lblCorpCity = new System.Windows.Forms.Label();
            this.txtCorpAddressLine1 = new System.Windows.Forms.TextBox();
            this._lblCorpAddress = new System.Windows.Forms.Label();
            this.txtCorpName = new System.Windows.Forms.TextBox();
            this._lblCorpName = new System.Windows.Forms.Label();
            this.grpBilling = new System.Windows.Forms.GroupBox();
            this.txtBillAddressLine2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtBillZip4 = new System.Windows.Forms.TextBox();
            this.chkUseCompanyforBilling = new System.Windows.Forms.CheckBox();
            this.txtBillZip5 = new System.Windows.Forms.TextBox();
            this._lblBillZip = new System.Windows.Forms.Label();
            this.txtBillState = new System.Windows.Forms.TextBox();
            this._lblBillState = new System.Windows.Forms.Label();
            this.txtBillCity = new System.Windows.Forms.TextBox();
            this._lblBillCity = new System.Windows.Forms.Label();
            this.txtBillAddressLine1 = new System.Windows.Forms.TextBox();
            this._lblBillAddress = new System.Windows.Forms.Label();
            this.grpContact = new System.Windows.Forms.GroupBox();
            this.mskContactPhone = new System.Windows.Forms.MaskedTextBox();
            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this._lblContactEmail = new System.Windows.Forms.Label();
            this._lblContactPhone = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this._lblContactName = new System.Windows.Forms.Label();
            this.grpFinance = new System.Windows.Forms.GroupBox();
            this.txtTaxID = new System.Windows.Forms.TextBox();
            this._lblCorpTaxID = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this._lblStatus = new System.Windows.Forms.Label();
            this.txtTsortClientNumber = new System.Windows.Forms.TextBox();
            this._lblTsortClientNumber = new System.Windows.Forms.Label();
            this.grpSales = new System.Windows.Forms.GroupBox();
            this.txtSalesRepClientNumber = new System.Windows.Forms.TextBox();
            this._lblSalesRepClientNumber = new System.Windows.Forms.Label();
            this.btnVerifyAddress = new System.Windows.Forms.Button();
            this.grpCompany.SuspendLayout();
            this.grpCorporate.SuspendLayout();
            this.grpBilling.SuspendLayout();
            this.grpContact.SuspendLayout();
            this.grpFinance.SuspendLayout();
            this.grpSales.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(707, 536);
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
            this.btnOk.Location = new System.Drawing.Point(626, 536);
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
            this.grpCompany.Controls.Add(this.label2);
            this.grpCompany.Controls.Add(this.txtZip5);
            this.grpCompany.Controls.Add(this.txtZip4);
            this.grpCompany.Controls.Add(this._lblZip);
            this.grpCompany.Controls.Add(this.txtState);
            this.grpCompany.Controls.Add(this._lblState);
            this.grpCompany.Controls.Add(this.txtCity);
            this.grpCompany.Controls.Add(this._lblCity);
            this.grpCompany.Controls.Add(this.txtAddressLine1);
            this.grpCompany.Controls.Add(this._lblAddress);
            this.grpCompany.Controls.Add(this.txtName);
            this.grpCompany.Controls.Add(this._lblName);
            this.grpCompany.Location = new System.Drawing.Point(12, 12);
            this.grpCompany.Name = "grpCompany";
            this.grpCompany.Size = new System.Drawing.Size(375, 155);
            this.grpCompany.TabIndex = 2;
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
            this.txtAddressLine2.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(269, 124);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 20);
            this.label2.TabIndex = 21;
            this.label2.Text = "-";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtZip5
            // 
            this.txtZip5.Location = new System.Drawing.Point(192, 124);
            this.txtZip5.MaxLength = 5;
            this.txtZip5.Name = "txtZip5";
            this.txtZip5.Size = new System.Drawing.Size(75, 20);
            this.txtZip5.TabIndex = 5;
            this.txtZip5.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtZip4
            // 
            this.txtZip4.Location = new System.Drawing.Point(280, 124);
            this.txtZip4.MaxLength = 4;
            this.txtZip4.Name = "txtZip4";
            this.txtZip4.ReadOnly = true;
            this.txtZip4.Size = new System.Drawing.Size(60, 20);
            this.txtZip4.TabIndex = 20;
            // 
            // _lblZip
            // 
            this._lblZip.Location = new System.Drawing.Point(160, 124);
            this._lblZip.Name = "_lblZip";
            this._lblZip.Size = new System.Drawing.Size(25, 20);
            this._lblZip.TabIndex = 12;
            this._lblZip.Text = "Zip";
            this._lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtState
            // 
            this.txtState.Location = new System.Drawing.Point(90, 124);
            this.txtState.MaxLength = 2;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(50, 20);
            this.txtState.TabIndex = 4;
            this.txtState.TextChanged += new System.EventHandler(this.OnValidateForm);
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
            this.txtCity.TextChanged += new System.EventHandler(this.OnValidateForm);
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
            this.txtAddressLine1.TextChanged += new System.EventHandler(this.OnValidateForm);
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
            // grpCorporate
            // 
            this.grpCorporate.Controls.Add(this.txtCorpAddressLine2);
            this.grpCorporate.Controls.Add(this.label1);
            this.grpCorporate.Controls.Add(this.chkUseCompanyforCorporate);
            this.grpCorporate.Controls.Add(this.txtCorpZip4);
            this.grpCorporate.Controls.Add(this.txtCorpZip5);
            this.grpCorporate.Controls.Add(this._lblCorpZip);
            this.grpCorporate.Controls.Add(this.txtCorpState);
            this.grpCorporate.Controls.Add(this._lblCorpState);
            this.grpCorporate.Controls.Add(this.txtCorpCity);
            this.grpCorporate.Controls.Add(this._lblCorpCity);
            this.grpCorporate.Controls.Add(this.txtCorpAddressLine1);
            this.grpCorporate.Controls.Add(this._lblCorpAddress);
            this.grpCorporate.Controls.Add(this.txtCorpName);
            this.grpCorporate.Controls.Add(this._lblCorpName);
            this.grpCorporate.Location = new System.Drawing.Point(12, 173);
            this.grpCorporate.Name = "grpCorporate";
            this.grpCorporate.Size = new System.Drawing.Size(375, 180);
            this.grpCorporate.TabIndex = 3;
            this.grpCorporate.TabStop = false;
            this.grpCorporate.Text = "Corporate";
            // 
            // txtCorpAddressLine2
            // 
            this.txtCorpAddressLine2.Location = new System.Drawing.Point(90, 99);
            this.txtCorpAddressLine2.MaxLength = 40;
            this.txtCorpAddressLine2.Name = "txtCorpAddressLine2";
            this.txtCorpAddressLine2.Size = new System.Drawing.Size(250, 20);
            this.txtCorpAddressLine2.TabIndex = 3;
            this.txtCorpAddressLine2.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(269, 152);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 20);
            this.label1.TabIndex = 23;
            this.label1.Text = "-";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // chkUseCompanyforCorporate
            // 
            this.chkUseCompanyforCorporate.AutoSize = true;
            this.chkUseCompanyforCorporate.Location = new System.Drawing.Point(90, 20);
            this.chkUseCompanyforCorporate.Name = "chkUseCompanyforCorporate";
            this.chkUseCompanyforCorporate.Size = new System.Drawing.Size(133, 17);
            this.chkUseCompanyforCorporate.TabIndex = 0;
            this.chkUseCompanyforCorporate.Text = "Use Company Address";
            this.chkUseCompanyforCorporate.UseVisualStyleBackColor = true;
            this.chkUseCompanyforCorporate.CheckedChanged += new System.EventHandler(this.OnUseCompanyForCorporate);
            // 
            // txtCorpZip4
            // 
            this.txtCorpZip4.Location = new System.Drawing.Point(280, 152);
            this.txtCorpZip4.MaxLength = 4;
            this.txtCorpZip4.Name = "txtCorpZip4";
            this.txtCorpZip4.ReadOnly = true;
            this.txtCorpZip4.Size = new System.Drawing.Size(60, 20);
            this.txtCorpZip4.TabIndex = 22;
            // 
            // txtCorpZip5
            // 
            this.txtCorpZip5.Location = new System.Drawing.Point(192, 152);
            this.txtCorpZip5.MaxLength = 5;
            this.txtCorpZip5.Name = "txtCorpZip5";
            this.txtCorpZip5.Size = new System.Drawing.Size(75, 20);
            this.txtCorpZip5.TabIndex = 6;
            this.txtCorpZip5.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCorpZip
            // 
            this._lblCorpZip.Location = new System.Drawing.Point(160, 150);
            this._lblCorpZip.Name = "_lblCorpZip";
            this._lblCorpZip.Size = new System.Drawing.Size(25, 20);
            this._lblCorpZip.TabIndex = 12;
            this._lblCorpZip.Text = "Zip";
            this._lblCorpZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCorpState
            // 
            this.txtCorpState.Location = new System.Drawing.Point(90, 151);
            this.txtCorpState.MaxLength = 2;
            this.txtCorpState.Name = "txtCorpState";
            this.txtCorpState.Size = new System.Drawing.Size(50, 20);
            this.txtCorpState.TabIndex = 5;
            this.txtCorpState.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCorpState
            // 
            this._lblCorpState.Location = new System.Drawing.Point(5, 151);
            this._lblCorpState.Name = "_lblCorpState";
            this._lblCorpState.Size = new System.Drawing.Size(75, 20);
            this._lblCorpState.TabIndex = 10;
            this._lblCorpState.Text = "State";
            this._lblCorpState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCorpCity
            // 
            this.txtCorpCity.Location = new System.Drawing.Point(90, 125);
            this.txtCorpCity.MaxLength = 40;
            this.txtCorpCity.Name = "txtCorpCity";
            this.txtCorpCity.Size = new System.Drawing.Size(250, 20);
            this.txtCorpCity.TabIndex = 4;
            this.txtCorpCity.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCorpCity
            // 
            this._lblCorpCity.Location = new System.Drawing.Point(5, 125);
            this._lblCorpCity.Name = "_lblCorpCity";
            this._lblCorpCity.Size = new System.Drawing.Size(75, 20);
            this._lblCorpCity.TabIndex = 8;
            this._lblCorpCity.Text = "City";
            this._lblCorpCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCorpAddressLine1
            // 
            this.txtCorpAddressLine1.Location = new System.Drawing.Point(90, 73);
            this.txtCorpAddressLine1.MaxLength = 40;
            this.txtCorpAddressLine1.Name = "txtCorpAddressLine1";
            this.txtCorpAddressLine1.Size = new System.Drawing.Size(250, 20);
            this.txtCorpAddressLine1.TabIndex = 2;
            this.txtCorpAddressLine1.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCorpAddress
            // 
            this._lblCorpAddress.Location = new System.Drawing.Point(5, 73);
            this._lblCorpAddress.Name = "_lblCorpAddress";
            this._lblCorpAddress.Size = new System.Drawing.Size(75, 20);
            this._lblCorpAddress.TabIndex = 6;
            this._lblCorpAddress.Text = "Address";
            this._lblCorpAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCorpName
            // 
            this.txtCorpName.Location = new System.Drawing.Point(90, 47);
            this.txtCorpName.MaxLength = 40;
            this.txtCorpName.Name = "txtCorpName";
            this.txtCorpName.Size = new System.Drawing.Size(250, 20);
            this.txtCorpName.TabIndex = 1;
            this.txtCorpName.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCorpName
            // 
            this._lblCorpName.Location = new System.Drawing.Point(5, 47);
            this._lblCorpName.Name = "_lblCorpName";
            this._lblCorpName.Size = new System.Drawing.Size(75, 20);
            this._lblCorpName.TabIndex = 4;
            this._lblCorpName.Text = "Name";
            this._lblCorpName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpBilling
            // 
            this.grpBilling.Controls.Add(this.txtBillAddressLine2);
            this.grpBilling.Controls.Add(this.label3);
            this.grpBilling.Controls.Add(this.txtBillZip4);
            this.grpBilling.Controls.Add(this.chkUseCompanyforBilling);
            this.grpBilling.Controls.Add(this.txtBillZip5);
            this.grpBilling.Controls.Add(this._lblBillZip);
            this.grpBilling.Controls.Add(this.txtBillState);
            this.grpBilling.Controls.Add(this._lblBillState);
            this.grpBilling.Controls.Add(this.txtBillCity);
            this.grpBilling.Controls.Add(this._lblBillCity);
            this.grpBilling.Controls.Add(this.txtBillAddressLine1);
            this.grpBilling.Controls.Add(this._lblBillAddress);
            this.grpBilling.Location = new System.Drawing.Point(12, 359);
            this.grpBilling.Name = "grpBilling";
            this.grpBilling.Size = new System.Drawing.Size(375, 160);
            this.grpBilling.TabIndex = 4;
            this.grpBilling.TabStop = false;
            this.grpBilling.Text = "Billing";
            // 
            // txtBillAddressLine2
            // 
            this.txtBillAddressLine2.Location = new System.Drawing.Point(90, 73);
            this.txtBillAddressLine2.MaxLength = 40;
            this.txtBillAddressLine2.Name = "txtBillAddressLine2";
            this.txtBillAddressLine2.Size = new System.Drawing.Size(250, 20);
            this.txtBillAddressLine2.TabIndex = 2;
            this.txtBillAddressLine2.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(269, 126);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 20);
            this.label3.TabIndex = 8;
            this.label3.Text = "-";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtBillZip4
            // 
            this.txtBillZip4.Location = new System.Drawing.Point(280, 126);
            this.txtBillZip4.MaxLength = 4;
            this.txtBillZip4.Name = "txtBillZip4";
            this.txtBillZip4.ReadOnly = true;
            this.txtBillZip4.Size = new System.Drawing.Size(60, 20);
            this.txtBillZip4.TabIndex = 7;
            // 
            // chkUseCompanyforBilling
            // 
            this.chkUseCompanyforBilling.AutoSize = true;
            this.chkUseCompanyforBilling.Location = new System.Drawing.Point(90, 20);
            this.chkUseCompanyforBilling.Name = "chkUseCompanyforBilling";
            this.chkUseCompanyforBilling.Size = new System.Drawing.Size(133, 17);
            this.chkUseCompanyforBilling.TabIndex = 0;
            this.chkUseCompanyforBilling.Text = "Use Company Address";
            this.chkUseCompanyforBilling.UseVisualStyleBackColor = true;
            this.chkUseCompanyforBilling.CheckedChanged += new System.EventHandler(this.OnUseCompanyForBilling);
            // 
            // txtBillZip5
            // 
            this.txtBillZip5.Location = new System.Drawing.Point(192, 126);
            this.txtBillZip5.MaxLength = 5;
            this.txtBillZip5.Name = "txtBillZip5";
            this.txtBillZip5.Size = new System.Drawing.Size(75, 20);
            this.txtBillZip5.TabIndex = 5;
            this.txtBillZip5.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBillZip
            // 
            this._lblBillZip.Location = new System.Drawing.Point(160, 125);
            this._lblBillZip.Name = "_lblBillZip";
            this._lblBillZip.Size = new System.Drawing.Size(25, 20);
            this._lblBillZip.TabIndex = 10;
            this._lblBillZip.Text = "Zip";
            this._lblBillZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBillState
            // 
            this.txtBillState.Location = new System.Drawing.Point(90, 125);
            this.txtBillState.MaxLength = 2;
            this.txtBillState.Name = "txtBillState";
            this.txtBillState.Size = new System.Drawing.Size(50, 20);
            this.txtBillState.TabIndex = 4;
            this.txtBillState.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBillState
            // 
            this._lblBillState.Location = new System.Drawing.Point(5, 125);
            this._lblBillState.Name = "_lblBillState";
            this._lblBillState.Size = new System.Drawing.Size(75, 20);
            this._lblBillState.TabIndex = 8;
            this._lblBillState.Text = "State";
            this._lblBillState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBillCity
            // 
            this.txtBillCity.Location = new System.Drawing.Point(90, 99);
            this.txtBillCity.MaxLength = 40;
            this.txtBillCity.Name = "txtBillCity";
            this.txtBillCity.Size = new System.Drawing.Size(250, 20);
            this.txtBillCity.TabIndex = 3;
            this.txtBillCity.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBillCity
            // 
            this._lblBillCity.Location = new System.Drawing.Point(5, 99);
            this._lblBillCity.Name = "_lblBillCity";
            this._lblBillCity.Size = new System.Drawing.Size(75, 20);
            this._lblBillCity.TabIndex = 6;
            this._lblBillCity.Text = "City";
            this._lblBillCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBillAddressLine1
            // 
            this.txtBillAddressLine1.Location = new System.Drawing.Point(90, 47);
            this.txtBillAddressLine1.MaxLength = 40;
            this.txtBillAddressLine1.Name = "txtBillAddressLine1";
            this.txtBillAddressLine1.Size = new System.Drawing.Size(250, 20);
            this.txtBillAddressLine1.TabIndex = 1;
            this.txtBillAddressLine1.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBillAddress
            // 
            this._lblBillAddress.Location = new System.Drawing.Point(5, 47);
            this._lblBillAddress.Name = "_lblBillAddress";
            this._lblBillAddress.Size = new System.Drawing.Size(75, 20);
            this._lblBillAddress.TabIndex = 4;
            this._lblBillAddress.Text = "Address";
            this._lblBillAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpContact
            // 
            this.grpContact.Controls.Add(this.mskContactPhone);
            this.grpContact.Controls.Add(this.txtContactEmail);
            this.grpContact.Controls.Add(this._lblContactEmail);
            this.grpContact.Controls.Add(this._lblContactPhone);
            this.grpContact.Controls.Add(this.txtContactName);
            this.grpContact.Controls.Add(this._lblContactName);
            this.grpContact.Location = new System.Drawing.Point(400, 12);
            this.grpContact.Name = "grpContact";
            this.grpContact.Size = new System.Drawing.Size(375, 118);
            this.grpContact.TabIndex = 5;
            this.grpContact.TabStop = false;
            this.grpContact.Text = "Contact";
            // 
            // mskContactPhone
            // 
            this.mskContactPhone.Location = new System.Drawing.Point(90, 46);
            this.mskContactPhone.Mask = "(999) 000-0000";
            this.mskContactPhone.Name = "mskContactPhone";
            this.mskContactPhone.Size = new System.Drawing.Size(125, 20);
            this.mskContactPhone.TabIndex = 1;
            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Location = new System.Drawing.Point(90, 72);
            this.txtContactEmail.MaxLength = 50;
            this.txtContactEmail.Name = "txtContactEmail";
            this.txtContactEmail.Size = new System.Drawing.Size(225, 20);
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
            // grpFinance
            // 
            this.grpFinance.Controls.Add(this.txtTaxID);
            this.grpFinance.Controls.Add(this._lblCorpTaxID);
            this.grpFinance.Controls.Add(this.cboStatus);
            this.grpFinance.Controls.Add(this._lblStatus);
            this.grpFinance.Controls.Add(this.txtTsortClientNumber);
            this.grpFinance.Controls.Add(this._lblTsortClientNumber);
            this.grpFinance.Location = new System.Drawing.Point(400, 359);
            this.grpFinance.Name = "grpFinance";
            this.grpFinance.Size = new System.Drawing.Size(375, 145);
            this.grpFinance.TabIndex = 7;
            this.grpFinance.TabStop = false;
            this.grpFinance.Text = "Finance";
            // 
            // txtTaxID
            // 
            this.txtTaxID.Location = new System.Drawing.Point(95, 99);
            this.txtTaxID.MaxLength = 10;
            this.txtTaxID.Name = "txtTaxID";
            this.txtTaxID.Size = new System.Drawing.Size(100, 20);
            this.txtTaxID.TabIndex = 2;
            this.txtTaxID.Text = "12-3456789";
            this.txtTaxID.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblCorpTaxID
            // 
            this._lblCorpTaxID.Location = new System.Drawing.Point(10, 99);
            this._lblCorpTaxID.Name = "_lblCorpTaxID";
            this._lblCorpTaxID.Size = new System.Drawing.Size(75, 20);
            this._lblCorpTaxID.TabIndex = 16;
            this._lblCorpTaxID.Text = "TaxID#";
            this._lblCorpTaxID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboStatus
            // 
            this.cboStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboStatus.FormattingEnabled = true;
            this.cboStatus.Items.AddRange(new object[] {
            "Approved",
            "Denied"});
            this.cboStatus.Location = new System.Drawing.Point(95, 73);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(125, 21);
            this.cboStatus.TabIndex = 1;
            this.cboStatus.SelectedIndexChanged += new System.EventHandler(this.OnStatusChanged);
            // 
            // _lblStatus
            // 
            this._lblStatus.Location = new System.Drawing.Point(6, 73);
            this._lblStatus.Name = "_lblStatus";
            this._lblStatus.Size = new System.Drawing.Size(75, 20);
            this._lblStatus.TabIndex = 8;
            this._lblStatus.Text = "Status";
            this._lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTsortClientNumber
            // 
            this.txtTsortClientNumber.Location = new System.Drawing.Point(95, 46);
            this.txtTsortClientNumber.MaxLength = 3;
            this.txtTsortClientNumber.Name = "txtTsortClientNumber";
            this.txtTsortClientNumber.ReadOnly = true;
            this.txtTsortClientNumber.Size = new System.Drawing.Size(57, 20);
            this.txtTsortClientNumber.TabIndex = 0;
            this.txtTsortClientNumber.TextChanged += new System.EventHandler(this.OnNumberChanged);
            // 
            // _lblTsortClientNumber
            // 
            this._lblTsortClientNumber.Location = new System.Drawing.Point(5, 46);
            this._lblTsortClientNumber.Name = "_lblTsortClientNumber";
            this._lblTsortClientNumber.Size = new System.Drawing.Size(75, 20);
            this._lblTsortClientNumber.TabIndex = 6;
            this._lblTsortClientNumber.Text = "Tsort Client#";
            this._lblTsortClientNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpSales
            // 
            this.grpSales.Controls.Add(this.txtSalesRepClientNumber);
            this.grpSales.Controls.Add(this._lblSalesRepClientNumber);
            this.grpSales.Location = new System.Drawing.Point(400, 173);
            this.grpSales.Name = "grpSales";
            this.grpSales.Size = new System.Drawing.Size(375, 93);
            this.grpSales.TabIndex = 6;
            this.grpSales.TabStop = false;
            this.grpSales.Text = "Sales";
            // 
            // txtSalesRepClientNumber
            // 
            this.txtSalesRepClientNumber.Location = new System.Drawing.Point(95, 47);
            this.txtSalesRepClientNumber.MaxLength = 3;
            this.txtSalesRepClientNumber.Name = "txtSalesRepClientNumber";
            this.txtSalesRepClientNumber.Size = new System.Drawing.Size(57, 20);
            this.txtSalesRepClientNumber.TabIndex = 0;
            this.txtSalesRepClientNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblSalesRepClientNumber
            // 
            this._lblSalesRepClientNumber.Location = new System.Drawing.Point(5, 47);
            this._lblSalesRepClientNumber.Name = "_lblSalesRepClientNumber";
            this._lblSalesRepClientNumber.Size = new System.Drawing.Size(75, 20);
            this._lblSalesRepClientNumber.TabIndex = 11;
            this._lblSalesRepClientNumber.Text = "Rep Client#";
            this._lblSalesRepClientNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnVerifyAddress
            // 
            this.btnVerifyAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnVerifyAddress.Location = new System.Drawing.Point(490, 536);
            this.btnVerifyAddress.Name = "btnVerifyAddress";
            this.btnVerifyAddress.Size = new System.Drawing.Size(100, 23);
            this.btnVerifyAddress.TabIndex = 8;
            this.btnVerifyAddress.Text = "&Verify Address";
            this.btnVerifyAddress.UseVisualStyleBackColor = true;
            this.btnVerifyAddress.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // dlgLTLClient
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 571);
            this.Controls.Add(this.btnVerifyAddress);
            this.Controls.Add(this.grpSales);
            this.Controls.Add(this.grpFinance);
            this.Controls.Add(this.grpContact);
            this.Controls.Add(this.grpBilling);
            this.Controls.Add(this.grpCorporate);
            this.Controls.Add(this.grpCompany);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgLTLClient";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.grpCompany.ResumeLayout(false);
            this.grpCompany.PerformLayout();
            this.grpCorporate.ResumeLayout(false);
            this.grpCorporate.PerformLayout();
            this.grpBilling.ResumeLayout(false);
            this.grpBilling.PerformLayout();
            this.grpContact.ResumeLayout(false);
            this.grpContact.PerformLayout();
            this.grpFinance.ResumeLayout(false);
            this.grpFinance.PerformLayout();
            this.grpSales.ResumeLayout(false);
            this.grpSales.PerformLayout();
            this.ResumeLayout(false);

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
        private System.Windows.Forms.GroupBox grpCorporate;
        private System.Windows.Forms.TextBox txtCorpZip5;
        private System.Windows.Forms.Label _lblCorpZip;
        private System.Windows.Forms.TextBox txtCorpState;
        private System.Windows.Forms.Label _lblCorpState;
        private System.Windows.Forms.TextBox txtCorpCity;
        private System.Windows.Forms.Label _lblCorpCity;
        private System.Windows.Forms.TextBox txtCorpAddressLine1;
        private System.Windows.Forms.Label _lblCorpAddress;
        private System.Windows.Forms.TextBox txtCorpName;
        private System.Windows.Forms.Label _lblCorpName;
        private System.Windows.Forms.GroupBox grpBilling;
        private System.Windows.Forms.TextBox txtBillZip5;
        private System.Windows.Forms.Label _lblBillZip;
        private System.Windows.Forms.TextBox txtBillState;
        private System.Windows.Forms.Label _lblBillState;
        private System.Windows.Forms.TextBox txtBillCity;
        private System.Windows.Forms.Label _lblBillCity;
        private System.Windows.Forms.TextBox txtBillAddressLine1;
        private System.Windows.Forms.Label _lblBillAddress;
        private System.Windows.Forms.GroupBox grpContact;
        private System.Windows.Forms.TextBox txtContactEmail;
        private System.Windows.Forms.Label _lblContactEmail;
        private System.Windows.Forms.Label _lblContactPhone;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label _lblContactName;
        private System.Windows.Forms.GroupBox grpFinance;
        private System.Windows.Forms.TextBox txtTsortClientNumber;
        private System.Windows.Forms.Label _lblTsortClientNumber;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Label _lblStatus;
        private System.Windows.Forms.GroupBox grpSales;
        private System.Windows.Forms.TextBox txtSalesRepClientNumber;
        private System.Windows.Forms.Label _lblSalesRepClientNumber;
        private System.Windows.Forms.CheckBox chkUseCompanyforCorporate;
        private System.Windows.Forms.CheckBox chkUseCompanyforBilling;
        private System.Windows.Forms.TextBox txtTaxID;
        private System.Windows.Forms.Label _lblCorpTaxID;
        private System.Windows.Forms.MaskedTextBox mskContactPhone;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZip4;
        private System.Windows.Forms.TextBox txtAddressLine2;
        private System.Windows.Forms.TextBox txtCorpAddressLine2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCorpZip4;
        private System.Windows.Forms.TextBox txtBillAddressLine2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtBillZip4;
        private System.Windows.Forms.Button btnVerifyAddress;
    }
}