<%@ Page Title="Client Login" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Employee Login</div>
    <asp:ValidationSummary ID="lvsLogin" runat="server" />
    <table>
        <tr style="font-size:1px; height:25px"><td style="width:100px">&nbsp;</td><td style="width:175px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right">User ID&nbsp;</td>
            <td><asp:TextBox ID="txtUserID" runat="server" Width="150px" MaxLength="25" /></td>
            <td style="text-align:left"></td>
        </tr>
        <tr style="font-size:1px; height:5px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right">Password&nbsp;</td>
            <td><asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" MaxLength="20" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:15px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="2"><asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="submit" OnClick="OnLogin" /></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="Please enter a valid User ID." SetFocusOnError="True" />
</asp:Content>
