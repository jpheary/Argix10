<%@ Page Title="Argix Logistics Carton Tracking" Language="C#" MasterPageFile="~/Views/Shared/TrackingSite.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ItemDetailResponse>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpBody" runat="server">
<a href="<%: Url.Action("Tracking", "Tracking") %>">Tracking...</a>
<a href="<%: Url.Action("ItemSummary", "Tracking") %>">Item Summary...</a>
<br /><br />
<div class="form">
	<table>
		<tr style="font-size:1px; height:12px"><td style="width:180px">&nbsp;</td><td style="width:180px">&nbsp;</td><td style="width:48px">&nbsp;</td><td style="width:180px">&nbsp;</td><td style="width:180px">&nbsp;</td></tr>
		<tr><td colspan="5" ><%= Model.Store %></td></tr>
        <tr style="font-size:1px; height:12px"><td colspan="5">&nbsp;</td></tr>
        <tr>
			<td style="text-align:right">Carton #&nbsp;</td>
			<td><%= Model.Item.CartonNumber %></td>
			<td>&nbsp;</td>
			<td style="text-align:right">Bill of Lading #&nbsp;</td>
			<td><%= Model.Item.BOLNumber %></td>
		</tr> 
		<tr>
			<td style="text-align:right">Client&nbsp;</td>
			<td><%= Model.Item.ClientName %></td>
			<td>&nbsp;</td>
            <td style="text-align:right">TL #&nbsp;</td>
			<td><%= Model.Item.TLNumber %></td>
		</tr> 
		<tr>
			<td style="text-align:right">DC/Vendor&nbsp;</td>
			<td><%= Model.Item.VendorName %></td>
			<td>&nbsp;</td>
			<td style="text-align:right">Label Sequence #&nbsp;</td>
			<td><%= Model.Item.LabelNumber %></td>
		</tr> 
		<tr>
			<td style="text-align:right">Pickup Date&nbsp;</td>
			<td><%= Model.Item.PickupDate %></td>
			<td>&nbsp;</td>
			<td style="text-align:right">Purchase Order #&nbsp;</td>
			<td><%= Model.Item.PONumber %></td>
		</tr> 
		<tr>
			<td style="text-align:right">Scheduled for Delivery&nbsp;</td>
			<td><%= Model.Item.ActualStoreDeliveryDate %></td>
			<td>&nbsp;</td>
			<td style="text-align:right">Weight&nbsp;</td>
			<td><%= Model.Item.Weight %></td>
		</tr> 
		<tr>
			<td style="text-align:right">Shipment #&nbsp;</td>
			<td><%= Model.Item.ShipmentNumber %></td>
			<td>&nbsp;</td>
			<td style="text-align:right">&nbsp;</td>
			<td>&nbsp;</td>
		</tr> 
	</table>
	<br /><br />
	<table class="detailShipmentTable">
        <tr>
			<td class="detailHeader" style="width:192px; height:16px">Date/Time</td>
			<td class="detailHeader" style="width:288px; height:16px">Status</td>
			<td class="detailHeader" style="height:16px">Location</td>
		</tr>
		<tr>
			<td class="detailCell"><%= Model.Item.SortFacilityArrivalDate %></td>
			<td class="detailCell"><%= Model.Item.SortFacilityArrivalStatus %></td>
			<td class="detailCell"><%= Model.Item.SortFacilityLocation %></td>
		</tr>
		<tr>
			<td class="detailCellAlt"><%= Model.Item.ActualDepartureDate %></td>
			<td class="detailCellAlt"><%= Model.Item.ActualDepartureStatus %></td>
			<td class="detailCellAlt"><%= Model.Item.ActualDepartureLocation %></td>
		</tr>
		<tr>
			<td class="detailCell"><%= Model.Item.ActualArrivalDate %></td>
			<td class="detailCell"><%= Model.Item.ActualArrivalStatus %></td>
			<td class="detailCell"><%= Model.Item.ActualArrivalLocation %></td>
		</tr>
		<tr>
			<td class="detailCellAlt"><%= Model.Item.ActualStoreDeliveryDate %></td>
			<td class="detailCellAlt"><%= Model.Item.ActualStoreDeliveryStatus %></td>
			<td class="detailCellAlt"><%= Model.Item.ActualStoreDeliveryLocation %></td>
		</tr>
		<tr>
			<td class="detailCell"><%= Model.Item.PODScanDate %></td>
			<td class="detailCell"><%= Model.Item.PODScanStatus %></td>
			<td class="detailCell"><%= Model.Item.PODScanLocation %></td>
		</tr>
	</table>
	<br /><br />
	<table>
		<tr style="font-size:1px;"><td style="width:96px">&nbsp;</td><td style="width:96px">&nbsp;</td><td style="width:240px"></td><td style="width:168px">&nbsp;</td><td style="width:168px">&nbsp;</td></tr>
		<tr>
			<td colspan="3">&nbsp;</td>
			<td style="text-align:right"><a href="" target="_blank">POD Request</a></td>
			<td style="text-align:right"><a href="" target="_blank">File Claim</a></td>
		</tr>
        <tr><td colspan="5" style="font-size:1px; height:12px">&nbsp;</td></tr>
	</table>
</div>
</asp:Content>

