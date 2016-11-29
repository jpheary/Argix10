<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewSearch.aspx.cs" Inherits="ViewSearch" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <asp:MultiView ID="mvPage" runat="server" ActiveViewIndex="0">
        <asp:View ID="vwSearch" runat="server">
            <div class="pageHeader">Search Issues</div>
            <div class="pageBody">
                <table style="width:100%; height:275px">
                    <tr><td class="label">Zone&nbsp;</td><td><asp:TextBox ID="txtZone" runat="server" style="width:75px" /></td></tr>
                    <tr><td class="label">Store&nbsp;</td><td><asp:TextBox ID="txtStore" runat="server" style="width:100px" /></td></tr>
                    <tr><td class="label">Agent&nbsp;</td><td>
                        <asp:DropDownList ID="cboAgent" runat="server" AppendDataBoundItems="true" DataSourceID="odsAgents" DatatextField="AgentName" DataValueField="AgentName" AutoPostBack="false" style="width:200px">
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                    </td></tr>
                    <tr><td class="label">Company&nbsp;</td><td>
                        <asp:DropDownList ID="cboCompany" runat="server" AppendDataBoundItems="true" DataSourceID="odsCompanies" DatatextField="CompanyName" DataValueField="CompanyName" AutoPostBack="false" style="width:200px">
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                    </td></tr>
                    <tr><td class="label">Type&nbsp;</td><td>
                        <asp:DropDownList ID="cboIssueType" runat="server" AppendDataBoundItems="true" DataSourceID="odsIssueTypes" DatatextField="Type" DataValueField="Type" AutoPostBack="false" style="width:175px">
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                    </td></tr>
                    <tr><td class="label">Action&nbsp;</td><td>
                        <asp:DropDownList ID="cboActionType" runat="server" AppendDataBoundItems="true" DataSourceID="odsActionTypes" DatatextField="Description" DataValueField="Description" AutoPostBack="false" style="width:150px">
                            <asp:ListItem Text="" Value="" Selected="True" />
                        </asp:DropDownList>
                    </td></tr>
                    <tr><td class="label">Received&nbsp;</td><td><asp:TextBox ID="txtReceived" runat="server" style="width:100px" /></td></tr>
                    <tr><td class="label">Subject&nbsp;</td><td><asp:TextBox ID="txtSubject" runat="server" style="width:150px" /></td></tr>
                    <tr><td class="label">Originator&nbsp;</td><td><asp:TextBox ID="txtOriginator" runat="server" style="width:150px" /></td></tr>
                    <tr><td class="label">Coordinator&nbsp;</td><td><asp:TextBox ID="txtCoordinator" runat="server" style="width:150px" /></td></tr>
                    <tr><td colspan="2" style="height:10px">&nbsp;</td></tr>
                    <tr><td class="label">&nbsp;</td><td><asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="submit" OnCommand="OnOnCommand" CommandName="Search" /></td></tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                </table>
            </div>
            <asp:ObjectDataSource ID="odsAgents" runat="server" SelectMethod="GetAgents" TypeName="Argix.Customers.CustomersGateway" />
            <asp:ObjectDataSource ID="odsCompanies" runat="server" SelectMethod="GetCompanies2" TypeName="Argix.Customers.CustomersGateway" />
            <asp:ObjectDataSource ID="odsIssueTypes" runat="server" SelectMethod="GetIssueTypes" TypeName="Argix.Customers.CustomersGateway" >
                <SelectParameters><asp:Parameter Name="category" DefaultValue="" Type="String" /></SelectParameters>
            </asp:ObjectDataSource>
            <asp:ObjectDataSource ID="odsActionTypes" runat="server" SelectMethod="GetActionTypes" TypeName="Argix.Customers.CustomersGateway" >
                <SelectParameters><asp:Parameter Name="issueID" DefaultValue="" ConvertEmptyStringToNull="true" Type="Int64" /></SelectParameters>
            </asp:ObjectDataSource>
        </asp:View>
        <asp:View ID="vwIssues" runat="server">
            <div class="pageHeader">
                <asp:Button ID="btnBack" runat="server" Text="<< Back" style="border-style:none" OnCommand="OnOnCommand" CommandName="Back" />
            </div>
            <div class="pageBody">
                <asp:ListView ID="lsvIssues" runat="server" DataSourceID="odsIssues">
                    <LayoutTemplate>
                        <div id="itemPlaceholder" runat="server" style="width:100%" ></div>
                    </LayoutTemplate>
                    <ItemTemplate>
                            <table style="width:100%; background-color:#ffffff">
                                <tr><td style="width:150px; font-weight:bold"><%# Eval("LastActionUserID")%></td><td style="text-align:right;"><%# GetDateInfo(Eval("LastActionCreated"))%></td>
                                    <td rowspan="3" style="width:24px"><asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="App_Themes/Argix/Images/select.gif" OnCommand="OnOnCommand" CommandName="View" CommandArgument='<%# Eval("ID") %>' /></td></tr>
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
                <asp:ObjectDataSource ID="odsIssues" runat="server" TypeName="Argix.Customers.CustomersGateway" SelectMethod="SearchIssuesAdvanced">
                    <SelectParameters>
                        <asp:Parameter Name="agentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="zone" ControlID="txtZone" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="store" ControlID="txtStore" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="agent" ControlID="cboAgent" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="company" ControlID="cboCompany" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="type" ControlID="cboIssueType" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="action" ControlID="cboActionType" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="received" ControlID="txtReceived" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="subject" ControlID="txtSubject" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:Parameter Name="contact" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="originator" ControlID="txtOriginator" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                        <asp:ControlParameter Name="coordinator" ControlID="txtCoordinator" PropertyName="Text" ConvertEmptyStringToNull="true" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>        
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>