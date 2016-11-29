using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _LoadTender : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        Response.Clear();
        Response.Buffer = true;
        Response.BufferOutput = true;
        Response.Write("<html><head runat='server'><title>Load Tenders</title>");
        Response.Write("<style> body { font-family:Calibri; font-size:10pt; color:#000000; background-color:#ffffff; } HR { page-break-after: always; } table { font-size:10pt; } tr { font-size:10pt; } td { font-size:10pt; }</style>");
        Response.Write("</head><body>");

        LoadTenderDataset ds = (LoadTenderDataset)Session["LoadTenders"];
        for(int i=0;i<ds.LoadTenderTable.Rows.Count;i++) {
            Response.Write("<table style=\"width:600px\">");
            Response.Write("<tr><td>Loc #" + ds.LoadTenderTable[i].LocationNumber.ToString() + "</td><td>" + ds.LoadTenderTable[i].Location.Trim() + "</td></tr>");
            Response.Write("<tr><td>ID #" + ds.LoadTenderTable[i].LocationID.Trim() + "</td><td>" + ds.LoadTenderTable[i].AddressLine1.Trim() + "</td></tr>");
            Response.Write("<tr><td>&nbsp;</td><td>" + ds.LoadTenderTable[i].AddressLine2.Trim() + "</td></tr>");
            Response.Write("<tr><td>Ref #" + ds.LoadTenderTable[i].ReferenceNumber.Trim() + "</td><td>" + ds.LoadTenderTable[i].City.Trim() + ", " + ds.LoadTenderTable[i].StateOrProvince.Trim() + " " + ds.LoadTenderTable[i].PostalCode.Trim() + "</td></tr>");
            Response.Write("</table><br />");

            if (!ds.LoadTenderTable[i].IsBarcode1Null() && ds.LoadTenderTable[i].Barcode1.Trim().Length > 0) {
                Response.Write("<table style=\"width:600px\">");
                Response.Write("<tr><td align=\"center\"><img src=\"http://192.168.151.65/argix10/argix10.enterprise.services/barcodeimage.aspx?barcode=" + ds.LoadTenderTable[i].Barcode1.Trim() + "&fontsize=24\" style=\"height:75px\" /></td></tr>");
                Response.Write("<tr><td align=\"center\">" + ds.LoadTenderTable[i].Barcode1.Trim() + "</td></tr>");
                Response.Write("</table><br />");
            }

            if (!ds.LoadTenderTable[i].IsBarcode2Null() && ds.LoadTenderTable[i].Barcode2.Trim().Length > 0) {
                Response.Write("<table style=\"width:600px\">");
                Response.Write("<tr><td align=\"center\"><img src=\"http://192.168.151.65/argix10/argix10.enterprise.services/barcodeimage.aspx?barcode=" + ds.LoadTenderTable[i].Barcode2.Trim() + "&fontsize=24\" style=\"height:75px\" /></td></tr>");
                Response.Write("<tr><td align=\"center\">" + ds.LoadTenderTable[i].Barcode2.Trim() + "</td></tr>");
                Response.Write("</table><br /><br /><br />");
            }

            LoadTenderDataset.LoadTenderDetailTableRow[] items = (LoadTenderDataset.LoadTenderDetailTableRow[])ds.LoadTenderDetailTable.Select("Load='" + ds.LoadTenderTable[i].Load + "'");
            if (items.Length > 0) {
                Response.Write("<table style=\"width:100%\"><tr><td>");
                Response.Write("<div style=\"float:left; width:400px; margin:0 0 20px 50px\"><table>");
                Response.Write("<tr><td align=\"right\"><b>Load#:</b></td><td><span>" + items[0].Load + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\"><b>Reference#:</b></td><td><span>" + items[0].ReferenceNumber + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\"><b>Location#:</b></td><td><span>" + items[0].LocationNumber + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\"><b>Location ID:</b></td><td><span>" + items[0].LocationID + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\"><b>PO#:</b></td><td><span>" + items[0].PONumber + "</span></td></tr>");
                Response.Write("</table></div>");
                Response.Write("<div style=\"float:left; width:400px; margin:0 0 20px 50px\"><table>");
                Response.Write("<tr><td align=\"right\"><b>Client:</b></td><td><span>" + items[0].Client + "</span>&nbsp;-&nbsp;<span>" + items[0].ClientName + "</span>, Store#&nbsp;<span>" + items[0].StoreNumber + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\"><b>Location:</b></td><td><span>" + items[0].Location + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\"><b>Address:</b></td><td><span>" + items[0].AddressLine1 + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\">&nbsp;</td><td><span>" + items[0].AddressLine2 + "</span></td></tr>");
                Response.Write("<tr><td align=\"right\">&nbsp;</td><td><span>" + items[0].City + "</span>,&nbsp;<span>" + items[0].StateOrProvince + "</span>&nbsp;<span>" + items[0].PostalCode + "</span></td></tr>");
                Response.Write("</table></div></td></tr>");
                Response.Write("<tr><td>");
                Response.Write("<div style=\"width:900px; margin:0 0 0px 0px\">");
                Response.Write("<table rules=\"all\" border=\"1\" style=\"background-color:White;border-width:1px;border-style:Inset;width:100%;border-collapse:collapse;\">");
                Response.Write("<tr style=\"color:buttontext;background-color:buttonface;border-color:window;border-width:0px;border-style:None;\"><th scope=\"col\" style=\"width:75px;\">Item#</th><th scope=\"col\" style=\"width:200px;\">Item Desc</th><th scope=\"col\" style=\"width:75px;\">Qty</th><th scope=\"col\" style=\"width:100px;\">Units</th><th scope=\"col\" style=\"width:150px;\">Sorted Item#</th><th scope=\"col\" style=\"width:150px;\">Sorted PO#</th><th scope=\"col\" style=\"width:75px;\">Sorted?</th></tr>");
                for (int j = 0;j < items.Length;j++) {
                    Response.Write("<tr style=\"color:#000000;background-color:#ffffff;border-width:0px;border-style:None;white-space:nowrap;\"><td>" + items[j].ItemNumber + "</td><td>" + items[j].ItemDescription + "</td><td>" + items[j].QuantityOrdered + "</td><td>" + items[j].UOM + "</td><td>" + items[j].SortedItemNumber + "</td><td>" + items[j].SortedPONumber + "</td><td>" + items[j].Sorted.ToString() + "</td></tr>");
                }
                Response.Write("</table></div></td></tr></table><br />");
            }
            if(i < ds.LoadTenderTable.Rows.Count - 1) Response.Write("<hr />");
        }
        Response.Write("</body></html>");
        Response.Flush();
    }
}
