<%@ Page Title="Log In" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%= LoginUser.FindControl("LoginButton").ClientID %>").button();
    });
</script>
<div class="account">
    <div class="subtitle">Log In</div>
    <p>Please enter your username and password.</p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" DestinationPageUrl="~/Client/Shipments.aspx" OnLoginError="OnLoginError" OnLoggedIn="OnLoggedIn" >
        <LayoutTemplate>
            <asp:ValidationSummary ID="vsLogin" runat="server" ValidationGroup="LoginUser" />
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <label for="UserName">Username</label><asp:TextBox ID="UserName" runat="server" Width="250px" /><br />
                    <label for="Password">Password</label><asp:TextBox ID="Password" runat="server" TextMode="Password" Width="150px" /><br />
                    <div class="services">
                        <asp:Button ID="LoginButton" runat="server" Text="Log In" UseSubmitBehavior="true" ValidationGroup="LoginUser" CommandName="Login" />
                    </div>
                    <br />
                </fieldset>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName" ValidationGroup="LoginUser" ErrorMessage="User Name is required." />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ValidationGroup="LoginUser" ErrorMessage="Password is required." />
            </div>
        </LayoutTemplate>
    </asp:Login>
    <br />
    <div>
        <asp:HyperLink ID="lnkForgotPassword" runat="server" EnableViewState="false" NavigateUrl="~/Account/RecoverPassword.aspx">Forgot Password...</asp:HyperLink>
    </div>
    <div>
        <asp:HyperLink ID="lnkEnroll" runat="server" EnableViewState="false" NavigateUrl="~/Enroll.aspx">Create a client account...</asp:HyperLink>
    </div>
</div>
</asp:Content>