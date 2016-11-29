<%@ Page Title="Memberships" Language="C#" MasterPageFile="~/MasterPages/Profile.master" AutoEventWireup="true" EnableEventValidation="false" CodeFile="Memberships.aspx.cs" Inherits="_Memberships" %>
<%@ MasterType VirtualPath="~/MasterPages/Profile.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
    <script type="text/javascript">
        $(document).ready(function () {
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
        
        function scroll(username) {
            var grd = document.getElementById("<%=grdMembers.ClientID %>");
            for (var i = 1; i < grd.rows.length; i++) {
                var cell = grd.rows[i].cells[1];
                if (cell.innerHTML.substr(0, username.length) == username) {
                    var pnl = document.getElementById("<%=grdMembers.ClientID %>");
                    pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                    break;
                }
            }
        }
    </script>
    <div class="admin">
        <div class="subtitle">Manage Membership</div>
        <p>&nbsp;</p>
        <div class="gridviewtitle">Login Accounts</div>
        <div class="gridviewbody">
            <asp:UpdatePanel ID="upnlMembers" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:GridView ID="grdMembers" runat="server" Width="100%" DataSourceID="odsMembers" DataKeyNames="UserName" AutoGenerateColumns="False" AllowSorting="True" OnSelectedIndexChanged="OnMemberSelected" >
                    <Columns>
                        <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                        <asp:BoundField DataField="UserName" HeaderText="User" HeaderStyle-Width="100px" ItemStyle-Wrap="false" SortExpression="UserName" />
                        <asp:BoundField DataField="UserFullName" HeaderText="Name" HeaderStyle-Width="150px" ItemStyle-Wrap="True" SortExpression="UserFullName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="150px" ItemStyle-Wrap="false" SortExpression="Email" Visible="False" />
                        <asp:BoundField DataField="Company" HeaderText="Company" HeaderStyle-Width="150px" ItemStyle-Wrap="false" SortExpression="Company" />
                        <asp:BoundField DataField="IsLockedOut" HeaderText="Locked" HeaderStyle-Width="50px" SortExpression="IsLockedOut" />
                        <asp:BoundField DataField="LastLoginDate" HeaderText="Logon" HeaderStyle-Width="100px" DataFormatString="{0:MMddyyyy}" SortExpression="LastLoginDate" HtmlEncode="False" />
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnDelete" EventName="Command" />
            </Triggers>
            </asp:UpdatePanel>
        </div>
        <div class="services">
            <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:Button ID="btnDelete" runat="server" Text="Delete" ToolTip="Delete selected mebership user" UseSubmitBehavior="true" OnClientClick="return confirm('Are you sure you want to delete this user?');" CommandName="Delete" OnCommand="OnCommand" />
                <asp:Button ID="btnUpdate" runat="server" Text="Edit" ToolTip="Edit selected membership user" UseSubmitBehavior="False" CommandName="Edit" OnCommand="OnCommand" />
                <asp:Button ID="btnAdd" runat="server" Text="Add" ToolTip="Add a new membership user" UseSubmitBehavior="False" CommandName="Add" OnCommand="OnCommand" />
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="grdMembers" EventName="SelectedIndexChanged" />
            </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsMembers" runat="server" TypeName="MembershipServices" SelectMethod="GetMembers" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="600" CacheKeyDependency="Memberships" />
</asp:Content>
