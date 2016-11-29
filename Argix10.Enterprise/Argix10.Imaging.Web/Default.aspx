<%@ Page Language="C#" masterpagefile="~/Imaging.master" AutoEventWireup="true"  CodeFile="Default.aspx.cs" Inherits="_Default" Title="SharePoint.Search" %>
<%@ MasterType VirtualPath="~/Imaging.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <table style="width:900px">
        <tr style="font-size:1px"><td style="width:150px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td>Search >>>>>>></td>
            <td>
                <table>
                    <tr style="height:25px">
                        <td style="width:200px; text-align:center"><asp:HyperLink ID="lnkFinance" runat="server" NavigateUrl="~/financeimages.aspx">Finance Images</asp:HyperLink></td>
                        <td style="width:200px; text-align:center"><asp:HyperLink ID="lnkHR" runat="server" NavigateUrl="~/hrimages.aspx">HR Images</asp:HyperLink></td>
                        <td style="width:200px; text-align:center"><asp:HyperLink ID="lnkTsort" runat="server" NavigateUrl="~/tsortimages.aspx">Tsort Images</asp:HyperLink></td>
                   </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:50px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td style="vertical-align:top">Site Config Info</td><td>&nbsp;<asp:TextBox ID="txtSearchInfo" runat="server" Width="575px" Height="75px" TextMode="MultiLine" Wrap="true" Text="" ReadOnly="true"></asp:TextBox></td></tr>
        <tr><td>&nbsp;</td><td>&nbsp;</td></tr>
        <tr><td style="vertical-align:top">Search MetaData</td><td>&nbsp;<asp:TextBox ID="txtMetaData" runat="server" Width="575px" Height="275px" TextMode="MultiLine" Wrap="true" Text="" ReadOnly="true"></asp:TextBox></td></tr>
    </table>
</asp:Content>