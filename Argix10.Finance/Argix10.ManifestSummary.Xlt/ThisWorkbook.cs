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
        private const string USP_INVOICE = "uspInvManifestSummaryByReleaseDateInvoiceGet",TBL_INVOICE = "ManifestSummaryTable";
        private const int ROW0_DETAIL=11;

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
                clid = "048"; invoice = "326459500";    //Amscan: 048, 326459500
#endif
                if (cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Create detail worksheet
                    SqlDataAdapter adapter = new SqlDataAdapter(USP_INVOICE,global::Argix.Finance.Settings.Default.RGXSQLR);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                    adapter.TableMappings.Add("Table",TBL_INVOICE);
                    InvoiceDataset ds = new InvoiceDataset();
                    adapter.Fill(ds,TBL_INVOICE);
                    if (ds.Tables[TBL_INVOICE] != null && ds.Tables[TBL_INVOICE].Rows.Count > 0) {
                        createDetailHeader(ds.ManifestSummaryTable[0]);
                        createDetailBody(ds.ManifestSummaryTable);
                    }
                    else
                        MessageBox.Show("No data found for invoice #" + invoice + ".");
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
        private void InternalStartup() {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void createDetailHeader(InvoiceDataset.ManifestSummaryTableRow invoice) {
            //Set named range summary values
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Remit To
            //detail.ClientNumberDiv.Value = (invoice.IsClientNumberNull()? "" : invoice.ClientNumber.Trim()) + " " + (invoice.IsClientDivisionNull()? "" : invoice.ClientDivision.Trim()) + " - ";
            detail.RemitToName.Value = "";    //invoice.RemitToName.Trim();
            detail.RemitToAddressLine1.Value = "";    //invoice.RemitToAddressLine1.Trim();
            //detail.RemitToAddressLine2.Value = invoice.IsRemitToAddressLine2Null() ? "" : invoice.RemitToAddressLine2.Trim();
            detail.RemitToCityStateZip.Value = "";    //invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            detail.Telephone.Value = "";    //(invoice.IsTelephoneNull() ? "" : invoice.Telephone.ToString());

            //Bill To
            detail.BillToName.Value = "";    //invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = "";    //invoice.BillToAddressline1.Trim();
            detail.BillToAddressLine2.Value = "";    //invoice.IsBillToAddressline2Null() ? "" : invoice.BillToAddressline2.Trim();
            detail.BillToCityStateZip.Value = "";    //invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Account
            detail.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            detail.InvoiceDate.Value = invoice.InvoiceDate;
            detail.ReleaseDate.Value = invoice.ReleaseDate;
        }
        private void createDetailBody(InvoiceDataset.ManifestSummaryTableDataTable summaryTable) {
            //Get worksheet
            Detail ws = global::Argix.Finance.Globals.Detail;
            Application.ScreenUpdating = false;

            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = summaryTable.Rows.Count;
            Excel.Range row0 = ws.Range[ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,7]].EntireRow;
            for(int i=0;i<rowCount - 1;i++)
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown,false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount,4];
            for(int i=0;i<rowCount;i++) {
                values[i,0] = "'" + summaryTable[i].StoreNumber;
                values[i,1] = summaryTable[i].Cartons;
                values[i,2] = summaryTable[i].Weight;
                values[i,3] = summaryTable[i].Manifests;
            }
            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,4]].Value2 = values;

            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,1]].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;
            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,1]].NumberFormat = "#,###_);(#,###)";
            ws.Range[ws.Cells[ROW0_DETAIL,2],ws.Cells[ROW0_DETAIL + rowCount - 1,2]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,2],ws.Cells[ROW0_DETAIL + rowCount - 1,2]].NumberFormat = "#,###_);(#,###);_(* _)";
            ws.Range[ws.Cells[ROW0_DETAIL,3],ws.Cells[ROW0_DETAIL + rowCount - 1,3]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,3],ws.Cells[ROW0_DETAIL + rowCount - 1,3]].NumberFormat = "#,###_);(#,###)";
            ws.Range[ws.Cells[ROW0_DETAIL,4],ws.Cells[ROW0_DETAIL + rowCount - 1,4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignLeft;

            Application.ScreenUpdating = true;
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
