using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Argix.Security;

namespace Argix.Freight { 
	//
    public class dlgClientInboundFreight:System.Windows.Forms.Form {
		//Members
        private DispatchDataset.ClientInboundScheduleTableRow mAppt = null;
        private bool mIsNewSchedule = true,mIsTemplate = false;

        #region Controls

        private System.Windows.Forms.Label _lblShipper;
		private System.Windows.Forms.DateTimePicker dtpScheduledArrival;
		private System.Windows.Forms.Label _lblScheduledArrival;
        private System.Windows.Forms.Label _lblComments;
		private System.Windows.Forms.Label _lblAmount;
		private System.Windows.Forms.TextBox txtAmount;
		private System.Windows.Forms.ComboBox cboAmountType;
        private System.Windows.Forms.TextBox txtComments;
        private System.Windows.Forms.Label _lblConsignee;
        private System.Windows.Forms.Label _lblID;
        private Button btnOk;
        private Button btnCancel;
        private Label lblID;
        private DateTimePicker dtpScheduleDate;
        private Label _lblScheduleDate;
        private DateTimePicker dtpActualArrival;
        private Label _lblActualArrival;
        private Label _lblSortDate;
        private DateTimePicker dtpSortDate;
        private CheckBox chkIsLiveUnload;
        private TextBox txtTDSCreatedBy;
        private Label _lblTDSCreatedBy;
        private TextBox txtTDSNumber;
        private Label _lblTDSNumber;
        private ComboBox cboShipper;
        private ComboBox cboConsignee;
        private GroupBox grpDispatch;
        private ComboBox cboDriver;
        private ComboBox cboCarrier;
        private Label _lblCarrier;
        private Label _lblDriver;
        private CheckBox chkConfirmed;
        private GroupBox grpGate;
        private TextBox txtTrailerNumber;
        private Label _lblTrailerNumber;
        private DispatchDataset mCarriers;
        private DispatchDataset mDrivers;
        private DispatchDataset mLocations;
        private DispatchDataset mConsignees;
        private Label _lblOf;
        private ComboBox cboFreightType;
		private System.ComponentModel.IContainer components = null;
		#endregion

