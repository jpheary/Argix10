<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ShipmentTracking.aspx.cs" Inherits="ShipmentTracking" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>

<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<table>
    <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td style="width:400px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr>
        <td colspan="2"><uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="335px" LabelWidth="95px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="31" OnDateTimeChanged="OnFromToDateChanged" /></td>
        <td>&nbsp;</td>
    </tr>
    <tr style="font-size:1px; height:5px"><td colspan="3">&nbsp;</td></tr>
    <tr>
        <td style="text-align:right">Client&nbsp;</td>
        <td><asp:DropDownList id="cboClient" runat="server" Width="300px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged"></asp:DropDownList></td>
        <td>&nbsp;</td>
    </tr>
</table>
<asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetClientsList" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="600">
    <SelectParameters>
        <asp:Parameter Name="activeOnly" DefaultValue="false" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>
