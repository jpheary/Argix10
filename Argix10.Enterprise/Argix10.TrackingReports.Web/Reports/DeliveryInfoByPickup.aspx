<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="DeliveryInfoByPickup.aspx.cs" Inherits="DeliveryInfoByPickup" %>
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
        <tr><td style="text-align:right">Vendor&nbsp;</td><td><asp:UpdatePanel runat="server" ID="upnlVendors" UpdateMode="Conditional"><ContentTemplate><asp:DropDownList ID="ddlVendor" runat="server" Width="275px" AppendDataBoundItems="true" DataSourceID="odsVendors" DataTextField="VendorName" DataValueField="VendorNumber" AutoPostBack="True" OnSelectedIndexChanged="OnVendorChanged"><asp:ListItem Text="All" Value="" Selected="True" /></asp:DropDownList></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel></td></tr>
        <tr style="font-size:1px; height:5px"><td colspan="2">&nbsp;</td></tr>
        <tr><td style="text-align:right">Pickups&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td colspan="2" style="vertical-align:top">
                <div style="width:800px; height:200px; margin:20px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
                    <asp:UpdatePanel runat="server" ID="upnlPickups" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:GridView ID="grdPickups" runat="server" Width="100%" DataSourceID="odsPickups" DataKeyNames="PickupID,TerminalCode" AutoGenerateColumns="False" AllowSorting="True">
                                <Columns>
                                    <asp:TemplateField HeaderText="" HeaderStyle-Width="25px" >
                                        <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllPickupsSelected"/></HeaderTemplate>
                                        <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnPickupSelected"/></ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="PickupID" HeaderText="ID" HeaderStyle-Width="25px" Visible="false" />
                                    <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="25px" Visible="false" />
                                    <asp:BoundField DataField="VendorNumber" HeaderText="Vendor#" HeaderStyle-Width="50px" SortExpression="VendorNumber" />
                                    <asp:BoundField DataField="VendorName" HeaderText="Vendor" ItemStyle-Wrap="false" SortExpression="VendorName" />
                                    <asp:BoundField DataField="PUDate" HeaderText="Pickup" HeaderStyle-Width="125px" SortExpression="PUDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                                    <asp:BoundField DataField="PUNumber" HeaderText="#" HeaderStyle-Width="50px" SortExpression="PUNumber" />
                                    <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-Wrap="true" NullDisplayText=" " />
                                    <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="150px" NullDisplayText=" " />
                                </Columns>
                            </asp:GridView>
                        </ContentTemplate>
                        <Triggers>
                            <asp:AsyncPostBackTrigger ControlID="ddpPickups" EventName="DateTimeChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
                            <asp:AsyncPostBackTrigger ControlID="ddlVendor" EventName="SelectedIndexChanged" />
                        </Triggers>
                    </asp:UpdatePanel>
                </div>
            </td>
        </tr>
    </table>
</div>
<asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetSecureClients" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900">
    <SelectParameters>
        <asp:Parameter Name="activeOnly" DefaultValue="True" Type="Boolean" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsVendors" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetVendors" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900" >
    <SelectParameters>
        <asp:ControlParameter Name="clientNumber" ControlID="ddlClient" PropertyName="SelectedValue" Type="String" />
        <asp:Parameter Name="clientTerminal"  DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="60" >
    <SelectParameters>
        <asp:ControlParameter Name="client" ControlID="ddlClient" PropertyName="SelectedValue" Type="string" />
        <asp:Parameter Name="division" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
        <asp:ControlParameter Name="startDate" ControlID="ddpPickups" PropertyName="FromDate" Type="DateTime" />
        <asp:ControlParameter Name="endDate" ControlID="ddpPickups" PropertyName="ToDate" Type="DateTime" />
        <asp:ControlParameter Name="vendor" ControlID="ddlVendor" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="string" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>

