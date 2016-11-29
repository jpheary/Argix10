<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="InboundShipmentTracking.aspx.cs" Inherits="InboundShipmentTracking" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/ClientVendorGrids.ascx" TagName="ClientVendorGrids" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<table style="width:100%">
    <tr style="font-size:1px"><td style="width:96px">&nbsp;</td><td style="width:384px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr>
        <td colspan="2">&nbsp;<uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="336px" LabelWidth="96px" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="7" OnDateTimeChanged="OnFromToDateChanged" /></td>
        <td>&nbsp;</td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    <tr><td>&nbsp;</td><td style="text-align:right"><asp:CheckBox ID="chkAllDivisons" runat="server" width="288px" Text="Pickups for all divisions" TextAlign="Left" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllDivisionsChecked" /></td><td style="text-align:right"><asp:CheckBox ID="chkAllVendors" runat="server" width="288px" Text="Pickups for all vendors" TextAlign="Left" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllVendorsChecked" /></td></tr>
    <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    <tr><td colspan="3"><uc2:ClientVendorGrids ID="dgdClientVendor" runat="server" Height="126px" ClientsEnabled="true" VendorsEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterVendorSelected="OnVendorSelected" /></td></tr>
    <tr style="font-size:1px; height:6px"><td colspan="3">&nbsp;</td></tr>
    <tr>
        <td colspan="3">
            <table style="width:100%">
                <tr style="height:16px"><td style="font-size:1.0em; vertical-align:middle; padding-left:6px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">&nbsp;Pickups</td></tr>
                <tr>
                    <td style="vertical-align:top">
                        <asp:UpdatePanel ID="upnlPickups" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                        <ContentTemplate>
                            <asp:Panel id="pnlPickups" runat="server" Width="100%" Height="150px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                                <asp:GridView ID="grdPickups" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="odsPickups" DataKeyNames="ClientDivision,VendorNumber,VendorName,PUDate,PUNumber" AllowSorting="True">
                                    <Columns>
                                        <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                                            <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllPickupsSelected"/></HeaderTemplate>
                                            <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnPickupSelected"/></ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="PickupID" HeaderText="ID" HeaderStyle-Width="24px" Visible="false" />
                                        <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="24px" Visible="false" />
                                        <asp:BoundField DataField="ClientDivision" HeaderText="Div" HeaderStyle-Width="24px" Visible="true" />
                                        <asp:BoundField DataField="VendorNumber" HeaderText="Vendor#" HeaderStyle-Width="60px" SortExpression="VendorNumber" />
                                        <asp:BoundField DataField="VendorName" HeaderText="Vendor" ItemStyle-Wrap="false" SortExpression="VendorName" />
                                        <asp:BoundField DataField="PUDate" HeaderText="Pickup" HeaderStyle-Width="120px" SortExpression="PUDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                        <asp:BoundField DataField="PUNumber" HeaderText="#" HeaderStyle-Width="48px" SortExpression="PUNumber" />
                                        <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="144px" ItemStyle-Width="144px" ItemStyle-Wrap="true" NullDisplayText=" " />
                                        <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="144px" NullDisplayText=" " />
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300" >
                                    <SelectParameters>
                                        <asp:ControlParameter Name="client" ControlID="dgdClientVendor" PropertyName="ClientNumber" Type="string" />
                                        <asp:Parameter Name="division" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                        <asp:ControlParameter Name="startDate" ControlID="ddpPickups" PropertyName="FromDate" Type="DateTime" />
                                        <asp:ControlParameter Name="endDate" ControlID="ddpPickups" PropertyName="ToDate" Type="DateTime" />
                                        <asp:ControlParameter Name="vendor" ControlID="dgdClientVendor" PropertyName="VendorNumber" Type="string" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </asp:Panel>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddpPickups" EventName="DateTimeChanged" />
                            <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterClientSelected" />
                            <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterVendorSelected" />
                        </Triggers>
                        </asp:UpdatePanel>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</table>
</asp:Content>

