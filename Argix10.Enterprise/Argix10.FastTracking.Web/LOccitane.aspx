<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="LOccitane.aspx.cs" Inherits="LOccitane" Title="Argix Logistics Fast Tracking" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<div id="detailHeader"  > 
    <asp:Label ID="lblDetail_ID" runat="server" Width="600px" Height="25px" style="font-size:20px" />
    <asp:Label ID="lblDetail_Status" runat="server" Width="600px" Height="35px" style="font-size:24px; text-transform:uppercase; color:#ee2a24" />
    <asp:Label ID="lblDetailSum" runat="server" Width="600px" Height="25px" /> 
</div>
<div id="detailGrid">
    <div class="detailTitle">Tracking History</div>
    <asp:GridView ID="grdDetail" runat="server" AutoGenerateColumns="False" Width="780px"  >
        <Columns>
            <asp:BoundField DataField="ItemNumber" HeaderText="Tracking Number" Visible="False" />
            <asp:BoundField DataField="DateTime" HeaderText="Date" ItemStyle-Width="120px" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
            <asp:BoundField DataField="Status" HeaderText="Status" />
            <asp:BoundField DataField="Location" HeaderText="Location" />
        </Columns>            
    </asp:GridView>
</div>
<div id="detailShipment">
    <div class="detailTitle">Shipment Details</div>
    <table style="width: 780px; background-color:#ffffff;">
        <tr class="rowHeader">
            <td class="detailHeader">From</td>
            <td class="detailHeader">To</td>
            <td class="detailHeader">Shipment Information</td>
        </tr>
        <tr><td colspan="3" style="font-size:1px; height:5px">&nbsp;</td></tr>
        <tr style="height:75px">
            <td class="detailCell"><asp:Label ID="lblFromInfo" runat="server" style="white-space:pre;" /></td>
            <td class="detailCell"><asp:Label ID="lblToInfo" runat="server" style="white-space:pre;" /></td>
            <td class="detailCell"><asp:Label ID="lbShipInfo" runat="server" style="white-space:pre;" /></td>
        </tr>
    </table>
</div>
</asp:Content>
