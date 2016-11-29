<%@ Page Title="Login" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>
<%@ MasterType VirtualPath="~/Site.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cphMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cphBody">
    <script type="text/jscript">
        $(document).ready(function () {
            $("#<%=LoginButton.ClientID %>").button();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) { }
    </script>
    <div class="account">
        <div class="subtitle">Login</div>
        <asp:ValidationSummary ID="vsLogin" runat="server" />
        <div>
            <fieldset>
                <legend>Account</legend>
                <br />
                <label for="txtUserID">Username</label><asp:TextBox ID="txtUserID" runat="server" Width="150px" MaxLength="25" /><br />
                <label for="txtPassword">Password</label><asp:TextBox ID="txtPassword" runat="server" Width="100px" TextMode="Password" MaxLength="20" /><br />
                <div class="services">
                    <asp:Button ID="LoginButton" runat="server" Text="Sign In" OnClick="OnLogin" />
                </div>
            </fieldset>
        </div>
        <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserID" ErrorMessage="Please enter a valid User ID." SetFocusOnError="True" />
    </div>
</asp:Content>