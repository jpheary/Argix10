<%@ Page Title="Membership User" Language="C#" MasterPageFile="~/MasterPages/Profile.master" AutoEventWireup="true" CodeFile="MembershipUserPage.aspx.cs" Inherits="_MembershipUserPage" %>
<%@ MasterType VirtualPath="~/MasterPages/Profile.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#<%=btnUnlock.ClientID %>").button();
            $("#<%=btnSubmit.ClientID %>").button();
            $("#<%=btnClose.ClientID %>").button();
        });
    </script>
    <div class="admin">
        <div class="subtitle">Manage Membership User</div>
        <p>&nbsp;</p>
        <asp:ValidationSummary ID="vsUser" runat="server" Width="100%" DisplayMode="List" />
        <div>
            <div class="adminaccount">
                <fieldset>
                    <legend>Login Account</legend>
                    <br />
                    <label for="txtUserName">Username</label><asp:TextBox ID="txtUserName" runat="server" MaxLength="32" Width="250px" OnTextChanged="OnValidateForm" AutoPostBack="True" /><br />
                    <label for="txtEmail">Email</label><asp:TextBox ID="txtEmail" runat="server" MaxLength="100" Width="300px" AutoCompleteType="Email" AutoPostBack="True" OnTextChanged="OnValidateForm" /><br />
                    <label for="txtPassword">Password</label><asp:TextBox ID="txtPassword" runat="server" Width="125px" MaxLength="20" AutoPostBack="True" OnTextChanged="OnValidateForm" /><br />
                    <label for="txtComments">Comments</label><asp:TextBox ID="txtComments" runat="server" Width="300px" AutoPostBack="True" OnTextChanged="OnValidateForm" /><br />
                    <br />
                    <asp:CheckBox ID="chkApproved" runat="server" Text="Approved" AutoPostBack="True" OnCheckedChanged="OnApprovedChanged" /><br />
                    <asp:CheckBox ID="chkLockedOut" runat="server" Enabled="False" Text="Locked Out" AutoPostBack="True" /><br />
                </fieldset>
                <asp:RequiredFieldValidator ID="rfvUserID" runat="server" ControlToValidate="txtUserName" ErrorMessage="Please enter your UserID. It can be your email address." />
                <asp:RegularExpressionValidator ID="revUserID" runat="server" ControlToValidate="txtUserName" ErrorMessage="UserID must be at least 4 characters long and without spaces." ValidationExpression="^\w{4}[^\s]*$" />
                <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="Please enter your email address." ControlToValidate="txtEmail" />
                <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="Please enter a valid email address." ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" />
                <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Please choose a password at least 6 characters long." ControlToValidate="txtPassword" />
            </div>
            <div class="adminroles">
                <fieldset>
                    <legend>Role</legend>
                    <asp:RadioButtonList ID="optRole" runat="server" AutoPostBack="True" OnSelectedIndexChanged="OnRoleChanged">
                        <asp:ListItem Text="Guest" Value="guests" />
                        <asp:ListItem Text="Administrator" Value="administrators" />
                        <asp:ListItem Text="Member" Value="members" />
                        <asp:ListItem Text="Web Service" Value="wsmembers" />
                    </asp:RadioButtonList>
                </fieldset>
            </div>
        </div>
        <br />
        <div>
            <div class="adminprofile">
                <fieldset>
                    <legend>Profile</legend>
                    <label for="txtFullName">Full Name</label><asp:TextBox ID="txtFullName" runat="server" MaxLength="50" Width="250px" AutoPostBack="True" OnTextChanged="OnValidateForm" /><br />
                    <label for="txtCompany">Company</label><asp:TextBox ID="txtCompany" runat="server" MaxLength="100" Width="250px" Enabled="false" /><br />
                    <label for="cboType">Company Type</label><asp:DropDownList ID="cboType" runat="server" Width="100px" AutoPostBack="True" OnSelectedIndexChanged="OnTypeChanged"><asp:ListItem Text="Client" Value="Client" Selected="True" /><asp:ListItem Text="Vendor" Value="Vendor" /></asp:DropDownList><br />
                    <label for="cboCustomer">Customer</label><asp:DropDownList ID="cboCustomer" runat="server" Width="300px" DataSourceID="odsCustomer" DataTextField="CompanyName" DataValueField="ClientID" AutoPostBack="True" OnSelectedIndexChanged="OnCustomerChanged" /><br />
                    <label for="cboStoreSearchType">Store Search Type</label><asp:DropDownList ID="cboStoreSearchType" runat="server" Width="150px" AutoPostBack="True"><asp:ListItem Text="Argix Store Numbers" Value="Argix" Selected="True" /><asp:ListItem Text="Sub Store Numbers" Value="Sub" /></asp:DropDownList><br />
                    <label for="txtStoreNumber">Store Number</label><asp:TextBox ID="txtStoreNumber" runat="server" MaxLength="5" Width="75px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
                    <br />
                    <asp:CheckBox ID="chkPWReset" runat="server" Text="Password Reset" AutoPostBack="True" OnCheckedChanged="OnValidateForm" /><br />
                </fieldset>
                <asp:RequiredFieldValidator ID="rfvFullName" runat="server" ErrorMessage="Please enter your first and last name." ControlToValidate="txtFullName" Display="Static" InitialValue="" />
                <asp:ObjectDataSource ID="odsCustomer" runat="server" TypeName="Argix.Enterprise.TrackingGateway" SelectMethod="GetCustomers" >
                    <SelectParameters>
                        <asp:ControlParameter Name="companyType" ControlID="cboType" PropertyName="SelectedValue" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <div class="adminroles">
                <fieldset>
                    <legend>Supplemental Roles</legend>
                    <asp:CheckBoxList ID="chkRoles" runat="server">
                        <asp:ListItem Text="File Claims" Value="fileclaimsmember" />
                        <asp:ListItem Text="PO\PRO Search" Value="pomembers" />
                        <asp:ListItem Text="Reports" Value="rsmembers" />
                    </asp:CheckBoxList>
                </fieldset>
            </div>
        </div>
        <div class="clearleft"></div>
        <div class="services">
            <asp:Button ID="btnUnlock" runat="server" Text="Unlock" CausesValidation="False" CommandName="Unlock" OnCommand="OnCommand" />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CommandName="OK" OnCommand="OnCommand" />
            <asp:Button ID="btnClose" runat="server" Text="Close" CausesValidation="False" CommandName="Close" OnCommand="OnCommand" />
        </div>
    </div>
</asp:Content>

