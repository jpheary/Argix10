<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ViewIssues.aspx.cs" Inherits="ViewIssues" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<div class="pageHeader">
        Terminal&nbsp;
        <asp:DropDownList ID="cboTerminal" runat="server" AppendDataBoundItems="true" DataSourceID="odsTerminals" DatatextField="Description" DataValueField="AgentID" ToolTip="Local Terminals" style="width:150px">
            <asp:ListItem Text="All" Value="" Selected="True" />
        </asp:DropDownList>
        <asp:ObjectDataSource ID="odsTerminals" runat="server" SelectMethod="GetTerminals" TypeName="Argix.Customers.CustomersGateway" CacheExpirationPolicy="Absolute" CacheDuration="900" EnableCaching="true" />
        &nbsp;&nbsp;&nbsp;<asp:Button ID="btnRefresh" runat="server" Text="Refresh" CssClass="submit" OnCommand="OnOnCommand" CommandName="Refresh" />
</div>
<div class="pageBody">
    <asp:ListView ID="lsvIssues" runat="server" DataSourceID="odsIssues">
        <LayoutTemplate>
            <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
        </LayoutTemplate>
        <ItemTemplate>
                <table style="width:100%; background-color:#ffffff">
                    <tr><td style="width:150px; text-align:left; font-weight:bold"><%# Eval("LastActionUserID")%></td><td style="text-align:right;"><%# GetDateInfo(Eval("LastActionCreated"))%></td>
                        <td rowspan="3" style="width:24px"><asp:ImageButton runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" CommandName="View" CommandArgument='<%# Eval("ID") %>' OnCommand="OnOnCommand" /></td></tr>
                    <tr><td><%# Eval("Type") %></td><td style="text-align:right;"><%# Eval("LastActionDescription")%></td></tr>
                    <tr><td colspan="2" style="text-align:left;"><%# GetCompanyInfo(Eval("CompanyName"),Eval("StoreNumber"),Eval("AgentNumber"))%></td></tr>
                    <tr><td colspan="3"><hr /></td></tr>
                </table>
        </ItemTemplate>
        <EmptyDataTemplate>
            <table style="width:100%; background-color:#ffffff">
                <tr style="background-color:white; height:48px"><td>&nbsp;</td></tr>
            </table>
        </EmptyDataTemplate>
    </asp:ListView>
    <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.CustomersGateway" SelectMethod="ViewIssues">
        <SelectParameters>
            <asp:ControlParameter Name="agentNumber" ControlID="cboTerminal" PropertyName="SelectedValue" DefaultValue="" ConvertEmptyStringToNull="true" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
</asp:Content>