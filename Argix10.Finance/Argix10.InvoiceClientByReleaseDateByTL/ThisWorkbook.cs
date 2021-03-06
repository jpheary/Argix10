﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using Microsoft.VisualStudio.Tools.Applications.Runtime;
using Excel=Microsoft.Office.Interop.Excel;
using Office=Microsoft.Office.Core;

namespace Argix.Finance {
    //
    public partial class ThisWorkbook {
        //Members
        private const string USP_DETAIL = "uspInvTsortClientInvoiceByReleaseDateWithInfoFromFirstTLGet",TBL_DETAIL = "ClientInvoiceTable";
        private const int ROW0_DETAIL=12;

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
                clid = "157"; invoice = "332161600";    //Ann Taylor: 033, 330543000
#endif
                if (cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Create detail worksheet
                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL,global::Argix.Finance.Settings.Default.TSortR);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                        adapter.TableMappings.Add("Table",TBL_DETAIL);
                        InvoiceDataset ds = new InvoiceDataset();
                        adapter.Fill(ds,TBL_DETAIL);
                        if (ds.Tables[TBL_DETAIL] != null && ds.Tables[TBL_DETAIL].Rows.Count > 0) {
                            createDetailHeader(ds.ClientInvoiceTable[0]);
                            createDetailBody(ds.ClientInvoiceTable);
                            createDetailFooter(ds.ClientInvoiceTable[0]);
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
        private void InternalStartup() {
            this.Startup += new System.EventHandler(this.ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void createDetailHeader(InvoiceDataset.ClientInvoiceTableRow invoice) {
            //Set named range summary values
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Remit To
            detail.ClientNumberDiv.Value = (invoice.IsClientNumberNull()?"":invoice.ClientNumber.Trim()) + ' ' + (invoice.IsClientDivisionNull()?"":invoice.ClientDivision.Trim());
            detail.RemitToName.Value = invoice.RemitToName.Trim();
            detail.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim() + " " + invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();
            detail.Telephone.Value = (invoice.IsTelephoneNull() ? "" : invoice.Telephone.ToString());

            //Bill To
            detail.BillToName.Value = invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            detail.BillToAddressLine2.Value = (!invoice.IsBillToAddressline2Null() && invoice.BillToAddressline2.Trim().Length > 0) ? invoice.BillToAddressline2.Trim() : invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim();
            detail.BillToCityStateZip.Value = (!invoice.IsBillToAddressline2Null() && invoice.BillToAddressline2.Trim().Length > 0) ? invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() : "";

            //Account
            detail.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            detail.InvoiceDate.Value = invoice.InvoiceDate;
        }
        private void createDetailBody(InvoiceDataset.ClientInvoiceTableDataTable invoiceTable) {
            //Create body of detail worksheet
            //Excel.Worksheet ws = (Excel.Worksheet)this.Worksheets["Detail"];
            Detail ws = global::Argix.Finance.Globals.Detail;
            Application.ScreenUpdating = false;

            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = invoiceTable.Rows.Count;
            Excel.Range row0 = ws.Range[ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,21]].EntireRow;
            for(int i=0;i<rowCount - 1;i++)
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown,false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount,21];
            for(int i=0;i<rowCount;i++) {
                values[i,0] = "'" + invoiceTable[i].PRONumber.Trim();
                values[i,1] = "'" + (!invoiceTable[i].IsStoreNumberNull() ? invoiceTable[i].StoreNumber.ToString() : "");
                values[i,2] = "'" + invoiceTable[i].StoreName.Trim();
                values[i,3] = "'" + (!invoiceTable[i].IsStoreAddressLine1Null() ? invoiceTable[i].StoreAddressLine1.ToString() : "");
                values[i,4] = "'" + (!invoiceTable[i].IsStoreCityNull() ? invoiceTable[i].StoreCity.ToString() : "");
                values[i,5] = "'" + invoiceTable[i].StoreState.Trim();
                values[i,6] = "'" + invoiceTable[i].StoreZip.Trim();
                values[i,7] = invoiceTable[i].CtnQty;
                values[i,8] = invoiceTable[i].CartonRate;
                values[i,9] = invoiceTable[i].PltQty;
                values[i,10] = invoiceTable[i].PalletRate;
                values[i,11] = invoiceTable[i].Weight;
                values[i,12] = invoiceTable[i].RatedWeight;
                values[i,13] = invoiceTable[i].WeightRate;
                values[i,14] = invoiceTable[i].Surcharge;
                values[i,15] = invoiceTable[i].IsConsolidationChargeNull() ? 0.0m : invoiceTable[i].ConsolidationCharge;
                values[i,16] = invoiceTable[i].FuelRate;
                values[i,17] = invoiceTable[i].FuelSurcharge;
                values[i,18] = invoiceTable[i].DeliveryTotal;
                values[i,19] = "'" + invoiceTable[i].StoreBLNumber.Trim();
                values[i,20] = "'" + invoiceTable[i].MasterBLNumber.Trim();
            }
            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,21]].Value2 = values;

            Application.ScreenUpdating = true;
        }
        private void createDetailFooter(InvoiceDataset.ClientInvoiceTableRow invoice) {
            //
            Detail detail = global::Argix.Finance.Globals.Detail;
            detail.Reference.Value2 = "PLEASE REFERENCE INVOICE# " + invoice.InvoiceNumber + " WHEN REMITING PAYMENT. I.C.C. REGULATIONS REQUIRE THAT THIS BILL BE PAID WITHIN " + invoice.PaymentDays + " DAYS";
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
