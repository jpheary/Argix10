<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewZones.aspx.cs" Inherits="ViewZones" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwZones" runat="server">
        <div class="pageHeader">
            Terminal&nbsp;
            <asp:DropDownList ID="cboTerminal" runat="server" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" AutoPostBack="True" ToolTip="Local Terminals" style="width:150px" OnSelectedIndexChanged="OnTerminalChanged" />
            <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetTerminals" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnClick="OnRefresh" />
            <div class="pageMenu">
		        <ul>
			        <li style="border-bottom-style:none"><asp:ImageButton ID="imgZones" runat="server" ImageUrl="~/App_Themes/Argix/Images/opentls.png" /></li>
			        <li style="border-top-style:none; border-right-style:none; border-left-style:none">&nbsp;</li>
		        </ul>
	        </div>
            <br /><br />
        </div>
        <div class="pageBody">
            <asp:ListView ID="lsvZones" runat="server" DataSourceID="odsZones">
                <LayoutTemplate>
                    <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                </LayoutTemplate>
                <ItemTemplate>
                        <table style="width:100%; background-color:#ffffff">
                            <tr><td style="width:125px; text-align:left; font-weight:bold">Zone&nbsp;<%# Eval("Zone")%></td><td style="text-align:right;"><%# Eval("Type")%></td>
                                <td rowspan="3" style="width:25px"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="Zone" CommandArgument='<%# Eval("Zone") %>' OnCommand="OnChangeView" /></td>
                            </tr>
                            <tr><td style="text-align:left;">TL#&nbsp;<%# GetTL(Eval("TL#"),Eval("CloseNumber"))%></td><td style="text-align:right;"><%# Eval("AssignedToShipScde")%></td></tr>
                            <tr><td colspan="2" style="text-align:left;"><%# GetClientInfo(Eval("ClientNumber"),Eval("ClientName"))%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table style="width:100%; background-color:White">
                        <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="odsZones" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetZones">
                <SelectParameters>
                    <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:View>
    <asp:View ID="vwZone" runat="server">
        <div class="pageHeader">
            <asp:Button ID="btnZones" runat="server" Text="<< Back" style="width:75px; height:20px; border:1px solid black" OnCommand="OnChangeView" CommandName="Zones" />
            <div class="pageMenu">
		        <ul>
			        <li style="border-bottom-style:none"><asp:ImageButton ID="btnZone" runat="server" ImageUrl="~/App_Themes/Argix/Images/zonedetail.png" /></li>
			        <li style="border-top-style:none; border-right-style:none; border-left-style:none">&nbsp;</li>
		        </ul>
	        </div>
        </div>
        <div class="pageBody">
            <asp:DetailsView ID="dvZone" runat="server" Width="100%" DataSourceID="odsZone" AutoGenerateRows="false" BorderStyle="None">
                <Fields>
                    <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="TL#" HeaderText="TL#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="ClientName" HeaderText="Client" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="TLDate" HeaderText="TLDate" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="" />
                    <asp:BoundField DataField="Lane" HeaderText="Lane" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="SmallSortLane" HeaderText="SmallSortLane" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="TypeID" HeaderText="TypeID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Description" HeaderText="Description" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="RollbackTL#" HeaderText="RollbackTL#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="IsExclusive" HeaderText="IsExclusive" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="CAN_BE_CLOSED" HeaderText="CAN_BE_CLOSED" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField HeaderText="" />
                    <asp:BoundField DataField="AssignedToShipScde" HeaderText="AssignedToShipScde" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="AgentTerminalID" HeaderText="AgentTerminalID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                    <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                </Fields>
            </asp:DetailsView>
            <asp:ObjectDataSource ID="odsZone" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetZone">
                <SelectParameters>
                    <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                    <asp:Parameter Name="zoneCode" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:View>
    </asp:MultiView>
</asp:Content>