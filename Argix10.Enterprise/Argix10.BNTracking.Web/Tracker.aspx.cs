//	File:	Tracker.aspx.cs
//	Author:	J. Heary
//	Date:	02/04/09
//	Desc:	Data and routing tracker page.
//	Rev:	
//	---------------------------------------------------------------------------
using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Tracker :System.Web.UI.Page {
    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for load event
        try {
            if (!Page.IsPostBack) {
                //Check page access and query string
                string itemNumbers = Request.QueryString["ID"];
                if(itemNumbers != null) {
                    //Form number array from CSV and track
                    string[] numbers = itemNumbers.Split(new string[] { "," },StringSplitOptions.RemoveEmptyEntries);

                    //Valid lengths 22, 30: Trim all numbers to 22 length
                    for (int i = 0;i < numbers.Length;i++) {
                        if (numbers[i].Length > 22) numbers[i] = numbers[i].Substring(numbers[i].Length-22,22);
                    }
                    track(numbers);
                }
                else {
                    //Invalid access
                    reportError(new ApplicationException("No carton numbers for tracking."));
                }
            }
        }
        catch (Exception ex) { reportError(ex); }
    }
    private void track(string[] numbers) {
        //Track one or more cartons
        try {
            //Reset session state
            Session["TrackData"] = null;
            
            //Validate carton numbers
            for(int i=0; i<numbers.Length; i++) {
                numbers[i] = numbers[i].Replace("\n", "");
                numbers[i] = numbers[i].Replace("-", "");
            }

            //Get tracking details for all cartons and retain in Session state
            TrackingDataset trackDS = new TrackingDataset();
            trackDS.Merge(new EnterpriseGateway().TrackCartons(numbers,null,null));
            Session["TrackData"] = trackDS;

            //Redirect to appropriate UI
            if(trackDS.TrackingSummaryTable.Rows.Count == 0)
                reportError(new ApplicationException("Carton not found."));
            else if(trackDS.TrackingSummaryTable.Rows.Count == 1)
                Response.Redirect("~/Detail.aspx?ID=" + trackDS.TrackingSummaryTable[0].ItemNumber.Trim(),false);
            else
                Response.Redirect("~/Summary.aspx",false);
        }
        catch(Exception ex) { reportError(ex); }
    }
    #region Local Services: reportError()
    private void reportError(Exception ex) {
        //Report an error to the user
        Session["Error"] = ex;
        Response.Redirect("~/Error.aspx",false);
    }
    #endregion
}
