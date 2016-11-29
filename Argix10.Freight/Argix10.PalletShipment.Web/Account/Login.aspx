<%@ Page Title="Log In" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Log In</div>
    <p>Please enter your username and password.</p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" DestinationPageUrl="~/Client/Shipments.aspx" OnLoggedIn="OnLoggedIn" >
        <LayoutTemplate>
            <asp:ValidationSummary ID="vsLogin" runat="server" ValidationGroup="vgLogin" />
            <div>
                <fieldset style="width:400px">
                    <legend>Account Information</legend>
                    <table style="margin:25px 0px 0px 50px">
                        <tr><td class="labelx">Username</td></tr>
                        <tr><td><asp:TextBox ID="UserName" runat="server" Width="125px" /></td></tr>
                        <tr><td class="labelx">Password</td></tr>
                        <tr><td><asp:TextBox ID="Password" runat="server" TextMode="Password" Width="125px" /></td></tr>
                    </table>
                    <div style="margin:25px 0px 5px 200px">
                        <asp:Button ID="LoginButton" runat="server" Text="Log In" ValidationGroup="vgLogin" CommandName="Login" CssClass="submit" />
                    </div>
                    <br />
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName" ValidationGroup="vgLogin" ErrorMessage="User Name is required." />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ValidationGroup="vgLogin" ErrorMessage="Password is required." />
                </fieldset>
            </div>
        </LayoutTemplate>
    </asp:Login>
    <br />
    <div>
        <asp:HyperLink ID="lnkForgotPassword" runat="server" EnableViewState="false" NavigateUrl="~/Account/RecoverPassword.aspx">Forgot Password?</asp:HyperLink>
    </div>
    <div>
        <asp:HyperLink ID="lnkEnroll" runat="server" EnableViewState="false" NavigateUrl="~/Enroll.aspx">Create a client account</asp:HyperLink>
    </div>
</asp:Content>