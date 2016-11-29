<%@ Page Title="Membership User" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="LoginAccount.aspx.cs" Inherits="_LoginAccount" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=btnUnlock.ClientID %>").button();
        $("#<%=btnSubmit.ClientID %>").button();
        $("#<%=btnClose.ClientID %>").button();
    });
</script>
<div class="admin">
    <div class="subtitle">Login Account</div>
    <div style="float:left; width:400px; margin:25px 25px">
        <fieldset style="width:400px">
            <legend>Account</legend>
            <table>
                <tr style="font-size:1px; height:5px"><td style="width:100px">&nbsp;</td><td>&nbsp;</td></tr>
                <tr><td style="text-align:right">User Name&nbsp;</td><td><asp:TextBox ID="txtUserName" runat="server" Width="150px" ReadOnly="true" /></td></tr>
                <tr><td style="text-align:right">Email&nbsp;</td><td><asp:TextBox ID="txtEmail" runat="server" Width="200px" AutoPostBack="true" OnTextChanged="OnValidateForm" /></td></tr>
                <tr><td style="text-align:right">Password&nbsp;</td><td><asp:TextBox ID="txtPassword" runat="server" Width="125px" AutoPostBack="true" OnTextChanged="OnValidateForm" /></td></tr>
                <tr><td style="text-align:right">Comments&nbsp;</td><td><asp:TextBox ID="txtComments" runat="server" Width="250px" AutoPostBack="true" OnTextChanged="OnValidateForm" /></td></tr>
                <tr><td style="text-align:right">ClientID&nbsp;</td><td><asp:TextBox ID="txtClientID" runat="server" MaxLength="3" Width="50px" AutoPostBack="true" OnTextChanged="OnValidateForm" /></td></tr>
            </table>
            <br />
        </fieldset>
        <br />
        <fieldset style="width:400px">
            <legend>Status</legend>
            <br />
            <asp:CheckBox ID="chkApproved" runat="server" Width="250px" Text="     Approved" AutoPostBack="True" OnCheckedChanged="OnApprovedChanged" />
            <br />
            <asp:CheckBox ID="chkLockedOut" runat="server" Width="250px" Text="     Locked Out" Enabled="false" />
            <br /><br />
        </fieldset>
    </div>
    <div style="float:left; width:200px; margin:25px 25px">
        <fieldset style="width:200px">
            <legend>Roles</legend>
            <br />
            <asp:RadioButtonList ID="optRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnRoleChanged">
                <asp:ListItem Text="Guest" Value="guest" />
                <asp:ListItem Text="Administrator" Value="administrator" />
                <asp:ListItem Text="Sales Rep" Value="salesrep" />
                <asp:ListItem Text="Client" Value="client" />
            </asp:RadioButtonList>
            <br />
        </fieldset>
    </div>
    <div class="clear"></div>
    <div class="services">
        <asp:Button ID="btnUnlock" runat="server" Text="Unlock" CausesValidation="False" CommandName="Unlock" OnCommand="OnCommand" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" CommandName="OK" OnCommand="OnCommand" />
        <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" CommandName="Close" OnCommand="OnCommand" />
    </div>
</div>
</asp:Content>

