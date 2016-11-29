using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Windows.Forms;
using Argix.Enterprise;

namespace Argix {
    //
    public partial class frmMain:Form {
        //
        public frmMain() {
            //
            InitializeComponent();
        }
        private void OnFormLoad(object sender,EventArgs e) {

        }
        private void OnValidateForm(object sender,EventArgs e) {
            this.btnSend.Enabled = this.txtFrom.Text.Trim().Length > 0 &&
                                    this.txtTo.Text.Trim().Length > 0 &&
                                    this.txtSubject.Text.Trim().Length > 0;
        }
        private void OnSend(object sender,EventArgs e) {
            try {
                if (this.rdoDirect.Checked) {
                    MailMessage email = new MailMessage(this.txtFrom.Text,this.txtTo.Text);
                    email.Subject = this.txtSubject.Text;
                    email.BodyEncoding = System.Text.Encoding.UTF8;
                    email.IsBodyHtml = false;
                    email.Body = this.txtBody.Text;

                    SmtpClient smtpClient = new SmtpClient(this.txtSMTPServer.Text);
                    smtpClient.Send(email);
                }
                else if (this.rdoWebService.Checked) {
                    SMTPGateway smtpGateway = new SMTPGateway();
                    smtpGateway.SendMailMessage(this.txtFrom.Text,this.txtTo.Text,this.txtSubject.Text,false,this.txtBody.Text);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.ToString()); }
        }
    }
}
