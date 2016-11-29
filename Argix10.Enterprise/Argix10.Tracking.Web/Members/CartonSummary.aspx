<%@ Page Title="Carton Summary" Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="CartonSummary.aspx.cs" Inherits="_CartonSummary" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cBody" ContentPlaceHolderID="cpContent" Runat="Server">
    <div class="subtitle">Carton Summary</div>
    <div class="trackresponse">
        <div class="gridviewtitle"><asp:Label ID="lblTitle" runat="server" Text="" /></div>
        <div class="gridviewbody">
            <asp:GridView ID="grdTrack" runat="server" Width="100%" AutoGenerateColumns="False" DataKeyNames="LabelNumber" AllowSorting="true" OnRowDataBound="OnItemDataBound">
                <Columns>
                    <asp:HyperLinkField DataTextField="ItemNumber" HeaderText="Tracking Number" HeaderStyle-Width="150px" ItemStyle-Font-Bold="true" DataNavigateUrlFields="LabelNumber" DataNavigateUrlFormatString="CartonDetail.aspx?item={0}" NavigateUrl="~/CartonDetail.aspx" />
                    <asp:BoundField DataField="DateTime" HeaderText="Date" HeaderStyle-Width="125px" DataFormatString="{0:MM-dd-yyyy}" HtmlEncode="False" />
                    <asp:BoundField DataField="Status" HeaderText="Status" HeaderStyle-Width="150px" />
                    <asp:BoundField DataField="Location" HeaderText="Location" HeaderStyle-Width="200px" />
                    <asp:BoundField DataField="CBOL" HeaderText="CBOL" SortExpression="CBOL" />
                </Columns>
                <HeaderStyle Font-Size="10pt" />
            </asp:GridView>
        </div>
	 </div>
</asp:Content>

