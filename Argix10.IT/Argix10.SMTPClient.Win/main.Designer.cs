namespace Argix {
    partial class frmMain {
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
            this._lblFrom = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.txtTo = new System.Windows.Forms.TextBox();
            this._lblTo = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this._lblSubject = new System.Windows.Forms.Label();
            this.txtBody = new System.Windows.Forms.TextBox();
            this._lblBody = new System.Windows.Forms.Label();
            this.btnSend = new System.Windows.Forms.Button();
            this.rdoDirect = new System.Windows.Forms.RadioButton();
            this.txtSMTPServer = new System.Windows.Forms.TextBox();
            this.rdoWebService = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // _lblFrom
            // 
            this._lblFrom.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblFrom.Location = new System.Drawing.Point(12, 9);
            this._lblFrom.Name = "_lblFrom";
            this._lblFrom.Size = new System.Drawing.Size(100, 23);
            this._lblFrom.TabIndex = 0;
            this._lblFrom.Text = "From Address";
            this._lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFrom
            // 
            this.txtFrom.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFrom.Location = new System.Drawing.Point(119, 9);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(225, 22);
            this.txtFrom.TabIndex = 1;
            this.txtFrom.Text = "extranet.support@argixlogistics.com";
            this.txtFrom.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTo.Location = new System.Drawing.Point(119, 35);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(225, 22);
            this.txtTo.TabIndex = 3;
            this.txtTo.Text = "jheary@argixlogistics.com";
            this.txtTo.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblTo
            // 
            this._lblTo.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblTo.Location = new System.Drawing.Point(12, 35);
            this._lblTo.Name = "_lblTo";
            this._lblTo.Size = new System.Drawing.Size(100, 23);
            this._lblTo.TabIndex = 2;
            this._lblTo.Text = "To Address";
            this._lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSubject.Location = new System.Drawing.Point(119, 61);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(350, 22);
            this.txtSubject.TabIndex = 5;
            this.txtSubject.Text = "Testing SMTP";
            this.txtSubject.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblSubject
            // 
            this._lblSubject.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblSubject.Location = new System.Drawing.Point(12, 61);
            this._lblSubject.Name = "_lblSubject";
            this._lblSubject.Size = new System.Drawing.Size(100, 23);
            this._lblSubject.TabIndex = 4;
            this._lblSubject.Text = "Subject";
            this._lblSubject.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(119, 87);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.Size = new System.Drawing.Size(350, 50);
            this.txtBody.TabIndex = 7;
            this.txtBody.Text = "Please do not respond";
            this.txtBody.TextChanged += new System.EventHandler(this.OnValidateForm);
            // 
            // _lblBody
            // 
            this._lblBody.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblBody.Location = new System.Drawing.Point(12, 87);
            this._lblBody.Name = "_lblBody";
            this._lblBody.Size = new System.Drawing.Size(100, 23);
            this._lblBody.TabIndex = 6;
            this._lblBody.Text = "Body";
            this._lblBody.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Calibri", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSend.Location = new System.Drawing.Point(394, 227);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(75, 23);
            this.btnSend.TabIndex = 8;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.OnSend);
            // 
            // rdoDirect
            // 
            this.rdoDirect.Checked = true;
            this.rdoDirect.Location = new System.Drawing.Point(12, 158);
            this.rdoDirect.Name = "rdoDirect";
            this.rdoDirect.Size = new System.Drawing.Size(104, 24);
            this.rdoDirect.TabIndex = 10;
            this.rdoDirect.TabStop = true;
            this.rdoDirect.Text = "Direct to SMTP";
            this.rdoDirect.UseVisualStyleBackColor = true;
            // 
            // txtSMTPServer
            // 
            this.txtSMTPServer.Location = new System.Drawing.Point(119, 160);
            this.txtSMTPServer.Name = "txtSMTPServer";
            this.txtSMTPServer.Size = new System.Drawing.Size(150, 20);
            this.txtSMTPServer.TabIndex = 11;
            this.txtSMTPServer.Text = "smtp.argix.com";
            // 
            // rdoWebService
            // 
            this.rdoWebService.Location = new System.Drawing.Point(12, 188);
            this.rdoWebService.Name = "rdoWebService";
            this.rdoWebService.Size = new System.Drawing.Size(457, 24);
            this.rdoWebService.TabIndex = 12;
            this.rdoWebService.Text = "Use Web Service: http://192.168.151.65/Argix10/Argix10.Enterprise.Services/SMTP.s" +
    "vc";
            this.rdoWebService.UseVisualStyleBackColor = true;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 262);
            this.Controls.Add(this.rdoWebService);
            this.Controls.Add(this.txtSMTPServer);
            this.Controls.Add(this.rdoDirect);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this._lblBody);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this._lblSubject);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this._lblTo);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this._lblFrom);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMain";
            this.Text = "SMTP Client";
            this.Load += new System.EventHandler(this.OnFormLoad);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label _lblFrom;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.Label _lblTo;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label _lblSubject;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Label _lblBody;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.RadioButton rdoDirect;
        private System.Windows.Forms.TextBox txtSMTPServer;
        private System.Windows.Forms.RadioButton rdoWebService;
    }
}

