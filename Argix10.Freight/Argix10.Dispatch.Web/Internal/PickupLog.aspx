<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="PickupLog.aspx.cs" Inherits="PickupLog" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="false" OnTick="OnScheduleTimerTick"></asp:Timer>
            </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upnlSchedule" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="gridtitle">
                Pickup Log&nbsp;
                <asp:DropDownList ID="cboSchedule" runat="server" AutoPostBack="true" ToolTip="Schedule" style="width:100px" >
                    <asp:ListItem Text="Today" Value="Today" Selected="True" />
                    <asp:ListItem Text="Advanced" Value="Advanced" />
                    <asp:ListItem Text="Archive" Value="Archive" />
                </asp:DropDownList>
                &nbsp;<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ImageAlign="Middle" ToolTip="Refresh schedule" CommandName="Refresh" OnCommand="OnRefreshSchedule" />
            </div>
            <div id="grid">
                <asp:GridView ID="grdSchedule" runat="server" Width="100%" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsRequests" AllowSorting="true" >
                    <Columns>
                        <asp:BoundField DataField="RequestID" HeaderText="Appt#" HeaderStyle-Width="60px" ItemStyle-Wrap="false" SortExpression="ID" />
                        <asp:BoundField DataField="Created" Visible="false" HeaderText="Created" HeaderStyle-Width="120px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy hh:mm}" SortExpression="Created" />
                        <asp:BoundField DataField="CreateUserID" Visible="false" HeaderText="Create By" HeaderStyle-Width="60px" ItemStyle-Wrap="false" SortExpression="CreateUserID" />
                        <asp:BoundField DataField="ScheduleDate" HeaderText="Requested" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="ScheduleDate" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="CallerName" Visible="false" />
                        <asp:BoundField DataField="ClientNumber" Visible="false" />
                        <asp:BoundField DataField="Client" HeaderText="Client" HeaderStyle-Width="120px" ItemStyle-Wrap="false" SortExpression="Client" />
                        <asp:BoundField DataField="ShipperNumber" Visible="false" />
                        <asp:BoundField DataField="Shipper" HeaderText="Shipper" HeaderStyle-Width="120px" ItemStyle-Wrap="false" SortExpression="Shipper" />
                        <asp:BoundField DataField="ShipperAddress"  Visible="false" />
                        <asp:BoundField DataField="ShipperPhone" Visible="false" />
                        <asp:BoundField DataField="WindowOpen" HeaderText="Open" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="WindowOpen" />
                        <asp:BoundField DataField="WindowClose" HeaderText="Close" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="WindowClose" />
                        <asp:BoundField DataField="TerminalNumber" Visible="false" />
                        <asp:BoundField DataField="Terminal" HeaderText="Terminal" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="Terminal" />
                        <asp:BoundField DataField="DriverName" Visible="false" />
                        <asp:BoundField DataField="ActualPickup" HeaderText="Act Pickup" HeaderStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" SortExpression="ActualPickup" />
                        <asp:BoundField DataField="OrderType" Visible="false" />
                        <asp:BoundField DataField="Amount" HeaderText="Amount" HeaderStyle-Width="48px" ItemStyle-Wrap="false" SortExpression="Amount" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" HeaderStyle-Width="48px" ItemStyle-Wrap="false" SortExpression="AmountType" />
                        <asp:BoundField DataField="FreightType" Visible="false" />
                        <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="72px" ItemStyle-Wrap="false" SortExpression="Weight" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" HeaderStyle-Width="96px" ItemStyle-Wrap="false" SortExpression="Comments" />
                        <asp:BoundField DataField="IsTemplate" Visible="false" />
                        <asp:BoundField DataField="CancelledUserID" Visible="false" />
                        <asp:BoundField DataField="Cancelled" Visible="false" />
                        <asp:BoundField DataField="LastUpdated" Visible="false" />
                        <asp:BoundField DataField="UserID" Visible="false" />
                        <asp:BoundField DataField="RowVersion" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsRequests" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewPickupLog" >
                    <SelectParameters>
                        <asp:ControlParameter Name="schedule" ControlID="cboSchedule" PropertyName="SelectedValue" Type="String" />
                   </SelectParameters>
                </asp:ObjectDataSource>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="tmrRefresh" EventName="Tick" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
