<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="LOccitaneCartonInfo.aspx.cs" Inherits="LOccitaneCartonInfo" %>
<%@ Register Src="~/DualDateTimePicker.ascx" TagName="DualDateTimePicker" TagPrefix="uc1" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<div class="form" style="width:800px">
    <uc1:DualDateTimePicker ID="ddpSetup" runat="server" Width="350px" LabelWidth="100px" FromLabel="Pickups From" ToLabel="To" DateDaysBack="90" DateDaysForward="0" DateDaysSpread="30" OnDateTimeChanged="OnFromToDateChanged" />
    <div style="margin:5px 0px 0px 65px">Pickups</div>
    <div style="width:800px; height:275px; margin:20px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
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
                <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="60" >
                    <SelectParameters>
                        <asp:Parameter Name="client" DefaultValue="012" Type="string" />
                        <asp:Parameter Name="division" DefaultValue="01" Type="string" />
                        <asp:ControlParameter Name="startDate" ControlID="ddpSetup" PropertyName="FromDate" Type="DateTime" />
                        <asp:ControlParameter Name="endDate" ControlID="ddpSetup" PropertyName="ToDate" Type="DateTime" />
                        <asp:Parameter Name="vendor" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddpSetup" EventName="DateTimeChanged" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</div>
</asp:Content>

