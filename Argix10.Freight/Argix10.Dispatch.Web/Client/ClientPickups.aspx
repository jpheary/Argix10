<%@ Page Title="Client Pickups" Language="C#" MasterPageFile="~/MasterPages/Client.master" AutoEventWireup="true" CodeFile="ClientPickups.aspx.cs" Inherits="_ClientPickups" %>
<%@ MasterType VirtualPath="~//MasterPages/Client.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="tabPage">
	<ul>
		<li id="liPickups" runat="server"><asp:ImageButton ID="tabPickups" runat="server" ImageUrl="~/App_Themes/Argix/Images/pickuplog.png" OnCommand="OnChangeView" CommandName="Pickups" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwPickups" runat="server">
    <asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="true" OnTick="OnTimerTick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upnlPickups" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="subtitle">Client Pickups</div>
            <br />
            <div>
                <asp:GridView ID="grdPickups" runat="server" Width="100%" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsPickups" DataKeyNames="ID" AllowSorting="true" OnSelectedIndexChanged="OnPickupSelected" >
                    <Columns>
                        <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                        <asp:BoundField DataField="ID" HeaderText="Appt#" HeaderStyle-Width="60px" ItemStyle-Wrap="false" SortExpression="ID" />
                        <asp:BoundField DataField="Created" Visible="false" HeaderText="Created" HeaderStyle-Width="125px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy hh:mm}" SortExpression="Created" />
                        <asp:BoundField DataField="CreateUserID" Visible="false" HeaderText="Create By" HeaderStyle-Width="60px" ItemStyle-Wrap="false" SortExpression="CreateUserID" />
                        <asp:BoundField DataField="ScheduleDate" HeaderText="Requested" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="ScheduleDate" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="60px" ItemStyle-Wrap="false" Visible="false" />
                        <asp:BoundField DataField="Client" HeaderText="Client" HeaderStyle-Width="125px" ItemStyle-Wrap="false" Visible="false" />
                        <asp:BoundField DataField="ShipperNumber" Visible="false" />
                        <asp:BoundField DataField="Shipper" HeaderText="Shipper" HeaderStyle-Width="125px" ItemStyle-Wrap="false" SortExpression="Shipper" />
                        <asp:BoundField DataField="ShipperAddress" HeaderText="Address" HeaderStyle-Width="125px" ItemStyle-Wrap="false"  Visible="true" />
                        <asp:BoundField DataField="ShipperPhone" Visible="false" />
                        <asp:BoundField DataField="ShipperWindowOpen" HeaderText="Open" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="WindowOpen" Visible="false" />
                        <asp:BoundField DataField="ShipperWindowClose" HeaderText="Close" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="WindowClose" Visible="false" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="Amount" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="AmountType" />
                        <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="Weight" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="Comments" />
                        <asp:BoundField DataField="ActualPickup" HeaderText="Pickup Date" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" SortExpression="ActualPickup" />
                        <asp:BoundField DataField="CancelledUserID" Visible="false" />
                        <asp:BoundField DataField="Cancelled" HeaderText="Cancel Date" HeaderStyle-Width="100px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" Visible="true" />
                        <asp:BoundField DataField="LastUpdated" Visible="false" />
                        <asp:BoundField DataField="UserID" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.Freight.Client.FreightClientGateway" SelectMethod="ViewClientPickups" >
                    <SelectParameters>
                    <asp:ProfileParameter Name="clientNumber" PropertyName="ClientID" DefaultValue="" Type="String" />
                   </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
            <br />
            <div>
                <asp:Button ID="btnNew" runat="server" Text="  New  " CssClass="submit" CommandName="New" OnCommand="OnPickupsCommand" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CausesValidation="false" OnClientClick="return confirm('Cancel selected pickup?');" CommandName="Cancel" OnCommand="OnPickupsCommand" />
            </div>
            <br />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tmrRefresh" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
</asp:View>
</asp:MultiView>
<br />
</div>
</asp:Content>
