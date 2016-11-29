<%@ Page Title="Client Login" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Tracking.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.LoginModel>" %>

<asp:Content ID="cContent" ContentPlaceHolderID="cpContent" runat="server">
    <% using (Html.BeginForm()) { %>
    <div class="trackingHeader"><div class="trackingTitle">Client Login</div></div>
    <div class="form">
        <table>
            <tr>
                <td style="text-align:right"><%: Html.LabelFor(m => m.UserName) %>&nbsp;</td>
                <td><%: Html.TextBoxFor(m => m.UserName,new { width="150px",maxlength="25" })%><%: Html.ValidationMessageFor(m => m.UserName)%></td>
            </tr>
            <tr style="font-size:1px; height:5px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td style="text-align:right"><%: Html.LabelFor(m => m.Password) %>&nbsp;</td>
                <td><%: Html.TextBoxFor(m => m.Password,new { type="password",width="150px",maxlength="20" })%><%: Html.ValidationMessageFor(m => m.Password)%></td>
            </tr>
            <tr style="font-size:1px; height:15px"><td colspan="2">&nbsp;</td></tr>
            <tr><td colspan="2" style="text-align:right; padding-right:5px"><input id="btnLogin" type="submit" value="Login" style="width:75px; height:25px" /></td></tr>
            <tr style="font-size:1px; height:15px"><td colspan="2">&nbsp;</td></tr>
        </table>
    </div>
    <% } %>
</asp:Content>
