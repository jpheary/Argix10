<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="OutboundSchedule.aspx.cs" Inherits="OutboundSchedule" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cphBody" Runat="Server">
    <asp:UpdatePanel ID="upnlTimer" runat="server" UpdateMode="Always">
            <ContentTemplate>
                <asp:Timer ID="tmrRefresh" runat="server" Interval="30000" Enabled="false" OnTick="OnScheduleTimerTick"></asp:Timer>
            </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="upnlSchedule" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <div id="gridtitle">
                Outbound Schedule&nbsp;
                <asp:DropDownList ID="cboSchedule" runat="server" AutoPostBack="true" ToolTip="Schedule" style="width:100px" >
                    <asp:ListItem Text="Today" Value="Today" Selected="True" />
                    <asp:ListItem Text="Advanced" Value="Advanced" />
                    <asp:ListItem Text="Archive" Value="Archive" />
                </asp:DropDownList>
                &nbsp;<asp:ImageButton ID="btnRefresh" runat="server" ImageUrl="~/App_Themes/Argix/Images/refresh.gif" ImageAlign="Middle" ToolTip="Refresh schedule" CommandName="Refresh" OnCommand="OnRefreshSchedule" />
            </div>
            <div id="gridheader">
            <table cellspacing="1" rules="all" border="0" id="ctl00_cphBody_grdSchedule" style="background-color:window;border-collapse:collapse;">
			<tr>
				<th scope="col" style="width:70px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$ScheduleDate&#39;)">Date</a></th>
                <th scope="col" style="width:125px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$DriverName&#39;)">Driver</a></th>
                <th scope="col" style="width:60px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$ScheduledDeparture&#39;)">Sch Depart</a></th>
                <th scope="col" style="width:60px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$ActualDeparture&#39;)">Act Depart</a></th>
                <th scope="col" style="width:200px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$Origin&#39;)">Origin</a></th>
                <th scope="col" style="width:200px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$Destination&#39;)">Destination</a></th>
                <th scope="col" style="width:60px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$ScheduledArrival&#39;)">Sch Arrival</a></th>
                <th scope="col" style="width:60px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$ActualArrival&#39;)">Act Arrival</a></th>
                <th scope="col" style="width:50px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$TrailerNumber&#39;)">Trailer#</a></th>
                <th scope="col" style="width:40px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$Amount&#39;)">Amt</a></th>
                <th scope="col" style="width:50px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$AmountType&#39;)">Type</a></th>
                <th scope="col" style="width:150px;"><a href="javascript:__doPostBack(&#39;ctl00$cphBody$grdSchedule&#39;,&#39;Sort$Comments&#39;)">Comments</a></th>
			    <th scope="col" style="width:15px;background-color: #c5c7c9; border:0 none #000000">&nbsp;</th>
            </tr>
            </table>
            </div>
            <div id="grid">
                <asp:GridView ID="grdSchedule" runat="server" BackColor="Window" AutoGenerateColumns="False" DataSourceID="odsOutbound" AllowSorting="true" >
                    <HeaderStyle ForeColor="#ffffff" />
                    <Columns>
                        <asp:BoundField DataField="ID" Visible="false" />
                        <asp:BoundField DataField="Created" Visible="false" />
                        <asp:BoundField DataField="CreateUserID" Visible="false" />
                        <asp:BoundField DataField="ScheduleDate" HeaderText="Date" ItemStyle-Width="70px"  ItemStyle-Wrap="false" SortExpression="ScheduleDate" HtmlEncode="true" DataFormatString="{0:MM/dd/yyyy}" />
                        <asp:BoundField DataField="CarrierName" Visible="false" />
                        <asp:BoundField DataField="DriverName" HeaderText="Driver" ItemStyle-Width="125px" ItemStyle-Wrap="false" SortExpression="DriverName" />
                        <asp:BoundField DataField="ScheduledDeparture" HeaderText="Sch Depart" ItemStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:hh:mm tt}" SortExpression="ScheduledDeparture" />
                        <asp:BoundField DataField="ActualDeparture" HeaderText="Act Depart" ItemStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:hh:mm tt}" SortExpression="ActualDeparture" />
                        <asp:BoundField DataField="Origin" HeaderText="Origin" ItemStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="Origin" />
                        <asp:BoundField DataField="OriginLocation" Visible="false" />
                        <asp:BoundField DataField="Destination" HeaderText="Destination" ItemStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="Destination" />
                        <asp:BoundField DataField="DestinationLocation" Visible="false" />
                        <asp:BoundField DataField="ScheduledArrival" HeaderText="Sch Arrival" ItemStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:hh:mm tt}" SortExpression="ScheduledArrival" />
                        <asp:BoundField DataField="ActualArrival" HeaderText="Act Arrival" ItemStyle-Width="60px" ItemStyle-Wrap="false" HtmlEncode="true" DataFormatString="{0:hh:mm tt}" SortExpression="ActualArrival" />
                        <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" ItemStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="TrailerNumber" />
                        <asp:BoundField DataField="Confirmed" Visible="false" />
                        <asp:BoundField DataField="Amount" HeaderText="Amt" ItemStyle-Width="40px" ItemStyle-Wrap="false" SortExpression="Amount" />
                        <asp:BoundField DataField="AmountType" HeaderText="Type" ItemStyle-Width="50px" ItemStyle-Wrap="false" SortExpression="AmountType" />
                        <asp:BoundField DataField="FreightType" Visible="false" />
                        <asp:BoundField DataField="DropEmptyTrailerNumber" Visible="false" />
                        <asp:BoundField DataField="Comments" HeaderText="Comments" ItemStyle-Width="150px" ItemStyle-Wrap="true" SortExpression="Comments" />
                        <asp:BoundField DataField="IsTemplate" Visible="false" />
                        <asp:BoundField DataField="CancelledUserID" Visible="false" />
                        <asp:BoundField DataField="Cancelled" Visible="false" />
                        <asp:BoundField DataField="LastUpdated" Visible="false" />
                        <asp:BoundField DataField="UserID" Visible="false" />
                        <asp:BoundField DataField="RowVersion" Visible="false" />
                    </Columns>
                </asp:GridView>
                <asp:ObjectDataSource ID="odsOutbound" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewOutboundSchedule" >
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


