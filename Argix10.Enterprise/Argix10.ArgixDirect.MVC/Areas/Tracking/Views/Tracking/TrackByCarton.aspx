<%@ Page Title="Track By Carton" Language="C#" MasterPageFile="~/Areas/Tracking/Views/Tracking/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Areas.Tracking.Models.TrackByCartonModel>" %>
<%@ MasterType VirtualPath="~/Areas/Tracking/Views/Tracking/Tracking.Master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="MetaContent">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cContent" ContentPlaceHolderID="PageContent" runat="server">
    <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
        <tr style="font-size:1px; height:32px"><td width="96px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td align="right"><%: Html.LabelFor(m => m.TrackBy) %>&nbsp;</td>
            <td><%: Html.DropDownList("TrackBy")%></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right" valign="top">Items&nbsp;</td>
            <td><asp:TextBox ID="txtNumbers" runat="server" Width="240px" MaxLength="400" Rows="11" TextMode="MultiLine"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvTracking" runat="server" ControlToValidate="txtNumbers" ErrorMessage="Please enter at least one tracking number.">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td>&nbsp;</td><td colspan="2">Track up to ten cartons at a time.<br />Enter one tracking# per line, or separate each with a comma.</td></tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="3" align="right"><asp:Button ID="btnTrack" runat="server" Width="72px" Height="24px" Text="Track" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" align="left"><asp:ValidationSummary ID="vsTracking" runat="server" /></td></tr>
    </table>
</asp:Content>

