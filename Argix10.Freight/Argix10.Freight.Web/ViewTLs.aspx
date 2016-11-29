<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewTLs.aspx.cs" Inherits="ViewTLs" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwTLs" runat="server">
        <div class="pageHeader">
            Terminal&nbsp;
            <asp:DropDownList ID="cboTerminal" runat="server" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" AutoPostBack="True" ToolTip="Local Terminals" style="width:150px" OnSelectedIndexChanged="OnTerminalChanged" />
            <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetTerminals" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnClick="OnRefresh" />
            <div class="pageMenu">
		        <ul>
					<li id="liTLs" runat="server"><asp:ImageButton ID="imgTLs" runat="server" ImageUrl="~/App_Themes/Argix/Images/activetls.png" OnCommand="OnChangeView" CommandName="TLView" /></li>
			        <li id="liAgents" runat="server"><asp:ImageButton ID="imgAgents" runat="server" ImageUrl="~/App_Themes/Argix/Images/agentsums.png" OnCommand="OnChangeView" CommandName="AgentView" /></li>
		        </ul>
	        </div>
            <br /><br />
        </div>
        <div class="pageBody">
            <asp:MultiView ID="mvTLs" runat="server" ActiveViewIndex="0">
            <asp:View ID="vwTLView" runat="server">
                <asp:ListView ID="lsvTLs" runat="server" DataSourceID="odsTLs">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table style="width:100%; background-color:#ffffff">
                            <tr><td style="width:125px; text-align:left; font-weight:bold">TL#&nbsp;<%# GetTL(Eval("TLNumber"),Eval("CloseNumber"))%></td><td style="text-align:right;"><%# GetDate(Eval("TLDate"))%></td>
                                <td rowspan="3" style="width:25px"><asp:ImageButton runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="TL" CommandArgument='<%# Eval("TLNumber") %>' OnCommand="OnChangeView" /></td>
                            </tr>
                            <tr><td style="text-align:left;">Zone:&nbsp;<%# Eval("Zone")%></td><td style="text-align:right;">Lane:&nbsp;<%# Eval("Lane")%></td></tr>
                            <tr><td style="text-align:left;"><%# GetClientInfo(Eval("ClientNumber"),Eval("ClientName"))%></td><td style="text-align:right;">Sm. Lane:&nbsp;<%# Eval("SmallLane")%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table style="width:100%; background-color:White">
                            <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="odsTLs" runat="server" TypeName="Argix.Shipping.TLViewerGateway" SelectMethod="GetTLView">
                    <SelectParameters>
                        <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:View>
            <asp:View ID="vwAgentView" runat="server">
                <asp:ListView ID="lsvAgents" runat="server" DataSourceID="odsAgents">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                        <table style="width:100%; background-color:#ffffff">
                            <tr><td style="width:125px; text-align:left; font-weight:bold"><%# GetAgentInfo(Eval("AgentNumber"),Eval("AgentName"))%></td><td style="text-align:right;">&nbsp;</td>
                                <td rowspan="3" style="width:25px">&nbsp;</td>
                            </tr>
                            <tr><td style="text-align:left;">Zone:&nbsp;<%# Eval("Zone")%></td><td style="text-align:right;">&nbsp;</td></tr>
                            <tr><td style="text-align:left;"><%# GetFreightCounts(Eval("Cartons"),Eval("Pallets"))%></td><td style="text-align:left;"><%# GetItemSpecs(Eval("Weight"),Eval("Cube"))%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                    </ItemTemplate>
                    <EmptyDataTemplate>
                        <table style="width:100%; background-color:White">
                            <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                        </table>
                    </EmptyDataTemplate>
                </asp:ListView>
                <asp:ObjectDataSource ID="odsAgents" runat="server" TypeName="Argix.Shipping.TLViewerGateway" SelectMethod="GetAgentSummary">
                    <SelectParameters>
                        <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </asp:View>
            </asp:MultiView> 
        </div>
    </asp:View>
    <asp:View ID="vwTL" runat="server">
        <div class="pageHeader">
            <asp:Button ID="btnTLs" runat="server" Text="<< Back" style="width:75px; height:20px; border:1px solid black" OnCommand="OnChangeView" CommandName="TLs" />
            <div class="pageMenu">
		        <ul>
			        <li style="border-bottom-style:none"><asp:Label runat="server" ID="lblTL" Text="" /></li>
			        <li style="border-top-style:none; border-right-style:none; border-left-style:none">&nbsp;</li>
		        </ul>
	        </div>
        </div>
        <div class="pageBody">
            <asp:ListView ID="lsvTL" runat="server" DataSourceID="odsTL">
                <LayoutTemplate>
                    <div id="itemPlaceholder" runat="server" style="width:90%;" ></div>
                </LayoutTemplate>
                <ItemTemplate>
                        <table style="width:100%; background-color:#ffffff">
                            <tr><td colspan="2" style="width:125px; text-align:left; font-weight:bold"><%# GetClientInfo(Eval("ClientNumber"),Eval("ClientName"))%></td>
                                <td rowspan="3" style="width:25px">&nbsp;</td>
                            </tr>
                            <tr><td colspan="2" style="text-align:left"><%# GetShipToInfo(Eval("ShipToLocationID"),Eval("ShipToLocationName"))%></td></tr>
                            <tr><td style="text-align:left"><%# GetFreightCounts(Eval("Cartons"),Eval("Pallets"))%></td><td style="text-align:left"><%# GetItemSpecs(Eval("Weight"),Eval("Cube"))%></td></tr>
                            <tr><td colspan="3"><hr /></td></tr>
                        </table>
                </ItemTemplate>
                <EmptyDataTemplate>
                    <table style="width:100%; background-color:White">
                        <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                    </table>
                </EmptyDataTemplate>
            </asp:ListView>
            <asp:ObjectDataSource ID="odsTL" runat="server" SelectMethod="GetTLDetail" TypeName="Argix.Shipping.TLViewerGateway">
                <SelectParameters>
                    <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                    <asp:Parameter Name="tlNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:View>
    </asp:MultiView>
</asp:Content>