<%@ Page Title="Registration" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Register.aspx.cs" Inherits="_Register" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Register" />
    <meta name="description" content="Register"/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= btnRegister.ClientID %>").button();
    });
</script>
<div class="account">
    <div class="subtitle">Register</div>
    <p>Enter the information below to register for a login account.</p>
    <asp:ValidationSummary runat="server" ID="vsRegister" ValidationGroup="vgRegister" />
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <br />
            <label for="txtFullName">Full Name</label><asp:TextBox ID="txtFullName" runat="server" Width="250px" MaxLength="50" /><br />
            <label for="txtCompany">Company</label><asp:TextBox ID="txtCompany" runat="server" Width="250px" MaxLength="100" /><br />
            <label for="txtEmail">Email</label><asp:TextBox ID="txtEmail" runat="server" Width="300px" MaxLength="100" /><br />
            <br />
            <label for="txtUserID">Username</label><asp:TextBox ID="txtUserID" runat="server" Width="250px" MaxLength="25" /><br />
            <label for="txtPassword">Password</label><asp:TextBox ID="txtPassword" runat="server" Width="125px" MaxLength="20" TextMode="Password" EnableViewState="False" /><br />
            <label for="txtPasswordConfirm">Confirm Password</label><asp:TextBox ID="txtPasswordConfirm" runat="server" Width="125px" MaxLength="20" TextMode="Password" EnableViewState="False" /><br />
            <div class="services">
                <asp:Button ID="btnRegister" runat="server" Text="Register" ValidationGroup="vgRegister" OnClick="OnRegister" />
            </div>
        </fieldset>
        <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ControlToValidate="txtFullName" ValidationGroup="vgRegister" ErrorMessage="Please enter your first and last name." InitialValue="" />
        <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="txtCompany" ValidationGroup="vgRegister" ErrorMessage="Please enter company name." />
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="vgRegister" ErrorMessage="Please enter your email address." />
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="txtEmail" ValidationGroup="vgRegister" ErrorMessage="Please enter a valid email address." ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
        <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" ValidationGroup="vgRegister" ErrorMessage="Please enter your UserID. It can be your email address." />
        <asp:RegularExpressionValidator ID="revUserID" runat="server" ControlToValidate="txtUserID" ValidationGroup="vgRegister" ErrorMessage="UserID must be at least 4 characters long and without spaces." ValidationExpression="^\w{4}[^\s]*$" />
        <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="txtPassword" ValidationGroup="vgRegister" ErrorMessage="Please choose a password at least 6 characters long." />
        <asp:RequiredFieldValidator ID="rfvPasswordConfirm" runat="server" ControlToValidate="txtPasswordConfirm" ValidationGroup="vgRegister" ErrorMessage="Please retype the password." />
        <asp:CompareValidator ID="cvPasswordConfirm" runat="server" ControlToValidate="txtPasswordConfirm" ControlToCompare="txtPassword" ValidationGroup="vgRegister" ErrorMessage="Passwords don't match. Please retype password." SetFocusOnError="True" ValueToCompare="Text" />
    </div>
</div>
</asp:Content>

