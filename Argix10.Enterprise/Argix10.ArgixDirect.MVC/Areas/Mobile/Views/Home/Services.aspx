<%@ Page Title="Argix Direct - Services" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Services</h2>
    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/services.gif") %>" alt="Services" width="120" height="120"/>
	<div  class="textContainer"> 
        <p>Argix Direct will be your partner in developing and supporting a TOTAL Logistics solution that is right for you. We develop a customized solution that fits your retailing needs. At Argix Direct, we have extensive experience in both retail and logistics. We will provide you with support from Line Haul to DC bypass; from Import Deconsolidation to Storage - tailored to your needs.</p>
        <p>Contact us to see how Argix Direct can help develop your TOTAL Logistics Solution. </p>
    </div>
    <br />
    <a class="pageMenu" href="javascript: unhide('storeDelivery');">Store Delivery</a>
    <div id="storeDelivery" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/deliveries.jpg") %>"  alt="Store Deliveries" width="120" />
	    <div class="textContainer"> 
            <p>Our store delivery program is customized for the retailer, on either a regional or national basis. Argix Direct offers both DC pickup or bypass to consolidate all components into a single store delivery.</p>
            <p>Argix Direct delivers in virtually every mall, strip, and outlet center throughout the country every day. This gives our retail clients expedited freight at non-expedited rates. All Argix Direct deliveries are inside with consistent transit times– absolutely critical for retail.</p>
            <p>Argix Direct also offers EDI capabilities, drop trailers, and zone skip if necessary.</p>
            <p>We have 42 delivery terminals nationwide that support your time sensitive shipments. As always, you can fully track your shipments with our proprietary <a href="<%: Url.Action("SmartScan", "Home")%>"><span class="redBoldLink">Smartscan</span></a> technology. Our goal is to support our clients with less internal handling and lower overall costs.</p>
	    </div>
    </div>
    <a class="pageMenu" href="javascript: unhide('consumerDirect');">Consumer Direct</a>
    <div id="consumerDirect" class="hidden">
        <img class="imgFloatRight" src="<%= Url.Content("~/content/images/house.jpg") %>" alt="Consumer Direct." width="120" />
        <div  class="textContainer"> 
            <p>The Argix Direct – Consumer Direct  program offers a solution for your catalog, e-commerce, seasonal and small  parcel shipping and delivery needs to the consumer.</p>
            <p>Working with the United  States Postal Service system at both the BMC (Bulk Mail Center) and DDU  (Destination Delivery Unit) level, Argix Direct can leverage its current  network to provide an efficient and cost effective program for you.</p>
            <p>The Argix Direct – Consumer  Direct program can provide you with a customized solution that provides the  same level of system support, package tracking, security and customer service  that our current customers enjoy with our other service offerings.</p>
            <p>&nbsp;</p>
        </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('dcBypass');">DC Bypass</a>
    <div id="dcBypass" class="hidden">
 	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/dcbypass.jpg") %>"  alt="DC Bypass" width="120" />
	    <div  class="textContainer"> 
            <p>Argix Direct has the expertise and the ability to support your logistics needs through bypassing the DC. This is often necessary during peak seasons or for specialized retail programs.</p>
            <p>We have the ability to receive your freight directly into any of our terminals nationwide. We also have the expertise to manage receiving from multiple points: Distribution Center, Imports, Storage Facilities, or Vendors. Argix Direct will consolidate these shipments in our National Sort Center and create a single store delivery.</p>
            <p>You always have the ability to track shipments through our proprietary <a href="<%: Url.Action("SmartScan", "Home")%>"><span class="redBoldLink">Smartscan</span></a> technology.</p>
            <p>With our DC bypass service, you can free up space in your DC, minimize handling and lower your overall costs.<br />
            </p>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('freightPickup');">Freight Pickup</a>
    <div id="freightPickup" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/freight.jpg") %>"  alt="Freight Pickup" width="120" />
        <div  class="textContainer"> 
            <p>Argix Direct provides our clients with freight pickup support. We have long standing relationships with an extensive network of carriers with nationwide capabilities.</p>
            <p>Argix Direct provides single store deliveries from multiple shipments. This provides our clients with significant cost savings.</p>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('importDeconsolidation');">Import Deconsolidation</a>
    <div id="importDeconsolidation" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/deconsolidation.jpg") %>" alt="Import Deconsolidation" width="120" />
	    <div  class="textContainer"> 
            <p>With Argix Direct’s strategic location near the New Jersey Port, we can offer exceptional support for your Import Deconsolidation needs. Our sorting facility  gives you the capability to consolidate multiple shipments.</p>
            <p>As always, you can fully track your shipments with our proprietary <a href="<%: Url.Action("SmartScan", "Home")%>"><span class="redBoldLink">Smartscan</span></a> technology. Our goal is to support our clients with less internal handling and reduce overall costs.</p>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('consolidation');">Consolidation</a>
    <div id="consolidation" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/consolidation.jpg") %>" alt="Consolidation" width="120" />
	    <div  class="textContainer"> 
            <p>Argix Direct specializes in single store deliveries from multiple shipments. We have extensive experience in managing multiple points (DC, vendors, imports, storage) into single deliveries. This helps to lower costs for shipments.</p>
            <p>Consolidation also provides in-store cost savings by improved scheduling, reduced paperwork and shrink potential.</p>
            <p>Additionally, Argix Direct’s expertise in consolidation helps our clients free up space in their DC’s.</p>
            <p>You always have the ability to track shipments through our proprietary <a href="<%: Url.Action("SmartScan", "Home")%>"><span class="redBoldLink">Smartscan</span></a> technology.<br />
            </p>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('lineHaul');">Line Haul</a>
    <div id="lineHaul" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/linehaul.jpg") %>"  alt="Line Haul" width="120" />
	    <div  class="textContainer"> 
            <p>At Argix Direct we pride ourselves on our long-standing relationships with an extensive network of carriers. This provides our clients with excellent service for their store delivery needs.</p>
            <p>Argix Direct has extensive regional and national capabilities, assuring our clients the line haul support that they deserve at very competitive rates.</p>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('warehousing');">Warehousing</a>
    <div id="warehousing" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/warehousing.jpg") %>"  alt="Warehousing" width="120" />
	    <div  class="textContainer"> 
            <p>Argix Direct offers full service storage facilities for our clients. This can be for temporary or long term needs, to support new store openings, free up space in the DC, or provide shipment consolidation opportunities.</p>
            <p>Our storage facility is tied directly into our National Sort Center. This helps to provide inventory security with both inbound-outbound scanning. You can also receive customized reporting.</p>
            <p>Our storage option is a great service to support split shipments for stores with space constraints or to merge with additional outbound shipping.</p>
            <p>Argix Direct provides retailers with the ability to support seasonal buys, bulk buys or special merchandise release dates with both storage options and DC bypass.</p>
	    </div>	  
    </div>
</asp:Content>
