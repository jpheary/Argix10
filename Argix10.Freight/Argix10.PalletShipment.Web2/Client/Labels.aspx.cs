using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Labels : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        string number = Request.QueryString["number"] != null ? Request.QueryString["number"] : "";
        if (number.Length > 0) {
            Response.Write("<html");
            Response.Write("<head runat='server'>");
            Response.Write("<title>Pallet Shipment Labels</title>");
            Response.Write("<style>");
            Response.Write("body { font-family:Calibri, sans-serif; font-size:10pt; color:#000000; background-color:#ffffff; text-align:left; }");
            Response.Write(".page { position:relative; width:700px; height:400px; margin:auto; text-align:left; background-color:#ffffff; }");
            Response.Write(".header { height:50px; }");
            Response.Write(".logo { float:left; display:inline; width:auto; margin:0px 5px 5px 5px; }");
            Response.Write(".title { float:right; display:inline; width:auto; margin:10px 0px 0px 0px; padding:0px 0px 0px 5px; color:#000000; font-family:Calibri; font-size:16pt; font-weight:400; text-align:right; }");
            Response.Write(".body { position:relative; width:700px; padding:0px 0px 0px 0px; }");
            Response.Write(".biglabel { padding:0px 0px 0px 0px; font-size:12pt; font-weight:bold; text-align:right; }");
            Response.Write(".bigtext { padding:0px 0px 0px 0px; font-size:12pt; font-weight:bold; text-align:left; }");
            Response.Write(".label { padding:0px 0px 0px 0px; font-size:10pt; font-weight:bold; text-align:right; }");
            Response.Write(".clear { clear:both;  }");
            Response.Write("HR { width:700px; page-break-after:always; color:#ffffff; }");
            Response.Write("td { font-size:10pt; }");
            Response.Write("</style>");
            Response.Write("</head>");
            Response.Write("<body>");
                
            LTLPalletLabelDataset ds = new Argix.Freight.FreightGateway().ReadPalletLabels(number);
            for (int i=0; i< ds.LTLPalletLabelTable.Rows.Count; i++) {
                LTLPalletLabelDataset.LTLPalletLabelTableRow label = ds.LTLPalletLabelTable[i];
                Response.Write("<div class=\"page\">");
                Response.Write("<div class=\"header\">");
                Response.Write("<div class=\"logo\"><img src=\"../App_Themes/Argix/Images/argix-logo.gif\" alt=\"Argix logo\" style=\"height:35px; border: 0;\" /></div>");
                Response.Write("<div class=\"title\">PALLET SHIPMENT PROGRAM</div>");
                Response.Write("</div>");
                Response.Write("<div class=\"clear\"></div>");
                Response.Write("<div class=\"body\">");
                Response.Write("<table style=\"width:700px; border:1px solid #c5c7c9\">");
                Response.Write("<tr style=\"height:1px; font-size:1px\"><td style=\"width:100px\">&nbsp;</td><td style=\"width:250px\">&nbsp;</td><td style=\"width:100px\">&nbsp;</td><td style=\"width:250px\">&nbsp;</td></tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"biglabel\">CLIENT:&nbsp;</td><td>" + label.ClientNumber + "-" + label.ClientDivision + " : " + label.ClientName.Trim() + "</td>");
                Response.Write("<td class=\"biglabel\">FROM:&nbsp;</td><td>" + label.VendorNumber + " : " + label.VendorName + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"label\">&nbsp;</td><td>&nbsp;</td>");
                Response.Write("<td class=\"label\">&nbsp;</td><td>" + label.VendorAddressLine1.Trim() + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"biglabel\">PALLET#&nbsp;</td><td class=\"bigtext\">" + label.LabelSequenceNumber.Trim().Substring(label.LabelSequenceNumber.Trim().Length - 6, 2) + "</td>");
                Response.Write("<td class=\"label\">&nbsp;</td><td>" + label.VendorCity + ", " + label.VendorState + " " + label.VendorZip + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\"><td class=\"label\">&nbsp;</td><td>&nbsp;</td><td class=\"label\">&nbsp;</td><td>&nbsp;</td></tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"label\">Shipment#:&nbsp;</td><td>" + label.ShipmentNumber + "</td>");
                Response.Write("<td class=\"biglabel\">TO:&nbsp;</td><td>" + label.ConsigneeNumber + " : " + label.ConsigneeName + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"label\">Weight:&nbsp;</td><td>" + label.Weight.ToString() + "Lbs" + "</td>");
                Response.Write("<td class=\"label\">&nbsp;</td><td>" + label.ConsigneeAddressLine1.Trim() + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"label\">Insured Value:&nbsp;</td><td>" + "$" + label.InsuranceAmount.ToString() + "</td>");
                Response.Write("<td class=\"label\">&nbsp;</td><td>" + label.ConsigneeCity + ", " + label.ConsigneeState + " " + label.ConsigneeZip + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td class=\"label\">Terminal:&nbsp;</td><td>" + label.TerminalCode + " - " + label.Terminal.Trim() + "</td>");
                Response.Write("<td class=\"label\">&nbsp;</td><td>" + "Zone " + label.Zone + "</td>");
                Response.Write("</tr>");
                Response.Write("<tr style=\"height:25px;\"><td class=\"label\">&nbsp;</td><td>&nbsp;</td><td class=\"label\">&nbsp;</td><td>&nbsp;</td></tr>");
                Response.Write("<tr style=\"height:75px;\"><td colspan=\"4\" style=\"text-align:center\"><img src=\"http://192.168.151.65/argix10/argix10.enterprise.services/barcodeimage.aspx?barcode=" + label.LabelSequenceNumber + "&fontsize=24\" style=\"height:50px\" /></td></tr>");
                Response.Write("<tr style=\"height:25px;\"><td colspan=\"4\" style=\"text-align:center; font-size:12pt\">" + label.LabelSequenceNumber + "</td></tr>");
                Response.Write("<tr style=\"height:25px;\"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
                Response.Write("</table>");
                Response.Write("</div>");
                Response.Write("</div><br />");
                if(i < ds.LTLPalletLabelTable.Rows.Count - 1) Response.Write("<hr />");
            }
            Response.Write("</body>");
            Response.Write("</html>");
        }
    }
}