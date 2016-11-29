using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Argix {
    //
    public partial class dlgReminders:Form {
        //Members
        private ReminderService mService=null;

        //Interface
        public dlgReminders(ReminderService service, CRMDataset reminders) {
            //Constructor
            InitializeComponent();
            this.mService = service;
            this.mOpenReminders = reminders;
            this.grdReminders.DataSource = this.mOpenReminders;
        }

        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.cboSnooze.SelectedIndex = 0;
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { OnValidateForm(null,EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnFormClosing(object sender,FormClosingEventArgs e) {
            //Event handler for form closing event
            if(e.CloseReason == CloseReason.UserClosing) {
                this.Hide();
                e.Cancel = true;
            }
        }
        private void OnGridAfterSelectChange(object sender,Infragistics.Win.UltraWinGrid.AfterSelectChangeEventArgs e) {
            //Event handler for gris row selected
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            try {
                this.btnSnooze.Enabled = this.grdReminders.Selected.Rows.Count > 0;
                this.btnDismiss.Enabled = this.grdReminders.Selected.Rows.Count > 0;
                this.btnOpenItem.Enabled = this.grdReminders.Selected.Rows.Count > 0;
                this.btnDismissAll.Enabled = true;
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        private void OnButtonClick(object sender,EventArgs e) {
            //Button click event handler
            long id=0;
            string userID="", message="";
            DateTime time;
            DataRow[] rows=null;
            this.Cursor = Cursors.WaitCursor;
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnSnooze":
                        if(this.grdReminders.Selected.Rows.Count > 0) {
                            id = Convert.ToInt32(this.grdReminders.Selected.Rows[0].Cells["IssueID"].Value);
                            userID = this.grdReminders.Selected.Rows[0].Cells["UserID"].Value.ToString();
                            time = DateTime.Now;    //.Parse(this.grdReminders.Selected.Rows[0].Cells["Time"].Value.ToString());
                            rows = this.mOpenReminders.ReminderTable.Select("IssueID=" + id + " AND UserID='" + userID + "'");
                            switch(this.cboSnooze.Text) {
                                case "5 minutes": time = time.AddMinutes(5); break;
                                case "10 minutes": time = time.AddMinutes(10); break;
                                case "15 minutes": time = time.AddMinutes(15); break;
                                case "30 minutes": time = time.AddMinutes(30); break;
                                case "1 hour": time = time.AddHours(1); break;
                                case "2 hours": time = time.AddHours(2); break;
                                case "4 hours": time = time.AddHours(4); break;
                                case "8 hours": time = time.AddHours(8); break;
                                case "0.5 days": time = time.AddHours(12); break;
                                case "1 day": time = time.AddDays(1); break;
                                case "2 days": time = time.AddDays(2); break;
                                case "3 days": time = time.AddDays(3); break;
                                case "4 days": time = time.AddDays(4); break;
                                case "1 week": time = time.AddDays(7); break;
                                case "2 weeks": time = time.AddDays(14); break;
                            }
                            //Update the reminder
                            this.mService.UpdateReminder(id,userID,time);
                            rows = this.mOpenReminders.ReminderTable.Select("IssueID=" + id + " AND UserID='" + userID + "'");
                            if(rows.Length>0) rows[0].Delete();
                            if(this.mOpenReminders.ReminderTable.Rows.Count == 0) this.Hide();
                        }
                        break;
                    case "btnDismiss":
                        //Remove the reminder
                        if(this.grdReminders.Selected.Rows.Count > 0) {
                            id = Convert.ToInt32(this.grdReminders.Selected.Rows[0].Cells["IssueID"].Value);
                            userID = this.grdReminders.Selected.Rows[0].Cells["UserID"].Value.ToString();
                            this.mService.RemoveReminder(id,userID);
                            rows = this.mOpenReminders.ReminderTable.Select("IssueID=" + id + " AND UserID='" + userID + "'");
                            if(rows.Length>0) rows[0].Delete();
                            if(this.mOpenReminders.ReminderTable.Rows.Count == 0) this.Hide();
                        }
                        break;
                    case "btnOpenItem":
                        if(this.grdReminders.Selected.Rows.Count > 0) {
                            id = Convert.ToInt32(this.grdReminders.Selected.Rows[0].Cells["IssueID"].Value);
                            message = this.grdReminders.Selected.Rows[0].Cells["Message"].Value.ToString();
                            this.mService.OpenReminderItem(id,message);
                        }
                        break;
                    case "btnDismissAll":
                        //Remove all open reminders
                        for(int i=0;i<this.mOpenReminders.ReminderTable.Rows.Count;i++) {
                            id = this.mOpenReminders.ReminderTable[i].IssueID;
                            userID = this.mOpenReminders.ReminderTable[i].UserID;
                            this.mService.RemoveReminder(id,userID);
                        }
                        //Remove the open reminder
                        this.mOpenReminders.ReminderTable.Clear();
                        this.Hide();
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, Customers.LogLevel.Error); }
            finally { OnValidateForm(null,EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
    }
}
