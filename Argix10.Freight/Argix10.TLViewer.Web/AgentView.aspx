<%@ Page Title="TLViewer" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="AgentView.aspx.cs" Inherits="AgentView" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div id="view">
        <div class="agentview">
            <asp:UpdatePanel runat="server" ID="upnlAgents" UpdateMode="Conditional">
            <ContentTemplate>
            <asp:GridView ID="grdSummary" runat="server" Width="100%" DataSourceID="odsSummary" AutoGenerateColumns="False" EnableTheming="True" AllowSorting="True">
                <Columns>
                    <asp:BoundField DataField="AgentNumber" HeaderText="Agent#" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" ItemStyle-Wrap="False" SortExpression="AgentNumber" />
                    <asp:BoundField DataField="AgentName" HeaderText="Agent" ItemStyle-Wrap="False" SortExpression="AgentName" />
                    <asp:BoundField DataField="Zone" HeaderText="Zone" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="Zone" />
                    <asp:BoundField DataField="TLNumber" HeaderText="TL#" HeaderStyle-Width="75px" ItemStyle-Wrap="False" SortExpression="TLNumber" />
                    <asp:BoundField DataField="TLDate" HeaderText="TL Date" HeaderStyle-Width="75px" ItemStyle-Wrap="False" SortExpression="TLDate" DataFormatString="{0:MMddyy}" />
                    <asp:BoundField DataField="CloseNumber" HeaderText="Close#" HeaderStyle-Width="50px" ItemStyle-Wrap="False" SortExpression="CloseNumber" />
                    <asp:BoundField DataField="Cartons" HeaderText="Cartons" HeaderStyle-Width="50px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="Pallets" HeaderText="Pallets" HeaderStyle-Width="50px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="50px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" />
                    <asp:BoundField DataField="WeightPercent" HeaderText="Weight%" HeaderStyle-Width="50px" ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" />
                    <asp:BoundField DataField="CubePercent" HeaderText="Cube%" HeaderStyle-Width="50px"  ItemStyle-Wrap="False" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:#'%'}" />
                </Columns>
            </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsSummary" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="GetAgentSummary" SortParameterName="sortBy">
        <SelectParameters>
            <asp:Parameter Name="terminalID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
