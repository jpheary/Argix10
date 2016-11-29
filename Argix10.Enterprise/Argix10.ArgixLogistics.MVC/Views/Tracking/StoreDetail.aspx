<%@ Page Title="Track By Store" Language="C#" MasterPageFile="~/Views/Shared/TrackingSite.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.StoreItemDetailResponse>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<a href="<%: Url.Action("Tracking", "Tracking") %>">Track By Store...</a>
&nbsp;&nbsp;&nbsp;<a href="<%: Url.Action("StoreSummary", "Tracking") %>">Store Summary...</a>
<br /><br />
<div class="form">
    <div class="title"><%= Model.Store %></div>
    <table style="color:#333333; width:100%; border-collapse:collapse;">
		<tr style="color:Black;background-color:#999999;font-weight:bold;">
			<th scope="col">Carton Number</th><th scope="col">PU Date</th><th scope="col">Weight</th><th scope="col">Shipper</th><th scope="col">Address</th><th scope="col">Carton Status</th><th scope="col">Scan Status</th><th scope="col">Scan Date/Time</th><th scope="col">CBOL</th>
		</tr>
        <%  bool on = true;
            foreach(Argix.Enterprise.TrackingStoreItem item in Model.Items) {
                if (on) {
        %>
        <tr style="color:#333333;background-color:#ffffff;">
        <%      }
                else {
        %>
        <tr style="color:#333333;background-color:#cccccc;">
        <%      }
        %>
			<td><a href="<%: Url.Action("ItemDetailForStore", "Tracking", new {itemNumber=item.LabelNumber}) %>"><%=item.CartonNumber%></a></td><td><%=item.PickupDate%></td><td style="text-align:right"><%=item.Weight%></td><td style="white-space:nowrap;"><%=item.ShipperName%></td><td style="white-space:nowrap;"><span style="font-size:1em;position: relative"><%=item.ShipperCity%>,&nbsp;<%=item.ShipperState%>&nbsp<%=item.ShipperZip%></span></td><td><%=item.CartonStatus%></td><td><%=item.ScanStatus%></td><td style="white-space:nowrap;"><span style="font-size:1em;position: relative"><%=item.PODDate%>&nbsp;<%=item.PODTime%></span></td><td><%=item.CBOL%></td>
		</tr>
        <%  on = !on;
            } 
        %>
    </table>
</div>
 </asp:Content>


