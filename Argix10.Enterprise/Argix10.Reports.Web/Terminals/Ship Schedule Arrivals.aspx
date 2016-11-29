<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Ship Schedule Arrivals.aspx.cs" Inherits="ShipScheduleArrivals" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<script type="text/javascript">
    var daysback = -90, daysforward = 30, daysspread = 21;
    $(function () {
        $("#<%=txtArrivalStart.ClientID %>").datepicker({
            minDate: daysback, maxDate: 0, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtArrivalEnd.ClientID %>").datepicker("option", "minDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() + daysspread);
                var todate = $("#<%=txtArrivalEnd.ClientID %>").datepicker("getDate");
                if (todate > date) $("#<%=txtArrivalEnd.ClientID %>").datepicker("setDate", date);
            }
        });
        $("#<%=txtArrivalEnd.ClientID %>").datepicker({
            minDate: 0, maxDate: daysforward, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtArrivalStart.ClientID %>").datepicker("option", "maxDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() - daysspread);
                var fromdate = $("#<%=txtArrivalStart.ClientID %>").datepicker("getDate");
                if (fromdate < date) $("#<%=txtArrivalStart.ClientID %>").datepicker("setDate", date);
            }
        });
    });
</script>
<div>
    <div style="margin:50px 0px 0px 25px">
        <label for="txtArrivalStart">Arrival Date&nbsp;</label><asp:TextBox ID="txtArrivalStart" runat="server" Width="100px" />
        &nbsp;-&nbsp;<asp:TextBox ID="txtArrivalEnd" runat="server" Width="100px" />
    </div>
</div>
</asp:Content>

