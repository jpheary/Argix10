using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Argix.Customers;
using Word=Microsoft.Office.Interop.Word;

namespace Argix.Customers {
    //
    public partial class dlgAction :Form {
        //Members
        private Issue mIssue=null;
        private Action mAction = null;
        private ToolTip mToolTip = null;

        //Interface
        public dlgAction(Issue issue, Action action) {
            //Constructor
            try {
                InitializeComponent();
                this.mIssue = issue;
                this.mAction = action;
                this.Text = "Action (New) for " + this.mIssue.Subject;
            }
            catch(Exception ex) { throw new ApplicationException("Unexpected error while creating new dlgAction instance.",ex); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            this.Cursor = Cursors.WaitCursor;
            try {
                this.mToolTip = new ToolTip();
                this.mToolTip.ShowAlways = true;
                this.mToolTip.SetToolTip(this.cboActionType,"Select an action type.");

                this.mActionsDS.Clear(); this.mActionsDS.Merge(CRMGateway.GetActionTypes(this.mIssue.ID));

                this.cboActionType.SelectedValue = this.mAction.TypeID;
                this.txtComment.Text = this.mAction.Comment;
                this.txtComments.Text = showAllActions(this.mIssue.Actions);
                this.chkShowAll.Checked = true;
                OnShowAllCheckedChanged(null,EventArgs.Empty);
            }
            catch(Exception ex) { App.ReportError(ex); }
            finally { OnValidateForm(null,EventArgs.Empty); this.Cursor = Cursors.Default; }
        }
        private void OnActionTypeSelected(object sender,EventArgs e) {
            //Event handler for change in selected action type
            OnValidateForm(null,EventArgs.Empty);
        }
        private void OnShowAllCheckedChanged(object sender,EventArgs e) {
            //Event handler for show all checked changed
            this.txtComments.Visible = this.chkShowAll.Checked;
        }
        private void OnValidateForm(object sender,EventArgs e) {
            //Event handler for control value changes
            try {
                this.btnSpellCheck.Enabled = this.txtComment.Text.Length > 0;
                this.lblSpellCheck.Text = "";
                this.btnOk.Enabled = (this.cboActionType.Text.Length > 0 && this.txtComment.Text.Length > 0);
            }
            catch { }
        }
        private void OnCmdClick(object sender,System.EventArgs e) {
            //Command button handler
            try {
                Button btn = (Button)sender;
                switch(btn.Name) {
                    case "btnCancel":
                        //Close the dialog
                        this.DialogResult = DialogResult.Cancel;
                        this.Close();
                        break;
                    case "btnOk":
                        //Initialize new action
                        this.Cursor = Cursors.WaitCursor;
                        this.DialogResult = DialogResult.OK;
                        this.mAction.TypeID = (byte)this.cboActionType.SelectedValue;
                        this.mAction.Comment = this.txtComment.Text;
                        this.Close();
                        break;
                    case "btnSpellCheck":
                        spellCheck(this.txtComment,this.lblSpellCheck);
                        break;
                    default: break;
                }
            }
            catch(Exception ex) { App.ReportError(ex); }
        }
        #region Local Services: showAllActions(), spellCheck()
        private string showAllActions(Actions actions) {
            //Get a running comment for this action
            string comments="";
            for(int i = 0;i < actions.Count;i++) {
                Action action = actions[i];
                if(i > 0) {
                    comments += "\r\n\r\n";
                    comments += "".PadRight(75,'-');
                    comments += "\r\n";
                }
                comments += action.Created.ToString("f") + ", " + action.UserID + ", " + action.TypeName;
                comments += "\r\n\r\n";
                comments += action.Comment;
            }
            return comments;
        }
        private void spellCheck(TextBox txt,Label lbl) {
            //Spell check
            if(txt.Text.Length > 0) {
                //Create an instance of MS Word application
                Word.Application app = new Word.Application();
                app.Visible = false;

                //Setup spell checker with text to check and count errors
                //Setting these variables is comparable to passing null to the function
                //This is necessary because the C# null cannot be passed by reference
                object temp=Missing.Value,newTemp=Missing.Value,docType=Missing.Value,vis=true;
                Word.Document doc = app.Documents.Add(ref temp,ref newTemp,ref docType,ref vis);
                doc.Words.First.InsertBefore(txt.Text);
                int errCount = doc.SpellingErrors.Count;

                //Run the checker
                object opt = Missing.Value;
                doc.CheckSpelling(ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt,ref opt);

                //Update original text
                object first = 0,last = doc.Characters.Count - 1;
                txt.Text = doc.Range(ref first,ref last).Text;
                txt.Text = txt.Text.Replace("\r","\r\n");
                lbl.Text = "Spelling OK. " + errCount + " error(s) corrected ";

                //Close MS Word application
                object save = false,format = Missing.Value,rtDoc = Missing.Value;
                app.Quit(ref save,ref format,ref rtDoc);
            }
        }
        #endregion

    }
}