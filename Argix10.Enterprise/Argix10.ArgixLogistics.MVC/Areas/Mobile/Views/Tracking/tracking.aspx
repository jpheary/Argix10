<%@ Page Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Areas.Mobile.Models.TrackingModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
    <% using (Html.BeginForm()) { %>
    <div class="trackingHeader"><div class="trackingTitle">Tracking By Store</div></div>
    <div class="form">
        <table style="width:100%;">
            <tr><td style="text-align:right">Client&nbsp;</td><td><%: Html.DropDownListFor(m => m.ClientID,new SelectList(Argix.Areas.Mobile.Models.TrackingModel.Clients,"ClientID","CompanyName"),new object{})%></td></tr>
            <tr><td style="text-align:right">Store&nbsp;</td><td><%: Html.TextBoxFor(m => m.Store) %></td></tr>
            <tr><td style="text-align:right">From&nbsp;</td><td><%: Html.TextBoxFor(m => m.From, new { type="date" }) %></td></tr>
            <tr><td style="text-align:right">To&nbsp;</td><td><%: Html.TextBoxFor(m => m.To,new { type = "date" })%></td></tr>
            <tr><td style="text-align:right">By&nbsp;</td><td><%: Html.DropDownListFor(m => m.By,new SelectList(Argix.Areas.Mobile.Models.TrackingModel.Bys,"Description","Description"))%></td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr><td style="text-align:right">&nbsp;</td><td style="text-align:right; padding-right:75px"><input id="btnTrack" type="submit" value="Track" /></td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
        </table>
    </div>
    <% } %>
</asp:Content>
