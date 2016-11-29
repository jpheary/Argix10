<%@ Page Title="" Language="C#" MasterPageFile="~/Default.master" AutoEventWireup="true" CodeFile="Admin.aspx.cs" Inherits="Admin" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" ContentPlaceHolderID="cpMeta" Runat="Server">
</asp:Content>
<asp:Content ID="cBody" ContentPlaceHolderID="cpBody" Runat="Server">
<div class="admin">
    <script type="text/jscript">
        $(document).ready(function () {
            jQueryBind();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) {
            jQueryBind();
        }
        function jQueryBind() {
            $("#<%=btnOk.ClientID %>").button();
            $("#<%=btnCancel.ClientID %>").button();
        }
    </script>
    <div class="subtitle">Manage Departments</div>
    <div>
        <fieldset>
            <br />
            <legend>Badge Information</legend>
                <asp:UpdatePanel ID="upnlPage" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <label for="cboBadgeType">Badge Type</label><asp:DropDownList ID="cboBadgeType" runat="server" Width="200px" AutoPostBack="true" OnTextChanged="OnBadgeTypeChanged"><asp:ListItem Text="Employees" Value="Employees" Selected="True" /><asp:ListItem Text="Vendors" Value="Vendors" /></asp:DropDownList><br />
                    <label for="lstDepartments">Departments</label><asp:ListBox ID="lstDepartments" runat="server" style="width:200px; height:250px" DataSourceID="" DataTextField="Name" DataValueField="Name" /><br />
                    <label for="txtName">Add Name</label><asp:TextBox ID="txtName" runat="server" MaxLength="30" Width="200px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
                </ContentTemplate>
                </asp:UpdatePanel>
            <div class="services">
                <asp:UpdatePanel ID="upnlServices" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnOk" runat="server" Text="Add" CommandName="Ok" OnCommand="OnCommand"  />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" UseSubmitBehavior="false" CommandName="Cancel" OnCommand="OnCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="cboBadgeType" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtName" EventName="TextChanged" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
        </fieldset>
    </div>
</div>
<asp:ObjectDataSource ID="odsEmployeeDepartments" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="GetEmployeeDepartmentList" />
<asp:ObjectDataSource ID="odsVendorDepartments" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="GetVendorDepartmentList" />
</asp:Content>

