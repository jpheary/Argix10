<%@ Page Title="Store Detail" Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="StoreDetail.aspx.cs" Inherits="_StoreDetail" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpContent">
    <div class="trackresponse">
        <div class="subtitle">Store Detail</div>
        <p>A list of the cartons contained in the selected load (TL).</p>
        <div class="gridviewtitle"><asp:LinkButton id="lnkStoreSummary" runat="server" CausesValidation="False" PostBackUrl="~/Members/StoreSummary.aspx"><<&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTitle" runat="server" Text="" /></asp:LinkButton></div>
        <div class="gridviewbody">
            <asp:GridView ID="grdTLDetail" runat="server" Width="100%" AutoGenerateColumns="False">
                <Columns>
                    <asp:HyperLinkField DataNavigateUrlFields="CartonNumber,LabelNumber,TL" DataNavigateUrlFormatString="StoreDetail.aspx?CTN={0}&amp;LBL={1}&amp;TL={2}" DataTextField="CartonNumber" HeaderStyle-Width="150px"  HeaderText="Carton Number" Target="_self" />
                    <asp:BoundField DataField="PickupDate" HeaderText="PU Date" HeaderStyle-Width="75px" SortExpression="PickupDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="Weight" HeaderText="Weight" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" />
                    <asp:BoundField DataField="ShipperName" HeaderText="Shipper" HeaderStyle-Width="250px" SortExpression="ShipperName" >
                        <ItemStyle Wrap="False" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="Address" HeaderStyle-Width="150px" Visible="false">
                        <ItemTemplate>
                            <asp:Label ID="Label3" runat="server" Style="position: relative" Text='<%# Eval("ShipperCity") + "," + Eval("ShipperState") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CartonStatus" HeaderText="Status" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" SortExpression="CartonStatus" />
                    <asp:BoundField DataField="ScanStatus" HeaderText="Scan" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Center" SortExpression="ScanStatus" />
                    <asp:TemplateField HeaderText="Scan Date" HeaderStyle-Width="125px">
                        <ItemTemplate>
                            <asp:Label ID="Label2" runat="server" Style="position: relative" Text='<%# GetPODDate(Eval("PodDate"), Eval("PodTime")) %>' />
                        </ItemTemplate>
                        <ItemStyle Wrap="False" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="CBOL" HeaderText="CBOL" SortExpression="CBOL" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
 </asp:Content>


