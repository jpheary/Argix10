<%@ Page Title="Book Quote" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="BookQuote.aspx.cs" Inherits="BookQuote" %>
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

        var btn = document.getElementById('<%=btnBook.ClientID %>');
        if (btn != null) {
            document.getElementById('<%=btnBook.ClientID %>').disabled = 'disabled';
            document.getElementById('<%=btnBook.ClientID %>').className = 'aspNetDisabled submit';
        }
    }
</script>
<div class="subtitle">Shipment Quote for <asp:Label ID="lblClientName" runat="server" Text="" /></div>
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
        <tr><td class="label"><asp:HyperLink ID="lnkShippers" runat="server" NavigateUrl="~/Client/Manage.aspx?view=shippers">Shipper</asp:HyperLink></td>
            <td class="input">
                <asp:DropDownList ID="ddlShippers" runat="server" Width="250px" AppendDataBoundItems="true" DataSourceID="odsShippers" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnShipperChanged">
                    <asp:ListItem Text="" Value="" Selected="true" />
                    <asp:ListItem Text="Add a New Shipper" Value="New" />
                </asp:DropDownList>
            </td>
            <td><a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('../PickupMap.aspx','','dialogWidth:603px;dialogHeight:403px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;" title="Click here to see a map of our pickup region.">&nbsp;Map</a></td>
        </tr>
        <tr><td class="label"><asp:HyperLink ID="lnkConsignees" runat="server" NavigateUrl="~/Client/Manage.aspx?view=consignees">Consignee</asp:HyperLink></td>
            <td class="input">
                <asp:DropDownList ID="ddlConsignees" runat="server" Width="250px" AppendDataBoundItems="true" DataSourceID="odsConsignees" DataTextField="Name" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="OnConsigneeChanged">
                    <asp:ListItem Text="" Value="" Selected="true" />
                    <asp:ListItem Text="Add a New Consignee" Value="New" />
                </asp:DropDownList>
            </td>
            <td>&nbsp;</td>
        </tr>
    </table>
    <asp:RequiredFieldValidator ID="rfvShipDate" runat="server" ControlToValidate="txtShipDate" ErrorMessage="Shipping Date is required." ValidationGroup="vgQuote" />
    <br />
    <table>
        <tr><td class="label">&nbsp;</td><td class="labelC">Weight</td><td class="labelC">Class</td><td class="labelC">Insured Value</td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 1</td><td class="input"><asp:TextBox ID="txtWeight1" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass1" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue1" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 2</td><td class="input"><asp:TextBox ID="txtWeight2" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass2" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue2" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 3</td><td class="input"><asp:TextBox ID="txtWeight3" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass3" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue3" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 4</td><td class="input"><asp:TextBox ID="txtWeight4" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass4" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue4" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
        <tr><td class="label">Pallet 5</td><td class="input"><asp:TextBox ID="txtWeight5" runat="server" Width="100px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td class="inputC"><asp:DropDownList ID="ddlClass5" runat="server" Width="75px" ><asp:ListItem Text="FAK" Value="FAK" /></asp:DropDownList></td><td class="inputC"><asp:TextBox ID="txtInsuranceValue5" runat="server" Width="75px" CssClass="numeric" onchange="javascript: isNumeric(this);" /></td><td>&nbsp;</td></tr>
    </table>
    <asp:ObjectDataSource ID="odsShippers" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ReadLTLShippersList">
        <SelectParameters>
            <asp:Parameter Name="clientID" DefaultValue="" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="odsConsignees" runat="server" TypeName="Argix.Freight.FreightGateway" SelectMethod="ReadLTLConsigneesList">
        <SelectParameters>
            <asp:Parameter Name="clientID" DefaultValue="" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
</div>
<div style="float:left; margin-left:25px">
    <div class="sectiontitle">Assessorial Options</div>
    <table>
        <tr><th>Pickup</th></tr>
        <tr><td><asp:CheckBox ID="chkInsideO" runat="server" Text="   Inside Pickup" Width="200px" onchange="javascript: resetQuote();" /></td></tr>
        <tr><td><asp:CheckBox ID="chkLiftGateO" runat="server" Text="   Lift Gate Required" CssClass="sectionitem" Width="200px" onchange="javascript: resetQuote();" /></td></tr>
        <tr><td>Appointment&nbsp<asp:TextBox ID="txtApptO" runat="server" Width="100px" onchange="javascript: resetQuote();" /></td></tr>
    </table>
     <table>
        <tr><th>Delivery</th></tr>
        <tr><td><asp:CheckBox ID="chkInsideD" runat="server" Text="   Inside Delivery" Width="200px" onchange="javascript: resetQuote();" /></td></tr>
        <tr><td><asp:CheckBox ID="chkLiftGateD" runat="server" Text="   Lift Gate Required" CssClass="sectionitem" Width="200px" onchange="javascript: resetQuote();" /></td></tr>
        <tr><td>Appointment&nbsp<asp:TextBox ID="txtApptD" runat="server" Width="100px" onchange="javascript: resetQuote();" /></td></tr>
    </table>
    <br /><br /><br />
    <div><asp:Button ID="btnSubmit" runat="server" Text="Get Quote" ValidationGroup="vgQuote" CssClass="submit" OnClick="OnSubmit" /></div>
</div>
<div style="clear:both"></div>
<br />
<div class="redline"></div>
<br />
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
                <td style="padding-left:25px;"><asp:Button ID="btnBook" runat="server" Text="Book Quote" ValidationGroup="vgQuote" CssClass="submit" OnClick="OnBookQuote" /></td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </div>
    <br />
    <div>
        <div>
            Rate Quote is based upon entered data. Final invoice will include charges for any services required in the movement of your
            shipment that may have been omitted in the QUICK RATE QUOTE process. No HazMat or Perishable Freight accepted; click here for 
            a full list of <a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('../TermsConditions.aspx','','dialogWidth:500px;dialogHeight:325px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;">Terms and Conditions.</a> 
            Any questions please call 732.656.2550 or email <a href="mailto:extranet.support@argixlogistics.com" target="_top">Argix Logistics, Inc.</a>. 
        </div>
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
    <asp:AsyncPostBackTrigger ControlID="chkInsideO" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkLiftGateO" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtApptO" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkInsideD" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="chkLiftGateD" EventName="CheckedChanged" />
    <asp:AsyncPostBackTrigger ControlID="txtApptD" EventName="TextChanged" />
    <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
</Triggers>
</asp:UpdatePanel>
</asp:Content>
