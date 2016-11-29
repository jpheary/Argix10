namespace Argix.Freight {
    partial class dlgLoadTender {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) { if(disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.mtbClose = new System.Windows.Forms.MaskedTextBox();
            this.mtbOpen = new System.Windows.Forms.MaskedTextBox();
            this.txtContactPhone = new System.Windows.Forms.MaskedTextBox();
            this._lblContactPhone = new System.Windows.Forms.Label();
            this._lblLbs = new System.Windows.Forms.Label();
            this._lblWeight = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.wbAddress = new System.Windows.Forms.WebBrowser();
            this.cboShipper = new System.Windows.Forms.ComboBox();
            this.mVendors = new Argix.DispatchDataset();
            this._lblShipperAddress = new System.Windows.Forms.Label();
            this._lblWindow = new System.Windows.Forms.Label();
            this._lblComments = new System.Windows.Forms.Label();
            this._lblQuantity = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.cboContainer = new System.Windows.Forms.ComboBox();
            this._lblID = new System.Windows.Forms.Label();
            this._lblClient = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.mClients = new Argix.DispatchDataset();
            this.lblID = new System.Windows.Forms.Label();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this._lblScheduleDate = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this._lblContactName = new System.Windows.Forms.Label();
            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this._lblContactEmail = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this._lblDescription = new System.Windows.Forms.Label();
            this.chkFullTrailer = new System.Windows.Forms.CheckBox();
            this.txtState = new System.Windows.Forms.TextBox();
            this._lblState = new System.Windows.Forms.Label();
            this.txtAddressLine2 = new System.Windows.Forms.TextBox();
            this.txtZip = new System.Windows.Forms.TextBox();
            this.txtCity = new System.Windows.Forms.TextBox();
            this._lblCity = new System.Windows.Forms.Label();
            this._lblZip = new System.Windows.Forms.Label();
            this.txtAddressLine1 = new System.Windows.Forms.TextBox();
            this._lblAddress = new System.Windows.Forms.Label();
            this.txtZip4 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.mVendors)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(159, 310);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 18);
            this.label1.TabIndex = 153;
            this.label1.Text = "to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtbClose
            // 
            this.mtbClose.Location = new System.Drawing.Point(186, 310);
            this.mtbClose.Mask = "00:00";
            this.mtbClose.Name = "mtbClose";
            this.mtbClose.Size = new System.Drawing.Size(48, 20);
            this.mtbClose.TabIndex = 14;
            this.mtbClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbClose.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // mtbOpen
            // 
            this.mtbOpen.Location = new System.Drawing.Point(108, 310);
            this.mtbOpen.Mask = "00:00";
            this.mtbOpen.Name = "mtbOpen";
            this.mtbOpen.Size = new System.Drawing.Size(48, 20);
            this.mtbOpen.TabIndex = 13;
            this.mtbOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbOpen.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtbOpen.Validating += new System.ComponentModel.CancelEventHandler(this.OnWindowValidating);
            // 
            // txtContactPhone
            // 
            this.txtContactPhone.Location = new System.Drawing.Point(108, 245);
            this.txtContactPhone.Mask = "(999) 000-0000";
            this.txtContactPhone.Name = "txtContactPhone";
            this.txtContactPhone.Size = new System.Drawing.Size(126, 20);
            this.txtContactPhone.TabIndex = 11;
            this.txtContactPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // _lblContactPhone
            // 
            this._lblContactPhone.Location = new System.Drawing.Point(6, 245);
            this._lblContactPhone.Name = "_lblContactPhone";
            this._lblContactPhone.Size = new System.Drawing.Size(96, 18);
            this._lblContactPhone.TabIndex = 148;
            this._lblContactPhone.Text = "Phone";
            this._lblContactPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLbs
            // 
            this._lblLbs.Location = new System.Drawing.Point(184, 391);
            this._lblLbs.Name = "_lblLbs";
            this._lblLbs.Size = new System.Drawing.Size(48, 18);
            this._lblLbs.TabIndex = 147;
            this._lblLbs.Text = "Lbs";
            this._lblLbs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblWeight
            // 
            this._lblWeight.Location = new System.Drawing.Point(6, 391);
            this._lblWeight.Name = "_lblWeight";
            this._lblWeight.Size = new System.Drawing.Size(96, 18);
            this._lblWeight.TabIndex = 146;
            this._lblWeight.Text = "Weight";
            this._lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(108, 391);
            this.txtWeight.MaxLength = 7;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(72, 20);
            this.txtWeight.TabIndex = 18;
            this.txtWeight.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // wbAddress
            // 
            this.wbAddress.AllowNavigation = false;
            this.wbAddress.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.wbAddress.Location = new System.Drawing.Point(392, 43);
            this.wbAddress.Name = "wbAddress";
            this.wbAddress.ScriptErrorsSuppressed = true;
            this.wbAddress.ScrollBarsEnabled = false;
            this.wbAddress.Size = new System.Drawing.Size(340, 287);
            this.wbAddress.TabIndex = 13;
            this.wbAddress.TabStop = false;
            this.wbAddress.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnDocumentCompleted);
            // 
            // cboShipper
            // 
            this.cboShipper.DataSource = this.mVendors;
            this.cboShipper.DisplayMember = "VendorTable.Name";
            this.cboShipper.Location = new System.Drawing.Point(108, 76);
            this.cboShipper.Name = "cboShipper";
            this.cboShipper.Size = new System.Drawing.Size(250, 21);
            this.cboShipper.TabIndex = 3;
            this.cboShipper.ValueMember = "VendorTable.Number";
            this.cboShipper.TextChanged += new System.EventHandler(this.OnShipperTextChanged);
            // 
            // mVendors
            // 
            this.mVendors.DataSetName = "DispatchDataset";
            this.mVendors.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblShipperAddress
            // 
            this._lblShipperAddress.Location = new System.Drawing.Point(6, 76);
            this._lblShipperAddress.Name = "_lblShipperAddress";
            this._lblShipperAddress.Size = new System.Drawing.Size(96, 18);
            this._lblShipperAddress.TabIndex = 142;
            this._lblShipperAddress.Text = "Vendor";
            this._lblShipperAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblWindow
            // 
            this._lblWindow.Location = new System.Drawing.Point(6, 310);
            this._lblWindow.Name = "_lblWindow";
            this._lblWindow.Size = new System.Drawing.Size(96, 18);
            this._lblWindow.TabIndex = 140;
            this._lblWindow.Text = "Window";
            this._lblWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(6, 422);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(96, 18);
            this._lblComments.TabIndex = 129;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblQuantity
            // 
            this._lblQuantity.Location = new System.Drawing.Point(6, 365);
            this._lblQuantity.Name = "_lblQuantity";
            this._lblQuantity.Size = new System.Drawing.Size(96, 18);
            this._lblQuantity.TabIndex = 125;
            this._lblQuantity.Text = "Quantity";
            this._lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(108, 422);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(250, 20);
            this.txtComments.TabIndex = 20;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(108, 365);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(50, 20);
            this.txtQuantity.TabIndex = 16;
            this.txtQuantity.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // cboContainer
            // 
            this.cboContainer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboContainer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboContainer.Items.AddRange(new object[] {
            "Cartons",
            "Pallets"});
            this.cboContainer.Location = new System.Drawing.Point(162, 365);
            this.cboContainer.MaxDropDownItems = 5;
            this.cboContainer.Name = "cboContainer";
            this.cboContainer.Size = new System.Drawing.Size(75, 21);
            this.cboContainer.TabIndex = 17;
            this.cboContainer.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblID
            // 
            this._lblID.Location = new System.Drawing.Point(464, 12);
            this._lblID.Name = "_lblID";
            this._lblID.Size = new System.Drawing.Size(112, 18);
            this._lblID.TabIndex = 135;
            this._lblID.Text = "Load Tender#";
            this._lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(6, 43);
            this._lblClient.Name = "_lblClient";
            this._lblClient.Size = new System.Drawing.Size(96, 18);
            this._lblClient.TabIndex = 130;
            this._lblClient.Text = "Client";
            this._lblClient.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(636, 460);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnClick);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(534, 460);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 24);
            this.btnOk.TabIndex = 21;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnClick);
            // 
            // cboClient
            // 
            this.cboClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClient.DataSource = this.mClients;
            this.cboClient.DisplayMember = "ClientTable.Name";
            this.cboClient.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboClient.Location = new System.Drawing.Point(108, 43);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(250, 21);
            this.cboClient.TabIndex = 2;
            this.cboClient.ValueMember = "ClientTable.Number";
            this.cboClient.SelectedIndexChanged += new System.EventHandler(this.OnClientSelectedIndexChanged);
            // 
            // mClients
            // 
            this.mClients.DataSetName = "DispatchDataset";
            this.mClients.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // lblID
            // 
            this.lblID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblID.Location = new System.Drawing.Point(589, 12);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(120, 18);
            this.lblID.TabIndex = 141;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScheduleDate.Location = new System.Drawing.Point(108, 12);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduleDate.TabIndex = 1;
            // 
            // _lblScheduleDate
            // 
            this._lblScheduleDate.Location = new System.Drawing.Point(6, 12);
            this._lblScheduleDate.Name = "_lblScheduleDate";
            this._lblScheduleDate.Size = new System.Drawing.Size(96, 16);
            this._lblScheduleDate.TabIndex = 171;
            this._lblScheduleDate.Text = "Schedule Date";
            this._lblScheduleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(108, 219);
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(250, 20);
            this.txtContactName.TabIndex = 10;
            // 
            // _lblContactName
            // 
            this._lblContactName.Location = new System.Drawing.Point(6, 219);
            this._lblContactName.Name = "_lblContactName";
            this._lblContactName.Size = new System.Drawing.Size(96, 18);
            this._lblContactName.TabIndex = 204;
            this._lblContactName.Text = "Name";
            this._lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Location = new System.Drawing.Point(108, 271);
            this.txtContactEmail.Name = "txtContactEmail";
            this.txtContactEmail.Size = new System.Drawing.Size(250, 20);
            this.txtContactEmail.TabIndex = 12;
            // 
            // _lblContactEmail
            // 
            this._lblContactEmail.Location = new System.Drawing.Point(6, 271);
            this._lblContactEmail.Name = "_lblContactEmail";
            this._lblContactEmail.Size = new System.Drawing.Size(96, 18);
            this._lblContactEmail.TabIndex = 206;
            this._lblContactEmail.Text = "Email";
            this._lblContactEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(108, 339);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(250, 20);
            this.txtDescription.TabIndex = 15;
            // 
            // _lblDescription
            // 
            this._lblDescription.Location = new System.Drawing.Point(6, 339);
            this._lblDescription.Name = "_lblDescription";
            this._lblDescription.Size = new System.Drawing.Size(96, 18);
            this._lblDescription.TabIndex = 208;
            this._lblDescription.Text = "Description";
            this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFullTrailer
            // 
            this.chkFullTrailer.AutoSize = true;
            this.chkFullTrailer.Location = new System.Drawing.Point(273, 391);
            this.chkFullTrailer.Name = "chkFullTrailer";
            this.chkFullTrailer.Size = new System.Drawing.Size(85, 17);
            this.chkFullTrailer.TabIndex = 19;
            this.chkFullTrailer.Text = "Is Full Trailer";
            this.chkFullTrailer.UseVisualStyleBackColor = true;
            // 
            // txtState
            // 
            this.txtState.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mVendors, "VendorTable.State", true));
            this.txtState.Location = new System.Drawing.Point(108, 181);
            this.txtState.MaxLength = 2;
            this.txtState.Name = "txtState";
            this.txtState.Size = new System.Drawing.Size(50, 20);
            this.txtState.TabIndex = 7;
            this.txtState.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // _lblState
            // 
            this._lblState.Location = new System.Drawing.Point(23, 181);
            this._lblState.Name = "_lblState";
            this._lblState.Size = new System.Drawing.Size(75, 20);
            this._lblState.TabIndex = 217;
            this._lblState.Text = "State";
            this._lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddressLine2
            // 
            this.txtAddressLine2.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mVendors, "VendorTable.AddressLine2", true));
            this.txtAddressLine2.Location = new System.Drawing.Point(108, 129);
            this.txtAddressLine2.MaxLength = 40;
            this.txtAddressLine2.Name = "txtAddressLine2";
            this.txtAddressLine2.Size = new System.Drawing.Size(250, 20);
            this.txtAddressLine2.TabIndex = 5;
            this.txtAddressLine2.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // txtZip
            // 
            this.txtZip.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mVendors, "VendorTable.Zip", true));
            this.txtZip.Location = new System.Drawing.Point(223, 181);
            this.txtZip.MaxLength = 5;
            this.txtZip.Name = "txtZip";
            this.txtZip.Size = new System.Drawing.Size(75, 20);
            this.txtZip.TabIndex = 8;
            this.txtZip.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // txtCity
            // 
            this.txtCity.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mVendors, "VendorTable.City", true));
            this.txtCity.Location = new System.Drawing.Point(108, 155);
            this.txtCity.MaxLength = 40;
            this.txtCity.Name = "txtCity";
            this.txtCity.Size = new System.Drawing.Size(250, 20);
            this.txtCity.TabIndex = 6;
            this.txtCity.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // _lblCity
            // 
            this._lblCity.Location = new System.Drawing.Point(23, 155);
            this._lblCity.Name = "_lblCity";
            this._lblCity.Size = new System.Drawing.Size(75, 20);
            this._lblCity.TabIndex = 216;
            this._lblCity.Text = "City";
            this._lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblZip
            // 
            this._lblZip.Location = new System.Drawing.Point(188, 181);
            this._lblZip.Name = "_lblZip";
            this._lblZip.Size = new System.Drawing.Size(25, 20);
            this._lblZip.TabIndex = 218;
            this._lblZip.Text = "Zip";
            this._lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAddressLine1
            // 
            this.txtAddressLine1.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mVendors, "VendorTable.AddressLine1", true));
            this.txtAddressLine1.Location = new System.Drawing.Point(108, 103);
            this.txtAddressLine1.MaxLength = 40;
            this.txtAddressLine1.Name = "txtAddressLine1";
            this.txtAddressLine1.Size = new System.Drawing.Size(250, 20);
            this.txtAddressLine1.TabIndex = 4;
            this.txtAddressLine1.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // _lblAddress
            // 
            this._lblAddress.Location = new System.Drawing.Point(23, 103);
            this._lblAddress.Name = "_lblAddress";
            this._lblAddress.Size = new System.Drawing.Size(75, 20);
            this._lblAddress.TabIndex = 215;
            this._lblAddress.Text = "Address";
            this._lblAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtZip4
            // 
            this.txtZip4.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mVendors, "VendorTable.Zip4", true));
            this.txtZip4.Location = new System.Drawing.Point(304, 181);
            this.txtZip4.MaxLength = 4;
            this.txtZip4.Name = "txtZip4";
            this.txtZip4.Size = new System.Drawing.Size(54, 20);
            this.txtZip4.TabIndex = 9;
            this.txtZip4.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // dlgLoadTender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 496);
            this.Controls.Add(this.txtZip4);
            this.Controls.Add(this.txtState);
            this.Controls.Add(this._lblState);
            this.Controls.Add(this.txtAddressLine2);
            this.Controls.Add(this.txtZip);
            this.Controls.Add(this.txtCity);
            this.Controls.Add(this._lblCity);
            this.Controls.Add(this._lblZip);
            this.Controls.Add(this.txtAddressLine1);
            this.Controls.Add(this._lblAddress);
            this.Controls.Add(this.chkFullTrailer);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this._lblDescription);
            this.Controls.Add(this.txtContactEmail);
            this.Controls.Add(this._lblContactEmail);
            this.Controls.Add(this.txtContactName);
            this.Controls.Add(this._lblContactName);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lblScheduleDate);
            this.Controls.Add(this.mtbClose);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.mtbOpen);
            this.Controls.Add(this.cboClient);
            this.Controls.Add(this.txtContactPhone);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this._lblContactPhone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this._lblLbs);
            this.Controls.Add(this._lblWeight);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this._lblID);
            this.Controls.Add(this.wbAddress);
            this.Controls.Add(this.cboShipper);
            this.Controls.Add(this.cboContainer);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblShipperAddress);
            this.Controls.Add(this._lblQuantity);
            this.Controls.Add(this._lblComments);
            this.Controls.Add(this._lblWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgLoadTender";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Tender";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mVendors)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblShipperAddress;
        private System.Windows.Forms.Label _lblWindow;
        private System.Windows.Forms.Label _lblID;
        private System.Windows.Forms.Label _lblComments;
        private System.Windows.Forms.Label _lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.ComboBox cboContainer;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.ComboBox cboShipper;
        private System.Windows.Forms.WebBrowser wbAddress;
        private System.Windows.Forms.Label _lblWeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label lblID;
        private System.Windows.Forms.Label _lblLbs;
        private System.Windows.Forms.Label _lblContactPhone;
        private System.Windows.Forms.MaskedTextBox txtContactPhone;
        private System.Windows.Forms.MaskedTextBox mtbClose;
        private System.Windows.Forms.MaskedTextBox mtbOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker dtpScheduleDate;
        private System.Windows.Forms.Label _lblScheduleDate;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label _lblContactName;
        private System.Windows.Forms.TextBox txtContactEmail;
        private System.Windows.Forms.Label _lblContactEmail;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label _lblDescription;
        private System.Windows.Forms.CheckBox chkFullTrailer;
        private DispatchDataset mClients;
        private DispatchDataset mVendors;
        private System.Windows.Forms.TextBox txtState;
        private System.Windows.Forms.Label _lblState;
        private System.Windows.Forms.TextBox txtAddressLine2;
        private System.Windows.Forms.TextBox txtZip;
        private System.Windows.Forms.TextBox txtCity;
        private System.Windows.Forms.Label _lblCity;
        private System.Windows.Forms.Label _lblZip;
        private System.Windows.Forms.TextBox txtAddressLine1;
        private System.Windows.Forms.Label _lblAddress;
        private System.Windows.Forms.TextBox txtZip4;
    }
}