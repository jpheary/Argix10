<%@ Page Title="Change Password" Language="C#" MasterPageFile="~/MasterPages/Profile.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Account_ChangePassword" %>
<%@ MasterType VirtualPath="~/MasterPages/Profile.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=ChangeUserPassword.FindControl("ChangePasswordContainerID").FindControl("ChangeButton").ClientID %>").button();
        $("#<%=ChangeUserPassword.FindControl("ChangePasswordContainerID").FindControl("CancelButton").ClientID %>").button();
    });
</script>
<div class="account">
    <div class="subtitle">Change Password</div>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" OnChangePasswordError="OnChangePasswordError">
        <ChangePasswordTemplate>
            <p>Use the form below to change your password. New passwords are required to be a minimum of 6 characters in length.</p>
            <asp:ValidationSummary runat="server" ID="vsChangeUserPassword" ValidationGroup="ChangeUserPassword" />
            <div>
                <fieldset>
                    <legend>Account Information</legend>
                    <br />
                    <label for="CurrentPassword">Old Password</label><asp:TextBox ID="CurrentPassword" runat="server" TextMode="Password" /><br />
                    <label for="NewPassword">New Password</label><asp:TextBox ID="NewPassword" runat="server" TextMode="Password" /><br />
                    <label for="ConfirmNewPassword">Confirm New Password</label><td><asp:TextBox ID="ConfirmNewPassword" runat="server" TextMode="Password" /><br />
                    <div class="services">
                        <asp:Button ID="ChangeButton" runat="server" Text="Change" CommandName="ChangePassword" ValidationGroup="ChangeUserPassword" />
                        <asp:Button ID="CancelButton" runat="server" Text="Cancel" CausesValidation="False" Visible="false" CommandName="Cancel" />
                    </div>
                </fieldset>
                <asp:RequiredFieldValidator ID="rfvCurrent" runat="server" ControlToValidate="CurrentPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="Password is required." />
                <asp:RequiredFieldValidator ID="rfvNew" runat="server" ControlToValidate="NewPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="New password is required." />
                <asp:RequiredFieldValidator ID="rfvConfirm" runat="server" ControlToValidate="ConfirmNewPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="Confirm new password is required." />
                <asp:CompareValidator ID="rfvCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" ValidationGroup="ChangeUserPassword" ErrorMessage="The Confirm new password must match the new password entry." />
            </div>
        </ChangePasswordTemplate>
        <SuccessTemplate>
            <div style="margin:25px 0px 100px 0px">Your password was successfully changed.</div>
        </SuccessTemplate>
    </asp:ChangePassword>
</div>
</asp:Content>