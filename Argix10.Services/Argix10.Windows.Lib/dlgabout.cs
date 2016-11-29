using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Argix.Windows {
	//
	public class dlgAbout : System.Windows.Forms.Form {
		//Members
		
		//Constants
		private const string CMD_CLOSE = "&Close";
		#region Controls
        private System.Windows.Forms.PictureBox picDialog;
		private System.Windows.Forms.Label lblDesc;
		private System.Windows.Forms.Label lblVer;
		private System.Windows.Forms.Label lblCopy;
		private System.Windows.Forms.Label lblDisc;
        private System.Windows.Forms.Button cmdDialog;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Label lblUpdated;
		private System.ComponentModel.Container components = null;		//Required designer variable
		#endregion
		
		//Interface
		public dlgAbout() : this("Unknown Application","Version: Unknown","Copyright: Unknown","") {}
		public dlgAbout(string description, string version, string copyright) : this(description,version,copyright,"") { }
		public dlgAbout(string description, string version, string copyright, string updated) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
			
				//Set command identities
				this.cmdDialog.Text = CMD_CLOSE;
				
				//Set labels for app description, version, and copyright, and update
				this.Text = "About " + description;
                this.lblDesc.Text = description;
                this.lblVer.Text = version;
				this.lblCopy.Text = copyright;
				this.lblUpdated.Text = updated;
			} 
			catch(Exception ex) { throw ex; }
		}
		protected override void Dispose( bool disposing ) {
			//Clean up any resources being used
			if(disposing) {
				if(components != null)
					components.Dispose();
			}
			base.Dispose( disposing );
		}
		
		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgAbout));
            this.picDialog = new System.Windows.Forms.PictureBox();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblVer = new System.Windows.Forms.Label();
            this.lblCopy = new System.Windows.Forms.Label();
            this.lblDisc = new System.Windows.Forms.Label();
            this.cmdDialog = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblUpdated = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // picDialog
            // 
            this.picDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.picDialog.BackColor = System.Drawing.Color.Transparent;
            this.picDialog.Image = ((System.Drawing.Image)(resources.GetObject("picDialog.Image")));
            this.picDialog.Location = new System.Drawing.Point(424, 16);
            this.picDialog.Name = "picDialog";
            this.picDialog.Size = new System.Drawing.Size(171, 57);
            this.picDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDialog.TabIndex = 0;
            this.picDialog.TabStop = false;
            // 
            // lblDesc
            // 
            this.lblDesc.BackColor = System.Drawing.Color.Transparent;
            this.lblDesc.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDesc.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDesc.Location = new System.Drawing.Point(6, 3);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(268, 18);
            this.lblDesc.TabIndex = 2;
            this.lblDesc.Text = "Argix Application";
            this.lblDesc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblVer
            // 
            this.lblVer.BackColor = System.Drawing.Color.Transparent;
            this.lblVer.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVer.Location = new System.Drawing.Point(6, 27);
            this.lblVer.Name = "lblVer";
            this.lblVer.Size = new System.Drawing.Size(268, 18);
            this.lblVer.TabIndex = 3;
            this.lblVer.Text = "Version: 1.0.0.0";
            this.lblVer.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCopy
            // 
            this.lblCopy.BackColor = System.Drawing.Color.Transparent;
            this.lblCopy.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopy.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCopy.Location = new System.Drawing.Point(6, 51);
            this.lblCopy.Name = "lblCopy";
            this.lblCopy.Size = new System.Drawing.Size(268, 18);
            this.lblCopy.TabIndex = 4;
            this.lblCopy.Text = "Copyright 2003-2004 Argix Direct";
            this.lblCopy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDisc
            // 
            this.lblDisc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblDisc.BackColor = System.Drawing.Color.Transparent;
            this.lblDisc.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDisc.ForeColor = System.Drawing.Color.White;
            this.lblDisc.Location = new System.Drawing.Point(6, 229);
            this.lblDisc.Name = "lblDisc";
            this.lblDisc.Size = new System.Drawing.Size(423, 42);
            this.lblDisc.TabIndex = 5;
            this.lblDisc.Text = resources.GetString("lblDisc.Text");
            // 
            // cmdDialog
            // 
            this.cmdDialog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdDialog.BackColor = System.Drawing.SystemColors.Control;
            this.cmdDialog.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdDialog.Location = new System.Drawing.Point(492, 243);
            this.cmdDialog.Name = "cmdDialog";
            this.cmdDialog.Size = new System.Drawing.Size(96, 24);
            this.cmdDialog.TabIndex = 6;
            this.cmdDialog.Text = "O&K";
            this.cmdDialog.UseVisualStyleBackColor = false;
            this.cmdDialog.Click += new System.EventHandler(this.OnCmdClick);
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.Window;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lblCopy);
            this.panel1.Controls.Add(this.lblDesc);
            this.panel1.Controls.Add(this.lblVer);
            this.panel1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.panel1.Location = new System.Drawing.Point(305, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(283, 80);
            this.panel1.TabIndex = 9;
            // 
            // lblUpdated
            // 
            this.lblUpdated.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUpdated.BackColor = System.Drawing.Color.Transparent;
            this.lblUpdated.Font = new System.Drawing.Font("Verdana", 6F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUpdated.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblUpdated.Location = new System.Drawing.Point(398, 184);
            this.lblUpdated.Name = "lblUpdated";
            this.lblUpdated.Size = new System.Drawing.Size(190, 18);
            this.lblUpdated.TabIndex = 10;
            this.lblUpdated.Text = "Updated 00/00/2004, jph";
            this.lblUpdated.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dlgAbout
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(594, 272);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.lblUpdated);
            this.Controls.Add(this.picDialog);
            this.Controls.Add(this.cmdDialog);
            this.Controls.Add(this.lblDisc);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgAbout";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "About Argix";
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnCmdClick(object sender, System.EventArgs e) {
			//Command button handler
			Button btn = (Button)sender;
			switch(btn.Text) {
				case CMD_CLOSE:
					//Close the dialog
					this.DialogResult = DialogResult.Cancel;
					this.Close();
					break;
			}
		}
	}
}