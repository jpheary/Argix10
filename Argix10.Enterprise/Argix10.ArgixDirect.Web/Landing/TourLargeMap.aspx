﻿<%@ page Title="Argix Direct - Transit Time Map" language="C#" autoeventwireup="false" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="keywords" content="location, map, terminals, service network" />
    <meta name="description" content="Map of Argix Direct's nationwide service network."/>
    <link href="styles/landing.css" rel="stylesheet" type="text/css" />
    <link href="styles/landing.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-19986845-1']);
        _gaq.push(['_trackPageview']);

        (function() {
        var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
        ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
        var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</head>
<body>
    <div id="container">
        <div id="topBorder"></div>
        <div id="sectionGap_Grey"></div>
        <div id="topBanner"><img runat="server" src="~/styles/images/Banner_landingPage-2.jpg"  alt="Banner" width="268" height="56" /></div>	
        <div id="sectionGap_thinGrey"></div>
        <div id="mapContainer"><img runat="server" src="~/styles/images/transittimes960_tourLink.jpg" alt="Terminal map" width="960" height="636"  /></div>
        <div id="footDiv"><img runat="server" src="~/styles/images/TOTALLogisticsSolution_footer.jpg" alt="Logo" width="200" height="18" /></div>
        <div id="copyrightDiv">Copyright © 2011 Argix Direct.  All  Rights Reserved.</div>
    </div>
</body>
</html>
