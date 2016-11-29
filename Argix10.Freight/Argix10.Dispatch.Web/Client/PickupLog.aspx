<%@ Page Title="Pickup Log" Language="C#" MasterPageFile="~/MasterPages/Client.master" AutoEventWireup="true" CodeFile="PickupLog.aspx.cs" Inherits="_PickupLog" %>
<%@ MasterType VirtualPath="~//MasterPages/Client.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="tabPage">
	<ul>
		<li id="liPickupLog" runat="server"><asp:ImageButton ID="tabPickupLog" runat="server" ImageUrl="~/App_Themes/Argix/Images/pickuplog.png" OnCommand="OnChangeView" CommandName="PickupLog" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwPickupLog" runat="server">
    <asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="true" OnTick="OnTimerTick"></asp:Timer>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upnlPickupLog" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div class="subtitle">Pickup Requests</div>
            <br />
            <div>
                <asp:GridView ID="grdPickupLog" runat="server" Width="100%" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsPickupLog" DataKeyNames="RequestID" AllowSorting="true" OnSelectedIndexChanged="OnPickupSelected" >
                    <Columns>
                        <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                        <asp:BoundField DataField="RequestID" HeaderText="Appt#" HeaderStyle-Width="60px" ItemStyle-Wrap="false" SortExpression="RequestID" />
                        <asp:BoundField DataField="Created" Visible="false" HeaderText="Created" HeaderStyle-Width="125px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy hh:mm}" SortExpression="Created" />
                        <asp:BoundField DataField="CreateUserID" Visible="false" HeaderText="Create By" HeaderStyle-Width="60px" ItemStyle-Wrap="false" SortExpression="CreateUserID" />
                        <asp:BoundField DataField="ScheduleDate" HeaderText="Requested" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="ScheduleDate" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="CallerName" Visible="false" />
                        <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="60px" ItemStyle-Wrap="false" Visible="false" />
                        <asp:BoundField DataField="Client" HeaderText="Client" HeaderStyle-Width="125px" ItemStyle-Wrap="false" Visible="false" />
                        <asp:BoundField DataField="ShipperNumber" Visible="false" />
                        <asp:BoundField DataField="Shipper" HeaderText="Shipper" HeaderStyle-Width="125px" ItemStyle-Wrap="false" SortExpression="Shipper" />
                        <asp:BoundField DataField="ShipperAddress" HeaderText="Address" HeaderStyle-Width="125px" ItemStyle-Wrap="false"  Visible="true" />
                        <asp:BoundField DataField="ShipperPhone" Visible="false" />
                        <asp:BoundField DataField="WindowOpen" HeaderText="Open" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="WindowOpen" />
                        <asp:BoundField DataField="WindowClose" HeaderText="Close" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="WindowClose" />
                        <asp:BoundField DataField="TerminalNumber" Visible="false" />
                        <asp:BoundField DataField="Terminal" HeaderText="Terminal" HeaderStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="Terminal" Visible="false" />
                        <asp:BoundField DataField="DriverName" Visible="false" />
                        <asp:BoundField DataField="OrderType" Visible="false" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="Amount" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="AmountType" />
                        <asp:BoundField DataField="FreightType" Visible="false" />
                        <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="Weight" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="Comments" />
                        <asp:BoundField DataField="ActualPickup" HeaderText="Pickup Date" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" SortExpression="ActualPickup" />
                        <asp:BoundField DataField="IsTemplate" Visible="false" />
                        <asp:BoundField DataField="CancelledUserID" Visible="false" />
                        <asp:BoundField DataField="Cancelled" HeaderText="Cancel?" HeaderStyle-Width="100px" ItemStyle-Wrap="false" Visible="false" />
                        <asp:BoundField DataField="LastUpdated" Visible="false" />
                        <asp:BoundField DataField="UserID" Visible="false" />
                        <asp:BoundField DataField="RowVersion" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsPickupLog" runat="server" TypeName="Argix.Freight.Client.FreightClientGateway" SelectMethod="ViewPickupLog" >
                    <SelectParameters>
                    <asp:ProfileParameter Name="clientNumber" PropertyName="ClientID" DefaultValue="" Type="String" />
                   </SelectParameters>
                </asp:ObjectDataSource>
            </div>
            <br />
            <br />
            <div>
                <asp:Button ID="btnNew" runat="server" Text="  New  " CssClass="submit" CommandName="New" OnCommand="OnPickupLogCommand" />
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CausesValidation="false" OnClientClick="return confirm('Cancel selected pickup?');" CommandName="Cancel" OnCommand="OnPickupLogCommand" />
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
