namespace Argix.Freight {
    partial class dlgPickupRequest {
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
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.mtbClose = new System.Windows.Forms.MaskedTextBox();
            this.mtbOpen = new System.Windows.Forms.MaskedTextBox();
            this._lblAcctID = new System.Windows.Forms.Label();
            this.lblAcctID = new System.Windows.Forms.Label();
            this.bsShippers = new System.Windows.Forms.BindingSource(this.components);
            this.txtShipperPhone = new System.Windows.Forms.MaskedTextBox();
            this._lblShipperPhone = new System.Windows.Forms.Label();
            this._lblLbs = new System.Windows.Forms.Label();
            this._lblWeight = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this.wbAddress = new System.Windows.Forms.WebBrowser();
            this.grpDispatch = new System.Windows.Forms.GroupBox();
            this.cboOrderType = new System.Windows.Forms.ComboBox();
            this._lblOrderType = new System.Windows.Forms.Label();
            this.cboDriver = new System.Windows.Forms.ComboBox();
            this.bsDrivers = new System.Windows.Forms.BindingSource(this.components);
            this._lblDriver = new System.Windows.Forms.Label();
            this.dtpActual = new System.Windows.Forms.DateTimePicker();
            this._lblActual = new System.Windows.Forms.Label();
            this.cboTerminal = new System.Windows.Forms.ComboBox();
            this.bsAgents = new System.Windows.Forms.BindingSource(this.components);
            this.dispatchDataset = new Argix.DispatchDataset();
            this._lblTerminal = new System.Windows.Forms.Label();
            this._lblNote = new System.Windows.Forms.Label();
            this.cboShipper = new System.Windows.Forms.ComboBox();
            this._lblShipperAddress = new System.Windows.Forms.Label();
            this.txtShipperAddress = new System.Windows.Forms.TextBox();
            this._lblWindow = new System.Windows.Forms.Label();
            this._lblComments = new System.Windows.Forms.Label();
            this._lblQuantity = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.cboContainer = new System.Windows.Forms.ComboBox();
            this._lblRequestID = new System.Windows.Forms.Label();
            this._lblClient = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.cboClient = new System.Windows.Forms.ComboBox();
            this.bsClients = new System.Windows.Forms.BindingSource(this.components);
            this._lblCaller = new System.Windows.Forms.Label();
            this.txtCaller = new System.Windows.Forms.TextBox();
            this.lblRequestID = new System.Windows.Forms.Label();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this._lblScheduleDate = new System.Windows.Forms.Label();
            this._lblOf = new System.Windows.Forms.Label();
            this.cboFreightType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.bsShippers)).BeginInit();
            this.grpDispatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.bsDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgents)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispatchDataset)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsClients)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(159, 272);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(24, 18);
            this.label1.TabIndex = 153;
            this.label1.Text = "to";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // mtbClose
            // 
            this.mtbClose.Location = new System.Drawing.Point(186, 272);
            this.mtbClose.Mask = "00:00";
            this.mtbClose.Name = "mtbClose";
            this.mtbClose.Size = new System.Drawing.Size(48, 20);
            this.mtbClose.TabIndex = 8;
            this.mtbClose.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbClose.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // mtbOpen
            // 
            this.mtbOpen.Location = new System.Drawing.Point(108, 272);
            this.mtbOpen.Mask = "00:00";
            this.mtbOpen.Name = "mtbOpen";
            this.mtbOpen.Size = new System.Drawing.Size(48, 20);
            this.mtbOpen.TabIndex = 7;
            this.mtbOpen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mtbOpen.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            this.mtbOpen.Validating += new System.ComponentModel.CancelEventHandler(this.OnWindowValidating);
            // 
            // _lblAcctID
            // 
            this._lblAcctID.Location = new System.Drawing.Point(6, 207);
            this._lblAcctID.Name = "_lblAcctID";
            this._lblAcctID.Size = new System.Drawing.Size(96, 18);
            this._lblAcctID.TabIndex = 150;
            this._lblAcctID.Text = "Acct ID";
            this._lblAcctID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAcctID
            // 
            this.lblAcctID.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsShippers, "AccountID", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.lblAcctID.Location = new System.Drawing.Point(108, 207);
            this.lblAcctID.Name = "lblAcctID";
            this.lblAcctID.Size = new System.Drawing.Size(124, 18);
            this.lblAcctID.TabIndex = 149;
            this.lblAcctID.Text = "Acct ID: ";
            this.lblAcctID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bsShippers
            // 
            this.bsShippers.DataSource = typeof(Argix.Terminals.Customers2);
            this.bsShippers.CurrentChanged += new System.EventHandler(this.OnShipperCurrentChanged);
            // 
            // txtShipperPhone
            // 
            this.txtShipperPhone.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsShippers, "Phone", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.txtShipperPhone.Location = new System.Drawing.Point(108, 237);
            this.txtShipperPhone.Mask = "(999) 000-0000";
            this.txtShipperPhone.Name = "txtShipperPhone";
            this.txtShipperPhone.Size = new System.Drawing.Size(126, 20);
            this.txtShipperPhone.TabIndex = 6;
            this.txtShipperPhone.TextMaskFormat = System.Windows.Forms.MaskFormat.ExcludePromptAndLiterals;
            // 
            // _lblShipperPhone
            // 
            this._lblShipperPhone.Location = new System.Drawing.Point(6, 237);
            this._lblShipperPhone.Name = "_lblShipperPhone";
            this._lblShipperPhone.Size = new System.Drawing.Size(96, 18);
            this._lblShipperPhone.TabIndex = 148;
            this._lblShipperPhone.Text = "Phone";
            this._lblShipperPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLbs
            // 
            this._lblLbs.Location = new System.Drawing.Point(184, 359);
            this._lblLbs.Name = "_lblLbs";
            this._lblLbs.Size = new System.Drawing.Size(48, 18);
            this._lblLbs.TabIndex = 147;
            this._lblLbs.Text = "Lbs";
            this._lblLbs.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblWeight
            // 
            this._lblWeight.Location = new System.Drawing.Point(6, 359);
            this._lblWeight.Name = "_lblWeight";
            this._lblWeight.Size = new System.Drawing.Size(96, 18);
            this._lblWeight.TabIndex = 146;
            this._lblWeight.Text = "Weight";
            this._lblWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(108, 359);
            this.txtWeight.MaxLength = 7;
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(72, 20);
            this.txtWeight.TabIndex = 11;
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
            this.wbAddress.Size = new System.Drawing.Size(340, 249);
            this.wbAddress.TabIndex = 13;
            this.wbAddress.TabStop = false;
            this.wbAddress.DocumentCompleted += new System.Windows.Forms.WebBrowserDocumentCompletedEventHandler(this.OnDocumentCompleted);
            // 
            // grpDispatch
            // 
            this.grpDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDispatch.Controls.Add(this.cboOrderType);
            this.grpDispatch.Controls.Add(this._lblOrderType);
            this.grpDispatch.Controls.Add(this.cboDriver);
            this.grpDispatch.Controls.Add(this._lblDriver);
            this.grpDispatch.Controls.Add(this.dtpActual);
            this.grpDispatch.Controls.Add(this._lblActual);
            this.grpDispatch.Controls.Add(this.cboTerminal);
            this.grpDispatch.Controls.Add(this._lblTerminal);
            this.grpDispatch.Location = new System.Drawing.Point(392, 298);
            this.grpDispatch.Name = "grpDispatch";
            this.grpDispatch.Size = new System.Drawing.Size(340, 165);
            this.grpDispatch.TabIndex = 14;
            this.grpDispatch.TabStop = false;
            this.grpDispatch.Text = "Agent";
            // 
            // cboOrderType
            // 
            this.cboOrderType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboOrderType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOrderType.FormattingEnabled = true;
            this.cboOrderType.Items.AddRange(new object[] {
            "Pickup",
            "Backhaul"});
            this.cboOrderType.Location = new System.Drawing.Point(92, 97);
            this.cboOrderType.Name = "cboOrderType";
            this.cboOrderType.Size = new System.Drawing.Size(120, 21);
            this.cboOrderType.TabIndex = 2;
            // 
            // _lblOrderType
            // 
            this._lblOrderType.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOrderType.Location = new System.Drawing.Point(6, 97);
            this._lblOrderType.Name = "_lblOrderType";
            this._lblOrderType.Size = new System.Drawing.Size(79, 18);
            this._lblOrderType.TabIndex = 139;
            this._lblOrderType.Text = "Order Type";
            this._lblOrderType.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDriver
            // 
            this.cboDriver.DataSource = this.bsDrivers;
            this.cboDriver.DisplayMember = "Name";
            this.cboDriver.Location = new System.Drawing.Point(92, 61);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Size = new System.Drawing.Size(242, 21);
            this.cboDriver.TabIndex = 1;
            this.cboDriver.ValueMember = "RouteName";
            // 
            // bsDrivers
            // 
            this.bsDrivers.DataSource = typeof(Argix.Terminals.Drivers);
            // 
            // _lblDriver
            // 
            this._lblDriver.Location = new System.Drawing.Point(6, 61);
            this._lblDriver.Name = "_lblDriver";
            this._lblDriver.Size = new System.Drawing.Size(79, 18);
            this._lblDriver.TabIndex = 137;
            this._lblDriver.Text = "Driver";
            this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpActual
            // 
            this.dtpActual.CustomFormat = "";
            this.dtpActual.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpActual.Location = new System.Drawing.Point(92, 132);
            this.dtpActual.Name = "dtpActual";
            this.dtpActual.ShowCheckBox = true;
            this.dtpActual.Size = new System.Drawing.Size(120, 20);
            this.dtpActual.TabIndex = 3;
            // 
            // _lblActual
            // 
            this._lblActual.Location = new System.Drawing.Point(6, 132);
            this._lblActual.Name = "_lblActual";
            this._lblActual.Size = new System.Drawing.Size(79, 18);
            this._lblActual.TabIndex = 130;
            this._lblActual.Text = "Actual P/U Date";
            this._lblActual.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTerminal
            // 
            this.cboTerminal.DataSource = this.bsAgents;
            this.cboTerminal.DisplayMember = "Name";
            this.cboTerminal.Location = new System.Drawing.Point(92, 27);
            this.cboTerminal.Name = "cboTerminal";
            this.cboTerminal.Size = new System.Drawing.Size(242, 21);
            this.cboTerminal.TabIndex = 0;
            this.cboTerminal.ValueMember = "Number";
            this.cboTerminal.SelectedIndexChanged += new System.EventHandler(this.OnTerminalSelectedIndexChanged);
            this.cboTerminal.TextChanged += new System.EventHandler(this.OnTerminalTextChanged);
            // 
            // bsAgents
            // 
            this.bsAgents.DataMember = "AgentTable";
            this.bsAgents.DataSource = this.dispatchDataset;
            // 
            // dispatchDataset
            // 
            this.dispatchDataset.DataSetName = "DispatchDataset";
            this.dispatchDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblTerminal
            // 
            this._lblTerminal.Location = new System.Drawing.Point(6, 27);
            this._lblTerminal.Name = "_lblTerminal";
            this._lblTerminal.Size = new System.Drawing.Size(79, 18);
            this._lblTerminal.TabIndex = 138;
            this._lblTerminal.Text = "Terminal";
            this._lblTerminal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblNote
            // 
            this._lblNote.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this._lblNote.Location = new System.Drawing.Point(108, 439);
            this._lblNote.Name = "_lblNote";
            this._lblNote.Size = new System.Drawing.Size(220, 24);
            this._lblNote.TabIndex = 136;
            this._lblNote.Text = "All pickups must be ready when called in.";
            this._lblNote.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cboShipper
            // 
            this.cboShipper.DataSource = this.bsShippers;
            this.cboShipper.DisplayMember = "Name";
            this.cboShipper.Location = new System.Drawing.Point(108, 105);
            this.cboShipper.Name = "cboShipper";
            this.cboShipper.Size = new System.Drawing.Size(250, 21);
            this.cboShipper.TabIndex = 4;
            this.cboShipper.ValueMember = "AccountID";
            this.cboShipper.TextChanged += new System.EventHandler(this.OnShipperTextChanged);
            // 
            // _lblShipperAddress
            // 
            this._lblShipperAddress.Location = new System.Drawing.Point(6, 105);
            this._lblShipperAddress.Name = "_lblShipperAddress";
            this._lblShipperAddress.Size = new System.Drawing.Size(96, 18);
            this._lblShipperAddress.TabIndex = 142;
            this._lblShipperAddress.Text = "Shipper";
            this._lblShipperAddress.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtShipperAddress
            // 
            this.txtShipperAddress.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.bsShippers, "Address", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.txtShipperAddress.Location = new System.Drawing.Point(108, 139);
            this.txtShipperAddress.MaxLength = 75;
            this.txtShipperAddress.Multiline = true;
            this.txtShipperAddress.Name = "txtShipperAddress";
            this.txtShipperAddress.Size = new System.Drawing.Size(250, 54);
            this.txtShipperAddress.TabIndex = 5;
            this.txtShipperAddress.Text = "Address Line 1\r\nAddress Line 2\r\nCity, State Zip-Zip4";
            this.txtShipperAddress.TextChanged += new System.EventHandler(this.OnShipperAddressChanged);
            // 
            // _lblWindow
            // 
            this._lblWindow.Location = new System.Drawing.Point(6, 272);
            this._lblWindow.Name = "_lblWindow";
            this._lblWindow.Size = new System.Drawing.Size(96, 18);
            this._lblWindow.TabIndex = 140;
            this._lblWindow.Text = "Window";
            this._lblWindow.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(6, 396);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(96, 18);
            this._lblComments.TabIndex = 129;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblQuantity
            // 
            this._lblQuantity.Location = new System.Drawing.Point(6, 325);
            this._lblQuantity.Name = "_lblQuantity";
            this._lblQuantity.Size = new System.Drawing.Size(96, 18);
            this._lblQuantity.TabIndex = 125;
            this._lblQuantity.Text = "Quantity";
            this._lblQuantity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(108, 396);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(250, 20);
            this.txtComments.TabIndex = 12;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(108, 325);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(50, 20);
            this.txtQuantity.TabIndex = 9;
            this.txtQuantity.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // cboContainer
            // 
            this.cboContainer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboContainer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboContainer.Items.AddRange(new object[] {
            "Cartons",
            "Pallets"});
            this.cboContainer.Location = new System.Drawing.Point(162, 325);
            this.cboContainer.MaxDropDownItems = 5;
            this.cboContainer.Name = "cboContainer";
            this.cboContainer.Size = new System.Drawing.Size(75, 21);
            this.cboContainer.TabIndex = 10;
            this.cboContainer.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblRequestID
            // 
            this._lblRequestID.Location = new System.Drawing.Point(504, 12);
            this._lblRequestID.Name = "_lblRequestID";
            this._lblRequestID.Size = new System.Drawing.Size(72, 18);
            this._lblRequestID.TabIndex = 135;
            this._lblRequestID.Text = "Pickup#";
            this._lblRequestID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblClient
            // 
            this._lblClient.Location = new System.Drawing.Point(6, 72);
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
            this.btnCancel.Location = new System.Drawing.Point(636, 476);
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
            this.btnOk.Location = new System.Drawing.Point(534, 476);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 24);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnClick);
            // 
            // cboClient
            // 
            this.cboClient.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboClient.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboClient.DataSource = this.bsClients;
            this.cboClient.DisplayMember = "Name";
            this.cboClient.Location = new System.Drawing.Point(108, 72);
            this.cboClient.Name = "cboClient";
            this.cboClient.Size = new System.Drawing.Size(250, 21);
            this.cboClient.TabIndex = 3;
            this.cboClient.ValueMember = "Number";
            this.cboClient.SelectedIndexChanged += new System.EventHandler(this.OnClientSelectedIndexChanged);
            this.cboClient.TextChanged += new System.EventHandler(this.OnClientTextChanged);
            // 
            // bsClients
            // 
            this.bsClients.DataMember = "ClientTable";
            this.bsClients.DataSource = this.dispatchDataset;
            // 
            // _lblCaller
            // 
            this._lblCaller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblCaller.Location = new System.Drawing.Point(6, 43);
            this._lblCaller.Name = "_lblCaller";
            this._lblCaller.Size = new System.Drawing.Size(96, 18);
            this._lblCaller.TabIndex = 137;
            this._lblCaller.Text = "Caller";
            this._lblCaller.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCaller
            // 
            this.txtCaller.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtCaller.Location = new System.Drawing.Point(108, 43);
            this.txtCaller.Name = "txtCaller";
            this.txtCaller.Size = new System.Drawing.Size(144, 20);
            this.txtCaller.TabIndex = 2;
            // 
            // lblRequestID
            // 
            this.lblRequestID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblRequestID.Location = new System.Drawing.Point(589, 12);
            this.lblRequestID.Name = "lblRequestID";
            this.lblRequestID.Size = new System.Drawing.Size(120, 18);
            this.lblRequestID.TabIndex = 141;
            this.lblRequestID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this._lblScheduleDate.Text = "Pickup Date";
            this._lblScheduleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblOf
            // 
            this._lblOf.Location = new System.Drawing.Point(243, 325);
            this._lblOf.Name = "_lblOf";
            this._lblOf.Size = new System.Drawing.Size(20, 18);
            this._lblOf.TabIndex = 172;
            this._lblOf.Text = "of";
            this._lblOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboFreightType
            // 
            this.cboFreightType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFreightType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFreightType.Items.AddRange(new object[] {
            "ISA",
            "PCS",
            "PSP",
            "Tsort"});
            this.cboFreightType.Location = new System.Drawing.Point(268, 325);
            this.cboFreightType.MaxDropDownItems = 5;
            this.cboFreightType.Name = "cboFreightType";
            this.cboFreightType.Size = new System.Drawing.Size(90, 21);
            this.cboFreightType.TabIndex = 202;
            this.cboFreightType.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // dlgPickupRequest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(744, 512);
            this.Controls.Add(this.cboFreightType);
            this.Controls.Add(this._lblOf);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.label1);
            this.Controls.Add(this._lblScheduleDate);
            this.Controls.Add(this.mtbClose);
            this.Controls.Add(this.lblRequestID);
            this.Controls.Add(this.mtbOpen);
            this.Controls.Add(this.txtCaller);
            this.Controls.Add(this._lblAcctID);
            this.Controls.Add(this._lblCaller);
            this.Controls.Add(this.lblAcctID);
            this.Controls.Add(this.cboClient);
            this.Controls.Add(this.txtShipperPhone);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this._lblShipperPhone);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this._lblLbs);
            this.Controls.Add(this._lblWeight);
            this.Controls.Add(this._lblClient);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this._lblRequestID);
            this.Controls.Add(this.wbAddress);
            this.Controls.Add(this.cboShipper);
            this.Controls.Add(this.grpDispatch);
            this.Controls.Add(this.cboContainer);
            this.Controls.Add(this._lblNote);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblShipperAddress);
            this.Controls.Add(this._lblQuantity);
            this.Controls.Add(this.txtShipperAddress);
            this.Controls.Add(this._lblComments);
            this.Controls.Add(this._lblWindow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgPickupRequest";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pickup Request";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.bsShippers)).EndInit();
            this.grpDispatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.bsDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsAgents)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dispatchDataset)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bsClients)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblShipperAddress;
        private System.Windows.Forms.TextBox txtShipperAddress;
        private System.Windows.Forms.Label _lblWindow;
        private System.Windows.Forms.Label _lblTerminal;
        private System.Windows.Forms.Label _lblDriver;
        private System.Windows.Forms.Label _lblRequestID;
        private System.Windows.Forms.DateTimePicker dtpActual;
        private System.Windows.Forms.Label _lblActual;
        private System.Windows.Forms.Label _lblComments;
        private System.Windows.Forms.Label _lblQuantity;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.ComboBox cboContainer;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.ComboBox cboTerminal;
        private System.Windows.Forms.Label _lblClient;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.ComboBox cboClient;
        private System.Windows.Forms.GroupBox grpDispatch;
        private System.Windows.Forms.ComboBox cboShipper;
        private System.Windows.Forms.WebBrowser wbAddress;
        private System.Windows.Forms.Label _lblNote;
        private System.Windows.Forms.Label _lblCaller;
        private System.Windows.Forms.TextBox txtCaller;
        private System.Windows.Forms.ComboBox cboDriver;
        private System.Windows.Forms.Label _lblWeight;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.BindingSource bsClients;
        private System.Windows.Forms.BindingSource bsShippers;
        private System.Windows.Forms.BindingSource bsAgents;
        private System.Windows.Forms.BindingSource bsDrivers;
        private System.Windows.Forms.Label lblRequestID;
        private System.Windows.Forms.Label _lblLbs;
        private System.Windows.Forms.Label _lblShipperPhone;
        private System.Windows.Forms.MaskedTextBox txtShipperPhone;
        private System.Windows.Forms.Label lblAcctID;
        private System.Windows.Forms.Label _lblAcctID;
        private System.Windows.Forms.MaskedTextBox mtbClose;
        private System.Windows.Forms.MaskedTextBox mtbOpen;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cboOrderType;
        private System.Windows.Forms.Label _lblOrderType;
        private System.Windows.Forms.DateTimePicker dtpScheduleDate;
        private System.Windows.Forms.Label _lblScheduleDate;
        private DispatchDataset dispatchDataset;
        private System.Windows.Forms.Label _lblOf;
        private System.Windows.Forms.ComboBox cboFreightType;
    }
}