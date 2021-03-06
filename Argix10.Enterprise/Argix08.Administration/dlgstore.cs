//	File:	dlgstore.cs
//	Author:	J. Heary
//	Date:	04/28/06
//	Desc:	Dialog to create a new store or edit an existing store.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Tsort.Enterprise;

namespace Tsort {
	//
	public class dlgStoreDetail : System.Windows.Forms.Form {
		//Members
		private int mStoreID = 0;
		private bool mParentIsActive = true;
		#region Controls
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit mskPhone;
		private System.Windows.Forms.CheckBox chkStatus;
		private System.Windows.Forms.Label _lblName;
		private System.Windows.Forms.Label _lblNumber;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label _lblFax;
		private System.Windows.Forms.Label lblPhone;
		private Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit mskFax;
		private System.Windows.Forms.Label _lblContact;
		private System.Windows.Forms.TextBox txtContact;
		private System.Windows.Forms.TextBox txtNumber;
		private System.Windows.Forms.Label _lblExt;
		private System.Windows.Forms.TextBox txtExt;
		private System.Windows.Forms.GroupBox fraAddress;
		private System.Windows.Forms.Label _lblZip;
		private System.Windows.Forms.TextBox txtZip;
		private System.Windows.Forms.Label _lblCity;
		private System.Windows.Forms.TextBox txtCity;
		private System.Windows.Forms.Label _lblState;
		private System.Windows.Forms.ComboBox cboStates;
		private System.Windows.Forms.Label _lblLine2;
		private System.Windows.Forms.TextBox txtLine2;
		private System.Windows.Forms.Label _lblLine1;
		private System.Windows.Forms.TextBox txtLine1;
		private Tsort.Enterprise.StoreDS mStoresDS;
		private System.Windows.Forms.Label _lbleMail;
		private System.Windows.Forms.TextBox txteMail;
		private Tsort.Enterprise.StateDS mStatesDS;
		private System.Windows.Forms.Label _lblCountry;
		private System.Windows.Forms.ComboBox cboCountries;
		private Tsort.Enterprise.CountryDS mCountriesDS;
		private System.Windows.Forms.GroupBox fraContact;
		private System.Windows.Forms.DateTimePicker dtpClose;
		private System.Windows.Forms.DateTimePicker dtpOpen;
		private System.Windows.Forms.Label _lblClose;
		private System.Windows.Forms.Label _lblOpen;
		private System.Windows.Forms.TabControl tabDialog;
		private System.Windows.Forms.TabPage tabGeneral;
		private System.Windows.Forms.TabPage tabTrans;
		private System.Windows.Forms.TabPage tabFreight;
		private System.Windows.Forms.GroupBox fraHours;
		private System.Windows.Forms.Label _lblInstructions;
		private System.Windows.Forms.TextBox txtInstructions;
		private System.Windows.Forms.TextBox txtLabelData;
		private System.Windows.Forms.Label _lblLabelData;
		private System.Windows.Forms.TextBox txtSAN;
		private System.Windows.Forms.Label _lblSAN;
		private System.Windows.Forms.Label _lblMnemonic;
		private System.Windows.Forms.TextBox txtMnemonic;
		private System.Windows.Forms.CheckBox chkIsLocationException;
		private System.Windows.Forms.GroupBox fraLocationException;
		private System.Windows.Forms.TextBox txtLocationException;
		private System.Windows.Forms.Label _lblDescription;
		private System.Windows.Forms.CheckBox chkArgix;
		private System.Windows.Forms.GroupBox fraContact_;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		#endregion
		
		//Constants
		private const string CMD_CANCEL = "&Cancel";
		private const string CMD_OK = "O&K";		
		
		//Events
		public event ErrorEventHandler ErrorMessage=null;
		
