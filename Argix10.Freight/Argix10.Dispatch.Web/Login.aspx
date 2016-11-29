<%@ Page Title="Log In" Language="C#" MasterPageFile="~/MasterPages/Client.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Account_Login" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Log In</div>
    <p>
        Please enter your username and password.
        <%-- <asp:HyperLink ID="lnkRegister" runat="server" EnableViewState="false">Register</asp:HyperLink> if you don't have an account. --%>    
    </p>
    <asp:Login ID="LoginUser" runat="server" EnableViewState="false" RenderOuterTable="false" DestinationPageUrl="~/PickupRequest.aspx" >
        <LayoutTemplate>
            <asp:ValidationSummary ID="vsLogin" runat="server" ValidationGroup="vgLogin" />
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <table>
                        <tr><td class="labelx">Username</td></tr>
                        <tr><td><asp:TextBox ID="UserName" runat="server" Width="125px" /></td></tr>
                        <tr><td class="labelx">Password</td></tr>
                        <tr><td><asp:TextBox ID="Password" runat="server" TextMode="Password" Width="125px" /></td></tr>
                    </table>
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName" ValidationGroup="vgLogin" ErrorMessage="User Name is required." />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="Password" ValidationGroup="vgLogin" ErrorMessage="Password is required." />
                </fieldset>
                <br />
                <div>
                    <asp:Button ID="LoginButton" runat="server" Text="Log In" ValidationGroup="vgLogin" CommandName="Login" CssClass="submit" />
                </div>
            </div>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>