        public dlgClientInboundFreight(DispatchDataset.ClientInboundScheduleTableRow freight,bool isTemplate=false) {
			//Constructor
			try {
				InitializeComponent();
                this.mAppt = freight;
                this.mIsNewSchedule = this.mAppt.IsIDNull() || (!this.mAppt.IsIDNull() && this.mAppt.ID == 0);
                this.mIsTemplate = isTemplate;
			} 
			catch(Exception ex) { throw new ApplicationException(ex.Message, ex); }
		}
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
        #region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            this.txtTDSCreatedBy = new System.Windows.Forms.TextBox();
            this._lblTDSCreatedBy = new System.Windows.Forms.Label();
            this.txtTDSNumber = new System.Windows.Forms.TextBox();
            this._lblTDSNumber = new System.Windows.Forms.Label();
            this.chkIsLiveUnload = new System.Windows.Forms.CheckBox();
            this._lblSortDate = new System.Windows.Forms.Label();
            this.dtpSortDate = new System.Windows.Forms.DateTimePicker();
            this.dtpActualArrival = new System.Windows.Forms.DateTimePicker();
            this._lblActualArrival = new System.Windows.Forms.Label();
            this._lblShipper = new System.Windows.Forms.Label();
            this.dtpScheduledArrival = new System.Windows.Forms.DateTimePicker();
            this._lblScheduledArrival = new System.Windows.Forms.Label();
            this._lblComments = new System.Windows.Forms.Label();
            this._lblAmount = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.cboAmountType = new System.Windows.Forms.ComboBox();
            this.txtComments = new System.Windows.Forms.TextBox();
            this._lblConsignee = new System.Windows.Forms.Label();
            this._lblID = new System.Windows.Forms.Label();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblID = new System.Windows.Forms.Label();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this._lblScheduleDate = new System.Windows.Forms.Label();
            this.cboShipper = new System.Windows.Forms.ComboBox();
            this.mLocations = new Argix.DispatchDataset();
            this.cboConsignee = new System.Windows.Forms.ComboBox();
            this.mConsignees = new Argix.DispatchDataset();
            this.grpDispatch = new System.Windows.Forms.GroupBox();
            this.cboDriver = new System.Windows.Forms.ComboBox();
            this.mDrivers = new Argix.DispatchDataset();
            this.cboCarrier = new System.Windows.Forms.ComboBox();
            this.mCarriers = new Argix.DispatchDataset();
            this._lblCarrier = new System.Windows.Forms.Label();
            this._lblDriver = new System.Windows.Forms.Label();
            this.chkConfirmed = new System.Windows.Forms.CheckBox();
            this.grpGate = new System.Windows.Forms.GroupBox();
            this.txtTrailerNumber = new System.Windows.Forms.TextBox();
            this._lblTrailerNumber = new System.Windows.Forms.Label();
            this._lblOf = new System.Windows.Forms.Label();
            this.cboFreightType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mLocations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mConsignees)).BeginInit();
            this.grpDispatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriers)).BeginInit();
            this.grpGate.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtTDSCreatedBy
            // 
            this.txtTDSCreatedBy.Location = new System.Drawing.Point(90, 83);
            this.txtTDSCreatedBy.Name = "txtTDSCreatedBy";
            this.txtTDSCreatedBy.Size = new System.Drawing.Size(106, 20);
            this.txtTDSCreatedBy.TabIndex = 2;
            // 
            // _lblTDSCreatedBy
            // 
            this._lblTDSCreatedBy.Location = new System.Drawing.Point(5, 83);
            this._lblTDSCreatedBy.Name = "_lblTDSCreatedBy";
            this._lblTDSCreatedBy.Size = new System.Drawing.Size(80, 14);
            this._lblTDSCreatedBy.TabIndex = 155;
            this._lblTDSCreatedBy.Text = "TDS By";
            this._lblTDSCreatedBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTDSNumber
            // 
            this.txtTDSNumber.Location = new System.Drawing.Point(90, 57);
            this.txtTDSNumber.Name = "txtTDSNumber";
            this.txtTDSNumber.Size = new System.Drawing.Size(106, 20);
            this.txtTDSNumber.TabIndex = 1;
            // 
            // _lblTDSNumber
            // 
            this._lblTDSNumber.Location = new System.Drawing.Point(5, 57);
            this._lblTDSNumber.Name = "_lblTDSNumber";
            this._lblTDSNumber.Size = new System.Drawing.Size(80, 14);
            this._lblTDSNumber.TabIndex = 153;
            this._lblTDSNumber.Text = "TDS#";
            this._lblTDSNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkIsLiveUnload
            // 
            this.chkIsLiveUnload.Location = new System.Drawing.Point(253, 184);
            this.chkIsLiveUnload.Name = "chkIsLiveUnload";
            this.chkIsLiveUnload.Size = new System.Drawing.Size(106, 15);
            this.chkIsLiveUnload.TabIndex = 7;
            this.chkIsLiveUnload.Text = "Live Unload?";
            // 
            // _lblSortDate
            // 
            this._lblSortDate.Location = new System.Drawing.Point(5, 141);
            this._lblSortDate.Name = "_lblSortDate";
            this._lblSortDate.Size = new System.Drawing.Size(100, 14);
            this._lblSortDate.TabIndex = 150;
            this._lblSortDate.Text = "Sort Date";
            this._lblSortDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpSortDate
            // 
            this.dtpSortDate.Checked = false;
            this.dtpSortDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpSortDate.Location = new System.Drawing.Point(110, 141);
            this.dtpSortDate.Name = "dtpSortDate";
            this.dtpSortDate.ShowCheckBox = true;
            this.dtpSortDate.Size = new System.Drawing.Size(125, 20);
            this.dtpSortDate.TabIndex = 6;
            // 
            // dtpActualArrival
            // 
            this.dtpActualArrival.Checked = false;
            this.dtpActualArrival.CustomFormat = "hh:mm tt";
            this.dtpActualArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActualArrival.Location = new System.Drawing.Point(91, 25);
            this.dtpActualArrival.Name = "dtpActualArrival";
            this.dtpActualArrival.ShowCheckBox = true;
            this.dtpActualArrival.ShowUpDown = true;
            this.dtpActualArrival.Size = new System.Drawing.Size(100, 20);
            this.dtpActualArrival.TabIndex = 0;
            // 
            // _lblActualArrival
            // 
            this._lblActualArrival.Location = new System.Drawing.Point(5, 25);
            this._lblActualArrival.Name = "_lblActualArrival";
            this._lblActualArrival.Size = new System.Drawing.Size(80, 16);
            this._lblActualArrival.TabIndex = 148;
            this._lblActualArrival.Text = "Actual Arrival";
            this._lblActualArrival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblShipper
            // 
            this._lblShipper.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblShipper.Location = new System.Drawing.Point(5, 51);
            this._lblShipper.Name = "_lblShipper";
            this._lblShipper.Size = new System.Drawing.Size(100, 16);
            this._lblShipper.TabIndex = 142;
            this._lblShipper.Text = "Shipper";
            this._lblShipper.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpScheduledArrival
            // 
            this.dtpScheduledArrival.Checked = false;
            this.dtpScheduledArrival.CustomFormat = "hh:mm tt";
            this.dtpScheduledArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduledArrival.Location = new System.Drawing.Point(110, 182);
            this.dtpScheduledArrival.Name = "dtpScheduledArrival";
            this.dtpScheduledArrival.ShowUpDown = true;
            this.dtpScheduledArrival.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduledArrival.TabIndex = 8;
            this.dtpScheduledArrival.ValueChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblScheduledArrival
            // 
            this._lblScheduledArrival.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblScheduledArrival.Location = new System.Drawing.Point(5, 182);
            this._lblScheduledArrival.Name = "_lblScheduledArrival";
            this._lblScheduledArrival.Size = new System.Drawing.Size(100, 16);
            this._lblScheduledArrival.TabIndex = 131;
            this._lblScheduledArrival.Text = "Sched Arrival";
            this._lblScheduledArrival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(5, 240);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(100, 15);
            this._lblComments.TabIndex = 129;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblAmount
            // 
            this._lblAmount.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblAmount.Location = new System.Drawing.Point(5, 111);
            this._lblAmount.Name = "_lblAmount";
            this._lblAmount.Size = new System.Drawing.Size(100, 15);
            this._lblAmount.TabIndex = 125;
            this._lblAmount.Text = "Amount";
            this._lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(110, 109);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(40, 20);
            this.txtAmount.TabIndex = 4;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // cboAmountType
            // 
            this.cboAmountType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboAmountType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboAmountType.Items.AddRange(new object[] {
            "Cartons",
            "Mixed",
            "Pallets",
            "Trailer"});
            this.cboAmountType.Location = new System.Drawing.Point(155, 109);
            this.cboAmountType.MaxDropDownItems = 5;
            this.cboAmountType.Name = "cboAmountType";
            this.cboAmountType.Size = new System.Drawing.Size(75, 21);
            this.cboAmountType.TabIndex = 5;
            this.cboAmountType.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(110, 240);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(250, 20);
            this.txtComments.TabIndex = 10;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblConsignee
            // 
            this._lblConsignee.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblConsignee.Location = new System.Drawing.Point(5, 77);
            this._lblConsignee.Name = "_lblConsignee";
            this._lblConsignee.Size = new System.Drawing.Size(100, 16);
            this._lblConsignee.TabIndex = 126;
            this._lblConsignee.Text = "Consignee";
            this._lblConsignee.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblID
            // 
            this._lblID.Location = new System.Drawing.Point(521, 17);
            this._lblID.Name = "_lblID";
            this._lblID.Size = new System.Drawing.Size(100, 15);
            this._lblID.TabIndex = 122;
            this._lblID.Text = "Appt#";
            this._lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(540, 290);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 24);
            this.btnOk.TabIndex = 13;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(642, 290);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(627, 17);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(100, 16);
            this.lblID.TabIndex = 143;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScheduleDate.Location = new System.Drawing.Point(110, 17);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduleDate.TabIndex = 1;
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.OnScheduleDateChanged);
            // 
            // _lblScheduleDate
            // 
            this._lblScheduleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblScheduleDate.Location = new System.Drawing.Point(5, 17);
            this._lblScheduleDate.Name = "_lblScheduleDate";
            this._lblScheduleDate.Size = new System.Drawing.Size(100, 15);
            this._lblScheduleDate.TabIndex = 169;
            this._lblScheduleDate.Text = "Schedule Date";
            this._lblScheduleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboShipper
            // 
            this.cboShipper.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboShipper.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboShipper.DataSource = this.mLocations;
            this.cboShipper.DisplayMember = "LocationTable.Name";
            this.cboShipper.Location = new System.Drawing.Point(110, 51);
            this.cboShipper.Name = "cboShipper";
            this.cboShipper.Size = new System.Drawing.Size(250, 21);
            this.cboShipper.TabIndex = 2;
            this.cboShipper.ValueMember = "LocationTable.Name";
            this.cboShipper.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mLocations
            // 
            this.mLocations.DataSetName = "DispatchDataset";
            this.mLocations.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboConsignee
            // 
            this.cboConsignee.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboConsignee.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboConsignee.DataSource = this.mConsignees;
            this.cboConsignee.DisplayMember = "LocationTable.Name";
            this.cboConsignee.Location = new System.Drawing.Point(110, 77);
            this.cboConsignee.Name = "cboConsignee";
            this.cboConsignee.Size = new System.Drawing.Size(250, 21);
            this.cboConsignee.TabIndex = 3;
            this.cboConsignee.ValueMember = "LocationTable.Name";
            this.cboConsignee.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mConsignees
            // 
            this.mConsignees.DataSetName = "DispatchDataset";
            this.mConsignees.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpDispatch
            // 
            this.grpDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDispatch.Controls.Add(this.cboDriver);
            this.grpDispatch.Controls.Add(this.cboCarrier);
            this.grpDispatch.Controls.Add(this._lblCarrier);
            this.grpDispatch.Controls.Add(this._lblDriver);
            this.grpDispatch.Controls.Add(this.chkConfirmed);
            this.grpDispatch.Location = new System.Drawing.Point(396, 51);
            this.grpDispatch.Name = "grpDispatch";
            this.grpDispatch.Size = new System.Drawing.Size(342, 100);
            this.grpDispatch.TabIndex = 11;
            this.grpDispatch.TabStop = false;
            this.grpDispatch.Text = "Dispatch";
            // 
            // cboDriver
            // 
            this.cboDriver.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDriver.DataSource = this.mDrivers;
            this.cboDriver.DisplayMember = "DriverTable.Description";
            this.cboDriver.FormattingEnabled = true;
            this.cboDriver.Location = new System.Drawing.Point(90, 51);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Size = new System.Drawing.Size(241, 21);
            this.cboDriver.TabIndex = 2;
            this.cboDriver.ValueMember = "DriverTable.Description";
            // 
            // mDrivers
            // 
            this.mDrivers.DataSetName = "DispatchDataset";
            this.mDrivers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboCarrier
            // 
            this.cboCarrier.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cboCarrier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCarrier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCarrier.DataSource = this.mCarriers;
            this.cboCarrier.DisplayMember = "CarrierTable.Description";
            this.cboCarrier.FormattingEnabled = true;
            this.cboCarrier.Location = new System.Drawing.Point(90, 25);
            this.cboCarrier.Name = "cboCarrier";
            this.cboCarrier.Size = new System.Drawing.Size(241, 21);
            this.cboCarrier.TabIndex = 0;
            this.cboCarrier.ValueMember = "CarrierTable.Description";
            // 
            // mCarriers
            // 
            this.mCarriers.DataSetName = "DispatchDataset";
            this.mCarriers.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // _lblCarrier
            // 
            this._lblCarrier.Location = new System.Drawing.Point(5, 25);
            this._lblCarrier.Name = "_lblCarrier";
            this._lblCarrier.Size = new System.Drawing.Size(80, 16);
            this._lblCarrier.TabIndex = 148;
            this._lblCarrier.Text = "Carrier";
            this._lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDriver
            // 
            this._lblDriver.Location = new System.Drawing.Point(5, 51);
            this._lblDriver.Name = "_lblDriver";
            this._lblDriver.Size = new System.Drawing.Size(80, 16);
            this._lblDriver.TabIndex = 137;
            this._lblDriver.Text = "Driver";
            this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkConfirmed
            // 
            this.chkConfirmed.Location = new System.Drawing.Point(90, 77);
            this.chkConfirmed.Name = "chkConfirmed";
            this.chkConfirmed.Size = new System.Drawing.Size(80, 16);
            this.chkConfirmed.TabIndex = 4;
            this.chkConfirmed.Text = "Confirmed?";
            // 
            // grpGate
            // 
            this.grpGate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGate.Controls.Add(this.dtpActualArrival);
            this.grpGate.Controls.Add(this._lblActualArrival);
            this.grpGate.Controls.Add(this.txtTDSNumber);
            this.grpGate.Controls.Add(this.txtTDSCreatedBy);
            this.grpGate.Controls.Add(this._lblTDSNumber);
            this.grpGate.Controls.Add(this._lblTDSCreatedBy);
            this.grpGate.Location = new System.Drawing.Point(396, 157);
            this.grpGate.Name = "grpGate";
            this.grpGate.Size = new System.Drawing.Size(342, 109);
            this.grpGate.TabIndex = 12;
            this.grpGate.TabStop = false;
            this.grpGate.Text = "Arrival";
            // 
            // txtTrailerNumber
            // 
            this.txtTrailerNumber.Location = new System.Drawing.Point(110, 214);
            this.txtTrailerNumber.Name = "txtTrailerNumber";
            this.txtTrailerNumber.Size = new System.Drawing.Size(100, 20);
            this.txtTrailerNumber.TabIndex = 9;
            // 
            // _lblTrailerNumber
            // 
            this._lblTrailerNumber.Location = new System.Drawing.Point(5, 214);
            this._lblTrailerNumber.Name = "_lblTrailerNumber";
            this._lblTrailerNumber.Size = new System.Drawing.Size(100, 15);
            this._lblTrailerNumber.TabIndex = 176;
            this._lblTrailerNumber.Text = "Trailer#";
            this._lblTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblOf
            // 
            this._lblOf.Location = new System.Drawing.Point(241, 109);
            this._lblOf.Name = "_lblOf";
            this._lblOf.Size = new System.Drawing.Size(20, 18);
            this._lblOf.TabIndex = 177;
            this._lblOf.Text = "of";
            this._lblOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboFreightType
            // 
            this.cboFreightType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFreightType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFreightType.Items.AddRange(new object[] {
            "ISA",
            "BBB-PCS",
            "Tsort"});
            this.cboFreightType.Location = new System.Drawing.Point(270, 109);
            this.cboFreightType.MaxDropDownItems = 5;
            this.cboFreightType.Name = "cboFreightType";
            this.cboFreightType.Size = new System.Drawing.Size(90, 21);
            this.cboFreightType.TabIndex = 202;
            this.cboFreightType.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // dlgClientInboundFreight
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 322);
            this.Controls.Add(this.cboFreightType);
            this.Controls.Add(this._lblOf);
            this.Controls.Add(this.txtTrailerNumber);
            this.Controls.Add(this._lblTrailerNumber);
            this.Controls.Add(this.grpGate);
            this.Controls.Add(this.grpDispatch);
            this.Controls.Add(this.cboConsignee);
            this.Controls.Add(this.dtpSortDate);
            this.Controls.Add(this.cboShipper);
            this.Controls.Add(this._lblSortDate);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this._lblScheduleDate);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.chkIsLiveUnload);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this._lblID);
            this.Controls.Add(this._lblConsignee);
            this.Controls.Add(this._lblShipper);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.cboAmountType);
            this.Controls.Add(this.dtpScheduledArrival);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this._lblScheduledArrival);
            this.Controls.Add(this._lblAmount);
            this.Controls.Add(this._lblComments);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgClientInboundFreight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Client Inbound Freight";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mLocations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mConsignees)).EndInit();
            this.grpDispatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriers)).EndInit();
            this.grpGate.ResumeLayout(false);
            this.grpGate.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load selection lists
                this.mLocations.Merge(FreightGateway.GetLocations());
                this.mConsignees.Merge(FreightGateway.GetLocations());
                this.mCarriers.Merge(FreightGateway.GetCarriers());
                this.mDrivers.Merge(FreightGateway.GetDrivers());
				
				//Load controls
				this.Text = "Client Inbound Freight" + " (" + (!this.mAppt.IsIDNull() ? this.mAppt.ID.ToString() : "New") + (this.mIsTemplate ? " Template)" : ")");
                this.lblID.Text = !this.mAppt.IsIDNull() ? this.mAppt.ID.ToString("00000000") : "";
                this.dtpScheduleDate.MinDate = DateTime.MinValue;
                this.dtpScheduleDate.MaxDate = DateTime.Today.AddDays(30);
                this.dtpScheduleDate.Value = !this.mAppt.IsScheduleDateNull() ? this.mAppt.ScheduleDate : DateTime.Today;
                if (!this.mAppt.IsVendorNameNull()) 
                    this.cboShipper.Text = this.mAppt.VendorName;
                else {
                    this.cboShipper.Text = ""; this.cboShipper.SelectedIndex = -1;
                }
                this.cboConsignee.Text = !this.mAppt.IsConsigneeNameNull() ? this.mAppt.ConsigneeName : "";
                this.txtAmount.Text = !this.mAppt.IsAmountNull() ? this.mAppt.Amount.ToString() : "0";
                this.cboAmountType.Text = !this.mAppt.IsAmountTypeNull() ? this.mAppt.AmountType : "Cartons";
                this.cboFreightType.Text = !this.mAppt.IsFreightTypeNull() ? this.mAppt.FreightType : "Tsort";
                this.dtpSortDate.MinDate = DateTime.MinValue;
                this.dtpSortDate.MaxDate = DateTime.MaxValue;
                if (!this.mAppt.IsSortDateNull()) this.dtpSortDate.Value = this.mAppt.SortDate;
                this.chkIsLiveUnload.Checked = !this.mAppt.IsIsLiveUnloadNull() ? this.mAppt.IsLiveUnload : false;
                this.dtpScheduledArrival.MinDate = DateTime.MinValue;
                this.dtpScheduledArrival.MaxDate = DateTime.MaxValue;
                if (!this.mAppt.IsScheduledArrivalNull())
                    this.dtpScheduledArrival.Value = this.mAppt.ScheduledArrival;
                else {
                    this.dtpScheduledArrival.Value = this.dtpScheduleDate.Value;    //Set time to 00:00:00 AM
                }
                this.txtTrailerNumber.Text = !this.mAppt.IsTrailerNumberNull() ? this.mAppt.TrailerNumber : "";
                this.cboCarrier.Text = !this.mAppt.IsCarrierNameNull() ? this.mAppt.CarrierName : "";
                this.cboDriver.Text = !this.mAppt.IsDriverNameNull() ? this.mAppt.DriverName : "";
                this.dtpActualArrival.MinDate = DateTime.MinValue;
                this.dtpActualArrival.MaxDate = DateTime.MaxValue;
                if (!this.mAppt.IsActualArrivalNull()) {
                    this.dtpActualArrival.Value = this.mAppt.ActualArrival;
                    this.dtpActualArrival.Checked = true;
                }
                else {
                    this.dtpActualArrival.Value = DateTime.Now;
                    this.dtpActualArrival.Checked = false;
                }
                this.txtTDSNumber.Text = !this.mAppt.IsTDSNumberNull() ? this.mAppt.TDSNumber : "";
                this.txtTDSCreatedBy.Text = !this.mAppt.IsTDSCreateUserIDNull() ? this.mAppt.TDSCreateUserID : "";
                this.txtComments.Text = !this.mAppt.IsCommentsNull() ? this.mAppt.Comments : "";
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
		}
        private void OnScheduleDateChanged(object sender,EventArgs e) {
            //Event handler for change in schedule date
			try {
                //Keep the scheduled arrival date the same (but preserve the time) for better sorting
                this.dtpScheduledArrival.Value = this.dtpScheduleDate.Value.Date + this.dtpScheduledArrival.Value.TimeOfDay;
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); }
        }
        private void OnValidateForm(object sender,EventArgs e) {
			//Set user services
			try {
                //Set services
                bool access = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk;

                this.dtpScheduleDate.Enabled = !this.mIsTemplate;
                this.cboShipper.Enabled = true;
                this.cboConsignee.Enabled = true;
                this.txtAmount.Enabled = true;
                this.cboAmountType.Enabled = true;
                this.cboFreightType.Enabled = true;
                this.dtpSortDate.Enabled = !this.mIsTemplate;
                this.chkIsLiveUnload.Enabled = true;
                this.dtpScheduledArrival.Enabled = true;
                this.txtTrailerNumber.Enabled = !this.mIsTemplate;
                this.cboCarrier.Enabled = true;
                this.cboDriver.Enabled = true;
                this.chkConfirmed.Enabled = false;
                this.dtpActualArrival.Enabled = !this.mIsTemplate && !this.mIsNewSchedule;
                this.txtTDSNumber.Enabled = !this.mIsTemplate && !this.mIsNewSchedule;
                this.txtTDSCreatedBy.Enabled = !this.mIsTemplate && !this.mIsNewSchedule;
                this.txtComments.Enabled = true;

                bool cancelled = !this.mAppt.IsCancelledNull() && this.mAppt.Cancelled.CompareTo(DateTime.MinValue) > 0;
                this.btnOk.Enabled = (access && !cancelled &&
                                        this.cboShipper.Text.Trim().Length > 0 &&
                                        this.cboConsignee.Text.Trim().Length > 0 &&
                                        this.txtAmount.Text.Trim().Length > 0 && 
                                        this.cboAmountType.Text.Trim().Length > 0 &&
                                        this.dtpScheduledArrival.Text.Trim().Length > 0);
			}
            catch (Exception ex) { App.ReportError(ex,false,LogLevel.Warning); }
        }
        private void OnCommandClick(object sender,System.EventArgs e) {
            //Event handler for button selection
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    case "btnOk":
                        DateTime now = DateTime.Now;
                        if (this.mAppt.IsIDNull()) {
                            this.mAppt.Created = DateTime.Now;
                            this.mAppt.CreateUserID = Environment.UserName;
                        }
                        this.mAppt.ScheduleDate = this.dtpScheduleDate.Value;
				        this.mAppt.VendorName = this.cboShipper.Text.Trim();
				        this.mAppt.ConsigneeName = this.cboConsignee.Text;
                        this.mAppt.Amount = Convert.ToInt32(this.txtAmount.Text.Trim());
                        this.mAppt.AmountType = this.cboAmountType.Text.Trim();
                        this.mAppt.FreightType = this.cboFreightType.Text.Trim();
                        if (this.dtpSortDate.Checked) this.mAppt.SortDate = this.dtpSortDate.Value; else this.mAppt.SortDate = DateTime.MinValue;
                        this.mAppt.IsLiveUnload = this.chkIsLiveUnload.Checked;
                        this.mAppt.ScheduledArrival = this.dtpScheduledArrival.Value;
                        this.mAppt.TrailerNumber = this.txtTrailerNumber.Text.Trim();
                        this.mAppt.CarrierName = this.cboCarrier.Text.Trim();
                        this.mAppt.DriverName = this.cboDriver.Text.Trim();
                        if (this.dtpActualArrival.Checked) this.mAppt.ActualArrival = this.dtpActualArrival.Value; else this.mAppt.ActualArrival = DateTime.MinValue;
                        this.mAppt.TDSNumber = this.txtTDSNumber.Text.Trim();
                        this.mAppt.TDSCreateUserID = this.txtTDSCreatedBy.Text.Trim();
                        this.mAppt.Comments = this.txtComments.Text.Trim();
                        if(this.mIsTemplate) this.mAppt.IsTemplate = true;
                        this.mAppt.LastUpdated = now;
                        this.mAppt.UserID = Environment.UserName;

                        this.DialogResult = DialogResult.OK;
                        this.Close();
                        break;
                }
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}

