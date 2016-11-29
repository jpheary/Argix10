<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewFreight.aspx.cs" Inherits="ViewFreight" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <script type="text/javascript">
        function toggleConfirm(btn) {
            var id1 = btn.id.toString();
            var id2 = id1.replace("btnUnassign", "btnConfirm");
            var btn2 = document.getElementById(id2);
            if (btn.value == "Unassign") {
                btn.value = "Cancel";
                btn2.style.visibility = "visible";
            }
            else {
                btn.value = "Unassign";
                btn2.style.visibility = "hidden";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwShipments" runat="server">
        <div class="pageHeader">
            Terminal&nbsp;
            <asp:DropDownList ID="cboTerminal" runat="server" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" AutoPostBack="True" ToolTip="Local Terminals" style="width:150px" OnSelectedIndexChanged="OnTerminalChanged" />
            <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetTerminals" />
            &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnClick="OnRefresh" />
            <div class="pageMenu">
		        <ul>
			        <li id="liRegular" runat="server"><asp:ImageButton ID="btnRegular" runat="server" ImageUrl="~/App_Themes/Argix/Images/regular.png" OnCommand="OnChangeView" CommandName="Regular" /></li>
			        <li id="liReturns" runat="server"><asp:ImageButton ID="btnReturns" runat="server" ImageUrl="~/App_Themes/Argix/Images/returns.png" OnCommand="OnChangeView" CommandName="Returns" /></li>
		        </ul>
	        </div>
            <br /><br />
        </div>
        <div class="pageBody">
            <asp:ListView ID="lsvShipments" runat="server" DataSourceID="odsShipments">
                <LayoutTemplate>
                    <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                </LayoutTemplate>
                <ItemTemplate>
                    <table style="width:100%; background-color:#ffffff">
                        <tr><td style="width:125px; text-align:left; font-weight:bold">TDS#&nbsp;<%# Eval("TDSNumber")%></td><td style="text-align:right;"><%# Eval("Status")%></td>
                            <td rowspan="3" style="width:25px"><asp:ImageButton runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="Shipment" CommandArgument='<%# Eval("FreightID") %>' OnCommand="OnChangeView" /></td>
                        </tr>
                        <tr><td style="text-align:left;">Trailer#&nbsp;<%# Eval("TrailerNumber")%></td><td style="text-align:right;"><%# GetItemCount(Eval("Cartons"),Eval("Pallets"))%></td></tr>
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
            <asp:ObjectDataSource ID="odsShipments" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetInboundFreight">
                <SelectParameters>
                    <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                    <asp:Parameter Name="freightType" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </div>
    </asp:View>
    <asp:View ID="vwShipment" runat="server">
        <div class="pageHeader">
            <asp:Button ID="btnShipments" runat="server" Text="<< Back" style="width:75px; height:20px; border:1px solid black" OnCommand="OnChangeView" CommandName="Shipments" />
            <div class="pageMenu">
		        <ul>
			        <li id="liFreight" runat="server"><asp:ImageButton ID="btnFreight" runat="server" ImageUrl="~/App_Themes/Argix/Images/freight.png" OnCommand="OnChangeView" CommandName="Freight" /></li>
			        <li id="liAssignments" runat="server"><asp:ImageButton ID="btnAssignments" runat="server" ImageUrl="~/App_Themes/Argix/Images/assignments.png" OnCommand="OnChangeView" CommandName="Assignments" /></li>
		        </ul>
	        </div>
        </div>
        <div class="pageBody">
            <asp:MultiView ID="mvShipment" runat="server" ActiveViewIndex="0">
                <asp:View ID="vwDetail" runat="server">
                    <asp:DetailsView ID="dvShipment" runat="server" Width="100%" DataSourceID="odsShipment" AutoGenerateRows="false" BorderStyle="None">
                        <Fields>
                            <asp:BoundField DataField="TDSNumber" HeaderText="TDS#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="VendorKey" HeaderText="Vendor Key" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="FreightID" HeaderText="FreightID" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" Visible="true" />
                            <asp:BoundField DataField="FreightType" HeaderText="Freight Type" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="IsSortable" HeaderText="Sortable" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="CurrentLocation" HeaderText="Location" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />
                            <asp:BoundField DataField="UnloadedStatus" HeaderText="Unloaded Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="FloorStatus" HeaderText="Floor Status" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField HeaderText="" />
                            <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="96px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="ClientName" HeaderText="Client Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="ShipperNumber" HeaderText="Shipper#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="ShipperName" HeaderText="Shipper Name" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField HeaderText="" />
                            <asp:BoundField DataField="Pickup" HeaderText="Pickup" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />      
                            <asp:BoundField DataField="ReceiveDate" HeaderText="Receive Date" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />      
                            <asp:BoundField DataField="CarrierNumber" HeaderText="Carrier#" HeaderStyle-Width="96px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="DriverNumber" HeaderText="Driver#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="TrailerNumber" HeaderText="Trailer#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="SealNumber" HeaderText="Seal#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="StorageTrailerNumber" HeaderText="Storage Trailer#" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                            <asp:BoundField DataField="TerminalID" HeaderText="TerminalID" HeaderStyle-Width="96px" HeaderStyle-HorizontalAlign="Left" ItemStyle-HorizontalAlign="Left" />      
                        </Fields>
                    </asp:DetailsView>
                    <asp:ObjectDataSource ID="odsShipment" runat="server" SelectMethod="GetInboundShipment" TypeName="Argix.Freight.TsortGateway">
                        <SelectParameters>
                            <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                            <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
                <asp:View ID="vwAssignments" runat="server">
                    <a class="assignMenu" href="javascript: unhide('assign');">Add Assignment</a>
                    <div id="assign" class="hidden">
                        <div id="assignment">
                            <table style="width:280px; background-color:#ffffff">
                                <tr><td style="width:50px; text-align:right">Type:&nbsp;</td>
                                    <td style="width:230px"><asp:DropDownList ID="cboSortType" runat="server" DataSourceID="odsSortTypes" DatatextField="Description" DataValueField="ID" ToolTip="" style="width:215px" /></td>
                                </tr>
                                <tr>
                                    <td style="width:50px; text-align:right">Station:&nbsp;</td>
                                    <td style="width:230px">
                                        <asp:DropDownList ID="cboStation" runat="server" DataSourceID="odsStations" DatatextField="Description" DataValueField="WorkstationID" ToolTip="" style="width:150px" />
                                        &nbsp;<asp:Button ID="btnAssign" runat="server" Text="Assign" style="width:60px; Height:20px; border:1px solid #000000" CommandName="Add" OnCommand="OnAssignment" />
                                    </td>
                                </tr>
                                <tr><td colspan="2"><hr /></td></tr>
                            </table>
                            <asp:ObjectDataSource ID="odsSortTypes" runat="server" SelectMethod="GetFreightSortTypes" TypeName="Argix.Freight.TsortGateway" >
                                <SelectParameters>
                                    <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsStations" runat="server" SelectMethod="GetAssignableSortStations" TypeName="Argix.Freight.TsortGateway" >
                                <SelectParameters>
                                    <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                                    <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                                    <asp:ControlParameter Name="sortTypeID" ControlID="cboSortType" PropertyName="SelectedValue" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                    </div>
                    <asp:ListView ID="lsvAssignments" runat="server" DataSourceID="odsAssignments">
                        <LayoutTemplate>
                            <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                        </LayoutTemplate>
                        <ItemTemplate>
                            <table style="width:100%; background-color:White">
                                <tr><td style="width:125px; text-align:left; font-weight:bold">Station#&nbsp;<%# Eval("StationNumber")%></td><td style="text-align:right;"><%# Eval("SortType")%></td>
                                    <td rowspan="2" style="width:25px"></td>
                                </tr>
                                <tr>
                                    <td><asp:Button ID="btnUnassign" runat="server" Text="Unassign" UseSubmitBehavior="false" OnClientClick="toggleConfirm(this);//" style="width:75px; Height:20px; border:1px solid #000000" /></td>
                                    <td style="text-align:right"><asp:Button ID="btnConfirm" runat="server" Text="Confirm" style="width:75px; Height:20px; border:1px solid #000000; visibility:hidden" CommandName="Unassign" CommandArgument='<%# GetStationAssignment(Eval("WorkstationID"),Eval("FreightID"),Eval("SortTypeID"))%>' OnCommand="OnAssignment" /></td>
                                </tr>
                                <tr><td colspan="3"><hr /></td></tr>
                            </table>
                        </ItemTemplate>
                        <EmptyDataTemplate>
                            <table style="width:100%; background-color:White">
                                <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
                            </table>
                        </EmptyDataTemplate>
                    </asp:ListView>
                    <asp:ObjectDataSource ID="odsAssignments" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetStationAssignments">
                        <SelectParameters>
                            <asp:ControlParameter Name="terminalID" ControlID="cboTerminal" PropertyName="SelectedValue" Type="Int32" />
                            <asp:Parameter Name="freightID" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </asp:View>
            </asp:MultiView>
        </div>
    </asp:View>
    </asp:MultiView>
    <script type="text/javascript">
        function showTab(id) {
            var item = document.getElementById(id).style.borderBottomStyle = "none";
            if (item) item.className = 'on';
        }
    </script>
</asp:Content>