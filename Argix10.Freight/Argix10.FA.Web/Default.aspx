<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div>
    Terminal&nbsp;
    <asp:DropDownList ID="cboTerminal" runat="server" AutoPostBack="True" ToolTip="Local Terminals" style="width:150px" OnSelectedIndexChanged="OnTerminalChanged" />
    &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnClick="OnRefresh" />
</div>
<div class="tabPage">
	<ul>
		<li id="liRegular" runat="server"><asp:ImageButton ID="tabRegular" runat="server" ImageUrl="~/App_Themes/Argix/Images/regular.png" OnCommand="OnChangeView" CommandName="Regular" /></li>
		<li id="liReturns" runat="server"><asp:ImageButton ID="tabReturns" runat="server" ImageUrl="~/App_Themes/Argix/Images/returns.png" OnCommand="OnChangeView" CommandName="Returns" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
    <div class="subtitle">Freight</div>
    <div class="pageBody">
        <asp:GridView ID="grdShipments" runat="server" Width="100%" Height="100%" DataSourceID="odsShipments" DataKeyNames="FreightID" AutoGenerateColumns="false" OnSelectedIndexChanged="OnShipmentSelected" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="FreightID" HeaderText="ID" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="FreightType" HeaderText="Type" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="CurrentLocation" HeaderText="Location" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="TDSNumber" HeaderText="TDS#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="StorageTrailerNumber" HeaderText="StTrailer#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ClientNumber" HeaderText="Client#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="ShipperNumber" HeaderText="Shipper#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="ShipperName" HeaderText="Shipper" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="Pickup" HeaderText="Pickup" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="Cartons" HeaderText="Cartons" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="Pallets" HeaderText="Pallets" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="CarrierNumber" HeaderText="Carrier#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="DriverNumber" HeaderText="Driver#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="FloorStatus" HeaderText="Floor" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="SealNumber" HeaderText="Seal#" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="UnloadedStatus" HeaderText="UnStatus" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="VendorKey" HeaderText="VendorKey" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="ReceiveDate" HeaderText="Received" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="TerminalID" HeaderText="Terminal" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="IsSortable" HeaderText="Sort?" ItemStyle-Width="50px" ItemStyle-Wrap="False" Visible="True" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsShipments" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetInboundFreight">
            <SelectParameters>
                <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                <asp:Parameter Name="freightType" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
    <br />
    <div>
        <asp:Button ID="btnAssign" runat="server" Text=" Assign " CssClass="submit" CommandName="Assign" OnCommand="OnCommand" />
    </div>
</div>
</asp:Content>
