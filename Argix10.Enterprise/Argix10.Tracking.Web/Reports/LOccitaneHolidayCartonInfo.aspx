<%@ Page Language="C#" MasterPageFile="~/MasterPages/Reports.master" AutoEventWireup="true" CodeFile="LOccitaneHolidayCartonInfo.aspx.cs" Inherits="LOccitaneHolidayCartonInfo" %>
<%@ MasterType VirtualPath="~/MasterPages/Reports.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cScript" runat="server" ContentPlaceHolderID="cpScript">
    <script type="text/javascript">
        var daysback = -90, daysforward = 0, daysspread = 30;
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
            </fieldset>
        </div>
        <br />
        <div class="gridviewtitle">Pickups</div>
        <div class="gridviewbody250">
            <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
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
                        <asp:BoundField DataField="ManifestNumbers" HeaderText="Manifests" HeaderStyle-Width="150px" ItemStyle-Width="150px" ItemStyle-Wrap="true" NullDisplayText=" " />
                        <asp:BoundField DataField="TrailerNumbers" HeaderText="Trailers" HeaderStyle-Width="150px" NullDisplayText=" " />
                    </Columns>
                </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="txtStart" EventName="TextChanged" />
                    <asp:AsyncPostBackTrigger ControlID="txtEnd" EventName="TextChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>
    <asp:ObjectDataSource ID="odsPickups" runat="server" TypeName="Argix.EnterpriseService" SelectMethod="GetPickups" EnableCaching="true" CacheExpirationPolicy="Sliding" CacheDuration="60" >
        <SelectParameters>
            <asp:Parameter Name="client" DefaultValue="122" Type="string" />
            <asp:Parameter Name="division" DefaultValue="01" Type="string" />
            <asp:ControlParameter Name="startDate" ControlID="txtStart" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter Name="endDate" ControlID="txtEnd" PropertyName="Text" Type="DateTime" />
            <asp:Parameter Name="vendor" DefaultValue="" ConvertEmptyStringToNull="true" Type="string" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>

