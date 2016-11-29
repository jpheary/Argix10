<%@ page Title="Store Summary" language="C#" masterpagefile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="StoreSummary.aspx.cs" Inherits="_StoreSummary" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpContent">
    <div class="trackresponse">
        <div class="subtitle">Store Summary</div>
        <p>A summary of the loads (TL) delivered to the store for the selected date range.</p>
        <div class="gridviewtitle"><asp:Label ID="lblTitle" runat="server" Text="" /></div>
        <div class="gridviewbody">
            <asp:GridView ID="grdSummary" runat="server" Width="100%" AutoGenerateColumns="False" AllowSorting="true">
                <Columns>
                    <asp:TemplateField HeaderText="TL" HeaderStyle-Width="75px">
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkTL" runat="server" NavigateUrl='<%# "StoreDetail.aspx?TL=" + HttpUtility.UrlEncode(Eval("TL").ToString()) %>' Text='<%# Eval("TL").ToString() %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="CartonCount" HeaderText="Cartons" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="CBOL" HeaderText="CBOL" SortExpression="CBOL" />
                    <asp:TemplateField HeaderText="ETA/POD" HeaderStyle-Width="125px">
                        <ItemTemplate>
                            <asp:Label ID="lblETAPOD" runat="server" Text='<%# GetOFDorPOD(Eval("OFD1"),Eval("PodDate"),Eval("PodTime"))%>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AgentName" HeaderText="Terminal" HeaderStyle-Width="250px" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
 </asp:Content>

