namespace Argix.Freight {
    partial class frmLoadTender {
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
            this.lblBarcode1 = new System.Windows.Forms.Label();
            this.lblBarcode2 = new System.Windows.Forms.Label();
            this._lblBarcode1 = new System.Windows.Forms.Label();
            this._lblBarcode2 = new System.Windows.Forms.Label();
            this.btnPrint = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblBarcode1
            // 
            this.lblBarcode1.BackColor = System.Drawing.SystemColors.Window;
            this.lblBarcode1.Font = new System.Drawing.Font("Agency FB", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode1.Location = new System.Drawing.Point(25, 28);
            this.lblBarcode1.Name = "lblBarcode1";
            this.lblBarcode1.Size = new System.Drawing.Size(427, 77);
            this.lblBarcode1.TabIndex = 0;
            this.lblBarcode1.Text = "EXPUS0US1500009 ";
            this.lblBarcode1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblBarcode2
            // 
            this.lblBarcode2.BackColor = System.Drawing.SystemColors.Window;
            this.lblBarcode2.Font = new System.Drawing.Font("Agency FB", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBarcode2.Location = new System.Drawing.Point(25, 160);
            this.lblBarcode2.Name = "lblBarcode2";
            this.lblBarcode2.Size = new System.Drawing.Size(427, 77);
            this.lblBarcode2.TabIndex = 1;
            this.lblBarcode2.Text = "00209";
            this.lblBarcode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblBarcode1
            // 
            this._lblBarcode1.Location = new System.Drawing.Point(118, 109);
            this._lblBarcode1.Name = "_lblBarcode1";
            this._lblBarcode1.Size = new System.Drawing.Size(249, 23);
            this._lblBarcode1.TabIndex = 2;
            this._lblBarcode1.Text = "EXPUS0US1500009 ";
            this._lblBarcode1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // _lblBarcode2
            // 
            this._lblBarcode2.Location = new System.Drawing.Point(118, 242);
            this._lblBarcode2.Name = "_lblBarcode2";
            this._lblBarcode2.Size = new System.Drawing.Size(249, 23);
            this._lblBarcode2.TabIndex = 3;
            this._lblBarcode2.Text = "00209";
            this._lblBarcode2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(406, 254);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(75, 23);
            this.btnPrint.TabIndex = 4;
            this.btnPrint.Text = "Print";
            this.btnPrint.UseVisualStyleBackColor = true;
            this.btnPrint.Click += new System.EventHandler(this.OnPrint);
            // 
            // frmLoadTender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 289);
            this.Controls.Add(this.btnPrint);
            this.Controls.Add(this._lblBarcode2);
            this.Controls.Add(this._lblBarcode1);
            this.Controls.Add(this.lblBarcode2);
            this.Controls.Add(this.lblBarcode1);
            this.Name = "frmLoadTender";
            this.Text = "Load Tender Detail";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBarcode1;
        private System.Windows.Forms.Label lblBarcode2;
        private System.Windows.Forms.Label _lblBarcode1;
        private System.Windows.Forms.Label _lblBarcode2;
        private System.Windows.Forms.Button btnPrint;
    }
}