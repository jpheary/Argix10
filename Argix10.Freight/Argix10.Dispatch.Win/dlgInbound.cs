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
    public class dlgInboundFreight:System.Windows.Forms.Form {
		//Members
        private DispatchDataset.InboundScheduleTableRow mTrip = null;
        private bool mIsNewSchedule = true,mIsTemplate = false;

        #region Controls

        private Button btnOk;
        private Button btnCancel;
        private TextBox txtDestinationLoc;
        private TextBox txtOriginLoc;
        private TextBox txtDropEmptyTrailerNumber;
        private Label _lblDropEmptyTrailerNumber;
        private GroupBox grpGate;
        private DateTimePicker dtpActualArrival;
        private Label _lblActualDeparture;
        private DateTimePicker dtpActualDeparture;
        private Label _lblActualArrival;
        private DateTimePicker dtpScheduleDate;
        private Label lblID;
        private Label _lblID;
        private Label _lblScheduleDate;
        private Label _lblDestination;
        private ComboBox cboDestination;
        private DateTimePicker dtpScheduledDeparture;
        private Label _lblScheduledDeparture;
        private Label _lblComments;
        private GroupBox grpDispatch;
        private ComboBox cboDriver;
        private ComboBox cboCarrier;
        private Label _lblCarrier;
        private Label _lblDriver;
        private CheckBox chkConfirmed;
        private TextBox txtTrailerNumber;
        private Label _lblTrailerNumber;
        private TextBox txtComments;
        private Label _lblOrigin;
        private ComboBox cboOrigin;
        private DateTimePicker dtpScheduledArrival;
        private Label _lblScheduledArrival;
        private DispatchDataset mCarriers;
        private DispatchDataset mDrivers;
        private ComboBox cboAmountType;
        private TextBox txtAmount;
        private Label _lblAmount;
        private DispatchDataset mOrigins;
        private DispatchDataset mDestinations;
        private Label _lblOf;
        private ComboBox cboFreightType;
		private System.ComponentModel.IContainer components = null;
		#endregion

        public dlgInboundFreight(DispatchDataset.InboundScheduleTableRow freight,bool isTemplate = false) {
			//Constructor
			try {
				InitializeComponent();
                this.mTrip = freight;
                this.mIsNewSchedule = this.mTrip.IsIDNull() || (!this.mTrip.IsIDNull() && this.mTrip.ID == 0);
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtDestinationLoc = new System.Windows.Forms.TextBox();
            this.mDestinations = new Argix.DispatchDataset();
            this.txtOriginLoc = new System.Windows.Forms.TextBox();
            this.mOrigins = new Argix.DispatchDataset();
            this.txtDropEmptyTrailerNumber = new System.Windows.Forms.TextBox();
            this._lblDropEmptyTrailerNumber = new System.Windows.Forms.Label();
            this.grpGate = new System.Windows.Forms.GroupBox();
            this.dtpActualArrival = new System.Windows.Forms.DateTimePicker();
            this._lblActualDeparture = new System.Windows.Forms.Label();
            this.dtpActualDeparture = new System.Windows.Forms.DateTimePicker();
            this._lblActualArrival = new System.Windows.Forms.Label();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.lblID = new System.Windows.Forms.Label();
            this._lblID = new System.Windows.Forms.Label();
            this._lblScheduleDate = new System.Windows.Forms.Label();
            this._lblDestination = new System.Windows.Forms.Label();
            this.cboDestination = new System.Windows.Forms.ComboBox();
            this.dtpScheduledDeparture = new System.Windows.Forms.DateTimePicker();
            this._lblScheduledDeparture = new System.Windows.Forms.Label();
            this._lblComments = new System.Windows.Forms.Label();
            this.grpDispatch = new System.Windows.Forms.GroupBox();
            this.cboDriver = new System.Windows.Forms.ComboBox();
            this.mDrivers = new Argix.DispatchDataset();
            this.cboCarrier = new System.Windows.Forms.ComboBox();
            this.mCarriers = new Argix.DispatchDataset();
            this._lblCarrier = new System.Windows.Forms.Label();
            this._lblDriver = new System.Windows.Forms.Label();
            this.chkConfirmed = new System.Windows.Forms.CheckBox();
            this.txtTrailerNumber = new System.Windows.Forms.TextBox();
            this._lblTrailerNumber = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this._lblOrigin = new System.Windows.Forms.Label();
            this.cboOrigin = new System.Windows.Forms.ComboBox();
            this.dtpScheduledArrival = new System.Windows.Forms.DateTimePicker();
            this._lblScheduledArrival = new System.Windows.Forms.Label();
            this.cboAmountType = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this._lblAmount = new System.Windows.Forms.Label();
            this._lblOf = new System.Windows.Forms.Label();
            this.cboFreightType = new System.Windows.Forms.ComboBox();
            ((System.ComponentModel.ISupportInitialize)(this.mDestinations)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOrigins)).BeginInit();
            this.grpGate.SuspendLayout();
            this.grpDispatch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriers)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(539, 311);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(96, 24);
            this.btnOk.TabIndex = 15;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(641, 311);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 24);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // txtDestinationLoc
            // 
            this.txtDestinationLoc.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mDestinations, "LocationTable.Location", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.txtDestinationLoc.Location = new System.Drawing.Point(112, 132);
            this.txtDestinationLoc.Name = "txtDestinationLoc";
            this.txtDestinationLoc.Size = new System.Drawing.Size(250, 20);
            this.txtDestinationLoc.TabIndex = 5;
            // 
            // mDestinations
            // 
            this.mDestinations.DataSetName = "DispatchDataset";
            this.mDestinations.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtOriginLoc
            // 
            this.txtOriginLoc.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mOrigins, "LocationTable.Location", true, System.Windows.Forms.DataSourceUpdateMode.Never));
            this.txtOriginLoc.Location = new System.Drawing.Point(112, 76);
            this.txtOriginLoc.Name = "txtOriginLoc";
            this.txtOriginLoc.Size = new System.Drawing.Size(250, 20);
            this.txtOriginLoc.TabIndex = 3;
            // 
            // mOrigins
            // 
            this.mOrigins.DataSetName = "DispatchDataset";
            this.mOrigins.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // txtDropEmptyTrailerNumber
            // 
            this.txtDropEmptyTrailerNumber.Location = new System.Drawing.Point(112, 267);
            this.txtDropEmptyTrailerNumber.Name = "txtDropEmptyTrailerNumber";
            this.txtDropEmptyTrailerNumber.Size = new System.Drawing.Size(100, 20);
            this.txtDropEmptyTrailerNumber.TabIndex = 11;
            // 
            // _lblDropEmptyTrailerNumber
            // 
            this._lblDropEmptyTrailerNumber.Location = new System.Drawing.Point(6, 267);
            this._lblDropEmptyTrailerNumber.Name = "_lblDropEmptyTrailerNumber";
            this._lblDropEmptyTrailerNumber.Size = new System.Drawing.Size(100, 16);
            this._lblDropEmptyTrailerNumber.TabIndex = 172;
            this._lblDropEmptyTrailerNumber.Text = "Drop Empty Trailer#";
            this._lblDropEmptyTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpGate
            // 
            this.grpGate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpGate.Controls.Add(this.dtpActualArrival);
            this.grpGate.Controls.Add(this._lblActualDeparture);
            this.grpGate.Controls.Add(this.dtpActualDeparture);
            this.grpGate.Controls.Add(this._lblActualArrival);
            this.grpGate.Location = new System.Drawing.Point(390, 163);
            this.grpGate.Name = "grpGate";
            this.grpGate.Size = new System.Drawing.Size(347, 82);
            this.grpGate.TabIndex = 13;
            this.grpGate.TabStop = false;
            this.grpGate.Text = "Departure/Arrival";
            // 
            // dtpActualArrival
            // 
            this.dtpActualArrival.Checked = false;
            this.dtpActualArrival.CustomFormat = "hh:mm tt";
            this.dtpActualArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActualArrival.Location = new System.Drawing.Point(87, 51);
            this.dtpActualArrival.Name = "dtpActualArrival";
            this.dtpActualArrival.ShowCheckBox = true;
            this.dtpActualArrival.ShowUpDown = true;
            this.dtpActualArrival.Size = new System.Drawing.Size(100, 20);
            this.dtpActualArrival.TabIndex = 1;
            // 
            // _lblActualDeparture
            // 
            this._lblActualDeparture.Location = new System.Drawing.Point(5, 25);
            this._lblActualDeparture.Name = "_lblActualDeparture";
            this._lblActualDeparture.Size = new System.Drawing.Size(75, 16);
            this._lblActualDeparture.TabIndex = 141;
            this._lblActualDeparture.Text = "Act Departure";
            this._lblActualDeparture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpActualDeparture
            // 
            this.dtpActualDeparture.Checked = false;
            this.dtpActualDeparture.CustomFormat = "hh:mm tt";
            this.dtpActualDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpActualDeparture.Location = new System.Drawing.Point(87, 25);
            this.dtpActualDeparture.Name = "dtpActualDeparture";
            this.dtpActualDeparture.ShowCheckBox = true;
            this.dtpActualDeparture.ShowUpDown = true;
            this.dtpActualDeparture.Size = new System.Drawing.Size(100, 20);
            this.dtpActualDeparture.TabIndex = 0;
            // 
            // _lblActualArrival
            // 
            this._lblActualArrival.Location = new System.Drawing.Point(5, 51);
            this._lblActualArrival.Name = "_lblActualArrival";
            this._lblActualArrival.Size = new System.Drawing.Size(75, 16);
            this._lblActualArrival.TabIndex = 143;
            this._lblActualArrival.Text = "Act Arrival";
            this._lblActualArrival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScheduleDate.Location = new System.Drawing.Point(112, 18);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduleDate.TabIndex = 1;
            this.dtpScheduleDate.ValueChanged += new System.EventHandler(this.OnScheduleDateChanged);
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(627, 18);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(100, 16);
            this.lblID.TabIndex = 167;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblID
            // 
            this._lblID.Location = new System.Drawing.Point(521, 18);
            this._lblID.Name = "_lblID";
            this._lblID.Size = new System.Drawing.Size(100, 15);
            this._lblID.TabIndex = 164;
            this._lblID.Text = "Trip#";
            this._lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblScheduleDate
            // 
            this._lblScheduleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblScheduleDate.Location = new System.Drawing.Point(6, 18);
            this._lblScheduleDate.Name = "_lblScheduleDate";
            this._lblScheduleDate.Size = new System.Drawing.Size(100, 15);
            this._lblScheduleDate.TabIndex = 165;
            this._lblScheduleDate.Text = "Schedule Date";
            this._lblScheduleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDestination
            // 
            this._lblDestination.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblDestination.Location = new System.Drawing.Point(6, 107);
            this._lblDestination.Name = "_lblDestination";
            this._lblDestination.Size = new System.Drawing.Size(100, 16);
            this._lblDestination.TabIndex = 166;
            this._lblDestination.Text = "Destination";
            this._lblDestination.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDestination
            // 
            this.cboDestination.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDestination.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDestination.DataSource = this.mDestinations;
            this.cboDestination.DisplayMember = "LocationTable.Name";
            this.cboDestination.Location = new System.Drawing.Point(112, 107);
            this.cboDestination.Name = "cboDestination";
            this.cboDestination.Size = new System.Drawing.Size(250, 21);
            this.cboDestination.TabIndex = 4;
            this.cboDestination.ValueMember = "LocationTable.Name";
            this.cboDestination.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // dtpScheduledDeparture
            // 
            this.dtpScheduledDeparture.CustomFormat = "hh:mm tt";
            this.dtpScheduledDeparture.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduledDeparture.Location = new System.Drawing.Point(112, 189);
            this.dtpScheduledDeparture.Name = "dtpScheduledDeparture";
            this.dtpScheduledDeparture.ShowUpDown = true;
            this.dtpScheduledDeparture.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduledDeparture.TabIndex = 8;
            this.dtpScheduledDeparture.ValueChanged += new System.EventHandler(this.OnScheduleChanged);
            // 
            // _lblScheduledDeparture
            // 
            this._lblScheduledDeparture.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblScheduledDeparture.Location = new System.Drawing.Point(6, 189);
            this._lblScheduledDeparture.Name = "_lblScheduledDeparture";
            this._lblScheduledDeparture.Size = new System.Drawing.Size(100, 16);
            this._lblScheduledDeparture.TabIndex = 163;
            this._lblScheduledDeparture.Text = "Sch Departure";
            this._lblScheduledDeparture.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComments
            // 
            this._lblComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this._lblComments.Location = new System.Drawing.Point(298, 267);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(85, 16);
            this._lblComments.TabIndex = 160;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpDispatch
            // 
            this.grpDispatch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDispatch.Controls.Add(this.cboDriver);
            this.grpDispatch.Controls.Add(this.cboCarrier);
            this.grpDispatch.Controls.Add(this._lblCarrier);
            this.grpDispatch.Controls.Add(this._lblDriver);
            this.grpDispatch.Controls.Add(this.chkConfirmed);
            this.grpDispatch.Location = new System.Drawing.Point(390, 51);
            this.grpDispatch.Name = "grpDispatch";
            this.grpDispatch.Size = new System.Drawing.Size(347, 101);
            this.grpDispatch.TabIndex = 12;
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
            this.cboDriver.Location = new System.Drawing.Point(87, 51);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Size = new System.Drawing.Size(250, 21);
            this.cboDriver.TabIndex = 1;
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
            this.cboCarrier.Location = new System.Drawing.Point(87, 25);
            this.cboCarrier.Name = "cboCarrier";
            this.cboCarrier.Size = new System.Drawing.Size(250, 21);
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
            this._lblCarrier.Location = new System.Drawing.Point(6, 25);
            this._lblCarrier.Name = "_lblCarrier";
            this._lblCarrier.Size = new System.Drawing.Size(75, 16);
            this._lblCarrier.TabIndex = 148;
            this._lblCarrier.Text = "Carrier";
            this._lblCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblDriver
            // 
            this._lblDriver.Location = new System.Drawing.Point(6, 51);
            this._lblDriver.Name = "_lblDriver";
            this._lblDriver.Size = new System.Drawing.Size(75, 16);
            this._lblDriver.TabIndex = 137;
            this._lblDriver.Text = "Driver";
            this._lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkConfirmed
            // 
            this.chkConfirmed.Location = new System.Drawing.Point(87, 79);
            this.chkConfirmed.Name = "chkConfirmed";
            this.chkConfirmed.Size = new System.Drawing.Size(80, 16);
            this.chkConfirmed.TabIndex = 2;
            this.chkConfirmed.Text = "Confirmed?";
            // 
            // txtTrailerNumber
            // 
            this.txtTrailerNumber.Location = new System.Drawing.Point(112, 241);
            this.txtTrailerNumber.Name = "txtTrailerNumber";
            this.txtTrailerNumber.Size = new System.Drawing.Size(100, 20);
            this.txtTrailerNumber.TabIndex = 10;
            // 
            // _lblTrailerNumber
            // 
            this._lblTrailerNumber.Location = new System.Drawing.Point(6, 241);
            this._lblTrailerNumber.Name = "_lblTrailerNumber";
            this._lblTrailerNumber.Size = new System.Drawing.Size(100, 16);
            this._lblTrailerNumber.TabIndex = 115;
            this._lblTrailerNumber.Text = "Trailer#";
            this._lblTrailerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComments.Location = new System.Drawing.Point(390, 267);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(347, 20);
            this.txtComments.TabIndex = 14;
            // 
            // _lblOrigin
            // 
            this._lblOrigin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblOrigin.Location = new System.Drawing.Point(6, 51);
            this._lblOrigin.Name = "_lblOrigin";
            this._lblOrigin.Size = new System.Drawing.Size(100, 16);
            this._lblOrigin.TabIndex = 162;
            this._lblOrigin.Text = "Origin";
            this._lblOrigin.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboOrigin
            // 
            this.cboOrigin.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboOrigin.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOrigin.DataSource = this.mOrigins;
            this.cboOrigin.DisplayMember = "LocationTable.Name";
            this.cboOrigin.Location = new System.Drawing.Point(112, 51);
            this.cboOrigin.Name = "cboOrigin";
            this.cboOrigin.Size = new System.Drawing.Size(250, 21);
            this.cboOrigin.TabIndex = 2;
            this.cboOrigin.ValueMember = "LocationTable.Name";
            this.cboOrigin.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // dtpScheduledArrival
            // 
            this.dtpScheduledArrival.CustomFormat = "hh:mm tt";
            this.dtpScheduledArrival.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpScheduledArrival.Location = new System.Drawing.Point(112, 215);
            this.dtpScheduledArrival.Name = "dtpScheduledArrival";
            this.dtpScheduledArrival.ShowUpDown = true;
            this.dtpScheduledArrival.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduledArrival.TabIndex = 9;
            this.dtpScheduledArrival.ValueChanged += new System.EventHandler(this.OnScheduleChanged);
            // 
            // _lblScheduledArrival
            // 
            this._lblScheduledArrival.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblScheduledArrival.Location = new System.Drawing.Point(6, 215);
            this._lblScheduledArrival.Name = "_lblScheduledArrival";
            this._lblScheduledArrival.Size = new System.Drawing.Size(100, 16);
            this._lblScheduledArrival.TabIndex = 161;
            this._lblScheduledArrival.Text = "Sch Arrival";
            this._lblScheduledArrival.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
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
            this.cboAmountType.Location = new System.Drawing.Point(168, 163);
            this.cboAmountType.MaxDropDownItems = 5;
            this.cboAmountType.Name = "cboAmountType";
            this.cboAmountType.Size = new System.Drawing.Size(75, 21);
            this.cboAmountType.TabIndex = 7;
            this.cboAmountType.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(112, 163);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(50, 20);
            this.txtAmount.TabIndex = 6;
            this.txtAmount.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmount.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblAmount
            // 
            this._lblAmount.Location = new System.Drawing.Point(6, 166);
            this._lblAmount.Name = "_lblAmount";
            this._lblAmount.Size = new System.Drawing.Size(100, 15);
            this._lblAmount.TabIndex = 175;
            this._lblAmount.Text = "Amount";
            this._lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblOf
            // 
            this._lblOf.Location = new System.Drawing.Point(249, 163);
            this._lblOf.Name = "_lblOf";
            this._lblOf.Size = new System.Drawing.Size(20, 18);
            this._lblOf.TabIndex = 176;
            this._lblOf.Text = "of";
            this._lblOf.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // cboFreightType
            // 
            this.cboFreightType.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.cboFreightType.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboFreightType.Items.AddRange(new object[] {
            "Empty",
            "ISA",
            "Mixed",
            "PCS",
            "PSP",
            "Tsort"});
            this.cboFreightType.Location = new System.Drawing.Point(272, 163);
            this.cboFreightType.MaxDropDownItems = 5;
            this.cboFreightType.Name = "cboFreightType";
            this.cboFreightType.Size = new System.Drawing.Size(90, 21);
            this.cboFreightType.TabIndex = 202;
            this.cboFreightType.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // dlgInboundFreight
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(744, 347);
            this.Controls.Add(this.cboFreightType);
            this.Controls.Add(this._lblOf);
            this.Controls.Add(this.cboAmountType);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this._lblAmount);
            this.Controls.Add(this.txtDestinationLoc);
            this.Controls.Add(this.txtOriginLoc);
            this.Controls.Add(this.txtDropEmptyTrailerNumber);
            this.Controls.Add(this._lblDropEmptyTrailerNumber);
            this.Controls.Add(this.grpGate);
            this.Controls.Add(this.txtTrailerNumber);
            this.Controls.Add(this._lblTrailerNumber);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this._lblID);
            this.Controls.Add(this._lblScheduleDate);
            this.Controls.Add(this._lblDestination);
            this.Controls.Add(this.cboDestination);
            this.Controls.Add(this.dtpScheduledDeparture);
            this.Controls.Add(this._lblScheduledDeparture);
            this.Controls.Add(this._lblComments);
            this.Controls.Add(this.grpDispatch);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblOrigin);
            this.Controls.Add(this.cboOrigin);
            this.Controls.Add(this.dtpScheduledArrival);
            this.Controls.Add(this._lblScheduledArrival);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgInboundFreight";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Inbound Trip";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.mDestinations)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mOrigins)).EndInit();
            this.grpGate.ResumeLayout(false);
            this.grpDispatch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.mDrivers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriers)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
                //Load selection lists
                this.mOrigins.Merge(FreightGateway.GetLocations());
                this.mDestinations.Merge(FreightGateway.GetLocations());
                this.mCarriers.Merge(FreightGateway.GetCarriers());
                this.mDrivers.Merge(FreightGateway.GetDrivers());
				
				//Load controls
                this.Text = "Inbound Freight" + " (" + (!this.mIsNewSchedule ? this.mTrip.ID.ToString() : "New") + (this.mIsTemplate ? " Template)" : ")");
                this.lblID.Text = !this.mTrip.IsIDNull() ? this.mTrip.ID.ToString("00000000") : "";
                this.dtpScheduleDate.Value = !this.mTrip.IsScheduleDateNull() ? this.mTrip.ScheduleDate : DateTime.Today;
                this.cboOrigin.Text = !this.mTrip.IsOriginNull() ? this.mTrip.Origin : "";
                this.txtOriginLoc.Text = !this.mTrip.IsOriginLocationNull() ? this.mTrip.OriginLocation : "";
                this.cboDestination.Text = !this.mTrip.IsDestinationNull() ? this.mTrip.Destination : "";
                this.txtDestinationLoc.Text = !this.mTrip.IsDestinationLocationNull() ? this.mTrip.DestinationLocation : "";
                this.txtAmount.Text = !this.mTrip.IsAmountNull() ? this.mTrip.Amount.ToString() : "0";
                this.cboAmountType.Text = !this.mTrip.IsAmountTypeNull() ? this.mTrip.AmountType : "Cartons";
                this.cboFreightType.Text = !this.mTrip.IsFreightTypeNull() ? this.mTrip.FreightType : "Tsort";

                this.dtpScheduledDeparture.MinDate = DateTime.MinValue;
                this.dtpScheduledDeparture.MaxDate = DateTime.MaxValue;
                if (!this.mTrip.IsScheduledDepartureNull()) this.dtpScheduledDeparture.Value = this.mTrip.ScheduledDeparture;

                this.dtpScheduledArrival.MinDate = DateTime.MinValue;
                this.dtpScheduledArrival.MaxDate = DateTime.MaxValue;
                if (!this.mTrip.IsScheduledArrivalNull()) this.dtpScheduledArrival.Value = this.mTrip.ScheduledArrival;

                this.txtTrailerNumber.Text = !this.mTrip.IsDriverNameNull() ? this.mTrip.TrailerNumber : "";
                this.txtDropEmptyTrailerNumber.Text = !this.mTrip.IsDropEmptyTrailerNumberNull() ? this.mTrip.DropEmptyTrailerNumber : "";

                this.cboCarrier.Text = !this.mTrip.IsCarrierNameNull() ? this.mTrip.CarrierName : "";
                this.cboDriver.Text = !this.mTrip.IsDriverNameNull() ? this.mTrip.DriverName : "";
                this.chkConfirmed.Checked = !this.mTrip.IsConfirmedNull() ? this.mTrip.Confirmed : false;
                
                this.dtpActualDeparture.MinDate = DateTime.MinValue;
                this.dtpActualDeparture.MaxDate = DateTime.MaxValue;
                if (!this.mTrip.IsActualDepartureNull()) this.dtpActualDeparture.Value = this.mTrip.ActualDeparture;
                this.dtpActualDeparture.Checked = !this.mTrip.IsActualDepartureNull();

                this.dtpActualArrival.MinDate = DateTime.MinValue;
                this.dtpActualArrival.MaxDate = DateTime.MaxValue;
                if (!this.mTrip.IsActualArrivalNull()) this.dtpActualArrival.Value = this.mTrip.ActualArrival;
                this.dtpActualArrival.Checked = !this.mTrip.IsActualArrivalNull();

				this.txtComments.Text = !this.mTrip.IsCommentsNull() ? this.mTrip.Comments : "";
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
		}
        private void OnScheduleDateChanged(object sender,EventArgs e) {
            //Event handler for change in schedule date
            try {
                //Keep the scheduled departure/arrival dates same as schedule date (but preserve the time) for better sorting
                this.dtpScheduledArrival.Value = this.dtpScheduleDate.Value.Date + this.dtpScheduledArrival.Value.TimeOfDay;
                this.dtpScheduledDeparture.Value = this.dtpScheduleDate.Value.Date + this.dtpScheduledDeparture.Value.TimeOfDay;
            
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); }
        }
        private void OnScheduleChanged(object sender,EventArgs e) {
            //Event handler for change in scheduled departure/arrival times
            try {
                //Kick up the schdeuled arrival day (preserve time) if it is later than the scheduled departure
                if (this.dtpScheduledArrival.Value.CompareTo(this.dtpScheduledDeparture.Value) < 0)
                    this.dtpScheduledArrival.Value = this.dtpScheduleDate.Value.Date.AddDays(1) + this.dtpScheduledArrival.Value.TimeOfDay;
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
                this.cboOrigin.Enabled = true;
                this.txtOriginLoc.Enabled = true;
                this.cboDestination.Enabled = true;
                this.txtDestinationLoc.Enabled = true;
                this.txtAmount.Enabled = true;
                this.cboAmountType.Enabled = true;
                this.cboFreightType.Enabled = true;
                this.dtpScheduledDeparture.Enabled = true;
                this.dtpScheduledArrival.Enabled = true;
                this.txtTrailerNumber.Enabled = !this.mIsTemplate;
                this.txtDropEmptyTrailerNumber.Enabled = !this.mIsTemplate;
                this.cboCarrier.Enabled = true;
                this.cboDriver.Enabled = true;
                this.chkConfirmed.Enabled = !this.mIsTemplate && !this.mIsNewSchedule;
                this.dtpActualDeparture.Enabled = !this.mIsTemplate && !this.mIsNewSchedule;
                this.dtpActualArrival.Enabled = !this.mIsTemplate && !this.mIsNewSchedule;
                this.txtComments.Enabled = true;

                bool cancelled = !this.mTrip.IsCancelledNull() && this.mTrip.Cancelled.CompareTo(DateTime.MinValue) > 0;
                this.btnOk.Enabled = (access && !cancelled &&
                                        this.cboOrigin.Text.Trim().Length > 0 &&
                                        this.cboDestination.Text.Trim().Length > 0 &&
                                        this.dtpScheduledDeparture.Text.Trim().Length > 0 &&
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
                        break;
                    case "btnOk":
                        if (this.mTrip.IsIDNull()) {
                            this.mTrip.Created = DateTime.Now;
                            this.mTrip.CreateUserID = Environment.UserName;
                        }
                        this.mTrip.ScheduleDate = this.dtpScheduleDate.Value;
				        this.mTrip.Origin = this.cboOrigin.Text;
				        this.mTrip.OriginLocation = this.txtOriginLoc.Text;
				        this.mTrip.Destination = this.cboDestination.Text;
                        this.mTrip.DestinationLocation = this.txtDestinationLoc.Text;
                        if (this.txtAmount.Text.Trim().Length > 0) this.mTrip.Amount = int.Parse(this.txtAmount.Text);
                        this.mTrip.AmountType = this.cboAmountType.Text;
                        this.mTrip.FreightType = this.cboFreightType.Text;
                        this.mTrip.ScheduledDeparture = this.dtpScheduledDeparture.Value;
                        this.mTrip.ScheduledArrival = this.dtpScheduledArrival.Value;
                        this.mTrip.TrailerNumber = this.txtTrailerNumber.Text;
                        this.mTrip.DropEmptyTrailerNumber = this.txtDropEmptyTrailerNumber.Text;
                        this.mTrip.CarrierName = this.cboCarrier.Text;
                        this.mTrip.DriverName = this.cboDriver.Text;
                        this.mTrip.Confirmed = this.chkConfirmed.Checked;
                        if (this.dtpActualDeparture.Checked) this.mTrip.ActualDeparture = this.dtpActualDeparture.Value; else this.mTrip.SetActualDepartureNull();
                        if (this.dtpActualArrival.Checked) this.mTrip.ActualArrival = this.dtpActualArrival.Value; else this.mTrip.SetActualArrivalNull();
				        this.mTrip.Comments = this.txtComments.Text;
                        if (this.mIsTemplate) this.mTrip.IsTemplate = true;
                        this.mTrip.LastUpdated = DateTime.Now;
                        this.mTrip.UserID = Environment.UserName;

                        this.DialogResult = DialogResult.OK;
                        break;
                }
                this.Close();
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}

