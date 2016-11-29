<%@ Page Language="C#" masterpagefile="~/Default.master" AutoEventWireup="true" CodeFile="RevokePermit.aspx.cs" Inherits="_RevokePermit" %>
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
<div class="permit">
    <div class="subtitle">Revoke Permit#&nbsp;<asp:Label ID="lblPermitNumber" runat="server" Width="100px" Text="" /></div>
    <asp:ValidationSummary ID="vsPermit" runat="server" ValidationGroup="vgPermit" />
    <div>
        <label for="txtReason">Reason</label><asp:TextBox ID="txtReason" runat="server" MaxLength="200" Width="450px" /><br />
    </div>
    <div>
        <fieldset>
            <legend>Vehicle</legend>
            <label for="lblPlate">Plate#</label><asp:Label ID="lblPlate" runat="server" Width="200px" Text="" /><br />
            <label for="lblMake">Make</label><asp:Label ID="lblMake" runat="server" Width="375px" Text="" /><br />
            <br />
        </fieldset>
        <fieldset>
            <legend>Contact</legend>
            <label for="lblName">Name</label><asp:Label ID="lblName" runat="server" Width="375px" Text="" /><br />
            <label for="lblPhone">Phone</label><asp:Label ID="lblPhone" runat="server" Width="150px" Text="" /><br />
            <label for="lblBadge">Badge#</label><asp:Label ID="lblBadge" runat="server" Width="100px" Text="" /><br />
           <br />
        </fieldset>
        <div class="services">
            <asp:Button ID="btnOk" runat="server" Text="Ok" ValidationGroup="vgPermit" OnClientClick="return confirm('Revoke this permit?');" CommandName="Ok" OnCommand="OnCommand"  />
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" UseSubmitBehavior="false" CommandName="Cancel" OnCommand="OnCommand" />
        </div>
        <asp:RequiredFieldValidator ID="rfvReason" runat="server" ControlToValidate="txtReason" ErrorMessage="Revoke reason is required." ValidationGroup="vgPermit" />
    </div>
</div>
</asp:Content>