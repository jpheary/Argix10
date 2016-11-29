<%@ Page Title="Quick Quote" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="QuickQuote.aspx.cs" Inherits="QuickQuote" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=txtShipDate.ClientID %>").datepicker({ minDate: +1, maxDate: +30 });
    });

    function isNumeric(control) {
        if (!$.isNumeric($('#' + control.id).val())) {
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
        
        var btn = document.getElementById('<%=btnEnroll.ClientID %>');
        if (btn != null) {
            document.getElementById('<%=btnEnroll.ClientID %>').disabled = 'disabled';
            document.getElementById('<%=btnEnroll.ClientID %>').className = 'aspNetDisabled submit';
        }
    }
</script>
<div class="subtitle">Quick Rate Quote</div>
<asp:ValidationSummary ID="vsQuote" runat="server" ValidationGroup="vgQuote" />
<div style="float:left; width:515px">
    <table>
        <tr><td class="label">Terms</td>
            <td class="input">
                <asp:DropDownList ID="ddlTerms" runat="server" Width="100px">
                    <asp:ListItem Text="Prepaid" Value="Prepaid" Selected="True" />
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
        <tr><td class="label">Shipping Date</td>
            <td class="input"><asp:TextBox ID="txtShipDate" runat="server" Width="100px" onchange="javascript: resetQuote();" /></td>
            <td>&nbsp;</td>
        </tr>
        <tr><td class="label">Origin Zip</td>
            <td class="input"><asp:UpdatePanel runat="server" ID="upnlOrigin" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtOriginZip" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="OnOriginChanged" /></ContentTemplate></asp:UpdatePanel></td>
            <td><a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('PickupMap.aspx','','dialogWidth:603px;dialogHeight:403px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;" title="Click here to see a map of our pickup region.">&nbsp;Map</a></td>
        </tr>
        <tr><td class="label">Destination Zip</td>
            <td class="input"><asp:UpdatePanel runat="server" ID="upnlDest" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtDestZip" runat="server" Width="100px" AutoPostBack="true" OnTextChanged="OnDestinationChanged" /></ContentTemplate></asp:UpdatePanel></td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvShipDate" runat="server" ControlToValidate="txtShipDate" ErrorMessage="Shipping date is required." ValidationGroup="vgQuote" />
    <asp:RequiredFieldValidator ID="rfvOriginZip" runat="server" ControlToValidate="txtOriginZip" ErrorMessage="Origin zip code is required." ValidationGroup="vgQuote" />
    <asp:RequiredFieldValidator ID="rfvDestZip" runat="server" ControlToValidate="txtDestZip" ErrorMessage="Destination zip code is required." ValidationGroup="vgQuote" />
    <br />
    <table>
        <tr><td class="label">&nbsp;</td><td class="labelC">Weight</td><td class="labelC">Class</td><td class="labelC">Insured Value</td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 1</td><td class="input"><asp:TextBox ID="txtWeight1" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass1" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue1" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 2</td><td class="input"><asp:TextBox ID="txtWeight2" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass2" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue2" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 3</td><td class="input"><asp:TextBox ID="txtWeight3" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass3" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue3" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 4</td><td class="input"><asp:TextBox ID="txtWeight4" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass4" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue4" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 5</td><td class="input"><asp:TextBox ID="txtWeight5" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass5" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue5" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
    </table>
</div>
<div style="float:left; margin-left:100px">
    <fieldset style="width:200px">
        <legend class="sectiontitle">Assessorial Options</legend>
        <table>
            <tr><th class="sectiontitle">Pickup</th></tr>
            <tr><td><asp:CheckBox ID="chkInsideO" runat="server" Text="Inside Pickup" CssClass="sectionitem" onchange="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkLiftGateO" runat="server" Text="Lift Gate Required" CssClass="sectionitem" onchange="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkApptO" runat="server" Text="Appointment" CssClass="sectionitem" onchange="javascript: resetQuote();" /></td></tr>
        </table>
        <br />
        <table>
            <tr><th class="sectiontitle">Delivery</th></tr>
            <tr><td><asp:CheckBox ID="chkInsideD" runat="server" Text="Inside Delivery" CssClass="sectionitem" onchange="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkLiftGateD" runat="server" Text="Lift Gate Required" CssClass="sectionitem" onchange="javascript: resetQuote();" /></td></tr>
            <tr><td><asp:CheckBox ID="chkApptD" runat="server" Text="Appointment" CssClass="sectionitem" onchange="javascript: resetQuote();" /></td></tr>
        </table>
    </fieldset>
    <br /><br /><br />
    <div><asp:Button ID="btnSubmit" runat="server" Text="Get Quote" ValidationGroup="vgQuote" CssClass="submit" OnClick="OnSubmit" /></div>
</div>
<div style="clear:both"></div>
<div class="redline"></div>
<asp:UpdatePanel runat="server" ID="pnlResponse" UpdateMode="Conditional">
<ContentTemplate>
    <div>
        <table>
            <tr><td class="labelC">Pallets</td>
                <td class="labelC">Weight</td>
                <td class="labelC">Pallet Rate</td>
                <td class="labelC">FSC</td>
                <td class="labelC">Accessorial</td>
                <td class="labelC">Insurance</td>
                <td class="labelC">TSC</td>
                <td class="labelC">Total Charges</td>
                <td>&nbsp;</td>
            </tr>
            <tr><td class="input"><asp:TextBox ID="txtPallets" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtWeight" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtRate" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtFSC" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtAccessorial" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtInsurance" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtTSC" runat="server" Width="75px" ReadOnly="true" CssClass="numeric" /></td>
                <td class="input"><asp:TextBox ID="txtCharges" runat="server" Width="100px" ReadOnly="true" CssClass="numericT" /></td>
                <td style="padding-left:25px;"><asp:Button ID="btnEnroll" runat="server" Text="   Enroll   " ValidationGroup="vgQuote" CssClass="submit" OnClick="OnEnroll" /></td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        Rate Quote is based upon entered data. Final invoice will include charges for any services required in the movement of your
        shipment that may have been omitted in the QUICK RATE QUOTE process. No HazMat or Perishable Freight accepted; click here for 
        a full list of <a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('TermsConditions.aspx','','dialogWidth:500px;dialogHeight:325px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;">Terms and Conditions.</a> 
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
    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>
