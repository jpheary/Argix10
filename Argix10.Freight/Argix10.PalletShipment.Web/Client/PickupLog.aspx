<%@ Page Title="Pickup Log" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="PickupLog.aspx.cs" Inherits="_PickupLog" %>
<%@ MasterType VirtualPath="~//MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="tabPage">
	<ul>
		<li id="liPickupLog" runat="server"><asp:ImageButton ID="tabPickupLog" runat="server" ImageUrl="~/App_Themes/Argix/Images/shipments.png" OnCommand="OnChangeView" CommandName="PickupLog" /></li>
		<li id="liBlank1" runat="server">&nbsp;</li>
		<li id="liBlank2" runat="server">&nbsp;</li>
		<li id="liBlank3" runat="server">&nbsp;</li>
		<li id="liBlank4" runat="server">&nbsp;</li>
	</ul>
</div>
<div style="border:1px solid #000000; border-top-style:none; padding:10px 10px 10px 10px; margin-top:25px">
<asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
<asp:View ID="vwPickupLog" runat="server">
    <div class="subtitle">Pickup Requests for <asp:Label ID="lblClient" runat="server" Text="" /></div>
    <asp:ValidationSummary ID="vsPickups" runat="server" ValidationGroup="vgPickups" />
    <asp:CustomValidator ID="cvStatus" runat="server" ValidationGroup="vgPickups" EnableClientScript="False" />
    <br />
    <div style="width:890px; height:275px; margin:0px 0px 0px 0px; padding:0px 0px 0px 0px; overflow-x:hidden; overflow-y:scroll; white-space:nowrap;">
        <asp:UpdatePanel runat="server" ID="upnlPickupLog" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdPickupLog" runat="server" Width="100%" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsPickupLog" DataKeyNames="RequestID,ScheduleDate,ActualPickup,Cancelled" AllowSorting="true" OnSelectedIndexChanged="OnPickupSelected" >
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
                <asp:BoundField DataField="WindowOpen" HeaderText="Open" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="WindowOpen" Visible="false" />
                <asp:BoundField DataField="WindowClose" HeaderText="Close" HeaderStyle-Width="75px" ItemStyle-Wrap="false" SortExpression="WindowClose" Visible="false" />
                <asp:BoundField DataField="TerminalNumber" Visible="false" />
                <asp:BoundField DataField="Terminal" HeaderText="Terminal" HeaderStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="Terminal" Visible="false" />
                <asp:BoundField DataField="DriverName" Visible="false" />
                <asp:BoundField DataField="OrderType" Visible="false" />
                <asp:BoundField DataField="Amount" HeaderText="Qty" HeaderStyle-Width="35px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" SortExpression="Amount" />
                <asp:BoundField DataField="AmountType" HeaderText="Type" HeaderStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="AmountType" />
                <asp:BoundField DataField="FreightType" Visible="false" />
                <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="60px" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Right" SortExpression="Weight" />
                <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-Width="200px" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="ActualPickup" HeaderText="Pickup" HeaderStyle-Width="75px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" SortExpression="ActualPickup" />
                <asp:BoundField DataField="IsTemplate" Visible="false" />
                <asp:BoundField DataField="CancelledUserID" Visible="false" />
                <asp:BoundField DataField="Cancelled" HeaderText="Cancel" HeaderStyle-Width="75px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" Visible="true" />
                <asp:BoundField DataField="LastUpdated" Visible="false" />
                <asp:BoundField DataField="UserID" Visible="false" />
                <asp:BoundField DataField="RowVersion" Visible="false" />
            </Columns>
        </asp:GridView>
        <asp:ObjectDataSource ID="odsPickupLog" runat="server" TypeName="Argix.Freight.Client.FreightClientGateway" SelectMethod="ViewPickupLog" >
            <SelectParameters>
                <asp:Parameter Name="clientNumber" DefaultValue="" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </ContentTemplate>
        <Triggers><asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Command" /></Triggers>
        </asp:UpdatePanel>
   </div>
    <br />
    <div>
        <asp:UpdatePanel ID="upnlPickupLogCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
        <asp:Button ID="btnNew" runat="server" Text="  New  " CssClass="submit" CommandName="New" OnCommand="OnPickupLogCommand" />
        <asp:Button ID="btnUpdate" runat="server" Text="Update" CssClass="submit" CommandName="Update" OnCommand="OnPickupLogCommand" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CausesValidation="false" OnClientClick="return confirm('Cancel selected pickup?');" CommandName="Cancel" OnCommand="OnPickupLogCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:View>
</asp:MultiView>
<br />
</div>
</asp:Content>
