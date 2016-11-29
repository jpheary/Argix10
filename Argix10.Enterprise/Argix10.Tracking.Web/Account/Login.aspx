<%@ Page Title="Client Login" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="_Login" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= LoginUser.FindControl("LoginButton").ClientID %>").button();
    });
</script>
<div class="account">
    <div class="subtitle">Log In</div>
    <p>Please enter your username and password.</p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" OnLoginError="OnLoginError" OnLoggedIn="OnLoggedIn" >
        <LayoutTemplate>
            <asp:ValidationSummary ID="vsLogin" runat="server" ValidationGroup="vgLogin" />
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <br />
                    <label for="UserName">Username</label><asp:TextBox ID="UserName" runat="server" Width="200px" /><br />
                    <label for="Password">Password</label><asp:TextBox ID="Password" runat="server" TextMode="Password" Width="125px" /><br />
                    <div class="services">
                        <asp:Button ID="LoginButton" runat="server" Text="Log In" UseSubmitBehavior="true" ValidationGroup="vgLogin" CommandName="Login" />
                    </div>
                </fieldset>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName" ValidationGroup="vgLogin" ErrorMessage="User Name is required." />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ValidationGroup="vgLogin" ErrorMessage="Password is required." />
            </div>
        </LayoutTemplate>
    </asp:Login>
    <br /><br />
    <div>
        <asp:HyperLink ID="lnkForgotPassword" runat="server" EnableViewState="false" ToolTip="Click here to request an email with your current password." NavigateUrl="~/Account/RecoverPassword.aspx">Forgot Password...</asp:HyperLink>
    </div>
    <div>
        <asp:HyperLink ID="lnkRegister" runat="server" EnableViewState="false" ToolTip="Click herre to register for a Tracking account." NavigateUrl="~/Account/Register.aspx">Register...</asp:HyperLink>
    </div>
</div>
</asp:Content>
