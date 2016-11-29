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
        private const string USP_DETAIL = "uspInvDotsInvoiceGet",TBL_DETAIL = "ClientInvoiceTable";
        private const int ROW0_DETAIL=14;

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
                clid = "069"; invoice = "332114700";    //DOTS: 069, 330544000
#endif
                if (cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Create detail worksheet
                    SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL,global::Argix.Finance.Settings.Default.TSortR);
                    adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                    adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                    adapter.TableMappings.Add("Table",TBL_DETAIL);
                    InvoiceDataset ds = new InvoiceDataset();
                    adapter.Fill(ds,TBL_DETAIL);
                    if (ds.Tables[TBL_DETAIL] != null && ds.Tables[TBL_DETAIL].Rows.Count > 0) {
                        createDetailHeader(ds.ClientInvoiceTable[0]);
                        createDetailBody(ds.ClientInvoiceTable);
                        createDetailTotals(ds.ClientInvoiceTable);
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
        private void InternalStartup()
        {
            this.Startup += new System.EventHandler(ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }

        #endregion
        private void createDetailHeader(InvoiceDataset.ClientInvoiceTableRow invoice) {
            //Create header of detail worksheet
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Bill To
            detail.BillToAttention.Value = invoice.BillToAttention;
            detail.BillToName.Value = invoice.BillToName.Trim();
            detail.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            //detail.BillToAddressLine2.Value = invoice.IsBillToAddressline2Null() ? "" : invoice.BillToAddressline2.Trim();
            detail.BillToCityStateZip.Value = invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Remit To
            detail.RemitToName.Value = invoice.RemitToName.Trim();
            detail.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim();
            //detail.RemitToAddressLine2.Value = invoice.IsRemitToAddressLine2Null() ? "" : invoice.RemitToAddressLine2.Trim();
            detail.RemitToCityStateZip.Value = invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim();

            //Account
            detail.InvoiceDate.Value = invoice.InvoiceDate;
            detail.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            detail.InvoiceAmount.Value = invoice.InvoiceAmount;
            detail.AccountNumber.Value = !invoice.Is_Account_Null() ? invoice._Account_ : "";
            detail.ServicePeriod.Value = invoice.ServicePeriodStart.ToString("MM/dd/yyyy") + " - " + invoice.ServicePeriodSEnd.ToString("MM/dd/yyyy");
            detail.FuelPrice.Value = !invoice.IsFuelPriceLineHaulNull() ? invoice.FuelPriceLineHaul : 0.0M;
            detail.FuelCharge.Value = !invoice.IsFuelChargePCTLineHaulNull() ? invoice.FuelChargePCTLineHaul / 100 : 0.0M;
            detail.FuelPriceD.Value = !invoice.IsFuelPriceDeliveryNull() ? invoice.FuelPriceDelivery : 0.0M;
            detail.FuelChargeD.Value = !invoice.IsFuelChargePCTDeliveryNull() ? invoice.FuelChargePCTDelivery / 100 : 0.0M;
            detail.PhoneNumber.Value = invoice.PhoneNumber;
            detail.Email1.Value = !invoice.IsEmailLMOORENull() ? invoice.EmailLMOORE : "";
            detail.Email2.Value = !invoice.IsEmailRVRANICNull() ? invoice.EmailRVRANIC : "";
        }
        private void createDetailBody(InvoiceDataset.ClientInvoiceTableDataTable invoiceTable) {
            //Create body of detail worksheet
            Detail ws = global::Argix.Finance.Globals.Detail;
            Application.ScreenUpdating = false;

            //Insert a row at row0 + 1 (pushes down) for every row of data
            int rowCount = invoiceTable.Rows.Count;
            Excel.Range row0 = ws.Range[ws.Cells[ROW0_DETAIL + 1,1],ws.Cells[ROW0_DETAIL + 1,12]].EntireRow;
            for(int i=0;i<rowCount - 1;i++)
                row0.Insert(Excel.XlInsertShiftDirection.xlShiftDown,false);

            //Populate entire data table into a range of worksheet cells
            object[,] values = new object[rowCount,12];
            for(int i=0;i<rowCount;i++) {
                if(!invoiceTable[i].IsShipDateNull()) {
                    values[i,0] = (!invoiceTable[i].IsShipDateNull() ? invoiceTable[i].ShipDate.ToString("MM/dd/yyyy") : "");
                    values[i,1] = "'" + (!invoiceTable[i].IsNoColumnNameNull() ? invoiceTable[i].NoColumnName.ToString() : "");
                    values[i,2] = "'" + (!invoiceTable[i].IsPRONull() ? invoiceTable[i].PRO.Trim() : "");
                    values[i,3] = (!invoiceTable[i].IsPCSNull() ? invoiceTable[i].PCS : 0);
                    values[i,4] = (!invoiceTable[i].IsWEIGHTNull() ? invoiceTable[i].WEIGHT : 0);
                    values[i,5] = "'" + (!invoiceTable[i].IsCONSIGNEENull() ? invoiceTable[i].CONSIGNEE.Trim() : "");
                    values[i,6] = (!invoiceTable[i].Is_CITY_STNull() ? invoiceTable[i]._CITY_ST.Trim() : "");
                    values[i,7] = (!invoiceTable[i].IsTotalNull() ? invoiceTable[i].Total : 0);
                    values[i,8] = (!invoiceTable[i].IsSpecialChargesNull() ? invoiceTable[i].SpecialCharges : 0);
                    values[i,9] = (!invoiceTable[i].IsFuelNull() ? invoiceTable[i].Fuel : 0);
                    values[i,10] = (!invoiceTable[i].IsDeliveryNull() ? invoiceTable[i].Delivery : 0);
                    values[i,11] = "'" + (!invoiceTable[i].IsPoolNull() ? invoiceTable[i].Pool.Trim() : "");
                }
            }
            ws.Range[ws.Cells[ROW0_DETAIL,1],ws.Cells[ROW0_DETAIL + rowCount - 1,12]].Value2 = values;
            
            ws.Range[ws.Cells[ROW0_DETAIL,4],ws.Cells[ROW0_DETAIL + rowCount - 1,4]].NumberFormat = "#,###_);(#,###)";
            ws.Range[ws.Cells[ROW0_DETAIL,4],ws.Cells[ROW0_DETAIL + rowCount - 1,4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,5],ws.Cells[ROW0_DETAIL + rowCount - 1,5]].NumberFormat = "#,###_);(#,###)";
            ws.Range[ws.Cells[ROW0_DETAIL,5],ws.Cells[ROW0_DETAIL + rowCount - 1,5]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,8],ws.Cells[ROW0_DETAIL + rowCount - 1,8]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL,8],ws.Cells[ROW0_DETAIL + rowCount - 1,8]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,9],ws.Cells[ROW0_DETAIL + rowCount - 1,9]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL,9],ws.Cells[ROW0_DETAIL + rowCount - 1,9]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,10],ws.Cells[ROW0_DETAIL + rowCount - 1,10]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL,10],ws.Cells[ROW0_DETAIL + rowCount - 1,10]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL,11],ws.Cells[ROW0_DETAIL + rowCount - 1,11]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL,11],ws.Cells[ROW0_DETAIL + rowCount - 1,11]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;

            Application.ScreenUpdating = true;
        }
        private void createDetailTotals(InvoiceDataset.ClientInvoiceTableDataTable invoiceTable) {
            //Get worksheet
            Detail ws = global::Argix.Finance.Globals.Detail;

            //Totals
            int rowCount = invoiceTable.Rows.Count;
            object[,] totals = new object[1,12];
            int pcs=0,weight=0;
            decimal total=0.0M,charges=0.0M,fuel=0.0M,delivery=0.0M;
            bool start=false;        //Flag to skip linehaul charge (i.e. first row); turned on per Maryann 07/24/13
            for(int i=0;i<rowCount;i++) {
                //if(start) {
                pcs += !invoiceTable[i].IsPCSNull() ? invoiceTable[i].PCS : 0;
                weight += !invoiceTable[i].IsWEIGHTNull() ? invoiceTable[i].WEIGHT : 0;
                total += !invoiceTable[i].IsTotalNull() ? invoiceTable[i].Total : 0.0M;
                charges += !invoiceTable[i].IsSpecialChargesNull() ? invoiceTable[i].SpecialCharges : 0.0M;
                fuel += !invoiceTable[i].IsFuelNull() ? invoiceTable[i].Fuel : 0.0M;
                delivery += !invoiceTable[i].IsDeliveryNull() ? invoiceTable[i].Delivery : 0.0M;
                //}
                if(!start) start = invoiceTable[i].IsShipDateNull();
            }
            totals[0,0] = totals[0,1] = totals[0,2] = "";
            totals[0,3] = pcs;
            totals[0,4] = weight;
            totals[0,5] = totals[0,6] = "";
            totals[0,7] = total;
            totals[0,8] = charges;
            totals[0,9] = fuel;
            totals[0,10] = delivery;
            totals[0,11] = "";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,1],ws.Cells[ROW0_DETAIL + rowCount + 0,12]].Value2 = totals;

            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,4],ws.Cells[ROW0_DETAIL + rowCount + 0,4]].NumberFormat = "#,###_);(#,###)";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,4],ws.Cells[ROW0_DETAIL + rowCount + 0,4]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,4],ws.Cells[ROW0_DETAIL + rowCount + 0,4]].Font.Bold = true;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,5],ws.Cells[ROW0_DETAIL + rowCount + 0,5]].NumberFormat = "#,##0_);(#,##0)";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,5],ws.Cells[ROW0_DETAIL + rowCount + 0,5]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,5],ws.Cells[ROW0_DETAIL + rowCount + 0,5]].Font.Bold = true;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,8],ws.Cells[ROW0_DETAIL + rowCount + 0,8]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,8],ws.Cells[ROW0_DETAIL + rowCount + 0,8]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,8],ws.Cells[ROW0_DETAIL + rowCount + 0,8]].Font.Bold = true;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,9],ws.Cells[ROW0_DETAIL + rowCount + 0,9]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,9],ws.Cells[ROW0_DETAIL + rowCount + 0,9]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,9],ws.Cells[ROW0_DETAIL + rowCount + 0,9]].Font.Bold = true;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,10],ws.Cells[ROW0_DETAIL + rowCount + 0,10]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,10],ws.Cells[ROW0_DETAIL + rowCount + 0,10]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,10],ws.Cells[ROW0_DETAIL + rowCount + 0,10]].Font.Bold = true;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,11],ws.Cells[ROW0_DETAIL + rowCount + 0,11]].NumberFormat = "$#,##0.00_);($#,##0.00)";
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,11],ws.Cells[ROW0_DETAIL + rowCount + 0,11]].HorizontalAlignment = Excel.XlHAlign.xlHAlignRight;
            ws.Range[ws.Cells[ROW0_DETAIL + rowCount + 0,11],ws.Cells[ROW0_DETAIL + rowCount + 0,11]].Font.Bold = true;
        }
        private void reportError(Exception ex) { MessageBox.Show("UNEXPECTED ERROR: " + ex.Message); }
    }
}
