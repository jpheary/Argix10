<%@ Page Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Areas.Mobile.Models.TrackingStoreSummary>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <div class="trackingHeader">
        <a href="javascript:history.go(-1)"><< Back</a>
        <div class="trackingTitle"><%= Model.ClientName %> #<%= Model.Store %></div>
    </div>
    <div class="trackingBody">
        <% foreach(Argix.Areas.Mobile.Models.TrackingStoreTL tl in Model.TLs) {  %>
        <table style="width:100%; background-color:#ffffff">
            <tr><td style="width:50px; text-align:right">TL:&nbsp;</td><td style="width:75px; font-weight:bold"><%=tl.TL %></td><td style="text-align:right;"><%=tl.CartonCount %>ctns,&nbsp;<%=tl.Weight %>lbs</td>
                <td rowspan="4" style="width:24px"><a href="<%: Url.Action("TrackingTLs", "Tracking", new {clientID = Model.ClientID, clientName = Model.ClientName, store = Model.Store, from = Model.From, to = Model.To, by = Model.By, tl = tl.TL}) %>"><img src="<%= Url.Content("~/Content/Images/select.gif") %>" alt="" style="border: 0;" /></a></td></tr>
            <tr><td style="text-align:right">CBOL:&nbsp;</td><td colspan="2"><%=tl.CBOL %></td></tr>
            <tr><td style="text-align:right"><%=tl.OFDorPODLabel %></td><td colspan="2"><%=tl.OFDorPODValue %></td></tr>
            <tr><td style="text-align:right">Agent:&nbsp;</td><td colspan="3"><%=tl.AgentName %></td></tr>
            <tr><td colspan="4"><hr /></td></tr>
        </table>
        <% } %>
    </div>
</asp:Content>
