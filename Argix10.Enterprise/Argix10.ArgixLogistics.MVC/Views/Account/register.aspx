<%@ Page Title="Argix" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.RegisterModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
<% using(Html.BeginForm()) { %>
    <div class="header-blank"></div>
    <div class="content">
    <div id="title">Register</div>
    <div class="form">
        <table width="100%" border="0px" cellpadding="0px" cellspacing="0px">
            <tr style="font-size:1px;"><td width="120px">&nbsp;</td><td width="384px">&nbsp;</td><td>&nbsp;</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td>&nbsp;</td><td colspan="2">Enter your first and last name, company name, and email.</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.FullName)%>&nbsp;</td>
                <td align="left"><%: Html.TextBoxFor(m => m.FullName)%><%: Html.ValidationMessageFor(m => m.FullName)%></td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.Company)%>&nbsp;</td>
                <td align="left"><%: Html.TextBoxFor(m => m.Company)%><%: Html.ValidationMessageFor(m => m.Company)%></td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.Email)%>&nbsp;</td>
                <td><%: Html.TextBoxFor(m => m.Email)%><%: Html.ValidationMessageFor(m => m.Email)%></td>
                <td>&nbsp;</td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr><td>&nbsp;</td><td colspan="2">Enter a User ID at least 4 characers long without spaces;<br /> and a password at least 6 characters long.</td></tr>
            <tr><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.UserID)%>&nbsp;</td>
                <td><%: Html.TextBoxFor(m => m.UserID)%><%: Html.ValidationMessageFor(m => m.UserID)%></td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.Password)%>&nbsp;</td>
                <td><%: Html.PasswordFor(m => m.Password)%><%: Html.ValidationMessageFor(m => m.Password)%></td>
                <td>&nbsp;</td>
            </tr>
            <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right" ><%: Html.LabelFor(m => m.ConfirmPassword)%>&nbsp;</td>
                <td><%: Html.PasswordFor(m => m.ConfirmPassword)%><%: Html.ValidationMessageFor(m => m.ConfirmPassword)%></td>
                <td>&nbsp;</td>
            </tr>
            <tr><td colspan="3">&nbsp;</td></tr>
	        <tr>
                <td>&nbsp;</td>
	            <td align="right" style="height: 19px"><input type="submit" value="Submit" /></td>
                <td>&nbsp;</td>
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
        alert("Thank you. Your registration request has been received. You will receive email confirmation when your account becomes active. If you dont receive confirmation in the next 2 business days then contact customer support at extranet.support@argixlogistics.com or 800-274-4582.");
        window.navigate('<%: Url.Action("Index", "Home")%>');
    </script> 
<% } %>
</asp:Content>
