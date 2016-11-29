<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="BNNookTracking.aspx.cs" Inherits="BNNookTracking" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="Setup" Runat="Server">
<script type="text/javascript">
    var daysback = -396, daysforward = 7, daysspread = 365;
    $(function () {
        $("#<%=txtFrom.ClientID %>").datepicker({
            minDate: daysback, maxDate: 0, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtTo.ClientID %>").datepicker("option", "minDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() + daysspread);
                var todate = $("#<%=txtTo.ClientID %>").datepicker("getDate");
                if (todate > date) $("#<%=txtTo.ClientID %>").datepicker("setDate", date);
            }
        });
        $("#<%=txtTo.ClientID %>").datepicker({
            minDate: 0, maxDate: daysforward, defaultDate: 0,
            onClose: function (selectedDate, instance) {
                $("#<%=txtFrom.ClientID %>").datepicker("option", "maxDate", selectedDate);

                var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                date.setDate(date.getDate() - daysspread);
                var fromdate = $("#<%=txtFrom.ClientID %>").datepicker("getDate");
                if (fromdate < date) $("#<%=txtFrom.ClientID %>").datepicker("setDate", date);
            }
        });
    });
</script>
<div>
    <div style="margin:50px 0px 0px 50px">
        <label for="txtFrom">Dates&nbsp;</label>
        <asp:TextBox ID="txtFrom" runat="server" Width="100px" />
        <label for="txtTo">&nbsp;-&nbsp;</label>
        <asp:TextBox ID="txtTo" runat="server" Width="100px" />    
    </div>
    <div style="margin:20px 0px 0px 83px">
        <asp:CheckBox ID="chkException" runat="server" Text="Exceptions only" Checked="true" />
    </div>
</div>
</asp:Content>

