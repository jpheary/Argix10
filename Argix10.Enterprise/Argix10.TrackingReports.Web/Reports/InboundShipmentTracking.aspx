<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="InboundShipmentTracking.aspx.cs" Inherits="InboundShipmentTracking" %>
<%@ Register Src="../DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<div class="form" style="width:800px">
    <table>
        <tr style="font-size:1px; height:1px"><td style="width:100px">&nbsp;</td><td style="width:700px">&nbsp;</td></tr>
        <tr><td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" FromLabel="Pickups From" Width="350px" LabelWidth="90px" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td></tr>
        <tr style="font-size:1px; height:5px"><td colspan="2">&nbsp;</td></tr>
        <tr><td style="text-align:right">Client&nbsp;</td><td><asp:DropDownList ID="ddlClient" runat="server" Width="275px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged" /></td></tr>
        <tr style="font-size:1px; height:5px"><td colspan="2">&nbsp;</td></tr>
    </table>
</div>
<asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetSecureClients" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900">
    <SelectParameters>
        <asp:Parameter Name="activeOnly" DefaultValue="True" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>

