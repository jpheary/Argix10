<%@ Page Title="Track By Contract" Language="C#" MasterPageFile="~/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.TrackByItemRequest>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <div id="title">Track By PO/PRO</div>
    <div class="form">
    <table>
        <tr style="font-size:1px; height:32px"><td style="width:96px">&nbsp;</td><td style="width:384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.Client)%>&nbsp;</td>
            <td><%: Html.DropDownListFor(m => m.Client,new SelectList(Argix.Models.TrackByItemRequest.Clients,"ClientID","CompanyName"))%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.TrackingNumbers)%>&nbsp;</td>
            <td><%: Html.TextBoxFor(m => m.TrackingNumbers,new { maxlength="30" })%></td>
            <td>&nbsp;<%: Html.ValidationMessageFor(m => m.TrackingNumbers)%></td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right"><%: Html.LabelFor(m => m.TrackBy)%>&nbsp;</td>
            <td><%: Html.DropDownListFor(m => m.TrackBy,new SelectList(Argix.Models.TrackByItemRequest.SearchByItemTypes,"Description","Description"))%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
	    <tr>
	        <td colspan="3" style="height:19px; text-align:right"><input type="submit" value="Track" /></td>
	    </tr>
        <tr style="font-size:1px; height:24px"><td colspan="3">&nbsp;</td></tr>
    </table>
    <%: Html.ValidationSummary(true, "") %>
    </div>
</asp:Content>
