namespace Argix.HR {
    partial class dlgSubscription {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dlgSubscription));
            this.lblDriver = new System.Windows.Forms.Label();
            this.lblDepot = new System.Windows.Forms.Label();
            this.cboRouteClass = new System.Windows.Forms.ComboBox();
            this.cboDriver = new System.Windows.Forms.ComboBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblDriver
            // 
            this.lblDriver.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDriver.Location = new System.Drawing.Point(6, 48);
            this.lblDriver.Name = "lblDriver";
            this.lblDriver.Size = new System.Drawing.Size(72, 23);
            this.lblDriver.TabIndex = 1;
            this.lblDriver.Text = "Driver";
            this.lblDriver.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblDepot
            // 
            this.lblDepot.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDepot.Location = new System.Drawing.Point(6, 12);
            this.lblDepot.Name = "lblDepot";
            this.lblDepot.Size = new System.Drawing.Size(72, 23);
            this.lblDepot.TabIndex = 2;
            this.lblDepot.Text = "Depot";
            this.lblDepot.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboRouteClass
            // 
            this.cboRouteClass.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRouteClass.FormattingEnabled = true;
            this.cboRouteClass.Location = new System.Drawing.Point(84, 13);
            this.cboRouteClass.Name = "cboRouteClass";
            this.cboRouteClass.Size = new System.Drawing.Size(192, 21);
            this.cboRouteClass.TabIndex = 4;
            this.cboRouteClass.SelectionChangeCommitted += new System.EventHandler(this.OnRouteClassChanged);
            // 
            // cboDriver
            // 
            this.cboDriver.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDriver.FormattingEnabled = true;
            this.cboDriver.Location = new System.Drawing.Point(84, 49);
            this.cboDriver.Name = "cboDriver";
            this.cboDriver.Size = new System.Drawing.Size(231, 21);
            this.cboDriver.TabIndex = 5;
            this.cboDriver.SelectionChangeCommitted += new System.EventHandler(this.OnDriverChanged);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(243, 105);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(72, 24);
            this.btnCancel.TabIndex = 6;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnCancel);
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(165, 105);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(72, 24);
            this.btnOk.TabIndex = 7;
            this.btnOk.Text = "O&k";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnOk);
            // 
            // dlgSubscription
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 140);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cboDriver);
            this.Controls.Add(this.cboRouteClass);
            this.Controls.Add(this.lblDepot);
            this.Controls.Add(this.lblDriver);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgSubscription";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Security Route Subscription";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDriver;
        private System.Windows.Forms.Label lblDepot;
        private System.Windows.Forms.ComboBox cboRouteClass;
        private System.Windows.Forms.ComboBox cboDriver;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}