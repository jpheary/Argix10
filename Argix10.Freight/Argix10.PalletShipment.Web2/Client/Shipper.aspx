<%@ Page Title="LTL Shipper" Language="C#" MasterPageFile="~/MasterPages/Default.master" AutoEventWireup="true" CodeFile="Shipper.aspx.cs" Inherits="Shipper" %>
<%@ MasterType VirtualPath="~/MasterPages/Default.master" %>

<asp:Content ID="cMeta" runat="server" ContentPlaceHolderID="cpMeta">
    <script charset="UTF-8" type="text/javascript" src="http://ecn.dev.virtualearth.net/mapcontrol/mapcontrol.ashx?v=6.3&mkt=en-us"></script>   
</asp:Content>
<asp:Content ID="cContent" runat="server" ContentPlaceHolderID="cpBody">
    <div class="subtitle">Shipper for <asp:Label ID="lblClientName" runat="server" Text="" /></div>
    <asp:MultiView runat="server" ID="mvwPage" ActiveViewIndex="0">
    <asp:View ID="vwAddress" runat="server">
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
                jQueryBind();
            });

            Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(OnBeginRequest);
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(OnEndRequest);
            function OnBeginRequest(sender, args) { }
            function OnEndRequest(sender, args) {
                jQueryBind();
            }
            function jQueryBind() {
                $("#<%=txtContactPhone.ClientID %>").inputmask({ "mask": "999-999-9999" });
                $("#<%=txtWindowOpen.ClientID %>").timespinner({
                    change: function (event, ui) {
                        var f = $("#<%=txtWindowOpen.ClientID %>").timespinner("value");
                        var t = $("#<%=txtWindowClose.ClientID %>").timespinner("value");
                        if (f > t) {
                            $("#<%=txtWindowOpen.ClientID %>").timespinner("value", t);
                            alert("Open time cannot be after close time.");
                        }
                    }
                });
                $("#<%=txtWindowOpen.ClientID %>").timespinner("option", "disabled", true);
                $("#<%=txtWindowClose.ClientID %>").timespinner({
                    change: function (event, ui) {
                        var f = $("#<%=txtWindowOpen.ClientID %>").timespinner("value");
                        var t = $("#<%=txtWindowClose.ClientID %>").timespinner("value");
                        if (t < f) {
                            $("#<%=txtWindowClose.ClientID %>").timespinner("value", f);
                            alert("Close time cannot be before open time.");
                        }
                    }
                });
                $("#<%=txtWindowClose.ClientID %>").timespinner("option", "disabled", true);

                $("#<%=btnValidate.ClientID %>").button();
                $("#<%=btnSubmit.ClientID %>").button();
                $("#<%=btnCancel.ClientID %>").button();
            }
        </script>
        <div class="manage">
            <asp:ValidationSummary ID="vsShipper" runat="server" ValidationGroup="vgShipper" />
            <div class="address">
                <div>
                    <asp:UpdatePanel runat="server" ID="upnlZip" UpdateMode="Always" RenderMode="Inline">
                    <ContentTemplate>
                        <label for="txtZip5">Zip Code</label><asp:TextBox ID="txtZip5" runat="server" Width="75px" MaxLength="5" AutoPostBack="true" OnTextChanged="OnZipChanged" onchange="javascript: updateMap();" />&nbsp;-&nbsp;<asp:TextBox ID="txtZip4" runat="server" Width="40px" MaxLength="4" ReadOnly="true" />
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <a class="popupLink" href="" onclick="javascript:var w=window.showModelessDialog('../PickupMap.aspx','','dialogWidth:603px;dialogHeight:403px;center:yes;resizable:yes;scroll:yes;status:no;unadorned:yes');return false;" title="Click here to see a map of our pickup region.">&nbsp;Map</a>
                </div>
                <br />
                <div>
                    <asp:UpdatePanel runat="server" ID="upnlAddress" UpdateMode="Always" RenderMode="Inline">
                    <ContentTemplate>
                    <fieldset>
                        <legend>Address</legend>
                        <label for="txtName">Name</label><asp:TextBox ID="txtName" runat="server" Width="275px" MaxLength="40" /><br />
                        <label for="txtAddressLine1">Street</label><asp:TextBox ID="txtAddressLine1" runat="server" Width="275px" MaxLength="40" onchange="javascript: updateMap();" /><br />
                        <label for="txtAddressLine2">&nbsp;</label><asp:TextBox ID="txtAddressLine2" runat="server" Width="275px" MaxLength="40" onchange="javascript: updateMap();" /><br />
                        <label for="txtCity">City</label><asp:TextBox ID="txtCity" runat="server" Width="175px" MaxLength="40" onchange="javascript: updateMap();" /><br />
                        <label for="txtState">State</label><asp:TextBox ID="txtState" runat="server" Width="50px" MaxLength="2" onchange="javascript: updateMap();" /><br />
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>Contact</legend>
                        <label for="txtContactName">Name</label><asp:TextBox ID="txtContactName" runat="server" Width="200px" MaxLength="40" /><br />
                        <label for="txtContactPhone">Phone</label><asp:TextBox ID="txtContactPhone" runat="server" Width="100px" MaxLength="24" /><br />
                        <label for="txtContactEmail">Email</label><asp:TextBox ID="txtContactEmail" runat="server" Width="275px" MaxLength="50" /><br />
                    </fieldset>
                    <br />
                    <fieldset>
                        <legend>Availability</legend>
                        <label for="txtWindowOpen">Open</label><asp:TextBox ID="txtWindowOpen" runat="server" Width="100px" Enabled="false" />
                        &nbsp;&nbsp;-&nbsp;&nbsp;<asp:TextBox ID="txtWindowClose" runat="server" Width="100px" Enabled="false" /><br />
                    </fieldset>
                    </ContentTemplate>
                    </asp:UpdatePanel>
                    <br />
                    <div>
                        <asp:Button ID="btnValidate" runat="server" Text="Validate Address" CommandName="Validate" OnCommand="OnCommand" />&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgShipper" CommandName="Submit" OnCommand="OnCommand" />
                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" CommandName="Cancel" OnCommand="OnCommand" />
                    </div>
                    <br />
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." ValidationGroup="vgShipper" />
                    <asp:RequiredFieldValidator ID="rfvStreet" runat="server" ControlToValidate="txtAddressLine1" ErrorMessage="Street is required." ValidationGroup="vgShipper" />
                    <asp:RequiredFieldValidator ID="rfvCity" runat="server" ControlToValidate="txtCity" ErrorMessage="City is required." ValidationGroup="vgShipper" />
                    <asp:RequiredFieldValidator ID="rfvState" runat="server" ControlToValidate="txtState" ErrorMessage="State is required." ValidationGroup="vgShipper" />
                    <asp:RequiredFieldValidator ID="rfvZip" runat="server" ControlToValidate="txtZip5" ErrorMessage="Zip is required." ValidationGroup="vgShipper" />
                    <asp:RequiredFieldValidator ID="rfvContactName" runat="server" ControlToValidate="txtContactName" ErrorMessage="Contact name is required." ValidationGroup="vgShipper" />
                    <asp:RequiredFieldValidator ID="rfvContactPhone" runat="server" ControlToValidate="txtContactPhone" ErrorMessage="Contact phone number is required." ValidationGroup="vgShipper" />
                </div>
            </div>
            <div class="addressmap">
                <div id='myMap' style="position:relative; width:425px; height:383px"></div>
            </div>
            <div style="clear:both"></div>
        </div>
        <script type="text/javascript">
            var map = new VEMap('myMap');
            map.LoadMap();
            updateMap();

            function updateMap() {
                var street = document.getElementById('<%=txtAddressLine1.ClientID %>').value;
                var city = document.getElementById('<%=txtCity.ClientID %>').value;
                var state = document.getElementById('<%=txtState.ClientID %>').value;
                var zip = document.getElementById('<%=txtZip5.ClientID %>').value;

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
    </asp:View>
    <asp:View ID="vwVerifyAddress" runat="server">
        <script type="text/jscript">
            $(document).ready(function () {
                $("#<%=btnChooseAddress.ClientID %>").button();
                $("#<%=btnChooseUSPSAddress.ClientID %>").button();
            });
        </script>
        <div class="manage">
            <p>Verify Address: The US Postal Service suggests the following address information. Please choose the USPS address unless you are sure it is incorrect.</p>
            <div class="address">
                <fieldset>
                    <legend>Address</legend>
                    <label for="txtSrcName">Name</label><asp:TextBox ID="txtSrcName" runat="server" Width="250px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtSrcAddressLine1">Address</label><asp:TextBox ID="txtSrcAddressLine1" runat="server" Width="250px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtSrcAddressLine2">&nbsp;</label><asp:TextBox ID="txtSrcAddressLine2" runat="server" Width="250px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtSrcCity">City</label><asp:TextBox ID="txtSrcCity" runat="server" Width="225px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtSrcState">State</label><asp:TextBox ID="txtSrcState" runat="server" Width="40px" MaxLength="2" ReadOnly="true" />
                    <label for="txtSrcZip5">Zip</label><asp:TextBox ID="txtSrcZip5" runat="server" Width="50px" MaxLength="5" ReadOnly="true" />
                    &nbsp;-&nbsp;<asp:TextBox ID="txtSrcZip4" runat="server" Width="40px" MaxLength="4" ReadOnly="true" />
                    <br /><br />
                    <asp:Button ID="btnChooseAddress" runat="server" Text="Choose" style="margin-left:165px" CommandName="ChooseAddress" OnCommand="OnCommand" />
                    <br /><br />
                </fieldset>
            </div>
            <div class="address">
                <fieldset>
                    <legend>US Postal Address</legend>
                    <label for="txtUSPSName">Name</label><asp:TextBox ID="txtUSPSName" runat="server" Width="275px" MaxLength="40" ReadOnly="true" />
                    <label for="txtUSPSAddressLine1">Address</label><asp:TextBox ID="txtUSPSAddressLine1" runat="server" Width="250px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtUSPSAddressLine2">&nbsp;</label><asp:TextBox ID="txtUSPSAddressLine2" runat="server" Width="250px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtUSPSCity">City</label><asp:TextBox ID="txtUSPSCity" runat="server" Width="225px" MaxLength="40" ReadOnly="true" /><br />
                    <label for="txtUSPSState">State</label><asp:TextBox ID="txtUSPSState" runat="server" Width="40px" MaxLength="2" ReadOnly="true" />
                    <label for="txtUSPSZip5">Zip</label><asp:TextBox ID="txtUSPSZip5" runat="server" Width="50px" MaxLength="5" ReadOnly="true" />
                    &nbsp;-&nbsp;<asp:TextBox ID="txtUSPSZip4" runat="server" Width="40px" MaxLength="4" ReadOnly="true" />
                    <br /><br />
                    <asp:Button ID="btnChooseUSPSAddress" runat="server" Text="Choose" style="margin-left:165px" CommandName="ChooseUSPSAddress" OnCommand="OnCommand" />
                    <br /><br />
                </fieldset>
            </div>
            <div style="clear:left"></div>
            <div><asp:Label ID="lblMessage" runat="server" Text="" /></div>
        </div>
    </asp:View>
    </asp:MultiView>
</asp:Content>
