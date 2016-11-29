<%@ Page Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="IssueNew.aspx.cs" Inherits="IssueNew" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cHead" runat="server" ContentPlaceHolderID="cpHead">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
<div class="subtitle">New Issue</div>
<asp:ValidationSummary ID="vsIssue" runat="server" ValidationGroup="vgIssue" />
<br />
<div style="float:left">
    <table ID="tblPage" style="width:500px">
        <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Company&nbsp;</td>
            <td><asp:DropDownList ID="cboCompany" runat="server" Width="384px" AppendDataBoundItems="true" DataSourceID="odsCompanies" DataTextField="CompanyName" DataValueField="Number" AutoPostBack="true" OnSelectedIndexChanged="OnCompanyChanged">
                <asp:ListItem Text="All" Value="" Selected="True" />
            </asp:DropDownList></td>
        </tr>
        <tr style="font-size:1px; height:3px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Location&nbsp;</td>
            <td><asp:DropDownList ID="cboScope" runat="server" Width="96px" AutoPostBack="true" OnSelectedIndexChanged="OnScopeChanged" /></td>
        </tr>
        <tr style="font-size:1px; height:3px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">#&nbsp;</td>
            <td>
                <asp:MultiView ID="mvLocation" runat="server" ActiveViewIndex="0">
                    <asp:View runat="server" ID="vwOther">
                        <asp:DropDownList ID="cboLocation" runat="server" Width="384px" DataSourceID="odsAgents" DataTextField="AgentName" DataValueField="AgentNumber" AutoPostBack="true" OnSelectedIndexChanged="OnLocationChanged" />
                    </asp:View>
                    <asp:View runat="server" ID="vwStore">
                        <asp:TextBox ID="txtStore" runat="server" Width="72px" BorderStyle="Inset" BorderWidth="2px" TextMode="SingleLine" AutoPostBack="True" OnTextChanged="OnStoreChanged"></asp:TextBox>
                        <asp:TextBox ID="txtStoreDetail" runat="server" Width="100%" Height="192px" BorderStyle="Inset" BorderWidth="2px" TextMode="MultiLine" ReadOnly="true" AutoPostBack="False"></asp:TextBox>                        </asp:View>
                </asp:MultiView>
            </td>
        </tr>
        <tr style="font-size:1px; height:3px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Type&nbsp;</td>
            <td>
                <asp:DropDownList ID="cboIssueCategory" runat="server" Width="96px" DataSourceID="odsIssueCategories" DataTextField="Category" DataValueField="Category" />
                &nbsp;<asp:DropDownList ID="cboIssueType" runat="server" Width="144px" DataSourceID="odsIssueTypes" DataTextField="Type" DataValueField="ID" />
        </td>
        </tr>
        <tr style="font-size:1px; height:3px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Contact&nbsp;</td>
            <td><asp:TextBox ID="txtContact" runat="server" Width="288px" /></td>
        </tr>
        <tr style="font-size:1px; height:3px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Subject&nbsp;</td>
            <td><asp:TextBox ID="txtSubject" runat="server" Width="100%" BorderStyle="Inset" BorderWidth="2px" TextMode="SingleLine"></asp:TextBox></td>
        </tr>
        <tr style="font-size:1px; height:24px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td>&nbsp;</td>
            <td style="text-align:right">
                <asp:Button ID="btnOk" runat="server" Text="   OK   " ToolTip="Create new issue" Height="20px" Width="96px" UseSubmitBehavior="False" ValidationGroup="vgIssue" CommandName="OK" OnCommand="OnCommandClick" />
                &nbsp;<asp:Button ID="btnCancel" runat="server" Text=" Cancel " ToolTip="Cancel new issue" Height="20px" Width="96px" UseSubmitBehavior="False" CommandName="Cancel" OnCommand="OnCommandClick" />
            </td>
        </tr>
    </table>
</div>
<div style="float:left; margin:0px 0px 0px 25px">
    <table id="Table1" style="width:400px">
        <tr style="font-size:1px"><td style="width:100px">&nbsp;</td><td>&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Type&nbsp;</td>
            <td><asp:DropDownList ID="cboActionType" runat="server" Width="144px" DataSourceID="odsActionTypes" DataTextField="Type" DataValueField="ID" /></td>
        </tr>
        <tr style="font-size:1px; height:3px"><td colspan="2">&nbsp;</td></tr>
        <tr>
            <td style="text-align:right; vertical-align:top">Comments&nbsp;</td>
            <td><asp:TextBox ID="txtComments" runat="server" Width="400px" Height="125px" BorderStyle="Inset" BorderWidth="2px" TextMode="MultiLine" /></td>
        </tr>
        <asp:ObjectDataSource ID="odsActionTypes" runat="server" SelectMethod="GetActionTypes" TypeName="Argix.Customers.CustomersGateway">
            <SelectParameters>
                <asp:QueryStringParameter Name="issueID" QueryStringField="issueID" DefaultValue="0" Type="Int64" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </table>
    <asp:RequiredFieldValidator ID="rfvComments" runat="server" ControlToValidate="txtComments" ErrorMessage="Please enter a comment." ValidationGroup="vgIssue" />
</div>
<asp:ObjectDataSource ID="odsCompanies" runat="server" SelectMethod="GetCompanies" TypeName="Argix.Customers.CustomersGateway" />
<asp:ObjectDataSource ID="odsAgents" runat="server" SelectMethod="GetAgentsByClient" TypeName="Argix.Customers.CustomersGateway">
    <SelectParameters>
        <asp:ControlParameter Name="clientNumber" ControlID="cboCompany" PropertyName="SelectedValue" Type="String" />
        <asp:SessionParameter Name="agentNumber" SessionField="AgentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsIssueCategories" runat="server" SelectMethod="GetIssueCategories" TypeName="Argix.Customers.CustomersGateway">
    <SelectParameters>
        <asp:SessionParameter Name="agentNumber" SessionField="AgentNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="odsIssueTypes" runat="server" SelectMethod="GetIssueTypes" TypeName="Argix.Customers.CustomersGateway">
    <SelectParameters>
        <asp:ControlParameter Name="issueCategory" ControlID="cboIssueCategory" PropertyName="SelectedValue" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
</asp:Content>
