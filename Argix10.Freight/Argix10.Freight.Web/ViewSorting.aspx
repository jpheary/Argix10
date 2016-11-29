<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewSorting.aspx.cs" Inherits="ViewSorting" %>
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
    <div class="pageHeader">
        Terminal&nbsp;
        <asp:DropDownList ID="cboTerminal" runat="server" DataSourceID="odsTerminals" DataTextField="Description" DataValueField="Number" AutoPostBack="True" ToolTip="Local Terminals" style="width:150px" OnSelectedIndexChanged="OnTerminalChanged" />
        <asp:ObjectDataSource ID="odsTerminals" runat="server" TypeName="Argix.Freight.TsortGateway" SelectMethod="GetTerminals" />
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnClick="OnRefresh" />
        <div class="pageMenu">
		    <ul>
			    <li style="border-bottom-style:none"><asp:ImageButton ID="btnAssignments" runat="server" ImageUrl="~/App_Themes/Argix/Images/assignments.png" /></li>
			    <li style="border-top-style:none; border-right-style:none; border-left-style:none">&nbsp;</li>
		    </ul>
	    </div>
        <br /><br />
    </div>
    <div class="pageBody">
        <asp:ListView ID="lsvAssignments" runat="server" DataSourceID="odsAssignments">
            <LayoutTemplate>
                <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
            </LayoutTemplate>
            <ItemTemplate>
                    <table style="width:100%; background-color:White">
                        <tr><td style="width:125px; text-align:left; font-weight:bold">Station#&nbsp;<%# Eval("StationNumber")%></td><td style="text-align:right;"><%# Eval("SortType")%></td></tr>
                        <tr><td style="text-align:left;">TDS#&nbsp;<%# Eval("TDSNumber")%></td><td style="text-align:right;">Trailer#&nbsp;<%# Eval("TrailerNumber")%></td></tr>
                        <tr><td colspan="2" style="text-align:left;"><%# Eval("Client")%></td></tr>
                        <tr><td colspan="2" style="text-align:left;"><%# Eval("Shipper")%></td></tr>
                        <tr>
                            <td><asp:Button ID="btnUnassign" runat="server" Text="Unassign" UseSubmitBehavior="false" OnClientClick="toggleConfirm(this);//" style="width:75px; Height:20px; border:1px solid #000000" /></td>
                            <td style="text-align:right"><asp:Button ID="btnConfirm" runat="server" Text="Confirm" style="width:75px; Height:20px; border:1px solid #000000; visibility:hidden" CommandName="Unassign" CommandArgument='<%# GetStationAssignment(Eval("WorkstationID"),Eval("FreightID"),Eval("SortTypeID"))%>' OnCommand="OnAssignment" /></td>
                        </tr>
                        <tr><td colspan="2"><hr /></td></tr>
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
    </div>
</asp:Content>