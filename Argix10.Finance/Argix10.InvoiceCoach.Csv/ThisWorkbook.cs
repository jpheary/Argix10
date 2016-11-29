using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;
using Microsoft.Office.Tools.Excel;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_DETAIL = "uspInvCoachInvoiceWithSeparateLineHaulGet",TBL_DETAIL = "ClientInvoiceTable";
        private const int ROW0_DETAIL=4;

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender,System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid="",invoice="";   
#if DEBUG
                clid = "010"; invoice = "331593200";    //010, 331316400
#endif
                if (cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Display header and detail data
                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL,global::Argix.Finance.Settings.Default.TSortR);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                        adapter.TableMappings.Add("Table",TBL_DETAIL);
                        InvoiceDataset ds = new InvoiceDataset();
                        adapter.Fill(ds,TBL_DETAIL);
                        if (ds.Tables[TBL_DETAIL] != null && ds.Tables[TBL_DETAIL].Rows.Count > 0) {
                            showHeader(ds.ClientInvoiceTable[0]);
                            showDetail(ds.ClientInvoiceTable);
                        }
                        else
                            MessageBox.Show("No data found for invoice #" + invoice + ".");
                    }
                    catch(Exception ex) { reportError(ex); }
                }
                else {
                    MessageBox.Show("Invoice unspecified.");
                }
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_BeforeSave(bool SaveAsUI,ref bool Cancel) {
            //Event handler for before save
            try {
                //Remove customization so dll isn't called from a saved file (i.e. only from the template)
                this.RemoveCustomization();
            }
            catch(Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_Shutdown(object sender,System.EventArgs e) {
            //Event handler for workbook shutdown event
        }

        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void showHeader(InvoiceDataset.ClientInvoiceTableRow invoice) {
            //Set named range summary values
            Sheet1 ws = global::Argix.Finance.Globals.Sheet1;
            Application.ScreenUpdating = false;
            ws.Range[ws.Cells[1,1],ws.Cells[1,1]].Value2 = invoice.FileHeader;
            Application.ScreenUpdating = true;
        }
        private void showDetail(InvoiceDataset.ClientInvoiceTableDataTable invoiceTable) {
            //Get worksheet
            Sheet1 ws = global::Argix.Finance.Globals.Sheet1;
            Application.ScreenUpdating = false;

            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = invoiceTable.Rows.Count;
            Excel.Range row0 = ws.Range[ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,104]].EntireRow;
            for(int i=0;i<rowCount - 1;i++)
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown,false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount,104];
            for(int i=0;i<rowCount;i++) {
                for(int j=0;j<104;j++) {
                    values[i,j] = invoiceTable[i][j+1];
                }
            }
            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,104]].Value2 = values;

            Application.ScreenUpdating = true;
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
