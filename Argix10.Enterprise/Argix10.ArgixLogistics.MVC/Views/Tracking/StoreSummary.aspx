<%@ Page Title="Track By Store" Language="C#" MasterPageFile="~/Views/Shared/TrackingSite.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.StoreItemSummaryResponse>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<a href="<%: Url.Action("Tracking", "Tracking") %>">Track By Store...</a>
<br /><br />
<div class="form">
    <div class="title"><%= Model.Title %></div>
    <table style=" border:1px solid #000000; color:#333333; width:100%; border-collapse:collapse;">
		<tr style="color:Black; background-color:#999999; font-weight:bold;">
			<th scope="col">TL</th><th scope="col">Cartons</th><th scope="col">Weight (lbs)</th><th scope="col">CBOL</th><th scope="col">ETA or POD</th><th scope="col">Terminal</th>
        </tr>
        <%  bool on = true;
            foreach(Argix.Enterprise.TrackingStoreItem item in Model.Items) {
                if (on) {
        %>
        <tr style="color:#333333; background-color:#ffffff;">
        <%      }
                else {
        %>
        <tr style="color:#333333; background-color:#cccccc;">
        <%      }
        %>
            <td style="padding:2px;"><a href="<%: Url.Action("StoreDetail", "Tracking", new {tl=item.TL}) %>"><%=item.TL%></a></td><td style="padding:2px; text-align:right"><%=item.CartonCount%></td><td style="padding:2px; text-align:right"><%=item.Weight%></td><td style="padding:2px;"><%=item.CBOL%></td><td style="padding:2px;"><%=item.OFD1%></td><td style="padding:2px;"><%=item.Terminal%></td>
        </tr>
        <%  on = !on;
            } 
        %>
	</table>
</div>
<script type="text/javascript">
    $(document).ready(function () {
        //jQuery ready is quicker than onload
        $(".stripeMe tr").mouseover(function () { $(this).addClass("over"); }).mouseout(function () { $(this).removeClass("over"); });
        $(".stripeMe tr:even").addClass("alt");
    });
</script>
</asp:Content>

