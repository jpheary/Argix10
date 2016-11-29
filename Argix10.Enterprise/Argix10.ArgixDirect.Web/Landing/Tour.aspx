<%@ Page Title="Argix Direct - Tour" Language="C#" AutoEventWireup="true" CodeFile="Tour.aspx.cs" Inherits="Tour" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <link href="~/styles/landing.css" rel="stylesheet" type="text/css" />
    <link href="~/styles/landing.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="~/scripts/jquery.js"></script>
    <script type="text/javascript" src="~/scripts/jquery.validate.js"></script>
    <script type="text/javascript">
        $(document).ready(function() {
        $('#infoForm').validate({
        errorPlacement: function(error, element) {
        error.appendTo( element.parent("td").next("td") );
        }, 								// end errorPlacement
        rules: {
        name: {
        required: true  
        },
        state: {
        required: true  
        },   
        userOpts: {
        required: true  
        },   
        email: {
        required: true,
        email: true
        }
        },									// end rules
        messages: {
        name: {
        required: "Required"
        },
        state: {
        required: "Required"
        },
        email: {
        required: "Required",
        email: "Invalid"
        }
        }									// end messages
        });  //end validate()
        }); // end ready()
    </script>
    <script type="text/javascript">
        function isRequiredSet(selectedOption) {
        var option = selectedOption.value;
        if (option == "Phone")
        {
        //var phoneClass = document.getElementById('phone').className;
        jQuery("#phone").rules("add", "required");
        //jQuery("#phone").messages("add", "Required");
        } 
        else 
        {
        jQuery("#phone").rules("remove", "required");
        }
        //alert("user selected " + option);
        }
    </script>
    <script type="text/javascript">
        var element = document.getElementById('userOpts').value;
        alert("user selected " + element);
    </script>
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
    <div id="topBanner"><img runat="server" src="~/styles/images/logo.jpg" alt="Banner" width="268" height="56" /></div>	
    <div id="sectionGap_thinGrey"></div>
    <div id="contentTopTour"> 
        <h2 class="contentTop">Attention NJ, NYC &amp; Philadelphia metro area shippers:</h2>
        <p class="content14bold">Argix Direct invites you to take a tour of our National Sort Facility right in your own backyard.</p>
        <p> Do you need a solution for either vendor consolidation or final mile delivery? Are you looking for an East Coast based local, regional, or national solution to reduce your transportation cost? Take advantage 
        of the service performance and flexibility associated with having a <span class="content14bold"> <strong>National Sort Facility</strong></span>  in your own backyard.</p>
        <p>Located just 45 miles from Manhattan and 60 miles from Philadelphia, Argix Direct’s <strong>National Sort Facility</strong> in Jamesburg, NJ is ideally located to reduce the costs of
        distribution centers, vendor shipping docks and warehouses in the region. Our facility is over 300,000 square feet and features a fully automated sortation system which is capable of 
        sorting over 220,000 cartons per day. Our <strong>National Sort Facility</strong> services our 42 delivery terminals across the country, where we can provide either standard 1–6 day 
        delivery (as shown in the map below) or customized delivery windows to meet your logistical needs. Fill out the form below to arrange a tour of the facility or just to have a conversation to 
        determine if Argix Direct is a fit for your business.</p>
    </div>		
    <div id="sectionGap_thinGrey"></div>
    <form id="infoForm" runat="server" name="infoForm" method="post" action="~/Landing/Tour.aspx" > 
        <div id="contentMid">   
            <div id="contentForm" style="visibility:visible">  
                <h3 class="contentForm">Contact us today:</h3> 
                <table cellspacing="0" cellpadding="3" width="190px">
                    <tr>
                        <td width="50px"> <label class="contactLabel"> Name:</label></td>
                        <td width="120px"><input class="contactInput required" name="name"  type="text" title="Required" /> </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="50px"> <label class="contactLabel"> Email:</label> </td>
                        <td width="120px"><input class="contactInput required email" name="email"  type="text" title="Required" /> </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="50px"> <label class="contactLabel"> Phone:</label> </td>
                        <td width="120px"><input class="contactInput" id="phone" name="phone" type="text" title="Required" /> </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td width="50px"> <label class="contactLabel"> Company:</label> </td>
                        <td width="120px"><input class="contactInput" name="company" type="text"  /> </td>
                        <td></td>
                    </tr>		        
                    <tr>
                        <td width="50px"> <label class="contactLabel"> Address:</label> </td>
                        <td width="120px"><input class="contactInput" name="address" type="text" value=""  /> </td>
                        <td></td>
                    </tr>      
                    <tr>
                        <td width="50px"> <label class="contactLabel"> State:</label></td>
                        <td width="120px">
                            <SELECT  name="state"  class="required" title="Required" > <OPTION  value="" selected="selected" >-- Please Select --</OPTION><OPTION value=NonUS>Non US</OPTION><OPTION value=AK>Alaska</OPTION><OPTION value=AL>Alabama</OPTION><OPTION value=AR>Arkansas</OPTION><OPTION value=AZ>Arizona</OPTION><OPTION value=CA>California</OPTION><OPTION value=CO>Colorado</OPTION><OPTION value=CT>Connecticut</OPTION><OPTION value=DC>D.C.</OPTION><OPTION value=DE>Delaware</OPTION><OPTION value=FL>Florida</OPTION><OPTION value=GA>Georgia</OPTION><OPTION value=HI>Hawaii</OPTION><OPTION value=IA>Iowa</OPTION><OPTION value=ID>Idaho</OPTION><OPTION value=IL>Illinois</OPTION><OPTION value=IN>Indiana</OPTION><OPTION value=KS>Kansas</OPTION><OPTION value=KY>Kentucky</OPTION><OPTION value=LA>Louisiana</OPTION><OPTION value=MA>Massachusetts</OPTION><OPTION value=MD>Maryland</OPTION><OPTION value=ME>Maine</OPTION><OPTION value=MI>Michigan</OPTION><OPTION value=MN>Minnesota</OPTION><OPTION value=MO>Missouri</OPTION><OPTION value=MS>Mississippi</OPTION><OPTION value=MT>Montana</OPTION><OPTION value=NC>North Carolina</OPTION><OPTION value=ND>North Dakota</OPTION><OPTION value=NE>Nebraska</OPTION><OPTION value=NH>New Hampshire</OPTION><OPTION value=NJ>New Jersey</OPTION><OPTION value=NM>New Mexico</OPTION><OPTION value=NV>Nevada</OPTION><OPTION value=NY>New York</OPTION><OPTION value=OH>Ohio</OPTION><OPTION value=OK>Oklahoma</OPTION><OPTION value=OR>Oregon</OPTION><OPTION value=PA>Pennsylvania</OPTION><OPTION value=RI>Rhode Island</OPTION><OPTION value=SC>South Carolina</OPTION><OPTION value=SD>South Dakota</OPTION><OPTION value=TN>Tennessee</OPTION><OPTION value=TX>Texas</OPTION><OPTION value=UT>Utah</OPTION><OPTION value=VA>Virginia</OPTION><OPTION value=VT>Vermont</OPTION><OPTION value=WA>Washington</OPTION><OPTION value=WI>Wisconsin</OPTION><OPTION value=WV>West Virginia</OPTION><OPTION value=WY>Wyoming</OPTION></SELECT></td>
                        <td></td>              
                    </tr>
                    <tr>
                        <td width="50px"> <label class="contactLabel" > Options:</label> </td>
                        <td width="120px">
                            <select name="userOpts"  class="required" title="Required" size="1" id="userOpts" onchange="isRequiredSet(this)">
                            <option value="">-- Please Select --</option>
                            <option value="Email">Email Me  </option>
                            <option value="Phone">Phone Me  </option>
                            <option value="Brochure">Send Brochure </option>
                            </select>
                        </td>
                        <td></td>
                    </tr>   
                </table>
                <div id="submit"><input name="SUBMIT" value="Submit" type="submit" /></div>
            </div>
            <div id="contentThanks" style="visibility:hidden">
                <h4>THANK YOU </h4>
                <p> Thank you for your request.  We will contact you soon.         </p> 
                <p>Email: <a href="mailto:info@argixdirect.com">info@argixdirect.com</a></p>
                <p>Phone: 732.656.2550 </p>
            </div>
            <div id="graphicMidRight"><a runat="server" href="~/Landing/TourLargemap.aspx" target="_blank"><img runat="server" src="~/styles/images/transitMap.png" alt="Transit Map" /></a></div>
        </div>
        <script type="text/javascript" >
            function showThankyou(){
            document.getElementById("contentThanks").style.visibility="visible";
            document.getElementById("contentForm").style.visibility="hidden";
            }
        </script>
    </form>
    <div id="sectionGap_thinGrey"></div>
    <div id="contentBot">
            <div id="leftContentBot">
                <h4>  About <span class="redBoldCaps">Argix</span>►<span class="redBoldCaps">Direct</span></h4>
                <p>For over 30 years <a href="http://www.argixdirect.com"><span class="redBoldCaps">Argix</span><span style="color:#000">►</span><span class="redBoldCaps">Direct</span> </a> 
                has moved parcels, pallets and mailings quickly and reliably to retail stores, post offices, distribution centers, warehouses, industrial centers and business parks. Now, with our Direct to Consumer service we also provide a transportation solution for parcel delivery to residential locations. </p>
            </div>
            <div id="imageRight"><img runat="server" src="~/styles/images/noSurcharges.jpg" alt="No surcharges" width="151" height="134" /></div>
        </div>
    <div id="footDiv"><img runat="server" src="~styles/images/TOTALLogisticsSolution_footer.jpg"  alt="Logo." width="200" height="18"/></div>
    <div id="copyrightDiv"> Copyright  2011 Argix Direct. All  Rights Reserved.</div>
</div>
</body>
</html>
