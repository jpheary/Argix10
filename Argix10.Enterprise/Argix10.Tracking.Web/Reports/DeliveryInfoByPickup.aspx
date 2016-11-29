<%@ Page Language="C#" MasterPageFile="~/MasterPages/Reports.master" AutoEventWireup="true" CodeFile="DeliveryInfoByPickup.aspx.cs" Inherits="DeliveryInfoByPickup" %>
<%@ MasterType VirtualPath="~/MasterPages/Reports.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScript">
    <script type="text/javascript">
        var daysback = -180, daysforward = 0, daysspread = 14;
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
<asp:Content ID="cSetup" ContentPlaceHolderID="cpSetup" runat="Server">
    <div class="reports">
        <div>
            <fieldset>
                <legend>Setup</legend>
                <label for="txtStart">Pickup Date</label><asp:TextBox ID="txtStart" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnPickupDatesChanged" />&nbsp;-&nbsp;<asp:TextBox ID="txtEnd" runat="server" Width="100px" AutoPostBack="True" OnTextChanged="OnPickupDatesChanged" /><br />
                <label for="ddlClient">Client</label><asp:DropDownList ID="ddlClient" runat="server" Width="275px" DataSourceID="odsClients" DataTextField="ClientName" DataValueField="ClientNumber" AutoPostBack="True" OnSelectedIndexChanged="OnClientChanged" /><br />
                <label for="ddlVendor">Vendor</label><asp:UpdatePanel runat="server" ID="upnlVendors" style="display:inline-block" UpdateMode="Conditional"><ContentTemplate><asp:DropDownList ID="ddlVendor" runat="server" Width="275px" AppendDataBoundItems="true" DataSourceID="odsVendors" DataTextField="VendorName" DataValueField="VendorNumber" AutoPostBack="True" OnSelectedIndexChanged="OnVendorChanged"><asp:ListItem Text="All" Value="" Selected="True" /></asp:DropDownList></ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel><br />
            </fieldset>
        </div>
        <br />
        <div class="gridviewtitle">Pickups</div>
        <div class="gridviewbody250">
            <asp:UpdatePanel runat="server" ID="upnlPickups" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:GridView ID="grdPickups" runat="server" Width="100%" DataSourceID="odsPickups" DataKeyNames="PickupID,TerminalCode" AutoGenerateColumns="False" AllowSorting="True">
                        <Columns>
                            <asp:TemplateField HeaderText="" HeaderStyle-Width="25px" >
                                <HeaderTemplate><asp:CheckBox ID="chkAll" runat="server" Enabled="true" AutoPostBack="true" OnCheckedChanged="OnAllPickupsSelected"/></HeaderTemplate>
                                <ItemTemplate><asp:CheckBox ID="chkSelect" runat="server" AutoPostBack="true" OnCheckedChanged="OnPickupSelected"/></ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="PickupID" HeaderText="ID" HeaderStyle-Width="25px" Visible="false" />
                            <asp:BoundField DataField="TerminalCode" HeaderText="Term" HeaderStyle-Width="25px" Visible="false" />
                            <asp:BoundField DataField="VendorNumber" HeaderText="Vendor#" HeaderStyle-Width="50px" SortExpression="VendorNumber" />
                            <asp:BoundField DataField="VendorName" HeaderText="Vendor" ItemStyle-Wrap="false" SortExpression="VendorName" />
                            <asp:BoundField DataField="PUDate" HeaderText="Pickup" HeaderStyle-Width="125px" SortExpression="PUDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" />
                            <asp:BoundField DataField="PUNumber" HeaderText="#" HeaderStyle-Width="50px" SortExpression="PUNumber" />
                            <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="100px" ItemStyle-Wrap="true" NullDisplayText=" " />
                            <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="100px" NullDisplayText=" " />
                        </Columns>
                    </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtStart" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtEnd" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlClient" EventName="SelectedIndexChanged" />
                    <asp:AsyncPostBackTrigger ControlID="ddlVendor" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsClients" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetSecureClients" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900">
        <SelectParameters>
            <asp:Parameter Name="activeOnly" DefaultValue="True" Type="Boolean" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsVendors" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetVendors" EnableCaching="false" CacheExpirationPolicy="Sliding" CacheDuration="900" >
        <SelectParameters>
            <asp:ControlParameter Name="clientNumber" ControlID="ddlClient" PropertyName="SelectedValue" Type="String" />
            <asp:Parameter Name="clientTerminal"  DefaultValue="" ConvertEmptyStringToNull="true" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="60" >
        <SelectParameters>
            <asp:ControlParameter Name="client" ControlID="ddlClient" PropertyName="SelectedValue" Type="string" />
            <asp:Parameter Name="division" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
            <asp:ControlParameter Name="startDate" ControlID="txtStart" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter Name="endDate" ControlID="txtEnd" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter Name="vendor" ControlID="ddlVendor" PropertyName="SelectedValue" ConvertEmptyStringToNull="true" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

