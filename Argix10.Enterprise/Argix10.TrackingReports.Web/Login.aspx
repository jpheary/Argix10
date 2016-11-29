<%@ Page Title="Client Login" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Login" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cSetup" runat="Server" ContentPlaceHolderID="Setup">
    <div class="form" style="width:400px">
    <table style="width:400px" >
        <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td style="width:300px">&nbsp;</td></tr>
        <tr><td colspan="2" style="font-size:1px; height:15px">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right">User ID&nbsp;</td>
            <td><asp:TextBox ID="txtUserID" runat="server" Width="200px" MaxLength="25" /></td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:5px">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right">Password&nbsp;</td>
            <td><asp:TextBox ID="txtPassword" runat="server" Width="200px" TextMode="Password" MaxLength="20" /></td>
        </tr>
        <tr><td colspan="2" style="font-size:1px; height:15px">&nbsp;</td></tr>
 	    <tr>
	        <td colspan="2" style="height:20px; text-align:right">
                <asp:LinkButton ID="btnLogin" runat="server" Text="Login" OnClick="OnLogin" />
                &nbsp;<asp:ImageButton ID="imgLogin" runat="server" OnClick="OnLoginImg"  ImageUrl="~/App_Themes/Argix/Images/redarrow.gif" ImageAlign="AbsBottom" />&nbsp;&nbsp;
		    </td>
	    </tr>
        <tr><td colspan="2" style="font-size:1px; height:5px">&nbsp;</td></tr>
        <tr><td colspan="2"><asp:ValidationSummary ID="lvsLogin" runat="server" /></td></tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ErrorMessage="Please enter a valid User ID." ControlToValidate="txtUserID" SetFocusOnError="True" />
    </div>
</asp:Content>
