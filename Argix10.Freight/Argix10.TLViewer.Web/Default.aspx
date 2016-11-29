<%@ Page Title="TLViewer" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="Default" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div id="view">
    <table>
        <tr>
            <td>
    <div class="totalview">
        <asp:UpdatePanel ID="upnlTotals" runat="server" UpdateMode="Conditional" ChildrenAsTriggers="false">
        <ContentTemplate>
            <table id="tblTotals" style="width: 100%;">
                <tr><td class="Header" style="width: 120px"># of TLs</td><td style="width: 72px; border-style: solid; border-width: thin; text-align: right"><div id="TotalTLs">0</div></td></tr>
                <tr><td class="Label">Cartons</td><td class="Data"><div id="TotalCartons">0</div></td></tr>
                <tr><td class="Label">Pallets</td><td class="Data"><div id="TotalPallets">0</div></td></tr>
                <tr><td class="Label">Weight (lbs)</td><td class="Data"><div id="TotalWeight">0</div></td></tr>
                <tr><td class="Label">Cube (in3)</td><td class="Data"><div id="TotalCubeFt">0</div></td></tr>
                <tr><td colspan="2" style="font-size: 3px;">&nbsp;</td></tr>
                <tr><td colspan="2" class="Header"> + ISA</td></tr>
                <tr><td class="Label">Weight (lbs)</td><td><input class="InputData" id="ISAWeight" type="text" value="0" style="width: 72px" /></td></tr>
                <tr><td class="Label">Cube (in3)</td><td class="Data"><div id="ISACubeFt">0</div></td></tr>
                <tr><td colspan="2" style="font-size: 3px;"><hr /></td></tr>
                <tr><td colspan="2" class="Header"> = Total</td></tr>
                <tr><td class="Label">Weight (lbs)</td><td class="GrandData"><div id="GrandWeight">0</div></td></tr>
                <tr><td class="Label">Cube (in3)</td><td class="GrandData"><div id="GrandCubeFt">0</div></td></tr>
                <tr><td colspan="2" style="font-size: 3px;"><hr /><br /><hr /></td></tr>
                <tr><td colspan="2" class="Header">Trailer Load% (53ft)</td></tr>
                <tr><td class="Label">Weight (lbs)</td><td class="Data"><div id="WeightPercent">0</div></td></tr>
                <tr><td class="Label">Cube (in3)</td><td class="Data"><div id="CubePercent">0</div></td></tr>
                <tr><td colspan="2">&nbsp;</td></tr>
            </table>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            </td>
            <td>
    <div class="tlview">
        <asp:UpdatePanel ID="upnlTLs" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
        <asp:GridView ID="grdTLs" runat="server" Width="100%" AutoGenerateColumns="False" DataSourceID="odsTLs" EnableTheming="True" AllowSorting="True" OnRowDataBound="OnRowDataBound" OnSorted="OnSorted">
            <Columns>
                <asp:BoundField DataField="TerminalID" HeaderText="Terminal" HeaderStyle-Width="50px" ItemStyle-Wrap="False" Visible="False" />
                <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="75px" ItemStyle-Wrap="False" SortExpression="TLNumber" Visible="False" />
                <asp:TemplateField HeaderText="TL#" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" SortExpression="TLNumber"><ItemTemplate><a href="" onclick="javascript:var w=window.showModelessDialog('TLDetail.aspx?location=<%# Eval("TerminalID") %>&tl=<%# Eval("TLNumber") %>','','dialogWidth:800px;dialogHeight:200px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;"><%# Eval("TLNumber")%></a></ItemTemplate></asp:TemplateField>
                <asp:BoundField DataField="TLDate" HeaderText="TL Date" HeaderStyle-Width="75px" ItemStyle-Wrap="False" SortExpression="TLDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="CloseNumber" />
                <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="AgentNumber" />
                <asp:BoundField DataField="ClientNumber" HeaderText="Client#" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="ClientNumber" />
                <asp:BoundField DataField="ClientName" HeaderText="Client" HeaderStyle-Width="150px" ItemStyle-Wrap="True" SortExpression="ClientName" />
                <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Zone" />
                <asp:BoundField DataField="Lane" HeaderText="Lane" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Lane" />
                <asp:BoundField DataField="SmallLane" HeaderText="SmLane" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="SmallLane" />
                <asp:BoundField DataField="Cartons" HeaderText="Ctns" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Cartons" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="Pallets" HeaderText="Pllts" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Pallets" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Weight" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="Cube" HeaderText="Cube" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="False" SortExpression="Cube" DataFormatString="{0:N0}" />
                <asp:BoundField DataField="WeightPercent" HeaderText="Weight%" HeaderStyle-Width="50px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" Visible="false" />
                <asp:BoundField DataField="CubePercent" HeaderText="Cube%" HeaderStyle-Width="50px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" Visible="false" />
            </Columns>
        </asp:GridView>
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
            </td>
        </tr>
    </table>
</div>
<asp:ObjectDataSource ID="odsTLs" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="GetTLView" SortParameterName="sortBy">
    <SelectParameters>
        <asp:Parameter Name="terminalID" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>
