using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel = Microsoft.Office.Interop.Excel;
using Office = Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_DETAIL = "uspInvInvoiceWithStoreBLNumbersGet", TBL_DETAIL = "ClientInvoiceWithStoreBLNumbersTable";
       
        private const int DETAIL_NUMBER_OF_COLUMNS = 17;
        private const int ROW0_DETAIL=11;

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender, System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid = "",invoice = "";
#if DEBUG
                clid = "003"; invoice = "332103100";    //003-Bourassa;   330510700
#endif
                if(cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Display summary and detail data
                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL, global::Argix.Finance.Settings.Default.TSortR);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber", invoice) });
                        adapter.TableMappings.Add("Table", TBL_DETAIL);
                        InvoiceDataset ds = new InvoiceDataset();
                        adapter.Fill(ds, TBL_DETAIL);
                        if (ds.Tables[TBL_DETAIL] != null && ds.Tables[TBL_DETAIL].Rows.Count > 0) {
                            showSummary(ds.ClientInvoiceWithStoreBLNumbersTable[0]);
                            showDetail(ds.ClientInvoiceWithStoreBLNumbersTable);
                            createDetailFooter(ds.ClientInvoiceWithStoreBLNumbersTable[0]);
                        }
                        else
                            MessageBox.Show("No data found for invoice #" + invoice + ".");
                    }
                    catch (Exception ex) { reportError(ex); }
                }
                else {
                    MessageBox.Show("Invoice unspecified.");
                }
            }
            catch (Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_BeforeSave(bool SaveAsUI, ref bool Cancel)
        {
            //Event handler for before save
            try
            {
                //Remove customization so dll isn't called from a saved file (i.e. only from the template)
                this.RemoveCustomization();
            }
            catch (Exception ex) { reportError(ex); }
        }
        private void ThisWorkbook_Shutdown(object sender, System.EventArgs e)
        {
            //Event handler for workbook shutdown event
        }
        #region VSTO Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(this.ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void showSummary(InvoiceDataset.ClientInvoiceWithStoreBLNumbersTableRow invoice) {
            //Set named range summary values
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Remit To
            detail.RemitToName.Value = invoice.RemitToName.Trim() + " " + invoice.RemitToAddressLine1.Trim() + " " + invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            //detail.RemitToName.Value = invoice.RemitToName.Trim();
            //detail.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim();
            ////          detail.RemitToAddressLine2.Value = invoice.RemitToAddressLine2.Trim();
            //detail.RemitToCityStateZip.Value = invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            detail.Telephone.Value = invoice.Telephone.Trim();

            //Bill To
            detail.ClientNumberDiv.Value = invoice.ClientNumber.Trim() + ' ' + invoice.ClientDivision.Trim();
            detail.BillToName.Value = invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            detail.BillToAddressLine2.Value = invoice.BillToAddressline2.Trim();
            detail.BillToCityStateZip.Value = invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Account
            detail.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            detail.InvoiceDate.Value = invoice.InvoiceDate;
            //summary.Terms1.Value = invoice.Terms;
        }
        private void showDetail(DataTable invoiceTable) {
            //Get worksheet
            Detail ws = global::Argix.Finance.Globals.Detail;
            Application.ScreenUpdating = false;

            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = invoiceTable.Rows.Count;
            Excel.Range row0 = ws.Range[ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,DETAIL_NUMBER_OF_COLUMNS]].EntireRow;
            for(int i=0;i<rowCount - 1;i++)
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown,false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount,DETAIL_NUMBER_OF_COLUMNS];
            for(int i = 0;i < rowCount;i++) {
                for(int j = 0;j < DETAIL_NUMBER_OF_COLUMNS;j++) {
                    values[i,j] =  invoiceTable.Rows[i][j].GetType().Equals(typeof(System.String)) ? "'" + invoiceTable.Rows[i][j].ToString().Trim() : invoiceTable.Rows[i][j];
                }
            }
            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,DETAIL_NUMBER_OF_COLUMNS]].Value2 = values;
            
            Application.ScreenUpdating = true;
        }
        private void createDetailFooter(InvoiceDataset.ClientInvoiceWithStoreBLNumbersTableRow invoice) {
            //
            Detail detail = global::Argix.Finance.Globals.Detail;
            detail.Reference.Value2 = "PLEASE REFERENCE INVOICE# " + invoice.InvoiceNumber + " WHEN REMITING PAYMENT. I.C.C. REGULATIONS REQUIRE THAT THIS BILL BE PAID WITHIN " + invoice.PaymentDays + " DAYS";
        }
        private void reportError(Exception ex) {
            MessageBox.Show("ERROR: " + ex.Message);
        }
    }
}
