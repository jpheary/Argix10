<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="DamagedCartons.aspx.cs" Inherits="DamagedCartons" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/ClientVendorGrids.ascx" TagName="ClientVendorGrids" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<table style="width:100%">
    <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td style="width:400px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr>
        <td colspan="2"><uc1:DualDateTimePicker ID="ddpPickups" runat="server" Width="350px" LabelWidth="95px" DateDaysBack="365" DateDaysForward="0" DateDaysSpread="14" OnDateTimeChanged="OnFromToDateChanged" />
        <td>&nbsp;</td>
    </tr>
    <tr>
        <td style="text-align:right">Damage Code&nbsp;</td>
        <td style="text-align:left"><asp:DropDownList ID="cboDamageCode" runat="server" Width="210px" DataSourceID="odsDamageCodes" DataTextField="DESCRIPTION" DataValueField="CODE" /></td>
        <td>&nbsp;</td>
    </tr>
</table>
<table style="width:100%">
    <tr><td style="text-align:right"><asp:CheckBox ID="chkAllVendors" runat="server" Width="300px" Text="Pickups for all vendors" TextAlign="Right" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllVendorsChecked" /></td></tr>
    <tr><td>&nbsp;<uc2:ClientVendorGrids ID="dgdClientVendor" runat="server" Height="125px" ClientsEnabled="true" VendorsEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterVendorSelected="OnVendorSelected" /></td></tr>
    <tr>
        <td>
            <div style="height:20px; font-size:1.0em; vertical-align:middle; padding-left:5px; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">&nbsp;Pickups</div>
            <asp:Panel id="pnlPickups" runat="server" Width="100%" Height="140px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
                <asp:UpdatePanel ID="upnlPickups" runat="server" ChildrenAsTriggers="true" RenderMode="Block" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="grdPickups" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="odsPickups" DataKeyNames="PickupID,TerminalCode,VendorNumber,VendorName,PUDate,PUNumber,ManifestNumbers,TrailerNumbers" AllowSorting="True">
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="25px" >
                                <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllPickupsSelected"/></HeaderTemplate>
                                <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnPickupSelected"/></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PickupID" HeaderText="ID" HeaderStyle-Width="25px" Visible="false" />
                            <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="25px" Visible="false" />
                            <asp:BoundField DataField="VendorNumber" HeaderText="Vendor#" HeaderStyle-Width="75px" SortExpression="VendorNumber" />
                            <asp:BoundField DataField="VendorName" HeaderText="Vendor" ItemStyle-Wrap="False" SortExpression="VendorName" HtmlEncode="False" />
                            <asp:BoundField DataField="PUDate" HeaderText="Pickup" HeaderStyle-Width="125px" SortExpression="PUDate" DataFormatString="{0:yyyy-MM-dd}" HtmlEncode="False" />
                            <asp:BoundField DataField="PUNumber" HeaderText="#" HeaderStyle-Width="50px" SortExpression="PUNumber" />
                            <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="150px" ItemStyle-Width="144px" ItemStyle-Wrap="true" NullDisplayText=" " />
                            <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="150px" NullDisplayText=" " />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddpPickups" EventName="DateTimeChanged" />
                    <asp:AsyncPostBackTrigger ControlID="chkAllVendors" EventName="CheckedChanged" />
                    <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterClientSelected" />
                    <asp:AsyncPostBackTrigger ControlID="dgdClientVendor" EventName="AfterVendorSelected" />
                </Triggers>
                </asp:UpdatePanel>
            </asp:Panel>
        </td>
    </tr>
</table>
<asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="300" >
    <SelectParameters>
        <asp:ControlParameter Name="client" ControlID="dgdClientVendor" PropertyName="ClientNumber" Type="string" />
        <asp:ControlParameter Name="division" ControlID="dgdClientVendor" PropertyName="ClientDivsionNumber" Type="string" />
        <asp:ControlParameter Name="startDate" ControlID="ddpPickups" PropertyName="FromDate" Type="DateTime" />
        <asp:ControlParameter Name="endDate" ControlID="ddpPickups" PropertyName="ToDate" Type="DateTime" />
        <asp:ControlParameter Name="vendor" ControlID="dgdClientVendor" PropertyName="VendorNumber" Type="string" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsDamageCodes" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetDamageCodes" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="900" />
</asp:Content>

