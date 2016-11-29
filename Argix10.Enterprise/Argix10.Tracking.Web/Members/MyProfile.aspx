<%@ Page Title="My Profile" Language="C#" MasterPageFile="~/MasterPages/Profile.master" AutoEventWireup="true" CodeFile="MyProfile.aspx.cs" Inherits="_MyProfile" %>
<%@ MasterType VirtualPath="~/MasterPages/Profile.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= btnUpdate.ClientID %>").button();
    });
</script>
<div class="account">
    <div class="subtitle">My Profile</div>
    <p>&nbsp;</p>
    <div>
        <fieldset>
            <legend>Account Information</legend>
            <label for="txtUserName">Username</label><asp:TextBox ID="txtUserName" runat="server" MaxLength="32" Width="100px" Enabled="false" /><br />
            <label for="txtEmail">Email</label><asp:TextBox ID="txtEmail" runat="server" MaxLength="75" Width="200px" AutoCompleteType="Email" AutoPostBack="True" OnTextChanged="OnEmailChanged" /><br />
            <br />
            <label for="txtFullName">Fullname</label><asp:TextBox ID="txtFullName" runat="server" MaxLength="50" Width="200px" AutoPostBack="True" OnTextChanged="OnFullNameChanged" /><br />
            <label for="txtCompany">Company</label><asp:TextBox ID="txtCompany" runat="server" MaxLength="100" Width="200px" Enabled="false" /><br />
            <label for="txtStoreNumber">Store Number</label><asp:TextBox ID="txtStoreNumber" runat="server" MaxLength="5" Width="75px" Enabled="false" /><br />
            <div class="services">
                <asp:Button ID="btnUpdate" runat="server" Text="Update" UseSubmitBehavior="true" CommandName="Update" OnCommand="OnCommand" />
            </div>
        </fieldset>
    </div>
</div>
</asp:Content>

