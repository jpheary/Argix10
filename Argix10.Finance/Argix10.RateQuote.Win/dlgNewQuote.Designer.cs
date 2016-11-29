namespace Argix {
    partial class dlgNewQuote {
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
            this.cboRequestType = new System.Windows.Forms.ComboBox();
            this.lblNew = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOk = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // cboRequestType
            // 
            this.cboRequestType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRequestType.FormattingEnabled = true;
            this.cboRequestType.Items.AddRange(new object[] {
            "Basic",
            "LTL"});
            this.cboRequestType.Location = new System.Drawing.Point(12, 37);
            this.cboRequestType.Name = "cboRequestType";
            this.cboRequestType.Size = new System.Drawing.Size(223, 21);
            this.cboRequestType.TabIndex = 37;
            this.cboRequestType.SelectionChangeCommitted += new System.EventHandler(this.OnValidateForm);
            // 
            // lblNew
            // 
            this.lblNew.Location = new System.Drawing.Point(12, 12);
            this.lblNew.Name = "lblNew";
            this.lblNew.Size = new System.Drawing.Size(270, 23);
            this.lblNew.TabIndex = 38;
            this.lblNew.Text = "Select a quote type:";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(207, 87);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 39;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(126, 87);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 40;
            this.btnOk.Text = "O&k";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.OnButtonClick);
            // 
            // dlgNewQuote
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(294, 122);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblNew);
            this.Controls.Add(this.cboRequestType);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "dlgNewQuote";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "New Quote";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ComboBox cboRequestType;
        private System.Windows.Forms.Label lblNew;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
    }
}