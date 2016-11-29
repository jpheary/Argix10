using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Argix.Windows;

namespace Argix.Freight {
    //
    public partial class dlgTLDetail:Form {
        //Members
        private int mTerminalID = 0;
        private string mTLNumber = "";
        private UltraGridSvc mGridSvc = null;

        //Interface
        public dlgTLDetail(int terminalID, string tlNumber) {
            //Constructor
            try {
                InitializeComponent();
                this.mTerminalID = terminalID;
                this.mTLNumber = tlNumber;
                this.grdTLDetail.Text = "TL# " + tlNumber;
                this.mGridSvc = new UltraGridSvc(this.grdTLDetail);
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Form load event handler
            try {
                this.mTLDetail.Merge(Freight.FreightGateway.GetTLDetail(this.mTerminalID,this.mTLNumber));
            }
            catch (Exception ex) { App.ReportError(ex,true,LogLevel.Error); }
        }
        private void OnItemClick(object sender, System.EventArgs e) {
            //Menu services
            try {
                ToolStripItem menu = (ToolStripItem)sender;
                switch(menu.Name) {
                    case "csSaveAs":
                        SaveFileDialog dlgSave = new SaveFileDialog();
                        dlgSave.AddExtension = true;
                        dlgSave.Filter = "Excel files (*.xls)|*.xls|All files (*.*)|*.*";
                        dlgSave.FilterIndex = 0;
                        dlgSave.Title = "Save TL View As...";
                        dlgSave.FileName = "TL# " + this.mTLNumber;
                        dlgSave.CheckFileExists = false;
                        dlgSave.OverwritePrompt = true;
                        dlgSave.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                        if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                            this.Cursor = Cursors.WaitCursor;
                            Application.DoEvents();
                            new Argix.ExcelFormat().Transform(this.mTLDetail, "TLTable", dlgSave.FileName);
                        }
                        break;
                    case "csPrint": UltraGridPrinter.Print(this.grdTLDetail, "TL Detail for TL# " + this.mTLNumber + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt"), true); break;
                    case "csPreview": UltraGridPrinter.PrintPreview(this.grdTLDetail, "TL Detail for TL# " + this.mTLNumber + ", " + DateTime.Now.ToString("MM/dd/yyyy hh:mm tt")); break;
                    case "csRefresh":
                        this.Cursor = Cursors.WaitCursor;
                        this.mTLDetail.Clear();
                        this.mTLDetail.Merge(Freight.FreightGateway.GetTLDetail(this.mTerminalID, this.mTLNumber));
                        break;
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
            finally { this.Cursor = Cursors.Default; }
        }
    }
}
