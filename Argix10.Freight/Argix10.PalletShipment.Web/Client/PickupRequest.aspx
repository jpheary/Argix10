<%@ Page Title="Pickup Request" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="PickupRequest.aspx.cs" Inherits="_PickupRequest" %>
<%@ MasterType VirtualPath="~//MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <script charset="UTF-8" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3&mkt=en-us"></script>   
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=txtPickupDate.ClientID %>").datepicker({ minDate: +1 });
        $("#<%=txtShipperPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
        $("#<%=txtWindowOpen.ClientID %>").inputmask({ "mask": "99:99" });
        $("#<%=txtWindowClose.ClientID %>").inputmask({ "mask": "99:99" });
    });

    function isNumeric(control) {
        if (!$.isNumeric($('#' + control.id).val())) {
            document.getElementById(control.id).value = '';
            document.getElementById(control.id).focus();
            alert("Numeric values only.");
        }
    }
</script>
<div class="subtitle">Pickup Request for <asp:Label ID="lblClientName" runat="server" Text="" /></div>
<asp:ValidationSummary ID="vsPickup" runat="server" ValidationGroup="vgPickup" />
<div style="float:left">
    <table>
        <tr>
            <td class="labelx" style="width:150px">Pickup Date</td>
            <td class="labelx" style="width:150px">Shipper Zip Code</td>
            <td class="labelx" style="width:150px">Consignee Zip Code</td>
        </tr>
        <tr>
            <td><asp:TextBox ID="txtPickupDate" runat="server" Width="100px" /></td>
            <td>
                <asp:UpdatePanel runat="server" ID="upnlOrigin" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtShipperZip" runat="server" Width="50px" AutoPostBack="true" OnTextChanged="OnShipperZipChanged" onchange="javascript: updateMap();" /></ContentTemplate></asp:UpdatePanel>
                <a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('../PickupMap.aspx','','dialogWidth:603px;dialogHeight:403px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;" title="Click here to see a map of our pickup region.">&nbsp;Map</a>
            </td>
            <td><asp:UpdatePanel runat="server" ID="upnlDest" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtConsigneeZip" runat="server" Width="50px" AutoPostBack="true" OnTextChanged="OnConsigneeZipChanged" onchange="javascript: updateMap();" /></ContentTemplate></asp:UpdatePanel></td>
        </tr>
    </table>
    <br />
    <div class="redline"></div>
    <br />
    <table>
        <tr><td colspan="2" class="labelx">Shipper Name</td></tr>
        <tr><td colspan="2"><asp:TextBox ID="txtShipperName" runat="server" Width="275px" /></td></tr>
        <tr><td colspan="2" class="labelx">Shipper Address</td></tr>
        <tr><td colspan="2"><asp:UpdatePanel runat="server" ID="upnlAddress" UpdateMode="Always" RenderMode="Inline"><ContentTemplate>
                <asp:TextBox ID="txtShipperStreet" runat="server" Width="225px" onchange="javascript: updateMap();" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtShipperCity" runat="server" Width="150px" onchange="javascript: updateMap();" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtShipperState" runat="server" MaxLength="2" Width="50px" onchange="javascript: updateMap();" />
        </ContentTemplate></asp:UpdatePanel></td></tr>
        <tr><td colspan="2"><div class="sublabel" style="float:left; width:225px; margin-right:17px">Street</div><div class="sublabel" style="float:left; width:150px; margin-right:17px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:17px">State</div></td></tr>
        <tr><td class="labelx" style="width:150px">Shipper Phone#</td>
            <td class="labelx">Hours of Operation (0 - 2359)</td>
        </tr>
        <tr><td><asp:TextBox ID="txtShipperPhone" runat="server" Width="100px" /></td>
            <td><asp:TextBox ID="txtWindowOpen" runat="server" Width="75px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWindowClose" runat="server" Width="75px" /></td>
        </tr>
        <tr><td colspan="2"><div class="sublabel" style="float:left; width:225px; margin-right:17px; height: 9px;">&nbsp;</div><div class="sublabel" style="float:left; width:75px; margin-right:17px">Open</div><div class="sublabel" style="float:left; width:75px; margin-right:17px">Close</div></td></tr>
        <tr><td colspan="2" class="labelx">Freight</td></tr>
        <tr><td colspan="2"><asp:TextBox ID="txtQuantity" runat="server" MaxLength="2" Width="50px" style="text-align:right" onchange="javascript: isNumeric(this);" />&nbsp;&nbsp;<span class="sublabel">Pallets</span>
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWeight" runat="server" Width="75px" style="text-align:right" onchange="javascript: isNumeric(this);" />&nbsp;&nbsp;<span class="sublabel">Lbs</span>
        </td></tr>
        <tr><td colspan="2" class="labelx">Comments</td></tr>
        <tr><td colspan="2"><asp:TextBox ID="txtComments" runat="server" Width="500px" /></td></tr>
    </table>
    <br />
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgPickup" CssClass="submit" CommandName="Submit" OnCommand="OnCommand" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CommandName="Cancel" OnCommand="OnCommand" Visible="true" />
    </div>
    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtShipperName" ErrorMessage="Shipper name is required." ValidationGroup="vgPickup" />
    <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtShipperStreet" ErrorMessage="Shipper street is required." ValidationGroup="vgPickup" />
    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtShipperCity" ErrorMessage="Shipper city is required." ValidationGroup="vgPickup" />
    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtShipperState" ErrorMessage="Shipper state is required." ValidationGroup="vgPickup" />
    <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtShipperZip" ErrorMessage="Shipper zip is required." ValidationGroup="vgPickup" />
    <asp:RequiredFieldValidator ID="rfvShipperPhone" runat="server" ControlToValidate="txtShipperPhone" ErrorMessage="Shipper phone number is required." ValidationGroup="vgPickup" />
</div>
<div style="float:left; padding:20px 0px 0px 50px">
    <div id='myMap' style="position:relative; width:265px; height:325px; border: 3px solid #ee2a24"></div>
</div>
<div style="clear:both"></div>

<script type="text/javascript">
    var map = new VEMap('myMap');
    map.LoadMap();
    updateMap();

    function updateMap() {
        var street = document.getElementById('<%=txtShipperStreet.ClientID %>').value;
        var city = document.getElementById('<%=txtShipperCity.ClientID %>').value;
        var state = document.getElementById('<%=txtShipperState.ClientID %>').value;
        var zip = document.getElementById('<%=txtShipperZip.ClientID %>').value;

        var address = street + ' ' + city + ', ' + state + ' ' + zip;
        MapLocation(address);
    }
    function MyHandleCredentialsError() { alert("The Bing Map credentials are invalid."); }
    function UnloadMap() { if (myMap != null) myMap.Dispose(); }
    function MapLocation(address) {
        var points = new Array(address);
        for (var i = 0; i < points.length; i++) 
            map.Find(null, points[i], null, null, 0, 10, false, false, false, true, ProcessStore);
    }
    function ProcessStore(layer, results, places, hasmore) {
        //Create a custom pin
        if (places != null && places[0].LatLong != 'Unavailable') {
            var spec = new VECustomIconSpecification();
            spec.CustomHTML = "<div style='font-size:8px; border:solid 1px Black; background-color:red; width:8px;'>&nbsp;<div>";
            var pin = new VEShape(VEShapeType.Pushpin, places[0].LatLong);
            pin.SetCustomIcon(spec);
            map.AddShape(pin);
        }
    }
    function callWebService(url) {
        //Calls web service with url and callback function; callback will be executed when XMLHttpRequest object returns from web service call
        var xmlDoc = new XMLHttpRequest();
        if (xmlDoc) {
            //Execute synchronous call to web service; asynchronous never returns a readystate > 1 with POST
            xmlDoc.onreadystatechange = function () { stateChange(xmlDoc); };
            xmlDoc.open("GET", url, true);
            //params = "name=" + document.infoForm.name.value + "&email=" + document.infoForm.email.value + "&phone=" + document.infoForm.phone.value + "&company=" + document.infoForm.company.value + "&address=" + document.infoForm.address.value + "&state=" + document.infoForm.state.value + "&options=" + document.infoForm.options.value;
            //xmlDoc.setRequestHeader("Content-length", params.length);
            xmlDoc.send(null);
        }
        else 
            alert("Unable to create XMLHttpRequest object.");
    }

    function stateChange(xmlDoc) {
        //Updates readystate by callback
        if (xmlDoc.readyState == 4) {
            var text = "";
            if (xmlDoc.status == 200) {
                //sSelect node containing data
                var nd = xmlDoc.responseXML.getElementsByTagName("mail");
                if (nd && nd.length == 1) {
                    //IE use .text, others .textContent
                    text = !nd[0].text ? nd[0].textContent : nd[0].text;
                    if (text != "") alert(text); else alert("Web service call failed: " + text);
                }
            }
            else
                alert("Bad response: status code=" + xmlDoc.status);
        }
    }
</script>
</asp:Content>