		//Interface
		public dlgStoreDetail(bool parentIsActive, ref StoreDS store) {
			//Constructor
			try {
				//Required designer support
				InitializeComponent();
				this.btnOk.Text = CMD_OK;
				this.btnCancel.Text = CMD_CANCEL;
				
				//Set mediator service, data, and titlebar caption
				this.mParentIsActive = parentIsActive;
				this.mStoresDS = store;
				if(this.mStoresDS.StoreDetailTable.Count>0) {
					this.mStoreID = this.mStoresDS.StoreDetailTable[0].LocationID;
					this.Text = (this.mStoreID>0) ? "Store (" + this.mStoreID + ")" : "Store (New)";
				}
				else
					this.Text = "Store (Data Unavailable)";
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if( disposing ) { if (components != null) components.Dispose(); } base.Dispose( disposing ); }
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(dlgStoreDetail));
			this._lblName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this._lbleMail = new System.Windows.Forms.Label();
			this.txteMail = new System.Windows.Forms.TextBox();
			this.fraAddress = new System.Windows.Forms.GroupBox();
			this._lblCountry = new System.Windows.Forms.Label();
			this.cboCountries = new System.Windows.Forms.ComboBox();
			this.mCountriesDS = new Tsort.Enterprise.CountryDS();
			this._lblZip = new System.Windows.Forms.Label();
			this.txtZip = new System.Windows.Forms.TextBox();
			this._lblCity = new System.Windows.Forms.Label();
			this.txtCity = new System.Windows.Forms.TextBox();
			this._lblState = new System.Windows.Forms.Label();
			this.cboStates = new System.Windows.Forms.ComboBox();
			this.mStatesDS = new Tsort.Enterprise.StateDS();
			this._lblLine2 = new System.Windows.Forms.Label();
			this.txtLine2 = new System.Windows.Forms.TextBox();
			this._lblLine1 = new System.Windows.Forms.Label();
			this.txtLine1 = new System.Windows.Forms.TextBox();
			this._lblExt = new System.Windows.Forms.Label();
			this.txtExt = new System.Windows.Forms.TextBox();
			this._lblContact = new System.Windows.Forms.Label();
			this.txtContact = new System.Windows.Forms.TextBox();
			this.lblPhone = new System.Windows.Forms.Label();
			this.mskFax = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
			this.chkStatus = new System.Windows.Forms.CheckBox();
			this._lblNumber = new System.Windows.Forms.Label();
			this.txtNumber = new System.Windows.Forms.TextBox();
			this.txtName = new System.Windows.Forms.TextBox();
			this._lblFax = new System.Windows.Forms.Label();
			this.mskPhone = new Infragistics.Win.UltraWinMaskedEdit.UltraMaskedEdit();
			this.mStoresDS = new Tsort.Enterprise.StoreDS();
			this.fraContact = new System.Windows.Forms.GroupBox();
			this.dtpClose = new System.Windows.Forms.DateTimePicker();
			this.dtpOpen = new System.Windows.Forms.DateTimePicker();
			this._lblClose = new System.Windows.Forms.Label();
			this._lblOpen = new System.Windows.Forms.Label();
			this.tabDialog = new System.Windows.Forms.TabControl();
			this.tabGeneral = new System.Windows.Forms.TabPage();
			this.fraContact_ = new System.Windows.Forms.GroupBox();
			this.txtMnemonic = new System.Windows.Forms.TextBox();
			this._lblMnemonic = new System.Windows.Forms.Label();
			this.tabTrans = new System.Windows.Forms.TabPage();
			this.txtInstructions = new System.Windows.Forms.TextBox();
			this._lblInstructions = new System.Windows.Forms.Label();
			this.fraHours = new System.Windows.Forms.GroupBox();
			this.tabFreight = new System.Windows.Forms.TabPage();
			this.chkIsLocationException = new System.Windows.Forms.CheckBox();
			this.fraLocationException = new System.Windows.Forms.GroupBox();
			this._lblDescription = new System.Windows.Forms.Label();
			this.chkArgix = new System.Windows.Forms.CheckBox();
			this.txtLocationException = new System.Windows.Forms.TextBox();
			this.txtLabelData = new System.Windows.Forms.TextBox();
			this._lblLabelData = new System.Windows.Forms.Label();
			this.txtSAN = new System.Windows.Forms.TextBox();
			this._lblSAN = new System.Windows.Forms.Label();
			this.fraAddress.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.mStoresDS)).BeginInit();
			this.tabDialog.SuspendLayout();
			this.tabGeneral.SuspendLayout();
			this.fraContact_.SuspendLayout();
			this.tabTrans.SuspendLayout();
			this.fraHours.SuspendLayout();
			this.tabFreight.SuspendLayout();
			this.fraLocationException.SuspendLayout();
			this.SuspendLayout();
			// 
			// _lblName
			// 
			this._lblName.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblName.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblName.Location = new System.Drawing.Point(159, 12);
			this._lblName.Name = "_lblName";
			this._lblName.Size = new System.Drawing.Size(90, 18);
			this._lblName.TabIndex = 13;
			this._lblName.Text = "Description";
			this._lblName.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancel
			// 
			this.btnCancel.BackColor = System.Drawing.SystemColors.Control;
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(375, 333);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(96, 24);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "&Cancel";
			this.btnCancel.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// btnOk
			// 
			this.btnOk.BackColor = System.Drawing.SystemColors.Control;
			this.btnOk.Enabled = false;
			this.btnOk.Location = new System.Drawing.Point(273, 333);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(96, 24);
			this.btnOk.TabIndex = 0;
			this.btnOk.Text = "&OK";
			this.btnOk.Click += new System.EventHandler(this.OnCmdClick);
			// 
			// _lbleMail
			// 
			this._lbleMail.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lbleMail.Location = new System.Drawing.Point(3, 69);
			this._lbleMail.Name = "_lbleMail";
			this._lbleMail.Size = new System.Drawing.Size(72, 18);
			this._lbleMail.TabIndex = 30;
			this._lbleMail.Text = "eMail";
			this._lbleMail.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txteMail
			// 
			this.txteMail.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txteMail.Location = new System.Drawing.Point(81, 69);
			this.txteMail.Name = "txteMail";
			this.txteMail.Size = new System.Drawing.Size(192, 21);
			this.txteMail.TabIndex = 7;
			this.txteMail.Text = "";
			this.txteMail.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// fraAddress
			// 
			this.fraAddress.Controls.Add(this._lblCountry);
			this.fraAddress.Controls.Add(this.cboCountries);
			this.fraAddress.Controls.Add(this._lblZip);
			this.fraAddress.Controls.Add(this.txtZip);
			this.fraAddress.Controls.Add(this._lblCity);
			this.fraAddress.Controls.Add(this.txtCity);
			this.fraAddress.Controls.Add(this._lblState);
			this.fraAddress.Controls.Add(this.cboStates);
			this.fraAddress.Controls.Add(this._lblLine2);
			this.fraAddress.Controls.Add(this.txtLine2);
			this.fraAddress.Controls.Add(this._lblLine1);
			this.fraAddress.Controls.Add(this.txtLine1);
			this.fraAddress.Location = new System.Drawing.Point(6, 66);
			this.fraAddress.Name = "fraAddress";
			this.fraAddress.Size = new System.Drawing.Size(447, 120);
			this.fraAddress.TabIndex = 2;
			this.fraAddress.TabStop = false;
			this.fraAddress.Text = "Mailing Address";
			// 
			// _lblCountry
			// 
			this._lblCountry.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCountry.Location = new System.Drawing.Point(258, 93);
			this._lblCountry.Name = "_lblCountry";
			this._lblCountry.Size = new System.Drawing.Size(54, 18);
			this._lblCountry.TabIndex = 31;
			this._lblCountry.Text = "Country";
			this._lblCountry.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboCountries
			// 
			this.cboCountries.DataSource = this.mCountriesDS;
			this.cboCountries.DisplayMember = "CountryDetailTable.Country";
			this.cboCountries.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboCountries.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboCountries.Location = new System.Drawing.Point(318, 93);
			this.cboCountries.Name = "cboCountries";
			this.cboCountries.Size = new System.Drawing.Size(120, 21);
			this.cboCountries.TabIndex = 3;
			this.cboCountries.ValueMember = "CountryDetailTable.CountryID";
			this.cboCountries.SelectionChangeCommitted += new System.EventHandler(this.OnCountryChanged);
			// 
			// mCountriesDS
			// 
			this.mCountriesDS.DataSetName = "CountryDS";
			this.mCountriesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblZip
			// 
			this._lblZip.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblZip.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblZip.Location = new System.Drawing.Point(147, 93);
			this._lblZip.Name = "_lblZip";
			this._lblZip.Size = new System.Drawing.Size(24, 18);
			this._lblZip.TabIndex = 29;
			this._lblZip.Text = "Zip";
			this._lblZip.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtZip
			// 
			this.txtZip.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtZip.Location = new System.Drawing.Point(177, 93);
			this.txtZip.MaxLength = 5;
			this.txtZip.Name = "txtZip";
			this.txtZip.Size = new System.Drawing.Size(72, 21);
			this.txtZip.TabIndex = 5;
			this.txtZip.Text = "";
			this.txtZip.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblCity
			// 
			this._lblCity.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblCity.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblCity.Location = new System.Drawing.Point(3, 69);
			this._lblCity.Name = "_lblCity";
			this._lblCity.Size = new System.Drawing.Size(72, 18);
			this._lblCity.TabIndex = 28;
			this._lblCity.Text = "City";
			this._lblCity.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtCity
			// 
			this.txtCity.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtCity.Location = new System.Drawing.Point(81, 69);
			this.txtCity.Name = "txtCity";
			this.txtCity.Size = new System.Drawing.Size(168, 21);
			this.txtCity.TabIndex = 2;
			this.txtCity.Text = "";
			this.txtCity.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblState
			// 
			this._lblState.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblState.Location = new System.Drawing.Point(3, 93);
			this._lblState.Name = "_lblState";
			this._lblState.Size = new System.Drawing.Size(72, 18);
			this._lblState.TabIndex = 20;
			this._lblState.Text = "State";
			this._lblState.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cboStates
			// 
			this.cboStates.DataSource = this.mStatesDS;
			this.cboStates.DisplayMember = "StateListTable.STATE";
			this.cboStates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboStates.ForeColor = System.Drawing.SystemColors.ControlText;
			this.cboStates.Location = new System.Drawing.Point(81, 93);
			this.cboStates.Name = "cboStates";
			this.cboStates.Size = new System.Drawing.Size(60, 21);
			this.cboStates.TabIndex = 4;
			this.cboStates.ValueMember = "StateListTable.STATE";
			this.cboStates.SelectedIndexChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mStatesDS
			// 
			this.mStatesDS.DataSetName = "StateDS";
			this.mStatesDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// _lblLine2
			// 
			this._lblLine2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine2.Location = new System.Drawing.Point(3, 45);
			this._lblLine2.Name = "_lblLine2";
			this._lblLine2.Size = new System.Drawing.Size(72, 18);
			this._lblLine2.TabIndex = 27;
			this._lblLine2.Text = "Line 2";
			this._lblLine2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine2
			// 
			this.txtLine2.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine2.Location = new System.Drawing.Point(81, 45);
			this.txtLine2.Name = "txtLine2";
			this.txtLine2.Size = new System.Drawing.Size(357, 21);
			this.txtLine2.TabIndex = 1;
			this.txtLine2.Text = "";
			this.txtLine2.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblLine1
			// 
			this._lblLine1.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLine1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLine1.Location = new System.Drawing.Point(3, 21);
			this._lblLine1.Name = "_lblLine1";
			this._lblLine1.Size = new System.Drawing.Size(72, 18);
			this._lblLine1.TabIndex = 26;
			this._lblLine1.Text = "Line 1";
			this._lblLine1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtLine1
			// 
			this.txtLine1.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLine1.Location = new System.Drawing.Point(81, 21);
			this.txtLine1.Name = "txtLine1";
			this.txtLine1.Size = new System.Drawing.Size(357, 21);
			this.txtLine1.TabIndex = 0;
			this.txtLine1.Text = "";
			this.txtLine1.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblExt
			// 
			this._lblExt.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblExt.Location = new System.Drawing.Point(195, 45);
			this._lblExt.Name = "_lblExt";
			this._lblExt.Size = new System.Drawing.Size(24, 18);
			this._lblExt.TabIndex = 27;
			this._lblExt.Text = "Ext";
			this._lblExt.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtExt
			// 
			this.txtExt.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtExt.Location = new System.Drawing.Point(225, 45);
			this.txtExt.Name = "txtExt";
			this.txtExt.Size = new System.Drawing.Size(48, 21);
			this.txtExt.TabIndex = 5;
			this.txtExt.Text = "";
			this.txtExt.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblContact
			// 
			this._lblContact.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblContact.Location = new System.Drawing.Point(3, 21);
			this._lblContact.Name = "_lblContact";
			this._lblContact.Size = new System.Drawing.Size(72, 18);
			this._lblContact.TabIndex = 23;
			this._lblContact.Text = "Contact";
			this._lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtContact
			// 
			this.txtContact.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtContact.Location = new System.Drawing.Point(81, 21);
			this.txtContact.Name = "txtContact";
			this.txtContact.Size = new System.Drawing.Size(192, 21);
			this.txtContact.TabIndex = 6;
			this.txtContact.Text = "";
			this.txtContact.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// lblPhone
			// 
			this.lblPhone.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPhone.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.lblPhone.Location = new System.Drawing.Point(3, 45);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(72, 18);
			this.lblPhone.TabIndex = 22;
			this.lblPhone.Text = "Phone #";
			this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mskFax
			// 
			this.mskFax.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
			this.mskFax.Location = new System.Drawing.Point(339, 45);
			this.mskFax.Name = "mskFax";
			this.mskFax.Size = new System.Drawing.Size(96, 21);
			this.mskFax.TabIndex = 8;
			this.mskFax.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// chkStatus
			// 
			this.chkStatus.Checked = true;
			this.chkStatus.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkStatus.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.chkStatus.Location = new System.Drawing.Point(252, 36);
			this.chkStatus.Name = "chkStatus";
			this.chkStatus.Size = new System.Drawing.Size(96, 18);
			this.chkStatus.TabIndex = 3;
			this.chkStatus.Text = "Active";
			this.chkStatus.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblNumber
			// 
			this._lblNumber.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblNumber.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblNumber.Location = new System.Drawing.Point(9, 12);
			this._lblNumber.Name = "_lblNumber";
			this._lblNumber.Size = new System.Drawing.Size(72, 18);
			this._lblNumber.TabIndex = 15;
			this._lblNumber.Text = "Number";
			this._lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtNumber
			// 
			this.txtNumber.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtNumber.Location = new System.Drawing.Point(87, 12);
			this.txtNumber.Name = "txtNumber";
			this.txtNumber.Size = new System.Drawing.Size(72, 21);
			this.txtNumber.TabIndex = 0;
			this.txtNumber.Text = "";
			this.txtNumber.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtName
			// 
			this.txtName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtName.Location = new System.Drawing.Point(252, 12);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(192, 21);
			this.txtName.TabIndex = 1;
			this.txtName.Text = "";
			this.txtName.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblFax
			// 
			this._lblFax.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblFax.Location = new System.Drawing.Point(285, 45);
			this._lblFax.Name = "_lblFax";
			this._lblFax.Size = new System.Drawing.Size(48, 18);
			this._lblFax.TabIndex = 17;
			this._lblFax.Text = "Fax #";
			this._lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// mskPhone
			// 
			this.mskPhone.EditAs = Infragistics.Win.UltraWinMaskedEdit.EditAsType.UseSpecifiedMask;
			this.mskPhone.Location = new System.Drawing.Point(81, 45);
			this.mskPhone.Name = "mskPhone";
			this.mskPhone.Size = new System.Drawing.Size(96, 21);
			this.mskPhone.TabIndex = 4;
			this.mskPhone.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// mStoresDS
			// 
			this.mStoresDS.DataSetName = "StoreDS";
			this.mStoresDS.Locale = new System.Globalization.CultureInfo("en-US");
			// 
			// fraContact
			// 
			this.fraContact.Location = new System.Drawing.Point(0, 0);
			this.fraContact.Name = "fraContact";
			this.fraContact.Size = new System.Drawing.Size(138, 60);
			this.fraContact.TabIndex = 31;
			this.fraContact.TabStop = false;
			this.fraContact.Text = "Contact";
			// 
			// dtpClose
			// 
			this.dtpClose.CalendarForeColor = System.Drawing.SystemColors.ControlText;
			this.dtpClose.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpClose.Location = new System.Drawing.Point(228, 21);
			this.dtpClose.Name = "dtpClose";
			this.dtpClose.ShowUpDown = true;
			this.dtpClose.Size = new System.Drawing.Size(102, 21);
			this.dtpClose.TabIndex = 34;
			this.dtpClose.Value = new System.DateTime(2003, 4, 14, 0, 0, 0, 0);
			this.dtpClose.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// dtpOpen
			// 
			this.dtpOpen.CalendarForeColor = System.Drawing.SystemColors.ControlText;
			this.dtpOpen.Format = System.Windows.Forms.DateTimePickerFormat.Time;
			this.dtpOpen.Location = new System.Drawing.Point(66, 21);
			this.dtpOpen.Name = "dtpOpen";
			this.dtpOpen.ShowUpDown = true;
			this.dtpOpen.Size = new System.Drawing.Size(102, 21);
			this.dtpOpen.TabIndex = 33;
			this.dtpOpen.Value = new System.DateTime(2003, 4, 14, 0, 0, 0, 0);
			this.dtpOpen.ValueChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblClose
			// 
			this._lblClose.Location = new System.Drawing.Point(174, 21);
			this._lblClose.Name = "_lblClose";
			this._lblClose.Size = new System.Drawing.Size(48, 18);
			this._lblClose.TabIndex = 32;
			this._lblClose.Text = "Close";
			this._lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// _lblOpen
			// 
			this._lblOpen.Location = new System.Drawing.Point(12, 21);
			this._lblOpen.Name = "_lblOpen";
			this._lblOpen.Size = new System.Drawing.Size(48, 18);
			this._lblOpen.TabIndex = 31;
			this._lblOpen.Text = "Open";
			this._lblOpen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabDialog
			// 
			this.tabDialog.Controls.Add(this.tabGeneral);
			this.tabDialog.Controls.Add(this.tabTrans);
			this.tabDialog.Controls.Add(this.tabFreight);
			this.tabDialog.Location = new System.Drawing.Point(3, 3);
			this.tabDialog.Name = "tabDialog";
			this.tabDialog.SelectedIndex = 0;
			this.tabDialog.Size = new System.Drawing.Size(468, 324);
			this.tabDialog.TabIndex = 1;
			// 
			// tabGeneral
			// 
			this.tabGeneral.BackColor = System.Drawing.SystemColors.Control;
			this.tabGeneral.Controls.Add(this.fraContact_);
			this.tabGeneral.Controls.Add(this.txtMnemonic);
			this.tabGeneral.Controls.Add(this._lblMnemonic);
			this.tabGeneral.Controls.Add(this.txtName);
			this.tabGeneral.Controls.Add(this.chkStatus);
			this.tabGeneral.Controls.Add(this._lblName);
			this.tabGeneral.Controls.Add(this.fraAddress);
			this.tabGeneral.Controls.Add(this.txtNumber);
			this.tabGeneral.Controls.Add(this._lblNumber);
			this.tabGeneral.Location = new System.Drawing.Point(4, 22);
			this.tabGeneral.Name = "tabGeneral";
			this.tabGeneral.Size = new System.Drawing.Size(460, 298);
			this.tabGeneral.TabIndex = 0;
			this.tabGeneral.Text = "General";
			this.tabGeneral.ToolTipText = "General information";
			// 
			// fraContact_
			// 
			this.fraContact_.Controls.Add(this._lbleMail);
			this.fraContact_.Controls.Add(this.txteMail);
			this.fraContact_.Controls.Add(this._lblFax);
			this.fraContact_.Controls.Add(this.mskPhone);
			this.fraContact_.Controls.Add(this._lblContact);
			this.fraContact_.Controls.Add(this.txtContact);
			this.fraContact_.Controls.Add(this.lblPhone);
			this.fraContact_.Controls.Add(this.mskFax);
			this.fraContact_.Controls.Add(this._lblExt);
			this.fraContact_.Controls.Add(this.txtExt);
			this.fraContact_.Location = new System.Drawing.Point(6, 192);
			this.fraContact_.Name = "fraContact_";
			this.fraContact_.Size = new System.Drawing.Size(447, 96);
			this.fraContact_.TabIndex = 34;
			this.fraContact_.TabStop = false;
			this.fraContact_.Text = "Contact";
			// 
			// txtMnemonic
			// 
			this.txtMnemonic.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtMnemonic.Location = new System.Drawing.Point(87, 36);
			this.txtMnemonic.Name = "txtMnemonic";
			this.txtMnemonic.Size = new System.Drawing.Size(36, 21);
			this.txtMnemonic.TabIndex = 2;
			this.txtMnemonic.Text = "";
			this.txtMnemonic.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblMnemonic
			// 
			this._lblMnemonic.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblMnemonic.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblMnemonic.Location = new System.Drawing.Point(6, 36);
			this._lblMnemonic.Name = "_lblMnemonic";
			this._lblMnemonic.Size = new System.Drawing.Size(72, 18);
			this._lblMnemonic.TabIndex = 33;
			this._lblMnemonic.Text = "Mnemonic";
			this._lblMnemonic.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tabTrans
			// 
			this.tabTrans.Controls.Add(this.txtInstructions);
			this.tabTrans.Controls.Add(this._lblInstructions);
			this.tabTrans.Controls.Add(this.fraHours);
			this.tabTrans.Location = new System.Drawing.Point(4, 22);
			this.tabTrans.Name = "tabTrans";
			this.tabTrans.Size = new System.Drawing.Size(460, 298);
			this.tabTrans.TabIndex = 1;
			this.tabTrans.Text = "Transportation";
			this.tabTrans.ToolTipText = "Transportation related information";
			// 
			// txtInstructions
			// 
			this.txtInstructions.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtInstructions.Location = new System.Drawing.Point(6, 102);
			this.txtInstructions.Multiline = true;
			this.txtInstructions.Name = "txtInstructions";
			this.txtInstructions.Size = new System.Drawing.Size(444, 60);
			this.txtInstructions.TabIndex = 37;
			this.txtInstructions.Text = "";
			this.txtInstructions.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblInstructions
			// 
			this._lblInstructions.Location = new System.Drawing.Point(12, 84);
			this._lblInstructions.Name = "_lblInstructions";
			this._lblInstructions.Size = new System.Drawing.Size(144, 18);
			this._lblInstructions.TabIndex = 36;
			this._lblInstructions.Text = "Special Instructions";
			this._lblInstructions.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fraHours
			// 
			this.fraHours.Controls.Add(this.dtpOpen);
			this.fraHours.Controls.Add(this._lblOpen);
			this.fraHours.Controls.Add(this._lblClose);
			this.fraHours.Controls.Add(this.dtpClose);
			this.fraHours.Location = new System.Drawing.Point(6, 12);
			this.fraHours.Name = "fraHours";
			this.fraHours.Size = new System.Drawing.Size(444, 60);
			this.fraHours.TabIndex = 35;
			this.fraHours.TabStop = false;
			this.fraHours.Text = "Store Hours";
			// 
			// tabFreight
			// 
			this.tabFreight.Controls.Add(this.chkIsLocationException);
			this.tabFreight.Controls.Add(this.fraLocationException);
			this.tabFreight.Controls.Add(this.txtLabelData);
			this.tabFreight.Controls.Add(this._lblLabelData);
			this.tabFreight.Controls.Add(this.txtSAN);
			this.tabFreight.Controls.Add(this._lblSAN);
			this.tabFreight.Location = new System.Drawing.Point(4, 22);
			this.tabFreight.Name = "tabFreight";
			this.tabFreight.Size = new System.Drawing.Size(460, 298);
			this.tabFreight.TabIndex = 2;
			this.tabFreight.Text = "Freight";
			this.tabFreight.ToolTipText = "Freight related information";
			// 
			// chkIsLocationException
			// 
			this.chkIsLocationException.Location = new System.Drawing.Point(13, 114);
			this.chkIsLocationException.Name = "chkIsLocationException";
			this.chkIsLocationException.Size = new System.Drawing.Size(148, 20);
			this.chkIsLocationException.TabIndex = 30;
			this.chkIsLocationException.Text = "Is Location Exception";
			this.chkIsLocationException.CheckedChanged += new System.EventHandler(this.OnLocationExceptionChecked);
			// 
			// fraLocationException
			// 
			this.fraLocationException.Controls.Add(this._lblDescription);
			this.fraLocationException.Controls.Add(this.chkArgix);
			this.fraLocationException.Controls.Add(this.txtLocationException);
			this.fraLocationException.Location = new System.Drawing.Point(6, 138);
			this.fraLocationException.Name = "fraLocationException";
			this.fraLocationException.Size = new System.Drawing.Size(447, 72);
			this.fraLocationException.TabIndex = 29;
			this.fraLocationException.TabStop = false;
			this.fraLocationException.Text = "Location Exception";
			// 
			// _lblDescription
			// 
			this._lblDescription.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblDescription.Location = new System.Drawing.Point(9, 21);
			this._lblDescription.Name = "_lblDescription";
			this._lblDescription.Size = new System.Drawing.Size(78, 21);
			this._lblDescription.TabIndex = 28;
			this._lblDescription.Text = "Description";
			this._lblDescription.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// chkArgix
			// 
			this.chkArgix.Location = new System.Drawing.Point(90, 48);
			this.chkArgix.Name = "chkArgix";
			this.chkArgix.Size = new System.Drawing.Size(117, 20);
			this.chkArgix.TabIndex = 27;
			this.chkArgix.Text = "Argix Use Only";
			this.chkArgix.CheckedChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtLocationException
			// 
			this.txtLocationException.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLocationException.Location = new System.Drawing.Point(91, 21);
			this.txtLocationException.Name = "txtLocationException";
			this.txtLocationException.Size = new System.Drawing.Size(347, 21);
			this.txtLocationException.TabIndex = 26;
			this.txtLocationException.Text = "";
			this.txtLocationException.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// txtLabelData
			// 
			this.txtLabelData.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtLabelData.Location = new System.Drawing.Point(12, 78);
			this.txtLabelData.Name = "txtLabelData";
			this.txtLabelData.Size = new System.Drawing.Size(432, 21);
			this.txtLabelData.TabIndex = 16;
			this.txtLabelData.Text = "";
			this.txtLabelData.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblLabelData
			// 
			this._lblLabelData.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblLabelData.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblLabelData.Location = new System.Drawing.Point(12, 57);
			this._lblLabelData.Name = "_lblLabelData";
			this._lblLabelData.Size = new System.Drawing.Size(96, 18);
			this._lblLabelData.TabIndex = 18;
			this._lblLabelData.Text = "User Label Data";
			this._lblLabelData.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSAN
			// 
			this.txtSAN.ForeColor = System.Drawing.SystemColors.ControlText;
			this.txtSAN.Location = new System.Drawing.Point(12, 30);
			this.txtSAN.Name = "txtSAN";
			this.txtSAN.Size = new System.Drawing.Size(96, 21);
			this.txtSAN.TabIndex = 17;
			this.txtSAN.Text = "";
			this.txtSAN.TextChanged += new System.EventHandler(this.ValidateForm);
			// 
			// _lblSAN
			// 
			this._lblSAN.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this._lblSAN.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
			this._lblSAN.Location = new System.Drawing.Point(12, 12);
			this._lblSAN.Name = "_lblSAN";
			this._lblSAN.Size = new System.Drawing.Size(96, 18);
			this._lblSAN.TabIndex = 19;
			this._lblSAN.Text = "SAN";
			this._lblSAN.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dlgStoreDetail
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(474, 359);
			this.Controls.Add(this.tabDialog);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.ForeColor = System.Drawing.SystemColors.ControlText;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "dlgStoreDetail";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Store Details";
			this.Load += new System.EventHandler(this.OnFormLoad);
			this.fraAddress.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.mCountriesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mStatesDS)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.mStoresDS)).EndInit();
			this.tabDialog.ResumeLayout(false);
			this.tabGeneral.ResumeLayout(false);
			this.fraContact_.ResumeLayout(false);
			this.tabTrans.ResumeLayout(false);
			this.fraHours.ResumeLayout(false);
			this.tabFreight.ResumeLayout(false);
			this.fraLocationException.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void OnFormLoad(object sender, System.EventArgs e) {
			//Initialize controls - set default values
			this.Cursor = Cursors.WaitCursor;
			try {			
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Get selection lists
				this.mCountriesDS.Merge(EnterpriseFactory.GetCountries());
				this.mStatesDS.Merge(EnterpriseFactory.GetStates());
				
				//General
				this.txtNumber.MaxLength = 8;
				this.txtNumber.Text = this.mStoresDS.StoreDetailTable[0].Number;
				this.txtNumber.Enabled = (mStoreID==0);
				this.txtName.MaxLength = 30;
				this.txtName.Text = this.mStoresDS.StoreDetailTable[0].Description;
				this.txtMnemonic.MaxLength = 3;
				this.txtMnemonic.Text = "";
				if(!this.mStoresDS.StoreDetailTable[0].IsMnemonicNull())
					this.txtMnemonic.Text = this.mStoresDS.StoreDetailTable[0].Mnemonic;
				
				this.txtLine1.MaxLength = 40;
				this.txtLine1.Text = this.mStoresDS.AddressDetailTable[0].AddressLine1;
				this.txtLine2.MaxLength = 40;
				if(!this.mStoresDS.AddressDetailTable[0].IsAddressLine2Null())
					this.txtLine2.Text = this.mStoresDS.AddressDetailTable[0].AddressLine2;
				this.txtCity.MaxLength = 40;
				this.txtCity.Text = this.mStoresDS.AddressDetailTable[0].City;
				//if(this.mStoresDS.StoreDetailTable[0].IsStateOrProvinceNull()) 
				//	this.cboStates.SelectedIndex = 0;
				//else
				this.cboStates.SelectedValue = this.mStoresDS.AddressDetailTable[0].StateOrProvince;
				this.cboStates.Enabled = (this.cboStates.Items.Count>0);
				this.txtZip.MaxLength = 15;
				//if(!this.mStoresDS.StoreDetailTable[0].IsPostalCodeNull())
				this.txtZip.Text = this.mStoresDS.AddressDetailTable[0].PostalCode;
				if(this.mStoresDS.AddressDetailTable[0].CountryID==0) 
					this.cboCountries.SelectedIndex = 0;
				else
					this.cboCountries.SelectedValue = this.mStoresDS.AddressDetailTable[0].CountryID;
				this.cboCountries.Enabled = (this.cboCountries.Items.Count>0);
				
				this.txtContact.MaxLength = 30;
				if(!this.mStoresDS.StoreDetailTable[0].IsContactNameNull())
					this.txtContact.Text = this.mStoresDS.StoreDetailTable[0].ContactName;
				this.mskPhone.InputMask = "###-###-####";
				if(!this.mStoresDS.StoreDetailTable[0].IsPhoneNull())
					this.mskPhone.Value = this.mStoresDS.StoreDetailTable[0].Phone;
				this.txtExt.MaxLength = 4;
				if(!this.mStoresDS.StoreDetailTable[0].IsExtensionNull())
					this.txtExt.Text = this.mStoresDS.StoreDetailTable[0].Extension;
				this.mskFax.InputMask = "###-###-####";
				if(!this.mStoresDS.StoreDetailTable[0].IsFaxNull())
					this.mskFax.Value = this.mStoresDS.StoreDetailTable[0].Fax;
				if(!this.mStoresDS.StoreDetailTable[0].IsEmailNull())
					this.txteMail.Text = this.mStoresDS.StoreDetailTable[0].Email;
				
				this.chkStatus.Checked = this.mStoresDS.StoreDetailTable[0].IsActive;
				if(!mParentIsActive) {
					//If parent is inactive: 1. Status MUST be inactive for new
					//					     2. Status cannot be changed for new or existing
					if(mStoreID==0) this.chkStatus.Checked = false;
					this.chkStatus.Enabled = false;
				}
				
				//Transporatation
				if(!this.mStoresDS.StoreDetailTable[0].IsOpenTimeNull())
					this.dtpOpen.Value = this.mStoresDS.StoreDetailTable[0].OpenTime;
				if(!this.mStoresDS.StoreDetailTable[0].IsCloseTimeNull())
					this.dtpClose.Value = this.mStoresDS.StoreDetailTable[0].CloseTime;
				this.txtInstructions.MaxLength = 70;
				this.txtInstructions.Text = "";
				if(!this.mStoresDS.StoreDetailTable[0].IsSpecialInstructionsNull())
					this.txtInstructions.Text = this.mStoresDS.StoreDetailTable[0].SpecialInstructions;					
				
				//Freight
				this.txtSAN.MaxLength = 7;
				this.txtSAN.Text = "";
				if(!this.mStoresDS.StoreDetailTable[0].IsSanNumberNull())
					this.txtSAN.Text = this.mStoresDS.StoreDetailTable[0].SanNumber;
				this.txtLabelData.MaxLength = 25;
				this.txtLabelData.Text = "";
				if(!this.mStoresDS.StoreDetailTable[0].IsUserLabelDataNull())
					this.txtLabelData.Text = this.mStoresDS.StoreDetailTable[0].UserLabelData;
				if(!this.mStoresDS.StoreDetailTable[0].IsExceptionNull()) {
					this.chkIsLocationException.Checked = (this.mStoresDS.StoreDetailTable[0].Exception=="");
					this.chkIsLocationException.Checked = (this.mStoresDS.StoreDetailTable[0].Exception!="");
				}
				else
					OnLocationExceptionChecked(null, null);
			}
			catch(Exception ex) { reportError(ex); }
			finally { this.btnOk.Enabled = false; this.Cursor = Cursors.Default; }
		}
		private void OnCountryChanged(object sender, System.EventArgs e) {
			//Event handler for user changing country selection
			try {
				//Update state list
				this.cboStates.Enabled = (this.cboCountries.Text=="USA" && this.cboStates.Items.Count>0);
				this.ValidateForm(null, null);
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnLocationExceptionChecked(object sender, System.EventArgs e) {
			//
			try {
				if(this.chkIsLocationException.Checked == true) {
					//Load data
					this.fraLocationException.Enabled = true;
					this.txtLocationException.Text = "";
					if(!this.mStoresDS.StoreDetailTable[0].IsExceptionNull())
						this.txtLocationException.Text = this.mStoresDS.StoreDetailTable[0].Exception;
					this.chkArgix.Checked = false;
					if(!this.mStoresDS.StoreDetailTable[0].IsIsArgixUseNull())
						this.chkArgix.Checked = this.mStoresDS.StoreDetailTable[0].IsArgixUse;
				}
				else {
					//Clear fields
					this.fraLocationException.Enabled = false;
					this.txtLocationException.Text = "";
					this.chkArgix.Checked = false;
				}	
			}	 
			catch(Exception ex) { reportError(ex); }
			this.ValidateForm(null, null);
		}
		#region User Services: ValidateForm(), OnCmdClick()
		private void ValidateForm(object sender, System.EventArgs e) {
			//Event handler for changes oin form data
			try {
				if(this.mStoresDS.StoreDetailTable.Count>0) {
					//Enable OK service if details have valid changes
					this.btnOk.Enabled = (	this.txtNumber.Text!="" && this.txtName.Text!="" && 
						this.txtLine1.Text!="" && this.txtCity.Text!="" && 
						this.cboStates.Text!="" && this.txtZip.Text!="" && this.cboCountries.Text!="" && 
						this.mskPhone.Value!=System.DBNull.Value &&
						(!this.chkIsLocationException.Checked) || (this.txtLocationException.Text!=""));
				}
			} 
			catch(Exception ex) { reportError(ex); }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Text) {
					case CMD_CANCEL:
						//Close the dialog
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case CMD_OK:
						//General
						this.Cursor = Cursors.WaitCursor;
						this.mStoresDS.StoreDetailTable[0].Number = this.txtNumber.Text;
						this.mStoresDS.StoreDetailTable[0].Description = this.txtName.Text;
						if(this.txtMnemonic.Text!="")
							this.mStoresDS.StoreDetailTable[0].Mnemonic = this.txtMnemonic.Text;
						else
							this.mStoresDS.StoreDetailTable[0].SetMnemonicNull();
						
						this.mStoresDS.AddressDetailTable[0].AddressLine1 = this.txtLine1.Text;
						if(this.txtLine2.Text!="")
							this.mStoresDS.AddressDetailTable[0].AddressLine2 = this.txtLine2.Text;
						else
							this.mStoresDS.AddressDetailTable[0].SetAddressLine2Null();
						this.mStoresDS.AddressDetailTable[0].City = this.txtCity.Text;
						this.mStoresDS.AddressDetailTable[0].StateOrProvince = this.cboStates.SelectedValue.ToString();
						this.mStoresDS.AddressDetailTable[0].PostalCode = this.txtZip.Text;
						this.mStoresDS.AddressDetailTable[0].CountryID = (int)this.cboCountries.SelectedValue;
						
						if(this.txtContact.Text!="")
							this.mStoresDS.StoreDetailTable[0].ContactName = this.txtContact.Text;
						else
							this.mStoresDS.StoreDetailTable[0].SetContactNameNull();
						this.mStoresDS.StoreDetailTable[0].Phone = this.mskPhone.Value.ToString();
						if(this.txtExt.Text!="")
							this.mStoresDS.StoreDetailTable[0].Extension = this.txtExt.Text;
						else
							this.mStoresDS.StoreDetailTable[0].SetExtensionNull();
						if(this.mskFax.Value!=System.DBNull.Value)
							this.mStoresDS.StoreDetailTable[0].Fax = this.mskFax.Value.ToString();
						else
							this.mStoresDS.StoreDetailTable[0].SetFaxNull();
						if(this.txteMail.Text!="")
							this.mStoresDS.StoreDetailTable[0].Email = this.txteMail.Text;
						else
							this.mStoresDS.StoreDetailTable[0].SetEmailNull();
						this.mStoresDS.StoreDetailTable[0].IsActive = this.chkStatus.Checked;
						
						//Transportation
						if(this.fraHours.Enabled) {
							this.mStoresDS.StoreDetailTable[0].OpenTime = this.dtpOpen.Value;
							this.mStoresDS.StoreDetailTable[0].CloseTime = this.dtpClose.Value;
						}
						else {
							this.mStoresDS.StoreDetailTable[0].SetOpenTimeNull();
							this.mStoresDS.StoreDetailTable[0].SetCloseTimeNull();
						}
						if(this.txtInstructions.Text!="")
							this.mStoresDS.StoreDetailTable[0].SpecialInstructions = this.txtInstructions.Text;					
						else
							this.mStoresDS.StoreDetailTable[0].SetSpecialInstructionsNull();
						
						//Freight
						if(this.txtSAN.Text!="")
							this.mStoresDS.StoreDetailTable[0].SanNumber = this.txtSAN.Text;
						else
							this.mStoresDS.StoreDetailTable[0].SetSanNumberNull();
						if(this.txtLabelData.Text!="")
							this.mStoresDS.StoreDetailTable[0].UserLabelData = this.txtLabelData.Text;					
						else
							this.mStoresDS.StoreDetailTable[0].SetUserLabelDataNull();
						if(this.chkIsLocationException.Checked) {
							this.mStoresDS.StoreDetailTable[0].Exception = this.txtLocationException.Text;
							this.mStoresDS.StoreDetailTable[0].IsArgixUse = this.chkArgix.Checked;
						}
						else {
							this.mStoresDS.StoreDetailTable[0].SetExceptionNull();
							this.mStoresDS.StoreDetailTable[0].SetIsArgixUseNull();
						}
						
						this.mStoresDS.AcceptChanges();
						this.DialogResult = DialogResult.OK;
						this.Close();
						break;
				}
			} 
			catch(Exception ex) { reportError(ex); }
			finally { this.Cursor = Cursors.Default; }
		}
		#endregion
		#region Local Services: reportError()
		private void reportError(Exception ex) { reportError(ex, "", "", ""); }
		private void reportError(Exception ex, string keyword1, string keyword2, string keyword3) { 
			if(this.ErrorMessage != null) this.ErrorMessage(this, new ErrorEventArgs(ex,keyword1,keyword2,keyword3));
		}
		#endregion
	}
}
