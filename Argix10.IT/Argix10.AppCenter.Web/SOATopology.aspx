<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="SOATopology.aspx.cs" Inherits="_SOATopology" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="tabPage">
	<ul>
		<li id="liAgentLineHaul" runat="server"><asp:Button ID="tabAgentLineHaul" runat="server" Text="Agent/LineHaul" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="AgentLineHaul" /></li>
		<li id="liCustomers" runat="server"><asp:Button ID="tabCustomers" runat="server" Text="Customers" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="Customers" /></li>
		<li id="liEnterprise" runat="server"><asp:Button ID="tabEnterprise" runat="server" Text="Enterprise" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="Enterprise" /></li>
		<li id="liFinance" runat="server"><asp:Button ID="tabFinance" runat="server" Text="Finance" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="Finance" /></li>
		<li id="liFreight" runat="server"><asp:Button ID="tabFreight" runat="server" Text="Freight" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="Freight" /></li>
		<li id="liHR" runat="server"><asp:Button ID="tabHR" runat="server" Text="HR" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="HR" /></li>
		<li id="liTerminals" runat="server"><asp:Button ID="tabTerminals" runat="server" Text="Terminals" style="background-color:#ffffff" OnCommand="OnChangeView" CommandName="Terminals" /></li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwAgentLineHaul" runat="server">
    <div class="sectiontitle">Agent/LineHaul</div>
    <asp:Image ID="imgAgentLineHaul" runat="server" ImageUrl="~/App_Themes/Argix/Images/agentlinehaultopology.png" Width="700px" />
</asp:View>
<asp:View ID="vwCustomers" runat="server">
    <div class="sectiontitle">Customers</div>
    <asp:Image ID="imgCustomers" runat="server" ImageUrl="~/App_Themes/Argix/Images/customerstopology.png" Width="700px" />
</asp:View>
<asp:View ID="vwEnterprise" runat="server">
    <div class="sectiontitle">Enterprise</div>
    <asp:Image ID="imgEnterprise" runat="server" ImageUrl="~/App_Themes/Argix/Images/enterprisetopology.png" Width="700px" />
</asp:View>
<asp:View ID="vwFinance" runat="server">
    <div class="sectiontitle">Finance</div>
    <asp:Image ID="imgFinance" runat="server" ImageUrl="~/App_Themes/Argix/Images/financetopology.png" Width="700px" />
</asp:View>
<asp:View ID="vwFreight" runat="server">
    <div class="sectiontitle">Freight</div>
    <asp:Image ID="imgFreight" runat="server" ImageUrl="~/App_Themes/Argix/Images/freighttopology.png" Width="700px" />
</asp:View>
<asp:View ID="vwHR" runat="server">
    <div class="sectiontitle">HR</div>
    <asp:Image ID="imgHR" runat="server" ImageUrl="~/App_Themes/Argix/Images/hrtopology.png" Width="700px" />
</asp:View>
<asp:View ID="vwTerminals" runat="server">
    <div class="sectiontitle">Terminals</div>
    <asp:Image ID="imgTerminals" runat="server" ImageUrl="~/App_Themes/Argix/Images/terminalstopology.png" Width="700px" />
</asp:View>
</asp:MultiView>
<br />
</div>

</asp:Content>
