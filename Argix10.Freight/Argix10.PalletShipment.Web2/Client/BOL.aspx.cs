using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class BOL : System.Web.UI.Page {
    //Members

    //Interface
    protected void Page_Load(object sender, EventArgs e) {
        //Page load event handler
        string number = Request.QueryString["number"] != null ? Request.QueryString["number"] : "";
        if (number.Length > 0) {
            Response.Write("<html");
            Response.Write("<head runat='server'>");
            Response.Write("<title>Pallet Shipment Bill of Lading</title>");
            Response.Write("<style>");
            Response.Write("body { font-family:Calibri, sans-serif; font-size:10pt; color:#000000; background-color:#ffffff; text-align:left; }");
            Response.Write(".page { position:relative; width:800px; height:375px; margin:auto; text-align:left; background-color:#ffffff; }");
            Response.Write(".header { height:50px; }");
            Response.Write(".logo { float:left; display:inline; width:auto; margin:0px 5px 5px 5px; }");
            Response.Write(".title { float:right; display:inline; width:auto; margin:10px 0px 0px 0px; padding:0px 0px 0px 5px; color:#000000; font-family:Calibri; font-size:16pt; font-weight:400; text-align:right; }");
            Response.Write(".subtitle { float:right; margin:0px 0px 5px 0px; font-size:1.2em; text-align:right; }");
            Response.Write(".body { position:relative; width: 800px; padding: 0px 0px 0px 0px; }");
            Response.Write(".biglabel { padding:0px 0px 0px 0px; font-size: 12pt; font-weight: bold; text-align: right; }");
            Response.Write(".label { padding:0px 0px 0px 0px; font-size: 10pt; font-weight: bold; text-align: right; }");
            Response.Write(".labelC { padding:0px 0px 0px 0px; font-size: 10pt; font-weight: bold; text-align: center; }");
            Response.Write(".clear { clear:both;  }");
            Response.Write("HR { page-break-after:always; color:#ffffff; }");
            Response.Write("td { font-size:10pt; }");
            Response.Write("</style>");
            Response.Write("</head>");
            Response.Write("<body>");
                
            LTLPalletBOLDataset ds = new Argix.Freight.FreightGateway().ReadPalletBOLData(number);
            LTLPalletBOLDataset.LTLPalletBOLTableRow label0 = ds.LTLPalletBOLTable[0];
            Response.Write("<div class=\"page\">");
            Response.Write("<div class=\"header\">");
            Response.Write("<div class=\"logo\"><img src=\"../App_Themes/Argix/Images/argix-logo.gif\" alt=\"Argix logo\" style=\"height:35px; border: 0;\" /></div>");
            Response.Write("<div class=\"title\">PALLET SHIPMENT PROGRAM</div>");
            Response.Write("</div>");
            Response.Write("<div class=\"clear\"></div>");
            Response.Write("<div class=\"body\">");
            Response.Write("<div class=\"subtitle\">Bill of Lading</div>");
            Response.Write("<table style=\"width:800px; border:1px solid #c5c7c9\">");
            Response.Write("<tr style=\"height:1px; font-size:1px\"><td style=\"width:100px\">&nbsp;</td><td style=\"width:300px\">&nbsp;</td><td style=\"width:100px\">&nbsp;</td><td style=\"width:300px\">&nbsp;</td></tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"biglabel\">CLIENT:&nbsp;</td><td>" + label0.ClientNumber + "-" + label0.ClientDivision + " : " + label0.ClientName.Trim() + "</td>");
            Response.Write("<td class=\"label\">Shipment#&nbsp;</td><td>" + label0.ShipmentNumber + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>" + label0.ClientAddressLine1 + "</td>");
            Response.Write("<td class=\"label\">Ship Date&nbsp;</td><td>" + label0.ShipDate.ToString("MM/dd/yyyy") + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>" + label0.ClientCity + ", " + label0.ClientState + " " + label0.ClientZip + "</td>");
            Response.Write("<td class=\"label\">Ref#&nbsp;</td><td>" + label0.BLNumber + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>&nbsp;</td>");
            Response.Write("<td class=\"label\">Pickup#&nbsp;</td><td>" + label0.PickupID.ToString() + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\"><td class=\"label\">&nbsp;</td><td>&nbsp;</td><td class=\"label\">&nbsp;</td><td>&nbsp;</td></tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"biglabel\">SHIP FROM:&nbsp;</td><td>" + label0.VendorNumber + " : " + label0.VendorName + "</td>");
            Response.Write("<td class=\"biglabel\">SHIP TO:&nbsp;</td><td>" + label0.ConsigneeNumber + " : " + label0.ConsigneeName + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>" + label0.VendorAddressLine1.Trim() + "</td>");
            Response.Write("<td class=\"label\">&nbsp;</td><td>" + label0.ConsigneeAddressLine1.Trim() + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>" + label0.VendorCity + ", " + label0.VendorState + " " + label0.VendorZip + "</td>");
            Response.Write("<td class=\"label\">&nbsp;</td><td>" + label0.ConsigneeCity + ", " + label0.ConsigneeState + " " + label0.ConsigneeZip + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>&nbsp;</td>");
            Response.Write("<td class=\"label\">&nbsp;</td><td>Tele:&nbsp;" + label0.ConsigneePhone + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>Inside Pickup:&nbsp;" + (label0.InsidePickup.ToLower() == "true" ? "yes" : "no") + "</td>");
            Response.Write("<td class=\"label\">&nbsp;</td><td>Inside Delivery:&nbsp;" + (label0.InsideDelivery.ToLower() == "true" ? "yes" : "no") + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td>Lift Gate:&nbsp;" + (label0.LiftGateOrigin.ToLower() == "true" ? "yes" : "no") + "</td>");
            Response.Write("<td class=\"label\">&nbsp;</td><td>Lift Gate:&nbsp;" + (label0.LiftGateDestination.ToLower() == "true" ? "yes" : "no") + "</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            if(!label0.IsPickupAppointmentWindowTimeStartNull())
                Response.Write("<td class=\"label\">&nbsp;</td><td>Appointment:&nbsp;" + label0.PickupAppointmentWindowTimeStart.ToString("HH:mm") + " - " + label0.PickupAppointmentWindowTimeEnd.ToString("HH:mm") + "</td>");
            else
                Response.Write("<td class=\"label\">&nbsp;</td><td>Appointment:&nbsp;n/a</td>");
            if(!label0.IsDeliveryAppointmentWindowTimeStartNull())
                Response.Write("<td class=\"label\">&nbsp;</td><td>Appointment:&nbsp;" + label0.DeliveryAppointmentWindowTimeStart.ToString("HH:mm") + " - " + label0.DeliveryAppointmentWindowTimeEnd.ToString("HH:mm") + "</td>");
            else
                Response.Write("<td class=\"label\">&nbsp;</td><td>Appointment:&nbsp;n/a</td>");
            Response.Write("</tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"label\">&nbsp;</td><td colspan=\"3\"><i>** Please verify any accessorial requirements during pickup and delivery.</i></td>");
            Response.Write("</tr>");
            Response.Write("</table>");
            Response.Write("<table style=\"width:800px; border:1px solid #c5c7c9\">");
            Response.Write("<tr style=\"height:1px; font-size:1px\"><td style=\"width:175px\">&nbsp;</td><td style=\"width:150px\">&nbsp;</td><td style=\"width:150px\">&nbsp;</td><td style=\"width:150px\">&nbsp;</td><td style=\"width:75px\">&nbsp;</td><td style=\"width:100px\">&nbsp;</td></tr>");
            Response.Write("<tr style=\"height:25px;\">");
            Response.Write("<td class=\"labelC\">Tracking#</td>");
            Response.Write("<td class=\"labelC\">Item#</td>");
            Response.Write("<td class=\"labelC\">PO#</td>");
            Response.Write("<td class=\"labelC\">Reference#</td>");
            Response.Write("<td class=\"labelC\">Weight</td>");
            Response.Write("<td class=\"labelC\">Insured Value</td>");
            Response.Write("</tr>");
            for(int i = 0; i < ds.LTLPalletBOLTable.Rows.Count; i++) {
                LTLPalletBOLDataset.LTLPalletBOLTableRow label = ds.LTLPalletBOLTable[i];
                Response.Write("<tr style=\"height:25px;\">");
                Response.Write("<td style=\"text-align:right\">" + label.TrackingNumber + "</td>");
                Response.Write("<td style=\"text-align:right\">" + label.ItemNumber + "</td>");
                Response.Write("<td style=\"text-align:right\">" + label.PONumber + "</td>");
                Response.Write("<td style=\"text-align:right\">" + label.ReferenceNumber + "</td>");
                Response.Write("<td style=\"text-align:right\">" + label.ItemWeight.ToString() + "</td>");
                Response.Write("<td style=\"text-align:right\">$" + label.InsuranceValue.ToString() + "</td>");
                Response.Write("</tr>");
            }
            for(int i = ds.LTLPalletBOLTable.Rows.Count; i < 5; i++) {
                Response.Write("<tr style=\"height:25px;\"><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td><td>&nbsp;</td></tr>");
            }
            Response.Write("</table>");
            Response.Write("</div>");
            Response.Write("</div>");
            Response.Write("</body>");
            Response.Write("</html>");
        }
    }
}