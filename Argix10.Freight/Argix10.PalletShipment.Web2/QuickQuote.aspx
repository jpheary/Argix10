<%@ Page Title="Quick Quote" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="QuickQuote.aspx.cs" Inherits="QuickQuote" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
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

        $("#<%=btnQuote.ClientID %>").button();
        $("#<%=btnEnroll.ClientID %>").button();
    });

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
    }
</script>
<div class="subtitle">Quick Rate Quote</div>
<asp:ValidationSummary ID="vsQuote" runat="server" ValidationGroup="vgQuote" />
<div class="quote">
    <table>
        <tr><td><label for="ddlTerms">Terms</label></td>
            <td><asp:DropDownList ID="ddlTerms" runat="server" Width="100px"><asp:ListItem Text="Prepaid" Value="Prepaid" Selected="True" /></asp:DropDownList></td>
            <td>&nbsp;</td>
        </tr>
        <tr><td><label for="txtShipDate">Ship Date</label></td>
            <td><asp:TextBox ID="txtShipDate" runat="server" Width="100px" onchange="javascript: resetQuote();" /></td>
            <td><asp:UpdatePanel runat="server" ID="upnlTransit" UpdateMode="Always" RenderMode="Inline"><ContentTemplate>&nbsp;<i><asp:Label id="lblEstimatedDeliveryDate" runat="server" /></i></ContentTemplate></asp:UpdatePanel></td>
        </tr>
        <tr><td><label for="txtOriginZip">Orig Zip</label></td>
            <td><asp:UpdatePanel runat="server" ID="upnlOrigin" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtOriginZip" runat="server" Width="100px" MaxLength="5" AutoPostBack="true" OnTextChanged="OnOriginChanged" /></ContentTemplate></asp:UpdatePanel></td>
            <td><a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('PickupMap.aspx','','dialogWidth:610px;dialogHeight:410px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;" title="Click here to see a map of our pickup region.">&nbsp;Map</a></td>
        </tr>
        <tr><td><label for="txtDestZip">Dest Zip</label></td>
            <td><asp:UpdatePanel runat="server" ID="upnlDest" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtDestZip" runat="server" Width="100px" MaxLength="5" AutoPostBack="true" OnTextChanged="OnDestinationChanged" /></ContentTemplate></asp:UpdatePanel></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvShipDate" runat="server" ControlToValidate="txtShipDate" ErrorMessage="Shipping date is required." ValidationGroup="vgQuote" />
    <asp:RequiredFieldValidator ID="rfvOriginZip" runat="server" ControlToValidate="txtOriginZip" ErrorMessage="Origin zip code is required." ValidationGroup="vgQuote" />
    <asp:RequiredFieldValidator ID="rfvDestZip" runat="server" ControlToValidate="txtDestZip" ErrorMessage="Destination zip code is required." ValidationGroup="vgQuote" />
    <br />
    <table class="quotefreight">
        <tr><th>&nbsp;</th><th>Weight</th><th>Class</th><th>Insured Value</th><th>&nbsp;</th></tr>
        <tr><td><label for="txtWeight1">Pallet 1</label></td><td><asp:TextBox ID="txtWeight1" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass1" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue1" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight2">Pallet 2</label></td><td><asp:TextBox ID="txtWeight2" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass2" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue2" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight3">Pallet 3</label></td><td><asp:TextBox ID="txtWeight3" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass3" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue3" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight4">Pallet 4</label></td><td><asp:TextBox ID="txtWeight4" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass4" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue4" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td><label for="txtWeight5">Pallet 5</label></td><td><asp:TextBox ID="txtWeight5" runat="server" Width="50px" MaxLength="4" onchange="javascript: isNumeric(this);" /></td><td><asp:DropDownList ID="ddlClass5" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td><asp:TextBox ID="txtInsuranceValue5" runat="server" Width="75px" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
    </table>
    <br />
</div>
<div class="accessorials">
    <fieldset>
        <legend>Assessorial Options</legend>
        <br />
        <table>
            <tr><th>Pickup</th></tr>
            <tr><td><asp:CheckBox ID="chkInsideO" runat="server" Text="   Inside Pickup" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkLiftGateO" runat="server" Text="   Lift Gate Required" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkApptO" runat="server" Text="   Appointment" onclick="javascript: resetQuote();" /></td></tr>
        </table>
        <br />
        <table>
            <tr><th>Delivery</th></tr>
            <tr><td><asp:CheckBox ID="chkInsideD" runat="server" Text="   Inside Delivery" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkLiftGateD" runat="server" Text="   Lift Gate Required" onclick="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkApptD" runat="server" Text="   Appointment" onclick="javascript: resetQuote();" /></td></tr>
        </table>
    </fieldset>
    <br />
    <div style="margin:10px 25px 10px 0px; text-align:right">
        <asp:Button ID="btnQuote" runat="server" Text="Quote" ValidationGroup="vgQuote" CommandName="Quote" OnCommand="OnCommand" />
        <asp:Button ID="btnEnroll" runat="server" Text="Enroll" ValidationGroup="vgQuote" CommandName="Enroll" OnCommand="OnCommand" />
    </div>
    <br />
</div>
<div style="clear:both"></div>
<div class="redline"></div>
<br />
<asp:UpdatePanel runat="server" ID="pnlResponse" UpdateMode="Conditional">
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
        a full list of <a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('TermsConditions.aspx','','dialogWidth:600px;dialogHeight:400px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;">Terms and Conditions.</a> 
        <asp:Label ID="lblEnroll" runat="server"> If you are satisfied with the quotes and would like to become a Pallet Shipment client 
        of Argix Logistics please click on the Enroll link above. </asp:Label>Any questions please call 732.656.2550 or email <a href="mailto:extranet.support@argixlogistics.com" target="_top">Argix Logistics, Inc.</a>. 
    </div>
</ContentTemplate>
<Triggers>
    <asp:AsyncPostBackTrigger ControlID="txtShipDate" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtOriginZip" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtDestZip" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight1" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight2" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight3" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight4" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtWeight5" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkInsideO" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkLiftGateO" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkApptO" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkInsideD" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkLiftGateD" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkApptD" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="btnQuote" EventName="Click" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>
