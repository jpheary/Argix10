<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Ship Schedule OFDs.aspx.cs" Inherits="ShipScheduleOFDs" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<script type="text/javascript">
    var daysback = -90, daysforward = 30, daysspread = 21;
    $(function () {
        $("#<%=txtOFDStart.ClientID %>").datepicker({
            minDate: daysback, maxDate: 0, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtOFDEnd.ClientID %>").datepicker("option", "minDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() + daysspread);
                var todate = $("#<%=txtOFDEnd.ClientID %>").datepicker("getDate");
                if (todate > date) $("#<%=txtOFDEnd.ClientID %>").datepicker("setDate", date);
            }
        });
        $("#<%=txtOFDEnd.ClientID %>").datepicker({
            minDate: 0, maxDate: daysforward, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtOFDStart.ClientID %>").datepicker("option", "maxDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() - daysspread);
                var fromdate = $("#<%=txtOFDStart.ClientID %>").datepicker("getDate");
                if (fromdate < date) $("#<%=txtOFDStart.ClientID %>").datepicker("setDate", date);
            }
        });
    });
</script>
<div>
    <div style="margin:50px 0px 0px 25px">
        <label for="txtOFDStart">OFD Date&nbsp;</label><asp:TextBox ID="txtOFDStart" runat="server" Width="100px" />
        &nbsp;-&nbsp;<asp:TextBox ID="txtOFDEnd" runat="server" Width="100px" />
    </div>
</div>
</asp:Content>
