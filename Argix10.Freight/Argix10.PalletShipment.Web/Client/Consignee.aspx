<%@ Page Title="LTL Consignee" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Consignee.aspx.cs" Inherits="Consignee" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <script charset="UTF-8" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3&mkt=en-us"></script>   
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
<script type="text/jscript">
    $(document).ready(function () {
        $("#<%=txtContactPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
        $("#<%=txtWindowOpen.ClientID %>").inputmask({ "mask": "99:99" });
        $("#<%=txtWindowClose.ClientID %>").inputmask({ "mask": "99:99" });
    });
</script>
<div class="subtitle">Consignee for <asp:Label ID="lblClientName" runat="server" Text="" /></div>
<asp:ValidationSummary ID="vsConsignee" runat="server" ValidationGroup="vgConsignee" />
<div style="float:left">
    <table>
        <tr><td class="labelx">Zip Code</td></tr>
        <tr><td>
            <asp:UpdatePanel runat="server" ID="upnlZip" UpdateMode="Always" RenderMode="Inline"><ContentTemplate><asp:TextBox ID="txtZip" runat="server" Width="75px" AutoPostBack="true" OnTextChanged="OnZipChanged" /></ContentTemplate></asp:UpdatePanel>
        </td></tr>
    </table>
    <br />
    <div class="redline"></div>
    <br />
    <table>
        <tr><td class="labelx">Name</td></tr>
        <tr><td><asp:TextBox ID="txtName" runat="server" Width="275px" ReadOnly="true" /></td></tr>
        <tr><td class="labelx">Address</td></tr>
        <tr><td><asp:UpdatePanel runat="server" ID="upnlAddress" UpdateMode="Always" RenderMode="Inline"><ContentTemplate>
                <asp:TextBox ID="txtStreet" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Width="175px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Width="50px" />
        </ContentTemplate></asp:UpdatePanel></td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:17px">Street</div><div class="sublabel" style="float:left; width:175px; margin-right:17px">City</div><div class="sublabel" style="float:left; width:50px; margin-right:17px">State</div></td></tr>
        <tr><td class="labelx">Contact Info</td></tr>
        <tr><td><asp:TextBox ID="txtContactName" runat="server" Width="275px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Width="100px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactEmail" runat="server" Width="175px" />
        </td></tr>
        <tr><td><div class="sublabel" style="float:left; width:275px; margin-right:17px">Name</div><div class="sublabel" style="float:left; width:100px; margin-right:17px">Phone Number</div><div class="sublabel" style="float:left; width:175px">Email Address</div></td></tr>
        <tr><td class="labelx">Hours of Operation</td></tr>
        <tr><td><asp:TextBox ID="txtWindowOpen" runat="server" Width="75px" />
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWindowClose" runat="server" Width="75px" /></td></tr>
    </table>
    <br />
    <br />
    <div>
        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgConsignee" CssClass="submit" CommandName="Submit" OnCommand="OnCommand" />
        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="submit" CommandName="Cancel" OnCommand="OnCommand" />
    </div>
    <br />
    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ValidationGroup="vgConsignee" />
    <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtStreet" ErrorMessage="Street is required." ValidationGroup="vgConsignee" />
    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." ValidationGroup="vgConsignee" />
    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" ErrorMessage="State is required." ValidationGroup="vgConsignee" />
    <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip" ErrorMessage="Zip is required." ValidationGroup="vgConsignee" />
    <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" ErrorMessage="Contact name is required." ValidationGroup="vgConsignee" />
    <asp:RequiredFieldValidator ID="rfvContactEmail" runat="server" ControlToValidate="txtContactEmail" ErrorMessage="Contact email is required." ValidationGroup="vgConsignee" />
</div>
<div style="float:left; margin-left:25px; margin-top:10px; padding:0 0 0 0; border-style:solid; border-color:#ee2a24">
    <div id='myMap' style="position:relative; width:265px; height:325px"></div>
</div>
<div style="clear:both"></div>

<script type="text/javascript">
    var map = new VEMap('myMap');
    map.LoadMap();
    updateMap();

    function updateMap() {
        var street = document.getElementById('<%=txtStreet.ClientID %>').value;
        var city = document.getElementById('<%=txtCity.ClientID %>').value;
        var state = document.getElementById('<%=txtState.ClientID %>').value;
        var zip = document.getElementById('<%=txtZip.ClientID %>').value;

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
