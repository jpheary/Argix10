<%@ Page Title="Shipments" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Shipments.aspx.cs" Inherits="Shipments" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="tabPage">
	<ul>
		<li id="liShipments" runat="server"><asp:ImageButton ID="tabShipments" runat="server" ImageUrl="~/App_Themes/Argix/Images/shipments.png" OnCommand="OnChangeView" CommandName="Shipments" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwShipments" runat="server">
    <div class="subtitle">Shipments for <asp:Label ID="lblClient" runat="server" Text="" /></div>
    <asp:ValidationSummary ID="vsShipments" runat="server" ValidationGroup="vgShipments" />
    <asp:CustomValidator ID="cvStatus" runat="server" ValidationGroup="vgShipments" EnableClientScript="False" />
    <br />
    <div style="width:890px; height:275px; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
        <asp:UpdatePanel runat="server" ID="upnlShipments" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdShipments" runat="server" Width="100%" AutoGenerateColumns="false" DataSourceID="odsShipments" DataKeyNames="ID,ShipDate,PickupID,PickupDate" AllowSorting="true" OnSelectedIndexChanged="OnShipmentSelected" >
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="Created" HeaderText="Created" ItemStyle-Wrap="False" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="False" />
                <asp:BoundField DataField="ID" HeaderText="ID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ShipmentNumber" HeaderText="Shipment#" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                <asp:BoundField DataField="ClientID" HeaderText="ClientID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                <asp:BoundField DataField="ShipperID" HeaderText="ShipperID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ShipperName" HeaderText="Shipper" ItemStyle-Wrap="False" ItemStyle-Width="150px" Visible="True" />
                <asp:BoundField DataField="ConsigneeID" HeaderText="ConsigneeID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="ConsigneeName" HeaderText="Consignee" ItemStyle-Wrap="False" ItemStyle-Width="150px" Visible="True" />
                <asp:BoundField DataField="Pallets" HeaderText="Pallets" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" Visible="True" />
                <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" Visible="True" />
                <asp:BoundField DataField="PalletRate" HeaderText="Rate" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <asp:BoundField DataField="FuelSurcharge" HeaderText="FSC" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <asp:BoundField DataField="AccessorialCharge" HeaderText="Access" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="InsuranceCharge" HeaderText="Insure" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <asp:BoundField DataField="TollCharge" HeaderText="TSC" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" Visible="False" />
                <asp:BoundField DataField="TotalCharge" HeaderText="Total Charge" ItemStyle-Wrap="False" ItemStyle-Width="100px" ItemStyle-HorizontalAlign="Right" Visible="True" />
                <asp:BoundField DataField="InsidePickup" HeaderText="Inside?" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="LiftGateOrigin" HeaderText="LiftGate" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="AppointmentOrigin" HeaderText="Appt Orig" ItemStyle-Wrap="False" DataFormatString="{0:HH:mm tt}" HtmlEncode="False" Visible="False" />
                <asp:BoundField DataField="InsideDelivery" HeaderText="Inside?" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="LiftGateDestination" HeaderText="LiftGate" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="AppointmentDestination" HeaderText="Appt Dest" ItemStyle-Wrap="False" DataFormatString="{0:HH:mm tt}" HtmlEncode="False" Visible="False" />
                <asp:BoundField DataField="LastUpdated" HeaderText="LastUpdated" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="UserID" HeaderText="UserID" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="PickupID" HeaderText="Pickup#" ItemStyle-Wrap="False" Visible="True" />
                <asp:BoundField DataField="PickupDate" HeaderText="Pickup" ItemStyle-Wrap="False" Visible="True" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsShipments" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewLTLShipments">
            <SelectParameters>
                <asp:Parameter Name="clientID" DefaultValue="" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Command" /></Triggers>
        </asp:UpdatePanel>
    </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upnlShipmentsCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
        <asp:Button ID="btnNew" runat="server" Text="  New  " CssClass="submit" CommandName="New" OnCommand="OnShipmentCommand" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="submit" CommandName="Update" OnCommand="OnShipmentCommand" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CommandName="Cancel" OnCommand="OnShipmentCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
</asp:View>
</asp:MultiView>
<br />
</div>
</asp:Content>
