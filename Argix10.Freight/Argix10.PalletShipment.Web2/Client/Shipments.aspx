<%@ Page Title="Shipments" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Shipments.aspx.cs" Inherits="Shipments" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <script type="text/jscript">
        var daysback = -365, daysforward = 30, daysspread = 92;
        $(document).ready(function () {
            $("#tabs").tabs( { active:"<%=this.mView %>" } );
            $("#<%=txtShipDateStart.ClientID %>").datepicker({
                minDate: daysback, maxDate: 0, defaultDate: 0,
                onClose: function (selectedDate, instance) {
                    $("#<%=txtShipDateEnd.ClientID %>").datepicker("option", "minDate", selectedDate);

                    var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                    date.setDate(date.getDate() + daysspread);
                    var todate = $("#<%=txtShipDateEnd.ClientID %>").datepicker("getDate");
                    if (todate > date) $("#<%=txtShipDateEnd.ClientID %>").datepicker("setDate", date);
                }
            });
            $("#<%=txtShipDateEnd.ClientID %>").datepicker({
                minDate: 0, maxDate: daysforward, defaultDate: 0,
                onClose: function (selectedDate, instance) {
                    $("#<%=txtShipDateStart.ClientID %>").datepicker("option", "maxDate", selectedDate);

                    var date = $.datepicker.parseDate("mm/dd/yy", selectedDate, instance.settings);
                    date.setDate(date.getDate() - daysspread);
                    var fromdate = $("#<%=txtShipDateStart.ClientID %>").datepicker("getDate");
                    if (fromdate < date) $("#<%=txtShipDateStart.ClientID %>").datepicker("setDate", date);
                }
            });

            jQueryBind();
        });

        Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
        function OnBeginRequest(sender, args) { }
        function OnEndRequest(sender, args) { jQueryBind(); }
        function jQueryBind() {
            $("#<%=btnNew.ClientID %>").button();
            $("#<%=btnUpdate.ClientID %>").button();
            $("#<%=btnCancel.ClientID %>").button();
            $("#<%=btnBOL.ClientID %>").button();
            $("#<%=btnLabels.ClientID %>").button();
            $("#<%=btnSearch.ClientID %>").button();
        }
    </script>
    <div class="subtitle">Shipments for&nbsp;<asp:Label ID="lblClient" runat="server" Text="" /></div>
    <div id="tabs">
        <ul>
            <li><a href="#tabShipments">Active</a></li>
            <li><a href="#tabSearch">Search</a></li>
        </ul>
        <div id="tabShipments">
            <asp:ValidationSummary ID="vsShipments" runat="server" ValidationGroup="vgShipments" />
            <asp:CustomValidator ID="cvStatus" runat="server" ValidationGroup="vgShipments" EnableClientScript="False" />
            <br />
            <div class="sublabel">Select a shipment using the black arrows, and then use the buttons below (if applicable).</div>
            <div class="gridviewbody">
                <asp:UpdatePanel runat="server" ID="upnlShipments" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdShipments" runat="server" Width="100%" AutoGenerateColumns="false" DataSourceID="odsShipments" DataKeyNames="ShipmentNumber,ShipDate,PickupID,Cancelled" AllowSorting="true" OnSelectedIndexChanged="OnShipmentSelected">
                            <Columns>
                                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                                <asp:TemplateField HeaderText="Shipment#" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" SortExpression="ShipmentNumber">
                                    <ItemTemplate>
                                        <a class="popupLink" href="" onclick="javascript:var w=window.open('<%# "Tracking.aspx?number=" + Eval("ShipmentNumber").ToString() %>','_blank','width=800px,height=500px,resizable=no,scrollbars=yes,titlebar=no,menubar=no,toolbar=yes,status=no');return false;" title="Click here to track this shipment.">&nbsp;<%# Eval("ShipmentNumber").ToString() %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BLNumber" HeaderText="Ref#" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                                <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" ItemStyle-Wrap="False" ItemStyle-Width="75px" SortExpression="ShipDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                                <asp:BoundField DataField="ClientNumber" HeaderText="ClientNumber" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ShipperNumber" HeaderText="ShipperNumber" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ShipperName" HeaderText="Shipper" ItemStyle-Wrap="True" ItemStyle-Width="150px" Visible="True" />
                                <asp:BoundField DataField="ConsigneeNumber" HeaderText="ConsigneeNumber" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ConsigneeName" HeaderText="Consignee" ItemStyle-Wrap="True" ItemStyle-Width="150px" Visible="True" />
                                <asp:BoundField DataField="Pallets" HeaderText="Pallets" ItemStyle-Wrap="False" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right" Visible="True" />
                                <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Visible="True" />
                                <asp:BoundField DataField="TotalCharge" HeaderText="Charge" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}" Visible="True" />
                                <asp:BoundField DataField="TerminalCode" HeaderText="Terminal" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <asp:BoundField DataField="LTLZone" HeaderText="Zone" ItemStyle-Wrap="False" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <asp:BoundField DataField="Created" HeaderText="Created" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="False" />
                                <asp:BoundField DataField="PickupID" HeaderText="Pickup#" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="False" />
                                <asp:BoundField DataField="PickupDate" HeaderText="Pickup" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                                <asp:BoundField DataField="Cancelled" HeaderText="Cancelled" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                            </Columns>
                        </asp:GridView>
                        <asp:ObjectDataSource ID="odsShipments" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ViewLTLShipments" >
                            <SelectParameters>
                                <asp:Parameter Name="clientNumber" DefaultValue="" ConvertEmptyStringToNull="false" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnCancel" EventName="Command" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <br />
            <div>
                <asp:UpdatePanel ID="upnlShipmentsCommands" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnNew" runat="server" Text="  New  " CommandName="New" OnCommand="OnShipmentCommand" />
                    <asp:Button ID="btnUpdate" runat="server" Text="Edit" CommandName="Update" OnCommand="OnShipmentCommand" />
                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClientClick="return confirm('Cancel selected shipment?');" CommandName="Cancel" OnCommand="OnShipmentCommand" />
                    <asp:Button ID="btnBOL" runat="server" Text="View BOL" />
                    <asp:Button ID="btnLabels" runat="server" Text="View Labels" />
                </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="grdShipments" EventName="SelectedIndexChanged" />
                    </Triggers>
                </asp:UpdatePanel>
            </div>
            <br />
        </div>
        <div id="tabSearch">
            <asp:ValidationSummary ID="vsSearch" runat="server" ValidationGroup="vgSearch" />
            <br />
            <div class="sublabel">Search criteria...</div>
            <div class="search">
                <div style="margin:5px 0px">
                    <label for="txtShipDateStart">Ship Date</label>
                    <asp:TextBox ID="txtShipDateStart" runat="server" Width="100px" />
                    &nbsp;-&nbsp;
                    <asp:TextBox ID="txtShipDateEnd" runat="server" Width="100px" />
                </div>
                <div style="margin:5px 0px">
                    <label for="cboShippers">Shipper</label><asp:DropDownList ID="cboShippers" runat="server" Width="300px" AppendDataBoundItems="true" DataSourceID="odsShippers" DataTextField="Name" DataValueField="ShipperNumber" />
                </div>
                <div style="margin:5px 0px">
                    <label for="cboConsignees">Consignee</label><asp:DropDownList ID="cboConsignees" runat="server" Width="300px" AppendDataBoundItems="true" DataSourceID="odsConsignees" DataTextField="Name" DataValueField="ConsigneeNumber" />
                </div>
            </div>
            <br />
            <div class="gridviewbody">
                <asp:UpdatePanel runat="server" ID="upnlSearch" UpdateMode="Conditional">
                    <ContentTemplate>
                        <asp:GridView ID="grdSearch" runat="server" Width="100%" AutoGenerateColumns="false" DataSourceID="odsSearch" DataKeyNames="ShipmentNumber,ShipDate,PickupID,Cancelled" AllowSorting="true" OnSelectedIndexChanged="OnShipmentSelected">
                            <Columns>
                                <asp:CommandField HeaderStyle-Width="16px" ButtonType="Image" ShowSelectButton="True" SelectImageUrl="~/App_Themes/Argix/Images/select.gif" />
                                <asp:TemplateField HeaderText="Shipment#" ItemStyle-Width="75px" ItemStyle-HorizontalAlign="Left" SortExpression="ShipmentNumber">
                                    <ItemTemplate>
                                        <a class="popupLink" href="" onclick="javascript:var w=window.open('<%# "Tracking.aspx?number=" + Eval("ShipmentNumber").ToString() %>','_blank','width=800px,height=500px,resizable=no,scrollbars=yes,titlebar=no,menubar=no,toolbar=yes,status=no');return false;" title="Click here to track this shipment.">&nbsp;<%# Eval("ShipmentNumber").ToString() %></a>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="BLNumber" HeaderText="Ref#" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="True" />
                                <asp:BoundField DataField="ShipDate" HeaderText="Ship Date" ItemStyle-Wrap="False" ItemStyle-Width="75px" SortExpression="ShipDate" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                                <asp:BoundField DataField="ClientNumber" HeaderText="ClientNumber" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ClientName" HeaderText="Client" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ShipperNumber" HeaderText="ShipperNumber" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ShipperName" HeaderText="Shipper" ItemStyle-Wrap="True" ItemStyle-Width="150px" Visible="True" />
                                <asp:BoundField DataField="ConsigneeNumber" HeaderText="ConsigneeNumber" ItemStyle-Wrap="False" Visible="False" />
                                <asp:BoundField DataField="ConsigneeName" HeaderText="Consignee" ItemStyle-Wrap="True" ItemStyle-Width="150px" Visible="True" />
                                <asp:BoundField DataField="Pallets" HeaderText="Pallets" ItemStyle-Wrap="False" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Right" Visible="True" />
                                <asp:BoundField DataField="Weight" HeaderText="Weight" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:N0}" Visible="True" />
                                <asp:BoundField DataField="TotalCharge" HeaderText="Charge" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Right" DataFormatString="{0:C2}" Visible="True" />
                                <asp:BoundField DataField="TerminalCode" HeaderText="Terminal" ItemStyle-Wrap="False" ItemStyle-Width="50px" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <asp:BoundField DataField="LTLZone" HeaderText="Zone" ItemStyle-Wrap="False" ItemStyle-Width="25px" ItemStyle-HorizontalAlign="Center" Visible="False" />
                                <asp:BoundField DataField="Created" HeaderText="Created" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="False" />
                                <asp:BoundField DataField="PickupID" HeaderText="Pickup#" ItemStyle-Wrap="False" ItemStyle-Width="75px" Visible="False" />
                                <asp:BoundField DataField="PickupDate" HeaderText="Pickup" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                                <asp:BoundField DataField="Cancelled" HeaderText="Cancelled" ItemStyle-Wrap="False" ItemStyle-Width="75px" DataFormatString="{0:MM/dd/yyyy}" HtmlEncode="False" Visible="True" />
                            </Columns>
                        </asp:GridView>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <br />
            <div>
                <asp:Button ID="btnSearch" runat="server" Text="  Search  " ValidationGroup="vgSearch" CommandName="Search" OnCommand="OnShipmentCommand" />
            </div>
            <br />
        </div>
    </div>
    <asp:ObjectDataSource ID="odsShippers" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ReadLTLShippersList">
        <SelectParameters>
            <asp:Parameter Name="clientNumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsConsignees" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ReadLTLConsigneesList">
        <SelectParameters>
            <asp:Parameter Name="clientNumber" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsSearch" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="SearchLTLShipments" >
        <SelectParameters>
            <asp:ControlParameter Name="shipDateStart" ControlID="txtShipDateStart" PropertyName="Text" Type="DateTime" />
            <asp:ControlParameter Name="shipDateEnd" ControlID="txtShipDateEnd" PropertyName="Text" Type="DateTime" />
            <asp:Parameter Name="clientNumber" Type="String" />
            <asp:ControlParameter Name="shipperNumber" ControlID="cboShippers" PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter Name="consigneeNumber" ControlID="cboConsignees" PropertyName="SelectedValue" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</asp:Content>
