<%@ Page Title="Argix Logistics Carton Tracking" Language="C#" MasterPageFile="~/Views/Shared/Profile.Master" Inherits="System.Web.Mvc.ViewPage<Argix.Models.ProfileModel>" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="" />
    <meta name="description" content=""/>
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" runat="server">
<% using(Html.BeginForm()) { %>
    <div id="title">My Profile</div>
    <div class="form">
        <table>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td style="text-align:right"><%: Html.LabelFor(m => m.UserName)%>&nbsp;</td>
                            <td><%: Html.TextBoxFor(m => m.UserName) %><%: Html.ValidationMessageFor(m => m.UserName) %></td>
                        </tr>
                        <tr>
                            <td style="text-align:right"><%: Html.LabelFor(m => m.Email)%>&nbsp;</td>
                            <td><%: Html.TextBoxFor(m => m.Email)%><%: Html.ValidationMessageFor(m => m.Email)%></td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td style="text-align:right"><%: Html.LabelFor(m => m.FullName)%>&nbsp;</td>
                            <td><%: Html.TextBoxFor(m => m.FullName)%><%: Html.ValidationMessageFor(m => m.FullName)%></td>
                        </tr>
                        <tr>
                            <td style="text-align:right"><%: Html.LabelFor(m => m.Company)%>&nbsp;</td>
                            <td><%: Html.TextBoxFor(m => m.Company)%><%: Html.ValidationMessageFor(m => m.Company)%></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr style="font-size:1px; height:6px"><td>&nbsp;</td></tr>
	        <tr><td style="height: 19px; text-align:right"><input type="submit" value="Update" /></td></tr>
            <tr style="font-size:1px; height:12px"><td colspan="2">&nbsp;</td></tr>
            <tr style="font-size:1px; height:12px"><td colspan="2"><%: Html.ValidationSummary(true, "") %></td></tr>
            <tr style="font-size:1px; height:12px"><td colspan="2">&nbsp;</td></tr>
       </table>
    </div   
<% } %>
</asp:Content>

