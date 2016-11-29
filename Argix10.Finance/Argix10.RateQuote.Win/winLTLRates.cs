using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Argix.Windows;
using Infragistics.Win;
using Infragistics.Win.UltraWinGrid;
using Excel = Microsoft.Office.Interop.Excel;
using Argix.Freight;

namespace Argix.Finance {
    //
    public partial class winLTLRates:Form {
        //Members
        private DataSet mRates=null;
        private UltraGridSvc mGridSvc = null;

        //Interface
        public winLTLRates() {
            //Constructor
            try {
                InitializeComponent();
                this.mGridSvc = new UltraGridSvc(this.grdRates);
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
        private void OnFormLoad(object sender,EventArgs e) {
            //Event handler for form load event
            try {
                //Set control defaults
                #region Grid customizations from normal layout (to support cell editing)
                this.grdRates.DisplayLayout.Bands[0].Override.RowFilterMode = RowFilterMode.AllRowsInBand;
                #endregion
            
                //Get all tariffs and create dataset schema
                this.mRates = new DataSet();
                this.mRates.Tables.Add("LTLRatesTable");
                this.mRates.Tables["LTLRatesTable"].Columns.Add("Origin");
                this.mRates.Tables["LTLRatesTable"].Columns.Add("Destination");
                this.mRates.Tables["LTLRatesTable"].Columns.Add("NMFC");
                this.mRates.Tables["LTLRatesTable"].Columns.Add("Weight");
                DataModule[] tariffs = FinanceGateway.GetAvailableTariffs();
                for(int k = 0; k < tariffs.Length; k++) {
                    this.mRates.Tables["LTLRatesTable"].Columns.Add(tariffs[k].effectiveDateField);
                }
                this.mRates.Tables["LTLRatesTable"].Columns.Add("PSP");
                this.mRates.AcceptChanges();

                //Loop thru all delivery locations
                DataSet destinations = FinanceGateway.ViewDeliveryZips();
                DataRow row = null;
                //for(int j = 0; j < destinations.Tables["DeliveryZipTable"].Rows.Count; j=j+100) {
                for(int j = 0; j < 5; j++) {
                    //Get an LTL rate for each tariff for this destination
                    row = this.mRates.Tables["LTLRatesTable"].NewRow();
                    row["Origin"] = "07657";
                    row["Destination"] = destinations.Tables["DeliveryZipTable"].Rows[j]["Zip"].ToString();
                    row["NMFC"] = "50";
                    row["Weight"] = "1500";
                    for(int i = 0; i < tariffs.Length; i++) {
                        //Setup the request for NMFC=50, weight=100lbs, Origin=Ridgefield
                        DataModule tariff = tariffs[i];

                        LTLRateShipmentSimpleRequest request = new LTLRateShipmentSimpleRequest();
                        request.tariffNameField = tariff != null ? tariff.tariffNameField : "";
                        request.shipmentDateCCYYMMDDField = tariff != null ? tariff.effectiveDateField : "";
                        request.destinationCountryField = request.originCountryField = "USA";
                        request.originPostalCodeField = "07657";
                        request.destinationPostalCodeField = destinations.Tables["DeliveryZipTable"].Rows[j]["Zip"].ToString();
                        LTLRequestDetail detail = new LTLRequestDetail();
                        detail.nmfcClassField = "50";
                        detail.weightField = "1500";
                        request.detailsField = new LTLRequestDetail[] { detail };

                        //Get the LTL rate and add a datarow
                        LTLRateShipmentSimpleResponse response = FinanceGateway.CalculateLTLSimpleRate(request);
                        row[tariff.effectiveDateField] = response.totalChargeField;
                    }

                    //Get the PSP rate for this destination
                    LTLQuote2 quote = new LTLQuote2();
                    quote.ShipDate = DateTime.Today;
                    quote.OriginZip = "07657";
                    quote.DestinationZip = destinations.Tables["DeliveryZipTable"].Rows[j]["Zip"].ToString();
                    quote.Pallet1Weight = 100;
                    quote = FreightGateway.CreateQuote(quote);
                    row["PSP"] = quote.TotalCharge.ToString();

                    //Ad the results for this destination
                    this.mRates.Tables["LTLRatesTable"].Rows.Add(row);
                }
                this.grdRates.DataSource = this.mRates;
                this.grdRates.DataMember = "LTLRatesTable";
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }

        private void OnExport(object sender, EventArgs e) {
            //
            try {
			    SaveFileDialog dlgSave = new SaveFileDialog();
			    dlgSave.AddExtension = true;
			    dlgSave.Filter = "Export Files (*.xml) | *.xml | Excel Files (*.xls) | *.xls";
			    dlgSave.FilterIndex = 0;
			    dlgSave.Title = "Save Rates As...";
			    dlgSave.FileName = "LTL Rates";
			    dlgSave.OverwritePrompt = true;
                if(dlgSave.ShowDialog(this) == DialogResult.OK) {
                    this.Cursor = Cursors.WaitCursor;
                    Application.DoEvents();
                    if(dlgSave.FileName.EndsWith("xls")) {
                        new Argix.ExcelFormat().Transform(this.mRates, "LTLRatesTable", dlgSave.FileName);
                    }
                    else {
                        this.mRates.WriteXml(dlgSave.FileName, XmlWriteMode.WriteSchema);
                    }
                }
            }
            catch(Exception ex) { App.ReportError(ex, true, LogLevel.Error); }
        }
    }
}
