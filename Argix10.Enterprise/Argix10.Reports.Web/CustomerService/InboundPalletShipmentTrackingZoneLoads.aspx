<%@ Page Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="InboundPalletShipmentTrackingZoneLoads.aspx.cs" Inherits="InboundPalletShipmentTrackingZoneLoads" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cHead" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="Setup" Runat="Server">
<script type="text/javascript">
    var daysback = -365, daysforward = 0, daysspread = 92;
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
    <div style="margin:50px 0px 20px 25px">
        <label for="cboClient" style="margin:0px 35px 0px 0px">Client&nbsp;</label>
        <asp:DropDownList id="cboClient" runat="server" Width="300px" DataTextField="ClientName" DataValueField="ClientNumber" />
    </div>
    <div style="margin:20px 0px 0px 25px">
        <label for="txtFrom">Pickups from&nbsp;</label><asp:TextBox ID="txtFrom" runat="server" Width="100px" />
        <label for="txtTo">&nbsp;to&nbsp;</label><asp:TextBox ID="txtTo" runat="server" Width="100px" />    
    </div>
</div>
</asp:Content>

