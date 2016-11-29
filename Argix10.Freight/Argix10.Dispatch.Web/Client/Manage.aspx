<%@ Page Title="Quick Quote" Language="C#" MasterPageFile="~/MasterPages/Client.master" AutoEventWireup="true" CodeFile="Manage.aspx.cs" Inherits="Manage" %>
<%@ MasterType VirtualPath="~//MasterPages/Client.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="tabPage">
	<ul>
		<li id="liClient" runat="server"><asp:ImageButton ID="tabClient" runat="server" ImageUrl="~/App_Themes/Argix/Images/account.png" OnCommand="OnChangeView" CommandName="Client" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwClient" runat="server">
    <div class="subtitle">Client Account #<asp:Label ID="lblID" runat="server" /></div>
    <asp:ValidationSummary ID="vsClient" runat="server" ValidationGroup="vgClient" />
    <asp:CustomValidator id="cvStatus" runat="server" ValidationGroup="vgClient" EnableClientScript="False" />
    <br />
    <br />
    <div>
        <asp:Button ID="btnAccountUpdate" runat="server" Text="Update" CssClass="submit" CommandName="AccountUpdate" OnCommand="OnManageCommand" />
    </div>
    <br />
</asp:View>
</asp:MultiView>
<br />
</div>
</asp:Content>
