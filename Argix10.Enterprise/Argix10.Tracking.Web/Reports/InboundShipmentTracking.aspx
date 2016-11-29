<%@ Page Language="C#" MasterPageFile="~/MasterPages/Reports.master" AutoEventWireup="true" CodeFile="InboundShipmentTracking.aspx.cs" Inherits="InboundShipmentTracking" %>
<%@ MasterType VirtualPath="~/MasterPages/Reports.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScript">
    <script type="text/javascript">
        var daysback = -90, daysforward = 0, daysspread = 7;
        $(document).ready(function () {
            jQueryBindPage();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequestPage);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequestPage);
        function OnBeginRequestPage(sender, args) { }
        function OnEndRequestPage(sender, args) { jQueryBindPage(); }
        function jQueryBindPage() {
            $("#<%=txtStart.ClientID %>").datepicker({
                minDate: daysback, maxDate: 0, defaultDate: 0,
                onClose: function (selectedDate, instance) {
                    $("#<%=txtEnd.ClientID %>").datepicker("option", "minDate", selectedDate);

                    var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                    date.setDate(date.getDate() + daysspread);
                    var todate = $("#<%=txtEnd.ClientID %>").datepicker("getDate");
                    if (todate > date) $("#<%=txtEnd.ClientID %>").datepicker("setDate", date);
                }
            });
            $("#<%=txtEnd.ClientID %>").datepicker({
                minDate: 0, maxDate: daysforward, defaultDate: 0,
                onClose: function (selectedDate, instance) {
                    $("#<%=txtStart.ClientID %>").datepicker("option", "maxDate", selectedDate);

                    var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                    date.setDate(date.getDate() - daysspread);
                    var fromdate = $("#<%=txtStart.ClientID %>").datepicker("getDate");
                    if (fromdate < date) $("#<%=txtStart.ClientID %>").datepicker("setDate", date);
                }
            });
        }
    </script>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="cpSetup" runat="Server">
    <div class="reports">
        <div>
            <fieldset>
                <legend>Setup</legend>
                <label for="txtStart">Pickup Date</label><asp:TextBox ID="txtStart" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnPickupDatesChanged" />&nbsp;-&nbsp;<asp:TextBox ID="txtEnd" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnPickupDatesChanged" /><br />
                <label for="ddlClient">Client</label><asp:DropDownList ID="ddlClient" runat="server" Width="300px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged" /><br />
            </fieldset>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetSecureClients" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900">
        <SelectParameters>
            <asp:Parameter Name="activeOnly" DefaultValue="True" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
