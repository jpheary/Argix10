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
        private const string USP_SUMMARY = "uspInvCoachInvoiceHeaderGet",TBL_SUMMARY = "InvoiceSummaryTable";
        private const string USP_DETAIL = "uspInvCoachInvoiceDetailGetList",TBL_DETAIL = "InvoiceDetailTable";
        private const string USP_TLDETAIL = "uspInvCoachTrailerLoadDetailGetList",TBL_TLDETAIL = "InvoiceTLDetailTable";

        [System.Runtime.InteropServices.DllImport("kernel32.dll",CharSet=System.Runtime.InteropServices.CharSet.Auto)]
        private static extern System.IntPtr GetCommandLine();

        //Interface
        private void ThisWorkbook_Startup(object sender, System.EventArgs e) {
            //Event handler for workbook startup event
            try {
                System.IntPtr p = GetCommandLine();
                string cmd = System.Runtime.InteropServices.Marshal.PtrToStringAuto(p);
                string clid="",invoice="";   
#if DEBUG
                clid = "092"; invoice = "331316400";    //010, 331316400
#endif
                if (cmd != null) {
                    string query = cmd.Substring(cmd.IndexOf('?') + 1);
                    string[] args = query.Split('&');
                    if(args.Length > 0) clid = args[0].Substring(args[0].IndexOf("=") + 1).Trim();
                    if(args.Length > 1) invoice = args[1].Substring(args[1].IndexOf("=") + 1).Trim();
                }
                if(invoice.Length > 0) {
                    //Display summary and detail data
                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_SUMMARY,global::Argix.Finance.Settings.Default.TSortR);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                        InvoiceDataset summaryDS = new InvoiceDataset();
                        adapter.TableMappings.Add("Table",TBL_SUMMARY);
                        adapter.Fill(summaryDS,TBL_SUMMARY);
                        if (summaryDS.Tables[TBL_SUMMARY] != null && summaryDS.Tables[TBL_SUMMARY].Rows.Count > 0) {
                            showSummary(summaryDS);
                        }
                        else
                            MessageBox.Show("No summary data found for invoice #" + invoice + ".");
                    }
                    catch(Exception ex) { reportError(ex); }

                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_DETAIL,global::Argix.Finance.Settings.Default.TSortR);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                        adapter.TableMappings.Add("Table",TBL_DETAIL);
                        InvoiceDataset detailDS = new InvoiceDataset();
                        adapter.Fill(detailDS,TBL_DETAIL);
                        if (detailDS.Tables[TBL_DETAIL] != null && detailDS.Tables[TBL_DETAIL].Rows.Count > 0) {
                            showDetail(detailDS);
                        }
                        else
                            MessageBox.Show("No detail data found for invoice #" + invoice + ".");
                    }
                    catch(Exception ex) { reportError(ex); }

                    try {
                        SqlDataAdapter adapter = new SqlDataAdapter(USP_TLDETAIL,global::Argix.Finance.Settings.Default.TSortR);
                        adapter.SelectCommand.CommandType = CommandType.StoredProcedure;
                        adapter.SelectCommand.Parameters.AddRange(new SqlParameter[] { new SqlParameter("@InvoiceNumber",invoice) });
                        adapter.TableMappings.Add("Table",TBL_TLDETAIL);
                        InvoiceDataset tldetailDS = new InvoiceDataset();
                        adapter.Fill(tldetailDS,TBL_TLDETAIL);
                        if (tldetailDS.Tables[TBL_TLDETAIL] != null && tldetailDS.Tables[TBL_TLDETAIL].Rows.Count > 0) {
                            showTLDetail(tldetailDS);
                        }
                        else
                            MessageBox.Show("No TL data found for invoice #" + invoice + ".");
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
            this.Startup += new System.EventHandler(this.ThisWorkbook_Startup);
            this.Shutdown += new System.EventHandler(ThisWorkbook_Shutdown);
            this.BeforeSave += new Microsoft.Office.Interop.Excel.WorkbookEvents_BeforeSaveEventHandler(ThisWorkbook_BeforeSave);
        }
        
        #endregion
        private void showSummary(InvoiceDataset ds) {
            //Set named range summary values
            Summary summary = global::Argix.Finance.Globals.Summary;
            InvoiceDataset.InvoiceSummaryTableRow invoice = ds.InvoiceSummaryTable[0];

            //Bill To
            summary.BillToName.Value = invoice.BillToName.Trim();
            summary.BillToAddressLine1.Value = invoice.BillToAddressline1.Trim();
            if(!invoice.IsBillToAddressline2Null()) summary.BillToAddressLine2.Value = invoice.BillToAddressline2.Trim();
            summary.BillToCityStateZip.Value = invoice.BillToCity.Trim() + ", " + invoice.BillToState.Trim() + " " + invoice.BillToZip.Trim() + "-" + invoice.BillToZIP4.Trim();

            //Account
            summary.Account.Value = invoice._Account_.Trim();
            summary.InvoiceNumber.Value = invoice.InvoiceNumber.Trim();
            summary.InvoiceDate.Value = invoice.InvoiceDate;
            summary.Terms1.Value = invoice.Terms;
            
            //Remit To
            summary.RemitToName.Value = invoice.RemitToName.Trim();
            summary.RemitToAddressLine1.Value = invoice.RemitToAddressLine1.Trim();
            if(!invoice.IsRemitToAddressLine2Null()) summary.RemitToAddressLine2.Value = invoice.RemitToAddressLine2.Trim();
            summary.RemitToCityStateZip.Value = invoice.RemitToCity.Trim() + ", " + invoice.RemitToState.Trim() + " " + invoice.RemitToZip.Trim() + "-" + invoice.RemitToZip4.Trim();

            //Shipment
            if(!invoice.IsShipmentDayNull()) summary.ShipmentDay.Value = invoice.ShipmentDay;
            if(!invoice.IsPiecesNull()) summary.Pieces.Value = invoice.Pieces;
            if(!invoice.IsWeightNull()) summary.Weight.Value = invoice.Weight;
            if(!invoice.IsDeliveryChargesNull()) summary.DeliveryCharges.Value = invoice.DeliveryCharges;
            if(!invoice.IsLinehaulChargesNull()) summary.LineHaulCharges.Value = invoice.LinehaulCharges;
        }
        private void showDetail(InvoiceDataset ds) {
            //Get worksheet
            Detail detail = global::Argix.Finance.Globals.Detail;

            //Header
            int rows = ds.InvoiceDetailTable.Rows.Count;
            if(rows == 0) return;
            detail.Manifests.Value = ds.InvoiceDetailTable[0].ManifestNumbers;
            detail.PerGallon.Value = ds.InvoiceDetailTable[0].PerGallon;
            detail.FSC.Value = ds.InvoiceDetailTable[0].FSCPercentage/100;

            //Detail
            object[,] destZip = new object[rows,1];
            object[,] store = new object[rows,1];
            object[,] city = new object[rows,1];
            object[,] state = new object[rows,1];
            object[,] pieces = new object[rows,1];
            object[,] weight = new object[rows,1];
            object[,] delivrate = new object[rows,1];
            object[,] delivchgs = new object[rows,1];
            object[,] fuelsurchg = new object[rows,1];
            object[,] totaldelivchgs = new object[rows,1];
            object[,] lhalloc = new object[rows,1];
            object[,] totalchgs = new object[rows,1];
            for(int i = 0; i < rows; i++) {
                if(!ds.InvoiceDetailTable[i].IsDestinationZipNull()) destZip[i,0] = ds.InvoiceDetailTable[i].DestinationZip;
                if(!ds.InvoiceDetailTable[i].IsStoreNumberNull()) store[i,0] = ds.InvoiceDetailTable[i].StoreNumber;
                if(!ds.InvoiceDetailTable[i].IsStoreCityNull()) city[i,0] = ds.InvoiceDetailTable[i].StoreCity;
                if(!ds.InvoiceDetailTable[i].IsDestinationStateNull()) state[i,0] = ds.InvoiceDetailTable[i].DestinationState;
                if(!ds.InvoiceDetailTable[i].IsTotalPiecesNull()) pieces[i,0] = ds.InvoiceDetailTable[i].TotalPieces;
                if(!ds.InvoiceDetailTable[i].IsTotalWeightNull()) weight[i,0] = ds.InvoiceDetailTable[i].TotalWeight;
                if(!ds.InvoiceDetailTable[i].IsDeliveryRateNull()) delivrate[i,0] = ds.InvoiceDetailTable[i].DeliveryRate;
                if(!ds.InvoiceDetailTable[i].IsDeliveryChargesNull()) delivchgs[i,0] = ds.InvoiceDetailTable[i].DeliveryCharges;
                if(!ds.InvoiceDetailTable[i].IsFuelSurchargeNull()) fuelsurchg[i,0] = ds.InvoiceDetailTable[i].FuelSurcharge;
                if(!ds.InvoiceDetailTable[i].IsTotalDeliveryChargesNull()) totaldelivchgs[i,0] = ds.InvoiceDetailTable[i].TotalDeliveryCharges;
                if(!ds.InvoiceDetailTable[i].IsLHAllocationNull()) lhalloc[i,0] = ds.InvoiceDetailTable[i].LHAllocation;
                if(!ds.InvoiceDetailTable[i].IsTotalChargesNull()) totalchgs[i,0] = ds.InvoiceDetailTable[i].TotalCharges;
            }
            detail.Range[detail.Cells[4,1],detail.Cells[3 + rows,1]].Value2 = destZip;
            detail.Range[detail.Cells[4,2],detail.Cells[3 + rows,2]].Value2 = store;
            detail.Range[detail.Cells[4,3],detail.Cells[3 + rows,3]].Value2 = city;
            detail.Range[detail.Cells[4,4],detail.Cells[3 + rows,4]].Value2 = state;
            detail.Range[detail.Cells[4,5],detail.Cells[3 + rows,5]].Value2 = pieces;
            detail.Range[detail.Cells[4,6],detail.Cells[3 + rows,6]].Value2 = weight;
            detail.Range[detail.Cells[4,7],detail.Cells[3 + rows,7]].Value2 = delivrate;
            detail.Range[detail.Cells[4,8],detail.Cells[3 + rows,8]].Value2 = delivchgs;
            detail.Range[detail.Cells[4,9],detail.Cells[3 + rows,9]].Value2 = fuelsurchg;
            detail.Range[detail.Cells[4,10],detail.Cells[3 + rows,10]].Value2 = totaldelivchgs;
            detail.Range[detail.Cells[4,11],detail.Cells[3 + rows,11]].Value2 = lhalloc;
            detail.Range[detail.Cells[4,12],detail.Cells[3 + rows,12]].Value2 = totalchgs;
        }
        private void showTLDetail(InvoiceDataset ds) {
            //Get worksheet
            TLDetail detail = global::Argix.Finance.Globals.TLDetail;

            //Header
            int rows = ds.InvoiceTLDetailTable.Rows.Count;
            if(rows == 0) return;
            detail.PerGallonTL.Value = ds.InvoiceTLDetailTable[0].PerGallon;

            //Detail
            object[,] trip = new object[rows,1];
            object[,] departdate = new object[rows,1];
            object[,] unloaddate = new object[rows,1];
            object[,] trailer = new object[rows,1];
            object[,] miles = new object[rows,1];
            object[,] ratepermile = new object[rows,1];
            object[,] fscpermile = new object[rows,1];
            object[,] totalmiles = new object[rows,1];
            object[,] trailerchg = new object[rows,1];
            object[,] totalweight = new object[rows,1];
            object[,] costperlb = new object[rows,1];
            object[,] bol = new object[rows,1];
            for(int i = 0; i < rows; i++) {
                trip[i,0] = ds.InvoiceTLDetailTable[i].Trip;
                if(!ds.InvoiceTLDetailTable[i].IsDepartureDateNull()) departdate[i,0] = ds.InvoiceTLDetailTable[i].DepartureDate;
                if(!ds.InvoiceTLDetailTable[i].IsUnloadDateNull()) unloaddate[i,0] = ds.InvoiceTLDetailTable[i].UnloadDate;
                if(!ds.InvoiceTLDetailTable[i].IsTrailerNumberNull()) trailer[i,0] = ds.InvoiceTLDetailTable[i].TrailerNumber.Trim();
                if(!ds.InvoiceTLDetailTable[i].IsMilesNull()) miles[i,0] = ds.InvoiceTLDetailTable[i].Miles;
                if(!ds.InvoiceTLDetailTable[i].IsRatePerMileNull()) ratepermile[i,0] = ds.InvoiceTLDetailTable[i].RatePerMile;
                if(!ds.InvoiceTLDetailTable[i].IsFCSPerMileNull()) fscpermile[i,0] = ds.InvoiceTLDetailTable[i].FCSPerMile;
                if(!ds.InvoiceTLDetailTable[i].IsTotalPerMileNull()) totalmiles[i,0] = ds.InvoiceTLDetailTable[i].TotalPerMile;
                if(!ds.InvoiceTLDetailTable[i].IsTrailerChargeNull()) trailerchg[i,0] = ds.InvoiceTLDetailTable[i].TrailerCharge;
                if(!ds.InvoiceTLDetailTable[i].IsTotalWeightNull()) totalweight[i,0] = ds.InvoiceTLDetailTable[i].TotalWeight;
                if(!ds.InvoiceTLDetailTable[i].IsCostPerPoundNull()) costperlb[i,0] = ds.InvoiceTLDetailTable[i].CostPerPound;
                if(!ds.InvoiceTLDetailTable[i].IsBLNumberNull()) bol[i,0] = ds.InvoiceTLDetailTable[i].BLNumber;
            }
            detail.Range[detail.Cells[4,1],detail.Cells[3 + rows,1]].Value2 = trip;
            detail.Range[detail.Cells[4,2],detail.Cells[3 + rows,2]].Value2 = departdate;
            detail.Range[detail.Cells[4,3],detail.Cells[3 + rows,3]].Value2 = unloaddate;
            detail.Range[detail.Cells[4,4],detail.Cells[3 + rows,4]].Value2 = trailer;
            detail.Range[detail.Cells[4,5],detail.Cells[3 + rows,5]].Value2 = miles;
            detail.Range[detail.Cells[4,6],detail.Cells[3 + rows,6]].Value2 = ratepermile;
            detail.Range[detail.Cells[4,7],detail.Cells[3 + rows,7]].Value2 = fscpermile;
            detail.Range[detail.Cells[4,8],detail.Cells[3 + rows,8]].Value2 = totalmiles;
            detail.Range[detail.Cells[4,9],detail.Cells[3 + rows,9]].Value2 = trailerchg;
            detail.Range[detail.Cells[4,11],detail.Cells[3 + rows,11]].Value2 = totalweight;
            detail.Range[detail.Cells[4,12],detail.Cells[3 + rows,12]].Value2 = costperlb;
            detail.Range[detail.Cells[4,13],detail.Cells[3 + rows,13]].Value2 = bol;
        }
        private void reportError(Exception ex) {
            MessageBox.Show("ERROR: " + ex.Message);
        }
    }
}
