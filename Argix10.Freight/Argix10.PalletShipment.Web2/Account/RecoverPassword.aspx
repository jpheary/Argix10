<%@ Page Title="Recover Password" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="RecoverPassword.aspx.cs" Inherits="_RecoverPassword" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=RecoverUserPassword.FindControl("UserNameContainerID").FindControl("SubmitButton").ClientID %>").button();
    });
</script>
<div class="account">
    <div class="subtitle">Recover Password</div>
    <p>Please enter your username to receive your password by email.</p>
    <asp:PasswordRecovery ID="RecoverUserPassword" runat="server" Width="100%"  OnSendingMail="OnSendingMail" OnSendMailError="OnSendMailError" OnVerifyingUser="OnVerifyingUser" OnUserLookupError="OnUserLookupError">
        <UserNameTemplate>
            <asp:ValidationSummary ID="vsRecover" runat="server" ValidationGroup="RecoverUserPassword" />
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <label for="UserName">Username</label><asp:TextBox ID="UserName" runat="server" Width="250px" /><br />
                    <div class="services">
                        <asp:Button ID="SubmitButton" runat="server" Text="Recover" ValidationGroup="RecoverUserPassword" CommandName="Submit" />
                    </div>
                    <br />
                </fieldset>
                <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName" ValidationGroup="RecoverUserPassword" ErrorMessage="Username is required." />
            </div>
        </UserNameTemplate>
    </asp:PasswordRecovery>
</div>
</asp:Content>
