<%@ Page Title="Track By PO/PRO" Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="TrackByShipment.aspx.cs" Inherits="_TrackByShipment" %>
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
</script>
<div class="trackrequest">
    <div class="subtitle">By Shipment</div>
    <p>Track by BOL#, PO#, or shipment#.</p>
    <asp:ValidationSummary ID="vsTracking" runat="server" ValidationGroup="vgTracking" />
    <div>
        <fieldset>
            <legend>Tracking Request</legend>
            <br />
            <label for="cboClient">Client</label><asp:DropDownList ID="cboClient" runat="server" Width="250px" DataSourceID="odsClients" DataTextField="CompanyName" DataValueField="ClientID" /><br />
            <label for="cboTrackBy">Track By</label><asp:DropDownList ID="cboTrackBy" runat="server" Width="150px"><asp:ListItem Text="BOL Number" Value="BOLNumber" /><asp:ListItem Text="PO Number" Value="PONumber" /><asp:ListItem Text="PRO Number" Value="PRONumber" Selected="True" /></asp:DropDownList><br />
            <label for="txtNumber">Tracking#</label><asp:TextBox ID="txtNumber" runat="server" Width="200px" MaxLength="30" /><br />
            <div class="services">
                <asp:Button ID="btnTrack" runat="server" Text="Track" ValidationGroup="vgTracking" UseSubmitBehavior="true" OnClick="OnTrack" />
            </div>
        </fieldset>
    </div>
    <asp:RequiredFieldValidator ID="rfvNumbers" runat="server" ControlToValidate="txtNumber" />
    <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.Enterprise.TrackingGateway" SelectMethod="GetClients" EnableCaching="false" />
</div>
</asp:Content>
