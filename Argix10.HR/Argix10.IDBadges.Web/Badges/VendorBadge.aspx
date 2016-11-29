<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="VendorBadge.aspx.cs" Inherits="_VendorBadge" %>
<%@ MasterType VirtualPath="~/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cBody" runat="server" ContentPlaceHolderID="cpBody">
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
<div class="badge">
    <div class="subtitle">Vendor Badge&nbsp;:&nbsp;<asp:Label ID="lblID" runat="server" Text="New" /></div>
    <p>&nbsp;</p>
    <asp:ValidationSummary ID="vsBadge" runat="server" ValidationGroup="vgBadge" />
    <div class="badgedata">
        <fieldset>
            <legend>Badge Information</legend>
            <label for="txtLastName">Last Name</label><asp:TextBox ID="txtLastName" runat="server" MaxLength="30" Width="250px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
            <label for="txtFirstName">First Name</label><asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" Width="250px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
            <label for="txtMiddle">Middle</label><asp:TextBox ID="txtMiddle" runat="server" MaxLength="20" Width="150px" /><br />
            <label for="txtSuffix">Suffix</label><asp:TextBox ID="txtSuffix" runat="server" MaxLength="20" Width="150px" /><br />
            <br /><br />
            <label for="cboLocation">Location</label><asp:DropDownList ID="cboLocation" runat="server" DataSourceID="odsLocationList" DataTextField="Name" DataValueField="Name" Width="200px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
            <label for="cboDepartment">Department</label><asp:DropDownList ID="cboDepartment" runat="server" DataSourceID="odsDepartmentList" DataTextField="Name" DataValueField="Name" Width="200px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
            <label for="cboStatus">Status</label><asp:DropDownList ID="cboStatus" runat="server" DataSourceID="odsStatusList" DataTextField="Name" DataValueField="Name" Width="100px" AutoPostBack="true" OnTextChanged="OnValidateForm" /><br />
            <div class="services">
                <asp:UpdatePanel ID="upnlServices" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnOk" runat="server" Text="Ok" CommandName="Ok" OnCommand="OnCommand" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" UseSubmitBehavior="false" CommandName="Cancel" OnCommand="OnCommand" />
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtLastName" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtFirstName" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboLocation" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboDepartment" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="cboStatus" EventName="TextChanged" />
                </Triggers>
                </asp:UpdatePanel>
            </div>
            <br />
        </fieldset>
        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required." ValidationGroup="vgBadge" />
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required." ValidationGroup="vgBadge" />
    </div>
    <div class="badgedataphoto">
        <asp:Image ID="imgPhoto" runat="server" />
    </div>
</div>
<div style="clear:both"></div>
<asp:ObjectDataSource ID="odsLocationList" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="GetVendorLocationList" />
<asp:ObjectDataSource ID="odsDepartmentList" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="GetVendorDepartmentList" />
<asp:ObjectDataSource ID="odsStatusList" runat="server" TypeName="Argix.HR.BadgeGateway" SelectMethod="GetVendorStatusList" />
</asp:Content>