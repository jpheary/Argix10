//	File:	dlgagentsummary.cs
//	Author:	J. Heary
//	Date:	10/28/08
//	Desc:	Modal dialog for agent summary views.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;
using Argix.Windows;
using Infragistics.Shared;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;

namespace Argix.Freight {
	//
	public class dlgAgentSummary : System.Windows.Forms.Form {
		//Members
        private int mTerminalID=0;
        private string mTerminalName="";
		private UltraGridSvc mGridSvc=null;
		
        #region Controls

        private Infragistics.Win.UltraWinGrid.UltraGrid grdAgentSummary;
		private System.Windows.Forms.Button cmdPrint;
        private System.Windows.Forms.Button cmdClose;
        private FreightDataset mTLs;
        private IContainer components;
        #endregion

        public dlgAgentSummary(int terminalID, string terminalName) {
			//Constructor			
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.Text = "Argix Direct " + App.Product;
                this.mTerminalID = terminalID;
                this.mTerminalName = terminalName;
				this.mGridSvc = new UltraGridSvc(this.grdAgentSummary);
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) { if(disposing) { if(components != null) components.Dispose(); } base.Dispose( disposing ); }
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            Infragistics.Win.Appearance appearance1 = new Infragistics.Win.Appearance();
            Infragistics.Win.UltraWinGrid.UltraGridBand ultraGridBand1 = new Infragistics.Win.UltraWinGrid.UltraGridBand("TLTable", -1);
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn22 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TerminalID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn23 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Zone");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn24 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn25 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("AgentName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn26 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn27 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("TLDate");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn28 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CloseNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn29 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientNumber");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn30 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ClientName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn31 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationID");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn32 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("ShipToLocationName");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn33 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Lane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn34 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("SmallLane");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn35 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cartons");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn36 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Pallets");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn37 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Weight");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn38 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("Cube");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn39 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("WeightPercent");
            Infragistics.Win.UltraWinGrid.UltraGridColumn ultraGridColumn40 = new Infragistics.Win.UltraWinGrid.UltraGridColumn("CubePercent");
            Infragistics.Win.Appearance appearance2 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance3 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance4 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance5 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance6 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance7 = new Infragistics.Win.Appearance();
            Infragistics.Win.Appearance appearance8 = new Infragistics.Win.Appearance();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAgentSummary));
            this.grdAgentSummary = new Infragistics.Win.UltraWinGrid.UltraGrid();
            this.cmdPrint = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.mTLs = new Argix.FreightDataset();
            ((System.ComponentModel.ISupportInitialize)(this.grdAgentSummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLs)).BeginInit();
            this.SuspendLayout();
            // 
            // grdAgentSummary
            // 
            this.grdAgentSummary.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.grdAgentSummary.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdAgentSummary.DataMember = "TLTable";
            this.grdAgentSummary.DataSource = this.mTLs;
            appearance1.BackColor = System.Drawing.SystemColors.Window;
            appearance1.FontData.Name = "Verdana";
            appearance1.FontData.SizeInPoints = 8F;
            appearance1.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance1.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.Appearance = appearance1;
            ultraGridBand1.AddButtonCaption = "ClientViewTable";
            ultraGridColumn22.Header.VisiblePosition = 0;
            ultraGridColumn22.Hidden = true;
            ultraGridColumn23.Header.VisiblePosition = 1;
            ultraGridColumn24.Header.VisiblePosition = 2;
            ultraGridColumn25.Header.VisiblePosition = 3;
            ultraGridColumn26.Header.VisiblePosition = 4;
            ultraGridColumn27.Header.VisiblePosition = 5;
            ultraGridColumn28.Header.VisiblePosition = 6;
            ultraGridColumn29.Header.VisiblePosition = 7;
            ultraGridColumn30.Header.VisiblePosition = 8;
            ultraGridColumn31.Header.VisiblePosition = 9;
            ultraGridColumn32.Header.VisiblePosition = 10;
            ultraGridColumn33.Header.VisiblePosition = 11;
            ultraGridColumn34.Header.VisiblePosition = 12;
            ultraGridColumn35.Header.VisiblePosition = 13;
            ultraGridColumn36.Header.VisiblePosition = 14;
            ultraGridColumn37.Header.VisiblePosition = 15;
            ultraGridColumn38.Header.VisiblePosition = 16;
            ultraGridColumn39.Header.VisiblePosition = 17;
            ultraGridColumn40.Header.VisiblePosition = 18;
            ultraGridBand1.Columns.AddRange(new object[] {
            ultraGridColumn22,
            ultraGridColumn23,
            ultraGridColumn24,
            ultraGridColumn25,
            ultraGridColumn26,
            ultraGridColumn27,
            ultraGridColumn28,
            ultraGridColumn29,
            ultraGridColumn30,
            ultraGridColumn31,
            ultraGridColumn32,
            ultraGridColumn33,
            ultraGridColumn34,
            ultraGridColumn35,
            ultraGridColumn36,
            ultraGridColumn37,
            ultraGridColumn38,
            ultraGridColumn39,
            ultraGridColumn40});
            appearance2.BackColor = System.Drawing.SystemColors.ActiveCaption;
            appearance2.FontData.Name = "Arial";
            appearance2.FontData.SizeInPoints = 8F;
            appearance2.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            ultraGridBand1.Override.ActiveRowAppearance = appearance2;
            appearance3.BackColor = System.Drawing.SystemColors.Control;
            appearance3.FontData.BoldAsString = "True";
            appearance3.FontData.Name = "Arial";
            appearance3.FontData.SizeInPoints = 8F;
            appearance3.ForeColor = System.Drawing.SystemColors.ControlText;
            appearance3.TextHAlignAsString = "Left";
            ultraGridBand1.Override.HeaderAppearance = appearance3;
            appearance4.BackColor = System.Drawing.SystemColors.Window;
            appearance4.FontData.Name = "Arial";
            appearance4.FontData.SizeInPoints = 8F;
            appearance4.ForeColor = System.Drawing.SystemColors.WindowText;
            ultraGridBand1.Override.RowAlternateAppearance = appearance4;
            appearance5.BackColor = System.Drawing.SystemColors.Window;
            appearance5.FontData.Name = "Arial";
            appearance5.FontData.SizeInPoints = 8F;
            appearance5.ForeColor = System.Drawing.SystemColors.WindowText;
            appearance5.TextHAlignAsString = "Left";
            ultraGridBand1.Override.RowAppearance = appearance5;
            this.grdAgentSummary.DisplayLayout.BandsSerializer.Add(ultraGridBand1);
            this.grdAgentSummary.DisplayLayout.BorderStyle = Infragistics.Win.UIElementBorderStyle.InsetSoft;
            appearance6.BackColor = System.Drawing.SystemColors.InactiveCaption;
            appearance6.FontData.BoldAsString = "True";
            appearance6.FontData.Name = "Verdana";
            appearance6.FontData.SizeInPoints = 8F;
            appearance6.ForeColor = System.Drawing.SystemColors.InactiveCaptionText;
            appearance6.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.CaptionAppearance = appearance6;
            this.grdAgentSummary.DisplayLayout.Override.AllowAddNew = Infragistics.Win.UltraWinGrid.AllowAddNew.No;
            this.grdAgentSummary.DisplayLayout.Override.AllowDelete = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.AllowUpdate = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.CellClickAction = Infragistics.Win.UltraWinGrid.CellClickAction.RowSelect;
            appearance7.BackColor = System.Drawing.SystemColors.Control;
            appearance7.FontData.BoldAsString = "True";
            appearance7.FontData.Name = "Verdana";
            appearance7.FontData.SizeInPoints = 8F;
            appearance7.TextHAlignAsString = "Left";
            this.grdAgentSummary.DisplayLayout.Override.HeaderAppearance = appearance7;
            this.grdAgentSummary.DisplayLayout.Override.HeaderClickAction = Infragistics.Win.UltraWinGrid.HeaderClickAction.SortSingle;
            this.grdAgentSummary.DisplayLayout.Override.MaxSelectedRows = 1;
            appearance8.BorderColor = System.Drawing.SystemColors.ControlLight;
            this.grdAgentSummary.DisplayLayout.Override.RowAppearance = appearance8;
            this.grdAgentSummary.DisplayLayout.Override.RowSelectors = Infragistics.Win.DefaultableBoolean.False;
            this.grdAgentSummary.DisplayLayout.Override.SelectTypeRow = Infragistics.Win.UltraWinGrid.SelectType.Single;
            this.grdAgentSummary.DisplayLayout.ScrollBounds = Infragistics.Win.UltraWinGrid.ScrollBounds.ScrollToFill;
            this.grdAgentSummary.DisplayLayout.ScrollStyle = Infragistics.Win.UltraWinGrid.ScrollStyle.Immediate;
            this.grdAgentSummary.DisplayLayout.TabNavigation = Infragistics.Win.UltraWinGrid.TabNavigation.NextControl;
            this.grdAgentSummary.DisplayLayout.ViewStyle = Infragistics.Win.UltraWinGrid.ViewStyle.SingleBand;
            this.grdAgentSummary.Location = new System.Drawing.Point(0, 0);
            this.grdAgentSummary.Name = "grdAgentSummary";
            this.grdAgentSummary.Size = new System.Drawing.Size(760, 305);
            this.grdAgentSummary.TabIndex = 1;
            this.grdAgentSummary.Text = "Agent Summary View for ";
            this.grdAgentSummary.UseOsThemes = Infragistics.Win.DefaultableBoolean.False;
            // 
            // cmdPrint
            // 
            this.cmdPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdPrint.Location = new System.Drawing.Point(550, 314);
            this.cmdPrint.Name = "cmdPrint";
            this.cmdPrint.Size = new System.Drawing.Size(96, 24);
            this.cmdPrint.TabIndex = 2;
            this.cmdPrint.Text = "Print";
            this.cmdPrint.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cmdClose.Location = new System.Drawing.Point(652, 314);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 24);
            this.cmdClose.TabIndex = 0;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // mTLs
            // 
            this.mTLs.DataSetName = "FreightDataset";
            this.mTLs.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dlgAgentSummary
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.CancelButton = this.cmdClose;
            this.ClientSize = new System.Drawing.Size(760, 350);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdPrint);
            this.Controls.Add(this.grdAgentSummary);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAgentSummary";
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "TLViewer";
            this.Load += new System.EventHandler(this.OnFormLoad);
            ((System.ComponentModel.ISupportInitialize)(this.grdAgentSummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mTLs)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Form load event handler
			this.Cursor = Cursors.WaitCursor;
			try {
				//Show early
				this.Visible = true;
				Application.DoEvents();
				
				//Load agent summarymEntTerminal
                this.grdAgentSummary.Text = "Agent Summary for " + this.mTerminalName;
				this.grdAgentSummary.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                this.grdAgentSummary.DisplayLayout.Bands[0].Columns["AgentNumber"].SortIndicator = SortIndicator.Ascending;
                this.mTLs.Merge(FreightGateway.GetAgentSummary(this.mTerminalID));
				if(this.grdAgentSummary.Rows.Count > 0) 
					this.grdAgentSummary.Rows[0].Selected = this.grdAgentSummary.Rows[0].Activate();
				this.cmdPrint.Enabled = (this.grdAgentSummary.Rows.Count > 0);
			} 
			catch(Exception ex) { throw ex; }
			finally { this.Cursor = Cursors.Default; }
		}
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			try {
				Button btn = (Button)sender;
				switch(btn.Name) {
					case "cmdClose":
						this.DialogResult = DialogResult.Cancel;
						this.Close();
						break;
					case "cmdPrint":
                        UltraGridPrinter.Print(this.grdAgentSummary,"Agent Summary View for " + this.mTerminalName + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"),true);
						break;
				}
			} 
			catch(Exception) { }
		}
	}
}
