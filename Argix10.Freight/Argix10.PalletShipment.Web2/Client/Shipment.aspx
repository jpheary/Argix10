<%@ Page Title="Book Quote" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Shipment.aspx.cs" Inherits="_Shipment" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $.widget("ui.timespinner", $.ui.spinner, {
        options: { step: 60000, page: 60 },
        _parse: function (value) {
            if (typeof value === "string") {
                if (Number(value) == value) {
                    return Number(value);
                }
                return +Globalize.parseDate(value);
            }
            return value;
        },
        _format: function (value) {
            return Globalize.format(new Date(value), "t");
        }
    });
    $(document).ready(function () {
        $("#<%=txtShipDate.ClientID %>").datepicker({ minDate: +1, maxDate: +30, beforeShowDay: checkDate });
        function checkDate(date) {
            var day = date.getDay();
            return (day != 0 && day != 6) ? [true] : [false];
        }

        $("#<%=txtWeight1.ClientID %>").spinner({ min: 0, max: 2000, stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtWeight2.ClientID %>").spinner({ min: 0, max: 2000, stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtWeight3.ClientID %>").spinner({ min: 0, max: 2000, stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtWeight4.ClientID %>").spinner({ min: 0, max: 2000, stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtWeight5.ClientID %>").spinner({ min: 0, max: 2000, stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtInsuranceValue1.ClientID %>").spinner({ min: 0, max: 10000, numberFormat: "C", stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtInsuranceValue2.ClientID %>").spinner({ min: 0, max: 10000, numberFormat: "C", stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtInsuranceValue3.ClientID %>").spinner({ min: 0, max: 10000, numberFormat: "C", stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtInsuranceValue4.ClientID %>").spinner({ min: 0, max: 10000, numberFormat: "C", stop: function (event, ui) { resetQuote(); } });
        $("#<%=txtInsuranceValue5.ClientID %>").spinner({ min: 0, max: 10000, numberFormat: "C", stop: function (event, ui) { resetQuote(); } });

        $("#<%=txtShipperApptStart.ClientID %>").timespinner({
            change: function (event, ui) {
                var f = $("#<%=txtShipperApptStart.ClientID %>").timespinner("value");
                var t = $("#<%=txtShipperApptEnd.ClientID %>").timespinner("value");
                if (f > t) {
                    $("#<%=txtShipperApptStart.ClientID %>").timespinner("value", t);
                    alert("Open time cannot be after close time.");
                }
            }
        });
        $("#<%=txtShipperApptEnd.ClientID %>").timespinner({
            change: function (event, ui) {
                var f = $("#<%=txtShipperApptStart.ClientID %>").timespinner("value");
                var t = $("#<%=txtShipperApptEnd.ClientID %>").timespinner("value");
                if (t < f) {
                    $("#<%=txtShipperApptEnd.ClientID %>").timespinner("value", f);
                    alert("Close time cannot be before open time.");
                }
            }
        });
        $("#<%=txtConsigneeApptStart.ClientID %>").timespinner({
            change: function (event, ui) {
                var f = $("#<%=txtConsigneeApptStart.ClientID %>").timespinner("value");
                var t = $("#<%=txtConsigneeApptEnd.ClientID %>").timespinner("value");
                if (f > t) {
                    $("#<%=txtConsigneeApptStart.ClientID %>").timespinner("value", t);
                    alert("Open time cannot be after close time.");
                }
            }
        });
        $("#<%=txtConsigneeApptEnd.ClientID %>").timespinner({
            change: function (event, ui) {
                var f = $("#<%=txtConsigneeApptStart.ClientID %>").timespinner("value");
                var t = $("#<%=txtConsigneeApptEnd.ClientID %>").timespinner("value");
                if (t < f) {
                    $("#<%=txtConsigneeApptEnd.ClientID %>").timespinner("value", f);
                    alert("Close time cannot be before open time.");
                }
            }
        });

        $("#<%=btnQuote.ClientID %>").button();
        $("#<%=btnSubmit.ClientID %>").button();
        $("#<%=btnCancel.ClientID %>").button();
    });

    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
    Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
    function OnBeginRequest(sender, args) { }
    function OnEndRequest(sender, args) {
        $("#<%=btnQuote.ClientID %>").button();
        $("#<%=btnSubmit.ClientID %>").button();
        $("#<%=btnCancel.ClientID %>").button();
    }

    function isNumeric(control) {
        var val = document.getElementById(control.id).value.replace("$", "");
        if (!$.isNumeric(val)) {
            document.getElementById(control.id).value = '';
            document.getElementById(control.id).focus();
            alert("Numeric values only.");
        }
        resetQuote();
    }
    function resetQuote() {
        document.getElementById('<%=txtPallets.ClientID %>').value = '';
        document.getElementById('<%=txtWeight.ClientID %>').value = '';
        document.getElementById('<%=txtRate.ClientID %>').value = '';
        document.getElementById('<%=txtFSC.ClientID %>').value = '';
        document.getElementById('<%=txtAccessorial.ClientID %>').value = '';
        document.getElementById('<%=txtInsurance.ClientID %>').value = '';
        document.getElementById('<%=txtTSC.ClientID %>').value = '';
        document.getElementById('<%=txtCharges.ClientID %>').value = '';

        document.getElementById('<%=btnSubmit.ClientID %>').disabled = 'disabled';
    }
</script>
<div class="subtitle">Shipment Quote for <asp:Label ID="lblClientName" runat="server" Text="" /></div>
<asp:ValidationSummary ID="vsQuote" runat="server" ValidationGroup="vgQuote" />
<div class="quote">
    <table>
        <tr><td><label for="ddlTerms">Terms</label></td>
            <td><asp:DropDownList ID="ddlTerms" runat="server" Width="100px"><asp:ListItem Text="Prepaid" Value="Prepaid" Selected="True" /></asp:DropDownList></td>
            <td>&nbsp;</td>
        </tr>
        <tr><td><label for="txtShipDate">Ship Date</label></td>
            <td>
                <asp:TextBox ID="txtShipDate" runat="server" Width="100px" onchange="javascript: resetQuote();" />
                &nbsp;
                <asp:UpdatePanel runat="server" ID="upnlTransit" UpdateMode="Always" RenderMode="Inline"><ContentTemplate>&nbsp;<i><asp:Label id="lblEstimatedDeliveryDate" runat="server" /></i></ContentTemplate></asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td style="vertical-align:text-top"><label for="ddlShippers">Shipper</label></td>
            <td>
                <asp:DropDownList ID="ddlShippers" runat="server" Width="250px" AppendDataBoundItems="true" DataSourceID="odsShippers" DataTextField="Name" DataValueField="ShipperNumber" onchange="javascript: resetQuote();" AutoPostBack="true" OnSelectedIndexChanged="OnShipperChanged">
                    <asp:ListItem Text="" Value="" Selected="true" />
                    <asp:ListItem Text="Add a New Shipper" Value="New" />
                </asp:DropDownList>
                &nbsp;<a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('../PickupMap.aspx','','dialogWidth:610px;dialogHeight:410px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;" title="Click here to see a map of our pickup region.">&nbsp;Map</a>
                <br />
                <asp:UpdatePanel runat="server" ID="upnlShiAdd" UpdateMode="Conditional" RenderMode="Inline"><ContentTemplate>
                    <asp:Label ID="lblShipperAddress" runat="server" Text="" Width="500px" Height="15px" style="margin-top:5px; padding:2px 0px 0px 5px; border:0px solid #cccccc" />
                </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="ddlShippers" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>
            </td>
            <td></td>
        </tr>
        <tr><td style="vertical-align:top"><label for="ddlConsignees">Consignee</label></td>
            <td>
                <asp:DropDownList ID="ddlConsignees" runat="server" Width="250px" AppendDataBoundItems="true" DataSourceID="odsConsignees" DataTextField="Name" DataValueField="ConsigneeNumber" onchange="javascript: resetQuote();" AutoPostBack="true" OnSelectedIndexChanged="OnConsigneeChanged">
                    <asp:ListItem Text="" Value="" Selected="true" />
                    <asp:ListItem Text="Add a New Consignee" Value="New" />
                </asp:DropDownList>
                <br />
                <asp:UpdatePanel runat="server" ID="upnlConAdd" UpdateMode="Conditional" RenderMode="Inline"><ContentTemplate>
                    <asp:Label ID="lblConsigneeAddress" runat="server" Text="" Width="500px" Height="15px" style="margin-top:5px; padding:2px 0px 0px 5px; border:0px solid #cccccc" />
                </ContentTemplate><Triggers><asp:AsyncPostBackTrigger ControlID="ddlConsignees" EventName="SelectedIndexChanged" /></Triggers></asp:UpdatePanel>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvShipDate" runat="server" ControlToValidate="txtShipDate" ErrorMessage="Shipping Date is required." ValidationGroup="vgQuote" />
    <asp:RequiredFieldValidator ID="rfvOriginZip" runat="server" ControlToValidate="ddlShippers" ErrorMessage="Shipper is required." ValidationGroup="vgQuote" />
    <asp:RequiredFieldValidator ID="rfvConsignees" runat="server" ControlToValidate="ddlConsignees" ErrorMessage="Consignee is required." ValidationGroup="vgQuote" />
    <br />
    <table class="quotefreight">
        <tr><td>&nbsp;</td><td class="labelC">Weight</td><td class="labelC">Class</td><td class="labelC">Insured Value</td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight1">Pallet 1</label></td><td><asp:TextBox ID="txtWeight1" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass1" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue1" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight2">Pallet 2</label></td><td><asp:TextBox ID="txtWeight2" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass2" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue2" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight3">Pallet 3</label></td><td><asp:TextBox ID="txtWeight3" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass3" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue3" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight4">Pallet 4</label></td><td><asp:TextBox ID="txtWeight4" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass4" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue4" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight5">Pallet 5</label></td><td><asp:TextBox ID="txtWeight5" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass5" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue5" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
    </table>
    <br />
    <table><tr><td><label for="txtBOLNumber">Client Ref#</label></td><td><asp:TextBox ID="txtBOLNumber" runat="server" Width="180px" MaxLength="20" /></td><td>&nbsp;</td></tr></table>
    <asp:ObjectDataSource ID="odsShippers" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ReadLTLShippersList">
        <SelectParameters><asp:Parameter Name="clientNumber" Type="String" /></SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsConsignees" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ReadLTLConsigneesList">
        <SelectParameters><asp:Parameter Name="clientNumber" Type="String" /></SelectParameters>
    </asp:ObjectDataSource>
</div>
<div class="accessorials">
    <fieldset>
        <legend>Assessorial Options</legend>
        <br />
        <table>
            <tr><th>Pickup</th></tr>
            <tr><td><asp:CheckBox ID="chkShipperInsidePickup" runat="server" Text="   Inside Pickup" Width="175px" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkShipperLiftGate" runat="server" Text="   Lift Gate Required" Width="175px" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkShipperAppt" runat="server" Text="   Appointment" AutoPostBack="true" OnCheckedChanged="OnShipperApptChecked" />
                    <asp:Panel ID="pnlShipperAppt" runat="server" style="margin:5px 0px 0px 20px">
                        <asp:TextBox ID="txtShipperApptStart" runat="server" Width="50px" onchange="javascript: resetQuote();" ToolTip="Enter an appointment start time (i.e. 9:00 AM)" />
                        &nbsp-&nbsp<asp:TextBox ID="txtShipperApptEnd" runat="server" Width="50px" onchange="javascript: resetQuote();" ToolTip="Enter an appointment end time (i.e. 3:00 PM)" />
                    </asp:Panel>
            </td></tr>
        </table>
        <br />
        <table>
            <tr><th>Delivery</th></tr>
            <tr><td><asp:CheckBox ID="chkConsigneeInsidePickup" runat="server" Text="   Inside Delivery" Width="175px" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkConsigneeLiftGate" runat="server" Text="   Lift Gate Required" Width="175px" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkConsigneeAppt" runat="server" Text="   Appointment" AutoPostBack="true" OnCheckedChanged="OnConsigneeApptChecked" />
                    <asp:Panel ID="pnlConsigneeAppt" runat="server" style="margin:5px 0px 0px 20px">
                        <asp:TextBox ID="txtConsigneeApptStart" runat="server" Width="50px" onchange="javascript: resetQuote();" ToolTip="Enter an appointment start time (i.e. 9:00 AM)" />
                        &nbsp-&nbsp<asp:TextBox ID="txtConsigneeApptEnd" runat="server" Width="50px" onchange="javascript: resetQuote();" ToolTip="Enter an appointment end time (i.e. 3:00 PM)" />
                    </asp:Panel>
            </td></tr>
        </table>
    </fieldset>
    <br />
    <div style="margin:10px 25px 10px 0px; text-align:right">
        <asp:UpdatePanel ID="upnlShipmentCommands" runat="server" UpdateMode="Always" >
        <ContentTemplate>
        <asp:Button ID="btnQuote" runat="server" Text="Quote" ValidationGroup="vgQuote" CommandName="Quote" OnCommand="OnCommand" />
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgQuote" CommandName="Submit" OnCommand="OnCommand" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" OnCommand="OnCommand" />
        </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
</div>
<div style="clear:both"></div>
<div class="redline"></div>
<br />
<asp:UpdatePanel runat="server" ID="upnlResponse" UpdateMode="Conditional">
<ContentTemplate>
    <div class="quoterates">
        <table>
            <tr><th>&nbsp;</th>
                <th>Pallets</th>
                <th>Weight</th>
                <th>Pallet Rate</th>
                <th title="Fuel Surcharge">FSC</th>
                <th>Accessorial</th>
                <th>Insurance</th>
                <th title="Toll Surcharge">TSC</th>
                <th>&nbsp;</th>
                <th>Total Charges</th>
                <th>&nbsp;</th>
            </tr>
            <tr><td><label>Quote</label></td>
                <td><asp:TextBox ID="txtPallets" runat="server" Width="75px" ReadOnly="true" /></td>
                <td><asp:TextBox ID="txtWeight" runat="server" Width="75px" ReadOnly="true" /></td>
                <td><asp:TextBox ID="txtRate" runat="server" Width="75px" ReadOnly="true" /></td>
                <td><asp:TextBox ID="txtFSC" runat="server" Width="75px" ReadOnly="true" ToolTip="Fuel Surcharge" /></td>
                <td><asp:TextBox ID="txtAccessorial" runat="server" Width="75px" ReadOnly="true" /></td>
                <td><asp:TextBox ID="txtInsurance" runat="server" Width="75px" ReadOnly="true" /></td>
                <td><asp:TextBox ID="txtTSC" runat="server" Width="75px" ReadOnly="true" ToolTip="Toll Surcharge" /></td>
                <td style="width:200px">&nbsp;</td>
                <td><asp:TextBox ID="txtCharges" runat="server" Width="100px" ReadOnly="true" CssClass="numericT" /></td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        Rate Quote is based upon entered data. Final invoice will include charges for any services required in the movement of your
        shipment that may have been omitted in the QUICK RATE QUOTE process. No HazMat or Perishable Freight accepted; click here for 
        a full list of <a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('../TermsConditions.aspx','','dialogWidth:600px;dialogHeight:400px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;">Terms and Conditions.</a> 
        Any questions please call 732.656.2550 or email <a href="mailto:extranet.support@argixlogistics.com" target="_top">Argix Logistics, Inc.</a>. 
    </div>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="txtShipDate" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="ddlShippers" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="ddlConsignees" EventName="SelectedIndexChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight1" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight2" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight3" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight4" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight5" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkShipperInsidePickup" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkShipperLiftGate" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkConsigneeInsidePickup" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkConsigneeLiftGate" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="btnQuote" EventName="Click" />
    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>
