<%@ Page Title="Track By Store" Language="C#" MasterPageFile="~/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.TrackByStoreRequest>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<div id="title">Track By Store</div>
<div class="form">
    <table>
        <tr style="font-size:1px; height:32px"><td style="width:96px">&nbsp;</td><td style="width:432px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.ClientID)%>&nbsp;</td>
            <td><%: Html.DropDownListFor(m => m.ClientID,new SelectList(Argix.Models.TrackByStoreRequest.Clients,"ClientID","CompanyName"))%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.StoreNumber)%>&nbsp;</td>
            <td><%: Html.TextBoxFor(m => m.StoreNumber)%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.By)%>&nbsp;</td>
            <td><%: Html.DropDownListFor(m => m.By,new SelectList(Argix.Models.TrackByStoreRequest.TrackByDateTypes,"Description","Description"))%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.FromDate)%>&nbsp;</td>
            <td><%: Html.TextBoxFor(m => m.FromDate, new { type="date" }) %></td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.ToDate)%>&nbsp;</td>
            <td><%: Html.TextBoxFor(m => m.ToDate,new { type = "date" })%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
 	    <tr><td colspan="3" style="height:19px; text-align:right"><input type="submit" value="Track" /></td></tr>
        <tr style="font-size:1px; height:24px"><td colspan="3">&nbsp;</td></tr>
    </table>
</div>
</asp:Content>
