<%@ Page Title="Carton Detail" Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="CartonDetail.aspx.cs" Explicit="false" Inherits="_CartonDetail" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpContent" Runat="Server">
    <div class="subtitle">Carton Detail</div>
    <div class="trackresponse">
        <div class="gridviewtitle"><asp:LinkButton id="lnkBack" runat="server" PostBackUrl="~/Members/CartonSummary.aspx"><<&nbsp;&nbsp;&nbsp;Back</asp:LinkButton></div>
		<div>
            <div class="trackresponsetitle"><asp:Label id="lblStore" runat="server" /></div>
            <div class="trackresponsedetail">
                <label for="lblCartonNumber">Carton#</label><asp:Label id="lblCartonNumber" runat="server" /><br />
                <label for="lblClientName">Client</label><asp:Label id="lblClientName" runat="server" /><br />
                <label for="lblVendorName">DC/Vendor</label><asp:Label id="lblVendorName" runat="server" /><br />
                <label for="lblPickupDate">Pickup Date</label><asp:Label id="lblPickupDate" runat="server" /><br />
                <label for="lblSchDelivery">Est. Delivery</label><asp:Label id="lblSchDelivery" runat="server" /><br />
                <label for="lblShipmentNumber">Shipment#</label><asp:Label ID="lblShipmentNumber" runat="server" /><br />
           </div>
            <div class="trackresponsedetail">
                <label for="lblBOLNumber">BOL#</label><asp:Label id="lblBOLNumber" runat="server" /><br />
                <label for="lblTLNumber">TL#</label><asp:Label id="lblTLNumber" runat="server" /><br />
                <label for="lblLabelNumber">Label#</label><asp:Label id="lblLabelNumber" runat="server" /><br />
                <label for="lblPONumber">PO#</label><asp:Label id="lblPONumber" runat="server" /><br />
                <label for="lblWeight">Weight</label><asp:Label id="lblWeight" runat="server" /><br />
                <label>&nbsp;</label>
            </div>
            <div class="clearleft"></div>
            <br />
            <div class="trackresponsetitle">History</div>
            <asp:GridView ID="grdDetail" runat="server" Width="720px" AutoGenerateColumns="False" >
                <Columns>
                    <asp:BoundField DataField="ItemNumber" HeaderText="Tracking Number" ItemStyle-Width="150px" Visible="False" />
                    <asp:BoundField DataField="DateTime" HeaderText="Date" ItemStyle-Width="150px" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-Width="250px" />
                    <asp:BoundField DataField="Location" HeaderText="Location" ItemStyle-Width="250px" />
                </Columns>            
                <HeaderStyle Font-Size="1.1em" />
            </asp:GridView>
            <br />
        </div>
        <div class="services">
            <asp:LinkButton id="lnkPODReq" runat="server" Text="POD Image" Visible="True" OnClick="OnPODRequest" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink  id="lnkFileClaim" runat="server" Text="File Claim" Target="_blank" Visible="True" />
        </div>
    </div>
</asp:Content>

