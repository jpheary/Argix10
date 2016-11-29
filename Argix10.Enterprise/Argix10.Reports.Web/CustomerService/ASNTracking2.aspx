<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="ASNTracking2.aspx.cs" Inherits="ASNTracking2" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ Register Src="~/ClientVendorGrids.ascx" TagName="ClientVendorGrids" TagPrefix="uc2" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<div>
    <div style="margin:25px 0px 0px 0px">
        <uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="300px" LabelWidth="75px" DateDaysBack="180" DateDaysForward="0" DateDaysSpread="14" OnDateTimeChanged="OnFromToDateChanged" />
    </div>
    <div style="margin:5px 0px 0px 48px">
        <label for="txtStore">Store#</label>&nbsp;<asp:TextBox ID="txtStore" runat="server" Width="75px" MaxLength="5" Text="" />&nbsp;&nbsp;&nbsp;
        <label for="txtZone">Zone</label>&nbsp;<asp:TextBox ID="txtZone" runat="server" Width="50px" MaxLength="2" Text="" />
    </div>
    <div style="margin:0px 20px 0px 0px; text-align:right">
        <asp:CheckBox ID="chkAllVendors" runat="server" width="288px" Text="Pickups for all vendors" TextAlign="Left" Checked="true" AutoPostBack="True" OnCheckedChanged="OnAllVendorsChecked" />
    </div>
    <div style="height:150px; margin:5px 0px 0px 10px">
        <uc2:ClientVendorGrids ID="dgdClientVendor" runat="server" Height="126px" ClientsEnabled="true" VendorsEnabled="false" OnAfterClientSelected="OnClientSelected" OnAfterVendorSelected="OnVendorSelected" />
    </div>
    <div class="clear"></div>
    <div style="margin:10px 0px 0px 10px">
        <div style="height:20px; padding:2px 5px; font-size:1.0em; background-image:url(../App_Themes/Reports/Images/gridtitle.gif); background-repeat:repeat-x;">
            <asp:Image ID="imgClients" runat="server" ImageUrl="~/App_Themes/Reports/Images/pickups.gif" ImageAlign="Middle" />&nbsp;Pickups
        </div>
        <asp:Panel id="pnlPickups" runat="server" Width="100%" Height="125px" BorderStyle="Inset" BorderWidth="1px" ScrollBars="Auto">
            <asp:GridView ID="grdPickups" runat="server" Width="100%" DataSourceID="odsPickups" DataKeyNames="PickupID,TerminalCode" AutoGenerateColumns="False" AllowSorting="True">
                <Columns>
                    <asp:TemplateField HeaderText="" HeaderStyle-Width="24px" >
                        <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllPickupsSelected"/></HeaderTemplate>
                        <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnPickupSelected"/></ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="PickupID" HeaderText="ID" HeaderStyle-Width="24px" Visible="false" />
                    <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="24px" Visible="false" />
                    <asp:BoundField DataField="VendorNumber" HeaderText="Vendor#" HeaderStyle-Width="60px" SortExpression="VendorNumber" />
                    <asp:BoundField DataField="VendorName" HeaderText="Vendor" ItemStyle-Wrap="false" SortExpression="VendorName" />
                    <asp:BoundField DataField="PUDate" HeaderText="Pickup" HeaderStyle-Width="120px" SortExpression="PUDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="PUNumber" HeaderText="#" HeaderStyle-Width="48px" SortExpression="PUNumber" />
                    <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="144px" ItemStyle-Width="144px" ItemStyle-Wrap="true" NullDisplayText=" " />
                    <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="144px" NullDisplayText=" " />
                </Columns>
            </asp:GridView>
        </asp:Panel>
    </div>
</div>
<asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseGateway" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="60" >
    <SelectParameters>
        <asp:ControlParameter Name="client" ControlID="dgdClientVendor" PropertyName="ClientNumber" Type="string" />
        <asp:ControlParameter Name="division" ControlID="dgdClientVendor" PropertyName="ClientDivsionNumber" Type="string" />
        <asp:ControlParameter Name="startDate" ControlID="ddpSetup" PropertyName="FromDate" Type="DateTime" />
        <asp:ControlParameter Name="endDate" ControlID="ddpSetup" PropertyName="ToDate" Type="DateTime" />
        <asp:ControlParameter Name="vendor" ControlID="dgdClientVendor" PropertyName="VendorNumber" Type="string" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>

