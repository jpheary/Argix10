<%@ Page Title="Log In" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Assign.aspx.cs" Inherits="Assign" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Assign</div>
    <div id="assignment">
        <table>
            <tr><td style="width:100px; text-align:right">Type&nbsp;</td>
                <td><asp:DropDownList ID="cboSortType" runat="server" DataSourceID="odsSortTypes" DatatextField="Description" DataValueField="ID" ToolTip="" style="width:300px" /></td>
            </tr>
            <tr><td colspan="2">&nbsp;</td></tr>
            <tr>
                <td style="width:100px; text-align:right">Station&nbsp;</td>
                <td><asp:DropDownList ID="cboStation" runat="server" DataSourceID="odsStations" DatatextField="Description" DataValueField="WorkstationID" ToolTip="" style="width:200px" /></td>
            </tr>
        </table>
        <br />
        <br />
        <div>
            <asp:Button ID="btnAssign" runat="server" Text="Assign" CssClass="submit" CommandName="Assign" OnCommand="OnCommand" />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CommandName="Cancel" OnCommand="OnCommand" />
       </div>
        <asp:ObjectDataSource ID="odsSortTypes" runat="server" SelectMethod="GetFreightSortTypes" TypeName="Argix.Freight.TsortGateway" >
            <SelectParameters>
                <asp:QueryStringParameter Name="terminalID" QueryStringField="terminalID" DefaultValue="0" ConvertEmptyStringToNull="true" Type="Int32" />
                <asp:QueryStringParameter Name="freightID" QueryStringField="freightID" DefaultValue="0" ConvertEmptyStringToNull="true" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsStations" runat="server" SelectMethod="GetAssignableSortStations" TypeName="Argix.Freight.TsortGateway" >
            <SelectParameters>
                <asp:QueryStringParameter Name="terminalID" QueryStringField="terminalID" DefaultValue="0" ConvertEmptyStringToNull="true" Type="Int32" />
                <asp:QueryStringParameter Name="freightID" QueryStringField="freightID" DefaultValue="0" ConvertEmptyStringToNull="true" Type="String" />
                <asp:ControlParameter Name="sortTypeID" ControlID="cboSortType" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>