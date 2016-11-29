using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Diagnostics;

namespace Argix.Windows {
	//
	internal class dlgSplash : System.Windows.Forms.Form {
		//Members
        private bool mClosed = false;
		
		public event EventHandler ITEvent=null;
		#region Controls

        public System.Windows.Forms.Label lblCopyRight;
		public System.Windows.Forms.Label lblTitle;
		public System.Windows.Forms.Label lblCopyright2;
        public System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.PictureBox picDialog;
		
		private System.ComponentModel.Container components = null;	//Required designer variable
		#endregion
		
		//Interface
		public dlgSplash(string title, string version, string copyright) {
			//Constructor
			try {
				//Required for Windows Form Designer support
				InitializeComponent();
				this.lblTitle.Text = title;
				this.lblVersion.Text = version;
				this.lblCopyRight.Text = copyright;
			}
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgSplash instance.",ex); }
		}
		protected override void Dispose(bool disposing) { if(disposing) { if(components!=null) components.Dispose(); } base.Dispose(disposing); }
        public bool Closed { get { return this.mClosed; } }
        #region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSplash));
            this.lblCopyRight = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblCopyright2 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.picDialog = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).BeginInit();
            this.SuspendLayout();
            // 
            // lblCopyRight
            // 
            this.lblCopyRight.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyRight.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCopyRight.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyRight.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCopyRight.Location = new System.Drawing.Point(365, 218);
            this.lblCopyRight.Name = "lblCopyRight";
            this.lblCopyRight.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCopyRight.Size = new System.Drawing.Size(228, 18);
            this.lblCopyRight.TabIndex = 11;
            this.lblCopyRight.Text = "Copyright 2004 Argix Direct";
            this.lblCopyRight.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblTitle.Font = new System.Drawing.Font("Square721 BT", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTitle.Location = new System.Drawing.Point(334, 109);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblTitle.Size = new System.Drawing.Size(259, 80);
            this.lblTitle.TabIndex = 14;
            this.lblTitle.Text = "Application Name";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblCopyright2
            // 
            this.lblCopyright2.BackColor = System.Drawing.Color.Transparent;
            this.lblCopyright2.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblCopyright2.Font = new System.Drawing.Font("Arial", 6.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCopyright2.ForeColor = System.Drawing.Color.White;
            this.lblCopyright2.Location = new System.Drawing.Point(196, 273);
            this.lblCopyright2.Name = "lblCopyright2";
            this.lblCopyright2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblCopyright2.Size = new System.Drawing.Size(397, 18);
            this.lblCopyright2.TabIndex = 13;
            this.lblCopyright2.Text = "This program is protected by US and international copyright laws as described in " +
    "Help About.";
            // 
            // lblVersion
            // 
            this.lblVersion.BackColor = System.Drawing.Color.Transparent;
            this.lblVersion.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblVersion.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblVersion.Location = new System.Drawing.Point(365, 191);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblVersion.Size = new System.Drawing.Size(228, 18);
            this.lblVersion.TabIndex = 12;
            this.lblVersion.Text = "Version #";
            // 
            // picDialog
            // 
            this.picDialog.BackColor = System.Drawing.Color.Transparent;
            this.picDialog.Image = ((System.Drawing.Image)(resources.GetObject("picDialog.Image")));
            this.picDialog.Location = new System.Drawing.Point(420, 30);
            this.picDialog.Name = "picDialog";
            this.picDialog.Size = new System.Drawing.Size(175, 63);
            this.picDialog.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picDialog.TabIndex = 18;
            this.picDialog.TabStop = false;
            // 
            // dlgSplash
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.BackColor = System.Drawing.SystemColors.Window;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(594, 294);
            this.ControlBox = false;
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.picDialog);
            this.Controls.Add(this.lblCopyRight);
            this.Controls.Add(this.lblCopyright2);
            this.Controls.Add(this.lblVersion);
            this.Font = new System.Drawing.Font("Verdana", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSplash";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.Closing += new System.ComponentModel.CancelEventHandler(this.OnFormClosing);
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.OnKeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.picDialog)).EndInit();
            this.ResumeLayout(false);

		}
		#endregion
		
		private void OnFormLoad(object sender, System.EventArgs e) {
			//Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try { 
				this.Refresh();
				this.BringToFront();
				this.Focus();
			}
			catch { }
            finally { this.Cursor = Cursors.WaitCursor; }
		}
		private void OnFormClosing(object sender, System.ComponentModel.CancelEventArgs e) {
			//Event handler for form load event
			try { this.SendToBack(); } catch { }
		}
		private void OnKeyDown(object sender, System.Windows.Forms.KeyEventArgs e) {
			//Event handler for form keydown event
			try {
				if(e.Control && e.KeyCode == Keys.Enter) {
					if(this.ITEvent != null) this.ITEvent(this, new EventArgs());
				}
			} 
			catch { }
		}
		private void OnKeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e) {
			//Event handler for form keypress eventt
			try {
                switch(e.KeyChar) {
                    case (char)13: e.Handled = true; this.mClosed = true;  this.Close(); break;
                    case (char)32: e.Handled = true; this.mClosed = true; this.Close(); break;
					default:        e.Handled = true; break;
				}
			} 
			catch { }
		}
	}
}
