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

public partial class _LoadTenderDetail : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender,EventArgs e) {
        //Event handler for form load event
        Response.Write("<html>");
        Response.Write("<head runat='server'>");
        Response.Write("<title>Load Tender Detail</title>");
        Response.Write("<style> HR { page-break-after: always; } </style>");
        Response.Write("<link href=\"App_Themes/Argix/Argix.css\" rel=\"stylesheet\" type=\"text/css\" />");
        Response.Write("</head>");
        Response.Write("<body>");

        LoadTenderDetailDataset ds = (LoadTenderDetailDataset)Session["LoadTenderDetail"];
        int i=0;
        string load = "",nextLoad = "";
        while(i<ds.LoadTenderDetailTable.Rows.Count) {
            load = ds.LoadTenderDetailTable[i].Load;
            Response.Write("<table width=\"100%\" border=\"0\"><tr><td>");
            Response.Write("<div style=\"float:left; width:400px; margin:0 0 20px 50px\">");
            Response.Write("<table>");
            Response.Write("<tr><td align=\"right\"><b>Load#:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].Load + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\"><b>Reference#:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].ReferenceNumber + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\"><b>Location#:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].LocationNumber + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\"><b>Location ID:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].LocationID + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\"><b>PO#:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].PONumber + "</span></td></tr>");
            Response.Write("</table>");
            Response.Write("</div>");
            Response.Write("<div style=\"float:left; width:400px; margin:0 0 20px 50px\">");
            Response.Write("<table>");
            Response.Write("<tr><td align=\"right\"><b>Client:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].Client + "</span>&nbsp;-&nbsp;<span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].ClientName + "</span>, Store#&nbsp;<span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].StoreNumber + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\"><b>Location:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].Location + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\"><b>Address:</b></td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].AddressLine1 + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\">&nbsp;</td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].AddressLine2 + "</span></td></tr>");
            Response.Write("<tr><td align=\"right\">&nbsp;</td><td><span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].City + "</span>,&nbsp;<span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].StateOrProvince + "</span>&nbsp;<span style=\"color:Black;\">" + ds.LoadTenderDetailTable[i].PostalCode + "</span></td></tr>");
            Response.Write("</table>");
            Response.Write("</div>");
            Response.Write("</td>");
            Response.Write("</tr>");
            Response.Write("<tr>");
            Response.Write("<td>");
            Response.Write("<div style=\"width:400px; margin:0 0 0px 6px\">Load Tender Details</div>");
            Response.Write("<div style=\"width:900px; margin:0 0 0px 0px\">");
            Response.Write("<table cellspacing=\"0\" cellpadding=\"2\" rules=\"all\" border=\"1\" style=\"background-color:White;border-width:1px;border-style:Inset;font-size:0.9em;width:100%;border-collapse:collapse;\">");
            Response.Write("<tr style=\"color:buttontext;background-color:buttonface;border-color:window;border-width:0px;border-style:None;\"><th scope=\"col\" style=\"width:75px;\">Item#</th><th scope=\"col\" style=\"width:200px;\">Item Desc</th><th scope=\"col\" style=\"width:75px;\">Qty</th><th scope=\"col\" style=\"width:100px;\">Units</th><th scope=\"col\" style=\"width:150px;\">Sorted Item#</th><th scope=\"col\" style=\"width:150px;\">Sorted PO#</th><th scope=\"col\" style=\"width:75px;\">Sorted?</th></tr>");
            do {
                Response.Write("<tr style=\"color:Black;background-color:White;border-width:0px;border-style:None;white-space:nowrap;\"><td>" + ds.LoadTenderDetailTable[i].ItemNumber + "</td><td>" + ds.LoadTenderDetailTable[i].ItemDescription + "</td><td>" + ds.LoadTenderDetailTable[i].QuantityOrdered + "</td><td>" + ds.LoadTenderDetailTable[i].UOM + "</td><td>" + ds.LoadTenderDetailTable[i].SortedItemNumber + "</td><td>" + ds.LoadTenderDetailTable[i].SortedPONumber + "</td><td>" + ds.LoadTenderDetailTable[i].Sorted + "</td></tr>");
                i++;
                nextLoad = i < ds.LoadTenderDetailTable.Rows.Count ? ds.LoadTenderDetailTable[i].Load : "";
            } while (nextLoad == load);
            Response.Write("</table>");
            Response.Write("</div>");
            Response.Write("</td>");
            Response.Write("</tr>");
            Response.Write("</table>");
            Response.Write("<br />");
            Response.Write("<hr />");
        }
        Response.Write("</body>");
        Response.Write("</html>");
    }
}
