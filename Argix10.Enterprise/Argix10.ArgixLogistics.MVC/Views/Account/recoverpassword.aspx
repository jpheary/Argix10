<%@ Page Title="Argix" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.RecoverPasswordModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
<% using(Html.BeginForm()) { %>
    <div class="header-blank"></div>
    <div class="content">
    <div id="title">Recover Password</div>
    <div class="form">
        <table border="0px" cellpadding="0px" cellspacing="0px">
            <tr style="font-size:1px"><td width="96px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td>&nbsp;</td><td colspan="2" align="center">Enter your User ID to receive a new password by email.</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.UserID)%>&nbsp;</td>
                <td colspan="2"><%: Html.TextBoxFor(m => m.UserID)%><%: Html.ValidationMessageFor(m => m.UserID)%></td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
	        <tr>
	            <td colspan="3" align="right" style="height: 19px"><input type="submit" value="Submit" /></td>
	        </tr>
            <tr style="font-size:1px; height:12px"><td colspan="3">&nbsp;</td></tr>
            <tr style="font-size:1px; height:24px"><td colspan="3"><%: Html.ValidationSummary(true, "") %></td></tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
        </table>
    </div>
    </div>
<% } %>
<% if(ViewBag.Popup) { %>
    <script type="text/javascript">
        alert("Your password has been reset. You will receive an email confirmation with a new password. If you dont receive confirmation in the next 2 business days then contact customer support at extranet.support@argixlogistics.com or 800-274-4582.");
        window.navigate('<%: Url.Action("Login", "Account")%>');
    </script> 
<% } %>
</asp:Content>
