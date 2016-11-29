<%@ Page Title="Track By Store" Language="C#" MasterPageFile="~/MasterPages/Tracking.master" AutoEventWireup="true" CodeFile="TrackByStore.aspx.cs" Inherits="_TrackByStore" %>
<%@ MasterType VirtualPath="~/MasterPages/Tracking.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <meta name="keywords" content="Login" />
    <meta name="description" content="Login"/>
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpContent">
<script type="text/javascript">
    var daysback = -365, daysforward = 7, daysspread = 30;
    $(document).ready(function () {
        $("#<%=txtFromDate.ClientID %>").datepicker({
            minDate: daysback, maxDate: 0, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtToDate.ClientID %>").datepicker("option", "minDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() + daysspread);
                var todate = $("#<%=txtToDate.ClientID %>").datepicker("getDate");
                if (todate > date) $("#<%=txtToDate.ClientID %>").datepicker("setDate", date);
            }
        });
        $("#<%=txtToDate.ClientID %>").datepicker({
            minDate: 0, maxDate: daysforward, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtFromDate.ClientID %>").datepicker("option", "maxDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() - daysspread);
                var fromdate = $("#<%=txtFromDate.ClientID %>").datepicker("getDate");
                if (fromdate < date) $("#<%=txtFromDate.ClientID %>").datepicker("setDate", date);
            }
        });

        $("#<%= btnTrack.ClientID %>").button();
        $("#<%= btnContinue.ClientID %>").button();
        $("#<%= btnCancel.ClientID %>").button();
    });
</script>
<div class="trackrequest">
    <div class="subtitle">By Store</div>
    <asp:MultiView ID="mvMain" runat="server" ActiveViewIndex="0">
    <asp:View ID="vwSearchStore" runat="server">
        <p>Track by store# and pickup or delivery date (365 days back, 31 day max range).</p>
        <div>
            <fieldset>
                <legend>Tracking Request</legend>
                <br />
                <label for="cboClient">Client</label><asp:DropDownList ID="cboClient" runat="server" Width="250px" DataSourceID="odsClients" DataTextField="CompanyName" DataValueField="ClientID" /><br />
                <label for="txtStore">Store#</label><asp:TextBox ID="txtStore" runat="server" Width="100px" MaxLength="30" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSubSearch" runat="server" Height="20px" Text="Sub-store search" /><br />
                <label for="cboSearchType">Track By</label><asp:DropDownList ID="cboSearchType" runat="server" Width="150px"><asp:ListItem Text="Delivery Date" Value="Delivery" /><asp:ListItem Text="Pickup Date" Value="Pickup" Selected="True" /></asp:DropDownList><br />
                <label for="txtFromDate">Dates</label><asp:TextBox ID="txtFromDate" runat="server" />&nbsp;-&nbsp;<asp:TextBox ID="txtToDate" runat="server" /><br />
                <div class="services">
                    <asp:Button ID="btnTrack" runat="server" Text="Track" ValidationGroup="vgTracking" UseSubmitBehavior="true" CommandName="Track" OnCommand="OnCommand"  />
                </div>
            </fieldset>
        </div>
        <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.Enterprise.TrackingGateway" SelectMethod="GetClients" EnableCaching="false" />
    </asp:View>
    <asp:View ID="vwSelectStore" runat="server">
        <p>Please select your store...</p>
        <div>
            <fieldset>
                <legend>Tracking Request</legend>
                <label for="lstStores">Store</label><asp:ListBox ID="lstStores" runat="server" Width="575px" Height="200px" DataTextField="DESCRIPTION" DataValueField="NUMBER" /><br />
                <div class="services">
                    <asp:Button ID="btnContinue" runat="server" Width="75px" Height="25px" Text="Continue" CommandName="Continue" OnCommand="OnCommand" />
                    <asp:Button ID="btnCancel" runat="server" Width="75px" Height="25px" Text="Cancel" CommandName="Cancel" OnCommand="OnCommand" />
                </div>
            </fieldset>
        </div>
        <asp:ObjectDataSource ID="odsStores" runat="server" TypeName="Argix.Enterprise.TrackingGateway" SelectMethod="GetStoresForSubStore">
            <SelectParameters>
                <asp:Parameter Name="subStoreNumber" DefaultValue="2" ConvertEmptyStringToNull="true" Type="String" />
                <asp:Parameter Name="clientNumber" DefaultValue="172" ConvertEmptyStringToNull="true" Type="String" />
                <asp:Parameter Name="vendorNumber" DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </asp:View>
    </asp:MultiView>
</div>
</asp:Content>
