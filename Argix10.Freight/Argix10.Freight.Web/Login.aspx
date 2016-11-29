<%@ Page Title="Client Login" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    Employee Login
    <table style="width:100%">
        <tr style="font-size:1px; height:25px"><td style="width:100px">&nbsp;</td><td style="width:175px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right">User ID&nbsp;</td>
            <td><asp:TextBox ID="txtUserID" runat="server" Width="150px" MaxLength="25" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:5px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right">Password&nbsp;</td>
            <td><asp:TextBox ID="txtPassword" runat="server" Width="150px" TextMode="Password" MaxLength="20" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:15px"><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="2" style="text-align:right; padding-right:25px">
                <asp:Button ID="btnLogin" runat="server" Text="Sign In" CssClass="submit" OnCommand="OnOnCommand" CommandName="Login" />
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr style="font-size:1px; height:15px"><td colspan="3">&nbsp;</td></tr>
        <tr><td colspan="3" style="padding-left:25px"><asp:ValidationSummary ID="vsLogin" runat="server" ValidationGroup="vgLogin" /></td></tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="Please enter a valid User ID." SetFocusOnError="True" ValidationGroup="vgLogin" />
</asp:Content>
