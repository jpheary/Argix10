<%@ Page Language="C#" MasterPageFile="~/MasterPages/Reports.master" AutoEventWireup="true" CodeFile="PacSunDelivery.aspx.cs" Inherits="_PacSunDelivery" %>
<%@ MasterType VirtualPath="~/MasterPages/Reports.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScript">
    <script type="text/javascript">
        var daysback = -365, daysforward = 0, daysspread = 31;
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
                <label for="txtStart">Date</label><asp:TextBox ID="txtStart" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnDatesChanged" />&nbsp;-&nbsp;<asp:TextBox ID="txtEnd" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnDatesChanged" /><br />
                <labelfor="">Scope</label><asp:DropDownList ID="cboFilter" runat="server" Width="125px"><asp:ListItem Text="All" Selected="True" Value="0" /><asp:ListItem Text="OS&D Only" Value="1" /></asp:DropDownList><br />
            </fieldset>
        </div>
    </div>
</asp:Content>

