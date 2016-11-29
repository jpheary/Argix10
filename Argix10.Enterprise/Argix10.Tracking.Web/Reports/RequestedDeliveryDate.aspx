<%@ Page Language="C#" MasterPageFile="~/MasterPages/Reports.master" AutoEventWireup="true" CodeFile="RequestedDeliveryDate.aspx.cs" Inherits="RequestedDeliveryDate" %>
<%@ MasterType VirtualPath="~/MasterPages/Reports.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScript">
    <script type="text/javascript">
        $(document).ready(function () {
            jQueryBindPage();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequestPage);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequestPage);
        function OnBeginRequestPage(sender, args) { }
        function OnEndRequestPage(sender, args) { jQueryBindPage(); }
        function jQueryBindPage() {
            $("#<%=txtDate.ClientID %>").datepicker({ minDate: -7, maxDate: 0, beforeShowDay: $.datepicker.noWeekends });
        }
    </script>
</asp:Content>
<asp:Content ID="cSetup" ContentPlaceHolderID="cpSetup" runat="Server">
    <div class="reports">
        <div>
            <fieldset>
                <legend>Setup</legend>
                <label for="txtStart">OFD Date</label><asp:TextBox ID="txtDate" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnDateChanged" /><br />
            </fieldset>
        </div>
    </div>
</asp:Content>

