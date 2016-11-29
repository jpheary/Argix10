<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Change Password</div>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false">
        <ChangePasswordTemplate>
            <p>Use the form below to change your password. New passwords are required to be a minimum of 6 characters in length.</p>
            <asp:ValidationSummary runat="server" ID="vsChangeUserPassword" ValidationGroup="ChangeUserPassword" />
            <div>
                <fieldset style="width:400px">
                    <legend>Login Account Information</legend>
                    <table style="margin:25px 0px 0px 50px">
                        <tr><td class="labelx">Old Password</td></tr>
                        <tr><td><asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" /></td></tr>
                        <tr><td class="labelx">New Password</td></tr>
                        <tr><td><asp:TextBox ID="NewPassword" runat="server" TextMode="Password" /></td></tr>
                        <tr><td class="labelx">Confirm New Password</td></tr>
                        <tr><td><asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" /></td></tr>
                    </table>
                    <div style="margin:25px 0px 5px 175px">
                        <asp:Button ID="ChangePasswordPushButton" runat="server" Text="Change" CssClass="submit" CommandName="ChangePassword" ValidationGroup="ChangeUserPassword" />
                        &nbsp;<asp:Button ID="CancelPushButton" runat="server" Text="Cancel" CssClass="submit" CausesValidation="False" CommandName="Cancel" />
                    </div>
                    <br />
                    <asp:Literal ID="FailureText" runat="server" EnableViewState="False" />
                    <br />
                    <asp:RequiredFieldValidator ID="rfvCurrent" runat="server" ControlToValidate="CurrentPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="Password is required." />
                    <asp:RequiredFieldValidator ID="rfvNew" runat="server" ControlToValidate="NewPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="New password is required." />
                    <asp:RequiredFieldValidator ID="rfvConfirm" runat="server" ControlToValidate="ConfirmNewPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="Confirm new password is required." />
                    <asp:CompareValidator ID="rfvCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="The Confirm new password must match the new password entry." />
                </fieldset>
            </div>
        </ChangePasswordTemplate>
        <SuccessTemplate>
            <div style="margin:25px 0px 100px 0px">Your password was successfully changed.</div>
        </SuccessTemplate>
    </asp:ChangePassword>
</asp:Content>