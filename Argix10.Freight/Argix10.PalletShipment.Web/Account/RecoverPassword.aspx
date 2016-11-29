<%@ Page Title="Recover Password" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="RecoverPassword.aspx.cs" Inherits="_RecoverPassword" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Recover Password</div>
    <p>Please enter your username to receive your password by email.</p>
    <div>
    <asp:PasswordRecovery ID="ctlRecoverPW" runat="server" Width="100%"  OnSendingMail="OnSendingMail" OnSendMailError="OnSendMailError" OnVerifyingUser="OnVerifyingUser" OnUserLookupError="OnUserLookupError">
        <UserNameTemplate>
            <asp:ValidationSummary ID="vsRecover" runat="server" ValidationGroup="vgRecover" />
            <div>
                <fieldset style="width:400px">
                    <legend>Account Information</legend>
                    <table style="margin:25px 0px 0px 50px">
                        <tr><td class="labelx">Username</td></tr>
                        <tr><td><asp:TextBox ID="UserName" runat="server" Width="125px" /></td></tr>
                    </table>
                    <div style="margin:25px 0px 5px 200px">
                        <asp:Button ID="LoginButton" runat="server" Text="Recover" ValidationGroup="vgRecover" CommandName="Submit" CssClass="submit" />
                    </div>
                    <br />
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvUsername" runat="server" ControlToValidate="UserName" ValidationGroup="vgRecover" ErrorMessage="Username is required." />
                </fieldset>
            </div>
        </UserNameTemplate>
    </asp:PasswordRecovery>
    </div>
</asp:Content>
