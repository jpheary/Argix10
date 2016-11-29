<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="TLView.aspx.cs" Inherits="TLView" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div>
    Terminal&nbsp;
    <asp:DropDownList ID="cboTerminal" runat="server" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" AutoPostBack="True" ToolTip="Local Terminals" style="width:150px" OnSelectedIndexChanged="OnTerminalChanged" />
    <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetTerminals" />
    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnClick="OnRefresh" />
</div>
<div class="tabPage">
	<ul>
		<li id="liTLs" runat="server"><asp:ImageButton ID="tabTLs" runat="server" ImageUrl="~/App_Themes/Argix/Images/tlview.png" OnCommand="OnChangeView" CommandName="TLs" /></li>
		<li id="liAgents" runat="server"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Argix/Images/agentsums.png" OnCommand="OnChangeView" CommandName="Agents" /></li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
		<li id="liBlank5" runat="server">&nbsp;</li>
	</ul>
</div>
<asp:MultiView ID="mvTLs" runat="server" ActiveViewIndex="0">
<asp:View ID="vwTLView" runat="server">
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
    <div class="subtitle">Active TLs</div>
    <div class="pageBody">
        <asp:GridView id="grdTLs" runat="server" Width="100%" DataSourceID="odsTLs" AutoGenerateColumns="False" EnableTheming="True" AllowSorting="True">
            <Columns>
                <asp:BoundField DataField="TerminalID" HeaderText="Terminal" HeaderStyle-Width="48px" ItemStyle-Wrap="False" Visible="False" />
			    <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="72px" ItemStyle-Wrap="False" SortExpression="TLNumber" />
			    <asp:BoundField DataField="TLDate" HeaderText="TL Date" HeaderStyle-Width="75px" ItemStyle-Wrap="False" SortExpression="TLDate" DataFormatString="{0:MMddyy}" HtmlEncode="False" />
			    <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="CloseNumber" />
			    <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="AgentNumber" />
			    <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="ClientNumber" />
			    <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" SortExpression="ClientName" />
			    <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="Zone" />
			    <asp:BoundField DataField="Lane" HeaderText="Lane" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="Lane" />
			    <asp:BoundField DataField="SmallLane" HeaderText="SmLane" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="SmallLane" />
			    <asp:BoundField DataField="Cartons" HeaderText="Ctns" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Cartons" DataFormatString="{0:N0}" />
			    <asp:BoundField DataField="Pallets" HeaderText="Pllts" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Pallets" DataFormatString="{0:N0}" />
			    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Weight" DataFormatString="{0:N0}" />
			    <asp:BoundField DataField="Cube" HeaderText="Cube" HeaderStyle-Width="72px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Cube" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="WeightPercent" HeaderText="Weight%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" Visible="false" />
                <asp:BoundField DataField="CubePercent" HeaderText="Cube%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" Visible="false" />
		    </Columns>
	    </asp:GridView>
        <asp:ObjectDataSource ID="odsTLs" runat="server" TypeName="Argix.Shipping.TLViewerGateway" SelectMethod="GetTLView">
            <SelectParameters>
                <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
</asp:View>
<asp:View ID="vwAgentView" runat="server">
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
    <div class="subtitle">Active TLs</div>
    <div class="pageBody">
        <asp:GridView id="grdSummary" runat="server" Width="100%" DataSourceID="odsAgents" AutoGenerateColumns="False" EnableTheming="True" AllowSorting="True">
	        <Columns>
		        <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-Width="48px" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="False" SortExpression="AgentNumber" />
		        <asp:BoundField DataField="AgentName" HeaderText="Agent" ItemStyle-Wrap="False" SortExpression="AgentName" />
		        <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="48px" ItemStyle-Wrap="False" SortExpression="Zone" />
		        <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="72px" ItemStyle-Wrap="False" SortExpression="TLNumber" />
		        <asp:BoundField DataField="TLDate" HeaderText="TL Date" HeaderStyle-Width="60px" ItemStyle-Wrap="False" SortExpression="TLDate" DataFormatString="{0:MMddyy}" />
		        <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-Width="51px" ItemStyle-Wrap="False" SortExpression="CloseNumber" />
		        <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-Width="60px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
		        <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-Width="60px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
		        <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
		        <asp:BoundField DataField="WeightPercent" HeaderText="Weight%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" />
		        <asp:BoundField DataField="CubePercent" HeaderText="Cube%" HeaderStyle-Width="48px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" />
	        </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.Shipping.TLViewerGateway" SelectMethod="GetAgentSummary">
            <SelectParameters>
                <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</div>
</asp:View>
</asp:MultiView> 
</asp:Content>
