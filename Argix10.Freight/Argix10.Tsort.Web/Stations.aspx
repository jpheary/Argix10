<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Stations.aspx.cs" Inherits="Stations" %>
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
		<li id="liStations" runat="server"><asp:ImageButton ID="tabStations" runat="server" ImageUrl="~/App_Themes/Argix/Images/stations.png" OnCommand="OnChangeView" CommandName="Stations" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
		<li id="liBlank5" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
    <div class="subtitle">Assignments</div>
    <div class="pageBody">
        <asp:GridView ID="grdStations" runat="server" Width="100%" Height="100%" DataSourceID="odsStations" DataKeyNames="WorkStationID,FreightID,SortTypeID" AutoGenerateColumns="false" AllowSorting="true" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="WorkStationID" HeaderText="ID" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="WorkStationID" Visible="False" />
                <asp:BoundField DataField="StationNumber" HeaderText="Station#" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="StationNumber" Visible="True" />
                <asp:BoundField DataField="FreightID" HeaderText="FreightID" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="FreightID" Visible="False" />
                <asp:BoundField DataField="FreightType" HeaderText="Type" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="FreightType" Visible="True" />
                <asp:BoundField DataField="SortTypeID" HeaderText="SortTypeID" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="SortTypeID" Visible="False" />
                <asp:BoundField DataField="SortType" HeaderText="Sort Type" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="SortType" Visible="True" />
                <asp:BoundField DataField="TDSNumber" HeaderText="TDS#" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="TDSNumber" Visible="True" />
                <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="TrailerNumber" Visible="True" />
                <asp:BoundField DataField="Client" HeaderText="Client" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Client" Visible="True" />
                <asp:BoundField DataField="Shipper" HeaderText="Shipper" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Shipper" Visible="True" />
                <asp:BoundField DataField="Pickup" HeaderText="Pickup" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Pickup" Visible="True" />
                <asp:BoundField DataField="TerminalID" HeaderText="TerminalID" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="TerminalID" Visible="False" />
                <asp:BoundField DataField="Result" HeaderText="Result" ItemStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Result" Visible="True" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsStations" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetStationAssignments">
            <SelectParameters>
                <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <br />
    <div>
        <asp:Button ID="btnUnassign" runat="server" Text=" Unssign " CssClass="submit" CommandName="Unassign" OnCommand="OnCommand" />
    </div>
</div>
</asp:Content>
