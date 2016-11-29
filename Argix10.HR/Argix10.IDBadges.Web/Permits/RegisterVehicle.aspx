<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="RegisterVehicle.aspx.cs" Inherits="_RegisterVehicle" %>
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
        $("#<%=txtPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
        $("#<%=btnOk.ClientID %>").button();
        $("#<%=btnCancel.ClientID %>").button();
    }
</script>
<div class="permit">
    <div class="subtitle">Register Vehicle</div>
    <asp:ValidationSummary ID="vsPermit" runat="server" ValidationGroup="vgPermit" />
    <div>
        <label for="dduType">Type</label>
        <asp:DropDownList ID="dduType" runat="server" Width="125px" AutoPostBack="true" OnSelectedIndexChanged="OnTypeChanged"><asp:ListItem Text="Employee" Value="Employee" Selected="True" /><asp:ListItem Text="Driver" Value="Driver" /><asp:ListItem Text="Vendor" Value="Vendor" /><asp:ListItem Text="Other" Value="Other" /></asp:DropDownList>
        <label for="txtPermitNumber">Permit#</label><asp:Label ID="lblPrefix" runat="server" CssClass="permitprefix" Text="" />&nbsp;<asp:TextBox ID="txtPermitNumber" runat="server" MaxLength="4" CssClass="permitprefixr" Width="50px" AutoPostBack="true" OnTextChanged="OnValidatePermitNumber" /><br />
    </div>
    <div>
        <fieldset>
            <legend>Vehicle</legend>
            <label for="dduStates">State</label><asp:DropDownList ID="dduStates" runat="server" Width="75px" DataSourceID="odsStates" DataTextField="Name" DataValueField="Name" />
            <label for="txtPlate">Plate#</label><asp:TextBox ID="txtPlate" runat="server" MaxLength="10" Width="100px" AutoPostBack="true" OnTextChanged="OnValidatePlateNumber" /><br />
            <label for="txtYear">Year</label><asp:TextBox ID="txtYear" runat="server" MaxLength="4" Width="75px" /><br />
            <label for="txtMake">Make</label><asp:TextBox ID="txtMake" runat="server" MaxLength="20" Width="200px" /><br />
            <label for="txtModel">Model</label><asp:TextBox ID="txtModel" runat="server" MaxLength="20" Width="200px" /><br />
            <label for="txtColor">Color</label><asp:TextBox ID="txtColor" runat="server" MaxLength="20" Width="200px" /><br />
            <br />
        </fieldset>
        <fieldset>
            <legend>Contact</legend>
            <label for="txtFirstName">First Name</label><asp:TextBox ID="txtFirstName" runat="server" MaxLength="30" Width="250px" /><br />
            <label for="txtMiddle">Middle Name</label><asp:TextBox ID="txtMiddle" runat="server" MaxLength="20" Width="150px" /><br />
            <label for="txtLastName">Last Name</label><asp:TextBox ID="txtLastName" runat="server" MaxLength="30" Width="250px" /><br />
            <label for="txtPhone">Phone</label><asp:TextBox ID="txtPhone" runat="server" MaxLength="20" Width="150px" /><br />
            <br />
            <label for="dduBadge">Badge#</label><asp:TextBox ID="txtBadgeNumber" runat="server" MaxLength="5" Width="100px" /><br />
            <br />
        </fieldset>
        <div class="services">
            <asp:Button ID="btnOk" runat="server" Text="Ok" ValidationGroup="vgPermit" CommandName="Ok" OnCommand="OnCommand"  />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" UseSubmitBehavior="false" CommandName="Cancel" OnCommand="OnCommand" />
        </div>
        <asp:RequiredFieldValidator ID="rfvPermitNumber" runat="server" ControlToValidate="txtPermitNumber" ErrorMessage="Permit# is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvPlate" runat="server" ControlToValidate="txtPlate" ErrorMessage="License plate is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvYear" runat="server" ControlToValidate="txtYear" ErrorMessage="Vehicle year is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvMake" runat="server" ControlToValidate="txtMake" ErrorMessage="Vehicle make is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvModel" runat="server" ControlToValidate="txtModel" ErrorMessage="Vehicle model is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvColor" runat="server" ControlToValidate="txtColor" ErrorMessage="Vehicle color is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="First Name is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="txtLastName" ErrorMessage="Last Name is required." ValidationGroup="vgPermit" />
        <asp:RequiredFieldValidator ID="rfvPhone" runat="server" ControlToValidate="txtPhone" ErrorMessage="Phone number is required." ValidationGroup="vgPermit" />
    </div>
    <asp:ObjectDataSource ID="odsStates" runat="server" TypeName="Argix.HR.Permits.PermitGateway" SelectMethod="GetStateList" />
</div>
</asp:Content>