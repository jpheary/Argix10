<%@ Page Title="Memberships" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="LoginAccounts.aspx.cs" Inherits="_LoginAccounts" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <script type="text/jscript">
        $(document).ready(function () {
            $("#tabs").tabs({ active: "<%=this.mView %>" });
            jQueryBind();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) { jQueryBind(); }
        function jQueryBind() {
            $("#<%=btnDelete.ClientID %>").button();
            $("#<%=btnUpdate.ClientID %>").button();
            $("#<%=btnAdd.ClientID %>").button();
        }
   </script>
    <div class="subtitle">Administration</div>
    <div id="tabs">
        <ul>
            <li><a href="#tabAccounts">Client Login Accounts</a></li>
        </ul>
        <div id="tabAccounts">
            <div class="gridviewtitle">Membership Users</div>
            <div class="gridviewbody">
                <asp:UpdatePanel ID="upnlAccounts" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:GridView ID="grdAccounts" runat="server" Width="100%" DataSourceID="odsAccounts" DataKeyNames="UserName" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnAccountSelected" >
                        <Columns>
                            <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                            <asp:BoundField DataField="UserName" HeaderText="User" HeaderStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="UserName" />
                            <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="200px" ItemStyle-Wrap="false" SortExpression="Email" Visible="true" />
                            <asp:BoundField DataField="ClientID" HeaderText="ClientID" HeaderStyle-Width="50px" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" SortExpression="ClientID" />
                            <asp:BoundField DataField="IsApproved" HeaderText="Approved" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" SortExpression="IsApproved" />
                            <asp:BoundField DataField="IsLockedOut" HeaderText="Locked" HeaderStyle-Width="75px" ItemStyle-HorizontalAlign="Center" SortExpression="IsLockedOut" />
                            <asp:BoundField DataField="LastLoginDate" HeaderText="Logon" HeaderStyle-Width="100px" DataFormatString="{0:yyyy-MM-dd}" SortExpression="LastLoginDate" HtmlEncode="False" />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Command" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <div class="services">
                <asp:UpdatePanel ID="upnlCommands" runat="server" UpdateMode="Conditional" >
                <ContentTemplate>
                    <asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete selected mebership user" UseSubmitBehavior="true" OnClientClick="return confirm('Are you sure you want to delete this user?');" CommandName="Delete" OnCommand="OnCommand" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Edit" ToolTip="Edit selected membership user" UseSubmitBehavior="False" CommandName="Edit" OnCommand="OnCommand" />
                    <asp:Button ID="btnAdd" runat="server" Text="Add" ToolTip="Add a new membership user" UseSubmitBehavior="False" CommandName="Add" OnCommand="OnCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="grdAccounts" EventName="SelectedIndexChanged" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <asp:ObjectDataSource ID="odsAccounts" runat="server" TypeName="MembershipServices" SelectMethod="GetMembers" />
        </div>
    </div>
</asp:Content>
