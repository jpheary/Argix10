<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="ActionNew.aspx.cs" Inherits="ActionNew" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<div class="subtitle"><asp:Label ID="lblTitle" runat="server" Text="New Action"></asp:Label></div>
<asp:ValidationSummary ID="vsAction" runat="server" ValidationGroup="vgAction" />
<table id="tblPage" style="width:600px">
    <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td>&nbsp;</td></tr>
    <tr>
        <td style="text-align:right; vertical-align:top">Type&nbsp;</td>
        <td><asp:DropDownList ID="cboActionType" runat="server" Width="144px" DataSourceID="odsActionTypes" DataTextField="Type" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnTypeChanged" /></td>
    </tr>
    <tr style="font-size:1px; height:6px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td style="text-align:right; vertical-align:top">Comments&nbsp;</td>
        <td><asp:TextBox ID="txtComments" runat="server" Width="100%" Height="200px" BorderStyle="Inset" BorderWidth="2px" TextMode="MultiLine" AutoPostBack="True" OnTextChanged="OnCommentsChanged"></asp:TextBox></td>
    </tr>
    <tr style="font-size:1px; height:24px"><td colspan="2">&nbsp;</td></tr>
    <tr>
        <td>&nbsp;</td>
        <td style="text-align:right">
            <asp:Button ID="btnOk" runat="server" Width="96px" Height="20px" Text="OK" ToolTip="Create new action" UseSubmitBehavior="False" ValidationGroup="vgAction" CommandName="OK" OnCommand="OnCommandClick" />
            &nbsp;<asp:Button ID="btnCancel" runat="server" Width="96px" Height="20px" Text=" Cancel " ToolTip="Cancel new action" UseSubmitBehavior="False" CommandName="Cancel" OnCommand="OnCommandClick" />
        </td>
    </tr>
</table>
<asp:RequiredFieldValidator ID="rfvComments" runat="server" ControlToValidate="txtComments" ErrorMessage="Please enter a comment." ValidationGroup="vgAction" />
<asp:ObjectDataSource ID="odsActionTypes" runat="server" SelectMethod="GetActionTypes" TypeName="Argix.Customers.CustomersGateway">
    <SelectParameters>
        <asp:QueryStringParameter Name="issueID" QueryStringField="issueID" DefaultValue="0" Type="Int64" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>
