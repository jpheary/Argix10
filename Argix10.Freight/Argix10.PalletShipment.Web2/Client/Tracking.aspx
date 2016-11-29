<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Tracking.aspx.cs" Inherits="_Tracking" %>
<!DOCTYPE HTML>

<html>
<head runat="server">
    <title>Pallet Shipment Tracking</title>
</head>
<body>
<form id="form1" runat="server">
    <asp:ScriptManager ID="smPage" runat="server" EnableCdn="false" EnablePartialRendering="true" AsyncPostBackTimeout="600" ScriptMode="Auto" LoadScriptsBeforeUI="false"></asp:ScriptManager>
    <asp:UpdatePanel runat="server" ID="pnlMsg" UpdateMode="Always"><ContentTemplate><asp:Label ID="lblMsg" runat="server" Text="" /></ContentTemplate></asp:UpdatePanel>
    <div class="trackresponse">
        <div class="trackresponsetitle">Pallets for shipment#&nbsp;<asp:Label ID="lblShipmentNumber" runat="server" /></div>
        <asp:GridView ID="grdTrack" runat="server" Width="750px" AutoGenerateColumns="False" DataKeyNames="ItemNumber" OnSelectedIndexChanged="OnItemSelected">
            <Columns>
                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                <asp:BoundField DataField="ItemNumber" HeaderText="Tracking#" HeaderStyle-Width="150px" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="DateTime" HeaderText="Date" HeaderStyle-Width="120px" HeaderStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
                <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="144px" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="192px" HeaderStyle-HorizontalAlign="Left" />
                <asp:BoundField DataField="CBOL" HeaderText="CBOL" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" SortExpression="CBOL" />
            </Columns>
        </asp:GridView>
        <br /><br />
        <asp:UpdatePanel runat="server" ID="upnlDetail" UpdateMode="Always" RenderMode="Inline">
        <ContentTemplate>
        <div class="trackresponsetitle">&nbsp;<asp:Label id="lblStoreNumber" runat="server" Text="Consignee " />&nbsp;<asp:Label id="lblStore" runat="server" /></div>
        <div class="trackresponsedetail">
            <label for="lblCartonNumber">Carton#</label><asp:Label id="lblCartonNumber" runat="server" /><br />
            <label for="lblClientName">Client</label><asp:Label id="lblClientName" runat="server" /><br />
            <label for="lblVendorName">DC/Vendor</label><asp:Label id="lblVendorName" runat="server" /><br />
            <label for="lblPickupDate">Pickup Date</label><asp:Label id="lblPickupDate" runat="server" /><br />
            <label for="lblSchDelivery">Est. Delivery</label><asp:Label id="lblSchDelivery" runat="server" /><br />
        </div>
        <div class="trackresponsedetail">
            <label for="lblBOLNumber">BOL#</label><asp:Label id="lblBOLNumber" runat="server" /><br />
            <label for="lblTLNumber">TL#</label><asp:Label id="lblTLNumber" runat="server" /><br />
            <label for="lblLabelNumber">Label#</label><asp:Label id="lblLabelNumber" runat="server" /><br />
            <label for="lblPONumber">PO#</label><asp:Label id="lblPONumber" runat="server" /><br />
            <label for="lblWeight">Weight</label><asp:Label id="lblWeight" runat="server" /><br />
        </div>
        <div class="clearleft"></div>
        <br />
        <div class="trackresponsetitle">&nbsp;History</div>
        <asp:GridView ID="grdDetail" runat="server" Width="750px" AutoGenerateColumns="False" >
            <Columns>
                <asp:BoundField DataField="ItemNumber" HeaderText="Tracking Number" Visible="False" />
                <asp:BoundField DataField="DateTime" HeaderText="Date" ItemStyle-Width="120px" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
                <asp:BoundField DataField="Status" HeaderText="Status" />
                <asp:BoundField DataField="Location" HeaderText="Location" />
            </Columns>            
        </asp:GridView>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</form>
</body>
</html>
