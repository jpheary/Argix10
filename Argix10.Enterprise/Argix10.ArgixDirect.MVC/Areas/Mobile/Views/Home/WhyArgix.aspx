<%@ Page Title="Argix Direct - Why Argix" Language="C#" MasterPageFile="~/Areas/Mobile/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="cMain" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Why Argix</h2>
    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/whyargix.jpg") %>" alt="Why Argix?" width="120" />
	<div class="textContainer"> 
        <p>Argix Direct will provide you with a fully customized logistics solution for your specific retailing needs. We provide a lower cost solution by synthesizing shipments, eliminating multiple deliveries, and freeing up capacity in your DC.</p>
        <p>Argix Direct has a proprietary Smartscan system that provides immediate and detailed shipment reportings. Our professional customer service representatives are always ready to support your logistical needs. </p>
	</div>
    <br />
    <a class="pageMenu" href="javascript: unhide('customerService');">Customer Service</a>
    <div id="customerService" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/customerservice.jpg") %>" alt="Customer Service" width="120" />
	    <div class="textContainer"> 
            <h5>Argix Direct addresses our customer’s needs from every angle</h5>
            <ul>
                <li>System designed for the needs of our customers</li>
                <li>Dedicated Service Representatives</li>
                <li>Track packages using your internal numbers</li>
                <li>Easily access snapshots of truck loads, all the way down to specific packages</li>
                <li>Know and rely on specific shipment details</li>
                <li>Customized reporting</li>
                <li>Advanced CRM software</li>
                <li>Online Tracking</li>
            </ul>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('shipmentIntegrity');">Shipment Integrity &amp; Security</a>
    <div id="shipmentIntegrity" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/integrity.jpg") %>" alt="Shipment Integrity" width="120" />
	    <div class="textContainer"> 
            <p>The VisTrak Visitor Management Solution is a biometric system that Argix Direct uses to ensure security for shipments and safety for drivers. This program keeps track of who is entering or exiting an Argix Direct facility and verifies that access is permitted, all with the touch of a finger. This results in enhanced security along with faster entry and egress times. </p>
            <p>The <a href="<%: Url.Action("SmartScan", "Home")%>"><span class="redBoldLink">Smartscan</span></a> System keeps track of individual packages throughout the sorting and delivery system. This system helps to make sure that shipments are both timely and accurate. Each package can be tracked either using your own or Argix Direct’s item/code number.<br />
            </p>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('customizedSolutions');">Customized Solutions</a>
    <div id="customizedSolutions" class="hidden">
	    <img class="imgFloatRight" src="<%= Url.Content("~/content/images/solutions.jpg") %>" alt="Customized Solutions." width="120" />
	    <div class="textContainer"> 
            <h4>Shrink Your Supply Chain<br /><span class="redText">Expand Your Competitive Edge</span></h4>
            <img src="<%= Url.Content("~/content/images/blackbar.jpg") %>" alt="" width="100" height="3" />
            <p class="style1">Exclusive technology-driven design for specialty retail store deliveries:</p>
            <ul>
                <li>Custom Store Delivery Program</li>
                <li>Increased Accuracy</li>
                <li>Expedited Transit Times</li>
                <li>Efficient Handling</li>
                <li>Greater Flexibility</li>
                <li>Lower Cost</li>
                <li><a href="<%: Url.Action("SynthesizedShipments", "Home")%>"><span class="redBoldLink">Synthesized Shipments</span></a></li>
            </ul>
	    </div>	  
    </div>
    <a class="pageMenu" href="javascript: unhide('transitTimes');">Transit Times : Time Sensitive Solutions</a>
    <div id="transitTimes" class="hidden">
	<div class="textContainer">
        <br />
        <img src="<%= Url.Content("~/content/images/transittimes.jpg") %>" alt="Transit Times" width="270" />
        <p class="footnote">*Transit times from National Sort Center in New Jersey. </p>
        </div>  
    </div>
    <a class="pageMenu" href="javascript: unhide('volumeTotals');">Volume Totals : 2010</a>
    <div id="volumeTotals" class="hidden">
  	    <div class="textContainer"> 
            <table>
                <tr><td class="execName">► Weight</td><td class="execTerm">540 million pounds</td></tr>
                <tr><td class="execName">► Cartons</td><td class="execTerm">32 million</td></tr>
                <tr><td class="execName">► Deliveries</td><td class="execTerm">816,000</td></tr>
                <tr><td class="execName">► Locations</td><td class="execTerm">11,900+</td></tr>
            </table>      
        </div>	  
    </div>
</asp:Content>
