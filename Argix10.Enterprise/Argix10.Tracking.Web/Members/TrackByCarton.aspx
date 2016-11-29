<%@ Page Title="Track By Carton" Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="TrackByCarton.aspx.cs" Inherits="_TrackByCarton" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
<script type="text/javascript">
    $(document).ready(function () {
        $("#<%= btnTrack.ClientID %>").button();
    });

    function checkTextLen(field, maxlimit) {
        if (field.value.length > maxlimit) {
            field.value = field.value.substring(0, maxlimit);
            alert('Length of the carton number exceeded the maximum allowed.');
            return false;
        }
    }
    function checkEmptyTextBox(field) {
        if (field.value.replace(/^\s+/, '').replace(/\s+$/, '') == '') {
            alert('No valid tracking numbers were entered.');
            return false;
        }
        else
            return true;
    }
    function removeNonNumerics(evt) {
        if (document.all.cboSearchBy[1].checked) {
            var keyCode = evt.which ? evt.which : evt.keyCode;
            if ((keyCode > '0'.charCodeAt() && keyCode <= '9'.charCodeAt()) || (keyCode == 13 || keyCode == 188))
                return true;
            else
                return false;
        }
        else
            return true;
    }
</script>
<div class="trackrequest">
    <div class="subtitle">By Carton</div>
    <p>Track up to 25 cartons at a time. Enter one tracking# per line, or separate each with a comma.</p>
    <asp:ValidationSummary ID="vsTracking" runat="server" ValidationGroup="vgTracking" />
    <div>
        <fieldset>
            <legend>Tracking Request</legend>
            <br />
            <label for="cboTrackBy">Track By</label>
            <asp:DropDownList ID="cboTrackBy" runat="server" Width="200px">
                <asp:ListItem Text="Carton Number" Value="CartonNumber" Selected="True" />
                <asp:ListItem Text="Argix Label Number" Value="LabelNumber" />
                <asp:ListItem Text="License Plate Number" Value="PlateNumber" />
            </asp:DropDownList>
            <br />
            <label for="txtNumbers">Tracking #</label><asp:TextBox ID="txtNumbers" runat="server" Width="300px" MaxLength="1000" Rows="11" TextMode="MultiLine" />
            <div class="services">
                <asp:Button ID="btnTrack" runat="server" Text="Track" ValidationGroup="vgTracking" UseSubmitBehavior="true" OnClick="OnTrack" />
            </div>
        </fieldset>
    </div>
    <asp:RequiredFieldValidator ID="rfvNumbers" runat="server" ControlToValidate="txtNumbers" Display="None" ErrorMessage="Please enter a valid number." />
</div>
</asp:Content>

