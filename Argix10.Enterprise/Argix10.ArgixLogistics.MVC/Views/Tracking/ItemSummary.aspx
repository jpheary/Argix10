<%@ Page Title="Argix Logistics Carton Tracking" Language="C#" MasterPageFile="~/Views/Shared/TrackingSite.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ItemSummaryResponse>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpBody" runat="server">
    <a href="<%: Url.Action("Tracking", "Tracking") %>">Tracking...</a>
    <br /><br />
    <div class="form">
        <div class="title"><%= Model.Title %></div>
	    <table style="width:100%; color:#333333; border-collapse:collapse;">
		    <tr style="color:Black; background-color:#999999; font-weight:bold;">
			    <th scope="col" style="text-align:left">Tracking Number</th><th scope="col" style="text-align:left">Date/Time</th><th scope="col" style="text-align:left">Location</th><th scope="col" style="text-align:left">Status</th><th scope="col" style="text-align:left">CBOL</th>
		    </tr>
            <% foreach(Argix.Enterprise.TrackingItem item in Model.Items) {  %>
            <tr style="color:#333333; background-color:#ffffff;">
			    <td><a href="<%: Url.Action("ItemDetail", "Tracking", new {itemNumber = item.ItemNumber}) %>"><%=item.ItemNumber%></a></td><td><%=item.DateTime%></td><td><%=item.Location%></td><td><%=item.Status%></td><td><%=item.CBOL%></td>
		    </tr>
            <% } %>
	    </table>
	 </div>
</asp:Content>

