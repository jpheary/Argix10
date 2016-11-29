<%@ Page Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Areas.Mobile.Models.TrackingStoreDetail>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="trackingHeader">
        <a href="javascript:history.go(-1)"><< Back</a>
        <div class="trackingTitle"><%= Model.ClientName %> #<%= Model.Store %>,&nbsp;TL#&nbsp;<%= Model.TL %></div>
    </div>
    <div class="trackingBody">
        <% foreach (Argix.Areas.Mobile.Models.TrackingStoreCarton carton in Model.Cartons) {  %>
            <table style="width:100%; background-color:#ffffff">
                <tr><td style="width:50px; text-align:right">Ctn:&nbsp;</td><td style="width:100px; font-weight:bold"><%=carton.CartonNumber %></td><td style="text-align:right;"><%=carton.Weight %>lbs</td></tr>
                <tr><td style="text-align:right">Pickup:&nbsp;</td><td colspan="2"><%=carton.PickupDate.ToString("MM-dd-yyyy") %></td></tr>
                <tr><td style="text-align:right">Shipper:</td><td colspan="2"><%=carton.ShipperName %></td></tr>
                <tr><td style="text-align:right">Status:&nbsp;</td><td colspan="2">Ctn=&nbsp;<%=carton.CartonStatus %>;&nbsp;&nbsp;&nbsp;Scan=&nbsp;<%=carton.ScanStatus %></td></tr>
                <tr><td style="text-align:right">POD:&nbsp;</td><td colspan="2"><%=carton.POD %></td></tr>
                <tr><td colspan="3"><hr /></td></tr>
            </table>
        <% } %>
    </div>
</asp:Content>
