<%@ page Language="C#" MasterPageFile="~/MasterPages/Profile.master" AutoEventWireup="true" CodeFile="ManageUsers.aspx.cs" inherits="ManageUsers" title="Manage Users" %>
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
            $("#<%=btnRefresh.ClientID %>").button();
            $("#<%=btnWelcomeMessage.ClientID %>").button();
            $("#<%=btnResetPassword.ClientID %>").button();
        }

        function scroll(username) {
            var grd = document.getElementById('ctl00_cpBody_grdUsers');
            for (var i = 1; i < grd.rows.length; i++) {
                var cell = grd.rows[i].cells[1];
                if (cell.innerHTML.substr(0, username.length) == username) {
                    var pnl = document.getElementById('ctl00_cpBody_pnlUsers');
                    pnl.scrollTop = i * (grd.clientHeight / grd.rows.length);
                    break;
                }
            }
        }
    </script>
    <div class="admin">
        <div class="subtitle">Manage Membership Users</div>
        <p>&nbsp;</p>
        <div class="gridviewtitle">Membership Users</div>
        <div class="gridviewbody">
            <asp:UpdatePanel ID="upnlUsers" runat="server" UpdateMode="Conditional" >
            <ContentTemplate>
                <asp:GridView ID="grdUsers" runat="server" Width="100%" DataSourceID="odsUsers" DataKeyNames="UserID" AutoGenerateColumns="False" AllowPaging="false" AllowSorting="True" OnSelectedIndexChanged="OnUserChanged" OnRowEditing="OnGridRowEditing" OnRowCancelingEdit="OnGridRowCancelingEdit" OnRowUpdating="OnGridRowUpdating" OnRowUpdated="OnGridRowUpdated" OnRowDeleting="OnGridRowDeleting">
                    <Columns>
                        <asp:CommandField ButtonType="Image" HeaderStyle-Width="16px" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                        <asp:BoundField DataField="UserId" HeaderText="User ID" HeaderStyle-Width="75px" ItemStyle-Wrap="True" ReadOnly="True" SortExpression="UserID" />
                        <asp:BoundField DataField="UserFullName" HeaderText="Full Name" HeaderStyle-Width="100px" ItemStyle-Wrap="True" SortExpression="UserFullName" />
                        <asp:BoundField DataField="Email" HeaderText="Email" HeaderStyle-Width="100px" SortExpression="Email" />
                        <asp:BoundField DataField="CompanyID" HeaderText="CompanyID" HeaderStyle-Width="75px" SortExpression="CompanyID" Visible="false" />
                        <asp:BoundField DataField="Company" HeaderText="Company" HeaderStyle-Width="100px" SortExpression="Company" Visible="false" />
                        <asp:BoundField DataField="Type" HeaderText="Type" HeaderStyle-Width="75px" ReadOnly="True" SortExpression="Type" />
                        <asp:TemplateField HeaderText="Customer" HeaderStyle-Width="100px" ItemStyle-Wrap="True" SortExpression="Company">
                            <ItemTemplate>
                                <asp:Label ID="lblCompany" runat="server" Text='<%# Eval("Company") %>'></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="cboCustomers" runat="server" DataSourceID="odsCustomers" DataTextField="CompanyName" DataValueField="ClientID" SelectedValue='<%# Bind("CompanyID") %>' AutoPostBack="True" OnDataBinding="OnCustomersDataBinding"></asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:CommandField ShowEditButton="True" />
                        <asp:TemplateField ShowHeader="False">
                            <ItemTemplate>
                                <asp:LinkButton ID="btnDelete" runat="server" CausesValidation="False" Text="Delete" CommandName="Delete" OnClientClick="return confirm('Are you sure you want to delete this user?');"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="services">
            <asp:UpdatePanel ID="upnlCommand" runat="server" UpdateMode="Always" >
                <ContentTemplate>
                    <asp:Button ID="btnRefresh" runat="server" Text="Refresh" ToolTip="Refresh membership cache" UseSubmitBehavior="False" CommandName="Refresh" OnCommand="OnCommand" />
                    <asp:Button ID="btnWelcomeMessage" runat="server" Text="Send Welcome" ToolTip="No password reset, only welcome message is sent. " CommandName="Welcome" OnCommand="OnCommand" />
                    <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" ToolTip="Password is reset and mailed to the user." CommandName="Reset" OnCommand="OnCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:PostBackTrigger ControlID="btnRefresh" />
                    <asp:PostBackTrigger ControlID="btnWelcomeMessage" />
                    <asp:PostBackTrigger ControlID="btnResetPassword" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsUsers" runat="server" TypeName="MembershipServices" SelectMethod="GetTrackingUsers" UpdateMethod="UpdateUser" DeleteMethod="DeleteUser">
        <UpdateParameters>
            <asp:ControlParameter Name="userID" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter Name="userFullName" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter Name="email" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
            <asp:Parameter Name="company" Type="String" />
            <asp:ControlParameter Name="companyID" ControlID="grdUsers" PropertyName="SelectedValue" Type="String" />
        </UpdateParameters>
        <DeleteParameters>
            <asp:ControlParameter Name="userID" ControlID="grdUsers" PropertyName="SelectedDataKey.Values[0]" Type="String" />
        </DeleteParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsCustomers" runat="server" TypeName="Argix.Enterprise.TrackingGateway" SelectMethod="GetCustomers" >
        <SelectParameters>
            <asp:Parameter Name="companyType" DefaultValue="client" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
