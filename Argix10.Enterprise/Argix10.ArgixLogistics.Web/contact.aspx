<%@ Page Language="C#" AutoEventWireup="true" CodeFile="contact.aspx.cs" Inherits="contact" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta http-equiv="Content-Type" content="text/html; charset=UTF-8" />
    <title>Argix :: Contact Us</title>
    <link href="https://fonts.googleapis.com/css?family=Michroma" rel="stylesheet" type="text/css" />
    <link href="argix_style.css" rel="stylesheet" type="text/css" />
    <style>
        .shhh { display: none; }
        input[type="text"] { font-size:1.0em; }
        textarea { font-size:1.0em; }
    </style>
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-34430942-1']);
        _gaq.push(['_trackPageview']);
        (function () {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = ('https:' == document.location.protocol ? 'https://ssl' : 'http://www') + '.google-analytics.com/ga.js';
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
</head>
<body>
    <form runat="server"> 
    <asp:ScriptManager ID="smPage" runat="server" EnableCdn="false" EnablePartialRendering="true" AsyncPostBackTimeout="600" ScriptMode="Auto" LoadScriptsBeforeUI="false"></asp:ScriptManager>
        <div class="container">
            <div class="navigation">
                <div style="float:left"><a href="index.html"><img src="images/index_01.gif" width="264" height="112" alt="Argix" style="border-style:none" /></a></div>
                <div style="float:right; padding-top:30px;">
                    <script type="text/javascript" src="menu/milonic_src.js"></script>
                    <script type="text/javascript" src="menu/mmenudom.js"></script> 
                    <script type="text/javascript" src="menu/menu_data.js"></script>
                    <script type="text/javascript">
	                    with (milonic = new menuname("Main Menu")) {
	                        alwaysvisible = 1;
	                        orientation = "horizontal";
	                        style = AllImageStyle;
	                        position = "relative";
	                        aI("title=The Argix Difference;image=images/argix-nav_01.gif;overimage=images/argix-nav-over_01.gif;pageimage=images/argix-nav-over_01.gif;showmenu=The_Argix_Difference;status=The Argix Difference;url=difference.html;");
	                        aI("title=Transportation;image=images/argix-nav_02.gif;overimage=images/argix-nav-over_02.gif;pageimage=images/argix-nav-over_02.gif;showmenu=Transportation;status=Transportation;url=transportation.html;");
	                        aI("title=Distribution;image=images/argix-nav_03.gif;overimage=images/argix-nav-over_03.gif;pageimage=images/argix-nav-over_03.gif;showmenu=Distribution;status=Distribution;url=distribution.html;");
	                        aI("title=Supply Chain Management;image=images/argix-nav_04.gif;overimage=images/argix-nav-over_04.gif;pageimage=images/argix-nav-over_04.gif;showmenu=Supply_Chain_Management;status=Supply Chain Management;url=supply_chain.html;");
	                        aI("title=Technology Edge;image=images/argix-nav_05.gif;overimage=images/argix-nav-over_05.gif;pageimage=images/argix-nav-over_05.gif;status=Technology Edge;url=technology-difference.html;");
	                        aI("title=Brands Served;image=images/argix-nav_06.gif;overimage=images/argix-nav-over_06.gif;pageimage=images/argix-nav-over_06.gif;status=Brands Served;url=brands-served.html;");
	                        aI("title=Pallet Shipment;image=images/argix-nav_07.gif;overimage=images/argix-nav-over_07.gif;pageimage=images/argix-nav-over_07.gif;status=Pallet Shipment;url=LTDdelivery.html;");
	                    }
	                    drawMenus();
	                </script>
                </div>
                <div style="clear:both;"></div>
            </div>
            <div class="login-home"><a href="login.aspx" style=""><img src="images/login.png" alt="Login" style="border:0 none" /></a></div>
            <div class="header-home"><span class="headline"></span><span class="headline-red"></span></div>
            <div class="content">
                <span class="big_caps">R</span><span class="titles">equest Information</span>
                <p class="body-copy">Contact us to find out more on how the Argix Network can provide your organization with a faster, smarter and more affordable logistics solution. You'll enjoy a logistics program that is as unique as your brand.<br />
                <br />
                There's a better way to improve your performance, let us show you how.</p>
                <div style="padding-left:60px;">
                    <asp:UpdatePanel runat="server" ID="pnlMsg" UpdateMode="Always"><ContentTemplate><asp:Label ID="lblMsg" runat="server" Text="" /></ContentTemplate></asp:UpdatePanel>
                    <asp:ValidationSummary ID="vsContact" runat="server" ValidationGroup="vgContact" Font-Size="12px" Font-Bold="True" ForeColor="#ee2a24" DisplayMode="BulletList" ShowSummary="false" ShowMessageBox="true"  />
                    <table style="width:500px; border-style:none">
                        <tr>
                            <td>
                                <p class="shhh"><input name="botTrap" id="botTrap" type="text" class="shhh" /></p>
                                <table style="width:250px">
                                    <tr>
                                        <td style="width:75px; text-align:right; vertical-align:middle"><strong>Name:</strong></td>
                                        <td style="text-align:left; vertical-align:middle"><asp:TextBox ID="txtName" runat="server" Width="200px" MaxLength="40" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; vertical-align:middle"><strong>Company:</strong></td>
                                        <td style="text-align:left; vertical-align:middle"><asp:TextBox ID="txtCompany" runat="server" Width="200px" MaxLength="40" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; vertical-align:middle"><strong>Title:</strong></td>
                                        <td style="text-align:left; vertical-align:middle"><asp:TextBox ID="txtTitle" runat="server" Width="200px" MaxLength="20" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-top:10px; padding-left:57px; text-align:left; vertical-align:middle">
                                            <table style="border-style:none">
                                                <tr><td style="text-align:left; vertical-align:middle"><asp:CheckBox ID="chkBrochure" runat="server" Text="Send a Brochure" Checked="false" /></td></tr>
                                                <tr><td style="text-align:left; vertical-align:middle"><asp:CheckBox ID="chkAssessment" runat="server" Text="Free Logistics Assessment" Checked="false" /></td></tr>
                                                <tr><td style="text-align:left; vertical-align:middle"><asp:CheckBox ID="chkTour" runat="server" Text="Schedule a Tour" Checked="false" /></td></tr>
                                                <tr><td style="text-align:left; vertical-align:middle"><asp:CheckBox ID="chkContact" runat="server" Text="Contact Me" Checked="true" /></td></tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td style="vertical-align:top">
                                <table style="width:250px">
                                    <tr>
                                        <td style="width:75px; text-align:right; vertical-align:middle"><strong>Email:</strong></td>
                                        <td style="text-align:left; vertical-align:middle"><asp:TextBox ID="txtEmail" runat="server" Width="200px" MaxLength="50" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; vertical-align:middle"><strong>Telephone:</strong></td>
                                        <td style="text-align:left; vertical-align:middle"><asp:TextBox ID="txtTele" runat="server" Width="200px" MaxLength="12" /></td>
                                    </tr>
                                    <tr>
                                        <td style="text-align:right; vertical-align:middle"><strong>Address:</strong></td>
                                        <td style="text-align:left; vertical-align:middle"><asp:TextBox ID="txtAddress" runat="server" Width="200px" MaxLength="100" /></td>
                                    </tr>
                                    <tr>
                                        <td colspan="2" style="padding-top:17px; padding-left:75px; vertical-align:middle">
                                            Additional Requests:<br />
                                            <asp:TextBox ID="txtRequests" runat="server" TextMode="MultiLine" Height="55px" Columns="1" Rows="4" MaxLength="100" style="width:200px; margin-top:8px" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    <div style="padding-top:25px; padding-left:70px">
                        <asp:UpdatePanel runat="server" ID="upnlContact" UpdateMode="Conditional" ChildrenAsTriggers="true" RenderMode="Block">
                        <ContentTemplate>
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="vgContact" CssClass="submit" CommandName="Submit" OnCommand="OnCommand" />
                        </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                    <asp:RequiredFieldValidator ID="rfvName" runat="server" ControlToValidate="txtName" ErrorMessage="Name is required." Display="None" ValidationGroup="vgContact" />
                    <asp:RequiredFieldValidator ID="rfvCompany" runat="server" ControlToValidate="txtCompany" ErrorMessage="Company is required." Display="None" ValidationGroup="vgContact" />
                </div>
            </div>
            <div class="side_bar_industries">
                <p class="sub-nav">
                    <span class="sub-nav-bigger-red">Argix Logistics</span><br />100 Middlesex Center Blvd.<br />Jamesburg, NJ 08831<br />Tel: 732.656.2550
                </p>
            </div>
            <div style="clear:both;"></div>
            <div class="red_swirl"><img src="images/argix_swoosh.jpg" width="960" height="90" alt="red swirl" /></div>
            <div class="seo">Argix Logistics is a leading provider of transportation, distribution, and supply chain management in the continental United States.</div>
            <div class="footer">
                <p>Argix Logistics | 732.656.2550 | Copyright 2012 - 2014 | <a href="privacy.html" target="_self" title="Privacy Notice" style="width:150px; color:black; text-decoration:underline;">Privacy Notice</a></p>
            </div>
        </div>
    </form>
    <script type="text/javascript">
        qcdata = {} || qcdata;
        (function () {
            var elem = document.createElement('script');
            elem.src = (document.location.protocol == "https:" ? "https://secure" : "http://pixel") + ".quantserve.com/aquant.js?a=p-et67SKx8cdCYA";
            elem.async = true;
            elem.type = "text/javascript";
            var scpt = document.getElementsByTagName('script')[0];
            scpt.parentNode.insertBefore(elem, scpt);
        } ());
        var qcdata = { qacct: 'p-et67SKx8cdCYA', orderid: '', revenue: '' };
    </script>
    <noscript><img src="//pixel.quantserve.com/pixel/p-et67SKx8cdCYA.gif?labels=_fp.event.Default" style="display: none;" border="0" height="1" width="1" alt="Quantcast"/></noscript>
</body>
</html>
