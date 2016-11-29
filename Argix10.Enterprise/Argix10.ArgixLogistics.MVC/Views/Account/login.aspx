<%@ Page Title="Argix" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.LoginModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
<script type="text/javascript">
    $(document).ready(function () {
        var txtBox = document.getElementById("UserName");
        if (txtBox != null) txtBox.focus();
    });
</script>
<% using(Html.BeginForm()) { %>
<div class="header-blank"></div>
<div class="content">
    <div id="title">Login</div>
    <div class="form">
        <table style="width:500px" >
            <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td style="width:250px">&nbsp;</td></tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td style="text-align:right"><%: Html.LabelFor(m => m.UserName)%>&nbsp;</td>
                <td><%: Html.TextBoxFor(m => m.UserName) %><%: Html.ValidationMessageFor(m => m.UserName) %></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td style="text-align:right"><%: Html.LabelFor(m => m.Password)%>&nbsp;</td>
                <td><%: Html.PasswordFor(m => m.Password)%><%: Html.ValidationMessageFor(m => m.Password)%></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
 	        <tr>
                <td>&nbsp;</td>
	            <td style="height: 19px; text-align:right"><input type="submit" value="Submit" /></td>
	        </tr>
            <tr style="font-size:1px; height:12px"><td colspan="2">&nbsp;</td></tr>
            <tr style="font-size:1px; height:12px"><td colspan="2"><%: Html.ValidationSummary(true, "") %></td></tr>
            <tr style="font-size:1px; height:12px"><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td colspan="2">&nbsp;<a href="<%: Url.Action("Register", "Account")%>">New user? Click here to register...</a></td>
                <td>&nbsp;</td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td colspan="2">&nbsp;<a href="<%: Url.Action("RecoverPassword", "Account")%>">Forgot your password?</a></td>
            </tr>
            <tr style="font-size:1px; height:24px"><td colspan="2">&nbsp;</td></tr>
        </table>
    </div>
</div>
<% } %>
</asp:Content>
