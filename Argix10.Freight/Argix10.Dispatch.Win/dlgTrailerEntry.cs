using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Argix.Security;

namespace Argix.Freight {
    //
    public class dlgTrailerEntry:System.Windows.Forms.Form {
		//Members
        private DispatchDataset.TrailerLogTableRow mEntry = null;
        private bool mIsNewSchedule = true,mIsTemplate = false;

        #region Controls

        private System.Windows.Forms.Label _lblComments;
        private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.Label _lblInCarrier;
        private System.Windows.Forms.Label _lblInDriver;
		private System.Windows.Forms.Label _lblInDate;
		private System.Windows.Forms.DateTimePicker dtpInDate;
		private System.Windows.Forms.Label _lblInLoc;
		private System.Windows.Forms.TextBox txtInLoc;
		private System.Windows.Forms.Label _lblInSeal;
        private System.Windows.Forms.TextBox txtInSeal;
		private System.Windows.Forms.Label _lblOutDate;
		private System.Windows.Forms.DateTimePicker dtpOutDate;
		private System.Windows.Forms.Label _lblOutSeal;
		private System.Windows.Forms.TextBox txtOutSeal;
        private System.Windows.Forms.Label _lblOutCarrier;
        private System.Windows.Forms.Label _lblOutDriver;
		private System.Windows.Forms.Label _lblTrailer;
		private System.Windows.Forms.TextBox txtTrailer;
        private Button btnOk;
        private Button btnCancel;
        private DateTimePicker dtpScheduleDate;
        private Label lblID;
        private Label _lblID;
        private Label _lblScheduleDate;
        private GroupBox grpInbound;
        private GroupBox grpOutbound;
        private ComboBox cboInDriver;
        private DispatchDataset mCarriersIn;
        private DispatchDataset mDriversIn;
        private DispatchDataset mCarriersOut;
        private DispatchDataset mDriversOut;
        private ComboBox cboOutDriver;
        private ComboBox cboInCarrier;
        private ComboBox cboOutCarrier;
        private TextBox txtTDSNumber;
        private Label _lblTDSNumber;
        private TextBox txtBOLNumber;
        private Label _lblBOLNumber;
        private TabControl tabMain;
        private TabPage tabIncoming;
        private Label label1;
        private DateTimePicker dateTimePicker1;
        private Label label2;
        private TextBox textBox1;
        private Label label3;
        private TextBox textBox2;
        private Label label4;
        private ComboBox comboBox1;
        private Label label5;
        private TextBox txtInDriver;
        private TabPage tabMoves;
        private CheckBox chkMoveInProgress;
        private CheckBox chkLoadSheetReady;
        private UltraGrid grdMoves;
        private TabPage tabOutgoing;
        private CheckBox chkTrailerOut;
        private Label label6;
        private DateTimePicker dateTimePicker2;
        private Label label7;
        private TextBox textBox3;
        private Label label8;
        private ComboBox comboBox2;
        private Label label9;
        private TextBox txtOutDriver;
		private System.ComponentModel.IContainer components = null;
		#endregion

