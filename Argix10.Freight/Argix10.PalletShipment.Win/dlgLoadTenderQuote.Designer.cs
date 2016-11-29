namespace Argix.Freight {
    partial class dlgLoadTenderQuote {
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.grpContact = new System.Windows.Forms.GroupBox();
            this.mskContactPhone = new System.Windows.Forms.MaskedTextBox();
            this.txtContactEmail = new System.Windows.Forms.TextBox();
            this._lblContactEmail = new System.Windows.Forms.Label();
            this._lblContactPhone = new System.Windows.Forms.Label();
            this.txtContactName = new System.Windows.Forms.TextBox();
            this._lblContactName = new System.Windows.Forms.Label();
            this.txtBroker = new System.Windows.Forms.TextBox();
            this._lblBroker = new System.Windows.Forms.Label();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this._lblDescription = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this._lblComments = new System.Windows.Forms.Label();
            this.grpQuote = new System.Windows.Forms.GroupBox();
            this.txtTotal = new System.Windows.Forms.TextBox();
            this._lblTotal = new System.Windows.Forms.Label();
            this.txtTSC = new System.Windows.Forms.TextBox();
            this._lblTSC = new System.Windows.Forms.Label();
            this.txtInsurance = new System.Windows.Forms.TextBox();
            this._lblInsurance = new System.Windows.Forms.Label();
            this.txtAccessorials = new System.Windows.Forms.TextBox();
            this._lblAccessorials = new System.Windows.Forms.Label();
            this.txtFSC = new System.Windows.Forms.TextBox();
            this._lblFSC = new System.Windows.Forms.Label();
            this.txtRate = new System.Windows.Forms.TextBox();
            this._lblRate = new System.Windows.Forms.Label();
            this.txtWeight = new System.Windows.Forms.TextBox();
            this._blWeight = new System.Windows.Forms.Label();
            this.txtPallets = new System.Windows.Forms.TextBox();
            this._lblPallets = new System.Windows.Forms.Label();
            this.grpLoadTender = new System.Windows.Forms.GroupBox();
            this.txtLoad = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.txtOrigin = new System.Windows.Forms.TextBox();
            this.txtShipDate = new System.Windows.Forms.TextBox();
            this._lblLoad = new System.Windows.Forms.Label();
            this._lblDestination = new System.Windows.Forms.Label();
            this._lblOrigin = new System.Windows.Forms.Label();
            this._lblShipDate = new System.Windows.Forms.Label();
            this.chkApproved = new System.Windows.Forms.CheckBox();
            this.grpContact.SuspendLayout();
            this.grpQuote.SuspendLayout();
            this.grpLoadTender.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(426, 411);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "O&k";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(507, 411);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // grpContact
            // 
            this.grpContact.Controls.Add(this.mskContactPhone);
            this.grpContact.Controls.Add(this.txtContactEmail);
            this.grpContact.Controls.Add(this._lblContactEmail);
            this.grpContact.Controls.Add(this._lblContactPhone);
            this.grpContact.Controls.Add(this.txtContactName);
            this.grpContact.Controls.Add(this._lblContactName);
            this.grpContact.Location = new System.Drawing.Point(12, 70);
            this.grpContact.Name = "grpContact";
            this.grpContact.Size = new System.Drawing.Size(337, 125);
            this.grpContact.TabIndex = 3;
            this.grpContact.TabStop = false;
            this.grpContact.Text = "Contact";
            // 
            // mskContactPhone
            // 
            this.mskContactPhone.Location = new System.Drawing.Point(90, 51);
            this.mskContactPhone.Mask = "(999) 000-0000";
            this.mskContactPhone.Name = "mskContactPhone";
            this.mskContactPhone.Size = new System.Drawing.Size(125, 20);
            this.mskContactPhone.TabIndex = 1;
            this.mskContactPhone.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtContactEmail
            // 
            this.txtContactEmail.Location = new System.Drawing.Point(90, 77);
            this.txtContactEmail.MaxLength = 50;
            this.txtContactEmail.Name = "txtContactEmail";
            this.txtContactEmail.Size = new System.Drawing.Size(225, 20);
            this.txtContactEmail.TabIndex = 2;
            this.txtContactEmail.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblContactEmail
            // 
            this._lblContactEmail.Location = new System.Drawing.Point(5, 77);
            this._lblContactEmail.Name = "_lblContactEmail";
            this._lblContactEmail.Size = new System.Drawing.Size(75, 20);
            this._lblContactEmail.TabIndex = 8;
            this._lblContactEmail.Text = "Email";
            this._lblContactEmail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblContactPhone
            // 
            this._lblContactPhone.Location = new System.Drawing.Point(5, 51);
            this._lblContactPhone.Name = "_lblContactPhone";
            this._lblContactPhone.Size = new System.Drawing.Size(75, 20);
            this._lblContactPhone.TabIndex = 6;
            this._lblContactPhone.Text = "Phone";
            this._lblContactPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtContactName
            // 
            this.txtContactName.Location = new System.Drawing.Point(90, 25);
            this.txtContactName.MaxLength = 40;
            this.txtContactName.Name = "txtContactName";
            this.txtContactName.Size = new System.Drawing.Size(200, 20);
            this.txtContactName.TabIndex = 0;
            this.txtContactName.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblContactName
            // 
            this._lblContactName.Location = new System.Drawing.Point(5, 25);
            this._lblContactName.Name = "_lblContactName";
            this._lblContactName.Size = new System.Drawing.Size(75, 20);
            this._lblContactName.TabIndex = 4;
            this._lblContactName.Text = "Name";
            this._lblContactName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBroker
            // 
            this.txtBroker.Location = new System.Drawing.Point(102, 39);
            this.txtBroker.MaxLength = 40;
            this.txtBroker.Name = "txtBroker";
            this.txtBroker.Size = new System.Drawing.Size(200, 20);
            this.txtBroker.TabIndex = 2;
            this.txtBroker.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBroker
            // 
            this._lblBroker.Location = new System.Drawing.Point(17, 39);
            this._lblBroker.Name = "_lblBroker";
            this._lblBroker.Size = new System.Drawing.Size(75, 20);
            this._lblBroker.TabIndex = 4;
            this._lblBroker.Text = "Broker";
            this._lblBroker.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(102, 12);
            this.txtDescription.MaxLength = 75;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(480, 20);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblDescription
            // 
            this._lblDescription.Location = new System.Drawing.Point(17, 12);
            this._lblDescription.Name = "_lblDescription";
            this._lblDescription.Size = new System.Drawing.Size(75, 20);
            this._lblDescription.TabIndex = 45;
            this._lblDescription.Text = "Description";
            this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(98, 357);
            this.txtComments.MaxLength = 300;
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(484, 40);
            this.txtComments.TabIndex = 6;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(13, 357);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(75, 20);
            this._lblComments.TabIndex = 47;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpQuote
            // 
            this.grpQuote.Controls.Add(this.txtTotal);
            this.grpQuote.Controls.Add(this._lblTotal);
            this.grpQuote.Controls.Add(this.txtTSC);
            this.grpQuote.Controls.Add(this._lblTSC);
            this.grpQuote.Controls.Add(this.txtInsurance);
            this.grpQuote.Controls.Add(this._lblInsurance);
            this.grpQuote.Controls.Add(this.txtAccessorials);
            this.grpQuote.Controls.Add(this._lblAccessorials);
            this.grpQuote.Controls.Add(this.txtFSC);
            this.grpQuote.Controls.Add(this._lblFSC);
            this.grpQuote.Controls.Add(this.txtRate);
            this.grpQuote.Controls.Add(this._lblRate);
            this.grpQuote.Controls.Add(this.txtWeight);
            this.grpQuote.Controls.Add(this._blWeight);
            this.grpQuote.Controls.Add(this.txtPallets);
            this.grpQuote.Controls.Add(this._lblPallets);
            this.grpQuote.Location = new System.Drawing.Point(358, 70);
            this.grpQuote.Name = "grpQuote";
            this.grpQuote.Size = new System.Drawing.Size(224, 281);
            this.grpQuote.TabIndex = 5;
            this.grpQuote.TabStop = false;
            this.grpQuote.Text = "Quote";
            // 
            // txtTotal
            // 
            this.txtTotal.Location = new System.Drawing.Point(93, 234);
            this.txtTotal.Name = "txtTotal";
            this.txtTotal.Size = new System.Drawing.Size(100, 20);
            this.txtTotal.TabIndex = 7;
            this.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTotal.TextChanged += new System.EventHandler(this.OnValidateForm);
            this.txtTotal.KeyUp += new System.Windows.Forms.KeyEventHandler(this.OnTotalQuoteKeyUp);
            this.txtTotal.Leave += new System.EventHandler(this.OnTotalQuoteLeave);
            // 
            // _lblTotal
            // 
            this._lblTotal.Location = new System.Drawing.Point(5, 234);
            this._lblTotal.Name = "_lblTotal";
            this._lblTotal.Size = new System.Drawing.Size(75, 20);
            this._lblTotal.TabIndex = 102;
            this._lblTotal.Text = "Total";
            this._lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTSC
            // 
            this.txtTSC.Location = new System.Drawing.Point(93, 181);
            this.txtTSC.Name = "txtTSC";
            this.txtTSC.ReadOnly = true;
            this.txtTSC.Size = new System.Drawing.Size(100, 20);
            this.txtTSC.TabIndex = 6;
            this.txtTSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblTSC
            // 
            this._lblTSC.Location = new System.Drawing.Point(5, 181);
            this._lblTSC.Name = "_lblTSC";
            this._lblTSC.Size = new System.Drawing.Size(75, 20);
            this._lblTSC.TabIndex = 101;
            this._lblTSC.Text = "TSC";
            this._lblTSC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInsurance
            // 
            this.txtInsurance.Location = new System.Drawing.Point(93, 155);
            this.txtInsurance.Name = "txtInsurance";
            this.txtInsurance.ReadOnly = true;
            this.txtInsurance.Size = new System.Drawing.Size(100, 20);
            this.txtInsurance.TabIndex = 5;
            this.txtInsurance.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblInsurance
            // 
            this._lblInsurance.Location = new System.Drawing.Point(5, 155);
            this._lblInsurance.Name = "_lblInsurance";
            this._lblInsurance.Size = new System.Drawing.Size(75, 20);
            this._lblInsurance.TabIndex = 100;
            this._lblInsurance.Text = "Insurance";
            this._lblInsurance.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAccessorials
            // 
            this.txtAccessorials.Location = new System.Drawing.Point(93, 129);
            this.txtAccessorials.Name = "txtAccessorials";
            this.txtAccessorials.ReadOnly = true;
            this.txtAccessorials.Size = new System.Drawing.Size(100, 20);
            this.txtAccessorials.TabIndex = 4;
            this.txtAccessorials.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblAccessorials
            // 
            this._lblAccessorials.Location = new System.Drawing.Point(5, 129);
            this._lblAccessorials.Name = "_lblAccessorials";
            this._lblAccessorials.Size = new System.Drawing.Size(75, 20);
            this._lblAccessorials.TabIndex = 99;
            this._lblAccessorials.Text = "Accessorials";
            this._lblAccessorials.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFSC
            // 
            this.txtFSC.Location = new System.Drawing.Point(93, 103);
            this.txtFSC.Name = "txtFSC";
            this.txtFSC.ReadOnly = true;
            this.txtFSC.Size = new System.Drawing.Size(100, 20);
            this.txtFSC.TabIndex = 3;
            this.txtFSC.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblFSC
            // 
            this._lblFSC.Location = new System.Drawing.Point(5, 103);
            this._lblFSC.Name = "_lblFSC";
            this._lblFSC.Size = new System.Drawing.Size(75, 20);
            this._lblFSC.TabIndex = 98;
            this._lblFSC.Text = "FSC";
            this._lblFSC.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtRate
            // 
            this.txtRate.Location = new System.Drawing.Point(93, 77);
            this.txtRate.Name = "txtRate";
            this.txtRate.ReadOnly = true;
            this.txtRate.Size = new System.Drawing.Size(100, 20);
            this.txtRate.TabIndex = 2;
            this.txtRate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblRate
            // 
            this._lblRate.Location = new System.Drawing.Point(5, 77);
            this._lblRate.Name = "_lblRate";
            this._lblRate.Size = new System.Drawing.Size(75, 20);
            this._lblRate.TabIndex = 97;
            this._lblRate.Text = "Pallet Rate";
            this._lblRate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtWeight
            // 
            this.txtWeight.Location = new System.Drawing.Point(93, 51);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.ReadOnly = true;
            this.txtWeight.Size = new System.Drawing.Size(100, 20);
            this.txtWeight.TabIndex = 1;
            this.txtWeight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _blWeight
            // 
            this._blWeight.Location = new System.Drawing.Point(5, 51);
            this._blWeight.Name = "_blWeight";
            this._blWeight.Size = new System.Drawing.Size(75, 20);
            this._blWeight.TabIndex = 96;
            this._blWeight.Text = "Weight";
            this._blWeight.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPallets
            // 
            this.txtPallets.Location = new System.Drawing.Point(93, 25);
            this.txtPallets.Name = "txtPallets";
            this.txtPallets.ReadOnly = true;
            this.txtPallets.Size = new System.Drawing.Size(100, 20);
            this.txtPallets.TabIndex = 0;
            this.txtPallets.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // _lblPallets
            // 
            this._lblPallets.Location = new System.Drawing.Point(5, 25);
            this._lblPallets.Name = "_lblPallets";
            this._lblPallets.Size = new System.Drawing.Size(75, 20);
            this._lblPallets.TabIndex = 95;
            this._lblPallets.Text = "Pallets";
            this._lblPallets.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpLoadTender
            // 
            this.grpLoadTender.Controls.Add(this.txtLoad);
            this.grpLoadTender.Controls.Add(this.txtDestination);
            this.grpLoadTender.Controls.Add(this.txtOrigin);
            this.grpLoadTender.Controls.Add(this.txtShipDate);
            this.grpLoadTender.Controls.Add(this._lblLoad);
            this.grpLoadTender.Controls.Add(this._lblDestination);
            this.grpLoadTender.Controls.Add(this._lblOrigin);
            this.grpLoadTender.Controls.Add(this._lblShipDate);
            this.grpLoadTender.Location = new System.Drawing.Point(12, 201);
            this.grpLoadTender.Name = "grpLoadTender";
            this.grpLoadTender.Size = new System.Drawing.Size(337, 150);
            this.grpLoadTender.TabIndex = 4;
            this.grpLoadTender.TabStop = false;
            this.grpLoadTender.Text = "Load Tender";
            // 
            // txtLoad
            // 
            this.txtLoad.Location = new System.Drawing.Point(86, 104);
            this.txtLoad.Name = "txtLoad";
            this.txtLoad.ReadOnly = true;
            this.txtLoad.Size = new System.Drawing.Size(129, 20);
            this.txtLoad.TabIndex = 3;
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(86, 78);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.ReadOnly = true;
            this.txtDestination.Size = new System.Drawing.Size(225, 20);
            this.txtDestination.TabIndex = 2;
            // 
            // txtOrigin
            // 
            this.txtOrigin.Location = new System.Drawing.Point(86, 52);
            this.txtOrigin.Name = "txtOrigin";
            this.txtOrigin.ReadOnly = true;
            this.txtOrigin.Size = new System.Drawing.Size(225, 20);
            this.txtOrigin.TabIndex = 1;
            // 
            // txtShipDate
            // 
            this.txtShipDate.Location = new System.Drawing.Point(86, 26);
            this.txtShipDate.Name = "txtShipDate";
            this.txtShipDate.ReadOnly = true;
            this.txtShipDate.Size = new System.Drawing.Size(125, 20);
            this.txtShipDate.TabIndex = 0;
            // 
            // _lblLoad
            // 
            this._lblLoad.Location = new System.Drawing.Point(5, 103);
            this._lblLoad.Name = "_lblLoad";
            this._lblLoad.Size = new System.Drawing.Size(75, 20);
            this._lblLoad.TabIndex = 8;
            this._lblLoad.Text = "Load";
            this._lblLoad.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDestination
            // 
            this._lblDestination.Location = new System.Drawing.Point(5, 77);
            this._lblDestination.Name = "_lblDestination";
            this._lblDestination.Size = new System.Drawing.Size(75, 20);
            this._lblDestination.TabIndex = 7;
            this._lblDestination.Text = "Destination";
            this._lblDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblOrigin
            // 
            this._lblOrigin.Location = new System.Drawing.Point(5, 51);
            this._lblOrigin.Name = "_lblOrigin";
            this._lblOrigin.Size = new System.Drawing.Size(75, 20);
            this._lblOrigin.TabIndex = 6;
            this._lblOrigin.Text = "Origin";
            this._lblOrigin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblShipDate
            // 
            this._lblShipDate.Location = new System.Drawing.Point(5, 25);
            this._lblShipDate.Name = "_lblShipDate";
            this._lblShipDate.Size = new System.Drawing.Size(75, 20);
            this._lblShipDate.TabIndex = 5;
            this._lblShipDate.Text = "Ship Date";
            this._lblShipDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkApproved
            // 
            this.chkApproved.AutoSize = true;
            this.chkApproved.Location = new System.Drawing.Point(98, 411);
            this.chkApproved.Name = "chkApproved";
            this.chkApproved.Size = new System.Drawing.Size(72, 17);
            this.chkApproved.TabIndex = 48;
            this.chkApproved.Text = "Approved";
            this.chkApproved.UseVisualStyleBackColor = true;
            this.chkApproved.CheckedChanged += new System.EventHandler(this.OnApprovedChecked);
            // 
            // dlgLoadTenderQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(594, 446);
            this.Controls.Add(this.chkApproved);
            this.Controls.Add(this.grpLoadTender);
            this.Controls.Add(this.txtBroker);
            this.Controls.Add(this._lblBroker);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblComments);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this._lblDescription);
            this.Controls.Add(this.grpContact);
            this.Controls.Add(this.grpQuote);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgLoadTenderQuote";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Load Tender Quote";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.grpContact.ResumeLayout(false);
            this.grpContact.PerformLayout();
            this.grpQuote.ResumeLayout(false);
            this.grpQuote.PerformLayout();
            this.grpLoadTender.ResumeLayout(false);
            this.grpLoadTender.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.GroupBox grpContact;
        private System.Windows.Forms.MaskedTextBox mskContactPhone;
        private System.Windows.Forms.TextBox txtContactEmail;
        private System.Windows.Forms.Label _lblContactEmail;
        private System.Windows.Forms.Label _lblContactPhone;
        private System.Windows.Forms.TextBox txtContactName;
        private System.Windows.Forms.Label _lblContactName;
        private System.Windows.Forms.TextBox txtBroker;
        private System.Windows.Forms.Label _lblBroker;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.Label _lblDescription;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label _lblComments;
        private System.Windows.Forms.GroupBox grpQuote;
        private System.Windows.Forms.TextBox txtTotal;
        private System.Windows.Forms.Label _lblTotal;
        private System.Windows.Forms.TextBox txtTSC;
        private System.Windows.Forms.Label _lblTSC;
        private System.Windows.Forms.TextBox txtInsurance;
        private System.Windows.Forms.Label _lblInsurance;
        private System.Windows.Forms.TextBox txtAccessorials;
        private System.Windows.Forms.Label _lblAccessorials;
        private System.Windows.Forms.TextBox txtFSC;
        private System.Windows.Forms.Label _lblFSC;
        private System.Windows.Forms.TextBox txtRate;
        private System.Windows.Forms.Label _lblRate;
        private System.Windows.Forms.TextBox txtWeight;
        private System.Windows.Forms.Label _blWeight;
        private System.Windows.Forms.TextBox txtPallets;
        private System.Windows.Forms.Label _lblPallets;
        private System.Windows.Forms.GroupBox grpLoadTender;
        private System.Windows.Forms.TextBox txtLoad;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.TextBox txtOrigin;
        private System.Windows.Forms.TextBox txtShipDate;
        private System.Windows.Forms.Label _lblLoad;
        private System.Windows.Forms.Label _lblDestination;
        private System.Windows.Forms.Label _lblOrigin;
        private System.Windows.Forms.Label _lblShipDate;
        private System.Windows.Forms.CheckBox chkApproved;
    }
}