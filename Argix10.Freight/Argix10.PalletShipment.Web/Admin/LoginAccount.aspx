<%@ Page Title="Membership User" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="LoginAccount.aspx.cs" Inherits="_LoginAccount" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<div class="subtitle">Login Account</div>
    <br />
    <div style="float:left; width:400px; margin:25px">
        <asp:Panel ID="pnlUser" runat="server" GroupingText="Membership">
            <table>
                <tr style="font-size:1px; height:5px"><td style="width:100px">&nbsp;</td><td>&nbsp;</td></tr>
                <tr>
                    <td style="text-align:right">User Name&nbsp;</td>
                    <td><asp:TextBox ID="txtUserName" runat="server" Width="150px" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">Email&nbsp;</td>
                    <td><asp:TextBox ID="txtEmail" runat="server" Width="200px" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">Password&nbsp;</td>
                    <td><asp:TextBox ID="txtPassword" runat="server" Width="125px" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">Comments&nbsp;</td>
                    <td><asp:TextBox ID="txtComments" runat="server" Width="250px" ReadOnly="true" /></td>
                </tr>
                <tr>
                    <td style="text-align:right">ClientID&nbsp;</td>
                    <td><asp:TextBox ID="txtCompany" runat="server" MaxLength="100" Width="100px" ReadOnly="true" /></td>
                </tr>
            </table>
            <br />
        </asp:Panel>
        <br />
        <asp:Panel ID="pnlStatus" runat="server" GroupingText="Status">
            <br />
            <asp:CheckBox ID="chkApproved" runat="server" Width="150px" Text="Approved" />
            <br />
            <asp:CheckBox ID="chkLockedOut" runat="server" Width="150px" Text="Locked Out" />
            <br /><br />
        </asp:Panel>
    </div>
    <div style="float:left; width:200px; margin:25px">
        <asp:Panel ID="pnlRole" runat="server" GroupingText="Role">
            <br />
            <asp:RadioButtonList ID="optRole" runat="server">
                <asp:ListItem Text="Guest" Value="guest" />
                <asp:ListItem Text="Administrator" Value="administrator" />
                <asp:ListItem Text="Sales Rep" Value="salesrep" />
                <asp:ListItem Text="Client" Value="client" />
            </asp:RadioButtonList>
            <br />
        </asp:Panel>
    </div>
    <div class="clear"></div>
    <div>
        <asp:LinkButton ID="btnSubmit" runat="server" Text="Submit" CssClass="submit" CommandName="OK" OnCommand="OnCommand" />
        <asp:LinkButton ID="btnClose" runat="server" Text="Close" CssClass="submit" CausesValidation="False" CommandName="Close" OnCommand="OnCommand" />
    </div>
    <br />
</asp:Content>