        public dlgTrailerEntry(DispatchDataset.TrailerLogTableRow entry,bool isTemplate = false) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
                this.mEntry = entry;
                this.mIsNewSchedule = this.mEntry.IsIDNull() || (!this.mEntry.IsIDNull() && this.mEntry.ID == 0);
                this.mIsTemplate = isTemplate;
            } 
			catch(Exception ex) { throw ex; }
		}
        protected override void Dispose(bool disposing) { if (disposing && (components != null)) { components.Dispose(); } base.Dispose(disposing); }
        #region Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            this._lblInCarrier = new System.Windows.Forms.Label();
            this._lblInDriver = new System.Windows.Forms.Label();
            this._lblComments = new System.Windows.Forms.Label();
            this.txtComments = new System.Windows.Forms.TextBox();
            this._lblInDate = new System.Windows.Forms.Label();
            this.dtpInDate = new System.Windows.Forms.DateTimePicker();
            this._lblInLoc = new System.Windows.Forms.Label();
            this.txtInLoc = new System.Windows.Forms.TextBox();
            this._lblInSeal = new System.Windows.Forms.Label();
            this.txtInSeal = new System.Windows.Forms.TextBox();
            this._lblOutDate = new System.Windows.Forms.Label();
            this.dtpOutDate = new System.Windows.Forms.DateTimePicker();
            this._lblOutSeal = new System.Windows.Forms.Label();
            this.txtOutSeal = new System.Windows.Forms.TextBox();
            this._lblOutCarrier = new System.Windows.Forms.Label();
            this._lblOutDriver = new System.Windows.Forms.Label();
            this._lblTrailer = new System.Windows.Forms.Label();
            this.txtTrailer = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.dtpScheduleDate = new System.Windows.Forms.DateTimePicker();
            this.lblID = new System.Windows.Forms.Label();
            this._lblID = new System.Windows.Forms.Label();
            this._lblScheduleDate = new System.Windows.Forms.Label();
            this.grpInbound = new System.Windows.Forms.GroupBox();
            this.txtTDSNumber = new System.Windows.Forms.TextBox();
            this._lblTDSNumber = new System.Windows.Forms.Label();
            this.cboInCarrier = new System.Windows.Forms.ComboBox();
            this.mCarriersIn = new Argix.DispatchDataset();
            this.cboInDriver = new System.Windows.Forms.ComboBox();
            this.mDriversIn = new Argix.DispatchDataset();
            this.grpOutbound = new System.Windows.Forms.GroupBox();
            this.txtBOLNumber = new System.Windows.Forms.TextBox();
            this._lblBOLNumber = new System.Windows.Forms.Label();
            this.cboOutCarrier = new System.Windows.Forms.ComboBox();
            this.mCarriersOut = new Argix.DispatchDataset();
            this.cboOutDriver = new System.Windows.Forms.ComboBox();
            this.mDriversOut = new Argix.DispatchDataset();
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabIncoming = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtInDriver = new System.Windows.Forms.TextBox();
            this.tabMoves = new System.Windows.Forms.TabPage();
            this.chkMoveInProgress = new System.Windows.Forms.CheckBox();
            this.chkLoadSheetReady = new System.Windows.Forms.CheckBox();
            this.grdMoves = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.tabOutgoing = new System.Windows.Forms.TabPage();
            this.chkTrailerOut = new System.Windows.Forms.CheckBox();
            this.label6 = new System.Windows.Forms.Label();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtOutDriver = new System.Windows.Forms.TextBox();
            this.grpInbound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriersIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriversIn)).BeginInit();
            this.grpOutbound.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriersOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriversOut)).BeginInit();
            this.tabMain.SuspendLayout();
            this.tabIncoming.SuspendLayout();
            this.tabMoves.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdMoves)).BeginInit();
            this.tabOutgoing.SuspendLayout();
            this.SuspendLayout();
            // 
            // _lblInCarrier
            // 
            this._lblInCarrier.Location = new System.Drawing.Point(6, 60);
            this._lblInCarrier.Name = "_lblInCarrier";
            this._lblInCarrier.Size = new System.Drawing.Size(96, 17);
            this._lblInCarrier.TabIndex = 138;
            this._lblInCarrier.Text = "Carrier";
            this._lblInCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblInDriver
            // 
            this._lblInDriver.Location = new System.Drawing.Point(6, 88);
            this._lblInDriver.Name = "_lblInDriver";
            this._lblInDriver.Size = new System.Drawing.Size(96, 17);
            this._lblInDriver.TabIndex = 137;
            this._lblInDriver.Text = "Driver";
            this._lblInDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblComments
            // 
            this._lblComments.Location = new System.Drawing.Point(279, 51);
            this._lblComments.Name = "_lblComments";
            this._lblComments.Size = new System.Drawing.Size(96, 17);
            this._lblComments.TabIndex = 129;
            this._lblComments.Text = "Comments";
            this._lblComments.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtComments
            // 
            this.txtComments.Location = new System.Drawing.Point(379, 51);
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(254, 20);
            this.txtComments.TabIndex = 3;
            this.txtComments.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblInDate
            // 
            this._lblInDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblInDate.Location = new System.Drawing.Point(6, 27);
            this._lblInDate.Name = "_lblInDate";
            this._lblInDate.Size = new System.Drawing.Size(96, 17);
            this._lblInDate.TabIndex = 6;
            this._lblInDate.Text = "Inbound";
            this._lblInDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpInDate
            // 
            this.dtpInDate.CustomFormat = "MMM dd, yyyy   hh:mm tt";
            this.dtpInDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpInDate.Location = new System.Drawing.Point(108, 27);
            this.dtpInDate.Name = "dtpInDate";
            this.dtpInDate.ShowUpDown = true;
            this.dtpInDate.Size = new System.Drawing.Size(168, 20);
            this.dtpInDate.TabIndex = 0;
            this.dtpInDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblInLoc
            // 
            this._lblInLoc.Location = new System.Drawing.Point(6, 178);
            this._lblInLoc.Name = "_lblInLoc";
            this._lblInLoc.Size = new System.Drawing.Size(96, 17);
            this._lblInLoc.TabIndex = 142;
            this._lblInLoc.Text = "Door\\Yard Loc";
            this._lblInLoc.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInLoc
            // 
            this.txtInLoc.Location = new System.Drawing.Point(108, 178);
            this.txtInLoc.Name = "txtInLoc";
            this.txtInLoc.Size = new System.Drawing.Size(150, 20);
            this.txtInLoc.TabIndex = 5;
            this.txtInLoc.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblInSeal
            // 
            this._lblInSeal.Location = new System.Drawing.Point(6, 122);
            this._lblInSeal.Name = "_lblInSeal";
            this._lblInSeal.Size = new System.Drawing.Size(96, 17);
            this._lblInSeal.TabIndex = 140;
            this._lblInSeal.Text = "Seal";
            this._lblInSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInSeal
            // 
            this.txtInSeal.Location = new System.Drawing.Point(108, 122);
            this.txtInSeal.Name = "txtInSeal";
            this.txtInSeal.Size = new System.Drawing.Size(100, 20);
            this.txtInSeal.TabIndex = 3;
            this.txtInSeal.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblOutDate
            // 
            this._lblOutDate.Location = new System.Drawing.Point(6, 27);
            this._lblOutDate.Name = "_lblOutDate";
            this._lblOutDate.Size = new System.Drawing.Size(96, 17);
            this._lblOutDate.TabIndex = 154;
            this._lblOutDate.Text = "Outbound";
            this._lblOutDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpOutDate
            // 
            this.dtpOutDate.CustomFormat = "MMM dd, yyyy   hh:mm tt";
            this.dtpOutDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpOutDate.Location = new System.Drawing.Point(108, 27);
            this.dtpOutDate.Name = "dtpOutDate";
            this.dtpOutDate.ShowCheckBox = true;
            this.dtpOutDate.ShowUpDown = true;
            this.dtpOutDate.Size = new System.Drawing.Size(170, 20);
            this.dtpOutDate.TabIndex = 0;
            this.dtpOutDate.ValueChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblOutSeal
            // 
            this._lblOutSeal.Location = new System.Drawing.Point(6, 122);
            this._lblOutSeal.Name = "_lblOutSeal";
            this._lblOutSeal.Size = new System.Drawing.Size(96, 17);
            this._lblOutSeal.TabIndex = 150;
            this._lblOutSeal.Text = "Seal";
            this._lblOutSeal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOutSeal
            // 
            this.txtOutSeal.Location = new System.Drawing.Point(108, 122);
            this.txtOutSeal.Name = "txtOutSeal";
            this.txtOutSeal.Size = new System.Drawing.Size(100, 20);
            this.txtOutSeal.TabIndex = 3;
            this.txtOutSeal.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblOutCarrier
            // 
            this._lblOutCarrier.Location = new System.Drawing.Point(6, 60);
            this._lblOutCarrier.Name = "_lblOutCarrier";
            this._lblOutCarrier.Size = new System.Drawing.Size(96, 17);
            this._lblOutCarrier.TabIndex = 148;
            this._lblOutCarrier.Text = "Carrier";
            this._lblOutCarrier.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblOutDriver
            // 
            this._lblOutDriver.Location = new System.Drawing.Point(6, 88);
            this._lblOutDriver.Name = "_lblOutDriver";
            this._lblOutDriver.Size = new System.Drawing.Size(96, 17);
            this._lblOutDriver.TabIndex = 147;
            this._lblOutDriver.Text = "Driver";
            this._lblOutDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblTrailer
            // 
            this._lblTrailer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTrailer.Location = new System.Drawing.Point(6, 51);
            this._lblTrailer.Name = "_lblTrailer";
            this._lblTrailer.Size = new System.Drawing.Size(100, 16);
            this._lblTrailer.TabIndex = 146;
            this._lblTrailer.Text = "Trailer#";
            this._lblTrailer.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTrailer
            // 
            this.txtTrailer.Location = new System.Drawing.Point(112, 51);
            this.txtTrailer.Name = "txtTrailer";
            this.txtTrailer.Size = new System.Drawing.Size(100, 20);
            this.txtTrailer.TabIndex = 2;
            this.txtTrailer.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(1063, 312);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 6;
            this.btnOk.Text = "&Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(1144, 312);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCommandClick);
            // 
            // dtpScheduleDate
            // 
            this.dtpScheduleDate.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpScheduleDate.Location = new System.Drawing.Point(112, 18);
            this.dtpScheduleDate.Name = "dtpScheduleDate";
            this.dtpScheduleDate.Size = new System.Drawing.Size(100, 20);
            this.dtpScheduleDate.TabIndex = 1;
            // 
            // lblID
            // 
            this.lblID.Location = new System.Drawing.Point(627, 18);
            this.lblID.Name = "lblID";
            this.lblID.Size = new System.Drawing.Size(100, 16);
            this.lblID.TabIndex = 171;
            this.lblID.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // _lblID
            // 
            this._lblID.Location = new System.Drawing.Point(521, 18);
            this._lblID.Name = "_lblID";
            this._lblID.Size = new System.Drawing.Size(100, 15);
            this._lblID.TabIndex = 169;
            this._lblID.Text = "Entry#";
            this._lblID.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblScheduleDate
            // 
            this._lblScheduleDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblScheduleDate.Location = new System.Drawing.Point(6, 18);
            this._lblScheduleDate.Name = "_lblScheduleDate";
            this._lblScheduleDate.Size = new System.Drawing.Size(100, 15);
            this._lblScheduleDate.TabIndex = 170;
            this._lblScheduleDate.Text = "Schedule Date";
            this._lblScheduleDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // grpInbound
            // 
            this.grpInbound.Controls.Add(this.txtTDSNumber);
            this.grpInbound.Controls.Add(this._lblTDSNumber);
            this.grpInbound.Controls.Add(this.cboInCarrier);
            this.grpInbound.Controls.Add(this.cboInDriver);
            this.grpInbound.Controls.Add(this._lblInDate);
            this.grpInbound.Controls.Add(this.dtpInDate);
            this.grpInbound.Controls.Add(this._lblInLoc);
            this.grpInbound.Controls.Add(this._lblInDriver);
            this.grpInbound.Controls.Add(this.txtInLoc);
            this.grpInbound.Controls.Add(this._lblInSeal);
            this.grpInbound.Controls.Add(this._lblInCarrier);
            this.grpInbound.Controls.Add(this.txtInSeal);
            this.grpInbound.Location = new System.Drawing.Point(6, 84);
            this.grpInbound.Name = "grpInbound";
            this.grpInbound.Size = new System.Drawing.Size(360, 215);
            this.grpInbound.TabIndex = 4;
            this.grpInbound.TabStop = false;
            this.grpInbound.Text = "Inbound";
            // 
            // txtTDSNumber
            // 
            this.txtTDSNumber.Location = new System.Drawing.Point(108, 149);
            this.txtTDSNumber.Name = "txtTDSNumber";
            this.txtTDSNumber.Size = new System.Drawing.Size(100, 20);
            this.txtTDSNumber.TabIndex = 4;
            this.txtTDSNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblTDSNumber
            // 
            this._lblTDSNumber.Location = new System.Drawing.Point(23, 149);
            this._lblTDSNumber.Name = "_lblTDSNumber";
            this._lblTDSNumber.Size = new System.Drawing.Size(80, 14);
            this._lblTDSNumber.TabIndex = 155;
            this._lblTDSNumber.Text = "TDS#";
            this._lblTDSNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboInCarrier
            // 
            this.cboInCarrier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboInCarrier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboInCarrier.DataSource = this.mCarriersIn;
            this.cboInCarrier.DisplayMember = "CarrierTable.Description";
            this.cboInCarrier.FormattingEnabled = true;
            this.cboInCarrier.Location = new System.Drawing.Point(108, 60);
            this.cboInCarrier.Name = "cboInCarrier";
            this.cboInCarrier.Size = new System.Drawing.Size(240, 21);
            this.cboInCarrier.TabIndex = 1;
            this.cboInCarrier.ValueMember = "CarrierTable.Description";
            this.cboInCarrier.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mCarriersIn
            // 
            this.mCarriersIn.DataSetName = "DispatchDataset";
            this.mCarriersIn.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboInDriver
            // 
            this.cboInDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboInDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboInDriver.DataSource = this.mDriversIn;
            this.cboInDriver.DisplayMember = "DriverTable.Description";
            this.cboInDriver.FormattingEnabled = true;
            this.cboInDriver.Location = new System.Drawing.Point(108, 88);
            this.cboInDriver.Name = "cboInDriver";
            this.cboInDriver.Size = new System.Drawing.Size(240, 21);
            this.cboInDriver.TabIndex = 2;
            this.cboInDriver.ValueMember = "DriverTable.Description";
            this.cboInDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mDriversIn
            // 
            this.mDriversIn.DataSetName = "DispatchDataset";
            this.mDriversIn.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // grpOutbound
            // 
            this.grpOutbound.Controls.Add(this.txtBOLNumber);
            this.grpOutbound.Controls.Add(this._lblBOLNumber);
            this.grpOutbound.Controls.Add(this.cboOutCarrier);
            this.grpOutbound.Controls.Add(this.cboOutDriver);
            this.grpOutbound.Controls.Add(this._lblOutDate);
            this.grpOutbound.Controls.Add(this.dtpOutDate);
            this.grpOutbound.Controls.Add(this._lblOutSeal);
            this.grpOutbound.Controls.Add(this._lblOutDriver);
            this.grpOutbound.Controls.Add(this.txtOutSeal);
            this.grpOutbound.Controls.Add(this._lblOutCarrier);
            this.grpOutbound.Location = new System.Drawing.Point(379, 84);
            this.grpOutbound.Name = "grpOutbound";
            this.grpOutbound.Size = new System.Drawing.Size(360, 215);
            this.grpOutbound.TabIndex = 5;
            this.grpOutbound.TabStop = false;
            this.grpOutbound.Text = "Outbound";
            // 
            // txtBOLNumber
            // 
            this.txtBOLNumber.Location = new System.Drawing.Point(108, 149);
            this.txtBOLNumber.Name = "txtBOLNumber";
            this.txtBOLNumber.Size = new System.Drawing.Size(100, 20);
            this.txtBOLNumber.TabIndex = 4;
            this.txtBOLNumber.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBOLNumber
            // 
            this._lblBOLNumber.Location = new System.Drawing.Point(23, 149);
            this._lblBOLNumber.Name = "_lblBOLNumber";
            this._lblBOLNumber.Size = new System.Drawing.Size(80, 14);
            this._lblBOLNumber.TabIndex = 158;
            this._lblBOLNumber.Text = "BOL#";
            this._lblBOLNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboOutCarrier
            // 
            this.cboOutCarrier.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboOutCarrier.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOutCarrier.DataSource = this.mCarriersOut;
            this.cboOutCarrier.DisplayMember = "CarrierTable.Description";
            this.cboOutCarrier.FormattingEnabled = true;
            this.cboOutCarrier.Location = new System.Drawing.Point(108, 60);
            this.cboOutCarrier.Name = "cboOutCarrier";
            this.cboOutCarrier.Size = new System.Drawing.Size(240, 21);
            this.cboOutCarrier.TabIndex = 1;
            this.cboOutCarrier.ValueMember = "CarrierTable.Description";
            this.cboOutCarrier.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mCarriersOut
            // 
            this.mCarriersOut.DataSetName = "DispatchDataset";
            this.mCarriersOut.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // cboOutDriver
            // 
            this.cboOutDriver.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboOutDriver.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboOutDriver.DataSource = this.mDriversOut;
            this.cboOutDriver.DisplayMember = "DriverTable.Description";
            this.cboOutDriver.FormattingEnabled = true;
            this.cboOutDriver.Location = new System.Drawing.Point(108, 88);
            this.cboOutDriver.Name = "cboOutDriver";
            this.cboOutDriver.Size = new System.Drawing.Size(240, 21);
            this.cboOutDriver.TabIndex = 2;
            this.cboOutDriver.ValueMember = "DriverTable.Description";
            this.cboOutDriver.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // mDriversOut
            // 
            this.mDriversOut.DataSetName = "DispatchDataset";
            this.mDriversOut.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabIncoming);
            this.tabMain.Controls.Add(this.tabMoves);
            this.tabMain.Controls.Add(this.tabOutgoing);
            this.tabMain.Location = new System.Drawing.Point(757, 42);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(654, 257);
            this.tabMain.TabIndex = 172;
            // 
            // tabIncoming
            // 
            this.tabIncoming.Controls.Add(this.label1);
            this.tabIncoming.Controls.Add(this.dateTimePicker1);
            this.tabIncoming.Controls.Add(this.label2);
            this.tabIncoming.Controls.Add(this.textBox1);
            this.tabIncoming.Controls.Add(this.label3);
            this.tabIncoming.Controls.Add(this.textBox2);
            this.tabIncoming.Controls.Add(this.label4);
            this.tabIncoming.Controls.Add(this.comboBox1);
            this.tabIncoming.Controls.Add(this.label5);
            this.tabIncoming.Controls.Add(this.txtInDriver);
            this.tabIncoming.Location = new System.Drawing.Point(4, 22);
            this.tabIncoming.Name = "tabIncoming";
            this.tabIncoming.Size = new System.Drawing.Size(646, 231);
            this.tabIncoming.TabIndex = 0;
            this.tabIncoming.Text = "Incoming";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 18);
            this.label1.TabIndex = 144;
            this.label1.Text = "Incoming:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.CustomFormat = "MMM dd, yyyy   hh:mm tt";
            this.dateTimePicker1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker1.Location = new System.Drawing.Point(123, 21);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.ShowUpDown = true;
            this.dateTimePicker1.Size = new System.Drawing.Size(192, 20);
            this.dateTimePicker1.TabIndex = 143;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(15, 141);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(96, 18);
            this.label2.TabIndex = 142;
            this.label2.Text = "Door\\Yard Loc: ";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(123, 141);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(192, 20);
            this.textBox1.TabIndex = 141;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(15, 111);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(96, 18);
            this.label3.TabIndex = 140;
            this.label3.Text = "Seal: ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(123, 111);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(192, 20);
            this.textBox2.TabIndex = 139;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(15, 51);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 18);
            this.label4.TabIndex = 138;
            this.label4.Text = "Carrier: ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox1
            // 
            this.comboBox1.Location = new System.Drawing.Point(123, 51);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(192, 21);
            this.comboBox1.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(15, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(96, 18);
            this.label5.TabIndex = 137;
            this.label5.Text = "Driver: ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInDriver
            // 
            this.txtInDriver.Location = new System.Drawing.Point(123, 81);
            this.txtInDriver.Name = "txtInDriver";
            this.txtInDriver.Size = new System.Drawing.Size(192, 20);
            this.txtInDriver.TabIndex = 0;
            // 
            // tabMoves
            // 
            this.tabMoves.Controls.Add(this.chkMoveInProgress);
            this.tabMoves.Controls.Add(this.chkLoadSheetReady);
            this.tabMoves.Controls.Add(this.grdMoves);
            this.tabMoves.Location = new System.Drawing.Point(4, 22);
            this.tabMoves.Name = "tabMoves";
            this.tabMoves.Size = new System.Drawing.Size(646, 175);
            this.tabMoves.TabIndex = 1;
            this.tabMoves.Text = "Moves";
            // 
            // chkMoveInProgress
            // 
            this.chkMoveInProgress.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.chkMoveInProgress.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            this.chkMoveInProgress.Location = new System.Drawing.Point(486, 6);
            this.chkMoveInProgress.Name = "chkMoveInProgress";
            this.chkMoveInProgress.Size = new System.Drawing.Size(144, 16);
            this.chkMoveInProgress.TabIndex = 157;
            this.chkMoveInProgress.Text = "Move In Progress?";
            this.chkMoveInProgress.UseVisualStyleBackColor = false;
            // 
            // chkLoadSheetReady
            // 
            this.chkLoadSheetReady.Location = new System.Drawing.Point(6, 153);
            this.chkLoadSheetReady.Name = "chkLoadSheetReady";
            this.chkLoadSheetReady.Size = new System.Drawing.Size(192, 21);
            this.chkLoadSheetReady.TabIndex = 156;
            this.chkLoadSheetReady.Text = "Load Sheet Ready?";
            // 
            // grdMoves
            // 
            this.grdMoves.DataMember = "TrailerLogTable.TrailerLogTable_TrailerMoveTable";
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdMoves.DisplayLayout.Appearance = appearance1;
            appearance2.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance2.FontData.BoldAsString = "True";
            appearance2.FontData.Name = "Verdana";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance2.TextHAlignAsString = "Left";
            this.grdMoves.DisplayLayout.CaptionAppearance = appearance2;
            this.grdMoves.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdMoves.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdMoves.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdMoves.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Verdana";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.TextHAlignAsString = "Left";
            this.grdMoves.DisplayLayout.Override.HeaderAppearance = appearance3;
            this.grdMoves.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdMoves.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance4.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdMoves.DisplayLayout.Override.RowAppearance = appearance4;
            this.grdMoves.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdMoves.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdMoves.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdMoves.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdMoves.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdMoves.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdMoves.Location = new System.Drawing.Point(0, 3);
            this.grdMoves.Name = "grdMoves";
            this.grdMoves.Size = new System.Drawing.Size(639, 144);
            this.grdMoves.TabIndex = 0;
            this.grdMoves.Text = "Moves";
            this.grdMoves.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // tabOutgoing
            // 
            this.tabOutgoing.Controls.Add(this.chkTrailerOut);
            this.tabOutgoing.Controls.Add(this.label6);
            this.tabOutgoing.Controls.Add(this.dateTimePicker2);
            this.tabOutgoing.Controls.Add(this.label7);
            this.tabOutgoing.Controls.Add(this.textBox3);
            this.tabOutgoing.Controls.Add(this.label8);
            this.tabOutgoing.Controls.Add(this.comboBox2);
            this.tabOutgoing.Controls.Add(this.label9);
            this.tabOutgoing.Controls.Add(this.txtOutDriver);
            this.tabOutgoing.Location = new System.Drawing.Point(4, 22);
            this.tabOutgoing.Name = "tabOutgoing";
            this.tabOutgoing.Size = new System.Drawing.Size(646, 175);
            this.tabOutgoing.TabIndex = 2;
            this.tabOutgoing.Text = "Outgoing";
            // 
            // chkTrailerOut
            // 
            this.chkTrailerOut.Location = new System.Drawing.Point(447, 141);
            this.chkTrailerOut.Name = "chkTrailerOut";
            this.chkTrailerOut.Size = new System.Drawing.Size(192, 21);
            this.chkTrailerOut.TabIndex = 155;
            this.chkTrailerOut.Text = "Trailer completed and out?";
            // 
            // label6
            // 
            this.label6.Location = new System.Drawing.Point(15, 21);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(96, 18);
            this.label6.TabIndex = 154;
            this.label6.Text = "Outgoing:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.CustomFormat = "MMM dd, yyyy   hh:mm tt";
            this.dateTimePicker2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dateTimePicker2.Location = new System.Drawing.Point(123, 21);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.ShowUpDown = true;
            this.dateTimePicker2.Size = new System.Drawing.Size(192, 20);
            this.dateTimePicker2.TabIndex = 153;
            // 
            // label7
            // 
            this.label7.Location = new System.Drawing.Point(15, 111);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(96, 18);
            this.label7.TabIndex = 150;
            this.label7.Text = "Seal: ";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(123, 111);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(192, 20);
            this.textBox3.TabIndex = 149;
            // 
            // label8
            // 
            this.label8.Location = new System.Drawing.Point(15, 51);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(96, 18);
            this.label8.TabIndex = 148;
            this.label8.Text = "Carrier: ";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // comboBox2
            // 
            this.comboBox2.Location = new System.Drawing.Point(123, 51);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(192, 21);
            this.comboBox2.TabIndex = 146;
            // 
            // label9
            // 
            this.label9.Location = new System.Drawing.Point(15, 81);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 18);
            this.label9.TabIndex = 147;
            this.label9.Text = "Driver: ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOutDriver
            // 
            this.txtOutDriver.Location = new System.Drawing.Point(123, 81);
            this.txtOutDriver.Name = "txtOutDriver";
            this.txtOutDriver.Size = new System.Drawing.Size(192, 20);
            this.txtOutDriver.TabIndex = 145;
            // 
            // dlgTrailerEntry
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(1231, 347);
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.grpOutbound);
            this.Controls.Add(this.grpInbound);
            this.Controls.Add(this.dtpScheduleDate);
            this.Controls.Add(this.lblID);
            this.Controls.Add(this._lblID);
            this.Controls.Add(this._lblScheduleDate);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this._lblComments);
            this.Controls.Add(this._lblTrailer);
            this.Controls.Add(this.txtTrailer);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgTrailerEntry";
            this.Text = "Trailer Log Entry";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.grpInbound.ResumeLayout(false);
            this.grpInbound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriersIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriversIn)).EndInit();
            this.grpOutbound.ResumeLayout(false);
            this.grpOutbound.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.mCarriersOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mDriversOut)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.tabIncoming.ResumeLayout(false);
            this.tabIncoming.PerformLayout();
            this.tabMoves.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdMoves)).EndInit();
            this.tabOutgoing.ResumeLayout(false);
            this.tabOutgoing.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
			this.Cursor = Cursors.WaitCursor;
			try {
				//Load selection lists
                this.mCarriersIn.Merge(FreightGateway.GetCarriers());
                this.mDriversIn.Merge(FreightGateway.GetDrivers());
                this.mCarriersOut.Merge(FreightGateway.GetCarriers());
                this.mDriversOut.Merge(FreightGateway.GetDrivers());

                //Load controls
                this.Text = "Trailer Log Entry" + " (" + (!this.mIsNewSchedule ? this.mEntry.ID.ToString() : "New") + (this.mIsTemplate ? " Template)" : ")");
                this.lblID.Text = !this.mEntry.IsIDNull() ? this.mEntry.ID.ToString("00000000") : "";
                this.dtpScheduleDate.Value = !this.mEntry.IsScheduleDateNull() ? this.mEntry.ScheduleDate : DateTime.Today;
				this.txtTrailer.Text = !this.mEntry.IsTrailerNumberNull() ? this.mEntry.TrailerNumber : "";

                this.dtpInDate.MinDate = DateTime.MinValue;
                this.dtpInDate.MaxDate = DateTime.MaxValue;
                if (!this.mEntry.IsInboundDateNull()) this.dtpInDate.Value = this.mEntry.InboundDate; else this.dtpInDate.Value = DateTime.Now;
                this.cboInCarrier.Text = !this.mEntry.IsInboundCarrierNull() ? this.mEntry.InboundCarrier : "";
                this.cboInDriver.Text = !this.mEntry.IsInboundDriverNameNull() ? this.mEntry.InboundDriverName : "";
                this.txtInSeal.Text = !this.mEntry.IsInboundSealNull() ? this.mEntry.InboundSeal : "";
                this.txtTDSNumber.Text = !this.mEntry.IsTDSNumberNull() ? this.mEntry.TDSNumber : "";
                this.txtInLoc.Text = !this.mEntry.IsInitialYardLocationNull() ? this.mEntry.InitialYardLocation : "";

                this.dtpOutDate.MinDate = DateTime.MinValue;
                this.dtpOutDate.MaxDate = DateTime.MaxValue;
                if (!this.mEntry.IsOutboundDateNull()) this.dtpOutDate.Value = this.mEntry.OutboundDate;
                this.dtpOutDate.Checked = !this.mEntry.IsOutboundDateNull();
                this.cboOutCarrier.Text = !this.mEntry.IsOutboundCarrierNull() ? this.mEntry.OutboundCarrier : "";
                this.cboOutDriver.Text = !this.mEntry.IsOutboundDriverNameNull() ? this.mEntry.OutboundDriverName : "";
                this.txtOutSeal.Text = !this.mEntry.IsOutboundSealNull() ? this.mEntry.OutboundSeal : "";
                this.txtBOLNumber.Text = !this.mEntry.IsBOLNumberNull() ? this.mEntry.BOLNumber : "";
                this.txtComments.Text = !this.mEntry.IsCommentsNull() ? this.mEntry.Comments : "";
			}
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
            finally { OnValidateForm(null,null); this.Cursor = Cursors.Default; }
        }
		private void OnValidateForm(object sender, EventArgs e) {
			//Set user services
			try {
                //Set services
                bool access = RoleServiceGateway.IsDispatchSupervisor || RoleServiceGateway.IsDispatchClerk || RoleServiceGateway.IsWindowClerk || RoleServiceGateway.IsSafetySupervisor;

                this.dtpScheduleDate.Enabled = !this.mIsTemplate;
                this.txtTrailer.Enabled = true;
                this.dtpInDate.Enabled = true;
                this.cboInCarrier.Enabled = true;
                this.cboInDriver.Enabled = true;
                this.txtInSeal.Enabled = true;
                this.txtTDSNumber.Enabled = true;
                this.txtInLoc.Enabled = true;
                this.dtpOutDate.Enabled = true;
                this.cboOutCarrier.Enabled = true;
                this.cboOutDriver.Enabled = true;
                this.txtOutSeal.Enabled = true;
                this.txtBOLNumber.Enabled = true;
                this.txtComments.Enabled = true;

                bool cancelled = !this.mEntry.IsCancelledNull() && this.mEntry.Cancelled.CompareTo(DateTime.MinValue) > 0;
                this.btnOk.Enabled = (access && !cancelled &&
                                        this.txtTrailer.Text.Trim().Length > 0 && this.dtpInDate.Value > DateTime.MinValue);
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
                        if (this.mEntry.IsIDNull()) {
                            this.mEntry.Created = DateTime.Now;
                            this.mEntry.CreateUserID = Environment.UserName;
                        }

                        this.mEntry.ScheduleDate = this.dtpScheduleDate.Value;
				        this.mEntry.TrailerNumber = this.txtTrailer.Text;
                        this.mEntry.InboundDate = this.dtpInDate.Value;
				        this.mEntry.InboundCarrier = this.cboInCarrier.Text;
				        this.mEntry.InboundDriverName = this.cboInDriver.Text;
				        this.mEntry.InboundSeal = this.txtInSeal.Text;
				        this.mEntry.TDSNumber = this.txtTDSNumber.Text;
				        this.mEntry.InitialYardLocation = this.txtInLoc.Text;
                        if (this.dtpOutDate.Checked) this.mEntry.OutboundDate = this.dtpOutDate.Value; else this.mEntry.SetOutboundDateNull();
				        this.mEntry.OutboundCarrier = this.cboOutCarrier.Text;
				        this.mEntry.OutboundDriverName = this.cboOutDriver.Text;
				        this.mEntry.OutboundSeal = this.txtOutSeal.Text;
                        this.mEntry.BOLNumber = this.txtBOLNumber.Text;
				        this.mEntry.Comments = this.txtComments.Text;
                        this.mEntry.LastUpdated = DateTime.Now;
                        this.mEntry.UserID = Environment.UserName;
                        
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

