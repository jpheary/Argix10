using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.Security;
using System.Web.Services;
using System.Web.Services.Protocols;

namespace Argix {
    //
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.NotAllowed)]
    public class ConsumerTrackingService:IConsumerTrackingService {
        //Members

        //Interface
        public ConsumerTrackingService() { }
        public TrackingDataset TrackCartons(string[] cartonNumbers,string clientNumber,string vendorNumber) {
            //Get a list of carton tracking details
            TrackingDataset trackDS = null;     //Composite dataset (i.e. summary and detail info)
            try {
                //Init
                trackDS = new TrackingDataset();
                TrackResponse trackResponse = new TrackResponse();
                Exception Exa = null,Exu = null;
                
                #region 1. Get Argix tracking data 
                try {
                    string numbers = "";
                    for(int i = 0; i < cartonNumbers.Length; i++) { if(i > 0) numbers += ","; numbers += cartonNumbers[i]; }
                    TrackResponse _respone = new TrackResponse();
                    _respone.Merge(new EnterpriseRGateway().TrackUSPSCartons(numbers,clientNumber,vendorNumber));
                    if (_respone.TrackingTable.Rows.Count > 0) 
                        trackResponse.Merge(_respone.TrackingTable.Select("","Date DESC"));
                }
                catch(Exception ex) { Exa = ex; }
                #endregion
                #region 2. Get USPS carton tracking details if no Argix data or TenderedDate is null
                try {
                    //Build string[] of applicable #s for USPS web service data call
                    //1. Get data for cartonNumbers[i] if no Argix data or TenderedDate=null
                    //2. Get data for OsTrackingNumber if OsTrackingNumber != cartonNumbers[i]
                    ArrayList ctnList = new ArrayList();
                    for(int i = 0;i < cartonNumbers.Length;i++) {
                        //Select Argix data for this carton number
                        TrackResponse.TrackingTableRow[] items = (TrackResponse.TrackingTableRow[])trackResponse.TrackingTable.Select("ItemNumber='" + cartonNumbers[i] + "'");
                        if(items.Length == 0 || (items.Length > 0 && items[0].IsTenderedDateNull()))
                            ctnList.Add(cartonNumbers[i]);
                        if(items.Length > 0 && (items[0].OsTrackingNumber.Trim() != cartonNumbers[i]))
                            ctnList.Add(items[0].OsTrackingNumber.Trim());
                    }

                    //Make a batch call to the USPS web service for cartonNumbers
                    string[] request = new string[ctnList.Count];
                    for(int j=0; j<ctnList.Count; j++) request[j] = (string)ctnList[j];
                    trackResponse.Merge(new USPSGateway().TrackFieldRequests(request));
                }
                catch(Exception ex) { Exu = ex; }
                #endregion
                #region 3. Populate tracking view dataset; use two tables in TrackingDataset:
                //1. TrackingDataset.TrackingSummaryTable- holds a single summary record per carton item
                //2. TrackingDataset.TrackingDetailTable- holds all detail records for all carton items
                for(int i = 0; i < cartonNumbers.Length; i++) {
                    //Create summary/detail for each carton
                    string itemNumber = cartonNumbers[i].Trim();
                    string osTrackingNumber = "";
                    TrackResponse.TrackingTableRow[] items = (TrackResponse.TrackingTableRow[])trackResponse.TrackingTable.Select("ItemNumber='" + cartonNumbers[i] + "'");
                    if(items.Length > 0 && !items[0].IsOsTrackingNumberNull() && items[0].OsTrackingNumber.Trim().Length > 0)
                        osTrackingNumber = items[0].OsTrackingNumber.Trim();
                    
                    //Get all data for this carton#
                    //NOTE: rgxRows      Argix records
                    //      uspsRows     TrackResponse.TrackInfoRow parent record (single record per item)
                    //      uspsSumRow   TrackResponse.TrackSummary child record (single record per item)
                    //      uspsDetRows  TrackResponse.TrackDetail child records (multiple records per item)
                    //      uspsErrRow   TrackResponse.Error child records (single record per item)
                    TrackResponse.TrackingTableRow[] rgxRows = (TrackResponse.TrackingTableRow[])trackResponse.TrackingTable.Select("ItemNumber='" + itemNumber + "'","Date DESC, Time DESC");
                    TrackResponse.TrackInfoRow[] uspsRows=null;
                    TrackResponse.TrackInfoRow[] uspsRows1 = (TrackResponse.TrackInfoRow[])trackResponse.TrackInfo.Select("ID='" + itemNumber + "'");
                    TrackResponse.TrackInfoRow[] uspsRows2 = (TrackResponse.TrackInfoRow[])trackResponse.TrackInfo.Select("ID='" + osTrackingNumber + "'");
                    TrackResponse.TrackSummaryRow[] uspsSumRow=new TrackResponse.TrackSummaryRow[] { };
                    TrackResponse.TrackDetailRow[] uspsDetRows=new TrackResponse.TrackDetailRow[] { };
                    TrackResponse.ErrorRow[] uspsErrRow=new TrackResponse.ErrorRow[] { };

                    //Order of use: cartonNumber detail, osTrackingNumber detail, cartonNumber error, osTrackingNumber error
                    if(uspsRows1.Length > 0 && trackResponse.TrackSummary.Select("TrackInfo_Id=" + uspsRows1[0].TrackInfo_Id).Length > 0)
                        uspsRows = uspsRows1;
                    else if(uspsRows2.Length > 0 && trackResponse.TrackSummary.Select("TrackInfo_Id=" + uspsRows2[0].TrackInfo_Id).Length > 0)
                        uspsRows = uspsRows2;
                    else if(uspsRows1.Length > 0 && trackResponse.Error.Select("TrackInfo_Id=" + uspsRows1[0].TrackInfo_Id).Length > 0)
                        uspsRows = uspsRows1;
                    else if(uspsRows2.Length > 0 && trackResponse.Error.Select("TrackInfo_Id=" + uspsRows2[0].TrackInfo_Id).Length > 0)
                        uspsRows = uspsRows2;

                    if(uspsRows != null && uspsRows.Length > 0) {
                        //Determine USPS summary, detail, and error records
                        int id = uspsRows[0].TrackInfo_Id;
                        uspsSumRow = (TrackResponse.TrackSummaryRow[])trackResponse.TrackSummary.Select("TrackInfo_Id=" + id);
                        uspsDetRows = (TrackResponse.TrackDetailRow[])trackResponse.TrackDetail.Select("TrackInfo_Id=" + id);
                        uspsErrRow = (TrackResponse.ErrorRow[])trackResponse.Error.Select("TrackInfo_Id=" + id);
                    
                        //Remove 'Electronic Billing Info Received' record (if applicable)
                        //Rebuild uspsSumRow, uspsDetRows since dataset maybe modified by a delete
                        for(int j=0;j<uspsSumRow.Length;j++) {
                            if(uspsSumRow[j].Event.Contains("Electronic")) uspsSumRow[j].Delete();
                        }
                        uspsSumRow = (TrackResponse.TrackSummaryRow[])trackResponse.TrackSummary.Select("TrackInfo_Id=" + id);
                        for(int j=0;j<uspsDetRows.Length;j++) {
                            if(uspsDetRows[j].Event.Contains("Electronic")) uspsDetRows[j].Delete();
                        }
                        uspsDetRows = (TrackResponse.TrackDetailRow[])trackResponse.TrackDetail.Select("TrackInfo_Id=" + id);
                    }
    #if DEBUG
                    DataSet ds = new DataSet();
                    if(rgxRows != null) ds.Merge(rgxRows);
                    if(uspsRows != null) ds.Merge(uspsRows);
                    if(uspsSumRow != null) ds.Merge(uspsSumRow);
                    if(uspsDetRows != null) ds.Merge(uspsDetRows);
                    if(uspsErrRow != null) ds.Merge(uspsErrRow);
                    Debug.WriteLine(ds.GetXml() + "\n");
#endif
                    //Create a summary record for this carton# (this is a key for detail records)
                    TrackingDataset.TrackingSummaryTableRow sumRow = trackDS.TrackingSummaryTable.NewTrackingSummaryTableRow();
                    sumRow.ItemNumber = itemNumber;
                    trackDS.TrackingSummaryTable.AddTrackingSummaryTableRow(sumRow);
                    
                    //Build detail for this carton#
                    if(rgxRows.Length > 0) {
                        //Update summary; populate with details from first (or any) Argix record for this carton#
                        #region Copy fields from TrackResponse.TrackingTable to TrackingDataset.TrackingSummaryTable
                        sumRow.LabelSequenceNumber = rgxRows[0].LabelSequenceNumber;
                        if(!rgxRows[0].IsDateNull()) sumRow.Date = rgxRows[0].Date;
                        if(!rgxRows[0].IsTimeNull()) sumRow.Time = rgxRows[0].Time;
                        sumRow.Status = rgxRows[0].Status.Trim();
                        sumRow.LocationName = rgxRows[0].LocationName.Trim();
                        sumRow.ShipperName = rgxRows[0].ShipperName.Trim();
                        sumRow.ShipperCity = rgxRows[0].ShipperCity.Trim();
                        sumRow.ShipperState = rgxRows[0].ShipperState.Trim();
                        sumRow.ShipperZip = rgxRows[0].ShipperZip.Trim();
                        sumRow.ShipperCountry = rgxRows[0].ShipperCountry.Trim();
                        sumRow.ConsigneeName = rgxRows[0].ConsigneeName.Trim();
                        sumRow.ConsigneeCity = rgxRows[0].ConsigneeCity.Trim();
                        sumRow.ConsigneeState = rgxRows[0].ConsigneeState.Trim();
                        sumRow.ConsigneeZip = rgxRows[0].ConsigneeZip.Trim();
                        sumRow.ConsigneeCountry = rgxRows[0].ConsigneeCountry.Trim();
                        sumRow.Pieces = rgxRows[0].Pieces;
                        sumRow.Weight = rgxRows[0].Weight;
                        if(!rgxRows[0].IsShipDateNull()) sumRow.ShipDate = rgxRows[0].ShipDate;
                        if(!rgxRows[0].IsShipTimeNull()) sumRow.ShipTime = rgxRows[0].ShipTime;
                        if(!rgxRows[0].IsTenderedDateNull()) sumRow.TenderedDate = rgxRows[0].TenderedDate;
                        if(!rgxRows[0].IsTenderedTimeNull()) sumRow.TenderedTime = rgxRows[0].TenderedTime;
                        #endregion
                        
                        //Add Argix tracking detail records
                        for(int j = 0; j < rgxRows.Length; j++) {
                            trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,rgxRows[j].Date,rgxRows[j].Time,rgxRows[j].Status.Trim(),rgxRows[j].LocationName.Trim());
                        }

                        //Copy USPS data for this item
                        if(uspsSumRow.Length > 0 || uspsDetRows.Length > 0) { 
                            //Add USPS tracking data
                            if(uspsSumRow.Length > 0) {
                                DateTime time = Convert.ToDateTime("1900-01-01 " + uspsSumRow[0].EventTime);
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,Convert.ToDateTime(uspsSumRow[0].EventDate),time,uspsSumRow[0].Event,uspsSumRow[0].EventCity + ", " + uspsSumRow[0].EventState + " " + uspsSumRow[0].EventZIPCode);
                            }
                            for(int n = 0; n < uspsDetRows.Length; n++) {
                                DateTime time2 = Convert.ToDateTime("1900-01-01 " + uspsDetRows[n].EventTime);
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,Convert.ToDateTime(uspsDetRows[n].EventDate),time2,uspsDetRows[n].Event,uspsDetRows[n].EventCity + ", " + uspsDetRows[n].EventState + " " + uspsDetRows[n].EventZIPCode);
                            }
                        }
                    }
                    else {
                        //No Argix data
                        //Update summary; populate with 'N/A'
                        #region TrackingDataset.TrackingSummaryTable
                        sumRow.LabelSequenceNumber = 0;
                        sumRow.Date = sumRow.Time = DateTime.Now;
                        if(uspsSumRow.Length > 0)
                            sumRow.Status = uspsSumRow[0].Event;
                        else if(uspsDetRows.Length > 0)
                            sumRow.Status = uspsDetRows[0].Event;
                        else if(uspsErrRow.Length > 0)
                            uspsErrRow[0].Description.Trim();
                        sumRow.LocationName = "N/A";
                        sumRow.ShipperName = sumRow.ShipperCity = sumRow.ShipperState = sumRow.ShipperZip = sumRow.ShipperCountry = "N/A";
                        sumRow.ConsigneeName = sumRow.ConsigneeCity = sumRow.ConsigneeState = sumRow.ConsigneeZip = sumRow.ConsigneeCountry = "N/A";
                        sumRow.Pieces = sumRow.Weight = 0;
                        //sumRow.ShipDate = sumRow.ShipTime = ;
                        //sumRow.TenderedDate = sumRow.TenderedTime = ;
                        #endregion

                        //Copy USPS data for this item
                        if(uspsSumRow.Length > 0 || uspsDetRows.Length > 0 || uspsErrRow.Length > 0) {
                            //Add USPS tracking data
                            if(uspsSumRow.Length > 0) {
                                DateTime time = Convert.ToDateTime("1900-01-01 " + uspsSumRow[0].EventTime);
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,Convert.ToDateTime(uspsSumRow[0].EventDate),time,uspsSumRow[0].Event,uspsSumRow[0].EventCity + ", " + uspsSumRow[0].EventState + " " + uspsSumRow[0].EventZIPCode);
                            }
                            for(int n = 0; n < uspsDetRows.Length; n++) {
                                DateTime time2 = Convert.ToDateTime("1900-01-01 " + uspsDetRows[n].EventTime);
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,Convert.ToDateTime(uspsDetRows[n].EventDate),time2,uspsDetRows[n].Event,uspsDetRows[n].EventCity + ", " + uspsDetRows[n].EventState + " " + uspsDetRows[n].EventZIPCode);
                            }
                            //02/06/13- Don't return "No Record Of That Item" in case of error
                            //for(int e = 0; e < uspsErrRow.Length; e++)
                                //trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,DateTime.Now,DateTime.Now,uspsErrRow[e].Description,"N/A");
                            if(uspsErrRow.Length > 0) 
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,DateTime.Now,DateTime.Now,"Order Being Processed","N/A");
                        }
                        else {
                            //No USPS data for this carton- check for USPS error
                            if(Exa != null || Exu != null) {
                                //Argix or USPS exception- add "Order Being Processed" record
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,DateTime.Now,DateTime.Now,"Order Being Processed","N/A");
                            }
                            else if(Exa == null && Exu == null) {
                                //No data, no exceptions (should not happen)- add "Item Not Found" record
                                trackDS.TrackingDetailTable.AddTrackingDetailTableRow(sumRow,DateTime.Now,DateTime.Now,"Item Not Found","N/A");
                            }
                        }
                    }

                    //Update summary record with most recent tracking detail record
                    TrackingDataset.TrackingDetailTableRow[] _rows = (TrackingDataset.TrackingDetailTableRow[])trackDS.TrackingDetailTable.Select("ItemNumber='" + itemNumber + "'","Date DESC, Time DESC");
                    sumRow.Date = _rows[0].Date;
                    sumRow.Time = _rows[0].Time;
                    sumRow.Status = _rows[0].Status.Trim();
                    sumRow.LocationName = _rows[0].LocationName.Trim();
                }
                #endregion
            }
            catch (Exception ex) { throw new FaultException<TrackingFault>(new TrackingFault(ex.Message),"Service Error"); }
            return trackDS;
        }
    }
}