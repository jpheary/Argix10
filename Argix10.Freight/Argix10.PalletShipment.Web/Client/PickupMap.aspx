<%@ Page Title="Pickup Map" Language="C#" AutoEventWireup="true" CodeFile="PickupMap.aspx.cs" Inherits="PickupMap" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>Pallet Shipment Program</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href='http://fonts.googleapis.com/css?family=Michroma' rel='stylesheet' type='text/css'>
    <link href='http://fonts.googleapis.com/css?family=Open+Sans+Condensed:300,700,300italic' rel='stylesheet' type='text/css'>
    <link id="Link1" runat="server" href="~/App_Themes/Argix/Argix.css" rel="stylesheet" type="text/css" />
    <script type="text/jscript" src="http://code.jquery.com/jquery-1.9.1.js"></script>
    <script type="text/jscript" src="http://code.jquery.com/ui/1.10.3/jquery-ui.js"></script>
    <link rel="stylesheet" href="http://code.jquery.com/ui/1.10.3/themes/blitzer/jquery-ui.css" />
</head>
<body id="idBody" runat="server" style=" position:relative; margin-top:0; " >
<form id="idForm" runat="server" >
<img id="imgMap" runat="server" src="~/App_Themes/Argix/Images/pickupmap.jpg" alt="Pickup Map" style=" width:600px; height:400px; border:0;" />
</form>
</body>
</html>

