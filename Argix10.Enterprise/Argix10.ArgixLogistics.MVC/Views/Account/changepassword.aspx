<%@ Page Title="Argix" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ChangePasswordModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cMain" ContentPlaceHolderID="cpContent" runat="server">
<% using(Html.BeginForm()) { %>
    <div class="header-blank"></div>
    <div class="content">
    <div id="title">Change Password</div>
    <div class="form">
        <table id="tblPage" width="100%" border="0px" cellpadding="6px" cellspacing="6px" >
            <tr style="font-size:1px"><td width="144px">&nbsp;</td><td width="332px">&nbsp;</td><td>&nbsp;</td></tr>
            <tr><td colspan="3">>&nbsp;</td></tr>
            <tr style="font-size:1px; height:12px"><td colspan="3">&nbsp;</td></tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.UserID)%>&nbsp;</td>
                <td><%: Html.TextBoxFor(m => m.UserID)%></td>
                <td>><%: Html.ValidationMessageFor(m => m.UserID)%></td>
            </tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.OldPassword)%>&nbsp;</td>
                <td><%: Html.PasswordFor(m => m.OldPassword)%></td>
                <td><%: Html.ValidationMessageFor(m => m.OldPassword)%></td>
            </tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.NewPassword)%>&nbsp;</td>
                <td><%: Html.PasswordFor(m => m.NewPassword)%></td>
                <td>><%: Html.ValidationMessageFor(m => m.NewPassword)%></td>
            </tr>
            <tr>
                <td align="right"><%: Html.LabelFor(m => m.ConfirmPassword)%>&nbsp;</td>
                <td><%: Html.PasswordFor(m => m.ConfirmPassword)%></td>
                <td><%: Html.ValidationMessageFor(m => m.ConfirmPassword)%></td>
            </tr>
            <tr style="font-size:1px; height:12px"><td colspan="3">&nbsp;</td></tr>
 	        <tr>
                <td>&nbsp;</td>
	            <td align="right" style="height: 19px"><input type="submit" value="Submit" /></td>
                <td>&nbsp;</td>
	        </tr>
            <tr style="font-size:1px; height:24px"><td colspan="3">&nbsp;</td></tr>
        </table>
    </div>
    </div>
<% } %>
</asp:Content>
