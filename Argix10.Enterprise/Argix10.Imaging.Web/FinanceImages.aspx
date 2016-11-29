<%@ page language="C#" masterpagefile="~/Imaging.master" autoeventwireup="true" CodeFile="FinanceImages.aspx.cs" inherits="_FinanceImages" title="Finance Images Search 2.0.0" %>
<%@ MasterType VirtualPath="~/Imaging.master" %>

<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:UpdatePanel ID="upnlPage" runat="server" RenderMode="Block" UpdateMode="Conditional" ChildrenAsTriggers="true">
    <ContentTemplate>
        <table style="width:100%">
            <tr style="font-size:1px"><td style="width:125px">&nbsp;</td><td style="width:75px">&nbsp;</td></tr>
            <tr><td colspan="2" class="subtitle">Finance Images</td></tr>
            <tr>
                <td style="text-align:right">Document Class&nbsp;</td>
                <td><asp:DropDownList ID="cboDocClass" runat="server" DataSourceID="odsDocs" DataTextField="ClassName" DataValueField="ClassName" Width="131px" AutoPostBack="True" OnSelectedIndexChanged="OnDocClassChanged" />
                    &nbsp;&nbsp;&nbsp;Use % for prefix/postfix multiple character wild card; use _ for single character wildcard. (e.g. %12_4%).
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:5px">&nbsp;</td></tr>
            <tr>
                <td style="text-align:right">Search Criteria&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboProp1" runat="server" DataSourceID="odsMetaData" DataTextField="Property" DataValueField="Property" Width="96px"></asp:DropDownList>
                    <asp:TextBox ID="txtSearch1" runat="server" MaxLength="30" />
                    <asp:DropDownList ID="cboOperand1" runat="server" Width="75px" ><asp:ListItem Text="AND" Value="AND" /><asp:ListItem Text="OR" Value="OR" /></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:DropDownList ID="cboProp2" runat="server" DataSourceID="odsMetaData" DataTextField="Property" DataValueField="Property" Width="96px"></asp:DropDownList>
                    <asp:TextBox ID="txtSearch2" runat="server" MaxLength="30" />
                </td>
            </tr>        
            <tr><td colspan="2" style="font-size:1px; height:10px">&nbsp;</td></tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="OnSearchClicked" />
                    &nbsp;<asp:RequiredFieldValidator ID="rfvSearch" runat="server" ControlToValidate="txtSearch1" ErrorMessage="Please enter search text in the first text box."></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr><td colspan="2" style="font-size:1px; height:10px">&nbsp;</td></tr>
            <tr>
                <td colspan="2">
                    <asp:Panel ID="pnlImages" runat="server" Width="900px" Height="250px" ScrollBars="Auto">
                        <asp:GridView ID="grdImages" runat="server" Width="100%" Height="100%" AutoGenerateColumns="False" AllowSorting="True" EmptyDataText="No images found." OnSorting="OnGridSorting" OnSorted="OnGridSorted">
                        </asp:GridView>
                    </asp:Panel>
                </td>
            </tr>
     </table>
    </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdateProgress ID="uprgPage" runat="server" AssociatedUpdatePanelID="upnlPage">
        <ProgressTemplate>Searching for images...</ProgressTemplate>
    </asp:UpdateProgress>
    <asp:ObjectDataSource ID="odsDocs" runat="server" TypeName="Argix.Enterprise.ImagingGateway" SelectMethod="GetDocumentClasses" CacheExpirationPolicy="Sliding" CacheDuration="900" EnableCaching="true">
        <SelectParameters>
            <asp:Parameter Name="Department" DefaultValue="Finance" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsMetaData" runat="server" TypeName="Argix.Enterprise.ImagingGateway" SelectMethod="GetMetaData">
        <SelectParameters>
            <asp:ControlParameter Name="className" ControlID="cboDocClass" PropertyName="SelectedValue" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>