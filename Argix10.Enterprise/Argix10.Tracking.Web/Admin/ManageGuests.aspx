<%@ Page Language="C#" MasterPageFile="~/MasterPages/Profile.master" AutoEventWireup="true" CodeFile="ManageGuests.aspx.cs" Inherits="ManageGuests" Title="Manage Guests" %>
<%@ MasterType VirtualPath="~/MasterPages/Profile.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnReject.ClientID %>").button();
            $("#<%=btnApprove.ClientID %>").button();
            $("#<%=btnClose.ClientID %>").button();
        });
    </script>
    <div class="admin">
        <div class="subtitle">Manage Registered Users (Guests)</div>
        <p>&nbsp;</p>
        <asp:ValidationSummary ID="vsUser" runat="server" Width="100%" DisplayMode="List" />
        <div>
            <fieldset>
                <legend>Account Information</legend>
                <br />
                <label for="cboGuest">Guest</label><asp:DropDownList ID="cboGuest" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="OnGuestChanged" /><br />
                <br />
                <label for="txtUserID">Username</label><asp:Label ID="txtUserID" runat="server" Width="200px" /><br />
                <label for="txtUserName">Fullname</label><asp:TextBox ID="txtUserName" runat="server" Width="250px" MaxLength="50" EnableViewState="False" /><br />
                <label for="txtEmail">Email</label><asp:TextBox ID="txtEmail" runat="server" Width="300px" MaxLength="100" EnableViewState="False" /><br />
                <label for="txtCompany">Company</label><asp:TextBox ID="txtCompany" runat="server" Width="300px" MaxLength="100" EnableViewState="False" /><br />
                <br />
                <label for="cboType">Type</label><asp:DropDownList ID="cboType" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="OnCompanyTypeChanged"><asp:ListItem Text="Client" Value="Client" Selected="True" /><asp:ListItem Text="Vendor" Value="Vendor" /></asp:DropDownList><br />
                <label for="cboCustomer">Company</label><asp:DropDownList ID="cboCustomer" runat="server" Width="300px" DataSourceID="odsCustomers" DataTextField="CompanyName" DataValueField="ClientID" /><br />
                <label for="txtComments">Comments</label><asp:TextBox ID="txtComments" runat="server" Rows="3" TextMode="MultiLine" Width="300px" ToolTip="Use comments to give reason to user if you are rejecting registeration." EnableViewState="False" /><br />
                <div class="services">
                    <asp:Button ID="btnReject" runat="server" Text="Reject" ToolTip="User record will be removed permanently." CommandName="Reject" OnCommand="OnCommand" />
                    <asp:Button ID="btnApprove" runat="server" Text="Approve" CommandName="Approve" OnCommand="OnCommand" />
                    <asp:Button ID="btnClose" runat="server" Text="Close" CommandName="Close" OnCommand="OnCommand" />
                </div>
           </fieldset>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsGuests" runat="server" TypeName="Roles" SelectMethod="GetUsersInRole" >
        <SelectParameters>
            <asp:Parameter Name="roleName" DefaultValue="Guests" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomers" runat="server" TypeName="Argix.Enterprise.TrackingGateway" SelectMethod="GetCustomers" >
        <SelectParameters>
            <asp:ControlParameter Name="companyType" ControlID="cboType" PropertyName="SelectedValue" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

