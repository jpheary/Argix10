<%@ Page Title="Memberships" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="LoginAccounts.aspx.cs" Inherits="_LoginAccounts" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="subtitle">Login Accounts</div>
<br />
<div style="width:890px; height:275px; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
    <asp:GridView ID="grdAccounts" runat="server" Width="100%" Height="100%" DataSourceID="odsAccounts" DataKeyNames="UserName" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnAccountSelected" >
        <Columns>
            <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
            <asp:BoundField DataField="UserName" HeaderText="User" HeaderStyle-Width="150px" ItemStyle-Wrap="false" SortExpression="UserName" />
            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="Email" Visible="true" />
            <asp:BoundField DataField="ClientID" HeaderText="ClientID" HeaderStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="ClientID" />
            <asp:BoundField DataField="IsApproved" HeaderText="Approved" HeaderStyle-Width="75px" SortExpression="IsApproved" />
            <asp:BoundField DataField="IsLockedOut" HeaderText="Locked" HeaderStyle-Width="75px" SortExpression="IsLockedOut" />
            <asp:BoundField DataField="LastLoginDate" HeaderText="Logon" HeaderStyle-Width="100px" DataFormatString="{0:MMddyyyy}" SortExpression="LastLoginDate" HtmlEncode="False" />
        </Columns>
    </asp:GridView>
    <asp:ObjectDataSource ID="odsAccounts" runat="server" TypeName="MembershipServices" SelectMethod="GetMembers" />
</div>
<br />
<div>
    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" CommandName="Refresh" OnCommand="OnCommand" />
    <asp:Button ID="btnEdit" runat="server" Text="Edit" CssClass="submit" CommandName="Edit" OnCommand="OnCommand" />
</div>
<br />
</asp:Content>
