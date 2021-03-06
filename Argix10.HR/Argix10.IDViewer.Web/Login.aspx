<%@ Page Title="Login" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <table width="100%"border="0px" cellpadding="0px" cellspacing="0px" >
        <tr style="font-size:1px"><td width="192px">&nbsp;</td><td width="288px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">User ID&nbsp;</td>
            <td><asp:TextBox ID="txtUserID" runat="server" Width="192px" MaxLength="25"></asp:TextBox></td>
            <td><asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="Please enter a valid User ID." ControlToValidate="txtUserID" SetFocusOnError="True">*</asp:RequiredFieldValidator></td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td align="right">Password&nbsp;</td>
            <td><asp:TextBox ID="txtPassword" runat="server" Width="192px" TextMode="Password" MaxLength="20"></asp:TextBox></td>
            <td>&nbsp;</td>
        </tr>
        <tr><td colspan="3">&nbsp;</td></tr>
        <tr>
            <td colspan="2" align="right"><asp:Button ID="LoginButton" runat="server" Width="72" Height="24" Text="Sign In" OnClick="OnLogin" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr><td colspan="3"><asp:ValidationSummary ID="lvsLogin" runat="server" Height="100%" Width="100%" DisplayMode="SingleParagraph" /></td></tr>
    </table>
</asp:Content>