<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true"  CodeFile="ViewIssue.aspx.cs" Inherits="ViewIssue" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
    <div class="issueHeader">
        <asp:Button ID="btnBack" runat="server" Text="<< Back" style="border-style:none" OnCommand="OnOnCommand" CommandName="Back" />
        <div class="issuetitle">
            <asp:Label ID="lblType" runat="server" Width="100%" Height="18px" Text="" /><br />
        </div>
        <div class="issuesubtitle">
            <asp:Label ID="lblCompany" runat="server" Width="100%" Height="18px" Text="" /><br />
            <asp:Label ID="lblSubject" runat="server" Width="100%" Height="18px" Text="" /><br />
        </div>
    </div>
    <div class="issueBody">
        <a class="addMenu" href="javascript: unhide('addaction');">Add Action</a>
        <div id="addaction" class="hidden">
            <table style="width:300px; background-color:#ffffff">
                <tr><td style="width:300px; text-align:left; font-weight:bold"><%= DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") %>&nbsp;&nbsp;&nbsp;<%= HttpContext.Current.User.Identity.Name %></td></tr>
                <tr><td style="width:300px; text-align:left; font-weight:bold">
                    <asp:DropDownList ID="cboActionType" runat="server" DataSourceID="odsTypes" DatatextField="Type" DataValueField="ID" ToolTip="" style="width:150px" />
                    &nbsp;<asp:Button ID="btnAdd" runat="server" Text="Send" CssClass="submit" OnCommand="OnOnCommand" CommandName="New" />
                </td></tr>
                <tr><td style="text-align:left; white-space:normal"><asp:TextBox ID="txtComment" runat="server" Width="275px" Rows="3" TextMode="MultiLine" /></td></tr>
                <tr><td><hr /></td></tr>
            </table>
            <asp:ObjectDataSource ID="odsTypes" runat="server" SelectMethod="GetActionTypes" TypeName="Argix.Customers.CustomersGateway" >
                <SelectParameters><asp:QueryStringParameter Name="issueID" QueryStringField="issueID" Type="Int64" /></SelectParameters>
            </asp:ObjectDataSource>
        </div>
        <asp:ListView ID="lsvAction" runat="server"  DataSourceID="odsActions">
            <LayoutTemplate>
                <div id="itemPlaceholder" style="width:100%" runat="server" ></div>
            </LayoutTemplate>
            <ItemTemplate>
                <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:#ffffff">
                    <tr><td style="width:100%; text-align:left; font-weight:bold"><%# Eval("Created") %>&nbsp;&nbsp;&nbsp;<%# Eval("UserID") %></td></tr>
                    <tr><td style="width:100%; text-align:left; font-weight:bold"><%# Eval("TypeName") %></td></tr>
                    <tr><td>&nbsp;</td></tr>
                    <tr><td style="text-align:left; white-space:normal"><%# Eval("Comment") %></td></tr>
                    <tr><td><hr /></td></tr>
                </table>
            </ItemTemplate>
            <EmptyDataTemplate>
                <table border="0" cellpadding="3" cellspacing="0" style="width:100%; background-color:#ffffff">
                    <tr style="background-color:white; height:192px"><td>&nbsp;</td></tr>
                </table>
            </EmptyDataTemplate>
        </asp:ListView>
        <asp:ObjectDataSource ID="odsActions" runat="server" SelectMethod="GetIssueActions" TypeName="Argix.Customers.CustomersGateway">
            <SelectParameters><asp:QueryStringParameter Name="issueID" QueryStringField="issueID" Type="Int64" /></SelectParameters>
        </asp:ObjectDataSource>
    </div>
</asp:Content>
