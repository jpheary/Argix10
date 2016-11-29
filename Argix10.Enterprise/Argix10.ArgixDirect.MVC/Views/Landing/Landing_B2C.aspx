<%@ page language="C#" autoeventwireup="true" CodeFile="~/landing_B2C.aspx.cs" Inherits="landing_B2C" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">


<head id="Head1" runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>Argix Direct - B2C </title>
<link href="CSS/LP_layout.css" rel="stylesheet" type="text/css" />
<link href="CSS/LP_format.css" rel="stylesheet" type="text/css" />


<script type="text/javascript" src="js/jquery.js"></script>
<script type="text/javascript" src="js/jquery.validate.js"></script>

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

<%--<body>--%>
<body>
 
<div id="container">
<div id="topBorder"> </div>
<div id="sectionGap_Grey"> </div>

<div id="topBanner"><img src="content/images/Banner_landingPage-2.jpg"  alt="Banner." width="268" height="56" /></div>	
	
<div id="sectionGap_thinGrey"> </div>
<div id="contentTop"> 
    
    <h2 class="contentTop">Are B-2-C shipping costs eating into your margins?</h2>
    <p class="content14bold">Are you looking for better cost effective options to fulfill orders for your residential customers?</p>
    
<p> <a href="http://www.argixdirect.com"><span class="redBoldCaps">Argix</span><span style="color:#000">►</span><span class="redBoldCaps">Direct</span> </a>
offers an alternative to high priced residential shipping with our Consumer Direct residential parcel service. We will pick up, process, sort and deliver your shipment to any address in the continental United States, working in conjunction with the United States Postal Service.</p>
     <p>Our partnership with the United States Postal Service allows us to take advantage of our nationwide distribution network coupled with the USPS's unrivaled reach to every residence in the U.S. And we include standard Saturday delivery at no additional charge, while providing full tracking visibility from  pick-up through delivery.</p>
</div>		<!--contentTop--> 

<div id="sectionGap_thinGrey"> </div>

 <form id="infoForm" runat="server" name="infoForm" method="post" action="landing_B2C.aspx" > 

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
                <td width="50px"> <label class="contactLabel"> State:</label> </td>
                <td width="120px">
                <SELECT  name="state"  class="required" title="Required" > <OPTION  value="" selected="selected" >-- Please Select --</OPTION><OPTION value=NonUS>Non US</OPTION><OPTION value=AK>Alaska</OPTION><OPTION value=AL>Alabama</OPTION><OPTION value=AR>Arkansas</OPTION><OPTION value=AZ>Arizona</OPTION><OPTION value=CA>California</OPTION><OPTION value=CO>Colorado</OPTION><OPTION value=CT>Connecticut</OPTION><OPTION value=DC>D.C.</OPTION><OPTION value=DE>Delaware</OPTION><OPTION value=FL>Florida</OPTION><OPTION value=GA>Georgia</OPTION><OPTION value=HI>Hawaii</OPTION><OPTION value=IA>Iowa</OPTION><OPTION value=ID>Idaho</OPTION><OPTION value=IL>Illinois</OPTION><OPTION value=IN>Indiana</OPTION><OPTION value=KS>Kansas</OPTION><OPTION value=KY>Kentucky</OPTION><OPTION value=LA>Louisiana</OPTION><OPTION value=MA>Massachusetts</OPTION><OPTION value=MD>Maryland</OPTION><OPTION value=ME>Maine</OPTION><OPTION value=MI>Michigan</OPTION><OPTION value=MN>Minnesota</OPTION><OPTION value=MO>Missouri</OPTION><OPTION value=MS>Mississippi</OPTION><OPTION value=MT>Montana</OPTION><OPTION value=NC>North Carolina</OPTION><OPTION value=ND>North Dakota</OPTION><OPTION value=NE>Nebraska</OPTION><OPTION value=NH>New Hampshire</OPTION><OPTION value=NJ>New Jersey</OPTION><OPTION value=NM>New Mexico</OPTION><OPTION value=NV>Nevada</OPTION><OPTION value=NY>New York</OPTION><OPTION value=OH>Ohio</OPTION><OPTION value=OK>Oklahoma</OPTION><OPTION value=OR>Oregon</OPTION><OPTION value=PA>Pennsylvania</OPTION><OPTION value=RI>Rhode Island</OPTION><OPTION value=SC>South Carolina</OPTION><OPTION value=SD>South Dakota</OPTION><OPTION value=TN>Tennessee</OPTION><OPTION value=TX>Texas</OPTION><OPTION value=UT>Utah</OPTION><OPTION value=VA>Virginia</OPTION><OPTION value=VT>Vermont</OPTION><OPTION value=WA>Washington</OPTION><OPTION value=WI>Wisconsin</OPTION><OPTION value=WV>West Virginia</OPTION><OPTION value=WY>Wyoming</OPTION></SELECT></TD>
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
      <div id="submit">
     <input name="SUBMIT" value="Submit" type="submit" />
        <!--<input name="SUBMIT" value="Submit" type="submit"  />-->
      </div>
    </div>     <!--contentForm-->


    <div id="contentThanks" style="visibility:hidden">  <%-- thankYouDiv--%>
         <h4>THANK YOU </h4>
        <p> Thank you for your request.  We will contact you soon.         </p> 
      <p>Email: <a href="mailto:info@argixdirect.com">info@argixdirect.com</a></p>
       <p>Phone: 732.656.2550 </p>
  </div>     <!--thanYouDiv-->
  
  

   <div id="bulletsRight">
       <p class="content16bold">Our <span  class="redBoldLink"> <u>Direct to Consumer</u> </span> product features:</p>
       <ul>
       <li>►Competitive transit times </li>
       <li>►No surcharges (except fuel)  </li>
       <li>►Accurate,  customized billing  </li>
       <li>►Service to PO Boxes, APO’s, and FPO’s  </li>
       <li>►Insurance coverage through final delivery </li>
       </ul>

  </div>        <!--bulletsRight--> 
  </div>     <!--contentMid-->
  
  <script type="text/javascript" >
function showThankyou(){
document.getElementById("contentThanks").style.visibility="visible";
document.getElementById("contentForm").style.visibility="hidden";
}
</script>

</form>
<div id="sectionGap_thinGrey"> </div>

  
  <div id="contentBot">
  <div id="leftContentBot">
  <h4>  About <span class="redBoldCaps">Argix</span>►<span class="redBoldCaps">Direct</span></h4>
      <p>For over 30 years 
      <a href="http://www.argixdirect.com"><span class="redBoldCaps">Argix</span><span style="color:#000">►</span><span class="redBoldCaps">Direct</span> </a> 
      has moved parcels, pallets and mailings quickly and reliably to retail stores, post offices, distribution centers, warehouses, industrial centers and business parks. Now, with our Direct to Consumer service we also provide a transportation solution for parcel delivery to residential locations. </p>
 </div>     <!--leftContentBot  -->

  <div id="imageRight">
      <img src="content//noSurcharges.jpg" alt="No surcharges." width="151" height="134" />
  </div>     <!--imageRight-->
 </div>  	  <!--contentBot  -->

<div id="footDiv"><img src="content//TOTALLogisticsSolution_footer.jpg"  alt="Logo." width="200" height="18"/></div>
<div id="copyrightDiv"> Copyright  2011 Argix Direct. All  Rights Reserved.</div>

</div>		<!-- #container -->




</body>
</html>
