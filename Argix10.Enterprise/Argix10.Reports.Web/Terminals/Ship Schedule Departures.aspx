<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Ship Schedule Departures.aspx.cs" Inherits="ShipScheduleDepartures" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<script type="text/javascript">
    var daysback = -90, daysforward = 30, daysspread = 21;
    $(function () {
        $("#<%=txtDepartureStart.ClientID %>").datepicker({
            minDate: daysback, maxDate: 0, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtDepartureEnd.ClientID %>").datepicker("option", "minDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() + daysspread);
                var todate = $("#<%=txtDepartureEnd.ClientID %>").datepicker("getDate");
                if (todate > date) $("#<%=txtDepartureEnd.ClientID %>").datepicker("setDate", date);
            }
        });
        $("#<%=txtDepartureEnd.ClientID %>").datepicker({
            minDate: 0, maxDate: daysforward, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtDepartureStart.ClientID %>").datepicker("option", "maxDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() - daysspread);
                var fromdate = $("#<%=txtDepartureStart.ClientID %>").datepicker("getDate");
                if (fromdate < date) $("#<%=txtDepartureStart.ClientID %>").datepicker("setDate", date);
            }
        });
    });
</script>
<div>
    <div style="margin:50px 0px 0px 25px">
        <label for="txtDepartureStart">Departure Date&nbsp;</label><asp:TextBox ID="txtDepartureStart" runat="server" Width="100px" />
        &nbsp;-&nbsp;<asp:TextBox ID="txtDepartureEnd" runat="server" Width="100px" />
    </div>
</div>
</asp:Content>

